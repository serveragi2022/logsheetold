Imports System.IO
Imports System.Data.SqlClient

Public Class coaselect
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
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub coaselect_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub coaselect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewpending()
    End Sub

    Public Sub viewpending()
        Try
            grdcoa.Rows.Clear()

            sql = "Select tblcoa.coaid, tblcoa.coanum, tblcoa.ofnum, tblorderfill.wrsnum, tblorderfill.customer, tblcoa.status from tblcoa right outer join tblorderfill on tblcoa.ofnum=tblorderfill.ofnum where tblcoa.status='1' and tblcoa.branch='" & login.branch & "'"

            If login.logwhse.ToUpper <> "ALL" Then
                '/sql = sql & " and whsename='" & login.logwhse & "'"
            End If

            sql = sql & " order by tblcoa.coaid"

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "In Process"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If
                grdcoa.Rows.Add(dr("coaid"), dr("coanum"), dr("ofnum"), dr("wrsnum"), dr("customer"), stat)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grdcoa.Rows.Count = 0 Then
                btnselect.Enabled = False
            Else
                btnselect.Enabled = True
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

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        Try
            Dim d As Integer = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(0).Value
            Dim a As String = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(1).Value
            Dim s As String = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(2).Value
            coa.defaultform()
            coa.lblcoaid.Text = d
            coa.txtcoanum.Text = a.Substring(4, a.Length - 4)
            coa.txtofnum.Text = s.Substring(3, s.Length - 3)
            coa.ginfo.Enabled = True
            coa.grd1.Rows.Clear()
            coa.grd2.Rows.Clear()
            coa.grd3.Rows.Clear()
            coa.grd4.Rows.Clear()
            coa.btnsearch.PerformClick()
            Me.Close()

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
End Class