Imports System.Data.SqlClient
Imports System.ComponentModel
Public Class branorderfill
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim AscendingListBox As New List(Of Integer)
    Public rowindex As Integer
    Public ofcnf As Boolean = False, ofcnfby As String = "", ofrems As String = "", picktickets As Boolean
    Dim b4edit As Integer, gridsql As String, loginbranch As String
    Dim withtemptickets As Boolean = False
    Dim table As DataTable
    Dim rcount As Integer = 0

    Private threadEnabled As Boolean, threadEnabledcomp As Boolean
    Private bwselectitem As BackgroundWorker, bwviewtickets As BackgroundWorker, bwviewpick As BackgroundWorker, bwselected As BackgroundWorker
    Private bwloadcompleted As BackgroundWorker, bwloadcompletedticks As BackgroundWorker

    Private Sub connect()
        conn = New SqlConnection
        conn.ConnectionString = strconn
        If conn.State <> ConnectionState.Open Then
            conn.Open()
        End If
    End Sub

    Private Sub cmbitem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbitem.SelectedIndexChanged

    End Sub

    Private Sub disconnect()
        conn = New SqlConnection
        conn.ConnectionString = strconn
        If conn.State <> ConnectionState.Open Then
            conn.Close()
        End If
    End Sub
    Private Sub branorderfill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtorf.Focus()
        viewofnum()
        loginbranch = login.branch
        cmbitem.DropDownWidth = 300
    End Sub

    Public Sub defaultform()
        txtitem.Text = ""
        txtwrs.Text = ""
        txtwhse.Text = ""
        txtcus.Text = ""
        txtref.Text = ""
        txtplate.Text = ""
        txtdriver.Text = ""
        txtrems.Text = ""
        cmbitem.Text = ""
        txtbags.Text = ""
        txtrems.Text = ""
        grdlog.Rows.Clear()
        txtofitemid.Text = ""
        btnconfirm.Enabled = True
        btnsearch.Enabled = True
        btnwrssearch.Enabled = True
    End Sub

    Private Sub branorderfill_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub branorderfill_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Panel2.Enabled = True
        txtwrs.Focus()
        viewofnum()
        viewitems()
    End Sub
    Public Sub viewitems()
        Try
            If txtorf.Text <> "" Then
                cmbitem.Items.Clear()
                cmbitem.Items.Add("")

                sql = "Select itemname from tblofitem where ofnum='" & lblorf.Text & txtorf.Text & "' and status<>'3' and branch='" & login.branch & "' order by itemname"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    cmbitem.Items.Add(dr("itemname"))
                End While
                dr.Dispose()
                cmd.Dispose()
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
    Private Sub viewofnum()
        Try
            If txtorf.ReadOnly = False Then
                sql = "Select * from tblorderfill where ofnum='" & lblorf.Text & Trim(txtorf.Text) & "' and branch='" & login.branch & "' and whsename='BRAN WHSE'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        txtorf.ReadOnly = False
                        txtorf.Text = ""
                        txtorf.Focus()
                        defaultform()
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        txtorf.ReadOnly = False
                        txtorf.Text = ""
                        txtorf.Focus()
                        defaultform()
                        Exit Sub
                    End If

                    'check user level
                    lblorfid.Text = dr("ofid")
                    Dim s As String = dr("ofnum").ToString
                    txtorf.Text = s.Substring(3, s.Length - 3)
                    txtwrs.Text = dr("wrsnum")
                    txtwhse.Text = dr("whsename").ToString
                    txtcus.Text = dr("customer")
                    txtref.Text = dr("cusref")
                    txtplate.Text = dr("platenum")
                    txtdriver.Text = dr("driver").ToString
                    txtrems.Text = dr("remarks")
                    txtorf.ReadOnly = True
                    lblwhse.Text = dr("whsename")

                    If dr("status") = 2 Then
                        btnconfirm.Text = "Confirmed Order Fill"
                        Panel1.Enabled = False
                        btnconfirm.Enabled = False
                    Else
                        btnconfirm.Text = "Confirm Order Fill"
                        Panel1.Enabled = True
                        btnconfirm.Enabled = False
                    End If

                    txtorf.ReadOnly = True
                Else
                    MsgBox("Cannot find order fill number.", MsgBoxStyle.Critical, "")
                    txtorf.ReadOnly = False
                    txtorf.Text = ""
                    txtorf.Focus()
                    defaultform()
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if wala ng tblofitem na status is 1
                sql = "Select status from tblofitem where ofnum='" & lblorf.Text & txtorf.Text & "' and (status='1' or status='0') and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    btnconfirm.Enabled = False
                Else
                    If btnconfirm.Text <> "Confirmed Order Fill" Then
                        btnconfirm.Enabled = True
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            End If

            If txtorf.ReadOnly = True And Trim(txtorf.Text) <> "" Then
                Panel3.Enabled = True
            Else
                Panel3.Enabled = False
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
    Private Sub viewwrsnum()
        Try
            If txtwrs.ReadOnly = False Then
                sql = "Select * from tblorderfill where wrsnum='" & Trim(txtwrs.Text) & "' and status='1' and branch='" & login.branch & "' and whsename='BRAN WHSE'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    'check user level
                    lblorfid.Text = dr("ofid")
                    Dim s As String = dr("ofnum").ToString
                    txtorf.Text = s.Substring(3, s.Length - 3)
                    txtwrs.Text = dr("wrsnum")
                    txtwhse.Text = dr("whsename").ToString
                    txtcus.Text = dr("customer")
                    txtref.Text = dr("cusref")
                    txtplate.Text = dr("platenum")
                    txtdriver.Text = dr("driver").ToString
                    txtrems.Text = dr("remarks")
                    txtwrs.ReadOnly = True

                    If dr("status") = 2 Then
                        btnconfirm.Text = "Confirmed Order Fill"
                        Panel1.Enabled = False
                        btnconfirm.Enabled = False
                    Else
                        btnconfirm.Text = "Confirm Order Fill"
                        Panel1.Enabled = True
                        btnconfirm.Enabled = False
                    End If
                Else
                    MsgBox("Cannot find WRS number.", MsgBoxStyle.Critical, "")
                    txtwrs.Text = ""
                    txtwrs.Focus()
                    defaultform()
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if wala ng tblofitem na status is 1
                sql = "Select * from tblofitem where ofnum='" & lblorf.Text & txtwrs.Text & "' and (status='1' or status='0') and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    btnconfirm.Enabled = False
                Else
                    If btnconfirm.Text <> "Confirmed Order Fill" Then
                        btnconfirm.Enabled = True
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            End If

            If txtwrs.ReadOnly = True And Trim(txtwrs.Text) <> "" Then
                Panel3.Enabled = True
            Else
                Panel3.Enabled = False
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

    Public Sub viewofid()
        Try
            If txtorf.ReadOnly = False Then
                sql = "Select status,ofid,ofnum,wrsnum,whsename,customer,cusref,platenum,driver,remarks,whsename from tblorderfill where ofid='" & lblorfid.Text & "' and whsename='BRAN WHSE'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        txtorf.ReadOnly = False
                        txtorf.Text = ""
                        txtorf.Focus()
                        defaultform()
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        txtorf.ReadOnly = False
                        txtorf.Text = ""
                        txtorf.Focus()
                        defaultform()
                        Exit Sub
                    End If

                    'check user level
                    lblorfid.Text = dr("ofid")
                    Dim s As String = dr("ofnum").ToString
                    txtorf.Text = s.Substring(3, s.Length - 3)
                    txtwrs.Text = dr("wrsnum")
                    txtwhse.Text = dr("whsename").ToString
                    txtcus.Text = dr("customer")
                    txtref.Text = dr("cusref")
                    txtplate.Text = dr("platenum")
                    txtdriver.Text = dr("driver").ToString
                    txtrems.Text = dr("remarks")
                    txtorf.ReadOnly = True
                    lblwhse.Text = dr("whsename")

                    If dr("status") = 2 Then
                        btnconfirm.Text = "Confirmed Order Fill"
                        Panel1.Enabled = False
                        btnconfirm.Enabled = False
                    Else
                        btnconfirm.Text = "Confirm Order Fill"
                        Panel1.Enabled = True
                        btnconfirm.Enabled = False
                    End If

                    txtorf.ReadOnly = True
                Else
                    MsgBox("Cannot find order fill number.", MsgBoxStyle.Critical, "")
                    txtorf.ReadOnly = False
                    txtorf.Text = ""
                    txtorf.Focus()
                    defaultform()
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if wala ng tblofitem na status is 1
                sql = "Select ofnum from tblofitem where ofnum='" & lblorf.Text & txtorf.Text & "' and (status='1' or status='0') and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    btnconfirm.Enabled = False
                Else
                    If btnconfirm.Text <> "Confirmed Order Fill" Then
                        btnconfirm.Enabled = True
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            End If

            If txtorf.ReadOnly = True And Trim(txtorf.Text) <> "" Then
                Panel3.Enabled = True
            Else
                Panel3.Enabled = False
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
    Private Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        Try
            If txtorf.ReadOnly = True Then
                'parang warning para maisave muna kung may pending na order fill
                txtorf.ReadOnly = False
                txtorf.Focus()
                Panel3.Enabled = False
                '/grdlog.Enabled = False
                'Me.ContextMenuStrip1.Enabled = False
                txtwrs.Text = ""
                txtwrs.ReadOnly = True
                If btnconfirm.Text <> "Confirmed Order Fill" Then

                Else
                    txtorf.ReadOnly = False
                    txtorf.Focus()
                    Panel3.Enabled = False
                End If
            Else
                '/grdlog.Enabled = true
                'Me.ContextMenuStrip1.Enabled = True
                viewofnum()
                viewitems()
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
    Private Sub btnselect_Click(sender As Object, e As EventArgs) Handles btnselect.Click
        If cmbitem.Enabled = False Then
            cmbitem.Enabled = True
            Panelbuttons.Enabled = False
            Me.CancelToolStripMenuItem.Visible = False
            Me.EditNoOfSelectedBagsToolStripMenuItem.Visible = False
        End If
    End Sub

    Private Sub cmbitem_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbitem.SelectedValueChanged
        selectitemcmb()
    End Sub
    Private Sub selectitemcmb()
        Try
            If Trim(cmbitem.Text) <> "" Then
                Me.Cursor = Cursors.WaitCursor
                sql = "Select * from tblofitem where ofnum='" & lblorf.Text & txtorf.Text & "' and itemname='" & Trim(cmbitem.Text) & "' and status<>'3' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    txtitem.Text = dr("itemname")
                    txtofitemid.Text = dr("ofitemid")
                    txtbags.Text = dr("numbags")
                    Panelbuttons.Enabled = True
                    Me.CancelToolStripMenuItem.Visible = True

                    If dr("status") = 2 Then
                        'meaning confirm for selected item only
                        '/grdtickets.Rows.Clear()
                        grdlog.Rows.Clear()

                        btnqasample.Text = "QA Sampling Completed"
                        btnqasample.Enabled = False
                        btncomplete.Text = "Selected Item Completed"
                        btncomplete.Enabled = False
                        btnpallet.Enabled = False
                        btnrefresh.Enabled = False
                        btnprint.Enabled = False
                        If btnconfirm.Text <> "Confirmed Order Fill" Then
                            btnconfirm.Enabled = True
                        Else
                            btnprint.Enabled = True
                        End If
                        btnsave.Enabled = False

                    Else
                        '/If dr("qasampling") = 0 Then
                        '/viewsavetbltempsperitem()

                        '/btnqasample.Enabled = True
                        '/btnqasample.Text = "QA Sampling Complete"
                        '/btncomplete.Enabled = False
                        '/btncomplete.Text = "Selected Item Complete"
                        '/btnpallet.Enabled = False
                        '/btnrefresh.Enabled = True
                        '/btnconfirm.Enabled = False
                        '/btnsave.Enabled = False

                        '/ElseIf dr("qasampling") = 1 Then
                        '/viewsavetbltempsperitem()

                        If dr("qasampling") = 0 Then
                            btnqasample.Text = "QA Sampling Complete"
                            btnqasample.Enabled = True
                        ElseIf dr("qasampling") = 1 Then
                            btnqasample.Text = "QA Sampling Completed"
                            btnqasample.Enabled = False
                        End If

                        btncomplete.Enabled = True
                        btncomplete.Text = "Selected Item Complete"
                        btnpallet.Enabled = True
                        btnrefresh.Enabled = True
                        btnconfirm.Enabled = False
                        btnprint.Enabled = False
                        btnsave.Enabled = True
                        '/End If
                    End If
                Else
                    txtitem.Text = ""
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                sql = "Select tickettype from tblitems where itemname='" & Trim(cmbitem.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    lbltype.Text = dr("tickettype").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                Me.Cursor = Cursors.Default

                If Val(txtbags.Text) <> 0 Then
                    cmbitem.Enabled = False
                End If

                If btncomplete.Text = "Selected Item Completed" Then
                    loadcompleted()
                Else

                    '====viewsavetbltempsperitem()
                    viewitemlogs()
                    '===viewtickets()
                    'can edit grdlogs
                    '/grdlog.Enabled = true
                    Me.ContextMenuStrip1.Enabled = True
                End If

                countselected()
            Else
                txtbags.Text = 0
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
    Private Sub countselected()
        Dim ctr As Integer = 0
        If grdlog.Rows.Count <> 0 Then
            For Each row As DataGridViewRow In grdlog.Rows
                ctr = ctr + grdlog.Rows(row.Index).Cells("selected").Value
            Next
        End If
        lblseltotal.Text = ctr
    End Sub

    Private Sub loadcompleted()
        Try

            'gridsql = "Select tbllogticket.logticketid, tbllogticket.letter1, tbllogticket.letter4, tbllogticket.astart, tbllogticket.fend, tbloflog.oflogid, tbloflog.logsheetdate, tbloflog.logsheetnum, tbloflog.palletnum, tbloflog.selectedbags, tbloflog.ticketseries, tbloflog.picktickets"
            'gridsql = gridsql & " from tbloflog left outer join tbllogticket on tbloflog.palletnum=tbllogticket.palletnum"
            'gridsql = gridsql & " where tbloflog.ofitemid='" & txtofitemid.Text & "'"

            gridsql = "Select t.logticketid, t.letter1, t.letter4, t.astart, t.fend, l.oflogid, l.logsheetdate, l.logsheetnum,"
            gridsql = gridsql & " l.palletnum, l.selectedbags, l.ticketseries, l.picktickets"
            gridsql = gridsql & " from tbloflog l left outer join tbllogticket t on l.palletnum=t.palletnum and t.logsheetid=(Select s.logsheetid from tbllogsheet s where s.logsheetnum=l.logsheetnum)"
            gridsql = gridsql & " where l.ofitemid='" & txtofitemid.Text & "'"

            rcount = 0
            Dim sqlcnt As String = "Select Count(*) from (" & gridsql & ") x"
            connect()
            cmd = New SqlCommand(sqlcnt, conn)
            rcount = cmd.ExecuteScalar
            cmd.Dispose()
            conn.Close()

            pgb1.Value = 0

            bwloadcompleted = New BackgroundWorker()
            bwloadcompleted.WorkerSupportsCancellation = True
            bwloadcompletedticks = New BackgroundWorker()
            bwloadcompletedticks.WorkerSupportsCancellation = True

            AddHandler bwloadcompleted.DoWork, New DoWorkEventHandler(AddressOf bwloadcompleted_DoWork)
            AddHandler bwloadcompleted.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bwloadcompleted_Completed)
            AddHandler bwloadcompleted.ProgressChanged, New ProgressChangedEventHandler(AddressOf bwloadcompleted_ProgressChanged)
            m_CompaddDelegate = New CompAddDelegate(AddressOf CompAddDGVRow)

            '/AddHandler bwloadcompletedticks.DoWork, New DoWorkEventHandler(AddressOf bwloadcompletedticks_DoWork)
            '/AddHandler bwloadcompletedticks.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bwloadcompletedticks_Completed)
            '/AddHandler bwloadcompletedticks.ProgressChanged, New ProgressChangedEventHandler(AddressOf bwloadcompletedticks_ProgressChanged)
            '/m_CompviewtickDelegate = New CompviewtickDelegate(AddressOf CompviewtickRow)

            If Not bwloadcompleted.IsBusy Then
                bwloadcompleted.WorkerReportsProgress = True
                bwloadcompleted.WorkerSupportsCancellation = True
                bwloadcompleted.RunWorkerAsync() 'start ng select query
            End If

            '/ExecuteLoadCompleted(strconn)

            '/loadcompletedtickets()
            '/grdlog.Enabled = False
            Me.ContextMenuStrip1.Enabled = False

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
    Private Sub bwloadcompleted_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        threadEnabled = True
        Dim rowcount As Integer = 0, i As Integer = 0

        Dim connection As SqlConnection
        connection = New SqlConnection
        connection.ConnectionString = strconn
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If
        cmd = New SqlCommand(gridsql, connection)
        Dim drx As SqlDataReader = cmd.ExecuteReader
        While drx.Read
            If grdlog.InvokeRequired Then
                grdlog.Invoke(m_CompaddDelegate, drx("oflogid"), Format(drx("logsheetdate"), "yyyy/MM/dd"), drx("logsheetnum"), drx("palletnum"), 0, 0, drx("selectedbags"), drx("logticketid"), i)
            Else
                CompAddDGVRow(drx("oflogid"), Format(drx("logsheetdate"), "yyyy/MM/dd"), drx("logsheetnum"), drx("palletnum"), 0, 0, drx("selectedbags"), drx("logticketid"), i)
            End If
            i += 1
            bwloadcompleted.ReportProgress((i / (rcount)) * 100) '/ idivide kung ilan ang total
        End While
        drx.Dispose()
        cmd.Dispose()
        connection.Close()

        sql = "Select logsheetnum, palletnum, cticketnum, cticketdate, remarks, cancelby from tbloflogcancel where ofitemid='" & txtofitemid.Text & "'"
        connection = New SqlConnection
        connection.ConnectionString = strconn
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If
        cmd = New SqlCommand(sql, connection)
        drx = cmd.ExecuteReader
        While drx.Read
            '/cmbcancel.Items.Add(drx("cticketnum"))
            '/grdcancel.Rows.Add(drx("logsheetnum"), drx("palletnum"), drx("letter"), drx("cticketnum"), drx("cticketdate"), drx("remarks"), drx("ticketid"), drx("cancelby").ToString)
            AddDGVRowCancel(drx("logsheetnum"), drx("palletnum"), "1", drx("cticketdate"), drx("remarks"), drx("cancelby"))
        End While
        drx.Dispose()
        cmd.Dispose()
        connection.Close()
    End Sub

    Delegate Sub CompAddDelegate(ByVal value0 As Object, ByVal value1 As Object, ByVal value2 As Object, ByVal value3 As Object, ByVal value4 As Object, ByVal value5 As Object, ByVal value6 As Object, ByVal value7 As Object, ByVal valuerowin As Object)
    Private m_CompaddDelegate As CompAddDelegate

    Private Sub CompAddDGVRow(ByVal val0 As Integer, ByVal val1 As Date, ByVal val2 As String, ByVal val3 As String, ByVal val4 As String, ByVal val5 As String, ByVal val6 As String, ByVal val7 As Integer, ByVal rowin As Integer)
        If threadEnabled = True Then
            If grdlog.InvokeRequired Then
                grdlog.BeginInvoke(New CompAddDelegate(AddressOf CompAddDGVRow), val0, val1, val2, val3, val4, val5, val6, val7, rowin)
            Else
                grdlog.Rows.Add(val0, val1, val2, val3, val4, val5, val6, val7)
                '/grdlog.Rows(rowin).Cells(0).Tag = valcancel
            End If
        End If
    End Sub

    Private Sub btnothers_Click(sender As Object, e As EventArgs) Handles btnothers.Click
        orderfillothers.ofid = lblorfid.Text
        orderfillothers.frm = Me.Name
        orderfillothers.ShowDialog()
    End Sub

    Private Sub bwloadcompleted_Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Me.Cursor = Cursors.Default
        lblloading.Visible = False
        pgb1.Visible = False
        pgb1.Style = ProgressBarStyle.Blocks
        Panel2.Enabled = True
    End Sub

    Private Sub bwloadcompleted_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        '3, 104
        lblloading.Location = New Point(3, 104)
        lblloading.Visible = True
        pgb1.Style = ProgressBarStyle.Blocks
        pgb1.Visible = True
        pgb1.Value = e.ProgressPercentage
        Panel2.Enabled = False
    End Sub

    Private Sub SelectToolStripButton1_Click(sender As Object, e As EventArgs) Handles SelectToolStripButton1.Click
        If grdlog.Rows.Count <> 0 And btnsave.Enabled = True Then
            'dapat automatic mag ssave
            Dim a As String = MsgBox("Do you want to save first?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
            If a = vbYes Then
                ofcnf = False
                btnsave.PerformClick()
                If ofcnf = False Then
                    Exit Sub
                End If
            End If
        End If


        txtorf.ReadOnly = False
        orderfillselect.frm = Me.Name
        orderfillselect.ShowDialog()
    End Sub

    Private Sub btnwrssearch_Click(sender As Object, e As EventArgs) Handles btnwrssearch.Click
        Try
            If txtwrs.ReadOnly = True Then
                'parang warning para maisave muna kung may pending na order fill
                txtwrs.ReadOnly = False
                txtwrs.Focus()
                Panel3.Enabled = False
                '/grdlog.Enabled = False
                Me.ContextMenuStrip1.Enabled = False
                txtorf.Text = ""
                txtorf.ReadOnly = True
                If btnconfirm.Text <> "Confirmed Order Fill" Then

                Else
                    txtwrs.ReadOnly = False
                    txtwrs.Focus()
                    Panel3.Enabled = False
                End If
            Else
                '/grdlog.Enabled = true
                Me.ContextMenuStrip1.Enabled = True
                viewwrsnum()
                viewitems()
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

    Private Sub txtorf_TextChanged(sender As Object, e As EventArgs) Handles txtorf.TextChanged
        Dim str As String
        str = txtorf.Text
        If str.Length > 3 Then
            Dim answer As String
            answer = str.Substring(0, 3)
            If answer = "OF." Then
                str = str.Substring(3, str.Length - 3)
                txtorf.Text = str
                txtorf.Select(txtorf.Text.Length, 0)
            End If
        End If
    End Sub

    Private Sub viewitemlogs()
        Try
            Me.Cursor = Cursors.WaitCursor

            grdlog.Rows.Clear()

            Dim tbltempofitem As String = "tbltempofitem" & txtofitemid.Text
            Dim tbltempnamelog As String = "tbltempoflog" & txtofitemid.Text
            Dim tblexistofitem As Boolean = False, tblexistlog As Boolean = False

            sql = "Select name from sys.tables where name = '" & tbltempofitem & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                tblexistofitem = True
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If tblexistofitem = True Then
                sql = "Select name from sys.tables where name = '" & tbltempnamelog & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    tblexistlog = True
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            End If

            If tblexistlog = True Then
                gridsql = "Select t.letter1, t.letter4, t.astart, t.fend, " & tbltempnamelog & ".oflogid, " & tbltempnamelog & ".logsheetdate, " & tbltempnamelog & ".logsheetnum, " & tbltempnamelog & ".logticketid, " & tbltempnamelog & ".palletnum, " & tbltempnamelog & ".selectedbags, " & tbltempnamelog & ".ticketseries, " & tbltempnamelog & ".picktickets"
                gridsql = gridsql & " from " & tbltempnamelog & " inner join tbllogticket t on " & tbltempnamelog & ".palletnum=t.palletnum inner join tbllogsheet s on t.logsheetid=s.logsheetid"
                gridsql = gridsql & " where " & tbltempnamelog & ".ofitemid='" & txtofitemid.Text & "' and s.branch='" & login.branch & "'"

                rcount = 0
                Dim sqlcnt As String = "Select Count(*) from (" & gridsql & ") x"
                connect()
                cmd = New SqlCommand(sqlcnt, conn)
                rcount = cmd.ExecuteScalar
                cmd.Dispose()
                conn.Close()

                pgb1.Value = 0

                bwselectitem = New BackgroundWorker()
                bwselectitem.WorkerSupportsCancellation = True
                bwviewtickets = New BackgroundWorker()
                bwviewtickets.WorkerSupportsCancellation = True
                bwviewpick = New BackgroundWorker()
                bwviewpick.WorkerSupportsCancellation = True
                bwselected = New BackgroundWorker()
                bwselected.WorkerSupportsCancellation = True

                '/ProgressBar1.Style = ProgressBarStyle.Marquee
                AddHandler bwselectitem.DoWork, New DoWorkEventHandler(AddressOf bwselectitem_DoWork)
                AddHandler bwselectitem.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bwselectitem_Completed)
                AddHandler bwselectitem.ProgressChanged, New ProgressChangedEventHandler(AddressOf bwselectitem_ProgressChanged)
                m_addRowDelegate = New AddRowDelegate(AddressOf AddDGVRow)

                AddHandler bwselected.DoWork, New DoWorkEventHandler(AddressOf bwselected_DoWork)
                AddHandler bwselected.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bwselected_Completed)
                AddHandler bwselected.ProgressChanged, New ProgressChangedEventHandler(AddressOf bwselected_ProgressChanged)
                m_addselectedDelegate = New AddselectedDelegate(AddressOf AddselectedRow)

                If Not bwselectitem.IsBusy Then
                    bwselectitem.WorkerReportsProgress = True
                    bwselectitem.WorkerSupportsCancellation = True
                    bwselectitem.RunWorkerAsync() 'start ng select query
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

    Private Sub txtwrs_TextChanged(sender As Object, e As EventArgs) Handles txtwrs.TextChanged

    End Sub

    Private Sub bwselectitem_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        threadEnabled = True
        Dim rowcount As Integer = 0, i As Integer = 0

        Dim connection As SqlConnection
        connection = New SqlConnection
        connection.ConnectionString = strconn
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If
        cmd = New SqlCommand(gridsql, connection)
        Dim drx As SqlDataReader = cmd.ExecuteReader
        While drx.Read
            If grdlog.InvokeRequired Then
                grdlog.Invoke(m_addRowDelegate, drx("oflogid"), Format(drx("logsheetdate"), "yyyy/MM/dd"), drx("logsheetnum"), drx("palletnum"), 0, 0, drx("selectedbags"), drx("logticketid"), i)
            Else
                AddDGVRow(drx("oflogid"), Format(drx("logsheetdate"), "yyyy/MM/dd"), drx("logsheetnum"), drx("palletnum"), 0, 0, drx("selectedbags"), drx("logticketid"), i)
            End If
            i += 1
            bwselectitem.ReportProgress((i / (rcount)) * 100) '/ idivide kung ilan ang total
            '/System.Threading.Thread.Sleep(50)
        End While
        drx.Dispose()
        cmd.Dispose()
        connection.Close()

        Dim tbltempnamecancel As String = "tbltempoflogcancel" & txtofitemid.Text
        Dim tblexistcancel As Boolean = False
        sql = "Select name from sys.tables where name = '" & tbltempnamecancel & "'"
        connection = New SqlConnection
        connection.ConnectionString = strconn
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If
        cmd = New SqlCommand(sql, connection)
        drx = cmd.ExecuteReader
        If drx.Read Then
            tblexistcancel = True
        End If
        drx.Dispose()
        cmd.Dispose()
        connection.Close()

        If tblexistcancel = True Then
            For Each row As DataGridViewRow In grdlog.Rows
                Dim paltag As String = grdlog.Rows(row.Index).Cells("pallettag").Value
                sql = "Select logsheetnum, palletnum, cticketnum, cticketdate, remarks, cancelby from " & tbltempnamecancel
                sql = sql & " where ofitemid='" & txtofitemid.Text & "' and palletnum='" & paltag & "'"
                connection = New SqlConnection
                connection.ConnectionString = strconn
                If connection.State <> ConnectionState.Open Then
                    connection.Open()
                End If
                cmd = New SqlCommand(sql, connection)
                drx = cmd.ExecuteReader
                While drx.Read
                    AddDGVRowCancel(drx("logsheetnum"), drx("palletnum"), drx("cticketnum"), drx("cticketdate"), drx("remarks"), drx("cancelby"))
                End While
                drx.Dispose()
                cmd.Dispose()
                connection.Close()
            Next
        End If

    End Sub

    Delegate Sub AddRowDelegate(ByVal value0 As Object, ByVal value1 As Object, ByVal value2 As Object, ByVal value3 As Object, ByVal value4 As Object, ByVal value5 As Object, ByVal value6 As Object, ByVal value7 As Object, ByVal valuerowin As Object)
    Private m_addRowDelegate As AddRowDelegate

    Private Sub AddDGVRow(ByVal val0 As Integer, ByVal val1 As Date, ByVal val2 As String, ByVal val3 As String, ByVal val4 As String, ByVal val5 As String, ByVal val6 As String, ByVal val7 As Integer, ByVal rowin As Integer)
        If threadEnabled = True Then
            If grdlog.InvokeRequired Then
                grdlog.BeginInvoke(New AddRowDelegate(AddressOf AddDGVRow), val0, val1, val2, val3, val4, val5, val6, val7, rowin)
            Else
                grdlog.Rows.Add(val0, val1, val2, val3, val4, val5, val6, val7)
                '/grdlog.Rows(rowin).Cells(0).Tag = valcancel
            End If
        End If
    End Sub

    Delegate Sub AddRowCancelDelegate(ByVal value0 As Object, ByVal value1 As Object, ByVal value2 As Object, ByVal value3 As Object, ByVal value4 As Object, ByVal value5 As Object)
    Private m_addRowCancelDelegate As AddRowCancelDelegate

    Private Sub AddDGVRowCancel(ByVal val0 As String, ByVal val1 As String, ByVal val2 As String, ByVal val3 As Date, ByVal val4 As String, ByVal val5 As String)
        If threadEnabled = True Then
            If grdcancel.InvokeRequired Then
                grdcancel.BeginInvoke(New AddRowCancelDelegate(AddressOf AddDGVRowCancel), val0, val1, val2, val3, val4, val5)
            Else
                grdcancel.Rows.Add(val0, val1, val2, val3, val4, val5)
            End If
        End If
    End Sub

    Private Sub bwselectitem_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        '3, 104
        lblloading.Location = New Point(3, 104)
        lblloading.Visible = True
        pgb1.Style = ProgressBarStyle.Blocks
        pgb1.Visible = True
        pgb1.Value = e.ProgressPercentage
        Panel2.Enabled = False
    End Sub

    Private Sub bwselectitem_Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        If Not bwselectitem.IsBusy Then
            bwselected.WorkerReportsProgress = True
            bwselected.WorkerSupportsCancellation = True
            bwselected.RunWorkerAsync()
        End If
        'Me.Cursor = Cursors.Default
        'lblloading.Visible = False
        'pgb1.Visible = False
        'pgb1.Style = ProgressBarStyle.Blocks
        'Panel2.Enabled = True

        'If grdlog.Rows.Count <> 0 And threadEnabled = True And lblloading.Visible = False Then
        '    grdlog.SuspendLayout()
        '    grdlog.ResumeLayout()

        '    '/MsgBox("Loading data completed.", MsgBoxStyle.Information, "")
        '    threadEnabled = False

        '    'viewselectedtickets()
        'End If
    End Sub
    Private Sub bwselected_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        threadEnabled = True
        Dim i As Integer = 0

        For Each row As DataGridViewRow In grdlog.Rows
            Dim countgood As Integer = 0
            Dim lognum As String = grdlog.Rows(row.Index).Cells("lognum").Value

            sql = "Select Count(loggoodid) from tblloggood g"
            sql = sql & " Right outer join tbllogticket t on t.logticketid=g.logticketid"
            sql = sql & " Right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
            sql = sql & " where s.logsheetnum='" & lognum & "' and t.palletnum='" & grdlog.Rows(row.Index).Cells("pallettag").Value & "' and g.status='1' and s.branch='" & loginbranch & "'"
            Dim connection As SqlConnection
            connection = New SqlConnection
            connection.ConnectionString = strconn
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            cmd = New SqlCommand(sql, connection)
            countgood = cmd.ExecuteScalar
            cmd.Dispose()
            connection.Close()

            AddselectedRow(countgood, i)
            i += 1

            bwselected.ReportProgress((i / rcount) * 100)
        Next
    End Sub

    Delegate Sub AddselectedDelegate(ByVal value0 As Object, ByVal vrow As Object)
    Private m_addselectedDelegate As AddselectedDelegate

    Private Sub grdlog_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdlog.CellContentClick

    End Sub

    Private Sub AddselectedRow(ByVal val0 As Integer, ByVal vrow As Integer)
        If threadEnabled = True Then
            If grdlog.InvokeRequired Then
                grdlog.BeginInvoke(New AddselectedDelegate(AddressOf AddselectedRow), val0, vrow)
            Else
                'count cancel per pallet
                Dim countcancelticket As Integer = 0

                grdlog.Item("avail", vrow).Value = val0
                If grdlog.Item("selected", vrow).Value = 0 Then
                    grdlog.Item("selected", vrow).Value = (val0) - grdlog.Item("cnl", vrow).Value
                End If

                If grdlog.Item("avail", vrow).Value < grdlog.Item("selected", vrow).Value Then
                    If btncomplete.Text <> "Selected Item Completed" Then
                        grdlog.Rows(vrow).Cells("selected").Style.BackColor = Color.Maroon
                        grdlog.Rows(vrow).Cells("selected").ErrorText = "Total available bags is less than the number of selected bags."
                    End If
                Else
                    grdlog.Rows(vrow).Cells("selected").Style.BackColor = Nothing
                    grdlog.Rows(vrow).Cells("selected").ErrorText = ""
                End If
            End If
        End If
    End Sub

    Private Sub bwselected_Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Me.Cursor = Cursors.Default
        lblloading.Visible = False
        pgb1.Visible = False
        pgb1.Style = ProgressBarStyle.Blocks
        Panel2.Enabled = True

        If grdlog.Rows.Count <> 0 And threadEnabled = True And lblloading.Visible = False Then
            grdlog.SuspendLayout()
            grdlog.ResumeLayout()

            '/MsgBox("Loading data completed.", MsgBoxStyle.Information, "")
            threadEnabled = False

            'viewselectedtickets()
            countcnl()
            countselected()
        End If
    End Sub

    Private Sub bwselected_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        pgb1.Value = e.ProgressPercentage
    End Sub


    Public Sub selectorderfill()
        Try
            If txtorf.ReadOnly = True Then
                'parang warning para maisave muna kung may pending na order fill
                txtorf.ReadOnly = False
                txtorf.Focus()
                Panel3.Enabled = False
                '/grdlog.Enabled = False
                Me.ContextMenuStrip1.Enabled = False
                txtwrs.Text = ""
                txtwrs.ReadOnly = True
                If btnconfirm.Text <> "Confirmed Order Fill" Then

                Else
                    txtorf.ReadOnly = False
                    txtorf.Focus()
                    Panel3.Enabled = False
                End If
            Else
                '/grdlog.Enabled = true
                Me.ContextMenuStrip1.Enabled = True
                viewofid()
                viewitems()
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

    Private Sub btnpallet_Click(sender As Object, e As EventArgs) Handles btnpallet.Click
        Try
            If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                branofpallets.grdpallets.Rows.Clear()
                branofpallets.orderfillitemid = txtofitemid.Text
                branofpallets.lblwhse.Text = lblwhse.Text
                branofpallets.datefrom.Value = Date.Now
                branofpallets.dateto.Value = Date.Now
                branofpallets.ofnum = lblorf.Text & txtorf.Text
                branofpallets.ofc = txtcus.Text
                '/MsgBox("if grdlog is not zero then ung selected nya dapat yun din ang selected sa grdselect sa orderfillpallet")

                If grdlog.Rows.Count <> 0 Then
                    For Each rowlog As DataGridViewRow In grdlog.Rows
                        For Each rowselect As DataGridViewRow In branofpallets.grdselect.Rows
                            If branofpallets.grdselect.Rows(rowselect.Index).Cells(3).Value = grdlog.Rows(rowlog.Index).Cells("pallettag").Value Then
                                branofpallets.grdselect.Rows(rowselect.Index).Cells(8).Value = grdlog.Rows(rowlog.Index).Cells("selected").Value
                                branofpallets.grdselect.Rows(rowselect.Index).Cells(10).Value = False
                            End If
                        Next
                    Next
                End If

                branofpallets.ShowDialog()
                countselected()
                ExecuteAutoSave(strconn)

                For i = 0 To grdlog.Columns.Count - 1
                    grdlog.Columns(i).ReadOnly = True
                Next
            Else
                MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
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

    Private Sub ExecuteAutoSave(ByVal connectionString As String)
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

                'check if orderfill is not cancelled
                sql = "Select status from tblorderfill where ofid='" & lblorfid.Text & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        '/MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        '/MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()

                'check if ofitem is not cancelled
                sql = "Select status from tblofitem where ofitemid='" & txtofitemid.Text & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        '/MsgBox("Item is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        '/MsgBox("Item is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()

                'gawing 0 - in process yung status ng tblofitem
                sql = "Update tblofitem set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where ofitemid='" & txtofitemid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'saving\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                'where ofitemid=lblofitemid.text

                'insert temporary tblofitem in tbltempofitem1
                'per items////////////////////////////////////////////////////////////////////////////
                Dim tbltempofitem As String = "tbltempofitem" & txtofitemid.Text
                Dim tblexistofitem As Boolean = False
                sql = "Select name from sys.tables where name = '" & tbltempofitem & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistofitem = True
                End If
                dr.Dispose()

                If tblexistofitem = False Then
                    'create tbltempofitem1
                    sql = "Create Table " & tbltempofitem & " (ofitemid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofnum nvarchar(MAX), wrsnum nvarchar(MAX), itemname nvarchar(50), numbags int, oftype nvarchar(50), remarks nvarchar(MAX), datecreated datetime, createdby nvarchar(50), datemodified datetime, modifiedby nvarchar(50), status int, branch nvarchar(50))"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempofitem1 where lblofitemid
                    sql = "TRUNCATE Table " & tbltempofitem & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert ofitem in temporary table
                sql = "Insert into " & tbltempofitem & " (ofnum, wrsnum, itemname, numbags, remarks, datecreated, createdby, datemodified, modifiedby, status, branch) values ('" & txtorf.Text & "', '" & lblorf.Text & txtwrs.Text & "', '" & Trim(cmbitem.Text) & "', '" & Val(txtbags.Text) & "', '" & txtrems.Text & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1', '" & login.branch & "')"
                command.CommandText = sql
                command.ExecuteNonQuery()
                'per items////////////////////////////////////////////////////////////////////////////

                'insert temporary tbloflog in tbltempoflog
                'per log////////////////////////////////////////////////////////////////////////////
                Dim tbltempoflog As String = "tbltempoflog" & txtofitemid.Text
                Dim tblexistoflog As Boolean = False
                sql = "Select name from sys.tables where name = '" & tbltempoflog & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistoflog = True
                End If
                dr.Dispose()

                If tblexistoflog = False Then
                    'create tbltempoflog
                    sql = "Create Table " & tbltempoflog & " (oflogid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofnum nvarchar(MAX), ofitemid	int, logsheetdate date, logsheetnum nvarchar(MAX), logticketid	int, palletnum nvarchar(MAX), selectedbags int, ticketseries nvarchar(MAX), datecreated datetime, createdby nvarchar(50), picktickets nvarchar(50))"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempoflog where lblofitemid
                    sql = "TRUNCATE Table " & tbltempoflog & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert oflog in temporary table
                For Each row As DataGridViewRow In grdlog.Rows
                    '/Dim logdate As String = grdlog.Rows(row.Index).Cells(1).Value
                    Dim lognum As String = grdlog.Rows(row.Index).Cells("lognum").Value
                    Dim pallettag As String = (grdlog.Rows(row.Index).Cells("pallettag").Value)
                    Dim selectednum As String = grdlog.Rows(row.Index).Cells("selected").Value
                    Dim logticketid As String = grdlog.Rows(row.Index).Cells("palletid").Value

                    sql = "Insert into " & tbltempoflog & " (ofnum, ofitemid, logsheetdate, logsheetnum, logticketid, palletnum, selectedbags, ticketseries, datecreated, createdby, picktickets) "
                    sql = sql & "values ('" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "','" & grdlog.Rows(row.Index).Cells("proddate").Value & "','" & lognum & "','" & logticketid & "','" & pallettag & "','" & selectednum & "','',GetDate(),'" & login.user & "', 'FALSE')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next
                'per log////////////////////////////////////////////////////////////////////////////

                ''per log picktickets////////////////////////////////////////////////////////////////
                'Dim tbltempoflogpick As String = "tbltempoflogpick" & txtofitemid.Text
                'Dim tblexistoflogpick As Boolean = False
                'sql = "Select name from sys.tables where name = '" & tbltempoflogpick & "'"
                'command.CommandText = sql
                'dr = command.ExecuteReader
                'If dr.Read Then
                '    tblexistoflogpick = True
                'End If
                'dr.Dispose()

                'If tblexistoflogpick = False Then
                '    'create tbltempoflogpick
                '    sql = "Create Table " & tbltempoflogpick & " (ofloggoodid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofid int, ofnum nvarchar(MAX), ofitemid int, palletnum nvarchar(MAX), letter nvarchar(50), gticketnum nvarchar(MAX), status int, picked int)"
                '    command.CommandText = sql
                '    command.ExecuteNonQuery()
                'Else
                '    'truncate tbltempoflogpick where lblofitemid
                '    sql = "TRUNCATE Table " & tbltempoflogpick & ""
                '    command.CommandText = sql
                '    command.ExecuteNonQuery()
                'End If

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

    Private Sub txtorf_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtorf.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If

        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            If txtorf.ReadOnly = False Then
                '/grdlog.Enabled = true
                Me.ContextMenuStrip1.Enabled = True
                viewofnum()
                viewitems()
            End If
        End If
    End Sub

    Private Sub txtwrs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtwrs.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            If txtwrs.ReadOnly = False Then
                '/grdlog.Enabled = true
                Me.ContextMenuStrip1.Enabled = True
                viewwrsnum()
                viewitems()
            End If
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        Else
            e.Handled = True
        End If
    End Sub
    Private Sub ItemsToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsToolStripButton1.Click
        If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            If txtorf.Text = "" Then
                MsgBox("Cannot set items. Search Order Fill # first.", MsgBoxStyle.Exclamation, "")
            Else
                'check if orderfill is not cancelled
                sql = "Select * from tblorderfill where ofid='" & lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                orderfillitems.ofid = lblorfid.Text
                orderfillitems.txtofnum.Text = lblorf.Text & txtorf.Text
                orderfillitems.txtwrs.Text = txtwrs.Text
                orderfillitems.viewwhse()
                orderfillitems.cmbwhse.Text = txtwhse.Text
                orderfillitems.txtcus.Text = txtcus.Text
                orderfillitems.txtref.Text = txtref.Text
                orderfillitems.txtplate.Text = txtplate.Text
                orderfillitems.txtdriver.Text = txtdriver.Text
                orderfillitems.Panel1.Enabled = False
                orderfillitems.GroupBox2.Enabled = True
                orderfillitems.ShowDialog()
                If Trim(cmbitem.Text) <> "" Then
                    cmbitem.Enabled = True
                    selectitemcmb()
                End If
                btnsearch.PerformClick()
                btnsearch.PerformClick()
            End If
        Else
            MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
        End If
    End Sub

    Private Sub UpdateToolStripButton1_Click(sender As Object, e As EventArgs) Handles UpdateToolStripButton1.Click
        If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            If txtorf.Text = "" Then
                MsgBox("Cannot set items. Search Order Fill # first.", MsgBoxStyle.Exclamation, "")
            Else
                'check if orderfill is not cancelled
                sql = "Select * from tblorderfill where ofid='" & lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                orderfillitems.txtofnum.Text = lblorf.Text & txtorf.Text
                orderfillitems.txtwrs.Text = txtwrs.Text
                orderfillitems.viewwhse()
                orderfillitems.cmbwhse.Text = txtwhse.Text
                orderfillitems.txtcus.Text = txtcus.Text
                orderfillitems.txtref.Text = txtref.Text
                orderfillitems.txtplate.Text = txtplate.Text
                orderfillitems.txtdriver.Text = txtdriver.Text
                orderfillitems.Panel1.Enabled = True
                orderfillitems.GroupBox2.Enabled = False
                orderfillitems.frm = Me.Name
                orderfillitems.ShowDialog()

                btnsearch.PerformClick()
                btnsearch.PerformClick()
            End If
        Else
            MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
        End If
    End Sub

    Private Sub grdlog_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles grdlog.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then
                If btncomplete.Text = "Selected Item Complete" Then
                    If Panelcancel.Enabled = False Then
                        rowindex = e.RowIndex
                        grdlog.ClearSelection()
                        grdlog.Rows(rowindex).Cells(e.ColumnIndex).Selected = True

                        If e.ColumnIndex = 5 Then 'cancel
                            Me.ContextMenuStrip1.Show(Cursor.Position)
                            EditNoOfSelectedBagsToolStripMenuItem.Visible = False
                            CancelToolStripMenuItem.Visible = True
                        ElseIf e.ColumnIndex = 6 Then 'selected
                            Me.ContextMenuStrip1.Show(Cursor.Position)
                            EditNoOfSelectedBagsToolStripMenuItem.Visible = True
                            CancelToolStripMenuItem.Visible = False
                        End If
                    End If
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

    Private Sub txtcancel_TextChanged(sender As Object, e As EventArgs) Handles txtcancel.TextChanged

    End Sub

    Private Sub EditNoOfSelectedBagsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditNoOfSelectedBagsToolStripMenuItem.Click
        Try
            If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                grdlog.ReadOnly = False
                For i = 0 To grdlog.Columns.Count - 1
                    grdlog.Columns(i).ReadOnly = True
                Next
                b4edit = grdlog.Rows(rowindex).Cells("selected").Value
                grdlog.Rows(rowindex).Cells("selected").ReadOnly = False
                grdlog.Rows(rowindex).Cells("selected").Style.BackColor = Color.HotPink
                grdlog.Rows(rowindex).Cells("selected").Selected = True
                grdlog.BeginEdit(True)

            Else
                MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
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

    Private Sub grdlog_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles grdlog.CellEndEdit
        If grdlog.Rows(rowindex).Cells("selected").Style.BackColor = Color.HotPink Then
            'check if numeric
            If (e.ColumnIndex = 6) Then   ' Checking numeric value for Column1 only
                If grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> Nothing Then
                    Dim value As String = grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
                    If Not Information.IsNumeric(value) Or Val(value) < 1 Or Val(value) > (grdlog.Rows(e.RowIndex).Cells("avail").Value - grdlog.Rows(e.RowIndex).Cells("cnl").Value) Then
                        MsgBox("Invalid input.", MsgBoxStyle.Exclamation, "")
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = b4edit
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                        '/grdlog.Columns(13).ReadOnly = False

                        countselected()
                    Else
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Fix(grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                        '/grdlog.Columns(13).ReadOnly = False

                        countselected()
                    End If
                End If
            End If

        ElseIf grdlog.Rows(rowindex).Cells("cnl").Style.BackColor = Color.HotPink Then
            'check if numeric
            If (e.ColumnIndex = 5) Then   ' Checking numeric value for Column1 only
                If grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> Nothing Then
                    Dim value As String = grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
                    If Not Information.IsNumeric(value) Or Val(value) < 0 Or Val(value) > (grdlog.Rows(e.RowIndex).Cells("avail").Value - grdlog.Rows(e.RowIndex).Cells("selected").Value) Then
                        MsgBox("Invalid input.", MsgBoxStyle.Exclamation, "")
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = b4edit
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                        '/grdlog.Columns(13).ReadOnly = False

                        countselected()
                    Else
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Fix(grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                        '/grdlog.Columns(13).ReadOnly = False

                        countselected()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub CancelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelToolStripMenuItem.Click
        Try
            If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                ofcnf = False
                ofcnfby = ""
                confirm.GroupBox1.Text = "Supervisor"
                confirm.ShowDialog()
                If ofcnf = True Then
                    'can cancel pallet tag #
                    Panelcancel.Enabled = True
                    lbllog.Text = grdlog.Rows(rowindex).Cells("lognum").Value
                    txtpallet.Text = grdlog.Rows(rowindex).Cells("pallettag").Value
                    Me.CancelToolStripMenuItem.Visible = False
                    Me.EditNoOfSelectedBagsToolStripMenuItem.Visible = False
                    Panel2.Enabled = False
                    txtcancel.Focus()
                    'grdlog.ReadOnly = False
                    'For i = 0 To grdlog.Columns.Count - 1
                    '    grdlog.Columns(i).ReadOnly = True
                    'Next
                    'b4edit = grdlog.Rows(rowindex).Cells("cnl").Value
                    'grdlog.Rows(rowindex).Cells("cnl").ReadOnly = False
                    'grdlog.Rows(rowindex).Cells("cnl").Style.BackColor = Color.HotPink
                    'grdlog.Rows(rowindex).Cells("cnl").Selected = True
                    'grdlog.BeginEdit(True)
                End If

            Else
                MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
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

    Private Sub btncomplete_Click(sender As Object, e As EventArgs) Handles btncomplete.Click
        Try
            If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then

                'check if orderfill is not cancelled
                sql = "Select status from tblorderfill where ofid='" & lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if ofitem is not cancelled
                sql = "Select status from tblofitem where ofitemid='" & txtofitemid.Text & "'"
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


                If grdlog.Rows.Count = 0 Then
                    MsgBox("No selected pallets.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                'check if count selected bags is equal to numbags
                If Val(lblseltotal.Text) = Val(txtbags.Text) Then

                ElseIf Val(lblseltotal.Text) < Val(txtbags.Text) Then
                    MsgBox("Number of selected bags is less than the required number of bags.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                ElseIf Val(lblseltotal.Text) > Val(txtbags.Text) Then
                    MsgBox("Number of selected bags is greater than the required number of bags.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                If grdlog.Rows.Count <> 0 Then
                    'check if may errortext sa grdlogs series cells 11
                    Dim witherror As Boolean = False
                    For Each row As DataGridViewRow In grdlog.Rows
                        If grdlog.Rows(row.Index).Cells("selected").ErrorText <> "" Then
                            MsgBox("There are some errors occured. Cannot save.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                    Next
                End If

                '/MsgBox("check muna kung valid yung mga nasa grdcancel lalo na kung galing to sa save as draft")

                'check if kulang yung bags sa grdlog 
                Dim countbags As Integer = 0
                For Each row As DataGridViewRow In grdlog.Rows
                    countbags += grdlog.Rows(row.Index).Cells("selected").Value
                Next
                If Val(txtbags.Text) > Val(countbags) Then
                    MsgBox("Number of selected bags is less than the required number of bags.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                Dim a As String = MsgBox("Are you sure " & cmbitem.Text & " is complete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbNo Then
                    Exit Sub
                End If

                'Update Complete itemssss
                ofcnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If ofcnf = True Then
                    ExecuteCompletelog(strconn)
                End If

            Else
                MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
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
    Private Sub ExecuteCompletelog(ByVal connectionString As String)
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
                'update tblofitem
                sql = "Update tblofitem set status='2', datemodified=GetDate(), modifiedby='" & login.user & "', remarks='" & Trim(txtrems.Text) & "'  where ofid='" & lblorfid.Text & "' and ofitemid='" & txtofitemid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'insert into tbloflog
                For Each row As DataGridViewRow In grdlog.Rows
                    Dim logdate As Date = CDate(grdlog.Rows(row.Index).Cells("proddate").Value)
                    Dim lognum As String = grdlog.Rows(row.Index).Cells("lognum").Value
                    Dim pallettag As String = grdlog.Rows(row.Index).Cells("pallettag").Value
                    Dim availbags As Integer = grdlog.Rows(row.Index).Cells("avail").Value
                    Dim cnlbags As Integer = grdlog.Rows(row.Index).Cells("cnl").Value
                    Dim selbags As Integer = grdlog.Rows(row.Index).Cells("selected").Value
                    Dim seltickets As String = ""
                    Dim logticketid As String = grdlog.Rows(row.Index).Cells("palletid").Value

                    sql = "Insert into tbloflog (ofid, ofnum, ofitemid, logsheetdate, logsheetnum, logticketid, palletnum, selectedbags, ticketseries, picktickets, datecreated, createdby, datemodified, modifiedby, status)"
                    sql = sql & " values ('" & lblorfid.Text & "', '" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', (Select logsheetdate from tbllogsheet where logsheetnum='" & lognum & "'), '" & lognum & "', '" & logticketid & "', '" & pallettag & "', '" & selbags & "','" & seltickets & "','FALSE', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    'update yung tbllogticket cusreserve and ofnum
                    If availbags = (selbags + cnlbags) Then
                        'sql = "Update tbllogticket set status='0', cusreserve='0', ofnum=NULL where logsheetnum='" & lognum & "' and palletnum='" & pallettag & "' and branch='" & login.branch & "'"
                        sql = "Update tbllogticket set status='0', cusreserve='0', ofnum=NULL "
                        sql = sql & " where logticketid=(Select t.logticketid from tbllogticket t right outer join tbllogsheet s on t.logsheetid=s.logsheetid "
                        sql = sql & " where s.logsheetnum='" & lognum & "' and s.branch='" & login.branch & "' and t.palletnum='" & pallettag & "')"
                    Else
                        'sql = "Update tbllogticket set cusreserve='0', ofnum=NULL where logsheetnum='" & lognum & "' and palletnum='" & pallettag & "' and cusreserve<>'1' and branch='" & login.branch & "'"
                        sql = "Update tbllogticket set cusreserve='0', ofnum=NULL "
                        sql = sql & " where logticketid=(Select t.logticketid from tbllogticket t right outer join tbllogsheet s on t.logsheetid=s.logsheetid "
                        sql = sql & " where s.logsheetnum='" & lognum & "' and s.branch='" & login.branch & "' and t.palletnum='" & pallettag & "' and t.cusreserve<>'1')"
                    End If
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                'insert into tblofloggood
                For Each row As DataGridViewRow In grdlog.Rows
                    Dim lognum As String = grdlog.Rows(row.Index).Cells("lognum").Value
                    Dim pallettag As String = grdlog.Rows(row.Index).Cells("pallettag").Value
                    Dim palletid As String = grdlog.Rows(row.Index).Cells("palletid").Value
                    Dim selected As Integer = Val(grdlog.Rows(row.Index).Cells("selected").Value)

                    For i = 0 To selected - 1
                        sql = "Insert into tblofloggood (ofid, ofnum, ofitemid, oflogid, logsheetnum, palletnum, tickettype, letter, gticketnum, remarks, status)"
                        sql = sql & " values ('" & lblorfid.Text & "', '" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', (Select oflogid from tbloflog where ofnum='" & lblorf.Text & txtorf.Text & "' and logsheetnum='" & lognum & "' and palletnum='" & pallettag & "' and status<>'3'), '" & lognum & "', '" & pallettag & "', '" & lbltype.Text & "', null, null, '', '1')"
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    Next

                    'tblloggood
                    'update tblloggood where ticketnum
                    sql = "Update tblloggood set status='0', ofid='" & lblorfid.Text & "'"
                    sql = sql & " where loggoodid In (Select TOP " & selected & " loggoodid from tblloggood where logticketid='" & palletid & "' And status='1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                'insert into tbloflogcancel
                For Each row As DataGridViewRow In grdcancel.Rows
                    Dim lognum As String = grdcancel.Rows(row.Index).Cells("clog").Value
                    Dim pallettag As String = grdcancel.Rows(row.Index).Cells("cpallet").Value
                    Dim cnl As Integer = grdcancel.Rows(row.Index).Cells("cqty").Value
                    Dim tickettime As String = grdcancel.Rows(row.Index).Cells("ctime").Value
                    Dim disposition As String = grdcancel.Rows(row.Index).Cells("cdispo").Value
                    Dim cancelby As String = grdcancel.Rows(row.Index).Cells("cby").Value

                    For i = 0 To cnl - 1
                        sql = "Insert into tbloflogcancel (ofid, ofnum, ofitemid, oflogid, logsheetnum, palletnum, ticketid, tickettype, letter, cticketnum, cticketdate, remarks, cancelby, status)"
                        sql = sql & " values ('" & lblorfid.Text & "', '" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', (Select oflogid from tbloflog where ofnum='" & lblorf.Text & txtorf.Text & "' and logsheetnum='" & lognum & "' and palletnum='" & pallettag & "' and status<>'3'),"
                        sql = sql & "'" & lognum & "','" & pallettag & "', null, '" & lbltype.Text & "', null, null, '" & tickettime & "', '" & disposition & "','" & cancelby & "','1')"
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    Next

                    'tblloggood
                    'update tblloggood where ticketnum
                    sql = "Update tblloggood set status='0', ofid='" & lblorfid.Text & "'"
                    sql = sql & " where loggoodid In (Select TOP " & cnl & " loggoodid from tblloggood where palletnum='" & pallettag & "' And status='1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next


                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully complete Item " & Trim(cmbitem.Text) & ".", MsgBoxStyle.Information, "")
                refreshitem()
                grdlog.Rows.Clear()
                branofpallets.grdselect.Rows.Clear()
                grdcancel.Rows.Clear()

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.ToString, MsgBoxStyle.Exclamation, "")
                ' Attempt to roll back the transaction. 
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

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Try
            'refresh lahat mula sa orderfill, wrs details then refershtickets then viewselectedpallets
            '/grdlog.Enabled = true
            Me.ContextMenuStrip1.Enabled = True
            viewofnum()
            viewitems()
            btnsearch.PerformClick()
            btnsearch.PerformClick()


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

    Private Sub refreshitem()
        Try
            Dim cpleted As Boolean = True
            sql = "Select ofitemid,numbags,status from tblofitem where ofnum='" & lblorf.Text & txtorf.Text & "' and itemname='" & Trim(cmbitem.Text) & "' and status<>'3' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                txtofitemid.Text = dr("ofitemid")
                txtbags.Text = dr("numbags")
                Panelbuttons.Enabled = True
                Me.CancelToolStripMenuItem.Visible = True

                If dr("status") = 2 Then
                    'meaning confirm for selected item only
                    btncomplete.Text = "Selected Item Completed"
                    btncomplete.Enabled = False
                    btnpallet.Enabled = False
                    btnrefresh.Enabled = False
                    btnprint.Enabled = False
                    If btnconfirm.Text <> "Confirmed Order Fill" Then
                        btnconfirm.Enabled = True
                    Else
                        btnprint.Enabled = True
                    End If
                    btnsave.Enabled = False
                Else
                    cpleted = False
                End If
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If cpleted = False Then
                '===viewsavetbltempsperitem()
                viewitemlogs()

                btncomplete.Enabled = True
                btncomplete.Text = "Selected Item Complete"
                btnpallet.Enabled = True
                btnrefresh.Enabled = True
                btnconfirm.Enabled = False
                btnprint.Enabled = False
                btnsave.Enabled = True
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

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                'check if orderfill is not cancelled
                sql = "Select * from tblorderfill where ofid='" & lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if ofitem is not cancelled
                sql = "Select * from tblofitem where ofitemid='" & txtofitemid.Text & "'"
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

                'check if may selected na item or i disable kung walang selected na item

                'check if may nagenerate ba
                If grdlog.Rows.Count = 0 Then
                    Dim a As String = MsgBox("No record found. Generate first. Do you want to save it anyway?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                    If a <> vbYes Then
                        Exit Sub
                    End If
                Else
                    'check if may errortext sa grdlogs series cells 11
                    Dim witherror As Boolean = False
                    For Each row As DataGridViewRow In grdlog.Rows
                        If grdlog.Rows(row.Index).Cells("selected").ErrorText <> "" Then
                            MsgBox("There are some errors occured. Cannot save.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                    Next
                End If


                'password
                ofcnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If ofcnf = True Then
                    ExecuteSaveasDraft(strconn)
                End If

            Else
                MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
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
    Private Sub ExecuteSaveasDraft(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                'gawing 0 - in process yung status ng tblofitem
                sql = "Update tblofitem set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where ofitemid='" & txtofitemid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'saving\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                'where ofitemid=lblofitemid.text

                'insert temporary tblofitem in tbltempofitem1
                'per items////////////////////////////////////////////////////////////////////////////
                Dim tbltempofitem As String = "tbltempofitem" & txtofitemid.Text
                Dim tblexistofitem As Boolean = False
                sql = "Select name from sys.tables where name = '" & tbltempofitem & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistofitem = True
                End If
                dr.Dispose()

                If tblexistofitem = False Then
                    'create tbltempofitem1
                    sql = "Create Table " & tbltempofitem & " (ofitemid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofnum nvarchar(MAX), wrsnum nvarchar(MAX), itemname nvarchar(50), numbags int, oftype nvarchar(50), remarks nvarchar(MAX), datecreated datetime, createdby nvarchar(50), datemodified datetime, modifiedby nvarchar(50), status int, branch nvarchar(50))"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempofitem1 where lblofitemid
                    sql = "TRUNCATE Table " & tbltempofitem & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert ofitem in temporary table
                sql = "Insert into " & tbltempofitem & " (ofnum, wrsnum, itemname, numbags, remarks, datecreated, createdby, datemodified, modifiedby, status, branch)"
                sql = sql & " values ('" & txtorf.Text & "', '" & lblorf.Text & txtwrs.Text & "', '" & Trim(cmbitem.Text) & "', '" & Val(txtbags.Text) & "', '" & txtrems.Text & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1', '" & login.branch & "')"
                command.CommandText = sql
                command.ExecuteNonQuery()
                'per items////////////////////////////////////////////////////////////////////////////

                'insert temporary tbloflog in tbltempoflog
                'per log////////////////////////////////////////////////////////////////////////////
                Dim tbltempoflog As String = "tbltempoflog" & txtofitemid.Text
                Dim tblexistoflog As Boolean = False
                sql = "Select name from sys.tables where name = '" & tbltempoflog & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistoflog = True
                End If
                dr.Dispose()

                If tblexistoflog = False Then
                    'create tbltempoflog
                    sql = "Create Table " & tbltempoflog & " (oflogid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofnum nvarchar(MAX), ofitemid	int, logsheetdate date, logsheetnum nvarchar(MAX), logticketid	int, palletnum nvarchar(MAX), selectedbags int, ticketseries nvarchar(MAX), datecreated datetime, createdby nvarchar(50), picktickets nvarchar(50))"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempoflog where lblofitemid
                    sql = "TRUNCATE Table " & tbltempoflog & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert oflog in temporary table
                For Each row As DataGridViewRow In grdlog.Rows
                    '/Dim logdate As String = grdlog.Rows(row.Index).Cells("proddate").Value
                    Dim lognum As String = grdlog.Rows(row.Index).Cells("lognum").Value
                    Dim pallettag As String = (grdlog.Rows(row.Index).Cells("pallettag").Value)
                    Dim selectednum As String = grdlog.Rows(row.Index).Cells("selected").Value
                    Dim seriesnum As String = ""
                    Dim logticketid As String = grdlog.Rows(row.Index).Cells("palletid").Value

                    sql = "Insert into " & tbltempoflog & " (ofnum, ofitemid, logsheetdate, logsheetnum, logticketid, palletnum, selectedbags, ticketseries, datecreated, createdby, picktickets) "
                    sql = sql & "values ('" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "','" & grdlog.Rows(row.Index).Cells("proddate").Value & "','" & lognum & "','" & logticketid & "','" & pallettag & "','" & selectednum & "','" & seriesnum & "',GetDate(),'" & login.user & "', 'False')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next
                'per log////////////////////////////////////////////////////////////////////////////

                ''per log picktickets////////////////////////////////////////////////////////////////
                'Dim tbltempoflogpick As String = "tbltempoflogpick" & txtofitemid.Text
                'Dim tblexistoflogpick As Boolean = False
                'sql = "Select name from sys.tables where name = '" & tbltempoflogpick & "'"
                'command.CommandText = sql
                'dr = command.ExecuteReader
                'If dr.Read Then
                '    tblexistoflogpick = True
                'End If
                'dr.Dispose()

                'If tblexistoflogpick = False Then
                '    'create tbltempoflogpick
                '    sql = "Create Table " & tbltempoflogpick & " (ofloggoodid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofid int, ofnum nvarchar(MAX), ofitemid int, palletnum nvarchar(MAX), letter nvarchar(50), gticketnum nvarchar(MAX), status int, picked int)"
                '    command.CommandText = sql
                '    command.ExecuteNonQuery()
                'Else
                '    'truncate tbltempoflogpick where lblofitemid
                '    sql = "TRUNCATE Table " & tbltempoflogpick & ""
                '    command.CommandText = sql
                '    command.ExecuteNonQuery()
                'End If

                ''insert oflog in temporary table
                'For Each row As DataGridViewRow In grdlog.Rows
                '    If Val(grdlog.Rows(row.Index).Cells(6).Value.ToString) = 1 Then
                '        Dim pallettag As String = grdlog.Rows(row.Index).Cells("pallettag").Value
                '        Dim letter As String = ""
                '        Dim ticknum As String = ""
                '        Dim picked As Integer = 1
                '        Dim selected As Integer = grdlog.Rows(row.Index).Cells("selected").Value

                '        If selected > 0 Then
                '            For i = 0 To selected - 1
                '                sql = "Insert into " & tbltempoflogpick & " (ofid, ofnum, ofitemid, palletnum, letter, gticketnum, status, picked) "
                '                sql = sql & "values ('" & lblorfid.Text & "','" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', '" & pallettag & "', '" & letter & "', '" & ticknum & "', '1', '" & picked & "')"
                '                command.CommandText = sql
                '                command.ExecuteNonQuery()
                '            Next
                '        End If
                '    End If
                'Next
                ''per log picktickets////////////////////////////////////////////////////////////////

                'insert temporary tbloflogcancel in tbltempoflogcancel
                'per logcancel////////////////////////////////////////////////////////////////////////////
                Dim tbltempoflogcancel As String = "tbltempoflogcancel" & txtofitemid.Text
                Dim tblexistoflogcancel As Boolean = False
                sql = "Select name from sys.tables where name = '" & tbltempoflogcancel & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistoflogcancel = True
                End If
                dr.Dispose()

                If tblexistoflogcancel = False Then
                    'create tbltempoflogcancel
                    sql = "Create Table " & tbltempoflogcancel & " (oflogcancelid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofnum nvarchar(MAX), ofitemid	int, oflogid int, logsheetnum nvarchar(MAX), palletnum nvarchar(MAX), ticketid int, letter nvarchar(50), cticketnum int, cticketdate datetime, remarks	nvarchar(MAX), cancelby nvarchar(50), status int)"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempoflogcancel where lblofitemid
                    sql = "TRUNCATE Table " & tbltempoflogcancel & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert oflogcancel in temporary table
                For Each row As DataGridViewRow In grdcancel.Rows
                    Dim lognum As String = grdcancel.Rows(row.Index).Cells("clog").Value
                    Dim pallettag As String = (grdcancel.Rows(row.Index).Cells("cpallet").Value)
                    Dim ticketnum As String = grdcancel.Rows(row.Index).Cells("cqty").Value
                    Dim tickettime As String = grdcancel.Rows(row.Index).Cells("ctime").Value
                    Dim disposition As String = grdcancel.Rows(row.Index).Cells("cdispo").Value
                    Dim cancelby As String = grdcancel.Rows(row.Index).Cells("cby").Value

                    sql = "Insert into " & tbltempoflogcancel & " (ofnum, ofitemid, oflogid, logsheetnum, palletnum, ticketid, letter, cticketnum, cticketdate, remarks, cancelby, status)"
                    sql = sql & " values ('" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', (Select oflogid from tbloflog where ofnum='" & lblorf.Text & txtorf.Text & "' and logsheetnum='" & lognum & "'),'" & lognum & "','" & pallettag & "', null, null,'" & ticketnum & "','" & tickettime & "','" & disposition & "','" & cancelby & "','1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                'per logcancel////////////////////////////////////////////////////////////////////////////

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully Saved Order Fill# " & lblorf.Text & txtorf.Text & ".", MsgBoxStyle.Information, "")

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

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        If Trim(txtpallet.Text) <> "" And Trim(txtcancel.Text) <> "" And Trim(txtreason.Text) <> "" Then
            'check kung yung qty is valid
            For Each row As DataGridViewRow In grdlog.Rows
                Dim rlognum As String = grdlog.Rows(row.Index).Cells("lognum").Value
                Dim rpallet As String = grdlog.Rows(row.Index).Cells("pallettag").Value
                Dim rqty As Integer = 0

                If rlognum = lbllog.Text And rpallet = Trim(txtpallet.Text) Then
                    If Val(Trim(txtcancel.Text)) + grdlog.Rows(row.Index).Cells("cnl").Value > (grdlog.Rows(row.Index).Cells("avail").Value - grdlog.Rows(row.Index).Cells("selected").Value) Then
                        MsgBox("Invalid input.", MsgBoxStyle.Exclamation, "")
                        txtcancel.Focus()
                        Exit Sub
                    ElseIf Val(Trim(txtcancel.Text)) < 1 Then
                        MsgBox("Invalid input.", MsgBoxStyle.Exclamation, "")
                        txtcancel.Focus()
                        Exit Sub
                    End If

                    grdcancel.Rows.Add(lbllog.Text, Trim(txtpallet.Text), Trim(txtcancel.Text), login.datenow, Trim(txtreason.Text), ofcnfby)

                    For Each jrow As DataGridViewRow In grdcancel.Rows
                        Dim jlognum As String = grdcancel.Rows(jrow.Index).Cells("clog").Value
                        Dim jpallet As String = grdcancel.Rows(jrow.Index).Cells("cpallet").Value
                        Dim jqty As Integer = grdcancel.Rows(jrow.Index).Cells("cqty").Value

                        If rlognum = jlognum And rpallet = jpallet Then
                            rqty += jqty
                        End If
                    Next

                    grdlog.Rows(row.Index).Cells("cnl").Value = rqty
                    Exit For
                End If
            Next

            countselected()

            txtcancel.Text = ""
            txtcancel.Focus()
        End If
    End Sub

    Private Sub txtcancel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcancel.KeyPress
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnadd.PerformClick()
        ElseIf Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 68 Or Asc(e.KeyChar) = 100 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click
        Me.CancelToolStripMenuItem.Visible = True
        Me.EditNoOfSelectedBagsToolStripMenuItem.Visible = True
        txtpallet.Text = ""
        txtcancel.Text = ""
        txtreason.Text = ""
        Panelcancel.Enabled = False
        Panel2.Enabled = True
    End Sub

    Private Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        txtcancel.Text = ""
        txtcancel.Focus()
        txtreason.Text = ""
    End Sub

    Private Sub btnremove_Click(sender As Object, e As EventArgs) Handles btnremove.Click
        For Each row As DataGridViewRow In grdcancel.SelectedRows
            Dim cmbindex As Integer = row.Index
            grdcancel.Rows.Remove(row)
        Next

        countcnl()
        countselected()
    End Sub

    Private Sub countcnl()
        For Each row As DataGridViewRow In grdlog.Rows
            Dim rlognum As String = grdlog.Rows(row.Index).Cells("lognum").Value
            Dim rpallet As String = grdlog.Rows(row.Index).Cells("pallettag").Value
            Dim rqty As Integer = 0

            For Each jrow As DataGridViewRow In grdcancel.Rows
                Dim jlognum As String = grdcancel.Rows(jrow.Index).Cells("clog").Value
                Dim jpallet As String = grdcancel.Rows(jrow.Index).Cells("cpallet").Value
                Dim jqty As Integer = grdcancel.Rows(jrow.Index).Cells("cqty").Value

                If rlognum = jlognum And rpallet = jpallet Then
                    rqty += jqty
                End If
            Next

            grdlog.Rows(row.Index).Cells("cnl").Value = rqty
        Next
    End Sub
    Private Sub btnconfirm_Click(sender As Object, e As EventArgs) Handles btnconfirm.Click
        Try
            If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                'check if orderfill is not cancelled
                sql = "Select status from tblorderfill where ofid='" & lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If grdlog.Rows.Count <> 0 Then
                    'check if may errortext sa grdlogs series cells 11
                    Dim witherror As Boolean = False
                    For Each row As DataGridViewRow In grdlog.Rows
                        If grdlog.Rows(row.Index).Cells("selected").ErrorText <> "" Then
                            MsgBox("There are some errors occured. Cannot confirm.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                    Next
                End If


                If btncomplete.Enabled = False Then
                    sql = "Select status from tblofitem where ofid='" & lblorfid.Text & "' and status<>'2' and status<>'3'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MsgBox("Cannot confirm orderfill. Some items are not yet completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If

                sql = "Select status from tblofitem where ofid='" & lblorfid.Text & "' and status='2'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                Else
                    MsgBox("Cannot confirm orderfill. Some items are not yet completed.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if other details are completed
                sql = "Select status from tblorderfill where ofid='" & lblorfid.Text & "' and ((forklift='' or forklift is NULL))"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Complete other details first.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                ofcnf = False
                ofcnfby = ""
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If ofcnf = True Then
                    ExecuteConfirm(strconn)
                End If

            Else
                MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
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
    Private Sub ExecuteConfirm(ByVal connectionString As String)
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

                'check if wala ng tblofitem na status is 1
                sql = "Select status from tblofitem where ofid='" & lblorfid.Text & "' and (status='1' or status='0')"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    MsgBox("Cannot confirm order fill# " & dr("ofnum") & ". " & dr("itemname") & " is still pending.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()

                'update tblorderfill
                sql = "Update tblorderfill set status='2', completedby='" & ofcnfby & "', completed=GetDate() where ofid='" & lblorfid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                '/deletetemp()

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully confirmed order fill# " & lblorf.Text & txtorf.Text & ".", MsgBoxStyle.Information, "")
                '/defaultform()
                '/btnsearch.PerformClick()
                btnprint.Enabled = True
                btnprint.PerformClick()
                btnprint.Enabled = False
                btnconfirm.Enabled = False
                btnothers.Enabled = False

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.ToString, MsgBoxStyle.Exclamation, "")
                ' Attempt to roll back the transaction. 
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

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        Try
            'print order fill
            rptorderfill.stat = ""
            rptorderfill.ofnum = lblorf.Text & txtorf.Text
            rptorderfill.ofid = lblorfid.Text
            rptorderfill.ShowDialog()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class