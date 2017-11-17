Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.ComponentModel

Public Class UDPSniffer
  Public listenPort As Integer
  Private WithEvents BackgroundListener As BackgroundWorker

  Public Event ValueReceived(ByVal CValue As TrackingValue)
  Public Event DataReceived(ByVal buffer() As Byte, ByVal text As String)

  Public Sub New(ByVal niPort As Integer)
    Try
      Me.listenPort = niPort
      Me.BackgroundListener = New BackgroundWorker
      With Me.BackgroundListener
        .WorkerSupportsCancellation = True
        .WorkerReportsProgress = True
        .RunWorkerAsync()
      End With
    Catch ex As Exception
    End Try
  End Sub


  Private Sub BackgroundListener_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles BackgroundListener.Disposed

  End Sub

  Private Structure eUPDData
    Dim Text As String
    Dim Buffer() As Byte
  End Structure

  Private Sub BackgroundListener_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundListener.DoWork
    Dim done As Boolean = False
    Dim listener As New UdpClient(listenPort)
    Dim groupEP As New IPEndPoint(IPAddress.Any, listenPort)
    Dim Data As eUPDData
    Try
      While Not done
        Console.WriteLine("Waiting for broadcast")
        Dim bytes As Byte() = listener.Receive(groupEP)
        Console.WriteLine("Received broadcast from {0} :", _
            groupEP.ToString())
        Console.WriteLine( _
            Encoding.ASCII.GetString(bytes, 0, bytes.Length))
        Console.WriteLine()
        Data.Buffer = bytes
        Data.Text = Encoding.ASCII.GetString(bytes, 0, bytes.Length)
        Me.BackgroundListener.ReportProgress(0, Data)
      End While
    Catch err As Exception
      Console.WriteLine(err.ToString())
    Finally
      listener.Close()
    End Try
  End Sub 'StartListener

  Private Sub BackgroundListener_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundListener.ProgressChanged
    Try
      Dim Data As eUPDData = CType(e.UserState, eUPDData)
      RaiseEvent DataReceived(Data.Buffer, Data.Text)
    Catch ex As Exception

    End Try
  End Sub

  Private Sub BackgroundListener_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundListener.RunWorkerCompleted

  End Sub

  Protected Overrides Sub Finalize()
    Me.BackgroundListener.CancelAsync()

    MyBase.Finalize()
  End Sub
End Class 'UDPSniffer