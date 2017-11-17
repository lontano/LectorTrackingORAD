Imports System.Collections.Generic
Imports System.Text
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms


Namespace Connections
  Public Class UDPSender
    Private CPiClient As UdpClient

    Private sPiHostName As String
    Private CPiIPAddress As IPAddress
    Private nPiPort As Integer
    Private bytCommand() As Byte

    Public Event ErrorEvent(ByVal CiException As Exception)
    Public Event ActivityOutgoing()


    Public Function Connect(ByVal siHost As String, ByVal niPort As Integer) As Boolean
      Try
        Me.nPiPort = niPort
        Me.CPiClient = New UdpClient
        If IPAddress.TryParse(siHost, Me.CPiIPAddress) Then
          Me.sPiHostName = ""
          Me.CPiClient.Connect(Me.CPiIPAddress, Me.nPiPort)
        Else
          Me.sPiHostName = siHost
          Me.CPiClient.Connect(Me.sPiHostName, Me.nPiPort)
        End If
      Catch ex As Exception
        RaiseEvent ErrorEvent(ex)
      End Try
    End Function

    Public Function SendData(ByVal siData As String) As Integer
      Dim nRes As Integer = 0
      Try
        Me.bytCommand = Encoding.UTF8.GetBytes(siData)
        nRes = Me.CPiClient.Send(Me.bytCommand, Me.bytCommand.Length)
        RaiseEvent ActivityOutgoing()
      Catch ex As Exception
        RaiseEvent ErrorEvent(ex)
      End Try
      Return nRes
    End Function

    Public Sub Disconnect()
      Try
        Me.CPiClient.Close()
        Me.CPiClient = Nothing
      Catch ex As Exception
        RaiseEvent ErrorEvent(ex)
      End Try
    End Sub

  End Class


  Public Class UDPReceiver
    Private CPiClient As UdpClient

    Private CPiReceivingIPEndPoint As System.Net.IPEndPoint
    Private WithEvents CPiBackgroundWorker As System.ComponentModel.BackgroundWorker

    Private sPiHostName As String
    Private CPiIPAddress As IPAddress
    Private nPiPort As Integer
    Private bytCommand() As Byte

    Public Event ErrorEvent(ByVal CiException As Exception)
    Public Event DataReceive(ByVal siData As String)
    Public Event ActivityIncoming()

    Public ReceiverBusy As Boolean = False

    Private CPiLlistaData As New List(Of String)
    Private CPiLlistaDataIPEndPoint As New List(Of System.Net.IPEndPoint)

    Public Function Listen(ByVal niPort As Integer) As Boolean
      Try
        Me.nPiPort = niPort
        Me.CPiReceivingIPEndPoint = New System.Net.IPEndPoint(System.Net.IPAddress.Any, 0)
        Me.CPiClient = New System.Net.Sockets.UdpClient(niPort)


        Me.CPiBackgroundWorker = New System.ComponentModel.BackgroundWorker
        Me.CPiBackgroundWorker.WorkerReportsProgress = True
        Me.CPiBackgroundWorker.WorkerSupportsCancellation = True
        Me.CPiBackgroundWorker.RunWorkerAsync()

      Catch ex As Exception
        RaiseEvent ErrorEvent(ex)
      End Try
    End Function

    Public Sub Disconnect()
      Try
        Me.CPiBackgroundWorker.CancelAsync()
        Me.CPiBackgroundWorker.Dispose()
        Me.CPiClient.Close()
        Me.CPiClient = Nothing
      Catch ex As Exception
        RaiseEvent ErrorEvent(ex)
      End Try
    End Sub

    Private Sub CPiBackgroundWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles CPiBackgroundWorker.DoWork
      Dim receiveBytes() As Byte
      Dim bDone As Boolean = False
      Dim RemoteIpEndPoint As New IPEndPoint(IPAddress.Any, 0)
      Try
        While Not Me.CPiBackgroundWorker.CancellationPending
          RemoteIpEndPoint = New IPEndPoint(IPAddress.Any, 0)
          receiveBytes = Me.CPiClient.Receive(RemoteIpEndPoint)
          If Not ReceiverBusy Then
            Dim sData As String = System.Text.Encoding.UTF8.GetString(receiveBytes)
            Me.CPiBackgroundWorker.ReportProgress(0, sData)
            ReceiverBusy = False
          End If
        End While
      Catch ex As Exception
        'RaiseEvent ErrorEvent(ex)
      End Try
    End Sub

    Private Sub CPiBackgroundWorker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles CPiBackgroundWorker.ProgressChanged
      Try
        Dim sAux As String = CStr(e.UserState)
        RaiseEvent ActivityIncoming()
        RaiseEvent DataReceive(sAux)
      Catch ex As Exception
        RaiseEvent ErrorEvent(ex)
      End Try
    End Sub
  End Class

End Namespace