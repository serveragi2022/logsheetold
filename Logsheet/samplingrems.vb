Public Class samplingrems

    Private Sub txtrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtrems_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrems.TextChanged

    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        If Trim(txtrems.Text) = "" Then
            sampling.qarems = ""
            MsgBox("Input remarks.", MsgBoxStyle.Exclamation, "")
            Exit Sub
        Else
            sampling.qarems = Trim(txtrems.Text)
        End If

        sampling.samprems = True
        Me.Close()
    End Sub

    Private Sub samplingrems_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        lblitem.Text = ""
        lblofitemid.Text = ""
        lblofnum.Text = ""
        txtrems.Text = ""
    End Sub

    Private Sub samplingrems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        sampling.samprems = False
        Me.Close()
    End Sub
End Class