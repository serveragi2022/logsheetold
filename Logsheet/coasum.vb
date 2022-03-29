Imports System.IO
Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class coasum
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim selectedrow As Integer, clickbtn As String, gridsql As String, logbranch As String, loginwhse As String

    Private threadEnabled As Boolean, threadEnabledsteps As Boolean, threadEnableddestin As Boolean
    Private backgroundWorker As BackgroundWorker
    Private backgroundWorkersteps As BackgroundWorker
    Private backgroundWorkerdestin As BackgroundWorker

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

    Private Sub coasum_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewcus()
        viewwhse()
        viewshift()
        logbranch = login.branch
        btnview.PerformClick()
    End Sub

    Public Sub viewcus()
        Try
            cmbcus.Items.Clear()
            cmbcus.Items.Add("")

            sql = "Select * from tblcustomer where status='1' order by customer"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbcus.Items.Add(dr("customer"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

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

    Public Sub viewwhse()
        Try
            cmbwhse.Items.Clear()
            cmbwhse.Items.Add("")

            sql = "Select * from tblwhse where status='1' and branch='" & login.branch & "' order by whsename"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbwhse.Items.Add(dr("whsename"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If cmbwhse.Items.Count <> 0 Then
                cmbwhse.Items.Add("All")
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

    Public Sub viewshift()
        Try
            cmbshift.Items.Clear()
            cmbshift.Items.Add("")

            sql = "Select * from tblshift where status='1' order by shift"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbshift.Items.Add(dr("shift"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If cmbshift.Items.Count <> 0 Then
                cmbshift.Items.Add("All")
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

    Private Sub cmbwhse_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbwhse.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbwhse_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbwhse.Leave
        Try
            If Trim(cmbwhse.Text) <> "" Then
                If Not cmbwhse.Items.Contains(Trim(cmbwhse.Text.ToUpper)) Then
                    cmbwhse.Text = ""
                Else
                    sql = "Select * from tblwhse where whsename='" & Trim(cmbwhse.Text.ToUpper) & "' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If dr("status") = 1 Then
                            cmbwhse.SelectedItem = dr("whsename")
                        Else
                            MsgBox(cmbwhse.Text.ToUpper & " is already deactivated.", MsgBoxStyle.Information, "")
                            cmbwhse.SelectedItem = ""
                        End If
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Else
                cmbwhse.SelectedItem = ""
            End If

            Me.Cursor = Cursors.Default
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

    Private Sub cmbwhse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbwhse.SelectedIndexChanged

    End Sub

    Private Sub cmbshift_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbshift.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbshift_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbshift.Leave
        Try
            If Trim(cmbshift.Text) <> "" Then
                If Not cmbshift.Items.Contains(Trim(cmbshift.Text.ToUpper)) Then
                    cmbshift.Text = ""
                Else
                    sql = "Select * from tblshift where shift='" & Trim(cmbshift.Text.ToUpper) & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If dr("status") = 1 Then
                            cmbshift.SelectedItem = dr("shift")
                        Else
                            MsgBox(cmbshift.Text.ToUpper & " is already deactivated.", MsgBoxStyle.Information, "")
                            cmbshift.SelectedItem = ""
                        End If
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Else
                cmbshift.SelectedItem = ""
            End If

            Me.Cursor = Cursors.Default
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

    Private Sub cmbshift_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbshift.SelectedIndexChanged

    End Sub

    Private Sub btnview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnview.Click
        cmbwhse.Text = ""
        cmbshift.Text = ""
        txtcoanum.Text = ""
        viewpending()
    End Sub

    Public Sub viewpending()
        Try
            clickbtn = "Pending"

            sql = "Select tblcoa.*, tblorderfill.customer, tblorderfill.cusref, tblorderfill.platenum, tblorderfill.ofnum from tblcoa"
            sql = sql & " INNER JOIN tblorderfill on tblcoa.ofnum=tblorderfill.ofnum"
            sql = sql & " where tblcoa.status='1' and tblcoa.branch='" & login.branch & "' and tblorderfill.branch='" & login.branch & "'"
            sql = sql & " order by coadate"

            gridsql = sql

            grdcoa.Rows.Clear()

            backgroundWorker = New BackgroundWorker()
            backgroundWorker.WorkerSupportsCancellation = True
            backgroundWorkersteps = New BackgroundWorker()
            backgroundWorkersteps.WorkerSupportsCancellation = True

            backgroundWorker = New BackgroundWorker()
            AddHandler backgroundWorker.DoWork, New DoWorkEventHandler(AddressOf backgroundWorker_DoWork)
            AddHandler backgroundWorker.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf backgroundWorker_Completed)
            AddHandler backgroundWorker.ProgressChanged, New ProgressChangedEventHandler(AddressOf backgroundWorker_ProgressChanged)
            m_addRowDelegate = New AddRowDelegate(AddressOf AddDGVRow)

            AddHandler backgroundWorkersteps.DoWork, New DoWorkEventHandler(AddressOf backgroundWorkersteps_DoWork)
            AddHandler backgroundWorkersteps.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf backgroundWorkersteps_Completed)
            AddHandler backgroundWorkersteps.ProgressChanged, New ProgressChangedEventHandler(AddressOf backgroundWorkersteps_ProgressChanged)
            m_updateRowDelegate = New UpdateRowDelegate(AddressOf UpdateDGVRow)

            If Not backgroundWorker.IsBusy Then
                backgroundWorker.WorkerReportsProgress = True
                backgroundWorker.WorkerSupportsCancellation = True
                backgroundWorker.RunWorkerAsync() 'start ng select query
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

    Private Sub backgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
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
            Dim stat As String = ""
            If drx("status") = 1 Then
                stat = "In Process"
            ElseIf drx("status") = 2 Then
                stat = "Completed"
            ElseIf drx("status") = 3 Then
                stat = "Cancelled"
            End If

            If grdcoa.InvokeRequired Then
                grdcoa.Invoke(m_addRowDelegate, drx("coaid"), Format(drx("coadate"), "yyyy/MM/dd"), drx("coanum"), drx("ofnum"), drx("customer").ToString, drx("coacustomer").ToString, drx("cusref").ToString, drx("ofitemid"), drx("itemname"), 0, drx("batchnum"), drx("platenum").ToString, drx("loaddate"), drx("deldate"), drx("whsename"), drx("shift"), stat, drx("remarks").ToString, drx("datecreated"), drx("createdby"), drx("datemodified"), drx("modifiedby"), drx("canceldate").ToString, drx("cancelby").ToString, drx("cancelreason").ToString, drx("coaform").ToString, i)
            Else
                AddDGVRow(drx("coaid"), Format(drx("coadate"), "yyyy/MM/dd"), drx("coanum"), drx("ofnum"), drx("customer").ToString, drx("coacustomer").ToString, drx("cusref").ToString, drx("ofitemid"), drx("itemname"), 0, drx("batchnum"), drx("platenum").ToString, drx("loaddate"), drx("deldate"), drx("whsename"), drx("shift"), stat, drx("remarks").ToString, drx("datecreated"), drx("createdby"), drx("datemodified"), drx("modifiedby"), drx("canceldate").ToString, drx("cancelby").ToString, drx("cancelreason").ToString, drx("coaform").ToString, i)
            End If

            i += 1
            backgroundWorker.ReportProgress(i) '/ idivide kung ilan ang total

        End While
        drx.Dispose()
        cmd.Dispose()
        connection.Close()
    End Sub

    Delegate Sub AddRowDelegate(ByVal v0 As Object, ByVal v1 As Object, ByVal v2 As Object, ByVal v3 As Object, ByVal v4 As Object, ByVal v5 As Object, ByVal v6 As Object, ByVal v7 As Object, ByVal v8 As Object, ByVal v9 As Object, ByVal v10 As Object, ByVal v11 As Object, ByVal v12 As Object, ByVal v13 As Object, ByVal v14 As Object, ByVal v15 As Object, ByVal v16 As Object, ByVal v17 As Object, ByVal v18 As Object, ByVal v19 As Object, ByVal v20 As Object, ByVal v21 As Object, ByVal v22 As Object, ByVal v23 As Object, ByVal v24 As Object, ByVal v25 As Object, ByVal vrow As Object)
    Private m_addRowDelegate As AddRowDelegate

    Private Sub AddDGVRow(ByVal v0 As Object, ByVal v1 As Object, ByVal v2 As Object, ByVal v3 As Object, ByVal v4 As Object, ByVal v5 As Object, ByVal v6 As Object, ByVal v7 As Object, ByVal v8 As Object, ByVal v9 As Object, ByVal v10 As Object, ByVal v11 As Object, ByVal v12 As Object, ByVal v13 As Object, ByVal v14 As Object, ByVal v15 As Object, ByVal v16 As Object, ByVal v17 As Object, ByVal v18 As Object, ByVal v19 As Object, ByVal v20 As Object, ByVal v21 As Object, ByVal v22 As Object, ByVal v23 As Object, ByVal v24 As Object, ByVal v25 As Object, ByVal vrow As Integer)
        If threadEnabled = True Then
            If grdcoa.InvokeRequired Then
                grdcoa.BeginInvoke(New AddRowDelegate(AddressOf AddDGVRow), v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, vrow)
            Else
                grdcoa.Rows.Add(v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25)
            End If
        End If
    End Sub

    Private Sub backgroundWorker_Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        '/ProgressBar1.Visible = False
        If Not backgroundWorker.IsBusy Then
            backgroundWorkersteps.WorkerReportsProgress = True
            backgroundWorkersteps.WorkerSupportsCancellation = True
            backgroundWorkersteps.RunWorkerAsync()
        End If
    End Sub

    Private Sub backgroundWorker_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        '/Me.Label12.Text = e.ProgressPercentage.ToString() & "% complete"
        lblloading.Visible = True
        Panel1.Enabled = False
        pgb1.Style = ProgressBarStyle.Marquee
        pgb1.Visible = True
        pgb1.Minimum = 0
    End Sub

    Private Sub backgroundWorkersteps_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        threadEnabledsteps = True
        Dim i As Integer = 0
        'status ng logsheet kung admin pending confirmation nlng or in process
        'ofitemid-7, numbags-9,
        Dim connection As SqlConnection
        For Each row As DataGridViewRow In grdcoa.Rows
            Dim ofitemidnum As String = grdcoa.Rows(row.Index).Cells(7).Value

            sql = "Select numbags from tblofitem where ofitemid='" & ofitemidnum & "' and branch='" & logbranch & "'"
            connection = New SqlConnection
            connection.ConnectionString = strconn
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            cmd = New SqlCommand(sql, connection)
            Dim drx As SqlDataReader = cmd.ExecuteReader
            If drx.Read Then
                UpdateDGVRow(drx("numbags"), i)
            End If
            dr.Dispose()
            cmd.Dispose()
            connection.Close()

            i += 1

            backgroundWorkersteps.ReportProgress(i * 100 / 155) '/ idivide kung ilan ang total
        Next
    End Sub

    Delegate Sub UpdateRowDelegate(ByVal valueindex As Object, ByVal value1 As Object)
    Private m_updateRowDelegate As UpdateRowDelegate

    Private Sub UpdateDGVRow(ByVal vstat As Integer, ByVal rowin As Integer)
        If threadEnabledsteps = True Then
            If grdcoa.InvokeRequired Then
                grdcoa.BeginInvoke(New UpdateRowDelegate(AddressOf UpdateDGVRow), vstat, rowin)
            Else
                grdcoa.Rows(rowin).Cells(9).Value = vstat
            End If
        End If
    End Sub

    Private Sub backgroundWorkersteps_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        '/Me.Label2.Text = e.ProgressPercentage.ToString() & "% complete"
    End Sub

    Private Sub backgroundWorkersteps_Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Me.Cursor = Cursors.Default
        pgb1.Visible = False
        pgb1.Style = ProgressBarStyle.Blocks
        lblloading.Visible = False
        Panel1.Enabled = True
        If e.Error IsNot Nothing Then
            MsgBox(e.Error.ToString, MsgBoxStyle.Critical, "")
        ElseIf e.Cancelled = True Then
            MsgBox("Operation is cancelled.", MsgBoxStyle.Exclamation, "")
        Else
            If grdcoa.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Information, "")
            Else
                grdcoa.SuspendLayout()
                grdcoa.ResumeLayout()

                If clickbtn = "Pending" Then
                    MsgBox("Loading data completed.", MsgBoxStyle.Information, "")
                End If
            End If
        End If
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            clickbtn = "Search"

            If Trim(txtcoanum.Text) <> "" Then
                '/coanumsearch()
                '/Me.Cursor = Cursors.Default
                '/Exit Sub
                sql = "Select tblcoa.*, tblorderfill.customer, tblorderfill.cusref, tblorderfill.platenum, tblorderfill.ofnum from tblcoa"
                sql = sql & " INNER JOIN tblorderfill on tblcoa.ofnum=tblorderfill.ofnum"
                sql = sql & " and tblcoa.coanum='" & lblcoa.Text & Trim(txtcoanum.Text) & "' and tblcoa.branch='" & login.branch & "' and tblorderfill.branch='" & login.branch & "'"
            ElseIf Trim(txtcoaid.Text) <> "" Then
                '/coaidsearch()
                '/Me.Cursor = Cursors.Default
                '/Exit Sub
                sql = "Select tblcoa.*, tblorderfill.customer, tblorderfill.cusref, tblorderfill.platenum, tblorderfill.ofnum from tblcoa"
                sql = sql & " INNER JOIN tblorderfill on tblcoa.ofnum=tblorderfill.ofnum"
                sql = sql & " and tblcoa.coaid='" & Trim(txtcoaid.Text) & "' and tblcoa.branch='" & login.branch & "' and tblorderfill.branch='" & login.branch & "'"
            ElseIf Trim(txtwrs.Text) <> "" Then
                '/wrssearch()
                sql = "Select tblcoa.*, tblorderfill.customer, tblorderfill.cusref, tblorderfill.platenum, tblorderfill.ofnum from tblcoa"
                sql = sql & " INNER JOIN tblorderfill on tblcoa.ofnum=tblorderfill.ofnum"
                sql = sql & " and tblorderfill.wrsnum='" & Trim(txtwrs.Text) & "' and tblcoa.branch='" & login.branch & "' and tblorderfill.branch='" & login.branch & "'"
            ElseIf Trim(cmbcus.Text) <> "" Then
                '/cussearch()
                sql = "Select tblcoa.*, tblorderfill.customer, tblorderfill.cusref, tblorderfill.platenum, tblorderfill.ofnum from tblcoa"
                sql = sql & " INNER JOIN tblorderfill on tblcoa.ofnum=tblorderfill.ofnum"
                sql = sql & " and tblcoa.coacustomer='" & Trim(cmbcus.Text) & "' and tblcoa.branch='" & login.branch & "' and tblorderfill.branch='" & login.branch & "'"
            Else
                sql = "Select tblcoa.*, tblorderfill.customer, tblorderfill.cusref, tblorderfill.platenum, tblorderfill.ofnum from tblcoa"
                sql = sql & " INNER JOIN tblorderfill on tblcoa.ofnum=tblorderfill.ofnum"
                sql = sql & " where coadate>='" & Format(datefrom.Value, "yyyy/MM/dd") & "' and coadate<='" & Format(dateto.Value, "yyyy/MM/dd") & "'"
                sql = sql & " and tblcoa.branch='" & login.branch & "' and tblorderfill.branch='" & login.branch & "'"

                If cmbwhse.Text <> "" Then
                    sql = sql & " and tblcoa.whsename='" & cmbwhse.Text & "'"
                End If

                If cmbshift.Text <> "" Then
                    sql = sql & " and tblcoa.shift='" & cmbshift.Text & "'"
                End If

                If chkhide.Checked = True Then
                    sql = sql & " and tblcoa.status<>'3'"
                End If

                sql = sql & " order by coadate"
            End If

            gridsql = sql

            grdcoa.Rows.Clear()

            backgroundWorker = New BackgroundWorker()
            backgroundWorker.WorkerSupportsCancellation = True
            backgroundWorkersteps = New BackgroundWorker()
            backgroundWorkersteps.WorkerSupportsCancellation = True

            backgroundWorker = New BackgroundWorker()
            AddHandler backgroundWorker.DoWork, New DoWorkEventHandler(AddressOf backgroundWorker_DoWork)
            AddHandler backgroundWorker.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf backgroundWorker_Completed)
            AddHandler backgroundWorker.ProgressChanged, New ProgressChangedEventHandler(AddressOf backgroundWorker_ProgressChanged)
            m_addRowDelegate = New AddRowDelegate(AddressOf AddDGVRow)

            AddHandler backgroundWorkersteps.DoWork, New DoWorkEventHandler(AddressOf backgroundWorkersteps_DoWork)
            AddHandler backgroundWorkersteps.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf backgroundWorkersteps_Completed)
            AddHandler backgroundWorkersteps.ProgressChanged, New ProgressChangedEventHandler(AddressOf backgroundWorkersteps_ProgressChanged)
            m_updateRowDelegate = New UpdateRowDelegate(AddressOf UpdateDGVRow)

            If Not backgroundWorker.IsBusy Then
                backgroundWorker.WorkerReportsProgress = True
                backgroundWorker.WorkerSupportsCancellation = True
                backgroundWorker.RunWorkerAsync() 'start ng select query
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

    Private Sub datefrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles datefrom.ValueChanged
        dateto.MinDate = datefrom.Value
    End Sub

    Public Sub coanumsearch()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            grdcoa.Rows.Clear()

            clickbtn = "Search"

            sql = "Select * from tblcoa where coanum='" & lblcoa.Text & Trim(txtcoanum.Text) & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "In Process"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If

                grdcoa.Rows.Add(dr("coaid"), Format(dr("coadate"), "yyyy/MM/dd"), dr("coanum"), dr("ofnum"), "", dr("coacustomer").ToString, "", dr("ofitemid"), dr("itemname"), 0, dr("batchnum"), "", dr("loaddate"), dr("deldate"), dr("whsename"), dr("shift"), stat, dr("remarks").ToString, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                If dr("status") = 3 Then
                    grdcoa.Rows(grdcoa.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            'ofnum-3, customer-4, cusref-6, platenum-12
            For Each row As DataGridViewRow In grdcoa.Rows
                Dim ofnumber As String = grdcoa.Rows(row.Index).Cells(3).Value

                sql = "Select * from tblorderfill where ofnum='" & ofnumber & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdcoa.Rows(row.Index).Cells(4).Value = dr("customer").ToString
                    grdcoa.Rows(row.Index).Cells(6).Value = dr("cusref").ToString
                    grdcoa.Rows(row.Index).Cells(11).Value = dr("platenum").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            'ofitemid-7, numbags-9,
            For Each row As DataGridViewRow In grdcoa.Rows
                Dim ofitemidnum As String = grdcoa.Rows(row.Index).Cells(7).Value

                sql = "Select * from tblofitem where ofitemid='" & ofitemidnum & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdcoa.Rows(row.Index).Cells(9).Value = dr("numbags")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            Me.Cursor = Cursors.Default
            If grdcoa.Rows.Count = 0 Then
                MsgBox("No Record Found.", MsgBoxStyle.Critical, "")
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

    Public Sub coaidsearch()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            grdcoa.Rows.Clear()

            clickbtn = "Search"

            sql = "Select * from tblcoa where coaid='" & Trim(txtcoaid.Text) & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "In Process"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If

                grdcoa.Rows.Add(dr("coaid"), Format(dr("coadate"), "yyyy/MM/dd"), dr("coanum"), dr("ofnum"), "", dr("coacustomer").ToString, "", dr("ofitemid"), dr("itemname"), 0, dr("batchnum"), "", dr("loaddate"), dr("deldate"), dr("whsename"), dr("shift"), stat, dr("remarks").ToString, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                If dr("status") = 3 Then
                    grdcoa.Rows(grdcoa.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            'ofnum-3, customer-4, cusref-6, platenum-12
            For Each row As DataGridViewRow In grdcoa.Rows
                Dim ofnumber As String = grdcoa.Rows(row.Index).Cells(3).Value

                sql = "Select * from tblorderfill where ofnum='" & ofnumber & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdcoa.Rows(row.Index).Cells(4).Value = dr("customer").ToString
                    grdcoa.Rows(row.Index).Cells(6).Value = dr("cusref").ToString
                    grdcoa.Rows(row.Index).Cells(11).Value = dr("platenum").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            'ofitemid-7, numbags-9,
            For Each row As DataGridViewRow In grdcoa.Rows
                Dim ofitemidnum As String = grdcoa.Rows(row.Index).Cells(7).Value

                sql = "Select * from tblofitem where ofitemid='" & ofitemidnum & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdcoa.Rows(row.Index).Cells(9).Value = dr("numbags")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            Me.Cursor = Cursors.Default
            If grdcoa.Rows.Count = 0 Then
                MsgBox("No Record Found.", MsgBoxStyle.Critical, "")
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

    Public Sub wrssearch()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            grdcoa.Rows.Clear()

            clickbtn = "Search"

            list1.Items.Clear()
            sql = "Select tblofitem.coanum from tblofitem where tblofitem.coanum<>'' and tblofitem.coanum is not NULL and tblofitem.wrsnum='" & Trim(txtwrs.Text) & "' and status<>'3' and tblofitem.branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                list1.Items.Add(dr("coanum").ToString)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            For i = 0 To list1.Items.Count - 1
                Dim coa As String = list1.Items(i)
                sql = "Select * from tblcoa where coanum='" & coa & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    Dim stat As String = ""
                    If dr("status") = 1 Then
                        stat = "In Process"
                    ElseIf dr("status") = 2 Then
                        stat = "Completed"
                    ElseIf dr("status") = 3 Then
                        stat = "Cancelled"
                    End If

                    grdcoa.Rows.Add(dr("coaid"), Format(dr("coadate"), "yyyy/MM/dd"), dr("coanum"), dr("ofnum"), "", dr("coacustomer").ToString, "", dr("ofitemid"), dr("itemname"), 0, dr("batchnum"), "", dr("loaddate"), dr("deldate"), dr("whsename"), dr("shift"), stat, dr("remarks").ToString, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                    If dr("status") = 3 Then
                        grdcoa.Rows(grdcoa.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                    End If
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next


            'ofnum-3, customer-4, cusref-6, platenum-12
            For Each row As DataGridViewRow In grdcoa.Rows
                Dim ofnumber As String = grdcoa.Rows(row.Index).Cells(3).Value

                sql = "Select * from tblorderfill where ofnum='" & ofnumber & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdcoa.Rows(row.Index).Cells(4).Value = dr("customer").ToString
                    grdcoa.Rows(row.Index).Cells(6).Value = dr("cusref").ToString
                    grdcoa.Rows(row.Index).Cells(11).Value = dr("platenum").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            'ofitemid-7, numbags-9,
            For Each row As DataGridViewRow In grdcoa.Rows
                Dim ofitemidnum As String = grdcoa.Rows(row.Index).Cells(7).Value

                sql = "Select * from tblofitem where ofitemid='" & ofitemidnum & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdcoa.Rows(row.Index).Cells(9).Value = dr("numbags")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            Me.Cursor = Cursors.Default
            If grdcoa.Rows.Count = 0 Then
                MsgBox("No Record Found.", MsgBoxStyle.Critical, "")
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

    Public Sub cussearch()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            grdcoa.Rows.Clear()

            clickbtn = "Search"

            list1.Items.Clear()
            sql = "Select tblofitem.coanum from tblofitem left outer join tblorderfill on tblofitem.ofid=tblorderfill.ofid where tblofitem.coanum<>'' and tblofitem.coanum is not NULL and tblorderfill.customer='" & Trim(cmbcus.Text) & "' and tblorderfill.status<>'3' and tblofitem.status<>'3' and tblorderfill.branch='" & login.branch & "' and tblofitem.branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                list1.Items.Add(dr("coanum").ToString)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            For i = 0 To list1.Items.Count - 1
                Dim coa As String = list1.Items(i)
                sql = "Select * from tblcoa where coanum='" & coa & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    Dim stat As String = ""
                    If dr("status") = 1 Then
                        stat = "In Process"
                    ElseIf dr("status") = 2 Then
                        stat = "Completed"
                    ElseIf dr("status") = 3 Then
                        stat = "Cancelled"
                    End If

                    grdcoa.Rows.Add(dr("coaid"), Format(dr("coadate"), "yyyy/MM/dd"), dr("coanum"), dr("ofnum"), "", dr("coacustomer").ToString, "", dr("ofitemid"), dr("itemname"), 0, dr("batchnum"), "", dr("loaddate"), dr("deldate"), dr("whsename"), dr("shift"), stat, dr("remarks").ToString, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                    If dr("status") = 3 Then
                        grdcoa.Rows(grdcoa.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                    End If
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next


            'ofnum-3, customer-4, cusref-6, platenum-12
            For Each row As DataGridViewRow In grdcoa.Rows
                Dim ofnumber As String = grdcoa.Rows(row.Index).Cells(3).Value

                sql = "Select * from tblorderfill where ofnum='" & ofnumber & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdcoa.Rows(row.Index).Cells(4).Value = dr("customer").ToString
                    grdcoa.Rows(row.Index).Cells(6).Value = dr("cusref").ToString
                    grdcoa.Rows(row.Index).Cells(11).Value = dr("platenum").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            'ofitemid-7, numbags-9,
            For Each row As DataGridViewRow In grdcoa.Rows
                Dim ofitemidnum As String = grdcoa.Rows(row.Index).Cells(7).Value

                sql = "Select * from tblofitem where ofitemid='" & ofitemidnum & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdcoa.Rows(row.Index).Cells(9).Value = dr("numbags")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            Me.Cursor = Cursors.Default
            If grdcoa.Rows.Count = 0 Then
                MsgBox("No Record Found.", MsgBoxStyle.Critical, "")
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

    Private Sub txtcoanum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcoanum.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/coanumsearch()
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtcoanum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcoanum.TextChanged
        If Trim(txtcoanum.Text) <> "" Then
            Dim str As String
            str = txtcoanum.Text
            If str.Length > 4 Then
                Dim answer As String
                answer = str.Substring(0, 4)
                If answer = "COA." Then
                    str = str.Substring(4, str.Length - 4)
                    txtcoanum.Text = str
                    txtcoanum.Select(txtcoanum.Text.Length, 0)
                End If
            End If

            cmbwhse.Enabled = False
            cmbshift.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
        Else
            cmbwhse.Enabled = True
            cmbshift.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
        End If
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        Try
            If grdcoa.SelectedCells.Count = 1 Or grdcoa.SelectedRows.Count = 1 Then
                If grdcoa.Rows(selectedrow).Cells(16).Value.ToString = "Completed" Then
                    'ofnum As String, ofitemid As Integer
                    If grdcoa.Rows(selectedrow).Cells(25).Value.ToString = "" Or grdcoa.Rows(selectedrow).Cells(25).Value.ToString = "Master COA" Then
                        rptcoarevise.qty = grdcoa.Rows(selectedrow).Cells(9).Value.ToString
                        rptcoarevise.ofnum = grdcoa.Rows(selectedrow).Cells(3).Value.ToString
                        rptcoarevise.coanum = grdcoa.Rows(selectedrow).Cells(2).Value.ToString
                        rptcoarevise.ofitemid = grdcoa.Rows(selectedrow).Cells(7).Value.ToString
                        rptcoarevise.stat = ""
                        rptcoarevise.coaform = grdcoa.Rows(selectedrow).Cells(25).Value.ToString
                        rptcoarevise.ShowDialog()
                    Else
                        rptcoaformat.qty = grdcoa.Rows(selectedrow).Cells(9).Value.ToString
                        rptcoaformat.ofnum = grdcoa.Rows(selectedrow).Cells(3).Value.ToString
                        rptcoaformat.coanum = grdcoa.Rows(selectedrow).Cells(2).Value.ToString
                        rptcoaformat.ofitemid = grdcoa.Rows(selectedrow).Cells(7).Value.ToString
                        rptcoaformat.stat = ""
                        rptcoaformat.ShowDialog()
                    End If
                Else
                    MsgBox("Cannot print coa.", MsgBoxStyle.Exclamation, "")
                End If
            Else
                MsgBox("Select only one.", MsgBoxStyle.Exclamation, "")
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

    Private Sub grdcoa_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcoa.CellContentClick
        Try
            Me.Cursor = Cursors.WaitCursor

            'link
            If e.ColumnIndex = 2 And e.RowIndex > -1 Then

                Dim cell As DataGridViewCell = grdcoa.Rows(e.RowIndex).Cells(e.ColumnIndex)
                grdcoa.CurrentCell = cell
                ' Me.ContextMenuStrip2.Show(Cursor.Position)
                If grdcoa.RowCount <> 0 Then
                    If grdcoa.Item(2, grdcoa.CurrentRow.Index).Value IsNot Nothing Then
                        If grdcoa.Item(25, grdcoa.CurrentRow.Index).Value.ToString <> "" Then
                            coainfowithform.coanum = grdcoa.Item(2, grdcoa.CurrentRow.Index).Value
                            coainfowithform.ShowDialog()
                        Else
                            coainfo.coanum = grdcoa.Item(2, grdcoa.CurrentRow.Index).Value
                            coainfo.ShowDialog()
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

    Private Sub grdcoa_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdcoa.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then

                grdcoa.ClearSelection()
                grdcoa.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True

                selectedrow = e.RowIndex
                '/If btncomplete.Text = "Selected Item Complete" Then
                Me.ContextMenuStrip1.Show(Cursor.Position)
                '/End If
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

    Private Sub grdcoa_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles grdcoa.RowPrePaint
        If e.RowIndex > -1 Then
            Dim dgvRow As DataGridViewRow = grdcoa.Rows(e.RowIndex)
            '<== But this is the name assigned to it in the properties of the control
            If dgvRow.Cells(16).Value = "Cancelled" Then 'step1
                dgvRow.DefaultCellStyle.BackColor = Color.DeepSkyBlue
            End If
        End If
    End Sub

    Private Sub grdcoa_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdcoa.SelectionChanged
        count()
    End Sub

    Public Sub count()
        Try
            lblcount.Text = "     Selected Rows Count: " & grdcoa.SelectedRows.Count
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub CancelCOAToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelCOAToolStripMenuItem.Click
        Try
            'check level of users
            If login.depart <> "All" And login.depart <> "QCA" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdcoa.SelectedCells.Count = 1 Or grdcoa.SelectedRows.Count = 1 Then
                'check if orderfill is not cancelled
                sql = "Select * from tblcoa where coaid='" & grdcoa.Rows(selectedrow).Cells(0).Value & "' and status='3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("COA is already cancelled.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                Dim a As String = MsgBox("Are you sure you want to cancel COA?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbNo Then
                    Exit Sub
                Else
                    coacancel.lblcoaid.Text = grdcoa.Rows(selectedrow).Cells(0).Value
                    coacancel.lblcoanum.Text = grdcoa.Rows(selectedrow).Cells(2).Value
                    coacancel.ShowDialog()
                End If

                If clickbtn = "Search" Then
                    btnsearch.PerformClick()
                Else
                    btnview.PerformClick()
                End If

            Else
                MsgBox("Select only one.", MsgBoxStyle.Exclamation, "")
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

    Private Sub txtwrs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwrs.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/wrssearch()
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtwrs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwrs.TextChanged
        Dim charactersDisallowed As String = "0123456789"
        Dim theText As String = txtwrs.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtwrs.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtwrs.Text.Length - 1
            Letter = txtwrs.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtwrs.Text = theText
        txtwrs.Select(SelectionIndex - Change, 0)

        If Trim(txtwrs.Text) <> "" Then
            cmbwhse.Enabled = False
            cmbshift.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
            txtcoanum.Text = ""
            txtcoanum.Enabled = False
            txtcoaid.Text = ""
            txtcoaid.Enabled = False
            cmbcus.Text = ""
            cmbcus.Enabled = False
        Else
            cmbwhse.Enabled = True
            cmbshift.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtcoanum.Text = ""
            txtcoanum.Enabled = True
            txtcoaid.Text = ""
            txtcoaid.Enabled = True
            cmbcus.Text = ""
            cmbcus.Enabled = True
        End If
    End Sub

    Private Sub txtcoaid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcoaid.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/coaidsearch()
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtcoaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcoaid.TextChanged
        Dim charactersDisallowed As String = "0123456789"
        Dim theText As String = txtcoaid.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtcoaid.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtcoaid.Text.Length - 1
            Letter = txtcoaid.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtcoaid.Text = theText
        txtcoaid.Select(SelectionIndex - Change, 0)

        If Trim(txtcoaid.Text) <> "" Then
            cmbwhse.Enabled = False
            cmbshift.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
            txtcoanum.Text = ""
            txtcoanum.Enabled = False
            txtwrs.Text = ""
            txtwrs.Enabled = False
            cmbcus.Text = ""
            cmbcus.Enabled = False
        Else
            cmbwhse.Enabled = True
            cmbshift.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtcoanum.Text = ""
            txtcoanum.Enabled = True
            txtwrs.Text = ""
            txtwrs.Enabled = True
            cmbcus.Text = ""
            cmbcus.Enabled = True
        End If
    End Sub

    Private Sub cmbcus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbcus.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbcus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcus.SelectedIndexChanged

    End Sub
End Class