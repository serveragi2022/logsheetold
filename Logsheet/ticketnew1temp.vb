Public Class ticketnew1temp

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txt3.Text = 0
        txt4.Text = 0
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        list2.Items.Clear()
        lblseries.Text = ""
        txt3.Text = Val(txt1.Text)

        If list1.Items.Count <> 0 Then
            'final start
            For i = Val(txt1.Text) To Val(txt4.Text)
                If list1.Items.Contains(i) = False Then
                    txt3.Text = i
                    Exit For
                End If
            Next
        End If

        Dim ctr As Integer = 0
        For i = Val(txt1.Text) To Val(txt4.Text)
            list4.Items.Clear()
            For Each item As Object In list1.Items
                If item <> i Then
                    list4.Items.Add("False")
                Else
                    list4.Items.Add("True")
                End If
            Next

            If Not list4.Items.Contains("True") Then
                'Valid Tickets
                list3.Items.Add(i)
                '/MsgBox(i)
            Else
                'Cancel Tickets
                '/MsgBox("cancel " & i)
                If list3.Items.Count <> 0 Then
                    If Val(list3.Items(0)) - Val(list3.Items(list3.Items.Count - 1)) = 0 Then
                        list2.Items.Add(list3.Items(list3.Items.Count - 1))
                        lblseries.Text = lblseries.Text & list3.Items(list3.Items.Count - 1) & "  ;  "
                        list3.Items.Clear()
                    Else
                        list2.Items.Add(list3.Items(0) & " - " & list3.Items(list3.Items.Count - 1))
                        lblseries.Text = lblseries.Text & list3.Items(0) & " - " & list3.Items(list3.Items.Count - 1) & "  ;  "
                        list3.Items.Clear()
                    End If
                End If
            End If
        Next

        If list3.Items.Count <> 0 Then
            If Val(list3.Items(0)) - Val(list3.Items(list3.Items.Count - 1)) = 0 Then
                list2.Items.Add(list3.Items(list3.Items.Count - 1))
                lblseries.Text = lblseries.Text & list3.Items(list3.Items.Count - 1) & "  ;  "
                list3.Items.Clear()
            Else
                list2.Items.Add(list3.Items(0) & " - " & list3.Items(list3.Items.Count - 1))
                lblseries.Text = lblseries.Text & list3.Items(0) & " - " & list3.Items(list3.Items.Count - 1)
                list3.Items.Clear()
            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        list1.Items.Clear()
        list2.Items.Clear()
        txt3.Text = txt1.Text
        txt4.Text = txt2.Text
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'final end
        If list1.Items.Count <> 0 Then
            txt4.Text = Val(txt1.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count
        Else
            txt4.Text = Val(txt1.Text) + (Val(txtbags.Text) - 1)
        End If


        If Val(txtcancel.Text) >= Val(txt1.Text) And Val(txtcancel.Text) <= Val(txt4.Text) Then
            If list1.Items.Contains(Val(txtcancel.Text)) = True Then
                MsgBox("Invalid Input")
                txtcancel.Text = ""
            Else
                list1.Items.Add(Val(txtcancel.Text))
                txtcancel.Text = ""
            End If

            'final end
            If list1.Items.Count <> 0 Then
                txt4.Text = Val(txt1.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count
            End If
        Else
            MsgBox("Invalid input")
            txtcancel.Text = ""
        End If

        If list1.Items.Count <> 0 Then
            If Val(txt1.Text) = list1.Items(0) Then
                'final start
                txt3.Text = Val(txt1.Text) + 1
            End If
        End If

        Button1.PerformClick()
    End Sub

    Private Sub txtbags_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbags.Leave
        'expected end
        txt2.Text = Val(txt1.Text) + Val(txtbags.Text) - 1
        'final end
        If list1.Items.Count <> 0 Then
            txt4.Text = Val(txt1.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count
        Else
            txt4.Text = Val(txt1.Text) + (Val(txtbags.Text) - 1)
        End If
    End Sub

    Private Sub txt1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt1.Leave
        'expected end
        txt2.Text = Val(txt1.Text) + Val(txtbags.Text) - 1
        txt3.Text = Val(txt1.Text)
        'final end
        If list1.Items.Count <> 0 Then
            txt4.Text = Val(txt1.Text) + (Val(txtbags.Text) - 1) + list1.Items.Count
        Else
            txt4.Text = Val(txt1.Text) + (Val(txtbags.Text) - 1)
        End If
    End Sub
End Class
