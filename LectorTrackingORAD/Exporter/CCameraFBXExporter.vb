Public Class CameraFBXExporter
  Implements IExporter


  Public Sub Export(ByVal CiTrackingFile As TrackingFile, ByVal siFile As String, Optional ByVal niMaxValues As Integer = -1) Implements IExporter.Export
    Try
      Dim CFile As New FBXFile
      Dim FileWriter As FileExporter = New FileExporter(siFile)

      Dim CCam As FBXCamera
      Dim CTarget As FBXLookAt
      Dim sCameraID As String
      Dim CKey As FBXKey
      Dim CTakeCamModel As FBXTakeModel
      Dim CTakeTargetModel As FBXTakeModel
      Dim CTake As New FBXTake
      CFile.Takes.LlistaTakes.Add(CTake)

      Dim fStartTime As Single = -1
      Dim fTime As Single = 0
      Dim CTrackingValue As TrackingValue

      For Each CTrackingSource As TrackingSource In CiTrackingFile.TrackingSources
        If CTrackingSource.TrackingValues.Count > 0 Then
          If CTrackingSource.TrackingValues(0).OffsetMS < fStartTime Or fStartTime = -1 Then
            fStartTime = CTrackingSource.TrackingValues(0).CapturedMS
          End If
        End If
      Next

      For Each CTrackingSource As TrackingSource In CiTrackingFile.TrackingSources
        'For nSource As Integer = 0 To CiTrackingFile.TrackingSources.Count - 1
        If CTrackingSource.Selected Then
          sCameraID = CTrackingSource.Host & "_" & CTrackingSource.Port
          'La càmera
          CCam = New FBXCamera
          CCam.ModelName = sCameraID
          CCam.Name = "Model"
          CCam.InstanceName = "Model::" & sCameraID
          CCam.InstanceSubName = "Camera"
          CFile.Objects.LlistaValues.Add(CCam)
          CFile.Objects.LlistaObjects.Add(CCam)
          CCam.SetPosition(CTrackingSource.LastTrackingValue.POS_X, CTrackingSource.LastTrackingValue.POS_Y, CTrackingSource.LastTrackingValue.POS_Z)
          'CCam.SetRotation(CTrackingSource.LastTrackingValue.ROT_X, CTrackingSource.LastTrackingValue.ROT_Y, CTrackingSource.LastTrackingValue.ROT_Z)

          CCam.SetRotation(0, 0, 0)
          CCam.SetProperty("FieldOfView", "FieldOfView", "A+N", CTrackingSource.LastTrackingValue.FOV)
          CCam.SetTarget(CTrackingSource.LastTrackingValue.TARGET_X, CTrackingSource.LastTrackingValue.TARGET_Y, CTrackingSource.LastTrackingValue.TARGET_Z)


          CTakeCamModel = New FBXTakeModel()
          CTakeCamModel.Model = CCam

          CTake.LlistaModelTakes.Add(CTakeCamModel)

          'El target
          CTarget = New FBXLookAt
          CTarget.ModelName = sCameraID & ".Target"
          CTarget.Name = "Model"
          CTarget.InstanceName = "Model::" & sCameraID & ".Target"
          CTarget.InstanceSubName = "Null"
          CFile.Objects.LlistaValues.Add(CTarget)
          CFile.Objects.LlistaObjects.Add(CTarget)
          CTarget.SetPosition(CTrackingSource.LastTrackingValue.TARGET_X, CTrackingSource.LastTrackingValue.TARGET_Y, CTrackingSource.LastTrackingValue.TARGET_Z)

          CTakeTargetModel = New FBXTakeModel()
          CTakeTargetModel.Model = CTarget

          CFile.Objects.Connections.LlistaConnections.Add(New FBXConnection("OO", CCam.InstanceName, "Model::Scene", ""))
          CFile.Objects.Connections.LlistaConnections.Add(New FBXConnection("OO", CTarget.InstanceName, "Model::Scene", ""))
          CFile.Objects.Connections.LlistaConnections.Add(New FBXConnection("OP", CTarget.InstanceName, CCam.InstanceName, "LookAtProperty"))
          CTake.LlistaModelTakes.Add(CTakeTargetModel)

          'Les animacions

          For nIndex As Integer = 0 To CTrackingSource.TrackingValues.Count - 1
            CTrackingValue = CTrackingSource.TrackingValues(nIndex)
            fTime = CTrackingValue.CapturedMS - CTrackingValue.OffsetMS
            fTime = (nIndex + 1) * 40
            CTakeCamModel.ChannelTransformX.AddKey(fTime, CTrackingValue.POS_X)
            CTakeCamModel.ChannelTransformY.AddKey(fTime, CTrackingValue.POS_Y)
            CTakeCamModel.ChannelTransformZ.AddKey(fTime, CTrackingValue.POS_Z)
            CTakeCamModel.ChannelFOV.AddKey(fTime, CTrackingValue.FOV)
            CTakeTargetModel.ChannelTransformX.AddKey(fTime, CTrackingValue.TARGET_X)
            CTakeTargetModel.ChannelTransformY.AddKey(fTime, CTrackingValue.TARGET_Y)
            CTakeTargetModel.ChannelTransformZ.AddKey(fTime, CTrackingValue.TARGET_Z)
          Next
        End If
      Next

      With FileWriter
        .writeLine("; FBX 6.1.0 project file")
        .writeLine("; Copyright (C) 1997-2009 Autodesk Inc. and/or its licensors.")
        .writeLine("; All rights reserved.")
        .writeLine("; ----------------------------------------------------")
        .writeLine("")

        .write(CFile.HeaderExtension.GetFormattedString(0))

        .writeLine("")
        .writeLine("; Document Description")
        .writeLine(";------------------------------------------------------------------")
        .writeLine("")

        .write(CFile.Document.GetFormattedString(0))

        .writeLine("")
        .writeLine("; Document References")
        .writeLine(";------------------------------------------------------------------")
        .writeLine("")

        .write(CFile.References.GetFormattedString(0))

        .writeLine("")
        .writeLine("; Object definitions")
        .writeLine(";------------------------------------------------------------------")
        .writeLine("")

        .write(CFile.Objects.Definitions.GetFormattedString(0))

        .writeLine("")
        .writeLine("; Object properties")
        .writeLine(";------------------------------------------------------------------")
        .writeLine("")

        .write(CFile.Objects.GetFormattedString(0))

        .writeLine("")
        .writeLine("; Object connections")
        .writeLine(";------------------------------------------------------------------")
        .writeLine("")

        .write(CFile.Objects.Connections.GetFormattedString(0))

        .writeLine("")
        .writeLine("; Takes and animation section")
        .writeLine(";------------------------------------------------------------------")
        .writeLine("")

        .write(CFile.Takes.GetFBXValue.GetFormattedString(0))

        .Dispose()

      End With

      FileWriter = Nothing
    Catch ex As Exception

    End Try
  End Sub

  Public Event UpdateProgress(ByVal diProgress1 As Double, ByVal diProgress2 As Double, ByVal siText As String) Implements IExporter.UpdateProgress
End Class
