Imports System.IO
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.ComponentModel

Public Class rptlogsheetrevise
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim dscmd As New SqlDataAdapter
    Dim rpt As ReportDocument
    Public objRpt As New crlogsheetrevise
    Public subobjRpt As New crloghour
    Public ds As New DataSet1
    Public sqlquery As String
    Public constring As String
    Public lognum As String, palletid As String, palletloc As String, logticket As String, logline As String, stat As String
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

    Private Sub rptlogsheetrevise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ds = New DataSet1
        Try
            sqlquery = "SELECT logsheetnum, logsheetdate, whsename, palletizer, shift, logitemid, itemname, location, columns, palletnum, datecreated,"
            sqlquery = sqlquery & " addtoloc, gseries, bags, cticketdate, grossw, cticketnum, qadispo, remarks, createdby, letter1, cremarks, prodrems, qcaremsx"
            sqlquery = sqlquery & " FROM vlogsheetprint"
            sqlquery = sqlquery & " where logsheetnum='" & lognum & "' and branch='" & login.branch & "' order by palletnum" 't.status<>'3'
            connect()
            sql = procesSQL()
            Me.Cursor = Cursors.WaitCursor
            dscmd = New SqlDataAdapter(sql, conn)
            dscmd.Fill(ds.DataTable3)
            'MsgBox(ds.Tables(2).Rows.Count)
            disconnect()

            Dim MyText As TextObject
            '/MyText = CType(objRpt.ReportDefinition.ReportObjects("Text115"), TextObject)
            '/MyText.Text = lognum

            'summary
            For i = 0 To ds.Tables(2).Rows.Count - 1
                If IsDBNull(ds.Tables(2).Rows(i).Item("DataColumn6")) = False Then
                    If ds.Tables(2).Rows(i).Item("DataColumn18") = 1 Then
                        ds.Tables(2).Rows(i).Item("DataColumn18") = "Ok"
                    ElseIf ds.Tables(2).Rows(i).Item("DataColumn18") = 2 Then
                        ds.Tables(2).Rows(i).Item("DataColumn18") = "Hold"
                    Else
                        ds.Tables(2).Rows(i).Item("DataColumn18") = "---"
                    End If

                    sql = "Select Count(loggoodid) from tblloggood where logitemid='" & Val(ds.Tables(2).Rows(i).Item("DataColumn6")) & "' and tblloggood.status<>'3' and palletnum<>''"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    Dim totalgood As Integer = cmd.ExecuteScalar
                    cmd.Dispose()
                    conn.Close()

                    'total good
                    ds.Tables(2).Rows(i).Item("DataColumn25") = Val(totalgood).ToString("n2")

                    sql = "Select Count(cticketnum) from tbllogcancel where (NOT remarks like '%Missing%') and logitemid='" & Val(ds.Tables(2).Rows(i).Item("DataColumn6")) & "' and tbllogcancel.status<>'3'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    Dim totalcancel As Integer = cmd.ExecuteScalar
                    cmd.Dispose()
                    conn.Close()

                    'total cancel
                    ds.Tables(2).Rows(i).Item("DataColumn26") = Val(totalcancel).ToString("n2")
                End If
            Next

            sql = "Select fullname from tblusers where username='" & ds.Tables(2).Rows(0).Item("DataColumn20") & "' and (branch='" & login.branch & "' or branch='All')"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                ds.Tables(2).Rows(ds.Tables(2).Rows.Count - 1).Item("DataColumn20") = dr("fullname")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            sql = "Select fullname from tbllogsheet s right outer join tblusers on s.createdby=tblusers.username"
            sql = sql & " where s.logsheetnum='" & lognum & "' and s.branch='" & login.branch & "' and (tblusers.branch='" & login.branch & "' or tblusers.branch='All')"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text130"), TextObject)
                MyText.Text = dr("fullname")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            sql = "Select millersup, weyt, remarks, binnum from tbllogsheet s where s.logsheetnum='" & lognum & "' and s.branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text141"), TextObject)
                MyText.Text = dr("millersup").ToString
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text146"), TextObject)
                MyText.Text = ""
                If IsDBNull(dr("weyt")) = False Then
                    If dr("weyt") = 1 Then
                        MyText.Text = "With Digital Weighing Scale"
                    End If
                End If
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            sql = "Select t.qaname from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid where s.logsheetnum='" & lognum & "' and t.status<>'3'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text134"), TextObject)
                MyText.Text = dr("qaname").ToString
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

            viewgeneral()
            viewhours()

            '/objRpt.SetDataSource(Me.ds.Tables(2))

            objRpt.SetDataSource(Me.ds.Tables(2))
            objRpt.Subreports(0).SetDataSource(Me.ds.Tables(6))
            CrystalReportViewer1.ReportSource = objRpt
            CrystalReportViewer1.Refresh()

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox("Try again. " & ex.ToString, MsgBoxStyle.Information)
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

    Public Sub viewgeneral()
        Try
            Dim temprange As String = ""
            sql = "Select r.* from tbllogrange r right outer join tbllogsheet s on s.logsheetid=r.logsheetid where s.logsheetnum='" & lognum & "' and r.status<>'3'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim temp1 As String = ""
                If dr("arfrom") < 1000000 Then
                    For vv As Integer = 1 To 6 - dr("arfrom").ToString.Length
                        temp1 += "0"
                    Next
                End If

                Dim temp2 As String = ""
                If dr("arto") < 1000000 Then
                    For vv As Integer = 1 To 6 - dr("arto").ToString.Length
                        temp2 += "0"
                    Next
                End If

                If temprange <> "" Then
                    temprange = temprange & "  ;  " & dr("letter1") & " " & temp1 & dr("arfrom") & " - " & dr("letter2") & " " & temp2 & dr("arto")
                Else
                    temprange = dr("letter1") & " " & temp1 & dr("arfrom") & " - " & dr("letter2") & " " & temp2 & dr("arto")
                End If

            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            '/ txtrange.Text = temprange
            Dim MyText As TextObject
            MyText = CType(objRpt.ReportDefinition.ReportObjects("Text145"), TextObject)
            MyText.Text = temprange

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub viewhours()
        Try
            
            Dim colnum As Integer, i = 0
            sql = "Select tblloghour.num,tblloghour.dfrom,tblloghour.dto, tblloghourwt.* from tblloghour inner join tblloghourwt on tblloghour.num=tblloghourwt.num"
            sql = sql & " where tblloghour.logsheetnum='" & lognum & "' and status='1' order by tblloghourwt.num"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If colnum <> dr("num") Then
                    Dim rr As Integer = 0
                    If dr("num") <> grdhour.Rows.Count + 1 Then
                        rr = dr("num") - (grdhour.Rows.Count + 1)
                        For i = 0 To rr - 1
                            grdhour.Rows.Add()
                            grdhour.Item(0, grdhour.Rows.Count - 1).Value = grdhour.Rows.Count
                        Next
                    End If

                    i = 0
                    colnum = dr("num")
                    grdhour.Rows.Add()
                    grdhour.Item(0, dr("num") - 1).Value = dr("num")
                    i += 1
                    grdhour.Item(i, dr("num") - 1).Value = dr("dfrom").ToString
                    i += 1
                    grdhour.Item(i, dr("num") - 1).Value = dr("dto").ToString
                    i += 1
                    grdhour.Item(i, dr("num") - 1).Value = dr("netw")
                Else
                    grdhour.Item(i, dr("num") - 1).Value = dr("netw")
                End If

                grdhour.Item(8, dr("num") - 1).Value = dr("createdby")

                i += 1
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            For Each row As DataGridViewRow In grdhour.Rows
                Dim col0 As String = grdhour.Rows(row.Index).Cells(0).Value
                Dim col1 As String = grdhour.Rows(row.Index).Cells(1).Value
                Dim col2 As String = grdhour.Rows(row.Index).Cells(2).Value
                Dim col3 As String = grdhour.Rows(row.Index).Cells(3).Value
                Dim col4 As String = grdhour.Rows(row.Index).Cells(4).Value
                Dim col5 As String = grdhour.Rows(row.Index).Cells(5).Value
                Dim col6 As String = grdhour.Rows(row.Index).Cells(6).Value
                Dim col7 As String = grdhour.Rows(row.Index).Cells(7).Value
                Dim col8 As String = grdhour.Rows(row.Index).Cells(8).Value
                ds.Tables(6).Rows.Add()
                ds.Tables(6).Rows(row.Index).Item("DataColumn1") = col0
                ds.Tables(6).Rows(row.Index).Item("DataColumn2") = col1
                ds.Tables(6).Rows(row.Index).Item("DataColumn3") = col2
                ds.Tables(6).Rows(row.Index).Item("DataColumn4") = col3
                ds.Tables(6).Rows(row.Index).Item("DataColumn5") = col4
                ds.Tables(6).Rows(row.Index).Item("DataColumn6") = col5
                ds.Tables(6).Rows(row.Index).Item("DataColumn7") = col6
                ds.Tables(6).Rows(row.Index).Item("DataColumn8") = col7
                ds.Tables(6).Rows(row.Index).Item("DataColumn9") = col8
            Next

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub
End Class