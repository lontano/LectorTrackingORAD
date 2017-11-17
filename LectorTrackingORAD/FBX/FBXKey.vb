Public Class FBXKey
  Public Time As Single
  Public Value As Single

  Public Sub New()

  End Sub

  Public Sub New(ByVal diTime As Double, ByVal diValue As Double)
    Me.Time = diTime
    Me.Value = diValue
  End Sub

  Public Function GetFormattedText() As String
    Dim sRes As String = ""
    'Time
    sRes = Format((38488465 * Me.Time), "#####################").Replace(",", ".")
    'Value
    sRes = sRes & "," & Me.Value.ToString.Replace(",", ".")
    'Interpolation method 'C' (Constant), 'L' (linear) or 'U' (used defined)
    sRes = sRes & ",L"
    ''In case of U: 's' (unified tangents), 'b' (broken tangents
    'sRes = sRes & ",s"
    ''direction of the rigth tangent of the current key, amplitude that the tangent would hve at the current time + 1 second
    'sRes = sRes & ",0"
    ''direction of the left tangent of the next key, amplitude that the tangent would hve at the current time + 1 second
    'sRes = sRes & ",0"
    ''character 'a', no meaning
    'sRes = sRes & ",a"
    ''horizontal amplitude of the right tangent of the current key (0 to 1, 1 is the distance to the next keyframe)
    'sRes = sRes & ",0"
    ''horizontal amplitude of the left tangent of the next key (0 to 1, 1 is the distance to the next keyframe)
    'sRes = sRes & ",0"

    Return sRes
  End Function
End Class
