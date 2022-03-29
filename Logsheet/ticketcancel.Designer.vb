<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ticketcancel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ticketcancel))
        Me.lblline = New System.Windows.Forms.Label()
        Me.lbllognum = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblitem = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbpallet = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtrems = New System.Windows.Forms.TextBox()
        Me.btnok = New System.Windows.Forms.Button()
        Me.btncancel = New System.Windows.Forms.Button()
        Me.lblitemid = New System.Windows.Forms.Label()
        Me.lbllogticketid = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblline
        '
        Me.lblline.AutoSize = True
        Me.lblline.Location = New System.Drawing.Point(109, 57)
        Me.lblline.Name = "lblline"
        Me.lblline.Size = New System.Drawing.Size(0, 15)
        Me.lblline.TabIndex = 28
        '
        'lbllognum
        '
        Me.lbllognum.AutoSize = True
        Me.lbllognum.Location = New System.Drawing.Point(152, 24)
        Me.lbllognum.Name = "lbllognum"
        Me.lbllognum.Size = New System.Drawing.Size(0, 15)
        Me.lbllognum.TabIndex = 27
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(35, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 15)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Log Sheet #:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(35, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 15)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Palletizer:"
        '
        'lblitem
        '
        Me.lblitem.AutoSize = True
        Me.lblitem.Location = New System.Drawing.Point(109, 91)
        Me.lblitem.Name = "lblitem"
        Me.lblitem.Size = New System.Drawing.Size(0, 15)
        Me.lblitem.TabIndex = 30
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(35, 91)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 15)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Item name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(35, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 15)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Pallet Tag#:"
        '
        'cmbpallet
        '
        Me.cmbpallet.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbpallet.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbpallet.FormattingEnabled = True
        Me.cmbpallet.Location = New System.Drawing.Point(112, 123)
        Me.cmbpallet.Name = "cmbpallet"
        Me.cmbpallet.Size = New System.Drawing.Size(307, 23)
        Me.cmbpallet.TabIndex = 32
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(35, 186)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 15)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Reason:"
        '
        'txtrems
        '
        Me.txtrems.Location = New System.Drawing.Point(38, 204)
        Me.txtrems.Multiline = True
        Me.txtrems.Name = "txtrems"
        Me.txtrems.Size = New System.Drawing.Size(381, 119)
        Me.txtrems.TabIndex = 34
        '
        'btnok
        '
        Me.btnok.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnok.Image = CType(resources.GetObject("btnok.Image"), System.Drawing.Image)
        Me.btnok.Location = New System.Drawing.Point(229, 344)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(92, 27)
        Me.btnok.TabIndex = 66
        Me.btnok.Text = "Ok"
        Me.btnok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnok.UseVisualStyleBackColor = True
        '
        'btncancel
        '
        Me.btncancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(327, 344)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(92, 27)
        Me.btncancel.TabIndex = 67
        Me.btncancel.Text = "Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'lblitemid
        '
        Me.lblitemid.AutoSize = True
        Me.lblitemid.Location = New System.Drawing.Point(359, 24)
        Me.lblitemid.Name = "lblitemid"
        Me.lblitemid.Size = New System.Drawing.Size(0, 15)
        Me.lblitemid.TabIndex = 68
        Me.lblitemid.Visible = False
        '
        'lbllogticketid
        '
        Me.lbllogticketid.AutoSize = True
        Me.lbllogticketid.Location = New System.Drawing.Point(109, 149)
        Me.lbllogticketid.Name = "lbllogticketid"
        Me.lbllogticketid.Size = New System.Drawing.Size(0, 15)
        Me.lbllogticketid.TabIndex = 69
        '
        'ticketcancel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(462, 395)
        Me.Controls.Add(Me.lbllogticketid)
        Me.Controls.Add(Me.lblitemid)
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.txtrems)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbpallet)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblitem)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblline)
        Me.Controls.Add(Me.lbllognum)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ticketcancel"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cancel Pallet Tag"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblline As System.Windows.Forms.Label
    Friend WithEvents lbllognum As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblitem As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbpallet As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtrems As System.Windows.Forms.TextBox
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents lblitemid As System.Windows.Forms.Label
    Friend WithEvents lbllogticketid As System.Windows.Forms.Label
End Class
