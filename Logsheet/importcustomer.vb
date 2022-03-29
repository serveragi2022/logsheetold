Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports Excel = Microsoft.Office.Interop.Excel

Public Class importcustomer
    Dim SheetList As New ArrayList
    Dim MyConnection As OleDbConnection
    Dim DtSet As System.Data.DataSet
    Dim MyCommand As OleDbDataAdapter
    Dim ii As Integer
    Dim checkBoxColumn As New DataGridViewCheckBoxColumn()
    Dim ds As New DataSet
    Dim dv As DataView

    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim derrors As Boolean = False
    Public importcnf As Boolean = False

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

    Private Sub importvadd_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub importcustomer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub importvadd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'DtSet.Clear()
        txtPath.Text = ""
        btnLoadData.Enabled = False
        btncheck.Enabled = False
        btnadd.Enabled = False
        'dgvdata.Rows.Clear()
        dgvdata.Columns.Clear()
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        With OpenFileDialog1
            .FileName = "Excel File"
            .Filter = "Excel Worksheets|*.xls;*.xlsx"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                txtPath.Text = .FileName
                If txtPath.Text <> "" Then
                    btnLoadData.Enabled = True
                    btncheck.Enabled = False
                    btnadd.Enabled = False
                Else
                    btnLoadData.Enabled = False
                End If
            End If
        End With
    End Sub

    Private Sub btnLoadData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadData.Click
        Try
            ' DtSet.Clear()
            Me.Cursor = Cursors.WaitCursor
            MyConnection = New OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & txtPath.Text & "';Extended Properties=""Excel 8.0;HDR={1}""")

            Dim objExcel As Excel.Application
            Dim objWorkBook As Excel.Workbook
            Dim objWorkSheets As Excel.Worksheet
            Dim ExcelSheetName As String = ""

            objExcel = CreateObject("Excel.Application")
            objWorkBook = objExcel.Workbooks.Open(txtPath.Text)

            For Each objWorkSheets In objWorkBook.Worksheets
                SheetList.Add(objWorkSheets.Name)
            Next

            MyCommand = New OleDbDataAdapter("select * from [" & SheetList(0) & "$]", MyConnection)
            MyCommand.TableMappings.Add("Table", "Net-informations.com")
            'MsgBox(SheetList(0).ToString)
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet)
            dgvdata.DataSource = DtSet.Tables(0)
            ' MyCommand.Dispose()
            MyConnection.Close()

            objWorkBook.Close()
            objExcel.Quit()

            releaseObject(objWorkBook)
            releaseObject(objExcel)

            Me.Cursor = Cursors.Default

            dgvdata.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True
            'dgvdata.ColumnHeadersHeight = 40
            dgvdata.Columns(0).Width = 200
            dgvdata.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dgvdata.Columns(1).Width = 200
            dgvdata.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dgvdata.Columns(2).Width = 180
            dgvdata.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dgvdata.Columns(3).Width = 200
            dgvdata.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            If dgvdata.Rows.Count <> 0 Then
                btncheck.Enabled = True
            Else
                btncheck.Enabled = False
            End If

            derrors = False

            'remember: sa price kung nde sya numeric di sya iloload
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

    Private Sub btncheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncheck.Click
        Try
            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                Me.Cursor = Cursors.Default
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            'dito magaganap ung checking of errors
            '////check yung per column if wlng magkaparehas n item code and item name

            checkinput()

            If derrors = False Then
                MsgBox("Click add button to continue.", MsgBoxStyle.Information, "")
                btnadd.Enabled = True
            Else
                MsgBox("Error occurred. Please correct the highlighted errors and try again.", MsgBoxStyle.Critical, "")
                btnadd.Enabled = False
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

    Public Sub checkinput()
        Try
            For Each row As DataGridViewRow In dgvdata.Rows
                'check if there's no apostrophe
                'cells(0)
                If dgvdata.Rows(row.Index).Cells(0).Value.ToString.Contains("'") Then
                    dgvdata.Rows(row.Index).Cells(0).Value = dgvdata.Rows(row.Index).Cells(0).Value.ToString.Replace("'", "")
                End If
                dgvdata.Rows(row.Index).Cells(0).Value = dgvdata.Rows(row.Index).Cells(0).Value.ToString.ToUpper

                'cells(1)
                If dgvdata.Rows(row.Index).Cells(1).Value.ToString.Contains("'") Then
                    dgvdata.Rows(row.Index).Cells(1).Value = dgvdata.Rows(row.Index).Cells(1).Value.ToString.Replace("'", "")
                End If
                dgvdata.Rows(row.Index).Cells(1).Value = StrConv(dgvdata.Rows(row.Index).Cells(1).Value.ToString, VbStrConv.ProperCase)

                'cells(2)
                If dgvdata.Rows(row.Index).Cells(2).Value.ToString.Contains("'") Then
                    dgvdata.Rows(row.Index).Cells(2).Value = dgvdata.Rows(row.Index).Cells(2).Value.ToString.Replace("'", "")
                End If
                dgvdata.Rows(row.Index).Cells(2).Value = StrConv(dgvdata.Rows(row.Index).Cells(2).Value.ToString, VbStrConv.ProperCase)

                'cells(3)
                If dgvdata.Rows(row.Index).Cells(3).Value.ToString.Contains("'") Then
                    dgvdata.Rows(row.Index).Cells(3).Value = dgvdata.Rows(row.Index).Cells(3).Value.ToString.Replace("'", "")
                End If

                'cells(4)
                If dgvdata.Rows(row.Index).Cells(4).Value.ToString.Contains("'") Then
                    dgvdata.Rows(row.Index).Cells(4).Value = dgvdata.Rows(row.Index).Cells(4).Value.ToString.Replace("'", "")
                End If


                'check if null
                If dgvdata.Rows(row.Index).Cells(0).Value.ToString = "" Then
                    dgvdata.ClearSelection()
                    dgvdata.Rows(row.Index).Cells(0).ToolTipText = "Customer code should not be null."
                    dgvdata.Rows(row.Index).Cells(0).Selected = True
                    dgvdata.Rows(row.Index).Cells(0).Style.BackColor = Color.Yellow
                    dgvdata.ClearSelection()
                    derrors = True
                End If

                'check if null
                If dgvdata.Rows(row.Index).Cells(1).Value.ToString = "" Then
                    dgvdata.ClearSelection()
                    dgvdata.Rows(row.Index).Cells(1).ToolTipText = "Customer name should not be null."
                    dgvdata.Rows(row.Index).Cells(1).Selected = True
                    dgvdata.Rows(row.Index).Cells(1).Style.BackColor = Color.Yellow
                    dgvdata.ClearSelection()
                    derrors = True
                End If

                If dgvdata.Rows(row.Index).Cells(2).Value.ToString = "" Then
                    dgvdata.ClearSelection()
                    dgvdata.Rows(row.Index).Cells(2).ToolTipText = "Address should not be null."
                    dgvdata.Rows(row.Index).Cells(2).Selected = True
                    dgvdata.Rows(row.Index).Cells(2).Style.BackColor = Color.Yellow
                    dgvdata.ClearSelection()
                    derrors = True
                End If

                If dgvdata.Rows(row.Index).Cells(0).Value.ToString <> "" Then
                    sql = "Select * from tblcustomer where customer='" & dgvdata.Rows(row.Index).Cells(0).Value & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        dgvdata.ClearSelection()
                        dgvdata.Rows(row.Index).Cells(0).ToolTipText = "Customer code " & dgvdata.Rows(row.Index).Cells(0).Value.ToString & " is already exist."
                        dgvdata.Rows(row.Index).Cells(0).Selected = True
                        dgvdata.Rows(row.Index).Cells(0).Style.BackColor = Color.Yellow
                        dgvdata.ClearSelection()
                        derrors = True
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If

                If dgvdata.Rows(row.Index).Cells(1).Value.ToString <> "" Then
                    sql = "Select * from tblcustomer where description='" & dgvdata.Rows(row.Index).Cells(1).Value & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        dgvdata.ClearSelection()
                        dgvdata.Rows(row.Index).Cells(1).ToolTipText = "Customer name " & dgvdata.Rows(row.Index).Cells(1).Value.ToString & " is already exist."
                        dgvdata.Rows(row.Index).Cells(1).Selected = True
                        dgvdata.Rows(row.Index).Cells(1).Style.BackColor = Color.Yellow
                        dgvdata.ClearSelection()
                        derrors = True
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If

            Next

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
            importcnf = False
            If derrors = False Then
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If importcnf = True Then

                    For Each row As DataGridViewRow In dgvdata.Rows
                        Dim icus As String = dgvdata.Rows(row.Index).Cells(0).Value.ToString
                        Dim ides As String = dgvdata.Rows(row.Index).Cells(1).Value.ToString
                        Dim iadd As String = dgvdata.Rows(row.Index).Cells(2).Value.ToString
                        Dim iemail As String = dgvdata.Rows(row.Index).Cells(3).Value.ToString
                        Dim irems As String = dgvdata.Rows(row.Index).Cells(4).Value.ToString


                        sql = "Insert into tblcustomer (customer, description, address, email, remarks, datecreated, createdby, datemodified, modifiedby, status) values ('" & Trim(icus) & "', '" & Trim(ides) & "', '" & Trim(iadd) & "', '" & Trim(iemail) & "', '" & Trim(irems) & "', GetDate(), '" & login.user & "', GetDate(), '" & login.user & "', '1')"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()
                    Next

                    importcnf = False
                    MsgBox("Successfully Added.", MsgBoxStyle.Information, "")
                    Me.Close()
                End If
            End If
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
End Class