Imports System.IO
Imports System.Data.SqlClient

Public Class mdiform
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim sql As String

    Public mdicnf As Boolean, mdidate As Date

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

    Private Sub mdiform_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        '/Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub mdiform_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim a As String = MsgBox("Are you sure you want to close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            '/login.logshift = ""
            login.savelogout()
            login.Show()
            Me.Dispose()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub mdiform_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If login.wgroup = "Warehouse" Or login.wgroup = "Production" Then
            chooseshift.ShowDialog()
        End If
    End Sub

    Private Sub logouttool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles logouttool.Click
        Dim a As String = MsgBox("Are you sure you want to logout?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "")
        If a = vbYes Then
            login.savelogout()
            login.Show()
            Me.Dispose()
        End If
    End Sub

    Private Sub MenuStrip2_ItemAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemEventArgs) Handles MenuStrip2.ItemAdded
        Dim s As String = e.Item.GetType().ToString()
        If (s = "System.Windows.Forms.MdiControlStrip+SystemMenuItem") Then
            e.Item.Visible = False
        End If
    End Sub

    Private Sub ShiftToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShiftToolStripMenuItem.Click
        shift.MdiParent = Me
        shift.Focus()
        shift.Show()
        shift.WindowState = FormWindowState.Normal
    End Sub

    Private Sub WarehouseToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WarehouseToolStripMenuItem.Click
        whse.MdiParent = Me
        whse.Focus()
        whse.Show()
        whse.WindowState = FormWindowState.Normal
    End Sub

    Private Sub PalletizerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PalletizerToolStripMenuItem.Click
        palletizer.MdiParent = Me
        palletizer.Focus()
        palletizer.Show()
        palletizer.WindowState = FormWindowState.Normal
    End Sub

    Private Sub ItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemToolStripMenuItem.Click
        newitems.MdiParent = Me
        newitems.Show()
        newitems.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub LocationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LocationToolStripMenuItem.Click
        plocation.MdiParent = Me
        plocation.Focus()
        plocation.Show()
        plocation.WindowState = FormWindowState.Normal
    End Sub

    Private Sub CategoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CategoryToolStripMenuItem.Click
        category.MdiParent = Me
        category.Focus()
        category.Show()
        category.WindowState = FormWindowState.Normal
    End Sub

    Private Sub WorkgroupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WorkgroupToolStripMenuItem.Click
        wgroup.MdiParent = Me
        wgroup.Focus()
        wgroup.Show()
        wgroup.WindowState = FormWindowState.Normal
    End Sub

    Private Sub UsersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsersToolStripMenuItem.Click
        users.MdiParent = Me
        users.Show()
        users.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub toolshift_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolshift.Click
        If Application.OpenForms().OfType(Of chooseshift).Any Then
            chooseshift.Activate()
        Else
            Dim f2 As New chooseshift
            f2.ShowDialog()
        End If
    End Sub

    Private Sub toolmngticket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolmngticket.Click
        ticket.MdiParent = Me
        ticket.Show()
        ticket.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click
        ofchoose.ShowDialog()
    End Sub

    Private Sub SummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummaryToolStripMenuItem.Click
        orderfillsum.MdiParent = Me
        orderfillsum.Show()
        orderfillsum.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        ofchoose.ShowDialog()
    End Sub

    Private Sub ManageToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManageToolStripMenuItem1.Click
        coa.MdiParent = Me
        coa.Show()
        coa.txtofnum.Focus()
        coa.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub SummaryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummaryToolStripMenuItem1.Click
        coasum.MdiParent = Me
        coasum.Show()
        coasum.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        coa.MdiParent = Me
        coa.Show()
        coa.txtofnum.Focus()
        coa.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub UserScheduleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub DepToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DepToolStripMenuItem.Click
        dept.MdiParent = Me
        dept.Focus()
        dept.Show()
        dept.WindowState = FormWindowState.Normal
    End Sub

    Private Sub ManageToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub UserScheduleToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
       
    End Sub

    Private Sub palletToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles palletToolStripButton3.Click
        palletsum.MdiParent = Me
        palletsum.Show()
        palletsum.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub NewTicketLogSheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewTicketLogSheetToolStripMenuItem.Click
        If (login.depart = "All" Or login.depart = "Production" Or login.depart = "Admin Dispatching") Then
            ticketchoose.ShowDialog()
        Else
            MsgBox("Access denied", MsgBoxStyle.Critical, "")
        End If
    End Sub

    Private Sub ManageTicketLogSheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManageTicketLogSheetToolStripMenuItem.Click
        ticket.MdiParent = Me
        ticket.Show()
        ticket.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub SummaryTicketLogSheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummaryTicketLogSheetToolStripMenuItem.Click
        '/If (login.depart = "All" Or login.depart = "Warehouse" Or login.depart = "QCA") Then
        ticketsum.MdiParent = Me
        ticketsum.Show()
        ticketsum.WindowState = FormWindowState.Maximized
        '/Else
        '/MsgBox("Access denied", MsgBoxStyle.Critical, "")
        '/End If
    End Sub

    Private Sub CustomersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomersToolStripMenuItem.Click
        customer.MdiParent = Me
        customer.Show()
        customer.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub SamplingToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SamplingToolStripButton3.Click
        '/sampling.MdiParent = Me
        '/sampling.Show()
        '/sampling.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub VersionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VersionToolStripMenuItem.Click
        version.MdiParent = Me
        version.Focus()
        version.Show()
        version.WindowState = FormWindowState.Normal
    End Sub

    Private Sub LoginLogsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginLogsToolStripMenuItem.Click
        loginlogs.MdiParent = Me
        loginlogs.Show()
        loginlogs.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ValidBagWeightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ValidBagWeightToolStripMenuItem.Click
        ticketbag.MdiParent = Me
        ticketbag.Focus()
        ticketbag.Show()
        ticketbag.WindowState = FormWindowState.Normal
    End Sub

    Private Sub IbalikToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ibalik.MdiParent = Me
        ibalik.Show()
        ibalik.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ReturnItemsLogsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnItemsLogsToolStripMenuItem.Click
        orfreturn.MdiParent = Me
        orfreturn.Show()
        orfreturn.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub BranchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BranchToolStripMenuItem.Click
        branch.MdiParent = Me
        branch.Focus()
        branch.Show()
        branch.WindowState = FormWindowState.Normal
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        stocksheet.MdiParent = Me
        stocksheet.Focus()
        stocksheet.Show()
        stocksheet.WindowState = FormWindowState.Normal
    End Sub

    Private Sub WeightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WeightToolStripMenuItem.Click
        tickethourly.MdiParent = Me
        tickethourly.Focus()
        tickethourly.Show()
        tickethourly.WindowState = FormWindowState.Normal
    End Sub

    Private Sub ReceiveStockTransferToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceiveStockTransferToolStripMenuItem.Click
        recticket.MdiParent = Me
        recticket.Show()
        recticket.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub TransferReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TransferReportToolStripMenuItem.Click
        receivenew.MdiParent = Me
        receivenew.Show()
        receivenew.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub TransferSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TransferSummaryToolStripMenuItem.Click
        'reclogsheet.MdiParent = Me
        'reclogsheet.Show()
        'reclogsheet.WindowState = FormWindowState.Maximized
        receiveinfo.MdiParent = Me
        receiveinfo.Show()
        receiveinfo.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub NewOrderFillToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewOrderFillToolStripMenuItem.Click
        If (login.depart = "All" Or login.depart = "Warehouse" Or login.depart = "Admin Dispatching") Then
            orderfillnew.ShowDialog()
        Else
            MsgBox("Access denied", MsgBoxStyle.Critical, "")
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs)
        Form1.MdiParent = Me
        Form1.Show()
    End Sub

    Private Sub COAFormatParametersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COAFormatParametersToolStripMenuItem.Click
        If login.depart = "QCA" Or login.depart = "All" Then
            coaformat.MdiParent = Me
            coaformat.Show()
            coaformat.WindowState = FormWindowState.Maximized
        Else
            MsgBox("Access denied.", MsgBoxStyle.Critical, "")
        End If
    End Sub
End Class