Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Windows.Forms
Imports System.Collections.Specialized
Imports System.Collections
Imports System.Collections.Generic

Namespace IpComm
  Public Class DNSHeader
    'DNS header fields
    Private usIdentification As UShort
    'Sixteen bits for identification
    Private usFlags As UShort
    'Sixteen bits for DNS flags
    Private usTotalQuestions As UShort
    'Sixteen bits indicating the number of entries 
    'in the questions list
    Private usTotalAnswerRRs As UShort
    'Sixteen bits indicating the number of entries
    'entries in the answer resource record list
    Private usTotalAuthorityRRs As UShort
    'Sixteen bits indicating the number of entries
    'entries in the authority resource record list
    Private usTotalAdditionalRRs As UShort
    'Sixteen bits indicating the number of entries
    'entries in the additional resource record list
    'End DNS header fields
    Public Sub New(ByVal byBuffer As Byte(), ByVal nReceived As Integer)
      Dim memoryStream As New MemoryStream(byBuffer, 0, nReceived)
      Dim binaryReader As New BinaryReader(memoryStream)

      'First sixteen bits are for identification
      usIdentification = CUShort(IPAddress.NetworkToHostOrder(binaryReader.ReadInt16()))

      'Next sixteen contain the flags
      usFlags = CUShort(IPAddress.NetworkToHostOrder(binaryReader.ReadInt16()))

      'Read the total numbers of questions in the quesion list
      usTotalQuestions = CUShort(IPAddress.NetworkToHostOrder(binaryReader.ReadInt16()))

      'Read the total number of answers in the answer list
      usTotalAnswerRRs = CUShort(IPAddress.NetworkToHostOrder(binaryReader.ReadInt16()))

      'Read the total number of entries in the authority list
      usTotalAuthorityRRs = CUShort(IPAddress.NetworkToHostOrder(binaryReader.ReadInt16()))

      'Total number of entries in the additional resource record list
      usTotalAdditionalRRs = CUShort(IPAddress.NetworkToHostOrder(binaryReader.ReadInt16()))
    End Sub

    Public ReadOnly Property Identification() As String
      Get
        Return String.Format("0x{0:x2}", usIdentification)
      End Get
    End Property

    Public ReadOnly Property Flags() As String
      Get
        Return String.Format("0x{0:x2}", usFlags)
      End Get
    End Property

    Public ReadOnly Property TotalQuestions() As String
      Get
        Return usTotalQuestions.ToString()
      End Get
    End Property

    Public ReadOnly Property TotalAnswerRRs() As String
      Get
        Return usTotalAnswerRRs.ToString()
      End Get
    End Property

    Public ReadOnly Property TotalAuthorityRRs() As String
      Get
        Return usTotalAuthorityRRs.ToString()
      End Get
    End Property

    Public ReadOnly Property TotalAdditionalRRs() As String
      Get
        Return usTotalAdditionalRRs.ToString()
      End Get
    End Property
  End Class
End Namespace
