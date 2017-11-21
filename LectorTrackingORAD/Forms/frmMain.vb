
Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports System.Drawing


Public Class frmMain
  Public WithEvents CPuUDPSniffer As UDPSniffer
  Private WithEvents IPiCameraExporter As IExporter

  Private frmPiProgress As FormExportProgress

  Private WithEvents _sniffer As IpComm.Sniffer

  Public WithEvents CPuTrackingPlayer As ATrackingPlayer

  Public WithEvents frmPiPackets As frmPackets

  Private Enum eType As Integer
    File
    Sniffer
  End Enum

  Private ePiType As eType = eType.File


#Region "Value display"
  Private Enum eValueCols
    Selected
    Studio
    Cam
    Protocol
    Host
    Port
    Count
    FPS
    Total
  End Enum

  Private _busy As Boolean = False
  Private Sub MostrarValors(ByVal biRedraw As Boolean)
    Try
      If _busy Then Exit Sub
      Debug.Print(Now.ToString & "." & Strings.Format(Now.Millisecond, "000") & "  MostrarValors biRedraw = " & biRedraw)
      Dim sw As New Stopwatch
      sw.Start()
      _busy = True
      Dim nRows As Integer
      Dim nMaxValues As Integer = 0
      Dim CAux As TrackingSource
      With Me.C1FlexGridPorts
        nRows = .Rows.Count
        .Rows.Count = gTrackingFile.TrackingSources.Count + CPiTrackingSourceManager.TrackingSources.Count + 1
        .Cols.Count = eValueCols.Total
        .Rows.Fixed = 1
        .Cols.Fixed = 0
        .Cols(0).DataType = GetType(Boolean)

        Dim nRow As Integer = .Rows.Fixed

        For Each CAux In CPiTrackingSourceManager.TrackingSources
          .SetData(nRow, eValueCols.Port, CAux.Port)
          .SetData(nRow, eValueCols.Host, CAux.Host)
          .SetData(nRow, eValueCols.Selected, CAux.Selected)
          .SetData(nRow, eValueCols.Count, CAux.TrackingValues.Count)
          .SetData(nRow, eValueCols.FPS, CAux.ValuesPerSecond)
          If Not CAux.TrackingHost Is Nothing Then
            .SetData(nRow, eValueCols.Studio, CAux.TrackingHost.Studio)
            .SetData(nRow, eValueCols.Cam, CAux.TrackingHost.CamNumber)
            .SetData(nRow, eValueCols.Protocol, CAux.TrackingHost.TrackingType.ToString)
          End If
          nRow += 1
        Next
        For Each CAux In gTrackingFile.TrackingSources
          .SetData(nRow, eValueCols.Port, CAux.Port)
          .SetData(nRow, eValueCols.Host, CAux.Host)
          .SetData(nRow, eValueCols.Selected, CAux.Selected)
          .SetData(nRow, eValueCols.Count, CAux.TrackingValues.Count)
          If Not CAux.TrackingHost Is Nothing Then
            .SetData(nRow, eValueCols.Studio, CAux.TrackingHost.Studio)
            .SetData(nRow, eValueCols.Cam, CAux.TrackingHost.CamNumber)
            .SetData(nRow, eValueCols.Protocol, CAux.TrackingHost.TrackingType.ToString)
          End If
          nRow += 1
        Next
        If nRows <> .Rows.Count Then
          .ExtendLastCol = True
          .AutoSizeCols(0, .Cols.Count - 1, 5)
        End If
      End With

      Dim sAux As String = Me.ComboBoxTrackingPlayer.Text

      Me.ComboBoxTrackingPlayer.Items.Clear()
      For Each CAux In gTrackingFile.TrackingSources
        Me.ComboBoxTrackingPlayer.Items.Add(CAux.Host & " " & CAux.Port)
      Next

      If GràficaEnTempsRealToolStripMenuItem.Checked Then
        Me.OnRenderFrame()
      End If

      If Not Me.frmPiPackets Is Nothing Then
        Me.frmPiPackets.MostrarPackets()
      End If

      sw.Stop()
      Debug.Print("     Done in " & sw.ElapsedMilliseconds & "ms")
    Catch ex As Exception

    End Try
    _busy = False
  End Sub

  Private Sub C1FlexGridPorts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1FlexGridPorts.Click
    Try
      With Me.C1FlexGridPorts
        Dim CAux As TrackingSource
        If .Row >= .Rows.Fixed And .Col = 0 Then
          CAux = gTrackingFile.TrackingSourceByPort(CInt(.GetData(.Row, eValueCols.Port)))
          If Not CAux Is Nothing Then
            CAux.Selected = Not CAux.Selected
            gTrackingFile.ToggleSelection(CAux.Port, CAux.Selected)

            MostrarValors(True)
          End If
        End If
      End With
    Catch ex As Exception

    End Try
  End Sub
#End Region

#Region "Sniffer"

  Private Sub ButtonListen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonListen.Click
    Try
      If _sniffer Is Nothing Then
        _sniffer = New IpComm.Sniffer
        If _sniffer.LlistaInterfaces.Count = 0 Then
          MsgBox("Cap interface de xarxa per escoltar")
          _sniffer = Nothing
        Else

          Dim dlg As New DialogInterfaces
          dlg.LlistaInterfaces = _sniffer.LlistaInterfaces
          dlg.SelectedInterface = gudtCnfg.SelectedInterface
          dlg.ShowDialog(Me)
          If dlg.DialogResult = DialogResult.OK Then
            gudtCnfg.SelectedInterface = dlg.SelectedInterface
            _sniffer.ShowPort = 0
            _sniffer.ShowProtocol = IpComm.Protocol.UDP
            _sniffer._Parent = Me.PanelCapture
            _sniffer.StartStop()
          Else
            _sniffer = Nothing
          End If
        End If
        Me.InitManager()
        frmPiSniffer_ListenState(True)
        Me.CPiTrackingSourceManager.Enabled = True
      Else
        Me.StopManager()
        'CPiSniffer.StartStop()
        frmPiSniffer_ListenState(False)
        Me.CPiTrackingSourceManager.Enabled = False
        _sniffer = Nothing
      End If

      'CPuUDPSniffer = New UDPSniffer(Me.NumericUpDownPort.Value)
    Catch ex As Exception

    End Try
  End Sub

  Private Sub frmPiSniffer_DataArrivalUDP(ByVal data As IpComm.UDPHeader) Handles _sniffer.DataArrivalUDP
    Try
      Dim CValor As New TrackingValue(0)
      Dim bRes As Boolean

      If data.Data.Length > 100 Then
        bRes = CValor.FromBuffer(data.Data, eTrackingType.UDP_ORAD)
        If bRes Then
          CValor.PORT = CInt(data.DestinationPort)
          CValor.PORT = CInt(data.SourcePort)
          CValor.HOST = data.Source.ToString

          If gTrackingFile Is Nothing Then gTrackingFile = New TrackingFile("", False)
          If CValor.Valid Then
            gTrackingFile.AddTrackingValue(CValor)
            'Me.UpdateTrackBar(CValor)

            'gTrackingFile.TrackingValues.Add(CValor)
            MostrarValors(False)
          Else
            gTrackingFile.AddTrackingValue(CValor)
            'Debug.Print(CValor.MessageBytes.ToString)
            'Debug.Print(CValor.MessageBytesString)

            'gTrackingFile.TrackingValues.Add(CValor)
            MostrarValors(False)
          End If
        End If
      End If
    Catch ex As Exception

    End Try
  End Sub

#End Region

#Region "Source manager"
  Private WithEvents CPiTrackingSourceManager As New TrackingSourceManager

  Public Sub InitManager()
    Me.CPiTrackingSourceManager.ClearHosts()
    For Each CHost As TrackingHost In gudtCnfg.Hosts.LlistaHosts
      Me.CPiTrackingSourceManager.AddHost(CHost)
    Next
    Me.CPiTrackingSourceManager.Enabled = True
  End Sub

  Public Sub StopManager()
    For Each CSource As TrackingSource In Me.CPiTrackingSourceManager.TrackingSources
      CSource.StopTrackingListener()
    Next
    Me.CPiTrackingSourceManager.Enabled = False
  End Sub


  Private Sub CPiTrackingSourceManager_NewTrackingValue(ByVal CiValue As TrackingValue) Handles CPiTrackingSourceManager.NewTrackingValue
    Debug.Print(CiValue.HOST)
    Me.MostrarValors(True)
  End Sub
#End Region

