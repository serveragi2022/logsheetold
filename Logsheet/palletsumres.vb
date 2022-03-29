Imports System.IO
Imports System.Data.SqlClient

Public Class palletsumres
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Public palreserbcnf As Boolean = False
    Public logticketid As Integer, lognum As String, logitemid As Integer, pallettag As String, logyear As Integer

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

    Private Sub palletsumres_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            loadcustomer()

            sql = "Select logsheetyear from tbllogsheet where logsheetnum='" & lognum & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                logyear = dr("logsheetyear")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            viewreserve()
            countavail()

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

    Public Sub loadcustomer()
        Try
            cmbcus.Items.Clear()

            sql = "Select * from tblcustomer where status='1' order by customer"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbcus.Items.Add(dr("customer"))
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

    Private Sub btncancelres_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Public Sub viewreserve()
        Try
            'check muna kung hindi sya nakaselect sa tbllogticket
            sql = "Select * from tbllogticket where logticketid='" & logticketid & "' and tbllogticket.status<>'3'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                If dr("cusreserve") = 2 Then
                    lblstatus.Text = "Not Available"
                    cmbcus.Enabled = False
                    btnreserve.Enabled = False
                    btnupdateres.Enabled = False
                    btnclose.Enabled = False
                    btnremove.Enabled = False
                    Exit Sub
                ElseIf dr("cusreserve") = 0 Then
                    lblreserveid.Text = ""
                    lblresnum.Text = ""
                    txtrems.Text = ""
                    cmbcus.Text = ""
                    lbladmin.Text = ""
                    lbldateres.Text = ""
                    lblstatus.Text = "Available"
                    cmbcus.Enabled = True
                    btnreserve.Enabled = True
                    btnupdateres.Enabled = False
                    btnclose.Enabled = True
                    btnremove.Enabled = False
                    Exit Sub
                End If
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            sql = "Select Top 1 * from tbllogreserve where logticketid='" & logticketid & "' and status='1' order by resid DESC"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                lblreserveid.Text = dr("resid")
                lblresnum.Text = dr("reservenum")
                cmbcus.Text = dr("customer")
                lbladmin.Text = dr("adminstaff")
                lbldateres.Text = dr("datereserve")
                lblstatus.Text = "Reserved"
                txtrems.Text = dr("remarks")
                cmbcus.Enabled = False
                btnreserve.Enabled = True
                btnupdateres.Enabled = True
                btnclose.Enabled = True
                btnremove.Enabled = True
            Else
                lblreserveid.Text = ""
                lblresnum.Text = ""
                cmbcus.Text = ""
                lbladmin.Text = ""
                lbldateres.Text = ""
                lblstatus.Text = "Available"
                cmbcus.Enabled = True
                btnreserve.Enabled = True
                btnupdateres.Enabled = True
                btnclose.Enabled = True
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            countavail()

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

    Public Sub countavail()
        Try
            Dim totalgood As Integer = 0, totaldouble As Integer = 0

            'tblloggood no of available
            sql = "Select Count(loggoodid) from tblloggood where palletnum='" & txtpallet.Text & "' and status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            totalgood = cmd.ExecuteScalar
            cmd.Dispose()
            conn.Close()

            'tbllogdouble no of available
            sql = "Select Count(logdoubleid) from tbllogdouble where palletnum='" & txtpallet.Text & "' and status='1'"
            connect()
            cmd = New SqlCommand(sql, conn)
            totaldouble = cmd.ExecuteScalar
            cmd.Dispose()
            conn.Close()

            lbltotal.Text = totalgood + totaldouble

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

    Private Sub btnreserve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreserve.Click
        Try
            If login.depart = "Admin Dispatching" Or login.wgroup = "Manager" Or login.wgroup = "Administrator" Then
                'check if may dispo na si QA
                If Trim(cmbcus.Text) <> "" Then
                    palreserbcnf = False
                    confirmsave.GroupBox1.Text = login.wgroup
                    confirmsave.ShowDialog()
                    If palreserbcnf = True Then
                        'check muna ulet kung di pa naseselect yung pallet
                        sql = "Select * from tbllogticket where logticketid='" & logticketid & "' and tbllogticket.status<>'3'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        dr = cmd.ExecuteReader
                        If dr.Read Then
                            If dr("cusreserve") <> 0 Then
                                MsgBox("Pallet is already selected by " & dr("ofnum") & ".", MsgBoxStyle.Exclamation, "")
                                Exit Sub
                            End If
                        End If
                        dr.Dispose()
                        cmd.Dispose()
                        conn.Close()

                        'insert into tbllogreserve
                        loadreservenum()
                        sql = "Insert into tbllogreserve (logsheetyear, logsheetnum, logitemid, logticketid, palletnum, reservenum, customer, availbags, datereserve, adminstaff, remarks, datecreated, createdby, datemodified, modifiedby, status) values ('" & logyear & "', '" & lognum & "', '" & logitemid & "', '" & logticketid & "', '" & pallettag & "', '" & lblresnum.Text & "', '" & Trim(cmbcus.Text) & "', '" & lbltotal.Text & "', GetDate(), '" & login.fullneym & "','" & Trim(txtrems.Text) & "', GetDate(), '" & login.user & "', GetDate(),  '" & login.user & "', '1')"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        'update tbllogticket cusrteserve
                        sql = "Update tbllogticket set cusreserve='1', customer='" & Trim(cmbcus.Text) & "' where logticketid='" & logticketid & "'"
                        connect()
                        cmd = New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        cmd.Dispose()
                        conn.Close()

                        MsgBox("Successfully reserved.", MsgBoxStyle.Information, "")
                        viewreserve()
                    End If

                Else
                    MsgBox("Input Customer name.", MsgBoxStyle.Exclamation, "")
                End If
            Else
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
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

    Private Sub btnupdateres_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdateres.Click
        Try
            If login.depart = "Admin Dispatching" Or login.wgroup = "Manager" Or login.wgroup = "Administrator" Then
                If btnupdateres.Text = "Update" Then
                    cmbcus.Enabled = True
                    btnupdateres.Text = "Save"

                ElseIf btnupdateres.Text = "Save" Then
                    'check if may dispo na si QA
                    If Trim(cmbcus.Text) <> "" Then
                        If lblstatus.Text = "Reserved" Then
                            palreserbcnf = False
                            confirmsave.GroupBox1.Text = login.wgroup
                            confirmsave.ShowDialog()
                            If palreserbcnf = True Then
                                'update tbllogticket customer tbllogreserve
                                sql = "Update tbllogticket set customer='" & Trim(cmbcus.Text) & "', datemodified=GetDate(), modifiedby='" & login.user & "' where logticketid='" & logticketid & "'"
                                connect()
                                cmd = New SqlCommand(sql, conn)
                                cmd.ExecuteNonQuery()
                                cmd.Dispose()
                                conn.Close()

                                sql = "Update tbllogreserve set customer='" & Trim(cmbcus.Text) & "', remarks='" & Trim(txtrems.Text) & "', adminstaff='" & login.fullneym & "', datereserve=GetDate(), datemodified=GetDate(), modifiedby='" & login.user & "' where resid='" & lblreserveid.Text & "' and logticketid='" & logticketid & "'"
                                connect()
                                cmd = New SqlCommand(sql, conn)
                                cmd.ExecuteNonQuery()
                                cmd.Dispose()
                                conn.Close()

                                MsgBox("Successfully saved.", MsgBoxStyle.Information, "")
                                viewreserve()
                                btnupdateres.Text = "Update"
                            End If
                        Else
                            MsgBox("Cannot update. Pallet is not yet reaerved.", MsgBoxStyle.Exclamation, "")
                        End If
                    Else
                        MsgBox("Input Customer name.", MsgBoxStyle.Exclamation, "")
                    End If
                End If
            Else
                MsgBox("Access denied.", MsgBoxStyle.Critical, "")
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

    Public Sub loadreservenum()
        Try
            'check kung pang ilang palletnum na
            Dim countnum As String = "", resnum As String = ""
            sql = "Select Count(resid) from tbllogreserve where logsheetyear='" & logyear & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            countnum = cmd.ExecuteScalar + 1
            cmd.Dispose()
            conn.Close()

            Dim temp As String = ""
            If countnum < 1000000 Then
                For vv As Integer = 1 To 6 - countnum.Length
                    temp += "0"
                Next
            End If

            Dim lyear As String = logyear
            lblresnum.Text = lyear.ToString.Substring(2, lyear.ToString.Length - 2) & "-" & temp & countnum

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

    Private Sub btnremove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremove.Click
        Try
            If login.depart = "Admin Dispatching" Or login.wgroup = "Manager" Or login.wgroup = "Administrator" Then
                'check if may dispo na si QA
                palreserbcnf = False
                confirmsave.GroupBox1.Text = login.wgroup
                confirmsave.ShowDialog()
                If palreserbcnf = True Then
                    'check muna ulet kung di pa naseselect yung pallet
                    sql = "Select * from tbllogticket where logticketid='" & logticketid & "' and tbllogticket.status<>'3'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr("cusreserve") = 2 Then
                            MsgBox("Pallet is already selected by " & dr("ofnum") & ".", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        ElseIf dr("cusreserve") = 0 Then
                            MsgBox("Pallet is not yet reserved.", MsgBoxStyle.Exclamation, "")
                            Exit Sub
                        End If
                    End If
                    dr.Dispose()
                    cmd.Dispose()
                    conn.Close()

                    'remove na
                    'update tbllogticket cusrteserve
                    sql = "Update tbllogticket set cusreserve='0', customer='' where logticketid='" & logticketid & "'"
                    connect()
                    cmd = New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    conn.Close()

                    viewreserve()
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

    Private Sub cmbcus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbcus.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbcus_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcus.Leave
        Try
            If Trim(cmbcus.Text) <> "" Then
                sql = "Select * from tblcustomer where customer='" & Trim(cmbcus.Text) & "' and status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    cmbcus.Text = Trim(cmbcus.Text)
                Else
                    MsgBox("Invalid customer name.", MsgBoxStyle.Critical, "")
                    cmbcus.Text = ""
                    cmbcus.Focus()
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub cmbcus_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbcus.MouseClick
        Dim senderComboBox = DirectCast(sender, ComboBox)
        Dim width As Integer = senderComboBox.DropDownWidth
        Dim g As Graphics = senderComboBox.CreateGraphics()
        Dim font As Font = senderComboBox.Font

        Dim vertScrollBarWidth As Integer = If((senderComboBox.Items.Count > senderComboBox.MaxDropDownItems), SystemInformation.VerticalScrollBarWidth, 0)

        Dim newWidth As Integer
        For Each s As String In DirectCast(sender, ComboBox).Items
            newWidth = CInt(g.MeasureString(s, font).Width) + vertScrollBarWidth
            If width < newWidth Then
                width = newWidth
            End If
        Next

        senderComboBox.DropDownWidth = width
    End Sub

    Private Sub cmbcus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcus.SelectedIndexChanged

    End Sub

    Private Sub txtrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtrems_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrems.TextChanged

    End Sub
End Class