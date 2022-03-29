Imports System.IO
Imports System.Data.SqlClient

Public Class tsearch
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

    Private Sub tsearch_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        txtsearch.Text = ""
    End Sub

    Private Sub tsearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lbltemp.Text = "L."
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Me.Close()
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            sql = "Select * from tbllogsheet where logsheetnum='" & lbltemp.Text & Trim(txtsearch.Text) & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                'check user level
                If dr("whsename").ToString = login.logwhse Then
                    Dim s As String = dr("logsheetnum").ToString
                    ticket.txtsearch.Text = s.Substring(2, s.Length - 2)
                    ticket.lblwhse.Text = dr("whsename").ToString
                    ticket.lbldate.Text = Format(Date.Now, "yyyy/MM/dd")
                    ticket.lblshift.Text = dr("shift").ToString
                    Me.Dispose()
                Else
                    MsgBox("Warehouse name access denied.", MsgBoxStyle.Critical, "")
                End If
            Else
                MsgBox("Cannot find ticket log sheet number.", MsgBoxStyle.Critical, "")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub txtsearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearch.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged

    End Sub
End Class