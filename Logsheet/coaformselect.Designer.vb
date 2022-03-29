<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class coaformselect
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(coaformselect))
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbformat = New System.Windows.Forms.ComboBox
        Me.btnclose = New System.Windows.Forms.Button
        Me.btnok = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "COA Format:"
        '
        'cmbformat
        '
        Me.cmbformat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbformat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbformat.BackColor = System.Drawing.Color.PaleTurquoise
        Me.cmbformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbformat.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbformat.FormattingEnabled = True
        Me.cmbformat.Location = New System.Drawing.Point(104, 17)
        Me.cmbformat.Name = "cmbformat"
        Me.cmbformat.Size = New System.Drawing.Size(128, 21)
        Me.cmbformat.TabIndex = 39
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.Color.Gainsboro
        Me.btnclose.Image = CType(resources.GetObject("btnclose.Image"), System.Drawing.Image)
        Me.btnclose.Location = New System.Drawing.Point(157, 54)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(75, 23)
        Me.btnclose.TabIndex = 42
        Me.btnclose.Text = "Cancel"
        Me.btnclose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'btnok
        '
        Me.btnok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnok.BackColor = System.Drawing.Color.Gainsboro
        Me.btnok.Image = CType(resources.GetObject("btnok.Image"), System.Drawing.Image)
        Me.btnok.Location = New System.Drawing.Point(67, 54)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(75, 23)
        Me.btnok.TabIndex = 41
        Me.btnok.Text = "Ok"
        Me.btnok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnok.UseVisualStyleBackColor = False
        '
        'coaformselect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(272, 89)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbformat)
        Me.Name = "coaformselect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbformat As System.Windows.Forms.ComboBox
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnok As System.Windows.Forms.Button
End Class
