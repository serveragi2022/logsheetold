<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class recticketpick
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(recticketpick))
        Me.viewpanel = New System.Windows.Forms.Panel
        Me.btnunselect = New System.Windows.Forms.Button
        Me.btnselect = New System.Windows.Forms.Button
        Me.txtpallet = New System.Windows.Forms.TextBox
        Me.lblpalletid = New System.Windows.Forms.Label
        Me.btnok = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblcount = New System.Windows.Forms.Label
        Me.txtfind = New System.Windows.Forms.TextBox
        Me.btnsearch = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtlet = New System.Windows.Forms.TextBox
        Me.txtseries = New System.Windows.Forms.TextBox
        Me.list1 = New System.Windows.Forms.ListBox
        Me.list2 = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'viewpanel
        '
        Me.viewpanel.AutoScroll = True
        Me.viewpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.viewpanel.Location = New System.Drawing.Point(12, 50)
        Me.viewpanel.Name = "viewpanel"
        Me.viewpanel.Size = New System.Drawing.Size(641, 384)
        Me.viewpanel.TabIndex = 0
        '
        'btnunselect
        '
        Me.btnunselect.BackColor = System.Drawing.Color.Gainsboro
        Me.btnunselect.Image = CType(resources.GetObject("btnunselect.Image"), System.Drawing.Image)
        Me.btnunselect.Location = New System.Drawing.Point(455, 440)
        Me.btnunselect.Name = "btnunselect"
        Me.btnunselect.Size = New System.Drawing.Size(117, 23)
        Me.btnunselect.TabIndex = 16
        Me.btnunselect.Text = "Unselect All"
        Me.btnunselect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnunselect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnunselect.UseVisualStyleBackColor = False
        '
        'btnselect
        '
        Me.btnselect.BackColor = System.Drawing.Color.Gainsboro
        Me.btnselect.Image = CType(resources.GetObject("btnselect.Image"), System.Drawing.Image)
        Me.btnselect.Location = New System.Drawing.Point(332, 440)
        Me.btnselect.Name = "btnselect"
        Me.btnselect.Size = New System.Drawing.Size(117, 23)
        Me.btnselect.TabIndex = 15
        Me.btnselect.Text = "Pick All Available"
        Me.btnselect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnselect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnselect.UseVisualStyleBackColor = False
        '
        'txtpallet
        '
        Me.txtpallet.BackColor = System.Drawing.Color.LightCyan
        Me.txtpallet.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpallet.Location = New System.Drawing.Point(12, 12)
        Me.txtpallet.Name = "txtpallet"
        Me.txtpallet.ReadOnly = True
        Me.txtpallet.Size = New System.Drawing.Size(641, 29)
        Me.txtpallet.TabIndex = 1
        Me.txtpallet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblpalletid
        '
        Me.lblpalletid.AutoSize = True
        Me.lblpalletid.ForeColor = System.Drawing.Color.Blue
        Me.lblpalletid.Location = New System.Drawing.Point(23, 23)
        Me.lblpalletid.Name = "lblpalletid"
        Me.lblpalletid.Size = New System.Drawing.Size(39, 13)
        Me.lblpalletid.TabIndex = 0
        Me.lblpalletid.Text = "Label1"
        Me.lblpalletid.Visible = False
        '
        'btnok
        '
        Me.btnok.BackColor = System.Drawing.Color.Gainsboro
        Me.btnok.Image = CType(resources.GetObject("btnok.Image"), System.Drawing.Image)
        Me.btnok.Location = New System.Drawing.Point(578, 440)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(75, 23)
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
        Me.Label1.Location = New System.Drawing.Point(9, 566)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(158, 15)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "No. of Selected Tickets:"
        '
        'lblcount
        '
        Me.lblcount.AutoSize = True
        Me.lblcount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcount.Location = New System.Drawing.Point(173, 566)
        Me.lblcount.Name = "lblcount"
        Me.lblcount.Size = New System.Drawing.Size(15, 15)
        Me.lblcount.TabIndex = 11
        Me.lblcount.Text = "0"
        '
        'txtfind
        '
        Me.txtfind.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtfind.Location = New System.Drawing.Point(109, 440)
        Me.txtfind.Name = "txtfind"
        Me.txtfind.Size = New System.Drawing.Size(136, 21)
        Me.txtfind.TabIndex = 17
        '
        'btnsearch
        '
        Me.btnsearch.Image = CType(resources.GetObject("btnsearch.Image"), System.Drawing.Image)
        Me.btnsearch.Location = New System.Drawing.Point(251, 440)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(75, 23)
        Me.btnsearch.TabIndex = 18
        Me.btnsearch.Text = "Search"
        Me.btnsearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnsearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 443)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 15)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Ticket #:"
        '
        'txtlet
        '
        Me.txtlet.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtlet.Location = New System.Drawing.Point(75, 440)
        Me.txtlet.Name = "txtlet"
        Me.txtlet.Size = New System.Drawing.Size(28, 21)
        Me.txtlet.TabIndex = 20
        '
        'txtseries
        '
        Me.txtseries.BackColor = System.Drawing.Color.LightCyan
        Me.txtseries.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtseries.Location = New System.Drawing.Point(12, 468)
        Me.txtseries.Multiline = True
        Me.txtseries.Name = "txtseries"
        Me.txtseries.ReadOnly = True
        Me.txtseries.Size = New System.Drawing.Size(641, 91)
        Me.txtseries.TabIndex = 21
        Me.txtseries.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'list1
        '
        Me.list1.FormattingEnabled = True
        Me.list1.Location = New System.Drawing.Point(389, 551)
        Me.list1.Name = "list1"
        Me.list1.Size = New System.Drawing.Size(128, 30)
        Me.list1.TabIndex = 17
        Me.list1.Visible = False
        '
        'list2
        '
        Me.list2.FormattingEnabled = True
        Me.list2.Location = New System.Drawing.Point(525, 551)
        Me.list2.Name = "list2"
        Me.list2.Size = New System.Drawing.Size(128, 30)
        Me.list2.TabIndex = 22
        Me.list2.Visible = False
        '
        'recticketpick
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(665, 591)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnunselect)
        Me.Controls.Add(Me.btnselect)
        Me.Controls.Add(Me.list2)
        Me.Controls.Add(Me.list1)
        Me.Controls.Add(Me.txtseries)
        Me.Controls.Add(Me.txtlet)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnsearch)
        Me.Controls.Add(Me.txtfind)
        Me.Controls.Add(Me.lblcount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.lblpalletid)
        Me.Controls.Add(Me.txtpallet)
        Me.Controls.Add(Me.viewpanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "recticketpick"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pick Tickets"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents viewpanel As System.Windows.Forms.Panel
    Friend WithEvents txtpallet As System.Windows.Forms.TextBox
    Friend WithEvents lblpalletid As System.Windows.Forms.Label
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblcount As System.Windows.Forms.Label
    Friend WithEvents btnunselect As System.Windows.Forms.Button
    Friend WithEvents btnselect As System.Windows.Forms.Button
    Friend WithEvents txtfind As System.Windows.Forms.TextBox
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtlet As System.Windows.Forms.TextBox
    Friend WithEvents txtseries As System.Windows.Forms.TextBox
    Friend WithEvents list1 As System.Windows.Forms.ListBox
    Friend WithEvents list2 As System.Windows.Forms.ListBox
End Class
