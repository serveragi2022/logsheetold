<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tlogbin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(tlogbin))
        Me.txtbin = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.btncancel = New System.Windows.Forms.Button
        Me.btnok = New System.Windows.Forms.Button
        Me.lbllognum = New System.Windows.Forms.Label
        Me.lblid = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtbin
        '
        Me.txtbin.Location = New System.Drawing.Point(28, 58)
        Me.txtbin.Multiline = True
        Me.txtbin.Name = "txtbin"
        Me.txtbin.Size = New System.Drawing.Size(229, 75)
        Me.txtbin.TabIndex = 52
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(12, 40)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(59, 15)
        Me.Label14.TabIndex = 51
        Me.Label14.Text = "Flour Bin:"
        '
        'btncancel
        '
        Me.btncancel.BackColor = System.Drawing.Color.Gainsboro
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(182, 152)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(75, 23)
        Me.btncancel.TabIndex = 54
        Me.btncancel.Text = "Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = False
        '
        'btnok
        '
        Me.btnok.BackColor = System.Drawing.Color.Gainsboro
        Me.btnok.Image = CType(resources.GetObject("btnok.Image"), System.Drawing.Image)
        Me.btnok.Location = New System.Drawing.Point(92, 152)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(75, 23)
        Me.btnok.TabIndex = 53
        Me.btnok.Text = "Ok"
        Me.btnok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnok.UseVisualStyleBackColor = False
        '
        'lbllognum
        '
        Me.lbllognum.AutoSize = True
        Me.lbllognum.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllognum.Location = New System.Drawing.Point(12, 11)
        Me.lbllognum.Name = "lbllognum"
        Me.lbllognum.Size = New System.Drawing.Size(0, 15)
        Me.lbllognum.TabIndex = 55
        '
        'lblid
        '
        Me.lblid.AutoSize = True
        Me.lblid.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblid.Location = New System.Drawing.Point(198, 11)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(0, 15)
        Me.lblid.TabIndex = 56
        '
        'tlogbin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(284, 187)
        Me.Controls.Add(Me.lblid)
        Me.Controls.Add(Me.lbllognum)
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.txtbin)
        Me.Controls.Add(Me.Label14)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "tlogbin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtbin As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents lbllognum As System.Windows.Forms.Label
    Friend WithEvents lblid As System.Windows.Forms.Label
End Class
