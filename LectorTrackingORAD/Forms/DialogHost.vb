Imports System.Windows.Forms

Public Class DialogHost
  Public CPuHost As TrackingHost

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If Me.CPuHost Is Nothing Then
      Me.CPuHost = New TrackingHost
    End If
    Me.CPuHost.Host = Me.TextBoxHost.Text
    Me.CPuHost.IP = Me.TextBoxIP.Text
    Me.CPuHost.Studio = Me.TextBoxStudio.Text
    Me.CPuHost.CamNumber = Me.NumericUpDownCAM.Value
    Me.CPuHost.SourcePort = Me.NumericUpDownSourcePort.Value
    Me.CPuHost.TargetPort = Me.NumericUpDown1.Value

    Select Case Me.ComboBoxTipus.Text
      Case eTrackingType.TCP_Client.ToString
        Me.CPuHost.TrackingType = eTrackingType.TCP_Client
      Case eTrackingType.TCP_Server.ToString
        Me.CPuHost.TrackingType = eTrackingType.TCP_Server
      Case eTrackingType.UDP.ToString
        Me.CPuHost.TrackingType = eTrackingType.UDP
      Case eTrackingType.UDP_ORAD.ToString
        Me.CPuHost.TrackingType = eTrackingType.UDP_ORAD
    End Select

    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub DialogHost_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.ShowHost()
  End Sub

  Private Sub ShowHost()
    With Me.ComboBoxTipus
      .Items.Clear()
      .Items.Add(eTrackingType.TCP_Client.ToString)
      .Items.Add(eTrackingType.TCP_Server.ToString)
      .Items.Add(eTrackingType.UDP.ToString)
      .Items.Add(eTrackingType.UDP_ORAD.ToString)
    End With
    If Not Me.CPuHost Is Nothing Then
      Me.TextBoxHost.Text = Me.CPuHost.Host
      Me.TextBoxIP.Text = Me.CPuHost.IP
      Me.TextBoxStudio.Text = Me.CPuHost.Studio
      Me.NumericUpDownCAM.Value = Me.CPuHost.CamNumber
      Me.NumericUpDownSourcePort.Value = Me.CPuHost.SourcePort
      Me.NumericUpDown1.Value = Me.CPuHost.TargetPort
      Me.ComboBoxTipus.Text = Me.CPuHost.TrackingType.ToString

    End If
  End Sub

  Private Sub ComboBoxTipus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxTipus.SelectedIndexChanged
    Select Case Me.ComboBoxTipus.Text
      Case eTrackingType.TCP_Client.ToString
        Me.GroupBoxSource.Visible = True
        Me.GroupBoxTarget.Visible = False
        Me.GroupBoxSource.Text = "Address of TCP server"
      Case eTrackingType.TCP_Server.ToString
        Me.GroupBoxSource.Visible = False
        Me.GroupBoxTarget.Visible = True
        Me.GroupBoxTarget.Text = "TCP server: receiving port"
      Case eTrackingType.UDP.ToString
        Me.GroupBoxSource.Visible = False
        Me.GroupBoxTarget.Visible = True
        Me.GroupBoxTarget.Text = "UDP: receiving port"
      Case eTrackingType.UDP_ORAD.ToString
        Me.GroupBoxSource.Visible = True
        Me.GroupBoxTarget.Visible = False
        Me.GroupBoxSource.Text = "UDP broadcast: address of the source machine"
    End Select
  End Sub
End Class
