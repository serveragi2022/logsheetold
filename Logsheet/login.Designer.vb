<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class login
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(login))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cmbbr = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblver = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnlogin = New System.Windows.Forms.Button()
        Me.txtpass = New System.Windows.Forms.TextBox()
        Me.txtusername = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.chkshow = New System.Windows.Forms.CheckBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'cmbbr
        '
        Me.cmbbr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbbr.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbbr.FormattingEnabled = True
        Me.cmbbr.Items.AddRange(New Object() {"", "Agi-Calamba", "Agi-Calaca"})
        Me.cmbbr.Location = New System.Drawing.Point(242, 120)
        Me.cmbbr.Name = "cmbbr"
        Me.cmbbr.Size = New System.Drawing.Size(143, 21)
        Me.cmbbr.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(322, 200)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 12)
        Me.Label8.TabIndex = 44
        Me.Label8.Text = "Version:"
        '
        'lblver
        '
        Me.lblver.AutoSize = True
        Me.lblver.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblver.Location = New System.Drawing.Point(366, 200)
        Me.lblver.Name = "lblver"
        Me.lblver.Size = New System.Drawing.Size(26, 12)
        Me.lblver.TabIndex = 43
        Me.lblver.Text = "3.1.6"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(257, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 13)
        Me.Label4.TabIndex = 40
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(215, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 15)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "Date:"
        '
        'btnlogin
        '
        Me.btnlogin.BackColor = System.Drawing.Color.Gainsboro
        Me.btnlogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnlogin.Image = CType(resources.GetObject("btnlogin.Image"), System.Drawing.Image)
        Me.btnlogin.Location = New System.Drawing.Point(218, 160)
        Me.btnlogin.Name = "btnlogin"
        Me.btnlogin.Size = New System.Drawing.Size(172, 28)
        Me.btnlogin.TabIndex = 4
        Me.btnlogin.Text = "Login"
        Me.btnlogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnlogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnlogin.UseVisualStyleBackColor = False
        '
        'txtpass
        '
        Me.txtpass.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtpass.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpass.Location = New System.Drawing.Point(245, 86)
        Me.txtpass.Name = "txtpass"
        Me.txtpass.Size = New System.Drawing.Size(114, 14)
        Me.txtpass.TabIndex = 2
        Me.txtpass.Text = "Password"
        '
        'txtusername
        '
        Me.txtusername.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtusername.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtusername.Location = New System.Drawing.Point(245, 51)
        Me.txtusername.Name = "txtusername"
        Me.txtusername.Size = New System.Drawing.Size(139, 14)
        Me.txtusername.TabIndex = 1
        Me.txtusername.Text = "Username"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 38)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(19, 23)
        Me.Button1.TabIndex = 32
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Timer2
        '
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(365, 83)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(20, 23)
        Me.PictureBox1.TabIndex = 48
        Me.PictureBox1.TabStop = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "hide.ico")
        Me.ImageList1.Images.SetKeyName(1, "show.ico")
        '
        'chkshow
        '
        Me.chkshow.AutoSize = True
        Me.chkshow.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkshow.Location = New System.Drawing.Point(12, 160)
        Me.chkshow.Name = "chkshow"
        Me.chkshow.Size = New System.Drawing.Size(102, 17)
        Me.chkshow.TabIndex = 49
        Me.chkshow.Text = "Show Password"
        Me.chkshow.UseVisualStyleBackColor = True
        Me.chkshow.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(13, 123)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 50
        Me.TextBox1.Visible = False
        '
        'login
        '
        Me.AcceptButton = Me.btnlogin
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(402, 218)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.chkshow)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmbbr)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtusername)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtpass)
        Me.Controls.Add(Me.lblver)
        Me.Controls.Add(Me.btnlogin)
        Me.Controls.Add(Me.Label4)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ticket Logsheet System"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnlogin As System.Windows.Forms.Button
    Friend WithEvents txtpass As System.Windows.Forms.TextBox
    Friend WithEvents txtusername As System.Windows.Forms.TextBox
    Friend WithEvents lblver As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents cmbbr As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents chkshow As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox

End Class
