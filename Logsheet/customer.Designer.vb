<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class customer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(customer))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnexport = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtcode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnimport = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtrems = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtemail = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btndeactivate = New System.Windows.Forms.Button()
        Me.txtaddress = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblcus = New System.Windows.Forms.Label()
        Me.btncancel = New System.Windows.Forms.Button()
        Me.lblid = New System.Windows.Forms.Label()
        Me.btnview = New System.Windows.Forms.Button()
        Me.btnsearch = New System.Windows.Forms.Button()
        Me.btnadd = New System.Windows.Forms.Button()
        Me.txtname = New System.Windows.Forms.TextBox()
        Me.btnupdate = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grdcus = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkhide = New System.Windows.Forms.CheckBox()
        Me.lblcount = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdcus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnexport)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtcode)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnimport)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtrems)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtemail)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btndeactivate)
        Me.GroupBox1.Controls.Add(Me.txtaddress)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblcus)
        Me.GroupBox1.Controls.Add(Me.btncancel)
        Me.GroupBox1.Controls.Add(Me.lblid)
        Me.GroupBox1.Controls.Add(Me.btnview)
        Me.GroupBox1.Controls.Add(Me.btnsearch)
        Me.GroupBox1.Controls.Add(Me.btnadd)
        Me.GroupBox1.Controls.Add(Me.txtname)
        Me.GroupBox1.Controls.Add(Me.btnupdate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(351, 530)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Manage Customer Info"
        '
        'btnexport
        '
        Me.btnexport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexport.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexport.Image = CType(resources.GetObject("btnexport.Image"), System.Drawing.Image)
        Me.btnexport.Location = New System.Drawing.Point(81, 501)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(247, 23)
        Me.btnexport.TabIndex = 58
        Me.btnexport.Text = "Export Customer"
        Me.btnexport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnexport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexport.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Red
        Me.Label9.Location = New System.Drawing.Point(328, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(17, 22)
        Me.Label9.TabIndex = 56
        Me.Label9.Text = "*"
        '
        'txtcode
        '
        Me.txtcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcode.Location = New System.Drawing.Point(81, 37)
        Me.txtcode.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(247, 21)
        Me.txtcode.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 15)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Code:"
        '
        'btnimport
        '
        Me.btnimport.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnimport.Image = CType(resources.GetObject("btnimport.Image"), System.Drawing.Image)
        Me.btnimport.Location = New System.Drawing.Point(81, 468)
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Size = New System.Drawing.Size(247, 27)
        Me.btnimport.TabIndex = 53
        Me.btnimport.Text = "&Import Customer"
        Me.btnimport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnimport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnimport.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(328, 100)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(17, 22)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "*"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(328, 71)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(17, 22)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "*"
        '
        'txtrems
        '
        Me.txtrems.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrems.Location = New System.Drawing.Point(81, 275)
        Me.txtrems.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtrems.Multiline = True
        Me.txtrems.Name = "txtrems"
        Me.txtrems.Size = New System.Drawing.Size(247, 86)
        Me.txtrems.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 278)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 15)
        Me.Label6.TabIndex = 50
        Me.Label6.Text = "Remarks:"
        '
        'txtemail
        '
        Me.txtemail.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtemail.Location = New System.Drawing.Point(81, 244)
        Me.txtemail.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtemail.Name = "txtemail"
        Me.txtemail.Size = New System.Drawing.Size(247, 21)
        Me.txtemail.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 247)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 15)
        Me.Label4.TabIndex = 47
        Me.Label4.Text = "Vicinity:"
        '
        'btndeactivate
        '
        Me.btndeactivate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndeactivate.Image = CType(resources.GetObject("btndeactivate.Image"), System.Drawing.Image)
        Me.btndeactivate.Location = New System.Drawing.Point(81, 402)
        Me.btndeactivate.Name = "btndeactivate"
        Me.btndeactivate.Size = New System.Drawing.Size(119, 27)
        Me.btndeactivate.TabIndex = 7
        Me.btndeactivate.Text = "&Deactivate"
        Me.btndeactivate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btndeactivate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndeactivate.UseVisualStyleBackColor = True
        '
        'txtaddress
        '
        Me.txtaddress.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaddress.Location = New System.Drawing.Point(81, 97)
        Me.txtaddress.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtaddress.Multiline = True
        Me.txtaddress.Name = "txtaddress"
        Me.txtaddress.Size = New System.Drawing.Size(247, 137)
        Me.txtaddress.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 100)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 15)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Address:"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label5.Location = New System.Drawing.Point(19, 514)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 42
        Me.Label5.Text = "-Jecel-"
        '
        'lblcus
        '
        Me.lblcus.AutoSize = True
        Me.lblcus.Location = New System.Drawing.Point(285, 17)
        Me.lblcus.Name = "lblcus"
        Me.lblcus.Size = New System.Drawing.Size(45, 15)
        Me.lblcus.TabIndex = 32
        Me.lblcus.Text = "Label3"
        Me.lblcus.Visible = False
        '
        'btncancel
        '
        Me.btncancel.Enabled = False
        Me.btncancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(209, 402)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(119, 27)
        Me.btncancel.TabIndex = 8
        Me.btncancel.Text = "&Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'lblid
        '
        Me.lblid.AutoSize = True
        Me.lblid.Location = New System.Drawing.Point(80, 17)
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
        Me.btnview.Location = New System.Drawing.Point(209, 435)
        Me.btnview.Name = "btnview"
        Me.btnview.Size = New System.Drawing.Size(119, 27)
        Me.btnview.TabIndex = 10
        Me.btnview.Text = "&View All"
        Me.btnview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnview.UseVisualStyleBackColor = True
        '
        'btnsearch
        '
        Me.btnsearch.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsearch.Image = CType(resources.GetObject("btnsearch.Image"), System.Drawing.Image)
        Me.btnsearch.Location = New System.Drawing.Point(81, 435)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(119, 27)
        Me.btnsearch.TabIndex = 9
        Me.btnsearch.Text = "&Search"
        Me.btnsearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnsearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'btnadd
        '
        Me.btnadd.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.Image = CType(resources.GetObject("btnadd.Image"), System.Drawing.Image)
        Me.btnadd.Location = New System.Drawing.Point(81, 369)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(119, 27)
        Me.btnadd.TabIndex = 5
        Me.btnadd.Text = "&Add"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'txtname
        '
        Me.txtname.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtname.Location = New System.Drawing.Point(81, 68)
        Me.txtname.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(247, 21)
        Me.txtname.TabIndex = 1
        '
        'btnupdate
        '
        Me.btnupdate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.Image = CType(resources.GetObject("btnupdate.Image"), System.Drawing.Image)
        Me.btnupdate.Location = New System.Drawing.Point(209, 369)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(119, 27)
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
        Me.Label2.Location = New System.Drawing.Point(16, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 15)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Name:"
        '
        'grdcus
        '
        Me.grdcus.AllowUserToAddRows = False
        Me.grdcus.AllowUserToDeleteRows = False
        Me.grdcus.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.grdcus.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdcus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdcus.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdcus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdcus.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column5, Me.Column2, Me.Column3, Me.Column4, Me.Column6, Me.Column7})
        Me.grdcus.EnableHeadersVisualStyles = False
        Me.grdcus.Location = New System.Drawing.Point(369, 35)
        Me.grdcus.Name = "grdcus"
        Me.grdcus.ReadOnly = True
        Me.grdcus.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdcus.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdcus.RowHeadersWidth = 10
        Me.grdcus.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdcus.Size = New System.Drawing.Size(694, 471)
        Me.grdcus.TabIndex = 11
        '
        'Column1
        '
        Me.Column1.HeaderText = "ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        '
        'Column5
        '
        Me.Column5.HeaderText = "Code"
        Me.Column5.MinimumWidth = 150
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 150
        '
        'Column2
        '
        Me.Column2.HeaderText = "Name"
        Me.Column2.MinimumWidth = 200
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 200
        '
        'Column3
        '
        Me.Column3.HeaderText = "Address"
        Me.Column3.MinimumWidth = 200
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 200
        '
        'Column4
        '
        Me.Column4.HeaderText = "Vicinity"
        Me.Column4.MinimumWidth = 150
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 150
        '
        'Column6
        '
        Me.Column6.HeaderText = "Remarks"
        Me.Column6.MinimumWidth = 150
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 150
        '
        'Column7
        '
        Me.Column7.HeaderText = "Status"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'chkhide
        '
        Me.chkhide.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkhide.AutoSize = True
        Me.chkhide.Checked = True
        Me.chkhide.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkhide.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkhide.Location = New System.Drawing.Point(881, 11)
        Me.chkhide.Name = "chkhide"
        Me.chkhide.Size = New System.Drawing.Size(182, 18)
        Me.chkhide.TabIndex = 34
        Me.chkhide.Text = "Hide Deactivated Customers"
        Me.chkhide.UseVisualStyleBackColor = True
        '
        'lblcount
        '
        Me.lblcount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblcount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcount.Location = New System.Drawing.Point(366, 517)
        Me.lblcount.Name = "lblcount"
        Me.lblcount.Size = New System.Drawing.Size(697, 25)
        Me.lblcount.TabIndex = 35
        Me.lblcount.Text = "Count:"
        '
        'customer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1075, 554)
        Me.Controls.Add(Me.lblcount)
        Me.Controls.Add(Me.chkhide)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grdcus)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "customer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Customer"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdcus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents grdcus As System.Windows.Forms.DataGridView
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents txtname As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents btnview As System.Windows.Forms.Button
    Friend WithEvents lblid As System.Windows.Forms.Label
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblcus As System.Windows.Forms.Label
    Friend WithEvents txtaddress As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btndeactivate As System.Windows.Forms.Button
    Friend WithEvents txtrems As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtemail As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkhide As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnimport As System.Windows.Forms.Button
    Friend WithEvents txtcode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblcount As System.Windows.Forms.Label
    Friend WithEvents btnexport As System.Windows.Forms.Button
End Class
