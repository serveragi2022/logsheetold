Imports System.IO
Imports System.Data.SqlClient

Public Class CopyorderfillpalletsNObgnd
    Dim lines = System.IO.File.ReadAllLines("connectionstring.txt")
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public orderfillitemid As String
    Dim selectedrow As Integer
    Dim clickbtn As String

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

    Private Sub orderfillpallets_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        txtletter.Text = ""
        txtpallet.Text = ""
        txtticket.Text = ""
        cmbloc.Text = ""
    End Sub

    Private Sub orderfillpallets_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        grdpallets.Columns(0).ReadOnly = True
        grdpallets.Columns(2).ReadOnly = True
        grdpallets.Columns(3).ReadOnly = True
        grdpallets.Columns(4).ReadOnly = True
        grdpallets.Columns(5).ReadOnly = True
        grdpallets.Columns(6).ReadOnly = True
        grdpallets.Columns(7).ReadOnly = True
        grdpallets.Columns(8).ReadOnly = True
        grdpallets.Columns(9).ReadOnly = True

        grdselect.Columns(0).ReadOnly = True
        grdselect.Columns(1).ReadOnly = True
        grdselect.Columns(2).ReadOnly = True
        grdselect.Columns(3).ReadOnly = True
        grdselect.Columns(4).ReadOnly = True
        grdselect.Columns(5).ReadOnly = True
        grdselect.Columns(6).ReadOnly = True
        grdselect.Columns(7).ReadOnly = True
    End Sub

    Private Sub orderfillpallets_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        viewlocation()
        btnsearch.PerformClick()
        viewselected()
    End Sub

    Public Sub viewlocation()
        Try
            cmbloc.Items.Clear()

            sql = "Select * from tbllocation where whsename='" & lblwhse.Text & "' and status='1' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbloc.Items.Add(dr("location"))
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

    Public Sub viewselected()
        Try
            Dim totalbags As Integer = 0

            If orderfill.grdlog.Rows.Count <> 0 Then
                'to gaya the number of selected bags
                grdselect.Rows.Clear()

                For Each rowlog As DataGridViewRow In orderfill.grdlog.Rows
                    Dim col0 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(0).Value
                    Dim col1 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(1).Value
                    Dim col2 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(2).Value
                    Dim col3 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(3).Value
                    Dim col4 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(7).Value
                    Dim col5 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(4).Value
                    Dim col6 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(5).Value
                    Dim col7 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(6).Value
                    Dim col8 As String = orderfill.grdlog.Rows(rowlog.Index).Cells(10).Value
                    Dim collogticketid As String = orderfill.grdlog.Rows(rowlog.Index).Cells(12).Value
                    Dim colpickticket As Boolean = orderfill.grdlog.Rows(rowlog.Index).Cells(13).Value

                    grdselect.Rows.Add(col0, col1, col2, col3, col4, col5, col6, col7, col8, collogticketid, colpickticket)

                    totalbags = totalbags + col8
                Next

            Else
                'kunin yung nasa db na nkaselect na
                grdselect.Rows.Clear()

                '/sql = "Select * from tbllogticket where NOT addtoloc is NULL and cusreserve='2' and ofnum='" & orderfill.lblorf.Text & orderfill.txtorf.Text & "'"
                sql = "Select * from tbllogticket left outer join tbllogitem on tbllogitem.logitemid=tbllogticket.logitemid"
                sql = sql & " where itemname='" & orderfill.cmbitem.Text & "' and NOT addtoloc is NULL and ofnum='" & orderfill.lblorf.Text & orderfill.txtorf.Text & "' and tbllogticket.status<>'3' and tbllogticket.branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                While dr.Read
                    grdselect.Rows.Add(dr("logticketid"), Format(dr("ticketdate"), "yyyy/MM/dd"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), 0)
                End While
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                Dim totalgood As Integer = 0, totaldouble As Integer = 0
                For Each row As DataGridViewRow In grdselect.Rows
                    'tblloggood no of available
                    sql = "Select Count(loggoodid) from tblloggood where palletnum='" & grdselect.Rows(row.Index).Cells(3).Value & "' and status='1' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    totalgood = cmd.ExecuteScalar
                    cmd.Dispose()
                    conn.Close()

                    'tbllogdouble no of available
                    sql = "Select Count(logdoubleid) from tbllogdouble where palletnum='" & grdselect.Rows(row.Index).Cells(3).Value & "' and status='1' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    totaldouble = cmd.ExecuteScalar
                    cmd.Dispose()
                    conn.Close()

                    grdselect.Item(4, row.Index).Value = totalgood + totaldouble
                    grdselect.Item(8, row.Index).Value = totalgood + totaldouble

                    totalbags = totalbags + grdselect.Item(8, row.Index).Value
                Next
            End If

            lbltotal.Text = totalbags.ToString("n2")

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

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            clickbtn = "Searched"

            If Trim(txtpallet.Text) <> "" Then
                searchpallet()
                Exit Sub
            ElseIf Trim(txtticket.Text) <> "" Or Trim(txtletter.Text) <> "" Then
                searchticket()
                Exit Sub
            End If

            cmbtemp.Items.Clear()
            grdpallets.Rows.Clear()

            sql = "Select tbllogticket.qadispo, tbllogticket.palletnum, tbllogitem.itemname from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
            sql = sql & " full outer join tblloggood on tbllogticket.logticketid=tblloggood.logticketid full outer join tbllogdouble on tbllogticket.logticketid=tbllogdouble.logticketid"
            '/sql = sql & " where tbllogsheet.logsheetdate>='" & CDate(Format(datefrom.Value, "yyyy/MM/dd")) & "' and tbllogsheet.logsheetdate<='" & CDate(Format(dateto.Value, "yyyy/MM/dd")) & "' and tbllogsheet.status<>'3'"
            sql = sql & " where tbllogsheet.logsheetdate='" & CDate(Format(datefrom.Value, "yyyy/MM/dd")) & "' and tbllogsheet.status<>'3' and tbllogsheet.branch='" & login.branch & "'"
            sql = sql & " and tbllogticket.qadispo='1' and tbllogitem.itemname='" & orderfill.cmbitem.Text & "' and tbllogticket.status='1' and tbllogitem.status='2'"

            If Trim(cmbloc.Text) <> "" Then
                sql = sql & " and tbllogticket.location='" & Trim(cmbloc.Text) & "'"
            End If

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("qadispo") = 1 Then
                    Dim meronna As Boolean = False
                    For Each item As Object In cmbtemp.Items
                        If item = dr("palletnum") Then
                            meronna = True
                            Exit For
                        End If
                    Next
                    If meronna = False Then
                        cmbtemp.Items.Add(dr("palletnum"))
                    End If
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            For Each item As Object In cmbtemp.Items
                sql = "Select * from tbllogticket where palletnum='" & item & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If IsDBNull(dr("addtoloc")) = False Then
                        Dim stat As String = ""
                        If dr("cusreserve") = 0 Then
                            stat = "Available"
                            grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                        ElseIf dr("cusreserve") = 1 Then
                            stat = "Reserved"
                            If dr("customer") = orderfill.txtcus.Text And dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                                grdpallets.Rows(grdpallets.Rows.Count - 1).Cells(1).ReadOnly = True
                            ElseIf dr("customer") = orderfill.txtcus.Text Then
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.NavajoWhite
                            Else
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                            End If
                        Else
                            stat = "Selected"
                            If dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                                grdpallets.Rows(grdpallets.Rows.Count - 1).Cells(1).ReadOnly = True
                            Else
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.NavajoWhite
                            End If
                        End If
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            countnumbags()

            'add remarks ni prod 13
            For Each row As DataGridViewRow In grdpallets.Rows
                Dim lognum As String = grdpallets.Rows(row.Index).Cells(4).Value
                sql = "Select * from tbllogsheet where logsheetnum='" & lognum & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdpallets.Rows(row.Index).Cells(13).Value = dr("prodrems").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next


            If grdpallets.Rows.Count = 0 Then
                '/MsgBox("No record found.", MsgBoxStyle.Critical, "")
            End If

            gpallets.Text = "Searched Pallets"

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

    Private Sub grdpallets_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpallets.CellContentClick
        Try
            If grdpallets.CurrentCell.ColumnIndex = 1 Then
                Dim checkselect As DataGridViewCheckBoxCell = CType(grdpallets.Rows(grdpallets.CurrentRow.Index).Cells(1), DataGridViewCheckBoxCell)
                If grdpallets.Rows(e.RowIndex).Cells(11).Value.ToString <> orderfill.lblorf.Text & orderfill.txtorf.Text Then
                    Button1.PerformClick()

                    'check sa db wag sa grd lng
                    sql = "Select * from tbllogticket where palletnum='" & grdpallets.Rows(e.RowIndex).Cells(5).Value & "' and cusreserve<>'0' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        'check if may dispo na ni QA
                        If dr("qadispo") <> 1 Then
                            'cannot proceed
                            If dr("qadispo") = 2 Then
                                MsgBox("Pallet tag # " & grdpallets.Rows(e.RowIndex).Cells(5).Value & " is in HOLD status.", MsgBoxStyle.Exclamation, "")
                                checkselect.Value = False
                            Else
                                MsgBox("Pallet tag # " & grdpallets.Rows(e.RowIndex).Cells(5).Value & " is still PENDING for QA disposition.", MsgBoxStyle.Exclamation, "")
                                checkselect.Value = False
                            End If

                        Else
                            If dr("cusreserve") = 1 Then
                                grdpallets.Rows(e.RowIndex).Cells(10).Value = "Reserved"
                                If dr("customer") = orderfill.txtcus.Text Then
                                    grdpallets.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Yellow
                                Else
                                    grdpallets.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                                    MsgBox("Cannot pick reserved pallet tag # " & grdpallets.Rows(e.RowIndex).Cells(5).Value & ".", MsgBoxStyle.Exclamation, "")
                                    checkselect.Value = False
                                End If
                            ElseIf dr("cusreserve") = 2 Then
                                grdpallets.Rows(e.RowIndex).Cells(10).Value = "Selected"
                                grdpallets.Rows(e.RowIndex).Cells(11).Value = dr("ofnum")
                                grdpallets.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.NavajoWhite
                                MsgBox("Cannot pick selected pallet tag # " & grdpallets.Rows(e.RowIndex).Cells(5).Value & ".", MsgBoxStyle.Exclamation, "")
                                checkselect.Value = False
                            End If
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
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

    Private Sub grdpallets_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdpallets.CellMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right And e.RowIndex > -1 Then

                grdpallets.ClearSelection()
                grdpallets.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True

                selectedrow = e.RowIndex
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

    Private Sub grdpallets_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdpallets.CellValueChanged
        Try
            If grdpallets.CurrentCell.ColumnIndex = 1 Then
                Dim checkselect As DataGridViewCheckBoxCell = CType(grdpallets.Rows(grdpallets.CurrentRow.Index).Cells(1), DataGridViewCheckBoxCell)
                Button1.PerformClick()

                grdpallets.Invalidate()
            End If

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MsgBox(ex.tostring, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        Try
            If grdpallets.Rows.Count <> 0 Then
                'check if orderfill is not cancelled
                sql = "Select * from tblorderfill where ofid='" & orderfill.lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if ofitem is not cancelled
                sql = "Select * from tblofitem where ofitemid='" & orderfill.txtofitemid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Item is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Item is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if may nauna ng nag reserve or selected

                For Each rowlist As DataGridViewRow In grdpallets.Rows
                    Dim checkselect As DataGridViewCheckBoxCell = CType(grdpallets.Rows(rowlist.Index).Cells(1), DataGridViewCheckBoxCell)
                    If checkselect.Value = True Then
                        '/MsgBox("check if may nauna ng nag reserve or selected")
                        sql = "Select * from tbllogticket where palletnum='" & grdpallets.Rows(rowlist.Index).Cells(5).Value & "' and cusreserve<>'0' and branch='" & login.branch & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            If dr("cusreserve") = 1 Then
                                grdpallets.Rows(rowlist.Index).Cells(10).Value = "Reserved"
                                If dr("customer") = orderfill.txtcus.Text And dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.Yellow
                                    checkselect.Value = True
                                    grdpallets.Rows(rowlist.Index).Cells(1).ReadOnly = True
                                ElseIf dr("customer") <> orderfill.txtcus.Text Then
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                                    MsgBox("Cannot select reserved pallets.", MsgBoxStyle.Exclamation, "")
                                    checkselect.Value = False
                                End If

                            ElseIf dr("cusreserve") = 2 Then
                                grdpallets.Rows(rowlist.Index).Cells(10).Value = "Selected"
                                grdpallets.Rows(rowlist.Index).Cells(11).Value = dr("ofnum")
                                If dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.Yellow
                                    checkselect.Value = True
                                    grdpallets.Rows(rowlist.Index).Cells(1).ReadOnly = True
                                Else
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.NavajoWhite
                                    MsgBox("Cannot select already selected pallets.", MsgBoxStyle.Exclamation, "")
                                    checkselect.Value = False
                                End If
                            End If
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()
                    End If
                Next


                '/MsgBox("dapat hindi na rereset.. idadagdag lang yung iadd")
                '/grdselect.Rows.Clear()
                Dim totalbags As Integer = 0
                'verified kaya ililipat nlng ng grd

                For Each rowlist As DataGridViewRow In grdpallets.Rows
                    Dim checkselect As DataGridViewCheckBoxCell = CType(grdpallets.Rows(rowlist.Index).Cells(1), DataGridViewCheckBoxCell)
                    If checkselect.Value = True Then
                        Dim meronna As Boolean = False
                        'check if nasa grd na yung palletnum
                        For Each rowpal As DataGridViewRow In grdselect.Rows
                            If grdselect.Rows(rowpal.Index).Cells(3).Value = grdpallets.Rows(rowlist.Index).Cells(5).Value Then
                                meronna = True
                                Exit For
                            End If
                        Next

                        If meronna = False Then
                            Dim col0 As Integer = grdpallets.Rows(rowlist.Index).Cells(0).Value
                            Dim col1 As Date = Format(CDate(grdpallets.Rows(rowlist.Index).Cells(2).Value), "yyyy/MM/dd")
                            Dim col2 As String = grdpallets.Rows(rowlist.Index).Cells(4).Value
                            Dim col3 As String = grdpallets.Rows(rowlist.Index).Cells(5).Value
                            Dim col4 As Integer = grdpallets.Rows(rowlist.Index).Cells(6).Value
                            Dim col5 As String = grdpallets.Rows(rowlist.Index).Cells(7).Value
                            '/Dim sticket As String = grdpallets.Rows(rowlist.Index).Cells(8).Value
                            '/Dim col6 As Integer = Val(sticket.Remove(0, 2))
                            Dim col6 As String = grdpallets.Rows(rowlist.Index).Cells(8).Value
                            Dim col7 As String = grdpallets.Rows(rowlist.Index).Cells(9).Value
                            Dim collogticketid As Integer = grdpallets.Rows(rowlist.Index).Cells(0).Value

                            'update cusreserve
                            If grdpallets.Rows(rowlist.Index).Cells(10).Value = "Reserved" Then
                                sql = "Update tbllogticket set ofid='" & orderfill.lblorfid.Text & "', ofnum='" & orderfill.lblorf.Text & orderfill.txtorf.Text & "' where palletnum='" & col3 & "' and branch='" & login.branch & "'"
                            Else
                                'selected only
                                sql = "Update tbllogticket set ofid='" & orderfill.lblorfid.Text & "', cusreserve='2', ofnum='" & orderfill.lblorf.Text & orderfill.txtorf.Text & "' where palletnum='" & col3 & "' and branch='" & login.branch & "'"
                            End If

                            connect()
                            cmd = New SqlCommand(sql, conn)
                            cmd.ExecuteNonQuery()
                            cmd.Dispose()
                            conn.Close()

                            'i add sa grdselect
                            grdselect.Rows.Add(col0, col1, col2, col3, col4, col5, col6, col7, col4, collogticketid)

                            totalbags = totalbags + col4
                        End If
                    End If
                Next


                lbltotal.Text = totalbags

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

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Try
            If grdselect.Rows.Count <> 0 Then
                'check if orderfill is not cancelled
                sql = "Select * from tblorderfill where ofid='" & orderfill.lblorfid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Order fill is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Order fill is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                'check if ofitem is not cancelled
                sql = "Select * from tblofitem where ofitemid='" & orderfill.txtofitemid.Text & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 3 Then
                        MsgBox("Item is already cancelled.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    ElseIf dr("status") = 2 Then
                        MsgBox("Item is already completed.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                Dim a As String
                If orderfill.grdlog.Rows.Count <> 0 Then
                    a = MsgBox("Are you sure you want to remove pallets? Removing pallets will reset your generated list.", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                Else
                    a = MsgBox("Are you sure you want to remove pallets?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                End If

                If a <> vbYes Then
                    Exit Sub
                End If

                'remove from grdselect
                Dim totalbags As Integer = Val(lbltotal.Text)
                For Each row As DataGridViewRow In grdselect.SelectedRows
                    Dim col3 As String = grdselect.Rows(row.Index).Cells(3).Value
                    Dim col4 As String = grdselect.Rows(row.Index).Cells(4).Value

                    'remove in grdtickets
                    For Each rowlog As DataGridViewRow In orderfill.grdtickets.SelectedRows
                        If orderfill.grdtickets.Rows(row.Index).Cells(3).Value = grdselect.Rows(row.Index).Cells(3).Value Then
                            Dim grdindex As Integer = row.Index
                            orderfill.grdtickets.Rows.Remove(row)
                            Exit For
                        End If
                    Next

                    'update cusreserve selected into not selected
                    sql = "Update tbllogticket set cusreserve='0', ofnum=NULL where palletnum='" & col3 & "' and cusreserve='2' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    'update cusreserve reserved into not selected
                    sql = "Update tbllogticket set ofnum=NULL where palletnum='" & col3 & "' and cusreserve='1' and branch='" & login.branch & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    'check kung meron dun sa temp nya para maremove din

                    totalbags = totalbags - col4

                    Dim listindex As Integer = row.Index
                    grdselect.Rows.Remove(row)
                Next


                lbltotal.Text = totalbags
                '/btnview.PerformClick()
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

    Public Sub checkcusreserve()
        Try
            If grdpallets.Rows.Count <> 0 Then
                'check if may nauna ng nag reserve or selected
                For Each rowlist As DataGridViewRow In grdpallets.Rows
                    Dim checkselect As DataGridViewCheckBoxCell = CType(grdpallets.Rows(rowlist.Index).Cells(1), DataGridViewCheckBoxCell)
                    If checkselect.Value = True Then
                        '/MsgBox("check if may nauna ng nag reserve or selected")
                        sql = "Select * from tbllogticket where palletnum='" & grdpallets.Rows(rowlist.Index).Cells(5).Value & "' and cusreserve<>'0' and branch='" & login.branch & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            If dr("cusreserve") = 1 Then
                                grdpallets.Rows(rowlist.Index).Cells(10).Value = "Reserved"
                                If dr("customer").ToString = orderfill.txtcus.Text And dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.Yellow
                                    checkselect.Value = True
                                    grdpallets.Rows(rowlist.Index).Cells(1).ReadOnly = True
                                ElseIf dr("customer").ToString = orderfill.txtcus.Text Then
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.Yellow
                                    checkselect.Value = True
                                    grdpallets.Rows(rowlist.Index).Cells(1).ReadOnly = True
                                Else
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                                    MsgBox("Cannot select reserved pallets.", MsgBoxStyle.Exclamation, "")
                                    checkselect.Value = False
                                End If
                            ElseIf dr("cusreserve") = 2 Then
                                grdpallets.Rows(rowlist.Index).Cells(10).Value = "Selected"
                                grdpallets.Rows(rowlist.Index).Cells(11).Value = dr("ofnum")
                                If dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.Yellow
                                    checkselect.Value = True
                                    grdpallets.Rows(rowlist.Index).Cells(1).ReadOnly = True
                                Else
                                    grdpallets.Rows(rowlist.Index).DefaultCellStyle.BackColor = Color.NavajoWhite
                                    MsgBox("Cannot select already selected pallets.", MsgBoxStyle.Exclamation, "")
                                    checkselect.Value = False
                                End If
                            End If
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()
                    End If
                Next
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

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Try
            '/MsgBox("check ulet ung status ng cusreserve nya tapos dito plng dapat i update tbl cusreserve nya")
            checkcusreserve()

            orderfill.grdlog.Rows.Clear()

            For Each row As DataGridViewRow In grdselect.Rows
                Dim colid As Integer = grdselect.Rows(row.Index).Cells(0).Value
                Dim coldate As Date = Format(CDate(grdselect.Rows(row.Index).Cells(1).Value), "yyyy/MM/dd")
                Dim collognum As String = grdselect.Rows(row.Index).Cells(2).Value
                Dim colpallet As String = grdselect.Rows(row.Index).Cells(3).Value
                Dim colavail As Integer = grdselect.Rows(row.Index).Cells(4).Value
                Dim colletter As String = grdselect.Rows(row.Index).Cells(5).Value
                Dim colstart As String = grdselect.Rows(row.Index).Cells(6).Value
                Dim colend As String = grdselect.Rows(row.Index).Cells(7).Value
                Dim colselect As Integer = grdselect.Rows(row.Index).Cells(8).Value
                Dim collogticketid As Integer = grdselect.Rows(row.Index).Cells(9).Value
                Dim colpicktickets As Boolean = grdselect.Rows(row.Index).Cells(10).Value


                'update cusreserve available to selected
                sql = "Update tbllogticket set cusreserve='2', ofnum='" & orderfill.lblorf.Text & orderfill.txtorf.Text & "' where palletnum='" & colpallet & "' and cusreserve='0' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()

                'update cusreserve reserve to selected ofnum only
                sql = "Update tbllogticket set ofnum='" & orderfill.lblorf.Text & orderfill.txtorf.Text & "' where palletnum='" & colpallet & "' and cusreserve='1' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                conn.Close()

                orderfill.grdlog.Rows.Add(colid, Format(coldate, "yyyy/MM/dd"), collognum, colpallet, colletter, colstart, colend, colavail, 0, colavail, colselect, "", collogticketid, colpicktickets)
            Next


            For r As Integer = orderfill.grdcancel.Rows.Count - 1 To 0 Step -1
                Dim pallettagnum As String = orderfill.grdcancel.Rows(r).Cells(1).Value
                Dim notingrdlog As Boolean = True
                For Each row As DataGridViewRow In orderfill.grdlog.Rows
                    Dim pall As String = orderfill.grdlog.Rows(row.Index).Cells(3).Value
                    If pallettagnum = pall Then
                        notingrdlog = False
                        Exit For
                    End If
                Next
                If notingrdlog Then orderfill.grdcancel.Rows.RemoveAt(r)
            Next


            Me.Close()

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

    Private Sub grdselect_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdselect.CellContentClick

    End Sub

    Private Sub grdselect_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdselect.CellEndEdit
        'check if numeric
        If (e.ColumnIndex = 8) Then   ' Checking numeric value for Column1 only
            If grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> Nothing Then
                Dim value As String = grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString()
                If Not Information.IsNumeric(value) Or Val(value) < 1 Or Val(value) > grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex - 4).Value Then
                    MsgBox("Invalid input.", MsgBoxStyle.Exclamation, "")
                    grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex - 4).Value
                    grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
                Else
                    grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Fix(grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                    grdselect.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Nothing
                End If

                Dim totalbags As Integer = 0
                For Each row As DataGridViewRow In grdselect.Rows
                    totalbags = totalbags + grdselect.Item(8, row.Index).Value
                Next

                lbltotal.Text = totalbags.ToString("n2")
            End If
        End If
    End Sub

    Private Sub btnview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnview.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            clickbtn = "View All"

            cmbtemp.Items.Clear()
            grdpallets.Rows.Clear()

            sql = "Select tbllogticket.palletnum, tbllogitem.itemname from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
            sql = sql & " full outer join tblloggood on tbllogticket.logticketid=tblloggood.logticketid full outer join tbllogdouble on tbllogticket.logticketid=tbllogdouble.logticketid"
            sql = sql & " where tbllogticket.qadispo='1' and tbllogitem.itemname='" & orderfill.cmbitem.Text & "' and tbllogitem.status='2' and tbllogticket.status='1' and tbllogsheet.status<>'3' and tbllogsheet.branch='" & login.branch & "'"

            '/sql = "Select * from tbllogticket left outer join tbllogitem on tbllogitem.logitemid=tbllogticket.logitemid where tbllogticket.qadispo='1' and tbllogticket.ticketwhse='" & lblwhse.Text & "'  and tbllogitem.itemname='" & orderfill.cmbitem.Text & "'"

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                Dim meronna As Boolean = False
                For Each item As Object In cmbtemp.Items
                    If item = dr("palletnum") Then
                        meronna = True
                        Exit For
                    End If
                Next
                If meronna = False Then
                    cmbtemp.Items.Add(dr("palletnum"))
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            For Each item As Object In cmbtemp.Items
                sql = "Select * from tbllogticket where palletnum='" & item & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If IsDBNull(dr("addtoloc")) = False Then
                        Dim stat As String = ""
                        If dr("cusreserve") = 0 Then
                            stat = "Available"
                            grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                        ElseIf dr("cusreserve") = 1 Then
                            stat = "Reserved"
                            If dr("customer") = orderfill.txtcus.Text And dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                                grdpallets.Rows(grdpallets.Rows.Count - 1).Cells(1).ReadOnly = True
                            ElseIf dr("customer") = orderfill.txtcus.Text Then
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.NavajoWhite
                            Else
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                            End If
                        Else
                            stat = "Selected"
                            If dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                                grdpallets.Rows(grdpallets.Rows.Count - 1).Cells(1).ReadOnly = True
                            Else
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.NavajoWhite
                            End If
                        End If
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            Me.Cursor = Cursors.Default

            countnumbags()

            'add remarks ni prod 13
            For Each row As DataGridViewRow In grdpallets.Rows
                Dim lognum As String = grdpallets.Rows(row.Index).Cells(4).Value
                sql = "Select * from tbllogsheet where logsheetnum='" & lognum & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdpallets.Rows(row.Index).Cells(13).Value = dr("prodrems").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            If grdpallets.Rows.Count = 0 Then
                '/MsgBox("No record found.", MsgBoxStyle.Critical, "")
            End If

            gpallets.Text = "View All Pallets"

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

    Private Sub txtpallet_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpallet.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            searchpallet()
        End If
    End Sub

    Private Sub txtpallet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpallet.TextChanged
        If Trim(txtpallet.Text) = "" Then
            txtticket.Enabled = True
            txtletter.Enabled = True
            '/btnview.PerformClick()
        Else
            txtticket.Text = ""
            txtletter.Text = ""
            txtticket.Enabled = False
            txtletter.Enabled = False
        End If
    End Sub

    Private Sub cmbloc_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbloc.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/btnadd.PerformClick()
        End If
    End Sub

    Private Sub cmbloc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbloc.SelectedIndexChanged

    End Sub

    Public Sub searchpallet()
        Try
            cmbtemp.Items.Clear()
            grdpallets.Rows.Clear()

            sql = "Select tbllogticket.location, tbllogticket.ticketwhse, tbllogticket.qadispo, tbllogticket.palletnum, tbllogitem.itemname, tbllogticket.status as ticketstatus, tbllogitem.status as itemstatus from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
            sql = sql & " full outer join tblloggood on tbllogticket.logticketid=tblloggood.logticketid full outer join tbllogdouble on tbllogticket.logticketid=tbllogdouble.logticketid"
            sql = sql & " where tbllogticket.palletnum='" & Trim(txtpallet.Text) & "' and tbllogitem.itemname='" & orderfill.cmbitem.Text & "'"
            sql = sql & " and tbllogsheet.status<>'3' and tbllogsheet.branch='" & login.branch & "'" 'and tbllogitem.status='2'" 

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                'and ='1'
                If dr("itemstatus") <> 2 Then
                    MsgBox("Pallet Tag# " & Trim(txtpallet.Text) & " is not yet completed.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub
                End If

                If dr("ticketstatus") = 0 Then
                    MsgBox("Pallet Tag# " & Trim(txtpallet.Text) & " is already empty.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub
                ElseIf dr("ticketstatus") = 3 Then
                    MsgBox("Pallet Tag# " & Trim(txtpallet.Text) & " is already cancelled.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub
                ElseIf dr("ticketwhse") <> lblwhse.Text Then
                    '/MsgBox("Cannot found Pallet Tag# " & Trim(txtpallet.Text) & " in " & lblwhse.Text & ".", MsgBoxStyle.Information, "")
                    '/txtpallet.Focus()
                    '/txtpallet.SelectAll()
                    '/Exit Sub
                ElseIf dr("location") <> Trim(cmbloc.Text) And Trim(cmbloc.Text) <> "" Then
                    MsgBox("Cannot found Pallet Tag# " & Trim(txtpallet.Text) & " in " & Trim(cmbloc.Text) & " location.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub
                End If

                If dr("qadispo") = 2 Then
                    MsgBox("Pallet Tag# " & Trim(txtpallet.Text) & " is in HOLD status.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub

                ElseIf (dr("qadispo") = 0 Or dr("qadispo") = 3) Then
                    MsgBox("Pallet Tag# " & Trim(txtpallet.Text) & " is still pending for QA disposition.", MsgBoxStyle.Information, "")
                    txtpallet.Focus()
                    txtpallet.SelectAll()
                    Exit Sub
                End If

                If dr("qadispo") = 1 Then
                    Dim meronna As Boolean = False
                    For Each item As Object In cmbtemp.Items
                        If item = dr("palletnum") Then
                            meronna = True
                            Exit For
                        End If
                    Next
                    If meronna = False Then
                        cmbtemp.Items.Add(dr("palletnum"))
                    End If
                End If
            Else
                MsgBox("Cannot found Pallet Tag# " & Trim(txtpallet.Text) & ".", MsgBoxStyle.Critical, "")
                txtpallet.Focus()
                txtpallet.SelectAll()
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            For Each item As Object In cmbtemp.Items
                sql = "Select * from tbllogticket where palletnum='" & item & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If IsDBNull(dr("addtoloc")) = False Then
                        Dim stat As String = ""
                        If dr("cusreserve") = 0 Then
                            stat = "Available"
                            grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                        ElseIf dr("cusreserve") = 1 Then
                            stat = "Reserved"
                            grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                            If dr("customer") = orderfill.txtcus.Text Then
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                            Else
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                            End If
                        Else
                            stat = "Selected"
                            If dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                                grdpallets.Rows(grdpallets.Rows.Count - 1).Cells(1).ReadOnly = True
                            Else
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.NavajoWhite
                            End If
                        End If
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next


            countnumbags()

            'add remarks ni prod 13
            For Each row As DataGridViewRow In grdpallets.Rows
                Dim lognum As String = grdpallets.Rows(row.Index).Cells(4).Value
                sql = "Select * from tbllogsheet where logsheetnum='" & lognum & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdpallets.Rows(row.Index).Cells(13).Value = dr("prodrems").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            If grdpallets.Rows.Count = 0 Then
                '/MsgBox("No record found.", MsgBoxStyle.Critical, "")
            End If

            gpallets.Text = "Searched Pallets"

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

    Private Sub txtticket_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtticket.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            searchticket()
        End If
    End Sub

    Private Sub txtticket_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtticket.TextChanged
        If Trim(txtticket.Text) = "" Then
            txtpallet.Enabled = True
            '/btnview.PerformClick()
        Else
            txtpallet.Text = ""
            txtpallet.Enabled = False
        End If
    End Sub

    Public Sub searchticket()
        Try
            If Trim(txtletter.Text) = "" Or Trim(txtticket.Text) = "" Then
                MsgBox("Complete the required fields to search ticket#.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If

            cmbtemp.Items.Clear()
            grdpallets.Rows.Clear()

            sql = "Select tbllogticket.ticketwhse, tbllogticket.qadispo, tbllogticket.palletnum, tbllogitem.itemname, tbllogticket.status, tbllogticket.location from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
            sql = sql & " full outer join tblloggood on tbllogticket.logticketid=tblloggood.logticketid full outer join tbllogdouble on tbllogticket.logticketid=tbllogdouble.logticketid full outer join tbllogcancel on tbllogticket.logticketid=tbllogcancel.logticketid"
            sql = sql & " where ((tblloggood.letter='" & Trim(txtletter.Text) & "' and tblloggood.gticketnum='" & Trim(txtticket.Text) & "' and tblloggood.status<>'3') or (tbllogdouble.letter='" & Trim(txtletter.Text) & "' and tbllogdouble.dticketnum='" & Trim(txtticket.Text) & "' and tbllogdouble.status<>'3')) "
            sql = sql & " and tbllogitem.itemname='" & orderfill.cmbitem.Text & "' and tbllogitem.status='2' and tbllogticket.status<>'3' and tbllogsheet.status<>'3' and tbllogsheet.branch='" & login.branch & "'"

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                'and ='1'
                If dr("status") <> 1 Then
                    MsgBox("Pallet Tag# " & dr("palletnum") & " is already empty.", MsgBoxStyle.Information, "")
                    txtticket.Focus()
                    txtticket.SelectAll()
                    Exit Sub
                ElseIf dr("ticketwhse") <> lblwhse.Text Then
                    '/MsgBox("Cannot found Pallet Tag# " & dr("palletnum") & " in " & lblwhse.Text & ".", MsgBoxStyle.Information, "")
                    '/txtticket.Focus()
                    '/txtticket.SelectAll()
                    '/Exit Sub
                ElseIf dr("qadispo") = 0 Or dr("qadispo") = 3 Then
                    MsgBox("Pallet Tag# " & dr("palletnum") & " is pending for QA Disposition.", MsgBoxStyle.Information, "")
                    txtticket.Focus()
                    txtticket.SelectAll()
                    Exit Sub
                ElseIf dr("qadispo") = 2 Then
                    MsgBox("Pallet Tag# " & dr("palletnum") & " is in HOLD Status.", MsgBoxStyle.Information, "")
                    txtticket.Focus()
                    txtticket.SelectAll()
                    Exit Sub
                ElseIf dr("location") <> Trim(cmbloc.Text) And Trim(cmbloc.Text) <> "" Then
                    MsgBox("Cannot found Pallet Tag# " & dr("palletnum") & " in " & Trim(cmbloc.Text) & " location.", MsgBoxStyle.Information, "")
                    txtticket.Focus()
                    txtticket.SelectAll()
                    Exit Sub
                End If

                If dr("qadispo") = 1 Then
                    Dim meronna As Boolean = False
                    For Each item As Object In cmbtemp.Items
                        If item = dr("palletnum") Then
                            meronna = True
                            Exit For
                        End If
                    Next
                    If meronna = False Then
                        cmbtemp.Items.Add(dr("palletnum"))
                    End If
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            If grdpallets.Rows.Count = 0 Then
                MsgBox("Cannot found Ticket# " & Trim(txtletter.Text) & Trim(txtticket.Text) & ".", MsgBoxStyle.Critical, "")
                txtticket.Focus()
                txtticket.SelectAll()
            End If


            For Each item As Object In cmbtemp.Items
                sql = "Select * from tbllogticket where palletnum='" & item & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If IsDBNull(dr("addtoloc")) = False Then
                        Dim stat As String = ""
                        If dr("cusreserve") = 0 Then
                            stat = "Available"
                            grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                        ElseIf dr("cusreserve") = 1 Then
                            stat = "Reserved"
                            grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                            If dr("customer") = orderfill.txtcus.Text Then
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                            Else
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                            End If
                        Else
                            stat = "Selected"
                            If dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                                grdpallets.Rows(grdpallets.Rows.Count - 1).Cells(1).ReadOnly = True
                            Else
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.NavajoWhite
                            End If
                        End If
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next


            countnumbags()

            'add remarks ni prod 13
            For Each row As DataGridViewRow In grdpallets.Rows
                Dim lognum As String = grdpallets.Rows(row.Index).Cells(4).Value
                sql = "Select * from tbllogsheet where logsheetnum='" & lognum & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdpallets.Rows(row.Index).Cells(13).Value = dr("prodrems").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            If grdpallets.Rows.Count = 0 Then
                '/MsgBox("No record found.", MsgBoxStyle.Critical, "")
            End If

            gpallets.Text = "Searched Pallets"

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

    Public Sub countnumbags()
        Try
            Dim totalgood As Integer = 0, totaldouble As Integer = 0
            For Each row As DataGridViewRow In grdpallets.Rows
                'tblloggood no of available
                sql = "Select Count(loggoodid) from tblloggood where palletnum='" & grdpallets.Rows(row.Index).Cells(5).Value & "' and status='1' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                totalgood = cmd.ExecuteScalar
                cmd.Dispose()
                conn.Close()

                'tbllogdouble no of available
                sql = "Select Count(logdoubleid) from tbllogdouble where palletnum='" & grdpallets.Rows(row.Index).Cells(5).Value & "' and status='1' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                totaldouble = cmd.ExecuteScalar
                cmd.Dispose()
                conn.Close()

                grdpallets.Item(6, row.Index).Value = totalgood + totaldouble
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

    Private Sub txtletter_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtletter.KeyPress
        If Asc(e.KeyChar) = 39 Or Asc(e.KeyChar) = 46 Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            '/btnadd.PerformClick()
        ElseIf (Asc(e.KeyChar) >= 65 And Asc(e.KeyChar) <= 90) Or (Asc(e.KeyChar) >= 97 And Asc(e.KeyChar) <= 122) Or Asc(e.KeyChar) = 8 Then
            If txtletter.Text.ToString.Length = 1 And Not Asc(e.KeyChar) = 8 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtletter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtletter.TextChanged
        If Trim(txtletter.Text) = "" Then
            txtpallet.Enabled = True
            '/btnview.PerformClick()
        Else
            txtpallet.Text = ""
            txtpallet.Enabled = False
        End If
    End Sub

    Private Sub ViewPalletToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewPalletToolStripMenuItem.Click
        'print pallet tag
        Dim logticketid As Integer = 0
        sql = "Select * from tbllogticket where palletnum='" & grdpallets.Rows(selectedrow).Cells(5).Value & "' and tbllogticket.status<>'3' and branch='" & login.branch & "'"
        connect()
        cmd = New SqlCommand(sql, conn)
        dr = cmd.ExecuteReader
        If dr.Read Then
            logticketid = dr("logticketid")
        End If
        dr.Dispose()
        cmd.Dispose()
        conn.Close()

        rptpallettag.logticket = logticketid
        rptpallettag.ShowDialog()
    End Sub

    Private Sub btnfirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfirst.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            clickbtn = "View First"

            txtpallet.Text = ""
            txtletter.Text = ""
            txtticket.Text = ""
            cmbtemp.Items.Clear()
            grdpallets.Rows.Clear()

            Dim firstlogsheetid As Integer

            sql = "Select tbllogsheet.logsheetdate, tbllogsheet.logsheetid from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
            '/sql = sql & " full outer join tblloggood on tbllogticket.logticketid=tblloggood.logticketid full outer join tbllogdouble on tbllogticket.logticketid=tbllogdouble.logticketid"
            sql = sql & " where tbllogticket.qadispo='1' and tbllogitem.itemname='" & orderfill.cmbitem.Text & "' and tbllogitem.status='2' and tbllogticket.status='1' and tbllogsheet.status<>'3' and tbllogsheet.branch='" & login.branch & "' order by logsheetdate"

            '/sql = "Select * from tbllogticket left outer join tbllogitem on tbllogitem.logitemid=tbllogticket.logitemid where tbllogticket.qadispo='1' and tbllogticket.ticketwhse='" & lblwhse.Text & "'  and tbllogitem.itemname='" & orderfill.cmbitem.Text & "'"
            '/MsgBox(sql.ToString)
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                sql = "Select * from tblloggood where logsheetid='" & dr("logsheetid") & "' and status='1'"
                connect()
                Dim cmd1 As SqlCommand = New SqlCommand(sql, conn)
                Dim dr1 As SqlDataReader = cmd1.ExecuteReader
                If dr1.Read Then
                    firstlogsheetid = dr("logsheetid")
                    '/MsgBox(dr("logsheetid").ToString)
                    Exit While
                End If
                dr1.Dispose()
                cmd1.Dispose()
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            sql = "Select tbllogticket.qadispo, tbllogticket.palletnum, tbllogitem.itemname from tbllogsheet right outer join tbllogitem on tbllogsheet.logsheetnum=tbllogitem.logsheetnum right outer join tbllogticket on tbllogitem.logitemid=tbllogticket.logitemid"
            sql = sql & " full outer join tblloggood on tbllogticket.logticketid=tblloggood.logticketid full outer join tbllogdouble on tbllogticket.logticketid=tbllogdouble.logticketid"
            sql = sql & " where tbllogticket.qadispo='1' and tbllogitem.itemname='" & orderfill.cmbitem.Text & "' and tbllogitem.status='2' and tbllogticket.status='1' and tbllogsheet.status<>'3' and tblloggood.status='1'"
            sql = sql & " and tbllogsheet.logsheetid='" & firstlogsheetid & "' and tbllogsheet.branch='" & login.branch & "'"
            '/sql = "Select * from tbllogticket left outer join tbllogitem on tbllogitem.logitemid=tbllogticket.logitemid where tbllogticket.qadispo='1' and tbllogticket.ticketwhse='" & lblwhse.Text & "'  and tbllogitem.itemname='" & orderfill.cmbitem.Text & "'"

            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If dr("qadispo") = 1 Then
                    Dim meronna As Boolean = False
                    For Each item As Object In cmbtemp.Items
                        If item = dr("palletnum") Then
                            meronna = True
                            Exit For
                        End If
                    Next
                    If meronna = False Then
                        cmbtemp.Items.Add(dr("palletnum"))
                    End If
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()


            For Each item As Object In cmbtemp.Items
                sql = "Select * from tbllogticket where palletnum='" & item & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If IsDBNull(dr("addtoloc")) = False Then
                        Dim stat As String = ""
                        If dr("cusreserve") = 0 Then
                            stat = "Available"
                            grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                        ElseIf dr("cusreserve") = 1 Then
                            stat = "Reserved"
                            If dr("customer") = orderfill.txtcus.Text And dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                                grdpallets.Rows(grdpallets.Rows.Count - 1).Cells(1).ReadOnly = True
                            ElseIf dr("customer") = orderfill.txtcus.Text Then
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.NavajoWhite
                            Else
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DeepSkyBlue
                            End If
                        Else
                            stat = "Selected"
                            If dr("ofnum").ToString = orderfill.lblorf.Text & orderfill.txtorf.Text Then
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
                                grdpallets.Rows(grdpallets.Rows.Count - 1).Cells(1).ReadOnly = True
                            Else
                                grdpallets.Rows.Add(dr("logticketid"), False, Format(dr("ticketdate"), "yyyy/MM/dd"), dr("location"), dr("logsheetnum"), dr("palletnum"), 0, "", dr("letter1") & " " & dr("astart"), dr("letter4") & " " & dr("fend"), stat, dr("ofnum").ToString, dr("ofnum").ToString, "", dr("remarks").ToString, dr("qarems").ToString)
                                grdpallets.Rows(grdpallets.Rows.Count - 1).DefaultCellStyle.BackColor = Color.NavajoWhite
                            End If
                        End If
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            Me.Cursor = Cursors.Default

            countnumbags()

            'add remarks ni prod 13
            For Each row As DataGridViewRow In grdpallets.Rows
                Dim lognum As String = grdpallets.Rows(row.Index).Cells(4).Value
                sql = "Select * from tbllogsheet where logsheetnum='" & lognum & "' and branch='" & login.branch & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    grdpallets.Rows(row.Index).Cells(13).Value = dr("prodrems").ToString
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
            Next

            If grdpallets.Rows.Count = 0 Then
                '/MsgBox("No record found.", MsgBoxStyle.Critical, "")
            End If

            gpallets.Text = "View First"

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