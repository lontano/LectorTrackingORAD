Imports System.Windows.Forms

Friend Class DialogOpcions
  Private sPiSelectedHost As String = ""
  Public tPuConfiguracio As Configuracio

  Private bPiInit As Boolean = False

  Private Enum eCols
    Studio
    Cam
    Host
    IP
    SourcePort
    TargetPort
    Protocol
    Total
  End Enum

  Private Sub MostrarOpcions()
    bPiInit = True
    Try
      Me.CheckBoxHosts.Checked = tPuConfiguracio.UseHosts

      With Me.ListView1
        .Items.Clear()
        For Each CHost As TrackingHost In tPuConfiguracio.Hosts.LlistaHosts
          Dim CItem As ListViewItem = .Items.Add(CHost.Studio)
          CItem.SubItems.Add(CHost.CamNumber)

          If CHost.TrackingType = eTrackingType.UDP_ORAD Then
            CItem.SubItems.Add(CHost.Host)
            CItem.SubItems.Add(CHost.IP)
            CItem.SubItems.Add(CHost.SourcePort)
            CItem.SubItems.Add("")
          Else
            CItem.SubItems.Add("")
            CItem.SubItems.Add("")
            CItem.SubItems.Add("")
            CItem.SubItems.Add(CHost.TargetPort)
          End If
          CItem.SubItems.Add(CHost.TrackingType.ToString)
        Next
      End With
      
    Catch ex As Exception
    End Try
    bPiInit = False
  End Sub

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub AfegirHostToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AfegirHostToolStripMenuItem.Click
    Try
      Dim dlg As New DialogHost
      dlg.CPuHost = Nothing
      dlg.ShowDialog(Me)
      If dlg.DialogResult = Windows.Forms.DialogResult.OK Then
        tPuConfiguracio.Hosts.LlistaHosts.Add(dlg.CPuHost)
      End If


      Me.MostrarOpcions()
    Catch ex As Exception

    End Try
  End Sub

  Private Sub EliminarHostToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EliminarHostToolStripMenuItem.Click
    Try
      Dim CHost As TrackingHost
      
      If Me.ListView1.SelectedItems.Count = 1 Then
        Dim sHost As String = Me.ListView1.SelectedItems(0).SubItems(eCols.Host).Text
        Dim nSourcePort As Integer = CInt("0" & Me.ListView1.SelectedItems(0).SubItems(eCols.SourcePort).Text)
        Dim nTargetPort As Integer = CInt("0" & Me.ListView1.SelectedItems(0).SubItems(eCols.TargetPort).Text)
        Dim eType As eTrackingType
        Select Case Me.ListView1.SelectedItems(0).SubItems(eCols.Protocol).Text
          Case eTrackingType.TCP_Client.ToString
            eType = eTrackingType.TCP_Client
          Case eTrackingType.UDP.ToString
            eType = eTrackingType.UDP
          Case eTrackingType.UDP_ORAD.ToString
            eType = eTrackingType.UDP_ORAD
        End Select
        CHost = Me.tPuConfiguracio.Hosts.GetHost(sHost, nSourcePort, nTargetPort, eType)

        If MsgBox("Estàs segur?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
          Me.tPuConfiguracio.Hosts.LlistaHosts.Remove(CHost)
        End If
      End If

      Me.MostrarOpcions()
    Catch ex As Exception

    End Try
  End Sub

  Private Sub EditarHostToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditarHostToolStripMenuItem.Click
    Try
      Dim CHost As TrackingHost
      If Me.ListView1.SelectedItems.Count = 1 Then
        Dim sHost As String = Me.ListView1.SelectedItems(0).SubItems(eCols.Host).Text
        Dim nSourcePort As Integer = CInt("0" & Me.ListView1.SelectedItems(0).SubItems(eCols.SourcePort).Text)
        Dim nTargetPort As Integer = CInt("0" & Me.ListView1.SelectedItems(0).SubItems(eCols.TargetPort).Text)
        Dim eType As eTrackingType
        Select Case Me.ListView1.SelectedItems(0).SubItems(eCols.Protocol).Text
          Case eTrackingType.TCP_Client.ToString
            eType = eTrackingType.TCP_Client
          Case eTrackingType.TCP_Server.ToString
            eType = eTrackingType.TCP_Server
          Case eTrackingType.UDP.ToString
            eType = eTrackingType.UDP
          Case eTrackingType.UDP_ORAD.ToString
            eType = eTrackingType.UDP_ORAD
        End Select
        CHost = Me.tPuConfiguracio.Hosts.GetHost(sHost, nSourcePort, nTargetPort, eType)
        Dim dlg As New DialogHost
        If Not CHost Is Nothing Then
          dlg.CPuHost = CHost
          dlg.ShowDialog(Me)
          If dlg.DialogResult = Windows.Forms.DialogResult.OK Then
            'tPuConfiguracio.Hosts.LlistaHosts.Add(dlg.CPuHost)
          End If
        End If
      End If

      Me.MostrarOpcions()
    Catch ex As Exception

    End Try
  End Sub

  Private Sub DialogOpcions_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Me.MostrarOpcions()
  End Sub

#Region "Scan ports"
  Private WithEvents dlgScan As New DialogScanPorts

  Private Sub ButtonScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonScan.Click
    If dlgScan Is Nothing Then
      dlgScan = New DialogScanPorts
    End If
    dlgScan.CPuHosts = tPuConfiguracio.Hosts
    dlgScan.Show(Me)
    Me.ButtonScan.Enabled = False
  End Sub

  Private Sub dlgScan_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles dlgScan.FormClosed
    Me.ButtonScan.Enabled = True
    dlgScan = Nothing
  End Sub

  Private Sub dlgScan_SelectHost(ByVal siHost As String, ByVal siIP As String, ByVal niPort As Integer) Handles dlgScan.SelectHost
    Try
      Dim CHost As TrackingHost
      CHost = Me.tPuConfiguracio.Hosts.GetHost(siHost, niPort, 0, eTrackingType.UDP_ORAD)
      If CHost Is Nothing Then
        CHost = New TrackingHost()
        CHost.Host = siHost
        CHost.IP = siIP
        CHost.SourcePort = niPort
        CHost.TargetPort = 0
        CHost.TrackingType = eTrackingType.UDP_ORAD
        Me.tPuConfiguracio.Hosts.LlistaHosts.Add(CHost)
        Me.MostrarOpcions()
      End If
    Catch ex As Exception

    End Try
  End Sub
#End Region

End Class
