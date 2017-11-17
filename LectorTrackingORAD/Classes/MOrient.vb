Imports System.Math

Module MOrient
#Region "Vector"
  Public Structure _Vector
    Public x As Double
    Public y As Double
    Public z As Double
  End Structure

  Public Vector As _Vector
  Public VectorPtr As _Vector

  '/* Takes the modulus of v */
  Public Function VMod(ByRef v As _Vector) As Double
    Return (Sqrt(v.x * v.x + v.y * v.y + v.z * v.z))
  End Function

  '/* Returns the dot product of v1 & v2 */
  Public Function VDot(ByRef v1 As _Vector, ByRef v2 As _Vector) As Double
    Return (v1.x * v2.x + v1.y * v2.y + v1.z * v2.z)
  End Function

  '/* Fills the fields of a vector.	*/
  Public Sub VNew(ByRef a As Double, ByRef b As Double, ByRef c As Double, ByRef r As _Vector)
    r.x = a
    r.y = b
    r.z = c
  End Sub

  Public Function VNew(ByRef a As Double, ByRef b As Double, ByRef c As Double) As _Vector
    Dim r As New _Vector
    r.x = a
    r.y = b
    r.z = c
    Return r
  End Function


  Public Sub VAdd(ByRef v1 As _Vector, ByRef v2 As _Vector, ByRef r As _Vector)
    r.x = v1.x + v2.x
    r.y = v1.y + v2.y
    r.z = v1.z + v2.z
  End Sub

  Public Sub VSub(ByRef v1 As _Vector, ByRef v2 As _Vector, ByRef r As _Vector)
    r.x = v1.x - v2.x
    r.y = v1.y - v2.y
    r.z = v1.z - v2.z
  End Sub

  Public Sub VCross(ByRef v1 As _Vector, ByRef v2 As _Vector, ByRef r As _Vector)
    r.x = v1.y * v2.z - v1.z * v2.y
    r.y = v1.z * v2.x - v1.x * v2.z
    r.z = v1.x * v2.y - v1.y * v2.x
  End Sub

  Public Sub VScalarMul(ByRef v As _Vector, ByRef d As Double, ByRef r As _Vector)
    r.x = v.x * d
    r.y = v.y * d
    r.z = v.z * d
  End Sub

  Public Sub VUnit(ByRef v As _Vector, ByRef t As Double, ByRef r As _Vector)
    t = 1 / VMod(v)
    VScalarMul(v, t, r)
  End Sub

#End Region

