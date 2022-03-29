Imports System.IO
Imports System.Data.SqlClient

Public Class sampling
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim selectedrow As Integer
    Public qarems As String = "", samplingcnf As Boolean, samprems As Boolean = False

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

    Private Sub sampling_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewpendingsamp()
    End Sub

    Public Sub viewpendingsamp()
        Try
            grdsamp.Rows.Clear()

            sql = "Select tblorderfill.ofid, tblorderfill.ofnum, tblorderfill.wrsnum, tblorderfill.customer, tblofitem.ofitemid, tblofitem.itemname, tblofitem.qasampling, tblofitem.qasampdate, tblofitem.qasampneym, tblofitem.qasamprems"
            sql = sql & " from tblorderfill right outer join tblofitem on tblorderfill.ofnum=tblofitem.ofnum where tblofitem.qasampling='0' and tblorderfill.status='1' and tblofitem.status<>'3' and tblorderfill.branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim stat As String = ""
                If dr("qasampling") = 1 Then
                    stat = "Completed"
                Else
                    stat = "Pending"
                End If

                grdsamp.Rows.Add(dr("ofid"), dr("ofnum"), dr("wrsnum"), dr("customer"), dr("ofitemid"), dr("itemname"), stat, dr("qasampdate"), dr("qasampneym"), dr("qasamprems"))
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

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        viewpendingsamp()
    End Sub

    Private Sub SamplingCompleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SamplingCompleteToolStripMenuItem.Click
        Try
            If grdsamp.SelectedCells.Count = 1 Or grdsamp.SelectedRows.Count = 1 Then
                Dim ofitemid As String = grdsamp.Rows(selectedrow).Cells(4).Value

                If login.depart = "Admin Dispatching" Or login.depart = "All" Or login.depart = "QCA" Then
                    qarems = ""
                    samplingrems.lblitem.Text = grdsamp.Rows(selectedrow).Cells(5).Value
                    samplingrems.lblofnum.Text = grdsamp.Rows(selectedrow).Cells(1).Value
                    samplingrems.ShowDialog()

                    If samprems = True Then
                        samplingcnf = False
                        confirmsave.GroupBox1.Text = login.wgroup
                        confirmsave.ShowDialog()
                        If samplingcnf = True Then
                            'update tblofitem
                            sql = "Update tblofitem set qasampling='1', qasampdate=GetDate(), qasampneym='" & login.user & "', qasamprems='" & qarems & "' where ofitemid='" & ofitemid & "'"
                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            MsgBox("Successfully completed.", MsgBoxStyle.Information, "")
                            viewpendingsamp()
                        Else
                            MsgBox("User cancelled QA sampling entry.", MsgBoxStyle.Information, "")
                        End If
                    Else
                        MsgBox("User cancelled QA sampling entry.", MsgBoxStyle.Information, "")
                    End If
                    
                Else
                    MsgBox("Access denied.", MsgBoxStyle.Critical, "")
                End If

            Else
                MsgBox("Select only one.", MsgBoxStyle.Exclamation, "")
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

    Private Sub grdsamp_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdsamp.CellContentClick

    End Sub

    Private Sub grdsamp_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdsamp.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then
                selectedrow = e.RowIndex

                grdsamp.ClearSelection()
                grdsamp.Rows(selectedrow).Cells(e.ColumnIndex).Selected = True

                Me.ContextMenuStrip1.Show(Cursor.Position)
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
End Class