Public Class FormExportProgress
  Public Sub showProgress(ByVal diValue1 As Double, ByVal diValue2 As Double, ByVal siText As String)
    If 100 * diValue1 >= 0 And 100 * diValue1 <= 100 Then
      Me.ProgressBar1.Value = CInt(100 * diValue1)
    End If
    If 100 * diValue2 >= 0 And 100 * diValue2 <= 100 Then
      Me.ProgressBar2.Value = CInt(100 * diValue2)
    End If
    Me.Text = siText
  End Sub
End Class