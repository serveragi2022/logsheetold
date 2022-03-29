Imports System.Data.OleDb
Imports System.IO
Imports System.Data.SqlClient
Imports System.Security
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Text

Public Class confirmsupprod
    Dim lines = System.IO.File.ReadAllLines(login.connectline)
    Dim strconn As String = lines(0)
    Dim sql As String
    Dim conn As SqlConnection
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand
    Public frm As Boolean = False

    Private Sub connect()
        conn = New SqlConnection
        conn.ConnectionString = strconn
        If conn.State <> ConnectionState.Open Then
            conn.Open()
        End If
    End Sub
    Private Sub disconnect()
        conn = New SqlConnection
        conn.ConnectionString = strconn
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Trim(txtuser.Text) = "" Then
                MsgBox("Input username.", MsgBoxStyle.Exclamation, "Authenticate")
                txtuser.Focus()
                Exit Sub
            ElseIf Trim(txtpass.Text) = "" Then
                MsgBox("Input password", MsgBoxStyle.Exclamation, "Authenticate")
                txtpass.Focus()
                Exit Sub
            Else
                sql = "SELECT * from tblusers where status='1' and username='" & Trim(txtuser.Text) & "' and (department='Production' or department='All' or department='Admin Dispatching')"
                connect()
                cmd = New SqlCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If (dr("workgroup").ToString.Contains("Administrator") = True And txtpass.Text = Decrypt(dr("password"))) Or (dr("workgroup") = "Manager" And txtpass.Text = Decrypt(dr("password"))) Or (dr("workgroup") = "Supervisor" And txtpass.Text = Decrypt(dr("password"))) Or (dr("department") = "Admin Dispatching" And txtpass.Text = Decrypt(dr("password"))) Then
                        ticketlines.tsupcnf = True
                        ticketlines.tsupby = dr("fullname")
                        tlogcancel.logcnlcnf = True
                        tlogcancel.logcnlby = dr("fullname")
                        Me.Close()
                        Exit Sub
                        'End If
                    End If
                End If
                MsgBox("Authentication failed! Invalid username / password", MsgBoxStyle.Critical, "")
                txtuser.Text = ""
                txtpass.Text = ""
                txtuser.Focus()
                dr.Close()
                cmd.Dispose()
                conn.Close()

            End If

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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Cursor = Cursors.Default
        ticketlines.tsupcnf = False
        ticketlines.tsupby = ""
        tlogcancel.logcnlcnf = False
        tlogcancel.logcnlby = ""
        Me.Close()
    End Sub

    Private Sub txtpass_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpass.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If

        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub confirm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        txtuser.Text = ""
        txtpass.Text = ""
        txtuser.Focus()
    End Sub

    Private Sub confirm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtuser.Text = ""
        txtpass.Text = ""
        txtuser.Focus()
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
        Dim keyBytes As Byte() = password.GetBytes(keySize / 8)

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

    Private Sub txtpass_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpass.TextChanged

    End Sub

    Private Sub txtuser_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtuser.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If

        If Asc(e.KeyChar) = Windows.Forms.Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub txtuser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtuser.TextChanged

    End Sub

    Private Sub chkshow_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkshow.CheckedChanged
        If chkshow.Checked = True Then
            txtpass.PasswordChar = ""
        Else
            txtpass.PasswordChar = "*"
        End If
    End Sub
End Class