Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports Excel = Microsoft.Office.Interop.Excel

Public Class customer

    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim stat As String
    Public cuscnf As Boolean = False, bodycnf As Boolean = False
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
        If conn.State <> ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub customer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub customer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        view()
    End Sub

    Private Sub txtname_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtname.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtaddress_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtaddress.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtemail_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtemail.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtphone_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnview.Click
        btncancel.PerformClick()
        view()
    End Sub

    Public Sub view()
        Try
            clickbtn = "ALL"

            grdcus.Rows.Clear()

            sql = "Select * from tblcustomer"
            If chkhide.Checked = True Then
                sql = sql & " where status='1'"
            End If
            sql = sql & " order by customer"
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

                grdcus.Rows.Add(dr("cusid"), dr("customer"), dr("description"), dr("address"), dr("email"), dr("remarks"), stat)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grdcus.Rows.Count = 0 Then
                btnupdate.Enabled = False
                btndeactivate.Enabled = False
            Else
                btnupdate.Enabled = True
                btndeactivate.Enabled = True
            End If

            txtcode.Text = ""
            txtname.Text = ""
            txtaddress.Text = ""
            txtemail.Text = ""
            txtrems.Text = ""
            txtname.Focus()
            btnupdate.Text = "&Update"
            btnsearch.Enabled = True
            btndeactivate.Enabled = True
            btnadd.Enabled = True
            btncancel.Enabled = False
            chkhide.Enabled = True

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

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                Me.Cursor = Cursors.Default
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            'check complete
            If Trim(txtcode.Text) <> "" And Trim(txtname.Text) <> "" And Trim(txtaddress.Text) <> "" Then
                'check if already exist
                sql = "Select * from tblcustomer where customer='" & txtcode.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Customer code is already exist.", MsgBoxStyle.Exclamation, "")
                    txtcode.Text = ""
                    txtcode.Focus()
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                cuscnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If cuscnf = True Then
                    sql = "Insert into tblcustomer (customer, description, address, email, remarks, datecreated, createdby, datemodified, modifiedby, status) values ('" & txtcode.Text & "','" & txtname.Text & "', '" & Trim(txtaddress.Text) & "', '" & Trim(txtemail.Text) & "', '" & Trim(txtrems.Text) & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1')"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    MsgBox("Successfully saved.", MsgBoxStyle.Information, "")
                    view()
                End If
            Else
                MsgBox("Complete the required fields.", MsgBoxStyle.Exclamation, "")
                txtcode.Focus()
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

    Private Sub txtname_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtname.Leave
        txtname.Text = StrConv(Trim(txtname.Text), VbStrConv.ProperCase)
    End Sub

    Private Sub txtaddress_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtaddress.Leave
        txtaddress.Text = StrConv(Trim(txtaddress.Text), VbStrConv.ProperCase)
    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Try
            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                Me.Cursor = Cursors.Default
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdcus.SelectedRows.Count = 1 Or grdcus.SelectedCells.Count = 1 Then
                If btnupdate.Text = "&Update" Then
                    If grdcus.Rows(grdcus.CurrentRow.Index).Cells(6).Value = "Deactivated" Then
                        MsgBox("Cannot update deactivated customer.", MsgBoxStyle.Exclamation, "")
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                    lblid.Text = grdcus.Rows(grdcus.CurrentRow.Index).Cells(0).Value
                    lblcus.Text = grdcus.Rows(grdcus.CurrentRow.Index).Cells(1).Value
                    txtcode.Text = grdcus.Rows(grdcus.CurrentRow.Index).Cells(1).Value
                    txtname.Text = grdcus.Rows(grdcus.CurrentRow.Index).Cells(2).Value
                    txtaddress.Text = grdcus.Rows(grdcus.CurrentRow.Index).Cells(3).Value
                    txtemail.Text = grdcus.Rows(grdcus.CurrentRow.Index).Cells(4).Value
                    txtrems.Text = grdcus.Rows(grdcus.CurrentRow.Index).Cells(5).Value
                    btnsearch.Enabled = False
                    btnadd.Enabled = False
                    btnupdate.Text = "&Save"
                    btncancel.Enabled = True
                    btndeactivate.Enabled = False
                Else
                    'update
                    If Trim(txtcode.Text) = "" Or Trim(txtname.Text) = "" Or Trim(txtaddress.Text) = "" Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Complete the required fields.", MsgBoxStyle.Exclamation, "")
                        txtname.Focus()
                        Exit Sub
                    End If

                    sql = "Select * from tblcustomer where customer='" & txtcode.Text & "' and cusid<>'" & lblid.Text & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MsgBox("Customer code is already exist.", MsgBoxStyle.Exclamation, "")
                        txtcode.Text = ""
                        txtcode.Focus()
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    cuscnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If cuscnf = True Then
                        sql = "Update tblcustomer set customer='" & Trim(txtcode.Text) & "', description='" & Trim(txtname.Text) & "', address='" & Trim(txtaddress.Text) & "', email='" & Trim(txtemail.Text) & "', remarks='" & Trim(txtrems.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where cusid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        If Trim(txtcode.Text) <> Trim(lblcus.Text) Then
                            'update other tbl in database
                            sql = "Update tbllogreserve set customer='" & Trim(txtcode.Text) & "' where customer='" & lblcus.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            sql = "Update tbllogticket set customer='" & Trim(txtcode.Text) & "' where customer='" & lblcus.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            sql = "Update tblorderfill set customer='" & Trim(txtcode.Text) & "' where customer='" & lblcus.Text & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()
                        End If


                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    btnupdate.Text = "&Update"
                    btnsearch.Enabled = True
                    btnadd.Enabled = True
                    btndeactivate.Enabled = True
                    btncancel.Enabled = False
                    txtcode.Text = ""
                    txtname.Text = ""
                    txtaddress.Text = ""
                    txtemail.Text = ""
                    txtrems.Text = ""
                    txtname.Focus()
                    cuscnf = False
                End If
            Else
                MsgBox("Select only one", MsgBoxStyle.Exclamation, "")
                btnview.PerformClick()
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

    Private Sub grdcus_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdcus.SelectionChanged
        If grdcus.Rows(grdcus.CurrentRow.Index).Cells(6).Value = "Active" Then
            btndeactivate.Text = "&Deactivate"
        Else
            btndeactivate.Text = "A&ctivate"
        End If

        count()
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        txtcode.Text = ""
        txtname.Text = ""
        txtaddress.Text = ""
        txtemail.Text = ""
        txtrems.Text = ""
        txtname.Focus()
        btnupdate.Text = "&Update"
        btnsearch.Enabled = True
        btndeactivate.Enabled = True
        btnadd.Enabled = True
        btncancel.Enabled = False
        chkhide.Enabled = True
    End Sub

    Private Sub btndeactivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeactivate.Click
        Try
            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                Me.Cursor = Cursors.Default
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdcus.SelectedRows.Count = 1 Or grdcus.SelectedCells.Count = 1 Then
                lblid.Text = grdcus.Rows(grdcus.CurrentRow.Index).Cells(0).Value
                If btndeactivate.Text = "&Deactivate" Then
                    'check if theres item available status
                    sql = "Select * from tblorderfill where customer='" & grdcus.Rows(grdcus.CurrentRow.Index).Cells(1).Value & "' and status<>'2'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MsgBox("Cannot deactivate. Customer is still in use.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    cuscnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If cuscnf = True Then
                        sql = "Update tblcustomer set status='0', datemodified=GetDate(), modifiedby='" & login.user & "' where cusid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    cuscnf = False
                Else
                    cuscnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If cuscnf = True Then
                        sql = "Update tblcustomer set status='1', datemodified=GetDate(), modifiedby='" & login.user & "' where cusid='" & lblid.Text & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully Saved", MsgBoxStyle.Information, "")
                        btnview.PerformClick()
                    End If

                    cuscnf = False
                End If
            Else
                MsgBox("Select only one", MsgBoxStyle.Exclamation, "")
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

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            clickbtn = "Searched"

            If Trim(txtcode.Text) <> "" Or Trim(txtname.Text) <> "" Then
                grdcus.Rows.Clear()
                sql = "Select * from tblcustomer where "
                If Trim(txtcode.Text) <> "" Then
                    sql = sql & " customer like '%" & Trim(txtcode.Text) & "%'"
                End If
                If Trim(txtname.Text) <> "" Then
                    sql = sql & " description like '%" & Trim(txtname.Text) & "%'"
                End If
                sql = sql & " order by customer"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    chkhide.Enabled = False
                    If dr("status") = 1 Then
                        stat = "Active"
                    Else
                        stat = "Deactivated"
                    End If
                    grdcus.Rows.Add(dr("cusid"), dr("customer"), dr("description"), dr("address"), dr("email"), dr("remarks"), stat)
                    txtname.Text = ""
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            Else
                MsgBox("Input customer code first.", MsgBoxStyle.Exclamation, "")
                txtcode.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            If grdcus.Rows.Count = 0 Then
                MsgBox("Cannot found " & Trim(txtname.Text), MsgBoxStyle.Critical, "")
                txtname.Text = ""
                txtname.Focus()
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

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
            Me.Cursor = Cursors.Default
            MsgBox("Access denied!", MsgBoxStyle.Critical, "")
            Exit Sub
        End If
        importcustomer.ShowDialog()
        view()
    End Sub

    Private Sub txtcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcode.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtcode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcode.Leave
        txtcode.Text = Trim(txtcode.Text).ToString.ToUpper
    End Sub

    Public Sub count()
        Try
            lblcount.Text = "     Selected Rows Count: " & grdcus.SelectedRows.Count
        Catch ex As Exception
            MsgBox(ex.Message)
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
            Dim sfilename As String = clickbtn & " Customer as of " & daterange & ".xls"

            objExcel = New Excel.Application
            bkWorkBook = objExcel.Workbooks.Add
            shWorkSheet = CType(bkWorkBook.ActiveSheet, Excel.Worksheet)

            With shWorkSheet
                .Range("A1", misValue).EntireRow.Font.Bold = True
                .Range("A1:E1").EntireRow.WrapText = True
                .Range("A1:E" & grdcus.RowCount + 1).HorizontalAlignment = -4108
                .Range("A1:E" & grdcus.RowCount + 1).VerticalAlignment = -4108
                .Range("A1:E" & grdcus.RowCount + 1).Font.Size = 10
                'Set Clipboard Copy Mode     
                grdcus.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
                grdcus.SelectAll()
                grdcus.RowHeadersVisible = False

                'Get the content from Grid for Clipboard     
                'Dim str As String = TryCast(grdcus.GetClipboardContent().GetData(DataFormats.UnicodeText), String)
                Dim str As String = grdcus.GetClipboardContent().GetData(DataFormats.UnicodeText)

                'Set the content to Clipboard     
                Clipboard.SetText(str, TextDataFormat.UnicodeText) 'TextDataFormat.UnicodeText)

                'Identify and select the range of cells in Excel to paste the clipboard data.     
                .Range("A1:E1", misValue).Select()

                'WIDTH
                .Range("A1:A" & grdcus.RowCount + 1).ColumnWidth = 22

                .Range("B1:B" & grdcus.RowCount + 1).ColumnWidth = 50
                .Range("C1:C" & grdcus.RowCount + 1).ColumnWidth = 15
                .Range("D1:D" & grdcus.RowCount + 1).ColumnWidth = 13
                .Range("E1:E" & grdcus.RowCount + 1).ColumnWidth = 20

                'Paste the clipboard data     
                .Paste()
                Clipboard.Clear()

            End With

            'format alignment
            'shWorkSheet.Range("D2", "D" & grdcus.RowCount + 1).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
            For i = 0 To grdcus.RowCount - 1
                'shWorkSheet.Cells(i + 2, 1) = grd.Rows(i).Cells(1).Value
                ' shWorkSheet.Range("A1").EntireRow.NumberFormat = "MM/dd/yyyy"
            Next

            'lagyan ng title na red door kit tska ung date na sakop ng report
            shWorkSheet.Range("A1").EntireRow.Insert()
            shWorkSheet.Range("A2").EntireRow.Insert()
            shWorkSheet.Range("A3").EntireRow.Insert()
            shWorkSheet.Cells(1, 1) = login.logwhse & " Warehouse"
            shWorkSheet.Cells(2, 1) = clickbtn & " Customer as of " & daterange
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
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As System.Runtime.InteropServices.COMException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
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

    Private Sub txtcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged

    End Sub

    Private Sub txtname_TextChanged(sender As Object, e As EventArgs) Handles txtname.TextChanged

    End Sub

    Private Sub customer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.WindowState = FormWindowState.Maximized
    End Sub
End Class