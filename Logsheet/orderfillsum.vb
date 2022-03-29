Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.ComponentModel

Public Class orderfillsum
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim clickbtn As String, selectedrow As Integer, gridsql As String, logbranch As String, loginwhse As String

    Private threadEnabled As Boolean, threadEnabledsteps As Boolean, threadEnableddestin As Boolean
    Private backgroundWorker As BackgroundWorker

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
        If conn.State <> ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub orderfillsum_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub orderfillsum_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewcus()
        viewwhse()
        viewshift()
        viewpending()
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
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
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
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
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
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MsgBox(ex.Message, MsgBoxStyle.Information)
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
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub cmbshift_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbshift.SelectedIndexChanged

    End Sub

    Private Sub btnview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnview.Click
        cmbcus.Text = ""
        cmbwhse.Text = ""
        cmbshift.Text = ""
        txtofnum.Text = ""
        txtofid.Text = ""
        txtwrs.Text = ""
        viewpending()
    End Sub

    Public Sub viewpending()
        Try
            clickbtn = "Pending"

            sql = "Select * from tblorderfill where status='1' and branch='" & login.branch & "'"

            gridsql = sql

            grdof.Rows.Clear()

            backgroundWorker = New BackgroundWorker()
            backgroundWorker.WorkerSupportsCancellation = True

            backgroundWorker = New BackgroundWorker()
            AddHandler backgroundWorker.DoWork, New DoWorkEventHandler(AddressOf backgroundWorker_DoWork)
            AddHandler backgroundWorker.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf backgroundWorker_Completed)
            AddHandler backgroundWorker.ProgressChanged, New ProgressChangedEventHandler(AddressOf backgroundWorker_ProgressChanged)
            m_addRowDelegate = New AddRowDelegate(AddressOf AddDGVRow)

            If Not backgroundWorker.IsBusy Then
                backgroundWorker.WorkerReportsProgress = True
                backgroundWorker.WorkerSupportsCancellation = True
                backgroundWorker.RunWorkerAsync() 'start ng select query
            End If

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
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
            
            If grdof.InvokeRequired Then
                grdof.Invoke(m_addRowDelegate, drx("ofid"), Format(drx("ofdate"), "yyyy/MM/dd"), drx("ofnum"), drx("wrsnum"), drx("coanum").ToString, drx("customer"), drx("cusref"), drx("platenum"), drx("whsename"), drx("matdispo"), drx("remarks"), stat, drx("datecreated"), drx("createdby"), drx("datemodified"), drx("modifiedby"), drx("canceldate").ToString, drx("cancelby").ToString, drx("cancelreason").ToString, i)
            Else
                AddDGVRow(drx("ofid"), Format(drx("ofdate"), "yyyy/MM/dd"), drx("ofnum"), drx("wrsnum"), drx("coanum").ToString, drx("customer"), drx("cusref"), drx("platenum"), drx("whsename"), drx("matdispo"), drx("remarks"), stat, drx("datecreated"), drx("createdby"), drx("datemodified"), drx("modifiedby"), drx("canceldate").ToString, drx("cancelby").ToString, drx("cancelreason").ToString, i)
            End If

            i += 1
            backgroundWorker.ReportProgress(i) '/ idivide kung ilan ang total

        End While
        drx.Dispose()
        cmd.Dispose()
        connection.Close()
    End Sub

    Delegate Sub AddRowDelegate(ByVal v0 As Object, ByVal v1 As Object, ByVal v2 As Object, ByVal v3 As Object, ByVal v4 As Object, ByVal v5 As Object, ByVal v6 As Object, ByVal v7 As Object, ByVal v8 As Object, ByVal v9 As Object, ByVal v10 As Object, ByVal v11 As Object, ByVal v12 As Object, ByVal v13 As Object, ByVal v14 As Object, ByVal v15 As Object, ByVal v16 As Object, ByVal v17 As Object, ByVal v18 As Object, ByVal vrow As Object)
    Private m_addRowDelegate As AddRowDelegate

    Private Sub AddDGVRow(ByVal v0 As Object, ByVal v1 As Object, ByVal v2 As Object, ByVal v3 As Object, ByVal v4 As Object, ByVal v5 As Object, ByVal v6 As Object, ByVal v7 As Object, ByVal v8 As Object, ByVal v9 As Object, ByVal v10 As Object, ByVal v11 As Object, ByVal v12 As Object, ByVal v13 As Object, ByVal v14 As Object, ByVal v15 As Object, ByVal v16 As Object, ByVal v17 As Object, ByVal v18 As Object, ByVal vrow As Integer)
        If threadEnabled = True Then
            If grdof.InvokeRequired Then
                grdof.BeginInvoke(New AddRowDelegate(AddressOf AddDGVRow), v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, vrow)
            Else
                grdof.Rows.Add(v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18)
            End If
        End If
    End Sub

    Private Sub backgroundWorker_Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
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
            If grdof.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Information, "")
            Else
                grdof.SuspendLayout()
                grdof.ResumeLayout()

                If clickbtn <> "Search" Then
                    MsgBox("Loading data completed.", MsgBoxStyle.Information, "")
                End If
            End If
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

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            clickbtn = "Search"

            If Trim(txtofnum.Text) <> "" Then
                '/ofnumsearch()
                sql = "Select * from tblorderfill where ofnum='" & lblof.Text & Trim(txtofnum.Text) & "' and branch='" & login.branch & "'"
            ElseIf Trim(txtofid.Text) <> "" Then
                '/ofidsearch()
                sql = "Select * from tblorderfill where ofid='" & Trim(txtofid.Text) & "' and branch='" & login.branch & "'"
            ElseIf Trim(txtwrs.Text) <> "" Then
                '/wrssearch()
                sql = "Select * from tblorderfill where wrsnum='" & Trim(txtwrs.Text) & "' and branch='" & login.branch & "'"
            ElseIf Trim(cmbcus.Text) <> "" Then
                '/cussearch()
                sql = "Select * from tblorderfill where customer='" & Trim(cmbcus.Text) & "' and ofdate>='" & Format(datefrom.Value, "yyyy/MM/dd") & "' and ofdate<='" & Format(dateto.Value, "yyyy/MM/dd") & "' and branch='" & login.branch & "'"
            Else
                sql = "Select * from tblorderfill where ofdate>='" & Format(datefrom.Value, "yyyy/MM/dd") & "' and ofdate<='" & Format(dateto.Value, "yyyy/MM/dd") & "' and branch='" & login.branch & "'"

                If cmbwhse.Text <> "" Then
                    sql = sql & " and tblorderfill.whsename='" & cmbwhse.Text & "'"
                End If

                If cmbshift.Text <> "" Then
                    sql = sql & " and tblorderfill.shift='" & cmbshift.Text & "'"
                End If

                If chkhide.Checked = True Then
                    sql = sql & " and tblorderfill.status<>'3'"
                End If

                sql = sql & " order by tblorderfill.ofdate"
            End If

            gridsql = sql

            grdof.Rows.Clear()

            backgroundWorker = New BackgroundWorker()
            backgroundWorker.WorkerSupportsCancellation = True

            backgroundWorker = New BackgroundWorker()
            AddHandler backgroundWorker.DoWork, New DoWorkEventHandler(AddressOf backgroundWorker_DoWork)
            AddHandler backgroundWorker.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf backgroundWorker_Completed)
            AddHandler backgroundWorker.ProgressChanged, New ProgressChangedEventHandler(AddressOf backgroundWorker_ProgressChanged)
            m_addRowDelegate = New AddRowDelegate(AddressOf AddDGVRow)

            If Not backgroundWorker.IsBusy Then
                backgroundWorker.WorkerReportsProgress = True
                backgroundWorker.WorkerSupportsCancellation = True
                backgroundWorker.RunWorkerAsync() 'start ng select query
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

    Private Sub datefrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles datefrom.ValueChanged
        dateto.MinDate = datefrom.Value
    End Sub

    Public Sub ofnumsearch()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            grdof.Rows.Clear()

            clickbtn = "Search"

            sql = "Select * from tblorderfill where ofnum='" & lblof.Text & Trim(txtofnum.Text) & "' and branch='" & login.branch & "'"
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

                grdof.Rows.Add(dr("ofid"), Format(dr("ofdate"), "yyyy/MM/dd"), dr("ofnum"), dr("wrsnum"), dr("coanum").ToString, dr("customer"), dr("cusref"), dr("platenum"), dr("whsename"), dr("matdispo"), dr("remarks"), stat, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                If dr("status") = 3 Then
                    grdof.Rows(grdof.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.Default
            If grdof.Rows.Count = 0 Then
                MsgBox("No Record Found.", MsgBoxStyle.Critical, "")
            End If

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

    Public Sub cussearch()
        Try
            Me.Cursor = Cursors.WaitCursor

            grdof.Rows.Clear()

            clickbtn = "Search"

            sql = "Select * from tblorderfill where customer='" & Trim(cmbcus.Text) & "' and ofdate>='" & Format(datefrom.Value, "yyyy/MM/dd") & "' and ofdate<='" & Format(dateto.Value, "yyyy/MM/dd") & "' and branch='" & login.branch & "'"

            If chkhide.Checked = True Then
                sql = sql & " and status<>'3'"
            End If

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

                grdof.Rows.Add(dr("ofid"), Format(dr("ofdate"), "yyyy/MM/dd"), dr("ofnum"), dr("wrsnum"), dr("coanum").ToString, dr("customer"), dr("cusref"), dr("platenum"), dr("whsename"), dr("matdispo"), dr("remarks"), stat, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                If dr("status") = 3 Then
                    grdof.Rows(grdof.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.Default
            If grdof.Rows.Count = 0 Then
                MsgBox("No Record Found.", MsgBoxStyle.Critical, "")
            End If

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

    Public Sub ofidsearch()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            grdof.Rows.Clear()

            clickbtn = "Search"

            sql = "Select * from tblorderfill where ofid='" & Trim(txtofid.Text) & "' and branch='" & login.branch & "'"
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

                grdof.Rows.Add(dr("ofid"), Format(dr("ofdate"), "yyyy/MM/dd"), dr("ofnum"), dr("wrsnum"), dr("coanum").ToString, dr("customer"), dr("cusref"), dr("platenum"), dr("whsename"), dr("matdispo"), dr("remarks"), stat, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                If dr("status") = 3 Then
                    grdof.Rows(grdof.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.Default
            If grdof.Rows.Count = 0 Then
                MsgBox("No Record Found.", MsgBoxStyle.Critical, "")
            End If

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

    Private Sub txtofnum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtofnum.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/ofnumsearch()
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtofnum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtofnum.TextChanged
        If Trim(txtofnum.Text) <> "" Then
            Dim str As String
            str = txtofnum.Text
            If str.Length > 3 Then
                Dim answer As String
                answer = str.Substring(0, 3)
                If answer = "OF." Then
                    str = str.Substring(3, str.Length - 3)
                    txtofnum.Text = str
                    txtofnum.Select(txtofnum.Text.Length, 0)
                End If
            End If

            cmbwhse.Enabled = False
            cmbshift.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
            '/txtwrs.Text = ""
            txtwrs.Enabled = False
            '/txtofid.Text = ""
            txtofid.Enabled = False
            '/cmbcus.Text = ""
            cmbcus.Enabled = False
        Else
            cmbwhse.Enabled = True
            cmbshift.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtwrs.Enabled = True
            txtofid.Enabled = True
            cmbcus.Enabled = True
        End If
    End Sub

    Public Sub wrssearch()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            grdof.Rows.Clear()

            clickbtn = "Search"

            sql = "Select * from tblorderfill where wrsnum='" & Trim(txtwrs.Text) & "' and branch='" & login.branch & "'"
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

                grdof.Rows.Add(dr("ofid"), Format(dr("ofdate"), "yyyy/MM/dd"), dr("ofnum"), dr("wrsnum"), dr("coanum").ToString, dr("customer"), dr("cusref"), dr("platenum"), dr("whsename"), dr("matdispo"), dr("remarks"), stat, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                If dr("status") = 3 Then
                    grdof.Rows(grdof.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.Default
            If grdof.Rows.Count = 0 Then
                MsgBox("No Record Found.", MsgBoxStyle.Critical, "")
            End If

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
            '/txtofnum.Text = ""
            txtofnum.Enabled = False
            '/txtofid.Text = ""
            txtofid.Enabled = False
            '/cmbcus.Text = ""
            cmbcus.Enabled = False
        Else
            cmbwhse.Enabled = True
            cmbshift.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtofnum.Enabled = True
            txtofid.Enabled = True
            cmbcus.Enabled = True
        End If
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        Try
            If grdof.SelectedCells.Count = 1 Or grdof.SelectedRows.Count = 1 Then
                If grdof.Rows(selectedrow).Cells(11).Value.ToString = "Completed" Then
                    'print order fill
                    rptorderfill.ofnum = grdof.Rows(selectedrow).Cells(2).Value.ToString
                    rptorderfill.ofid = grdof.Rows(selectedrow).Cells(0).Value.ToString
                    rptorderfill.ShowDialog()

                    'ElseIf grdof.Rows(selectedrow).Cells(11).Value = "In Process" Then

                    '    Dim a As String = MsgBox("Order fill is pending. Do you want to print it anyway?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                    '    If a = vbYes Then
                    '        rptorderfill.stat = "Pending"
                    '        rptorderfill.ofnum = grdof.Rows(selectedrow).Cells(2).Value.ToString
                    '        rptorderfill.ofid = grdof.Rows(selectedrow).Cells(0).Value.ToString
                    '        rptorderfill.ShowDialog()
                    '    End If
                Else
                    MsgBox("Cannot print order fill.", MsgBoxStyle.Exclamation, "")
                End If
            Else
                MsgBox("Select only one.", MsgBoxStyle.Exclamation, "")
            End If

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

    Private Sub grdof_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdof.CellContentClick
        Try
            Me.Cursor = Cursors.WaitCursor

            'link
            If e.ColumnIndex = 2 And e.RowIndex > -1 Then

                Dim cell As DataGridViewCell = grdof.Rows(e.RowIndex).Cells(e.ColumnIndex)
                grdof.CurrentCell = cell
                ' Me.ContextMenuStrip2.Show(Cursor.Position)
                If grdof.RowCount <> 0 Then
                    If grdof.Item(2, grdof.CurrentRow.Index).Value IsNot Nothing Then
                        'view laman ng orderfill
                        'grdofsheet tska grdcancel
                        orderfillinfo.txtofid.Text = grdof.Item(0, grdof.CurrentRow.Index).Value
                        orderfillinfo.ShowDialog()
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

    Private Sub grdof_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdof.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then

                grdof.ClearSelection()
                grdof.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True

                selectedrow = e.RowIndex

                '/If btncomplete.Text = "Selected Item Complete" Then
                Me.ContextMenuStrip1.Show(Cursor.Position)
                '/End If
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

    Private Sub grdof_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles grdof.RowPrePaint
        If e.RowIndex > -1 Then
            Dim dgvRow As DataGridViewRow = grdof.Rows(e.RowIndex)
            '<== But this is the name assigned to it in the properties of the control
            If dgvRow.Cells(11).Value = "Cancelled" Then 'step1
                dgvRow.DefaultCellStyle.BackColor = Color.DeepSkyBlue
            End If
        End If
    End Sub

    Private Sub grdof_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdof.SelectionChanged
        count()
    End Sub

    Public Sub count()
        Try
            lblcount.Text = "     Selected Rows Count: " & grdof.SelectedRows.Count
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CancelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelToolStripMenuItem.Click
        Try
            'check level of users
            If login.depart <> "All" And login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdof.SelectedCells.Count = 1 Or grdof.SelectedRows.Count = 1 Then
                'check if orderfill is not cancelled
                sql = "Select * from tblorderfill where ofid='" & grdof.Rows(selectedrow).Cells(0).Value & "' and tblorderfill.status='3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check kung may nka create na na coa isa sa ofitem nito
                sql = "Select * from tblofitem right outer join tblcoa on tblofitem.coanum=tblcoa.coanum where tblofitem.ofid='" & grdof.Rows(selectedrow).Cells(0).Value & "' and tblcoa.status<>'3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Cannot cancel. There is a pending COA under this Order fill.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()


                Dim a As String = MsgBox("Are you sure you want to cancel order fill?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbNo Then
                    Exit Sub
                Else
                    orderfillcancel.lblofid.Text = grdof.Rows(selectedrow).Cells(0).Value
                    orderfillcancel.lblofnum.Text = grdof.Rows(selectedrow).Cells(2).Value
                    orderfillcancel.ShowDialog()
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

    Private Sub cmbcus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbcus.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbcus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcus.SelectedIndexChanged

    End Sub

    Private Sub txtofid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtofid.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/ofidsearch()
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtofid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtofid.TextChanged
        Dim charactersDisallowed As String = "0123456789"
        Dim theText As String = txtofid.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtofid.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtofid.Text.Length - 1
            Letter = txtofid.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtofid.Text = theText
        txtofid.Select(SelectionIndex - Change, 0)

        If Trim(txtofid.Text) <> "" Then
            cmbwhse.Enabled = False
            cmbshift.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
            '/txtofnum.Text = ""
            txtofnum.Enabled = False
            '/txtwrs.Text = ""
            txtwrs.Enabled = False
            '/cmbcus.Text = ""
            cmbcus.Enabled = False
        Else
            cmbwhse.Enabled = True
            cmbshift.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtofnum.Enabled = True
            txtwrs.Enabled = True
            cmbcus.Enabled = True
        End If
    End Sub

    Private Sub cmbcus_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcus.TextChanged
        If Trim(cmbcus.Text) <> "" Then
            cmbwhse.Enabled = False
            cmbshift.Enabled = False
            '/txtofnum.Text = ""
            txtofnum.Enabled = False
            '/txtwrs.Text = ""
            txtwrs.Enabled = False
            '/txtofid.Text = ""
            txtofid.Enabled = False
        Else
            cmbwhse.Enabled = True
            cmbshift.Enabled = True
            txtofnum.Enabled = True
            txtwrs.Enabled = True
            txtofid.Enabled = True
        End If
    End Sub

    Private Sub AllQuantityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllQuantityToolStripMenuItem.Click
        Try
            'check level of users
            If login.depart <> "All" And login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            'check if completed na yung orderfill at may coa na
            If grdof.SelectedCells.Count = 1 Or grdof.SelectedRows.Count = 1 Then
                Dim ok As Boolean = False

                sql = "Select * from tblofitem where ofid='" & grdof.Rows(selectedrow).Cells(0).Value & "' and status='2' and coanum is not NULL"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    ok = True
                Else
                    MsgBox("Cannot return items.", MsgBoxStyle.Exclamation, "")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If ok = True Then
                    orderfillreturn.rball.Checked = True
                    orderfillreturn.txtofid.Text = grdof.Rows(selectedrow).Cells(0).Value
                    orderfillreturn.txtofnum.Text = grdof.Rows(selectedrow).Cells(2).Value
                    orderfillreturn.Text = "Return All Items"
                    orderfillreturn.grdlog.Columns(9).Visible = False
                    orderfillreturn.grdlog.Columns(10).Visible = False
                    orderfillreturn.grdlog.Columns(11).Visible = False
                    orderfillreturn.ShowDialog()
                End If
            Else
                MsgBox("Select only one.", MsgBoxStyle.Information, "")
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

    Private Sub SomeQuantityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SomeQuantityToolStripMenuItem.Click
        Try
            'check level of users
            If login.depart <> "All" And login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            'check if completed na yung orderfill at may coa na
            If grdof.SelectedCells.Count = 1 Or grdof.SelectedRows.Count = 1 Then
                Dim ok As Boolean = False

                sql = "Select * from tblofitem where ofid='" & grdof.Rows(selectedrow).Cells(0).Value & "' and status='2' and coanum is not NULL"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    ok = True
                Else
                    MsgBox("Cannot return items.", MsgBoxStyle.Exclamation, "")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If ok = True Then
                    orderfillreturn.rbsome.Checked = True
                    orderfillreturn.txtofid.Text = grdof.Rows(selectedrow).Cells(0).Value
                    orderfillreturn.txtofnum.Text = grdof.Rows(selectedrow).Cells(2).Value
                    orderfillreturn.Text = "Return Some Items"
                    '/orderfillreturn.grdof.Columns(9).Visible = True
                    orderfillreturn.grdlog.Columns(10).Visible = True
                    orderfillreturn.grdlog.Columns(11).Visible = True
                    orderfillreturn.ShowDialog()
                End If
            Else
                MsgBox("Select only one.", MsgBoxStyle.Information, "")
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
End Class