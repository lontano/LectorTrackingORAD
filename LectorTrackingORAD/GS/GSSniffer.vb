Imports System.ComponentModel
Imports System.IO
Imports GSTools
Imports LectorArxiuTrackingORAD

Public Class GSSniffer
  Private WithEvents _trackingFile As TrackingFile

  Public Property TrackingFile As TrackingFile
    Get
      Return _trackingFile
    End Get
    Set(value As TrackingFile)
      _trackingFile = value
      ImportTrackingFile()
    End Set
  End Property


  Public InitPosition As Double = -1
  Public FinalPosition As Double = -1

  Private _frameInfoStream As GSTools.FrameInfoStream
  Private _frameInfoStreams As New Dictionary(Of String, GSTools.FrameInfoStream)

  Private _streamsPath As String = "C:\GELO\"
  Public Property StreamsPath As String
    Get
      Return _streamsPath
    End Get
    Set(value As String)
      _streamsPath = value
    End Set
  End Property

  Public Function ExportarTrackingFile() As TrackingFile
    Dim res As New TrackingFile
    Try
      res.InitTickTimeMS = Double.MaxValue
      res.LastTickTimeMS = Double.MinValue

      For Each kvp As KeyValuePair(Of String, FrameInfoStream) In _frameInfoStreams
        Dim v1 As String = kvp.Key
        Dim fis As FrameInfoStream = kvp.Value

        Dim src As New TrackingSource

        src.Host = fis.Host
        src.Port = fis.Port

        src.TrackingHost = New TrackingHost
        src.TrackingHost.Host = fis.Host
        src.TrackingHost.IP = fis.IP
        src.TrackingHost.SourcePort = fis.Port
        src.TrackingHost.TargetPort = fis.Port


        res.TrackingSources.Add(src)

        For Each fi As FrameInfo In fis.LlistaFrameInfo
          Dim value As New TrackingValue
          value.CAM_NUM = fis.Index
          value.FOV = fi.CameraParameters.fov
          value.POS_X = fi.CameraParameters.x
          value.POS_Y = fi.CameraParameters.y
          value.POS_Z = fi.CameraParameters.z
          value.ROT_X = fi.CameraParameters.tilt
          value.ROT_Y = fi.CameraParameters.pan
          value.ROT_Z = fi.CameraParameters.roll
          value.TARGET_X = fi.CameraParameters.lookAtX
          value.TARGET_Y = fi.CameraParameters.lookAtY
          value.TARGET_Z = fi.CameraParameters.lookAtZ
          value.ASPECT = 16 / 9
          value.FOV_RAD = value.FOV
          value.HOST = fis.Name
          value.CapturedMS = fi.CameraParameters.CapturedMS

          value.UpdateRadianValues()

          value.CapturedMS = fi.ClockMs
          src.TrackingValues.Add(value)

          res.InitTickTimeMS = Math.Min(res.InitTickTimeMS, value.CapturedMS)
          res.LastTickTimeMS = Math.Max(res.LastTickTimeMS, value.CapturedMS)
        Next
      Next

    Catch ex As Exception

    End Try
    Return res
  End Function

  Private Sub ImportTrackingFile()
    Try
      Dim lastMs As Double = 0
      If Not _trackingFile Is Nothing Then
        _frameInfoStream = New FrameInfoStream
        _frameInfoStream.Name = System.IO.Path.GetFileNameWithoutExtension(_trackingFile.Path)
        'a saco
        Dim aSaco As Boolean = True
        If aSaco Then
          For Each src As TrackingSource In _trackingFile.SelectedSources
            For Each value As TrackingValue In src.TrackingValues
              Dim bAdd As Boolean = True
              If Me.InitPosition <> Me.FinalPosition Then
                bAdd = (value.CapturedMS >= Me.InitPosition And value.CapturedMS <= Me.FinalPosition)
              End If
              If bAdd Then Me.AddTrackingValueToStream(value, False)

              lastMs = value.CapturedMS
            Next
          Next
        Else
          'a ver, un poco más inteligente
          Dim initValueTime As Double = Double.MaxValue
          Dim endValueTime As Double = Double.MinValue

          For Each src As TrackingSource In _trackingFile.SelectedSources
            For Each value As TrackingValue In src.TrackingValues
              initValueTime = Math.Min(initValueTime, value.CapturedMS)
              endValueTime = Math.Max(endValueTime, value.CapturedMS)
            Next
          Next

          If Me.InitPosition <> Me.FinalPosition Then
            initValueTime = Me.InitPosition
            endValueTime = Me.FinalPosition
          End If

          For Each src As TrackingSource In _trackingFile.SelectedSources
            If src.Selected Then
              Dim msPerFrame As Double = 40
              For time As Double = initValueTime To endValueTime Step msPerFrame
                Dim value As TrackingValue = src.GetValueByTime(time, 0, False)
                If Not value Is Nothing Then
                  Me.AddTrackingValueToStream(value, False)
                  lastMs = value.CapturedMS
                End If
              Next
            End If
          Next
        End If
      End If
    Catch ex As Exception

    End Try
  End Sub

  Public Sub DesarFrameInfoStreams()
    Try
      Me.UpdateFrameInfoCollectionFile(_frameInfoStream, Me.StreamsPath)
      For Each kvp As KeyValuePair(Of String, FrameInfoStream) In _frameInfoStreams
        Dim v1 As String = kvp.Key
        Dim fis As FrameInfoStream = kvp.Value

        Dim file As String = System.IO.Path.GetDirectoryName(Me.StreamsPath)
        file = System.IO.Path.Combine(file, System.IO.Path.GetFileNameWithoutExtension(Me.StreamsPath))
        file = System.IO.Path.Combine(file, fis.Name & ".json")

        Me.UpdateFrameInfoCollectionFile(fis, file)
      Next

      'Crear el frameInfo amb tots els valors
      _frameInfoStream.Name = System.IO.Path.GetFileNameWithoutExtension(_gsSniffer.StreamsPath)
      _frameInfoStream.LlistaFrameInfo.Clear()
      Dim channelIndex As Integer = 0
      For Each kvp As KeyValuePair(Of String, FrameInfoStream) In _frameInfoStreams
        Dim v1 As String = kvp.Key
        Dim v2 As FrameInfoStream = kvp.Value
        For i As Integer = 0 To _frameInfoStream.LlistaFrameInfo.Count - 1
          Dim fi As FrameInfo = v2.GetFrameInfoByFrameNumber(i)
          _frameInfoStream.LlistaFrameInfo(i).SetCameraParametersForChannel(channelIndex, fi.CameraParameters)
        Next
        For i As Integer = _frameInfoStream.LlistaFrameInfo.Count To v2.LlistaFrameInfo.Count - 1
          Dim fi As FrameInfo = v2.GetFrameInfoByFrameNumber(i)
          While _frameInfoStream.LlistaFrameInfo.Count <= i
            Dim newFI As New FrameInfo
            newFI.FrameNumber = _frameInfoStream.LlistaFrameInfo.Count
            newFI.ClockMs = fi.ClockMs
            newFI.InfoText = fi.InfoText
            newFI.OpticCodeInfo = fi.OpticCodeInfo
            newFI.CameraParameters = fi.CameraParameters
            newFI.ActiveChannel = channelIndex
            _frameInfoStream.LlistaFrameInfo.Add(newFI)
          End While
          _frameInfoStream.LlistaFrameInfo(i).SetCameraParametersForChannel(channelIndex, fi.CameraParameters)
        Next
        channelIndex += 1
      Next
      Me.UpdateFrameInfoCollectionFile(_frameInfoStream, Me.StreamsPath)
    Catch ex As Exception

    End Try
  End Sub

