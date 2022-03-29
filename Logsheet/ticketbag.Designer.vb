<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ticketbag
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ticketbag))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btncancel = New System.Windows.Forms.Button
        Me.btnupdate = New System.Windows.Forms.Button
        Me.txtmin = New System.Windows.Forms.TextBox
        Me.txtmax = New System.Windows.Forms.TextBox
        Me.cmbsack = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtwait = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Minimum:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Maximum:"
        '
        'btncancel
        '
        Me.btncancel.BackColor = System.Drawing.Color.Gainsboro
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(225, 243)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(75, 23)
        Me.btncancel.TabIndex = 9
        Me.btncancel.Text = "Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = False
        '
        'btnupdate
        '
        Me.btnupdate.BackColor = System.Drawing.Color.Gainsboro
        Me.btnupdate.Image = CType(resources.GetObject("btnupdate.Image"), System.Drawing.Image)
        Me.btnupdate.Location = New System.Drawing.Point(135, 243)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(75, 23)
        Me.btnupdate.TabIndex = 8
        Me.btnupdate.Text = "Update"
        Me.btnupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'txtmin
        '
        Me.txtmin.Location = New System.Drawing.Point(85, 58)
        Me.txtmin.Name = "txtmin"
        Me.txtmin.ReadOnly = True
        Me.txtmin.Size = New System.Drawing.Size(215, 20)
        Me.txtmin.TabIndex = 10
        Me.txtmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtmax
        '
        Me.txtmax.Location = New System.Drawing.Point(85, 98)
        Me.txtmax.Name = "txtmax"
        Me.txtmax.ReadOnly = True
        Me.txtmax.Size = New System.Drawing.Size(215, 20)
        Me.txtmax.TabIndex = 11
        Me.txtmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbsack
        '
        Me.cmbsack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsack.FormattingEnabled = True
        Me.cmbsack.Items.AddRange(New Object() {"Cotton", "Poly", "Pollard"})
        Me.cmbsack.Location = New System.Drawing.Point(85, 22)
        Me.cmbsack.Name = "cmbsack"
        Me.cmbsack.Size = New System.Drawing.Size(215, 21)
        Me.cmbsack.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Sack Type:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Moccasin
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtwait)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.cmbsack)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtmax)
        Me.Panel1.Controls.Add(Me.btnupdate)
        Me.Panel1.Controls.Add(Me.txtmin)
        Me.Panel1.Controls.Add(Me.btncancel)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(319, 290)
        Me.Panel1.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 128)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(295, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "________________________________________________"
        '
        'txtwait
        '
        Me.txtwait.Location = New System.Drawing.Point(222, 165)
        Me.txtwait.Name = "txtwait"
        Me.txtwait.ReadOnly = True
        Me.txtwait.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtwait.Size = New System.Drawing.Size(78, 20)
        Me.txtwait.TabIndex = 2
        Me.txtwait.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 168)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(202, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Weighing Scale Waiting Time (seconds): "
        '
        'ticketbag
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(344, 314)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ticketbag"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bag Weight"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents txtmin As System.Windows.Forms.TextBox
    Friend WithEvents txtmax As System.Windows.Forms.TextBox
    Friend WithEvents cmbsack As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtwait As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
