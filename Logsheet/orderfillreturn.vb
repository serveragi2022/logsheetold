Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class orderfillreturn
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public retreason As String = "", returnedby As String = "", ofretcnf As Boolean = False
    Public selectedrow As Integer

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

    Private Sub orderfillreturn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbitem.Items.Clear()
        cmbitem.Text = ""
        txtofitemid.Text = ""
        grdlog.Rows.Clear()
        grdlog.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        viewitems()
    End Sub

    Public Sub viewitems()
        Try
            cmbitem.Items.Clear()

            sql = "Select * from tblofitem where ofid='" & Trim(txtofid.Text) & "' and status='2' order by itemname"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbitem.Items.Add(dr("itemname"))
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

    Private Sub cmbitem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbitem.SelectedIndexChanged
        Try
            sql = "Select * from tblofitem where ofid='" & txtofid.Text & "' and itemname='" & Trim(cmbitem.Text) & "' and status='2'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                txtofitemid.Text = dr("ofitemid")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            selectitem()

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

    Public Sub selectitem()
        Try
            Me.Cursor = Cursors.WaitCursor

            'view tbloflog
            grdlog.Rows.Clear()

            sql = "Select tbllogticket.letter1, tbllogticket.letter4, tbllogticket.astart, tbllogticket.fend, tbloflog.oflogid, tbloflog.logsheetdate, tbloflog.logsheetnum, tbloflog.logticketid, tbloflog.palletnum, tbloflog.selectedbags, tbloflog.ticketseries"
            sql = sql & " from tbloflog left outer join tbllogticket on tbloflog.logticketid=tbllogticket.logticketid"
            sql = sql & " where tbloflog.ofitemid='" & txtofitemid.Text & "' and tbloflog.status<>'3' order by tbloflog.logsheetdate, tbloflog.logsheetnum, tbloflog.palletnum"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                grdlog.Rows.Add(dr("oflogid"), Format(dr("logsheetdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("logticketid"), dr("palletnum"), dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), dr("selectedbags"), dr("ticketseries"), False, "", "")
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            grdtickets.Rows.Clear()
            For Each row As DataGridViewRow In grdlog.Rows
                Dim oflogid As Integer = grdlog.Rows(row.Index).Cells(0).Value
                Dim ctr As Integer = 0
                sql = "Select * from tblofloggood where oflogid='" & oflogid & "' and status='1'"
                conn.Open()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    ctr += 1
                    grdtickets.Rows.Add(dr("oflogid"), dr("ofloggoodid"), dr("logsheetnum"), dr("palletnum"), dr("letter") & " " & dr("gticketnum"), dr("gticketnum"), dr("letter"), 0)
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'no of available bags
                grdlog.Rows(row.Index).Cells(7).Value = ctr
            Next

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

    Private Sub btnreturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreturn.Click
        Try
            'Admin Dispatching and All na department lang ang pwede
            If login.depart <> "All" And login.depart <> "Admin Dispatching" Then
                Me.Cursor = Cursors.Default
                MsgBox("Access denied!", MsgBoxStyle.Critical, "")
                Exit Sub
            End If


            'check if may selected na na item
            If Trim(txtofitemid.Text) = "" Then
                MsgBox("Select item name first.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            'check if may hindi pa na return sa tblofloggood
            Dim meronpa As Boolean = False

            sql = "Select * from tblofloggood where ofid='" & Trim(txtofid.Text) & "' and ofitemid='" & Trim(txtofitemid.Text) & "' and status='1'"
            conn.Open()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                meronpa = True
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If meronpa = False Then
                MsgBox("All items are already returned.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            'MsgBox("Input reason")
            retreason = ""
            orderfillretreason.txtreason.Text = ""
            orderfillretreason.ShowDialog()
            If retreason = "" Then
                Exit Sub
            End If


            'MsgBox("confirmation password")
            ofretcnf = False
            confirmsupadmin.ShowDialog()
            If ofretcnf = False Then
                Exit Sub
            End If

            If rball.Checked = True Then '''''''''''''''''''''''''''''''''''''''''''''ALL ITEMS'''''''''''''''''''''''''''''''''''''''
                Me.Cursor = Cursors.WaitCursor
                For Each row As DataGridViewRow In grdlog.Rows
                    Dim orflogid As Integer = grdlog.Rows(row.Index).Cells(0).Value
                    Dim lognum As String = grdlog.Rows(row.Index).Cells(2).Value
                    Dim palletid As Integer = grdlog.Rows(row.Index).Cells(3).Value
                    Dim palletnum As String = grdlog.Rows(row.Index).Cells(4).Value

                    'update tblloggood
                    sql = "update tblloggood set status=1 where status<>'1' and logticketid='" & palletid & "' and ofid='" & Trim(txtofid.Text) & "'"  '1 para mag available ulet
                    conn.Open()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    'insert into tblofreturn
                    sql = "Insert into tblofreturn (ofid, ofnum, ofitemid, oflogid, logsheetnum, logticketid, palletnum, returntype, returndate, returnby, reason, status) values ('" & Trim(txtofid.Text) & "', '" & Trim(txtofnum.Text) & "', '" & Trim(txtofitemid.Text) & "', '" & orflogid & "', '" & lognum & "', '" & palletid & "', '" & palletnum & "', '" & rball.Text & "', GetDate(), '" & returnedby & "', '" & retreason & "', '1')"
                    conn.Open()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    'update tblofloggood
                    sql = "Update tblofloggood set status=0, retid=(Select Top 1 retid from tblofreturn where status<>'0' and ofid='" & Trim(txtofid.Text) & "' and ofitemid='" & Trim(txtofitemid.Text) & "' and logticketid='" & palletid & "' order by retid DESC) where oflogid='" & orflogid & "' and ofid='" & Trim(txtofid.Text) & "' and status='1'"
                    conn.Open()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()
                Next

                Me.Cursor = Cursors.Default
                MsgBox("Successfully returned.", MsgBoxStyle.Information, "")

                'refresh selected
                selectitem()

            Else '''''''''''''''''''''''''''''''''''''''''''''SOME ITEMS'''''''''''''''''''''''''''''''''''''''
                Dim okreturn As Boolean = False

                Me.Cursor = Cursors.WaitCursor
                For Each row As DataGridViewRow In grdlog.Rows
                    Dim orflogid As Integer = grdlog.Rows(row.Index).Cells(0).Value
                    Dim lognum As String = grdlog.Rows(row.Index).Cells(2).Value
                    Dim palletid As Integer = grdlog.Rows(row.Index).Cells(3).Value
                    Dim palletnum As String = grdlog.Rows(row.Index).Cells(4).Value
                    Dim nobags As Integer = grdlog.Rows(row.Index).Cells(7).Value
                    Dim checkCell As DataGridViewCheckBoxCell = CType(grdlog.Rows(row.Index).Cells(9), DataGridViewCheckBoxCell)
                    Dim selbags As Integer = Val(grdlog.Rows(row.Index).Cells(10).Value)

                    If checkCell.Value = True Then
                        okreturn = True
                        If selbags < nobags Then
                            'mas konti yung sinelect kesa sa available na pwede pa i return
                            'insert into tblofreturn
                            sql = "Insert into tblofreturn (ofid, ofnum, ofitemid, oflogid, logsheetnum, logticketid, palletnum, returntype, returndate, returnby, reason, status) values ('" & Trim(txtofid.Text) & "', '" & Trim(txtofnum.Text) & "', '" & Trim(txtofitemid.Text) & "', '" & orflogid & "', '" & lognum & "', '" & palletid & "', '" & palletnum & "', '" & rbsome.Text & "', GetDate(), '" & returnedby & "', '" & retreason & "', '1')"
                            conn.Open()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            For Each rowtic As DataGridViewRow In grdtickets.Rows
                                Dim oflogtick As Integer = grdtickets.Rows(rowtic.Index).Cells(0).Value
                                Dim number As Integer = grdtickets.Rows(rowtic.Index).Cells(5).Value
                                Dim letter As String = grdtickets.Rows(rowtic.Index).Cells(6).Value
                                Dim picked As Integer = grdtickets.Rows(rowtic.Index).Cells(7).Value

                                If orflogid = oflogtick And picked = 1 Then
                                    'update tblloggood
                                    sql = "update tblloggood set status=1 where letter='" & letter & "' and gticketnum='" & number & "' and  status<>'1' and logticketid='" & palletid & "' and ofid='" & Trim(txtofid.Text) & "'"  '1 para mag available ulet
                                    conn.Open()
                                    cmd = New SqlCommand(sql, conn)
                                    cmd.ExecuteNonQuery()
                                    cmd.Dispose()
                                    conn.Close()

                                    'update tblofloggood
                                    sql = "Update tblofloggood set status=0, retid=(Select Top 1 retid from tblofreturn where status<>'0' and ofid='" & Trim(txtofid.Text) & "' and ofitemid='" & Trim(txtofitemid.Text) & "' and logticketid='" & palletid & "' order by retid DESC) where letter='" & letter & "' and gticketnum='" & number & "' and oflogid='" & orflogid & "' and ofid='" & Trim(txtofid.Text) & "' and status='1'"
                                    conn.Open()
                                    cmd = New SqlCommand(sql, conn)
                                    cmd.ExecuteNonQuery()
                                    cmd.Dispose()
                                    conn.Close()
                                End If
                            Next

                        ElseIf selbags = nobags Then
                            'equal na ung available sa selected
                            'update tblloggood
                            sql = "update tblloggood set status=1 where status<>'1' and logticketid='" & palletid & "' and ofid='" & Trim(txtofid.Text) & "'"  '1 para mag available ulet
                            conn.Open()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            'insert into tblofreturn
                            sql = "Insert into tblofreturn (ofid, ofnum, ofitemid, oflogid, logsheetnum, logticketid, palletnum, returntype, returndate, returnby, reason, status) values ('" & Trim(txtofid.Text) & "', '" & Trim(txtofnum.Text) & "', '" & Trim(txtofitemid.Text) & "', '" & orflogid & "', '" & lognum & "', '" & palletid & "', '" & palletnum & "', '" & rbsome.Text & "', GetDate(), '" & returnedby & "', '" & retreason & "', '1')"
                            conn.Open()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            'update tblofloggood
                            sql = "Update tblofloggood set status=0, retid=(Select Top 1 retid from tblofreturn where status<>'0' and ofid='" & Trim(txtofid.Text) & "' and ofitemid='" & Trim(txtofitemid.Text) & "' and logticketid='" & palletid & "' order by retid DESC) where oflogid='" & orflogid & "' and ofid='" & Trim(txtofid.Text) & "' and status='1'"
                            conn.Open()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()
                        End If
                    End If
                Next

                Me.Cursor = Cursors.Default

                If okreturn = True Then
                    Me.Cursor = Cursors.Default
                    MsgBox("Successfully returned.", MsgBoxStyle.Information, "")

                    'refresh selected
                    selectitem()
                End If
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

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            If Trim(txtlet.Text) <> "" And Trim(txtticket.Text) <> "" Then
                grdlog.ClearSelection()
                For Each row As DataGridViewRow In grdtickets.Rows
                    If grdtickets.Rows(row.Index).Cells(4).Value = Trim(txtlet.Text) & " " & Trim(txtticket.Text) Then
                        For Each rowlog As DataGridViewRow In grdlog.Rows
                            If grdlog.Rows(rowlog.Index).Cells(0).Value = grdtickets.Rows(row.Index).Cells(0).Value Then
                                grdlog.CurrentCell = grdlog.Rows(rowlog.Index).Cells(2)
                                grdlog.Rows(rowlog.Index).Selected = True
                                Exit For
                                Exit For
                            End If
                        Next
                    End If
                Next
            Else
                MsgBox("Incomplete ticket number.", MsgBoxStyle.Exclamation, "")
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

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        cmbitem.Items.Clear()
        cmbitem.Text = ""
        txtofitemid.Text = ""
        grdlog.Rows.Clear()
    End Sub

    Public Sub generateseries()
        Try
            '/For Each rowlog As DataGridViewRow In grdlog.Rows
            list1.Items.Clear()
            list2.Items.Clear()

            Dim pallettagnum As String = grdlog.Rows(selectedrow).Cells(4).Value
            Dim checkCell As DataGridViewCheckBoxCell = CType(grdlog.Rows(selectedrow).Cells(9), DataGridViewCheckBoxCell)
            '/MsgBox(selectedrow)

            If checkCell.Value = True Then
                Dim table = New DataTable()
                table.Columns.Add("LetterNum", GetType(String))
                table.Columns.Add("Number", GetType(Integer))
                table.Columns.Add("Letter", GetType(Char))

                For Each rowtic As DataGridViewRow In grdtickets.Rows
                    If grdtickets.Rows(rowtic.Index).Cells(3).Value = pallettagnum And grdtickets.Rows(rowtic.Index).Cells(7).Value = 1 Then
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
                grdlog.Item(11, selectedrow).Value = ofseries

            ElseIf checkCell.Value = False Then
                '///////////////////////////////////////////////////////////////////////////////////////////
                grdlog.Item(10, selectedrow).Value = ""
                grdlog.Item(11, selectedrow).Value = ""

                'ibalik sa 1 yung picked na mga ticket where pallettag = pallettagnum
                For Each rowtic As DataGridViewRow In grdtickets.Rows
                    If grdtickets.Rows(rowtic.Index).Cells(3).Value = pallettagnum And grdtickets.Rows(rowtic.Index).Cells(7).Value = 0 Then
                        grdtickets.Rows(rowtic.Index).Cells(7).Value = 1
                    End If
                Next
            End If
            '/Next

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

    Private Sub SelectTicketToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectTicketToolStripMenuItem.Click
        Try
            orderfillretticks.lblpalletid.Text = grdlog.Rows(selectedrow).Cells(3).Value
            orderfillretticks.txtpallet.Text = grdlog.Rows(selectedrow).Cells(4).Value
            orderfillretticks.ShowDialog()
            generateseries()

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

    Private Sub grdlog_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdlog.CellContentClick
        Try
            selectedrow = e.RowIndex
            'check kung may chineck nga sa grdlog then ska lng sya pde i enabled

            If e.ColumnIndex = 10 Then
                Exit Sub
                Dim checkCell As DataGridViewCheckBoxCell = CType(grdlog.Rows(selectedrow).Cells(9), DataGridViewCheckBoxCell)
                '/Button1.PerformClick()
                If checkCell.Value = False Then
                    checkCell.Value = True
                ElseIf checkCell.Value = True Then
                    Button1.PerformClick()
                    Dim a As String = MsgBox("Are you sure you want to cancel picked tickets?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                    If a = vbYes Then
                        checkCell.Value = False
                    End If
                End If
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub grdlog_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdlog.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then
                If rbsome.Checked = True Then
                    grdlog.ClearSelection()
                    grdlog.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True

                    selectedrow = e.RowIndex

                    '/If btncomplete.Text = "Selected Item Complete" Then
                    Me.ContextMenuStrip1.Show(Cursor.Position)
                    '/End If
                End If
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

    Private Sub txtlet_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtlet.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtlet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlet.TextChanged

    End Sub

    Private Sub txtticket_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtticket.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtticket_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtticket.TextChanged

    End Sub
End Class