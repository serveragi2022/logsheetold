Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports Excel = Microsoft.Office.Interop

Public Class loginlogs
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn = lines(0)
    Dim conn As SqlConnection
    Dim cmd As sqlcommand
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

    Private Sub loginlogs_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub loginlogs_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
        If a = vbYes Then
            mdiform.Show()
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub loginlogs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        datefrom.CustomFormat = "yyyy/MM/dd"
        dateto.CustomFormat = "yyyy/MM/dd"
        datefrom.MaxDate = Date.Now
        dateto.MinDate = datefrom.Value

        loadversion()
        loaduser()
        loadgroup()

        cmbgroup.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmbgroup.DropDownStyle = ComboBoxStyle.DropDown
        cmbgroup.AutoCompleteSource = AutoCompleteSource.ListItems

        searchlogs()
    End Sub

    Public Sub searchlogs()
        Try
            grdlogin.Rows.Clear()

            'sql = "Select * from tbllogin where datelogin>='" & Format(datefrom.Value, "yyyy/MM/dd") & "' and datelogin<='" & Format(dateto.Value, "yyyy/MM/dd") & "'"

            sql = "SELECT * FROM tbllogin RIGHT OUTER JOIN tblusers ON tbllogin.username=tblusers.username where CAST(tbllogin.datelogin as date)>='" & Format(datefrom.Value, "yyyy/MM/dd") & "' and CAST(tbllogin.datelogin as date)<='" & Format(dateto.Value, "yyyy/MM/dd") & "'"


            If Trim(cmbuser.Text) <> "" And Trim(cmbuser.Text) <> "All" Then
                sql = sql & " and tbllogin.username='" & Trim(cmbuser.Text) & "'"
            End If

            If Trim(cmbgroup.Text) <> "" And Trim(cmbgroup.Text) <> "All" Then
                sql = sql & " and tblusers.workgroup='" & Trim(cmbgroup.Text) & "'"
            End If

            If Trim(cmbversion.Text) <> "" And Trim(cmbversion.Text) <> "All" Then
                sql = sql & " and tbllogin.version='" & Trim(cmbversion.Text) & "'"
            End If

            If Trim(txtcom.Text) <> "" Then
                sql = sql & " and tbllogin.pcname='" & Trim(txtcom.Text) & "'"
            End If

            If Trim(txtip.Text) <> "" Then
                sql = sql & " and tbllogin.ipaddress='" & Trim(txtip.Text) & "'"
            End If

            sql = sql & " order by tbllogin.loginid"

            connect()
            Dim cmd1 As SqlCommand = New SqlCommand(sql, conn)
            Dim dr1 As SqlDataReader = cmd1.ExecuteReader
            While dr1.Read
                If IsDBNull(dr1("datelogout")) = True Then
                    grdlogin.Rows.Add(dr1("loginid"), dr1("username"), dr1("workgroup"), Format(dr1("datelogin"), "yyyy/MM/dd"), dr1("login"), dr1("datelogout"), dr1("logout"), dr1("version"), dr1("pcname"), dr1("ipaddress"), dr1("branch"))
                Else
                    grdlogin.Rows.Add(dr1("loginid"), dr1("username"), dr1("workgroup"), Format(dr1("datelogin"), "yyyy/MM/dd"), dr1("login"), Format(dr1("datelogout"), "yyyy/MM/dd"), dr1("logout"), dr1("version"), dr1("pcname"), dr1("ipaddress"), dr1("branch"))
                End If
            End While
            dr1.Dispose()
            cmd1.Dispose()

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
        searchlogs()
    End Sub

    Public Sub loaduser()
        Try
            cmbuser.Items.Clear()

            sql = "Select username from tblusers order by username"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbuser.Items.Add(dr("username").ToString)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If cmbuser.Items.Count <> 0 Then
                cmbuser.Items.Add("All")
                cmbuser.SelectedItem = "All"
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

    Public Sub loadgroup()
        Try
            cmbgroup.Items.Clear()
            cmbgroup.Items.Add("")

            sql = "Select workgroup from tblwgroup where status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbgroup.Items.Add(dr("workgroup"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()
           
            If cmbgroup.Items.Count <> 0 Then
                cmbgroup.Items.Add("All")
                cmbgroup.SelectedItem = "All"
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

    Public Sub loadversion()
        Try
            cmbversion.Items.Clear()

            sql = "Select version from tblversion"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbversion.Items.Add(dr("version").ToString)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            If cmbversion.Items.Count <> 0 Then
                cmbversion.Items.Add("All")
                cmbversion.SelectedItem = "All"
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

    Private Sub cmbuser_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbuser.Leave
        Try
            If Trim(cmbuser.Text) <> "" And Trim(cmbuser.Text) <> "All" And Trim(cmbuser.Text) <> "ALL" And Trim(cmbuser.Text) <> "all" Then
                Dim tempuser As String = Trim(cmbuser.Text.ToLower)
                If Not cmbuser.Text.ToLower.Contains(tempuser.ToLower) Then
                    cmbuser.Text = ""
                Else
                    sql = "Select * from tblusers where username='" & Trim(cmbuser.Text) & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        cmbuser.SelectedItem = dr("username")
                        cmbgroup.SelectedItem = dr("workgroup")
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Else
                cmbuser.SelectedItem = "All"
            End If
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub cmbgroup_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbgroup.Leave
        Try
            If Trim(cmbgroup.Text) <> "" And Trim(cmbgroup.Text) <> "All" And Trim(cmbgroup.Text) <> "ALL" And Trim(cmbgroup.Text) <> "all" Then
                Dim tempgrp As String = Trim(cmbgroup.Text.ToLower)

                For i = 0 To cmbgroup.Items.Count - 1
                    cmbgroup.SelectedIndex = i
                    If Trim(cmbgroup.SelectedItem.ToString.ToLower) = tempgrp.ToLower Then
                        cmbgroup.SelectedItem = tempgrp
                        Exit For
                    End If
                Next

            Else
                cmbgroup.SelectedItem = "All"
            End If

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub cmbversion_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbversion.Leave
        Try
            If Trim(cmbversion.Text) <> "" And Trim(cmbversion.Text) <> "All" And Trim(cmbversion.Text) <> "ALL" And Trim(cmbversion.Text) <> "all" Then
                Dim tempgrp As String = Trim(cmbversion.Text.ToLower)
                If Not cmbversion.Items.Contains(tempgrp.ToLower) Then
                    cmbversion.Text = ""
                Else
                    sql = "Select * from tblversion where version='" & Trim(cmbversion.Text) & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        cmbversion.SelectedItem = dr("version")
                    End While
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()
                End If
            Else
                cmbversion.SelectedItem = "All"
            End If
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            'MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub cmbuser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbuser.SelectedIndexChanged

    End Sub

    Private Sub datefrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles datefrom.ValueChanged
        dateto.MinDate = datefrom.Value
    End Sub
End Class