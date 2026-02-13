Imports System.Data.Odbc

Public Class popUpFormAddStudent

#Region "Form Load - Initialize ComboBoxes"
    Private Sub popUpFormAddStudent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitializeGenderComboBox()
        InitializeCourseComboBox()
        InitializeYearLevelComboBox()
    End Sub

    Private Sub InitializeGenderComboBox()
        Student_Gender_ComboBox.Items.Clear()
        Student_Gender_ComboBox.Items.Add("male")
        Student_Gender_ComboBox.Items.Add("female")
        Student_Gender_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

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
        If Not ValidateFields() Then
            Return
        End If
        RegisterStudent()
    End Sub

    Private Sub Student_Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Cancel_Button.Click
        Me.Close()
    End Sub
#End Region

#Region "Validation"
    Private Function ValidateFields() As Boolean
        If String.IsNullOrEmpty(Student_Lastname_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter student's last name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Lastname_TextBox.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(Student_Firstname_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter student's first name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Firstname_TextBox.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(Student_Email_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter student's email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Email_TextBox.Focus()
            Return False
        End If

        If Not IsValidEmail(Student_Email_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Email_TextBox.Focus()
            Return False
        End If

        If Student_Gender_ComboBox.SelectedIndex = -1 Then
            MessageBox.Show("Please select student's gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Gender_ComboBox.Focus()
            Return False
        End If

        If Student_Course_ComboBox.SelectedIndex = -1 Then
            MessageBox.Show("Please select student's course.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_Course_ComboBox.Focus()
            Return False
        End If

        If Student_YrLvl_ComboBox.SelectedIndex = -1 Then
            MessageBox.Show("Please select student's year level.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Student_YrLvl_ComboBox.Focus()
            Return False
        End If

        Return True
    End Function

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
    ''' Main registration flow. Opens ONE connection for all sub-operations.
    ''' The DB trigger trg_student_to_account auto-populates the student table on account INSERT.
    ''' </summary>
    Private Sub RegisterStudent()
        Try
            Connect_me()

            If EmailExists(Student_Email_TextBox.Text.Trim()) Then
                MessageBox.Show("This email address is already registered in the system.", _
                                "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim sectionName As String = GetOrCreateSection()

            If String.IsNullOrEmpty(sectionName) Then
                MessageBox.Show("Failed to assign section to student.", _
                                "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim hashedPassword As String = HashPassword("12345")

            If String.IsNullOrEmpty(hashedPassword) Then
                MessageBox.Show("Failed to generate secure password.", _
                                "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim accountInserted As Boolean = InsertAccountWithTrigger(hashedPassword, sectionName)

            If accountInserted Then
                ' ✅ FIXED: Removed the "Note: Password is securely hashed" line
                MessageBox.Show("Student registered successfully!" & vbCrLf & vbCrLf & _
                                "Email/Username: " & Student_Email_TextBox.Text.Trim() & vbCrLf & _
                                "Default Password: 12345" & vbCrLf & _
                                "Section: " & sectionName, _
                                "Registration Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()
                Me.Close()
            Else
                MessageBox.Show("Failed to register student.", "Registration Error", _
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error during registration: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Function EmailExists(ByVal email As String) As Boolean
        Try
            Dim query As String = "SELECT COUNT(*) FROM account WHERE email = ?"
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@email", email)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return count > 0
        Catch ex As Exception
            MessageBox.Show("Error checking email: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function InsertAccountWithTrigger(ByVal hashedPassword As String, ByVal sectionName As String) As Boolean
        Try
            Dim query As String = "INSERT INTO account " & _
                                  "(email, pword, role, firstname, middlename, lastname, section, gender, course, yr_lvl) " & _
                                  "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"

            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@email", Student_Email_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@pword", hashedPassword)
            cmd.Parameters.AddWithValue("@role", "student")
            cmd.Parameters.AddWithValue("@firstname", Student_Firstname_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@middlename", Student_Middlename_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@lastname", Student_Lastname_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@section", sectionName)
            cmd.Parameters.AddWithValue("@gender", Student_Gender_ComboBox.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@course", Student_Course_ComboBox.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@yr_lvl", Convert.ToInt32(Student_YrLvl_ComboBox.SelectedItem.ToString()))

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            Return rowsAffected > 0

        Catch ex As Exception
            MessageBox.Show("Error inserting account record: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function GetOrCreateSection() As String
        Try
            Dim course As String = Student_Course_ComboBox.SelectedItem.ToString()
            Dim yearLevel As Integer = Convert.ToInt32(Student_YrLvl_ComboBox.SelectedItem.ToString())
            Dim sectionNumber As Integer = 1
            Dim maxStudentsPerSection As Integer = 45

            Do While sectionNumber <= 100
                Dim sectionName As String = course & " " & yearLevel.ToString() & "-" & sectionNumber.ToString()

                Dim checkQuery As String = "SELECT section_id FROM section WHERE section = ? AND year_lvl = ?"
                Dim checkCmd As New OdbcCommand(checkQuery, con)
                checkCmd.Parameters.AddWithValue("@section", sectionName)
                checkCmd.Parameters.AddWithValue("@year_lvl", yearLevel)

                Dim result As Object = checkCmd.ExecuteScalar()

                If result Is Nothing OrElse IsDBNull(result) Then
                    Dim createQuery As String = "INSERT INTO section (year_lvl, section) VALUES (?, ?)"
                    Dim createCmd As New OdbcCommand(createQuery, con)
                    createCmd.Parameters.AddWithValue("@year_lvl", yearLevel)
                    createCmd.Parameters.AddWithValue("@section", sectionName)
                    createCmd.ExecuteNonQuery()
                    Return sectionName
                End If

                Dim countQuery As String = "SELECT COUNT(*) FROM student WHERE section = ?"
                Dim countCmd As New OdbcCommand(countQuery, con)
                countCmd.Parameters.AddWithValue("@section", sectionName)

                Dim studentCount As Integer = Convert.ToInt32(countCmd.ExecuteScalar())

                If studentCount < maxStudentsPerSection Then
                    Return sectionName
                End If

                sectionNumber += 1
            Loop

            Return ""

        Catch ex As Exception
            MessageBox.Show("Error managing section: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

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
    End Sub

    Private Sub Student_Firstname_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Firstname_TextBox.TextChanged
    End Sub

    Private Sub Student_Middlename_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Middlename_TextBox.TextChanged
    End Sub

    Private Sub Student_Email_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Email_TextBox.TextChanged
    End Sub

    Private Sub Student_Gender_ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Gender_ComboBox.SelectedIndexChanged
    End Sub

    Private Sub Student_Course_ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Course_ComboBox.SelectedIndexChanged
    End Sub

    Private Sub Student_YrLvl_ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_YrLvl_ComboBox.SelectedIndexChanged
    End Sub
#End Region

End Class