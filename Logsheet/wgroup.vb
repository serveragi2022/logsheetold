Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class wgroup
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim stat As String
    Public wgroupcnf As Boolean = False

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
            If Trim(txtwgroup.Text) = "" And Trim(cmbdept.Text) = "" Then
                grdwgroup.Rows.Clear()
                MsgBox("Input workgroup name or select department first.", MsgBoxStyle.Exclamation, "")
                txtwgroup.Focus()
                Me.Cursor = Cursors.Default

            ElseIf Trim(txtwgroup.Text) <> "" And Trim(cmbdept.Text) = "" Then
                grdwgroup.Rows.Clear()
                sql = "Select * from tblwgroup where workgroup like '" & Trim(txtwgroup.Text) & "%'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If dr("status") = 1 Then
                        stat = "Active"
                    Else
                        stat = "Deactivated"
                    End If
                    grdwgroup.Rows.Add(dr("wgroupid"), dr("workgroup"), dr("department"), stat)
                    txtwgroup.Text = ""
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            ElseIf Trim(txtwgroup.Text) = "" And Trim(cmbdept.Text) <> "" Then
                grdwgroup.Rows.Clear()
                sql = "Select * from tblwgroup where department like '" & Trim(cmbdept.Text) & "%'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If dr("status") = 1 Then
                        stat = "Active"
                    Else
                        stat = "Deactivated"
                    End If
                    grdwgroup.Rows.Add(dr("wgroupid"), dr("workgroup"), dr("department"), stat)
                    txtwgroup.Text = ""
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            ElseIf Trim(txtwgroup.Text) <> "" And Trim(cmbdept.Text) <> "" Then
                grdwgroup.Rows.Clear()
                sql = "Select * from tblwgroup where department like '" & Trim(cmbdept.Text) & "%' and workgroup like '" & Trim(txtwgroup.Text) & "%'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If dr("status") = 1 Then
                        stat = "Active"
                    Else
                        stat = "Deactivated"
                    End If
                    grdwgroup.Rows.Add(dr("wgroupid"), dr("workgroup"), dr("department"), stat)
                    txtwgroup.Text = ""
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            End If

            If grdwgroup.Rows.Count = 0 Then
                MsgBox("Cannot found " & Trim(txtwgroup.Text), MsgBoxStyle.Critical, "")
                txtwgroup.Text = ""
                txtwgroup.Focus()
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

            If Trim(txtwgroup.Text) <> "" And Trim(cmbdept.Text) <> "" Then
                sql = "Select * from tblwgroup where workgroup='" & Trim(txtwgroup.Text) & "' and department='" & Trim(cmbdept.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox(Trim(txtwgroup.Text) & " is already exist in " & Trim(cmbdept.Text) & " department.", MsgBoxStyle.Information, "")
                    btnupdate.Text = "&Update"
                    txtwgroup.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                wgroupcnf = False
                confirm.GroupBox1.Text = login.wgroup
                confirm.ShowDialog()
                If wgroupcnf = True Then
                    sql = "Insert into tblwgroup (workgroup, department, datecreated, createdby, datemodified, modifiedby, status) values('" & Trim(txtwgroup.Text) & "','" & Trim(cmbdept.Text) & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully Added", MsgBoxStyle.Information, "")
                    btnview.PerformClick()

                    cmbdept.Text = ""
                    txtwgroup.Text = ""
                    txtwgroup.Focus()
                    wgroupcnf = False
                End If
            Else
                MsgBox("Input workgroup name and department first.", MsgBoxStyle.Exclamation, "")
                txtwgroup.Focus()
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
            viewdept()
            grdwgroup.Rows.Clear()
            Dim stat As String = ""

            sql = "Select * from tblwgroup order by department, workgroup"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("status") = 1 Then
                    stat = "Active"
                Else
                    stat = "Deactivated"
                End If
                grdwgroup.Rows.Add(dr("wgroupid"), dr("workgroup"), dr("department"), stat)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grdwgroup.Rows.Count = 0 Then
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

    Private Sub txtwgroup_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwgroup.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtwgroup_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtwgroup.Leave
        '.txtwgroup.Text = StrConv(txtwgroup.Text, VbStrConv.ProperCase)
    End Sub

    Private Sub txtwgroup_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwgroup.TextChanged
        If Trim(txtwgroup.Text) <> "" Then
            btncancel.Enabled = True
        ElseIf Trim(txtwgroup.Text) = "" And btnupdate.Text = "&Save" Then
            btncancel.Enabled = True
        Else
            btncancel.Enabled = False
        End If

        Dim charactersDisallowed As String = "'"
        Dim theText As String = txtwgroup.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtwgroup.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtwgroup.Text.Length - 1
            Letter = txtwgroup.Text.Substring(x, 1)
            If charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtwgroup.Text = theText
        txtwgroup.Select(SelectionIndex - Change, 0)
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

    Public Sub viewdept()
        Try
            cmbdept.Items.Clear()

            sql = "Select * from tbldepartment order by department"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbdept.Items.Add(dr("department"))
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

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        cmbdept.Text = ""
        txtwgroup.Text = ""
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

            If grdwgroup.SelectedRows.Count = 1 Or grdwgroup.SelectedCells.Count = 1 Then
                If btnupdate.Text = "&Update" Then
                    If grdwgroup.Rows(grdwgroup.CurrentRow.Index).Cells(3).Value = "Deactivated" Then
                        MsgBox("Cannot update deactivated workgroup.", MsgBoxStyle.Exclamation, "")
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    lblid.Text = grdwgroup.Rows(grdwgroup.CurrentRow.Index).Cells(0).Value
                    lblcat.Text = grdwgroup.Rows(grdwgroup.CurrentRow.Index).Cells(1).Value
                    txtwgroup.Text = grdwgroup.Rows(grdwgroup.CurrentRow.Index).Cells(1).Value
                    cmbdept.Text = grdwgroup.Rows(grdwgroup.CurrentRow.Index).Cells(2).Value
                    btnsearch.Enabled = False
                    btnadd.Enabled = False
                    btnupdate.Text = "&Save"
                    btncancel.Enabled = True
                    btndeactivate.Enabled = False
                Else
                    'update
                    If Trim(txtwgroup.Text) = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Workgroup name should not be empty.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    If Trim(cmbdept.Text) = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Department should not be empty.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    sql = "Select * from tblwgroup where workgroup='" & Trim(txtwgroup.Text) & "' and department='" & Trim(cmbdept.Text) & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Me.Cursor = Cursors.Default
                        If dr("wgroupid").ToString <> Trim(lblid.Text) Then
                            Me.Cursor = Cursors.Default
                            MsgBox(Trim(txtwgroup.Text) & " is already exist in " & Trim(cmbdept.Text) & " department.", MsgBoxStyle.Information, "")
                            txtwgroup.Focus()
                            Exit Sub
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    wgroupcnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If wgroupcnf = True Then
                        sql = "Update tblwgroup set workgroup='" & Trim(txtwgroup.Text) & "', department='" & Trim(cmbdept.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where wgroupid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        sql = "Update tblusers set workgroup='" & Trim(txtwgroup.Text) & "', department='" & Trim(cmbdept.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where workgroup='" & lblcat.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()

                        btnupdate.Text = "&Update"
                        btnsearch.Enabled = True
                        btnadd.Enabled = True
                        btndeactivate.Enabled = True
                        btncancel.Enabled = False
                        cmbdept.Text = ""
                        txtwgroup.Text = ""
                        txtwgroup.Focus()
                        wgroupcnf = False
                    End If

                End If
            Else
                MsgBox("Select only one", MsgBoxStyle.Exclamation, "")
                btnview.PerformClick()
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

    Private Sub btndeactivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeactivate.Click
        Try
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" Then
                Me.Cursor = Cursors.Default
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdwgroup.SelectedRows.Count = 1 Or grdwgroup.SelectedCells.Count = 1 Then
                lblid.Text = grdwgroup.Rows(grdwgroup.CurrentRow.Index).Cells(0).Value
                If btndeactivate.Text = "&Deactivate" Then
                    wgroupcnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If wgroupcnf = True Then
                        sql = "Update tblwgroup set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where wgroupid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    wgroupcnf = False
                Else
                    wgroupcnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If wgroupcnf = True Then
                        sql = "Update tblwgroup set status='1', datemodified=GetDate(), modifiedby='" & login.user & "' where wgroupid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    wgroupcnf = False
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

    Private Sub grdwgroup_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdwgroup.SelectionChanged
        If grdwgroup.Rows(grdwgroup.CurrentRow.Index).Cells(3).Value = "Active" Then
            btndeactivate.Text = "&Deactivate"
        Else
            btndeactivate.Text = "A&ctivate"
        End If
    End Sub

    Private Sub cmbdept_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbdept.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbdept_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbdept.Leave
        Try
            If cmbdept.Items.Contains(Trim(cmbdept.Text)) = False And Trim(cmbdept.Text) <> "" Then
                MsgBox("Invalid department.", MsgBoxStyle.Critical, "")
                cmbdept.Text = ""
                cmbdept.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbdept_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdept.SelectedIndexChanged
        
    End Sub
End Class