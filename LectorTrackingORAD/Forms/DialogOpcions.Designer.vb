<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogOpcions
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
        Me.components = New System.ComponentModel.Container()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.CheckBoxHosts = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStripHosts = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AfegirHostToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EliminarHostToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditarHostToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeaderStudio = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderCAM = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderHost = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderIP = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderPort = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderTargetPort = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderProtocol = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ButtonScan = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.ContextMenuStripHosts.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonScan, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 274)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(692, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Location = New System.Drawing.Point(495, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(94, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Aceptar"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.Location = New System.Drawing.Point(595, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(94, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancelar"
        '
        'CheckBoxHosts
        '
        Me.CheckBoxHosts.AutoSize = True
        Me.CheckBoxHosts.Location = New System.Drawing.Point(12, 12)
        Me.CheckBoxHosts.Name = "CheckBoxHosts"
        Me.CheckBoxHosts.Size = New System.Drawing.Size(177, 17)
        Me.CheckBoxHosts.TabIndex = 1
        Me.CheckBoxHosts.Text = "Limitar els hosts als que escoltar"
        Me.CheckBoxHosts.UseVisualStyleBackColor = True
        '
        'ContextMenuStripHosts
        '
        Me.ContextMenuStripHosts.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AfegirHostToolStripMenuItem, Me.EliminarHostToolStripMenuItem, Me.EditarHostToolStripMenuItem})
        Me.ContextMenuStripHosts.Name = "ContextMenuStripHosts"
        Me.ContextMenuStripHosts.Size = New System.Drawing.Size(153, 70)
        '
        'AfegirHostToolStripMenuItem
        '
        Me.AfegirHostToolStripMenuItem.Name = "AfegirHostToolStripMenuItem"
        Me.AfegirHostToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AfegirHostToolStripMenuItem.Text = "Afegir host..."
        '
        'EliminarHostToolStripMenuItem
        '
        Me.EliminarHostToolStripMenuItem.Name = "EliminarHostToolStripMenuItem"
        Me.EliminarHostToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.EliminarHostToolStripMenuItem.Text = "Eliminar host..."
        '
        'EditarHostToolStripMenuItem
        '
        Me.EditarHostToolStripMenuItem.Name = "EditarHostToolStripMenuItem"
        Me.EditarHostToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.EditarHostToolStripMenuItem.Text = "Editar host..."
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderStudio, Me.ColumnHeaderCAM, Me.ColumnHeaderHost, Me.ColumnHeaderIP, Me.ColumnHeaderPort, Me.ColumnHeaderTargetPort, Me.ColumnHeaderProtocol})
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStripHosts
        Me.ListView1.Location = New System.Drawing.Point(12, 35)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(692, 233)
        Me.ListView1.TabIndex = 3
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeaderStudio
        '
        Me.ColumnHeaderStudio.Text = "Studio"
        Me.ColumnHeaderStudio.Width = 120
        '
        'ColumnHeaderCAM
        '
        Me.ColumnHeaderCAM.Text = "CAM"
        '
        'ColumnHeaderHost
        '
        Me.ColumnHeaderHost.Text = "Host"
        Me.ColumnHeaderHost.Width = 150
        '
        'ColumnHeaderIP
        '
        Me.ColumnHeaderIP.Text = "IP"
        Me.ColumnHeaderIP.Width = 120
        '
        'ColumnHeaderPort
        '
        Me.ColumnHeaderPort.Text = "Port"
        Me.ColumnHeaderPort.Width = 40
        '
        'ColumnHeaderTargetPort
        '
        Me.ColumnHeaderTargetPort.Text = "TargetPort"
        '
        'ColumnHeaderProtocol
        '
        Me.ColumnHeaderProtocol.Text = "Protocol"
        Me.ColumnHeaderProtocol.Width = 110
        '
        'ButtonScan
        '
        Me.ButtonScan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonScan.Location = New System.Drawing.Point(3, 3)
        Me.ButtonScan.Name = "ButtonScan"
        Me.ButtonScan.Size = New System.Drawing.Size(114, 23)
        Me.ButtonScan.TabIndex = 4
        Me.ButtonScan.Text = "Buscar ports..."
        Me.ButtonScan.UseVisualStyleBackColor = True
        '
        'DialogOpcions
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(716, 315)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.CheckBoxHosts)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DialogOpcions"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "DialogConfig"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ContextMenuStripHosts.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents CheckBoxHosts As System.Windows.Forms.CheckBox
  Friend WithEvents ContextMenuStripHosts As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents AfegirHostToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents EliminarHostToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ListView1 As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeaderHost As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeaderPort As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeaderStudio As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeaderCAM As System.Windows.Forms.ColumnHeader
  Friend WithEvents EditarHostToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ButtonScan As System.Windows.Forms.Button
  Friend WithEvents ColumnHeaderIP As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeaderProtocol As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeaderTargetPort As System.Windows.Forms.ColumnHeader

End Class
