<Serializable()> Public Class TrackingFile
  Public Path As String
  Public FrameCount As Integer
  Public FrameRate As Integer
  Public Channels As New List(Of String)
  Public TrackingValues As New List(Of TrackingValue)
  Public Binary As Boolean
  Public FullString As String

  'Public TrackingSources As New Hashtable()
  Public TrackingSources As New List(Of TrackingSource)

  Public SelectedPorts As New List(Of Integer)
  Public SelectedSources As New List(Of TrackingSource)
  Public SelectedValues As New List(Of TrackingValue)

  Public TickTimeGlobalOffset As Double
  Public InitTickTimeMS As Double
  Public LastTickTimeMS As Double
  'Public SelectedSources As New (Of TrackingSource)

  Public Event SelectionUpdated()
  Public Event NewTrackingValue(ByVal CiTrackingSource As TrackingSource, ByVal CiValue As TrackingValue)
  Public Event SourcesUpdated()


  Public Sub ToggleSelection(ByVal niPort As Integer, ByVal biValue As Boolean)
    Try
      If biValue = False Then
        If Me.SelectedPorts.Contains(niPort) Then
          Me.SelectedPorts.Remove(niPort)

          UpdateSelectedValueList()
        End If
      Else
        If Not Me.SelectedPorts.Contains(niPort) Then
          Me.SelectedPorts.Add(niPort)
          UpdateSelectedValueList()
        End If
      End If
      RaiseEvent SelectionUpdated()
    Catch ex As Exception

    End Try
  End Sub

  Private Sub UpdateSelectedValueList()
    Try
      Dim CAux As TrackingSource
      SelectedPorts.Clear()
      SelectedSources.Clear()
      For Each CAux In Me.TrackingSources
        If CAux.Selected Then
          SelectedPorts.Add(CAux.Port)
          SelectedSources.Add(CAux)
        End If
      Next
      'recòrrer tots, afegint els seleccionats
      Me.SelectedValues.Clear()
      For Each CValue As TrackingValue In Me.TrackingValues
        If SelectedPorts.Contains(CValue.PORT) Then
          Me.SelectedValues.Add(CValue)
        End If
      Next
    Catch ex As Exception
    End Try
  End Sub

  Public Sub AddTrackingValue(ByVal CiValue As TrackingValue)
    Try
      Dim CTrackingSource As TrackingSource

      If InitTickTimeMS = 0 Then
        'Obtenir l'offset per que el tickcount tingui sentit
        Dim fAux As Double = (((Now.Hour) * 60 + Now.Minute) * 60 + Now.Second) * 1000 + Now.Millisecond
        TickTimeGlobalOffset = System.Environment.TickCount - fAux
        'Obtenir el ms del primer tickcount
        InitTickTimeMS = System.Environment.TickCount - TickTimeGlobalOffset
      End If
      CiValue.OffsetMS = InitTickTimeMS
      CiValue.CapturedMS = System.Environment.TickCount - TickTimeGlobalOffset

      If CiValue.CapturedMS > LastTickTimeMS Then
        LastTickTimeMS = CiValue.CapturedMS
      End If

      CTrackingSource = Me.TrackingSourceByPort(CiValue.PORT)
      If CTrackingSource Is Nothing Then
        CTrackingSource = New TrackingSource

        CTrackingSource.Port = CiValue.PORT
        CTrackingSource.Host = CiValue.HOST
        Dim sAux As String
        Try
          If Not CiValue.HOST Is Nothing AndAlso CiValue.HOST <> "" Then
            sAux = gHostEntries.GetDNSHostEntry(CiValue.HOST)
          Else
            sAux = "File"
          End If

          CTrackingSource.Host = sAux


          CTrackingSource.TrackingHost = New TrackingHost()

          CTrackingSource.TrackingHost.Host = CTrackingSource.Host
          CTrackingSource.TrackingHost.IP = CiValue.HOST
          CTrackingSource.TrackingHost.SourcePort = CiValue.PORT
          CTrackingSource.TrackingHost.TrackingType = eTrackingType.UDP_ORAD
        Catch ex As Exception

        End Try

        CTrackingSource.Selected = False

        TrackingSources.Add(CTrackingSource)
        RaiseEvent SourcesUpdated()
      End If
      Dim bAdd As Boolean = False
      If gudtCnfg.UseHosts = False Then
        bAdd = True
      Else
        'If Not gudtCnfg.Hosts.GetHost(CTrackingSource.Host, CTrackingSource.Port) Is Nothing Or CTrackingSource.Host = "File" Then
        bAdd = True
        'End If
      End If

      If bAdd Then
        Me.TrackingValues.Add(CiValue)
        CTrackingSource.AddTrackingValue(CiValue)

        If CTrackingSource.Selected Then
          Me.SelectedValues.Add(CiValue)
        End If

        RaiseEvent NewTrackingValue(CTrackingSource, CiValue)
      End If

    Catch ex As Exception
    End Try
  End Sub

  Public Sub Clear()
    Try
      Me.TrackingValues.Clear()
      Me.TrackingSources.Clear()
      Me.SelectedPorts.Clear()
      Me.SelectedSources.Clear()
      Me.SelectedValues.Clear()
      RaiseEvent SourcesUpdated()
      RaiseEvent SelectionUpdated()
    Catch ex As Exception

    End Try
  End Sub