#Region "GSTools"
  Private Sub _trackingFile_NewTrackingValue(CiTrackingSource As TrackingSource, CiValue As TrackingValue) Handles _trackingFile.NewTrackingValue
    If CiTrackingSource.Selected Or True Then
      Me.AddTrackingValueToStream(CiValue)
    End If
  End Sub

  Private Sub AddTrackingValueToStream(CiTrackingValue As TrackingValue, Optional saveFile As Boolean = False)
    Try
      Dim sTFId As String = CiTrackingValue.HOST & "@" & CiTrackingValue.PORT
      Dim tf As GSTools.FrameInfoStream = Nothing

      If Not _frameInfoStreams.ContainsKey(sTFId) Then
        tf = New GSTools.FrameInfoStream()
        tf.Name = sTFId
        tf.Index = _frameInfoStreams.Count
        tf.Host = CiTrackingValue.HOST
        tf.IP = CiTrackingValue.HOST
        tf.Port = CiTrackingValue.PORT
        _frameInfoStreams.Add(sTFId, tf)
      Else
        tf = _frameInfoStreams.Item(sTFId)
      End If

      Dim frameInfo As New GSTools.FrameInfo
      frameInfo.ClockMs = CiTrackingValue.CapturedMS
      frameInfo.InfoText = sTFId

      frameInfo.FrameNumber = tf.LlistaFrameInfo.Count
      frameInfo.CameraParameters = New GSTools.FrameTools.CameraParameters()
      frameInfo.CameraParameters.CapturedMS = frameInfo.ClockMs

      frameInfo.CameraParameters.fov = CiTrackingValue.FOV
      frameInfo.CameraParameters.x = CiTrackingValue.POS_X
      frameInfo.CameraParameters.y = CiTrackingValue.POS_Y
      frameInfo.CameraParameters.z = CiTrackingValue.POS_Z
      frameInfo.CameraParameters.lookAtX = CiTrackingValue.TARGET_X
      frameInfo.CameraParameters.lookAtY = CiTrackingValue.TARGET_Y
      frameInfo.CameraParameters.lookAtZ = CiTrackingValue.TARGET_Z
      frameInfo.CameraParameters.tilt = CiTrackingValue.ROT_X
      frameInfo.CameraParameters.pan = CiTrackingValue.ROT_Y
      frameInfo.CameraParameters.roll = CiTrackingValue.ROT_Z

      frameInfo.CameraParameters.Host = CiTrackingValue.HOST
      frameInfo.CameraParameters.IP = CiTrackingValue.IP
      frameInfo.CameraParameters.Port = CiTrackingValue.PORT


      tf.AddFrameInfo(frameInfo, False)
      If saveFile Then
        'UpdateFrameInfoCollectionFile(tf,  frameInfo)
      End If
    Catch ex As Exception

    End Try

  End Sub


  Private Sub AddTrackingValueToStream(cameraParameters As GSTools.FrameTools.CameraParameters, Optional saveFile As Boolean = False)
    Try
      Dim sTFId As String = cameraParameters.Host & "@" & cameraParameters.Port
      Dim tf As GSTools.FrameInfoStream = Nothing

      If Not _frameInfoStreams.ContainsKey(sTFId) Then
        tf = New GSTools.FrameInfoStream()
        tf.Name = sTFId
        tf.Index = _frameInfoStreams.Count
        tf.Host = cameraParameters.Host
        tf.IP = cameraParameters.Host
        tf.Port = cameraParameters.Port
        _frameInfoStreams.Add(sTFId, tf)
      Else
        tf = _frameInfoStreams.Item(sTFId)
      End If

      Dim frameInfo As New GSTools.FrameInfo
      frameInfo.ClockMs = cameraParameters.CapturedMS
      frameInfo.InfoText = sTFId

      frameInfo.FrameNumber = tf.LlistaFrameInfo.Count
      frameInfo.CameraParameters = cameraParameters

      tf.AddFrameInfo(frameInfo, False)
      If saveFile Then
        ' UpdateFrameInfoCollectionFile(tf, frameInfo)
      End If
    Catch ex As Exception

    End Try
  End Sub

  Private Sub AddTrackingValueToStream(frameInfo As FrameInfo, Optional saveFile As Boolean = False)
    Try
      Dim sTFId As String = frameInfo.InfoText
      Dim tf As GSTools.FrameInfoStream = Nothing

      For i As Integer = 0 To frameInfo.CameraParameterChannels.Count - 1
        Me.AddTrackingValueToStream(frameInfo.CameraParameterChannels(i), saveFile)
      Next

    Catch ex As Exception

    End Try

  End Sub

