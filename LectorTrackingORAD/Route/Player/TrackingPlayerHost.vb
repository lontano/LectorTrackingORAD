Public Class TrackingPlayerHost
  Public broadcastAddress As String = "255.255.255.255" 'Sends data to all LOCAL listening clients, to send data over WAN you'll need to enter a public (external) IP address of the other client
  Public BroadcastPort As Integer = 9653
  Public AliasName As String
  Public Type As eTrackingType = eTrackingType.TCP_Client
  Public Enabled As Boolean = False


  Public Shared Operator =(ByVal v1 As TrackingPlayerHost, ByVal v2 As TrackingPlayerHost) As Boolean
    Dim bRes As Boolean
    Try
      bRes = True
      bRes = bRes And (v1.broadcastAddress = v2.broadcastAddress)
      bRes = bRes And (v1.BroadcastPort = v2.BroadcastPort)
      bRes = bRes And (v1.Type = v2.Type)

      'el alias name no és important!
      bRes = bRes And (v1.AliasName = v2.AliasName)
      bRes = bRes And (v1.Enabled = v2.Enabled)

    Catch ex As Exception

    End Try
  End Operator

  Public Shared Operator <>(ByVal v1 As TrackingPlayerHost, ByVal v2 As TrackingPlayerHost) As Boolean
    Return Not (v1 = v2)
  End Operator
End Class
