Namespace TrackingPlayer
  Public Class TrackingPlayerUDP
    Inherits TrackingPlayer.ATrackingPlayer


#Region "Broadcast"
    '''''''''''''''''''''''Set up variables''''''''''''''''''''
    Private receivingClient As Net.Sockets.UdpClient                         'Client for handling incoming data
    Private sendingClient As Net.Sockets.UdpClient                           'Client for sending data
    Private receivingThread As Threading.Thread                            'Create a separate thread to listen for incoming data, helps to prevent the form from freezing up
    Private closing As Boolean = False                           'Used to close clients if form is closing


    ''''''''''''''''''''Setup sender client'''''''''''''''''
    Public Overrides Sub InitializeSender()
      broadcastAddress = "192.168.146.76"
      'broadcastAddress = "172.26.110.42"
      'broadcastAddress = "127.0.0.1"
      sendingClient = New Net.Sockets.UdpClient(broadcastAddress, BroadcastPort)
      sendingClient.EnableBroadcast = False
    End Sub

    Public Overrides Sub InitComm()
      InitializeSender()          'Initializes startup of sender client
      InitializeReceiver()        'Starts listening for incoming data  
    End Sub

    Public Overrides Sub CloseComm()
      closing = True          'Tells receiving loop to close
      receivingClient.Close()
      sendingClient.Close()
    End Sub


    '''''''''''''''''''''Setup receiving client'''''''''''''

    Public Overrides Sub InitializeReceiver()
      receivingClient = New Net.Sockets.UdpClient(BroadcastPort)
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
        Dim data() As Byte                                           'buffer for storing incoming bytes
        Try
          data = receivingClient.Receive(endPoint)                     'Receive incoming bytes 
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
        sendingClient.Send(CiValue.buffer, CiValue.buffer.Length)               'Send bytes
      Catch ex As Exception

      End Try
    End Sub
#End Region
  End Class
End Namespace
