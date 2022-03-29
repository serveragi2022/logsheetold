Imports System.IO
Imports System.Data.SqlClient

Public Class viewpallet
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public qadispo As Boolean = False

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

    Private Sub viewpallet_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ticket.cancelviewpalletonly()
        ticket.viewlocation()
        defaultform()
    End Sub

    Private Sub viewpallet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewpalletloc()
        viewforklift()
    End Sub

    Public Sub defaultform()
        lblline.Text = ""
        txtitem.Text = ""
        txtlogitemid.Text = ""
        txtbags.Text = ""
        lblbags.Text = ""
        lblrfrom.Text = ""
        lblrto.Text = ""
        txtchecker.Text = ""
        txtpallet.Text = ""
        txtlogticket.Text = ""
        cmbloc.Text = ""
        txtnumcol.Text = 1
        cmbfork.Text = ""
        txtastart.Text = ""
        txtaend.Text = ""
        txtfstart.Text = ""
        txtfend.Text = ""
        lblcancel.Text = ""
        lblseries.Text = ""
        txtbin.text = ""
        lbllet1.Text = ""
        lbllet2.Text = ""
        txtletter1.Text = ""
        txtletter2.Text = ""
        txtlet1.Text = ""
        txtlet2.Text = ""
        lblstart.Text = ""
        lblfinish.Text = ""
        txtrems.Text = ""
        grdcancel.Rows.Clear()
        grddouble.Rows.Clear()
        txttype.Text = ""
    End Sub

    Public Sub viewpalletloc()
        Try
            cmbloc.Items.Clear()

            'check active pallet location in tbllocation
            sql = "Select * from tbllocation where status='1' and whsename='" & ticket.lblwhse.Text & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbloc.Items.Add(dr("location"))
                '/numcol.Maximum = dr("max")
                txtmaxpal.Text = dr("maxpallet")
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
End Class