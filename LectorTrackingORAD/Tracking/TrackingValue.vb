Imports System.io

<Serializable()> Public Class TrackingValue
  Public POS_X, POS_Y, POS_Z, TARGET_X, TARGET_Y, TARGET_Z, ROLL, FOV, CENTER_H, CENTER_V, ASPECT, IRIS, FOCAL_LEN, FOCAL_PLANE As Single
  Public ROT_X, ROT_Y, ROT_Z, ROT_ROLL, FOV_RAD As Single
  Public CAM_NUM As Integer
  Public CapturedMS As Double
  Public OffsetMS As Double
  Public TIMECODE As String
  Public COUNTER As Integer
  Public PORT As Integer
  Public HOST As String
  Public IP As String

  Public buffer(107) As Byte
  Public Index As Integer


  Public Sub UpdateRadianValues()
    Try
      Me.FOV_RAD = CSng(Math.PI * Me.FOV / 180)

      Dim fAuxX As Double = Me.POS_X - Me.TARGET_X
      Dim fAuxY As Double = Me.POS_Y - Me.TARGET_Y
      Dim fAuxZ As Double = Me.POS_Z - Me.TARGET_Z

      'Debug.Print(Me.Frame)
      'Debug.Print(Me.Valid)

      Me.ROT_X = CSng(Math.Atan2(fAuxY, CDbl(IIf(fAuxZ <> 0, fAuxZ, 0.000000001))))
      Me.ROT_Y = CSng(Math.Atan2(fAuxX, CDbl(IIf(fAuxZ <> 0, fAuxZ, 0.000000001))))
      Me.ROT_Z = CSng(Math.Atan2(fAuxX, CDbl(IIf(fAuxY <> 0, fAuxY, 0.000000001))))
      Me.ROT_Z = Me.ROLL

      'Dim Axis As New _Vector
      'Dim Angle As Double = 0

      'Convert_Camera_Model(VNew(Me.POS_X, Me.POS_Y, Me.POS_Z), _
      '  VNew(Me.TARGET_X, Me.TARGET_Y, Me.TARGET_Z), _
      '  VNew(0, 1, 0), _
      '  Axis, Angle)

      ''Me.ROT_X = Math.PI * Axis.x / 2
      ''Me.ROT_Y = Math.PI * Axis.y / 2
      ''Me.ROT_Z = Math.PI * Axis.z / 2
      'Me.ROLL = Math.PI * Angle / 2



    Catch ex As Exception

    End Try
  End Sub

  Public Sub New()
    Try
      Me.COUNTER = 0
      Me.CapturedMS = 0
    Catch ex As Exception
    End Try
  End Sub

  Public Sub New(ByVal niIndex As Integer)
    Try
      Me.COUNTER = niIndex
      Me.CapturedMS = System.Environment.TickCount
    Catch ex As Exception
    End Try
  End Sub

  Public Sub New(ByVal udpHeader As IpComm.UDPHeader)
    Me.FromBuffer(udpHeader.Data, eTrackingType.UDP_ORAD)
    Me.IP = udpHeader.Source.ToString
    Me.HOST = gHostEntries.GetDNSHostEntry(udpHeader.Source.ToString)
    Me.PORT = udpHeader.SourcePort
  End Sub

  Public Sub New(ByVal tcpHeader As IpComm.TCPHeader)
    Me.FromBuffer(tcpHeader.Data, eTrackingType.TCP_Client)
    Me.IP = ""
    Me.HOST = ""
    Me.PORT = tcpHeader.SourcePort
  End Sub

