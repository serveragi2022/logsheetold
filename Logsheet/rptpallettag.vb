Imports System.IO
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.ComponentModel

Public Class rptpallettag
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim dscmd As New SqlDataAdapter
    Public objRpt As New crpallettag
    Public ds As New DataSet1
    Public sqlquery As String
    Public constring As String
    Public lognum As String, palletid As String, palletloc As String, logticket As String, logline As String

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

    Private Sub rptpallettag_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ds = New DataSet1
        Try
            Dim MyText As TextObject

            'sqlquery = "SELECT itemname,qty,totalprice,discprice FROM tblorder where transnum='" & trnum & "'"
            sqlquery = "SELECT t.palletnum,s.logsheetdate,i.itemname,t.gseries,t.cseries,t.bags,t.location,s.palletizer,s.logsheetnum,t.datecreated,t.addtoloc,t.remarks,t.binnum"
            sqlquery = sqlquery & " FROM tbllogsheet s RIGHT OUTER JOIN tbllogitem i ON s.logsheetid=i.logsheetid RIGHT OUTER JOIN tbllogticket t ON i.logitemid=t.logitemid"
            sqlquery = sqlquery & " where t.logticketid='" & logticket & "'"

            connect()
            sql = procesSQL()
            Me.Cursor = Cursors.WaitCursor
            dscmd = New SqlDataAdapter(sql, conn)
            dscmd.Fill(ds.DataTable1)
            '/MsgBox(ds.Tables(0).Rows.Count)
            '/MsgBox(ds.Tables(0).Rows(0).Item("DataColumn1"))
            conn.Close()


            MyText = CType(objRpt.ReportDefinition.ReportObjects("Text129"), TextObject) 'pallettag
            MyText.Text = ds.Tables(0).Rows(0).Item("DataColumn1")
            MyText = CType(objRpt.ReportDefinition.ReportObjects("Text130"), TextObject) 'pd
            MyText.Text = ds.Tables(0).Rows(0).Item("DataColumn2")
            MyText = CType(objRpt.ReportDefinition.ReportObjects("Text131"), TextObject) 'pd
            MyText.Text = Format(ds.Tables(0).Rows(0).Item("DataColumn11"), "HH:mm")


            '/sql = "SELECT tbllogticket.palletnum,tbllogticket.ticketdate from tbllogticket where tbllogticket.logticketid='" & logticket & "'"
            '/connect()
            '/cmd = New SqlCommand(sql, conn)
            '/dr = cmd.ExecuteReader
            '/If dr.Read Then
            '/ds.Tables(0).Rows(0).Item("DataColumn1") = dr("palletnum")
            '/ds.Tables(0).Rows(0).Item("DataColumn2") = dr("ticketdate")
            '/MyText = CType(objRpt.ReportDefinition.ReportObjects("Text113"), TextObject)
            '/MyText.Text = dr("letter")
            '/MyText = CType(objRpt.ReportDefinition.ReportObjects("Text114"), TextObject)
            '/MyText.Text = dr("letter")
            '/End If
            '/dr.Dispose()
            '/cmd.Dispose()
            '/conn.Close()

            objRpt.SetDataSource(Me.ds.Tables(0))
            CrystalReportViewer1.ReportSource = objRpt
            CrystalReportViewer1.Refresh()

            Me.Cursor = Cursors.Default

            '/Dim a As String = MsgBox("Are you sure you want to print pallet tag # " & palletid & ".", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
            '/If a = vbYes Then
            'MsgBox(ds.Tables(0).Rows(0).Item(0))
            '/objRpt.PrintToPrinter(1, False, 0, 0)
            '/End If

            '/Me.Close()

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
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
        '/Dim MyText As TextObject
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
                '/MyText = CType(objRpt.ReportDefinition.ReportObjects("Text" & i + 1), TextObject)
                '/ MyText.Text = Trim(fields(i).ToString())
            Else
                firstPart = firstPart & fields(i).ToString() & " as DataColumn" & i + 1
                '/MyText = CType(objRpt.ReportDefinition.ReportObjects("Text" & i + 1), TextObject)
                '/MyText.Text = Trim(fields(i).ToString())
            End If
        Next


        sql = "SELECT " & firstPart & " " & lastPart

        Return sql
    End Function
End Class