Public Class frmPackets
  Private CPiTrackingFile As TrackingFile = Nothing
  Public CPuTrackingPlayer As ATrackingPlayer = Nothing

  Public _updateRequested As Boolean = False

  Public Event SelectionChanged()

  Public Property CPuTrackingFile() As TrackingFile
    Get
      Return Me.CPiTrackingFile
    End Get
    Set(ByVal value As TrackingFile)
      Me.CPiTrackingFile = value
      Me.MostrarPackets()
    End Set
  End Property


  Public Sub MostrarPackets()
    _updateRequested = True
  End Sub

  Public Sub UpdatePackets()
    Dim nRows As Integer = 0
    Dim CAux As TrackingSource

    Try
      If Me.CPuTrackingFile Is Nothing Then Exit Sub

      With Me.C1FlexGridPorts
        nRows = .Rows.Count
        .Rows.Count = Me.CPuTrackingFile.TrackingSources.Count + 1
        .Cols.Count = 4
        .Rows.Fixed = 1
        .Cols.Fixed = 0
        .Cols(0).DataType = GetType(Boolean)

        Dim nRow As Integer = .Rows.Fixed
        For Each CAux In Me.CPuTrackingFile.TrackingSources
          .SetData(nRow, 1, CAux.Port)
          .SetData(nRow, 2, CAux.Host)

          .SetData(nRow, 0, CAux.Selected)
          .SetData(nRow, 3, CAux.TrackingValues.Count)
          nRow += 1
        Next
        If nRows <> .Rows.Count Then
          .ExtendLastCol = True
          .AutoSizeCols(0, .Cols.Count - 1, 5)
        End If
      End With

      With Me.C1FlexGridPackets
        .Rows.Fixed = 1
        .Cols.Count = 3
        .Cols.Fixed = 1
        .Rows.Count = .Rows.Fixed + Me.CPuTrackingFile.SelectedValues.Count
        For nValor As Integer = 0 To Me.CPuTrackingFile.SelectedValues.Count - 1
          .SetData(nValor + .Rows.Fixed, 0, nValor)
          '
          .SetData(nValor + .Rows.Fixed, 1, Me.CPuTrackingFile.SelectedValues(nValor).MessageBytesString)
        Next
        .AutoSizeCols(0, .Cols.Count - 1, 5)
        .ExtendLastCol = True
      End With


    Catch ex As Exception

    End Try
  End Sub

  Private Sub C1FlexGridPorts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1FlexGridPorts.Click
    Try
      With Me.C1FlexGridPorts
        Dim CAux As TrackingSource
        If .Row >= .Rows.Fixed And .Col = 0 Then
          CAux = Me.CPuTrackingFile.TrackingSourceByPort(CInt(.GetData(.Row, 1)))
          If Not CAux Is Nothing Then
            CAux.Selected = Not CAux.Selected
            Me.CPuTrackingFile.ToggleSelection(CAux.Port, CAux.Selected)

            MostrarPackets()
            RaiseEvent SelectionChanged()
          End If
        End If
      End With
    Catch ex As Exception

    End Try
  End Sub

  Private Sub frmPackets_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

  End Sub

  Private Sub EnviarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnviarToolStripMenuItem.Click
    Try
      Dim CValor As TrackingValue
      With Me.C1FlexGridPackets
        CValor = Me.CPuTrackingFile.SelectedValues(.Row - .Rows.Fixed)
      End With
      If Not CValor Is Nothing Then
        Me.CPuTrackingPlayer.BroadcastValue(CValor)
      End If

    Catch ex As Exception

    End Try
  End Sub

  Private _busy As Boolean = False
  Private Sub TimerUpdate_Tick(sender As Object, e As EventArgs) Handles TimerUpdate.Tick
    If _busy Then Exit Sub
    _busy = True
    Try
      If _updateRequested Then
        UpdatePackets()
      End If
    Catch ex As Exception
    End Try
    _busy = False
  End Sub
End Class