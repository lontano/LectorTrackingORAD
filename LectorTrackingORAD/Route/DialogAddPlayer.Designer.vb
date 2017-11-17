<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogAddPlayer
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
    Me.ComboBoxTipus = New System.Windows.Forms.ComboBox
    Me.Label6 = New System.Windows.Forms.Label
    Me.GroupBoxDestí = New System.Windows.Forms.GroupBox
    Me.TextBoxAliasName = New System.Windows.Forms.TextBox
    Me.Label3 = New System.Windows.Forms.Label
    Me.CheckBoxEnabled = New System.Windows.Forms.CheckBox
    Me.TextBoxHost = New System.Windows.Forms.TextBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.NumericUpDownPort = New System.Windows.Forms.NumericUpDown
    Me.Label2 = New System.Windows.Forms.Label
    Me.TableLayoutPanel1.SuspendLayout()
    Me.GroupBoxDestí.SuspendLayout()
    CType(Me.NumericUpDownPort, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 166)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
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
    'ComboBoxTipus
    '
    Me.ComboBoxTipus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxTipus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.ComboBoxTipus.FormattingEnabled = True
    Me.ComboBoxTipus.Location = New System.Drawing.Point(79, 97)
    Me.ComboBoxTipus.Name = "ComboBoxTipus"
    Me.ComboBoxTipus.Size = New System.Drawing.Size(327, 21)
    Me.ComboBoxTipus.TabIndex = 3
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(17, 100)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(33, 13)
    Me.Label6.TabIndex = 11
    Me.Label6.Text = "Tipus"
    '
    'GroupBoxDestí
    '
    Me.GroupBoxDestí.Controls.Add(Me.TextBoxAliasName)
    Me.GroupBoxDestí.Controls.Add(Me.Label3)
    Me.GroupBoxDestí.Controls.Add(Me.CheckBoxEnabled)
    Me.GroupBoxDestí.Controls.Add(Me.TextBoxHost)
    Me.GroupBoxDestí.Controls.Add(Me.ComboBoxTipus)
    Me.GroupBoxDestí.Controls.Add(Me.Label6)
    Me.GroupBoxDestí.Controls.Add(Me.Label1)
    Me.GroupBoxDestí.Controls.Add(Me.NumericUpDownPort)
    Me.GroupBoxDestí.Controls.Add(Me.Label2)
    Me.GroupBoxDestí.Location = New System.Drawing.Point(12, 12)
    Me.GroupBoxDestí.Name = "GroupBoxDestí"
    Me.GroupBoxDestí.Size = New System.Drawing.Size(411, 148)
    Me.GroupBoxDestí.TabIndex = 13
    Me.GroupBoxDestí.TabStop = False
    Me.GroupBoxDestí.Text = "Destí"
    '
    'TextBoxAliasName
    '
    Me.TextBoxAliasName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBoxAliasName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.TextBoxAliasName.Location = New System.Drawing.Point(79, 19)
    Me.TextBoxAliasName.Name = "TextBoxAliasName"
    Me.TextBoxAliasName.Size = New System.Drawing.Size(326, 20)
    Me.TextBoxAliasName.TabIndex = 0
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(17, 22)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(35, 13)
    Me.Label3.TabIndex = 14
    Me.Label3.Text = "Name"
    '
    'CheckBoxEnabled
    '
    Me.CheckBoxEnabled.AutoSize = True
    Me.CheckBoxEnabled.Location = New System.Drawing.Point(79, 124)
    Me.CheckBoxEnabled.Name = "CheckBoxEnabled"
    Me.CheckBoxEnabled.Size = New System.Drawing.Size(50, 17)
    Me.CheckBoxEnabled.TabIndex = 4
    Me.CheckBoxEnabled.Text = "Actiu"
    Me.CheckBoxEnabled.UseVisualStyleBackColor = True
    '
    'TextBoxHost
    '
    Me.TextBoxHost.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TextBoxHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.TextBoxHost.Location = New System.Drawing.Point(79, 45)
    Me.TextBoxHost.Name = "TextBoxHost"
    Me.TextBoxHost.Size = New System.Drawing.Size(326, 20)
    Me.TextBoxHost.TabIndex = 1
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(17, 48)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(29, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Host"
    '
    'NumericUpDownPort
    '
    Me.NumericUpDownPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.NumericUpDownPort.Location = New System.Drawing.Point(79, 71)
    Me.NumericUpDownPort.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
    Me.NumericUpDownPort.Name = "NumericUpDownPort"
    Me.NumericUpDownPort.Size = New System.Drawing.Size(107, 20)
    Me.NumericUpDownPort.TabIndex = 2
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
    'DialogAddPlayer
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(435, 207)
    Me.Controls.Add(Me.GroupBoxDestí)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "DialogAddPlayer"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "DialogAddPlayer"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.GroupBoxDestí.ResumeLayout(False)
    Me.GroupBoxDestí.PerformLayout()
    CType(Me.NumericUpDownPort, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents ComboBoxTipus As System.Windows.Forms.ComboBox
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents GroupBoxDestí As System.Windows.Forms.GroupBox
  Friend WithEvents TextBoxHost As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents NumericUpDownPort As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents CheckBoxEnabled As System.Windows.Forms.CheckBox
  Friend WithEvents TextBoxAliasName As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
