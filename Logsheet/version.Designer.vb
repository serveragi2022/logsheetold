<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class version
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(version))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnopen1 = New System.Windows.Forms.Button
        Me.txtup = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnopen = New System.Windows.Forms.Button
        Me.txtfile = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btndeactivate = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblcat = New System.Windows.Forms.Label
        Me.btncancel = New System.Windows.Forms.Button
        Me.lblid = New System.Windows.Forms.Label
        Me.btnview = New System.Windows.Forms.Button
        Me.btnsearch = New System.Windows.Forms.Button
        Me.grdver = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnadd = New System.Windows.Forms.Button
        Me.txtver = New System.Windows.Forms.TextBox
        Me.btnupdate = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdver, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnopen1)
        Me.GroupBox1.Controls.Add(Me.txtup)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnopen)
        Me.GroupBox1.Controls.Add(Me.txtfile)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btndeactivate)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblcat)
        Me.GroupBox1.Controls.Add(Me.btncancel)
        Me.GroupBox1.Controls.Add(Me.lblid)
        Me.GroupBox1.Controls.Add(Me.btnview)
        Me.GroupBox1.Controls.Add(Me.btnsearch)
        Me.GroupBox1.Controls.Add(Me.grdver)
        Me.GroupBox1.Controls.Add(Me.btnadd)
        Me.GroupBox1.Controls.Add(Me.txtver)
        Me.GroupBox1.Controls.Add(Me.btnupdate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(637, 547)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Manage Version"
        '
        'btnopen1
        '
        Me.btnopen1.Image = CType(resources.GetObject("btnopen1.Image"), System.Drawing.Image)
        Me.btnopen1.Location = New System.Drawing.Point(481, 101)
        Me.btnopen1.Name = "btnopen1"
        Me.btnopen1.Size = New System.Drawing.Size(32, 23)
        Me.btnopen1.TabIndex = 48
        Me.btnopen1.UseVisualStyleBackColor = True
        '
        'txtup
        '
        Me.txtup.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtup.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtup.Location = New System.Drawing.Point(169, 102)
        Me.txtup.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtup.Name = "txtup"
        Me.txtup.ReadOnly = True
        Me.txtup.Size = New System.Drawing.Size(306, 21)
        Me.txtup.TabIndex = 47
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(108, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 15)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Updater:"
        '
        'btnopen
        '
        Me.btnopen.Image = CType(resources.GetObject("btnopen.Image"), System.Drawing.Image)
        Me.btnopen.Location = New System.Drawing.Point(481, 70)
        Me.btnopen.Name = "btnopen"
        Me.btnopen.Size = New System.Drawing.Size(32, 23)
        Me.btnopen.TabIndex = 45
        Me.btnopen.UseVisualStyleBackColor = True
        '
        'txtfile
        '
        Me.txtfile.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtfile.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfile.Location = New System.Drawing.Point(169, 71)
        Me.txtfile.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtfile.Name = "txtfile"
        Me.txtfile.ReadOnly = True
        Me.txtfile.Size = New System.Drawing.Size(306, 21)
        Me.txtfile.TabIndex = 44
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(90, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 15)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "Application:"
        '
        'btndeactivate
        '
        Me.btndeactivate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndeactivate.Image = CType(resources.GetObject("btndeactivate.Image"), System.Drawing.Image)
        Me.btndeactivate.Location = New System.Drawing.Point(411, 133)
        Me.btndeactivate.Name = "btndeactivate"
        Me.btndeactivate.Size = New System.Drawing.Size(92, 27)
        Me.btndeactivate.TabIndex = 6
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
        Me.Label5.Location = New System.Drawing.Point(21, 531)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 42
        Me.Label5.Text = "-Jecel-"
        '
        'lblcat
        '
        Me.lblcat.AutoSize = True
        Me.lblcat.Location = New System.Drawing.Point(362, 20)
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
        Me.btncancel.Location = New System.Drawing.Point(317, 133)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(88, 27)
        Me.btncancel.TabIndex = 5
        Me.btncancel.Text = "&Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'lblid
        '
        Me.lblid.AutoSize = True
        Me.lblid.Location = New System.Drawing.Point(166, 20)
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
        Me.btnview.Location = New System.Drawing.Point(509, 133)
        Me.btnview.Name = "btnview"
        Me.btnview.Size = New System.Drawing.Size(88, 27)
        Me.btnview.TabIndex = 7
        Me.btnview.Text = "&View All"
        Me.btnview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnview.UseVisualStyleBackColor = True
        '
        'btnsearch
        '
        Me.btnsearch.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsearch.Image = CType(resources.GetObject("btnsearch.Image"), System.Drawing.Image)
        Me.btnsearch.Location = New System.Drawing.Point(33, 133)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(88, 27)
        Me.btnsearch.TabIndex = 2
        Me.btnsearch.Text = "&Search"
        Me.btnsearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnsearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'grdver
        '
        Me.grdver.AllowUserToAddRows = False
        Me.grdver.AllowUserToDeleteRows = False
        Me.grdver.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.grdver.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdver.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdver.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdver.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdver.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3})
        Me.grdver.EnableHeadersVisualStyles = False
        Me.grdver.Location = New System.Drawing.Point(21, 178)
        Me.grdver.Name = "grdver"
        Me.grdver.ReadOnly = True
        Me.grdver.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdver.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdver.RowHeadersWidth = 10
        Me.grdver.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdver.Size = New System.Drawing.Size(588, 342)
        Me.grdver.TabIndex = 7
        '
        'Column1
        '
        Me.Column1.HeaderText = "ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        '
        'Column2
        '
        Me.Column2.HeaderText = "Version"
        Me.Column2.MinimumWidth = 240
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 240
        '
        'Column3
        '
        Me.Column3.HeaderText = "Status"
        Me.Column3.MinimumWidth = 120
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 120
        '
        'btnadd
        '
        Me.btnadd.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.Image = CType(resources.GetObject("btnadd.Image"), System.Drawing.Image)
        Me.btnadd.Location = New System.Drawing.Point(127, 133)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(90, 27)
        Me.btnadd.TabIndex = 3
        Me.btnadd.Text = "&Add"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'txtver
        '
        Me.txtver.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtver.Location = New System.Drawing.Point(169, 40)
        Me.txtver.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtver.Name = "txtver"
        Me.txtver.Size = New System.Drawing.Size(344, 21)
        Me.txtver.TabIndex = 0
        '
        'btnupdate
        '
        Me.btnupdate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.Image = CType(resources.GetObject("btnupdate.Image"), System.Drawing.Image)
        Me.btnupdate.Location = New System.Drawing.Point(223, 133)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(88, 27)
        Me.btnupdate.TabIndex = 4
        Me.btnupdate.Text = "&Update"
        Me.btnupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(110, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 15)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Version:"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'version
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(661, 571)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "version"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Version"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdver, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents grdver As System.Windows.Forms.DataGridView
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents txtver As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents btnview As System.Windows.Forms.Button
    Friend WithEvents lblid As System.Windows.Forms.Label
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblcat As System.Windows.Forms.Label
    Friend WithEvents btndeactivate As System.Windows.Forms.Button
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnopen As System.Windows.Forms.Button
    Friend WithEvents txtfile As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnopen1 As System.Windows.Forms.Button
    Friend WithEvents txtup As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
