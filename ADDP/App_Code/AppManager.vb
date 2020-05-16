Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Configuration

Public Class AppManager

    Public Shared Sub ADDPDental(ByVal PDFType As String, ByVal OutputFile As String, ByVal dtPat As DataTable, ByVal dtDen As DataTable, ByVal dtTooth As DataTable)
        'Dim oldFile As String = "D:\NewScape\ADDPClaimForm.pdf"
        Dim oldFile As String = System.Configuration.ConfigurationSettings.AppSettings("ADDPTemplatePdfPath") & "\" &
                                System.Configuration.ConfigurationSettings.AppSettings("ADDPTemplatePdf")
        Dim document As Document
        Dim reader As PdfReader
        Dim newfile As String
        Dim InterCount As Integer
        Dim dtTooth10 As New DataTable()

        If PDFType = "P" Then
            'newfile = "D:\NewScape\ADDPDentalClaimFormPP.pdf"
            newfile = System.Configuration.ConfigurationSettings.AppSettings("ADDPTemplatePdfPath") & "\" & OutputFile
            reader = New PdfReader(oldFile)
            Dim size As Rectangle = reader.GetPageSizeWithRotation(1)
            document = New Document(size)
        Else
            newfile = System.Configuration.ConfigurationSettings.AppSettings("ADDPTemplatePdfPath") & "\" & OutputFile
            document = New Document(PageSize.LETTER, 0, 0, 0, 0)
        End If

        Dim fs As FileStream = New FileStream(newfile, FileMode.Create, FileAccess.Write)
        Dim writer As PdfWriter = PdfWriter.GetInstance(document, fs)

        document.Open()
        Dim cb As PdfContentByte = writer.DirectContent
        Dim canvas As PdfContentByte = writer.DirectContent
        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, False)

        Dim fc6 As New iTextSharp.text.Font(bf, 6, iTextSharp.text.Font.NORMAL)
        Dim fc8 As New iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL)

        cb.SetColorFill(BaseColor.DARK_GRAY)
        cb.SetFontAndSize(bf, 8)

        Dim dTotalFees As Decimal
        Dim hdrStartLeft, hdrStartTop As Integer
        hdrStartLeft = 0
        hdrStartTop = 696

        InterCount = 0
        'This denotes no of loops required to print interventions
        InterCount = CInt(Math.Ceiling(dtTooth.Rows.Count / 10))

        For d = 1 To InterCount
            ' new page when to print on new page where no of lines more than 10
            If (d > 1) Then
                document.NewPage()
            End If

            dtTooth10 = dtTooth.Clone()
            dtTooth10.Clear()
            dTotalFees = 0

            For DosCnt = (d * 10) - 10 To ((d - 1) * 10) + 9
                If (dtTooth.Rows.Count > DosCnt) Then
                    dtTooth10.ImportRow(dtTooth.Rows(DosCnt))
                Else
                    Exit For
                End If
            Next

            'Field 1
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("PatGenderM").ToString, fc6), hdrStartLeft + 54, hdrStartTop, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("PatGenderF").ToString, fc6), hdrStartLeft + 85, hdrStartTop, 0)

            'Field 2
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("PatBirthM").ToString, fc8), hdrStartLeft + 205, hdrStartTop, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("PatBirthD").ToString, fc8), hdrStartLeft + 231, hdrStartTop, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("PatBirthY").ToString, fc8), hdrStartLeft + 257, hdrStartTop, 0)

            'Field 3
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("FirstName").ToString & " " & dtPat.Rows(0).Item("MiddleName").ToString & " " & dtPat.Rows(0).Item("LastName").ToString, fc8), hdrStartLeft + 40, hdrStartTop - 26, 0)

            'Field 4
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("SSN").ToString, fc8), hdrStartLeft + 40, hdrStartTop - 46, 0)

            'Field 9
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("CtlNum").ToString, fc8), hdrStartLeft + 327, hdrStartTop - 46, 0)

            'Field 5
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("StAddr").ToString, fc8), hdrStartLeft + 40, hdrStartTop - 70, 0)

            'Field 9A
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("AuthNum").ToString, fc8), hdrStartLeft + 327, hdrStartTop - 70, 0)

            'Field 5A
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("City").ToString & " " & dtPat.Rows(0).Item("State").ToString & " " & dtPat.Rows(0).Item("Zip").ToString, fc8), hdrStartLeft + 40, hdrStartTop - 95, 0)

            'Field 10
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("EmailAddr").ToString, fc8), hdrStartLeft + 327, hdrStartTop - 95, 0)

            'Field 6
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("Telephone").ToString, fc8), hdrStartLeft + 40, hdrStartTop - 118, 0)

            'Field 11
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase("SIGNATURE ON FILE", fc8), hdrStartLeft + 327, hdrStartTop - 133, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("dtTreat").ToString, fc8), hdrStartLeft + 530, hdrStartTop - 133, 0)

            'Field 7
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPat.Rows(0).Item("Rank").ToString, fc8), hdrStartLeft + 40, hdrStartTop - 143, 0)

            'Field 12
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("Name").ToString, fc8), hdrStartLeft + 44, hdrStartTop - 167, 0)
            'Field 12A
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("ProvNo").ToString, fc8), hdrStartLeft + 190, hdrStartTop - 167, 0)
            'Field 12B
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("ProvNPI").ToString, fc8), hdrStartLeft + 263, hdrStartTop - 167, 0)
            'Field 16
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("StAddr").ToString, fc8), hdrStartLeft + 331, hdrStartTop - 167, 0)

            'Field 13
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("SSNTIN").ToString, fc8), hdrStartLeft + 44, hdrStartTop - 191, 0)
            'Field 14
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("License").ToString, fc8), hdrStartLeft + 147, hdrStartTop - 191, 0)
            'Field 15
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("Telephone").ToString, fc8), hdrStartLeft + 234, hdrStartTop - 191, 0)
            'Field 16A
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("City").ToString & " " & dtDen.Rows(0).Item("State").ToString & " " & dtDen.Rows(0).Item("Zip").ToString, fc8), hdrStartLeft + 331, hdrStartTop - 191, 0)

            'Field Dental Readiness
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("Class").ToString, fc8), hdrStartLeft + 110, hdrStartTop - 208, 0)
            'Field 1
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("ADSM1").ToString, fc6), hdrStartLeft + 32, hdrStartTop - 220, 0)
            'Field 2
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("ADSM2").ToString, fc6), hdrStartLeft + 32, hdrStartTop - 230, 0)
            'Field 3
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("ADSM3").ToString, fc6), hdrStartLeft + 32, hdrStartTop - 247, 0)
            'Field a
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("ADSM3a").ToString, fc6), hdrStartLeft + 44, hdrStartTop - 257, 0)
            'Field b
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("ADSM3b").ToString, fc6), hdrStartLeft + 44, hdrStartTop - 267, 0)
            'Field c
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("ADSM3c").ToString, fc6), hdrStartLeft + 44, hdrStartTop - 284, 0)
            'Field d
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("ADSM3d").ToString, fc6), hdrStartLeft + 44, hdrStartTop - 294, 0)
            'Field e
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("ADSM3e").ToString, fc6), hdrStartLeft + 44, hdrStartTop - 311, 0)
            'Field f
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("ADSM3f").ToString, fc6), hdrStartLeft + 44, hdrStartTop - 321, 0)

            'Field 17
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("ADSMDet").ToString, fc8), hdrStartLeft + 43, hdrStartTop - 340, 0)

            dTotalFees = 0
            'Field 18a
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(0).Item("Tooth").ToString, fc8), hdrStartLeft + 38, hdrStartTop - 408, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(0).Item("Surface").ToString, fc8), hdrStartLeft + 64, hdrStartTop - 408, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(0).Item("Description").ToString, fc8), hdrStartLeft + 110, hdrStartTop - 408, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(0).Item("dtDosMM").ToString, fc8), hdrStartLeft + 385, hdrStartTop - 408, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(0).Item("dtDosDD").ToString, fc8), hdrStartLeft + 406, hdrStartTop - 408, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(0).Item("dtDosYY").ToString, fc8), hdrStartLeft + 423, hdrStartTop - 408, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(0).Item("dtCPTCode").ToString, fc8), hdrStartLeft + 452, hdrStartTop - 408, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(0).Item("dtCharge").ToString, fc8), hdrStartLeft + 576, hdrStartTop - 408, 0)
            dTotalFees = CDec(dtTooth10.Rows(0).Item("dtCharge").ToString)

            'Field 18b
            If dtTooth10.Rows.Count > 1 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(1).Item("Tooth").ToString, fc8), hdrStartLeft + 38, hdrStartTop - 426, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(1).Item("Surface").ToString, fc8), hdrStartLeft + 64, hdrStartTop - 426, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(1).Item("Description").ToString, fc8), hdrStartLeft + 110, hdrStartTop - 426, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(1).Item("dtDosMM").ToString, fc8), hdrStartLeft + 385, hdrStartTop - 426, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(1).Item("dtDosDD").ToString, fc8), hdrStartLeft + 406, hdrStartTop - 426, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(1).Item("dtDosYY").ToString, fc8), hdrStartLeft + 423, hdrStartTop - 426, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(1).Item("dtCPTCode").ToString, fc8), hdrStartLeft + 452, hdrStartTop - 426, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(1).Item("dtCharge").ToString, fc8), hdrStartLeft + 576, hdrStartTop - 426, 0)
                dTotalFees = dTotalFees + CDec(dtTooth10.Rows(0).Item("dtCharge").ToString)
            End If

            'Field 18c
            If dtTooth10.Rows.Count > 2 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(2).Item("Tooth").ToString, fc8), hdrStartLeft + 38, hdrStartTop - 445, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(2).Item("Surface").ToString, fc8), hdrStartLeft + 64, hdrStartTop - 445, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(2).Item("Description").ToString, fc8), hdrStartLeft + 110, hdrStartTop - 445, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(2).Item("dtDosMM").ToString, fc8), hdrStartLeft + 385, hdrStartTop - 445, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(2).Item("dtDosDD").ToString, fc8), hdrStartLeft + 406, hdrStartTop - 445, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(2).Item("dtDosYY").ToString, fc8), hdrStartLeft + 423, hdrStartTop - 445, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(2).Item("dtCPTCode").ToString, fc8), hdrStartLeft + 452, hdrStartTop - 445, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(2).Item("dtCharge").ToString, fc8), hdrStartLeft + 576, hdrStartTop - 445, 0)
                dTotalFees = dTotalFees + CDec(dtTooth10.Rows(0).Item("dtCharge").ToString)
            End If

            'Field 18d
            If dtTooth10.Rows.Count > 3 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(3).Item("Tooth").ToString, fc8), hdrStartLeft + 38, hdrStartTop - 465, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(3).Item("Surface").ToString, fc8), hdrStartLeft + 64, hdrStartTop - 465, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(3).Item("Description").ToString, fc8), hdrStartLeft + 110, hdrStartTop - 465, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(3).Item("dtDosMM").ToString, fc8), hdrStartLeft + 385, hdrStartTop - 465, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(3).Item("dtDosDD").ToString, fc8), hdrStartLeft + 406, hdrStartTop - 465, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(3).Item("dtDosYY").ToString, fc8), hdrStartLeft + 423, hdrStartTop - 465, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(3).Item("dtCPTCode").ToString, fc8), hdrStartLeft + 452, hdrStartTop - 465, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(3).Item("dtCharge").ToString, fc8), hdrStartLeft + 576, hdrStartTop - 465, 0)
                dTotalFees = dTotalFees + CDec(dtTooth10.Rows(0).Item("dtCharge").ToString)
            End If

            'Field 18e
            If dtTooth10.Rows.Count > 4 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(4).Item("Tooth").ToString, fc8), hdrStartLeft + 38, hdrStartTop - 483, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(4).Item("Surface").ToString, fc8), hdrStartLeft + 64, hdrStartTop - 483, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(4).Item("Description").ToString, fc8), hdrStartLeft + 110, hdrStartTop - 483, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(4).Item("dtDosMM").ToString, fc8), hdrStartLeft + 385, hdrStartTop - 483, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(4).Item("dtDosDD").ToString, fc8), hdrStartLeft + 406, hdrStartTop - 483, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(4).Item("dtDosYY").ToString, fc8), hdrStartLeft + 423, hdrStartTop - 483, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(4).Item("dtCPTCode").ToString, fc8), hdrStartLeft + 452, hdrStartTop - 483, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(4).Item("dtCharge").ToString, fc8), hdrStartLeft + 576, hdrStartTop - 483, 0)
                dTotalFees = dTotalFees + CDec(dtTooth10.Rows(0).Item("dtCharge").ToString)
            End If

            'Field 18f
            If dtTooth10.Rows.Count > 5 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(5).Item("Tooth").ToString, fc8), hdrStartLeft + 38, hdrStartTop - 499, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(5).Item("Surface").ToString, fc8), hdrStartLeft + 64, hdrStartTop - 499, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(5).Item("Description").ToString, fc8), hdrStartLeft + 110, hdrStartTop - 499, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(5).Item("dtDosMM").ToString, fc8), hdrStartLeft + 385, hdrStartTop - 499, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(5).Item("dtDosDD").ToString, fc8), hdrStartLeft + 406, hdrStartTop - 499, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(5).Item("dtDosYY").ToString, fc8), hdrStartLeft + 423, hdrStartTop - 499, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(5).Item("dtCPTCode").ToString, fc8), hdrStartLeft + 452, hdrStartTop - 499, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(5).Item("dtCharge").ToString, fc8), hdrStartLeft + 576, hdrStartTop - 499, 0)
                dTotalFees = dTotalFees + CDec(dtTooth10.Rows(0).Item("dtCharge").ToString)
            End If

            'Field 18g
            If dtTooth10.Rows.Count > 6 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(6).Item("Tooth").ToString, fc8), hdrStartLeft + 38, hdrStartTop - 518, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(6).Item("Surface").ToString, fc8), hdrStartLeft + 64, hdrStartTop - 518, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(6).Item("Description").ToString, fc8), hdrStartLeft + 110, hdrStartTop - 518, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(6).Item("dtDosMM").ToString, fc8), hdrStartLeft + 385, hdrStartTop - 518, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(6).Item("dtDosDD").ToString, fc8), hdrStartLeft + 406, hdrStartTop - 518, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(6).Item("dtDosYY").ToString, fc8), hdrStartLeft + 423, hdrStartTop - 518, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(6).Item("dtCPTCode").ToString, fc8), hdrStartLeft + 452, hdrStartTop - 518, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(6).Item("dtCharge").ToString, fc8), hdrStartLeft + 576, hdrStartTop - 518, 0)
                dTotalFees = dTotalFees + CDec(dtTooth10.Rows(0).Item("dtCharge").ToString)
            End If

            'Field 18h
            If dtTooth10.Rows.Count > 7 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(7).Item("Tooth").ToString, fc8), hdrStartLeft + 38, hdrStartTop - 534, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(7).Item("Surface").ToString, fc8), hdrStartLeft + 64, hdrStartTop - 534, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(7).Item("Description").ToString, fc8), hdrStartLeft + 110, hdrStartTop - 534, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(7).Item("dtDosMM").ToString, fc8), hdrStartLeft + 385, hdrStartTop - 534, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(7).Item("dtDosDD").ToString, fc8), hdrStartLeft + 406, hdrStartTop - 534, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(7).Item("dtDosYY").ToString, fc8), hdrStartLeft + 423, hdrStartTop - 534, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(7).Item("dtCPTCode").ToString, fc8), hdrStartLeft + 452, hdrStartTop - 534, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(7).Item("dtCharge").ToString, fc8), hdrStartLeft + 576, hdrStartTop - 534, 0)
                dTotalFees = dTotalFees + CDec(dtTooth10.Rows(0).Item("dtCharge").ToString)
            End If

            'Field 18i
            If dtTooth10.Rows.Count > 8 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(8).Item("Tooth").ToString, fc8), hdrStartLeft + 38, hdrStartTop - 554, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(8).Item("Surface").ToString, fc8), hdrStartLeft + 64, hdrStartTop - 554, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(8).Item("Description").ToString, fc8), hdrStartLeft + 110, hdrStartTop - 554, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(8).Item("dtDosMM").ToString, fc8), hdrStartLeft + 385, hdrStartTop - 554, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(8).Item("dtDosDD").ToString, fc8), hdrStartLeft + 406, hdrStartTop - 554, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(8).Item("dtDosYY").ToString, fc8), hdrStartLeft + 423, hdrStartTop - 554, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(8).Item("dtCPTCode").ToString, fc8), hdrStartLeft + 452, hdrStartTop - 554, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(8).Item("dtCharge").ToString, fc8), hdrStartLeft + 576, hdrStartTop - 554, 0)
                dTotalFees = dTotalFees + CDec(dtTooth10.Rows(0).Item("dtCharge").ToString)
            End If

            'Field 18j
            If dtTooth10.Rows.Count > 9 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(9).Item("Tooth").ToString, fc8), hdrStartLeft + 38, hdrStartTop - 570, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(9).Item("Surface").ToString, fc8), hdrStartLeft + 64, hdrStartTop - 570, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(9).Item("Description").ToString, fc8), hdrStartLeft + 110, hdrStartTop - 570, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(9).Item("dtDosMM").ToString, fc8), hdrStartLeft + 385, hdrStartTop - 570, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(9).Item("dtDosDD").ToString, fc8), hdrStartLeft + 406, hdrStartTop - 570, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(9).Item("dtDosYY").ToString, fc8), hdrStartLeft + 423, hdrStartTop - 570, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtTooth10.Rows(9).Item("dtCPTCode").ToString, fc8), hdrStartLeft + 452, hdrStartTop - 570, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtTooth10.Rows(9).Item("dtCharge").ToString, fc8), hdrStartLeft + 576, hdrStartTop - 570, 0)
                dTotalFees = dTotalFees + CDec(dtTooth10.Rows(0).Item("dtCharge").ToString)
            End If

            'Field 19
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dTotalFees.ToString, fc8), hdrStartLeft + 576, hdrStartTop - 600, 0)
            'Field 20a
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase("SIGNATURE ON FILE", fc8), hdrStartLeft + 110, hdrStartTop - 640, 0)
            'Field 20b
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("dtSign").ToString, fc8), hdrStartLeft + 408, hdrStartTop - 640, 0)

            If PDFType = "P" Then
                Dim page As PdfImportedPage = writer.GetImportedPage(reader, 1)
                cb.AddTemplate(page, 0, 0)
            End If
        Next


        document.Close()
        fs.Close()
        writer.Close()

        If PDFType = "P" Then
            reader.Close()
        End If

    End Sub

    Public Shared Sub ADADental(ByVal PDFType As String, ByVal OutputFile As String, ByVal dtHdr As DataTable, ByVal dtBenPlanInfo As DataTable, ByVal dtOthCvg As DataTable,
                                 ByVal dtPolHldInfo As DataTable, ByVal dtPatInfo As DataTable, ByVal dtRecSvcs As DataTable, ByVal dtFee As DataTable,
                                 ByVal dtMissTInfo As DataTable, ByVal dtDiag As DataTable, ByVal dtClm As DataTable,
                                 ByVal dtDen As DataTable)
        'Dim oldFile As String = "D:\NewScape\ADA 2019 ClaimForm v2.pdf"
        Dim oldFile As String = System.Configuration.ConfigurationSettings.AppSettings("ADATemplatePdfPath") & "\" &
                                System.Configuration.ConfigurationSettings.AppSettings("ADATemplatePdf")
        Dim document As Document
        Dim reader As PdfReader
        Dim newfile As String
        Dim InterCount As Integer
        Dim dtRecSvcs10 As New DataTable()

        If PDFType = "P" Then
            'newfile = "D:\NewScape\ADADentalClaimFormPP.pdf"
            newfile = System.Configuration.ConfigurationSettings.AppSettings("ADATemplatePdfPath") & "\" & OutputFile
            reader = New PdfReader(oldFile)
            Dim size As Rectangle = reader.GetPageSizeWithRotation(1)
            document = New Document(size)
        Else
            'newfile = "D:\NewScape\ADADentalClaimFormB.pdf"
            newfile = System.Configuration.ConfigurationSettings.AppSettings("ADATemplatePdfPath") & "\" & OutputFile
            document = New Document(PageSize.LETTER, 0, 0, 0, 0)
        End If

        Dim fs As FileStream = New FileStream(newfile, FileMode.Create, FileAccess.Write)
        Dim writer As PdfWriter = PdfWriter.GetInstance(document, fs)

        document.Open()
        Dim cb As PdfContentByte = writer.DirectContent
        Dim canvas As PdfContentByte = writer.DirectContent
        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, False)

        Dim fc6 As New iTextSharp.text.Font(bf, 6, iTextSharp.text.Font.NORMAL)
        Dim fc8 As New iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL)
        Dim fc10 As New iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL)

        cb.SetColorFill(BaseColor.DARK_GRAY)
        cb.SetFontAndSize(bf, 8)
        Dim dTotalFees As Decimal
        Dim hdrStartLeft, hdrStartTop As Integer

        hdrStartLeft = 0
        hdrStartTop = 735

        InterCount = 0
        'This denotes no of loops required to print interventions
        InterCount = CInt(Math.Ceiling(dtRecSvcs.Rows.Count / 10))

        For d = 1 To InterCount
            ' new page when to print on new page where no of lines more than 10
            If (d > 1) Then
                document.NewPage()
            End If

            dtRecSvcs10 = dtRecSvcs.Clone()
            dtRecSvcs10.Clear()
            dTotalFees = 0

            For DosCnt = (d * 10) - 10 To ((d - 1) * 10) + 9
                If (dtRecSvcs.Rows.Count > DosCnt) Then
                    dtRecSvcs10.ImportRow(dtRecSvcs.Rows(DosCnt))
                Else
                    Exit For
                End If
            Next


            'Field 1
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtHdr.Rows(0).Item("SAS").ToString, fc10), hdrStartLeft + 26, hdrStartTop, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtHdr.Rows(0).Item("PredPreAuth").ToString, fc10), hdrStartLeft + 142, hdrStartTop, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtHdr.Rows(0).Item("EPSDT").ToString, fc10), hdrStartLeft + 26, hdrStartTop - 13, 0)

            'Field 2
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtHdr.Rows(0).Item("PredPreAuthNo").ToString, fc10), hdrStartLeft + 26, hdrStartTop - 35, 0)

            'Field 3
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtBenPlanInfo.Rows(0).Item("Name").ToString, fc10), hdrStartLeft + 26, hdrStartTop - 72, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtBenPlanInfo.Rows(0).Item("StAddr").ToString, fc10), hdrStartLeft + 26, hdrStartTop - 87, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtBenPlanInfo.Rows(0).Item("CityStZip").ToString, fc10), hdrStartLeft + 26, hdrStartTop - 102, 0)

            'Field 4
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("DenYes").ToString, fc10), hdrStartLeft + 55, hdrStartTop - 132, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("MedYes").ToString, fc10), hdrStartLeft + 113, hdrStartTop - 132, 0)

            'Field 5
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthName").ToString, fc10), hdrStartLeft + 26, hdrStartTop - 155, 0)

            'Field 6
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthdtBirth").ToString, fc10), hdrStartLeft + 26, hdrStartTop - 178, 0)

            'Field 7
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthGenderM").ToString, fc10), hdrStartLeft + 127, hdrStartTop - 180, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthGenderF").ToString, fc10), hdrStartLeft + 141, hdrStartTop - 180, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthGenderU").ToString, fc10), hdrStartLeft + 156, hdrStartTop - 180, 0)

            'Field 8
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthPolicy").ToString, fc10), hdrStartLeft + 184, hdrStartTop - 178, 0)

            'Field 9
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthPlan").ToString, fc10), hdrStartLeft + 26, hdrStartTop - 204, 0)

            'Field 10
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthPatRelnS").ToString, fc10), hdrStartLeft + 126, hdrStartTop - 204, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthPatRelnSp").ToString, fc10), hdrStartLeft + 163, hdrStartTop - 204, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthPatRelnD").ToString, fc10), hdrStartLeft + 206, hdrStartTop - 204, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthPatRelnO").ToString, fc10), hdrStartLeft + 256, hdrStartTop - 204, 0)

            'Field 11
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthName").ToString, fc10), hdrStartLeft + 26, hdrStartTop - 225, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthAddr").ToString, fc10), hdrStartLeft + 26, hdrStartTop - 240, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtOthCvg.Rows(0).Item("OthCityStZip").ToString, fc10), hdrStartLeft + 26, hdrStartTop - 253, 0)

            'Field 12
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPolHldInfo.Rows(0).Item("PolName").ToString, fc10), hdrStartLeft + 320, hdrStartTop - 45, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPolHldInfo.Rows(0).Item("PolAddr").ToString, fc10), hdrStartLeft + 320, hdrStartTop - 60, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPolHldInfo.Rows(0).Item("PolCityStZip").ToString, fc10), hdrStartLeft + 320, hdrStartTop - 75, 0)

            'Field 13
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPolHldInfo.Rows(0).Item("PoldtBirth").ToString, fc10), hdrStartLeft + 320, hdrStartTop - 105, 0)

            'Field 14
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPolHldInfo.Rows(0).Item("PolGenderM").ToString, fc10), hdrStartLeft + 415, hdrStartTop - 108, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPolHldInfo.Rows(0).Item("PolGenderF").ToString, fc10), hdrStartLeft + 430, hdrStartTop - 108, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPolHldInfo.Rows(0).Item("PolGenderU").ToString, fc10), hdrStartLeft + 444, hdrStartTop - 108, 0)

            'Field 15
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPolHldInfo.Rows(0).Item("PolPolicyID").ToString, fc10), hdrStartLeft + 476, hdrStartTop - 108, 0)

            'Field 16
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPolHldInfo.Rows(0).Item("PolGroupNo").ToString, fc10), hdrStartLeft + 320, hdrStartTop - 130, 0)

            'Field 17
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPolHldInfo.Rows(0).Item("PolEmplName").ToString, fc10), hdrStartLeft + 417, hdrStartTop - 130, 0)

            'Field 18
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("PatRelnSelf").ToString, fc10), hdrStartLeft + 321, hdrStartTop - 168, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("PatRelnSpouse").ToString, fc10), hdrStartLeft + 358, hdrStartTop - 168, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("PatRelnChild").ToString, fc10), hdrStartLeft + 401, hdrStartTop - 168, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("PatRelnOther").ToString, fc10), hdrStartLeft + 466, hdrStartTop - 168, 0)

            'Field 19
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("PatRes").ToString, fc8), hdrStartLeft + 527, hdrStartTop - 168, 0)

            'Field 20
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("PatName").ToString, fc10), hdrStartLeft + 321, hdrStartTop - 190, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("PatAddr").ToString, fc10), hdrStartLeft + 321, hdrStartTop - 203, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("PatCityStZip").ToString, fc10), hdrStartLeft + 321, hdrStartTop - 215, 0)

            'Field 21
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("PatdtBirth").ToString, fc10), hdrStartLeft + 321, hdrStartTop - 250, 0)

            'Field 22
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("PatGenderM").ToString, fc10), hdrStartLeft + 415, hdrStartTop - 252, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("PatGenderF").ToString, fc10), hdrStartLeft + 429, hdrStartTop - 252, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("PatGenderU").ToString, fc10), hdrStartLeft + 444, hdrStartTop - 252, 0)

            'Field 23
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("PatId").ToString, fc10), hdrStartLeft + 477, hdrStartTop - 252, 0)

            dTotalFees = 0
            'Line 1 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(0).Item("dtDos").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 300, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(0).Item("Area").ToString, fc10), hdrStartLeft + 110, hdrStartTop - 300, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(0).Item("System").ToString, fc10), hdrStartLeft + 132, hdrStartTop - 300, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(0).Item("Number").ToString, fc10), hdrStartLeft + 152, hdrStartTop - 300, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(0).Item("Surface").ToString, fc10), hdrStartLeft + 242, hdrStartTop - 300, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(0).Item("ProcCode").ToString, fc10), hdrStartLeft + 280, hdrStartTop - 300, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(0).Item("DiagPointer").ToString, fc10), hdrStartLeft + 330, hdrStartTop - 300, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(0).Item("Qty").ToString, fc10), hdrStartLeft + 358, hdrStartTop - 300, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(0).Item("Description").ToString, fc10), hdrStartLeft + 388, hdrStartTop - 300, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtRecSvcs10.Rows(0).Item("Fee").ToString, fc10), hdrStartLeft + 595, hdrStartTop - 300, 0)
            dTotalFees = CDec(dtRecSvcs10.Rows(0).Item("Fee").ToString)

            'Line 2
            If dtRecSvcs10.Rows.Count > 1 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(1).Item("dtDos").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 312, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(1).Item("Area").ToString, fc10), hdrStartLeft + 110, hdrStartTop - 312, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(1).Item("System").ToString, fc10), hdrStartLeft + 132, hdrStartTop - 312, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(1).Item("Number").ToString, fc10), hdrStartLeft + 152, hdrStartTop - 312, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(1).Item("Surface").ToString, fc10), hdrStartLeft + 242, hdrStartTop - 312, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(1).Item("ProcCode").ToString, fc10), hdrStartLeft + 280, hdrStartTop - 312, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(1).Item("DiagPointer").ToString, fc10), hdrStartLeft + 330, hdrStartTop - 312, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(1).Item("Qty").ToString, fc10), hdrStartLeft + 358, hdrStartTop - 312, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(1).Item("Description").ToString, fc10), hdrStartLeft + 388, hdrStartTop - 312, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtRecSvcs10.Rows(1).Item("Fee").ToString, fc10), hdrStartLeft + 595, hdrStartTop - 312, 0)
                dTotalFees = dTotalFees + CDec(dtRecSvcs10.Rows(1).Item("Fee").ToString)
            End If

            'Line 3 
            If dtRecSvcs10.Rows.Count > 2 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(2).Item("dtDos").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 324, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(2).Item("Area").ToString, fc10), hdrStartLeft + 110, hdrStartTop - 324, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(2).Item("System").ToString, fc10), hdrStartLeft + 132, hdrStartTop - 324, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(2).Item("Number").ToString, fc10), hdrStartLeft + 152, hdrStartTop - 324, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(2).Item("Surface").ToString, fc10), hdrStartLeft + 242, hdrStartTop - 324, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(2).Item("ProcCode").ToString, fc10), hdrStartLeft + 280, hdrStartTop - 324, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(2).Item("DiagPointer").ToString, fc10), hdrStartLeft + 330, hdrStartTop - 324, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(2).Item("Qty").ToString, fc10), hdrStartLeft + 358, hdrStartTop - 324, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(2).Item("Description").ToString, fc10), hdrStartLeft + 388, hdrStartTop - 324, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtRecSvcs10.Rows(2).Item("Fee").ToString, fc10), hdrStartLeft + 595, hdrStartTop - 324, 0)

                dTotalFees = dTotalFees + CDec(dtRecSvcs10.Rows(2).Item("Fee").ToString)
            End If

            'Line 4 
            If dtRecSvcs10.Rows.Count > 3 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(3).Item("dtDos").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 336, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(3).Item("Area").ToString, fc10), hdrStartLeft + 110, hdrStartTop - 336, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(3).Item("System").ToString, fc10), hdrStartLeft + 132, hdrStartTop - 336, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(3).Item("Number").ToString, fc10), hdrStartLeft + 152, hdrStartTop - 336, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(3).Item("Surface").ToString, fc10), hdrStartLeft + 242, hdrStartTop - 336, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(3).Item("ProcCode").ToString, fc10), hdrStartLeft + 280, hdrStartTop - 336, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(3).Item("DiagPointer").ToString, fc10), hdrStartLeft + 330, hdrStartTop - 336, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(3).Item("Qty").ToString, fc10), hdrStartLeft + 358, hdrStartTop - 336, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(3).Item("Description").ToString, fc10), hdrStartLeft + 388, hdrStartTop - 336, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtRecSvcs10.Rows(3).Item("Fee").ToString, fc10), hdrStartLeft + 595, hdrStartTop - 336, 0)

                dTotalFees = dTotalFees + CDec(dtRecSvcs10.Rows(3).Item("Fee").ToString)
            End If
            'Line 5 
            If dtRecSvcs10.Rows.Count > 4 Then

                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(4).Item("dtDos").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 348, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(4).Item("Area").ToString, fc10), hdrStartLeft + 110, hdrStartTop - 348, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(4).Item("System").ToString, fc10), hdrStartLeft + 132, hdrStartTop - 348, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(4).Item("Number").ToString, fc10), hdrStartLeft + 152, hdrStartTop - 348, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(4).Item("Surface").ToString, fc10), hdrStartLeft + 242, hdrStartTop - 348, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(4).Item("ProcCode").ToString, fc10), hdrStartLeft + 280, hdrStartTop - 348, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(4).Item("DiagPointer").ToString, fc10), hdrStartLeft + 330, hdrStartTop - 348, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(4).Item("Qty").ToString, fc10), hdrStartLeft + 358, hdrStartTop - 348, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(4).Item("Description").ToString, fc10), hdrStartLeft + 388, hdrStartTop - 348, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtRecSvcs10.Rows(4).Item("Fee").ToString, fc10), hdrStartLeft + 595, hdrStartTop - 348, 0)
                dTotalFees = dTotalFees + CDec(dtRecSvcs10.Rows(4).Item("Fee").ToString)
            End If

            'Line 6 
            If dtRecSvcs10.Rows.Count > 5 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(5).Item("dtDos").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 360, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(5).Item("Area").ToString, fc10), hdrStartLeft + 110, hdrStartTop - 360, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(5).Item("System").ToString, fc10), hdrStartLeft + 132, hdrStartTop - 360, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(5).Item("Number").ToString, fc10), hdrStartLeft + 152, hdrStartTop - 360, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(5).Item("Surface").ToString, fc10), hdrStartLeft + 242, hdrStartTop - 360, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(5).Item("ProcCode").ToString, fc10), hdrStartLeft + 280, hdrStartTop - 360, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(5).Item("DiagPointer").ToString, fc10), hdrStartLeft + 330, hdrStartTop - 360, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(5).Item("Qty").ToString, fc10), hdrStartLeft + 358, hdrStartTop - 360, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(5).Item("Description").ToString, fc10), hdrStartLeft + 388, hdrStartTop - 360, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtRecSvcs10.Rows(5).Item("Fee").ToString, fc10), hdrStartLeft + 595, hdrStartTop - 360, 0)
                dTotalFees = dTotalFees + CDec(dtRecSvcs10.Rows(5).Item("Fee").ToString)
            End If

            'Line 7 
            If dtRecSvcs10.Rows.Count > 6 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(6).Item("dtDos").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 372, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(6).Item("Area").ToString, fc10), hdrStartLeft + 110, hdrStartTop - 372, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(6).Item("System").ToString, fc10), hdrStartLeft + 132, hdrStartTop - 372, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(6).Item("Number").ToString, fc10), hdrStartLeft + 152, hdrStartTop - 372, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(6).Item("Surface").ToString, fc10), hdrStartLeft + 242, hdrStartTop - 372, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(6).Item("ProcCode").ToString, fc10), hdrStartLeft + 280, hdrStartTop - 372, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(6).Item("DiagPointer").ToString, fc10), hdrStartLeft + 330, hdrStartTop - 372, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(6).Item("Qty").ToString, fc10), hdrStartLeft + 358, hdrStartTop - 372, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(6).Item("Description").ToString, fc10), hdrStartLeft + 388, hdrStartTop - 372, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtRecSvcs10.Rows(6).Item("Fee").ToString, fc10), hdrStartLeft + 595, hdrStartTop - 372, 0)
                dTotalFees = dTotalFees + CDec(dtRecSvcs10.Rows(6).Item("Fee").ToString)
            End If

            'Line 8 
            If dtRecSvcs10.Rows.Count > 7 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(7).Item("dtDos").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 384, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(7).Item("Area").ToString, fc10), hdrStartLeft + 110, hdrStartTop - 384, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(7).Item("System").ToString, fc10), hdrStartLeft + 132, hdrStartTop - 384, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(7).Item("Number").ToString, fc10), hdrStartLeft + 152, hdrStartTop - 384, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(7).Item("Surface").ToString, fc10), hdrStartLeft + 242, hdrStartTop - 384, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(7).Item("ProcCode").ToString, fc10), hdrStartLeft + 280, hdrStartTop - 384, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(7).Item("DiagPointer").ToString, fc10), hdrStartLeft + 330, hdrStartTop - 384, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(7).Item("Qty").ToString, fc10), hdrStartLeft + 358, hdrStartTop - 384, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(7).Item("Description").ToString, fc10), hdrStartLeft + 388, hdrStartTop - 384, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtRecSvcs10.Rows(7).Item("Fee").ToString, fc10), hdrStartLeft + 595, hdrStartTop - 384, 0)
                dTotalFees = dTotalFees + CDec(dtRecSvcs10.Rows(7).Item("Fee").ToString)
            End If

            'Line 9 
            If dtRecSvcs10.Rows.Count > 8 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(8).Item("dtDos").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 397, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(8).Item("Area").ToString, fc10), hdrStartLeft + 110, hdrStartTop - 397, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(8).Item("System").ToString, fc10), hdrStartLeft + 132, hdrStartTop - 397, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(8).Item("Number").ToString, fc10), hdrStartLeft + 152, hdrStartTop - 397, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(8).Item("Surface").ToString, fc10), hdrStartLeft + 242, hdrStartTop - 397, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(8).Item("ProcCode").ToString, fc10), hdrStartLeft + 280, hdrStartTop - 397, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(8).Item("DiagPointer").ToString, fc10), hdrStartLeft + 330, hdrStartTop - 397, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(8).Item("Qty").ToString, fc10), hdrStartLeft + 358, hdrStartTop - 397, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(8).Item("Description").ToString, fc10), hdrStartLeft + 388, hdrStartTop - 397, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtRecSvcs10.Rows(8).Item("Fee").ToString, fc10), hdrStartLeft + 595, hdrStartTop - 397, 0)
                dTotalFees = dTotalFees + CDec(dtRecSvcs10.Rows(8).Item("Fee").ToString)
            End If

            'Line 10 
            If dtRecSvcs10.Rows.Count > 9 Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(9).Item("DtDos").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 409, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(9).Item("Area").ToString, fc10), hdrStartLeft + 110, hdrStartTop - 409, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(9).Item("System").ToString, fc10), hdrStartLeft + 132, hdrStartTop - 409, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(9).Item("Number").ToString, fc10), hdrStartLeft + 152, hdrStartTop - 409, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(9).Item("Surface").ToString, fc10), hdrStartLeft + 242, hdrStartTop - 409, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(9).Item("ProcCode").ToString, fc10), hdrStartLeft + 280, hdrStartTop - 409, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(9).Item("DiagPointer").ToString, fc10), hdrStartLeft + 330, hdrStartTop - 409, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(9).Item("Qty").ToString, fc10), hdrStartLeft + 358, hdrStartTop - 409, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtRecSvcs10.Rows(9).Item("Description").ToString, fc10), hdrStartLeft + 388, hdrStartTop - 409, 0)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtRecSvcs10.Rows(9).Item("Fee").ToString, fc10), hdrStartLeft + 595, hdrStartTop - 409, 0)
                dTotalFees = dTotalFees + CDec(dtRecSvcs10.Rows(9).Item("Fee").ToString)
            End If

            'Line 31a 
            If (d = 1) Then
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtFee.Rows(0).Item("OthFee1").ToString, fc10), hdrStartLeft + 595, hdrStartTop - 421, 0)
                dTotalFees = dTotalFees + CDec(dtFee.Rows(0).Item("OthFee1").ToString)
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dtFee.Rows(0).Item("OthFee2").ToString, fc10), hdrStartLeft + 595, hdrStartTop - 432, 0)
                dTotalFees = dTotalFees + CDec(dtFee.Rows(0).Item("OthFee2").ToString)
            End If

            'Line 32 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_RIGHT, New Phrase(dTotalFees, fc10), hdrStartLeft + 595, hdrStartTop - 443, 0)

            'Field 33 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth1").ToString, fc10), hdrStartLeft + 27, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth2").ToString, fc10), hdrStartLeft + 40, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth3").ToString, fc10), hdrStartLeft + 56, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth4").ToString, fc10), hdrStartLeft + 70, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth5").ToString, fc10), hdrStartLeft + 84, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth6").ToString, fc10), hdrStartLeft + 98, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth7").ToString, fc10), hdrStartLeft + 113, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth8").ToString, fc10), hdrStartLeft + 127, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth9").ToString, fc10), hdrStartLeft + 140, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth10").ToString, fc10), hdrStartLeft + 156, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth11").ToString, fc10), hdrStartLeft + 170, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth12").ToString, fc10), hdrStartLeft + 184, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth13").ToString, fc10), hdrStartLeft + 198, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth14").ToString, fc10), hdrStartLeft + 213, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth15").ToString, fc10), hdrStartLeft + 227, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth16").ToString, fc10), hdrStartLeft + 240, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth17").ToString, fc10), hdrStartLeft + 27, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth18").ToString, fc10), hdrStartLeft + 40, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth19").ToString, fc10), hdrStartLeft + 56, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth20").ToString, fc10), hdrStartLeft + 70, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth21").ToString, fc10), hdrStartLeft + 84, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth22").ToString, fc10), hdrStartLeft + 98, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth23").ToString, fc10), hdrStartLeft + 113, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth24").ToString, fc10), hdrStartLeft + 127, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth25").ToString, fc10), hdrStartLeft + 156, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth26").ToString, fc10), hdrStartLeft + 170, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth27").ToString, fc10), hdrStartLeft + 184, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth28").ToString, fc10), hdrStartLeft + 198, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth29").ToString, fc10), hdrStartLeft + 213, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth30").ToString, fc10), hdrStartLeft + 227, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth31").ToString, fc10), hdrStartLeft + 240, hdrStartTop - 444, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtMissTInfo.Rows(0).Item("Tooth32").ToString, fc10), hdrStartLeft + 274, hdrStartTop - 444, 0)

            'Field 34 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDiag.Rows(0).Item("DiagLostQual").ToString, fc10), hdrStartLeft + 358, hdrStartTop - 421, 0)

            'Field 34a 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDiag.Rows(0).Item("DiagA").ToString, fc10), hdrStartLeft + 385, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDiag.Rows(0).Item("DiagB").ToString, fc10), hdrStartLeft + 450, hdrStartTop - 432, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDiag.Rows(0).Item("DiagC").ToString, fc10), hdrStartLeft + 385, hdrStartTop - 443, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDiag.Rows(0).Item("DiagD").ToString, fc10), hdrStartLeft + 450, hdrStartTop - 443, 0)

            'Field 35 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDiag.Rows(0).Item("Remarks").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 465, 0)

            'Field 36 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase("SIGNATURE ON FILE", fc10), hdrStartLeft + 29, hdrStartTop - 530, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPatInfo.Rows(0).Item("dtPatSign").ToString, fc10), hdrStartLeft + 220, hdrStartTop - 530, 0)

            'Field 37 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase("SIGNATURE ON FILE", fc10), hdrStartLeft + 29, hdrStartTop - 578, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtPolHldInfo.Rows(0).Item("dtSubsSign").ToString, fc10), hdrStartLeft + 220, hdrStartTop - 578, 0)

            'Field 38 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("Place").ToString, fc8), hdrStartLeft + 384, hdrStartTop - 492, 0)

            'Field 39 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("Encl").ToString, fc10), hdrStartLeft + 522, hdrStartTop - 504, 0)

            'Field 40 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("OrthoY").ToString, fc10), hdrStartLeft + 321, hdrStartTop - 526, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("OrthoN").ToString, fc10), hdrStartLeft + 386, hdrStartTop - 526, 0)

            'Field 41 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("dtAppl").ToString, fc10), hdrStartLeft + 486, hdrStartTop - 526, 0)

            'Field 42 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("Months").ToString, fc10), hdrStartLeft + 313, hdrStartTop - 550, 0)

            'Field 43 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("ReplProsY").ToString, fc10), hdrStartLeft + 386, hdrStartTop - 552, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("ReplProsN").ToString, fc10), hdrStartLeft + 407, hdrStartTop - 552, 0)

            'Field 44 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("dtPrior").ToString, fc10), hdrStartLeft + 486, hdrStartTop - 550, 0)

            'Field 45 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("TreatFromO").ToString, fc10), hdrStartLeft + 321, hdrStartTop - 576, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("TreatFromAA").ToString, fc10), hdrStartLeft + 428, hdrStartTop - 576, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("TreatFromOA").ToString, fc10), hdrStartLeft + 500, hdrStartTop - 576, 0)

            'Field 46 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("dtAccident").ToString, fc10), hdrStartLeft + 428, hdrStartTop - 588, 0)

            'Field 47 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtClm.Rows(0).Item("AutoSt").ToString, fc10), hdrStartLeft + 576, hdrStartTop - 588, 0)

            'Field 48 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("BOName").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 633, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("BOAddr").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 643, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("BOCityStZip").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 653, 0)

            'Field 49 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("BONPI").ToString, fc10), hdrStartLeft + 29, hdrStartTop - 693, 0)

            'Field 50 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("BOLicNum").ToString, fc10), hdrStartLeft + 123, hdrStartTop - 693, 0)

            'Field 51 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("BOSSNTIN").ToString, fc10), hdrStartLeft + 216, hdrStartTop - 693, 0)

            'Field 52 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("BOPhone").ToString, fc10), hdrStartLeft + 54, hdrStartTop - 708, 0)

            'Field 52a 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("AddlProvId").ToString, fc10), hdrStartLeft + 220, hdrStartTop - 708, 0)

            'Field 53 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase("SIGNATURE ON FILE", fc10), hdrStartLeft + 320, hdrStartTop - 637, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("DdtSign").ToString, fc10), hdrStartLeft + 515, hdrStartTop - 637, 0)

            'Field 54 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("DNPI").ToString, fc10), hdrStartLeft + 330, hdrStartTop - 659, 0)

            'Field 55 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("DLicNo").ToString, fc10), hdrStartLeft + 515, hdrStartTop - 659, 0)

            'Field 56 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("DAddr").ToString, fc10), hdrStartLeft + 312, hdrStartTop - 679, 0)
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("DCityStZip").ToString, fc10), hdrStartLeft + 312, hdrStartTop - 689, 0)

            'Field 56a 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("DPRovSpCode").ToString, fc10), hdrStartLeft + 515, hdrStartTop - 672, 0)

            'Field 57 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("DPhone").ToString, fc10), hdrStartLeft + 350, hdrStartTop - 707, 0)

            'Field 58 
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, New Phrase(dtDen.Rows(0).Item("DAddlProvId").ToString, fc10), hdrStartLeft + 515, hdrStartTop - 707, 0)

            If PDFType = "P" Then
                Dim page As PdfImportedPage = writer.GetImportedPage(reader, 1)
                cb.AddTemplate(page, 0, 0)
            End If
        Next

        document.Close()
        fs.Close()
        writer.Close()

        If PDFType = "P" Then
            reader.Close()
        End If

    End Sub

End Class
