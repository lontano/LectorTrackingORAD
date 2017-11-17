Public Class FormTrackingPlayer
  Public CPuTrackingPlayer As TrackingPlayer.ATrackingPlayer

  Private Sub FormTrackingPlayer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    Me.CPuTrackingPlayer.SendValue(0)
  End Sub
End Class