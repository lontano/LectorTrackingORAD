'
' Defines the Point3D class that represents points in 3D space.
' Developed by leonelmachava <leonelmachava@gmail.com>
' http://codentronix.com
'
' Copyright (c) 2011 Leonel Machava
'
Option Explicit On

Public Class Point3D
  Protected m_x As Double, m_y As Double, m_z As Double

  Public Sub New(ByVal x As Double, ByVal y As Double, ByVal z As Double)
    Me.X = x
    Me.Y = y
    Me.Z = z
  End Sub

  Public Property X() As Double
    Get
      Return m_x
    End Get
    Set(ByVal value As Double)
      m_x = value
    End Set
  End Property

  Public Property Y() As Double
    Get
      Return m_y
    End Get
    Set(ByVal value As Double)
      m_y = value
    End Set
  End Property

  Public Property Z() As Double
    Get
      Return m_z
    End Get
    Set(ByVal value As Double)
      m_z = value
    End Set
  End Property

  Public Function RotateX(ByVal angle As Double) As Point3D
    Dim rad As Double, cosa As Double, sina As Double, yn As Double, zn As Double

    rad = angle '* Math.PI / 180
    cosa = Math.Cos(rad)
    sina = Math.Sin(rad)
    yn = Me.Y * cosa - Me.Z * sina
    zn = Me.Y * sina + Me.Z * cosa
    Return New Point3D(Me.X, yn, zn)
  End Function

  Public Function RotateY(ByVal angle As Double) As Point3D
    Dim rad As Double, cosa As Double, sina As Double, Xn As Double, Zn As Double

    rad = angle ' * Math.PI / 180
    cosa = Math.Cos(rad)
    sina = Math.Sin(rad)
    Zn = Me.Z * cosa - Me.X * sina
    Xn = Me.Z * sina + Me.X * cosa

    Return New Point3D(Xn, Me.Y, Zn)
  End Function

  Public Function RotateZ(ByVal angle As Double) As Point3D
    Dim rad As Double, cosa As Double, sina As Double, Xn As Double, Yn As Double

    rad = angle '* Math.PI / 180
    cosa = Math.Cos(rad)
    sina = Math.Sin(rad)
    Xn = Me.X * cosa - Me.Y * sina
    Yn = Me.X * sina + Me.Y * cosa
    Return New Point3D(Xn, Yn, Me.Z)
  End Function

  Public Function Translate(ByVal dX As Double, ByVal dY As Double, ByVal dZ As Double) As Point3D
    Me.X += dX
    Me.Y += dY
    Me.Z += dZ
    Return New Point3D(Me.X, Me.Y, Me.Z)

  End Function

  Public Function Project(ByVal viewWidth As Double, ByVal viewHeight As Double, ByVal fov As Double, ByVal viewDistance As Double) As Point3D
    Dim factor As Double, Xn As Double, Yn As Double
    If viewDistance + Me.Z <> 0 Then
      factor = fov / (viewDistance + Me.Z)
    Else
      factor = 10000
    End If
    Xn = Me.X * factor + viewWidth / 2
    Yn = Me.Y * factor + viewHeight / 2
    Return New Point3D(Xn, Yn, Me.Z)
  End Function

  Public Enum eProjectionPlane
    XY
    XZ
    YZ
  End Enum

  Public Function Project(ByVal viewWidth As Double, ByVal viewHeight As Double, ByVal fov As Double, ByVal viewDistance As Double, ByVal eiProjectionPlane As eProjectionPlane) As Point3D
    Dim factor As Double, Xn As Double, Yn As Double
    Dim Axis1 As Double, Axis2 As Double, ProjectionAxis As Double
    Dim CRes As Point3D = Nothing
    Select Case eiProjectionPlane
      Case eProjectionPlane.XY
        Axis1 = Me.X
        Axis2 = Me.Y
        ProjectionAxis = Me.Z
      Case eProjectionPlane.XZ
        Axis1 = Me.X
        Axis2 = Me.Z
        ProjectionAxis = Me.Y
      Case eProjectionPlane.YZ
        Axis1 = Me.Y
        Axis2 = Me.Z
        ProjectionAxis = Me.X
    End Select
    If viewDistance + Z <> 0 Then
      factor = fov / (viewDistance + ProjectionAxis)
    Else
      factor = 10000
    End If
    Xn = Axis1 * factor + viewWidth / 2
    Yn = Axis2 * factor + viewHeight / 2

    Select Case eiProjectionPlane
      Case eProjectionPlane.XY
        CRes = New Point3D(Xn, Yn, ProjectionAxis)
      Case eProjectionPlane.XZ
        CRes = New Point3D(Xn, Yn, ProjectionAxis)
      Case eProjectionPlane.YZ
        CRes = New Point3D(Xn, Yn, ProjectionAxis)
    End Select
    Return CRes
  End Function
End Class