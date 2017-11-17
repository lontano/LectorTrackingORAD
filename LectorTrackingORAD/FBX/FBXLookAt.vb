Public Class FBXLookAt
  Inherits FBXValue
  Private sPiModelName As String = "Camerax01"

  Public Sub New()
    Me.SetValue("Version", "232")
    Me.SetValue("MultiLayer", "0")
    Me.SetValue("MultiTake", "0")
    Me.SetValue("Shading", "T")
    Me.SetValue("Culling", """CullingOff""")
    Me.SetValue("TypeFlags", """Null""")

    Me.SetProperty("QuaternionInterpolate", "bool", "", 0)
    Me.SetProperty("RotationOffset", "Vector3D", "", 0, 0, 0)
    Me.SetProperty("RotationPivot", "Vector3D", "", 0, 0, 0)
    Me.SetProperty("ScalingOffset", "Vector3D", "", 0, 0, 0)
    Me.SetProperty("ScalingPivot", "Vector3D", "", 0, 0, 0)
    Me.SetProperty("TranslationActive", "bool", "", 0)
    Me.SetProperty("TranslationMin", "Vector3D", "", 0, 0, 0)
    Me.SetProperty("TranslationMax", "Vector3D", "", 0, 0, 0)
    Me.SetProperty("TranslationMinX", "bool", "", 0)
    Me.SetProperty("TranslationMinY", "bool", "", 0)
    Me.SetProperty("TranslationMinZ", "bool", "", 0)
    Me.SetProperty("TranslationMaxX", "bool", "", 0)
    Me.SetProperty("TranslationMaxY", "bool", "", 0)
    Me.SetProperty("TranslationMaxZ", "bool", "", 0)
    Me.SetProperty("RotationOrder", "enum", "", 0)
    Me.SetProperty("RotationSpaceForLimitOnly", "bool", "", 0)
    Me.SetProperty("RotationStiffnessX", "double", "", 0)
    Me.SetProperty("RotationStiffnessY", "double", "", 0)
    Me.SetProperty("RotationStiffnessZ", "double", "", 0)
    Me.SetProperty("AxisLen", "double", "", 10)
    Me.SetProperty("PreRotation", "Vector3D", "", -90, 0, 0)
    Me.SetProperty("PostRotation", "Vector3D", "", -0, -90, 0)
    Me.SetProperty("RotationActive", "bool", "", 1)
    Me.SetProperty("RotationMin", "Vector3D", "", 0, 0, 0)
    Me.SetProperty("RotationMax", "Vector3D", "", 0, 0, 0)
    Me.SetProperty("RotationMinX", "bool", "", 0)
    Me.SetProperty("RotationMinY", "bool", "", 0)
    Me.SetProperty("RotationMinZ", "bool", "", 0)
    Me.SetProperty("RotationMaxX", "bool", "", 0)
    Me.SetProperty("RotationMaxY", "bool", "", 0)
    Me.SetProperty("RotationMaxZ", "bool", "", 0)
    Me.SetProperty("InheritType", "enum", "", 1)
    Me.SetProperty("ScalingActive", "bool", "", 0)
    Me.SetProperty("ScalingMin", "Vector3D", "", 1, 1, 1)
    Me.SetProperty("ScalingMax", "Vector3D", "", 1, 1, 1)
    Me.SetProperty("ScalingMinX", "bool", "", 0)
    Me.SetProperty("ScalingMinY", "bool", "", 0)
    Me.SetProperty("ScalingMinZ", "bool", "", 0)
    Me.SetProperty("ScalingMaxX", "bool", "", 0)
    Me.SetProperty("ScalingMaxY", "bool", "", 0)
    Me.SetProperty("ScalingMaxZ", "bool", "", 0)
    Me.SetProperty("GeometricTranslation", "Vector3D", "", 0, 0, 0)
    Me.SetProperty("GeometricRotation", "Vector3D", "", 0, 0, 0)
    Me.SetProperty("GeometricScaling", "Vector3D", "", 1, 1, 1)
    Me.SetProperty("MinDampRangeX", "double", "", 0)
    Me.SetProperty("MinDampRangeY", "double", "", 0)
    Me.SetProperty("MinDampRangeZ", "double", "", 0)
    Me.SetProperty("MaxDampRangeX", "double", "", 0)
    Me.SetProperty("MaxDampRangeY", "double", "", 0)
    Me.SetProperty("MaxDampRangeZ", "double", "", 0)
    Me.SetProperty("MinDampStrengthX", "double", "", 0)
    Me.SetProperty("MinDampStrengthY", "double", "", 0)
    Me.SetProperty("MinDampStrengthZ", "double", "", 0)
    Me.SetProperty("MaxDampStrengthX", "double", "", 0)
    Me.SetProperty("MaxDampStrengthY", "double", "", 0)
    Me.SetProperty("MaxDampStrengthZ", "double", "", 0)
    Me.SetProperty("PreferedAngleX", "double", "", 0)
    Me.SetProperty("PreferedAngleY", "double", "", 0)
    Me.SetProperty("PreferedAngleZ", "double", "", 0)
    Me.SetProperty("LookAtProperty", "object", "")
    Me.SetProperty("UpVectorProperty", "object", "")
    Me.SetProperty("Show", "bool", "", 1)
    Me.SetProperty("NegativePercentShapeSupport", "bool", "", 1)
    Me.SetProperty("DefaultAttributeIndex", "int", "", 0)
    Me.SetProperty("Freeze", "bool", "", 0)
    Me.SetProperty("LODBox", "bool", "", 0)
    Me.SetProperty("Lcl Translation", "Lcl Translation", "A+", 4.01245498657227, -3.21262097358704, -1.07940304279327)
    Me.SetProperty("Lcl Rotation", "Lcl Rotation", "A+", 79.0858932112017, -1.62263409563818, -52.5885703092691)
    Me.SetProperty("Lcl Scaling", "Lcl Scaling", "A+", 1, 1, 1)
    Me.SetProperty("Visibility", "Visibility", "A+", 1)
    Me.SetProperty("MaxHandle", "int", "UH", 1)
  End Sub


  Public Property ModelName() As String
    Get
      Return Me.sPiModelName
    End Get
    Set(ByVal value As String)
      Me.sPiModelName = value
      Me.SetValue("NodeAttributeName", """NodeAttribute::" & Me.ModelName & "_ncl1_1""")
    End Set
  End Property

  Public Sub SetPosition(ByVal X As Double, ByVal Y As Double, ByVal z As Double)
    Me.SetProperty("Lcl Translation", "Lcl Translation", "A+", X, Y, z)
    Me.SetValue("Position", CInt(X) & "," & CInt(Y) & "," & CInt(z))

  End Sub

  Public Sub SetRotation(ByVal X As Double, ByVal Y As Double, ByVal z As Double)
    Me.SetProperty("Lcl Rotation", "Lcl Rotation", "A+", X, Y, z)
  End Sub

  Public Sub SetTarget(ByVal X As Double, ByVal Y As Double, ByVal z As Double)
    Me.SetValue("LookAt", CInt(X) & "," & CInt(Y) & "," & CInt(z))
  End Sub
End Class
