Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.ComponentModel

Public Class receivesum
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public adlogcnf As Boolean, trems As String, adlogby As String
    Dim clickbtn As String = "", selectedrow As Integer, gridsql As String, logbranch As String, loginwhse As String

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
        If conn.State <> ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub ticketmanage_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub ticketmanage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewwhse()
        viewline()
        viewshift()
        viewpending()
        viewitems()
        viewpallets.TreeView1.ImageList = ImageList1
        cmbitem.DropDownWidth = 300

        logbranch = login.branch
        loginwhse = login.logwhse
        rbprod.Checked = True

        If login.depart = "All" Then
            ActivateLogsheetToolStripMenuItem.Visible = True
            ActivateAdminConfirmationToolStripMenuItem.Visible = True
        Else
            ActivateLogsheetToolStripMenuItem.Visible = False
            ActivateAdminConfirmationToolStripMenuItem.Visible = False
        End If
    End Sub

    Public Sub viewitems()
        Try
            cmbitem.Items.Clear()
            cmbitem.Items.Add("")

            sql = "Select * from tblitems where status='1' order by itemname"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbitem.Items.Add(dr("itemname"))
            End While
            dr.Dispose()
            cmd.Dispose()

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

    Public Sub viewwhse()
        Try
            cmbwhse.Items.Clear()
            cmbwhse.Items.Add("")

            sql = "Select * from tblwhse where branch='" & login.branch & "' and status='1' order by whsename"
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

    Public Sub viewline()
        Try
            cmbline.Items.Clear()
            cmbline.Items.Add("")

            sql = "Select * from tblpalletizer where branch='" & login.branch & "' and status='1' order by palletizer"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If cmbline.Items.Contains(dr("palletizer")) = False Then
                    cmbline.Items.Add(dr("palletizer"))
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If cmbline.Items.Count <> 0 Then
                cmbline.Items.Add("All")
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
                    sql = "Select * from tblwhse where branch='" & login.branch & "' and whsename='" & Trim(cmbwhse.Text.ToUpper) & "'"
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

    Private Sub cmbline_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbline.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbline_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbline.Leave
        Try
            If Trim(cmbline.Text) <> "" Then
                If Not cmbline.Items.Contains(Trim(cmbline.Text.ToUpper)) Then
                    cmbline.Text = ""
                Else
                    sql = "Select * from tblpalletizer where branch='" & login.branch & "' and palletizer='" & Trim(cmbline.Text.ToUpper) & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If dr("status") = 1 Then
                            cmbline.SelectedItem = dr("palletizer")
                        Else
                            MsgBox(cmbline.Text.ToUpper & " is already deactivated.", MsgBoxStyle.Information, "")
                            cmbline.SelectedItem = ""
                        End If
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Else
                cmbline.SelectedItem = ""
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

    Private Sub cmbline_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbline.SelectedIndexChanged

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
        cmbwhse.Text = ""
        cmbline.Text = ""
        cmbshift.Text = ""
        txtlog.Text = ""
        cmbitem.Text = ""
        txtlogsheetid.Text = ""
        viewpending()
    End Sub

    Public Sub viewpending()
        Try
            Dim rb As String = ""
            If rbprod.Checked = True Then
                rb = "<>"
            Else
                rb = "="
            End If

            clickbtn = "Pending"

            sql = "Select tbllogsheet.*, tbllogitem.itemname from tbllogsheet"
            sql = sql & " INNER JOIN tbllogitem on tbllogsheet.logsheetid=tbllogitem.logsheetid"
            sql = sql & " where tbllogsheet.branch='" & login.branch & "' and tbllogsheet.printtype" & rb & "'Receive' and tbllogsheet.status='1' and tbllogitem.status<>'3'"

            gridsql = sql

            grdlog.Rows.Clear()

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
                If drx("allitems") = 1 Then
                    stat = "Admin confirmation pending"
                End If
            ElseIf drx("status") = 0 Then
                stat = "Queue"
            ElseIf drx("status") = 2 Then
                stat = "Completed"
            ElseIf drx("status") = 3 Then
                stat = "Cancelled"
            End If

            If grdlog.InvokeRequired Then
                grdlog.Invoke(m_addRowDelegate, drx("logsheetid"), Format(drx("logsheetdate"), "yyyy/MM/dd"), drx("logsheetnum"), drx("whsename"), drx("palletizer"), drx("shift"), drx("itemname"), drx("thread").ToString, drx("bagtype").ToString, drx("printtype").ToString, drx("binnum").ToString, drx("prodrems").ToString, drx("qcarems").ToString, drx("millersup").ToString, drx("datecreated"), drx("createdby"), drx("datemodified"), drx("modifiedby"), stat, drx("remarks"), drx("canceldate").ToString, drx("cancelby").ToString, drx("cancelreason").ToString, i)
            Else
                AddDGVRow(drx("logsheetid"), Format(drx("logsheetdate"), "yyyy/MM/dd"), drx("logsheetnum"), drx("whsename"), drx("palletizer"), drx("shift"), drx("itemname"), drx("thread").ToString, drx("bagtype").ToString, drx("printtype").ToString, drx("binnum").ToString, drx("prodrems").ToString, drx("qcarems").ToString, drx("millersup").ToString, drx("datecreated"), drx("createdby"), drx("datemodified"), drx("modifiedby"), stat, drx("remarks"), drx("canceldate").ToString, drx("cancelby").ToString, drx("cancelreason").ToString, i)
            End If

            i += 1
            backgroundWorker.ReportProgress(i) '/ idivide kung ilan ang total

        End While
        drx.Dispose()
        cmd.Dispose()
        connection.Close()
    End Sub

    Delegate Sub AddRowDelegate(ByVal v0 As Object, ByVal v1 As Object, ByVal v2 As Object, ByVal v3 As Object, ByVal v4 As Object, ByVal v5 As Object, ByVal v6 As Object, ByVal v7 As Object, ByVal v8 As Object, ByVal v9 As Object, ByVal v10 As Object, ByVal v11 As Object, ByVal v12 As Object, ByVal v13 As Object, ByVal v14 As Object, ByVal v15 As Object, ByVal v16 As Object, ByVal v17 As Object, ByVal v18 As Object, ByVal v19 As Object, ByVal v20 As Object, ByVal v21 As Object, ByVal v22 As Object, ByVal vrow As Object)
    Private m_addRowDelegate As AddRowDelegate

    Private Sub AddDGVRow(ByVal v0 As Object, ByVal v1 As Object, ByVal v2 As Object, ByVal v3 As Object, ByVal v4 As Object, ByVal v5 As Object, ByVal v6 As Object, ByVal v7 As Object, ByVal v8 As Object, ByVal v9 As Object, ByVal v10 As Object, ByVal v11 As Object, ByVal v12 As Object, ByVal v13 As Object, ByVal v14 As Object, ByVal v15 As Object, ByVal v16 As Object, ByVal v17 As Object, ByVal v18 As Object, ByVal v19 As Object, ByVal v20 As Object, ByVal v21 As Object, ByVal v22 As Object, ByVal vrow As Integer)
        If threadEnabled = True Then
            If grdlog.InvokeRequired Then
                grdlog.BeginInvoke(New AddRowDelegate(AddressOf AddDGVRow), v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, vrow)
            Else
                grdlog.Rows.Add(v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22)
                '/grdlog.Rows(vrow).Cells(0).Tag = v22
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
        For Each row As DataGridViewRow In grdlog.Rows
            If grdlog.Rows(row.Index).Cells(18).Value = "Admin confirmation pending" Then
                'check
                sql = "Select status from tbllogticket where logsheetnum='" & grdlog.Rows(row.Index).Cells(2).Value & "' and (qadispo='0' or qadispo='3') and tbllogticket.status<>'3' and branch='" & logbranch & "'"
                Dim connection As SqlConnection
                connection = New SqlConnection
                connection.ConnectionString = strconn
                If connection.State <> ConnectionState.Open Then
                    connection.Open()
                End If
                cmd = New SqlCommand(sql, connection)
                Dim drx As SqlDataReader = cmd.ExecuteReader
                If drx.Read Then
                    UpdateDGVRow("QA disposition pending", i)
                End If
                drx.Dispose()
                cmd.Dispose()
                connection.Close()
            End If

            i += 1

            backgroundWorkersteps.ReportProgress(i * 100 / 155) '/ idivide kung ilan ang total
        Next
    End Sub

    Delegate Sub UpdateRowDelegate(ByVal valueindex As Object, ByVal value1 As Object)
    Private m_updateRowDelegate As UpdateRowDelegate

    Private Sub UpdateDGVRow(ByVal vstat As String, ByVal rowin As Integer)
        If threadEnabledsteps = True Then
            If grdlog.InvokeRequired Then
                grdlog.BeginInvoke(New UpdateRowDelegate(AddressOf UpdateDGVRow), vstat, rowin)
            Else
                grdlog.Rows(rowin).Cells(18).Value = vstat
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
            If grdlog.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Information, "")
            Else
                grdlog.SuspendLayout()
                grdlog.ResumeLayout()

                If clickbtn <> "Search" Then
                    MsgBox("Loading data completed.", MsgBoxStyle.Information, "")
                End If
            End If
        End If
    End Sub

    Private Sub grdlog_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdlog.CellContentClick
        Try
            Me.Cursor = Cursors.WaitCursor

            'link
            If e.ColumnIndex = 2 And e.RowIndex > -1 Then

                Dim cell As DataGridViewCell = grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex)
                grdlog.CurrentCell = cell
                ' Me.ContextMenuStrip2.Show(Cursor.Position)
                If grdlog.RowCount <> 0 Then
                    If grdlog.Item(2, grdlog.CurrentRow.Index).Value IsNot Nothing Then
                        'populate grdtrans
                        Dim temploc As String = "", tempcol As String = "", temppar As String = "", tempitem As String = ""

                        viewpallets.Text = "View Transactions (" & grdlog.Item(2, grdlog.CurrentRow.Index).Value.ToString & ")"
                        viewpallets.txtlog.Text = grdlog.Item(2, grdlog.CurrentRow.Index).Value.ToString
                        viewpallets.lblline.Text = grdlog.Item(4, grdlog.CurrentRow.Index).Value.ToString
                        viewpallets.lognum = grdlog.Item(2, grdlog.CurrentRow.Index).Value
                        viewpallets.txttype.Text = grdlog.Item(9, grdlog.CurrentRow.Index).Value.ToString
                        viewpallets.TreeView1.Nodes.Clear()


                        sql = "Select tbllogsheet.logsheetid, tbllogsheet.palletizer, tbllogitem.logitemid as litemid, tbllogitem.itemname, tbllogticket.palletnum, tbllogticket.location, tbllogticket.columns, tbllogticket.logticketid, tbllogticket.addtoloc, tbllogticket.logitemid as titemid, tbllogticket.qadispo, tbllogticket.status from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
                        sql = sql & " where tbllogsheet.branch='" & login.branch & "' and tbllogsheet.logsheetnum='" & grdlog.Item(2, grdlog.CurrentRow.Index).Value & "' order by location, columns"  ' and tbllogticket.status<>'3'
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        While dr.Read
                            Dim parentnode As TreeNode

                            If temppar <> dr("palletizer").ToString Then
                                'magkaiba ibig sabihin new location
                                temppar = dr("palletizer").ToString
                                parentnode = New TreeNode(dr("palletizer").ToString)
                                parentnode.Tag = "Parent"
                                viewpallets.TreeView1.Nodes.Add(parentnode)
                            Else
                                'same location ibang item
                            End If

                            Dim child As TreeNode
                            Dim childnode As TreeNode
                            Dim childchildnode As TreeNode
                            Dim childchildchildnode As TreeNode

                            If tempitem <> dr("itemname").ToString Then
                                'magkaiba ibig sabihin new location
                                tempitem = dr("itemname").ToString
                                child = New TreeNode()
                                child = parentnode.Nodes.Add(dr("itemname").ToString)
                                child.Tag = "Itemcode"
                                child.ImageIndex = 3
                                child.SelectedImageIndex = 3
                            Else
                                'same location ibang item
                            End If

                            If temploc <> dr("location").ToString Then
                                If temploc = "Cancelled" Then
                                    'same location ibang item
                                Else
                                    If IsDBNull(dr("addtoloc")) = True Then
                                        'magkaiba ibig sabihin new location
                                        temploc = "Cancelled"
                                        childnode = New TreeNode()
                                        childnode = child.Nodes.Add("Cancelled")
                                        childnode.Tag = "Location"
                                        childnode.ImageIndex = 4
                                        childnode.SelectedImageIndex = 4

                                    Else
                                        'magkaiba ibig sabihin new location
                                        temploc = dr("location").ToString
                                        childnode = New TreeNode()
                                        childnode = child.Nodes.Add(dr("location").ToString)
                                        childnode.Tag = "Location"
                                        childnode.ImageIndex = 4
                                        childnode.SelectedImageIndex = 4
                                    End If
                                End If
                            Else
                                'same location ibang item
                            End If


                            If temploc = "Cancelled" Then
                                If tempcol <> "0" Then
                                    'magkaiba ibig sabihin new column
                                    tempcol = "0"
                                    childchildnode = New TreeNode()
                                    childchildnode = childnode.Nodes.Add("0")
                                    childchildnode.Tag = "0"
                                    childchildnode.ImageIndex = 5
                                    childchildnode.SelectedImageIndex = 5
                                Else
                                    'same column ibang item
                                End If

                                childchildchildnode = New TreeNode()
                                childchildchildnode = childchildnode.Nodes.Add("P#" & dr("palletnum").ToString)
                                childchildchildnode.Tag = dr("logticketid").ToString
                                If dr("qadispo") = 1 Then
                                    childchildchildnode.ImageIndex = 1
                                    childchildchildnode.SelectedImageIndex = 1
                                ElseIf dr("qadispo") = 2 Then
                                    childchildchildnode.ImageIndex = 2
                                    childchildchildnode.SelectedImageIndex = 2
                                Else 'cancelled or wla png dispo
                                    childchildchildnode.ImageIndex = 0
                                    childchildchildnode.SelectedImageIndex = 0
                                End If
                                If dr("status") = 3 Then
                                    childchildchildnode.ForeColor = Color.Red
                                End If

                            Else
                                If dr("location").ToString <> "" Then
                                    If tempcol <> dr("columns").ToString Then
                                        'magkaiba ibig sabihin new column
                                        tempcol = dr("columns").ToString
                                        childchildnode = New TreeNode()
                                        childchildnode = childnode.Nodes.Add(dr("columns").ToString)
                                        childchildnode.Tag = "Column"
                                        childchildnode.ImageIndex = 5
                                        childchildnode.SelectedImageIndex = 5
                                    Else
                                        'same column ibang item
                                    End If

                                    childchildchildnode = New TreeNode()
                                    childchildchildnode = childchildnode.Nodes.Add("P#" & dr("palletnum").ToString)
                                    childchildchildnode.Tag = dr("logticketid").ToString
                                    If dr("qadispo") = 1 Then
                                        childchildchildnode.ImageIndex = 1
                                        childchildchildnode.SelectedImageIndex = 1
                                    ElseIf dr("qadispo") = 2 Then
                                        childchildchildnode.ImageIndex = 2
                                        childchildchildnode.SelectedImageIndex = 2
                                    Else 'cancelled or wla png dispo
                                        childchildchildnode.ImageIndex = 0
                                        childchildchildnode.SelectedImageIndex = 0
                                    End If
                                    If dr("status") = 3 Then
                                        childchildchildnode.ForeColor = Color.Red
                                    End If
                                End If
                            End If
                        End While
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()

                        'cancelled palletssssss//////////////////////////////////////////////////////
                        sql = "Select tbllogsheet.logsheetid, tbllogsheet.palletizer, tbllogitem.logitemid as litemid, tbllogitem.itemname, tbllogticket.palletnum, tbllogticket.location, tbllogticket.columns, tbllogticket.logticketid, tbllogticket.addtoloc, tbllogticket.logitemid as titemid, tbllogticket.qadispo, tbllogticket.status from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid where tbllogsheet.logsheetnum='" & grdlog.Item(2, grdlog.CurrentRow.Index).Value & "' and tbllogticket.status='3' and tbllogsheet.branch='" & login.branch & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        While dr.Read
                            Dim child As TreeNode
                            Dim childnode As TreeNode
                            Dim childchildnode As TreeNode
                            Dim childchildchildnode As TreeNode

                            If temploc <> dr("location").ToString Then
                                If temploc = "Cancelled" Then
                                    'same location ibang item
                                Else
                                    If dr("status") = 3 Then
                                        'magkaiba ibig sabihin new location
                                        temploc = "Cancelled"
                                        childnode = New TreeNode()
                                        childnode = viewpallets.TreeView1.Nodes(0).Nodes.Add("Cancelled")
                                        childnode.Tag = "Location"
                                        childnode.ImageIndex = 4
                                        childnode.SelectedImageIndex = 4
                                    End If
                                End If
                            Else
                                'same location ibang item
                            End If


                            If temploc = "Cancelled" Then
                                If tempcol <> "0" Then
                                    'magkaiba ibig sabihin new column
                                    tempcol = "0"
                                    childchildnode = New TreeNode()
                                    childchildnode = viewpallets.TreeView1.Nodes(0).Nodes(1).Nodes.Add("0")
                                    childchildnode.Tag = "0"
                                    childchildnode.ImageIndex = 5
                                    childchildnode.SelectedImageIndex = 5
                                Else
                                    'same column ibang item
                                End If

                                childchildchildnode = New TreeNode()
                                childchildchildnode = viewpallets.TreeView1.Nodes(0).Nodes(1).Nodes(0).Nodes.Add("P#" & dr("palletnum").ToString)
                                childchildchildnode.Tag = dr("logticketid").ToString
                                If dr("qadispo") = 1 Then
                                    childchildchildnode.ImageIndex = 1
                                    childchildchildnode.SelectedImageIndex = 1
                                ElseIf dr("qadispo") = 2 Then
                                    childchildchildnode.ImageIndex = 2
                                    childchildchildnode.SelectedImageIndex = 2
                                Else 'cancelled or wla png dispo
                                    childchildchildnode.ImageIndex = 0
                                    childchildchildnode.SelectedImageIndex = 0
                                End If
                                If dr("status") = 3 Then
                                    childchildchildnode.ForeColor = Color.Red
                                End If
                            End If
                        End While
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()

                        '/viewpallets.grouptrans.Visible = True
                        viewpallets.TreeView1.ExpandAll()
                        viewpallets.ShowDialog()
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

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim rb As String = ""
            If rbprod.Checked = True Then
                rb = "<>"
            Else
                rb = "="
            End If

            clickbtn = "Search"

            If Trim(txtlog.Text) <> "" Then
                '/tripsearch()
                sql = "Select tbllogsheet.*, tbllogitem.itemname from tbllogsheet INNER JOIN tbllogitem ON tbllogsheet.logsheetid=tbllogitem.logsheetid"
                sql = sql & " where tbllogsheet.branch='" & login.branch & "' and tbllogsheet.printtype" & rb & "'Receive' and tbllogsheet.logsheetnum='" & lbltrip.Text & Trim(txtlog.Text) & "'"

            ElseIf Trim(txtlogsheetid.Text) <> "" Then
                '/logidsearch()
                sql = "Select tbllogsheet.*, tbllogitem.itemname from tbllogsheet INNER JOIN tbllogitem ON tbllogsheet.logsheetid=tbllogitem.logsheetid"
                sql = sql & " where tbllogsheet.branch='" & login.branch & "' and tbllogsheet.printtype" & rb & "'Receive' and tbllogsheet.logsheetid='" & Trim(txtlogsheetid.Text) & "'"

            ElseIf Trim(cmbitem.Text) <> "" Then
                '/itemsearch()
                sql = "Select tbllogsheet.*, tbllogitem.itemname from tbllogsheet INNER JOIN tbllogitem ON tbllogsheet.logsheetid=tbllogitem.logsheetid"
                sql = sql & " where tbllogsheet.branch='" & login.branch & "' and tbllogsheet.printtype" & rb & "'Receive' and tbllogsheet.logsheetdate>='" & Format(datefrom.Value, "yyyy/MM/dd") & "' and tbllogsheet.logsheetdate<='" & Format(dateto.Value, "yyyy/MM/dd") & "'"
                sql = sql & " and tbllogitem.itemname='" & Trim(cmbitem.Text) & "'"

            Else
                sql = "Select tbllogsheet.*, tbllogitem.itemname from tbllogsheet INNER JOIN tbllogitem on tbllogsheet.logsheetid=tbllogitem.logsheetid"
                sql = sql & " where tbllogsheet.branch='" & login.branch & "' and tbllogsheet.printtype" & rb & "'Receive' and logsheetdate>='" & Format(datefrom.Value, "yyyy/MM/dd") & "' and logsheetdate<='" & Format(dateto.Value, "yyyy/MM/dd") & "'"
            End If

            If cmbwhse.Text <> "" Then
                sql = sql & " and tbllogsheet.whsename='" & cmbwhse.Text & "'"
            End If

            If cmbline.Text <> "" Then
                sql = sql & " and tbllogsheet.palletizer='" & cmbline.Text & "'"
            End If

            If cmbshift.Text <> "" Then
                sql = sql & " and tbllogsheet.shift='" & cmbshift.Text & "'"
            End If

            If chkhide.Checked = True Then
                sql = sql & " and tbllogsheet.status<>'3'"
            End If

            sql = sql & " order by tbllogsheet.logsheetid"

            gridsql = sql

            grdlog.Rows.Clear()

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
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub tripsearch()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            grdlog.Rows.Clear()

            clickbtn = "Search"

            sql = "Select * from tbllogsheet where branch='" & login.branch & "' and logsheetnum='" & lbltrip.Text & Trim(txtlog.Text) & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "In Process"
                    If dr("allitems") = 1 Then
                        stat = "Admin confirmation pending"
                    End If
                ElseIf dr("status") = 0 Then
                    stat = "Queue"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If

                If login.logwhse.ToUpper = "ALL" Then
                    grdlog.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("whsename"), dr("palletizer"), dr("shift"), "", dr("thread").ToString, dr("bagtype").ToString, dr("printtype").ToString, dr("prodrems").ToString, dr("qcarems").ToString, dr("millersup").ToString, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), stat, dr("remarks"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)
                Else
                    If login.logwhse = dr("whsename") Then
                        grdlog.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("whsename"), dr("palletizer"), dr("shift"), "", dr("thread").ToString, dr("bagtype").ToString, dr("printtype").ToString, dr("prodrems").ToString, dr("qcarems").ToString, dr("millersup").ToString, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), stat, dr("remarks"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)
                    End If
                End If

                If dr("status") = 3 Then
                    grdlog.Rows(grdlog.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            'status ng logsheet kung admin pending confirmation nlng or in process
            For Each row As DataGridViewRow In grdlog.Rows
                sql = "Select * from tbllogitem where logsheetid='" & grdlog.Rows(row.Index).Cells(0).Value & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdlog.Rows(row.Index).Cells(6).Value = dr("itemname").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If grdlog.Rows(row.Index).Cells(18).Value = "Admin confirmation pending" Then
                    'check
                    sql = "Select * from tbllogticket where logsheetnum='" & grdlog.Rows(row.Index).Cells(2).Value & "' and (qadispo='0' or qadispo='3') and tbllogticket.status<>'3' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        grdlog.Rows(row.Index).Cells(18).Value = "QA disposition pending"
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Next


            Me.Cursor = Cursors.Default
            If grdlog.Rows.Count = 0 Then
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

    Public Sub logidsearch()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            grdlog.Rows.Clear()

            clickbtn = "Search"

            sql = "Select * from tbllogsheet where branch='" & login.branch & "' and logsheetid='" & Trim(txtlogsheetid.Text) & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "In Process"
                    If dr("allitems") = 1 Then
                        stat = "Admin confirmation pending"
                    End If
                ElseIf dr("status") = 0 Then
                    stat = "Queue"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If

                If login.logwhse.ToUpper = "ALL" Then
                    grdlog.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("whsename"), dr("palletizer"), dr("shift"), "", dr("thread").ToString, dr("bagtype").ToString, dr("printtype").ToString, dr("prodrems").ToString, dr("qcarems").ToString, dr("millersup").ToString, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), stat, dr("remarks"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)
                Else
                    If login.logwhse = dr("whsename") Then
                        grdlog.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("whsename"), dr("palletizer"), dr("shift"), "", dr("thread").ToString, dr("bagtype").ToString, dr("printtype").ToString, dr("prodrems").ToString, dr("qcarems").ToString, dr("millersup").ToString, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), stat, dr("remarks"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)
                    End If
                End If

                If dr("status") = 3 Then
                    grdlog.Rows(grdlog.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            'status ng logsheet kung admin pending confirmation nlng or in process
            For Each row As DataGridViewRow In grdlog.Rows
                sql = "Select * from tbllogitem where logsheetid='" & grdlog.Rows(row.Index).Cells(0).Value & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdlog.Rows(row.Index).Cells(6).Value = dr("itemname").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If grdlog.Rows(row.Index).Cells(18).Value = "Admin confirmation pending" Then
                    'check
                    sql = "Select * from tbllogticket where logsheetnum='" & grdlog.Rows(row.Index).Cells(2).Value & "' and (qadispo='0' or qadispo='3') and tbllogticket.status<>'3' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        grdlog.Rows(row.Index).Cells(18).Value = "QA disposition pending"
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Next


            Me.Cursor = Cursors.Default
            If grdlog.Rows.Count = 0 Then
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

    Public Sub itemsearch()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            clickbtn = "Search"

            grdlog.Rows.Clear()

            sql = "Select * from tbllogsheet RIGHT OUTER JOIN tbllogitem ON tbllogsheet.logsheetnum=tbllogitem.logsheetnum"

            sql = sql & " where tbllogsheet.branch='" & login.branch & "' and tbllogsheet.logsheetdate>='" & Format(datefrom.Value, "yyyy/MM/dd") & "' and tbllogsheet.logsheetdate<='" & Format(dateto.Value, "yyyy/MM/dd") & "'"

            If cmbwhse.Text <> "" Then
                sql = sql & " and tbllogsheet.whsename='" & cmbwhse.Text & "'"
            End If

            If cmbline.Text <> "" Then
                sql = sql & " and tbllogsheet.palletizer='" & cmbline.Text & "'"
            End If

            If cmbshift.Text <> "" Then
                sql = sql & " and tbllogsheet.shift='" & cmbshift.Text & "'"
            End If

            If chkhide.Checked = True Then
                sql = sql & " and tbllogsheet.status<>'3'"
            End If

            sql = sql & " and tbllogitem.itemname='" & Trim(cmbitem.Text) & "'"

            sql = sql & " order by tbllogsheet.logsheetid"

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "In Process"
                    If dr("allitems") = 1 Then
                        stat = "Admin confirmation pending"
                    End If
                ElseIf dr("status") = 0 Then
                    stat = "Queue"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If

                If login.logwhse.ToUpper = "ALL" Then
                    grdlog.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("whsename"), dr("palletizer"), dr("shift"), "", dr("thread").ToString, dr("bagtype").ToString, dr("printtype").ToString, dr("prodrems").ToString, dr("qcarems").ToString, dr("millersup").ToString, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), stat, dr("remarks"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)
                Else
                    If login.logwhse = dr("whsename") Then
                        grdlog.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("whsename"), dr("palletizer"), dr("shift"), "", dr("thread").ToString, dr("bagtype").ToString, dr("printtype").ToString, dr("prodrems").ToString, dr("qcarems").ToString, dr("millersup").ToString, dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), stat, dr("remarks"), dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)
                    End If
                End If

                If dr("status") = 3 Then
                    grdlog.Rows(grdlog.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            'status ng logsheet kung admin pending confirmation nlng or in process
            For Each row As DataGridViewRow In grdlog.Rows
                sql = "Select * from tbllogitem where logsheetid='" & grdlog.Rows(row.Index).Cells(0).Value & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdlog.Rows(row.Index).Cells(6).Value = dr("itemname").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If grdlog.Rows(row.Index).Cells(18).Value = "Admin confirmation pending" Then
                    'check
                    sql = "Select * from tbllogticket where logsheetnum='" & grdlog.Rows(row.Index).Cells(2).Value & "' and (qadispo='0' or qadispo='3') and tbllogticket.status<>'3' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        grdlog.Rows(row.Index).Cells(18).Value = "QA disposition pending"
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Next


            Me.Cursor = Cursors.Default

            If grdlog.Rows.Count = 0 Then
                MsgBox("No Record Found.", MsgBoxStyle.Critical, "")
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

    Private Sub txtlog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtlog.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/tripsearch()
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtlog_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlog.TextChanged
        If Trim(txtlog.Text) <> "" Then
            cmbwhse.Enabled = False
            cmbline.Enabled = False
            cmbshift.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
            cmbitem.Enabled = False
            txtlogsheetid.Enabled = False
            cmbitem.Text = ""
            txtlogsheetid.Text = ""
        Else
            cmbwhse.Enabled = True
            cmbline.Enabled = True
            cmbshift.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            cmbitem.Enabled = True
            txtlogsheetid.Enabled = True
        End If
    End Sub

    Private Sub PrintTicketToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintTicketToolStripMenuItem.Click
        Try
            If grdlog.SelectedCells.Count = 1 Or grdlog.SelectedRows.Count = 1 Then
                If grdlog.Rows(selectedrow).Cells(18).Value.ToString = "Completed" Or grdlog.Rows(selectedrow).Cells(18).Value.ToString = "Admin confirmation pending" Then
                    rptlogsheetrevise.stat = ""
                    rptlogsheetrevise.lognum = grdlog.Rows(selectedrow).Cells(2).Value
                    rptlogsheetrevise.ShowDialog()

                    If clickbtn = "Search" Then
                        btnsearch.PerformClick()
                    Else
                        btnview.PerformClick()
                    End If
                Else
                    sql = "Select * from tbllogticket where logsheetnum='" & grdlog.Rows(selectedrow).Cells(2).Value & "' and (qadispo='0' or qadispo='3') and tbllogticket.status<>'3' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MsgBox("Cannot print Ticket Log Sheet. There are pallets that are still pending for QA disposition.", MsgBoxStyle.Information, "")
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()


                    MsgBox("Cannot print pending Ticket Log Sheet.", MsgBoxStyle.Information, "")

                    '/Dim a As String = MsgBox("Log sheet is pending. Do you want to print it anyway?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                    '/If a = vbYes Then
                    '/rptlogsheetrevise.stat = "Pending"
                    '/rptlogsheetrevise.lognum = grdlog.Rows(selectedrow).Cells(2).Value
                    '/rptlogsheetrevise.ShowDialog()
                    '/End If
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

    Private Sub grdlog_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdlog.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then

                grdlog.ClearSelection()
                grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True

                selectedrow = e.RowIndex
                Me.ContextMenuStrip1.Show(Cursor.Position)

                If login.depart = "Admin Dispatching" Then
                    AdminConfirmLogsheetToolStripMenuItem.Visible = True
                Else
                    AdminConfirmLogsheetToolStripMenuItem.Visible = False
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

    Private Sub grdlog_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles grdlog.RowPrePaint
        If e.RowIndex > -1 Then
            Dim dgvRow As DataGridViewRow = grdlog.Rows(e.RowIndex)
            '<== But this is the name assigned to it in the properties of the control
            If dgvRow.Cells(18).Value = "Cancelled" Then 'step1
                dgvRow.DefaultCellStyle.BackColor = Color.DeepSkyBlue
            End If
        End If
    End Sub

    Private Sub grdlog_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdlog.SelectionChanged
        count()
    End Sub

    Public Sub count()
        Try
            lblcount.Text = "     Selected Rows Count: " & grdlog.SelectedRows.Count
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub datefrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles datefrom.ValueChanged
        dateto.MinDate = datefrom.Value
    End Sub

    Private Sub AdminConfirmLogsheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdminConfirmLogsheetToolStripMenuItem.Click
        Try
            'check level of users
            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdlog.SelectedCells.Count = 1 Or grdlog.SelectedRows.Count = 1 Then
                'check if allitems=1
                sql = "Select * from tbllogsheet where logsheetnum='" & grdlog.Rows(selectedrow).Cells(2).Value & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("allitems") = 1 Then
                        trems = dr("remarks")
                    Else
                        MsgBox("Cannot confirm. Some pallets are pending.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    If dr("status") = 3 Then
                        MsgBox("Cannot confirm. Log Sheet is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        '/MsgBox("Cannot confirm. Log Sheet is already confirmed.", MsgBoxStyle.Exclamation, "")
                        '/Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                'check if all logticket has qadispo
                sql = "Select * from tbllogsheet right outer join tbllogticket on tbllogsheet.logsheetnum=tbllogticket.logsheetnum where tbllogsheet.logsheetnum='" & grdlog.Rows(selectedrow).Cells(2).Value & "' and tbllogticket.status<>'3' and tbllogsheet.status<>'3' and tbllogsheet.branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If dr("qadispo") = 0 Then
                        MsgBox("Cannot confirm. Some pallets are pending for QA disposition.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                'remarks yung sap number
                ticketrems.txtrems.Text = trems
                ticketrems.ShowDialog()
                If trems <> "" Then
                    adlogcnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If adlogcnf = True Then
                        'update ststus ng log sheet to 2 meaning confirmed and nde na available
                        sql = "Update tbllogsheet set status='2', remarks='" & trems & "', datemodified=GetDate(), modifiedby='" & login.user & "' where logsheetnum='" & grdlog.Rows(selectedrow).Cells(2).Value & "' and branch='" & login.branch & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Ticket Log Sheet# " & grdlog.Rows(selectedrow).Cells(2).Value & " confirmed.", MsgBoxStyle.Information, "")

                        If clickbtn = "Search" Then
                            btnsearch.PerformClick()
                        Else
                            btnview.PerformClick()
                        End If
                    End If
                Else
                    MsgBox("User cancelled confirmation.", MsgBoxStyle.Information, "")
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

    Private Sub chkhide_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkhide.CheckedChanged

    End Sub

    Private Sub CancelLogsheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelLogsheetToolStripMenuItem.Click
        Try
            'check level of users
            If login.depart <> "All" And login.depart <> "Production" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdlog.SelectedCells.Count = 1 Or grdlog.SelectedRows.Count = 1 Then
                'check if logsheet is not cancelled
                sql = "Select * from tbllogsheet where logsheetid='" & grdlog.Rows(selectedrow).Cells(0).Value & "' and tbllogsheet.status='3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Logsheet is already cancelled.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                Dim a As String = MsgBox("Are you sure you want to cancel logsheet?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbNo Then
                    Exit Sub
                Else
                    tlogcancel.lbllogid.Text = grdlog.Rows(selectedrow).Cells(0).Value
                    tlogcancel.lbllognum.Text = grdlog.Rows(selectedrow).Cells(2).Value
                    tlogcancel.ShowDialog()
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

    Private Sub ActivateLogsheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActivateLogsheetToolStripMenuItem.Click
        Try
            If grdlog.SelectedCells.Count = 1 Or grdlog.SelectedRows.Count = 1 Then
                'check if hindi completed at hindi cancel ung logsheet at allitems is one
                Dim okallitems As Boolean = False

                sql = "Select * from tbllogsheet where logsheetid='" & grdlog.Rows(selectedrow).Cells(0).Value & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 2 Then
                        MsgBox("Cannot activate logsheet that is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 3 Then
                        MsgBox("Cannot activate logsheet that is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    Else
                        If dr("allitems") = 1 Then
                            okallitems = True
                        Else
                            MsgBox("Cannot activate logsheet that is still pending.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If okallitems = True Then
                    adlogcnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If adlogcnf = True Then
                        sql = "Update tbllogsheet set allitems='0' where logsheetid='" & grdlog.Rows(selectedrow).Cells(0).Value & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Dispose()

                        MsgBox("Successfully activated.", MsgBoxStyle.Information, "")

                        If clickbtn = "Search" Then
                            btnsearch.PerformClick()
                        Else
                            btnview.PerformClick()
                        End If
                    End If
                End If
            Else
                MsgBox("Select one only.", MsgBoxStyle.Information, "")
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

    Private Sub ActivateAdminConfirmationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActivateAdminConfirmationToolStripMenuItem.Click
        Try
            If grdlog.SelectedCells.Count = 1 Or grdlog.SelectedRows.Count = 1 Then
                'check if logsheet is completed
                sql = "Select * from tbllogsheet where logsheetid='" & grdlog.Rows(selectedrow).Cells(0).Value & "' and status='2'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then

                Else
                    MsgBox("Cannot activate logsheet admin confirmation.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                adlogcnf = False
                confirm.GroupBox1.Text = login.wgroup
                confirm.ShowDialog()
                If adlogcnf = True Then
                    sql = "Update tbllogsheet set status='1' where logsheetid='" & grdlog.Rows(selectedrow).Cells(0).Value & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Dispose()

                    MsgBox("Successfully activated admin confirmation.", MsgBoxStyle.Information, "")

                    If clickbtn = "Search" Then
                        btnsearch.PerformClick()
                    Else
                        btnview.PerformClick()
                    End If
                End If
            Else
                MsgBox("Select one only.", MsgBoxStyle.Information, "")
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

    Private Sub cmbitem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbitem.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbitem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbitem.SelectedIndexChanged

    End Sub

    Private Sub txtlogsheetid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtlogsheetid.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/logidsearch()
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtlogsheetid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlogsheetid.TextChanged
        Dim charactersDisallowed As String = "0123456789"
        Dim theText As String = txtlogsheetid.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtlogsheetid.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtlogsheetid.Text.Length - 1
            Letter = txtlogsheetid.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtlogsheetid.Text = theText
        txtlogsheetid.Select(SelectionIndex - Change, 0)


        If Trim(txtlogsheetid.Text) <> "" Then
            cmbwhse.Enabled = False
            cmbline.Enabled = False
            cmbshift.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
            cmbitem.Enabled = False
            txtlog.Enabled = False
            cmbitem.Text = ""
            txtlog.Text = ""
        Else
            cmbwhse.Enabled = True
            cmbline.Enabled = True
            cmbshift.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            cmbitem.Enabled = True
            txtlog.Enabled = True
        End If
    End Sub

    Private Sub ChangeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeToolStripMenuItem.Click
        Try
            'check level of users
            If login.depart <> "All" And login.depart <> "Production" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdlog.SelectedCells.Count = 1 Or grdlog.SelectedRows.Count = 1 Then
                'check if logsheet is not cut off
                sql = "Select status from tbllogsheet where logsheetid='" & grdlog.Rows(selectedrow).Cells(0).Value & "' and tbllogsheet.status='1' and allitems='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Logsheet is already cut off.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                tlogbin.lblid.Text = grdlog.Rows(selectedrow).Cells(0).Value
                tlogbin.lbllognum.Text = grdlog.Rows(selectedrow).Cells(2).Value
                tlogbin.ShowDialog()

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

    Private Sub ViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewToolStripMenuItem.Click
        viewbin.lblid.Text = grdlog.Rows(selectedrow).Cells(0).Value
        viewbin.lbllognum.Text = grdlog.Rows(selectedrow).Cells(2).Value
        viewbin.ShowDialog()
    End Sub

    Private Sub rbreceive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbreceive.CheckedChanged

    End Sub
End Class