Imports System.IO
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.ComponentModel

Public Class rptcoarevise
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim dscmd As New SqlDataAdapter
    Public objRpt As New crcoarevise
    Public ds As New DataSet1
    Public sqlquery As String
    Public constring As String
    Public coanum As String, stat As String, ofnum As String, ofitemid As Integer, qty As Integer, seriesline As String, coaform As String

    Private Sub CrystalReportViewer1_Load(sender As Object, e As EventArgs) Handles CrystalReportViewer1.Load

    End Sub

    Dim AscendingListBox As New List(Of Integer)

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

    Private Sub rptcoarevise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ds = New DataSet1
        Try
            sqlquery = "SELECT c.coaid, c.coanum, c.refnum, c.itemname, c.batchnum, o.cusref, i.numbags,"
            sqlquery = sqlquery & " c.coacustomer, o.platenum, c.loaddate, c.deldate,"
            sqlquery = sqlquery & " s.coasubid, s.proddate, s.expirydate, s.tseries, s.moisture,"
            sqlquery = sqlquery & " s.protein, s.ash, s.wetgluten, s.water, s.others, c.createdby, s.ofproddate"
            sqlquery = sqlquery & " FROM tblcoa c"
            sqlquery = sqlquery & " RIGHT OUTER JOIN tblcoasub s on c.coaid=s.coaid"
            sqlquery = sqlquery & " RIGHT OUTER JOIN tblorderfill o on c.ofnum=o.ofnum"
            sqlquery = sqlquery & " RIGHT OUTER JOIN tblofitem i on c.ofitemid=i.ofitemid"
            sqlquery = sqlquery & " where c.coanum='" & coanum & "' and c.branch='" & login.branch & "'"
            connect()

            sql = procesSQL()
            Me.Cursor = Cursors.WaitCursor
            dscmd = New SqlDataAdapter(sql, conn)
            dscmd.Fill(ds.DataTable6)
            'MsgBox(ds.Tables(5).Rows.Count)
            disconnect()

            Dim Mytext As TextObject
            Mytext = CType(objRpt.ReportDefinition.ReportObjects("Text125"), TextObject)
            sql = "Select itemcode from tblitems where itemname='" & ds.Tables(5).Rows(0).Item("DataColumn4").ToString & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Mytext.Text = dr("itemcode")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Dim bran As Boolean = False
            sql = "Select whsename from tblorderfill where ofnum='" & ofnum & "' and whsename='BRAN WHSE'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                bran = True
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            seriesline = ""
            For i = 0 To ds.Tables(5).Rows.Count - 1

                If bran = False Then
                    'tseries////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    list1.Items.Clear()
                    list2.Items.Clear()

                    Dim table = New DataTable()
                    table.Columns.Add("LetterNum", GetType(String))
                    table.Columns.Add("Number", GetType(Integer))
                    table.Columns.Add("Letter", GetType(Char))

                    sql = "select og.gticketnum, og.letter from tblofitem i right outer join tbloflog ol on i.ofitemid=ol.ofitemid"
                    sql = sql & " right outer join tblcoaoflog col on ol.logsheetdate=col.ofproddate"
                    sql = sql & " right outer join tblofloggood og on ol.oflogid=og.oflogid"
                    sql = sql & " where col.coasubid='" & ds.Tables(5).Rows(i).Item("DataColumn12") & "'"
                    sql = sql & " and i.itemname='" & ds.Tables(5).Rows(0).Item("DataColumn4") & "'"
                    sql = sql & " and i.status!=3 and og.status!=3 and i.ofnum='" & ofnum & "' and i.branch='" & login.branch & "'"
                    '/MsgBox(sql)
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If dr("gticketnum").ToString.Contains("D") = True Then
                            'double
                            list2.Items.Add(dr("letter") & " " & dr("gticketnum"))
                        Else
                            'good
                            '/grdgoods.Rows.Add(dr("gticketnum") & " " & dr("letter"), dr("gticketnum"))
                            table.Rows.Add(dr("gticketnum") & " " & dr("letter"), dr("gticketnum"), dr("letter"))
                        End If
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    '/grdgoods.Sort(grdgoods.Columns(1), System.ComponentModel.ListSortDirection.Ascending)

                    '//apply sort on Letter, then Number column
                    table.DefaultView.Sort = "Letter, Number"

                    grdgoods.Columns.Clear()
                    Me.grdgoods.DataSource = table

                    For Each row As DataGridViewRow In grdgoods.Rows
                        list1.Items.Add(grdgoods.Rows(row.Index).Cells(0).Value)
                    Next


                    'generate series
                    Dim ctrlist As Integer = 0
                    Dim temp As String = "", first As String = "", last As String = ""
                    Dim letter1 As String = "", letter2 As String = ""


                    For Each item As Object In list1.Items
                        ctrlist += 1

                        If first = "" Then
                            letter1 = item
                            letter1 = letter1.Substring(letter1.Length - 1)

                            first = Val(item)
                        End If

                        letter2 = item
                        letter2 = letter2.Substring(letter2.Length - 1)

                        temp = Val(item)
                        last = temp

                        If list1.Items.Count >= ctrlist Then
                            If ctrlist > list1.Items.Count - 1 Then
                                If first = temp Then
                                    Dim gtiket As String = first
                                    Dim tempzero As String = ""

                                    If gtiket < 1000000 Then
                                        For vv As Integer = 1 To 6 - gtiket.Length
                                            tempzero += "0"
                                        Next
                                    End If


                                    list2.Items.Add(letter1 & " " & tempzero & gtiket)
                                    '/MsgBox(" ---1")
                                    last = gtiket

                                Else
                                    Dim gtiket As String = first
                                    Dim tempzero As String = ""
                                    If gtiket < 1000000 Then
                                        For vv As Integer = 1 To 6 - gtiket.Length
                                            tempzero += "0"
                                        Next
                                    End If


                                    Dim gtikettemp As String = temp
                                    Dim tempzerotemp As String = ""
                                    If gtikettemp < 1000000 Then
                                        For vv As Integer = 1 To 6 - gtikettemp.Length
                                            tempzerotemp += "0"
                                        Next
                                    End If


                                    list2.Items.Add(letter1 & " " & tempzero & gtiket & " - " & letter2 & " " & tempzerotemp & gtikettemp)
                                    '/MsgBox("---2")
                                    last = gtikettemp
                                End If
                            Else
                                If temp + 1 < Val(list1.Items(ctrlist)) Then
                                    If first = temp Then
                                        Dim gtiket As String = first
                                        Dim tempzero As String = ""

                                        If gtiket < 1000000 Then
                                            For vv As Integer = 1 To 6 - gtiket.Length
                                                tempzero += "0"
                                            Next
                                        End If


                                        list2.Items.Add(letter1 & " " & tempzero & gtiket)
                                        '/MsgBox(" ---3")
                                        last = gtiket
                                    Else
                                        Dim gtiket As String = first
                                        Dim tempzero As String = ""
                                        If gtiket < 1000000 Then
                                            For vv As Integer = 1 To 6 - gtiket.Length
                                                tempzero += "0"
                                            Next
                                        End If


                                        Dim gtikettemp As String = temp
                                        Dim tempzerotemp As String = ""
                                        If gtikettemp < 1000000 Then
                                            For vv As Integer = 1 To 6 - gtikettemp.Length
                                                tempzerotemp += "0"
                                            Next
                                        End If


                                        list2.Items.Add(letter1 & " " & tempzero & gtiket & " - " & letter2 & " " & tempzerotemp & gtikettemp)
                                        '/MsgBox(" ---4")
                                        last = gtikettemp
                                    End If
                                    first = ""

                                ElseIf temp + 1 > Val(list1.Items(ctrlist)) Then
                                    'next series na ibig sabihin mag kaiba na letter nito
                                    '/MsgBox(temp + 1 & " " & Val(list1.Items(ctrlist)))

                                    Dim gtiket As String = first
                                    Dim tempzero As String = ""
                                    If gtiket < 1000000 Then
                                        For vv As Integer = 1 To 6 - gtiket.Length
                                            tempzero += "0"
                                        Next
                                    End If


                                    Dim gtiketlast As String = Val(last)
                                    Dim tempzerotemp As String = ""
                                    If gtiketlast < 1000000 Then
                                        For vv As Integer = 1 To 6 - gtiketlast.Length
                                            tempzerotemp += "0"
                                        Next
                                    End If

                                    list2.Items.Add(letter1 & " " & tempzero & gtiket & " - " & letter2 & " " & tempzerotemp & gtiketlast)
                                    '/MsgBox(" ---5")
                                    first = ""
                                End If
                            End If
                        End If
                    Next

                    Dim ofseries As String = ""
                    For Each item As Object In list2.Items
                        ofseries = ofseries & item & ", "
                    Next
                    ds.Tables(5).Rows(i).Item("DataColumn15") = ofseries


                    Dim str As String, tempserieswithline As String = ""
                    Dim strArr() As String
                    Dim count As Integer
                    str = ds.Tables(5).Rows(i).Item("DataColumn15")
                    strArr = str.Split(", ")
                    For count = 0 To strArr.Length - 1
                        If tempserieswithline = "" Then
                            tempserieswithline = " " & (strArr(count))
                        Else
                            tempserieswithline = tempserieswithline & vbCrLf & (strArr(count))
                        End If
                    Next

                    ds.Tables(5).Rows(i).Item("DataColumn15") = tempserieswithline
                    seriesline = seriesline & "Prod. Date: " & ds.Tables(5).Rows(i).Item("DataColumn13") & vbCrLf & tempserieswithline & vbCrLf & vbCrLf
                    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                End If

                If ds.Tables(5).Rows(i).Item("DataColumn14").ToString = "" Then
                    '/ds.Tables(5).Rows(i).Item("DataColumn14") = "N/A"
                End If

                If coaform = "Master COA" Then
                    Dim ctr As Integer = 0
                    sql = "Select p.cresult from tblcoaparam p inner join tblcoa c on p.coanum=c.coanum"
                    sql = sql & " where c.coanum='" & coanum & "' and c.branch='" & login.branch & "' and p.coasubid='" & ds.Tables(5).Rows(i).Item("DataColumn12") & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        ctr += 1
                        If ctr = 1 Then
                            ds.Tables(5).Rows(i).Item("DataColumn16") = dr("cresult")
                        ElseIf ctr = 2 Then
                            ds.Tables(5).Rows(i).Item("DataColumn17") = dr("cresult")
                        ElseIf ctr = 3 Then
                            ds.Tables(5).Rows(i).Item("DataColumn18") = dr("cresult")
                        ElseIf ctr = 4 Then
                            ds.Tables(5).Rows(i).Item("DataColumn19") = dr("cresult")
                        ElseIf ctr = 5 Then
                            ds.Tables(5).Rows(i).Item("DataColumn20") = dr("cresult")
                        End If
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Next

            If ds.Tables(5).Rows.Count <> 0 Then
                sql = "Select fullname from tblusers where username='" & ds.Tables(5).Rows(0).Item("DataColumn22") & "' and (branch='" & login.branch & "' or branch='All')"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    ds.Tables(5).Rows(ds.Tables(5).Rows.Count - 1).Item("DataColumn22") = dr("fullname")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            End If
            
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

    Private Sub btncopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncopy.Click
        If seriesline <> String.Empty Then
            Clipboard.Clear()
            Clipboard.SetText(seriesline)
            MsgBox("Successfully copied to clipboard.", MsgBoxStyle.Information, "")
        Else
            Clipboard.Clear()
        End If
    End Sub
End Class