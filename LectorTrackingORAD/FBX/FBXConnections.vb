Public Class FBXConnection
  Public Type As String
  Public Source As String
  Public Destination As String
  Public Parameter As String

  Public Sub New(ByVal siType As String, ByVal siSource As String, ByVal siDestination As String, ByVal siParameter As String)
    Me.Type = siType
    Me.Source = siSource
    Me.Destination = siDestination
    Me.Parameter = siParameter
  End Sub
End Class

Public Class FBXConnections

  Public LlistaConnections As New List(Of FBXConnection)

  Public Function GetFormattedString(ByVal niLevel As Integer) As String
    Dim sRes As String = ""
    Try
      Dim CRes As New FBXValue
      CRes.Name = "Connections"
      Dim nModels As Integer = 0
      Dim nGlobalSettings As Integer = 0
      Dim nSceneInfo As Integer = 0
      Dim CValue As FBXValue
      For Each CAux As FBXConnection In Me.LlistaConnections
        CValue = New FBXValue
        CValue.Name = "Connect"
        CValue.Value = """" & CAux.Type & """, """ & CAux.Source & """, """ & CAux.Destination & """"
        If CAux.Parameter <> "" Then
          CValue.Value = CValue.Value & ", """ & CAux.Parameter & """"
        End If
        CRes.LlistaValues.Add(CValue)
      Next

      sRes = CRes.GetFormattedString(niLevel)

    Catch ex As Exception

    End Try
    Return sRes
  End Function
End Class
