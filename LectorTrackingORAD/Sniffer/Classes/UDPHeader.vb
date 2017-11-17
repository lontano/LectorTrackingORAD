Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Windows.Forms

Namespace IpComm
  Public Class UDPHeader
    'UDP header fields
    Private usSourcePort As UShort
    'Sixteen bits for the source port number        
    Private usDestinationPort As UShort
    'Sixteen bits for the destination port number
    Private usLength As UShort
    'Length of the UDP header
    Private sChecksum As Integer
    'Sixteen bits for the checksum
    '(checksum can be negative so taken as short)              
    'End UDP header fields
    Private byUDPData As Byte() = New Byte(4095) {}
    'Data carried by the UDP packet
    Public Source As IPAddress

    Public Sub New(ByVal byBuffer As Byte(), ByVal nReceived As Integer)
      Dim memoryStream As New MemoryStream(byBuffer, 0, nReceived)
      Dim binaryReader As New BinaryReader(memoryStream)
      Dim lAux As Integer

      ReDim byUDPData(nReceived)

      'The first sixteen bits contain the source port
      usSourcePort = CUInt(CUInt(binaryReader.ReadUInt16))

      'The next sixteen bits contain the destination port
      usDestinationPort = CUInt((binaryReader.ReadUInt16()))

      'The next sixteen bits contain the length of the UDP packet
      usLength = CUInt((binaryReader.ReadUInt16()))

      'The next sixteen bits contain the checksum
      lAux = binaryReader.ReadUInt16()
      sChecksum = lAux ' IPAddress.NetworkToHostOrder(lAux)

      'Copy the data carried by the UDP packet into the data buffer
      'The UDP header is of 8 bytes so we start copying after it
      Array.Copy(byBuffer, 8, byUDPData, 0, nReceived - 8)
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

    Public ReadOnly Property Length() As String
      Get
        Return usLength.ToString()
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
        Return byUDPData
      End Get
    End Property
  End Class
End Namespace
