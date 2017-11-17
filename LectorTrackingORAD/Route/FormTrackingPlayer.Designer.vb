<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormTrackingPlayer
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
    Me.components = New System.ComponentModel.Container
    Me.Button1 = New System.Windows.Forms.Button
    Me.ListViewSources = New System.Windows.Forms.ListView
    Me.ColumnHeaderHost = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeaderStudio = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeaderCAM = New System.Windows.Forms.ColumnHeader
    Me.ContextMenuStripSources = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.AfegirFontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.EliminarFontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ListViewPlayers = New System.Windows.Forms.ListView
    Me.ColumnHeaderName = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
    Me.ContextMenuStripPlayers = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.AfegirPlayerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.EliminarPlayerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.EditarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.CheckBoxForceSend = New System.Windows.Forms.CheckBox
    Me.CheckBoxRunning = New System.Windows.Forms.CheckBox
    Me.TimerForceSend = New System.Windows.Forms.Timer(Me.components)
    Me.ContextMenuStripSources.SuspendLayout()
    Me.ContextMenuStripPlayers.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'Button1
    '
    Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.Button1.Location = New System.Drawing.Point(6, 19)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(191, 31)
    Me.Button1.TabIndex = 0
    Me.Button1.Text = "Button1"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'ListViewSources
    '
    Me.ListViewSources.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.ListViewSources.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderHost, Me.ColumnHeaderStudio, Me.ColumnHeaderCAM})
    Me.ListViewSources.ContextMenuStrip = Me.ContextMenuStripSources
    Me.ListViewSources.FullRowSelect = True
    Me.ListViewSources.HideSelection = False
    Me.ListViewSources.Location = New System.Drawing.Point(12, 12)
    Me.ListViewSources.MultiSelect = False
    Me.ListViewSources.Name = "ListViewSources"
    Me.ListViewSources.Size = New System.Drawing.Size(511, 201)
    Me.ListViewSources.TabIndex = 1
    Me.ListViewSources.UseCompatibleStateImageBehavior = False
    Me.ListViewSources.View = System.Windows.Forms.View.Details
    '
    'ColumnHeaderHost
    '
    Me.ColumnHeaderHost.Text = "Host"
    Me.ColumnHeaderHost.Width = 200
    '
    'ColumnHeaderStudio
    '
    Me.ColumnHeaderStudio.Text = "Studio"
    Me.ColumnHeaderStudio.Width = 220
    '
    'ColumnHeaderCAM
    '
    Me.ColumnHeaderCAM.Text = "CAM"
    Me.ColumnHeaderCAM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.ColumnHeaderCAM.Width = 50
    '
    'ContextMenuStripSources
    '
    Me.ContextMenuStripSources.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AfegirFontToolStripMenuItem, Me.EliminarFontToolStripMenuItem})
    Me.ContextMenuStripSources.Name = "ContextMenuStripSources"
    Me.ContextMenuStripSources.Size = New System.Drawing.Size(152, 48)
    '
    'AfegirFontToolStripMenuItem
    '
    Me.AfegirFontToolStripMenuItem.Name = "AfegirFontToolStripMenuItem"
    Me.AfegirFontToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
    Me.AfegirFontToolStripMenuItem.Text = "Afegir font..."
    '
    'EliminarFontToolStripMenuItem
    '
    Me.EliminarFontToolStripMenuItem.Name = "EliminarFontToolStripMenuItem"
    Me.EliminarFontToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
    Me.EliminarFontToolStripMenuItem.Text = "Eliminar font..."
    '
    'ListViewPlayers
    '
    Me.ListViewPlayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.ListViewPlayers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderName, Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
    Me.ListViewPlayers.ContextMenuStrip = Me.ContextMenuStripPlayers
    Me.ListViewPlayers.FullRowSelect = True
    Me.ListViewPlayers.HideSelection = False
    Me.ListViewPlayers.Location = New System.Drawing.Point(12, 219)
    Me.ListViewPlayers.MultiSelect = False
    Me.ListViewPlayers.Name = "ListViewPlayers"
    Me.ListViewPlayers.Size = New System.Drawing.Size(511, 201)
    Me.ListViewPlayers.TabIndex = 2
    Me.ListViewPlayers.UseCompatibleStateImageBehavior = False
    Me.ListViewPlayers.View = System.Windows.Forms.View.Details
    '
    'ColumnHeaderName
    '
    Me.ColumnHeaderName.Text = "Name"
    Me.ColumnHeaderName.Width = 200
    '
    'ColumnHeader1
    '
    Me.ColumnHeader1.Text = "Host"
    Me.ColumnHeader1.Width = 90
    '
    'ColumnHeader2
    '
    Me.ColumnHeader2.Text = "Port"
    Me.ColumnHeader2.Width = 90
    '
    'ColumnHeader3
    '
    Me.ColumnHeader3.Text = "Protocol"
    Me.ColumnHeader3.Width = 110
    '
    'ContextMenuStripPlayers
    '
    Me.ContextMenuStripPlayers.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AfegirPlayerToolStripMenuItem, Me.EliminarPlayerToolStripMenuItem, Me.EditarToolStripMenuItem})
    Me.ContextMenuStripPlayers.Name = "ContextMenuStripPlayers"
    Me.ContextMenuStripPlayers.Size = New System.Drawing.Size(162, 70)
    '
    'AfegirPlayerToolStripMenuItem
    '
    Me.AfegirPlayerToolStripMenuItem.Name = "AfegirPlayerToolStripMenuItem"
    Me.AfegirPlayerToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
    Me.AfegirPlayerToolStripMenuItem.Text = "Afegir player..."
    '
    'EliminarPlayerToolStripMenuItem
    '
    Me.EliminarPlayerToolStripMenuItem.Name = "EliminarPlayerToolStripMenuItem"
    Me.EliminarPlayerToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
    Me.EliminarPlayerToolStripMenuItem.Text = "Eliminar player..."
    '
    'EditarToolStripMenuItem
    '
    Me.EditarToolStripMenuItem.Name = "EditarToolStripMenuItem"
    Me.EditarToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
    Me.EditarToolStripMenuItem.Text = "Editar..."
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.CheckBoxForceSend)
    Me.GroupBox1.Controls.Add(Me.CheckBoxRunning)
    Me.GroupBox1.Controls.Add(Me.Button1)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 426)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(511, 162)
    Me.GroupBox1.TabIndex = 3
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Control"
    '
    'CheckBoxForceSend
    '
    Me.CheckBoxForceSend.AutoSize = True
    Me.CheckBoxForceSend.Location = New System.Drawing.Point(24, 95)
    Me.CheckBoxForceSend.Name = "CheckBoxForceSend"
    Me.CheckBoxForceSend.Size = New System.Drawing.Size(312, 17)
    Me.CheckBoxForceSend.TabIndex = 2
    Me.CheckBoxForceSend.Text = "Enviar valors cada 40ms encara que no hi hagi dades noves"
    Me.CheckBoxForceSend.UseVisualStyleBackColor = True
    '
    'CheckBoxRunning
    '
    Me.CheckBoxRunning.AutoSize = True
    Me.CheckBoxRunning.Location = New System.Drawing.Point(24, 72)
    Me.CheckBoxRunning.Name = "CheckBoxRunning"
    Me.CheckBoxRunning.Size = New System.Drawing.Size(66, 17)
    Me.CheckBoxRunning.TabIndex = 1
    Me.CheckBoxRunning.Text = "Running"
    Me.CheckBoxRunning.UseVisualStyleBackColor = True
    '
    'TimerForceSend
    '
    Me.TimerForceSend.Interval = 40
    '
    'FormTrackingPlayer
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(535, 600)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.ListViewPlayers)
    Me.Controls.Add(Me.ListViewSources)
    Me.Name = "FormTrackingPlayer"
    Me.Text = "FormTrackingPlayer"
    Me.ContextMenuStripSources.ResumeLayout(False)
    Me.ContextMenuStripPlayers.ResumeLayout(False)
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents ListViewSources As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeaderStudio As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeaderCAM As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeaderHost As System.Windows.Forms.ColumnHeader
  Friend WithEvents ListViewPlayers As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ContextMenuStripSources As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents AfegirFontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents EliminarFontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ContextMenuStripPlayers As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents AfegirPlayerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents EliminarPlayerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ColumnHeaderName As System.Windows.Forms.ColumnHeader
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents CheckBoxRunning As System.Windows.Forms.CheckBox
  Friend WithEvents CheckBoxForceSend As System.Windows.Forms.CheckBox
  Friend WithEvents TimerForceSend As System.Windows.Forms.Timer
  Friend WithEvents EditarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
