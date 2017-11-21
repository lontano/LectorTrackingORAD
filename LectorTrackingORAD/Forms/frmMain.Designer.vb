<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
    Me.OpenFileDialogImportar = New System.Windows.Forms.OpenFileDialog()
    Me.ButtonClear = New System.Windows.Forms.Button()
    Me.ButtonListen = New System.Windows.Forms.Button()
    Me.NumericUpDownPlayerChannel = New System.Windows.Forms.NumericUpDown()
    Me.ComboBoxTrackingPlayer = New System.Windows.Forms.ComboBox()
    Me.ButtonPlayer = New System.Windows.Forms.Button()
    Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
    Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
    Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.NovaSessióToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.DesarSessióToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.CarregarSessióToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
    Me.ImportarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ArxiuPathDORADToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ArxiuEnFormatGSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ExportarToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    Me.ExportarToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
    Me.ExportarEnFormatGSToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
    Me.OpcionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.VeureSimulacióToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
    Me.TancarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.VeureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ValorsEnTempsRealToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.GràficaEnTempsRealToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ViewportsEnTempsRealToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.EinesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.VeureFinestraDePaquetsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ReplayFormToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.PanelCapture = New System.Windows.Forms.Panel()
    Me.PanelRealtime = New System.Windows.Forms.Panel()
    Me.SplitContainerAll = New System.Windows.Forms.SplitContainer()
    Me.C1FlexGridPorts = New C1.Win.C1FlexGrid.C1FlexGrid()
    Me.ContextMenuStripHosts = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.EditarHostsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
    Me.AfegirHostToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.EditarHostToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.EliminarDeLaLlistaDeHostsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.PanelOpenGL = New System.Windows.Forms.Panel()
    Me.ContextMenuStripViewport = New System.Windows.Forms.ContextMenuStrip(Me.components)
    Me.TopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.BottomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.LeftToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.RightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.FrontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.BackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
    Me.PerspectiveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
    Me.TrackBarFrame = New System.Windows.Forms.TrackBar()
    Me.CheckBoxRender = New System.Windows.Forms.CheckBox()
    Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
    Me.ButtonSetInitPosition = New System.Windows.Forms.Button()
    Me.ButtonSetOutPosition = New System.Windows.Forms.Button()
    Me.LabelInitPosition = New System.Windows.Forms.Label()
    Me.LabelOutPosition = New System.Windows.Forms.Label()
    Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
    Me.TimerFrame = New System.Windows.Forms.Timer(Me.components)
    Me.TimerInitOPENGL = New System.Windows.Forms.Timer(Me.components)
    Me.OpenFileDialogImportarGS = New System.Windows.Forms.OpenFileDialog()
    Me.GLControl1 = New LectorArxiuTrackingORAD.GControlHigh()
    Me.SaveFileDialogExportGS = New System.Windows.Forms.SaveFileDialog()
    CType(Me.NumericUpDownPlayerChannel, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.MenuStrip1.SuspendLayout()
    Me.PanelCapture.SuspendLayout()
    Me.PanelRealtime.SuspendLayout()
    CType(Me.SplitContainerAll, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SplitContainerAll.Panel1.SuspendLayout()
    Me.SplitContainerAll.Panel2.SuspendLayout()
    Me.SplitContainerAll.SuspendLayout()
    CType(Me.C1FlexGridPorts, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.ContextMenuStripHosts.SuspendLayout()
    Me.Panel1.SuspendLayout()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.PanelOpenGL.SuspendLayout()
    Me.ContextMenuStripViewport.SuspendLayout()
    CType(Me.TrackBarFrame, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TableLayoutPanel2.SuspendLayout()
    CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'OpenFileDialogImportar
    '
    Me.OpenFileDialogImportar.FileName = "OpenFileDialog1"
    Me.OpenFileDialogImportar.Filter = "path files|*.path|all files|*.*"
    '
    'ButtonClear
    '
    Me.ButtonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.ButtonClear.Location = New System.Drawing.Point(85, 3)
    Me.ButtonClear.Name = "ButtonClear"
    Me.ButtonClear.Size = New System.Drawing.Size(75, 23)
    Me.ButtonClear.TabIndex = 3
    Me.ButtonClear.Text = "Reset"
    Me.ButtonClear.UseVisualStyleBackColor = True
    '
    'ButtonListen
    '
    Me.ButtonListen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.ButtonListen.Location = New System.Drawing.Point(4, 3)
    Me.ButtonListen.Name = "ButtonListen"
    Me.ButtonListen.Size = New System.Drawing.Size(75, 23)
    Me.ButtonListen.TabIndex = 2
    Me.ButtonListen.Text = "Listen"
    Me.ButtonListen.UseVisualStyleBackColor = True
    '
    'NumericUpDownPlayerChannel
    '
    Me.NumericUpDownPlayerChannel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.NumericUpDownPlayerChannel.Location = New System.Drawing.Point(231, 4)
    Me.NumericUpDownPlayerChannel.Name = "NumericUpDownPlayerChannel"
    Me.NumericUpDownPlayerChannel.Size = New System.Drawing.Size(86, 20)
    Me.NumericUpDownPlayerChannel.TabIndex = 2
    '
    'ComboBoxTrackingPlayer
    '
    Me.ComboBoxTrackingPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBoxTrackingPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.ComboBoxTrackingPlayer.FormattingEnabled = True
    Me.ComboBoxTrackingPlayer.Location = New System.Drawing.Point(9, 3)
    Me.ComboBoxTrackingPlayer.Name = "ComboBoxTrackingPlayer"
    Me.ComboBoxTrackingPlayer.Size = New System.Drawing.Size(216, 21)
    Me.ComboBoxTrackingPlayer.TabIndex = 1
    '
    'ButtonPlayer
    '
    Me.ButtonPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.ButtonPlayer.Location = New System.Drawing.Point(324, 3)
    Me.ButtonPlayer.Name = "ButtonPlayer"
    Me.ButtonPlayer.Size = New System.Drawing.Size(119, 23)
    Me.ButtonPlayer.TabIndex = 0
    Me.ButtonPlayer.Text = "Play"
    Me.ButtonPlayer.UseVisualStyleBackColor = True
    '
    'SaveFileDialog1
    '
    Me.SaveFileDialog1.Filter = "Collada|*.DAE|VRML|*.wrl|All files|*.*"
    '
    'MenuStrip1
    '
    Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
    Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem, Me.VeureToolStripMenuItem, Me.EinesToolStripMenuItem})
    Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
    Me.MenuStrip1.Name = "MenuStrip1"
    Me.MenuStrip1.Size = New System.Drawing.Size(1013, 24)
    Me.MenuStrip1.TabIndex = 12
    Me.MenuStrip1.Text = "MenuStrip1"
    '
    'ArxiuToolStripMenuItem
    '
    Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NovaSessióToolStripMenuItem, Me.DesarSessióToolStripMenuItem, Me.CarregarSessióToolStripMenuItem, Me.ToolStripMenuItem3, Me.ImportarToolStripMenuItem, Me.ExportarToolStripMenuItem1, Me.ToolStripMenuItem2, Me.OpcionsToolStripMenuItem, Me.VeureSimulacióToolStripMenuItem, Me.ToolStripMenuItem1, Me.TancarToolStripMenuItem})
    Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
    Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
    Me.ArxiuToolStripMenuItem.Text = "Arxiu"
    '
    'NovaSessióToolStripMenuItem
    '
    Me.NovaSessióToolStripMenuItem.Name = "NovaSessióToolStripMenuItem"
    Me.NovaSessióToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
    Me.NovaSessióToolStripMenuItem.Text = "Nova sessió"
    '
    'DesarSessióToolStripMenuItem
    '
    Me.DesarSessióToolStripMenuItem.Name = "DesarSessióToolStripMenuItem"
    Me.DesarSessióToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
    Me.DesarSessióToolStripMenuItem.Text = "Desar sessió..."
    '
    'CarregarSessióToolStripMenuItem
    '
    Me.CarregarSessióToolStripMenuItem.Name = "CarregarSessióToolStripMenuItem"
    Me.CarregarSessióToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
    Me.CarregarSessióToolStripMenuItem.Text = "Carregar sessió..."
    '
    'ToolStripMenuItem3
    '
    Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
    Me.ToolStripMenuItem3.Size = New System.Drawing.Size(164, 6)
    '
    'ImportarToolStripMenuItem
    '
    Me.ImportarToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuPathDORADToolStripMenuItem, Me.ArxiuEnFormatGSToolStripMenuItem})
    Me.ImportarToolStripMenuItem.Name = "ImportarToolStripMenuItem"
    Me.ImportarToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
    Me.ImportarToolStripMenuItem.Text = "Importar"
    '
    'ArxiuPathDORADToolStripMenuItem
    '
    Me.ArxiuPathDORADToolStripMenuItem.Name = "ArxiuPathDORADToolStripMenuItem"
    Me.ArxiuPathDORADToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
    Me.ArxiuPathDORADToolStripMenuItem.Text = "Arxiu path d'ORAD..."
    '
    'ArxiuEnFormatGSToolStripMenuItem
    '
    Me.ArxiuEnFormatGSToolStripMenuItem.Name = "ArxiuEnFormatGSToolStripMenuItem"
    Me.ArxiuEnFormatGSToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
    Me.ArxiuEnFormatGSToolStripMenuItem.Text = "Arxiu en format GS..."
    '
    'ExportarToolStripMenuItem1
    '
    Me.ExportarToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportarToolStripMenuItem2, Me.ExportarEnFormatGSToolStripMenuItem1})
    Me.ExportarToolStripMenuItem1.Name = "ExportarToolStripMenuItem1"
    Me.ExportarToolStripMenuItem1.Size = New System.Drawing.Size(167, 22)
    Me.ExportarToolStripMenuItem1.Text = "Exportar"
    '
    'ExportarToolStripMenuItem2
    '
    Me.ExportarToolStripMenuItem2.Name = "ExportarToolStripMenuItem2"
    Me.ExportarToolStripMenuItem2.Size = New System.Drawing.Size(198, 22)
    Me.ExportarToolStripMenuItem2.Text = "Exportar..."
    '
    'ExportarEnFormatGSToolStripMenuItem1
    '
    Me.ExportarEnFormatGSToolStripMenuItem1.Name = "ExportarEnFormatGSToolStripMenuItem1"
    Me.ExportarEnFormatGSToolStripMenuItem1.Size = New System.Drawing.Size(198, 22)
    Me.ExportarEnFormatGSToolStripMenuItem1.Text = "Exportar en format GS..."
    '
    'ToolStripMenuItem2
    '
    Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
    Me.ToolStripMenuItem2.Size = New System.Drawing.Size(164, 6)
    '
    'OpcionsToolStripMenuItem
    '
    Me.OpcionsToolStripMenuItem.Name = "OpcionsToolStripMenuItem"
    Me.OpcionsToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
    Me.OpcionsToolStripMenuItem.Text = "Opcions..."
    '
    'VeureSimulacióToolStripMenuItem
    '
    Me.VeureSimulacióToolStripMenuItem.Name = "VeureSimulacióToolStripMenuItem"
    Me.VeureSimulacióToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
    Me.VeureSimulacióToolStripMenuItem.Text = "Veure simulació..."
    Me.VeureSimulacióToolStripMenuItem.Visible = False
    '
    'ToolStripMenuItem1
    '
    Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
    Me.ToolStripMenuItem1.Size = New System.Drawing.Size(164, 6)
    '
    'TancarToolStripMenuItem
    '
    Me.TancarToolStripMenuItem.Name = "TancarToolStripMenuItem"
    Me.TancarToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
    Me.TancarToolStripMenuItem.Text = "Tancar"
    '
    'VeureToolStripMenuItem
    '
    Me.VeureToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ValorsEnTempsRealToolStripMenuItem, Me.GràficaEnTempsRealToolStripMenuItem, Me.ViewportsEnTempsRealToolStripMenuItem})
    Me.VeureToolStripMenuItem.Name = "VeureToolStripMenuItem"
    Me.VeureToolStripMenuItem.Size = New System.Drawing.Size(49, 20)
    Me.VeureToolStripMenuItem.Text = "Veure"
    Me.VeureToolStripMenuItem.Visible = False
    '
    'ValorsEnTempsRealToolStripMenuItem
    '
    Me.ValorsEnTempsRealToolStripMenuItem.Name = "ValorsEnTempsRealToolStripMenuItem"
    Me.ValorsEnTempsRealToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
    Me.ValorsEnTempsRealToolStripMenuItem.Text = "Valors en temps real"
    '
    'GràficaEnTempsRealToolStripMenuItem
    '
    Me.GràficaEnTempsRealToolStripMenuItem.Name = "GràficaEnTempsRealToolStripMenuItem"
    Me.GràficaEnTempsRealToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
    Me.GràficaEnTempsRealToolStripMenuItem.Text = "Gràfica en temps real"
    '
    'ViewportsEnTempsRealToolStripMenuItem
    '
    Me.ViewportsEnTempsRealToolStripMenuItem.Name = "ViewportsEnTempsRealToolStripMenuItem"
    Me.ViewportsEnTempsRealToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
    Me.ViewportsEnTempsRealToolStripMenuItem.Text = "Viewports en temps real"
    '
    'EinesToolStripMenuItem
    '
    Me.EinesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VeureFinestraDePaquetsToolStripMenuItem, Me.ReplayFormToolStripMenuItem})
    Me.EinesToolStripMenuItem.Name = "EinesToolStripMenuItem"
    Me.EinesToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
    Me.EinesToolStripMenuItem.Text = "Eines"
    '
    'VeureFinestraDePaquetsToolStripMenuItem
    '
    Me.VeureFinestraDePaquetsToolStripMenuItem.Name = "VeureFinestraDePaquetsToolStripMenuItem"
    Me.VeureFinestraDePaquetsToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
    Me.VeureFinestraDePaquetsToolStripMenuItem.Text = "Veure finestra de paquets"
    '
    'ReplayFormToolStripMenuItem
    '
    Me.ReplayFormToolStripMenuItem.Name = "ReplayFormToolStripMenuItem"
    Me.ReplayFormToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
    Me.ReplayFormToolStripMenuItem.Text = "Replay form..."
    '
    'PanelCapture
    '
    Me.PanelCapture.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PanelCapture.Controls.Add(Me.ButtonListen)
    Me.PanelCapture.Controls.Add(Me.ButtonClear)
    Me.PanelCapture.Location = New System.Drawing.Point(12, 27)
    Me.PanelCapture.Name = "PanelCapture"
    Me.PanelCapture.Size = New System.Drawing.Size(545, 31)
    Me.PanelCapture.TabIndex = 13
    '
    'PanelRealtime
    '
    Me.PanelRealtime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PanelRealtime.Controls.Add(Me.ButtonPlayer)
    Me.PanelRealtime.Controls.Add(Me.NumericUpDownPlayerChannel)
    Me.PanelRealtime.Controls.Add(Me.ComboBoxTrackingPlayer)
    Me.PanelRealtime.Location = New System.Drawing.Point(563, 27)
    Me.PanelRealtime.Name = "PanelRealtime"
    Me.PanelRealtime.Size = New System.Drawing.Size(447, 31)
    Me.PanelRealtime.TabIndex = 4
    Me.PanelRealtime.Visible = False
    '
    'SplitContainerAll
    '
    Me.SplitContainerAll.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.SplitContainerAll.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
    Me.SplitContainerAll.Location = New System.Drawing.Point(12, 64)
    Me.SplitContainerAll.Name = "SplitContainerAll"
    '
    'SplitContainerAll.Panel1
    '
    Me.SplitContainerAll.Panel1.Controls.Add(Me.C1FlexGridPorts)
    '
    'SplitContainerAll.Panel2
    '
    Me.SplitContainerAll.Panel2.Controls.Add(Me.Panel1)
    Me.SplitContainerAll.Size = New System.Drawing.Size(998, 549)
    Me.SplitContainerAll.SplitterDistance = 261
    Me.SplitContainerAll.TabIndex = 11
    '
    'C1FlexGridPorts
    '
    Me.C1FlexGridPorts.BackColor = System.Drawing.SystemColors.ControlLight
    Me.C1FlexGridPorts.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
    Me.C1FlexGridPorts.ColumnInfo = "0,0,0,0,0,85,Columns:"
    Me.C1FlexGridPorts.ContextMenuStrip = Me.ContextMenuStripHosts
    Me.C1FlexGridPorts.Dock = System.Windows.Forms.DockStyle.Fill
    Me.C1FlexGridPorts.Location = New System.Drawing.Point(0, 0)
    Me.C1FlexGridPorts.Name = "C1FlexGridPorts"
    Me.C1FlexGridPorts.Rows.Count = 0
    Me.C1FlexGridPorts.Rows.Fixed = 0
    Me.C1FlexGridPorts.Size = New System.Drawing.Size(261, 549)
    Me.C1FlexGridPorts.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("C1FlexGridPorts.Styles"))
    Me.C1FlexGridPorts.TabIndex = 8
    '
    'ContextMenuStripHosts
    '
    Me.ContextMenuStripHosts.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditarHostsToolStripMenuItem, Me.ToolStripMenuItem4, Me.AfegirHostToolStripMenuItem, Me.EditarHostToolStripMenuItem, Me.EliminarDeLaLlistaDeHostsToolStripMenuItem})
    Me.ContextMenuStripHosts.Name = "ContextMenuStripHosts"
    Me.ContextMenuStripHosts.Size = New System.Drawing.Size(220, 98)
    '
    'EditarHostsToolStripMenuItem
    '
    Me.EditarHostsToolStripMenuItem.Name = "EditarHostsToolStripMenuItem"
    Me.EditarHostsToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
    Me.EditarHostsToolStripMenuItem.Text = "Editar hosts..."
    '
    'ToolStripMenuItem4
    '
    Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
    Me.ToolStripMenuItem4.Size = New System.Drawing.Size(216, 6)
    '
    'AfegirHostToolStripMenuItem
    '
    Me.AfegirHostToolStripMenuItem.Name = "AfegirHostToolStripMenuItem"
    Me.AfegirHostToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
    Me.AfegirHostToolStripMenuItem.Text = "Afegir host"
    '
    'EditarHostToolStripMenuItem
    '
    Me.EditarHostToolStripMenuItem.Name = "EditarHostToolStripMenuItem"
    Me.EditarHostToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
    Me.EditarHostToolStripMenuItem.Text = "Editar host"
    '
    'EliminarDeLaLlistaDeHostsToolStripMenuItem
    '
    Me.EliminarDeLaLlistaDeHostsToolStripMenuItem.Name = "EliminarDeLaLlistaDeHostsToolStripMenuItem"
    Me.EliminarDeLaLlistaDeHostsToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
    Me.EliminarDeLaLlistaDeHostsToolStripMenuItem.Text = "Eliminar de la llista de hosts"
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(733, 549)
    Me.Panel1.TabIndex = 2
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.PanelOpenGL, 0, 2)
    Me.TableLayoutPanel1.Controls.Add(Me.TrackBarFrame, 1, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.CheckBoxRender, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 1)
    Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 3
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(733, 549)
    Me.TableLayoutPanel1.TabIndex = 14
    '
    'PanelOpenGL
    '
    Me.TableLayoutPanel1.SetColumnSpan(Me.PanelOpenGL, 2)
    Me.PanelOpenGL.Controls.Add(Me.GLControl1)
    Me.PanelOpenGL.Dock = System.Windows.Forms.DockStyle.Fill
    Me.PanelOpenGL.Location = New System.Drawing.Point(3, 83)
    Me.PanelOpenGL.Name = "PanelOpenGL"
    Me.PanelOpenGL.Size = New System.Drawing.Size(727, 463)
    Me.PanelOpenGL.TabIndex = 4
    '
    'ContextMenuStripViewport
    '
    Me.ContextMenuStripViewport.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TopToolStripMenuItem, Me.BottomToolStripMenuItem, Me.LeftToolStripMenuItem, Me.RightToolStripMenuItem, Me.FrontToolStripMenuItem, Me.BackToolStripMenuItem, Me.ToolStripSeparator1, Me.PerspectiveToolStripMenuItem})
    Me.ContextMenuStripViewport.Name = "ContextMenuStripViewport"
    Me.ContextMenuStripViewport.Size = New System.Drawing.Size(135, 164)
    '
    'TopToolStripMenuItem
    '
    Me.TopToolStripMenuItem.Name = "TopToolStripMenuItem"
    Me.TopToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
    Me.TopToolStripMenuItem.Text = "Top"
    '
    'BottomToolStripMenuItem
    '
    Me.BottomToolStripMenuItem.Name = "BottomToolStripMenuItem"
    Me.BottomToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
    Me.BottomToolStripMenuItem.Text = "Bottom"
    '
    'LeftToolStripMenuItem
    '
    Me.LeftToolStripMenuItem.Name = "LeftToolStripMenuItem"
    Me.LeftToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
    Me.LeftToolStripMenuItem.Text = "Left"
    '
    'RightToolStripMenuItem
    '
    Me.RightToolStripMenuItem.Name = "RightToolStripMenuItem"
    Me.RightToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
    Me.RightToolStripMenuItem.Text = "Right"
    '
    'FrontToolStripMenuItem
    '
    Me.FrontToolStripMenuItem.Name = "FrontToolStripMenuItem"
    Me.FrontToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
    Me.FrontToolStripMenuItem.Text = "Front"
    '
    'BackToolStripMenuItem
    '
    Me.BackToolStripMenuItem.Name = "BackToolStripMenuItem"
    Me.BackToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
    Me.BackToolStripMenuItem.Text = "Back"
    '
    'ToolStripSeparator1
    '
    Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
    Me.ToolStripSeparator1.Size = New System.Drawing.Size(131, 6)
    '
    'PerspectiveToolStripMenuItem
    '
    Me.PerspectiveToolStripMenuItem.Name = "PerspectiveToolStripMenuItem"
    Me.PerspectiveToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
    Me.PerspectiveToolStripMenuItem.Text = "Perspective"
    '
    'TrackBarFrame
    '
    Me.TrackBarFrame.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TrackBarFrame.LargeChange = 25
    Me.TrackBarFrame.Location = New System.Drawing.Point(123, 3)
    Me.TrackBarFrame.Maximum = 1000
    Me.TrackBarFrame.Name = "TrackBarFrame"
    Me.TrackBarFrame.Size = New System.Drawing.Size(607, 34)
    Me.TrackBarFrame.SmallChange = 10
    Me.TrackBarFrame.TabIndex = 5
    Me.TrackBarFrame.TickFrequency = 0
    Me.TrackBarFrame.Value = 1000
    '
    'CheckBoxRender
    '
    Me.CheckBoxRender.AutoSize = True
    Me.CheckBoxRender.Dock = System.Windows.Forms.DockStyle.Fill
    Me.CheckBoxRender.Location = New System.Drawing.Point(3, 3)
    Me.CheckBoxRender.Name = "CheckBoxRender"
    Me.CheckBoxRender.Size = New System.Drawing.Size(114, 34)
    Me.CheckBoxRender.TabIndex = 6
    Me.CheckBoxRender.Text = "Render"
    Me.CheckBoxRender.UseVisualStyleBackColor = True
    '
    'TableLayoutPanel2
    '
    Me.TableLayoutPanel2.ColumnCount = 5
    Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
    Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
    Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
    Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
    Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
    Me.TableLayoutPanel2.Controls.Add(Me.ButtonSetInitPosition, 0, 0)
    Me.TableLayoutPanel2.Controls.Add(Me.ButtonSetOutPosition, 2, 0)
    Me.TableLayoutPanel2.Controls.Add(Me.LabelInitPosition, 1, 0)
    Me.TableLayoutPanel2.Controls.Add(Me.LabelOutPosition, 3, 0)
    Me.TableLayoutPanel2.Controls.Add(Me.NumericUpDown1, 4, 0)
    Me.TableLayoutPanel2.Location = New System.Drawing.Point(123, 43)
    Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
    Me.TableLayoutPanel2.RowCount = 1
    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel2.Size = New System.Drawing.Size(601, 28)
    Me.TableLayoutPanel2.TabIndex = 7
    '
    'ButtonSetInitPosition
    '
    Me.ButtonSetInitPosition.Location = New System.Drawing.Point(3, 3)
    Me.ButtonSetInitPosition.Name = "ButtonSetInitPosition"
    Me.ButtonSetInitPosition.Size = New System.Drawing.Size(114, 21)
    Me.ButtonSetInitPosition.TabIndex = 0
    Me.ButtonSetInitPosition.Text = "Set init position"
    Me.ButtonSetInitPosition.UseVisualStyleBackColor = True
    '
    'ButtonSetOutPosition
    '
    Me.ButtonSetOutPosition.Location = New System.Drawing.Point(243, 3)
    Me.ButtonSetOutPosition.Name = "ButtonSetOutPosition"
    Me.ButtonSetOutPosition.Size = New System.Drawing.Size(114, 21)
    Me.ButtonSetOutPosition.TabIndex = 1
    Me.ButtonSetOutPosition.Text = "Set out position"
    Me.ButtonSetOutPosition.UseVisualStyleBackColor = True
    '
    'LabelInitPosition
    '
    Me.LabelInitPosition.AutoSize = True
    Me.LabelInitPosition.Location = New System.Drawing.Point(123, 0)
    Me.LabelInitPosition.Name = "LabelInitPosition"
    Me.LabelInitPosition.Size = New System.Drawing.Size(60, 13)
    Me.LabelInitPosition.TabIndex = 2
    Me.LabelInitPosition.Text = "Init position"
    '
    'LabelOutPosition
    '
    Me.LabelOutPosition.AutoSize = True
    Me.LabelOutPosition.Location = New System.Drawing.Point(363, 0)
    Me.LabelOutPosition.Name = "LabelOutPosition"
    Me.LabelOutPosition.Size = New System.Drawing.Size(63, 13)
    Me.LabelOutPosition.TabIndex = 3
    Me.LabelOutPosition.Text = "Out position"
    '
    'NumericUpDown1
    '
    Me.NumericUpDown1.Location = New System.Drawing.Point(483, 3)
    Me.NumericUpDown1.Name = "NumericUpDown1"
    Me.NumericUpDown1.Size = New System.Drawing.Size(108, 20)
    Me.NumericUpDown1.TabIndex = 4
    '
    'TimerFrame
    '
    Me.TimerFrame.Interval = 40
    '
    'TimerInitOPENGL
    '
    '
    'OpenFileDialogImportarGS
    '
    Me.OpenFileDialogImportarGS.FileName = "OpenFileDialog1"
    Me.OpenFileDialogImportarGS.Filter = "json files|*.json|all files|*.*"
    '
    'GLControl1
    '
    Me.GLControl1.BackColor = System.Drawing.SystemColors.ControlDark
    Me.GLControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.GLControl1.ContextMenuStrip = Me.ContextMenuStripViewport
    Me.GLControl1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.GLControl1.Location = New System.Drawing.Point(0, 0)
    Me.GLControl1.Name = "GLControl1"
    Me.GLControl1.Size = New System.Drawing.Size(727, 463)
    Me.GLControl1.TabIndex = 1
    Me.GLControl1.VSync = False
    '
    'SaveFileDialogExportGS
    '
    Me.SaveFileDialogExportGS.Filter = "GS file|*.json"
    '
    'frmMain
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackColor = System.Drawing.SystemColors.ControlDark
    Me.ClientSize = New System.Drawing.Size(1013, 625)
    Me.Controls.Add(Me.PanelRealtime)
    Me.Controls.Add(Me.PanelCapture)
    Me.Controls.Add(Me.SplitContainerAll)
    Me.Controls.Add(Me.MenuStrip1)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MainMenuStrip = Me.MenuStrip1
    Me.Name = "frmMain"
    Me.Text = "Tracking capture"
    CType(Me.NumericUpDownPlayerChannel, System.ComponentModel.ISupportInitialize).EndInit()
    Me.MenuStrip1.ResumeLayout(False)
    Me.MenuStrip1.PerformLayout()
    Me.PanelCapture.ResumeLayout(False)
    Me.PanelRealtime.ResumeLayout(False)
    Me.SplitContainerAll.Panel1.ResumeLayout(False)
    Me.SplitContainerAll.Panel2.ResumeLayout(False)
    CType(Me.SplitContainerAll, System.ComponentModel.ISupportInitialize).EndInit()
    Me.SplitContainerAll.ResumeLayout(False)
    CType(Me.C1FlexGridPorts, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ContextMenuStripHosts.ResumeLayout(False)
    Me.Panel1.ResumeLayout(False)
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.TableLayoutPanel1.PerformLayout()
    Me.PanelOpenGL.ResumeLayout(False)
    Me.ContextMenuStripViewport.ResumeLayout(False)
    CType(Me.TrackBarFrame, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TableLayoutPanel2.ResumeLayout(False)
    Me.TableLayoutPanel2.PerformLayout()
    CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents OpenFileDialogImportar As System.Windows.Forms.OpenFileDialog
  Friend WithEvents ButtonListen As System.Windows.Forms.Button
  Friend WithEvents ButtonClear As System.Windows.Forms.Button
  Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
  Friend WithEvents C1FlexGridPorts As C1.Win.C1FlexGrid.C1FlexGrid
  Friend WithEvents SplitContainerAll As System.Windows.Forms.SplitContainer
  Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
  Friend WithEvents ArxiuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents OpcionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents TancarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents VeureSimulacióToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents VeureToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ValorsEnTempsRealToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents GràficaEnTempsRealToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ViewportsEnTempsRealToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ButtonPlayer As System.Windows.Forms.Button
  Friend WithEvents ComboBoxTrackingPlayer As System.Windows.Forms.ComboBox
  Friend WithEvents NumericUpDownPlayerChannel As System.Windows.Forms.NumericUpDown
  Friend WithEvents NovaSessióToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents DesarSessióToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents CarregarSessióToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents EinesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents VeureFinestraDePaquetsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents ImportarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ArxiuPathDORADToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents PanelCapture As System.Windows.Forms.Panel
  Friend WithEvents PanelRealtime As System.Windows.Forms.Panel
  Friend WithEvents PanelOpenGL As System.Windows.Forms.Panel
  Friend WithEvents GLControl1 As LectorArxiuTrackingORAD.GControlHigh
  Friend WithEvents TrackBarFrame As System.Windows.Forms.TrackBar
  Friend WithEvents ContextMenuStripViewport As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents TopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents BottomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents LeftToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents RightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents FrontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents BackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents PerspectiveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents TimerFrame As System.Windows.Forms.Timer
  Friend WithEvents TimerInitOPENGL As System.Windows.Forms.Timer
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents ReplayFormToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ContextMenuStripHosts As System.Windows.Forms.ContextMenuStrip
  Friend WithEvents AfegirHostToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents EditarHostToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents EliminarDeLaLlistaDeHostsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents EditarHostsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
  Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
  Friend WithEvents CheckBoxRender As CheckBox
  Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
  Friend WithEvents ButtonSetInitPosition As Button
  Friend WithEvents ButtonSetOutPosition As Button
  Friend WithEvents LabelInitPosition As Label
  Friend WithEvents LabelOutPosition As Label
  Friend WithEvents NumericUpDown1 As NumericUpDown
  Friend WithEvents ArxiuEnFormatGSToolStripMenuItem As ToolStripMenuItem
  Friend WithEvents ExportarToolStripMenuItem1 As ToolStripMenuItem
  Friend WithEvents ExportarToolStripMenuItem2 As ToolStripMenuItem
  Friend WithEvents ExportarEnFormatGSToolStripMenuItem1 As ToolStripMenuItem
  Friend WithEvents OpenFileDialogImportarGS As OpenFileDialog
  Friend WithEvents SaveFileDialogExportGS As SaveFileDialog
End Class
