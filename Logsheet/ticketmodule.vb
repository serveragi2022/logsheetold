Imports System.IO
Imports System.Data.SqlClient

Module ticketmodule
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim flo As String = "Forklift"

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

    Sub viewforklift()
        ticket.cmbfork.Items.Clear()
        recticket.cmbfork.Items.Clear()

        sql = "Select * from tblusers where workgroup='Forklift Operator' and (branch='" & login.branch & "' or branch='All') and status='1'"
        connect()
        cmd = New SqlCommand(sql, conn)
        dr = cmd.ExecuteReader
        While dr.Read
            ticket.cmbfork.Items.Add(dr("fullname"))
            recticket.cmbfork.Items.Add(dr("fullname"))
        End While
        dr.Dispose()
        cmd.Dispose()
        conn.Close()
    End Sub

    Sub viewforklift1()
        ticket.cmbfork.Items.Clear()

        sql = "Select * from tblusers where workgroup='Forklift Operator' and (branch='" & login.branch & "' or branch='All') and status='1'"
        connect()
        cmd = New SqlCommand(sql, conn)
        dr = cmd.ExecuteReader
        While dr.Read
            viewpallets.cmbfork.Items.Add(dr("fullname"))
        End While
        dr.Dispose()
        cmd.Dispose()
        conn.Close()
    End Sub

    Public Function remaining(ByVal logsheetid As Integer) As Integer
        Dim natira As Integer
        'ticket range - total number
        'vs sa combine count sa good cancel at double
        Dim totalrange As Integer = 0
        sql = "Select arfrom, arto from tbllogrange where status<>'3' and logsheetid='" & logsheetid & "'"
        connect()
        cmd = New SqlCommand(sql, conn)
        dr = cmd.ExecuteReader
        While dr.Read
            totalrange = totalrange + (dr("arto") - dr("arfrom")) + 1
        End While
        dr.Dispose()
        cmd.Dispose()
        conn.Close()

        Return totalrange
    End Function
End Module
