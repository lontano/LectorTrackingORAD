<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReplay
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
    Me.sendBut = New System.Windows.Forms.Button
    Me.tbSend = New System.Windows.Forms.TextBox
    Me.SuspendLayout()
    '
    'sendBut
    '
    Me.sendBut.Location = New System.Drawing.Point(21, 14)
    Me.sendBut.Name = "sendBut"
    Me.sendBut.Size = New System.Drawing.Size(130, 23)
    Me.sendBut.TabIndex = 0
    Me.sendBut.Text = "sendBut"
    Me.sendBut.UseVisualStyleBackColor = True
    '
    'tbSend
    '
    Me.tbSend.Location = New System.Drawing.Point(26, 50)
    Me.tbSend.Name = "tbSend"
    Me.tbSend.Size = New System.Drawing.Size(614, 20)
    Me.tbSend.TabIndex = 1
    '
    'frmReplay
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(652, 122)
    Me.Controls.Add(Me.tbSend)
    Me.Controls.Add(Me.sendBut)
    Me.Name = "frmReplay"
    Me.Text = "frmReplay"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents sendBut As System.Windows.Forms.Button
  Friend WithEvents tbSend As System.Windows.Forms.TextBox
End Class
