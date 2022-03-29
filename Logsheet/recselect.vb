Imports System.IO
Imports System.Data.SqlClient

Public Class recselect
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public winfrm As String

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

            If winfrm = "recticket" Then
                sql = "Select tbllogsheet.logsheetid, tbllogsheet.whsename, tbllogsheet.palletizer, tbllogsheet.logsheetnum, tbllogsheet.status, tblreceive.recid, tblreceive.recnum, tblreceive.platenum from tbllogsheet"
                sql = sql & " RIGHT outer JOIN tblreceive ON tbllogsheet.recnum=tblreceive.recnum"
                sql = sql & " where tbllogsheet.status='1' and tbllogsheet.allitems<>'1' and tbllogsheet.branch='" & login.branch & "' and tblreceive.branch='" & login.branch & "'"
                sql = sql & " and tbllogsheet.palletizer='LINE R'"
            Else
                sql = "Select tbllogsheet.logsheetid, tbllogsheet.whsename, tbllogsheet.palletizer, tbllogsheet.logsheetnum, tbllogsheet.status from tbllogsheet"
                sql = sql & " where tbllogsheet.status='1' and tbllogsheet.allitems<>'1' and tbllogsheet.branch='" & login.branch & "'"
                sql = sql & " and tbllogsheet.palletizer<>'LINE R'"
            End If

            If login.logwhse.ToUpper <> "ALL" Then
                sql = sql & " and tbllogsheet.whsename='" & login.logwhse & "'"
            End If

            sql = sql & " order by tbllogsheet.whsename, tbllogsheet.palletizer, tbllogsheet.logsheetid"

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

                If winfrm = "recticket" Then
                    grdline.Rows.Add(dr("logsheetid"), dr("whsename"), dr("palletizer"), dr("logsheetnum"), stat, dr("recid"), dr("recnum"), dr("platenum"))
                Else
                    grdline.Rows.Add(dr("logsheetid"), dr("whsename"), dr("palletizer"), dr("logsheetnum"), stat, "", "")
                End If

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
            Dim t As String = grdline.Rows(grdline.CurrentRow.Index).Cells(6).Value
            sql = "Select whsename,logsheetdate,shift from tbllogsheet where logsheetnum='" & s & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                If winfrm = "recticket" Then
                    recticket.txtsearch.Text = s.Substring(2, s.Length - 2)
                    recticket.lblwhse.Text = dr("whsename").ToString
                    recticket.lbldate.Text = Format(dr("logsheetdate"), "yyyy/MM/dd")
                    recticket.lblshift.Text = dr("shift").ToString
                    recticket.lblrecid.Text = grdline.Rows(grdline.CurrentRow.Index).Cells(5).Value
                    recticket.txtrecnum.Text = t.Substring(2, t.Length - 2)
                Else
                    ticket.txtsearch.Text = s.Substring(2, s.Length - 2)
                    ticket.lblwhse.Text = dr("whsename").ToString
                    ticket.lbldate.Text = Format(dr("logsheetdate"), "yyyy/MM/dd")
                    ticket.lblshift.Text = dr("shift").ToString
                End If
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If winfrm = "recticket" Then
                recticket.btnsearch.PerformClick()
                recticket.defaultform()
            Else
                ticket.btnsearch.PerformClick()
                ticket.defaultform()
            End If

            winfrm = ""
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