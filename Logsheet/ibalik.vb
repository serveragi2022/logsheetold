Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class ibalik
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
        If conn.State <> ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        sql = "select * from tbllogticket where canceldate is not NULL"
        connect()
        cmd = New SqlCommand(sql, conn)
        dr = cmd.ExecuteReader
        While dr.Read
            Dim temp As String = "update tbllogticket set status='3', cancelby='" & dr("cancelby").ToString & "', canceldate='" & dr("canceldate").ToString & "', cancelreason='" & dr("cancelreason").ToString & "' where logticketid=" & dr("logticketid") & ""
            grd.Rows.Add(dr("logticketid"), dr("cancelby").ToString, dr("canceldate").ToString, dr("cancelreason").ToString, temp)
        End While
        dr.Dispose()
        cmd.Dispose()
        conn.Close()

        MsgBox("Successfully get.")
    End Sub
End Class