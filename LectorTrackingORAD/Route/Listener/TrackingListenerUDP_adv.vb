Imports System.Net.Sockets
Imports System.Net

Namespace TrackingListener
  Public Class TrackingListenerUDP_adv
    Inherits TrackingListener.ATrackingListener

    Private mainSocket As Socket
    Private byteData As Byte() = New Byte(4095) {}

    Public Enum Protocol
      TCP = 6
      UDP = 17
      Unknown = -1
    End Enum

    '''''''''''''''''''''Setup receiving client'''''''''''''
    Public Overrides Sub InitializeReceiver()
      'Start capturing the packets...

      'For sniffing the socket to capture the packets has to be a raw socket, with the
      'address family being of type internetwork, and protocol being IP
      mainSocket = New Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP)

      'Bind the socket to the selected IP address
      mainSocket.Bind(New IPEndPoint(IPAddress.Parse(gudtCnfg.SelectedInterface), Me.receivePort))

      'Set the socket  options
      'Applies only to IP packets
      'Set the include the header
      mainSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, True)
      'option to true
      Dim byTrue As Byte() = New Byte(3) {1, 0, 0, 0}
      Dim byOut As Byte() = New Byte(3) {1, 0, 0, 0}
      'Capture outgoing packets
      'Socket.IOControl is analogous to the WSAIoctl method of Winsock 2
      'Equivalent to SIO_RCVALL constant
      'of Winsock 2
      mainSocket.IOControl(IOControlCode.ReceiveAll, byTrue, byOut)

      'Start receiving the packets asynchronously
      'mainSocket.Receive(byteData, byteData.Length, SocketFlags.None)
      'mainSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, New AsyncCallback(AddressOf OnReceive), Nothing)
      Me.InitializeReceiverThread()
    End Sub

    Public Overrides Sub CloseComm()
      closing = True          'Tells receiving loop to close
      'To stop capturing the packets close the socket
      mainSocket.Close()
    End Sub

    Public Overrides Sub InitComm()
      InitializeReceiver()
    End Sub


    '''''''''''''''''''''Start receiving loop''''''''''''''''''''''' 
    Public Overrides Sub Receiver()
      Dim endPoint As Net.IPEndPoint = New Net.IPEndPoint(Net.IPAddress.Any, Me.receivePort) 'Listen for incoming data from any IP address on the specified port (i personally select 9653)
      Dim message As String = ""
      While (Not Me.closing)                                                    'Setup an infinite loop
        Dim data() As Byte = New Byte(4095) {}                                         'buffer for storing incoming bytes
        Dim data_length As Integer
        Try
          data_length = mainSocket.Receive(data, data.Length, SocketFlags.None)                    'Receive incoming bytes 
          Dim udpheader As IpComm.UDPHeader = ParseDataUDP(data, data_length)
          If udpheader.DestinationPort = Me.TrackingListenerHost.TargetPort Or True Then
            Dim CValue As New TrackingValue(udpheader)
            If CValue.Valid Then
              Me.BackgroundListener.ReportProgress(0, CValue)
              'MyBase.Raise_ValueReceived(CValue)
            End If
          End If

        Catch ex As Exception
        End Try
        If closing = True Then                                        'Exit sub if form is closing
          Exit Sub
        End If
        ' Debug.Print(Now.ToString & " " & message)
      End While
    End Sub


    Private Function ParseDataTCP(ByVal byteData As Byte(), ByVal nReceived As Integer) As IpComm.TCPHeader
      Dim ipHeader As New IpComm.IPHeader(byteData, nReceived)
      Dim tcpHeader As IpComm.TCPHeader

      tcpHeader = New IpComm.TCPHeader(ipHeader.Data, ipHeader.MessageLength)

      Return tcpHeader
    End Function

    Private Function ParseDataUDP(ByVal byteData As Byte(), ByVal nReceived As Integer) As IpComm.UDPHeader
      Dim ipHeader As New IpComm.IPHeader(byteData, nReceived)
      Dim udpHeader As IpComm.UDPHeader

      udpHeader = New IpComm.UDPHeader(ipHeader.Data, CInt(ipHeader.MessageLength))
      udpHeader.Source = ipHeader.SourceAddress

      Return udpHeader
    End Function
  End Class
End Namespace
