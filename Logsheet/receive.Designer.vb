<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class receive
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(receive))
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.txttrans = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmbbr = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtwrs = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnadd = New System.Windows.Forms.Button
        Me.grdof = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txtof = New System.Windows.Forms.TextBox
        Me.lblof = New System.Windows.Forms.Label
        Me.grdmerge = New System.Windows.Forms.DataGridView
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grdlist = New System.Windows.Forms.DataGridView
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.list1 = New System.Windows.Forms.ListBox
        Me.list2 = New System.Windows.Forms.ListBox
        Me.grdletters = New System.Windows.Forms.DataGridView
        Me.Column14 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column17 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column18 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btntransfer = New System.Windows.Forms.Button
        Me.btngen = New System.Windows.Forms.Button
        Me.txtrems = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnconfirm = New System.Windows.Forms.Button
        Me.cmbwhse = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdof, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdmerge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdletters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txttrans
        '
        Me.txttrans.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txttrans.Location = New System.Drawing.Point(107, 40)
        Me.txttrans.Name = "txttrans"
        Me.txttrans.Size = New System.Drawing.Size(289, 22)
        Me.txttrans.TabIndex = 166
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(12, 43)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 16)
        Me.Label7.TabIndex = 165
        Me.Label7.Text = "Receive #:"
        '
        'cmbbr
        '
        Me.cmbbr.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbbr.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbbr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbbr.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbbr.FormattingEnabled = True
        Me.cmbbr.Location = New System.Drawing.Point(107, 68)
        Me.cmbbr.Name = "cmbbr"
        Me.cmbbr.Size = New System.Drawing.Size(289, 23)
        Me.cmbbr.TabIndex = 164
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(12, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 16)
        Me.Label5.TabIndex = 163
        Me.Label5.Text = "From Branch:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.txtwrs)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.btnadd)
        Me.GroupBox1.Controls.Add(Me.grdof)
        Me.GroupBox1.Controls.Add(Me.txtof)
        Me.GroupBox1.Controls.Add(Me.lblof)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(15, 101)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(381, 300)
        Me.GroupBox1.TabIndex = 167
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Order Fill#"
        '
        'txtwrs
        '
        Me.txtwrs.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtwrs.Location = New System.Drawing.Point(39, 54)
        Me.txtwrs.Name = "txtwrs"
        Me.txtwrs.Size = New System.Drawing.Size(155, 22)
        Me.txtwrs.TabIndex = 162
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(3, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 15)
        Me.Label1.TabIndex = 163
        Me.Label1.Text = "WRS:"
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(287, 27)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(89, 49)
        Me.Button1.TabIndex = 161
        Me.Button1.Text = "Remove"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnadd
        '
        Me.btnadd.Image = CType(resources.GetObject("btnadd.Image"), System.Drawing.Image)
        Me.btnadd.Location = New System.Drawing.Point(200, 27)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(81, 49)
        Me.btnadd.TabIndex = 160
        Me.btnadd.Text = "Add"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'grdof
        '
        Me.grdof.AllowUserToAddRows = False
        Me.grdof.AllowUserToDeleteRows = False
        Me.grdof.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdof.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdof.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column3, Me.Column2})
        Me.grdof.Location = New System.Drawing.Point(6, 82)
        Me.grdof.MultiSelect = False
        Me.grdof.Name = "grdof"
        Me.grdof.ReadOnly = True
        Me.grdof.RowHeadersWidth = 10
        Me.grdof.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdof.Size = New System.Drawing.Size(369, 212)
        Me.grdof.TabIndex = 159
        '
        'Column1
        '
        Me.Column1.HeaderText = "ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        '
        'Column3
        '
        Me.Column3.HeaderText = "WRS"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 130
        '
        'Column2
        '
        Me.Column2.HeaderText = "Order Fill #"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 200
        '
        'txtof
        '
        Me.txtof.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtof.Location = New System.Drawing.Point(39, 27)
        Me.txtof.Name = "txtof"
        Me.txtof.Size = New System.Drawing.Size(155, 22)
        Me.txtof.TabIndex = 152
        '
        'lblof
        '
        Me.lblof.AutoSize = True
        Me.lblof.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblof.Location = New System.Drawing.Point(15, 31)
        Me.lblof.Name = "lblof"
        Me.lblof.Size = New System.Drawing.Size(25, 15)
        Me.lblof.TabIndex = 158
        Me.lblof.Text = "OF."
        '
        'grdmerge
        '
        Me.grdmerge.AllowUserToAddRows = False
        Me.grdmerge.AllowUserToDeleteRows = False
        Me.grdmerge.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdmerge.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.grdmerge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdmerge.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.Column5, Me.Column16, Me.Column6})
        Me.grdmerge.Location = New System.Drawing.Point(402, 15)
        Me.grdmerge.MultiSelect = False
        Me.grdmerge.Name = "grdmerge"
        Me.grdmerge.ReadOnly = True
        Me.grdmerge.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.grdmerge.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdmerge.Size = New System.Drawing.Size(766, 240)
        Me.grdmerge.TabIndex = 169
        '
        'Column4
        '
        DataGridViewCellStyle6.Format = "yyyy/MM/dd"
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle6
        Me.Column4.HeaderText = "Production Date"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 110
        '
        'Column5
        '
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle7
        Me.Column5.HeaderText = "Item Name"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 300
        '
        'Column16
        '
        Me.Column16.HeaderText = "Qty"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        '
        'Column6
        '
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle8
        Me.Column6.HeaderText = "Ticket Range"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 200
        '
        'grdlist
        '
        Me.grdlist.AllowUserToAddRows = False
        Me.grdlist.AllowUserToDeleteRows = False
        Me.grdlist.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdlist.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.grdlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdlist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column7, Me.Column8, Me.Column9, Me.Column10, Me.Column11})
        Me.grdlist.Location = New System.Drawing.Point(402, 140)
        Me.grdlist.MultiSelect = False
        Me.grdlist.Name = "grdlist"
        Me.grdlist.ReadOnly = True
        Me.grdlist.RowHeadersWidth = 10
        Me.grdlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdlist.Size = New System.Drawing.Size(766, 115)
        Me.grdlist.TabIndex = 171
        Me.grdlist.Visible = False
        '
        'Column7
        '
        Me.Column7.HeaderText = "id"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Visible = False
        '
        'Column8
        '
        DataGridViewCellStyle9.Format = "yyyy/MM/dd"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.Column8.DefaultCellStyle = DataGridViewCellStyle9
        Me.Column8.HeaderText = "Date"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "Itemname"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 200
        '
        'Column10
        '
        Me.Column10.HeaderText = "Letter"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Width = 60
        '
        'Column11
        '
        Me.Column11.HeaderText = "Ticketnum"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        '
        'list1
        '
        Me.list1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.list1.FormattingEnabled = True
        Me.list1.Location = New System.Drawing.Point(1001, 15)
        Me.list1.Name = "list1"
        Me.list1.Size = New System.Drawing.Size(167, 264)
        Me.list1.TabIndex = 172
        Me.list1.Visible = False
        '
        'list2
        '
        Me.list2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.list2.FormattingEnabled = True
        Me.list2.Location = New System.Drawing.Point(1001, 283)
        Me.list2.Name = "list2"
        Me.list2.Size = New System.Drawing.Size(167, 251)
        Me.list2.TabIndex = 173
        Me.list2.Visible = False
        '
        'grdletters
        '
        Me.grdletters.AllowUserToAddRows = False
        Me.grdletters.AllowUserToDeleteRows = False
        Me.grdletters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdletters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdletters.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column14, Me.Column12, Me.Column15, Me.Column13, Me.Column17, Me.Column18})
        Me.grdletters.Location = New System.Drawing.Point(402, 285)
        Me.grdletters.Name = "grdletters"
        Me.grdletters.ReadOnly = True
        Me.grdletters.RowHeadersWidth = 10
        Me.grdletters.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdletters.Size = New System.Drawing.Size(766, 249)
        Me.grdletters.TabIndex = 174
        '
        'Column14
        '
        Me.Column14.HeaderText = "Letter"
        Me.Column14.Name = "Column14"
        Me.Column14.ReadOnly = True
        '
        'Column12
        '
        Me.Column12.HeaderText = "From"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        Me.Column12.Width = 140
        '
        'Column15
        '
        Me.Column15.HeaderText = "Letter"
        Me.Column15.Name = "Column15"
        Me.Column15.ReadOnly = True
        '
        'Column13
        '
        Me.Column13.HeaderText = "To"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        Me.Column13.Width = 140
        '
        'Column17
        '
        DataGridViewCellStyle10.Format = "yyyy/MM/dd"
        Me.Column17.DefaultCellStyle = DataGridViewCellStyle10
        Me.Column17.HeaderText = "Date"
        Me.Column17.Name = "Column17"
        Me.Column17.ReadOnly = True
        Me.Column17.Visible = False
        '
        'Column18
        '
        Me.Column18.HeaderText = "Item"
        Me.Column18.Name = "Column18"
        Me.Column18.ReadOnly = True
        Me.Column18.Visible = False
        '
        'btntransfer
        '
        Me.btntransfer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btntransfer.Image = CType(resources.GetObject("btntransfer.Image"), System.Drawing.Image)
        Me.btntransfer.Location = New System.Drawing.Point(972, 540)
        Me.btntransfer.Name = "btntransfer"
        Me.btntransfer.Size = New System.Drawing.Size(95, 23)
        Me.btntransfer.TabIndex = 170
        Me.btntransfer.Text = "Transfer"
        Me.btntransfer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btntransfer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btntransfer.UseVisualStyleBackColor = True
        Me.btntransfer.Visible = False
        '
        'btngen
        '
        Me.btngen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btngen.Image = CType(resources.GetObject("btngen.Image"), System.Drawing.Image)
        Me.btngen.Location = New System.Drawing.Point(302, 540)
        Me.btngen.Name = "btngen"
        Me.btngen.Size = New System.Drawing.Size(95, 23)
        Me.btngen.TabIndex = 168
        Me.btngen.Text = "Generate"
        Me.btngen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btngen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btngen.UseVisualStyleBackColor = True
        '
        'txtrems
        '
        Me.txtrems.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtrems.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtrems.Location = New System.Drawing.Point(15, 432)
        Me.txtrems.Multiline = True
        Me.txtrems.Name = "txtrems"
        Me.txtrems.Size = New System.Drawing.Size(381, 102)
        Me.txtrems.TabIndex = 175
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(12, 413)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 16)
        Me.Label2.TabIndex = 176
        Me.Label2.Text = "Remarks:"
        '
        'btnconfirm
        '
        Me.btnconfirm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnconfirm.Image = CType(resources.GetObject("btnconfirm.Image"), System.Drawing.Image)
        Me.btnconfirm.Location = New System.Drawing.Point(1073, 540)
        Me.btnconfirm.Name = "btnconfirm"
        Me.btnconfirm.Size = New System.Drawing.Size(95, 23)
        Me.btnconfirm.TabIndex = 177
        Me.btnconfirm.Text = "Confirm"
        Me.btnconfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnconfirm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnconfirm.UseVisualStyleBackColor = True
        '
        'cmbwhse
        '
        Me.cmbwhse.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbwhse.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbwhse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbwhse.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbwhse.FormattingEnabled = True
        Me.cmbwhse.Location = New System.Drawing.Point(107, 11)
        Me.cmbwhse.Name = "cmbwhse"
        Me.cmbwhse.Size = New System.Drawing.Size(289, 23)
        Me.cmbwhse.TabIndex = 179
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(12, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 16)
        Me.Label3.TabIndex = 178
        Me.Label3.Text = "Whse:"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(402, 263)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 16)
        Me.Label4.TabIndex = 180
        Me.Label4.Text = "Ticket Range:"
        '
        'receive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1180, 575)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbwhse)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnconfirm)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtrems)
        Me.Controls.Add(Me.grdletters)
        Me.Controls.Add(Me.list2)
        Me.Controls.Add(Me.list1)
        Me.Controls.Add(Me.grdlist)
        Me.Controls.Add(Me.btntransfer)
        Me.Controls.Add(Me.grdmerge)
        Me.Controls.Add(Me.btngen)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txttrans)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbbr)
        Me.Controls.Add(Me.Label5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "receive"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Receive"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdof, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdmerge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdletters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txttrans As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbbr As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grdof As System.Windows.Forms.DataGridView
    Friend WithEvents txtof As System.Windows.Forms.TextBox
    Friend WithEvents lblof As System.Windows.Forms.Label
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btngen As System.Windows.Forms.Button
    Friend WithEvents grdmerge As System.Windows.Forms.DataGridView
    Friend WithEvents btntransfer As System.Windows.Forms.Button
    Friend WithEvents txtwrs As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdlist As System.Windows.Forms.DataGridView
    Friend WithEvents list1 As System.Windows.Forms.ListBox
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents list2 As System.Windows.Forms.ListBox
    Friend WithEvents grdletters As System.Windows.Forms.DataGridView
    Friend WithEvents txtrems As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Column14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnconfirm As System.Windows.Forms.Button
    Friend WithEvents cmbwhse As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
