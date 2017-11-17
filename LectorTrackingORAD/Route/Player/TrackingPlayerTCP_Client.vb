Imports System.Net
Imports System.Net.Sockets


Public Class TrackingPlayerTCP_Client
  Inherits ATrackingPlayer


#Region "Broadcast"
  '''''''''''''''''''''''Set up variables''''''''''''''''''''
  Private sendingClient As Net.Sockets.TcpClient                           'Client for sending data
  Private receivingThread As Threading.Thread                            'Create a separate thread to listen for incoming data, helps to prevent the form from freezing up
  Private closing As Boolean = False                           'Used to close clients if form is closing
  Private stream As NetworkStream

  ''''''''''''''''''''Setup sender client'''''''''''''''''
  Public Overrides Sub InitializeSender()
    Try
      Me.sPiName = "TCP"
      sendingClient = New Net.Sockets.TcpClient(Me.TrackingPlayerHost.broadcastAddress, Me.TrackingPlayerHost.BroadcastPort)
      stream = sendingClient.GetStream()
    Catch ex As Exception
      Debug.Print(ex.ToString)
    End Try

  End Sub

  Public Overrides Sub InitComm()
    InitializeSender()          'Initializes startup of sender client
  End Sub

  Public Overrides Sub CloseComm()
    closing = True          'Tells receiving loop to close
    sendingClient.Close()
  End Sub


  Public Overrides Sub BroadcastValue(ByVal CiValue As TrackingValue)
    Try
      If Me.sendingClient.Connected = False Or stream Is Nothing Then
        Me.InitializeSender()
      End If
      stream.Write(CiValue.buffer, 0, CiValue.buffer.Length)
    Catch ex As Exception
      stream.Close()
      stream = Nothing
      Select Case ex.InnerException

      End Select
    End Try
  End Sub
#End Region
End Class
