<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newitems
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(newitems))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmbtype = New System.Windows.Forms.ComboBox()
        Me.lblname = New System.Windows.Forms.Label()
        Me.txtid = New System.Windows.Forms.TextBox()
        Me.btndiscon = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnview = New System.Windows.Forms.Button()
        Me.btnexport = New System.Windows.Forms.Button()
        Me.txtname = New System.Windows.Forms.TextBox()
        Me.btnimport = New System.Windows.Forms.Button()
        Me.txtstatus = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnprint = New System.Windows.Forms.Button()
        Me.txtcode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtprice = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtdes = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbcat = New System.Windows.Forms.ComboBox()
        Me.btnsearch = New System.Windows.Forms.Button()
        Me.btnupdate = New System.Windows.Forms.Button()
        Me.btncancel = New System.Windows.Forms.Button()
        Me.btnadditem = New System.Windows.Forms.Button()
        Me.chkhide = New System.Windows.Forms.CheckBox()
        Me.grditems = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grditems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.cmbtype)
        Me.GroupBox1.Controls.Add(Me.lblname)
        Me.GroupBox1.Controls.Add(Me.txtid)
        Me.GroupBox1.Controls.Add(Me.btndiscon)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.btnview)
        Me.GroupBox1.Controls.Add(Me.btnexport)
        Me.GroupBox1.Controls.Add(Me.txtname)
        Me.GroupBox1.Controls.Add(Me.btnimport)
        Me.GroupBox1.Controls.Add(Me.txtstatus)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnprint)
        Me.GroupBox1.Controls.Add(Me.txtcode)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtprice)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtdes)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbcat)
        Me.GroupBox1.Controls.Add(Me.btnsearch)
        Me.GroupBox1.Controls.Add(Me.btnupdate)
        Me.GroupBox1.Controls.Add(Me.btncancel)
        Me.GroupBox1.Controls.Add(Me.btnadditem)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(319, 595)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Manage Items"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(14, 268)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(74, 15)
        Me.Label15.TabIndex = 83
        Me.Label15.Text = "Ticket Type:"
        '
        'cmbtype
        '
        Me.cmbtype.BackColor = System.Drawing.SystemColors.Window
        Me.cmbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtype.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbtype.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtype.FormattingEnabled = True
        Me.cmbtype.Items.AddRange(New Object() {"", "Hard", "Soft", "Specialty"})
        Me.cmbtype.Location = New System.Drawing.Point(94, 265)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.Size = New System.Drawing.Size(206, 23)
        Me.cmbtype.TabIndex = 82
        '
        'lblname
        '
        Me.lblname.AutoSize = True
        Me.lblname.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblname.Location = New System.Drawing.Point(216, 10)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(0, 15)
        Me.lblname.TabIndex = 81
        Me.lblname.Visible = False
        '
        'txtid
        '
        Me.txtid.BackColor = System.Drawing.Color.White
        Me.txtid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtid.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtid.Location = New System.Drawing.Point(94, 28)
        Me.txtid.Name = "txtid"
        Me.txtid.ReadOnly = True
        Me.txtid.Size = New System.Drawing.Size(206, 21)
        Me.txtid.TabIndex = 0
        '
        'btndiscon
        '
        Me.btndiscon.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndiscon.Image = CType(resources.GetObject("btndiscon.Image"), System.Drawing.Image)
        Me.btndiscon.Location = New System.Drawing.Point(94, 450)
        Me.btndiscon.Name = "btndiscon"
        Me.btndiscon.Size = New System.Drawing.Size(99, 23)
        Me.btndiscon.TabIndex = 8
        Me.btndiscon.Text = "Discontinue"
        Me.btndiscon.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btndiscon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndiscon.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(39, 31)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 15)
        Me.Label7.TabIndex = 79
        Me.Label7.Text = "Item ID:"
        '
        'btnview
        '
        Me.btnview.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnview.Image = CType(resources.GetObject("btnview.Image"), System.Drawing.Image)
        Me.btnview.Location = New System.Drawing.Point(201, 479)
        Me.btnview.Name = "btnview"
        Me.btnview.Size = New System.Drawing.Size(99, 23)
        Me.btnview.TabIndex = 11
        Me.btnview.Text = "&View All"
        Me.btnview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnview.UseVisualStyleBackColor = True
        '
        'btnexport
        '
        Me.btnexport.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnexport.Image = CType(resources.GetObject("btnexport.Image"), System.Drawing.Image)
        Me.btnexport.Location = New System.Drawing.Point(201, 508)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(99, 23)
        Me.btnexport.TabIndex = 13
        Me.btnexport.Text = "Export"
        Me.btnexport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnexport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexport.UseVisualStyleBackColor = True
        '
        'txtname
        '
        Me.txtname.BackColor = System.Drawing.Color.White
        Me.txtname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtname.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtname.Location = New System.Drawing.Point(94, 149)
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(206, 21)
        Me.txtname.TabIndex = 3
        '
        'btnimport
        '
        Me.btnimport.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnimport.Image = CType(resources.GetObject("btnimport.Image"), System.Drawing.Image)
        Me.btnimport.Location = New System.Drawing.Point(94, 508)
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Size = New System.Drawing.Size(99, 23)
        Me.btnimport.TabIndex = 12
        Me.btnimport.Text = "&Import"
        Me.btnimport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnimport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnimport.UseVisualStyleBackColor = True
        '
        'txtstatus
        '
        Me.txtstatus.BackColor = System.Drawing.Color.White
        Me.txtstatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstatus.Location = New System.Drawing.Point(94, 352)
        Me.txtstatus.Name = "txtstatus"
        Me.txtstatus.ReadOnly = True
        Me.txtstatus.Size = New System.Drawing.Size(206, 21)
        Me.txtstatus.TabIndex = 71
        Me.txtstatus.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(41, 355)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 15)
        Me.Label6.TabIndex = 77
        Me.Label6.Text = "Status:"
        Me.Label6.Visible = False
        '
        'btnprint
        '
        Me.btnprint.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Image = CType(resources.GetObject("btnprint.Image"), System.Drawing.Image)
        Me.btnprint.Location = New System.Drawing.Point(94, 537)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(206, 23)
        Me.btnprint.TabIndex = 14
        Me.btnprint.Text = "&Print Items"
        Me.btnprint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnprint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'txtcode
        '
        Me.txtcode.BackColor = System.Drawing.Color.White
        Me.txtcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcode.Location = New System.Drawing.Point(94, 110)
        Me.txtcode.Name = "txtcode"
        Me.txtcode.Size = New System.Drawing.Size(206, 21)
        Me.txtcode.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 15)
        Me.Label5.TabIndex = 76
        Me.Label5.Text = "Item Code:"
        '
        'txtprice
        '
        Me.txtprice.BackColor = System.Drawing.Color.White
        Me.txtprice.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprice.Location = New System.Drawing.Point(94, 226)
        Me.txtprice.Name = "txtprice"
        Me.txtprice.Size = New System.Drawing.Size(206, 21)
        Me.txtprice.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(48, 229)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 15)
        Me.Label4.TabIndex = 75
        Me.Label4.Text = "Price:"
        '
        'txtdes
        '
        Me.txtdes.BackColor = System.Drawing.Color.White
        Me.txtdes.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdes.Location = New System.Drawing.Point(94, 188)
        Me.txtdes.Name = "txtdes"
        Me.txtdes.Size = New System.Drawing.Size(206, 21)
        Me.txtdes.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 191)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 15)
        Me.Label3.TabIndex = 74
        Me.Label3.Text = "Description:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 152)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 15)
        Me.Label1.TabIndex = 73
        Me.Label1.Text = "Item Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(27, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 15)
        Me.Label2.TabIndex = 72
        Me.Label2.Text = "Category:"
        '
        'cmbcat
        '
        Me.cmbcat.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcat.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbcat.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcat.FormattingEnabled = True
        Me.cmbcat.Location = New System.Drawing.Point(94, 67)
        Me.cmbcat.Name = "cmbcat"
        Me.cmbcat.Size = New System.Drawing.Size(206, 23)
        Me.cmbcat.TabIndex = 1
        '
        'btnsearch
        '
        Me.btnsearch.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsearch.Image = CType(resources.GetObject("btnsearch.Image"), System.Drawing.Image)
        Me.btnsearch.Location = New System.Drawing.Point(94, 479)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(99, 23)
        Me.btnsearch.TabIndex = 10
        Me.btnsearch.Text = "Search"
        Me.btnsearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnsearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'btnupdate
        '
        Me.btnupdate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.Image = CType(resources.GetObject("btnupdate.Image"), System.Drawing.Image)
        Me.btnupdate.Location = New System.Drawing.Point(201, 421)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(99, 23)
        Me.btnupdate.TabIndex = 7
        Me.btnupdate.Text = "&Update"
        Me.btnupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = True
        '
        'btncancel
        '
        Me.btncancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(201, 450)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(99, 23)
        Me.btncancel.TabIndex = 9
        Me.btncancel.Text = "&Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'btnadditem
        '
        Me.btnadditem.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadditem.Image = CType(resources.GetObject("btnadditem.Image"), System.Drawing.Image)
        Me.btnadditem.Location = New System.Drawing.Point(94, 421)
        Me.btnadditem.Name = "btnadditem"
        Me.btnadditem.Size = New System.Drawing.Size(99, 23)
        Me.btnadditem.TabIndex = 6
        Me.btnadditem.Text = "&Add Item"
        Me.btnadditem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadditem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadditem.UseVisualStyleBackColor = True
        '
        'chkhide
        '
        Me.chkhide.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkhide.AutoSize = True
        Me.chkhide.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkhide.Location = New System.Drawing.Point(931, 19)
        Me.chkhide.Name = "chkhide"
        Me.chkhide.Size = New System.Drawing.Size(124, 18)
        Me.chkhide.TabIndex = 17
        Me.chkhide.Text = "Deactivated Items"
        Me.chkhide.UseVisualStyleBackColor = True
        '
        'grditems
        '
        Me.grditems.AllowUserToAddRows = False
        Me.grditems.AllowUserToDeleteRows = False
        Me.grditems.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grditems.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grditems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grditems.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grditems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grditems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8})
        Me.grditems.EnableHeadersVisualStyles = False
        Me.grditems.Location = New System.Drawing.Point(337, 43)
        Me.grditems.Name = "grditems"
        Me.grditems.ReadOnly = True
        Me.grditems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.grditems.RowHeadersWidth = 10
        Me.grditems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grditems.Size = New System.Drawing.Size(718, 564)
        Me.grditems.TabIndex = 61
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
        Me.Column2.HeaderText = "Item Code"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Item Name"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 200
        '
        'Column4
        '
        Me.Column4.HeaderText = "Description"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        DataGridViewCellStyle3.Format = "n2"
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column5.HeaderText = "Price"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "Category"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "Ticket Type"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column8
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column8.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column8.HeaderText = "Status"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'newitems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1067, 619)
        Me.Controls.Add(Me.grditems)
        Me.Controls.Add(Me.chkhide)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "newitems"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Items"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grditems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents btnadditem As System.Windows.Forms.Button
    Friend WithEvents btndiscon As System.Windows.Forms.Button
    Friend WithEvents chkhide As System.Windows.Forms.CheckBox
    Friend WithEvents txtname As System.Windows.Forms.TextBox
    Friend WithEvents txtstatus As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtcode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtprice As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtdes As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbcat As System.Windows.Forms.ComboBox
    Friend WithEvents btnexport As System.Windows.Forms.Button
    Friend WithEvents btnimport As System.Windows.Forms.Button
    Friend WithEvents btnview As System.Windows.Forms.Button
    Friend WithEvents btnprint As System.Windows.Forms.Button
    Friend WithEvents txtid As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblname As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents grditems As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
End Class
