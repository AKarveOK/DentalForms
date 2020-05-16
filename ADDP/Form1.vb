Imports System.Data
Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim strSQL As String
        Dim dtPat As New DataTable()
        Dim dtDen As New DataTable()
        Dim dtTooth As New DataTable()
        Dim db As New DBHelper

        strSQL = " Select 'X' as PatGenderM, '' as PatGenderF, '01' as PatBirthM, '10' as PatBirthD, '1976' as PatBirthY, " &
                " 'JOHN' as FirstName, 'ROB' as MiddleName, 'DOS' as LastName, '123-04-5698' as SSN, " &
                " '3129 RUNNING HORSE CIRCLE' as StAddr, 'CAMP HILL' as City, 'PA' as State, '17011' as Zip, " &
                " '543-908-7658' as Telephone, 'OFFICER' as Rank, '12345678' as CtlNum, '9876543' as AuthNum, " &
                " 'JOHN.DOE@MYCOMPANY.COM' as EmailAddr, '05/10/2020' as dtTreat "
        dtPat = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

        strSQL = " Select 'RICHARD LOBO' as Name, '7653566878' as ProvNo, '1234567890' as ProvNPI, '71-0415188' as SSNTIN, " &
                " '31-0345740' as License, '800-908-7865' as Telephone, '1014 Food Street' as StAddr, 'Cincinnati' as City, " &
                " 'OH' as State,  '‎45202' as Zip, 'ABC' as Class, 'X' as ADSM1, 'X' as ADSM2, 'X' as ADSM3, 'X' as ADSM3a, " &
                " 'X' as ADSM3b, 'X' as ADSM3c, 'X' as ADSM3d, 'X' as ADSM3e, 'X' as ADSM3f, 'X' as ADSMDet, '05/08/2020' as dtSign "
        dtDen = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

        strSQL = "Select '1' as Tooth, 'FRONT' as Surface, 'INITAL EVAL' as Description, '04' as dtDosMM, '15' as dtDosDD, '2020' as dtDosYY, " &
                "  '97010' as dtCPTCode, '100.50' as dtCharge " &
                "  UNION " &
                " Select '4' as Tooth, 'FRONT' as Surface, 'INITAL EVAL' as Description, '04' as dtDosMM, '15' as dtDosDD, '2020' as dtDosYY, " &
                "  '97010' as dtCPTCode, '100.50' as dtCharge " &
                "  UNION " &
                " Select '8' as Tooth, 'FRONT' as Surface, 'INITAL EVAL' as Description, '04' as dtDosMM, '15' as dtDosDD, '2020' as dtDosYY, " &
                "  '97010' as dtCPTCode, '100.50' as dtCharge " &
                "  UNION " &
                " Select '10' as Tooth, 'FRONT' as Surface, 'INITAL EVAL' as Description, '04' as dtDosMM, '15' as dtDosDD, '2020' as dtDosYY, " &
                "  '97010' as dtCPTCode, '100.50' as dtCharge " &
                "  UNION " &
                " Select '12' as Tooth, 'FRONT' as Surface, 'INITAL EVAL' as Description, '04' as dtDosMM, '15' as dtDosDD, '2020' as dtDosYY, " &
                "  '97010' as dtCPTCode, '100.50' as dtCharge " &
                "  UNION " &
                " Select '14' as Tooth, 'FRONT' as Surface, 'INITAL EVAL' as Description, '04' as dtDosMM, '15' as dtDosDD, '2020' as dtDosYY, " &
                "  '97010' as dtCPTCode, '100.50' as dtCharge " &
                "  UNION " &
                " Select '16' as Tooth, 'FRONT' as Surface, 'INITAL EVAL' as Description, '04' as dtDosMM, '15' as dtDosDD, '2020' as dtDosYY, " &
                "  '97010' as dtCPTCode, '100.50' as dtCharge " &
                "  UNION " &
                " Select '19' as Tooth, 'FRONT' as Surface, 'INITAL EVAL' as Description, '04' as dtDosMM, '15' as dtDosDD, '2020' as dtDosYY, " &
                "  '97010' as dtCPTCode, '100.50' as dtCharge " &
                "  UNION " &
                " Select '22' as Tooth, 'FRONT' as Surface, 'INITAL EVAL' as Description, '04' as dtDosMM, '15' as dtDosDD, '2020' as dtDosYY, " &
                "  '97010' as dtCPTCode, '100.50' as dtCharge " &
                "  UNION " &
                " Select '24' as Tooth, 'FRONT' as Surface, 'INITAL EVAL' as Description, '04' as dtDosMM, '15' as dtDosDD, '2020' as dtDosYY, " &
                "  '97010' as dtCPTCode, '100.50' as dtCharge " &
                "  UNION " &
                " Select '25' as Tooth, 'FRONT' as Surface, 'INITAL EVAL' as Description, '04' as dtDosMM, '15' as dtDosDD, '2020' as dtDosYY, " &
                "  '97010' as dtCPTCode, '50.50' as dtCharge " &
                "  UNION " &
                " Select '26' as Tooth, 'FRONT' as Surface, 'INITAL EVAL' as Description, '04' as dtDosMM, '15' as dtDosDD, '2020' as dtDosYY, " &
                "  '97010' as dtCPTCode, '10.50' as dtCharge "
        dtTooth = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

        AppManager.ADDPDental("P", "ADDP_C1234_A1234PP.pdf", dtPat, dtDen, dtTooth)
        AppManager.ADDPDental("B", "ADDP_C1234_A1234B.pdf", dtPat, dtDen, dtTooth)


        Dim dtHdr, dtBenPlanInfo, dtOthCvg, dtPolHldInfo, dtPatInfo, dtRecSvcs, dtFee, dtMissTInfo, dtDiag, dtClm As DataTable

        strSQL = " Select 'X' as SAS,'' as PredPreAuth,'' as EPSDT, 'A123' as PredPreAuthNo "
        dtHdr = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

        strSQL = " Select 'AK DENTAL' as Name,'119 JUNE DRIVE, SUITE #203' as StAddr,'LOUISVILLE KY 40241' as CityStZip "
        dtBenPlanInfo = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

        strSQL = " Select 'X' as DenYes,'' as MedYes,'DOE JILL A' as OthName,'01/01/2001' as OthdtBirth,'X' as OthGenderM,'' as OthGenderF,'' as OthGenderU,
                'ID123' as OthPolicy,'GRP123' as OthPlan,'' as OthPatRelnS,'X' as OthPatRelnSp,'' as OthPatRelnD,'' as OthPatRelnO,
                'Dodiedo Dental' as OthName,'3120 OCTOBER DR' as OthAddr,'LOUISVILLE KY 40241' as OthCityStZip "
        dtOthCvg = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

        strSQL = " Select 'DOE JOHN Mr' as PolName,'9 WESTON CLOSE HINCKLEY' as PolAddr,'LESTER PA 41103' as PolCityStZip,'09/09/2009' as PoldtBirth, 
                'X' as PolGenderM,'' as PolGenderF,'' as PolGenderU,'OPOL123' as PolPolicyID,'OG123' as PolGroupNo,'WALMART INC' as PolEmplName,'05/12/2020' as dtSubsSign "
        dtPolHldInfo = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

        strSQL = " Select 'X' as PatRelnSelf,'' as PatRelnSpouse,'' as PatRelnChild,'' as PatRelnOther,'' as PatRes,'DOE JOHN' as PatName,
        '9 WESTON CLOSE HINCKLEY' as PatAddr,'Pune MH 41101' as PatCityStZip,'01/01/2001' as PatdtBirth,
            'X' as PatGenderM,'' as PatGenderF,'' as PatGenderU,'P123' as PatId,'05/12/2020' as dtPatSign "
        dtPatInfo = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

        strSQL = "Select '10/10/2010' as dtDos, 'Y' as Area,'Z' as System,'3' as Number,'Flat' as Surface,'97010' as ProcCode,
                'D01' as DiagPointer,'1' as Qty,'SOME LONG DESCRIPTION' AS Description,'100.50' as Fee 
                  UNION
                Select '10/10/2010' as dtDos, 'Y' as Area,'Z' as System,'3' as Number,'Flat' as Surface,'97011' as ProcCode,
                'D01' as DiagPointer,'1' as Qty,'SOME LONG DESCRIPTION' AS Description,'100.50' as Fee 
                UNION
                Select '10/10/2010' as dtDos, 'Y' as Area,'Z' as System,'3' as Number,'Flat' as Surface,'97012' as ProcCode,
                'D01' as DiagPointer,'1' as Qty,'SOME LONG DESCRIPTION' AS Description,'100.50' as Fee 
                  UNION
                Select '10/10/2010' as dtDos, 'Y' as Area,'Z' as System,'3' as Number,'Flat' as Surface,'97013' as ProcCode,
                'D01' as DiagPointer,'1' as Qty,'SOME LONG DESCRIPTION' AS Description,'100.50' as Fee 
                  UNION
                Select '10/10/2010' as dtDos, 'Y' as Area,'Z' as System,'3' as Number,'Flat' as Surface,'97014' as ProcCode,
                'D01' as DiagPointer,'1' as Qty,'SOME LONG DESCRIPTION' AS Description,'100.50' as Fee 
                  UNION
                Select '10/10/2010' as dtDos, 'Y' as Area,'Z' as System,'3' as Number,'Flat' as Surface,'97015' as ProcCode,
                'D01' as DiagPointer,'1' as Qty,'SOME LONG DESCRIPTION' AS Description,'100.50' as Fee 
                  UNION
                Select '10/10/2010' as dtDos, 'Y' as Area,'Z' as System,'3' as Number,'Flat' as Surface,'97016' as ProcCode,
                'D01' as DiagPointer,'1' as Qty,'SOME LONG DESCRIPTION' AS Description,'100.50' as Fee 
                  UNION
                Select '10/10/2010' as dtDos, 'Y' as Area,'Z' as System,'3' as Number,'Flat' as Surface,'97017' as ProcCode,
                'D01' as DiagPointer,'1' as Qty,'SOME LONG DESCRIPTION' AS Description,'100.50' as Fee 
                  UNION
                Select '10/10/2010' as dtDos, 'Y' as Area,'Z' as System,'3' as Number,'Flat' as Surface,'97018' as ProcCode,
                'D01' as DiagPointer,'1' as Qty,'SOME LONG DESCRIPTION' AS Description,'100.50' as Fee 
                  UNION
                Select '10/10/2010' as dtDos, 'Y' as Area,'Z' as System,'3' as Number,'Flat' as Surface,'97019' as ProcCode,
                'D01' as DiagPointer,'1' as Qty,'SOME LONG DESCRIPTION' AS Description,'100.50' as Fee 
                UNION
                Select '10/10/2010' as dtDos, 'Y' as Area,'Z' as System,'3' as Number,'Flat' as Surface,'97020' as ProcCode,
                'D01' as DiagPointer,'1' as Qty,'SOME LONG DESCRIPTION' AS Description,'50.50' as Fee 
                  UNION
                Select '10/10/2010' as dtDos, 'Y' as Area,'Z' as System,'3' as Number,'Flat' as Surface,'97021' as ProcCode,
                'D01' as DiagPointer,'1' as Qty,'SOME LONG DESCRIPTION' AS Description,'20.50' as Fee "
        dtRecSvcs = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

                strSQL = " Select '110.00' as OthFee1,'0.00' as OthFee2 "
        dtFee = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

        strSQL = " Select '' as Tooth1,'X' as Tooth2,'' as Tooth3,'' as Tooth4,'' as Tooth5,'' as Tooth6,'' as Tooth7,'' as Tooth8,'' as Tooth9,'' as Tooth10,
        '' as Tooth11,'' as Tooth12,'X' as Tooth13,'' as Tooth14,'' as Tooth15,'' as Tooth16,'' as Tooth17,'' as Tooth18,
        '' as Tooth19,'' as Tooth20,'' as Tooth21,'X' as Tooth22,'' as Tooth23,'' as Tooth24,'' as Tooth25,'' as Tooth26,'' as Tooth27,
        '' as Tooth28,'' as Tooth29,'' as Tooth30,'' as Tooth31,'X' as Tooth32 "
        dtMissTInfo = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

        strSQL = " Select 'XY' as DiagLostQual,'97001' as DiagA,'97002' as DiagB,'97003' as DiagC,'97004' as DiagD,'These are some Diag Code Remarks' as Remarks "
        dtDiag = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

        strSQL = " Select '11' as Place,'X' as Encl,'X' as OrthoY,'' as OrthoN,'11/12/2013' as dtAppl,'11' as Months,'X' as ReplProsY,'' as ReplProsN,'10/11/2012' as dtPrior,
        'X' as TreatFromO,'' as TreatFromAA,'' as TreatFromOA, '04/01/2020' as dtAccident,'NY' as AutoSt "
        dtClm = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

        strSQL = " Select 'BO NAME' as BOName,'7 LEISTER AENUE' as BOAddr,'BOSTON MA 41109' as BOCityStZip,'NPI123' as BONPI,'LIC123' as BOLicNum,
        'TIN123' as BOSSNTIN,'123-456-7890' as BOPhone,'ADDPROID123' as AddlProvId,'05/12/2020' as DdtSign,'NPI123456' as DNPI,
        'DLIC1234' as DLicNo,'9 CITY ROAD' as DAddr,'LAKESHIRE VA 41108' as DCityStZip,'PSC123' as DPRovSpCode,
        '890-687-7868' as DPhone,'ADDPROVID987' as DAddlProvId "
        dtDen = db.DataAdapter(CommandType.Text, strSQL).Tables(0)

        AppManager.ADADental("P", "ADA_C1234_A1234PP.pdf", dtHdr, dtBenPlanInfo, dtOthCvg, dtPolHldInfo, dtPatInfo, dtRecSvcs, dtFee, dtMissTInfo, dtDiag, dtClm, dtDen)
        AppManager.ADADental("B", "ADA_C1234_A1234B.pdf", dtHdr, dtBenPlanInfo, dtOthCvg, dtPolHldInfo, dtPatInfo, dtRecSvcs, dtFee, dtMissTInfo, dtDiag, dtClm, dtDen)

        End
    End Sub

End Class
