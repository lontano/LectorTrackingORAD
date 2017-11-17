
Public Class TrackingPlayerUDP_ORAD
  Inherits ATrackingPlayer


#Region "Broadcast"
  '''''''''''''''''''''''Set up variables''''''''''''''''''''
  Private sendingClient As Net.Sockets.UdpClient                           'Client for sending data
  Private receivingThread As Threading.Thread                            'Create a separate thread to listen for incoming data, helps to prevent the form from freezing up
  Private closing As Boolean = False                           'Used to close clients if form is closing


  ''''''''''''''''''''Setup sender client'''''''''''''''''
  Public Overrides Sub InitializeSender()
    Me.sPiName = "UDP_ORAD"
    TrackingPlayerHost.broadcastAddress = "192.168.146.76"
    'broadcastAddress = "172.26.110.42"
    'broadcastAddress = "127.0.0.1"
    sendingClient = New Net.Sockets.UdpClient(TrackingPlayerHost.broadcastAddress, TrackingPlayerHost.BroadcastPort)
    sendingClient.EnableBroadcast = False
  End Sub

  Public Overrides Sub InitComm()
    InitializeSender()          'Initializes startup of sender client
  End Sub

  Public Overrides Sub CloseComm()
    closing = True          'Tells receiving loop to close
    sendingClient.Close()
  End Sub


  Public Overrides Sub BroadcastValue(ByVal CiValue As TrackingValue)
    Try
      sendingClient.Send(CiValue.buffer, CiValue.buffer.Length)               'Send bytes
    Catch ex As Exception

    End Try
  End Sub
#End Region
End Class
