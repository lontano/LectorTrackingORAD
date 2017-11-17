Public Class FBXTakeModel
  Public Version As String = "1.1"
  Public Model As FBXValue
  Public LlistaChannels As New List(Of FBXChannel)

  Public ChannelTransform As FBXChannel
  Public ChannelTransformT As FBXChannel
  Public ChannelTransformX As FBXChannel
  Public ChannelTransformY As FBXChannel
  Public ChannelTransformZ As FBXChannel

  Public ChannelRotateX As FBXChannel
  Public ChannelRotateY As FBXChannel
  Public ChannelRotateZ As FBXChannel

  Public ChannelFOV As FBXChannel
  Public ChannelFOVX As FBXChannel
  Public ChannelFOVY As FBXChannel


  Public Sub New()
    ChannelTransform = New FBXChannel("Transform")
    ChannelTransformT = New FBXChannel("T")

    Me.ChannelTransformX = New FBXChannel("X")
    Me.ChannelTransformY = New FBXChannel("Y")
    Me.ChannelTransformZ = New FBXChannel("Z")

    Me.ChannelTransform.LlistaChannels.Add(Me.ChannelTransformT)
    Me.ChannelTransformT.LlistaChannels.Add(Me.ChannelTransformX)
    Me.ChannelTransformT.LlistaChannels.Add(Me.ChannelTransformY)
    Me.ChannelTransformT.LlistaChannels.Add(Me.ChannelTransformZ)

    'Me.ChannelRotateX = New FBXChannel("Transform", "R", "X")
    'Me.ChannelRotateY = New FBXChannel("Transform", "R", "Y")
    'Me.ChannelRotateZ = New FBXChannel("Transform", "R", "Z")

    Me.ChannelFOV = New FBXChannel("FieldOfView")
    'Me.ChannelFOVX = New FBXChannel("FieldOfView")
    'Me.ChannelFOVY = New FBXChannel("FieldOfView")

    Me.LlistaChannels.Add(Me.ChannelTransform)

    'LlistaChannels.Add(Me.ChannelRotateX)
    'LlistaChannels.Add(Me.ChannelRotateY)
    'LlistaChannels.Add(Me.ChannelRotateZ)

    Me.LlistaChannels.Add(Me.ChannelFOV)
    'LlistaChannels.Add(Me.ChannelFOVX)
    'LlistaChannels.Add(Me.ChannelFOVY)
  End Sub


  Public Function GetFBXValue() As FBXValue
    Dim CRes As New FBXValue
    Try
      CRes.Name = "Model"
      CRes.InstanceName = Me.Model.InstanceName
      CRes.SetValue("Version", "1.1")

      For Each CChannel As FBXChannel In Me.LlistaChannels
        CRes.LlistaValues.Add(CChannel.GetFBXValue)
      Next
    Catch ex As Exception

    End Try
    Return CRes
  End Function
End Class
