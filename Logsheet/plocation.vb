Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class plocation
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim stat As String
    Public loccnf As Boolean = False

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
            If Trim(txtloc.Text) = "" And cmbwhse.SelectedItem = "" Then
                grdloc.Columns.Clear()
                MsgBox("Input pallet location or select warehouse.", MsgBoxStyle.Exclamation, "")
                txtloc.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim table = New DataTable()
            table.Columns.Add("locationid", GetType(Integer))
            table.Columns.Add("whsename", GetType(String))
            table.Columns.Add("location", GetType(String))

            table.Columns.Add("max", GetType(Integer))
            table.Columns.Add("maxpallet", GetType(Integer))
            table.Columns.Add("status", GetType(String))

            table.Columns.Add("letter", GetType(Char))
            table.Columns.Add("number", GetType(Integer))

            Dim stat As String = ""

            If Trim(txtloc.Text) <> "" And cmbwhse.SelectedItem = "" Then
                sql = "Select * from tbllocation where location like '" & Trim(txtloc.Text) & "%' order by whsename,location"
            ElseIf Trim(txtloc.Text) = "" And cmbwhse.SelectedItem <> "" Then
                sql = "Select * from tbllocation where whsename ='" & cmbwhse.SelectedItem & "' order by whsename,location"
            ElseIf Trim(txtloc.Text) <> "" And cmbwhse.SelectedItem <> "" Then
                sql = "Select * from tbllocation where location like '" & Trim(txtloc.Text) & "%' and whsename ='" & cmbwhse.SelectedItem & "' order by whsename,location"
            End If

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("status") = 1 Then
                    stat = "Active"
                Else
                    stat = "Deactivated"
                End If
                table.Rows.Add(dr("locationid"), dr("whsename"), dr("location"), dr("max"), dr("maxpallet"), stat, dr("location").ToString.Substring(dr("location").ToString.Length - 1), Val(dr("location").ToString))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            table.DefaultView.Sort = "whsename, letter, number"

            grdloc.Columns.Clear()
            Me.grdloc.DataSource = table
            grdloc.Columns(0).HeaderText = "ID"
            grdloc.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            grdloc.Columns(1).HeaderText = "Warehouse"
            grdloc.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            grdloc.Columns(2).HeaderText = "Pallet Location"
            grdloc.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            grdloc.Columns(3).HeaderText = "Max no. of Columns"
            grdloc.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            grdloc.Columns(4).HeaderText = "Max no. of Pallets"
            grdloc.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
            grdloc.Columns(5).HeaderText = "Status"
            grdloc.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable

            grdloc.Columns(6).Visible = False
            grdloc.Columns(7).Visible = False

            If grdloc.Rows.Count = 0 Then
                btnupdate.Enabled = False
            Else
                btnupdate.Enabled = True
            End If

            If grdloc.Rows.Count = 0 Then
                MsgBox("Cannot found " & Trim(txtloc.Text), MsgBoxStyle.Critical, "")
                txtloc.Text = ""
                txtloc.Focus()
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

            'check if complete details
            If cmbwhse.SelectedItem = "" Or Trim(txtloc.Text) = "" Then
                Me.Cursor = Cursors.Default
                MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            If Trim(txtloc.Text) <> "" Then
                sql = "Select * from tbllocation where whsename='" & cmbwhse.SelectedItem & "' and location='" & Trim(txtloc.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    Me.Cursor = Cursors.Default
                    If dr("locationid").ToString <> Trim(lblid.Text) Then
                        Me.Cursor = Cursors.Default
                        MsgBox(Trim(txtloc.Text) & " is already exist in " & cmbwhse.SelectedItem & ".", MsgBoxStyle.Information, "")
                        btnupdate.Text = "&Update"
                        txtloc.Text = ""
                        txtloc.Focus()
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()



                loccnf = False
                confirm.GroupBox1.Text = login.wgroup
                confirm.ShowDialog()
                If loccnf = True Then
                    sql = "Insert into tbllocation (whsename, location, max, maxpallet, datecreated, createdby, datemodified, modifiedby, status, branch) values('" & cmbwhse.SelectedItem & "','" & Trim(txtloc.Text) & "','" & CInt(numcol.Value) & "','" & CInt(numrow.Value) & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1',(Select branch from tblwhse where whsename='" & cmbwhse.SelectedItem & "' and status='1'))"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully Added", MsgBoxStyle.Information, "")
                    btnview.PerformClick()
                End If

                txtloc.Text = ""
                txtloc.Focus()
                loccnf = False
            Else
                MsgBox("Input pallet location first", MsgBoxStyle.Exclamation, "")
                txtloc.Focus()
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
            viewwhse()

            Dim table = New DataTable()
            table.Columns.Add("locationid", GetType(Integer))
            table.Columns.Add("whsename", GetType(String))
            table.Columns.Add("location", GetType(String))

            table.Columns.Add("max", GetType(Integer))
            table.Columns.Add("maxpallet", GetType(Integer))
            table.Columns.Add("status", GetType(String))

            table.Columns.Add("letter", GetType(Char))
            table.Columns.Add("number", GetType(Integer))

            Dim stat As String = ""

            sql = "Select * from tbllocation where branch='" & login.branch & "' order by whsename, location"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("status") = 1 Then
                    stat = "Active"
                Else
                    stat = "Deactivated"
                End If
                table.Rows.Add(dr("locationid"), dr("whsename"), dr("location"), dr("max"), dr("maxpallet"), stat, dr("location").ToString.Substring(dr("location").ToString.Length - 1), Val(dr("location").ToString))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            table.DefaultView.Sort = "whsename, letter, number"

            grdloc.Columns.Clear()
            Me.grdloc.DataSource = table
            grdloc.Columns(0).HeaderText = "ID"
            grdloc.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            grdloc.Columns(1).HeaderText = "Warehouse"
            grdloc.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            grdloc.Columns(2).HeaderText = "Pallet Location"
            grdloc.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            grdloc.Columns(3).HeaderText = "Max no. of Columns"
            grdloc.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            grdloc.Columns(4).HeaderText = "Max no. of Pallets"
            grdloc.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
            grdloc.Columns(5).HeaderText = "Status"
            grdloc.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable

            grdloc.Columns(6).Visible = False
            grdloc.Columns(7).Visible = False

            If grdloc.Rows.Count = 0 Then
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
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub txtloc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtloc.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtwgroup_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtloc.Leave
        txtloc.Text = StrConv(txtloc.Text, VbStrConv.ProperCase)
    End Sub

    Private Sub txtwgroup_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtloc.TextChanged
        If Trim(txtloc.Text) <> "" Then
            btncancel.Enabled = True
        ElseIf Trim(txtloc.Text) = "" And btnupdate.Text = "&Save" Then
            btncancel.Enabled = True
        Else
            btncancel.Enabled = False
        End If

        Dim charactersDisallowed As String = "'"
        Dim theText As String = txtloc.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtloc.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtloc.Text.Length - 1
            Letter = txtloc.Text.Substring(x, 1)
            If charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtloc.Text = theText
        txtloc.Select(SelectionIndex - Change, 0)
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

    Public Sub viewwhse()
        Try
            cmbwhse.Items.Clear()

            sql = "Select * from tblwhse where branch='" & login.branch & "' and status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbwhse.Items.Add(dr("whsename"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

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
        txtloc.Text = ""
        numcol.Value = 1
        numrow.Value = 1
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


            If grdloc.SelectedRows.Count = 1 Or grdloc.SelectedCells.Count = 1 Then
                If btnupdate.Text = "&Update" Then
                    If grdloc.Rows(grdloc.CurrentRow.Index).Cells(5).Value = "Deactivated" Then
                        MsgBox("Cannot update deactivated pallet location.", MsgBoxStyle.Exclamation, "")
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    cmbwhse.SelectedItem = grdloc.Rows(grdloc.CurrentRow.Index).Cells(1).Value
                    lblcat.Text = grdloc.Rows(grdloc.CurrentRow.Index).Cells(2).Value
                    txtloc.Text = grdloc.Rows(grdloc.CurrentRow.Index).Cells(2).Value
                    lblid.Text = grdloc.Rows(grdloc.CurrentRow.Index).Cells(0).Value
                    numcol.Value = grdloc.Rows(grdloc.CurrentRow.Index).Cells(3).Value
                    numrow.Value = grdloc.Rows(grdloc.CurrentRow.Index).Cells(4).Value
                    btnsearch.Enabled = False
                    btnadd.Enabled = False
                    btnupdate.Text = "&Save"
                    btncancel.Enabled = True
                    btndeactivate.Enabled = False
                Else
                    'update
                    If Trim(txtloc.Text) = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Pallet location should not be empty.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    If cmbwhse.SelectedItem = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Select warehouse.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    sql = "Select * from tbllocation where whsename='" & cmbwhse.SelectedItem & "' and location='" & Trim(txtloc.Text) & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Me.Cursor = Cursors.Default
                        If dr("locationid").ToString <> Trim(lblid.Text) Then
                            Me.Cursor = Cursors.Default
                            MsgBox(Trim(txtloc.Text) & " is already exist in " & cmbwhse.SelectedItem & ".", MsgBoxStyle.Information, "")
                            txtloc.Text = ""
                            txtloc.Focus()
                            Exit Sub
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    loccnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If loccnf = True Then
                        sql = "Update tbllocation set whsename='" & cmbwhse.SelectedItem & "', location='" & Trim(txtloc.Text) & "', max='" & CInt(numcol.Value) & "', maxpallet='" & CInt(numrow.Value) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where locationid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        'lblcat
                        'tbllogticket
                        sql = "Update tbllogticket set location='" & Trim(txtloc.Text) & "' where location='" & lblcat.Text & "'"
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
                    txtloc.Text = ""
                    txtloc.Focus()
                    loccnf = False
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

            If grdloc.SelectedRows.Count = 1 Or grdloc.SelectedCells.Count = 1 Then
                lblid.Text = grdloc.Rows(grdloc.CurrentRow.Index).Cells(0).Value
                If btndeactivate.Text = "&Deactivate" Then
                    'check if theres item available status
                    '/sql = "Select * from tblgeneral where location='" & grdwgroup.Rows(grdwgroup.CurrentRow.Index).Cells(2).Value & "'"
                    '/connect()
                    '/cmd = New SqlCommand(sql, conn)
                    '/dr = cmd.ExecuteReader
                    '/If dr.Read Then
                    '/MsgBox("Cannot deactivate. location is still in use.", MsgBoxStyle.Exclamation, "")
                    '/Exit Sub
                    '/End If
                    '/dr.Dispose()
                    '/cmd.Dispose()

                    loccnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If loccnf = True Then
                        sql = "Update tbllocation set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where locationid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    loccnf = False
                Else
                    loccnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If loccnf = True Then
                        sql = "Update tbllocation set status='1', datemodified=GetDate(), modifiedby='" & login.user & "' where locationid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    loccnf = False
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

    Private Sub grdwgroup_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdloc.SelectionChanged
        If grdloc.Rows(grdloc.CurrentRow.Index).Cells(5).Value = "Active" Then
            btndeactivate.Text = "&Deactivate"
        Else
            btndeactivate.Text = "A&ctivate"
        End If
    End Sub

    Private Sub grdloc_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdloc.CellContentClick

    End Sub
End Class