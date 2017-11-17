Public Class FBXTakes
  Public LlistaTakes As New List(Of FBXTake)

  Public Function GetFBXValue() As FBXValue
    Dim CRes As New FBXValue
    Try
      CRes.Name = "Takes"
      CRes.SetValueText("Current", "")
      Dim CValue As FBXValue
      For Each CTake As FBXTake In Me.LlistaTakes
        CValue = CTake.GetFBXValue
        CRes.LlistaValues.Add(CValue)
      Next

    Catch ex As Exception

    End Try
    Return CRes
  End Function
End Class
