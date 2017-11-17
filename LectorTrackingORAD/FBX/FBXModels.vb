Public Enum FBXObjectType
  Model
  SceneInfo
  GlobalSettings
End Enum

Public Enum FBXModelsubType
  Camera
  UserData
  None
End Enum


Public Class FBXModels
  Inherits FBXValue

  Public LlistaObjects As New List(Of FBXValue)
  Public GlobalSettings As New FBXGlobalSettings
  Public SceneInfo As New FBXSceneInfo

  Public Sub New()
    Me.Name = "Objects"
    Me.LlistaValues.Add(Me.SceneInfo)
    Me.LlistaValues.Add(Me.GlobalSettings)
  End Sub


  Public ReadOnly Property Definitions() As FBXValue
    Get
      Dim CRes As New FBXValue
      Try
        CRes.Name = "Definitions"
        CRes.SetValue("Version", "100")
        CRes.SetValue("Count", CStr(Me.LlistaObjects.Count) + 2)
        Dim nModels As Integer = 0
        Dim nGlobalSettings As Integer = 0
        Dim nSceneInfo As Integer = 0
        For Each CAux As FBXValue In Me.LlistaValues
          Select Case CAux.Name
            Case "Model"
              nModels += 1
            Case "GlobalSettings"
              nGlobalSettings += 1
            Case "SceneInfo"
              nSceneInfo += 1
          End Select
        Next

        Dim CValue As FBXValue
        'Models
        CValue = New FBXValue
        With CValue
          .Name = "ObjectType"
          .InstanceName = "Model"
          .SetValue("Count", nModels)
        End With
        CRes.LlistaValues.Add(CValue)

        'SceneInfo
        CValue = New FBXValue
        With CValue
          .Name = "ObjectType"
          .InstanceName = "SceneInfo"
          .SetValue("Count", nSceneInfo)
        End With
        CRes.LlistaValues.Add(CValue)

        'GlobalSettings
        CValue = New FBXValue
        With CValue
          .Name = "ObjectType"
          .InstanceName = "GlobalSettings"
          .SetValue("Count", nGlobalSettings)
        End With
        CRes.LlistaValues.Add(CValue)


      Catch ex As Exception

      End Try
      Return CRes
    End Get

  End Property


  Public Connections As New FBXConnections
End Class
