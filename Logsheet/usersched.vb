Imports System.IO
Imports System.Data.SqlClient

Public Class usersched
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public schedcnf As Boolean = False

    Public Sub connect()
        conn = New SqlConnection
        conn.ConnectionString = strconn
        If conn.State <> ConnectionState.Open Then
            conn.Open()
        End If
    End Sub

    Public Sub disconnect()
        conn = New SqlConnection
        conn.ConnectionString = strconn
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub usersched_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        datefrom.Value = Date.Now
        viewusers()
        viewshift()
        viewwhse()
        btnrefresh.PerformClick()
    End Sub

    Public Sub viewusers()
        Try
            cmbname.Items.Clear()

            sql = "Select * from tblusers where status='1' and (workgroup<>'Administrator' and workgroup<>'Manager' and workgroup<>'Supervisor') order by fullname"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If login.depart = "All" Or login.depart = "Admin Dispatching" Then
                    If login.fullneym <> dr("fullname") Then
                        cmbname.Items.Add(dr("fullname"))
                    End If

                Else
                    If login.depart = dr("department") Then
                        cmbname.Items.Add(dr("fullname"))
                    End If
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub viewshift()
        Try
            cmbshift.Items.Clear()

            sql = "Select * from tblshift where status='1' order by shift"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbshift.Items.Add(dr("shift"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If cmbshift.Items.Count <> 0 Then
                cmbshift.Items.Add("ALL")
            End If

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub viewwhse()
        Try
            cmbwhse.Items.Clear()

            sql = "Select * from tblwhse where status='1' order by whsename"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbwhse.Items.Add(dr("whsename"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If cmbwhse.Items.Count <> 0 Then
                cmbwhse.Items.Add("ALL")
            End If

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            grdsched.Rows.Clear()

            sql = "Select * from tblsched where status<>'3'"

            If Trim(cmbname.Text) <> "" Then
                sql = sql & " and fullname like '%" & Trim(cmbname.Text) & "%'"
            End If

            If Trim(cmbshift.Text) <> "" Then
                sql = sql & " and shift='" & Trim(cmbshift.Text) & "'"
            End If

            If Trim(cmbwhse.Text) <> "" Then
                sql = sql & " and whsename='" & Trim(cmbwhse.Text) & "'"
            End If

            If login.depart <> "All" Then
                sql = sql & " and department='" & login.depart & "'"
            End If

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "Duty"
                Else
                    stat = "Day off"
                End If

                If (CDate(Format(datefrom.Value, "yyyy/MM/dd")) <= CDate(Format(dr("datefrom"), "yyyy/MM/dd")) And CDate(Format(dateto.Value, "yyyy/MM/dd")) >= CDate(Format(dr("datefrom"), "yyyy/MM/dd"))) Then
                    grdsched.Rows.Add(dr("schedid"), dr("fullname"), dr("userid"), Format(dr("datefrom"), "yyyy/MM/dd"), Format(dr("dateto"), "yyyy/MM/dd"), dr("shift"), dr("whsename"), dr("department"), stat)
                End If

                If (CDate(Format(dr("datefrom"), "yyyy/MM/dd")) <= CDate(Format(datefrom.Value, "yyyy/MM/dd")) And CDate(Format(dr("dateto"), "yyyy/MM/dd")) >= CDate(Format(datefrom.Value, "yyyy/MM/dd"))) Then
                    '/MsgBox(dr("fullname").ToString)
                    grdsched.Rows.Add(dr("schedid"), dr("fullname"), dr("userid"), Format(dr("datefrom"), "yyyy/MM/dd"), Format(dr("dateto"), "yyyy/MM/dd"), dr("shift"), dr("whsename"), dr("department"), stat)
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If login.depart <> "All" And login.depart <> "Admin Dispatching" And login.wgroup <> "Supervisor" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            'check if complete details
            If Trim(cmbname.Text) <> "" And Trim(cmbshift.Text) <> "" And Trim(cmbwhse.Text) <> "" Then
                schedcnf = False
                confirm.GroupBox1.Text = login.wgroup
                confirm.ShowDialog()
                If schedcnf = True Then
                    'insert user din sa schedule
                    sql = "Insert into tblsched(fullname,userid,datefrom,dateto,shift,whsename,department,datecreated,createdby,datemodified,modifiedby,status)values('" & Trim(cmbname.Text) & "','" & lbluserid.Text & "','" & datefrom.Value & "','" & dateto.Value & "','" & Trim(cmbshift.Text) & "','" & Trim(cmbwhse.Text) & "','" & lbldepart.Text & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()

                    MsgBox("Successfully added.", MsgBoxStyle.Information, "")
                    btnclear.PerformClick()
                    btnrefresh.PerformClick()
                End If

            Else
                MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
            End If

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" And login.wgroup <> "Agi Admin Staff" And login.wgroup <> "Supervisor" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If btnupdate.Text = "Update" Then
                If grdsched.SelectedRows.Count = 1 Or grdsched.SelectedCells.Count = 1 Then

                    lblid.Text = grdsched.Rows(grdsched.CurrentRow.Index).Cells(0).Value
                    cmbname.Text = grdsched.Rows(grdsched.CurrentRow.Index).Cells(1).Value
                    lbluserid.Text = grdsched.Rows(grdsched.CurrentRow.Index).Cells(2).Value
                    datefrom.Value = grdsched.Rows(grdsched.CurrentRow.Index).Cells(3).Value
                    dateto.Value = grdsched.Rows(grdsched.CurrentRow.Index).Cells(4).Value
                    cmbshift.Text = grdsched.Rows(grdsched.CurrentRow.Index).Cells(5).Value
                    cmbwhse.Text = grdsched.Rows(grdsched.CurrentRow.Index).Cells(6).Value
                    lbldepart.Text = grdsched.Rows(grdsched.CurrentRow.Index).Cells(7).Value

                    btnupdate.Text = "Save"
                    btnadd.Enabled = False
                    btnsearch.Enabled = False
                    btncancel.Enabled = False
                    btnrefresh.Enabled = False
                    btnclear.Text = "Cancel"
                    btnclear.Image = My.Resources.cancel
                Else
                    MsgBox("Select only one.", MsgBoxStyle.Exclamation, "")
                End If
            Else
                If Trim(cmbname.Text) <> "" And Trim(cmbshift.Text) <> "" And Trim(cmbwhse.Text) <> "" Then
                    'check if may sched ng ganun si fullname na iba ang id sa tblsched
                    sql = "Select * from tblsched where schedid<>'" & lblid.Text & "' and fullname='" & Trim(cmbname.Text) & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If (CDate(Format(dr("datefrom"), "yyyy/MM/dd")) >= CDate(Format(datefrom.Value, "yyyy/MM/dd")) And CDate(Format(dr("dateto"), "yyyy/MM/dd")) <= CDate(Format(datefrom.Value, "yyyy/MM/dd"))) Or (CDate(Format(dr("datefrom"), "yyyy/MM/dd")) >= CDate(Format(dateto.Value, "yyyy/MM/dd")) And CDate(Format(dr("dateto"), "yyyy/MM/dd")) <= CDate(Format(dateto.Value, "yyyy/MM/dd"))) Then
                            MsgBox(Trim(cmbname.Text) & " has already schedule.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()


                    schedcnf = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If schedcnf = True Then
                        sql = "Update tblsched set fullname='" & Trim(cmbname.Text) & "',userid='" & lbluserid.Text & "',datefrom='" & CDate(Format(datefrom.Value, "yyyy/MM/dd")) & "',dateto='" & CDate(Format(dateto.Value, "yyyy/MM/dd")) & "',shift='" & Trim(cmbshift.Text) & "',whsename='" & Trim(cmbwhse.Text) & "',department='" & lbldepart.Text & "',datemodified=GetDate(),modifiedby='" & login.user & "' where schedid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully saved.", MsgBoxStyle.Information, "")

                        btnupdate.Text = "Update"
                        btnadd.Enabled = True
                        btnsearch.Enabled = True
                        btncancel.Enabled = True
                        btnrefresh.Enabled = True
                        btnclear.Text = "Clear"
                        btnclear.Image = My.Resources.broom
                        btnclear.PerformClick()
                        btnrefresh.PerformClick()
                    End If
                Else
                    MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
                End If
            End If

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Try
            grdsched.Rows.Clear()

            sql = "Select * from tblsched where datefrom<='" & Format(Date.Now, "yyyy/MM/dd") & "' and dateto>='" & Format(Date.Now, "yyyy/MM/dd") & "'"

            If Trim(cmbname.Text) <> "" Then
                sql = sql & " and fullname like '%" & Trim(cmbname.Text) & "%'"
            End If

            If Trim(cmbshift.Text) <> "" Then
                sql = sql & " and shift='" & Trim(cmbshift.Text) & "'"
            End If

            If Trim(cmbwhse.Text) <> "" Then
                sql = sql & " and whsename='" & Trim(cmbwhse.Text) & "'"
            End If

            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                sql = sql & " and department='" & login.depart & "'"
            End If

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "Duty"
                Else
                    stat = "Day off"
                End If
                grdsched.Rows.Add(dr("schedid"), dr("fullname"), dr("userid"), Format(dr("datefrom"), "yyyy/MM/dd"), Format(dr("dateto"), "yyyy/MM/dd"), dr("shift"), dr("whsename"), dr("department"), stat)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        datefrom.Value = Date.Now
        cmbname.Text = ""
        cmbshift.Text = ""
        cmbwhse.Text = ""
        btnupdate.Text = "Update"

        If btnclear.Text = "Cancel" Then
            btnupdate.Text = "Update"
            btnadd.Enabled = True
            btnsearch.Enabled = True
            btncancel.Enabled = True
            btnrefresh.Enabled = True
            btnclear.Text = "Clear"
            btnclear.Image = My.Resources.broom
            btnclear.PerformClick()
        End If
    End Sub

    Private Sub datefrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles datefrom.ValueChanged
        dateto.MinDate = datefrom.Value
    End Sub

    Private Sub cmbname_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Leave
        Try
            If Trim(cmbname.Text) <> "" Then
                sql = "Select * from tblusers where fullname='" & Trim(cmbname.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    cmbname.Text = dr("fullname")
                    lbluserid.Text = dr("userid")
                    lbldepart.Text = dr("department")
                Else
                    MsgBox("Invalid full name.", MsgBoxStyle.Critical, "")
                    cmbname.Text = ""
                    cmbname.Focus()
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Else
                lbluserid.Text = ""
            End If
           
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbshift_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbshift.Leave
        Try
            If Not cmbshift.Items.Contains(Trim(cmbshift.Text.ToUpper)) = True And Trim(cmbshift.Text.ToUpper) <> "" Then
                MsgBox("Invalid shift.", MsgBoxStyle.Critical, "")
                cmbshift.Text = ""
                cmbshift.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbshift_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbshift.SelectedIndexChanged

    End Sub

    Private Sub cmbwhse_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbwhse.Leave
        Try
            If Not cmbwhse.Items.Contains(Trim(cmbwhse.Text.ToUpper)) = True And Trim(cmbwhse.Text.ToUpper) <> "" Then
                MsgBox("Invalid warehouse.", MsgBoxStyle.Critical, "")
                cmbwhse.Text = ""
                cmbwhse.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbwhse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbwhse.SelectedIndexChanged

    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click

    End Sub
End Class