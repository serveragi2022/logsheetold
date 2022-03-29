Public Class ofchoose
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "QCA" Or login.depart = "All" Then
            branorderfill.MdiParent = mdiform
            branorderfill.Show()
            branorderfill.txtorf.Focus()
            branorderfill.WindowState = FormWindowState.Maximized
            Me.Close()
        Else
            MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If login.depart = "Warehouse" Or login.depart = "Admin Dispatching" Or login.depart = "QCA" Or login.depart = "All" Then
            orderfill.MdiParent = mdiform
            orderfill.Show()
            orderfill.txtorf.Focus()
            orderfill.WindowState = FormWindowState.Maximized
            Me.Close()
        Else
            MsgBox("Access Denied.", MsgBoxStyle.Critical, "")
        End If
    End Sub
End Class