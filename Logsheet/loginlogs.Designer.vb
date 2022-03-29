<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class loginlogs
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(loginlogs))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.cmbgroup = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.dateto = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbuser = New System.Windows.Forms.ComboBox
        Me.datefrom = New System.Windows.Forms.DateTimePicker
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtip = New System.Windows.Forms.TextBox
        Me.txtcom = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.grdlogin = New System.Windows.Forms.DataGridView
        Me.btnsearch = New System.Windows.Forms.Button
        Me.cmbversion = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdlogin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbgroup
        '
        Me.cmbgroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbgroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbgroup.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmbgroup.FormattingEnabled = True
        Me.cmbgroup.Items.AddRange(New Object() {"", "Administrator", "Supervisor", "Manager", "Logistics Staff", "Whse Logistics Staff", "Whse Accounting Staff", "Encoder", "Checker", "Inspector", "Diesel Controller", "Guard"})
        Me.cmbgroup.Location = New System.Drawing.Point(449, 46)
        Me.cmbgroup.Name = "cmbgroup"
        Me.cmbgroup.Size = New System.Drawing.Size(183, 23)
        Me.cmbgroup.TabIndex = 35
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(446, 26)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 15)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "Department:"
        '
        'dateto
        '
        Me.dateto.CustomFormat = "yyyy/MM/dd"
        Me.dateto.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.dateto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dateto.Location = New System.Drawing.Point(129, 48)
        Me.dateto.Name = "dateto"
        Me.dateto.Size = New System.Drawing.Size(119, 21)
        Me.dateto.TabIndex = 33
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(100, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 15)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "To:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 15)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Date Login From:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(257, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 15)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "Username:"
        '
        'cmbuser
        '
        Me.cmbuser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbuser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbuser.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmbuser.FormattingEnabled = True
        Me.cmbuser.Location = New System.Drawing.Point(260, 46)
        Me.cmbuser.Name = "cmbuser"
        Me.cmbuser.Size = New System.Drawing.Size(175, 23)
        Me.cmbuser.Sorted = True
        Me.cmbuser.TabIndex = 34
        '
        'datefrom
        '
        Me.datefrom.CustomFormat = "yyyy/MM/dd"
        Me.datefrom.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.datefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.datefrom.Location = New System.Drawing.Point(129, 21)
        Me.datefrom.Name = "datefrom"
        Me.datefrom.Size = New System.Drawing.Size(119, 21)
        Me.datefrom.TabIndex = 32
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtip)
        Me.GroupBox1.Controls.Add(Me.txtcom)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.grdlogin)
        Me.GroupBox1.Controls.Add(Me.btnsearch)
        Me.GroupBox1.Controls.Add(Me.cmbversion)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.datefrom)
        Me.GroupBox1.Controls.Add(Me.cmbgroup)
        Me.GroupBox1.Controls.Add(Me.cmbuser)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.dateto)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1218, 550)
        Me.GroupBox1.TabIndex = 39
        Me.GroupBox1.TabStop = False
        '
        'txtip
        '
        Me.txtip.Location = New System.Drawing.Point(978, 46)
        Me.txtip.Name = "txtip"
        Me.txtip.Size = New System.Drawing.Size(117, 21)
        Me.txtip.TabIndex = 46
        '
        'txtcom
        '
        Me.txtcom.Location = New System.Drawing.Point(777, 46)
        Me.txtcom.Name = "txtcom"
        Me.txtcom.Size = New System.Drawing.Size(188, 21)
        Me.txtcom.TabIndex = 45
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(975, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 15)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "IP Address:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(774, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 15)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Computer Name:"
        '
        'grdlogin
        '
        Me.grdlogin.AllowUserToAddRows = False
        Me.grdlogin.AllowUserToDeleteRows = False
        Me.grdlogin.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.grdlogin.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdlogin.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdlogin.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.grdlogin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdlogin.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column4, Me.Column6, Me.Column3, Me.Column10, Me.Column5, Me.Column7, Me.Column8, Me.Column9, Me.Column11})
        Me.grdlogin.Location = New System.Drawing.Point(24, 83)
        Me.grdlogin.Name = "grdlogin"
        Me.grdlogin.ReadOnly = True
        Me.grdlogin.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.grdlogin.RowHeadersWidth = 10
        Me.grdlogin.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdlogin.Size = New System.Drawing.Size(1168, 461)
        Me.grdlogin.TabIndex = 42
        '
        'btnsearch
        '
        Me.btnsearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnsearch.Image = CType(resources.GetObject("btnsearch.Image"), System.Drawing.Image)
        Me.btnsearch.Location = New System.Drawing.Point(1113, 44)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(79, 23)
        Me.btnsearch.TabIndex = 41
        Me.btnsearch.Text = "Search"
        Me.btnsearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnsearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'cmbversion
        '
        Me.cmbversion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbversion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbversion.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.cmbversion.FormattingEnabled = True
        Me.cmbversion.Location = New System.Drawing.Point(646, 46)
        Me.cmbversion.Name = "cmbversion"
        Me.cmbversion.Size = New System.Drawing.Size(117, 23)
        Me.cmbversion.TabIndex = 39
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(643, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 15)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Version:"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Location = New System.Drawing.Point(2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1240, 573)
        Me.Panel1.TabIndex = 40
        '
        'Column1
        '
        Me.Column1.HeaderText = "systemid"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        '
        'Column2
        '
        Me.Column2.HeaderText = "Username"
        Me.Column2.MinimumWidth = 164
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 164
        '
        'Column4
        '
        Me.Column4.HeaderText = "Department"
        Me.Column4.MinimumWidth = 150
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 150
        '
        'Column6
        '
        DataGridViewCellStyle2.Format = "yyyy/MM/dd"
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column6.HeaderText = "Date Login"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Time Login"
        Me.Column3.MinimumWidth = 50
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 80
        '
        'Column10
        '
        DataGridViewCellStyle3.Format = "yyyy/MM/dd"
        Me.Column10.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column10.HeaderText = "Date Logout"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Time Logout"
        Me.Column5.MinimumWidth = 50
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 80
        '
        'Column7
        '
        Me.Column7.HeaderText = "Version"
        Me.Column7.MinimumWidth = 50
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 120
        '
        'Column8
        '
        Me.Column8.HeaderText = "Computer Name"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 180
        '
        'Column9
        '
        Me.Column9.HeaderText = "IP Address"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 180
        '
        'Column11
        '
        Me.Column11.HeaderText = "Branch"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        '
        'loginlogs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1244, 585)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "loginlogs"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login Logs"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdlogin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbgroup As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dateto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbuser As System.Windows.Forms.ComboBox
    Friend WithEvents datefrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbversion As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents grdlogin As System.Windows.Forms.DataGridView
    Friend WithEvents txtip As System.Windows.Forms.TextBox
    Friend WithEvents txtcom As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
