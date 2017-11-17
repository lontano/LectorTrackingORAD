Public Class FormTrackingPlayer
  Public CPuTrackingPlayer As ATrackingPlayer
  Public WithEvents CPuTrackingListener As TrackingListener.ATrackingListener
  Public WithEvents CPuTrackingRouterManager As TrackingRouter.TrackingRouterManager = gTrackingRouterManager
  Public WithEvents CPuTrackingPlayerFactory As TrackingPlayerFactory = gTrackingPlayerFactory

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    Me.CPuTrackingRouterManager.SendLastValue()
  End Sub

  Private Sub CPuTrackingListener_ValueReceived(ByVal CiValue As TrackingValue) Handles CPuTrackingListener.ValueReceived
    Debug.Print("Received value " & CiValue.CAM_NUM)
  End Sub

  Private Enum eSourceCols
    Host
    Studio
    CAM
    Total
  End Enum

  Private Enum ePlayerCols
    Name
    Host
    Port
    Protocol
    Total
  End Enum

#Region "Sources menu"
  Private Sub AfegirFontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AfegirFontToolStripMenuItem.Click
    Dim dlg As New DialogAddSource
    dlg.CPuTrackingFile = gTrackingFile
    dlg.ShowDialog(Me)
    If dlg.DialogResult = DialogResult.OK Then
      For Each CTrackingHost As TrackingHost In dlg.LlistaSelectedHosts
        Me.CPuTrackingRouterManager.CreateRouter(CTrackingHost)
      Next
    End If
    Me.UpdateSources()
    DesarConfiguracio(gnNumConfig, gudtCnfg)
  End Sub

  Private Sub EliminarFontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminarFontToolStripMenuItem.Click
    Try
      For nIndex As Integer = 0 To Me.ListViewSources.SelectedIndices.Count - 1
        Me.CPuTrackingRouterManager.LlistaRouters.RemoveAt(nIndex)
      Next
      Me.UpdateSources()
      DesarConfiguracio(gnNumConfig, gudtCnfg)
    Catch ex As Exception

    End Try
  End Sub
#End Region
  

  Private nPiSelectedSource As Integer = -1
  Private CPiSelectedRoute As TrackingRouter.TrackingRouter

  Private Sub UpdateSources()
    With Me.ListViewSources
      Dim CItem As ListViewItem
      .Items.Clear()
      For nIndex As Integer = 0 To Me.CPuTrackingRouterManager.LlistaRouters.Count - 1
        CItem = .Items.Add(Me.CPuTrackingRouterManager.LlistaRouters(nIndex).TrackingHost.Host)
        CItem.SubItems.Add(Me.CPuTrackingRouterManager.LlistaRouters(nIndex).TrackingHost.Studio)
        CItem.SubItems.Add(Me.CPuTrackingRouterManager.LlistaRouters(nIndex).TrackingHost.CamNumber)
      Next
    End With
  End Sub

  Private Sub ListViewSources_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListViewSources.SelectedIndexChanged
    If ListViewSources.SelectedIndices.Count = 1 Then
      Me.nPiSelectedSource = ListViewSources.SelectedIndices(0)
      CPiSelectedRoute = Me.CPuTrackingRouterManager.LlistaRouters(Me.nPiSelectedSource)
    Else
      Me.nPiSelectedSource = -1
      CPiSelectedRoute = Nothing
    End If
    Me.UpdateRouter()
  End Sub

  Private Sub UpdateRouter()
    Try
      Dim CItem As ListViewItem
      With Me.ListViewPlayers
        .Items.Clear()
        If Not Me.CPiSelectedRoute Is Nothing Then
          For nIndex As Integer = 0 To Me.CPiSelectedRoute.TrackingPlayerHosts.Count - 1
            CItem = .Items.Add(Me.CPiSelectedRoute.TrackingPlayerHosts(nIndex).AliasName)
            CItem.SubItems.Add(Me.CPiSelectedRoute.TrackingPlayerHosts(nIndex).broadcastAddress)
            CItem.SubItems.Add(Me.CPiSelectedRoute.TrackingPlayerHosts(nIndex).BroadcastPort)
            CItem.SubItems.Add(Me.CPiSelectedRoute.TrackingPlayerHosts(nIndex).Type.ToString)
          Next
        End If
      End With
    Catch ex As Exception

    End Try
  End Sub

  Private Sub CheckBoxRunning_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxRunning.CheckedChanged
    Me.CPuTrackingRouterManager.Enabled = Me.CheckBoxRunning.Checked
  End Sub

  Private Sub TimerForceSend_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerForceSend.Tick
    If Me.CheckBoxForceSend.Checked Then
      If Me.CPuTrackingRouterManager.Enabled Then
        Me.CPuTrackingRouterManager.SendLastValue()
      End If
    End If
    TimerForceSend.Interval = 1
  End Sub

  Private Sub FormTrackingPlayer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Me.UpdateSources()
    Me.UpdateRouter()
  End Sub

  Private Sub CheckBoxForceSend_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxForceSend.CheckedChanged
    Me.TimerForceSend.Enabled = Me.CheckBoxForceSend.Checked
  End Sub

#Region "Players"
  Private Sub AfegirPlayerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AfegirPlayerToolStripMenuItem.Click
    If Not CPiSelectedRoute Is Nothing Then
      If gTrackingRouterManager.Enabled = True Then
        MsgBox("No es pot editar en aquest estat")
        Exit Sub
      End If
      Dim dlg As New DialogAddPlayer
      dlg.CPuPlayerHost = Nothing
      dlg.ShowDialog(Me)
      If dlg.DialogResult = DialogResult.OK Then
        CPiSelectedRoute.AddTrackingPlayerHost(dlg.CPuPlayerHost)
        UpdateRouter()
        DesarConfiguracio(gnNumConfig, gudtCnfg)
      End If
    End If
  End Sub

  Private Sub EliminarPlayerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminarPlayerToolStripMenuItem.Click
    Try
      If Not CPiSelectedRoute Is Nothing Then
        If gTrackingRouterManager.Enabled = True Then
          MsgBox("No es pot editar en aquest estat")
          Exit Sub
        End If
        For nIndex As Integer = 0 To Me.ListViewPlayers.SelectedIndices.Count - 1
          Me.CPiSelectedRoute.TrackingPlayerHosts.RemoveAt(Me.ListViewPlayers.SelectedIndices(nIndex))
        Next
        UpdateRouter()
        DesarConfiguracio(gnNumConfig, gudtCnfg)
      End If
    Catch ex As Exception

    End Try
  End Sub

  Private Sub EditarPlayerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditarToolStripMenuItem.Click
    Try
      If Not CPiSelectedRoute Is Nothing And Me.ListViewPlayers.SelectedIndices.Count > 0 Then
        If gTrackingRouterManager.Enabled = True Then
          MsgBox("No es pot editar en aquest estat")
          Exit Sub
        End If

        Dim dlg As New DialogAddPlayer
        Dim nIndex As Integer = Me.ListViewPlayers.SelectedIndices(0)
        dlg.CPuPlayerHost = Me.CPiSelectedRoute.TrackingPlayerHosts(nIndex)
        dlg.ShowDialog(Me)
        If dlg.DialogResult = DialogResult.OK Then
          Me.CPiSelectedRoute.TrackingPlayerHosts(nIndex) = dlg.CPuPlayerHost
          UpdateRouter()
          DesarConfiguracio(gnNumConfig, gudtCnfg)
        End If
      End If
    Catch ex As Exception

    End Try
  End Sub
#End Region
End Class