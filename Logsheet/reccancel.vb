Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class reccancel
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public reccnlcnf As Boolean = False, reccnlby As String = ""

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

    Private Sub reccancel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            If Trim(txtrems.Text) <> "" Then
                reccnlcnf = False
                reccnlby = ""
                confirmsupwhse.ShowDialog()
                If reccnlcnf = True Then
                    ExecuteCancel(strconn)
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

    Private Sub ExecuteCancel(ByVal connectionString As String)
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

                'update ststus ng log sheet to 2 meaning confirmed and nde na available
                sql = "Update tblreceive set status='3', datecancelled=GetDate(), cancelledby='" & reccnlby & "', creason='" & Trim(txtrems.Text) & "' where recid='" & lbllogid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'update ststus ng log sheet to 2 meaning confirmed and nde na available
                sql = "Update tblrecof set status='3' where recnum='" & lbllognum.Text & "' and (branch='" & login.branch & "' or branch is null)"
                command.CommandText = sql
                command.ExecuteNonQuery()

                list2.Items.Clear()
                sql = "Select logsheetid from tbllogsheet where recnum='" & lbllognum.Text & "' and status<>'3'"
                command.CommandText = sql
                dr = command.ExecuteReader
                While dr.Read
                    list2.Items.Add(dr("logsheetid"))
                End While
                dr.Dispose()

                'update ststus ng log sheet to 2 meaning confirmed and nde na available
                sql = "Update tbllogsheet set status='3', canceldate=GetDate(), cancelby='" & reccnlby & "', cancelreason='" & Trim(txtrems.Text) & "' where recnum='" & lbllognum.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                For Each item As Object In list2.Items
                    '/tbllogrange()
                    'sql = "Update tbllogrange set status='3', canceldate=GetDate(), cancelby='" & reccnlby & "', cancelreason='" & Trim(txtrems.Text) & "' where logsheetnum=(Select TOP 1 logsheetnum from tbllogsheet where recnum='" & lbllognum.Text & "') and canceldate is NULL"
                    sql = "Update tbllogrange set status='3', canceldate=GetDate(), cancelby='" & reccnlby & "', cancelreason='" & Trim(txtrems.Text) & "' where logsheetid='" & item & "' and tbllogrange.canceldate is NULL"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '/tbllogitem()
                    sql = "Update tbllogitem set status='3', canceldate=GetDate(), cancelby='" & reccnlby & "', cancelreason='" & Trim(txtrems.Text) & "' where logsheetid='" & item & "' and tbllogitem.canceldate is NULL"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '/tbllogticket()
                    sql = "Update tbllogticket set status='3', canceldate=GetDate(), cancelby='" & reccnlby & "', cancelreason='" & Trim(txtrems.Text) & "' where logsheetid='" & item & "' and tbllogticket.canceldate is NULL"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '/tblloggood()
                    sql = "Update tblloggood set status='3', canceldate=GetDate(), cancelby='" & reccnlby & "' where logsheetid='" & item & "' and tblloggood.canceldate is NULL"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '/tbllogdouble()
                    sql = "Update tbllogdouble set status='3', canceldate=GetDate(), cancelby='" & reccnlby & "' where logsheetid='" & item & "' and tbllogdouble.canceldate is NULL"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '/tbllogcancel()
                    sql = "Update tbllogcancel set status='3', canceldate=GetDate(), cancelby='" & reccnlby & "' where logsheetid='" & item & "' and tbllogcancel.canceldate is NULL"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default

                deletetemptables()

                MsgBox("Successfully cancelled Receive# " & lbllogid.Text & ".", MsgBoxStyle.Information, "")
                Me.Dispose()

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.ToString, MsgBoxStyle.Exclamation, "")
                ' Attempt to roll back the transaction. 
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

    Public Sub deletetemptables()
        Try
            list1.Items.Clear()

            sql = "Select logticketid from tbllogticket where logsheetid='" & lbllogid.Text & "' and addtoloc is NULL"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                list1.Items.Add(dr("logticketid"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            For Each item As Object In list1.Items
                Dim tblticketid As String = item
                Dim tblexistgood As Boolean = False
                Dim tblexistcancel As Boolean = False
                Dim tblexistdouble As Boolean = False

                sql = "Select * from sys.tables where name = 'tbltempgood" & tblticketid & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    tblexistgood = True
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If tblexistgood = True Then
                    sql = "DROP Table tbltempgood" & tblticketid & ""
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                End If

                sql = "Select * from sys.tables where name = 'tbltempcancel" & tblticketid & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    tblexistcancel = True
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If tblexistcancel = True Then
                    sql = "DROP Table tbltempcancel" & tblticketid & ""
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                End If

                sql = "Select * from sys.tables where name = 'tbltempdouble" & tblticketid & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    tblexistdouble = True
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If tblexistdouble = True Then
                    sql = "DROP Table tbltempdouble" & tblticketid & ""
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                End If
            Next

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