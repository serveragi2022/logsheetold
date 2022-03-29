Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class palletizer
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim stat As String
    Public linecnf As Boolean = False

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
            If Trim(txtcode.Text) <> "" Or Trim(txtline.Text) <> "" Or cmbwhse.SelectedItem <> "" Then
                grdline.Rows.Clear()

                sql = "Select * from tblpalletizer where branch='" & login.branch & "' and whsename='" & cmbwhse.SelectedItem & "'"

                If Trim(txtcode.Text) <> "" Then
                    sql = sql & " and palcode like '" & Trim(txtcode.Text) & "%'"
                ElseIf Trim(txtline.Text) <> "" Then
                    sql = sql & " and palletizer like '" & Trim(txtline.Text) & "%'"
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
                    grdline.Rows.Add(dr("palletizerid"), dr("palcode"), dr("palletizer"), dr("whsename"), dr("description"), stat)
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            Else
                grdline.Rows.Clear()
                MsgBox("Input Palletizer code/name first.", MsgBoxStyle.Exclamation, "")
                txtcode.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If



            If grdline.Rows.Count = 0 Then
                MsgBox("Cannot found.", MsgBoxStyle.Critical, "")
                txtline.Text = ""
                txtline.Focus()
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

            If Trim(txtline.Text) <> "" And Trim(txtcode.Text) <> "" And cmbwhse.SelectedItem <> "" Then
                sql = "Select * from tblpalletizer where palcode='" & Trim(txtcode.Text) & "' and  whsename='" & cmbwhse.SelectedItem & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox(Trim(txtcode.Text) & " is already exist in " & cmbwhse.SelectedItem & ".", MsgBoxStyle.Information, "")
                    btnupdate.Text = "&Update"
                    txtcode.Text = ""
                    txtcode.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                sql = "Select * from tblpalletizer where palletizer='" & Trim(txtline.Text) & "' and whsename='" & cmbwhse.SelectedItem & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox(Trim(txtline.Text) & " is already exist in " & cmbwhse.SelectedItem & ".", MsgBoxStyle.Information, "")
                    btnupdate.Text = "&Update"
                    txtline.Text = ""
                    txtline.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                linecnf = False
                confirm.GroupBox1.Text = login.wgroup
                confirm.ShowDialog()
                If linecnf = True Then
                    sql = "Insert into tblpalletizer (palcode, palletizer, whsename, description, branch, datecreated, createdby, datemodified, modifiedby, status) values('" & Trim(txtcode.Text) & "','" & Trim(txtline.Text) & "','" & cmbwhse.SelectedItem & "','" & Trim(txtdes.Text) & "','" & login.branch & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully Added", MsgBoxStyle.Information, "")
                    btnview.PerformClick()
                End If

                txtdes.Text = ""
                txtcode.Text = ""
                txtline.Text = ""
                txtline.Focus()
                linecnf = False
            Else
                MsgBox("Complete the required fields.", MsgBoxStyle.Exclamation, "")
                txtline.Focus()
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
            loadwhse()
            grdline.Rows.Clear()
            Dim stat As String = ""

            sql = "Select * from tblpalletizer where branch='" & login.branch & "' order by whsename, palletizer"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("status") = 1 Then
                    stat = "Active"
                Else
                    stat = "Deactivated"
                End If
                grdline.Rows.Add(dr("palletizerid"), dr("palcode"), dr("palletizer"), dr("whsename"), dr("description"), stat)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grdline.Rows.Count = 0 Then
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

    Private Sub txtline_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtline.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtline_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtline.Leave
        txtline.Text = StrConv(txtline.Text, VbStrConv.ProperCase)
    End Sub

    Private Sub txttype_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtline.TextChanged
        If (Trim(txtline.Text) <> "" Or Trim(txtdes.Text) <> "") Then
            btncancel.Enabled = True
        ElseIf (Trim(txtline.Text) = "" And Trim(txtdes.Text) = "") And btnupdate.Text = "&Save" Then
            btncancel.Enabled = True
        Else
            btncancel.Enabled = False
        End If

        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789.-/"
        Dim theText As String = txtline.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtline.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtline.Text.Length - 1
            Letter = txtline.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtline.Text = theText
        txtline.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub txtaddress_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdes.Leave
        txtdes.Text = StrConv(txtdes.Text, VbStrConv.ProperCase)
    End Sub

    Private Sub txtaddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdes.TextChanged
        If (Trim(txtline.Text) <> "" Or Trim(txtdes.Text) <> "") Then
            btncancel.Enabled = True
        ElseIf (Trim(txtline.Text) = "" And Trim(txtdes.Text) = "") And btnupdate.Text = "&Save" Then
            btncancel.Enabled = True
        Else
            btncancel.Enabled = False
        End If

        Dim charactersDisallowed As String = "'"
        Dim theText As String = txtdes.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtdes.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtdes.Text.Length - 1
            Letter = txtdes.Text.Substring(x, 1)
            If charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtdes.Text = theText
        txtdes.Select(SelectionIndex - Change, 0)
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
        txtline.Text = ""
        txtcode.Text = ""
        txtdes.Text = ""
        cmbwhse.Text = ""
        btnupdate.Text = "&Update"
        btnsearch.Enabled = True
        btndeactivate.Enabled = True
        btnadd.Enabled = True
        btncancel.Enabled = False
        'MsgBox(btncancel.Enabled)
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" And login.wgroup <> "Agi Admin Staff" And login.wgroup <> "Supervisor" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdline.SelectedRows.Count = 1 Or grdline.SelectedCells.Count = 1 Then
                If btnupdate.Text = "&Update" Then
                    If grdline.Rows(grdline.CurrentRow.Index).Cells(5).Value = "Deactivated" Then
                        MsgBox("Cannot update deactivated warehouse name.", MsgBoxStyle.Exclamation, "")
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    txtcode.Text = grdline.Rows(grdline.CurrentRow.Index).Cells(1).Value
                    lblid.Text = grdline.Rows(grdline.CurrentRow.Index).Cells(0).Value
                    lblcat.Text = grdline.Rows(grdline.CurrentRow.Index).Cells(2).Value
                    txtline.Text = grdline.Rows(grdline.CurrentRow.Index).Cells(2).Value
                    cmbwhse.SelectedItem = grdline.Rows(grdline.CurrentRow.Index).Cells(3).Value
                    txtdes.Text = grdline.Rows(grdline.CurrentRow.Index).Cells(4).Value
                    btnsearch.Enabled = False
                    btnadd.Enabled = False
                    btndeactivate.Enabled = False
                    btnupdate.Text = "&Save"
                    btncancel.Enabled = True
                Else
                    'update
                    If Trim(txtcode.Text) = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Palletizer code should not be empty.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    If Trim(txtline.Text) = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Palletizer name should not be empty.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    If cmbwhse.SelectedItem = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Warehouse name should not be empty.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    sql = "Select * from tblpalletizer where palcode='" & Trim(txtcode.Text) & "' and whsename='" & cmbwhse.SelectedItem & "' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Me.Cursor = Cursors.Default
                        If dr("palletizerid").ToString <> Trim(lblid.Text) Then
                            Me.Cursor = Cursors.Default
                            MsgBox(Trim(txtcode.Text) & " is already exist", MsgBoxStyle.Information, "")
                            txtcode.Text = ""
                            txtcode.Focus()
                            Exit Sub
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    sql = "Select * from tblpalletizer where palletizer='" & Trim(txtline.Text) & "' and whsename='" & cmbwhse.SelectedItem & "' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        Me.Cursor = Cursors.Default
                        If dr("palletizerid").ToString <> Trim(lblid.Text) Then
                            Me.Cursor = Cursors.Default
                            MsgBox(Trim(txtline.Text) & " is already exist", MsgBoxStyle.Information, "")
                            txtline.Text = ""
                            txtline.Focus()
                            Exit Sub
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    linecnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If linecnf = True Then
                        ' If Trim(txtwhse.Text) <> Trim(lblcat.Text) Then
                        sql = "Update tblpalletizer set palcode='" & Trim(txtcode.Text) & "', palletizer='" & Trim(txtline.Text) & "', whsename='" & cmbwhse.SelectedItem & "', description='" & Trim(txtdes.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where palletizerid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        sql = "Update tbllogsheet set palletizer='" & Trim(txtline.Text) & "' where palletizer='" & lblcat.Text & "'"
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
                    txtcode.Text = ""
                    txtline.Text = ""
                    txtdes.Text = ""
                    txtline.Focus()
                    linecnf = False
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

            If grdline.SelectedRows.Count = 1 Or grdline.SelectedCells.Count = 1 Then
                lblid.Text = grdline.Rows(grdline.CurrentRow.Index).Cells(0).Value
                If btndeactivate.Text = "&Deactivate" Then
                    'check if theres item available status
                    '/sql = "Select * from tblgeneral where whsename='" & grdline.Rows(grdline.CurrentRow.Index).Cells(2).Value & "'"
                    '/connect()
                    '/cmd = New SqlCommand(sql, conn)
                    '/dr = cmd.ExecuteReader
                    '/If dr.Read Then
                    '/MsgBox("Cannot deactivate. Warehouse name is still in use.", MsgBoxStyle.Exclamation, "")
                    '/Exit Sub
                    '/End If
                    '/dr.Dispose()
                    '/cmd.Dispose()

                    linecnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If linecnf = True Then
                        sql = "Update tblpalletizer set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where palletizerid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    linecnf = False
                Else
                    linecnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If linecnf = True Then
                        sql = "Update tblpalletizer set status='1', datemodified=GetDate(), modifiedby='" & login.user & "' where palletizerid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    linecnf = False
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

    Private Sub grdwhse_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdline.SelectionChanged
        If grdline.Rows(grdline.CurrentRow.Index).Cells(5).Value = "Active" Then
            btndeactivate.Text = "&Deactivate"
        Else
            btndeactivate.Text = "A&ctivate"
        End If
    End Sub

    Public Sub loadwhse()
        Try
            cmbwhse.Items.Clear()

            sql = "Select * from tblwhse where status='1' and branch='" & login.branch & "' order by whsename"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbwhse.Items.Add(dr("whsename"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If cmbwhse.Items.Count <> 0 Then
                cmbwhse.SelectedIndex = 0
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

    Private Sub txtcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcode.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtcode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged
        If (Trim(txtcode.Text) <> "" Or Trim(txtdes.Text) <> "") Then
            btncancel.Enabled = True
        ElseIf (Trim(txtcode.Text) = "" And Trim(txtdes.Text) = "") And btnupdate.Text = "&Save" Then
            btncancel.Enabled = True
        Else
            btncancel.Enabled = False
        End If

        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789.-/"
        Dim theText As String = txtcode.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtcode.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtcode.Text.Length - 1
            Letter = txtcode.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtcode.Text = theText
        txtcode.Select(SelectionIndex - Change, 0)
    End Sub
End Class