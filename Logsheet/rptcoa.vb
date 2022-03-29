Imports System.IO
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.ComponentModel

Public Class rptcoa
    Dim lines = System.IO.File.ReadAllLines("connectionstring.txt")
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String
    Dim dscmd As New SqlDataAdapter
    Public objRpt As New crcoa
    Public ds As New DataSet1
    Public sqlquery As String
    Public constring As String
    Public coanum As String, stat As String, ofnum As String, ofitemid As Integer, qty As Integer

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

    Private Sub rptbintag_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ds = New DataSet1
        Try
            sqlquery = "SELECT tblcoa.coanum, tblcoa.refnum, tblcoa.itemname, tblcoa.ofitemid, tblcoa.batchnum, tblorderfill.cusref,"
            sqlquery = sqlquery & "tblcoa.refnum, tblcoa.proddate, tblcoa.expirydate, tblcoa.coaid, tblorderfill.platenum,"
            sqlquery = sqlquery & "tblcoa.loaddate, tblcoa.deldate, tblorderfill.customer, tblcoa.moisture, tblcoa.protein, tblcoa.ash, tblcoa.wetgluten, tblcoa.water, tblcoa.createdby"
            sqlquery = sqlquery & " FROM tblcoa RIGHT OUTER JOIN tblorderfill ON tblcoa.ofnum=tblorderfill.ofnum"
            sqlquery = sqlquery & " where tblcoa.coanum='" & coanum & "'"
            connect()
            sql = procesSQL()
            Me.Cursor = Cursors.WaitCursor
            dscmd = New SqlDataAdapter(sql, conn)
            dscmd.Fill(ds.DataTable5)
            'MsgBox(ds.Tables(4).Rows.Count)
            disconnect()


            'datacolumn4 - itemcode
            sql = "Select * from tblitems where itemname='" & ds.Tables(4).Rows(0).Item("DataColumn3").ToString & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                ds.Tables(4).Rows(0).Item("DataColumn4") = dr("itemcode")
            End If
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Dim MyText As TextObject
            MyText = CType(objRpt.ReportDefinition.ReportObjects("Text126"), TextObject)
            MyText.Text = "A"

            'datacolumn7 - ticketseries
            Dim tempseries As String = ""
            sql = "Select * from tbloflog where ofnum='" & ofnum & "' and ofitemid='" & ofitemid & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                If tempseries = "" Then
                    tempseries = dr("ticketseries")
                Else
                    tempseries = tempseries & dr("ticketseries")
                End If
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            grd.Rows.Clear()
            sql = "Select tbloflog.oflogid, tbloflog.palletnum, tblofloggood.gticketnum from tbloflog right outer join tblofloggood on tbloflog.oflogid=tblofloggood.oflogid where tbloflog.ofnum='" & ofnum & "' and tbloflog.ofitemid='" & ofitemid & "'"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                grd.Rows.Add(dr("oflogid"), dr("palletnum"), dr("gticketnum"))
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Dim str As String, tempserieswithline As String = ""
            Dim strArr() As String
            Dim count As Integer
            str = tempseries
            strArr = str.Split(", ")
            For count = 0 To strArr.Length - 1
                If tempserieswithline = "" Then
                    tempserieswithline = " " & (strArr(count))
                Else
                    tempserieswithline = tempserieswithline & vbCrLf & (strArr(count))
                End If
            Next

            ds.Tables(4).Rows(0).Item("DataColumn7") = tempserieswithline


            'datacolumn10 - quantity
            ds.Tables(4).Rows(0).Item("DataColumn10") = qty


            '/Dim MyText As TextObject
            '/If stat <> "" Then
            '/MyText = CType(objRpt.ReportDefinition.ReportObjects("Text124"), TextObject)
            '/MyText.Text = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - P E N D I N G - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -"
            '/Else
            '/MyText = CType(objRpt.ReportDefinition.ReportObjects("Text124"), TextObject)
            '/MyText.Text = ""
            '/End If

            objRpt.SetDataSource(Me.ds.Tables(4))
            CrystalReportViewer1.ReportSource = objRpt
            CrystalReportViewer1.Refresh()

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        End Try
    End Sub

    Public Function procesSQL() As String
        Dim inSql As String
        Dim firstPart As String
        Dim lastPart As String
        Dim selectStart As Integer
        Dim fromStart As Integer
        Dim fields As String()
        Dim i As Integer
        Dim MyText As TextObject
        'Dim mycol As FieldObject

        inSql = sqlquery
        'inSql = inSql.ToUpper

        selectStart = inSql.IndexOf("SELECT")
        fromStart = inSql.IndexOf("FROM")
        selectStart = selectStart + 6
        firstPart = inSql.Substring(selectStart, (fromStart - selectStart))
        lastPart = inSql.Substring(fromStart, inSql.Length - fromStart)

        fields = firstPart.Split(",")
        firstPart = ""
        'MsgBox(fields.Length)
        For i = 0 To fields.Length - 1
            If i > 0 Then
                firstPart = firstPart & " , " & fields(i).ToString() & " as DataColumn" & i + 1
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text" & i + 1), TextObject)
                MyText.Text = Trim(fields(i).ToString())

                If MyText.Text = "tbllogsheet.logsheetdate" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogsheet.whsename" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogsheet.shift" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogitem.logitemid" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogitem.palletizer" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogticket.logticketid" Then
                    MyText.Text = "Pallet Tag Number"
                ElseIf MyText.Text = "tbllogticket.location" Then
                    MyText.Text = "Location"
                ElseIf MyText.Text = "tbllogticket.columns" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogitem.itemname" Then
                    MyText.Text = "Item Description"
                ElseIf MyText.Text = "tbllogticket.gseries" Then
                    MyText.Text = "Ticket Series"
                ElseIf MyText.Text = "tbllogticket.bags" Then
                    MyText.Text = "Bags"
                ElseIf MyText.Text = "tbllogcancel.cticketdate" Then
                    MyText.Text = "Time"
                ElseIf MyText.Text = "tbllogcancel.grossw" Then
                    MyText.Text = "Gross W"
                ElseIf MyText.Text = "tbllogcancel.cticketnum" Then
                    MyText.Text = "Cancel Ticket"
                ElseIf MyText.Text = "tbllogticket.whsechecker" Then
                    MyText.Text = ""
                ElseIf MyText.Text = "tbllogsheet.logsheetnum" Then
                    MyText.Text = ""
                End If
            Else
                firstPart = firstPart & fields(i).ToString() & " as DataColumn" & i + 1
                MyText = CType(objRpt.ReportDefinition.ReportObjects("Text" & i + 1), TextObject)
                MyText.Text = Trim(fields(i).ToString())

                'tbllogticket.location,tbllogitem.itemname,tbllogticket.columns,tbllogticket.bags,tbllogticket.astart,tbllogticket.fend,tbllogticket.cseries
            End If
        Next


        sql = "SELECT " & firstPart & " " & lastPart

        Return sql
    End Function
End Class