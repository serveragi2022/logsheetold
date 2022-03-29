Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.ComponentModel

Public Class Copypalletsumwoutbgnd
    Dim lines = System.IO.File.ReadAllLines("connectionstring.txt")
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim clickbtn As String
    Public palsumcnf As Boolean = False
    Dim selectedrow As Integer

    Private threadEnabled As Boolean
    Private bwselectitem As BackgroundWorker, bwviewtickets As BackgroundWorker

    Public Sub connect()
        conn = New SqlConnection
        conn.ConnectionString = strconn
        If conn.State <> ConnectionState.Open Then
            conn.Open()
        End If
    End Sub

    Public Sub disconnect()
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

        If login.logwhse.ToUpper = "ALL" Then
            cmbwhse.Enabled = True
            cmbwhse.Text = ""
        Else
            cmbwhse.Text = login.logwhse
            cmbwhse.Enabled = False
        End If

        viewpending()
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

            sql = "Select tbllogsheet.logsheetid, tbllogsheet.logsheetdate, tbllogsheet.logsheetnum, tbllogitem.logitemid, tbllogitem.itemname,"
            sql = sql & " tbllogticket.logticketid, tbllogticket.palletnum, tbllogticket.letter1, tbllogticket.fstart, tbllogticket.fend, tbllogticket.gseries,"
            sql = sql & " tbllogticket.qadispo, tbllogticket.qadate, tbllogticket.qaname, tbllogticket.status, tbllogticket.datecreated, tbllogticket.addtoloc, tbllogticket.remarks,"
            sql = sql & " tbllogsheet.whsename, tbllogticket.createdby, tbllogticket.location, tbllogticket.cusreserve, tbllogticket.qarems, tbllogticket.ofnum, tbllogticket.customer,"
            sql = sql & " tbllogticket.canceldate, tbllogticket.cancelby, tbllogticket.cancelreason"
            sql = sql & " from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum"
            sql = sql & " right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
            sql = sql & " where tbllogsheet.status='1' and tbllogsheet.branch='" & login.branch & "' and (tbllogticket.qadispo='0' or tbllogticket.qadispo='3') and tbllogticket.status<>'3'"
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

                Dim dateaddloc As String = ""
                If IsDBNull(dr("addtoloc")) = False Then
                    dateaddloc = dr("addtoloc")
                End If

                Dim qdisp As String = ""
                If (dr("qadispo")) = 0 Or (dr("qadispo")) = 3 Then
                    qdisp = "Pending"
                ElseIf (dr("qadispo")) = 1 Then
                    qdisp = "Ok"
                ElseIf (dr("qadispo")) = 2 Then
                    qdisp = "Hold"
                End If

                Dim reserb As String = ""
                If dr("cusreserve") = 0 Then
                    'not reserved''' available
                    reserb = "Available"
                ElseIf dr("cusreserve") = 1 Then
                    'reserved
                    reserb = "Reserved"
                ElseIf dr("cusreserve") = 2 Then
                    'selected in orderfill
                    reserb = "Selected"
                End If

                If login.logwhse.ToUpper = "ALL" Then
                    grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                    If reserb = "Reserved" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                    ElseIf reserb = "Selected" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                    End If

                    If dr("qarems") <> "" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                    End If
                Else
                    If login.logwhse = dr("whsename") Then
                        grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                        If reserb = "Reserved" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                        ElseIf reserb = "Selected" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                        End If

                        If dr("qarems") <> "" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                        End If
                    End If
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            'status ng logsheet kung admin pending confirmation nlng or in process
            For Each row As DataGridViewRow In grdpallettag.Rows
                If grdpallettag.Rows(row.Index).Cells(22).Value = "In Process" Then
                    'check
                    sql = "Select tbllogsheet.status from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum where tbllogsheet.logsheetnum='" & grdpallettag.Rows(row.Index).Cells(2).Value & "' and tbllogsheet.branch='" & login.branch & "' and tbllogitem.status='2'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr("status") = 1 Then
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Admin confirmation pending"
                        Else
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Completed"
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Next

            countavail()
            viewof()

            If grdpallettag.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Critical, "")
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


                        sql = "Select tbllogsheet.logsheetid, tbllogsheet.palletizer, tbllogitem.logitemid as litemid, tbllogitem.itemname, tbllogticket.palletnum, tbllogticket.location, tbllogticket.columns, tbllogticket.logticketid, tbllogticket.addtoloc, tbllogticket.logitemid as titemid, tbllogticket.qadispo, tbllogticket.status from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid where tbllogsheet.logsheetnum='" & grdpallettag.Item(2, grdpallettag.CurrentRow.Index).Value & "' and tbllogsheet.branch='" & login.branch & "' and tbllogticket.status<>'3' order by location, columns" '  "  
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
                        sql = "Select tbllogsheet.logsheetid, tbllogsheet.palletizer, tbllogitem.logitemid as litemid, tbllogitem.itemname, tbllogticket.palletnum, tbllogticket.location, tbllogticket.columns, tbllogticket.logticketid, tbllogticket.addtoloc, tbllogticket.logitemid as titemid, tbllogticket.qadispo, tbllogticket.status from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid where tbllogsheet.logsheetnum='" & grdpallettag.Item(2, grdpallettag.CurrentRow.Index).Value & "' and tbllogsheet.branch='" & login.branch & "' and tbllogticket.status='3'" '  "  
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

            chkhide.Checked = False
            clickbtn = "Search"

            If Trim(txtlog.Text) <> "" Then
                searchlogsheet()
                Me.Cursor = Cursors.Default
                Exit Sub
            ElseIf Trim(txtlogid.Text) <> "" Then
                searchlogid()
                Me.Cursor = Cursors.Default
                Exit Sub
            ElseIf Trim(txtpalid.Text) <> "" Then
                searchpalid()
                Me.Cursor = Cursors.Default
                Exit Sub
            ElseIf Trim(txtpallet.Text) <> "" Then
                searchpallet()
                Me.Cursor = Cursors.Default
                Exit Sub
            ElseIf Trim(txtticket.Text) <> "" Or Trim(txtlet.Text) <> "" Then
                searchticket()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            grdpallettag.Rows.Clear()

            sql = "Select tbllogsheet.logsheetid, tbllogsheet.logsheetdate, tbllogsheet.logsheetnum, tbllogitem.logitemid, tbllogitem.itemname,"
            sql = sql & " tbllogticket.logticketid, tbllogticket.palletnum, tbllogticket.letter1, tbllogticket.fstart, tbllogticket.fend, tbllogticket.gseries,"
            sql = sql & " tbllogticket.qadispo, tbllogticket.qadate, tbllogticket.qaname, tbllogticket.status, tbllogticket.datecreated, tbllogticket.addtoloc, tbllogticket.remarks,"
            sql = sql & " tbllogsheet.whsename, tbllogsheet.shift, tbllogticket.createdby, tbllogticket.location, tbllogticket.cusreserve, tbllogticket.qarems, tbllogticket.ofnum, tbllogticket.customer,"
            sql = sql & " tbllogticket.canceldate, tbllogticket.cancelby, tbllogticket.cancelreason"
            sql = sql & " from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum"
            sql = sql & " right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
            sql = sql & " where logsheetdate>='" & Format(datefrom.Value, "yyyy/MM/dd") & "' and logsheetdate<='" & Format(dateto.Value, "yyyy/MM/dd") & "' and tbllogsheet.branch='" & login.branch & "'"


            If Trim(cmbwhse.Text) <> "" And Trim(cmbwhse.Text) <> "All" Then
                sql = sql & " and tbllogsheet.whsename='" & Trim(cmbwhse.Text) & "'"
            End If

            If Trim(cmbline.Text) <> "" And Trim(cmbline.Text) <> "All" Then
                sql = sql & " and tbllogsheet.palletizer='" & Trim(cmbline.Text) & "'"
            End If

            If Trim(cmbshift.Text) <> "" And Trim(cmbshift.Text) <> "All" Then
                sql = sql & " and tbllogsheet.shift='" & Trim(cmbshift.Text) & "'"
            End If

            If Trim(cmbitem.Text) <> "" And Trim(cmbitem.Text) <> "All" Then
                sql = sql & " and tbllogitem.itemname='" & Trim(cmbitem.Text) & "'"
            End If

            If Trim(cmbloc.Text) <> "" And Trim(cmbloc.Text) <> "All" Then
                sql = sql & " and tbllogticket.location='" & Trim(cmbloc.Text) & "'"
            End If

            If Trim(cmbdispo.Text) <> "" And Trim(cmbdispo.Text) <> "All" Then
                If Trim(cmbdispo.Text) = "Ok" Then
                    sql = sql & " and tbllogticket.qadispo='1'"
                ElseIf Trim(cmbdispo.Text) = "Hold" Then
                    sql = sql & " and tbllogticket.qadispo='2'"
                ElseIf Trim(cmbdispo.Text) = "Pending" Then
                    sql = sql & " and (tbllogticket.qadispo='0' or tbllogticket.qadispo='3')"
                End If
            End If

            If Trim(cmbstatus.Text) <> "" Then
                If Trim(cmbstatus.Text) = "In Process" Then
                    sql = sql & " and tbllogticket.status='1'"
                ElseIf Trim(cmbstatus.Text) = "Completed" Then
                    sql = sql & " and tbllogticket.status='2'"
                ElseIf Trim(cmbstatus.Text) = "Cancelled" Then
                    sql = sql & " and tbllogticket.status='3'"
                End If
            End If

            If chkhide.Checked = True Then
                sql = sql & " and tbllogsheet.status<>'3'"
            End If

            sql = sql & " order by tbllogsheet.logsheetdate,tbllogsheet.whsename"

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

                Dim dateaddloc As String = ""
                If IsDBNull(dr("addtoloc")) = False Then
                    dateaddloc = dr("addtoloc")
                End If

                Dim qdisp As String = ""
                If (dr("qadispo")) = 0 Or (dr("qadispo")) = 3 Then
                    qdisp = "Pending"
                ElseIf (dr("qadispo")) = 1 Then
                    qdisp = "Ok"
                ElseIf (dr("qadispo")) = 2 Then
                    qdisp = "Hold"
                End If

                Dim reserb As String = ""
                If dr("cusreserve") = 0 Then
                    'not reserved''' available
                    reserb = "Available"
                ElseIf dr("cusreserve") = 1 Then
                    'reserved
                    reserb = "Reserved"
                ElseIf dr("cusreserve") = 2 Then
                    'selected in orderfill
                    reserb = "Selected"
                End If

                If login.logwhse.ToUpper = "ALL" Then
                    grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                    If reserb = "Reserved" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                    ElseIf reserb = "Selected" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                    End If

                    If dr("qarems") <> "" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                    End If
                Else
                    If login.logwhse = dr("whsename") Then
                        grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                        If reserb = "Reserved" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                        ElseIf reserb = "Selected" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                        End If

                        If dr("qarems") <> "" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                        End If
                    End If
                End If
                If dr("status") = 3 Then
                    grdpallettag.Rows(grdpallettag.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            'status ng logsheet kung admin pending confirmation nlng or in process
            For Each row As DataGridViewRow In grdpallettag.Rows
                If grdpallettag.Rows(row.Index).Cells(22).Value = "In Process" Then
                    'check
                    sql = "Select tbllogsheet.status from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum where tbllogsheet.logsheetnum='" & grdpallettag.Rows(row.Index).Cells(2).Value & "' and tbllogsheet.branch='" & login.branch & "' and tbllogitem.status='2'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr("status") = 1 Then
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Admin confirmation pending"
                        Else
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Completed"
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Next

            countavail()
            viewof()

            If grdpallettag.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Critical, "")
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

    Public Sub searchlogsheet()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            clickbtn = "Search"

            grdpallettag.Rows.Clear()

            sql = "Select tbllogsheet.logsheetid, tbllogsheet.logsheetdate, tbllogsheet.logsheetnum, tbllogitem.logitemid, tbllogitem.itemname,"
            sql = sql & " tbllogticket.logticketid, tbllogticket.palletnum, tbllogticket.letter1, tbllogticket.fstart, tbllogticket.fend, tbllogticket.gseries,"
            sql = sql & " tbllogticket.qadispo, tbllogticket.qadate, tbllogticket.qaname, tbllogticket.status, tbllogticket.datecreated, tbllogticket.addtoloc, tbllogticket.remarks,"
            sql = sql & " tbllogsheet.whsename, tbllogsheet.shift, tbllogticket.createdby, tbllogticket.location, tbllogticket.cusreserve, tbllogticket.qarems, tbllogticket.ofnum, tbllogticket.customer,"
            sql = sql & " tbllogticket.canceldate, tbllogticket.cancelby, tbllogticket.cancelreason"
            sql = sql & " from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum"
            sql = sql & " right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
            sql = sql & " where tbllogsheet.logsheetnum='" & lbltrip.Text & Trim(txtlog.Text) & "' and tbllogsheet.branch='" & login.branch & "'"


            If Trim(cmbwhse.Text) <> "" And Trim(cmbwhse.Text) <> "All" Then
                sql = sql & " and tbllogsheet.whsename='" & Trim(cmbwhse.Text) & "'"
            End If

            If Trim(cmbline.Text) <> "" And Trim(cmbline.Text) <> "All" Then
                sql = sql & " and tbllogsheet.palletizer='" & Trim(cmbline.Text) & "'"
            End If

            If Trim(cmbshift.Text) <> "" And Trim(cmbshift.Text) <> "All" Then
                sql = sql & " and tbllogsheet.shift='" & Trim(cmbshift.Text) & "'"
            End If

            If Trim(cmbitem.Text) <> "" And Trim(cmbitem.Text) <> "All" Then
                sql = sql & " and tbllogitem.itemname='" & Trim(cmbitem.Text) & "'"
            End If

            If Trim(cmbloc.Text) <> "" And Trim(cmbloc.Text) <> "All" Then
                sql = sql & " and tbllogticket.location='" & Trim(cmbloc.Text) & "'"
            End If

            If Trim(cmbdispo.Text) <> "" And Trim(cmbdispo.Text) <> "All" Then
                If Trim(cmbdispo.Text) = "Ok" Then
                    sql = sql & " and tbllogticket.qadispo='1'"
                ElseIf Trim(cmbdispo.Text) = "Hold" Then
                    sql = sql & " and tbllogticket.qadispo='2'"
                ElseIf Trim(cmbdispo.Text) = "Pending" Then
                    sql = sql & " and (tbllogticket.qadispo='0' or tbllogticket.qadispo='3')"
                End If
            End If

            If chkhide.Checked = True Then
                sql = sql & " and tbllogsheet.status<>'3'"
            End If

            sql = sql & " order by tbllogsheet.logsheetdate,tbllogsheet.whsename"

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

                Dim dateaddloc As String = ""
                If IsDBNull(dr("addtoloc")) = False Then
                    dateaddloc = dr("addtoloc")
                End If

                Dim qdisp As String = ""
                If (dr("qadispo")) = 0 Or (dr("qadispo")) = 3 Then
                    qdisp = "Pending"
                ElseIf (dr("qadispo")) = 1 Then
                    qdisp = "Ok"
                ElseIf (dr("qadispo")) = 2 Then
                    qdisp = "Hold"
                End If

                Dim reserb As String = ""
                If dr("cusreserve") = 0 Then
                    'not reserved''' available
                    reserb = "Available"
                ElseIf dr("cusreserve") = 1 Then
                    'reserved
                    reserb = "Reserved"
                ElseIf dr("cusreserve") = 2 Then
                    'selected in orderfill
                    reserb = "Selected"
                End If

                If login.logwhse.ToUpper = "ALL" Then
                    grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                    If reserb = "Reserved" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                    ElseIf reserb = "Selected" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                    End If

                    If dr("qarems") <> "" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                    End If
                Else
                    If login.logwhse = dr("whsename") Then
                        grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                        If reserb = "Reserved" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                        ElseIf reserb = "Selected" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                        End If

                        If dr("qarems") <> "" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                        End If
                    End If
                End If

                If dr("status") = 3 Then
                    grdpallettag.Rows(grdpallettag.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            'status ng logsheet kung admin pending confirmation nlng or in process
            For Each row As DataGridViewRow In grdpallettag.Rows
                If grdpallettag.Rows(row.Index).Cells(22).Value = "In Process" Then
                    'check
                    sql = "Select tbllogsheet.status from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum where tbllogsheet.logsheetnum='" & grdpallettag.Rows(row.Index).Cells(2).Value & "' and tbllogsheet.branch='" & login.branch & "' and tbllogitem.status='2'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr("status") = 1 Then
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Admin confirmation pending"
                        Else
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Completed"
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Next

            countavail()
            viewof()

            If grdpallettag.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Critical, "")
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

    Public Sub searchlogid()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            clickbtn = "Search"

            grdpallettag.Rows.Clear()

            sql = "Select tbllogsheet.logsheetid, tbllogsheet.logsheetdate, tbllogsheet.logsheetnum, tbllogitem.logitemid, tbllogitem.itemname,"
            sql = sql & " tbllogticket.logticketid, tbllogticket.palletnum, tbllogticket.letter1, tbllogticket.fstart, tbllogticket.fend, tbllogticket.gseries,"
            sql = sql & " tbllogticket.qadispo, tbllogticket.qadate, tbllogticket.qaname, tbllogticket.status, tbllogticket.datecreated, tbllogticket.addtoloc, tbllogticket.remarks,"
            sql = sql & " tbllogsheet.whsename, tbllogsheet.shift, tbllogticket.createdby, tbllogticket.location, tbllogticket.cusreserve, tbllogticket.qarems, tbllogticket.ofnum, tbllogticket.customer,"
            sql = sql & " tbllogticket.canceldate, tbllogticket.cancelby, tbllogticket.cancelreason"
            sql = sql & " from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum"
            sql = sql & " right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
            sql = sql & " where tbllogsheet.logsheetid='" & Trim(txtlogid.Text) & "' and tbllogsheet.branch='" & login.branch & "'"


            If Trim(cmbwhse.Text) <> "" And Trim(cmbwhse.Text) <> "All" Then
                sql = sql & " and tbllogsheet.whsename='" & Trim(cmbwhse.Text) & "'"
            End If

            If Trim(cmbline.Text) <> "" And Trim(cmbline.Text) <> "All" Then
                sql = sql & " and tbllogsheet.palletizer='" & Trim(cmbline.Text) & "'"
            End If

            If Trim(cmbshift.Text) <> "" And Trim(cmbshift.Text) <> "All" Then
                sql = sql & " and tbllogsheet.shift='" & Trim(cmbshift.Text) & "'"
            End If

            If Trim(cmbitem.Text) <> "" And Trim(cmbitem.Text) <> "All" Then
                sql = sql & " and tbllogitem.itemname='" & Trim(cmbitem.Text) & "'"
            End If

            If Trim(cmbloc.Text) <> "" And Trim(cmbloc.Text) <> "All" Then
                sql = sql & " and tbllogticket.location='" & Trim(cmbloc.Text) & "'"
            End If

            If Trim(cmbdispo.Text) <> "" And Trim(cmbdispo.Text) <> "All" Then
                If Trim(cmbdispo.Text) = "Ok" Then
                    sql = sql & " and tbllogticket.qadispo='1'"
                ElseIf Trim(cmbdispo.Text) = "Hold" Then
                    sql = sql & " and tbllogticket.qadispo='2'"
                ElseIf Trim(cmbdispo.Text) = "Pending" Then
                    sql = sql & " and (tbllogticket.qadispo='0' or tbllogticket.qadispo='3')"
                End If
            End If

            If chkhide.Checked = True Then
                sql = sql & " and tbllogsheet.status<>'3'"
            End If

            sql = sql & " order by tbllogsheet.logsheetdate,tbllogsheet.whsename"

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

                Dim dateaddloc As String = ""
                If IsDBNull(dr("addtoloc")) = False Then
                    dateaddloc = dr("addtoloc")
                End If

                Dim qdisp As String = ""
                If (dr("qadispo")) = 0 Or (dr("qadispo")) = 3 Then
                    qdisp = "Pending"
                ElseIf (dr("qadispo")) = 1 Then
                    qdisp = "Ok"
                ElseIf (dr("qadispo")) = 2 Then
                    qdisp = "Hold"
                End If

                Dim reserb As String = ""
                If dr("cusreserve") = 0 Then
                    'not reserved''' available
                    reserb = "Available"
                ElseIf dr("cusreserve") = 1 Then
                    'reserved
                    reserb = "Reserved"
                ElseIf dr("cusreserve") = 2 Then
                    'selected in orderfill
                    reserb = "Selected"
                End If

                If login.logwhse.ToUpper = "ALL" Then
                    grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                    If reserb = "Reserved" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                    ElseIf reserb = "Selected" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                    End If

                    If dr("qarems") <> "" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                    End If
                Else
                    If login.logwhse = dr("whsename") Then
                        grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                        If reserb = "Reserved" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                        ElseIf reserb = "Selected" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                        End If

                        If dr("qarems") <> "" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                        End If
                    End If
                End If

                If dr("status") = 3 Then
                    grdpallettag.Rows(grdpallettag.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            'status ng logsheet kung admin pending confirmation nlng or in process
            For Each row As DataGridViewRow In grdpallettag.Rows
                If grdpallettag.Rows(row.Index).Cells(22).Value = "In Process" Then
                    'check
                    sql = "Select tbllogsheet.status from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum where tbllogsheet.logsheetnum='" & grdpallettag.Rows(row.Index).Cells(2).Value & "' and tbllogsheet.branch='" & login.branch & "' and tbllogitem.status='2'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr("status") = 1 Then
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Admin confirmation pending"
                        Else
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Completed"
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Next

            countavail()
            viewof()

            If grdpallettag.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Critical, "")
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

    Private Sub txtlog_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtlog.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            searchlogsheet()
        End If
    End Sub

    Private Sub txtlog_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlog.TextChanged
        If Trim(txtlog.Text) <> "" Then
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
        Else
            txtpallet.Enabled = True
            txtticket.Enabled = True
            txtlet.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtlogid.Enabled = True
            txtpalid.Enabled = True
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

    Public Sub searchpallet()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            clickbtn = "Search"

            grdpallettag.Rows.Clear()

            sql = "Select tbllogsheet.logsheetid, tbllogsheet.logsheetdate, tbllogsheet.logsheetnum, tbllogitem.logitemid, tbllogitem.itemname,"
            sql = sql & " tbllogticket.logticketid, tbllogticket.palletnum, tbllogticket.letter1, tbllogticket.fstart, tbllogticket.fend, tbllogticket.gseries,"
            sql = sql & " tbllogticket.qadispo, tbllogticket.qadate, tbllogticket.qaname, tbllogticket.status, tbllogticket.datecreated, tbllogticket.addtoloc, tbllogticket.remarks,"
            sql = sql & " tbllogsheet.whsename, tbllogsheet.shift, tbllogticket.createdby, tbllogticket.location, tbllogticket.cusreserve, tbllogticket.qarems, tbllogticket.ofnum, tbllogticket.customer,"
            sql = sql & " tbllogticket.canceldate, tbllogticket.cancelby, tbllogticket.cancelreason"
            sql = sql & " from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum"
            sql = sql & " right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
            sql = sql & " where tbllogticket.palletnum='" & Trim(txtpallet.Text) & "' and tbllogsheet.branch='" & login.branch & "'"


            If Trim(cmbwhse.Text) <> "" And Trim(cmbwhse.Text) <> "All" Then
                sql = sql & " and tbllogsheet.whsename='" & Trim(cmbwhse.Text) & "'"
            End If

            If Trim(cmbline.Text) <> "" And Trim(cmbline.Text) <> "All" Then
                sql = sql & " and tbllogsheet.palletizer='" & Trim(cmbline.Text) & "'"
            End If

            If Trim(cmbshift.Text) <> "" And Trim(cmbshift.Text) <> "All" Then
                sql = sql & " and tbllogsheet.shift='" & Trim(cmbshift.Text) & "'"
            End If

            If Trim(cmbitem.Text) <> "" And Trim(cmbitem.Text) <> "All" Then
                sql = sql & " and tbllogitem.itemname='" & Trim(cmbitem.Text) & "'"
            End If

            If Trim(cmbloc.Text) <> "" And Trim(cmbloc.Text) <> "All" Then
                sql = sql & " and tbllogticket.location='" & Trim(cmbloc.Text) & "'"
            End If

            If Trim(cmbdispo.Text) <> "" And Trim(cmbdispo.Text) <> "All" Then
                If Trim(cmbdispo.Text) = "Ok" Then
                    sql = sql & " and tbllogticket.qadispo='1'"
                ElseIf Trim(cmbdispo.Text) = "Hold" Then
                    sql = sql & " and tbllogticket.qadispo='2'"
                ElseIf Trim(cmbdispo.Text) = "Pending" Then
                    sql = sql & " and (tbllogticket.qadispo='0' or tbllogticket.qadispo='3')"
                End If
            End If

            If chkhide.Checked = True Then
                sql = sql & " and tbllogsheet.status<>'3'"
            End If

            sql = sql & " order by tbllogsheet.logsheetdate,tbllogsheet.whsename"

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then

                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "In Process"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If

                Dim dateaddloc As String = ""
                If IsDBNull(dr("addtoloc")) = False Then
                    dateaddloc = dr("addtoloc")
                End If

                Dim qdisp As String = ""
                If (dr("qadispo")) = 0 Or (dr("qadispo")) = 3 Then
                    qdisp = "Pending"
                ElseIf (dr("qadispo")) = 1 Then
                    qdisp = "Ok"
                ElseIf (dr("qadispo")) = 2 Then
                    qdisp = "Hold"
                End If

                Dim reserb As String = ""
                If dr("cusreserve") = 0 Then
                    'not reserved''' available
                    reserb = "Available"
                ElseIf dr("cusreserve") = 1 Then
                    'reserved
                    reserb = "Reserved"
                ElseIf dr("cusreserve") = 2 Then
                    'selected in orderfill
                    reserb = "Selected"
                End If

                If login.logwhse.ToUpper = "ALL" Then
                    grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                    If reserb = "Reserved" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                    ElseIf reserb = "Selected" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                    End If

                    If dr("qarems") <> "" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                    End If
                Else
                    If login.logwhse = dr("whsename") Then
                        grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                        If reserb = "Reserved" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                        ElseIf reserb = "Selected" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                        End If

                        If dr("qarems") <> "" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                        End If
                    End If
                End If

                If dr("status") = 3 Then
                    grdpallettag.Rows(grdpallettag.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            'status ng logsheet kung admin pending confirmation nlng or in process
            For Each row As DataGridViewRow In grdpallettag.Rows
                If grdpallettag.Rows(row.Index).Cells(22).Value = "In Process" Then
                    'check
                    sql = "Select tbllogsheet.status from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum where tbllogsheet.logsheetnum='" & grdpallettag.Rows(row.Index).Cells(2).Value & "' and tbllogsheet.branch='" & login.branch & "' and tbllogitem.status='2'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr("status") = 1 Then
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Admin confirmation pending"
                        Else
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Completed"
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Next

            countavail()
            viewof()

            If grdpallettag.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Critical, "")
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

    Public Sub searchpalid()
        Try
            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            clickbtn = "Search"

            grdpallettag.Rows.Clear()

            sql = "Select tbllogsheet.logsheetid, tbllogsheet.logsheetdate, tbllogsheet.logsheetnum, tbllogitem.logitemid, tbllogitem.itemname,"
            sql = sql & " tbllogticket.logticketid, tbllogticket.palletnum, tbllogticket.letter1, tbllogticket.fstart, tbllogticket.fend, tbllogticket.gseries,"
            sql = sql & " tbllogticket.qadispo, tbllogticket.qadate, tbllogticket.qaname, tbllogticket.status, tbllogticket.datecreated, tbllogticket.addtoloc, tbllogticket.remarks,"
            sql = sql & " tbllogsheet.whsename, tbllogsheet.shift, tbllogticket.createdby, tbllogticket.location, tbllogticket.cusreserve, tbllogticket.qarems, tbllogticket.ofnum, tbllogticket.customer,"
            sql = sql & " tbllogticket.canceldate, tbllogticket.cancelby, tbllogticket.cancelreason"
            sql = sql & " from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum"
            sql = sql & " right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
            sql = sql & " where tbllogticket.logticketid='" & Trim(txtpalid.Text) & "' and tbllogsheet.branch='" & login.branch & "'"


            If Trim(cmbwhse.Text) <> "" And Trim(cmbwhse.Text) <> "All" Then
                sql = sql & " and tbllogsheet.whsename='" & Trim(cmbwhse.Text) & "'"
            End If

            If Trim(cmbline.Text) <> "" And Trim(cmbline.Text) <> "All" Then
                sql = sql & " and tbllogsheet.palletizer='" & Trim(cmbline.Text) & "'"
            End If

            If Trim(cmbshift.Text) <> "" And Trim(cmbshift.Text) <> "All" Then
                sql = sql & " and tbllogsheet.shift='" & Trim(cmbshift.Text) & "'"
            End If

            If Trim(cmbitem.Text) <> "" And Trim(cmbitem.Text) <> "All" Then
                sql = sql & " and tbllogitem.itemname='" & Trim(cmbitem.Text) & "'"
            End If

            If Trim(cmbloc.Text) <> "" And Trim(cmbloc.Text) <> "All" Then
                sql = sql & " and tbllogticket.location='" & Trim(cmbloc.Text) & "'"
            End If

            If Trim(cmbdispo.Text) <> "" And Trim(cmbdispo.Text) <> "All" Then
                If Trim(cmbdispo.Text) = "Ok" Then
                    sql = sql & " and tbllogticket.qadispo='1'"
                ElseIf Trim(cmbdispo.Text) = "Hold" Then
                    sql = sql & " and tbllogticket.qadispo='2'"
                ElseIf Trim(cmbdispo.Text) = "Pending" Then
                    sql = sql & " and (tbllogticket.qadispo='0' or tbllogticket.qadispo='3')"
                End If
            End If

            If chkhide.Checked = True Then
                sql = sql & " and tbllogsheet.status<>'3'"
            End If

            sql = sql & " order by tbllogsheet.logsheetdate,tbllogsheet.whsename"

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then

                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "In Process"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If

                Dim dateaddloc As String = ""
                If IsDBNull(dr("addtoloc")) = False Then
                    dateaddloc = dr("addtoloc")
                End If

                Dim qdisp As String = ""
                If (dr("qadispo")) = 0 Or (dr("qadispo")) = 3 Then
                    qdisp = "Pending"
                ElseIf (dr("qadispo")) = 1 Then
                    qdisp = "Ok"
                ElseIf (dr("qadispo")) = 2 Then
                    qdisp = "Hold"
                End If

                Dim reserb As String = ""
                If dr("cusreserve") = 0 Then
                    'not reserved''' available
                    reserb = "Available"
                ElseIf dr("cusreserve") = 1 Then
                    'reserved
                    reserb = "Reserved"
                ElseIf dr("cusreserve") = 2 Then
                    'selected in orderfill
                    reserb = "Selected"
                End If

                If login.logwhse.ToUpper = "ALL" Then
                    grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                    If reserb = "Reserved" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                    ElseIf reserb = "Selected" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                    End If

                    If dr("qarems") <> "" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                    End If
                Else
                    If login.logwhse = dr("whsename") Then
                        grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                        If reserb = "Reserved" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                        ElseIf reserb = "Selected" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                        End If

                        If dr("qarems") <> "" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                        End If
                    End If
                End If

                If dr("status") = 3 Then
                    grdpallettag.Rows(grdpallettag.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            'status ng logsheet kung admin pending confirmation nlng or in process
            For Each row As DataGridViewRow In grdpallettag.Rows
                If grdpallettag.Rows(row.Index).Cells(22).Value = "In Process" Then
                    'check
                    sql = "Select tbllogsheet.status from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum where tbllogsheet.logsheetnum='" & grdpallettag.Rows(row.Index).Cells(2).Value & "' and tbllogsheet.branch='" & login.branch & "' and tbllogitem.status='2'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr("status") = 1 Then
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Admin confirmation pending"
                        Else
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Completed"
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Next

            countavail()
            viewof()

            If grdpallettag.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Critical, "")
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

    Public Sub searchticket()
        Try
            clickbtn = "Search"

            If Trim(txtticket.Text) = "" Or Trim(txtlet.Text) = "" Then
                MsgBox("Complete the required fields to search ticket number.", MsgBoxStyle.Exclamation, "")
                txtticket.Focus()
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            chkhide.Checked = False

            grdpallettag.Rows.Clear()

            sql = "Select tbllogsheet.logsheetid, tbllogsheet.logsheetdate, tbllogsheet.logsheetnum, tbllogitem.logitemid, tbllogitem.itemname,"
            sql = sql & " tbllogticket.logticketid, tbllogticket.palletnum, tbllogticket.letter1, tbllogticket.fstart, tbllogticket.fend, tbllogticket.gseries,"
            sql = sql & " tbllogticket.qadispo, tbllogticket.qadate, tbllogticket.qaname, tbllogticket.status, tbllogticket.datecreated, tbllogticket.addtoloc, tbllogticket.remarks,"
            sql = sql & " tbllogsheet.whsename, tbllogsheet.shift, tbllogticket.createdby, tbllogticket.location, tbllogticket.cusreserve, tbllogticket.qarems, tbllogticket.ofnum, tbllogticket.customer,"
            sql = sql & " tbllogticket.canceldate, tbllogticket.cancelby, tbllogticket.cancelreason"
            sql = sql & " from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
            sql = sql & " full outer join tblloggood on tbllogticket.logticketid=tblloggood.logticketid full outer join tbllogdouble on tbllogticket.logticketid=tbllogdouble.logticketid full outer join tbllogcancel on tbllogticket.logticketid=tbllogcancel.logticketid"
            sql = sql & " where tbllogsheet.status<>'-1'"

            If Trim(txtticket.Text).ToString.Contains("D") Then
                sql = sql & " and (tbllogdouble.letter='" & Trim(txtlet.Text) & "' and tbllogdouble.dticketnum='" & Trim(txtticket.Text) & "' and tbllogdouble.status<>'3')"
            Else
                sql = sql & " and ((tblloggood.letter='" & Trim(txtlet.Text) & "' and tblloggood.gticketnum='" & Trim(txtticket.Text) & "' and tblloggood.status<>'3') or (tbllogcancel.letter='" & Trim(txtlet.Text) & "' and tbllogcancel.cticketnum='" & Trim(txtticket.Text) & "' and tbllogcancel.status<>'3'))"
            End If

            If Trim(cmbwhse.Text) <> "" And Trim(cmbwhse.Text) <> "All" Then
                sql = sql & " and tbllogsheet.whsename='" & Trim(cmbwhse.Text) & "'"
            End If

            If Trim(cmbline.Text) <> "" And Trim(cmbline.Text) <> "All" Then
                sql = sql & " and tbllogsheet.palletizer='" & Trim(cmbline.Text) & "'"
            End If

            If Trim(cmbshift.Text) <> "" And Trim(cmbshift.Text) <> "All" Then
                sql = sql & " and tbllogsheet.shift='" & Trim(cmbshift.Text) & "'"
            End If

            If Trim(cmbitem.Text) <> "" And Trim(cmbitem.Text) <> "All" Then
                sql = sql & " and tbllogitem.itemname='" & Trim(cmbitem.Text) & "'"
            End If

            If Trim(cmbloc.Text) <> "" And Trim(cmbloc.Text) <> "All" Then
                sql = sql & " and tbllogticket.location='" & Trim(cmbloc.Text) & "'"
            End If

            If Trim(cmbdispo.Text) <> "" And Trim(cmbdispo.Text) <> "All" Then
                If Trim(cmbdispo.Text) = "Ok" Then
                    sql = sql & " and tbllogticket.qadispo='1'"
                ElseIf Trim(cmbdispo.Text) = "Hold" Then
                    sql = sql & " and tbllogticket.qadispo='2'"
                ElseIf Trim(cmbdispo.Text) = "Pending" Then
                    sql = sql & " and (tbllogticket.qadispo='0' or tbllogticket.qadispo='3')"
                End If
            End If

            If chkhide.Checked = True Then
                sql = sql & " and tbllogsheet.status<>'3'"
            End If

            sql = sql & "  and tbllogsheet.branch='" & login.branch & "' order by tbllogsheet.logsheetdate,tbllogsheet.whsename"

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then

                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "In Process"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If

                Dim dateaddloc As String = ""
                If IsDBNull(dr("addtoloc")) = False Then
                    dateaddloc = dr("addtoloc")
                End If

                Dim qdisp As String = ""
                If (dr("qadispo")) = 0 Or (dr("qadispo")) = 3 Then
                    qdisp = "Pending"
                ElseIf (dr("qadispo")) = 1 Then
                    qdisp = "Ok"
                ElseIf (dr("qadispo")) = 2 Then
                    qdisp = "Hold"
                End If

                Dim reserb As String = ""
                If dr("cusreserve") = 0 Then
                    'not reserved''' available
                    reserb = "Available"
                ElseIf dr("cusreserve") = 1 Then
                    'reserved
                    reserb = "Reserved"
                ElseIf dr("cusreserve") = 2 Then
                    'selected in orderfill
                    reserb = "Selected"
                End If

                If login.logwhse.ToUpper = "ALL" Then
                    grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                    If reserb = "Reserved" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                    ElseIf reserb = "Selected" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                    End If

                    If dr("qarems") <> "" Then
                        grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                    End If
                Else
                    If login.logwhse = dr("whsename") Then
                        grdpallettag.Rows.Add(dr("logsheetid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logitemid"), dr("itemname"), dr("logticketid"), dr("palletnum"), "", dr("fstart"), dr("fend"), dr("gseries"), 0, reserb, dr("createdby"), dr("datecreated"), dateaddloc, dr("qadate"), qdisp, dr("qaname"), dr("remarks").ToString, dr("whsename"), dr("location"), stat, dr("canceldate").ToString, dr("cancelby").ToString, dr("cancelreason").ToString)

                        If reserb = "Reserved" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("customer")
                        ElseIf reserb = "Selected" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(12).ErrorText = "   " & dr("ofnum")
                        End If

                        If dr("qarems") <> "" Then
                            grdpallettag.Rows(grdpallettag.Rows.Count - 1).Cells(17).ErrorText = "   " & dr("qarems")
                        End If
                    End If
                End If

                If dr("status") = 3 Then
                    grdpallettag.Rows(grdpallettag.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            'status ng logsheet kung admin pending confirmation nlng or in process
            For Each row As DataGridViewRow In grdpallettag.Rows
                If grdpallettag.Rows(row.Index).Cells(22).Value = "In Process" Then
                    'check
                    sql = "Select tbllogsheet.status from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum where tbllogsheet.logsheetnum='" & grdpallettag.Rows(row.Index).Cells(2).Value & "' and tbllogsheet.branch='" & login.branch & "' and tbllogitem.status='2'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr("status") = 1 Then
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Admin confirmation pending"
                        Else
                            grdpallettag.Rows(row.Index).Cells(22).Value = "Completed"
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Next

            countavail()
            viewof()

            If grdpallettag.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Critical, "")
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

    Private Sub txtpallet_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpallet.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            searchpallet()
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
        ElseIf Trim(txtpallet.Text) = "" And Trim(txtlet.Text) = "" Then
            txtlog.Enabled = True
            txtticket.Enabled = True
            txtlet.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtlogid.Enabled = True
            txtpalid.Enabled = True
        End If
    End Sub

    Private Sub txtticket_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtticket.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            searchticket()
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
        Else
            txtlog.Enabled = True
            txtpallet.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtlogid.Enabled = True
            txtpalid.Enabled = True
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

    Public Sub countavail()
        Try
            Dim totalgood As Integer = 0, totaldouble As Integer = 0

            For Each row As DataGridViewRow In grdpallettag.Rows
                'tblloggood no of available
                sql = "Select Count(loggoodid) from tblloggood where palletnum='" & grdpallettag.Rows(row.Index).Cells(6).Value & "' and status='1' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                totalgood = cmd.ExecuteScalar
                cmd.Dispose()
                conn.Close()

                'tbllogdouble no of available
                sql = "Select Count(logdoubleid) from tbllogdouble where palletnum='" & grdpallettag.Rows(row.Index).Cells(6).Value & "' and status='1' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                totaldouble = cmd.ExecuteScalar
                cmd.Dispose()
                conn.Close()

                grdpallettag.Item(11, row.Index).Value = totalgood + totaldouble

                If grdpallettag.Item(11, row.Index).Value = 0 Then
                    grdpallettag.Item(12, row.Index).Value = "Not Available"
                End If
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
            searchticket()
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
        ElseIf Trim(txtpallet.Text) = "" And Trim(txtlet.Text) = "" Then
            txtlog.Enabled = True
            txtpallet.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtlogid.Enabled = True
            txtpalid.Enabled = True
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
            searchlogid()
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
        Else
            txtpallet.Enabled = True
            txtticket.Enabled = True
            txtlet.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtlog.Enabled = True
            txtpalid.Enabled = True
        End If
    End Sub

    Private Sub txtpalid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpalid.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            searchpalid()
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
        ElseIf Trim(txtpallet.Text) = "" And Trim(txtlet.Text) = "" Then
            txtlog.Enabled = True
            txtticket.Enabled = True
            txtlet.Enabled = True
            datefrom.Enabled = True
            dateto.Enabled = True
            txtlogid.Enabled = True
            txtpallet.Enabled = True
        End If
    End Sub

    Private Sub RemoveSelectedStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveSelectedStatusToolStripMenuItem.Click
        Try
            'check if yung cusreserve=2 and 
            If grdpallettag.SelectedCells.Count = 1 Or grdpallettag.SelectedRows.Count = 1 Then
                Dim logticketid As String = grdpallettag.Rows(selectedrow).Cells(5).Value
                Dim selectedofnum As String = ""

                sql = "Select * from tbllogticket where logticketid='" & logticketid & "' and cusreserve='2'"
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
                sql = "Select * from tbllogticket where palletnum='" & grdpallettag.Rows(selectedrow).Cells(6).Value & "' and status='3' and branch='" & login.branch & "'"
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
End Class