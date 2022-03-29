Imports System.IO
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.ComponentModel

Public Class rptbintag
    Dim lines = System.IO.File.ReadAllLines("connectionstring.txt")
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim dscmd As New SqlDataAdapter
    Public objRpt As New crbintag
    Public ds As New DataSet1
    Public sqlquery As String
    Public constring As String
    Public lognum As String, palletid As String, palletloc As String, logticket As String, logline As String

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

    Private Sub rptbintag_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ds = New DataSet1
        Try
            sql = "SELECT tbllogticket.location,tbllogticket.columns,tbllogitem.itemname,tbllogticket.bags,tbllogticket.astart,tbllogticket.fend,tbllogcancel.cticketnum,tbllogticket.whsechecker,tbllogticket.forklift FROM tbllogitem RIGHT OUTER JOIN tbllogticket ON tbllogitem.logitemid=tbllogticket.logitemid RIGHT OUTER JOIN tbllogcancel ON tbllogticket.logticketid=tbllogcancel.logticketid where tbllogticket.location='1A' and tbllogticket.status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                grd.Rows.Add(dr("location"), dr("columns"), dr("itemname"), dr("bags"), dr("astart"), dr("fend"), dr("cticketnum"), dr("whsechecker"), dr("forklift"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            sql = "Select * from tbllocation where location='" & ticket.lblbinloc.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                lblmax.Text = dr("max")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            sqlquery = "SELECT tbllogticket.location,tbllogitem.itemname,tbllogticket.columns,tbllogticket.bags,tbllogticket.astart,tbllogticket.fend,tbllogticket.cseries,tbllogticket.whsechecker,tbllogticket.forklift FROM tbllogitem RIGHT OUTER JOIN tbllogticket ON tbllogitem.logitemid=tbllogticket.logitemid where tbllogticket.location='" & ticket.lblbinloc.Text & "' and tbllogticket.status='1'"
            connect()
            sql = procesSQL()
            Me.Cursor = Cursors.WaitCursor
            dscmd = New SqlDataAdapter(sql, conn)
            dscmd.Fill(ds.DataTable2)
            'MsgBox(ds.Tables(1).Rows.Count)
            disconnect()

            objRpt.SetDataSource(Me.ds.Tables(1))
            CrystalReportViewer1.ReportSource = objRpt
            CrystalReportViewer1.Refresh()

            Me.Cursor = Cursors.Default

            '/Dim a As String = MsgBox("Are you sure you want to print bin tag ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
            '/If a = vbYes Then
            'MsgBox(ds.Tables(1).Rows(0).Item(0))
            '/objRpt.PrintToPrinter(1, False, 0, 0)
            '/End If

            '/Me.Close()

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
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
                If MyText.Text = "tbllogticket.columns" Then
                    MyText.Text = "#"
                ElseIf MyText.Text = "tbllogticket.bags" Then
                    MyText.Text = "Bags"
                ElseIf MyText.Text = "tbllogticket.astart" Then
                    MyText.Text = "Start Ticket"
                ElseIf MyText.Text = "tbllogticket.fend" Then
                    MyText.Text = "End Ticket"
                ElseIf MyText.Text = "tbllogticket.cseries" Then
                    MyText.Text = "Cancel Ticket"
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