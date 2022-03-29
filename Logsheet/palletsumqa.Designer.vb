<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class palletsumqa
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(palletsumqa))
        Me.grdpallettag = New System.Windows.Forms.DataGridView
        Me.Column20 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnqacancel = New System.Windows.Forms.Button
        Me.btnqahold = New System.Windows.Forms.Button
        Me.btnqaok = New System.Windows.Forms.Button
        Me.txtrems = New System.Windows.Forms.TextBox
        CType(Me.grdpallettag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdpallettag
        '
        Me.grdpallettag.AllowUserToAddRows = False
        Me.grdpallettag.AllowUserToDeleteRows = False
        Me.grdpallettag.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grdpallettag.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdpallettag.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdpallettag.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdpallettag.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdpallettag.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdpallettag.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column20, Me.Column3, Me.Column1, Me.Column2})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdpallettag.DefaultCellStyle = DataGridViewCellStyle3
        Me.grdpallettag.EnableHeadersVisualStyles = False
        Me.grdpallettag.Location = New System.Drawing.Point(13, 14)
        Me.grdpallettag.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.grdpallettag.Name = "grdpallettag"
        Me.grdpallettag.ReadOnly = True
        Me.grdpallettag.RowHeadersWidth = 10
        Me.grdpallettag.Size = New System.Drawing.Size(791, 497)
        Me.grdpallettag.TabIndex = 73
        '
        'Column20
        '
        Me.Column20.Frozen = True
        Me.Column20.HeaderText = "logticketid"
        Me.Column20.Name = "Column20"
        Me.Column20.ReadOnly = True
        Me.Column20.Visible = False
        '
        'Column3
        '
        Me.Column3.Frozen = True
        Me.Column3.HeaderText = "Pallet Tag #"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 200
        '
        'Column1
        '
        Me.Column1.HeaderText = "QA Dispo"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 140
        '
        'Column2
        '
        Me.Column2.HeaderText = "Remarks"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 400
        '
        'btnqacancel
        '
        Me.btnqacancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnqacancel.Image = CType(resources.GetObject("btnqacancel.Image"), System.Drawing.Image)
        Me.btnqacancel.Location = New System.Drawing.Point(648, 563)
        Me.btnqacancel.Name = "btnqacancel"
        Me.btnqacancel.Size = New System.Drawing.Size(156, 23)
        Me.btnqacancel.TabIndex = 76
        Me.btnqacancel.Text = "Remove Dispo"
        Me.btnqacancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnqacancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnqacancel.UseVisualStyleBackColor = True
        '
        'btnqahold
        '
        Me.btnqahold.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnqahold.Image = CType(resources.GetObject("btnqahold.Image"), System.Drawing.Image)
        Me.btnqahold.Location = New System.Drawing.Point(729, 534)
        Me.btnqahold.Name = "btnqahold"
        Me.btnqahold.Size = New System.Drawing.Size(75, 23)
        Me.btnqahold.TabIndex = 75
        Me.btnqahold.Text = "Hold"
        Me.btnqahold.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnqahold.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnqahold.UseVisualStyleBackColor = True
        '
        'btnqaok
        '
        Me.btnqaok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnqaok.Image = Global.Logsheet.My.Resources.Resources.ok
        Me.btnqaok.Location = New System.Drawing.Point(648, 534)
        Me.btnqaok.Name = "btnqaok"
        Me.btnqaok.Size = New System.Drawing.Size(75, 23)
        Me.btnqaok.TabIndex = 74
        Me.btnqaok.Text = "Ok"
        Me.btnqaok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnqaok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnqaok.UseVisualStyleBackColor = True
        '
        'txtrems
        '
        Me.txtrems.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtrems.Location = New System.Drawing.Point(13, 534)
        Me.txtrems.Multiline = True
        Me.txtrems.Name = "txtrems"
        Me.txtrems.Size = New System.Drawing.Size(617, 65)
        Me.txtrems.TabIndex = 77
        '
        'palletsumqa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(816, 611)
        Me.Controls.Add(Me.txtrems)
        Me.Controls.Add(Me.btnqacancel)
        Me.Controls.Add(Me.btnqahold)
        Me.Controls.Add(Me.btnqaok)
        Me.Controls.Add(Me.grdpallettag)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "palletsumqa"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "QA Disposition"
        CType(Me.grdpallettag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdpallettag As System.Windows.Forms.DataGridView
    Friend WithEvents btnqacancel As System.Windows.Forms.Button
    Friend WithEvents btnqahold As System.Windows.Forms.Button
    Friend WithEvents btnqaok As System.Windows.Forms.Button
    Friend WithEvents txtrems As System.Windows.Forms.TextBox
    Friend WithEvents Column20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
