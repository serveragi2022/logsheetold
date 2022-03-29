Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class orderfillcancel
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public ofcnlcnf As Boolean = False, ofcnlby As String = ""

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
                ofcnlcnf = False
                ofcnlby = ""
                confirmsupwhse.ShowDialog()
                If ofcnlcnf = True Then
                    ExecuteCancelOF(strconn)
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

    Private Sub ExecuteCancelOF(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                '/command.CommandText = sql
                '/command.ExecuteNonQuery()

                'update ststus ng tblorderfill to 2 meaning confirmed and nde na available
                sql = "Update tblorderfill set status='3', canceldate=GetDate(), cancelby='" & ofcnlby & "', cancelreason='" & Trim(txtrems.Text) & "' where ofid='" & lblofid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                '/tblofitem()
                sql = "Update tblofitem set status='3', canceldate=GetDate(), cancelby='" & ofcnlby & "', cancelreason='" & Trim(txtrems.Text) & "' where ofid='" & lblofid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                '/tbloflog()
                sql = "Update tbloflog set status='3', canceldate=GetDate(), cancelby='" & ofcnlby & "', cancelreason='" & Trim(txtrems.Text) & "' where ofid='" & lblofid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'gawing status=1 ulet lahat kasi pag buong pick ginagawang status=0 na kasi wala ng laman
                sql = "Update tbllogticket SET status = 1"
                sql = sql & " FROM (Select distinct logticketid from tblloggood where status='0' and ofid='" & lblofid.Text & "') i"
                sql = sql & " WHERE tbllogticket.status ='0' and tbllogticket.logticketid=i.logticketid"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'gawing status=1
                sql = "Update tblloggood Set status='1', ofid=null where status='0' and ofid='" & lblofid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'gawing status=1
                sql = "Update tbllogdouble set status='1', ofid=null where status='0' and ofid='" & lblofid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                '/tblofloggood()
                sql = "Update tblofloggood set status='3' where ofid='" & lblofid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                '/tbloflogcancel()
                sql = "Update tbloflogcancel set status='3' where ofid='" & lblofid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'tanggalin ung mga selected na tbllogticket sakanya
                sql = "Update tbllogticket set cusreserve='0', ofnum='', ofid=NULL where ofid='" & lblofid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()


                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                ExecuteDeleteTemp(strconn)
                MsgBox("Successfully cancelled Order fill # " & lblofnum.Text & ".", MsgBoxStyle.Information, "")
                Me.Dispose()

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.Message, MsgBoxStyle.Exclamation, "")
                ' Attempt to roll back the transaction. 
                Try
                    Me.Cursor = Cursors.Default
                    transaction.Rollback()
                Catch ex2 As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox("2: " & ex2.Message & vbCrLf & vbCrLf & "Please try again.", MsgBoxStyle.Information, "")
                End Try
            End Try
        End Using
    End Sub

    Private Sub ExecuteDeleteTemp(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                '/command.CommandText = sql
                '/command.ExecuteNonQuery()
                list1.Items.Clear()

                sql = "Select ofitemid from tblofitem where ofid='" & lblofid.Text & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                While dr.Read
                    list1.Items.Add(dr("ofitemid"))
                End While
                dr.Dispose()


                For i = 0 To list1.Items.Count
                    Dim tblofitemid As String = list1.Items(0).ToString
                    Dim tblexistofitem As Boolean = False
                    Dim tblexistoflog As Boolean = False
                    Dim tblexistoflogcancel As Boolean = False

                    sql = "Select * from sys.tables where name = 'tbltempofitem" & tblofitemid & "'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    If dr.Read Then
                        tblexistofitem = True
                    End If
                    dr.Dispose()

                    If tblexistofitem = True Then
                        sql = "DROP Table tbltempofitem" & tblofitemid & ""
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    End If

                    sql = "Select * from sys.tables where name = 'tbltempoflog" & tblofitemid & "'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    If dr.Read Then
                        tblexistoflog = True
                    End If
                    dr.Dispose()

                    If tblexistoflog = True Then
                        sql = "DROP Table tbltempoflog" & tblofitemid & ""
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    End If

                    sql = "Select * from sys.tables where name = 'tbltempoflogcancel" & tblofitemid & "'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    If dr.Read Then
                        tblexistoflogcancel = True
                    End If
                    dr.Dispose()

                    If tblexistoflogcancel = True Then
                        sql = "DROP Table tbltempoflogcancel" & tblofitemid & ""
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    End If
                Next

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.Message, MsgBoxStyle.Exclamation, "")
                ' Attempt to roll back the transaction. 
                Try
                    Me.Cursor = Cursors.Default
                    transaction.Rollback()
                Catch ex2 As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox("2: " & ex2.Message & vbCrLf & vbCrLf & "Please try again.", MsgBoxStyle.Information, "")
                End Try
            End Try
        End Using
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