Imports Microsoft.Win32
Public Class CRegConfig
  Private Const REGROOTPATH As String = "Software\Gelosoft\"
  Private Const PREFIXCONFIGDEF As String = "Config"
  Private Const NOM_MODUL As String = "CRegConfig" 'Nom modul per rutina errors
  Public Enum eErrorsA3Config
    errBaseA3Config = 10020
    errPathRegInvalid
    errParamConfigInvalits
    errWriteValue
    errDeleteValue
    errTipusInvalid
    errGenSubKey
  End Enum
  Public Enum eBrancaReg  'Per triar si volem llegir o esciure a la branca de sistema o d'usuari
    brBrancaSistema
    brBrancaUsuari
  End Enum

  Private sPiRegPathSistema As String   'Path de sistema(hklm)
  Private sPiRegPathUsuari As String    'Path d'usuari (hkcu)
  Private sPiNomAplicacio As String
  Private nPiNumConfig As Integer       'Número de configuració
  Private sPiPrefixConfig As String      'normalment "Config"
  Public Sub New()
    ' Inicialitza el paths on anira a buscar o desar la configuracio.   
    sPiPrefixConfig = PREFIXCONFIGDEF
    sPiNomAplicacio = My.Application.Info.Title 'Valor per defecte de l'aplicació
    nPiNumConfig = 0              'Número de configuració per defecte
  End Sub
  Public Sub New(ByVal siNomAplicacio As String, ByVal siPrefixConfig As String, ByVal niNumConfig As Integer)
    sPiPrefixConfig = siPrefixConfig
    sPiNomAplicacio = siNomAplicacio
    nPiNumConfig = niNumConfig
  End Sub
  Property Aplicacio() As String
    Get
      Aplicacio = sPiNomAplicacio
    End Get
    Set(ByVal value As String)
      sPiNomAplicacio = Trim$(value)
    End Set
  End Property
  Property NumConfig() As Integer
    Get
      NumConfig = nPiNumConfig
    End Get
    Set(ByVal value As Integer)
      nPiNumConfig = value
    End Set
  End Property
  Property PrefixConfig() As String
    Get
      PrefixConfig = sPiPrefixConfig
    End Get
    Set(ByVal value As String)
      sPiPrefixConfig = Trim$(value)
    End Set
  End Property
  Public Sub WriteSubKeyValue(ByVal siSubKey As String, ByVal siClau As String, ByVal viValor As Object, ByVal eiBranca As eBrancaReg, _
    ByVal eiTipusReg As RegistryValueKind, ByVal biXifrat As Boolean)
    Dim vValor As Object
    Dim Clau As RegistryKey
    If biXifrat Then 'Encriptem Valor
      vValor = Enxifra(CStr(viValor), UCase$(Trim$(siClau)))
    Else
      vValor = viValor
    End If
    'Arreglem format de les dades. cal passar-ho com a string     
    Clau = GetRootKey(eiBranca, siSubKey, True, True)
    Clau.SetValue(siClau, viValor, eiTipusReg)
    Clau.Close()
    Clau = Nothing
  End Sub
  Public Sub WriteValue(ByVal siClau As String, ByVal viValor As Object, ByVal eiBranca As eBrancaReg, _
  ByVal eiTipusReg As RegistryValueKind, ByVal biXifrat As Boolean)
    WriteSubKeyValue("", siClau, viValor, eiBranca, eiTipusReg, biXifrat)
  End Sub
  Public Function ReadSubKeyValue(ByVal siSubKey As String, ByVal siValue As String, ByVal viDefault As Object, ByVal eBranca As eBrancaReg, _
  ByVal biXifrat As Boolean) As Object
    Dim vValor As Object
    Dim Clau As RegistryKey

    Clau = GetRootKey(eBranca, sisubkey, False, False)
    If Not Clau Is Nothing Then
      vValor = Clau.GetValue(siValue)
      Clau.Close()
      Clau = Nothing
      If vValor Is Nothing Then
        'Si la clau no existeix retorna un nothing
        Return (viDefault)
      Else
        If biXifrat Then
          vValor = Desxifra(CStr(vValor), UCase$(Trim$(siValue)))
        End If
      End If
      Return vValor
    Else
      Return viDefault
    End If
  End Function
  Public Function ReadValue(ByVal siClau As String, ByVal viDefault As Object, ByVal eBranca As eBrancaReg, _
  ByVal biXifrat As Boolean) As Object
    Return ReadSubKeyValue("", siClau, viDefault, eBranca, biXifrat)
  End Function
  Public Sub DeleteSubKeyValue(ByVal siSubKey As String, ByVal siValue As String, ByVal eBranca As eBrancaReg)
    'Elimina una clau de la configuració.
    Dim Clau As RegistryKey
    Clau = GetRootKey(eBranca, siSubKey, True, False) '& "\" ' per forçar que esborri la clau          
    If Not Clau Is Nothing Then
      Clau.DeleteValue(siValue)
      Clau.Close()
      Clau = Nothing
    End If
  End Sub
  Public Sub DeleteValue(ByVal siClau As String, ByVal eBranca As eBrancaReg)
    DeleteSubKeyValue("", siClau, eBranca)
  End Sub
  Private Function GetRootKey(ByVal eiBranca As eBrancaReg, ByVal siSubKey As String, ByVal biWritable As Boolean, ByVal biCreate As Boolean) As RegistryKey
    Dim sKey As String
    Dim RegKey As RegistryKey
    sKey = Concat(REGROOTPATH, "\", sPiNomAplicacio)
    If Not sPiPrefixConfig.Length = 0 Then
      sKey = Concat(sKey, "\", sPiPrefixConfig & nPiNumConfig)
    End If
    If siSubKey.Trim.Length > 0 Then
      sKey = Concat(sKey, "\", siSubKey)
    End If
    Select Case eiBranca
      Case eBrancaReg.brBrancaSistema
        RegKey = Registry.LocalMachine.OpenSubKey(sKey, biWritable)
        If RegKey Is Nothing AndAlso biCreate Then
          RegKey = Registry.LocalMachine.CreateSubKey(sKey)
        End If
      Case Else
        RegKey = Registry.CurrentUser.OpenSubKey(sKey, biWritable)
        If RegKey Is Nothing AndAlso biCreate Then
          RegKey = Registry.CurrentUser.CreateSubKey(sKey)
        End If
    End Select
    GetRootKey = RegKey
    RegKey = Nothing
  End Function
  Private Function Concat(ByVal si1 As String, ByVal siSep As String, _
    ByVal si2 As String) As String
    If si2.StartsWith(siSep) Then
      si2.Remove(0, 1)
    End If
    If si1.EndsWith(siSep) Then
      Concat = si1 & si2
    Else
      Concat = si1 & siSep & si2
    End If
  End Function
  Public Function GetSubKeyValueNames(ByVal siKey As String, ByVal eiBranca As eBrancaReg) As String()
    Dim Clau As RegistryKey
    Dim sValueNames As String()
    Clau = GetRootKey(eiBranca, siKey, False, False)
    If Not Clau Is Nothing Then
      sValueNames = Clau.GetValueNames()
    Else
      sValueNames = Nothing
    End If
    Clau.Close()
    Clau = Nothing
    Return sValueNames
  End Function
  Public Function GetSubKeyKeyNames(ByVal siKey As String, ByVal eiBranca As eBrancaReg) As String()
    Dim Clau As RegistryKey
    Dim sSubKeyNames As String()
    Clau = GetRootKey(eBrancaReg.brBrancaSistema, siKey, False, False)
    If Not Clau Is Nothing Then
      sSubKeyNames = Clau.GetSubKeyNames()
    Else
      sSubKeyNames = Nothing
    End If
    Clau.Close()
    Clau = Nothing
    Return sSubKeyNames
  End Function
  Private Function Enxifra(ByVal siStr As String, _
      Optional ByVal siClau As String = "") As String
    'S'usa per xifrar els passwords que es guardin al registre.
    'Per ara, fa un xor entre pClau i pStr, en cada caracter, i converteix el
    'resultat en hexadecimal.
    Dim sAsterisc As String
    Dim sOut As String = ""
    Dim nNum As Integer
    Dim nI As Integer
    Dim sClau As String
    Dim sStr As String
    Dim nLast As Integer
    sClau = "-" & siClau
    sStr = HexN(Len(siStr), 4) & siStr
    If Len(sStr) < 20 Then
      sAsterisc = New String("*"c, 20)
      sStr = Left$(sStr & sAsterisc, 20)
    End If
    While Len(sClau) <= Len(sStr)
      sClau = sClau & sClau
    End While
    nLast = 85
    For nI = 1 To Len(sStr)
      nNum = (Asc(Mid$(sStr, nI, 1)) Xor Asc(Mid$(sClau, nI, 1))) Xor nLast Xor 255
      nLast = nNum
      sOut = sOut & HexN(nNum, 2)
    Next
    Enxifra = sOut
  End Function
  Private Function Desxifra(ByVal siStr As String, Optional ByVal siClau As String = "") As String
    Dim nNum As Integer
    Dim nLast As Integer
    Dim sClau As String
    Dim sStr As String = ""
    Dim nI As Integer
    Dim nH As Integer
    Dim nLen As Integer

    sClau = "-" & siClau

    While Len(sClau) <= Len(siStr) / 2
      sClau = sClau & sClau
    End While

    nLast = 85
    For nI = 1 To siStr.Length \ 2
      nH = CInt(Val("&H" & Mid$(siStr, (nI * 2) - 1, 2)))
      nNum = nH Xor Asc(Mid$(sClau, nI, 1)) Xor nLast Xor 255
      nLast = nH
      sStr = sStr & Chr(nNum)
    Next nI

    nLen = CInt(Val("&H" & Left$(sStr, 4)))
    Desxifra = Mid$(sStr, 5, nLen)
  End Function
  Private Function HexN(ByVal niNum As Integer, ByVal niDigits As Integer) As String
    ''Converteix un número en una cadena hexadecimal dels digits
    ''especificats, omplint amb 0.
    Dim sString As String
    sString = New String("0"c, niDigits)
    HexN = Right$(sString & Trim$(Hex$(niNum)), niDigits)
  End Function
End Class

