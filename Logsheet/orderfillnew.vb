Imports System.IO
Imports System.Data.SqlClient

Public Class orderfillnew
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public ofnewcnf As Boolean = False

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

    Private Sub orderfillnew_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub orderfillnew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewwhse()
        '/If login.logwhse.ToUpper = "ALL" Then
        '/cmbwhse.Text = ""
        '/cmbwhse.Enabled = True
        '/Else
        '/cmbwhse.Text = login.logwhse
        '/cmbwhse.Enabled = False
        '/End If

        defaultform()
        viewcus()
        viewofnum()
    End Sub

    Public Sub viewwhse()
        Try
            cmbwhse.Items.Clear()
            cmbwhse.Items.Add("")

            sql = "Select whsename from tblwhse where status='1' and branch='" & login.branch & "' order by whsename"
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

    Public Sub defaultform()
        cmbitem.Items.Clear()
        grditems.Rows.Clear()
        cmbtemp.Items.Clear()
        txtofnum.Text = ""
        txtwrs.Text = ""
        cmbcus.Text = ""
        txtref.Text = ""
        txtplate.Text = ""
        txtrems.Text = ""
        txtbags.Text = ""
    End Sub

    Public Sub viewofnum()
        Try
            Dim ofnum As String = "1", temp As String = "", tlognum As String = ""
            Dim prefix As String = ""

            'check kung pang ilang orderfill NA SA YEAR
            sql = "Select Count(ofid) from tblorderfill where ofyear=Year(GetDate()) and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            ofnum = cmd.ExecuteScalar + 1
            cmd.Dispose()
            conn.Close()

            If ofnum < 1000000 Then
                For vv As Integer = 1 To 6 - ofnum.Length
                    temp += "0"
                Next
                'lbltrnum.Text = Date.Now.Year & "-" & Format(Date.Now, "MM") & Format(Date.Now, "dd") & temp & trnum
            End If

            tlognum = "OF." & lblwhsecode.Text & "-" & Format(Date.Now, "yy") & "-" & temp & ofnum
            txtofnum.Text = tlognum

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

    Public Sub viewcus()
        Try
            cmbcus.Items.Clear()

            sql = "Select customer from tblcustomer where status='1' order by customer"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbcus.Items.Add(dr("customer"))
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

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            'whse should not be all
            If Trim(cmbwhse.Text) = "" Then
                Me.Cursor = Cursors.Default
                MsgBox("Warehouse should not empty.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            'check fields
            If txtofnum.Text <> "" And Trim(txtwrs.Text) <> "" And Trim(cmbcus.Text) <> "" And Trim(txtref.Text) <> "" And Trim(txtplate.Text) <> "" And grditems.Rows.Count <> 0 Then
                ofnewcnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If ofnewcnf = True Then
                    ExecuteNewOrderfill(strconn)
                End If

            ElseIf grditems.Rows.Count = 0 Then
                MsgBox("Add item first.", MsgBoxStyle.Exclamation, "")
            Else
                MsgBox("Complete the required fields.", MsgBoxStyle.Exclamation, "")
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
    Private Sub ExecuteNewOrderfill(ByVal connectionString As String)
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
                Dim ofnum As String = "1", temp As String = "", tlognum As String = ""
                Dim prefix As String = ""

                'check kung pang ilang orderfill NA SA YEAR
                sql = "Select Count(ofid) from tblorderfill where ofyear=Year(GetDate()) and branch='" & login.branch & "'"
                command.CommandText = sql
                ofnum = command.ExecuteScalar + 1

                If ofnum < 1000000 Then
                    For vv As Integer = 1 To 6 - ofnum.Length
                        temp += "0"
                    Next
                End If

                tlognum = "OF." & lblwhsecode.Text & "-" & Format(Date.Now, "yy") & "-" & temp & ofnum
                txtofnum.Text = tlognum

                'check if may ganito ng ofnum then exit sub
                sql = "Select ofnum from tblorderfill where ofnum='" & txtofnum.Text & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    Exit Sub
                End If
                dr.Dispose()

                'insert into tblorderfill
                sql = "Insert into tblorderfill (ofnum, ofyear, ofdate, wrsnum, coanum, customer, cusref, platenum, driver, whsename, shift, remarks, datecreated, createdby, datemodified, modifiedby, status, branch)"
                sql = sql & " values ('" & txtofnum.Text & "', YEAR(GETDATE()), GetDate(), '" & Trim(txtwrs.Text) & "', '', '" & Trim(cmbcus.Text) & "', '" & Trim(txtref.Text) & "', '" & Trim(txtplate.Text) & "', '" & Trim(txtdriver.Text) & "', '" & Trim(cmbwhse.Text) & "', '" & login.logshift & "', '" & Trim(txtrems.Text) & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1', '" & login.branch & "')"
                command.CommandText = sql
                command.ExecuteNonQuery()

                For Each row As DataGridViewRow In grditems.Rows
                    Dim code As String = grditems.Rows(row.Index).Cells(0).Value
                    Dim bags As Integer = Val(grditems.Rows(row.Index).Cells(1).Value)
                    Dim item As String = grditems.Rows(row.Index).Cells(0).Value.ToString.ToLower

                    'insert into tblofitem
                    sql = "Insert into tblofitem (ofid, ofnum, wrsnum, itemname, numbags, coanum, qasampling, datecreated, createdby, datemodified, modifiedby, status, branch)"
                    sql = sql & " values ((Select ofid from tblorderfill where ofnum='" & txtofnum.Text & "' and branch='" & login.branch & "'),'" & txtofnum.Text & "', '" & Trim(txtwrs.Text) & "', '" & code & "', '" & bags & "', '', '0', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1', '" & login.branch & "')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                Next


                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully created.", MsgBoxStyle.Information, "")

                Dim a As String = MsgBox("Do you want to create another orderfill?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a <> vbYes Then
                    Dim s As String = txtofnum.Text
                    If Trim(cmbwhse.Text) <> "BRAN WHSE" Then
                        orderfill.txtorf.Text = s.Substring(3, s.Length - 3)
                        orderfill.defaultform()
                        orderfill.Panelbuttons.Enabled = False
                        orderfill.txtorf.ReadOnly = False
                        orderfill.btnsearch.PerformClick()
                        orderfill.MdiParent = mdiform
                        orderfill.WindowState = FormWindowState.Maximized
                        orderfill.Show()
                        Me.Close()
                    Else 'bran
                        branorderfill.txtorf.Text = s.Substring(3, s.Length - 3)
                        branorderfill.defaultform()
                        branorderfill.Panelbuttons.Enabled = False
                        branorderfill.txtorf.ReadOnly = False
                        branorderfill.btnsearch.PerformClick()
                        branorderfill.MdiParent = mdiform
                        branorderfill.WindowState = FormWindowState.Maximized
                        branorderfill.Show()
                        Me.Close()
                    End If
                End If

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

    Private Sub txtwrs_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtwrs.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnok.PerformClick()
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtwrs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwrs.TextChanged

    End Sub

    Private Sub txtref_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtref.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnok.PerformClick()
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtref_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtref.TextChanged

    End Sub

    Private Sub txtcus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnok.PerformClick()
        End If
    End Sub

    Private Sub txtplate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtplate.TextChanged
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789"
        Dim theText As String = txtplate.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtplate.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtplate.Text.Length - 1
            Letter = txtplate.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtplate.Text = theText
        txtplate.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub txtrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtrems_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrems.TextChanged

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub viewitems()
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

            cmbitem.DropDownWidth = 300

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

    Private Sub cmbitem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbitem.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/btnadd.PerformClick()
        End If
    End Sub

    Private Sub cmbitem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitem.Leave
        Try
            If Not cmbitem.Items.Contains(Trim(cmbitem.Text.ToUpper)) = True And Trim(cmbitem.Text.ToUpper) <> "" Then
                MsgBox("Invalid Item name.", MsgBoxStyle.Critical, "")
                cmbitem.Text = ""
                cmbitem.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbitem_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbitem.MouseClick
        
    End Sub

    Private Sub cmbitem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbitem.SelectedIndexChanged

    End Sub

    Private Sub txtbags_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbags.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnadd.PerformClick()
        ElseIf (Asc(e.KeyChar) >= 47 And Asc(e.KeyChar) <= 57) Or Asc(e.KeyChar) = 8 Then

        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtbags_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbags.TextChanged

    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If Trim(cmbitem.Text) <> "" And Trim(txtbags.Text) <> "" Then
                If Val(Trim(txtbags.Text)) = 0 Then
                    MsgBox("Number of bags must not be equal to zero.", MsgBoxStyle.Exclamation, "")
                    txtbags.Text = ""
                    Exit Sub
                End If

                Dim meron As Boolean = False
                For Each item As Object In cmbtemp.Items
                    If item = Trim(cmbitem.Text) Then
                        meron = True
                        Exit For
                    End If
                Next

                If meron = True Then
                    MsgBox(Trim(cmbitem.Text) & " is already added.", MsgBoxStyle.Exclamation, "")
                ElseIf meron = False Then
                    'add in grditem
                    grditems.Rows.Add(Trim(cmbitem.Text), Trim(txtbags.Text))
                    cmbtemp.Items.Add(Trim(cmbitem.Text))
                    txtbags.Text = ""
                End If
                cmbitem.Text = ""
            Else
                MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
            End If

            cmbitem.Focus()

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

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        For Each row As DataGridViewRow In grditems.SelectedRows
            Dim cmbindex As Integer = row.Index
            grditems.Rows.Remove(row)
            cmbitem.Items.RemoveAt(cmbindex)
        Next
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        cmbitem.Text = ""
        txtbags.Text = ""
        btnupdate.Text = "Update"
        GroupBox1.Enabled = True
        btnremove.Enabled = True
        btnadd.Enabled = True
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            If btnupdate.Text = "Update" Then
                If grditems.Rows.Count = 0 Then
                    MsgBox("No item selected.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                grditems.Enabled = False
                cmbitem.Text = grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value
                txtbags.Text = grditems.Rows(grditems.CurrentRow.Index).Cells(1).Value

                btnupdate.Text = "Save"
                btnremove.Enabled = False
                btnadd.Enabled = False
                GroupBox1.Enabled = False
            Else
                If Trim(cmbitem.Text) <> "" And Trim(txtbags.Text) <> "" Then
                    grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value = cmbitem.Text
                    grditems.Rows(grditems.CurrentRow.Index).Cells(1).Value = Val(Trim(txtbags.Text))

                    btnupdate.Text = "Update"
                    btnremove.Enabled = True
                    btnadd.Enabled = True
                    GroupBox1.Enabled = True
                    btncancel.PerformClick()
                Else
                    MsgBox("Complete the fields")
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbcus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbcus.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbcus_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcus.Leave
        Try
            If Trim(cmbcus.Text.ToUpper) <> "" Then
                sql = "Select customer from tblcustomer where customer='" & Trim(cmbcus.Text.ToUpper) & "' and status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    cmbcus.Text = dr("customer")
                Else
                    MsgBox("Invalid customer name.", MsgBoxStyle.Critical, "")
                    cmbcus.Text = ""
                    cmbcus.Focus()
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbcus_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbcus.MouseClick
        Dim senderComboBox = DirectCast(sender, ComboBox)
        Dim width As Integer = senderComboBox.DropDownWidth
        Dim g As Graphics = senderComboBox.CreateGraphics()
        Dim font As Font = senderComboBox.Font

        Dim vertScrollBarWidth As Integer = If((senderComboBox.Items.Count > senderComboBox.MaxDropDownItems), SystemInformation.VerticalScrollBarWidth, 0)

        Dim newWidth As Integer
        For Each s As String In DirectCast(sender, ComboBox).Items
            newWidth = CInt(g.MeasureString(s, font).Width) + vertScrollBarWidth
            If width < newWidth Then
                width = newWidth
            End If
        Next

        senderComboBox.DropDownWidth = width
    End Sub

    Private Sub cmbcus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcus.SelectedIndexChanged
       
    End Sub

    Private Sub cmbwhse_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbwhse.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbwhse_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbwhse.Leave
        'check if valid whsename
        If Trim(cmbwhse.Text) <> "" Then
            sql = "Select * from tblwhse where whsename='" & Trim(cmbwhse.Text) & "' and branch='" & login.branch & "' and status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                cmbwhse.Text = dr("whsename")
                lblwhsecode.Text = dr("whsecode")
            Else
                MsgBox("Invalid warehouse name.", MsgBoxStyle.Exclamation, "")
                lblwhsecode.Text = ""
                cmbwhse.Text = ""
                cmbwhse.Focus()
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            viewofnum()
        End If
    End Sub

    Private Sub cmbwhse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbwhse.SelectedIndexChanged

    End Sub

    Private Sub txtdriver_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdriver.TextChanged
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789"
        Dim theText As String = txtdriver.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtdriver.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtdriver.Text.Length - 1
            Letter = txtdriver.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtdriver.Text = theText
        txtdriver.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub cmbwhse_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbwhse.SelectedValueChanged
        If Trim(cmbwhse.Text) <> "" Then
            GroupBox2.Enabled = True
        Else
            GroupBox2.Enabled = False
        End If
        viewitems()
    End Sub
End Class
