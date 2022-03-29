Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.ComponentModel

Public Class recsum
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public adlogcnf As Boolean, trems As String, adlogby As String
    Dim clickbtn As String = "", selectedrow As Integer, gridsql As String, logbranch As String, loginwhse As String

    Private threadEnabled As Boolean, threadEnabledsteps As Boolean, threadEnableddestin As Boolean
    Private backgroundWorker As BackgroundWorker
    Private backgroundWorkersteps As BackgroundWorker
    Private backgroundWorkerdestin As BackgroundWorker

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
        If conn.State <> ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub recsum_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewpending()
        reclogsheet.TopLevel = False
        reclogsheet.TopMost = True
        reclogsheet.Parent = Panel1
        reclogsheet.WindowState = FormWindowState.Maximized
        '/reclogsheet.Size = Panel1.Size
        reclogsheet.Focus()
        reclogsheet.BringToFront()
        reclogsheet.Location = New Point(10, 10)
        reclogsheet.Show()
    End Sub

    Private Sub viewpending()
        Try
            clickbtn = "Receive Pending"

            grdrec.Rows.Clear()

            Dim stat As String = ""
            sql = "Select * from tblreceive where status='1' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("status") = 1 Then
                    stat = "In Process"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If
                grdrec.Rows.Add(dr("recid"), dr("recdate"), dr("recnum"), dr("frmbranch"), dr("remarks"), dr("datecreated"), dr("createdby"), dr("datemodified"), dr("modifiedby"), stat, dr("datecancelled"), dr("cancelledby"), dr("creason"))
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
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnview.Click
        viewpending()
    End Sub

    Private Sub CancelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelToolStripMenuItem.Click
        Try
            'check level of users
            If login.depart <> "All" And login.depart <> "Warehouse" And login.depart <> "Admin Dispatching" Then
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If

            If grdrec.SelectedCells.Count = 1 Or grdrec.SelectedRows.Count = 1 Then
                'check if logsheet is not cancelled
                sql = "Select recid from tblreceive where recid='" & grdrec.Rows(selectedrow).Cells(0).Value & "' and status='3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Receive# " & grdrec.Rows(selectedrow).Cells(2).Value & " is already cancelled.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if logsheet is not cancelled
                sql = "Select status from tbllogsheet where recnum='" & grdrec.Rows(selectedrow).Cells(2).Value & "' and allitems='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MsgBox("Invalid. A Receive Log Sheet is already cut off.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                Dim a As String = MsgBox("Are you sure you want to cancel?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                If a = vbNo Then
                    Exit Sub
                Else
                    reccancel.lbllogid.Text = grdrec.Rows(selectedrow).Cells(0).Value
                    reccancel.lbllognum.Text = grdrec.Rows(selectedrow).Cells(2).Value
                    reccancel.ShowDialog()
                End If

                If clickbtn = "Search" Then
                    btnsearch.PerformClick()
                Else
                    btnview.PerformClick()
                End If

            Else
                MsgBox("Select only one.", MsgBoxStyle.Exclamation, "")
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

    Private Sub grdrec_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdrec.CellContentClick

    End Sub

    Private Sub grdrec_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdrec.CellMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then

            grdrec.ClearSelection()
            grdrec.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True

            selectedrow = e.RowIndex
            Me.ContextMenuStrip1.Show(Cursor.Position)
        End If
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        
    End Sub
End Class