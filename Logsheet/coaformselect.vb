Public Class coaformselect

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        coa.coaform = ""
        Me.Dispose()
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        If cmbformat.SelectedItem <> "" Then
            coa.coaform = cmbformat.SelectedItem
            Me.Dispose()
        Else
            MsgBox("Select format.", MsgBoxStyle.Exclamation, "")
        End If
    End Sub

    Private Sub coaformselect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class