#Region "Quaterion"
  Public Structure _Quaternion
    Public vect_part As _Vector
    Public real_part As Double
  End Structure

  Public Quaterion As _Quaternion

  Public Function Build_Rotate_Quaternion(ByRef axis As _Vector, ByRef cos_angle As Double) As _Quaternion

    Dim quat As New _Quaternion
    Dim sin_half_angle As Double
    Dim cos_half_angle As Double
    Dim angle As Double

    '/* The quaternion requires half angles. */
    If (cos_angle > 1.0) Then cos_angle = 1.0
    If (cos_angle < -1.0) Then cos_angle = -1.0
    angle = Acos(cos_angle)
    sin_half_angle = Sin(angle / 2)
    cos_half_angle = Cos(angle / 2)

    VScalarMul(axis, sin_half_angle, quat.vect_part)
    quat.real_part = cos_half_angle

    Return quat
  End Function

  Public Function QQMul(ByRef q1 As _Quaternion, ByRef q2 As _Quaternion) As _Quaternion

    Dim res As New _Quaternion
    Dim temp_v As New _Vector

    res.real_part = q1.real_part * q2.real_part - VDot(q1.vect_part, q2.vect_part)
    VCross(q1.vect_part, q2.vect_part, res.vect_part)
    VScalarMul(q1.vect_part, q2.real_part, temp_v)
    VAdd(temp_v, res.vect_part, res.vect_part)
    VScalarMul(q2.vect_part, q1.real_part, temp_v)
    VAdd(temp_v, res.vect_part, res.vect_part)

    Return res
  End Function

  Public Sub Quaternion_To_Axis_Angle(ByRef q As _Quaternion, ByRef axis As _Vector, ByRef angle As Double)

    Dim half_angle As Double
    Dim sin_half_angle As Double

    half_angle = Acos(q.real_part)
    sin_half_angle = Sin(half_angle)
    angle = half_angle * 2
    If (sin_half_angle < 0.00000001 And sin_half_angle > -0.00000001) Then
      VNew(1, 0, 0, axis)
    Else
      sin_half_angle = 1 / sin_half_angle
      VScalarMul(q.vect_part, sin_half_angle, axis)
    End If
  End Sub


  Public Sub Convert_Camera_Model(ByRef pos As _Vector, ByRef at As _Vector, ByRef up As _Vector, ByRef res_axis As _Vector, ByRef res_angle As Double)

    Dim n, v As New _Vector

    Dim rot_quat As New _Quaternion
    Dim norm_axis As New _Vector
    Dim norm_quat As New _Quaternion
    Dim inv_norm_quat As New _Quaternion
    Dim y_quat, new_y_quat, rot_y_quat As New _Quaternion
    Dim new_y As _Vector

    Dim temp_d As Double
    Dim temp_v As New _Vector

    '/* n = (norm)(pos - at) */
    VSub(at, pos, n)
    VUnit(n, temp_d, n)

    '/* v = (norm)(view_up - (view_up.n)n) */
    VUnit(up, temp_d, up)
    temp_d = VDot(up, n)
    VScalarMul(n, temp_d, temp_v)
    VSub(up, temp_v, v)
    VUnit(v, temp_d, v)

    VNew(n.y, -n.x, 0, norm_axis)
    If (VDot(norm_axis, norm_axis) < 0.00000001) Then

      '/* Already aligned, or maybe inverted. */
      If (n.z > 0.0) Then

        norm_quat.real_part = 0.0
        VNew(0, 1, 0, norm_quat.vect_part)

      Else

        norm_quat.real_part = 1.0
        VNew(0, 0, 0, norm_quat.vect_part)
      End If

    Else

      VUnit(norm_axis, temp_d, norm_axis)
      norm_quat = Build_Rotate_Quaternion(norm_axis, -n.z)
    End If

    '/* norm_quat now holds the rotation needed to line up the view directions.
    '/* We need to find the rotation to align the up vectors also. 	*/

    '/* Need to rotate the world y vector to see where it ends up. */
    '/* Find the inverse rotation. */
    inv_norm_quat.real_part = norm_quat.real_part
    VScalarMul(norm_quat.vect_part, -1, inv_norm_quat.vect_part)

    '/* Rotate the y. */
    y_quat.real_part = 0.0
    VNew(0, 1, 0, y_quat.vect_part)
    new_y_quat = QQMul(norm_quat, y_quat)
    new_y_quat = QQMul(new_y_quat, inv_norm_quat)
    new_y = new_y_quat.vect_part

    '/* Now need to find out how much to rotate about n to line up y. */
    VCross(new_y, v, temp_v)
    If (VDot(temp_v, temp_v) < 0.00000001) Then

      '/* The old and new may be pointing in the same or opposite. Need
      '** to generate a vector perpendicular to the old or new y.		*/
      VNew(0, -v.z, v.y, temp_v)
      If (VDot(temp_v, temp_v) < 0.00000001) Then
        VNew(v.z, 0, -v.x, temp_v)
      End If
    End If
    VUnit(temp_v, temp_d, temp_v)
    rot_y_quat = Build_Rotate_Quaternion(temp_v, VDot(new_y, v))

    '/* rot_y_quat holds the rotation about the initial camera direction needed
    '** to align the up vectors in the final position.	*/

    '/* Put the 2 rotations together. */
    rot_quat = QQMul(rot_y_quat, norm_quat)

    '/* Extract the axis and angle from the quaternion. */
    Quaternion_To_Axis_Angle(rot_quat, res_axis, res_angle)
  End Sub


#End Region
End Module
