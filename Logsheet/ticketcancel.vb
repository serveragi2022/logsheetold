Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing

Public Class ticketcancel
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public cnlpallet As Boolean = False, cnlby As String = ""
    Dim addtolocation As Boolean = False, astarttick As Integer, rems As String = ""

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

    Private Sub ticketcancel_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        lbllogticketid.Text = ""
        txtrems.Text = ""
        cmbpallet.Items.Clear()
        cmbpallet.Text = ""
        lbllognum.Text = ""
        lblline.Text = ""
        lblitem.Text = ""
    End Sub

    Private Sub ticketcancel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        viewlist()
    End Sub

    Public Sub viewlist()
        Try
            cmbpallet.Items.Clear()

            sql = "Select palletnum from tbllogticket where status<>'3' and logitemid='" & lblitemid.Text & "' order by logticketid"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                cmbpallet.Items.Add(dr("palletnum"))
            End While
            dr.Read()
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

    Private Sub txtrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtrems_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrems.TextChanged

    End Sub

    Private Sub cmbpallet_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbpallet.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbpallet_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbpallet.Leave
        Try
            If Trim(cmbpallet.Text) <> "" Then
                'check palletnum sa tbllogticket
                rems = ""

                sql = "Select t.logticketid,t.palletnum,t.astart,t.remarks,t.addtoloc from tbllogticket t"
                sql = sql & " right outer join tbllogsheet s on t.logsheetid=s.logsheetid"
                sql = sql & " where t.palletnum='" & Trim(cmbpallet.Text) & "' and s.branch='" & login.branch & "' and t.status<>'3'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    cmbpallet.Text = dr("palletnum")
                    lbllogticketid.Text = dr("logticketid")
                    astarttick = Val(dr("astart").ToString)
                    rems = dr("remarks").ToString
                    If IsDBNull(dr("addtoloc")) = False Then
                        addtolocation = True
                    Else
                        addtolocation = False
                    End If
                Else
                    MsgBox("Invalid pallet tag#.", MsgBoxStyle.Exclamation, "")
                    cmbpallet.Text = ""
                    lbllogticketid.Text = ""
                    cmbpallet.Focus()
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()
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

    Private Sub cmbpallet_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbpallet.SelectedIndexChanged

    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            If lbllogticketid.Text <> "" And Trim(txtrems.Text) <> "" Then
                Dim a As String = ""
                'check if dis is last pallet tag then mag warning
                sql = "Select TOP 1 t.logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                sql = sql & " where t.logitemid='" & lblitemid.Text & "' and s.branch='" & login.branch & "' and t.status<>'3' order by t.logticketid DESC"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("logticketid") <> lbllogticketid.Text Then
                        '/a = MsgBox("Pallet Tag is not the last pallet tag for Item: " & lblitem.Text & "." & vbCrLf & "Do you want to cancel pallet tag?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                        MsgBox("Cannot cancel pallet tag. Cancel the last pallet tag first.", MsgBoxStyle.Exclamation, "")
                        Exit Sub
                    End If
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()

                If a = "" Then
                    a = MsgBox("Are you sure you want to cancel pallet tag# " & Trim(cmbpallet.Text) & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "")
                End If

                If a = vbYes Then
                    cnlpallet = False
                    cnlby = ""
                    confirmsupwhse.ShowDialog()
                    If cnlpallet = True Then
                        ExecuteCancel(strconn)
                    End If
                End If

            Else
                MsgBox("Complete the fields.", MsgBoxStyle.Exclamation, "")
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

    Private Sub ExecuteCancel(ByVal connectionString As String)
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                Me.Cursor = Cursors.WaitCursor
                '/command.CommandText = sql
                '/command.ExecuteNonQuery()
                sql = "Update tbllogticket set status='3', cancelby='" & cnlby & "', canceldate=GetDate(), cancelreason='" & Trim(txtrems.Text) & "' where logticketid='" & lbllogticketid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'check if addtoloc is null... if not then check kung last pallet ba to para ma update yung logrange nya
                If addtolocation = True Then
                    Dim firstpallet As Boolean = False
                    'check kung ito ba ung unang pallet.. pag yes zero lng dpat astarttick nya
                    sql = "Select TOP 1 t.logticketid from tbllogticket t right outer join tbllogsheet s on s.logsheetid=t.logsheetid"
                    sql = sql & " where t.logitemid='" & lblitemid.Text & "' and s.branch='" & login.branch & "' and t.status<>'3' order by t.logticketid"
                    command.CommandText = sql
                    dr = command.ExecuteReader
                    If dr.Read Then
                        If dr("logticketid") = lbllogticketid.Text Then
                            firstpallet = True
                        Else
                            firstpallet = False
                        End If
                    End If
                    dr.Dispose()

                    If firstpallet = False Then
                        sql = "Update tbllogrange set frto='" & astarttick - 1 & "', status='1' where logsheetid='" & lbllognum.Tag & "'"
                    Else
                        sql = "Update tbllogrange set frto='0', status='1' where logsheetid='" & lbllognum.Tag & "'"
                    End If

                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If
                '////////////////////////////////////////////////////////////////////////////////


                'set status for tblloggood, double cancel
                sql = "Update tblloggood set status='3', cancelby='" & cnlby & "', canceldate=GetDate() where logticketid='" & lbllogticketid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tbllogdouble set status='3', cancelby='" & cnlby & "', canceldate=GetDate() where logticketid='" & lbllogticketid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                sql = "Update tbllogcancel set status='3', cancelby='" & cnlby & "', canceldate=GetDate() where logticketid='" & lbllogticketid.Text & "'"
                command.CommandText = sql
                command.ExecuteNonQuery()

                'drop temporary tables
                'drop tbltempgood//////////////////////////////////////////////////////////
                Dim tbltempgood As String = "tbltempgood" & lbllogticketid.Text
                Dim tblexistgood As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempgood & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistgood = True
                End If
                dr.Dispose()

                If tblexistgood = True Then
                    sql = "DROP Table " & tbltempgood & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If
                'good//////////////////////////////////////////////////////////////////////

                'drop tbltempcancel///////////////////////////////////////////////////////
                Dim tbltempcancel As String = "tbltempcancel" & lbllogticketid.Text
                Dim tblexistcancel As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempcancel & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistcancel = True
                End If
                dr.Dispose()

                If tblexistcancel = True Then
                    sql = "DROP Table " & tbltempcancel & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If
                'cancel/////////////////////////////////////////////////////////////////////////


                'drop tbltempdouble///////////////////////////////////////////////////////
                Dim tbltempdouble As String = "tbltempdouble" & lbllogticketid.Text
                Dim tblexistdouble As Boolean = False
                sql = "Select * from sys.tables where name = '" & tbltempdouble & "'"
                command.CommandText = sql
                dr = command.ExecuteReader
                If dr.Read Then
                    tblexistdouble = True
                End If
                dr.Dispose()

                If tblexistdouble = True Then
                    sql = "DROP Table " & tbltempdouble & ""
                    command.CommandText = sql
                    command.ExecuteNonQuery()
                End If
                'double/////////////////////////////////////////////////////////////////////////


                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default
                MsgBox("Successfully cancelled pallet tag# " & Trim(cmbpallet.Text) & ".", MsgBoxStyle.Information, "")
                Me.Dispose()

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox("1: " & ex.Message, MsgBoxStyle.Exclamation, "")
                ' Attempt to roll back the transaction. 
                Try
                    Me.Cursor = Cursors.Default
                    transaction.Rollback()
                Catch ex2 As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox("2: " & ex2.Message & vbCrLf & vbCrLf & "Please try again.", MsgBoxStyle.Information, "")
                End Try
            End Try
        End Using
    End Sub
End Class