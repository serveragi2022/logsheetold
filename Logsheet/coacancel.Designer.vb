﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class coacancel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(coacancel))
        Me.lblcoanum = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtrems = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.btncancel = New System.Windows.Forms.Button
        Me.btnok = New System.Windows.Forms.Button
        Me.lblcoaid = New System.Windows.Forms.Label
        Me.list1 = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'lblcoanum
        '
        Me.lblcoanum.AutoSize = True
        Me.lblcoanum.Location = New System.Drawing.Point(94, 21)
        Me.lblcoanum.Name = "lblcoanum"
        Me.lblcoanum.Size = New System.Drawing.Size(0, 14)
        Me.lblcoanum.TabIndex = 29
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 14)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "COA #:"
        '
        'txtrems
        '
        Me.txtrems.Location = New System.Drawing.Point(29, 90)
        Me.txtrems.Multiline = True
        Me.txtrems.Name = "txtrems"
        Me.txtrems.Size = New System.Drawing.Size(381, 131)
        Me.txtrems.TabIndex = 69
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 14)
        Me.Label5.TabIndex = 68
        Me.Label5.Text = "Reason:"
        '
        'btncancel
        '
        Me.btncancel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.Image = CType(resources.GetObject("btncancel.Image"), System.Drawing.Image)
        Me.btncancel.Location = New System.Drawing.Point(318, 242)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(92, 27)
        Me.btncancel.TabIndex = 71
        Me.btncancel.Text = "Cancel"
        Me.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = True
        '
        'btnok
        '
        Me.btnok.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnok.Image = CType(resources.GetObject("btnok.Image"), System.Drawing.Image)
        Me.btnok.Location = New System.Drawing.Point(220, 242)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(92, 27)
        Me.btnok.TabIndex = 70
        Me.btnok.Text = "Ok"
        Me.btnok.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnok.UseVisualStyleBackColor = True
        '
        'lblcoaid
        '
        Me.lblcoaid.AutoSize = True
        Me.lblcoaid.Location = New System.Drawing.Point(406, 21)
        Me.lblcoaid.Name = "lblcoaid"
        Me.lblcoaid.Size = New System.Drawing.Size(0, 14)
        Me.lblcoaid.TabIndex = 72
        Me.lblcoaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'list1
        '
        Me.list1.FormattingEnabled = True
        Me.list1.ItemHeight = 14
        Me.list1.Location = New System.Drawing.Point(289, 133)
        Me.list1.Name = "list1"
        Me.list1.Size = New System.Drawing.Size(120, 88)
        Me.list1.TabIndex = 93
        Me.list1.Visible = False
        '
        'coacancel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(443, 288)
        Me.ControlBox = False
        Me.Controls.Add(Me.list1)
        Me.Controls.Add(Me.lblcoaid)
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.txtrems)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblcoanum)
        Me.Controls.Add(Me.Label3)
        Me.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "coacancel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "COA Cancel"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblcoanum As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents txtrems As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblcoaid As System.Windows.Forms.Label
    Friend WithEvents list1 As System.Windows.Forms.ListBox
End Class
