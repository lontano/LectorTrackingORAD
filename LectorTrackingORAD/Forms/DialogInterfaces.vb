Imports System.Windows.Forms

Public Class DialogInterfaces
  Public LlistaInterfaces As List(Of String)
  Public SelectedInterface As String

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    With Me.ListBoxInterface
      If .SelectedItem Is Nothing Then
        MsgBox("Heu de seleccionar un interface")
      Else
        Me.SelectedInterface = CStr(.SelectedItem)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
      End If
    End With
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub DialogInterfaces_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      If Not Me.LlistaInterfaces Is Nothing Then
        With Me.ListBoxInterface
          .Items.Clear()
          .AllowDrop = False
          .MultiColumn = False
          .SelectionMode = SelectionMode.One

          For Each sInterface As String In Me.LlistaInterfaces
            .Items.Add(sInterface)
          Next
        End With
      End If
    Catch ex As Exception

    End Try
  End Sub

End Class
