Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Net.Sockets
Imports System.Net


Public Class UDPPlayer
  Private mainSocket As Socket

  Private GroupEP As IPEndPoint
  Private SendUdp As New UdpClient()
  Private GroupIP As IPAddress

  Public Sub SendValue(ByVal CiTrackingValue As TrackingValue)
    Try
      Debug.Print(CiTrackingValue.MessageBytesString)


      Dim bteSendDate() As Byte

      Try
        bteSendDate = Encoding.Unicode.GetBytes("message")
        SendUdp.Send(CiTrackingValue.MessageBytes, bteSendDate.Length, GroupEP)

      Catch ex As Exception
        Console.WriteLine(ex.Message)
      End Try
    Catch ex As Exception

    End Try
  End Sub

  Public Sub New()
    Try
      GroupIP = IPAddress.Parse("127.0.0.1")
      GroupEP = New IPEndPoint(GroupIP, 18746)

    Catch ex As Exception

    End Try



  End Sub
End Class
