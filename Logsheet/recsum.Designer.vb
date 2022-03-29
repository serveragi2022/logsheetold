<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class recsum
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(recsum))
        Me.Label1 = New System.Windows.Forms.Label
        Me.dateto = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.datefrom = New System.Windows.Forms.DateTimePicker
        Me.chkhide = New System.Windows.Forms.CheckBox
        Me.grdrec = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewLinkColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column17 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column18 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column19 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lblcount = New System.Windows.Forms.Label
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CancelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.txtrecid = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtrecnum = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lbltrip = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtwrs = New System.Windows.Forms.TextBox
        Me.lblof = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtofnum = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnexport = New System.Windows.Forms.Button
        Me.btnsearch = New System.Windows.Forms.Button
        Me.btnview = New System.Windows.Forms.Button
        CType(Me.grdrec, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 15)
        Me.Label1.TabIndex = 81
        Me.Label1.Text = "Receive Date From:"
        '
        'dateto
        '
        Me.dateto.CustomFormat = "yyyy/MM/dd"
        Me.dateto.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dateto.Location = New System.Drawing.Point(133, 35)
        Me.dateto.Name = "dateto"
        Me.dateto.Size = New System.Drawing.Size(108, 21)
        Me.dateto.TabIndex = 80
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(104, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 15)
        Me.Label2.TabIndex = 82
        Me.Label2.Text = "To:"
        '
        'datefrom
        '
        Me.datefrom.CustomFormat = "yyyy/MM/dd"
        Me.datefrom.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.datefrom.Location = New System.Drawing.Point(133, 8)
        Me.datefrom.Name = "datefrom"
        Me.datefrom.Size = New System.Drawing.Size(108, 21)
        Me.datefrom.TabIndex = 79
        '
        'chkhide
        '
        Me.chkhide.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkhide.AutoSize = True
        Me.chkhide.Location = New System.Drawing.Point(1178, 62)
        Me.chkhide.Name = "chkhide"
        Me.chkhide.Size = New System.Drawing.Size(101, 17)
        Me.chkhide.TabIndex = 92
        Me.chkhide.Text = "Hide Cancelled "
        Me.chkhide.UseVisualStyleBackColor = True
        '
        'grdrec
        '
        Me.grdrec.AllowUserToAddRows = False
        Me.grdrec.AllowUserToDeleteRows = False
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grdrec.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle8
        Me.grdrec.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdrec.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdrec.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.grdrec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdrec.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column11, Me.Column2, Me.Column3, Me.Column16, Me.Column6, Me.Column7, Me.Column8, Me.Column9, Me.Column10, Me.Column17, Me.Column18, Me.Column19})
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdrec.DefaultCellStyle = DataGridViewCellStyle14
        Me.grdrec.EnableHeadersVisualStyles = False
        Me.grdrec.Location = New System.Drawing.Point(15, 85)
        Me.grdrec.Name = "grdrec"
        Me.grdrec.ReadOnly = True
        Me.grdrec.RowHeadersWidth = 10
        Me.grdrec.Size = New System.Drawing.Size(1258, 146)
        Me.grdrec.TabIndex = 94
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
        DataGridViewCellStyle10.Format = "yyyy/MM/dd"
        Me.Column11.DefaultCellStyle = DataGridViewCellStyle10
        Me.Column11.Frozen = True
        Me.Column11.HeaderText = "Receive Date"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        Me.Column11.Width = 120
        '
        'Column2
        '
        Me.Column2.Frozen = True
        Me.Column2.HeaderText = "Receive #"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column2.Width = 170
        '
        'Column3
        '
        Me.Column3.HeaderText = "From Branch"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column16
        '
        Me.Column16.HeaderText = "Remarks"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        Me.Column16.Width = 140
        '
        'Column6
        '
        DataGridViewCellStyle11.Format = "yyyy/MM/dd HH:mm"
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle11
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
        DataGridViewCellStyle12.Format = "yyyy/MM/dd HH:mm"
        Me.Column8.DefaultCellStyle = DataGridViewCellStyle12
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
        'Column10
        '
        Me.Column10.HeaderText = "Status"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Width = 150
        '
        'Column17
        '
        DataGridViewCellStyle13.Format = "yyyy/MM/dd HH:mm"
        Me.Column17.DefaultCellStyle = DataGridViewCellStyle13
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
        'lblcount
        '
        Me.lblcount.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblcount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcount.Location = New System.Drawing.Point(0, 646)
        Me.lblcount.Name = "lblcount"
        Me.lblcount.Size = New System.Drawing.Size(1294, 25)
        Me.lblcount.TabIndex = 95
        Me.lblcount.Text = "Count:"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CancelToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(111, 26)
        '
        'CancelToolStripMenuItem
        '
        Me.CancelToolStripMenuItem.Image = CType(resources.GetObject("CancelToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CancelToolStripMenuItem.Name = "CancelToolStripMenuItem"
        Me.CancelToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.CancelToolStripMenuItem.Text = "Cancel"
        '
        'txtrecid
        '
        Me.txtrecid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtrecid.Location = New System.Drawing.Point(361, 10)
        Me.txtrecid.Name = "txtrecid"
        Me.txtrecid.Size = New System.Drawing.Size(210, 20)
        Me.txtrecid.TabIndex = 96
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(270, 13)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 15)
        Me.Label8.TabIndex = 97
        Me.Label8.Text = "Receive ID#:"
        '
        'txtrecnum
        '
        Me.txtrecnum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtrecnum.Location = New System.Drawing.Point(361, 36)
        Me.txtrecnum.Name = "txtrecnum"
        Me.txtrecnum.Size = New System.Drawing.Size(210, 20)
        Me.txtrecnum.TabIndex = 98
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(270, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 15)
        Me.Label3.TabIndex = 99
        Me.Label3.Text = "Receive #:"
        '
        'lbltrip
        '
        Me.lbltrip.AutoSize = True
        Me.lbltrip.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lbltrip.Location = New System.Drawing.Point(340, 38)
        Me.lbltrip.Name = "lbltrip"
        Me.lbltrip.Size = New System.Drawing.Size(16, 15)
        Me.lbltrip.TabIndex = 100
        Me.lbltrip.Text = "T."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(605, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 15)
        Me.Label4.TabIndex = 105
        Me.Label4.Text = "WRS #:"
        '
        'txtwrs
        '
        Me.txtwrs.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtwrs.Location = New System.Drawing.Point(690, 10)
        Me.txtwrs.Name = "txtwrs"
        Me.txtwrs.Size = New System.Drawing.Size(210, 20)
        Me.txtwrs.TabIndex = 101
        '
        'lblof
        '
        Me.lblof.AutoSize = True
        Me.lblof.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lblof.Location = New System.Drawing.Point(681, 38)
        Me.lblof.Name = "lblof"
        Me.lblof.Size = New System.Drawing.Size(25, 15)
        Me.lblof.TabIndex = 104
        Me.lblof.Text = "OF."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(605, 38)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 15)
        Me.Label6.TabIndex = 103
        Me.Label6.Text = "Order Fill #:"
        '
        'txtofnum
        '
        Me.txtofnum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtofnum.Location = New System.Drawing.Point(712, 36)
        Me.txtofnum.Name = "txtofnum"
        Me.txtofnum.Size = New System.Drawing.Size(188, 20)
        Me.txtofnum.TabIndex = 102
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Location = New System.Drawing.Point(15, 270)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1267, 373)
        Me.Panel1.TabIndex = 106
        '
        'btnexport
        '
        Me.btnexport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexport.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.btnexport.Image = CType(resources.GetObject("btnexport.Image"), System.Drawing.Image)
        Me.btnexport.Location = New System.Drawing.Point(1169, 9)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(104, 23)
        Me.btnexport.TabIndex = 93
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
        Me.btnsearch.Location = New System.Drawing.Point(1057, 9)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(106, 23)
        Me.btnsearch.TabIndex = 90
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
        Me.btnview.Location = New System.Drawing.Point(1057, 36)
        Me.btnview.Name = "btnview"
        Me.btnview.Size = New System.Drawing.Size(216, 23)
        Me.btnview.TabIndex = 91
        Me.btnview.Text = "View All Pending"
        Me.btnview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnview.UseVisualStyleBackColor = True
        '
        'recsum
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1294, 671)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtwrs)
        Me.Controls.Add(Me.lblof)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtofnum)
        Me.Controls.Add(Me.lbltrip)
        Me.Controls.Add(Me.txtrecnum)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtrecid)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblcount)
        Me.Controls.Add(Me.grdrec)
        Me.Controls.Add(Me.chkhide)
        Me.Controls.Add(Me.btnexport)
        Me.Controls.Add(Me.btnsearch)
        Me.Controls.Add(Me.btnview)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dateto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.datefrom)
        Me.Name = "recsum"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Receive Summary"
        CType(Me.grdrec, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dateto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents datefrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkhide As System.Windows.Forms.CheckBox
    Friend WithEvents btnexport As System.Windows.Forms.Button
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents btnview As System.Windows.Forms.Button
    Friend WithEvents grdrec As System.Windows.Forms.DataGridView
    Friend WithEvents lblcount As System.Windows.Forms.Label
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CancelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtrecid As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtrecnum As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbltrip As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtwrs As System.Windows.Forms.TextBox
    Friend WithEvents lblof As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtofnum As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
