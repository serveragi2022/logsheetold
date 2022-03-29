Imports System.IO
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Drawing
Public Class Form1
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Dim AscendingListBox As New List(Of Integer)
    Public rowindex As Integer
    Public ofcnf As Boolean = False, ofcnfby As String = "", ofrems As String = "", picktickets As Boolean
    Dim b4edit As Integer, gridsql As String, loginbranch As String
    Dim withtemptickets As Boolean = False
    Dim table As DataTable
    Dim rcount As Integer = 0

    Private threadEnabled As Boolean, threadEnabledcomp As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private bwselectitem As BackgroundWorker, bwviewtickets As BackgroundWorker, bwviewpick As BackgroundWorker, bwselected As BackgroundWorker
    Private bwloadcompleted As BackgroundWorker, bwloadcompletedticks As BackgroundWorker

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
    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click
        list1.Items.Clear()

        Dim first As Integer = Val(txtfrom.Text)
        Dim second As Integer = Val(txtto.Text)

        For i = first To second
            list1.Items.Add(i)
        Next

        If list1.Items.Count <> 0 Then
            Executeok(strconn)
        End If
    End Sub


    Private Sub Executeok(ByVal connectionString As String)
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
                For i = 0 To list1.Items.Count - 1
                    Dim ticketnum As String = list1.Items(i)

                    sql = "Insert into tblofloggood (ofid, ofnum, ofitemid, oflogid, logsheetnum, palletnum, tickettype, letter, gticketnum, remarks, status)"
                    sql = sql & " values ('46281', 'OF.C-21-000622', '56278', '" & Trim(txtoflogid.Text) & "', '" & Trim(txtlogsheet.Text) & "', '" & Trim(txtpallet.Text) & "', '" & Trim(txtxtype.Text) & "', 'B', '" & ticketnum & "', '', '1')"
                    command.CommandText = sql
                    command.ExecuteNonQuery()

                    'If ticketnum.ToString.Contains("D") = True Then
                    '    'tbllogdouble

                    '    'update tbllogdouble where ticketnum
                    '    sql = "Update tbllogdouble set status='0', ofid='" & lblorfid.Text & "' where logdoubleid='" & ticketid & "'"
                    '    command.CommandText = sql
                    '    command.ExecuteNonQuery()

                    'Else
                    '    'tblloggood

                    '    'update tblloggood where ticketnum
                    '    sql = "Update tblloggood set status='0', ofid='" & lblorfid.Text & "' where loggoodid='" & ticketid & "'"
                    '    command.CommandText = sql
                    '    command.ExecuteNonQuery()
                    'End If
                Next

                ' Attempt to commit the transaction.
                transaction.Commit()
                Me.Cursor = Cursors.Default

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