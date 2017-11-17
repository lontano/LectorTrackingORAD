Imports OpenTK
Imports OpenTK.Graphics

Class GControlHigh
  Inherits GLControl
  Private components As System.ComponentModel.IContainer
  ' 32bpp color, 24bpp z-depth, 8bpp stencil and 4x antialiasing
  ' OpenGL version is major=3, minor=0
  Public Sub New()
    MyBase.new(New GraphicsMode(32, 24, 8, 4))
  End Sub

  Private Sub InitializeComponent()
    Me.SuspendLayout()
    '
    'GControlHigh
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.Name = "GControlHigh"
    Me.ResumeLayout(False)

  End Sub
End Class