#Region "Menus"
  Private Sub OpcionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpcionsToolStripMenuItem.Click
    MostrarOpcions(Me)
  End Sub

  Private frmPiDraw As frmDraw

  Private Sub VeureSimulacióToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VeureSimulacióToolStripMenuItem.Click

    Try
      frmPiDraw = New frmDraw
      frmPiDraw.Show(Me)
    Catch ex As Exception
    End Try
  End Sub

  Private Sub frmPiSniffer_ListenState(ByVal bListenState As Boolean) Handles _sniffer.ListenState
    If bListenState = True Then
      Me.ButtonListen.Text = "Stop"
      Me.ButtonListen.BackColor = Color.Green
    Else
      Me.ButtonListen.Text = "Listen"
      Me.ButtonListen.BackColor = Color.Gray
    End If
  End Sub

  Private Sub IPiCameraExporter_UpdateProgress(ByVal diProgress1 As Double, ByVal diProgress2 As Double, ByVal siText As String) Handles IPiCameraExporter.UpdateProgress
    Try
      frmPiProgress.showProgress(diProgress1, diProgress2, siText)
    Catch ex As Exception

    End Try

  End Sub

  Private Sub ValorsEnTempsRealToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ValorsEnTempsRealToolStripMenuItem.Click
    ValorsEnTempsRealToolStripMenuItem.Checked = Not ValorsEnTempsRealToolStripMenuItem.Checked
  End Sub

  Private Sub GràficaEnTempsRealToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GràficaEnTempsRealToolStripMenuItem.Click
    GràficaEnTempsRealToolStripMenuItem.Checked = Not GràficaEnTempsRealToolStripMenuItem.Checked
  End Sub

  Private Sub ViewportsEnTempsRealToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewportsEnTempsRealToolStripMenuItem.Click
    ViewportsEnTempsRealToolStripMenuItem.Checked = Not ViewportsEnTempsRealToolStripMenuItem.Checked
  End Sub

  Private Sub ButtonPlayer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPlayer.Click
    Try
      If Me.CPuTrackingPlayer Is Nothing Then Me.CPuTrackingPlayer = New TrackingPlayerUDP
      Me.CPuTrackingPlayer.Channel = CInt(Me.NumericUpDownPlayerChannel.Value)
      Select Case Me.CPuTrackingPlayer.TrackingPlayerState
        Case eTrackingPlayerState.Started
          Me.CPuTrackingPlayer.TrackingStop()
          Me.ButtonPlayer.Text = "Start"
        Case eTrackingPlayerState.Relay
          Me.CPuTrackingPlayer.TrackingStop()
          Me.ButtonPlayer.Text = "Start"
        Case Else

          Dim CAux As TrackingSource
          Me.CPuTrackingPlayer.TrackingSource = Nothing
          For Each CAux In gTrackingFile.TrackingSources
            If Me.ComboBoxTrackingPlayer.Text = CAux.Host & " " & CAux.Port Then
              Me.CPuTrackingPlayer.TrackingSource = CAux
              Exit For
            End If
          Next

          If Not Me.CPuTrackingPlayer.TrackingSource Is Nothing Then
            Me.CPuTrackingPlayer.TrackingStart()
            Me.ButtonPlayer.Text = "Stop"
          End If
      End Select

    Catch ex As Exception

    End Try
  End Sub

  Private Sub ArxiuPathDORADToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArxiuPathDORADToolStripMenuItem.Click
    Try
      Dim sImportFile As String
      Me.OpenFileDialogImportar.ShowDialog()
      sImportFile = Me.OpenFileDialogImportar.FileName

      gTrackingFile = New TrackingFile(sImportFile, (System.IO.Path.GetExtension(Me.OpenFileDialogImportar.FileName).ToLower = ".path"))
      MostrarValors(True)
    Catch ex As Exception

    End Try
  End Sub
#End Region

#Region "Gestió de sessions"
  Private Sub NovaSessio(ByVal siName As String)
    Try
      If Not gTrackingFile Is Nothing Then
        gTrackingFile.Clear()
        If Not Me.frmPiPackets Is Nothing Then Me.frmPiPackets.CPuTrackingFile = gTrackingFile

        MostrarValors(True)
      End If

    Catch ex As Exception

    End Try
  End Sub

  Private Sub NovaSessióToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NovaSessióToolStripMenuItem.Click
    NovaSessio("")
  End Sub

  Private Sub DesarSessióToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DesarSessióToolStripMenuItem.Click
    Try
      Me.SaveFileDialog1.Filter = "Tracking sessions|*.trs|All files|*.*"
      Me.SaveFileDialog1.CheckPathExists = True
      Me.SaveFileDialog1.AutoUpgradeEnabled = True
      Me.SaveFileDialog1.ShowDialog()

      If Me.SaveFileDialog1.FileName <> "" Then
        If Not gTrackingFile Is Nothing Then
          SerializeObjectToFile(Me.SaveFileDialog1.FileName, gTrackingFile, False)
        End If
      End If
    Catch ex As Exception

    End Try
  End Sub

  Private Sub CarregarSessióToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarregarSessióToolStripMenuItem.Click
    Try
      Me.OpenFileDialogImportar.Filter = "Tracking sessions|*.trs|All files|*.*"
      Me.OpenFileDialogImportar.CheckPathExists = True
      Me.OpenFileDialogImportar.AutoUpgradeEnabled = True
      Me.OpenFileDialogImportar.Multiselect = False
      Me.OpenFileDialogImportar.ShowDialog()

      If Me.OpenFileDialogImportar.FileName <> "" Then
        Me.Cursor = Cursors.WaitCursor
        If gTrackingFile Is Nothing Then gTrackingFile = New TrackingFile("", False)
        DesserializeObjectFromFile(Me.OpenFileDialogImportar.FileName, gTrackingFile, False)
        If Not Me.frmPiPackets Is Nothing Then Me.frmPiPackets.CPuTrackingFile = gTrackingFile
        For Each CValue As TrackingValue In gTrackingFile.TrackingValues
          CValue.UpdateRadianValues()
        Next
        Me.MostrarValors(True)
        'IniciarSliderFromTrackingData(Me.CPuTrackingFile)
      End If
      Me.InitOpenGL()
    Catch ex As Exception

    End Try
    Me.Cursor = Cursors.Default
  End Sub

  Private Sub ButtonClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonClear.Click
    NovaSessio("")
  End Sub
#End Region

#Region "Finestra paquets"
  Private Sub VeureFinestraDePaquetsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VeureFinestraDePaquetsToolStripMenuItem.Click
    Try
      If Me.frmPiPackets Is Nothing Then
        frmPiPackets = New frmPackets
        frmPiPackets.CPuTrackingFile = gTrackingFile
        frmPiPackets.CPuTrackingPlayer = Me.CPuTrackingPlayer
        frmPiPackets.Show(Me)
      Else
        Me.frmPiPackets.Close()
        Me.frmPiPackets = Nothing
      End If
      Me.VeureFinestraDePaquetsToolStripMenuItem.Checked = Not (Me.frmPiPackets Is Nothing)
    Catch ex As Exception

    End Try
  End Sub


  Private Sub frmPiPackets_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frmPiPackets.FormClosed
    Me.VeureFinestraDePaquetsToolStripMenuItem.Checked = False
    Me.frmPiPackets = Nothing
  End Sub

  Private Sub frmPiPackets_SelectionChanged() Handles frmPiPackets.SelectionChanged
    Me.MostrarValors(False)
  End Sub

#End Region

#Region "Init OpenGL"
  Private LiveUpdate As Boolean = True
  Private OpenGLLoaded As Boolean = False

  Private Function InitOpenGL() As [Boolean]
    Dim bRes As Boolean = False
    Try
      GL.ClearColor(Me.GLControl1.BackColor)
      GL.Enable(EnableCap.DepthTest)
      GL.Disable(EnableCap.Texture2D)
      GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill)
      'GLControl1 = New GLControl(New OpenTK.Graphics.GraphicsMode(32, 24, 8, 4), 3, 0, OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible)
      'Me.PanelOpenGL.Controls.Add(GLControl1)
      'GLControl1.Dock = DockStyle.Fill
      'GLControl1.Visible = True
      Me.OpenGLLoaded = True

      GlControl1_Resize(Me.GLControl1, EventArgs.Empty)

      'Me.InitOpenGL()
      bRes = True
      Me.TimerFrame.Enabled = True
    Catch ex As Exception
      'MsgBox(ex.ToString)
    End Try
    Return bRes
  End Function

  Private Sub GlControl1_Resize(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim CControl As OpenTK.GLControl = CType(sender, OpenTK.GLControl)
    ' TODO: Perceber melhor sobre o glViewport()
    GL.Viewport(0, 0, CControl.ClientSize.Width, CControl.ClientSize.Height)

    Dim aspect As Single = CSng(CControl.Width) / CControl.Height
    Dim projMat As Matrix4 = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect, 0.1, 100.0)

    GL.MatrixMode(MatrixMode.Projection)
    GL.LoadMatrix(projMat)

    GL.MatrixMode(MatrixMode.Modelview)
    GL.LoadIdentity()
  End Sub

