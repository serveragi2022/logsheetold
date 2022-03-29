Imports System.Data.SqlClient
Imports System.IO

Module recticketmodule
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)

    Public Function rec_bagIN(ByVal logsheet As String) As Integer
        Dim totalin As Integer = 0, g As Integer = 0, c As Integer = 0, d As Integer = 0

        Using connection As New SqlConnection(strconn)
            connection.Open()
            Dim command As SqlCommand = connection.CreateCommand()
            Dim drstep As SqlDataReader
            Dim transaction As SqlTransaction
            transaction = connection.BeginTransaction("SampleTransaction")
            command.Connection = connection
            command.Transaction = transaction

            Try
                '/command.CommandText =
                '/command.ExecuteNonQuery()
                command.CommandText = "select Count(g.loggoodid) from tbllogsheet s "
                command.CommandText = command.CommandText & " inner join tblloggood g on s.logsheetid=g.logsheetid"
                command.CommandText = command.CommandText & " where s.status='1' and g.status<>'3' and g.palletnum is not null"
                command.CommandText = command.CommandText & " and s.branch='" & login.branch & "' and s.logsheetnum='" & logsheet & "'"
                g = command.ExecuteScalar

                command.CommandText = "select Count(c.logcancelid) from tbllogsheet s "
                command.CommandText = command.CommandText & " inner join tbllogcancel c on s.logsheetid=c.logsheetid"
                command.CommandText = command.CommandText & " where s.status='1' and c.status<>'3' and c.palletnum is not null"
                command.CommandText = command.CommandText & " and s.branch='" & login.branch & "' and s.logsheetnum='" & logsheet & "'"
                c = command.ExecuteScalar

                command.CommandText = "select Count(d.logdoubleid) from tbllogsheet s "
                command.CommandText = command.CommandText & " inner join tbllogdouble d on s.logsheetid=d.logsheetid"
                command.CommandText = command.CommandText & " where s.status='1' and d.status<>'3' and d.palletnum is not null"
                command.CommandText = command.CommandText & " and s.branch='" & login.branch & "' and s.logsheetnum='" & logsheet & "'"
                d = command.ExecuteScalar

                ' Attempt to commit the transaction.
                transaction.Commit()
                totalin = g + c + d

            Catch ex As Exception
                ' Attempt to roll back the transaction. 
                MsgBox(ex.ToString)
                Try
                    transaction.Rollback()

                Catch ex2 As Exception
                    MsgBox(ex2.ToString)
                End Try
            End Try
        End Using

        Return totalin
    End Function
End Module
