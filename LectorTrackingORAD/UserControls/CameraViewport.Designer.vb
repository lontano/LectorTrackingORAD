<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CameraViewport
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
    Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.ViewportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.TopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.LeftToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.FrontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.ScaleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
    Me.TrackBarScale = New System.Windows.Forms.TrackBar
    Me.ContextMenuStrip1.SuspendLayout()
    CType(Me.TrackBarScale, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ContextMenuStrip1
    '
    Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewportToolStripMenuItem, Me.ScaleToolStripMenuItem})
    Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
    Me.ContextMenuStrip1.Size = New System.Drawing.Size(122, 48)
    '
    'ViewportToolStripMenuItem
    '
    Me.ViewportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TopToolStripMenuItem, Me.LeftToolStripMenuItem, Me.FrontToolStripMenuItem})
    Me.ViewportToolStripMenuItem.Name = "ViewportToolStripMenuItem"
    Me.ViewportToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
    Me.ViewportToolStripMenuItem.Text = "Viewport"
    '
    'TopToolStripMenuItem
    '
    Me.TopToolStripMenuItem.Name = "TopToolStripMenuItem"
    Me.TopToolStripMenuItem.Size = New System.Drawing.Size(102, 22)
    Me.TopToolStripMenuItem.Text = "Top"
    '
    'LeftToolStripMenuItem
    '
    Me.LeftToolStripMenuItem.Name = "LeftToolStripMenuItem"
    Me.LeftToolStripMenuItem.Size = New System.Drawing.Size(102, 22)
    Me.LeftToolStripMenuItem.Text = "Left"
    '
    'FrontToolStripMenuItem
    '
    Me.FrontToolStripMenuItem.Name = "FrontToolStripMenuItem"
    Me.FrontToolStripMenuItem.Size = New System.Drawing.Size(102, 22)
    Me.FrontToolStripMenuItem.Text = "Front"
    '
    'ScaleToolStripMenuItem
    '
    Me.ScaleToolStripMenuItem.Name = "ScaleToolStripMenuItem"
    Me.ScaleToolStripMenuItem.Size = New System.Drawing.Size(121, 22)
    Me.ScaleToolStripMenuItem.Text = "Scale"
    '
    'TrackBarScale
    '
    Me.TrackBarScale.AutoSize = False
    Me.TrackBarScale.Dock = System.Windows.Forms.DockStyle.Top
    Me.TrackBarScale.LargeChange = 50
    Me.TrackBarScale.Location = New System.Drawing.Point(0, 0)
    Me.TrackBarScale.Maximum = 1000
    Me.TrackBarScale.Name = "TrackBarScale"
    Me.TrackBarScale.Size = New System.Drawing.Size(609, 32)
    Me.TrackBarScale.SmallChange = 10
    Me.TrackBarScale.TabIndex = 1
    Me.TrackBarScale.TickFrequency = 10
    Me.TrackBarScale.Visible = False
    '
    'CameraViewport
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ContextMenuStrip = Me.ContextMenuStrip1
    Me.Controls.Add(Me.TrackBarScale)
    Me.Name = "CameraViewport"
    Me.Size = New System.Drawing.Size(609, 150)
    Me.ContextMenuStrip1.ResumeLayout(False)
    CType(Me.TrackBarScale, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents ViewportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents TopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents LeftToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents FrontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ScaleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents TrackBarScale As System.Windows.Forms.TrackBar

End Class
