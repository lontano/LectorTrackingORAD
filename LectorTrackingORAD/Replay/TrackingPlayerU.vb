Namespace TrackingPlayer
  <Serializable()> Public Class TrackingPlayerU
    'Inherits TrackingPlayer.ATrackingPlayer

    Private WithEvents CPiTrackingSource As TrackingSource
    Private WithEvents CPiTimer As New Timer

    Private nPiCurrentPlayTime_ms As Double
    Private nPiCurrentValueIndex As Integer = 0

    Public Channel As Integer = -1



    Private ePiTrackingState As eTrackingPlayerState = eTrackingPlayerState.Idle


#Region "Events"
    Public Event CurrentPositionChanged(ByVal diPosition_ms As Double)
    Public Event Started()
    Public Event Stopped()
    Public Event Paused()
    Public Event Ended()
#End Region

#Region "Class functions"
    Public Sub New()
      CPiTimer.Enabled = False
      CPiTimer.Interval = 40
      'UDPSender.Connect("192.168.146.255", 8747)
      Me.InitializeComm()
    End Sub

    Public Sub New(ByVal siHost As String, ByVal niPort As Integer)
      CPiTimer.Enabled = False
      CPiTimer.Interval = 40
      Me.broadcastAddress = siHost
      Me.BroadcastPort = niPort
      Me.InitializeComm()
    End Sub

    Protected Overrides Sub Finalize()
      Me.CPiTimer.Enabled = False
      Me.CPiTimer = Nothing

      MyBase.Finalize()
    End Sub
#End Region

#Region "Properties"
    Public Property TrackingSource() As TrackingSource
      Get
        Return Me.CPiTrackingSource
      End Get
      Set(ByVal value As TrackingSource)
        Me.CPiTrackingSource = value
      End Set
    End Property

    Public ReadOnly Property TrackingPlayerState() As eTrackingPlayerState
      Get
        Return Me.ePiTrackingState
      End Get
    End Property
#End Region

#Region "Public Functions"
    Private Sub ChangeState(ByVal eiTrackingState As eTrackingPlayerState)
      Try
        If eiTrackingState <> Me.ePiTrackingState Then
          Me.ePiTrackingState = eiTrackingState
          Select Case eiTrackingState
            Case eTrackingPlayerState.Idle
              Me.CPiTimer.Enabled = False
              Me.nPiCurrentPlayTime_ms = 0
              Me.nPiCurrentValueIndex = 0
              RaiseEvent Stopped()
            Case eTrackingPlayerState.Ended
              Me.CPiTimer.Enabled = False
              RaiseEvent Ended()
            Case eTrackingPlayerState.Paused
              Me.CPiTimer.Enabled = False
              RaiseEvent Paused()
            Case eTrackingPlayerState.Started
              Me.CPiTimer.Enabled = True
              RaiseEvent Started()
            Case eTrackingPlayerState.Stopped
              Me.CPiTimer.Enabled = False
              Me.nPiCurrentPlayTime_ms = 0
              Me.nPiCurrentValueIndex = 0
              RaiseEvent Stopped()
          End Select
        End If
      Catch ex As Exception

      End Try
    End Sub

    Public Sub TrackingStart(Optional ByVal StartTime_ms As Long = 0)
      Try
        nPiCurrentPlayTime_ms = StartTime_ms
        Me.ChangeState(eTrackingPlayerState.Started)
      Catch ex As Exception

      End Try
    End Sub

    Public Sub TrackingStop()
      Try
        Me.ChangeState(eTrackingPlayerState.Stopped)
      Catch ex As Exception

      End Try
    End Sub

    Public Sub TrackingPause()
      Try
        Me.ChangeState(eTrackingPlayerState.Paused)
      Catch ex As Exception
      End Try
    End Sub

    Public Sub TrackingContinue()
      Try
        Me.ChangeState(eTrackingPlayerState.Started)
      Catch ex As Exception
      End Try
    End Sub


    Public Sub SendValue(ByVal niIndex As Integer)
      Try
        Dim CValue As TrackingValue = Me.CPiTrackingSource.TrackingValues(niIndex)
        Me.BroadcastValue(CValue)
      Catch ex As Exception

      End Try
    End Sub
#End Region

