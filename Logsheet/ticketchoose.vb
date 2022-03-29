Public Class ticketchoose

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ticketlines.rbticket.Checked = True
        ticketlines.lbltype.Text = ticketlines.rbticket.Text
        ticketlines.lbltype.BackColor = Button1.BackColor
        ticketlines.MdiParent = mdiform
        ticketlines.Focus()
        ticketlines.Show()
        ticketlines.WindowState = FormWindowState.Normal
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ticketlines.rbink.Checked = True
        ticketlines.lbltype.Text = ticketlines.rbink.Text
        ticketlines.lbltype.BackColor = Button2.BackColor
        ticketlines.MdiParent = mdiform
        ticketlines.Focus()
        ticketlines.Show()
        ticketlines.WindowState = FormWindowState.Normal
        Me.Close()
    End Sub

    Private Sub ticketchoose_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        
    End Sub

    Private Sub ticketchoose_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ticketlines.rbbran.Checked = True
        ticketlines.lbltype.Text = ticketlines.rbbran.Text
        ticketlines.lbltype.BackColor = Button3.BackColor
        ticketlines.MdiParent = mdiform
        ticketlines.Focus()
        ticketlines.Show()
        ticketlines.WindowState = FormWindowState.Normal
        Me.Close()
    End Sub
End Class