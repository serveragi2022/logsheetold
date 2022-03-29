<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class stocksheet
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(stocksheet))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grdstocks = New System.Windows.Forms.DataGridView
        Me.btnexport = New System.Windows.Forms.Button
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column19 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.grdstocks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdstocks
        '
        Me.grdstocks.AllowUserToAddRows = False
        Me.grdstocks.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grdstocks.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdstocks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdstocks.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdstocks.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdstocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdstocks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column11, Me.Column19, Me.Column8})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdstocks.DefaultCellStyle = DataGridViewCellStyle4
        Me.grdstocks.EnableHeadersVisualStyles = False
        Me.grdstocks.Location = New System.Drawing.Point(11, 12)
        Me.grdstocks.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.grdstocks.Name = "grdstocks"
        Me.grdstocks.ReadOnly = True
        Me.grdstocks.RowHeadersWidth = 20
        Me.grdstocks.Size = New System.Drawing.Size(996, 519)
        Me.grdstocks.TabIndex = 20
        '
        'btnexport
        '
        Me.btnexport.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.btnexport.Image = CType(resources.GetObject("btnexport.Image"), System.Drawing.Image)
        Me.btnexport.Location = New System.Drawing.Point(903, 537)
        Me.btnexport.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(104, 24)
        Me.btnexport.TabIndex = 21
        Me.btnexport.Text = "Export"
        Me.btnexport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnexport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexport.UseVisualStyleBackColor = True
        '
        'Column11
        '
        DataGridViewCellStyle3.Format = "MM/dd/yyyy"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Column11.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column11.Frozen = True
        Me.Column11.HeaderText = "Production Date"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        Me.Column11.Width = 150
        '
        'Column19
        '
        Me.Column19.Frozen = True
        Me.Column19.HeaderText = "Item Name"
        Me.Column19.Name = "Column19"
        Me.Column19.ReadOnly = True
        Me.Column19.Width = 350
        '
        'Column8
        '
        Me.Column8.HeaderText = "No. of Available Bags"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 180
        '
        'stocksheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1018, 571)
        Me.Controls.Add(Me.btnexport)
        Me.Controls.Add(Me.grdstocks)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "stocksheet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Stock Sheet"
        CType(Me.grdstocks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdstocks As System.Windows.Forms.DataGridView
    Friend WithEvents btnexport As System.Windows.Forms.Button
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
