Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class shift
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim stat As String
    Public shiftcnf As Boolean = False

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
            If Trim(txtshift.Text) = "" Then
                If Trim(txtcode.Text) = "" Then
                    grdshift.Rows.Clear()
                    MsgBox("Input shift code first.", MsgBoxStyle.Exclamation, "")
                    txtcode.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                Else
                    grdshift.Rows.Clear()
                    sql = "Select * from tblshift where shiftcode like '" & Trim(txtcode.Text) & "%'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If dr("status") = 1 Then
                            stat = "Active"
                        Else
                            stat = "Deactivated"
                        End If
                        grdshift.Rows.Add(dr("shiftid"), dr("shiftcode"), dr("shift"), dr("description"), stat)
                        txtcode.Text = ""
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If

                grdshift.Rows.Clear()
                MsgBox("Input shift code first.", MsgBoxStyle.Exclamation, "")
                txtshift.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            Else
                grdshift.Rows.Clear()
                sql = "Select * from tblshift where shift like '" & Trim(txtshift.Text) & "%'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If dr("status") = 1 Then
                        stat = "Active"
                    Else
                        stat = "Deactivated"
                    End If
                    grdshift.Rows.Add(dr("shiftid"), dr("shiftcode"), dr("shift"), dr("description"), stat)
                    txtshift.Text = ""
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            End If


            If grdshift.Rows.Count = 0 Then
                MsgBox("Cannot found " & Trim(txtshift.Text), MsgBoxStyle.Critical, "")
                txtshift.Text = ""
                txtshift.Focus()
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

            If Trim(txtshift.Text) <> "" And Trim(txtcode.Text) <> "" And Trim(txtdes.Text) <> "" Then
                sql = "Select * from tblshift where shiftcode='" & Trim(txtcode.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox(Trim(txtcode.Text) & " is already exist", MsgBoxStyle.Information, "")
                    btnupdate.Text = "&Update"
                    txtcode.Text = ""
                    txtcode.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                sql = "Select * from tblshift where shift='" & Trim(txtshift.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox(Trim(txtshift.Text) & " is already exist", MsgBoxStyle.Information, "")
                    btnupdate.Text = "&Update"
                    txtshift.Text = ""
                    txtshift.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                shiftcnf = False
                confirm.GroupBox1.Text = login.wgroup
                confirm.ShowDialog()
                If shiftcnf = True Then
                    sql = "Insert into tblshift (shiftcode, shift, description, datecreated, createdby, datemodified, modifiedby, status) values('" & Trim(txtcode.Text) & "','" & Trim(txtshift.Text) & "','" & Trim(txtdes.Text) & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully Added", MsgBoxStyle.Information, "")
                    btnview.PerformClick()
                End If

                txtcode.Text = ""
                txtshift.Text = ""
                txtdes.Text = ""
                txtshift.Focus()
                shiftcnf = False
            Else
                MsgBox("Complete the required fields", MsgBoxStyle.Exclamation, "")
                txtshift.Focus()
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
            grdshift.Rows.Clear()
            Dim stat As String = ""

            sql = "Select * from tblshift"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("status") = 1 Then
                    stat = "Active"
                Else
                    stat = "Deactivated"
                End If
                grdshift.Rows.Add(dr("shiftid"), dr("shiftcode"), dr("shift"), dr("description"), stat)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grdshift.Rows.Count = 0 Then
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

    Private Sub txtshift_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtshift.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtshift_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtshift.Leave
        txtshift.Text = StrConv(txtshift.Text, VbStrConv.ProperCase)
    End Sub

    Private Sub txtshift_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtshift.TextChanged
        If Trim(txtshift.Text) <> "" Then
            btncancel.Enabled = True
        ElseIf Trim(txtshift.Text) = "" And btnupdate.Text = "&Save" Then
            btncancel.Enabled = True
        Else
            btncancel.Enabled = False
        End If

        Dim charactersDisallowed As String = "'"
        Dim theText As String = txtshift.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtshift.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtshift.Text.Length - 1
            Letter = txtshift.Text.Substring(x, 1)
            If charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtshift.Text = theText
        txtshift.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub shift_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            mdiform.Show()
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub shift_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnview.PerformClick()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        txtcode.Text = ""
        txtshift.Text = ""
        txtdes.Text = ""
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

            If grdshift.SelectedRows.Count = 1 Or grdshift.SelectedCells.Count = 1 Then
                If btnupdate.Text = "&Update" Then
                    If grdshift.Rows(grdshift.CurrentRow.Index).Cells(4).Value = "Deactivated" Then
                        MsgBox("Cannot update deactivated shift.", MsgBoxStyle.Exclamation, "")
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    txtcode.Text = grdshift.Rows(grdshift.CurrentRow.Index).Cells(1).Value
                    lblcat.Text = grdshift.Rows(grdshift.CurrentRow.Index).Cells(1).Value
                    txtshift.Text = grdshift.Rows(grdshift.CurrentRow.Index).Cells(2).Value
                    txtdes.Text = grdshift.Rows(grdshift.CurrentRow.Index).Cells(3).Value
                    lblid.Text = grdshift.Rows(grdshift.CurrentRow.Index).Cells(0).Value
                    btnsearch.Enabled = False
                    btnadd.Enabled = False
                    btnupdate.Text = "&Save"
                    btncancel.Enabled = True
                    btndeactivate.Enabled = False
                Else
                    'update
                    If Trim(txtcode.Text) = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("shift code should not be empty.", MsgBoxStyle.Exclamation, "")
                        txtcode.Focus()
                        Exit Sub
                    End If

                    If Trim(txtshift.Text) = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("shift name should not be empty.", MsgBoxStyle.Exclamation, "")
                        txtshift.Focus()
                        Exit Sub
                    End If

                    If Trim(txtdes.Text) = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Description should not be empty.", MsgBoxStyle.Exclamation, "")
                        txtdes.Focus()
                        Exit Sub
                    End If

                    sql = "Select * from tblshift where shiftcode='" & Trim(txtcode.Text) & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Me.Cursor = Cursors.Default
                        If dr("shiftid").ToString <> Trim(lblid.Text) Then
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

                    sql = "Select * from tblshift where shift='" & Trim(txtshift.Text) & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Me.Cursor = Cursors.Default
                        If dr("shiftid").ToString <> Trim(lblid.Text) Then
                            Me.Cursor = Cursors.Default
                            MsgBox(Trim(txtshift.Text) & " is already exist.", MsgBoxStyle.Information, "")
                            txtshift.Text = ""
                            txtshift.Focus()
                            Exit Sub
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    shiftcnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If shiftcnf = True Then
                        sql = "Update tblshift set shiftcode='" & Trim(txtcode.Text) & "', shift='" & Trim(txtshift.Text) & "', description='" & Trim(txtdes.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where shiftid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        sql = "Update tblcoa set shift='" & Trim(txtcode.Text) & "' where shift='" & lblcat.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        sql = "Update tbllogin set shift='" & Trim(txtcode.Text) & "' where shift='" & lblcat.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        sql = "Update tbllogsheet set shift='" & Trim(txtcode.Text) & "' where shift='" & lblcat.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        sql = "Update tblorderfill set shift='" & Trim(txtcode.Text) & "' where shift='" & lblcat.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        sql = "Update tblsched set shift='" & Trim(txtcode.Text) & "' where shift='" & lblcat.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    btnupdate.Text = "&Update"
                    btnsearch.Enabled = True
                    btnadd.Enabled = True
                    btndeactivate.Enabled = True
                    btncancel.Enabled = False
                    txtdes.Text = ""
                    txtcode.Text = ""
                    txtshift.Text = ""
                    txtshift.Focus()
                    shiftcnf = False
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

            If grdshift.SelectedRows.Count = 1 Or grdshift.SelectedCells.Count = 1 Then
                lblid.Text = grdshift.Rows(grdshift.CurrentRow.Index).Cells(0).Value
                If btndeactivate.Text = "&Deactivate" Then
                    'check if theres item available status
                    '/sql = "Select * from tbllogsheet where shift='" & grdshift.Rows(grdshift.CurrentRow.Index).Cells(2).Value & "'"
                    '/connect()
                    '/cmd = New SqlCommand(sql, conn)
                    '/dr = cmd.ExecuteReader
                    '/If dr.Read Then
                    '/MsgBox("Cannot deactivate. shift is still in use.", MsgBoxStyle.Exclamation, "")
                    '/Exit Sub
                    '/End If
                    '/dr.Dispose()
                    '/cmd.Dispose()

                    shiftcnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If shiftcnf = True Then
                        sql = "Update tblshift set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where shiftid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    shiftcnf = False
                Else
                    shiftcnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If shiftcnf = True Then
                        sql = "Update tblshift set status='1', datemodified=GetDate(), modifiedby='" & login.user & "' where shiftid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    shiftcnf = False
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

    Private Sub grdshift_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdshift.SelectionChanged
        If grdshift.Rows(grdshift.CurrentRow.Index).Cells(4).Value = "Active" Then
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