#End Region

#Region "Draw functions"
  Protected Sub OnRenderFrame()
    Try
      If Me.CheckBoxRender.Checked = False Then Exit Sub

      GlControl1_Resize(Me.GLControl1, System.EventArgs.Empty)
      GL.Clear(ClearBufferMask.ColorBufferBit Or ClearBufferMask.DepthBufferBit)

      SetCamPosition(Me.ePiViewPort)

      GL.Disable(EnableCap.Blend)
      GL.Disable(EnableCap.Texture2D)


      'DrawGrid(New Vector3(0.3, 1.3, 0.3))

      'DrawGrid(New Vector3(0.3, 0.3, 0.3))
      Dim fovx, fovy As Single
      Dim CValue As TrackingValue
      Dim CSource As TrackingSource
      Me.LlistaTextos.Clear()
      Dim sTitle As String = ""

      sTitle = sTitle & "[" & Me.ePiViewPort.ToString & "] "
      If Me.LiveUpdate Then
        sTitle = sTitle & "[Live] "
      Else
        sTitle = sTitle & "[" & msToTimeCode(Me.CurrentTime - Me.InitTime) & "] " & (Me.CurrentTime)
      End If
      Me.LlistaTextos.Add(sTitle)
      If Not gTrackingFile Is Nothing Then
        For nIndex As Integer = 0 To gTrackingFile.SelectedSources.Count - 1
          CSource = gTrackingFile.SelectedSources(nIndex)
          If Me.LiveUpdate Then
            CValue = CSource.LastTrackingValue
          Else
            CValue = CSource.GetValueByTime(Me.CurrentTime)
          End If
          With CValue
            'DrawCube(.POS_X, .POS_Y, .POS_Z, 0.25, 0.25, 1, .ROT_X, .ROT_Y, .ROT_Z)
            fovx = .FOV_RAD
            fovy = System.Math.Atan(.ASPECT * System.Math.Tan(.FOV_RAD))
            'DrawCamera(.POS_X, .POS_Y, .POS_Z, 0.2, 0.2, 0.5, 0, 0, 0, fovx, fovy)
            DrawCamera2(.POS_X, .POS_Y, .POS_Z, 0.2, 0.2, 0.5, .TARGET_X, .TARGET_Y, .TARGET_Z, fovx, fovy)
            'DrawPoint(.TARGET_X, .TARGET_Y, .TARGET_Z)
            Dim CSphere As New Sphere
            CSphere.DrawSphere(New OpenTK.Vector3(.TARGET_X, .TARGET_Y, .TARGET_Z), 0.1, 32)

            Me.LlistaTextos.Add(CSource.Host & " " & CSource.TrackingHost.IP)
            Me.LlistaTextos.Add("Frame " & .Index & " " & .Frame)
            Me.LlistaTextos.Add("Pos" & vbTab & Format(CValue.POS_X, "0.000") & vbTab & Format(CValue.POS_Y, "0.000") & vbTab & Format(CValue.POS_Z, "0.000"))
            Me.LlistaTextos.Add("Target" & vbTab & Format(CValue.TARGET_X, "0.000") & vbTab & Format(CValue.TARGET_Y, "0.000") & vbTab & Format(CValue.TARGET_Z, "0.000"))
            Me.LlistaTextos.Add("Rot" & vbTab & Format(180 * CValue.ROT_X / System.Math.PI, "0.0") & vbTab & Format(180 * CValue.ROT_Y / System.Math.PI, "0.0") & vbTab & Format(180 * CValue.ROT_Z / System.Math.PI, "0.0"))
            Me.LlistaTextos.Add("Fov" & vbTab & Format(CValue.FOV, "0.0"))
            Me.LlistaTextos.Add("ROLL" & vbTab & Format(CValue.ROT_ROLL, "0.0"))
            Me.LlistaTextos.Add("")
          End With
        Next
      End If

      'DrawGrid(New Vector3(0.9, 0.9, 0.9))
      DrawGrids(New Vector3(1, 1, 1))

      SetCamPosition(eViewport.Overlay)
      UpdateTextTextures()
      Me.RenderText()

      Me.GLControl1.SwapBuffers()

    Catch ex As Exception
      Debug.Print(ex.ToString)
    End Try
  End Sub

  Protected Sub SetCamPosition(ByVal eiViewPort As eViewport)

    GL.LoadIdentity()
    GL.Translate(0, 0, -30)
    'GL.Rotate(angle, 1, 0, 1)
    'GL.Rotate(angle, 0, 1, 0)

    Select Case eiViewPort
      Case eViewport.Top
        GL.Rotate(90, 1, 0, 0)
        GL.Rotate(0, 0, 1, 0)
        GL.Rotate(0, 0, 0, 1)

      Case eViewport.Bottom
        GL.Rotate(-90, 1, 0, 0)
        GL.Rotate(0, 0, 1, 0)
        GL.Rotate(0, 0, 0, 1)
      Case eViewport.Left
        GL.Rotate(0, 1, 0, 0)
        GL.Rotate(90, 0, 1, 0)
        GL.Rotate(0, 0, 0, 1)
      Case eViewport.Right
        GL.Rotate(0, 1, 0, 0)
        GL.Rotate(-90, 0, 1, 0)
        GL.Rotate(0, 0, 0, 1)
      Case eViewport.Front
        GL.Rotate(0, 1, 0, 0)
        GL.Rotate(0, 0, 1, 0)
        GL.Rotate(0, 0, 0, 1)
      Case eViewport.Back
        GL.Rotate(180, 1, 0, 0)
        GL.Rotate(0, 0, 1, 0)
        GL.Rotate(180, 0, 0, 1)
      Case eViewport.Perspective
        GL.Rotate(45, 1, 0, 0)
        GL.Rotate(45, 0, 1, 0)
        GL.Rotate(0, 0, 0, 1)
      Case eViewport.Overlay
        GL.MatrixMode(MatrixMode.Projection)
        GL.LoadIdentity()
        Dim ortho As Matrix4 = Matrix4.CreateOrthographicOffCenter(-1, 1, 1, -1, 0, 3000)
        GL.LoadMatrix(ortho)

    End Select
  End Sub

  Protected Sub DrawGrid(ByVal iColor As Vector3)
    GL.Color3(iColor)
    SetCamPosition(Me.ePiViewPort)
    GL.Begin(BeginMode.Lines)
    For i As Integer = -10 To 10
      If i <> 0 Then
        GL.Vertex3(i, 0, -10)
        GL.Vertex3(i, 0, 10)

        GL.Vertex3(-10, 0, i)
        GL.Vertex3(10, 0, i)
      End If
    Next
    GL.End()

    Dim NewColor As New Vector3(1.0, 1.0, 1.0)
    NewColor = NewColor - iColor

    GL.Color3(NewColor)
    GL.Begin(BeginMode.Lines)

    GL.Vertex3(0, 0, -10)
    GL.Vertex3(0, 0, 10)

    GL.Vertex3(-10, 0, 0)
    GL.Vertex3(10, 0, 0)

    GL.End()
  End Sub

  Protected Sub DrawGrids(ByVal iDirection As Vector3)

    Dim CColor As OpenTK.Graphics.Color4
    Dim CBaseColor As OpenTK.Graphics.Color4
    Dim CVertexColor As OpenTK.Graphics.Color4

    Dim ColorLevel As Double = 0.25
    SetCamPosition(Me.ePiViewPort)
    GL.Begin(BeginMode.Lines)

    'X
    CBaseColor = New OpenTK.Graphics.Color4(0.2F, 0.2F, 0.2F, 0.2F)
    For i As Integer = -10 To 10
      CColor = CBaseColor
      GL.Color3(CColor)
      GL.Vertex3(i * iDirection.X, 0, -10)
      GL.Vertex3(i * iDirection.X, 0, 10)
      GL.Vertex3(-10, 0, i * iDirection.Z)
      GL.Vertex3(10, 0, i * iDirection.Z)
    Next
    ''y
    'For i As Integer = -10 To 10
    '  'CColor = CBaseColor
    '  If i <> 0 Then
    '    CVertexColor = CColor : CVertexColor.R = 0 : CVertexColor.B = ColorLevel : GL.Color4(CVertexColor)
    '    GL.Vertex3(0, i * iDirection.Y, -10)
    '    GL.Vertex3(0, i * iDirection.Y, 10)
    '    CVertexColor = CColor : CVertexColor.R = ColorLevel : CVertexColor.B = 0 : GL.Color4(CVertexColor)
    '    GL.Vertex3(-10, i * iDirection.Y, 0)
    '    GL.Vertex3(10, i * iDirection.Y, 0)
    '  Else
    '    CVertexColor = CColor : CVertexColor.R = 0 : CVertexColor.B = 0 : GL.Color4(CVertexColor)
    '    GL.Vertex3(0, i * iDirection.Y, -10)
    '    GL.Vertex3(0, i * iDirection.Y, 10)
    '    CVertexColor = CColor : CVertexColor.R = 0 : CVertexColor.B = 0 : GL.Color4(CVertexColor)
    '    GL.Vertex3(-10, i * iDirection.Y, 0)
    '    GL.Vertex3(10, i * iDirection.Y, 0)
    '  End If
    'Next
    ''z
    'For i As Integer = -10 To 10
    '  'CColor = CBaseColor
    '  If i <> 0 Then
    '    CVertexColor = CColor : CVertexColor.R = 0 : CVertexColor.G = ColorLevel : GL.Color4(CVertexColor)
    '    GL.Vertex3(0, -10, i * iDirection.Z)
    '    GL.Vertex3(0, 10, i * iDirection.Z)
    '    CVertexColor = CColor : CVertexColor.R = ColorLevel : CVertexColor.G = 0 : GL.Color4(CVertexColor)
    '    GL.Vertex3(-10, 0, i * iDirection.Z)
    '    GL.Vertex3(10, 0, i * iDirection.Z)
    '  Else
    '    CVertexColor = CColor : CVertexColor.R = 0 : CVertexColor.G = 0 : GL.Color4(CVertexColor)
    '    GL.Vertex3(0, -10, i * iDirection.Z)
    '    GL.Vertex3(0, 10, i * iDirection.Z)
    '    CVertexColor = CColor : CVertexColor.R = 0 : CVertexColor.G = 0 : GL.Color4(CVertexColor)
    '    GL.Vertex3(-10, 0, i * iDirection.Z)
    '    GL.Vertex3(10, 0, i * iDirection.Z)
    '  End If
    'Next
    GL.End()
  End Sub

  Protected Sub DrawCube(ByVal posX As Single, ByVal posY As Single, ByVal posZ As Single, ByVal ScaleX As Single, ByVal ScaleY As Single, ByVal ScaleZ As Single, ByVal RotX As Single, ByVal RotY As Single, ByVal RotZ As Single)
    Dim CubeVertexes(8) As OpenTK.Vector4
    Dim LineVertexes(2) As OpenTK.Vector4

    CubeVertexes(0) = New OpenTK.Vector4(-1, 1, 1, 1)
    CubeVertexes(1) = New OpenTK.Vector4(1, 1, 1, 1)
    CubeVertexes(2) = New OpenTK.Vector4(1, -1, 1, 1)
    CubeVertexes(3) = New OpenTK.Vector4(-1, -1, 1, 1)
    CubeVertexes(4) = New OpenTK.Vector4(1, 1, -1, 1)
    CubeVertexes(5) = New OpenTK.Vector4(-1, 1, -1, 1)
    CubeVertexes(6) = New OpenTK.Vector4(-1, -1, -1, 1)
    CubeVertexes(7) = New OpenTK.Vector4(1, -1, -1, 1)

    LineVertexes(0) = New OpenTK.Vector4(0, 0, 0, 1)
    LineVertexes(1) = New OpenTK.Vector4(0, 0, 10000, 1)

    Dim MatTranslate As OpenTK.Matrix4 = OpenTK.Matrix4.Identity
    MatTranslate = OpenTK.Matrix4.CreateTranslation(posX, posY, posZ)


    Dim MatRotateX As OpenTK.Matrix4 = OpenTK.Matrix4.CreateRotationX(RotX)
    Dim MatRotateY As OpenTK.Matrix4 = OpenTK.Matrix4.CreateRotationY(-RotY)
    Dim MatRotateZ As OpenTK.Matrix4 = OpenTK.Matrix4.CreateRotationZ(RotZ)
    Dim MatRotate As OpenTK.Matrix4 = MatRotateZ * MatRotateY * MatRotateX
    Dim MatRotoTranslate As OpenTK.Matrix4 = MatRotate * MatTranslate

    Dim MatScale As OpenTK.Matrix4 = OpenTK.Matrix4.Identity
    MatScale.M11 = ScaleX
    MatScale.M22 = ScaleY
    MatScale.M33 = ScaleZ

    'MatRotoTranslate = MatRotate * MatTranslate * MatScale

    'Transform vertexes
    For nIndex As Integer = 0 To 7
      CubeVertexes(nIndex) = Vector4.Transform(CubeVertexes(nIndex), MatScale)
      CubeVertexes(nIndex) = Vector4.Transform(CubeVertexes(nIndex), MatRotoTranslate)
    Next
    For nIndex As Integer = 0 To 1
      LineVertexes(nIndex) = Vector4.Transform(LineVertexes(nIndex), MatRotoTranslate)
    Next

    GL.Color4(0.5, 0.5, 0.5, 0.5)
    GL.Begin(BeginMode.QuadStrip)
    ' Face frontal
    GL.TexCoord2(0.0, 1.0) : GL.Vertex3(CubeVertexes(0).Xyz)
    GL.TexCoord2(1.0, 1.0) : GL.Vertex3(CubeVertexes(1).Xyz)
    GL.TexCoord2(1.0, 0.0) : GL.Vertex3(CubeVertexes(2).Xyz)
    GL.TexCoord2(0.0, 0.0) : GL.Vertex3(CubeVertexes(3).Xyz)

    ' Face traseira
    GL.TexCoord2(0.0, 1.0) : GL.Vertex3(CubeVertexes(4).Xyz)
    GL.TexCoord2(1.0, 1.0) : GL.Vertex3(CubeVertexes(5).Xyz)
    GL.TexCoord2(1.0, 0.0) : GL.Vertex3(CubeVertexes(6).Xyz)
    GL.TexCoord2(0.0, 0.0) : GL.Vertex3(CubeVertexes(7).Xyz)

    ' Face lateral direita
    GL.TexCoord2(0.0, 1.0) : GL.Vertex3(CubeVertexes(1).Xyz)
    GL.TexCoord2(1.0, 1.0) : GL.Vertex3(CubeVertexes(4).Xyz)
    GL.TexCoord2(1.0, 0.0) : GL.Vertex3(CubeVertexes(7).Xyz)
    GL.TexCoord2(0.0, 0.0) : GL.Vertex3(CubeVertexes(2).Xyz)

    ' Face lateral esquerda
    GL.TexCoord2(0.0, 1.0) : GL.Vertex3(CubeVertexes(5).Xyz)
    GL.TexCoord2(0.0, 0.0) : GL.Vertex3(CubeVertexes(6).Xyz)
    GL.TexCoord2(1.0, 0.0) : GL.Vertex3(CubeVertexes(3).Xyz)
    GL.TexCoord2(1.0, 1.0) : GL.Vertex3(CubeVertexes(0).Xyz)

    ' Face de cima
    GL.TexCoord2(0.0, 1.0) : GL.Vertex3(CubeVertexes(5).Xyz)
    GL.TexCoord2(1.0, 1.0) : GL.Vertex3(CubeVertexes(4).Xyz)
    GL.TexCoord2(1.0, 0.0) : GL.Vertex3(CubeVertexes(1).Xyz)
    GL.TexCoord2(0.0, 0.0) : GL.Vertex3(CubeVertexes(0).Xyz)

    ' Face de baixo
    GL.TexCoord2(0.0, 0.0) : GL.Vertex3(CubeVertexes(6).Xyz)
    GL.TexCoord2(0.0, 1.0) : GL.Vertex3(CubeVertexes(3).Xyz)
    GL.TexCoord2(1.0, 1.0) : GL.Vertex3(CubeVertexes(2).Xyz)
    GL.TexCoord2(1.0, 0.0) : GL.Vertex3(CubeVertexes(7).Xyz)
    GL.End()



    GL.Color3(1.0, 1.0, 1.0)
    GL.Begin(BeginMode.Lines)
    GL.Color3(1.0, 0.0, 1.0)
    GL.Vertex3(LineVertexes(0).Xyz)
    GL.Color3(1.0, 0.0, 1.0)
    GL.Vertex3(LineVertexes(1).Xyz)
    GL.End()

  End Sub

  Protected Sub DrawCamera(ByVal posX As Single, ByVal posY As Single, ByVal posZ As Single, ByVal ScaleX As Single, ByVal ScaleY As Single, ByVal ScaleZ As Single, ByVal RotX As Single, ByVal RotY As Single, ByVal RotZ As Single, ByVal FOVX As Single, ByVal FOVY As Single)
    Dim CubeVertexes(8) As OpenTK.Vector4
    Dim FustrumVertexes(5) As OpenTK.Vector4
    Dim LineVertexes(2) As OpenTK.Vector4
    Dim Amplitude As Double = 10

    CubeVertexes(0) = New OpenTK.Vector4(-1, 1, 0, 1)
    CubeVertexes(1) = New OpenTK.Vector4(1, 1, 0, 1)
    CubeVertexes(2) = New OpenTK.Vector4(1, -1, 0, 1)
    CubeVertexes(3) = New OpenTK.Vector4(-1, -1, 0, 1)
    CubeVertexes(4) = New OpenTK.Vector4(1, 1, -2, 1)
    CubeVertexes(5) = New OpenTK.Vector4(-1, 1, -2, 1)
    CubeVertexes(6) = New OpenTK.Vector4(-1, -1, -2, 1)
    CubeVertexes(7) = New OpenTK.Vector4(1, -1, -2, 1)

    FustrumVertexes(0) = New OpenTK.Vector4(0, 0, 0, 1)
    FustrumVertexes(1) = New OpenTK.Vector4(Amplitude * System.Math.Sin(FOVX), Amplitude * System.Math.Sin(FOVY), Amplitude, 1)
    FustrumVertexes(2) = New OpenTK.Vector4(-Amplitude * System.Math.Sin(FOVX), Amplitude * System.Math.Sin(FOVY), Amplitude, 1)
    FustrumVertexes(3) = New OpenTK.Vector4(-Amplitude * System.Math.Sin(FOVX), -Amplitude * System.Math.Sin(FOVY), Amplitude, 1)
    FustrumVertexes(4) = New OpenTK.Vector4(Amplitude * System.Math.Sin(FOVX), -Amplitude * System.Math.Sin(FOVY), Amplitude, 1)

    LineVertexes(0) = New OpenTK.Vector4(0, 0, 0, 1)
    LineVertexes(1) = New OpenTK.Vector4(0, 0, 10000, 1)

    Dim MatTranslate As OpenTK.Matrix4 = OpenTK.Matrix4.Identity
    MatTranslate = OpenTK.Matrix4.CreateTranslation(posX, posY, posZ)


    Dim MatRotateX As OpenTK.Matrix4 = OpenTK.Matrix4.CreateRotationX(System.Math.PI / 2 + RotX)
    Dim MatRotateY As OpenTK.Matrix4 = OpenTK.Matrix4.CreateRotationY(RotY)
    Dim MatRotateZ As OpenTK.Matrix4 = OpenTK.Matrix4.CreateRotationZ(RotZ)
    Dim MatRotate As OpenTK.Matrix4 = MatRotateX * MatRotateY * MatRotateZ
    Dim MatRotoTranslate As OpenTK.Matrix4 = MatRotate * MatTranslate

    Dim MatScale As OpenTK.Matrix4 = OpenTK.Matrix4.Identity
    MatScale.M11 = ScaleX
    MatScale.M22 = ScaleY
    MatScale.M33 = ScaleZ

    'MatRotoTranslate = MatRotate * MatTranslate * MatScale

    'Transform vertexes
    For nIndex As Integer = 0 To 7
      CubeVertexes(nIndex) = Vector4.Transform(CubeVertexes(nIndex), MatScale)
      CubeVertexes(nIndex) = Vector4.Transform(CubeVertexes(nIndex), MatRotoTranslate)
    Next

    For nIndex As Integer = 0 To 4
      FustrumVertexes(nIndex) = Vector4.Transform(FustrumVertexes(nIndex), MatScale)
      FustrumVertexes(nIndex) = Vector4.Transform(FustrumVertexes(nIndex), MatRotoTranslate)
    Next
    For nIndex As Integer = 0 To 1
      LineVertexes(nIndex) = Vector4.Transform(LineVertexes(nIndex), MatRotoTranslate)
    Next

    DrawVertexes(CubeVertexes, New Vector3(0.5, 0.5, 0.5), BeginMode.Quads)
    DrawVertexes(CubeVertexes, New Vector3(0.9, 0.9, 0.9), BeginMode.Lines)
    DrawFustrum(FustrumVertexes)


    GL.Color3(1.0, 1.0, 1.0)
    GL.Begin(BeginMode.Lines)
    GL.Color3(1.0, 0.0, 1.0)
    GL.Vertex3(LineVertexes(0).Xyz)
    GL.Color3(1.0, 0.0, 1.0)
    GL.Vertex3(LineVertexes(1).Xyz)
    GL.End()

  End Sub


  Protected Sub DrawCamera2(ByVal posX As Single, ByVal posY As Single, ByVal posZ As Single, ByVal ScaleX As Single, ByVal ScaleY As Single, ByVal ScaleZ As Single, ByVal TargetX As Single, ByVal TargetY As Single, ByVal TargetZ As Single, ByVal FOVX As Single, ByVal FOVY As Single)
    Dim CubeVertexes(8) As OpenTK.Vector4
    Dim FustrumVertexes(5) As OpenTK.Vector4
    Dim LineVertexes(2) As OpenTK.Vector4
    Dim Amplitude As Double = 10

    CubeVertexes(0) = New OpenTK.Vector4(-1, 1, 0, 1)
    CubeVertexes(1) = New OpenTK.Vector4(1, 1, 0, 1)
    CubeVertexes(2) = New OpenTK.Vector4(1, -1, 0, 1)
    CubeVertexes(3) = New OpenTK.Vector4(-1, -1, 0, 1)
    CubeVertexes(4) = New OpenTK.Vector4(1, 1, 2, 1)
    CubeVertexes(5) = New OpenTK.Vector4(-1, 1, 2, 1)
    CubeVertexes(6) = New OpenTK.Vector4(-1, -1, 2, 1)
    CubeVertexes(7) = New OpenTK.Vector4(1, -1, 2, 1)

    FustrumVertexes(0) = New OpenTK.Vector4(0, 0, 0, 1)
    FustrumVertexes(1) = New OpenTK.Vector4(Amplitude * System.Math.Sin(FOVX), -Amplitude * System.Math.Sin(FOVY), -Amplitude, 1)
    FustrumVertexes(2) = New OpenTK.Vector4(-Amplitude * System.Math.Sin(FOVX), -Amplitude * System.Math.Sin(FOVY), -Amplitude, 1)
    FustrumVertexes(3) = New OpenTK.Vector4(-Amplitude * System.Math.Sin(FOVX), Amplitude * System.Math.Sin(FOVY), -Amplitude, 1)
    FustrumVertexes(4) = New OpenTK.Vector4(Amplitude * System.Math.Sin(FOVX), Amplitude * System.Math.Sin(FOVY), -Amplitude, 1)

    LineVertexes(0) = New OpenTK.Vector4(0, 0, 0, 1)
    LineVertexes(1) = New OpenTK.Vector4(0, 0, 10000, 1)

    Dim MatTranslate As OpenTK.Matrix4 = OpenTK.Matrix4.Identity
    MatTranslate = OpenTK.Matrix4.CreateTranslation(posX, posY, posZ)

    Dim rotx, roty, rotz As Double
    Dim MatRotateX As OpenTK.Matrix4
    Dim MatRotateY As OpenTK.Matrix4 = OpenTK.Matrix4.CreateRotationY(0)
    Dim MatRotateZ As OpenTK.Matrix4 = OpenTK.Matrix4.CreateRotationZ(0)
    rotx = 0
    roty = 0
    rotz = 0

    Dim prod As Double
    Dim x As Double = TargetX - posX
    Dim y As Double = TargetY - posY
    Dim z As Double = TargetZ - posZ
    Dim Center As OpenTK.Vector3 = New OpenTK.Vector3(posX, posY, posZ)
    Dim Target As OpenTK.Vector3 = New OpenTK.Vector3(TargetX, TargetY, TargetZ)
    Dim DirectionVector As OpenTK.Vector3 = New OpenTK.Vector3(-x, -y, z)
    Dim VLength As Double = System.Math.Sqrt(x * x + y * y + z * z)
    'Rotación con respecto X

    Dim MatRotate As OpenTK.Matrix4

    MatRotate = OpenTK.Matrix4.LookAt(New Vector3(0, 0, 0), DirectionVector, New Vector3(0, 1, 0))
    MatRotate = OpenTK.Matrix4.RotateZ(3.14159 / 2) * MatRotate
    Dim MatRotoTranslate As OpenTK.Matrix4 = MatRotate * MatTranslate

    Dim MatScale As OpenTK.Matrix4 = OpenTK.Matrix4.Identity
    MatScale.M11 = ScaleX
    MatScale.M22 = ScaleY
    MatScale.M33 = ScaleZ

    'MatRotoTranslate = MatRotate * MatTranslate * MatScale

    'Transform vertexes
    For nIndex As Integer = 0 To 7
      CubeVertexes(nIndex) = Vector4.Transform(CubeVertexes(nIndex), MatScale)
      CubeVertexes(nIndex) = Vector4.Transform(CubeVertexes(nIndex), MatRotoTranslate)
    Next

    For nIndex As Integer = 0 To 4
      FustrumVertexes(nIndex) = Vector4.Transform(FustrumVertexes(nIndex), MatScale)
      FustrumVertexes(nIndex) = Vector4.Transform(FustrumVertexes(nIndex), MatRotoTranslate)
    Next
    For nIndex As Integer = 0 To 1
      LineVertexes(nIndex) = Vector4.Transform(LineVertexes(nIndex), MatRotoTranslate)
    Next

    DrawVertexes(CubeVertexes, New Vector3(0.5, 0.5, 0.5), BeginMode.Quads)
    DrawVertexes(CubeVertexes, New Vector3(0.9, 0.9, 0.9), BeginMode.Lines)
    DrawFustrum(FustrumVertexes)

    GL.Begin(BeginMode.Lines)
    GL.Color3(1.0, 0.0, 0.0)
    GL.Vertex3(Center)
    GL.Vertex3(Target)
    GL.End()


    'GL.Color3(1.0, 1.0, 1.0)
    'GL.Begin(BeginMode.Lines)
    'GL.Color3(1.0, 0.0, 1.0)
    'GL.Vertex3(LineVertexes(0).Xyz)
    'GL.Color3(1.0, 0.0, 1.0)
    'GL.Vertex3(LineVertexes(1).Xyz)
    'GL.End()

  End Sub
  Private Sub DrawVertexes(ByVal CubeVertexes() As OpenTK.Vector4, ByVal iColor As Vector3, ByVal iMode As BeginMode)

    GL.Color3(iColor)
    Dim anFace(6, 4) As Integer
    anFace(0, 0) = 0 : anFace(0, 1) = 1 : anFace(0, 2) = 2 : anFace(0, 3) = 3
    anFace(1, 0) = 4 : anFace(1, 1) = 5 : anFace(1, 2) = 6 : anFace(1, 3) = 7
    anFace(2, 0) = 1 : anFace(2, 1) = 4 : anFace(2, 2) = 7 : anFace(2, 3) = 2
    anFace(3, 0) = 5 : anFace(3, 1) = 6 : anFace(3, 2) = 3 : anFace(3, 3) = 0
    anFace(4, 0) = 5 : anFace(4, 1) = 4 : anFace(4, 2) = 1 : anFace(4, 3) = 0
    anFace(5, 0) = 6 : anFace(5, 1) = 3 : anFace(5, 2) = 2 : anFace(5, 3) = 7

    Select Case iMode
      Case BeginMode.Lines
        GL.Begin(iMode)
        ' Face frontal
        For nFace As Integer = 0 To 5
          GL.Vertex3(CubeVertexes(anFace(nFace, 0)).Xyz)
          GL.Vertex3(CubeVertexes(anFace(nFace, 1)).Xyz)

          GL.Vertex3(CubeVertexes(anFace(nFace, 1)).Xyz)
          GL.Vertex3(CubeVertexes(anFace(nFace, 2)).Xyz)

          GL.Vertex3(CubeVertexes(anFace(nFace, 2)).Xyz)
          GL.Vertex3(CubeVertexes(anFace(nFace, 3)).Xyz)

          GL.Vertex3(CubeVertexes(anFace(nFace, 0)).Xyz)
          GL.Vertex3(CubeVertexes(anFace(nFace, 3)).Xyz)
        Next
        GL.End()
      Case BeginMode.LineLoop
        GL.Begin(iMode)
        ' Face frontal
        For nFace As Integer = 0 To 5
          GL.Vertex3(CubeVertexes(anFace(nFace, 0)).Xyz)
          GL.Vertex3(CubeVertexes(anFace(nFace, 1)).Xyz)
          GL.Vertex3(CubeVertexes(anFace(nFace, 2)).Xyz)
          GL.Vertex3(CubeVertexes(anFace(nFace, 3)).Xyz)
        Next
        GL.End()
      Case Else
        GL.Begin(iMode)
        ' Face frontal
        For nFace As Integer = 0 To 5
          GL.Vertex3(CubeVertexes(anFace(nFace, 0)).Xyz)
          GL.Vertex3(CubeVertexes(anFace(nFace, 1)).Xyz)
          GL.Vertex3(CubeVertexes(anFace(nFace, 2)).Xyz)
          GL.Vertex3(CubeVertexes(anFace(nFace, 3)).Xyz)
        Next
        GL.End()
    End Select
  End Sub

  Private Sub DrawFustrum(ByVal FustrumVertexes() As OpenTK.Vector4)
    Dim LineVertexes(2) As OpenTK.Vector4

    GL.Color3(1.0, 1.0, 1.0)
    GL.Begin(BeginMode.Lines)
    ' Face frontal
    GL.Vertex3(FustrumVertexes(0).Xyz)
    GL.Vertex3(FustrumVertexes(1).Xyz)

    GL.Vertex3(FustrumVertexes(0).Xyz)
    GL.Vertex3(FustrumVertexes(2).Xyz)

    GL.Vertex3(FustrumVertexes(0).Xyz)
    GL.Vertex3(FustrumVertexes(3).Xyz)

    GL.Vertex3(FustrumVertexes(0).Xyz)
    GL.Vertex3(FustrumVertexes(4).Xyz)

    GL.Vertex3(FustrumVertexes(1).Xyz)
    GL.Vertex3(FustrumVertexes(2).Xyz)

    GL.Vertex3(FustrumVertexes(2).Xyz)
    GL.Vertex3(FustrumVertexes(3).Xyz)

    GL.Vertex3(FustrumVertexes(3).Xyz)
    GL.Vertex3(FustrumVertexes(4).Xyz)

    GL.Vertex3(FustrumVertexes(4).Xyz)
    GL.Vertex3(FustrumVertexes(1).Xyz)

    GL.End()
  End Sub

  Private Sub DrawCamInfo(ByVal CiValue As TrackingValue, ByVal CiTrackingSource As TrackingSource, ByVal g As Graphics, ByVal nIndex As Integer)
    Try

    Catch ex As Exception

    End Try
  End Sub

  Private Sub DrawPoint(ByVal posX As Single, ByVal posY As Single, ByVal posZ As Single)
    Dim CubeVertexes(8) As OpenTK.Vector4
    Dim LineVertexes(2) As OpenTK.Vector4

    CubeVertexes(0) = New OpenTK.Vector4(-1, 1, 1, 1)
    CubeVertexes(1) = New OpenTK.Vector4(1, 1, 1, 1)
    CubeVertexes(2) = New OpenTK.Vector4(1, -1, 1, 1)
    CubeVertexes(3) = New OpenTK.Vector4(-1, -1, 1, 1)
    CubeVertexes(4) = New OpenTK.Vector4(1, 1, -1, 1)
    CubeVertexes(5) = New OpenTK.Vector4(-1, 1, -1, 1)
    CubeVertexes(6) = New OpenTK.Vector4(-1, -1, -1, 1)
    CubeVertexes(7) = New OpenTK.Vector4(1, -1, -1, 1)

    LineVertexes(0) = New OpenTK.Vector4(0, 0, 0, 1)
    LineVertexes(1) = New OpenTK.Vector4(0, 0, 10000, 1)

    Dim MatTranslate As OpenTK.Matrix4 = OpenTK.Matrix4.Identity
    MatTranslate = OpenTK.Matrix4.CreateTranslation(posX, posY, posZ)


    Dim MatRotoTranslate As OpenTK.Matrix4 = MatTranslate

    Dim MatScale As OpenTK.Matrix4 = OpenTK.Matrix4.Identity
    MatScale.M11 = 0.1
    MatScale.M22 = 0.1
    MatScale.M33 = 0.1

    'MatRotoTranslate = MatRotate * MatTranslate * MatScale

    'Transform vertexes
    For nIndex As Integer = 0 To 7
      CubeVertexes(nIndex) = Vector4.Transform(CubeVertexes(nIndex), MatScale)
      CubeVertexes(nIndex) = Vector4.Transform(CubeVertexes(nIndex), MatRotoTranslate)
    Next
    For nIndex As Integer = 0 To 1
      LineVertexes(nIndex) = Vector4.Transform(LineVertexes(nIndex), MatRotoTranslate)
    Next

    GL.Color4(0.5, 0.5, 0.5, 0.5)
    GL.Begin(BeginMode.QuadStrip)
    ' Face frontal
    GL.TexCoord2(0.0, 1.0) : GL.Vertex3(CubeVertexes(0).Xyz)
    GL.TexCoord2(1.0, 1.0) : GL.Vertex3(CubeVertexes(1).Xyz)
    GL.TexCoord2(1.0, 0.0) : GL.Vertex3(CubeVertexes(2).Xyz)
    GL.TexCoord2(0.0, 0.0) : GL.Vertex3(CubeVertexes(3).Xyz)

    ' Face traseira
    GL.TexCoord2(0.0, 1.0) : GL.Vertex3(CubeVertexes(4).Xyz)
    GL.TexCoord2(1.0, 1.0) : GL.Vertex3(CubeVertexes(5).Xyz)
    GL.TexCoord2(1.0, 0.0) : GL.Vertex3(CubeVertexes(6).Xyz)
    GL.TexCoord2(0.0, 0.0) : GL.Vertex3(CubeVertexes(7).Xyz)

    ' Face lateral direita
    GL.TexCoord2(0.0, 1.0) : GL.Vertex3(CubeVertexes(1).Xyz)
    GL.TexCoord2(1.0, 1.0) : GL.Vertex3(CubeVertexes(4).Xyz)
    GL.TexCoord2(1.0, 0.0) : GL.Vertex3(CubeVertexes(7).Xyz)
    GL.TexCoord2(0.0, 0.0) : GL.Vertex3(CubeVertexes(2).Xyz)

    ' Face lateral esquerda
    GL.TexCoord2(0.0, 1.0) : GL.Vertex3(CubeVertexes(5).Xyz)
    GL.TexCoord2(0.0, 0.0) : GL.Vertex3(CubeVertexes(6).Xyz)
    GL.TexCoord2(1.0, 0.0) : GL.Vertex3(CubeVertexes(3).Xyz)
    GL.TexCoord2(1.0, 1.0) : GL.Vertex3(CubeVertexes(0).Xyz)

    ' Face de cima
    GL.TexCoord2(0.0, 1.0) : GL.Vertex3(CubeVertexes(5).Xyz)
    GL.TexCoord2(1.0, 1.0) : GL.Vertex3(CubeVertexes(4).Xyz)
    GL.TexCoord2(1.0, 0.0) : GL.Vertex3(CubeVertexes(1).Xyz)
    GL.TexCoord2(0.0, 0.0) : GL.Vertex3(CubeVertexes(0).Xyz)

    ' Face de baixo
    GL.TexCoord2(0.0, 0.0) : GL.Vertex3(CubeVertexes(6).Xyz)
    GL.TexCoord2(0.0, 1.0) : GL.Vertex3(CubeVertexes(3).Xyz)
    GL.TexCoord2(1.0, 1.0) : GL.Vertex3(CubeVertexes(2).Xyz)
    GL.TexCoord2(1.0, 0.0) : GL.Vertex3(CubeVertexes(7).Xyz)
    GL.End()
  End Sub

