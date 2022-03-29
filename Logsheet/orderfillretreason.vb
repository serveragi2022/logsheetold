Public Class orderfillretreason

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Trim(txtreason.Text) <> "" Then
            orderfillreturn.retreason = Trim(txtreason.Text)
            Me.Dispose()
        Else
            MsgBox("Input reason first.", MsgBoxStyle.Exclamation, "")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        orderfillreturn.retreason = ""
        Me.Dispose()
    End Sub
End Class