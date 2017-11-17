Public Enum eTrackingType
  UDP_ORAD
  UDP
  TCP_Server
  TCP_Client
End Enum

<Serializable()> Public Class TrackingHost
  Private sPiHost As String
  Private sPiIP As String
  Private nPiSourcePort As Integer
  Private nPiTargetPort As Integer
  Private sPiStudio As String
  Private nPiCamNumber As Integer
  Private ePiTrackignType As eTrackingType = eTrackingType.UDP_ORAD

#Region "Properties"
  Public Property Host() As String
    Get
      Return Me.sPiHost
    End Get
    Set(ByVal value As String)
      Me.sPiHost = value
    End Set
  End Property

  Public Property IP() As String
    Get
      Return Me.sPiIP
    End Get
    Set(ByVal value As String)
      Me.sPiIP = value
    End Set
  End Property

  Public Property Studio() As String
    Get
      Return Me.sPiStudio
    End Get
    Set(ByVal value As String)
      Me.sPiStudio = value
    End Set
  End Property

  Public Property SourcePort() As Integer
    Get
      Return Me.nPiSourcePort
    End Get
    Set(ByVal value As Integer)
      Me.nPiSourcePort = value
    End Set
  End Property

  Public Property TargetPort() As Integer
    Get
      Return Me.nPiTargetPort
    End Get
    Set(ByVal value As Integer)
      Me.nPiTargetPort = value
    End Set
  End Property

  Public Property CamNumber() As Integer
    Get
      Return Me.nPiCamNumber
    End Get
    Set(ByVal value As Integer)
      Me.nPiCamNumber = value
    End Set
  End Property

  Public Property TrackingType() As eTrackingType
    Get
      Return Me.ePiTrackignType
    End Get
    Set(ByVal value As eTrackingType)
      Me.ePiTrackignType = value
    End Set
  End Property

#End Region

  Public Shared Operator =(ByVal v1 As TrackingHost, ByVal v2 As TrackingHost) As Boolean
    Dim bRes As Boolean = True
    Try
      If v1 Is Nothing And v2 Is Nothing Then
        bRes = True
      ElseIf v1 Is Nothing Or v2 Is Nothing Then
        bRes = False
      Else
        If v1.TrackingType <> v2.TrackingType Then
          bRes = False
        Else
          Select Case v1.TrackingType
            Case eTrackingType.TCP_Server
              bRes = bRes And (v1.TargetPort = v2.TargetPort)
            Case eTrackingType.TCP_Client
              bRes = bRes And (v1.Host = v2.Host)
              bRes = bRes And (v1.IP = v2.IP)
              bRes = bRes And (v1.TargetPort = v2.TargetPort)
            Case eTrackingType.UDP
              bRes = bRes And (v1.Host = v2.Host)
              bRes = bRes And (v1.IP = v2.IP)
              bRes = bRes And (v1.TargetPort = v2.TargetPort)
            Case eTrackingType.UDP_ORAD
              bRes = bRes And (v1.Host = v2.Host)
              bRes = bRes And (v1.IP = v2.IP)
              bRes = bRes And (v1.SourcePort = v2.SourcePort)
          End Select
          'bRes = bRes And (v1.Studio = v2.Studio)
          'bRes = bRes And (v1.CamNumber = v2.CamNumber)
        End If
      End If
    Catch ex As Exception

    End Try
    Return bRes
  End Operator


  Public Shared Operator <>(ByVal v1 As TrackingHost, ByVal v2 As TrackingHost) As Boolean
    Return Not (v1 = v2)
  End Operator
End Class
