<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class orderfillnew
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(orderfillnew))
        Me.txtwrs = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtref = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtplate = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnclose = New System.Windows.Forms.Button()
        Me.btnok = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtofnum = New System.Windows.Forms.TextBox()
        Me.txtrems = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtdriver = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbwhse = New System.Windows.Forms.ComboBox()
        Me.cmbcus = New System.Windows.Forms.ComboBox()
        Me.lblwhsecode = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btnupdate = New System.Windows.Forms.Button()
        Me.cmbtemp = New System.Windows.Forms.ComboBox()
        Me.btncancel = New System.Windows.Forms.Button()
        Me.btnremove = New System.Windows.Forms.Button()
        Me.btnadd = New System.Windows.Forms.Button()
        Me.grditems = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbitem = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtbags = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grditems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtwrs
        '
        Me.txtwrs.Location = New System.Drawing.Point(94, 79)
        Me.txtwrs.Name = "txtwrs"
        Me.txtwrs.Size = New System.Drawing.Size(203, 20)
        Me.txtwrs.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(45, 82)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 13)
        Me.Label8.TabIndex = 98
        Me.Label8.Text = "WRS#:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(34, 108)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 100
        Me.Label1.Text = "Customer:"
        '
        'txtref
        '
        Me.txtref.Location = New System.Drawing.Point(94, 131)
        Me.txtref.Name = "txtref"
        Me.txtref.Size = New System.Drawing.Size(203, 20)
        Me.txtref.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 134)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 102
        Me.Label2.Text = "Cus. Ref#:"
        '
        'txtplate
        '
        Me.txtplate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtplate.Location = New System.Drawing.Point(94, 157)
        Me.txtplate.Name = "txtplate"
        Me.txtplate.Size = New System.Drawing.Size(203, 20)
        Me.txtplate.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(34, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 104
        Me.Label3.Text = "Truck No.:"
        '
        'btnclose
        '
        Me.btnclose.Image = CType(resources.GetObject("btnclose.Image"), System.Drawing.Image)
        Me.btnclose.Location = New System.Drawing.Point(211, 347)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(86, 27)
        Me.btnclose.TabIndex = 15
        Me.btnclose.Text = "Close"
        Me.btnclose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = True
        '
        'btnok
        '
        Me.btnok.Image = CType(resources.GetObject("btnok.Image"), System.Drawing.Image)
        Me.btnok.Location = New System.Drawing.Point(119, 347)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(86, 27)
        Me.btnok.TabIndex = 14
        Me.btnok.Text = "Ok"
        Me.btnok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnok.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(30, 29)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 107
        Me.Label4.Text = "Order Fill#:"
        '
        'txtofnum
        '
        Me.txtofnum.BackColor = System.Drawing.SystemColors.Control
        Me.txtofnum.Location = New System.Drawing.Point(94, 26)
        Me.txtofnum.Name = "txtofnum"
        Me.txtofnum.ReadOnly = True
        Me.txtofnum.Size = New System.Drawing.Size(203, 20)
        Me.txtofnum.TabIndex = 16
        '
        'txtrems
        '
        Me.txtrems.Location = New System.Drawing.Point(94, 210)
        Me.txtrems.Multiline = True
        Me.txtrems.Name = "txtrems"
        Me.txtrems.Size = New System.Drawing.Size(203, 91)
        Me.txtrems.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(36, 213)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 110
        Me.Label5.Text = "Remarks:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtdriver)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cmbwhse)
        Me.GroupBox1.Controls.Add(Me.cmbcus)
        Me.GroupBox1.Controls.Add(Me.lblwhsecode)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtrems)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtwrs)
        Me.GroupBox1.Controls.Add(Me.txtofnum)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnclose)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnok)
        Me.GroupBox1.Controls.Add(Me.txtref)
        Me.GroupBox1.Controls.Add(Me.txtplate)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(329, 397)
        Me.GroupBox1.TabIndex = 111
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Order Fill Info"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Red
        Me.Label16.Location = New System.Drawing.Point(298, 184)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(14, 16)
        Me.Label16.TabIndex = 122
        Me.Label16.Text = "*"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Red
        Me.Label15.Location = New System.Drawing.Point(298, 158)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(14, 16)
        Me.Label15.TabIndex = 121
        Me.Label15.Text = "*"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Red
        Me.Label14.Location = New System.Drawing.Point(298, 132)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(14, 16)
        Me.Label14.TabIndex = 120
        Me.Label14.Text = "*"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Red
        Me.Label13.Location = New System.Drawing.Point(298, 106)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(14, 16)
        Me.Label13.TabIndex = 119
        Me.Label13.Text = "*"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Red
        Me.Label12.Location = New System.Drawing.Point(298, 80)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(14, 16)
        Me.Label12.TabIndex = 118
        Me.Label12.Text = "*"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Red
        Me.Label11.Location = New System.Drawing.Point(298, 53)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(14, 16)
        Me.Label11.TabIndex = 117
        Me.Label11.Text = "*"
        '
        'txtdriver
        '
        Me.txtdriver.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtdriver.Location = New System.Drawing.Point(94, 183)
        Me.txtdriver.Name = "txtdriver"
        Me.txtdriver.Size = New System.Drawing.Size(203, 20)
        Me.txtdriver.TabIndex = 5
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(16, 186)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 13)
        Me.Label10.TabIndex = 116
        Me.Label10.Text = "Driver Name: "
        '
        'cmbwhse
        '
        Me.cmbwhse.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbwhse.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbwhse.FormattingEnabled = True
        Me.cmbwhse.Location = New System.Drawing.Point(94, 52)
        Me.cmbwhse.Name = "cmbwhse"
        Me.cmbwhse.Size = New System.Drawing.Size(203, 21)
        Me.cmbwhse.TabIndex = 0
        '
        'cmbcus
        '
        Me.cmbcus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbcus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbcus.FormattingEnabled = True
        Me.cmbcus.Items.AddRange(New Object() {"", "FLOUR1"})
        Me.cmbcus.Location = New System.Drawing.Point(94, 105)
        Me.cmbcus.Name = "cmbcus"
        Me.cmbcus.Size = New System.Drawing.Size(203, 21)
        Me.cmbcus.TabIndex = 2
        '
        'lblwhsecode
        '
        Me.lblwhsecode.AutoSize = True
        Me.lblwhsecode.Location = New System.Drawing.Point(232, 31)
        Me.lblwhsecode.Name = "lblwhsecode"
        Me.lblwhsecode.Size = New System.Drawing.Size(0, 13)
        Me.lblwhsecode.TabIndex = 113
        Me.lblwhsecode.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(23, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 13)
        Me.Label9.TabIndex = 111
        Me.Label9.Text = "Warehouse:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.btnupdate)
        Me.GroupBox2.Controls.Add(Me.cmbtemp)
        Me.GroupBox2.Controls.Add(Me.btncancel)
        Me.GroupBox2.Controls.Add(Me.btnremove)
        Me.GroupBox2.Controls.Add(Me.btnadd)
        Me.GroupBox2.Controls.Add(Me.grditems)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.cmbitem)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtbags)
        Me.GroupBox2.Enabled = False
        Me.GroupBox2.Location = New System.Drawing.Point(347, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(577, 397)
        Me.GroupBox2.TabIndex = 116
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Set Items"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Red
        Me.Label18.Location = New System.Drawing.Point(342, 55)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(14, 16)
        Me.Label18.TabIndex = 124
        Me.Label18.Text = "*"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Red
        Me.Label17.Location = New System.Drawing.Point(342, 26)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(14, 16)
        Me.Label17.TabIndex = 123
        Me.Label17.Text = "*"
        '
        'btnupdate
        '
        Me.btnupdate.Image = CType(resources.GetObject("btnupdate.Image"), System.Drawing.Image)
        Me.btnupdate.Location = New System.Drawing.Point(367, 50)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(101, 27)
        Me.btnupdate.TabIndex = 11
        Me.btnupdate.Text = "Update"
        Me.btnupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = True
        '
        'cmbtemp
        '
        Me.cmbtemp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbtemp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbtemp.FormattingEnabled = True
        Me.cmbtemp.Location = New System.Drawing.Point(296, 127)
        Me.cmbtemp.Name = "cmbtemp"
        Me.cmbtemp.Size = New System.Drawing.Size(203, 21)
        Me.cmbtemp.TabIndex = 111
        Me.cmbtemp.Visible = False
        '
        'btncancel
        '
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(470, 50)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(101, 27)
        Me.btncancel.TabIndex = 12
        Me.btncancel.Text = "Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'btnremove
        '
        Me.btnremove.Image = CType(resources.GetObject("btnremove.Image"), System.Drawing.Image)
        Me.btnremove.Location = New System.Drawing.Point(470, 19)
        Me.btnremove.Name = "btnremove"
        Me.btnremove.Size = New System.Drawing.Size(101, 27)
        Me.btnremove.TabIndex = 10
        Me.btnremove.Text = "Remove"
        Me.btnremove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnremove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnremove.UseVisualStyleBackColor = True
        '
        'btnadd
        '
        Me.btnadd.Image = CType(resources.GetObject("btnadd.Image"), System.Drawing.Image)
        Me.btnadd.Location = New System.Drawing.Point(367, 19)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(101, 27)
        Me.btnadd.TabIndex = 9
        Me.btnadd.Text = "Add"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'grditems
        '
        Me.grditems.AllowUserToAddRows = False
        Me.grditems.AllowUserToDeleteRows = False
        Me.grditems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grditems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grditems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        Me.grditems.EnableHeadersVisualStyles = False
        Me.grditems.Location = New System.Drawing.Point(6, 83)
        Me.grditems.MultiSelect = False
        Me.grditems.Name = "grditems"
        Me.grditems.ReadOnly = True
        Me.grditems.RowHeadersWidth = 10
        Me.grditems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grditems.Size = New System.Drawing.Size(565, 308)
        Me.grditems.TabIndex = 13
        '
        'Column1
        '
        Me.Column1.HeaderText = "Item"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 280
        '
        'Column2
        '
        Me.Column2.HeaderText = "Total Number of Bags"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 160
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(28, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Item:"
        '
        'cmbitem
        '
        Me.cmbitem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbitem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbitem.FormattingEnabled = True
        Me.cmbitem.Items.AddRange(New Object() {"", "FLOUR1"})
        Me.cmbitem.Location = New System.Drawing.Point(64, 23)
        Me.cmbitem.Name = "cmbitem"
        Me.cmbitem.Size = New System.Drawing.Size(277, 21)
        Me.cmbitem.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(28, 57)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(113, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Total Number of Bags:"
        '
        'txtbags
        '
        Me.txtbags.Location = New System.Drawing.Point(147, 54)
        Me.txtbags.Name = "txtbags"
        Me.txtbags.Size = New System.Drawing.Size(194, 20)
        Me.txtbags.TabIndex = 8
        '
        'orderfillnew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(937, 426)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "orderfillnew"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New Order Fill"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grditems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtwrs As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtref As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtplate As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtofnum As System.Windows.Forms.TextBox
    Friend WithEvents txtrems As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbitem As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtbags As System.Windows.Forms.TextBox
    Friend WithEvents grditems As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents btnremove As System.Windows.Forms.Button
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents cmbtemp As System.Windows.Forms.ComboBox
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblwhsecode As System.Windows.Forms.Label
    Friend WithEvents cmbcus As System.Windows.Forms.ComboBox
    Friend WithEvents cmbwhse As System.Windows.Forms.ComboBox
    Friend WithEvents txtdriver As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
End Class
