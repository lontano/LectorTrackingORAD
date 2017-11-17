Namespace TrackingPlayer
  Public Class TrackingPlayerTCP
    Inherits TrackingPlayer.ATrackingPlayer

    '''''''''''''''''''''''Set up variables''''''''''''''''''''
    Private receivingTCPListener As Net.Sockets.TcpListener                         'Client for handling incoming data
    Private sendingTCPClient As Net.Sockets.TcpClient                          'Client for sending data
    Private receivingThread As Threading.Thread                            'Create a separate thread to listen for incoming data, helps to prevent the form from freezing up
    Private closing As Boolean = False                           'Used to close clients if form is closing

    Private sendEndPoint As Net.IPEndPoint
    Private receiveEndPoint As Net.IPEndPoint


    ''''''''''''''''''''Setup sender client'''''''''''''''''
    Public Overrides Sub InitializeSender()
      broadcastAddress = "192.168.146.76"
      'broadcastAddress = "172.26.110.42"
      'broadcastAddress = "127.0.0.1"
      sendingSocket = New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Dgram, Net.Sockets.ProtocolType.Udp)
      sendEndPoint = New Net.IPEndPoint(Me.broadcastAddress, BroadcastPort) 'Send to the specified address/port

    End Sub

    Public Overrides Sub InitComm()
      InitializeSender()          'Initializes startup of sender client
      InitializeReceiver()        'Starts listening for incoming data  
    End Sub

    Public Overrides Sub CloseComm()
      closing = True          'Tells receiving loop to close
      receivingSocket.Shutdown(Net.Sockets.SocketShutdown.Receive)
      receivingSocket.Close()
      sendingSocket.Shutdown(Net.Sockets.SocketShutdown.Send)
      sendingSocket.Close()
    End Sub


    '''''''''''''''''''''Setup receiving client'''''''''''''
    Public Overrides Sub InitializeReceiver()
      receivingSocket = New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Dgram, Net.Sockets.ProtocolType.Udp)
      receiveEndPoint = New Net.IPEndPoint(Net.IPAddress.Any, BroadcastPort) 'Listen for incoming data from any IP address on the specified port (i personally select 9653)

      Dim start As Threading.ThreadStart = New Threading.ThreadStart(AddressOf Receiver)
      receivingThread = New Threading.Thread(start)
      receivingThread.IsBackground = True
      receivingThread.Start()
    End Sub

    '''''''''''''''''''''Start receiving loop''''''''''''''''''''''' 

    Public Overrides Sub Receiver()
      Dim endPoint As Net.IPEndPoint = New Net.IPEndPoint(Net.IPAddress.Any, BroadcastPort) 'Listen for incoming data from any IP address on the specified port (i personally select 9653)
      Dim message As String = ""
      While (True)                                                     'Setup an infinite loop
        Dim data(0) As Byte                                           'buffer for storing incoming bytes
        Try
          receivingSocket.ReceiveFrom(data, System.Net.Sockets.SocketFlags.Broadcast, endPoint)
          message = System.Text.Encoding.ASCII.GetString(data)       'Convert bytes back to string
          Dim CValue As New TrackingValue(0)
          CValue.FromBuffer(data, 24)
          Debug.Print(CValue.CapturedMS)

        Catch ex As Exception
        End Try
        If closing = True Then                                       'Exit sub if form is closing
          Exit Sub
        End If
        Debug.Print(Now.ToString & " " & message)
      End While
    End Sub

    Public Overrides Sub BroadcastValue(ByVal CiValue As TrackingValue)
      Try
        sendingSocket.SendTo(CiValue.buffer, CiValue.buffer.Length, sendEndPoint)               'Send bytes
      Catch ex As Exception

      End Try
    End Sub
  End Class
End Namespace
