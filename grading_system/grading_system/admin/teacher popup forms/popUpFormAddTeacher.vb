Imports System.Data.Odbc

Public Class popUpFormAddTeacher

#Region "Form Load"

    Private Sub popUpFormAddTeacher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Pre-populate Gender ComboBox
        Teacher_Gender_ComboBox.Items.Clear()
        Teacher_Gender_ComboBox.Items.Add("Male")
        Teacher_Gender_ComboBox.Items.Add("Female")
        Teacher_Gender_ComboBox.SelectedIndex = -1

        ' Make Gender ComboBox non-editable (only dropdown selection)
        Teacher_Gender_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

#End Region

#Region "Real-Time Validation - TextChanged / SelectedIndexChanged Events"

    Private Sub Teacher_Lastname_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Teacher_Lastname_TextBox.TextChanged
        If Teacher_Lastname_TextBox.Text.Trim() = "" Then
            Teacher_Lastname_TextBox.BackColor = System.Drawing.Color.MistyRose
        Else
            Teacher_Lastname_TextBox.BackColor = System.Drawing.Color.White
        End If
    End Sub

    Private Sub Teacher_Firstname_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Teacher_Firstname_TextBox.TextChanged
        If Teacher_Firstname_TextBox.Text.Trim() = "" Then
            Teacher_Firstname_TextBox.BackColor = System.Drawing.Color.MistyRose
        Else
            Teacher_Firstname_TextBox.BackColor = System.Drawing.Color.White
        End If
    End Sub

    Private Sub Teacher_Middlename_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Teacher_Middlename_TextBox.TextChanged
        ' Middle name is optional — no error highlight needed
        Teacher_Middlename_TextBox.BackColor = System.Drawing.Color.White
    End Sub

    Private Sub Teacher_Email_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Teacher_Email_TextBox.TextChanged
        Dim email As String = Teacher_Email_TextBox.Text.Trim()

        If email = "" OrElse Not IsValidEmail(email) Then
            Teacher_Email_TextBox.BackColor = System.Drawing.Color.MistyRose
        Else
            Teacher_Email_TextBox.BackColor = System.Drawing.Color.White
        End If
    End Sub

    Private Sub Teacher_Gender_ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Teacher_Gender_ComboBox.SelectedIndexChanged
        If Teacher_Gender_ComboBox.SelectedIndex = -1 Then
            Teacher_Gender_ComboBox.BackColor = System.Drawing.Color.MistyRose
        Else
            Teacher_Gender_ComboBox.BackColor = System.Drawing.Color.White
        End If
    End Sub

#End Region

