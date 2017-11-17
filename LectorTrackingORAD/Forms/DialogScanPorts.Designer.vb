<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogScanPorts
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
    Me.ListView1 = New System.Windows.Forms.ListView
    Me.ColumnHeaderHost = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeaderPort = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeaderPackets = New System.Windows.Forms.ColumnHeader
    Me.ColumnHeaderIP = New System.Windows.Forms.ColumnHeader
    Me.SuspendLayout()
    '
    'ListView1
    '
    Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderHost, Me.ColumnHeaderIP, Me.ColumnHeaderPort, Me.ColumnHeaderPackets})
    Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ListView1.Location = New System.Drawing.Point(0, 0)
    Me.ListView1.Name = "ListView1"
    Me.ListView1.Size = New System.Drawing.Size(459, 433)
    Me.ListView1.TabIndex = 1
    Me.ListView1.UseCompatibleStateImageBehavior = False
    Me.ListView1.View = System.Windows.Forms.View.Details
    '
    'ColumnHeaderHost
    '
    Me.ColumnHeaderHost.Text = "Host"
    Me.ColumnHeaderHost.Width = 200
    '
    'ColumnHeaderPort
    '
    Me.ColumnHeaderPort.Text = "Port"
    '
    'ColumnHeaderPackets
    '
    Me.ColumnHeaderPackets.Text = "Packets"
    '
    'ColumnHeaderIP
    '
    Me.ColumnHeaderIP.Text = "IP"
    Me.ColumnHeaderIP.Width = 100
    '
    'DialogScanPorts
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(459, 433)
    Me.Controls.Add(Me.ListView1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "DialogScanPorts"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "DialogScanPorts"
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ListView1 As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeaderHost As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeaderPort As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeaderPackets As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeaderIP As System.Windows.Forms.ColumnHeader

End Class
