Imports System.Data.Odbc

Public Class Login_Form

    Private Sub loginemailTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles loginemailTextBox.TextChanged
        ' Optional: You can add validation here if needed
    End Sub

    Private Sub loginpasswordTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles loginpasswordTextBox.TextChanged
        ' Optional: You can add validation here if needed
    End Sub

    Private Sub loginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles loginButton.Click
        ' Validate input fields
        If String.IsNullOrEmpty(loginemailTextBox.Text.Trim()) Then
            MessageBox.Show("Please enter your username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            loginemailTextBox.Focus()
            Return
        End If

        If String.IsNullOrEmpty(loginpasswordTextBox.Text) Then
            MessageBox.Show("Please enter your password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            loginpasswordTextBox.Focus()
            Return
        End If

        ' Authenticate user
        AuthenticateUser()
    End Sub

    Private Sub AuthenticateUser()
        Try
            ' Connect to database
            Connect_me()

            ' SQL query with parameters to prevent SQL injection
            Dim query As String = "SELECT user_id, username, role FROM user_tbl WHERE username = ? AND passwords = ?"

            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@username", loginemailTextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@password", loginpasswordTextBox.Text)

            Dim adapter As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ' Check if user exists
            If dt.Rows.Count > 0 Then
                ' Get user information
                Dim userId As Integer = Convert.ToInt32(dt.Rows(0)("user_id"))
                Dim username As String = dt.Rows(0)("username").ToString()
                Dim userRole As String = dt.Rows(0)("role").ToString()

                ' Store user information in global variables (optional - create a Module for this)
                ' Example: CurrentUser.UserId = userId
                ' Example: CurrentUser.Username = username
                ' Example: CurrentUser.Role = userRole

                MessageBox.Show("Login successful! Welcome " & username & ".", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Redirect based on role
                Select Case userRole.ToUpper()
                    Case "ADMIN"
                        Dim adminForm As New Admin_Form()
                        adminForm.Show()
                        Me.Hide()

                    Case "STUDENT"
                        Dim studentForm As New Student_Form()
                        studentForm.Show()
                        Me.Hide()

                    Case "TEACHER"
                        Dim teacherForm As New Teacher_Form()
                        teacherForm.Show()
                        Me.Hide()

                    Case Else
                        MessageBox.Show("Invalid user role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Select

            Else
                ' Invalid credentials
                MessageBox.Show("Invalid username or password.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                loginpasswordTextBox.Clear()
                loginpasswordTextBox.Focus()
            End If

        Catch ex As Exception
            MessageBox.Show("Login error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close connection
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        ' Optional: Add functionality for password reset or registration
        MessageBox.Show("Password reset functionality coming soon.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Login_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Optional: Clear textboxes on form load
        loginemailTextBox.Clear()
        loginpasswordTextBox.Clear()
        loginemailTextBox.Focus()

        ' Set password character
        loginpasswordTextBox.PasswordChar = "●"
    End Sub

    Private Sub Login_Form_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ' Close database connection if still open
        If con.State = ConnectionState.Open Then
            con.Close()
        End If

        ' Exit application completely
        Application.Exit()
    End Sub

End Class