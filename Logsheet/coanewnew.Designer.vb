<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class coanewnew
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(coanewnew))
        Me.ginfo = New System.Windows.Forms.GroupBox
        Me.txtcoanum = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.dateprod = New System.Windows.Forms.DateTimePicker
        Me.datedel = New System.Windows.Forms.DateTimePicker
        Me.dateexpiry = New System.Windows.Forms.DateTimePicker
        Me.txtseries = New System.Windows.Forms.TextBox
        Me.dateload = New System.Windows.Forms.DateTimePicker
        Me.txttruck = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtpo = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtcus = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtqty = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtbatch = New System.Windows.Forms.TextBox
        Me.txtrefnum = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtrems = New System.Windows.Forms.TextBox
        Me.NewToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.COAToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.OFToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.btnprint = New System.Windows.Forms.Button
        Me.btnsave = New System.Windows.Forms.Button
        Me.btncancel = New System.Windows.Forms.Button
        Me.btnok = New System.Windows.Forms.Button
        Me.btncoasearch = New System.Windows.Forms.Button
        Me.btnchange = New System.Windows.Forms.Button
        Me.ginfo.SuspendLayout()
        Me.gparam.SuspendLayout()
        Me.grheo.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ginfo
        '
        Me.ginfo.Controls.Add(Me.btncoasearch)
        Me.ginfo.Controls.Add(Me.txtcoanum)
        Me.ginfo.Controls.Add(Me.Label17)
        Me.ginfo.Controls.Add(Me.btnchange)
        Me.ginfo.Controls.Add(Me.dateprod)
        Me.ginfo.Controls.Add(Me.datedel)
        Me.ginfo.Controls.Add(Me.dateexpiry)
        Me.ginfo.Controls.Add(Me.txtseries)
        Me.ginfo.Controls.Add(Me.dateload)
        Me.ginfo.Controls.Add(Me.txttruck)
        Me.ginfo.Controls.Add(Me.Label12)
        Me.ginfo.Controls.Add(Me.txtpo)
        Me.ginfo.Controls.Add(Me.Label6)
        Me.ginfo.Controls.Add(Me.txtcus)
        Me.ginfo.Controls.Add(Me.Label9)
        Me.ginfo.Controls.Add(Me.Label10)
        Me.ginfo.Controls.Add(Me.txtqty)
        Me.ginfo.Controls.Add(Me.Label7)
        Me.ginfo.Controls.Add(Me.txtbatch)
        Me.ginfo.Controls.Add(Me.txtrefnum)
        Me.ginfo.Controls.Add(Me.Label11)
        Me.ginfo.Controls.Add(Me.Label13)
        Me.ginfo.Controls.Add(Me.Label8)
        Me.ginfo.Controls.Add(Me.Label5)
        Me.ginfo.Controls.Add(Me.Label3)
        Me.ginfo.Controls.Add(Me.Label4)
        Me.ginfo.Enabled = False
        Me.ginfo.Location = New System.Drawing.Point(12, 37)
        Me.ginfo.Name = "ginfo"
        Me.ginfo.Size = New System.Drawing.Size(420, 539)
        Me.ginfo.TabIndex = 3
        Me.ginfo.TabStop = False
        Me.ginfo.Text = "General Info"
        '
        'txtcoanum
        '
        Me.txtcoanum.BackColor = System.Drawing.Color.LightCyan
        Me.txtcoanum.Location = New System.Drawing.Point(135, 25)
        Me.txtcoanum.Name = "txtcoanum"
        Me.txtcoanum.ReadOnly = True
        Me.txtcoanum.Size = New System.Drawing.Size(213, 20)
        Me.txtcoanum.TabIndex = 28
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(25, 28)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(42, 13)
        Me.Label17.TabIndex = 27
        Me.Label17.Text = "COA #:"
        '
        'dateprod
        '
        Me.dateprod.CalendarMonthBackground = System.Drawing.Color.MistyRose
        Me.dateprod.Enabled = False
        Me.dateprod.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateprod.Location = New System.Drawing.Point(135, 103)
        Me.dateprod.Name = "dateprod"
        Me.dateprod.Size = New System.Drawing.Size(213, 20)
        Me.dateprod.TabIndex = 25
        '
        'datedel
        '
        Me.datedel.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.datedel.Location = New System.Drawing.Point(135, 501)
        Me.datedel.Name = "datedel"
        Me.datedel.Size = New System.Drawing.Size(247, 20)
        Me.datedel.TabIndex = 24
        '
        'dateexpiry
        '
        Me.dateexpiry.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateexpiry.Location = New System.Drawing.Point(135, 129)
        Me.dateexpiry.Name = "dateexpiry"
        Me.dateexpiry.Size = New System.Drawing.Size(247, 20)
        Me.dateexpiry.TabIndex = 22
        '
        'txtseries
        '
        Me.txtseries.BackColor = System.Drawing.Color.MistyRose
        Me.txtseries.Location = New System.Drawing.Point(135, 181)
        Me.txtseries.Multiline = True
        Me.txtseries.Name = "txtseries"
        Me.txtseries.ReadOnly = True
        Me.txtseries.Size = New System.Drawing.Size(247, 210)
        Me.txtseries.TabIndex = 21
        '
        'dateload
        '
        Me.dateload.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateload.Location = New System.Drawing.Point(135, 475)
        Me.dateload.Name = "dateload"
        Me.dateload.Size = New System.Drawing.Size(247, 20)
        Me.dateload.TabIndex = 23
        '
        'txttruck
        '
        Me.txttruck.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txttruck.Location = New System.Drawing.Point(135, 449)
        Me.txttruck.Name = "txttruck"
        Me.txttruck.ReadOnly = True
        Me.txttruck.Size = New System.Drawing.Size(247, 20)
        Me.txttruck.TabIndex = 20
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(25, 184)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Ticket Series:"
        '
        'txtpo
        '
        Me.txtpo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtpo.Location = New System.Drawing.Point(135, 423)
        Me.txtpo.Name = "txtpo"
        Me.txtpo.ReadOnly = True
        Me.txtpo.Size = New System.Drawing.Size(247, 20)
        Me.txtpo.TabIndex = 19
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(25, 106)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Production Date:"
        '
        'txtcus
        '
        Me.txtcus.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtcus.Location = New System.Drawing.Point(135, 397)
        Me.txtcus.Name = "txtcus"
        Me.txtcus.ReadOnly = True
        Me.txtcus.Size = New System.Drawing.Size(247, 20)
        Me.txtcus.TabIndex = 18
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(25, 132)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 13)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Expiry Date:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(25, 478)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 13)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Loading Date:"
        '
        'txtqty
        '
        Me.txtqty.BackColor = System.Drawing.Color.MistyRose
        Me.txtqty.Location = New System.Drawing.Point(135, 155)
        Me.txtqty.Name = "txtqty"
        Me.txtqty.ReadOnly = True
        Me.txtqty.Size = New System.Drawing.Size(247, 20)
        Me.txtqty.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(25, 508)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Delivery Date:"
        '
        'txtbatch
        '
        Me.txtbatch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtbatch.Location = New System.Drawing.Point(135, 77)
        Me.txtbatch.Name = "txtbatch"
        Me.txtbatch.Size = New System.Drawing.Size(247, 20)
        Me.txtbatch.TabIndex = 12
        '
        'txtrefnum
        '
        Me.txtrefnum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtrefnum.Location = New System.Drawing.Point(135, 51)
        Me.txtrefnum.Name = "txtrefnum"
        Me.txtrefnum.Size = New System.Drawing.Size(247, 20)
        Me.txtrefnum.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(25, 452)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(58, 13)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Truck No.:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(25, 426)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(48, 13)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "PO. No.:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(25, 400)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Customer:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(25, 158)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Quantity:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Reference #:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Batch No. / Lot No.:"
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
        Me.gparam.Location = New System.Drawing.Point(441, 166)
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
        Me.grheo.Location = New System.Drawing.Point(441, 324)
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
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripButton1, Me.COAToolStripButton1, Me.OFToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(873, 25)
        Me.ToolStrip1.TabIndex = 11
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtrems)
        Me.GroupBox1.Enabled = False
        Me.GroupBox1.Location = New System.Drawing.Point(441, 400)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(420, 99)
        Me.GroupBox1.TabIndex = 31
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Remarks"
        '
        'txtrems
        '
        Me.txtrems.Location = New System.Drawing.Point(28, 25)
        Me.txtrems.Multiline = True
        Me.txtrems.Name = "txtrems"
        Me.txtrems.Size = New System.Drawing.Size(356, 57)
        Me.txtrems.TabIndex = 30
        Me.txtrems.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        'OFToolStripButton1
        '
        Me.OFToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OFToolStripButton1.Image = CType(resources.GetObject("OFToolStripButton1.Image"), System.Drawing.Image)
        Me.OFToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OFToolStripButton1.Name = "OFToolStripButton1"
        Me.OFToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.OFToolStripButton1.Text = "Select Order Fill"
        Me.OFToolStripButton1.Visible = False
        '
        'btnprint
        '
        Me.btnprint.Image = CType(resources.GetObject("btnprint.Image"), System.Drawing.Image)
        Me.btnprint.Location = New System.Drawing.Point(677, 538)
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
        Me.btnsave.Location = New System.Drawing.Point(462, 538)
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
        Me.btncancel.Location = New System.Drawing.Point(775, 538)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(86, 27)
        Me.btncancel.TabIndex = 7
        Me.btncancel.Text = "Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'btnok
        '
        Me.btnok.Image = CType(resources.GetObject("btnok.Image"), System.Drawing.Image)
        Me.btnok.Location = New System.Drawing.Point(585, 538)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(86, 27)
        Me.btnok.TabIndex = 6
        Me.btnok.Text = "Confirm"
        Me.btnok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnok.UseVisualStyleBackColor = True
        '
        'btncoasearch
        '
        Me.btncoasearch.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncoasearch.Image = CType(resources.GetObject("btncoasearch.Image"), System.Drawing.Image)
        Me.btncoasearch.Location = New System.Drawing.Point(354, 23)
        Me.btncoasearch.Name = "btncoasearch"
        Me.btncoasearch.Size = New System.Drawing.Size(30, 23)
        Me.btncoasearch.TabIndex = 31
        Me.btncoasearch.UseVisualStyleBackColor = True
        '
        'btnchange
        '
        Me.btnchange.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnchange.Image = CType(resources.GetObject("btnchange.Image"), System.Drawing.Image)
        Me.btnchange.Location = New System.Drawing.Point(354, 102)
        Me.btnchange.Name = "btnchange"
        Me.btnchange.Size = New System.Drawing.Size(28, 23)
        Me.btnchange.TabIndex = 26
        Me.btnchange.UseVisualStyleBackColor = True
        '
        'coanewnew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(873, 583)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.btnsave)
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.grheo)
        Me.Controls.Add(Me.gparam)
        Me.Controls.Add(Me.ginfo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "coanewnew"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Certificate of Analysis"
        Me.ginfo.ResumeLayout(False)
        Me.ginfo.PerformLayout()
        Me.gparam.ResumeLayout(False)
        Me.gparam.PerformLayout()
        Me.grheo.ResumeLayout(False)
        Me.grheo.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
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
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents dateprod As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnchange As System.Windows.Forms.Button
    Friend WithEvents txtcoanum As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnsave As System.Windows.Forms.Button
    Friend WithEvents btnprint As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents COAToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtrems As System.Windows.Forms.TextBox
    Friend WithEvents OFToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents NewToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents btncoasearch As System.Windows.Forms.Button
End Class
