Imports System.Data.OleDb
Imports System.IO
Imports System.Data.SqlClient
Imports System.Security
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Text

Public Class login
    Dim lines1 = System.IO.File.ReadAllLines("connectionstring.txt")  'Agi-Calamba
    Dim strconn1 As String = lines1(0) 'Agi-Calamba   
    Dim lines2 = System.IO.File.ReadAllLines("cnstringcalaca.txt") 'Agi-Calaca
    Dim strconn2 As String = lines2(0) 'Agi-Calaca

    Dim sql As String
    Dim conn As SqlConnection
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand

    Dim a As String
    Public wgroup As String, user As String, fullneym As String, userid As Integer, shift As String, depart As String, branch As String
    Public bypass As Boolean = False, connectline As String = "", svrcal As Boolean, svrclc As Boolean
    Dim cutoff As Boolean = False
    Dim version As String
    Public strHostName As String
    Public logshift As String, logwhse As String

    Dim dirpath As String = My.Application.Info.DirectoryPath
    Dim wclient As New System.Net.WebClient
    Dim tool As String = dirpath + "\autoupdater.exe"
    Public datenow As Date

    Private Sub connect()
        conn = New SqlConnection 'New OleDb.OleDbConnection
        If cmbbr.SelectedItem = "Agi-Calamba" Then
            conn.ConnectionString = strconn1
            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If
            connectline = "connectionstring.txt"

        ElseIf cmbbr.SelectedItem = "Agi-Calaca" Then
            conn.ConnectionString = strconn2
            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If
            connectline = "cnstringcalaca.txt"
        End If
    End Sub

    Private Sub disconnect()
        conn = New SqlConnection 'New OleDb.OleDbConnection
        If cmbbr.SelectedItem = "Agi-Calamba" Then
            conn.ConnectionString = strconn1
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        ElseIf cmbbr.SelectedItem = "Agi-Calaca" Then
            conn.ConnectionString = strconn2
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End If
    End Sub

    Private Sub login_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub

    Private Sub login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Start()
        txtusername.Focus()
        version = lblver.Text
        datenow = Format(Date.Now, "MM/dd/yyyy hh:mm:ss tt")
        cmbbr.SelectedIndex = 2
    End Sub

    Private Sub login_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If System.IO.File.Exists(dirpath & "\autoupdater.exe") Then
                System.IO.File.Delete(dirpath & "\autoupdater.exe")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub downloadapp()
        Try
            sql = "SELECT Top 1 updater FROM tblversion where updater is not null order by versionid DESC"
            connect()
            cmd = New SqlCommand(sql, conn)
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            If rdr.Read Then
                WriteWordDoc(tool, rdr("updater"))
            Else
                MsgBox(tool & " not found")
                Exit Sub
            End If
            rdr.Close()
            conn.Close()

            Threading.Thread.Sleep(2000) 'freeze thread for 5 seconds...
            Timer2.Start()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "")
        End Try
    End Sub

    Private Sub WriteWordDoc(ByVal filename As String, ByVal data As Byte())
        Dim fs As New System.IO.FileStream(filename, IO.FileMode.Create)
        Dim bw As New System.IO.BinaryWriter(fs)
        bw.Write(data)
        bw.Close()
        fs.Close()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Process.Start(dirpath + "\autoupdater.exe")
        Application.Exit()
    End Sub

    Private Sub btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        Try
            Try
                If System.IO.File.Exists(dirpath & "\autoupdater.exe") Then
                    System.IO.File.Delete(dirpath & "\autoupdater.exe")
                End If
            Catch ex As Exception
                Exit Sub
            End Try

            If Trim(txtusername.Text) = "" Or Trim(txtpass.Text) = "" Or cmbbr.SelectedItem = "" Then
                MsgBox("Incomplete Fields.", MsgBoxStyle.Exclamation, "Login")
                txtusername.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            Else
                'check if updated ung app
                sql = "Select * from tblversion where version='" & version & "' and status='1'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then

                Else
                    Dim aa As String = MsgBox("Please update the ticket log sheet system to the latest version. Click Ok to continue.", MsgBoxStyle.Information + MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton1, "")
                    If aa = vbOK Then
                        downloadapp()
                    End If
                    Exit Sub
                End If
                dr.Dispose()
                cmd.Dispose()
                conn.Close()


                sql = "SELECT * from tblusers where username='" & txtusername.Text & "' and password='" & Encrypt(txtpass.Text) & "'"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If dr("status") = 0 Then
                        MsgBox("Account Deactivated", MsgBoxStyle.Exclamation, "Status")
                        txtusername.Focus()
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If

                    If (dr("workgroup").ToString = "Administrator" Or dr("workgroup").ToString = "Manager" Or dr("workgroup").ToString = "Supervisor") Then
                        savelogin()
                        wgroup = dr("workgroup")
                        user = txtusername.Text
                        fullneym = dr("fullname")
                        userid = dr("userid")
                        depart = dr("department")
                        branch = dr("branch").ToString
                        txtusername.Focus()
                        txtusername.Text = "Username"
                        txtpass.Text = "Password"
                        txtpass.PasswordChar = ""
                        Me.Cursor = Cursors.Default

                        '/logshift = dr("shift")
                        logwhse = dr("whsename")
                        If depart = "All" Then
                            mdiform.AdministratorToolStripMenuItem.Visible = True
                            mdiform.COAFormatParametersToolStripMenuItem.Visible = True
                        Else
                            mdiform.AdministratorToolStripMenuItem.Visible = False
                            mdiform.COAFormatParametersToolStripMenuItem.Visible = False
                        End If

                        If branch = "All" Then
                            '/branchlist.Show()
                            checkdatetime()
                            Me.Hide()
                            branch = cmbbr.SelectedItem
                            mdiform.Text = mdiform.Text & " (" & branch & " - " & Me.user & ")"
                            viewbranch()
                            mdiform.Show()
                        ElseIf branch = cmbbr.SelectedItem Then
                            checkdatetime()
                            Me.Hide()
                            mdiform.Text = mdiform.Text & " (" & branch & " - " & Me.user & ")"
                            viewbranch()
                            mdiform.Show()
                        ElseIf branch <> cmbbr.SelectedItem Then
                            MsgBox("Access denied", MsgBoxStyle.Critical, "")
                        End If

                        Exit Sub

                    Else
                        savelogin()
                        wgroup = dr("workgroup")
                        user = txtusername.Text
                        fullneym = dr("fullname")
                        userid = dr("userid")
                        depart = dr("department")
                        branch = dr("branch").ToString
                        txtusername.Focus()
                        txtusername.Text = "Username"
                        txtpass.Text = "Password"
                        txtpass.PasswordChar = ""
                        Me.Cursor = Cursors.Default
                        mdiform.AdministratorToolStripMenuItem.Visible = False
                        mdiform.COAFormatParametersToolStripMenuItem.Visible = False

                        '/logshift = dr("shift")
                        logwhse = dr("whsename")

                        If branch = "All" Then
                            '/branchlist.Show()
                            checkdatetime()
                            Me.Hide()
                            branch = cmbbr.SelectedItem
                            mdiform.Text = mdiform.Text & " (" & branch & " - " & Me.user & ")"
                            viewbranch()
                            mdiform.Show()
                        ElseIf branch = cmbbr.SelectedItem Then
                            checkdatetime()
                            Me.Hide()
                            mdiform.Text = mdiform.Text & " (" & branch & " - " & Me.user & ")"
                            viewbranch()
                            mdiform.Show()
                        ElseIf branch <> cmbbr.SelectedItem Then
                            MsgBox("Access denied", MsgBoxStyle.Critical, "")
                        End If

                        Exit Sub
                    End If

                Else
                    Me.Cursor = Cursors.Default
                    MsgBox("Invalid Username or Password", MsgBoxStyle.Critical, "Invalid")
                    'txtusername.Text = ""
                    'txtpass.Text = ""
                    txtusername.Focus()
                    Exit Sub
                End If
                dr.Close()
                cmd.Dispose()
                conn.Close()


                'check kung sya ay nasa user schedule at kung may schedule sya dis day kung hindi sup manager or administrator sa workgroup
                '/sql = "Select * from tblsched where userid='" & userid & "' and status='1' order by schedid DESC"
                '/connect()
                '/cmd = New SqlCommand(sql, conn)
                '/dr = cmd.ExecuteReader
                '/While dr.Read
                '/MsgBox(dr("schedid").ToString & " " & CDate(Format(dr("datefrom"), "yyyy/MM/dd")) & " " & CDate(Format(Date.Now, "yyyy/MM/dd")) & " " & CDate(Format(dr("dateto"), "yyyy/MM/dd")))
                '/If CDate(Format(dr("datefrom"), "yyyy/MM/dd")) <= CDate(Format(Date.Now, "yyyy/MM/dd")) And CDate(Format(dr("dateto"), "yyyy/MM/dd")) >= CDate(Format(Date.Now, "yyyy/MM/dd")) Then
                '/logshift = dr("shift")
                '/logwhse = dr("whsename")
                '/Me.Hide()
                '/mdiform.Text = mdiform.Text & " (Whse:" & logwhse & " - Shift:" & logshift & " - " & Me.user & ")"
                '/mdiform.Show()
                '/Exit Sub
                '/End If
                '/End While
                '/dr.Dispose()
                '/cmd.Dispose()
                '/conn.Close()

                '/MsgBox("You don't have schedule for today.", MsgBoxStyle.Information, "")
            End If


            'Catch ex As System.Data.SqlClient.SqlException
            'Me.Cursor = Cursors.Default
            'MsgBox("The server was not found or was not accessible.", MsgBoxStyle.Critical, "Server Error")
        Catch ex As System.FormatException
            Me.Cursor = Cursors.Default
            MsgBox("Invalid Username or Password", MsgBoxStyle.Critical, "Invalid")
            'txtusername.Text = ""
            'txtpass.Text = ""
            txtusername.Focus()
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub txtusername_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtusername.KeyPress
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnlogin.PerformClick()
        ElseIf Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtpass_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpass.KeyPress
        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            btnlogin.PerformClick()
        ElseIf Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Public Function Decrypt(ByVal cipherText As String) As String
        Dim passPhrase As String = "minePassPhrase"
        Dim saltValue As String = "mySaltValue"
        Dim hashAlgorithm As String = "SHA1"

        Dim passwordIterations As Integer = 2
        Dim initVector As String = "@1B2c3D4e5F6g7H8"
        Dim keySize As Integer = 256
        ' Convert strings defining encryption key characteristics into byte
        ' arrays. Let us assume that strings only contain ASCII codes.
        ' If strings include Unicode characters, use Unicode, UTF7, or UTF8
        ' encoding.
        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

        ' Convert our ciphertext into a byte array.
        Dim cipherTextBytes As Byte() = Convert.FromBase64String(cipherText)

        ' First, we must create a password, from which the key will be 
        ' derived. This password will be generated from the specified 
        ' passphrase and salt value. The password will be created using
        ' the specified hash algorithm. Password creation can be done in
        ' several iterations.
        Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)

        ' Use the password to generate pseudo-random bytes for the encryption
        ' key. Specify the size of the key in bytes (instead of bits).
        Dim keyBytes As Byte() = password.GetBytes(keySize \ 8)

        ' Create uninitialized Rijndael encryption object.
        Dim symmetricKey As New RijndaelManaged()

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC

        ' Generate decryptor from the existing key bytes and initialization 
        ' vector. Key size will be defined based on the number of the key 
        ' bytes.
        Dim decryptor As ICryptoTransform = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As New MemoryStream(cipherTextBytes)

        ' Define cryptographic stream (always use Read mode for encryption).
        Dim cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)

        ' Since at this point we don't know what the size of decrypted data
        ' will be, allocate the buffer long enough to hold ciphertext;
        ' plaintext is never longer than ciphertext.
        Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}

        ' Start decrypting.
        Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert decrypted data into a string. 
        ' Let us assume that the original plaintext string was UTF8-encoded.
        Dim plainText As String = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)

        ' Return decrypted string.   
        Return plainText
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        datenow = datenow.AddSeconds(+1)
        Label4.Text = datenow
    End Sub

    Public Function Encrypt(ByVal plainText As String) As String

        Dim passPhrase As String = "minePassPhrase"
        Dim saltValue As String = "mySaltValue"
        Dim hashAlgorithm As String = "SHA1"

        Dim passwordIterations As Integer = 2
        Dim initVector As String = "@1B2c3D4e5F6g7H8"
        Dim keySize As Integer = 256

        Dim initVectorBytes As Byte() = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte() = Encoding.ASCII.GetBytes(saltValue)

        Dim plainTextBytes As Byte() = Encoding.UTF8.GetBytes(plainText)


        Dim password As New PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)

        Dim keyBytes As Byte() = password.GetBytes(keySize \ 8)
        Dim symmetricKey As New RijndaelManaged()

        symmetricKey.Mode = CipherMode.CBC

        Dim encryptor As ICryptoTransform = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

        Dim memoryStream As New MemoryStream()
        Dim cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)

        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
        cryptoStream.FlushFinalBlock()
        Dim cipherTextBytes As Byte() = memoryStream.ToArray()
        memoryStream.Close()
        cryptoStream.Close()
        Dim cipherText As String = Convert.ToBase64String(cipherTextBytes)
        Return cipherText
    End Function

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        bypass = False
        confirm.ShowDialog()
        If bypass = True Then
            '/password.ShowDialog()
        End If
    End Sub

    Public Sub savelogin()
        Try
            GetIPv4Address()
            strHostName = System.Net.Dns.GetHostName()

            sql = "Insert into tbllogin (username, login, logout, datelogin, pcname, ipaddress, version, branch) values ('" & Trim(txtusername.Text) & "', '" & Format(Date.Now, "HH:mm") & "', '', GetDate(), '" & strHostName.ToString & "', '" & GetIPv4Address.ToString & "', '" & lblver.Text & "', '" & branch & "')"
            connect()
            Dim cmd1 As SqlCommand = New SqlCommand(sql, conn)
            cmd1.ExecuteNonQuery()
            cmd1.Dispose()
            conn.Close()

        Catch ex As System.Data.SqlClient.SqlException
            Me.Cursor = Cursors.Default
            MsgBox("The server was not found or was not accessible.", MsgBoxStyle.Critical, "Server Error")
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub savelogout()
        Try
            GetIPv4Address()
            strHostName = System.Net.Dns.GetHostName()

            sql = "Update tbllogin set logout='" & Format(Date.Now, "HH:mm") & "', datelogout=GetDate(), branch='" & branch & "' where loginid=(Select TOP 1 loginid from tbllogin where username='" & user & "' and pcname='" & strHostName.ToString & "' and version='" & lblver.Text & "' order by loginid DESC)"
            connect()
            Dim cmd1 As SqlCommand = New SqlCommand(sql, conn)
            cmd1.ExecuteNonQuery()
            cmd1.Dispose()
            conn.Close()

            branch = ""

        Catch ex As System.Data.SqlClient.SqlException
            Me.Cursor = Cursors.Default
            MsgBox("The server was not found or was not accessible.", MsgBoxStyle.Critical, "Server Error")
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Function GetIPv4Address() As String
        GetIPv4Address = String.Empty
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                GetIPv4Address = ipheal.ToString()
            End If
        Next

    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'GetIPv4Address()
        'MsgBox(GetIPv4Address)
        TextBox1.Text = Encrypt("desagun")
        'MsgBox(strHostName)
    End Sub

    Private Sub txtusername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtusername.TextChanged
        If Trim(txtusername.Text) = "Username" Then
            txtusername.ForeColor = Color.Silver
        Else
            txtusername.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If IO.File.Exists(Application.StartupPath() & "\RS232Capture.txt") Then
            MsgBox(Application.StartupPath())
        End If

        Exit Sub
        If Date.Now >= Convert.ToDateTime(Date.Now.ToShortDateString & " " & #12:00:01 AM#) Then
            If Date.Now < Convert.ToDateTime(Date.Now.ToShortDateString & " " & #6:00:00 AM#) Then
                MsgBox("adjust to yesterday's date")
            Else
                MsgBox("todays date")
            End If
        Else
            MsgBox("today")
        End If
    End Sub

    Public Sub checkdatetime()
        Try
            sql = "Select GETDATE()"
            connect()
            cmd = New SqlCommand(sql, conn)
            datenow = cmd.ExecuteScalar
            cmd.Dispose()
            conn.Close()

            datenow = Format(datenow, "MM/dd/yyyy hh:mm:ss tt")

        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Public Sub viewbranch()
        Try
            receivenew.cmbbr.Items.Clear()

            sql = "Select branch from tblbranch where status='1' and branch<>'" & Me.branch & "' order by branch"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                receivenew.cmbbr.Items.Add(dr("branch").ToString.ToUpper)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            receivenew.cmbbr.SelectedIndex = 0

            receivenew.cmbwhse.Items.Clear()

            sql = "Select whsename from tblwhse where status='1' and branch='" & Me.branch & "' order by whsename"
            connect()
            cmd = New SqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            While dr.Read
                receivenew.cmbwhse.Items.Add(dr("whsename").ToString.ToUpper)
            End While
            dr.Dispose()
            cmd.Dispose()
            conn.Close()

            Me.Cursor = Cursors.Default
        Catch ex As System.InvalidOperationException
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString, MsgBoxStyle.Information)
        Finally
            disconnect()
        End Try
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        If chkshow.Checked = True Then
            chkshow.Checked = False
        Else
            chkshow.Checked = True
        End If
    End Sub

    Private Sub txtpass_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpass.TextChanged
        If Trim(txtpass.Text) = "Password" Then
            txtpass.ForeColor = Color.Silver
        Else
            txtpass.ForeColor = Color.Black
        End If
    End Sub

    Private Sub chkshow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkshow.CheckedChanged
        If chkshow.Checked = True Then
            PictureBox1.BackgroundImage = ImageList1.Images(1)
            PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
            txtpass.PasswordChar = ""
        Else
            PictureBox1.BackgroundImage = ImageList1.Images(0)
            PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
            If Trim(txtpass.Text) <> "Password" Then
                txtpass.PasswordChar = "*"
            End If
        End If
    End Sub

    Private Sub txtusername_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtusername.GotFocus
        If Trim(txtusername.Text) = "Username" Then
            txtusername.Text = ""
        End If
    End Sub

    Private Sub txtusername_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtusername.LostFocus
        If Trim(txtusername.Text) = "" Then
            txtusername.Text = "Username"
        End If
    End Sub

    Private Sub txtpass_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtpass.GotFocus
        If Trim(txtpass.Text) = "Password" Then
            txtpass.Text = ""
            If chkshow.Checked = False Then
                txtpass.PasswordChar = "*"
            Else
                txtpass.PasswordChar = ""
            End If
        End If
    End Sub

    Private Sub txtpass_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtpass.LostFocus
        If Trim(txtpass.Text) = "" Then
            txtpass.Text = "Password"
            txtpass.PasswordChar = ""
        End If
    End Sub
End Class
