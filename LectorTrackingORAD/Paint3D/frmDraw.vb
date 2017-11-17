
'
' Simulation of a Rotating Cube using GDI+
' Developed by leonelmachava <leonelmachava@gmail.com>
' http://codentronix.com
'
' Copyright (c) 2011 Leonel Machava
'
Imports System.Drawing.Graphics
Imports System.Drawing.Pen
Imports System.Drawing.Color
Imports System.Drawing.Brush
Imports System.Drawing.Point
Imports System.Drawing.Bitmap

Public Class frmDraw
  Protected WithEvents m_timer As Timer
  Protected m_gridvertices(32) As Point3D
  Protected m_vertices(8) As Point3D
  Protected m_faces(6, 4) As Integer
  Protected m_colors(6) As Color
  Protected m_brushes(6) As Brush
  Protected m_angle As Integer

  Protected CameraMatrix(2, 3) As Double

  Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ' Enable double-buffering to eliminate flickering.
    Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
    Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

    InitCube()

    ' Create the timer.
    m_timer = New Timer()

    ' Set the timer interval to 25 milliseconds. This will give us 1000/25 ~ 40 frames per second.
    m_timer.Interval = 25

    ' Set the callback for the timer.
    AddHandler m_timer.Tick, AddressOf AnimationLoop

    ' Start the timer.
    m_timer.Start()
  End Sub

  Private Sub ComputeCameraMatrix()
    Try
      Dim C(2, 3) As Double
      Dim f As Double


    Catch ex As Exception

    End Try
  End Sub

  Private Sub InitCube()

    For nIndex As Integer = 0 To 7
      m_gridvertices(2 * nIndex) = New Point3D(1000 * nIndex, 0, -10000)
      m_gridvertices(2 * nIndex + 1) = New Point3D(1000 * nIndex, 0, 10000)
      m_gridvertices(16 + 2 * nIndex) = New Point3D(-1000 * (nIndex + 1), 0, -10000)
      m_gridvertices(16 + 2 * nIndex + 1) = New Point3D(-1000 * (nIndex + 1), 0, 10000)
    Next


    ' Create the cube vertices.
    m_vertices = New Point3D() { _
                 New Point3D(-1, 1, -1), _
                 New Point3D(1, 1, -1), _
                 New Point3D(1, -1, -1), _
                 New Point3D(-1, -1, -1), _
                 New Point3D(-1, 1, 1), _
                 New Point3D(1, 1, 1), _
                 New Point3D(1, -1, 1), _
                 New Point3D(-1, -1, 1)}

    ' Create an array representing the 6 faces of a cube. Each face is composed by indices to the vertex array
    ' above.
    m_faces = New Integer(,) {{0, 1, 2, 3}, {1, 5, 6, 2}, {5, 4, 7, 6}, {4, 0, 3, 7}, {0, 4, 5, 1}, {3, 2, 6, 7}}

    ' Define the colors of each face.
    m_colors = New Color() {Color.BlueViolet, Color.Cyan, Color.Green, Color.Yellow, Color.Violet, Color.LightSkyBlue}

    ' Create the brushes to draw each face. Brushes are used to draw filled polygons.
    For i As Integer = 0 To 5
      m_brushes(i) = New SolidBrush(m_colors(i))
    Next
  End Sub

  Private Sub AnimationLoop(ByVal sender As Object, ByVal e As System.EventArgs)
    ' Forces the Paint event to be called.
    Me.PictureBoxTarget.Invalidate()

    ' Update the variable after each frame.
    m_angle += 1
  End Sub

  Private Sub Main_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    Dim g(32) As Point3D
    Dim t(8) As Point3D
    Dim f(4) As Integer
    Dim v As Point3D
    Dim avgZ(6) As Double
    Dim order(6) As Integer
    Dim tmp As Double
    Dim iMax As Integer

    Try
      ' Clear the window
      e.Graphics.Clear(Color.LightBlue)

      ' Transform all the points and store them on the "t" array.
      InitCube()
      For i As Integer = 0 To 31
        Dim b As Brush = New SolidBrush(Color.White)
        v = m_gridvertices(i)

        g(i) = v.Translate(-Me.NumericUpDown1.Value, -Me.NumericUpDown2.Value, -Me.NumericUpDown3.Value)
        g(i) = v.RotateX(-Me.NumericUpDown4.Value).RotateY(-Me.NumericUpDown5.Value).RotateZ(-Me.NumericUpDown6.Value)
        g(i) = g(i).Project(sender.ClientSize.Width, sender.ClientSize.Height, (180 - Me.NumericUpDownFOV.Value), 1)
      Next
      For i As Integer = 0 To 7
        Dim b As Brush = New SolidBrush(Color.White)
        v = m_vertices(i)

        t(i) = v.Translate(-Me.NumericUpDown1.Value, -Me.NumericUpDown2.Value, -Me.NumericUpDown3.Value)
        t(i) = v.RotateX(-Me.NumericUpDown4.Value).RotateY(-Me.NumericUpDown5.Value).RotateZ(-Me.NumericUpDown6.Value)
        t(i) = t(i).Project(sender.ClientSize.Width, sender.ClientSize.Height, (180 - Me.NumericUpDownFOV.Value), 1)
      Next

      ' Compute the average Z value of each face.
      For i As Integer = 0 To 5
        avgZ(i) = (t(m_faces(i, 0)).Z + t(m_faces(i, 1)).Z + t(m_faces(i, 2)).Z + t(m_faces(i, 3)).Z) / 1.0
        order(i) = i
      Next

      ' Next we sort the faces in descending order based on the Z value.
      ' The objective is to draw distant faces first. This is called
      ' the PAINTERS ALGORITHM. So, the visible faces will hide the invisible ones.
      ' The sorting algorithm used is the SELECTION SORT.
      For i As Integer = 0 To 4
        iMax = i
        For j As Integer = i + 1 To 5
          If avgZ(j) > avgZ(iMax) Then
            iMax = j
          End If
        Next
        If iMax <> i Then
          tmp = avgZ(i)
          avgZ(i) = avgZ(iMax)
          avgZ(iMax) = tmp

          tmp = order(i)
          order(i) = order(iMax)
          order(iMax) = CInt(tmp)
        End If
      Next

      'draw the grid
      For i As Integer = 0 To 15
        e.Graphics.DrawLine(Pens.White, CSng(g(2 * i).X), CSng(g(2 * i).Y), CSng(g(2 * i + 1).X), CSng(g(2 * i + 1).Y))

      Next
      ' Draw the faces using the PAINTERS ALGORITHM (distant faces first, closer faces last).
      For i As Integer = 0 To 5
        Dim points() As Point
        Dim index As Integer = order(i)
        points = New Point() { _
            New Point(CInt(t(m_faces(index, 0)).X), CInt(t(m_faces(index, 0)).Y)), _
            New Point(CInt(t(m_faces(index, 1)).X), CInt(t(m_faces(index, 1)).Y)), _
            New Point(CInt(t(m_faces(index, 2)).X), CInt(t(m_faces(index, 2)).Y)), _
            New Point(CInt(t(m_faces(index, 3)).X), CInt(t(m_faces(index, 3)).Y))}
        e.Graphics.FillPolygon(m_brushes(index), points)
      Next
    Catch ex As Exception

    End Try
    
  End Sub

  Private Sub PictureBoxTarget_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBoxTarget.Paint
    Main_Paint(sender, e)
  End Sub
End Class