#End Region

#Region "Trackbar functions"
  Public InitTime As Double = -1
  Public FinalTime As Double = 0
  Public CurrentValue As Double = 0
  Public CurrentTime As Double = 0

  Protected Sub OnUpdateScroll()
    Try
      Me.LiveUpdate = (Me.TrackBarFrame.Value = Me.TrackBarFrame.Maximum)
      InitTime = -1
      FinalTime = -1
      If Not gTrackingFile Is Nothing Then
        For Each CSource As TrackingSource In gTrackingFile.SelectedSources
          If CSource.LastTrackingValue.CapturedMS < InitTime Or InitTime = -1 Then
            InitTime = CSource.TrackingValues(0).CapturedMS
          End If
        Next
        For Each CSource As TrackingSource In gTrackingFile.SelectedSources
          If CSource.LastTrackingValue.CapturedMS - InitTime > FinalTime Then
            FinalTime = CSource.LastTrackingValue.CapturedMS
          End If
        Next
        If Me.LiveUpdate Then
          If Not Me.TrackBarFrame.Value = Me.TrackBarFrame.Maximum Then
            Me.TrackBarFrame.Value = Me.TrackBarFrame.Maximum
          End If
        End If
      End If

    Catch ex As Exception

    End Try

  End Sub

  Private Sub TrackBarFrame_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBarFrame.Scroll
    Me.LiveUpdate = (Me.TrackBarFrame.Value = Me.TrackBarFrame.Maximum)
    CurrentTime = Me.TrackBarFrame.Value / 1000 * (Me.FinalTime - Me.InitTime) + Me.InitTime
    ' Me.OnRenderFrame()
  End Sub

