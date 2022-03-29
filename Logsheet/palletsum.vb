Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.ComponentModel

Public Class palletsum
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim clickbtn As String, gridsql As String, loginwhse As String, loginbranch As String
    Public palsumcnf As Boolean = False
    Dim selectedrow As Integer
    Dim rcount As Integer = 0
    Dim ofnum_list As New List(Of String)

    Private threadEnabled As Boolean
    Private bwork As BackgroundWorker, bworkstat As BackgroundWorker, bworkofn As BackgroundWorker

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

    Private Sub palletsum_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub palletsum_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewwhse()
        viewshift()
        viewline()
        viewitem()
        viewpallets.TreeView1.ImageList = ImageList1
        loginwhse = login.logwhse
        loginbranch = login.branch

        If login.logwhse.ToUpper = "ALL" Then
            cmbwhse.Enabled = True
            cmbwhse.Text = ""
        Else
            cmbwhse.Text = login.logwhse
            cmbwhse.Enabled = False
        End If

        'viewpending()
    End Sub

    Public Sub viewwhse()
        Try
            cmbwhse.Items.Clear()
            cmbwhse.Items.Add("")

            sql = "Select * from tblwhse where branch='" & login.branch & "' order by whsename"
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

            sql = "Select * from tblshift order by shiftcode"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbshift.Items.Add(dr("shiftcode"))
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

    Public Sub viewline()
        Try
            cmbline.Items.Clear()
            cmbline.Items.Add("")

            sql = "Select * from tblpalletizer where branch='" & login.branch & "' order by palletizer"
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
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub viewitem()
        Try
            cmbitem.Items.Clear()
            cmbitem.Items.Add("")

            sql = "Select * from tblitems order by itemname"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If cmbitem.Items.Contains(dr("itemname")) = False Then
                    cmbitem.Items.Add(dr("itemname"))
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If cmbitem.Items.Count <> 0 Then
                cmbitem.Items.Add("All")
            End If

            cmbitem.DropDownWidth = 300

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
        Try
            If Trim(cmbwhse.Text) <> "" And Trim(cmbwhse.Text) <> "All" Then
                cmbloc.Items.Clear()

                sql = "Select * from tbllocation where whsename='" & Trim(cmbwhse.Text) & "' and branch='" & login.branch & "' order by location"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    cmbloc.Items.Add(dr("location"))
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            ElseIf Trim(cmbwhse.Text) = "All" Then
                cmbloc.Items.Clear()

                sql = "Select * from tbllocation order by location"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    cmbloc.Items.Add(dr("location"))
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            Else
                cmbloc.Items.Clear()
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

    Private Sub btnview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnview.Click
        cmbline.Text = ""
        txtlog.Text = ""
        viewpending()
    End Sub

    Public Sub viewpending()

        Try
            clickbtn = "Pending"

            grdpallettag.Rows.Clear()

            gridsql = "Select * from vlistpallets v"
            gridsql = gridsql & " where v.logstat='1' and v.branch='" & login.branch & "' and (v.qadispo='0' or v.qadispo='3') and v.ticketstat<>'3'"

            rcount = 0
            Dim sqlcnt As String = "Select Count(logticketid) from (" & gridsql & ") x"
            connect()
            cmd = New SqlCommand(sqlcnt, conn)
            rcount = cmd.ExecuteScalar
            cmd.Dispose()
            conn.Close()

            pgb1.Value = 0

            bwork = New BackgroundWorker()
            bwork.WorkerSupportsCancellation = True
            bworkstat = New BackgroundWorker()
            bworkstat.WorkerSupportsCancellation = True
            bworkofn = New BackgroundWorker()
            bworkofn.WorkerSupportsCancellation = True

            AddHandler bwork.DoWork, New DoWorkEventHandler(AddressOf bwork_DoWork)
            AddHandler bwork.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bwork_Completed)
            AddHandler bwork.ProgressChanged, New ProgressChangedEventHandler(AddressOf bwork_ProgressChanged)
            m_addRowDelegate = New AddRowDelegate(AddressOf AddDGVRow)

            AddHandler bworkofn.DoWork, New DoWorkEventHandler(AddressOf bworkofn_DoWork)
            AddHandler bworkofn.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bworkofn_Completed)
            AddHandler bworkofn.ProgressChanged, New ProgressChangedEventHandler(AddressOf bworkofn_ProgressChanged)

            If Not bwork.IsBusy Then
                bwork.WorkerReportsProgress = True
                bwork.WorkerSupportsCancellation = True
                bwork.RunWorkerAsync() 'start ng select query
            End If

            Me.Cursor = Cursors.Default

            '/countavail()
            viewof()

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
        Dim cnt As Integer = 0, i As Integer = 0

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
            If drx("ticketstat") = 1 Then
                stat = "In Process"
            ElseIf drx("ticketstat") = 2 Then
                stat = "Completed"
            ElseIf drx("ticketstat") = 3 Then
                stat = "Cancelled"
            ElseIf drx("ticketstat") = 0 Then
                stat = "Empty"
            End If


            Dim dateaddloc As String = "", dateqadispo As String = "", datecancel As String = ""
            If IsDBNull(drx("addtoloc")) = False Then
                dateaddloc = Format(drx("addtoloc"), "yyyy/MM/dd h:mm:ss tt")
            End If
            If IsDBNull(drx("qadate")) = False Then
                dateqadispo = Format(drx("qadate"), "yyyy/MM/dd h:mm:ss tt")
            End If
            If IsDBNull(drx("canceldate")) = False Then
                datecancel = Format(drx("canceldate"), "yyyy/MM/dd h:mm:ss tt")
            End If

            Dim qdisp As String = ""
            If (drx("qadispo")) = 0 Or (drx("qadispo")) = 3 Then
                qdisp = "Pending"
            ElseIf (drx("qadispo")) = 1 Then
                qdisp = "Ok"
            ElseIf (drx("qadispo")) = 2 Then
                qdisp = "Hold"
            End If

            If drx("itemstat") = 2 And stat = "In Process" And qdisp <> "Pending" Then
                If drx("logstat") = 1 Then
                    stat = "Admin confirmation pending"
                Else
                    stat = "Completed"
                End If
            End If

            Dim reserb As String = "", error12 As String = "", error17 As String = ""
            If drx("cusreserve") = 0 Then
                'not reserved''' available
                reserb = "Available"
            ElseIf drx("cusreserve") = 1 Then
                'reserved
                reserb = "Reserved"
            ElseIf drx("cusreserve") = 2 Then
                'selected in orderfill
                reserb = "Selected"
            End If


            Dim avtickets As Double = 0, avstat As String = "Not Available"
            If IsDBNull(drx("avtickets")) = False Then
                avtickets = drx("avtickets")
                If avtickets > 0 Then
                    avstat = "Available"
                End If
            End If

            If loginwhse.ToUpper = "ALL" Then
                '/grdpallettag.Rows.Add(drx("logsheetid"), Format(drx("logsheetdate"), "yyyy/MM/dd"), drx("logsheetnum"), drx("logitemid"), drx("itemname"), drx("logticketid"), drx("palletnum"), "", drx("astart"), drx("fend"), drx("gseries"), 0, reserb, drx("createdby"), drx("datecreated"), dateaddloc, drx("qadate"), qdisp, drx("qaname"), drx("remarks").ToString, drx("whsename"), drx("location"), stat, drx("canceldate").ToString, drx("cancelby").ToString, drx("cancelreason").ToString)
                If reserb = "Reserved" Then
                    error12 = "   " & drx("customer")
                    '/grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & drx("customer")
                ElseIf reserb = "Selected" Then
                    error12 = "   " & drx("ofnum")
                    '/grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & drx("ofnum")
                End If

                If drx("qarems") <> "" Then
                    error17 = "   " & drx("qarems")
                    '/grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & drx("qarems")
                End If

                AddDGVRow(drx("logsheetid"), drx("logsheetdate"), drx("logsheetnum"), drx("logitemid"), drx("itemname"), drx("logticketid"), drx("palletnum"), "", drx("astart").ToString, drx("fend").ToString, drx("gseries"), avtickets, avstat, drx("createdby"), Format(drx("datecreated"), "yyyy/MM/dd h:mm:ss tt"), dateaddloc, dateqadispo, qdisp, drx("qaname").ToString, drx("remarks").ToString, drx("whsename"), drx("location"), stat, datecancel, drx("cancelby").ToString, drx("cancelreason").ToString, error12, error17, i)
            Else
                If loginwhse = drx("whsename") Then
                    '/grdpallettag.Rows.Add(drx("logsheetid"), Format(drx("logsheetdate"), "yyyy/MM/dd"), drx("logsheetnum"), drx("logitemid"), drx("itemname"), drx("logticketid"), drx("palletnum"), "", drx("astart"), drx("fend"), drx("gseries"), 0, reserb, drx("createdby"), drx("datecreated"), dateaddloc, drx("qadate"), qdisp, drx("qaname"), drx("remarks").ToString, drx("whsename"), drx("location"), stat, drx("canceldate").ToString, drx("cancelby").ToString, drx("cancelreason").ToString)
                    If reserb = "Reserved" Then
                        error12 = "   " & drx("customer")
                        '/grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & drx("customer")
                    ElseIf reserb = "Selected" Then
                        error12 = "   " & drx("ofnum")
                        '/grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & drx("ofnum")
                    End If

                    If drx("qarems") <> "" Then
                        error17 = "   " & drx("qarems")
                        '/grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & drx("qarems")
                    End If

                    AddDGVRow(drx("logsheetid"), drx("logsheetdate"), drx("logsheetnum"), drx("logitemid"), drx("itemname"), drx("logticketid"), drx("palletnum"), "", drx("astart").ToString, drx("fend").ToString, drx("gseries"), avtickets, avstat, drx("createdby"), Format(drx("datecreated"), "yyyy/MM/dd h:mm:ss tt"), dateaddloc, dateqadispo, qdisp, drx("qaname").ToString, drx("remarks").ToString, drx("whsename"), drx("location"), stat, datecancel, drx("cancelby").ToString, drx("cancelreason").ToString, error12, error17, i)
                End If
            End If

            i += 1
            bwork.ReportProgress((i / (rcount)) * 100)
        End While
        drx.Dispose()
        cmd.Dispose()
        connection.Close()
    End Sub

    Delegate Sub AddRowDelegate(ByVal value0 As Object, ByVal value1 As Object, ByVal value2 As Object, ByVal value3 As Object, ByVal value4 As Object, ByVal value5 As Object, ByVal value6 As Object, ByVal value7 As Object, ByVal value8 As Object, ByVal value9 As Object, ByVal value10 As Object, ByVal value11 As Object, ByVal value12 As Object, ByVal value13 As Object, ByVal value14 As Object, ByVal value15 As Object, ByVal value16 As Object, ByVal value17 As Object, ByVal value18 As Object, ByVal value19 As Object, ByVal value20 As Object, ByVal value21 As Object, ByVal value22 As Object, ByVal value23 As Object, ByVal value24 As Object, ByVal value25 As Object, ByVal valuerr12 As Object, ByVal valuerr17 As Object, ByVal valuerowin As Object)
    Private m_addRowDelegate As AddRowDelegate

    Private Sub AddDGVRow(ByVal v0 As Integer, ByVal v1 As Date, ByVal v2 As String, ByVal v3 As Integer, ByVal v4 As String, ByVal v5 As Integer, ByVal v6 As String, ByVal v7 As String, ByVal v8 As String, ByVal v9 As String, ByVal v10 As String, ByVal v11 As Integer, ByVal v12 As String, ByVal v13 As String, ByVal v14 As String, ByVal v15 As String, ByVal v16 As String, ByVal v17 As String, ByVal v18 As String, ByVal v19 As String, ByVal v20 As String, ByVal v21 As String, ByVal v22 As String, ByVal v23 As String, ByVal v24 As String, ByVal v25 As String, ByVal vrr12 As String, ByVal vrr17 As String, ByVal rowin As Integer)
        If threadEnabled = True Then
            If grdpallettag.InvokeRequired Then
                grdpallettag.BeginInvoke(New AddRowDelegate(AddressOf AddDGVRow), v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, vrr12, vrr17, rowin)
            Else
                grdpallettag.Rows.Add(v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25)
                grdpallettag.Rows(rowin).Cells(12).ErrorText = vrr12
                grdpallettag.Rows(rowin).Cells(17).ErrorText = vrr17
            End If
        End If
    End Sub

    Private Sub bwork_Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        'If Not bwork.IsBusy Then
        '    bworkstat.WorkerReportsProgress = True
        '    bworkstat.WorkerSupportsCancellation = True
        '    bworkstat.RunWorkerAsync()
        'End If
        If Not bworkstat.IsBusy Then
            bworkofn.WorkerReportsProgress = True
            bworkofn.WorkerSupportsCancellation = True
            bworkofn.RunWorkerAsync()
        End If
    End Sub

    Private Sub bwork_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        lblloading.Location = New Point(285, 61)
        lblloading.Visible = True
        pgb1.Style = ProgressBarStyle.Blocks
        pgb1.Visible = True
        pgb1.Value = e.ProgressPercentage
        Panel2.Enabled = False
        Panel3.Enabled = False
    End Sub

    Private Sub bworkofn_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        threadEnabled = True
        Dim cnt As Integer = 0
        Dim connection As SqlConnection

        'status ng logsheet kung admin pending confirmation nlng or in process
        For Each row As DataGridViewRow In grdpallettag.Rows
            Dim palid As String = grdpallettag.Rows(row.Index).Cells(5).Value
            Dim palstat As String = grdpallettag.Rows(row.Index).Cells(22).Value
            Dim ofidd As Integer = -1

            ofnum_list = New List(Of String)

            If palstat <> "Cancelled" Then
                sql = "Select DISTINCT g.ofid, o.ofnum from tblloggood g inner join tblorderfill o on g.ofid=o.ofid right outer join tbllogsheet s on s.logsheetid=g.logsheetid"
                sql = sql & " where g.logticketid='" & palid & "' and g.ofid IS NOT NULL and s.branch='" & loginbranch & "' and o.branch='" & loginbranch & "' and g.status='0'"
                connection = New SqlConnection
                connection.ConnectionString = strconn
                If connection.State <> ConnectionState.Open Then
                    connection.Open()
                End If
                cmd = New SqlCommand(sql, connection)
                Dim drx As SqlDataReader = cmd.ExecuteReader
                While drx.Read
                    ofnum_list.Add(drx("ofnum"))
                End While
                drx.Dispose()
                cmd.Dispose()
                connection.Close()

                Dim temp As String = ""
                For i = 0 To ofnum_list.Count - 1
                    If temp = "" Then
                        temp = ofnum_list.Item(i)
                    Else
                        temp = temp & ", " & ofnum_list.Item(i)
                    End If
                Next

                AddDGVofn(temp, cnt)
            End If

            cnt += 1

            bworkofn.ReportProgress((cnt / rcount) * 100)
        Next
    End Sub

    Delegate Sub AddofnDelegate(ByVal value0 As Object, ByVal valuerow As Object)
    Private m_addofnDelegate As AddofnDelegate

    Private Sub AddDGVofn(ByVal v0 As String, ByVal vrow As Integer)
        If threadEnabled = True Then
            If grdpallettag.InvokeRequired Then
                grdpallettag.BeginInvoke(New AddofnDelegate(AddressOf AddDGVofn), v0, vrow)
            Else
                grdpallettag.Rows(vrow).Cells(26).Value = v0
            End If
        End If
    End Sub

    Private Sub bworkofn_Completed(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Me.Cursor = Cursors.Default
        lblloading.Visible = False
        pgb1.Visible = False
        pgb1.Style = ProgressBarStyle.Blocks
        Panel2.Enabled = True
        Panel3.Enabled = True

        If e.Error IsNot Nothing Then
            MsgBox(e.Error.ToString, MsgBoxStyle.Critical, "")
        ElseIf e.Cancelled = True Then
            MsgBox("Operation is cancelled.", MsgBoxStyle.Exclamation, "")
        Else
            If grdpallettag.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Critical, "")
            Else
                grdpallettag.SuspendLayout()
                grdpallettag.ResumeLayout()

                If clickbtn <> "Search" Then
                    '/MsgBox("Loading data completed.", MsgBoxStyle.Information, "")
                End If
            End If
        End If
    End Sub

    Private Sub bworkofn_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        pgb1.Value = e.ProgressPercentage
    End Sub

    Private Sub grdpallettag_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpallettag.CellContentClick
        Try
            Me.Cursor = Cursors.WaitCursor

            'link
            If e.ColumnIndex = 2 And e.RowIndex > -1 Then

                Dim cell As DataGridViewCell = grdpallettag.Rows(e.RowIndex).Cells(e.ColumnIndex)
                grdpallettag.CurrentCell = cell
                ' Me.ContextMenuStrip2.Show(Cursor.Position)
                If grdpallettag.RowCount <> 0 Then
                    If grdpallettag.Item(2, grdpallettag.CurrentRow.Index).Value IsNot Nothing Then
                        'populate grdtrans
                        Dim temploc As String = "", tempcol As String = "", temppar As String = "", tempitem As String = ""

                        viewpallets.Text = "View Transactions (" & grdpallettag.Item(2, grdpallettag.CurrentRow.Index).Value.ToString & ")"
                        viewpallets.txtlog.Text = grdpallettag.Item(2, grdpallettag.CurrentRow.Index).Value.ToString
                        '/viewpallets.lblline.Text = grdpallettag.Item(4, grdpallettag.CurrentRow.Index).Value.ToString
                        viewpallets.lognum = grdpallettag.Item(2, grdpallettag.CurrentRow.Index).Value
                        viewpallets.TreeView1.Nodes.Clear()


                        sql = "Select s.logsheetid, s.palletizer, i.logitemid as litemid, i.itemname, t.palletnum, t.location, t.columns, t.logticketid, t.addtoloc, t.logitemid as titemid, "
                        sql = sql & " t.qadispo, t.status from tbllogsheet s inner join tbllogitem i on s.logsheetid=i.logsheetid right outer join tbllogticket t on i.logitemid=t.logitemid"
                        sql = sql & " where s.logsheetnum='" & grdpallettag.Item(2, grdpallettag.CurrentRow.Index).Value & "' and s.branch='" & login.branch & "' and t.status<>'3' order by t.location, t.columns"
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
                        sql = "Select s.logsheetid, s.palletizer, i.logitemid as litemid, i.itemname, t.palletnum, t.location, "
                        sql = sql & " t.columns, t.logticketid, t.addtoloc, t.logitemid as titemid, t.qadispo, t.status from tbllogsheet s"
                        sql = sql & " inner join tbllogitem i on s.logsheetid=i.logsheetid right outer join tbllogticket t on i.logitemid=t.logitemid "
                        sql = sql & " where s.logsheetnum='" & grdpallettag.Item(2, grdpallettag.CurrentRow.Index).Value & "' and s.branch='" & login.branch & "' and t.status='3'"
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
            lblloading.Visible = True
            Panel2.Enabled = False
            Panel3.Enabled = False
            Me.Cursor = Cursors.WaitCursor

            chkhide.Checked = False
            clickbtn = "Search"

            If Trim(txtlog.Text) <> "" Then
                '/searchlogsheet()
                sql = "Select * from vlistpallets v"
                sql = sql & " where v.logsheetnum='" & lbltrip.Text & Trim(txtlog.Text) & "' and v.branch='" & login.branch & "'"

            ElseIf Trim(txtlogid.Text) <> "" Then
                '//searchlogid()
                sql = "Select * from vlistpallets v"
                sql = sql & " where v.logsheetid='" & Trim(txtlogid.Text) & "' and v.branch='" & login.branch & "'"

            ElseIf Trim(txtpalid.Text) <> "" Then
                '//searchpalid()
                sql = "Select * from vlistpallets v"
                sql = sql & " where v.logticketid='" & Trim(txtpalid.Text) & "' and v.branch='" & login.branch & "'"

            ElseIf Trim(txtpallet.Text) <> "" Then
                '//searchpallet()
                sql = "Select * from vlistpallets v"
                sql = sql & " where v.palletnum='" & Trim(txtpallet.Text) & "' and v.branch='" & login.branch & "'"

            ElseIf Trim(txtrec.Text) <> "" Then
                '//searchlogid()
                sql = "Select v.* from vlistpallets v"
                sql = sql & " inner join tblreceive r on v.recnum=r.recnum and v.branch=r.branch and r.status<>'3'"
                sql = sql & " where r.recid='" & Trim(txtrec.Text) & "' and v.branch='" & login.branch & "'"

            ElseIf Trim(txtticket.Text) <> "" Or Trim(txtlet.Text) <> "" Then
                '//searchticket
                If Trim(txtticket.Text) = "" Or Trim(txtlet.Text) = "" Then
                    lblloading.Visible = False
                    pgb1.Visible = False
                    pgb1.Style = ProgressBarStyle.Blocks
                    Panel2.Enabled = True
                    Panel3.Enabled = True
                    Me.Cursor = Cursors.Default
                    MsgBox("Complete the required fields to search ticket number.", MsgBoxStyle.Exclamation, "")
                    txtticket.Focus()
                    Exit Sub
                End If

                chkhide.Checked = False

                If Trim(txtticket.Text).ToString.Contains("D") Then
                    sql = "Select v.* from vlistpallets v"
                    sql = sql & " inner join tbllogdouble d on v.logticketid=d.logticketid"
                    sql = sql & " where (d.letter='" & Trim(txtlet.Text) & "' and d.dticketnum='" & Trim(txtticket.Text) & "' and d.status<>'3') and v.branch='" & login.branch & "'"

                Else
                    sql = "Select v.* from vlistpallets v"
                    sql = sql & " inner join tblloggood g on v.logticketid=g.logticketid"
                    sql = sql & " where (g.letter='" & Trim(txtlet.Text) & "' and g.gticketnum='" & Trim(txtticket.Text) & "' and g.status<>'3') and v.branch='" & login.branch & "'"

                    sql = sql & " UNION "

                    sql = sql & " Select v.* from vlistpallets v"
                    sql = sql & " inner join tbllogcancel c on v.logticketid=c.logticketid"
                    sql = sql & " where (c.letter='" & Trim(txtlet.Text) & "' and c.cticketnum='" & Trim(txtticket.Text) & "' and c.status<>'3') and v.branch='" & login.branch & "'"
                End If

            Else
                If Trim(cmbitem.Text) = "" Then
                    lblloading.Visible = False
                    pgb1.Visible = False
                    pgb1.Style = ProgressBarStyle.Blocks
                    Panel2.Enabled = True
                    Panel3.Enabled = True
                    Me.Cursor = Cursors.Default
                    MsgBox("Input item first.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                sql = "Select v.* from vlistpallets v"
                sql = sql & " where v.logsheetdate>='" & Format(datefrom.Value, "yyyy/MM/dd") & "' and v.logsheetdate<='" & Format(dateto.Value, "yyyy/MM/dd") & "' and v.branch='" & login.branch & "'"

                If Trim(cmbstatus.Text) <> "" Then
                    If Trim(cmbstatus.Text) = "In Process" Then
                        sql = sql & " and v.ticketstat='1'"
                    ElseIf Trim(cmbstatus.Text) = "Completed" Then
                        sql = sql & " and v.ticketstat='2'"
                    ElseIf Trim(cmbstatus.Text) = "Cancelled" Then
                        sql = sql & " and v.ticketstat='3'"
                    End If
                End If
            End If

            If Trim(cmbwhse.Text) <> "" And Trim(cmbwhse.Text) <> "All" Then
                sql = sql & " and v.whsename='" & Trim(cmbwhse.Text) & "'"
            End If

            If Trim(cmbline.Text) <> "" And Trim(cmbline.Text) <> "All" Then
                sql = sql & " and v.palletizer='" & Trim(cmbline.Text) & "'"
            End If

            If Trim(cmbshift.Text) <> "" And Trim(cmbshift.Text) <> "All" Then
                sql = sql & " and v.shift='" & Trim(cmbshift.Text) & "'"
            End If

            If Trim(cmbitem.Text) <> "" And Trim(cmbitem.Text) <> "All" Then
                sql = sql & " and v.itemname='" & Trim(cmbitem.Text) & "'"
            End If

            If Trim(cmbloc.Text) <> "" And Trim(cmbloc.Text) <> "All" Then
                sql = sql & " and v.location='" & Trim(cmbloc.Text) & "'"
            End If

            If Trim(cmbdispo.Text) <> "" And Trim(cmbdispo.Text) <> "All" Then
                If Trim(cmbdispo.Text) = "Ok" Then
                    sql = sql & " and v.qadispo='1'"
                ElseIf Trim(cmbdispo.Text) = "Hold" Then
                    sql = sql & " and v.qadispo='2'"
                ElseIf Trim(cmbdispo.Text) = "Pending" Then
                    sql = sql & " and (v.qadispo='0' or v.qadispo='3')"
                End If
            End If

            If chkhide.Checked = True Then
                sql = sql & " and v.logstatus<>'3'"
            End If

            sql = sql & "  and v.branch='" & login.branch & "'"
            'TextBox1.Text = sql
            Dim ordersql As String = " order by v.logsheetdate,v.whsename"

            gridsql = sql & ordersql

            rcount = 0
            Dim sqlcnt As String = "Select Count(logticketid) from (" & sql & ") x"
            connect()
            cmd = New SqlCommand(sqlcnt, conn)
            rcount = cmd.ExecuteScalar
            cmd.Dispose()
            conn.Close()

            pgb1.Value = 0

            grdpallettag.Rows.Clear()

            bwork = New BackgroundWorker()
            bwork.WorkerSupportsCancellation = True
            bworkstat = New BackgroundWorker()
            bworkstat.WorkerSupportsCancellation = True
            bworkofn = New BackgroundWorker()
            bworkofn.WorkerSupportsCancellation = True

            AddHandler bwork.DoWork, New DoWorkEventHandler(AddressOf bwork_DoWork)
            AddHandler bwork.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bwork_Completed)
            AddHandler bwork.ProgressChanged, New ProgressChangedEventHandler(AddressOf bwork_ProgressChanged)
            m_addRowDelegate = New AddRowDelegate(AddressOf AddDGVRow)


            AddHandler bworkofn.DoWork, New DoWorkEventHandler(AddressOf bworkofn_DoWork)
            AddHandler bworkofn.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf bworkofn_Completed)
            AddHandler bworkofn.ProgressChanged, New ProgressChangedEventHandler(AddressOf bworkofn_ProgressChanged)

            If Not bwork.IsBusy Then
                bwork.WorkerReportsProgress = True
                bwork.WorkerSupportsCancellation = True
                bwork.RunWorkerAsync() 'start ng select query
            End If

            Me.Cursor = Cursors.Default

            '/countavail()
            viewof()

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

    Private Sub txtlog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtlog.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtlog_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlog.TextChanged
        If Trim(txtlog.Text) <> "" Then
            Dim str As String
            str = txtlog.Text
            If str.Length > 2 Then
                Dim answer As String
                answer = str.Substring(0, 2)
                If answer = "L." Then
                    str = str.Substring(2, str.Length - 2)
                    txtlog.Text = str
                    txtlog.Select(txtlog.Text.Length, 0)
                End If
            End If

            txtpallet.Text = ""
            txtpallet.Enabled = False
            txtticket.Text = ""
            txtlet.Text = ""
            txtticket.Enabled = False
            txtlet.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
            txtlogid.Text = ""
            txtlogid.Enabled = False
            txtpalid.Text = ""
            txtpalid.Enabled = False
            txtrec.Text = ""
            txtrec.Enabled = False
        Else
            txtpallet.Enabled = True
            txtticket.Enabled = True
            txtlet.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtlogid.Enabled = True
            txtpalid.Enabled = True
            txtrec.Enabled = True
        End If
    End Sub

    Private Sub PrintTicketToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintTicketToolStripMenuItem.Click
        Try
            If grdpallettag.SelectedCells.Count = 1 Or grdpallettag.SelectedRows.Count = 1 Then
                Dim logticketid As String = grdpallettag.Rows(selectedrow).Cells(5).Value
                'print pallet tag
                rptpallettag.logticket = logticketid
                rptpallettag.ShowDialog()

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

    Private Sub grdlog_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdpallettag.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then
                grdpallettag.ClearSelection()
                grdpallettag.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True

                selectedrow = e.RowIndex
                Me.ContextMenuStrip1.Show(Cursor.Position)

                SelectAllToolStripMenuItem.Visible = False
                ReserveToolStripMenuItem2.Visible = True
                PrintTicketToolStripMenuItem.Visible = True
                RemoveSelectedStatusToolStripMenuItem.Visible = False

                If login.depart = "All" Then
                    RemoveSelectedStatusToolStripMenuItem.Visible = True
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

    Private Sub grdpallettag_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdpallettag.ColumnHeaderMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex = -1 And e.ColumnIndex = 17 Then
            Me.ContextMenuStrip1.Show(Cursor.Position)
            SelectAllToolStripMenuItem.Visible = True
            ReserveToolStripMenuItem2.Visible = False
            PrintTicketToolStripMenuItem.Visible = False
            RemoveSelectedStatusToolStripMenuItem.Visible = False
        End If
    End Sub

    Private Sub grdpallettag_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles grdpallettag.RowPrePaint
        If e.RowIndex > -1 Then
            Dim dgvRow As DataGridViewRow = grdpallettag.Rows(e.RowIndex)
            '<== But this is the name assigned to it in the properties of the control
            'If CDate(Format(dgvRow.Cells(1).Value, "yyyy/MM/dd")) <= CDate(Format(Date.Now.AddDays(-1), "yyyy/MM/dd")) Then
            If dgvRow.Cells(22).Value = "Cancelled" Then 'step1
                dgvRow.DefaultCellStyle.BackColor = Color.DeepSkyBlue
            End If
        End If
    End Sub

    Private Sub grdlog_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpallettag.SelectionChanged
        count()
    End Sub

    Public Sub count()
        Try
            lblcount.Text = "     Selected Rows Count: " & grdpallettag.SelectedRows.Count
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub datefrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles datefrom.ValueChanged
        dateto.MinDate = datefrom.Value
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
                    sql = "Select * from tblpalletizer where palletizer='" & Trim(cmbline.Text.ToUpper) & "' and branch='" & login.branch & "'"
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
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MsgBox(ex.tostring, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub cmbline_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbline.SelectedIndexChanged

    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click

    End Sub

    Private Sub txtpallet_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpallet.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtpallet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpallet.TextChanged
        If Trim(txtpallet.Text) <> "" Then
            txtlog.Text = ""
            txtlog.Enabled = False
            txtticket.Text = ""
            txtticket.Enabled = False
            txtlet.Text = ""
            txtlet.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
            txtlogid.Text = ""
            txtlogid.Enabled = False
            txtpalid.Text = ""
            txtpalid.Enabled = False
            txtrec.Text = ""
            txtrec.Enabled = False
        ElseIf Trim(txtpallet.Text) = "" And Trim(txtlet.Text) = "" Then
            txtlog.Enabled = True
            txtticket.Enabled = True
            txtlet.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtlogid.Enabled = True
            txtpalid.Enabled = True
            txtrec.Enabled = True
        End If
    End Sub

    Private Sub txtticket_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtticket.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtticket_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtticket.TextChanged
        If Trim(txtticket.Text) <> "" Then
            txtlog.Text = ""
            txtlog.Enabled = False
            txtpallet.Text = ""
            txtpallet.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
            txtlogid.Text = ""
            txtlogid.Enabled = False
            txtpalid.Text = ""
            txtpalid.Enabled = False
            txtrec.Text = ""
            txtrec.Enabled = False
        Else
            txtlog.Enabled = True
            txtpallet.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtlogid.Enabled = True
            txtpalid.Enabled = True
            txtrec.Enabled = True
        End If
    End Sub

    Private Sub ReserveToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReserveToolStripMenuItem2.Click
        Try
            If grdpallettag.SelectedCells.Count = 1 Or grdpallettag.SelectedRows.Count = 1 Then
                Dim logticketid As String = grdpallettag.Rows(selectedrow).Cells(5).Value

                If login.depart = "Admin Dispatching" Or login.depart = "All" Then
                    'check if may dispo na si QA
                    Dim dispo As String = grdpallettag.Rows(selectedrow).Cells(17).Value
                    Dim stat As String = grdpallettag.Rows(selectedrow).Cells(12).Value

                    If dispo = "Ok" And (stat = "Available" Or stat = "Reserved") Then
                        palletsumres.logticketid = logticketid
                        palletsumres.lognum = grdpallettag.Rows(selectedrow).Cells(2).Value
                        palletsumres.logitemid = grdpallettag.Rows(selectedrow).Cells(3).Value
                        palletsumres.pallettag = grdpallettag.Rows(selectedrow).Cells(6).Value
                        palletsumres.txtpallet.Text = grdpallettag.Rows(selectedrow).Cells(6).Value
                        palletsumres.ShowDialog()

                        'refreshlist
                        If clickbtn = "Search" Then
                            btnsearch.PerformClick()
                        Else
                            btnview.PerformClick()
                        End If

                    ElseIf stat <> "Available" And stat <> "Reserved" Then
                        MsgBox("Cannot reseve pallet that is not available.", MsgBoxStyle.Exclamation, "")
                    ElseIf dispo <> "Ok" Then
                        MsgBox("Cannot reseve pallet without OK disposition of QA.", MsgBoxStyle.Exclamation, "")
                    End If
                Else
                    MsgBox("Access denied.", MsgBoxStyle.Critical, "")
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

    Private Sub btnqadispo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnqadispo.Click
        'check if all selected cells are in dispo column
        palletsumqa.grdpallettag.Rows.Clear()

        Dim cell17only As Boolean = False
        For Each cell As DataGridViewCell In grdpallettag.SelectedCells
            If cell.ColumnIndex = 17 Then
                If grdpallettag.Rows(cell.RowIndex).Cells(22).Value = "Cancelled" Then
                    MsgBox(grdpallettag.Rows(cell.RowIndex).Cells(6).Value & " is already cancelled.", MsgBoxStyle.Exclamation, "")
                ElseIf grdpallettag.Rows(cell.RowIndex).Cells(12).Value = "Available" Then
                    'load sa palletsumqa yung mga palletnum
                    palletsumqa.grdpallettag.Rows.Add(grdpallettag.Rows(cell.RowIndex).Cells(5).Value, grdpallettag.Rows(cell.RowIndex).Cells(6).Value, grdpallettag.Rows(cell.RowIndex).Cells(17).Value, grdpallettag.Rows(cell.RowIndex).Cells(17).ErrorText)
                ElseIf grdpallettag.Rows(cell.RowIndex).Cells(15).Value = "" Then
                    MsgBox(grdpallettag.Rows(cell.RowIndex).Cells(6).Value & " is not yet finished.", MsgBoxStyle.Exclamation, "")
                Else
                    MsgBox(grdpallettag.Rows(cell.RowIndex).Cells(6).Value & " is " & grdpallettag.Rows(cell.RowIndex).Cells(12).Value & ".", MsgBoxStyle.Exclamation, "")
                End If
            Else
                MsgBox("Select only in QA Dispo column.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If
        Next

        palletsumqa.ShowDialog()

        'refreshlist
        If clickbtn = "Search" Then
            btnsearch.PerformClick()
        Else
            btnview.PerformClick()
        End If
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        grdpallettag.ClearSelection()

        For Each row As DataGridViewRow In grdpallettag.Rows
            grdpallettag.Rows(row.Index).Cells(17).Selected = True
        Next
    End Sub

    Private Sub txtlet_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtlet.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        ElseIf (Asc(e.KeyChar) >= 65 And Asc(e.KeyChar) <= 90) Or (Asc(e.KeyChar) >= 97 And Asc(e.KeyChar) <= 122) Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 127 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtlet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlet.TextChanged
        If Trim(txtlet.Text) <> "" Then
            txtlog.Text = ""
            txtlog.Enabled = False
            txtpallet.Text = ""
            txtpallet.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
            txtlogid.Text = ""
            txtlogid.Enabled = False
            txtpalid.Text = ""
            txtpalid.Enabled = False
            txtrec.Text = ""
            txtrec.Enabled = False
        ElseIf Trim(txtpallet.Text) = "" And Trim(txtlet.Text) = "" Then
            txtlog.Enabled = True
            txtpallet.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtlogid.Enabled = True
            txtpalid.Enabled = True
            txtrec.Enabled = True
        End If
    End Sub

    Private Sub dateto_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dateto.ValueChanged
        datefrom.MaxDate = dateto.Value
    End Sub

    Private Sub cmbitem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitem.Click

    End Sub

    Private Sub cmbitem_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbitem.MouseClick

        Exit Sub
        Dim senderComboBox = DirectCast(sender, ComboBox)
        Dim width As Integer = senderComboBox.DropDownWidth
        Dim g As Graphics = senderComboBox.CreateGraphics()
        Dim font As Font = senderComboBox.Font

        Dim vertScrollBarWidth As Integer = If((senderComboBox.Items.Count > senderComboBox.MaxDropDownItems), SystemInformation.VerticalScrollBarWidth, 0)

        Dim newWidth As Integer
        For Each s As String In DirectCast(sender, ComboBox).Items
            newWidth = CInt(g.MeasureString(s, font).Width) + vertScrollBarWidth
            If width < newWidth Then
                width = newWidth
            End If
        Next

        senderComboBox.DropDownWidth = width
    End Sub

    Private Sub cmbitem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbitem.SelectedIndexChanged

    End Sub

    Public Sub viewof()
        Try

            For Each row As DataGridViewRow In grdpallettag.Rows
                Dim logticketid As Integer = grdpallettag.Rows(row.Index).Cells(5).Value
                Dim palnum As String = grdpallettag.Rows(row.Index).Cells(6).Value
                Dim temp As String = ""

                sql = "Select tbloflog.ofnum from tbloflog right outer join tblorderfill on tbloflog.ofnum=tblorderfill.ofnum where tbloflog.status<>'3' and tbloflog.palletnum='" & palnum & "' and tblorderfill.branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If temp = "" Then
                        temp = dr("ofnum")
                    Else
                        temp = temp & ", " & dr("ofnum")
                    End If
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                grdpallettag.Rows(row.Index).Cells(26).Value = temp
            Next

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

    Private Sub txtlogid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtlogid.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtlogid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlogid.TextChanged
        If Trim(txtlogid.Text) <> "" Then
            txtpallet.Text = ""
            txtpallet.Enabled = False
            txtticket.Text = ""
            txtlet.Text = ""
            txtticket.Enabled = False
            txtlet.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
            txtlog.Text = ""
            txtlog.Enabled = False
            txtpalid.Text = ""
            txtpalid.Enabled = False
            txtrec.Text = ""
            txtrec.Enabled = False
        Else
            txtpallet.Enabled = True
            txtticket.Enabled = True
            txtlet.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtlog.Enabled = True
            txtpalid.Enabled = True
            txtrec.Enabled = True
        End If
    End Sub

    Private Sub txtpalid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpalid.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtpalid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpalid.TextChanged
        If Trim(txtpalid.Text) <> "" Then
            txtlog.Text = ""
            txtlog.Enabled = False
            txtticket.Text = ""
            txtticket.Enabled = False
            txtlet.Text = ""
            txtlet.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
            txtlogid.Text = ""
            txtlogid.Enabled = False
            txtpallet.Text = ""
            txtpallet.Enabled = False
            txtrec.Text = ""
            txtrec.Enabled = False
        ElseIf Trim(txtpallet.Text) = "" And Trim(txtlet.Text) = "" Then
            txtlog.Enabled = True
            txtticket.Enabled = True
            txtlet.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtlogid.Enabled = True
            txtpallet.Enabled = True
            txtrec.Enabled = True
        End If
    End Sub

    Private Sub RemoveSelectedStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveSelectedStatusToolStripMenuItem.Click
        Try
            'check if yung cusreserve=2 and 
            If grdpallettag.SelectedCells.Count = 1 Or grdpallettag.SelectedRows.Count = 1 Then
                Dim logticketid As String = grdpallettag.Rows(selectedrow).Cells(5).Value
                Dim selectedofnum As String = ""

                sql = "Select ofnum from tbllogticket where logticketid='" & logticketid & "' and cusreserve='2'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    selectedofnum = dr("ofnum").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If selectedofnum <> "" Then
                    Dim goremove As Boolean = False
                    Dim stat As Integer

                    'check if ofnum is cancelled na
                    sql = "Select * from tblorderfill where ofnum='" & selectedofnum & "' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        goremove = True
                        stat = dr("status")
                        If stat = 1 Then
                            MsgBox("Order fill is in process.", MsgBoxStyle.Information, "")
                        ElseIf stat = 2 Then
                            MsgBox("Order fill is completed.", MsgBoxStyle.Information, "")
                        Else
                            MsgBox("Order fill is cancelled.", MsgBoxStyle.Information, "")
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    If goremove = True Then
                        Dim a As String = MsgBox("Are you sure you want to remove selected status?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                        If a = vbYes Then
                            sql = "Update tbllogticket set cusreserve=0, ofid=NULL, ofnum=NULL where logticketid='" & logticketid & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            MsgBox("Successfully removed.", MsgBoxStyle.Information, "")
                        End If
                    End If
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
                    sql = "Select * from tblshift where shiftcode='" & Trim(cmbshift.Text.ToUpper) & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If dr("status") = 1 Then
                            cmbshift.SelectedItem = dr("shiftcode")
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

    Private Sub CancelTicketToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelTicketToolStripMenuItem.Click
        Try
            If grdpallettag.SelectedCells.Count = 1 Or grdpallettag.SelectedRows.Count = 1 Then
                'check if cancel
                sql = "Select palletnum from tbllogticket where logticketid='" & grdpallettag.Rows(selectedrow).Cells(5).Value & "' and status='3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Pallet tag# " & grdpallettag.Rows(selectedrow).Cells(6).Value & " is already cancelled.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If Trim(grdpallettag.Rows(selectedrow).Cells(15).Value.ToString) = "" Then
                    MsgBox("Pallet tag# " & grdpallettag.Rows(selectedrow).Cells(6).Value & " is not yet finished.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                palletsumcancel.Panelcancel.Enabled = True
                palletsumcancel.txtitem.Text = grdpallettag.Rows(selectedrow).Cells(4).Value
                palletsumcancel.lbllogticketid.Text = grdpallettag.Rows(selectedrow).Cells(5).Value
                palletsumcancel.txtlog.Text = grdpallettag.Rows(selectedrow).Cells(2).Value
                palletsumcancel.txtpallet.Text = grdpallettag.Rows(selectedrow).Cells(6).Value
                '/txtletter.Text = grdlog.Rows(rowindex).Cells(4).Value
                palletsumcancel.txtletter.Focus()
                palletsumcancel.ShowDialog()
            Else
                MsgBox("Select only one.", MsgBoxStyle.Exclamation, "")
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

    Private Sub txtrec_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrec.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtrec_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrec.TextChanged
        If Trim(txtrec.Text) <> "" Then
            txtpallet.Text = ""
            txtpallet.Enabled = False
            txtticket.Text = ""
            txtlet.Text = ""
            txtticket.Enabled = False
            txtlet.Enabled = False
            datefrom.Enabled = False
            dateto.Enabled = False
            txtlog.Text = ""
            txtlog.Enabled = False
            txtpalid.Text = ""
            txtpalid.Enabled = False
            txtlogid.Text = ""
            txtlogid.Enabled = False
        Else
            txtpallet.Enabled = True
            txtticket.Enabled = True
            txtlet.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtlog.Enabled = True
            txtpalid.Enabled = True
            txtlogid.Enabled = True
        End If
    End Sub
End Class