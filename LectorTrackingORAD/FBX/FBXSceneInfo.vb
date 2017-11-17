Public Class FBXSceneInfo
  Inherits FBXValue

  Public Metadata As New FBXValue
  Private sPiFilePath As String = ""


  Public Sub New()
    Me.Name = "SceneInfo"
    Me.InstanceName = "SceneInfo::GlobalInfo"
    Me.InstanceSubName = "UserData"
    Me.SetValueText("Type", "UserData")
    Me.SetValue("Version", "100")

    Me.LlistaValues.Add(Metadata)

    Me.Metadata.Name = "Metadata"
    Me.Metadata.SetValue("Version", "100")
    Me.Metadata.SetValueText("Title", "")
    Me.Metadata.SetValueText("Subject", "")
    Me.Metadata.SetValueText("Author", "")
    Me.Metadata.SetValueText("Keywords", "")
    Me.Metadata.SetValueText("Revision", "")
    Me.Metadata.SetValueText("Comment", "")

    Me.SetProperty("DocumentUrl", "KString", "", "D:\GELO\3D_Exporters\Proves\cam02_ASCII_2006.FBX")
    Me.SetProperty("SrcDocumentUrl", "KString", "", "D:\GELO\3D_Exporters\Proves\cam02_ASCII_2006.FBX")
    Me.SetProperty("Original", "Compound", "")
    Me.SetProperty("Original|ApplicationVendor", "KString", "", My.Application.Info.CompanyName)
    Me.SetProperty("Original|ApplicationName", "KString", "", My.Application.Info.ProductName)
    Me.SetProperty("Original|ApplicationVersion", "KString", "", My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision)
    Me.SetProperty("Original|DateTime_GMT", "DateTime", "", Now.ToString("dd/MM/yyy H:mm:ss.fff"))
    Me.SetProperty("Original|FileName", "KString", "", "D:\GELO\3D_Exporters\Proves\cam02_ASCII_2006.FBX")
    Me.SetProperty("LastSaved", "Compound", "")
    Me.SetProperty("LastSaved|ApplicationVendor", "KString", "", My.Application.Info.CompanyName)
    Me.SetProperty("LastSaved|ApplicationName", "KString", "", My.Application.Info.ProductName)
    Me.SetProperty("LastSaved|ApplicationVersion", "KString", "", My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision)
    Me.SetProperty("LastSaved|DateTime_GMT", "DateTime", "", Now.ToString("dd/MM/yyy H:mm:ss.fff"))
  End Sub

  Public Property DocumentUrl() As String
    Get
      Return Me.sPiFilePath
    End Get
    Set(ByVal value As String)
      Me.sPiFilePath = value
      Dim CProp As FBXProperty
      CProp = Me.GetProperty("DocumentUrl")
      CProp.Values(0) = Me.sPiFilePath
      CProp = Me.GetProperty("SrcDocumentUrl")
      CProp.Values(0) = Me.sPiFilePath
      CProp = Me.GetProperty("Original|FileName")
      CProp.Values(0) = Me.sPiFilePath
    End Set
  End Property
End Class
