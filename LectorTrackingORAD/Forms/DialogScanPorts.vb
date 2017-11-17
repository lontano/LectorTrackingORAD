Imports System.Windows.Forms

Public Class DialogScanPorts
  Public Event SelectHost(ByVal siHost As String, ByVal siIP As String, ByVal niPort As Integer)

  Private WithEvents CPiSniffer As TrackingListener.TrackingListenerUDP_adv
  Private nPiSelectedIndex As Integer = -1
  Public CPuHosts As TrackingHosts

  Private Enum eCols
    Host
    IP
    Port
    Packets
    Total
  End Enum

  Private Sub DialogScanPorts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    CPiSniffer = New TrackingListener.TrackingListenerUDP_adv()
    CPiSniffer.InitComm()
  End Sub

  Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
    Try
      Me.nPiSelectedIndex = ListView1.SelectedItems(0).Index

      RaiseEvent SelectHost(Me.ListView1.Items(Me.nPiSelectedIndex).SubItems(eCols.Host).Text, Me.ListView1.Items(Me.nPiSelectedIndex).SubItems(eCols.IP).Text, Me.ListView1.Items(Me.nPiSelectedIndex).SubItems(eCols.Port).Text)
    Catch ex As Exception
    End Try
  End Sub

  Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
    If Me.ListView1.SelectedItems.Count > 0 Then
      Me.nPiSelectedIndex = ListView1.SelectedIndices(0)
    Else
      Me.nPiSelectedIndex = -1
    End If
  End Sub

  Private Sub CPiSniffer_ValueReceived(ByVal CiValue As TrackingValue) Handles CPiSniffer.ValueReceived
    Try
      If Not CiValue Is Nothing Then
        With Me.ListView1
          Dim CItem As ListViewItem = Nothing
          For i As Integer = 0 To .Items.Count - 1
            If .Items(i).SubItems(eCols.Host).Text = CiValue.HOST And .Items(i).SubItems(eCols.Port).Text Then
              CItem = .Items(i)
              Exit For
            End If
          Next
          If CItem Is Nothing Then
            CItem = .Items.Add(gHostEntries.GetDNSHostEntry(CiValue.HOST))
            CItem.SubItems.Add(CiValue.IP)
            CItem.SubItems.Add(CiValue.PORT)
            CItem.SubItems.Add(0)
          End If
          CItem.SubItems(eCols.Packets).Text = CInt(CItem.SubItems(eCols.Packets).Text) + 1
        End With

      End If
    Catch ex As Exception
      Debug.Print(ex.ToString)
    End Try
  End Sub
End Class
