<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class coainfo
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(coainfo))
        Me.ginfo = New System.Windows.Forms.GroupBox
        Me.lblid = New System.Windows.Forms.Label
        Me.lblcoa = New System.Windows.Forms.Label
        Me.Panelinfo = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtcoacus = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtrems = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtdriver = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.datedel = New System.Windows.Forms.DateTimePicker
        Me.txtwrs = New System.Windows.Forms.TextBox
        Me.txttruck = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.dateload = New System.Windows.Forms.DateTimePicker
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtpo = New System.Windows.Forms.TextBox
        Me.txtcus = New System.Windows.Forms.TextBox
        Me.txtcoanum = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblofnum = New System.Windows.Forms.Label
        Me.txtofnum = New System.Windows.Forms.TextBox
        Me.gselect = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtitem = New System.Windows.Forms.TextBox
        Me.grdcoa = New System.Windows.Forms.DataGridView
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lblitemid = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtqty = New System.Windows.Forms.TextBox
        Me.txtrefnum = New System.Windows.Forms.TextBox
        Me.txtbatch = New System.Windows.Forms.TextBox
        Me.ginfo.SuspendLayout()
        Me.Panelinfo.SuspendLayout()
        Me.gselect.SuspendLayout()
        CType(Me.grdcoa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ginfo
        '
        Me.ginfo.Controls.Add(Me.lblid)
        Me.ginfo.Controls.Add(Me.lblcoa)
        Me.ginfo.Controls.Add(Me.Panelinfo)
        Me.ginfo.Controls.Add(Me.txtcoanum)
        Me.ginfo.Controls.Add(Me.Label17)
        Me.ginfo.Controls.Add(Me.Label2)
        Me.ginfo.Controls.Add(Me.lblofnum)
        Me.ginfo.Controls.Add(Me.txtofnum)
        Me.ginfo.Location = New System.Drawing.Point(12, 12)
        Me.ginfo.Name = "ginfo"
        Me.ginfo.Size = New System.Drawing.Size(984, 186)
        Me.ginfo.TabIndex = 4
        Me.ginfo.TabStop = False
        Me.ginfo.Text = "General Info"
        '
        'lblid
        '
        Me.lblid.AutoSize = True
        Me.lblid.Location = New System.Drawing.Point(154, 9)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(0, 13)
        Me.lblid.TabIndex = 32
        '
        'lblcoa
        '
        Me.lblcoa.AutoSize = True
        Me.lblcoa.Location = New System.Drawing.Point(122, 28)
        Me.lblcoa.Name = "lblcoa"
        Me.lblcoa.Size = New System.Drawing.Size(32, 13)
        Me.lblcoa.TabIndex = 31
        Me.lblcoa.Text = "COA."
        '
        'Panelinfo
        '
        Me.Panelinfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panelinfo.Controls.Add(Me.Label12)
        Me.Panelinfo.Controls.Add(Me.txtcoacus)
        Me.Panelinfo.Controls.Add(Me.Label9)
        Me.Panelinfo.Controls.Add(Me.txtrems)
        Me.Panelinfo.Controls.Add(Me.Label6)
        Me.Panelinfo.Controls.Add(Me.txtdriver)
        Me.Panelinfo.Controls.Add(Me.Label11)
        Me.Panelinfo.Controls.Add(Me.Label10)
        Me.Panelinfo.Controls.Add(Me.datedel)
        Me.Panelinfo.Controls.Add(Me.txtwrs)
        Me.Panelinfo.Controls.Add(Me.txttruck)
        Me.Panelinfo.Controls.Add(Me.Label7)
        Me.Panelinfo.Controls.Add(Me.dateload)
        Me.Panelinfo.Controls.Add(Me.Label20)
        Me.Panelinfo.Controls.Add(Me.Label8)
        Me.Panelinfo.Controls.Add(Me.Label13)
        Me.Panelinfo.Controls.Add(Me.txtpo)
        Me.Panelinfo.Controls.Add(Me.txtcus)
        Me.Panelinfo.Location = New System.Drawing.Point(15, 47)
        Me.Panelinfo.Name = "Panelinfo"
        Me.Panelinfo.Size = New System.Drawing.Size(963, 120)
        Me.Panelinfo.TabIndex = 30
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(15, 63)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(95, 13)
        Me.Label12.TabIndex = 35
        Me.Label12.Text = "Specific Customer:"
        '
        'txtcoacus
        '
        Me.txtcoacus.BackColor = System.Drawing.Color.White
        Me.txtcoacus.Location = New System.Drawing.Point(110, 60)
        Me.txtcoacus.Name = "txtcoacus"
        Me.txtcoacus.ReadOnly = True
        Me.txtcoacus.Size = New System.Drawing.Size(220, 20)
        Me.txtcoacus.TabIndex = 36
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(719, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 13)
        Me.Label9.TabIndex = 33
        Me.Label9.Text = "Remarks:"
        '
        'txtrems
        '
        Me.txtrems.BackColor = System.Drawing.Color.White
        Me.txtrems.Location = New System.Drawing.Point(722, 36)
        Me.txtrems.Multiline = True
        Me.txtrems.Name = "txtrems"
        Me.txtrems.ReadOnly = True
        Me.txtrems.Size = New System.Drawing.Size(220, 72)
        Me.txtrems.TabIndex = 34
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(359, 37)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Driver Name:"
        '
        'txtdriver
        '
        Me.txtdriver.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtdriver.Location = New System.Drawing.Point(468, 34)
        Me.txtdriver.Name = "txtdriver"
        Me.txtdriver.ReadOnly = True
        Me.txtdriver.Size = New System.Drawing.Size(220, 20)
        Me.txtdriver.TabIndex = 32
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(359, 11)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(58, 13)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Truck No.:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(359, 68)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 13)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Loading Date:"
        '
        'datedel
        '
        Me.datedel.CustomFormat = "yyyy/MM/dd"
        Me.datedel.Enabled = False
        Me.datedel.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.datedel.Location = New System.Drawing.Point(468, 88)
        Me.datedel.Name = "datedel"
        Me.datedel.Size = New System.Drawing.Size(220, 20)
        Me.datedel.TabIndex = 24
        '
        'txtwrs
        '
        Me.txtwrs.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtwrs.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtwrs.Location = New System.Drawing.Point(110, 8)
        Me.txtwrs.Name = "txtwrs"
        Me.txtwrs.ReadOnly = True
        Me.txtwrs.Size = New System.Drawing.Size(220, 20)
        Me.txtwrs.TabIndex = 30
        '
        'txttruck
        '
        Me.txttruck.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txttruck.Location = New System.Drawing.Point(468, 8)
        Me.txttruck.Name = "txttruck"
        Me.txttruck.ReadOnly = True
        Me.txttruck.Size = New System.Drawing.Size(220, 20)
        Me.txttruck.TabIndex = 20
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(359, 94)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Delivery Date:"
        '
        'dateload
        '
        Me.dateload.CustomFormat = "yyyy/MM/dd"
        Me.dateload.Enabled = False
        Me.dateload.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dateload.Location = New System.Drawing.Point(468, 62)
        Me.dateload.Name = "dateload"
        Me.dateload.Size = New System.Drawing.Size(220, 20)
        Me.dateload.TabIndex = 23
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(15, 11)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(46, 13)
        Me.Label20.TabIndex = 28
        Me.Label20.Text = "WRS #:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 37)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Customer:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(15, 89)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(48, 13)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "PO. No.:"
        '
        'txtpo
        '
        Me.txtpo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtpo.Location = New System.Drawing.Point(110, 86)
        Me.txtpo.Name = "txtpo"
        Me.txtpo.ReadOnly = True
        Me.txtpo.Size = New System.Drawing.Size(220, 20)
        Me.txtpo.TabIndex = 19
        '
        'txtcus
        '
        Me.txtcus.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtcus.Location = New System.Drawing.Point(110, 34)
        Me.txtcus.Name = "txtcus"
        Me.txtcus.ReadOnly = True
        Me.txtcus.Size = New System.Drawing.Size(220, 20)
        Me.txtcus.TabIndex = 18
        '
        'txtcoanum
        '
        Me.txtcoanum.BackColor = System.Drawing.Color.LightCyan
        Me.txtcoanum.Location = New System.Drawing.Point(157, 25)
        Me.txtcoanum.Name = "txtcoanum"
        Me.txtcoanum.ReadOnly = True
        Me.txtcoanum.Size = New System.Drawing.Size(188, 20)
        Me.txtcoanum.TabIndex = 28
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(30, 28)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(42, 13)
        Me.Label17.TabIndex = 27
        Me.Label17.Text = "COA #:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(374, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Order Fill #:"
        '
        'lblofnum
        '
        Me.lblofnum.AutoSize = True
        Me.lblofnum.Location = New System.Drawing.Point(480, 28)
        Me.lblofnum.Name = "lblofnum"
        Me.lblofnum.Size = New System.Drawing.Size(24, 13)
        Me.lblofnum.TabIndex = 27
        Me.lblofnum.Text = "OF."
        '
        'txtofnum
        '
        Me.txtofnum.BackColor = System.Drawing.Color.LightCyan
        Me.txtofnum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtofnum.Location = New System.Drawing.Point(515, 25)
        Me.txtofnum.Name = "txtofnum"
        Me.txtofnum.ReadOnly = True
        Me.txtofnum.Size = New System.Drawing.Size(188, 20)
        Me.txtofnum.TabIndex = 25
        '
        'gselect
        '
        Me.gselect.Controls.Add(Me.Label3)
        Me.gselect.Controls.Add(Me.Label4)
        Me.gselect.Controls.Add(Me.txtitem)
        Me.gselect.Controls.Add(Me.grdcoa)
        Me.gselect.Controls.Add(Me.lblitemid)
        Me.gselect.Controls.Add(Me.Label5)
        Me.gselect.Controls.Add(Me.Label1)
        Me.gselect.Controls.Add(Me.txtqty)
        Me.gselect.Controls.Add(Me.txtrefnum)
        Me.gselect.Controls.Add(Me.txtbatch)
        Me.gselect.Location = New System.Drawing.Point(12, 204)
        Me.gselect.Name = "gselect"
        Me.gselect.Size = New System.Drawing.Size(984, 477)
        Me.gselect.TabIndex = 5
        Me.gselect.TabStop = False
        Me.gselect.Text = "Item Info"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Location = New System.Drawing.Point(374, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Reference #:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(374, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Batch No. / Lot No.:"
        '
        'txtitem
        '
        Me.txtitem.BackColor = System.Drawing.Color.MistyRose
        Me.txtitem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtitem.Location = New System.Drawing.Point(120, 21)
        Me.txtitem.Name = "txtitem"
        Me.txtitem.ReadOnly = True
        Me.txtitem.Size = New System.Drawing.Size(219, 20)
        Me.txtitem.TabIndex = 31
        '
        'grdcoa
        '
        Me.grdcoa.AllowUserToAddRows = False
        Me.grdcoa.AllowUserToDeleteRows = False
        Me.grdcoa.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.grdcoa.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdcoa.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdcoa.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdcoa.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdcoa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdcoa.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.Column1, Me.Column2, Me.Column3, Me.Column6, Me.Column7, Me.Column8, Me.Column9, Me.Column10, Me.Column11})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdcoa.DefaultCellStyle = DataGridViewCellStyle6
        Me.grdcoa.EnableHeadersVisualStyles = False
        Me.grdcoa.Location = New System.Drawing.Point(33, 79)
        Me.grdcoa.MultiSelect = False
        Me.grdcoa.Name = "grdcoa"
        Me.grdcoa.ReadOnly = True
        Me.grdcoa.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdcoa.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.grdcoa.RowHeadersWidth = 10
        Me.grdcoa.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdcoa.Size = New System.Drawing.Size(919, 381)
        Me.grdcoa.TabIndex = 32
        '
        'Column4
        '
        Me.Column4.HeaderText = "ID"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Visible = False
        '
        'Column1
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Format = "yyyy/MM/dd"
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column1.HeaderText = "Production Date"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 120
        '
        'Column2
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column2.HeaderText = "Ticket Series"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 400
        '
        'Column3
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.Format = "yyyy/MM/dd"
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle5
        Me.Column3.HeaderText = "Expiry Date"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 120
        '
        'Column6
        '
        Me.Column6.HeaderText = "Moisture"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "Protein"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "Ash"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "Wet Gluten"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'Column10
        '
        Me.Column10.HeaderText = "Water Absorption"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        '
        'Column11
        '
        Me.Column11.HeaderText = "Others"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        '
        'lblitemid
        '
        Me.lblitemid.AutoSize = True
        Me.lblitemid.Location = New System.Drawing.Point(99, 24)
        Me.lblitemid.Name = "lblitemid"
        Me.lblitemid.Size = New System.Drawing.Size(0, 13)
        Me.lblitemid.TabIndex = 26
        Me.lblitemid.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Quantity:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Item:"
        '
        'txtqty
        '
        Me.txtqty.BackColor = System.Drawing.Color.MistyRose
        Me.txtqty.Location = New System.Drawing.Point(120, 47)
        Me.txtqty.Name = "txtqty"
        Me.txtqty.ReadOnly = True
        Me.txtqty.Size = New System.Drawing.Size(219, 20)
        Me.txtqty.TabIndex = 13
        '
        'txtrefnum
        '
        Me.txtrefnum.BackColor = System.Drawing.Color.White
        Me.txtrefnum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtrefnum.Location = New System.Drawing.Point(483, 21)
        Me.txtrefnum.Name = "txtrefnum"
        Me.txtrefnum.ReadOnly = True
        Me.txtrefnum.Size = New System.Drawing.Size(216, 20)
        Me.txtrefnum.TabIndex = 11
        '
        'txtbatch
        '
        Me.txtbatch.BackColor = System.Drawing.Color.White
        Me.txtbatch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtbatch.Location = New System.Drawing.Point(483, 47)
        Me.txtbatch.Name = "txtbatch"
        Me.txtbatch.ReadOnly = True
        Me.txtbatch.Size = New System.Drawing.Size(216, 20)
        Me.txtbatch.TabIndex = 12
        '
        'coainfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 690)
        Me.Controls.Add(Me.gselect)
        Me.Controls.Add(Me.ginfo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "coainfo"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "COA Information"
        Me.ginfo.ResumeLayout(False)
        Me.ginfo.PerformLayout()
        Me.Panelinfo.ResumeLayout(False)
        Me.Panelinfo.PerformLayout()
        Me.gselect.ResumeLayout(False)
        Me.gselect.PerformLayout()
        CType(Me.grdcoa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ginfo As System.Windows.Forms.GroupBox
    Friend WithEvents lblcoa As System.Windows.Forms.Label
    Friend WithEvents Panelinfo As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtofnum As System.Windows.Forms.TextBox
    Friend WithEvents datedel As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtwrs As System.Windows.Forms.TextBox
    Friend WithEvents txttruck As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblofnum As System.Windows.Forms.Label
    Friend WithEvents dateload As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtpo As System.Windows.Forms.TextBox
    Friend WithEvents txtcus As System.Windows.Forms.TextBox
    Friend WithEvents txtcoanum As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents gselect As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtitem As System.Windows.Forms.TextBox
    Friend WithEvents grdcoa As System.Windows.Forms.DataGridView
    Friend WithEvents lblitemid As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtqty As System.Windows.Forms.TextBox
    Friend WithEvents txtrefnum As System.Windows.Forms.TextBox
    Friend WithEvents txtbatch As System.Windows.Forms.TextBox
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtrems As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtdriver As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtcoacus As System.Windows.Forms.TextBox
    Friend WithEvents lblid As System.Windows.Forms.Label
End Class
