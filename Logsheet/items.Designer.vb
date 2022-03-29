<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class items
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(items))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.lblid = New System.Windows.Forms.Label
        Me.txtname = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnavail = New System.Windows.Forms.Button
        Me.paneladd = New System.Windows.Forms.Panel
        Me.Label16 = New System.Windows.Forms.Label
        Me.acmbtype = New System.Windows.Forms.ComboBox
        Me.atxtname = New System.Windows.Forms.TextBox
        Me.atxtcode = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.atxtprice = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.atxtdes = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.acmbcat = New System.Windows.Forms.ComboBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.cmbtype = New System.Windows.Forms.ComboBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.cmbcategory = New System.Windows.Forms.ComboBox
        Me.cmbgroup = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.lbls = New System.Windows.Forms.Label
        Me.btnview = New System.Windows.Forms.Button
        Me.lblname = New System.Windows.Forms.Label
        Me.lblcode = New System.Windows.Forms.Label
        Me.btnupdate = New System.Windows.Forms.Button
        Me.txtstatus = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtcode = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.btncancel = New System.Windows.Forms.Button
        Me.btnadditem = New System.Windows.Forms.Button
        Me.txtprice = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtdes = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbname = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbcat = New System.Windows.Forms.ComboBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnexport = New System.Windows.Forms.Button
        Me.Label13 = New System.Windows.Forms.Label
        Me.btndiscon = New System.Windows.Forms.Button
        Me.cmbview = New System.Windows.Forms.ComboBox
        Me.btnselect = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.chkhide = New System.Windows.Forms.CheckBox
        Me.btnprint = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.grditems = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.GroupBox1.SuspendLayout()
        Me.paneladd.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grditems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblid
        '
        Me.lblid.AutoSize = True
        Me.lblid.Location = New System.Drawing.Point(240, 29)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(45, 15)
        Me.lblid.TabIndex = 25
        Me.lblid.Text = "Label7"
        Me.lblid.Visible = False
        '
        'txtname
        '
        Me.txtname.BackColor = System.Drawing.Color.White
        Me.txtname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtname.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtname.Location = New System.Drawing.Point(104, 146)
        Me.txtname.Name = "txtname"
        Me.txtname.ReadOnly = True
        Me.txtname.Size = New System.Drawing.Size(206, 21)
        Me.txtname.TabIndex = 2
        Me.txtname.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnavail)
        Me.GroupBox1.Controls.Add(Me.paneladd)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.cmbtype)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.cmbcategory)
        Me.GroupBox1.Controls.Add(Me.cmbgroup)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lbls)
        Me.GroupBox1.Controls.Add(Me.btnview)
        Me.GroupBox1.Controls.Add(Me.lblname)
        Me.GroupBox1.Controls.Add(Me.lblcode)
        Me.GroupBox1.Controls.Add(Me.lblid)
        Me.GroupBox1.Controls.Add(Me.txtname)
        Me.GroupBox1.Controls.Add(Me.btnupdate)
        Me.GroupBox1.Controls.Add(Me.txtstatus)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtcode)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.btncancel)
        Me.GroupBox1.Controls.Add(Me.btnadditem)
        Me.GroupBox1.Controls.Add(Me.txtprice)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtdes)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbname)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbcat)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(336, 597)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Item"
        '
        'btnavail
        '
        Me.btnavail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnavail.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnavail.Image = CType(resources.GetObject("btnavail.Image"), System.Drawing.Image)
        Me.btnavail.Location = New System.Drawing.Point(104, 537)
        Me.btnavail.Name = "btnavail"
        Me.btnavail.Size = New System.Drawing.Size(206, 23)
        Me.btnavail.TabIndex = 63
        Me.btnavail.Text = "Available"
        Me.btnavail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnavail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnavail.UseVisualStyleBackColor = True
        '
        'paneladd
        '
        Me.paneladd.Controls.Add(Me.Label16)
        Me.paneladd.Controls.Add(Me.acmbtype)
        Me.paneladd.Controls.Add(Me.atxtname)
        Me.paneladd.Controls.Add(Me.atxtcode)
        Me.paneladd.Controls.Add(Me.Label8)
        Me.paneladd.Controls.Add(Me.atxtprice)
        Me.paneladd.Controls.Add(Me.Label9)
        Me.paneladd.Controls.Add(Me.atxtdes)
        Me.paneladd.Controls.Add(Me.Label10)
        Me.paneladd.Controls.Add(Me.Label11)
        Me.paneladd.Controls.Add(Me.Label12)
        Me.paneladd.Controls.Add(Me.acmbcat)
        Me.paneladd.Location = New System.Drawing.Point(316, 29)
        Me.paneladd.Name = "paneladd"
        Me.paneladd.Size = New System.Drawing.Size(324, 321)
        Me.paneladd.TabIndex = 62
        Me.paneladd.Visible = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(55, 246)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(36, 15)
        Me.Label16.TabIndex = 63
        Me.Label16.Text = "Type:"
        '
        'acmbtype
        '
        Me.acmbtype.BackColor = System.Drawing.SystemColors.Window
        Me.acmbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.acmbtype.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.acmbtype.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.acmbtype.FormattingEnabled = True
        Me.acmbtype.Items.AddRange(New Object() {"", "Hard", "Soft"})
        Me.acmbtype.Location = New System.Drawing.Point(97, 243)
        Me.acmbtype.Name = "acmbtype"
        Me.acmbtype.Size = New System.Drawing.Size(206, 23)
        Me.acmbtype.TabIndex = 62
        '
        'atxtname
        '
        Me.atxtname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.atxtname.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.atxtname.Location = New System.Drawing.Point(97, 126)
        Me.atxtname.Name = "atxtname"
        Me.atxtname.Size = New System.Drawing.Size(206, 21)
        Me.atxtname.TabIndex = 8
        '
        'atxtcode
        '
        Me.atxtcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.atxtcode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.atxtcode.Location = New System.Drawing.Point(97, 86)
        Me.atxtcode.Name = "atxtcode"
        Me.atxtcode.Size = New System.Drawing.Size(206, 21)
        Me.atxtcode.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(25, 89)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 15)
        Me.Label8.TabIndex = 46
        Me.Label8.Text = "Item Code:"
        '
        'atxtprice
        '
        Me.atxtprice.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.atxtprice.Location = New System.Drawing.Point(97, 208)
        Me.atxtprice.Name = "atxtprice"
        Me.atxtprice.Size = New System.Drawing.Size(206, 21)
        Me.atxtprice.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(52, 211)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(40, 15)
        Me.Label9.TabIndex = 45
        Me.Label9.Text = "Price:"
        '
        'atxtdes
        '
        Me.atxtdes.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.atxtdes.Location = New System.Drawing.Point(97, 170)
        Me.atxtdes.Name = "atxtdes"
        Me.atxtdes.Size = New System.Drawing.Size(206, 21)
        Me.atxtdes.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(17, 173)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 15)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "Description:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(21, 129)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(71, 15)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = "Item Name:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(31, 45)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(61, 15)
        Me.Label12.TabIndex = 42
        Me.Label12.Text = "Category:"
        '
        'acmbcat
        '
        Me.acmbcat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.acmbcat.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.acmbcat.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.acmbcat.FormattingEnabled = True
        Me.acmbcat.Location = New System.Drawing.Point(98, 42)
        Me.acmbcat.Name = "acmbcat"
        Me.acmbcat.Size = New System.Drawing.Size(206, 23)
        Me.acmbcat.TabIndex = 6
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(62, 266)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(36, 15)
        Me.Label15.TabIndex = 61
        Me.Label15.Text = "Type:"
        '
        'cmbtype
        '
        Me.cmbtype.BackColor = System.Drawing.SystemColors.Window
        Me.cmbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtype.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbtype.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtype.FormattingEnabled = True
        Me.cmbtype.Items.AddRange(New Object() {"", "Hard", "Soft", "Specialty"})
        Me.cmbtype.Location = New System.Drawing.Point(104, 263)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.Size = New System.Drawing.Size(206, 23)
        Me.cmbtype.TabIndex = 60
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(8, 386)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(68, 97)
        Me.Label14.TabIndex = 59
        '
        'cmbcategory
        '
        Me.cmbcategory.FormattingEnabled = True
        Me.cmbcategory.Location = New System.Drawing.Point(17, 443)
        Me.cmbcategory.Name = "cmbcategory"
        Me.cmbcategory.Size = New System.Drawing.Size(44, 23)
        Me.cmbcategory.TabIndex = 58
        '
        'cmbgroup
        '
        Me.cmbgroup.FormattingEnabled = True
        Me.cmbgroup.Location = New System.Drawing.Point(17, 414)
        Me.cmbgroup.Name = "cmbgroup"
        Me.cmbgroup.Size = New System.Drawing.Size(44, 23)
        Me.cmbgroup.TabIndex = 57
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label7.Location = New System.Drawing.Point(14, 578)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 54
        Me.Label7.Text = "-Jecel-"
        '
        'lbls
        '
        Me.lbls.AutoSize = True
        Me.lbls.Location = New System.Drawing.Point(101, 29)
        Me.lbls.Name = "lbls"
        Me.lbls.Size = New System.Drawing.Size(49, 15)
        Me.lbls.TabIndex = 52
        Me.lbls.Text = "Search:"
        '
        'btnview
        '
        Me.btnview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnview.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnview.Image = CType(resources.GetObject("btnview.Image"), System.Drawing.Image)
        Me.btnview.Location = New System.Drawing.Point(104, 566)
        Me.btnview.Name = "btnview"
        Me.btnview.Size = New System.Drawing.Size(206, 23)
        Me.btnview.TabIndex = 20
        Me.btnview.Text = "&Refresh"
        Me.btnview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnview.UseVisualStyleBackColor = True
        '
        'lblname
        '
        Me.lblname.AutoSize = True
        Me.lblname.Location = New System.Drawing.Point(265, 346)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(45, 15)
        Me.lblname.TabIndex = 48
        Me.lblname.Text = "Label7"
        Me.lblname.Visible = False
        '
        'lblcode
        '
        Me.lblcode.AutoSize = True
        Me.lblcode.Location = New System.Drawing.Point(101, 346)
        Me.lblcode.Name = "lblcode"
        Me.lblcode.Size = New System.Drawing.Size(45, 15)
        Me.lblcode.TabIndex = 47
        Me.lblcode.Text = "Label7"
        Me.lblcode.Visible = False
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.Enabled = False
        Me.btnupdate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.Image = CType(resources.GetObject("btnupdate.Image"), System.Drawing.Image)
        Me.btnupdate.Location = New System.Drawing.Point(104, 477)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(206, 23)
        Me.btnupdate.TabIndex = 12
        Me.btnupdate.Text = "&Update Item"
        Me.btnupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = True
        '
        'txtstatus
        '
        Me.txtstatus.BackColor = System.Drawing.Color.White
        Me.txtstatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstatus.Location = New System.Drawing.Point(104, 300)
        Me.txtstatus.Name = "txtstatus"
        Me.txtstatus.ReadOnly = True
        Me.txtstatus.Size = New System.Drawing.Size(206, 21)
        Me.txtstatus.TabIndex = 5
        Me.txtstatus.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(51, 303)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 15)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Status:"
        Me.Label6.Visible = False
        '
        'txtcode
        '
        Me.txtcode.BackColor = System.Drawing.Color.White
        Me.txtcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcode.Location = New System.Drawing.Point(104, 106)
        Me.txtcode.Name = "txtcode"
        Me.txtcode.ReadOnly = True
        Me.txtcode.Size = New System.Drawing.Size(206, 21)
        Me.txtcode.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(31, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 15)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Item Code:"
        '
        'btncancel
        '
        Me.btncancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btncancel.Enabled = False
        Me.btncancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(104, 508)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(206, 23)
        Me.btncancel.TabIndex = 13
        Me.btncancel.Text = "&Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'btnadditem
        '
        Me.btnadditem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnadditem.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadditem.Image = CType(resources.GetObject("btnadditem.Image"), System.Drawing.Image)
        Me.btnadditem.Location = New System.Drawing.Point(104, 445)
        Me.btnadditem.Name = "btnadditem"
        Me.btnadditem.Size = New System.Drawing.Size(206, 23)
        Me.btnadditem.TabIndex = 11
        Me.btnadditem.Text = "&Add Item"
        Me.btnadditem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadditem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadditem.UseVisualStyleBackColor = True
        '
        'txtprice
        '
        Me.txtprice.BackColor = System.Drawing.Color.White
        Me.txtprice.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprice.Location = New System.Drawing.Point(104, 228)
        Me.txtprice.Name = "txtprice"
        Me.txtprice.ReadOnly = True
        Me.txtprice.Size = New System.Drawing.Size(206, 21)
        Me.txtprice.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(58, 231)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 15)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Price:"
        '
        'txtdes
        '
        Me.txtdes.BackColor = System.Drawing.Color.White
        Me.txtdes.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdes.Location = New System.Drawing.Point(104, 190)
        Me.txtdes.Name = "txtdes"
        Me.txtdes.ReadOnly = True
        Me.txtdes.Size = New System.Drawing.Size(206, 21)
        Me.txtdes.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(23, 193)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 15)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Description:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 149)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 15)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Item Name:"
        '
        'cmbname
        '
        Me.cmbname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbname.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbname.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbname.FormattingEnabled = True
        Me.cmbname.Location = New System.Drawing.Point(104, 146)
        Me.cmbname.Name = "cmbname"
        Me.cmbname.Size = New System.Drawing.Size(206, 23)
        Me.cmbname.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(37, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 15)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Category:"
        '
        'cmbcat
        '
        Me.cmbcat.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcat.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbcat.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcat.FormattingEnabled = True
        Me.cmbcat.Location = New System.Drawing.Point(104, 62)
        Me.cmbcat.Name = "cmbcat"
        Me.cmbcat.Size = New System.Drawing.Size(206, 23)
        Me.cmbcat.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.btnexport)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.btndiscon)
        Me.GroupBox2.Controls.Add(Me.cmbview)
        Me.GroupBox2.Controls.Add(Me.btnselect)
        Me.GroupBox2.Controls.Add(Me.Button4)
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Controls.Add(Me.chkhide)
        Me.GroupBox2.Controls.Add(Me.btnprint)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.grditems)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(350, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(656, 597)
        Me.GroupBox2.TabIndex = 53
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Items"
        '
        'btnexport
        '
        Me.btnexport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnexport.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnexport.Image = CType(resources.GetObject("btnexport.Image"), System.Drawing.Image)
        Me.btnexport.Location = New System.Drawing.Point(257, 566)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(134, 23)
        Me.btnexport.TabIndex = 60
        Me.btnexport.Text = "Export Items"
        Me.btnexport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnexport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexport.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(14, 18)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(38, 15)
        Me.Label13.TabIndex = 56
        Me.Label13.Text = "View:"
        '
        'btndiscon
        '
        Me.btndiscon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btndiscon.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndiscon.Image = CType(resources.GetObject("btndiscon.Image"), System.Drawing.Image)
        Me.btndiscon.Location = New System.Drawing.Point(548, 15)
        Me.btndiscon.Name = "btndiscon"
        Me.btndiscon.Size = New System.Drawing.Size(102, 23)
        Me.btndiscon.TabIndex = 38
        Me.btndiscon.Text = "Discontinue"
        Me.btndiscon.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btndiscon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndiscon.UseVisualStyleBackColor = True
        '
        'cmbview
        '
        Me.cmbview.BackColor = System.Drawing.SystemColors.Window
        Me.cmbview.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbview.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbview.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbview.FormattingEnabled = True
        Me.cmbview.Location = New System.Drawing.Point(58, 15)
        Me.cmbview.Name = "cmbview"
        Me.cmbview.Size = New System.Drawing.Size(206, 23)
        Me.cmbview.Sorted = True
        Me.cmbview.TabIndex = 55
        '
        'btnselect
        '
        Me.btnselect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnselect.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnselect.Image = CType(resources.GetObject("btnselect.Image"), System.Drawing.Image)
        Me.btnselect.Location = New System.Drawing.Point(440, 15)
        Me.btnselect.Name = "btnselect"
        Me.btnselect.Size = New System.Drawing.Size(102, 23)
        Me.btnselect.TabIndex = 37
        Me.btnselect.Text = "Check All"
        Me.btnselect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnselect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnselect.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.Location = New System.Drawing.Point(143, 566)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(108, 23)
        Me.Button4.TabIndex = 36
        Me.Button4.Text = "&Import"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.Location = New System.Drawing.Point(498, 566)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(63, 23)
        Me.Button3.TabIndex = 35
        Me.Button3.Text = "&Manage Discount of Items"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'chkhide
        '
        Me.chkhide.AutoSize = True
        Me.chkhide.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkhide.Location = New System.Drawing.Point(280, 18)
        Me.chkhide.Name = "chkhide"
        Me.chkhide.Size = New System.Drawing.Size(133, 18)
        Me.chkhide.TabIndex = 33
        Me.chkhide.Text = "Discontinued Items"
        Me.chkhide.UseVisualStyleBackColor = True
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnprint.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Image = CType(resources.GetObject("btnprint.Image"), System.Drawing.Image)
        Me.btnprint.Location = New System.Drawing.Point(7, 566)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(130, 23)
        Me.btnprint.TabIndex = 19
        Me.btnprint.Text = "&Print Items"
        Me.btnprint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnprint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(10, 475)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(54, 13)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
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
        Me.grditems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grditems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8, Me.Column9})
        Me.grditems.Location = New System.Drawing.Point(7, 44)
        Me.grditems.Name = "grditems"
        Me.grditems.ReadOnly = True
        Me.grditems.RowHeadersWidth = 10
        Me.grditems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grditems.Size = New System.Drawing.Size(643, 516)
        Me.grditems.TabIndex = 32
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
        '
        'Column4
        '
        Me.Column4.HeaderText = "Description"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
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
        Me.Column7.HeaderText = "Flour Type"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column8
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column8.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column8.HeaderText = "Status"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "Select"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'items
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1018, 621)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "items"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manage Items"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.paneladd.ResumeLayout(False)
        Me.paneladd.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grditems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblid As System.Windows.Forms.Label
    Friend WithEvents txtname As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents txtstatus As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtcode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents btnadditem As System.Windows.Forms.Button
    Friend WithEvents txtprice As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtdes As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbname As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbcat As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents grditems As System.Windows.Forms.DataGridView
    Friend WithEvents btnprint As System.Windows.Forms.Button
    Friend WithEvents btnview As System.Windows.Forms.Button
    Friend WithEvents lblcode As System.Windows.Forms.Label
    Friend WithEvents lblname As System.Windows.Forms.Label
    Friend WithEvents lbls As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkhide As System.Windows.Forms.CheckBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents btnselect As System.Windows.Forms.Button
    Friend WithEvents btndiscon As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbview As System.Windows.Forms.ComboBox
    Friend WithEvents cmbgroup As System.Windows.Forms.ComboBox
    Friend WithEvents cmbcategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnexport As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents paneladd As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents acmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents atxtname As System.Windows.Forms.TextBox
    Friend WithEvents atxtcode As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents atxtprice As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents atxtdes As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents acmbcat As System.Windows.Forms.ComboBox
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents btnavail As System.Windows.Forms.Button
End Class
