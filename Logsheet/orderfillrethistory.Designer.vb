<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class orderfillrethistory
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grdreturn = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lblitem = New System.Windows.Forms.Label
        Me.grdtickets = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewLinkColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewLinkColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grdgoods = New System.Windows.Forms.DataGridView
        Me.list2 = New System.Windows.Forms.ListBox
        Me.list1 = New System.Windows.Forms.ListBox
        CType(Me.grdreturn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdtickets, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdgoods, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdreturn
        '
        Me.grdreturn.AllowUserToAddRows = False
        Me.grdreturn.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grdreturn.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdreturn.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdreturn.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdreturn.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdreturn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdreturn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column7, Me.Column2, Me.Column3, Me.Column5, Me.Column4, Me.Column6, Me.Column8})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdreturn.DefaultCellStyle = DataGridViewCellStyle3
        Me.grdreturn.EnableHeadersVisualStyles = False
        Me.grdreturn.Location = New System.Drawing.Point(12, 50)
        Me.grdreturn.MultiSelect = False
        Me.grdreturn.Name = "grdreturn"
        Me.grdreturn.ReadOnly = True
        Me.grdreturn.RowHeadersWidth = 10
        Me.grdreturn.Size = New System.Drawing.Size(1054, 446)
        Me.grdreturn.TabIndex = 15
        '
        'Column1
        '
        Me.Column1.Frozen = True
        Me.Column1.HeaderText = "ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 80
        '
        'Column7
        '
        Me.Column7.Frozen = True
        Me.Column7.HeaderText = "Pallet Tag #"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column7.Width = 150
        '
        'Column2
        '
        Me.Column2.HeaderText = "Returned Type"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Number of Returned Bags"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 115
        '
        'Column5
        '
        Me.Column5.HeaderText = "Returned Ticket Series"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 150
        '
        'Column4
        '
        Me.Column4.HeaderText = "Returned Date"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "Returned By"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "Reason"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 200
        '
        'lblitem
        '
        Me.lblitem.AutoSize = True
        Me.lblitem.Location = New System.Drawing.Point(12, 18)
        Me.lblitem.Name = "lblitem"
        Me.lblitem.Size = New System.Drawing.Size(39, 13)
        Me.lblitem.TabIndex = 16
        Me.lblitem.Text = "Label1"
        '
        'grdtickets
        '
        Me.grdtickets.AllowUserToAddRows = False
        Me.grdtickets.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grdtickets.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.grdtickets.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdtickets.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 8.25!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdtickets.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.grdtickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdtickets.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewLinkColumn1, Me.DataGridViewLinkColumn2, Me.DataGridViewTextBoxColumn4, Me.Column15, Me.Column16, Me.Column9})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Arial", 8.25!)
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdtickets.DefaultCellStyle = DataGridViewCellStyle7
        Me.grdtickets.EnableHeadersVisualStyles = False
        Me.grdtickets.Location = New System.Drawing.Point(68, 145)
        Me.grdtickets.MultiSelect = False
        Me.grdtickets.Name = "grdtickets"
        Me.grdtickets.ReadOnly = True
        Me.grdtickets.RowHeadersWidth = 10
        Me.grdtickets.Size = New System.Drawing.Size(932, 219)
        Me.grdtickets.TabIndex = 108
        Me.grdtickets.Visible = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "oflogid"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewLinkColumn1
        '
        Me.DataGridViewLinkColumn1.HeaderText = "Ticket Log Sheet"
        Me.DataGridViewLinkColumn1.Name = "DataGridViewLinkColumn1"
        Me.DataGridViewLinkColumn1.ReadOnly = True
        Me.DataGridViewLinkColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewLinkColumn1.Width = 170
        '
        'DataGridViewLinkColumn2
        '
        Me.DataGridViewLinkColumn2.HeaderText = "Pallet Tag #"
        Me.DataGridViewLinkColumn2.Name = "DataGridViewLinkColumn2"
        Me.DataGridViewLinkColumn2.ReadOnly = True
        Me.DataGridViewLinkColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle6.NullValue = Nothing
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn4.HeaderText = "Ticket Number"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 150
        '
        'Column15
        '
        Me.Column15.HeaderText = "number"
        Me.Column15.Name = "Column15"
        Me.Column15.ReadOnly = True
        '
        'Column16
        '
        Me.Column16.HeaderText = "Letter"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "retid"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'grdgoods
        '
        Me.grdgoods.AllowUserToAddRows = False
        Me.grdgoods.AllowUserToDeleteRows = False
        Me.grdgoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdgoods.Location = New System.Drawing.Point(68, 370)
        Me.grdgoods.Name = "grdgoods"
        Me.grdgoods.ReadOnly = True
        Me.grdgoods.Size = New System.Drawing.Size(240, 105)
        Me.grdgoods.TabIndex = 114
        Me.grdgoods.Visible = False
        '
        'list2
        '
        Me.list2.FormattingEnabled = True
        Me.list2.Location = New System.Drawing.Point(440, 370)
        Me.list2.Name = "list2"
        Me.list2.Size = New System.Drawing.Size(120, 95)
        Me.list2.TabIndex = 113
        Me.list2.Visible = False
        '
        'list1
        '
        Me.list1.FormattingEnabled = True
        Me.list1.Location = New System.Drawing.Point(314, 370)
        Me.list1.Name = "list1"
        Me.list1.Size = New System.Drawing.Size(120, 95)
        Me.list1.TabIndex = 112
        Me.list1.Visible = False
        '
        'orderfillrethistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1078, 508)
        Me.Controls.Add(Me.grdgoods)
        Me.Controls.Add(Me.list2)
        Me.Controls.Add(Me.list1)
        Me.Controls.Add(Me.grdtickets)
        Me.Controls.Add(Me.lblitem)
        Me.Controls.Add(Me.grdreturn)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "orderfillrethistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Returned Items"
        CType(Me.grdreturn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdtickets, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdgoods, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdreturn As System.Windows.Forms.DataGridView
    Friend WithEvents lblitem As System.Windows.Forms.Label
    Friend WithEvents grdtickets As System.Windows.Forms.DataGridView
    Friend WithEvents grdgoods As System.Windows.Forms.DataGridView
    Friend WithEvents list2 As System.Windows.Forms.ListBox
    Friend WithEvents list1 As System.Windows.Forms.ListBox
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewLinkColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewLinkColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
