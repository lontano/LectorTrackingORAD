Module MMain
  Public frmPuMain As frmMain

  Public gHostEntries As New HostEntries
  Public gTrackingFile As New TrackingFile
  Public _gsSniffer As GSSniffer
  Public gTrackingRouterManager As New TrackingRouter.TrackingRouterManager
  Public gTrackingPlayerFactory As New TrackingPlayerFactory

  Public Structure Configuracio
    Public Hosts As TrackingHosts
    Public UseHosts As Boolean
    Public LogFolder As String
    Public LogarErrorsAFitxer As Boolean
    Public VeureFinestraerrors As Boolean
    Public OffsetBytes As Integer
    Public SelectedInterface As String


    Public Sub Inicialitzar()
      Me.Hosts = New TrackingHosts
      Me.UseHosts = False
      LogFolder = ""
      LogarErrorsAFitxer = True
      VeureFinestraerrors = False
      OffsetBytes = 24
    End Sub
  End Structure

  Public gnNumConfig As Integer
  Public gudtCnfg As Configuracio

  Public Sub Main()
    Try
      frmPuMain = New frmMain

      LLegirLiniaComanda()
      gudtCnfg = LlegirConfiguracio(gnNumConfig)
      Application.Run(frmPuMain)

    Catch ex As Exception
      MsgBox(ex.ToString)
    End Try
  End Sub


  Private Sub LLegirLiniaComanda()
    Dim sParam As String
    gnNumConfig = 0
    For Each sParam In My.Application.CommandLineArgs
      If sParam.ToLower.StartsWith("/config=") Then
        gnNumConfig = CInt(sParam.Remove(0, "/config=".Length))
      ElseIf sParam.ToLower.StartsWith("-config=") Then
        gnNumConfig = CInt(sParam.Remove(0, "-config=".Length))
      End If
    Next sParam
  End Sub

  Public Function LlegirConfiguracio(ByVal niNumConfig As Integer) As Configuracio
    Dim sTemp As String = ""
    Dim MyConfig As Configuracio
    Dim CMyRegConfig As New CRegConfig(My.Application.Info.Title, "Config", niNumConfig)
    Dim sAux As String
    Dim asAux() As String

    MyConfig = New Configuracio
    MyConfig.Inicialitzar()

    'Netowork interface
    MyConfig.SelectedInterface = CStr(CMyRegConfig.ReadValue("SelectedInterface", "", CRegConfig.eBrancaReg.brBrancaSistema, False))

    'Paràmetres de hosts
    MyConfig.Hosts = New TrackingHosts()
    sAux = CStr(CMyRegConfig.ReadValue("NewHosts", "", CRegConfig.eBrancaReg.brBrancaSistema, False))
    DesserializeObjectFromString(sAux, MyConfig.Hosts, False)

    'Paràmetres de routing
    gTrackingRouterManager = New TrackingRouter.TrackingRouterManager
    sAux = CStr(CMyRegConfig.ReadValue("TrackingRouterManager", "", CRegConfig.eBrancaReg.brBrancaSistema, False))
    DesserializeObjectFromString(sAux, gTrackingRouterManager, False)
    gTrackingRouterManager.CPuTrackingFile = gTrackingFile

    _gsSniffer = New GSSniffer()
    _gsSniffer.StreamsPath = "C:\GELO\"
    _gsSniffer.TrackingFile = gTrackingFile


    'Paràmetres debug
    MyConfig.LogarErrorsAFitxer = CBool(CMyRegConfig.ReadValue("LogarErrorsAFitxer", True, CRegConfig.eBrancaReg.brBrancaSistema, False))
    MyConfig.VeureFinestraErrors = CBool(CMyRegConfig.ReadValue("VeureFinestraErrors", True, CRegConfig.eBrancaReg.brBrancaSistema, False))

    MyConfig.LogFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
    MyConfig.LogFolder = System.IO.Path.Combine(MyConfig.LogFolder, String.Format("Gelosoft\{0}", My.Application.Info.Title))
    If Not System.IO.Directory.Exists(MyConfig.LogFolder) Then
      System.IO.Directory.CreateDirectory(MyConfig.LogFolder)
    End If

    Debug.Print(MyConfig.LogFolder)
    Return MyConfig
    MyConfig = Nothing
    CMyRegConfig = Nothing
  End Function

  Public Sub DesarConfiguracio(ByVal niNumConfig As Integer, ByVal tiConfig As Configuracio)
    Dim CMyRegConfig As New CRegConfig(My.Application.Info.Title, "Config", niNumConfig)

    'Network interface
    CMyRegConfig.WriteValue("SelectedInterface", tiConfig.SelectedInterface, CRegConfig.eBrancaReg.brBrancaSistema, Microsoft.Win32.RegistryValueKind.String, False)


    'Paràmetres hosts
    CMyRegConfig.WriteValue("UseHosts", tiConfig.UseHosts, CRegConfig.eBrancaReg.brBrancaSistema, Microsoft.Win32.RegistryValueKind.DWord, False)

    Dim sAux As String = SerializeObjectToString(tiConfig.Hosts, False)
    CMyRegConfig.WriteValue("NewHosts", sAux, CRegConfig.eBrancaReg.brBrancaSistema, Microsoft.Win32.RegistryValueKind.String, False)

    'Paràmetres de routing
    sAux = SerializeObjectToString(gTrackingRouterManager, False)
    CMyRegConfig.WriteValue("TrackingRouterManager", sAux, CRegConfig.eBrancaReg.brBrancaSistema, Microsoft.Win32.RegistryValueKind.String, False)

    'Paràmetres debug
    CMyRegConfig.WriteValue("LogarErrorsAFitxer", tiConfig.LogarErrorsAFitxer, CRegConfig.eBrancaReg.brBrancaSistema, Microsoft.Win32.RegistryValueKind.DWord, False)
    CMyRegConfig.WriteValue("VeureFinestraErrors", tiConfig.VeureFinestraErrors, CRegConfig.eBrancaReg.brBrancaSistema, Microsoft.Win32.RegistryValueKind.DWord, False)

    CMyRegConfig = Nothing
  End Sub


  Public Sub MostrarOpcions(ByVal iForm As System.Windows.Forms.IWin32Window)
    Try
      Dim fConfig As DialogOpcions

      fConfig = New DialogOpcions
      fConfig.tPuConfiguracio = gudtCnfg
      If iForm Is Nothing Then
        fConfig.ShowInTaskbar = True
      Else
        fConfig.ShowInTaskbar = False
      End If
      fConfig.ShowDialog(iForm)
      If fConfig.DialogResult = System.Windows.Forms.DialogResult.OK Then
        gudtCnfg = fConfig.tPuConfiguracio
        DesarConfiguracio(gnNumConfig, gudtCnfg)
      End If

    Catch ex As Exception
      'AppErrors.Errors.LogError(AppErrors.Errors.ErrorsMotor.ErrorVB, "MMain", "MostrarOpcions", TraceEventType.Error, ex.ToString)
    End Try
  End Sub
End Module
