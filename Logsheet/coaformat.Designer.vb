<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class coaformat
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(coaformat))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtlabel = New System.Windows.Forms.TextBox()
        Me.txtcat = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtname = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rbspecs = New System.Windows.Forms.RadioButton()
        Me.rbmethod = New System.Windows.Forms.RadioButton()
        Me.rbstand = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtvalue = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btndeac = New System.Windows.Forms.Button()
        Me.btnupdate = New System.Windows.Forms.Button()
        Me.btncancel = New System.Windows.Forms.Button()
        Me.lblid = New System.Windows.Forms.Label()
        Me.btnadd = New System.Windows.Forms.Button()
        Me.grd = New System.Windows.Forms.DataGridView()
        Me.cmbformat = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnconfirm = New System.Windows.Forms.Button()
        Me.txtleg = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.pid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.neym = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.spec = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Analysis Label:"
        '
        'txtlabel
        '
        Me.txtlabel.Location = New System.Drawing.Point(107, 46)
        Me.txtlabel.Name = "txtlabel"
        Me.txtlabel.Size = New System.Drawing.Size(185, 20)
        Me.txtlabel.TabIndex = 1
        '
        'txtcat
        '
        Me.txtcat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcat.Location = New System.Drawing.Point(106, 47)
        Me.txtcat.Name = "txtcat"
        Me.txtcat.Size = New System.Drawing.Size(174, 20)
        Me.txtcat.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(48, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Category:"
        '
        'txtname
        '
        Me.txtname.Location = New System.Drawing.Point(106, 73)
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(174, 20)
        Me.txtname.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Criteria Name:"
        '
        'rbspecs
        '
        Me.rbspecs.AutoSize = True
        Me.rbspecs.Location = New System.Drawing.Point(348, 47)
        Me.rbspecs.Name = "rbspecs"
        Me.rbspecs.Size = New System.Drawing.Size(86, 17)
        Me.rbspecs.TabIndex = 6
        Me.rbspecs.Text = "Specification"
        Me.rbspecs.UseVisualStyleBackColor = True
        '
        'rbmethod
        '
        Me.rbmethod.AutoSize = True
        Me.rbmethod.Location = New System.Drawing.Point(459, 47)
        Me.rbmethod.Name = "rbmethod"
        Me.rbmethod.Size = New System.Drawing.Size(61, 17)
        Me.rbmethod.TabIndex = 7
        Me.rbmethod.Text = "Method"
        Me.rbmethod.UseVisualStyleBackColor = True
        '
        'rbstand
        '
        Me.rbstand.AutoSize = True
        Me.rbstand.Location = New System.Drawing.Point(547, 47)
        Me.rbstand.Name = "rbstand"
        Me.rbstand.Size = New System.Drawing.Size(68, 17)
        Me.rbstand.TabIndex = 8
        Me.rbstand.Text = "Standard"
        Me.rbstand.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(6, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 17)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Criteria:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtvalue
        '
        Me.txtvalue.Location = New System.Drawing.Point(106, 99)
        Me.txtvalue.Name = "txtvalue"
        Me.txtvalue.Size = New System.Drawing.Size(174, 20)
        Me.txtvalue.TabIndex = 10
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btndeac)
        Me.GroupBox1.Controls.Add(Me.btnupdate)
        Me.GroupBox1.Controls.Add(Me.btncancel)
        Me.GroupBox1.Controls.Add(Me.lblid)
        Me.GroupBox1.Controls.Add(Me.btnadd)
        Me.GroupBox1.Controls.Add(Me.txtcat)
        Me.GroupBox1.Controls.Add(Me.grd)
        Me.GroupBox1.Controls.Add(Me.txtvalue)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtname)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 80)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(983, 380)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parameters"
        '
        'btndeac
        '
        Me.btndeac.BackColor = System.Drawing.Color.Gainsboro
        Me.btndeac.Image = CType(resources.GetObject("btndeac.Image"), System.Drawing.Image)
        Me.btndeac.Location = New System.Drawing.Point(106, 229)
        Me.btndeac.Name = "btndeac"
        Me.btndeac.Size = New System.Drawing.Size(174, 23)
        Me.btndeac.TabIndex = 36
        Me.btndeac.Text = "Deactivate"
        Me.btndeac.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btndeac.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndeac.UseVisualStyleBackColor = False
        '
        'btnupdate
        '
        Me.btnupdate.BackColor = System.Drawing.Color.Gainsboro
        Me.btnupdate.Image = CType(resources.GetObject("btnupdate.Image"), System.Drawing.Image)
        Me.btnupdate.Location = New System.Drawing.Point(106, 171)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(174, 23)
        Me.btnupdate.TabIndex = 35
        Me.btnupdate.Text = "Update"
        Me.btnupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'btncancel
        '
        Me.btncancel.BackColor = System.Drawing.Color.Gainsboro
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(106, 200)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(174, 23)
        Me.btncancel.TabIndex = 34
        Me.btncancel.Text = "Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = False
        '
        'lblid
        '
        Me.lblid.AutoSize = True
        Me.lblid.Location = New System.Drawing.Point(121, 31)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(0, 13)
        Me.lblid.TabIndex = 33
        Me.lblid.Visible = False
        '
        'btnadd
        '
        Me.btnadd.BackColor = System.Drawing.Color.Gainsboro
        Me.btnadd.Image = CType(resources.GetObject("btnadd.Image"), System.Drawing.Image)
        Me.btnadd.Location = New System.Drawing.Point(106, 142)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(174, 23)
        Me.btnadd.TabIndex = 32
        Me.btnadd.Text = "Add"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'grd
        '
        Me.grd.AllowUserToAddRows = False
        Me.grd.AllowUserToDeleteRows = False
        Me.grd.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.grd.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grd.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.pid, Me.cat, Me.neym, Me.spec, Me.Column1})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grd.DefaultCellStyle = DataGridViewCellStyle3
        Me.grd.EnableHeadersVisualStyles = False
        Me.grd.Location = New System.Drawing.Point(289, 26)
        Me.grd.MultiSelect = False
        Me.grd.Name = "grd"
        Me.grd.ReadOnly = True
        Me.grd.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grd.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grd.RowHeadersWidth = 10
        Me.grd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grd.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grd.Size = New System.Drawing.Size(688, 348)
        Me.grd.TabIndex = 34
        '
        'cmbformat
        '
        Me.cmbformat.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbformat.FormattingEnabled = True
        Me.cmbformat.Items.AddRange(New Object() {"", "Master COA", "COA 1", "COA 2", "COA 3", "COA 4", "COA 5", "COA 6", "COA 7"})
        Me.cmbformat.Location = New System.Drawing.Point(107, 14)
        Me.cmbformat.Name = "cmbformat"
        Me.cmbformat.Size = New System.Drawing.Size(185, 21)
        Me.cmbformat.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(59, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Format:"
        '
        'btnconfirm
        '
        Me.btnconfirm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnconfirm.BackColor = System.Drawing.Color.Gainsboro
        Me.btnconfirm.Image = CType(resources.GetObject("btnconfirm.Image"), System.Drawing.Image)
        Me.btnconfirm.Location = New System.Drawing.Point(892, 484)
        Me.btnconfirm.Name = "btnconfirm"
        Me.btnconfirm.Size = New System.Drawing.Size(103, 23)
        Me.btnconfirm.TabIndex = 36
        Me.btnconfirm.Text = "Confirm"
        Me.btnconfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnconfirm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnconfirm.UseVisualStyleBackColor = False
        '
        'txtleg
        '
        Me.txtleg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtleg.Location = New System.Drawing.Point(67, 466)
        Me.txtleg.Multiline = True
        Me.txtleg.Name = "txtleg"
        Me.txtleg.Size = New System.Drawing.Size(592, 41)
        Me.txtleg.TabIndex = 38
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 469)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 13)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = "Legend:"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(95, 26)
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Image = CType(resources.GetObject("EditToolStripMenuItem.Image"), System.Drawing.Image)
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(94, 22)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'pid
        '
        Me.pid.HeaderText = "ID"
        Me.pid.Name = "pid"
        Me.pid.ReadOnly = True
        Me.pid.Visible = False
        '
        'cat
        '
        Me.cat.HeaderText = "Category"
        Me.cat.Name = "cat"
        Me.cat.ReadOnly = True
        Me.cat.Width = 200
        '
        'neym
        '
        Me.neym.HeaderText = "Criteria Name"
        Me.neym.Name = "neym"
        Me.neym.ReadOnly = True
        Me.neym.Width = 220
        '
        'spec
        '
        Me.spec.HeaderText = "Criteria"
        Me.spec.Name = "spec"
        Me.spec.ReadOnly = True
        Me.spec.Width = 300
        '
        'Column1
        '
        Me.Column1.HeaderText = "Status"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        '
        'coaformat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 523)
        Me.Controls.Add(Me.txtleg)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnconfirm)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbformat)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtlabel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.rbspecs)
        Me.Controls.Add(Me.rbmethod)
        Me.Controls.Add(Me.rbstand)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "coaformat"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "COA Format"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtlabel As System.Windows.Forms.TextBox
    Friend WithEvents txtcat As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtname As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rbspecs As System.Windows.Forms.RadioButton
    Friend WithEvents rbmethod As System.Windows.Forms.RadioButton
    Friend WithEvents rbstand As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtvalue As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbformat As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents grd As System.Windows.Forms.DataGridView
    Friend WithEvents btnconfirm As System.Windows.Forms.Button
    Friend WithEvents txtleg As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblid As System.Windows.Forms.Label
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnupdate As Button
    Friend WithEvents btndeac As Button
    Friend WithEvents pid As DataGridViewTextBoxColumn
    Friend WithEvents cat As DataGridViewTextBoxColumn
    Friend WithEvents neym As DataGridViewTextBoxColumn
    Friend WithEvents spec As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
End Class
