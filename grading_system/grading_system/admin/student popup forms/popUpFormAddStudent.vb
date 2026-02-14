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
    Private Sub RegisterStudent()
        Try
            Connect_me()

            ' --- Step 1: Check for duplicate email ---
            If EmailExists(Student_Email_TextBox.Text.Trim()) Then
                MessageBox.Show("This email address is already registered in the system.", _
                                "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' --- Step 2: Get or create section, get section name ---
            Dim sectionId As Integer = GetOrCreateSectionId()
            If sectionId = -1 Then
                MessageBox.Show("Failed to assign section to student.", _
                                "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim sectionName As String = GetSectionNameById(sectionId)

            ' --- Step 3: Hash default password ---
            Dim hashedPassword As String = HashPassword("12345")
            If String.IsNullOrEmpty(hashedPassword) Then
                MessageBox.Show("Failed to generate secure password.", _
                                "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' --- Step 4: Insert into account (trigger fires → inserts into student) ---
            ' account table HAS: acc_id, email, pword, role, firstname, middlename,
            '                    lastname, section, gender, course, yr_lvl, subject
            ' We insert 'section' as the section NAME (text) for account table.
            ' student.section_id (FK) is set separately after trigger fires.
            Dim accountInserted As Boolean = InsertAccountWithTrigger(hashedPassword, sectionId, sectionName)

            If accountInserted Then
                MessageBox.Show("Student registered successfully!" & vbCrLf & vbCrLf & _
                                "Email/Username: " & Student_Email_TextBox.Text.Trim() & vbCrLf & _
                                "Default Password: 12345" & vbCrLf & _
                                "Section: " & sectionName, _
                                "Registration Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()
                Me.Close()
            Else
                MessageBox.Show("Failed to register student. Please check the database trigger.", _
                                "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    ''' <summary>
    ''' Inserts into account table.
    ''' - account.section = sectionName (text, e.g. "BSIT 1-1")
    ''' - Trigger fires and inserts student row (WITHOUT section_id — fixed trigger)
    ''' - Then we update student.section_id via UpdateStudentSectionId()
    ''' </summary>
    Private Function InsertAccountWithTrigger(ByVal hashedPassword As String, _
                                               ByVal sectionId As Integer, _
                                               ByVal sectionName As String) As Boolean
        Try
            ' account table columns:
            ' acc_id(auto), email, pword, role, firstname, middlename, lastname,
            ' section(varchar), gender, course, yr_lvl, subject
            Dim query As String = "INSERT INTO account " & _
                                  "(email, pword, role, firstname, middlename, lastname, " & _
                                  "section, gender, course, yr_lvl) " & _
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

            If rowsAffected > 0 Then
                ' Small delay to ensure trigger has completed
                System.Threading.Thread.Sleep(150)

                ' Get the newly created acc_id
                Dim accId As Integer = GetLastInsertedAccountId()

                If accId > 0 Then
                    ' Update student.section_id with the FK integer value
                    UpdateStudentSectionId(accId, sectionId)
                Else
                    MessageBox.Show("Warning: Could not retrieve account ID to assign section.", _
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

                Return True
            End If

            Return False

        Catch ex As Exception
            MessageBox.Show("Error inserting account record: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function GetLastInsertedAccountId() As Integer
        Try
            Dim query As String = "SELECT LAST_INSERT_ID()"
            Dim cmd As New OdbcCommand(query, con)
            Dim result As Object = cmd.ExecuteScalar()

            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return Convert.ToInt32(result)
            End If

            Return -1
        Catch ex As Exception
            MessageBox.Show("Error getting last inserted ID: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' After trigger creates student row, set student.section_id (FK to section table)
    ''' Matches by acc_id since trigger copied NEW.acc_id into student.acc_id
    ''' </summary>
    Private Sub UpdateStudentSectionId(ByVal accId As Integer, ByVal sectionId As Integer)
        Try
            Dim query As String = "UPDATE student SET section_id = ? WHERE acc_id = ?"
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@section_id", sectionId)
            cmd.Parameters.AddWithValue("@acc_id", accId)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected = 0 Then
                MessageBox.Show("Warning: Student section_id could not be set." & vbCrLf & _
                                "The trigger may not have created the student row correctly.", _
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Warning: Section ID not updated: " & ex.Message, _
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    ''' <summary>
    ''' Finds an available section for the given course + year level.
    ''' Creates a new section if none exists or all are full (max 45 students).
    ''' Returns section_id or -1 on failure.
    ''' </summary>
    Private Function GetOrCreateSectionId() As Integer
        Try
            Dim course As String = Student_Course_ComboBox.SelectedItem.ToString()
            Dim yearLevel As Integer = Convert.ToInt32(Student_YrLvl_ComboBox.SelectedItem.ToString())
            Dim maxStudentsPerSection As Integer = 45

            Dim sectionNumber As Integer = 1

            Do While sectionNumber <= 100
                Dim sectionName As String = course & " " & yearLevel.ToString() & "-" & sectionNumber.ToString()

                ' Check if this named section already exists
                Dim checkQuery As String = "SELECT section_id FROM section WHERE section = ? AND year_lvl = ?"
                Dim checkCmd As New OdbcCommand(checkQuery, con)
                checkCmd.Parameters.AddWithValue("@section", sectionName)
                checkCmd.Parameters.AddWithValue("@year_lvl", yearLevel)
                Dim result As Object = checkCmd.ExecuteScalar()

                If result Is Nothing OrElse IsDBNull(result) Then
                    ' Section does not exist — create it
                    Dim createQuery As String = "INSERT INTO section (year_lvl, section) VALUES (?, ?)"
                    Dim createCmd As New OdbcCommand(createQuery, con)
                    createCmd.Parameters.AddWithValue("@year_lvl", yearLevel)
                    createCmd.Parameters.AddWithValue("@section", sectionName)
                    createCmd.ExecuteNonQuery()

                    ' Retrieve the new section_id
                    Dim getIdQuery As String = "SELECT section_id FROM section WHERE section = ? AND year_lvl = ?"
                    Dim getIdCmd As New OdbcCommand(getIdQuery, con)
                    getIdCmd.Parameters.AddWithValue("@section", sectionName)
                    getIdCmd.Parameters.AddWithValue("@year_lvl", yearLevel)
                    Dim newId As Object = getIdCmd.ExecuteScalar()

                    If newId IsNot Nothing AndAlso Not IsDBNull(newId) Then
                        Return Convert.ToInt32(newId)
                    End If

                    ' Should not reach here, but fail safe
                    Return -1
                End If

                ' Section exists — check student count
                Dim sectionId As Integer = Convert.ToInt32(result)

                Dim countQuery As String = "SELECT COUNT(*) FROM student WHERE section_id = ?"
                Dim countCmd As New OdbcCommand(countQuery, con)
                countCmd.Parameters.AddWithValue("@section_id", sectionId)
                Dim studentCount As Integer = Convert.ToInt32(countCmd.ExecuteScalar())

                If studentCount < maxStudentsPerSection Then
                    Return sectionId   ' Section has space
                End If

                ' Section full — try next number
                sectionNumber += 1
            Loop

            Return -1  ' No available section after 100 attempts

        Catch ex As Exception
            MessageBox.Show("Error managing section: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return -1
        End Try
    End Function

    Private Function GetSectionNameById(ByVal sectionId As Integer) As String
        Try
            Dim query As String = "SELECT section FROM section WHERE section_id = ?"
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@section_id", sectionId)
            Dim result As Object = cmd.ExecuteScalar()

            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return result.ToString()
            End If

            Return "Unknown"
        Catch ex As Exception
            Return "Unknown"
        End Try
    End Function

    Private Function HashPassword(ByVal password As String) As String
        Try
            Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider()
            Dim inputBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(password)
            Dim hashBytes As Byte() = md5.ComputeHash(inputBytes)
            Dim sb As New System.Text.StringBuilder()
            Dim i As Integer
            For i = 0 To hashBytes.Length - 1
                sb.Append(hashBytes(i).ToString("x2"))
            Next i
            Return sb.ToString()
        Catch ex As Exception
            Return String.Empty
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