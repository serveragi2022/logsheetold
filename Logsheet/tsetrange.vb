Imports System.IO
Imports System.Data.SqlClient

Public Class tsetrange
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public tsetrcnf As Boolean

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

    Private Sub tsetrange_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        lbllognum.Text = ""
        lblline.Text = ""
    End Sub

    Private Sub tsetrange_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If login.depart = "Production" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            If ticket.btnlogconfirm.Text = "Confirmed" Or ticket.btnlogcnfitems.Enabled = False Then
                btnadd.Enabled = False
                btndeactivate.Enabled = False
                btncancel.Enabled = False
                grdrange.Enabled = False
            Else
                btnadd.Enabled = True
                btndeactivate.Enabled = True
                btncancel.Enabled = True
                grdrange.Enabled = True
            End If
        Else
            btnadd.Enabled = False
            btndeactivate.Enabled = False
            btncancel.Enabled = False
            grdrange.Enabled = False
        End If

        view()
    End Sub

    Public Sub view()
        Try
            grdrange.Rows.Clear()

            sql = "Select * from tbllogrange where logsheetid='" & lbllognum.Tag & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim stat As String = "", lefttick As Integer = 0

                lefttick = dr("arto") - dr("frto")

                If dr("status") = 1 Then
                    stat = "In Process"
                    If lefttick >= 0 And dr("frto") <> 0 Then
                        '/stat = "Completed"
                    End If
                ElseIf dr("status") = 0 Then
                    stat = "Available"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If

                grdrange.Rows.Add(dr("lograngeid"), dr("letter1"), dr("arfrom"), dr("letter2"), dr("arto"), lefttick, stat)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            sql = "Select TOP 1 * from tbllogitem where logsheetid='" & lbllognum.Tag & "' and status<>'3' order by logitemid DESC"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                lbltype.Text = dr("tickettype")
                btnadd.Enabled = True
            Else
                MsgBox("Add item first.", MsgBoxStyle.Exclamation, "")
                btnadd.Enabled = False
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

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
            If Trim(txtletter1.Text) <> "" And numfrom.Value <> 0 And numto.Value <> 0 Then
                'check if nagcutoff na si whse
                sql = "Select * from tbllogsheet where logsheetnum='" & lbllognum.Text & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 2 Then
                        MsgBox("Cannot add item. Logsheet is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    If dr("allitems") = 1 Then
                        MsgBox("Cannot add item. Warehouse already cut off.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                sql = "Select TOP 1 * from tbllogitem where logsheetid='" & lbllognum.Tag & "' and status<>'3' order by logitemid DESC"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    lbltype.Text = dr("tickettype")
                    btnadd.Enabled = True
                Else
                    MsgBox("Add item first.", MsgBoxStyle.Exclamation, "")
                    btnadd.Enabled = False
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                '/MsgBox("check sa database kung may existing na na ganung range una sa tbltickets if na sa in betweeen then next if nasa tbllogrange na sya")
                'tblloggood and tbllogcancel
                Dim ttype As String = lbltype.Text
                If ttype = "Inkjet" Or ttype = "Receive" Then
                    'sql = "Select tblloggood.logsheetid, tblloggood.gticketnum, tbllogsheet.palletizer, tbllogsheet.whsename from tbllogsheet"
                    'sql = sql & " inner join tblloggood on tbllogsheet.logsheetid=tblloggood.logsheetid"
                    'sql = sql & " where palletizer='" & ticket.lblline.Text & "' and whsename='" & ticket.lblwhse.Text & "' and tbllogsheet.branch='" & login.branch & "'"
                    'sql = sql & " and (tblloggood.logyear=CAST (YEAR(GetDate()) as nvarchar(50)) and tblloggood.tickettype='" & ttype & "' and tblloggood.letter='" & txtletter1.Text & "' and tblloggood.gticketnum>='" & numfrom.Value & "' and tblloggood.gticketnum<='" & numto.Value & "' and tblloggood.status<>'3')"

                    'sql = sql & " UNION "

                    'sql = "Select tbllogcancel.logsheetid, tbllogcancel.cticketnum, tbllogsheet.palletizer, tbllogsheet.whsename from tbllogsheet"
                    'sql = sql & " inner join tbllogcancel on tbllogsheet.logsheetid=tbllogcancel.logsheetid"
                    'sql = sql & " where palletizer='" & ticket.lblline.Text & "' and whsename='" & ticket.lblwhse.Text & "' and tbllogsheet.branch='" & login.branch & "'"
                    'sql = sql & " and (tbllogcancel.logyear=CAST (YEAR(GetDate()) as nvarchar(50)) and tbllogcancel.tickettype='" & ttype & "' and tbllogcancel.letter='" & txtletter1.Text & "' and tbllogcancel.cticketnum>='" & numfrom.Value & "' and tbllogcancel.cticketnum<='" & numto.Value & "' and tbllogcancel.status<>'3')"

                    sql = "Select s.logsheetid, g.gticketnum, s.palletizer, s.whsename"
                    sql = sql & " From tbllogsheet s"
                    sql = sql & " inner Join tblloggood g On s.logsheetid=g.logsheetid"
                    sql = sql & " where s.palletizer ='" & ticket.lblline.Text & "' and s.whsename='" & ticket.lblwhse.Text & "' and s.branch='" & login.branch & "'"
                    sql = sql & " And s.logsheetyear = CAST(Year(GetDate()) As nvarchar(50)) And (g.tickettype='" & ttype & "' "
                    sql = sql & " And g.letter='" & txtletter1.Text & "' and g.gticketnum>='" & numfrom.Value & "' "
                    sql = sql & " And g.gticketnum <='" & numto.Value & "' and g.status<>'3')"

                    sql = sql & " UNION"

                    sql = sql & " Select s.logsheetid, c.cticketnum, s.palletizer, s.whsename "
                    sql = sql & " From tbllogsheet s"
                    sql = sql & " inner Join tbllogcancel c On s.logsheetid=c.logsheetid"
                    sql = sql & " where s.palletizer ='" & ticket.lblline.Text & "' and s.whsename='" & ticket.lblwhse.Text & "' and s.branch='" & login.branch & "'"
                    sql = sql & " And s.logsheetyear = CAST(Year(GetDate()) As nvarchar(50)) And (c.tickettype='" & ttype & "' "
                    sql = sql & " And c.letter='" & txtletter1.Text & "' and c.cticketnum>='" & numfrom.Value & "' "
                    sql = sql & " And c.cticketnum <='" & numto.Value & "' and c.status<>'3')"
                Else
                    'sql = "Select tblloggood.gticketnum from tblloggood"
                    'sql = sql & " where (tblloggood.branch='" & login.branch & "' and tblloggood.logyear=CAST (YEAR(GetDate()) as nvarchar(50)) and tblloggood.tickettype='" & ttype & "' and tblloggood.letter='" & txtletter1.Text & "' and tblloggood.gticketnum>='" & numfrom.Value & "' and tblloggood.gticketnum<='" & numto.Value & "' and tblloggood.status<>'3')"

                    'sql = sql & " UNION "

                    'sql = sql & "Select tbllogcancel.cticketnum from tbllogcancel"
                    'sql = sql & " where (tbllogcancel.branch='" & login.branch & "' and tbllogcancel.logyear=CAST (YEAR(GetDate()) as nvarchar(50)) and tbllogcancel.tickettype='" & ttype & "' and tbllogcancel.letter='" & txtletter1.Text & "' and tbllogcancel.cticketnum>='" & numfrom.Value & "' and tbllogcancel.cticketnum<='" & numto.Value & "' and tbllogcancel.status<>'3')"
                    sql = "Select g.gticketnum"
                    sql = sql & " From tbllogsheet s"
                    sql = sql & " inner Join tblloggood g On s.logsheetid=g.logsheetid"
                    sql = sql & " where s.branch='" & login.branch & "'"
                    sql = sql & " And s.logsheetyear = CAST(Year(GetDate()) As nvarchar(50)) And (g.tickettype='" & ttype & "' "
                    sql = sql & " And g.letter='" & txtletter1.Text & "' and g.gticketnum>='" & numfrom.Value & "' "
                    sql = sql & " And g.gticketnum <='" & numto.Value & "' and g.status<>'3')"

                    sql = sql & " UNION"

                    sql = sql & " Select c.cticketnum"
                    sql = sql & " From tbllogsheet s"
                    sql = sql & " inner Join tbllogcancel c On s.logsheetid=c.logsheetid"
                    sql = sql & " where s.branch='" & login.branch & "'"
                    sql = sql & " And s.logsheetyear = CAST(Year(GetDate()) As nvarchar(50)) And (c.tickettype='" & ttype & "' "
                    sql = sql & " And c.letter='" & txtletter1.Text & "' and c.cticketnum>='" & numfrom.Value & "' "
                    sql = sql & " And c.cticketnum <='" & numto.Value & "' and c.status<>'3')"
                End If

                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.CommandTimeout = 0
                dr = cmd.ExecuteReader
                If dr.Read Then
                    Me.Cursor = Cursors.Default
                    MsgBox("Some ticket # are already exist.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                'check if nag overlap nmn ung nasa grd na tska ung balak plng i add
                For Each row As DataGridViewRow In grdrange.Rows
                    Dim nlet As String = grdrange.Rows(row.Index).Cells(1).Value
                    Dim nfrom As Integer = grdrange.Rows(row.Index).Cells(2).Value
                    Dim nto As Integer = grdrange.Rows(row.Index).Cells(4).Value

                    If numfrom.Value >= nfrom And numto.Value <= nfrom Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Some ticket # are already exist in range.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    If numfrom.Value >= nto And numto.Value <= nto Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Some ticket # are already exist in range.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                Next


                'tbllogrange
                sql = "Select s.logsheetnum, s.status, s.allitems, r.frfrom, r.frto, r.arfrom, r.arto from tbllogsheet s"
                sql = sql & " right outer join tbllogrange r on s.logsheetid=r.logsheetid"
                sql = sql & " where s.branch='" & login.branch & "' and s.logsheetyear='" & Format(Date.Now, "yyyy") & "'"
                sql = sql & " and s.whsename='" & ticket.lblwhse.Text & "' and s.palletizer='" & ticket.lblline.Text & "'"
                sql = sql & " and r.letter1='" & txtletter1.Text & "' and r.tickettype='" & lbltype.Text & "' and s.allitems<>'1'"
                sql = sql & " and s.status<>'3' order by r.lograngeid DESC"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If numfrom.Value <= dr("arfrom") And numto.Value >= dr("arfrom") Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Some ticket # are within the range of logsheet# " & dr("logsheetnum").ToString, MsgBoxStyle.Exclamation, "")
                        '/MsgBox("1")
                        Exit Sub
                    End If

                    If dr("arfrom") <= numfrom.Value And dr("arto") >= numfrom.Value Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Some ticket # are within the range of logsheet# " & dr("logsheetnum").ToString, MsgBoxStyle.Exclamation, "")
                        '/MsgBox("2")
                        Exit Sub
                    End If
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                ';check if may mas mataas na naunang ininput kesa sa nasa numfrom.value
                For i = grdrange.Rows.Count - 1 To 0 Step -1
                    If grdrange.Rows(i).Cells(1).Value = txtletter1.Text Then
                        If grdrange.Rows(i).Cells(6).Value <> "Cancelled" Then
                            If grdrange.Rows(i).Cells(4).Value > numfrom.Value Then
                                MsgBox("Invalid input. Some ticket # that are higher than " & txtletter1.Text & " " & numfrom.Value & " are already added.", MsgBoxStyle.Exclamation, "")
                                Exit Sub
                            End If
                        End If
                    End If
                Next


                tsetrcnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If tsetrcnf = True Then
                    Dim total As Integer = (numto.Value - numfrom.Value) + 1

                    sql = "Insert into tbllogrange (logsheetid, tickettype, letter1, letter2, arfrom, arto, atotal, "
                    sql = sql & " frfrom, frto, ftotal, datecreated, createdby, datemodified, modifiedby, status) "
                    sql = sql & " values ((Select logsheetid from tbllogsheet where logsheetnum='" & lbllognum.Text & "' and branch='" & login.branch & "'), "
                    sql = sql & " '" & lbltype.Text & "', '" & txtletter1.Text & "', '" & txtletter2.Text & "', '" & numfrom.Value & "', '" & numto.Value & "', "
                    sql = sql & " '" & total & "', '0', '0', '0', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '0')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully added range.", MsgBoxStyle.Information, "")

                    view()

                    txtletter1.Text = ""
                    txtletter2.Text = ""
                    numfrom.Value = 0
                    numto.Value = 0

                    MsgBox("Close Ticket Log Sheet Form, Then open again To refresh.", MsgBoxStyle.Information, "")
                End If

            Else
                MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
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

    Private Sub numfrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numfrom.ValueChanged
        numto.Minimum = numfrom.Value
        If numfrom.Value <> 0 Then
            numto.Maximum = numfrom.Value + 1999
            numto.Value = numfrom.Value + 1999
        End If
    End Sub

    Private Sub txtletter1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtletter1.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/btnadd.PerformClick()
        ElseIf (Asc(e.KeyChar) >= 65 And Asc(e.KeyChar) <= 90) Or (Asc(e.KeyChar) >= 97 And Asc(e.KeyChar) <= 122) Or Asc(e.KeyChar) = 8 Then
            If txtletter1.Text.ToString.Length = 1 And Not Asc(e.KeyChar) = 8 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtletter1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtletter1.TextChanged
        txtletter2.Text = txtletter1.Text
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        lblid.Text = ""
        txtletter1.Text = ""
        numfrom.Value = 0
        numto.Value = 0
        btnadd.Enabled = True
        btndeactivate.Enabled = True
    End Sub

    Private Sub grdrange_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdrange.CellContentClick

    End Sub

    Private Sub grdrange_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdrange.SelectionChanged
       
    End Sub

    Private Sub btndeactivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeactivate.Click
        Try
            If grdrange.SelectedCells.Count = 1 Or grdrange.SelectedRows.Count = 1 Then
                'check first if status=0 or available
                Dim statavail As Boolean = False
                sql = "Select * from tbllogrange where lograngeid='" & grdrange.Rows(grdrange.CurrentRow.Index).Cells(0).Value & "' and status='0'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    statavail = True
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If statavail = True Then
                    tsetrcnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If tsetrcnf = True Then
                        'can remove

                        sql = "Update tbllogrange set status='3', canceldate=GetDate(), cancelby='" & login.user & "' where lograngeid='" & grdrange.Rows(grdrange.CurrentRow.Index).Cells(0).Value & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Cancelled.", MsgBoxStyle.Information, "")
                        view()

                        MsgBox("Close Ticket Log Sheet Form, then open again to refresh.", MsgBoxStyle.Information, "")
                    End If
                Else
                    MsgBox("Ticket Series is in process. There is already a pallet tag that uses the ticket series.", MsgBoxStyle.Exclamation, "")
                End If
            Else
                MsgBox("Select only one.", MsgBoxStyle.Information, "")
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

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class