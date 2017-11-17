Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Windows.Forms

Namespace IpComm
  Public Class TCPHeader
    'TCP header fields
    Private usSourcePort As Short
    'Sixteen bits for the source port number
    Private usDestinationPort As Short
    'Sixteen bits for the destination port number
    Private uiSequenceNumber As Long = 555
    'Thirty two bits for the sequence number
    Private uiAcknowledgementNumber As Long = 555
    'Thirty two bits for the acknowledgement number
    Private usDataOffsetAndFlags As Short = 555
    'Sixteen bits for flags and data offset
    Private usWindow As Short = 555
    'Sixteen bits for the window size
    Private sChecksum As Short = 555
    'Sixteen bits for the checksum
    '(checksum can be negative so taken as short)
    Private usUrgentPointer As Short
    'Sixteen bits for the urgent pointer
    'End TCP header fields
    Private byHeaderLength As Byte
    'Header length
    Private usMessageLength As UShort
    'Length of the data being carried
    Private byTCPData As Byte() = New Byte(4095) {}
    'Data carried by the TCP packet
    Public Sub New(ByVal byBuffer As Byte(), ByVal nReceived As Integer)
      Try
        Dim memoryStream As New MemoryStream(byBuffer, 0, nReceived)
        Dim binaryReader As New BinaryReader(memoryStream)

        'The first sixteen bits contain the source port
        usSourcePort = CInt(IPAddress.NetworkToHostOrder(binaryReader.ReadInt16()))

        'The next sixteen contain the destiination port
        usDestinationPort = CInt(IPAddress.NetworkToHostOrder(binaryReader.ReadInt16()))

        'Next thirty two have the sequence number
        uiSequenceNumber = CInt(IPAddress.NetworkToHostOrder(binaryReader.ReadInt32()))

        'Next thirty two have the acknowledgement number
        uiAcknowledgementNumber = CLng(IPAddress.NetworkToHostOrder(binaryReader.ReadInt32()))

        'The next sixteen bits hold the flags and the data offset
        usDataOffsetAndFlags = CUShort(IPAddress.NetworkToHostOrder(binaryReader.ReadInt16()))

        'The next sixteen contain the window size
        usWindow = CShort(IPAddress.NetworkToHostOrder(binaryReader.ReadInt16()))

        'In the next sixteen we have the checksum
        sChecksum = CShort(IPAddress.NetworkToHostOrder(binaryReader.ReadInt16()))

        'The following sixteen contain the urgent pointer
        usUrgentPointer = CUShort(IPAddress.NetworkToHostOrder(binaryReader.ReadInt16()))

        'The data offset indicates where the data begins, so using it we
        'calculate the header length
        byHeaderLength = CByte(usDataOffsetAndFlags >> 12)
        byHeaderLength *= 4

        'Message length = Total length of the TCP packet - Header length
        usMessageLength = CUShort(nReceived - byHeaderLength)

        'Copy the TCP data into the data buffer
        Array.Copy(byBuffer, byHeaderLength, byTCPData, 0, nReceived - byHeaderLength)
      Catch ex As Exception
        MessageBox.Show(ex.Message, "MJsniff TCP" & (nReceived), MessageBoxButtons.OK, MessageBoxIcon.[Error])
      End Try
    End Sub

    Public ReadOnly Property SourcePort() As String
      Get
        Return usSourcePort.ToString()
      End Get
    End Property

    Public ReadOnly Property DestinationPort() As String
      Get
        Return usDestinationPort.ToString()
      End Get
    End Property

    Public ReadOnly Property SequenceNumber() As String
      Get
        Return uiSequenceNumber.ToString()
      End Get
    End Property

    Public ReadOnly Property AcknowledgementNumber() As String
      Get
        'If the ACK flag is set then only we have a valid value in
        'the acknowlegement field, so check for it beore returning 
        'anything
        If (usDataOffsetAndFlags And &H10) <> 0 Then
          Return uiAcknowledgementNumber.ToString()
        Else
          Return ""
        End If
      End Get
    End Property

    Public ReadOnly Property HeaderLength() As String
      Get
        Return byHeaderLength.ToString()
      End Get
    End Property

    Public ReadOnly Property WindowSize() As String
      Get
        Return usWindow.ToString()
      End Get
    End Property

    Public ReadOnly Property UrgentPointer() As String
      Get
        'If the URG flag is set then only we have a valid value in
        'the urgent pointer field, so check for it beore returning 
        'anything
        If (usDataOffsetAndFlags And &H20) <> 0 Then
          Return usUrgentPointer.ToString()
        Else
          Return ""
        End If
      End Get
    End Property

    Public ReadOnly Property Flags() As String
      Get
        'The last six bits of the data offset and flags contain the
        'control bits

        'First we extract the flags
        Dim nFlags As Integer = usDataOffsetAndFlags And &H3F

        Dim strFlags As String = String.Format("0x{0:x2} (", nFlags)

        'Now we start looking whether individual bits are set or not
        If (nFlags And &H1) <> 0 Then
          strFlags += "FIN, "
        End If
        If (nFlags And &H2) <> 0 Then
          strFlags += "SYN, "
        End If
        If (nFlags And &H4) <> 0 Then
          strFlags += "RST, "
        End If
        If (nFlags And &H8) <> 0 Then
          strFlags += "PSH, "
        End If
        If (nFlags And &H10) <> 0 Then
          strFlags += "ACK, "
        End If
        If (nFlags And &H20) <> 0 Then
          strFlags += "URG"
        End If
        strFlags += ")"

        If strFlags.Contains("()") Then
          strFlags = strFlags.Remove(strFlags.Length - 3)
        ElseIf strFlags.Contains(", )") Then
          strFlags = strFlags.Remove(strFlags.Length - 3, 2)
        End If

        Return strFlags
      End Get
    End Property

    Public ReadOnly Property Checksum() As String
      Get
        'Return the checksum in hexadecimal format
        Return String.Format("0x{0:x2}", sChecksum)
      End Get
    End Property

    Public ReadOnly Property Data() As Byte()
      Get
        Return byTCPData
      End Get
    End Property

    Public ReadOnly Property MessageLength() As UShort
      Get
        Return usMessageLength
      End Get
    End Property
  End Class
End Namespace