#End Region

#Region "Viewport"
  Public Enum eViewport
    Top
    Bottom
    Left
    Right
    Front
    Back
    Perspective
    Overlay
  End Enum
  Private ePiViewPort As eViewport = eViewport.Top

  Public Property Viewport() As eViewport
    Get
      Return Me.ePiViewPort
    End Get
    Set(ByVal value As eViewport)
      Me.ePiViewPort = value
    End Set
  End Property

  Private Sub TopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TopToolStripMenuItem.Click
    Me.Viewport = eViewport.Top
  End Sub

  Private Sub BottomToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BottomToolStripMenuItem.Click
    Me.Viewport = eViewport.Bottom
  End Sub

  Private Sub LeftToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeftToolStripMenuItem.Click
    Me.Viewport = eViewport.Left
  End Sub

  Private Sub RightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RightToolStripMenuItem.Click
    Me.Viewport = eViewport.Right
  End Sub

  Private Sub FrontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FrontToolStripMenuItem.Click
    Me.Viewport = eViewport.Front
  End Sub

  Private Sub BackToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackToolStripMenuItem.Click
    Me.Viewport = eViewport.Back
  End Sub

  Private Sub PerspectiveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PerspectiveToolStripMenuItem.Click
    Me.Viewport = eViewport.Perspective
  End Sub
