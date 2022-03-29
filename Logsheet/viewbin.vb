Imports System.IO
Imports System.Data.SqlClient

Public Class viewbin
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

    Private Sub viewbin_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        lbllognum.Text = ""
        lblid.Text = ""
        grd.Rows.Clear()
    End Sub

    Private Sub viewbin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            sql = "Select * from tbllogbin where logsheetnum='" & lbllognum.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                grd.Rows.Add(dr("logbin"), dr("binnum"), Format(dr("datecreated"), "yyyy/MM/dd HH:mm"), dr("createdby"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

        Catch ex As Exception

        End Try
    End Sub
End Class