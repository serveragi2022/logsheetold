Imports System.IO
Imports System.Data.SqlClient

Public Class coanew
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
        txtcoanum.Focus()
        dateexpiry.MinDate = dateprod.Value
    End Sub

    Private Sub coanew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        '/If login.logshift = "" Or login.logwhse = "" Then
        '/MsgBox("Choose warehouse.", MsgBoxStyle.Exclamation, "")
        '/chooseshift.ShowDialog()
        '/Me.Dispose()
        '/Else
        txtcoanum.Focus()
        ginfo.Enabled = False
        gparam.Enabled = False
        grheo.Enabled = False
        btnconfirm.Enabled = False
        btncancel.Enabled = False
        btnprint.Enabled = False
        btnsave.Enabled = False
        '/End If
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
        txtitem.Text = ""
        txtmois.Text = ""
        txtprot.Text = ""
        txtash.Text = ""
        txtwet.Text = ""
        txtwater.Text = ""
        btnconfirm.Text = "Confirm"

        txtitem.BackColor = Color.MistyRose
        txtqty.BackColor = Color.MistyRose
        txtseries.BackColor = Color.MistyRose
        txtcus.BackColor = Color.FromArgb(192, 255, 192)
        txtpo.BackColor = Color.FromArgb(192, 255, 192)
        txttruck.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub txtofnum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtofnum.TextChanged

    End Sub

    Private Sub btnchange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchange.Click
        Try
            If login.depart = "QCA" Then
                coacnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If coacnf = True Then
                    dateprod.Enabled = True
                    dateprod.Focus()
                End If
            Else
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
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

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnconfirm.Click
        Try
            If login.depart = "QCA" Then
                If btnconfirm.Text = "Confirmed" Then
                    MsgBox(lblcoa.Text & txtcoanum.Text & " is already confirmed.", MsgBoxStyle.Exclamation, "")
                Else
                    'check if may laman lahat
                    If Trim(txtcoanum.Text) <> "" And Trim(txtrefnum.Text) <> "" And Trim(txtbatch.Text) <> "" And Trim(txtqty.Text) <> "" And Trim(txtcus.Text) <> "" And Trim(txtpo.Text) <> "" And Trim(txttruck.Text) <> "" And Trim(txtseries.Text) <> "" And Trim(txtmois.Text) <> "" And Trim(txtprot.Text) <> "" And Trim(txtash.Text) <> "" And Trim(txtwet.Text) <> "" And Trim(txtwater.Text) <> "" Then
                        coacnf = False
                        confirmsave.GroupBox1.Text = login.wgroup
                        confirmsave.ShowDialog()
                        If coacnf = True Then

                            'update into tblcoa status=2
                            sql = "Update tblcoa set refnum='" & txtrefnum.Text & "', batchnum='" & txtbatch.Text & "', proddate='" & dateprod.Value & "', expirydate='" & dateexpiry.Value & "', loaddate='" & dateload.Value & "', deldate='" & datedel.Value & "', moisture='" & txtmois.Text & "', protein='" & txtprot.Text & "', ash='" & txtash.Text & "', wetgluten='" & txtwet.Text & "', water='" & txtwater.Text & "', remarks='" & Trim(txtrems.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "', status='2' where coanum='" & lblcoa.Text & txtcoanum.Text & "' and branch='" & login.branch & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            'update tblorderfill with coanumber
                            sql = "Update tblorderfill set coanum='" & lblcoa.Text & txtcoanum.Text & "' where ofnum='" & lblofnum.Text & txtofnum.Text & "' and branch='" & login.branch & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            MsgBox("Successfully confirmed.", MsgBoxStyle.Information, "")

                            btnconfirm.Text = "Confirmed"
                            btnconfirm.Enabled = False
                            btnsave.Enabled = False
                            btnprint.Enabled = True
                            gselect.Enabled = False
                            gparam.Enabled = False
                            grheo.Enabled = False
                            grems.Enabled = False
                        End If
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
            If login.depart = "QCA" Then
                'kelangan may itemname na select to be able to save as draft
                coacnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If coacnf = True Then
                    'update tblcoa
                    sql = "Update tblcoa set refnum='" & txtrefnum.Text & "', batchnum='" & txtbatch.Text & "', proddate='" & dateprod.Value & "', expirydate='" & dateexpiry.Value & "', loaddate='" & dateload.Value & "', deldate='" & datedel.Value & "', moisture='" & txtmois.Text & "', protein='" & txtprot.Text & "', ash='" & txtash.Text & "', wetgluten='" & txtwet.Text & "', water='" & txtwater.Text & "', remarks='" & Trim(txtrems.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where coanum='" & lblcoa.Text & txtcoanum.Text & "' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully saved.", MsgBoxStyle.Information, "")
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

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Try
            'ofnum As String, ofitemid As Integer
            rptcoa.qty = Val(txtqty.Text)
            rptcoa.ofnum = lblofnum.Text & txtofnum.Text
            rptcoa.ofitemid = Val(lblitemid.Text)
            rptcoa.coanum = lblcoa.Text & Trim(txtcoanum.Text)
            rptcoa.stat = ""
            rptcoa.ShowDialog()

            'refresh list ng location
            defaultform()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub COAToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COAToolStripButton1.Click
        If txtseries.Text <> "" Then
            'dapat automatic mag ssave
            btnsave.PerformClick()
        End If
        txtcoanum.ReadOnly = False
        coaselect.ShowDialog()
        txtcoanum.ReadOnly = False
    End Sub

    Private Sub NewToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton1.Click
        If login.depart = "QCA" Then
            txtcoanum.ReadOnly = False
            coacreate.ShowDialog()
            txtcoanum.ReadOnly = False
            If coacreate.txtcoanum.Text <> "" Then
                ginfo.Enabled = True
                btnsearch.PerformClick()
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
                gparam.Enabled = False
                grheo.Enabled = False
                grems.Enabled = False
                gselect.Enabled = False
                btnconfirm.Enabled = False
                btnsave.Enabled = False
                btncancel.Enabled = False

                If btnconfirm.Text <> "Confirmed Order Fill" Then

                Else
                    txtcoanum.ReadOnly = False
                    txtcoanum.Focus()
                    ginfo.Enabled = False
                    gparam.Enabled = False
                    grheo.Enabled = False
                    grems.Enabled = False
                    gselect.Enabled = False
                    btnconfirm.Enabled = False
                    btnsave.Enabled = False
                    btncancel.Enabled = False
                End If
            Else
                defaultform()

                'load yung coa info
                sql = "Select * from tblcoa where coanum='" & lblcoa.Text & txtcoanum.Text & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    lblitemid.Text = dr("ofitemid")
                    txtitem.Text = dr("itemname")
                    txtrefnum.Text = dr("refnum").ToString
                    txtbatch.Text = dr("batchnum").ToString
                    If IsDBNull(dr("proddate")) = False Then
                        dateprod.Value = dr("proddate")
                    End If
                    If IsDBNull(dr("expirydate")) = False Then
                        dateexpiry.Value = dr("expirydate")
                    End If
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
                    txtrems.Text = dr("remarks").ToString
                Else
                    MsgBox("COA # cannot found.", MsgBoxStyle.Critical, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'load yung wrs info
                sql = "Select * from tblorderfill where ofnum='" & lblofnum.Text & txtofnum.Text & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    txtwrs.Text = dr("wrsnum")
                    txtcus.Text = dr("customer")
                    txtpo.Text = dr("cusref")
                    txttruck.Text = dr("platenum")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'load yung sa tblofitem
                sql = "Select tblofitem.numbags, tbloflog.ticketseries from tbloflog left outer join tblofitem on tbloflog.ofitemid=tblofitem.ofitemid where tbloflog.ofitemid='" & lblitemid.Text & "' and tblofitem.branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    txtqty.Text = dr("numbags")
                    txtseries.Text = txtseries.Text & dr("ticketseries")
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                txtcoanum.ReadOnly = True
                Panelinfo.Enabled = True
                ginfo.Enabled = True
                gparam.Enabled = True
                grheo.Enabled = True
                grems.Enabled = True
                gselect.Enabled = True
                btnconfirm.Enabled = True
                btnsave.Enabled = True
                btncancel.Enabled = True
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

    Private Sub txtcoanum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcoanum.KeyPress
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        ElseIf Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 45 Or Asc(e.KeyChar) = 65 Or Asc(e.KeyChar) = 67 Or Asc(e.KeyChar) = 79 Or Asc(e.KeyChar) = 97 Or Asc(e.KeyChar) = 99 Or Asc(e.KeyChar) = 111 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtcoanum_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcoanum.ReadOnlyChanged
        If txtcoanum.ReadOnly = True Then
            txtcoanum.BackColor = Color.LightCyan
        Else
            txtcoanum.BackColor = SystemColors.Control
        End If
    End Sub

    Private Sub txtrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        defaultform()
        Me.Close()
    End Sub
End Class