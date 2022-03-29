Imports System.IO
Imports System.Data.SqlClient

Public Class coawoutformat
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim proceed As Boolean = False
    Public coacnf As Boolean = False
    Dim closingna As Boolean = False
    Dim rowindex As Integer

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

    Private Sub coanew_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        closingna = True
    End Sub

    Private Sub coanew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ginfo.Enabled = True
        defaultform()
        txtcoanum.ReadOnly = False
        txtcoanum.Focus()
        analysisreadonlytrue()

        grdcoa.ReadOnly = False
        grdcoa.Columns(0).ReadOnly = True
        grdcoa.Columns(1).ReadOnly = True
        grdcoa.Columns(2).ReadOnly = True

        grdcoa.Columns(1).DefaultCellStyle.Format = "yyyy/MM/dd"
        grdcoa.Columns(4).DefaultCellStyle.Format = "yyyy/MM/dd"
    End Sub

    Private Sub coanew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        '/If login.logshift = "" Or login.logwhse = "" Then
        '/MsgBox("Choose warehouse.", MsgBoxStyle.Exclamation, "")
        '/chooseshift.ShowDialog()
        '/Me.Dispose()
        '/ Else
        txtcoanum.Focus()
        btnconfirm.Enabled = False
        btncancel.Enabled = False
        btnprint.Enabled = False
        btnsave.Enabled = False
        txtrems.Enabled = False
        '/End If
    End Sub

    Public Sub defaultform()
        lblitemid.Text = ""
        txtrefnum.Text = ""
        txtbatch.Text = ""
        txtqty.Text = ""
        dateload.Value = Date.Now
        datedel.Value = Date.Now
        txtcus.Text = ""
        txtpo.Text = ""
        txttruck.Text = ""
        txtdriver.Text = ""
        txtitem.Text = ""
        txtmois.Text = ""
        txtprot.Text = ""
        txtash.Text = ""
        txtwet.Text = ""
        txtwater.Text = ""
        btnconfirm.Text = "Confirm"

        txtitem.BackColor = Color.MistyRose
        txtqty.BackColor = Color.MistyRose
        txtcus.BackColor = Color.FromArgb(192, 255, 192)
        txtpo.BackColor = Color.FromArgb(192, 255, 192)
        txttruck.BackColor = Color.FromArgb(192, 255, 192)
        txtdriver.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub txtofnum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtofnum.TextChanged
        Dim str As String
        str = txtofnum.Text
        If str.Length > 3 Then
            Dim answer As String
            answer = str.Substring(0, 3)
            If answer = "OF." Then
                str = str.Substring(3, str.Length - 3)
                txtofnum.Text = str
                txtofnum.Select(txtofnum.Text.Length, 0)
            End If
        End If
    End Sub

    Private Sub btnconfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnconfirm.Click
        Try
            If login.depart = "QCA" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                If btnconfirm.Text = "Confirmed" Then
                    MsgBox(lblcoa.Text & txtcoanum.Text & " is already confirmed.", MsgBoxStyle.Exclamation, "")
                Else
                    'check if orderfill is not cancelled
                    sql = "Select * from tblcoa where coaid='" & lblcoaid.Text & "' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr("status") = 3 Then
                            MsgBox("COA is already cancelled.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        ElseIf dr("status") = 2 Then
                            MsgBox("COA is already completed.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()


                    'check if may laman lahat
                    If Trim(txtcoanum.Text) <> "" And Trim(txtrefnum.Text) <> "" And Trim(txtbatch.Text) <> "" And Trim(txtqty.Text) <> "" And Trim(txtcus.Text) <> "" And Trim(txtpo.Text) <> "" And Trim(txttruck.Text) <> "" And Trim(txtcoacus.Text) <> "" Then
                        '/ MsgBox("check if may expiry date na lahat aba")

                        Dim walapalahat As Boolean = False
                        For Each row As DataGridViewRow In grdcoa.Rows
                            If grdcoa.Rows(row.Index).Cells(3).Value.ToString = "" Then
                                walapalahat = True
                            End If
                        Next

                        If walapalahat = True Then
                            Me.Cursor = Cursors.Default
                            MsgBox("Please complete the expiry dates.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If

                        'check sa sql if may analysis na lahat
                        Dim lahatmayanalysis As Boolean = False
                        For Each row As DataGridViewRow In grdcoa.Rows
                            sql = "Select * from tblcoasub where coasubid='" & grdcoa.Rows(row.Index).Cells(0).Value & "'"
                            conn.Open()
                            cmd = New SqlCommand(sql, conn)
                            dr = cmd.ExecuteReader
                            If dr.Read Then
                                If dr("moisture") = 0 And dr("protein") = 0 And dr("ash") = 0 And dr("wetgluten") = 0 And dr("water") = 0 And Trim(dr("others").ToString) = "" Then
                                    Me.Cursor = Cursors.Default
                                    MsgBox("Complete analysis first.", MsgBoxStyle.Exclamation, "")
                                    Exit Sub
                                End If
                            End If
                            dr.Dispose()
                            cmd.Dispose()
                            conn.Close()
                        Next


                        coacnf = False
                        confirmsave.GroupBox1.Text = login.wgroup
                        confirmsave.ShowDialog()
                        If coacnf = True Then
                            ExecuteConfirm(strconn)
                        End If

                    ElseIf Trim(txtrefnum.Text) = "" Then
                        MsgBox("Reference number must not be empty.", MsgBoxStyle.Exclamation, "")
                    ElseIf Trim(txtbatch.Text) = "" Then
                        MsgBox("Batch no. / Lot no. must not be empty.", MsgBoxStyle.Exclamation, "")
                    ElseIf Trim(txtcoacus.Text) = "" Then
                        MsgBox("COA Customer name must not be empty.", MsgBoxStyle.Exclamation, "")
                    Else
                        MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
                    End If
                End If
            Else
                MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
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

                'update into tblcoa status=2
                sql = "Update tblcoa set coacustomer='" & Trim(txtcoacus.Text) & "', refnum='" & txtrefnum.Text & "', batchnum='" & txtbatch.Text & "', loaddate='" & Format(dateload.Value, "yyyy/MM/dd") & "', deldate='" & Format(datedel.Value, "yyyy/MM/dd") & "', remarks='" & Trim(txtrems.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "', status='2' where coaid='" & lblcoaid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                For Each row As DataGridViewRow In grdcoa.Rows
                    Dim subid As Integer = grdcoa.Rows(row.Index).Cells(0).Value
                    Dim subprod As Date = grdcoa.Rows(row.Index).Cells(1).Value
                    Dim subofprod As Date = grdcoa.Rows(row.Index).Cells(4).Value

                    If Trim(grdcoa.Rows(row.Index).Cells(3).Value.ToString.ToUpper) = "N/A" Then
                        sql = "Update tblcoasub set ofproddate='" & Format(subofprod, "yyyy/MM/dd") & "', proddate='" & Format(subprod, "yyyy/MM/dd") & "', expirydate=NULL where coasubid='" & subid & "'"
                    Else
                        Dim subexpiry As Date = grdcoa.Rows(row.Index).Cells(3).Value
                        sql = "Update tblcoasub set ofproddate='" & Format(subofprod, "yyyy/MM/dd") & "', proddate='" & Format(subprod, "yyyy/MM/dd") & "', expirydate='" & Format(subexpiry, "yyyy/MM/dd") & "' where coasubid='" & subid & "'"
                    End If

                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                'update tblofitem with coanumber
                sql = "Update tblofitem set coanum='" & lblcoa.Text & txtcoanum.Text & "' where ofnum='" & lblofnum.Text & txtofnum.Text & "' and ofitemid='" & lblitemid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully confirmed.", MsgBoxStyle.Information, "")

                btnconfirm.Text = "Confirmed"
                btnconfirm.Enabled = False
                btnsave.Enabled = False
                btnprint.Enabled = True
                ginfo.Enabled = False
                gselect.Enabled = False
                txtrems.Enabled = False

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

    Private Sub txtrefnum_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtrefnum.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub txtrefnum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrefnum.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtrefnum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrefnum.TextChanged

    End Sub

    Private Sub txtbatch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtbatch.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub txtbatch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbatch.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtbatch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbatch.TextChanged

    End Sub

    Private Sub txtmois_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtmois.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub txtmois_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmois.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 46 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then
            If Asc(e.KeyChar) = 46 And Trim(txtmois.Text).Contains(".") = True Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtmois_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmois.ReadOnlyChanged
        If txtmois.ReadOnly = True Then
            txtmois.BackColor = Color.White
        Else
            txtmois.BackColor = Color.FromArgb(192, 255, 255)
        End If
    End Sub

    Private Sub txtmois_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmois.TextChanged

    End Sub

    Private Sub txtprot_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtprot.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub txtprot_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtprot.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 46 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then
            If Asc(e.KeyChar) = 46 And Trim(txtprot.Text).Contains(".") = True Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtprot_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtprot.ReadOnlyChanged
        If txtprot.ReadOnly = True Then
            txtprot.BackColor = Color.White
        Else
            txtprot.BackColor = Color.FromArgb(192, 255, 255)
        End If
    End Sub

    Private Sub txtprot_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprot.TextChanged

    End Sub

    Private Sub txtash_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtash.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub txtash_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtash.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 46 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then
            If Asc(e.KeyChar) = 46 And Trim(txtash.Text).Contains(".") = True Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtash_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtash.ReadOnlyChanged
        If txtash.ReadOnly = True Then
            txtash.BackColor = Color.White
        Else
            txtash.BackColor = Color.FromArgb(192, 255, 255)
        End If
    End Sub

    Private Sub txtash_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtash.TextChanged

    End Sub

    Private Sub txtwet_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtwet.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub txtwet_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwet.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 46 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then
            If Asc(e.KeyChar) = 46 And Trim(txtwet.Text).Contains(".") = True Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtwet_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtwet.ReadOnlyChanged
        If txtwet.ReadOnly = True Then
            txtwet.BackColor = Color.White
        Else
            txtwet.BackColor = Color.FromArgb(192, 255, 255)
        End If
    End Sub

    Private Sub txtwet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwet.TextChanged

    End Sub

    Private Sub txtwater_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtwater.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Your code here
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub txtwater_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwater.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 46 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then
            If Asc(e.KeyChar) = 46 And Trim(txtwater.Text).Contains(".") = True Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtwater_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtwater.ReadOnlyChanged
        If txtwater.ReadOnly = True Then
            txtwater.BackColor = Color.White
        Else
            txtwater.BackColor = Color.FromArgb(192, 255, 255)
        End If
    End Sub

    Private Sub txtwater_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwater.TextChanged

    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If login.depart = "QCA" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                'check if orderfill is not cancelled
                sql = "Select * from tblcoa where coaid='" & lblcoaid.Text & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("COA is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("COA is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                'kelangan may itemname na select to be able to save as draft
                coacnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If coacnf = True Then
                    ExecuteSaveasDraft(strconn)
                End If

            Else
                MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
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
                'update tblcoa
                sql = "Update tblcoa set coacustomer='" & Trim(txtcoacus.Text) & "', refnum='" & txtrefnum.Text & "', batchnum='" & txtbatch.Text & "', loaddate='" & Format(dateload.Value, "yyyy/MM/dd") & "', deldate='" & Format(datedel.Value, "yyyy/MM/dd") & "', remarks='" & Trim(txtrems.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where coaid='" & lblcoaid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                For Each row As DataGridViewRow In grdcoa.Rows
                    Dim subid As Integer = grdcoa.Rows(row.Index).Cells(0).Value
                    Dim subprod As Date = grdcoa.Rows(row.Index).Cells(1).Value

                    If IsDBNull(grdcoa.Rows(row.Index).Cells(3).Value) = True Or Trim(grdcoa.Rows(row.Index).Cells(3).Value.ToString) = "" Or Trim(grdcoa.Rows(row.Index).Cells(3).Value.ToString.ToUpper) = "N/A" Then
                        sql = "Update tblcoasub set proddate='" & Format(subprod, "yyyy/MM/dd") & "', expirydate=NULL where coasubid='" & subid & "'"
                    Else
                        Dim subexpiry As Date = grdcoa.Rows(row.Index).Cells(3).Value
                        sql = "Update tblcoasub set proddate='" & Format(subprod, "yyyy/MM/dd") & "', expirydate='" & Format(subexpiry, "yyyy/MM/dd") & "' where coasubid='" & subid & "'"
                    End If

                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully saved.", MsgBoxStyle.Information, "")

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

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Try
            'ofnum As String, ofitemid As Integer
            rptcoarevise.qty = Val(txtqty.Text)
            rptcoarevise.ofnum = lblofnum.Text & txtofnum.Text
            rptcoarevise.coanum = lblcoa.Text & Trim(txtcoanum.Text)
            rptcoarevise.ofitemid = Val(lblitemid.Text)
            rptcoarevise.stat = ""
            rptcoarevise.ShowDialog()

            'refresh list ng location
            defaultform()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub COAToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COAToolStripButton1.Click
        If login.depart = "QCA" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            'dapat automatic mag ssave
            btnsave.PerformClick()
            txtcoanum.ReadOnly = False
            coaselect.ShowDialog()
            txtcoanum.ReadOnly = False
            ginfo.Enabled = True
        Else
            MsgBox("Access denied.", MsgBoxStyle.Critical, "")
        End If
    End Sub

    Private Sub NewToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton1.Click
        If login.depart = "QCA" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            txtcoanum.ReadOnly = False
            coacreate.grdcoa.Rows.Clear()
            coacreate.ShowDialog()
            txtcoanum.ReadOnly = False
            If coacreate.txtcoanum.Text <> "" Then
                ginfo.Enabled = True
                COAToolStripButton1.PerformClick()
                '/btnsearch.PerformClick()
            End If

        Else
            MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
        End If
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            If txtcoanum.ReadOnly = True Then
                'parang warning para maisave muna kung may pending na order fill
                txtcoanum.ReadOnly = False
                txtcoanum.Focus()
                Panelinfo.Enabled = False
                gselect.Enabled = False
                btnconfirm.Enabled = False
                btnsave.Enabled = False
                btncancel.Enabled = False
                txtrems.Enabled = False

                If btnconfirm.Text <> "Confirmed Order Fill" Then

                Else
                    txtcoanum.ReadOnly = False
                    txtcoanum.Focus()
                    ginfo.Enabled = False
                    gselect.Enabled = False
                    btnconfirm.Enabled = False
                    btnsave.Enabled = False
                    btncancel.Enabled = False
                    txtrems.Enabled = False
                End If
            Else
                defaultform()

                ExecuteSearch(strconn)
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

    Private Sub ExecuteSearch(ByVal connectionString As String)
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
                sql = "Select * from tblcoa where coanum='" & lblcoa.Text & txtcoanum.Text & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    Dim ofn As String = dr("ofnum")
                    txtofnum.Text = ofn.Substring(3, ofn.Length - 3)
                    lblitemid.Text = dr("ofitemid")
                    txtitem.Text = dr("itemname")
                    txtcoacus.Text = dr("coacustomer").ToString
                    txtrefnum.Text = dr("refnum").ToString
                    txtbatch.Text = dr("batchnum").ToString
                    If IsDBNull(dr("loaddate")) = False Then
                        dateload.Value = dr("loaddate")
                    End If
                    If IsDBNull(dr("deldate")) = False Then
                        datedel.Value = dr("deldate")
                    End If
                    txtmois.Text = dr("moisture").ToString
                    txtprot.Text = dr("protein").ToString
                    txtash.Text = dr("ash").ToString
                    txtwet.Text = dr("wetgluten").ToString
                    txtwater.Text = dr("water").ToString
                    txtothers.Text = dr("remarks").ToString
                Else
                    MsgBox("COA # cannot found.", MsgBoxStyle.Critical, "")
                    Exit Sub
                End If
                dr.Dispose()

                'load yung wrs info
                sql = "Select * from tblorderfill where ofnum='" & lblofnum.Text & txtofnum.Text & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    txtwrs.Text = dr("wrsnum")
                    txtcus.Text = dr("customer")
                    txtpo.Text = dr("cusref")
                    txttruck.Text = dr("platenum")
                    txtdriver.Text = dr("driver")
                End If
                dr.Dispose()

                'load yung sa tblofitem
                sql = "Select tblofitem.numbags, tbloflog.ticketseries from tbloflog left outer join tblofitem on tbloflog.ofitemid=tblofitem.ofitemid where tbloflog.ofitemid='" & lblitemid.Text & "' and tblofitem.branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                While dr.Read
                    txtqty.Text = dr("numbags")
                End While
                dr.Dispose()

                grdcoa.Rows.Clear()
                Dim i As Integer = 0
                'load yung sa tblcoasub
                sql = "Select * from tblcoasub where coaid='" & lblcoaid.Text & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                While dr.Read
                    If IsDBNull(dr("expirydate")) = False Then
                        grdcoa.Rows.Add(dr("coasubid"), Format(dr("proddate"), "yyyy/MM/dd"), dr("tseries"), dr("expirydate"), Format(dr("proddate"), "yyyy/MM/dd"))
                    Else
                        grdcoa.Rows.Add(dr("coasubid"), Format(dr("proddate"), "yyyy/MM/dd"), dr("tseries"), dr("expirydate"), Format(dr("proddate"), "yyyy/MM/dd"))
                    End If

                    If i Mod 2 = 0 Then
                        grdcoa.Rows(i).DefaultCellStyle.BackColor = Color.White
                    Else
                        grdcoa.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
                    End If
                    i += 1
                End While
                dr.Dispose()

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default

                txtcoanum.ReadOnly = True
                Panelinfo.Enabled = True
                ginfo.Enabled = True
                gselect.Enabled = True
                btnconfirm.Enabled = True
                btnsave.Enabled = True
                btncancel.Enabled = True
                txtrems.Enabled = True

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

    Private Sub txtcoanum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcoanum.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If

        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtcoanum_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcoanum.ReadOnlyChanged
        If txtcoanum.ReadOnly = True Then
            txtcoanum.BackColor = Color.LightCyan
        Else
            txtcoanum.BackColor = SystemColors.Control
        End If
    End Sub

    Private Sub txtothers_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtothers.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        defaultform()
        Me.Close()
    End Sub

    Private Sub grdcoa_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcoa.CellContentClick

    End Sub

    Private Sub grdcoa_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcoa.CellEndEdit
        Try
            'column1
            If (e.ColumnIndex = 1) Then
                If grdcoa.Rows(rowindex).Cells(1).Style.BackColor = Color.HotPink Then
                    'check if numeric
                    ' Checking numeric value for Column1 only
                    If IsDate(grdcoa.Rows(e.RowIndex).Cells(1).Value) = True Then
                        Dim tempid As Integer = grdcoa.Rows(e.RowIndex).Cells(0).Value
                        Dim tempprod As Date = grdcoa.Rows(e.RowIndex).Cells(1).Value

                        If IsDBNull(grdcoa.Rows(e.RowIndex).Cells(3).Value) = True Then
                            If grdcoa.Rows(e.RowIndex).Cells(1).Value <> grdcoa.Rows(e.RowIndex).Cells(4).Value Then
                                '/MsgBox(grdcoa.Rows(e.RowIndex).Cells(1).Value < grdcoa.Rows(e.RowIndex).Cells(4).Value)
                                'check dapat hindi mas mababa sa prod date
                                If grdcoa.Rows(e.RowIndex).Cells(1).Value < grdcoa.Rows(e.RowIndex).Cells(4).Value Then
                                    MsgBox("Cannot change production date into previous dates.", MsgBoxStyle.Exclamation, "")
                                    grdcoa.Rows(e.RowIndex).Cells(1).Value = grdcoa.Rows(e.RowIndex).Cells(4).Value
                                    grdcoa.Rows(e.RowIndex).Cells(1).ReadOnly = True
                                    grdcoa.Rows(rowindex).Cells(1).Style.BackColor = grdcoa.Rows(rowindex).Cells(2).Style.BackColor
                                    Exit Sub
                                End If

                                coacnf = False
                                confirmsave.GroupBox1.Text = login.wgroup
                                confirmsave.ShowDialog()
                                If coacnf = True Then
                                    grdcoa.Rows(e.RowIndex).Cells(1).Value = Format(tempprod, "yyyy/MM/dd")
                                Else
                                    MsgBox("User cancelled editing production date.", MsgBoxStyle.Information, "")
                                    grdcoa.Rows(e.RowIndex).Cells(1).Value = grdcoa.Rows(e.RowIndex).Cells(4).Value
                                End If
                            End If
                        Else
                            If grdcoa.Rows(e.RowIndex).Cells(1).Value < grdcoa.Rows(e.RowIndex).Cells(4).Value Then
                                MsgBox("Cannot change production date into previous dates.", MsgBoxStyle.Exclamation, "")
                                grdcoa.Rows(e.RowIndex).Cells(1).Value = grdcoa.Rows(e.RowIndex).Cells(4).Value
                                grdcoa.Rows(e.RowIndex).Cells(1).ReadOnly = True
                                grdcoa.Rows(rowindex).Cells(1).Style.BackColor = grdcoa.Rows(rowindex).Cells(2).Style.BackColor
                                Exit Sub
                            End If

                            Dim tempexpiry As Date = grdcoa.Rows(e.RowIndex).Cells(3).Value

                            If tempexpiry <= tempprod Then
                                MsgBox("Invalid date. Production date is less than or equal to the Expiry date.", MsgBoxStyle.Exclamation, "")
                                grdcoa.Rows(e.RowIndex).Cells(1).Value = grdcoa.Rows(e.RowIndex).Cells(4).Value
                            Else
                                If grdcoa.Rows(e.RowIndex).Cells(1).Value <> grdcoa.Rows(e.RowIndex).Cells(4).Value Then
                                    coacnf = False
                                    confirmsave.GroupBox1.Text = login.wgroup
                                    confirmsave.ShowDialog()
                                    If coacnf = True Then
                                        grdcoa.Rows(e.RowIndex).Cells(1).Value = Format(tempprod, "yyyy/MM/dd")
                                    Else
                                        MsgBox("User cancelled editing production date.", MsgBoxStyle.Information, "")
                                        grdcoa.Rows(e.RowIndex).Cells(1).Value = grdcoa.Rows(e.RowIndex).Cells(4).Value
                                    End If
                                End If
                            End If
                        End If
                    Else
                        MsgBox("Invalid date.", MsgBoxStyle.Exclamation, "")
                        grdcoa.Rows(e.RowIndex).Cells(1).Value = grdcoa.Rows(e.RowIndex).Cells(4).Value
                    End If
                End If

                grdcoa.Rows(e.RowIndex).Cells(1).ReadOnly = True
                grdcoa.Rows(rowindex).Cells(1).Style.BackColor = grdcoa.Rows(rowindex).Cells(2).Style.BackColor

            ElseIf (e.ColumnIndex = 3) Then
                'columns(3)
                'check if date
                If IsDate(grdcoa.Rows(e.RowIndex).Cells(3).Value) = True Then
                    Dim tempid As Integer = grdcoa.Rows(e.RowIndex).Cells(0).Value
                    Dim tempprod As Date = grdcoa.Rows(e.RowIndex).Cells(1).Value
                    Dim tempexpiry As Date = grdcoa.Rows(e.RowIndex).Cells(3).Value

                    If tempexpiry <= tempprod Then
                        MsgBox("Invalid date. Expiry date is less than or equal to the Production date.", MsgBoxStyle.Exclamation, "")
                        grdcoa.Rows(e.RowIndex).Cells(3).Value = Nothing
                    Else
                        grdcoa.Rows(e.RowIndex).Cells(3).Value = Format(tempexpiry, "yyyy/MM/dd")
                    End If
                Else
                    If Trim(grdcoa.Rows(e.RowIndex).Cells(3).Value.ToString.ToUpper) = "N/A" Then
                        grdcoa.Rows(e.RowIndex).Cells(3).Value = grdcoa.Rows(e.RowIndex).Cells(3).Value.ToString.ToUpper
                    Else
                        MsgBox("Invalid date.", MsgBoxStyle.Exclamation, "")
                        grdcoa.Rows(e.RowIndex).Cells(3).Value = ""
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

    Private Sub grdcoa_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdcoa.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then
                rowindex = e.RowIndex
                grdcoa.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True
                If btnconfirm.Text = "Confirm" Then
                    Me.ContextMenuStrip1.Show(Cursor.Position)
                    If e.ColumnIndex = 1 Then
                        '/EditNoOfSelectedBagsToolStripMenuItem.Visible = True
                    Else
                        '/EditNoOfSelectedBagsToolStripMenuItem.Visible = False
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

    Private Sub AnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnalysisToolStripMenuItem.Click
        If login.depart = "QCA" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            btnok.Enabled = True

            analysisreadonlyfalse()

            lblcoasubid.Text = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(0).Value
            lbldate.Text = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(1).Value
            gparam.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor
            grheo.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor
            grems.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor

            sql = "Select * from tblcoasub where coasubid='" & lblcoasubid.Text & "'"
            connect()
            Dim cmd1 As SqlCommand = New SqlCommand(sql, conn)
            Dim dr1 As SqlDataReader = cmd1.ExecuteReader
            If dr1.Read Then
                txtmois.Text = dr1("moisture")
                txtprot.Text = dr1("protein")
                txtash.Text = dr1("ash")
                txtwet.Text = dr1("wetgluten")
                txtwater.Text = dr1("water")
                txtothers.Text = dr1("others")
            End If
            dr1.Dispose()
            cmd1.Dispose()

            ginfo.Enabled = False
            Panelbuttons.Enabled = False
            ContextMenuStrip1.Enabled = False
            grdcoa.Enabled = False
        Else
            MsgBox("Access denied.", MsgBoxStyle.Critical, "")
        End If

    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            If login.depart = "QCA" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                'check if orderfill is not cancelled
                sql = "Select * from tblcoa where coaid='" & lblcoaid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("COA is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("COA is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                coacnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If coacnf = True Then
                    'save yung tblsub
                    sql = "Update tblcoasub set moisture='" & txtmois.Text & "', protein='" & txtprot.Text & "', ash='" & txtash.Text & "', wetgluten='" & txtwet.Text & "', water='" & txtwater.Text & "', others='" & txtothers.Text & "' where coasubid='" & lblcoasubid.Text & "'"
                    conn.Open()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    '/MsgBox("Successfully saved analysis.", MsgBoxStyle.Information, "")
                End If
            Else
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
            End If


            ginfo.Enabled = True
            Panelbuttons.Enabled = True
            ContextMenuStrip1.Enabled = True
            lbldate.Text = ""
            analysisreadonlytrue()
            grdcoa.Enabled = True
            btnok.Enabled = False

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

    Public Sub analysisreadonlytrue()
        txtmois.ReadOnly = True
        txtprot.ReadOnly = True
        txtash.ReadOnly = True
        txtwet.ReadOnly = True
        txtwater.ReadOnly = True
        txtothers.ReadOnly = True
    End Sub

    Public Sub analysisreadonlyfalse()
        txtmois.ReadOnly = False
        txtprot.ReadOnly = False
        txtash.ReadOnly = False
        txtwet.ReadOnly = False
        txtwater.ReadOnly = False
        txtothers.ReadOnly = False
    End Sub

    Private Sub grdcoa_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdcoa.SelectionChanged
        Try
            lblcoasubid.Text = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(0).Value
            lbldate.Text = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(1).Value

            gparam.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor
            grheo.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor
            grems.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor

            sql = "Select * from tblcoasub where coasubid='" & lblcoasubid.Text & "'"
            connect()
            Dim cmd1 As SqlCommand = New SqlCommand(sql, conn)
            Dim dr1 As SqlDataReader = cmd1.ExecuteReader
            If dr1.Read Then
                txtmois.Text = dr1("moisture")
                txtprot.Text = dr1("protein")
                txtash.Text = dr1("ash")
                txtwet.Text = dr1("wetgluten")
                txtwater.Text = dr1("water")
                txtothers.Text = dr1("others")
            End If
            dr1.Dispose()
            cmd1.Dispose()

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

    Private Sub txtothers_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtothers.ReadOnlyChanged
        If txtothers.ReadOnly = True Then
            txtothers.BackColor = Color.White
        Else
            txtothers.BackColor = Color.FromArgb(192, 255, 255)
        End If
    End Sub

    Private Sub EditNoOfSelectedBagsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditNoOfSelectedBagsToolStripMenuItem.Click
        Try
            If login.depart = "QCA" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                grdcoa.ReadOnly = False
                For i = 0 To 2
                    grdcoa.Columns(i).ReadOnly = True
                Next

                '/rowindex = grdcoa.CurrentRow.Index
                grdcoa.ClearSelection()
                grdcoa.Rows(rowindex).Cells(1).ReadOnly = False
                grdcoa.Rows(rowindex).Cells(1).Style.BackColor = Color.HotPink
                grdcoa.Rows(rowindex).Cells(1).Selected = True
                grdcoa.BeginEdit(True)

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

    Private Sub txtrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtrems_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrems.TextChanged

    End Sub

    Private Sub txtcoanum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcoanum.TextChanged
        Dim str As String
        str = txtcoanum.Text
        If str.Length > 4 Then
            Dim answer As String
            answer = str.Substring(0, 4)
            If answer = "COA." Then
                str = str.Substring(4, str.Length - 4)
                txtcoanum.Text = str
                txtcoanum.Select(txtcoanum.Text.Length, 0)
            End If
        End If
    End Sub
End Class