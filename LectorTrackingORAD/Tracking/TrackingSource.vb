<Serializable()> Public Class TrackingSource
  Public TrackingValues As New List(Of TrackingValue)
  Public Port As Integer = 0
  Public Host As String
  Public Selected As Boolean = False

  Public TrackingHost As TrackingHost

#Region "Events"
  Public Event ListenerStarted()
  Public Event ListenerStopped()
  Public Event NewTrackingValue(ByVal CiTrackingValue As TrackingValue)
  Public Event TrackignValuesCleared()
#End Region

  Private WithEvents TrackingListener As TrackingListener.ATrackingListener

#Region "Values"
  Public Sub AddTrackingValue(ByVal CiTrackingValue As TrackingValue)
    Try
      CiTrackingValue.Index = Me.TrackingValues.Count
      Me.TrackingValues.Add(CiTrackingValue)
      RaiseEvent NewTrackingValue(CiTrackingValue)
    Catch ex As Exception
    End Try
  End Sub

  Public Sub ClearTrackingValues()
    Try
      Me.TrackingValues.Clear()
      RaiseEvent TrackignValuesCleared()
    Catch ex As Exception
    End Try
  End Sub

  Public Function GetValueByTime(ByVal diTime_ms As Double, Optional ByVal niStartIndex As Integer = 0, Optional ByVal biInterpolate As Boolean = False) As TrackingValue
    Dim CRes As TrackingValue = Nothing
    Dim nResIndex As Integer = 0
    Try
      If niStartIndex >= 0 And niStartIndex < Me.TrackingValues.Count Then
        CRes = Me.TrackingValues(niStartIndex)
        For nIndex As Integer = niStartIndex To Me.TrackingValues.Count - 1
          nResIndex = nIndex
          If Me.TrackingValues(nIndex).CapturedMS > diTime_ms Then
            Exit For
          End If
        Next
        CRes = Me.TrackingValues(nResIndex)
      End If
    Catch ex As Exception
    End Try
    Return CRes
  End Function
#End Region


#Region "Constructor"
  Public Sub New()

  End Sub

  Public Sub New(ByVal CiTrackingHost As TrackingHost)
    Me.TrackingHost = CiTrackingHost
  End Sub
#End Region

#Region "Listener"
  Public Function InitTrackingListener() As Boolean
    Dim bRes As Boolean = False
    Try
      If Not Me.TrackingHost Is Nothing Then
        Select Case Me.TrackingHost.TrackingType
          Case eTrackingType.TCP_Client
            Me.TrackingListener = New TrackingListener.TrackingListenerTCP_Client
            Me.TrackingListener.receivePort = Me.TrackingHost.SourcePort
          Case eTrackingType.TCP_Server
            Me.TrackingListener = New TrackingListener.TrackingListenerTCP_Server
            Me.TrackingListener.receivePort = Me.TrackingHost.TargetPort
          Case Else
            Me.TrackingListener = New TrackingListener.TrackingListenerUDP_adv()
            Me.TrackingListener.receivePort = Me.TrackingHost.SourcePort
        End Select
        Me.TrackingListener.TrackingListenerHost = Me.TrackingHost
        Me.TrackingListener.InitComm()
        bRes = True
      End If
    Catch ex As Exception
    End Try
    Return bRes
  End Function

  Public Function StopTrackingListener() As Boolean
    Dim bRes As Boolean = False
    Try
      If Not Me.TrackingHost Is Nothing Then
        If Not Me.TrackingListener Is Nothing Then
          Me.TrackingListener.CloseComm()
        End If
      End If
    Catch ex As Exception

    End Try
    Return bres
  End Function

  Private Sub TrackingListener_Started() Handles TrackingListener.Started

  End Sub

  Private Sub TrackingListener_Stopped() Handles TrackingListener.Stopped

  End Sub

  Private Sub TrackingListener_ValueReceived(ByVal CiValue As TrackingValue) Handles TrackingListener.ValueReceived
    Me.TrackingValues.Add(CiValue)
    RaiseEvent NewTrackingValue(CiValue)
  End Sub

  Public ReadOnly Property LastTrackingValue() As TrackingValue
    Get
      If Me.TrackingValues.Count > 0 Then
        Return Me.TrackingValues(Me.TrackingValues.Count - 1)
      Else
        Return Nothing
      End If
    End Get
  End Property

  Public ReadOnly Property FirstTrackingValue() As TrackingValue
    Get
      If Me.TrackingValues.Count > 0 Then
        Return Me.TrackingValues(0)
      Else
        Return Nothing
      End If
    End Get
  End Property

  Public ReadOnly Property ValuesPerSecond() As Double
    Get
      If Me.TrackingValues.Count > 1 Then
        If Me.TrackingValues.Count > 100 Then
          Return (100 * 1000) / (Me.LastTrackingValue.CapturedMS - Me.TrackingValues(Me.TrackingValues.Count - 100).CapturedMS)
        Else
          Return (Me.TrackingValues.Count * 1000) / (Me.LastTrackingValue.CapturedMS - Me.FirstTrackingValue.CapturedMS)
        End If

      Else
        Return 0
      End If
    End Get
  End Property
#End Region

End Class
