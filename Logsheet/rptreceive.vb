Imports System.IO
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.ComponentModel

Public Class rptreceive
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim dscmd As New SqlDataAdapter
    Dim rpt As ReportDocument
    Public objRpt As New crreceive
    Public ds As New DataSet1
    Public sqlquery As String
    Public constring As String
    Public recid As Integer
    Dim dtGridSource As DataTable

    Private Sub connect()
        conn = New SqlConnection
        conn.ConnectionString = strconn
        If conn.State <> ConnectionState.Open Then
            conn.Open()
        End If
    End Sub

    Private Sub disconnect()
        conn = New SqlConnection
        conn.ConnectionString = strconn
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub rptreceive_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ds = New DataSet1
        Try
            sqlquery = "SELECT r.recid, r.recdate,r.platenum, r.driver, r.frmbranch, r.qty, r.datecreated, r.createdby, r.datemodified, r.modifiedby, l.remarks,"
            sqlquery = sqlquery & " l.logsheetdate , l.logsheetnum, i.itemname, l.qty, o.wrsnum, o.ofnum FROM tblreceive r "
            sqlquery = sqlquery & " inner join tbllogsheet l on r.recnum=l.recnum and r.branch=l.branch and l.status<>'3'"
            sqlquery = sqlquery & " inner join tblrecof o on o.logsheetnum=l.reclogsheet and o.branch=l.branch and o.status<>'3' and r.recnum=o.recnum"
            sqlquery = sqlquery & " inner join tbllogitem i on i.logsheetid=l.logsheetid and i.status<>'3' "
            sqlquery = sqlquery & " where (recid = '" & recid & "')"
            sqlquery = sqlquery & " group by r.recid, r.recdate,r.platenum, r.driver, r.frmbranch, r.qty,  r.datecreated, r.createdby, r.datemodified, r.modifiedby,"
            sqlquery = sqlquery & " o.wrsnum, o.ofnum, l.logsheetdate, l.logsheetnum, i.itemname, l.qty, l.remarks"

            connect()
            sql = procesSQL()
            Me.Cursor = Cursors.WaitCursor
            dscmd = New SqlDataAdapter(sql, conn)
            dscmd.Fill(ds.DataTable8)
            'MsgBox(ds.Tables(7).Rows.Count)
            conn.Close()

            Dim MyText As TextObject
            '/MyText = CType(objRpt.ReportDefinition.ReportObjects("Text115"), TextObject)
            '/MyText.Text = lognum

            'summary
            sql = "Select fullname from tblusers where username='" & ds.Tables(7).Rows(0).Item("DataColumn8") & "' and (branch='" & login.branch & "' or branch='All')"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                ds.Tables(7).Rows(0).Item("DataColumn8") = dr("fullname")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            sql = "Select fullname from tblusers where username='" & ds.Tables(7).Rows(0).Item("DataColumn10") & "' and (branch='" & login.branch & "' or branch='All')"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                ds.Tables(7).Rows(0).Item("DataColumn10") = dr("fullname")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            MyText = CType(objRpt.ReportDefinition.ReportObjects("Text12"), TextObject)
            MyText.Text = "Production Date"
            MyText = CType(objRpt.ReportDefinition.ReportObjects("Text13"), TextObject)
            MyText.Text = "Ticket Log Sheet#"
            MyText = CType(objRpt.ReportDefinition.ReportObjects("Text14"), TextObject)
            MyText.Text = "Item Name"
            MyText = CType(objRpt.ReportDefinition.ReportObjects("Text15"), TextObject)
            MyText.Text = "Qty"
            MyText = CType(objRpt.ReportDefinition.ReportObjects("Text16"), TextObject)
            MyText.Text = "From WRS #"
            MyText = CType(objRpt.ReportDefinition.ReportObjects("Text17"), TextObject)
            MyText.Text = "Order Fill#"

            '/objRpt.SetDataSource(Me.ds.Tables(7))

            objRpt.SetDataSource(Me.ds.Tables(7))
            CrystalReportViewer1.ReportSource = objRpt
            CrystalReportViewer1.Refresh()

            Me.Cursor = Cursors.Default

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try
    End Sub

    Public Function procesSQL() As String
        Dim inSql As String
        Dim firstPart As String
        Dim lastPart As String
        Dim selectStart As Integer
        Dim fromStart As Integer
        Dim fields As String()
        Dim i As Integer
        Dim MyText As TextObject
        'Dim mycol As FieldObject

        inSql = sqlquery
        'inSql = inSql.ToUpper
        selectStart = inSql.IndexOf("SELECT")
        fromStart = inSql.IndexOf("FROM")
        selectStart = selectStart + 6
        firstPart = inSql.Substring(selectStart, (fromStart - selectStart))
        lastPart = inSql.Substring(fromStart, inSql.Length - fromStart)

        fields = firstPart.Split(",")
        firstPart = ""
        'MsgBox(fields.Length)
        For i = 0 To fields.Length - 1
            If i > 0 Then
                firstPart = firstPart & " , " & fields(i).ToString() & " as DataColumn" & i + 1
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text" & i + 1), TextObject)
                MyText.Text = Trim(fields(i).ToString())

            Else
                firstPart = firstPart & fields(i).ToString() & " as DataColumn" & i + 1
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text" & i + 1), TextObject)
                MyText.Text = Trim(fields(i).ToString())
            End If
        Next


        sql = "SELECT " & firstPart & " " & lastPart

        Return sql
    End Function
End Class