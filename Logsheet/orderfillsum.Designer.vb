<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class orderfillsum
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(orderfillsum))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtofid = New System.Windows.Forms.TextBox
        Me.cmbcus = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtwrs = New System.Windows.Forms.TextBox
        Me.lblcount = New System.Windows.Forms.Label
        Me.chkhide = New System.Windows.Forms.CheckBox
        Me.btnexport = New System.Windows.Forms.Button
        Me.btnsearch = New System.Windows.Forms.Button
        Me.btnview = New System.Windows.Forms.Button
        Me.lblof = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtofnum = New System.Windows.Forms.TextBox
        Me.cmbshift = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dateto = New System.Windows.Forms.DateTimePicker
        Me.grdof = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewLinkColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column12 = New System.Windows.Forms.DataGridViewLinkColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column14 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column17 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column18 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column19 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label5 = New System.Windows.Forms.Label
        Me.datefrom = New System.Windows.Forms.DateTimePicker
        Me.cmbwhse = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CancelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReturnItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AllQuantityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SomeQuantityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.pgb1 = New System.Windows.Forms.ProgressBar
        Me.lblloading = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.grdof, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtofid)
        Me.Panel1.Controls.Add(Me.cmbcus)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtwrs)
        Me.Panel1.Controls.Add(Me.lblcount)
        Me.Panel1.Controls.Add(Me.chkhide)
        Me.Panel1.Controls.Add(Me.btnexport)
        Me.Panel1.Controls.Add(Me.btnsearch)
        Me.Panel1.Controls.Add(Me.btnview)
        Me.Panel1.Controls.Add(Me.lblof)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtofnum)
        Me.Panel1.Controls.Add(Me.cmbshift)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.dateto)
        Me.Panel1.Controls.Add(Me.grdof)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.datefrom)
        Me.Panel1.Controls.Add(Me.cmbwhse)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1244, 661)
        Me.Panel1.TabIndex = 93
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(730, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 15)
        Me.Label9.TabIndex = 97
        Me.Label9.Text = "Order Fill ID:"
        '
        'txtofid
        '
        Me.txtofid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtofid.Location = New System.Drawing.Point(837, 13)
        Me.txtofid.Name = "txtofid"
        Me.txtofid.Size = New System.Drawing.Size(146, 20)
        Me.txtofid.TabIndex = 80
        '
        'cmbcus
        '
        Me.cmbcus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbcus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbcus.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmbcus.FormattingEnabled = True
        Me.cmbcus.Location = New System.Drawing.Point(500, 39)
        Me.cmbcus.Name = "cmbcus"
        Me.cmbcus.Size = New System.Drawing.Size(210, 23)
        Me.cmbcus.TabIndex = 79
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(429, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 15)
        Me.Label1.TabIndex = 94
        Me.Label1.Text = "Customer:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(429, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 15)
        Me.Label8.TabIndex = 93
        Me.Label8.Text = "WRS #:"
        '
        'txtwrs
        '
        Me.txtwrs.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtwrs.Location = New System.Drawing.Point(500, 13)
        Me.txtwrs.Name = "txtwrs"
        Me.txtwrs.Size = New System.Drawing.Size(210, 20)
        Me.txtwrs.TabIndex = 78
        '
        'lblcount
        '
        Me.lblcount.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblcount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcount.Location = New System.Drawing.Point(0, 636)
        Me.lblcount.Name = "lblcount"
        Me.lblcount.Size = New System.Drawing.Size(1244, 25)
        Me.lblcount.TabIndex = 91
        Me.lblcount.Text = "Count:"
        '
        'chkhide
        '
        Me.chkhide.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkhide.AutoSize = True
        Me.chkhide.Location = New System.Drawing.Point(1138, 60)
        Me.chkhide.Name = "chkhide"
        Me.chkhide.Size = New System.Drawing.Size(100, 18)
        Me.chkhide.TabIndex = 85
        Me.chkhide.Text = "Hide Cancelled "
        Me.chkhide.UseVisualStyleBackColor = True
        '
        'btnexport
        '
        Me.btnexport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexport.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.btnexport.Image = CType(resources.GetObject("btnexport.Image"), System.Drawing.Image)
        Me.btnexport.Location = New System.Drawing.Point(1128, 7)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(104, 23)
        Me.btnexport.TabIndex = 84
        Me.btnexport.Text = "Export"
        Me.btnexport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnexport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexport.UseVisualStyleBackColor = True
        '
        'btnsearch
        '
        Me.btnsearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnsearch.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.btnsearch.Image = CType(resources.GetObject("btnsearch.Image"), System.Drawing.Image)
        Me.btnsearch.Location = New System.Drawing.Point(1016, 7)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(106, 23)
        Me.btnsearch.TabIndex = 82
        Me.btnsearch.Text = "Search"
        Me.btnsearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnsearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'btnview
        '
        Me.btnview.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnview.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.btnview.Image = CType(resources.GetObject("btnview.Image"), System.Drawing.Image)
        Me.btnview.Location = New System.Drawing.Point(1016, 34)
        Me.btnview.Name = "btnview"
        Me.btnview.Size = New System.Drawing.Size(216, 23)
        Me.btnview.TabIndex = 83
        Me.btnview.Text = "View All Pending"
        Me.btnview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnview.UseVisualStyleBackColor = True
        '
        'lblof
        '
        Me.lblof.AutoSize = True
        Me.lblof.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblof.Location = New System.Drawing.Point(806, 44)
        Me.lblof.Name = "lblof"
        Me.lblof.Size = New System.Drawing.Size(25, 15)
        Me.lblof.TabIndex = 85
        Me.lblof.Text = "OF."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(730, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 15)
        Me.Label6.TabIndex = 84
        Me.Label6.Text = "Order Fill #:"
        '
        'txtofnum
        '
        Me.txtofnum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtofnum.Location = New System.Drawing.Point(837, 42)
        Me.txtofnum.Name = "txtofnum"
        Me.txtofnum.Size = New System.Drawing.Size(146, 20)
        Me.txtofnum.TabIndex = 81
        '
        'cmbshift
        '
        Me.cmbshift.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbshift.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbshift.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmbshift.FormattingEnabled = True
        Me.cmbshift.Location = New System.Drawing.Point(306, 41)
        Me.cmbshift.Name = "cmbshift"
        Me.cmbshift.Size = New System.Drawing.Size(103, 23)
        Me.cmbshift.TabIndex = 77
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(226, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 15)
        Me.Label4.TabIndex = 81
        Me.Label4.Text = "Shift:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 15)
        Me.Label2.TabIndex = 77
        Me.Label2.Text = "OF Date From:"
        '
        'dateto
        '
        Me.dateto.CustomFormat = "yyyy/MM/dd"
        Me.dateto.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dateto.Location = New System.Drawing.Point(101, 41)
        Me.dateto.Name = "dateto"
        Me.dateto.Size = New System.Drawing.Size(107, 21)
        Me.dateto.TabIndex = 75
        '
        'grdof
        '
        Me.grdof.AllowUserToAddRows = False
        Me.grdof.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grdof.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdof.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdof.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdof.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdof.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdof.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column12, Me.Column5, Me.Column11, Me.Column13, Me.Column14, Me.Column15, Me.Column16, Me.Column10, Me.Column6, Me.Column7, Me.Column8, Me.Column9, Me.Column17, Me.Column18, Me.Column19})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdof.DefaultCellStyle = DataGridViewCellStyle7
        Me.grdof.EnableHeadersVisualStyles = False
        Me.grdof.Location = New System.Drawing.Point(6, 83)
        Me.grdof.Name = "grdof"
        Me.grdof.ReadOnly = True
        Me.grdof.RowHeadersWidth = 10
        Me.grdof.Size = New System.Drawing.Size(1226, 545)
        Me.grdof.TabIndex = 86
        '
        'Column1
        '
        Me.Column1.Frozen = True
        Me.Column1.HeaderText = "ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        DataGridViewCellStyle3.Format = "yyyy/MM/dd"
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column2.Frozen = True
        Me.Column2.HeaderText = "Date"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.Frozen = True
        Me.Column3.HeaderText = "Order Fill #"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Column4
        '
        Me.Column4.Frozen = True
        Me.Column4.HeaderText = "WRS #"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column12
        '
        Me.Column12.HeaderText = "COA #"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        Me.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column12.Visible = False
        '
        'Column5
        '
        Me.Column5.HeaderText = "Customer"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column11
        '
        Me.Column11.HeaderText = "Customer Ref"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        '
        'Column13
        '
        Me.Column13.HeaderText = "Plate number"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        '
        'Column14
        '
        Me.Column14.HeaderText = "Warehouse"
        Me.Column14.Name = "Column14"
        Me.Column14.ReadOnly = True
        '
        'Column15
        '
        Me.Column15.HeaderText = "Material Disposition#"
        Me.Column15.Name = "Column15"
        Me.Column15.ReadOnly = True
        Me.Column15.Width = 130
        '
        'Column16
        '
        Me.Column16.HeaderText = "Remarks"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        '
        'Column10
        '
        Me.Column10.HeaderText = "Status"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Width = 150
        '
        'Column6
        '
        DataGridViewCellStyle4.Format = "yyyy/MM/dd HH:mm"
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column6.HeaderText = "Date Created"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 120
        '
        'Column7
        '
        Me.Column7.HeaderText = "Created by"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 140
        '
        'Column8
        '
        DataGridViewCellStyle5.Format = "yyyy/MM/dd HH:mm"
        Me.Column8.DefaultCellStyle = DataGridViewCellStyle5
        Me.Column8.HeaderText = "Date Modified"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 120
        '
        'Column9
        '
        Me.Column9.HeaderText = "Modified by"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 140
        '
        'Column17
        '
        DataGridViewCellStyle6.Format = "yyyy/MM/dd HH:mm"
        Me.Column17.DefaultCellStyle = DataGridViewCellStyle6
        Me.Column17.HeaderText = "Date Cancelled"
        Me.Column17.Name = "Column17"
        Me.Column17.ReadOnly = True
        Me.Column17.Width = 120
        '
        'Column18
        '
        Me.Column18.HeaderText = "Cancelled by"
        Me.Column18.Name = "Column18"
        Me.Column18.ReadOnly = True
        Me.Column18.Width = 140
        '
        'Column19
        '
        Me.Column19.HeaderText = "Cancel Reason"
        Me.Column19.Name = "Column19"
        Me.Column19.ReadOnly = True
        Me.Column19.Width = 150
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(72, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(23, 15)
        Me.Label5.TabIndex = 78
        Me.Label5.Text = "To:"
        '
        'datefrom
        '
        Me.datefrom.CustomFormat = "yyyy/MM/dd"
        Me.datefrom.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.datefrom.Location = New System.Drawing.Point(101, 14)
        Me.datefrom.Name = "datefrom"
        Me.datefrom.Size = New System.Drawing.Size(107, 21)
        Me.datefrom.TabIndex = 74
        '
        'cmbwhse
        '
        Me.cmbwhse.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbwhse.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbwhse.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmbwhse.FormattingEnabled = True
        Me.cmbwhse.Location = New System.Drawing.Point(306, 12)
        Me.cmbwhse.Name = "cmbwhse"
        Me.cmbwhse.Size = New System.Drawing.Size(103, 23)
        Me.cmbwhse.TabIndex = 76
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(226, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 15)
        Me.Label7.TabIndex = 73
        Me.Label7.Text = "Warehouse:"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripMenuItem, Me.CancelToolStripMenuItem, Me.ReturnItemsToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(162, 70)
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Image = CType(resources.GetObject("PrintToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.PrintToolStripMenuItem.Text = "Print Order Fill"
        '
        'CancelToolStripMenuItem
        '
        Me.CancelToolStripMenuItem.Image = CType(resources.GetObject("CancelToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CancelToolStripMenuItem.Name = "CancelToolStripMenuItem"
        Me.CancelToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.CancelToolStripMenuItem.Text = "Cancel Order Fill"
        '
        'ReturnItemsToolStripMenuItem
        '
        Me.ReturnItemsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AllQuantityToolStripMenuItem, Me.SomeQuantityToolStripMenuItem})
        Me.ReturnItemsToolStripMenuItem.Image = CType(resources.GetObject("ReturnItemsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ReturnItemsToolStripMenuItem.Name = "ReturnItemsToolStripMenuItem"
        Me.ReturnItemsToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.ReturnItemsToolStripMenuItem.Text = "Return Items"
        '
        'AllQuantityToolStripMenuItem
        '
        Me.AllQuantityToolStripMenuItem.Image = CType(resources.GetObject("AllQuantityToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AllQuantityToolStripMenuItem.Name = "AllQuantityToolStripMenuItem"
        Me.AllQuantityToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.AllQuantityToolStripMenuItem.Text = "All Quantity"
        '
        'SomeQuantityToolStripMenuItem
        '
        Me.SomeQuantityToolStripMenuItem.Image = CType(resources.GetObject("SomeQuantityToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SomeQuantityToolStripMenuItem.Name = "SomeQuantityToolStripMenuItem"
        Me.SomeQuantityToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.SomeQuantityToolStripMenuItem.Text = "Some Quantity"
        '
        'pgb1
        '
        Me.pgb1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgb1.Location = New System.Drawing.Point(4, 609)
        Me.pgb1.MarqueeAnimationSpeed = 50
        Me.pgb1.Maximum = 10
        Me.pgb1.Name = "pgb1"
        Me.pgb1.Size = New System.Drawing.Size(1236, 23)
        Me.pgb1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pgb1.TabIndex = 104
        Me.pgb1.Visible = False
        '
        'lblloading
        '
        Me.lblloading.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblloading.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblloading.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblloading.Location = New System.Drawing.Point(4, 105)
        Me.lblloading.Name = "lblloading"
        Me.lblloading.Size = New System.Drawing.Size(1236, 509)
        Me.lblloading.TabIndex = 103
        Me.lblloading.Text = "Loading..."
        Me.lblloading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'orderfillsum
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1244, 661)
        Me.Controls.Add(Me.pgb1)
        Me.Controls.Add(Me.lblloading)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "orderfillsum"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Order Fill"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.grdof, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblcount As System.Windows.Forms.Label
    Friend WithEvents chkhide As System.Windows.Forms.CheckBox
    Friend WithEvents btnexport As System.Windows.Forms.Button
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents btnview As System.Windows.Forms.Button
    Friend WithEvents lblof As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtofnum As System.Windows.Forms.TextBox
    Friend WithEvents cmbshift As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dateto As System.Windows.Forms.DateTimePicker
    Friend WithEvents grdof As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents datefrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbwhse As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtwrs As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CancelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbcus As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtofid As System.Windows.Forms.TextBox
    Friend WithEvents ReturnItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllQuantityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SomeQuantityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pgb1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblloading As System.Windows.Forms.Label
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column12 As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column19 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
