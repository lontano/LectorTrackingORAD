Imports System.Windows.Forms

Public Class DialogAddPlayer
  Public CPuPlayerHost As TrackingPlayerHost = Nothing

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Aplicar()
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub DialogAddPlayer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ShowPlayer()
  End Sub

  Private Sub ShowPlayer()
    With Me.ComboBoxTipus
      .Items.Clear()
      .Items.Add("TCP Client")
      .Items.Add("TCP Server")
      .Items.Add("UDP")
      .Items.Add("UDP ORAD")
    End With
    If Not Me.CPuPlayerHost Is Nothing Then
      Me.TextBoxAliasName.Text = Me.CPuPlayerHost.AliasName
      Me.TextBoxHost.Text = Me.CPuPlayerHost.broadcastAddress
      Me.NumericUpDownPort.Value = Me.CPuPlayerHost.BroadcastPort
      Me.CheckBoxEnabled.Checked = Me.CPuPlayerHost.Enabled
      Me.ComboBoxTipus.Text = Me.CPuPlayerHost.Type.ToString
    End If
  End Sub

  Private Sub Aplicar()
    Me.CPuPlayerHost = New TrackingPlayerHost

    If Not Me.CPuPlayerHost Is Nothing Then
      Me.CPuPlayerHost.AliasName = Me.TextBoxAliasName.Text
      Me.CPuPlayerHost.broadcastAddress = Me.TextBoxHost.Text
      Me.CPuPlayerHost.BroadcastPort = Me.NumericUpDownPort.Value
      Me.CPuPlayerHost.Enabled = Me.CheckBoxEnabled.Checked
      Select Case Me.ComboBoxTipus.Text
        Case "TCP Client"
          Me.CPuPlayerHost.Type = eTrackingType.TCP_Client
        Case "TCP Server"
          Me.CPuPlayerHost.Type = eTrackingType.TCP_Server
        Case "UDP"
          Me.CPuPlayerHost.Type = eTrackingType.UDP
        Case "UDP ORAD"
          Me.CPuPlayerHost.Type = eTrackingType.UDP_ORAD
      End Select
    End If
  End Sub
End Class
