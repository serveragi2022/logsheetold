Imports System.IO
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.ComponentModel

Public Class rptorderfill
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim dscmd As New SqlDataAdapter
    Public objRpt As New crorderfillnew
    Public ds As New DataSet1
    Public sqlquery As String
    Public constring As String
    Public ofnum As String, stat As String, ofid As Integer

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

    Private Sub rptorderfill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ds = New DataSet1
        Try
            Dim MyText As TextObject

            sqlquery = "SELECT tblorderfill.ofnum,tblorderfill.ofdate,tblorderfill.wrsnum,tblorderfill.platenum,tblorderfill.customer,tblorderfill.cusref,"
            sqlquery = sqlquery & " tblofitem.itemname,tbloflog.logsheetdate,tbloflog.logsheetnum,tbloflog.palletnum,tbloflog.ticketseries,tbloflog.selectedbags,"
            sqlquery = sqlquery & " tblorderfill.remarks,tblorderfill.createdby,tbloflog.oflogid,tblorderfill.datecreated,tblorderfill.completed,tblofitem.ofitemid,"
            sqlquery = sqlquery & " tblorderfill.pstart, tblorderfill.pend, tblorderfill.forklift, tblorderfill.stevedore, tblorderfill.passnum, tblorderfill.matdispo, tblorderfill.fgstart, tblorderfill.fgend, tblorderfill.loadstart, tblorderfill.loadend"
            sqlquery = sqlquery & " FROM tblorderfill RIGHT OUTER JOIN tblofitem ON tblorderfill.ofnum=tblofitem.ofnum"
            sqlquery = sqlquery & " RIGHT OUTER JOIN tbloflog ON tblofitem.ofitemid=tbloflog.ofitemid"
            sqlquery = sqlquery & " where tblorderfill.ofid='" & ofid & "' and tblofitem.status<>'3'"
            sqlquery = sqlquery & " order by tbloflog.logsheetnum,tbloflog.palletnum"

            connect()
            sql = procesSQL()
            Me.Cursor = Cursors.WaitCursor
            dscmd = New SqlDataAdapter(sql, conn)
            dscmd.Fill(ds.DataTable4)
            'MsgBox(ds.Tables(3).Rows.Count)
            conn.Close()

            If ds.Tables(3).Rows.Count <> 0 Then
                sql = "Select fullname from tblusers where username='" & ds.Tables(3).Rows(0).Item("DataColumn14") & "' and (branch='" & login.branch & "' or branch='All')"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    ds.Tables(3).Rows(ds.Tables(3).Rows.Count - 1).Item("DataColumn14") = dr("fullname")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                sql = "Select completedby from tblorderfill where ofid='" & ofid & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If IsDBNull(dr("completedby")) = False Then
                        ds.Tables(3).Rows(ds.Tables(3).Rows.Count - 1).Item("DataColumn14") = dr("completedby").ToString
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                For i = 0 To ds.Tables(3).Rows.Count - 1
                    sql = "Select remarks from tblofitem where ofitemid='" & ds.Tables(3).Rows(i).Item("DataColumn18") & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MyText = CType(objRpt.ReportDefinition.ReportObjects("Text128"), TextObject)
                        MyText.Text = dr("remarks").ToString
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                Next
            End If


            If stat <> "" Then
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text124"), TextObject)
                MyText.Text = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - P E N D I N G - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
            Else
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text124"), TextObject)
                MyText.Text = ""
            End If

            objRpt.SetDataSource(Me.ds.Tables(3))
            CrystalReportViewer1.ReportSource = objRpt
            CrystalReportViewer1.Refresh()

            Me.Cursor = Cursors.Default

            '/Dim a As String = MsgBox("Are you sure you want to print log sheet?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
            '/If a = vbYes Then
            'MsgBox(ds.Tables(3).Rows(0).Item(0))
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