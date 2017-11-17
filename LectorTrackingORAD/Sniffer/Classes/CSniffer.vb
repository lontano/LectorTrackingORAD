Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Net.Sockets
Imports System.Net


Namespace IpComm
  Public Enum Protocol
    TCP = 6
    UDP = 17
    Unknown = -1
  End Enum

  Public Class Sniffer
    Private mainSocket As Socket
    'The socket which captures all incoming packets
    Private byteData As Byte() = New Byte(4095) {}
    Private bContinueCapturing As Boolean = False
    'A flag to check if packets are to be captured or not
    Private Delegate Sub AddTreeNode(ByVal node As TreeNode)

    Public ShowProtocol As Protocol

    Public ShowPort As Integer

    Public _Parent As Control

    Public Event ListenState(ByVal bListenState As Boolean)
    Public Event DataArrivalUDP(ByVal data As UDPHeader)
    Private Delegate Sub EventArgsDelegate(ByVal data As UDPHeader)

    Public LlistaInterfaces As New List(Of String)


    Public Sub StartStop()
      If gudtCnfg.SelectedInterface = "" Then
        MessageBox.Show("Select an Interface to capture the packets.", "MJsniffer", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Return
      End If
      Try
        If Not bContinueCapturing Then
          'Start capturing the packets...
          RaiseEvent ListenState(True)

          bContinueCapturing = True

          'For sniffing the socket to capture the packets has to be a raw socket, with the
          'address family being of type internetwork, and protocol being IP
          mainSocket = New Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP)

          'Bind the socket to the selected IP address
          mainSocket.Bind(New IPEndPoint(IPAddress.Parse(gudtCnfg.SelectedInterface), 0))

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
          mainSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, New AsyncCallback(AddressOf OnReceive), Nothing)
        Else
          bContinueCapturing = False
          'To stop capturing the packets close the socket
          mainSocket.Close()
          RaiseEvent ListenState(False)
        End If
      Catch ex As Exception
        MessageBox.Show(ex.Message, "MJsniffer", MessageBoxButtons.OK, MessageBoxIcon.[Error])
      End Try
    End Sub

    Private Sub OnReceive(ByVal ar As IAsyncResult)
      Try
        Dim nReceived As Integer = mainSocket.EndReceive(ar)

        'Analyze the bytes received...

        ParseData(byteData, nReceived)

        If bContinueCapturing Then
          byteData = New Byte(4095) {}

          'Another call to BeginReceive so that we continue to receive the incoming
          'packets
          mainSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, New AsyncCallback(AddressOf OnReceive), Nothing)
        End If
      Catch generatedExceptionName As ObjectDisposedException
      Catch ex As Exception
        MessageBox.Show(ex.Message, "MJsniffer", MessageBoxButtons.OK, MessageBoxIcon.[Error])
      End Try
    End Sub

    Private Sub ParseData(ByVal byteData As Byte(), ByVal nReceived As Integer)
      Dim rootNode As New TreeNode()
      Dim bDraw As Boolean

      'Since all protocol packets are encapsulated in the IP datagram
      'so we start by parsing the IP header and see what protocol data
      'is being carried by it
      Dim ipHeader As New IPHeader(byteData, nReceived)
      Dim tcpHeader As TCPHeader
      Dim udpHeader As UDPHeader

      'Now according to the protocol being carried by the IP datagram we parse 
      'the data field of the datagram
      bDraw = False
      If Me.ShowProtocol = ipHeader.ProtocolType Then
        Select Case ipHeader.ProtocolType
          Case Protocol.TCP
            'IPHeader.Data stores the data being 
            'carried by the IP datagram
            tcpHeader = New TCPHeader(ipHeader.Data, ipHeader.MessageLength)
            bDraw = (tcpHeader.DestinationPort = Me.ShowPort) Or (Me.ShowPort = 0)
          Case Protocol.UDP
            'IPHeader.Data stores the data being 
            'carried by the IP datagram

            udpHeader = New UDPHeader(ipHeader.Data, CInt(ipHeader.MessageLength))
            udpHeader.Source = ipHeader.SourceAddress
            bDraw = (udpHeader.DestinationPort = Me.ShowPort) Or (Me.ShowPort = 0)
            If bDraw Then
              Me.ThreadSafeRaiseUDPDataEvent(udpHeader)
              'RaiseEvent DataArrivalUDP(udpHeader)
            End If
        End Select
      End If

      If bDraw Then

        Dim ipNode As TreeNode = MakeIPTreeNode(ipHeader)
        rootNode.Nodes.Add(ipNode)


        Select Case ipHeader.ProtocolType
          Case Protocol.TCP


            'Length of the data field                    
            Dim tcpNode As TreeNode = MakeTCPTreeNode(tcpHeader)

            rootNode.Nodes.Add(tcpNode)
            rootNode.Text = ipHeader.SourceAddress.ToString() & "-" & ipHeader.DestinationAddress.ToString()

            'If the port is equal to 53 then the underlying protocol is DNS
            'Note: DNS can use either TCP or UDP thats why the check is done twice
            If tcpHeader.DestinationPort = "53" OrElse tcpHeader.SourcePort = "53" Then
              Dim dnsNode As TreeNode = MakeDNSTreeNode(tcpHeader.Data, CInt(tcpHeader.MessageLength))
              rootNode.Nodes.Add(dnsNode)

            End If
            Exit Select

          Case Protocol.UDP



            'Length of the data field                    
            Dim udpNode As TreeNode = MakeUDPTreeNode(udpHeader)

            rootNode.Nodes.Add(udpNode)
            rootNode.Text = ipHeader.SourceAddress.ToString() & ":" & udpHeader.SourcePort & "-" & ipHeader.DestinationAddress.ToString() & ":" & udpHeader.DestinationPort

            'If the port is equal to 53 then the underlying protocol is DNS
            'Note: DNS can use either TCP or UDP thats why the check is done twice
            If udpHeader.DestinationPort = "53" OrElse udpHeader.SourcePort = "53" Then

              'Length of UDP header is always eight bytes so we subtract that out of the total 
              'length to find the length of the data
              Dim dnsNode As TreeNode = MakeDNSTreeNode(udpHeader.Data, Convert.ToInt32(udpHeader.Length) - 8)
              rootNode.Nodes.Add(dnsNode)
            End If

            Exit Select

          Case Protocol.Unknown
            Exit Select
        End Select

      End If
    End Sub

    Public Sub ThreadSafeRaiseUDPDataEvent(ByVal udpdata As UDPHeader)
      Try
        If _Parent.InvokeRequired Then
          'Me.Parent.Invoke(New EventArgsDelegate(AddressOf (ThreadSafeRaiseUDPDataEvent), udpdata)
          _Parent.Invoke(New EventArgsDelegate(AddressOf ThreadSafeRaiseUDPDataEvent), udpdata)


        Else
          RaiseEvent DataArrivalUDP(udpdata)
        End If

      Catch ex As Exception

      End Try
    End Sub

    'Helper function which returns the information contained in the IP header as a
    'tree node
    Private Function MakeIPTreeNode(ByVal ipHeader As IPHeader) As TreeNode
      Dim ipNode As New TreeNode()

      ipNode.Text = "IP"
      ipNode.Nodes.Add("Ver: " & Convert.ToString(ipHeader.Version))
      ipNode.Nodes.Add("Header Length: " & Convert.ToString(ipHeader.HeaderLength))
      ipNode.Nodes.Add("Differntiated Services: " & Convert.ToString(ipHeader.DifferentiatedServices))
      ipNode.Nodes.Add("Total Length: " & Convert.ToString(ipHeader.TotalLength))
      ipNode.Nodes.Add("Identification: " & Convert.ToString(ipHeader.Identification))
      ipNode.Nodes.Add("Flags: " & Convert.ToString(ipHeader.Flags))
      ipNode.Nodes.Add("Fragmentation Offset: " & Convert.ToString(ipHeader.FragmentationOffset))
      ipNode.Nodes.Add("Time to live: " & Convert.ToString(ipHeader.TTL))
      Select Case ipHeader.ProtocolType
        Case Protocol.TCP
          ipNode.Nodes.Add("Protocol: " & "TCP")
          Exit Select
        Case Protocol.UDP
          ipNode.Nodes.Add("Protocol: " & "UDP")
          Exit Select
        Case Protocol.Unknown
          ipNode.Nodes.Add("Protocol: " & "Unknown")
          Exit Select
      End Select
      ipNode.Nodes.Add("Checksum: " & Convert.ToString(ipHeader.Checksum))
      ipNode.Nodes.Add("Source: " & ipHeader.SourceAddress.ToString())
      ipNode.Nodes.Add("Destination: " & ipHeader.DestinationAddress.ToString())

      Return ipNode
    End Function

    'Helper function which returns the information contained in the TCP header as a
    'tree node
    Private Function MakeTCPTreeNode(ByVal tcpHeader As TCPHeader) As TreeNode
      Dim tcpNode As New TreeNode()

      tcpNode.Text = "TCP"

      tcpNode.Nodes.Add("Source Port: " & Convert.ToString(tcpHeader.SourcePort))
      tcpNode.Nodes.Add("Destination Port: " & Convert.ToString(tcpHeader.DestinationPort))
      tcpNode.Nodes.Add("Sequence Number: " & Convert.ToString(tcpHeader.SequenceNumber))

      If tcpHeader.AcknowledgementNumber <> "" Then
        tcpNode.Nodes.Add("Acknowledgement Number: " & Convert.ToString(tcpHeader.AcknowledgementNumber))
      End If

      tcpNode.Nodes.Add("Header Length: " & Convert.ToString(tcpHeader.HeaderLength))
      tcpNode.Nodes.Add("Flags: " & Convert.ToString(tcpHeader.Flags))
      tcpNode.Nodes.Add("Window Size: " & Convert.ToString(tcpHeader.WindowSize))
      tcpNode.Nodes.Add("Checksum: " & Convert.ToString(tcpHeader.Checksum))

      If tcpHeader.UrgentPointer <> "" Then
        tcpNode.Nodes.Add("Urgent Pointer: " & Convert.ToString(tcpHeader.UrgentPointer))
      End If

      Return tcpNode
    End Function

    'Helper function which returns the information contained in the UDP header as a
    'tree node
    Private Function MakeUDPTreeNode(ByVal udpHeader As UDPHeader) As TreeNode
      Dim udpNode As New TreeNode()

      udpNode.Text = "UDP"
      udpNode.Nodes.Add("Source Port: " & Convert.ToString(udpHeader.SourcePort))
      udpNode.Nodes.Add("Destination Port: " & Convert.ToString(udpHeader.DestinationPort))
      udpNode.Nodes.Add("Length: " & Convert.ToString(udpHeader.Length))
      udpNode.Nodes.Add("Checksum: " & Convert.ToString(udpHeader.Checksum))
      udpNode.Nodes.Add("Data: " & Convert.ToString(BitConverter.ToString(udpHeader.Data)))

      Return udpNode
    End Function

    'Helper function which returns the information contained in the DNS header as a
    'tree node
    Private Function MakeDNSTreeNode(ByVal byteData As Byte(), ByVal nLength As Integer) As TreeNode
      Dim dnsHeader As New DNSHeader(byteData, nLength)

      Dim dnsNode As New TreeNode()

      dnsNode.Text = "DNS"
      dnsNode.Nodes.Add("Identification: " + dnsHeader.Identification)
      dnsNode.Nodes.Add("Flags: " + dnsHeader.Flags)
      dnsNode.Nodes.Add("Questions: " + dnsHeader.TotalQuestions)
      dnsNode.Nodes.Add("Answer RRs: " + dnsHeader.TotalAnswerRRs)
      dnsNode.Nodes.Add("Authority RRs: " + dnsHeader.TotalAuthorityRRs)
      dnsNode.Nodes.Add("Additional RRs: " + dnsHeader.TotalAdditionalRRs)

      Return dnsNode
    End Function

    Public Sub New()
      Dim strIP As String = Nothing

      Dim HosyEntry As IPHostEntry = Dns.GetHostEntry((Dns.GetHostName()))
      If HosyEntry.AddressList.Length > 0 Then
        For Each ip As IPAddress In HosyEntry.AddressList
          strIP = ip.ToString()
          LlistaInterfaces.Add(strIP)
        Next
        If LlistaInterfaces.Count > 0 Then
          gudtCnfg.SelectedInterface = LlistaInterfaces(0)
        End If
      End If
    End Sub


    Protected Overrides Sub Finalize()
      MyBase.Finalize()
      If bContinueCapturing Then
        mainSocket.Close()
      End If
    End Sub
  End Class
End Namespace