#End Region

#Region "Text functions"
  Private TextTextureIDs(1) As Integer
  Private Bitmaps(1) As Bitmap
  Private LlistaTextos As New List(Of String)



  Protected Sub UpdateTextTextures()
    UpdateTexture(0, LlistaTextos)
  End Sub


  Protected Sub UpdateTexture(ByVal textureIndex As Integer, Optional ByVal LlistaText As List(Of String) = Nothing)
    If Me.Bitmaps(textureIndex) Is Nothing Then
      Me.Bitmaps(textureIndex) = New Bitmap(Me.GLControl1.Width, Me.GLControl1.Height)
    End If
    If Me.Bitmaps(textureIndex).Width <> Me.GLControl1.Width Or Me.Bitmaps(textureIndex).Height <> Me.GLControl1.Height Then
      Me.Bitmaps(textureIndex).Dispose()
      Me.Bitmaps(textureIndex) = New Bitmap(Me.GLControl1.Width, Me.GLControl1.Height)
    End If

    Dim bmp As Bitmap = Me.Bitmaps(textureIndex)
    Dim textureId = Me.TextTextureIDs(textureIndex)

    Dim g As Graphics = Graphics.FromImage(bmp)
    g.Clear(Color.Transparent)
    'g.DrawString(Now.ToString, Me.Font, Brushes.White, 0, 0)
    If Not LlistaText Is Nothing Then
      For nIndex As Integer = 0 To LlistaText.Count - 1
        g.DrawString(LlistaText(nIndex), Me.Font, Brushes.White, 0, 15 * nIndex)
      Next
    End If
    g.Dispose()

    Dim data As System.Drawing.Imaging.BitmapData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height),
                                            System.Drawing.Imaging.ImageLockMode.ReadOnly,
                                            System.Drawing.Imaging.PixelFormat.Format32bppArgb)

    If Not GL.IsTexture(textureId) Then
      GL.BindTexture(TextureTarget.Texture2D, textureId)
    End If

    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                  bmp.Width, bmp.Height, 0, PixelFormat.Bgra,
                  PixelType.UnsignedByte, data.Scan0)

    bmp.UnlockBits(data)
    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear)
    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear)
  End Sub


  Private Sub RenderText()
    'Enable texturing
    GL.Enable(EnableCap.Blend)
    GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.DstColor)
    GL.Enable(EnableCap.Texture2D)
    GL.BindTexture(TextureTarget.Texture2D, Me.TextTextureIDs(0))

    'GL.Disable(EnableCap.Texture2D)
    GL.Color4(1.0, 1.0, 1.0, 1.0)
    'Draw screen aligned quad
    GL.Begin(BeginMode.Quads)
    GL.TexCoord2(0.0, 0.0) : GL.Vertex3(-1, -1, 0)
    GL.TexCoord2(1.0, 0.0) : GL.Vertex3(1, -1, 0)
    GL.TexCoord2(1.0, 1.0) : GL.Vertex3(1, 1, 0)
    GL.TexCoord2(0.0, 1.0) : GL.Vertex3(-1, 1, 0)
    GL.End()
  End Sub

