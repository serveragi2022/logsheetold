Public Class orderfillrems

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        If Trim(txtrems.Text) <> "" Then
            orderfill.ofrems = Trim(txtrems.Text)
            txtrems.Text = ""
            Me.Dispose()
        Else
            MsgBox("Input remarks.", MsgBoxStyle.Exclamation, "")
            txtrems.Focus()
        End If
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        txtrems.Text = ""
        orderfill.ofrems = ""
        Me.Dispose()
    End Sub

    Private Sub txtrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtrems_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrems.TextChanged

    End Sub

    Private Sub ticketrems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class