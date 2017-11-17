Namespace TrackingListener

  <Serializable()> Public MustInherit Class ATrackingListener
    Private WithEvents CPiTrackingSource As TrackingSource
    
    Public Channel As Integer = -1


#Region "Events"
    Public Event ValueReceived(ByVal CiValue As TrackingValue)
    Public Event Started()
    Public Event Stopped()
#End Region

#Region "Class functions"
    Public Sub New()
      'UDPSender.Connect("192.168.146.255", 8747)
      Me.InitComm()
    End Sub

    Public Sub New(ByVal siHost As String, ByVal niPort As Integer)
      Me.broadcastAddress = siHost
      Me.BroadcastPort = niPort
      Me.InitComm()
    End Sub

    Protected Overrides Sub Finalize()
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
#End Region

#Region "Broadcast"
    '''''''''''''''''''''''Set up variables''''''''''''''''''''
   Public receivePort As Integer = 9653
    Private receivingThread As Threading.Thread                            'Create a separate thread to listen for incoming data, helps to prevent the form from freezing up
    Private closing As Boolean = False                           'Used to close clients if form is closing

    Public MustOverride Sub InitComm()

    Public MustOverride Sub CloseComm()

    '''''''''''''''''''''Setup receiving client'''''''''''''
    Public MustOverride Sub InitializeReceiver()

    '''''''''''''''''''''Setup receiving thread'''''''''''''
    Public Sub InitializeReceiverThread()
      Dim start As Threading.ThreadStart = New Threading.ThreadStart(AddressOf Receiver)
      receivingThread = New Threading.Thread(start)
      receivingThread.IsBackground = True
      receivingThread.Start()
    End Sub

    '''''''''''''''''''''Start receiving loop''''''''''''''''''''''' 
    Public MustOverride Sub Receiver()
#End Region

  End Class
End Namespace