Imports System.IO
Imports System.Data.SqlClient

Public Class coacreate
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim proceed As Boolean = False
    Public coacreatecnf As Boolean = False
    Dim closingna As Boolean = False, rowindex As Integer = 0

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

    Private Sub coacreate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtcoanum.Text = ""
        txtofnum.Text = ""
        cmbitem.Text = ""
        txtwrsnum.Text = ""
        lblofid.Text = ""
        txtwrsnum.ReadOnly = True
        txtofnum.ReadOnly = True

        cmbitem.DropDownWidth = 300
        viewformat()
    End Sub

    Private Sub viewformat()
        Try
            cmbformat.Items.Clear()

            sql = "Select distinct coaform from tblcoaformat where status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbformat.Items.Add(dr("coaform"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub OFToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OFToolStripButton1.Click
        txtofnum.ReadOnly = False
        coaofnumselect.ShowDialog()
        txtofnum.ReadOnly = False
        btnsearch.PerformClick()
    End Sub

    Private Sub btnwrssearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnwrssearch.Click
        Try
            If txtwrsnum.ReadOnly = True Then
                'parang warning para maisave muna kung may pending na order fill
                '/If ginfo.Enabled = True Then
                '/Else
                txtwrsnum.ReadOnly = False
                txtwrsnum.Focus()
                cmbitem.Enabled = False
                cmbformat.Enabled = False
                btnok.Enabled = False
                '/End If
            Else
                If txtwrsnum.Text <> "" Then
                    txtwrsnum.Focus()
                    viewwrsnum()
                Else
                    MsgBox("Input order fill number first.", MsgBoxStyle.Exclamation, "")
                    txtwrsnum.Focus()
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

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            If txtofnum.ReadOnly = True Then
                'parang warning para maisave muna kung may pending na order fill
                '/If ginfo.Enabled = True Then
                '/Else
                txtofnum.ReadOnly = False
                txtofnum.Focus()
                cmbitem.Enabled = False
                cmbformat.Enabled = False
                btnok.Enabled = False
                '/End If
            Else
                If txtofnum.Text <> "" Then
                    txtofnum.Focus()
                    viewofnum()
                Else
                    MsgBox("Input order fill number first.", MsgBoxStyle.Exclamation, "")
                    txtofnum.Focus()
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

    Public Sub viewofnum()
        Try
            If txtofnum.ReadOnly = False Then
                proceed = False

                sql = "Select * from tblorderfill where ofnum='" & lblofnum.Text & Trim(txtofnum.Text) & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 2 Then 'completed
                        'view
                        proceed = True
                        txtwrsnum.Text = dr("wrsnum")
                        txtofnum.ReadOnly = True
                        lblofid.Text = dr("ofid")

                    ElseIf dr("status") = 3 Then 'cancelled
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Information, "")

                    ElseIf dr("status") = 1 Then 'in process
                        MsgBox("Order fill is still in process.", MsgBoxStyle.Exclamation, "")
                    End If
                Else
                    MsgBox("Cannot find order fill number.", MsgBoxStyle.Critical, "")
                    txtofnum.Text = ""
                    txtofnum.Focus()
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If proceed = True Then
                    'view items
                    cmbitem.Text = ""
                    cmbitem.Items.Clear()
                    cmbitem.Items.Add("")

                    sql = "Select * from tblofitem where ofnum='" & lblofnum.Text & Trim(txtofnum.Text) & "' and status='2' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        cmbitem.Items.Add(dr("itemname"))
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    cmbitem.Enabled = True
                    cmbformat.Enabled = True
                    btnok.Enabled = False
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

    Public Sub viewwrsnum()
        Try
            If txtwrsnum.ReadOnly = False Then
                proceed = False

                sql = "Select * from tblorderfill where wrsnum='" & Trim(txtwrsnum.Text) & "' and status='2' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 2 Then 'completed
                        'view
                        proceed = True
                        txtofnum.Text = dr("ofnum").ToString.Substring(3, dr("ofnum").ToString.Length - 3)
                        txtwrsnum.ReadOnly = True
                        lblofid.Text = dr("ofid")

                    ElseIf dr("status") = 3 Then 'cancelled
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Information, "")

                    ElseIf dr("status") = 1 Then 'in process
                        MsgBox("Order fill is still in process.", MsgBoxStyle.Exclamation, "")
                    End If
                Else
                    MsgBox("Cannot find order fill number.", MsgBoxStyle.Critical, "")
                    lblofid.Text = ""
                    txtwrsnum.Text = ""
                    txtwrsnum.Focus()
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If proceed = True Then

                    'view items
                    cmbitem.Items.Clear()
                    cmbitem.Items.Add("")

                    sql = "Select * from tblofitem where ofnum='" & lblofnum.Text & Trim(txtofnum.Text) & "' and status='2' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        cmbitem.Items.Add(dr("itemname"))
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    cmbitem.Enabled = True
                    cmbformat.Enabled = True
                    btnok.Enabled = False
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

    Public Sub viewcoanum()
        Try
            Dim coanum As String = "1", temp As String = "", tlognum As String = ""
            Dim prefix As String = ""

            sql = "Select Top 1 * from tblcoa where branch='" & login.branch & "' order by coaid DESC"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                '/coanum = Val(dr("ofid")) + 1
            End If
            cmd.Dispose()
            dr.Dispose()
            conn.Close()

            'check kung pang ilang coa NA SA YEAR NA 2018 na
            sql = "Select Count(coaid) from tblcoa where coayear=Year(GetDate()) and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            coanum = cmd.ExecuteScalar + 1
            cmd.Dispose()
            conn.Close()

            If coanum < 1000000 Then
                For vv As Integer = 1 To 6 - coanum.Length
                    temp += "0"
                Next
                'lbltrnum.Text = Date.Now.Year & "-" & Format(Date.Now, "MM") & Format(Date.Now, "dd") & temp & trnum
            End If

            tlognum = "COA." & Format(Date.Now, "yy") & "-" & temp & coanum
            txtcoanum.Text = tlognum

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
            If login.depart = "QCA" Or login.depart = "All" Then
                'insert into tblcoa
                'check if may laman lahat
                If Trim(txtcoanum.Text) <> "" And Trim(txtwrsnum.Text) <> "" And Trim(txtofnum.Text) <> "" And Trim(cmbitem.Text) <> "" And Trim(cmbformat.Text) <> "" Then
                    coacreatecnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If coacreatecnf = True Then

                        viewcoanum()

                        Dim withcoanum As Boolean = False
                        'check if may ganito ng coanum then exit sub
                        sql = "Select coanum from tblcoa where coanum='" & txtcoanum.Text & "' and branch='" & login.branch & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            withcoanum = True
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()

                        If withcoanum = True Then
                            viewcoanum()
                        End If

                        ExecuteCreate(strconn)
                    End If
                Else
                    MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
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

    Private Sub ExecuteCreate(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                Dim coanum As String = "1", temp As String = "", tlognum As String = ""
                'check kung pang ilang coa NA SA YEAR NA 2018 na
                sql = "Select Count(coaid) from tblcoa where coayear=Year(GetDate()) and branch='" & login.branch & "'"
                command.CommandText = sql
                coanum = command.ExecuteScalar + 1

                If coanum < 1000000 Then
                    For vv As Integer = 1 To 6 - coanum.Length
                        temp += "0"
                    Next
                    'lbltrnum.Text = Date.Now.Year & "-" & Format(Date.Now, "MM") & Format(Date.Now, "dd") & temp & trnum
                End If

                tlognum = "COA." & Format(Date.Now, "yy") & "-" & temp & coanum
                txtcoanum.Text = tlognum

                'insert into tblcoa status=2
                sql = "Insert into tblcoa (coanum, coayear, coadate, ofid, ofnum, ofitemid, itemname, whsename, shift, remarks, datecreated, createdby, datemodified, modifiedby, status, branch, coaform)"
                sql = sql & " values ('" & txtcoanum.Text & "', YEAR(GETDATE()), GetDate(), '" & lblofid.Text & "', '" & lblofnum.Text & txtofnum.Text & "', '" & lblitemid.Text & "', '" & cmbitem.Text & "', '" & login.logwhse & "', '" & login.logshift & "', '', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1', '" & login.branch & "', '" & cmbformat.SelectedItem & "')"
                command.CommandText = sql
                command.ExecuteNonQuery()

                '/MsgBox("dapat dito na insert na din yung mga groupby ng production date")
                Dim tempseries As String = "", tempprod As Date
                grdmerge.Rows.Clear()
                For Each row As DataGridViewRow In grdcoa.Rows
                    Dim prod As Date = CDate(grdcoa.Rows(row.Index).Cells(0).Value)
                    Dim tseries As String = grdcoa.Rows(row.Index).Cells(1).Value

                    If tempprod.ToString = "" Then
                        tempprod = prod
                        tempseries = tseries
                        grdmerge.Rows.Add(Format(tempprod, "yyyy/MM/dd"), tempseries)
                    ElseIf tempprod = prod Then
                        tempseries = tempseries & " " & tseries
                        For Each rowmerge As DataGridViewRow In grdmerge.Rows
                            Dim mergeprod As Date = CDate(grdmerge.Rows(rowmerge.Index).Cells(0).Value)
                            If mergeprod = tempprod Then
                                grdmerge.Rows(rowmerge.Index).Cells(1).Value = tempseries
                                Exit For
                            End If
                        Next
                    ElseIf tempprod <> prod Then
                        tempprod = prod
                        tempseries = tseries
                        grdmerge.Rows.Add(Format(tempprod, "yyyy/MM/dd"), tempseries)
                    End If
                Next

                'insert into tblcoasub
                For Each row As DataGridViewRow In grdmerge.Rows
                    Dim prod As Date = CDate(grdmerge.Rows(row.Index).Cells(0).Value)
                    Dim tseries As String = grdmerge.Rows(row.Index).Cells(1).Value
                    sql = "Insert into tblcoasub (coaid, coanum, proddate, tseries, moisture, protein, ash, wetgluten, water, others) values((Select coaid from tblcoa where coanum='" & txtcoanum.Text & "' and branch='" & login.branch & "'), '" & txtcoanum.Text & "', '" & Format(prod, "yyyy/MM/dd") & "', '" & tseries & "', '0', '0', '0', '0', '0', '')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                For Each row As DataGridViewRow In grdcoa.Rows
                    Dim prod As Date = CDate(grdcoa.Rows(row.Index).Cells(0).Value)
                    Dim ofprod As Date = CDate(grdcoa.Rows(row.Index).Cells(3).Value)
                    sql = "Insert into tblcoaoflog (coanum, coasubid, ofproddate) values('" & txtcoanum.Text & "', (Select tblcoasub.coasubid from tblcoa right outer join tblcoasub on tblcoa.coaid=tblcoasub.coaid where tblcoa.coanum='" & txtcoanum.Text & "' and tblcoasub.proddate='" & Format(prod, "yyyy/MM/dd") & "' and tblcoa.branch='" & login.branch & "'), '" & Format(ofprod, "yyyy/MM/dd") & "')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next

                'coaparameters
                sql = "insert into tblcoaparam (coanum,coasubid,paramid)"
                sql = sql & " Select c.coanum, s.coasubid, f.paramid"
                sql = sql & " from tblcoa c left outer join tblcoasub s on s.coaid=c.coaid"
                sql = sql & " left outer join tblcoaformat f on c.coaform=f.coaform and f.status='1'"
                sql = sql & " where c.coanum='" & txtcoanum.Text & "' and c.status=1 and s.coasubid is not null and f.category is not null"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'update coanum ng ofitemid
                sql = "Update tblofitem set coanum='" & Trim(txtcoanum.Text) & "' where ofitemid='" & lblitemid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully created.", MsgBoxStyle.Information, "")

                grdmerge.Rows.Clear()

                Dim a As String = MsgBox("Do you want to create another COA?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a <> vbYes Then
                    coa.txtcoanum.Text = txtcoanum.Text.ToString.Substring(4, txtcoanum.Text.Length - 4)
                    coa.txtwrs.Text = txtwrsnum.Text
                    coa.txtofnum.Text = txtofnum.Text
                    coa.txtitem.Text = Trim(cmbitem.Text)
                    coa.lblitemid.Text = lblitemid.Text
                    Me.Close()
                End If

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

    Private Sub cmbitem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitem.Leave
        
    End Sub

    Private Sub cmbitem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbitem.SelectedIndexChanged
        
    End Sub

    Public Sub selectitem()
        Try
            If Trim(cmbitem.Text) <> "" Then
                lblitemid.Text = ""
                'view ofitemid
                sql = "Select ofitemid from tblofitem where ofnum='" & lblofnum.Text & Trim(txtofnum.Text) & "' and itemname='" & Trim(cmbitem.Text) & "' and status='2' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    lblitemid.Text = dr("ofitemid")
                Else
                    MsgBox("Invalid item.", MsgBoxStyle.Exclamation, "")
                    grdcoa.Rows.Clear()
                    cmbitem.Text = ""
                    cmbitem.Focus()
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If lblitemid.Text <> "" Then
                    'check if may coa na ung item na yun 
                    sql = "Select status from tblcoa where ofnum='" & lblofnum.Text & Trim(txtofnum.Text) & "' and itemname='" & Trim(cmbitem.Text) & "' and ofitemid='" & lblitemid.Text & "' and status<>'3' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MsgBox(Trim(cmbitem.Text) & " is already with COA.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    grdcoa.Rows.Clear()
                    'load yung sa tblofitem

                    sql = "Select tbloflog.logsheetdate from tbloflog left outer join tblofitem on tbloflog.ofitemid=tblofitem.ofitemid where tbloflog.ofitemid='" & lblitemid.Text & "' group by tbloflog.logsheetdate"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        grdcoa.Rows.Add((Format(dr("logsheetdate"), "yyyy/MM/dd")), "", "", (Format(dr("logsheetdate"), "yyyy/MM/dd")), (Format(dr("logsheetdate"), "yyyy/MM/dd")))
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    For Each row As DataGridViewRow In grdcoa.Rows
                        Dim tempseries As String = ""
                        sql = "Select tbloflog.ticketseries from tbloflog left outer join tblofitem on tbloflog.ofitemid=tblofitem.ofitemid where tbloflog.ofitemid='" & lblitemid.Text & "' and tbloflog.logsheetdate='" & grdcoa.Rows(row.Index).Cells(0).Value & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        While dr.Read
                            tempseries = tempseries & dr("ticketseries")
                        End While
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()

                        grdcoa.Rows(row.Index).Cells(1).Value = tempseries
                    Next

                    viewcoanum()

                    btnok.Enabled = True
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

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtofnum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtofnum.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtofnum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtofnum.TextChanged

    End Sub

    Private Sub txtwrsnum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwrsnum.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnwrssearch.PerformClick()
        End If
    End Sub

    Private Sub txtwrsnum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwrsnum.TextChanged

    End Sub

    Private Sub grdcoa_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcoa.CellContentClick

    End Sub

    Private Sub grdcoa_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcoa.CellEndEdit
        Try
            'column1
            If (e.ColumnIndex = 0) Then
                If grdcoa.Rows(rowindex).Cells(0).Style.BackColor = Color.HotPink Then
                    'check if numeric
                    ' Checking numeric value for Column0 only
                    If IsDate(grdcoa.Rows(e.RowIndex).Cells(0).Value) = True Then
                        Dim tempprod As Date = grdcoa.Rows(e.RowIndex).Cells(0).Value

                        If (grdcoa.Rows(e.RowIndex).Cells(2).Value) = "" Then
                            If grdcoa.Rows(e.RowIndex).Cells(0).Value <> grdcoa.Rows(e.RowIndex).Cells(3).Value Then
                                '/MsgBox(grdcoa.Rows(e.RowIndex).Cells(0).Value < grdcoa.Rows(e.RowIndex).Cells(3).Value)
                                'check dapat hindi mas mababa sa prod date
                                If grdcoa.Rows(e.RowIndex).Cells(0).Value < grdcoa.Rows(e.RowIndex).Cells(3).Value Then
                                    MsgBox("Cannot change production date into previous dates.", MsgBoxStyle.Exclamation, "")
                                    grdcoa.Rows(e.RowIndex).Cells(0).Value = grdcoa.Rows(e.RowIndex).Cells(3).Value
                                    grdcoa.Rows(e.RowIndex).Cells(0).ReadOnly = True
                                    grdcoa.Rows(rowindex).Cells(0).Style.BackColor = grdcoa.Rows(rowindex).Cells(1).Style.BackColor
                                    Exit Sub
                                End If

                                coacreatecnf = False
                                confirmsave.GroupBox1.Text = login.wgroup
                                confirmsave.ShowDialog()
                                If coacreatecnf = True Then
                                    grdcoa.Rows(e.RowIndex).Cells(0).Value = Format(tempprod, "yyyy/MM/dd")
                                Else
                                    MsgBox("User cancelled editing production date.", MsgBoxStyle.Information, "")
                                    grdcoa.Rows(e.RowIndex).Cells(0).Value = grdcoa.Rows(e.RowIndex).Cells(3).Value
                                End If
                            End If
                        Else
                            If grdcoa.Rows(e.RowIndex).Cells(0).Value < grdcoa.Rows(e.RowIndex).Cells(3).Value Then
                                MsgBox("Cannot change production date into previous dates.", MsgBoxStyle.Exclamation, "")
                                grdcoa.Rows(e.RowIndex).Cells(0).Value = grdcoa.Rows(e.RowIndex).Cells(3).Value
                                grdcoa.Rows(e.RowIndex).Cells(0).ReadOnly = True
                                grdcoa.Rows(rowindex).Cells(0).Style.BackColor = grdcoa.Rows(rowindex).Cells(1).Style.BackColor
                                Exit Sub
                            End If

                            Dim tempexpiry As Date = grdcoa.Rows(e.RowIndex).Cells(2).Value

                            If tempexpiry <= tempprod Then
                                MsgBox("Invalid date. Production date is less than or equal to the Expiry date.", MsgBoxStyle.Exclamation, "")
                                grdcoa.Rows(e.RowIndex).Cells(0).Value = grdcoa.Rows(e.RowIndex).Cells(3).Value
                            Else
                                If grdcoa.Rows(e.RowIndex).Cells(0).Value <> grdcoa.Rows(e.RowIndex).Cells(3).Value Then
                                    coacreatecnf = False
                                    confirmsave.GroupBox1.Text = login.wgroup
                                    confirmsave.ShowDialog()
                                    If coacreatecnf = True Then
                                        grdcoa.Rows(e.RowIndex).Cells(0).Value = Format(tempprod, "yyyy/MM/dd")
                                    Else
                                        MsgBox("User cancelled editing production date.", MsgBoxStyle.Information, "")
                                        grdcoa.Rows(e.RowIndex).Cells(0).Value = grdcoa.Rows(e.RowIndex).Cells(3).Value
                                    End If
                                End If
                            End If
                        End If
                    Else
                        MsgBox("Invalid date.", MsgBoxStyle.Exclamation, "")
                        grdcoa.Rows(e.RowIndex).Cells(0).Value = grdcoa.Rows(e.RowIndex).Cells(3).Value
                    End If
                End If

                grdcoa.Rows(e.RowIndex).Cells(0).ReadOnly = True
                grdcoa.Rows(rowindex).Cells(0).Style.BackColor = grdcoa.Rows(rowindex).Cells(1).Style.BackColor

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

                Me.ContextMenuStrip1.Show(Cursor.Position)
                If e.ColumnIndex = 0 Then
                    EditNoOfSelectedBagsToolStripMenuItem.Visible = True
                Else
                    EditNoOfSelectedBagsToolStripMenuItem.Visible = False
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

    Private Sub EditNoOfSelectedBagsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditNoOfSelectedBagsToolStripMenuItem.Click
        Try
            If login.depart = "QCA" Or login.depart = "Admin Dispatching" Or login.depart = "All" Then
                grdcoa.ReadOnly = False
                For i = 0 To 3
                    grdcoa.Columns(i).ReadOnly = True
                Next

                rowindex = grdcoa.CurrentRow.Index
                grdcoa.ClearSelection()
                grdcoa.Rows(rowindex).Cells(0).ReadOnly = False
                grdcoa.Rows(rowindex).Cells(0).Style.BackColor = Color.HotPink
                grdcoa.Rows(rowindex).Cells(0).Selected = True
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

    Private Sub cmbitem_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitem.SelectedValueChanged
        selectitem()
    End Sub

    Private Sub gselect_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gselect.Enter

    End Sub
End Class