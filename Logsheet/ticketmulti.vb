Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class ticketmulti
    Dim lines = System.IO.File.ReadAllLines("connectionstring.txt")
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public ticketcnf As Boolean, ticketline As String, trems As String
    Dim AscendingListBox As New List(Of Integer)
    Dim contastart As Boolean = False

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

    Private Sub ticket_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            txtsearch.Text = ""
            lblwhse.Text = ""
            lblshift.Text = ""
            lbldate.Text = ""
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtfstart.Text = 0
        txtfend.Text = 0
        panelticket.Enabled = False
        txtrems.Enabled = False
        viewline()
        ticketmodule.viewforklift()
        btnsetstart.Image = My.Resources.ok
        btnsetstart.Tag = 0
        TreeView2.ImageList = ImageList1
    End Sub

    Private Sub ticket_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If login.logshift = "" Or login.logwhse = "" Then
            MsgBox("Choose warehouse.", MsgBoxStyle.Exclamation, "")
            chooseshift.ShowDialog()
            Me.Dispose()
        Else
            txtfstart.Text = 0
            txtfend.Text = 0
            panelticket.Enabled = False
            txtrems.Enabled = False
            viewline()
            ticketmodule.viewforklift()
            Me.WindowState = FormWindowState.Maximized
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As Form = New Form1()
        f.TopMost = False
        f.Owner = Me
        f.Show()
    End Sub

    Private Sub btnset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnset.Click
        If login.depart = "Production" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            If txtsearch.Text = "" Then
                MsgBox("Cannot set items. Search Ticket Log Sheet # first.", MsgBoxStyle.Exclamation, "")
            Else
                tsetitem.lbllognum.Text = lbltemp.Text & txtsearch.Text
                tsetitem.ShowDialog()
                viewline()
            End If
        Else
            MsgBox("Access denied.", MsgBoxStyle.Critical, "")
        End If
    End Sub

    Public Sub viewpalletloc()
        Try
            cmbloc.Items.Clear()

            'check active pallet location in tbllocation
            sql = "Select * from tbllocation where status='1' and whsename='" & lblwhse.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbloc.Items.Add(dr("location"))
                numcol.Maximum = dr("max")
                txtmaxpal.Text = dr("maxpallet")
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

    Public Sub viewline()
        Try
            'check bags per pallet where whsename
            sql = "Select * from tblwhse where whsename='" & lblwhse.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                lblwhse.Tag = dr("bags")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            TreeView1.Nodes.Clear()
            listlinestatus.Items.Clear()
            Dim templine As String = ""

            sql = "Select * from tbllogitem where logsheetnum='" & lbltemp.Text & txtsearch.Text & "' and status<>'3'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim parentnode As TreeNode
                Dim childnode As TreeNode

                If templine <> dr("palletizer").ToString Then
                    'magkaiba ibig sabihin new line
                    templine = dr("palletizer").ToString
                    parentnode = New TreeNode(dr("palletizer").ToString)
                    parentnode.Tag = "Parent"
                    TreeView1.Nodes.Add(parentnode)
                Else
                    'same location ibang item
                End If


                ''''populate child'''''
                childnode = New TreeNode()
                childnode = parentnode.Nodes.Add(dr("itemname").ToString)
                childnode.Tag = dr("logitemid").ToString

                listlinestatus.Items.Add(dr("status"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            TreeView1.ExpandAll()

            If listlinestatus.Items.Count = 0 Then
                btnlogconfirm.Enabled = False
                btnprintlog.Enabled = False
                btnlogcnfitems.Enabled = False
                Exit Sub
            End If

            'check if nkapag cutoff na


            'check if all status = 2 in listlinestatus para maging available yung  
            Dim checkstat As Boolean = True
            For Each item As Object In listlinestatus.Items
                If item <> 2 Then
                    checkstat = False
                    Exit For
                End If
            Next

            If btnlogcnfitems.Enabled = True Then
                btnlogconfirm.Enabled = False
                btnprintlog.Enabled = False
            Else
                btnlogconfirm.Enabled = True
                btnprintlog.Enabled = True
            End If

            'check if logsheetnum is already confirmed
            If btnlogconfirm.Text = "Confirmed" And Trim(txtsearch.Text) <> "" Then
                btnlogconfirm.Enabled = False
                btnprintlog.Enabled = True
                btnlogcnfitems.Enabled = False
                Exit Sub
            ElseIf Trim(txtsearch.Text) = "" Then
                btnlogconfirm.Enabled = False
                btnprintlog.Enabled = False
                btnlogcnfitems.Enabled = False
                Exit Sub
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

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

    End Sub

    Private Sub TreeView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeView1.MouseDoubleClick
        Try

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

    Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                'tn.Tag
                'lblwhse
                'txtsearch

                TreeView1.SelectedNode = e.Node

                If e.Node.Tag <> "Parent" Then
                    Me.ContextMenuStrip1.Show(Cursor.Position)
                    AddPalletToolStripMenuItem.Visible = True
                    ViewLocationToolStripMenuItem.Visible = True
                    ViewPalletToolStripMenuItem.Visible = False
                    PrintPalletTagToolStripMenuItem.Visible = False
                    'check if completed na ung logitemid
                    sql = "Select * from tbllogitem where logitemid='" & e.Node.Tag & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr("status") = 2 Then
                            AddPalletToolStripMenuItem.Enabled = False
                        Else
                            If lblcreatedshift.Text <> lblshift.Text Then
                                AddPalletToolStripMenuItem.Enabled = False
                                panelticket.Enabled = False
                                txtrems.Enabled = False
                            Else
                                AddPalletToolStripMenuItem.Enabled = True
                            End If
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                End If
            End If

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MsgBox(ex.tostring, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            If txtsearch.ReadOnly = True Then
                txtsearch.ReadOnly = False
                txtsearch.Focus()
            Else
                lognumsearch()
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

    Private Sub txtsearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearch.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If

        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            lognumsearch()
        End If
    End Sub

    Public Sub lognumsearch()
        Try
            If txtsearch.ReadOnly = False Then
                sql = "Select * from tbllogsheet where logsheetnum='" & lbltemp.Text & Trim(txtsearch.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 2 Then
                        btnlogconfirm.Text = "Confirmed"
                    Else
                        btnlogconfirm.Text = "Confirm"
                    End If

                    If dr("allitems") = 1 Then
                        btnlogcnfitems.Enabled = False
                    Else
                        btnlogcnfitems.Enabled = True
                    End If

                    'check user level
                    Dim s As String = dr("logsheetnum").ToString
                    lblline.Text = dr("palletizer")
                    txtsearch.Text = s.Substring(2, s.Length - 2)
                    lblwhse.Text = dr("whsename").ToString
                    lbldate.Text = CDate(Format(dr("logsheetdate"), "yyyy/MM/dd"))
                    lblshift.Text = dr("shift").ToString
                    lbllinetext.Text = dr("palletizer")
                    txtsearch.ReadOnly = True
                    lblcreatedshift.Text = dr("shift")
                    lblyear.Text = dr("logsheetyear")
                Else
                    MsgBox("Cannot find ticket log sheet number.", MsgBoxStyle.Critical, "")
                    txtsearch.Focus()
                    TreeView1.Nodes.Clear()
                    lblline.Text = ""
                    defaultform()
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                viewline()
                TreeView2.Nodes.Clear()
                lblbin.Text = ""
                lbllistline.Text = ""

                If lbllinetext.Text.ToString.Contains("1") = True Then
                    SplitContainer1.Panel1.BackColor = Color.MintCream
                    SplitContainer2.Panel2.BackColor = Color.MintCream
                    panelticket.BackColor = Color.MintCream
                ElseIf lbllinetext.Text.ToString.Contains("2") = True Then
                    SplitContainer1.Panel1.BackColor = Color.Ivory
                    SplitContainer2.Panel2.BackColor = Color.Ivory
                    panelticket.BackColor = Color.Ivory
                Else
                    SplitContainer1.Panel1.BackColor = SystemColors.Control
                    SplitContainer2.Panel2.BackColor = SystemColors.Control
                    panelticket.BackColor = SystemColors.Control
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

    Private Sub btnchange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchange.Click
        ticketcnf = False
        confirmsave.GroupBox1.Text = login.wgroup
        confirmsave.ShowDialog()
        If ticketcnf = True Then
            txtbags.ReadOnly = False
            txtbags.Focus()
        End If
    End Sub

    Private Sub txtbags_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbags.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            If Val(txtbags.Text) > Val(lblbags.Text) Then
                MsgBox("Exceeds in maximum number of bags per pallet.", MsgBoxStyle.Exclamation, "")
                txtbags.Text = ""
                txtbags.Focus()
            Else
                txtbags.ReadOnly = True
                If Val(txtastart.Text) <> 0 Then
                    txtbags.Text = Val(txtbags.Text)
                    txtbags.ReadOnly = True

                    'expected end
                    txtaend.Text = Val(txtastart.Text) + Val(txtbags.Text) - 1
                    'final end
                    txtfend.Text = Val(txtastart.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count - list6.Items.Count

                    btngenerate.PerformClick()
                End If
            End If
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtbags_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbags.Leave
        If Val(txtbags.Text) > Val(lblbags.Text) Then
            MsgBox("Exceeds in maximum number of bags per pallet.", MsgBoxStyle.Exclamation, "")
            txtbags.Text = ""
            txtbags.Focus()
        ElseIf Val(txtbags.Text) = 0 Then
            MsgBox("Number of bags per pallet should not be equal to zero.", MsgBoxStyle.Exclamation, "")
            txtbags.Text = ""
            txtbags.Focus()
        Else
            If Val(txtastart.Text) <> 0 Then
                txtbags.Text = Val(txtbags.Text)
                txtbags.ReadOnly = True

                'expected end
                txtaend.Text = Val(txtastart.Text) + Val(txtbags.Text) - 1
                'final end
                txtfend.Text = Val(txtastart.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count - list6.Items.Count

                btngenerate.PerformClick()
            End If
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If txtlogitemid.Text <> "" Then
                If grdcancel.Rows.Count <> 0 And Trim(txtastart.Text) = "" Then
                    MsgBox("Cannot save. Input initial start ticket number.", MsgBoxStyle.Exclamation, "")
                    txtastart.Focus()
                    Exit Sub
                End If

                If btnsetstart.Tag = 0 Then
                    MsgBox("Cannot save. Set initial start ticket number.", MsgBoxStyle.Exclamation, "")
                    txtastart.Focus()
                    Exit Sub
                End If

                ticketcnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If ticketcnf = True Then
                    Dim tickettype As String = txttype.Text

                    'update wag lang yung addtoloc
                    sql = "Update tbllogticket set remarks='" & Trim(txtrems.Text) & "', ticketwhse='" & login.logwhse & "', ticketshift='" & login.logshift & "', location='" & Trim(cmbloc.Text) & "', columns='" & CInt(numcol.Value) & "', bags='" & txtbags.Text & "', palletnum='" & txtpallet.Text & "', letter='" & txtlet1.Text & "', astart='" & txtastart.Text & "', aend='" & txtaend.Text & "', fstart='" & txtfstart.Text & "', fend='" & txtfend.Text & "', gseries='" & lblseries.Text & "', cseries='" & lblcancel.Text & "', whsechecker='" & txtchecker.Text & "', forklift='" & cmbfork.Text & "', datemodified=GetDate(), modifiedby='" & login.user & "' where logitemid='" & txtlogitemid.Text & "' and logticketid='" & txtlogticket.Text & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    'cancel///////////////////////////////////////////////////////////////////////
                    'insert temporary ticket cancel in tbltempcancel
                    Dim tbltempcancel As String = "tbltempcancel" & txtlogticket.Text
                    Dim tblexistcancel As Boolean = False
                    sql = "Select * from sys.tables where name = '" & tbltempcancel & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        tblexistcancel = True
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    If tblexistcancel = False Then
                        'create tbltempcancel
                        sql = "Create Table " & tbltempcancel & " (logcancelid int NOT NULL PRIMARY KEY IDENTITY(1,1), logsheetnum nvarchar(MAX), logitemid int, logticketid int, palletnum nvarchar(MAX), letter nchar(10), cticketnum int, remarks nvarchar(MAX), grossw float, cticketdate datetime, status int)"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    Else
                        'truncate tbltempcancel where txtlogticket
                        sql = "TRUNCATE Table " & tbltempcancel & ""
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    End If

                    'insert cancel ticket in temporary table

                    For Each row As DataGridViewRow In grdcancel.Rows 'list1 are the list of cancel tickets
                        Dim col0 As String = grdcancel.Rows(row.Index).Cells(0).Value
                        Dim col1 As Date = CDate(grdcancel.Rows(row.Index).Cells(1).Value)
                        Dim col2 As String = grdcancel.Rows(row.Index).Cells(2).Value
                        Dim col3 As String = grdcancel.Rows(row.Index).Cells(3).Value
                        sql = "Insert into " & tbltempcancel & " (logsheetnum, logitemid, logticketid, palletnum, letter, cticketnum, cticketdate, remarks, grossw, status) values ('" & lbltemp.Text & txtsearch.Text & "', '" & txtlogitemid.Text & "', '" & txtlogticket.Text & "', '" & txtpallet.Text & "', '" & txtlet1.Text & "', '" & col0 & "', '" & col1 & "', '" & col2 & "', '" & col3 & "', '1')"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    Next
                    'cancel///////////////////////////////////////////////////////////////////////


                    'good///////////////////////////////////////////////////////////////////////
                    'insert temporary ticket good in tbltempgood
                    Dim tbltempgood As String = "tbltempgood" & txtlogticket.Text
                    Dim tblexistgood As Boolean = False
                    sql = "Select * from sys.tables where name = '" & tbltempgood & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        tblexistgood = True
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    If tblexistgood = False Then
                        'create tbltempgood
                        sql = "Create Table " & tbltempgood & " (loggoodid int NOT NULL PRIMARY KEY IDENTITY(1,1), logsheetnum nvarchar(MAX), logitemid int, logticketid int, palletnum nvarchar(MAX), letter nchar(10), gticketnum int, remarks nvarchar(MAX), status int)"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    Else
                        'truncate tbltempgood where txtlogticket
                        sql = "TRUNCATE Table " & tbltempgood & ""
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    End If

                    'insert good ticket in temporary table
                    For Each item As Object In list5.Items 'list5 are the list of good tickets
                        sql = "Insert into " & tbltempgood & " (logsheetnum, logitemid, logticketid, palletnum, letter, gticketnum, remarks, status) values ('" & lbltemp.Text & txtsearch.Text & "', '" & txtlogitemid.Text & "', '" & txtlogticket.Text & "', '" & txtpallet.Text & "', '" & txtlet1.Text & "', '" & item & "', '', '1')"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    Next
                    'good///////////////////////////////////////////////////////////////////////



                    'double///////////////////////////////////////////////////////////////////////
                    'insert temporary ticket double in tbltempdouble
                    Dim tbltempdouble As String = "tbltempdouble" & txtlogticket.Text
                    Dim tblexistdouble As Boolean = False
                    sql = "Select * from sys.tables where name = '" & tbltempdouble & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        tblexistdouble = True
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    If tblexistdouble = False Then
                        'create tbltempdouble
                        sql = "Create Table " & tbltempdouble & " (logdoubleid int NOT NULL PRIMARY KEY IDENTITY(1,1), logsheetnum nvarchar(MAX), logitemid int, logticketid int, palletnum nvarchar(MAX), letter nchar(10), dticketnum nvarchar(MAX), remarks nvarchar(MAX), dticketdate datetime, status int)"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    Else
                        'truncate tbltempdouble where txtlogticket
                        sql = "TRUNCATE Table " & tbltempdouble & ""
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    End If

                    'insert double ticket in temporary table
                    For Each row As DataGridViewRow In grddouble.Rows 'list1 are the list of double tickets
                        Dim col0 As String = grddouble.Rows(row.Index).Cells(0).Value
                        Dim col1 As Date = CDate(grddouble.Rows(row.Index).Cells(1).Value)
                        Dim col2 As String = grddouble.Rows(row.Index).Cells(2).Value
                        sql = "Insert into " & tbltempdouble & " (logsheetnum, logitemid, logticketid, palletnum, letter, dticketnum, dticketdate, remarks, status) values ('" & lbltemp.Text & txtsearch.Text & "', '" & txtlogitemid.Text & "', '" & txtlogticket.Text & "', '" & txtpallet.Text & "', '" & txtlet1.Text & "', '" & col0 & "', '" & col1 & "', '" & col2 & "', '1')"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    Next
                    'double///////////////////////////////////////////////////////////////////////


                    MsgBox("Successfully saved.", MsgBoxStyle.Information, "")
                End If
            Else
                MsgBox("Cannot save. Select item first.", MsgBoxStyle.Exclamation, "")
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

    Public Sub defaultform()
        lblline.Text = ""
        txtlogitemid.Text = ""
        txtlogticket.Text = ""
        txtitem.Text = ""
        txtchecker.Text = ""
        txtbags.Text = ""
        lblbags.Text = ""
        txtpallet.Text = ""
        cmbloc.Text = ""
        cmbfork.Text = ""
        txtastart.Text = ""
        txtaend.Text = ""
        txtfstart.Text = ""
        txtfend.Text = ""
        list1.Items.Clear()
        list2.Items.Clear()
        list3.Items.Clear()
        list4.Items.Clear()
        list6.Items.Clear()
        lblseries.Text = ""
        txtcancel.Text = ""
        txtgross.Text = ""
        lblbin.Text = ""
        lblbinloc.Text = ""
        lbllistline.Text = ""
        numcol.Minimum = 0
        numcol.Value = 0
        txtnextcol.Text = ""
        txtmaxpal.Text = ""
        lbllinetext.Text = ""
        txtrems.Text = ""
        txttype.Text = ""
        txtletter1.Text = ""
        txtletter2.Text = ""
        txtlet1.Text = ""
        txtlet2.Text = ""
        cmbreason.Text = ""
        grdcancel.Rows.Clear()
        grddouble.Rows.Clear()
        TreeView2.Nodes.Clear()
        panelticket.Enabled = False
        txtrems.Enabled = False
        btnprintlog.Enabled = False
        btnlogconfirm.Enabled = False
    End Sub

    Private Sub txtbags_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbags.ReadOnlyChanged
        If txtbags.ReadOnly = True Then
            txtbags.BackColor = Color.White
        Else
            txtbags.BackColor = Color.NavajoWhite
        End If
    End Sub

    Private Sub btngenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngenerate.Click
        'genseries()
        generateseries222()
    End Sub

    Public Sub genseries()
        Try
            list2.Items.Clear()
            lblseries.Text = ""
            txtfstart.Text = Val(txtastart.Text)

            If list1.Items.Count <> 0 Then
                'final start
                For i = Val(txtastart.Text) To Val(txtfend.Text)
                    If list1.Items.Contains(i) = False Then
                        txtfstart.Text = i
                        Exit For
                    End If
                Next
            End If

            Dim ctr As Integer = 0
            list5.Items.Clear()

            For i = Val(txtastart.Text) To Val(txtfend.Text)
                list4.Items.Clear()
                For Each item As Object In list1.Items
                    If item <> i Then
                        list4.Items.Add("False")
                    Else
                        list4.Items.Add("True")
                    End If
                Next

                If Not list4.Items.Contains("True") Then
                    'Valid Tickets
                    list3.Items.Add(i)
                    list5.Items.Add(i)
                Else
                    'Cancel Tickets
                    '/MsgBox("cancel " & i)
                    If list3.Items.Count <> 0 Then
                        If Val(list3.Items(0)) - Val(list3.Items(list3.Items.Count - 1)) = 0 Then
                            list2.Items.Add(list3.Items(list3.Items.Count - 1))
                            lblseries.Text = lblseries.Text & list3.Items(list3.Items.Count - 1) & ", "
                            list3.Items.Clear()
                        Else
                            list2.Items.Add(list3.Items(0) & " - " & list3.Items(list3.Items.Count - 1))
                            lblseries.Text = lblseries.Text & list3.Items(0) & " - " & list3.Items(list3.Items.Count - 1) & ", "
                            list3.Items.Clear()
                        End If
                    End If
                End If
            Next

            If list3.Items.Count <> 0 Then
                If Val(list3.Items(0)) - Val(list3.Items(list3.Items.Count - 1)) = 0 Then
                    list2.Items.Add(list3.Items(list3.Items.Count - 1))
                    lblseries.Text = lblseries.Text & list3.Items(list3.Items.Count - 1) & ", "
                    list3.Items.Clear()
                Else
                    list2.Items.Add(list3.Items(0) & " - " & list3.Items(list3.Items.Count - 1))
                    lblseries.Text = lblseries.Text & list3.Items(0) & " - " & list3.Items(list3.Items.Count - 1)
                    list3.Items.Clear()
                End If
            End If

            'sort list of cancel tickets
            If list1.Items.Count > 1 Then
                AscendingListBox.Clear()
                For i = 0 To list1.Items.Count - 1
                    AscendingListBox.Add(CInt(list1.Items(i)))
                Next
                AscendingListBox.Sort()
                list1.Items.Clear()
                For i = 0 To AscendingListBox.Count - 1
                    list1.Items.Add(AscendingListBox(i))
                Next
            End If

            'generate cancel tickets in lblcancel
            If list1.Items.Count <> 0 Then
                lblcancel.Text = ""
                For Each item As Object In list1.Items
                    If Trim(lblcancel.Text) = "" Then
                        lblcancel.Text = item
                    Else
                        lblcancel.Text = lblcancel.Text & ", " & item
                    End If
                Next
            End If

            'generate double tickets in series
            Dim dbltic As String = ""
            If list6.Items.Count <> 0 Then
                For Each item As Object In list6.Items
                    If dbltic = "" Then
                        dbltic = item & "D"
                    Else
                        dbltic = dbltic & ", " & item & "D"
                    End If
                Next
            End If

            'final end
            '//////txtfend.Text = Val(txtastart.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count - list6.Items.Count

            'final start
            If list1.Items.Count <> 0 Then
                If Val(txtastart.Text) = list1.Items(0) Then
                    txtfstart.Text = Val(txtastart.Text) + 1
                End If
            End If

            If dbltic <> "" Then
                lblseries.Text = dbltic & ", " & lblseries.Text
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub generateseries222()
        Try
            list2.Items.Clear()
            lblseries.Text = ""
            txtfstart.Text = Val(txtastart.Text)

            If list1.Items.Count <> 0 Then
                'final start
                For i = Val(txtastart.Text) To Val(txtfend.Text)
                    If list1.Items.Contains(i) = False Then
                        txtfstart.Text = i
                        Exit For
                    End If
                Next
            End If

            Dim ctr As Integer = 0
            list5.Items.Clear()

            For i = Val(txtastart.Text) To Val(txtfend.Text)
                list4.Items.Clear()
                For Each item As Object In list1.Items
                    If item <> i Then
                        list4.Items.Add("False")
                    Else
                        list4.Items.Add("True")
                    End If
                Next

                If Not list4.Items.Contains("True") Then
                    'Valid Tickets
                    list3.Items.Add(i)
                    list5.Items.Add(i)
                Else
                    'Cancel Tickets
                    '/MsgBox("cancel " & i)
                    If list3.Items.Count <> 0 Then
                        If Val(list3.Items(0)) - Val(list3.Items(list3.Items.Count - 1)) = 0 Then
                            list2.Items.Add(list3.Items(list3.Items.Count - 1))
                            lblseries.Text = lblseries.Text & list3.Items(list3.Items.Count - 1) & ", "
                            list3.Items.Clear()
                        Else
                            list2.Items.Add(list3.Items(0) & " - " & list3.Items(list3.Items.Count - 1))
                            lblseries.Text = lblseries.Text & list3.Items(0) & " - " & list3.Items(list3.Items.Count - 1) & ", "
                            list3.Items.Clear()
                        End If
                    End If
                End If
            Next


            Dim ctrnew As Integer = 0
            list8.Items.Clear()
            For Each item As Object In list7.Items
                If item >= Val(txtfstart.Text) And ctrnew <= (Val(txtbags.Text) - list6.Items.Count) - 1 Then
                    Dim itemiscancel As Boolean = False
                    For Each cnlitem As Object In list1.Items
                        If cnlitem = item Then
                            itemiscancel = True
                            Exit For
                        End If
                    Next

                    If itemiscancel = False Then
                        ctrnew += 1
                        list8.Items.Add(item)
                    End If
                End If
            Next



            If list3.Items.Count <> 0 Then
                If Val(list3.Items(0)) - Val(list3.Items(list3.Items.Count - 1)) = 0 Then
                    list2.Items.Add(list3.Items(list3.Items.Count - 1))
                    lblseries.Text = lblseries.Text & list3.Items(list3.Items.Count - 1) & ", "
                    list3.Items.Clear()
                Else
                    list2.Items.Add(list3.Items(0) & " - " & list3.Items(list3.Items.Count - 1))
                    lblseries.Text = lblseries.Text & list3.Items(0) & " - " & list3.Items(list3.Items.Count - 1)
                    list3.Items.Clear()
                End If
            End If

            'sort list of cancel tickets
            If list1.Items.Count > 1 Then
                AscendingListBox.Clear()
                For i = 0 To list1.Items.Count - 1
                    AscendingListBox.Add(CInt(list1.Items(i)))
                Next
                AscendingListBox.Sort()
                list1.Items.Clear()
                For i = 0 To AscendingListBox.Count - 1
                    list1.Items.Add(AscendingListBox(i))
                Next
            End If

            'generate cancel tickets in lblcancel
            If list1.Items.Count <> 0 Then
                lblcancel.Text = ""
                For Each item As Object In list1.Items
                    If Trim(lblcancel.Text) = "" Then
                        lblcancel.Text = item
                    Else
                        lblcancel.Text = lblcancel.Text & ", " & item
                    End If
                Next
            End If

            'generate double tickets in series
            Dim dbltic As String = ""
            If list6.Items.Count <> 0 Then
                For Each item As Object In list6.Items
                    If dbltic = "" Then
                        dbltic = item & "D"
                    Else
                        dbltic = dbltic & ", " & item & "D"
                    End If
                Next
            End If

            'final end
            '//////txtfend.Text = Val(txtastart.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count - list6.Items.Count

            'final start
            If list1.Items.Count <> 0 Then
                If Val(txtastart.Text) = list1.Items(0) Then
                    txtfstart.Text = Val(txtastart.Text) + 1
                End If
            End If

            If dbltic <> "" Then
                lblseries.Text = dbltic & ", " & lblseries.Text
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        list1.Items.Clear()
        list2.Items.Clear()
        grdcancel.Rows.Clear()
        txtfstart.Text = txtastart.Text
        txtfend.Text = txtaend.Text
        btngenerate.PerformClick()
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If Trim(txtcancel.Text) = "" Then
                Me.Cursor = Cursors.Default
                MsgBox("Input ticket number first.", MsgBoxStyle.Exclamation, "")
                txtcancel.Focus()
                Exit Sub
            ElseIf Trim(cmbreason.Text) = "" Then
                Me.Cursor = Cursors.Default
                MsgBox("Select reason of cancel ticket number.", MsgBoxStyle.Exclamation, "")
                cmbreason.Focus()
                Exit Sub
            ElseIf Trim(cmbreason.Text).ToString.Contains("Weight") = True And Trim(txtgross.Text) = "" Then
                Me.Cursor = Cursors.Default
                MsgBox("Input gross weight of cancel ticket number.", MsgBoxStyle.Exclamation, "")
                txtgross.Focus()
                Exit Sub
            End If


            If Trim(cmbreason.Text).ToString.Contains("Double") = True Then
                'double ticket meaning additional ticket
                'final end
                'LIST1 = cancel or missing ticket
                'LIST2 = double ticket

                Dim containslist As Boolean = False
                For Each item As Object In list6.Items
                    If item = Val(txtcancel.Text) Then
                        containslist = True
                        Exit For
                    End If
                Next
                If containslist = True Then
                    Me.Cursor = Cursors.Default
                    Dim a As String = MsgBox("Ticket number is already added in double ticket." & vbCrLf & "Do you want to add anyway?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                    If a = vbYes Then
                        list6.Items.Add(Val(txtcancel.Text) & "D")
                        grddouble.Rows.Add(Val(txtcancel.Text) & "D", Date.Now, Trim(cmbreason.Text), txtgross.Text)
                        grddouble.Sort(grddouble.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
                        txtcancel.Text = ""
                        txtgross.Text = ""
                    Else
                        txtcancel.Text = ""
                        txtcancel.Focus()
                        Exit Sub
                    End If
                Else
                    '/MsgBox(list1.Items.Contains(Val(txtcancel.Text)))
                    list6.Items.Add(Val(txtcancel.Text))
                    grddouble.Rows.Add(Val(txtcancel.Text) & "D", Date.Now, Trim(cmbreason.Text), txtgross.Text)
                    grddouble.Sort(grddouble.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
                    txtcancel.Text = ""
                    txtgross.Text = ""
                End If

            Else
                'cancel or missing ticket\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                Dim containslist As Boolean = False
                For Each item As Object In list1.Items
                    If item = Val(txtcancel.Text) Then
                        containslist = True
                        Exit For
                    End If
                Next
                If containslist = True Then
                    Me.Cursor = Cursors.Default
                    MsgBox("Invalid input", MsgBoxStyle.Exclamation, "")
                    txtcancel.Text = ""
                    txtcancel.Focus()
                Else
                    '/MsgBox(list1.Items.Contains(Val(txtcancel.Text)))
                    list1.Items.Add(Val(txtcancel.Text))
                    grdcancel.Rows.Add(Val(txtcancel.Text), Date.Now, Trim(cmbreason.Text), txtgross.Text)
                    grdcancel.Sort(grdcancel.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
                    txtcancel.Text = ""
                    txtgross.Text = ""
                End If
            End If

            'final end
            '//////txtfend.Text = Val(txtastart.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count - list6.Items.Count

            'final start
            If list1.Items.Count <> 0 Then
                If Val(txtastart.Text) = list1.Items(0) Then
                    txtfstart.Text = Val(txtastart.Text) + 1
                End If
            End If

            txtgross.Enabled = False
            btngenerate.PerformClick()

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

    Private Sub txtastart_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtastart.KeyDown

    End Sub

    Private Sub txtastart_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtastart.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            If txtastart.ReadOnly = False Then
                btnsetstart.PerformClick()
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtastart_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtastart.Leave
        Try
            If Val(txtastart.Text) <> 0 Then
                validateastart()
                If contastart = False Then
                    Exit Sub
                End If

                If btnsetstart.Tag = 1 Then
                    txtastart.ReadOnly = True
                    'expected end
                    txtaend.Text = Val(txtastart.Text) + Val(txtbags.Text) - 1
                    txtfstart.Text = Val(txtastart.Text)

                    'final end
                    '////txtfend.Text = Val(txtastart.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count - list6.Items.Count

                    btnadd.Enabled = True
                    btngenerate.Enabled = True
                    btnloc.Enabled = True
                    btngenerate.PerformClick()

                Else
                    txtaend.Text = ""
                    txtfstart.Text = ""
                    txtfend.Text = ""
                End If
            Else
                'reset 4 textbox
                txtastart.Text = ""
                txtaend.Text = ""
                txtfstart.Text = ""
                txtfend.Text = ""

                btnadd.Enabled = False
                btngenerate.Enabled = False
                btnloc.Enabled = False
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

    Private Sub txtpallet_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpallet.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        For Each row As DataGridViewRow In grdcancel.SelectedRows
            Dim listindex As Integer = row.Index
            grdcancel.Rows.Remove(row)
            list1.Items.RemoveAt(listindex)
            '/grdcancel.SortedColumn
        Next

        For Each row As DataGridViewRow In grddouble.SelectedRows
            Dim listindex As Integer = row.Index
            grddouble.Rows.Remove(row)
            list6.Items.RemoveAt(listindex)
            '/grddouble.SortedColumn
        Next

        genseries()
        btngenerate.PerformClick()

        Exit Sub

        While list1.Items.Count > 0 And list1.SelectedItems.Count <> 0
            '/grdcancel.Rows.RemoveAt(grdcancel.Rows.Count - 1)
            '/Dim iindex As Integer = grdcancel.Rows.Count - 1
            list1.Items.RemoveAt(list1.SelectedIndex)
        End While
    End Sub

    Private Sub txtcancel_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcancel.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub txtcancel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcancel.KeyPress
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            If Val(txtcancel.Text) >= Val(txtastart.Text) And Val(txtcancel.Text) <= Val(txtfend.Text) Then
                txtcancel.Text = Val(txtcancel.Text)
                cmbreason.Enabled = True
                cmbreason.Focus()
            End If
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 68 Or Asc(e.KeyChar) = 100 Then

        ElseIf Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtcancel_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcancel.Leave
        If Val(txtcancel.Text) <> 0 And Val(txtastart.Text) <> 0 Then
            'check if valid ung cancel ticket number
            If Val(txtcancel.Text) >= Val(txtastart.Text) And Val(txtcancel.Text) <= Val(txtfend.Text) Then
                txtcancel.Text = Val(txtcancel.Text)
                cmbreason.Enabled = True
                cmbreason.Focus()
            Else
                Me.Cursor = Cursors.Default
                MsgBox("Invalid input", MsgBoxStyle.Exclamation, "")
                txtgross.Text = ""
                txtcancel.Text = ""
                txtcancel.Focus()
            End If

        Else
            txtgross.Text = ""
            txtcancel.Text = ""
            cmbreason.Enabled = False
            txtgross.Enabled = False
        End If
    End Sub

    Private Sub AddPalletToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddPalletToolStripMenuItem.Click
        Try
            'check if wgroup is ticket checker
            If login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" And login.depart <> "All" Then
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            Dim tn As TreeNode = Me.TreeView1.SelectedNode
            If tn.Tag <> "Parent" Then
                'msgbox remind to save before swithing to other line
                If tn.Parent.Text <> lblline.Text And lblline.Text <> "" Then
                    Dim a As String = MsgBox("Are you sure you want to close pallet tag # " & txtlogticket.Text & " for " & lblline.Text & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                    If a = vbNo Then
                        Exit Sub
                    End If
                End If

                defaultform()

                lblline.Text = tn.Parent.Text
                txtitem.Text = tn.Text
                txtlogitemid.Text = tn.Tag
                txtbags.Text = lblwhse.Tag
                lblbags.Text = lblwhse.Tag
                txtchecker.Text = login.fullneym

                'check if may pending sa tbllogticket yung wala pang location
                Dim checkpending As Boolean = False
                Dim pandingticketid As Integer
                sql = "Select * from tbllogticket where logitemid='" & tn.Tag & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr("addtoloc")) = True Then
                        checkpending = True
                        pandingticketid = dr("logticketid")
                    End If
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If checkpending = False Then
                    ticketcnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If ticketcnf = False Then
                        MsgBox("User cancelled adding pallet #.", MsgBoxStyle.Information, "")
                        defaultform()
                        Exit Sub
                    End If

                    'insert tbllogticket
                    'check number of bags in whse
                    Dim bagnum As Integer
                    sql = "Select * from tblwhse where whsename='" & login.logwhse & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        bagnum = dr("bags")
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    'check kung pang ilang palletnum na
                    Dim palnum As String = "", palreftag As String = ""
                    sql = "Select Count(logticketid) from tbllogticket where logsheetyear='" & lblyear.Text & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    palnum = cmd.ExecuteScalar + 1
                    cmd.Dispose()
                    conn.Close()

                    Dim temp As String = ""
                    If palnum < 1000000 Then
                        For vv As Integer = 1 To 6 - palnum.Length
                            temp += "0"
                        Next
                    End If

                    palreftag = lblyear.Text.ToString.Substring(lblyear.Text.Length - 2) & "-" & temp & palnum

                    'insert dun sa tbllogticket
                    sql = "Insert into tbllogticket (logsheetyear, logsheetnum, logitemid, ticketdate, palletnum, bags, location, columns, gseries, cseries, qadispo, qarems, cusreserve, datecreated, createdby, datemodified, modifiedby, status) values ('" & Val(lblyear.Text) & "', '" & lbltemp.Text & Trim(txtsearch.Text) & "', '" & txtlogitemid.Text & "', '" & CDate(lbldate.Text) & "', '" & palreftag & "', '" & bagnum & "', '', '1', '', '', '0', '', '0', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    'update set tbllogitem set status into 0 para in process na sya
                    sql = "Update tbllogitem set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where logitemid='" & txtlogitemid.Text & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully added.", MsgBoxStyle.Information, "")

                Else
                    MsgBox("There is a pending Pallet Tag #.", MsgBoxStyle.Information, "")
                End If

                defaultform()

                lblline.Text = tn.Parent.Text
                txtitem.Text = tn.Text
                txtlogitemid.Text = tn.Tag
                txtbags.Text = lblwhse.Tag
                lblbags.Text = lblwhse.Tag
                txtchecker.Text = login.fullneym

                'view pending tbllogticket ////////////////////////////////////////////////////////////
                If checkpending = True Then
                    sql = "Select * from tbllogticket where logitemid='" & tn.Tag & "' and logticketid='" & pandingticketid & "'"
                Else
                    sql = "Select * from tbllogticket where logitemid='" & tn.Tag & "' and addtoloc is NULL"
                End If
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If IsDBNull(dr("addtoloc")) = True Then
                        lblline.Text = tn.Parent.Text
                        txtitem.Text = tn.Text
                        txtlogitemid.Text = tn.Tag
                        txtpallet.Text = dr("palletnum")
                        txtlogticket.Text = dr("logticketid")
                        txtbags.Text = dr("bags")
                        '/lblbags.Text = dr("bags")
                        txtchecker.Text = login.fullneym
                        If IsDBNull(dr("palletnum")) = False Then
                            txtpallet.Text = dr("palletnum")
                        End If
                        cmbloc.Text = dr("location").ToString
                        numcol.Value = Val(dr("columns"))
                        cmbfork.Text = dr("forklift").ToString
                        txtastart.Text = dr("astart").ToString
                        If Val(txtastart.Text) = 0 Then
                            txtastart.Text = ""
                        End If
                        txtaend.Text = dr("aend").ToString
                        If Val(txtaend.Text) = 0 Then
                            txtaend.Text = ""
                        End If
                        txtfstart.Text = dr("fstart").ToString
                        If Val(txtfstart.Text) = 0 Then
                            txtfstart.Text = ""
                        End If
                        txtfend.Text = dr("fend").ToString
                        If Val(txtfend.Text) = 0 Then
                            txtfend.Text = ""
                        End If
                        lblseries.Text = dr("gseries")
                        lblcancel.Text = dr("cseries")
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
                '////////////////////////////////////////////////////////////////////////////

                If checkpending = True Then
                    'view cancel tickets in temporary table in list1 where logticketid = txtlogticket.Text
                    'view cancel tickets in temporary table in grdcancel where logticketid = txtlogticket.Text
                    list1.Items.Clear()
                    grdcancel.Rows.Clear()
                    Dim tbltempnamecancel As String = "tbltempcancel" & txtlogticket.Text
                    Dim tblexistcancel As Boolean = False
                    sql = "Select * from sys.tables where name = '" & tbltempnamecancel & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        tblexistcancel = True
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    If tblexistcancel = True Then
                        sql = "Select * from " & tbltempnamecancel & " where logticketid='" & txtlogticket.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        While dr.Read
                            list1.Items.Add(dr("cticketnum"))
                            grdcancel.Rows.Add(dr("cticketnum"), dr("cticketdate"), dr("remarks"), dr("grossw"))
                        End While
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()
                    End If     'cancel////////////////////////////////////////////////////////////////////////////////

                    'view good tickets in list5 where logticketid = txtlogticket.Text
                    list5.Items.Clear()
                    Dim tbltempnamegood As String = "tbltempgood" & txtlogticket.Text
                    Dim tblexistgood As Boolean = False
                    sql = "Select * from sys.tables where name = '" & tbltempnamegood & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        tblexistgood = True
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    If tblexistgood = True Then
                        sql = "Select * from " & tbltempnamegood & " where logticketid='" & txtlogticket.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        While dr.Read
                            list5.Items.Add(dr("gticketnum"))
                        End While
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()
                    End If    'good////////////////////////////////////////////////////////////////////////////////



                    'view double tickets in temporary table in grddouble where logticketid = txtlogticket.Text
                    list6.Items.Clear()
                    grddouble.Rows.Clear()
                    Dim tbltempnamedouble As String = "tbltempdouble" & txtlogticket.Text
                    Dim tblexistdouble As Boolean = False
                    sql = "Select * from sys.tables where name = '" & tbltempnamedouble & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        tblexistdouble = True
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    If tblexistdouble = True Then
                        sql = "Select * from " & tbltempnamedouble & " where logticketid='" & txtlogticket.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        While dr.Read
                            list6.Items.Add(dr("dticketnum"))
                            grddouble.Rows.Add(dr("dticketnum"), dr("dticketdate"), dr("remarks"))
                        End While
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()
                    End If     'double////////////////////////////////////////////////////////////////////////////////

                End If

                'View ticket range
                list7.Items.Clear()
                grdrange.Rows.Clear()
                Dim rctr As Integer = 0, temprange As String = ""
                sql = "Select * from tbllogrange where logsheetnum='" & lbltemp.Text & txtsearch.Text & "' and status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    rctr += 1

                    lbllet1.Text = dr("letter1")
                    txtletter1.Text = lbllet1.Text

                    Dim temp1 As String = ""
                    If dr("arfrom") < 1000000 Then
                        For vv As Integer = 1 To 6 - dr("arfrom").ToString.Length
                            temp1 += "0"
                        Next
                    End If

                    If rctr = 1 Then
                        lblrfrom.Text = temp1 & dr("arfrom")
                    End If


                    lbllet2.Text = dr("letter2")
                    txtletter2.Text = lbllet2.Text

                    Dim temp2 As String = ""
                    If dr("arto") < 1000000 Then
                        For vv As Integer = 1 To 6 - dr("arto").ToString.Length
                            temp2 += "0"
                        Next
                    End If
                    lblrto.Text = temp2 & dr("arto")

                    If temprange <> "" Then
                        temprange = temprange & "  ;  " & dr("letter1") & " " & temp1 & dr("arfrom") & " - " & dr("letter2") & " " & temp2 & dr("arto")
                    Else
                        temprange = dr("letter1") & " " & temp1 & dr("arfrom") & " - " & dr("letter2") & " " & temp2 & dr("arto")
                    End If

                    For i = dr("arfrom") To dr("arto")
                        list7.Items.Add(i)
                    Next

                    Dim leftnum As Integer = dr("arto") - dr("frto")
                    grdrange.Rows.Add(dr("letter1"), dr("arfrom"), dr("arto"), leftnum)
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                txtrange.Text = temprange

                viewinitialticket()

                txtrems.ReadOnly = False

                If txtfend.Text <> "" Then
                    txtastart.ReadOnly = True
                    btnsetstart.Image = LogSheet.My.Resources.Resources.cancel
                    btnsetstart.Tag = 1
                Else
                    txtastart.ReadOnly = False
                End If

                'view ticket type
                sql = "Select * from tblitems where itemname='" & txtitem.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    txttype.Text = dr("tickettype") & " Flour"
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                'view location in treeview2 /////////////////////////////////////////////////
                viewlocation()
                TreeView2.Nodes(0).ExpandAll()
                TreeView2.SelectedNode = TreeView2.Nodes(0)

                panelticket.Enabled = True
                txtrems.Enabled = True

                viewpalletloc()
            End If

            If Trim(cmbloc.Text) <> "" Then
                numcol.Enabled = True
            Else
                numcol.Enabled = False
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

    Public Sub viewinitialticket()
        Try
            'view initial start ticket
            sql = "Select TOP 1 * from tbllogticket where logsheetnum='" & lbltemp.Text & txtsearch.Text & "' and logticketid<>'" & txtlogticket.Text & "' order by logticketid DESC"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                'get yung final end ticket plus one
                txtastart.Text = dr("fend") + 1
            Else
                'kunin ung nasa from ng range
                txtastart.Text = Val(lblrfrom.Text)
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            lblastart.Text = txtastart.Text

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

    Private Sub btnloc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnloc.Click
        Try
            If btnsetstart.Tag = 0 Then
                MsgBox("Cannot save. Set initial start ticket number.", MsgBoxStyle.Exclamation, "")
                txtastart.Focus()
                Exit Sub
            End If

            'check if kumpleto details
            If Trim(cmbloc.Text) <> "" And Trim(cmbfork.Text) <> "" And Trim(txtastart.Text) <> "" And Trim(lblseries.Text) <> "" Then
                Dim a As String = MsgBox("Are you sure you want to add pallet tag # " & txtpallet.Text & " to " & Trim(cmbloc.Text) & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbYes Then
                    ticketcnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If ticketcnf = True Then
                        Dim tickettype As String = txttype.Text

                        'update add to location datetime
                        sql = "Update tbllogticket set remarks='" & Trim(txtrems.Text) & "', addtoloc=GetDate(), ticketwhse='" & login.logwhse & "', ticketshift='" & login.logshift & "', location='" & Trim(cmbloc.Text) & "', columns='" & CInt(numcol.Value) & "', bags='" & txtbags.Text & "', palletnum='" & txtpallet.Text & "', letter='" & txtlet1.Text & "', astart='" & txtastart.Text & "', aend='" & txtaend.Text & "', fstart='" & txtfstart.Text & "', fend='" & txtfend.Text & "', gseries='" & lblseries.Text & "', cseries='" & lblcancel.Text & "', whsechecker='" & txtchecker.Text & "', forklift='" & cmbfork.Text & "', datemodified=GetDate(), modifiedby='" & login.user & "' where logitemid='" & txtlogitemid.Text & "' and logticketid='" & txtlogticket.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        'insert sa tblcancel yung mga tickets
                        For Each row As DataGridViewRow In grdcancel.Rows 'list1 are the list of cancel tickets
                            Dim col0 As String = grdcancel.Rows(row.Index).Cells(0).Value
                            Dim col1 As Date = CDate(grdcancel.Rows(row.Index).Cells(1).Value)
                            Dim col2 As String = grdcancel.Rows(row.Index).Cells(2).Value
                            Dim col3 As String = grdcancel.Rows(row.Index).Cells(3).Value
                            sql = "Insert into tbllogcancel (logsheetnum, logitemid, logticketid, palletnum, letter, cticketnum, cticketdate, remarks, grossw, status) values ('" & lbltemp.Text & txtsearch.Text & "', '" & txtlogitemid.Text & "', '" & txtlogticket.Text & "', '" & txtpallet.Text & "', '" & txtlet1.Text & "', '" & col0 & "', '" & col1 & "', '" & col2 & "', '" & col3 & "', '1')"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()
                        Next

                        'insert sa tblgood yung mga tickets
                        For Each item As Object In list5.Items 'list5 are the list of valid tickets
                            sql = "Insert into tblloggood (logsheetnum, logitemid, logticketid, palletnum, letter, gticketnum, remarks, greserve, status) values ('" & lbltemp.Text & txtsearch.Text & "', '" & txtlogitemid.Text & "', '" & txtlogticket.Text & "', '" & txtpallet.Text & "', '" & txtlet1.Text & "', '" & item & "', '', '0', '1')"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()
                        Next

                        'insert sa tbldouble yung mga tickets
                        For Each row As DataGridViewRow In grddouble.Rows 'list1 are the list of double tickets
                            Dim col0 As String = grddouble.Rows(row.Index).Cells(0).Value
                            Dim col1 As Date = CDate(grddouble.Rows(row.Index).Cells(1).Value)
                            Dim col2 As String = grddouble.Rows(row.Index).Cells(2).Value
                            sql = "Insert into tbllogdouble (logsheetnum, logitemid, logticketid, palletnum, letter, dticketnum, dticketdate, remarks, dreserve, status) values ('" & lbltemp.Text & txtsearch.Text & "', '" & txtlogitemid.Text & "', '" & txtlogticket.Text & "', '" & txtpallet.Text & "', '" & txtlet1.Text & "', '" & col0 & "', '" & col1 & "', '" & col2 & "', '0', '1')"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()
                        Next

                        'drop tbltempgood//////////////////////////////////////////////////////////
                        Dim tbltempgood As String = "tbltempgood" & txtlogticket.Text
                        Dim tblexistgood As Boolean = False
                        sql = "Select * from sys.tables where name = '" & tbltempgood & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            tblexistgood = True
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()

                        If tblexistgood = True Then
                            sql = "DROP Table " & tbltempgood & ""
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()
                        End If
                        'good//////////////////////////////////////////////////////////////////////

                        'drop tbltempcancel///////////////////////////////////////////////////////
                        Dim tbltempcancel As String = "tbltempcancel" & txtlogticket.Text
                        Dim tblexistcancel As Boolean = False
                        sql = "Select * from sys.tables where name = '" & tbltempcancel & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            tblexistcancel = True
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()

                        If tblexistcancel = True Then
                            sql = "DROP Table " & tbltempcancel & ""
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()
                        End If
                        'cancel/////////////////////////////////////////////////////////////////////////


                        'drop tbltempdouble///////////////////////////////////////////////////////
                        Dim tbltempdouble As String = "tbltempdouble" & txtlogticket.Text
                        Dim tblexistdouble As Boolean = False
                        sql = "Select * from sys.tables where name = '" & tbltempdouble & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            tblexistdouble = True
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()

                        If tblexistdouble = True Then
                            sql = "DROP Table " & tbltempdouble & ""
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()
                        End If
                        'double/////////////////////////////////////////////////////////////////////////


                        'update final range to in tbllogrange //////////////////////////////////////////
                        Dim ftot As Integer = (Val(txtfend.Text) - Val(lblrfrom.Text)) + 1
                        sql = "Update tbllogrange set frto='" & Val(txtfend.Text) & "', ftotal='" & ftot & "' where logsheetnum='" & lbltemp.Text & txtsearch.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                        '////////////////////////////////////////////////////////////////////////////////


                        MsgBox("Sucessfully added.", MsgBoxStyle.Information, "")

                        'print pallet tag
                        rptpallettag.lognum = lbltemp.Text & Trim(txtsearch.Text)
                        rptpallettag.palletid = txtlogticket.Text
                        rptpallettag.palletloc = Trim(cmbloc.Text)
                        rptpallettag.logticket = txtlogticket.Text
                        rptpallettag.logline = txtlogitemid.Text
                        rptpallettag.ShowDialog()

                        'refresh list ng location
                        defaultform()
                        lognumsearch()
                        viewlocation()
                        TreeView2.Nodes(0).ExpandAll()
                        TreeView2.SelectedNode = TreeView2.Nodes(0)
                    End If
                End If
            Else
                MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
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

    Private Sub btnbin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprintbin.Click
        Try
            If lblline.Enabled = True Then
                Me.Cursor = Cursors.Default
                MsgBox("Cannot Print Bin Tag. There is a pending pallet in " & lbllistline.Text & ".", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            If lblbin.Text <> "" Then
                'check if may pending sa tbllogticket yung wala pang location
                Dim checkpending As Boolean = False, pendingpallet As String = ""
                sql = "Select * from tbllogticket where logitemid='" & lblbin.Text & "' and addtoloc is NULL"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    checkpending = True
                    pendingpallet = dr("logticketid").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If checkpending = True Then
                    Me.Cursor = Cursors.Default
                    MsgBox("Cannot Print Bin Tag. Pallet # " & pendingpallet & " is pending in " & lbllistline.Text & ".", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                'print pallet tag
                rptbintag.lognum = lbltemp.Text & Trim(txtsearch.Text)
                rptbintag.palletid = txtlogticket.Text
                rptbintag.palletloc = Trim(cmbloc.Text)
                rptbintag.logticket = txtlogticket.Text
                rptbintag.logline = txtlogitemid.Text
                rptbintag.ShowDialog()

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

    Private Sub txtastart_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtastart.ReadOnlyChanged
        If txtastart.ReadOnly = True Then
            txtastart.BackColor = Color.White
            btnsetstart.Image = LogSheet.My.Resources.Resources.cancel
            btnsetstart.Tag = 1
        Else
            txtastart.BackColor = Color.NavajoWhite
            btnsetstart.Image = LogSheet.My.Resources.Resources.ok
            btnsetstart.Tag = 0
        End If
    End Sub

    Private Sub txtastart_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtastart.TextChanged
        If Val(txtastart.Text) <> 0 Then
            btnadd.Enabled = True
            btnremove.Enabled = True
            btnclear.Enabled = True
            btngenerate.Enabled = True
            btnloc.Enabled = True
            txtcancel.Enabled = True
            txtcancel.Text = ""
        Else
            btnadd.Enabled = False
            btngenerate.Enabled = False
            btnloc.Enabled = False
            txtcancel.Text = ""
            txtcancel.Enabled = False
        End If
    End Sub

    Public Sub viewlocation()
        Try
            Dim tn As TreeNode = Me.TreeView1.SelectedNode
            Dim temploc As String = "", tempcol As String = ""

            If TreeView1.Nodes.Count <> 0 Then
                If tn.Tag <> "Parent" Then 'And tn.Tag IsNot Nothing Then
                    'view location in treeview2 /////////////////////////////////////////////////
                    TreeView2.Nodes.Clear()
                    lblbin.Text = tn.Tag
                    Dim parentnode As TreeNode
                    parentnode = New TreeNode(tn.Text)
                    parentnode.Tag = "Parent"
                    parentnode.ImageIndex = 3
                    parentnode.SelectedImageIndex = 3
                    lbllistline.Text = tn.Parent.Text
                    TreeView2.Nodes.Add(parentnode)
                    ''''populate child'''''
                    'check sa tblticket yung locations
                    sql = "Select * from tbllogticket where logitemid='" & lblbin.Text & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If IsDBNull(dr("addtoloc")) = False Then
                            Dim childnode As TreeNode
                            Dim childchildnode As TreeNode
                            Dim childchildchildnode As TreeNode

                            If temploc <> dr("location").ToString Then
                                'magkaiba ibig sabihin new location
                                temploc = dr("location").ToString
                                lblbinloc.Text = dr("location").ToString
                                childnode = New TreeNode()
                                childnode = parentnode.Nodes.Add(dr("location").ToString)
                                childnode.Tag = "Location"
                                childnode.ImageIndex = 4
                                childnode.SelectedImageIndex = 4
                            Else
                                'same location ibang item
                            End If

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
                            End If
                        End If
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()


                    '////////////////////////////////////////////////////////////////////////////
                    If lblbin.Text <> "" Then
                        'check if status sa tbllogitem is 2
                        sql = "Select * from tbllogitem where logitemid='" & lblbin.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            If dr("status") = 2 Then
                                btncomp.Text = "Completed"
                                btncomp.Enabled = False
                            Else
                                btncomp.Enabled = True
                                btncomp.Text = "Complete"
                            End If
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()
                    End If
                Else
                    lbllistline.Text = tn.Text
                End If
            Else
                lbllistline.Text = tn.Text
            End If

            If TreeView2.Nodes.Count <> 0 Then
                btnprintbin.Enabled = True
            Else
                btnprintbin.Enabled = False
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

    Private Sub ViewLocationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewLocationToolStripMenuItem.Click
        Dim tn As TreeNode = Me.TreeView1.SelectedNode
        If tn.Tag <> "Parent" Then
            'msgbox remind to save before swithing to other line
            If tn.Parent.Text <> lblline.Text And lblline.Text <> "" Then
                Dim a As String = MsgBox("Are you sure you want to close pallet tag # " & txtlogticket.Text & " for " & lblline.Text & " ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbNo Then
                    Exit Sub
                End If

                defaultform()
            End If

            viewlocation()
            TreeView2.Nodes(0).ExpandAll()
            TreeView2.SelectedNode = TreeView2.Nodes(0)
        End If
    End Sub

    Private Sub cmbloc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbloc.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub cmbloc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbloc.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then

        End If
    End Sub

    Private Sub cmbloc_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbloc.Leave
        If Trim(cmbloc.Text) <> "" Then
            'check tbllogitem if same ng item
            sql = "Select * from tbllogticket right outer join tbllogitem on tbllogticket.logitemid=tbllogitem.logitemid where tbllogticket.status='1' and tbllogticket.location='" & Trim(cmbloc.Text) & "' and tbllogitem.itemname<>'" & txtitem.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MsgBox(dr("itemname") & " is also in the location.", MsgBoxStyle.Information, "")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            numcol.Enabled = True

            'check tbllogticket kung available ba yung location
            'if nakuha na ung lahat ng items sya check anu ung latest nya
            Dim tempcol As Integer = 0, ctr As Integer = 1
            sql = "Select TOP 4 * from tbllogticket where status='1' and location='" & Trim(cmbloc.Text) & "' order by logticketid DESC"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If tempcol <> 0 Then
                    If tempcol = Val(dr("columns")) Then
                        ctr += 1
                        ' check if all are same meaning pwede na mag plus one
                    End If
                Else
                    tempcol = Val(dr("columns"))
                End If
                '/MsgBox(ctr)
                '/numcol.Minimum = Val(dr("columns"))
                '/txtnextcol.Text = Val(dr("columns"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If ctr = 4 Then
                If numcol.Maximum < tempcol + 1 Then
                    MsgBox("Location " & cmbloc.Text & " is already full")
                    cmbloc.Text = ""
                    numcol.Value = 0
                Else
                    numcol.Value = tempcol + 1
                End If
            Else
                If tempcol <> 0 Then
                    numcol.Value = tempcol
                Else
                    numcol.Value = 1
                End If
            End If

            'check yung max column ng location
            sql = "Select * from tbllocation where status='1' and location='" & Trim(cmbloc.Text) & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                If Val(txtnextcol.Text) + 1 <= dr("max") Then
                    txtnextcol.Text = Val(txtnextcol.Text) + 1
                End If
                txtmaxpal.Text = dr("maxpallet")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

        Else
            numcol.Enabled = False
        End If
    End Sub

    Private Sub numcol_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numcol.ValueChanged
        If Trim(txtnextcol.Text) <> "" Then
            If numcol.Value > Val(txtnextcol.Text) Then
                '/MsgBox("Cannot skip column #.", MsgBoxStyle.Exclamation, "")
                If Val(txtnextcol.Text) - 1 = 0 Then
                    '/numcol.Value = 0
                Else
                    '/numcol.Value = Val(txtnextcol.Text) - 1
                End If
            End If
        End If
    End Sub

    Private Sub cmbreason_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbreason.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub cmbreason_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbreason.KeyPress
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            cmbreason.Text = StrConv(Trim(cmbreason.Text), VbStrConv.ProperCase)
            If cmbreason.Items.Contains(Trim(cmbreason.Text)) = False And Trim(cmbreason.Text) <> "" Then
                MsgBox("Invalid reason. Select from drop down list.", MsgBoxStyle.Exclamation, "")
                cmbreason.Text = ""
                cmbreason.Focus()
                txtgross.Enabled = False
            Else
                If cmbreason.Text.Contains("Weight") = True Then
                    txtgross.Enabled = True
                    txtgross.Text = ""
                    txtgross.Focus()
                Else
                    txtgross.Text = ""
                    txtgross.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Sub cmbreason_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbreason.Leave
        cmbreason.Text = StrConv(Trim(cmbreason.Text), VbStrConv.ProperCase)
        If cmbreason.Items.Contains(Trim(cmbreason.Text)) = False And Trim(cmbreason.Text) <> "" Then
            MsgBox("Invalid reason. Select from drop down list.", MsgBoxStyle.Exclamation, "")
            cmbreason.Text = ""
            cmbreason.Focus()
            txtgross.Enabled = False
        Else
            If cmbreason.Text.Contains("Weight") = True Then
                txtgross.Enabled = True
                txtgross.Text = ""
                txtgross.Focus()
            Else
                txtgross.Text = ""
                txtgross.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnlogconfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogconfirm.Click
        Try
            'check level of users
            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            'check if may pending pa or may palletnum na wala pang dispo ni qa
            sql = "Select * from tbllogsheet right outer join tbllogticket on tbllogsheet.logsheetnum=tbllogticket.logsheetnum"
            sql = sql & " where tbllogsheet.logsheetnum='" & lbltemp.Text & txtsearch.Text & "' and (tbllogticket.addtoloc is NULL or tbllogticket.qadispo='0' or tbllogticket.qadispo='3')"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MsgBox("Cannot confirm. Some pallets are pending and/or without QA disposition.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            'remarks yung sap number
            trems = ""
            ticketrems.ShowDialog()
            If trems <> "" Then
                ticketcnf = False
                confirm.GroupBox1.Text = login.wgroup
                confirm.ShowDialog()
                If ticketcnf = True Then
                    'update ststus ng log sheet to 2 meaning confirmed and nde na available
                    sql = "Update tbllogsheet set status='2', remarks='" & trems & "', datemodified=GetDate(), modifiedby='" & login.user & "' where logsheetnum='" & lbltemp.Text & txtsearch.Text & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Ticket Log Sheet# " & lbltemp.Text & txtsearch.Text & " confirmed.", MsgBoxStyle.Information, "")
                    txtsearch.Text = ""
                    TreeView1.Nodes.Clear()
                    defaultform()
                End If
            Else
                MsgBox("User cancelled confirmation.", MsgBoxStyle.Information, "")
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

    Private Sub txtgross_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtgross.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = 46 And Trim(txtgross.Text) <> "" And txtgross.Text.Contains(".") = True Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 46 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnadd.PerformClick()
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub cmbreason_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbreason.SelectedIndexChanged

    End Sub

    Private Sub txtcancel_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcancel.TextChanged

    End Sub

    Private Sub txtgross_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtgross.Leave
        If cmbreason.Text = "Under Weight" And (Val(txtgross.Text) >= 25) And txtgross.Text <> "" Then
            MsgBox("Invalid input of gross weight.", MsgBoxStyle.Exclamation, "")
            txtgross.Focus()
            txtgross.Text = ""
        ElseIf cmbreason.Text = "Over Weight" And (Val(txtgross.Text) <= 25) And txtgross.Text <> "" Then
            MsgBox("Invalid input of gross weight.", MsgBoxStyle.Exclamation, "")
            txtgross.Focus()
            txtgross.Text = ""
        End If
    End Sub

    Private Sub txtgross_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtgross.TextChanged

    End Sub

    Private Sub btnprintlog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprintlog.Click
        Try
            sql = "Select * from tbllogticket where logsheetnum='" & lbltemp.Text & txtsearch.Text & "' and (qadispo='0' or qadispo='3')"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MsgBox("Cannot print logsheet. There are pallets that are still pending for QA disposition.", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            'print log sheet
            rptlogsheetrevise.lognum = lbltemp.Text & Trim(txtsearch.Text)
            rptlogsheetrevise.palletid = txtlogticket.Text
            rptlogsheetrevise.palletloc = Trim(cmbloc.Text)
            rptlogsheetrevise.logticket = txtlogticket.Text
            rptlogsheetrevise.logline = txtlogitemid.Text
            rptlogsheetrevise.stat = ""
            rptlogsheetrevise.ShowDialog()



            'refresh list ng location
            defaultform()
            lognumsearch()
            viewline()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbloc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbloc.SelectedIndexChanged

    End Sub

    Private Sub btncomp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncomp.Click
        Try
            If btncomp.Text = "Complete" Then
                If login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" And login.depart <> "All" Then
                    MsgBox("Access denied.", MsgBoxStyle.Critical, "")
                    Exit Sub
                End If

                'check if there is pending pallet
                'check if may pending sa tbllogticket yung wala pang addloc
                Dim checkpending As Boolean = False
                Dim pandingpallettagnum As String = ""
                sql = "Select * from tbllogticket where logsheetnum='" & lbltemp.Text & txtsearch.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr("addtoloc")) = True Then
                        checkpending = True
                        pandingpallettagnum = dr("palletnum")
                        Exit While
                    End If
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If checkpending = True Then
                    MsgBox("Cannot complete. Pallet tag# " & pandingpallettagnum & " is still pending.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If


                'check if may pallet tag nmn bago i complete
                If TreeView2.Nodes(0).Nodes.Count <> 0 Then
                    ticketcnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If ticketcnf = True Then
                        'set tbllogitemid set status into 2 para completed na sya
                        sql = "Update tbllogitem set status='2', modifiedby='" & login.user & "', datemodified=GetDate() where logitemid='" & lblbin.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox(TreeView2.Nodes(0).Text & " completed.", MsgBoxStyle.Information, "")

                        btnprintlog.Enabled = False
                        viewlocation()
                        TreeView2.Nodes(0).ExpandAll()
                        TreeView2.SelectedNode = TreeView2.Nodes(0)
                        viewline()
                    End If
                Else
                    MsgBox("Cannot complete. No pallet tag record.", MsgBoxStyle.Exclamation, "")
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

    Private Sub TreeView2_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView2.AfterSelect

    End Sub

    Private Sub TreeView2_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView2.NodeMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then

                TreeView2.SelectedNode = e.Node
                'pag right click dpat ma right click

                If e.Node.Tag <> "Location" And e.Node.Tag <> "Column" And e.Node.Tag <> "Parent" Then
                    Me.ContextMenuStrip1.Show(Cursor.Position)
                    AddPalletToolStripMenuItem.Visible = False
                    ViewLocationToolStripMenuItem.Visible = False
                    ViewPalletToolStripMenuItem.Visible = True
                    PrintPalletTagToolStripMenuItem.Visible = True
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

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        If lblline.Text <> "" Then
            'dapat automatic mag ssave
            btnsave.PerformClick()
        End If
        txtsearch.ReadOnly = False
        tselectline.ShowDialog()
        txtsearch.ReadOnly = False
        btnsearch.PerformClick()
        btncomp.Enabled = False
    End Sub

    Private Sub ViewPalletToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewPalletToolStripMenuItem.Click
        Try
            Dim tn As TreeNode = Me.TreeView2.SelectedNode
            If tn.Tag <> "Location" And tn.Tag <> "Column" Then
                'msgbox remind to save before swithing to other line
                If lblline.Text <> "" Then
                    '/Dim a As String = MsgBox("Are you sure you want to close pallet tag # " & txtlogticket.Text & " for " & lblline.Text & " ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                    '/If a = vbNo Then
                    '/Exit Sub
                    '/End If
                End If

                viewpalletloc()
                viewforklift()

                viewpallet.lblline.Text = lbllistline.Text
                viewpallet.txtitem.Text = TreeView2.Nodes(0).Text
                viewpallet.txtlogitemid.Text = lblbin.Text
                viewpallet.txtbags.Text = lblwhse.Tag
                viewpallet.lblbags.Text = lblwhse.Tag
                '///////
                viewpallet.lblrfrom.Text = lblrfrom.Text
                viewpallet.lblrto.Text = lblrto.Text
                '///////

                'view pallet
                sql = "Select * from tbllogticket where logticketid='" & tn.Tag & "' and status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    viewpallet.txtchecker.Text = dr("whsechecker")
                    viewpallet.txtpallet.Text = dr("palletnum")
                    viewpallet.txtlogticket.Text = tn.Tag
                    viewpallet.cmbloc.Text = dr("location")
                    viewpallet.numcol.Value = dr("columns")
                    viewpallet.cmbfork.Text = dr("forklift")
                    viewpallet.txtastart.Text = dr("astart")
                    viewpallet.txtaend.Text = dr("aend")
                    viewpallet.txtfstart.Text = dr("fstart")
                    viewpallet.txtfend.Text = dr("fend")
                    viewpallet.lblcancel.Text = dr("cseries")
                    viewpallet.lblseries.Text = dr("gseries")

                    viewpallet.lbllet1.Text = dr("letter")
                    viewpallet.lbllet2.Text = dr("letter")
                    viewpallet.txtletter1.Text = dr("letter")
                    viewpallet.txtletter2.Text = dr("letter")
                    viewpallet.txtlet1.Text = dr("letter")
                    viewpallet.txtlet2.Text = dr("letter")

                    'view datetime start(datecreated) and end(addtoloc)
                    viewpallet.lblstart.Text = dr("datecreated")
                    viewpallet.lblfinish.Text = dr("addtoloc")
                    viewpallet.txtrems.Text = dr("remarks")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                viewpallet.grdcancel.Rows.Clear()
                sql = "Select * from tbllogcancel where logticketid='" & tn.Tag & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    viewpallet.grdcancel.Rows.Add(dr("cticketnum"), Format(dr("cticketdate"), "HH:mm"), dr("remarks"), dr("grossw"))
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                viewpallet.grddouble.Rows.Clear()
                sql = "Select * from tbllogdouble where logticketid='" & tn.Tag & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    viewpallet.grddouble.Rows.Add(dr("dticketnum"), Format(dr("dticketdate"), "HH:mm"), dr("remarks"))
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                'view flour type
                sql = "Select * from tblitems where itemname='" & TreeView2.Nodes(0).Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    viewpallet.txttype.Text = dr("tickettype").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                viewpalletonly()

                viewpallet.ShowDialog()
                TreeView2.Nodes(0).ExpandAll()

                'txtnextcol.text
                'txtmaxpal.text
                'btngenerate
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

    Public Sub viewpalletonly()
        cmbloc.Enabled = False
        numcol.ReadOnly = True
        cmbfork.Enabled = False
        'mga buttons
        btnadd.Enabled = False
        btnremove.Enabled = False
        btnclear.Enabled = False
        btngenerate.Enabled = False
        btnloc.Enabled = False
        btnsave.Enabled = False
    End Sub

    Public Sub cancelviewpalletonly()
        cmbloc.Enabled = True
        numcol.ReadOnly = False
        cmbfork.Enabled = True
        'mga buttons
        btnadd.Enabled = True
        btnremove.Enabled = True
        btnclear.Enabled = True
        btngenerate.Enabled = True
        btnloc.Enabled = True
        btnsave.Enabled = True
    End Sub

    Private Sub cmbfork_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbfork.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub cmbfork_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbfork.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then

        End If
    End Sub

    Private Sub cmbfork_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbfork.Leave
        Try
            If Trim(cmbfork.Text.ToUpper) <> "" Then
                sql = "Select * from tblusers where workgroup='Forklift Operator' and username='" & Trim(cmbfork.Text.ToUpper) & "' and status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    cmbfork.Text = Trim(cmbfork.Text)
                Else
                    MsgBox("Invalid Forklift Operator name.", MsgBoxStyle.Critical, "")
                    cmbfork.Text = ""
                    cmbfork.Focus()
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub txtletter1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtletter1.TextChanged
        txtlet1.Text = txtletter1.Text
    End Sub

    Private Sub txtletter2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtletter2.TextChanged
        txtlet2.Text = txtletter2.Text
    End Sub

    Private Sub btnsetstart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsetstart.Click
        Try
            If btnsetstart.Tag = 0 Then
                If Val(txtastart.Text) <> 0 Then
                    validateastart()
                    If contastart = False Then
                        Exit Sub
                    End If

                    checkleftrange()

                    txtgross.Enabled = False
                    btnadd.Enabled = True
                    btngenerate.Enabled = True
                    btnloc.Enabled = True
                    btngenerate.PerformClick()

                    txtastart.ReadOnly = True
                    btnsetstart.Image = My.Resources.cancel
                    btnsetstart.Tag = 1

                    If txtastart.Text <> lblastart.Text Then
                        MsgBox("magkaiba")
                    End If

                    'check if txtastart to txtfstart is less than the txtbags then if may second range sila
                    If ((Val(txtaend.Text) - Val(txtastart.Text)) + 1) < Val(txtbags.Text) And txtrange.Text.ToString.Contains(";") = True Then
                        sql = "Select Top 1 * from tbllogrange where logsheetnum='" & lbltemp.Text & txtsearch.Text & "' and status='1' order by lograngeid DESC"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            txtletter1b.Text = dr("letter1")
                            txtletter2b.Text = dr("letter1")
                            txtlet1b.Text = dr("letter2")
                            txtlet2b.Text = dr("letter2")
                            txtastartb.Text = dr("arfrom")
                            If dr("arto") >= dr("arfrom") + (Val(txtbags.Text) - ((Val(txtaend.Text) - Val(txtastart.Text)) + 1)) - 1 Then
                                txtaendb.Text = dr("arfrom") + (Val(txtbags.Text) - ((Val(txtaend.Text) - Val(txtastart.Text)) + 1)) - 1
                            Else
                                txtaendb.Text = dr("arto")
                            End If
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()
                    End If
                Else
                    txtaend.Text = ""
                    txtfstart.Text = ""
                    txtfend.Text = ""
                End If
            Else
                lblastart.Text = Trim(txtastart.Text)
                txtastart.ReadOnly = False
                txtastart.Focus()
                txtastart.SelectAll()
                btnsetstart.Image = LogSheet.My.Resources.Resources.ok
                btnsetstart.Tag = 0
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

    Public Sub checkleftrange()
        Try
            'check if left if firstrow is <txtbags
            If grdrange.Rows(0).Cells(3).Value < Val(txtbags.Text) Then
                'expected end
                txtaend.Text = grdrange.Rows(0).Cells(2).Value

                'final start
                If list1.Items.Count <> 0 Then
                    If Val(txtastart.Text) = list1.Items(0) Then
                        txtfstart.Text = Val(txtastart.Text) + 1
                    End If
                End If

                'final end
                Dim tempfinalend As Integer = Val(txtastart.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count - list6.Items.Count
                If tempfinalend > grdrange.Rows(0).Cells(2).Value Then
                    Dim tempfinalendiscancel As Boolean = False
                    For i = grdrange.Rows(0).Cells(2).Value To Val(txtfstart.Text) Step -1
                        For Each item As Object In list1.Items
                            If item = i Then
                                tempfinalendiscancel = True
                            End If
                        Next

                        If tempfinalendiscancel = False Then
                            txtfend.Text = i
                            Exit For
                        End If
                    Next
                End If


                'set range b
                txtletter1b.Text = grdrange.Rows(1).Cells(0).Value
                txtastartb.Text = grdrange.Rows(1).Cells(1).Value


            Else
                'expected end
                txtaend.Text = Val(txtastart.Text) + Val(txtbags.Text) - 1

                'final end
                txtfend.Text = Val(txtastart.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count - list6.Items.Count

                'final start
                If list1.Items.Count <> 0 Then
                    If Val(txtastart.Text) = list1.Items(0) Then
                        txtfstart.Text = Val(txtastart.Text) + 1
                    End If
                End If

                'set set b range to empty
                txtletter1b.Text = ""
                txtastartb.Text = ""
                txtletter2b.Text = ""
                txtaendb.Text = ""

                txtlet1b.Text = ""
                txtfstartb.Text = ""
                txtlet2b.Text = ""
                txtfendb.Text = ""
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

    Public Sub validateastart()
        Try
            contastart = False
            Dim temp As String = ""
            If Val(Trim(txtastart.Text)) < 1000000 Then
                For vv As Integer = 1 To 6 - txtastart.Text.ToString.Length
                    temp += "0"
                Next
            End If

            'check if already existing in tblloggood and tbllogcancel
            sql = "Select * from tblloggood full outer join tbllogcancel on tblloggood.logticketid = tbllogcancel.logticketid where (tblloggood.letter='" & txtlet1.Text & "' or tbllogcancel.letter='" & txtlet1.Text & "') and (tblloggood.gticketnum='" & Trim(txtastart.Text) & "' or tbllogcancel.cticketnum='" & Trim(txtastart.Text) & "')"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Me.Cursor = Cursors.Default

                MsgBox("Ticket # " & txtletter1.Text & " " & temp & Trim(txtastart.Text) & " is already exist.", MsgBoxStyle.Exclamation, "")
                viewinitialticket()
                txtastart.Focus()
                lblseries.Text = ""
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            '/MsgBox("check if within the range yung nasa initial start ticket")
            If (Val(lblrfrom.Text) <= Val(txtastart.Text)) And (Val(lblrto.Text) >= Val(txtastart.Text)) Then
                'check if may na left 
                'proceed
                contastart = True
            Else
                MsgBox("Ticket # " & txtletter1.Text & " " & temp & Trim(txtastart.Text) & " is not within the ticket range.", MsgBoxStyle.Exclamation, "")
                viewinitialticket()
                txtastart.Focus()
                lblseries.Text = ""
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

    Private Sub grddouble_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grddouble.SelectionChanged
        grddouble.Select()
        grdcancel.ClearSelection()
    End Sub

    Private Sub grdcancel_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdcancel.SelectionChanged
        grdcancel.Select()
        grddouble.ClearSelection()
    End Sub

    Private Sub grdcancel_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcancel.CellContentClick

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If (login.depart = "All" Or login.depart = "Production" Or login.depart = "Admin Dispatching") Then
            ticketlines.MdiParent = mdiform
            ticketlines.Focus()
            ticketlines.Show()
            ticketlines.WindowState = FormWindowState.Normal
        Else
            MsgBox("Access denied", MsgBoxStyle.Critical, "")
        End If
    End Sub

    Private Sub txtbags_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbags.TextChanged

    End Sub

    Private Sub PrintPalletTagToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPalletTagToolStripMenuItem.Click
        Dim tn As TreeNode = Me.TreeView2.SelectedNode
        If tn.Tag <> "Location" And tn.Tag <> "Column" Then
            'print pallet tag
            Dim logticketid As Integer = 0
            sql = "Select * from tbllogticket where palletnum='" & tn.Text.Substring(2, tn.Text.Length - 2) & "'"
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
        End If
    End Sub

    Private Sub btnlogcnfitems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogcnfitems.Click
        Try
            'check if wgroup is ticket checker
            If login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" And login.depart <> "All" Then
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            'check if all items are complete
            sql = "Select * from tbllogitem where (status='1' or status='0') and logsheetnum='" & lbltemp.Text & txtsearch.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MsgBox(dr("itemname") & " is still pending. Complete it first.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If
            dr.Dispose()

            ticketcnf = False
            confirmsave.GroupBox1.Text = login.wgroup
            confirmsave.ShowDialog()
            If ticketcnf = True Then
                sql = "Update tbllogsheet set allitems='1' where logsheetnum='" & lbltemp.Text & txtsearch.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()

                MsgBox("Shift " & login.logshift & " successfully cut off.", MsgBoxStyle.Information, "")
                'refresh list ng location
                defaultform()
                lognumsearch()
                viewlocation()
                btnlogcnfitems.Enabled = False
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

    Private Sub cmbfork_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbfork.SelectedIndexChanged

    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        If login.depart = "Production" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            If txtsearch.Text = "" Then
                MsgBox("Cannot set range. Search Ticket Log Sheet # first.", MsgBoxStyle.Exclamation, "")
            Else
                '/ tsetrange.lbllognum.Text = lbltemp.Text & txtsearch.Text
                '/ tsetrange.lblline.Text = lblline.Text
                '/ tsetrange.ShowDialog()
            End If
        Else
            MsgBox("Access denied.", MsgBoxStyle.Critical, "")
        End If
    End Sub
End Class
