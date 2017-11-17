Namespace TrackingRouter
  Public Class TrackingRouterManager
    Public WithEvents CPuTrackingFile As TrackingFile
    Public LlistaRouters As New List(Of TrackingRouter)
    Private bPiEnabled As Boolean = False

#Region "add/remove routers"
    Public Function CreateRouter(ByVal CiHost As TrackingHost) As TrackingRouter
      Dim CRes As TrackingRouter = Nothing
      Try
        For Each CAux As TrackingRouter In Me.LlistaRouters
          If CAux.TrackingHost.Host = CiHost.Host And CAux.TrackingHost.SourcePort = CiHost.SourcePort Then
            CRes = CAux
            Exit For
          End If
        Next
        If CRes Is Nothing Then
          CRes = New TrackingRouter(CiHost)
          Me.LlistaRouters.Add(CRes)
        End If
      Catch ex As Exception

      End Try
      Return (CRes)
    End Function
#End Region

#Region "Status functions"
    Public Property Enabled() As Boolean
      Get
        Return Me.bPiEnabled
      End Get
      Set(ByVal value As Boolean)
        Me.bPiEnabled = value
        If value Then
          Me.TurnOn()
        Else
          Me.TurnOff()
        End If
      End Set
    End Property

    Public Sub TurnOn()
      For Each CRouter As TrackingRouter In Me.LlistaRouters
        CRouter.TrackingSource = gTrackingFile.GetTrackingSource(CRouter.TrackingHost)

        CRouter.TrackingPlayers.Clear()

        For Each CTrackingPlayerHost As TrackingPlayerHost In CRouter.TrackingPlayerHosts
          CRouter.TrackingPlayers.Add(gTrackingPlayerFactory.GetTrackingPlayer(CTrackingPlayerHost, True))
        Next
      Next
    End Sub

    Public Sub TurnOff()
      For Each CRouter As TrackingRouter In Me.LlistaRouters
        CRouter.TrackingSource = Nothing
        CRouter.TrackingPlayers.Clear()
      Next
      gTrackingPlayerFactory.ClearPlayers()
    End Sub
#End Region

#Region "Send functions"
    Public Sub SendLastValue()
      Try
        For Each CRouter As TrackingRouter In Me.LlistaRouters
          CRouter.SendLastValue()
        Next
      Catch ex As Exception
      End Try
    End Sub
#End Region

  End Class
End Namespace
