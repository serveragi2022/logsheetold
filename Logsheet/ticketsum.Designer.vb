<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ticketsum
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ticketsum))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grdlog = New System.Windows.Forms.DataGridView()
        Me.dateto = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbwhse = New System.Windows.Forms.ComboBox()
        Me.datefrom = New System.Windows.Forms.DateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbreceive = New System.Windows.Forms.RadioButton()
        Me.rbprod = New System.Windows.Forms.RadioButton()
        Me.pgb1 = New System.Windows.Forms.ProgressBar()
        Me.lblloading = New System.Windows.Forms.Label()
        Me.txtlogsheetid = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbitem = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblcount = New System.Windows.Forms.Label()
        Me.chkhide = New System.Windows.Forms.CheckBox()
        Me.btnexport = New System.Windows.Forms.Button()
        Me.btnsearch = New System.Windows.Forms.Button()
        Me.btnview = New System.Windows.Forms.Button()
        Me.lbltrip = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtlog = New System.Windows.Forms.TextBox()
        Me.cmbshift = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbline = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PrintTicketToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdminConfirmLogsheetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelLogsheetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActivateLogsheetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActivateAdminConfirmationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdateFlourBinToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VerifyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.qca = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.stat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.grdlog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdlog
        '
        Me.grdlog.AllowUserToAddRows = False
        Me.grdlog.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grdlog.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdlog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdlog.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdlog.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdlog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdlog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column11, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column21, Me.Column13, Me.Column20, Me.Column22, Me.Column23, Me.Column6, Me.Column7, Me.Column14, Me.Column16, Me.Column15, Me.Column10, Me.qca, Me.stat, Me.Column12, Me.Column17, Me.Column18, Me.Column19})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdlog.DefaultCellStyle = DataGridViewCellStyle8
        Me.grdlog.EnableHeadersVisualStyles = False
        Me.grdlog.Location = New System.Drawing.Point(6, 83)
        Me.grdlog.Name = "grdlog"
        Me.grdlog.ReadOnly = True
        Me.grdlog.RowHeadersWidth = 10
        Me.grdlog.Size = New System.Drawing.Size(1266, 529)
        Me.grdlog.TabIndex = 83
        '
        'dateto
        '
        Me.dateto.CustomFormat = "yyyy/MM/dd"
        Me.dateto.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dateto.Location = New System.Drawing.Point(144, 42)
        Me.dateto.Name = "dateto"
        Me.dateto.Size = New System.Drawing.Size(108, 21)
        Me.dateto.TabIndex = 75
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(115, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 15)
        Me.Label2.TabIndex = 78
        Me.Label2.Text = "To:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 15)
        Me.Label1.TabIndex = 77
        Me.Label1.Text = "Production Date From:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(271, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 15)
        Me.Label5.TabIndex = 73
        Me.Label5.Text = "Warehouse:"
        '
        'cmbwhse
        '
        Me.cmbwhse.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbwhse.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbwhse.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmbwhse.FormattingEnabled = True
        Me.cmbwhse.Location = New System.Drawing.Point(351, 11)
        Me.cmbwhse.Name = "cmbwhse"
        Me.cmbwhse.Size = New System.Drawing.Size(130, 23)
        Me.cmbwhse.TabIndex = 76
        '
        'datefrom
        '
        Me.datefrom.CustomFormat = "yyyy/MM/dd"
        Me.datefrom.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.datefrom.Location = New System.Drawing.Point(144, 15)
        Me.datefrom.Name = "datefrom"
        Me.datefrom.Size = New System.Drawing.Size(108, 21)
        Me.datefrom.TabIndex = 74
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.rbreceive)
        Me.Panel1.Controls.Add(Me.rbprod)
        Me.Panel1.Controls.Add(Me.pgb1)
        Me.Panel1.Controls.Add(Me.lblloading)
        Me.Panel1.Controls.Add(Me.txtlogsheetid)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.cmbitem)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lblcount)
        Me.Panel1.Controls.Add(Me.chkhide)
        Me.Panel1.Controls.Add(Me.btnexport)
        Me.Panel1.Controls.Add(Me.btnsearch)
        Me.Panel1.Controls.Add(Me.btnview)
        Me.Panel1.Controls.Add(Me.lbltrip)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtlog)
        Me.Panel1.Controls.Add(Me.cmbshift)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.cmbline)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.dateto)
        Me.Panel1.Controls.Add(Me.grdlog)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.datefrom)
        Me.Panel1.Controls.Add(Me.cmbwhse)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1284, 661)
        Me.Panel1.TabIndex = 79
        '
        'rbreceive
        '
        Me.rbreceive.AutoSize = True
        Me.rbreceive.Location = New System.Drawing.Point(11, 60)
        Me.rbreceive.Name = "rbreceive"
        Me.rbreceive.Size = New System.Drawing.Size(65, 17)
        Me.rbreceive.TabIndex = 104
        Me.rbreceive.Text = "Receive"
        Me.rbreceive.UseVisualStyleBackColor = True
        Me.rbreceive.Visible = False
        '
        'rbprod
        '
        Me.rbprod.AutoSize = True
        Me.rbprod.Checked = True
        Me.rbprod.Location = New System.Drawing.Point(11, 40)
        Me.rbprod.Name = "rbprod"
        Me.rbprod.Size = New System.Drawing.Size(76, 17)
        Me.rbprod.TabIndex = 103
        Me.rbprod.TabStop = True
        Me.rbprod.Text = "Production"
        Me.rbprod.UseVisualStyleBackColor = True
        Me.rbprod.Visible = False
        '
        'pgb1
        '
        Me.pgb1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgb1.Location = New System.Drawing.Point(4, 607)
        Me.pgb1.MarqueeAnimationSpeed = 50
        Me.pgb1.Maximum = 10
        Me.pgb1.Name = "pgb1"
        Me.pgb1.Size = New System.Drawing.Size(1276, 23)
        Me.pgb1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pgb1.TabIndex = 102
        Me.pgb1.Visible = False
        '
        'lblloading
        '
        Me.lblloading.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblloading.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblloading.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblloading.Location = New System.Drawing.Point(4, 103)
        Me.lblloading.Name = "lblloading"
        Me.lblloading.Size = New System.Drawing.Size(1276, 509)
        Me.lblloading.TabIndex = 101
        Me.lblloading.Text = "Loading..."
        Me.lblloading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtlogsheetid
        '
        Me.txtlogsheetid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtlogsheetid.Location = New System.Drawing.Point(592, 43)
        Me.txtlogsheetid.Name = "txtlogsheetid"
        Me.txtlogsheetid.Size = New System.Drawing.Size(119, 20)
        Me.txtlogsheetid.TabIndex = 79
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(501, 46)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 15)
        Me.Label8.TabIndex = 94
        Me.Label8.Text = "Log Sheet ID#:"
        '
        'cmbitem
        '
        Me.cmbitem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbitem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbitem.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmbitem.FormattingEnabled = True
        Me.cmbitem.Location = New System.Drawing.Point(815, 12)
        Me.cmbitem.Name = "cmbitem"
        Me.cmbitem.Size = New System.Drawing.Size(233, 23)
        Me.cmbitem.TabIndex = 81
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(732, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 15)
        Me.Label7.TabIndex = 92
        Me.Label7.Text = "Item Name:"
        '
        'lblcount
        '
        Me.lblcount.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblcount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcount.Location = New System.Drawing.Point(0, 636)
        Me.lblcount.Name = "lblcount"
        Me.lblcount.Size = New System.Drawing.Size(1284, 25)
        Me.lblcount.TabIndex = 91
        Me.lblcount.Text = "Count:"
        '
        'chkhide
        '
        Me.chkhide.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkhide.AutoSize = True
        Me.chkhide.Location = New System.Drawing.Point(1177, 60)
        Me.chkhide.Name = "chkhide"
        Me.chkhide.Size = New System.Drawing.Size(101, 17)
        Me.chkhide.TabIndex = 84
        Me.chkhide.Text = "Hide Cancelled "
        Me.chkhide.UseVisualStyleBackColor = True
        '
        'btnexport
        '
        Me.btnexport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexport.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.btnexport.Image = CType(resources.GetObject("btnexport.Image"), System.Drawing.Image)
        Me.btnexport.Location = New System.Drawing.Point(1168, 7)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(104, 23)
        Me.btnexport.TabIndex = 89
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
        Me.btnsearch.Location = New System.Drawing.Point(1056, 7)
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
        Me.btnview.Location = New System.Drawing.Point(1056, 34)
        Me.btnview.Name = "btnview"
        Me.btnview.Size = New System.Drawing.Size(216, 23)
        Me.btnview.TabIndex = 83
        Me.btnview.Text = "View All Pending"
        Me.btnview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnview.UseVisualStyleBackColor = True
        '
        'lbltrip
        '
        Me.lbltrip.AutoSize = True
        Me.lbltrip.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lbltrip.Location = New System.Drawing.Point(812, 46)
        Me.lbltrip.Name = "lbltrip"
        Me.lbltrip.Size = New System.Drawing.Size(17, 15)
        Me.lbltrip.TabIndex = 85
        Me.lbltrip.Text = "L."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(732, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 15)
        Me.Label6.TabIndex = 84
        Me.Label6.Text = "Log Sheet #:"
        '
        'txtlog
        '
        Me.txtlog.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtlog.Location = New System.Drawing.Point(835, 44)
        Me.txtlog.Name = "txtlog"
        Me.txtlog.Size = New System.Drawing.Size(213, 20)
        Me.txtlog.TabIndex = 80
        '
        'cmbshift
        '
        Me.cmbshift.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbshift.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbshift.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmbshift.FormattingEnabled = True
        Me.cmbshift.Location = New System.Drawing.Point(592, 13)
        Me.cmbshift.Name = "cmbshift"
        Me.cmbshift.Size = New System.Drawing.Size(119, 23)
        Me.cmbshift.TabIndex = 78
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(501, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 15)
        Me.Label4.TabIndex = 81
        Me.Label4.Text = "Shift:"
        '
        'cmbline
        '
        Me.cmbline.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbline.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbline.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmbline.FormattingEnabled = True
        Me.cmbline.Location = New System.Drawing.Point(351, 40)
        Me.cmbline.Name = "cmbline"
        Me.cmbline.Size = New System.Drawing.Size(130, 23)
        Me.cmbline.TabIndex = 77
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(271, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 15)
        Me.Label3.TabIndex = 79
        Me.Label3.Text = "Palletizer:"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintTicketToolStripMenuItem, Me.AdminConfirmLogsheetToolStripMenuItem, Me.CancelLogsheetToolStripMenuItem, Me.ActivateLogsheetToolStripMenuItem, Me.ActivateAdminConfirmationToolStripMenuItem, Me.UpdateFlourBinToolStripMenuItem, Me.VerifyToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(231, 158)
        '
        'PrintTicketToolStripMenuItem
        '
        Me.PrintTicketToolStripMenuItem.Image = CType(resources.GetObject("PrintTicketToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PrintTicketToolStripMenuItem.Name = "PrintTicketToolStripMenuItem"
        Me.PrintTicketToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.PrintTicketToolStripMenuItem.Text = "Print Log Sheet"
        '
        'AdminConfirmLogsheetToolStripMenuItem
        '
        Me.AdminConfirmLogsheetToolStripMenuItem.Image = CType(resources.GetObject("AdminConfirmLogsheetToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AdminConfirmLogsheetToolStripMenuItem.Name = "AdminConfirmLogsheetToolStripMenuItem"
        Me.AdminConfirmLogsheetToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.AdminConfirmLogsheetToolStripMenuItem.Text = "Admin Confirmation"
        '
        'CancelLogsheetToolStripMenuItem
        '
        Me.CancelLogsheetToolStripMenuItem.Image = CType(resources.GetObject("CancelLogsheetToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CancelLogsheetToolStripMenuItem.Name = "CancelLogsheetToolStripMenuItem"
        Me.CancelLogsheetToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.CancelLogsheetToolStripMenuItem.Text = "Cancel Logsheet"
        '
        'ActivateLogsheetToolStripMenuItem
        '
        Me.ActivateLogsheetToolStripMenuItem.Image = CType(resources.GetObject("ActivateLogsheetToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ActivateLogsheetToolStripMenuItem.Name = "ActivateLogsheetToolStripMenuItem"
        Me.ActivateLogsheetToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.ActivateLogsheetToolStripMenuItem.Text = "Activate Cut Off Logsheet"
        '
        'ActivateAdminConfirmationToolStripMenuItem
        '
        Me.ActivateAdminConfirmationToolStripMenuItem.Image = CType(resources.GetObject("ActivateAdminConfirmationToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ActivateAdminConfirmationToolStripMenuItem.Name = "ActivateAdminConfirmationToolStripMenuItem"
        Me.ActivateAdminConfirmationToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.ActivateAdminConfirmationToolStripMenuItem.Text = "Activate Admin Confirmation"
        '
        'UpdateFlourBinToolStripMenuItem
        '
        Me.UpdateFlourBinToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewToolStripMenuItem, Me.ChangeToolStripMenuItem})
        Me.UpdateFlourBinToolStripMenuItem.Image = CType(resources.GetObject("UpdateFlourBinToolStripMenuItem.Image"), System.Drawing.Image)
        Me.UpdateFlourBinToolStripMenuItem.Name = "UpdateFlourBinToolStripMenuItem"
        Me.UpdateFlourBinToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.UpdateFlourBinToolStripMenuItem.Text = "Flour Bin"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.ViewToolStripMenuItem.Text = "View All"
        '
        'ChangeToolStripMenuItem
        '
        Me.ChangeToolStripMenuItem.Name = "ChangeToolStripMenuItem"
        Me.ChangeToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.ChangeToolStripMenuItem.Text = "Change"
        '
        'VerifyToolStripMenuItem
        '
        Me.VerifyToolStripMenuItem.Image = CType(resources.GetObject("VerifyToolStripMenuItem.Image"), System.Drawing.Image)
        Me.VerifyToolStripMenuItem.Name = "VerifyToolStripMenuItem"
        Me.VerifyToolStripMenuItem.Size = New System.Drawing.Size(230, 22)
        Me.VerifyToolStripMenuItem.Text = "Verify"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "check white.ico")
        Me.ImageList1.Images.SetKeyName(1, "check green.ico")
        Me.ImageList1.Images.SetKeyName(2, "check orange.ico")
        Me.ImageList1.Images.SetKeyName(3, "sack.ico")
        Me.ImageList1.Images.SetKeyName(4, "location.ico")
        Me.ImageList1.Images.SetKeyName(5, "columnbar.ico")
        '
        'Column1
        '
        Me.Column1.Frozen = True
        Me.Column1.HeaderText = "ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column11
        '
        DataGridViewCellStyle3.Format = "yyyy/MM/dd"
        Me.Column11.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column11.Frozen = True
        Me.Column11.HeaderText = "Production Date"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        Me.Column11.Width = 120
        '
        'Column2
        '
        Me.Column2.Frozen = True
        Me.Column2.HeaderText = "Ticket Log Sheet"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column2.Width = 170
        '
        'Column3
        '
        Me.Column3.HeaderText = "Warehouse"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Palletizer"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Shift"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column21
        '
        Me.Column21.HeaderText = "Item Name"
        Me.Column21.Name = "Column21"
        Me.Column21.ReadOnly = True
        Me.Column21.Width = 250
        '
        'Column13
        '
        Me.Column13.HeaderText = "Thread"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        '
        'Column20
        '
        Me.Column20.HeaderText = "Sack Type"
        Me.Column20.Name = "Column20"
        Me.Column20.ReadOnly = True
        '
        'Column22
        '
        Me.Column22.HeaderText = "Ticket Type"
        Me.Column22.Name = "Column22"
        Me.Column22.ReadOnly = True
        '
        'Column23
        '
        Me.Column23.HeaderText = "Flour Bin"
        Me.Column23.Name = "Column23"
        Me.Column23.ReadOnly = True
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
        'Column14
        '
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column14.DefaultCellStyle = DataGridViewCellStyle5
        Me.Column14.HeaderText = "Prod. Remarks"
        Me.Column14.Name = "Column14"
        Me.Column14.ReadOnly = True
        Me.Column14.Width = 200
        '
        'Column16
        '
        Me.Column16.HeaderText = "Checked by"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        Me.Column16.Width = 140
        '
        'Column15
        '
        Me.Column15.HeaderText = "Date Verified"
        Me.Column15.Name = "Column15"
        Me.Column15.ReadOnly = True
        '
        'Column10
        '
        Me.Column10.HeaderText = "Verified by"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        '
        'qca
        '
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.qca.DefaultCellStyle = DataGridViewCellStyle6
        Me.qca.HeaderText = "QCA Remarks"
        Me.qca.Name = "qca"
        Me.qca.ReadOnly = True
        Me.qca.Width = 200
        '
        'stat
        '
        Me.stat.HeaderText = "Status"
        Me.stat.Name = "stat"
        Me.stat.ReadOnly = True
        Me.stat.Width = 150
        '
        'Column12
        '
        Me.Column12.HeaderText = "Admin Remarks"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        Me.Column12.Width = 200
        '
        'Column17
        '
        DataGridViewCellStyle7.Format = "yyyy/MM/dd HH:mm"
        Me.Column17.DefaultCellStyle = DataGridViewCellStyle7
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
        'ticketsum
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1284, 661)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ticketsum"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ticket Summary"
        CType(Me.grdlog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdlog As System.Windows.Forms.DataGridView
    Friend WithEvents dateto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbwhse As System.Windows.Forms.ComboBox
    Friend WithEvents datefrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lbltrip As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtlog As System.Windows.Forms.TextBox
    Friend WithEvents cmbshift As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbline As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkhide As System.Windows.Forms.CheckBox
    Friend WithEvents btnexport As System.Windows.Forms.Button
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents btnview As System.Windows.Forms.Button
    Friend WithEvents lblcount As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PrintTicketToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents AdminConfirmLogsheetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CancelLogsheetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ActivateLogsheetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ActivateAdminConfirmationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbitem As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtlogsheetid As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents pgb1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblloading As System.Windows.Forms.Label
    Friend WithEvents UpdateFlourBinToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rbreceive As System.Windows.Forms.RadioButton
    Friend WithEvents rbprod As System.Windows.Forms.RadioButton
    Friend WithEvents VerifyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column11 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewLinkColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column21 As DataGridViewTextBoxColumn
    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column20 As DataGridViewTextBoxColumn
    Friend WithEvents Column22 As DataGridViewTextBoxColumn
    Friend WithEvents Column23 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column14 As DataGridViewTextBoxColumn
    Friend WithEvents Column16 As DataGridViewTextBoxColumn
    Friend WithEvents Column15 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents qca As DataGridViewTextBoxColumn
    Friend WithEvents stat As DataGridViewTextBoxColumn
    Friend WithEvents Column12 As DataGridViewTextBoxColumn
    Friend WithEvents Column17 As DataGridViewTextBoxColumn
    Friend WithEvents Column18 As DataGridViewTextBoxColumn
    Friend WithEvents Column19 As DataGridViewTextBoxColumn
End Class
