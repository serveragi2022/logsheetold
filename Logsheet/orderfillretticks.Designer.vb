<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class orderfillretticks
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(orderfillretticks))
        Me.viewpanel = New System.Windows.Forms.Panel
        Me.txtpallet = New System.Windows.Forms.TextBox
        Me.lblpalletid = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.btnok = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblcount = New System.Windows.Forms.Label
        Me.btnclose = New System.Windows.Forms.Button
        Me.btnselect = New System.Windows.Forms.Button
        Me.btnunselect = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'viewpanel
        '
        Me.viewpanel.AutoScroll = True
        Me.viewpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.viewpanel.Location = New System.Drawing.Point(12, 47)
        Me.viewpanel.Name = "viewpanel"
        Me.viewpanel.Size = New System.Drawing.Size(648, 378)
        Me.viewpanel.TabIndex = 0
        '
        'txtpallet
        '
        Me.txtpallet.BackColor = System.Drawing.Color.LightCyan
        Me.txtpallet.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpallet.Location = New System.Drawing.Point(12, 12)
        Me.txtpallet.Name = "txtpallet"
        Me.txtpallet.ReadOnly = True
        Me.txtpallet.Size = New System.Drawing.Size(648, 29)
        Me.txtpallet.TabIndex = 1
        Me.txtpallet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblpalletid
        '
        Me.lblpalletid.AutoSize = True
        Me.lblpalletid.Location = New System.Drawing.Point(23, 20)
        Me.lblpalletid.Name = "lblpalletid"
        Me.lblpalletid.Size = New System.Drawing.Size(39, 13)
        Me.lblpalletid.TabIndex = 0
        Me.lblpalletid.Text = "Label1"
        Me.lblpalletid.Visible = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Gainsboro
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(559, 431)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(101, 28)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Cancel Pick"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button2.UseVisualStyleBackColor = False
        '
        'btnok
        '
        Me.btnok.BackColor = System.Drawing.Color.Gainsboro
        Me.btnok.Image = CType(resources.GetObject("btnok.Image"), System.Drawing.Image)
        Me.btnok.Location = New System.Drawing.Point(452, 431)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(101, 28)
        Me.btnok.TabIndex = 8
        Me.btnok.Text = "Ok"
        Me.btnok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnok.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 470)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(158, 15)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "No. of Selected Tickets:"
        '
        'lblcount
        '
        Me.lblcount.AutoSize = True
        Me.lblcount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcount.Location = New System.Drawing.Point(173, 470)
        Me.lblcount.Name = "lblcount"
        Me.lblcount.Size = New System.Drawing.Size(15, 15)
        Me.lblcount.TabIndex = 11
        Me.lblcount.Text = "0"
        '
        'btnclose
        '
        Me.btnclose.BackColor = System.Drawing.Color.Gainsboro
        Me.btnclose.Image = CType(resources.GetObject("btnclose.Image"), System.Drawing.Image)
        Me.btnclose.Location = New System.Drawing.Point(452, 464)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(208, 28)
        Me.btnclose.TabIndex = 12
        Me.btnclose.Text = "Close"
        Me.btnclose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'btnselect
        '
        Me.btnselect.BackColor = System.Drawing.Color.Gainsboro
        Me.btnselect.Image = CType(resources.GetObject("btnselect.Image"), System.Drawing.Image)
        Me.btnselect.Location = New System.Drawing.Point(12, 431)
        Me.btnselect.Name = "btnselect"
        Me.btnselect.Size = New System.Drawing.Size(91, 28)
        Me.btnselect.TabIndex = 13
        Me.btnselect.Text = "Select All"
        Me.btnselect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnselect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnselect.UseVisualStyleBackColor = False
        '
        'btnunselect
        '
        Me.btnunselect.BackColor = System.Drawing.Color.Gainsboro
        Me.btnunselect.Image = CType(resources.GetObject("btnunselect.Image"), System.Drawing.Image)
        Me.btnunselect.Location = New System.Drawing.Point(109, 431)
        Me.btnunselect.Name = "btnunselect"
        Me.btnunselect.Size = New System.Drawing.Size(91, 28)
        Me.btnunselect.TabIndex = 14
        Me.btnunselect.Text = "Unselect All"
        Me.btnunselect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnunselect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnunselect.UseVisualStyleBackColor = False
        '
        'orderfillretticks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(672, 502)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnunselect)
        Me.Controls.Add(Me.btnselect)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.lblcount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.lblpalletid)
        Me.Controls.Add(Me.txtpallet)
        Me.Controls.Add(Me.viewpanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "orderfillretticks"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Order fill Pick Tickets"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents viewpanel As System.Windows.Forms.Panel
    Friend WithEvents txtpallet As System.Windows.Forms.TextBox
    Friend WithEvents lblpalletid As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblcount As System.Windows.Forms.Label
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnselect As System.Windows.Forms.Button
    Friend WithEvents btnunselect As System.Windows.Forms.Button
End Class
