

Public Enum eTrackingPlayerState
  Idle
  Started
  Stopped
  Paused
  Relay
  Ended
End Enum

<Serializable()> Public MustInherit Class ATrackingPlayer
  Private WithEvents CPiTrackingSource As TrackingSource
  Private WithEvents CPiTimer As New Timer

  Private nPiCurrentPlayTime_ms As Double
  Private nPiCurrentValueIndex As Integer = 0
  Private bPiEnabled As Boolean = True

  Public Channel As Integer = -1

  Private ePiTrackingState As eTrackingPlayerState = eTrackingPlayerState.Idle

  Protected sPiName As String = "Prototype"


#Region "Events"
  Public Event CurrentPositionChanged(ByVal diPosition_ms As Double)
  Public Event Started()
  Public Event Stopped()
  Public Event Paused()
  Public Event Ended()
#End Region

#Region "Class functions"
  Public Sub New()
    CPiTimer.Enabled = False
    CPiTimer.Interval = 40
    'UDPSender.Connect("192.168.146.255", 8747)
    'Me.InitComm()
  End Sub

  Public Sub New(ByVal siHost As String, ByVal niPort As Integer)
    CPiTimer.Enabled = False
    CPiTimer.Interval = 40
    Me.TrackingPlayerHost.broadcastAddress = siHost
    Me.TrackingPlayerHost.BroadcastPort = niPort
    'Me.InitComm()
  End Sub

  Protected Overrides Sub Finalize()
    Me.CPiTimer.Enabled = False
    Me.CPiTimer = Nothing
    MyBase.Finalize()
  End Sub
#End Region

#Region "Properties"
  Public Property TrackingSource() As TrackingSource
    Get
      Return Me.CPiTrackingSource
    End Get
    Set(ByVal value As TrackingSource)
      Me.CPiTrackingSource = value
    End Set
  End Property

  Public ReadOnly Property TrackingPlayerState() As eTrackingPlayerState
    Get
      Return Me.ePiTrackingState
    End Get
  End Property

  Public Property Enabled() As Boolean
    Get
      Return Me.bPiEnabled
    End Get
    Set(ByVal value As Boolean)
      Me.bPiEnabled = value
    End Set
  End Property

  Public ReadOnly Property Name() As String
    Get
      Return Me.sPiName
    End Get
  End Property
#End Region

#Region "Public Functions"
  Private Sub ChangeState(ByVal eiTrackingState As eTrackingPlayerState)
    Try
      If eiTrackingState <> Me.ePiTrackingState Then
        Me.ePiTrackingState = eiTrackingState
        Select Case eiTrackingState
          Case eTrackingPlayerState.Idle
            Me.CPiTimer.Enabled = False
            Me.nPiCurrentPlayTime_ms = 0
            Me.nPiCurrentValueIndex = 0
            RaiseEvent Stopped()
          Case eTrackingPlayerState.Ended
            Me.CPiTimer.Enabled = False
            RaiseEvent Ended()
          Case eTrackingPlayerState.Paused
            Me.CPiTimer.Enabled = False
            RaiseEvent Paused()
          Case eTrackingPlayerState.Started
            Me.CPiTimer.Enabled = True
            RaiseEvent Started()
          Case eTrackingPlayerState.Stopped
            Me.CPiTimer.Enabled = False
            Me.nPiCurrentPlayTime_ms = 0
            Me.nPiCurrentValueIndex = 0
            RaiseEvent Stopped()
        End Select
      End If
    Catch ex As Exception

    End Try
  End Sub

  Public Sub TrackingStart(Optional ByVal StartTime_ms As Long = 0)
    Try
      nPiCurrentPlayTime_ms = StartTime_ms
      Me.ChangeState(eTrackingPlayerState.Started)
    Catch ex As Exception
    End Try
  End Sub

  Public Sub TrackingStop()
    Try
      Me.ChangeState(eTrackingPlayerState.Stopped)
    Catch ex As Exception
    End Try
  End Sub

  Public Sub TrackingPause()
    Try
      Me.ChangeState(eTrackingPlayerState.Paused)
    Catch ex As Exception
    End Try
  End Sub

  Public Sub TrackingContinue()
    Try
      Me.ChangeState(eTrackingPlayerState.Started)
    Catch ex As Exception
    End Try
  End Sub


  Public Sub SendValue(ByVal niIndex As Integer)
    Try
      Dim CValue As TrackingValue = Me.CPiTrackingSource.TrackingValues(niIndex)
      Me.BroadcastValue(CValue)
    Catch ex As Exception
    End Try
  End Sub

  Public Sub SendValue(ByVal CiValue As TrackingValue)
    Try
      Me.BroadcastValue(CiValue)
    Catch ex As Exception
    End Try
  End Sub
#End Region

#Region "Player functions and events"
  Private Sub CPiTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles CPiTimer.Tick
    Try
      Dim CValue As TrackingValue = Me.CPiTrackingSource.GetValueByTime(Me.nPiCurrentPlayTime_ms, Me.nPiCurrentValueIndex)
      If CValue.Index <> Me.nPiCurrentValueIndex Or True Then
        Me.BroadcastValue(CValue)
      End If
    Catch ex As Exception

    End Try
  End Sub

  Private Sub CPiTrackingSource_NewTrackingValue(ByVal CiTrackingValue As TrackingValue) Handles CPiTrackingSource.NewTrackingValue
    Try
      If Me.ePiTrackingState = eTrackingPlayerState.Relay Then
        Me.BroadcastValue(CiTrackingValue)
      End If
    Catch ex As Exception

    End Try
  End Sub

  Private Sub CPiTrackingSource_TrackignValuesCleared() Handles CPiTrackingSource.TrackignValuesCleared
    Try
      Me.ChangeState(eTrackingPlayerState.Idle)
    Catch ex As Exception

    End Try
  End Sub
#End Region

#Region "Broadcast"
  Public TrackingPlayerHost As New TrackingPlayerHost
  '''''''''''''''''''''''Set up variables''''''''''''''''''''
  Private closing As Boolean = False                           'Used to close clients if form is closing

  Public MustOverride Sub InitComm()

  Public MustOverride Sub CloseComm()

  ''''''''''''''''''''Setup sender client'''''''''''''''''
  Public MustOverride Sub InitializeSender()

  Public MustOverride Sub BroadcastValue(ByVal CiValue As TrackingValue)
#End Region

End Class