Imports System.IO
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.ComponentModel

Public Class rptlogsheet
    Dim lines = System.IO.File.ReadAllLines("connectionstring.txt")
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim dscmd As New SqlDataAdapter
    Public objRpt As New Copyofcrlogsheet
    Public ds As New DataSet1
    Public sqlquery As String
    Public constring As String
    Public lognum As String, palletid As String, palletloc As String, logticket As String, logline As String, stat As String

    Public Sub connect()
        conn = New SqlConnection
        conn.ConnectionString = strconn
        If conn.State <> ConnectionState.Open Then
            conn.Open()
        End If
    End Sub

    Public Sub disconnect()
        conn = New SqlConnection
        conn.ConnectionString = strconn
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub rptlogsheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ds = New DataSet1
        Try
            sqlquery = "SELECT tbllogsheet.logsheetdate,tbllogsheet.whsename,tbllogsheet.shift,tbllogitem.logitemid,tbllogitem.palletizer,tbllogitem.itemname,"
            sqlquery = sqlquery & "tbllogticket.logticketid,tbllogticket.location,tbllogticket.columns,tbllogticket.tickettype,tbllogticket.gseries,tbllogticket.bags,tbllogcancel.cticketdate,tbllogcancel.grossw,tbllogcancel.cticketnum,tbllogticket.whsechecker"
            sqlquery = sqlquery & " FROM tbllogsheet fULL JOIN tbllogitem ON tbllogsheet.logsheetnum=tbllogitem.logsheetnum"
            sqlquery = sqlquery & " fULL JOIN tbllogticket ON tbllogitem.logitemid=tbllogticket.logitemid"
            sqlquery = sqlquery & " fULL JOIN tbllogcancel ON tbllogticket.logticketid=tbllogcancel.logticketid"
            sqlquery = sqlquery & " where tbllogsheet.logsheetnum='" & lognum & "' and tbllogticket.status<>'3' and tbllogsheet.branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sqlquery, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                grd.Rows.Add(dr("logsheetdate"), dr("whsename"), dr("shift"), dr("logitemid"), dr("palletizer"), dr("itemname"), dr("logticketid"), dr("location"), dr("columns"), dr("tickettype"), dr("gseries"), dr("bags"), dr("cticketdate"), dr("grossw"), dr("cticketnum"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            sqlquery = "SELECT tbllogsheet.logsheetdate,tbllogsheet.whsename,tbllogsheet.shift,tbllogitem.logitemid,tbllogitem.palletizer,tbllogitem.itemname,"
            sqlquery = sqlquery & "tbllogticket.logticketid,tbllogticket.location,tbllogticket.columns,tbllogticket.tickettype,tbllogticket.gseries,tbllogticket.bags,tbllogcancel.cticketdate,tbllogcancel.grossw,tbllogcancel.cticketnum,tbllogticket.whsechecker"
            sqlquery = sqlquery & " FROM tbllogsheet fULL JOIN tbllogitem ON tbllogsheet.logsheetnum=tbllogitem.logsheetnum"
            sqlquery = sqlquery & " fULL JOIN tbllogticket ON tbllogitem.logitemid=tbllogticket.logitemid"
            sqlquery = sqlquery & " fULL JOIN tbllogcancel ON tbllogticket.logticketid=tbllogcancel.logticketid"
            sqlquery = sqlquery & " where tbllogsheet.logsheetnum='" & lognum & "' and tbllogticket.status<>'3' and tbllogsheet.branch='" & login.branch & "'"
            connect()
            sql = procesSQL()
            Me.Cursor = Cursors.WaitCursor
            dscmd = New SqlDataAdapter(sql, conn)
            dscmd.Fill(ds.DataTable3)
            'MsgBox(ds.Tables(2).Rows.Count)

            conn.Close()

            Dim MyText As TextObject
            MyText = CType(objRpt.ReportDefinition.ReportObjects("Text115"), TextObject)
            MyText.Text = lognum
            MyText = CType(objRpt.ReportDefinition.ReportObjects("Text122"), TextObject)
            MyText.Text = login.user

            'ds.Tables(2).Rows(0).Item("DataColumn6")
            For i = 0 To ds.Tables(2).Rows.Count - 1
                If IsDBNull(ds.Tables(2).Rows(i).Item("DataColumn4")) = False Then
                    sql = "Select Sum(tbllogticket.bags) from tbllogticket right outer join tbllogitem on tbllogticket.logitemid=tbllogitem.logitemid where tbllogticket.logsheetnum='" & lognum & "' and tbllogitem.logitemid='" & Val(ds.Tables(2).Rows(i).Item("DataColumn4")) & "' and tbllogticket.status<>'3'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    Dim totalgood As Integer = cmd.ExecuteScalar
                    cmd.Dispose()
                    conn.Close()

                    sql = "Select Count(cticketnum) from tbllogcancel where logsheetnum='" & lognum & "' and logitemid='" & Val(ds.Tables(2).Rows(i).Item("DataColumn4")) & "' and tbllogcancel.status<>'3'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    Dim totalcancel As Integer = cmd.ExecuteScalar
                    cmd.Dispose()
                    conn.Close()

                    'total production
                    ds.Tables(2).Rows(i).Item("DataColumn17") = Val(totalgood + totalcancel).ToString("n2")
                    'total valid
                    ds.Tables(2).Rows(i).Item("DataColumn18") = Val(totalgood).ToString("n2")
                    'total cancel
                    ds.Tables(2).Rows(i).Item("DataColumn19") = Val(totalcancel).ToString("n2")
                End If
            Next


            sql = "Select * from tblusers where username='" & ds.Tables(2).Rows(0).Item("DataColumn20") & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                ds.Tables(2).Rows(0).Item("DataColumn20") = dr("fullname")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            If stat <> "" Then
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text123"), TextObject)
                MyText.Text = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - P E N D I N G - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
            Else
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text123"), TextObject)
                MyText.Text = ""
            End If

            objRpt.SetDataSource(Me.ds.Tables(2))
            CrystalReportViewer1.ReportSource = objRpt
            CrystalReportViewer1.Refresh()

            Me.Cursor = Cursors.Default

            '/Dim a As String = MsgBox("Are you sure you want to print log sheet?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
            '/If a = vbYes Then
            'MsgBox(ds.Tables(2).Rows(0).Item(0))
            '/objRpt.PrintToPrinter(1, False, 0, 0)
            '/End If

            '/Me.Close()

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

                If MyText.Text = "tbllogsheet.logsheetdate" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogsheet.whsename" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogsheet.shift" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogitem.logitemid" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogitem.palletizer" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogticket.logticketid" Then
                    MyText.Text = "Pallet Tag Number"
                ElseIf MyText.Text = "tbllogticket.location" Then
                    MyText.Text = "Location"
                ElseIf MyText.Text = "tbllogticket.columns" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogitem.itemname" Then
                    MyText.Text = "Item Description"
                ElseIf MyText.Text = "tbllogticket.gseries" Then
                    MyText.Text = "Ticket Series"
                ElseIf MyText.Text = "tbllogticket.bags" Then
                    MyText.Text = "Bags"
                ElseIf MyText.Text = "tbllogcancel.cticketdate" Then
                    MyText.Text = "Time"
                ElseIf MyText.Text = "tbllogcancel.grossw" Then
                    MyText.Text = "Gross W"
                ElseIf MyText.Text = "tbllogcancel.cticketnum" Then
                    MyText.Text = "Cancel Ticket"
                ElseIf MyText.Text = "tbllogticket.whsechecker" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogsheet.logsheetnum" Then
                    MyText.Text = ""
                End If
            Else
                firstPart = firstPart & fields(i).ToString() & " as DataColumn" & i + 1
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text" & i + 1), TextObject)
                MyText.Text = Trim(fields(i).ToString())

                'tbllogticket.location,tbllogitem.itemname,tbllogticket.columns,tbllogticket.bags,tbllogticket.astart,tbllogticket.fend,tbllogticket.cseries
            End If
        Next


        sql = "SELECT " & firstPart & " " & lastPart

        Return sql
    End Function
End Class