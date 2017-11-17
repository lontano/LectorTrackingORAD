<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPackets
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPackets))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.C1FlexGridPorts = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.C1FlexGridPackets = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.ContextMenuStripPackets = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EnviarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.TimerUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.C1FlexGridPorts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1FlexGridPackets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStripPackets.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.C1FlexGridPorts)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.C1FlexGridPackets)
        Me.SplitContainer1.Size = New System.Drawing.Size(1124, 588)
        Me.SplitContainer1.SplitterDistance = 372
        Me.SplitContainer1.TabIndex = 0
        '
        'C1FlexGridPorts
        '
        Me.C1FlexGridPorts.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.C1FlexGridPorts.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.C1FlexGridPorts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1FlexGridPorts.Location = New System.Drawing.Point(0, 0)
        Me.C1FlexGridPorts.Name = "C1FlexGridPorts"
        Me.C1FlexGridPorts.Size = New System.Drawing.Size(372, 588)
        Me.C1FlexGridPorts.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("C1FlexGridPorts.Styles"))
        Me.C1FlexGridPorts.TabIndex = 9
        '
        'C1FlexGridPackets
        '
        Me.C1FlexGridPackets.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.C1FlexGridPackets.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.C1FlexGridPackets.ContextMenuStrip = Me.ContextMenuStripPackets
        Me.C1FlexGridPackets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1FlexGridPackets.Location = New System.Drawing.Point(0, 0)
        Me.C1FlexGridPackets.Name = "C1FlexGridPackets"
        Me.C1FlexGridPackets.Size = New System.Drawing.Size(748, 588)
        Me.C1FlexGridPackets.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("C1FlexGridPackets.Styles"))
        Me.C1FlexGridPackets.TabIndex = 9
        '
        'ContextMenuStripPackets
        '
        Me.ContextMenuStripPackets.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EnviarToolStripMenuItem})
        Me.ContextMenuStripPackets.Name = "ContextMenuStripPackets"
        Me.ContextMenuStripPackets.Size = New System.Drawing.Size(107, 26)
        '
        'EnviarToolStripMenuItem
        '
        Me.EnviarToolStripMenuItem.Name = "EnviarToolStripMenuItem"
        Me.EnviarToolStripMenuItem.Size = New System.Drawing.Size(106, 22)
        Me.EnviarToolStripMenuItem.Text = "Enviar"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1124, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'TimerUpdate
        '
        Me.TimerUpdate.Enabled = True
        '
        'frmPackets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1124, 612)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmPackets"
        Me.Text = "frmPackets"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.C1FlexGridPorts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1FlexGridPackets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStripPackets.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
  Friend WithEvents C1FlexGridPorts As C1.Win.C1FlexGrid.C1FlexGrid
  Friend WithEvents C1FlexGridPackets As C1.Win.C1FlexGrid.C1FlexGrid
  Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
  Friend WithEvents ContextMenuStripPackets As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents EnviarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimerUpdate As Timer
End Class
