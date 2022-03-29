Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class ticketbag
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public bagcnf As Boolean = False

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

    Private Sub ticketbag_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtmin.ReadOnly = True
        txtmax.ReadOnly = True
        txtwait.ReadOnly = True
        btnupdate.Text = "Update"
        cmbsack.Enabled = True
    End Sub

    Private Sub txtmin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmin.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = 46 And Trim(txtmin.Text) <> "" And txtmin.Text.Contains(".") = True Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 48 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 46 Then

        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            If Trim(txtmin.Text) <> "" And Trim(txtmax.Text) <> "" Then
                'check if min is less than max
                If Val(txtmin.Text) < Val(txtmax.Text) Then
                    btnupdate.PerformClick()

                Else
                    MsgBox("Invalid input.", MsgBoxStyle.Exclamation, "")
                    txtmin.Text = ""
                    txtmin.Focus()
                End If
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtmin_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmin.Leave
        If Trim(txtmin.Text) <> "" And Trim(txtmax.Text) <> "" Then
            'check if min is less than max
            If Val(txtmin.Text) < Val(txtmax.Text) Then

            Else
                MsgBox("Invalid input.", MsgBoxStyle.Exclamation, "")
                txtmin.Text = ""
                txtmin.Focus()
            End If
        End If
    End Sub

    Private Sub txtmin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmin.TextChanged
        Dim charactersDisallowed As String = "0123456789."
        Dim theText As String = txtmin.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtmin.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtmin.Text.Length - 1
            Letter = txtmin.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtmin.Text = theText
        txtmin.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub txtmax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmax.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = 46 And Trim(txtmax.Text) <> "" And txtmax.Text.Contains(".") = True Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 48 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 46 Then

        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            If Trim(txtmin.Text) <> "" And Trim(txtmax.Text) <> "" Then
                'check if min is less than max
                If Val(txtmax.Text) > Val(txtmin.Text) Then
                    btnupdate.PerformClick()

                Else
                    MsgBox("Invalid input.", MsgBoxStyle.Exclamation, "")
                    txtmax.Text = ""
                    txtmax.Focus()
                End If
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtmax_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmax.Leave
        If Trim(txtmin.Text) <> "" And Trim(txtmax.Text) <> "" Then
            'check if min is less than max
            If Val(txtmax.Text) > Val(txtmin.Text) Then

            Else
                MsgBox("Invalid input.", MsgBoxStyle.Exclamation, "")
                txtmax.Text = ""
                txtmax.Focus()
            End If
        End If
    End Sub

    Private Sub txtmax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmax.TextChanged
        Dim charactersDisallowed As String = "0123456789."
        Dim theText As String = txtmax.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtmax.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtmax.Text.Length - 1
            Letter = txtmax.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtmax.Text = theText
        txtmax.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            'check level of users
            If login.depart <> "All" And login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" And login.wgroup <> "Supervisor" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If btnupdate.Text = "Update" Then
                txtmin.ReadOnly = False
                txtmax.ReadOnly = False
                txtwait.ReadOnly = False
                btnupdate.Text = "Save"
                cmbsack.Enabled = False
            Else

                If Val(txtwait.Text) = 0 Then
                    MsgBox("Input waiting time.", MsgBoxStyle.Exclamation, "")
                    txtwait.Focus()
                    Exit Sub
                End If

                If cmbsack.SelectedItem = "" Then
                    MsgBox("Input bag type.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                bagcnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If bagcnf = True Then
                    'check muna kung cmbsacl is meron na. if wala insert
                    Dim meronna As Boolean = False
                    sql = "Select status from tblbag where bagtype='" & cmbsack.SelectedItem & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        meronna = True
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    If meronna = True Then
                        sql = "Update tblbag set minimum='" & Val(txtmin.Text) & "', maximum='" & Val(txtmax.Text) & "', tym='" & Val(txtwait.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where bagtype='" & cmbsack.SelectedItem & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    Else
                        sql = "Insert into tblbag (bagtype, minimum, maximum, tym, datecreated, createdby, datemodified, modifiedby, status)"
                        sql = sql & " values ('" & cmbsack.SelectedItem & "','" & Val(txtmin.Text) & "','" & Val(txtmax.Text) & "','" & Val(txtwait.Text) & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1')"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    End If

                    MsgBox("Successfully saved.", MsgBoxStyle.Information, "")

                    txtmin.ReadOnly = True
                    txtmax.ReadOnly = True
                    txtwait.ReadOnly = True
                    btnupdate.Text = "Update"
                    cmbsack.Enabled = True
                    vieww()
                End If
            End If

            Me.Cursor = Cursors.Default
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

    Public Sub vieww()
        Try
            sql = "Select * from tblbag where bagtype='" & cmbsack.SelectedItem & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                txtmin.Text = dr("minimum")
                txtmax.Text = dr("maximum")
                txtwait.Text = dr("tym")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.Default
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

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        vieww()
        txtmin.ReadOnly = True
        txtmax.ReadOnly = True
        txtwait.ReadOnly = True
        btnupdate.Text = "Update"
        cmbsack.Enabled = True
    End Sub

    Private Sub cmbsack_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsack.SelectedIndexChanged
        Try
            If cmbsack.SelectedItem <> "" Then
                vieww()
            Else
                txtmin.Text = ""
                txtmax.Text = ""
                txtwait.Text = 0
            End If

            Me.Cursor = Cursors.Default
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

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwait.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwait.TextChanged
        Dim charactersDisallowed As String = "0123456789"
        Dim theText As String = txtwait.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtwait.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtwait.Text.Length - 1
            Letter = txtwait.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtwait.Text = theText
        txtwait.Select(SelectionIndex - Change, 0)
    End Sub
End Class