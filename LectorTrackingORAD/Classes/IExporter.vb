Public Interface IExporter
  Event UpdateProgress(ByVal diProgress1 As Double, ByVal diProgress2 As Double, ByVal siText As String)

  Sub Export(ByVal CiTrackingFile As TrackingFile, ByVal siFile As String, Optional ByVal niMaxValues As Integer = -1)
End Interface
