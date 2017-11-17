Imports System.Net
Imports System.Net.Sockets
Imports System.ComponentModel

Public Class TrackingPlayerTCP_Server
  Inherits ATrackingPlayer


#Region "Broadcast"
  '''''''''''''''''''''''Set up variables''''''''''''''''''''
  Private sendingServer As Net.Sockets.TcpListener                           'Client for sending data
  Private receivingThread As Threading.Thread                            'Create a separate thread to listen for incoming data, helps to prevent the form from freezing up
  Private closing As Boolean = False                           'Used to close clients if form is closing
  
  Private LlistaClients As New List(Of Net.Sockets.TcpClient)
  Private LlistaStreams As New List(Of NetworkStream)

  Private WithEvents CPiBackgroundWorker As BackgroundWorker

  ''''''''''''''''''''Setup sender client'''''''''''''''''
  Public Overrides Sub InitializeSender()
    Try
      Me.sPiName = "TCP"
      sendingServer = New Net.Sockets.TcpListener(IPAddress.Any, Me.TrackingPlayerHost.BroadcastPort)
      Me.CPiBackgroundWorker.WorkerReportsProgress = True
      Me.CPiBackgroundWorker.WorkerSupportsCancellation = True
      Me.CPiBackgroundWorker.RunWorkerAsync()
    Catch ex As Exception
      Debug.Print(ex.ToString)
    End Try
  End Sub

  Public Overrides Sub InitComm()
    InitializeSender()          'Initializes startup of sender client
  End Sub

  Public Overrides Sub CloseComm()
    closing = True          'Tells receiving loop to close
    For Each stream As NetworkStream In Me.LlistaStreams
      stream.Close()
    Next
    For Each client As Net.Sockets.TcpClient In Me.LlistaClients
      client.Close()
    Next
    Me.LlistaStreams.Clear()
    Me.LlistaClients.Clear()
  End Sub


  Public Overrides Sub BroadcastValue(ByVal CiValue As TrackingValue)
    Try
      Dim buffer() As Byte = CiValue.buffer
      For nIndex As Integer = Me.LlistaStreams.Count - 1 To 0 Step -1
        If Me.LlistaClients(nIndex).Connected = False Then
          Me.LlistaClients.RemoveAt(nIndex)
          Me.LlistaStreams.RemoveAt(nIndex)
        Else
          Me.LlistaStreams(nIndex).Write(buffer, 0, buffer.Length)
        End If
      Next
    Catch ex As Exception
    End Try
  End Sub
#End Region

#Region "Backgroundworker"
  Private Sub CPiBackgroundWorker_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles CPiBackgroundWorker.Disposed

  End Sub

  Private Sub CPiBackgroundWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles CPiBackgroundWorker.DoWork
    Try
      While Not Me.CPiBackgroundWorker.CancellationPending
        'Esperem connexions
        Dim client As TcpClient = Me.sendingServer.AcceptTcpClient()
        'Connexió feta!
        Dim stream As NetworkStream = client.GetStream()
        Me.LlistaClients.Add(client)
        Me.LlistaStreams.Add(stream)
        Me.CPiBackgroundWorker.ReportProgress(0, client)
      End While
    Catch ex As Exception

    End Try
  End Sub

  Private Sub CPiBackgroundWorker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles CPiBackgroundWorker.ProgressChanged
    Try
      Dim CClient As TcpClient = CType(e.UserState, TcpClient)
      Debug.Print(CClient.ToString)
    Catch ex As Exception

    End Try
  End Sub

  Private Sub CPiBackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles CPiBackgroundWorker.RunWorkerCompleted

  End Sub
#End Region
End Class
