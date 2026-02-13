Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports System.Text

Public Class Login_Form

    Public Shared id As String
    'refreshh logic
    Private Sub refreshme()
        loginemailTextBox.Text = ""
        loginpasswordTextBox.Text = ""
        loginemailTextBox.Focus()
    End Sub

    'hash password
    Public Function md5fromstring(ByVal source As String) As String
        Dim sb As New StringBuilder()
        Dim bytes() As Byte
        If String.IsNullOrEmpty(source) Then
            Throw New ArgumentNullException()
        End If
        bytes = Encoding.Default.GetBytes(source)
        bytes = MD5.Create().ComputeHash(bytes)
        For Each b As Byte In bytes
            sb.Append(b.ToString("x2"))
        Next
        Return sb.ToString()
    End Function

    Private Sub loginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles loginButton.Click
        Dim username As String = loginemailTextBox.Text.Trim()
        Dim password As String = loginpasswordTextBox.Text.Trim()
        ' Validate inputs
        If String.IsNullOrEmpty(loginemailTextBox.Text.Trim()) Then
            MessageBox.Show("Please enter your username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            loginemailTextBox.Focus()
            Return
        End If

        If String.IsNullOrEmpty(loginpasswordTextBox.Text.Trim()) Then
            MessageBox.Show("Please enter your password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            loginpasswordTextBox.Focus()
            Return
        End If

        ' Connect to database
        Connect_me()

        Try
            ' Query to check username, password, and get role
            Dim query As String = "SELECT acc_id, role, firstname, lastname FROM account WHERE email = ? AND pword = ?"
            Dim cmd As New OdbcCommand(query, con)

            ' Add parameters to prevent SQL injection
            cmd.Parameters.AddWithValue("email", username)
            cmd.Parameters.AddWithValue("pword", md5fromstring(password))


            Dim reader As OdbcDataReader = cmd.ExecuteReader()
            If reader.Read() Then

                ' Login successful - get all user info
                Dim userId As String = reader("acc_id").ToString().Trim()
                Dim userRole As String = reader("role").ToString().Trim().ToLower()
                Dim fname As String = reader("firstname").ToString().Trim()
                Dim lname As String = reader("lastname").ToString().Trim()
                Dim email As String = loginemailTextBox.Text.Trim()

                ' Store sa module - CORRECT WAY
                login_logic.userid = userId
                login_logic.loginuser = email
                login_logic.firstname = fname
                login_logic.lastname = lname
                login_logic.userrole = userRole
                id = userId ' Para sa compatibility with existing code

                reader.Close()

                ' Close login form
                Me.Hide()

                ' Redirect based on role
                Select Case userRole
                    Case "admin"
                        Dim adminForm As New Admin_Form()
                        adminForm.Show()
                        refreshme()

                    Case "student"
                        Dim studentForm As New Student_Form()
                        studentForm.Show()
                        refreshme()

                    Case "teacher", "professor"
                        Dim teacherForm As New Teacher_Form()
                        teacherForm.Show()
                        refreshme()
                    Case Else
                        MessageBox.Show("Unknown role: " & userRole & ". Please contact administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.Show()
                End Select

            Else
                ' Login failed
                reader.Close()
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                refreshme()
            End If

        Catch ex As Exception
            MessageBox.Show("Login error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Show()
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        MessageBox.Show("Please contact administrator for password reset.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Login_Form_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub

    Private Sub Login_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loginpasswordTextBox.PasswordChar = ChrW(&H25CF)  ' Sets the masking character to ●
    End Sub

    Private Sub showpass_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles showpass.CheckedChanged
        If showpass.Checked Then
            loginpasswordTextBox.PasswordChar = ChrW(0) ' set to show password
        Else
            loginpasswordTextBox.PasswordChar = ChrW(&H25CF) ' hide password
        End If
    End Sub
End Class