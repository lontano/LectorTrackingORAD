Imports OpenTK
Imports OpenTK.Graphics.OpenGL


Public Class Sphere

  Public Sub DrawSphere(ByVal Center As Vector3, ByVal Radius As Single, ByVal Precision As UInteger)
    If Radius < 0.0F Then
      Radius = -Radius
    End If
    If Radius = 0.0F Then
      Throw New DivideByZeroException("DrawSphere: Radius cannot be 0f.")
    End If
    If Precision = 0 Then
      Throw New DivideByZeroException("DrawSphere: Precision of 8 or greater is required.")
    End If

    Const HalfPI As Single = CSng(System.Math.PI * 0.5)
    Dim OneThroughPrecision As Single = 1.0F / Precision
    Dim TwoPIThroughPrecision As Single = CSng(System.Math.PI * 2.0 * OneThroughPrecision)

    Dim theta1 As Single, theta2 As Single, theta3 As Single
    Dim Normal As Vector3, Position As Vector3

    For j As UInteger = 0 To Precision \ 2 - 1
      theta1 = (j * TwoPIThroughPrecision) - HalfPI
      theta2 = ((j + 1) * TwoPIThroughPrecision) - HalfPI

      GL.Begin(BeginMode.TriangleStrip)
      For i As UInteger = 0 To Precision
        theta3 = i * TwoPIThroughPrecision

        Normal.X = CSng(System.Math.Cos(theta2) * System.Math.Cos(theta3))
        Normal.Y = CSng(System.Math.Sin(theta2))
        Normal.Z = CSng(System.Math.Cos(theta2) * System.Math.Sin(theta3))
        Position.X = Center.X + Radius * Normal.X
        Position.Y = Center.Y + Radius * Normal.Y
        Position.Z = Center.Z + Radius * Normal.Z

        GL.Normal3(Normal)
        GL.TexCoord2(i * OneThroughPrecision, 2.0F * (j + 1) * OneThroughPrecision)
        GL.Vertex3(Position)

        Normal.X = CSng(System.Math.Cos(theta1) * System.Math.Cos(theta3))
        Normal.Y = CSng(System.Math.Sin(theta1))
        Normal.Z = CSng(System.Math.Cos(theta1) * System.Math.Sin(theta3))
        Position.X = Center.X + Radius * Normal.X
        Position.Y = Center.Y + Radius * Normal.Y
        Position.Z = Center.Z + Radius * Normal.Z

        GL.Normal3(Normal)
        GL.TexCoord2(i * OneThroughPrecision, 2.0F * j * OneThroughPrecision)
        GL.Vertex3(Position)
      Next
      GL.[End]()
    Next
  End Sub

End Class
