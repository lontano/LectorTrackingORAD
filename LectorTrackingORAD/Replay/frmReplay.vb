Public Class frmReplay

  '''''''''''''''''''''''Set up variables''''''''''''''''''''
  Private Const port As Integer = 9653                         'Port number to send/recieve data on
  Private broadcastAddress As String = "255.255.255.255" 'Sends data to all LOCAL listening clients, to send data over WAN you'll need to enter a public (external) IP address of the other client
  Private receivingClient As Net.Sockets.UdpClient                         'Client for handling incoming data
  Private sendingClient As Net.Sockets.UdpClient                           'Client for sending data
  Private receivingThread As Threading.Thread                            'Create a separate thread to listen for incoming data, helps to prevent the form from freezing up
  Private closing As Boolean = False                           'Used to close clients if form is closing

  ''''''''''''''''''''Initialize listening & sending subs'''''''''''''''''

  Private Sub frmReplay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
    InitializeSender()          'Initializes startup of sender client
    InitializeReceiver()        'Starts listening for incoming data   

  End Sub

  ''''''''''''''''''''Setup sender client'''''''''''''''''

    Private Sub InitializeSender()
        broadcastAddress = "193.104.51.199"
        'broadcastAddress = "172.26.110.42"
    broadcastAddress = "127.0.0.1"
        sendingClient = New Net.Sockets.UdpClient(broadcastAddress, port)
        sendingClient.EnableBroadcast = False
    End Sub

  '''''''''''''''''''''Setup receiving client'''''''''''''

  Private Sub InitializeReceiver()
    receivingClient = New Net.Sockets.UdpClient(port)
    Dim start As Threading.ThreadStart = New Threading.ThreadStart(AddressOf Receiver)
    receivingThread = New Threading.Thread(start)
    receivingThread.IsBackground = True
    receivingThread.Start()
  End Sub

  '''''''''''''''''''Send data if send button is clicked'''''''''''''''''''

  Private Sub sendBut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendBut.Click
    Dim toSend As String = tbSend.Text                  'tbSend is a textbox, replace it with whatever you want to send as a string
    Dim data() As Byte = System.Text.Encoding.ASCII.GetBytes(toSend) 'Convert string to bytes
    sendingClient.Send(data, data.Length)               'Send bytes
  End Sub

  '''''''''''''''''''''Start receiving loop''''''''''''''''''''''' 

  Private Sub Receiver()
    Dim endPoint As Net.IPEndPoint = New Net.IPEndPoint(Net.IPAddress.Any, port) 'Listen for incoming data from any IP address on the specified port (i personally select 9653)
    Dim message As String = ""
    While (True)                                                     'Setup an infinite loop
      Dim data() As Byte                                           'buffer for storing incoming bytes
      Try
        data = receivingClient.Receive(endPoint)                     'Receive incoming bytes 
        Message = System.Text.Encoding.ASCII.GetString(data)       'Convert bytes back to string
      Catch ex As Exception
      End Try
      If closing = True Then                                       'Exit sub if form is closing
        Exit Sub
      End If
      Debug.Print(Now.ToString & " " & message)
    End While
  End Sub

  '''''''''''''''''''Close clients if form closes''''''''''''''''''

  Private Sub frmReplay_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    closing = True          'Tells receiving loop to close
    receivingClient.Close()
    sendingClient.Close()
  End Sub

  Public Sub SendValue(ByVal CiValue As TrackingValue)
    Try
      sendingClient.Send(CiValue.buffer, CiValue.buffer.Length)               'Send bytes
    Catch ex As Exception

    End Try
  End Sub
End Class