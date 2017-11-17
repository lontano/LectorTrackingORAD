Public Class TrackingSourceManager
  Public TrackingSources As New List(Of TrackingSource)

  Private bPiTrackingEnabled As Boolean = False

  Public Event NewTrackingValue(ByVal CiValue As TrackingValue)

  Public Sub New()

  End Sub

  Public Property Enabled() As Boolean
    Get
      Return Me.bPiTrackingEnabled
    End Get
    Set(ByVal value As Boolean)
      If value <> Me.bPiTrackingEnabled Then
        Me.bPiTrackingEnabled = value
        If value = True Then
          For Each CTrackingSource As TrackingSource In Me.TrackingSources
            'Només TCPs
            ' If CTrackingSource.TrackingHost.TrackingType = eTrackingType.TCP_Client Then
            CTrackingSource.InitTrackingListener()
            ' End If
          Next
        Else
          For Each CTrackingSource As TrackingSource In Me.TrackingSources
            ' If CTrackingSource.TrackingHost.TrackingType = eTrackingType.TCP_Client Then
            CTrackingSource.StopTrackingListener()
            ' End If
          Next
        End If
      End If

    End Set
  End Property

#Region "Add/remove hosts"
  Public Function AddHost(ByVal CiHost As TrackingHost) As TrackingSource
    Dim CRes As TrackingSource = Nothing
    Try
      For Each CSource As TrackingSource In Me.TrackingSources
        If CSource.TrackingHost.Host = CiHost.Host And CSource.TrackingHost.SourcePort = CiHost.SourcePort Then
          CRes = CSource
          Exit For
        End If
      Next
      If CRes Is Nothing Then
        CRes = New TrackingSource
        CRes.TrackingHost = CiHost
        Me.TrackingSources.Add(CRes)
        AddHandler CRes.ListenerStarted, AddressOf Me.TrackingSource_ListenerStarted
        AddHandler CRes.ListenerStopped, AddressOf Me.TrackingSource_ListenerStopped
        AddHandler CRes.NewTrackingValue, AddressOf Me.TrackingSource_NewTrackingValue
        AddHandler CRes.TrackignValuesCleared, AddressOf Me.TrackingSource_TrackignValuesCleared
      End If
    Catch ex As Exception

    End Try
    Return CRes
  End Function

  Public Sub RemoveHost(ByVal CiHost As TrackingHost)
    Try
      For Each CSource As TrackingSource In Me.TrackingSources
        If CSource.TrackingHost.Host = CiHost.Host And CSource.TrackingHost.SourcePort = CiHost.SourcePort Then
          Me.TrackingSources.Remove(CSource)
          RemoveHandler CSource.ListenerStarted, AddressOf Me.TrackingSource_ListenerStarted
          RemoveHandler CSource.ListenerStopped, AddressOf Me.TrackingSource_ListenerStopped
          RemoveHandler CSource.NewTrackingValue, AddressOf Me.TrackingSource_NewTrackingValue
          RemoveHandler CSource.TrackignValuesCleared, AddressOf Me.TrackingSource_TrackignValuesCleared
          Exit For
        End If
      Next
    Catch ex As Exception

    End Try
  End Sub

  Public Sub ClearHosts()
    Try
      For nIndex As Integer = 0 To Me.TrackingSources.Count - 1
        With Me.TrackingSources(0)
          RemoveHandler .ListenerStarted, AddressOf Me.TrackingSource_ListenerStarted
          RemoveHandler .ListenerStopped, AddressOf Me.TrackingSource_ListenerStopped
          RemoveHandler .NewTrackingValue, AddressOf Me.TrackingSource_NewTrackingValue
          RemoveHandler .TrackignValuesCleared, AddressOf Me.TrackingSource_TrackignValuesCleared
        End With
        Me.TrackingSources.RemoveAt(0)

      Next
    Catch ex As Exception

    End Try
  End Sub
#End Region

#Region "Tracking source events"
  Private Sub TrackingSource_ListenerStarted()

  End Sub

  Private Sub TrackingSource_ListenerStopped()

  End Sub

  Private Sub TrackingSource_NewTrackingValue(ByVal CiTrackingValue As TrackingValue)
    RaiseEvent NewTrackingValue(CiTrackingValue)
  End Sub

  Private Sub TrackingSource_TrackignValuesCleared()

  End Sub
#End Region
End Class
