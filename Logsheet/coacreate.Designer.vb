<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class coacreate
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(coacreate))
        Me.gselect = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbformat = New System.Windows.Forms.ComboBox
        Me.grdmerge = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lblofid = New System.Windows.Forms.Label
        Me.grdcoa = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txtcoanum = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnclose = New System.Windows.Forms.Button
        Me.btnok = New System.Windows.Forms.Button
        Me.btnwrssearch = New System.Windows.Forms.Button
        Me.txtwrsnum = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.lblofnum = New System.Windows.Forms.Label
        Me.lblitemid = New System.Windows.Forms.Label
        Me.btnsearch = New System.Windows.Forms.Button
        Me.cmbitem = New System.Windows.Forms.ComboBox
        Me.txtofnum = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.OFToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditNoOfSelectedBagsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.gselect.SuspendLayout()
        CType(Me.grdmerge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdcoa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gselect
        '
        Me.gselect.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gselect.Controls.Add(Me.Label4)
        Me.gselect.Controls.Add(Me.cmbformat)
        Me.gselect.Controls.Add(Me.grdmerge)
        Me.gselect.Controls.Add(Me.lblofid)
        Me.gselect.Controls.Add(Me.grdcoa)
        Me.gselect.Controls.Add(Me.txtcoanum)
        Me.gselect.Controls.Add(Me.Label3)
        Me.gselect.Controls.Add(Me.btnclose)
        Me.gselect.Controls.Add(Me.btnok)
        Me.gselect.Controls.Add(Me.btnwrssearch)
        Me.gselect.Controls.Add(Me.txtwrsnum)
        Me.gselect.Controls.Add(Me.Label20)
        Me.gselect.Controls.Add(Me.lblofnum)
        Me.gselect.Controls.Add(Me.lblitemid)
        Me.gselect.Controls.Add(Me.btnsearch)
        Me.gselect.Controls.Add(Me.cmbitem)
        Me.gselect.Controls.Add(Me.txtofnum)
        Me.gselect.Controls.Add(Me.Label1)
        Me.gselect.Controls.Add(Me.Label2)
        Me.gselect.Location = New System.Drawing.Point(12, 28)
        Me.gselect.Name = "gselect"
        Me.gselect.Size = New System.Drawing.Size(750, 537)
        Me.gselect.TabIndex = 3
        Me.gselect.TabStop = False
        Me.gselect.Text = "Select"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(441, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 15)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "COA Format:"
        '
        'cmbformat
        '
        Me.cmbformat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbformat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbformat.BackColor = System.Drawing.Color.PaleTurquoise
        Me.cmbformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbformat.Enabled = False
        Me.cmbformat.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbformat.FormattingEnabled = True
        Me.cmbformat.Location = New System.Drawing.Point(523, 23)
        Me.cmbformat.Name = "cmbformat"
        Me.cmbformat.Size = New System.Drawing.Size(205, 23)
        Me.cmbformat.TabIndex = 37
        '
        'grdmerge
        '
        Me.grdmerge.AllowUserToAddRows = False
        Me.grdmerge.AllowUserToDeleteRows = False
        Me.grdmerge.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.grdmerge.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdmerge.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdmerge.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdmerge.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdmerge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdmerge.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdmerge.DefaultCellStyle = DataGridViewCellStyle5
        Me.grdmerge.EnableHeadersVisualStyles = False
        Me.grdmerge.Location = New System.Drawing.Point(26, 320)
        Me.grdmerge.MultiSelect = False
        Me.grdmerge.Name = "grdmerge"
        Me.grdmerge.ReadOnly = True
        Me.grdmerge.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdmerge.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdmerge.RowHeadersWidth = 10
        Me.grdmerge.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdmerge.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdmerge.Size = New System.Drawing.Size(702, 160)
        Me.grdmerge.TabIndex = 36
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Format = "yyyy/MM/dd"
        DataGridViewCellStyle3.NullValue = "yyyy/MM/dd"
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn1.HeaderText = "Production Date"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn2.HeaderText = "Ticket Series"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 550
        '
        'lblofid
        '
        Me.lblofid.AutoSize = True
        Me.lblofid.Location = New System.Drawing.Point(397, 79)
        Me.lblofid.Name = "lblofid"
        Me.lblofid.Size = New System.Drawing.Size(0, 15)
        Me.lblofid.TabIndex = 35
        Me.lblofid.Visible = False
        '
        'grdcoa
        '
        Me.grdcoa.AllowUserToAddRows = False
        Me.grdcoa.AllowUserToDeleteRows = False
        Me.grdcoa.AllowUserToResizeRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.grdcoa.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.grdcoa.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdcoa.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdcoa.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.grdcoa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdcoa.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4})
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdcoa.DefaultCellStyle = DataGridViewCellStyle13
        Me.grdcoa.EnableHeadersVisualStyles = False
        Me.grdcoa.Location = New System.Drawing.Point(26, 143)
        Me.grdcoa.MultiSelect = False
        Me.grdcoa.Name = "grdcoa"
        Me.grdcoa.ReadOnly = True
        Me.grdcoa.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdcoa.RowHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.grdcoa.RowHeadersWidth = 10
        Me.grdcoa.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdcoa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdcoa.Size = New System.Drawing.Size(702, 171)
        Me.grdcoa.TabIndex = 33
        '
        'Column1
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.Format = "yyyy/MM/dd"
        DataGridViewCellStyle9.NullValue = "yyyy/MM/dd"
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle9
        Me.Column1.HeaderText = "Production Date"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 120
        '
        'Column2
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle10
        Me.Column2.HeaderText = "Ticket Series"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 550
        '
        'Column3
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle11
        Me.Column3.HeaderText = "Expiry Date"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Visible = False
        '
        'Column4
        '
        DataGridViewCellStyle12.Format = "yyyy/MM/dd"
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle12
        Me.Column4.HeaderText = "OF Prod"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Visible = False
        '
        'txtcoanum
        '
        Me.txtcoanum.BackColor = System.Drawing.Color.PaleTurquoise
        Me.txtcoanum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcoanum.Location = New System.Drawing.Point(135, 23)
        Me.txtcoanum.Name = "txtcoanum"
        Me.txtcoanum.ReadOnly = True
        Me.txtcoanum.Size = New System.Drawing.Size(247, 21)
        Me.txtcoanum.TabIndex = 34
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 15)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "COA #:"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.Color.Gainsboro
        Me.btnclose.Image = CType(resources.GetObject("btnclose.Image"), System.Drawing.Image)
        Me.btnclose.Location = New System.Drawing.Point(653, 499)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(75, 23)
        Me.btnclose.TabIndex = 32
        Me.btnclose.Text = "Close"
        Me.btnclose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'btnok
        '
        Me.btnok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnok.BackColor = System.Drawing.Color.Gainsboro
        Me.btnok.Image = CType(resources.GetObject("btnok.Image"), System.Drawing.Image)
        Me.btnok.Location = New System.Drawing.Point(563, 499)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(75, 23)
        Me.btnok.TabIndex = 31
        Me.btnok.Text = "Create"
        Me.btnok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnok.UseVisualStyleBackColor = False
        '
        'btnwrssearch
        '
        Me.btnwrssearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnwrssearch.Image = CType(resources.GetObject("btnwrssearch.Image"), System.Drawing.Image)
        Me.btnwrssearch.Location = New System.Drawing.Point(353, 48)
        Me.btnwrssearch.Name = "btnwrssearch"
        Me.btnwrssearch.Size = New System.Drawing.Size(30, 23)
        Me.btnwrssearch.TabIndex = 29
        Me.btnwrssearch.UseVisualStyleBackColor = True
        '
        'txtwrsnum
        '
        Me.txtwrsnum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtwrsnum.Location = New System.Drawing.Point(135, 50)
        Me.txtwrsnum.Name = "txtwrsnum"
        Me.txtwrsnum.ReadOnly = True
        Me.txtwrsnum.Size = New System.Drawing.Size(211, 21)
        Me.txtwrsnum.TabIndex = 30
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(23, 53)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(48, 15)
        Me.Label20.TabIndex = 28
        Me.Label20.Text = "WRS #:"
        '
        'lblofnum
        '
        Me.lblofnum.AutoSize = True
        Me.lblofnum.Location = New System.Drawing.Point(132, 79)
        Me.lblofnum.Name = "lblofnum"
        Me.lblofnum.Size = New System.Drawing.Size(25, 15)
        Me.lblofnum.TabIndex = 27
        Me.lblofnum.Text = "OF."
        '
        'lblitemid
        '
        Me.lblitemid.AutoSize = True
        Me.lblitemid.Location = New System.Drawing.Point(95, 106)
        Me.lblitemid.Name = "lblitemid"
        Me.lblitemid.Size = New System.Drawing.Size(0, 15)
        Me.lblitemid.TabIndex = 26
        Me.lblitemid.Visible = False
        '
        'btnsearch
        '
        Me.btnsearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsearch.Image = CType(resources.GetObject("btnsearch.Image"), System.Drawing.Image)
        Me.btnsearch.Location = New System.Drawing.Point(353, 74)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(30, 23)
        Me.btnsearch.TabIndex = 8
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'cmbitem
        '
        Me.cmbitem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbitem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbitem.BackColor = System.Drawing.Color.MistyRose
        Me.cmbitem.Enabled = False
        Me.cmbitem.FormattingEnabled = True
        Me.cmbitem.Location = New System.Drawing.Point(135, 103)
        Me.cmbitem.Name = "cmbitem"
        Me.cmbitem.Size = New System.Drawing.Size(247, 23)
        Me.cmbitem.TabIndex = 25
        '
        'txtofnum
        '
        Me.txtofnum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtofnum.Location = New System.Drawing.Point(162, 76)
        Me.txtofnum.Name = "txtofnum"
        Me.txtofnum.ReadOnly = True
        Me.txtofnum.Size = New System.Drawing.Size(184, 21)
        Me.txtofnum.TabIndex = 25
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 106)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Item:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Order Fill #:"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OFToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(774, 25)
        Me.ToolStrip1.TabIndex = 12
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'OFToolStripButton1
        '
        Me.OFToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OFToolStripButton1.Image = CType(resources.GetObject("OFToolStripButton1.Image"), System.Drawing.Image)
        Me.OFToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OFToolStripButton1.Name = "OFToolStripButton1"
        Me.OFToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.OFToolStripButton1.Text = "Select Order Fill"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditNoOfSelectedBagsToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(184, 26)
        '
        'EditNoOfSelectedBagsToolStripMenuItem
        '
        Me.EditNoOfSelectedBagsToolStripMenuItem.Image = CType(resources.GetObject("EditNoOfSelectedBagsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.EditNoOfSelectedBagsToolStripMenuItem.Name = "EditNoOfSelectedBagsToolStripMenuItem"
        Me.EditNoOfSelectedBagsToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.EditNoOfSelectedBagsToolStripMenuItem.Text = "Edit Production Date"
        '
        'coacreate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(774, 577)
        Me.ControlBox = False
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.gselect)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "coacreate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create New COA"
        Me.gselect.ResumeLayout(False)
        Me.gselect.PerformLayout()
        CType(Me.grdmerge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdcoa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gselect As System.Windows.Forms.GroupBox
    Friend WithEvents btnwrssearch As System.Windows.Forms.Button
    Friend WithEvents txtwrsnum As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblofnum As System.Windows.Forms.Label
    Friend WithEvents lblitemid As System.Windows.Forms.Label
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents cmbitem As System.Windows.Forms.ComboBox
    Friend WithEvents txtofnum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents OFToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents txtcoanum As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grdcoa As System.Windows.Forms.DataGridView
    Friend WithEvents lblofid As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EditNoOfSelectedBagsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grdmerge As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbformat As System.Windows.Forms.ComboBox
End Class