#Region "Buffer read"
  Public Function FromBuffer(ByRef buffer() As Byte, ByVal eiTrackingType As eTrackingType) As Boolean
    Dim bRes As Boolean = False
    Select Case eiTrackingType
      Case eTrackingType.UDP_ORAD
        bRes = FromBuffer_UDP_ORAD(buffer)
      Case eTrackingType.UDP
        bRes = FromBuffer_UDP(buffer)
      Case eTrackingType.TCP_Client
        bRes = FromBuffer_TCP(buffer)
    End Select
    Return bRes
  End Function

  Private Function FromBuffer_UDP_ORAD(ByRef buffer() As Byte) As Boolean
    Dim bRes As Boolean = False
    Dim nOffsetBytes As Integer = 24
    Try
      Me.buffer = buffer
      Me.COUNTER = 0
      Dim oReader As New BinaryReader(New MemoryStream(buffer))

      Dim bDone As Boolean = False
      Dim nAux As Integer
      Dim fAux As Single

      If nOffsetBytes = -1 Then 'ho fem automàtic'comencem amb 0?
        nAux = oReader.PeekChar()
        If nAux = 0 Then
          'comencem amb 0's!!
          While nAux = 0
            nAux = oReader.PeekChar
            If nAux = 0 Then nAux = oReader.ReadInt32
          End While
        End If

        'tenim 4294967295?
        If nAux = 4294967295 Then
          'ens treiem de sobre els caràcters rarus!
          While nAux = 4294967295
            nAux = oReader.PeekChar
            If nAux = 4294967295 Then nAux = oReader.ReadInt32
          End While
        End If
      Else
        oReader.ReadBytes(nOffsetBytes)
      End If
      If oReader.BaseStream.Length > 104 Then
        Me.POS_X = oReader.ReadSingle
        Me.POS_Y = oReader.ReadSingle
        Me.POS_Z = oReader.ReadSingle
        Me.TARGET_X = oReader.ReadSingle
        Me.TARGET_Y = oReader.ReadSingle
        Me.TARGET_Z = oReader.ReadSingle
        'està repetit, no sé per què

        Me.POS_X = oReader.ReadSingle
        Me.POS_Y = oReader.ReadSingle
        Me.POS_Z = oReader.ReadSingle
        Me.TARGET_X = oReader.ReadSingle
        Me.TARGET_Y = oReader.ReadSingle
        Me.TARGET_Z = oReader.ReadSingle

        fAux = oReader.ReadSingle
        Me.FOV_RAD = oReader.ReadSingle
        Me.FOV = CSng(180 * Me.FOV_RAD / Math.PI)

        Me.ASPECT = oReader.ReadSingle
        fAux = oReader.ReadSingle
        Me.FOCAL_PLANE = oReader.ReadSingle
        Me.FOCAL_LEN = oReader.ReadSingle
        Me.CENTER_H = oReader.ReadSingle
        Me.CENTER_V = oReader.ReadSingle

        If Me.Valid Then
          Me.UpdateRadianValues()
        End If

        bRes = True ' Me.Valid
      Else
        bRes = False
      End If
    Catch ex As Exception

    End Try
    Return bRes
  End Function

  Private Function FromBuffer_UDP(ByRef buffer() As Byte) As Boolean
    Dim bRes As Boolean = False
    Dim nOffsetBytes As Integer = 24
    Try
      Me.buffer = buffer
      Me.COUNTER = 0
      Dim oReader As New BinaryReader(New MemoryStream(buffer))

      Dim bDone As Boolean = False
      Dim nAux As Integer
      Dim fAux As Single

      If nOffsetBytes = -1 Then 'ho fem automàtic'comencem amb 0?
        nAux = oReader.PeekChar()
        If nAux = 0 Then
          'comencem amb 0's!!
          While nAux = 0
            nAux = oReader.PeekChar
            If nAux = 0 Then nAux = oReader.ReadInt32
          End While
        End If

        'tenim 4294967295?
        If nAux = 4294967295 Then
          'ens treiem de sobre els caràcters rarus!
          While nAux = 4294967295
            nAux = oReader.PeekChar
            If nAux = 4294967295 Then nAux = oReader.ReadInt32
          End While
        End If
      Else
        oReader.ReadBytes(nOffsetBytes)
      End If

      Me.POS_X = oReader.ReadSingle
      Me.POS_Y = oReader.ReadSingle
      Me.POS_Z = oReader.ReadSingle
      Me.TARGET_X = oReader.ReadSingle
      Me.TARGET_Y = oReader.ReadSingle
      Me.TARGET_Z = oReader.ReadSingle
      'està repetit, no sé per què

      Me.POS_X = oReader.ReadSingle
      Me.POS_Y = oReader.ReadSingle
      Me.POS_Z = oReader.ReadSingle
      Me.TARGET_X = oReader.ReadSingle
      Me.TARGET_Y = oReader.ReadSingle
      Me.TARGET_Z = oReader.ReadSingle

      fAux = oReader.ReadSingle
      Me.FOV_RAD = oReader.ReadSingle
      Me.FOV = CSng(180 * Me.FOV_RAD / Math.PI)

      Me.ASPECT = oReader.ReadSingle
      fAux = oReader.ReadSingle
      Me.FOCAL_PLANE = oReader.ReadSingle
      Me.FOCAL_LEN = oReader.ReadSingle
      Me.CENTER_H = oReader.ReadSingle
      Me.CENTER_V = oReader.ReadSingle

      If Me.Valid Then
        Me.UpdateRadianValues()
      End If


      bRes = True ' Me.Valid
    Catch ex As Exception

    End Try
    Return bRes
  End Function

  Private Function FromBuffer_TCP(ByRef buffer() As Byte) As Boolean
    Dim bRes As Boolean = False
    Dim nOffsetBytes As Integer = 24
    Try
      Me.buffer = buffer
      Me.COUNTER = 0
      Dim oReader As New BinaryReader(New MemoryStream(buffer))

      Dim bDone As Boolean = False
      Dim nAux As Integer
      Dim fAux As Single

      If nOffsetBytes = -1 Then 'ho fem automàtic'comencem amb 0?
        nAux = oReader.PeekChar()
        If nAux = 0 Then
          'comencem amb 0's!!
          While nAux = 0
            nAux = oReader.PeekChar
            If nAux = 0 Then nAux = oReader.ReadInt32
          End While
        End If

        'tenim 4294967295?
        If nAux = 4294967295 Then
          'ens treiem de sobre els caràcters rarus!
          While nAux = 4294967295
            nAux = oReader.PeekChar
            If nAux = 4294967295 Then nAux = oReader.ReadInt32
          End While
        End If
      Else
        oReader.ReadBytes(nOffsetBytes)
      End If

      Me.POS_X = oReader.ReadSingle
      Me.POS_Y = oReader.ReadSingle
      Me.POS_Z = oReader.ReadSingle
      Me.TARGET_X = oReader.ReadSingle
      Me.TARGET_Y = oReader.ReadSingle
      Me.TARGET_Z = oReader.ReadSingle
      'està repetit, no sé per què

      Me.POS_X = oReader.ReadSingle
      Me.POS_Y = oReader.ReadSingle
      Me.POS_Z = oReader.ReadSingle
      Me.TARGET_X = oReader.ReadSingle
      Me.TARGET_Y = oReader.ReadSingle
      Me.TARGET_Z = oReader.ReadSingle

      fAux = oReader.ReadSingle
      Me.FOV_RAD = oReader.ReadSingle
      Me.FOV = CSng(180 * Me.FOV_RAD / Math.PI)

      Me.ASPECT = oReader.ReadSingle
      fAux = oReader.ReadSingle
      Me.FOCAL_PLANE = oReader.ReadSingle
      Me.FOCAL_LEN = oReader.ReadSingle
      Me.CENTER_H = oReader.ReadSingle
      Me.CENTER_V = oReader.ReadSingle

      If Me.Valid Then
        Me.UpdateRadianValues()
      End If


      bRes = True ' Me.Valid
    Catch ex As Exception

    End Try
    Return bRes
  End Function
