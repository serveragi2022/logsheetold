Imports System.IO
Imports System.Data.SqlClient

Public Class orderfillitems
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public ofsetcnf As Boolean = False, ofsetby As String = "", frm As String, ofid As Integer

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

    Private Sub orderfillitems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewwhse()
        viewaddeditems()

        cmbitem.DropDownWidth = 300
    End Sub

    Private Sub viewitems()
        Try
            cmbitem.Items.Clear()
            sql = "Select itemname from tblitems where status='1'"
            If Trim(cmbwhse.Text) = "BRAN WHSE" Then
                sql = sql & " and category='By-Products'"
            Else
                sql = sql & " and category<>'By-Products'"
            End If
            sql = sql & " order by itemname"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbitem.Items.Add(dr("itemname"))
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
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub viewwhse()
        Try
            cmbwhse.Items.Clear()
            cmbwhse.Items.Add("")

            sql = "Select whsename from tblwhse where status='1' and branch='" & login.branch & "'"
            If frm = "orderfill" Then
                sql = sql & " and whsename<>'BRAN WHSE'"
            ElseIf frm = "branorderfill" Then
                sql = sql & " and whsename='BRAN WHSE'"
            End If
            sql = sql & " order by whsename"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbwhse.Items.Add(dr("whsename"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

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

    Public Sub viewaddeditems()
        Try
            grditems.Rows.Clear()
            cmbtemp.Items.Clear()

            sql = "Select * from tblofitem where ofnum='" & txtofnum.Text & "' and branch='" & login.branch & "' order by itemname"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim stat As String = ""
                If dr("status") = 0 Then
                    stat = "In Process"
                ElseIf dr("status") = 1 Then
                    stat = "Available"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If
                grditems.Rows.Add(dr("ofitemid"), dr("itemname"), dr("numbags"), stat)
                cmbtemp.Items.Add(dr("itemname"))

                If dr("status") = 3 Then
                    grditems.Rows(grditems.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
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
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub cmbitem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbitem.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub cmbitem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbitem.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/btnadd.PerformClick()
        End If
    End Sub

    Private Sub cmbitem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbitem.SelectedIndexChanged

    End Sub

    Private Sub txtbags_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtbags.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            SendKeys.Send("{TAB}")
            e.Handled = True

        ElseIf e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.A Then
            txtbags.SelectAll()
        ElseIf e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.C Then
            txtbags.Copy()
        ElseIf e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.V Then
            txtbags.Paste()
        End If
    End Sub

    Private Sub txtbags_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbags.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/btnadd.PerformClick()
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtbags_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbags.TextChanged
        Dim charactersDisallowed As String = "0123456789"
        Dim theText As String = txtbags.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtbags.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtbags.Text.Length - 1
            Letter = txtbags.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtbags.Text = theText
        txtbags.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub cmbitem_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitem.TextChanged
        Dim charactersDisallowed As String = "'"
        Dim theText As String = cmbitem.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = cmbitem.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To cmbitem.Text.Length - 1
            Letter = cmbitem.Text.Substring(x, 1)
            If charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        cmbitem.Text = theText
        cmbitem.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If Trim(cmbitem.Text) <> "" And Trim(txtbags.Text) <> "" Then
                If Val(Trim(txtbags.Text)) = 0 Then
                    MsgBox("Number of bags must not be equal to zero.", MsgBoxStyle.Exclamation, "")
                    txtbags.Text = ""
                    Exit Sub
                End If

                sql = "Select itemname from tblofitem where itemname='" & Trim(cmbitem.Text) & "' and ofnum='" & txtofnum.Text & "' and branch='" & login.branch & "' and status<>'3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox(Trim(cmbitem.Text) & " is already exist.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'add in grditem
                ofsetcnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If ofsetcnf = True Then
                    'insert into tblofitem
                    sql = "Insert into tblofitem (ofid, ofnum, wrsnum, itemname, numbags, coanum, qasampling, datecreated, createdby, datemodified, modifiedby, status, branch)"
                    sql = sql & " values ('" & ofid & "', '" & txtofnum.Text & "', '" & Trim(txtwrs.Text) & "', '" & Trim(cmbitem.Text) & "', '" & Val(Trim(txtbags.Text)) & "', '', '0', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1','" & login.branch & "')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully added.", MsgBoxStyle.Information, "")
                    viewaddeditems()
                End If


                cmbtemp.Items.Add(Trim(cmbitem.Text))
                txtbags.Text = ""
                cmbitem.Text = ""
            Else
                MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
                cmbitem.Focus()
                Exit Sub
            End If

            cmbitem.Text = ""
            cmbitem.Focus()

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

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        If btnremove.Text = "Deactivate" Then
            'check status if available status=1
            sql = "Select * from tblofitem where ofnum='" & txtofnum.Text & "' and ofitemid='" & grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value & "' and status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then

            Else
                MsgBox("Cannot deactivate item.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            ofsetcnf = False
            confirmsave.GroupBox1.Text = login.wgroup
            confirmsave.ShowDialog()
            If ofsetcnf = True Then
                'remove item
                sql = "Update tblofitem set status='3' where ofitemid='" & grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()

                MsgBox("Sucessfully deactivated.", MsgBoxStyle.Information, "")

                viewaddeditems()
            End If
        Else
            'check status if deactivated status=3
            sql = "Select * from tblofitem where ofnum='" & txtofnum.Text & "' and ofitemid='" & grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value & "' and status='3'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then

            Else
                MsgBox("Cannot activate item.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            ofsetcnf = False
            confirmsave.GroupBox1.Text = login.wgroup
            confirmsave.ShowDialog()
            If ofsetcnf = True Then
                'remove item

                sql = "Update tblofitem set status='1' where ofitemid='" & grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()

                MsgBox("Sucessfully activated.", MsgBoxStyle.Information, "")

                viewaddeditems()
            End If
        End If
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        grditems.Enabled = True
        btnupdate.Text = "Update No. of Bags"
        btnchange.Text = "Change Item"
        GroupBox1.Enabled = True
        btnadd.Enabled = True
        btnchange.Enabled = True
        btnupdate.Enabled = True
        btncnlitem.Enabled = True
        cmbitem.Enabled = True
        txtbags.Enabled = True
        cmbitem.Text = ""
        txtbags.Text = ""
        lblid.Text = ""
        lblitem.Text = ""

        If grditems.Rows(grditems.CurrentRow.Index).Cells(3).Value = "Deactivated" Then
            btnremove.Enabled = True
            btnremove.Text = "Activate"
        ElseIf grditems.Rows(grditems.CurrentRow.Index).Cells(3).Value = "Available" Then
            btnremove.Enabled = True
            btnremove.Text = "Deactivate"
        Else
            btnremove.Enabled = False
        End If
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            If grditems.SelectedCells.Count = 1 Or grditems.SelectedRows.Count = 1 Then

                'check status first kung cancelled na
                sql = "Select * from tblofitem where ofitemid='" & grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value & "' and ofid='" & ofid & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 2 Then
                        MsgBox("Item is already completed.", MsgBoxStyle.Information, "")
                        Exit Sub
                    ElseIf dr("status") = 3 Then
                        MsgBox("Item is already cancelled.", MsgBoxStyle.Information, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If btnupdate.Text = "Update No. of Bags" Then
                    If grditems.Rows.Count = 0 Then
                        MsgBox("No item selected.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    If grditems.Rows(grditems.CurrentRow.Index).Cells(3).Value = "Completed" Then
                        MsgBox("Item is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf grditems.Rows(grditems.CurrentRow.Index).Cells(3).Value = "In Process" Then
                        Dim a As String = MsgBox("Item is in process status. Do you want to update its no of bags?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                        If a = vbYes Then

                        Else
                            Exit Sub
                        End If
                    ElseIf grditems.Rows(grditems.CurrentRow.Index).Cells(3).Value = "Removed" Then
                        MsgBox("")
                    End If


                    lblid.Text = grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value
                    cmbitem.Text = grditems.Rows(grditems.CurrentRow.Index).Cells(1).Value
                    txtbags.Text = grditems.Rows(grditems.CurrentRow.Index).Cells(2).Value
                    cmbitem.Enabled = False
                    grditems.Enabled = False

                    btnupdate.Text = "Save No. of Bags"
                    btnadd.Enabled = False
                    btnchange.Enabled = False
                    GroupBox1.Enabled = False
                    btnremove.Enabled = False
                    btncnlitem.Enabled = False
                Else
                    If Trim(cmbitem.Text) <> "" And Trim(txtbags.Text) <> "" Then
                        If Val(Trim(txtbags.Text)) = 0 Then
                            MsgBox("Number of bags must not be equal to zero.", MsgBoxStyle.Exclamation, "")
                            txtbags.Text = ""
                            Exit Sub
                        End If

                        'save yung number of bags lang
                        ofsetcnf = False
                        confirmsave.GroupBox1.Text = login.wgroup
                        confirmsave.ShowDialog()
                        If ofsetcnf = True Then
                            sql = "Update tblofitem set numbags='" & Val(Trim(txtbags.Text)) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where ofitemid='" & lblid.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            MsgBox("Successfully saved.", MsgBoxStyle.Information, "")
                            viewaddeditems()
                        End If

                        lblid.Text = ""
                        cmbitem.Enabled = True
                        btnupdate.Text = "Update No. of Bags"
                        btnadd.Enabled = True
                        btnchange.Enabled = True
                        GroupBox1.Enabled = True
                        btnremove.Enabled = True
                        btncnlitem.Enabled = True
                        btncancel.PerformClick()
                    Else
                        MsgBox("Complete the fields")
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

    Private Sub btnchange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchange.Click
        'pag nag update check muna kung may na generate na si user para sa item na yun para madelete muna yun kasi ibang item nakuha yun eh
        Try
            'check status first kung cancelled na
            sql = "Select * from tblofitem where ofitemid='" & grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value & "' and ofid='" & ofid & "' and status='3'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MsgBox("Item is already cancelled.", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If btnchange.Text = "Change Item" Then
                If grditems.Rows.Count = 0 Then
                    MsgBox("No item selected.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                If grditems.Rows(grditems.CurrentRow.Index).Cells(3).Value = "Completed" Then
                    MsgBox("Item is already completed.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                ElseIf grditems.Rows(grditems.CurrentRow.Index).Cells(3).Value = "In Process" Then
                    Dim a As String = MsgBox("Item is in process status. Do you want to change it into another item?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                    If a = vbYes Then
                        'delete save tbltemp

                    Else
                        Exit Sub
                    End If
                End If


                lblid.Text = grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value
                lblitem.Text = grditems.Rows(grditems.CurrentRow.Index).Cells(1).Value
                cmbitem.Text = grditems.Rows(grditems.CurrentRow.Index).Cells(1).Value
                txtbags.Text = grditems.Rows(grditems.CurrentRow.Index).Cells(2).Value
                txtbags.Enabled = False
                grditems.Enabled = False

                btnchange.Text = "Save Change"
                btnremove.Enabled = False
                btnadd.Enabled = False
                btnupdate.Enabled = False
                GroupBox1.Enabled = False
                btncnlitem.Enabled = False
            Else
                If Trim(cmbitem.Text) <> "" And Trim(txtbags.Text) <> "" Then
                    If Val(Trim(txtbags.Text)) = 0 Then
                        MsgBox("Number of bags must not be equal to zero.", MsgBoxStyle.Exclamation, "")
                        txtbags.Text = ""
                        Exit Sub
                    End If

                    'check if may ibang ofitemid na existing na sa db
                    sql = "Select * from tblofitem where itemname='" & Trim(cmbitem.Text) & "' and ofitemid<>'" & lblid.Text & "' and ofnum='" & txtofnum.Text & "' and branch='" & login.branch & "' and status<>'3'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MsgBox(Trim(cmbitem.Text) & " is already exist.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    'save yung itemname if magkaiba tlga
                    If Trim(cmbitem.Text) = lblitem.Text Then
                        MsgBox("Same item.", MsgBoxStyle.Information, "")
                    Else
                        'delete yung mga tbltempsssss///////////////////////////
                        'drop tbltempofitem///////////////////////////////////////////////////////
                        Dim tbltempofitem As String = "tbltempofitem" & lblid.Text
                        Dim tblexistofitem As Boolean = False
                        sql = "Select * from sys.tables where name = '" & tbltempofitem & "'"
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
                            sql = "DROP Table " & tbltempofitem & ""
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()
                        End If
                        'tbltempofitem/////////////////////////////////////////////////////////////////////////

                        'drop tbltempoflogcancel///////////////////////////////////////////////////////
                        Dim tbltempoflogcancel As String = "tbltempoflogcancel" & lblid.Text
                        Dim tblexistoflogcancel As Boolean = False
                        sql = "Select * from sys.tables where name = '" & tbltempoflogcancel & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            tblexistoflogcancel = True
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()

                        If tblexistoflogcancel = True Then
                            sql = "DROP Table " & tbltempoflogcancel & ""
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()
                        End If
                        'tbltempoflogcancel/////////////////////////////////////////////////////////////////////////

                        'Update
                        ofsetcnf = False
                        confirmsave.GroupBox1.Text = login.wgroup
                        confirmsave.ShowDialog()
                        If ofsetcnf = True Then
                            sql = "Update tblofitem set itemname='" & Trim(cmbitem.Text) & "', status='1', datemodified=GetDate(), modifiedby='" & login.user & "' where ofitemid='" & lblid.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            'refreshlist ng itemname sa cmbtemp

                            MsgBox("Successfully saved.", MsgBoxStyle.Information, "")
                            viewaddeditems()
                        End If
                    End If

                    lblid.Text = ""
                    lblitem.Text = ""
                    txtbags.Enabled = True
                    btnchange.Text = "Change Item"
                    btnadd.Enabled = True
                    btnupdate.Enabled = True
                    GroupBox1.Enabled = True
                    btnremove.Enabled = True
                    btncnlitem.Enabled = True
                    btncancel.PerformClick()
                Else
                    MsgBox("Complete the fields")
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

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        If frm = "orderfill" Then
            orderfill.btnselect.PerformClick()
            orderfill.cmbitem.Text = ""
            orderfill.grdlog.Rows.Clear()
        ElseIf frm = "branorderfill" Then
            branorderfill.btnselect.PerformClick()
            branorderfill.cmbitem.Text = ""
            branorderfill.grdlog.Rows.Clear()
        End If
        Me.Dispose()
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            If GroupBox2.Enabled = True Then
                'set items
                If frm = "orderfill" Then
                    orderfill.btnselect.PerformClick()
                    orderfill.cmbitem.Text = ""
                    orderfill.grdlog.Rows.Clear()
                ElseIf frm = "branorderfill" Then
                    branorderfill.btnselect.PerformClick()
                    branorderfill.cmbitem.Text = ""
                    branorderfill.grdlog.Rows.Clear()
                End If
                Me.Dispose()
            Else
                'edit info
                'check fields
                If txtofnum.Text <> "" And Trim(txtwrs.Text) <> "" And Trim(cmbwhse.Text) <> "" And Trim(txtcus.Text) <> "" And Trim(txtref.Text) <> "" And Trim(txtplate.Text) <> "" And Trim(txtdriver.Text) <> "" Then
                    ofsetcnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If ofsetcnf = True Then

                        'Update into tblorderfill
                        sql = "Update tblorderfill set wrsnum='" & Trim(txtwrs.Text) & "', whsename='" & Trim(cmbwhse.Text) & "', customer='" & Trim(txtcus.Text) & "', cusref='" & Trim(txtref.Text) & "', platenum='" & Trim(txtplate.Text) & "', driver='" & Trim(txtdriver.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where ofnum='" & txtofnum.Text & "' and branch='" & login.branch & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully saved.", MsgBoxStyle.Information, "")
                        txtwrs.Text = ""
                        txtcus.Text = ""
                        txtref.Text = ""
                        txtplate.Text = ""
                        txtrems.Text = ""
                        '/viewofnum()
                        grditems.Rows.Clear()
                        cmbtemp.Items.Clear()

                        If frm = "orderfill" Then
                            orderfill.btnsearch.PerformClick()
                        ElseIf frm = "branorderfill" Then
                            branorderfill.btnsearch.PerformClick()
                        End If
                    End If

                Else
                    MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
            End If

            Me.Dispose()

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

    Private Sub txtwrs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwrs.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnok.PerformClick()
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtwrs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwrs.TextChanged

    End Sub

    Private Sub txtref_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtref.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnok.PerformClick()
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtref_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtref.TextChanged

    End Sub

    Private Sub txtcus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcus.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnok.PerformClick()
        End If
    End Sub

    Private Sub txtcus_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcus.TextChanged

    End Sub

    Private Sub txtplate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtplate.TextChanged
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789"
        Dim theText As String = txtplate.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtplate.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtplate.Text.Length - 1
            Letter = txtplate.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtplate.Text = theText
        txtplate.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub cmbwhse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbwhse.SelectedIndexChanged

    End Sub

    Private Sub grditems_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grditems.CellContentClick

    End Sub

    Private Sub grditems_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grditems.SelectionChanged
        If grditems.Rows(grditems.CurrentRow.Index).Cells(3).Value = "Deactivated" Or grditems.Rows(grditems.CurrentRow.Index).Cells(3).Value = "Completed" Then
            btnremove.Enabled = True
            btnremove.Text = "Activate"
        ElseIf grditems.Rows(grditems.CurrentRow.Index).Cells(3).Value = "Available" Then
            btnremove.Enabled = True
            btnremove.Text = "Deactivate"
        Else
            btnremove.Enabled = False
        End If
    End Sub

    Private Sub btncnlitem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncnlitem.Click
        Try
            If grditems.SelectedCells.Count = 1 Or grditems.SelectedRows.Count = 1 Then
                'check status first kung cancelled na
                sql = "Select * from tblofitem where ofitemid='" & grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value & "' and ofid='" & ofid & "' and status='3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Item is already cancelled.", MsgBoxStyle.Information, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                Dim a As String = MsgBox("Are you sure you want to cancel " & grditems.Rows(grditems.CurrentRow.Index).Cells(1).Value & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbYes Then
                    ofsetcnf = False
                    ofsetby = ""
                    confirmsupwhse.ShowDialog()
                    If ofsetcnf = True Then
                        ExecuteCancelItem(strconn, ofid, grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value)
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


    Private Sub ExecuteCancelItem(ByVal connectionString As String, ByVal ofidd As Integer, ByVal ofitemid As Integer)
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
                '/tblofitem()
                sql = "Update tblofitem set status='3', datemodified=GetDate(), modifiedby='" & login.user & "' where ofitemid='" & ofitemid & "' and ofid='" & ofidd & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                '/tbloflog()
                sql = "Update tbloflog set status='3' where ofitemid='" & ofitemid & "' and ofid='" & ofidd & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'gawing status=1 ulet lahat kasi pag buong pick ginagawang status=0 na kasi wala ng laman
                list1.Items.Clear()
                sql = "Select logticketid from tbloflog where ofitemid='" & ofitemid & "' and ofid='" & ofidd & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                While dr.Read
                    list1.Items.Add(dr("logticketid"))
                End While
                dr.Dispose()

                '/MsgBox("dapat pati sa temporary logs")
                'per log////////////////////////////////////////////////////////////////////////////
                Dim tbltempoflog As String = "tbltempoflog" & ofitemid
                Dim tblexistoflog As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempoflog & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistoflog = True
                End If
                dr.Dispose()

                If tblexistoflog = True Then
                    sql = "Select logticketid from " & tbltempoflog & " where ofitemid='" & ofitemid & "' and ofnum='" & txtofnum.Text & "'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    While dr.Read
                        list1.Items.Add(dr("logticketid"))
                    End While
                    dr.Dispose()
                End If


                For Each item As Object In list1.Items
                    'gawing status=1
                    sql = "Update tblloggood set status='1' where status='0' and logticketid='" & item & "' and ofid='" & ofidd & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    'gawing status=1
                    sql = "Update tbllogdouble set status='1' where status='0' and logticketid='" & item & "' and ofid='" & ofidd & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    'tanggalin ung mga selected na tbllogticket sakanya
                    sql = "Update tbllogticket set cusreserve='0', ofnum='', ofid=NULL, status='1' where status='0' and logticketid='" & item & "' and ofid='" & ofidd & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                '/tblofloggood()
                sql = "Update tblofloggood set status='3' where ofitemid='" & ofitemid & "' and ofid='" & ofidd & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                '/tbloflogcancel()
                sql = "Update tbloflogcancel set status='3' where ofitemid='" & ofitemid & "' and ofid='" & ofidd & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()


                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully cancelled", MsgBoxStyle.Information, "")
                viewaddeditems()

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

    Private Sub cmbwhse_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbwhse.SelectedValueChanged
        viewitems()
    End Sub
End Class