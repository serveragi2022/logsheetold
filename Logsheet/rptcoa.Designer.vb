<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptcoa
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptcoa))
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.lblmax = New System.Windows.Forms.Label
        Me.list2 = New System.Windows.Forms.ListBox
        Me.list1 = New System.Windows.Forms.ListBox
        Me.grd = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.DisplayGroupTree = False
        Me.CrystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.SelectionFormula = ""
        Me.CrystalReportViewer1.ShowGroupTreeButton = False
        Me.CrystalReportViewer1.ShowRefreshButton = False
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(1243, 675)
        Me.CrystalReportViewer1.TabIndex = 0
        Me.CrystalReportViewer1.ViewTimeSelectionFormula = ""
        '
        'lblmax
        '
        Me.lblmax.AutoSize = True
        Me.lblmax.Location = New System.Drawing.Point(55, 211)
        Me.lblmax.Name = "lblmax"
        Me.lblmax.Size = New System.Drawing.Size(0, 13)
        Me.lblmax.TabIndex = 2
        Me.lblmax.Visible = False
        '
        'list2
        '
        Me.list2.FormattingEnabled = True
        Me.list2.Location = New System.Drawing.Point(199, 94)
        Me.list2.Name = "list2"
        Me.list2.Size = New System.Drawing.Size(62, 121)
        Me.list2.TabIndex = 101
        Me.list2.Visible = False
        '
        'list1
        '
        Me.list1.FormattingEnabled = True
        Me.list1.Location = New System.Drawing.Point(77, 94)
        Me.list1.Name = "list1"
        Me.list1.Size = New System.Drawing.Size(62, 121)
        Me.list1.TabIndex = 100
        Me.list1.Visible = False
        '
        'grd
        '
        Me.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3})
        Me.grd.Location = New System.Drawing.Point(612, 94)
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(351, 445)
        Me.grd.TabIndex = 102
        Me.grd.Visible = False
        '
        'Column1
        '
        Me.Column1.HeaderText = "oflogid"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "palletnum"
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "gticketnum"
        Me.Column3.Name = "Column3"
        '
        'rptcoa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1243, 675)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.list2)
        Me.Controls.Add(Me.list1)
        Me.Controls.Add(Me.lblmax)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "rptcoa"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Order Fill"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents lblmax As System.Windows.Forms.Label
    Friend WithEvents list2 As System.Windows.Forms.ListBox
    Friend WithEvents list1 As System.Windows.Forms.ListBox
    Friend WithEvents grd As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
