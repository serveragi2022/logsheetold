Imports System.IO
Imports System.Data.SqlClient

Public Class ticketlines
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public tnewcnf As Boolean = False, tsupcnf As Boolean = False, tsupby As String = ""

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

    Public Sub viewline()
        Try
            cmbline.Items.Clear()

            sql = "Select palletizer from tblpalletizer where whsename='" & Trim(cmbwhse.Text) & "' and branch='" & login.branch & "' and status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbline.Items.Add(dr("palletizer"))
            End While
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

    Public Sub viewshift()
        Try
            cmbshift.Items.Clear()

            sql = "Select * from tblshift where status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbshift.Items.Add(dr("shift"))
            End While
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

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            If login.depart <> "Production" And login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If txtlognum.Text <> "" And Trim(cmbline.Text) <> "" And Trim(cmbwhse.Text) <> "" And Trim(cmbshift.Text) <> "" And cmbsack.SelectedItem <> "" And grdline.Rows.Count <> 0 And Trim(txtthread.Text) <> "" And Trim(txtbin.Text) <> "" Then
                If login.depart = "Admin Dispatching" And (Trim(cmbline.Text) <> "LINE 4" And Trim(cmbline.Text) <> "LINE E") Then
                    MsgBox("Access denied in " & Trim(cmbline.Text) & ".", MsgBoxStyle.Critical, "")
                    cmbline.Text = ""
                    cmbline.Focus()
                    Exit Sub
                ElseIf login.depart <> "Admin Dispatching" And (Trim(cmbline.Text) = "LINE 4" Or Trim(cmbline.Text) = "LINE E") Then
                    MsgBox("Access denied in " & Trim(cmbline.Text) & ".", MsgBoxStyle.Critical, "")
                    cmbline.Text = ""
                    cmbline.Focus()
                    Exit Sub
                End If

                'If Trim(txtqcarems.Text) = "" Then
                '    MsgBox("Input QCA remarks.", MsgBoxStyle.Exclamation, "")
                '    Exit Sub
                'End If

                If GroupBox2.Enabled = True And grdrange.Rows.Count = 0 Then
                    MsgBox("Input ticket range.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                'if pending=true
                'check first if may logsheet na sa date na yun at sa shift na yun bago if insert
                Dim logshid As Integer
                sql = "Select s.logsheetid from tbllogsheet s"
                sql = sql & " right join tbllogitem on s.logsheetid=tbllogitem.logsheetid"
                sql = sql & " where s.logsheetdate=CAST(GetDate() as Date) and s.whsename='" & Trim(cmbwhse.Text) & "' and s.shift='" & Trim(cmbshift.Text) & "'"
                sql = sql & " and s.palletizer='" & Trim(cmbline.Text) & "' and s.branch='" & login.branch & "' and s.status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    Me.Cursor = Cursors.Default
                    logshid = dr("logsheetid")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if yung logsheet na pending is complete na yung mga items
                sql = "Select status from tbllogitem"
                sql = sql & " where logsheetid='" & logshid & "' and (status='0' or status='1')"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Log sheet for " & Trim(cmbwhse.Text) & " - Shift: " & Trim(cmbshift.Text) & " " & Trim(cmbline.Text) & " is still pending.", MsgBoxStyle.Exclamation, "")
                    cmbline.Text = ""
                    cmbline.Focus()
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                tnewcnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If tnewcnf = True Then
                    'password ni shift miller
                    tsupcnf = False
                    tsupby = ""
                    confirmsupprod.ShowDialog()
                    If tsupcnf = True Then
                        'new ticket log sheet
                        loadlogsheetnum()

                        ExecuteOk(strconn)
                    End If
                End If

            ElseIf Trim(cmbline.Text) = "" Then
                MsgBox("Select palletizer first.", MsgBoxStyle.Exclamation, "")
            ElseIf Trim(cmbwhse.Text) = "" Then
                MsgBox("Select Warehouse first.", MsgBoxStyle.Exclamation, "")
            ElseIf Trim(cmbshift.Text) = "" Then
                MsgBox("Select shift first.", MsgBoxStyle.Exclamation, "")
            ElseIf cmbsack.SelectedItem = "" Then
                MsgBox("Select sack type first.", MsgBoxStyle.Exclamation, "")
            ElseIf grdline.Rows.Count = 0 Then
                MsgBox("Input item name first.", MsgBoxStyle.Exclamation, "")
            ElseIf grdrange.Rows.Count = 0 And GroupBox2.Enabled = True Then
                MsgBox("Input range first.", MsgBoxStyle.Exclamation, "")
            ElseIf Trim(txtthread.Text) = "" Then
                MsgBox("Input thread color first.", MsgBoxStyle.Exclamation, "")
                'ElseIf Trim(txtqcarems.Text) = "" Then
                '    MsgBox("Input QCA remarks first.", MsgBoxStyle.Exclamation, "")
            ElseIf Trim(txtbin.Text) = "" Then
                MsgBox("Input flour bin first.", MsgBoxStyle.Exclamation, "")
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

    Private Sub ExecuteOk(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction

            ' Start a local transaction
            transaction = connection.BeginTransaction("SampleTransaction")

            ' Must assign both transaction object and connection 
            ' to Command object for a pending local transaction.
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor

                Dim lognum As String = "1", temp As String = "", tlognum As String = ""
                Dim prefix As String = ""

                'set prefix to whsecode + linecode + shiftcode
                sql = "Select whsecode from tblwhse where whsename='" & Trim(cmbwhse.Text) & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    prefix = dr("whsecode")
                End If
                dr.Dispose()

                sql = "Select palcode from tblpalletizer where palletizer='" & Trim(cmbline.Text) & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    prefix = prefix & dr("palcode")
                End If
                dr.Dispose()

                sql = "Select shiftcode from tblshift where shift='" & Trim(cmbshift.Text) & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    prefix = prefix & dr("shiftcode")
                End If
                dr.Dispose()

                Dim yr As Integer = Format(dateprod.Value, "yyyy")

                'check kung pang ilang LOGSHEET NA SA YEAR NA 2018 na
                sql = "Select Count(logsheetid) from tbllogsheet where logsheetyear='" & yr & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                lognum = command.ExecuteScalar + 1

                If lognum < 1000000 Then
                    For vv As Integer = 1 To 6 - lognum.Length
                        temp += "0"
                    Next
                    'lbltrnum.Text = Date.Now.Year & "-" & Format(Date.Now, "MM") & Format(Date.Now, "dd") & temp & trnum
                End If

                tlognum = "L." & prefix & "-" & Format(dateprod.Value, "yy") & "-" & temp & lognum
                txtlognum.Text = tlognum

                'check if may ganito ng logsheetnum then exit sub
                sql = "Select status from tbllogsheet where logsheetnum='" & txtlognum.Text & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    Exit Sub
                End If
                dr.Dispose()

                Dim weyt As Integer = 0
                If chkwait.Checked = True Then
                    weyt = 1
                End If

                'insert to tbllogsheet
                'check if shift c and time is 12am to 5:59am
                If Trim(cmbline.Text) = "LINE 4" Or Trim(cmbline.Text) = "LINE E" Then
                    'user can decide the production date
                    sql = "Insert into tbllogsheet (logsheetyear, logsheetdate, logsheetnum, whsename, palletizer, shift, thread, bagtype, remarks, prodrems, printtype, allitems, millersup, datecreated, createdby, datemodified, modifiedby, status, branch, weyt, binnum)"
                    sql = sql & " values ('" & dateprod.Value.Year & "', '" & dateprod.Value.Date & "', '" & txtlognum.Text & "', '" & Trim(cmbwhse.Text) & "', '" & Trim(cmbline.Text) & "', '" & Trim(cmbshift.Text) & "', '" & Trim(txtthread.Text) & "', '" & cmbsack.SelectedItem & "', '', '" & Trim(txtprodrems.Text) & "', '" & Trim(lbltype.Text) & "', '0', '" & tsupby & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '0', '" & login.branch & "','" & weyt & "','" & Trim(txtbin.Text) & "')"

                Else
                    'user cannot decide the production date
                    If Trim(cmbshift.Text) = "C" Or Trim(cmbshift.Text) = "3" Then
                        Dim datenow As Date
                        sql = "Select GETDATE()"
                        command.CommandText = sql
                        datenow = command.ExecuteScalar

                        If datenow >= Convert.ToDateTime(datenow.ToShortDateString & " " & #12:00:01 AM#) Then
                            If datenow < Convert.ToDateTime(datenow.ToShortDateString & " " & #6:00:00 AM#) Then
                                MsgBox("Production date automatically adjust to yesterday's date.", MsgBoxStyle.Information, "")
                                Dim yesterdate As Date
                                sql = "Select DATEADD(day,-1,GETDATE())"
                                command.CommandText = sql
                                yesterdate = command.ExecuteScalar

                                sql = "Insert into tbllogsheet (logsheetyear, logsheetdate, logsheetnum, whsename, palletizer, shift, thread, bagtype, remarks, prodrems, printtype, allitems, millersup, datecreated, createdby, datemodified, modifiedby, status, branch, weyt, binnum)"
                                sql = sql & " values ('" & yesterdate.Year & "', '" & yesterdate & "', '" & txtlognum.Text & "', '" & Trim(cmbwhse.Text) & "', '" & Trim(cmbline.Text) & "', '" & Trim(cmbshift.Text) & "', '" & Trim(txtthread.Text) & "', '" & cmbsack.SelectedItem & "', '', '" & Trim(txtprodrems.Text) & "', '" & Trim(lbltype.Text) & "', '0', '" & tsupby & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '0', '" & login.branch & "','" & weyt & "','" & Trim(txtbin.Text) & "')"
                            Else
                                '/MessageBox.Show("todays date")
                                sql = "Insert into tbllogsheet (logsheetyear, logsheetdate, logsheetnum, whsename, palletizer, shift, thread, bagtype, remarks, prodrems, printtype, allitems, millersup, datecreated, createdby, datemodified, modifiedby, status, branch, weyt, binnum)"
                                sql = sql & " values (YEAR(GetDate()), GetDate(), '" & txtlognum.Text & "', '" & Trim(cmbwhse.Text) & "', '" & Trim(cmbline.Text) & "', '" & Trim(cmbshift.Text) & "', '" & Trim(txtthread.Text) & "', '" & cmbsack.SelectedItem & "', '', '" & Trim(txtprodrems.Text) & "', '" & Trim(lbltype.Text) & "', '0', '" & tsupby & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '0', '" & login.branch & "','" & weyt & "','" & Trim(txtbin.Text) & "')"
                            End If
                        Else
                            '/MessageBox.Show("today")
                            sql = "Insert into tbllogsheet (logsheetyear, logsheetdate, logsheetnum, whsename, palletizer, shift, thread, bagtype, remarks, prodrems, printtype, allitems, millersup, datecreated, createdby, datemodified, modifiedby, status, branch, weyt, binnum)"
                            sql = sql & " values (YEAR(GetDate()), GetDate(), '" & txtlognum.Text & "', '" & Trim(cmbwhse.Text) & "', '" & Trim(cmbline.Text) & "', '" & Trim(cmbshift.Text) & "', '" & Trim(txtthread.Text) & "', '" & cmbsack.SelectedItem & "', '', '" & Trim(txtprodrems.Text) & "', '" & Trim(lbltype.Text) & "', '0', '" & tsupby & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '0', '" & login.branch & "','" & weyt & "','" & Trim(txtbin.Text) & "')"
                        End If
                    Else
                        '/MessageBox.Show("not c")
                        sql = "Insert into tbllogsheet (logsheetyear, logsheetdate, logsheetnum, whsename, palletizer, shift, thread, bagtype, remarks, prodrems, printtype, allitems, millersup, datecreated, createdby, datemodified, modifiedby, status, branch, weyt, binnum)"
                        sql = sql & " values (YEAR(GetDate()), GetDate(), '" & txtlognum.Text & "', '" & Trim(cmbwhse.Text) & "', '" & Trim(cmbline.Text) & "', '" & Trim(cmbshift.Text) & "', '" & Trim(txtthread.Text) & "', '" & cmbsack.SelectedItem & "', '', '" & Trim(txtprodrems.Text) & "', '" & Trim(lbltype.Text) & "', '0', '" & tsupby & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '0', '" & login.branch & "','" & weyt & "','" & Trim(txtbin.Text) & "')"
                    End If
                End If

                command.CommandText = sql
                command.ExecuteNonQuery()

                'insert dun sa grd tska sa tbllogitem
                '/For Each row As DataGridViewRow In grdline.Rows
                Dim iitem As String = grdline.Rows(0).Cells(0).Value
                Dim itype As String = grdline.Rows(0).Cells(1).Value
                sql = "Insert into tbllogitem (logsheetid, itemname, tickettype, datecreated, createdby, datemodified, modifiedby, status)"
                sql = sql & " values ((Select logsheetid from tbllogsheet where logsheetnum='" & txtlognum.Text & "' and branch='" & login.branch & "'),"
                sql = sql & " '" & iitem & "', '" & itype & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1')"
                command.CommandText = sql
                command.ExecuteNonQuery()
                '/Next

                'insert dun sa tbllogrange
                For Each row As DataGridViewRow In grdrange.Rows
                    Dim letter1 As String = grdrange.Rows(row.Index).Cells(0).Value
                    Dim rfrom As String = grdrange.Rows(row.Index).Cells(1).Value
                    Dim letter2 As String = grdrange.Rows(row.Index).Cells(2).Value
                    Dim rto As String = grdrange.Rows(row.Index).Cells(3).Value
                    Dim total As String = grdrange.Rows(row.Index).Cells(4).Value

                    sql = "Insert into tbllogrange (logsheetid, tickettype, letter1, letter2, arfrom, arto, atotal, frfrom, frto, ftotal, datecreated, createdby, datemodified, modifiedby, status)"
                    sql = sql & " values ((Select logsheetid from tbllogsheet where logsheetnum='" & txtlognum.Text & "' and branch='" & login.branch & "'),"
                    sql = sql & " '" & itype & "', '" & letter1 & "', '" & letter2 & "', '" & rfrom & "', '" & rto & "', '" & total & "', '0', '0', '0', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '0')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                sql = "Insert into tbllogbin (logsheetnum, binnum, createdby, datecreated, status) values ('" & txtlognum.Text & "', '" & Trim(txtbin.Text) & "', '" & login.user & "', GetDate(), '1')"
                command.CommandText = sql
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully created. Ticket Log Sheet # " & txtlognum.Text & ".", MsgBoxStyle.Information, "")

                Me.Dispose()
                'ticket.MdiParent = mdiform
                'ticket.Show()
                'ticket.WindowState = FormWindowState.Maximized

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

    Private Sub ticketlines_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MsgBox("You cancelled creating new ticket log sheet.", MsgBoxStyle.Information, "")
        Me.Dispose()
    End Sub

    Private Sub ticketdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim datenow As Date
        sql = "Select GETDATE()"
        connect()
        cmd = New SqlCommand(sql, conn)
        datenow = cmd.ExecuteScalar
        cmd.Dispose()
        conn.Close()

        dateprod.CustomFormat = "yyyy/MM/dd"
        dateprod.MinDate = datenow.AddDays(-60)
        dateprod.MaxDate = datenow

        viewline()
        viewwhse()
        viewshift()

        If rbbran.Checked = True Then
            cmbwhse.Text = "BRAN WHSE"
            cmbwhse.Focus()
            cmbline.Focus()
            cmbwhse.Enabled = False
            cmbsack.SelectedItem = "Pollard"
            cmbsack.Enabled = False
        Else
            cmbwhse.Items.Remove("BRAN WHSE")
            cmbsack.Items.Remove("Pollard")
        End If
    End Sub

    Public Sub loadlogsheetnum()
        Try
            Dim lognum As String = "1", temp As String = "", tlognum As String = ""
            Dim prefix As String = ""

            'set prefix to whsecode + linecode + shiftcode
            sql = "Select whsecode from tblwhse where whsename='" & Trim(cmbwhse.Text) & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                prefix = dr("whsecode")
            End If
            cmd.Dispose()
            dr.Dispose()
            conn.Close()

            sql = "Select palcode from tblpalletizer where palletizer='" & Trim(cmbline.Text) & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                prefix = prefix & dr("palcode")
            End If
            cmd.Dispose()
            dr.Dispose()
            conn.Close()

            sql = "Select shiftcode from tblshift where shift='" & Trim(cmbshift.Text) & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                prefix = prefix & dr("shiftcode")
            End If
            cmd.Dispose()
            dr.Dispose()
            conn.Close()

            Dim yr As Integer = Format(dateprod.Value, "yyyy")

            'check kung pang ilang LOGSHEET NA SA YEAR NA 2018 na
            sql = "Select Count(logsheetid) from tbllogsheet where logsheetyear='" & yr & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            lognum = cmd.ExecuteScalar + 1
            cmd.Dispose()
            conn.Close()

            If lognum < 1000000 Then
                For vv As Integer = 1 To 6 - lognum.Length
                    temp += "0"
                Next
                'lbltrnum.Text = Date.Now.Year & "-" & Format(Date.Now, "MM") & Format(Date.Now, "dd") & temp & trnum
            End If

            tlognum = "L." & prefix & "-" & Format(dateprod.Value, "yy") & "-" & temp & lognum
            txtlognum.Text = tlognum

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

    Private Sub cmbline_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbline.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbline_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbline.Leave
        If Trim(cmbline.Text) <> "" Then
            sql = "Select * from tblpalletizer where whsename='" & Trim(cmbwhse.Text) & "' and palletizer='" & Trim(cmbline.Text) & "' and branch='" & login.branch & "' and status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                cmbline.Text = dr("palletizer")
            Else
                MsgBox("Invalid palletizer name.", MsgBoxStyle.Exclamation, "")
                cmbline.Focus()
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If login.depart = "Admin Dispatching" And (Trim(cmbline.Text) <> "LINE 4" And Trim(cmbline.Text) <> "LINE E") Then
                MsgBox("Access denied in " & Trim(cmbline.Text) & ".", MsgBoxStyle.Critical, "")
                cmbline.Text = ""
                cmbline.Focus()
                Exit Sub
            ElseIf login.depart <> "Admin Dispatching" And (Trim(cmbline.Text) = "LINE 4" Or Trim(cmbline.Text) = "LINE E") Then
                MsgBox("Access denied in " & Trim(cmbline.Text) & ". (Reworks)", MsgBoxStyle.Critical, "")
                cmbline.Text = ""
                cmbline.Focus()
                Exit Sub
            End If

            If Trim(cmbline.Text) <> "" And Trim(cmbwhse.Text) <> "" And Trim(cmbshift.Text) <> "" Then
                GroupBox1.Enabled = True
                If cmbwhse.Text <> "BRAN WHSE" Then
                    GroupBox2.Visible = True
                    GroupBox2.Enabled = True
                End If
                loadlogsheetnum()
                btnok.Enabled = True
                'viewlastticket()
                'grdrange.Rows.Clear()
                checkpending()
                checkdate()
            Else
                GroupBox1.Enabled = False
                GroupBox2.Enabled = False
                txtlognum.Text = ""
            End If


        End If
    End Sub

    Private Sub cmbline_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbline.SelectedIndexChanged
        checkdate()
    End Sub

    Public Sub viewitems()
        Try
            cmbitem.Items.Clear()

            sql = "Select itemname from tblitems where status='1'"
            If Trim(cmbwhse.Text) = "BRAN WHSE" Then
                sql = sql & " and category='By-Products'"
            Else
                sql = sql & " and category<>'By-Products'"
            End If
            sql = sql & " order by itemname"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbitem.Items.Add(dr("itemname"))
            End While
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

    Public Sub viewwhse()
        Try
            cmbwhse.Items.Clear()

            sql = "Select whsename from tblwhse where branch='" & login.branch & "' and status='1' order by whsename"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbwhse.Items.Add(dr("whsename"))
            End While
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
            If Trim(cmbitem.Text) <> "" And Trim(cmbline.Text) <> "" Then
                'check if meron ng logsheetnum and palletizer
                Dim meronna As Boolean = False
                For Each item As Object In cmbtemp.Items
                    If item = Trim(cmbitem.Text) Then
                        meronna = True
                        MsgBox(Trim(cmbitem.Text) & " is already added.", MsgBoxStyle.Exclamation, "")
                        btncancel.PerformClick()
                        Exit Sub
                    End If
                Next

                'add item one at a time
                'check if may item na
                If grdline.Rows.Count <> 0 Then
                    MsgBox("Complete the item first.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                Dim ttype As String = ""
                If rbticket.Checked = True Then
                    sql = "Select tickettype from tblitems where itemname='" & Trim(cmbitem.Text) & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        ttype = dr("tickettype")
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                ElseIf rbink.Checked = True Then
                    ttype = "Inkjet"
                ElseIf rbbran.Checked = True Then
                    ttype = "Bran"
                Else
                    ttype = "Receive"
                End If

                grdline.Rows.Add(Trim(cmbitem.Text), ttype)
                cmbtemp.Items.Add(Trim(cmbitem.Text))
                btncancel.PerformClick()
                viewlastticket()
                grdrange.Rows.Clear()

            Else
                MsgBox("Complete the required fields.", MsgBoxStyle.Exclamation, "")
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

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            If btnupdate.Text = "Update" Then
                If grdline.SelectedCells.Count = 1 Then
                    cmbitem.Text = grdline.Rows(grdline.CurrentRow.Index).Cells(0).Value
                    btnupdate.Text = "Save"
                    btnadd.Enabled = False
                    btnremove.Enabled = False
                    grdline.Enabled = False
                Else
                    MsgBox("Select one only.", MsgBoxStyle.Exclamation, "")
                End If
            Else
                If Trim(cmbitem.Text) <> "" Then
                    'hindi pwede i update if sa tbllogticket may location na kahit yung isa lang
                    Dim meronna As Boolean = False
                    For Each row As Object In grdline.Rows
                        If grdline.Rows(row.index).Cells(0).Value = Trim(cmbitem.Text) And grdline.CurrentRow.Index <> row.index Then
                            meronna = True
                            MsgBox(Trim(cmbitem.Text) & " is already added.", MsgBoxStyle.Exclamation, "")
                            btncancel.PerformClick()
                            Exit Sub
                        End If
                    Next

                    Dim ttype As String = ""
                    sql = "Select tickettype from tblitems where itemname='" & Trim(cmbitem.Text) & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        ttype = dr("tickettype")
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    grdline.Rows(grdline.CurrentRow.Index).Cells(0).Value = cmbitem.Text
                    grdline.Rows(grdline.CurrentRow.Index).Cells(1).Value = ttype

                    btnupdate.Text = "Update"
                    btnadd.Enabled = True
                    btnremove.Enabled = True
                    grdline.Enabled = True
                    btncancel.PerformClick()
                Else
                    MsgBox("Select item.", MsgBoxStyle.Exclamation, "")
                End If
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

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        cmbitem.Text = ""
        btnadd.Enabled = True
        btnupdate.Text = "Update"
        btnremove.Enabled = True
        grdline.Enabled = True
    End Sub

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        For Each row As DataGridViewRow In grdline.SelectedRows
            Dim listindex As Integer = row.Index
            grdline.Rows.Remove(row)
            cmbtemp.Items.RemoveAt(listindex)
        Next
    End Sub

    Private Sub cmbline_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbline.SelectedValueChanged
        If Trim(cmbline.Text) <> "" And Trim(cmbwhse.Text) <> "" And Trim(cmbshift.Text) <> "" Then
            GroupBox1.Enabled = True
            If cmbwhse.Text <> "BRAN WHSE" Then
                GroupBox2.Visible = True
                GroupBox2.Enabled = True
            End If
            loadlogsheetnum()
            btnok.Enabled = True
            'viewlastticket()
            checkpending()
            checkdate()
        Else
            GroupBox1.Enabled = False
            GroupBox2.Enabled = False
            txtlognum.Text = ""
        End If


    End Sub

    Private Sub btnaddr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddr.Click
        Try
            If grdline.Rows.Count = 0 Then
                MsgBox("Add item first.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            If Trim(txtletter1.Text) <> "" And numfrom.Value <> 0 And numto.Value <> 0 Then
                '/MsgBox("check sa database kung may existing na na ganung range una sa tbltickets if na sa in betweeen then next if nasa tbllogrange na sya")
                'tblloggood and tbllogcancel
                Dim ttype As String = grdline.Rows(0).Cells(1).Value
                If ttype = "Inkjet" Or ttype = "Receive" Then
                    'sql = "Select tblloggood.logsheetid, tblloggood.gticketnum, tbllogsheet.palletizer, tbllogsheet.whsename from tbllogsheet"
                    'sql = sql & " inner join tblloggood on tbllogsheet.logsheetid=tblloggood.logsheetid"
                    'sql = sql & " where palletizer='" & cmbline.Text & "' and whsename='" & cmbwhse.Text & "' and tbllogsheet.branch='" & login.branch & "'"
                    'sql = sql & " and (tblloggood.logyear=CAST (YEAR(GetDate()) as nvarchar(50)) and tblloggood.tickettype='" & ttype & "' and tblloggood.letter='" & txtletter1.Text & "' and tblloggood.gticketnum>='" & numfrom.Value & "' and tblloggood.gticketnum<='" & numto.Value & "' and tblloggood.status<>'3')"

                    'sql = sql & " UNION "

                    'sql = "Select tbllogcancel.logsheetid, tbllogcancel.cticketnum, tbllogsheet.palletizer, tbllogsheet.whsename from tbllogsheet"
                    'sql = sql & " inner join tbllogcancel on tbllogsheet.logsheetid=tbllogcancel.logsheetid"
                    'sql = sql & " where palletizer='" & cmbline.Text & "' and whsename='" & cmbwhse.Text & "' and tbllogsheet.branch='" & login.branch & "'"
                    'sql = sql & " and (tbllogcancel.logyear=CAST (YEAR(GetDate()) as nvarchar(50)) and tbllogcancel.tickettype='" & ttype & "' and tbllogcancel.letter='" & txtletter1.Text & "' and tbllogcancel.cticketnum>='" & numfrom.Value & "' and tbllogcancel.cticketnum<='" & numto.Value & "' and tbllogcancel.status<>'3')"

                    sql = "Select s.logsheetid, g.gticketnum, s.palletizer, s.whsename"
                    sql = sql & " From tbllogsheet s"
                    sql = sql & " inner Join tblloggood g On s.logsheetid=g.logsheetid"
                    sql = sql & " where s.palletizer ='" & cmbline.Text & "' and s.whsename='" & cmbwhse.Text & "' and s.branch='" & login.branch & "'"
                    sql = sql & " And s.logsheetyear = CAST(Year(GetDate()) As nvarchar(50)) And (g.tickettype='" & ttype & "' "
                    sql = sql & " And g.letter='" & txtletter1.Text & "' and g.gticketnum>='" & numfrom.Value & "' "
                    sql = sql & " And g.gticketnum <='" & numto.Value & "' and g.status<>'3')"

                    sql = sql & " UNION"

                    sql = sql & " Select s.logsheetid, c.cticketnum, s.palletizer, s.whsename "
                    sql = sql & " From tbllogsheet s"
                    sql = sql & " inner Join tbllogcancel c On s.logsheetid=c.logsheetid"
                    sql = sql & " where s.palletizer ='" & cmbline.Text & "' and s.whsename='" & cmbwhse.Text & "' and s.branch='" & login.branch & "'"
                    sql = sql & " And s.logsheetyear = CAST(Year(GetDate()) As nvarchar(50)) And (c.tickettype='" & ttype & "' "
                    sql = sql & " And c.letter='" & txtletter1.Text & "' and c.cticketnum>='" & numfrom.Value & "' "
                    sql = sql & " And c.cticketnum <='" & numto.Value & "' and c.status<>'3')"
                Else
                    'no palletizer and whsename kasi ticket ito pangkalahatan sa branch

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
                    Dim nlet As String = grdrange.Rows(row.Index).Cells(0).Value
                    Dim nfrom As Integer = grdrange.Rows(row.Index).Cells(1).Value
                    Dim nto As Integer = grdrange.Rows(row.Index).Cells(3).Value

                    If numfrom.Value >= nfrom And numto.Value <= nfrom Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Some ticket # are already exist in the list.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If

                    If numfrom.Value >= nto And numto.Value <= nto Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Some ticket # are already exist in the list.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                Next


                'tbllogrange
                'sql = "Select tbllogsheet.logsheetnum, tbllogsheet.status, tbllogsheet.allitems, tbllogrange.frfrom, tbllogrange.frto, tbllogrange.arfrom, tbllogrange.arto from tbllogsheet"
                'sql = sql & " full outer join tbllogrange on tbllogsheet.logsheetnum=tbllogrange.logsheetnum"
                'sql = sql & " where tbllogsheet.logsheetyear=YEAR(GetDate()) and tbllogsheet.branch='" & login.branch & "' and tbllogrange.whsename='" & cmbwhse.Text & "'"
                'sql = sql & " and tbllogrange.palletizer='" & cmbline.Text & "' and tbllogrange.letter1='" & txtletter1.Text & "' and tbllogrange.tickettype='" & grdline.Rows(0).Cells(1).Value & "' and tbllogsheet.allitems<>'1' and tbllogsheet.status<>'3' order by lograngeid DESC"

                sql = "Select s.logsheetnum, s.status, s.allitems, r.frfrom, r.frto, r.arfrom, r.arto"
                sql = sql & " From tbllogsheet s"
                sql = sql & " full outer join tbllogrange r On s.logsheetid=r.logsheetid"
                sql = sql & " where s.logsheetyear = Year(GetDate()) And s.branch ='" & login.branch & "'"
                sql = sql & " And s.whsename ='" & cmbwhse.Text & "'and s.palletizer='" & cmbline.Text & "'"
                sql = sql & " And r.letter1 ='" & txtletter1.Text & "' and r.tickettype='" & grdline.Rows(0).Cells(1).Value & "'"
                sql = sql & " And s.allitems <>'1' and s.status<>'3' "
                sql = sql & " order by r.lograngeid DESC"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If numfrom.Value <= dr("arfrom") And numto.Value >= dr("arfrom") Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Some ticket # are within the range Of logsheet# " & dr("logsheetnum").ToString, MsgBoxStyle.Exclamation, "")
                        'MsgBox("1")
                        Exit Sub
                    End If

                    If dr("arfrom") <= numfrom.Value And dr("arto") >= numfrom.Value Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Some ticket # are within the range Of logsheet# " & dr("logsheetnum").ToString, MsgBoxStyle.Exclamation, "")
                        'MsgBox("2")
                        Exit Sub
                    End If
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                ';check if may mas mataas na naunang ininput kesa sa nasa numfrom.value
                For i = grdrange.Rows.Count - 1 To 0 Step -1
                    If grdrange.Rows(i).Cells(2).Value = txtletter1.Text Then
                        If grdrange.Rows(i).Cells(3).Value > numfrom.Value Then
                            MsgBox("Invalid input. Some ticket # that are higher than " & txtletter1.Text & " " & numfrom.Value & " are already added.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                    End If
                Next


                Dim total As Integer = (numto.Value - numfrom.Value) + 1
                grdrange.Rows.Add(Trim(txtletter1.Text), numfrom.Value, Trim(txtletter2.Text), numto.Value, total, "In Process")
                numfrom.Value = 0
                numto.Value = 0

            ElseIf Trim(txtletter1.Text) = "" Then
                MsgBox("Input letter.", MsgBoxStyle.Exclamation, "")
            ElseIf (numfrom.Value = 0) Then
                MsgBox("Input Start Ticket.", MsgBoxStyle.Exclamation, "")
            ElseIf numto.Value = 0 Then
                MsgBox("Input End Ticket.", MsgBoxStyle.Exclamation, "")
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
            '/ numto.Minimum = numfrom.Value
            '/numto.Value = numfrom.Value + 1999
        End If
    End Sub

    Private Sub cmbitem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbitem.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbitem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitem.Leave
        Try
            If Trim(cmbitem.Text) <> "" Then
                For i = 0 To cmbitem.Items.Count - 1
                    If cmbitem.Items(0).ToString = "" And Trim(cmbitem.Text.ToUpper) <> "" Then

                    End If
                Next

                sql = "Select itemname from tblitems where itemname='" & Trim(cmbitem.Text) & "' and status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    cmbitem.Text = dr("itemname")
                Else
                    MsgBox("Invalid Item name.", MsgBoxStyle.Critical, "")
                    cmbitem.Text = ""
                    cmbitem.Focus()
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbitem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbitem.SelectedIndexChanged

    End Sub

    Private Sub txtletter_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtletter1.KeyPress
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

    Private Sub txtletter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtletter1.TextChanged
        txtletter2.Text = txtletter1.Text
    End Sub

    Private Sub btnupdater_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdater.Click
        Try
            If btnupdater.Text = "Update" Then
                If grdrange.SelectedCells.Count = 1 Then
                    txtletter1.Text = grdrange.Rows(grdrange.CurrentRow.Index).Cells(0).Value
                    numfrom.Value = grdrange.Rows(grdrange.CurrentRow.Index).Cells(1).Value
                    txtletter2.Text = grdrange.Rows(grdrange.CurrentRow.Index).Cells(2).Value
                    numto.Value = grdrange.Rows(grdrange.CurrentRow.Index).Cells(3).Value
                    btnupdater.Text = "Save"
                    btnaddr.Enabled = False
                    grdrange.Enabled = False
                Else
                    MsgBox("Select one only.", MsgBoxStyle.Exclamation, "")
                End If
            Else
                'check if may natamaang range
                If Trim(txtletter1.Text) <> "" And numfrom.Value <> 0 And numto.Value <> 0 Then
                    'MsgBox("check if may natamaang range")

                    Dim total As Integer = (numto.Value - numfrom.Value) + 1

                    grdrange.Rows(grdrange.CurrentRow.Index).Cells(0).Value = Trim(txtletter1.Text)
                    grdrange.Rows(grdrange.CurrentRow.Index).Cells(1).Value = numfrom.Value
                    grdrange.Rows(grdrange.CurrentRow.Index).Cells(2).Value = Trim(txtletter2.Text)
                    grdrange.Rows(grdrange.CurrentRow.Index).Cells(3).Value = numto.Value
                    grdrange.Rows(grdrange.CurrentRow.Index).Cells(0).Value = total
                    btnupdater.Text = "Update"
                    btnaddr.Enabled = True
                    grdrange.Enabled = True
                    btncancelr.PerformClick()
                Else
                    MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
                End If
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

    Private Sub btnremover_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremover.Click
        For Each row As DataGridViewRow In grdrange.SelectedRows
            Dim listindex As Integer = row.Index
            grdrange.Rows.Remove(row)
            '/grdcancel.SortedColumn
        Next
    End Sub

    Private Sub btncancelr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelr.Click
        txtletter1.Text = ""
        numfrom.Value = 0
        numto.Value = 0
    End Sub

    Private Sub cmbwhse_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbwhse.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbwhse_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbwhse.Leave
        If Trim(cmbwhse.Text) <> "" Then
            sql = "Select whsename from tblwhse where whsename='" & Trim(cmbwhse.Text) & "' and branch='" & login.branch & "' and status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                cmbwhse.Text = dr("whsename")
            Else
                MsgBox("Invalid warehouse name.", MsgBoxStyle.Exclamation, "")
                cmbwhse.Focus()
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            viewline()
            If Trim(cmbline.Text) <> "" And Trim(cmbwhse.Text) <> "" And Trim(cmbshift.Text) <> "" Then
                GroupBox1.Enabled = True
                If cmbwhse.Text <> "BRAN WHSE" Then
                    GroupBox2.Visible = True
                    GroupBox2.Enabled = True
                End If
                loadlogsheetnum()
                btnok.Enabled = True
                'viewlastticket()
                checkpending()
            Else
                GroupBox1.Enabled = False
                GroupBox2.Enabled = False
                txtlognum.Text = ""
            End If

            If cmbwhse.Text = "BRAN WHSE" Then
                cmbsack.SelectedItem = "Pollard"
                GroupBox2.Visible = False
            Else
                cmbsack.SelectedItem = ""
                GroupBox2.Visible = True
                GroupBox2.Enabled = True
            End If
        End If
    End Sub

    Private Sub cmbwhse_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbwhse.SelectedValueChanged
        viewline()
        viewitems()
        If Trim(cmbline.Text) <> "" And Trim(cmbwhse.Text) <> "" And Trim(cmbshift.Text) <> "" Then
            GroupBox1.Enabled = True
            GroupBox2.Enabled = True
            loadlogsheetnum()
            btnok.Enabled = True
            'viewlastticket()
            checkpending()
        Else
            GroupBox1.Enabled = False
            GroupBox2.Enabled = False
            txtlognum.Text = ""
        End If

        If cmbwhse.Text = "BRAN WHSE" Then
            cmbsack.SelectedItem = "Pollard"
            GroupBox2.Visible = False
        Else
            cmbsack.SelectedItem = ""
            GroupBox2.Visible = True
            GroupBox2.Enabled = True
        End If
    End Sub

    Public Sub viewlastticket()
        Try
            If Trim(cmbline.Text) <> "" And Trim(cmbwhse.Text) <> "" And Trim(cmbshift.Text) <> "" Then
                'check muna last na logsheet na ang whse at palletizer na ung status kung completed na or admin confirmation nlng ang pending to proceed
                'kunin ung last na ticket
                Dim lastticket As Integer, rangeid As Integer, lognumid As Integer

                sql = "Select allitems,logsheetid from tbllogsheet where branch='" & login.branch & "' and whsename='" & Trim(cmbwhse.Text) & "'"
                sql = sql & " and palletizer='" & Trim(cmbline.Text) & "' and tbllogsheet.printtype='" & lbltype.Text & "' and status<>'3'"
                sql = sql & " order by logsheetid DESC"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("allitems") = 1 Then
                        lognumid = dr("logsheetid")
                    Else
                        'pending
                        cmbline.Text = ""
                        MsgBox("There is a pending logsheet. Cannot create.", MsgBoxStyle.Exclamation, "")
                        btnok.Enabled = False
                    End If
                Else
                    btnok.Enabled = True
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                '/MsgBox(lognum)

                sql = "Select TOP 1 * from tbllogrange where logsheetid='" & lognumid & "' order by lograngeid DESC"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    lastticket = dr("frto")
                    txtletter1.Text = dr("letter2")
                    numfrom.Value = lastticket + 1
                    If numfrom.Value < dr("arto") Then
                        numto.Value = dr("arto")
                    End If

                    If numto.Value <> dr("arto") Then
                        numfrom.Value = 0
                        numto.Value = 0
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
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

    Private Sub cmbwhse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbwhse.SelectedIndexChanged

    End Sub

    Private Sub numto_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numto.ValueChanged

    End Sub

    Private Sub txtprodrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtprodrems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtprodrems_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprodrems.TextChanged

    End Sub

    Private Sub txtqcarems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtqcarems_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmbshift_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbshift.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbshift_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbshift.Leave
        If Trim(cmbshift.Text) <> "" Then
            sql = "Select shift from tblshift where shift='" & Trim(cmbshift.Text) & "' and status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                cmbshift.Text = dr("shift")
            Else
                MsgBox("Invalid shift name.", MsgBoxStyle.Exclamation, "")
                cmbshift.Focus()
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If Trim(cmbline.Text) <> "" And Trim(cmbwhse.Text) <> "" And Trim(cmbshift.Text) <> "" Then
                GroupBox1.Enabled = True
                If cmbwhse.Text <> "BRAN WHSE" Then
                    GroupBox2.Visible = True
                    GroupBox2.Enabled = True
                End If
                loadlogsheetnum()
                btnok.Enabled = True
                'viewlastticket()
                checkpending()
                checkdate()
            Else
                GroupBox1.Enabled = False
                GroupBox2.Enabled = False
                txtlognum.Text = ""
            End If
        End If
    End Sub

    Private Sub cmbshift_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbshift.SelectedIndexChanged

    End Sub

    Private Sub txtthread_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtthread.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtthread_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtthread.TextChanged

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim datenow As Date
        sql = "Select GETDATE()"
        connect()
        cmd = New SqlCommand(sql, conn)
        datenow = cmd.ExecuteScalar
        cmd.Dispose()
        conn.Close()

        If datenow >= Convert.ToDateTime(datenow.ToShortDateString & " " & #12:00:01 AM#) Then
            If datenow < Convert.ToDateTime(datenow.ToShortDateString & " " & #6:00:00 AM#) Then
                MsgBox("adjust to yesterday's date")
            Else
                MsgBox("todays date")
            End If
        Else
            MsgBox("today")
        End If
    End Sub

    Private Sub rbbran_CheckedChanged(sender As Object, e As EventArgs) Handles rbbran.CheckedChanged

    End Sub

    Public Sub checkpending()
        Try
            sql = "Select logsheetnum from tbllogsheet where branch='" & login.branch & "' and whsename='" & Trim(cmbwhse.Text) & "'"
            sql = sql & " and palletizer='" & Trim(cmbline.Text) & "' and tbllogsheet.printtype='" & lbltype.Text & "' and status<>'3'"
            sql = sql & " and allitems='0' order by logsheetid DESC"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                'pending
                cmbline.Text = ""
                MsgBox("There is a pending logsheet. Cannot create.", MsgBoxStyle.Exclamation, "")
                btnok.Enabled = False
            Else
                btnok.Enabled = True
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

    Public Sub checkdate()
        Try
            If Trim(cmbline.Text) = "LINE 4" Or Trim(cmbline.Text) = "LINE E" Then
                dateprod.Enabled = True
            Else
                dateprod.Enabled = False

                Dim datenow As Date
                sql = "Select GETDATE()"
                connect()
                cmd = New SqlCommand(sql, conn)
                datenow = cmd.ExecuteScalar
                cmd.Dispose()
                conn.Close()

                If Trim(cmbshift.Text) = "C" Or Trim(cmbshift.Text) = "3" Then
                    If datenow >= Convert.ToDateTime(datenow.ToShortDateString & " " & #12:00:01 AM#) Then
                        If datenow < Convert.ToDateTime(datenow.ToShortDateString & " " & #6:00:00 AM#) Then
                            '/MsgBox("adjust to yesterday's date")
                            dateprod.Value = datenow.AddDays(-1)
                        Else
                            '/MsgBox("todays date")
                            dateprod.Value = datenow.Date
                        End If
                    Else
                        '/MsgBox("today")
                        dateprod.Value = datenow.Date
                    End If
                Else
                    dateprod.Value = datenow.Date
                End If
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

End Class