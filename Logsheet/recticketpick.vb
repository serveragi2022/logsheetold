Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class recticketpick
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim AscendingListBox As New List(Of String)

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

    Private Sub orderfilltickets_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewtickets()
    End Sub

    Public Sub viewtickets()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim rdb As CheckBox
            Dim wid As Int32, widlbl As Int32
            Dim temp As Integer = 0, y As Integer, mody As Integer, row As Integer

            viewpanel.Visible = False
            viewpanel.Controls.Clear()
            viewpanel.Visible = True

            Dim lettr As String = ""
            For Each item As Object In recticket.list7.Items
                lettr = item
                lettr = lettr.Substring(lettr.Length - 1)

                temp = temp + 1
                mody = temp Mod 10
                row = temp / 5

                If mody = 1 Then
                    y = (row * 50) + (30 * row) + 10
                    wid = 0
                    widlbl = 0
                End If

                rdb = New CheckBox
                '/picstp1.Image = bitmap
                '/ms.Dispose()
                rdb.Text = lettr & " " & item.ToString.Substring(0, item.ToString.Length - 2)
                rdb.Tag = item
                rdb.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Regular)
                rdb.ForeColor = Color.Blue

                For Each selected As Object In recticket.list8.Items
                    If rdb.Tag = selected Then
                        rdb.Checked = False
                        rdb.Enabled = False
                    End If
                Next

                For Each selected As Object In recticket.list1.Items
                    If rdb.Tag = selected Then
                        rdb.Checked = False
                        rdb.Enabled = False
                    End If
                Next

                For Each selected As Object In recticket.list11.Items
                    If rdb.Tag = selected Then
                        rdb.Checked = False
                        rdb.Enabled = False
                    End If
                Next

                'For Each cnlrow As DataGridViewRow In orderfill.grdcancel.Rows
                '    If orderfill.grdtickets.Rows(ofrow.Index).Cells(0).Value = orderfill.grdcancel.Rows(cnlrow.Index).Cells(6).Value And orderfill.grdcancel.Rows(cnlrow.Index).Cells(1).Value = txtpallet.Text Then
                '        rdb.Checked = False
                '        rdb.Enabled = False
                '    End If
                'Next

                rdb.SetBounds(y, wid, 145, 50)

                wid += 35

                viewpanel.Controls.Add(rdb)
                AddHandler rdb.CheckStateChanged, AddressOf rdbClicked
                AddHandler rdb.EnabledChanged, AddressOf rdbEnabled
            Next

            countchk()

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
            If list1.Items.Count <> 0 Then
                For Each item As Object In list1.Items
                    Dim lettr As String = item.ToString.Substring(item.ToString.Length - 1)
                    Dim valtick As Integer = Val(item.ToString)

                    recticket.grdgood.Rows.Add(Val(valtick), lettr, 0)
                    recticket.grdgood.Sort(recticket.grdgood.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
                Next

                recticket.list8.Items.Clear()
                For Each row As DataGridViewRow In recticket.grdgood.Rows
                    recticket.list8.Items.Add(recticket.grdgood.Rows(row.Index).Cells(0).Value & " " & recticket.grdgood.Rows(row.Index).Cells(1).Value)
                Next

                recticket.btngenerate.PerformClick()
            End If
            
            Me.Dispose()

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

    Public Sub countchk()
        Dim ctr As Integer = 0
        For Each item As Object In viewpanel.Controls
            If item.checked = True Then
                ctr += 1
            End If
        Next

        lblcount.Text = ctr
    End Sub

    Public Sub rdbClicked(ByVal sender As Object, ByVal e As EventArgs)
        Dim s As String = CStr(sender.text)
        Dim valticket As String = s.ToString.Substring(2, s.ToString.Length - 2) 'sender.text.ToString.Substring(2, )  '
        Dim letticket As String = s.ToString.Substring(0, 1)

        If sender.checked = True Then
            Me.Cursor = Cursors.WaitCursor

            Dim checkgood As Integer = 0, checkcancel As Integer = 0
            sql = "select Count(g.loggoodid) from tblloggood g right outer join tbllogsheet s on s.logsheetid=g.logsheetid"
            sql = sql & " where s.logsheetyear ='" & Val(recticket.lblyear.Text) & "' and g.tickettype='Receive' "
            sql = sql & " And s.logsheetnum ='" & recticket.lbltemp.Text & recticket.txtsearch.Text & "' and s.branch='" & login.branch & "' "
            sql = sql & " And g.letter ='" & letticket & "' and g.gticketnum='" & valticket & "' and g.status<>'3'"
            connect()
            cmd = New SqlCommand(sql, conn)
            checkgood = cmd.ExecuteScalar
            cmd.Dispose()
            conn.Close()

            sql = "select Count(c.logcancelid) from tbllogcancel c right outer join tbllogsheet s on s.logsheetid=c.logsheetid"
            sql = sql & " where s.logsheetyear ='" & Val(recticket.lblyear.Text) & "' and c.tickettype='Receive' "
            sql = sql & " And s.logsheetnum ='" & recticket.lbltemp.Text & recticket.txtsearch.Text & "' and s.branch='" & login.branch & "' "
            sql = sql & " And c.letter ='" & letticket & "' and c.cticketnum='" & valticket & "' and c.status<>'3'"
            connect()
            cmd = New SqlCommand(sql, conn)
            checkcancel = cmd.ExecuteScalar
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.Default
            If checkgood <> 0 Or checkcancel <> 0 Then
                sender.checked = False
                MsgBox("Ticket is already exist.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            Else
                list1.Items.Add(valticket & " " & letticket)
                sender.backcolor = Color.MistyRose
            End If
        Else
            For l_index As Integer = 0 To list1.Items.Count - 1
                Dim l_text As String = CStr(list1.Items(l_index))
                If valticket = Val(l_text) Then
                    list1.Items.RemoveAt(l_index)
                    sender.backcolor = Color.White
                    Exit For
                End If
            Next
        End If

        genticket()
        countchk()
    End Sub

    Public Sub rdbEnabled(ByVal sender As Object, ByVal e As EventArgs)
        If sender.enabled = False Then
            sender.forecolor = Color.Red
        Else
            sender.forecolor = Color.Black
        End If
    End Sub

    Public Sub genticket()
        Try
            'sort list of tickets
            If list1.Items.Count > 1 Then
                AscendingListBox.Clear()
                For i = 0 To list1.Items.Count - 1
                    AscendingListBox.Add(CStr((list1.Items(i))))
                Next
                AscendingListBox.Sort()
                list1.Items.Clear()
                For i = 0 To AscendingListBox.Count - 1
                    list1.Items.Add(AscendingListBox(i))
                Next
            End If

            'generate series
            list2.Items.Clear()
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

                            first = ""
                        End If
                    End If
                End If
            Next

            Dim ofseries As String = ""
            For Each item As Object In list2.Items
                ofseries = ofseries & item & ", "
            Next
            txtseries.Text = ofseries

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

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        For Each rdb As Object In viewpanel.Controls
            If rdb.enabled = True Then
                If Val(lblcount.Text) + 1 + recticket.grdgood.Rows.Count > Val(recticket.txtbags.Text) Then
                    MsgBox(Val(lblcount.Text) & " bags only.", MsgBoxStyle.Exclamation, "")
                    Exit Sub
                Else
                    rdb.checked = True
                End If
            End If
        Next
    End Sub

    Private Sub btnunselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnunselect.Click
        For Each rdb As Object In viewpanel.Controls
            If rdb.enabled = True Then
                rdb.checked = False
            End If
        Next
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        If Trim(txtlet.Text) = "" Then
            MsgBox("Input letter.", MsgBoxStyle.Exclamation, "")
            txtlet.Focus()
            Exit Sub
        ElseIf Trim(txtfind.Text) = "" Then
            MsgBox("Input ticket number.", MsgBoxStyle.Exclamation, "")
            txtfind.Focus()
            Exit Sub
        End If

        If Val(lblcount.Text) + 1 + recticket.grdgood.Rows.Count > Val(recticket.txtbags.Text) Then
            MsgBox(Val(lblcount.Text) & " bags only.", MsgBoxStyle.Exclamation, "")
            Exit Sub
        End If

        Dim meron As Boolean = False
        For Each rdb As Object In viewpanel.Controls
            If Trim(txtlet.Text) & " " & Trim(txtfind.Text) = rdb.text And rdb.enabled = True Then
                rdb.focus()
                rdb.checked = True
                meron = True
                Exit For
            End If
        Next

        If meron = False Then
            MsgBox("Invalid ticket number.", MsgBoxStyle.Exclamation, "")
            txtfind.Focus()
        End If
    End Sub

    Private Sub txtfind_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfind.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnsearch.PerformClick()
        End If
    End Sub

    Private Sub txtlet_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtlet.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

End Class