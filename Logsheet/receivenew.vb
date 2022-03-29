Imports System.IO
Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class receivenew
    Dim lines1 = System.IO.File.ReadAllLines("connectionstring.txt")  'Agi-Calamba
    Dim strconn1 As String = lines1(0) 'Agi-Calamba   
    Dim lines2 = System.IO.File.ReadAllLines("cnstringcalaca.txt") 'Agi-Calaca
    Dim strconn2 As String = lines2(0) 'Agi-Calaca

    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String, svrcal As Boolean, svrclc As Boolean

    Public reccnf As Boolean = False
    Private threadEnabled As Boolean, threadstring As String
    Private bwork As BackgroundWorker, bworkstat As BackgroundWorker, bworkofn As BackgroundWorker

    Dim table = New DataTable()

    Private Sub connect()
        conn = New SqlConnection 'New OleDb.OleDbConnection
        If cmbbr.SelectedItem = "AGI-CALAMBA" Then
            conn.ConnectionString = strconn1
            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If
        ElseIf cmbbr.SelectedItem = "AGI-CALACA" Then
            conn.ConnectionString = strconn2
            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If
        End If
    End Sub

    Private Sub disconnect()
        conn = New SqlConnection 'New OleDb.OleDbConnection
        If cmbbr.SelectedItem = "AGI-CALAMBA" Then
            conn.ConnectionString = strconn1
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        ElseIf cmbbr.SelectedItem = "AGI-CALACA" Then
            conn.ConnectionString = strconn2
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End If
    End Sub

    Private Sub receivenew_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub receive_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        login.viewbranch()
        daterec.Value = login.datenow
        daterec.MaxDate = login.datenow
    End Sub

    Dim linescalamba = System.IO.File.ReadAllLines("connectionstring.txt")
    Dim connStrcal As String = linescalamba(0)

    Dim linescalaca = System.IO.File.ReadAllLines("cnstringcalaca.txt")
    Dim connStrclc As String = linescalaca(0)

    Private Function checkconnection(ByVal str As String, ByVal whse As String) As Boolean
        Dim chkstr As Boolean = False
        Try
            Static conn As SqlConnection
            'If conn Is Nothing Then
            conn = New SqlConnection(str)
            conn.Open()
            chkstr = True
            conn.Close()
            'End If
        Catch ex As Exception
            Return chkstr
        End Try
        Return chkstr
    End Function

    Private Sub txtofnum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If Trim(txtof.Text) = "" And Trim(txtwrs.Text) = "" Then
                MsgBox("Input order fill # or WRS#.", MsgBoxStyle.Exclamation, "")

            ElseIf Trim(txtof.Text) <> "" Then
                Dim meron As Boolean = False

                For Each row As DataGridViewRow In grdof.Rows
                    Dim orf As String = grdof.Rows(row.Index).Cells(2).Value.ToString
                    If orf = lblof.Text & txtof.Text Then
                        meron = True
                        Exit For
                    End If
                Next

                If meron = True Then
                    MsgBox("Order fill# " & lblof.Text & txtof.Text & " is already added.", MsgBoxStyle.Exclamation, "")
                Else
                    sql = "Select ofid, wrsnum, ofnum, platenum, driver from tblorderfill where ofnum='" & lblof.Text & txtof.Text & "' and status='2' and customer='" & login.branch.ToString.ToUpper & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        grdof.Rows.Add(dr("ofid"), dr("wrsnum"), dr("ofnum"), dr("platenum"), dr("driver"))
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If

            ElseIf Trim(txtwrs.Text) <> "" Then
                sql = "Select ofid, wrsnum, ofnum, platenum, driver from tblorderfill where wrsnum='" & txtwrs.Text & "' and status='2' and customer='" & login.branch.ToString.ToUpper & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    Dim meron As Boolean = False

                    For Each row As DataGridViewRow In grdof.Rows
                        Dim orf As String = grdof.Rows(row.Index).Cells(2).Value
                        If orf = dr("ofnum").ToString Then
                            meron = True
                            Exit For
                        End If
                    Next

                    If meron = True Then
                        MsgBox("Order fill# " & dr("ofnum") & " is already added.", MsgBoxStyle.Exclamation, "")
                    Else
                        grdof.Rows.Add(dr("ofid"), dr("wrsnum"), dr("ofnum"), dr("platenum"), dr("driver"))
                    End If
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            End If

            txtof.Text = ""
            txtwrs.Text = ""

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

    Private Sub ExecuteLoadTickets(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                '/command.CommandText = sql
                '/command.ExecuteNonQuery()
                Dim table = New DataTable()
                table.Columns.Add("Order Fill", GetType(String))
                table.Columns.Add("Date", GetType(Date))
                table.Columns.Add("Logsheet", GetType(String))
                table.Columns.Add("Itemname", GetType(String))
                table.Columns.Add("LetterNum", GetType(String))
                table.Columns.Add("Number", GetType(Integer))
                table.Columns.Add("Letter", GetType(Char))

                For Each row As DataGridViewRow In grdof.Rows
                    Dim ofid As String = grdof.Rows(row.Index).Cells(0).Value

                    sql = "Select tblofloggood.ofid, tblofitem.itemname, tbloflog.logsheetdate, tblofloggood.letter, tblofloggood.gticketnum from tblofloggood"
                    sql = sql & " right outer join tbloflog on tblofloggood.oflogid=tbloflog.oflogid"
                    sql = sql & " right outer join tblofitem on tblofloggood.ofitemid=tblofitem.ofitemid"
                    sql = sql & " where tblofloggood.ofid='" & ofid & "' and tblofloggood.status='1'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    While dr.Read
                        '/grd.Rows.Add(dr("ofid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("itemname"), dr("letter"), dr("gticketnum"), Val(dr("gticketnum")))
                        table.Rows.Add(Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("itemname"), dr("gticketnum") & " " & dr("letter"), dr("gticketnum"), dr("letter"))
                    End While
                    dr.Dispose()
                Next

                table.DefaultView.Sort = "Date, Itemname, Letter, Number"

                Dim distinctDT As DataTable = table.DefaultView.ToTable(True, "Date", "Itemname")
                '/CType(table.Rows(0)("Date"), Date).ToShortDateString()
                grdmerge.Columns.Clear()
                grdmerge.DataSource = distinctDT
                grdmerge.Columns.Add("Qty", "Qty")
                For Each row As DataGridViewRow In grdmerge.Rows 'Format(grdmerge.Rows(row.Index).Cells(0).Value, "yyyy/MM/dd") ' 
                    Dim numberOfRecords As String = table.[Select]("Date = '" & (grdmerge.Rows(row.Index).Cells(0).Value) & "' And Itemname = '" & grdmerge.Rows(row.Index).Cells(1).Value & "'").Length
                    grdmerge.Rows(row.Index).Cells(2).Value = numberOfRecords
                Next


                grdlist.Columns.Clear()
                Me.grdlist.DataSource = table

                grdlist.Columns(0).DefaultCellStyle.Format = "yyyy/MM/dd"

                grdmerge.Columns(0).DefaultCellStyle.Format = "yyyy/MM/dd"
                grdmerge.Columns(0).Width = 110
                grdmerge.Columns(1).Width = 300
                grdmerge.Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True


                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.ToString, MsgBoxStyle.Exclamation, "")
                Try
                    Me.Cursor = Cursors.Default
                    transaction.Rollback()
                Catch ex2 As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox("2: " & ex2.ToString & vbCrLf & vbCrLf & "Please try again.", MsgBoxStyle.Information, "")
                End Try
            End Try
        End Using
    End Sub

    Private Sub btngen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngen.Click
        If grdof.Rows.Count <> 0 Then
            'check in recof kung merong wrs at ofnum na ganito status<>3
            For Each row As DataGridViewRow In grdof.Rows
                Dim wrsnum As String = grdof.Rows(row.Index).Cells(1).Value
                Dim ofnum As String = grdof.Rows(row.Index).Cells(2).Value

                sql = "Select status from tblrecof where branch='" & login.branch & "' and status<>'3' and wrsnum='" & wrsnum & "' and ofnum='" & ofnum & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("WRS#" & wrsnum & " - " & ofnum & " is already exist.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            Dim list_ofnum As String = ""
            For Each ofrow As DataGridViewRow In grdof.Rows
                If list_ofnum = "" Then
                    list_ofnum = "tblofitem.ofnum='" & grdof.Rows(ofrow.Index).Cells(2).Value & "'"
                Else
                    list_ofnum = list_ofnum & "or tblofitem.ofnum='" & grdof.Rows(ofrow.Index).Cells(2).Value & "'"
                End If
            Next

            table = New DataTable
            table.Columns.Add("OrderFill", GetType(String))
            table.Columns.Add("Date", GetType(Date))
            table.Columns.Add("Logsheet", GetType(String))
            table.Columns.Add("Itemname", GetType(String))
            table.Columns.Add("LetterNum", GetType(String))
            table.Columns.Add("Number", GetType(Integer))
            table.Columns.Add("Letter", GetType(Char))
            table.Columns.Add("Thread", GetType(String))
            table.Columns.Add("SackType", GetType(String))
            table.Columns.Add("Bin", GetType(String))

            If cmbbr.SelectedItem = "AGI-CALAMBA" Then
                'ExecuteLoadTickets(strconn1)
                threadstring = strconn1
            Else
                'ExecuteLoadTickets(strconn2)
                threadstring = strconn2
            End If

            bwork = New BackgroundWorker()
            bwork.WorkerSupportsCancellation = True

            AddHandler bwork.DoWork, New DoWorkEventHandler(AddressOf bwork_DoWork)
            AddHandler bwork.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bwork_Completed)
            AddHandler bwork.ProgressChanged, New ProgressChangedEventHandler(AddressOf bwork_ProgressChanged)
            m_addRowDelegate = New AddRowDelegate(AddressOf AddDGVRow)

            If Not bwork.IsBusy Then
                bwork.WorkerReportsProgress = True
                bwork.WorkerSupportsCancellation = True
                bwork.RunWorkerAsync() 'start ng select query
            End If

            btnadd.Enabled = False
            btnremove.Enabled = False
            txtof.Enabled = False
            txtwrs.Enabled = False
            btngen.Enabled = False
            btncancel.Enabled = True
        Else
            MsgBox("Input orderfill / wrsnum.", MsgBoxStyle.Exclamation, "")
        End If
    End Sub

    Private Sub bwork_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        threadEnabled = True
        Dim cnt As Integer = 0
        Dim connection As SqlConnection

        For Each row As DataGridViewRow In grdof.Rows
            Dim ofid As String = grdof.Rows(row.Index).Cells(0).Value

            sql = "Select tblofloggood.ofid, tblofitem.itemname, tbloflog.logsheetdate, tblofloggood.letter,"
            sql = sql & " tblofloggood.gticketnum, tblofloggood.logsheetnum, tblofloggood.ofnum,"
            sql = sql & " tbllogsheet.thread, tbllogsheet.bagtype, tbllogsheet.binnum from tblofloggood"
            sql = sql & " inner join tbloflog on tblofloggood.oflogid=tbloflog.oflogid"
            sql = sql & " inner join tblofitem on tblofloggood.ofitemid=tblofitem.ofitemid"
            sql = sql & " inner join tbllogsheet on tblofloggood.logsheetnum=tbllogsheet.logsheetnum"
            sql = sql & " where tblofloggood.ofid='" & ofid & "' and tblofloggood.status='1'"

            connection = New SqlConnection
            connection.ConnectionString = threadstring
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            cmd = New SqlCommand(sql, connection)
            Dim drx As SqlDataReader = cmd.ExecuteReader
            While drx.Read
                AddDGVRow(drx("ofnum"), Format(drx("logsheetdate"), "yyyy/MM/dd"), drx("logsheetnum"), drx("itemname"), drx("gticketnum") & " " & drx("letter"), drx("gticketnum"), drx("letter"), drx("thread"), drx("bagtype"), drx("binnum"))

                cnt += 1
                'bwork.ReportProgress((i / (rcount)) * 100)
            End While
            drx.Dispose()
            cmd.Dispose()
            connection.Close()

            '/bworkstat.ReportProgress((cnt / (rcount)) * 100)
        Next
    End Sub

    Delegate Sub AddRowDelegate(ByVal v0 As Object, ByVal v1 As Object, ByVal v2 As Object, ByVal v3 As Object, ByVal v4 As Object, ByVal v5 As Object, ByVal v6 As Object, ByVal v7 As Object, ByVal v8 As Object, ByVal v9 As Object)
    Private m_addRowDelegate As AddRowDelegate

    Private Sub AddDGVRow(ByVal v0 As Object, ByVal v1 As Object, ByVal v2 As Object, ByVal v3 As Object, ByVal v4 As Object, ByVal v5 As Object, ByVal v6 As Object, ByVal v7 As Object, ByVal v8 As Object, ByVal v9 As Object)
        If threadEnabled = True Then
            If grdof.InvokeRequired Then
                grdof.BeginInvoke(New AddRowDelegate(AddressOf AddDGVRow), v0, v1, v2, v3, v4, v5, v6, v7, v8, v9)
            Else
                table.Rows.Add(v0, v1, v2, v3, v4, v5, v6, v7, v8, v9)
            End If
        End If
    End Sub

    Private Sub bwork_Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Try
            table.DefaultView.Sort = "OrderFill, Date, Logsheet, Itemname, Letter, Number, Thread, SackType, Bin"

            Dim distinctDT As DataTable = table.DefaultView.ToTable(True, "OrderFill", "Date", "Logsheet", "Itemname", "Thread", "SackType", "Bin")
            '/CType(table.Rows(0)("Date"), Date).ToShortDateString()
            grdmerge.Columns.Clear()
            grdmerge.DataSource = distinctDT
            grdmerge.Columns.Add("Qty", "Qty")
            For Each row As DataGridViewRow In grdmerge.Rows 'Format(grdmerge.Rows(row.Index).Cells(0).Value, "yyyy/MM/dd") ' 
                Dim numberOfRecords As String = table.[Select]("OrderFill = '" & (grdmerge.Rows(row.Index).Cells(0).Value) & "' And Logsheet = '" & grdmerge.Rows(row.Index).Cells(2).Value & "'").Length
                grdmerge.Rows(row.Index).Cells(7).Value = numberOfRecords
            Next


            grdlist.Columns.Clear()
            Me.grdlist.DataSource = table

            grdlist.Columns(1).DefaultCellStyle.Format = "yyyy/MM/dd"
            grdmerge.Columns(1).DefaultCellStyle.Format = "yyyy/MM/dd"
            grdmerge.Columns(1).Width = 100

            If grdmerge.Rows.Count <> 0 Then
                grdmerge.Columns.Add("ticket", "Ticket Range")
                grdmerge.Columns(0).Frozen = True
                grdmerge.Columns(1).Frozen = True
                grdmerge.Columns(1).Width = 80
                grdmerge.Columns(2).Frozen = True
                grdmerge.Columns(2).Width = 120
                grdmerge.Columns(3).Width = 120
                grdmerge.Columns(3).Width = 300
                grdmerge.Columns(3).Frozen = True
                grdmerge.Columns(5).Width = 80
                grdmerge.Columns(6).Width = 80
                grdmerge.Columns(7).Width = 80
                grdmerge.Columns(8).Width = 240
                grdmerge.Columns(8).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                ticketrange()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub bwork_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        'lblloading.Location = New Point(285, 61)
        'lblloading.Visible = True
        'pgb1.Style = ProgressBarStyle.Blocks
        'pgb1.Visible = True
        'pgb1.Value = e.ProgressPercentage
        'Panel2.Enabled = False
        'Panel3.Enabled = False
    End Sub

    Private Sub ticketrange()
        Try
            grdletters.Rows.Clear()

            For Each rowtemp As DataGridViewRow In grdmerge.Rows
                Dim m_of As String = grdmerge.Rows(rowtemp.Index).Cells(0).Value
                Dim m_log As String = grdmerge.Rows(rowtemp.Index).Cells(2).Value

                list1.Items.Clear()
                list2.Items.Clear()
                For Each row As DataGridViewRow In grdlist.Rows
                    Dim i_of As String = grdlist.Rows(row.Index).Cells(0).Value
                    Dim i_log As String = grdlist.Rows(row.Index).Cells(2).Value

                    If m_of = i_of And m_log = i_log Then
                        list1.Items.Add(grdlist.Rows(row.Index).Cells(4).Value)
                    End If
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
                                grdletters.Rows.Add(letter1, gtiket, letter1, gtiket, m_of, m_log)
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
                                grdletters.Rows.Add(letter1, gtiket, letter2, gtikettemp, m_of, m_log)
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
                                    grdletters.Rows.Add(letter1, gtiket, letter1, gtiket, m_of, m_log)
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
                                    grdletters.Rows.Add(letter1, gtiket, letter2, gtikettemp, m_of, m_log)
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
                                grdletters.Rows.Add(letter1, gtiket, letter2, gtiketlast, m_of, m_log)
                                '/MsgBox(" ---5")
                                first = ""
                            End If
                        End If
                    End If
                Next
                '////end generate

                Dim transferseries As String = ""
                For Each item As Object In list2.Items
                    transferseries = transferseries & item & ", "
                Next
                grdmerge.Rows(rowtemp.Index).Cells(8).Value = transferseries
            Next

            grdmerge.Columns(3).Width = 200
            grdmerge.Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True

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

    Private Sub txtof_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtof.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnadd.PerformClick()
        End If
    End Sub

    Private Sub txtof_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtof.TextChanged
        If Trim(txtof.Text) <> "" Then
            txtwrs.Enabled = False
        Else
            txtwrs.Enabled = True
        End If
    End Sub

    Private Sub txtwrs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwrs.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnadd.PerformClick()
        End If
    End Sub

    Private Sub txtwrs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwrs.TextChanged
        If Trim(txtwrs.Text) <> "" Then
            txtof.Enabled = False
        Else
            txtof.Enabled = True
        End If
    End Sub

    Private Sub btntransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntransfer.Click
        Try
            If grdmerge.Rows.Count = 0 Then
                MsgBox("Cannot confirm.", MsgBoxStyle.Exclamation, "")
            Else
                If login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" And login.depart <> "All" Then
                    MsgBox("Access denied.", MsgBoxStyle.Critical, "")
                    Exit Sub
                End If

                reccnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If reccnf = True Then
                    If cmbbr.SelectedItem = "AGI-CALAMBA" Then
                        ExecuteTransfer(strconn2)
                    Else
                        ExecuteTransfer(strconn1)
                    End If
                End If
            End If

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

    Private Sub ExecuteTransfer(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                '/command.CommandText = sql
                '/command.ExecuteNonQuery()

                Dim recnum As String = "", temp As String = "", trnum As String = ""
                'check kung pang ilang LOGSHEET NA SA YEAR NA 2018 na
                sql = "Select Count(recid) from tblreceive where recyear=YEAR(GetDate()) and branch='" & login.branch & "'"
                command.CommandText = sql
                recnum = command.ExecuteScalar + 1

                If recnum < 1000000 Then
                    For vv As Integer = 1 To 6 - recnum.Length
                        temp += "0"
                    Next
                    'lbltrnum.Text = Date.Now.Year & "-" & Format(Date.Now, "MM") & Format(Date.Now, "dd") & temp & trnum
                End If

                trnum = "R." & Format(Date.Now, "yy") & "-" & temp & recnum
                txttrans.Text = trnum


                sql = "Select recnum from tblreceive where recnum='" & txttrans.Text & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    Exit Sub
                End If
                dr.Dispose()

                sql = "Insert into tblreceive (recyear, recdate, recnum, branch, qty, remarks, createdby, datecreated, modifiedby, datemodified, status) values(YEAR(GetDate()), GetDate(), '" & txttrans.Text & "', '" & login.branch & "', '" & grdlist.Rows.Count & "', '" & Trim(txtrems.Text.ToString.Replace("'", "''")) & "', '" & login.user & "', GetDate(), '" & login.user & "', GetDate(), '1')"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'insert dun sa tbltransof yung list ng mga orderfill
                For Each row As DataGridViewRow In grdof.Rows
                    Dim col0 As String = grdof.Rows(row.Index).Cells(0).Value
                    Dim col1 As String = grdof.Rows(row.Index).Cells(1).Value
                    Dim col2 As String = grdof.Rows(row.Index).Cells(2).Value

                    sql = "Insert into tblrecof (recnum, ofid, wrsnum, ofnum, createdby, datecreated, status) values ('" & txttrans.Text & "', '" & col0 & "', '" & col1 & "', '" & col2 & "', '" & login.user & "', GetDate(), '1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                'insert dun sa tbltransmerge
                For Each row As DataGridViewRow In grdmerge.Rows
                    '/Dim col0 As String = grdmerge.Rows(row.Index).Cells(0).Value
                    Dim col1 As String = grdmerge.Rows(row.Index).Cells(1).Value
                    Dim col2 As String = grdmerge.Rows(row.Index).Cells(2).Value
                    Dim col3 As String = grdmerge.Rows(row.Index).Cells(3).Value

                    sql = "Insert into tbltransmerge (recnum, proddate, itemname, qty, range, createdby, datecreated, status) values ('" & txttrans.Text & "','" & Format(grdmerge.Rows(row.Index).Cells(0).Value, "yyyy/MM/dd") & "', '" & col1 & "', '" & col2 & "', '" & col3 & "', '" & login.user & "', GetDate(), '1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    'insert dun sa tbltransrange
                    For Each rowrange As DataGridViewRow In grdletters.Rows
                        Dim rcol0 As String = grdletters.Rows(rowrange.Index).Cells(0).Value
                        Dim rcol1 As String = grdletters.Rows(rowrange.Index).Cells(1).Value
                        Dim rcol2 As String = grdletters.Rows(rowrange.Index).Cells(2).Value
                        Dim rcol3 As String = grdletters.Rows(rowrange.Index).Cells(3).Value

                        If Format(grdletters.Rows(rowrange.Index).Cells(4).Value, "yyyy/MM/dd") = Format(grdmerge.Rows(row.Index).Cells(0).Value, "yyyy/MM/dd") And grdletters.Rows(rowrange.Index).Cells(5).Value = col1 Then
                            sql = "Insert into tbltransrange (recnum, transmergeid, letter1, frfrom, letter2, frto, createdby, datecreated, status)"
                            sql = sql & " values ('" & txttrans.Text & "',(Select transmergeid from tbltransmerge where recnum='" & txttrans.Text & "' and proddate='" & Format(grdletters.Rows(rowrange.Index).Cells(4).Value, "yyyy/MM/dd") & "' and itemname='" & grdletters.Rows(rowrange.Index).Cells(5).Value & "'),"
                            sql = sql & " '" & rcol0 & "', '" & rcol1 & "', '" & rcol2 & "', '" & rcol3 & "', '" & login.user & "', GetDate(), '1')"
                            command.CommandText = sql
                            command.ExecuteNonQuery()
                        End If
                    Next
                Next


                MsgBox("Successfully saved.", MsgBoxStyle.Information, "")

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.ToString, MsgBoxStyle.Exclamation, "")
                Try
                    Me.Cursor = Cursors.Default
                    transaction.Rollback()
                Catch ex2 As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox("2: " & ex2.ToString & vbCrLf & vbCrLf & "Please try again.", MsgBoxStyle.Information, "")
                End Try
            End Try
        End Using
    End Sub

    Private Sub txtrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrems.KeyPress

    End Sub

    Private Sub txtrems_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrems.TextChanged

    End Sub

    Private Sub cmbbr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbbr.SelectedIndexChanged

    End Sub

    Private Sub btnconfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnconfirm.Click
        Try
            If grdmerge.Rows.Count = 0 Then
                MsgBox("Cannot confirm.", MsgBoxStyle.Exclamation, "")
            ElseIf cmbwhse.SelectedItem = "" Then
                MsgBox("Select warehouse.", MsgBoxStyle.Exclamation, "")
            Else
                If login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" And login.depart <> "All" Then
                    MsgBox("Access denied.", MsgBoxStyle.Critical, "")
                    Exit Sub
                End If

                reccnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If reccnf = True Then
                    If cmbbr.SelectedItem = "AGI-CALAMBA" Then
                        ExecuteReceive(strconn2)
                    Else
                        ExecuteReceive(strconn1)
                    End If
                End If
            End If

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

    Private Sub ExecuteReceive(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                '/command.CommandText = sql
                '/command.ExecuteNonQuery()

                Dim recnum As String = "", temp As String = "", trnum As String = ""
                'check kung pang ilang LOGSHEET NA SA YEAR NA 2018 na
                Dim rdate As String = Format(daterec.Value, "yyyy")
                sql = "Select Count(recid) from tblreceive where recyear='" & rdate & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                recnum = command.ExecuteScalar + 1

                If recnum < 1000000 Then
                    For vv As Integer = 1 To 6 - recnum.Length
                        temp += "0"
                    Next
                End If

                Dim prefix As String = ""
                If login.branch = "Agi-Calamba" Then
                    prefix = "RCA."
                ElseIf login.branch = "Agi-Calaca" Then
                    prefix = "RCC."
                End If
                trnum = prefix & Format(Date.Now, "yy") & "-" & temp & recnum
                txttrans.Text = trnum


                sql = "Select recnum from tblreceive where recnum='" & txttrans.Text & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    Exit Sub
                End If
                dr.Dispose()

                sql = "Insert into tblreceive (recyear, recdate, recnum, branch, qty, remarks, frmbranch, platenum, driver, createdby, datecreated, modifiedby, datemodified, status)"
                sql = sql & " values('" & rdate & "', '" & Format(daterec.Value, "yyyy/MM/dd") & "', '" & txttrans.Text & "', '" & login.branch & "', '" & grdlist.Rows.Count & "', '" & Trim(txtrems.Text.ToString.Replace("'", "''")) & "', '" & cmbbr.SelectedItem & "', '" & grdof.Rows(0).Cells(3).Value & "', '" & grdof.Rows(0).Cells(4).Value & "', '" & login.user & "', GetDate(), '" & login.user & "', GetDate(), '1')"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'insert dun sa tbltransof yung list ng mga orderfill
                For Each row As DataGridViewRow In grdof.Rows
                    Dim col0 As String = grdof.Rows(row.Index).Cells(0).Value 'ofid
                    Dim col1 As String = grdof.Rows(row.Index).Cells(1).Value 'wrsnum
                    Dim col2 As String = grdof.Rows(row.Index).Cells(2).Value 'ofnum

                    For Each rowlog As DataGridViewRow In grdmerge.Rows
                        Dim ofnum As String = grdmerge.Rows(rowlog.Index).Cells(0).Value 'orderfill
                        Dim logsheet As String = grdmerge.Rows(rowlog.Index).Cells(2).Value

                        If col2 = ofnum Then
                            sql = "Insert into tblrecof (recnum, ofid, wrsnum, ofnum, logsheetnum, createdby, datecreated, status, branch)"
                            sql = sql & " values ('" & txttrans.Text & "', '" & col0 & "', '" & col1 & "', '" & col2 & "', '" & logsheet & "', '" & login.user & "', GetDate(), '1', '" & login.branch & "')"
                            command.CommandText = sql
                            command.ExecuteNonQuery()
                        End If
                    Next
                Next

                'insert dun sa tbltransmerge
                For Each row As DataGridViewRow In grdmerge.Rows
                    Dim colof As String = grdmerge.Rows(row.Index).Cells(0).Value 'orderfill
                    Dim coldate As String = grdmerge.Rows(row.Index).Cells(1).Value 'productiondate
                    Dim collog As String = grdmerge.Rows(row.Index).Cells(2).Value 'logsheet
                    Dim col1 As String = grdmerge.Rows(row.Index).Cells(3).Value 'itemname
                    Dim col4 As String = grdmerge.Rows(row.Index).Cells(4).Value 'thread, 
                    Dim col5 As String = grdmerge.Rows(row.Index).Cells(5).Value 'bagtype
                    Dim col6 As String = grdmerge.Rows(row.Index).Cells(6).Value.ToString 'binnum
                    Dim col2 As String = grdmerge.Rows(row.Index).Cells(7).Value 'qty
                    Dim col3 As String = grdmerge.Rows(row.Index).Cells(8).Value 'ticket


                    Dim lognum As String = "", logtemp As String = "", trlognum As String = ""
                    sql = "Select Count(logsheetid) from tbllogsheet where logsheetyear='" & Format(grdmerge.Rows(row.Index).Cells(1).Value, "yyyy") & "' and branch='" & login.branch & "' and printtype='Receive'"
                    command.CommandText = sql
                    lognum = command.ExecuteScalar + 1

                    If lognum < 1000000 Then
                        For vv As Integer = 1 To 6 - lognum.Length
                            logtemp += "0"
                        Next
                    End If

                    trlognum = "L.R-" & Format(grdmerge.Rows(row.Index).Cells(1).Value, "yy") & "-" & logtemp & lognum

                    '/sql = "Insert into tbltransmerge (transnum, proddate, itemname, qty, range, createdby, datecreated, status) values ('" & txttrans.Text & "','" & Format(grdmerge.Rows(row.Index).Cells(0).Value, "yyyy/MM/dd") & "', '" & col1 & "', '" & col2 & "', '" & col3 & "', '" & login.user & "', GetDate(), '1')"
                    sql = "Insert into tbllogsheet (logsheetyear, logsheetnum, recnum, logsheetdate, createdby, datecreated, modifiedby, datemodified, status, branch, printtype, allitems, weyt, palletizer, whsename, qty, reclogsheet, thread, bagtype, binnum)"
                    sql = sql & " values ('" & Format(grdmerge.Rows(row.Index).Cells(1).Value, "yyyy") & "', '" & trlognum & "', '" & txttrans.Text & "','" & Format(grdmerge.Rows(row.Index).Cells(1).Value, "yyyy/MM/dd") & "', '" & login.user & "', GetDate(), '" & login.user & "', GetDate(), '1', '" & login.branch & "', 'Receive', '0', '0', 'LINE R', '" & cmbwhse.SelectedItem & "', '" & col2 & "', '" & collog & "','" & col4 & "', '" & col5 & "', '" & col6 & "')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Insert into tbllogitem (logsheetid, itemname, tickettype, datecreated, createdby, datemodified, modifiedby, status)"
                    sql = sql & " values ((Select logsheetid from tbllogsheet where logsheetnum='" & trlognum & "' and branch='" & login.branch & "' and recnum='" & txttrans.Text & "' and reclogsheet='" & collog & "'),"
                    sql = sql & " '" & col1 & "', 'Receive', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()


                    'insert dun sa tbltransrange
                    For Each rowrange As DataGridViewRow In grdletters.Rows
                        Dim rcol0 As String = grdletters.Rows(rowrange.Index).Cells(0).Value
                        Dim rcol1 As String = grdletters.Rows(rowrange.Index).Cells(1).Value
                        Dim rcol2 As String = grdletters.Rows(rowrange.Index).Cells(2).Value
                        Dim rcol3 As String = grdletters.Rows(rowrange.Index).Cells(3).Value

                        If grdletters.Rows(rowrange.Index).Cells(4).Value = colof And grdletters.Rows(rowrange.Index).Cells(5).Value = collog Then
                            'sql = "Insert into tbltransrange (transnum, transmergeid, letter1, frfrom, letter2, frto, createdby, datecreated, status)"
                            'sql = sql & " values ('" & txttrans.Text & "',(Select transmergeid from tbltransmerge where transnum='" & txttrans.Text & "' and proddate='" & Format(grdletters.Rows(rowrange.Index).Cells(4).Value, "yyyy/MM/dd") & "' and itemname='" & grdletters.Rows(rowrange.Index).Cells(5).Value & "'),"
                            'sql = sql & " '" & rcol0 & "', '" & rcol1 & "', '" & rcol2 & "', '" & rcol3 & "', '" & login.user & "', GetDate(), '1')"
                            sql = "Insert into tbllogrange (logsheetid, tickettype, letter1, letter2, arfrom, arto, atotal, frfrom, frto, ftotal, datecreated, createdby, datemodified, modifiedby, status)"
                            sql = sql & " values ((Select logsheetid from tbllogsheet where logsheetnum='" & trlognum & "' and branch='" & login.branch & "' and recnum='" & txttrans.Text & "' and reclogsheet='" & collog & "'),"
                            sql = sql & " 'Receive', '" & rcol0 & "', '" & rcol2 & "', '" & rcol1 & "', '" & rcol3 & "', '" & (rcol3 - rcol1) + 1 & "', '0', '0', '0', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '0')"
                            command.CommandText = sql
                            command.ExecuteNonQuery()
                        End If
                    Next
                Next

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default

                MsgBox("Successfully saved.", MsgBoxStyle.Information, "")
                btnadd.Enabled = True
                btnremove.Enabled = True
                txtof.Enabled = True
                txtwrs.Enabled = True
                btngen.Enabled = True
                btncancel.Enabled = False

                table.columns.clear()
                grdlist.Columns.Clear()
                grdmerge.Columns.Clear()
                grdletters.Rows.Clear()
                grdof.Rows.Clear()

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.ToString, MsgBoxStyle.Exclamation, "")
                Try
                    Me.Cursor = Cursors.Default
                    transaction.Rollback()
                Catch ex2 As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox("2: " & ex2.ToString & vbCrLf & vbCrLf & "Please try again.", MsgBoxStyle.Information, "")
                End Try
            End Try
        End Using
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        Dim ofnum As String = grdof.Rows(grdof.CurrentRow.Index).Cells(2).Value
        grdof.Rows.Remove(grdof.CurrentRow)
        MsgBox("You remove " & ofnum & ".", MsgBoxStyle.Information, "")
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        If grdmerge.Rows.Count <> 0 Then
            Dim a As String = MsgBox("Are you sure you want to cancel", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
            If a = vbYes Then
                btnadd.Enabled = True
                btnremove.Enabled = True
                txtof.Enabled = True
                txtwrs.Enabled = True
                btngen.Enabled = True
                btncancel.Enabled = False

                table.columns.clear()
                grdlist.Columns.Clear()
                grdmerge.Columns.Clear()
                grdletters.Rows.Clear()
            End If
        End If
    End Sub

    Private Sub receivenew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        disconnect()
        If login.branch = "Agi-Calaca" Then
            svrcal = True
            svrcal = checkconnection(connStrcal, "Agi-Calamba")
            If svrcal = False Then
                Me.Cursor = Cursors.Default
                MsgBox("Can't connect to Agi-Calamba server.", MsgBoxStyle.Critical, "")
                Me.Dispose()
            End If
        End If

        If login.branch = "Agi-Calamba" Then
            svrcal = True
            svrclc = checkconnection(connStrclc, "Agi-Calaca")
            If svrclc = False Then
                Me.Cursor = Cursors.Default
                MsgBox("Can't connect to Agi-Calaca server.", MsgBoxStyle.Critical, "")
                Me.Dispose()
            End If
        End If
    End Sub
End Class