Namespace TrackingListener
  Public Class TrackingListenerUDP
    Inherits TrackingListener.ATrackingListener
    Private receivingClient As Net.Sockets.UdpClient                         'Client for handling incoming data


    '''''''''''''''''''''Setup receiving client'''''''''''''

    Public Overrides Sub InitializeReceiver()
      receivingClient = New Net.Sockets.UdpClient(Me.receivePort)
    End Sub

    Public Overrides Sub CloseComm()
      closing = True          'Tells receiving loop to close
      receivingClient.Close()
    End Sub

    Public Overrides Sub InitComm()
      InitializeReceiver()
      InitializeReceiverThread()
    End Sub


    '''''''''''''''''''''Start receiving loop''''''''''''''''''''''' 

    Public Overrides Sub Receiver()
      Dim endPoint As Net.IPEndPoint = New Net.IPEndPoint(Net.IPAddress.Any, Me.receivePort) 'Listen for incoming data from any IP address on the specified port (i personally select 9653)
      Dim message As String = ""
      While (True)                                                    'Setup an infinite loop
        Dim data() As Byte                                            'buffer for storing incoming bytes
        Try
          data = receivingClient.Receive(endPoint)                    'Receive incoming bytes 
          Dim CValue As New TrackingValue(0)
          CValue.FromBuffer(data, eTrackingType.UDP_ORAD)
        Catch ex As Exception
        End Try
        If closing = True Then                                        'Exit sub if form is closing
          Exit Sub
        End If
        Debug.Print(Now.ToString & " " & message)
      End While
    End Sub
  End Class
End Namespace