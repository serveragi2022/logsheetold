Imports System.IO
Imports System.Data.SqlClient

Public Class coaofnumselect
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

    Private Sub coaofnumselect_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub coaofnumselect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ExecuteView(strconn)
    End Sub

    Private Sub ExecuteView(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                'load yung coa info
                grdcoa.Rows.Clear()

                sql = "Select distinct o.ofid, o.ofnum, o.wrsnum, o.customer, o.status from tblorderfill o"
                sql = sql & " inner join tblofitem i on o.ofid=i.ofid and o.branch=i.branch and (i.coanum='' or i.coanum is NULL)"
                sql = sql & " where o.status=2 and i.status=2 and o.branch='" & login.branch & "'"
                sql = sql & " order by o.ofid"
                command.CommandText = sql
                dr = command.ExecuteReader
                While dr.Read
                    Dim stat As String = ""
                    If dr("status") = 1 Then
                        stat = "In Process"
                    ElseIf dr("status") = 2 Then
                        stat = "Completed"
                    ElseIf dr("status") = 3 Then
                        stat = "Cancelled"
                    End If
                    grdcoa.Rows.Add(dr("ofid"), dr("ofnum"), dr("wrsnum"), dr("customer"), stat)
                End While
                dr.Dispose()


                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.ToString, MsgBoxStyle.Exclamation, "")
                Try
                    Me.Cursor = Cursors.Default
                    transaction.Rollback()
                Catch ex2 As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox("2: " & ex2.ToString & vbCrLf & vbCrLf & "Please try again.", MsgBoxStyle.Information, "")
                End Try
            End Try
        End Using
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        Try
            Dim s As String = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(1).Value
            sql = "Select * from tblorderfill where ofnum='" & s & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                coacreate.txtofnum.Text = s.Substring(3, s.Length - 3)
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            '/coa.btnsearch.PerformClick()
            Me.Close()

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
End Class