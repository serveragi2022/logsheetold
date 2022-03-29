<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class palletsumcancel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(palletsumcancel))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtitem = New System.Windows.Forms.TextBox
        Me.list3 = New System.Windows.Forms.ListBox
        Me.Panelcancel = New System.Windows.Forms.Panel
        Me.btncnlall = New System.Windows.Forms.Button
        Me.lbllogticketid = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtlog = New System.Windows.Forms.TextBox
        Me.txtletter = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnadd = New System.Windows.Forms.Button
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtcancel = New System.Windows.Forms.TextBox
        Me.txtpallet = New System.Windows.Forms.TextBox
        Me.btnremove = New System.Windows.Forms.Button
        Me.txtreason = New System.Windows.Forms.TextBox
        Me.cmbcancel = New System.Windows.Forms.ComboBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.grdcancel = New System.Windows.Forms.DataGridView
        Me.lblcount = New System.Windows.Forms.Label
        Me.Column20 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1.SuspendLayout()
        Me.Panelcancel.SuspendLayout()
        CType(Me.grdcancel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtitem)
        Me.GroupBox1.Controls.Add(Me.list3)
        Me.GroupBox1.Controls.Add(Me.Panelcancel)
        Me.GroupBox1.Controls.Add(Me.grdcancel)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1043, 418)
        Me.GroupBox1.TabIndex = 103
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Cancel Tickets"
        '
        'txtitem
        '
        Me.txtitem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtitem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtitem.Location = New System.Drawing.Point(307, 12)
        Me.txtitem.Name = "txtitem"
        Me.txtitem.ReadOnly = True
        Me.txtitem.Size = New System.Drawing.Size(730, 20)
        Me.txtitem.TabIndex = 99
        Me.txtitem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'list3
        '
        Me.list3.FormattingEnabled = True
        Me.list3.ItemHeight = 14
        Me.list3.Location = New System.Drawing.Point(917, 162)
        Me.list3.Name = "list3"
        Me.list3.Size = New System.Drawing.Size(120, 88)
        Me.list3.TabIndex = 101
        Me.list3.Visible = False
        '
        'Panelcancel
        '
        Me.Panelcancel.Controls.Add(Me.btncnlall)
        Me.Panelcancel.Controls.Add(Me.lbllogticketid)
        Me.Panelcancel.Controls.Add(Me.Label1)
        Me.Panelcancel.Controls.Add(Me.txtlog)
        Me.Panelcancel.Controls.Add(Me.txtletter)
        Me.Panelcancel.Controls.Add(Me.Label2)
        Me.Panelcancel.Controls.Add(Me.btnadd)
        Me.Panelcancel.Controls.Add(Me.Label19)
        Me.Panelcancel.Controls.Add(Me.txtcancel)
        Me.Panelcancel.Controls.Add(Me.txtpallet)
        Me.Panelcancel.Controls.Add(Me.btnremove)
        Me.Panelcancel.Controls.Add(Me.txtreason)
        Me.Panelcancel.Controls.Add(Me.cmbcancel)
        Me.Panelcancel.Controls.Add(Me.Label21)
        Me.Panelcancel.Enabled = False
        Me.Panelcancel.Location = New System.Drawing.Point(6, 12)
        Me.Panelcancel.Name = "Panelcancel"
        Me.Panelcancel.Size = New System.Drawing.Size(295, 400)
        Me.Panelcancel.TabIndex = 100
        '
        'btncnlall
        '
        Me.btncnlall.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncnlall.Image = CType(resources.GetObject("btncnlall.Image"), System.Drawing.Image)
        Me.btncnlall.Location = New System.Drawing.Point(83, 164)
        Me.btncnlall.Name = "btncnlall"
        Me.btncnlall.Size = New System.Drawing.Size(199, 25)
        Me.btncnlall.TabIndex = 18
        Me.btncnlall.Text = "Cancel All Available"
        Me.btncnlall.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncnlall.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncnlall.UseVisualStyleBackColor = True
        '
        'lbllogticketid
        '
        Me.lbllogticketid.AutoSize = True
        Me.lbllogticketid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllogticketid.Location = New System.Drawing.Point(80, 6)
        Me.lbllogticketid.Name = "lbllogticketid"
        Me.lbllogticketid.Size = New System.Drawing.Size(0, 14)
        Me.lbllogticketid.TabIndex = 99
        Me.lbllogticketid.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 14)
        Me.Label1.TabIndex = 98
        Me.Label1.Text = "Logsheet #:"
        '
        'txtlog
        '
        Me.txtlog.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtlog.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlog.Location = New System.Drawing.Point(83, 27)
        Me.txtlog.Name = "txtlog"
        Me.txtlog.ReadOnly = True
        Me.txtlog.Size = New System.Drawing.Size(199, 20)
        Me.txtlog.TabIndex = 97
        '
        'txtletter
        '
        Me.txtletter.BackColor = System.Drawing.Color.White
        Me.txtletter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtletter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtletter.Location = New System.Drawing.Point(83, 80)
        Me.txtletter.Name = "txtletter"
        Me.txtletter.Size = New System.Drawing.Size(23, 20)
        Me.txtletter.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 14)
        Me.Label2.TabIndex = 96
        Me.Label2.Text = "Pallet Tag #:"
        '
        'btnadd
        '
        Me.btnadd.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.Image = CType(resources.GetObject("btnadd.Image"), System.Drawing.Image)
        Me.btnadd.Location = New System.Drawing.Point(83, 133)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(199, 25)
        Me.btnadd.TabIndex = 17
        Me.btnadd.Text = "Add"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(10, 110)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(62, 14)
        Me.Label19.TabIndex = 89
        Me.Label19.Text = "Disposition:"
        '
        'txtcancel
        '
        Me.txtcancel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcancel.Location = New System.Drawing.Point(112, 80)
        Me.txtcancel.Name = "txtcancel"
        Me.txtcancel.Size = New System.Drawing.Size(170, 20)
        Me.txtcancel.TabIndex = 15
        '
        'txtpallet
        '
        Me.txtpallet.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtpallet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpallet.Location = New System.Drawing.Point(83, 53)
        Me.txtpallet.Name = "txtpallet"
        Me.txtpallet.ReadOnly = True
        Me.txtpallet.Size = New System.Drawing.Size(199, 20)
        Me.txtpallet.TabIndex = 13
        '
        'btnremove
        '
        Me.btnremove.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnremove.Image = CType(resources.GetObject("btnremove.Image"), System.Drawing.Image)
        Me.btnremove.Location = New System.Drawing.Point(83, 195)
        Me.btnremove.Name = "btnremove"
        Me.btnremove.Size = New System.Drawing.Size(199, 25)
        Me.btnremove.TabIndex = 100
        Me.btnremove.Text = "Remove"
        Me.btnremove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnremove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnremove.UseVisualStyleBackColor = True
        Me.btnremove.Visible = False
        '
        'txtreason
        '
        Me.txtreason.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtreason.Location = New System.Drawing.Point(83, 107)
        Me.txtreason.Name = "txtreason"
        Me.txtreason.Size = New System.Drawing.Size(199, 20)
        Me.txtreason.TabIndex = 16
        '
        'cmbcancel
        '
        Me.cmbcancel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbcancel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbcancel.FormattingEnabled = True
        Me.cmbcancel.Location = New System.Drawing.Point(10, 181)
        Me.cmbcancel.Name = "cmbcancel"
        Me.cmbcancel.Size = New System.Drawing.Size(62, 22)
        Me.cmbcancel.TabIndex = 89
        Me.cmbcancel.Visible = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(25, 83)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(47, 14)
        Me.Label21.TabIndex = 90
        Me.Label21.Text = "Ticket #:"
        '
        'grdcancel
        '
        Me.grdcancel.AllowUserToAddRows = False
        Me.grdcancel.AllowUserToDeleteRows = False
        Me.grdcancel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdcancel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdcancel.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column20, Me.Column2, Me.Column9, Me.Column8, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.Column1, Me.DataGridViewTextBoxColumn11, Me.Column3, Me.Column4})
        Me.grdcancel.EnableHeadersVisualStyles = False
        Me.grdcancel.Location = New System.Drawing.Point(307, 38)
        Me.grdcancel.Name = "grdcancel"
        Me.grdcancel.ReadOnly = True
        Me.grdcancel.RowHeadersWidth = 20
        Me.grdcancel.Size = New System.Drawing.Size(730, 374)
        Me.grdcancel.TabIndex = 21
        '
        'lblcount
        '
        Me.lblcount.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblcount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcount.Location = New System.Drawing.Point(0, 433)
        Me.lblcount.Name = "lblcount"
        Me.lblcount.Size = New System.Drawing.Size(1069, 25)
        Me.lblcount.TabIndex = 104
        Me.lblcount.Text = "Count:"
        '
        'Column20
        '
        Me.Column20.HeaderText = "ID"
        Me.Column20.Name = "Column20"
        Me.Column20.ReadOnly = True
        Me.Column20.Visible = False
        '
        'Column2
        '
        Me.Column2.HeaderText = "From"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "Log Sheet#"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Visible = False
        '
        'Column8
        '
        Me.Column8.HeaderText = "Pallet Tag #"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Visible = False
        Me.Column8.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "L"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 40
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Cancel Ticket #"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 150
        '
        'DataGridViewTextBoxColumn7
        '
        DataGridViewCellStyle1.Format = "yyyy/MM/dd HH:mm"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewTextBoxColumn7.HeaderText = "Date Time"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 120
        '
        'Column1
        '
        Me.Column1.HeaderText = "Gross W"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 80
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "Disposition"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Width = 250
        '
        'Column3
        '
        Me.Column3.HeaderText = "Ticket Type"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Visible = False
        '
        'Column4
        '
        Me.Column4.HeaderText = "Cancelled by"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 120
        '
        'palletsumcancel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1069, 458)
        Me.Controls.Add(Me.lblcount)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "palletsumcancel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cancel Ticket"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panelcancel.ResumeLayout(False)
        Me.Panelcancel.PerformLayout()
        CType(Me.grdcancel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents list3 As System.Windows.Forms.ListBox
    Friend WithEvents Panelcancel As System.Windows.Forms.Panel
    Friend WithEvents txtletter As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtcancel As System.Windows.Forms.TextBox
    Friend WithEvents txtpallet As System.Windows.Forms.TextBox
    Friend WithEvents btnremove As System.Windows.Forms.Button
    Friend WithEvents txtreason As System.Windows.Forms.TextBox
    Friend WithEvents cmbcancel As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents grdcancel As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtlog As System.Windows.Forms.TextBox
    Friend WithEvents txtitem As System.Windows.Forms.TextBox
    Friend WithEvents lbllogticketid As System.Windows.Forms.Label
    Friend WithEvents btncnlall As System.Windows.Forms.Button
    Friend WithEvents lblcount As System.Windows.Forms.Label
    Friend WithEvents Column20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
