Module MFunctions
  Public Function msToTimeCode(ByVal ms As Double) As String
    Dim sRes As String
    Try
      Dim nAux As Integer = ms
      Dim nFields As Integer = nAux Mod 2
      nAux = nAux \ 2
      Dim nFrames As Integer = nAux Mod 25
      nAux = nAux \ 25
      Dim nSeconds As Integer = nAux Mod 60
      nAux = nAux \ 60
      Dim nMinutes As Integer = nAux Mod 60
      nAux = nAux \ 60
      Dim nHours As Integer = nAux Mod 24

      If nHours > 0 Then
        sRes = nHours & ":" & nMinutes.ToString("00") & ":" & nSeconds.ToString("00") & "." & nFrames.ToString("00")
      Else
        sRes = nMinutes & ":" & nSeconds.ToString("00") & "." & nFrames.ToString("00")
      End If
      If nFrames = 1 Then sRes = sRes & "+"


    Catch ex As Exception

    End Try
    Return sRes
  End Function
End Module
