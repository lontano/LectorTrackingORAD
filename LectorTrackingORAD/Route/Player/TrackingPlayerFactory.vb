Public Class TrackingPlayerFactory
  Public LlistaTrackingPlayers As New List(Of ATrackingPlayer)

  Public Function GetTrackingPlayer(ByVal CiTrackingPlayerHost As TrackingPlayerHost, Optional ByVal biCreate As Boolean = True) As ATrackingPlayer
    Dim CRes As ATrackingPlayer = Nothing
    Try
      For Each CAux As ATrackingPlayer In Me.LlistaTrackingPlayers
        If CAux.TrackingPlayerHost = CiTrackingPlayerHost Then
          CRes = CAux
          Exit For
        End If
      Next
      If CRes Is Nothing And biCreate Then
        Select Case CiTrackingPlayerHost.Type
          Case eTrackingType.TCP_Client
            CRes = New TrackingPlayerTCP_Client
          Case eTrackingType.TCP_Server
            CRes = New TrackingPlayerTCP_Server
          Case eTrackingType.UDP
            CRes = New TrackingPlayerUDP
          Case eTrackingType.UDP_ORAD
            CRes = New TrackingPlayerUDP
        End Select
        CRes.TrackingPlayerHost = CiTrackingPlayerHost
        If CiTrackingPlayerHost.Enabled Then
          CRes.InitComm()
        End If
      End If

    Catch ex As Exception

    End Try
    Return CRes
  End Function

  Public Sub ClearPlayers()
    Try
      For nIndex As Integer = 0 To Me.LlistaTrackingPlayers.Count - 1
        Me.LlistaTrackingPlayers(0).CloseComm()
        Me.LlistaTrackingPlayers.RemoveAt(0)
      Next

    Catch ex As Exception

    End Try
  End Sub
End Class
