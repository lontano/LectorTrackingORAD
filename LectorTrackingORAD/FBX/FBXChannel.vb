Public Class FBXChannel
  Public Name As String
  Public LlistaChannels As New List(Of FBXChannel)
  Public LayerType As String = "1"
  Public DefaultValue As String = "0"
  Public KeyVer As String = "4005"

  Public Hierarchy As New List(Of String)
  Public LlistaKeys As New List(Of FBXKey)

  Public Sub New(ByVal ParamArray asParams As String())
    Try
      Me.Hierarchy.Clear()

      For Each sAux As String In asParams
        Me.Hierarchy.Add(sAux)
      Next
    Catch ex As Exception

    End Try
  End Sub

  Public ReadOnly Property KeyCount() As Integer
    Get
      Return LlistaKeys.Count
    End Get
  End Property

  Public Sub AddKey(ByVal diTime As Double, ByVal diValue As Double)
    Me.LlistaKeys.Add(New FBXKey(diTime, diValue))
  End Sub

  Public Function GetFBXValue() As FBXValue
    Dim CRes As New FBXValue
    Try
      Dim CRootValue As FBXValue = CRes
      Dim CAuxValue As FBXValue
      Dim sHierarchy As String = ""
      If Me.LlistaChannels.Count > 0 Then
        If Me.Hierarchy.Count > 0 Then
          sHierarchy = Me.Hierarchy(0)
          CRootValue.Name = "Channel"
          CRootValue.InstanceName = sHierarchy
        End If
        For Each CAux As FBXChannel In Me.LlistaChannels
          CRes.LlistaValues.Add(CAux.GetFBXValue)
        Next
      Else
        If Me.Hierarchy.Count > 0 Then
          sHierarchy = Me.Hierarchy(0)
          CRootValue.Name = "Channel"
          CRootValue.InstanceName = sHierarchy
          For nIndex As Integer = 1 To Me.Hierarchy.Count - 1
            sHierarchy = Me.Hierarchy(nIndex)
            CAuxValue = New FBXValue
            CAuxValue.Name = "Channel"
            CAuxValue.InstanceName = sHierarchy
            CRootValue.LlistaValues.Add(CAuxValue)
            CRootValue = CAuxValue
          Next
        End If

        CRootValue.SetValue("Default", Me.DefaultValue)
        CRootValue.SetValue("KeyVer", Me.KeyVer)
        CRootValue.SetValue("KeyCount", Me.KeyCount)
        CRootValue.SetValue("Color", "1,1,1")

        Dim CExtrapolation As New FBXValue
        CExtrapolation.Name = "Post-Extrapolation"
        CExtrapolation.SetValue("Type", "C")
        CExtrapolation.SetValue("Repetition", "1")
        CRootValue.LlistaValues.Add(CExtrapolation)

        Dim sKeys As String = ""
        For Each CKey As FBXKey In Me.LlistaKeys
          sKeys = sKeys & CKey.GetFormattedText & ","
        Next
        CRootValue.SetValue("Key", sKeys)


      End If

    Catch ex As Exception

    End Try
    Return CRes
  End Function
End Class
