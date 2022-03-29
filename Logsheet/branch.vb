Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class branch
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim stat As String
    Public brcnf As Boolean '= False

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

    Private Sub branch_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub branch_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close Branch Form?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            mdiform.Show()
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub Branch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnview.PerformClick()
        grdbr.Columns(0).ReadOnly = True
        grdbr.Columns(1).ReadOnly = True
        grdbr.Columns(2).ReadOnly = True
    End Sub

    Private Sub btnview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnview.Click
        Try
            grdbr.Rows.Clear()
            sql = "Select * from tblbranch order by branch"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Me.Cursor = Cursors.WaitCursor
                If dr("status") = 1 Then
                    stat = "Active"
                Else
                    stat = "Deactivated"
                End If
                grdbr.Rows.Add(dr("brid"), dr("branch"), stat)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.Default

            btncancel.PerformClick()
            chkrows()

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

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            If Trim(txtbr.Text) = "" Then
                grdbr.Rows.Clear()
                MsgBox("Input branch first.", MsgBoxStyle.Exclamation, "")
                txtbr.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            grdbr.Rows.Clear()
            sql = "Select * from tblbranch where branch like '" & Trim(txtbr.Text) & "%'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("status") = 1 Then
                    stat = "Active"
                Else
                    stat = "Deactivated"
                End If
                grdbr.Rows.Add(dr("brid"), dr("branch"), stat)
                txtbr.Text = ""
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grdbr.Rows.Count = 0 Then
                MsgBox("Cannot found " & Trim(txtbr.Text), MsgBoxStyle.Critical, "")
                txtbr.Text = ""
                txtbr.Focus()
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

    Private Sub txtcat_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbr.KeyPress
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtcat_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbr.Leave
        'txtcat.Text = StrConv(txtcat.Text, VbStrConv.ProperCase)
    End Sub

    Private Sub txtcat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbr.TextChanged
        Dim charactersDisallowed As String = "'"
        Dim theText As String = txtbr.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtbr.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtbr.Text.Length - 1
            Letter = txtbr.Text.Substring(x, 1)
            If charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtbr.Text = theText
        txtbr.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If Trim(txtbr.Text) <> "" Then
                sql = "Select * from tblbranch where branch='" & Trim(txtbr.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox(Trim(txtbr.Text) & " is already exist", MsgBoxStyle.Information, "")
                    btnupdate.Text = "&Update"
                    txtbr.Text = ""
                    txtbr.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                brcnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If brcnf = True Then
                    sql = "Insert into tblbranch (branch, datecreated, createdby, datemodified, modifiedby, status) values('" & Trim(txtbr.Text) & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                    btnview.PerformClick()
                End If

                txtbr.Text = ""
                txtbr.Focus()
                brcnf = False
            Else
                MsgBox("Input branch first", MsgBoxStyle.Exclamation, "")
                txtbr.Focus()
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

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            If login.wgroup <> "Administrator" And login.wgroup <> "Admin Dispatching" And login.wgroup <> "Supervisor" And login.wgroup <> "Logistics Staff" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdbr.SelectedRows.Count = 1 Or grdbr.SelectedCells.Count = 1 Then
                If btnupdate.Text = "&Update" Then
                    If grdbr.Rows(grdbr.CurrentRow.Index).Cells(2).Value = "Deactivated" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Cannot update deactivated branch.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                    lblcat.Text = grdbr.Rows(grdbr.CurrentRow.Index).Cells(1).Value
                    txtbr.Text = grdbr.Rows(grdbr.CurrentRow.Index).Cells(1).Value
                    lblid.Text = grdbr.Rows(grdbr.CurrentRow.Index).Cells(0).Value
                    btnsearch.Enabled = False
                    btnadd.Enabled = False
                    btndeactivate.Enabled = False
                    btnupdate.Text = "&Save"
                    btncancel.Enabled = True
                Else
                    'update
                    sql = "Select * from tblbranch where branch='" & Trim(txtbr.Text) & "' and brid<>'" & lblid.Text & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MsgBox(Trim(txtbr.Text) & " is already exist", MsgBoxStyle.Information, "")
                        btnupdate.Text = "&Update"
                        btnsearch.Enabled = True
                        btnadd.Enabled = True
                        btndeactivate.Enabled = True
                        btncancel.Enabled = False
                        txtbr.Text = ""
                        txtbr.Focus()
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()


                    brcnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If brcnf = True Then
                        sql = "Update tblbranch set branch='" & Trim(txtbr.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where brid='" & lblid.Text & "'"
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
                    txtbr.Text = ""
                    txtbr.Focus()
                    brcnf = False
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

    Private Sub ExecuteUpdate(ByVal connectionString As String)
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
                sql = "Update tblbranch set branch='" & Trim(txtbr.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where brid='" & lblid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tblcoa set branch='" & Trim(txtbr.Text) & "' where branch='" & lblcat.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tbllocation set branch='" & Trim(txtbr.Text) & "' where branch='" & lblcat.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tbllogin set branch='" & Trim(txtbr.Text) & "' where branch='" & lblcat.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tbllogsheet set branch='" & Trim(txtbr.Text) & "' where branch='" & lblcat.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tblofitem set branch='" & Trim(txtbr.Text) & "' where branch='" & lblcat.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tblorderfill set branch='" & Trim(txtbr.Text) & "' where branch='" & lblcat.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tblpalletizer set branch='" & Trim(txtbr.Text) & "' where branch='" & lblcat.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tblreceive set frmbranch='" & Trim(txtbr.Text) & "' where frmbranch='" & lblcat.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tblreceive set branch='" & Trim(txtbr.Text) & "' where branch='" & lblcat.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tbltransfer set branch='" & Trim(txtbr.Text) & "' where branch='" & lblcat.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tblusers set branch='" & Trim(txtbr.Text) & "' where branch='" & lblcat.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tblwhse set branch='" & Trim(txtbr.Text) & "' where branch='" & lblcat.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()


                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully Saved.", MsgBoxStyle.Information, "")

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.ToString, MsgBoxStyle.Exclamation, "")
                ' Attempt to roll back the transaction. 
                Try
                    Me.Cursor = Cursors.Default
                    transaction.Rollback()

                Catch ex2 As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox("2: " & ex2.ToString & vbCrLf & vbCrLf & "Please try again.", MsgBoxStyle.Information, "")
                End Try
            End Try
        End Using
    End Sub

    Private Sub btndeactivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeactivate.Click
        Try
            If login.wgroup <> "Administrator" And login.wgroup <> "Admin Dispatching" And login.wgroup <> "Supervisor" And login.wgroup <> "Logistics Staff" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdbr.SelectedRows.Count = 1 Or grdbr.SelectedCells.Count = 1 Then
                lblid.Text = grdbr.Rows(grdbr.CurrentRow.Index).Cells(0).Value
                If btndeactivate.Text = "&Deactivate" Then
                    brcnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If brcnf = True Then
                        sql = "Update tblbranch set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where brid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    brcnf = False
                Else
                    brcnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If brcnf = True Then
                        sql = "Update tblbranch set status='1', datemodified=GetDate(), modifiedby='" & login.user & "' where brid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    brcnf = False
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

    Public Sub selectchnged()
        If grdbr.Rows(grdbr.CurrentRow.Index).Cells(2).Value = "Active" Then
            btndeactivate.Text = "&Deactivate"
        Else
            btndeactivate.Text = "A&ctivate"
        End If
    End Sub

    Private Sub grdcat_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdbr.SelectionChanged
        selectchnged()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        btnupdate.Text = "&Update"
        btnsearch.Enabled = True
        btnadd.Enabled = True
        btndeactivate.Enabled = True
        btncancel.Enabled = False
        txtbr.Text = ""
        txtbr.Focus()
    End Sub

    Public Sub chkrows()
        If grdbr.Rows.Count = 0 Then
            btnupdate.Enabled = False
            btndeactivate.Enabled = False
        Else
            btnupdate.Enabled = True
            btndeactivate.Enabled = True
        End If
    End Sub
End Class