<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.btnok = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtfrom = New System.Windows.Forms.TextBox()
        Me.txtto = New System.Windows.Forms.TextBox()
        Me.list1 = New System.Windows.Forms.ListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtoflogid = New System.Windows.Forms.TextBox()
        Me.txtlogsheet = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtxtype = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtpallet = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtticket = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnok
        '
        Me.btnok.Location = New System.Drawing.Point(14, 225)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(136, 24)
        Me.btnok.TabIndex = 0
        Me.btnok.Text = "Button1"
        Me.btnok.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "From:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "To:"
        '
        'txtfrom
        '
        Me.txtfrom.Location = New System.Drawing.Point(50, 12)
        Me.txtfrom.Name = "txtfrom"
        Me.txtfrom.Size = New System.Drawing.Size(100, 20)
        Me.txtfrom.TabIndex = 3
        '
        'txtto
        '
        Me.txtto.Location = New System.Drawing.Point(50, 45)
        Me.txtto.Name = "txtto"
        Me.txtto.Size = New System.Drawing.Size(100, 20)
        Me.txtto.TabIndex = 4
        '
        'list1
        '
        Me.list1.FormattingEnabled = True
        Me.list1.Location = New System.Drawing.Point(14, 71)
        Me.list1.Name = "list1"
        Me.list1.Size = New System.Drawing.Size(136, 147)
        Me.list1.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(176, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "oflogid:"
        '
        'txtoflogid
        '
        Me.txtoflogid.Location = New System.Drawing.Point(223, 12)
        Me.txtoflogid.Name = "txtoflogid"
        Me.txtoflogid.Size = New System.Drawing.Size(100, 20)
        Me.txtoflogid.TabIndex = 7
        '
        'txtlogsheet
        '
        Me.txtlogsheet.Location = New System.Drawing.Point(223, 45)
        Me.txtlogsheet.Name = "txtlogsheet"
        Me.txtlogsheet.Size = New System.Drawing.Size(100, 20)
        Me.txtlogsheet.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(176, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "logsheet:"
        '
        'txtxtype
        '
        Me.txtxtype.Location = New System.Drawing.Point(223, 111)
        Me.txtxtype.Name = "txtxtype"
        Me.txtxtype.Size = New System.Drawing.Size(100, 20)
        Me.txtxtype.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(176, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "tickettype:"
        '
        'txtpallet
        '
        Me.txtpallet.Location = New System.Drawing.Point(223, 78)
        Me.txtpallet.Name = "txtpallet"
        Me.txtpallet.Size = New System.Drawing.Size(100, 20)
        Me.txtpallet.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(176, 81)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "palletnum:"
        '
        'txtticket
        '
        Me.txtticket.Location = New System.Drawing.Point(223, 146)
        Me.txtticket.Name = "txtticket"
        Me.txtticket.Size = New System.Drawing.Size(100, 20)
        Me.txtticket.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(176, 149)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "gticketnum"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(349, 261)
        Me.Controls.Add(Me.txtticket)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtxtype)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtpallet)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtlogsheet)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtoflogid)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.list1)
        Me.Controls.Add(Me.txtto)
        Me.Controls.Add(Me.txtfrom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnok)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnok As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtfrom As TextBox
    Friend WithEvents txtto As TextBox
    Friend WithEvents list1 As ListBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtoflogid As TextBox
    Friend WithEvents txtlogsheet As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtxtype As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtpallet As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtticket As TextBox
    Friend WithEvents Label7 As Label
End Class
