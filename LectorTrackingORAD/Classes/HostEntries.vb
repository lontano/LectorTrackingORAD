
Public Class HostEntries
  Private LlistaIPEntries As New List(Of String)
  Private LlistaHostEntries As New List(Of String)

  Public Function GetDNSHostEntry(ByVal siHost As String) As String
    Dim sAux As String = ""
    Dim bFound As Boolean = False
    Try
      For nIndex As Integer = 0 To Me.LlistaIPEntries.Count - 1
        If Me.LlistaIPEntries(nIndex) = siHost Then
          sAux = Me.LlistaHostEntries(nIndex)
          bFound = True
          Exit For
        End If
      Next
      If bFound = False Then
        Try
          sAux = System.Net.Dns.GetHostEntry(siHost).HostName
        Catch ex As Exception
          sAux = siHost
        End Try
        Me.LlistaIPEntries.Add(siHost)
        Me.LlistaHostEntries.Add(sAux)
      End If

    Catch ex As Exception

    End Try
    Return sAux
  End Function
End Class
