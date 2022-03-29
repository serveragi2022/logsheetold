Imports System.IO
Imports System.Data.SqlClient

Public Class branchlist
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)

    Dim sql As String
    Dim conn As SqlConnection
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand

    Private Sub connect()
        conn = New SqlConnection 'New OleDb.OleDbConnection
        conn.ConnectionString = strconn
        If conn.State <> ConnectionState.Open Then
            conn.Open()
        End If
    End Sub

    Private Sub disconnect()
        conn = New SqlConnection 'New OleDb.OleDbConnection
        conn.ConnectionString = strconn
        If conn.State <> ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub branchlist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        vbranch()
    End Sub

    Private Sub vbranch()
        Try
            cmbbr.Items.Clear()
            sql = "Select * from tblbranch where status='1' order by branch"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbbr.Items.Add(dr("branch"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

        Catch ex As Exception
            ex.ToString()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Trim(cmbbr.SelectedItem) <> "" Then
            login.branch = Trim(cmbbr.Text)
            mdiform.Text = mdiform.Text & " (" & login.branch & " - Whse:" & login.logwhse & " - " & login.user & ")"
            mdiform.Show()
            Me.Dispose()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        login.savelogout()
        login.Show()
        Me.Dispose()
    End Sub
End Class