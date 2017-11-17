Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Windows.Forms

Namespace IpComm
  Public Class IPHeader
    'IP Header fields
    Private byVersionAndHeaderLength As Byte
    'Eight bits for version and header length
    Private byDifferentiatedServices As Byte
    'Eight bits for differentiated services (TOS)
    Private usTotalLength As UShort
    'Sixteen bits for total length of the datagram (header + message)
    Private usIdentification As UShort
    'Sixteen bits for identification
    Private usFlagsAndOffset As UShort
    'Eight bits for flags and fragmentation offset
    Private byTTL As Byte
    'Eight bits for TTL (Time To Live)
    Private byProtocol As Byte
    'Eight bits for the underlying protocol
    Private sChecksum As Short
    'Sixteen bits containing the checksum of the header
    '(checksum can be negative so taken as short)
    Private uiSourceIPAddress As Long
    'Thirty two bit source IP Address
    Private uiDestinationIPAddress As Long
    'Thirty two bit destination IP Address
    'End IP Header fields
    Private byHeaderLength As Byte
    'Header length
    Private byIPData As Byte() = New Byte(4095) {}
    'Data carried by the datagram
    Private bAux As Byte()

    Public Sub New(ByVal byBuffer As Byte(), ByVal nReceived As Integer)

      Try
        'Create MemoryStream out of the received bytes
        Dim memoryStream As New MemoryStream(byBuffer, 0, nReceived)
        'Next we create a BinaryReader out of the MemoryStream
        Dim binaryReader As New BinaryReader(memoryStream)
        Dim lAux As Integer

        'The first eight bits of the IP header contain the version and
        'header length so we read them
        byVersionAndHeaderLength = binaryReader.ReadByte()

        'The next eight bits contain the Differentiated services
        byDifferentiatedServices = binaryReader.ReadByte()

        'Next eight bits hold the total length of the datagram

        bAux = binaryReader.ReadBytes(2) : lAux = CInt(IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bAux, 0)))
        usTotalLength = CUShort(lAux)

        'Next sixteen have the identification bytes
        bAux = binaryReader.ReadBytes(2) : lAux = CInt(IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bAux, 0)))
        If lAux < 0 Then
          lAux = CInt((BitConverter.ToUInt16(bAux, 0)))
        Else
          lAux = lAux
        End If

        usIdentification = CUShort(lAux)

        'Next sixteen bits contain the flags and fragmentation offset
        bAux = binaryReader.ReadBytes(2) : lAux = CInt(IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bAux, 0)))
        usFlagsAndOffset = CUShort(lAux)

        'Next eight bits have the TTL value
        byTTL = binaryReader.ReadByte()

        'Next eight represnts the protocol encapsulated in the datagram
        byProtocol = binaryReader.ReadByte()

        'Next sixteen bits contain the checksum of the header
        sChecksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16())

        'Next thirty two bits have the source IP address
        uiSourceIPAddress = CLng(binaryReader.ReadUInt32())

        'Next thirty two hold the destination IP address
        uiDestinationIPAddress = CLng(binaryReader.ReadUInt32())

        'Now we calculate the header length

        byHeaderLength = byVersionAndHeaderLength
        'The last four bits of the version and header length field contain the
        'header length, we perform some simple binary airthmatic operations to
        'extract them
        byHeaderLength <<= 4
        byHeaderLength >>= 4
        'Multiply by four to get the exact header length
        byHeaderLength *= 4

        'Copy the data carried by the data gram into another array so that
        'according to the protocol being carried in the IP datagram
        'start copying from the end of the header
        Array.Copy(byBuffer, byHeaderLength, byIPData, 0, usTotalLength - byHeaderLength)
      Catch ex As Exception
        MessageBox.Show(ex.Message, "MJsniffer", MessageBoxButtons.OK, MessageBoxIcon.[Error])
      End Try
    End Sub

    Public ReadOnly Property Version() As String
      Get
        'Calculate the IP version

        'The four bits of the IP header contain the IP version
        If (byVersionAndHeaderLength >> 4) = 4 Then
          Return "IP v4"
        ElseIf (byVersionAndHeaderLength >> 4) = 6 Then
          Return "IP v6"
        Else
          Return "Unknown"
        End If
      End Get
    End Property

    Public ReadOnly Property HeaderLength() As String
      Get
        Return byHeaderLength.ToString()
      End Get
    End Property

    Public ReadOnly Property MessageLength() As UShort
      Get
        'MessageLength = Total length of the datagram - Header length
        Return CUShort(usTotalLength - byHeaderLength)
      End Get
    End Property

    Public ReadOnly Property DifferentiatedServices() As String
      Get
        'Returns the differentiated services in hexadecimal format
        Return String.Format("0x{0:x2} ({1})", byDifferentiatedServices, byDifferentiatedServices)
      End Get
    End Property

    Public ReadOnly Property Flags() As String
      Get
        'The first three bits of the flags and fragmentation field 
        'represent the flags (which indicate whether the data is 
        'fragmented or not)
        Dim nFlags As Integer = usFlagsAndOffset >> 13
        If nFlags = 2 Then
          Return "Don't fragment"
        ElseIf nFlags = 1 Then
          Return "More fragments to come"
        Else
          Return nFlags.ToString()
        End If
      End Get
    End Property

    Public ReadOnly Property FragmentationOffset() As String
      Get
        'The last thirteen bits of the flags and fragmentation field 
        'contain the fragmentation offset
        Dim nOffset As Integer = usFlagsAndOffset << 3
        nOffset >>= 3

        Return nOffset.ToString()
      End Get
    End Property

    Public ReadOnly Property TTL() As String
      Get
        Return byTTL.ToString()
      End Get
    End Property

    Public ReadOnly Property ProtocolType() As Protocol
      Get
        'The protocol field represents the protocol in the data portion
        'of the datagram
        If byProtocol = 6 Then
          'A value of six represents the TCP protocol
          Return Protocol.TCP
        ElseIf byProtocol = 17 Then
          'Seventeen for UDP
          Return Protocol.UDP
        Else
          Return Protocol.Unknown
        End If
      End Get
    End Property

    Public ReadOnly Property Checksum() As String
      Get
        'Returns the checksum in hexadecimal format
        Return String.Format("0x{0:x2}", sChecksum)
      End Get
    End Property

    Public ReadOnly Property SourceAddress() As IPAddress
      Get
        Return New IPAddress(uiSourceIPAddress)
      End Get
    End Property

    Public ReadOnly Property DestinationAddress() As IPAddress
      Get
        Return New IPAddress(uiDestinationIPAddress)
      End Get
    End Property

    Public ReadOnly Property TotalLength() As String
      Get
        Return usTotalLength.ToString()
      End Get
    End Property

    Public ReadOnly Property Identification() As String
      Get
        Return usIdentification.ToString()
      End Get
    End Property

    Public ReadOnly Property Data() As Byte()
      Get
        Return byIPData
      End Get
    End Property
  End Class
End Namespace
