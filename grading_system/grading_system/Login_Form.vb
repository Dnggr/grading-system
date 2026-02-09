Imports System.Data.Odbc

Public Class Login_Form

    Public Shared id As String

    Private Sub loginemailTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles loginemailTextBox.TextChanged
        ' Optional: You can add validation here if needed
    End Sub

    Private Sub loginpasswordTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles loginpasswordTextBox.TextChanged
        ' Optional: You can add validation here if needed
    End Sub

    Private Sub loginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles loginButton.Click
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
            Dim query As String = "SELECT acc_id, role FROM account WHERE username = ? AND password = ?"
            Dim cmd As New OdbcCommand(query, con)

            ' Add parameters to prevent SQL injection
            cmd.Parameters.AddWithValue("@username", loginemailTextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@password", loginpasswordTextBox.Text.Trim())

            Dim reader As OdbcDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                ' Login successful - get the role
                Dim userRole As String = reader("role").ToString().Trim().ToLower()
                id = reader("acc_id").ToString.Trim()
                reader.Close()

                ' Close login form
                Me.Hide()

                ' Redirect based on role
                Select Case userRole
                    Case "admin"
                        Dim adminForm As New Admin_Form()
                        adminForm.Show()

                    Case "student"
                        Dim studentForm As New Student_Form()
                        studentForm.Show()

                    Case "teacher", "professor"
                        Dim teacherForm As New Teacher_Form()
                        teacherForm.Show()

                    Case Else
                        MessageBox.Show("Unknown role: " & userRole & ". Please contact administrator.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.Show()
                End Select

            Else
                ' Login failed
                reader.Close()
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                loginpasswordTextBox.Clear()
                loginemailTextBox.Focus()
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
        ' Optional: Add functionality for "Forgot Password" or "Register" link
        MessageBox.Show("Please contact administrator for password reset.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Login_Form_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ' Close the entire application when login form is closed
        Application.Exit()
    End Sub

    Private Sub Login_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class