#Region "Validation Helpers"

    ''' <summary>
    ''' Validates that an email ends with @gmail.com and has a non-empty local part.
    ''' </summary>
    Private Function IsValidEmail(ByVal email As String) As Boolean
        If email Is Nothing OrElse email.Trim() = "" Then Return False

        email = email.Trim().ToLower()

        ' Must end with @gmail.com
        If Not email.EndsWith("@gmail.com") Then Return False

        ' Must have something before the @ (at least 1 character)
        Dim atIndex As Integer = email.IndexOf("@")
        If atIndex <= 0 Then Return False

        Return True
    End Function

    ''' <summary>
    ''' Runs all field validations.
    ''' Highlights invalid fields and returns False if any fail.
    ''' </summary>
    Private Function ValidateAllFields() As Boolean
        Dim isValid As Boolean = True

        ' Last Name
        If Teacher_Lastname_TextBox.Text.Trim() = "" Then
            Teacher_Lastname_TextBox.BackColor = System.Drawing.Color.MistyRose
            isValid = False
        End If

        ' First Name
        If Teacher_Firstname_TextBox.Text.Trim() = "" Then
            Teacher_Firstname_TextBox.BackColor = System.Drawing.Color.MistyRose
            isValid = False
        End If

        ' Email
        Dim emailText As String = Teacher_Email_TextBox.Text.Trim()
        If emailText = "" Then
            Teacher_Email_TextBox.BackColor = System.Drawing.Color.MistyRose
            isValid = False
        ElseIf Not IsValidEmail(emailText) Then
            Teacher_Email_TextBox.BackColor = System.Drawing.Color.MistyRose
            isValid = False
        End If

        ' Gender
        If Teacher_Gender_ComboBox.SelectedIndex = -1 Then
            Teacher_Gender_ComboBox.BackColor = System.Drawing.Color.MistyRose
            isValid = False
        End If

        If Not isValid Then
            MessageBox.Show("Please fill in all required fields correctly." & vbCrLf & vbCrLf & _
                            "- Last Name, First Name are required." & vbCrLf & _
                            "- Email is required and must end with @gmail.com." & vbCrLf & _
                            "- Gender must be selected.", _
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        Return isValid
    End Function

    ''' <summary>
    ''' Checks if the email is already taken in the account table.
    ''' Returns True if a duplicate is found.
    ''' </summary>
    Private Function IsEmailDuplicate(ByVal email As String) As Boolean
        Try
            Connect_me()
            Dim query As String = "SELECT COUNT(*) FROM account WHERE LOWER(email) = LOWER(?)"
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@email", email.Trim())
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return count > 0
        Catch ex As Exception
            MessageBox.Show("Error checking email: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return True  ' Treat error as duplicate to prevent a bad insert
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Function

#End Region

#Region "Register Teacher Button"

    Private Sub Register_Teacher_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Register_Teacher_Button.Click

        ' ── Step 1: Validate all fields ──
        If Not ValidateAllFields() Then Return

        ' ── Step 2: Collect values ──
        Dim lastName As String = Teacher_Lastname_TextBox.Text.Trim()
        Dim firstName As String = Teacher_Firstname_TextBox.Text.Trim()
        Dim middleName As String = Teacher_Middlename_TextBox.Text.Trim()
        Dim email As String = Teacher_Email_TextBox.Text.Trim()
        Dim gender As String = Teacher_Gender_ComboBox.SelectedItem.ToString().ToLower()

        ' ── Step 3: Check for duplicate email ──
        If IsEmailDuplicate(email) Then
            Teacher_Email_TextBox.BackColor = System.Drawing.Color.MistyRose
            MessageBox.Show("This email address is already registered." & vbCrLf & _
                            "Please use a different email.", _
                            "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' ── Step 4: Hash the default password (12345) using Module_PasswordHelper ──
        Dim hashedPassword As String = HashPassword("12345")

        ' Verify the hash was created successfully
        If hashedPassword = "" Then
            MessageBox.Show("Error creating password hash. Please try again.", _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' ── Step 5: Insert into account table ──
        ' FIXED: Removed 'section' column - teachers don't have sections in account table
        ' The trigger (insert_prof_if_role_prof) will automatically create the prof record
        Try
            Connect_me()

            Dim insertQuery As String = _
                "INSERT INTO account " & _
                "(email, pword, role, firstname, middlename, lastname, gender) " & _
                "VALUES (?, ?, ?, ?, ?, ?, ?)"

            Dim cmd As New OdbcCommand(insertQuery, con)
            cmd.Parameters.AddWithValue("@email", email)
            cmd.Parameters.AddWithValue("@pword", hashedPassword)
            cmd.Parameters.AddWithValue("@role", "teacher")
            cmd.Parameters.AddWithValue("@firstname", firstName)
            cmd.Parameters.AddWithValue("@middlename", If(middleName = "", DBNull.Value, CObj(middleName)))
            cmd.Parameters.AddWithValue("@lastname", lastName)
            cmd.Parameters.AddWithValue("@gender", gender)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show("Teacher registered successfully!" & vbCrLf & vbCrLf & _
                                "Email: " & email & vbCrLf & _
                                "Default Password: 12345" & vbCrLf & vbCrLf & _
                                "The teacher can now log in and should change their password.", _
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Else
                MessageBox.Show("Registration failed. No rows were inserted." & vbCrLf & _
                                "Please try again.", _
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error registering teacher: " & ex.Message, _
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try

    End Sub

#End Region

#Region "Cancel Button"

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.Close()
    End Sub

#End Region

End Class