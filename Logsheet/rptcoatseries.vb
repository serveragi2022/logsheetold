Imports System.IO
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.ComponentModel

Public Class rptcoatseries
    Dim lines = System.IO.File.ReadAllLines("connectionstring.txt")
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim dscmd As New SqlDataAdapter
    Public objRpt As New crcoatseries
    Public ds As New DataSet1
    Public sqlquery As String
    Public constring As String
    Public coanum As String, stat As String, ofnum As String, ofitemid As Integer, qty As Integer

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

    Private Sub rptcoarevise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ds = New DataSet1
        Try
            sqlquery = "SELECT tblcoa.coaid, tblcoa.coanum, tblcoa.refnum, tblcoa.itemname, tblcoa.batchnum, tblorderfill.cusref, tblofitem.numbags,"
            sqlquery = sqlquery & " tblorderfill.customer, tblorderfill.platenum, tblcoa.loaddate, tblcoa.deldate,"
            sqlquery = sqlquery & " tblcoasub.coasubid, tblcoasub.proddate, tblcoasub.expirydate, tblcoasub.tseries, tblcoasub.moisture, tblcoasub.protein, tblcoasub.ash, tblcoasub.wetgluten, tblcoasub.water, tblcoasub.others"
            sqlquery = sqlquery & " FROM tblcoa"
            sqlquery = sqlquery & " RIGHT OUTER JOIN tblcoasub on tblcoa.coaid=tblcoasub.coaid"
            sqlquery = sqlquery & " RIGHT OUTER JOIN tblorderfill on tblcoa.ofnum=tblorderfill.ofnum"
            sqlquery = sqlquery & " RIGHT OUTER JOIN tblofitem on tblcoa.ofitemid=tblofitem.ofitemid"
            sqlquery = sqlquery & " where tblcoa.coanum='" & coanum & "'"
            connect()

            sql = procesSQL()
            Me.Cursor = Cursors.WaitCursor
            dscmd = New SqlDataAdapter(sql, conn)
            dscmd.Fill(ds.DataTable6)
            'MsgBox(ds.Tables(5).Rows.Count)
            disconnect()


            'datacolumn4 - itemcode
            '/sql = "Select * from tblitems where itemname='" & ds.Tables(5).Rows(0).Item("DataColumn3").ToString & "'"
            '/connect()
            '/cmd = New SqlCommand(sql, conn)
            '/dr = cmd.ExecuteReader
            '/If dr.Read Then
            '/ ds.Tables(5).Rows(0).Item("DataColumn4") = dr("itemcode")
            '/End If
            '/dr.Dispose()
            '/cmd.Dispose()


            objRpt.SetDataSource(Me.ds.Tables(5))
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

        For i = 0 To fields.Length - 1
            If i > 0 Then
                firstPart = firstPart & " , " & fields(i).ToString() & " as DataColumn" & i + 1
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text" & i + 1), TextObject)
                MyText.Text = Trim(fields(i).ToString())

                '/ If MyText.Text = "tbllogsheet.logsheetdate" Then
                '/MyText.Text = ""
                '/End If
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