Imports System.Collections.Generic
Imports System.Text

Class CameraMayaExporter
  Implements IExporter


  Private _oFileWriter As FileExporter
  Private timeArray As List(Of Integer)
  Private positionArrayX As List(Of Single)
  Private positionArrayY As List(Of Single)
  Private positionArrayZ As List(Of Single)
  Private orientationArrayX As List(Of Single)
  Private orientationArrayY As List(Of Single)
  Private orientationArrayZ As List(Of Single)

  Public Sub New(ByVal sFilePath As String)
    _oFileWriter = New FileExporter(sFilePath, ".ma")
    timeArray = New List(Of Integer)()
    positionArrayX = New List(Of Single)()
    positionArrayY = New List(Of Single)()
    positionArrayZ = New List(Of Single)()
    orientationArrayX = New List(Of Single)()
    orientationArrayY = New List(Of Single)()
    orientationArrayZ = New List(Of Single)()
  End Sub

  Public Sub export()
    Dim elementCount As Integer = timeArray.Count - 1

    _oFileWriter.write("//Maya ASCII 2011 scene" & System.Environment.NewLine & "//Name: camara_maya.ma" & System.Environment.NewLine & "//Last modified: Thu, Oct 07, 2010 11:33:28 AM" & System.Environment.NewLine & "//Codeset: 1252" & System.Environment.NewLine & "requires maya ""2011"";" & System.Environment.NewLine & "currentUnit -l centimeter -a degree -t pal;" & System.Environment.NewLine & "fileInfo ""application"" ""maya"";" & System.Environment.NewLine & "fileInfo ""product"" ""Maya 2011"";" & System.Environment.NewLine & "fileInfo ""version"" ""2011 x64"";" & System.Environment.NewLine & "fileInfo ""cutIdentifier"" ""201003190311-771506"";" & System.Environment.NewLine & "fileInfo ""osv"" ""Microsoft Windows 7 Business Edition, 64-bit Windows 7  (Build 7600)\n"";" & System.Environment.NewLine & "createNode transform -n ""camera1"";" & System.Environment.NewLine & "createNode camera -n ""cameraShape1"" -p ""camera1"";" & System.Environment.NewLine & "    setAttr -k off "".v"";" & System.Environment.NewLine & "    setAttr "".rnd"" no;" & System.Environment.NewLine & "    setAttr "".cap"" -type ""double2"" 1.41732 0.94488 ;" & System.Environment.NewLine & "    setAttr "".ff"" 0;" & System.Environment.NewLine & "    setAttr "".coi"" 12.999096315447833;" & System.Environment.NewLine & "    setAttr "".ow"" 30;" & System.Environment.NewLine & "    setAttr "".imn"" -type ""string"" ""camera1"";" & System.Environment.NewLine & "    setAttr "".den"" -type ""string"" ""camera1_depth"";" & System.Environment.NewLine & "    setAttr "".man"" -type ""string"" ""camera1_mask"";" & System.Environment.NewLine & "createNode animCurveTL -n ""camera1_translateX"";" & System.Environment.NewLine & "    setAttr "".tan"" 9;" & System.Environment.NewLine & "    setAttr "".wgt"" no;" & System.Environment.NewLine & "    setAttr -s 4 "".ktv[0:" & elementCount & "]""  ")

    '"1 -0.94085873150770738 100 14.385823912484462 " + System.Environment.NewLine +
    '"        170 4.9892209912564764 248 3.6212668969187698;" + System.Environment.NewLine +

    writeArray(timeArray, positionArrayX)

    _oFileWriter.write("    setAttr -s 4 "".kit[0:3]""  3 9 9 3;" & System.Environment.NewLine & "    setAttr -s 4 "".kot[0:3]""  3 9 9 3;" & System.Environment.NewLine & "createNode animCurveTL -n ""camera1_translateY"";" & System.Environment.NewLine & "    setAttr "".tan"" 9;" & System.Environment.NewLine & "    setAttr "".wgt"" no;" & System.Environment.NewLine & "    setAttr -s 4 "".ktv[0:" & elementCount & "]""  ")

    '"1 12.206555691921414 100 7.2607210841498135 " + System.Environment.NewLine +
    '"        170 5.4955284757443827 248 1.6622723377468684;" + System.Environment.NewLine;

    writeArray(timeArray, positionArrayY)

    _oFileWriter.write("    setAttr -s 4 "".kit[0:3]""  3 9 9 3;" & System.Environment.NewLine & "    setAttr -s 4 "".kot[0:3]""  3 9 9 3;" & System.Environment.NewLine & "createNode animCurveTL -n ""camera1_translateZ"";" & System.Environment.NewLine & "    setAttr "".tan"" 9;" & System.Environment.NewLine & "    setAttr "".wgt"" no;" & System.Environment.NewLine & "    setAttr -s 4 "".ktv[0:" & elementCount & "]""  ")


    '"1 27.917265058324524 100 7.7253024453226979 " + System.Environment.NewLine +
    '"        170 -20.288706331342091 248 -21.574130225972603;" + System.Environment.NewLine;

    writeArray(timeArray, positionArrayZ)

    _oFileWriter.write("    setAttr -s 4 "".kit[0:3]""  3 9 9 3;" & System.Environment.NewLine & "    setAttr -s 4 "".kot[0:3]""  3 9 9 3;" & System.Environment.NewLine & "createNode animCurveTA -n ""camera1_rotateX"";" & System.Environment.NewLine & "    setAttr "".tan"" 9;" & System.Environment.NewLine & "    setAttr "".wgt"" no;" & System.Environment.NewLine & "    setAttr -s 4 "".ktv[0:" & elementCount & "]""  ")


    '"1 -24.599999999999987 100 -20.999999999999996 " + System.Environment.NewLine +
    '"        170 -21.59999999999998 248 -4.1999999999996369;" + System.Environment.NewLine;

    writeArray(timeArray, orientationArrayX)

    _oFileWriter.write("    setAttr -s 4 "".kit[0:3]""  3 9 9 3;" & System.Environment.NewLine & "    setAttr -s 4 "".kot[0:3]""  3 9 9 3;" & System.Environment.NewLine & "createNode animCurveTA -n ""camera1_rotateY"";" & System.Environment.NewLine & "    setAttr "".tan"" 9;" & System.Environment.NewLine & "    setAttr "".wgt"" no;" & System.Environment.NewLine & "    setAttr -s 4 "".ktv[0:" & elementCount & "]""  ")


    '"1 -1.5999999999999999 100 48.000000000000036 " + System.Environment.NewLine +
    '"        170 160.79999999999913 248 168.39999999999878;" + System.Environment.NewLine;

    writeArray(timeArray, orientationArrayY)

    _oFileWriter.write("    setAttr -s 4 "".kit[0:3]""  3 9 9 3;" & System.Environment.NewLine & "    setAttr -s 4 "".kot[0:3]""  3 9 9 3;" & System.Environment.NewLine & "createNode animCurveTA -n ""camera1_rotateZ"";" & System.Environment.NewLine & "    setAttr "".tan"" 9;" & System.Environment.NewLine & "    setAttr "".wgt"" no;" & System.Environment.NewLine & "    setAttr -s 4 "".ktv[0:" & elementCount & "]""  ")


    '"1 0 100 2.376632193504665e-015 170 0 248 " + System.Environment.NewLine +
    '"        0;" + System.Environment.NewLine;

    writeArray(timeArray, orientationArrayZ)

    _oFileWriter.write("    setAttr -s 4 "".kit[0:3]""  3 9 9 3;" & System.Environment.NewLine & "    setAttr -s 4 "".kot[0:3]""  3 9 9 3;" & System.Environment.NewLine & "createNode animCurveTU -n ""cameraShape1_focalLength"";" & System.Environment.NewLine & "    setAttr "".tan"" 9;" & System.Environment.NewLine & "    setAttr "".wgt"" no;" & System.Environment.NewLine & "    setAttr -s 3 "".ktv[0:2]""  ")


    _oFileWriter.write("100 35 170 35 248 97.800000000000011;" & System.Environment.NewLine)

    'writeArray(timeArray, positionArrayZ);


    _oFileWriter.write("    setAttr -s 3 "".kit[2]""  3;" & System.Environment.NewLine & "    setAttr -s 3 "".kot[2]""  3;" & System.Environment.NewLine & "select -ne :time1;" & System.Environment.NewLine & "    setAttr "".o"" 38;" & System.Environment.NewLine & "    setAttr "".unw"" 38;" & System.Environment.NewLine & "select -ne :renderPartition;" & System.Environment.NewLine & "    setAttr -s 2 "".st"";" & System.Environment.NewLine & "select -ne :initialShadingGroup;" & System.Environment.NewLine & "    setAttr -s 3 "".dsm"";" & System.Environment.NewLine & "    setAttr "".ro"" yes;" & System.Environment.NewLine & "select -ne :initialParticleSE;" & System.Environment.NewLine & "    setAttr "".ro"" yes;" & System.Environment.NewLine & "select -ne :defaultShaderList1;" & System.Environment.NewLine & "    setAttr -s 2 "".s"";" & System.Environment.NewLine & "select -ne :postProcessList1;" & System.Environment.NewLine & "    setAttr -s 2 "".p"";" & System.Environment.NewLine & "select -ne :renderGlobalsList1;" & System.Environment.NewLine & "select -ne :defaultRenderGlobals;" & System.Environment.NewLine & "    setAttr "".mcfr"" 25;" & System.Environment.NewLine & "select -ne :hardwareRenderGlobals;" & System.Environment.NewLine & "    setAttr "".ctrs"" 256;" & System.Environment.NewLine & "    setAttr "".btrs"" 512;" & System.Environment.NewLine & "    setAttr "".hwfr"" 25;" & System.Environment.NewLine & "select -ne :defaultHardwareRenderGlobals;" & System.Environment.NewLine & "    setAttr "".fn"" -type ""string"" ""im"";" & System.Environment.NewLine & "    setAttr "".res"" -type ""string"" ""ntsc_4d 646 485 1.333"";" & System.Environment.NewLine & "connectAttr ""camera1_translateX.o"" ""camera1.tx"";" & System.Environment.NewLine & "connectAttr ""camera1_translateY.o"" ""camera1.ty"";" & System.Environment.NewLine & "connectAttr ""camera1_translateZ.o"" ""camera1.tz"";" & System.Environment.NewLine & "connectAttr ""camera1_rotateX.o"" ""camera1.rx"";" & System.Environment.NewLine & "connectAttr ""camera1_rotateY.o"" ""camera1.ry"";" & System.Environment.NewLine & "connectAttr ""camera1_rotateZ.o"" ""camera1.rz"";" & System.Environment.NewLine & "connectAttr ""cameraShape1_focalLength.o"" ""cameraShape1.fl"";" & System.Environment.NewLine & "// End of camara_maya.ma" & System.Environment.NewLine)

    _oFileWriter.Dispose()


  End Sub

  Private Sub writeArray(ByVal keyArray As List(Of Integer), ByVal valueArray As List(Of Single))
    Dim count As Integer = 0
    While count < keyArray.Count
      _oFileWriter.write(" " & keyArray(count).ToString().Replace(",", ".") & " " & valueArray(count).ToString().Replace(",", "."))
      count += 1
    End While
    _oFileWriter.writeLine(";")
  End Sub

  Public Sub addKeyframe(ByVal position As Single(), ByVal orientation As Single(), ByVal time As Integer)
    timeArray.Add(time)
    positionArrayX.Add(position(0))
    positionArrayY.Add(position(1))
    positionArrayZ.Add(position(2))
    orientationArrayX.Add(orientation(0))
    orientationArrayY.Add(orientation(1))
    orientationArrayZ.Add(orientation(2))
  End Sub


  Public Sub Export(ByVal CiTrackingFile As TrackingFile, Optional ByVal niMaxValues As Integer = -1)

  End Sub

  Public Sub Export1(ByVal CiTrackingFile As TrackingFile, ByVal siFile As String, Optional ByVal niMaxValues As Integer = -1) Implements IExporter.Export

  End Sub

  Public Event UpdateProgress(ByVal diProgress1 As Double, ByVal diProgress2 As Double, ByVal siText As String) Implements IExporter.UpdateProgress
End Class
