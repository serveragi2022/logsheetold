Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports Excel = Microsoft.Office.Interop.Excel

Public Class newitems
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

    Private Sub newitems_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close Manage Items Form?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub newitems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor

            '/frmload()
            loadcat()
            cmbcat.Focus()
            btnview.PerformClick()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
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
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnview.Click
        Try
            grditems.Rows.Clear()

            Dim i As Integer = 0
            If chkhide.Checked = True Then
                sql = "Select * from tblitems where discontinued='1' order by category, tickettype, itemname"
            Else
                sql = "Select * from tblitems where discontinued='0' order by category, tickettype, itemname"
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
                grditems.Item(5, i).Value = dr("category")
                grditems.Item(6, i).Value = dr("tickettype")

                If dr("status") = 1 Then
                    grditems.Item(7, i).Value = "Active"
                Else
                    grditems.Item(7, i).Value = "Deactivated"
                End If

                i += 1
            End While
            cmd.Dispose()
            dr.Dispose()
            conn.Close()

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

    Private Sub btnadditem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadditem.Click
        Try
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" And login.wgroup <> "Agi Admin Staff" And login.wgroup <> "Supervisor" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            'check if complete
            If cmbcat.SelectedItem = "" Then
                MsgBox("Select category.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            ElseIf Trim(txtcode.Text) = "" Then
                MsgBox("Input item code.", MsgBoxStyle.Exclamation, "")
                txtcode.Focus()
                Exit Sub
            ElseIf Trim(txtname.Text) = "" Then
                MsgBox("Input item name.", MsgBoxStyle.Exclamation, "")
                txtname.Focus()
                Exit Sub
            ElseIf Trim(txtdes.Text) = "" Then
                MsgBox("Input item description.", MsgBoxStyle.Exclamation, "")
                txtdes.Focus()
                Exit Sub
            ElseIf Trim(txtprice.Text) = "" Then
                MsgBox("Input price.", MsgBoxStyle.Exclamation, "")
                txtprice.Focus()
                Exit Sub
            ElseIf cmbtype.SelectedItem = "" Then
                MsgBox("Select ticket type.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            'check if code is existing
            sql = "Select * from tblitems where itemcode='" & Trim(txtcode.Text) & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MsgBox("Item code is already exist.", MsgBoxStyle.Exclamation, "")
                txtcode.Focus()
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            'check if name is existing
            sql = "Select * from tblitems where itemname='" & Trim(txtname.Text) & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                MsgBox("Item name is already exist.", MsgBoxStyle.Exclamation, "")
                txtname.Focus()
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Dim a As String = MsgBox("Are you sure you want to add item?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Add Item")
            If a = vbYes Then
               
                sql = "Insert into tblitems (category, itemcode, itemname, description, price, tickettype, datecreated, createdby, datemodified, modifiedby, status, discontinued) values('" & cmbcat.SelectedItem & "','" & Trim(txtcode.Text) & "','" & Trim(txtname.Text) & "','" & Trim(txtdes.Text) & "','" & Trim(txtprice.Text) & "','" & cmbtype.SelectedItem & "',GetDate(),'" & login.user & "',GetDate(),'" & login.user & "','1','0')"
                connect()
                cmd = New SqlCommand(sql, conn) 'New OleDbCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()

                Me.Cursor = Cursors.Default
                MsgBox("Successfully Added", MsgBoxStyle.Information, "")
                btnview.PerformClick()
                btncancel.Enabled = False
                txtid.Text = ""
                txtcode.Text = ""
                txtdes.Text = ""
                txtname.Text = ""
                txtprice.Text = ""

            Else
                Me.Cursor = Cursors.Default
                Exit Sub
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

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            If login.wgroup <> "Administrator" And login.wgroup <> "Manager" And login.wgroup <> "Agi Admin Staff" And login.wgroup <> "Supervisor" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If btnupdate.Text = "&Update" Then
                If (grditems.SelectedCells.Count = 1 Or grditems.SelectedRows.Count = 1) Then
                Else
                    MsgBox("Select one only", MsgBoxStyle.Exclamation, "")
                    updatebool = False
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                If grditems.Rows(grditems.CurrentRow.Index).Cells(7).Value <> "Active" Then
                    Me.Cursor = Cursors.Default
                    MsgBox("Cannot update deactivated item.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If

                updatebool = True

                '/lblcode.Text = grditems.Item(1, grditems.CurrentRow.Index).Value
                lblname.Text = grditems.Item(2, grditems.CurrentRow.Index).Value
                txtcode.Enabled = True
                txtdes.Enabled = True
                txtprice.Enabled = True
                txtname.Visible = True
                txtcode.ReadOnly = False
                txtdes.ReadOnly = False
                txtprice.ReadOnly = False
                txtname.ReadOnly = False
                cmbtype.Enabled = True
                txtid.Text = grditems.Item(0, grditems.CurrentRow.Index).Value
                txtcode.Text = grditems.Item(1, grditems.CurrentRow.Index).Value
                txtname.Text = grditems.Item(2, grditems.CurrentRow.Index).Value
                txtdes.Text = grditems.Item(3, grditems.CurrentRow.Index).Value
                txtprice.Text = grditems.Item(4, grditems.CurrentRow.Index).Value
                cmbcat.SelectedItem = grditems.Item(5, grditems.CurrentRow.Index).Value
                cmbtype.SelectedItem = grditems.Item(6, grditems.CurrentRow.Index).Value
                txtstatus.Text = grditems.Item(7, grditems.CurrentRow.Index).Value

                btnupdate.Text = "&Save"
                btnadditem.Enabled = False
                btndiscon.Enabled = False
                btnexport.Enabled = False
                btnimport.Enabled = False
                btnprint.Enabled = False
                btnsearch.Enabled = False
                btnview.Enabled = False
                btncancel.Enabled = True

            ElseIf btnupdate.Text = "&Save" Then
                'check if complete
                If cmbcat.SelectedItem <> "" And Trim(txtcode.Text) <> "" And Trim(txtname.Text) <> "" And Trim(txtprice.Text) <> "" Then
                    'check if may item na ganun ibng id lang
                    'check if code is existing
                    sql = "Select itemcode from tblitems where itemcode='" & Trim(txtcode.Text) & "' and itemid<>'" & txtid.Text & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MsgBox("Item code is already exist.", MsgBoxStyle.Exclamation, "")
                        txtcode.Focus()
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    'check if name is existing
                    sql = "Select itemname from tblitems where itemname='" & Trim(txtname.Text) & "' and itemid<>'" & txtid.Text & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MsgBox("Item name is already exist.", MsgBoxStyle.Exclamation, "")
                        txtname.Focus()
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    Dim a As String = MsgBox("Are you sure you want to save the item?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
                    If a = vbYes Then
                        firmitems = False
                        confirm.GroupBox1.Text = login.wgroup
                        confirm.ShowDialog()
                        If firmitems = True Then
                            Me.Cursor = Cursors.WaitCursor
                            sql = "Update tblitems set category='" & cmbcat.SelectedItem & "', itemcode='" & Trim(txtcode.Text) & "', itemname='" & Trim(txtname.Text) & "', description='" & Trim(txtdes.Text) & "', price='" & Trim(txtprice.Text) & "', tickettype='" & cmbtype.SelectedItem & "', datemodified=GetDate(), modifiedby='" & login.user & "' where itemid='" & txtid.Text & "' "
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

                            Me.Cursor = Cursors.Default
                            MsgBox("Successfully Saved.", MsgBoxStyle.Information, "")
                            btnupdate.Text = "&Update"
                            txtid.Text = ""
                            txtcode.Text = ""
                            txtdes.Text = ""
                            txtname.Text = ""
                            txtprice.Text = ""
                            btnadditem.Enabled = True
                            btndiscon.Enabled = True
                            btnexport.Enabled = True
                            btnimport.Enabled = True
                            btnprint.Enabled = True
                            btnsearch.Enabled = True
                            btnview.Enabled = True
                            btncancel.Enabled = False
                            btnview.PerformClick()
                        End If
                    End If
                Else
                    Me.Cursor = Cursors.Default
                    MsgBox("Complete the required fields.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
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

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        btnupdate.Text = "&Update"
        txtid.Text = ""
        txtcode.Text = ""
        txtdes.Text = ""
        txtname.Text = ""
        txtprice.Text = ""
        btnadditem.Enabled = True
        btndiscon.Enabled = True
        btnexport.Enabled = True
        btnimport.Enabled = True
        btnprint.Enabled = True
        btnsearch.Enabled = True
        btnview.Enabled = True
        btncancel.Enabled = False
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Me.Cursor = Cursors.WaitCursor
        '/itemsprintprev.Close()
        '/itemsprint.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
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

            Dim password As String = "AGI123"
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

    Private Sub items_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btndiscon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndiscon.Click
        Try
            If grditems.Rows.Count <> 0 Then
                If grditems.SelectedCells.Count = 1 Or grditems.SelectedRows.Count = 1 Then
                    If btndiscon.Text = "Continue" Then
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

                            MsgBox("Successfully saved.", MsgBoxStyle.Information, "")
                            btnview.PerformClick()
                        End If
                    Else
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

                            MsgBox("Successfully saved.", MsgBoxStyle.Information, "")
                            btnview.PerformClick()
                        End If
                    End If
                Else
                    MsgBox("Select only one.", MsgBoxStyle.Exclamation, "")
                End If
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

    Private Sub grditems_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub selectgrd()
        If grditems.Rows(grditems.CurrentRow.Index).Cells(7).Value = "Deactivated" Then
            btndiscon.Text = "Continue"
        Else
            btndiscon.Text = "Discontinue"
        End If
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            If cmbcat.SelectedItem <> "" Or Trim(txtcode.Text) <> "" Or Trim(txtname.Text) <> "" Or cmbtype.SelectedItem <> "" Then
                grditems.Rows.Clear()
                Dim i As Integer = 0

                sql = "Select * from tblitems where itemname is not null"
                If cmbcat.SelectedItem <> "" Then
                    sql = sql & " and category='" & cmbcat.SelectedItem & "'"
                End If
                If Trim(txtcode.Text) <> "" Then
                    sql = sql & " and itemcode like '%" & Trim(txtcode.Text) & "%'"
                End If
                If Trim(txtname.Text) <> "" Then
                    sql = sql & " and itemname like '%" & Trim(txtname.Text) & "%'"
                End If
                If cmbtype.SelectedItem <> "" Then
                    sql = sql & " and tickettype='" & cmbtype.SelectedItem & "'"
                End If
                sql = sql & " order by category, tickettype, itemname"

                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    Dim stat As String = ""
                    If dr("status") = 1 Then
                        stat = "Active"
                    Else
                        stat = "Deactivated"
                    End If

                    grditems.Rows.Add(dr("itemid"), dr("itemcode"), dr("itemname"), dr("description"), dr("price"), dr("category"), dr("tickettype"), stat)

                    i += 1
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Else
                MsgBox("Input item code first.", MsgBoxStyle.Exclamation, "")
                txtcode.Focus()
                Exit Sub
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

    Private Sub txtcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789.-/"
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
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789.-/"
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
        Dim charactersDisallowed As String = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ 0123456789.-/"
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

    Private Sub grditems_SelectionChanged(sender As Object, e As EventArgs) Handles grditems.SelectionChanged
        selectgrd()
    End Sub
End Class