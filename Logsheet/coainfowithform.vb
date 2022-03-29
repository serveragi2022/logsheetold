Imports System.IO
Imports System.Data.SqlClient

Public Class coainfowithform
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim proceed As Boolean = False
    Public coacnf As Boolean = False
    Dim closingna As Boolean = False, coaform As String = ""
    Public coanum As String

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

    Private Sub coainfo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        defaultform()
    End Sub

    Private Sub coainfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'viewcoainfo()
        ExecuteSearch(strconn)
    End Sub

    Public Sub viewcoainfo()
        Try
            'load yung coa info
            txtcoanum.Text = coanum.Substring(4, coanum.Length - 4)
            sql = "Select * from tblcoa where coanum='" & coanum & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                lblid.Text = dr("coaid")
                Dim ofn As String = dr("ofnum")
                txtofnum.Text = ofn.Substring(3, ofn.Length - 3)
                lblitemid.Text = dr("ofitemid")
                txtitem.Text = dr("itemname")
                txtrefnum.Text = dr("refnum").ToString
                txtbatch.Text = dr("batchnum").ToString
                txtcoacus.Text = dr("coacustomer").ToString

                If IsDBNull(dr("loaddate")) = False Then
                    dateload.Value = dr("loaddate")
                End If
                If IsDBNull(dr("deldate")) = False Then
                    datedel.Value = dr("deldate")
                End If
                txtrems.Text = dr("remarks")
            Else
                MsgBox("COA # cannot found.", MsgBoxStyle.Critical, "")
                Exit Sub
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            'load yung wrs info
            sql = "Select * from tblorderfill where ofnum='" & lblofnum.Text & txtofnum.Text & "' and branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                txtwrs.Text = dr("wrsnum")
                txtcus.Text = dr("customer")
                txtpo.Text = dr("cusref")
                txttruck.Text = dr("platenum")
                txtdriver.Text = dr("driver")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            'load yung sa tblofitem
            sql = "Select tblofitem.numbags, tbloflog.ticketseries from tbloflog left outer join tblofitem on tbloflog.ofitemid=tblofitem.ofitemid where tbloflog.ofitemid='" & lblitemid.Text & "' and tblofitem.branch='" & login.branch & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                txtqty.Text = dr("numbags")
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            grdcoa.Rows.Clear()
            Dim i As Integer = 0
            'load yung sa tblcoasub
            sql = "Select * from tblcoasub where coaid='" & lblid.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If IsDBNull(dr("expirydate")) = False Then
                    grdcoa.Rows.Add(dr("coasubid"), Format(dr("proddate"), "yyyy/MM/dd"), dr("tseries"), dr("expirydate"), dr("moisture"), dr("protein"), dr("ash"), dr("wetgluten"), dr("water"), dr("others"))
                Else
                    grdcoa.Rows.Add(dr("coasubid"), Format(dr("proddate"), "yyyy/MM/dd"), dr("tseries"), "N/A", dr("moisture"), dr("protein"), dr("ash"), dr("wetgluten"), dr("water"), dr("others"))
                End If

                If i Mod 2 = 0 Then
                    grdcoa.Rows(i).DefaultCellStyle.BackColor = Color.White
                Else
                    grdcoa.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
                End If
                i += 1
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

    Private Sub ExecuteSearch(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                'load yung coa info
                txtcoanum.Text = coanum.Substring(4, coanum.Length - 4)
                sql = "Select * from tblcoa where coanum='" & coanum & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    Dim ofn As String = dr("ofnum")
                    lblid.Text = dr("coaid")
                    txtofnum.Text = ofn.Substring(3, ofn.Length - 3)
                    lblitemid.Text = dr("ofitemid")
                    txtitem.Text = dr("itemname")
                    txtcoacus.Text = dr("coacustomer").ToString
                    txtrefnum.Text = dr("refnum").ToString
                    txtbatch.Text = dr("batchnum").ToString
                    If IsDBNull(dr("loaddate")) = False Then
                        dateload.Value = dr("loaddate")
                    End If
                    If IsDBNull(dr("deldate")) = False Then
                        datedel.Value = dr("deldate")
                    End If

                    If IsDBNull(dr("coaform")) = False Then
                        coaform = dr("coaform")
                    End If
                    Panelanalysis.Text = coaform
                End If
                dr.Dispose()

                'If coaform = "" Then
                '    viewformat()
                '    coaformselect.ShowDialog()
                '    If coaform = "" Then
                '        MsgBox("Invalid format.", MsgBoxStyle.Exclamation, "")
                '        defaultform()
                '        Exit Sub
                '    Else
                '        Panelanalysis.Text = coaform
                '        'save to db
                '        sql = "Update tblcoa set coaform='" & coaform.ToUpper & "' where coaid='" & lblid.Text & "'"
                '        command.CommandText = sql
                '        command.ExecuteNonQuery()
                '    End If
                'End If

                If coaform = "COA 3" Or coaform = "COA 4" Then
                    grdcoa.Columns(3).Visible = False
                    grdcoa.Columns(4).Visible = True
                    grdcoa.Columns(5).Visible = True
                Else
                    grdcoa.Columns(3).Visible = True
                    grdcoa.Columns(4).Visible = False
                    grdcoa.Columns(5).Visible = False
                End If

                'load yung wrs info
                sql = "Select wrsnum, customer, cusref, platenum, driver from tblorderfill"
                sql = sql & " where ofnum='" & lblofnum.Text & txtofnum.Text & "' and branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    txtwrs.Text = dr("wrsnum")
                    txtcus.Text = dr("customer")
                    txtpo.Text = dr("cusref")
                    txttruck.Text = dr("platenum")
                    txtdriver.Text = dr("driver")
                End If
                dr.Dispose()

                'load yung sa tblofitem
                sql = "Select tblofitem.numbags, tbloflog.ticketseries from tbloflog left outer join tblofitem on tbloflog.ofitemid=tblofitem.ofitemid where tbloflog.ofitemid='" & lblitemid.Text & "' and tblofitem.branch='" & login.branch & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                While dr.Read
                    txtqty.Text = dr("numbags")
                End While
                dr.Dispose()

                grdcoa.Rows.Clear()
                Dim i As Integer = 0
                'load yung sa tblcoasub
                sql = "Select * from tblcoasub where coaid='" & lblid.Text & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                While dr.Read
                    If IsDBNull(dr("expirydate")) = False Then
                        grdcoa.Rows.Add(dr("coasubid"), Format(dr("proddate"), "yyyy/MM/dd"), dr("tseries"), dr("expirydate"), dr("shelf").ToString, dr("olddays").ToString)
                    Else
                        grdcoa.Rows.Add(dr("coasubid"), Format(dr("proddate"), "yyyy/MM/dd"), dr("tseries"), dr("expirydate"), dr("shelf").ToString, dr("olddays").ToString)
                    End If

                    If i Mod 2 = 0 Then
                        grdcoa.Rows(i).DefaultCellStyle.BackColor = Color.White
                    Else
                        grdcoa.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
                    End If
                    i += 1
                End While
                dr.Dispose()

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.ToString, MsgBoxStyle.Exclamation, "")
                Try
                    Me.Cursor = Cursors.Default
                    transaction.Rollback()
                Catch ex2 As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox("2: " & ex2.ToString & vbCrLf & vbCrLf & "Please try again.", MsgBoxStyle.Information, "")
                End Try
            End Try
        End Using
    End Sub

    Public Sub defaultform()
        lblitemid.Text = ""
        txtrefnum.Text = ""
        txtbatch.Text = ""
        txtcoacus.Text = ""
        txtqty.Text = ""
        dateload.Value = Date.Now
        datedel.Value = Date.Now
        txtcus.Text = ""
        txtpo.Text = ""
        txttruck.Text = ""
        txtitem.Text = ""
        txtrems.Text = ""

        txtitem.BackColor = Color.MistyRose
        txtqty.BackColor = Color.MistyRose
        txtcus.BackColor = Color.FromArgb(192, 255, 192)
        txtpo.BackColor = Color.FromArgb(192, 255, 192)
        txttruck.BackColor = Color.FromArgb(192, 255, 192)
        txtdriver.BackColor = Color.FromArgb(192, 255, 192)
    End Sub

    Private Sub selectparam()
        Try
            lblcoasubid.Text = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(0).Value
            lbldate.Text = grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(1).Value

            'gparam.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor
            'grheo.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor
            'grems.BackColor = grdcoa.Rows(grdcoa.CurrentRow.Index).DefaultCellStyle.BackColor

            'load ung coaform
            grd1.Rows.Clear()
            grd2.Rows.Clear()
            grd3.Rows.Clear()
            grd4.Rows.Clear()
            Dim ctr As Integer = 0, cattemp As String = ""
            sql = "select c.cid, f.type, f.category, f.cname, f.cvalue, c.cresult"
            sql = sql & " from tblcoaformat f inner join tblcoaparam c on f.paramid=c.paramid"
            sql = sql & " inner join tblcoa a on c.coanum=a.coanum"
            sql = sql & " where f.status='1' and c.coanum='" & lblcoa.Text & txtcoanum.Text & "' and a.branch='" & login.branch & "' and c.coasubid='" & grdcoa.Rows(grdcoa.CurrentRow.Index).Cells(0).Value & "'"
            connect()
            Dim cmd1 As SqlCommand = New SqlCommand(sql, conn)
            Dim dr1 As SqlDataReader = cmd1.ExecuteReader
            While dr1.Read
                If cattemp <> dr1("category") Then
                    ctr += 1
                    cattemp = dr1("category")
                End If

                If ctr = 1 Then
                    grd1.Columns(1).HeaderText = cattemp
                    grd1.Columns(2).HeaderText = dr1("type")
                    grd1.Rows.Add(dr1("cid"), dr1("cname"), dr1("cvalue"), dr1("cresult").ToString)
                ElseIf ctr = 2 Then
                    grd2.Columns(1).HeaderText = cattemp
                    grd2.Columns(2).HeaderText = dr1("type")
                    grd2.Rows.Add(dr1("cid"), dr1("cname"), dr1("cvalue"), dr1("cresult").ToString)
                ElseIf ctr = 3 Then
                    grd3.Columns(1).HeaderText = cattemp
                    grd3.Columns(2).HeaderText = dr1("type")
                    grd3.Rows.Add(dr1("cid"), dr1("cname"), dr1("cvalue"), dr1("cresult").ToString)
                ElseIf ctr = 4 Then
                    grd4.Columns(1).HeaderText = cattemp
                    grd4.Columns(2).HeaderText = dr1("type")
                    grd4.Rows.Add(dr1("cid"), dr1("cname"), dr1("cvalue"), dr1("cresult").ToString)
                End If
            End While
            dr1.Dispose()
            cmd1.Dispose()
            conn.Close()

            If grd1.Rows.Count <> 0 And grd2.Rows.Count <> 0 And grd3.Rows.Count = 0 And grd4.Rows.Count = 0 Then
                TableLayoutPanel1.RowStyles(0).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(0).Height = 50
                TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(1).Height = 50
                TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(2).Height = 0
                TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(3).Height = 0
            ElseIf grd1.Rows.Count <> 0 And grd2.Rows.Count <> 0 And grd3.Rows.Count <> 0 And grd4.Rows.Count = 0 Then
                TableLayoutPanel1.RowStyles(0).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(0).Height = 35
                TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(1).Height = 30
                TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(2).Height = 35
                TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(3).Height = 0
            ElseIf grd1.Rows.Count <> 0 And grd2.Rows.Count <> 0 And grd3.Rows.Count <> 0 And grd4.Rows.Count <> 0 Then
                TableLayoutPanel1.RowStyles(0).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(0).Height = 30
                TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(1).Height = 20
                TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(2).Height = 25
                TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(3).Height = 25
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub grdcoa_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcoa.CellClick
        selectparam()
    End Sub

    Private Sub grdcoa_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdcoa.CellContentClick

    End Sub

    Private Sub grdcoa_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdcoa.SelectionChanged
        selectparam()
    End Sub
End Class