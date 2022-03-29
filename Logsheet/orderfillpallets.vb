﻿Imports System.IO
Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class orderfillpallets
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public orderfillitemid As String, ofnum As String, ofc As String
    Dim selectedrow As Integer
    Dim clickbtn As String, gridsql As String, loginwhse As String, loginbranch As String, ofcus As String, ofn As String

    Private threadEnabled As Boolean, threadEnabledstat As Boolean, threadEnabledcount As Boolean
    Private bwork As BackgroundWorker, bworkstat As BackgroundWorker, bworkcount As BackgroundWorker

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

    Private Sub orderfillpallets_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If threadEnabled = True Then
            If bwork.IsBusy = True Then
                If bwork.WorkerSupportsCancellation = True Then
                    bwork.CancelAsync()
                End If
            End If
        End If

        If threadEnabledstat = True Then
            If bworkstat.IsBusy = True Then
                If bworkstat.WorkerSupportsCancellation = True Then
                    bworkstat.CancelAsync()
                End If
            End If
        End If

        If threadEnabledcount = True Then
            If bworkcount.IsBusy = True Then
                If bworkcount.WorkerSupportsCancellation = True Then
                    bworkcount.CancelAsync()
                End If
            End If
        End If

        txtletter.Text = ""
        txtpallet.Text = ""
        txtticket.Text = ""
        cmbloc.Text = ""
    End Sub

    Private Sub orderfillpallets_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For i = 0 To grdpallets.Columns.Count - 1
            If i <> 1 Then
                grdpallets.Columns(i).ReadOnly = True
            End If
        Next

        grdselect.Columns(0).ReadOnly = True
        grdselect.Columns(1).ReadOnly = True
        grdselect.Columns(2).ReadOnly = True
        grdselect.Columns(3).ReadOnly = True
        grdselect.Columns(4).ReadOnly = True
        grdselect.Columns(5).ReadOnly = True
        grdselect.Columns(6).ReadOnly = True
        grdselect.Columns(7).ReadOnly = True

        loginbranch = login.branch
        ofcus = ofc
        ofn = ofnum
    End Sub

    Private Sub orderfillpallets_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        viewlocation()
        '/btnsearch.PerformClick()
        viewselected()
    End Sub

    Private Sub viewlocation()
        Try
            cmbloc.Items.Clear()

            sql = "Select * from tbllocation where whsename='" & lblwhse.Text & "' and status='1' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbloc.Items.Add(dr("location"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub viewselected()
        Try
            Dim totalbags As Integer = 0

            If orderfill.grdlog.Rows.Count <> 0 Then
                'to gaya the number of selected bags
                grdselect.Rows.Clear()

                For Each rowlog As DataGridViewRow In orderfill.grdlog.Rows
                    Dim col0 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(0).Value
                    Dim col1 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(1).Value
                    Dim col2 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(2).Value
                    Dim col3 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(3).Value
                    Dim col4 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(7).Value
                    Dim col5 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(4).Value
                    Dim col6 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(5).Value
                    Dim col7 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(6).Value
                    Dim col8 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(10).Value
                    Dim collogticketid As String = orderfill.grdlog.Rows(rowlog.Index).Cells(12).Value
                    Dim colpickticket As Boolean = orderfill.grdlog.Rows(rowlog.Index).Cells(13).Value

                    grdselect.Rows.Add(col0, col1, col2, col3, col4, col5, col6, col7, col8, collogticketid, colpickticket)

                    totalbags = totalbags + col8
                Next

            Else
                'kunin yung nasa db na nkaselect na
                grdselect.Rows.Clear()

                sql = "Select t.logticketid, s.logsheetdate, s.logsheetnum, t.palletnum, t.letter1, t.astart, t.letter4, t.fend from tbllogticket t "
                sql = sql & " Right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                sql = sql & " Right outer join tbllogitem i on i.logitemid=t.logitemid"
                sql = sql & " where i.itemname='" & orderfill.cmbitem.Text & "' and NOT t.addtoloc is NULL and t.ofnum='" & ofnum & "' and t.status<>'3' and s.branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    grdselect.Rows.Add(dr("logticketid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), 0)
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                Dim totalgood As Integer = 0, totaldouble As Integer = 0
                For Each row As DataGridViewRow In grdselect.Rows
                    Dim lognum As String = grdselect.Rows(row.Index).Cells(2).Value
                    'tblloggood no of available
                    'sql = "Select Count(loggoodid) from tblloggood where logsheetnum='" & lognum & "' and palletnum='" & grdselect.Rows(row.Index).Cells(3).Value & "' and status='1' and branch='" & login.branch & "'"
                    sql = "Select Count(g.loggoodid) from tblloggood g right outer join tbllogticket t on t.logticketid=g.logticketid"
                    sql = sql & " Right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                    sql = sql & " where s.logsheetnum='" & lognum & "' and t.palletnum='" & grdselect.Rows(row.Index).Cells(3).Value & "' and g.status='1' and s.branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    totalgood = cmd.ExecuteScalar
                    cmd.Dispose()
                    conn.Close()

                    'tbllogdouble no of available
                    sql = "Select Count(d.logdoubleid) from tbllogdouble d right outer join tbllogticket t on t.logticketid=d.logticketid"
                    sql = sql & " Right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                    sql = sql & " where s.logsheetnum='" & lognum & "' and t.palletnum='" & grdselect.Rows(row.Index).Cells(3).Value & "' and d.status='1' and s.branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    totaldouble = cmd.ExecuteScalar
                    cmd.Dispose()
                    conn.Close()

                    grdselect.Item(4, row.Index).Value = totalgood + totaldouble
                    grdselect.Item(8, row.Index).Value = totalgood + totaldouble

                    totalbags = totalbags + grdselect.Item(8, row.Index).Value
                Next
            End If

            lbltotal.Text = totalbags.ToString("n2")

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            gpallets.Text = "Searched Pallets"
            clickbtn = "Search"

            If Trim(txtpallet.Text) <> "" Then
                searchpallet()
                Exit Sub
            ElseIf Trim(txtticket.Text) <> "" Or Trim(txtletter.Text) <> "" Then
                searchticket()
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            lblloading.Location = New Point(3, 9)
            lblloading.Visible = True
            pgb1.Style = ProgressBarStyle.Marquee
            pgb1.Visible = True
            pgb1.Value = 0

            cmbtemp.Items.Clear()
            grdpallets.Rows.Clear()

            sql = "Select * from vofpickpallets"
            sql = sql & " where logsheetdate='" & Format(datefrom.Value, "yyyy/MM/dd") & "' and branch='" & login.branch & "'"
            sql = sql & " and qadispo='1' and itemname='" & orderfill.cmbitem.Text & "' and ticketstatus='1' and itemstatus='2'"

            If Trim(cmbloc.Text) <> "" Then
                sql = sql & " and location='" & Trim(cmbloc.Text) & "'"
            End If

            gridsql = sql

            grdpallets.Rows.Clear()

            bwork = New BackgroundWorker()
            bwork.WorkerSupportsCancellation = True
            bworkstat = New BackgroundWorker()
            bworkstat.WorkerSupportsCancellation = True
            bworkcount = New BackgroundWorker()
            bworkcount.WorkerSupportsCancellation = True

            AddHandler bwork.DoWork, New DoWorkEventHandler(AddressOf bwork_DoWork)
            AddHandler bwork.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bwork_Completed)
            AddHandler bwork.ProgressChanged, New ProgressChangedEventHandler(AddressOf bwork_ProgressChanged)
            m_addRowDelegate = New AddRowDelegate(AddressOf AddDGVRow)

            AddHandler bworkcount.DoWork, New DoWorkEventHandler(AddressOf bworkcount_DoWork)
            AddHandler bworkcount.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bworkcount_Completed)
            AddHandler bworkcount.ProgressChanged, New ProgressChangedEventHandler(AddressOf bworkcount_ProgressChanged)

            If Not bwork.IsBusy Then
                bwork.WorkerReportsProgress = True
                bwork.WorkerSupportsCancellation = True
                bwork.RunWorkerAsync() 'start ng select query
            End If

            '/countnumbags()

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub bwork_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        threadEnabled = True
        Dim cnt As Integer = 0, i As Integer = 0, pick As Boolean = False, colorrow As String = "", redonly As Boolean = False, stat As String = ""

        Dim connection As SqlConnection
        connection = New SqlConnection
        connection.ConnectionString = strconn
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If
        cmd = New SqlCommand(gridsql, connection)
        Dim drx As SqlDataReader = cmd.ExecuteReader
        While drx.Read
            If bwork.CancellationPending = True Then
                e.Cancel = True
                Exit While
            End If
            pick = False
            colorrow = ""
            redonly = False
            stat = ""

            If drx("cusreserve") = 0 Then
                stat = "Available"
            ElseIf drx("cusreserve") = 1 Then
                stat = "Reserved"
                If drx("customer") = ofcus And drx("ofnum").ToString = ofn Then
                    colorrow = "Yellow"
                    redonly = True
                ElseIf drx("customer") = ofcus Then
                    colorrow = "NavajoWhite"
                Else
                    colorrow = "DeepSkyBlue"
                End If
            Else
                stat = "Selected"
                If drx("ofnum").ToString = ofn Then
                     colorrow = "Yellow"
                    redonly = False
                Else
                    colorrow = "NavajoWhite"
                End If
            End If

            AddDGVRow(drx("logticketid"), pick, drx("logsheetdate"), drx("location"), drx("logsheetnum"), drx("palletnum"), 0, drx("letter1"), drx("letter1") & " " & drx("astart"), drx("letter4") & " " & drx("fend"), stat, drx("ofnum").ToString, drx("prodrems").ToString, drx("remarks").ToString, drx("qarems").ToString, i, colorrow, redonly)
            'AddDGVRow(drx("logticketid"), False, Format(drx("ticketdate"), "yyyy/MM/dd"), drx("location"), drx("logsheetnum"), drx("palletnum"), 0, "", drx("letter1") & " " & drx("astart"), drx("letter4") & " " & drx("fend"), stat, drx("ofnum").ToString, drx("ofnum").ToString, "", drx("remarks").ToString, drx("qarems").ToString, i, colorrow, redonly)
            i += 1
        End While
        drx.Dispose()
        cmd.Dispose()
        connection.Close()
    End Sub

    Delegate Sub AddRowDelegate(ByVal value0 As Object, ByVal value1 As Object, ByVal value2 As Object, ByVal value3 As Object, ByVal value4 As Object, ByVal value5 As Object, ByVal value6 As Object, ByVal value7 As Object, ByVal value8 As Object, ByVal value9 As Object, ByVal value10 As Object, ByVal value11 As Object, ByVal value12 As Object, ByVal value13 As Object, ByVal value14 As Object, ByVal valuerow As Object, ByVal colorrow As Object, ByVal onlyrow As Object)
    Private m_addRowDelegate As AddRowDelegate

    Private Sub AddDGVRow(ByVal v0 As Integer, ByVal v1 As Boolean, ByVal v2 As Date, ByVal v3 As String, ByVal v4 As String, ByVal v5 As String, ByVal v6 As Integer, ByVal v7 As String, ByVal v8 As String, ByVal v9 As String, ByVal v10 As String, ByVal v11 As String, ByVal v12 As String, ByVal v13 As String, ByVal v14 As String, ByVal vrow As Integer, ByVal colorrow As String, ByVal onlyrow As Boolean)
        If threadEnabled = True Then
            If grdpallets.InvokeRequired Then
                grdpallets.BeginInvoke(New AddRowDelegate(AddressOf AddDGVRow), v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, vrow, colorrow, onlyrow)
            Else
                grdpallets.Rows.Add(v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14)
                grdpallets.Rows(vrow).Cells(1).ReadOnly = onlyrow
                grdpallets.Rows(vrow).Cells(0).Tag = colorrow
            End If
        End If
    End Sub

    Private Sub bwork_Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        If Not bwork.IsBusy Then
            bworkcount.WorkerReportsProgress = True
            bworkcount.WorkerSupportsCancellation = True
            bworkcount.RunWorkerAsync()
        End If
    End Sub

    Private Sub bwork_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)

    End Sub

    Private Sub bworkstat_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        threadEnabledstat = True

        Dim cnt As Integer = 0, i As Integer = 0

        For Each item As Object In cmbtemp.Items
            If bworkstat.CancellationPending = True Then
                e.Cancel = True
                Exit For
            End If
            sql = "Select t.* from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
            sql = sql & " where t.palletnum='" & item & "' and s.branch='" & loginbranch & "' and t.logitemid=(Select logitemid from tbllogitem as i where i.logitemid=t.logitemid and itemname='" & orderfill.cmbitem.Text & "'"
            Dim connection As SqlConnection
            connection = New SqlConnection
            connection.ConnectionString = strconn
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            cmd = New SqlCommand(sql, connection)
            Dim drx As SqlDataReader = cmd.ExecuteReader
            If drx.Read Then
                If IsDBNull(drx("addtoloc")) = False Then
                    Dim stat As String = "", colorrow As String = "", redonly As Boolean = False
                    If drx("cusreserve") = 0 Then
                        stat = "Available"
                        '/grdpallets.Rows.Add(drx("logticketid"), False, Format(drx("ticketdate"), "yyyy/MM/dd"), drx("location"), drx("logsheetnum"), drx("palletnum"), 0, "", drx("letter1") & " " & drx("astart"), drx("letter4") & " " & drx("fend"), stat, drx("ofnum").ToString, drx("ofnum").ToString, "", drx("remarks").ToString, drx("qarems").ToString)
                    ElseIf drx("cusreserve") = 1 Then
                        stat = "Reserved"
                        If drx("customer") = ofcus And drx("ofnum").ToString = ofn Then
                            '/grdpallets.Rows.Add(drx("logticketid"), False, Format(drx("ticketdate"), "yyyy/MM/dd"), drx("location"), drx("logsheetnum"), drx("palletnum"), 0, "", drx("letter1") & " " & drx("astart"), drx("letter4") & " " & drx("fend"), stat, drx("ofnum").ToString, drx("ofnum").ToString, "", drx("remarks").ToString, drx("qarems").ToString)
                            '/grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                            '/grdpallets.Rows(grdpallets.Rows.Count - 1).Cells(1).ReadOnly = True
                            colorrow = "Yellow"
                            redonly = True
                        ElseIf drx("customer") = ofcus Then
                            '/grdpallets.Rows.Add(drx("logticketid"), False, Format(drx("ticketdate"), "yyyy/MM/dd"), drx("location"), drx("logsheetnum"), drx("palletnum"), 0, "", drx("letter1") & " " & drx("astart"), drx("letter4") & " " & drx("fend"), stat, drx("ofnum").ToString, drx("ofnum").ToString, "", drx("remarks").ToString, drx("qarems").ToString)
                            '/grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.NavajoWhite
                            colorrow = "NavajoWhite"
                        Else
                            '/grdpallets.Rows.Add(drx("logticketid"), False, Format(drx("ticketdate"), "yyyy/MM/dd"), drx("location"), drx("logsheetnum"), drx("palletnum"), 0, "", drx("letter1") & " " & drx("astart"), drx("letter4") & " " & drx("fend"), stat, drx("ofnum").ToString, drx("ofnum").ToString, "", drx("remarks").ToString, drx("qarems").ToString)
                            '/grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                            colorrow = "DeepSkyBlue"
                        End If
                    Else
                        stat = "Selected"
                        If drx("ofnum").ToString = ofn Then
                            '/grdpallets.Rows.Add(drx("logticketid"), False, Format(drx("ticketdate"), "yyyy/MM/dd"), drx("location"), drx("logsheetnum"), drx("palletnum"), 0, "", drx("letter1") & " " & drx("astart"), drx("letter4") & " " & drx("fend"), stat, drx("ofnum").ToString, drx("ofnum").ToString, "", drx("remarks").ToString, drx("qarems").ToString)
                            '/grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                            '/grdpallets.Rows(grdpallets.Rows.Count - 1).Cells(1).ReadOnly = True
                            colorrow = "Yellow"
                            redonly = True
                        Else
                            '/grdpallets.Rows.Add(drx("logticketid"), False, Format(drx("ticketdate"), "yyyy/MM/dd"), drx("location"), drx("logsheetnum"), drx("palletnum"), 0, "", drx("letter1") & " " & drx("astart"), drx("letter4") & " " & drx("fend"), stat, drx("ofnum").ToString, drx("ofnum").ToString, "", drx("remarks").ToString, drx("qarems").ToString)
                            '/grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.NavajoWhite
                            colorrow = "NavajoWhite"
                        End If
                    End If

                    'AddDGVRow(drx("logticketid"), False, Format(drx("ticketdate"), "yyyy/MM/dd"), drx("location"), drx("logsheetnum"), drx("palletnum"), 0, "", drx("letter1") & " " & drx("astart"), drx("letter4") & " " & drx("fend"), stat, drx("ofnum").ToString, drx("ofnum").ToString, "", drx("remarks").ToString, drx("qarems").ToString, i, colorrow, redonly)
                End If
            End If
            drx.Dispose()
            cmd.Dispose()
            connection.Close()

            i += 1
        Next
    End Sub

    Private Sub bworkstat_Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        If Not bworkstat.IsBusy Then
            bworkcount.WorkerReportsProgress = True
            bworkcount.WorkerSupportsCancellation = True
            bworkcount.RunWorkerAsync()
        End If
    End Sub

    Private Sub bworkstat_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
    End Sub

    Private Sub bworkcount_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        threadEnabledcount = True
        Dim cnt As Integer = 0, i As Integer = 0

        Dim totalgood As Integer = 0, totaldouble As Integer = 0, prems As String = ""
        'add remarks ni prod 13
        For Each row As DataGridViewRow In grdpallets.Rows
            If bworkcount.CancellationPending = True Then
                e.Cancel = True
                Exit For
            End If
            'tblloggood no of available
            sql = "Select Count(g.loggoodid) from tblloggood g right outer join tbllogticket t on t.logticketid=g.logticketid right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
            sql = sql & " where t.logticketid='" & grdpallets.Rows(row.Index).Cells(0).Value & "' and g.status='1' and s.branch='" & loginbranch & "'"
            Dim connection As SqlConnection
            connection = New SqlConnection
            connection.ConnectionString = strconn
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            cmd = New SqlCommand(sql, connection)
            totalgood = cmd.ExecuteScalar
            cmd.Dispose()
            connection.Close()

            'tbllogdouble no of available
            sql = "Select Count(d.logdoubleid) from tbllogdouble d right outer join tbllogticket t on t.logticketid=d.logticketid right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
            sql = sql & " where t.logticketid='" & grdpallets.Rows(row.Index).Cells(0).Value & "' and d.status='1' and s.branch='" & loginbranch & "'"
            connection = New SqlConnection
            connection.ConnectionString = strconn
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            cmd = New SqlCommand(sql, connection)
            totaldouble = cmd.ExecuteScalar
            cmd.Dispose()
            connection.Close()

            AddDGVcount(totalgood + totaldouble, i)

            i += 1
        Next
    End Sub

    Delegate Sub AddcountDelegate(ByVal value0 As Object, ByVal vrow As Object)
    Private m_addcountDelegate As AddcountDelegate

    Private Sub AddDGVcount(ByVal v0 As Integer, ByVal vrow As Integer)
        If threadEnabled = True Then
            If grdpallets.InvokeRequired Then
                grdpallets.BeginInvoke(New AddcountDelegate(AddressOf AddDGVcount), v0, vrow)
            Else
                grdpallets.Rows(vrow).Cells(6).Value = v0
            End If
        End If
    End Sub

    Private Sub bworkcount_Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Me.Cursor = Cursors.Default
        lblloading.Visible = False
        pgb1.Visible = False
        pgb1.Style = ProgressBarStyle.Blocks

        If e.Error IsNot Nothing Then
            MsgBox(e.Error.ToString, MsgBoxStyle.Critical, "")
        ElseIf e.Cancelled = True Then
            MsgBox("Operation is cancelled.", MsgBoxStyle.Exclamation, "")
        Else
            If grdpallets.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Critical, "")
            Else
                grdpallets.SuspendLayout()
                grdpallets.ResumeLayout()
                '/If clickbtn <> "Search" Then
                '/MsgBox("Loading data completed.", MsgBoxStyle.Information, "")
                '/End If
            End If
        End If
    End Sub

    Private Sub bworkcount_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)

    End Sub

    Private Sub grdpallets_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpallets.CellContentClick
        Try
            If grdpallets.CurrentCell.ColumnIndex = 1 Then
                Dim checkselect As DataGridViewCheckBoxCell = CType(grdpallets.Rows(grdpallets.CurrentRow.Index).Cells(1), DataGridViewCheckBoxCell)
                If grdpallets.Rows(e.RowIndex).Cells(11).Value.ToString <> orderfill.lblorf.Text & orderfill.txtorf.Text Then
                    Button1.PerformClick()

                    'check sa db wag sa grd lng
                    sql = "Select t.qadispo, t.cusreserve, t.customer, t.ofnum from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                    sql = sql & " where t.palletnum='" & grdpallets.Rows(e.RowIndex).Cells(5).Value & "' and t.cusreserve<>'0' and s.branch='" & login.branch & "' and t.logitemid=(Select logitemid from tbllogitem as i where i.logitemid=t.logitemid and itemname='" & orderfill.cmbitem.Text & "')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        'check if may dispo na ni QA
                        If dr("qadispo") <> 1 Then
                            'cannot proceed
                            If dr("qadispo") = 2 Then
                                MsgBox("Pallet tag # " & grdpallets.Rows(e.RowIndex).Cells(5).Value & " is in HOLD status.", MsgBoxStyle.Exclamation, "")
                                checkselect.Value = False
                            Else
                                MsgBox("Pallet tag # " & grdpallets.Rows(e.RowIndex).Cells(5).Value & " is still PENDING for QA disposition.", MsgBoxStyle.Exclamation, "")
                                checkselect.Value = False
                            End If

                        Else
                            If dr("cusreserve") = 1 Then
                                grdpallets.Rows(e.RowIndex).Cells(10).Value = "Reserved"
                                If dr("customer") = orderfill.txtcus.Text Then
                                    grdpallets.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Yellow
                                Else
                                    grdpallets.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                                    MsgBox("Cannot pick reserved pallet tag # " & grdpallets.Rows(e.RowIndex).Cells(5).Value & ".", MsgBoxStyle.Exclamation, "")
                                    checkselect.Value = False
                                End If
                            ElseIf dr("cusreserve") = 2 Then
                                grdpallets.Rows(e.RowIndex).Cells(10).Value = "Selected"
                                grdpallets.Rows(e.RowIndex).Cells(11).Value = dr("ofnum")
                                grdpallets.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.NavajoWhite
                                MsgBox("Cannot pick selected pallet tag # " & grdpallets.Rows(e.RowIndex).Cells(5).Value & ".", MsgBoxStyle.Exclamation, "")
                                checkselect.Value = False
                            End If
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            End If

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub grdpallets_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdpallets.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then

                grdpallets.ClearSelection()
                grdpallets.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True

                selectedrow = e.RowIndex
                Me.ContextMenuStrip1.Show(Cursor.Position)

            End If

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub grdpallets_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpallets.CellValueChanged
        Try
            If grdpallets.CurrentCell.ColumnIndex = 1 Then
                Dim checkselect As DataGridViewCheckBoxCell = CType(grdpallets.Rows(grdpallets.CurrentRow.Index).Cells(1), DataGridViewCheckBoxCell)
                Button1.PerformClick()

                grdpallets.Invalidate()
            End If

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MsgBox(ex.tostring, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        Try
            If grdpallets.Rows.Count <> 0 Then
                'check if orderfill is not cancelled
                sql = "Select status from tblorderfill where ofid='" & orderfill.lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if ofitem is not cancelled
                sql = "Select status from tblofitem where ofitemid='" & orderfill.txtofitemid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Item is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Item is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if may nauna ng nag reserve or selected

                For Each rowlist As DataGridViewRow In grdpallets.Rows
                    Dim alrselected As String = grdpallets.Rows(rowlist.Index).Cells("cusreserve").Value
                    Dim checkselect As DataGridViewCheckBoxCell = CType(grdpallets.Rows(rowlist.Index).Cells(1), DataGridViewCheckBoxCell)
                    If checkselect.Value = True Or alrselected = "Selected" Then
                        '/MsgBox("check if may nauna ng nag reserve or selected")
                        sql = "Select t.* from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                        sql = sql & " where t.palletnum='" & grdpallets.Rows(rowlist.Index).Cells(5).Value & "' and t.cusreserve<>'0' and s.branch='" & login.branch & "' and t.logitemid=(Select logitemid from tbllogitem as i where i.logitemid=t.logitemid and itemname='" & orderfill.cmbitem.Text & "')"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            If dr("cusreserve") = 1 Then
                                grdpallets.Rows(rowlist.Index).Cells(10).Value = "Reserved"
                                If dr("customer") = orderfill.txtcus.Text And dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.Yellow
                                    checkselect.Value = True
                                    grdpallets.Rows(rowlist.Index).Cells(1).ReadOnly = True
                                ElseIf dr("customer") <> orderfill.txtcus.Text Then
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                                    MsgBox("Cannot select reserved pallets.", MsgBoxStyle.Exclamation, "")
                                    checkselect.Value = False
                                End If

                            ElseIf dr("cusreserve") = 2 Then
                                grdpallets.Rows(rowlist.Index).Cells(10).Value = "Selected"
                                grdpallets.Rows(rowlist.Index).Cells(11).Value = dr("ofnum")
                                If dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.Yellow
                                    checkselect.Value = True
                                    'grdpallets.Rows(rowlist.Index).Cells(1).ReadOnly = True
                                    'check kung nasa grdselected na kung wala i-add
                                Else
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.NavajoWhite
                                    MsgBox("Cannot select already selected pallets.", MsgBoxStyle.Exclamation, "")
                                    checkselect.Value = False
                                End If
                            End If
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()
                    End If
                Next


                '/MsgBox("dapat hindi na rereset.. idadagdag lang yung iadd")
                '/grdselect.Rows.Clear()
                Dim totalbags As Integer = 0
                'verified kaya ililipat nlng ng grd

                For Each rowlist As DataGridViewRow In grdpallets.Rows
                    Dim checkselect As DataGridViewCheckBoxCell = CType(grdpallets.Rows(rowlist.Index).Cells(1), DataGridViewCheckBoxCell)
                    If checkselect.Value = True Then
                        Dim meronna As Boolean = False
                        'check if nasa grd na yung palletnum
                        For Each rowpal As DataGridViewRow In grdselect.Rows
                            If grdselect.Rows(rowpal.Index).Cells(3).Value = grdpallets.Rows(rowlist.Index).Cells(5).Value Then
                                meronna = True
                                Exit For
                            End If
                        Next

                        If meronna = False Then
                            Dim col0 As Integer = grdpallets.Rows(rowlist.Index).Cells(0).Value
                            Dim col1 As Date = Format(CDate(grdpallets.Rows(rowlist.Index).Cells(2).Value), "yyyy/MM/dd")
                            Dim col2 As String = grdpallets.Rows(rowlist.Index).Cells(4).Value
                            Dim col3 As String = grdpallets.Rows(rowlist.Index).Cells(5).Value
                            Dim col4 As Integer = grdpallets.Rows(rowlist.Index).Cells(6).Value
                            Dim col5 As String = grdpallets.Rows(rowlist.Index).Cells(7).Value
                            '/Dim sticket As String = grdpallets.Rows(rowlist.Index).Cells(8).Value
                            '/Dim col6 As Integer = Val(sticket.Remove(0, 2))
                            Dim col6 As String = grdpallets.Rows(rowlist.Index).Cells(8).Value
                            Dim col7 As String = grdpallets.Rows(rowlist.Index).Cells(9).Value
                            Dim collogticketid As Integer = grdpallets.Rows(rowlist.Index).Cells(0).Value

                            'update cusreserve
                            If grdpallets.Rows(rowlist.Index).Cells(10).Value = "Reserved" Then
                                'sql = "Update tbllogticket set ofid='" & orderfill.lblorfid.Text & "', ofnum='" & orderfill.lblorf.Text & orderfill.txtorf.Text & "' where palletnum='" & col3 & "' and branch='" & login.branch & "'"
                                sql = "Update tbllogticket set ofid='" & orderfill.lblorfid.Text & "', ofnum='" & orderfill.lblorf.Text & orderfill.txtorf.Text & "'"
                                sql = sql & " where logticketid=(Select t.logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid where t.palletnum='" & col3 & "' and s.branch='" & login.branch & "' and t.logitemid=(Select logitemid from tbllogitem as i where i.logitemid=t.logitemid and itemname='" & orderfill.cmbitem.Text & "'))"
                            Else
                                'selected only
                                sql = "Update tbllogticket set ofid='" & orderfill.lblorfid.Text & "', cusreserve='2', ofnum='" & orderfill.lblorf.Text & orderfill.txtorf.Text & "'"
                                sql = sql & " where logticketid=(Select t.logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid where t.palletnum='" & col3 & "' and s.branch='" & login.branch & "' and t.logitemid=(Select logitemid from tbllogitem as i where i.logitemid=t.logitemid and itemname='" & orderfill.cmbitem.Text & "'))"
                            End If

                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            'i add sa grdselect
                            grdselect.Rows.Add(col0, col1, col2, col3, col4, col5, col6, col7, col4, collogticketid)

                            totalbags = totalbags + col4
                        End If
                    End If
                Next


                lbltotal.Text = totalbags

            End If

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Try
            If grdselect.Rows.Count <> 0 Then
                'check if orderfill is not cancelled
                sql = "Select * from tblorderfill where ofid='" & orderfill.lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if ofitem is not cancelled
                sql = "Select * from tblofitem where ofitemid='" & orderfill.txtofitemid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Item is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Item is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                Dim a As String
                If orderfill.grdlog.Rows.Count <> 0 Then
                    a = MsgBox("Are you sure you want to remove pallets? Removing pallets will reset your generated list.", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                Else
                    a = MsgBox("Are you sure you want to remove pallets?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                End If

                If a <> vbYes Then
                    Exit Sub
                End If

                'remove from grdselect
                Dim totalbags As Integer = Val(lbltotal.Text)
                For Each row As DataGridViewRow In grdselect.SelectedRows
                    Dim col3 As String = grdselect.Rows(row.Index).Cells(3).Value
                    Dim col4 As String = grdselect.Rows(row.Index).Cells(4).Value

                    'remove in grdtickets
                    For Each rowlog As DataGridViewRow In orderfill.grdtickets.SelectedRows
                        If orderfill.grdtickets.Rows(row.Index).Cells(3).Value = grdselect.Rows(row.Index).Cells(3).Value Then
                            Dim grdindex As Integer = row.Index
                            orderfill.grdtickets.Rows.Remove(row)
                            Exit For
                        End If
                    Next

                    'update cusreserve selected into not selected
                    'sql = "Update tbllogticket set cusreserve='0', ofnum=NULL where palletnum='" & col3 & "' and cusreserve='2' and branch='" & login.branch & "'"
                    sql = "Update tbllogticket set cusreserve='0', ofnum=NULL "
                    sql = sql & " where logticketid=(Select t.logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid "
                    sql = sql & " where t.palletnum='" & col3 & "' and t.cusreserve='2' and s.branch='" & login.branch & "' and t.logitemid=(Select logitemid from tbllogitem as i where i.logitemid=t.logitemid and itemname='" & orderfill.cmbitem.Text & "'))"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    'update cusreserve reserved into not selected
                    'sql = "Update tbllogticket set ofnum=NULL where palletnum='" & col3 & "' and cusreserve='1' and branch='" & login.branch & "'"
                    sql = "Update tbllogticket set ofnum=NULL where logticketid=(Select t.logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                    sql = sql & " where t.palletnum='" & col3 & "' and t.cusreserve='1' and s.branch='" & login.branch & "' and t.logitemid=(Select logitemid from tbllogitem as i where i.logitemid=t.logitemid and itemname='" & orderfill.cmbitem.Text & "'))"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    'check kung meron dun sa temp nya para maremove din

                    totalbags = totalbags - col4

                    Dim listindex As Integer = row.Index
                    grdselect.Rows.Remove(row)
                Next


                lbltotal.Text = totalbags
                '/btnview.PerformClick()
            End If

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub checkcusreserve()
        Try
            If grdpallets.Rows.Count <> 0 Then
                'check if may nauna ng nag reserve or selected
                For Each rowlist As DataGridViewRow In grdpallets.Rows
                    Dim checkselect As DataGridViewCheckBoxCell = CType(grdpallets.Rows(rowlist.Index).Cells(1), DataGridViewCheckBoxCell)
                    If checkselect.Value = True Then
                        '/MsgBox("check if may nauna ng nag reserve or selected")
                        sql = "Select * from tbllogticket where logticketid=(Select t.logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid "
                        sql = sql & " where t.palletnum='" & grdpallets.Rows(rowlist.Index).Cells(5).Value & "' and t.cusreserve<>'0' and s.branch='" & login.branch & "' and t.logitemid=(Select logitemid from tbllogitem as i where i.logitemid=t.logitemid and itemname='" & orderfill.cmbitem.Text & "'))"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            If dr("cusreserve") = 1 Then
                                grdpallets.Rows(rowlist.Index).Cells(10).Value = "Reserved"
                                If dr("customer").ToString = orderfill.txtcus.Text And dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.Yellow
                                    checkselect.Value = True
                                    grdpallets.Rows(rowlist.Index).Cells(1).ReadOnly = True
                                ElseIf dr("customer").ToString = orderfill.txtcus.Text Then
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.Yellow
                                    checkselect.Value = True
                                    grdpallets.Rows(rowlist.Index).Cells(1).ReadOnly = True
                                Else
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                                    MsgBox("Cannot select reserved pallets.", MsgBoxStyle.Exclamation, "")
                                    checkselect.Value = False
                                End If
                            ElseIf dr("cusreserve") = 2 Then
                                grdpallets.Rows(rowlist.Index).Cells(10).Value = "Selected"
                                grdpallets.Rows(rowlist.Index).Cells(11).Value = dr("ofnum")
                                If dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.Yellow
                                    checkselect.Value = True
                                    grdpallets.Rows(rowlist.Index).Cells(1).ReadOnly = True
                                Else
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.NavajoWhite
                                    MsgBox("Cannot select already selected pallets.", MsgBoxStyle.Exclamation, "")
                                    checkselect.Value = False
                                End If
                            End If
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()
                    End If
                Next
            End If

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            '/MsgBox("check ulet ung status ng cusreserve nya tapos dito plng dapat i update tbl cusreserve nya")
            checkcusreserve()

            orderfill.grdlog.Rows.Clear()

            For Each row As DataGridViewRow In grdselect.Rows
                Dim colid As Integer = grdselect.Rows(row.Index).Cells(0).Value
                Dim coldate As Date = Format(CDate(grdselect.Rows(row.Index).Cells(1).Value), "yyyy/MM/dd")
                Dim collognum As String = grdselect.Rows(row.Index).Cells(2).Value
                Dim colpallet As String = grdselect.Rows(row.Index).Cells(3).Value
                Dim colavail As Integer = grdselect.Rows(row.Index).Cells(4).Value
                Dim colletter As String = grdselect.Rows(row.Index).Cells(5).Value
                Dim colstart As String = grdselect.Rows(row.Index).Cells(6).Value
                Dim colend As String = grdselect.Rows(row.Index).Cells(7).Value
                Dim colselect As Integer = grdselect.Rows(row.Index).Cells(8).Value
                Dim collogticketid As Integer = grdselect.Rows(row.Index).Cells(9).Value
                Dim colpicktickets As Boolean = grdselect.Rows(row.Index).Cells(10).Value


                'update cusreserve available to selected
                sql = "Update tbllogticket set cusreserve='2', ofnum='" & orderfill.lblorf.Text & orderfill.txtorf.Text & "' "
                sql = sql & " where logticketid=(Select t.logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid "
                sql = sql & " where t.palletnum='" & colpallet & "' and t.cusreserve='0' and s.branch='" & login.branch & "' and t.logitemid=(Select logitemid from tbllogitem as i where i.logitemid=t.logitemid and itemname='" & orderfill.cmbitem.Text & "'))"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()

                'update cusreserve reserve to selected ofnum only
                'sql = "Update tbllogticket set ofnum='" & orderfill.lblorf.Text & orderfill.txtorf.Text & "' where palletnum='" & colpallet & "' and cusreserve='1' and branch='" & login.branch & "'"
                sql = "Update tbllogticket set ofnum='" & orderfill.lblorf.Text & orderfill.txtorf.Text & "'"
                sql = sql & " where logticketid=(Select t.logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                sql = sql & " where t.palletnum='" & colpallet & "' and t.cusreserve='1' and s.branch='" & login.branch & "' and t.logitemid=(Select logitemid from tbllogitem as i where i.logitemid=t.logitemid and itemname='" & orderfill.cmbitem.Text & "'))"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()

                orderfill.grdlog.Rows.Add(colid, Format(coldate, "yyyy/MM/dd"), collognum, colpallet, colletter, colstart, colend, colavail, 0, colavail, colselect, "", collogticketid, colpicktickets)
            Next


            For r As Integer = orderfill.grdcancel.Rows.Count - 1 To 0 Step -1
                Dim pallettagnum As String = orderfill.grdcancel.Rows(r).Cells(1).Value
                Dim notingrdlog As Boolean = True
                For Each row As DataGridViewRow In orderfill.grdlog.Rows
                    Dim pall As String = orderfill.grdlog.Rows(row.Index).Cells(3).Value
                    If pallettagnum = pall Then
                        notingrdlog = False
                        Exit For
                    End If
                Next
                If notingrdlog Then orderfill.grdcancel.Rows.RemoveAt(r)
            Next

            Me.Close()

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub grdselect_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdselect.CellContentClick

    End Sub

    Private Sub grdselect_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdselect.CellEndEdit
        'check if numeric
        If (e.ColumnIndex = 8) Then   ' Checking numeric value for Column1 only
            If grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> Nothing Then
                Dim value As String = grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
                If Not Information.IsNumeric(value) Or Val(value) < 1 Or Val(value) > grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex - 4).Value Then
                    MsgBox("Invalid input.", MsgBoxStyle.Exclamation, "")
                    grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex - 4).Value
                    grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
                Else
                    grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Fix(grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                    grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
                End If

                Dim totalbags As Integer = 0
                For Each row As DataGridViewRow In grdselect.Rows
                    totalbags = totalbags + grdselect.Item(8, row.Index).Value
                Next

                lbltotal.Text = totalbags.ToString("n2")
            End If
        End If
    End Sub

    Private Sub txtpallet_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpallet.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            searchpallet()
        End If
    End Sub

    Private Sub txtpallet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpallet.TextChanged
        If Trim(txtpallet.Text) = "" Then
            txtticket.Enabled = True
            txtletter.Enabled = True
            '/btnview.PerformClick()
        Else
            txtticket.Text = ""
            txtletter.Text = ""
            txtticket.Enabled = False
            txtletter.Enabled = False
        End If
    End Sub

    Private Sub cmbloc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbloc.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/btnadd.PerformClick()
        End If
    End Sub

    Private Sub cmbloc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbloc.SelectedIndexChanged

    End Sub

    Public Sub searchpallet()
        Try
            gpallets.Text = "Searched Pallets"
            cmbtemp.Items.Clear()
            grdpallets.Rows.Clear()

            sql = "Select DISTINCT palletnum, qadispo, itemstatus, ticketstatus from vofpickpallets"
            sql = sql & " where palletnum='" & Trim(txtpallet.Text) & "' and itemname='" & orderfill.cmbitem.Text & "' and branch='" & login.branch & "'"

            If Trim(cmbloc.Text) <> "" Then
                sql = sql & " and location='" & Trim(cmbloc.Text) & "'"
            End If
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                'and ='1'
                If dr("itemstatus") <> 2 Then
                    MsgBox("Pallet Tag# " & Trim(txtpallet.Text) & " is not yet completed.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub
                End If

                If dr("ticketstatus") = 0 Then
                    MsgBox("Pallet Tag# " & Trim(txtpallet.Text) & " is already empty.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub
                ElseIf dr("ticketstatus") = 3 Then
                    MsgBox("Pallet Tag# " & Trim(txtpallet.Text) & " is already cancelled.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub
                End If

                If dr("qadispo") = 2 Then
                    MsgBox("Pallet Tag# " & Trim(txtpallet.Text) & " is in HOLD status.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub

                ElseIf (dr("qadispo") = 0 Or dr("qadispo") = 3) Then
                    MsgBox("Pallet Tag# " & Trim(txtpallet.Text) & " is still pending for QA disposition.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub
                End If
            Else
                MsgBox("Cannot found Pallet Tag# " & Trim(txtpallet.Text) & ".", MsgBoxStyle.Critical, "")
                txtpallet.Focus()
                txtpallet.SelectAll()
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.WaitCursor
            lblloading.Location = New Point(3, 9)
            lblloading.Visible = True
            pgb1.Style = ProgressBarStyle.Marquee
            pgb1.Visible = True
            pgb1.Value = 0

            sql = "Select *"
            sql = sql & " from vofpickpallets where branch='" & login.branch & "'"
            sql = sql & " and qadispo='1' and itemname='" & orderfill.cmbitem.Text & "' and ticketstatus='1' and itemstatus='2' and palletnum='" & Trim(txtpallet.Text) & "'"

            If Trim(cmbloc.Text) <> "" Then
                sql = sql & " and location='" & Trim(cmbloc.Text) & "'"
            End If

            gridsql = sql

            grdpallets.Rows.Clear()

            bwork = New BackgroundWorker()
            bwork.WorkerSupportsCancellation = True
            bworkcount = New BackgroundWorker()
            bworkcount.WorkerSupportsCancellation = True

            AddHandler bwork.DoWork, New DoWorkEventHandler(AddressOf bwork_DoWork)
            AddHandler bwork.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bwork_Completed)
            AddHandler bwork.ProgressChanged, New ProgressChangedEventHandler(AddressOf bwork_ProgressChanged)
            m_addRowDelegate = New AddRowDelegate(AddressOf AddDGVRow)

            AddHandler bworkcount.DoWork, New DoWorkEventHandler(AddressOf bworkcount_DoWork)
            AddHandler bworkcount.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bworkcount_Completed)
            AddHandler bworkcount.ProgressChanged, New ProgressChangedEventHandler(AddressOf bworkcount_ProgressChanged)

            If Not bwork.IsBusy Then
                bwork.WorkerReportsProgress = True
                bwork.WorkerSupportsCancellation = True
                bwork.RunWorkerAsync() 'start ng select query
            End If

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub txtticket_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtticket.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            searchticket()
        End If
    End Sub

    Private Sub txtticket_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtticket.TextChanged
        If Trim(txtticket.Text) = "" Then
            txtpallet.Enabled = True
            '/btnview.PerformClick()
        Else
            txtpallet.Text = ""
            txtpallet.Enabled = False
        End If
    End Sub

    Public Sub searchticket()
        Try
            If Trim(txtletter.Text) = "" Or Trim(txtticket.Text) = "" Then
                MsgBox("Complete the required fields to search ticket#.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            gpallets.Text = "Searched Pallets"
            cmbtemp.Items.Clear()
            grdpallets.Rows.Clear()
            Dim palnum As String = ""

            sql = "Select s.whsename, t.qadispo, t.palletnum, i.itemname, t.status as ticketstatus, t.location, i.status as itemstatus from tbllogitem i"
            sql = sql & " Right outer join tbllogsheet s on s.logsheetid=i.logsheetid"
            sql = sql & " Right outer join tbllogticket t on i.logitemid=t.logitemid"
            sql = sql & " Right outer join tblloggood g on t.logticketid=g.logticketid"
            sql = sql & " where (g.letter='" & Trim(txtletter.Text) & "' and g.gticketnum='" & Trim(txtticket.Text) & "' and g.status<>'3')"
            sql = sql & " And i.itemname='" & orderfill.cmbitem.Text & "' and i.status='2' and t.status<>'3' and s.branch='" & login.branch & "'"
            If Trim(cmbloc.Text) <> "" Then
                sql = sql & " and t.location='" & Trim(cmbloc.Text) & "'"
            End If

            sql = sql & " UNION "

            sql = sql & " Select s.whsename, t.qadispo, t.palletnum, i.itemname, t.status as ticketstatus, t.location, i.status as itemstatus from tbllogitem i"
            sql = sql & " Right outer join tbllogsheet s on s.logsheetid=i.logsheetid"
            sql = sql & " Right outer join tbllogticket t on i.logitemid=t.logitemid"
            sql = sql & " Right outer join tbllogdouble d on t.logticketid=d.logticketid"
            sql = sql & " where (d.letter='" & Trim(txtletter.Text) & "' and d.dticketnum='" & Trim(txtticket.Text) & "' and d.status<>'3')"
            sql = sql & " And i.itemname='" & orderfill.cmbitem.Text & "' and i.status='2' and t.status<>'3' and s.branch='" & login.branch & "'"
            If Trim(cmbloc.Text) <> "" Then
                sql = sql & " and t.location='" & Trim(cmbloc.Text) & "'"
            End If


            connect()
            cmd = New SqlCommand(sql, conn)
            cmd.CommandTimeout = 0
            dr = cmd.ExecuteReader
            If dr.Read Then
                'and ='1'
                palnum = dr("palletnum")
                If dr("itemstatus") <> 2 Then
                    MsgBox("Pallet Tag# " & palnum & " is not yet completed.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub
                End If

                If dr("ticketstatus") = 0 Then
                    MsgBox("Pallet Tag# " & palnum & " is already empty.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub
                ElseIf dr("ticketstatus") = 3 Then
                    MsgBox("Pallet Tag# " & palnum & " is already cancelled.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub
                End If

                If dr("qadispo") = 2 Then
                    MsgBox("Pallet Tag# " & palnum & " is in HOLD status.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub

                ElseIf (dr("qadispo") = 0 Or dr("qadispo") = 3) Then
                    MsgBox("Pallet Tag# " & palnum & " is still pending for QA disposition.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub
                End If
            Else
                MsgBox("Cannot found Pallet Tag# " & palnum & " for Ticket# " & Trim(txtletter.Text) & " " & Trim(txtticket.Text) & ".", MsgBoxStyle.Critical, "")
                txtpallet.Focus()
                txtpallet.SelectAll()
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.WaitCursor
            lblloading.Location = New Point(3, 9)
            lblloading.Visible = True
            pgb1.Style = ProgressBarStyle.Marquee
            pgb1.Visible = True
            pgb1.Value = 0

            sql = "Select *"
            sql = sql & " from vofpickpallets where branch='" & login.branch & "'"
            sql = sql & " and qadispo='1' and itemname='" & orderfill.cmbitem.Text & "' and ticketstatus='1' and itemstatus='2' and palletnum='" & palnum & "'"

            If Trim(cmbloc.Text) <> "" Then
                sql = sql & " and location='" & Trim(cmbloc.Text) & "'"
            End If

            gridsql = sql

            grdpallets.Rows.Clear()

            bwork = New BackgroundWorker()
            bwork.WorkerSupportsCancellation = True
            bworkstat = New BackgroundWorker()
            bworkstat.WorkerSupportsCancellation = True
            bworkcount = New BackgroundWorker()
            bworkcount.WorkerSupportsCancellation = True

            AddHandler bwork.DoWork, New DoWorkEventHandler(AddressOf bwork_DoWork)
            AddHandler bwork.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bwork_Completed)
            AddHandler bwork.ProgressChanged, New ProgressChangedEventHandler(AddressOf bwork_ProgressChanged)
            m_addRowDelegate = New AddRowDelegate(AddressOf AddDGVRow)

            AddHandler bworkcount.DoWork, New DoWorkEventHandler(AddressOf bworkcount_DoWork)
            AddHandler bworkcount.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bworkcount_Completed)
            AddHandler bworkcount.ProgressChanged, New ProgressChangedEventHandler(AddressOf bworkcount_ProgressChanged)

            If Not bwork.IsBusy Then
                bwork.WorkerReportsProgress = True
                bwork.WorkerSupportsCancellation = True
                bwork.RunWorkerAsync() 'start ng select query
            End If

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub txtletter_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtletter.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/btnadd.PerformClick()
        ElseIf (Asc(e.KeyChar) >= 65 And Asc(e.KeyChar) <= 90) Or (Asc(e.KeyChar) >= 97 And Asc(e.KeyChar) <= 122) Or Asc(e.KeyChar) = 8 Then
            If txtletter.Text.ToString.Length = 1 And Not Asc(e.KeyChar) = 8 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtletter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtletter.TextChanged
        If Trim(txtletter.Text) = "" Then
            txtpallet.Enabled = True
            '/btnview.PerformClick()
        Else
            txtpallet.Text = ""
            txtpallet.Enabled = False
        End If
    End Sub

    Private Sub ViewPalletToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewPalletToolStripMenuItem.Click
        'print pallet tag
        Dim logticketid As Integer = 0
        sql = "Select t.logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
        sql = sql & " where t.palletnum='" & grdpallets.Rows(selectedrow).Cells(5).Value & "' and t.status<>'3' and s.branch='" & login.branch & "' and t.logitemid=(Select logitemid from tbllogitem as i where i.logitemid=t.logitemid and itemname='" & orderfill.cmbitem.Text & "')"
        connect()
        cmd = New SqlCommand(sql, conn)
        dr = cmd.ExecuteReader
        If dr.Read Then
            logticketid = dr("logticketid")
        End If
        dr.Dispose()
        cmd.Dispose()
        conn.Close()

        rptpallettag.logticket = logticketid
        rptpallettag.ShowDialog()
    End Sub

    Private Sub btnfirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfirst.Click
        Try
            gpallets.Text = "View First"
            clickbtn = "View First"

            txtpallet.Text = ""
            txtletter.Text = ""
            txtticket.Text = ""
            cmbtemp.Items.Clear()
            grdpallets.Rows.Clear()

            Dim logsheetnum As String = ""

            sql = "Select Top 1 logsheetnum"
            sql = sql & " from vofpickpallets where branch='" & login.branch & "' and qadispo='1'"
            sql = sql & " and itemname='" & orderfill.cmbitem.Text & "' and ticketstatus='1' and itemstatus='2' and avtickets>0"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                logsheetnum = dr("logsheetnum")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If logsheetnum = "" Then
                MsgBox("No record found.", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            lblloading.Location = New Point(3, 9)
            lblloading.Visible = True
            pgb1.Style = ProgressBarStyle.Marquee
            pgb1.Visible = True
            pgb1.Value = 0

            sql = "Select *"
            sql = sql & " from vofpickpallets where branch='" & login.branch & "' and qadispo='1'"
            sql = sql & " and itemname='" & orderfill.cmbitem.Text & "' and ticketstatus='1' and itemstatus='2' and logsheetnum='" & logsheetnum & "' and avtickets>0"
            sql = sql & " order by logticketid"
            gridsql = sql

            grdpallets.Rows.Clear()

            bwork = New BackgroundWorker()
            bwork.WorkerSupportsCancellation = True
            bworkstat = New BackgroundWorker()
            bworkstat.WorkerSupportsCancellation = True
            bworkcount = New BackgroundWorker()
            bworkcount.WorkerSupportsCancellation = True

            AddHandler bwork.DoWork, New DoWorkEventHandler(AddressOf bwork_DoWork)
            AddHandler bwork.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bwork_Completed)
            AddHandler bwork.ProgressChanged, New ProgressChangedEventHandler(AddressOf bwork_ProgressChanged)
            m_addRowDelegate = New AddRowDelegate(AddressOf AddDGVRow)

            AddHandler bworkcount.DoWork, New DoWorkEventHandler(AddressOf bworkcount_DoWork)
            AddHandler bworkcount.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bworkcount_Completed)
            AddHandler bworkcount.ProgressChanged, New ProgressChangedEventHandler(AddressOf bworkcount_ProgressChanged)

            If Not bwork.IsBusy Then
                bwork.WorkerReportsProgress = True
                bwork.WorkerSupportsCancellation = True
                bwork.RunWorkerAsync() 'start ng select query
            End If

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub grdpallets_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles grdpallets.RowPrePaint
        If e.RowIndex > -1 Then
            Dim dgvRow As DataGridViewRow = grdpallets.Rows(e.RowIndex)
            '<== But this is the name assigned to it in the properties of the control
            If dgvRow.Cells(0).Tag = "Yellow" Then
                dgvRow.DefaultCellStyle.BackColor = Color.Yellow
            ElseIf dgvRow.Cells(0).Tag = "NavajoWhite" Then
                dgvRow.DefaultCellStyle.BackColor = Color.NavajoWhite
            ElseIf dgvRow.Cells(0).Tag = "DeepSkyBlue" Then
                dgvRow.DefaultCellStyle.BackColor = Color.DeepSkyBlue
            End If
        End If
    End Sub
End Class