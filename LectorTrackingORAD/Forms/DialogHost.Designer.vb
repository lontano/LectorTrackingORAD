<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogHost
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
    Me.OK_Button = New System.Windows.Forms.Button
    Me.Cancel_Button = New System.Windows.Forms.Button
    Me.Label1 = New System.Windows.Forms.Label
    Me.TextBoxHost = New System.Windows.Forms.TextBox
    Me.NumericUpDownSourcePort = New System.Windows.Forms.NumericUpDown
    Me.Label2 = New System.Windows.Forms.Label
    Me.Label3 = New System.Windows.Forms.Label
    Me.NumericUpDownCAM = New System.Windows.Forms.NumericUpDown
    Me.TextBoxStudio = New System.Windows.Forms.TextBox
    Me.Label4 = New System.Windows.Forms.Label
    Me.TextBoxIP = New System.Windows.Forms.TextBox
    Me.Label5 = New System.Windows.Forms.Label
    Me.GroupBoxData = New System.Windows.Forms.GroupBox
    Me.GroupBoxSource = New System.Windows.Forms.GroupBox
    Me.Label6 = New System.Windows.Forms.Label
    Me.ComboBoxTipus = New System.Windows.Forms.ComboBox
    Me.GroupBoxTarget = New System.Windows.Forms.GroupBox
    Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown
    Me.Label9 = New System.Windows.Forms.Label
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.NumericUpDownSourcePort, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDownCAM, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBoxData.SuspendLayout()
    Me.GroupBoxSource.SuspendLayout()
    Me.GroupBoxTarget.SuspendLayout()
    CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 257)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "Aceptar"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "Cancelar"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(17, 22)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(29, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Host"
    '
    'TextBoxHost
    '
    Me.TextBoxHost.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBoxHost.Location = New System.Drawing.Point(79, 19)
    Me.TextBoxHost.Name = "TextBoxHost"
    Me.TextBoxHost.Size = New System.Drawing.Size(326, 20)
    Me.TextBoxHost.TabIndex = 1
    '
    'NumericUpDownSourcePort
    '
    Me.NumericUpDownSourcePort.Location = New System.Drawing.Point(79, 71)
    Me.NumericUpDownSourcePort.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
    Me.NumericUpDownSourcePort.Name = "NumericUpDownSourcePort"
    Me.NumericUpDownSourcePort.Size = New System.Drawing.Size(107, 20)
    Me.NumericUpDownSourcePort.TabIndex = 3
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(17, 73)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(26, 13)
    Me.Label2.TabIndex = 4
    Me.Label2.Text = "Port"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(16, 41)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(30, 13)
    Me.Label3.TabIndex = 8
    Me.Label3.Text = "CAM"
    '
    'NumericUpDownCAM
    '
    Me.NumericUpDownCAM.Location = New System.Drawing.Point(78, 39)
    Me.NumericUpDownCAM.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
    Me.NumericUpDownCAM.Name = "NumericUpDownCAM"
    Me.NumericUpDownCAM.Size = New System.Drawing.Size(107, 20)
    Me.NumericUpDownCAM.TabIndex = 5
    '
    'TextBoxStudio
    '
    Me.TextBoxStudio.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBoxStudio.Location = New System.Drawing.Point(78, 13)
    Me.TextBoxStudio.Name = "TextBoxStudio"
    Me.TextBoxStudio.Size = New System.Drawing.Size(327, 20)
    Me.TextBoxStudio.TabIndex = 4
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(16, 16)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(37, 13)
    Me.Label4.TabIndex = 5
    Me.Label4.Text = "Studio"
    '
    'TextBoxIP
    '
    Me.TextBoxIP.Location = New System.Drawing.Point(79, 45)
    Me.TextBoxIP.Name = "TextBoxIP"
    Me.TextBoxIP.Size = New System.Drawing.Size(107, 20)
    Me.TextBoxIP.TabIndex = 2
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(17, 48)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(17, 13)
    Me.Label5.TabIndex = 9
    Me.Label5.Text = "IP"
    '
    'GroupBoxData
    '
    Me.GroupBoxData.Controls.Add(Me.ComboBoxTipus)
    Me.GroupBoxData.Controls.Add(Me.Label6)
    Me.GroupBoxData.Controls.Add(Me.Label4)
    Me.GroupBoxData.Controls.Add(Me.TextBoxStudio)
    Me.GroupBoxData.Controls.Add(Me.NumericUpDownCAM)
    Me.GroupBoxData.Controls.Add(Me.Label3)
    Me.GroupBoxData.Location = New System.Drawing.Point(12, 12)
    Me.GroupBoxData.Name = "GroupBoxData"
    Me.GroupBoxData.Size = New System.Drawing.Size(411, 98)
    Me.GroupBoxData.TabIndex = 10
    Me.GroupBoxData.TabStop = False
    Me.GroupBoxData.Text = "Dades"
    '
    'GroupBoxSource
    '
    Me.GroupBoxSource.Controls.Add(Me.TextBoxHost)
    Me.GroupBoxSource.Controls.Add(Me.Label1)
    Me.GroupBoxSource.Controls.Add(Me.TextBoxIP)
    Me.GroupBoxSource.Controls.Add(Me.NumericUpDownSourcePort)
    Me.GroupBoxSource.Controls.Add(Me.Label5)
    Me.GroupBoxSource.Controls.Add(Me.Label2)
    Me.GroupBoxSource.Location = New System.Drawing.Point(12, 116)
    Me.GroupBoxSource.Name = "GroupBoxSource"
    Me.GroupBoxSource.Size = New System.Drawing.Size(411, 103)
    Me.GroupBoxSource.TabIndex = 11
    Me.GroupBoxSource.TabStop = False
    Me.GroupBoxSource.Text = "Origen"
    Me.GroupBoxSource.Visible = False
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(16, 68)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(33, 13)
    Me.Label6.TabIndex = 9
    Me.Label6.Text = "Tipus"
    '
    'ComboBoxTipus
    '
    Me.ComboBoxTipus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxTipus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.ComboBoxTipus.FormattingEnabled = True
    Me.ComboBoxTipus.Location = New System.Drawing.Point(78, 65)
    Me.ComboBoxTipus.Name = "ComboBoxTipus"
    Me.ComboBoxTipus.Size = New System.Drawing.Size(327, 21)
    Me.ComboBoxTipus.TabIndex = 10
    '
    'GroupBoxTarget
    '
    Me.GroupBoxTarget.Controls.Add(Me.NumericUpDown1)
    Me.GroupBoxTarget.Controls.Add(Me.Label9)
    Me.GroupBoxTarget.Location = New System.Drawing.Point(12, 118)
    Me.GroupBoxTarget.Name = "GroupBoxTarget"
    Me.GroupBoxTarget.Size = New System.Drawing.Size(411, 50)
    Me.GroupBoxTarget.TabIndex = 12
    Me.GroupBoxTarget.TabStop = False
    Me.GroupBoxTarget.Text = "Destí"
    Me.GroupBoxTarget.Visible = False
    '
    'NumericUpDown1
    '
    Me.NumericUpDown1.Location = New System.Drawing.Point(78, 19)
    Me.NumericUpDown1.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
    Me.NumericUpDown1.Name = "NumericUpDown1"
    Me.NumericUpDown1.Size = New System.Drawing.Size(107, 20)
    Me.NumericUpDown1.TabIndex = 3
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Location = New System.Drawing.Point(16, 21)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(26, 13)
    Me.Label9.TabIndex = 4
    Me.Label9.Text = "Port"
    '
    'DialogHost
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(435, 298)
    Me.Controls.Add(Me.GroupBoxTarget)
    Me.Controls.Add(Me.GroupBoxSource)
    Me.Controls.Add(Me.GroupBoxData)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "DialogHost"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "DialogHost"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.NumericUpDownSourcePort, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDownCAM, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GroupBoxData.ResumeLayout(False)
    Me.GroupBoxData.PerformLayout()
    Me.GroupBoxSource.ResumeLayout(False)
    Me.GroupBoxSource.PerformLayout()
    Me.GroupBoxTarget.ResumeLayout(False)
    Me.GroupBoxTarget.PerformLayout()
    CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents TextBoxHost As System.Windows.Forms.TextBox
  Friend WithEvents NumericUpDownSourcePort As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents NumericUpDownCAM As System.Windows.Forms.NumericUpDown
  Friend WithEvents TextBoxStudio As System.Windows.Forms.TextBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents TextBoxIP As System.Windows.Forms.TextBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents GroupBoxData As System.Windows.Forms.GroupBox
  Friend WithEvents GroupBoxSource As System.Windows.Forms.GroupBox
  Friend WithEvents ComboBoxTipus As System.Windows.Forms.ComboBox
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents GroupBoxTarget As System.Windows.Forms.GroupBox
  Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label9 As System.Windows.Forms.Label

End Class
