Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class whse
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim stat As String
    Public whsecnf As Boolean = False

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

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            If Trim(txtwhse.Text) = "" Then
                If Trim(txtcode.Text) = "" Then
                    grdwhse.Rows.Clear()
                    MsgBox("Input warehouse code first.", MsgBoxStyle.Exclamation, "")
                    txtcode.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                Else
                    grdwhse.Rows.Clear()
                    sql = "Select * from tblwhse where whsecode like '" & Trim(txtcode.Text) & "%' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If dr("status") = 1 Then
                            stat = "Active"
                        Else
                            stat = "Deactivated"
                        End If
                        grdwhse.Rows.Add(dr("whseid"), dr("branch"), dr("whsecode"), dr("whsename"), dr("bags"), dr("description"), stat)
                        txtcode.Text = ""
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                    Exit Sub
                End If

                grdwhse.Rows.Clear()
                MsgBox("Input warehouse name first.", MsgBoxStyle.Exclamation, "")
                txtwhse.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            Else
                grdwhse.Rows.Clear()
                sql = "Select * from tblwhse where whsename like '" & Trim(txtwhse.Text) & "%' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If dr("status") = 1 Then
                        stat = "Active"
                    Else
                        stat = "Deactivated"
                    End If
                    grdwhse.Rows.Add(dr("whseid"), dr("branch"), dr("whsecode"), dr("whsename"), dr("bags"), dr("description"), stat)
                    txtwhse.Text = ""
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                Exit Sub
            End If


            If grdwhse.Rows.Count = 0 Then
                MsgBox("Cannot found " & Trim(txtwhse.Text), MsgBoxStyle.Critical, "")
                txtwhse.Text = ""
                txtwhse.Focus()
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

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" And login.wgroup <> "Agi Admin Staff" And login.wgroup <> "Supervisor" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If Trim(txtwhse.Text) <> "" And Trim(txtcode.Text) <> "" Then
                sql = "Select * from tblwhse where whsecode='" & Trim(txtcode.Text) & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox(Trim(txtcode.Text) & " is already exist.", MsgBoxStyle.Information, "")
                    btnupdate.Text = "&Update"
                    txtcode.Text = ""
                    txtcode.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                sql = "Select * from tblwhse where whsename='" & Trim(txtwhse.Text) & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox(Trim(txtwhse.Text) & " is already exist.", MsgBoxStyle.Information, "")
                    btnupdate.Text = "&Update"
                    txtwhse.Text = ""
                    txtwhse.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                whsecnf = False
                confirm.GroupBox1.Text = login.wgroup
                confirm.ShowDialog()
                If whsecnf = True Then
                    sql = "Insert into tblwhse (whsecode, whsename, bags, description, branch, datecreated, createdby, datemodified, modifiedby, status) values('" & Trim(txtcode.Text) & "','" & Trim(txtwhse.Text) & "','" & CInt(numbags.Value) & "','" & Trim(txtdes.Text) & "','" & login.branch & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully Added", MsgBoxStyle.Information, "")
                    btnview.PerformClick()
                End If

                txtcode.Text = ""
                txtwhse.Text = ""
                txtdes.Text = ""
                numbags.Value = 0
                txtwhse.Focus()
                whsecnf = False
            Else
                MsgBox("Complete the required fields", MsgBoxStyle.Exclamation, "")
                txtwhse.Focus()
            End If

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

    Private Sub btnview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnview.Click
        Try
            grdwhse.Rows.Clear()
            Dim stat As String = ""

            sql = "Select * from tblwhse where branch='" & login.branch & "' order by branch, whsecode"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("status") = 1 Then
                    stat = "Active"
                Else
                    stat = "Deactivated"
                End If
                grdwhse.Rows.Add(dr("whseid"), dr("branch"), dr("whsecode"), dr("whsename"), dr("bags"), dr("description"), stat)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grdwhse.Rows.Count = 0 Then
                btnupdate.Enabled = False
            Else
                btnupdate.Enabled = True
            End If

            btncancel.PerformClick()

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

    Private Sub txtwhse_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwhse.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtwhse_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtwhse.Leave
        txtwhse.Text = StrConv(txtwhse.Text, VbStrConv.ProperCase)
    End Sub

    Private Sub txtwhse_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwhse.TextChanged
        If Trim(txtwhse.Text) <> "" Then
            btncancel.Enabled = True
        ElseIf Trim(txtwhse.Text) = "" And btnupdate.Text = "&Save" Then
            btncancel.Enabled = True
        Else
            btncancel.Enabled = False
        End If

        Dim charactersDisallowed As String = "'"
        Dim theText As String = txtwhse.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtwhse.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtwhse.Text.Length - 1
            Letter = txtwhse.Text.Substring(x, 1)
            If charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtwhse.Text = theText
        txtwhse.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub whse_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            mdiform.Show()
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub whse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnview.PerformClick()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        txtdes.Text = ""
        txtcode.Text = ""
        txtwhse.Text = ""
        numbags.Value = 0
        btnupdate.Text = "&Update"
        btnsearch.Enabled = True
        btndeactivate.Enabled = True
        btnadd.Enabled = True
        btncancel.Enabled = False
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" And login.wgroup <> "Agi Admin Staff" And login.wgroup <> "Supervisor" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdwhse.SelectedRows.Count = 1 Or grdwhse.SelectedCells.Count = 1 Then
                If btnupdate.Text = "&Update" Then
                    If grdwhse.Rows(grdwhse.CurrentRow.Index).Cells(6).Value = "Deactivated" Then
                        MsgBox("Cannot update deactivated warehouse name.", MsgBoxStyle.Exclamation, "")
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    txtcode.Text = grdwhse.Rows(grdwhse.CurrentRow.Index).Cells(2).Value
                    lblcat.Text = grdwhse.Rows(grdwhse.CurrentRow.Index).Cells(3).Value
                    txtwhse.Text = grdwhse.Rows(grdwhse.CurrentRow.Index).Cells(3).Value
                    numbags.Value = grdwhse.Rows(grdwhse.CurrentRow.Index).Cells(4).Value
                    txtdes.Text = grdwhse.Rows(grdwhse.CurrentRow.Index).Cells(5).Value
                    lblid.Text = grdwhse.Rows(grdwhse.CurrentRow.Index).Cells(0).Value
                    btnsearch.Enabled = False
                    btnadd.Enabled = False
                    btnupdate.Text = "&Save"
                    btncancel.Enabled = True
                    btndeactivate.Enabled = False
                Else
                    'update
                    If Trim(txtcode.Text) = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Warehouse code should not be empty.", MsgBoxStyle.Exclamation, "")
                        txtcode.Focus()
                        Exit Sub
                    End If

                    If Trim(txtwhse.Text) = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Warehouse name should not be empty.", MsgBoxStyle.Exclamation, "")
                        txtwhse.Focus()
                        Exit Sub
                    End If

                    If Val(numbags.Value) = 0 Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Number of bags per pallet should not be empty.", MsgBoxStyle.Exclamation, "")
                        numbags.Focus()
                        Exit Sub
                    End If

                    sql = "Select * from tblwhse where whsecode='" & Trim(txtcode.Text) & "' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr("whseid").ToString <> Trim(lblid.Text) Then
                            Me.Cursor = Cursors.Default
                            MsgBox(Trim(txtcode.Text) & " is already exist.", MsgBoxStyle.Information, "")
                            txtcode.Text = ""
                            txtcode.Focus()
                            Exit Sub
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    sql = "Select * from tblwhse where whsename='" & Trim(txtwhse.Text) & "' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr("whseid").ToString <> Trim(lblid.Text) Then
                            Me.Cursor = Cursors.Default
                            MsgBox(Trim(txtwhse.Text) & " is already exist.", MsgBoxStyle.Information, "")
                            txtwhse.Text = ""
                            txtwhse.Focus()
                            Exit Sub
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    whsecnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If whsecnf = True Then
                        sql = "Update tblwhse set whsecode='" & Trim(txtcode.Text) & "', whsename='" & Trim(txtwhse.Text) & "', bags='" & CInt(numbags.Value) & "', description='" & Trim(txtdes.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where whseid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        If Trim(txtwhse.Text) <> Trim(lblcat.Text) Then
                            'update other tbl in database
                            sql = "Update tblusers set whsename='" & Trim(txtwhse.Text) & "' where whsename='" & lblcat.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            sql = "Update tblpalletizer set whsename='" & Trim(txtwhse.Text) & "' where whsename='" & lblcat.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            sql = "Update tblorderfill set whsename='" & Trim(txtwhse.Text) & "' where whsename='" & lblcat.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            sql = "Update tbllogsheet set whsename='" & Trim(txtwhse.Text) & "' where whsename='" & lblcat.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            sql = "Update tblcoa set whsename='" & Trim(txtwhse.Text) & "' where whsename='" & lblcat.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            sql = "Update tbllocation set whsename='" & Trim(txtwhse.Text) & "' where whsename='" & lblcat.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            sql = "Update tbllogrange set whsename='" & Trim(txtwhse.Text) & "' where whsename='" & lblcat.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            sql = "Update tblsched set whsename='" & Trim(txtwhse.Text) & "' where whsename='" & lblcat.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()
                        End If

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    btnupdate.Text = "&Update"
                    btnsearch.Enabled = True
                    btnadd.Enabled = True
                    btndeactivate.Enabled = True
                    btncancel.Enabled = False
                    txtdes.Text = ""
                    txtwhse.Text = ""
                    txtcode.Text = ""
                    numbags.Value = 0
                    txtwhse.Focus()
                    whsecnf = False
                End If
            Else
                MsgBox("Select only one", MsgBoxStyle.Exclamation, "")
                btnview.PerformClick()
            End If

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

    Private Sub btndeactivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeactivate.Click
        Try
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" And login.wgroup <> "Agi Admin Staff" And login.wgroup <> "Supervisor" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdwhse.SelectedRows.Count = 1 Or grdwhse.SelectedCells.Count = 1 Then
                lblid.Text = grdwhse.Rows(grdwhse.CurrentRow.Index).Cells(0).Value
                If btndeactivate.Text = "&Deactivate" Then
                    'check if theres item available status
                    '/sql = "Select * from tbllogsheet where whse='" & grdwhse.Rows(grdwhse.CurrentRow.Index).Cells(3).Value & "'"
                    '/connect()
                    '/cmd = New SqlCommand(sql, conn)
                    '/dr = cmd.ExecuteReader
                    '/If dr.Read Then
                    '/MsgBox("Cannot deactivate. whse is still in use.", MsgBoxStyle.Exclamation, "")
                    '/Exit Sub
                    '/End If
                    '/dr.Dispose()
                    '/cmd.Dispose()

                    whsecnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If whsecnf = True Then
                        sql = "Update tblwhse set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where whseid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    whsecnf = False
                Else
                    whsecnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If whsecnf = True Then
                        sql = "Update tblwhse set status='1', datemodified=GetDate(), modifiedby='" & login.user & "' where whseid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    whsecnf = False
                End If
            Else
                MsgBox("Select only one", MsgBoxStyle.Exclamation, "")
            End If

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

    Private Sub grdwhse_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdwhse.SelectionChanged
        If grdwhse.Rows(grdwhse.CurrentRow.Index).Cells(6).Value = "Active" Then
            btndeactivate.Text = "&Deactivate"
        Else
            btndeactivate.Text = "A&ctivate"
        End If
    End Sub

    Private Sub txtdes_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdes.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcode.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged
        If Trim(txtcode.Text) <> "" Then
            btncancel.Enabled = True
        ElseIf Trim(txtcode.Text) = "" And btnupdate.Text = "&Save" Then
            btncancel.Enabled = True
        Else
            btncancel.Enabled = False
        End If

        Dim charactersDisallowed As String = "'"
        Dim theText As String = txtcode.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtcode.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtcode.Text.Length - 1
            Letter = txtcode.Text.Substring(x, 1)
            If charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtcode.Text = theText
        txtcode.Select(SelectionIndex - Change, 0)
    End Sub
End Class