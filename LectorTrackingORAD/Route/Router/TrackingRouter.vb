
Namespace TrackingRouter
  <Serializable()> Public Class TrackingRouter
    Private bPiEnabled As Boolean

    <NonSerialized()> Public WithEvents MyTrackingFile As TrackingFile
    Public WithEvents TrackingHost As TrackingHost
    <NonSerialized()> Public WithEvents TrackingSource As TrackingSource
    Public TrackingPlayerHosts As New List(Of TrackingPlayerHost)
    <NonSerialized()> Public TrackingPlayers As New List(Of ATrackingPlayer)


#Region "Constructor"
    Public Sub New()

    End Sub

    Public Sub New(ByVal CiSource As TrackingSource, ByVal siHost As String, ByVal niPort As Integer)
      Dim CTrackingPlayer As ATrackingPlayer = New TrackingPlayerTCP_Client()
      CTrackingPlayer.TrackingPlayerHost.broadcastAddress = siHost
      CTrackingPlayer.TrackingPlayerHost.BroadcastPort = niPort
      Me.TrackingPlayers.Add(CTrackingPlayer)
    End Sub

    Public Sub New(ByVal CiHost As TrackingHost)
      Me.TrackingHost = CiHost
      'buscar el source associat?
    End Sub
#End Region

#Region "Tracking players"
    Public Sub AddTrackingPlayer(ByVal CiTrackingPlayer As ATrackingPlayer)
      Try
        If CiTrackingPlayer Is Nothing Then Exit Sub
        If Not Me.TrackingPlayers.Contains(CiTrackingPlayer) Then
          Me.TrackingPlayers.Add(CiTrackingPlayer)
        End If
      Catch ex As Exception
      End Try
    End Sub


    Public Sub AddTrackingPlayerHost(ByVal CiTrackingPlayerHost As TrackingPlayerHost)
      Try
        If CiTrackingPlayerHost Is Nothing Then Exit Sub
        If Not Me.TrackingPlayerHosts.Contains(CiTrackingPlayerHost) Then
          Me.TrackingPlayerHosts.Add(CiTrackingPlayerHost)
        End If
      Catch ex As Exception
      End Try
    End Sub

    Public Sub RemoveTrackingPlayer(ByVal CiTrackingPlayer As ATrackingPlayer)
      Try
        If CiTrackingPlayer Is Nothing Then Exit Sub
        If Not Me.TrackingPlayers.Contains(CiTrackingPlayer) Then
          Me.TrackingPlayers.Remove(CiTrackingPlayer)
        End If
      Catch ex As Exception
      End Try
    End Sub

    Private Sub TrackingSource_NewTrackingValue(ByVal CiTrackingValue As TrackingValue) Handles TrackingSource.NewTrackingValue
      Try
        If Me.bPiEnabled Then
          For Each CTrackingPlayer As ATrackingPlayer In Me.TrackingPlayers
            If CTrackingPlayer.Enabled Then
              CTrackingPlayer.BroadcastValue(CiTrackingValue)
            End If
          Next
        End If
      Catch ex As Exception

      End Try
    End Sub
#End Region

#Region "Values"
    Public Sub SendLastValue()
      Try
        If Not Me.TrackingSource Is Nothing Then
          Dim CValue As TrackingValue = Me.TrackingSource.LastTrackingValue
          If Not CValue Is Nothing Then
            For Each CPlayer As ATrackingPlayer In Me.TrackingPlayers
              CPlayer.SendValue(CValue)
            Next
          End If
        End If
        
      Catch ex As Exception

      End Try
      
    End Sub
#End Region
  End Class
End Namespace
