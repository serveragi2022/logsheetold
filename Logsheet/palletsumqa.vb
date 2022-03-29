Imports System.IO
Imports System.Data.SqlClient

Public Class palletsumqa
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public palqacnf As Boolean = False

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

    Private Sub palletsumqa_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        grdpallettag.Rows.Clear()
        txtrems.Text = ""
    End Sub

    Private Sub palletsumqa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If grdpallettag.Rows.Count = 0 Then
            btnqaok.Enabled = False
            btnqahold.Enabled = False
            btnqacancel.Enabled = False
        Else
            btnqaok.Enabled = True
            btnqahold.Enabled = True
            btnqacancel.Enabled = True
        End If
    End Sub

    Private Sub btnqaok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnqaok.Click
        Try
            If login.depart = "QCA" Or login.depart = "All" Then
                palqacnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If palqacnf = True Then

                    For Each row As DataGridViewRow In grdpallettag.Rows
                        Dim logticketid As String = grdpallettag.Rows(row.Index).Cells(0).Value
                        Dim pallet As String = grdpallettag.Rows(row.Index).Cells(1).Value
                        Dim dispo As String = grdpallettag.Rows(row.Index).Cells(2).Value

                        '/If dispo <> "Ok" Then
                        'check muna if may disposition na si qa tapos anung indicator na may dispo na sya
                        sql = "Update tbllogticket set qadispo='1', qaname='" & login.fullneym & "', qadate=GetDate(), qarems='" & Trim(txtrems.Text) & "' where logticketid='" & logticketid & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        sql = "Insert into tbllogticketqa (logticketid, palletnum, qadispo, qaname, qadate, qarems) values ('" & logticketid & "', '" & pallet & "', '1', '" & login.fullneym & "', GetDate(), '" & Trim(txtrems.Text) & "')"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                        '/End If
                    Next

                    MsgBox("Successfully ok.", MsgBoxStyle.Information, "")
                    Me.Dispose()
                End If
            Else
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
            End If

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnqahold_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnqahold.Click
        Try
            If login.depart = "QCA" Or login.depart = "All" Then
                If Trim(txtrems.Text) = "" Then
                    MsgBox("Input remarks first.", MsgBoxStyle.Exclamation, "")
                    txtrems.Focus()
                    Exit Sub
                End If

                palqacnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If palqacnf = True Then
                    For Each row As DataGridViewRow In grdpallettag.Rows
                        Dim logticketid As String = grdpallettag.Rows(row.Index).Cells(0).Value
                        Dim pallet As String = grdpallettag.Rows(row.Index).Cells(1).Value
                        Dim dispo As String = grdpallettag.Rows(row.Index).Cells(2).Value

                        '/If dispo <> "Hold" Then
                        'check muna if may disposition na si qa tapos anung indicator na may dispo na sya
                        sql = "Update tbllogticket set qadispo='2', qaname='" & login.fullneym & "', qadate=GetDate(), qarems='" & Trim(txtrems.Text) & "' where logticketid='" & logticketid & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        sql = "Insert into tbllogticketqa (logticketid, palletnum, qadispo, qaname, qadate, qarems) values ('" & logticketid & "', '" & pallet & "', '2', '" & login.fullneym & "', GetDate(), '" & Trim(txtrems.Text) & "')"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                        '/End If
                    Next

                    MsgBox("Successfully hold.", MsgBoxStyle.Information, "")
                    Me.Dispose()
                End If
            Else
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
            End If

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnqacancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnqacancel.Click
        Try
            If login.depart = "QCA" Or login.depart = "All" Then
                palqacnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If palqacnf = True Then
                    For Each row As DataGridViewRow In grdpallettag.Rows
                        Dim logticketid As String = grdpallettag.Rows(row.Index).Cells(0).Value
                        Dim pallet As String = grdpallettag.Rows(row.Index).Cells(1).Value
                        Dim dispo As String = grdpallettag.Rows(row.Index).Cells(2).Value

                        '/If dispo <> "Pending" Then
                        'check muna if may disposition na si qa tapos anung indicator na may dispo na sya
                        sql = "Update tbllogticket set qadispo='3', qaname='" & login.fullneym & "', qadate=GetDate(), qarems='" & Trim(txtrems.Text) & "' where logticketid='" & logticketid & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        sql = "Insert into tbllogticketqa (logticketid, palletnum, qadispo, qaname, qadate, qarems) values ('" & logticketid & "', '" & pallet & "', '3', '" & login.fullneym & "', GetDate(), '" & Trim(txtrems.Text) & "')"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                        '/End If
                    Next

                    MsgBox("Successfully removed dispo.", MsgBoxStyle.Information, "")
                    Me.Dispose()
                End If
            Else
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
            End If

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub txtrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtrems_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrems.TextChanged

    End Sub
End Class