<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class coanew
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(coanew))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.gselect = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtitem = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblitemid = New System.Windows.Forms.Label
        Me.btnchange = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtqty = New System.Windows.Forms.TextBox
        Me.txtseries = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.dateprod = New System.Windows.Forms.DateTimePicker
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtbatch = New System.Windows.Forms.TextBox
        Me.dateexpiry = New System.Windows.Forms.DateTimePicker
        Me.txtrefnum = New System.Windows.Forms.TextBox
        Me.txtwrs = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.lblofnum = New System.Windows.Forms.Label
        Me.txtofnum = New System.Windows.Forms.TextBox
        Me.ginfo = New System.Windows.Forms.GroupBox
        Me.lblcoa = New System.Windows.Forms.Label
        Me.Panelinfo = New System.Windows.Forms.Panel
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.datedel = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.dateload = New System.Windows.Forms.DateTimePicker
        Me.txttruck = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtpo = New System.Windows.Forms.TextBox
        Me.txtcus = New System.Windows.Forms.TextBox
        Me.btnsearch = New System.Windows.Forms.Button
        Me.txtcoanum = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.gparam = New System.Windows.Forms.GroupBox
        Me.txtwet = New System.Windows.Forms.TextBox
        Me.txtash = New System.Windows.Forms.TextBox
        Me.txtprot = New System.Windows.Forms.TextBox
        Me.txtmois = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.grheo = New System.Windows.Forms.GroupBox
        Me.txtwater = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.NewToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.COAToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.grems = New System.Windows.Forms.GroupBox
        Me.txtrems = New System.Windows.Forms.TextBox
        Me.btnprint = New System.Windows.Forms.Button
        Me.btnsave = New System.Windows.Forms.Button
        Me.btncancel = New System.Windows.Forms.Button
        Me.btnconfirm = New System.Windows.Forms.Button
        Me.gselect.SuspendLayout()
        Me.ginfo.SuspendLayout()
        Me.Panelinfo.SuspendLayout()
        Me.gparam.SuspendLayout()
        Me.grheo.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.grems.SuspendLayout()
        Me.SuspendLayout()
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Order Fill #:"
        '
        'gselect
        '
        Me.gselect.Controls.Add(Me.Label3)
        Me.gselect.Controls.Add(Me.txtitem)
        Me.gselect.Controls.Add(Me.Label4)
        Me.gselect.Controls.Add(Me.lblitemid)
        Me.gselect.Controls.Add(Me.btnchange)
        Me.gselect.Controls.Add(Me.Label5)
        Me.gselect.Controls.Add(Me.Label1)
        Me.gselect.Controls.Add(Me.txtqty)
        Me.gselect.Controls.Add(Me.txtseries)
        Me.gselect.Controls.Add(Me.Label12)
        Me.gselect.Controls.Add(Me.Label6)
        Me.gselect.Controls.Add(Me.dateprod)
        Me.gselect.Controls.Add(Me.Label9)
        Me.gselect.Controls.Add(Me.txtbatch)
        Me.gselect.Controls.Add(Me.dateexpiry)
        Me.gselect.Controls.Add(Me.txtrefnum)
        Me.gselect.Enabled = False
        Me.gselect.Location = New System.Drawing.Point(12, 289)
        Me.gselect.Name = "gselect"
        Me.gselect.Size = New System.Drawing.Size(420, 323)
        Me.gselect.TabIndex = 2
        Me.gselect.TabStop = False
        Me.gselect.Text = "Item Info"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 207)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Reference #:"
        '
        'txtitem
        '
        Me.txtitem.BackColor = System.Drawing.Color.MistyRose
        Me.txtitem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtitem.Location = New System.Drawing.Point(139, 21)
        Me.txtitem.Name = "txtitem"
        Me.txtitem.ReadOnly = True
        Me.txtitem.Size = New System.Drawing.Size(247, 20)
        Me.txtitem.TabIndex = 31
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(30, 233)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Batch No. / Lot No.:"
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
        'btnchange
        '
        Me.btnchange.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnchange.Image = CType(resources.GetObject("btnchange.Image"), System.Drawing.Image)
        Me.btnchange.Location = New System.Drawing.Point(358, 254)
        Me.btnchange.Name = "btnchange"
        Me.btnchange.Size = New System.Drawing.Size(28, 23)
        Me.btnchange.TabIndex = 26
        Me.btnchange.UseVisualStyleBackColor = True
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
        'txtqty
        '
        Me.txtqty.BackColor = System.Drawing.Color.MistyRose
        Me.txtqty.Location = New System.Drawing.Point(139, 47)
        Me.txtqty.Name = "txtqty"
        Me.txtqty.ReadOnly = True
        Me.txtqty.Size = New System.Drawing.Size(247, 20)
        Me.txtqty.TabIndex = 13
        '
        'txtseries
        '
        Me.txtseries.BackColor = System.Drawing.Color.MistyRose
        Me.txtseries.Location = New System.Drawing.Point(139, 73)
        Me.txtseries.Multiline = True
        Me.txtseries.Name = "txtseries"
        Me.txtseries.ReadOnly = True
        Me.txtseries.Size = New System.Drawing.Size(247, 125)
        Me.txtseries.TabIndex = 21
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(30, 76)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Ticket Series:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(30, 262)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Production Date:"
        '
        'dateprod
        '
        Me.dateprod.CalendarMonthBackground = System.Drawing.Color.MistyRose
        Me.dateprod.Enabled = False
        Me.dateprod.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateprod.Location = New System.Drawing.Point(139, 256)
        Me.dateprod.Name = "dateprod"
        Me.dateprod.Size = New System.Drawing.Size(213, 20)
        Me.dateprod.TabIndex = 25
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(30, 288)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 13)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Expiry Date:"
        '
        'txtbatch
        '
        Me.txtbatch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtbatch.Location = New System.Drawing.Point(139, 230)
        Me.txtbatch.Name = "txtbatch"
        Me.txtbatch.Size = New System.Drawing.Size(247, 20)
        Me.txtbatch.TabIndex = 12
        '
        'dateexpiry
        '
        Me.dateexpiry.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateexpiry.Location = New System.Drawing.Point(139, 282)
        Me.dateexpiry.Name = "dateexpiry"
        Me.dateexpiry.Size = New System.Drawing.Size(247, 20)
        Me.dateexpiry.TabIndex = 22
        '
        'txtrefnum
        '
        Me.txtrefnum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtrefnum.Location = New System.Drawing.Point(139, 204)
        Me.txtrefnum.Name = "txtrefnum"
        Me.txtrefnum.Size = New System.Drawing.Size(247, 20)
        Me.txtrefnum.TabIndex = 11
        '
        'txtwrs
        '
        Me.txtwrs.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtwrs.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtwrs.Location = New System.Drawing.Point(124, 31)
        Me.txtwrs.Name = "txtwrs"
        Me.txtwrs.ReadOnly = True
        Me.txtwrs.Size = New System.Drawing.Size(247, 20)
        Me.txtwrs.TabIndex = 30
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(15, 34)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(46, 13)
        Me.Label20.TabIndex = 28
        Me.Label20.Text = "WRS #:"
        '
        'lblofnum
        '
        Me.lblofnum.AutoSize = True
        Me.lblofnum.Location = New System.Drawing.Point(122, 8)
        Me.lblofnum.Name = "lblofnum"
        Me.lblofnum.Size = New System.Drawing.Size(24, 13)
        Me.lblofnum.TabIndex = 27
        Me.lblofnum.Text = "OF."
        '
        'txtofnum
        '
        Me.txtofnum.BackColor = System.Drawing.Color.LightCyan
        Me.txtofnum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtofnum.Location = New System.Drawing.Point(156, 5)
        Me.txtofnum.Name = "txtofnum"
        Me.txtofnum.ReadOnly = True
        Me.txtofnum.Size = New System.Drawing.Size(215, 20)
        Me.txtofnum.TabIndex = 25
        '
        'ginfo
        '
        Me.ginfo.Controls.Add(Me.lblcoa)
        Me.ginfo.Controls.Add(Me.Panelinfo)
        Me.ginfo.Controls.Add(Me.btnsearch)
        Me.ginfo.Controls.Add(Me.txtcoanum)
        Me.ginfo.Controls.Add(Me.Label17)
        Me.ginfo.Location = New System.Drawing.Point(12, 37)
        Me.ginfo.Name = "ginfo"
        Me.ginfo.Size = New System.Drawing.Size(420, 246)
        Me.ginfo.TabIndex = 3
        Me.ginfo.TabStop = False
        Me.ginfo.Text = "General Info"
        '
        'lblcoa
        '
        Me.lblcoa.AutoSize = True
        Me.lblcoa.Location = New System.Drawing.Point(136, 28)
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
        Me.Panelinfo.Controls.Add(Me.txtwrs)
        Me.Panelinfo.Controls.Add(Me.Label20)
        Me.Panelinfo.Controls.Add(Me.lblofnum)
        Me.Panelinfo.Controls.Add(Me.Label8)
        Me.Panelinfo.Controls.Add(Me.txtofnum)
        Me.Panelinfo.Controls.Add(Me.Label13)
        Me.Panelinfo.Controls.Add(Me.Label2)
        Me.Panelinfo.Controls.Add(Me.Label11)
        Me.Panelinfo.Controls.Add(Me.datedel)
        Me.Panelinfo.Controls.Add(Me.Label7)
        Me.Panelinfo.Controls.Add(Me.dateload)
        Me.Panelinfo.Controls.Add(Me.txttruck)
        Me.Panelinfo.Controls.Add(Me.Label10)
        Me.Panelinfo.Controls.Add(Me.txtpo)
        Me.Panelinfo.Controls.Add(Me.txtcus)
        Me.Panelinfo.Enabled = False
        Me.Panelinfo.Location = New System.Drawing.Point(15, 47)
        Me.Panelinfo.Name = "Panelinfo"
        Me.Panelinfo.Size = New System.Drawing.Size(385, 193)
        Me.Panelinfo.TabIndex = 30
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 60)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Customer:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(15, 86)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(48, 13)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "PO. No.:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(15, 112)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(58, 13)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Truck No.:"
        '
        'datedel
        '
        Me.datedel.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.datedel.Location = New System.Drawing.Point(124, 161)
        Me.datedel.Name = "datedel"
        Me.datedel.Size = New System.Drawing.Size(247, 20)
        Me.datedel.TabIndex = 24
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 167)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Delivery Date:"
        '
        'dateload
        '
        Me.dateload.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateload.Location = New System.Drawing.Point(124, 135)
        Me.dateload.Name = "dateload"
        Me.dateload.Size = New System.Drawing.Size(247, 20)
        Me.dateload.TabIndex = 23
        '
        'txttruck
        '
        Me.txttruck.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txttruck.Location = New System.Drawing.Point(124, 109)
        Me.txttruck.Name = "txttruck"
        Me.txttruck.ReadOnly = True
        Me.txttruck.Size = New System.Drawing.Size(247, 20)
        Me.txttruck.TabIndex = 20
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 141)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 13)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Loading Date:"
        '
        'txtpo
        '
        Me.txtpo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtpo.Location = New System.Drawing.Point(124, 83)
        Me.txtpo.Name = "txtpo"
        Me.txtpo.ReadOnly = True
        Me.txtpo.Size = New System.Drawing.Size(247, 20)
        Me.txtpo.TabIndex = 19
        '
        'txtcus
        '
        Me.txtcus.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtcus.Location = New System.Drawing.Point(124, 57)
        Me.txtcus.Name = "txtcus"
        Me.txtcus.ReadOnly = True
        Me.txtcus.Size = New System.Drawing.Size(247, 20)
        Me.txtcus.TabIndex = 18
        '
        'btnsearch
        '
        Me.btnsearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsearch.Image = CType(resources.GetObject("btnsearch.Image"), System.Drawing.Image)
        Me.btnsearch.Location = New System.Drawing.Point(357, 23)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(30, 23)
        Me.btnsearch.TabIndex = 29
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'txtcoanum
        '
        Me.txtcoanum.BackColor = System.Drawing.SystemColors.Control
        Me.txtcoanum.Location = New System.Drawing.Point(171, 25)
        Me.txtcoanum.Name = "txtcoanum"
        Me.txtcoanum.ReadOnly = True
        Me.txtcoanum.Size = New System.Drawing.Size(181, 20)
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
        'gparam
        '
        Me.gparam.Controls.Add(Me.txtwet)
        Me.gparam.Controls.Add(Me.txtash)
        Me.gparam.Controls.Add(Me.txtprot)
        Me.gparam.Controls.Add(Me.txtmois)
        Me.gparam.Controls.Add(Me.Label18)
        Me.gparam.Controls.Add(Me.Label19)
        Me.gparam.Controls.Add(Me.Label14)
        Me.gparam.Controls.Add(Me.Label15)
        Me.gparam.Enabled = False
        Me.gparam.Location = New System.Drawing.Point(441, 37)
        Me.gparam.Name = "gparam"
        Me.gparam.Size = New System.Drawing.Size(420, 152)
        Me.gparam.TabIndex = 4
        Me.gparam.TabStop = False
        Me.gparam.Text = "Parameters"
        '
        'txtwet
        '
        Me.txtwet.Location = New System.Drawing.Point(137, 109)
        Me.txtwet.Name = "txtwet"
        Me.txtwet.Size = New System.Drawing.Size(247, 20)
        Me.txtwet.TabIndex = 29
        Me.txtwet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtash
        '
        Me.txtash.Location = New System.Drawing.Point(137, 83)
        Me.txtash.Name = "txtash"
        Me.txtash.Size = New System.Drawing.Size(247, 20)
        Me.txtash.TabIndex = 28
        Me.txtash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtprot
        '
        Me.txtprot.Location = New System.Drawing.Point(137, 57)
        Me.txtprot.Name = "txtprot"
        Me.txtprot.Size = New System.Drawing.Size(247, 20)
        Me.txtprot.TabIndex = 27
        Me.txtprot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtmois
        '
        Me.txtmois.Location = New System.Drawing.Point(137, 31)
        Me.txtmois.Name = "txtmois"
        Me.txtmois.Size = New System.Drawing.Size(247, 20)
        Me.txtmois.TabIndex = 26
        Me.txtmois.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(25, 86)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(72, 13)
        Me.Label18.TabIndex = 2
        Me.Label18.Text = "Ash, % (as is):"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(25, 112)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(97, 13)
        Me.Label19.TabIndex = 3
        Me.Label19.Text = "Wet Gluten, (as is):"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(25, 34)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(64, 13)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Moisture, %:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(25, 60)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(57, 13)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "Protein, %:"
        '
        'grheo
        '
        Me.grheo.Controls.Add(Me.txtwater)
        Me.grheo.Controls.Add(Me.Label16)
        Me.grheo.Enabled = False
        Me.grheo.Location = New System.Drawing.Point(441, 196)
        Me.grheo.Name = "grheo"
        Me.grheo.Size = New System.Drawing.Size(420, 70)
        Me.grheo.TabIndex = 5
        Me.grheo.TabStop = False
        Me.grheo.Text = "Rheological"
        '
        'txtwater
        '
        Me.txtwater.Location = New System.Drawing.Point(137, 36)
        Me.txtwater.Name = "txtwater"
        Me.txtwater.Size = New System.Drawing.Size(247, 20)
        Me.txtwater.TabIndex = 30
        Me.txtwater.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(25, 39)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(106, 13)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "Water Absorption, %:"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripButton1, Me.COAToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(874, 25)
        Me.ToolStrip1.TabIndex = 11
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'NewToolStripButton1
        '
        Me.NewToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NewToolStripButton1.Image = CType(resources.GetObject("NewToolStripButton1.Image"), System.Drawing.Image)
        Me.NewToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewToolStripButton1.Name = "NewToolStripButton1"
        Me.NewToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.NewToolStripButton1.Text = "New COA"
        '
        'COAToolStripButton1
        '
        Me.COAToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.COAToolStripButton1.Image = CType(resources.GetObject("COAToolStripButton1.Image"), System.Drawing.Image)
        Me.COAToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.COAToolStripButton1.Name = "COAToolStripButton1"
        Me.COAToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.COAToolStripButton1.Text = "Pending COA"
        '
        'grems
        '
        Me.grems.Controls.Add(Me.txtrems)
        Me.grems.Enabled = False
        Me.grems.Location = New System.Drawing.Point(441, 272)
        Me.grems.Name = "grems"
        Me.grems.Size = New System.Drawing.Size(420, 175)
        Me.grems.TabIndex = 31
        Me.grems.TabStop = False
        Me.grems.Text = "Remarks"
        '
        'txtrems
        '
        Me.txtrems.Location = New System.Drawing.Point(28, 25)
        Me.txtrems.Multiline = True
        Me.txtrems.Name = "txtrems"
        Me.txtrems.Size = New System.Drawing.Size(356, 134)
        Me.txtrems.TabIndex = 30
        '
        'btnprint
        '
        Me.btnprint.Image = CType(resources.GetObject("btnprint.Image"), System.Drawing.Image)
        Me.btnprint.Location = New System.Drawing.Point(677, 570)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(92, 27)
        Me.btnprint.TabIndex = 9
        Me.btnprint.Text = "Print COA"
        Me.btnprint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnprint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'btnsave
        '
        Me.btnsave.Image = CType(resources.GetObject("btnsave.Image"), System.Drawing.Image)
        Me.btnsave.Location = New System.Drawing.Point(462, 570)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(117, 27)
        Me.btnsave.TabIndex = 8
        Me.btnsave.Text = "Save as Draft"
        Me.btnsave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnsave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsave.UseVisualStyleBackColor = True
        '
        'btncancel
        '
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(775, 570)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(86, 27)
        Me.btncancel.TabIndex = 7
        Me.btncancel.Text = "Close"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'btnconfirm
        '
        Me.btnconfirm.Image = CType(resources.GetObject("btnconfirm.Image"), System.Drawing.Image)
        Me.btnconfirm.Location = New System.Drawing.Point(585, 570)
        Me.btnconfirm.Name = "btnconfirm"
        Me.btnconfirm.Size = New System.Drawing.Size(86, 27)
        Me.btnconfirm.TabIndex = 6
        Me.btnconfirm.Text = "Confirm"
        Me.btnconfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnconfirm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnconfirm.UseVisualStyleBackColor = True
        '
        'coanew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(874, 620)
        Me.Controls.Add(Me.grems)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.btnsave)
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.btnconfirm)
        Me.Controls.Add(Me.grheo)
        Me.Controls.Add(Me.gparam)
        Me.Controls.Add(Me.ginfo)
        Me.Controls.Add(Me.gselect)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "coanew"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Certificate of Analysis"
        Me.gselect.ResumeLayout(False)
        Me.gselect.PerformLayout()
        Me.ginfo.ResumeLayout(False)
        Me.ginfo.PerformLayout()
        Me.Panelinfo.ResumeLayout(False)
        Me.Panelinfo.PerformLayout()
        Me.gparam.ResumeLayout(False)
        Me.gparam.PerformLayout()
        Me.grheo.ResumeLayout(False)
        Me.grheo.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.grems.ResumeLayout(False)
        Me.grems.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents gselect As System.Windows.Forms.GroupBox
    Friend WithEvents ginfo As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents gparam As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents grheo As System.Windows.Forms.GroupBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtqty As System.Windows.Forms.TextBox
    Friend WithEvents txtbatch As System.Windows.Forms.TextBox
    Friend WithEvents txtrefnum As System.Windows.Forms.TextBox
    Friend WithEvents txtofnum As System.Windows.Forms.TextBox
    Friend WithEvents txtseries As System.Windows.Forms.TextBox
    Friend WithEvents txttruck As System.Windows.Forms.TextBox
    Friend WithEvents txtpo As System.Windows.Forms.TextBox
    Friend WithEvents txtcus As System.Windows.Forms.TextBox
    Friend WithEvents txtwet As System.Windows.Forms.TextBox
    Friend WithEvents txtash As System.Windows.Forms.TextBox
    Friend WithEvents txtprot As System.Windows.Forms.TextBox
    Friend WithEvents txtmois As System.Windows.Forms.TextBox
    Friend WithEvents txtwater As System.Windows.Forms.TextBox
    Friend WithEvents datedel As System.Windows.Forms.DateTimePicker
    Friend WithEvents dateload As System.Windows.Forms.DateTimePicker
    Friend WithEvents dateexpiry As System.Windows.Forms.DateTimePicker
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents btnconfirm As System.Windows.Forms.Button
    Friend WithEvents dateprod As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnchange As System.Windows.Forms.Button
    Friend WithEvents lblitemid As System.Windows.Forms.Label
    Friend WithEvents lblofnum As System.Windows.Forms.Label
    Friend WithEvents txtcoanum As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnsave As System.Windows.Forms.Button
    Friend WithEvents btnprint As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents COAToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtwrs As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents grems As System.Windows.Forms.GroupBox
    Friend WithEvents txtrems As System.Windows.Forms.TextBox
    Friend WithEvents NewToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents Panelinfo As System.Windows.Forms.Panel
    Friend WithEvents lblcoa As System.Windows.Forms.Label
    Friend WithEvents txtitem As System.Windows.Forms.TextBox
End Class
