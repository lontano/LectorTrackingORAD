Public Class CCameraColladaExporter
  Implements IExporter



  Public Sub Export(ByVal CiTrackingFile As TrackingFile, ByVal siFile As String, Optional ByVal niMaxValues As Integer = -1) Implements IExporter.Export
    Dim ColladaModel As Collada141.COLLADA
    Dim sFile As String
    Dim CTrackingValues As New List(Of TrackingValue)
    Dim CTrackingSource As TrackingSource
    Dim nCount As Integer
    Dim sCameraID As String

    Try
      ColladaModel = New Collada141.COLLADA()

      'agafar valors 


      'General info
      Dim CAsset As New Collada141.asset
      Dim CContributors(0) As Collada141.assetContributor
      Dim CContributor As New Collada141.assetContributor

      CContributor.author = "EGTR"
      CContributor.authoring_tool = "ORAD Tracking sniffer"
      CContributor.comments = "imported from camera tracking"
      CContributors(0) = CContributor
      CAsset.contributor = CContributors
      CAsset.created = Now
      CAsset.modified = Now
      CAsset.up_axis = Collada141.UpAxisType.Y_UP
      ColladaModel.asset = CAsset

      'objectes de la escena
      Dim CObjects(2) As Object

      ColladaModel.Items = CObjects

      'visual scene, això és bàsic!

      Dim CSceneLibrary As New Collada141.library_visual_scenes
      CObjects(2) = CSceneLibrary
      Dim CVisualScenes(0) As Collada141.visual_scene
      CSceneLibrary.visual_scene = CVisualScenes
      CSceneLibrary.visual_scene(0) = New Collada141.visual_scene
      CSceneLibrary.visual_scene(0).id = "RootNode"
      CSceneLibrary.visual_scene(0).name = "RootNode"
      Dim CSceneNodes(2 * CiTrackingFile.SelectedPorts.Count) As Collada141.node
      CSceneLibrary.visual_scene(0).node = CSceneNodes


      'scene, això també és bàsic!!!
      Dim CScene As New Collada141.COLLADAScene
      CScene.instance_visual_scene = New Collada141.InstanceWithExtra
      CScene.instance_visual_scene.url = "#RootNode"

      ColladaModel.scene = (CScene)
      'CScene.instance_visual_scene



      'Càmeres
      Dim CCamLibrary As New Collada141.library_cameras
      Dim CCams(CiTrackingFile.SelectedPorts.Count) As Collada141.camera



      CCamLibrary.camera = CCams
      CObjects(0) = CCamLibrary



      For nIndex As Integer = 0 To CiTrackingFile.SelectedPorts.Count - 1
        CCams(nIndex) = New Collada141.camera

        Dim CPers As Collada141.cameraOpticsTechnique_commonPerspective
        With CCams(nIndex)
          .name = "CAM" & (nIndex + 1)
          .id = "CAM" & nIndex & "_PORT_" & CiTrackingFile.SelectedPorts(nIndex)

          'node de la càmera
          CSceneNodes(2 * nIndex) = New Collada141.node
          CSceneNodes(2 * nIndex).id = .id
          Dim CAuxInstanceWithExtra(0) As Collada141.InstanceWithExtra
          CSceneNodes(2 * nIndex).instance_camera = CAuxInstanceWithExtra
          CSceneNodes(2 * nIndex).instance_camera(0) = New Collada141.InstanceWithExtra
          CSceneNodes(2 * nIndex).instance_camera(0).url = "#" & .id
          Dim CMatrix As New Collada141.matrix
          Dim CMatrixes(0) As Collada141.matrix
          Dim CValues(15) As Double
          CMatrixes(0) = CMatrix
          CMatrix.sid = "matrix"
          CMatrix.Values = CValues
          For nValue As Integer = 0 To 3
            CMatrix.Values(nValue * 4 + nValue) = 1
          Next
          CMatrix.Values(12 + 0) = 100
          CMatrix.Values(12 + 1) = 100
          CMatrix.Values(12 + 2) = 100
          CSceneNodes(2 * nIndex).Items = CMatrixes

          Dim CItemsElementNameAux(1) As Collada141.ItemsChoiceType1
          CSceneNodes(2 * nIndex).ItemsElementName = CItemsElementNameAux
          CSceneNodes(2 * nIndex).ItemsElementName(0) = Collada141.ItemsChoiceType2.matrix



          'node del target de la càmera
          CSceneNodes(2 * nIndex + 1) = New Collada141.node
          CSceneNodes(2 * nIndex + 1).id = .id & "_Target"
          Dim CMatrixTarget As New Collada141.matrix
          Dim CMatrixesTarget(0) As Collada141.matrix
          Dim CValuesTarget(15) As Double
          CMatrixesTarget(0) = CMatrixTarget
          CMatrixTarget.sid = "matrix"
          CMatrixTarget.Values = CValuesTarget
          For nValue As Integer = 0 To 3
            CMatrixTarget.Values(nValue * 4 + nValue) = 1
          Next
          CSceneNodes(2 * nIndex + 1).Items = CMatrixesTarget
          CSceneNodes(2 * nIndex + 1).ItemsElementName = CItemsElementNameAux
          CSceneNodes(2 * nIndex + 1).ItemsElementName(0) = Collada141.ItemsChoiceType2.matrix

          CCams(nIndex).optics = New Collada141.cameraOptics
          CCams(nIndex).optics.technique_common = New Collada141.cameraOpticsTechnique_common()
          CPers = New Collada141.cameraOpticsTechnique_commonPerspective

          'near plane
          CPers.znear = New Collada141.TargetableFloat
          CPers.znear.Value = 40
          CPers.znear.sid = "znear"

          'far plane
          CPers.zfar = New Collada141.TargetableFloat
          CPers.zfar.Value = 40000
          CPers.zfar.sid = "zfar"

          'field of view (animation?)
          Dim CItems(1) As Collada141.TargetableFloat

          CPers.Items = CItems

          Dim CItemsElementName(1) As Collada141.ItemsChoiceType1
          CPers.ItemsElementName = CItemsElementName

          CItems(0) = New Collada141.TargetableFloat
          CItems(0).sid = "xfov"
          CItems(0).Value = 2
          CPers.ItemsElementName(0) = Collada141.ItemsChoiceType1.xfov


          CItems(1) = New Collada141.TargetableFloat
          'CItems(1).sid = ""
          CItems(1).Value = 2
          CPers.ItemsElementName(1) = Collada141.ItemsChoiceType1.aspect_ratio




          CCams(nIndex).optics.technique_common.Item = CPers
        End With
      Next

      'animacions!
      Dim CAnimationLibrary As New Collada141.library_animations()

      Dim LlistaAnimations As New List(Of Collada141.animation)
      'CObjects(1) = CAnimationLibrary

      Dim CAnimAux(0) As Collada141.animation
      Dim CAnimSource As Collada141.source
      Dim LlistaSources As List(Of Object)
      Dim CFloatArray As Collada141.float_array
      Dim CTechniquesCommon(0) As Collada141.sourceTechnique_common
      Dim CAccessor As Collada141.accessor
      Dim CParams(0) As Collada141.param
      Dim nOffset As Integer

      For nIndex As Integer = 0 To CiTrackingFile.SelectedPorts.Count - 1

        For Each CTrackingSource In CiTrackingFile.TrackingSources
          'For nSource As Integer = 0 To CiTrackingFile.TrackingSources.Count - 1
          If CTrackingSource.Port = CiTrackingFile.SelectedPorts(nIndex) Then
            sCameraID = "CameraORADPort" & CTrackingSource.Port
            CTrackingValues = CTrackingSource.TrackingValues
            If niMaxValues = -1 Then
              nCount = CTrackingValues.Count
            Else
              nCount = Math.Min(niMaxValues, CTrackingValues.Count)
            End If
          End If
        Next


        Dim CAnimParent As Collada141.animation
        Dim CAnim As Collada141.animation
        If niMaxValues <= 0 Then

        Else
        End If
        'per cada càmera, animacions de fov i posició
        CAnimParent = New Collada141.animation

        'la base de temps!
        CAnimParent.name = "CAM" & (nIndex + 1)
        CAnimParent.id = CAnimParent.name & "_Anim"


        CAnimAux(0) = New Collada141.animation
        CAnimParent.Items = CAnimAux

        CAnim = New Collada141.animation
        CAnimAux(0) = CAnim
        CAnimSource = New Collada141.source
        LlistaSources = New List(Of Object)
        LlistaSources.Add(CAnimSource)
        CAnimSource.id = CAnimParent.name & "_lib_xfov-animation-input"


        CFloatArray = New Collada141.float_array()
        ReDim CFloatArray.Values(nCount - 1)
        CFloatArray.count = CType(nCount, ULong)
        For nValue As Integer = 0 To nCount - 1
          CFloatArray.Values(nValue) = nValue / 25
        Next

        CFloatArray.id = CAnimSource.id & "-array"
        CAnimSource.Item = CFloatArray

        CAnimSource.technique_common = New Collada141.sourceTechnique_common

        CAccessor = New Collada141.accessor
        CAccessor.source = "#" & CAnimSource.id
        CAccessor.param = CParams
        CParams(0) = New Collada141.param()
        CParams(0).name = "TIME"
        CParams(0).type = "float"
        CAnimSource.technique_common.accessor = CAccessor

        'position matrix
        CAnimSource = New Collada141.source
        LlistaSources.Add(CAnimSource)
        CAnimSource.id = CAnim.id & "-Matrix-animation-output-transform"
        CFloatArray = New Collada141.float_array()
        ReDim CFloatArray.Values(nCount * 16 - 1)
        CFloatArray.count = CType(nCount * 16, ULong)
        For nValue As Integer = 0 To nCount - 1
          nOffset = nValue * 16
          'matriu identitat
          CFloatArray.Values(nOffset + 0) = 1
          CFloatArray.Values(nOffset + 4 + 1) = 1
          CFloatArray.Values(nOffset + 8 + 2) = 1
          CFloatArray.Values(nOffset + 12 + 3) = 1
          'valors de desplaçament
          CFloatArray.Values(nOffset + 12 + 0) = CTrackingValues(nValue).POS_X
          CFloatArray.Values(nOffset + 12 + 1) = CTrackingValues(nValue).POS_Y
          CFloatArray.Values(nOffset + 12 + 2) = CTrackingValues(nValue).POS_Z
        Next

        CFloatArray.id = CAnimSource.id & "-array"
        CAnimSource.Item = CFloatArray

        CAnimSource.technique_common = New Collada141.sourceTechnique_common

        CAccessor = New Collada141.accessor
        CAccessor.source = "#" & CAnimSource.id
        CAccessor.param = CParams
        CAccessor.count = CType(CiTrackingFile.SelectedValues.Count, ULong)
        CAccessor.stride = 16
        CParams(0) = New Collada141.param()
        CParams(0).type = "float4x4"
        CAnimSource.technique_common.accessor = CAccessor






        CAnimSource = New Collada141.source
        LlistaSources.Add(CAnimSource)
        CAnimSource.id = CAnim.id & "_lib_xfov-animation-output"

        CAnimSource = New Collada141.source
        LlistaSources.Add(CAnimSource)
        CAnimSource.id = CAnim.id & "_lib_xfov-animation-intan"

        CAnimSource = New Collada141.source
        LlistaSources.Add(CAnimSource)
        CAnimSource.id = CAnim.id & "_lib_xfov-animation-outtan"

        Dim CAnimSampler As Collada141.sampler
        CAnimSampler = New Collada141.sampler
        LlistaSources.Add(CAnimSampler)
        CAnimSampler.id = CAnim.id & "_lib_xfov-animation-animation"




        CAnim.Items = LlistaSources.ToArray
        LlistaAnimations.Add(CAnimParent)
      Next
      CAnimationLibrary.animation = LlistaAnimations.ToArray()


      sFile = siFile
      If System.IO.Path.GetExtension(sFile) <> ".DAE" Then
        sFile = sFile & ".DAE"
      End If
      ColladaModel.Save(sFile)
    Catch ex As Exception
      MsgBox(ex.ToString)
    End Try
  End Sub

  Public Event UpdateProgress(ByVal diProgress1 As Double, ByVal diProgress2 As Double, ByVal siText As String) Implements IExporter.UpdateProgress
End Class

