Imports System.IO
Imports System.Data.SqlClient

Public Class coainfo
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim proceed As Boolean = False
    Public coacnf As Boolean = False
    Dim closingna As Boolean = False
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
        viewcoainfo()
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
End Class