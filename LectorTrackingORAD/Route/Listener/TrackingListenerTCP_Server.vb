Imports System.Net
Imports System.Net.Sockets
Imports system.io
Imports System.ComponentModel

Namespace TrackingListener
  Public Class TrackingListenerTCP_Server
    Inherits TrackingListener.ATrackingListener
    Private receivingServer As Net.Sockets.TcpListener                         'Client for handling incoming data

    Private LlistaClients As New List(Of Net.Sockets.TcpClient)
    Private LlistaBackgroundWorkers As New List(Of BackgroundWorker)


    '''''''''''''''''''''Setup receiving client'''''''''''''
    Public Overrides Sub InitializeReceiver()
      receivingServer = New Net.Sockets.TcpListener(IPAddress.Any, Me.receivePort)
      InitializeReceiverThread()
    End Sub

    Public Overrides Sub CloseComm()
      closing = True          'Tells receiving loop to close
      receivingServer.Stop()
      Me.BackgroundListener.CancelAsync()

    End Sub

    Public Overrides Sub InitComm()
      InitializeReceiver()
    End Sub

    Public Event NewValueReceived(ByVal CiValue As TrackingValue)

    '''''''''''''''''''''Start receiving loop''''''''''''''''''''''' 
    Public Overrides Sub Receiver()
      Dim endPoint As Net.IPEndPoint = New Net.IPEndPoint(Net.IPAddress.Any, Me.receivePort) 'Listen for incoming data from any IP address on the specified port (i personally select 9653)
      Dim message As String = ""
      Dim data As String
      Dim bytes(65535) As Byte
      Dim bytesRead As Integer

      receivingServer.Start()

      While Not Me.BackgroundListener.CancellationPending
        Try
          If receivingServer.Pending() Then
            Dim client As TcpClient = receivingServer.AcceptTcpClient()
            Console.WriteLine("Connected!")

            data = Nothing

            ' Get a stream object for reading and writing
            Dim clientStream As NetworkStream = client.GetStream()
            Dim messageStream As MemoryStream = New MemoryStream

            message = ""
            While client.Connected And Not Me.BackgroundListener.CancellationPending
              If clientStream.DataAvailable() Then
                bytesRead = clientStream.Read(bytes, 0, bytes.Length)
                Dim CValue As New TrackingValue(0)
                CValue.FromBuffer(bytes, eTrackingType.UDP_ORAD)
                Me.BackgroundListener.ReportProgress(0, CValue)
              End If
            End While
          End If
        Catch ex As Exception

        End Try
      End While

    End Sub
  End Class
End Namespace