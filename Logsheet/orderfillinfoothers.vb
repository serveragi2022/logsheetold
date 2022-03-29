
Public Class orderfillinfoothers

    Private Sub orderfillinfoothers_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        txtstart.Text = ""
        txtend.Text = ""
        txtfork.Text = ""
        txtsteve.Text = ""
        txtpass.Text = ""
        txtmat.Text = ""
        txtfgend.Text = ""
        txtfgstart.Text = ""
        txtloadstart.Text = ""
        txtloadend.Text = ""
    End Sub

    Private Sub orderfillinfoothers_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class