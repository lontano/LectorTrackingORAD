Imports System.IO
Imports System.xml.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Module MSerialize

  Public Function SerializeObjectToFile(ByVal siFile As String, ByRef CiObject As Object, ByVal biCompress As Boolean) As Boolean
    Dim myFileStream As FileStream = File.Create(siFile)
    Dim bRes As Boolean = False
    Dim bytes() As Byte
    Try
      Dim sRes As String = SerializeObjectToString(CiObject, biCompress)
      bytes = System.Text.Encoding.UTF8.GetBytes(sRes)
      myFileStream.Write(bytes, 0, bytes.Length)
      myFileStream.Flush()
      myFileStream.Close()
    Catch ex As Exception
      Debug.Print(ex.ToString)
    End Try
    Return bRes
  End Function

  Public Function SerializeObjectToString(ByRef CiObject As Object, ByVal biCompress As Boolean) As String
    Dim sRes As String = ""
    Try
      Dim sr As New IO.StringWriter()
      Dim serializer As New XmlSerializer(CiObject.GetType)
      serializer.Serialize(sr, CiObject)
      sRes = sr.ToString
      'If biCompress And False Then
      '  Dim CAux As New CompressedString(System.Text.Encoding.UTF8)
      '  CAux.UnCompressed = sRes
      '  sRes = CAux.Compressed
      'End If
    Catch ex As Exception
      Debug.Print(ex.ToString)
    End Try
    Return sRes
  End Function

  Public Function DesserializeObjectFromFile(ByVal siFile As String, ByRef CoObject As Object, ByVal biUnCompress As Boolean) As Boolean
    Dim bRes As Boolean = False
    Try
      If File.Exists(siFile) Then
        Dim myFileStream As FileStream = File.OpenRead(siFile)
        Dim bytes(CInt(myFileStream.Length - 1)) As Byte
        myFileStream.Read(bytes, 0, CInt(myFileStream.Length))
        Dim sRes As String
        sRes = System.Text.Encoding.UTF8.GetString(bytes)
        DesserializeObjectFromString(sRes, CoObject, biUnCompress)
        myFileStream.Close()
        bRes = True
      End If
    Catch ex As Exception
    End Try
    Return bRes
  End Function

  Public Function DesserializeObjectFromString(ByVal siString As String, ByRef CoObject As Object, ByVal biUnCompress As Boolean) As Boolean
    Dim bRes As Boolean = False
    Dim sRead As String = siString
    Try
      If sRead.Contains(vbNullChar) Then
        sRead = siString.Replace(vbNullChar, "")
      Else
        sRead = siString
      End If

      'Dim myFileStream As FileStream = File.Create("C:\tomaya.txt")

      'Dim bytes() As Byte
      'Try
      '  bytes = System.Text.Encoding.UTF8.GetBytes(sRead)
      '  myFileStream.Write(bytes, 0, bytes.Length)
      '  myFileStream.Flush()
      '  myFileStream.Close()
      'Catch ex As Exception
      'End Try

      'If biUnCompress And False Then
      '  Dim CAux As New CompressedString(System.Text.Encoding.UTF8)
      '  CAux.Compressed = siString
      '  sRead = CAux.UnCompressed
      'End If
      If sRead <> "" Then
        Dim sr As New IO.StringReader(sRead)
        Dim deserializer As New XmlSerializer(CoObject.GetType)
        CoObject = deserializer.Deserialize(sr)
        bRes = True
      End If
    Catch ex As Exception
    End Try
    Return bRes
  End Function
End Module
