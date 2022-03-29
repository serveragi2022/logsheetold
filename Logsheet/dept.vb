Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class dept
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim stat As String
    Public deptcnf As Boolean = False

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
            If Trim(txtdept.Text) = "" Then
                grddept.Rows.Clear()
                MsgBox("Input department name first.", MsgBoxStyle.Exclamation, "")
                txtdept.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            grddept.Rows.Clear()
            sql = "Select * from tbldepartment where department like '" & Trim(txtdept.Text) & "%' order by department"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("status") = 1 Then
                    stat = "Active"
                Else
                    stat = "Deactivated"
                End If
                grddept.Rows.Add(dr("deptid"), dr("department"), stat)
                txtdept.Text = ""
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grddept.Rows.Count = 0 Then
                MsgBox("Cannot found " & Trim(txtdept.Text), MsgBoxStyle.Critical, "")
                txtdept.Text = ""
                txtdept.Focus()
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
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" Then
                Me.Cursor = Cursors.Default
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If Trim(txtdept.Text) <> "" Then
                sql = "Select * from tbldepartment where department='" & Trim(txtdept.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox(Trim(txtdept.Text) & " is already exist", MsgBoxStyle.Information, "")
                    btnupdate.Text = "&Update"
                    txtdept.Text = ""
                    txtdept.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                deptcnf = False
                confirm.GroupBox1.Text = login.wgroup
                confirm.ShowDialog()
                If deptcnf = True Then
                    sql = "Insert into tbldepartment (department, datecreated, createdby, datemodified, modifiedby, status) values('" & Trim(txtdept.Text) & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully Added", MsgBoxStyle.Information, "")
                    btnview.PerformClick()
                End If

                txtdept.Text = ""
                txtdept.Focus()
                deptcnf = False
            Else
                MsgBox("Input department name first", MsgBoxStyle.Exclamation, "")
                txtdept.Focus()
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
            grddept.Rows.Clear()
            Dim stat As String = ""


            sql = "Select * from tbldepartment order by department"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("status") = 1 Then
                    stat = "Active"
                Else
                    stat = "Deactivated"
                End If
                grddept.Rows.Add(dr("deptid"), dr("department"), stat)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grddept.Rows.Count = 0 Then
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

    Private Sub txtdept_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdept.Leave
        '/txtdept.Text = StrConv(txtdept.Text, VbStrConv.ProperCase)
    End Sub

    Private Sub txtdept_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdept.TextChanged
        If Trim(txtdept.Text) <> "" Then
            btncancel.Enabled = True
        ElseIf Trim(txtdept.Text) = "" And btnupdate.Text = "&Save" Then
            btncancel.Enabled = True
        Else
            btncancel.Enabled = False
        End If

        Dim charactersDisallowed As String = "'"
        Dim theText As String = txtdept.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtdept.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtdept.Text.Length - 1
            Letter = txtdept.Text.Substring(x, 1)
            If charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtdept.Text = theText
        txtdept.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub wgroup_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            mdiform.Show()
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub wgroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnview.PerformClick()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        txtdept.Text = ""
        btnupdate.Text = "&Update"
        btnsearch.Enabled = True
        btndeactivate.Enabled = True
        btnadd.Enabled = True
        btncancel.Enabled = False
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" Then
                Me.Cursor = Cursors.Default
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grddept.SelectedRows.Count = 1 Or grddept.SelectedCells.Count = 1 Then
                If btnupdate.Text = "&Update" Then
                    If grddept.Rows(grddept.CurrentRow.Index).Cells(2).Value = "Deactivated" Then
                        MsgBox("Cannot update deactivated department.", MsgBoxStyle.Exclamation, "")
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    lblcat.Text = grddept.Rows(grddept.CurrentRow.Index).Cells(1).Value
                    txtdept.Text = grddept.Rows(grddept.CurrentRow.Index).Cells(1).Value
                    lblid.Text = grddept.Rows(grddept.CurrentRow.Index).Cells(0).Value
                    btnsearch.Enabled = False
                    btnadd.Enabled = False
                    btnupdate.Text = "&Save"
                    btncancel.Enabled = True
                    btndeactivate.Enabled = False
                Else
                    'update
                    If Trim(txtdept.Text) = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Department name should not be empty.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    sql = "Select * from tbldepartment where department='" & Trim(txtdept.Text) & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Me.Cursor = Cursors.Default
                        If dr("deptid").ToString <> Trim(lblid.Text) Then
                            Me.Cursor = Cursors.Default
                            MsgBox(Trim(txtdept.Text) & " is already exist.", MsgBoxStyle.Information, "")
                            txtdept.Text = ""
                            txtdept.Focus()
                            Exit Sub
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    deptcnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If deptcnf = True Then
                        sql = "Update tbldepartment set department='" & Trim(txtdept.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where deptid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        '/sql = "Update tblsched set department='" & Trim(txtdept.Text) & "' where department='" & lblcat.Text & "'"
                        '/connect()
                        '/cmd = New SqlCommand(sql, conn)
                        '/cmd.ExecuteNonQuery()
                        '/cmd.Dispose()
                        '/conn.Close()

                        sql = "Update tblwgroup set department='" & Trim(txtdept.Text) & "' where department='" & lblcat.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        sql = "Update tblusers set department='" & Trim(txtdept.Text) & "' where department='" & lblcat.Text & "'"
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
                    txtdept.Text = ""
                    txtdept.Focus()
                    deptcnf = False
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
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" Then
                Me.Cursor = Cursors.Default
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grddept.SelectedRows.Count = 1 Or grddept.SelectedCells.Count = 1 Then
                lblid.Text = grddept.Rows(grddept.CurrentRow.Index).Cells(0).Value
                If btndeactivate.Text = "&Deactivate" Then
                    '/MsgBox("check if theres user active status in dis department before ma deactivate ang dept")
                    sql = "Select * from tblusers where department='" & grddept.Rows(grddept.CurrentRow.Index).Cells(1).Value & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MsgBox("Cannot deactivate. department is still in use.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()

                    deptcnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If deptcnf = True Then
                        sql = "Update tbldepartment set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where deptid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    deptcnf = False
                Else
                    deptcnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If deptcnf = True Then
                        sql = "Update tbldepartment set status='1', datemodified=GetDate(), modifiedby='" & login.user & "' where deptid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    deptcnf = False
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

    Private Sub grdwgroup_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grddept.SelectionChanged
        If grddept.Rows(grddept.CurrentRow.Index).Cells(2).Value = "Active" Then
            btndeactivate.Text = "&Deactivate"
        Else
            btndeactivate.Text = "A&ctivate"
        End If
    End Sub
End Class