#End Region

  Public Sub New(ByVal siLine As String, ByVal CiChannels As List(Of String))
    Try
      Dim sAux As String = siLine.Replace("frame ", "")
      Dim asAux() As String = sAux.Split(CChar(" "))

      Dim culture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US")
      Dim numInfo As System.Globalization.NumberFormatInfo = culture.NumberFormat

      For nIndex As Integer = 0 To CiChannels.Count - 1

        Select Case CiChannels(nIndex)
          Case "POS_X" : Me.POS_X = Single.Parse(asAux(nIndex), numInfo)
          Case "POS_Y" : Me.POS_Y = Single.Parse(asAux(nIndex), numInfo)
          Case "POS_Z" : Me.POS_Z = Single.Parse(asAux(nIndex), numInfo)
          Case "TARGET_X" : Me.TARGET_X = Single.Parse(asAux(nIndex), numInfo)
          Case "TARGET_Y" : Me.TARGET_Y = Single.Parse(asAux(nIndex), numInfo)
          Case "TARGET_Z" : Me.TARGET_Z = Single.Parse(asAux(nIndex), numInfo)
          Case "ROLL" : Me.ROLL = Single.Parse(asAux(nIndex), numInfo)
          Case "FOV" : Me.FOV = Single.Parse(asAux(nIndex), numInfo)
          Case "CENTER_H" : Me.CENTER_H = Single.Parse(asAux(nIndex), numInfo)
          Case "CENTER_V" : Me.CENTER_V = Single.Parse(asAux(nIndex), numInfo)
          Case "ASPECT" : Me.ASPECT = Single.Parse(asAux(nIndex), numInfo)
          Case "IRIS" : Me.IRIS = Single.Parse(asAux(nIndex), numInfo)
          Case "FOCAL_LEN" : Me.FOCAL_LEN = Single.Parse(asAux(nIndex), numInfo)
          Case "FOCAL_PLANE" : Me.FOCAL_PLANE = Single.Parse(asAux(nIndex), numInfo)
          Case "FOCAL_PLANE" : Me.FOCAL_PLANE = Single.Parse(asAux(nIndex), numInfo)
          Case "CAM_NUM" : Me.CAM_NUM = Integer.Parse(asAux(nIndex), numInfo)
            'Case "TIMECODE" : Me.TIMECODE = Date.Parse(asAux(nIndex), numInfo)
          Case "COUNTER" : Me.COUNTER = Integer.Parse(asAux(nIndex), numInfo)
        End Select

      Next
    Catch ex As Exception
    End Try
  End Sub

  Public Function GetValueByChannelName(ByVal siChannel As String, ByVal CiChannels As List(Of String)) As String
    Dim sRes As String = ""
    Try

      For nIndex As Integer = 0 To CiChannels.Count - 1
        If CiChannels(nIndex) = siChannel Then

          Select Case CiChannels(nIndex)
            Case "POS_X" : sRes = CStr(Me.POS_X)
            Case "POS_Y" : sRes = CStr(Me.POS_Y) '= Single.Parse(asAux(nIndex), numInfo)
            Case "POS_Z" : sRes = CStr(Me.POS_Z) '= Single.Parse(asAux(nIndex), numInfo)
            Case "TARGET_X" : sRes = CStr(Me.TARGET_X) ' = Single.Parse(asAux(nIndex), numInfo)
            Case "TARGET_Y" : sRes = CStr(Me.TARGET_Y) '= Single.Parse(asAux(nIndex), numInfo)
            Case "TARGET_Z" : sRes = CStr(Me.TARGET_Z) '= Single.Parse(asAux(nIndex), numInfo)
            Case "ROLL" : sRes = CStr(Me.ROLL) '= Single.Parse(asAux(nIndex), numInfo)
            Case "FOV" : sRes = CStr(Me.FOV) '= Single.Parse(asAux(nIndex), numInfo)
            Case "CENTER_H" : sRes = CStr(Me.CENTER_H) '= Single.Parse(asAux(nIndex), numInfo)
            Case "CENTER_V" : sRes = CStr(Me.CENTER_V) '= Single.Parse(asAux(nIndex), numInfo)
            Case "ASPECT" : sRes = CStr(Me.ASPECT) '= Single.Parse(asAux(nIndex), numInfo)
            Case "IRIS" : sRes = CStr(Me.IRIS) '= Single.Parse(asAux(nIndex), numInfo)
            Case "FOCAL_LEN" : sRes = CStr(Me.FOCAL_LEN) '= Single.Parse(asAux(nIndex), numInfo)
            Case "FOCAL_PLANE" : sRes = CStr(Me.FOCAL_PLANE) '= Single.Parse(asAux(nIndex), numInfo)
            Case "FOCAL_PLANE" : sRes = CStr(Me.FOCAL_PLANE) '= Single.Parse(asAux(nIndex), numInfo)
            Case "CAM_NUM" : sRes = CStr(Me.CAM_NUM) '= Integer.Parse(asAux(nIndex), numInfo)
              'Case "TIMECODE" : Me.TIMECODE = Date.Parse(asAux(nIndex), numInfo)
            Case "COUNTER" : sRes = CStr(Me.COUNTER) ' = Integer.Parse(asAux(nIndex), numInfo)
            Case "ROT_X" : sRes = CStr(Me.ROT_X) ' = Integer.Parse(asAux(nIndex), numInfo)
            Case "ROT_Y" : sRes = CStr(Me.ROT_Y) ' = Integer.Parse(asAux(nIndex), numInfo)
            Case "ROT_Z" : sRes = CStr(Me.ROT_Z) ' = Integer.Parse(asAux(nIndex), numInfo)


          End Select
        End If
      Next
    Catch ex As Exception

    End Try
    Return sRes
  End Function

  Public Function SetValueByChannelName(ByVal siChannel As String, ByVal CiChannels As List(Of String), ByVal OiValue As Object) As String
    Dim sRes As String = ""
    Try

      For nIndex As Integer = 0 To CiChannels.Count - 1
        If CiChannels(nIndex) = siChannel Then

          Select Case CiChannels(nIndex)
            Case "POS_X" : Me.POS_X = CSng(OiValue)
            Case "POS_Y" : Me.POS_Y = CSng(OiValue)  '= Single.Parse(asAux(nIndex), numInfo)
            Case "POS_Z" : Me.POS_Z = CSng(OiValue)  '= Single.Parse(asAux(nIndex), numInfo)
            Case "TARGET_X" : Me.TARGET_X = CSng(OiValue)  ' = Single.Parse(asAux(nIndex), numInfo)
            Case "TARGET_Y" : Me.TARGET_Y = CSng(OiValue)  '= Single.Parse(asAux(nIndex), numInfo)
            Case "TARGET_Z" : Me.TARGET_Z = CSng(OiValue)  '= Single.Parse(asAux(nIndex), numInfo)
            Case "ROLL" : Me.ROLL = CSng(OiValue) '= Single.Parse(asAux(nIndex), numInfo)
            Case "FOV" : Me.FOV = CSng(OiValue) '= Single.Parse(asAux(nIndex), numInfo)
            Case "CENTER_H" : Me.CENTER_H = CSng(OiValue) '= Single.Parse(asAux(nIndex), numInfo)
            Case "CENTER_V" : Me.CENTER_V = CSng(OiValue) '= Single.Parse(asAux(nIndex), numInfo)
            Case "ASPECT" : Me.ASPECT = CSng(OiValue) '= Single.Parse(asAux(nIndex), numInfo)
            Case "IRIS" : Me.IRIS = CSng(OiValue) '= Single.Parse(asAux(nIndex), numInfo)
            Case "FOCAL_LEN" : Me.FOCAL_LEN = CSng(OiValue) '= Single.Parse(asAux(nIndex), numInfo)
            Case "FOCAL_PLANE" : Me.FOCAL_PLANE = CSng(OiValue) '= Single.Parse(asAux(nIndex), numInfo)
            Case "FOCAL_PLANE" : Me.FOCAL_PLANE = CSng(OiValue) '= Single.Parse(asAux(nIndex), numInfo)
            Case "CAM_NUM" : Me.CAM_NUM = CInt(OiValue) '= Integer.Parse(asAux(nIndex), numInfo)
              'Case "TIMECODE" : Me.TIMECODE = Date.Parse(asAux(nIndex), numInfo)
            Case "COUNTER" : Me.COUNTER = CInt(OiValue) ' = Integer.Parse(asAux(nIndex), numInfo)
            Case "ROT_X" : Me.ROT_X = CSng(OiValue) ' = Integer.Parse(asAux(nIndex), numInfo)
            Case "ROT_Y" : Me.ROT_Y = CSng(OiValue) ' = Integer.Parse(asAux(nIndex), numInfo)
            Case "ROT_Z" : Me.ROT_Z = CSng(OiValue) ' = Integer.Parse(asAux(nIndex), numInfo)


          End Select
        End If
      Next
      Me.UpdateRadianValues()
    Catch ex As Exception

    End Try
    Return sRes
  End Function

