Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class orderfillothers
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Public frm As String, ofid As Integer

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

    Private Sub orderfillothers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewothers()
    End Sub

    Private Sub txtstart_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
        Exit Sub
        If Asc(e.KeyChar) <> 8 And Asc(e.KeyChar) <> 1 And Asc(e.KeyChar) <> 3 And Asc(e.KeyChar) <> 24 And Asc(e.KeyChar) <> 25 And Asc(e.KeyChar) <> 26 Then
            If Asc(e.KeyChar) = 39 Then
                e.Handled = True
            End If
        End If
    End Sub


    Private Sub txtstart_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtend_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
        Exit Sub
        If Asc(e.KeyChar) <> 8 And Asc(e.KeyChar) <> 1 And Asc(e.KeyChar) <> 3 And Asc(e.KeyChar) <> 24 And Asc(e.KeyChar) <> 25 And Asc(e.KeyChar) <> 26 Then
            If Asc(e.KeyChar) = 39 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtend_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtfork_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfork.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtfork_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfork.TextChanged

    End Sub

    Private Sub txtsteve_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtsteve_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtpass_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 68 Or Asc(e.KeyChar) = 100 Then

        ElseIf Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtpass_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Me.Close()
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            sql = "Update tblorderfill set forklift='" & Trim(txtfork.Text) & "' where ofid='" & ofid & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            conn.Close()

            'MsgBox("Successfully Saved.")
            Me.Close()

            Me.Cursor = Cursors.Default
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

    Public Sub viewothers()
        Try
            sql = "Select pstart,pend,forklift,stevedore,passnum,matdispo,fgstart,fgend,loadstart,loadend  from tblorderfill where ofid='" & ofid & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                txtfork.Text = dr("forklift").ToString
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.Default
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
End Class