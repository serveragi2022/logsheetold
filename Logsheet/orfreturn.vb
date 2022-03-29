Imports System.IO
Imports System.Data.SqlClient

Public Class orfreturn
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

    Private Sub orfreturn_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub orfreturn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public Sub view()
        Try
            grdreturn.Rows.Clear()

            sql = "Select * from tblofreturn where CAST(returndate AS DATE)='" & Format(datefrom.Value, "yyyy/MM/dd") & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim stat As String = ""
                If dr("status") = 1 Then
                    stat = "Completed"
                ElseIf dr("status") = 3 Then
                    stat = "Cancelled"
                End If

                grdreturn.Rows.Add(dr("retid"), dr("ofid"), dr("ofnum"), dr("ofitemid"), "", dr("oflogid"), dr("palletnum"), dr("returntype"), 0, "", dr("returndate"), dr("returnby"), dr("reason"), stat)

                If dr("status") = 3 Then
                    grdreturn.Rows(grdreturn.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            viewseries()

            'itemname
            For Each row As DataGridViewRow In grdreturn.Rows
                Dim ofitemid As Integer = grdreturn.Rows(row.Index).Cells(3).Value

                sql = "Select * from tblofitem where ofitemid='" & ofitemid & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdreturn.Rows(row.Index).Cells(4).Value = dr("itemname")
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

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

    Public Sub viewseries()
        Try
            grdtickets.Rows.Clear()
            For Each row As DataGridViewRow In grdreturn.Rows
                Dim retid As Integer = grdreturn.Rows(row.Index).Cells(0).Value
                Dim palnum As String = grdreturn.Rows(row.Index).Cells(6).Value
                Dim ctr As Integer = 0
                sql = "Select * from tblofloggood where palletnum='" & palnum & "' and retid='" & retid & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    ctr += 1
                    grdtickets.Rows.Add(dr("oflogid"), dr("ofloggoodid"), dr("logsheetnum"), dr("palletnum"), dr("letter") & " " & dr("gticketnum"), dr("gticketnum"), dr("letter"), retid)
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'no of available bags
                grdreturn.Rows(row.Index).Cells(8).Value = ctr
            Next

            generateseries()

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

    Public Sub generateseries()
        Try
            For Each rowlog As DataGridViewRow In grdreturn.Rows
                list1.Items.Clear()
                list2.Items.Clear()

                Dim pallettagnum As String = grdreturn.Rows(rowlog.Index).Cells(6).Value
                Dim retid As Integer = grdreturn.Rows(rowlog.Index).Cells(0).Value
                '/MsgBox(selectedrow)

                Dim table = New DataTable()
                table.Columns.Add("LetterNum", GetType(String))
                table.Columns.Add("Number", GetType(Integer))
                table.Columns.Add("Letter", GetType(Char))

                For Each rowtic As DataGridViewRow In grdtickets.Rows
                    If grdtickets.Rows(rowtic.Index).Cells(3).Value = pallettagnum And grdtickets.Rows(rowtic.Index).Cells(7).Value = retid Then
                        If grdtickets.Rows(rowtic.Index).Cells(5).Value.ToString.Contains("D") = True Then
                            'double
                            list2.Items.Add(grdtickets.Rows(rowtic.Index).Cells(6).Value & " " & grdtickets.Rows(rowtic.Index).Cells(5).Value)
                        Else
                            'good
                            '/list1.Items.Add(grdselected.Rows(rowtic.Index).Cells(3).Value & " " & grdselected.Rows(rowtic.Index).Cells(4).Value)
                            '/grdgoods.Rows.Add(grdselected.Rows(rowtic.Index).Cells(3).Value & " " & grdselected.Rows(rowtic.Index).Cells(4).Value, grdselected.Rows(rowtic.Index).Cells(3).Value, grdselected.Rows(rowtic.Index).Cells(4).Value)
                            table.Rows.Add(grdtickets.Rows(rowtic.Index).Cells(5).Value & " " & grdtickets.Rows(rowtic.Index).Cells(6).Value, grdtickets.Rows(rowtic.Index).Cells(5).Value, grdtickets.Rows(rowtic.Index).Cells(6).Value)
                        End If
                    End If
                Next

                '////grdgoods.Sort(grdgoods.Columns(1), System.ComponentModel.ListSortDirection.Ascending)

                '//apply sort on Letter, then Number column
                table.DefaultView.Sort = "Letter, Number"

                grdgoods.Columns.Clear()
                Me.grdgoods.DataSource = table

                For Each row As DataGridViewRow In grdgoods.Rows
                    list1.Items.Add(grdgoods.Rows(row.Index).Cells(0).Value)
                Next

                'generate series
                Dim ctrlist As Integer = 0
                Dim temp As String = "", first As String = "", last As String = ""
                Dim letter1 As String = "", letter2 As String = ""

                For Each item As Object In list1.Items
                    ctrlist += 1

                    If first = "" Then
                        letter1 = item
                        letter1 = letter1.Substring(letter1.Length - 1)

                        first = Val(item)
                    End If

                    letter2 = item
                    letter2 = letter2.Substring(letter2.Length - 1)

                    temp = Val(item)
                    last = temp

                    If list1.Items.Count >= ctrlist Then
                        If ctrlist > list1.Items.Count - 1 Then
                            If first = temp Then
                                Dim gtiket As String = first
                                Dim tempzero As String = ""

                                If gtiket < 1000000 Then
                                    For vv As Integer = 1 To 6 - gtiket.Length
                                        '/tempzero += "0"
                                    Next
                                End If


                                list2.Items.Add(letter1 & " " & tempzero & gtiket)
                                '/MsgBox(" ---1")
                                last = gtiket

                            Else
                                Dim gtiket As String = first
                                Dim tempzero As String = ""
                                If gtiket < 1000000 Then
                                    For vv As Integer = 1 To 6 - gtiket.Length
                                        '/tempzero += "0"
                                    Next
                                End If


                                Dim gtikettemp As String = temp
                                Dim tempzerotemp As String = ""
                                If gtikettemp < 1000000 Then
                                    For vv As Integer = 1 To 6 - gtikettemp.Length
                                        '/tempzerotemp += "0"
                                    Next
                                End If


                                list2.Items.Add(letter1 & " " & tempzero & gtiket & " - " & letter2 & " " & tempzerotemp & gtikettemp)
                                '/MsgBox("---2")
                                last = gtikettemp
                            End If
                        Else
                            If temp + 1 < Val(list1.Items(ctrlist)) Then
                                If first = temp Then
                                    Dim gtiket As String = first
                                    Dim tempzero As String = ""

                                    If gtiket < 1000000 Then
                                        For vv As Integer = 1 To 6 - gtiket.Length
                                            '/tempzero += "0"
                                        Next
                                    End If


                                    list2.Items.Add(letter1 & " " & tempzero & gtiket)
                                    '/MsgBox(" ---3")
                                    last = gtiket
                                Else
                                    Dim gtiket As String = first
                                    Dim tempzero As String = ""
                                    If gtiket < 1000000 Then
                                        For vv As Integer = 1 To 6 - gtiket.Length
                                            '/tempzero += "0"
                                        Next
                                    End If


                                    Dim gtikettemp As String = temp
                                    Dim tempzerotemp As String = ""
                                    If gtikettemp < 1000000 Then
                                        For vv As Integer = 1 To 6 - gtikettemp.Length
                                            '/tempzerotemp += "0"
                                        Next
                                    End If


                                    list2.Items.Add(letter1 & " " & tempzero & gtiket & " - " & letter2 & " " & tempzerotemp & gtikettemp)
                                    '/MsgBox(" ---4")
                                    last = gtikettemp
                                End If
                                first = ""

                            ElseIf temp + 1 > Val(list1.Items(ctrlist)) Then
                                'next series na ibig sabihin mag kaiba na letter nito
                                '/MsgBox(temp + 1 & " " & Val(list1.Items(ctrlist)))

                                Dim gtiket As String = first
                                Dim tempzero As String = ""
                                If gtiket < 1000000 Then
                                    For vv As Integer = 1 To 6 - gtiket.Length
                                        '/tempzero += "0"
                                    Next
                                End If


                                Dim gtiketlast As String = Val(last)
                                Dim tempzerotemp As String = ""
                                If gtiketlast < 1000000 Then
                                    For vv As Integer = 1 To 6 - gtiketlast.Length
                                        '/tempzerotemp += "0"
                                    Next
                                End If

                                list2.Items.Add(letter1 & " " & tempzero & gtiket & " - " & letter2 & " " & tempzerotemp & gtiketlast)
                                '/MsgBox(" ---5")
                                first = ""
                            End If
                        End If
                    End If
                Next


                Dim ofseries As String = ""
                For Each item As Object In list2.Items
                    ofseries = ofseries & item & ", "
                Next
                grdreturn.Item(9, rowlog.Index).Value = ofseries

            Next

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

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            view()

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

    Private Sub grdreturn_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdreturn.CellContentClick
        Try
            Me.Cursor = Cursors.WaitCursor

            'link
            If e.ColumnIndex = 2 And e.RowIndex > -1 Then

                Dim cell As DataGridViewCell = grdreturn.Rows(e.RowIndex).Cells(e.ColumnIndex)
                grdreturn.CurrentCell = cell
                ' Me.ContextMenuStrip2.Show(Cursor.Position)
                If grdreturn.RowCount <> 0 Then
                    If grdreturn.Item(2, grdreturn.CurrentRow.Index).Value IsNot Nothing Then
                        'view laman ng orderfill
                        'grdlogsheet tska grdcancel
                        orderfillinfo.txtofid.Text = grdreturn.Item(1, grdreturn.CurrentRow.Index).Value
                        orderfillinfo.ShowDialog()
                    End If
                End If
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