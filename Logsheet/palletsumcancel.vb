Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class palletsumcancel
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public palsumcnlcnf As Boolean = False

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

    Private Sub palletsumcancel_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        grdcancel.Rows.Clear()
        txtpallet.Text = ""
        txtlog.Text = ""
    End Sub

    Private Sub palletsumcancel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewcancels()
    End Sub

    Public Sub viewcancels()
        Try
            grdcancel.Rows.Clear()

            sql = "Select c.logcancelid, s.logsheetnum, t.palletnum, c.letter, c.cticketnum, c.cticketdate, c.grossw, c.remarks, c.tickettype, t.whsechecker"
            sql =sql & " from tbllogcancel c left outer join tbllogticket t On c.logticketid=t.logticketid right outer join tbllogsheet s On s.logsheetid=t.logsheetid"
            sql = sql & " where t.palletnum='" & txtpallet.Text & "' and s.branch='" & login.branch & "' and s.logsheetnum='" & txtlog.Text & "' and c.status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                grdcancel.Rows.Add(dr("logcancelid"), "LogSheet", dr("logsheetnum"), dr("palletnum"), dr("letter"), dr("cticketnum"), Format(dr("cticketdate"), "yyyy/MM/dd HH:mm"), dr("grossw"), dr("remarks"), dr("tickettype").ToString, dr("whsechecker"))
            End While
            dr.Dispose()
            cmd.Dispose()

            sql = "Select * from tbloflogcancel where palletnum='" & txtpallet.Text & "' and logsheetnum='" & txtlog.Text & "' and tbloflogcancel.status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                grdcancel.Rows.Add(dr("oflogcancelid"), "OrderFill", dr("logsheetnum"), dr("palletnum"), dr("letter"), dr("cticketnum"), Format(dr("cticketdate"), "yyyy/MM/dd HH:mm"), 0, dr("remarks"), dr("tickettype").ToString, dr("cancelby").ToString)
            End While
            dr.Dispose()
            cmd.Dispose()

            sql = "Select * from tblticketcancel where palletnum='" & txtpallet.Text & "' and logsheetnum='" & txtlog.Text & "' and status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                grdcancel.Rows.Add(dr("tickcancelid"), "Others", dr("logsheetnum"), dr("palletnum"), dr("letter"), dr("cticketnum"), Format(dr("cticketdate"), "yyyy/MM/dd HH:mm"), 0, dr("remarks"), dr("tickettype").ToString, dr("cancelby"))
            End While
            dr.Dispose()
            cmd.Dispose()

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

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                'check if cancel
                sql = "Select * from tbllogticket where palletnum='" & txtpallet.Text & "' and status='3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Pallet tag# " & txtpallet.Text & " is already cancelled.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if yung status nya is not selected
                Dim reserb As String = "", ofnum As String = ""
                sql = "Select t.qadispo, t.cusreserve, t.customer, t.ofnum, Count(g.loggoodid)+count(d.logdoubleid) as avtickets"
                sql = sql & " From tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid "
                sql = sql & " Left outer join tblloggood g on g.logticketid=t.logticketid left outer join tbllogdouble d on d.logticketid=t.logticketid"
                sql = sql & " where t.logticketid='" & lbllogticketid.Text & "' and s.branch='" & login.branch & "' group by t.qadispo, t.cusreserve, t.customer, t.ofnum"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("qadispo") <> 1 Then
                        MsgBox("Pallet Tag# " & txtpallet.Tag & " is pending for QA dispo.", MsgBoxStyle.Exclamation, "")
                        txtpallet.Focus()
                        Exit Sub
                    End If
                    If dr("cusreserve") = 0 Then
                        'not reserved''' available
                        reserb = "Available"
                    ElseIf dr("cusreserve") = 1 Then
                        'reserved
                        reserb = "Reserved"
                        MsgBox("Pallet Tag# " & txtpallet.Tag & " is " & reserb & " for Customer " & dr("customer").ToString & ".", MsgBoxStyle.Exclamation, "")
                        txtpallet.Focus()
                        Exit Sub
                    ElseIf dr("cusreserve") = 2 Then
                        'selected in orderfill
                        reserb = "Selected"
                        ofnum = dr("ofnum").ToString
                        MsgBox("Pallet Tag# " & txtpallet.Tag & " is " & reserb & " for Orderfill# " & ofnum & ".", MsgBoxStyle.Exclamation, "")
                        txtpallet.Focus()
                        Exit Sub
                    End If

                    If dr("avtickets") = 0 Then
                        MsgBox("There is no available tickets.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if complete yung details
                If Trim(txtletter.Text) = "" Or Trim(txtcancel.Text) = "" Or Trim(txtreason.Text) = "" Then
                    MsgBox("Complete the fields.", MsgBoxStyle.Information, "")
                    txtcancel.Focus()
                    Exit Sub
                End If

                Dim avail As Boolean = False, ticketid As Integer, tbl As String = "", ttype As String = ""
                If reserb = "Available" Then
                    'check if available pa ung ticket na yun sa tblloggood or tbllogdouble
                    sql = "Select * from tblloggood where palletnum='" & txtpallet.Text & "' and logsheetnum='" & txtlog.Text & "' and letter='" & Trim(txtletter.Text) & "' and gticketnum='" & Trim(txtcancel.Text) & "' and status<>'3' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        'check if status=1
                        If dr("status") = 1 Then
                            ticketid = dr("loggoodid")
                            tbl = "good"
                            ttype = dr("tickettype")
                            avail = True
                        Else
                            MsgBox("Ticket# is already out in loading.", MsgBoxStyle.Exclamation, "")
                            txtcancel.Focus()
                            Exit Sub
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    sql = "Select * from tbllogdouble where palletnum='" & txtpallet.Text & "' and logsheetnum='" & txtlog.Text & "' and letter='" & Trim(txtletter.Text) & "' and dticketnum='" & Trim(txtcancel.Text) & "' and status<>'3' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        'check if status=1
                        If dr("status") = 1 Then
                            ticketid = dr("logdoubleid")
                            tbl = "double"
                            ttype = dr("tickettype")
                            avail = True
                        Else
                            MsgBox("Ticket# is already out in loading.", MsgBoxStyle.Exclamation, "")
                            txtcancel.Focus()
                            Exit Sub
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If

                If avail = False Then
                    'check sa grdcancel if na cancel na to
                    For Each row As DataGridViewRow In grdcancel.Rows
                        Dim letter As String = grdcancel.Rows(row.Index).Cells(4).Value
                        Dim ticket As String = grdcancel.Rows(row.Index).Cells(5).Value

                        If letter = Trim(txtletter.Text) And ticket = Trim(txtcancel.Text) Then
                            MsgBox("Ticket# is already cancelled.", MsgBoxStyle.Exclamation, "")
                            txtcancel.Focus()
                            Exit Sub
                        End If
                    Next

                    MsgBox("Cannot found ticket# in pallet tag#.", MsgBoxStyle.Exclamation, "")
                    txtcancel.Focus()
                    Exit Sub
                End If

                palsumcnlcnf = False
                confirm.GroupBox1.Text = login.wgroup
                confirm.ShowDialog()
                If palsumcnlcnf = True Then
                    If tbl = "good" Then
                        'update tblloggood where ticketnum
                        sql = "Update tblloggood set status='0' where loggoodid='" & ticketid & "' and branch='" & login.branch & "'"
                        conn.Open()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                    ElseIf tbl = "double" Then
                        'update tbllogdouble where ticketnum
                        sql = "Update tbllogdouble set status='0' where logdoubleid='" & ticketid & "' and branch='" & login.branch & "'"
                        conn.Open()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    End If

                    sql = "Insert into tblticketcancel (logsheetnum, palletnum, ticketid, tickettype, letter, cticketnum, cticketdate, remarks, cancelby, status)"
                    sql = sql & " values ('" & txtlog.Text & "', '" & txtpallet.Text & "', '" & ticketid & "', '" & ttype & "', '" & Trim(txtletter.Text) & "', '" & Trim(txtcancel.Text) & "', GetDate(), '" & Trim(txtreason.Text) & "', '" & login.fullneym & "', '1')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully cancelled.", MsgBoxStyle.Information, "")
                    txtcancel.Text = ""
                    txtreason.Text = ""
                    txtcancel.Focus()
                    viewcancels()
                End If
            Else
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
            End If

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

    Private Sub txtletter_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtletter.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/btnadd.PerformClick()
        ElseIf (Asc(e.KeyChar) >= 65 And Asc(e.KeyChar) <= 90) Or (Asc(e.KeyChar) >= 97 And Asc(e.KeyChar) <= 122) Or Asc(e.KeyChar) = 8 Then
            If txtletter.Text.ToString.Length = 1 And Not Asc(e.KeyChar) = 8 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtletter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtletter.TextChanged

    End Sub

    Private Sub txtcancel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcancel.KeyPress
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnadd.PerformClick()
        ElseIf Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 68 Or Asc(e.KeyChar) = 100 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtcancel_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcancel.TextChanged

    End Sub

    Private Sub txtreason_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtreason.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnadd.PerformClick()
        End If
    End Sub

    Private Sub txtreason_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtreason.TextChanged

    End Sub

    Private Sub btncnlall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncnlall.Click
        Try
            If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                'check if cancel
                sql = "Select t.palletnum from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid where t.palletnum='" & txtpallet.Text & "' and t.status='3' and s.branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Pallet tag# " & txtpallet.Text & " is already cancelled.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if yung status nya is not selected
                Dim reserb As String = "", ofnum As String = ""
                'sql = "Select t.* from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid where t.logticketid='" & lbllogticketid.Text & "' and s.branch='" & login.branch & "'"
                sql = "Select t.qadispo, t.cusreserve, t.customer, t.ofnum, Count(g.loggoodid)+count(d.logdoubleid) as avtickets"
                sql = sql & " From tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid "
                sql = sql & " Left outer join tblloggood g on g.logticketid=t.logticketid left outer join tbllogdouble d on d.logticketid=t.logticketid"
                sql = sql & " where t.logticketid='" & lbllogticketid.Text & "' and s.branch='" & login.branch & "' group by t.qadispo, t.cusreserve, t.customer, t.ofnum"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("qadispo") <> 1 Then
                        MsgBox("Pallet Tag# " & txtpallet.Tag & " is pending for QA dispo.", MsgBoxStyle.Exclamation, "")
                        txtpallet.Focus()
                        Exit Sub
                    End If
                    If dr("cusreserve") = 0 Then
                        'not reserved''' available
                        reserb = "Available"
                    ElseIf dr("cusreserve") = 1 Then
                        'reserved
                        reserb = "Reserved"
                        MsgBox("Pallet Tag# " & txtpallet.Tag & " is " & reserb & " for Customer " & dr("customer").ToString & ".", MsgBoxStyle.Exclamation, "")
                        txtpallet.Focus()
                        Exit Sub
                    ElseIf dr("cusreserve") = 2 Then
                        'selected in orderfill
                        reserb = "Selected"
                        ofnum = dr("ofnum").ToString
                        MsgBox("Pallet Tag# " & txtpallet.Tag & " is " & reserb & " for Orderfill# " & ofnum & ".", MsgBoxStyle.Exclamation, "")
                        txtpallet.Focus()
                        Exit Sub
                    End If

                    If dr("avtickets") = 0 Then
                        MsgBox("There is no available tickets.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if complete yung details
                If Trim(txtreason.Text) = "" Then
                    txtletter.Text = ""
                    txtcancel.Text = ""
                    MsgBox("Input disposition first.", MsgBoxStyle.Information, "")
                    txtreason.Focus()
                    Exit Sub
                End If

                palsumcnlcnf = False
                confirm.GroupBox1.Text = login.wgroup
                confirm.ShowDialog()
                If palsumcnlcnf = True Then
                    ExecuteCancelAll(strconn)
                End If
            Else
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
            End If

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


    Private Sub ExecuteCancelAll(ByVal connectionString As String)
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
                sql = "Insert into tblticketcancel (logsheetnum, palletnum, ticketid, tickettype, letter, cticketnum, cticketdate, remarks, cancelby, status)"
                sql = sql & " Select s.logsheetnum, t.palletnum, g.loggoodid, g.tickettype, g.letter, g.gticketnum, GetDate(), '" & Trim(txtreason.Text) & "', '" & login.fullneym & "', '1'"
                sql = sql & " from tblloggood g right outer join tbllogticket t on t.logticketid=g.logticketid"
                sql = sql & " Right outer join tbllogsheet s on s.logsheetid=t.logsheetid where g.status='1' and t.palletnum='" & txtpallet.Text & "' and s.branch='" & login.branch & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'update tblloggood where ticketnum
                'sql = "Update tblloggood set status='0' where status='1' and palletnum='" & txtpallet.Text & "' and branch='" & login.branch & "'"
                sql = "Update tblloggood set status='0' where status='1' and "
                sql = sql & " logticketid =(Select logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                sql = sql & " where t.palletnum='" & txtpallet.Text & "' and s.branch='" & login.branch & "')"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Insert into tblticketcancel (logsheetnum, palletnum, ticketid, tickettype, letter, cticketnum, cticketdate, remarks, cancelby, status)"
                sql = sql & " Select s.logsheetnum, t.palletnum, d.logdoubleid, d.tickettype, d.letter, d.dticketnum, GetDate(), '" & Trim(txtreason.Text) & "', '" & login.fullneym & "', '1'"
                sql = sql & " From tbllogdouble d right outer join tbllogticket t on t.logticketid=d.logticketid"
                sql = sql & " Right outer join tbllogsheet s on s.logsheetid=t.logsheetid where d.status='1' and t.palletnum='" & txtpallet.Text & "' and s.branch='" & login.branch & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'update tbllogdouble where ticketnum
                sql = "Update tbllogdouble set status='0' where status='1' and "
                sql = sql & " logticketid =(Select logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                sql = sql & " where t.palletnum='" & txtpallet.Text & "' and s.branch='" & login.branch & "')"
                command.CommandText = sql
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully cancelled.", MsgBoxStyle.Information, "")
                txtcancel.Text = ""
                txtreason.Text = ""
                txtcancel.Focus()
                viewcancels()

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

    Public Sub count()
        Try
            lblcount.Text = "     Selected Rows Count: " & grdcancel.SelectedRows.Count
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdcancel_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcancel.CellContentClick

    End Sub

    Private Sub grdcancel_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdcancel.SelectionChanged
        count()
    End Sub
End Class