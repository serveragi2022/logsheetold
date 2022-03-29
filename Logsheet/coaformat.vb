Imports System.IO
Imports System.Data.SqlClient

Public Class coaformat
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public cnf As Boolean

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

    Private Sub rbspecs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbspecs.CheckedChanged
        If rbspecs.Checked = True Then
            Label4.Text = rbspecs.Text & ":"
            grd.Columns(3).HeaderText = rbspecs.Text
        End If
    End Sub

    Private Sub rbmethod_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbmethod.CheckedChanged
        If rbmethod.Checked = True Then
            Label4.Text = rbmethod.Text & ":"
            grd.Columns(3).HeaderText = rbmethod.Text
        End If
    End Sub

    Private Sub rbstand_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbstand.CheckedChanged
        If rbstand.Checked = True Then
            Label4.Text = rbstand.Text & ":"
            grd.Columns(3).HeaderText = rbstand.Text
        End If
    End Sub

    Private Sub coaformat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'rbspecs.Checked = True
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        If Trim(txtcat.Text) = "" Then
            MsgBox("Input category.", MsgBoxStyle.Exclamation, "")
            txtcat.Focus()
        ElseIf Trim(txtname.Text) = "" Then
            MsgBox("Input criteria name.", MsgBoxStyle.Exclamation, "")
            txtname.Focus()
            'ElseIf Trim(txtvalue.Text) = "" Then
            '    MsgBox("Input " & grd.Columns(2).HeaderText & ".", MsgBoxStyle.Exclamation, "")
            '    txtvalue.Focus()
        Else
            'add to grd if wala pang ganung name sa grd
            For Each row As DataGridViewRow In grd.Rows
                Dim cat As String = grd.Rows(row.Index).Cells(1).Value
                Dim neym As String = grd.Rows(row.Index).Cells(2).Value
                Dim cval As String = grd.Rows(row.Index).Cells(3).Value
                If btnadd.Text = "Add" Then
                    If neym = Trim(txtname.Text) And cat = Trim(txtcat.Text) Then
                        MsgBox(Trim(txtname.Text) & " is already added.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                Else
                    If neym = Trim(txtname.Text) And cat = Trim(txtcat.Text) And cval = Trim(txtvalue.Text) Then
                        MsgBox(Trim(txtname.Text) & " is already exist.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
            Next

            If btnadd.Text = "Add" Then
                grd.Rows.Add("", Trim(txtcat.Text), Trim(txtname.Text), Trim(txtvalue.Text))
            Else
                grd.Rows(grd.CurrentRow.Index).Cells(1).Value = txtcat.Text
                grd.Rows(grd.CurrentRow.Index).Cells(2).Value = txtname.Text
                grd.Rows(grd.CurrentRow.Index).Cells(3).Value = txtvalue.Text
                btncancel.PerformClick()
            End If
            txtvalue.Text = ""
            txtname.Text = ""
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnconfirm.Click
        Try
            If login.wgroup <> "Supervisor" And login.wgroup <> "Administrator" Then
                MsgBox("Access Denied.", MsgBoxStyle.Information, "")
                Exit Sub
            End If

            If grd.Rows.Count = 0 Then
                MsgBox("Add criteria.", MsgBoxStyle.Exclamation, "")
            ElseIf cmbformat.Text = "" Then
                MsgBox("Select COA format.", MsgBoxStyle.Exclamation, "")
            Else
                'save to db
                'if row may id, update, if wala insert
                cnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If cnf = True Then
                    Dim tayp As String = ""
                    If rbspecs.Checked = True Then
                        tayp = rbspecs.Text
                    ElseIf rbmethod.Checked = True Then
                        tayp = rbmethod.Text
                    ElseIf rbstand.Checked = True Then
                        tayp = rbstand.Text
                    End If
                    For Each row As DataGridViewRow In grd.Rows
                        Dim id As String = grd.Rows(row.Index).Cells(0).Value
                        Dim cat As String = grd.Rows(row.Index).Cells(1).Value.ToString.Replace("'", "''")
                        Dim neym As String = grd.Rows(row.Index).Cells(2).Value.ToString.Replace("'", "''")
                        Dim cval As String = grd.Rows(row.Index).Cells(3).Value.ToString.Replace("'", "''")

                        If id = "" Then
                            'insert
                            sql = "Insert into tblcoaformat (coaform, analysis, type, category, cname, cvalue, legend, datecreated, createdby, datemodified, modifiedby, status)"
                            sql = sql & " values ('" & cmbformat.SelectedItem & "', '" & Trim(txtlabel.Text) & "', '" & tayp & "', '" & cat & "', '" & neym & "', '" & cval & "',"
                            sql = sql & " '" & Trim(txtleg.Text) & "', Getdate(), '" & login.user & "', Getdate(), '" & login.user & "', '1')"
                        Else
                            'update
                            sql = "Update tblcoaformat set analysis='" & Trim(txtlabel.Text) & "', type='" & tayp & "', category='" & cat & "', cname='" & neym & "', cvalue='" & cval & "',"
                            sql = sql & " legend='" & Trim(txtleg.Text) & "',datemodified=Getdate(), modifiedby='" & login.user & "' where paramid='" & id & "'"
                        End If

                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    Next

                    MsgBox("Successfully saved.", MsgBoxStyle.Information, "")
                    cmbformat.SelectedItem = cmbformat.Text
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbformat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbformat.SelectedIndexChanged

    End Sub

    Private Sub cmbformat_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbformat.SelectedValueChanged
        Try
            grd.Rows.Clear()
            txtlabel.Text = ""
            txtleg.Text = ""

            sql = "Select * from tblcoaformat where coaform='" & cmbformat.SelectedItem & "' and status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                txtlabel.Text = dr("analysis").ToString
                txtleg.Text = dr("legend").ToString
                If dr("type") = rbspecs.Text Then
                    rbspecs.Checked = True
                ElseIf dr("type") = rbmethod.Text Then
                    rbmethod.Checked = True
                ElseIf dr("type") = rbstand.Text Then
                    rbstand.Checked = True
                End If
                grd.Rows.Add(dr("paramid"), dr("category"), dr("cname"), dr("cvalue"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        lblid.Text = ""
        txtcat.Text = ""
        txtname.Text = ""
        txtvalue.Text = ""
        btnadd.Text = "Add"
        btnadd.Enabled = True
        btndeac.Enabled = True
        grd.Enabled = True
        btnupdate.Text = "Update"
    End Sub

    Private Sub grd_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd.CellContentClick

    End Sub

    Private Sub grd_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grd.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then

                grd.ClearSelection()
                grd.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True

                'selectedrow = e.RowIndex

                Me.ContextMenuStrip1.Show(Cursor.Position)

            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Try
            If grd.SelectedRows.Count = 1 Or grd.SelectedCells.Count = 1 Then
                lblid.Text = grd.CurrentRow.Index
                txtcat.Text = grd.Rows(grd.CurrentRow.Index).Cells(1).Value
                txtname.Text = grd.Rows(grd.CurrentRow.Index).Cells(2).Value
                txtvalue.Text = grd.Rows(grd.CurrentRow.Index).Cells(3).Value
                grd.Enabled = False
                btnadd.Text = "Save"
            Else
                MsgBox("Select only one.", MsgBoxStyle.Exclamation, "")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        If btnupdate.Text = "Update" Then
            lblid.Text = grd.Rows(grd.CurrentRow.Index).Cells("pid").Value
            txtcat.Text = grd.Rows(grd.CurrentRow.Index).Cells("cat").Value
            txtname.Text = grd.Rows(grd.CurrentRow.Index).Cells("neym").Value
            txtvalue.Text = grd.Rows(grd.CurrentRow.Index).Cells("spec").Value

            btnadd.Enabled = False
            btndeac.Enabled = False
            grd.Enabled = False
            btnupdate.Text = "Ok"
        Else
            'check if walang kaparehas
            For Each row As DataGridViewRow In grd.Rows
                Dim pid As Integer = grd.Rows(row.Index).Cells("pid").Value
                Dim cat As String = grd.Rows(row.Index).Cells("cat").Value
                Dim neym As String = grd.Rows(row.Index).Cells("neym").Value
                Dim valyu As String = grd.Rows(row.Index).Cells("spec").Value

                If pid <> lblid.Text Then
                    If cat = Trim(txtcat.Text) And neym = Trim(txtname.Text) And valyu = Trim(txtvalue.Text) Then
                        MsgBox("Already exist.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
            Next

            grd.Rows(grd.CurrentRow.Index).Cells("cat").Value = txtcat.Text
            grd.Rows(grd.CurrentRow.Index).Cells("neym").Value = txtname.Text
            grd.Rows(grd.CurrentRow.Index).Cells("spec").Value = txtvalue.Text

            btncancel.PerformClick()
        End If
    End Sub
End Class