<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormExportProgress
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
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
    Me.ProgressBar2 = New System.Windows.Forms.ProgressBar
    Me.SuspendLayout()
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Location = New System.Drawing.Point(12, 12)
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(529, 19)
    Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
    Me.ProgressBar1.TabIndex = 0
    Me.ProgressBar1.Value = 50
    '
    'ProgressBar2
    '
    Me.ProgressBar2.Location = New System.Drawing.Point(12, 37)
    Me.ProgressBar2.Name = "ProgressBar2"
    Me.ProgressBar2.Size = New System.Drawing.Size(529, 19)
    Me.ProgressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous
    Me.ProgressBar2.TabIndex = 1
    Me.ProgressBar2.Value = 50
    '
    'FormExportProgress
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(553, 63)
    Me.ControlBox = False
    Me.Controls.Add(Me.ProgressBar2)
    Me.Controls.Add(Me.ProgressBar1)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FormExportProgress"
    Me.Text = "FormExportProgress"
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents ProgressBar2 As System.Windows.Forms.ProgressBar
End Class
