Imports System.Data.Odbc

Public Class Login_Form

    Public Shared id As String

    ' Refresh UI fields
    Private Sub refreshme()
        loginemailTextBox.Text = ""
        loginpasswordTextBox.Text = ""
        loginemailTextBox.Focus()
    End Sub

    Private Sub form_keydown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            loginButton.PerformClick()
        End If
    End Sub

    Private Sub loginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles loginButton.Click
        Dim username As String = loginemailTextBox.Text.Trim()
        Dim password As String = loginpasswordTextBox.Text.Trim()

        ' --- Input Validation ---
        If String.IsNullOrEmpty(username) Then
            MessageBox.Show("Please enter your username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            loginemailTextBox.Focus()
            Return
        End If

        If String.IsNullOrEmpty(password) Then
            MessageBox.Show("Please enter your password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            loginpasswordTextBox.Focus()
            Return
        End If

        ' --- Hash password using the shared PasswordHelper module ---
        ' DB column `pword` stores MD5 hashes (e.g. 827ccb0eea8a706c4c34a16891f84e7b = "12345")
        Dim hashedPassword As String = HashPassword(password)

        If String.IsNullOrEmpty(hashedPassword) Then
            MessageBox.Show("Password hashing failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' --- Connect to database ---
        Connect_me()

        Try
            ' FIX: Column is `pword` in the `account` table, NOT `password`
            Dim query As String = _
         "SELECT a.acc_id, a.role, a.firstname, a.lastname, " & _
         "s.section_id, sec.course_id, sec.year_lvl " & _
         "FROM account a " & _
         "LEFT JOIN student s ON a.acc_id = s.acc_id " & _
         "LEFT JOIN section sec ON s.section_id = sec.section_id " & _
         "WHERE a.email = ? AND a.pword = ?"
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("email", username)
            cmd.Parameters.AddWithValue("pword", hashedPassword)

            Dim reader As OdbcDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                ' --- Login successful ---
                Dim userId As String = reader("acc_id").ToString().Trim()
                Dim userRole As String = reader("role").ToString().Trim().ToLower()
                'for the student log in logic wag tanggalin 
                If Not IsDBNull(reader("section_id")) Then
                    login_logic.secid = Convert.ToInt32(reader("section_id"))
                End If

                If Not IsDBNull(reader("course_id")) Then
                    login_logic.Courseid = Convert.ToInt32(reader("course_id"))
                End If

                If Not IsDBNull(reader("year_lvl")) Then
                    login_logic.yearlvl = Convert.ToInt32(reader("year_lvl"))
                End If


                ' firstname and lastname may be NULL for admin/default accounts — guard with DBNull check
                Dim fname As String = ""
                Dim lname As String = ""

                If Not IsDBNull(reader("firstname")) Then
                    fname = reader("firstname").ToString().Trim()
                End If

                If Not IsDBNull(reader("lastname")) Then
                    lname = reader("lastname").ToString().Trim()
                End If

                ' Store in shared login module
                login_logic.userid = userId
                login_logic.loginuser = username
                login_logic.firstname = fname
                login_logic.lastname = lname
                login_logic.userrole = userRole
                id = userId

                reader.Close()
                Me.Hide()

                ' --- Redirect based on role ---
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
                        MessageBox.Show("Unknown role: '" & userRole & "'. Please contact the administrator.", _
                                        "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.Show()
                End Select

            Else
                reader.Close()
                MessageBox.Show("Invalid username or password. Please try again.", _
                                "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        MessageBox.Show("Please contact your administrator for a password reset.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Login_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        loginpasswordTextBox.PasswordChar = ChrW(&H25CF)
    End Sub

    Private Sub Login_Form_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub

    Private Sub showpass_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles showpass.CheckedChanged
        If showpass.Checked Then
            loginpasswordTextBox.PasswordChar = ChrW(0)
        Else
            loginpasswordTextBox.PasswordChar = ChrW(&H25CF)
        End If
    End Sub

End Class