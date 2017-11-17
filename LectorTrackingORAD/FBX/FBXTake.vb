Public Class FBXTake
  Public FileName As String
  Public LocalTime As String
  Public ReferenceTime As String
  Public LlistaModelTakes As New List(Of FBXTakeModel)

  Public Function GetFBXValue() As FBXValue
    Dim CRes As New FBXValue
    Try
      CRes.Name = "Take"
      CRes.InstanceName = "Take 001"
      CRes.SetValueText("FileName", "Take_001.tak")
      CRes.SetValue("LocalTime", 0.954513932)
      CRes.SetValue("ReferenceTime", 0.954513932)

      Dim CValue As FBXValue
      For Each CTakeModel As FBXTakeModel In Me.LlistaModelTakes
        CValue = CTakeModel.GetFBXValue
        CRes.LlistaValues.Add(CValue)

      Next
    Catch ex As Exception

    End Try
    Return CRes
  End Function

End Class