#Region "Player functions and events"
    Private Sub CPiTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles CPiTimer.Tick
      Try
        Dim CValue As TrackingValue = Me.CPiTrackingSource.GetValueByTime(Me.nPiCurrentPlayTime_ms, Me.nPiCurrentValueIndex)
        If CValue.Index <> Me.nPiCurrentValueIndex Or True Then
          Me.BroadcastValue(CValue)
        End If
      Catch ex As Exception

      End Try
    End Sub

    Private Sub CPiTrackingSource_NewTrackingValue(ByVal CiTrackingValue As TrackingValue) Handles CPiTrackingSource.NewTrackingValue
      Try
        If Me.ePiTrackingState = eTrackingPlayerState.Relay Then
          Me.BroadcastValue(CiTrackingValue)
        End If
      Catch ex As Exception

      End Try
    End Sub

    Private Sub CPiTrackingSource_TrackignValuesCleared() Handles CPiTrackingSource.TrackignValuesCleared
      Try
        Me.ChangeState(eTrackingPlayerState.Idle)
      Catch ex As Exception

      End Try
    End Sub
#End Region

#Region "Broadcast"
    '''''''''''''''''''''''Set up variables''''''''''''''''''''
    Private broadcastAddress As String = "255.255.255.255" 'Sends data to all LOCAL listening clients, to send data over WAN you'll need to enter a public (external) IP address of the other client
    Public BroadcastPort As Integer = 9653
    Public receivePort As Integer = 9653
    Private receivingClient As Net.Sockets.UdpClient                         'Client for handling incoming data
    Private sendingClient As Net.Sockets.UdpClient                           'Client for sending data
    Private receivingThread As Threading.Thread                            'Create a separate thread to listen for incoming data, helps to prevent the form from freezing up
    Private closing As Boolean = False                           'Used to close clients if form is closing

    Private Sub InitializeComm()
      InitializeSender()          'Initializes startup of sender client
      'InitializeReceiver()        'Starts listening for incoming data  
    End Sub

    Private Sub CloseComm()
      closing = True          'Tells receiving loop to close
      receivingClient.Close()
      sendingClient.Close()
    End Sub

    ''''''''''''''''''''Setup sender client'''''''''''''''''
    Private Sub InitializeSender()
      broadcastAddress = "192.168.146.76"
      'broadcastAddress = "172.26.110.42"
      'broadcastAddress = "127.0.0.1"
      sendingClient = New Net.Sockets.UdpClient(broadcastAddress, BroadcastPort)
      sendingClient.EnableBroadcast = False
    End Sub

    '''''''''''''''''''''Setup receiving client'''''''''''''

    Private Sub InitializeReceiver()
      receivingClient = New Net.Sockets.UdpClient(BroadcastPort)
      Dim start As Threading.ThreadStart = New Threading.ThreadStart(AddressOf Receiver)
      receivingThread = New Threading.Thread(start)
      receivingThread.IsBackground = True
      receivingThread.Start()
    End Sub

    '''''''''''''''''''''Start receiving loop''''''''''''''''''''''' 

    Private Sub Receiver()
      Dim endPoint As Net.IPEndPoint = New Net.IPEndPoint(Net.IPAddress.Any, BroadcastPort) 'Listen for incoming data from any IP address on the specified port (i personally select 9653)
      Dim message As String = ""
      While (True)                                                     'Setup an infinite loop
        Dim data() As Byte                                           'buffer for storing incoming bytes
        Try
          data = receivingClient.Receive(endPoint)                     'Receive incoming bytes 
          message = System.Text.Encoding.ASCII.GetString(data)       'Convert bytes back to string
          Dim CValue As New TrackingValue(0)
          CValue.FromBuffer(data, 24)
          Debug.Print(CValue.CapturedMS)

        Catch ex As Exception
        End Try
        If closing = True Then                                       'Exit sub if form is closing
          Exit Sub
        End If
        Debug.Print(Now.ToString & " " & message)
      End While
    End Sub

    Public Sub BroadcastValue(ByVal CiValue As TrackingValue)
      Try
        sendingClient.Send(CiValue.buffer, CiValue.buffer.Length)               'Send bytes
      Catch ex As Exception

      End Try
    End Sub
#End Region

  End Class
End Namespace
