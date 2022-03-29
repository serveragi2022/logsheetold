<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tickethourly
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(tickethourly))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.txttimer = New System.Windows.Forms.TextBox
        Me.txtg = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtgross = New System.Windows.Forms.TextBox
        Me.txtremarks = New System.Windows.Forms.TextBox
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnselect = New System.Windows.Forms.ToolStripButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtlogsheet = New System.Windows.Forms.TextBox
        Me.txtitem = New System.Windows.Forms.TextBox
        Me.btnadd = New System.Windows.Forms.Button
        Me.btnreset = New System.Windows.Forms.Button
        Me.lbllogitemid = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtwhse = New System.Windows.Forms.TextBox
        Me.labels = New System.Windows.Forms.Label
        Me.txtline = New System.Windows.Forms.TextBox
        Me.grdhour = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lblhour = New System.Windows.Forms.Label
        Me.lblfrom = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblto = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblnum = New System.Windows.Forms.Label
        Me.lblserverdt = New System.Windows.Forms.TextBox
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Button1 = New System.Windows.Forms.Button
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grdhour, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txttimer
        '
        Me.txttimer.BackColor = System.Drawing.SystemColors.Window
        Me.txttimer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttimer.Location = New System.Drawing.Point(104, 348)
        Me.txttimer.Name = "txttimer"
        Me.txttimer.Size = New System.Drawing.Size(50, 21)
        Me.txttimer.TabIndex = 124
        Me.txttimer.Visible = False
        '
        'txtg
        '
        Me.txtg.BackColor = System.Drawing.SystemColors.Window
        Me.txtg.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtg.Location = New System.Drawing.Point(160, 348)
        Me.txtg.Name = "txtg"
        Me.txtg.Size = New System.Drawing.Size(50, 21)
        Me.txtg.TabIndex = 123
        Me.txtg.Visible = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(13, 354)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(85, 15)
        Me.Label22.TabIndex = 121
        Me.Label22.Text = "Gross Weight:"
        '
        'txtgross
        '
        Me.txtgross.BackColor = System.Drawing.SystemColors.Window
        Me.txtgross.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtgross.Location = New System.Drawing.Point(25, 372)
        Me.txtgross.Name = "txtgross"
        Me.txtgross.Size = New System.Drawing.Size(80, 21)
        Me.txtgross.TabIndex = 120
        '
        'txtremarks
        '
        Me.txtremarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtremarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.Location = New System.Drawing.Point(144, 373)
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(128, 21)
        Me.txtremarks.TabIndex = 126
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnselect})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1224, 25)
        Me.ToolStrip1.TabIndex = 130
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnselect
        '
        Me.btnselect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnselect.Image = CType(resources.GetObject("btnselect.Image"), System.Drawing.Image)
        Me.btnselect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnselect.Name = "btnselect"
        Me.btnselect.Size = New System.Drawing.Size(23, 22)
        Me.btnselect.Text = "Select Pending"
        Me.btnselect.ToolTipText = "Select"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 15)
        Me.Label2.TabIndex = 132
        Me.Label2.Text = "Logsheet#:"
        '
        'txtlogsheet
        '
        Me.txtlogsheet.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtlogsheet.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlogsheet.Location = New System.Drawing.Point(25, 43)
        Me.txtlogsheet.Name = "txtlogsheet"
        Me.txtlogsheet.ReadOnly = True
        Me.txtlogsheet.Size = New System.Drawing.Size(247, 21)
        Me.txtlogsheet.TabIndex = 131
        '
        'txtitem
        '
        Me.txtitem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtitem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtitem.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtitem.Location = New System.Drawing.Point(286, 42)
        Me.txtitem.Name = "txtitem"
        Me.txtitem.ReadOnly = True
        Me.txtitem.Size = New System.Drawing.Size(926, 21)
        Me.txtitem.TabIndex = 133
        Me.txtitem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnadd
        '
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.Image = CType(resources.GetObject("btnadd.Image"), System.Drawing.Image)
        Me.btnadd.Location = New System.Drawing.Point(25, 401)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(247, 29)
        Me.btnadd.TabIndex = 125
        Me.btnadd.Text = "Add"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'btnreset
        '
        Me.btnreset.BackColor = System.Drawing.Color.Transparent
        Me.btnreset.Image = CType(resources.GetObject("btnreset.Image"), System.Drawing.Image)
        Me.btnreset.Location = New System.Drawing.Point(111, 372)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(27, 23)
        Me.btnreset.TabIndex = 122
        Me.btnreset.UseVisualStyleBackColor = False
        '
        'lbllogitemid
        '
        Me.lbllogitemid.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllogitemid.Location = New System.Drawing.Point(1158, 42)
        Me.lbllogitemid.Name = "lbllogitemid"
        Me.lbllogitemid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lbllogitemid.Size = New System.Drawing.Size(54, 18)
        Me.lbllogitemid.TabIndex = 135
        Me.lbllogitemid.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 15)
        Me.Label4.TabIndex = 137
        Me.Label4.Text = "Warehouse:"
        '
        'txtwhse
        '
        Me.txtwhse.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtwhse.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtwhse.Location = New System.Drawing.Point(25, 96)
        Me.txtwhse.Name = "txtwhse"
        Me.txtwhse.ReadOnly = True
        Me.txtwhse.Size = New System.Drawing.Size(247, 21)
        Me.txtwhse.TabIndex = 136
        '
        'labels
        '
        Me.labels.AutoSize = True
        Me.labels.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labels.Location = New System.Drawing.Point(13, 132)
        Me.labels.Name = "labels"
        Me.labels.Size = New System.Drawing.Size(34, 15)
        Me.labels.TabIndex = 139
        Me.labels.Text = "Line:"
        '
        'txtline
        '
        Me.txtline.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtline.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtline.Location = New System.Drawing.Point(25, 150)
        Me.txtline.Name = "txtline"
        Me.txtline.ReadOnly = True
        Me.txtline.Size = New System.Drawing.Size(247, 21)
        Me.txtline.TabIndex = 138
        '
        'grdhour
        '
        Me.grdhour.AllowUserToAddRows = False
        Me.grdhour.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grdhour.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdhour.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdhour.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.grdhour.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        Me.grdhour.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.OldLace
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.NullValue = Nothing
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdhour.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdhour.ColumnHeadersHeight = 25
        Me.grdhour.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grdhour.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column7, Me.Column8, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdhour.DefaultCellStyle = DataGridViewCellStyle3
        Me.grdhour.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.grdhour.EnableHeadersVisualStyles = False
        Me.grdhour.GridColor = System.Drawing.Color.Salmon
        Me.grdhour.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.grdhour.Location = New System.Drawing.Point(286, 68)
        Me.grdhour.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.grdhour.Name = "grdhour"
        Me.grdhour.ReadOnly = True
        Me.grdhour.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdhour.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdhour.RowHeadersWidth = 15
        Me.grdhour.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.NullValue = Nothing
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdhour.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.grdhour.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdhour.Size = New System.Drawing.Size(926, 474)
        Me.grdhour.TabIndex = 141
        '
        'Column1
        '
        Me.Column1.HeaderText = "Hour"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "From"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 150
        '
        'Column8
        '
        Me.Column8.HeaderText = "To"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 150
        '
        'Column2
        '
        Me.Column2.HeaderText = "Sample 1"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Sample 2"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Sample 3"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Sample 4"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "Sample 5"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'lblhour
        '
        Me.lblhour.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblhour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblhour.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblhour.Location = New System.Drawing.Point(25, 227)
        Me.lblhour.Name = "lblhour"
        Me.lblhour.Size = New System.Drawing.Size(247, 36)
        Me.lblhour.TabIndex = 143
        Me.lblhour.Text = "1ST HOUR"
        Me.lblhour.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblfrom
        '
        Me.lblfrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblfrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblfrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfrom.Location = New System.Drawing.Point(80, 266)
        Me.lblfrom.Name = "lblfrom"
        Me.lblfrom.Size = New System.Drawing.Size(192, 23)
        Me.lblfrom.TabIndex = 145
        Me.lblfrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(25, 266)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 23)
        Me.Label7.TabIndex = 144
        Me.Label7.Text = "FROM:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblto
        '
        Me.lblto.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblto.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblto.Location = New System.Drawing.Point(80, 292)
        Me.lblto.Name = "lblto"
        Me.lblto.Size = New System.Drawing.Size(192, 23)
        Me.lblto.TabIndex = 147
        Me.lblto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(25, 292)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 23)
        Me.Label9.TabIndex = 146
        Me.Label9.Text = "TO:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'lblnum
        '
        Me.lblnum.AutoSize = True
        Me.lblnum.Location = New System.Drawing.Point(27, 248)
        Me.lblnum.Name = "lblnum"
        Me.lblnum.Size = New System.Drawing.Size(13, 13)
        Me.lblnum.TabIndex = 149
        Me.lblnum.Text = "1"
        Me.lblnum.Visible = False
        '
        'lblserverdt
        '
        Me.lblserverdt.Location = New System.Drawing.Point(25, 204)
        Me.lblserverdt.Name = "lblserverdt"
        Me.lblserverdt.Size = New System.Drawing.Size(247, 20)
        Me.lblserverdt.TabIndex = 150
        '
        'Timer2
        '
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(197, 318)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 151
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Timer3
        '
        Me.Timer3.Interval = 1000
        '
        'tickethourly
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1224, 561)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lblserverdt)
        Me.Controls.Add(Me.lblnum)
        Me.Controls.Add(Me.lblto)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblfrom)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lblhour)
        Me.Controls.Add(Me.grdhour)
        Me.Controls.Add(Me.labels)
        Me.Controls.Add(Me.txtline)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtwhse)
        Me.Controls.Add(Me.lbllogitemid)
        Me.Controls.Add(Me.txtitem)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtlogsheet)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.txtremarks)
        Me.Controls.Add(Me.btnadd)
        Me.Controls.Add(Me.txttimer)
        Me.Controls.Add(Me.txtg)
        Me.Controls.Add(Me.btnreset)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.txtgross)
        Me.Name = "tickethourly"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "QC Hourly Monitoring"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grdhour, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txttimer As System.Windows.Forms.TextBox
    Friend WithEvents txtg As System.Windows.Forms.TextBox
    Friend WithEvents btnreset As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtgross As System.Windows.Forms.TextBox
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents txtremarks As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnselect As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtlogsheet As System.Windows.Forms.TextBox
    Friend WithEvents txtitem As System.Windows.Forms.TextBox
    Friend WithEvents lbllogitemid As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtwhse As System.Windows.Forms.TextBox
    Friend WithEvents labels As System.Windows.Forms.Label
    Friend WithEvents txtline As System.Windows.Forms.TextBox
    Friend WithEvents grdhour As System.Windows.Forms.DataGridView
    Friend WithEvents lblhour As System.Windows.Forms.Label
    Friend WithEvents lblfrom As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblto As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblnum As System.Windows.Forms.Label
    Friend WithEvents lblserverdt As System.Windows.Forms.TextBox
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
