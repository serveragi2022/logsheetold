<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class orderfillitems
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(orderfillitems))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.list1 = New System.Windows.Forms.ListBox()
        Me.btncnlitem = New System.Windows.Forms.Button()
        Me.lblitem = New System.Windows.Forms.Label()
        Me.lblid = New System.Windows.Forms.Label()
        Me.btnchange = New System.Windows.Forms.Button()
        Me.btnupdate = New System.Windows.Forms.Button()
        Me.cmbtemp = New System.Windows.Forms.ComboBox()
        Me.btncancel = New System.Windows.Forms.Button()
        Me.btnremove = New System.Windows.Forms.Button()
        Me.btnadd = New System.Windows.Forms.Button()
        Me.grditems = New System.Windows.Forms.DataGridView()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbitem = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtbags = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmbwhse = New System.Windows.Forms.ComboBox()
        Me.txtdriver = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtrems = New System.Windows.Forms.TextBox()
        Me.txtplate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtref = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtwrs = New System.Windows.Forms.TextBox()
        Me.txtcus = New System.Windows.Forms.TextBox()
        Me.txtofnum = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnclose = New System.Windows.Forms.Button()
        Me.btnok = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grditems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.list1)
        Me.GroupBox2.Controls.Add(Me.btncnlitem)
        Me.GroupBox2.Controls.Add(Me.lblitem)
        Me.GroupBox2.Controls.Add(Me.lblid)
        Me.GroupBox2.Controls.Add(Me.btnchange)
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
        Me.GroupBox2.Location = New System.Drawing.Point(347, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(654, 398)
        Me.GroupBox2.TabIndex = 117
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Set Items"
        '
        'list1
        '
        Me.list1.FormattingEnabled = True
        Me.list1.Location = New System.Drawing.Point(451, 309)
        Me.list1.Name = "list1"
        Me.list1.Size = New System.Drawing.Size(120, 82)
        Me.list1.TabIndex = 119
        Me.list1.Visible = False
        '
        'btncnlitem
        '
        Me.btncnlitem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btncnlitem.Image = CType(resources.GetObject("btncnlitem.Image"), System.Drawing.Image)
        Me.btncnlitem.Location = New System.Drawing.Point(400, 83)
        Me.btncnlitem.Name = "btncnlitem"
        Me.btncnlitem.Size = New System.Drawing.Size(204, 27)
        Me.btncnlitem.TabIndex = 118
        Me.btncnlitem.Text = "Cancel Item"
        Me.btncnlitem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncnlitem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncnlitem.UseVisualStyleBackColor = True
        '
        'lblitem
        '
        Me.lblitem.AutoSize = True
        Me.lblitem.Location = New System.Drawing.Point(81, 92)
        Me.lblitem.Name = "lblitem"
        Me.lblitem.Size = New System.Drawing.Size(0, 13)
        Me.lblitem.TabIndex = 117
        Me.lblitem.Visible = False
        '
        'lblid
        '
        Me.lblid.AutoSize = True
        Me.lblid.Location = New System.Drawing.Point(48, 90)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(0, 13)
        Me.lblid.TabIndex = 116
        Me.lblid.Visible = False
        '
        'btnchange
        '
        Me.btnchange.Image = CType(resources.GetObject("btnchange.Image"), System.Drawing.Image)
        Me.btnchange.Location = New System.Drawing.Point(470, 186)
        Me.btnchange.Name = "btnchange"
        Me.btnchange.Size = New System.Drawing.Size(101, 27)
        Me.btnchange.TabIndex = 115
        Me.btnchange.Text = "Change Item"
        Me.btnchange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnchange.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnchange.UseVisualStyleBackColor = True
        Me.btnchange.Visible = False
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.Image = CType(resources.GetObject("btnupdate.Image"), System.Drawing.Image)
        Me.btnupdate.Location = New System.Drawing.Point(400, 50)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(204, 27)
        Me.btnupdate.TabIndex = 114
        Me.btnupdate.Text = "Update No. of Bags"
        Me.btnupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = True
        '
        'cmbtemp
        '
        Me.cmbtemp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbtemp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbtemp.FormattingEnabled = True
        Me.cmbtemp.Location = New System.Drawing.Point(242, 370)
        Me.cmbtemp.Name = "cmbtemp"
        Me.cmbtemp.Size = New System.Drawing.Size(203, 21)
        Me.cmbtemp.TabIndex = 111
        Me.cmbtemp.Visible = False
        '
        'btncancel
        '
        Me.btncancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(503, 17)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(101, 27)
        Me.btncancel.TabIndex = 113
        Me.btncancel.Text = "Clear"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'btnremove
        '
        Me.btnremove.Image = CType(resources.GetObject("btnremove.Image"), System.Drawing.Image)
        Me.btnremove.Location = New System.Drawing.Point(470, 157)
        Me.btnremove.Name = "btnremove"
        Me.btnremove.Size = New System.Drawing.Size(101, 27)
        Me.btnremove.TabIndex = 112
        Me.btnremove.Text = "Deactivate"
        Me.btnremove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnremove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnremove.UseVisualStyleBackColor = True
        Me.btnremove.Visible = False
        '
        'btnadd
        '
        Me.btnadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnadd.Image = CType(resources.GetObject("btnadd.Image"), System.Drawing.Image)
        Me.btnadd.Location = New System.Drawing.Point(400, 17)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(101, 27)
        Me.btnadd.TabIndex = 111
        Me.btnadd.Text = "Add"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'grditems
        '
        Me.grditems.AllowUserToAddRows = False
        Me.grditems.AllowUserToDeleteRows = False
        Me.grditems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grditems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grditems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.Column1, Me.Column2, Me.Column3})
        Me.grditems.EnableHeadersVisualStyles = False
        Me.grditems.Location = New System.Drawing.Point(6, 114)
        Me.grditems.MultiSelect = False
        Me.grditems.Name = "grditems"
        Me.grditems.ReadOnly = True
        Me.grditems.RowHeadersWidth = 10
        Me.grditems.Size = New System.Drawing.Size(639, 277)
        Me.grditems.TabIndex = 16
        '
        'Column4
        '
        Me.Column4.HeaderText = "ID"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
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
        '
        'Column3
        '
        Me.Column3.HeaderText = "Status"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 120
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(48, 26)
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
        Me.cmbitem.Location = New System.Drawing.Point(84, 23)
        Me.cmbitem.Name = "cmbitem"
        Me.cmbitem.Size = New System.Drawing.Size(277, 21)
        Me.cmbitem.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(48, 57)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(113, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Total Number of Bags:"
        '
        'txtbags
        '
        Me.txtbags.Location = New System.Drawing.Point(167, 54)
        Me.txtbags.Name = "txtbags"
        Me.txtbags.Size = New System.Drawing.Size(194, 20)
        Me.txtbags.TabIndex = 6
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.btnclose)
        Me.GroupBox1.Controls.Add(Me.btnok)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(329, 397)
        Me.GroupBox1.TabIndex = 118
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Order Fill Info"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmbwhse)
        Me.Panel1.Controls.Add(Me.txtdriver)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtrems)
        Me.Panel1.Controls.Add(Me.txtplate)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtref)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtwrs)
        Me.Panel1.Controls.Add(Me.txtcus)
        Me.Panel1.Controls.Add(Me.txtofnum)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(6, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(304, 307)
        Me.Panel1.TabIndex = 111
        '
        'cmbwhse
        '
        Me.cmbwhse.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbwhse.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbwhse.FormattingEnabled = True
        Me.cmbwhse.Location = New System.Drawing.Point(82, 42)
        Me.cmbwhse.Name = "cmbwhse"
        Me.cmbwhse.Size = New System.Drawing.Size(203, 21)
        Me.cmbwhse.TabIndex = 119
        '
        'txtdriver
        '
        Me.txtdriver.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtdriver.Location = New System.Drawing.Point(82, 173)
        Me.txtdriver.Name = "txtdriver"
        Me.txtdriver.Size = New System.Drawing.Size(203, 20)
        Me.txtdriver.TabIndex = 117
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(4, 176)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 13)
        Me.Label10.TabIndex = 118
        Me.Label10.Text = "Driver Name: "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 113
        Me.Label1.Text = "Warehouse:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 107
        Me.Label4.Text = "Order Fill#:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(22, 150)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(54, 13)
        Me.Label9.TabIndex = 104
        Me.Label9.Text = "Plate No.:"
        '
        'txtrems
        '
        Me.txtrems.Location = New System.Drawing.Point(82, 199)
        Me.txtrems.Multiline = True
        Me.txtrems.Name = "txtrems"
        Me.txtrems.Size = New System.Drawing.Size(203, 91)
        Me.txtrems.TabIndex = 109
        '
        'txtplate
        '
        Me.txtplate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtplate.Location = New System.Drawing.Point(82, 147)
        Me.txtplate.Name = "txtplate"
        Me.txtplate.Size = New System.Drawing.Size(203, 20)
        Me.txtplate.TabIndex = 103
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(33, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 13)
        Me.Label8.TabIndex = 98
        Me.Label8.Text = "WRS#:"
        '
        'txtref
        '
        Me.txtref.Location = New System.Drawing.Point(82, 121)
        Me.txtref.Name = "txtref"
        Me.txtref.Size = New System.Drawing.Size(203, 20)
        Me.txtref.TabIndex = 101
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(24, 202)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 110
        Me.Label5.Text = "Remarks:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 102
        Me.Label3.Text = "Cus. Ref#:"
        '
        'txtwrs
        '
        Me.txtwrs.Location = New System.Drawing.Point(82, 69)
        Me.txtwrs.Name = "txtwrs"
        Me.txtwrs.Size = New System.Drawing.Size(203, 20)
        Me.txtwrs.TabIndex = 97
        '
        'txtcus
        '
        Me.txtcus.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcus.Location = New System.Drawing.Point(82, 95)
        Me.txtcus.Name = "txtcus"
        Me.txtcus.Size = New System.Drawing.Size(203, 20)
        Me.txtcus.TabIndex = 99
        '
        'txtofnum
        '
        Me.txtofnum.BackColor = System.Drawing.SystemColors.Control
        Me.txtofnum.Location = New System.Drawing.Point(82, 16)
        Me.txtofnum.Name = "txtofnum"
        Me.txtofnum.ReadOnly = True
        Me.txtofnum.Size = New System.Drawing.Size(203, 20)
        Me.txtofnum.TabIndex = 108
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 100
        Me.Label2.Text = "Customer:"
        '
        'btnclose
        '
        Me.btnclose.Image = CType(resources.GetObject("btnclose.Image"), System.Drawing.Image)
        Me.btnclose.Location = New System.Drawing.Point(211, 347)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(86, 27)
        Me.btnclose.TabIndex = 106
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
        Me.btnok.TabIndex = 105
        Me.btnok.Text = "Ok"
        Me.btnok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnok.UseVisualStyleBackColor = True
        '
        'orderfillitems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1011, 426)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "orderfillitems"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Set Items"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grditems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents cmbtemp As System.Windows.Forms.ComboBox
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents btnremove As System.Windows.Forms.Button
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents grditems As System.Windows.Forms.DataGridView
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbitem As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtbags As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtrems As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtwrs As System.Windows.Forms.TextBox
    Friend WithEvents txtofnum As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtcus As System.Windows.Forms.TextBox
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents txtref As System.Windows.Forms.TextBox
    Friend WithEvents txtplate As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnchange As System.Windows.Forms.Button
    Friend WithEvents lblid As System.Windows.Forms.Label
    Friend WithEvents lblitem As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtdriver As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbwhse As System.Windows.Forms.ComboBox
    Friend WithEvents btncnlitem As System.Windows.Forms.Button
    Friend WithEvents list1 As System.Windows.Forms.ListBox
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
