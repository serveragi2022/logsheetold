<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class chooseshift
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(chooseshift))
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbshift = New System.Windows.Forms.ComboBox
        Me.btnok = New System.Windows.Forms.Button
        Me.btncancel = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbwhse = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Shift name:"
        '
        'cmbshift
        '
        Me.cmbshift.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbshift.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbshift.FormattingEnabled = True
        Me.cmbshift.Location = New System.Drawing.Point(107, 28)
        Me.cmbshift.Name = "cmbshift"
        Me.cmbshift.Size = New System.Drawing.Size(209, 23)
        Me.cmbshift.TabIndex = 0
        '
        'btnok
        '
        Me.btnok.Image = CType(resources.GetObject("btnok.Image"), System.Drawing.Image)
        Me.btnok.Location = New System.Drawing.Point(138, 135)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(86, 27)
        Me.btnok.TabIndex = 2
        Me.btnok.Text = "Ok"
        Me.btnok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnok.UseVisualStyleBackColor = True
        '
        'btncancel
        '
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(230, 135)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(86, 27)
        Me.btncancel.TabIndex = 3
        Me.btncancel.Text = "Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 15)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Warehouse:"
        '
        'cmbwhse
        '
        Me.cmbwhse.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbwhse.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbwhse.FormattingEnabled = True
        Me.cmbwhse.Location = New System.Drawing.Point(107, 75)
        Me.cmbwhse.Name = "cmbwhse"
        Me.cmbwhse.Size = New System.Drawing.Size(209, 23)
        Me.cmbwhse.TabIndex = 1
        '
        'chooseshift
        '
        Me.AcceptButton = Me.btnok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(361, 180)
        Me.Controls.Add(Me.cmbwhse)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.cmbshift)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "chooseshift"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ticket Log Sheet"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbshift As System.Windows.Forms.ComboBox
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbwhse As System.Windows.Forms.ComboBox
End Class
