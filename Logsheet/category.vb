Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class category
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim stat As String
    Public catg As Boolean '= False

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

    Private Sub category_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub category_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close Category Form?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            mdiform.Show()
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub category_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnview.PerformClick()
        grdcat.Columns(0).ReadOnly = True
        grdcat.Columns(1).ReadOnly = True
        grdcat.Columns(2).ReadOnly = True
    End Sub

    Private Sub btnview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnview.Click
        Try
            grdcat.Rows.Clear()
            sql = "Select * from tblcat order by category"
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
                grdcat.Rows.Add(dr("catid"), dr("category"), stat)
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
            If Trim(txtcat.Text) = "" Then
                grdcat.Rows.Clear()
                MsgBox("Input Category first.", MsgBoxStyle.Exclamation, "")
                txtcat.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            grdcat.Rows.Clear()
            sql = "Select * from tblcat where category like '" & Trim(txtcat.Text) & "%'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("status") = 1 Then
                    stat = "Active"
                Else
                    stat = "Deactivated"
                End If
                grdcat.Rows.Add(dr("catid"), dr("category"), stat)
                txtcat.Text = ""
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grdcat.Rows.Count = 0 Then
                MsgBox("Cannot found " & Trim(txtcat.Text), MsgBoxStyle.Critical, "")
                txtcat.Text = ""
                txtcat.Focus()
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

    Private Sub txtcat_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcat.KeyPress
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtcat_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcat.Leave
        'txtcat.Text = StrConv(txtcat.Text, VbStrConv.ProperCase)
    End Sub

    Private Sub txtcat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcat.TextChanged
        Dim charactersDisallowed As String = "'"
        Dim theText As String = txtcat.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtcat.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtcat.Text.Length - 1
            Letter = txtcat.Text.Substring(x, 1)
            If charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtcat.Text = theText
        txtcat.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If Trim(txtcat.Text) <> "" Then
                sql = "Select * from tblcat where category='" & Trim(txtcat.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox(Trim(txtcat.Text) & " is already exist", MsgBoxStyle.Information, "")
                    btnupdate.Text = "&Update"
                    txtcat.Text = ""
                    txtcat.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                catg = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If catg = True Then
                    sql = "Insert into tblcat (category, datecreated, createdby, datemodified, modifiedby, status) values('" & Trim(txtcat.Text) & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                    btnview.PerformClick()
                End If

                txtcat.Text = ""
                txtcat.Focus()
                catg = False
            Else
                MsgBox("Input category first", MsgBoxStyle.Exclamation, "")
                txtcat.Focus()
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

            If grdcat.SelectedRows.Count = 1 Or grdcat.SelectedCells.Count = 1 Then
                If btnupdate.Text = "&Update" Then
                    If grdcat.Rows(grdcat.CurrentRow.Index).Cells(2).Value = "Deactivated" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Cannot update deactivated category.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                    lblcat.Text = grdcat.Rows(grdcat.CurrentRow.Index).Cells(1).Value
                    txtcat.Text = grdcat.Rows(grdcat.CurrentRow.Index).Cells(1).Value
                    lblid.Text = grdcat.Rows(grdcat.CurrentRow.Index).Cells(0).Value
                    btnsearch.Enabled = False
                    btnadd.Enabled = False
                    btndeactivate.Enabled = False
                    btnupdate.Text = "&Save"
                    btncancel.Enabled = True
                Else
                    'update
                    sql = "Select * from tblcat where category='" & Trim(txtcat.Text) & "' and catid<>'" & lblid.Text & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MsgBox(Trim(txtcat.Text) & " is already exist", MsgBoxStyle.Information, "")
                        btnupdate.Text = "&Update"
                        btnsearch.Enabled = True
                        btnadd.Enabled = True
                        btndeactivate.Enabled = True
                        btncancel.Enabled = False
                        txtcat.Text = ""
                        txtcat.Focus()
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()


                    catg = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If catg = True Then
                        sql = "Update tblcat set category='" & Trim(txtcat.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where catid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        'update tbloritems category
                        sql = "Update tblitems set category='" & Trim(txtcat.Text) & "' where category='" & lblcat.Text & "'"
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
                    txtcat.Text = ""
                    txtcat.Focus()
                    catg = False
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
            If login.wgroup <> "Administrator" And login.wgroup <> "Admin Dispatching" And login.wgroup <> "Supervisor" And login.wgroup <> "Logistics Staff" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdcat.SelectedRows.Count = 1 Or grdcat.SelectedCells.Count = 1 Then
                lblid.Text = grdcat.Rows(grdcat.CurrentRow.Index).Cells(0).Value
                If btndeactivate.Text = "&Deactivate" Then
                    'check if theres item available status
                    sql = "Select * from tblitems where category='" & grdcat.Rows(grdcat.CurrentRow.Index).Cells(1).Value & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        If dr("status") = "1" Then
                            Me.Cursor = Cursors.Default
                            MsgBox("Cannot deactivate category beacause there are items under this category that are still available in status.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()


                    catg = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If catg = True Then
                        sql = "Update tblcat set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where catid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    catg = False
                Else
                    catg = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If catg = True Then
                        sql = "Update tblcat set status='1', datemodified=GetDate(), modifiedby='" & login.user & "' where catid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    catg = False
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
        If grdcat.Rows(grdcat.CurrentRow.Index).Cells(2).Value = "Active" Then
            btndeactivate.Text = "&Deactivate"
        Else
            btndeactivate.Text = "A&ctivate"
        End If
    End Sub

    Private Sub grdcat_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdcat.SelectionChanged
        selectchnged()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        btnupdate.Text = "&Update"
        btnsearch.Enabled = True
        btnadd.Enabled = True
        btndeactivate.Enabled = True
        btncancel.Enabled = False
        txtcat.Text = ""
        txtcat.Focus()
    End Sub

    Public Sub chkrows()
        If grdcat.Rows.Count = 0 Then
            btnupdate.Enabled = False
            btndeactivate.Enabled = False
        Else
            btnupdate.Enabled = True
            btndeactivate.Enabled = True
        End If
    End Sub
End Class