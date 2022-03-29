<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CopyusersWoutsigns
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CopyusersWoutsigns))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.btncancel = New System.Windows.Forms.Button
        Me.link = New System.Windows.Forms.LinkLabel
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtconfirm = New System.Windows.Forms.TextBox
        Me.lbluserid = New System.Windows.Forms.Label
        Me.chkpass = New System.Windows.Forms.CheckBox
        Me.btnviewall = New System.Windows.Forms.Button
        Me.btndeactivate = New System.Windows.Forms.Button
        Me.btnupdate = New System.Windows.Forms.Button
        Me.btnadd = New System.Windows.Forms.Button
        Me.cmbgroup = New System.Windows.Forms.ComboBox
        Me.txtpass = New System.Windows.Forms.TextBox
        Me.txtusername = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdusers = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.txtfull = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblcas = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmbwhse = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbbranch = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnsearch = New System.Windows.Forms.Button
        Me.lblfull = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmbdept = New System.Windows.Forms.ComboBox
        Me.chkhide = New System.Windows.Forms.CheckBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmbfilter = New System.Windows.Forms.ComboBox
        Me.cmbbr = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        CType(Me.grdusers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btncancel
        '
        Me.btncancel.Enabled = False
        Me.btncancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(226, 390)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(104, 30)
        Me.btncancel.TabIndex = 9
        Me.btncancel.Text = "Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'link
        '
        Me.link.AutoSize = True
        Me.link.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.link.LinkColor = System.Drawing.Color.Red
        Me.link.Location = New System.Drawing.Point(240, 133)
        Me.link.Name = "link"
        Me.link.Size = New System.Drawing.Size(90, 13)
        Me.link.TabIndex = 11
        Me.link.TabStop = True
        Me.link.Text = "Change Password"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 155)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 15)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Confirm Password:"
        '
        'txtconfirm
        '
        Me.txtconfirm.Enabled = False
        Me.txtconfirm.Location = New System.Drawing.Point(133, 153)
        Me.txtconfirm.Name = "txtconfirm"
        Me.txtconfirm.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtconfirm.Size = New System.Drawing.Size(197, 20)
        Me.txtconfirm.TabIndex = 3
        '
        'lbluserid
        '
        Me.lbluserid.AutoSize = True
        Me.lbluserid.Location = New System.Drawing.Point(18, 50)
        Me.lbluserid.Name = "lbluserid"
        Me.lbluserid.Size = New System.Drawing.Size(0, 13)
        Me.lbluserid.TabIndex = 30
        Me.lbluserid.Visible = False
        '
        'chkpass
        '
        Me.chkpass.AutoSize = True
        Me.chkpass.Enabled = False
        Me.chkpass.Font = New System.Drawing.Font("Arial", 7.0!)
        Me.chkpass.Location = New System.Drawing.Point(144, 134)
        Me.chkpass.Name = "chkpass"
        Me.chkpass.Size = New System.Drawing.Size(52, 17)
        Me.chkpass.TabIndex = 10
        Me.chkpass.Text = "Show"
        Me.chkpass.UseVisualStyleBackColor = True
        '
        'btnviewall
        '
        Me.btnviewall.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnviewall.Image = CType(resources.GetObject("btnviewall.Image"), System.Drawing.Image)
        Me.btnviewall.Location = New System.Drawing.Point(116, 462)
        Me.btnviewall.Name = "btnviewall"
        Me.btnviewall.Size = New System.Drawing.Size(214, 30)
        Me.btnviewall.TabIndex = 8
        Me.btnviewall.Text = "View All"
        Me.btnviewall.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnviewall.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnviewall.UseVisualStyleBackColor = True
        '
        'btndeactivate
        '
        Me.btndeactivate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndeactivate.Image = CType(resources.GetObject("btndeactivate.Image"), System.Drawing.Image)
        Me.btndeactivate.Location = New System.Drawing.Point(116, 390)
        Me.btndeactivate.Name = "btndeactivate"
        Me.btndeactivate.Size = New System.Drawing.Size(104, 30)
        Me.btndeactivate.TabIndex = 7
        Me.btndeactivate.Text = "Deactivate"
        Me.btndeactivate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndeactivate.UseVisualStyleBackColor = True
        '
        'btnupdate
        '
        Me.btnupdate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.Image = CType(resources.GetObject("btnupdate.Image"), System.Drawing.Image)
        Me.btnupdate.Location = New System.Drawing.Point(226, 354)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(104, 30)
        Me.btnupdate.TabIndex = 6
        Me.btnupdate.Text = "Update"
        Me.btnupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = True
        '
        'btnadd
        '
        Me.btnadd.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.Image = CType(resources.GetObject("btnadd.Image"), System.Drawing.Image)
        Me.btnadd.Location = New System.Drawing.Point(116, 354)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(104, 30)
        Me.btnadd.TabIndex = 5
        Me.btnadd.Text = "Add"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'cmbgroup
        '
        Me.cmbgroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbgroup.Enabled = False
        Me.cmbgroup.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbgroup.FormattingEnabled = True
        Me.cmbgroup.Location = New System.Drawing.Point(133, 216)
        Me.cmbgroup.Name = "cmbgroup"
        Me.cmbgroup.Size = New System.Drawing.Size(197, 21)
        Me.cmbgroup.TabIndex = 4
        '
        'txtpass
        '
        Me.txtpass.Location = New System.Drawing.Point(133, 108)
        Me.txtpass.Name = "txtpass"
        Me.txtpass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpass.Size = New System.Drawing.Size(197, 20)
        Me.txtpass.TabIndex = 2
        '
        'txtusername
        '
        Me.txtusername.Location = New System.Drawing.Point(133, 79)
        Me.txtusername.Name = "txtusername"
        Me.txtusername.Size = New System.Drawing.Size(197, 20)
        Me.txtusername.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(53, 218)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 15)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Workgroup:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(59, 110)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 15)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Password:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(58, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 15)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Username:"
        '
        'grdusers
        '
        Me.grdusers.AllowUserToAddRows = False
        Me.grdusers.AllowUserToDeleteRows = False
        Me.grdusers.AllowUserToResizeRows = False
        Me.grdusers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdusers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdusers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdusers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdusers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column7, Me.Column2, Me.Column3, Me.Column9, Me.Column4, Me.Column8, Me.Column10, Me.Column5, Me.Column6})
        Me.grdusers.Location = New System.Drawing.Point(368, 41)
        Me.grdusers.Name = "grdusers"
        Me.grdusers.ReadOnly = True
        Me.grdusers.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.grdusers.RowHeadersWidth = 10
        Me.grdusers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdusers.Size = New System.Drawing.Size(814, 523)
        Me.grdusers.TabIndex = 12
        '
        'Column1
        '
        Me.Column1.HeaderText = "userid"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        '
        'Column7
        '
        Me.Column7.HeaderText = "Full Name"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 164
        '
        'Column2
        '
        Me.Column2.HeaderText = "Username"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 130
        '
        'Column3
        '
        Me.Column3.HeaderText = "Password"
        Me.Column3.MinimumWidth = 120
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column3.Width = 120
        '
        'Column9
        '
        Me.Column9.HeaderText = "Department"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 120
        '
        'Column4
        '
        Me.Column4.HeaderText = "Workgroup"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 120
        '
        'Column8
        '
        Me.Column8.HeaderText = "Warehouse"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 80
        '
        'Column10
        '
        Me.Column10.HeaderText = "Branch"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Status"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "pass"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Visible = False
        '
        'txtfull
        '
        Me.txtfull.Location = New System.Drawing.Point(133, 50)
        Me.txtfull.Name = "txtfull"
        Me.txtfull.Size = New System.Drawing.Size(197, 20)
        Me.txtfull.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(62, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 15)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Full Name:"
        '
        'lblcas
        '
        Me.lblcas.AutoSize = True
        Me.lblcas.Location = New System.Drawing.Point(330, 34)
        Me.lblcas.Name = "lblcas"
        Me.lblcas.Size = New System.Drawing.Size(0, 13)
        Me.lblcas.TabIndex = 35
        Me.lblcas.Visible = False
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label6.Location = New System.Drawing.Point(9, 567)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "-Jecel-"
        '
        'cmbwhse
        '
        Me.cmbwhse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbwhse.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbwhse.FormattingEnabled = True
        Me.cmbwhse.Location = New System.Drawing.Point(133, 249)
        Me.cmbwhse.Name = "cmbwhse"
        Me.cmbwhse.Size = New System.Drawing.Size(197, 21)
        Me.cmbwhse.TabIndex = 43
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(5, 251)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(122, 15)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "Access Warehouse:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cmbbranch)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.btnsearch)
        Me.GroupBox1.Controls.Add(Me.lblfull)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cmbdept)
        Me.GroupBox1.Controls.Add(Me.txtfull)
        Me.GroupBox1.Controls.Add(Me.cmbwhse)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblcas)
        Me.GroupBox1.Controls.Add(Me.txtusername)
        Me.GroupBox1.Controls.Add(Me.txtpass)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbgroup)
        Me.GroupBox1.Controls.Add(Me.btncancel)
        Me.GroupBox1.Controls.Add(Me.btnadd)
        Me.GroupBox1.Controls.Add(Me.link)
        Me.GroupBox1.Controls.Add(Me.btnupdate)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btndeactivate)
        Me.GroupBox1.Controls.Add(Me.txtconfirm)
        Me.GroupBox1.Controls.Add(Me.btnviewall)
        Me.GroupBox1.Controls.Add(Me.lbluserid)
        Me.GroupBox1.Controls.Add(Me.chkpass)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(350, 549)
        Me.GroupBox1.TabIndex = 45
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Users"
        '
        'cmbbranch
        '
        Me.cmbbranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbbranch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbbranch.FormattingEnabled = True
        Me.cmbbranch.Location = New System.Drawing.Point(133, 281)
        Me.cmbbranch.Name = "cmbbranch"
        Me.cmbbranch.Size = New System.Drawing.Size(197, 21)
        Me.cmbbranch.TabIndex = 49
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(30, 283)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(97, 15)
        Me.Label10.TabIndex = 50
        Me.Label10.Text = "Access Branch:"
        '
        'btnsearch
        '
        Me.btnsearch.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsearch.Image = CType(resources.GetObject("btnsearch.Image"), System.Drawing.Image)
        Me.btnsearch.Location = New System.Drawing.Point(116, 426)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(214, 30)
        Me.btnsearch.TabIndex = 48
        Me.btnsearch.Text = "Search"
        Me.btnsearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnsearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'lblfull
        '
        Me.lblfull.AutoSize = True
        Me.lblfull.Location = New System.Drawing.Point(287, 34)
        Me.lblfull.Name = "lblfull"
        Me.lblfull.Size = New System.Drawing.Size(0, 13)
        Me.lblfull.TabIndex = 47
        Me.lblfull.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(50, 186)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 15)
        Me.Label8.TabIndex = 46
        Me.Label8.Text = "Department:"
        '
        'cmbdept
        '
        Me.cmbdept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdept.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbdept.FormattingEnabled = True
        Me.cmbdept.Items.AddRange(New Object() {"", "Administrator", "Supervisor", "User"})
        Me.cmbdept.Location = New System.Drawing.Point(133, 184)
        Me.cmbdept.Name = "cmbdept"
        Me.cmbdept.Size = New System.Drawing.Size(197, 21)
        Me.cmbdept.TabIndex = 45
        '
        'chkhide
        '
        Me.chkhide.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkhide.AutoSize = True
        Me.chkhide.Checked = True
        Me.chkhide.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkhide.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkhide.Location = New System.Drawing.Point(1018, 15)
        Me.chkhide.Name = "chkhide"
        Me.chkhide.Size = New System.Drawing.Size(164, 18)
        Me.chkhide.TabIndex = 49
        Me.chkhide.Text = "Hide Deactivated Account"
        Me.chkhide.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(371, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(108, 15)
        Me.Label9.TabIndex = 50
        Me.Label9.Text = "Filter Department:"
        '
        'cmbfilter
        '
        Me.cmbfilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbfilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbfilter.FormattingEnabled = True
        Me.cmbfilter.Items.AddRange(New Object() {"", "Administrator", "Supervisor", "User"})
        Me.cmbfilter.Location = New System.Drawing.Point(487, 13)
        Me.cmbfilter.Name = "cmbfilter"
        Me.cmbfilter.Size = New System.Drawing.Size(197, 21)
        Me.cmbfilter.TabIndex = 49
        '
        'cmbbr
        '
        Me.cmbbr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbbr.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbbr.FormattingEnabled = True
        Me.cmbbr.Location = New System.Drawing.Point(794, 13)
        Me.cmbbr.Name = "cmbbr"
        Me.cmbbr.Size = New System.Drawing.Size(197, 21)
        Me.cmbbr.TabIndex = 51
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(706, 15)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(82, 15)
        Me.Label11.TabIndex = 52
        Me.Label11.Text = "Filter Branch:"
        '
        'users
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1194, 584)
        Me.Controls.Add(Me.cmbbr)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.chkhide)
        Me.Controls.Add(Me.cmbfilter)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.grdusers)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "users"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manage Users"
        CType(Me.grdusers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents link As System.Windows.Forms.LinkLabel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtconfirm As System.Windows.Forms.TextBox
    Friend WithEvents lbluserid As System.Windows.Forms.Label
    Friend WithEvents chkpass As System.Windows.Forms.CheckBox
    Friend WithEvents btnviewall As System.Windows.Forms.Button
    Friend WithEvents btndeactivate As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents cmbgroup As System.Windows.Forms.ComboBox
    Friend WithEvents txtpass As System.Windows.Forms.TextBox
    Friend WithEvents txtusername As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdusers As System.Windows.Forms.DataGridView
    Friend WithEvents txtfull As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblcas As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbwhse As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkhide As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbdept As System.Windows.Forms.ComboBox
    Friend WithEvents lblfull As System.Windows.Forms.Label
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbfilter As System.Windows.Forms.ComboBox
    Friend WithEvents cmbbranch As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbbr As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
