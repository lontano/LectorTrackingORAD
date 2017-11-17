<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDraw
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
    Me.PictureBoxTarget = New System.Windows.Forms.PictureBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown
    Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown
    Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown
    Me.NumericUpDown4 = New System.Windows.Forms.NumericUpDown
    Me.NumericUpDown5 = New System.Windows.Forms.NumericUpDown
    Me.NumericUpDown6 = New System.Windows.Forms.NumericUpDown
    Me.Label2 = New System.Windows.Forms.Label
    Me.NumericUpDownFOV = New System.Windows.Forms.NumericUpDown
    Me.Label3 = New System.Windows.Forms.Label
    CType(Me.PictureBoxTarget, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDown6, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDownFOV, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'PictureBoxTarget
    '
    Me.PictureBoxTarget.Location = New System.Drawing.Point(12, 12)
    Me.PictureBoxTarget.Name = "PictureBoxTarget"
    Me.PictureBoxTarget.Size = New System.Drawing.Size(410, 252)
    Me.PictureBoxTarget.TabIndex = 0
    Me.PictureBoxTarget.TabStop = False
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(64, 284)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(52, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Posicions"
    '
    'NumericUpDown1
    '
    Me.NumericUpDown1.DecimalPlaces = 4
    Me.NumericUpDown1.Location = New System.Drawing.Point(67, 300)
    Me.NumericUpDown1.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
    Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1000, 0, 0, -2147483648})
    Me.NumericUpDown1.Name = "NumericUpDown1"
    Me.NumericUpDown1.Size = New System.Drawing.Size(151, 20)
    Me.NumericUpDown1.TabIndex = 2
    '
    'NumericUpDown2
    '
    Me.NumericUpDown2.DecimalPlaces = 4
    Me.NumericUpDown2.Location = New System.Drawing.Point(67, 326)
    Me.NumericUpDown2.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
    Me.NumericUpDown2.Minimum = New Decimal(New Integer() {1000, 0, 0, -2147483648})
    Me.NumericUpDown2.Name = "NumericUpDown2"
    Me.NumericUpDown2.Size = New System.Drawing.Size(151, 20)
    Me.NumericUpDown2.TabIndex = 3
    '
    'NumericUpDown3
    '
    Me.NumericUpDown3.DecimalPlaces = 4
    Me.NumericUpDown3.Location = New System.Drawing.Point(67, 352)
    Me.NumericUpDown3.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
    Me.NumericUpDown3.Minimum = New Decimal(New Integer() {1000, 0, 0, -2147483648})
    Me.NumericUpDown3.Name = "NumericUpDown3"
    Me.NumericUpDown3.Size = New System.Drawing.Size(151, 20)
    Me.NumericUpDown3.TabIndex = 4
    Me.NumericUpDown3.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'NumericUpDown4
    '
    Me.NumericUpDown4.DecimalPlaces = 4
    Me.NumericUpDown4.Location = New System.Drawing.Point(224, 352)
    Me.NumericUpDown4.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
    Me.NumericUpDown4.Minimum = New Decimal(New Integer() {1000, 0, 0, -2147483648})
    Me.NumericUpDown4.Name = "NumericUpDown4"
    Me.NumericUpDown4.Size = New System.Drawing.Size(151, 20)
    Me.NumericUpDown4.TabIndex = 8
    '
    'NumericUpDown5
    '
    Me.NumericUpDown5.DecimalPlaces = 4
    Me.NumericUpDown5.Location = New System.Drawing.Point(224, 326)
    Me.NumericUpDown5.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
    Me.NumericUpDown5.Minimum = New Decimal(New Integer() {1000, 0, 0, -2147483648})
    Me.NumericUpDown5.Name = "NumericUpDown5"
    Me.NumericUpDown5.Size = New System.Drawing.Size(151, 20)
    Me.NumericUpDown5.TabIndex = 7
    '
    'NumericUpDown6
    '
    Me.NumericUpDown6.DecimalPlaces = 4
    Me.NumericUpDown6.Location = New System.Drawing.Point(224, 300)
    Me.NumericUpDown6.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
    Me.NumericUpDown6.Minimum = New Decimal(New Integer() {1000, 0, 0, -2147483648})
    Me.NumericUpDown6.Name = "NumericUpDown6"
    Me.NumericUpDown6.Size = New System.Drawing.Size(151, 20)
    Me.NumericUpDown6.TabIndex = 6
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(221, 284)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(55, 13)
    Me.Label2.TabIndex = 5
    Me.Label2.Text = "Rotacions"
    '
    'NumericUpDownFOV
    '
    Me.NumericUpDownFOV.DecimalPlaces = 4
    Me.NumericUpDownFOV.Location = New System.Drawing.Point(98, 378)
    Me.NumericUpDownFOV.Maximum = New Decimal(New Integer() {179, 0, 0, 0})
    Me.NumericUpDownFOV.Minimum = New Decimal(New Integer() {1, 0, 0, 196608})
    Me.NumericUpDownFOV.Name = "NumericUpDownFOV"
    Me.NumericUpDownFOV.Size = New System.Drawing.Size(120, 20)
    Me.NumericUpDownFOV.TabIndex = 10
    Me.NumericUpDownFOV.Value = New Decimal(New Integer() {20, 0, 0, 0})
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(64, 380)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(28, 13)
    Me.Label3.TabIndex = 9
    Me.Label3.Text = "FOV"
    '
    'frmDraw
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(812, 513)
    Me.Controls.Add(Me.NumericUpDownFOV)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.NumericUpDown4)
    Me.Controls.Add(Me.NumericUpDown5)
    Me.Controls.Add(Me.NumericUpDown6)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.NumericUpDown3)
    Me.Controls.Add(Me.NumericUpDown2)
    Me.Controls.Add(Me.NumericUpDown1)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.PictureBoxTarget)
    Me.Name = "frmDraw"
    Me.Text = "frmDraw"
    CType(Me.PictureBoxTarget, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDown6, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDownFOV, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents PictureBoxTarget As System.Windows.Forms.PictureBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
  Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
  Friend WithEvents NumericUpDown3 As System.Windows.Forms.NumericUpDown
  Friend WithEvents NumericUpDown4 As System.Windows.Forms.NumericUpDown
  Friend WithEvents NumericUpDown5 As System.Windows.Forms.NumericUpDown
  Friend WithEvents NumericUpDown6 As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents NumericUpDownFOV As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