#End Region

#Region "Form events"

  Private Sub TimerInitOPENGL_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerInitOPENGL.Tick
    Try
      If Me.InitOpenGL Then
        Me.TimerInitOPENGL.Enabled = False
      End If
    Catch ex As Exception

    End Try
  End Sub

  Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Me.TimerInitOPENGL.Enabled = True
  End Sub

  Private Sub GLControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLControl1.Load

  End Sub

  Private Sub TimerFrame_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerFrame.Tick
    Me.TimerFrame.Enabled = False
    Me.OnUpdateScroll()
    Me.TimerFrame.Interval = 250
    Me.TimerFrame.Enabled = True
    Me.OnRenderFrame()
  End Sub

#End Region

#Region "Tracking player"
  Private fTrackingPlayer As FormTrackingPlayer

  Private Sub ReplayFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplayFormToolStripMenuItem.Click
    'If fReplay Is Nothing Then
    '  fReplay = New frmReplay
    'End If
    'fReplay.Show(Me)

    fTrackingPlayer = New FormTrackingPlayer
    fTrackingPlayer.Show(Me)

  End Sub
#End Region

#Region "Trackig hosts"
  Private Sub EditarHostsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditarHostsToolStripMenuItem.Click
    MostrarOpcions(Me)
  End Sub

  Private Sub AfegirHostToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AfegirHostToolStripMenuItem.Click
    Dim CHost As TrackingHost = Nothing
    Dim COldHost As TrackingHost = Nothing
    Dim dlg As New DialogHost

    If Not gTrackingFile Is Nothing Then
      Dim nIndex As Integer = Me.C1FlexGridPorts.Row - Me.C1FlexGridPorts.Rows.Fixed
      CHost = gTrackingFile.TrackingSources(nIndex).TrackingHost
      COldHost = gudtCnfg.Hosts.GetHost(CHost)
      If Not COldHost Is Nothing Then
        MsgBox("No es pot afegir, ja existeix un igual")
        Exit Sub
      End If
    End If
    dlg.CPuHost = CHost
    dlg.ShowDialog(Me)
    If dlg.DialogResult = DialogResult.OK Then
      CHost = dlg.CPuHost
      If Not CHost Is Nothing Then
        gudtCnfg.Hosts.LlistaHosts.Add(CHost)
      End If
    End If
  End Sub

  Private Sub EditarHostToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditarHostToolStripMenuItem.Click

  End Sub

  Private Sub EliminarDeLaLlistaDeHostsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminarDeLaLlistaDeHostsToolStripMenuItem.Click

  End Sub
