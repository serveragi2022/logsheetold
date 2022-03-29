Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.ComponentModel

Public Class receiveinfo
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public adlogcnf As Boolean, trems As String, adlogby As String
    Dim clickbtn As String = "", rec_clickbtn As String = "", selectedrow As Integer, rec_selectedrow As Integer, gridsql As String, logbranch As String, loginwhse As String

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

    Private Sub receiveinfo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub receiveinfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        rec_viewpending()

        'viewwhse()
        'viewline()
        'viewshift()
        'viewpending()
        'viewitems()
        'viewpallets.TreeView1.ImageList = ImageList1
        'cmbitem.DropDownWidth = 300

        logbranch = login.branch
        loginwhse = login.logwhse
        '/rbreceive.Checked = True

        'If login.depart = "All" Then
        '    ActivateLogsheetToolStripMenuItem.Visible = True
        '    ActivateAdminConfirmationToolStripMenuItem.Visible = True
        'Else
        '    ActivateLogsheetToolStripMenuItem.Visible = False
        '    ActivateAdminConfirmationToolStripMenuItem.Visible = False
        'End If
    End Sub

   
    Private Sub ToolSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolSearch.Click
        If ToolSearch.ToolTipText = "Open Search Panel" Then
            PanelSearch.Visible = True
            grdrec.Location = New Point(3, 215)
            grdrec.Height = grdrec.Height - 181
            ToolSearch.ToolTipText = "Close Search Panel"
        Else
            PanelSearch.Visible = False
            grdrec.Location = New Point(3, 28)
            grdrec.Height = grdrec.Height + 181
            ToolSearch.ToolTipText = "Open Search Panel"
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewToolStripButton1.Click
        rec_viewpending()
    End Sub

    Private Sub rec_viewpending()
        Try
            rec_clickbtn = "Receive Pending"

            grdrec.Rows.Clear()

            Dim stat As String = ""
            sql = "Select recid, recdate, recnum, platenum, driver, status, datecreated, createdby, datecancelled, cancelledby, creason  from tblreceive where status='1' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("status") = 1 Then
                    stat = "In Process"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If
                grdrec.Rows.Add(dr("recid"), dr("recdate"), dr("recnum"), dr("platenum"), dr("driver"), stat, dr("datecreated"), dr("createdby"), dr("datecancelled"), dr("cancelledby"), dr("creason"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grdrec.Rows.Count <> 0 Then
                Dim eventArgs = New DataGridViewCellEventArgs(1, 0)
                grdrec.Rows(0).Cells(0).Selected = True
                grdrec_CellClick(grdrec, eventArgs)
            Else
                grdlog.Rows.Clear()
                grdof.Rows.Clear()
                txtid.Text = ""
                txtdate.Text = ""
                txtbranch.Text = ""
                txtqty.Text = ""
                txtstatus.Text = ""
                txtrems.Text = ""
                txtcby.Text = ""
                txtcdate.Text = ""
                txtcnlby.Text = ""
                txtcnldate.Text = ""
                txtcnlreason.Text = ""
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

    Private Sub grdrec_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdrec.CellClick
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim recid As String = grdrec.Rows(e.RowIndex).Cells(0).Value
            Dim recnum As String = grdrec.Rows(e.RowIndex).Cells(2).Value

            sql = "Select recid, recdate, frmbranch, qty, status, remarks, createdby, datecreated, cancelledby, datecancelled, creason"
            sql = sql & " from tblreceive where branch='" & login.branch & "' and recid='" & recid & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                txtid.Text = dr("recid")
                txtdate.Text = dr("recdate")
                txtbranch.Text = dr("frmbranch").ToString.ToUpper
                txtqty.Text = dr("qty")
                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "In Process"
                Else
                    stat = "Completed"
                End If
                txtstatus.Text = stat
                txtrems.Text = dr("remarks")
                txtcby.Text = dr("createdby")
                txtcdate.Text = dr("datecreated")
                txtcnlby.Text = dr("cancelledby").ToString
                txtcnldate.Text = dr("datecancelled").ToString
                txtcnlreason.Text = dr("creason").ToString
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            grdof.Rows.Clear()
            sql = "Select ofid, wrsnum, ofnum from tblrecof"
            sql = sql & " where recnum='" & recnum & "' and (branch='" & login.branch & "' or branch is null)"
            sql = sql & " group by ofid, wrsnum, ofnum"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                grdof.Rows.Add(dr("ofid"), dr("wrsnum"), dr("ofnum"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            view_logsheet(recnum)

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub view_logsheet(ByVal recnum As String)
        Try
            sql = "Select tbllogsheet.*, tbllogitem.itemname from tbllogsheet INNER JOIN tbllogitem ON tbllogsheet.logsheetid=tbllogitem.logsheetid"
            sql = sql & " where tbllogsheet.branch='" & login.branch & "' and tbllogsheet.printtype='Receive' and tbllogsheet.recnum='" & recnum & "'"

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
        Catch ex As Exception
            MsgBox(ex.ToString)
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
                grdlog.Invoke(m_addRowDelegate, drx("logsheetid"), Format(drx("logsheetdate"), "yyyy/MM/dd"), drx("logsheetnum"), drx("whsename"), drx("palletizer"), drx("shift"), drx("itemname"), stat, drx("thread").ToString, drx("bagtype").ToString, drx("printtype").ToString, drx("binnum").ToString, drx("prodrems").ToString, drx("qcarems").ToString, drx("millersup").ToString, drx("datecreated"), drx("createdby"), drx("datemodified"), drx("modifiedby"), drx("remarks"), drx("canceldate").ToString, drx("cancelby").ToString, drx("cancelreason").ToString, i)
            Else
                AddDGVRow(drx("logsheetid"), Format(drx("logsheetdate"), "yyyy/MM/dd"), drx("logsheetnum"), drx("whsename"), drx("palletizer"), drx("shift"), drx("itemname"), stat, drx("thread").ToString, drx("bagtype").ToString, drx("printtype").ToString, drx("binnum").ToString, drx("prodrems").ToString, drx("qcarems").ToString, drx("millersup").ToString, drx("datecreated"), drx("createdby"), drx("datemodified"), drx("modifiedby"), drx("remarks"), drx("canceldate").ToString, drx("cancelby").ToString, drx("cancelreason").ToString, i)
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
        'lblloading.Visible = True
        'Panel1.Enabled = False
        'pgb1.Style = ProgressBarStyle.Marquee
        'pgb1.Visible = True
        'pgb1.Minimum = 0
    End Sub

    Private Sub backgroundWorkersteps_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        threadEnabledsteps = True
        Dim i As Integer = 0
        'status ng logsheet kung admin pending confirmation nlng or in process
        For Each row As DataGridViewRow In grdlog.Rows
            If grdlog.Rows(row.Index).Cells(7).Value = "Admin confirmation pending" Then
                'check
                sql = "Select t.status from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                sql = sql & " where t.logsheetid='" & grdlog.Rows(row.Index).Cells(0).Value & "' and (t.qadispo='0' or t.qadispo='3') and t.status<>'3' and s.branch='" & logbranch & "'"
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
                grdlog.Rows(rowin).Cells(7).Value = vstat
            End If
        End If
    End Sub

    Private Sub backgroundWorkersteps_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        '/Me.Label2.Text = e.ProgressPercentage.ToString() & "% complete"
    End Sub

    Private Sub backgroundWorkersteps_Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Me.Cursor = Cursors.Default
        'pgb1.Visible = False
        'pgb1.Style = ProgressBarStyle.Blocks
        'lblloading.Visible = False
        'Panel1.Enabled = True
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
                    'MsgBox("Loading data completed.", MsgBoxStyle.Information, "")
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


                        sql = "Select s.logsheetid, s.palletizer, i.logitemid as litemid, i.itemname, t.palletnum,"
                        sql = sql & " t.location, t.columns, t.logticketid, t.addtoloc, t.logitemid as titemid,"
                        sql = sql & " t.qadispo, t.status from tbllogsheet s right outer join tbllogitem i on s.logsheetid=i.logsheetid"
                        sql = sql & " right outer join tbllogticket t on i.logitemid=t.logitemid"
                        sql = sql & " where s.branch='" & login.branch & "' and s.logsheetnum='" & grdlog.Item(2, grdlog.CurrentRow.Index).Value & "' order by location, columns"
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
                        sql = "Select s.logsheetid, s.palletizer, i.logitemid as litemid, i.itemname, t.palletnum, t.location, t.columns, t.logticketid, "
                        sql = sql & " t.addtoloc, t.logitemid as titemid, t.qadispo, t.status from tbllogsheet s "
                        sql = sql & " right outer join tbllogitem i on s.logsheetid=i.logsheetid right outer join tbllogticket t on i.logitemid=t.logitemid "
                        sql = sql & " where s.logsheetnum='" & grdlog.Item(2, grdlog.CurrentRow.Index).Value & "' and t.status='3' and s.branch='" & login.branch & "'"
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

    Private Sub CancelReceiveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelReceiveToolStripMenuItem.Click
        Try
            'check level of users
            If login.depart <> "All" And login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdrec.SelectedCells.Count = 1 Or grdrec.SelectedRows.Count = 1 Then
                'check if logsheet is not cancelled
                sql = "Select recid from tblreceive where recid='" & grdrec.Rows(rec_selectedrow).Cells(0).Value & "' and status='3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Receive ID " & grdrec.Rows(rec_selectedrow).Cells(0).Value & " is already cancelled.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if logsheet is not cancelled
                Dim aa As Boolean = False
                sql = "Select logsheetnum from tbllogsheet where recnum='" & grdrec.Rows(rec_selectedrow).Cells(2).Value & "' and allitems='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    Dim ad As String = MsgBox("Log sheet# " & dr("logsheetnum") & " is already cut off. Do you want to continue?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                    If ad = vbYes Then
                        aa = True
                    Else
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if logsheet is not cancelled
                If aa = True Then
                    For Each row As DataGridViewRow In grdlog.Rows
                        Dim logsheet As String = grdlog.Rows(row.Index).Cells(2).Value
                        sql = "Select l.ofnum from tbloflog l inner join tblorderfill o on l.ofnum=o.ofnum"
                        sql = sql & " where l.logsheetnum='" & logsheet & "' and l.status <>'3' and o.branch='" & login.branch & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            MsgBox("Cannot cancel. Log sheet# " & logsheet & " is already used in order fill (" & dr("ofnum") & ").", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()
                    Next
                End If

                Dim a As String = MsgBox("Are you sure you want to cancel?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbNo Then
                    Exit Sub
                Else
                    reccancel.lbllogid.Text = grdrec.Rows(rec_selectedrow).Cells(0).Value
                    reccancel.lbllognum.Text = grdrec.Rows(rec_selectedrow).Cells(2).Value
                    reccancel.ShowDialog()
                End If

                If rec_clickbtn = "Search" Then
                    btnrecsearch.PerformClick()
                Else
                    ViewToolStripButton1.PerformClick()
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

    Private Sub grdrec_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdrec.CellContentClick

    End Sub

    Private Sub grdrec_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdrec.CellMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then

            grdrec.ClearSelection()
            grdrec.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True

            rec_selectedrow = e.RowIndex
            Me.ContextMenuStrip1.Show(Cursor.Position)
            CancelReceiveToolStripMenuItem.Visible = True
            PrintLogSheetToolStripMenuItem.Visible = False
            If login.depart = "Admin Dispatching" Or login.depart = "All" Then
                AdminConfirmToolStripMenuItem.Visible = True
            Else
                AdminConfirmToolStripMenuItem.Visible = False
            End If
            If grdrec.Rows(rec_selectedrow).Cells(5).Value = "Completed" Then
                PrintReceiveToolStripMenuItem.Visible = True
            Else
                PrintReceiveToolStripMenuItem.Visible = False
            End If
        End If
    End Sub

    Private Sub grdrec_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles grdrec.RowPrePaint
        If e.RowIndex > -1 Then
            Dim dgvRow As DataGridViewRow = grdrec.Rows(e.RowIndex)
            '<== But this is the name assigned to it in the properties of the control
            If dgvRow.Cells(5).Value = "Cancelled" Then 'step1
                dgvRow.DefaultCellStyle.BackColor = Color.DeepSkyBlue
            ElseIf dgvRow.Cells(5).Value = "Completed" Then 'step1
                dgvRow.DefaultCellStyle.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub grdlog_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdlog.CellMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then

            grdlog.ClearSelection()
            grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True

            selectedrow = e.RowIndex
            Me.ContextMenuStrip1.Show(Cursor.Position)
            PrintLogSheetToolStripMenuItem.Visible = True
            AdminConfirmToolStripMenuItem.Visible = False
            CancelReceiveToolStripMenuItem.Visible = False
            PrintReceiveToolStripMenuItem.Visible = False
        End If
    End Sub

    Private Sub grdlog_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles grdlog.RowPrePaint
        If e.RowIndex > -1 Then
            Dim dgvRow As DataGridViewRow = grdlog.Rows(e.RowIndex)
            '<== But this is the name assigned to it in the properties of the control
            If dgvRow.Cells(7).Value = "Cancelled" Then 'step1
                dgvRow.DefaultCellStyle.BackColor = Color.DeepSkyBlue
            End If
        End If
    End Sub

    Private Sub AdminConfirmToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdminConfirmToolStripMenuItem.Click
        Try
            'check level of users
            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            
            If grdrec.SelectedCells.Count = 1 Or grdrec.SelectedRows.Count = 1 Then
                'check if recnum is not cancelled
                sql = "Select recid from tblreceive where recid='" & grdrec.Rows(rec_selectedrow).Cells(0).Value & "' and status='3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Receive ID " & grdrec.Rows(rec_selectedrow).Cells(0).Value & " is already cancelled.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                'check if allitems=1
                sql = "Select allitems from tbllogsheet l"
                sql = sql & " where l.branch='" & login.branch & "' and l.recnum='" & grdrec.Rows(rec_selectedrow).Cells(2).Value & "' and l.status='1' and allitems='0'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Cannot confirm. Some pallets are pending.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                'check if all logticket has qadispo
                sql = "select qadispo from tbllogticket t"
                sql = sql & " inner join tbllogsheet l on t.logsheetid=l.logsheetid"
                sql = sql & " where l.branch='" & login.branch & "' and l.recnum='" & grdrec.Rows(rec_selectedrow).Cells(2).Value & "' and l.status='1' and t.status<>'3' and t.qadispo='0'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Cannot confirm. Some pallets are pending for QA disposition.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                'remarks yung sap number
                ticketrems.txtrems.Text = trems
                ticketrems.frm = "receiveinfo"
                ticketrems.ShowDialog()
                If trems <> "" Then
                    adlogcnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If adlogcnf = True Then
                        ExecuteAdminConfirm(strconn)
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

    Private Sub ExecuteAdminConfirm(ByVal connectionString As String)
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
                'update ststus ng log sheet to 2 meaning confirmed and nde na available
                command.CommandText = "Update tbllogsheet set status='2', remarks='" & trems & "', datemodified=GetDate(), modifiedby='" & login.user & "'"
                command.CommandText = command.CommandText & " where recnum='" & grdrec.Rows(rec_selectedrow).Cells(2).Value & "' and branch='" & login.branch & "' and status<>'3'"
                command.ExecuteNonQuery()

                'receive update status=2
                command.CommandText = "Update tblreceive set status='2', datemodified=GetDate(), modifiedby='" & login.user & "'"
                command.CommandText = command.CommandText & " where recid='" & grdrec.Rows(rec_selectedrow).Cells(0).Value & "' and branch='" & login.branch & "' and status<>'3'"
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Receive ID# " & grdrec.Rows(rec_selectedrow).Cells(0).Value & " confirmed.", MsgBoxStyle.Information, "")

                If clickbtn = "Search" Then
                    btnrecsearch.PerformClick()
                Else
                    ViewToolStripButton1.PerformClick()
                End If

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.Message, MsgBoxStyle.Exclamation, "")
                ' Attempt to roll back the transaction. 
                Try
                    Me.Cursor = Cursors.Default
                    transaction.Rollback()
                Catch ex2 As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox("2: " & ex2.Message & vbCrLf & vbCrLf & "Please try again.", MsgBoxStyle.Information, "")
                End Try
            End Try
        End Using
    End Sub

    Private Sub btnrecsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrecsearch.Click
        Try
            rec_clickbtn = "Receive Search"

            Dim firstDate As Date = recdfrom.Value
            Dim secondDate As Date = recdto.Value
            Dim diff2 As String = (secondDate - firstDate).TotalDays.ToString()

            If diff2.ToString > "7" Then
                MsgBox("Invalid date.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            grdrec.Rows.Clear()

            Dim stat As String = ""

            sql = "Select r.recid, r.recdate, r.recnum, r.platenum, r.driver, r.status, r.datecreated, r.createdby, r.datecancelled, r.cancelledby, r.creason"
            If Trim(txtrecid.Text) <> "" Then
                sql = sql & " from tblreceive r where r.branch='" & login.branch & "'"
                sql = sql & " and r.recid='" & Trim(txtrecid.Text) & "'"
            ElseIf Trim(txtwrs.Text) <> "" Then
                sql = sql & " from tblreceive r inner join tblrecof o on r.recnum=o.recnum where r.branch='" & login.branch & "' and o.branch='" & login.branch & "'"
                sql = sql & " and o.wrsnum='" & Trim(txtwrs.Text) & "'"
            ElseIf Trim(txtofnum.Text) <> "" Then 'lblof
                sql = sql & " from tblreceive r inner join tblrecof o on r.recnum=o.recnum where r.branch='" & login.branch & "' and o.branch='" & login.branch & "'"
                sql = sql & " and o.ofnum='" & lblof.Text & Trim(txtofnum.Text) & "'"
            Else
                sql = sql & " from tblreceive r where r.branch='" & login.branch & "'"
                sql = sql & " and r.recdate>='" & Format(recdfrom.Value, "yyyy/MM/dd") & "' and r.recdate<='" & Format(recdto.Value, "yyyy/MM/dd") & "'"
            End If
            If chkrechide.Checked = True Then
                sql = sql & " and r.status<>'3'"
            End If
            sql = sql & "  group by r.recid, r.recdate, r.recnum, r.platenum, r.driver, r.status, r.datecreated, r.createdby, r.datecancelled, r.cancelledby, r.creason"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("status") = 1 Then
                    stat = "In Process"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If
                grdrec.Rows.Add(dr("recid"), dr("recdate"), dr("recnum"), dr("platenum"), dr("driver"), stat, dr("datecreated"), dr("createdby"), dr("datecancelled"), dr("cancelledby"), dr("creason"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grdrec.Rows.Count <> 0 Then
                Dim eventArgs = New DataGridViewCellEventArgs(1, 0)
                grdrec.Rows(0).Cells(0).Selected = True
                grdrec_CellClick(grdrec, eventArgs)
            Else
                grdlog.Rows.Clear()
                grdof.Rows.Clear()
                txtid.Text = ""
                txtdate.Text = ""
                txtbranch.Text = ""
                txtqty.Text = ""
                txtstatus.Text = ""
                txtrems.Text = ""
                txtcby.Text = ""
                txtcdate.Text = ""
                txtcnlby.Text = ""
                txtcnldate.Text = ""
                txtcnlreason.Text = ""

                MsgBox("No record found.", MsgBoxStyle.Critical, "")
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

    Private Sub txtrecid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrecid.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnrecsearch.PerformClick()
        End If
    End Sub

    Private Sub txtrecid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrecid.TextChanged
        If Trim(txtrecid.Text) <> "" Then
            txtwrs.Enabled = False
            txtofnum.Enabled = False
        Else
            txtwrs.Enabled = True
            txtofnum.Enabled = True
        End If
    End Sub

    Private Sub txtwrs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwrs.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnrecsearch.PerformClick()
        End If
    End Sub

    Private Sub txtwrs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwrs.TextChanged
        If Trim(txtwrs.Text) <> "" Then
            txtrecid.Enabled = False
            txtofnum.Enabled = False
        Else
            txtrecid.Enabled = True
            txtofnum.Enabled = True
        End If
    End Sub

    Private Sub txtofnum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtofnum.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnrecsearch.PerformClick()
        End If
    End Sub

    Private Sub txtofnum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtofnum.TextChanged
        If Trim(txtofnum.Text) <> "" Then
            txtrecid.Enabled = False
            txtwrs.Enabled = False
        Else
            txtrecid.Enabled = True
            txtwrs.Enabled = True
        End If
    End Sub

    Private Sub PrintLogSheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintLogSheetToolStripMenuItem.Click
        Try
            If grdlog.SelectedCells.Count = 1 Or grdlog.SelectedRows.Count = 1 Then
                If grdlog.Rows(selectedrow).Cells(7).Value.ToString = "Completed" Or grdlog.Rows(selectedrow).Cells(7).Value.ToString = "Admin confirmation pending" Then
                    rptlogsheetrevise.stat = ""
                    rptlogsheetrevise.lognum = grdlog.Rows(selectedrow).Cells(2).Value
                    rptlogsheetrevise.ShowDialog()

                Else
                    sql = "Select t.status from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                    sql = sql & " where t.logsheetid='" & grdlog.Rows(selectedrow).Cells(0).Value & "' and (t.qadispo='0' or t.qadispo='3') and t.status<>'3' and s.branch='" & login.branch & "'"
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


                    MsgBox("Cannot print pending Ticket Log Sheet.", MsgBoxStyle.Exclamation, "")

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

    Private Sub PrintReceiveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintReceiveToolStripMenuItem.Click
        If grdrec.Rows(rec_selectedrow).Cells(5).Value = "Completed" Then
            rptreceive.recid = grdrec.Rows(rec_selectedrow).Cells(0).Value
            rptreceive.ShowDialog()
        Else
            MsgBox("Cannot print in process receive.", MsgBoxStyle.Exclamation, "")
        End If
    End Sub
End Class