#Region "Get tracking source"
  Public Function TrackingSourceByPort(ByVal niPort As Integer) As TrackingSource
    Dim CRes As TrackingSource = Nothing
    Try
      For Each CAux As TrackingSource In Me.TrackingSources
        If CAux.Port = niPort Then
          CRes = CAux
        End If
      Next

    Catch ex As Exception

    End Try
    Return CRes
  End Function

  Public Function TrackingValuesByPort(ByVal niPort As Integer) As List(Of TrackingValue)
    Dim CRes As List(Of TrackingValue) = Nothing
    Try
      Select Case niPort
        Case -1 'tots
          CRes = Me.TrackingValues
        Case 0 'els seleccionats
          'quins estan seleccionats?
          If Me.SelectedValues.Count <> 0 Then
            CRes = Me.SelectedValues
          Else
            CRes = Me.TrackingValues
          End If

        Case Else 'el que ens demanin
          For Each CAux As TrackingSource In Me.TrackingSources
            If CAux.Port = niPort Then
              CRes = CAux.TrackingValues
            End If
          Next

      End Select

    Catch ex As Exception

    End Try
    Return CRes
  End Function

  Public Function GetTrackingSource(ByVal CiHost As TrackingHost) As TrackingSource
    Dim CRes As TrackingSource = Nothing
    Try
      For Each CTrackingSource As TrackingSource In Me.TrackingSources
        If CTrackingSource.TrackingHost = CiHost Then
          CRes = CTrackingSource
          Exit For
        End If
      Next
    Catch ex As Exception

    End Try
    Return CRes

  End Function
#End Region

