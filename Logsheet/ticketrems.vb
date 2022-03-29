Public Class ticketrems
    Public frm As String
    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        If Trim(txtrems.Text) <> "" Then
            If frm = "receiveinfo" Then
                receiveinfo.trems = Trim(txtrems.Text)
                txtrems.Text = ""
                frm = ""
                Me.Dispose()
            Else
                ticketsum.trems = Trim(txtrems.Text)
                txtrems.Text = ""
                Me.Dispose()
            End If
        Else
            MsgBox("Input remarks.", MsgBoxStyle.Exclamation, "")
            txtrems.Focus()
        End If
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        txtrems.Text = ""
        ticketsum.trems = ""
        frm = ""
        Me.Dispose()
    End Sub

    Private Sub txtrems_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrems.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub ticketrems_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        txtrems.Text = ""
        frm = ""
    End Sub

    Private Sub ticketrems_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class