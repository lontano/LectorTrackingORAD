Imports System.Collections.Generic
Imports System.Text

Class FileExporter
  Implements IDisposable
  Private _oTextWrite As System.IO.StreamWriter

  Public Sub New(ByVal sFilePath As String, ByVal extension As String)
    Dim sPath As String

    'Si no hemos informado directorio de exportación, lo deja donde se esté ejecutando el programa
    If Not String.IsNullOrEmpty(sFilePath) Then
      sPath = sFilePath
    Else
      sPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
    End If


    ' El nombre del archivo a exportar siempre es la fecha de hoy con extensión .log
    Dim sFile As String = sPath & extension

    ' Si el stream de escritura se había quedado abierto, lo cerramos.
    If _oTextWrite IsNot Nothing Then
      _oTextWrite.Close()
      _oTextWrite.Dispose()
    End If
    ' Abrimos un nuevo stream de escritura.
    _oTextWrite = New System.IO.StreamWriter(sFile)
  End Sub

  Public Sub New(ByVal sFilePath As String)
    Dim sPath As String

    'Si no hemos informado directorio de exportación, lo deja donde se esté ejecutando el programa
    If Not String.IsNullOrEmpty(sFilePath) Then
      sPath = sFilePath
    Else
      sPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
    End If


    ' El nombre del archivo a exportar siempre es la fecha de hoy con extensión .log
    Dim sFile As String = sPath

    ' Si el stream de escritura se había quedado abierto, lo cerramos.
    If _oTextWrite IsNot Nothing Then
      _oTextWrite.Close()
      _oTextWrite.Dispose()
    End If
    ' Abrimos un nuevo stream de escritura.
    _oTextWrite = New System.IO.StreamWriter(sFile)
  End Sub

  Public Sub write(ByVal line As String)
    _oTextWrite.Write(line)
  End Sub

  Public Sub writeLine(ByVal line As String)
    _oTextWrite.WriteLine(line)
  End Sub

#Region "Miembros de IDisposable"

  Public Sub Dispose() Implements IDisposable.Dispose
    _oTextWrite.Flush()
    _oTextWrite.Close()
    _oTextWrite.Dispose()
    _oTextWrite = Nothing
  End Sub

#End Region
End Class
