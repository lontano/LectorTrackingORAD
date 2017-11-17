Public Class FBXHeader
  Inherits FBXValue

  Public CreationTimeStamps As FBXValue

  Public Sub New()
    Me.Name = "FBXHeaderExtension"
    Me.SetValue("FBXHeaderVersion", "1003")
    Me.SetValue("FBXVersion", "6100")
    Me.SetValueText("Creator", "Gelosoft " & My.Application.Info.ProductName & " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision)
    Me.SetValue("FBXHeaderVersion", "1003")

    CreationTimeStamps = Me.SetValue("CreationTimeStamp", "")
    CreationTimeStamps.SetValue("Version", "1000")
    CreationTimeStamps.SetValue("Year", Now.Year)
    CreationTimeStamps.SetValue("Month", Now.Month)
    CreationTimeStamps.SetValue("Day", Now.Day)
    CreationTimeStamps.SetValue("Hour", Now.Hour)
    CreationTimeStamps.SetValue("Minute", Now.Minute)
    CreationTimeStamps.SetValue("Second", Now.Second)
    CreationTimeStamps.SetValue("Millisecond", Now.Millisecond)
  End Sub

End Class