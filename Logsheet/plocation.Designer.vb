<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class plocation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(plocation))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbwhse = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.numrow = New System.Windows.Forms.NumericUpDown
        Me.Label3 = New System.Windows.Forms.Label
        Me.numcol = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.btndeactivate = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblcat = New System.Windows.Forms.Label
        Me.btncancel = New System.Windows.Forms.Button
        Me.lblid = New System.Windows.Forms.Label
        Me.btnview = New System.Windows.Forms.Button
        Me.btnsearch = New System.Windows.Forms.Button
        Me.grdloc = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnadd = New System.Windows.Forms.Button
        Me.txtloc = New System.Windows.Forms.TextBox
        Me.btnupdate = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.numrow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numcol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdloc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cmbwhse)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.numrow)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.numcol)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btndeactivate)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblcat)
        Me.GroupBox1.Controls.Add(Me.btncancel)
        Me.GroupBox1.Controls.Add(Me.lblid)
        Me.GroupBox1.Controls.Add(Me.btnview)
        Me.GroupBox1.Controls.Add(Me.btnsearch)
        Me.GroupBox1.Controls.Add(Me.grdloc)
        Me.GroupBox1.Controls.Add(Me.btnadd)
        Me.GroupBox1.Controls.Add(Me.txtloc)
        Me.GroupBox1.Controls.Add(Me.btnupdate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(887, 637)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Manage Pallet Location"
        '
        'cmbwhse
        '
        Me.cmbwhse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbwhse.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbwhse.FormattingEnabled = True
        Me.cmbwhse.Location = New System.Drawing.Point(208, 23)
        Me.cmbwhse.Name = "cmbwhse"
        Me.cmbwhse.Size = New System.Drawing.Size(257, 23)
        Me.cmbwhse.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(51, 31)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 15)
        Me.Label4.TabIndex = 47
        Me.Label4.Text = "Warehouse:"
        '
        'numrow
        '
        Me.numrow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numrow.Location = New System.Drawing.Point(208, 111)
        Me.numrow.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numrow.Name = "numrow"
        Me.numrow.Size = New System.Drawing.Size(157, 21)
        Me.numrow.TabIndex = 3
        Me.numrow.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(51, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(136, 15)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Maximum No. of Pallet:"
        '
        'numcol
        '
        Me.numcol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numcol.Location = New System.Drawing.Point(208, 82)
        Me.numcol.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numcol.Name = "numcol"
        Me.numcol.Size = New System.Drawing.Size(157, 21)
        Me.numcol.TabIndex = 2
        Me.numcol.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(51, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 15)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Maximum No. of Columns:"
        '
        'btndeactivate
        '
        Me.btndeactivate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndeactivate.Image = CType(resources.GetObject("btndeactivate.Image"), System.Drawing.Image)
        Me.btndeactivate.Location = New System.Drawing.Point(410, 152)
        Me.btndeactivate.Name = "btndeactivate"
        Me.btndeactivate.Size = New System.Drawing.Size(92, 27)
        Me.btndeactivate.TabIndex = 8
        Me.btndeactivate.Text = "&Deactivate"
        Me.btndeactivate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btndeactivate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndeactivate.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label5.Location = New System.Drawing.Point(21, 621)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 42
        Me.Label5.Text = "-Jecel-"
        '
        'lblcat
        '
        Me.lblcat.AutoSize = True
        Me.lblcat.Location = New System.Drawing.Point(420, 13)
        Me.lblcat.Name = "lblcat"
        Me.lblcat.Size = New System.Drawing.Size(45, 15)
        Me.lblcat.TabIndex = 32
        Me.lblcat.Text = "Label3"
        Me.lblcat.Visible = False
        '
        'btncancel
        '
        Me.btncancel.Enabled = False
        Me.btncancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(316, 152)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(88, 27)
        Me.btncancel.TabIndex = 7
        Me.btncancel.Text = "&Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'lblid
        '
        Me.lblid.AutoSize = True
        Me.lblid.Location = New System.Drawing.Point(205, 13)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(45, 15)
        Me.lblid.TabIndex = 2
        Me.lblid.Text = "Label3"
        Me.lblid.Visible = False
        '
        'btnview
        '
        Me.btnview.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnview.Image = CType(resources.GetObject("btnview.Image"), System.Drawing.Image)
        Me.btnview.Location = New System.Drawing.Point(508, 152)
        Me.btnview.Name = "btnview"
        Me.btnview.Size = New System.Drawing.Size(88, 27)
        Me.btnview.TabIndex = 9
        Me.btnview.Text = "&View All"
        Me.btnview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnview.UseVisualStyleBackColor = True
        '
        'btnsearch
        '
        Me.btnsearch.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsearch.Image = CType(resources.GetObject("btnsearch.Image"), System.Drawing.Image)
        Me.btnsearch.Location = New System.Drawing.Point(32, 152)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(88, 27)
        Me.btnsearch.TabIndex = 4
        Me.btnsearch.Text = "&Search"
        Me.btnsearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnsearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'grdloc
        '
        Me.grdloc.AllowUserToAddRows = False
        Me.grdloc.AllowUserToDeleteRows = False
        Me.grdloc.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.grdloc.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdloc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdloc.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdloc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdloc.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column6, Me.Column2, Me.Column3, Me.Column5, Me.Column4})
        Me.grdloc.EnableHeadersVisualStyles = False
        Me.grdloc.Location = New System.Drawing.Point(21, 192)
        Me.grdloc.Name = "grdloc"
        Me.grdloc.ReadOnly = True
        Me.grdloc.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdloc.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdloc.RowHeadersWidth = 10
        Me.grdloc.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdloc.Size = New System.Drawing.Size(845, 426)
        Me.grdloc.TabIndex = 9
        '
        'Column1
        '
        Me.Column1.HeaderText = "ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column1.Visible = False
        '
        'Column6
        '
        Me.Column6.HeaderText = "Warehouse"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column6.Width = 160
        '
        'Column2
        '
        Me.Column2.HeaderText = "Pallet Location"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column2.Width = 160
        '
        'Column3
        '
        Me.Column3.HeaderText = "Max no. of columns"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column3.Width = 160
        '
        'Column5
        '
        Me.Column5.HeaderText = "Max no. of pallet"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column5.Width = 160
        '
        'Column4
        '
        Me.Column4.HeaderText = "Status"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column4.Width = 150
        '
        'btnadd
        '
        Me.btnadd.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.Image = CType(resources.GetObject("btnadd.Image"), System.Drawing.Image)
        Me.btnadd.Location = New System.Drawing.Point(126, 152)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(90, 27)
        Me.btnadd.TabIndex = 5
        Me.btnadd.Text = "&Add"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'txtloc
        '
        Me.txtloc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtloc.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloc.Location = New System.Drawing.Point(208, 54)
        Me.txtloc.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtloc.Name = "txtloc"
        Me.txtloc.Size = New System.Drawing.Size(257, 21)
        Me.txtloc.TabIndex = 1
        '
        'btnupdate
        '
        Me.btnupdate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.Image = CType(resources.GetObject("btnupdate.Image"), System.Drawing.Image)
        Me.btnupdate.Location = New System.Drawing.Point(222, 152)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(88, 27)
        Me.btnupdate.TabIndex = 6
        Me.btnupdate.Text = "&Update"
        Me.btnupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(51, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 15)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Pallet Location:"
        '
        'plocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(911, 661)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "plocation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pallet Location"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.numrow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numcol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdloc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents grdloc As System.Windows.Forms.DataGridView
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents txtloc As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents btnview As System.Windows.Forms.Button
    Friend WithEvents lblid As System.Windows.Forms.Label
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblcat As System.Windows.Forms.Label
    Friend WithEvents btndeactivate As System.Windows.Forms.Button
    Friend WithEvents numcol As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents numrow As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbwhse As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
