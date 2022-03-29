<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tsetrange
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(tsetrange))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnclose = New System.Windows.Forms.Button()
        Me.txtletter2 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.numto = New System.Windows.Forms.NumericUpDown()
        Me.numfrom = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtletter1 = New System.Windows.Forms.TextBox()
        Me.btndeactivate = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btncancel = New System.Windows.Forms.Button()
        Me.grdrange = New System.Windows.Forms.DataGridView()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnadd = New System.Windows.Forms.Button()
        Me.lblline = New System.Windows.Forms.Label()
        Me.lbllognum = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblid = New System.Windows.Forms.Label()
        Me.lbltype = New System.Windows.Forms.Label()
        CType(Me.numto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numfrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdrange, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnclose.Image = CType(resources.GetObject("btnclose.Image"), System.Drawing.Image)
        Me.btnclose.Location = New System.Drawing.Point(551, 426)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(103, 31)
        Me.btnclose.TabIndex = 13
        Me.btnclose.Text = "Close"
        Me.btnclose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = True
        '
        'txtletter2
        '
        Me.txtletter2.BackColor = System.Drawing.Color.White
        Me.txtletter2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtletter2.Location = New System.Drawing.Point(356, 102)
        Me.txtletter2.Name = "txtletter2"
        Me.txtletter2.ReadOnly = True
        Me.txtletter2.Size = New System.Drawing.Size(34, 21)
        Me.txtletter2.TabIndex = 48
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 141)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 15)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "Letter:"
        Me.Label7.Visible = False
        '
        'numto
        '
        Me.numto.Location = New System.Drawing.Point(398, 102)
        Me.numto.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.numto.Name = "numto"
        Me.numto.Size = New System.Drawing.Size(145, 21)
        Me.numto.TabIndex = 46
        '
        'numfrom
        '
        Me.numfrom.Location = New System.Drawing.Point(147, 102)
        Me.numfrom.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.numfrom.Name = "numfrom"
        Me.numfrom.Size = New System.Drawing.Size(145, 21)
        Me.numfrom.TabIndex = 45
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(321, 104)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 15)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "To:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(52, 105)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 15)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "From:"
        '
        'txtletter1
        '
        Me.txtletter1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtletter1.Location = New System.Drawing.Point(105, 102)
        Me.txtletter1.Name = "txtletter1"
        Me.txtletter1.Size = New System.Drawing.Size(34, 21)
        Me.txtletter1.TabIndex = 42
        '
        'btndeactivate
        '
        Me.btndeactivate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndeactivate.Image = CType(resources.GetObject("btndeactivate.Image"), System.Drawing.Image)
        Me.btndeactivate.Location = New System.Drawing.Point(204, 141)
        Me.btndeactivate.Name = "btndeactivate"
        Me.btndeactivate.Size = New System.Drawing.Size(163, 31)
        Me.btndeactivate.TabIndex = 41
        Me.btndeactivate.Text = "&Cancel Ticket Series"
        Me.btndeactivate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btndeactivate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndeactivate.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 15)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Letter:"
        Me.Label4.Visible = False
        '
        'btncancel
        '
        Me.btncancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(373, 141)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(103, 31)
        Me.btncancel.TabIndex = 40
        Me.btncancel.Text = "&Clear"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'grdrange
        '
        Me.grdrange.AllowUserToAddRows = False
        Me.grdrange.AllowUserToDeleteRows = False
        Me.grdrange.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.grdrange.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdrange.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdrange.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdrange.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdrange.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column2, Me.DataGridViewTextBoxColumn1, Me.Column1, Me.Column6, Me.Column4, Me.Column5, Me.DataGridViewTextBoxColumn2})
        Me.grdrange.EnableHeadersVisualStyles = False
        Me.grdrange.Location = New System.Drawing.Point(16, 187)
        Me.grdrange.Name = "grdrange"
        Me.grdrange.ReadOnly = True
        Me.grdrange.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdrange.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.grdrange.RowHeadersWidth = 10
        Me.grdrange.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdrange.Size = New System.Drawing.Size(638, 219)
        Me.grdrange.TabIndex = 36
        '
        'Column2
        '
        Me.Column2.HeaderText = "id"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Visible = False
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn1.HeaderText = "L"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 40
        '
        'Column1
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column1.HeaderText = "Range from"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 150
        '
        'Column6
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle5
        Me.Column6.HeaderText = "L"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 40
        '
        'Column4
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle6
        Me.Column4.HeaderText = "Range to"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 150
        '
        'Column5
        '
        Me.Column5.HeaderText = "total"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn2.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'btnadd
        '
        Me.btnadd.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.Image = CType(resources.GetObject("btnadd.Image"), System.Drawing.Image)
        Me.btnadd.Location = New System.Drawing.Point(93, 141)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(105, 31)
        Me.btnadd.TabIndex = 38
        Me.btnadd.Text = "&Add"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'lblline
        '
        Me.lblline.AutoSize = True
        Me.lblline.Location = New System.Drawing.Point(99, 62)
        Me.lblline.Name = "lblline"
        Me.lblline.Size = New System.Drawing.Size(0, 15)
        Me.lblline.TabIndex = 52
        '
        'lbllognum
        '
        Me.lbllognum.AutoSize = True
        Me.lbllognum.Location = New System.Drawing.Point(149, 24)
        Me.lbllognum.Name = "lbllognum"
        Me.lbllognum.Size = New System.Drawing.Size(0, 15)
        Me.lbllognum.TabIndex = 51
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 15)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "Ticket Log Sheet #:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 15)
        Me.Label1.TabIndex = 49
        Me.Label1.Text = "Palletizer:"
        '
        'lblid
        '
        Me.lblid.AutoSize = True
        Me.lblid.Location = New System.Drawing.Point(548, 149)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(0, 15)
        Me.lblid.TabIndex = 53
        Me.lblid.Visible = False
        '
        'lbltype
        '
        Me.lbltype.AutoSize = True
        Me.lbltype.Location = New System.Drawing.Point(461, 24)
        Me.lbltype.Name = "lbltype"
        Me.lbltype.Size = New System.Drawing.Size(0, 15)
        Me.lbltype.TabIndex = 54
        Me.lbltype.Visible = False
        '
        'tsetrange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(667, 471)
        Me.ControlBox = False
        Me.Controls.Add(Me.lbltype)
        Me.Controls.Add(Me.lblid)
        Me.Controls.Add(Me.lblline)
        Me.Controls.Add(Me.lbllognum)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtletter2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.numto)
        Me.Controls.Add(Me.numfrom)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtletter1)
        Me.Controls.Add(Me.btndeactivate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.grdrange)
        Me.Controls.Add(Me.btnadd)
        Me.Controls.Add(Me.btnclose)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "tsetrange"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Set Range"
        CType(Me.numto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numfrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdrange, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents txtletter2 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents numto As System.Windows.Forms.NumericUpDown
    Friend WithEvents numfrom As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtletter1 As System.Windows.Forms.TextBox
    Friend WithEvents btndeactivate As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents grdrange As System.Windows.Forms.DataGridView
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents lblline As System.Windows.Forms.Label
    Friend WithEvents lbllognum As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblid As System.Windows.Forms.Label
    Friend WithEvents lbltype As System.Windows.Forms.Label
End Class
