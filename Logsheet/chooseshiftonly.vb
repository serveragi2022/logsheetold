Imports System.IO
Imports System.Data.SqlClient

Public Class chooseshiftonly
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

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

    Private Sub chooseshift_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If login.logshift = "" And (login.wgroup <> "Administrator" And login.wgroup <> "Admin Dispatching" And login.wgroup <> "Manager" And login.wgroup <> "Supervisor") Then
            Me.Dispose()
            mdiform.Dispose()
            login.Show()
        Else
        End If
    End Sub

    Private Sub chooseshift_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadshift()
    End Sub

    Public Sub loadshift()
        Try
            cmbshift.Items.Clear()

            sql = "Select * from tblshift where status='1' order by shift"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbshift.Items.Add(dr("shift"))
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

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            If Trim(cmbshift.Text) = "" Then
                MsgBox("Select shift first.", MsgBoxStyle.Information, "")
                cmbshift.Focus()
                Exit Sub
            Else
                sql = "Select * from tblshift where status<>'1' and shift='" & Trim(cmbshift.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Deactivated shift.", MsgBoxStyle.Information, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                sql = "Select * from tblshift where status='1' and shift='" & Trim(cmbshift.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                Else
                    MsgBox("Invalid shift.", MsgBoxStyle.Critical, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If login.logshift <> "" Then
                    'ibig sabihin mag sswitch lng 
                    'confirmation
                    Dim a As String = MsgBox("Are you sure you want to change shift?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                    If a <> vbYes Then
                        Exit Sub
                    End If
                End If

                Dim loginid As Integer = 0
                sql = "Select TOP 1 * from tbllogin where username='" & login.user & "' and datelogin=Getdate() order by loginid DESC"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    '/MsgBox(dr("datelogin").ToString)
                    loginid = dr("loginid")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                sql = "Update tbllogin set shift='" & Trim(cmbshift.Text) & "' where loginid='" & loginid & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()

                login.logshift = Trim(cmbshift.Text)

                mdiform.Text = "Ticket Log Sheet " & "(" & login.logwhse & " - Shift: " & login.logshift & " - " & login.user & ")"

                For Each f As Form In mdiform.MdiChildren
                    f.Dispose()
                Next

                Me.Dispose()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        If login.logshift = "" And (login.wgroup <> "Administrator" And login.wgroup <> "Admin Dispatching" And login.wgroup <> "Manager" And login.wgroup <> "Supervisor") Then
            Me.Dispose()
            mdiform.Dispose()
            login.Show()
        End If
    End Sub

    Private Sub cmbwhse_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbshift_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbshift.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnok.Focus()
            If Trim(cmbshift.Text) <> "" Then
                btnok.PerformClick()
            End If
        End If
    End Sub

    Private Sub cmbshift_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbshift.Leave
        Try
            If Not cmbshift.Items.Contains(Trim(cmbshift.Text.ToUpper)) = True And Trim(cmbshift.Text.ToUpper) <> "" Then
                MsgBox("Invalid shift name.", MsgBoxStyle.Critical, "")
                cmbshift.Text = ""
                cmbshift.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbwhse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmbshift_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbshift.SelectedIndexChanged

    End Sub

    Private Sub cmbwhse_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
End Class