#Region "Properties"
  Public ReadOnly Property Valid() As Boolean
    Get
      Dim bValid As Boolean = True
      If Me.POS_X = Me.TARGET_X And Me.POS_Y = Me.TARGET_Y And Me.POS_Z = Me.TARGET_Z Then
        bValid = False
      End If

      bValid = bValid And (Not Me.ASPECT <= 0)
      bValid = bValid And (Not Me.FOV <= 0)
      bValid = bValid And (Not Me.FOV > 1000)
      Return bValid
    End Get
  End Property

  Public ReadOnly Property Frame() As String
    Get
      Dim sFrame As String = ""
      Dim ms, s, m, h, aux As Long
      aux = CLng(Me.CapturedMS) ' - Me.OffsetMS)
      ms = aux Mod 1000
      aux = aux \ 1000
      s = aux Mod 60
      aux = aux \ 60
      m = aux Mod 60
      aux = aux \ 60
      h = aux Mod 60

      sFrame = Format(h, "00") & ":" & Format(m, "00") & ":" & Format(s, "00") & "." & Format(ms, "000")

      Return sFrame
    End Get
  End Property

  Public ReadOnly Property MessageBytes() As Byte()
    Get
      Try
        If Me.buffer Is Nothing Then
          Me.COUNTER = 0
          Dim oWriter As New BinaryWriter(New MemoryStream(Me.buffer))


          Dim bDone As Boolean = False
          Dim fAux As Single

          Dim empty As Byte = 100
          For nIndex As Integer = 0 To gudtCnfg.OffsetBytes
            oWriter.Write(empty)
          Next
          'oReader.ReadBytes(niOffsetBytes)

          oWriter.Write(Me.POS_X) ' '= oReader.ReadSingle
          oWriter.Write(Me.POS_Y) '= oReader.ReadSingle
          oWriter.Write(Me.POS_Z) '= oReader.ReadSingle
          oWriter.Write(Me.TARGET_X) '= oReader.ReadSingle
          oWriter.Write(Me.TARGET_Y) '= oReader.ReadSingle
          oWriter.Write(Me.TARGET_Z) '= oReader.ReadSingle
          'està repetit, no sé per què

          oWriter.Write(Me.POS_X) '= oReader.ReadSingle
          oWriter.Write(Me.POS_Y) '= oReader.ReadSingle
          oWriter.Write(Me.POS_Z) '= oReader.ReadSingle
          oWriter.Write(Me.TARGET_X) '= oReader.ReadSingle
          oWriter.Write(Me.TARGET_Y) '= oReader.ReadSingle
          oWriter.Write(Me.TARGET_Z) '= oReader.ReadSingle

          fAux = 0.0
          oWriter.Write(fAux) '      fAux() '= oReader.ReadSingle
          oWriter.Write(Me.FOV_RAD) '= oReader.ReadSingle


          oWriter.Write(Me.ASPECT) '= oReader.ReadSingle
          oWriter.Write(fAux) '      fAux() '= oReader.ReadSingle
          oWriter.Write(Me.FOCAL_PLANE) '= oReader.ReadSingle
          oWriter.Write(Me.FOCAL_LEN) '= oReader.ReadSingle
          oWriter.Write(Me.CENTER_H) '= oReader.ReadSingle
          oWriter.Write(Me.CENTER_V) '= oReader.ReadSingle

          oWriter.Close()
        Else
        End If
      Catch ex As Exception

      End Try
      Return Me.buffer
    End Get
  End Property

  Public ReadOnly Property MessageBytesString() As String
    Get
      Dim oReader As New BinaryReader(New MemoryStream(Me.MessageBytes))

      Dim sRes As String = "" '= System.Text.Encoding.ASCII.GetString(oReader.BaseStream.Read)

      For i As Integer = 0 To Me.MessageBytes.Length - 1
        sRes = sRes & Hex(Me.MessageBytes(i))
      Next
      'While oReader.BaseStream.Position < oReader.BaseStream.Length
      '  sRes = sRes & oReader.ReadString()
      'End While
      'oReader.Close()
      'oReader = Nothing
      Return sRes
    End Get
  End Property
#End Region
End Class
