Imports System.IO
Imports System.Data.SqlClient

Public Class orderfillselect
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Public frm As String

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

    Private Sub orderfillselect_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub orderfillselect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewpending()
    End Sub

    Public Sub viewpending()
        Try
            grdof.Rows.Clear()

            sql = "Select * from tblorderfill where status='1' and branch='" & login.branch & "'"

            If frm = "branorderfill" Then
                sql = sql & " and whsename='BRAN WHSE'"

            ElseIf frm = "orderfill" Then
                sql = sql & " and whsename<>'BRAN WHSE'"
            End If

            sql = sql & " order by ofid"

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
                grdof.Rows.Add(dr("ofid"), dr("ofnum"), dr("wrsnum"), dr("customer"), stat)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grdof.Rows.Count = 0 Then
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
        orderfill.txtorf.ReadOnly = True
        Me.Close()
    End Sub

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        Try
            Dim s As String = grdof.Rows(grdof.CurrentRow.Index).Cells(1).Value
            Dim d As Integer = grdof.Rows(grdof.CurrentRow.Index).Cells(0).Value
            Dim o As String = ""
            sql = "Select ofnum from tblorderfill where ofnum='" & s & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                o = s.Substring(3, s.Length - 3)
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If frm = "orderfill" Then
                orderfill.txtorf.Text = o
                orderfill.lblorfid.Text = d
                orderfill.defaultform()
                orderfill.Panelbuttons.Enabled = False
                orderfill.selectorderfill()
            ElseIf frm = "branorderfill" Then
                branorderfill.txtorf.Text = o
                branorderfill.lblorfid.Text = d
                branorderfill.defaultform()
                branorderfill.Panelbuttons.Enabled = False
                branorderfill.selectorderfill()
            End If
            Me.Dispose()

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