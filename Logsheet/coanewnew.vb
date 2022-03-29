Imports System.IO
Imports System.Data.SqlClient

Public Class coanewnew
    Dim lines = System.IO.File.ReadAllLines("connectionstring.txt")
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim proceed As Boolean = False
    Public coacnf As Boolean = False
    Dim closingna As Boolean = False

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

    Private Sub coanew_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        closingna = True
    End Sub

    Private Sub coanew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        defaultform()
        txtofnum.Focus()
        ginfo.Enabled = False
        gparam.Enabled = False
        grheo.Enabled = False
        cmbitem.Enabled = False
        btnok.Enabled = False
        btncancel.Enabled = False
        btnprint.Enabled = False
        btnsave.Enabled = False
        viewofnum()
        dateexpiry.MinDate = dateprod.Value
    End Sub

    Private Sub coanew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If login.logshift = "" Or login.logwhse = "" Then
            MsgBox("Choose warehouse.", MsgBoxStyle.Exclamation, "")
            chooseshift.ShowDialog()
            Me.Dispose()
        Else
            txtofnum.Focus()
            ginfo.Enabled = False
            gparam.Enabled = False
            grheo.Enabled = False
            cmbitem.Enabled = False
            btnok.Enabled = False
            btncancel.Enabled = False
            btnprint.Enabled = False
            btnsave.Enabled = False
            viewofnum()
        End If
    End Sub

    Public Sub defaultform()
        lblitemid.Text = ""
        txtrefnum.Text = ""
        txtbatch.Text = ""
        txtqty.Text = ""
        dateprod.Value = Date.Now
        dateexpiry.Value = Date.Now
        dateload.Value = Date.Now
        datedel.Value = Date.Now
        txtcus.Text = ""
        txtpo.Text = ""
        txttruck.Text = ""
        txtseries.Text = ""
        cmbitem.Text = ""
        cmbitem.Items.Clear()
        txtmois.Text = ""
        txtprot.Text = ""
        txtash.Text = ""
        txtwet.Text = ""
        txtwater.Text = ""
        btnok.Text = "Confirm"

        cmbitem.BackColor = SystemColors.Control
        txtqty.BackColor = SystemColors.Control
        txtcus.BackColor = SystemColors.Control
        txtpo.BackColor = SystemColors.Control
        txttruck.BackColor = SystemColors.Control
        txtseries.BackColor = SystemColors.Control
    End Sub


    Public Sub viewcoanum()
        Try
            Dim coanum As String = "1", temp As String = "", tlognum As String = ""
            Dim prefix As String = ""

            sql = "Select Top 1 * from tblcoa order by coaid DESC"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                '/coanum = Val(dr("ofid")) + 1
            End If
            cmd.Dispose()
            dr.Dispose()

            'check kung pang ilang LOGSHEET NA SA YEAR NA 2018 na
            sql = "Select Count(coaid) from tblcoa where coayear=Year(GetDate())"
            connect()
            cmd = New SqlCommand(sql, conn)
            coanum = cmd.ExecuteScalar + 1
            cmd.Dispose()

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
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub cmbitem_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not cmbitem.Items.Contains(Trim(cmbitem.Text.ToUpper)) = True And Trim(cmbitem.Text.ToUpper) <> "" Then
                MsgBox("Invalid Item name.", MsgBoxStyle.Critical, "")
                cmbitem.Text = ""
                cmbitem.Focus()
                Exit Sub
            End If

            If Trim(cmbitem.Text) = "" And closingna = False Then
                MsgBox("Select item.", MsgBoxStyle.Exclamation, "")
                cmbitem.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbitem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If proceed = True Then
                'check sa tblcoa kung ang orderfill may coa na per item
                sql = "Select * from tblcoa where ofnum='" & lblofnum.Text & Trim(txtofnum.Text) & "' and itemname='" & Trim(cmbitem.Text) & "' and status<>'3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Order fill # " & lblofnum.Text & Trim(txtofnum.Text) & ", " & Trim(cmbitem.Text) & " is already with COA.", MsgBoxStyle.Information, "")
                    cmbitem.Text = ""
                    cmbitem.Focus()
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()

                'view txtqty
                sql = "Select * from tblofitem where ofnum='" & lblofnum.Text & Trim(txtofnum.Text) & "' and itemname='" & Trim(cmbitem.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    lblitemid.Text = dr("ofitemid")
                    txtqty.Text = dr("numbags")
                End If
                dr.Dispose()
                cmd.Dispose()

                'view txtseries.Text=
                Dim tempseries As String = ""
                sql = "Select * from tbloflog where ofnum='" & lblofnum.Text & Trim(txtofnum.Text) & "' and ofitemid='" & Trim(lblitemid.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    If tempseries = "" Then
                        tempseries = dr("ticketseries")
                    Else
                        tempseries = tempseries & " " & dr("ticketseries")
                    End If
                End While
                dr.Dispose()
                cmd.Dispose()

                txtseries.Text = tempseries

                If txtseries.Text <> "" Then
                    btnok.Enabled = True
                    btncancel.Enabled = True
                    If btnok.Text = "Confirmed" Then
                        btnprint.Enabled = True
                    End If
                    btnsave.Enabled = True
                    cmbitem.BackColor = Color.MistyRose
                    txtqty.BackColor = Color.MistyRose
                    txtseries.BackColor = Color.MistyRose
                Else
                    btnok.Enabled = False
                    btncancel.Enabled = False
                    btnprint.Enabled = False
                    btnsave.Enabled = False
                End If
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

    Private Sub cmbitem_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtofnum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            If txtofnum.ReadOnly = False Then
                viewofnum()
            End If
        ElseIf Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 45 Or (Asc(e.KeyChar) >= 65 And Asc(e.KeyChar) <= 90) Or (Asc(e.KeyChar) >= 97 And Asc(e.KeyChar) <= 122) Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtofnum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnchange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchange.Click
        Try
            coacnf = False
            confirmsave.GroupBox1.Text = login.wgroup
            confirmsave.ShowDialog()
            If coacnf = True Then
                dateprod.Enabled = True
                dateprod.Focus()
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

    Private Sub dateprod_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles dateprod.Leave
        dateprod.Enabled = False
    End Sub

    Private Sub dateprod_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dateprod.ValueChanged
        dateexpiry.MinDate = dateprod.Value
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            'check if may laman lahat
            If Trim(txtcoanum.Text) <> "" And Trim(txtrefnum.Text) <> "" And Trim(txtbatch.Text) <> "" And Trim(txtqty.Text) <> "" And Trim(txtcus.Text) <> "" And Trim(txtpo.Text) <> "" And Trim(txttruck.Text) <> "" And Trim(txtseries.Text) <> "" And Trim(txtmois.Text) <> "" And Trim(txtprot.Text) <> "" And Trim(txtash.Text) <> "" And Trim(txtwet.Text) <> "" And Trim(txtwater.Text) <> "" Then
                coacnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If coacnf = True Then
                    viewcoanum()

                    'insert into tblcoa status=2
                    sql = "Insert into tblcoa (coanum, coayear, coadate, ofnum, ofitemid, itemname, refnum, batchnum, proddate, expirydate, loaddate, deldate, moisture, protein, ash, wetgluten, water, whsename, shift, datecreated, createdby, datemodified, modifiedby, status) values ('" & txtcoanum.Text & "', YEAR(GETDATE()), GetDate(), '" & lblofnum.Text & txtofnum.Text & "', '" & lblitemid.Text & "', '" & cmbitem.Text & "', '" & txtrefnum.Text & "', '" & txtbatch.Text & "', '" & dateprod.Value & "', '" & dateexpiry.Value & "', '" & dateload.Value & "', '" & datedel.Value & "', '" & txtmois.Text & "', '" & txtprot.Text & "', '" & txtash.Text & "', '" & txtwet.Text & "', '" & txtwater.Text & "', '" & login.logwhse & "', '" & login.logshift & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '2')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                    MsgBox("Successfully confirmed.", MsgBoxStyle.Information, "")

                    btnok.Text = "Confirmed"
                    btnok.Enabled = False
                    btnsave.Enabled = False
                    btncancel.Enabled = False
                    ginfo.Enabled = False
                    gparam.Enabled = False
                    grheo.Enabled = False
                    gselect.Enabled = False
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
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
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

    Private Sub txtwater_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwater.TextChanged

    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            'kelangan may itemname na select to be able to save as draft


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

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Try
            'ofnum As String, ofitemid As Integer
            rptcoa.qty = Val(txtqty.Text)
            rptcoa.ofnum = lblofnum.Text & txtofnum.Text
            rptcoa.ofitemid = Val(lblitemid.Text)
            rptcoa.coanum = Trim(txtcoanum.Text)
            rptcoa.stat = ""
            rptcoa.ShowDialog()

            'refresh list ng location
            defaultform()
            viewcoanum()
            gselect.Enabled = True
            btnprint.Enabled = False

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub COAToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COAToolStripButton1.Click
        If txtseries.Text <> "" Then
            'dapat automatic mag ssave
            btnsave.PerformClick()
        End If
        txtofnum.ReadOnly = False
        coaselect.ShowDialog()
        txtofnum.ReadOnly = False
        btnsearch.PerformClick()
    End Sub


    Private Sub OFToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OFToolStripButton1.Click
        
    End Sub

    Private Sub txtwrs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            If txtwrs.ReadOnly = False Then
                viewwrsnum()
            End If
        ElseIf Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtwrs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class