#End Region

  Private Sub ContextMenuStripHosts_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStripHosts.Opening

  End Sub

  Private Sub CheckBoxRender_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRender.CheckedChanged

  End Sub

  Private Sub ButtonSetInitPosition_Click(sender As Object, e As EventArgs) Handles ButtonSetInitPosition.Click
    Me.LabelInitPosition.Text = Me.CurrentTime.ToString
  End Sub

  Private Sub ButtonSetOutPosition_Click(sender As Object, e As EventArgs) Handles ButtonSetOutPosition.Click
    Me.LabelOutPosition.Text = Me.CurrentTime.ToString
  End Sub

  Private Sub ExportarEnFormatGSToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExportarEnFormatGSToolStripMenuItem1.Click
    Try
      Try
        Me.SaveFileDialogExportGS.Filter = "Format GS json|*.json|All files|*.*"
        Me.SaveFileDialogExportGS.CheckPathExists = True
        Me.SaveFileDialogExportGS.AutoUpgradeEnabled = True
        Me.SaveFileDialogExportGS.ShowDialog()

        If Me.SaveFileDialogExportGS.FileName <> "" Then
          If Not gTrackingFile Is Nothing Then
            _gsSniffer = New GSSniffer
            _gsSniffer.StreamsPath = Me.SaveFileDialogExportGS.FileName
            Double.TryParse(Me.LabelInitPosition.Text, _gsSniffer.InitPosition)
            Double.TryParse(Me.LabelOutPosition.Text, _gsSniffer.FinalPosition)
            _gsSniffer.TrackingFile = gTrackingFile
            _gsSniffer.DesarFrameInfoStreams()

            gTrackingFile = New TrackingFile
            gTrackingFile = _gsSniffer.ExportarTrackingFile()

            If Not Me.frmPiPackets Is Nothing Then Me.frmPiPackets.CPuTrackingFile = gTrackingFile
            For Each CValue As TrackingValue In gTrackingFile.TrackingValues
              CValue.UpdateRadianValues()
            Next
            Me.MostrarValors(True)
          End If
        End If
      Catch ex As Exception

      End Try


    Catch ex As Exception

    End Try
  End Sub

  Private Sub ExportarToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ExportarToolStripMenuItem2.Click
    Try
      Static sExportFile As String = ""
      Me.SaveFileDialog1.Filter = "FBX|*.FBX|VRML|*.WRL|All files|*.*"
      Me.SaveFileDialog1.FileName = sExportFile
      Me.SaveFileDialog1.ShowDialog()




      If Me.SaveFileDialog1.FileNames.Length > 0 Then
        sExportFile = Me.SaveFileDialog1.FileName

        frmPiProgress = New FormExportProgress


        Dim CExport As IExporter
        Me.Cursor = Cursors.WaitCursor
        Select Case System.IO.Path.GetExtension(sExportFile).ToUpper
          Case ".DAE"
            CExport = New CCameraColladaExporter()
          Case ".FBX"
            CExport = New CameraFBXExporter()
          Case Else
            CExport = New CameraVRMLExporter()
        End Select

        frmPiProgress.Text = "Export"
        frmPiProgress.Show(Me)

        AddHandler CExport.UpdateProgress, AddressOf Me.IPiCameraExporter_UpdateProgress


        Dim LlistaValors As List(Of TrackingValue) = gTrackingFile.TrackingValuesByPort(0)

        CExport.Export(gTrackingFile, sExportFile, -1)
        'CExport.Export(LlistaValors, -1)
      End If


    Catch ex As Exception

    End Try
    frmPiProgress.Close()
    frmPiProgress = Nothing
    Me.Cursor = Cursors.Default
  End Sub

  Private Sub ArxiuEnFormatGSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArxiuEnFormatGSToolStripMenuItem.Click
    Try
      Try
        Dim sImportFile As String
        Me.OpenFileDialogImportarGS.ShowDialog()
        sImportFile = Me.OpenFileDialogImportarGS.FileName

        _gsSniffer.CarregarFrameInfoStreams(sImportFile)

      Catch ex As Exception

      End Try

      gTrackingFile = New TrackingFile
      gTrackingFile = _gsSniffer.ExportarTrackingFile()

      If Not Me.frmPiPackets Is Nothing Then Me.frmPiPackets.CPuTrackingFile = gTrackingFile
      For Each CValue As TrackingValue In gTrackingFile.TrackingValues
        CValue.UpdateRadianValues()
      Next
      OnUpdateScroll()
      Me.MostrarValors(True)

    Catch ex As Exception

    End Try
  End Sub
End Class
