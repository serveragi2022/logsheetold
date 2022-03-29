Imports System.IO
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Text
Imports System.Globalization

Public Class mainmenucopycopyedit
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim sql As String
    Dim conn As SqlConnection
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand

    Dim dt As Integer = 0
    Dim lasttext As String
    Dim iftxt As Integer = 0
    Dim lastdgv As String
    Public activeTextBox As TextBox, imgname As String
    Public clickimg As PictureBox, voidd As Boolean = False
    Dim culture As CultureInfo = Nothing
    Dim tempamt As Double
    Public counters As Boolean = False

    Dim based As Double = 0, yesbased As Boolean = False, ibabawas As Double = 0

    Dim path As String, ColAdd As Boolean = False, refnum As String = ""

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

    Private Sub mainmenucopy_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub mainmenucopy_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If trans.edittrans = False Then
            moduless.Show()
        End If
    End Sub

    Private Sub mainmenucopy_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If grdorders.RowCount <> 0 Then
            Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
            If a = vbYes Then
                If trans.edittrans = False Then
                    moduless.Show()
                End If
                Me.Dispose()
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub mainmenucopy_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Asc(e.KeyCode) = Windows.Forms.Keys.Alt Then
            e.Handled = True
        End If
    End Sub

    Private Sub mainmenucopy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ColAdd = True

            datedel.MinDate = Date.Now

            path = (Microsoft.VisualBasic.Left(Application.StartupPath, Len(Application.StartupPath)))

            cmbcus.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cmbcus.DropDownStyle = ComboBoxStyle.DropDown
            cmbcus.AutoCompleteSource = AutoCompleteSource.ListItems

            cmbtype.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cmbtype.DropDownStyle = ComboBoxStyle.DropDown
            cmbtype.AutoCompleteSource = AutoCompleteSource.ListItems

            cmbpick.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cmbpick.DropDownStyle = ComboBoxStyle.DropDown
            cmbpick.AutoCompleteSource = AutoCompleteSource.ListItems

            grdorders.Columns(2).ReadOnly = True
            grdorders.Columns(3).ReadOnly = True
            grdorders.Columns(4).ReadOnly = True
            'grdorders.Columns(5).ReadOnly = True

            Dim dgvcCheckBox As New DataGridViewCheckBoxColumn()
            dgvcCheckBox.HeaderText = "Free"
            dgvcCheckBox.Width = 40
            grdorders.Columns.Add(dgvcCheckBox)
            grdorders.Columns(6).Visible = False

            grdorders.Columns.Add("code", "Item code")
            grdorders.Columns(7).Visible = False

            grdorders.Columns.Add("cat", "Category")
            grdorders.Columns(8).Visible = False

            grdorders.Rows.Add()

            Me.TableLayoutPanel1.SetRowSpan(Panel2, 2)
            Me.TableLayoutPanel1.SetRowSpan(Panel14, 2)

            datedel.CustomFormat = "yyyy/MM/dd"
            'grd.Columns(0).ReadOnly = True
            'grd.Columns(2).ReadOnly = True
            'grd.Columns(4).ReadOnly = True
            'grd.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'grd.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'grd.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'grd.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            whse()
            view()
            viewcat()
            customer()
            loadtransnum()
            ' defaultload()

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

    Public Sub viewcat()
        Try
            Exit Sub
            'update status of items
            Dim btncat(200) As Button
            Dim wid As Int32
            Dim ctr As Integer = 0, y As Integer, mody As Integer, row As Integer

            sql = "Select * from tblorcat where status='1' order by category"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Me.Cursor = Cursors.WaitCursor
                ctr += 1
                btncat(ctr) = New Button()
                btncat(ctr).Width = 95
                btncat(ctr).Height = 55
                btncat(ctr).Text = dr(1).ToString
                btncat(ctr).Tag = dr(0)
                btncat(ctr).Font = New Drawing.Font("Arial", 9.5)
                btncat(ctr).Font = New Font(btncat(ctr).Font, FontStyle.Bold)

                btncat(ctr).BackColor = Color.FromArgb(255, 224, 192)

                mody = ctr Mod 7
                row = ctr / 7

                If mody = 1 Then
                    y = (row * 55) + (1 * row)
                    wid = 0
                End If

                btncat(ctr).SetBounds(wid, y, 95, 55)
                wid += 98

                AddHandler btncat(ctr).Click, AddressOf imgClicked
            End While

            dr.Dispose()
            cmd.Dispose()

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

    Public Sub clearpanel()
        Try
            Panel14.Visible = False
            Panel14.Controls.Clear()
            Panel14.Visible = True
            enablelahat()
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub enablelahat()
        Try
            'For Each btn As Button In Panel5.Controls
            'If btn.Text <> clickbtn.Text Then
            'btn.Enabled = True
            'btn.BackColor = Color.FromArgb(255, 224, 192)
            'Else
            'btn.Enabled = False
            'btn.BackColor = Color.FromArgb(255, 192, 128)
            'End If
            'Next
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub viewitems()
        Try
            'update status of items
            Dim btn(200) As Button
            Dim wid As Int32
            Dim ctr As Integer = 0, y As Integer, mody As Integer, row As Integer

            sql = "Select * from tbloritems where category='" & clickimg.Text & "' and discontinued='0' order by itemname"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Me.Cursor = Cursors.WaitCursor
                ctr += 1
                btn(ctr) = New Button()
                'btn(ctr).Width = 126
                'btn(ctr).Height = 90
                btn(ctr).Text = dr("itemname").ToString
                btn(ctr).Tag = dr("itemcode")
                btn(ctr).TextAlign = ContentAlignment.BottomCenter
                btn(ctr).Font = New Drawing.Font("Arial", 11)
                btn(ctr).Font = New Font(btn(ctr).Font, FontStyle.Bold)
                btn(ctr).TextImageRelation = TextImageRelation.ImageAboveText

                btn(ctr).BackColor = Color.FromArgb(192, 255, 192)

                mody = ctr Mod 5
                row = ctr / 5

                If mody = 1 Then
                    y = (row * 89) + (8 * row)
                    wid = 0
                End If

                btn(ctr).SetBounds(wid, y, 130, 89)
                wid += 136

                If dr("status") <> 1 Then
                    btn(ctr).Text = dr(3).ToString & " - N/A"
                    btn(ctr).ForeColor = Color.Red
                Else
                    btn(ctr).ForeColor = Color.Black
                End If

                Panel14.Controls.Add(btn(ctr))
                AddHandler btn(ctr).Click, AddressOf ItemClicked
            End While
            dr.Dispose()
            cmd.Dispose()

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

    Public Sub ItemClicked(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Microsoft.VisualBasic.Right(sender.text.ToString, 6) = " - N/A" Then
                MsgBox("The Item is Not Available", MsgBoxStyle.Exclamation, "")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            'For Each row As DataGridViewRow In grd.Rows
            'If grd.Rows(row.Index).Cells(0).Value = sender.text Then
            'Exit Sub
            'End If
            'Next

            grdorders.Rows.Add()
            grdorders.Item(0, grdorders.RowCount - 1).Value = sender.text
            grdorders.Item(7, grdorders.RowCount - 1).Value = sender.tag
            grdorders.Item(5, grdorders.RowCount - 1).Value = ""

            Try
                sql = "Select * from tbloritems where itemcode='" & sender.tag & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdorders.Item(2, grdorders.RowCount - 1).Value = dr("price")
                    grdorders.Item(8, grdorders.RowCount - 1).Value = dr("category")
                End If
                dr.Dispose()
                cmd.Dispose()
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.Message, MsgBoxStyle.Information)
            Finally
                disconnect()
            End Try

            Me.grdorders.Columns(2).DefaultCellStyle.Format = "n2"
            Me.grdorders.Columns(3).DefaultCellStyle.Format = "n2"

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

    Private Sub chkkey_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkkey.CheckedChanged
        If chkkey.Checked = False Then
            panelfalse()
        End If
    End Sub

    Public Sub panelfalse()

        Panel14.Enabled = True

    End Sub

    Private Sub grd_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            grdorders.BeginEdit(True)
            Dim checkCell As DataGridViewCheckBoxCell = CType(grdorders.Rows(e.RowIndex).Cells(6), DataGridViewCheckBoxCell)
            If grdorders.CurrentCell.ColumnIndex = 6 Then
                Button1.PerformClick()
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grd_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            cellendedit()
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub cellendedit()
        Try

            If grdorders.CurrentCell.ColumnIndex = 1 Then

                If grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value IsNot Nothing Then
                    MsgBox("Input must be a non zero number", MsgBoxStyle.Exclamation, "")
                    grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value = lastdgv
                    amount()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

            ElseIf grdorders.CurrentCell.ColumnIndex = 3 Then
                If IsNumeric((grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value.ToString)) Then
                    If grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value.ToString IsNot Nothing And Val(grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value) = 0 Then
                        'MessageBox.Show("Input must be a non zero number")
                        'grd.Rows(grd.CurrentRow.Index).Cells(3).Value = lastdgv
                    ElseIf grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value.ToString IsNot Nothing And (grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value) > 100 Then
                        MsgBox("Input must not higher than 100%", MsgBoxStyle.Exclamation, "")
                        'MessageBox.Show("Input must not higher than 100%")
                        grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value = lastdgv
                    ElseIf grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value.ToString IsNot Nothing And (grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value) < 1 Then
                        MsgBox("Invalid input", MsgBoxStyle.Exclamation, "")
                        'MessageBox.Show("Input must be a positive number")
                        grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value = lastdgv
                    End If
                Else
                    If grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value.ToString IsNot Nothing Then
                        'MessageBox.Show("Input must be a non zero number")
                        grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value = lastdgv
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                End If

            ElseIf grdorders.CurrentCell.ColumnIndex = 5 Then
                MsgBox("sfskf")
                If grdorders.Rows(grdorders.CurrentRow.Index).Cells(5).Value.ToString.Contains("'") Then
                    Dim strtemp As String = grdorders.Rows(grdorders.CurrentRow.Index).Cells(5).Value
                    grdorders.Rows(grdorders.CurrentRow.Index).Cells(5).Value = strtemp.Replace("'", String.Empty)
                End If
            End If

            amount()
            Dim checkCell As DataGridViewCheckBoxCell = CType(grdorders.Rows(grdorders.CurrentRow.Index).Cells(6), DataGridViewCheckBoxCell)
            If checkCell.Value = True Then
                grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value = "0.00"
                grdorders.Rows(grdorders.CurrentRow.Index).Cells(4).Value = "0.00"
            End If

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

    Public Sub amount()
        Try
            Dim checkCell As DataGridViewCheckBoxCell = CType(grdorders.Rows(grdorders.CurrentRow.Index).Cells(6), DataGridViewCheckBoxCell)
            If grdorders.RowCount <> 0 And checkCell.Value = False Then
                If (grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value IsNot Nothing) Or (grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value IsNot Nothing) Then
                    Dim bd As Double = CDbl(Val(grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value))
                    Dim db As Double = CDbl(Val(grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value))
                    Dim isnm As Boolean = IsNumeric(bd)
                    Dim isnm1 As Boolean = IsNumeric(db)
                    If isnm = True And isnm1 = True Then
                        grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value = Val(grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value)
                        grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value = Val(grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value)
                        Dim q As Double = CDbl(grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value)
                        Dim p As Double
                        sql = "Select * from tbloritems where itemname='" & grdorders.Rows(grdorders.CurrentRow.Index).Cells(0).Value.ToString & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            p = Val(dr("price"))
                        End If
                        '////Dim p As Double = CDbl(grd.Rows(grd.CurrentRow.Index).Cells(2).Value)
                        Dim d As Double = (q * p) * (CDbl(grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value / 100))
                        Dim amt As Double = (q * p) - d

                        grdorders.Rows(grdorders.CurrentRow.Index).Cells(2).Value = p
                        grdorders.Rows(grdorders.CurrentRow.Index).Cells(4).Value = amt
                        grdorders.Columns(1).DefaultCellStyle.Format = "n3"
                        grdorders.Columns(3).DefaultCellStyle.Format = "n2"
                        grdorders.Columns(4).DefaultCellStyle.Format = "n2"
                    End If
                End If
            ElseIf grdorders.RowCount <> 0 And checkCell.Value = True Then
                grdorders.Rows(grdorders.CurrentRow.Index).Cells(2).Value = "0.00"
                grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value = "0.00"
                grdorders.Rows(grdorders.CurrentRow.Index).Cells(4).Value = "0.00"
            End If

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information) '////////
        End Try
    End Sub

    Private Sub grd_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        grdorders.Focus()
    End Sub

    Private Sub grd_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            If grdorders.Columns(e.ColumnIndex).HeaderText = "Free" And grdorders.RowCount <> 0 Then
                Dim checkCell As DataGridViewCheckBoxCell = CType(grdorders.Rows(e.RowIndex).Cells(6), DataGridViewCheckBoxCell)
                If checkCell.Value = True Then
                    'MsgBox("true")
                    voidd = False
                    confirm.ShowDialog()
                    If voidd = True Then
                        grdorders.Rows(grdorders.CurrentRow.Index).Cells(2).Value = "0.00"
                        grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value = "0.00"
                        grdorders.Rows(grdorders.CurrentRow.Index).Cells(4).Value = "0.00"
                    Else
                        checkCell.Value = False
                    End If
                    voidd = False
                Else
                    'MsgBox("false")
                    If grdorders.RowCount <> 0 Then
                        If (grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value.ToString IsNot Nothing) And (grdorders.Rows(grdorders.CurrentRow.Index).Cells(2).Value.ToString IsNot Nothing) And IsNumeric(grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value) = True And IsNumeric(grdorders.Rows(grdorders.CurrentRow.Index).Cells(2).Value) = True Then
                            grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value = Val(grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value)
                            grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value = Val(grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value)
                            Dim q As Double = CDbl(grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value)
                            Dim p As Double = CDbl(grdorders.Rows(grdorders.CurrentRow.Index).Cells(2).Value)
                            Dim d As Double = CDbl(grdorders.Rows(grdorders.CurrentRow.Index).Cells(3).Value)
                            Dim amt As Double = (q * p) - d
                            grdorders.Rows(grdorders.CurrentRow.Index).Cells(4).Value = amt
                            grdorders.Columns(1).DefaultCellStyle.Format = "n2"
                            grdorders.Columns(3).DefaultCellStyle.Format = "n2"
                            grdorders.Columns(4).DefaultCellStyle.Format = "n2"
                        End If
                    End If
                End If
                grdorders.Invalidate()
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MessageBox.Show(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub grd_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            If grdorders.RowCount <> 0 Then
                Dim cellcolumn As Integer = grdorders.CurrentCell.ColumnIndex
                If (grdorders.CurrentCell.ColumnIndex = 1 Or grdorders.CurrentCell.ColumnIndex = 3) And chkkey.Checked = True Then
                    iftxt = 0
                    If Not (grdorders.Rows(grdorders.CurrentRow.Index).Cells(cellcolumn).Value) Is Nothing Then
                        lastdgv = grdorders.Rows(grdorders.CurrentRow.Index).Cells(cellcolumn).Value.ToString
                    Else
                        grdorders.Rows(grdorders.CurrentRow.Index).Cells(cellcolumn).Value = ""
                        lastdgv = grdorders.Rows(grdorders.CurrentRow.Index).Cells(cellcolumn).Value.ToString
                    End If
                    grdorders.Rows(grdorders.CurrentRow.Index).Cells(cellcolumn).Value = ""
                    panelfalse()
                ElseIf (grdorders.CurrentCell.ColumnIndex = 5) And chkkey.Checked = True Then
                    iftxt = 0
                    If Not (grdorders.Rows(grdorders.CurrentRow.Index).Cells(cellcolumn).Value) Is Nothing Then
                        lastdgv = grdorders.Rows(grdorders.CurrentRow.Index).Cells(cellcolumn).Value.ToString
                    Else
                        grdorders.Rows(grdorders.CurrentRow.Index).Cells(cellcolumn).Value = ""
                        lastdgv = grdorders.Rows(grdorders.CurrentRow.Index).Cells(cellcolumn).Value.ToString
                    End If
                    grdorders.Rows(grdorders.CurrentRow.Index).Cells(cellcolumn).Value = ""
                    panelfalse()
                End If
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            If cmbtype.SelectedIndex = 0 Then
                MsgBox("Invalid transaction type.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            Else
                If cmbtype.Text.ToString.Contains("PICKUP") And Trim(cmbpick.Text) = "" Then
                    MsgBox("Input pick up Warehouse.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
            End If

            If txtso.Enabled = True And Val(txtso.Text) = 0 And txtso.Text.ToString.ToLower.Contains("follow") = False Then
                MsgBox("Input " & lblso.Text & ".", MsgBoxStyle.Exclamation, "")
                Exit Sub
            ElseIf txtpo.Enabled = True And Val(txtpo.Text) = 0 And txtpo.Text.ToString.ToLower.Contains("follow") = False Then
                MsgBox("Input " & lblpo.Text & ".", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            If Trim(cmbcus.Text) = "" Then
                MsgBox("Input recipient.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            'tanggalin ung mga rows na nde kumpleto then
            'check if grd has orders
            Dim meronpa As Boolean = True
            If grdorders.RowCount <> 0 Then
                If grdorders.RowCount = 1 Then
                    If (grdorders.Rows(0).Cells(1).Value Is Nothing Or Val(grdorders.Rows(0).Cells(1).Value) = 0) Then
                        MsgBox("No orders entered. Please check entries.", MsgBoxStyle.Exclamation, "")
                        grdorders.Rows.Clear()
                        grdorders.Rows.Add()
                        Exit Sub
                    End If
                End If

                For irow As Integer = 0 To grdorders.RowCount - 1
                    Try
                        While (grdorders.Rows(irow).Cells(1).Value Is Nothing Or Val(grdorders.Rows(irow).Cells(1).Value) = 0)
                            'MsgBox(irow)
                            grdorders.Rows.Remove(grdorders.Rows(irow))
                            grdorders.Refresh()
                            If grdorders.Rows.Count = 0 Then
                                MsgBox("No orders entered. Please check entries.", MsgBoxStyle.Exclamation, "")
                                grdorders.Rows.Clear()
                                grdorders.Rows.Add()
                                Exit Sub
                            End If
                        End While

                        While (Trim(grdorders.Rows(irow).Cells(0).Value.ToString) = "")
                            'MsgBox(irow)
                            grdorders.Rows.Remove(grdorders.Rows(irow))
                            grdorders.Refresh()
                            If grdorders.Rows.Count = 0 Then
                                MsgBox("No orders entered. Please check entries.", MsgBoxStyle.Exclamation, "")
                                grdorders.Rows.Clear()
                                grdorders.Rows.Add()
                                Exit Sub
                            End If
                        End While
                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        'MsgBox(ex.Message, MsgBoxStyle.Information)
                        If grdorders.RowCount = 0 Then
                            Exit Sub
                        End If
                    End Try
                Next
            Else
                MsgBox("No orders entered. Please check entries.", MsgBoxStyle.Exclamation, "")
                grdorders.Rows.Clear()
                grdorders.Rows.Add()
                Exit Sub
            End If


            'check if kung stock transfer and customer is contains whse
            If cmbtype.SelectedItem.ToString.Contains("STOCK TRANSFER") And Not cmbcus.SelectedItem.ToString.Contains(" WHSE") Then
                MsgBox("If recipient is not warehouse, transaction type should not be stock transfer.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            ElseIf Not cmbtype.SelectedItem.ToString.Contains("STOCK TRANSFER") And cmbcus.SelectedItem.ToString.Contains(" WHSE") Then
                MsgBox("If recipient is warehouse, transaction type should be stock transfer.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If


            amount()
            If Trim(cmbcus.Text) <> "" Then
                If btnattachpo.Enabled = True And imgbox2.Image Is Nothing Then
                    MsgBox("Attach " & GroupBox2.Text, MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                If btnattachso.Enabled = True And imgbox1.Image Is Nothing Then
                    MsgBox("Attach " & GroupBox1.Text, MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                Dim a As String = MsgBox("Confirmed Order?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbYes Then
                    If btnok.Text = "OK" Then
                        savetransaction()
                    Else
                        saveedittrans()
                        Me.Dispose()
                        Exit Sub
                    End If
                    grdorders.Rows.Clear()
                    grdorders.Rows.Add()

                End If
            Else
                MsgBox("Complete the required fields.", MsgBoxStyle.Exclamation, "")
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub savetransaction()
        Try
            Me.Cursor = Cursors.WaitCursor
            'first check entries dun sa btnok
            'transaction type
            Dim transtype As String = ""
            If cmbtype.SelectedIndex <> 0 Then
                If cmbtype.Text.ToString.Contains("PICKUP") And Trim(cmbpick.Text) = "" Then
                    'MsgBox("Pick up Warehouse.", MsgBoxStyle.Exclamation, "")
                    'Exit Sub
                Else
                    If Trim(cmbpick.Text) = "" Then
                        transtype = cmbtype.SelectedItem
                    Else
                        transtype = cmbtype.SelectedItem & " FRM " & Trim(cmbpick.Text)
                    End If
                End If
            End If


            If txtso.Enabled = True And txtpo.Enabled = True Then
                refnum = lblso.Text & txtso.Text & " / " & lblpo.Text & txtpo.Text
            ElseIf txtso.Enabled = True And txtpo.Enabled = False Then
                refnum = lblso.Text & txtso.Text
            ElseIf txtso.Enabled = False And txtpo.Enabled = True Then
                refnum = lblpo.Text & txtpo.Text
            ElseIf txtso.Enabled = False And txtpo.Enabled = False Then
                refnum = "0000"
            End If


            Dim itrnum As String = ""
            If refnum.ToString.Contains("ITR#") Then
                itrnum = txtpo.Text
            End If

            loadtransnum()

            If (cmbtype.SelectedItem = "JPSC STOCK TRANSFER PICKUP" Or cmbtype.SelectedItem = "TRUCKING STOCK TRANSFER PICKUP") And (Trim(cmbpick.Text) <> "AGI" And Not cmbpick.Text.Contains("PIER")) And (Trim(cmbcus.Text) = "C3 MANILA WHSE" Or Trim(cmbcus.Text) = "CALAMBA WHSE" Or Trim(cmbcus.Text) = "PAGBILAO WHSE" Or Trim(cmbcus.Text) = "MILAOR WHSE" Or Trim(cmbcus.Text) = "NAGA WHSE") Then
                'MsgBox("additional")
                sql = "Insert into tblortrans (transnum, refnum, deliverydate, bookedby, customer, transtype, notes, cancel, whsename, datecreated, createdby, datemodified, modifiedby, status, arnum, rdrnum, drnum, dnnum, itrnum, manager) values ('" & lbltrnum.Text & "', '" & refnum & "', '" & Format(datedel.Value, "yyyy/MM/dd") & "', '" & login.cashier & "', '" & cmbcus.Text & "', '" & transtype & "', '" & Trim(txtnotes.Text) & "', '0', '" & login.whse & "', GetDate(), '" & login.cashier & "', GetDate(), '" & login.cashier & "', '1', 'N/A', '', 'N/A', 'N/A', '', '')"

            ElseIf (cmbtype.SelectedItem = "JPSC STOCK TRANSFER WHSE TO WHSE") Then
                'Msgbox("IF WHSE TO WHSE")
                sql = "Insert into tblortrans (transnum, refnum, deliverydate, bookedby, customer, transtype, notes, cancel, whsename, datecreated, createdby, datemodified, modifiedby, status, arnum, rdrnum, drnum, dnnum, itrnum, manager) values ('" & lbltrnum.Text & "', '" & refnum & "', '" & Format(datedel.Value, "yyyy/MM/dd") & "', '" & login.cashier & "', '" & cmbcus.Text & "', '" & transtype & "', '" & Trim(txtnotes.Text) & "', '0', '" & login.whse & "', GetDate(), '" & login.cashier & "', GetDate(), '" & login.cashier & "', '1', 'N/A', '', 'N/A', 'N/A', '" & itrnum & "', '')"
            Else
                sql = "Insert into tblortrans (transnum, refnum, deliverydate, bookedby, customer, transtype, notes, cancel, whsename, datecreated, createdby, datemodified, modifiedby, status, arnum, rdrnum, drnum, dnnum, itrnum, manager) values ('" & lbltrnum.Text & "', '" & refnum & "', '" & Format(datedel.Value, "yyyy/MM/dd") & "', '" & login.cashier & "', '" & cmbcus.Text & "', '" & transtype & "', '" & Trim(txtnotes.Text) & "', '0', '" & login.whse & "', GetDate(), '" & login.cashier & "', GetDate(), '" & login.cashier & "', '1', '', '', '', '', '', '')"
            End If

            'save tblortrans
            connect()
            cmd = New SqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            disconnect()

            'save tblorder
            'tblorder
            connect()
            Dim dsc(grdorders.Rows.Count - 1) As String
            Dim marks As String = ""
            Dim totalfree As Double = 0

            For Each row As DataGridViewRow In grdorders.Rows
                Me.Cursor = Cursors.WaitCursor
                Dim iname As String = grdorders.Rows(row.Index).Cells(0).Value
                Dim iqty As Double = Val(grdorders.Rows(row.Index).Cells(1).Value.ToString)
                Dim iprice As Double = Val(grdorders.Rows(row.Index).Cells(2).Value.ToString)
                Dim idscnt As Double = Val(grdorders.Rows(row.Index).Cells(3).Value.ToString)
                Dim itotalprice As Double = Val(grdorders.Rows(row.Index).Cells(4).Value.ToString)
                Dim ireq As String = "" 'grdorders.Rows(row.Index).Cells(5).Value.ToString
                Dim icat As String = grdorders.Rows(row.Index).Cells(8).Value.ToString
                Dim ifree As Double = 0

                Dim dscntprice As Double = iprice - ((idscnt / 100) * iprice)

                Dim checkCell As DataGridViewCheckBoxCell = CType(grdorders.Rows(row.Index).Cells(6), DataGridViewCheckBoxCell)
                If checkCell.Value = True Then
                    'MsgBox("true")
                    sql = "Select * from tblitems where itemcode='" & grdorders.Rows(row.Index).Cells(7).Value & "'"

                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        tempamt = Val(dr(5)) * grdorders.Rows(row.Index).Cells(1).Value
                    End If
                    dr.Dispose()
                    cmd.Dispose()

                    ifree = tempamt
                    totalfree += ifree
                Else
                    'MsgBox("false")
                    ifree = 0
                End If

                Dim idisc As Integer = 0

                sql = "Insert into tblorder (transnum, category, itemname, qty, price, totalprice, dscnt, free, request, status, discprice, disctrans)values('" & lbltrnum.Text & "','" & icat & "','" & iname & "','" & iqty & "','" & iprice & "','" & itotalprice & "','" & idscnt & "','" & ifree & "','" & ireq & "','1','" & dscntprice & "','" & idisc & "')"
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Next
            disconnect()

            'save tblorimage
            If imgbox1.Image IsNot Nothing Then
                Dim ms As New MemoryStream()
                Dim filename As String = lblso.Text & Trim(txtso.Text)

                connect()
                cmd = New SqlCommand("Insert into tblorimage values(@transnum,@name,@img)", conn)
                cmd.Parameters.Add(New SqlClient.SqlParameter("transnum", lbltrnum.Text))
                cmd.Parameters.Add(New SqlClient.SqlParameter("name", filename))
                imgbox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim data As Byte() = ms.GetBuffer()
                Dim img As New SqlParameter("@img", SqlDbType.Image)
                img.Value = data
                cmd.Parameters.Add(img)
                cmd.ExecuteNonQuery()
                'MessageBox.Show("Image saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'imgbox.Image = Nothing
                imgbox1.BackColor = Color.Empty
                imgbox1.Invalidate()
                disconnect()
            End If

            If imgbox11.Image IsNot Nothing Then
                Dim ms As New MemoryStream()
                Dim filename As String = lblso.Text & Trim(txtso.Text) & "(2)"

                connect()
                cmd = New SqlCommand("Insert into tblorimage values(@transnum,@name,@img)", conn)
                cmd.Parameters.Add(New SqlClient.SqlParameter("transnum", lbltrnum.Text))
                cmd.Parameters.Add(New SqlClient.SqlParameter("name", filename))
                imgbox11.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim data As Byte() = ms.GetBuffer()
                Dim img As New SqlParameter("@img", SqlDbType.Image)
                img.Value = data
                cmd.Parameters.Add(img)
                cmd.ExecuteNonQuery()
                'MessageBox.Show("Image saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'imgbox.Image = Nothing
                imgbox11.BackColor = Color.Empty
                imgbox11.Invalidate()
                disconnect()
            End If

            If imgbox111.Image IsNot Nothing Then
                Dim ms As New MemoryStream()
                Dim filename As String = lblso.Text & Trim(txtso.Text) & "(3)"

                connect()
                cmd = New SqlCommand("Insert into tblorimage values(@transnum,@name,@img)", conn)
                cmd.Parameters.Add(New SqlClient.SqlParameter("transnum", lbltrnum.Text))
                cmd.Parameters.Add(New SqlClient.SqlParameter("name", filename))
                imgbox111.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim data As Byte() = ms.GetBuffer()
                Dim img As New SqlParameter("@img", SqlDbType.Image)
                img.Value = data
                cmd.Parameters.Add(img)
                cmd.ExecuteNonQuery()
                'MessageBox.Show("Image saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'imgbox.Image = Nothing
                imgbox111.BackColor = Color.Empty
                imgbox111.Invalidate()
                disconnect()
            End If

            If imgbox2.Image IsNot Nothing Then
                Dim ms As New MemoryStream()
                Dim filename As String = lblpo.Text & Trim(txtpo.Text)

                connect()
                cmd = New SqlCommand("Insert into tblorimage values(@transnum,@name,@img)", conn)
                cmd.Parameters.Add(New SqlClient.SqlParameter("transnum", lbltrnum.Text))
                cmd.Parameters.Add(New SqlClient.SqlParameter("name", filename))
                imgbox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim data As Byte() = ms.GetBuffer()
                Dim img As New SqlParameter("@img", SqlDbType.Image)
                img.Value = data
                cmd.Parameters.Add(img)
                cmd.ExecuteNonQuery()
                'MessageBox.Show("Image saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'imgbox.Image = Nothing
                imgbox2.BackColor = Color.Empty
                imgbox2.Invalidate()
                disconnect()
            End If

            If imgbox22.Image IsNot Nothing Then
                Dim ms As New MemoryStream()
                Dim filename As String = lblpo.Text & Trim(txtpo.Text) & "(2)"

                connect()
                cmd = New SqlCommand("Insert into tblorimage values(@transnum,@name,@img)", conn)
                cmd.Parameters.Add(New SqlClient.SqlParameter("transnum", lbltrnum.Text))
                cmd.Parameters.Add(New SqlClient.SqlParameter("name", filename))
                imgbox22.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim data As Byte() = ms.GetBuffer()
                Dim img As New SqlParameter("@img", SqlDbType.Image)
                img.Value = data
                cmd.Parameters.Add(img)
                cmd.ExecuteNonQuery()
                'MessageBox.Show("Image saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'imgbox.Image = Nothing
                imgbox22.BackColor = Color.Empty
                imgbox22.Invalidate()
                disconnect()
            End If

            If imgbox222.Image IsNot Nothing Then
                Dim ms As New MemoryStream()
                Dim filename As String = lblpo.Text & Trim(txtpo.Text) & "(3)"

                connect()
                cmd = New SqlCommand("Insert into tblorimage values(@transnum,@name,@img)", conn)
                cmd.Parameters.Add(New SqlClient.SqlParameter("transnum", lbltrnum.Text))
                cmd.Parameters.Add(New SqlClient.SqlParameter("name", filename))
                imgbox222.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim data As Byte() = ms.GetBuffer()
                Dim img As New SqlParameter("@img", SqlDbType.Image)
                img.Value = data
                cmd.Parameters.Add(img)
                cmd.ExecuteNonQuery()
                'MessageBox.Show("Image saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'imgbox.Image = Nothing
                imgbox222.BackColor = Color.Empty
                imgbox222.Invalidate()
                disconnect()
            End If

            Me.Cursor = Cursors.Default

            defaultload()

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

    Public Sub saveedittrans()
        Try
            Me.Cursor = Cursors.WaitCursor
            'first check entries dun sa btnok
            'transaction type
            Dim transtype As String = ""
            If cmbtype.SelectedIndex <> 0 Then
                If cmbtype.Text.ToString.Contains("PICKUP") And Trim(cmbpick.Text) = "" Then
                    'MsgBox("Pick up Warehouse.", MsgBoxStyle.Exclamation, "")
                    'Exit Sub
                Else
                    If Trim(cmbpick.Text) = "" Then
                        transtype = cmbtype.SelectedItem
                    Else
                        transtype = cmbtype.SelectedItem & " FRM " & Trim(cmbpick.Text)
                    End If
                End If
            End If

            If txtso.Enabled = True And txtpo.Enabled = True Then
                refnum = lblso.Text & txtso.Text & " / " & lblpo.Text & txtpo.Text
            ElseIf txtso.Enabled = True And txtpo.Enabled = False Then
                refnum = lblso.Text & txtso.Text
            ElseIf txtso.Enabled = False And txtpo.Enabled = True Then
                refnum = lblpo.Text & txtpo.Text
            ElseIf txtso.Enabled = False And txtpo.Enabled = False Then
                refnum = "0000"
            End If


            Dim itrnum As String = ""
            If refnum.ToString.Contains("ITR#") Then
                itrnum = txtpo.Text
            End If

            '/loadtransnum()
            '/sql = "Insert into tblortrans (transnum, refnum, deliverydate, bookedby, customer, transtype, notes, cancel, whsename, datecreated, createdby, datemodified, modifiedby, status, arnum, rdrnum, drnum, itrnum, dnnum, manager) values ('" & lbltrnum.Text & "', '" & refnum & "', '" & Format(datedel.Value, "yyyy/MM/dd") & "', '" & login.cashier & "', '" & cmbcus.Text & "', '" & transtype & "', '" & Trim(txtnotes.Text) & "', '0', '" & login.whse & "', GetDate(), '" & login.cashier & "', GetDate(), '" & login.cashier & "', '1', '', '', '', '', '', '')"

            'sql = "Update tblortrans set refnum='" & refnum & "', deliverydate='" & Format(datedel.Value, "yyyy/MM/dd") & "', customer='" & cmbcus.Text & "', transtype='" & transtype & "', notes='" & Trim(txtnotes.Text) & "', datemodified=GetDate(), modifiedby='" & login.cashier & "' where transnum='" & lbltrnum.Text & "'"
            'save tblortrans

            If (cmbtype.SelectedItem = "JPSC STOCK TRANSFER PICKUP" Or cmbtype.SelectedItem = "TRUCKING STOCK TRANSFER PICKUP") And (Trim(cmbcus.Text) = "C3 MANILA WHSE" Or Trim(cmbcus.Text) = "CALAMBA WHSE" Or Trim(cmbcus.Text) = "PAGBILAO WHSE" Or Trim(cmbcus.Text) = "MILAOR WHSE" Or Trim(cmbcus.Text) = "NAGA WHSE") Then
                'If (cmbtype.SelectedItem = "JPSC STOCK TRANSFER PICKUP" Or cmbtype.SelectedItem = "TRUCKING STOCK TRANSFER PICKUP") And (Trim(cmbpick.Text) <> "AGI" And Not cmbpick.Text.Contains("PIER")) And (Trim(cmbcus.Text) = "C3 MANILA WHSE" Or Trim(cmbcus.Text) = "CALAMBA WHSE" Or Trim(cmbcus.Text) = "PAGBILAO WHSE" Or Trim(cmbcus.Text) = "MILAOR WHSE" Or Trim(cmbcus.Text) = "NAGA WHSE") Then
                sql = "Update tblortrans set refnum='" & refnum & "',arnum='N/A', rdrnum='', drnum='N/A', dnnum='', itrnum='" & itrnum & "', deliverydate='" & Format(datedel.Value, "yyyy/MM/dd") & "', customer='" & cmbcus.Text & "', transtype='" & transtype & "', notes='" & Trim(txtnotes.Text) & "', datemodified=GetDate(), modifiedby='" & login.cashier & "' where transnum='" & lbltrnum.Text & "'"

            ElseIf (cmbtype.SelectedItem = "JPSC STOCK TRANSFER WHSE TO WHSE") Then
                'Msgbox("IF WHSE TO WHSE")
                sql = "Update tblortrans set refnum='" & refnum & "',arnum='N/A', rdrnum='', drnum='N/A', dnnum='N/A', itrnum='" & itrnum & "', deliverydate='" & Format(datedel.Value, "yyyy/MM/dd") & "', customer='" & cmbcus.Text & "', transtype='" & transtype & "', notes='" & Trim(txtnotes.Text) & "', datemodified=GetDate(), modifiedby='" & login.cashier & "' where transnum='" & lbltrnum.Text & "'"

            Else
                sql = "Update tblortrans set refnum='" & refnum & "',arnum='', rdrnum='', drnum='', dnnum='', itrnum='', deliverydate='" & Format(datedel.Value, "yyyy/MM/dd") & "', customer='" & cmbcus.Text & "', transtype='" & transtype & "', notes='" & Trim(txtnotes.Text) & "', datemodified=GetDate(), modifiedby='" & login.cashier & "' where transnum='" & lbltrnum.Text & "'"

            End If

            connect()
            cmd = New SqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            disconnect()

            'delete orders in tblorder
            sql = "Delete from tblorder where transnum='" & lbltrnum.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            disconnect()

            'save orders in tblorder
            connect()
            Dim dsc(grdorders.Rows.Count - 1) As String
            Dim marks As String = ""
            Dim totalfree As Double = 0

            For Each row As DataGridViewRow In grdorders.Rows
                Me.Cursor = Cursors.WaitCursor
                Dim iname As String = grdorders.Rows(row.Index).Cells(0).Value
                Dim iqty As Double = Val(grdorders.Rows(row.Index).Cells(1).Value.ToString)
                Dim iprice As Double = Val(grdorders.Rows(row.Index).Cells(2).Value.ToString)
                Dim idscnt As Double = Val(grdorders.Rows(row.Index).Cells(3).Value.ToString)
                Dim itotalprice As Double = Val(grdorders.Rows(row.Index).Cells(4).Value.ToString)
                Dim ireq As String = "" 'grdorders.Rows(row.Index).Cells(5).Value.ToString
                Dim icat As String = grdorders.Rows(row.Index).Cells(8).Value.ToString
                Dim ifree As Double = 0

                Dim dscntprice As Double = iprice - ((idscnt / 100) * iprice)

                Dim checkCell As DataGridViewCheckBoxCell = CType(grdorders.Rows(row.Index).Cells(6), DataGridViewCheckBoxCell)
                If checkCell.Value = True Then
                    'MsgBox("true")
                    sql = "Select * from tblitems where itemcode='" & grdorders.Rows(row.Index).Cells(7).Value & "'"

                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        tempamt = Val(dr(5)) * grdorders.Rows(row.Index).Cells(1).Value
                    End If
                    dr.Dispose()
                    cmd.Dispose()

                    ifree = tempamt
                    totalfree += ifree
                Else
                    'MsgBox("false")
                    ifree = 0
                End If

                Dim idisc As Integer = 0

                sql = "Insert into tblorder (transnum, category, itemname, qty, price, totalprice, dscnt, free, request, status, discprice, disctrans)values('" & lbltrnum.Text & "','" & icat & "','" & iname & "','" & iqty & "','" & iprice & "','" & itotalprice & "','" & idscnt & "','" & ifree & "','" & ireq & "','1','" & dscntprice & "','" & idisc & "')"
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Next
            disconnect()

            'delete image in tblorimage
            sql = "Delete from tblorimage where transnum='" & lbltrnum.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            disconnect()

            'save tblorimage
            If imgbox1.Image IsNot Nothing Then
                Dim ms As New MemoryStream()
                Dim filename As String = lblso.Text & Trim(txtso.Text)

                connect()
                cmd = New SqlCommand("Insert into tblorimage values(@transnum,@name,@img)", conn)
                cmd.Parameters.Add(New SqlClient.SqlParameter("transnum", lbltrnum.Text))
                cmd.Parameters.Add(New SqlClient.SqlParameter("name", filename))
                imgbox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim data As Byte() = ms.GetBuffer()
                Dim img As New SqlParameter("@img", SqlDbType.Image)
                img.Value = data
                cmd.Parameters.Add(img)
                cmd.ExecuteNonQuery()
                'MessageBox.Show("Image saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'imgbox.Image = Nothing
                imgbox1.BackColor = Color.Empty
                imgbox1.Invalidate()
                disconnect()
            End If

            If imgbox11.Image IsNot Nothing Then
                Dim ms As New MemoryStream()
                Dim filename As String = lblso.Text & Trim(txtso.Text) & "(2)"

                connect()
                cmd = New SqlCommand("Insert into tblorimage values(@transnum,@name,@img)", conn)
                cmd.Parameters.Add(New SqlClient.SqlParameter("transnum", lbltrnum.Text))
                cmd.Parameters.Add(New SqlClient.SqlParameter("name", filename))
                imgbox11.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim data As Byte() = ms.GetBuffer()
                Dim img As New SqlParameter("@img", SqlDbType.Image)
                img.Value = data
                cmd.Parameters.Add(img)
                cmd.ExecuteNonQuery()
                'MessageBox.Show("Image saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'imgbox.Image = Nothing
                imgbox11.BackColor = Color.Empty
                imgbox11.Invalidate()
                disconnect()
            End If

            If imgbox111.Image IsNot Nothing Then
                Dim ms As New MemoryStream()
                Dim filename As String = lblso.Text & Trim(txtso.Text) & "(3)"

                connect()
                cmd = New SqlCommand("Insert into tblorimage values(@transnum,@name,@img)", conn)
                cmd.Parameters.Add(New SqlClient.SqlParameter("transnum", lbltrnum.Text))
                cmd.Parameters.Add(New SqlClient.SqlParameter("name", filename))
                imgbox111.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim data As Byte() = ms.GetBuffer()
                Dim img As New SqlParameter("@img", SqlDbType.Image)
                img.Value = data
                cmd.Parameters.Add(img)
                cmd.ExecuteNonQuery()
                'MessageBox.Show("Image saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'imgbox.Image = Nothing
                imgbox111.BackColor = Color.Empty
                imgbox111.Invalidate()
                disconnect()
            End If

            If imgbox2.Image IsNot Nothing Then
                Dim ms As New MemoryStream()
                Dim filename As String = lblpo.Text & Trim(txtpo.Text)

                connect()
                cmd = New SqlCommand("Insert into tblorimage values(@transnum,@name,@img)", conn)
                cmd.Parameters.Add(New SqlClient.SqlParameter("transnum", lbltrnum.Text))
                cmd.Parameters.Add(New SqlClient.SqlParameter("name", filename))
                imgbox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim data As Byte() = ms.GetBuffer()
                Dim img As New SqlParameter("@img", SqlDbType.Image)
                img.Value = data
                cmd.Parameters.Add(img)
                cmd.ExecuteNonQuery()
                'MessageBox.Show("Image saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'imgbox.Image = Nothing
                imgbox2.BackColor = Color.Empty
                imgbox2.Invalidate()
                disconnect()
            End If

            If imgbox22.Image IsNot Nothing Then
                Dim ms As New MemoryStream()
                Dim filename As String = lblpo.Text & Trim(txtpo.Text) & "(2)"

                connect()
                cmd = New SqlCommand("Insert into tblorimage values(@transnum,@name,@img)", conn)
                cmd.Parameters.Add(New SqlClient.SqlParameter("transnum", lbltrnum.Text))
                cmd.Parameters.Add(New SqlClient.SqlParameter("name", filename))
                imgbox22.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim data As Byte() = ms.GetBuffer()
                Dim img As New SqlParameter("@img", SqlDbType.Image)
                img.Value = data
                cmd.Parameters.Add(img)
                cmd.ExecuteNonQuery()
                'MessageBox.Show("Image saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'imgbox.Image = Nothing
                imgbox22.BackColor = Color.Empty
                imgbox22.Invalidate()
                disconnect()
            End If

            If imgbox222.Image IsNot Nothing Then
                Dim ms As New MemoryStream()
                Dim filename As String = lblpo.Text & Trim(txtpo.Text) & "(3)"

                connect()
                cmd = New SqlCommand("Insert into tblorimage values(@transnum,@name,@img)", conn)
                cmd.Parameters.Add(New SqlClient.SqlParameter("transnum", lbltrnum.Text))
                cmd.Parameters.Add(New SqlClient.SqlParameter("name", filename))
                imgbox222.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim data As Byte() = ms.GetBuffer()
                Dim img As New SqlParameter("@img", SqlDbType.Image)
                img.Value = data
                cmd.Parameters.Add(img)
                cmd.ExecuteNonQuery()
                'MessageBox.Show("Image saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'imgbox.Image = Nothing
                imgbox222.BackColor = Color.Empty
                imgbox222.Invalidate()
                disconnect()
            End If

            Me.Cursor = Cursors.Default

            defaultload()

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

    Private Sub btncutoff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncutoff.Click
        Try
            If grdorders.Rows.Count <> 0 Then
                MsgBox("Cancel transaction first to continue.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            Dim a As String = MsgBox("Please confirm logout of current user.", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
            If a = vbYes Then
                confirm.ShowDialog()
                If voidd = True Then

                    'balik lahat sa umpisa
                    grdorders.Rows.Clear()

                    btncutoff.Enabled = True

                    savelogout()
                    login.Show()
                    Me.Dispose()

                End If
                voidd = False
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Try
            If btnok.Text = "OK" Then

                If grdorders.Rows.Count <> 0 Then
                    Dim a As String = MsgBox("Please confirm CANCELLATION of this transaction.", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MessageBoxDefaultButton.Button2, "")
                    If a = vbYes Then
                        defaultload()
                        '/gcform.grdgc.Rows.Clear()
                        grdorders.Rows.Clear()
                        grdorders.Rows.Add()

                        '/gcform.txtamt.Text = "100.00"
                        '/gcform.txtserial.Text = ""
                        '/gcform.lblgctotal.Text = "0.00"

                        voidd = False
                    End If
                Else
                    MsgBox("No orders entered. Please check entries.", MsgBoxStyle.Exclamation, "")
                    '/   btnok.Enabled = True
                    If grdorders.Rows.Count = 0 Then
                        grdorders.Rows.Add()
                    End If
                    defaultload()
                    Exit Sub
                End If

            Else
                Me.Dispose()
            End If

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub loadtransnum()
        Try
            If trans.edittrans = True Then
                sql = "Select * from tblortrans where transnum='" & lbltrnum.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    cmbcus.SelectedItem = dr("customer")
                    cmbtype.SelectedItem = dr("transtype")
                    txtnotes.Text = dr("notes")

                    If Format(datedel.Value, "MM/dd/yyyy") > Format(dr("deliverydate"), "MM/dd/yyyy") Then
                        datedel.MinDate = dr("deliverydate")
                        datedel.Value = dr("deliverydate")
                    End If

                    If dr("refnum").ToString.Contains("/") Then
                        If dr("refnum").ToString.Contains("SO#") Then
                            Dim inSql As String = dr("refnum").ToString
                            Dim fromStart As Integer
                            Dim firstpart As String

                            fromStart = inSql.IndexOf("/")
                            firstpart = inSql.Substring(3, fromStart - 4)
                            txtso.Text = Trim(firstpart)

                        End If

                        If dr("refnum").ToString.Contains("PO#") Then
                            Dim inSql As String = dr("refnum").ToString
                            Dim lastPart As String
                            Dim fromStart As Integer

                            fromStart = inSql.IndexOf("PO#") + 3
                            lastPart = inSql.Substring(fromStart, inSql.Length - fromStart)
                            txtpo.Text = Trim(lastPart)

                        End If
                    Else
                        If dr("refnum").ToString.Contains("SO#") Then
                            Dim inSql As String = dr("refnum").ToString
                            Dim fromStart As Integer
                            Dim firstpart As String

                            fromStart = inSql.IndexOf("#") + 1
                            firstpart = inSql.Substring(fromStart, inSql.Length - fromStart)
                            txtso.Text = Trim(firstpart)

                        Else
                            'If dr("refnum").ToString.Contains("PO#") Then
                            Dim inSql As String = dr("refnum").ToString
                            If inSql <> "" Then
                                Dim firstpart As String
                                Dim fromStart As Integer

                                fromStart = inSql.IndexOf("#") + 1
                                firstpart = inSql.Substring(fromStart, inSql.Length - fromStart)
                                lblpo.Text = inSql.Substring(0, 2)
                                txtpo.Text = Trim(firstpart)
                            End If

                        End If

                    End If

                    If dr("transtype").ToString.Contains("PICKUP") Then
                        Dim inSql As String = dr("transtype").ToString
                        Dim lastPart As String
                        Dim fromStart As Integer
                        Dim firstpart As String

                        fromStart = inSql.IndexOf("FRM ") + 4

                        firstpart = inSql.Substring(0, fromStart - 4)
                        cmbtype.SelectedItem = Trim(firstpart)

                        lastPart = inSql.Substring(fromStart, inSql.Length - fromStart)

                        cmbpick.Text = lastPart
                        cmbpick.Select()
                        txtnotes.Focus()
                    Else
                        cmbtype.SelectedItem = dr("transtype")
                        cmbtype.Select()
                        txtnotes.Focus()
                    End If

                    txtnotes.Focus()
                End If
                dr.Dispose()
                cmd.Dispose()
                disconnect()


                Dim ctr As Integer = 0
                sql = "Select * from tblorimage where transnum='" & lbltrnum.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    Me.Cursor = Cursors.WaitCursor
                    ctr += 1
                    Dim data As Byte() = DirectCast(dr("img"), Byte())
                    Dim ms As New MemoryStream(data)

                    If dr("name").ToString.Contains("SO") Then
                        If imgbox1.Image Is Nothing Then
                            imgbox1.Image = Image.FromStream(ms)
                        ElseIf imgbox11.Image Is Nothing Then
                            imgbox11.Image = Image.FromStream(ms)
                        ElseIf imgbox111.Image Is Nothing Then
                            imgbox111.Image = Image.FromStream(ms)
                        End If
                    Else
                        If imgbox2.Image Is Nothing Then
                            imgbox2.Image = Image.FromStream(ms)
                        ElseIf imgbox22.Image Is Nothing Then
                            imgbox22.Image = Image.FromStream(ms)
                        ElseIf imgbox222.Image Is Nothing Then
                            imgbox222.Image = Image.FromStream(ms)
                        End If
                    End If
                End While
                dr.Dispose()
                cmd.Dispose()
                disconnect()


                grdorders.Rows.Clear()
                sql = "Select * from tblorder where transnum='" & lbltrnum.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    grdorders.Rows.Add(dr("itemname"), dr("qty"), dr("price"), dr("dscnt"), dr("totalprice"), dr("request"), True, "", dr("category"))

                    Dim checkCell As DataGridViewCheckBoxCell = CType(grdorders.Rows(grdorders.RowCount - 1).Cells(6), DataGridViewCheckBoxCell)
                    If dr("free") <> 0 Then
                        checkCell.Value = True
                    Else
                        checkCell.Value = False
                    End If
                End While
                dr.Dispose()
                cmd.Dispose()
                disconnect()

                grdorders.Rows.Add()

                Exit Sub
            End If


            Dim trnum As String = "1", temp As String = ""
            sql = "Select Top 1 * from tblortrans order by transid DESC"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                trnum = Val(dr("transid")) + 1
            End If
            cmd.Dispose()
            dr.Dispose()
            disconnect()

            Dim prefix As String = ""
            If login.whse = "C3 Manila" Then
                prefix = "MNL"
            ElseIf login.whse = "Calamba" Then
                prefix = "CAL"
            ElseIf login.whse = "Pagbilao" Then
                prefix = "PGB"
            ElseIf login.whse = "Lucena" Then
                prefix = "LUC"
            ElseIf login.whse = "Milaor" Then
                prefix = "MIL"
            ElseIf login.whse = "Lc Office" Then
                prefix = "LCO"
            End If

            If trnum < 1000000 Then
                For vv As Integer = 1 To 6 - trnum.Length
                    temp += "0"
                Next
                'lbltrnum.Text = Date.Now.Year & "-" & Format(Date.Now, "MM") & Format(Date.Now, "dd") & temp & trnum
            End If

            lbltrnum.Text = "O." & prefix & "-" & temp & trnum

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

    Public Sub loadtransnumnew()
        Try
            If Format(Date.Now, "yyyy") <> "2017" Then
                'check if wla pang 2018 and above na year then saka gawin ung formula for first entry
                sql = "Select TOP 1 transnum from tblortrans where Not transdate like '2017%' order by transid DESC"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox(dr("transnum").ToString)
                Else
                    'first entry of transaction
                End If
                dr.Dispose()
                cmd.Dispose()
            Else
                '2017
                loadtransnum()
                MsgBox(lbltrnum.Text)
            End If
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

    Private Sub btnvoid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnvoid.Click
        Try
            If grdorders.Rows.Count <> 0 Then
                '/confirm.ShowDialog()
                '/If voidd = False Then
                '/ Exit Sub
                '/End If
                'select one of the items to void then remove in the datagridview
                'update computation
                For Each row As DataGridViewRow In grdorders.Rows
                    '/voidform.cmbitems.Items.Add(grd.Rows(row.Index).Cells(0).Value)
                Next
                '/voidform.ShowDialog()
                voidd = False
            Else
                MsgBox("No orders entered. Please check entries.", MsgBoxStyle.Exclamation, "")
                defaultload()
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub defaultload()
        Me.Cursor = Cursors.WaitCursor
        customer()
        'grdorders.Rows.Clear()
        cmbtrans.SelectedIndex = 0
        imgbox.Image = Nothing
        txtnotes.Text = ""
        cmbcus.Text = ""
        cmbtype.SelectedItem = ""
        cmbpick.Text = ""

        GroupBox2.Text = "PO"

        txtso.Text = "0000"
        imgbox1.Image = Nothing
        imgbox11.Image = Nothing
        imgbox111.Image = Nothing

        txtpo.Text = "0000"
        imgbox2.Image = Nothing
        imgbox22.Image = Nothing
        imgbox222.Image = Nothing
        datedel.Value = Date.Now


        lblso.Text = "SO#"
        lblso.Enabled = False
        txtso.Enabled = False
        lblpo.Text = "PO#"
        lblpo.Enabled = False
        txtpo.Enabled = False

        grdorders.Columns(2).ReadOnly = True
        grdorders.Columns(3).ReadOnly = True
        grdorders.Columns(4).ReadOnly = True
        'grdorders.Columns(5).ReadOnly = True

        btnattachso.Enabled = False
        btnattachpo.Enabled = False

        Panel14.Enabled = True
        TableLayoutPanel2.Enabled = True
        Panel15.Enabled = True
        chkkey.Enabled = True
        btncancel.Enabled = True
        btnvoid.Enabled = True
        btnok.Enabled = True

        loadtransnum()
        Me.Cursor = Cursors.Default
        'End If
    End Sub

    Private Sub btnreprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreprint.Click
        '/reprintor.ShowDialog()
    End Sub

    Public Sub savelogout()
        Try
            Dim logid As Integer = 0

            sql = "Select * from tbllogin where datelogin='" & Format(Date.Now, "yyyy/MM/dd") & "' and username='" & login.cashier & "' and logout=''"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                logid = dr("systemid")
            End If
            dr.Dispose()
            cmd.Dispose()
            disconnect()

            sql = "Update tbllogin set logout='" & Format(Date.Now, "HH:mm") & "' where systemid='" & logid & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            disconnect()

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

    Public Sub savebreak()
        Try
            Dim logid As Integer = 0

            sql = "Select * from tbllogin where datelogin='" & Format(Date.Now, "yyyy/MM/dd") & "' and username='" & login.cashier & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                logid = dr("systemid")
            End If
            dr.Dispose()
            cmd.Dispose()
            disconnect()

            sql = "Update tbllogin set cbreak='" & Format(Date.Now, "HH:mm") & "' where systemid='" & logid & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            disconnect()

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

    Public Sub ref()
        Try
            'For Each btn As Button In Panel5.Controls
            'If btn.Enabled = False Then
            'clickbtn = CType(btn, Button)
            'clearpanel()
            'viewitems()
            'End If
            'Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub mainmenucopy_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        defaultload()
    End Sub

    Public Sub customer()
        Try
            cmbcus.Items.Clear()

            sql = "Select * from tblcustomer where status='1' order by customer"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If Not cmbcus.Items.Contains(dr("customer")) Then
                    cmbcus.Items.Add(dr("customer"))
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            disconnect()

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

    Private Sub btnattach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnattachso.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim open As OpenFileDialog
            open = New OpenFileDialog
            open.FileName = ""
            open.Filter = "Image Formats(*.jpg;*.jpeg;*.bmp;*.gif;*.png;*.tif)|*.jpg;*.jpeg;*.bmp;*.gif;*.png;*.tif|JPEG Format(*.jpg;*.jpeg)|*.jpg;*.jpeg|BITMAP Format(*.bmp)|*.bmp|GIF Format(*.gif)|*.gif|PNG Format(*.png)|*.png"
            If open.ShowDialog = Windows.Forms.DialogResult.OK Then
                If imgbox1.Image Is Nothing Then
                    imgbox1.Image = Image.FromFile(open.FileName.ToUpper)
                    'filename = open.SafeFileName.ToString() 'Get as image name
                ElseIf imgbox11.Image Is Nothing Then
                    imgbox11.Image = Image.FromFile(open.FileName.ToUpper)
                    'filename = open.SafeFileName.ToString() 'Get as image name
                ElseIf imgbox111.Image Is Nothing Then
                    imgbox111.Image = Image.FromFile(open.FileName.ToUpper)
                    'filename = open.SafeFileName.ToString() 'Get as image name
                End If
            End If
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub imgbox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgbox1.Click
        If imgbox1.Image IsNot Nothing Then
            imgbox.Image = imgbox1.Image
            imgname = imgbox1.Name
        End If
    End Sub

    Public Sub imgClicked(ByVal sender As Object, ByVal e As EventArgs)
        Try
            clickimg = CType(sender, PictureBox)
            MsgBox(clickimg.Name.ToString)
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub txtso_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtso.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtnotes_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtnotes.KeyPress
        If Asc(e.KeyChar) <> 8 And Asc(e.KeyChar) <> 1 And Asc(e.KeyChar) <> 3 And Asc(e.KeyChar) <> 24 And Asc(e.KeyChar) <> 25 And Asc(e.KeyChar) <> 26 Then
            If Asc(e.KeyChar) = 39 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub cmbcus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbcus.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbcus_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcus.Leave
        If Trim(cmbcus.Text) <> "" Then
            sql = "Select * from tblcustomer where status='1' and customer='" & Trim(cmbcus.Text) & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                cmbcus.SelectedItem = Trim(cmbcus.Text)
            Else
                cmbcus.Text = ""
            End If
            dr.Dispose()
            cmd.Dispose()
            disconnect()
        End If
    End Sub

    Private Sub btnview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnview.Click
        trans.ShowDialog()
    End Sub

    Private Sub btnattachpo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnattachpo.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim open As OpenFileDialog
            open = New OpenFileDialog
            open.FileName = ""
            open.Filter = "Image Formats(*.jpg;*.jpeg;*.bmp;*.gif;*.png;*.tif)|*.jpg;*.jpeg;*.bmp;*.gif;*.png;*.tif|JPEG Format(*.jpg;*.jpeg)|*.jpg;*.jpeg|BITMAP Format(*.bmp)|*.bmp|GIF Format(*.gif)|*.gif|PNG Format(*.png)|*.png"
            If open.ShowDialog = Windows.Forms.DialogResult.OK Then
                If imgbox2.Image Is Nothing Then
                    imgbox2.Image = Image.FromFile(open.FileName.ToUpper)
                    'filename = open.SafeFileName.ToString() 'Get as image name
                ElseIf imgbox22.Image Is Nothing Then
                    imgbox22.Image = Image.FromFile(open.FileName.ToUpper)
                    'filename = open.SafeFileName.ToString() 'Get as image name
                ElseIf imgbox222.Image Is Nothing Then
                    imgbox222.Image = Image.FromFile(open.FileName.ToUpper)
                    'filename = open.SafeFileName.ToString() 'Get as image name
                End If
            End If
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub imgbox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgbox2.Click
        If imgbox2.Image IsNot Nothing Then
            imgbox.Image = imgbox2.Image
            imgname = imgbox2.Name
        End If
    End Sub

    Private Sub txtso_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtso.Leave
        Try
            If Trim(txtso.Text) <> "" Or Val(Trim(txtso.Text)) <> 0 Then
                sql = "Select * from tblortrans where cancel='0'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    Dim TestString As String = dr("refnum")
                    Dim TestArray() As String = TestString.ToString.Split(" / ")
                    ' TestArray holds {"apple", "", "", "", "pear", "banana", "", ""} 
                    Dim LastNonEmpty As Integer = -1
                    For i As Integer = 0 To TestArray.Length - 1
                        If TestArray(i) <> "/ " Then
                            LastNonEmpty += 1
                            TestArray(LastNonEmpty) = TestArray(i)
                        End If
                    Next
                    ReDim Preserve TestArray(LastNonEmpty)
                    ' TestArray now holds {"apple", "pear", "banana"}
                    For r As Integer = 0 To LastNonEmpty
                        If lblso.Text & Trim(txtso.Text) = TestArray(r) Then    'panu ung mga unang zero i val ba yun
                            MsgBox("SO# is already exist.", MsgBoxStyle.Exclamation, "")
                            'txtso.Text = "0000"
                            'Exit Sub
                        End If
                    Next

                End While
                dr.Dispose()
                cmd.Dispose()
                disconnect()
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

    Private Sub txtpo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpo.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtpo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpo.Leave
        Try
            If Trim(txtpo.Text) <> "" Or Val(Trim(txtpo.Text)) <> 0 Then
                sql = "Select * from tblortrans where cancel='0'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    Dim TestString As String = dr("refnum")
                    Dim TestArray() As String = TestString.ToString.Split(" / ")
                    ' TestArray holds {"apple", "", "", "", "pear", "banana", "", ""} 
                    Dim LastNonEmpty As Integer = -1
                    For i As Integer = 0 To TestArray.Length - 1
                        If TestArray(i) <> "/ " Then
                            LastNonEmpty += 1
                            TestArray(LastNonEmpty) = TestArray(i)
                        End If
                    Next
                    ReDim Preserve TestArray(LastNonEmpty)
                    ' TestArray now holds {"apple", "pear", "banana"}
                    For r As Integer = 0 To LastNonEmpty
                        If lblpo.Text & Trim(txtpo.Text) = TestArray(r) Then    'panu ung mga unang zero i val ba yun
                            MsgBox(lblpo.Text & " is already exist.", MsgBoxStyle.Exclamation, "")
                            'txtpo.Text = "0000"
                            'Exit Sub
                        End If
                    Next

                End While
                dr.Dispose()
                cmd.Dispose()
                disconnect()
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

    Private Sub txtso_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtso.TextChanged
        Try
            Dim charactersDisallowed As String = "toflwTOFLW1234567890 "
            Dim theText As String = txtso.Text
            Dim Letter As String
            Dim SelectionIndex As Integer = txtso.SelectionStart
            Dim Change As Integer

            For x As Integer = 0 To txtso.Text.Length - 1
                Letter = txtso.Text.Substring(x, 1)
                If Not charactersDisallowed.Contains(Letter) Then
                    theText = theText.Replace(Letter, String.Empty)
                    Change = 1
                End If
            Next

            txtso.Text = theText.ToUpper
            txtso.Select(SelectionIndex - Change, 0)

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

    Private Sub grdorders_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdorders.CellContentClick
        grdorders.BeginEdit(True)
    End Sub

    Private Sub grdorders_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdorders.CellEndEdit
        Try
            Dim ii As Integer = grdorders.CurrentRow.Index

            If e.ColumnIndex = 0 Then
                If grdorders.Rows(grdorders.CurrentRow.Index).Cells(0).Value IsNot Nothing Then
                    If Trim(grdorders.Rows(grdorders.CurrentRow.Index).Cells(0).Value.ToString) <> "" Then

                        If grdorders.Rows(grdorders.CurrentRow.Index).Cells(0).Value.ToString.Contains("'") Then
                            grdorders.Rows(grdorders.CurrentRow.Index).Cells(0).Value = grdorders.Rows(grdorders.CurrentRow.Index).Cells(0).Value.ToString.Replace("'", "")
                        End If

                        grdorders.Rows(grdorders.CurrentRow.Index).Cells(0).Value = grdorders.Rows(grdorders.CurrentRow.Index).Cells(0).Value.ToString.ToUpper()

                        'input yung price sa cells 3

                        sql = "Select * from tbloritems where itemname='" & grdorders.Rows(grdorders.CurrentRow.Index).Cells(0).Value.ToString & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            grdorders.Rows(grdorders.CurrentRow.Index).Cells(2).Value = dr("price")
                            grdorders.Rows(grdorders.CurrentRow.Index).Cells(8).Value = dr("category")
                            grdorders.Rows(grdorders.CurrentRow.Index).Cells(7).Value = ""
                            amount()
                        Else
                            MsgBox("Cannot found " & grdorders.Rows(grdorders.CurrentRow.Index).Cells(0).Value.ToString, MsgBoxStyle.Critical, "")
                            grdorders.Rows(grdorders.CurrentRow.Index).Cells(0).Value = Nothing
                            grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value = Nothing
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        disconnect()
                    End If
                End If
            End If

            If e.ColumnIndex = 1 Then
                If grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value.ToString IsNot Nothing And Val(grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value) = 0 Then
                    MsgBox("Input must be a non zero number", MsgBoxStyle.Exclamation, "")
                    amount()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                ElseIf grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value.ToString IsNot Nothing And Val(grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value) < 0 Then
                    MsgBox("Invalid input", MsgBoxStyle.Exclamation, "")
                    grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value = 0
                    amount()
                    Me.Cursor = Cursors.Default
                    Exit Sub
                Else
                    'computeamt()
                    'grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value = CInt(Val(grdorders.Rows(grdorders.CurrentRow.Index).Cells(1).Value))
                    amount()
                    'check if stocks are enough to purchase the quantity of orders
                End If
            End If

            If e.ColumnIndex = 5 Then
                If grdorders.Rows(grdorders.CurrentRow.Index).Cells(5).Value.ToString.Contains("'") Then
                    Dim strtemp As String = grdorders.Rows(grdorders.CurrentRow.Index).Cells(5).Value
                    grdorders.Rows(grdorders.CurrentRow.Index).Cells(5).Value = strtemp.Replace("'", String.Empty)
                End If
            End If

            If grdorders.Rows(ii).Cells(0).Value IsNot Nothing And grdorders.Rows(ii).Cells(1).Value IsNot Nothing Then
                If grdorders.RowCount = ii + 1 Then
                    grdorders.Rows.Add()
                End If
                grdorders.Rows(ii).ErrorText = ""
            Else
                grdorders.Rows(ii).ErrorText = "Complete the required fields"
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Sub view()
        Try
            fillcombo()

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub fillcombo()
        Try
            connect()
            'For rowIndex As Integer = 0 To grdorders.Rows.Count - 1
            sql = "Select * from tbloritems where discontinued='0' order by itemname"
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                CType(Me.grdorders.Columns(0), DataGridViewComboBoxColumn).Items.Add(dr("itemname"))
            End While
            dr.Dispose()
            cmd.Dispose()
            disconnect()

            'Next
            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub grdorders_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdorders.CellValidating
        If ColAdd Then
            Dim comboBoxColumn As DataGridViewComboBoxColumn = CType(grdorders.Columns(0), DataGridViewComboBoxColumn)
            If (e.ColumnIndex = comboBoxColumn.DisplayIndex) Then
                If (Not comboBoxColumn.Items.Contains(e.FormattedValue)) Then
                    comboBoxColumn.Items.Add(e.FormattedValue)
                    grdorders.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = comboBoxColumn.Items(comboBoxColumn.Items.Count - 1)
                End If
            End If
        End If
    End Sub

    Private Sub grdorders_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdorders.DataError
        If (e.Context = DataGridViewDataErrorContexts.LeaveControl) Then
            MessageBox.Show("leave control error")
        End If
    End Sub

    Private Sub grdorders_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdorders.EditingControlShowing
        Dim comboBoxColumn As DataGridViewComboBoxColumn = grdorders.Columns(0)
        If (grdorders.CurrentCellAddress.X = comboBoxColumn.DisplayIndex) Then
            Dim cb As ComboBox = e.Control
            If (cb IsNot Nothing) Then
                cb.DropDownStyle = ComboBoxStyle.DropDown
                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            End If
        End If
    End Sub

    Private Sub btnimgremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimgremove.Click
        If imgbox.Image Is Nothing Then
            MsgBox("Select photo first.", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        If imgname = "imgbox1" Then
            imgbox1.Image = imgbox11.Image
            imgbox11.Image = imgbox111.Image
            imgbox111.Image = Nothing
        ElseIf imgname = "imgbox11" Then
            imgbox11.Image = imgbox111.Image
            imgbox111.Image = Nothing
        ElseIf imgname = "imgbox111" Then
            imgbox111.Image = Nothing
        ElseIf imgname = "imgbox2" Then
            imgbox2.Image = imgbox22.Image
            imgbox22.Image = imgbox222.Image
            imgbox222.Image = Nothing
        ElseIf imgname = "imgbox22" Then
            imgbox22.Image = imgbox222.Image
            imgbox222.Image = Nothing
        ElseIf imgname = "imgbox222" Then
            imgbox222.Image = Nothing
        End If
        imgbox.Image = Nothing
    End Sub

    Private Sub imgbox11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgbox11.Click
        If imgbox11.Image IsNot Nothing Then
            imgbox.Image = imgbox11.Image
            imgname = imgbox11.Name
        End If
    End Sub

    Private Sub imgbox111_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgbox111.Click
        If imgbox111.Image IsNot Nothing Then
            imgbox.Image = imgbox111.Image
            imgname = imgbox111.Name
        End If
    End Sub

    Private Sub imgbox22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgbox22.Click
        If imgbox22.Image IsNot Nothing Then
            imgbox.Image = imgbox22.Image
            imgname = imgbox22.Name
        End If
    End Sub

    Private Sub imgbox222_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgbox222.Click
        If imgbox222.Image IsNot Nothing Then
            imgbox.Image = imgbox222.Image
            imgname = imgbox222.Name
        End If
    End Sub

    Private Sub btnimgcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimgcancel.Click
        If imgbox.Image IsNot Nothing Then
            imgbox.Image = Nothing
        End If
    End Sub

    Private Sub imgbox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgbox.Click
        If imgbox.Image IsNot Nothing Then
            viewimage.lblimgname.Text = ""
            viewimage.imgbox.Image = imgbox.Image
            viewimage.ShowDialog()
        End If
    End Sub

    Private Sub cmbtrans_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtrans.SelectedIndexChanged
        Try
            If cmbtrans.SelectedIndex = 0 Then
                btnattachso.Enabled = False
                btnattachpo.Enabled = False

                If trans.edittrans = False Then
                    txtso.Text = "0000"
                End If
                'txtpo.Text = "0000"
                lblso.Enabled = False
                lblpo.Enabled = False
                lblpo.Text = "PO#"
                btnattachpo.Text = "ATTACH PO"

            ElseIf cmbtrans.SelectedItem = "SALES TRANSACTION" Then  'sales transaction
                lblso.Enabled = True
                txtso.Enabled = True
                lblpo.Enabled = False
                txtpo.Enabled = False

                btnattachso.Enabled = True
                imgbox1.Enabled = True
                btnattachpo.Enabled = False
                GroupBox2.Text = "PO"
                imgbox2.Enabled = False
                imgbox2.Image = Nothing
                lblpo.Text = "PO#"
                btnattachpo.Text = "ATTACH PO"

            ElseIf cmbtrans.SelectedItem = "CUSTOMER DIRECT PICKUP FRM WHSE" Then  'CUSTOMER DIRECT PICKUP FRM WHSE
                lblso.Enabled = True
                txtso.Enabled = True
                lblpo.Enabled = False
                txtpo.Enabled = False

                btnattachso.Enabled = True
                imgbox1.Enabled = True
                btnattachpo.Enabled = False
                GroupBox2.Text = "PO"
                imgbox2.Enabled = False
                imgbox2.Image = Nothing
                lblpo.Text = "PO#"
                btnattachpo.Text = "ATTACH PO"

            ElseIf cmbtrans.SelectedItem = "JPSC DIRECT PICKUP FRM AGI" Then  'JPSC DIRECT PICKUP FRM AGI
                lblso.Enabled = True
                txtso.Enabled = True
                lblpo.Enabled = True
                txtpo.Enabled = True

                btnattachso.Enabled = True
                imgbox1.Enabled = True
                btnattachpo.Enabled = True
                GroupBox2.Text = "PO"
                imgbox2.Enabled = True
                lblpo.Text = "PO#"
                btnattachpo.Text = "ATTACH PO"

            ElseIf cmbtrans.SelectedItem = "CUSTOMER DIRECT PICKUP FRM AGI" Then  'CUSTOMER DIRECT PICKUP FRM AGI 
                lblso.Enabled = True
                txtso.Enabled = True
                lblpo.Enabled = True
                txtpo.Enabled = True

                btnattachso.Enabled = True
                imgbox1.Enabled = True
                btnattachpo.Enabled = True
                GroupBox2.Text = "PO"
                imgbox2.Enabled = True
                lblpo.Text = "PO#"
                btnattachpo.Text = "ATTACH PO"

            ElseIf cmbtrans.SelectedItem = "STOCK TRANSFER PICKUP FRM AGI" Then  'STOCK TRANSFER PICKUP FRM AGI

                lblso.Enabled = False
                txtso.Enabled = False
                lblpo.Enabled = True
                txtpo.Enabled = True

                btnattachso.Enabled = False
                imgbox1.Enabled = False
                imgbox1.Image = Nothing
                btnattachpo.Enabled = True
                imgbox2.Enabled = True

                lblpo.Text = "PO#"
                GroupBox2.Text = "PO"
                btnattachpo.Text = "ATTACH PO"

            ElseIf cmbtrans.SelectedItem = "STOCK TRANSFER WHSE TO WHSE" Then  'STOCK TRANSFER WHSE TO WHSE

                lblso.Enabled = False
                txtso.Enabled = False
                lblpo.Enabled = True
                txtpo.Enabled = True

                btnattachso.Enabled = False
                imgbox1.Enabled = False
                imgbox1.Image = Nothing
                btnattachpo.Enabled = True
                imgbox2.Enabled = True

                lblpo.Text = "ITR#"
                GroupBox2.Text = "ITR"
                btnattachpo.Text = "ATTACH ITR"

            ElseIf cmbtrans.SelectedItem = "STOCK TRANSFER PICKUP FRM PIER" Then   'STOCK TRANSFER PICKUP FRM PIER

                lblso.Enabled = False
                txtso.Enabled = False
                lblpo.Enabled = True
                txtpo.Enabled = True

                btnattachso.Enabled = False
                imgbox1.Enabled = False
                imgbox1.Image = Nothing
                btnattachpo.Enabled = True
                imgbox2.Enabled = True

                lblpo.Text = "SWS#"
                GroupBox2.Text = "SWS"
                btnattachpo.Text = "ATTACH SWS"

            ElseIf cmbtrans.SelectedItem = "JPSC DIRECT PICKUP FRM PIER" Or cmbtrans.SelectedItem = "CUSTOMER DIRECT PICKUP FRM PIER" Or cmbtrans.SelectedItem = "TRUCKING DIRECT PICKUP FRM PIER" Then
                'JPSC DIRECT PICKUP FRM PIER del customer (8)
                'CUSTOMER DIRECT PICKUP FRM PIER (9)
                'TRUCKING DIRECT PICKUP FRM PIER (10)

                lblso.Enabled = False
                txtso.Enabled = False
                lblpo.Enabled = True
                txtpo.Enabled = True

                btnattachso.Enabled = False
                imgbox1.Enabled = False
                imgbox1.Image = Nothing
                btnattachpo.Enabled = True
                imgbox2.Enabled = True

                lblpo.Text = "SWS#"
                GroupBox2.Text = "SWS"
                btnattachpo.Text = "ATTACH SWS"

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub grdorders_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdorders.RowsRemoved
        If grdorders.Rows.Count = 0 Then
            ' grdorders.Rows.Add()
        End If
    End Sub

    Private Sub cmbtype_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtype.Leave
        If Trim(cmbtype.Text) <> "" Then
            If Trim(cmbtype.Text).ToUpper <> "JPSC STOCK TRANSFER PICKUP" And Trim(cmbtype.Text).ToUpper <> "JPSC STOCK TRANSFER WHSE TO WHSE" And Trim(cmbtype.Text).ToUpper <> "JPSC SALES TRANSACTION" And Trim(cmbtype.Text).ToUpper <> "JPSC SALES TRANSACTION PICKUP" And Trim(cmbtype.Text).ToUpper <> "CUSTOMER SALES TRANSACTION WHSE" And Trim(cmbtype.Text).ToUpper <> "CUSTOMER SALES TRANSACTION PICKUP" And Trim(cmbtype.Text).ToUpper <> "TRUCKING STOCK TRANSFER PICKUP" And Trim(cmbtype.Text).ToUpper <> "TRUCKING SALES TRANSACTION PICKUP" And Trim(cmbtype.Text).ToUpper <> "TRUCKING SALES TRANSACTION WHSE" And Trim(cmbtype.Text).ToUpper <> "TRUCKING STOCK TRANSFER WHSE TO WHSE" Then
                MsgBox("Invalid transaction type.", MsgBoxStyle.Exclamation, "")
                cmbtype.Text = ""
                btnattachso.Enabled = False
                btnattachpo.Enabled = False

                'txtso.Text = "0000"
                'txtpo.Text = "0000"
                lblso.Enabled = False
                lblpo.Enabled = False
                lblpo.Text = "PO#"
                txtso.Enabled = False
                txtpo.Enabled = False
                btnattachpo.Text = "ATTACH PO"

                cmbpick.Text = ""
                cmbpick.Enabled = False
                refnum = ""
                Exit Sub
            End If
        End If

        Try
            If cmbtype.SelectedIndex = 0 Then
                btnattachso.Enabled = False
                btnattachpo.Enabled = False

                If trans.edittrans = False Then
                    txtso.Text = "0000"
                End If
                'txtpo.Text = "0000"
                lblso.Enabled = False
                lblpo.Enabled = False
                lblpo.Text = "PO#"
                btnattachpo.Text = "ATTACH PO"

                cmbpick.Enabled = False
                refnum = ""
            Else
                If cmbtype.SelectedItem.ToString.Contains("PICKUP") Then
                    If cmbpick.Text = "" Then
                        btnattachso.Enabled = False
                        btnattachpo.Enabled = False
                        txtso.Enabled = False
                        txtpo.Enabled = False

                        'txtso.Text = "0000"
                        'txtpo.Text = "0000"
                        lblso.Enabled = False
                        lblpo.Enabled = False
                        lblpo.Text = "PO#"
                        btnattachpo.Text = "ATTACH PO"

                        cmbpick.Enabled = True
                        refnum = ""
                    Else
                        cmbpick.Enabled = True
                    End If

                Else
                    btnattachso.Enabled = False
                    btnattachpo.Enabled = False

                    If trans.edittrans = False Then
                        txtso.Text = "0000"
                    End If
                    'txtpo.Text = "0000"
                    lblso.Enabled = False
                    lblpo.Enabled = False
                    lblpo.Text = "PO#"
                    btnattachpo.Text = "ATTACH PO"

                    cmbpick.Text = ""
                    cmbpick.Enabled = False
                    refnum = ""

                    If cmbtype.SelectedItem = "JPSC STOCK TRANSFER WHSE TO WHSE" Then
                        lblso.Enabled = False
                        txtso.Enabled = False
                        If trans.edittrans = False Then
                            txtso.Text = "0000"
                        End If
                        lblpo.Enabled = True
                        txtpo.Enabled = True
                        'txtpo.Text = "0000"

                        btnattachso.Enabled = False
                        imgbox1.Enabled = False
                        imgbox1.Image = Nothing
                        btnattachpo.Enabled = True
                        imgbox2.Enabled = True

                        lblpo.Text = "ITR#"
                        GroupBox2.Text = "ITR"
                        btnattachpo.Text = "ATTACH ITR"

                    ElseIf cmbtype.SelectedItem = "TRUCKING STOCK TRANSFER WHSE TO WHSE" Then
                        lblso.Enabled = False
                        txtso.Enabled = False
                        If trans.edittrans = False Then
                            txtso.Text = "0000"
                        End If
                        lblpo.Enabled = True
                        txtpo.Enabled = True
                        'txtpo.Text = "0000"

                        btnattachso.Enabled = False
                        imgbox1.Enabled = False
                        imgbox1.Image = Nothing
                        btnattachpo.Enabled = True
                        imgbox2.Enabled = True

                        lblpo.Text = "ITR#"
                        GroupBox2.Text = "ITR"
                        btnattachpo.Text = "ATTACH ITR"

                    ElseIf cmbtype.SelectedItem = "JPSC SALES TRANSACTION" Then
                        lblso.Enabled = True
                        txtso.Enabled = True
                        lblpo.Enabled = False
                        txtpo.Enabled = False

                        btnattachso.Enabled = True
                        imgbox1.Enabled = True
                        btnattachpo.Enabled = False
                        GroupBox2.Text = "PO"
                        imgbox2.Enabled = False
                        imgbox2.Image = Nothing
                        lblpo.Text = "PO#"
                        btnattachpo.Text = "ATTACH PO"

                    ElseIf cmbtype.SelectedItem = "CUSTOMER SALES TRANSACTION WHSE" Then
                        lblso.Enabled = True
                        txtso.Enabled = True
                        lblpo.Enabled = False
                        txtpo.Enabled = False

                        btnattachso.Enabled = True
                        imgbox1.Enabled = True
                        btnattachpo.Enabled = False
                        GroupBox2.Text = "PO"
                        imgbox2.Enabled = False
                        imgbox2.Image = Nothing
                        lblpo.Text = "PO#"
                        btnattachpo.Text = "ATTACH PO"

                    ElseIf cmbtype.SelectedItem = "TRUCKING SALES TRANSACTION WHSE" Then
                        lblso.Enabled = True
                        txtso.Enabled = True
                        lblpo.Enabled = False
                        txtpo.Enabled = False

                        btnattachso.Enabled = True
                        imgbox1.Enabled = True
                        btnattachpo.Enabled = False
                        GroupBox2.Text = "PO"
                        imgbox2.Enabled = False
                        imgbox2.Image = Nothing
                        lblpo.Text = "PO#"
                        btnattachpo.Text = "ATTACH PO"
                    End If
                    End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtype.SelectedIndexChanged
        Try
            If cmbtype.SelectedIndex = 0 Then
                btnattachso.Enabled = False
                btnattachpo.Enabled = False

                If trans.edittrans = False Then
                    txtso.Text = "0000"
                End If
                'txtpo.Text = "0000"
                lblso.Enabled = False
                lblpo.Enabled = False
                lblpo.Text = "PO#"
                btnattachpo.Text = "ATTACH PO"

                cmbpick.Enabled = False
                refnum = ""
            Else
                If cmbtype.SelectedItem.ToString.Contains("PICKUP") Then
                    If cmbpick.Text = "" Then
                        btnattachso.Enabled = False
                        btnattachpo.Enabled = False
                        txtso.Enabled = False
                        txtpo.Enabled = False

                        'txtso.Text = "0000"
                        'txtpo.Text = "0000"
                        lblso.Enabled = False
                        lblpo.Enabled = False
                        lblpo.Text = "PO#"
                        btnattachpo.Text = "ATTACH PO"

                        cmbpick.Enabled = True
                        refnum = ""
                    Else
                        cmbpick.Enabled = True
                    End If

                Else
                    btnattachso.Enabled = False
                    btnattachpo.Enabled = False

                    If trans.edittrans = False Then
                        txtso.Text = "0000"
                    End If
                    'txtpo.Text = "0000"
                    lblso.Enabled = False
                    lblpo.Enabled = False
                    lblpo.Text = "PO#"
                    btnattachpo.Text = "ATTACH PO"

                    cmbpick.Text = ""
                    cmbpick.Enabled = False
                    refnum = ""

                    If cmbtype.SelectedItem = "JPSC STOCK TRANSFER WHSE TO WHSE" Then
                        lblso.Enabled = False
                        txtso.Enabled = False
                        If trans.edittrans = False Then
                            txtso.Text = "0000"
                        End If
                        lblpo.Enabled = True
                        txtpo.Enabled = True
                        'txtpo.Text = "0000"

                        btnattachso.Enabled = False
                        imgbox1.Enabled = False
                        imgbox1.Image = Nothing
                        btnattachpo.Enabled = True
                        imgbox2.Enabled = True

                        lblpo.Text = "ITR#"
                        GroupBox2.Text = "ITR"
                        btnattachpo.Text = "ATTACH ITR"

                    ElseIf cmbtype.SelectedItem = "TRUCKING STOCK TRANSFER WHSE TO WHSE" Then
                        lblso.Enabled = False
                        txtso.Enabled = False
                        If trans.edittrans = False Then
                            txtso.Text = "0000"
                        End If
                        lblpo.Enabled = True
                        txtpo.Enabled = True
                        'txtpo.Text = "0000"

                        btnattachso.Enabled = False
                        imgbox1.Enabled = False
                        imgbox1.Image = Nothing
                        btnattachpo.Enabled = True
                        imgbox2.Enabled = True

                        lblpo.Text = "ITR#"
                        GroupBox2.Text = "ITR"
                        btnattachpo.Text = "ATTACH ITR"

                    ElseIf cmbtype.SelectedItem = "JPSC SALES TRANSACTION" Then
                        lblso.Enabled = True
                        txtso.Enabled = True
                        lblpo.Enabled = False
                        txtpo.Enabled = False

                        btnattachso.Enabled = True
                        imgbox1.Enabled = True
                        btnattachpo.Enabled = False
                        GroupBox2.Text = "PO"
                        imgbox2.Enabled = False
                        imgbox2.Image = Nothing
                        lblpo.Text = "PO#"
                        btnattachpo.Text = "ATTACH PO"

                    ElseIf cmbtype.SelectedItem = "CUSTOMER SALES TRANSACTION WHSE" Then
                        lblso.Enabled = True
                        txtso.Enabled = True
                        lblpo.Enabled = False
                        txtpo.Enabled = False

                        btnattachso.Enabled = True
                        imgbox1.Enabled = True
                        btnattachpo.Enabled = False
                        GroupBox2.Text = "PO"
                        imgbox2.Enabled = False
                        imgbox2.Image = Nothing
                        lblpo.Text = "PO#"
                        btnattachpo.Text = "ATTACH PO"

                    ElseIf cmbtype.SelectedItem = "TRUCKING SALES TRANSACTION WHSE" Then
                        lblso.Enabled = True
                        txtso.Enabled = True
                        lblpo.Enabled = False
                        txtpo.Enabled = False

                        btnattachso.Enabled = True
                        imgbox1.Enabled = True
                        btnattachpo.Enabled = False
                        GroupBox2.Text = "PO"
                        imgbox2.Enabled = False
                        imgbox2.Image = Nothing
                        lblpo.Text = "PO#"
                        btnattachpo.Text = "ATTACH PO"
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbpick_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbpick.Leave
        Try
            cmbpick.Text = Trim(cmbpick.Text)
            If Trim(cmbpick.Text) = "" Then
                Exit Sub
            Else
                sql = "Select * from tblwhse where status='1' and whsename='" & Trim(cmbpick.Text) & "' and (company='Atlantic Grains' or company='Supplier Warehouse')"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    cmbpick.Text = StrConv(Trim(cmbpick.Text), VbStrConv.ProperCase)
                Else
                    MsgBox("Cannot found " & Trim(cmbpick.Text), MsgBoxStyle.Critical, "")
                    cmbpick.Text = ""
                End If
                dr.Dispose()
                cmd.Dispose()
                disconnect()
            End If

            If cmbpick.Text = "AGI" Then 'And cmbtype.Text = "JPSC SALES TRANSACTION PICKUP" Then  'JPSC DIRECT PICKUP FRM AGI
                If cmbtype.Text = "JPSC SALES TRANSACTION PICKUP" Or cmbtype.Text = "CUSTOMER SALES TRANSACTION PICKUP" Or cmbtype.Text = "TRUCKING SALES TRANSACTION PICKUP" Then
                    lblso.Enabled = True
                    txtso.Enabled = True
                    lblpo.Enabled = True
                    txtpo.Enabled = True

                    btnattachso.Enabled = True
                    imgbox1.Enabled = True
                    btnattachpo.Enabled = True
                    GroupBox2.Text = "PO"
                    imgbox2.Enabled = True
                    lblpo.Text = "PO#"
                    btnattachpo.Text = "ATTACH PO"

                ElseIf cmbtype.Text = "JPSC STOCK TRANSFER PICKUP" Or cmbtype.Text = "TRUCKING STOCK TRANSFER PICKUP" Then
                    lblso.Enabled = False
                    txtso.Enabled = False
                    'txtso.Text = "0000"
                    lblpo.Enabled = True
                    txtpo.Enabled = True
                    If trans.edittrans = False Then
                        txtpo.Text = "0000"
                    End If

                    btnattachso.Enabled = False
                    imgbox1.Enabled = False
                    imgbox1.Image = Nothing
                    btnattachpo.Enabled = True
                    imgbox2.Enabled = True

                    lblpo.Text = "PO#"
                    GroupBox2.Text = "PO"
                    btnattachpo.Text = "ATTACH PO"

                End If

            ElseIf Trim(cmbpick.Text) = "" And cmbtype.Text.ToString.Contains("PICKUP") Then

                lblso.Enabled = False
                txtso.Enabled = False
                If trans.edittrans = False Then
                    txtso.Text = "0000"
                End If
                lblpo.Enabled = False
                txtpo.Enabled = False
                If trans.edittrans = False Then
                    txtpo.Text = "0000"
                End If

                btnattachso.Enabled = False
                imgbox1.Enabled = False
                imgbox1.Image = Nothing
                btnattachpo.Enabled = False
                imgbox2.Enabled = False

                lblpo.Text = "PO#"
                GroupBox2.Text = "PO"
                btnattachpo.Text = "ATTACH PO"

                txtnotes.Focus()

                ElseIf cmbpick.Text.Contains("PIER") Then 'ALL PICKUP PIER 

                    lblso.Enabled = False
                txtso.Enabled = False
                If trans.edittrans = False Then
                    txtso.Text = "0000"
                End If
                lblpo.Enabled = True
                txtpo.Enabled = True
                If trans.edittrans = False Then
                    txtpo.Text = "0000"
                End If

                btnattachso.Enabled = False
                imgbox1.Enabled = False
                imgbox1.Image = Nothing
                btnattachpo.Enabled = True
                imgbox2.Enabled = True

                lblpo.Text = "SWS#"
                GroupBox2.Text = "SWS"
                btnattachpo.Text = "ATTACH SWS"

                Else 'ALL PICKUP OTHER SUIPPLIER

                    lblso.Enabled = False
                txtso.Enabled = False
                If trans.edittrans = False Then
                    txtso.Text = "0000"
                End If
                lblpo.Enabled = False
                txtpo.Enabled = False
                If trans.edittrans = False Then
                    txtpo.Text = "0000"
                End If

                btnattachso.Enabled = False
                imgbox1.Enabled = False
                imgbox1.Image = Nothing
                btnattachpo.Enabled = False
                imgbox2.Enabled = False

                lblpo.Text = "PO#"
                GroupBox2.Text = "PO"
                btnattachpo.Text = "ATTACH PO"

                End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbpick_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbpick.SelectedIndexChanged
        Try
            cmbpick.Text = Trim(cmbpick.Text)
            If Trim(cmbpick.Text) = "" Then
                Exit Sub
            End If

            If cmbpick.Text = "AGI" Then 'And cmbtype.Text = "JPSC SALES TRANSACTION PICKUP" Then  'JPSC DIRECT PICKUP FRM AGI
                If cmbtype.Text = "JPSC SALES TRANSACTION PICKUP" Or cmbtype.Text = "CUSTOMER SALES TRANSACTION PICKUP" Or cmbtype.Text = "TRUCKING SALES TRANSACTION PICKUP" Then
                    lblso.Enabled = True
                    txtso.Enabled = True
                    lblpo.Enabled = True
                    txtpo.Enabled = True

                    btnattachso.Enabled = True
                    imgbox1.Enabled = True
                    btnattachpo.Enabled = True
                    GroupBox2.Text = "PO"
                    imgbox2.Enabled = True
                    lblpo.Text = "PO#"
                    btnattachpo.Text = "ATTACH PO"

                ElseIf cmbtype.Text = "JPSC STOCK TRANSFER PICKUP" Or cmbtype.Text = "TRUCKING STOCK TRANSFER PICKUP" Then
                    lblso.Enabled = False
                    txtso.Enabled = False
                    'txtso.Text = "0000"
                    lblpo.Enabled = True
                    txtpo.Enabled = True
                    If trans.edittrans = False Then
                        txtpo.Text = "0000"
                    End If

                    btnattachso.Enabled = False
                    imgbox1.Enabled = False
                    imgbox1.Image = Nothing
                    btnattachpo.Enabled = True
                    imgbox2.Enabled = True

                    lblpo.Text = "PO#"
                    GroupBox2.Text = "PO"
                    btnattachpo.Text = "ATTACH PO"

                End If

            ElseIf Trim(cmbpick.Text) = "" And cmbtype.Text.ToString.Contains("PICKUP") Then

                lblso.Enabled = False
                txtso.Enabled = False
                If trans.edittrans = False Then
                    txtso.Text = "0000"
                End If
                lblpo.Enabled = False
                txtpo.Enabled = False
                If trans.edittrans = False Then
                    txtpo.Text = "0000"
                End If

                btnattachso.Enabled = False
                imgbox1.Enabled = False
                imgbox1.Image = Nothing
                btnattachpo.Enabled = False
                imgbox2.Enabled = False

                lblpo.Text = "PO#"
                GroupBox2.Text = "PO"
                btnattachpo.Text = "ATTACH PO"

                txtnotes.Focus()

                ElseIf cmbpick.Text.Contains("PIER") Then 'ALL PICKUP PIER 

                    lblso.Enabled = False
                txtso.Enabled = False
                If trans.edittrans = False Then
                    txtso.Text = "0000"
                End If
                lblpo.Enabled = True
                txtpo.Enabled = True
                If trans.edittrans = False Then
                    txtpo.Text = "0000"
                End If

                btnattachso.Enabled = False
                imgbox1.Enabled = False
                imgbox1.Image = Nothing
                btnattachpo.Enabled = True
                imgbox2.Enabled = True

                lblpo.Text = "SWS#"
                GroupBox2.Text = "SWS"
                btnattachpo.Text = "ATTACH SWS"

            Else 'ALL PICKUP OTHER SUIPPLIER

                lblso.Enabled = False
                txtso.Enabled = False
                If trans.edittrans = False Then
                    txtso.Text = "0000"
                End If
                lblpo.Enabled = False
                txtpo.Enabled = False
                If trans.edittrans = False Then
                    txtpo.Text = "0000"
                End If

                btnattachso.Enabled = False
                imgbox1.Enabled = False
                imgbox1.Image = Nothing
                btnattachpo.Enabled = False
                imgbox2.Enabled = False

                lblpo.Text = "PO#"
                GroupBox2.Text = "PO"
                btnattachpo.Text = "ATTACH PO"

                End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbtype_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtype.SelectedValueChanged

    End Sub

    Private Sub cmbtype_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtype.TextChanged
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ "
        Dim theText As String = cmbtype.Text
        Dim Letter As String
        Dim SelectionIndex As Integer = cmbtype.SelectionStart
        Dim Change As Integer

        For x As Integer = 0 To cmbtype.Text.Length - 1
            Letter = cmbtype.Text.Substring(x, 1)
            If Not charactersDisallowed.Contains(Letter) Then
                theText = theText.Replace(Letter, String.Empty)
                Change = 1
            End If
        Next

        cmbtype.Text = theText.ToUpper
        cmbtype.Select(SelectionIndex - Change, 0)
    End Sub

    Private Sub cmbpick_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbpick.TextChanged
        Try
            Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ "
            Dim theText As String = cmbpick.Text
            Dim Letter As String
            Dim SelectionIndex As Integer = cmbpick.SelectionStart
            Dim Change As Integer

            For x As Integer = 0 To cmbpick.Text.Length - 1
                Letter = cmbpick.Text.Substring(x, 1)
                If Not charactersDisallowed.Contains(Letter) Then
                    theText = theText.Replace(Letter, String.Empty)
                    Change = 1
                End If
            Next

            cmbpick.Text = theText.ToUpper
            cmbpick.Select(SelectionIndex - Change, 0)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub whse()
        Try
            cmbpick.Items.Clear()
            cmbpick.Items.Add("")

            sql = "Select * from tblwhse where status='1' and (company='Atlantic Grains' or company='Supplier Warehouse') order by whsename"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbpick.Items.Add(dr("whsename").ToString.ToUpper)
            End While
            dr.Dispose()
            cmd.Dispose()
            disconnect()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub txtpo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpo.TextChanged
        Try
            Dim charactersDisallowed As String = "toflwTOFLW1234567890 "
            Dim theText As String = txtpo.Text
            Dim Letter As String
            Dim SelectionIndex As Integer = txtpo.SelectionStart
            Dim Change As Integer

            For x As Integer = 0 To txtpo.Text.Length - 1
                Letter = txtpo.Text.Substring(x, 1)
                If Not charactersDisallowed.Contains(Letter) Then
                    theText = theText.Replace(Letter, String.Empty)
                    Change = 1
                End If
            Next

            txtpo.Text = theText.ToUpper
            txtpo.Select(SelectionIndex - Change, 0)

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

    Private Sub cmbcus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcus.SelectedIndexChanged

    End Sub
End Class