<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class orderfillreturn
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(orderfillreturn))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtofitemid = New System.Windows.Forms.TextBox
        Me.cmbitem = New System.Windows.Forms.ComboBox
        Me.txtofnum = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtofid = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtlet = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtticket = New System.Windows.Forms.TextBox
        Me.rball = New System.Windows.Forms.RadioButton
        Me.rbsome = New System.Windows.Forms.RadioButton
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectTicketToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.grdtickets = New System.Windows.Forms.DataGridView
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewLinkColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewLinkColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column23 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Button1 = New System.Windows.Forms.Button
        Me.list1 = New System.Windows.Forms.ListBox
        Me.list2 = New System.Windows.Forms.ListBox
        Me.grdgoods = New System.Windows.Forms.DataGridView
        Me.grdlog = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnsearch = New System.Windows.Forms.Button
        Me.btncancel = New System.Windows.Forms.Button
        Me.btnreturn = New System.Windows.Forms.Button
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.grdtickets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdgoods, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdlog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "OF Item ID:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(331, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Item Name::"
        '
        'txtofitemid
        '
        Me.txtofitemid.BackColor = System.Drawing.Color.White
        Me.txtofitemid.Location = New System.Drawing.Point(88, 38)
        Me.txtofitemid.Name = "txtofitemid"
        Me.txtofitemid.ReadOnly = True
        Me.txtofitemid.Size = New System.Drawing.Size(183, 20)
        Me.txtofitemid.TabIndex = 2
        '
        'cmbitem
        '
        Me.cmbitem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbitem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbitem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbitem.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbitem.FormattingEnabled = True
        Me.cmbitem.Location = New System.Drawing.Point(410, 39)
        Me.cmbitem.Name = "cmbitem"
        Me.cmbitem.Size = New System.Drawing.Size(300, 21)
        Me.cmbitem.TabIndex = 3
        '
        'txtofnum
        '
        Me.txtofnum.BackColor = System.Drawing.Color.White
        Me.txtofnum.Location = New System.Drawing.Point(410, 13)
        Me.txtofnum.Name = "txtofnum"
        Me.txtofnum.ReadOnly = True
        Me.txtofnum.Size = New System.Drawing.Size(183, 20)
        Me.txtofnum.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(331, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Order Fill#:"
        '
        'txtofid
        '
        Me.txtofid.BackColor = System.Drawing.Color.White
        Me.txtofid.Location = New System.Drawing.Point(88, 12)
        Me.txtofid.Name = "txtofid"
        Me.txtofid.ReadOnly = True
        Me.txtofid.Size = New System.Drawing.Size(183, 20)
        Me.txtofid.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Order Fill ID:"
        '
        'txtlet
        '
        Me.txtlet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtlet.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtlet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlet.Location = New System.Drawing.Point(932, 528)
        Me.txtlet.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.txtlet.Name = "txtlet"
        Me.txtlet.Size = New System.Drawing.Size(28, 20)
        Me.txtlet.TabIndex = 103
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(881, 531)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 14)
        Me.Label8.TabIndex = 102
        Me.Label8.Text = "Ticket #:"
        '
        'txtticket
        '
        Me.txtticket.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtticket.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtticket.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtticket.Location = New System.Drawing.Point(964, 528)
        Me.txtticket.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.txtticket.Name = "txtticket"
        Me.txtticket.Size = New System.Drawing.Size(147, 20)
        Me.txtticket.TabIndex = 104
        '
        'rball
        '
        Me.rball.AutoSize = True
        Me.rball.Location = New System.Drawing.Point(777, 14)
        Me.rball.Name = "rball"
        Me.rball.Size = New System.Drawing.Size(36, 17)
        Me.rball.TabIndex = 105
        Me.rball.TabStop = True
        Me.rball.Text = "All"
        Me.rball.UseVisualStyleBackColor = True
        Me.rball.Visible = False
        '
        'rbsome
        '
        Me.rbsome.AutoSize = True
        Me.rbsome.BackColor = System.Drawing.SystemColors.Control
        Me.rbsome.Location = New System.Drawing.Point(777, 40)
        Me.rbsome.Name = "rbsome"
        Me.rbsome.Size = New System.Drawing.Size(52, 17)
        Me.rbsome.TabIndex = 106
        Me.rbsome.TabStop = True
        Me.rbsome.Text = "Some"
        Me.rbsome.UseVisualStyleBackColor = False
        Me.rbsome.Visible = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectTicketToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(140, 26)
        '
        'SelectTicketToolStripMenuItem
        '
        Me.SelectTicketToolStripMenuItem.Image = CType(resources.GetObject("SelectTicketToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SelectTicketToolStripMenuItem.Name = "SelectTicketToolStripMenuItem"
        Me.SelectTicketToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.SelectTicketToolStripMenuItem.Text = "Select Ticket"
        '
        'grdtickets
        '
        Me.grdtickets.AllowUserToAddRows = False
        Me.grdtickets.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grdtickets.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdtickets.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdtickets.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdtickets.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdtickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdtickets.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column6, Me.DataGridViewTextBoxColumn1, Me.DataGridViewLinkColumn1, Me.DataGridViewLinkColumn2, Me.DataGridViewTextBoxColumn4, Me.Column15, Me.Column16, Me.Column23})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdtickets.DefaultCellStyle = DataGridViewCellStyle4
        Me.grdtickets.EnableHeadersVisualStyles = False
        Me.grdtickets.Location = New System.Drawing.Point(24, 294)
        Me.grdtickets.MultiSelect = False
        Me.grdtickets.Name = "grdtickets"
        Me.grdtickets.ReadOnly = True
        Me.grdtickets.RowHeadersWidth = 10
        Me.grdtickets.Size = New System.Drawing.Size(942, 219)
        Me.grdtickets.TabIndex = 107
        Me.grdtickets.Visible = False
        '
        'Column6
        '
        Me.Column6.HeaderText = "oflogid"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewLinkColumn1
        '
        Me.DataGridViewLinkColumn1.HeaderText = "Ticket Log Sheet"
        Me.DataGridViewLinkColumn1.Name = "DataGridViewLinkColumn1"
        Me.DataGridViewLinkColumn1.ReadOnly = True
        Me.DataGridViewLinkColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewLinkColumn1.Width = 170
        '
        'DataGridViewLinkColumn2
        '
        Me.DataGridViewLinkColumn2.HeaderText = "Pallet Tag #"
        Me.DataGridViewLinkColumn2.Name = "DataGridViewLinkColumn2"
        Me.DataGridViewLinkColumn2.ReadOnly = True
        Me.DataGridViewLinkColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle3.NullValue = Nothing
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn4.HeaderText = "Ticket Number"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 150
        '
        'Column15
        '
        Me.Column15.HeaderText = "number"
        Me.Column15.Name = "Column15"
        Me.Column15.ReadOnly = True
        '
        'Column16
        '
        Me.Column16.HeaderText = "Letter"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        '
        'Column23
        '
        Me.Column23.HeaderText = "Picked"
        Me.Column23.Name = "Column23"
        Me.Column23.ReadOnly = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(472, 490)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 108
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'list1
        '
        Me.list1.FormattingEnabled = True
        Me.list1.Location = New System.Drawing.Point(990, 278)
        Me.list1.Name = "list1"
        Me.list1.Size = New System.Drawing.Size(120, 95)
        Me.list1.TabIndex = 109
        Me.list1.Visible = False
        '
        'list2
        '
        Me.list2.FormattingEnabled = True
        Me.list2.Location = New System.Drawing.Point(990, 391)
        Me.list2.Name = "list2"
        Me.list2.Size = New System.Drawing.Size(120, 95)
        Me.list2.TabIndex = 110
        Me.list2.Visible = False
        '
        'grdgoods
        '
        Me.grdgoods.AllowUserToAddRows = False
        Me.grdgoods.AllowUserToDeleteRows = False
        Me.grdgoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdgoods.Location = New System.Drawing.Point(24, 408)
        Me.grdgoods.Name = "grdgoods"
        Me.grdgoods.ReadOnly = True
        Me.grdgoods.Size = New System.Drawing.Size(240, 105)
        Me.grdgoods.TabIndex = 111
        Me.grdgoods.Visible = False
        '
        'grdlog
        '
        Me.grdlog.AllowUserToAddRows = False
        Me.grdlog.AllowUserToDeleteRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grdlog.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.grdlog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdlog.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.MintCream
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Arial", 8.0!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdlog.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdlog.ColumnHeadersHeight = 40
        Me.grdlog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdlog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column11, Me.Column2, Me.Column8, Me.Column7, Me.Column12, Me.Column13, Me.Column4, Me.Column10, Me.Column9, Me.Column3, Me.Column5})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Arial", 8.0!)
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdlog.DefaultCellStyle = DataGridViewCellStyle12
        Me.grdlog.EnableHeadersVisualStyles = False
        Me.grdlog.Location = New System.Drawing.Point(12, 72)
        Me.grdlog.Name = "grdlog"
        Me.grdlog.ReadOnly = True
        Me.grdlog.RowHeadersWidth = 10
        Me.grdlog.Size = New System.Drawing.Size(1134, 441)
        Me.grdlog.TabIndex = 112
        '
        'Column1
        '
        Me.Column1.HeaderText = "OF Log ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 80
        '
        'Column11
        '
        DataGridViewCellStyle7.Format = "yyyy/MM/dd"
        Me.Column11.DefaultCellStyle = DataGridViewCellStyle7
        Me.Column11.HeaderText = "Prod. Date"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        Me.Column11.Width = 80
        '
        'Column2
        '
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle8
        Me.Column2.HeaderText = "Ticket Log Sheet"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column2.Width = 150
        '
        'Column8
        '
        Me.Column8.HeaderText = "Pallet Tag ID"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column7
        '
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column7.DefaultCellStyle = DataGridViewCellStyle9
        Me.Column7.HeaderText = "Pallet Tag #"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column7.Width = 150
        '
        'Column12
        '
        Me.Column12.HeaderText = "Start Ticket"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        Me.Column12.Visible = False
        '
        'Column13
        '
        Me.Column13.HeaderText = "End Ticket"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        Me.Column13.Visible = False
        '
        'Column4
        '
        DataGridViewCellStyle10.Format = "N2"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle10
        Me.Column4.HeaderText = "Available No. of Bags"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 110
        '
        'Column10
        '
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column10.DefaultCellStyle = DataGridViewCellStyle11
        Me.Column10.HeaderText = "Ticket Series"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Width = 150
        '
        'Column9
        '
        Me.Column9.HeaderText = "Select"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Visible = False
        Me.Column9.Width = 70
        '
        'Column3
        '
        Me.Column3.HeaderText = "No. Selected Bags"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 110
        '
        'Column5
        '
        Me.Column5.HeaderText = "Selected Ticket Series"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 150
        '
        'btnsearch
        '
        Me.btnsearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnsearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsearch.Image = CType(resources.GetObject("btnsearch.Image"), System.Drawing.Image)
        Me.btnsearch.Location = New System.Drawing.Point(1116, 526)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(30, 23)
        Me.btnsearch.TabIndex = 105
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'btncancel
        '
        Me.btncancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btncancel.Image = Global.Logsheet.My.Resources.Resources.cancel
        Me.btncancel.Location = New System.Drawing.Point(1050, 40)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(96, 23)
        Me.btncancel.TabIndex = 7
        Me.btncancel.Text = "Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'btnreturn
        '
        Me.btnreturn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnreturn.Image = CType(resources.GetObject("btnreturn.Image"), System.Drawing.Image)
        Me.btnreturn.Location = New System.Drawing.Point(1050, 10)
        Me.btnreturn.Name = "btnreturn"
        Me.btnreturn.Size = New System.Drawing.Size(96, 23)
        Me.btnreturn.TabIndex = 6
        Me.btnreturn.Text = "Return"
        Me.btnreturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnreturn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnreturn.UseVisualStyleBackColor = True
        '
        'orderfillreturn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1158, 561)
        Me.Controls.Add(Me.grdlog)
        Me.Controls.Add(Me.grdgoods)
        Me.Controls.Add(Me.list2)
        Me.Controls.Add(Me.list1)
        Me.Controls.Add(Me.grdtickets)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.rbsome)
        Me.Controls.Add(Me.rball)
        Me.Controls.Add(Me.btnsearch)
        Me.Controls.Add(Me.txtofnum)
        Me.Controls.Add(Me.txtlet)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtticket)
        Me.Controls.Add(Me.txtofid)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbitem)
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.btnreturn)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtofitemid)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "orderfillreturn"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Return Items"
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.grdtickets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdgoods, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdlog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtofitemid As System.Windows.Forms.TextBox
    Friend WithEvents cmbitem As System.Windows.Forms.ComboBox
    Friend WithEvents btnreturn As System.Windows.Forms.Button
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents txtofnum As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtofid As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtlet As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtticket As System.Windows.Forms.TextBox
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents rball As System.Windows.Forms.RadioButton
    Friend WithEvents rbsome As System.Windows.Forms.RadioButton
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectTicketToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grdtickets As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewLinkColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewLinkColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents list1 As System.Windows.Forms.ListBox
    Friend WithEvents list2 As System.Windows.Forms.ListBox
    Friend WithEvents grdgoods As System.Windows.Forms.DataGridView
    Friend WithEvents grdlog As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
