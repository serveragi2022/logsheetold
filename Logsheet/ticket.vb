Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class ticket
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim str As String, eqw As Boolean

    Public ticketcnf As Boolean, ticketline As String, trems As String
    Dim AscendingListBox As New List(Of Integer)
    Dim contastart As Boolean = False
    Dim witherrortext As Boolean = False
    Dim kulang As Boolean = False
    Dim withinlist8 As Boolean = False
    Dim selectedrow As Integer = 0
    Dim minval As Double
    Dim maxval As Double
    Dim checkpending As Boolean = False, timercount As Integer = 0, weyttime As Integer = 0

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

    Private Sub ticket_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            Timer1.Stop()
            Timer2.Stop()
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
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        
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
            Dim table = New DataTable()
            table.Columns.Add("palletloc", GetType(String))
            table.Columns.Add("letter", GetType(Char))
            table.Columns.Add("number", GetType(Integer))


            cmbloc.Items.Clear()

            'check active pallet location in tbllocation
            sql = "Select location from tbllocation where status='1' and whsename='" & lblwhse.Text & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                '/cmbloc.Items.Add(dr("location"))
                table.Rows.Add(dr("location"), dr("location").ToString.Substring(dr("location").ToString.Length - 1), Val(dr("location").ToString))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            table.DefaultView.Sort = "letter, number"

            grdloc.Columns.Clear()
            Me.grdloc.DataSource = table

            For Each row As DataGridViewRow In grdloc.Rows
                cmbloc.Items.Add(grdloc.Rows(row.Index).Cells(0).Value)
            Next


            'check active pallet location in tbllocation
            sql = "Select max, maxpallet from tbllocation where status='1' and whsename='" & lblwhse.Text & "' and location='" & Trim(cmbloc.Text) & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
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
            ExecuteViewLine(strconn)

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

    Private Sub ExecuteViewLine(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")

            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                'check bags per pallet where whsename
                sql = "Select bags from tblwhse where whsename='" & lblwhse.Text & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    lblwhse.Tag = dr("bags")
                End If
                dr.Dispose()

                TreeView1.Nodes.Clear()
                listlinestatus.Items.Clear()
                Dim templine As String = ""

                sql = "Select s.palletizer,l.logitemid,l.itemname,l.status from tbllogitem l right outer join tbllogsheet s on s.logsheetid=l.logsheetid"
                sql = sql & " where s.logsheetid='" & lbllogid.Text & "' and l.status<>'3'"
                command.CommandText = sql
                dr = command.ExecuteReader
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

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                TreeView1.ExpandAll()

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.ToString, MsgBoxStyle.Exclamation, "")
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
                    sql = "Select status from tbllogitem where logitemid='" & e.Node.Tag & "'"
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
                panelticket.Enabled = False
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
                Dim sacktype As String = ""
                sql = "Select * from tbllogsheet where logsheetnum='" & lbltemp.Text & Trim(txtsearch.Text) & "' and tbllogsheet.status<>'3' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    lbllogid.Text = dr("logsheetid")

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
                    lblline.Text = dr("palletizer").ToString
                    txtsearch.Text = s.Substring(2, s.Length - 2)
                    lblwhse.Text = dr("whsename").ToString
                    lbldate.Text = Format(dr("logsheetdate"), "yyyy/MM/dd")
                    lblshift.Text = dr("shift").ToString
                    lbllinetext.Text = dr("palletizer").ToString
                    txtsearch.ReadOnly = True
                    lblcreatedshift.Text = dr("shift").ToString
                    lblyear.Text = dr("logsheetyear")
                    txtprod.Text = dr("prodrems").ToString
                    sacktype = dr("bagtype").ToString
                    txttype.Text = dr("printtype").ToString
                    txtbin.Text = dr("binnum").ToString
                    chkwait.Checked = False
                    btnreset.Enabled = False
                    txtgross.ReadOnly = False
                    If IsDBNull(dr("weyt")) = False Then
                        If dr("weyt") = 1 Then
                            chkwait.Checked = True
                            txtgross.ReadOnly = True
                            btnreset.Enabled = True
                        End If
                    End If
                Else
                    If Trim(txtsearch.Text) <> "" Then
                        MsgBox("Cannot find ticket log sheet number.", MsgBoxStyle.Critical, "")
                    End If
                    lbllogid.Text = ""
                    txtsearch.Focus()
                    TreeView1.Nodes.Clear()
                    lblline.Text = ""
                    defaultform()
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                viewticketrange()
                viewline()
                TreeView2.Nodes.Clear()
                lblbin.Text = ""
                lbllistline.Text = ""
                weyttime = 0

                If sacktype = "" Then
                    minval = 25.06
                    maxval = 25.27
                Else
                    sql = "Select * from tblbag where bagtype='" & sacktype & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        minval = dr("minimum")
                        maxval = dr("maximum")
                        weyttime = dr("tym")
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
                

                If lbllinetext.Text.ToString.Contains("1") = True Then
                    SplitContainer1.Panel1.BackColor = Color.MintCream
                    SplitContainer2.Panel2.BackColor = Color.MintCream
                    panelticket.BackColor = Color.MintCream
                    txtrange.BackColor = Color.MintCream
                ElseIf lbllinetext.Text.ToString.Contains("2") = True Then
                    SplitContainer1.Panel1.BackColor = Color.Ivory
                    SplitContainer2.Panel2.BackColor = Color.Ivory
                    panelticket.BackColor = Color.Ivory
                    txtrange.BackColor = Color.Ivory
                Else
                    SplitContainer1.Panel1.BackColor = SystemColors.Control
                    SplitContainer2.Panel2.BackColor = SystemColors.Control
                    panelticket.BackColor = SystemColors.Control
                    txtrange.BackColor = SystemColors.Control
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
            checknumbags()
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtbags_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbags.Leave
        If txtbags.ReadOnly = False Then
            checknumbags()
        End If
    End Sub

    Public Sub checknumbags()
        If Val(txtbags.Text) > Val(lblbags.Text) Then
            MsgBox("Exceeds in maximum number of bags per pallet.", MsgBoxStyle.Exclamation, "")
            txtbags.Text = ""
            txtbags.Focus()
        ElseIf Val(txtbags.Text) = 0 Then
            MsgBox("Number of bags per pallet should not be equal to zero.", MsgBoxStyle.Exclamation, "")
            txtbags.Text = ""
            txtbags.Focus()
        Else
            If lblwhse.Text <> "BRAN WHSE" Then
                If Val(txtastart.Text) <> 0 Then
                    txtbags.Text = Val(txtbags.Text)
                    txtbags.ReadOnly = True

                    If btnsetstart.Tag = 0 Then
                        Exit Sub
                    End If

                    btngenerate.PerformClick()

                    errortext()

                    kulang = ifkulangticket()
                    If kulang = True Then
                        Exit Sub
                    End If
                End If
            Else
                txtbags.Text = Val(txtbags.Text)
                txtbags.ReadOnly = True
            End If
        End If
    End Sub

    Public Sub errortext()
        Try
            'check itemns in grdcancel and grddouble kung within the list8
            'if not then error text in cell 0
            witherrortext = False

            'GRDCANCEL
            For Each rowcnl As DataGridViewRow In grdcancel.Rows
                Dim cnl As Integer = grdcancel.Rows(rowcnl.Index).Cells(0).Value
                Dim withinlist As Boolean = False
                For Each itemcnl As Object In list9.Items
                    If Val(itemcnl) = cnl Then
                        withinlist = True
                        grdcancel.Rows(rowcnl.Index).Cells(0).ErrorText = ""
                        Exit For
                    End If
                Next

                If withinlist = False Then
                    If grdcancel.Rows(rowcnl.Index).Cells(5).Value = 1 Then
                        'bypass error text
                    Else
                        grdcancel.Rows(rowcnl.Index).Cells(0).ErrorText = "   Error Ticket number. Must remove."
                        witherrortext = True
                    End If
                End If
            Next

            'GRDDOUBLE
            For Each rowdbl As DataGridViewRow In grddouble.Rows
                Dim cnl As Integer = Val(grddouble.Rows(rowdbl.Index).Cells(0).Value)
                Dim withinlist As Boolean = False
                For Each itemcnl As Object In list9.Items
                    If Val(itemcnl) = cnl Then
                        withinlist = True
                        grddouble.Rows(rowdbl.Index).Cells(0).ErrorText = ""
                        Exit For
                    End If
                Next

                If withinlist = False Then
                    grddouble.Rows(rowdbl.Index).Cells(0).ErrorText = "   Error Ticket number. Must remove."
                    witherrortext = True
                End If
            Next

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If txtlogitemid.Text <> "" Then

                'check if logsheet is not cancelled
                sql = "Select status,allitems from tbllogsheet where logsheetid='" & lbllogid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Logsheet is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Logsheet is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("allitems") = 1 Then
                        MsgBox("Logsheet is already cutoff.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If lblwhse.Text <> "BRAN WHSE" Then
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
                End If

                'check kung walang errortext
                errortext()
                If witherrortext = True Then
                    MsgBox("Remove error tickets first.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                kulang = ifkulangticket()
                If kulang = True Then
                    Exit Sub
                End If

                ticketcnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If ticketcnf = True Then
                    ExecuteSaveAsDraft(strconn)
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

    Private Sub ExecuteSaveAsDraft(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")

            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                Dim tickettype As String = txttype.Text

                'update wag lang yung addtoloc
                sql = "Update tbllogticket set remarks='" & Trim(txtrems.Text) & "', location='" & Trim(cmbloc.Text) & "', columns='" & CInt(numcol.Value) & "',"
                sql = sql & " bags='" & txtbags.Text & "', palletnum='" & txtpallet.Text & "', letter1='" & txtletter1.Text & "', astart='" & txtastart.Text & "',"
                sql = sql & " letter2='" & txtletter2.Text & "', aend='" & txtaend.Text & "', letter3='" & txtlet1.Text & "', fstart='" & txtfstart.Text & "',"
                sql = sql & " letter4='" & txtlet2.Text & "', fend='" & txtfend.Text & "', gseries='" & lblseries.Text & "', cseries='" & lblcancel.Text & "',"
                sql = sql & " whsechecker='" & txtchecker.Text & "', forklift='" & cmbfork.Text & "', datemodified=GetDate(), modifiedby='" & login.user & "'"
                sql = sql & " where logitemid='" & txtlogitemid.Text & "' and logticketid='" & txtlogticket.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'cancel///////////////////////////////////////////////////////////////////////
                'insert temporary ticket cancel in tbltempcancel
                Dim tbltempcancel As String = "tbltempcancel" & txtlogticket.Text
                Dim tblexistcancel As Boolean = False
                sql = "Select name from sys.tables where name= '" & tbltempcancel & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistcancel = True
                End If
                dr.Dispose()

                If tblexistcancel = False Then
                    'create tbltempcancel
                    sql = "Create Table " & tbltempcancel & " (logcancelid int NOT NULL PRIMARY KEY IDENTITY(1,1), logsheetid int, logitemid int, logticketid int, palletnum nvarchar(MAX), letter nvarchar(50), cticketnum int, remarks nvarchar(MAX), grossw float, cticketdate datetime, status int)"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempcancel where txtlogticket
                    sql = "TRUNCATE Table " & tbltempcancel & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert cancel ticket in temporary table
                For Each row As DataGridViewRow In grdcancel.Rows 'list1 are the list of cancel tickets
                    Dim col0 As String = grdcancel.Rows(row.Index).Cells(0).Value
                    '/Dim col1 As Date = Format(grdcancel.Rows(row.Index).Cells(1).Value, "yyyy/MM/dd HH:mm")
                    Dim col2 As String = grdcancel.Rows(row.Index).Cells(2).Value
                    Dim col3 As String = grdcancel.Rows(row.Index).Cells(3).Value
                    Dim col4 As String = grdcancel.Rows(row.Index).Cells(4).Value
                    sql = "Insert into " & tbltempcancel & " (logsheetid, logitemid, logticketid, palletnum, letter, cticketnum, cticketdate, remarks, grossw, status) "
                    sql = sql & " values ('" & lbllogid.Text & "', '" & txtlogitemid.Text & "', '" & txtlogticket.Text & "', '" & txtpallet.Text & "', '" & col4 & "',"
                    sql = sql & " '" & col0 & "', '" & Format(grdcancel.Rows(row.Index).Cells(1).Value, "yyyy/MM/dd HH:mm") & "', '" & col2 & "', '" & col3 & "', '1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next
                'cancel///////////////////////////////////////////////////////////////////////


                'good///////////////////////////////////////////////////////////////////////
                'insert temporary ticket good in tbltempgood
                Dim tbltempgood As String = "tbltempgood" & txtlogticket.Text
                Dim tblexistgood As Boolean = False
                sql = "Select name from sys.tables where name = '" & tbltempgood & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistgood = True
                End If
                dr.Dispose()

                If tblexistgood = False Then
                    'create tbltempgood
                    sql = "Create Table " & tbltempgood & " (loggoodid int NOT NULL PRIMARY KEY IDENTITY(1,1), logsheetid int, logitemid int, logticketid int, palletnum nvarchar(MAX), letter nvarchar(50), gticketnum int, remarks nvarchar(MAX), status int)"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempgood where txtlogticket
                    sql = "TRUNCATE Table " & tbltempgood & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert good ticket in temporary table
                For Each item As Object In list8.Items 'list8 are the list of valid tickets
                    Dim valticket As Integer = Val(item)
                    Dim letticket As String = item.ToString.Substring(item.ToString.Length - 1)

                    sql = "Insert into " & tbltempgood & " (logsheetid, logitemid, logticketid, palletnum, letter, gticketnum, remarks, status)"
                    sql = sql & " values ('" & lbllogid.Text & "', '" & txtlogitemid.Text & "', '" & txtlogticket.Text & "', '" & txtpallet.Text & "', '" & letticket & "', '" & valticket & "', '', '1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next
                'good///////////////////////////////////////////////////////////////////////


                'double///////////////////////////////////////////////////////////////////////
                'insert temporary ticket double in tbltempdouble
                Dim tbltempdouble As String = "tbltempdouble" & txtlogticket.Text
                Dim tblexistdouble As Boolean = False
                sql = "Select name from sys.tables where name = '" & tbltempdouble & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistdouble = True
                End If
                dr.Dispose()

                If tblexistdouble = False Then
                    'create tbltempdouble
                    sql = "Create Table " & tbltempdouble & " (logdoubleid int NOT NULL PRIMARY KEY IDENTITY(1,1), logsheetid int, logitemid int, logticketid int, palletnum nvarchar(MAX), letter nvarchar(50), dticketnum nvarchar(MAX), remarks nvarchar(MAX), dticketdate datetime, status int)"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempdouble where txtlogticket
                    sql = "TRUNCATE Table " & tbltempdouble & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert double ticket in temporary table
                For Each row As DataGridViewRow In grddouble.Rows 'list1 are the list of double tickets
                    Dim col0 As String = grddouble.Rows(row.Index).Cells(0).Value
                    '/Dim col1 As Date = Format(grddouble.Rows(row.Index).Cells(1).Value, "yyyy/MM/dd HH:mm")
                    Dim col2 As String = grddouble.Rows(row.Index).Cells(2).Value
                    Dim col3 As String = grddouble.Rows(row.Index).Cells(3).Value

                    sql = "Insert into " & tbltempdouble & " (logsheetid, logitemid, logticketid, palletnum, letter, dticketnum, dticketdate, remarks, status)"
                    sql = sql & " values ('" & lbllogid.Text & "', '" & txtlogitemid.Text & "', '" & txtlogticket.Text & "', '" & txtpallet.Text & "',"
                    sql = sql & " '" & col3 & "', '" & col0 & "', '" & Format(grddouble.Rows(row.Index).Cells(1).Value, "yyyy/MM/dd HH:mm") & "', '" & col2 & "', '1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next
                'double///////////////////////////////////////////////////////////////////////


                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully saved.", MsgBoxStyle.Information, "")

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.Message, MsgBoxStyle.Exclamation, "")
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
        numcol.Minimum = 1
        numcol.Value = 1
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
        btncnlpallet.Enabled = False
    End Sub

    Private Sub txtbags_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbags.ReadOnlyChanged
        If txtbags.ReadOnly = True Then
            txtbags.BackColor = Color.White
        Else
            txtbags.BackColor = Color.NavajoWhite
        End If
    End Sub

    Private Sub btngenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngenerate.Click
        genseries()
        errortext()
    End Sub

    Private Sub pollardtrue()
        Label4.Enabled = False
        txtletter1.Enabled = False
        txtastart.Enabled = False
        btnsetstart.Enabled = False
        Label5.Enabled = False
        txtletter2.Enabled = False
        txtaend.Enabled = False
        Label15.Enabled = False
        txtlet1.Enabled = False
        txtfstart.Enabled = False
        Label14.Enabled = False
        txtlet2.Enabled = False
        txtfend.Enabled = False
        Panel2.Enabled = False
    End Sub

    Private Sub pollardfalse()
        Label4.Enabled = True
        txtletter1.Enabled = True
        txtastart.Enabled = True
        btnsetstart.Enabled = True
        Label5.Enabled = True
        txtletter2.Enabled = True
        txtaend.Enabled = True
        Label15.Enabled = True
        txtlet1.Enabled = True
        txtfstart.Enabled = True
        Label14.Enabled = True
        txtlet2.Enabled = True
        txtfend.Enabled = True
        Panel2.Enabled = True
    End Sub

    Public Sub genseries()
        Try
            If lblwhse.Text = "BRAN WHSE" Then
                pollardtrue()
                Exit Sub
            Else
                pollardfalse()
            End If

            list2.Items.Clear()
            lblseries.Text = ""
            txtfstart.Text = Val(txtastart.Text)

            If list1.Items.Count <> 0 Then
                'final start
                Dim initialindex As Integer = -1
                For i = 0 To list7.Items.Count - 1
                    If Val(list7.Items(i)) = Val(txtastart.Text) Then
                        initialindex = i
                    End If

                    If initialindex <> -1 Then
                        'check if nasa list 1 yung item
                        Dim initialcnl As Boolean = False
                        For ii = 0 To list1.Items.Count - 1
                            If Val(list7.Items(i)) = Val(list1.Items(ii)) Then
                                initialcnl = True
                            End If
                        Next

                        If initialcnl = False Then
                            txtfstart.Text = Val(list7.Items(i))
                            Exit For
                        End If
                    End If
                Next
            End If


            '//////////////////////////list8
            Dim ctrnew As Integer = 0, firstindex7 As Integer = 0
            list8.Items.Clear()
            For i = 0 To list7.Items.Count - 1
                If list8.Items.Count = 0 Then
                    If Val(list7.Items(i)) = Val(txtfstart.Text) Then
                        firstindex7 = i

                        Dim itemiscancel As Boolean = False
                        For Each cnlitem As Object In list1.Items
                            If Val(cnlitem) = Val(list7.Items(i)) Then
                                itemiscancel = True
                                Exit For
                            End If
                        Next

                        If itemiscancel = False Then
                            ctrnew += 1
                            list8.Items.Add(list7.Items(i))
                        End If
                    Else
                        If i > firstindex7 And firstindex7 <> 0 Then
                            Dim itemiscancel As Boolean = False
                            For Each cnlitem As Object In list1.Items
                                If Val(cnlitem) = Val(list7.Items(i)) Then
                                    itemiscancel = True
                                    Exit For
                                End If
                            Next

                            If itemiscancel = False Then
                                ctrnew += 1
                                list8.Items.Add(list7.Items(i))
                            End If
                        End If
                    End If
                Else
                    If ctrnew <= ((Val(txtbags.Text) - list6.Items.Count) - 1) Then
                        Dim itemiscancel As Boolean = False
                        For Each cnlitem As Object In list1.Items
                            If Val(cnlitem) = Val(list7.Items(i)) Then
                                itemiscancel = True
                                Exit For
                            End If
                        Next

                        If itemiscancel = False Then
                            ctrnew += 1
                            list8.Items.Add(list7.Items(i))
                        End If
                    End If
                End If
            Next


            'expected end
            '///txtaend.Text = Val(txtastart.Text) + Val(txtbags.Text) - 1
            Dim itemindex As Integer = 0, itemctr As Integer = 0
            For Each item As Object In list7.Items
                itemctr += 1
                If Val(item) = Val(txtastart.Text) Then
                    itemindex = (itemctr - 1) + (Val(txtbags.Text) - 1)
                End If
            Next

            If itemindex > list7.Items.Count - 1 Then
                MsgBox("Insufficient ticket.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If
            Dim aendnum As String = list7.Items(itemindex)
            txtletter2.Text = aendnum.Substring(aendnum.Length - 1)
            txtaend.Text = Val(list7.Items(itemindex))


            'final end
            '///txtfend.Text = Val(txtastart.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count - list6.Items.Count
            Dim fendnum As String = list8.Items(list8.Items.Count - 1)
            txtlet2.Text = fendnum.Substring(fendnum.Length - 1)
            txtfend.Text = Val(list8.Items(list8.Items.Count - 1))

            'check if may bypass errortext
            For Each row As DataGridViewRow In grdcancel.Rows
                Dim cells0 As Integer = grdcancel.Rows(row.Index).Cells(0).Value
                Dim cells5 As Integer = grdcancel.Rows(row.Index).Cells(5).Value
                If cells5 = 1 Then
                    txtfend.Text = cells0
                End If
            Next

            '//////////////////////////list9
            Dim ctrlist9 As Integer = 0, firstindex9 As Integer = 0, lastindex9 As Integer = 0
            list9.Items.Clear()
            For i = 0 To list7.Items.Count - 1
                If Val(list7.Items(i)) = Val(txtastart.Text) Then
                    firstindex9 = i
                ElseIf Val(list7.Items(i)) = Val(txtfend.Text) Then
                    lastindex9 = i
                End If

                If firstindex9 <> 0 And lastindex9 <> 0 Then
                    Exit For
                End If
            Next

            For i = firstindex9 To lastindex9
                list9.Items.Add(list7.Items(i))
            Next


            Dim ctr As Integer = 0
            list5.Items.Clear()

            For i = Val(txtastart.Text) To Val(txtfend.Text)
                list4.Items.Clear()
                For Each item As Object In list1.Items
                    If Val(item) <> i Then
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
                    'MsgBox(list3.Items(list3.Items.Count - 1))
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
                '/AscendingListBox.Clear()
                '/For i = 0 To list1.Items.Count - 1
                '/AscendingListBox.Add(CInt(Val(list1.Items(i))))
                '/Next
                '/AscendingListBox.Sort()
                '/list1.Items.Clear()
                '/For i = 0 To AscendingListBox.Count - 1
                '/list1.Items.Add(AscendingListBox(i))
                '/Next
            End If

            'generate cancel tickets in lblcancel
            lblcancel.Text = ""
            For i = 0 To grdcancel.Rows.Count - 1
                If Trim(lblcancel.Text) = "" Then
                    lblcancel.Text = grdcancel.Rows(i).Cells(4).Value & " " & grdcancel.Rows(i).Cells(0).Value
                Else
                    lblcancel.Text = lblcancel.Text & ", " & grdcancel.Rows(i).Cells(4).Value & " " & grdcancel.Rows(i).Cells(0).Value
                End If
            Next

            genticket()

            'generate double tickets in series
            Dim dbltic As String = ""
            If list6.Items.Count <> 0 Then
                For Each item As Object In list6.Items
                    Dim lett As String = item
                    lett = lett.Substring(lett.Length - 1)
                    If dbltic = "" Then
                        dbltic = lett & " " & Val(item) & "D"
                    Else
                        dbltic = dbltic & ", " & lett & " " & Val(item) & "D"
                    End If
                Next
            End If

            If dbltic <> "" Then
                lblseries.Text = dbltic & ", " & lblseries.Text
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub genticket()
        Try
            'generate series
            list10.Items.Clear()
            Dim ctrlist As Integer = 0
            Dim temp As String = "", first As String = "", last As String = ""
            Dim letter1 As String = "", letter2 As String = ""

            For Each item As Object In list8.Items
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

                If list8.Items.Count >= ctrlist Then
                    If ctrlist > list8.Items.Count - 1 Then
                        If first = temp Then
                            Dim gtiket As String = first
                            Dim tempzero As String = ""

                            If gtiket < 1000000 Then
                                For vv As Integer = 1 To 6 - gtiket.Length
                                    '/tempzero += "0"
                                Next
                            End If


                            list10.Items.Add(letter1 & " " & tempzero & gtiket)
                            '/MsgBox(" ---1")
                            last = gtiket

                        Else
                            Dim gtiket As String = first
                            Dim tempzero As String = ""
                            If gtiket < 1000000 Then
                                For vv As Integer = 1 To 6 - gtiket.Length
                                    '/tempzero += "0"
                                Next
                            End If


                            Dim gtikettemp As String = temp
                            Dim tempzerotemp As String = ""
                            If gtikettemp < 1000000 Then
                                For vv As Integer = 1 To 6 - gtikettemp.Length
                                    '/tempzerotemp += "0"
                                Next
                            End If

                           
                            list10.Items.Add(letter1 & " " & tempzero & gtiket & " - " & letter2 & " " & tempzerotemp & gtikettemp)
                            '/MsgBox("---2")
                            last = gtikettemp
                        End If
                    Else
                        If temp + 1 < Val(list8.Items(ctrlist)) Then
                            If first = temp Then
                                Dim gtiket As String = first
                                Dim tempzero As String = ""

                                If gtiket < 1000000 Then
                                    For vv As Integer = 1 To 6 - gtiket.Length
                                        '/tempzero += "0"
                                    Next
                                End If


                                list10.Items.Add(letter1 & " " & tempzero & gtiket)
                                '/MsgBox(" ---3")
                                last = gtiket
                            Else
                                Dim gtiket As String = first
                                Dim tempzero As String = ""
                                If gtiket < 1000000 Then
                                    For vv As Integer = 1 To 6 - gtiket.Length
                                        '/tempzero += "0"
                                    Next
                                End If


                                Dim gtikettemp As String = temp
                                Dim tempzerotemp As String = ""
                                If gtikettemp < 1000000 Then
                                    For vv As Integer = 1 To 6 - gtikettemp.Length
                                        '/tempzerotemp += "0"
                                    Next
                                End If


                                list10.Items.Add(letter1 & " " & tempzero & gtiket & " - " & letter2 & " " & tempzerotemp & gtikettemp)
                                '/MsgBox(" ---4")
                                last = gtikettemp
                            End If
                            first = ""

                        ElseIf temp + 1 > Val(list8.Items(ctrlist)) Then
                            'next series na ibig sabihin mag kaiba na letter nito
                            '/MsgBox(temp + 1 & " " & Val(list8.Items(ctrlist)))

                            Dim gtiket As String = first
                            Dim tempzero As String = ""
                            If gtiket < 1000000 Then
                                For vv As Integer = 1 To 6 - gtiket.Length
                                    '/tempzero += "0"
                                Next
                            End If


                            Dim gtiketlast As String = Val(last)
                            Dim tempzerotemp As String = ""
                            If gtiketlast < 1000000 Then
                                For vv As Integer = 1 To 6 - gtiketlast.Length
                                    '/tempzerotemp += "0"
                                Next
                            End If

                            list10.Items.Add(letter1 & " " & tempzero & gtiket & " - " & letter2 & " " & tempzerotemp & gtiketlast)

                            first = ""
                        End If
                    End If
                End If
            Next

            Dim ofseries As String = ""
            For Each item As Object In list10.Items
                ofseries = ofseries & item & ", "
            Next
            lblseries.Text = ofseries

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

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        Dim a As String = MsgBox("Are you sure you want to clear all invalid tickets and double tickets?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
        If a = vbYes Then
            list1.Items.Clear()
            list2.Items.Clear()
            list6.Items.Clear()
            lblcancel.Text = ""
            grdcancel.Rows.Clear()
            grddouble.Rows.Clear()
            btngenerate.PerformClick()
        End If
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

            If Val(txtcancel.Text) <> 0 And Val(txtastart.Text) <> 0 Then
                'check if valid ung cancel ticket number
                'check kung nasa list8
                Dim winthinlist As Boolean = False, winthincancel As Boolean = False

                For Each item As Object In list8.Items
                    If Val(item) = Val(txtcancel.Text) Then
                        winthinlist = True
                    End If
                Next

                If winthinlist = False Then
                    'wala sa list8 ng goods

                    If Trim(cmbreason.Text).ToString.Contains("Double") = True Then
                        For Each item As Object In list1.Items
                            If Val(item) = Val(txtcancel.Text) Then
                                winthincancel = True
                            End If
                        Next

                        If winthincancel = False Then
                            Me.Cursor = Cursors.Default
                            MsgBox("Invalid input", MsgBoxStyle.Exclamation, "")
                            checkweyt()
                            txtcancel.Text = ""
                            txtcancel.Focus()
                            Exit Sub
                        End If
                    Else
                        Me.Cursor = Cursors.Default
                        MsgBox("Invalid input", MsgBoxStyle.Exclamation, "")
                        checkweyt()
                        txtcancel.Text = ""
                        txtcancel.Focus()
                        Exit Sub
                    End If
                End If
            Else
                checkweyt()
                txtcancel.Text = ""
                cmbreason.Enabled = False
                If chkwait.Checked = False Then
                    txtgross.Enabled = False
                End If
                Exit Sub
            End If

            If cmbreason.Text = "Under Weight" And Not (Val(txtgross.Text) < minval) And txtgross.Text <> "" Then
                MsgBox("Invalid input of gross weight.", MsgBoxStyle.Exclamation, "")
                txtgross.Focus()
                Exit Sub
            ElseIf cmbreason.Text = "Over Weight" And Not (Val(txtgross.Text) > maxval) And txtgross.Text <> "" Then
                MsgBox("Invalid input of gross weight.", MsgBoxStyle.Exclamation, "")
                txtgross.Focus()
                Exit Sub
            End If

            If chkwait.Checked = True Then
                If txtgross.BackColor <> Color.FromArgb(255, 255, 192) Then
                    Me.Cursor = Cursors.Default
                    MsgBox("Please wait for the final weight.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
            End If

            'check if nasa list ng list9 kasi dito cover nya lahat ng range astart to fend
            Dim nasalist As Boolean = False, itemwlet As String = "", lettr As String = ""
            For Each item As Object In list9.Items
                If Val(item) = Val(txtcancel.Text) Then
                    nasalist = True
                    itemwlet = item
                    lettr = item
                    lettr = lettr.Substring(lettr.Length - 1)
                    Exit For
                End If
            Next


            If Trim(cmbreason.Text).ToString.Contains("Double") = True Then
                'double ticket meaning additional ticket
                'LIST1 = cancel or missing ticket
                'LIST6 = double ticket

                Dim containslist As Boolean = False
                For Each item As Object In list6.Items
                    If Val(item) = Val(txtcancel.Text) Then
                        containslist = True
                        Exit For
                    End If
                Next

                If containslist = True Then
                    Me.Cursor = Cursors.Default
                    MsgBox("Ticket number is already added in double ticket.", MsgBoxStyle.Exclamation, "")
                    txtcancel.Text = ""
                    txtcancel.Focus()
                    Exit Sub
                Else
                    'check if nsa cancel na tapos missing ticket
                    Dim nsacancelmissing As Boolean = False
                    For Each row As DataGridViewRow In grdcancel.Rows
                        Dim ctick As Integer = grdcancel.Rows(row.Index).Cells(0).Value
                        Dim creas As String = grdcancel.Rows(row.Index).Cells(2).Value
                        If ctick = Val(txtcancel.Text) And creas.ToString.Contains("Missing") = True Then
                            MsgBox("Cannot add double ticket that is already added as a missing ticket.", MsgBoxStyle.Exclamation, "")
                            txtcancel.Text = ""
                            txtcancel.Focus()
                            Exit Sub
                        End If
                    Next

                    list6.Items.Add(itemwlet)

                    Dim canceldate As Date
                    sql = "Select GETDATE()"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    canceldate = cmd.ExecuteScalar
                    cmd.Dispose()
                    conn.Close()

                    grddouble.Rows.Add(Val(txtcancel.Text) & "D", canceldate, Trim(cmbreason.Text), lettr)
                    grddouble.Sort(grddouble.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
                    txtcancel.Text = ""
                    checkweyt()
                End If

            Else
                'cancel or missing ticket\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

                Dim containslist As Boolean = False
                For Each item As Object In list1.Items
                    If Val(item) = Val(txtcancel.Text) Then
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
                    If Trim(cmbreason.Text).ToString.Contains("Missing") = True Then
                        'check kung missing tapos nsa doble na ibig sabihin hindi na pwede
                        Dim nsadouble As Boolean = False
                        For Each row As DataGridViewRow In grddouble.Rows
                            Dim ctick As Integer = Val(grddouble.Rows(row.Index).Cells(0).Value)
                            If ctick = Val(txtcancel.Text) Then
                                MsgBox("Cannot add missing ticket that is already added as a double ticket.", MsgBoxStyle.Exclamation, "")
                                txtcancel.Text = ""
                                txtcancel.Focus()
                                Exit Sub
                            End If
                        Next
                    End If
                    
                    '/MsgBox(list1.Items.Contains(Val(txtcancel.Text)))
                    list1.Items.Add(itemwlet)

                    Dim canceldate As Date
                    sql = "Select GETDATE()"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    canceldate = cmd.ExecuteScalar
                    cmd.Dispose()
                    conn.Close()

                    grdcancel.Rows.Add(Val(txtcancel.Text), canceldate, Trim(cmbreason.Text), txtgross.Text, lettr, 0)
                    grdcancel.Sort(grdcancel.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
                    txtcancel.Text = ""
                    checkweyt()
                End If
            End If

            btngenerate.PerformClick()

            If chkwait.Checked = False Then
                txtgross.Enabled = False
            Else
                'overwrite sa rs232 txt into zero
                If IO.File.Exists(Application.StartupPath() & "\RS232Capture.txt") Then
                    Dim file As System.IO.StreamWriter
                    file = My.Computer.FileSystem.OpenTextFileWriter((Application.StartupPath() & "\RS232Capture.txt"), False)
                    file.WriteLine("0.00")
                    file.Close()
                Else
                    System.IO.File.CreateText(Application.StartupPath() & "\RS232Capture.txt")
                    Dim file As System.IO.StreamWriter
                    file = My.Computer.FileSystem.OpenTextFileWriter((Application.StartupPath() & "\RS232Capture.txt"), False)
                    file.WriteLine("0.00")
                    file.Close()
                End If

                Timer1.Start()
                txtgross.BackColor = Color.White
                timercount = weyttime
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

    Private Sub txtastart_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtastart.KeyDown

    End Sub

    Private Sub txtastart_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtastart.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtastart_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtastart.Leave
        Try
            If Val(txtastart.Text) <> 0 Then
                contastart = validateastart()
                If contastart = False Then
                    Exit Sub
                End If

                If btnsetstart.Tag = 1 Then
                    '/txtastart.ReadOnly = True
                    txtastart.BackColor = Color.White

                    'expected end
                    txtaend.Text = Val(txtastart.Text) + Val(txtbags.Text) - 1
                    txtfstart.Text = Val(txtastart.Text)

                    'final end
                    txtfend.Text = Val(txtastart.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count - list6.Items.Count

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

            If lblwhse.Text = "BRAN WHSE" Then
                btnloc.Enabled = True
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
            'check if bypass 1
            If Val(grdcancel.Rows(row.Index).Cells(5).Value) = 1 Then
                Dim numsel As Integer = Val(grdcancel.Rows(row.Index).Cells(0).Value)

                For i = grdcancel.Rows.Count - 1 To 0 Step -1
                    If Val(grdcancel.Rows(i).Cells(0).Value) = numsel Then

                        Dim listitem As Integer = Val(grdcancel.Rows(row.Index).Cells(0).Value)
                        grdcancel.Rows.Remove(row)
                        '/grdcancel.SortedColumn

                        For l_index As Integer = 0 To list1.Items.Count - 1
                            Dim l_text As String = CStr(list1.Items(l_index))
                            If listitem = Val(l_text) Then
                                list1.Items.RemoveAt(l_index)
                                Exit For
                            End If
                        Next

                        For l_index As Integer = 0 To list9.Items.Count - 1
                            Dim l_text As String = CStr(list9.Items(l_index))
                            If listitem = Val(l_text) Then
                                list9.Items.RemoveAt(l_index)
                                Exit For
                            End If
                        Next

                        genseries()
                        btngenerate.PerformClick()

                        Exit Sub

                    Else
                        MsgBox("Invalid remove.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                Next

            Else
                Dim listindex As Integer = row.Index
                Dim listitem As Integer = Val(grdcancel.Rows(row.Index).Cells(0).Value)

                grdcancel.Rows.Remove(row)
                '/grdcancel.SortedColumn

                For l_index As Integer = 0 To list1.Items.Count - 1
                    Dim l_text As String = CStr(list1.Items(l_index))
                    If listitem = Val(l_text) Then
                        list1.Items.RemoveAt(l_index)
                        Exit For
                    End If
                Next
            End If
        Next
       
        For Each row As DataGridViewRow In grddouble.SelectedRows
            Dim listindex As Integer = row.Index
            Dim listitem As Integer = Val(grddouble.Rows(row.Index).Cells(0).Value)

            grddouble.Rows.Remove(row)
            '/grddouble.SortedColumn

            For l_index As Integer = 0 To list6.Items.Count - 1
                Dim l_text As String = CStr(list6.Items(l_index))
                If listitem = Val(l_text) Then
                    list6.Items.RemoveAt(l_index)
                    Exit For
                End If
            Next
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
            cmbreason.Enabled = True
            cmbreason.Focus()
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 68 Or Asc(e.KeyChar) = 100 Then

        ElseIf Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtcancel_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcancel.Leave
        If Trim(txtcancel.Text) <> "" Then
            txtcancel.Text = Val(txtcancel.Text)
            cmbreason.Enabled = True
            cmbreason.Focus()
        End If
    End Sub

    Private Sub AddPalletToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddPalletToolStripMenuItem.Click
        Try
            'check if wgroup is ticket checker
            If login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" And login.depart <> "All" Then
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            'check if logsheet is not cancelled
            sql = "Select status, allitems from tbllogsheet where logsheetid='" & lbllogid.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                If dr("status") = 3 Then
                    MsgBox("Logsheet is already cancelled.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                ElseIf dr("status") = 2 Then
                    MsgBox("Logsheet is already completed.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                ElseIf dr("allitems") = 1 Then
                    MsgBox("Logsheet is already cutoff.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

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
                checkpending = False
                Dim pendingticketid As Integer
                sql = "Select addtoloc,logticketid from tbllogticket where logitemid='" & tn.Tag & "' and status<>'3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If IsDBNull(dr("addtoloc")) = True Then
                        checkpending = True
                        pendingticketid = dr("logticketid")
                        Exit While
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
                    Else
                        'check kung yung remaining tickets are less than 40 is not bran, walang ganto pag bran kasi wala nmng ticket range yun
                        'MsgBox(remaining(lbllogid.Text))
                    End If

                    ExecuteAddPallet(strconn)

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
                ExecuteViewPendingPallet(strconn, pendingticketid, tn)

                viewticketrange()
                viewinitialticket()

                txtrems.ReadOnly = False

                If txtfend.Text <> "" Then
                    '/txtastart.ReadOnly = True
                    txtastart.BackColor = Color.White
                    btnsetstart.Image = LogSheet.My.Resources.Resources.cancel
                    btnsetstart.Tag = 1
                Else
                    '/txtastart.ReadOnly = False
                    txtastart.BackColor = Color.NavajoWhite
                    btnsetstart.Image = LogSheet.My.Resources.Resources.ok
                    btnsetstart.Tag = 0
                End If

                'view ticket type
                sql = "Select printtype from tbllogsheet where logsheetid='" & lbllogid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    txttype.Text = dr("printtype").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If txttype.Text <> "Inkjet" And txttype.Text <> "Receive" And txttype.Text <> "Bran" Then
                    sql = "Select tickettype from tblitems where itemname='" & txtitem.Text & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        txttype.Text = dr("tickettype")
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If

                'forklift
                If cmbfork.Text = "" Then
                    sql = "Select TOP 2 forklift from tbllogticket where logsheetid='" & lbllogid.Text & "' and tbllogticket.status<>'3' order by logticketid DESC"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        cmbfork.Text = dr("forklift").ToString
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If

                'view location in treeview2 /////////////////////////////////////////////////
                viewlocation()
                TreeView2.Nodes(0).ExpandAll()
                TreeView2.SelectedNode = TreeView2.Nodes(0)

                panelticket.Enabled = True
                txtrems.Enabled = True

                viewpalletloc()
            End If

            numcol.Enabled = False

            genseries()

            If chkwait.Checked = True Then
                Timer1.Start()
                txtgross.BackColor = Color.White
                timercount = weyttime
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

    Private Sub ExecuteAddPallet(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                'insert tbllogticket
                'check number of bags in whse
                Dim bagnum As Integer
                sql = "Select bags from tblwhse where whsename='" & lblwhse.Text & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    bagnum = dr("bags")
                End If
                dr.Dispose()

                'check kung pang ilang palletnum na
                Dim palnum As String = "", palreftag As String = ""
                sql = "Select Count(t.logticketid) from tbllogticket t"
                sql = sql & " inner join tbllogsheet s on s.logsheetid=t.logsheetid"
                sql = sql & " where s.logsheetyear='" & lblyear.Text & "' and s.branch='" & login.branch & "'"
                command.CommandText = sql
                palnum = command.ExecuteScalar + 1

                Dim temp As String = ""
                If palnum < 1000000 Then
                    For vv As Integer = 1 To 6 - palnum.Length
                        temp += "0"
                    Next
                End If

                palreftag = lblyear.Text.ToString.Substring(lblyear.Text.Length - 2) & "-" & temp & palnum

                'check if may pallettag na ganun
                sql = "Select palletnum from tbllogticket where palletnum='" & palreftag & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    MsgBox("Try again.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()

                'insert dun sa tbllogticket
                sql = "Insert into tbllogticket (logsheetid, logitemid, palletnum, bags, location, columns, gseries, cseries, qadispo, qarems, cusreserve, customer, ofnum, datecreated, createdby, datemodified, modifiedby, status)"
                sql = sql & " values ('" & lbllogid.Text & "', '" & txtlogitemid.Text & "', '" & palreftag & "', '" & bagnum & "', '', '1', '', '', '0', '', '0', '', '', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1')"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'update set tbllogitem set status into 0 para in process na sya
                sql = "Update tbllogitem set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where logitemid='" & txtlogitemid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                '/MsgBox("Successfully added.", MsgBoxStyle.Information, "")

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

    Private Sub ExecuteViewPendingPallet(ByVal connectionString As String, ByVal pendingticketid As Integer, ByVal tn As TreeNode)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                If checkpending = True Then
                    sql = "Select * from tbllogticket where logitemid='" & tn.Tag & "' and logticketid='" & pendingticketid & "'"
                Else
                    sql = "Select * from tbllogticket where logitemid='" & tn.Tag & "' and addtoloc is NULL and tbllogticket.status<>'3'"
                End If
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    If IsDBNull(dr("addtoloc")) = True Then
                        lblline.Text = tn.Parent.Text
                        txtitem.Text = tn.Text
                        txtlogitemid.Text = tn.Tag
                        txtpallet.Text = dr("palletnum")
                        txtlogticket.Text = dr("logticketid")
                        txtbags.Text = dr("bags")
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
                        txtrems.Text = dr("remarks").ToString
                    End If
                End If
                dr.Dispose()
                '////////////////////////////////////////////////////////////////////////////

                If checkpending = True Then
                    'view cancel tickets in temporary table in list1 where logticketid = txtlogticket.Text
                    'view cancel tickets in temporary table in grdcancel where logticketid = txtlogticket.Text
                    list1.Items.Clear()
                    grdcancel.Rows.Clear()
                    Dim tbltempnamecancel As String = "tbltempcancel" & txtlogticket.Text
                    Dim tblexistcancel As Boolean = False
                    sql = "Select name from sys.tables where name = '" & tbltempnamecancel & "'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    If dr.Read Then
                        tblexistcancel = True
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    If tblexistcancel = True Then
                        sql = "Select * from " & tbltempnamecancel & " where logticketid='" & txtlogticket.Text & "'"
                        command.CommandText = sql
                        dr = command.ExecuteReader
                        While dr.Read
                            list1.Items.Add(dr("cticketnum") & " " & dr("letter"))
                            grdcancel.Rows.Add(Val(dr("cticketnum")), dr("cticketdate"), dr("remarks"), dr("grossw"), dr("letter"), 0)
                        End While
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()
                    End If     'cancel////////////////////////////////////////////////////////////////////////////////

                    'view good tickets in list5 where logticketid = txtlogticket.Text
                    list5.Items.Clear()
                    Dim tbltempnamegood As String = "tbltempgood" & txtlogticket.Text
                    Dim tblexistgood As Boolean = False
                    sql = "Select name from sys.tables where name = '" & tbltempnamegood & "'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    If dr.Read Then
                        tblexistgood = True
                    End If
                    dr.Dispose()

                    If tblexistgood = True Then
                        sql = "Select * from " & tbltempnamegood & " where logticketid='" & txtlogticket.Text & "'"
                        command.CommandText = sql
                        dr = command.ExecuteReader
                        While dr.Read
                            list5.Items.Add(dr("gticketnum"))
                        End While
                        dr.Dispose()
                    End If    'good////////////////////////////////////////////////////////////////////////////////



                    'view double tickets in temporary table in grddouble where logticketid = txtlogticket.Text
                    list6.Items.Clear()
                    grddouble.Rows.Clear()
                    Dim tbltempnamedouble As String = "tbltempdouble" & txtlogticket.Text
                    Dim tblexistdouble As Boolean = False
                    sql = "Select name from sys.tables where name = '" & tbltempnamedouble & "'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    If dr.Read Then
                        tblexistdouble = True
                    End If
                    dr.Dispose()

                    If tblexistdouble = True Then
                        sql = "Select * from " & tbltempnamedouble & " where logticketid='" & txtlogticket.Text & "'"
                        command.CommandText = sql
                        dr = command.ExecuteReader
                        While dr.Read
                            list6.Items.Add(dr("dticketnum") & " " & dr("letter"))
                            grddouble.Rows.Add(dr("dticketnum"), dr("dticketdate"), dr("remarks"), dr("letter"))
                        End While
                        dr.Dispose()
                    End If     'double////////////////////////////////////////////////////////////////////////////////
                End If

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.Message, MsgBoxStyle.Exclamation, "")
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

    Public Sub viewticketrange()
        Try
            'View ticket range
            list7.Items.Clear()

            Dim rctr As Integer = 0, temprange As String = ""
            sql = "Select * from tbllogrange where logsheetid='" & lbllogid.Text & "' and status<>'3' order by letter1,arfrom"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                rctr += 1

                Dim temp1 As String = ""
                If dr("arfrom") < 1000000 Then
                    For vv As Integer = 1 To 6 - dr("arfrom").ToString.Length
                        temp1 += "0"
                    Next
                End If

                If rctr = 1 Then
                    lbllet1.Text = dr("letter1")
                    txtletter1.Text = lbllet1.Text
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
                    list7.Items.Add(i & " " & dr("letter1"))
                Next
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            txtrange.Text = temprange

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
            If lblwhse.Text = "BRAN WHSE" Then
                Exit Sub
            End If

            'view initial start ticket
            sql = "Select TOP 1 * from tbllogticket where logsheetid='" & lbllogid.Text & "' and logticketid<>'" & txtlogticket.Text & "' and tbllogticket.status<>'3' order by logticketid DESC"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                Dim tempstart As Integer = 0, withinlist As Boolean = False, ctritem As Integer = 0, fendctr As Integer = 0
                'get yung final end ticket plus one
                tempstart = dr("fend") + 1
                For Each item As Object In list7.Items
                    ctritem += 1
                    If Val(item) = dr("fend") Then
                        fendctr = ctritem
                    End If
                    If Val(item) = tempstart Then
                        withinlist = True
                        Exit For
                    End If
                Next

                If withinlist = True Then
                    txtletter1.Text = dr("letter4")
                    txtastart.Text = dr("fend") + 1
                Else
                    Dim astartnum As String = list7.Items(fendctr)
                    txtletter1.Text = astartnum.Substring(astartnum.Length - 1)
                    txtastart.Text = Val(list7.Items(fendctr))
                End If

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
            If lblwhse.Text <> "BRAN WHSE" Then
                If btnsetstart.Tag = 0 Then
                    MsgBox("Cannot save. Set initial start ticket number.", MsgBoxStyle.Exclamation, "")
                    txtastart.Focus()
                    Exit Sub
                End If
            End If

            'check kung walang errortext
            errortext()
            If witherrortext = True Then
                MsgBox("Remove error tickets first.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            kulang = ifkulangticket()
            If kulang = True Then
                Exit Sub
            End If

            'check if logsheet is not cancelled
            sql = "Select status, allitems from tbllogsheet where logsheetid='" & lbllogid.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                If dr("status") = 3 Then
                    MsgBox("Logsheet is already cancelled.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                ElseIf dr("status") = 2 Then
                    MsgBox("Logsheet is already completed.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                ElseIf dr("allitems") = 1 Then
                    MsgBox("Logsheet is already cutoff.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            btngenerate.PerformClick()

            'check if kumpleto details
            If Trim(cmbloc.Text) <> "" And Trim(cmbfork.Text) <> "" Then
                If lblwhse.Text <> "BRAN WHSE" And (Trim(txtastart.Text) = "" Or Trim(lblseries.Text) = "") Then
                    MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
                Else
                    Dim a As String = MsgBox("Are you sure you want to add pallet tag # " & txtpallet.Text & " to " & Trim(cmbloc.Text) & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                    If a = vbYes Then
                        ExecuteAddtoLocation(strconn)
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

    Private Sub ExecuteAddtoLocation(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                Dim tickettype As String = txttype.Text

                'insert sa tblcancel yung mga tickets
                For Each row As DataGridViewRow In grdcancel.Rows 'list1 are the list of cancel tickets
                    Dim col0 As String = grdcancel.Rows(row.Index).Cells(0).Value
                    '/Dim col1 As Date = Format(grdcancel.Rows(row.Index).Cells(1).Value, "yyyy/MM/dd HH:mm")
                    Dim col2 As String = grdcancel.Rows(row.Index).Cells(2).Value
                    Dim col3 As String = grdcancel.Rows(row.Index).Cells(3).Value
                    Dim col4 As String = grdcancel.Rows(row.Index).Cells(4).Value

                    sql = "Select c.logticketid from tbllogcancel c"
                    sql = sql & " Right outer join tbllogsheet s on s.logsheetid=c.logsheetid"
                    sql = sql & " where s.logsheetyear=CAST(YEAR(GetDate()) as nvarchar(50)) and s.logsheetid='" & lbllogid.Text & "'"
                    sql = sql & " And c.logitemid='" & txtlogitemid.Text & "' and c.logticketid='" & txtlogticket.Text & "' and c.palletnum='" & txtpallet.Text & "'"
                    sql = sql & " And s.branch='" & login.branch & "' and c.tickettype='" & txttype.Text & "' and c.letter='" & col4 & "' and c.cticketnum='" & col0 & "' "
                    sql = sql & " And c.cticketdate='" & Format(grdcancel.Rows(row.Index).Cells(1).Value, "yyyy/MM/dd HH:mm") & "' and c.remarks='" & col2 & "' and c.grossw='" & col3 & "' and c.status='1'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    If dr.Read Then

                    Else
                        dr.Dispose()
                        sql = "Insert into tbllogcancel (logsheetid, logitemid, logticketid, palletnum, tickettype, letter, cticketnum, cticketdate, remarks, grossw, status) "
                        sql = sql & " values ('" & lbllogid.Text & "', '" & txtlogitemid.Text & "', '" & txtlogticket.Text & "', '" & txtpallet.Text & "', '" & txttype.Text & "',"
                        sql = sql & " '" & col4 & "', '" & col0 & "', '" & Format(grdcancel.Rows(row.Index).Cells(1).Value, "yyyy/MM/dd HH:mm") & "', '" & col2 & "', '" & col3 & "', '1')"
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    End If
                    dr.Dispose()
                Next

                If lblwhse.Text = "BRAN WHSE" Then
                    list8.Items.Clear()
                    For i = 1 To Val(txtbags.Text)
                        list8.Items.Add(i)
                    Next
                End If

                'insert sa tblgood yung mga tickets
                For Each item As Object In list8.Items 'list8 are the list of valid tickets
                    If lblwhse.Text = "BRAN WHSE" Then
                        'check if tblloggood count = no of bags else insert
                        sql = "Select count(loggoodid) from tblloggood g "
                        sql = sql & " Right outer join tbllogticket t on t.logticketid=g.logticketid"
                        sql = sql & " Right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                        sql = sql & " where s.logsheetyear = CAST(Year(GetDate()) As nvarchar(50)) And s.logsheetid='" & lbllogid.Text & "' and g.logitemid='" & txtlogitemid.Text & "'"
                        sql = sql & " And s.branch='" & login.branch & "' and g.logticketid='" & txtlogticket.Text & "' and g.palletnum='" & txtpallet.Text & "' "
                        sql = sql & " And g.tickettype='" & txttype.Text & "' and g.status='1'"
                        command.CommandText = sql
                        Dim ctr As Integer = command.ExecuteScalar
                        If ctr <> Val(txtbags.Text) Then
                            sql = "Insert into tblloggood (logsheetid, logitemid, logticketid, palletnum, tickettype, letter, gticketnum, remarks, status) "
                            sql = sql & " values ('" & lbllogid.Text & "', '" & txtlogitemid.Text & "', '" & txtlogticket.Text & "', '" & txtpallet.Text & "',"
                            sql = sql & " '" & txttype.Text & "', null, null, '', '1')"
                            command.CommandText = sql
                            command.ExecuteNonQuery()
                        End If
                    Else
                        Dim valticket As Integer = Val(item)
                        Dim letticket As String = item.ToString.Substring(item.ToString.Length - 1)

                        sql = "Select g.gticketnum from tblloggood g "
                        sql = sql & " Right outer join tbllogticket t on t.logticketid=g.logticketid"
                        sql = sql & " Right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                        sql = sql & " where s.logsheetyear = CAST(Year(GetDate()) As nvarchar(50)) And s.logsheetid='" & lbllogid.Text & "' and g.logitemid='" & txtlogitemid.Text & "'"
                        sql = sql & " And s.branch='" & login.branch & "' and g.logticketid='" & txtlogticket.Text & "' and g.palletnum='" & txtpallet.Text & "' "
                        sql = sql & " And g.tickettype='" & txttype.Text & "' and g.letter='" & letticket & "' and g.gticketnum='" & valticket & "' and g.status='1'"
                        command.CommandText = sql
                        dr = command.ExecuteReader
                        If dr.Read Then

                        Else
                            dr.Dispose()
                            sql = "Insert into tblloggood (logsheetid, logitemid, logticketid, palletnum, tickettype, letter, gticketnum, remarks, status) "
                            sql = sql & " values ('" & lbllogid.Text & "', '" & txtlogitemid.Text & "', '" & txtlogticket.Text & "', '" & txtpallet.Text & "',"
                            sql = sql & " '" & txttype.Text & "', '" & letticket & "', '" & valticket & "', '', '1')"
                            command.CommandText = sql
                            command.ExecuteNonQuery()
                        End If
                        dr.Dispose()
                    End If
                Next

                'insert sa tbldouble yung mga tickets
                For Each row As DataGridViewRow In grddouble.Rows 'list6 are the list of double tickets
                    Dim col0 As String = grddouble.Rows(row.Index).Cells(0).Value
                    '/Dim col1 As Date = Format(grddouble.Rows(row.Index).Cells(1).Value, "yyyy/MM/dd HH:mm")
                    Dim col2 As String = grddouble.Rows(row.Index).Cells(2).Value
                    Dim col3 As String = grddouble.Rows(row.Index).Cells(3).Value

                    sql = "Insert into tbllogdouble (logsheetid, logitemid, logticketid, palletnum, tickettype, letter, dticketnum, dticketdate, remarks, status)"
                    sql = sql & " values ('" & lbllogid.Text & "', '" & txtlogitemid.Text & "', '" & txtlogticket.Text & "', '" & txtpallet.Text & "',"
                    sql = sql & " '" & txttype.Text & "', '" & col3 & "', '" & col0 & "', '" & Format(grddouble.Rows(row.Index).Cells(1).Value, "yyyy/MM/dd HH:mm") & "', '" & col2 & "', '1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                'update add to location datetime
                sql = "Update tbllogticket set remarks='" & Trim(txtrems.Text) & "', addtoloc=GetDate(), location='" & Trim(cmbloc.Text) & "', columns='" & CInt(numcol.Value) & "',"
                sql = sql & " bags='" & txtbags.Text & "', palletnum='" & txtpallet.Text & "', letter1='" & txtletter1.Text & "', astart='" & txtastart.Text & "',"
                sql = sql & " letter2='" & txtletter2.Text & "', aend='" & txtaend.Text & "', letter3='" & txtlet1.Text & "', fstart='" & txtfstart.Text & "', letter4='" & txtlet2.Text & "', fend='" & txtfend.Text & "',"
                sql = sql & " gseries='" & lblseries.Text & "', cseries='" & lblcancel.Text & "', whsechecker='" & txtchecker.Text & "', forklift='" & cmbfork.Text & "', datemodified=GetDate(), modifiedby='" & login.user & "',"
                sql = sql & " binnum=(Select binnum from tbllogsheet where logsheetnum='" & lbltemp.Text & txtsearch.Text & "')"
                sql = sql & " where logitemid='" & txtlogitemid.Text & "' and logticketid='" & txtlogticket.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()


                'update final range to in tbllogrange //////////////////////////////////////////
                Dim ftot As Integer = (Val(txtfend.Text) - Val(lblrfrom.Text)) + 1
                sql = "Update tbllogrange set frto='" & Val(txtfend.Text) & "', ftotal='" & ftot & "', status='1' where logsheetid='" & lbllogid.Text & "' and status<>'3'"
                command.CommandText = sql
                command.ExecuteNonQuery()
                '////////////////////////////////////////////////////////////////////////////////


                'drop tbltempgood//////////////////////////////////////////////////////////
                Dim tbltempgood As String = "tbltempgood" & txtlogticket.Text
                Dim tblexistgood As Boolean = False
                sql = "Select name from sys.tables where name = '" & tbltempgood & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistgood = True
                End If
                dr.Dispose()

                If tblexistgood = True Then
                    sql = "DROP Table " & tbltempgood & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If
                'good//////////////////////////////////////////////////////////////////////

                'drop tbltempcancel///////////////////////////////////////////////////////
                Dim tbltempcancel As String = "tbltempcancel" & txtlogticket.Text
                Dim tblexistcancel As Boolean = False
                sql = "Select name from sys.tables where name = '" & tbltempcancel & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistcancel = True
                End If
                dr.Dispose()

                If tblexistcancel = True Then
                    sql = "DROP Table " & tbltempcancel & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If
                'cancel/////////////////////////////////////////////////////////////////////////


                'drop tbltempdouble///////////////////////////////////////////////////////
                Dim tbltempdouble As String = "tbltempdouble" & txtlogticket.Text
                Dim tblexistdouble As Boolean = False
                sql = "Select name from sys.tables where name = '" & tbltempdouble & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistdouble = True
                End If
                dr.Dispose()

                If tblexistdouble = True Then
                    sql = "DROP Table " & tbltempdouble & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If
                'double/////////////////////////////////////////////////////////////////////////


                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                'MsgBox("Successfully added.", MsgBoxStyle.Information, "")

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
                '/End If

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.ToString, MsgBoxStyle.Exclamation, "")
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
                sql = "Select logticketid from tbllogticket where logitemid='" & lblbin.Text & "' and addtoloc is NULL and tbllogticket.status<>'3'"
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
                '/rptbintag.lognum = lbltemp.Text & Trim(txtsearch.Text)
                '/rptbintag.palletid = txtlogticket.Text
                '/rptbintag.palletloc = Trim(cmbloc.Text)
                '/rptbintag.logticket = txtlogticket.Text
                '/rptbintag.logline = txtlogitemid.Text
                '/rptbintag.ShowDialog()
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
            '/txtastart.BackColor = Color.White
            '/btnsetstart.Image = LogSheet.My.Resources.Resources.cancel
            '/btnsetstart.Tag = 1
        Else
            '/txtastart.BackColor = Color.NavajoWhite
            '/btnsetstart.Image = LogSheet.My.Resources.Resources.ok
            '/btnsetstart.Tag = 0
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
            If lblwhse.Text = "BRAN WHSE" Then
                btnloc.Enabled = True
            End If
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
                    sql = "Select * from tbllogticket where logitemid='" & lblbin.Text & "' and tbllogticket.status<>'3' order by location, columns"
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

                                tempcol = ""
                            Else
                                'same location ibang item
                            End If

                            If dr("location").ToString = temploc Then
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
                        sql = "Select status from tbllogitem where logitemid='" & lblbin.Text & "'"
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
                btncnlpallet.Enabled = True
            Else
                btnprintbin.Enabled = False
                btncnlpallet.Enabled = False
            End If

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            '/MsgBox(ex.ToString, MsgBoxStyle.Information)
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
            'check if valid yung location
            sql = "Select * from tbllocation where whsename='" & lblwhse.Text & "' and location='" & Trim(cmbloc.Text) & "' and branch='" & login.branch & "' and status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                cmbloc.Text = dr("location")
            Else
                MsgBox("Invalid location.", MsgBoxStyle.Exclamation, "")
                cmbloc.Text = ""
                cmbloc.Focus()
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            txtnextcol.Text = ""
            numcol.Enabled = True

            'check tbllogticket kung available ba yung location
            'if nakuha na ung lahat ng items sya check anu ung latest nya
            Dim tempcol As Integer = 0, ctr As Integer = 1
            sql = "Select TOP 4 t.columns from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
            sql = sql & " where t.status='1' and t.location='" & Trim(cmbloc.Text) & "' and s.branch='" & login.branch & "' order by t.logticketid DESC"
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
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If ctr = 4 Then
                If numcol.Maximum < tempcol + 1 Then
                    '/MsgBox("Location " & cmbloc.Text & " is already full")
                    '/cmbloc.Text = ""
                    '/numcol.Value = 1
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

            txtnextcol.Text = numcol.Value

            'check yung max column ng location
            sql = "Select * from tbllocation where status='1' and location='" & Trim(cmbloc.Text) & "' and branch='" & login.branch & "'"
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

            numcol.Enabled = False
        Else
            numcol.Enabled = False
        End If
    End Sub

    Private Sub numcol_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numcol.KeyPress
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            numcol.Enabled = False
        End If
    End Sub

    Private Sub numcol_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles numcol.Leave
        numcol.Enabled = False
    End Sub

    Private Sub numcol_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numcol.ValueChanged
        If Trim(txtnextcol.Text) <> "" Then
            If numcol.Value > Val(txtnextcol.Text) Then
                '/MsgBox("Cannot skip column #.", MsgBoxStyle.Exclamation, "")
                If Val(txtnextcol.Text) - 1 = 0 Then
                    '/numcol.Value = 1
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
                If chkwait.Checked = False Then
                    txtgross.Enabled = False
                End If
            Else
                If cmbreason.Text.Contains("Weight") = True Then
                    txtgross.Enabled = True
                    checkweyt()
                    txtgross.Focus()
                Else
                    checkweyt()
                    If chkwait.Checked = False Then
                        txtgross.Enabled = False
                    End If
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
            If chkwait.Checked = False Then
                txtgross.Enabled = False
            End If
        Else
            If cmbreason.Text.Contains("Weight") = True Then
                txtgross.Enabled = True
                checkweyt()
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
            sql = "Select t.logticketid from tbllogsheet s right outer join tbllogticket t on s.logsheetid=t.logsheetid"
            sql = sql & " where s.branch='" & login.branch & "' and s.logsheetnum='" & lbltemp.Text & txtsearch.Text & "'"
            sql = sql & " and (t.addtoloc is NULL or t.qadispo='0' or t.qadispo='3') and t.status<>'3'"
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
                    sql = "Update tbllogsheet set status='2', remarks='" & trems & "', datemodified=GetDate(), modifiedby='" & login.user & "' where logsheetnum='" & lbltemp.Text & txtsearch.Text & "' and branch='" & login.branch & "'"
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
        ElseIf (Asc(e.KeyChar) >= 48 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 46 Then

        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnadd.PerformClick()
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub btnprintlog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprintlog.Click
        Try
            sql = "Select logticketid from tbllogticket where logsheetid='" & lbllogid.Text & "' and (qadispo='0' or qadispo='3') and status<>'3'"
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

                'check if logsheet is not cancelled
                sql = "Select * from tbllogsheet where logsheetid='" & lbllogid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Logsheet is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Logsheet is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("allitems") = 1 Then
                        MsgBox("Logsheet is already cutoff.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                'check if there is pending pallet
                'check if may pending sa tbllogticket yung wala pang addloc
                Dim checkpending As Boolean = False
                Dim pandingpallettagnum As String = ""
                sql = "Select addtoloc,palletnum from tbllogticket where logsheetid='" & lbllogid.Text & "' and status<>'3'"
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

                viewpallet.Label4.Visible = True
                viewpallet.txtletter1.Visible = True
                viewpallet.txtastart.Visible = True
                viewpallet.Label5.Visible = True
                viewpallet.txtletter2.Visible = True
                viewpallet.txtaend.Visible = True
                viewpallet.Label15.Visible = True
                viewpallet.txtlet1.Visible = True
                viewpallet.txtfstart.Visible = True
                viewpallet.Label14.Visible = True
                viewpallet.txtlet2.Visible = True
                viewpallet.txtfend.Visible = True

                viewpallet.lblline.Text = lbllistline.Text
                viewpallet.txtitem.Text = TreeView2.Nodes(0).Text
                viewpallet.txtlogitemid.Text = lblbin.Text
                viewpallet.txtbags.Text = lblwhse.Tag
                viewpallet.lblbags.Text = lblwhse.Tag
                '///////
                viewpallet.txtrange.Text = txtrange.Text
                '///////

                ExecuteViewPallet(strconn, tn.Tag)

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

    Private Sub ExecuteViewPallet(ByVal connectionString As String, ByVal tn As Integer)
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
                'view pallet
                sql = "Select * from tbllogticket where logticketid='" & tn & "' and status='1'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    viewpallet.txtchecker.Text = dr("whsechecker")
                    viewpallet.txtpallet.Text = dr("palletnum")
                    viewpallet.txtlogticket.Text = tn
                    viewpallet.cmbloc.Text = dr("location")
                    viewpallet.txtnumcol.Text = dr("columns")
                    viewpallet.cmbfork.Text = dr("forklift")
                    viewpallet.txtastart.Text = dr("astart")
                    viewpallet.txtaend.Text = dr("aend")
                    viewpallet.txtfstart.Text = dr("fstart")
                    viewpallet.txtfend.Text = dr("fend")
                    viewpallet.lblcancel.Text = dr("cseries")
                    viewpallet.lblseries.Text = dr("gseries")
                    viewpallet.txtbin.Text = dr("binnum").ToString
                    viewpallet.txtbags.Text = dr("bags")

                    viewpallet.txtletter1.Text = dr("letter1")
                    viewpallet.txtletter2.Text = dr("letter2")
                    viewpallet.txtlet1.Text = dr("letter3")
                    viewpallet.txtlet2.Text = dr("letter4")

                    'view datetime start(datecreated) and end(addtoloc)
                    viewpallet.lblstart.Text = Format(dr("datecreated"), "yyyy/MM/dd HH:mm")
                    viewpallet.lblfinish.Text = Format(dr("addtoloc"), "yyyy/MM/dd HH:mm")
                    viewpallet.txtrems.Text = dr("remarks")
                End If
                dr.Dispose()


                viewpallet.grdcancel.Rows.Clear()
                sql = "Select * from tbllogcancel where logticketid='" & tn & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                While dr.Read
                    viewpallet.grdcancel.Rows.Add(dr("cticketnum"), Format(dr("cticketdate"), "HH:mm"), dr("remarks"), dr("grossw"))
                End While
                dr.Dispose()

                viewpallet.grddouble.Rows.Clear()
                sql = "Select * from tbllogdouble where logticketid='" & tn & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                While dr.Read
                    viewpallet.grddouble.Rows.Add(dr("dticketnum"), Format(dr("dticketdate"), "HH:mm"), dr("remarks"))
                End While
                dr.Dispose()

                'view ticket type
                sql = "Select * from tbllogsheet where logsheetid='" & lbllogid.Text & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    viewpallet.txttype.Text = dr("printtype").ToString
                End If
                dr.Dispose()

                If viewpallet.txttype.Text <> "Inkjet" And viewpallet.txttype.Text <> "Receive" And viewpallet.txttype.Text <> "Bran" Then
                    'view flour type
                    sql = "Select tickettype from tblitems where itemname='" & TreeView2.Nodes(0).Text & "'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    If dr.Read Then
                        viewpallet.txttype.Text = dr("tickettype").ToString
                    End If
                    dr.Dispose()
                End If

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default

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
        If lblwhse.Text = "BRAN WHSE" Then
            btnloc.Enabled = True
        End If
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
                sql = "Select * from tblusers where workgroup='Forklift Operator' and username='" & Trim(cmbfork.Text.ToUpper) & "' and status='1' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    cmbfork.Text = Trim(cmbfork.Text)
                Else
                    '/MsgBox("Invalid Forklift Operator name.", MsgBoxStyle.Critical, "")
                    '/cmbfork.Text = ""
                    '/cmbfork.Focus()
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
        If btnsetstart.Tag = 0 Then
            If Val(txtastart.Text) <> 0 Then
                Me.Cursor = Cursors.WaitCursor

                If Val(txtastart.Text) = Val(lblrto.Text) Then
                    '/MsgBox("Add ticket series.", MsgBoxStyle.Exclamation, "")
                    '/Exit Sub
                End If

                contastart = validateastart()
                If contastart = False Then
                    Exit Sub
                End If

                kulang = ifkulangticket()
                If kulang = True Then
                    Exit Sub
                End If

                'final start
                If list1.Items.Count <> 0 Then
                    If Val(txtastart.Text) = Val(list1.Items(0)) Then
                        txtfstart.Text = Val(txtastart.Text) + 1
                    End If
                End If

                If chkwait.Checked = False Then
                    txtgross.Enabled = False
                End If
                btnadd.Enabled = True
                btngenerate.Enabled = True
                btnloc.Enabled = True
                btngenerate.PerformClick()

                '/txtastart.ReadOnly = True
                txtastart.BackColor = Color.White
                btnsetstart.Image = My.Resources.cancel
                btnsetstart.Tag = 1

                If txtastart.Text <> lblastart.Text Then
                    MsgBox("Add remarks.", MsgBoxStyle.Exclamation, "")
                    txtrems.Focus()
                End If

                Me.Cursor = Cursors.Default
            Else
                txtaend.Text = ""
                txtfstart.Text = ""
                txtfend.Text = ""
            End If
        Else
            lblastart.Text = Trim(txtastart.Text)
            '/txtastart.ReadOnly = False
            txtastart.BackColor = Color.NavajoWhite
            txtastart.Focus()
            txtastart.SelectAll()
            btnsetstart.Image = LogSheet.My.Resources.Resources.ok
            btnsetstart.Tag = 0
        End If
    End Sub

    Public Function ifkulangticket()
        'pag dulo tapos walang second range tapos kulang yung ticket based sa numbags dapat msgbox add ticket range
        If txtrange.Text.ToString.Contains(";") = False Then
            If (Val(lblrto.Text) - Val(txtfstart.Text)) + 1 < Val(txtbags.Text) Then
                MsgBox("Tickets left are not enough. Must add another ticket range.", MsgBoxStyle.Exclamation, "")
                viewinitialticket()
                txtastart.Focus()
                kulang = True
            Else
                kulang = False
            End If
        End If

        Return kulang
    End Function

    Public Function validateastart()
        Try
            contastart = False

            Dim temp As String = ""
            If Val(Trim(txtastart.Text)) < 1000000 Then
                For vv As Integer = 1 To 6 - txtastart.Text.ToString.Length
                    temp += "0"
                Next
            End If

            'check if already existing in tblloggood and tbllogcancel
            If txttype.Text = "Inkjet" Or txttype.Text = "Receive" Then
                sql = "Select s.logsheetyear, s.whsename, s.palletizer, g.logticketid, g.tickettype, g.letter, g.gticketnum, g.status"
                sql = sql & " From tbllogsheet s inner Join tblloggood g on s.logsheetid=g.logsheetid"
                sql = sql & " where s.branch='" & login.branch & "' and s.logsheetyear=Year(Getdate()) and s.whsename='" & lblwhse.Text & "' and s.palletizer='" & lblline.Text & "'"
                sql = sql & " And (g.palletnum<>'' and g.tickettype='" & txttype.Text & "' and g.letter='" & txtletter1.Text & "' and g.gticketnum='" & Trim(txtastart.Text) & "' and g.status<>'3')"

                sql = sql & " UNION "

                sql = sql & " Select s.logsheetyear, s.whsename, s.palletizer, c.logticketid, c.tickettype, c.letter, c.cticketnum, c.status"
                sql = sql & " From tbllogsheet s inner Join tbllogcancel c on s.logsheetid = c.logsheetid"
                sql = sql & " Where s.branch ='" & login.branch & "' and s.logsheetyear=Year(Getdate()) and s.whsename='" & lblwhse.Text & "' and s.palletizer='" & lblline.Text & "'"
                sql = sql & " And (c.palletnum <>'' and c.tickettype='" & txttype.Text & "' and c.letter='" & txtletter1.Text & "' and c.cticketnum='" & Trim(txtastart.Text) & "' and c.status<>'3')"

                sql = sql & " UNION "

                sql = sql & " Select s.logsheetyear, s.whsename, s.palletizer, d.logticketid, d.tickettype, d.letter, d.dticketnum, d.status"
                sql = sql & " From tbllogsheet s inner join tbllogdouble d on s.logsheetid = d.logsheetid"
                sql = sql & " where s.branch='" & login.branch & "' and s.logsheetyear=Year(Getdate()) and s.whsename='" & lblwhse.Text & "' and s.palletizer='" & lblline.Text & "'"
                sql = sql & " And (d.palletnum<>'' and d.tickettype='" & txttype.Text & "' and d.letter='" & txtletter1.Text & "' and d.dticketnum='" & Trim(txtastart.Text) & "' and d.status<>'3')"

            Else
                'no whsename at palletizer dahil pang kalahatan ito branch lang

                sql = "Select g.logticketid"
                sql = sql & " From tbllogsheet s inner Join tblloggood g on s.logsheetid=g.logsheetid"
                sql = sql & " where s.branch='" & login.branch & "' and s.logsheetyear=Year(Getdate())"
                sql = sql & " And (g.palletnum<>'' and g.tickettype='" & txttype.Text & "' and g.letter='" & txtletter1.Text & "' and g.gticketnum='" & Trim(txtastart.Text) & "' and g.status<>'3')"

                sql = sql & " UNION "

                sql = sql & " Select c.logticketid"
                sql = sql & " From tbllogsheet s inner Join tbllogcancel c on s.logsheetid = c.logsheetid"
                sql = sql & " Where s.branch ='" & login.branch & "' and s.logsheetyear=Year(Getdate())"
                sql = sql & " And (c.palletnum <>'' and c.tickettype='" & txttype.Text & "' and c.letter='" & txtletter1.Text & "' and c.cticketnum='" & Trim(txtastart.Text) & "' and c.status<>'3')"

                sql = sql & " UNION "

                sql = sql & " Select d.logticketid"
                sql = sql & " From tbllogsheet s inner join tbllogdouble d on s.logsheetid = d.logsheetid"
                sql = sql & " where s.branch='" & login.branch & "' and s.logsheetyear=Year(Getdate())"
                sql = sql & " And (d.palletnum<>'' and d.tickettype='" & txttype.Text & "' and d.letter='" & txtletter1.Text & "' and d.dticketnum='" & Trim(txtastart.Text) & "' and d.status<>'3')"

            End If

            connect()
            cmd = New SqlCommand(sql, conn)
            cmd.CommandTimeout = 0
            dr = cmd.ExecuteReader
            If dr.Read Then
                Me.Cursor = Cursors.Default
                If dr("logticketid").ToString <> Trim(txtlogticket.Text) Then
                    MsgBox("Ticket # " & txtletter1.Text & " " & temp & Trim(txtastart.Text) & " is already exist.", MsgBoxStyle.Exclamation, "")
                    '/viewinitialticket()
                    txtastart.Focus()
                    lblseries.Text = ""
                    Return contastart
                    Exit Function
                End If
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            '/MsgBox("check if within the range yung nasa initial start ticket")
            'check if nasa list7
            Dim nasalist7range As Boolean = False
            For Each item As Object In list7.Items
                Dim itemlet As String = item
                itemlet = itemlet.Substring(itemlet.Length - 1)
                If Val(item) = Val(txtastart.Text) And itemlet = txtletter1.Text Then
                    nasalist7range = True
                    Exit For
                End If
            Next

            If nasalist7range = True Then
                'proceed
                contastart = True
            Else
                MsgBox("Ticket # " & txtletter1.Text & " " & temp & Trim(txtastart.Text) & " is not within the ticket range.", MsgBoxStyle.Exclamation, "")
                viewinitialticket()
                txtastart.Focus()
                lblseries.Text = ""
            End If

            Return contastart

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
    End Function

    Private Sub grddouble_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grddouble.SelectionChanged
        grddouble.Select()
        grdcancel.ClearSelection()
    End Sub

    Private Sub grdcancel_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdcancel.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then

                grdcancel.ClearSelection()
                grdcancel.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True

                selectedrow = e.RowIndex
                If grdcancel.Rows(selectedrow).Cells(0).ErrorText <> "" Then
                    Me.ContextMenuStrip2.Show(Cursor.Position)
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

    Private Sub grdcancel_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdcancel.SelectionChanged
        grdcancel.Select()
        grddouble.ClearSelection()
    End Sub

    Private Sub grdcancel_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcancel.CellContentClick

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If (login.depart = "All" Or login.depart = "Production" Or login.depart = "Admin Dispatching") Then
            ticketchoose.ShowDialog()
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
            sql = "Select t.logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
            sql = sql & " where t.palletnum='" & tn.Text.Substring(2, tn.Text.Length - 2) & "' and s.branch='" & login.branch & "' and t.status<>'3'"
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

            'check if logsheet is not cancelled
            sql = "Select * from tbllogsheet where logsheetid='" & lbllogid.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                If dr("status") = 3 Then
                    MsgBox("Logsheet is already cancelled.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                ElseIf dr("status") = 2 Then
                    MsgBox("Logsheet is already completed.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                ElseIf dr("allitems") = 1 Then
                    MsgBox("Logsheet is already cutoff.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            'check if all items are complete
            sql = "Select itemname from tbllogitem where (status='1' or status='0') and logsheetid='" & lbllogid.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MsgBox(dr("itemname") & " is still pending. Complete it first.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            ticketcnf = False
            confirmsave.GroupBox1.Text = login.wgroup
            confirmsave.ShowDialog()
            If ticketcnf = True Then
                sql = "Update tbllogsheet set allitems='1' where logsheetnum='" & lbltemp.Text & txtsearch.Text & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()

                MsgBox("Shift " & lblshift.Text & " successfully cut off.", MsgBoxStyle.Information, "")
                'refresh list ng location
                defaultform()
                lognumsearch()
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

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetRangeToolStripButton2.Click
        If txtsearch.Text = "" Then
            MsgBox("Cannot set range. Search Ticket Log Sheet # first.", MsgBoxStyle.Exclamation, "")
        Else
            If login.depart <> "Production" And login.depart <> "Admin Dispatching" And login.depart <> "All" Then
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
            Else
                If lblwhse.Text = "BRAN WHSE" Then
                    MsgBox("Cannot set ticket range for BRAN WHSE.", MsgBoxStyle.Exclamation, "")
                Else
                    tsetrange.lbllognum.Text = lbltemp.Text & txtsearch.Text
                    tsetrange.lbllognum.Tag = lbllogid.Text
                    tsetrange.lblline.Text = lblline.Text
                    tsetrange.ShowDialog()
                End If
            End If
        End If
    End Sub

    Private Sub btncol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncol.Click
        ticketcnf = False
        confirmsave.GroupBox1.Text = login.wgroup
        confirmsave.ShowDialog()
        If ticketcnf = True Then
            numcol.Enabled = True
            numcol.Focus()
        End If
    End Sub

    Private Sub LastTicketToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LastTicketToolStripMenuItem.Click
        If grdcancel.Rows(grdcancel.CurrentRow.Index).Cells(0).ErrorText <> "" Then
            'check if ito ung kasunod ng fend
            If Val(txtfend.Text) + 1 = Val(grdcancel.Rows(grdcancel.CurrentRow.Index).Cells(0).Value) Then
                'authentication ng whse supervisor
                ticketcnf = False
                confirm.GroupBox1.Text = login.wgroup
                confirm.ShowDialog()
                If ticketcnf = True Then
                    txtfend.Text = Val(grdcancel.Rows(grdcancel.CurrentRow.Index).Cells(0).Value)
                    grdcancel.Rows(grdcancel.CurrentRow.Index).Cells(0).ErrorText = ""
                    grdcancel.Rows(grdcancel.CurrentRow.Index).Cells(5).Value = 1
                    list9.Items.Add(Val(grdcancel.Rows(grdcancel.CurrentRow.Index).Cells(0).Value) & " " & grdcancel.Rows(grdcancel.CurrentRow.Index).Cells(4).Value)
                End If
            Else
                MsgBox("Invalid last ticket.", MsgBoxStyle.Exclamation, "")
            End If
        End If
    End Sub

    Private Sub btncnlpallet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncnlpallet.Click
        If login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" And login.depart <> "All" Then
            MsgBox("Access denied.", MsgBoxStyle.Critical, "")
            Exit Sub
        End If

        'check if logsheet is not cancelled
        sql = "Select * from tbllogsheet where logsheetid='" & lbllogid.Text & "'"
        connect()
        cmd = New SqlCommand(sql, conn)
        dr = cmd.ExecuteReader
        If dr.Read Then
            If dr("status") = 3 Then
                MsgBox("Logsheet is already cancelled.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            ElseIf dr("status") = 2 Then
                MsgBox("Logsheet is already completed.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            ElseIf dr("allitems") = 1 Then
                MsgBox("Logsheet is already cutoff.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If
        End If
        dr.Dispose()
        cmd.Dispose()
        conn.Close()

        If TreeView2.Nodes.Count <> 0 Then
            ticketcancel.lbllognum.Text = lbltemp.Text & Trim(txtsearch.Text)
            ticketcancel.lbllognum.Tag = lbllogid.Text
            ticketcancel.lblline.Text = lbllistline.Text
            ticketcancel.lblitem.Text = TreeView2.Nodes(0).Text
            ticketcancel.lblitemid.Text = lblbin.Text
            ticketcancel.ShowDialog()
            viewlocation()
            TreeView2.Nodes(0).ExpandAll()
            TreeView2.SelectedNode = TreeView2.Nodes(0)
            panelticket.Enabled = False
        Else
            MsgBox("View location first.", MsgBoxStyle.Exclamation, "")
        End If

    End Sub

    Private Sub txtcancel_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcancel.TextChanged

    End Sub

    Private Sub txtsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        Dim str As String
        str = txtsearch.Text
        If str.Length > 2 Then
            Dim answer As String
            answer = str.Substring(0, 2)
            If answer = "L." Then
                str = str.Substring(2, str.Length - 2)
                txtsearch.Text = str
                txtsearch.Select(txtsearch.Text.Length, 0)
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        '/If txtgross.Focused = True Then
        If Val(readtxt1()) = 0 Then
            txtgross.Text = ""
            timercount = weyttime
        Else
            txttimer.Text = timercount

            If timercount = 0 Then
                If equalw() = True Then
                    stoptimer1()
                Else
                    txtg.Text = txtgross.Text
                    timercount = weyttime
                End If
            Else
                timercount -= 1
            End If

            txtgross.Text = readtxt1()
            If txtg.Text = "" And timercount <> 0 Then
                txtg.Text = txtgross.Text
            End If
        End If

        If Val(txtgross.Text) < minval And txtgross.Text <> "" Then
            cmbreason.Text = "Under Weight"
        ElseIf Val(txtgross.Text) > maxval And txtgross.Text <> "" Then
            cmbreason.Text = "Over Weight"
        Else
            cmbreason.Text = ""
        End If
        '/End If
    End Sub

    Private Sub stoptimer1()
        Timer1.Stop()
        txtg.Text = ""
        '/MsgBox("Weight " & txtgross.Text)
        txtgross.BackColor = Color.FromArgb(255, 255, 192)
    End Sub

    Private Function equalw() As Boolean
        eqw = False
        If Val(txtgross.Text) <> 0 And Val(txtg.Text) <> 0 Then
            If txtgross.Text = txtg.Text Then
                eqw = True
            End If
        End If

        Return eqw
    End Function

    Public Function readtxt1() As String
        Dim lines = System.IO.File.ReadAllLines("RS232Capture.txt")
        str = Trim(lines(lines.Length - 1))

        Return str
    End Function

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        txtgross.Text = ""
        txtg.Text = ""
        Timer1.Start()
        txtgross.BackColor = Color.White
        timercount = weyttime
    End Sub

    Private Sub txtgross_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtgross.TextChanged
        '/If Trim(txtgross.Text) = "" Then
        '/Timer1.Start()
        '/Else
        '/Timer1.Stop()
        '/End If
    End Sub

    Private Sub checkweyt()
        If chkwait.Checked = False Then
            txtgross.Text = ""
        End If
    End Sub

    Private Sub cmbreason_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbreason.SelectedIndexChanged

    End Sub

    Private Sub chkwait_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkwait.CheckedChanged

    End Sub

    Private Sub chkwait_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkwait.Click
        'admin.. and whse sup lang ang pwede mag alis
        If login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" And login.depart <> "All" Then
            MsgBox("Access denied.", MsgBoxStyle.Critical, "")
            Exit Sub
        ElseIf login.wgroup <> "Administrator" And login.wgroup <> "Manager" And login.wgroup <> "Supervisor" Then
            MsgBox("Access denied.", MsgBoxStyle.Critical, "")
            Exit Sub
        End If

        ticketcnf = False
        confirmsave.GroupBox1.Text = login.wgroup
        confirmsave.ShowDialog()
        If ticketcnf = True Then
            If chkwait.Checked = True Then
                chkwait.Checked = True
                txtgross.ReadOnly = True
                btnreset.Enabled = True
                txtgross.Text = ""
                txtg.Text = ""
                Timer1.Start()
                txtgross.BackColor = Color.White
                timercount = weyttime

                sql = "Update tbllogsheet set weyt='1' where logsheetid='" & lbllogid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
            Else
                chkwait.Checked = False
                txtgross.ReadOnly = False
                btnreset.Enabled = False
                txtgross.Text = ""
                txtg.Text = ""
                Timer1.Stop()
                txtgross.BackColor = Color.White
                timercount = weyttime

                sql = "Update tbllogsheet set weyt='0' where logsheetid='" & lbllogid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
            End If
        Else
            If chkwait.Checked = True Then
                chkwait.Checked = False
            Else
                chkwait.Checked = True
            End If
        End If
    End Sub

    Private Sub ToolStripButton2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        'MsgBox(remaining(lbltemp.Text & txtsearch.Text))
    End Sub
End Class
