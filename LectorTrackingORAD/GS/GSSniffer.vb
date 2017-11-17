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

  Private _frameInfoStreams As New Dictionary(Of String, GSTools.FrameInfoStream)

  Private _streamsPath As String = "C:\GELO\"
  Public Property StreamsPath As String
    Get
      Return _streamsPath
    End Get
    Set(value As String)
      _streamsPath = value
      Try
        System.IO.Directory.CreateDirectory(value)
      Catch ex As Exception

      End Try
    End Set
  End Property

  Private Sub ImportTrackingFile()
    Try
      If Not _trackingFile Is Nothing Then
        For Each src As TrackingSource In _trackingFile.SelectedSources
          For Each value As TrackingValue In src.TrackingValues
            Me.AddTrackingValueToStream(value, False)
          Next
        Next
      End If
    Catch ex As Exception

    End Try
  End Sub

  Public Sub DesarFrameInfoStreams()
    Try
      For Each kvp As KeyValuePair(Of String, FrameInfoStream) In _frameInfoStreams
        Dim v1 As String = kvp.Key
        Dim v2 As FrameInfoStream = kvp.Value

        Me.UpdateFrameInfoCollectionFile(v2)
      Next
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
        _frameInfoStreams.Add(sTFId, tf)
      Else
        tf = _frameInfoStreams.Item(sTFId)
      End If

      Dim frameInfo As New GSTools.FrameInfo
      frameInfo.ClockMs = CiTrackingValue.CapturedMS

      frameInfo.FrameNumber = tf.LlistaFrameInfo.Count
      frameInfo.CameraParameters = New GSTools.FrameTools.CameraParameters()
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

      tf.AddFrameInfo(frameInfo, False)
      If saveFile Then
        UpdateFrameInfoCollectionFile(tf, frameInfo)
      End If
    Catch ex As Exception

    End Try

  End Sub

#End Region


#Region "Chan bridge file"

  Private Sub UpdateFrameInfoCollectionFile(frameInfoStream As FrameInfoStream)
    Me.UpdateFrameInfoCollectionFile(frameInfoStream, Nothing)
  End Sub
  'save FrameInfoCollection file
  Private Sub UpdateFrameInfoCollectionFile(frameInfoStream As FrameInfoStream, frameInfo As FrameInfo)
    Try
      Dim file As String = System.IO.Path.Combine(Me.StreamsPath, frameInfoStream.Name & ".json")

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


#End Region

#Region "Load /  save file"
  Public Shared Function SaveStringToFile(ByVal siFile As String, siData As String) As String

    Dim bRes As Boolean = False
    Try
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

