<Serializable()> Public Class TrackingHosts
  Public LlistaHosts As New List(Of TrackingHost)

  Public Function GetHost(ByVal siSourceHost As String, ByVal niSourcePort As Integer, ByVal niTargetPort As Integer, ByVal eiTrackingType As eTrackingType) As TrackingHost
    Dim CRes As TrackingHost = Nothing
    Try
      For Each CHost As TrackingHost In Me.LlistaHosts
        If CHost.TrackingType = eiTrackingType Then
          Select Case eiTrackingType
            Case eTrackingType.UDP_ORAD
              If CHost.Host = siSourceHost And CHost.SourcePort = niSourcePort Then
                CRes = CHost
                Exit For
              End If
            Case Else
              If CHost.TargetPort = niTargetPort Then
                CRes = CHost
                Exit For
              End If
          End Select
        End If

      Next
    Catch ex As Exception
    End Try
    Return CRes
  End Function

  Public Function GetHost(ByVal CiHost As TrackingHost) As TrackingHost
    Dim CRes As TrackingHost = Nothing
    Try
      For Each CHost As TrackingHost In Me.LlistaHosts
        If CHost = CiHost Then
          CRes = CHost
          Exit For
        End If
      Next
    Catch ex As Exception
    End Try
    Return CRes
  End Function

  Public Function AddHost(ByVal siHost As String, ByVal niPort As Integer) As TrackingHost
    Return Me.AddHost(siHost, niPort, "", 0)
  End Function

  Public Function AddHost(ByVal siHost As String, ByVal niPort As Integer, ByVal siStudio As String, ByVal niCamNumber As Integer) As TrackingHost
    Dim CRes As TrackingHost = New TrackingHost()
    CRes.Host = siHost
    CRes.SourcePort = niPort
    CRes.Studio = siStudio
    CRes.CamNumber = niCamNumber
    Return CRes
  End Function
End Class
