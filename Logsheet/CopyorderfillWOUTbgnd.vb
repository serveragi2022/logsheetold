Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class CopyorderfillWOUTbgnd
    Dim lines = System.IO.File.ReadAllLines("connectionstring.txt")
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim AscendingListBox As New List(Of Integer)
    Public rowindex As Integer
    Public ofcnf As Boolean = False, ofcnfby As String = "", ofrems As String = "", picktickets As Boolean
    Dim b4edit As Integer

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
        If conn.State <> ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub orderfill_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub orderfill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtorf.Focus()
        viewofnum()

        cmbitem.DropDownWidth = 300
    End Sub

    Private Sub orderfill_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        '/If login.logshift = "" Or login.logwhse = "" Then
        '/MsgBox("Choose warehouse.", MsgBoxStyle.Exclamation, "")
        '/Panel2.Enabled = False
        '/chooseshift.ShowDialog()
        '/Me.Dispose()
        '/Else
        Panel2.Enabled = True
        txtwrs.Focus()
        viewofnum()
        viewitems()
        '/End If
    End Sub

    Public Sub defaultform()
        txtitem.Text = ""
        txtwrs.Text = ""
        txtwhse.Text = ""
        txtcus.Text = ""
        txtref.Text = ""
        txtplate.Text = ""
        txtdriver.Text = ""
        txtrems.Text = ""
        cmbitem.Text = ""
        txtbags.Text = ""
        txtrems.Text = ""
        grdlog.Rows.Clear()
        '/grdtickets.Rows.Clear()
        grdcancel.Rows.Clear()
        cmbcancel.Items.Clear()
        txtofitemid.Text = ""
        lbllog.Text = ""
        txtpallet.Text = ""
        txtcancel.Text = ""
        txtreason.Text = ""
        Panelcancel.Enabled = False
        btnconfirm.Enabled = True
        btnsearch.Enabled = True
        btnwrssearch.Enabled = True
        Me.CancelTicketToolStripMenuItem.Visible = True
        Me.EditNoOfSelectedBagsToolStripMenuItem.Visible = True
    End Sub

    Public Sub viewitems()
        Try
            If txtorf.Text <> "" Then
                cmbitem.Items.Clear()
                cmbitem.Items.Add("")

                sql = "Select * from tblofitem where ofnum='" & lblorf.Text & txtorf.Text & "' and status<>'3' and branch='" & login.branch & "' order by itemname"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    cmbitem.Items.Add(dr("itemname"))
                End While
                dr.Dispose()
                cmd.Dispose()
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

    Private Sub grdlog_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdlog.CellClick
    End Sub

    Private Sub grdlog_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdlog.CellEndEdit
        If grdlog.Rows(rowindex).Cells(10).Style.BackColor = Color.HotPink Then
            'check if numeric
            If (e.ColumnIndex = 10) Then   ' Checking numeric value for Column1 only
                If grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> Nothing Then
                    Dim value As String = grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
                    If Not Information.IsNumeric(value) Or Val(value) < 1 Or Val(value) > grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex - 1).Value Then
                        MsgBox("Invalid input.", MsgBoxStyle.Exclamation, "")
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex - 1).Value
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
                        grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                        '/grdlog.Columns(13).ReadOnly = False

                        viewselectedtickets()
                    Else
                        Dim checkCell As DataGridViewCheckBoxCell = CType(grdlog.Rows(grdlog.CurrentRow.Index).Cells(13), DataGridViewCheckBoxCell)
                        '/Button1.PerformClick()
                        If checkCell.Value = True Then
                            Dim a As String = MsgBox("Picked tickets will reset. Are you sure you want to continue?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                            If a = vbYes Then
                                grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Fix(grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                                grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
                                grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                                '/grdlog.Columns(13).ReadOnly = False

                                checkCell.Value = False

                                'reset grdtickets
                                For Each row As DataGridViewRow In grdtickets.Rows
                                    Dim pallet As String = grdtickets.Rows(row.Index).Cells(2).Value

                                    If grdlog.Rows(rowindex).Cells(3).Value = pallet Then
                                        grdtickets.Rows(row.Index).Cells(6).Value = ""
                                    End If
                                Next

                                viewselectedtickets()

                            Else
                                'cancel edit no. of selected bags
                                grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = b4edit 'grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex - 1).Value
                                grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
                                grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                                '/grdlog.Columns(13).ReadOnly = False

                            End If
                        Else
                            grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Fix(grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                            grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
                            grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                            '/grdlog.Columns(13).ReadOnly = False

                            viewselectedtickets()
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub grdlog_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdlog.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then

                rowindex = e.RowIndex
                grdlog.ClearSelection()
                grdlog.Rows(rowindex).Cells(e.ColumnIndex).Selected = True


                If btncomplete.Text = "Selected Item Complete" Then
                    Me.ContextMenuStrip1.Show(Cursor.Position)
                    If e.ColumnIndex = 10 And Panelcancel.Enabled = False Then
                        EditNoOfSelectedBagsToolStripMenuItem.Visible = True
                    Else
                        EditNoOfSelectedBagsToolStripMenuItem.Visible = False
                    End If
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

    Private Sub CancelTicketToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelTicketToolStripMenuItem.Click
        If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            ofcnf = False
            ofcnfby = ""
            confirm.GroupBox1.Text = login.wgroup
            confirm.ShowDialog()
            If ofcnf = True Then
                'can cancel pallet tag #
                Panelcancel.Enabled = True
                lbllog.Text = grdlog.Rows(rowindex).Cells(2).Value
                txtpallet.Text = grdlog.Rows(rowindex).Cells(3).Value
                '/txtletter.Text = grdlog.Rows(rowindex).Cells(4).Value
                Me.CancelTicketToolStripMenuItem.Visible = False
                Me.EditNoOfSelectedBagsToolStripMenuItem.Visible = False
                Panel2.Enabled = False
                txtletter.Focus()
            End If
        Else
            MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
        End If
    End Sub

    Private Sub btnconfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnconfirm.Click
        Try
            If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                'check if orderfill is not cancelled
                sql = "Select * from tblorderfill where ofid='" & lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If grdlog.Rows.Count <> 0 Then
                    'check if may errortext sa grdlogs series cells 11
                    Dim witherror As Boolean = False
                    For Each row As DataGridViewRow In grdlog.Rows
                        If grdlog.Rows(row.Index).Cells(11).ErrorText <> "" Then
                            MsgBox("There are some errors occured. Cannot confirm.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                    Next
                End If


                If btncomplete.Enabled = False Then
                    sql = "Select * from tblofitem where ofid='" & lblorfid.Text & "' and status<>'2' and status<>'3'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MsgBox("Cannot confirm orderfill. Some items are not yet completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If

                sql = "Select * from tblofitem where ofid='" & lblorfid.Text & "' and status='2'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                Else
                    MsgBox("Cannot confirm orderfill. Some items are not yet completed.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if other details are completed
                sql = "Select * from tblorderfill where ofid='" & lblorfid.Text & "' and ((pstart='' or pstart is NULL) or (pend='' or pend is NULL) or (forklift='' or forklift is NULL) or (stevedore='' or stevedore is NULL) or (passnum='' or passnum is NULL))"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Complete other details first.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                ofcnf = False
                ofcnfby = ""
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If ofcnf = True Then
                    ExecuteConfirm(strconn)
                End If

            Else
                MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
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

    Private Sub ExecuteConfirm(ByVal connectionString As String)
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

                'check if wala ng tblofitem na status is 1
                sql = "Select * from tblofitem where ofid='" & lblorfid.Text & "' and (status='1' or status='0')"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    MsgBox("Cannot confirm order fill# " & dr("ofnum") & ". " & dr("itemname") & " is still pending.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()

                'update tblorderfill
                sql = "Update tblorderfill set status='2', completedby='" & ofcnfby & "', completed=GetDate() where ofid='" & lblorfid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                '/deletetemp()

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully confirmed order fill# " & lblorf.Text & txtorf.Text & ".", MsgBoxStyle.Information, "")
                '/defaultform()
                '/btnsearch.PerformClick()
                btnprint.Enabled = True
                btnprint.PerformClick()
                btnprint.Enabled = False
                btnconfirm.Enabled = False
                btnothers.Enabled = False

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

    Public Sub viewofnum()
        Try
            If txtorf.ReadOnly = False Then
                sql = "Select * from tblorderfill where ofnum='" & lblorf.Text & Trim(txtorf.Text) & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        txtorf.ReadOnly = False
                        txtorf.Text = ""
                        txtorf.Focus()
                        defaultform()
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        txtorf.ReadOnly = False
                        txtorf.Text = ""
                        txtorf.Focus()
                        defaultform()
                        Exit Sub
                    End If

                    'check user level
                    lblorfid.Text = dr("ofid")
                    Dim s As String = dr("ofnum").ToString
                    txtorf.Text = s.Substring(3, s.Length - 3)
                    txtwrs.Text = dr("wrsnum")
                    txtwhse.Text = dr("whsename").ToString
                    txtcus.Text = dr("customer")
                    txtref.Text = dr("cusref")
                    txtplate.Text = dr("platenum")
                    txtdriver.Text = dr("driver").ToString
                    txtrems.Text = dr("remarks")
                    txtorf.ReadOnly = True
                    lblwhse.Text = dr("whsename")

                    If dr("status") = 2 Then
                        btnconfirm.Text = "Confirmed Order Fill"
                        Panel1.Enabled = False
                        btnconfirm.Enabled = False
                    Else
                        btnconfirm.Text = "Confirm Order Fill"
                        Panel1.Enabled = True
                        btnconfirm.Enabled = False
                    End If

                    txtorf.ReadOnly = True
                Else
                    MsgBox("Cannot find order fill number.", MsgBoxStyle.Critical, "")
                    txtorf.ReadOnly = False
                    txtorf.Text = ""
                    txtorf.Focus()
                    defaultform()
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if wala ng tblofitem na status is 1
                sql = "Select * from tblofitem where ofnum='" & lblorf.Text & txtorf.Text & "' and (status='1' or status='0') and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    btnconfirm.Enabled = False
                Else
                    If btnconfirm.Text <> "Confirmed Order Fill" Then
                        btnconfirm.Enabled = True
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            End If

            If txtorf.ReadOnly = True And Trim(txtorf.Text) <> "" Then
                Panel3.Enabled = True
            Else
                Panel3.Enabled = False
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

    Public Sub viewofid()
        Try
            If txtorf.ReadOnly = False Then
                sql = "Select * from tblorderfill where ofid='" & lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        txtorf.ReadOnly = False
                        txtorf.Text = ""
                        txtorf.Focus()
                        defaultform()
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        txtorf.ReadOnly = False
                        txtorf.Text = ""
                        txtorf.Focus()
                        defaultform()
                        Exit Sub
                    End If

                    'check user level
                    lblorfid.Text = dr("ofid")
                    Dim s As String = dr("ofnum").ToString
                    txtorf.Text = s.Substring(3, s.Length - 3)
                    txtwrs.Text = dr("wrsnum")
                    txtwhse.Text = dr("whsename").ToString
                    txtcus.Text = dr("customer")
                    txtref.Text = dr("cusref")
                    txtplate.Text = dr("platenum")
                    txtdriver.Text = dr("driver").ToString
                    txtrems.Text = dr("remarks")
                    txtorf.ReadOnly = True
                    lblwhse.Text = dr("whsename")

                    If dr("status") = 2 Then
                        btnconfirm.Text = "Confirmed Order Fill"
                        Panel1.Enabled = False
                        btnconfirm.Enabled = False
                    Else
                        btnconfirm.Text = "Confirm Order Fill"
                        Panel1.Enabled = True
                        btnconfirm.Enabled = False
                    End If

                    txtorf.ReadOnly = True
                Else
                    MsgBox("Cannot find order fill number.", MsgBoxStyle.Critical, "")
                    txtorf.ReadOnly = False
                    txtorf.Text = ""
                    txtorf.Focus()
                    defaultform()
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if wala ng tblofitem na status is 1
                sql = "Select * from tblofitem where ofnum='" & lblorf.Text & txtorf.Text & "' and (status='1' or status='0') and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    btnconfirm.Enabled = False
                Else
                    If btnconfirm.Text <> "Confirmed Order Fill" Then
                        btnconfirm.Enabled = True
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            End If

            If txtorf.ReadOnly = True And Trim(txtorf.Text) <> "" Then
                Panel3.Enabled = True
            Else
                Panel3.Enabled = False
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

    Private Sub txtwrs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwrs.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            If txtwrs.ReadOnly = False Then
                '/grdlog.Enabled = true
                Me.ContextMenuStrip1.Enabled = True
                viewwrsnum()
                viewitems()
            End If
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtwrs_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtwrs.ReadOnlyChanged
        If txtwrs.ReadOnly = True Then
            txtwrs.BackColor = Color.FromArgb(255, 224, 192)
        Else
            txtwrs.BackColor = Color.White
        End If
    End Sub

    Private Sub txtwrs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwrs.TextChanged

    End Sub

    Private Sub cmbitem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbitem.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            selectitemcmb()
        End If
    End Sub

    Private Sub cmbitem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitem.Leave

    End Sub

    Private Sub cmbitem_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitem.SelectedValueChanged
        selectitemcmb()
    End Sub

    Private Sub cmbitem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbitem.SelectedIndexChanged
        selectitemcmb()
    End Sub

    Public Sub selectitemcmb()
        Try
            If Trim(cmbitem.Text) <> "" Then
                Me.Cursor = Cursors.WaitCursor
                sql = "Select * from tblofitem where ofnum='" & lblorf.Text & txtorf.Text & "' and itemname='" & Trim(cmbitem.Text) & "' and status<>'3' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    txtitem.Text = dr("itemname")
                    txtofitemid.Text = dr("ofitemid")
                    txtbags.Text = dr("numbags")
                    Panelbuttons.Enabled = True
                    Me.CancelTicketToolStripMenuItem.Visible = True

                    If dr("status") = 2 Then
                        'meaning confirm for selected item only
                        '/grdtickets.Rows.Clear()
                        grdlog.Rows.Clear()
                        grdcancel.Rows.Clear()
                        cmbcancel.Items.Clear()

                        btnqasample.Text = "QA Sampling Completed"
                        btnqasample.Enabled = False
                        btncomplete.Text = "Selected Item Completed"
                        btncomplete.Enabled = False
                        btnpallet.Enabled = False
                        btnrefresh.Enabled = False
                        btnprint.Enabled = False
                        If btnconfirm.Text <> "Confirmed Order Fill" Then
                            btnconfirm.Enabled = True
                        Else
                            btnprint.Enabled = True
                        End If
                        btnsave.Enabled = False

                    Else
                        '/If dr("qasampling") = 0 Then
                        '/viewsavetbltempsperitem()

                        '/btnqasample.Enabled = True
                        '/btnqasample.Text = "QA Sampling Complete"
                        '/btncomplete.Enabled = False
                        '/btncomplete.Text = "Selected Item Complete"
                        '/btnpallet.Enabled = False
                        '/btnrefresh.Enabled = True
                        '/btnconfirm.Enabled = False
                        '/btnsave.Enabled = False

                        '/ElseIf dr("qasampling") = 1 Then
                        '/viewsavetbltempsperitem()

                        If dr("qasampling") = 0 Then
                            btnqasample.Text = "QA Sampling Complete"
                            btnqasample.Enabled = True
                        ElseIf dr("qasampling") = 1 Then
                            btnqasample.Text = "QA Sampling Completed"
                            btnqasample.Enabled = False
                        End If

                        btncomplete.Enabled = True
                        btncomplete.Text = "Selected Item Complete"
                        btnpallet.Enabled = True
                        btnrefresh.Enabled = True
                        btnconfirm.Enabled = False
                        btnprint.Enabled = False
                        btnsave.Enabled = True
                        '/End If
                    End If
                Else
                    txtitem.Text = ""
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                sql = "Select tickettype from tblitems where itemname='" & Trim(cmbitem.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    lbltype.Text = dr("tickettype").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                Me.Cursor = Cursors.Default

                If Val(txtbags.Text) <> 0 Then
                    cmbitem.Enabled = False
                End If

                If btncomplete.Text = "Selected Item Completed" Then
                    loadcompleted()
                Else

                    viewsavetbltempsperitem()
                    'can edit grdlogs
                    '/grdlog.Enabled = true
                    Me.ContextMenuStrip1.Enabled = True
                End If
            Else
                txtbags.Text = 0
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

    Public Sub viewsavetbltempsperitem()
        Try
            grdlog.Rows.Clear()
            cmbcancel.Items.Clear()
            grdcancel.Rows.Clear()

            'tbllogsheet
            Dim tbltempofitem As String = "tbltempofitem" & txtofitemid.Text
            Dim tblexistofitem As Boolean = False
            sql = "Select * from sys.tables where name = '" & tbltempofitem & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                tblexistofitem = True
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If tblexistofitem = True Then
                'generate first
                Me.Cursor = Cursors.WaitCursor

                ExecuteViewSavetbltempsperitem(strconn)

                Me.Cursor = Cursors.Default
                viewtickets()
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

    Private Sub ExecuteViewSavetbltempsperitem(ByVal connectionString As String)
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
                'view log in temporary table in grdlog where logticketid = txtlogticket.Text

                Dim tbltempnamelog As String = "tbltempoflog" & txtofitemid.Text
                Dim tblexistlog As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempnamelog & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistlog = True
                End If
                dr.Dispose()

                If tblexistlog = True Then
                    sql = "Select tbllogticket.letter1, tbllogticket.letter4, tbllogticket.astart, tbllogticket.fend, " & tbltempnamelog & ".oflogid, " & tbltempnamelog & ".logsheetdate, " & tbltempnamelog & ".logsheetnum, " & tbltempnamelog & ".logticketid, " & tbltempnamelog & ".palletnum, " & tbltempnamelog & ".selectedbags, " & tbltempnamelog & ".ticketseries, " & tbltempnamelog & ".picktickets"
                    sql = sql & " from " & tbltempnamelog & " left outer join tbllogticket on " & tbltempnamelog & ".palletnum=tbllogticket.palletnum"
                    sql = sql & " where " & tbltempnamelog & ".ofitemid='" & txtofitemid.Text & "' and tbllogticket.branch='" & login.branch & "'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    While dr.Read
                        Dim checkCell As Boolean = False
                        If IsDBNull(dr("picktickets")) = False Then
                            checkCell = dr("picktickets")
                        End If
                        '/MsgBox(checkCell.ToString)
                        grdlog.Rows.Add(dr("oflogid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("palletnum"), "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), 0, 0, 0, dr("selectedbags"), dr("ticketseries"), dr("logticketid"), checkCell)
                    End While
                    dr.Dispose()
                End If     'log////////////////////////////////////////////////////////////////////////////////


                'tapos i load lang yung mga cancel tickets para mag refresh lang ulet sila pati sa ticket series

                'view cancel tickets in temporary table in list1 where logticketid = txtlogticket.Text
                'view cancel tickets in temporary table in grdcancel where logticketid = txtlogticket.Text

                Dim tbltempnamecancel As String = "tbltempoflogcancel" & txtofitemid.Text
                Dim tblexistcancel As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempnamecancel & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistcancel = True
                End If
                dr.Dispose()

                If tblexistcancel = True Then
                    sql = "Select * from " & tbltempnamecancel & " where ofitemid='" & txtofitemid.Text & "'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    While dr.Read
                        cmbcancel.Items.Add(dr("cticketnum"))
                        grdcancel.Rows.Add(dr("logsheetnum"), dr("palletnum"), dr("letter"), dr("cticketnum"), dr("cticketdate"), dr("remarks"), dr("ticketid"), dr("cancelby").ToString)
                    End While
                    dr.Dispose()
                End If     'cancel////////////////////////////////////////////////////////////////////////////////

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default

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

    Private Sub txtrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            If txtorf.ReadOnly = True Then
                'parang warning para maisave muna kung may pending na order fill
                txtorf.ReadOnly = False
                txtorf.Focus()
                Panel3.Enabled = False
                '/grdlog.Enabled = False
                Me.ContextMenuStrip1.Enabled = False
                txtwrs.Text = ""
                txtwrs.ReadOnly = True
                If btnconfirm.Text <> "Confirmed Order Fill" Then

                Else
                    txtorf.ReadOnly = False
                    txtorf.Focus()
                    Panel3.Enabled = False
                End If
            Else
                '/grdlog.Enabled = true
                Me.ContextMenuStrip1.Enabled = True
                viewofnum()
                viewitems()
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

    Public Sub selectorderfill()
        Try
            If txtorf.ReadOnly = True Then
                'parang warning para maisave muna kung may pending na order fill
                txtorf.ReadOnly = False
                txtorf.Focus()
                Panel3.Enabled = False
                '/grdlog.Enabled = False
                Me.ContextMenuStrip1.Enabled = False
                txtwrs.Text = ""
                txtwrs.ReadOnly = True
                If btnconfirm.Text <> "Confirmed Order Fill" Then

                Else
                    txtorf.ReadOnly = False
                    txtorf.Focus()
                    Panel3.Enabled = False
                End If
            Else
                '/grdlog.Enabled = true
                Me.ContextMenuStrip1.Enabled = True
                viewofid()
                viewitems()
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

    Private Sub txtbags_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbags.TextChanged

    End Sub

    Private Sub NewToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton1.Click
        If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            If grdlog.Rows.Count <> 0 Then
                'dapat automatic mag ssave
                btnsave.PerformClick()
            End If
            txtorf.ReadOnly = False
            orderfillnew.ShowDialog()
            txtorf.ReadOnly = False
            btnsearch.PerformClick()
        Else
            MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
        End If
    End Sub

    Private Sub txtorf_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtorf.KeyDown

    End Sub

    Private Sub txtorf_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtorf.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If

        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            If txtorf.ReadOnly = False Then
                '/grdlog.Enabled = true
                Me.ContextMenuStrip1.Enabled = True
                viewofnum()
                viewitems()
            End If
        End If
    End Sub

    Private Sub txtorf_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtorf.TextChanged

    End Sub

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        If cmbitem.Enabled = False Then
            cmbitem.Enabled = True
            Panelbuttons.Enabled = False
            Me.CancelTicketToolStripMenuItem.Visible = False
            Me.EditNoOfSelectedBagsToolStripMenuItem.Visible = False
        End If
    End Sub

    Private Sub btncomplete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncomplete.Click
        Try
            If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then

                'check if orderfill is not cancelled
                sql = "Select * from tblorderfill where ofid='" & lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if ofitem is not cancelled
                sql = "Select * from tblofitem where ofitemid='" & txtofitemid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Item is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Item is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                If grdlog.Rows.Count = 0 Then
                    MsgBox("No selected tickets.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                'check if count selected bags is equal to numbags
                If Val(lblseltotal.Text) = Val(txtbags.Text) Then

                ElseIf Val(lblseltotal.Text) < Val(txtbags.Text) Then
                    MsgBox("Number of selected bags is less than the required number of bags.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                ElseIf Val(lblseltotal.Text) > Val(txtbags.Text) Then
                    MsgBox("Number of selected bags is greater than the required number of bags.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                If grdlog.Rows.Count <> 0 Then
                    'check if may errortext sa grdlogs series cells 11
                    Dim witherror As Boolean = False
                    For Each row As DataGridViewRow In grdlog.Rows
                        If grdlog.Rows(row.Index).Cells(11).ErrorText <> "" Then
                            MsgBox("There are some errors occured. Cannot save.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                    Next
                End If

                viewtickets()

                '/MsgBox("check muna kung valid yung mga nasa grdcancel lalo na kung galing to sa save as draft")

                'check if kulang yung bags sa grdlog 
                Dim countbags As Integer = 0
                For Each row As DataGridViewRow In grdlog.Rows
                    countbags += grdlog.Rows(row.Index).Cells(10).Value
                Next
                If Val(txtbags.Text) > Val(countbags) Then
                    MsgBox("Number of selected bags is less than the required number of bags.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If


                'check kung may qasampling na ung item
                '/MsgBox("check kung may qasampling na ung item")
                sql = "Select * from tblofitem where ofid='" & lblorfid.Text & "' and ofitemid='" & txtofitemid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("qasampling") = 0 Then
                        MsgBox("Cannot complete. QA Sampling first.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                Dim a As String = MsgBox("Are you sure " & cmbitem.Text & " is complete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbNo Then
                    Exit Sub
                End If

                'Update Complete itemssss
                ofcnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If ofcnf = True Then
                    ExecuteComplete(strconn)
                End If

            Else
                MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
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

    Private Sub ExecuteComplete(ByVal connectionString As String)
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
                'update tblofitem
                sql = "Update tblofitem set status='2', datemodified=GetDate(), modifiedby='" & login.user & "', remarks='" & Trim(txtrems.Text) & "'  where ofid='" & lblorfid.Text & "' and ofitemid='" & txtofitemid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'insert into tbloflog
                For Each row As DataGridViewRow In grdlog.Rows
                    Dim logdate As Date = CDate(grdlog.Rows(row.Index).Cells(1).Value)
                    Dim lognum As String = grdlog.Rows(row.Index).Cells(2).Value
                    Dim pallettag As String = grdlog.Rows(row.Index).Cells(3).Value
                    Dim availbags As Integer = grdlog.Rows(row.Index).Cells(9).Value
                    Dim selbags As Integer = grdlog.Rows(row.Index).Cells(10).Value
                    Dim seltickets As String = grdlog.Rows(row.Index).Cells(11).Value
                    Dim logticketid As String = grdlog.Rows(row.Index).Cells(12).Value
                    Dim checkCell As DataGridViewCheckBoxCell = CType(grdlog.Rows(row.Index).Cells(13), DataGridViewCheckBoxCell)

                    sql = "Insert into tbloflog (ofid, ofnum, ofitemid, logsheetdate, logsheetnum, logticketid, palletnum, selectedbags, ticketseries, picktickets, datecreated, createdby, datemodified, modifiedby, status) values ('" & lblorfid.Text & "', '" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', '" & logdate & "', '" & lognum & "', '" & logticketid & "', '" & pallettag & "', '" & selbags & "','" & seltickets & "','" & checkCell.Value & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    'update yung tbllogticket cusreserve and ofnum
                    If availbags = selbags Then
                        sql = "Update tbllogticket set status='0', cusreserve='0', ofnum=NULL where palletnum='" & pallettag & "' and branch='" & login.branch & "'"
                    Else
                        sql = "Update tbllogticket set cusreserve='0', ofnum=NULL where palletnum='" & pallettag & "' and cusreserve<>'1' and branch='" & login.branch & "'"
                    End If
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                'insert into tblofloggood
                For Each row As DataGridViewRow In grdselected.Rows
                    Dim ticketid As String = grdselected.Rows(row.Index).Cells(0).Value
                    Dim lognum As String = grdselected.Rows(row.Index).Cells(1).Value
                    Dim pallettag As String = grdselected.Rows(row.Index).Cells(2).Value
                    Dim ticketnum As String = grdselected.Rows(row.Index).Cells(3).Value
                    Dim tletter As String = grdselected.Rows(row.Index).Cells(4).Value

                    sql = "Insert into tblofloggood (ofid, ofnum, ofitemid, oflogid, logsheetnum, palletnum, tickettype, letter, gticketnum, remarks, status) values ('" & lblorfid.Text & "', '" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', (Select oflogid from tbloflog where ofnum='" & lblorf.Text & txtorf.Text & "' and logsheetnum='" & lognum & "' and palletnum='" & pallettag & "'), '" & lognum & "', '" & pallettag & "', '" & lbltype.Text & "', '" & tletter & "', '" & ticketnum & "', '', '1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    If ticketnum.ToString.Contains("D") = True Then
                        'tbllogdouble

                        'update tbllogdouble where ticketnum
                        sql = "Update tbllogdouble set status='0', ofid='" & lblorfid.Text & "' where logdoubleid='" & ticketid & "'"
                        command.CommandText = sql
                        command.ExecuteNonQuery()

                    Else
                        'tblloggood

                        'update tblloggood where ticketnum
                        sql = "Update tblloggood set status='0', ofid='" & lblorfid.Text & "' where loggoodid='" & ticketid & "'"
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    End If
                Next

                'insert into tbloflogcancel
                If grdcancel.Rows.Count <> 0 Then

                    For Each row As DataGridViewRow In grdcancel.Rows
                        Dim lognum As String = grdcancel.Rows(row.Index).Cells(0).Value
                        Dim pallettag As String = (grdcancel.Rows(row.Index).Cells(1).Value)
                        Dim tletter As String = grdcancel.Rows(row.Index).Cells(2).Value
                        Dim ticketnum As String = grdcancel.Rows(row.Index).Cells(3).Value
                        Dim tickettime As String = grdcancel.Rows(row.Index).Cells(4).Value
                        Dim disposition As String = grdcancel.Rows(row.Index).Cells(5).Value
                        Dim ticketid As String = grdcancel.Rows(row.Index).Cells(6).Value
                        Dim cancelby As String = grdcancel.Rows(row.Index).Cells(7).Value.ToString

                        sql = "Insert into tbloflogcancel (ofid, ofnum, ofitemid, oflogid, logsheetnum, palletnum, ticketid, tickettype, letter, cticketnum, cticketdate, remarks, cancelby, status) values ('" & lblorfid.Text & "', '" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', (Select oflogid from tbloflog where ofnum='" & lblorf.Text & txtorf.Text & "' and logsheetnum='" & lognum & "' and palletnum='" & pallettag & "'),'" & lognum & "','" & pallettag & "', '" & ticketid & "', '" & lbltype.Text & "','" & tletter & "','" & ticketnum & "','" & tickettime & "','" & disposition & "','" & cancelby & "','1')"
                        command.CommandText = sql
                        command.ExecuteNonQuery()

                        If ticketnum.ToString.Contains("D") = True Then
                            'tbllogdouble

                            'update tbllogdouble where ticketnum
                            sql = "Update tbllogdouble set status='0', ofid='" & lblorfid.Text & "' where logdoubleid='" & ticketid & "'"
                            command.CommandText = sql
                            command.ExecuteNonQuery()

                        Else
                            'tblloggood

                            'update tblloggood where ticketnum
                            sql = "Update tblloggood set status='0', ofid='" & lblorfid.Text & "' where loggoodid='" & ticketid & "'"
                            command.CommandText = sql
                            command.ExecuteNonQuery()
                        End If
                    Next
                End If


                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully complete Item " & Trim(cmbitem.Text) & ".", MsgBoxStyle.Information, "")
                refreshitem()
                grdselected.Rows.Clear()
                '/grdtickets.Rows.Clear()
                grdlog.Rows.Clear()
                orderfillpallets.grdselect.Rows.Clear()
                grdcancel.Rows.Clear()
                cmbcancel.Items.Clear()

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

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Try
            'print order fill
            rptorderfill.stat = ""
            rptorderfill.ofnum = lblorf.Text & txtorf.Text
            rptorderfill.ofid = lblorfid.Text
            rptorderfill.ShowDialog()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ItemsToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsToolStripButton1.Click
        If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            If txtorf.Text = "" Then
                MsgBox("Cannot set items. Search Order Fill # first.", MsgBoxStyle.Exclamation, "")
            Else
                'check if orderfill is not cancelled
                sql = "Select * from tblorderfill where ofid='" & lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                orderfillitems.txtofnum.Text = lblorf.Text & txtorf.Text
                orderfillitems.txtwrs.Text = txtwrs.Text
                orderfillitems.viewwhse()
                orderfillitems.cmbwhse.Text = txtwhse.Text
                orderfillitems.txtcus.Text = txtcus.Text
                orderfillitems.txtref.Text = txtref.Text
                orderfillitems.txtplate.Text = txtplate.Text
                orderfillitems.txtdriver.Text = txtdriver.Text
                orderfillitems.Panel1.Enabled = False
                orderfillitems.GroupBox2.Enabled = True
                orderfillitems.ShowDialog()
                grdtickets.Columns.Clear()
                grdselected.Rows.Clear()
                If Trim(cmbitem.Text) <> "" Then
                    cmbitem.Enabled = True
                    selectitemcmb()
                End If
                btnsearch.PerformClick()
                btnsearch.PerformClick()
            End If
        Else
            MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
        End If
    End Sub

    Private Sub UpdateToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateToolStripButton1.Click
        If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            If txtorf.Text = "" Then
                MsgBox("Cannot set items. Search Order Fill # first.", MsgBoxStyle.Exclamation, "")
            Else
                'check if orderfill is not cancelled
                sql = "Select * from tblorderfill where ofid='" & lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                orderfillitems.txtofnum.Text = lblorf.Text & txtorf.Text
                orderfillitems.txtwrs.Text = txtwrs.Text
                orderfillitems.viewwhse()
                orderfillitems.cmbwhse.Text = txtwhse.Text
                orderfillitems.txtcus.Text = txtcus.Text
                orderfillitems.txtref.Text = txtref.Text
                orderfillitems.txtplate.Text = txtplate.Text
                orderfillitems.txtdriver.Text = txtdriver.Text
                orderfillitems.Panel1.Enabled = True
                orderfillitems.GroupBox2.Enabled = False
                orderfillitems.ShowDialog()

                btnsearch.PerformClick()
                btnsearch.PerformClick()
            End If
        Else
            MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                'check if orderfill is not cancelled
                sql = "Select * from tblorderfill where ofid='" & lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if ofitem is not cancelled
                sql = "Select * from tblofitem where ofitemid='" & txtofitemid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Item is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Item is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if may selected na item or i disable kung walang selected na item

                'check if may nagenerate ba
                If grdlog.Rows.Count = 0 Then
                    Dim a As String = MsgBox("No record found. Generate first. Do you want to save it anyway?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                    If a <> vbYes Then
                        Exit Sub
                    End If
                Else
                    'check if may errortext sa grdlogs series cells 11
                    Dim witherror As Boolean = False
                    For Each row As DataGridViewRow In grdlog.Rows
                        If grdlog.Rows(row.Index).Cells(11).ErrorText <> "" Then
                            MsgBox("There are some errors occured. Cannot save.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                    Next
                End If


                'password
                ofcnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If ofcnf = True Then
                    ExecuteSaveasDraft(strconn)
                End If

            Else
                MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
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

    Private Sub ExecuteSaveasDraft(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                'gawing 0 - in process yung status ng tblofitem
                sql = "Update tblofitem set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where ofitemid='" & txtofitemid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'saving\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                'where ofitemid=lblofitemid.text

                'insert temporary tblofitem in tbltempofitem1
                'per items////////////////////////////////////////////////////////////////////////////
                Dim tbltempofitem As String = "tbltempofitem" & txtofitemid.Text
                Dim tblexistofitem As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempofitem & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistofitem = True
                End If
                dr.Dispose()

                If tblexistofitem = False Then
                    'create tbltempofitem1
                    sql = "Create Table " & tbltempofitem & " (ofitemid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofnum nvarchar(MAX), wrsnum nvarchar(MAX), itemname nvarchar(50), numbags int, oftype nvarchar(50), remarks nvarchar(MAX), datecreated datetime, createdby nvarchar(50), datemodified datetime, modifiedby nvarchar(50), status int, branch nvarchar(50))"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempofitem1 where lblofitemid
                    sql = "TRUNCATE Table " & tbltempofitem & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert ofitem in temporary table
                sql = "Insert into " & tbltempofitem & " (ofnum, wrsnum, itemname, numbags, remarks, datecreated, createdby, datemodified, modifiedby, status, branch) values ('" & txtorf.Text & "', '" & lblorf.Text & txtwrs.Text & "', '" & Trim(cmbitem.Text) & "', '" & Val(txtbags.Text) & "', '" & txtrems.Text & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1', '" & login.branch & "')"
                command.CommandText = sql
                command.ExecuteNonQuery()
                'per items////////////////////////////////////////////////////////////////////////////

                'insert temporary tbloflog in tbltempoflog
                'per log////////////////////////////////////////////////////////////////////////////
                Dim tbltempoflog As String = "tbltempoflog" & txtofitemid.Text
                Dim tblexistoflog As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempoflog & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistoflog = True
                End If
                dr.Dispose()

                If tblexistoflog = False Then
                    'create tbltempoflog
                    sql = "Create Table " & tbltempoflog & " (oflogid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofnum nvarchar(MAX), ofitemid	int, logsheetdate date, logsheetnum nvarchar(MAX), logticketid	int, palletnum nvarchar(MAX), selectedbags int, ticketseries nvarchar(MAX), datecreated datetime, createdby nvarchar(50), picktickets nvarchar(50))"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempoflog where lblofitemid
                    sql = "TRUNCATE Table " & tbltempoflog & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert oflog in temporary table
                For Each row As DataGridViewRow In grdlog.Rows
                    Dim logdate As String = grdlog.Rows(row.Index).Cells(1).Value
                    Dim lognum As String = grdlog.Rows(row.Index).Cells(2).Value
                    Dim pallettag As String = (grdlog.Rows(row.Index).Cells(3).Value)
                    Dim selectednum As String = grdlog.Rows(row.Index).Cells(10).Value
                    Dim seriesnum As String = grdlog.Rows(row.Index).Cells(11).Value
                    Dim logticketid As String = grdlog.Rows(row.Index).Cells(12).Value
                    Dim checkCell As DataGridViewCheckBoxCell = CType(grdlog.Rows(row.Index).Cells(13), DataGridViewCheckBoxCell)

                    sql = "Insert into " & tbltempoflog & " (ofnum, ofitemid, logsheetdate, logsheetnum, logticketid, palletnum, selectedbags, ticketseries, datecreated, createdby, picktickets) "
                    sql = sql & "values ('" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "','" & logdate & "','" & lognum & "','" & logticketid & "','" & pallettag & "','" & selectednum & "','" & seriesnum & "',GetDate(),'" & login.user & "', '" & checkCell.Value & "')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next
                'per log////////////////////////////////////////////////////////////////////////////

                'per log picktickets////////////////////////////////////////////////////////////////
                Dim tbltempoflogpick As String = "tbltempoflogpick" & txtofitemid.Text
                Dim tblexistoflogpick As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempoflogpick & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistoflogpick = True
                End If
                dr.Dispose()

                If tblexistoflogpick = False Then
                    'create tbltempoflogpick
                    sql = "Create Table " & tbltempoflogpick & " (ofloggoodid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofid int, ofnum nvarchar(MAX), ofitemid int, palletnum nvarchar(MAX), letter nvarchar(50), gticketnum nvarchar(MAX), status int, picked int)"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempoflogpick where lblofitemid
                    sql = "TRUNCATE Table " & tbltempoflogpick & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert oflog in temporary table
                For Each row As DataGridViewRow In grdtickets.Rows
                    If Val(grdtickets.Rows(row.Index).Cells(6).Value.ToString) = 1 Then
                        Dim pallettag As String = grdtickets.Rows(row.Index).Cells(2).Value
                        Dim letter As String = grdtickets.Rows(row.Index).Cells(5).Value
                        Dim ticknum As String = grdtickets.Rows(row.Index).Cells(4).Value
                        Dim picked As Integer = grdtickets.Rows(row.Index).Cells(6).Value

                        sql = "Insert into " & tbltempoflogpick & " (ofid, ofnum, ofitemid, palletnum, letter, gticketnum, status, picked) "
                        sql = sql & "values ('" & lblorfid.Text & "','" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', '" & pallettag & "', '" & letter & "', '" & ticknum & "', '1', '" & picked & "')"
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    End If
                Next

                'per log picktickets////////////////////////////////////////////////////////////////

                'insert temporary tbloflogcancel in tbltempoflogcancel
                'per logcancel////////////////////////////////////////////////////////////////////////////
                Dim tbltempoflogcancel As String = "tbltempoflogcancel" & txtofitemid.Text
                Dim tblexistoflogcancel As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempoflogcancel & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistoflogcancel = True
                End If
                dr.Dispose()

                If tblexistoflogcancel = False Then
                    'create tbltempoflogcancel
                    sql = "Create Table " & tbltempoflogcancel & " (oflogcancelid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofnum nvarchar(MAX), ofitemid	int, oflogid int, logsheetnum nvarchar(MAX), palletnum nvarchar(MAX), ticketid int, letter nvarchar(50), cticketnum int, cticketdate datetime, remarks	nvarchar(MAX), cancelby nvarchar(50), status int)"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempoflogcancel where lblofitemid
                    sql = "TRUNCATE Table " & tbltempoflogcancel & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert oflogcancel in temporary table
                For Each row As DataGridViewRow In grdcancel.Rows
                    Dim lognum As String = grdcancel.Rows(row.Index).Cells(0).Value
                    Dim pallettag As String = (grdcancel.Rows(row.Index).Cells(1).Value)
                    Dim tletter As String = grdcancel.Rows(row.Index).Cells(2).Value
                    Dim ticketnum As String = grdcancel.Rows(row.Index).Cells(3).Value
                    Dim tickettime As String = grdcancel.Rows(row.Index).Cells(4).Value
                    Dim disposition As String = grdcancel.Rows(row.Index).Cells(5).Value
                    Dim ticketid As String = grdcancel.Rows(row.Index).Cells(6).Value
                    Dim cancelby As String = grdcancel.Rows(row.Index).Cells(7).Value.ToString

                    sql = "Insert into " & tbltempoflogcancel & " (ofnum, ofitemid, oflogid, logsheetnum, palletnum, ticketid, letter, cticketnum, cticketdate, remarks, cancelby, status) values ('" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', (Select oflogid from tbloflog where ofnum='" & lblorf.Text & txtorf.Text & "' and logsheetnum='" & lognum & "'),'" & lognum & "','" & pallettag & "','" & ticketid & "','" & tletter & "','" & ticketnum & "','" & tickettime & "','" & disposition & "','" & cancelby & "','1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                'per logcancel////////////////////////////////////////////////////////////////////////////

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully Saved Order Fill# " & lblorf.Text & txtorf.Text & ".", MsgBoxStyle.Information, "")

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

    Public Sub savetbloflog()
        Try
            'insert temporary tbloflog in tbltempoflog
            'per log////////////////////////////////////////////////////////////////////////////
            Dim tbltempoflog As String = "tbltempoflog" & txtofitemid.Text
            Dim tblexistoflog As Boolean = False
            sql = "Select * from sys.tables where name = '" & tbltempoflog & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                tblexistoflog = True
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If tblexistoflog = False Then
                'create tbltempoflog
                sql = "Create Table " & tbltempoflog & " (oflogid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofnum nvarchar(MAX), ofitemid	int, logsheetdate date, logsheetnum nvarchar(MAX), logticketid	int, palletnum nvarchar(MAX), selectedbags int, ticketseries nvarchar(MAX), datecreated datetime, createdby nvarchar(50), picktickets nvarchar(50))"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
            Else
                'truncate tbltempoflog where lblofitemid
                sql = "TRUNCATE Table " & tbltempoflog & ""
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
            End If

            'insert oflog in temporary table
            For Each row As DataGridViewRow In grdlog.Rows
                Dim logdate As String = grdlog.Rows(row.Index).Cells(1).Value
                Dim lognum As String = grdlog.Rows(row.Index).Cells(2).Value
                Dim pallettag As String = (grdlog.Rows(row.Index).Cells(3).Value)
                Dim selectednum As String = grdlog.Rows(row.Index).Cells(10).Value
                Dim seriesnum As String = grdlog.Rows(row.Index).Cells(11).Value
                Dim logticketid As String = grdlog.Rows(row.Index).Cells(12).Value
                Dim checkCell As DataGridViewCheckBoxCell = CType(grdlog.Rows(row.Index).Cells(13), DataGridViewCheckBoxCell)

                sql = "Insert into " & tbltempoflog & " (ofnum, ofitemid, logsheetdate, logsheetnum, logticketid, palletnum, selectedbags, ticketseries, datecreated, createdby, picktickets) "
                sql = sql & "values ('" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "','" & logdate & "','" & lognum & "','" & logticketid & "','" & pallettag & "','" & selectednum & "','" & seriesnum & "',GetDate(),'" & login.user & "', '" & checkCell.Value & "')"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
            Next
            'per log////////////////////////////////////////////////////////////////////////////

            'per log picktickets////////////////////////////////////////////////////////////////
            Dim tbltempoflogpick As String = "tbltempoflogpick" & txtofitemid.Text
            Dim tblexistoflogpick As Boolean = False
            sql = "Select * from sys.tables where name = '" & tbltempoflogpick & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                tblexistoflogpick = True
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If tblexistoflogpick = False Then
                'create tbltempoflogpick
                sql = "Create Table " & tbltempoflogpick & " (ofloggoodid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofid int, ofnum nvarchar(MAX), ofitemid int, palletnum nvarchar(MAX), letter nvarchar(50), gticketnum nvarchar(MAX), status int, picked int)"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
            Else
                'truncate tbltempoflogpick where lblofitemid
                sql = "TRUNCATE Table " & tbltempoflogpick & ""
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
            End If

            'insert oflog in temporary table
            For Each row As DataGridViewRow In grdtickets.Rows
                If Val(grdtickets.Rows(row.Index).Cells(6).Value.ToString) = 1 Then
                    Dim pallettag As String = grdtickets.Rows(row.Index).Cells(2).Value
                    Dim letter As String = grdtickets.Rows(row.Index).Cells(5).Value
                    Dim ticknum As String = grdtickets.Rows(row.Index).Cells(4).Value
                    Dim picked As Integer = grdtickets.Rows(row.Index).Cells(6).Value

                    sql = "Insert into " & tbltempoflogpick & " (ofid, ofnum, ofitemid, palletnum, letter, gticketnum, status, picked) "
                    sql = sql & "values ('" & lblorfid.Text & "','" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', '" & pallettag & "', '" & letter & "', '" & ticknum & "', '1', '" & picked & "')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                End If
            Next

            'per log picktickets////////////////////////////////////////////////////////////////

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

    Public Sub savetbloflogcancel()
        Try
            'insert temporary tbloflogcancel in tbltempoflogcancel
            'per logcancel////////////////////////////////////////////////////////////////////////////
            Dim tbltempoflogcancel As String = "tbltempoflogcancel" & txtofitemid.Text
            Dim tblexistoflogcancel As Boolean = False
            sql = "Select * from sys.tables where name = '" & tbltempoflogcancel & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                tblexistoflogcancel = True
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If tblexistoflogcancel = False Then
                'create tbltempoflogcancel
                sql = "Create Table " & tbltempoflogcancel & " (oflogcancelid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofnum nvarchar(MAX), ofitemid	int, oflogid int, logsheetnum nvarchar(MAX), palletnum nvarchar(MAX), ticketid int, letter nvarchar(50), cticketnum int, cticketdate datetime, remarks	nvarchar(MAX), cancelby nvarchar(50), status int)"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
            Else
                'truncate tbltempoflogcancel where lblofitemid
                sql = "TRUNCATE Table " & tbltempoflogcancel & ""
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
            End If

            'insert oflogcancel in temporary table
            For Each row As DataGridViewRow In grdcancel.Rows
                Dim lognum As String = grdcancel.Rows(row.Index).Cells(0).Value
                Dim pallettag As String = (grdcancel.Rows(row.Index).Cells(1).Value)
                Dim tletter As String = grdcancel.Rows(row.Index).Cells(2).Value
                Dim ticketnum As String = grdcancel.Rows(row.Index).Cells(3).Value
                Dim tickettime As String = grdcancel.Rows(row.Index).Cells(4).Value
                Dim disposition As String = grdcancel.Rows(row.Index).Cells(5).Value
                Dim ticketid As String = grdcancel.Rows(row.Index).Cells(6).Value
                Dim cancelby As String = grdcancel.Rows(row.Index).Cells(7).Value.ToString

                sql = "Insert into " & tbltempoflogcancel & " (ofnum, ofitemid, oflogid, logsheetnum, palletnum, ticketid, letter, cticketnum, cticketdate, remarks, cancelby, status) values ('" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', (Select oflogid from tbloflog where ofnum='" & lblorf.Text & txtorf.Text & "' and logsheetnum='" & lognum & "'),'" & lognum & "','" & pallettag & "','" & ticketid & "','" & tletter & "','" & ticketnum & "','" & tickettime & "','" & disposition & "','" & cancelby & "','1')"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
            Next

            'per logcancel////////////////////////////////////////////////////////////////////////////

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

    Public Sub refreshitem()
        Try
            sql = "Select * from tblofitem where ofnum='" & lblorf.Text & txtorf.Text & "' and itemname='" & Trim(cmbitem.Text) & "' and status<>'3' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                txtofitemid.Text = dr("ofitemid")
                txtbags.Text = dr("numbags")
                Panelbuttons.Enabled = True
                Me.CancelTicketToolStripMenuItem.Visible = True

                If dr("status") = 2 Then
                    'meaning confirm for selected item only
                    btncomplete.Text = "Selected Item Completed"
                    btncomplete.Enabled = False
                    btnpallet.Enabled = False
                    btnrefresh.Enabled = False
                    btnprint.Enabled = False
                    If btnconfirm.Text <> "Confirmed Order Fill" Then
                        btnconfirm.Enabled = True
                    Else
                        btnprint.Enabled = True
                    End If
                    btnsave.Enabled = False
                Else
                    viewsavetbltempsperitem()

                    btncomplete.Enabled = True
                    btncomplete.Text = "Selected Item Complete"
                    btnpallet.Enabled = True
                    btnrefresh.Enabled = True
                    btnconfirm.Enabled = False
                    btnprint.Enabled = False
                    btnsave.Enabled = True
                End If
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

    Private Sub btnwrssearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnwrssearch.Click
        Try
            If txtwrs.ReadOnly = True Then
                'parang warning para maisave muna kung may pending na order fill
                txtwrs.ReadOnly = False
                txtwrs.Focus()
                Panel3.Enabled = False
                '/grdlog.Enabled = False
                Me.ContextMenuStrip1.Enabled = False
                txtorf.Text = ""
                txtorf.ReadOnly = True
                If btnconfirm.Text <> "Confirmed Order Fill" Then

                Else
                    txtwrs.ReadOnly = False
                    txtwrs.Focus()
                    Panel3.Enabled = False
                End If
            Else
                '/grdlog.Enabled = true
                Me.ContextMenuStrip1.Enabled = True
                viewwrsnum()
                viewitems()
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

    Public Sub viewwrsnum()
        Try
            If txtwrs.ReadOnly = False Then
                sql = "Select * from tblorderfill where wrsnum='" & Trim(txtwrs.Text) & "' and status='1' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    'check user level
                    lblorfid.Text = dr("ofid")
                    Dim s As String = dr("ofnum").ToString
                    txtorf.Text = s.Substring(3, s.Length - 3)
                    txtwrs.Text = dr("wrsnum")
                    txtwhse.Text = dr("whsename").ToString
                    txtcus.Text = dr("customer")
                    txtref.Text = dr("cusref")
                    txtplate.Text = dr("platenum")
                    txtdriver.Text = dr("driver").ToString
                    txtrems.Text = dr("remarks")
                    txtwrs.ReadOnly = True

                    If dr("status") = 2 Then
                        btnconfirm.Text = "Confirmed Order Fill"
                        Panel1.Enabled = False
                        btnconfirm.Enabled = False
                    Else
                        btnconfirm.Text = "Confirm Order Fill"
                        Panel1.Enabled = True
                        btnconfirm.Enabled = False
                    End If
                Else
                    MsgBox("Cannot find WRS number.", MsgBoxStyle.Critical, "")
                    txtwrs.Text = ""
                    txtwrs.Focus()
                    defaultform()
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if wala ng tblofitem na status is 1
                sql = "Select * from tblofitem where ofnum='" & lblorf.Text & txtwrs.Text & "' and (status='1' or status='0') and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    btnconfirm.Enabled = False
                Else
                    If btnconfirm.Text <> "Confirmed Order Fill" Then
                        btnconfirm.Enabled = True
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            End If

            If txtwrs.ReadOnly = True And Trim(txtwrs.Text) <> "" Then
                Panel3.Enabled = True
            Else
                Panel3.Enabled = False
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

    Private Sub SelectToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectToolStripButton1.Click
        If grdlog.Rows.Count <> 0 Then
            'dapat automatic mag ssave
            Dim a As String = MsgBox("Do you want to save first?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
            If a = vbYes Then
                ofcnf = False
                btnsave.PerformClick()
                If ofcnf = False Then
                    Exit Sub
                End If
            End If
        End If


        txtorf.ReadOnly = False
        orderfillselect.ShowDialog()
    End Sub

    Private Sub btnpallet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpallet.Click
        Try
            If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                orderfillpallets.orderfillitemid = txtofitemid.Text
                orderfillpallets.lblwhse.Text = lblwhse.Text
                orderfillpallets.datefrom.Value = Date.Now
                orderfillpallets.dateto.Value = Date.Now
                '/MsgBox("if grdlog is not zero then ung selected nya dapat yun din ang selected sa grdselect sa orderfillpallet")

                If grdlog.Rows.Count <> 0 Then
                    For Each rowlog As DataGridViewRow In grdlog.Rows
                        For Each rowselect As DataGridViewRow In orderfillpallets.grdselect.Rows
                            If orderfillpallets.grdselect.Rows(rowselect.Index).Cells(3).Value = grdlog.Rows(rowlog.Index).Cells(3).Value Then
                                orderfillpallets.grdselect.Rows(rowselect.Index).Cells(8).Value = grdlog.Rows(rowlog.Index).Cells(10).Value
                                orderfillpallets.grdselect.Rows(rowselect.Index).Cells(10).Value = grdlog.Rows(rowlog.Index).Cells(13).Value
                            End If
                        Next
                    Next
                End If

                orderfillpallets.ShowDialog()
                viewtickets()
                autosave()

                For i = 0 To 11
                    grdlog.Columns(i).ReadOnly = True
                Next
                '/grdlog.Columns(13).ReadOnly = False
            Else
                MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
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

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Try
            'refresh lahat mula sa orderfill, wrs details then refershtickets then viewselectedpallets
            '/grdlog.Enabled = true
            Me.ContextMenuStrip1.Enabled = True
            viewofnum()
            viewitems()
            btnsearch.PerformClick()
            btnsearch.PerformClick()


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

    Public Sub countselected()
        Dim ctr As Integer = 0
        If grdlog.Rows.Count <> 0 Then
            For Each row As DataGridViewRow In grdlog.Rows
                ctr = ctr + grdlog.Rows(row.Index).Cells(10).Value
            Next
        End If
        lblseltotal.Text = ctr
    End Sub

    Private Sub EditNoOfSelectedBagsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditNoOfSelectedBagsToolStripMenuItem.Click
        Try
            If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                grdlog.ReadOnly = False
                For i = 0 To 13
                    grdlog.Columns(i).ReadOnly = True
                Next
                b4edit = grdlog.Rows(rowindex).Cells(10).Value
                grdlog.Rows(rowindex).Cells(10).ReadOnly = False
                grdlog.Rows(rowindex).Cells(10).Style.BackColor = Color.HotPink
                grdlog.Rows(rowindex).Cells(10).Selected = True
                grdlog.BeginEdit(True)

            Else
                MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
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

    Public Sub viewtickets()
        Try
            If grdlog.Rows.Count = 0 Then
                '/grdtickets.Rows.Clear()
                grdcancel.Rows.Clear()
                cmbcancel.Items.Clear()
                lblseltotal.Text = "0.00"

            Else
                If btncomplete.Text <> "Selected Item Completed" Then
                    Me.Cursor = Cursors.WaitCursor

                    ExecuteViewTickets(strconn)

                    Me.Cursor = Cursors.Default
                    viewselectedtickets()

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

    Private Sub ExecuteViewTickets(ByVal connectionString As String)
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
                Dim withtemptickets As Boolean = False
                Dim table = New DataTable()
                table.Columns.Add("loggoodid", GetType(Integer))
                table.Columns.Add("logsheetnum", GetType(String))
                table.Columns.Add("palletnum", GetType(String))
                table.Columns.Add("gticketnum", GetType(String))
                table.Columns.Add("Number", GetType(Integer))
                table.Columns.Add("Letter", GetType(Char))
                table.Columns.Add("Picked", GetType(String))

                'generate grdtickets
                For Each row As DataGridViewRow In grdlog.Rows
                    Dim pallet As String = grdlog.Rows(row.Index).Cells(3).Value
                    Dim checkCell As DataGridViewCheckBoxCell = CType(grdlog.Rows(row.Index).Cells(13), DataGridViewCheckBoxCell)

                    '/Dim picktick As Boolean = grdlog.Rows(row.Index).Cells(13).Value

                    If checkCell.Value = False Then
                        sql = "Select * from tblloggood where palletnum='" & pallet & "' and status='1' and branch='" & login.branch & "'"
                        command.CommandText = sql
                        dr = command.ExecuteReader
                        While dr.Read
                            table.Rows.Add(dr("loggoodid"), dr("logsheetnum"), dr("palletnum"), dr("gticketnum").ToString, Val(dr("gticketnum")), dr("letter"), "")
                            '/grdtickets.Rows.Add(dr("loggoodid"), dr("logsheetnum"), dr("palletnum"), dr("gticketnum").ToString, Val(dr("gticketnum")), dr("letter"))
                        End While
                        dr.Dispose()

                        sql = "Select * from tbllogdouble where palletnum='" & pallet & "' and status='1' and branch='" & login.branch & "'"
                        command.CommandText = sql
                        dr = command.ExecuteReader
                        While dr.Read
                            table.Rows.Add(dr("logdoubleid"), dr("logsheetnum"), dr("palletnum"), dr("dticketnum").ToString, Val(dr("dticketnum")), dr("letter"), "")
                            '/grdtickets.Rows.Add(dr("logdoubleid"), dr("logsheetnum"), dr("palletnum"), dr("dticketnum").ToString, Val(dr("dticketnum")), dr("letter"))
                        End While
                        dr.Dispose()
                        '//////////////////////////////////////////////

                    ElseIf checkCell.Value = True Then
                        Dim existing As Boolean = False
                        For Each rowticket As DataGridViewRow In grdtickets.Rows
                            Dim tickpallet As String = grdtickets.Rows(rowticket.Index).Cells(2).Value
                            If pallet = tickpallet Then
                                existing = True
                                Exit For
                            End If
                        Next
                        'check sa grdtickets para dun nlng kokopyahin
                        If existing = True Then
                            For Each rowticket As DataGridViewRow In grdtickets.Rows
                                Dim tickpallet As String = grdtickets.Rows(rowticket.Index).Cells(2).Value

                                Dim col0 As Integer = grdtickets.Rows(rowticket.Index).Cells(0).Value
                                Dim col1 As String = grdtickets.Rows(rowticket.Index).Cells(1).Value
                                Dim col2 As String = grdtickets.Rows(rowticket.Index).Cells(2).Value
                                Dim col3 As String = grdtickets.Rows(rowticket.Index).Cells(3).Value
                                Dim col4 As Integer = grdtickets.Rows(rowticket.Index).Cells(4).Value
                                Dim col5 As String = grdtickets.Rows(rowticket.Index).Cells(5).Value
                                Dim col6 As String = grdtickets.Rows(rowticket.Index).Cells(6).Value

                                If pallet = tickpallet Then
                                    Dim meronna As Boolean = False

                                    For i = 0 To table.Rows.Count - 1
                                        If table.Rows(i).Item(3) = col3 And table.Rows(i).Item(5) = col5 Then
                                            meronna = True
                                            Exit For
                                        End If
                                    Next

                                    If meronna = False Then
                                        table.Rows.Add(col0, col1, col2, col3, col4, col5, col6)
                                    End If
                                End If
                            Next
                        Else
                            'get tickets sa database update lang yung pick status kung 1 or 0
                            withtemptickets = True

                            sql = "Select * from tblloggood where palletnum='" & pallet & "' and status='1' and branch='" & login.branch & "'"
                            command.CommandText = sql
                            dr = command.ExecuteReader
                            While dr.Read
                                table.Rows.Add(dr("loggoodid"), dr("logsheetnum"), dr("palletnum"), dr("gticketnum").ToString, Val(dr("gticketnum")), dr("letter"), "")
                                '/grdtickets.Rows.Add(dr("loggoodid"), dr("logsheetnum"), dr("palletnum"), dr("gticketnum").ToString, Val(dr("gticketnum")), dr("letter"))
                            End While
                            dr.Dispose()

                            sql = "Select * from tbllogdouble where palletnum='" & pallet & "' and status='1' and branch='" & login.branch & "'"
                            command.CommandText = sql
                            dr = command.ExecuteReader
                            While dr.Read
                                table.Rows.Add(dr("logdoubleid"), dr("logsheetnum"), dr("palletnum"), dr("dticketnum").ToString, Val(dr("dticketnum")), dr("letter"), "")
                                '/grdtickets.Rows.Add(dr("logdoubleid"), dr("logsheetnum"), dr("palletnum"), dr("dticketnum").ToString, Val(dr("dticketnum")), dr("letter"))
                            End While
                            dr.Dispose()
                        End If
                    End If
                Next

                '/grdtickets.Sort(grdtickets.Columns(4), System.ComponentModel.ListSortDirection.Ascending)
                '//apply sort on Letter, then Number column
                table.DefaultView.Sort = "Letter, Number"

                grdtickets.Columns.Clear()
                Me.grdtickets.DataSource = table
                If withtemptickets = True Then
                    'view pick tickets in temporary table 
                    Dim tbltempnamepick As String = "tbltempoflogpick" & txtofitemid.Text
                    Dim tblexistpick As Boolean = False
                    sql = "Select * from sys.tables where name = '" & tbltempnamepick & "'"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    If dr.Read Then
                        tblexistpick = True
                    End If
                    dr.Dispose()

                    If tblexistpick = True Then
                        For Each rowticket As DataGridViewRow In grdtickets.Rows
                            Dim tickpallet As String = grdtickets.Rows(rowticket.Index).Cells(2).Value
                            Dim tickletter As String = grdtickets.Rows(rowticket.Index).Cells(5).Value
                            Dim ticknumber As String = grdtickets.Rows(rowticket.Index).Cells(4).Value

                            sql = "Select * from " & tbltempnamepick & " where ofitemid='" & txtofitemid.Text & "' and palletnum='" & tickpallet & "' and letter='" & tickletter & "' and gticketnum='" & ticknumber & "'"
                            command.CommandText = sql
                            dr = command.ExecuteReader
                            If dr.Read Then
                                grdtickets.Rows(rowticket.Index).Cells(6).Value = dr("picked")
                            Else
                                grdtickets.Rows(rowticket.Index).Cells(6).Value = 0
                            End If
                            dr.Dispose()
                        Next
                    End If     'pick////////////////////////////////////////////////////////////////////////////////
                End If

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

    Public Sub viewselectedtickets()
        Try
            countcancel()

            If grdtickets.Rows.Count <> 0 Then
                Me.Cursor = Cursors.WaitCursor

                grdselected.Rows.Clear()

                'generate grdselected
                For Each rowlogs As DataGridViewRow In grdlog.Rows
                    Dim pallet As String = grdlog.Rows(rowlogs.Index).Cells(3).Value
                    Dim availnum As Integer = grdlog.Rows(rowlogs.Index).Cells(9).Value
                    Dim selectednum As Integer = grdlog.Rows(rowlogs.Index).Cells(10).Value
                    Dim temptotal As Integer = 0

                    Dim checkCell As DataGridViewCheckBoxCell = CType(grdlog.Rows(rowlogs.Index).Cells(13), DataGridViewCheckBoxCell)
                    'Button1.PerformClick()
                    '/MsgBox(checkCell.Value)
                    If checkCell.Value = False Then
                        'walang binago
                        If btncomplete.Text <> "Selected Item Completed" Then
                            If selectednum < availnum Then
                                For i = grdtickets.Rows.Count - 1 To 0 Step -1
                                    Dim pid As String = grdtickets.Rows(i).Cells(0).Value
                                    Dim plog As String = grdtickets.Rows(i).Cells(1).Value
                                    Dim ptag As String = grdtickets.Rows(i).Cells(2).Value
                                    Dim pticket As String = grdtickets.Rows(i).Cells(3).Value
                                    Dim pletter As String = grdtickets.Rows(i).Cells(5).Value

                                    If ptag = pallet Then
                                        'tignan if nasa grdcancel
                                        Dim nasacancel As Boolean = False
                                        For Each rowcancel As DataGridViewRow In grdcancel.Rows
                                            Dim cticket As String = grdcancel.Rows(rowcancel.Index).Cells(3).Value
                                            If cticket = pticket Then
                                                nasacancel = True
                                                Exit For
                                            End If
                                        Next

                                        If nasacancel = False Then
                                            temptotal += 1
                                            If temptotal <= selectednum Then
                                                grdselected.Rows.Add(pid, plog, ptag, pticket, pletter)
                                            Else
                                                Exit For
                                            End If
                                        End If
                                    End If
                                Next

                            ElseIf selectednum = availnum Then
                                For Each rowticket As DataGridViewRow In grdtickets.Rows
                                    Dim pid As String = grdtickets.Rows(rowticket.Index).Cells(0).Value
                                    Dim plog As String = grdtickets.Rows(rowticket.Index).Cells(1).Value
                                    Dim ptag As String = grdtickets.Rows(rowticket.Index).Cells(2).Value
                                    Dim pticket As String = grdtickets.Rows(rowticket.Index).Cells(3).Value
                                    Dim pletter As String = grdtickets.Rows(rowticket.Index).Cells(5).Value

                                    If ptag = pallet Then
                                        'tignan if nasa grdcancel
                                        Dim nasacancel As Boolean = False
                                        For Each rowcancel As DataGridViewRow In grdcancel.Rows
                                            Dim cticket As String = grdcancel.Rows(rowcancel.Index).Cells(3).Value
                                            If cticket = pticket Then
                                                nasacancel = True
                                                Exit For
                                            End If
                                        Next

                                        If nasacancel = False Then
                                            temptotal += 1
                                            If temptotal <= selectednum Then
                                                grdselected.Rows.Add(pid, plog, ptag, pticket, pletter)
                                            Else
                                                Exit For
                                            End If
                                        End If
                                    End If
                                Next
                            End If

                        Else
                            'view completed na
                            For Each rowticket As DataGridViewRow In grdtickets.Rows
                                Dim pid As String = grdtickets.Rows(rowticket.Index).Cells(0).Value
                                Dim plog As String = grdtickets.Rows(rowticket.Index).Cells(1).Value
                                Dim ptag As String = grdtickets.Rows(rowticket.Index).Cells(2).Value
                                Dim pticket As String = grdtickets.Rows(rowticket.Index).Cells(3).Value
                                Dim pletter As String = grdtickets.Rows(rowticket.Index).Cells(5).Value

                                If ptag = pallet Then
                                    'tignan if nasa grdcancel
                                    Dim nasacancel As Boolean = False
                                    For Each rowcancel As DataGridViewRow In grdcancel.Rows
                                        Dim cticket As String = grdcancel.Rows(rowcancel.Index).Cells(3).Value
                                        If cticket = pticket Then
                                            nasacancel = True
                                            Exit For
                                        End If
                                    Next

                                    If nasacancel = False Then
                                        temptotal += 1
                                        If temptotal <= selectednum Then
                                            grdselected.Rows.Add(pid, plog, ptag, pticket, pletter)
                                        Else
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                        End If '/////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    ElseIf checkCell.Value = True Then
                        '/////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        If btncomplete.Text <> "Selected Item Completed" Then
                            If selectednum < availnum Then
                                For i = grdtickets.Rows.Count - 1 To 0 Step -1
                                    Dim pid As String = grdtickets.Rows(i).Cells(0).Value
                                    Dim plog As String = grdtickets.Rows(i).Cells(1).Value
                                    Dim ptag As String = grdtickets.Rows(i).Cells(2).Value
                                    Dim pticket As String = grdtickets.Rows(i).Cells(3).Value
                                    Dim pletter As String = grdtickets.Rows(i).Cells(5).Value
                                    Dim ppick As Integer = Val(grdtickets.Rows(i).Cells(6).Value)

                                    If ptag = pallet And ppick = 1 Then
                                        'tignan if nasa grdcancel
                                        Dim nasacancel As Boolean = False
                                        For Each rowcancel As DataGridViewRow In grdcancel.Rows
                                            Dim cticket As String = grdcancel.Rows(rowcancel.Index).Cells(3).Value
                                            If cticket = pticket Then
                                                nasacancel = True
                                                Exit For
                                            End If
                                        Next

                                        If nasacancel = False Then
                                            temptotal += 1
                                            If temptotal <= selectednum Then
                                                grdselected.Rows.Add(pid, plog, ptag, pticket, pletter)
                                            Else
                                                Exit For
                                            End If
                                        End If
                                    End If
                                Next

                            ElseIf selectednum = availnum Then
                                For Each rowticket As DataGridViewRow In grdtickets.Rows
                                    Dim pid As String = grdtickets.Rows(rowticket.Index).Cells(0).Value
                                    Dim plog As String = grdtickets.Rows(rowticket.Index).Cells(1).Value
                                    Dim ptag As String = grdtickets.Rows(rowticket.Index).Cells(2).Value
                                    Dim pticket As String = grdtickets.Rows(rowticket.Index).Cells(3).Value
                                    Dim pletter As String = grdtickets.Rows(rowticket.Index).Cells(5).Value
                                    Dim ppick As Integer = Val(grdtickets.Rows(rowticket.Index).Cells(6).Value)

                                    If ptag = pallet And ppick = 1 Then
                                        'tignan if nasa grdcancel
                                        Dim nasacancel As Boolean = False
                                        For Each rowcancel As DataGridViewRow In grdcancel.Rows
                                            Dim cticket As String = grdcancel.Rows(rowcancel.Index).Cells(3).Value
                                            If cticket = pticket Then
                                                nasacancel = True
                                                Exit For
                                            End If
                                        Next

                                        If nasacancel = False Then
                                            temptotal += 1
                                            If temptotal <= selectednum Then
                                                grdselected.Rows.Add(pid, plog, ptag, pticket, pletter)
                                            Else
                                                Exit For
                                            End If
                                        End If
                                    End If
                                Next
                            End If

                        Else
                            'view completed na
                            For Each rowticket As DataGridViewRow In grdtickets.Rows
                                Dim pid As String = grdtickets.Rows(rowticket.Index).Cells(0).Value
                                Dim plog As String = grdtickets.Rows(rowticket.Index).Cells(1).Value
                                Dim ptag As String = grdtickets.Rows(rowticket.Index).Cells(2).Value
                                Dim pticket As String = grdtickets.Rows(rowticket.Index).Cells(3).Value
                                Dim pletter As String = grdtickets.Rows(rowticket.Index).Cells(5).Value
                                Dim ppick As Integer = Val(grdtickets.Rows(rowticket.Index).Cells(6).Value)

                                If ptag = pallet And ppick = 1 Then
                                    'tignan if nasa grdcancel
                                    Dim nasacancel As Boolean = False
                                    For Each rowcancel As DataGridViewRow In grdcancel.Rows
                                        Dim cticket As String = grdcancel.Rows(rowcancel.Index).Cells(3).Value
                                        If cticket = pticket Then
                                            nasacancel = True
                                            Exit For
                                        End If
                                    Next

                                    If nasacancel = False Then
                                        temptotal += 1
                                        If temptotal <= selectednum Then
                                            grdselected.Rows.Add(pid, plog, ptag, pticket, pletter)
                                        Else
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                Next

                Me.Cursor = Cursors.Default
            End If

            generateseries()
            countselected()


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

    Public Sub generateseries()
        Try
            For Each rowlog As DataGridViewRow In grdlog.Rows
                list1.Items.Clear()
                list2.Items.Clear()

                Dim pallettagnum As String = grdlog.Rows(rowlog.Index).Cells(3).Value

                Dim table = New DataTable()
                table.Columns.Add("LetterNum", GetType(String))
                table.Columns.Add("Number", GetType(Integer))
                table.Columns.Add("Letter", GetType(Char))

                For Each rowtic As DataGridViewRow In grdselected.Rows
                    If grdselected.Rows(rowtic.Index).Cells(2).Value = pallettagnum Then
                        If grdselected.Rows(rowtic.Index).Cells(3).Value.ToString.Contains("D") = True Then
                            'double
                            list2.Items.Add(grdselected.Rows(rowtic.Index).Cells(4).Value & " " & grdselected.Rows(rowtic.Index).Cells(3).Value)
                        Else
                            'good
                            '/list1.Items.Add(grdselected.Rows(rowtic.Index).Cells(3).Value & " " & grdselected.Rows(rowtic.Index).Cells(4).Value)
                            '/grdgoods.Rows.Add(grdselected.Rows(rowtic.Index).Cells(3).Value & " " & grdselected.Rows(rowtic.Index).Cells(4).Value, grdselected.Rows(rowtic.Index).Cells(3).Value, grdselected.Rows(rowtic.Index).Cells(4).Value)
                            table.Rows.Add(grdselected.Rows(rowtic.Index).Cells(3).Value & " " & grdselected.Rows(rowtic.Index).Cells(4).Value, grdselected.Rows(rowtic.Index).Cells(3).Value, grdselected.Rows(rowtic.Index).Cells(4).Value)
                        End If
                    End If
                Next

                '////grdgoods.Sort(grdgoods.Columns(1), System.ComponentModel.ListSortDirection.Ascending)

                '//apply sort on Letter, then Number column
                table.DefaultView.Sort = "Letter, Number"

                grdgoods.Columns.Clear()
                Me.grdgoods.DataSource = table

                For Each row As DataGridViewRow In grdgoods.Rows
                    list1.Items.Add(grdgoods.Rows(row.Index).Cells(0).Value)
                Next

                'generate series
                Dim ctrlist As Integer = 0
                Dim temp As String = "", first As String = "", last As String = ""
                Dim letter1 As String = "", letter2 As String = ""

                For Each item As Object In list1.Items
                    ctrlist += 1

                    If first = "" Then
                        letter1 = item
                        letter1 = letter1.Substring(letter1.Length - 1)

                        first = Val(item)
                    End If

                    letter2 = item
                    letter2 = letter2.Substring(letter2.Length - 1)

                    temp = Val(item)
                    last = temp

                    If list1.Items.Count >= ctrlist Then
                        If ctrlist > list1.Items.Count - 1 Then
                            If first = temp Then
                                Dim gtiket As String = first
                                Dim tempzero As String = ""

                                If gtiket < 1000000 Then
                                    For vv As Integer = 1 To 6 - gtiket.Length
                                        '/tempzero += "0"
                                    Next
                                End If


                                list2.Items.Add(letter1 & " " & tempzero & gtiket)
                                '/MsgBox(" ---1")
                                last = gtiket

                            Else
                                Dim gtiket As String = first
                                Dim tempzero As String = ""
                                If gtiket < 1000000 Then
                                    For vv As Integer = 1 To 6 - gtiket.Length
                                        '/tempzero += "0"
                                    Next
                                End If


                                Dim gtikettemp As String = temp
                                Dim tempzerotemp As String = ""
                                If gtikettemp < 1000000 Then
                                    For vv As Integer = 1 To 6 - gtikettemp.Length
                                        '/tempzerotemp += "0"
                                    Next
                                End If


                                list2.Items.Add(letter1 & " " & tempzero & gtiket & " - " & letter2 & " " & tempzerotemp & gtikettemp)
                                '/MsgBox("---2")
                                last = gtikettemp
                            End If
                        Else
                            If temp + 1 < Val(list1.Items(ctrlist)) Then
                                If first = temp Then
                                    Dim gtiket As String = first
                                    Dim tempzero As String = ""

                                    If gtiket < 1000000 Then
                                        For vv As Integer = 1 To 6 - gtiket.Length
                                            '/tempzero += "0"
                                        Next
                                    End If


                                    list2.Items.Add(letter1 & " " & tempzero & gtiket)
                                    '/MsgBox(" ---3")
                                    last = gtiket
                                Else
                                    Dim gtiket As String = first
                                    Dim tempzero As String = ""
                                    If gtiket < 1000000 Then
                                        For vv As Integer = 1 To 6 - gtiket.Length
                                            '/tempzero += "0"
                                        Next
                                    End If


                                    Dim gtikettemp As String = temp
                                    Dim tempzerotemp As String = ""
                                    If gtikettemp < 1000000 Then
                                        For vv As Integer = 1 To 6 - gtikettemp.Length
                                            '/tempzerotemp += "0"
                                        Next
                                    End If


                                    list2.Items.Add(letter1 & " " & tempzero & gtiket & " - " & letter2 & " " & tempzerotemp & gtikettemp)
                                    '/MsgBox(" ---4")
                                    last = gtikettemp
                                End If
                                first = ""

                            ElseIf temp + 1 > Val(list1.Items(ctrlist)) Then
                                'next series na ibig sabihin mag kaiba na letter nito
                                '/MsgBox(temp + 1 & " " & Val(list1.Items(ctrlist)))

                                Dim gtiket As String = first
                                Dim tempzero As String = ""
                                If gtiket < 1000000 Then
                                    For vv As Integer = 1 To 6 - gtiket.Length
                                        '/tempzero += "0"
                                    Next
                                End If


                                Dim gtiketlast As String = Val(last)
                                Dim tempzerotemp As String = ""
                                If gtiketlast < 1000000 Then
                                    For vv As Integer = 1 To 6 - gtiketlast.Length
                                        '/tempzerotemp += "0"
                                    Next
                                End If

                                list2.Items.Add(letter1 & " " & tempzero & gtiket & " - " & letter2 & " " & tempzerotemp & gtiketlast)
                                '/MsgBox(" ---5")
                                first = ""
                            End If
                        End If
                    End If
                Next


                Dim ofseries As String = ""
                For Each item As Object In list2.Items
                    ofseries = ofseries & item & ", "
                Next
                grdlog.Item(11, rowlog.Index).Value = ofseries
            Next

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

    Public Sub countcancel()
        Try
            For Each row As DataGridViewRow In grdlog.Rows
                Dim countgood As Integer = 0, countdouble As Integer = 0, countcancelticket As Integer = 0

                sql = "Select Count(loggoodid) from tblloggood where palletnum='" & grdlog.Rows(row.Index).Cells(3).Value & "' and status='1' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                countgood = cmd.ExecuteScalar
                cmd.Dispose()

                sql = "Select Count(logdoubleid) from tbllogdouble where palletnum='" & grdlog.Rows(row.Index).Cells(3).Value & "' and status='1' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                countdouble = cmd.ExecuteScalar
                cmd.Dispose()

                'count cancel per pallet
                For Each rowcancel As DataGridViewRow In grdcancel.Rows
                    If grdlog.Rows(row.Index).Cells(3).Value = grdcancel.Rows(rowcancel.Index).Cells(1).Value Then
                        countcancelticket = countcancelticket + 1
                    End If
                Next

                grdlog.Item(7, row.Index).Value = countgood + countdouble
                grdlog.Item(8, row.Index).Value = countcancelticket
                grdlog.Item(9, row.Index).Value = (countgood + countdouble) - countcancelticket

                If grdlog.Item(9, row.Index).Value < grdlog.Item(10, row.Index).Value Then
                    If btncomplete.Text <> "Selected Item Completed" Then
                        grdlog.Rows(row.Index).Cells(11).Style.BackColor = Color.Maroon
                        grdlog.Rows(row.Index).Cells(11).ErrorText = "Total available bags is less than the number of selected bags."
                    End If
                Else
                    grdlog.Rows(row.Index).Cells(11).Style.BackColor = Nothing
                    grdlog.Rows(row.Index).Cells(11).ErrorText = ""
                End If
            Next

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
            '/MsgBox("check ilang cancelled and if difference is not less than the selected bags. pag error highlight selectedbags")

            If Trim(txtpallet.Text) <> "" And Trim(txtcancel.Text) <> "" And Trim(txtreason.Text) <> "" And Trim(txtletter.Text) <> "" Then
                If cmbcancel.Items.Contains(txtcancel.Text) = True Then
                    MsgBox("Already cancelled.", MsgBoxStyle.Information, "")
                Else
                    'check muna if palletnum sa sql kung match
                    'check if double ticket kung may letter D
                    If Trim(txtcancel.Text).ToString.Contains("D") = True Then
                        'double
                        sql = "Select * from tbllogdouble where letter='" & Trim(txtletter.Text) & "' and dticketnum='" & Trim(txtcancel.Text) & "' and palletnum='" & Trim(txtpallet.Text) & "' and status='1' and branch='" & login.branch & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            'check if nasa grdtickets sya
                            For Each row As DataGridViewRow In grdtickets.Rows
                                If grdtickets.Rows(row.Index).Cells(3).Value = txtcancel.Text And grdtickets.Rows(row.Index).Cells(5).Value = Trim(txtletter.Text) Then
                                    cmbcancel.Items.Add(txtcancel.Text)
                                    grdcancel.Rows.Add(lbllog.Text, Trim(txtpallet.Text), txtletter.Text, Trim(txtcancel.Text), Date.Now, Trim(txtreason.Text), grdtickets.Rows(row.Index).Cells(0).Value, ofcnfby)
                                    grdtickets.Rows(row.Index).Cells(6).Value = 0
                                    viewselectedtickets()
                                    countselectedpicked()
                                    txtcancel.Text = ""
                                    txtcancel.Focus()
                                    btnok.PerformClick()
                                    Exit Sub
                                End If
                            Next

                            'pag lumagpas ng for loop ibig sabihin wala sa list ng grdtickets
                            MsgBox("Cannot found ticket# " & Trim(txtletter.Text) & Trim(txtcancel.Text) & " within the range.", MsgBoxStyle.Exclamation, "")
                        Else
                            MsgBox("Cannot found ticket# " & Trim(txtletter.Text) & Trim(txtcancel.Text) & " in Pallet Tag#" & Trim(txtpallet.Text) & ".", MsgBoxStyle.Critical, "")
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()

                    Else
                        'good
                        sql = "Select * from tblloggood where gticketnum='" & Trim(txtcancel.Text) & "' and palletnum='" & Trim(txtpallet.Text) & "' and status='1' and branch='" & login.branch & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            'check if nasa grdtickets sya
                            For Each row As DataGridViewRow In grdtickets.Rows
                                If grdtickets.Rows(row.Index).Cells(4).Value = txtcancel.Text And grdtickets.Rows(row.Index).Cells(5).Value = Trim(txtletter.Text) Then
                                    cmbcancel.Items.Add(txtcancel.Text)
                                    grdcancel.Rows.Add(lbllog.Text, Trim(txtpallet.Text), txtletter.Text, Trim(txtcancel.Text), Date.Now, Trim(txtreason.Text), grdtickets.Rows(row.Index).Cells(0).Value, ofcnfby)
                                    grdtickets.Rows(row.Index).Cells(6).Value = 0
                                    viewselectedtickets()
                                    countselectedpicked()
                                    txtcancel.Text = ""
                                    txtcancel.Focus()
                                    Exit Sub
                                End If
                            Next

                            'pag lumagpas ng for loop ibig sabihin wala sa list ng grdtickets
                            MsgBox("Cannot found ticket# " & Trim(txtletter.Text) & Trim(txtcancel.Text) & " within the range.", MsgBoxStyle.Exclamation, "")
                        Else
                            MsgBox("Cannot found ticket# " & Trim(txtletter.Text) & Trim(txtcancel.Text) & " in Pallet Tag#" & Trim(txtpallet.Text) & ".", MsgBoxStyle.Critical, "")
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()
                    End If
                End If
            Else
                MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            txtcancel.Text = ""
            txtcancel.Focus()

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

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        Try
            For Each row As DataGridViewRow In grdcancel.SelectedRows
                Dim cmbindex As Integer = row.Index
                grdcancel.Rows.Remove(row)
                cmbcancel.Items.RemoveAt(cmbindex)
            Next

            viewselectedtickets()

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

    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        txtcancel.Text = ""
        txtcancel.Focus()
        txtreason.Text = ""
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Me.CancelTicketToolStripMenuItem.Visible = True
        Me.EditNoOfSelectedBagsToolStripMenuItem.Visible = True
        viewselectedtickets()
        txtpallet.Text = ""
        txtcancel.Text = ""
        txtreason.Text = ""
        Me.CancelTicketToolStripMenuItem.Visible = True
        Panelcancel.Enabled = False
        Panel2.Enabled = True
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

    Private Sub btnqasample_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnqasample.Click
        Try
            If login.depart = "QCA" Or login.depart = "All" Then
                If btnqasample.Text = "QA Sampling Complete" Then
                    ofrems = ""
                    orderfillrems.ShowDialog()
                    If ofrems <> "" Then
                        ofcnf = False
                        confirmsave.GroupBox1.Text = login.wgroup
                        confirmsave.ShowDialog()
                        If ofcnf = True Then
                            'update tblofitem
                            sql = "Update tblofitem set qasampling='1', qasampdate=GetDate(), qasampneym='" & login.user & "', qasamprems='" & ofrems & "' where ofitemid='" & txtofitemid.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()

                            MsgBox("Successfully completed.", MsgBoxStyle.Information, "")
                            btnqasample.Text = "QA Sampling Completed"
                            btnqasample.Enabled = False
                            btncomplete.Enabled = True
                            btncomplete.Text = "Selected Item Complete"
                            btnpallet.Enabled = True
                            btnrefresh.Enabled = True
                            btnconfirm.Enabled = False
                            btnsave.Enabled = True
                        End If
                    Else
                        MsgBox("User cancelled QA sampling entry.", MsgBoxStyle.Information, "")
                    End If
                End If
            Else
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
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

    Private Sub grdlog_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdlog.CellContentClick
        Try
            rowindex = e.RowIndex
            'check kung may chineck nga sa grdlog then ska lng sya pde i enabled

            If e.ColumnIndex = 13 Then
                Exit Sub
                Dim checkCell As DataGridViewCheckBoxCell = CType(grdlog.Rows(rowindex).Cells(13), DataGridViewCheckBoxCell)
                '/Button1.PerformClick()
                If checkCell.Value = False Then
                    checkCell.Value = True
                ElseIf checkCell.Value = True Then
                    Button1.PerformClick()
                    Dim a As String = MsgBox("Are you sure you want to cancel picked tickets?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                    If a = vbYes Then
                        checkCell.Value = False
                    End If
                End If
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Public Sub loadcompleted()
        Try
            ExecuteLoadCompleted(strconn)

            loadcompletedtickets()
            '/grdlog.Enabled = False
            Me.ContextMenuStrip1.Enabled = False

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

    Private Sub ExecuteLoadCompleted(ByVal connectionString As String)
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

                'ibig sabihin completed na cannot edit grdlog
                sql = "Select tbllogticket.letter1, tbllogticket.letter4, tbllogticket.astart, tbllogticket.fend, tbloflog.oflogid, tbloflog.logsheetdate, tbloflog.logsheetnum, tbloflog.palletnum, tbloflog.selectedbags, tbloflog.ticketseries, tbloflog.picktickets"
                sql = sql & " from tbloflog left outer join tbllogticket on tbloflog.palletnum=tbllogticket.palletnum"
                sql = sql & " where tbloflog.ofitemid='" & txtofitemid.Text & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                While dr.Read
                    Dim checkcell As Boolean = False
                    If IsDBNull(dr("picktickets")) = False Then
                        checkcell = dr("picktickets")
                    End If
                    grdlog.Rows.Add(dr("oflogid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("palletnum"), "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), 0, 0, 0, dr("selectedbags"), dr("ticketseries"), checkcell)
                End While
                dr.Dispose()

                sql = "Select * from tbloflogcancel where ofitemid='" & txtofitemid.Text & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                While dr.Read
                    cmbcancel.Items.Add(dr("cticketnum"))
                    grdcancel.Rows.Add(dr("logsheetnum"), dr("palletnum"), dr("letter"), dr("cticketnum"), dr("cticketdate"), dr("remarks"), dr("ticketid"), dr("cancelby"))
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

    Public Sub loadcompletedtickets()
        Try
            If grdlog.Rows.Count = 0 Then
                '/grdtickets.Rows.Clear()
                grdcancel.Rows.Clear()
                cmbcancel.Items.Clear()
                lblseltotal.Text = "0.00"

            Else
                Dim table = New DataTable()
                table.Columns.Add("ofloggoodid", GetType(Integer))
                table.Columns.Add("logsheetnum", GetType(String))
                table.Columns.Add("palletnum", GetType(String))
                table.Columns.Add("gticketnum", GetType(String))
                table.Columns.Add("Number", GetType(Integer))
                table.Columns.Add("Letter", GetType(Char))
                table.Columns.Add("Picked", GetType(String))

                'generate grdtickets
                For Each row As DataGridViewRow In grdlog.Rows
                    Dim pallet As String = grdlog.Rows(row.Index).Cells(3).Value
                    sql = "Select * from tblofloggood where ofid='" & lblorfid.Text & "' and palletnum='" & pallet & "' and status='1'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        table.Rows.Add(dr("ofloggoodid"), dr("logsheetnum"), dr("palletnum"), dr("gticketnum").ToString, Val(dr("gticketnum")), dr("letter"), "")
                        '/grdtickets.Rows.Add(dr("loggoodid"), dr("logsheetnum"), dr("palletnum"), dr("gticketnum").ToString, Val(dr("gticketnum")), dr("letter"))
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                Next

                '/grdtickets.Sort(grdtickets.Columns(4), System.ComponentModel.ListSortDirection.Ascending)
                '//apply sort on Letter, then Number column
                table.DefaultView.Sort = "Letter, Number"

                grdtickets.Columns.Clear()
                Me.grdtickets.DataSource = table

                '/viewselectedtickets() dati meron to sa version2.5
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

    Public Sub autosave()
        Try
            ExecuteAutoSave(strconn)

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

    Private Sub ExecuteAutoSave(ByVal connectionString As String)
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

                'check if orderfill is not cancelled
                sql = "Select * from tblorderfill where ofid='" & lblorfid.Text & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        '/MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        '/MsgBox("Order fill is already confirmed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()

                'check if ofitem is not cancelled
                sql = "Select * from tblofitem where ofitemid='" & txtofitemid.Text & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        '/MsgBox("Item is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        '/MsgBox("Item is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()

                'gawing 0 - in process yung status ng tblofitem
                sql = "Update tblofitem set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where ofitemid='" & txtofitemid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'saving\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                'where ofitemid=lblofitemid.text

                'insert temporary tblofitem in tbltempofitem1
                'per items////////////////////////////////////////////////////////////////////////////
                Dim tbltempofitem As String = "tbltempofitem" & txtofitemid.Text
                Dim tblexistofitem As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempofitem & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistofitem = True
                End If
                dr.Dispose()

                If tblexistofitem = False Then
                    'create tbltempofitem1
                    sql = "Create Table " & tbltempofitem & " (ofitemid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofnum nvarchar(MAX), wrsnum nvarchar(MAX), itemname nvarchar(50), numbags int, oftype nvarchar(50), remarks nvarchar(MAX), datecreated datetime, createdby nvarchar(50), datemodified datetime, modifiedby nvarchar(50), status int, branch nvarchar(50))"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempofitem1 where lblofitemid
                    sql = "TRUNCATE Table " & tbltempofitem & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert ofitem in temporary table
                sql = "Insert into " & tbltempofitem & " (ofnum, wrsnum, itemname, numbags, remarks, datecreated, createdby, datemodified, modifiedby, status, branch) values ('" & txtorf.Text & "', '" & lblorf.Text & txtwrs.Text & "', '" & Trim(cmbitem.Text) & "', '" & Val(txtbags.Text) & "', '" & txtrems.Text & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1', '" & login.branch & "')"
                command.CommandText = sql
                command.ExecuteNonQuery()
                'per items////////////////////////////////////////////////////////////////////////////

                'insert temporary tbloflog in tbltempoflog
                'per log////////////////////////////////////////////////////////////////////////////
                Dim tbltempoflog As String = "tbltempoflog" & txtofitemid.Text
                Dim tblexistoflog As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempoflog & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistoflog = True
                End If
                dr.Dispose()

                If tblexistoflog = False Then
                    'create tbltempoflog
                    sql = "Create Table " & tbltempoflog & " (oflogid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofnum nvarchar(MAX), ofitemid	int, logsheetdate date, logsheetnum nvarchar(MAX), logticketid	int, palletnum nvarchar(MAX), selectedbags int, ticketseries nvarchar(MAX), datecreated datetime, createdby nvarchar(50), picktickets nvarchar(50))"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempoflog where lblofitemid
                    sql = "TRUNCATE Table " & tbltempoflog & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert oflog in temporary table
                For Each row As DataGridViewRow In grdlog.Rows
                    Dim logdate As String = grdlog.Rows(row.Index).Cells(1).Value
                    Dim lognum As String = grdlog.Rows(row.Index).Cells(2).Value
                    Dim pallettag As String = (grdlog.Rows(row.Index).Cells(3).Value)
                    Dim selectednum As String = grdlog.Rows(row.Index).Cells(10).Value
                    Dim seriesnum As String = grdlog.Rows(row.Index).Cells(11).Value
                    Dim logticketid As String = grdlog.Rows(row.Index).Cells(12).Value
                    Dim checkCell As DataGridViewCheckBoxCell = CType(grdlog.Rows(row.Index).Cells(13), DataGridViewCheckBoxCell)

                    sql = "Insert into " & tbltempoflog & " (ofnum, ofitemid, logsheetdate, logsheetnum, logticketid, palletnum, selectedbags, ticketseries, datecreated, createdby, picktickets) "
                    sql = sql & "values ('" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "','" & logdate & "','" & lognum & "','" & logticketid & "','" & pallettag & "','" & selectednum & "','" & seriesnum & "',GetDate(),'" & login.user & "', '" & checkCell.Value & "')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next
                'per log////////////////////////////////////////////////////////////////////////////

                'per log picktickets////////////////////////////////////////////////////////////////
                Dim tbltempoflogpick As String = "tbltempoflogpick" & txtofitemid.Text
                Dim tblexistoflogpick As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempoflogpick & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistoflogpick = True
                End If
                dr.Dispose()

                If tblexistoflogpick = False Then
                    'create tbltempoflogpick
                    sql = "Create Table " & tbltempoflogpick & " (ofloggoodid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofid int, ofnum nvarchar(MAX), ofitemid int, palletnum nvarchar(MAX), letter nvarchar(50), gticketnum nvarchar(MAX), status int, picked int)"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempoflogpick where lblofitemid
                    sql = "TRUNCATE Table " & tbltempoflogpick & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert oflog in temporary table
                For Each row As DataGridViewRow In grdtickets.Rows
                    If Val(grdtickets.Rows(row.Index).Cells(6).Value.ToString) = 1 Then
                        Dim pallettag As String = grdtickets.Rows(row.Index).Cells(2).Value
                        Dim letter As String = grdtickets.Rows(row.Index).Cells(5).Value
                        Dim ticknum As String = grdtickets.Rows(row.Index).Cells(4).Value
                        Dim picked As Integer = grdtickets.Rows(row.Index).Cells(6).Value

                        sql = "Insert into " & tbltempoflogpick & " (ofid, ofnum, ofitemid, palletnum, letter, gticketnum, status, picked) "
                        sql = sql & "values ('" & lblorfid.Text & "','" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', '" & pallettag & "', '" & letter & "', '" & ticknum & "', '1', '" & picked & "')"
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    End If
                Next
                'per log picktickets////////////////////////////////////////////////////////////////

                'insert temporary tbloflogcancel in tbltempoflogcancel
                'per logcancel////////////////////////////////////////////////////////////////////////////
                Dim tbltempoflogcancel As String = "tbltempoflogcancel" & txtofitemid.Text
                Dim tblexistoflogcancel As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempoflogcancel & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistoflogcancel = True
                End If
                dr.Dispose()

                If tblexistoflogcancel = False Then
                    'create tbltempoflogcancel
                    sql = "Create Table " & tbltempoflogcancel & " (oflogcancelid int NOT NULL PRIMARY KEY IDENTITY(1,1), ofnum nvarchar(MAX), ofitemid	int, oflogid int, logsheetnum nvarchar(MAX), palletnum nvarchar(MAX), ticketid int, letter nvarchar(50), cticketnum int, cticketdate datetime, remarks	nvarchar(MAX), cancelby nvarchar(50), status int)"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Else
                    'truncate tbltempoflogcancel where lblofitemid
                    sql = "TRUNCATE Table " & tbltempoflogcancel & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                'insert oflogcancel in temporary table
                For Each row As DataGridViewRow In grdcancel.Rows
                    Dim lognum As String = grdcancel.Rows(row.Index).Cells(0).Value
                    Dim pallettag As String = (grdcancel.Rows(row.Index).Cells(1).Value)
                    Dim tletter As String = grdcancel.Rows(row.Index).Cells(2).Value
                    Dim ticketnum As String = grdcancel.Rows(row.Index).Cells(3).Value
                    Dim tickettime As String = grdcancel.Rows(row.Index).Cells(4).Value
                    Dim disposition As String = grdcancel.Rows(row.Index).Cells(5).Value
                    Dim ticketid As String = grdcancel.Rows(row.Index).Cells(6).Value
                    Dim cancelby As String = grdcancel.Rows(row.Index).Cells(7).Value.ToString

                    sql = "Insert into " & tbltempoflogcancel & " (ofnum, ofitemid, oflogid, logsheetnum, palletnum, ticketid, letter, cticketnum, cticketdate, remarks, cancelby, status) values ('" & lblorf.Text & txtorf.Text & "', '" & txtofitemid.Text & "', (Select oflogid from tbloflog where ofnum='" & lblorf.Text & txtorf.Text & "' and logsheetnum='" & lognum & "'),'" & lognum & "','" & pallettag & "','" & ticketid & "','" & tletter & "','" & ticketnum & "','" & tickettime & "','" & disposition & "','" & cancelby & "','1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next
                'per logcancel////////////////////////////////////////////////////////////////////////////

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

    Public Sub deletetemp()
        Try
            list3.Items.Clear()
            sql = "Select ofitemid from tblorderfill right outer join tblofitem on tblorderfill.ofid=tblofitem.ofid where tblorderfill.ofid='" & lblorfid.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                list3.Items.Add(dr("ofitemid"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            For Each item As Object In list3.Items
                'delete yung mga tbltempsssss///////////////////////////
                'drop tbltempofitem///////////////////////////////////////////////////////
                Dim tbltempofitem As String = "tbltempofitem" & item
                Dim tblexistofitem As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempofitem & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    tblexistofitem = True
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If tblexistofitem = True Then
                    sql = "DROP Table " & tbltempofitem & ""
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                End If
                'tbltempofitem/////////////////////////////////////////////////////////////////////////

                'drop tbltempoflogcancel///////////////////////////////////////////////////////
                Dim tbltempoflogcancel As String = "tbltempoflogcancel" & item
                Dim tblexistoflogcancel As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempoflogcancel & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    tblexistoflogcancel = True
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If tblexistoflogcancel = True Then
                    sql = "DROP Table " & tbltempoflogcancel & ""
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                End If
                'tbltempoflogcancel/////////////////////////////////////////////////////////////////////////

                'drop tbltempoflog///////////////////////////////////////////////////////tbltempoflogpick
                Dim tbltempoflog As String = "tbltempoflog" & item
                Dim tblexistoflog As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempoflog & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    tblexistoflog = True
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If tblexistoflog = True Then
                    sql = "DROP Table " & tbltempoflog & ""
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                End If
                'tbltempoflog/////////////////////////////////////////////////////////////////////////

                'drop tbltempoflogpick///////////////////////////////////////////////////////
                Dim tbltempoflogpick As String = "tbltempoflog" & item
                Dim tblexistoflogpick As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempoflogpick & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    tblexistoflogpick = True
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If tblexistoflogpick = True Then
                    sql = "DROP Table " & tbltempoflogpick & ""
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                End If
                'tbltempoflogpick/////////////////////////////////////////////////////////////////////////
            Next

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

    Private Sub grdlog_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdlog.CellValueChanged
        Try
            If grdlog.Columns(e.ColumnIndex).HeaderText = "Pick Tickets" And grdlog.RowCount <> 0 And grdlog.CurrentCell.ColumnIndex = 13 Then
                If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                    'check muna kung walang errortext yung selectted para may definite no of selected bags
                    If grdlog.Rows(rowindex).Cells(11).ErrorText <> "" Then
                        MsgBox("Edit number of selected bags first.", MsgBoxStyle.Exclamation, "")
                    Else
                        If e.ColumnIndex = 13 Then
                            Exit Sub
                            Dim checkCell As DataGridViewCheckBoxCell = CType(grdlog.Rows(rowindex).Cells(13), DataGridViewCheckBoxCell)
                            '/Button1.PerformClick()
                            If checkCell.Value = True Then
                                'show all tickets sa isang form
                                '/viewtickets()
                                orderfilltickets.lblpalletid.Text = grdlog.Rows(rowindex).Cells(0).Value
                                orderfilltickets.txtpallet.Text = grdlog.Rows(rowindex).Cells(3).Value
                                orderfilltickets.ShowDialog()
                                viewselectedtickets()

                            ElseIf checkCell.Value = False Then
                                'reset grdtickets
                                For Each row As DataGridViewRow In grdtickets.Rows
                                    Dim pallet As String = grdtickets.Rows(row.Index).Cells(2).Value

                                    If grdlog.Rows(rowindex).Cells(3).Value = pallet Then
                                        grdtickets.Rows(row.Index).Cells(6).Value = ""
                                    End If
                                Next

                                viewselectedtickets()
                            End If
                        End If
                    End If
                Else
                    MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
                End If

                grdlog.Invalidate()
            End If

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnothers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnothers.Click
        orderfillothers.ShowDialog()
    End Sub

    Private Sub PickPalletsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PickPalletsToolStripMenuItem.Click
        Try
            orderfilltickets.lblpalletid.Text = grdlog.Rows(rowindex).Cells(0).Value
            orderfilltickets.txtpallet.Text = grdlog.Rows(rowindex).Cells(3).Value
            orderfilltickets.ShowDialog()
            viewselectedtickets()

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

    Public Sub countselectedpicked()
        Try
            For Each rowlog As DataGridViewRow In grdlog.Rows
                Dim checkCell As DataGridViewCheckBoxCell = CType(grdlog.Rows(rowlog.Index).Cells(13), DataGridViewCheckBoxCell)
                If checkCell.Value = True Then
                    Dim logpallet As String = grdlog.Rows(rowlog.Index).Cells(3).Value
                    Dim countsel As Integer = 0

                    For Each rowselected As DataGridViewRow In grdselected.Rows
                        Dim selpallet As String = grdselected.Rows(rowselected.Index).Cells(2).Value

                        If logpallet = selpallet Then
                            countsel += 1
                        End If
                    Next

                    grdlog.Rows(rowlog.Index).Cells(10).Value = countsel
                End If
            Next

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
End Class