Imports System.IO
Imports System.Data.SqlClient

Public Class tickethourly
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String, lbldt As Date, dcreated As Date, timercount As Integer = 0, weyttime As Integer = 0
    Dim str As String, eqw As Boolean
    Dim minval As Double
    Dim maxval As Double
    Public sacktype As String

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

    Private Sub tickethourly_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        defaultform()
    End Sub

    Private Sub tickethourly_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
    End Sub

    Public Sub defaultform()
        txtlogsheet.Text = ""
        txtwhse.Text = ""
        txtline.Text = ""
        txtitem.Text = ""
        lbllogitemid.Text = ""
        txtgross.Text = ""
        txtremarks.Text = ""
        grdhour.Rows.Clear()
        Timer1.Enabled = False
        Timer1.Stop()
        Timer2.Enabled = False
        Timer2.Stop()
    End Sub

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        tickethourselect.ShowDialog()
        showhours(strconn, txtlogsheet.Text)

        If Trim(txtlogsheet.Text) <> "" Then
            sql = "Select GetDate()"
            connect()
            cmd = New SqlCommand(sql, conn)
            lbldt = cmd.ExecuteScalar
            cmd.Dispose()
            conn.Close()

            lblnum.Text = checknum()
            lblhour.Text = hourname(lblnum.Text)
            hourrange(lblnum.Text)

            Timer1.Enabled = True
            Timer1.Start()
            Timer2.Enabled = True
            Timer2.Start()

            '/chkwait.Checked = True
            txtgross.ReadOnly = True
            btnreset.Enabled = True

            weyttime = 0

            If sacktype = "" Then
                minval = 25.06
                maxval = 25.27
            Else
                sql = "Select * from tblbag where bagtype='" & sacktype & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    minval = dr("minimum")
                    maxval = dr("maximum")
                    weyttime = dr("tym")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            End If


            '/If chkwait.Checked = False Then
            '/txtgross.Enabled = False
            '/Else
            'overwrite sa rs232 txt into zero
            If IO.File.Exists(Application.StartupPath() & "\RS232Capture.txt") Then
                Dim file As System.IO.StreamWriter
                file = My.Computer.FileSystem.OpenTextFileWriter((Application.StartupPath() & "\RS232Capture.txt"), False)
                file.WriteLine("0.00")
                file.Close()
            Else
                System.IO.File.CreateText(Application.StartupPath() & "\RS232Capture.txt")
                Dim file As System.IO.StreamWriter
                file = My.Computer.FileSystem.OpenTextFileWriter((Application.StartupPath() & "\RS232Capture.txt"), False)
                file.WriteLine("0.00")
                file.Close()
            End If

            Timer3.Start()
            txtgross.BackColor = Color.White
            timercount = weyttime
            '/End If
        End If
    End Sub

    Private Sub showhours(ByVal connectionString As String, ByVal logsheetnum As String)
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
                If logsheetnum <> "" Then
                    grdhour.Rows.Clear()

                    sql = "Select datecreated from tbllogsheet where logsheetnum='" & txtlogsheet.Text & "' and status=1"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    If dr.Read Then
                        dcreated = dr("datecreated")
                    End If
                    dr.Dispose()

                    Dim colnum As Integer, i = 0
                    sql = "Select tblloghour.num,tblloghour.dfrom,tblloghour.dto, tblloghourwt.* from tblloghour inner join tblloghourwt on tblloghour.num=tblloghourwt.num"
                    sql = sql & " where tblloghour.logsheetnum='" & txtlogsheet.Text & "' and tblloghourwt.logsheetnum='" & txtlogsheet.Text & "' and status='1' order by tblloghourwt.num"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    While dr.Read
                        If colnum <> dr("num") Then
                            Dim rr As Integer = 0
                            If dr("num") <> grdhour.Rows.Count + 1 Then
                                rr = dr("num") - (grdhour.Rows.Count + 1)
                                For i = 0 To rr - 1
                                    grdhour.Rows.Add()
                                    grdhour.Item(0, grdhour.Rows.Count - 1).Value = grdhour.Rows.Count
                                Next
                            End If

                            i = 0
                            colnum = dr("num")
                            grdhour.Rows.Add()
                            grdhour.Item(0, dr("num") - 1).Value = dr("num")
                            i += 1
                            grdhour.Item(i, dr("num") - 1).Value = dr("dfrom").ToString
                            i += 1
                            grdhour.Item(i, dr("num") - 1).Value = dr("dto").ToString
                            i += 1
                            grdhour.Item(i, dr("num") - 1).Value = dr("netw")
                        Else
                            grdhour.Item(i, dr("num") - 1).Value = dr("netw")
                        End If

                        i += 1
                    End While
                    dr.Dispose()

                    sql = "Select * from tblloghourwt where logsheetnum='" & txtlogsheet.Text & "'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    While dr.Read
                        '/grdhour.Rows.Add(dr("num"), dr("netw1"), dr("netw2"), dr("netw3"), dr("netw4"), dr("netw5"))
                    End While
                    dr.Dispose()
                End If

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

    Private Function checknum() As Integer
        Dim idlec As TimeSpan

        If Trim(lbldt.ToString) <> "" And Trim(dcreated.ToString) <> "" Then
            idlec = lbldt - dcreated
            '/MsgBox(idlec.TotalHours.ToString)
            checknum = Fix(Val(idlec.TotalHours.ToString)) + 1
        End If

        Return checknum
    End Function

    Private Function hourname(ByVal num As Integer) As String
        If num = 1 Then
            hourname = "1ST HOUR"
        ElseIf num = 2 Then
            hourname = "2ND HOUR"
        ElseIf num = 3 Then
            hourname = "3RD HOUR"
        Else
            hourname = num & "TH HOUR"
        End If

        Return hourname
    End Function

    Private Function hourrange(ByVal num As Integer) As Date
        num = num - 1
        If num = 0 Then
            lblfrom.Text = dcreated
            lblto.Text = dcreated.AddHours(+1)
        Else
            lblfrom.Text = dcreated.AddHours(+num).AddSeconds(+1)
            lblto.Text = dcreated.AddHours(+1 + num)
        End If
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lbldt = lbldt.AddSeconds(+1)
        lblserverdt.Text = lbldt
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        lblnum.Text = checknum()
        Dim hr As String = hourname(lblnum.Text)
        If hr <> lblhour.Text Then
            Timer2.Stop()
            lblhour.Text = hr
            hourrange(lblnum.Text)
            MsgBox(hr, MsgBoxStyle.Information, "")
            Timer2.Start()
        End If
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If Val(lblnum.Text) > 24 Then
                MsgBox("Invalid.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            'check if cut off na allitems=1
            sql = "Select allitems from tbllogsheet where logsheetnum='" & txtlogsheet.Text & "' and status='1' and allitems='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MsgBox("Logsheet is already cut off.", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If Val(Trim(txtgross.Text)) <> 0 Then
                Timer3.Stop()

                Dim meronna As Integer = 0
                sql = "Select count(hourid) from tblloghour where logsheetnum='" & txtlogsheet.Text & "' and num='" & lblnum.Text & "' and status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                meronna = cmd.ExecuteScalar
                cmd.Dispose()
                conn.Close()

                If meronna = 0 Then
                    sql = "Insert into tblloghour (logsheetnum, num, dfrom, dto, datecreated, createdby, datemodified, modifiedby, status)"
                    sql = sql & " values('" & txtlogsheet.Text & "','" & lblnum.Text & "','" & lblfrom.Text & "','" & lblto.Text & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                End If

                sql = "Insert into tblloghourwt (logsheetnum, num, netw, rems, datecreated, createdby)"
                sql = sql & " values ('" & txtlogsheet.Text & "','" & Val(lblnum.Text) & "','" & Val(txtgross.Text) & "','" & txtremarks.Text & "',GetDate(),'" & login.user & "')"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()

                MsgBox("ok")
                showhours(strconn, txtlogsheet.Text)
                txtgross.Text = ""
                Timer3.Start()
            Else
                MsgBox("Invalid gross weight.", MsgBoxStyle.Exclamation, "")
            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "")
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            '/grdhour.Columns.Clear()
            Dim colnum As Integer, i = 0
            sql = "Select tblloghour.num, tblloghourwt.* from tblloghour inner join tblloghourwt on tblloghour.num=tblloghourwt.num where tblloghour.logsheetnum='" & txtlogsheet.Text & "' and status='1' order by tblloghourwt.num"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If grdhour.Columns.Count = 0 Then
                    colnum = dr("num")
                    grdhour.Rows.Add()
                    grdhour.Item(i, dr("num") - 1).Value = dr("netw")
                    i += 1
                Else
                    If colnum = dr("num") Then
                        grdhour.Item(i, dr("num") - 1).Value = dr("netw")
                        i += 1
                    Else
                        i = 0
                        colnum = dr("num")
                        grdhour.Rows.Add()
                        grdhour.Item(i, dr("num") - 1).Value = dr("netw")
                        i += 1
                    End If
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "")
        End Try
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        txtgross.Text = ""
        txtg.Text = ""
        Timer3.Start()
        txtgross.BackColor = Color.White
        timercount = weyttime
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        '/If txtgross.Focused = True Then
        If Val(readtxt1()) = 0 Then
            txtgross.Text = ""
            timercount = weyttime
        Else
            txttimer.Text = timercount

            If timercount = 0 Then
                If equalw() = True Then
                    stoptimer1()
                Else
                    txtg.Text = txtgross.Text
                    timercount = weyttime
                End If
            Else
                timercount -= 1
            End If

            txtgross.Text = readtxt1()
            If txtg.Text = "" And timercount <> 0 Then
                txtg.Text = txtgross.Text
            End If
        End If

        If Val(txtgross.Text) < minval And txtgross.Text <> "" Then
            txtremarks.Text = "Under Weight"
        ElseIf Val(txtgross.Text) > maxval And txtgross.Text <> "" Then
            txtremarks.Text = "Over Weight"
        Else
            txtremarks.Text = ""
        End If
        '/End If
    End Sub

    Private Sub stoptimer1()
        Timer3.Stop()
        txtg.Text = ""
        '/MsgBox("Weight " & txtgross.Text)
        txtgross.BackColor = Color.FromArgb(255, 255, 192)
    End Sub

    Private Function equalw() As Boolean
        eqw = False
        If Val(txtgross.Text) <> 0 And Val(txtg.Text) <> 0 Then
            If txtgross.Text = txtg.Text Then
                eqw = True
            End If
        End If

        Return eqw
    End Function

    Public Function readtxt1() As String
        Dim lines = System.IO.File.ReadAllLines("RS232Capture.txt")
        str = Trim(lines(lines.Length - 1))

        Return str
    End Function
End Class