Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class coacancel
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public coacnlcnf As Boolean = False, coacnlby As String = ""

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

    Private Sub orderfillcancel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            If Trim(txtrems.Text) <> "" Then
                coacnlcnf = False
                coacnlby = ""
                confirmsupqca.ShowDialog()
                If coacnlcnf = True Then
                    'tblcoa
                    sql = "Update tblcoa set status='3', canceldate=GetDate(), cancelby='" & coacnlby & "', cancelreason='" & Trim(txtrems.Text) & "' where coaid='" & lblcoaid.Text & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    'tblcoaoflog
                    'tblcoasub

                    'update tblofitem with coanumber
                    sql = "Update tblofitem set coanum='' where coanum='" & lblcoanum.Text & "' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()


                    MsgBox("Successfully cancelled COA # " & lblcoanum.Text & ".", MsgBoxStyle.Information, "")
                    Me.Dispose()
                Else
                    MsgBox("Authentication has been cancelled.", MsgBoxStyle.Information, "")
                End If
            Else
                MsgBox("Input reason.", MsgBoxStyle.Exclamation, "")
                txtrems.Focus()
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

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Me.Dispose()
    End Sub

    Private Sub txtrems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtrems.KeyDown

    End Sub

    Private Sub txtrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtrems_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrems.TextChanged

    End Sub
End Class