#Region "Constructors"
  Public Sub New()
    Try
      Me.Path = ""
      Me.Channels.Clear()
      Me.Channels.Add("POS_X")
      Me.Channels.Add("POS_Y")
      Me.Channels.Add("POS_Z")
      Me.Channels.Add("TARGET_X")
      Me.Channels.Add("TARGET_Y")
      Me.Channels.Add("TARGET_Z")
      Me.Channels.Add("ROLL")
      Me.Channels.Add("FOV")
      Me.Channels.Add("CENTER_H")
      Me.Channels.Add("CENTER_V")
      Me.Channels.Add("ASPECT")
      Me.Channels.Add("IRIS")
      Me.Channels.Add("FOCAL_LEN")
      Me.Channels.Add("FOCAL_PLANE")
      Me.Channels.Add("CAM_NUM")
      Me.Channels.Add("ROT_X")
      Me.Channels.Add("ROT_Y")
      Me.Channels.Add("ROT_Z")
    Catch ex As Exception

    End Try

  End Sub

  Public Sub New(ByVal siFile As String, ByVal biBinaryFile As Boolean)
    Try
      Me.Path = siFile

      If siFile = "" Then
        'no hi ha arxiu, ens l'estem inventant sobre la marxa!
        Me.Channels.Clear()
        Me.Channels.Add("POS_X")
        Me.Channels.Add("POS_Y")
        Me.Channels.Add("POS_Z")
        Me.Channels.Add("TARGET_X")
        Me.Channels.Add("TARGET_Y")
        Me.Channels.Add("TARGET_Z")
        Me.Channels.Add("ROLL")
        Me.Channels.Add("FOV")
        Me.Channels.Add("CENTER_H")
        Me.Channels.Add("CENTER_V")
        Me.Channels.Add("ASPECT")
        Me.Channels.Add("IRIS")
        Me.Channels.Add("FOCAL_LEN")
        Me.Channels.Add("FOCAL_PLANE")
        Me.Channels.Add("CAM_NUM")
        Me.Channels.Add("ROT_X")
        Me.Channels.Add("ROT_Y")
        Me.Channels.Add("ROT_Z")
      Else

        Dim oRead As System.IO.StreamReader
        Dim oBinaryRead As System.IO.BinaryReader
        'Dim oRead As myStreamReader
        Dim bDone As Boolean = False

        'oOriginal = System.IO.File.OpenText(siFile)
        oRead = System.IO.File.OpenText(siFile)
        oBinaryRead = New System.IO.BinaryReader(oRead.BaseStream)

        'oRead = New myStreamReader(oOriginal.BaseStream)
        Dim sAux As String
        Dim asAux() As String
        Me.Binary = biBinaryFile
        'sAux = oRead.ReadLine()
        sAux = Me.ReadLine(oBinaryRead)
        While Not bDone
          If sAux.StartsWith("elset animation 1.0 binary") Then bDone = False 'és la capçalera correcta!
          If sAux.StartsWith("creator ") Then bDone = False 'és el creador
          If sAux.StartsWith("frameRate ") Then
            asAux = sAux.Split(CChar(" "))
            Me.FrameRate = CInt(asAux(1))
          End If
          If sAux.StartsWith("framesCount ") Then
            asAux = sAux.Split(CChar(" "))
            Me.FrameCount = CInt(asAux(1))
          End If
          If sAux.StartsWith("channels ") Then
            asAux = sAux.Split(CChar(" "))
            Me.Channels = New List(Of String)
            For nIndex As Integer = 1 To asAux.Length - 1
              If asAux(nIndex).Trim.Length > 0 Then
                Me.Channels.Add(asAux(nIndex))
              End If
            Next

            If Me.Binary Then
              Me.ReadTrackingValues(oBinaryRead)
            Else
              Me.ReadTrackingValues(oRead)
            End If

          End If

          sAux = Me.ReadLine(oBinaryRead)

          If sAux Is Nothing Then
            bDone = True
          End If

        End While


        oRead.BaseStream.Seek(0, IO.SeekOrigin.Begin)
        Me.FullString = oRead.ReadToEnd()
        oRead.Close()

      End If



    Catch ex As Exception

    End Try
  End Sub
#End Region

