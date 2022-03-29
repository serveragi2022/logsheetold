Imports System.IO
Imports System.Data.SqlClient

Public Class tsetitem
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public tsetcnf As Boolean

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

    Private Sub Form3_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        lbllognum.Text = ""
        lblline.Text = ""
    End Sub

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        view()

        If ticket.btnlogconfirm.Text = "Confirmed" Or ticket.btnlogcnfitems.Enabled = False Then
            btnactivate.Enabled = False
            grdline.Enabled = False
        Else
            btnactivate.Enabled = True
            grdline.Enabled = True
        End If
    End Sub

    Public Sub view()
        Try
            grdline.Rows.Clear()

            sql = "Select * from tbllogsheet where logsheetnum='" & lbllognum.Text & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                lblline.Text = dr("palletizer")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            sql = "Select i.status, i.logitemid, i.itemname, s.palletizer from tbllogitem i "
            sql = sql & " right outer join tbllogsheet s on i.logsheetid=s.logsheetid where i.logsheetid='" & ticket.lbllogid.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim stat As String = ""
                If dr("status") = 0 Then
                    stat = "In Process"
                ElseIf dr("status") = 1 Then
                    stat = "Available"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If
                grdline.Rows.Add(dr("logitemid"), dr("palletizer"), dr("itemname"), stat)
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

    Private Sub btnactivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnactivate.Click
        Try
            If grdline.SelectedCells.Count = 1 Or grdline.SelectedRows.Count = 1 Then
                If btnactivate.Text = "Activate" Then
                    'sa pag activate kelangan walang ibng status na zero or one para ma activate nya yung isa
                    sql = "Select * from tbllogitem where (status='0' or status='1') and logsheetid='" & ticket.lbllogid.Text & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        MsgBox("Cannot activete. There is a pending item. Complete it first.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    grdline.Enabled = False
                    tsetcnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If tsetcnf = True Then
                        Dim stat As Integer
                        'check if status=2 then zero if status=3 then one to be available
                        sql = "Select status from tbllogitem where logitemid='" & grdline.Rows(grdline.CurrentRow.Index).Cells(0).Value & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            stat = dr("status")
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()

                        If stat = 2 Then
                            sql = "Update tbllogitem set status='0', modifiedby='" & login.user & "', datemodified=GetDate() where logitemid='" & grdline.Rows(grdline.CurrentRow.Index).Cells(0).Value & "'"

                        ElseIf stat = 3 Then
                            sql = "Update tbllogitem set status='1', modifiedby='" & login.user & "', datemodified=GetDate() where logitemid='" & grdline.Rows(grdline.CurrentRow.Index).Cells(0).Value & "'"
                        End If
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        grdline.Enabled = True
                        MsgBox(grdline.Rows(grdline.CurrentRow.Index).Cells(2).Value & " is activated.", MsgBoxStyle.Information, "")
                        view()
                    End If

                ElseIf btnactivate.Text = "Deactivate" Then
                    '/MsgBox("check if available plng ung status meaning wala png pallet.")
                    sql = "Select * from tbllogitem where logsheetid='" & ticket.lbllogid.Text & "' and logitemid='" & grdline.Rows(grdline.CurrentRow.Index).Cells(0).Value & "' and status<>3"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr("status") <> 1 Then
                            MsgBox("Cannot deactivate item name that is in process.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                    Else
                        Exit Sub
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()


                    tsetcnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If tsetcnf = True Then
                        sql = "Update tbllogitem set status='3', datemodified=GetDate(), modifiedby='" & login.user & "' where logitemid='" & grdline.Rows(grdline.CurrentRow.Index).Cells(0).Value & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully deactivated.", MsgBoxStyle.Information, "")

                        view()
                    End If
                End If
            Else
                MsgBox("Select one only.", MsgBoxStyle.Exclamation, "")
            End If
            grdline.Enabled = True

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

    Private Sub grdline_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdline.CellContentClick

    End Sub

    Private Sub grdline_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdline.SelectionChanged
        If grdline.Rows(grdline.CurrentRow.Index).Cells(3).Value = "Completed" Then
            '/btnactivate.Enabled = True
            btnactivate.Text = "Activate"
        Else
            '/btnactivate.Enabled = False
            '/btnactivate.Text = "Deactivate"
        End If
    End Sub
End Class