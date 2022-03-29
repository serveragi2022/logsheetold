<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptcoarevise
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptcoarevise))
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.list2 = New System.Windows.Forms.ListBox()
        Me.list1 = New System.Windows.Forms.ListBox()
        Me.btncopy = New System.Windows.Forms.Button()
        Me.grdgoods = New System.Windows.Forms.DataGridView()
        Me.Column17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.grdgoods, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.ShowGroupTreeButton = False
        Me.CrystalReportViewer1.ShowRefreshButton = False
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1008, 558)
        Me.CrystalReportViewer1.TabIndex = 1
        Me.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        'list2
        '
        Me.list2.FormattingEnabled = True
        Me.list2.Location = New System.Drawing.Point(199, 122)
        Me.list2.Name = "list2"
        Me.list2.Size = New System.Drawing.Size(94, 121)
        Me.list2.TabIndex = 103
        Me.list2.Visible = False
        '
        'list1
        '
        Me.list1.FormattingEnabled = True
        Me.list1.Location = New System.Drawing.Point(101, 122)
        Me.list1.Name = "list1"
        Me.list1.Size = New System.Drawing.Size(92, 121)
        Me.list1.TabIndex = 102
        Me.list1.Visible = False
        '
        'btncopy
        '
        Me.btncopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btncopy.Image = CType(resources.GetObject("btncopy.Image"), System.Drawing.Image)
        Me.btncopy.Location = New System.Drawing.Point(856, 4)
        Me.btncopy.Name = "btncopy"
        Me.btncopy.Size = New System.Drawing.Size(148, 27)
        Me.btncopy.TabIndex = 104
        Me.btncopy.Text = "Copy Ticket Series"
        Me.btncopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncopy.UseVisualStyleBackColor = True
        '
        'grdgoods
        '
        Me.grdgoods.AllowUserToAddRows = False
        Me.grdgoods.AllowUserToDeleteRows = False
        Me.grdgoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdgoods.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column17, Me.Column18})
        Me.grdgoods.Location = New System.Drawing.Point(101, 249)
        Me.grdgoods.Name = "grdgoods"
        Me.grdgoods.ReadOnly = True
        Me.grdgoods.RowHeadersWidth = 10
        Me.grdgoods.Size = New System.Drawing.Size(249, 98)
        Me.grdgoods.TabIndex = 105
        Me.grdgoods.Visible = False
        '
        'Column17
        '
        Me.Column17.HeaderText = "ticketnum"
        Me.Column17.Name = "Column17"
        Me.Column17.ReadOnly = True
        '
        'Column18
        '
        Me.Column18.HeaderText = "num"
        Me.Column18.Name = "Column18"
        Me.Column18.ReadOnly = True
        '
        'rptcoarevise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 558)
        Me.Controls.Add(Me.grdgoods)
        Me.Controls.Add(Me.btncopy)
        Me.Controls.Add(Me.list2)
        Me.Controls.Add(Me.list1)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "rptcoarevise"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "COA Report"
        CType(Me.grdgoods, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents list2 As System.Windows.Forms.ListBox
    Friend WithEvents list1 As System.Windows.Forms.ListBox
    Friend WithEvents btncopy As System.Windows.Forms.Button
    Friend WithEvents grdgoods As System.Windows.Forms.DataGridView
    Friend WithEvents Column17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column18 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
