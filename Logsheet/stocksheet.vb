Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports Excel = Microsoft.Office.Interop.Excel

Public Class stocksheet
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

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

    Private Sub stocksheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            grdstocks.Rows.Clear()

            sql = "Select logsheetdate, itemname, SUM(avtickets) as avtickets from vlistpallets"
            sql = sql & " where logstat=2 and branch='" & login.branch & "' and avtickets is not null and avtickets>0"
            sql = sql & " group by logsheetdate, itemname"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                grdstocks.Rows.Add(dr("logsheetdate"), dr("itemname"), dr("avtickets"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grdstocks.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Critical, "")
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
            If grdstocks.Rows.Count = 0 Then
                MsgBox("No record found.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            Dim objExcel As New Excel.Application
            Dim bkWorkBook As Excel.Workbook
            Dim shWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            Dim i As Integer
            Dim j As Integer


            Dim daterange As String = Format(Date.Now, "MMMM dd, yyyy")
            Dim sfilename As String = login.branch & " Stock sheet as of " & daterange & ".xls"

            objExcel = New Excel.Application
            bkWorkBook = objExcel.Workbooks.Add
            shWorkSheet = CType(bkWorkBook.ActiveSheet, Excel.Worksheet)

            With shWorkSheet
                .Range("A1", misValue).EntireRow.Font.Bold = True
                .Range("A1:C1").EntireRow.WrapText = True
                .Range("A1:C" & grdstocks.RowCount + 1).HorizontalAlignment = -4108
                .Range("A1:C" & grdstocks.RowCount + 1).VerticalAlignment = -4108
                .Range("A1:C" & grdstocks.RowCount + 1).Font.Size = 10
                'Set Clipboard Copy Mode     
                grdstocks.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
                grdstocks.SelectAll()
                grdstocks.RowHeadersVisible = False

                'Get the content from Grid for Clipboard     
                'Dim str As String = TryCast(grdstocks.GetClipboardContent().GetData(DataFormats.UnicodeText), String)
                Dim str As String = grdstocks.GetClipboardContent().GetData(DataFormats.UnicodeText)

                'Set the content to Clipboard     
                Clipboard.SetText(str, TextDataFormat.UnicodeText) 'TextDataFormat.UnicodeText)

                'Identify and select the range of cells in Excel to paste the clipboard data.     
                .Range("A1:C1", misValue).Select()

                'WIDTH
                .Range("A1:A" & grdstocks.RowCount + 1).ColumnWidth = 30

                .Range("B1:B" & grdstocks.RowCount + 1).ColumnWidth = 70
                .Range("C1:C" & grdstocks.RowCount + 1).ColumnWidth = 30

                'Paste the clipboard data     
                .Paste()
                Clipboard.Clear()

            End With

            'format alignment
            'shWorkSheet.Range("D2", "D" & grdstocks.RowCount + 1).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
            For i = 0 To grdstocks.RowCount - 1
                'shWorkSheet.Cells(i + 2, 1) = grd.Rows(i).Cells(1).Value
                ' shWorkSheet.Range("A1").EntireRow.NumberFormat = "yyyy/MM/dd"
            Next

            'lagyan ng title na red door kit tska ung date na sakop ng report
            shWorkSheet.Range("A1").EntireRow.Insert()
            shWorkSheet.Range("A2").EntireRow.Insert()
            shWorkSheet.Range("A3").EntireRow.Insert()
            shWorkSheet.Cells(1, 1) = login.branch
            shWorkSheet.Cells(2, 1) = " Stock Sheet as of " & daterange
            shWorkSheet.Cells(1, 1).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
            shWorkSheet.Range("A4").EntireRow.WrapText = True

            Me.Cursor = Cursors.Default

            objExcel.Visible = False
            'objExcel.Application.DisplayAlerts = False

            Dim password As String = "agi123456"
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
End Class