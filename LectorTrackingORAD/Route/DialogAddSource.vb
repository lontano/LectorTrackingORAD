Imports System.Windows.Forms

Public Class DialogAddSource
  Public CPuTrackingFile As TrackingFile
  Public LlistaSelectedHosts As New List(Of TrackingHost)

  Private bPiInit As Boolean

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    For nIndex As Integer = 0 To Me.ListView1.SelectedIndices.Count - 1
      Me.LlistaSelectedHosts.Add(gudtCnfg.Hosts.LlistaHosts(Me.ListView1.SelectedIndices(nIndex)))
    Next
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub DialogAddSource_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    mostrarhosts()
  End Sub

  Private Sub MostrarHosts()
    bPiInit = True
    Try
      With Me.ListView1
        .Items.Clear()
        For Each CHost As TrackingHost In gudtCnfg.Hosts.LlistaHosts
          Dim CItem As ListViewItem = .Items.Add(CHost.Host)
          CItem.SubItems.Add(CHost.IP)
          CItem.SubItems.Add(CHost.SourcePort)
          CItem.SubItems.Add(CHost.Studio)
          CItem.SubItems.Add(CHost.CamNumber)
        Next
      End With
    Catch ex As Exception
    End Try
    bPiInit = False
  End Sub
End Class
