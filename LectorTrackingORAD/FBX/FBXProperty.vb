Public Class FBXProperty
  Private sPiName As String
  Private sPiSubName As String
  Private LlistaValues As New List(Of String)

  Public Type As String

  Public Property Name() As String
    Get
      Return Me.sPiName
    End Get
    Set(ByVal value As String)
      Me.sPiName = value
    End Set
  End Property

  Public Property SubName() As String
    Get
      Return Me.sPiSubName
    End Get
    Set(ByVal value As String)
      Me.sPiSubName = value
    End Set
  End Property

  Public Property Values() As List(Of String)
    Get
      Return Me.LlistaValues
    End Get
    Set(ByVal value As List(Of String))
      Me.LlistaValues = value
    End Set
  End Property

  Public Sub New()
  End Sub

  Public Sub New(ByVal ParamArray init_values As Object())
    Try
      If init_values.Length >= 4 Then
        Me.Name = CStr(init_values(0))
        Me.Type = CStr(init_values(1))
        Me.SubName = CStr(init_values(2))
        Me.LlistaValues.Clear()
        For nIndex As Integer = 0 To init_values.Length - 3
          Me.LlistaValues.Add(init_values(3 + nIndex))
        Next
      End If
    Catch ex As Exception

    End Try
  End Sub

  Public Function GetFormattedString(Optional ByVal Level As Integer = 0) As String
    Dim sRes As String = ""
    Dim sTabs1 As String = ""
    Try

      For i As Integer = 0 To Level - 1
        sTabs1 = sTabs1 & vbTab
      Next

      sRes = sTabs1 & "Property: "
      sRes = sRes & """" & Me.sPiName & """, "
      sRes = sRes & """" & Me.Type & """, "
      sRes = sRes & """" & Me.sPiSubName & """"

      If Me.Values.Count > 0 Then
        sRes = sRes & ", "
        For nIndex As Integer = 0 To Me.Values.Count - 1
          sRes = sRes & FormattedString(Me.Values(nIndex), Me.Type)
          If nIndex < Me.Values.Count - 1 Then
            sRes = sRes & ", "
          End If
        Next
      End If
      
      sRes = sRes & vbCrLf
    Catch ex As Exception

    End Try
    Return sRes
  End Function

  Private Function FormattedString(ByVal siValue As String, ByVal siType As String) As String
    Dim sRes As String = ""
    Dim dAux As Single
    Try
      Select Case siType
        Case "KString"
          sRes = """" & siValue & """"
        Case "DateTime"
          sRes = """" & siValue & """"
        Case "Vector", "Vector3D", "Lcl Translation", "Lcl Rotation", "Lcl Scaling"
          sRes = siValue.Replace(",", ".")
        Case Else
          'no conec el tipus, a veure si el podem deduir...
          If siValue = "0" Then
            sRes = siValue
          ElseIf siValue = "" Then
            sRes = """"""
          Else
            Try
              dAux = CSng("0" & siValue.Replace(",", "."))
            Catch ex As Exception
              dAux = 0
            End Try
            If dAux <> 0 Then
              sRes = siValue.Replace(",", ".")
            Else
              sRes = """" & siValue & """"
            End If
          End If
      End Select
     
    Catch ex As Exception

    End Try
    Return sRes
  End Function
End Class