#End Region


#Region "Chan bridge file"

  Private Sub UpdateFrameInfoCollectionFile(frameInfoStream As FrameInfoStream, fileName As String)
    Me.UpdateFrameInfoCollectionFile(frameInfoStream, Nothing, fileName)
  End Sub
  'save FrameInfoCollection file
  Private Sub UpdateFrameInfoCollectionFile(frameInfoStream As FrameInfoStream, frameInfo As FrameInfo, file As String)
    Try
      'Static busy As Boolean = False
      'If Not busy Then
      ' busy = True
      'we don't want duplicates here, we may lose some changes but it's better than to block everything
      LaunchBackWorker(frameInfo, frameInfoStream, file)
      '  Return
      'End If
      'busy = False
    Catch ex As Exception
    End Try
  End Sub

  Private WithEvents _backWorkerSaveFile As BackgroundWorker
  Private _updateRequest As Boolean = False
  Private _pendingParams As BackworkerSaveFileParams = Nothing
  Private _pendingParamsList As New Dictionary(Of String, BackworkerSaveFileParams)
  Private _done As Boolean = False

  Private Class BackworkerSaveFileParams
    Public frameInfo As GSTools.FrameInfo
    Public frameInfoStream As GSTools.FrameInfoStream
    Public file As String

    Public Sub New(ByVal myFrameInfo As FrameInfo, ByVal myFrameInfoStream As FrameInfoStream, myFile As String)
      Me.frameInfo = myFrameInfo
      Me.frameInfoStream = myFrameInfoStream
      Me.file = myFile
    End Sub
  End Class


  Private Sub LaunchBackWorker(frameInfo As FrameInfo, frameInfoStream As FrameInfoStream, file As String)
    Me.LaunchBackWorker(New BackworkerSaveFileParams(frameInfo, frameInfoStream, file))
  End Sub

  Private Sub LaunchBackWorker(args As BackworkerSaveFileParams)
    If _backWorkerSaveFile Is Nothing Then
      _backWorkerSaveFile = New BackgroundWorker
      _backWorkerSaveFile.WorkerReportsProgress = True
      _backWorkerSaveFile.WorkerSupportsCancellation = False
    End If
    If Not _backWorkerSaveFile.IsBusy Then
      _updateRequest = False
      _pendingParams = Nothing
      _backWorkerSaveFile.RunWorkerAsync(args)
    Else
      _updateRequest = True
      _pendingParams = args
      If _pendingParamsList.ContainsKey(args.file) Then
        _pendingParamsList.Item(args.file) = args
      Else
        _pendingParamsList.Add(args.file, args)
      End If
    End If
  End Sub

  Private Sub _backWorkerSaveFile_DoWork(sender As Object, e As DoWorkEventArgs) Handles _backWorkerSaveFile.DoWork
    Try
      Dim args As BackworkerSaveFileParams = CType(e.Argument, BackworkerSaveFileParams)
      Dim tempFile As String = (args.file) & ".tmp.json"
      Dim sData As String = GSTools.FrameInfoStream.EncodeToString(args.frameInfoStream)

      If SaveStringToFile(tempFile, sData) Then
        If System.IO.File.Exists(args.file) Then
          System.IO.File.Delete(args.file)
        End If
        My.Computer.FileSystem.RenameFile(tempFile, System.IO.Path.GetFileName(args.file))
      End If
    Catch ex As Exception

    End Try
  End Sub

  Private Sub _backWorkerSaveFile_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles _backWorkerSaveFile.RunWorkerCompleted
    If _pendingParamsList.Count > 0 Then
      For Each kvp As KeyValuePair(Of String, BackworkerSaveFileParams) In _pendingParamsList
        Dim v1 As String = kvp.Key
        Dim v2 As BackworkerSaveFileParams = kvp.Value
        _pendingParams = kvp.Value
        _pendingParamsList.Remove(kvp.Key)
        _updateRequest = True
        Exit For
      Next
    End If

    If _updateRequest Then
      LaunchBackWorker(_pendingParams)
    End If
  End Sub

  Public Sub CarregarFrameInfoStreams(file As String)
    Try
      _frameInfoStream = FrameInfoStream.DecodeFromString(LoadStringFromFile(file))
      If Not _frameInfoStream Is Nothing Then
        For Each fi As FrameInfo In _frameInfoStream.LlistaFrameInfo
          Me.AddTrackingValueToStream(fi, False)
        Next
      End If
    Catch ex As Exception

    End Try
  End Sub
