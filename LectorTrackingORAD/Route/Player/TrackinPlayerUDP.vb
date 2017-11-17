
Public Class TrackingPlayerUDP
  Inherits ATrackingPlayer


#Region "Broadcast"
  '''''''''''''''''''''''Set up variables''''''''''''''''''''
  Private sendingClient As Net.Sockets.UdpClient                           'Client for sending data
  Private receivingThread As Threading.Thread                            'Create a separate thread to listen for incoming data, helps to prevent the form from freezing up
  Private closing As Boolean = False                           'Used to close clients if form is closing

  Private endPoint As Net.IPEndPoint

  ''''''''''''''''''''Setup sender client'''''''''''''''''
  Public Overrides Sub InitializeSender()
    Me.sPiName = "UDP"
    'sendingClient = New Net.Sockets.UdpClient(TrackingPlayerHost.broadcastAddress, TrackingPlayerHost.BroadcastPort)
    sendingClient = New Net.Sockets.UdpClient(TrackingPlayerHost.broadcastAddress, TrackingPlayerHost.BroadcastPort)
    endPoint = New Net.IPEndPoint(Net.IPAddress.Parse(TrackingPlayerHost.broadcastAddress), TrackingPlayerHost.BroadcastPort)


    'sendingClient.EnableBroadcast = True
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
      'sendingClient.Send(CiValue.buffer, CiValue.buffer.Length, endPoint)               'Send bytes
      sendingClient.Send(CiValue.buffer, CiValue.buffer.Length)               'Send bytes
    Catch ex As Exception

    End Try
  End Sub
#End Region
End Class
