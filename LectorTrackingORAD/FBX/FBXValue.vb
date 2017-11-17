Public Class FBXValue
  Public Name As String
  Public Value As String
  Public InstanceName As String = ""
  Public InstanceSubName As String = ""
  Public LlistaValues As New List(Of FBXValue)
  Public LlistaProperties As New List(Of FBXProperty)

  Public Function GetFormattedString(Optional ByVal Level As Integer = 0) As String
    Dim sRes As String = ""
    Dim sTabs1 As String = ""
    Dim sName As String

    Try
      sName = Me.Name & ": "
      If Me.InstanceName <> "" And Me.InstanceSubName <> "" Then
        sName = sName & """" & Me.InstanceName & """, """ & Me.InstanceSubName & """"
      ElseIf Me.InstanceName <> "" Then
        sName = sName & """" & Me.InstanceName & """"
      ElseIf Me.InstanceSubName <> "" Then
        sName = sName & """" & Me.InstanceSubName & """"
      End If

      For i As Integer = 0 To Level - 1
        sTabs1 = sTabs1 & vbTab
      Next

      If (Me.LlistaValues.Count = 0 Or Me.LlistaProperties.Count = 0) And Not Me.Value Is Nothing Then
        sRes = sTabs1 & sName & " " & Me.Value & vbCrLf
      Else
        sRes = sTabs1 & sName & " {" & vbCrLf
        For Each CValue As FBXValue In Me.LlistaValues
          sRes = sRes & CValue.GetFormattedString(Level + 1)
        Next
        If Me.LlistaProperties.Count > 0 Then
          sRes = sRes & sTabs1 & vbTab & "Properties60: {" & vbCrLf
          For Each CProperty As FBXProperty In Me.LlistaProperties
            sRes = sRes & CProperty.GetFormattedString(Level + 2)
          Next
          sRes = sRes & sTabs1 & vbTab & "}" & vbCrLf
        End If
        sRes = sRes & sTabs1 & "}" & vbCrLf
      End If
    Catch ex As Exception
    End Try
    Return sRes
  End Function

#Region "Values"
  Public Function GetValue(ByVal siName As String, Optional ByVal biCreate As Boolean = False) As FBXValue
    Dim CValue As FBXValue = Nothing
    Try
      For nIndex As Integer = 0 To Me.LlistaValues.Count - 1
        If Me.LlistaValues(nIndex).Name = siName Then
          CValue = Me.LlistaValues(nIndex)
          Exit For
        End If
      Next

      If CValue Is Nothing Then
        CValue = New FBXValue
        CValue.Name = siName
        Me.LlistaValues.Add(CValue)
      End If

    Catch ex As Exception
    End Try
    Return CValue
  End Function

  Public Function SetValue(ByVal siName As String, ByVal siValue As String) As FBXValue
    Dim CValue As FBXValue = Nothing
    Try
      CValue = Me.GetValue(siName, True)
      CValue.Value = siValue
    Catch ex As Exception
    End Try
    Return CValue
  End Function

  Public Function SetValueText(ByVal siName As String, ByVal siValue As String) As FBXValue
    Dim CValue As FBXValue = Nothing
    Try
      CValue = Me.GetValue(siName, True)
      CValue.Value = """" & siValue & """"
    Catch ex As Exception
    End Try
    Return CValue
  End Function
#End Region


#Region "Properties"
  Public Function GetProperty(ByVal siName As String, Optional ByVal biCreate As Boolean = False) As FBXProperty
    Dim CProperty As FBXProperty = Nothing
    Try
      For nIndex As Integer = 0 To Me.LlistaProperties.Count - 1
        If Me.LlistaProperties(nIndex).Name = siName Then
          CProperty = Me.LlistaProperties(nIndex)
          Exit For
        End If
      Next

      If CProperty Is Nothing Then
        CProperty = New FBXProperty
        CProperty.Name = siName
        Me.LlistaProperties.Add(CProperty)
      End If

    Catch ex As Exception
    End Try
    Return CProperty
  End Function

  Public Function SetProperty(ByVal siName As String, ByVal siType As String, ByVal siSubName As String, ByVal ParamArray CiValues As Object()) As FBXProperty
    Dim CProperty As FBXProperty = Nothing
    Try
      CProperty = Me.GetProperty(siName, True)
      CProperty.Type = siType
      CProperty.SubName = siSubName
      CProperty.Values.Clear()
      For Each CAux As Object In CiValues
        CProperty.Values.Add(CAux)
      Next
    Catch ex As Exception
    End Try
    Return CProperty
  End Function

  Public Function SetPropertyText(ByVal siName As String, ByVal siProperty As String) As FBXProperty
    Dim CProperty As FBXProperty = Nothing
    Try
      CProperty = Me.GetProperty(siName, True)
      CProperty.Values.Add("""" & siProperty & """")
    Catch ex As Exception
    End Try
    Return CProperty
  End Function
#End Region


End Class
