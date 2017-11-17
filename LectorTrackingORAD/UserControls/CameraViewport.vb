
Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports System
Imports System.Drawing

Public Class CameraViewport
  Private loaded As Boolean = False
  Public Enum eViewport
    Top
    Front
    Left
  End Enum
  Private ePiViewPort As eViewport
  Private LlistaSources As List(Of TrackingSource)
  Private LlistaCameraBitmaps As New List(Of Bitmap)
  Private MyColors() As Color
  Private MyBrushes() As Brush
  Private MyPens() As Pen

  Private Const MAX_COLOR_SHADES As Integer = 2

  Private DrawingScale As Single = 20

  Private DrawBitmap As Bitmap = Nothing
  Private LastBitmapms As Double = 0

  Private WithEvents CPiTimer As New Timer
  Private dPiRefreshRate As Double = 250



  Public Property RefreshRate() As Double
    Get
      Return dPiRefreshRate
    End Get
    Set(ByVal value As Double)
      dPiRefreshRate = value
    End Set
  End Property

  Public Property ViewPort() As eViewport
    Get
      Return Me.ePiViewPort
    End Get
    Set(ByVal value As eViewport)
      If Not Me.ePiViewPort = value Then
        Me.ePiViewPort = value
        Me.Invalidate()
      End If
    End Set
  End Property

  Public Property Sources() As List(Of TrackingSource)
    Get
      Return Me.LlistaSources

    End Get
    Set(ByVal value As List(Of TrackingSource))
      Me.LlistaSources = value
      GenerateCameraBitmaps()
      Me.UpdateDisplay()
    End Set
  End Property



  Private Sub GenerateCameraBitmaps()
    Try
      Dim bmp As Bitmap
      Dim graph As Graphics
      Dim nColor As Integer
      Me.LlistaCameraBitmaps.Clear()
      If Not Me.LlistaSources Is Nothing Then
        For nIndex As Integer = 0 To Me.LlistaSources.Count - 1
          nColor = nIndex Mod (MAX_COLOR_SHADES * MAX_COLOR_SHADES * MAX_COLOR_SHADES)

          With Me.LlistaSources(nIndex)

            bmp = New Bitmap(2 * Me.ClientSize.Width, 10)
            graph = Graphics.FromImage(bmp)

            graph.FillRectangle(MyBrushes(nColor), New Rectangle(0, 0, 20, 10))
            graph.DrawLine(MyPens(nColor), 0, 5, bmp.Width, 5)


          End With

          graph.Dispose()
          Me.LlistaCameraBitmaps.Add(bmp)
        Next
      End If
    Catch ex As Exception

    End Try
  End Sub

  Private Sub GenerateBitmap(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    Dim StartTime As Integer = System.Environment.TickCount
    Try
      Static graph As Graphics
      If Me.DrawBitmap Is Nothing Then
        Me.DrawBitmap = New Bitmap(Me.ClientSize.Width, Me.ClientSize.Height)
        graph = Graphics.FromImage(Me.DrawBitmap)
      Else
        If Me.ClientSize.Width <> Me.DrawBitmap.Width Or Me.ClientSize.Height <> Me.DrawBitmap.Height Then
          graph.Dispose()
          Me.DrawBitmap.Dispose()
          Me.DrawBitmap = New Bitmap(Me.ClientSize.Width, Me.ClientSize.Height)
          graph = Graphics.FromImage(Me.DrawBitmap)
        End If
      End If

      graph.Clear(Me.BackColor)
      'pintar el títol
      graph.DrawString(Me.ePiViewPort.ToString, Me.Font, Brushes.Red, 0, 0)

      'pintar l'eix
      graph.DrawLine(Pens.Black, CInt(Me.ClientSize.Width / 2), 0, CInt(Me.ClientSize.Width / 2), CInt(Me.ClientSize.Height))
      graph.DrawLine(Pens.Black, 0, CInt(Me.ClientSize.Height / 2), CInt(Me.ClientSize.Width), CInt(Me.ClientSize.Height / 2))

      Dim CCenter As New Point3D(0, 0, 0)
      Dim CLookAt As New PointF
      Dim CDirection As New PointF
      Dim fAux As Single

      Dim fCameraSize As PointF = New PointF(50, 20)
      Dim fLookAtSize As Single = 5
      Dim CCameraBody(3) As PointF
      Dim CCameraDirection(3) As PointF
      Dim fAngle As Single
      Dim nColor As Integer

      Dim fWidth As Single = Me.ClientSize.Width
      Dim fHeight As Single = Me.ClientSize.Height

      If Not Me.LlistaSources Is Nothing Then
        For nIndex As Integer = 0 To Me.LlistaSources.Count - 1
          nColor = nIndex Mod (MAX_COLOR_SHADES * MAX_COLOR_SHADES * MAX_COLOR_SHADES)
          With Me.LlistaSources(nIndex)

            graph.DrawString(.Host & " " & .LastTrackingValue.Frame, Me.Font, MyBrushes(nColor), 0, 15 * (nIndex + 1))

            CCenter.X = .LastTrackingValue.POS_X
            CCenter.Y = .LastTrackingValue.POS_Y
            CCenter.Z = .LastTrackingValue.POS_Z

            'DrawCube(sender, e, CCenter)
            If .LastTrackingValue.Valid Then
              Select Case Me.ePiViewPort
                Case eViewport.Front
                  CCenter.X = .LastTrackingValue.POS_Y
                  CCenter.Y = .LastTrackingValue.POS_X
                  CLookAt.X = .LastTrackingValue.TARGET_Y
                  CLookAt.Y = .LastTrackingValue.TARGET_X

                Case eViewport.Left
                  CCenter.X = .LastTrackingValue.POS_Z
                  CCenter.Y = .LastTrackingValue.POS_Y
                  CLookAt.X = .LastTrackingValue.TARGET_Z
                  CLookAt.Y = .LastTrackingValue.TARGET_Y
                Case eViewport.Top
                  CCenter.X = .LastTrackingValue.POS_X
                  CCenter.Y = .LastTrackingValue.POS_Z
                  CLookAt.X = .LastTrackingValue.TARGET_X
                  CLookAt.Y = .LastTrackingValue.TARGET_Z
              End Select
            Else
              CCenter.X = 0
              CCenter.Y = 0
              CLookAt.X = 1
              CLookAt.Y = 0
            End If

            CCenter.X = CCenter.X * Me.DrawingScale + Me.ClientSize.Width / 2
            CCenter.Y = CCenter.Y * Me.DrawingScale + Me.ClientSize.Height / 2
            CLookAt.X = CSng(CLookAt.X * Me.DrawingScale + Me.ClientSize.Width / 2)
            CLookAt.Y = CSng(CLookAt.Y * Me.DrawingScale + Me.ClientSize.Height / 2)

            CDirection.X = CSng(CLookAt.X - CCenter.X)
            CDirection.Y = CSng(CLookAt.Y - CCenter.Y)
            fAux = CSng(Math.Sqrt(Math.Pow(CDirection.X, 2) + Math.Pow(CDirection.Y, 2)))
            fAngle = CSng(Math.Atan2(CDirection.Y, CDirection.X))
            CDirection.X /= fAux
            CDirection.Y /= fAux

            'rotate:
            fAngle = CSng(Math.Atan2(CDirection.Y, CDirection.X))

            fAngle = fAngle * 180 / 3.14159
            graph.TranslateTransform(CCenter.X, CCenter.Y)
            graph.RotateTransform(fAngle)
            graph.DrawImage(Me.LlistaCameraBitmaps(nIndex), New PointF(-10, -5))
            graph.RotateTransform(-fAngle)
            graph.TranslateTransform(-CCenter.X, -CCenter.Y)
          End With
        Next
      End If
    Catch ex As Exception

    End Try
  End Sub

  Private RefreshMe As Boolean = True

  Public Sub UpdateDisplay()
    RefreshMe = True
  End Sub


  Private Sub CameraViewport_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    Try
      If Me.DrawBitmap Is Nothing Then
        Me.LastBitmapms = 0
      ElseIf Me.DrawBitmap.Width <> Me.ClientSize.Width Or Me.DrawBitmap.Height <> Me.ClientSize.Height Then
        Me.LastBitmapms = 0
      End If
      If Math.Abs(System.Environment.TickCount - Me.LastBitmapms) > Me.dPiRefreshRate Then
        Me.LastBitmapms = System.Environment.TickCount
        GenerateBitmap(sender, e)
      End If
      e.Graphics.DrawImage(Me.DrawBitmap, New PointF(0, 0))
    Catch ex As Exception
    End Try
  End Sub

  Public Sub New()

    ' Llamada necesaria para el Diseñador de Windows Forms.
    InitializeComponent()

    ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    SetStyle(ControlStyles.DoubleBuffer, True)
    SetStyle(ControlStyles.UserPaint, True)
    SetStyle(ControlStyles.AllPaintingInWmPaint, True)

    InitCube()

    ReDim MyColors(MAX_COLOR_SHADES * MAX_COLOR_SHADES * MAX_COLOR_SHADES)
    ReDim MyBrushes(MAX_COLOR_SHADES * MAX_COLOR_SHADES * MAX_COLOR_SHADES)
    ReDim MyPens(MAX_COLOR_SHADES * MAX_COLOR_SHADES * MAX_COLOR_SHADES)
    Dim nIndex As Integer
    For nR As Integer = 0 To MAX_COLOR_SHADES - 1
      For nG As Integer = 0 To MAX_COLOR_SHADES - 1
        For nB As Integer = 0 To MAX_COLOR_SHADES - 1
          nIndex = nR * MAX_COLOR_SHADES * MAX_COLOR_SHADES + nG * MAX_COLOR_SHADES + nB
          MyColors(nIndex) = Color.FromArgb(CInt(Rnd() * 200), CInt(Rnd() * 200), CInt(Rnd() * 200))
          MyBrushes(nIndex) = New SolidBrush(MyColors(nIndex))
          MyPens(nIndex) = New Pen(MyColors(nIndex))
        Next
      Next
    Next

    CPiTimer.Interval = 100
    CPiTimer.Enabled = True

  End Sub

  Private Sub TopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TopToolStripMenuItem.Click
    Me.ViewPort = eViewport.Top
  End Sub

  Private Sub LeftToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeftToolStripMenuItem.Click
    Me.ViewPort = eViewport.Left
  End Sub

  Private Sub FrontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FrontToolStripMenuItem.Click
    Me.ViewPort = eViewport.Front
  End Sub

  Private Sub TrackBarScale_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TrackBarScale.MouseUp

    Me.TrackBarScale.Visible = False
  End Sub

  Private Sub TrackBarScale_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBarScale.Scroll
    Try
      Me.DrawingScale = Me.TrackBarScale.Value
      Me.LastBitmapms = 0
      Me.Invalidate()
    Catch ex As Exception

    End Try
  End Sub

  Private Sub ScaleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScaleToolStripMenuItem.Click
    Try
      Me.TrackBarScale.Value = CInt(Me.DrawingScale)
      Me.TrackBarScale.Visible = True
    Catch ex As Exception

    End Try
  End Sub

  Protected m_pen As Pen
  Protected m_timer As Timer
  Protected m_vertices(8) As Point3D
  Protected m_faces(6, 4) As Integer
  Protected m_angle As Integer


  Private Sub InitCube()
    ' Create an array with 8 points.
    m_pen = Pens.Blue
    Dim sizeX As Double = 0.5
    Dim sizey As Double = 1
    Dim sizeZ As Double = 0.5
    m_vertices = New Point3D() { _
                 New Point3D(-sizeX, sizey, -sizeZ), _
                 New Point3D(sizeX, sizey, -sizeZ), _
                 New Point3D(sizeX, -sizey, -sizeZ), _
                 New Point3D(-sizeX, -sizey, -sizeZ), _
                 New Point3D(-sizeX, sizey, sizeZ), _
                 New Point3D(sizeX, sizey, sizeZ), _
                 New Point3D(sizeX, -sizey, sizeZ), _
                 New Point3D(-sizeX, -sizey, sizeZ)}

    ' Create an array representing the 6 faces of a cube. Each face is composed by indices to the vertex array
    ' above.
    m_faces = New Integer(,) {{0, 1, 2, 3}, {1, 5, 6, 2}, {5, 4, 7, 6}, {4, 0, 3, 7}, {0, 4, 5, 1}, {3, 2, 6, 7}}
  End Sub


  Private Sub CPiTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles CPiTimer.Tick
    If Math.Abs(System.Environment.TickCount - Me.LastBitmapms) > Me.RefreshRate Then
      Me.LastBitmapms = 0
      Me.Invalidate()
    End If
  End Sub

  Private Sub CameraViewport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    loaded = True

  End Sub


End Class
