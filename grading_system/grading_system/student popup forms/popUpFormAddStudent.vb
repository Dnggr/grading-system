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
        Student_Course_ComboBox.Items.Add("BSIT") ' Bachelor of Science in Information Technology
        Student_Course_ComboBox.Items.Add("BSCS") ' Bachelor of Science in Computer Science
        Student_Course_ComboBox.Items.Add("BSA") ' Bachelor of Science in Accountancy
        Student_Course_ComboBox.Items.Add("BSBA") ' Bachelor of Science in Business Administration
        Student_Course_ComboBox.Items.Add("BSED") ' Bachelor of Secondary Education
        Student_Course_ComboBox.Items.Add("BEED") ' Bachelor of Elementary Education
        Student_Course_ComboBox.Items.Add("BSN") ' Bachelor of Science in Nursing
        Student_Course_ComboBox.Items.Add("BSECE") ' Bachelor of Science in Electronics and Communications Engineering
        Student_Course_ComboBox.Items.Add("BSCE") ' Bachelor of Science in Civil Engineering
        Student_Course_ComboBox.Items.Add("BSME") ' Bachelor of Science in Mechanical Engineering
        Student_Course_ComboBox.Items.Add("BSEE") ' Bachelor of Science in Electrical Engineering
        Student_Course_ComboBox.Items.Add("BSCpE") ' Bachelor of Science in Computer Engineering
        Student_Course_ComboBox.Items.Add("BSPSYCH") ' Bachelor of Science in Psychology
        Student_Course_ComboBox.Items.Add("BSHRM") ' Bachelor of Science in Hotel and Restaurant Management
        Student_Course_ComboBox.Items.Add("BSTM") ' Bachelor of Science in Tourism Management
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

        ' Check if firstname is empty
        If String.IsNullOrEmpty(Student_Firstname_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter student's first name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Firstname_TextBox.Focus()
            Return False
        End If

        ' Check if email is empty
        If String.IsNullOrEmpty(Student_Email_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter student's email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Email_TextBox.Focus()
            Return False
        End If

        ' Validate email format
        If Not IsValidEmail(Student_Email_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    ''' Validate email format
    ''' </summary>
    Private Function IsValidEmail(ByVal email As String) As Boolean
        Try
            Dim emailRegex As New System.Text.RegularExpressions.Regex("^[^@\s]+@[^@\s]+\.[^@\s]+$")
            Return emailRegex.IsMatch(email)
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Student Registration"
    ''' <summary>
    ''' Register student with automatic account creation and section assignment
    ''' </summary>
    Private Sub RegisterStudent()
        Try
            Connect_me()

            ' Check if email already exists in account table
            If EmailExists(Student_Email_TextBox.Text.Trim()) Then
                MessageBox.Show("This email address is already registered in the system.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Step 1: Create account for the student
            Dim accountId As Integer = CreateStudentAccount()

            If accountId = 0 Then
                MessageBox.Show("Failed to create student account.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Step 2: Get or create appropriate section
            Dim sectionName As String = GetOrCreateSection()

            If String.IsNullOrEmpty(sectionName) Then
                MessageBox.Show("Failed to assign section to student.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Step 3: Insert student record
            Dim studentInserted As Boolean = InsertStudentRecord(accountId, sectionName)

            If studentInserted Then
                MessageBox.Show("Student registered successfully!" & vbCrLf & vbCrLf & _
                               "Email/Username: " & Student_Email_TextBox.Text.Trim() & vbCrLf & _
                               "Default Password: 12345" & vbCrLf & _
                               "Section: " & sectionName, _
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
    ''' Check if email already exists in account table
    ''' </summary>
    Private Function EmailExists(ByVal email As String) As Boolean
        Try
            Connect_me()

            Dim query As String = "SELECT COUNT(*) FROM account WHERE username = ?"
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@username", email)

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            Return count > 0

        Catch ex As Exception
            MessageBox.Show("Error checking email: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Create account for student (username = email, password = 12345, role = STUDENT)
    ''' </summary>
    Private Function CreateStudentAccount() As Integer
        Try
            Connect_me()

            ' Insert account record
            Dim query As String = "INSERT INTO account (username, password, role) VALUES (?, ?, ?)"
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@username", Student_Email_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@password", "12345")
            cmd.Parameters.AddWithValue("@role", "STUDENT")

            cmd.ExecuteNonQuery()

            ' Get the last inserted account ID
            Dim getIdQuery As String = "SELECT LAST_INSERT_ID()"
            Dim getIdCmd As New OdbcCommand(getIdQuery, con)
            Dim accountId As Integer = Convert.ToInt32(getIdCmd.ExecuteScalar())

            Return accountId

        Catch ex As Exception
            MessageBox.Show("Error creating account: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
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
    ''' Insert student record into student table
    ''' </summary>
    Private Function InsertStudentRecord(ByVal accountId As Integer, ByVal sectionName As String) As Boolean
        Try
            Connect_me()

            Dim query As String = "INSERT INTO student (acc_id, firstname, middlename, lastname, section, gender, email, course, yr_lvl) " & _
                                  "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)"

            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@acc_id", accountId)
            cmd.Parameters.AddWithValue("@firstname", Student_Firstname_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@middlename", Student_Middlename_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@lastname", Student_Lastname_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@section", sectionName)
            cmd.Parameters.AddWithValue("@gender", Student_Gender_ComboBox.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@email", Student_Email_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@course", Student_Course_ComboBox.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@yr_lvl", Convert.ToInt32(Student_YrLvl_ComboBox.SelectedItem.ToString()))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            Return rowsAffected > 0

        Catch ex As Exception
            MessageBox.Show("Error inserting student record: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
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