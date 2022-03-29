Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class orderfilltickets
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

            For Each ofrow As DataGridViewRow In orderfill.grdtickets.Rows
                If orderfill.grdtickets.Rows(ofrow.Index).Cells(2).Value = txtpallet.Text And orderfill.grdtickets.Rows(ofrow.Index).Cells(1).Value = txtlog.Text Then
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
                    rdb.Text = orderfill.grdtickets.Rows(ofrow.Index).Cells(5).Value & " " & orderfill.grdtickets.Rows(ofrow.Index).Cells(3).Value
                    rdb.Tag = orderfill.grdtickets.Rows(ofrow.Index).Cells(0).Value
                    rdb.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Regular)

                    For Each ofselected As DataGridViewRow In orderfill.grdselected.Rows
                        If rdb.Tag = orderfill.grdselected.Rows(ofselected.Index).Cells(0).Value Then
                            rdb.Checked = True
                        End If
                    Next

                    For Each cnlrow As DataGridViewRow In orderfill.grdcancel.Rows
                        If orderfill.grdtickets.Rows(ofrow.Index).Cells(0).Value = orderfill.grdcancel.Rows(cnlrow.Index).Cells(6).Value And orderfill.grdcancel.Rows(cnlrow.Index).Cells(1).Value = txtpallet.Text Then
                            rdb.Checked = False
                            rdb.Enabled = False
                        End If
                    Next

                    rdb.SetBounds(y, wid, 145, 50)

                    wid += 35

                    viewpanel.Controls.Add(rdb)
                    AddHandler rdb.Click, AddressOf rdbClicked
                End If
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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim a As String = MsgBox("Are you sure you want to cancel picked tickets?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
        If a = vbYes Then
            orderfill.grdlog.Rows(orderfill.rowindex).Cells(13).Value = False
            Me.Close()
        End If
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            If lblcount.Text = 0 Then
                MsgBox("Select ticket(s) first.", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If
            '/If orderfill.grdlog.Rows(orderfill.rowindex).Cells(10).Value = Val(lblcount.Text) Then
            '/Dim a As String = MsgBox("Are you sure you want to pick tickets?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
            '/If a = vbYes Then
            For Each item As Object In viewpanel.Controls
                For Each row As DataGridViewRow In orderfill.grdtickets.Rows
                    Dim tid As Integer = orderfill.grdtickets.Rows(row.Index).Cells(0).Value
                    Dim pallet As String = orderfill.grdtickets.Rows(row.Index).Cells(2).Value

                    If pallet = txtpallet.Text And tid = item.tag Then
                        If item.checked = True Then
                            orderfill.grdtickets.Rows(row.Index).Cells(6).Value = 1
                        Else
                            orderfill.grdtickets.Rows(row.Index).Cells(6).Value = 0
                        End If
                    End If
                Next
            Next

            orderfill.grdlog.Rows(orderfill.rowindex).Cells(10).Value = Val(lblcount.Text)
            orderfill.grdlog.Rows(orderfill.rowindex).Cells(13).Value = True
            Me.Close()
            '/End If
            '/Else
            '/ MsgBox("Number of selected tickets is not equal.", MsgBoxStyle.Exclamation, "")
            '/End If

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
        countchk()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselect.Click
        For Each rdb As Object In viewpanel.Controls
            If rdb.enabled = True Then
                rdb.checked = True
            End If
        Next
        countchk()
    End Sub

    Private Sub btnunselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnunselect.Click
        For Each rdb As Object In viewpanel.Controls
            rdb.checked = False
        Next
        countchk()
    End Sub
End Class