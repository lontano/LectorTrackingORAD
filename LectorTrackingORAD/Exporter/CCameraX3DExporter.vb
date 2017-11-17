Imports System.Collections.Generic
Imports System.Text

Class CameraX3DExporter
  Implements IExporter


  Public Sub Export(ByVal CiTrackingFile As TrackingFile, ByVal siFile As String, Optional ByVal niMaxValues As Integer = -1) Implements IExporter.Export
    Dim sLine As String
    Dim nCount As Integer
    Dim nIndex As Integer
    Dim CTrackingValues As List(Of TrackingValue)
    Dim CTrackingSource As TrackingSource
    Dim sCameraID As String
    Dim FileWriter As FileExporter
    Dim nSteps As Integer = 10
    Dim nStep As Integer = 0
    Try
      FileWriter = New FileExporter(siFile, ".wrl")
      With FileWriter
        .writeLine("<?xml version=""1.0"" encoding=""UTF-8""?>")
        .writeLine("<!DOCTYPE X3D PUBLIC ""ISO//Web3D//DTD X3D 3.0//EN"" ""http://www.web3d.org/specifications/x3d-3.0.dtd"">")
        .writeLine("<X3D version=""3.0"" profile=""Immersive"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema-instance"" xsd:noNamespaceSchemaLocation=""http://www.web3d.org/specifications/x3d-3.0.xsd"">")
        .writeLine("<head>")
        .writeLine("<meta name=""filename"" content=" & siFile & " />")
        .writeLine("<meta name=""generator"" content=" & Application.ProductName & " " & Application.ProductVersion & " />")
        .writeLine("</head>")
        .writeLine("<Scene>")
        .writeLine("		<NavigationInfo headlight=""false""")
        .writeLine("visibilityLimit = ""0.0""")
        .writeLine("            type='""EXAMINE"", ""ANY""'")
        .writeLine("  avatarSize = ""0.25, 1.75, 0.75""")
        .writeLine("            />")
        .writeLine("		<Background DEF=""WO_World""")
        .writeLine("groundColor = ""0.051 0.051 0.051""")
        .writeLine("skyColor = ""0.051 0.051 0.051""")
        .writeLine("        />")

        RaiseEvent UpdateProgress(0, 0, "Iniciant")
        nSteps = 6 * CiTrackingFile.SelectedSources.Count

        For Each CTrackingSource In CiTrackingFile.TrackingSources
          'For nSource As Integer = 0 To CiTrackingFile.TrackingSources.Count - 1
          If CTrackingSource.Selected Then
            sCameraID = "CameraORADPort" & CTrackingSource.Port
            CTrackingValues = CTrackingSource.TrackingValues
            If niMaxValues = -1 Then
              nCount = CTrackingValues.Count
            Else
              nCount = Math.Min(niMaxValues, CTrackingValues.Count)
            End If

            .writeLine("DEF " & sCameraID & " Viewpoint {")
            .writeLine("  position " & CTrackingValues(0).POS_X.ToString.Replace(",", ".") & " " & CTrackingValues(0).POS_Y.ToString().Replace(",", ".") & " " & CTrackingValues(0).POS_Z.ToString.Replace(",", "."))
            .writeLine("  orientation " & CTrackingValues(0).ROT_X.ToString.Replace(",", ".") & " " & CTrackingValues(0).ROT_Y.ToString().Replace(",", ".") & " " & CTrackingValues(0).ROT_Z.ToString().Replace(",", ".") & " " & CTrackingValues(0).ROLL.ToString().Replace(",", "."))
            .writeLine("  fieldOfView " & (Math.PI * CTrackingValues(0).FOV / 180).ToString().Replace(",", "."))
            .writeLine("  description """ & sCameraID & """")
            .writeLine("}")
            .writeLine("DEF " & sCameraID & "-TIMER TimeSensor { loop TRUE cycleInterval 4 },")
            'Valor de 4 para PAL. Si fuera NTFS sería 3.333

            'Position
            .writeLine("DEF " & sCameraID & "-POS-INTERP PositionInterpolator { ")
            sLine = "  key ["
            nStep += 1
            For nIndex = 0 To nCount - 1
              RaiseEvent UpdateProgress(nStep / nSteps, nIndex / nCount, " create position interpolator")
              If CTrackingValues(nIndex).COUNTER = 0 Then
                sLine = sLine & (nIndex / 50).ToString().Replace(",", ".") & "," & " "
              Else
                sLine = sLine & (CTrackingValues(nIndex).COUNTER / 50).ToString().Replace(",", ".") & "," & " "
              End If

            Next

            sLine = sLine & "]"
            .writeLine(sLine)
            sLine = "  keyValue ["
            nStep += 1
            For nIndex = 0 To nCount - 1
              RaiseEvent UpdateProgress(nStep / nSteps, nIndex / nCount, " create position values")
              sLine = sLine & (CTrackingValues(nIndex).POS_X.ToString().Replace(",", ".") & " " & CTrackingValues(nIndex).POS_Y.ToString().Replace(",", ".") & " " & CTrackingValues(nIndex).POS_Z.ToString().Replace(",", ".") & ",")
            Next
            sLine = sLine & (" ] },")
            .writeLine(sLine)

            'Orientation
            .writeLine("DEF " & sCameraID & "-ROT-INTERP OrientationInterpolator { ")
            sLine = "  key ["
            nStep += 1
            For nIndex = 0 To nCount - 1
              If CTrackingValues(nIndex).COUNTER = 0 Then
                sLine = sLine & (nIndex / 50).ToString().Replace(",", ".") & "," & " "
              Else
                sLine = sLine & (CTrackingValues(nIndex).COUNTER / 50).ToString().Replace(",", ".") & "," & " "
              End If
              RaiseEvent UpdateProgress(nStep / nSteps, nIndex / nCount, " create orientation interpolator")
            Next
            sLine = sLine & ("]")
            .writeLine(sLine)

            sLine = ("  keyValue [")
            nStep += 1
            For nIndex = 0 To nCount - 1
              sLine = sLine & (CTrackingValues(nIndex).ROT_X.ToString().Replace(",", ".") & " " & CTrackingValues(nIndex).ROT_Y.ToString().Replace(",", ".") & " " & CTrackingValues(nIndex).ROT_Z.ToString().Replace(",", ".") & " " & CTrackingValues(nIndex).ROLL.ToString().Replace(",", ".") & ",")
              RaiseEvent UpdateProgress(nStep / nSteps, nIndex / nCount, " create orientation values")
              'sLine = sLine & (CTrackingValues(nIndex).ROT_X.ToString().Replace(",", ".") & " " & CTrackingValues(nIndex).ROT_Y.ToString().Replace(",", ".") & " " & CTrackingValues(nIndex).ROT_Z.ToString().Replace(",", ".") & " 0,")
            Next
            sLine = sLine & (" ] },")
            .writeLine(sLine)

            ''Field of view
            '.writeLine("DEF " & sCameraID & "-FOV-INTERP ScalarInterpolator { ")
            'sLine = "  key ["
            'For nIndex = 0 To nCount - 1
            '  sLine = sLine & ((CTrackingValues(nIndex).COUNTER / 50).ToString().Replace(",", ".") & " ")
            'Next
            'sLine = sLine & ("]")
            '.writeLine(sLine)

            'sLine = ("  keyValue [")
            'For nIndex = 0 To nCount - 1
            '  sLine = sLine & (Math.PI * CTrackingValues(nIndex).FOV / 180).ToString().Replace(",", ".") & " "
            'Next
            'sLine = sLine & (" ] },")
            '.writeLine(sLine)

            .writeLine("ROUTE " & sCameraID & "-TIMER.fraction_changed TO " & sCameraID & "-POS-INTERP.set_fraction")
            .writeLine("ROUTE " & sCameraID & "-POS-INTERP.value_changed TO " & sCameraID & ".set_position")
            .writeLine("ROUTE " & sCameraID & "-TIMER.fraction_changed TO " & sCameraID & "-ROT-INTERP.set_fraction")
            .writeLine("ROUTE " & sCameraID & "-ROT-INTERP.value_changed TO " & sCameraID & ".set_orientation")


            'crear objecte dummy pel FOV
            .writeLine("DEF " & sCameraID & "Dummy Transform {")
            .writeLine("translation 31.44 0 -34.23")
            .writeLine("children [")
            .writeLine("    Transform {")
            .writeLine("      translation 0 3.803 0")
            .writeLine("        children [")
            .writeLine("          Shape {")
            .writeLine("            appearance Appearance {")
            .writeLine("              material Material {")
            .writeLine("                diffuseColor 0.8824 0.3451 0.7804")
            .writeLine("              }")
            .writeLine("            }")
            .writeLine("            geometry Box { size 1.0 1.0 1.0 }")
            .writeLine("          }")
            .writeLine("    ] }")
            .writeLine("  ]")
            .writeLine("}")

            'Position
            .writeLine("DEF " & sCameraID & "Dummy-POS-INTERP PositionInterpolator { ")
            sLine = "  key ["
            nStep += 1
            For nIndex = 0 To nCount - 1
              If CTrackingValues(nIndex).COUNTER = 0 Then
                sLine = sLine & (nIndex / 50).ToString().Replace(",", ".") & "," & " "
              Else
                sLine = sLine & (CTrackingValues(nIndex).COUNTER / 50).ToString().Replace(",", ".") & "," & " "
              End If
              RaiseEvent UpdateProgress(nStep / nSteps, nIndex / nCount, " create FOV interpolator")
            Next
            sLine = sLine & "]"
            .writeLine(sLine)
            sLine = "  keyValue ["
            nStep += 1
            For nIndex = 0 To nCount - 1
              sLine = sLine & (Math.PI * CTrackingValues(nIndex).FOV / 180).ToString().Replace(",", ".") & " 0 0,"
              RaiseEvent UpdateProgress(nStep / nSteps, nIndex / nCount, " create FOV values")
            Next
            sLine = sLine & (" ] },")
            .writeLine(sLine)

            .writeLine("ROUTE " & sCameraID & "-TIMER.fraction_changed TO " & sCameraID & "Dummy-POS-INTERP.set_fraction")
            .writeLine("ROUTE " & sCameraID & "Dummy-POS-INTERP.value_changed TO " & sCameraID & "Dummy.set_translation")

          End If
        Next
        .Dispose()
      End With

      FileWriter = Nothing

    Catch ex As Exception

    End Try
  End Sub

  Public Event UpdateProgress(ByVal diProgress1 As Double, ByVal diProgress2 As Double, ByVal siText As String) Implements IExporter.UpdateProgress
End Class
