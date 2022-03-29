<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ticketrems
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ticketrems))
        Me.txtrems = New System.Windows.Forms.TextBox()
        Me.btncancel = New System.Windows.Forms.Button()
        Me.btnok = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtrems
        '
        Me.txtrems.Location = New System.Drawing.Point(27, 25)
        Me.txtrems.Multiline = True
        Me.txtrems.Name = "txtrems"
        Me.txtrems.Size = New System.Drawing.Size(280, 125)
        Me.txtrems.TabIndex = 0
        '
        'btncancel
        '
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(221, 174)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(86, 27)
        Me.btncancel.TabIndex = 5
        Me.btncancel.Text = "Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'btnok
        '
        Me.btnok.Image = CType(resources.GetObject("btnok.Image"), System.Drawing.Image)
        Me.btnok.Location = New System.Drawing.Point(129, 174)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(86, 27)
        Me.btnok.TabIndex = 4
        Me.btnok.Text = "Ok"
        Me.btnok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnok.UseVisualStyleBackColor = True
        '
        'ticketrems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(331, 213)
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.txtrems)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ticketrems"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Remarks"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtrems As System.Windows.Forms.TextBox
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents btnok As System.Windows.Forms.Button
End Class
