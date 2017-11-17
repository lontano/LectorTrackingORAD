Imports System.ComponentModel

Namespace TrackingListener
  <Serializable()> Public MustInherit Class ATrackingListener
    Private WithEvents CPiTrackingSource As TrackingSource
    Public TrackingListenerHost As TrackingHost

    Public Channel As Integer = -1

    Public WithEvents BackgroundListener As BackgroundWorker

#Region "Events"
    Public Event ValueReceived(ByVal CiValue As TrackingValue)
    Public Event DataReceived(ByVal buffer() As Byte, ByVal text As String)
    Public Event Started()
    Public Event Stopped()

    Protected Sub Raise_ValueReceived(ByVal CiValue As TrackingValue)
      RaiseEvent ValueReceived(CiValue)
    End Sub

    Protected Sub Raise_DataReceived(ByVal buffer() As Byte, ByVal text As String)
      RaiseEvent DataReceived(buffer, text)
    End Sub

    Protected Sub Raise_Started()
      RaiseEvent Started()
    End Sub

    Protected Sub Raise_Stopped()
      RaiseEvent Stopped()
    End Sub
#End Region

#Region "Class functions"
    Public Sub New()
      'UDPSender.Connect("192.168.146.255", 8747)
      'Me.InitComm()
    End Sub

    Public Sub New(ByVal niPort As Integer)
      Me.receivePort = niPort
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
    Public closing As Boolean = False                           'Used to close clients if form is closing

    Public MustOverride Sub InitComm()

    Public MustOverride Sub CloseComm()

    '''''''''''''''''''''Setup receiving client'''''''''''''
    Public MustOverride Sub InitializeReceiver()

    '''''''''''''''''''''Setup receiving thread'''''''''''''
    Public Sub InitializeReceiverThread()
      Me.BackgroundListener = New BackgroundWorker
      Me.BackgroundListener.WorkerReportsProgress = True
      Me.BackgroundListener.WorkerSupportsCancellation = True
      Me.BackgroundListener.RunWorkerAsync()
    End Sub

    '''''''''''''''''''''receiving loop''''''''''''''''''''''' 
    Public MustOverride Sub Receiver()
#End Region

#Region "Background worker"
    Private Sub BackgroundListener_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles BackgroundListener.Disposed

    End Sub

    Private Sub BackgroundListener_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundListener.DoWork
      Receiver()
    End Sub

    Private Sub BackgroundListener_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundListener.ProgressChanged
      Try
        Dim CValue As TrackingValue = CType(e.UserState, TrackingValue)
        If Not CValue Is Nothing Then
          RaiseEvent ValueReceived(CValue)
        End If
      Catch ex As Exception

      End Try
    End Sub

    Private Sub BackgroundListener_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundListener.RunWorkerCompleted

    End Sub
#End Region
  End Class
End Namespace