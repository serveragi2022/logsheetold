Imports System.IO
Imports System.Data.SqlClient

Public Class orderfillinfo
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

    Private Sub orderfillinfo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub orderfillinfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewof()

        cmbitems.DropDownWidth = 300
    End Sub

    Public Sub defaultform()
        txtofid.Text = ""
        txtofnum.Text = ""
        txtofdate.Text = ""
        txtwrs.Text = ""
        txtcus.Text = ""
        txtref.Text = ""
        txtplate.Text = ""
        txtdriver.Text = ""
        txtstatus.Text = ""
        cmbitems.Items.Clear()
        cmbitems.Text = ""
        txtbags.Text = ""
        txtcoanum.Text = ""
        lblitemid.Text = ""
        grdlog.Rows.Clear()
        grdcancel.Rows.Clear()
    End Sub

    Public Sub viewof()
        Try
            grdlog.Rows.Clear()
            grdcancel.Rows.Clear()
            cmbitems.Items.Clear()

            'view general
            sql = "Select * from tblorderfill where ofid='" & txtofid.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                txtofnum.Text = dr("ofnum")
                txtofdate.Text = Format(dr("ofdate"), "yyyy/MM/dd")
                txtwrs.Text = dr("wrsnum")
                txtcus.Text = dr("customer")
                txtref.Text = dr("cusref")
                txtplate.Text = dr("platenum")
                txtdriver.Text = dr("driver").ToString
                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "In Process"
                ElseIf dr("status") = 2 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If
                txtstatus.Text = stat
                If IsDBNull(dr("completed")) = False Then
                    txtcondate.Text = dr("completed")
                Else
                    txtcondate.Text = ""
                End If
                If IsDBNull(dr("completedby")) = True Then
                    txtconby.Text = dr("createdby")
                Else
                    txtconby.Text = dr("completedby").ToString
                End If
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            'view items
            sql = "Select * from tblofitem where ofnum='" & txtofnum.Text & "' and branch='" & login.branch & "' order by itemname"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbitems.Items.Add(dr("itemname"))
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

    Private Sub cmbitems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbitems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbitems_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbitems.Leave
        Try
            If Trim(cmbitems.Text) <> "" Then
                sql = "Select * from tblitems where itemname='" & Trim(cmbitems.Text) & "' and status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    cmbitems.Text = dr("itemname")
                Else
                    MsgBox("Invalid Item name.", MsgBoxStyle.Critical, "")
                    txtbags.Text = ""
                    txtcoanum.Text = ""
                    txtsampdeyt.Text = ""
                    txtsampneym.Text = ""
                    txtsamprems.Text = ""
                    txtitemstat.Text = ""
                    grdlog.Rows.Clear()
                    grdcancel.Rows.Clear()
                    cmbitems.Text = ""
                    cmbitems.Focus()
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            Else
                txtbags.Text = ""
                txtcoanum.Text = ""
                txtsampdeyt.Text = ""
                txtsampneym.Text = ""
                txtsamprems.Text = ""
                txtitemstat.Text = ""
                grdlog.Rows.Clear()
                grdcancel.Rows.Clear()
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbitems_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbitems.SelectedIndexChanged
        viewitem()
    End Sub

    Public Sub viewitem()
        Try
            If Trim(cmbitems.Text) <> "" Then
                txtsampneym.Text = ""
                txtsampdeyt.Text = ""
                txtsamprems.Text = ""
                txtitemstat.Text = ""

                sql = "Select * from tblofitem where ofnum='" & txtofnum.Text & "' and itemname='" & Trim(cmbitems.Text) & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    txtofitemid.Text = dr("ofitemid")
                    lblitemid.Text = dr("ofitemid")
                    txtbags.Text = dr("numbags")
                    If IsDBNull(dr("qasampneym")) = False Then
                        txtsampneym.Text = dr("qasampneym")
                    Else
                        txtsampneym.Text = ""
                    End If

                    If IsDBNull(dr("qasampdate")) = False Then
                        txtsampdeyt.Text = dr("qasampdate")
                    Else
                        txtsampdeyt.Text = ""
                    End If

                    If IsDBNull(dr("qasamprems")) = False Then
                        txtsamprems.Text = dr("qasamprems")
                    Else
                        txtsamprems.Text = ""
                    End If

                    If dr("Status") = 0 Then
                        txtitemstat.Text = "In Process"
                    ElseIf dr("Status") = 1 Then
                        txtitemstat.Text = "Available"
                    ElseIf dr("Status") = 2 Then
                        txtitemstat.Text = "Completed"
                    ElseIf dr("Status") = 3 Then
                        txtitemstat.Text = "Cancelled"
                    End If

                    If IsDBNull(dr("coanum")) = False Then
                        txtcoanum.Text = dr("coanum")
                    Else
                        txtcoanum.Text = ""
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                grdlog.Rows.Clear()
                grdcancel.Rows.Clear()

                sql = "Select * from tbloflog where ofnum='" & txtofnum.Text & "' and ofitemid='" & lblitemid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    grdlog.Rows.Add(dr("oflogid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("palletnum"), dr("selectedbags"), dr("ticketseries"))
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                sql = "Select * from tbloflogcancel where ofnum='" & txtofnum.Text & "' and ofitemid='" & lblitemid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    grdcancel.Rows.Add(dr("logsheetnum"), dr("palletnum"), dr("cticketnum"), Format(dr("cticketdate"), "hh:mm tt"), dr("remarks"), dr("cancelby").ToString)
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if oflogid has return in tblofreturn status=1
                For Each row As DataGridViewRow In grdlog.Rows
                    Dim oflogid As Integer = grdlog.Rows(row.Index).Cells(0).Value

                    sql = "Select * from tblofreturn where status='1' and oflogid='" & oflogid & "'"
                    conn.Open()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        grdlog.Rows(row.Index).DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128)
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                Next

            Else
                grdlog.Rows.Clear()
                grdcancel.Rows.Clear()
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

    Private Sub grdcancel_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcancel.CellContentClick

    End Sub

    Private Sub grdcancel_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdcancel.SelectionChanged
        countcancel()
    End Sub

    Public Sub countcancel()
        Try
            lblcountcnl.Text = "Selected Rows Count: " & grdcancel.SelectedRows.Count
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub linkreturn_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linkreturn.LinkClicked
        Try
            If grdlog.Rows.Count <> 0 Then
                orderfillrethistory.grdreturn.Rows.Clear()

                orderfillrethistory.lblitem.Text = cmbitems.Text

                sql = "Select * from tblofreturn where ofitemid='" & txtofitemid.Text & "' and status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    orderfillrethistory.grdreturn.Rows.Add(dr("retid"), dr("palletnum"), dr("returntype"), "", "", dr("returndate"), dr("returnby"), dr("reason"))
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                orderfillrethistory.ShowDialog()

            Else
                MsgBox("Select item first.", MsgBoxStyle.Exclamation, "")
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

    Private Sub linkothers_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linkothers.LinkClicked
        Try
            sql = "Select pstart,pend,forklift,stevedore,passnum,matdispo,fgstart,fgend,loadstart,loadend from tblorderfill where ofid='" & txtofid.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                orderfillinfoothers.txtstart.Text = dr("pstart").ToString
                orderfillinfoothers.txtend.Text = dr("pend").ToString
                orderfillinfoothers.txtfork.Text = dr("forklift").ToString
                orderfillinfoothers.txtsteve.Text = dr("stevedore").ToString
                orderfillinfoothers.txtpass.Text = dr("passnum").ToString
                orderfillinfoothers.txtmat.Text = dr("matdispo").ToString
                orderfillinfoothers.txtfgstart.Text = dr("fgstart").ToString
                orderfillinfoothers.txtfgend.Text = dr("fgend").ToString
                orderfillinfoothers.txtloadstart.Text = dr("loadstart").ToString
                orderfillinfoothers.txtloadend.Text = dr("loadend").ToString
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            orderfillinfoothers.ShowDialog()

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