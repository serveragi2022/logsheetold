Imports System.IO
Imports System.Data.SqlClient

Public Class viewpallets
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public lognum As String

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

    Private Sub viewpallets_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        lognum = ""
    End Sub

    Private Sub viewpallets_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        defaultform()
        viewgeneral()
        viewforklift1()
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

    End Sub

    Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then

            TreeView1.SelectedNode = e.Node
            'pag right click dpat ma right click

            If e.Node.Tag <> "Location" And e.Node.Tag <> "Column" And e.Node.Tag <> "Parent" Then
                Me.ContextMenuStrip1.Show(Cursor.Position)
                PrintPalletTagToolStripMenuItem.Visible = True
            End If
        End If
    End Sub

    Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Left Then

                TreeView1.SelectedNode = e.Node

                If e.Node.Tag <> "Location" And e.Node.Tag <> "Column" And e.Node.Tag <> "Parent" And e.Node.Tag <> "Itemcode" Then
                    viewpalletinfo()
                End If

            Else
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

    Public Sub viewpalletinfo()
        Try
            Dim tn As TreeNode = Me.TreeView1.SelectedNode

            panelticket.Enabled = True
            defaultform()

            cmbfork.Enabled = True
            cmbloc.Enabled = True
            numcol.Enabled = True

            lblline.Text = TreeView1.Nodes(0).Text

            sql = "Select * from tbllogticket where logticketid='" & tn.Tag & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                txtlogitemid.Text = dr("logitemid")
                txtlogticket.Text = dr("palletnum")
                txtitem.Text = tn.Parent.Parent.Parent.Text
                txtchecker.Text = dr("whsechecker").ToString
                txtbags.Text = dr("bags")
                lblbags.Text = dr("bags")
                txtpallet.Text = dr("logticketid")
                cmbloc.Text = dr("location").ToString
                numcol.Value = Val(dr("columns").ToString)
                cmbfork.Text = dr("forklift").ToString
                txtletter1.Text = dr("letter1").ToString
                txtastart.Text = Val(dr("astart").ToString)
                txtletter2.Text = dr("letter2").ToString
                txtaend.Text = Val(dr("aend").ToString)
                txtlet1.Text = dr("letter3").ToString
                txtfstart.Text = Val(dr("fstart").ToString)
                txtlet2.Text = dr("letter4").ToString
                txtfend.Text = Val(dr("fend").ToString)
                lblseries.Text = dr("gseries").ToString
                txtbin.Text = dr("binnum").ToString

                'view datetime start(datecreated) and end(addtoloc)
                lblstart.Text = Format(dr("datecreated"), "yyyy/MM/dd HH:mm")
                If IsDBNull(dr("addtoloc")) = False Then
                    lblfinish.Text = Format(dr("addtoloc"), "yyyy/MM/dd HH:mm")
                End If
                txtrems.Text = dr("remarks").ToString

                'QAdisposition
                If (dr("qadispo")) = 0 Or (dr("qadispo")) = 3 Then
                    lbldispo.Text = "Pending"
                ElseIf (dr("qadispo")) = 1 Then
                    lbldispo.Text = "Ok"
                ElseIf (dr("qadispo")) = 2 Then
                    lbldispo.Text = "Hold"
                End If

                lblqa.Text = dr("qaname").ToString
                lblqadate.Text = dr("qadate").ToString
                txtqarems.Text = dr("qarems").ToString

                If dr("status") = 3 Then
                    lblseries.Text = "CANCELLED PALLET TAG"
                End If
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            cmbfork.Enabled = False
            cmbloc.Enabled = False
            numcol.Enabled = False

            'populate grdcancel
            grdcancel.Rows.Clear()
            sql = "Select * from tbllogcancel where logticketid='" & tn.Tag & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                grdcancel.Rows.Add(dr("cticketnum"), dr("cticketdate"), dr("remarks"), dr("grossw"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            grddouble.Rows.Clear()
            sql = "Select * from tbllogdouble where logticketid='" & tn.Tag & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                grddouble.Rows.Add(dr("dticketnum"), Format(dr("dticketdate"), "HH:mm"), dr("remarks"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            sql = "Select printtype from tbllogsheet where logsheetnum='" & txtlog.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                txttype.Text = dr("printtype").ToString
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If txttype.Text = "Receive" Then
                Label4.Visible = False
                txtletter1.Visible = False
                txtastart.Visible = False
                Label5.Visible = False
                txtletter2.Visible = False
                txtaend.Visible = False
                Label15.Visible = False
                txtlet1.Visible = False
                txtfstart.Visible = False
                Label14.Visible = False
                txtlet2.Visible = False
                txtfend.Visible = False
            Else
                Label4.Visible = True
                txtletter1.Visible = True
                txtastart.Visible = True
                Label5.Visible = True
                txtletter2.Visible = True
                txtaend.Visible = True
                Label15.Visible = True
                txtlet1.Visible = True
                txtfstart.Visible = True
                Label14.Visible = True
                txtlet2.Visible = True
                txtfend.Visible = True
            End If

            If txttype.Text <> "Inkjet" And txttype.Text <> "Receive" And txttype.Text <> "Bran" Then
                sql = "Select * from tblitems where itemname='" & txtitem.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    txttype.Text = dr("tickettype").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
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
        txttype.Text = ""

        grdcancel.Rows.Clear()
        grddouble.Rows.Clear()

        txtlogitemid.Text = ""
        txtlogticket.Text = ""
        txtitem.Text = ""
        txtchecker.Text = ""
        txtbags.Text = ""
        lblbags.Text = ""
        txtpallet.Text = ""
        cmbloc.Text = ""
        cmbfork.Enabled = True
        cmbfork.Text = ""
        txtletter1.Text = ""
        txtastart.Text = ""
        txtletter2.Text = ""
        txtaend.Text = ""
        txtlet1.Text = ""
        txtfstart.Text = ""
        txtlet2.Text = ""
        txtfend.Text = ""
        lblseries.Text = ""
        txtbin.Text = ""
        numcol.Minimum = 1
        numcol.Value = 1
        txtnextcol.Text = ""
        txtmaxpal.Text = ""

        lblstart.Text = ""
        lblfinish.Text = ""
        txtrems.Text = ""

        lbldispo.Text = ""
        lblqa.Text = ""
        lblqadate.Text = ""
        txtqarems.Text = ""

        panelticketfalse()
    End Sub

    Public Sub panelticketfalse()
        'cannot edit
        txtbags.ReadOnly = True
        cmbloc.Enabled = False
        cmbfork.Enabled = False
        txtastart.ReadOnly = True
        txtaend.ReadOnly = True
        txtfstart.ReadOnly = True
        txtfend.ReadOnly = True
    End Sub

    Public Sub paneltickettrue()
        'edit
        txtbags.ReadOnly = False
        cmbloc.Enabled = True
        cmbfork.Enabled = True
        txtastart.ReadOnly = False
        txtaend.ReadOnly = False
        txtfstart.ReadOnly = False
        txtfend.ReadOnly = False
    End Sub

    Public Sub viewgeneral()
        Try
            Dim temprange As String = ""
            sql = "Select r.* from tbllogrange r right outer join tbllogsheet s on s.logsheetid=r.logsheetid where s.logsheetnum='" & lognum & "' and r.status<>'3'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim temp1 As String = ""
                If dr("arfrom") < 1000000 Then
                    For vv As Integer = 1 To 6 - dr("arfrom").ToString.Length
                        temp1 += "0"
                    Next
                End If

                Dim temp2 As String = ""
                If dr("arto") < 1000000 Then
                    For vv As Integer = 1 To 6 - dr("arto").ToString.Length
                        temp2 += "0"
                    Next
                End If

                If temprange <> "" Then
                    temprange = temprange & "  ;  " & dr("letter1") & " " & temp1 & dr("arfrom") & " - " & dr("letter2") & " " & temp2 & dr("arto")
                Else
                    temprange = dr("letter1") & " " & temp1 & dr("arfrom") & " - " & dr("letter2") & " " & temp2 & dr("arto")
                End If

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

    Private Sub PrintPalletTagToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPalletTagToolStripMenuItem.Click
        Try
            Dim tn As TreeNode = Me.TreeView1.SelectedNode
            If tn.Tag <> "Location" And tn.Tag <> "Column" Then
                'print pallet tag
                Dim ok As Boolean = False
                Dim logticketid As Integer = 0
                sql = "Select t.logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                sql = sql & " where t.palletnum='" & tn.Text.Substring(2, tn.Text.Length - 2) & "' and s.branch='" & login.branch & "' and t.status<>'3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    ok = True
                    logticketid = dr("logticketid")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If ok = True Then
                    rptpallettag.logticket = logticketid
                    rptpallettag.ShowDialog()
                Else
                    MsgBox("Cannot print cancelled pallet tag.", MsgBoxStyle.Exclamation, "")
                End If
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

    Private Sub panelticket_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panelticket.Paint

    End Sub
End Class