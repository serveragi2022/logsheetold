Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports Excel = Microsoft.Office.Interop.Excel

Public Class items
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim updatebool As Boolean = False
    Dim iid As Integer = 0
    Public firmitems As Boolean
    Dim clickbtn As String = ""

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

    Private Sub manageitems_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'btnview.PerformClick()
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub manageitems_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        
    End Sub

    Private Sub manageitems_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close Manage Items Form?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            mdiform.Show()
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub manageitems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor

            frmload()
            loadcat()
            cmbcat.Focus()
            btnview.PerformClick()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub cmbcat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcat.SelectedIndexChanged
        Try
            If cmbcat.SelectedItem <> "" And updatebool = False Then
                reload()
                loaditem()
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub loadcat()
        Try
            Me.Cursor = Cursors.WaitCursor
            cmbcat.Items.Clear()
            sql = "Select * from tblcat where status='1' order by category"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbcat.Items.Add(dr("category").ToString)
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

    Public Sub loaditem()
        cmbname.Items.Clear()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim chk As Boolean = False
            If chkhide.Checked = True Then
                sql = "Select * from tblitems where discontinued='1' and category='" & cmbcat.SelectedItem.ToString & "' order by itemname"
            Else
                sql = "Select * from tblitems where discontinued='0' and category='" & cmbcat.SelectedItem.ToString & "' order by itemname"
            End If
             connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                chk = True
                cmbname.Items.Add(dr("itemname").ToString)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If chk = True Then
                cmbname.Enabled = True
            Else
                reload()
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

    Private Sub cmbname_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbname.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            If cmbcat.SelectedItem <> "" And updatebool = False Then
                'Panel1.Enabled = True
                
                sql = "Select * from tblitems where category='" & cmbcat.SelectedItem & "' and itemname='" & cmbname.SelectedItem & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    txtcode.Enabled = True
                    txtdes.Enabled = True
                    txtprice.Enabled = True
                    txtstatus.Enabled = True
                    cmbtype.Enabled = True
                    'filename = dr(0).ToString
                    txtcode.Text = dr("itemcode").ToString
                    txtdes.Text = dr("description").ToString
                    txtprice.Text = Val(dr("price")).ToString("n2")
                    cmbtype.SelectedItem = dr("tickettype").ToString
                    If dr("status") = 1 Then
                        txtstatus.Text = "Available"
                    Else
                        txtstatus.Text = "Not Available"
                    End If

                Else
                    txtcode.Enabled = False
                    txtdes.Enabled = False
                    txtprice.Enabled = False
                    txtstatus.Enabled = False
                    cmbtype.Enabled = False
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                txtcode.Enabled = True
                txtdes.Enabled = True
                txtprice.Enabled = True
                txtstatus.Enabled = True
                cmbtype.Enabled = True
                cmbname.Enabled = True
                txtcode.ReadOnly = True
                txtdes.ReadOnly = True
                txtprice.ReadOnly = True
                txtstatus.ReadOnly = True


                Dim meon As Boolean = False
                For Each row As DataGridViewRow In grditems.Rows
                    If grditems.Rows(row.Index).Cells(2).Value = cmbname.SelectedItem Then

                        grditems.ClearSelection()
                        grditems.CurrentCell = grditems.Rows(row.Index).Cells(2)
                        grditems.Rows(row.Index).Selected = True

                        meon = True
                    End If
                Next
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

    Public Sub frmload()
        Try
            '/Dim dgvcCheckBox As New DataGridViewCheckBoxColumn()
            '/dgvcCheckBox.HeaderText = "Select"
            '/dgvcCheckBox.Width = 40
            '/grditems.Columns.Add(dgvcCheckBox)
            grditems.Columns(8).ReadOnly = False

            grditems.Columns(0).Visible = False
            'grditems.Columns(7).Visible = False
            grditems.Columns(1).Width = 100
            grditems.Columns(1).MinimumWidth = 100
            grditems.Columns(2).Width = 200
            grditems.Columns(2).MinimumWidth = 200
            grditems.Columns(3).Width = 120
            grditems.Columns(3).MinimumWidth = 20
            grditems.Columns(4).Width = 100
            grditems.Columns(4).MinimumWidth = 10
            grditems.Columns(5).Width = 130
            grditems.Columns(5).MinimumWidth = 30
            grditems.Columns(7).Width = 130
            grditems.Columns(7).MinimumWidth = 30
            grditems.Columns(8).Width = 100
            grditems.Columns(8).MinimumWidth = 10

            grditems.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            grditems.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            grditems.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub reload()
        Try
            Me.Cursor = Cursors.WaitCursor
            cmbname.Enabled = False
            cmbtype.Enabled = False
            txtcode.Enabled = False
            txtdes.Enabled = False
            txtprice.Enabled = False
            txtstatus.Enabled = False

            cmbname.Visible = True
            cmbname.Text = ""
            cmbname.SelectedItem = ""
            txtname.Visible = False
            txtname.Text = ""
            txtcode.Text = ""
            txtdes.Text = ""
            txtprice.Text = ""
            txtstatus.Text = ""
            lblid.Text = ""
            lbls.Visible = True
            cmbtype.SelectedItem = ""

            btnupdate.Text = "&Update Item"
            btnadditem.Text = "&Add Item"
            btnadditem.Enabled = True
            btnupdate.Enabled = True

            paneladd.Visible = False
            updatebool = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnadditem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadditem.Click
        Try
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" And login.wgroup <> "Agi Admin Staff" And login.wgroup <> "Supervisor" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor

            'check if complete fields
            If btnadditem.Text = "&Add Item" Then
                paneladd.Visible = True
                paneladd.Location = New System.Drawing.Point(6, 20)

                acmbcat.Focus()

                acmbcat.Items.Clear()
                sql = "Select * from tblcat where status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    acmbcat.Items.Add(dr("category").ToString)
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                btnupdate.Enabled = False
                btnadditem.Text = "&Add"
                btncancel.Enabled = True
                lbls.Visible = False
            Else
                'passcode here
                'if true
                'try catch then sql=insert

                If acmbcat.SelectedItem <> "" And Trim(atxtcode.Text) <> "" And Trim(atxtname.Text) <> "" And Trim(atxtprice.Text) <> "" And Trim(acmbtype.SelectedItem) <> "" Then
                    Try
                        Dim a As String = MsgBox("Are you sure you want to add item?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Add Item")
                        If a = vbYes Then

                            sql = "Insert into tblitems (category, itemcode, itemname, description, price, tickettype, datecreated, createdby, datemodified, modifiedby, status, discontinued) values('" & acmbcat.SelectedItem & "','" & Trim(atxtcode.Text) & "','" & Trim(atxtname.Text) & "','" & Trim(atxtdes.Text) & "','" & Trim(atxtprice.Text) & "','" & acmbtype.SelectedItem & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1','0')"
                            connect()
                            cmd = New SqlCommand(sql, conn) 'New OleDbCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            'select itemid
                            sql = "Select Top 1 * from tblitems order by itemid DESC"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            dr = cmd.ExecuteReader
                            If dr.Read Then
                                iid = dr("itemid")
                            End If
                            cmd.Dispose()
                            dr.Dispose()
                            conn.Close()

                            'sql = "Insert into tblinventory (itemid, itemcode, datecreated, createdby, datemodified, modifiedby, status) values('" & iid & "','" & Trim(atxtcode.Text) & "','" & (Format(Date.Now, ("MM-dd-yyyy"))) & "','Administrator','" & (Format(Date.Now, ("MM-dd-yyyy"))) & "','Administrator','0')"
                            'cmd = New SqlCommand(sql, conn) 'New OleDbCommand(sql, conn)
                            'cmd.ExecuteNonQuery()
                            'cmd.Dispose()
                            Me.Cursor = Cursors.Default
                            MsgBox("Successfully Added", MsgBoxStyle.Information, "")
                            btnview.PerformClick()
                            btncancel.Enabled = False
                            atxtcode.Text = ""
                            atxtdes.Text = ""
                            atxtname.Text = ""
                            atxtprice.Text = ""
                            acmbtype.SelectedItem = ""
                            'reload()
                        Else
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        MsgBox(ex.ToString, MsgBoxStyle.Information)
                    Finally
                        disconnect()
                    End Try
                Else
                    MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                reload()
            End If
            'else
            'msgbox complete the fields
            'end if
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
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" And login.wgroup <> "Agi Admin Staff" And login.wgroup <> "Supervisor" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            If btnupdate.Text = "&Update Item" Then
                lbls.Visible = False
                If (grditems.SelectedCells.Count = 1 Or grditems.SelectedRows.Count = 1) Then
                Else
                    MsgBox("Select one only", MsgBoxStyle.Exclamation, "")
                    updatebool = False
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                Dim checkCell As DataGridViewCheckBoxCell = CType(grditems.Rows(grditems.CurrentRow.Index).Cells(8), DataGridViewCheckBoxCell)
                If chkhide.Checked = True Then
                    MsgBox("Cannot update discontinued item.", MsgBoxStyle.Exclamation, "")
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                updatebool = True

                lblcode.Text = grditems.Item(1, grditems.CurrentRow.Index).Value
                lblname.Text = grditems.Item(2, grditems.CurrentRow.Index).Value
                cmbname.Visible = False
                txtcode.Enabled = True
                txtdes.Enabled = True
                txtprice.Enabled = True
                txtname.Visible = True
                txtcode.ReadOnly = False
                txtdes.ReadOnly = False
                txtprice.ReadOnly = False
                txtname.ReadOnly = False
                cmbtype.Enabled = True
                lblid.Text = grditems.Item(0, grditems.CurrentRow.Index).Value
                txtcode.Text = grditems.Item(1, grditems.CurrentRow.Index).Value
                txtname.Text = grditems.Item(2, grditems.CurrentRow.Index).Value
                txtdes.Text = grditems.Item(3, grditems.CurrentRow.Index).Value
                txtprice.Text = grditems.Item(4, grditems.CurrentRow.Index).Value
                cmbcat.SelectedItem = StrConv(grditems.Item(5, grditems.CurrentRow.Index).Value.ToString, vbProperCase)
                txtstatus.Text = grditems.Item(7, grditems.CurrentRow.Index).Value
                cmbtype.SelectedItem = grditems.Item(6, grditems.CurrentRow.Index).Value
                btnupdate.Text = "&Save Item"
                btnadditem.Enabled = False
                btncancel.Enabled = True

            ElseIf btnupdate.Text = "&Save Item" Then
                'check if complete
                If cmbcat.SelectedItem <> "" And Trim(txtcode.Text) <> "" And Trim(txtname.Text) <> "" And Trim(txtprice.Text) <> "" Then
                    Dim a As String = MsgBox("Are you sure you want to save the item?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
                    If a = vbYes Then
                        'passcode here
                        'if true
                        'try catch then sql=update
                        firmitems = False
                        confirm.GroupBox1.Text = login.wgroup
                        confirm.ShowDialog()
                        If firmitems = True Then
                            sql = "Update tblitems set category='" & cmbcat.SelectedItem & "', itemcode='" & Trim(txtcode.Text) & "', itemname='" & Trim(txtname.Text) & "', description='" & Trim(txtdes.Text) & "', price='" & Trim(txtprice.Text) & "', tickettype='" & cmbtype.SelectedItem & "', datemodified=GetDate(), modifiedby='" & login.user & "' where itemid='" & lblid.Text & "' "
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            sql = "Update tblofitem set itemname='" & Trim(txtname.Text) & "' where itemname='" & lblname.Text & "' "
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            sql = "Update tbllogitem set itemname='" & Trim(txtname.Text) & "' where itemname='" & lblname.Text & "' "
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            sql = "Update tblcoa set itemname='" & Trim(txtname.Text) & "' where itemname='" & lblname.Text & "' "
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            MsgBox("Successfully Saved.", MsgBoxStyle.Information, "")
                        End If

                        btnview.PerformClick()

                    End If
                Else
                    Me.Cursor = Cursors.Default
                    MsgBox("Complete the required fields.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                reload()
                btnupdate.Text = "&Update Item"
                btnadditem.Enabled = True
                btncancel.Enabled = False
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

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Try
            reload()
            updatebool = False

            paneladd.Visible = False
            btnadditem.Text = "&Add Item"
            btncancel.Enabled = False
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub acmbcat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If acmbcat.SelectedItem <> "" Then
            atxtcode.Enabled = True
            atxtdes.Enabled = True
            atxtname.Enabled = True
            atxtprice.Enabled = True
        End If
    End Sub

    Private Sub atxtcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnadditem.PerformClick()
        End If
    End Sub

    Private Sub atxtcode_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        'CHECK IF EXIST NA
        Try
            If Trim(atxtcode.Text) <> "" Then
                sql = "Select * from tblitems where itemcode='" & Trim(atxtcode.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Item Code '" & Trim(atxtcode.Text) & "' is already exist", MsgBoxStyle.Information, "")
                    atxtcode.Text = ""
                End If
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

    Private Sub atxtcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789.-/()"
        Dim theText As String = atxtcode.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = atxtcode.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To atxtcode.Text.Length - 1
            Letter = atxtcode.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        atxtcode.Text = theText
        atxtcode.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub atxtname_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnadditem.PerformClick()
        End If
    End Sub

    Private Sub atxtname_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        'CHECK IF EXIST NA
        Try
            If Trim(atxtname.Text) <> "" Then
                sql = "Select * from tblitems where itemname='" & Trim(atxtname.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Item Name '" & Trim(atxtname.Text) & "' is already exist", MsgBoxStyle.Information, "")
                    atxtname.Text = ""
                End If
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

    Private Sub atxtname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789.-/()"
        Dim theText As String = atxtname.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = atxtname.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To atxtname.Text.Length - 1
            Letter = atxtname.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        atxtname.Text = theText
        atxtname.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub atxtdes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789.-/()"
        Dim theText As String = atxtdes.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = atxtdes.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To atxtdes.Text.Length - 1
            Letter = atxtdes.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        atxtdes.Text = theText
        atxtdes.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub atxtprice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnadditem.PerformClick()
        End If

        If Asc(e.KeyChar) = 46 And Trim(atxtprice.Text) <> "" And atxtprice.Text.Contains(".") = False Then

        ElseIf Asc(e.KeyChar) = 46 And Trim(atxtprice.Text) <> "" And atxtprice.Text.Contains(".") = True Then
            e.Handled = True
        End If
    End Sub

    Private Sub atxtprice_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim pr As Double = Val(atxtprice.Text)
        atxtprice.Text = pr.ToString("n2")
    End Sub

    Private Sub atxtprice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim charactersDisallowed As String = "0123456789."
        Dim theText As String = atxtprice.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = atxtprice.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To atxtprice.Text.Length - 1
            Letter = atxtprice.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        atxtprice.Text = theText
        atxtprice.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub txtprice_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtprice.Leave
        Dim pr As Double = Val(txtprice.Text)
        txtprice.Text = pr.ToString("n2")
        If btnadditem.Enabled = True Then
            btnadditem.Focus()
        Else
            btnupdate.Focus()
        End If
    End Sub

    Private Sub grditems_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grditems.CellContentClick
        Try
            'check kung may chineck nga sa grditems then ska lng sya pde i enabled

            If grditems.CurrentCell.ColumnIndex = 8 Then
                Dim checkCell As DataGridViewCheckBoxCell = CType(grditems.Rows(grditems.CurrentRow.Index).Cells(8), DataGridViewCheckBoxCell)
                Button1.PerformClick()
                If checkCell.Value = False And grditems.Rows(grditems.CurrentRow.Index).Cells(7).Value = "Not Available" Then
                    'check if gamit sya sa ibang tbl ng database
                    If chkhide.Checked = False Then
                        '/sql = "Select * from tblgroupdisc where (itemname='" & grditems.Rows(grditems.CurrentRow.Index).Cells(2).Value & "' or basedname='" & grditems.Rows(grditems.CurrentRow.Index).Cells(2).Value & "') and status='1'"
                        '/connect()
                        '/cmd = New SqlCommand(sql, conn)
                        '/dr = cmd.ExecuteReader
                        '/If dr.Read Then
                        '/MsgBox("Cannot discontinue item. Item is use in senior/pwd discount for group meals.", MsgBoxStyle.Exclamation, "")
                        '/Me.Cursor = Cursors.Default
                        '/Exit Sub
                        '/Else
                        '/checkCell.Value = True
                        '/End If
                        '/dr.Dispose()
                        '/cmd.Dispose()
                    Else
                        'check first ung category 
                        sql = "Select * from tblcat where category='" & grditems.Rows(grditems.CurrentRow.Index).Cells(5).Value & "' and status='0'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            MsgBox("Cannot continue item. Category is deactivated.", MsgBoxStyle.Exclamation, "")
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If

                        checkCell.Value = True
                    End If

                ElseIf checkCell.Value = False And grditems.Rows(grditems.CurrentRow.Index).Cells(7).Value <> "Not Available" Then
                    MsgBox("Cannot discontinue items with available status.", MsgBoxStyle.Exclamation, "")
                    checkCell.Value = False
                Else
                    checkCell.Value = False
                End If
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub grditems_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grditems.CellValueChanged
        Try
            Exit Sub
            If grditems.Columns(e.ColumnIndex).HeaderText = "Discontinued" And grditems.RowCount <> 0 And grditems.CurrentCell.ColumnIndex = 8 Then
                Dim checkCell As DataGridViewCheckBoxCell = CType(grditems.Rows(e.RowIndex).Cells(8), DataGridViewCheckBoxCell)
                Button1.PerformClick()
                If checkCell.Value = True Then
                    'MsgBox("true")
                    'update tblitems discontinue status
                    sql = "Update tblitems set discontinued='1' where itemcode='" & grditems.Rows(grditems.CurrentRow.Index).Cells(1).Value.ToString & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn) 'New OleDbCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                Else
                    'MsgBox("false")
                    sql = "Update tblitems set discontinued='0' where itemcode='" & grditems.Rows(grditems.CurrentRow.Index).Cells(1).Value.ToString & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn) 'New OleDbCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                End If
                grditems.Invalidate()
            End If

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MsgBox(ex.tostring, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub txtcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcode.KeyPress
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnupdate.PerformClick()
        End If
    End Sub

    Private Sub txtcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789.-/()"
        Dim theText As String = txtcode.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtcode.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtcode.Text.Length - 1
            Letter = txtcode.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtcode.Text = theText
        txtcode.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub txtname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtname.TextChanged
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789.-/()"
        Dim theText As String = txtname.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtname.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtname.Text.Length - 1
            Letter = txtname.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtname.Text = theText
        txtname.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub txtdes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdes.TextChanged
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789.-/()"
        Dim theText As String = txtdes.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtdes.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtdes.Text.Length - 1
            Letter = txtdes.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtdes.Text = theText
        txtdes.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub txtprice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprice.TextChanged
        Dim charactersDisallowed As String = "0123456789."
        Dim theText As String = txtprice.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = txtprice.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To txtprice.Text.Length - 1
            Letter = txtprice.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        txtprice.Text = theText
        txtprice.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub btnview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnview.Click
        reload()
        loadcat()
        grditems.Rows.Clear()
        frmload()
        btnselect.Text = "Check All"

        Try
            clickbtn = "ALL"
            Me.Cursor = Cursors.WaitCursor

            Dim i As Integer = 0
            If chkhide.Checked = True Then
                sql = "Select * from tblitems where discontinued='1'"
            Else
                sql = "Select * from tblitems where discontinued='0'"
            End If

            If cmbview.SelectedItem = "" And cmbview.Items.Count <> 0 Then
                sql = sql & " order by category, tickettype, itemname"
            ElseIf cmbview.SelectedItem <> "All" And cmbview.Items.Count <> 0 Then
                sql = sql & " and category='" & cmbview.SelectedItem & "' order by category, tickettype, itemname"
            Else
                sql = sql & " order by category, tickettype, itemname"
                cmbview.Items.Clear()
            End If

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                grditems.Rows.Add()
                grditems.Item(0, i).Value = dr("itemid")
                grditems.Item(1, i).Value = dr("itemcode")
                grditems.Item(2, i).Value = dr("itemname")
                grditems.Item(3, i).Value = dr("description")
                grditems.Item(4, i).Value = dr("price")
                grditems.Columns(4).DefaultCellStyle.Format = "n2"
                grditems.Item(5, i).Value = dr("category")
                grditems.Item(6, i).Value = dr("tickettype")

                If dr("status") = 1 Then
                    grditems.Item(7, i).Value = "Available"
                Else
                    grditems.Item(7, i).Value = "Not Available"
                End If

                Dim checkCell As DataGridViewCheckBoxCell = CType(grditems.Rows(i).Cells(8), DataGridViewCheckBoxCell)
                checkCell.Value = False

                i += 1
            End While
            cmd.Dispose()
            dr.Dispose()
            conn.Close()

            sql = "Select * from tblcat order by category"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If Not cmbview.Items.Contains(StrConv(dr("category"), VbStrConv.ProperCase)) Then
                    cmbview.Items.Add(dr("category"))
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            '//inv()

            If i <> 0 Then
                btnupdate.Enabled = True
            Else
                btnupdate.Enabled = False
            End If

            If btnadditem.Text = "&Add Item" Then
                btncancel.Enabled = False
            Else
                btncancel.Enabled = True
            End If

            If cmbview.Items.Count <> 0 And Not cmbview.Items.Contains("All") Then
                cmbview.Items.Add("All")
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

    Public Sub inv()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim invsumnum As String = ""
            sql = "Select TOP 1 * from tblinvsum order by invsumid DESC"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                invsumnum = dr("invnum")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            For Each row As DataGridViewRow In grditems.Rows
                Dim stat As Integer = 0
                sql = "Select TOP 1 * from tblinvitems where itemcode='" & grditems.Rows(row.Index).Cells(1).Value & "' and invnum='" & invsumnum & "' order by invid DESC"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("endbal") <> 0 Then
                        grditems.Rows(row.Index).Cells(7).Value = "Available"
                        stat = 1
                    Else
                        grditems.Rows(row.Index).Cells(7).Value = "Not Available"
                        stat = 0
                    End If
                End If
                cmd.Dispose()
                dr.Dispose()
                conn.Close()

                sql = "Update tblitems set status='" & stat & "' where itemcode='" & grditems.Rows(row.Index).Cells(1).Value & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()
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

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Me.Cursor = Cursors.WaitCursor
        '/itemsprintprev.Close()
        '/itemsprint.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub chkhide_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkhide.CheckedChanged
        btnselect.Text = "Check All"
        If chkhide.Checked = True Then
            btndiscon.Text = "Continue"
            btndiscon.Enabled = True
        Else
            btndiscon.Text = "Discontinue"
        End If
        btnview.PerformClick()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        '/bundle.ShowDialog()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            If login.depart = "All" Or login.depart = "Admin Dispatching" Then
                '/MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                '/Exit Sub
                importitems.WindowState = FormWindowState.Maximized
                importitems.ShowDialog()
            Else
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
                Exit Sub
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

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        If login.wgroup <> "Administrator" And login.wgroup <> "Manager" And login.wgroup <> "Agi Admin Staff" And login.wgroup <> "Supervisor" Then
            MsgBox("Access denied!", MsgBoxStyle.Critical, "")
            Exit Sub
        End If

        Dim ChechboxColumnIndex As Integer = 8  '<-- putt the right index here
        If btnselect.Text = "Check All" Then
            cmbgroup.Items.Clear()
            '/sql = "Select * from tblgroupdisc where status='1'"
            '/connect()
            '/cmd = New SqlCommand(sql, conn)
            '/dr = cmd.ExecuteReader
            '/While dr.Read
            '/cmbgroup.Items.Add(dr("itemname"))
            '/If Not cmbgroup.Items.Contains(dr("basedname")) Then
            '/cmbgroup.Items.Add(dr("basedname"))
            '/End If
            '/End While
            '/dr.Dispose()
            '/cmd.Dispose()

            cmbcategory.Items.Clear()
            sql = "Select * from tblcat where status='0'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbcategory.Items.Add(dr("category"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            For i As Integer = 0 To grditems.RowCount - 1
                If CBool(grditems.Rows(i).Cells(ChechboxColumnIndex).Value) = False And grditems.Rows(i).Cells(7).Value = "Not Available" Then
                    'check if gamit sya sa ibang tbl ng database
                    If Not cmbgroup.Items.Contains(grditems.Rows(i).Cells(2).Value) Then
                        grditems.Rows(i).Cells(ChechboxColumnIndex).Value = True
                    Else
                        Me.Cursor = Cursors.Default
                    End If

                    If cmbcategory.Items.Contains(grditems.Rows(i).Cells(5).Value) Then
                        grditems.Rows(i).Cells(ChechboxColumnIndex).Value = False
                    End If
                End If
            Next
            btnselect.Text = "Uncheck All"
        Else
            For i As Integer = 0 To grditems.RowCount - 1
                If CBool(grditems.Rows(i).Cells(ChechboxColumnIndex).Value) = True Then
                    grditems.Rows(i).Cells(ChechboxColumnIndex).Value = False
                End If
            Next
            btnselect.Text = "Check All"
        End If
    End Sub

    Private Sub btndiscon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndiscon.Click
        Try
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" And login.wgroup <> "Agi Admin Staff" And login.wgroup <> "Supervisor" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            btnselect.Text = "Check All"
            Dim meron As Boolean = False
            For Each row In grditems.Rows
                Dim checkCell As DataGridViewCheckBoxCell = CType(grditems.Rows(row.index).Cells(8), DataGridViewCheckBoxCell)
                Button1.PerformClick()
                If checkCell.Value = True Then
                    meron = True
                End If
            Next

            If meron = False Then
                MsgBox("Select items first.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            If btndiscon.Text = "Discontinue" Then
                Dim a As String = MsgBox("Are you sure you want to discontinue items?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbYes Then
                    firmitems = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If firmitems = True Then

                        For Each row In grditems.Rows
                            Dim checkCell As DataGridViewCheckBoxCell = CType(grditems.Rows(row.index).Cells(8), DataGridViewCheckBoxCell)
                            Button1.PerformClick()
                            If checkCell.Value = True And grditems.Rows(row.index).Cells(7).Value = "Not Available" Then
                                sql = "Update tblitems set discontinued='1' where itemcode='" & grditems.Rows(row.index).Cells(1).Value.ToString & "'"
                                connect()
                                cmd = New SqlCommand(sql, conn) 'New OleDbCommand(sql, conn)
                                cmd.ExecuteNonQuery()
                                cmd.Dispose()
                                conn.Close()
                            End If
                        Next
                    End If
                    firmitems = False
                End If
            Else
                'check category if active

                Dim a As String = MsgBox("Are you sure you want to continue items?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbYes Then
                    firmitems = False
                    confirm.GroupBox1.Text = login.wgroup
                    confirm.ShowDialog()
                    If firmitems = True Then

                        For Each row In grditems.Rows
                            Dim checkCell As DataGridViewCheckBoxCell = CType(grditems.Rows(row.index).Cells(8), DataGridViewCheckBoxCell)
                            Button1.PerformClick()
                            If checkCell.Value = True And grditems.Rows(row.index).Cells(7).Value = "Not Available" Then
                                sql = "Update tblitems set discontinued='0' where itemcode='" & grditems.Rows(row.index).Cells(1).Value.ToString & "'"
                                connect()
                                cmd = New SqlCommand(sql, conn) 'New OleDbCommand(sql, conn)
                                cmd.ExecuteNonQuery()
                                cmd.Dispose()
                                conn.Close()
                            End If
                        Next
                    End If
                    firmitems = False
                End If
            End If
            btnview.PerformClick()

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

    Private Sub cmbview_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbview.SelectedIndexChanged
        Try
            btnview.PerformClick()

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

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim objExcel As New Excel.Application
            Dim bkWorkBook As Excel.Workbook
            Dim shWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            Dim i As Integer
            Dim j As Integer


            Dim daterange As String = Format(Date.Now, "MMMM dd, yyyy")
            Dim sfilename As String = "AGI " & clickbtn & " Items as of " & daterange & ".xls"

            objExcel = New Excel.Application
            bkWorkBook = objExcel.Workbooks.Add
            shWorkSheet = CType(bkWorkBook.ActiveSheet, Excel.Worksheet)

            With shWorkSheet
                .Range("A1", misValue).EntireRow.Font.Bold = True
                .Range("A1:F1").EntireRow.WrapText = True
                .Range("A1:F" & grditems.RowCount + 1).HorizontalAlignment = -4108
                .Range("A1:F" & grditems.RowCount + 1).VerticalAlignment = -4108
                .Range("A1:F" & grditems.RowCount + 1).Font.Size = 10
                'Set Clipboard Copy Mode     
                grditems.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
                grditems.SelectAll()
                grditems.RowHeadersVisible = False

                'Get the content from Grid for Clipboard     
                'Dim str As String = TryCast(grditems.GetClipboardContent().GetData(DataFormats.UnicodeText), String)
                Dim str As String = grditems.GetClipboardContent().GetData(DataFormats.UnicodeText)

                'Set the content to Clipboard     
                Clipboard.SetText(str, TextDataFormat.UnicodeText) 'TextDataFormat.UnicodeText)

                'Identify and select the range of cells in Excel to paste the clipboard data.     
                .Range("A1:F1", misValue).Select()

                'WIDTH
                .Range("A1:A" & grditems.RowCount + 1).ColumnWidth = 30

                .Range("B1:B" & grditems.RowCount + 1).ColumnWidth = 25
                .Range("C1:C" & grditems.RowCount + 1).ColumnWidth = 30
                .Range("D1:D" & grditems.RowCount + 1).ColumnWidth = 13
                .Range("E1:E" & grditems.RowCount + 1).ColumnWidth = 18

                'Paste the clipboard data     
                .Paste()
                Clipboard.Clear()

            End With

            'format alignment
            'shWorkSheet.Range("D2", "D" & grditems.RowCount + 1).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
            For i = 0 To grditems.RowCount - 1
                'shWorkSheet.Cells(i + 2, 1) = grd.Rows(i).Cells(1).Value
                ' shWorkSheet.Range("A1").EntireRow.NumberFormat = "yyyy/MM/dd"
            Next

            'lagyan ng title na red door kit tska ung date na sakop ng report
            shWorkSheet.Range("A1").EntireRow.Insert()
            shWorkSheet.Range("A2").EntireRow.Insert()
            shWorkSheet.Range("A3").EntireRow.Insert()
            shWorkSheet.Cells(1, 1) = "AGI"
            shWorkSheet.Cells(2, 1) = clickbtn & " Items as of " & daterange
            shWorkSheet.Cells(1, 1).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
            shWorkSheet.Range("A4").EntireRow.WrapText = True

            Me.Cursor = Cursors.Default

            objExcel.Visible = False
            'objExcel.Application.DisplayAlerts = False

            Dim password As String = "AB123"
            'objExcel.ActiveWorkbook.SaveAs(Application.StartupPath & "sample.xls", FileFormat:=51, )
            '/bkWorkBook.SaveAs(Filename:=Application.StartupPath & "\template\" & sfilename, FileFormat:=51, Password:=password, CreateBackup:=False)
            bkWorkBook.SaveAs(Filename:=Application.StartupPath & "\template\" & sfilename, FileFormat:=51, CreateBackup:=False)

            bkWorkBook.Close(True, misValue, misValue)
            objExcel.Quit()

            'objExcel = Nothing

            releaseObject(bkWorkBook)
            releaseObject(shWorkSheet)
            releaseObject(objExcel)

            MessageBox.Show("Data Successfully Exported") ' & sfilename)
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As System.Runtime.InteropServices.COMException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
            MessageBox.Show("Exception Occured while releasing object " + ex.ToString())
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub grditems_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grditems.SelectionChanged
        If grditems.Rows(grditems.CurrentRow.Index).Cells(7).Value = "Available" Then
            btnavail.Text = "Not Available"
        Else
            btnavail.Text = "Available"
        End If
    End Sub

    Private Sub btnavail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnavail.Click
        Try
            If grditems.Rows.Count <> 0 Then
                If grditems.SelectedCells.Count = 1 Or grditems.SelectedRows.Count = 1 Then
                    If btnavail.Text = "Available" Then
                        'check if discontinued='0'" else cannot available if it is discontinued item
                        Dim discon As Boolean = False
                        sql = "Select * from tblitems where itemid='" & grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            If dr("discontinued") = 1 Then
                                discon = True
                                MsgBox("Cannot update item into an available status.", MsgBoxStyle.Exclamation, "")
                            ElseIf dr("discontinued") = 0 Then
                                discon = False
                            End If
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()

                        If discon = False Then
                            firmitems = False
                            confirm.GroupBox1.Text = login.wgroup
                            confirm.ShowDialog()
                            If firmitems = True Then
                                'update status
                                sql = "Update tblitems set status='1' where itemid='" & grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value & "'"
                                connect()
                                cmd = New SqlCommand(sql, conn)
                                cmd.ExecuteNonQuery()
                                cmd.Dispose()

                                MsgBox("Successfully available.", MsgBoxStyle.Information, "")
                                btnview.PerformClick()
                            End If
                        End If
                    Else
                        'check if discontinued='0'" else cannot available if it is discontinued item
                        Dim discon As Boolean = False
                        sql = "Select * from tblitems where itemid='" & grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            If dr("discontinued") = 1 Then
                                discon = True
                                MsgBox("Cannot update item into a not available status.", MsgBoxStyle.Exclamation, "")
                            ElseIf dr("discontinued") = 0 Then
                                discon = False
                            End If
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()

                        If discon = False Then
                            firmitems = False
                            confirm.GroupBox1.Text = login.wgroup
                            confirm.ShowDialog()
                            If firmitems = True Then
                                'update status
                                sql = "Update tblitems set status='0' where itemid='" & grditems.Rows(grditems.CurrentRow.Index).Cells(0).Value & "'"
                                connect()
                                cmd = New SqlCommand(sql, conn)
                                cmd.ExecuteNonQuery()
                                cmd.Dispose()

                                MsgBox("Successfully not available.", MsgBoxStyle.Information, "")
                                btnview.PerformClick()
                            End If
                        End If
                    End If
                Else
                    MsgBox("Select only one.", MsgBoxStyle.Exclamation, "")
                End If
            End If


        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As System.Runtime.InteropServices.COMException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub
End Class