Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Security
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Text

Public Class CopyusersWoutsigns
    Dim lines = System.IO.File.ReadAllLines("connectionstring.txt")
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public cnf As Integer = 0, firm As Boolean = False

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

    Private Sub users_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub users_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            mdiform.Show()
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub users_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor

            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                GroupBox1.Enabled = False
            Else
                GroupBox1.Enabled = True
            End If

            whse()
            viewdept()
            viewfilterdept()
            viewfilterbranch()
            viewall()
            viewbranch()

            If grdusers.Rows.Count = 0 Then
                btnupdate.Enabled = False
                btndeactivate.Enabled = False
            Else
                btnupdate.Enabled = True
                btndeactivate.Enabled = True
            End If
            grdselect()

            grdusers.Columns(1).Width = 160
            grdusers.Columns(2).Width = 140
            grdusers.Columns(3).Width = 120
            grdusers.Columns(4).Width = 120
            grdusers.Columns(8).Width = 120

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

    Public Sub whse()
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
                cmbwhse.Items.Add("All")
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

    Public Sub viewbranch()
        Try
            cmbbranch.Items.Clear()

            sql = "Select * from tblbranch where status='1' order by branch"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbbranch.Items.Add(dr("branch"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If cmbbranch.Items.Count <> 0 Then
                cmbbranch.Items.Add("All")
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

    Public Sub viewwgroup()
        Try
            Exit Sub
            cmbgroup.Items.Clear()

            sql = "Select * from tblwgroup where status='1' order by workgroup"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbgroup.Items.Add(dr("workgroup"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If cmbgroup.Items.Count <> 0 Then
                cmbgroup.Items.Add("All")
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

    Public Sub viewdept()
        Try
            cmbdept.Items.Clear()
            cmbdept.Items.Add("")

            sql = "Select * from tbldepartment where status='1' order by department"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbdept.Items.Add(dr("department"))
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

    Public Sub viewfilterdept()
        Try
            cmbfilter.Items.Clear()
            cmbfilter.Items.Add("")

            sql = "Select * from tbldepartment where status='1' order by department"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbfilter.Items.Add(dr("department"))
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

    Public Sub viewfilterbranch()
        Try
            cmbbr.Items.Clear()
            cmbbr.Items.Add("")

            sql = "Select * from tblbranch where status='1' order by branch"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbbr.Items.Add(dr("branch"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If cmbbr.Items.Count <> 0 Then
                cmbbr.Items.Add("All")
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

    Public Sub viewall()
        Try
            Me.Cursor = Cursors.WaitCursor

            grdusers.Rows.Clear()
            Dim i As Integer = 0
            Dim pass As String = ""
            sql = "Select * from tblusers"
            If chkhide.Checked = True Then
                sql = sql & " where status='1'"
            End If

            If cmbfilter.SelectedItem <> "" Then
                sql = sql & " and department='" & cmbfilter.SelectedItem & "'"
            Else
                '/cmbfilter.Items.Clear()
            End If

            If cmbbr.SelectedItem <> "" Then
                sql = sql & " and branch='" & cmbbr.SelectedItem & "'"
            Else
                '/cmbbr.Items.Clear()
            End If

            sql = sql & " order by branch, department, workgroup, fullname"

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Me.Cursor = Cursors.WaitCursor
                Dim encryptpass As String = "", stat As String = ""
                pass = Decrypt(dr("password"))
                If pass <> "" Then
                    encryptpass = New String(CChar("*"), pass.Length)
                End If

                If dr("status") = 1 Then
                    stat = "Active"
                Else
                    stat = "Deactivated"
                End If

                grdusers.Rows.Add(dr("userid"), dr("fullname"), dr("username"), encryptpass, dr("department"), dr("workgroup"), dr("whsename"), dr("branch").ToString, stat, pass)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.Default

            If pass <> "" Then
                chkpass.Enabled = True
            End If
            grdselect()

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
            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                Me.Cursor = Cursors.Default
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            If grdusers.SelectedRows.Count = 1 Or grdusers.SelectedCells.Count = 1 Then
                If btnupdate.Text = "Update" Then
                    If grdusers.Rows(grdusers.CurrentRow.Index).Cells(5).Value = "Administrator" And login.wgroup <> "Administrator" Then
                        MsgBox("Authorization Failed.", MsgBoxStyle.Critical, "")
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If


                    If grdusers.Rows(grdusers.CurrentRow.Index).Cells(8).Value = "Deactivated" Then
                        MsgBox("Cannot update deactivated user account.", MsgBoxStyle.Exclamation, "")
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If

                    chkpass.Enabled = True
                    btnadd.Enabled = False
                    btndeactivate.Enabled = False
                    btncancel.Enabled = True
                    txtpass.Enabled = False
                    txtconfirm.Enabled = False
                    grdusers.Enabled = False

                    lbluserid.Text = grdusers.Rows(grdusers.CurrentRow.Index).Cells(0).Value
                    txtfull.Text = grdusers.Rows(grdusers.CurrentRow.Index).Cells(1).Value
                    lblfull.Text = grdusers.Rows(grdusers.CurrentRow.Index).Cells(1).Value
                    txtusername.Text = grdusers.Rows(grdusers.CurrentRow.Index).Cells(2).Value
                    txtpass.Text = grdusers.Rows(grdusers.CurrentRow.Index).Cells(9).Value
                    txtpass.Tag = grdusers.Rows(grdusers.CurrentRow.Index).Cells(9).Value
                    cmbdept.SelectedItem = grdusers.Rows(grdusers.CurrentRow.Index).Cells(4).Value
                    cmbgroup.SelectedItem = grdusers.Rows(grdusers.CurrentRow.Index).Cells(5).Value
                    cmbwhse.SelectedItem = grdusers.Rows(grdusers.CurrentRow.Index).Cells(6).Value
                    cmbbranch.SelectedItem = grdusers.Rows(grdusers.CurrentRow.Index).Cells(7).Value
                    lblcas.Text = txtusername.Text

                    btnupdate.Text = "Save"
                Else
                    If txtpass.Enabled = True And (Trim(txtusername.Text) = "" Or Trim(txtpass.Text) = "" Or Trim(txtconfirm.Text) = "" Or cmbdept.SelectedItem = "" Or cmbgroup.SelectedItem = "" Or cmbwhse.SelectedItem = "" Or cmbbranch.SelectedItem = "") Then
                        MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    Try
                        Me.Cursor = Cursors.WaitCursor
                        Dim readd As Integer = 0
                        Dim a As String = MsgBox("Are you sure you want to update record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
                        If a = vbYes Then
                            confirmsave.GroupBox1.Text = login.wgroup
                            confirmsave.ShowDialog()
                            If firm = False Then
                                Me.Cursor = Cursors.Default
                                Exit Sub
                            End If
                            sql = "Select * from tblusers where userid='" & lbluserid.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            dr = cmd.ExecuteReader
                            If dr.Read Then
                                readd = 1
                            End If
                            dr.Dispose()
                            cmd.Dispose()
                            conn.Close()

                            If readd = 1 Then
                                ExecuteUpdate(strconn)

                                btncancel.PerformClick()
                                btnviewall.PerformClick()
                            End If
                            firm = False
                            Me.Cursor = Cursors.Default
                        Else
                            btncancel.PerformClick()
                            firm = False
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        MsgBox(ex.ToString, MsgBoxStyle.Information)
                    Finally
                        disconnect()
                    End Try

                    btnupdate.Text = "Update"
                    btncancel.Enabled = False
                    btnadd.Enabled = True
                    btndeactivate.Enabled = True
                    grdusers.Enabled = True
                End If
            Else
                MsgBox("Select only one.", MsgBoxStyle.Exclamation, "")
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

    Private Sub ExecuteUpdate(ByVal connectionString As String)
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
                '/command.CommandText = sql
                '/command.ExecuteNonQuery()
                sql = "Update tblusers set fullname='" & Trim(txtfull.Text) & "', username='" & Trim(txtusername.Text) & "', password='" & Encrypt(Trim(txtpass.Text)) & "', department='" & Trim(cmbdept.SelectedItem) & "', workgroup='" & Trim(cmbgroup.SelectedItem) & "', whsename='" & Trim(cmbwhse.SelectedItem) & "', branch='" & Trim(cmbbranch.SelectedItem) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where userid='" & lbluserid.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                'update fullnames lblfull.Text
                'tbllogsheet
                sql = "Update tbllogsheet set millersup='" & Trim(txtfull.Text) & "' where millersup='" & lblfull.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                'tbllogsheet
                sql = "Update tbllogsheet set cancelby='" & Trim(txtfull.Text) & "' where cancelby='" & lblfull.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                'tbllogitem
                sql = "Update tbllogitem set cancelby='" & Trim(txtfull.Text) & "' where cancelby='" & lblfull.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                'tbllogticket
                sql = "Update tbllogticket set cancelby='" & Trim(txtfull.Text) & "' where cancelby='" & lblfull.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                'tblorderfill
                sql = "Update tblorderfill set cancelby='" & Trim(txtfull.Text) & "' where cancelby='" & lblfull.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                'completedby
                'tblorderfill
                sql = "Update tblorderfill set completedby='" & Trim(txtfull.Text) & "' where completedby='" & lblfull.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                'tblcoa
                sql = "Update tblcoa set cancelby='" & Trim(txtfull.Text) & "' where cancelby='" & lblfull.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                'tbllogcancel
                sql = "Update tbllogcancel set cancelby='" & Trim(txtfull.Text) & "' where cancelby='" & lblfull.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                'tbllogdouble
                sql = "Update tbllogdouble set cancelby='" & Trim(txtfull.Text) & "' where cancelby='" & lblfull.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                'tblloggood
                sql = "Update tblloggood set cancelby='" & Trim(txtfull.Text) & "' where cancelby='" & lblfull.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                'tbllogrange
                sql = "Update tbllogrange set cancelby='" & Trim(txtfull.Text) & "' where cancelby='" & lblfull.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                'tbloflogcancel
                sql = "Update tbloflogcancel set cancelby='" & Trim(txtfull.Text) & "' where cancelby='" & lblfull.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                'tblticketcancel
                sql = "Update tblticketcancel set cancelby='" & Trim(txtfull.Text) & "' where cancelby='" & lblfull.Text & "' "
                command.CommandText = sql
                command.ExecuteNonQuery()

                If Trim(txtusername.Text) <> lblcas.Text Then
                    'update tbllogin
                    sql = "Update tbllogin set username='" & Trim(txtusername.Text) & "' where username='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    'update tbllogticket
                    sql = "Update tbllogticket set whsechecker='" & Trim(txtfull.Text) & "' where whsechecker='" & lblfull.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tblcat set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tblcat set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tblcoa set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tblcoa set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tblcustomer set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tblcustomer set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tbldepartment set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tbldepartment set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tblitems set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tblitems set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tbllocation set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tbllocation set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tbllogitem set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tbllogitem set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tbllogrange set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tbllogrange set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tbllogreserve set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tbllogreserve set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tbllogsheet set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tbllogsheet set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tbllogticket set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tbllogticket set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tblofitem set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tblofitem set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tbloflog set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tbloflog set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tblorderfill set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tblorderfill set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tblpalletizer set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tblpalletizer set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tblsched set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tblsched set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tblshift set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tblshift set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tblversion set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tblversion set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tblwgroup set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tblwgroup set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    '///////
                    sql = "Update tblwhse set createdby='" & Trim(txtusername.Text) & "' where createdby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    sql = "Update tblwhse set modifiedby='" & Trim(txtusername.Text) & "' where modifiedby='" & lblcas.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If


                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully Saved.", MsgBoxStyle.Information, "")

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                '/Console.WriteLine("Commit Exception Type: {0}", ex.GetType())
                '/Console.WriteLine("  Message: {0}", ex.Message)
                '/Dim typeName = ex.GetType().Name
                MsgBox("1: " & ex.ToString, MsgBoxStyle.Exclamation, "")
                ' Attempt to roll back the transaction. 
                Try
                    Me.Cursor = Cursors.Default
                    transaction.Rollback()

                Catch ex2 As Exception
                    ' This catch block will handle any errors that may have occurred 
                    ' on the server that would cause the rollback to fail, such as 
                    ' a closed connection.
                    Me.Cursor = Cursors.Default
                    '/Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                    '/Console.WriteLine("  Message: {0}", ex2.Message)
                    MsgBox("2: " & ex2.ToString & vbCrLf & vbCrLf & "Please try again.", MsgBoxStyle.Information, "")
                End Try
            End Try
        End Using
    End Sub

    Private Sub btnviewall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnviewall.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            btncancel.PerformClick()
            txtfull.Text = ""
            lblfull.Text = ""
            txtusername.Text = ""
            txtpass.Text = ""
            txtpass.Enabled = True
            txtconfirm.Text = ""
            txtconfirm.Enabled = False
            cmbdept.SelectedIndex = 0
            cmbwhse.SelectedIndex = 0
            cmbbranch.SelectedIndex = 0
            chkpass.Checked = False
            btnupdate.Text = "Update"
            btnadd.Enabled = True
            btncancel.Enabled = False
            grdusers.Enabled = True

            whse()
            viewdept()
            viewwgroup()
            viewbranch()
            viewall()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Function Encrypt(ByVal plainText As String) As String

        Dim passPhrase As String = "minePassPhrase"
        Dim saltValue As String = "mySaltValue"
        Dim hashAlgorithm As String = "SHA1"

        Dim passwordIterations As Integer = 2
        Dim initVector As String = "@1B2c3D4e5F6g7H8"
        Dim keySize As Integer = 256

        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

        Dim plainTextBytes As Byte() = Encoding.UTF8.GetBytes(plainText)


        Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)

        Dim keyBytes As Byte() = password.GetBytes(keySize \ 8)
        Dim symmetricKey As New RijndaelManaged()

        symmetricKey.Mode = CipherMode.CBC

        Dim encryptor As ICryptoTransform = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

        Dim memoryStream As New MemoryStream()
        Dim cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)

        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
        cryptoStream.FlushFinalBlock()
        Dim cipherTextBytes As Byte() = memoryStream.ToArray()
        memoryStream.Close()
        cryptoStream.Close()
        Dim cipherText As String = Convert.ToBase64String(cipherTextBytes)
        Return cipherText
    End Function

    Public Function Decrypt(ByVal cipherText As String) As String
        Dim passPhrase As String = "minePassPhrase"
        Dim saltValue As String = "mySaltValue"
        Dim hashAlgorithm As String = "SHA1"

        Dim passwordIterations As Integer = 2
        Dim initVector As String = "@1B2c3D4e5F6g7H8"
        Dim keySize As Integer = 256
        ' Convert strings defining encryption key characteristics into byte
        ' arrays. Let us assume that strings only contain ASCII codes.
        ' If strings include Unicode characters, use Unicode, UTF7, or UTF8
        ' encoding.
        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

        ' Convert our ciphertext into a byte array.
        Dim cipherTextBytes As Byte() = Convert.FromBase64String(cipherText)

        ' First, we must create a password, from which the key will be 
        ' derived. This password will be generated from the specified 
        ' passphrase and salt value. The password will be created using
        ' the specified hash algorithm. Password creation can be done in
        ' several iterations.
        Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)

        ' Use the password to generate pseudo-random bytes for the encryption
        ' key. Specify the size of the key in bytes (instead of bits).
        Dim keyBytes As Byte() = password.GetBytes(keySize / 8)

        ' Create uninitialized Rijndael encryption object.
        Dim symmetricKey As New RijndaelManaged()

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC

        ' Generate decryptor from the existing key bytes and initialization 
        ' vector. Key size will be defined based on the number of the key 
        ' bytes.
        Dim decryptor As ICryptoTransform = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As New MemoryStream(cipherTextBytes)

        ' Define cryptographic stream (always use Read mode for encryption).
        Dim cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)

        ' Since at this point we don't know what the size of decrypted data
        ' will be, allocate the buffer long enough to hold ciphertext;
        ' plaintext is never longer than ciphertext.
        Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}

        ' Start decrypting.
        Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert decrypted data into a string. 
        ' Let us assume that the original plaintext string was UTF8-encoded.
        Dim plainText As String = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)

        ' Return decrypted string.   
        Return plainText
    End Function

    Private Sub chkpass_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkpass.CheckedChanged
        If chkpass.Checked = True Then
            txtpass.PasswordChar = ""
        Else
            txtpass.PasswordChar = "*"
        End If
    End Sub

    Private Sub link_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles link.LinkClicked
        If btnupdate.Text = "Save" Then
            txtpass.Text = ""
            txtpass.Enabled = True
            txtpass.Focus()
            btncancel.Enabled = True
        End If
    End Sub

    Private Sub txtpass_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpass.Leave
        Try
            Me.Cursor = Cursors.WaitCursor
            If Trim(txtpass.Text) <> "" Then
                If Trim(txtpass.Text) = Trim(txtpass.Tag) Then
                    MsgBox("New password is similar to old password. Try another password.", MsgBoxStyle.Critical, "")
                    txtpass.Text = ""
                    txtpass.Focus()
                    txtconfirm.Enabled = False
                    txtconfirm.Text = ""
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                txtconfirm.Enabled = True
                txtconfirm.Focus()
            Else
                txtconfirm.Enabled = False
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub txtconfirm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtconfirm.Leave
        Try
            If Trim(txtpass.Text) <> Trim(txtconfirm.Text) Then
                txtconfirm.Text = ""
                txtconfirm.Enabled = False
                txtpass.Text = ""
                MsgBox("Password not match.", MsgBoxStyle.Critical, "")
                txtpass.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            If btnupdate.Text = "Save" Then
                btnupdate.Focus()
            Else
                btnadd.Focus()
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        'If btnupdate.Text = "Save" Then
        txtfull.Text = ""
        lblfull.Text = ""
        txtusername.Text = ""
        txtpass.Text = ""
        txtpass.Enabled = True
        txtpass.Tag = ""
        txtconfirm.Text = ""
        txtconfirm.Enabled = False
        cmbdept.SelectedIndex = 0
        cmbgroup.SelectedIndex = 0
        cmbwhse.SelectedIndex = 0
        cmbbranch.SelectedIndex = 0
        chkpass.Checked = False
        btnupdate.Text = "Update"
        btnadd.Enabled = True
        btncancel.Enabled = False
        grdusers.Enabled = True
        'End If
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                Me.Cursor = Cursors.Default
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            If btnadd.Text = "Add" Then

                sql = "Select * from tblusers where username='" & Trim(txtusername.Text) & "' and status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Username is already exist.", MsgBoxStyle.Information, "")
                    txtusername.Text = ""
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                sql = "Select * from tblusers where fullname='" & Trim(txtfull.Text) & "' and status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Full name is already exist.", MsgBoxStyle.Information, "")
                    txtfull.Text = ""
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                If Trim(txtfull.Text) = "" Or Trim(txtusername.Text) = "" Or Trim(txtpass.Text) = "" Or Trim(txtconfirm.Text) = "" Or cmbdept.SelectedItem = "" Or cmbgroup.SelectedItem = "" Or cmbwhse.SelectedItem = "" Or cmbbranch.SelectedItem = "" Then
                    MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
                    txtusername.Focus()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                For Each row As DataGridViewRow In grdusers.Rows
                    If grdusers.Rows(row.Index).Cells(5).Value = "Administrator" And cmbgroup.SelectedItem = "Administrator" Then
                        MsgBox("Administrator is already exist.", MsgBoxStyle.Information, "")
                        cmbgroup.SelectedIndex = 0
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                Next

                Dim a As String = MsgBox("Are you sure you want to add user?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
                If a = vbYes Then
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If firm = False Then
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If

                    sql = "Insert into tblusers(fullname,username,password,department,workgroup,whsename,branch,datecreated,createdby,datemodified,modifiedby,status)values('" & Trim(txtfull.Text) & "','" & Trim(txtusername.Text) & "','" & Encrypt(Trim(txtpass.Text)) & "','" & cmbdept.SelectedItem & "','" & cmbgroup.SelectedItem & "','" & cmbwhse.SelectedItem & "','" & cmbbranch.SelectedItem & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()

                    MsgBox("Successfully Added.", MsgBoxStyle.Information, "")
                    btncancel.PerformClick()
                    btnviewall.PerformClick()
                    firm = False
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

    Private Sub txtusername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtusername.TextChanged
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789.-/"
        Dim theText As String = txtusername.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtusername.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtusername.Text.Length - 1
            Letter = txtusername.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtusername.Text = theText
        txtusername.Select(SelectionIndex - Change, 0)

        '/If Trim(txtusername.Text) <> "" And btnupdate.Text = "Update" Then
        btncancel.Enabled = True
        '/Else
        '/btncancel.Enabled = False
        '/End If
    End Sub

    Private Sub txtpass_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpass.TextChanged
        Dim charactersDisallowed As String = "'"
        Dim theText As String = txtpass.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtpass.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtpass.Text.Length - 1
            Letter = txtpass.Text.Substring(x, 1)
            If charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtpass.Text = theText
        txtpass.Select(SelectionIndex - Change, 0)

        If Trim(txtpass.Text) <> "" And btnupdate.Text = "Update" Then
            btncancel.Enabled = True
        ElseIf Trim(txtpass.Text) <> "" And btnupdate.Text = "Save" Then
            btncancel.Enabled = True
        ElseIf Trim(txtpass.Text) = "" And btnupdate.Text = "Save" Then
            btncancel.Enabled = True
        Else
            btncancel.Enabled = False
        End If
    End Sub

    Private Sub btndeactivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeactivate.Click
        Try
            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                Me.Cursor = Cursors.Default
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            If grdusers.Rows.Count <> 0 Then
                If grdusers.SelectedCells.Count = 1 Or grdusers.SelectedRows.Count = 1 Then
                    Try
                        Dim a As String = MsgBox("Are you sure you want to " & btndeactivate.Text & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
                        If a = vbYes Then
                            confirmsave.GroupBox1.Text = login.wgroup
                            confirmsave.ShowDialog()
                            If firm = False Then
                                Me.Cursor = Cursors.Default
                                Exit Sub
                            End If

                            If btndeactivate.Text = "Deactivate" Then
                                sql = "Update tblusers set status='0' where userid='" & grdusers.Rows(grdusers.CurrentRow.Index).Cells(0).Value & "' and workgroup<>'Administrator'"
                            Else
                                sql = "Update tblusers set status='1' where userid='" & grdusers.Rows(grdusers.CurrentRow.Index).Cells(0).Value & "' and workgroup<>'Administrator'"
                            End If
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()

                            btnviewall.PerformClick()

                            Me.Cursor = Cursors.Default
                        End If

                    Catch ex As System.InvalidOperationException
                        Me.Cursor = Cursors.Default
                        MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        MsgBox(ex.ToString, MsgBoxStyle.Information)
                    Finally
                        disconnect()
                    End Try
                Else
                    MsgBox("Select only one.", MsgBoxStyle.Exclamation, "")
                End If
                firm = False
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub grdusers_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdusers.SelectionChanged
        grdselect()
    End Sub

    Public Sub grdselect()
        Try
            Me.Cursor = Cursors.WaitCursor
            If grdusers.Rows.Count <> 0 Then
                If grdusers.Rows(grdusers.CurrentRow.Index).Cells(8).Value = "Active" Then
                    btndeactivate.Text = "Deactivate"
                Else
                    btndeactivate.Text = "Activate"
                End If
                If grdusers.Rows(grdusers.CurrentRow.Index).Cells(5).Value = "Administrator" Then
                    btndeactivate.Enabled = False
                Else
                    btndeactivate.Enabled = True
                End If
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub txtfull_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfull.Leave
        txtfull.Text = StrConv(txtfull.Text, VbStrConv.ProperCase)
    End Sub

    Private Sub txtfull_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfull.TextChanged
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789.-/"
        Dim theText As String = txtfull.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtfull.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtfull.Text.Length - 1
            Letter = txtfull.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtfull.Text = theText
        txtfull.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub txtconfirm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtconfirm.TextChanged
        Dim charactersDisallowed As String = "'"
        Dim theText As String = txtconfirm.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtconfirm.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtconfirm.Text.Length - 1
            Letter = txtconfirm.Text.Substring(x, 1)
            If charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtconfirm.Text = theText
        txtconfirm.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub cmbdept_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdept.SelectedIndexChanged
        If cmbdept.SelectedItem <> "" Then
            cmbgroup.Enabled = True

            cmbgroup.Items.Clear()
            cmbgroup.Items.Add("")

            sql = "Select * from tblwgroup where department='" & cmbdept.SelectedItem & "' and status='1' order by workgroup"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbgroup.Items.Add(dr("workgroup"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

        Else
            cmbgroup.SelectedItem = ""
            cmbgroup.Enabled = False
        End If
    End Sub

    Private Sub grdusers_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdusers.CellContentClick

    End Sub

    Public Sub createdbymodifiedby()
        Try


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

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            If Trim(txtfull.Text) <> "" Then
                whse()
                viewdept()
                viewfilterdept()
                viewfilterbranch()
                viewwgroup()
                viewbranch()

                grdusers.Rows.Clear()
                Dim pass As String = ""

                sql = "Select * from tblusers where fullname like '%" & Trim(txtfull.Text) & "%'"
                sql = sql & " order by branch, department, workgroup, fullname"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    Me.Cursor = Cursors.WaitCursor
                    Dim encryptpass As String = "", stat As String = ""
                    pass = Decrypt(dr("password"))
                    If pass <> "" Then
                        encryptpass = New String(CChar("*"), pass.Length)
                    End If

                    If dr("status") = 1 Then
                        stat = "Active"
                    Else
                        stat = "Deactivated"
                    End If

                    grdusers.Rows.Add(dr("userid"), dr("fullname"), dr("username"), encryptpass, dr("department"), dr("workgroup"), dr("whsename"), dr("branch").ToString, stat, pass)
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            ElseIf Trim(txtusername.Text) <> "" Then
                whse()
                viewdept()
                viewwgroup()
                viewbranch()

                grdusers.Rows.Clear()
                Dim pass As String = ""

                sql = "Select * from tblusers where username like '%" & Trim(txtusername.Text) & "%'"
                sql = sql & " order by branch, department, workgroup, fullname"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    Me.Cursor = Cursors.WaitCursor
                    Dim encryptpass As String = "", stat As String = ""
                    pass = Decrypt(dr("password"))
                    If pass <> "" Then
                        encryptpass = New String(CChar("*"), pass.Length)
                    End If

                    If dr("status") = 1 Then
                        stat = "Active"
                    Else
                        stat = "Deactivated"
                    End If

                    grdusers.Rows.Add(dr("userid"), dr("fullname"), dr("username"), encryptpass, dr("department"), dr("workgroup"), dr("whsename"), dr("branch").ToString, stat, pass)
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            End If

            If grdusers.Rows.Count = 0 Then
                MsgBox("Cannot found record.", MsgBoxStyle.Critical, "")
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

    Private Sub cmbfilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbfilter.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            btncancel.PerformClick()
            txtfull.Text = ""
            lblfull.Text = ""
            txtusername.Text = ""
            txtpass.Text = ""
            txtpass.Enabled = True
            txtconfirm.Text = ""
            txtconfirm.Enabled = False
            cmbdept.SelectedIndex = 0
            cmbwhse.SelectedIndex = 0
            cmbbranch.SelectedIndex = 0
            chkpass.Checked = False
            btnupdate.Text = "Update"
            btnadd.Enabled = True
            btncancel.Enabled = False
            grdusers.Enabled = True

            whse()
            viewdept()
            viewwgroup()
            viewall()
            viewbranch()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub cmbbr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbbr.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            btncancel.PerformClick()
            txtfull.Text = ""
            lblfull.Text = ""
            txtusername.Text = ""
            txtpass.Text = ""
            txtpass.Enabled = True
            txtconfirm.Text = ""
            txtconfirm.Enabled = False
            cmbdept.SelectedIndex = 0
            cmbwhse.SelectedIndex = 0
            cmbbranch.SelectedIndex = 0
            chkpass.Checked = False
            btnupdate.Text = "Update"
            btnadd.Enabled = True
            btncancel.Enabled = False
            grdusers.Enabled = True

            whse()
            viewdept()
            viewwgroup()
            viewall()
            viewbranch()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub
End Class