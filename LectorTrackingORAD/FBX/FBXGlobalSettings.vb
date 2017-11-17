Public Class FBXGlobalSettings
  Inherits FBXValue

  Public Sub New()
    Me.Name = "GlobalSettings"
    Me.SetValue("Version", "1000")
    Me.SetProperty("UpAxis", "int", "", 1)
    Me.SetProperty("UpAxisSign", "int", "", 1)
    Me.SetProperty("FrontAxis", "int", "", 2)
    Me.SetProperty("FrontAxisSign", "int", "", 1)
    Me.SetProperty("CoordAxis", "int", "", 0)
    Me.SetProperty("CoordAxisSign", "int", "", 1)
    Me.SetProperty("OriginalUpAxis", "int", "", 2)
    Me.SetProperty("OriginalUpAxisSign", "int", "", 1)
    Me.SetProperty("UnitScaleFactor", "double", "", 2.54)
    Me.SetProperty("OriginalUnitScaleFactor", "double", "", 2.54)
    Me.SetProperty("AmbientColor", "ColorRGB", "", 0, 0, 0)
    Me.SetProperty("DefaultCamera", "KString", "", "Producer Perspective")
    Me.SetProperty("TimeMode", "enum", "", 6)
    Me.SetProperty("TimeSpanStart", "KTime", "", 0)
    Me.SetProperty("TimeSpanStop", "KTime", "", 153953860000)
  End Sub
End Class