#Region "Legacy ORAD path file"
  Private Function ReadLine(ByRef iBinaryReader As System.IO.BinaryReader) As String
    Dim sRes As String = ""
    Dim bDone As Boolean = False
    Dim bCR As Boolean = False
    Dim bLF As Boolean = False
    Dim c As Char
    Try
      c = iBinaryReader.ReadChar()
      bDone = (c = Nothing)
      While Not bDone

        Select Case c
          Case CChar(vbCr)
            bCR = True
          Case CChar(vbLf)
            bLF = True
          Case Else
            sRes = sRes & c
        End Select

        If bCR And bLF Then
          bDone = True
        Else
          c = iBinaryReader.ReadChar()
          bDone = (c = Nothing)
        End If
      End While


    Catch ex As Exception
      sRes = Nothing
    End Try
    Return sRes
  End Function

  Private Sub ReadTrackingValues(ByRef iBinaryReader As System.IO.BinaryReader)
    Dim CTrackingValue As TrackingValue
    Dim c As Double
    Try
      Me.TrackingValues.Clear()

      For nIndex As Integer = 0 To Me.FrameCount - 1
        CTrackingValue = New TrackingValue(nIndex)
        CTrackingValue.PORT = 0
        Me.AddTrackingValue(CTrackingValue)
      Next
      For nChannel As Integer = 0 To Me.Channels.Count - 1
        Select Case Me.Channels(nChannel)
          Case "POS_X"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).POS_X = iBinaryReader.ReadSingle
            Next
          Case "POS_Y"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).POS_Y = iBinaryReader.ReadSingle
            Next
          Case "POS_Z"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).POS_Z = iBinaryReader.ReadSingle
            Next
          Case "TARGET_X"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).TARGET_X = iBinaryReader.ReadSingle
            Next
          Case "TARGET_Y"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).TARGET_Y = iBinaryReader.ReadSingle
            Next
          Case "TARGET_Z"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).TARGET_Z = iBinaryReader.ReadSingle
            Next
          Case "ROLL"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).ROLL = iBinaryReader.ReadSingle
            Next
          Case "FOV"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).FOV = iBinaryReader.ReadSingle
            Next
          Case "CENTER_H"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              c = iBinaryReader.ReadSingle
              Me.TrackingValues(nIndex).CENTER_H = iBinaryReader.ReadSingle
            Next
          Case "CENTER_V"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              'Me.TrackingValues(nIndex).CENTER_V = iBinaryReader.ReadSingle
            Next
          Case "ASPECT"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).ASPECT = iBinaryReader.ReadSingle
            Next
          Case "IRIS"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).IRIS = iBinaryReader.ReadSingle
            Next
          Case "FOCAL_LEN"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).FOCAL_LEN = iBinaryReader.ReadSingle
            Next
          Case "FOCAL_PLANE"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).FOCAL_PLANE = iBinaryReader.ReadSingle
            Next
          Case "CAM_NUM"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).CAM_NUM = iBinaryReader.ReadInt32
            Next
          Case "TIMECODE"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              Me.TrackingValues(nIndex).TIMECODE = CStr(iBinaryReader.ReadInt32)
            Next
          Case "COUNTER"
            For nIndex As Integer = 0 To Me.FrameCount - 1
              'Me.TrackingValues(nIndex).COUNTER = iBinaryReader.ReadInt32
            Next


            'TARGET_X TARGET_Y TARGET_Z ROLL FOV CENTER_H CENTER_V ASPECT IRIS FOCAL_LEN FOCAL_PLANE CAM_NUM TIMECODE COUNTER 
        End Select
      Next

      For nIndex As Integer = 0 To Me.FrameCount - 1
        Me.TrackingValues(nIndex).UpdateRadianValues()
      Next


    Catch ex As Exception
      MsgBox(ex.ToString)
    End Try
  End Sub

  Private Sub ReadTrackingValues(ByVal buffer() As Char)
    'binary
    Dim myBuffer(Me.Channels.Count * 4) As Char
    Dim byteBuffer(buffer.Length * 2) As Byte
    Dim sAux As String
    Dim nAux As Integer
    Try
      Dim L As Long
      Dim D As Double
      Dim S As Single

      Me.TrackingValues.Clear()
      Dim bBuffer(8) As Byte

      System.Buffer.BlockCopy(buffer, 0, byteBuffer, 0, buffer.Length)

      D = 0
      Dim vOut As Byte() = BitConverter.GetBytes(D)
      'BitConverter.ToChar (
      For nIndex As Integer = 0 To vOut.Length - 1
        Debug.Print(Hex(vOut(nIndex)))
      Next

      For nIndex As Integer = 0 To byteBuffer.Length - 8 Step 8

        L = BitConverter.ToInt64(byteBuffer, nIndex)
        D = BitConverter.ToDouble(byteBuffer, nIndex)
        S = BitConverter.ToSingle(byteBuffer, nIndex)
        For nDigit As Integer = 0 To 7
          bBuffer(nDigit) = byteBuffer(nIndex + (7 - nDigit))
        Next
        L = BitConverter.ToInt64(bBuffer, 0)
        D = BitConverter.ToDouble(bBuffer, 0)
        S = BitConverter.ToSingle(bBuffer, 0)
      Next

      For nIndex As Integer = 0 To CInt(buffer.Length / Me.Channels.Count)
        System.Buffer.BlockCopy(buffer, nIndex * Me.Channels.Count * 4, myBuffer, 0, 2 * Me.Channels.Count * 4)
        sAux = myBuffer(0) & myBuffer(1) & myBuffer(2) & myBuffer(3)
        Integer.TryParse(sAux, nAux)

        L = BitConverter.ToInt64(byteBuffer, 0)
        D = BitConverter.ToDouble(byteBuffer, 0)

      Next

    Catch ex As Exception

    End Try
  End Sub

  Private Sub ReadTrackingValues(ByVal CiStream As System.IO.StreamReader)
    'stream
    Try

      Dim sLine As String
      Dim bDone As Boolean

      sLine = CiStream.ReadLine
      bDone = sLine Is Nothing
      While Not bDone
        If sLine = "#" Then
          bDone = True
        Else
          Me.TrackingValues.Add(New TrackingValue(sLine, Me.Channels))
          If CiStream.EndOfStream Then
            bDone = True
          Else
            sLine = CiStream.ReadLine
            bDone = sLine Is Nothing
          End If
        End If

      End While
    Catch ex As Exception

    End Try
  End Sub

  Public Function GenerateBinaryCode() As String
    Dim sRes As String = ""
    Dim B() As Byte
    Dim C As Char
    Dim str As String
    Dim enc As New System.Text.UTF8Encoding()

    Try
      For nIndex As Integer = 0 To Me.TrackingValues.Count - 1
        With Me.TrackingValues(nIndex)
          B = BitConverter.GetBytes(.POS_X)
          C = BitConverter.ToChar(B, 0)
          str = System.Text.Encoding.Unicode.GetString(BitConverter.GetBytes(.POS_X))
          str = BitConverter.ToString(BitConverter.GetBytes(.POS_X))
          'str = Conversion.Oct(.POS_X)
          sRes = sRes & .POS_X & " "
          sRes = sRes & .POS_Y & " "
          sRes = sRes & .POS_Z & " " & vbCrLf
          'sRes = sRes & BitConverter.ToString(BitConverter.GetBytes(.POS_Y)) & " "
          'sRes = sRes & BitConverter.ToString(BitConverter.GetBytes(.POS_Z)) & " "
          'sRes = sRes & System.Text.Encoding.Unicode.GetString(BitConverter.GetBytes(.POS_X)) & " "
          'sRes = sRes & System.Text.Encoding.Unicode.GetString(BitConverter.GetBytes(.POS_Y)) & " "
          'sRes = sRes & System.Text.Encoding.Unicode.GetString(BitConverter.GetBytes(.POS_Z)) & " "

          Dim chars() As Char = "agbarnet".ToCharArray
          'Dim integers() As Integer = Array.ConvertAll(chars ,  New Converter(Of Char, Integer)(Function(c) Asc(c))) 


        End With
      Next
    Catch ex As Exception

    End Try
    Return sRes
  End Function
#End Region
End Class
