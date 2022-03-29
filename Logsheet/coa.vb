Imports System.IO
Imports System.Data.SqlClient

Public Class coa
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
    Public coaform As String = ""

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
        Me.Dispose()
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
                    sql = "Select status from tblcoa where coaid='" & lblcoaid.Text & "' and branch='" & login.branch & "'"
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
                        If coaform = "COA 3" Or coaform = "COA 4" Then
                            Dim walapalahat5 As Boolean = False, walapalahat6 As Boolean = False
                            For Each row As DataGridViewRow In grdcoa.Rows
                                If grdcoa.Rows(row.Index).Cells(5).Value.ToString = "" Then
                                    walapalahat5 = True
                                    Exit For
                                ElseIf grdcoa.Rows(row.Index).Cells(6).Value.ToString = "" Then
                                    walapalahat6 = True
                                    Exit For
                                End If
                            Next

                            If walapalahat5 = True Then
                                Me.Cursor = Cursors.Default
                                MsgBox("Please complete the " & grdcoa.Columns(5).HeaderText & "(s).", MsgBoxStyle.Exclamation, "")
                                Exit Sub
                            ElseIf walapalahat6 = True Then
                                Me.Cursor = Cursors.Default
                                MsgBox("Please complete the " & grdcoa.Columns(6).HeaderText & "(s).", MsgBoxStyle.Exclamation, "")
                                Exit Sub
                            End If
                        Else
                            Dim walapalahat As Boolean = False
                            For Each row As DataGridViewRow In grdcoa.Rows
                                If grdcoa.Rows(row.Index).Cells(3).Value.ToString = "" Then
                                    walapalahat = True
                                    Exit For
                                End If
                            Next

                            If walapalahat = True Then
                                Me.Cursor = Cursors.Default
                                MsgBox("Please complete the " & grdcoa.Columns(3).HeaderText & "(s).", MsgBoxStyle.Exclamation, "")
                                Exit Sub
                            End If
                        End If

                        'check sa sql if may analysis na lahat
                        Dim lahatmayanalysis As Integer = 0
                        sql = "select count(p.cid) from tblcoaparam p"
                        sql = sql & " inner join tblcoaformat f on p.paramid=f.paramid and f.status='1'"
                        sql = sql & " where p.coanum='" & lblcoa.Text & txtcoanum.Text & "' and (p.cresult is null or p.cresult='')"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        lahatmayanalysis = cmd.ExecuteScalar
                        cmd.Dispose()
                        conn.Close()

                        If lahatmayanalysis <> 0 Then
                            Me.Cursor = Cursors.Default
                            MsgBox("Complete analysis first.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If

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
                sql = "Update tblcoa set coacustomer='" & Trim(txtcoacus.Text.Replace("'", "''")) & "', refnum='" & txtrefnum.Text & "', batchnum='" & txtbatch.Text & "', loaddate='" & Format(dateload.Value, "yyyy/MM/dd") & "', deldate='" & Format(datedel.Value, "yyyy/MM/dd") & "', remarks='" & Trim(txtrems.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "', status='2' where coaid='" & lblcoaid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                For Each row As DataGridViewRow In grdcoa.Rows
                    Dim subid As Integer = grdcoa.Rows(row.Index).Cells(0).Value
                    Dim subprod As Date = grdcoa.Rows(row.Index).Cells(1).Value
                    Dim subofprod As Date = grdcoa.Rows(row.Index).Cells(4).Value
                    Dim subshelf As String = grdcoa.Rows(row.Index).Cells(5).Value.ToString.Replace("'", "")
                    Dim subold As String = grdcoa.Rows(row.Index).Cells(6).Value.ToString.Replace("'", "")

                    If Trim(grdcoa.Rows(row.Index).Cells(3).Value.ToString.ToUpper) = "N/A" Or IsDBNull(grdcoa.Rows(row.Index).Cells(3).Value) = True Then
                        sql = "Update tblcoasub set ofproddate='" & Format(subofprod, "yyyy/MM/dd") & "', proddate='" & Format(subprod, "yyyy/MM/dd") & "', expirydate=NULL, shelf='" & subshelf & "', olddays='" & subold & "' where coasubid='" & subid & "'"
                    Else
                        Dim subexpiry As Date = grdcoa.Rows(row.Index).Cells(3).Value
                        sql = "Update tblcoasub set ofproddate='" & Format(subofprod, "yyyy/MM/dd") & "', proddate='" & Format(subprod, "yyyy/MM/dd") & "', expirydate='" & Format(subexpiry, "yyyy/MM/dd") & "', shelf='" & subshelf & "', olddays='" & subold & "' where coasubid='" & subid & "'"
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

    'Private Sub txtprot_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        ' Your code here
    '        SendKeys.Send("{TAB}")
    '        e.Handled = True
    '    End If
    'End Sub

    'Private Sub txtprot_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If Asc(e.KeyChar) = 39 Then
    '        e.Handled = True
    '    ElseIf (Asc(e.KeyChar) >= 46 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then
    '        If Asc(e.KeyChar) = 46 And Trim(txtprot.Text).Contains(".") = True Then
    '            e.Handled = True
    '        End If
    '    Else
    '        e.Handled = True
    '    End If
    'End Sub

    'Private Sub txtprot_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If txtprot.ReadOnly = True Then
    '        txtprot.BackColor = Color.White
    '    Else
    '        txtprot.BackColor = Color.FromArgb(192, 255, 255)
    '    End If
    'End Sub

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
                sql = "Update tblcoa set coacustomer='" & Trim(txtcoacus.Text.Replace("'", "''")) & "', refnum='" & txtrefnum.Text & "', batchnum='" & txtbatch.Text & "', loaddate='" & Format(dateload.Value, "yyyy/MM/dd") & "', deldate='" & Format(datedel.Value, "yyyy/MM/dd") & "', remarks='" & Trim(txtrems.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where coaid='" & lblcoaid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                For Each row As DataGridViewRow In grdcoa.Rows
                    Dim subid As Integer = grdcoa.Rows(row.Index).Cells(0).Value
                    Dim subprod As Date = grdcoa.Rows(row.Index).Cells(1).Value
                    Dim subofprod As Date = grdcoa.Rows(row.Index).Cells(4).Value
                    Dim subshelf As String = grdcoa.Rows(row.Index).Cells(5).Value.ToString.Replace("'", "")
                    Dim subold As String = grdcoa.Rows(row.Index).Cells(6).Value.ToString.Replace("'", "")

                    If Trim(grdcoa.Rows(row.Index).Cells(3).Value.ToString.ToUpper) = "N/A" Or IsDBNull(grdcoa.Rows(row.Index).Cells(3).Value) = True Then
                        sql = "Update tblcoasub set ofproddate='" & Format(subofprod, "yyyy/MM/dd") & "', proddate='" & Format(subprod, "yyyy/MM/dd") & "', expirydate=NULL, shelf='" & subshelf & "', olddays='" & subold & "' where coasubid='" & subid & "'"
                    Else
                        Dim subexpiry As Date = grdcoa.Rows(row.Index).Cells(3).Value
                        sql = "Update tblcoasub set ofproddate='" & Format(subofprod, "yyyy/MM/dd") & "', proddate='" & Format(subprod, "yyyy/MM/dd") & "', expirydate='" & Format(subexpiry, "yyyy/MM/dd") & "', shelf='" & subshelf & "', olddays='" & subold & "' where coasubid='" & subid & "'"
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
            If Panelanalysis.Text = "Master COA" Then
                'ofnum As String, ofitemid As Integer
                rptcoarevise.qty = Val(txtqty.Text)
                rptcoarevise.ofnum = lblofnum.Text & txtofnum.Text
                rptcoarevise.coanum = lblcoa.Text & Trim(txtcoanum.Text)
                rptcoarevise.ofitemid = Val(lblitemid.Text)
                rptcoarevise.stat = ""
                rptcoarevise.coaform = "Master COA"
                rptcoarevise.ShowDialog()
            Else
                'ofnum As String, ofitemid As Integer
                rptcoaformat.qty = Val(txtqty.Text)
                rptcoaformat.ofnum = lblofnum.Text & txtofnum.Text
                rptcoaformat.coanum = lblcoa.Text & Trim(txtcoanum.Text)
                rptcoaformat.ofitemid = Val(lblitemid.Text)
                rptcoaformat.stat = ""
                rptcoaformat.ShowDialog()
            End If

            'refresh list ng location
            defaultform()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub COAToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COAToolStripButton1.Click
        If login.depart = "QCA" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            'dapat automatic mag ssave
            If btnsave.Enabled = True And btnsave.Focused = False Then
                Dim a As String = MsgBox("Do you want to save first?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbYes Then
                    btnsave.PerformClick()
                End If
            End If
            
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
                coaform = ""
                sql = "Select * from tblcoa where coanum='" & lblcoa.Text & txtcoanum.Text & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    Dim ofn As String = dr("ofnum")
                    lblcoaid.Text = dr("coaid")
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

                    If IsDBNull(dr("coaform")) = False Then
                        coaform = dr("coaform")
                    End If
                    Panelanalysis.Text = coaform
                Else
                    MsgBox("COA # cannot found.", MsgBoxStyle.Critical, "")
                    Exit Sub
                End If
                dr.Dispose()

                If coaform = "" Then
                    viewformat()
                    coaformselect.ShowDialog()
                    If coaform = "" Then
                        MsgBox("Invalid format.", MsgBoxStyle.Exclamation, "")
                        defaultform()
                        Exit Sub
                    Else
                        Panelanalysis.Text = coaform
                        'save to db
                        sql = "Update tblcoa set coaform='" & coaform.ToUpper & "' where coaid='" & lblcoaid.Text & "'"
                        command.CommandText = sql
                        command.ExecuteNonQuery()

                        'save paramid to tblcoaparam
                        'coaparameters
                        sql = "insert into tblcoaparam (coanum,coasubid,paramid)"
                        sql = sql & " Select c.coanum, s.coasubid, f.paramid"
                        sql = sql & " from tblcoa c left outer join tblcoasub s on s.coaid=c.coaid"
                        sql = sql & " left outer join tblcoaformat f on c.coaform=f.coaform and f.status='1'"
                        sql = sql & " where c.coanum='" & lblcoa.Text & txtcoanum.Text & "' and c.status=1 and s.coasubid is not null and f.category is not null"
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    End If
                End If

                If coaform = "COA 3" Or coaform = "COA 4" Then
                    grdcoa.Columns(3).Visible = False
                    grdcoa.Columns(5).Visible = True
                    grdcoa.Columns(6).Visible = True
                Else
                    grdcoa.Columns(3).Visible = True
                    grdcoa.Columns(5).Visible = False
                    grdcoa.Columns(6).Visible = False
                End If

                'load yung wrs info
                sql = "Select wrsnum, customer, cusref, platenum, driver from tblorderfill"
                sql = sql & " where ofnum='" & lblofnum.Text & txtofnum.Text & "' and branch='" & login.branch & "'"
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
                sql = "Select i.numbags, l.ticketseries from tbloflog l left outer join tblofitem i on l.ofitemid=i.ofitemid"
                sql = sql & " where l.ofitemid='" & lblitemid.Text & "' and i.branch='" & login.branch & "'"
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
                        grdcoa.Rows.Add(dr("coasubid"), Format(dr("proddate"), "yyyy/MM/dd"), dr("tseries"), dr("expirydate"), Format(dr("proddate"), "yyyy/MM/dd"), dr("shelf").ToString, dr("olddays").ToString)
                    Else
                        grdcoa.Rows.Add(dr("coasubid"), Format(dr("proddate"), "yyyy/MM/dd"), dr("tseries"), dr("expirydate"), Format(dr("proddate"), "yyyy/MM/dd"), dr("shelf").ToString, dr("olddays").ToString)
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

    Private Sub viewformat()
        Try
            coaformselect.cmbformat.Items.Clear()

            sql = "Select distinct coaform from tblcoaformat where status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                coaformselect.cmbformat.Items.Add(dr("coaform"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

        Catch ex As Exception

        End Try
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

    Private Sub txtothers_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
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

            selectparam()

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

    Private Sub selectparam()
        Try
            lblcoasubid.Text = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(0).Value
            lbldate.Text = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(1).Value

            'gparam.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor
            'grheo.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor
            'grems.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor
            sql = "Select others from tblcoasub where coasubid='" & lblcoasubid.Text & "'"
            connect()
            Dim cmd1 As SqlCommand = New SqlCommand(sql, conn)
            Dim dr1 As SqlDataReader = cmd1.ExecuteReader
            If dr1.Read Then
                'txtmois.Text = dr1("moisture")
                'txtprot.Text = dr1("protein")
                'txtash.Text = dr1("ash")
                'txtwet.Text = dr1("wetgluten")
                'txtwater.Text = dr1("water")
                txtothers.Text = dr1("others")
            End If
            dr1.Dispose()
            cmd1.Dispose()

            'load ung coaform
            grd1.Rows.Clear()
            grd2.Rows.Clear()
            grd3.Rows.Clear()
            grd4.Rows.Clear()
            Dim ctr As Integer = 0, cattemp As String = ""
            sql = "select c.cid, f.type, f.category, f.cname, f.cvalue, c.cresult"
            sql = sql & " from tblcoaformat f right outer join tblcoaparam c on f.paramid=c.paramid"
            sql = sql & " right outer join tblcoa a on c.coanum=a.coanum"
            sql = sql & " where f.status='1' and c.coanum='" & lblcoa.Text & txtcoanum.Text & "' and a.branch='" & login.branch & "'"
            sql = sql & " and c.coasubid='" & grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(0).Value & "'" 'order by f.category"
            sql = sql & " ORDER BY CASE WHEN f.category = 'PARAMETERS' THEN '1'"
            sql = sql & " WHEN f.category = 'RHEOLOGICAL' THEN '2'"
            sql = sql & " WHEN f.category = 'MYCOTOXIN' THEN '3' ELSE f.category END ASC"
            connect()
            cmd1 = New SqlCommand(sql, conn)
            dr1 = cmd1.ExecuteReader
            While dr1.Read
                If cattemp <> dr1("category") Then
                    ctr += 1
                    cattemp = dr1("category")
                End If

                If ctr = 1 Then
                    grd1.Columns(1).HeaderText = cattemp
                    grd1.Columns(2).HeaderText = dr1("type")
                    grd1.Rows.Add(dr1("cid"), dr1("cname"), dr1("cvalue"), dr1("cresult").ToString)
                ElseIf ctr = 2 Then
                    grd2.Columns(1).HeaderText = cattemp
                    grd2.Columns(2).HeaderText = dr1("type")
                    grd2.Rows.Add(dr1("cid"), dr1("cname"), dr1("cvalue"), dr1("cresult").ToString)
                ElseIf ctr = 3 Then
                    grd3.Columns(1).HeaderText = cattemp
                    grd3.Columns(2).HeaderText = dr1("type")
                    grd3.Rows.Add(dr1("cid"), dr1("cname"), dr1("cvalue"), dr1("cresult").ToString)
                ElseIf ctr = 4 Then
                    grd4.Columns(1).HeaderText = cattemp
                    grd4.Columns(2).HeaderText = dr1("type")
                    grd4.Rows.Add(dr1("cid"), dr1("cname"), dr1("cvalue"), dr1("cresult").ToString)
                End If
            End While
            dr1.Dispose()
            cmd1.Dispose()
            conn.Close()

            If grd1.Rows.Count <> 0 And grd2.Rows.Count <> 0 And grd3.Rows.Count = 0 And grd4.Rows.Count = 0 Then
                TableLayoutPanel1.RowStyles(0).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(0).Height = 35
                TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(1).Height = 35
                TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(2).Height = 0
                TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(3).Height = 0
                If Panelanalysis.Text = "Master COA" Then
                    TableLayoutPanel1.RowStyles(4).SizeType = SizeType.Percent
                    TableLayoutPanel1.RowStyles(4).Height = 30
                Else
                    TableLayoutPanel1.RowStyles(4).SizeType = SizeType.Percent
                    TableLayoutPanel1.RowStyles(4).Height = 0
                End If
            ElseIf grd1.Rows.Count <> 0 And grd2.Rows.Count <> 0 And grd3.Rows.Count <> 0 And grd4.Rows.Count = 0 Then
                TableLayoutPanel1.RowStyles(0).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(0).Height = 35
                TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(1).Height = 30
                TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(2).Height = 35
                TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(3).Height = 0
                TableLayoutPanel1.RowStyles(4).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(4).Height = 0
            ElseIf grd1.Rows.Count <> 0 And grd2.Rows.Count <> 0 And grd3.Rows.Count <> 0 And grd4.Rows.Count <> 0 Then
                TableLayoutPanel1.RowStyles(0).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(0).Height = 25
                TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(1).Height = 25
                TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(2).Height = 25
                TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(3).Height = 25
                TableLayoutPanel1.RowStyles(4).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(4).Height = 0
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub AnalysisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnalysisToolStripMenuItem.Click
        If login.depart = "QCA" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
            btnok.Enabled = True
            btnnot.Enabled = True

            analysisreadonlyfalse()

            lblcoasubid.Text = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(0).Value
            lbldate.Text = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(1).Value
            'gparam.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor
            'grheo.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor
            'grems.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor

            ginfo.Enabled = False
            Panelbuttons.Enabled = False
            ContextMenuStrip1.Enabled = False
            grdcoa.Enabled = False
            ToolStrip1.Enabled = False
        Else
            MsgBox("Access denied.", MsgBoxStyle.Critical, "")
        End If

    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            If login.depart = "QCA" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                'check if orderfill is not cancelled
                sql = "Select status from tblcoa where coaid='" & lblcoaid.Text & "'"
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


                'coacnf = False
                'confirmsave.GroupBox1.Text = login.wgroup
                'confirmsave.ShowDialog()
                'If coacnf = True Then
                '    'save yung tblsub
                '    'sql = "Update tblcoasub set moisture='" & txtmois.Text & "', protein='" & txtprot.Text & "', ash='" & txtash.Text & "', wetgluten='" & txtwet.Text & "', water='" & txtwater.Text & "', others='" & txtothers.Text & "' where coasubid='" & lblcoasubid.Text & "'"
                '    'conn.Open()
                '    'cmd = New SqlCommand(sql, conn)
                '    'cmd.ExecuteNonQuery()
                '    'cmd.Dispose()
                '    'conn.Close()

                '    '/MsgBox("Successfully saved analysis.", MsgBoxStyle.Information, "")
                'End If
                ExecuteSave(strconn)
            Else
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
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

    Private Sub ExecuteSave(ByVal connectionString As String)
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

                If grd1.Rows.Count <> 0 Then
                    For Each row As DataGridViewRow In grd1.Rows
                        Dim cid As Integer = grd1.Rows(row.Index).Cells(0).Value
                        Dim cresult As String = Trim(grd1.Rows(row.Index).Cells(3).Value)
                        sql = "Update tblcoaparam set cresult='" & cresult & "' where cid='" & cid & "'"
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    Next
                End If

                If grd2.Rows.Count <> 0 Then
                    For Each row As DataGridViewRow In grd2.Rows
                        Dim cid As Integer = grd2.Rows(row.Index).Cells(0).Value
                        Dim cresult As String = Trim(grd2.Rows(row.Index).Cells(3).Value)
                        sql = "Update tblcoaparam set cresult='" & cresult & "' where cid='" & cid & "'"
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    Next
                End If

                If grd3.Rows.Count <> 0 Then
                    For Each row As DataGridViewRow In grd3.Rows
                        Dim cid As Integer = grd3.Rows(row.Index).Cells(0).Value
                        Dim cresult As String = Trim(grd3.Rows(row.Index).Cells(3).Value)
                        sql = "Update tblcoaparam set cresult='" & cresult & "' where cid='" & cid & "'"
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    Next
                End If

                If grd4.Rows.Count <> 0 Then
                    For Each row As DataGridViewRow In grd4.Rows
                        Dim cid As Integer = grd4.Rows(row.Index).Cells(0).Value
                        Dim cresult As String = Trim(grd4.Rows(row.Index).Cells(3).Value)
                        sql = "Update tblcoaparam set cresult='" & cresult & "' where cid='" & cid & "'"
                        command.CommandText = sql
                        command.ExecuteNonQuery()
                    Next
                End If

                If Panelanalysis.Text.ToUpper = "MASTER COA" Then
                    sql = "Update tblcoasub set others='" & Trim(txtothers.Text) & "' where coasubid='" & lblcoasubid.Text & "'"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default

                ginfo.Enabled = True
                Panelbuttons.Enabled = True
                ContextMenuStrip1.Enabled = True
                lbldate.Text = ""
                analysisreadonlytrue()
                grdcoa.Enabled = True
                btnok.Enabled = False
                btnnot.Enabled = False
                ToolStrip1.Enabled = True

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

    Public Sub analysisreadonlytrue()
        'txtmois.ReadOnly = True
        'txtprot.ReadOnly = True
        'txtash.ReadOnly = True
        'txtwet.ReadOnly = True
        'txtwater.ReadOnly = True
        txtothers.ReadOnly = True
        grd1.ReadOnly = True
        grd2.ReadOnly = True
        grd3.ReadOnly = True
        grd4.ReadOnly = True
    End Sub

    Public Sub analysisreadonlyfalse()
        'txtmois.ReadOnly = False
        'txtprot.ReadOnly = False
        'txtash.ReadOnly = False
        'txtwet.ReadOnly = False
        'txtwater.ReadOnly = False
        txtothers.ReadOnly = False
        grd1.ReadOnly = False
        grd1.Columns(0).ReadOnly = True
        grd1.Columns(1).ReadOnly = True
        grd1.Columns(2).ReadOnly = True
        grd2.ReadOnly = False
        grd2.Columns(0).ReadOnly = True
        grd2.Columns(1).ReadOnly = True
        grd2.Columns(2).ReadOnly = True
        grd3.ReadOnly = False
        grd3.Columns(0).ReadOnly = True
        grd3.Columns(1).ReadOnly = True
        grd3.Columns(2).ReadOnly = True
        grd4.ReadOnly = False
        grd4.Columns(0).ReadOnly = True
        grd4.Columns(1).ReadOnly = True
        grd4.Columns(2).ReadOnly = True
    End Sub

    Private Sub grdcoa_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdcoa.SelectionChanged
        If grd1.Rows.Count <> 0 Then
            selectparam()
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

    Private Sub grd1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd1.CellClick
        grd1.BeginEdit(True)
    End Sub

    Private Sub grd1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd1.CellContentClick

    End Sub

    Private Sub grd1_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd1.ReadOnlyChanged
        If grd1.ReadOnly = False Then
            grd1.AlternatingRowsDefaultCellStyle.BackColor = Nothing
            grd1.Columns(3).DefaultCellStyle.BackColor = Color.Yellow
        Else
            grd1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan
            grd1.Columns(3).DefaultCellStyle.BackColor = grd1.Columns(2).DefaultCellStyle.BackColor
        End If
    End Sub

    Private Sub grd2_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd2.CellClick
        grd2.BeginEdit(True)
    End Sub

    Private Sub grd2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd2.CellContentClick

    End Sub

    Private Sub grd2_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd2.ReadOnlyChanged
        If grd2.ReadOnly = False Then
            grd2.AlternatingRowsDefaultCellStyle.BackColor = Nothing
            grd2.Columns(3).DefaultCellStyle.BackColor = Color.Yellow
        Else
            grd2.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan
            grd2.Columns(3).DefaultCellStyle.BackColor = grd2.Columns(2).DefaultCellStyle.BackColor
        End If
    End Sub

    Private Sub grd3_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd3.CellClick
        grd3.BeginEdit(True)
    End Sub

    Private Sub grd3_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd3.CellContentClick

    End Sub

    Private Sub grd3_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd3.ReadOnlyChanged
        If grd3.ReadOnly = False Then
            grd3.AlternatingRowsDefaultCellStyle.BackColor = Nothing
            grd3.Columns(3).DefaultCellStyle.BackColor = Color.Yellow
        Else
            grd3.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan
            grd3.Columns(3).DefaultCellStyle.BackColor = grd3.Columns(2).DefaultCellStyle.BackColor
        End If
    End Sub

    Private Sub grd4_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd4.CellClick
        grd4.BeginEdit(True)
    End Sub

    Private Sub grd4_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd4.CellContentClick

    End Sub

    Private Sub grd4_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd4.ReadOnlyChanged
        If grd4.ReadOnly = False Then
            grd4.AlternatingRowsDefaultCellStyle.BackColor = Nothing
            grd4.Columns(3).DefaultCellStyle.BackColor = Color.Yellow
        Else
            grd4.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan
            grd4.Columns(3).DefaultCellStyle.BackColor = grd4.Columns(2).DefaultCellStyle.BackColor
        End If
    End Sub

    Private Sub btnnot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnot.Click
        ginfo.Enabled = True
        Panelbuttons.Enabled = True
        ContextMenuStrip1.Enabled = True
        analysisreadonlytrue()
        grdcoa.Enabled = True
        ToolStrip1.Enabled = True
        btnok.Enabled = False
        btnnot.Enabled = False
        selectparam()
    End Sub
End Class