#End Region

#Region "Load /  save file"
  Public Shared Function SaveStringToFile(ByVal siFile As String, siData As String) As String

    Dim bRes As Boolean = False
    Try
      Dim sDirectory = System.IO.Path.GetDirectoryName(siFile)
      If Not System.IO.Directory.Exists(sDirectory) Then
        System.IO.Directory.CreateDirectory(sDirectory)
      End If
      Dim myFileStream As FileStream = File.Create(siFile)
      Dim bytes() As Byte
      bytes = System.Text.Encoding.UTF8.GetBytes(siData)
      myFileStream.Write(bytes, 0, bytes.Length)
      myFileStream.Flush()
      myFileStream.Close()
      bRes = True
    Catch ex As Exception
      Debug.Print(ex.ToString)
    End Try
    Return bRes
  End Function

  Public Shared Function LoadStringFromFile(ByVal siFile As String) As String
    Dim sRes As String = ""
    Try
      If File.Exists(siFile) Then
        Dim myFileStream As FileStream = File.OpenRead(siFile)
        Dim bytes(myFileStream.Length - 1) As Byte
        myFileStream.Read(bytes, 0, myFileStream.Length)
        sRes = System.Text.Encoding.UTF8.GetString(bytes)
        myFileStream.Close()
      End If
    Catch ex As Exception
    End Try
    Return sRes
  End Function
#End Region

End Class

