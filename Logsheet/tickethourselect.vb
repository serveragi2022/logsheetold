Imports System.IO
Imports System.Data.SqlClient

Public Class tickethourselect
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

    Private Sub ticketselectline_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewlogsheets()
    End Sub

    Public Sub viewlogsheets()
        Try
            grdline.Rows.Clear()

            sql = "Select logsheetid,whsename,palletizer,logsheetnum,status,bagtype from tbllogsheet where status='1' and allitems<>'1' and branch='" & login.branch & "'"

            If login.logwhse.ToUpper <> "ALL" Then
                sql = sql & " and whsename='" & login.logwhse & "'"
            End If

            sql = sql & " order by whsename, palletizer, logsheetid"

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
                grdline.Rows.Add(dr("logsheetid"), dr("whsename"), dr("palletizer"), dr("logsheetnum"), stat, dr("bagtype"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If grdline.Rows.Count = 0 Then
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
        Me.Dispose()
    End Sub

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        Try

            Dim s As String = grdline.Rows(grdline.CurrentRow.Index).Cells(3).Value
            sql = "Select i.itemname from tbllogitem i inner join tbllogsheet s on i.logsheetid=s.logsheetid "
            sql = sql & " where s.logsheetnum='" & s & "' and s.branch='" & login.branch & "' and s.status='1' and s.allitems='0' and i.status='0'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                tickethourly.defaultform()
                tickethourly.txtwhse.Text = grdline.Rows(grdline.CurrentRow.Index).Cells(1).Value
                tickethourly.txtline.Text = grdline.Rows(grdline.CurrentRow.Index).Cells(2).Value
                tickethourly.txtlogsheet.Text = grdline.Rows(grdline.CurrentRow.Index).Cells(3).Value
                tickethourly.sacktype = grdline.Rows(grdline.CurrentRow.Index).Cells(5).Value
                tickethourly.txtitem.Text = dr("itemname")

                dr.Dispose()
                cmd.Dispose()
                Me.Close()
            End If
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
End Class