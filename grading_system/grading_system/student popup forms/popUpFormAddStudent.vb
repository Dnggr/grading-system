Imports System.Data.Odbc

Public Class popUpFormAddStudent

#Region "Form Load - Initialize ComboBoxes"
    Private Sub popUpFormAddStudent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Initialize Gender ComboBox
        InitializeGenderComboBox()

        ' Initialize Course ComboBox
        InitializeCourseComboBox()

        ' Initialize Year Level ComboBox
        InitializeYearLevelComboBox()
    End Sub

    ''' <summary>
    ''' Initialize Gender ComboBox with options
    ''' </summary>
    Private Sub InitializeGenderComboBox()
        Student_Gender_ComboBox.Items.Clear()
        Student_Gender_ComboBox.Items.Add("Male")
        Student_Gender_ComboBox.Items.Add("Female")
        Student_Gender_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    ''' <summary>
    ''' Initialize Course ComboBox with 15 college courses
    ''' </summary>
    Private Sub InitializeCourseComboBox()
        Student_Course_ComboBox.Items.Clear()
        Student_Course_ComboBox.Items.Add("BSIT")
        Student_Course_ComboBox.Items.Add("BSCS")
        Student_Course_ComboBox.Items.Add("BSA")
        Student_Course_ComboBox.Items.Add("BSBA")
        Student_Course_ComboBox.Items.Add("BSED")
        Student_Course_ComboBox.Items.Add("BEED")
        Student_Course_ComboBox.Items.Add("BSN")
        Student_Course_ComboBox.Items.Add("BSECE")
        Student_Course_ComboBox.Items.Add("BSCE")
        Student_Course_ComboBox.Items.Add("BSME")
        Student_Course_ComboBox.Items.Add("BSEE")
        Student_Course_ComboBox.Items.Add("BSCpE")
        Student_Course_ComboBox.Items.Add("BSPSYCH")
        Student_Course_ComboBox.Items.Add("BSHRM")
        Student_Course_ComboBox.Items.Add("BSTM")
        Student_Course_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    ''' <summary>
    ''' Initialize Year Level ComboBox with 1-4 year levels
    ''' </summary>
    Private Sub InitializeYearLevelComboBox()
        Student_YrLvl_ComboBox.Items.Clear()
        Student_YrLvl_ComboBox.Items.Add("1")
        Student_YrLvl_ComboBox.Items.Add("2")
        Student_YrLvl_ComboBox.Items.Add("3")
        Student_YrLvl_ComboBox.Items.Add("4")
        Student_YrLvl_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub
#End Region

#Region "Button Click Events"
    Private Sub Student_Register_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Register_Button.Click
        ' Validate all required fields
        If Not ValidateFields() Then
            Return
        End If

        ' Register the student
        RegisterStudent()
    End Sub

    Private Sub Student_Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Cancel_Button.Click
        ' Close the form
        Me.Close()
    End Sub
#End Region

#Region "Validation"
    ''' <summary>
    ''' Validate all input fields before registration
    ''' </summary>
    Private Function ValidateFields() As Boolean
        ' Check if lastname is empty
        If String.IsNullOrEmpty(Student_Lastname_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter student's last name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Lastname_TextBox.Focus()
            Return False
        End If

        ' Validate lastname format (only letters, spaces, hyphens, and apostrophes)
        If Not IsValidName(Student_Lastname_TextBox.Text.Trim()) Then
            MessageBox.Show("Last name can only contain letters, spaces, hyphens, and apostrophes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Lastname_TextBox.Focus()
            Return False
        End If

        ' Check lastname length
        If Student_Lastname_TextBox.Text.Trim().Length < 2 Then
            MessageBox.Show("Last name must be at least 2 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Lastname_TextBox.Focus()
            Return False
        End If

        If Student_Lastname_TextBox.Text.Trim().Length > 50 Then
            MessageBox.Show("Last name cannot exceed 50 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Lastname_TextBox.Focus()
            Return False
        End If

        ' Check if firstname is empty
        If String.IsNullOrEmpty(Student_Firstname_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter student's first name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Firstname_TextBox.Focus()
            Return False
        End If

        ' Validate firstname format
        If Not IsValidName(Student_Firstname_TextBox.Text.Trim()) Then
            MessageBox.Show("First name can only contain letters, spaces, hyphens, and apostrophes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Firstname_TextBox.Focus()
            Return False
        End If

        ' Check firstname length
        If Student_Firstname_TextBox.Text.Trim().Length < 2 Then
            MessageBox.Show("First name must be at least 2 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Firstname_TextBox.Focus()
            Return False
        End If

        If Student_Firstname_TextBox.Text.Trim().Length > 50 Then
            MessageBox.Show("First name cannot exceed 50 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Firstname_TextBox.Focus()
            Return False
        End If

        ' Validate middlename if provided (optional field)
        If Not String.IsNullOrEmpty(Student_Middlename_TextBox.Text.Trim()) Then
            If Not IsValidName(Student_Middlename_TextBox.Text.Trim()) Then
                MessageBox.Show("Middle name can only contain letters, spaces, hyphens, and apostrophes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Student_Middlename_TextBox.Focus()
                Return False
            End If

            If Student_Middlename_TextBox.Text.Trim().Length > 50 Then
                MessageBox.Show("Middle name cannot exceed 50 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Student_Middlename_TextBox.Focus()
                Return False
            End If
        End If

        ' Check if email is empty
        If String.IsNullOrEmpty(Student_Email_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter student's email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Email_TextBox.Focus()
            Return False
        End If

        ' Validate email format
        If Not IsValidEmail(Student_Email_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter a valid email address." & vbCrLf, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Email_TextBox.Focus()
            Return False
        End If

        ' Check email length
        If Student_Email_TextBox.Text.Trim().Length > 50 Then
            MessageBox.Show("Email address cannot exceed 50 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Email_TextBox.Focus()
            Return False
        End If

        ' Check if gender is selected
        If Student_Gender_ComboBox.SelectedIndex = -1 Then
            MessageBox.Show("Please select student's gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Gender_ComboBox.Focus()
            Return False
        End If

        ' Check if course is selected
        If Student_Course_ComboBox.SelectedIndex = -1 Then
            MessageBox.Show("Please select student's course.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Course_ComboBox.Focus()
            Return False
        End If

        ' Check if year level is selected
        If Student_YrLvl_ComboBox.SelectedIndex = -1 Then
            MessageBox.Show("Please select student's year level.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_YrLvl_ComboBox.Focus()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Validate name format (allows letters, spaces, hyphens, apostrophes, and periods)
    ''' </summary>
    Private Function IsValidName(ByVal name As String) As Boolean
        Try
            ' Allow letters (including international characters), spaces, hyphens, apostrophes, and periods
            Dim nameRegex As New System.Text.RegularExpressions.Regex("^[a-zA-Z\s\-'.]+$")
            Return nameRegex.IsMatch(name)
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Validate email format with comprehensive rules
    ''' </summary>
    Private Function IsValidEmail(ByVal email As String) As Boolean
        Try
            ' More comprehensive email validation
            ' Format: localpart@domain.extension
            ' Local part: alphanumeric, dots, hyphens, underscores
            ' Domain: alphanumeric, dots, hyphens
            ' Extension: at least 2 letters
            Dim emailRegex As New System.Text.RegularExpressions.Regex("^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")

            If Not emailRegex.IsMatch(email) Then
                Return False
            End If

            ' Additional checks
            ' Must contain exactly one @ symbol
            If email.Split("@"c).Length <> 2 Then
                Return False
            End If

            ' Split email into local part and domain
            Dim parts() As String = email.Split("@"c)
            Dim localPart As String = parts(0)
            Dim domain As String = parts(1)

            ' Local part must not be empty and not start/end with dot
            If String.IsNullOrEmpty(localPart) OrElse localPart.StartsWith(".") OrElse localPart.EndsWith(".") Then
                Return False
            End If

            ' Domain must contain at least one dot
            If Not domain.Contains(".") Then
                Return False
            End If

            ' Domain must not start/end with dot or hyphen
            If domain.StartsWith(".") OrElse domain.EndsWith(".") OrElse domain.StartsWith("-") OrElse domain.EndsWith("-") Then
                Return False
            End If

            ' Check for consecutive dots
            If email.Contains("..") Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Student Registration"
    ''' <summary>
    ''' Register student using database triggers
    ''' The trigger will automatically insert into student table when we insert into account table
    ''' </summary>
    Private Sub RegisterStudent()
        Try
            Connect_me()

            ' Check if email already exists in account table
            If EmailExists(Student_Email_TextBox.Text.Trim()) Then
                MessageBox.Show("This email address is already registered in the system.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get or create appropriate section
            Dim sectionName As String = GetOrCreateSection()

            If String.IsNullOrEmpty(sectionName) Then
                MessageBox.Show("Failed to assign section to student.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Insert into account table ONLY - the trigger will handle student table insertion
            Dim query As String = "INSERT INTO account (email, password, role, firstname, middlename, lastname, section, gender, course, yr_lvl) " & _
                                  "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"

            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@email", Student_Email_TextBox.Text.Trim().ToLower())
            cmd.Parameters.AddWithValue("@password", "12345")
            cmd.Parameters.AddWithValue("@role", "STUDENT")
            cmd.Parameters.AddWithValue("@firstname", CapitalizeFirstLetter(Student_Firstname_TextBox.Text.Trim()))
            cmd.Parameters.AddWithValue("@middlename", CapitalizeFirstLetter(Student_Middlename_TextBox.Text.Trim()))
            cmd.Parameters.AddWithValue("@lastname", CapitalizeFirstLetter(Student_Lastname_TextBox.Text.Trim()))
            cmd.Parameters.AddWithValue("@section", sectionName)
            cmd.Parameters.AddWithValue("@gender", Student_Gender_ComboBox.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@course", Student_Course_ComboBox.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@yr_lvl", Convert.ToInt32(Student_YrLvl_ComboBox.SelectedItem.ToString()))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show("Student registered successfully!" & vbCrLf & vbCrLf & _
                               "Email/Username: " & Student_Email_TextBox.Text.Trim().ToLower() & vbCrLf & _
                               "Default Password: 12345" & vbCrLf & _
                               "Section: " & sectionName & vbCrLf & vbCrLf & _
                               "Student record automatically created via database trigger.", _
                               "Registration Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Clear all fields
                ClearFields()

                ' Close the form
                Me.Close()
            Else
                MessageBox.Show("Failed to register student.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error during registration: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Capitalize first letter of each word in a name
    ''' </summary>
    Private Function CapitalizeFirstLetter(ByVal text As String) As String
        If String.IsNullOrEmpty(text) Then
            Return text
        End If

        Dim words() As String = text.Split(" "c)
        Dim result As String = ""

        For i As Integer = 0 To words.Length - 1
            If words(i).Length > 0 Then
                words(i) = words(i).Substring(0, 1).ToUpper() & words(i).Substring(1).ToLower()
            End If
            result &= words(i)
            If i < words.Length - 1 Then
                result &= " "
            End If
        Next

        Return result
    End Function

    ''' <summary>
    ''' Check if email already exists in account table
    ''' </summary>
    Private Function EmailExists(ByVal email As String) As Boolean
        Try
            Connect_me()

            Dim query As String = "SELECT COUNT(*) FROM account WHERE LOWER(email) = LOWER(?)"
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@email", email.Trim())

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            Return count > 0

        Catch ex As Exception
            MessageBox.Show("Error checking email: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Get existing section or create new section based on course and year level
    ''' Format: COURSE YEARLEVEL-SECTION (e.g., BSIT 1-1, BSIT 1-2)
    ''' Maximum 45 students per section
    ''' </summary>
    Private Function GetOrCreateSection() As String
        Try
            Connect_me()

            Dim course As String = Student_Course_ComboBox.SelectedItem.ToString()
            Dim yearLevel As Integer = Convert.ToInt32(Student_YrLvl_ComboBox.SelectedItem.ToString())
            Dim sectionNumber As Integer = 1
            Dim maxStudentsPerSection As Integer = 45

            ' Loop to find available section or create new one
            While True
                Dim sectionName As String = course & " " & yearLevel.ToString() & "-" & sectionNumber.ToString()

                ' Check if section exists in section table
                Dim checkSectionQuery As String = "SELECT section_id FROM section WHERE section = ? AND year_lvl = ?"
                Dim checkSectionCmd As New OdbcCommand(checkSectionQuery, con)
                checkSectionCmd.Parameters.AddWithValue("@section", sectionName)
                checkSectionCmd.Parameters.AddWithValue("@year_lvl", yearLevel)

                Dim sectionId As Object = checkSectionCmd.ExecuteScalar()

                ' If section doesn't exist, create it
                If sectionId Is Nothing Then
                    Dim createSectionQuery As String = "INSERT INTO section (year_lvl, section) VALUES (?, ?)"
                    Dim createSectionCmd As New OdbcCommand(createSectionQuery, con)
                    createSectionCmd.Parameters.AddWithValue("@year_lvl", yearLevel)
                    createSectionCmd.Parameters.AddWithValue("@section", sectionName)
                    createSectionCmd.ExecuteNonQuery()

                    ' Return the newly created section
                    Return sectionName
                End If

                ' Count students in this section
                Dim countQuery As String = "SELECT COUNT(*) FROM student WHERE section = ?"
                Dim countCmd As New OdbcCommand(countQuery, con)
                countCmd.Parameters.AddWithValue("@section", sectionName)

                Dim studentCount As Integer = Convert.ToInt32(countCmd.ExecuteScalar())

                ' If section has space, use it
                If studentCount < maxStudentsPerSection Then
                    Return sectionName
                End If

                ' Section is full, try next section number
                sectionNumber += 1

                ' Safety break (prevent infinite loop)
                If sectionNumber > 100 Then
                    Exit While
                End If
            End While

            ' If we reach here, something went wrong
            Return ""

        Catch ex As Exception
            MessageBox.Show("Error managing section: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Clear all input fields
    ''' </summary>
    Private Sub ClearFields()
        Student_Lastname_TextBox.Clear()
        Student_Firstname_TextBox.Clear()
        Student_Middlename_TextBox.Clear()
        Student_Email_TextBox.Clear()
        Student_Gender_ComboBox.SelectedIndex = -1
        Student_Course_ComboBox.SelectedIndex = -1
        Student_YrLvl_ComboBox.SelectedIndex = -1
    End Sub
#End Region

#Region "TextBox and ComboBox Change Events"
    Private Sub Student_Lastname_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Lastname_TextBox.TextChanged
        ' Optional: Can add real-time validation or formatting here
    End Sub

    Private Sub Student_Firstname_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Firstname_TextBox.TextChanged
        ' Optional: Can add real-time validation or formatting here
    End Sub

    Private Sub Student_Middlename_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Middlename_TextBox.TextChanged
        ' Optional: Can add real-time validation or formatting here
    End Sub

    Private Sub Student_Email_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Email_TextBox.TextChanged
        ' Optional: Can add real-time email validation here
    End Sub

    Private Sub Student_Gender_ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Gender_ComboBox.SelectedIndexChanged
        ' Optional: Can add logic when gender is selected
    End Sub

    Private Sub Student_Course_ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Course_ComboBox.SelectedIndexChanged
        ' Optional: Can add logic when course is selected
    End Sub

    Private Sub Student_YrLvl_ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_YrLvl_ComboBox.SelectedIndexChanged
        ' Optional: Can add logic when year level is selected
    End Sub
#End Region

End Class