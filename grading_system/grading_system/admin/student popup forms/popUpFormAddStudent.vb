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
    '''
    ''' REGISTRATION FLOW:
    '''   STEP 1 — Lookup or create a section in the `section` table based on
    '''            course + year_level. Returns section_id AND section name.
    '''   STEP 2 — Insert into `account` (includes section name as plain text).
    '''            The DB trigger `insert_student_if_role_student` fires automatically
    '''            and inserts a matching row into `student` (without section_id).
    '''   STEP 3 — Update `student.section_id` using the acc_id returned in Step 2.
    '''            This links the student row to the correct section row.
    '''
    Private Sub RegisterStudent()
        Try
            Connect_me()

            If con.State <> ConnectionState.Open Then
                MessageBox.Show("Database connection failed. Please try again.", _
                                "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Guard: duplicate email check
            If EmailExists(Student_Email_TextBox.Text.Trim()) Then
                MessageBox.Show("This email address is already registered in the system.", _
                                "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ── STEP 1: section table → get or create section, return section_id ──
            Dim sectionId As Integer = 0
            Dim sectionName As String = ""

            If Not GetOrCreateSection(sectionId, sectionName) Then
                MessageBox.Show("Failed to assign a section to the student.", _
                                "Section Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' ── Hash the default password "12345" ──
            Dim hashedPassword As String = HashPassword("12345")
            If String.IsNullOrEmpty(hashedPassword) Then
                MessageBox.Show("Failed to generate secure password.", _
                                "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' ── STEP 2: Insert into account; trigger auto-inserts into student ──
            '    The account.section column stores the human-readable section name.
            Dim newAccId As Integer = InsertAccountWithTrigger(hashedPassword, sectionName)
            If newAccId = 0 Then
                MessageBox.Show("Failed to register student account.", _
                                "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' ── STEP 3: Update student.section_id (trigger could not do this) ──
            If UpdateStudentSectionId(newAccId, sectionId) Then
                MessageBox.Show("Student registered successfully!" & vbCrLf & vbCrLf & _
                                "Email / Username : " & Student_Email_TextBox.Text.Trim() & vbCrLf & _
                                "Default Password : 12345" & vbCrLf & _
                                "Section          : " & sectionName, _
                                "Registration Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()
                Me.Close()
            Else
                MessageBox.Show("Student account was created but the section assignment failed." & vbCrLf & _
                                "Please update the student's section manually.", _
                                "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Unexpected error during registration:" & vbCrLf & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    ' -----------------------------------------------------------------------
    ' Checks whether an email is already in use in the account table.
    ' -----------------------------------------------------------------------
    Private Function EmailExists(ByVal email As String) As Boolean
        Try
            Dim cmd As New OdbcCommand("SELECT COUNT(*) FROM account WHERE email = ?", con)
            cmd.Parameters.AddWithValue("@email", email)
            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
        Catch ex As Exception
            MessageBox.Show("Error checking email: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ' -----------------------------------------------------------------------
    ' STEP 2 — Inserts one row into `account`.
    ' The database trigger `insert_student_if_role_student` fires AFTER this
    ' INSERT and creates a matching row in `student` (section_id is NULL at
    ' that point — it gets filled in Step 3).
    ' Returns the new acc_id, or 0 on failure.
    ' -----------------------------------------------------------------------
    Private Function InsertAccountWithTrigger(ByVal hashedPassword As String, _
                                              ByVal sectionName As String) As Integer
        Try
            Dim insertSql As String = _
                "INSERT INTO account " & _
                "  (email, pword, role, firstname, middlename, lastname, section, gender, course, yr_lvl) " & _
                "VALUES (?, ?, 'student', ?, ?, ?, ?, ?, ?, ?)"

            Dim cmd As New OdbcCommand(insertSql, con)
            cmd.Parameters.AddWithValue("@email", Student_Email_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@pword", hashedPassword)
            cmd.Parameters.AddWithValue("@firstname", Student_Firstname_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@middlename", Student_Middlename_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@lastname", Student_Lastname_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@section", sectionName)   ' human-readable, e.g. "BSIT 1-1"
            cmd.Parameters.AddWithValue("@gender", Student_Gender_ComboBox.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@course", Student_Course_ComboBox.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@yr_lvl", Convert.ToInt32(Student_YrLvl_ComboBox.SelectedItem.ToString()))

            If cmd.ExecuteNonQuery() > 0 Then
                ' Use LAST_INSERT_ID() — safest way to get the auto-increment value
                Dim idCmd As New OdbcCommand("SELECT LAST_INSERT_ID()", con)
                Dim result As Object = idCmd.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Return Convert.ToInt32(result)
                End If
            End If

            Return 0

        Catch ex As Exception
            MessageBox.Show("Error inserting account record: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        End Try
    End Function

    ' -----------------------------------------------------------------------
    ' STEP 3 — Sets student.section_id for the row the trigger just created.
    ' Matched by acc_id because the trigger copied it from account.
    ' -----------------------------------------------------------------------
    Private Function UpdateStudentSectionId(ByVal accId As Integer, _
                                            ByVal sectionId As Integer) As Boolean
        Try
            Dim cmd As New OdbcCommand("UPDATE student SET section_id = ? WHERE acc_id = ?", con)
            cmd.Parameters.AddWithValue("@section_id", sectionId)
            cmd.Parameters.AddWithValue("@acc_id", accId)
            Return cmd.ExecuteNonQuery() > 0
        Catch ex As Exception
            MessageBox.Show("Error updating student section: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ' -----------------------------------------------------------------------
    ' STEP 1 — Finds or creates a section row in the `section` table.
    '
    ' Logic:
    '   • Build the section name from course + year_level + section number
    '     (e.g. "BSIT 1-1", "BSIT 1-2", …).
    '   • If a matching section already exists AND has fewer than
    '     maxStudentsPerSection students, reuse it.
    '   • Otherwise try the next section number.
    '   • If the section does not exist at all, create it and return it.
    '
    ' Uses ByRef parameters so both section_id AND section name are returned
    ' in one call (VB 2008 does not have tuples).
    ' -----------------------------------------------------------------------
    Private Function GetOrCreateSection(ByRef outSectionId As Integer, _
                                        ByRef outSectionName As String) As Boolean
        Const maxStudentsPerSection As Integer = 45

        Try
            Dim course As String = Student_Course_ComboBox.SelectedItem.ToString()
            Dim yearLevel As Integer = Convert.ToInt32(Student_YrLvl_ComboBox.SelectedItem.ToString())

            Dim sectionNumber As Integer = 1

            Do While sectionNumber <= 100

                Dim sectionName As String = course & " " & yearLevel.ToString() & "-" & sectionNumber.ToString()

                ' ── Does this section already exist in the section table? ──
                Dim checkCmd As New OdbcCommand( _
                    "SELECT section_id FROM section WHERE section = ? AND year_lvl = ?", con)
                checkCmd.Parameters.AddWithValue("@section", sectionName)
                checkCmd.Parameters.AddWithValue("@year_lvl", yearLevel)

                Dim checkResult As Object = checkCmd.ExecuteScalar()

                If checkResult Is Nothing OrElse IsDBNull(checkResult) Then
                    ' Section does not exist — create it now.
                    Dim createCmd As New OdbcCommand( _
                        "INSERT INTO section (year_lvl, section) VALUES (?, ?)", con)
                    createCmd.Parameters.AddWithValue("@year_lvl", yearLevel)
                    createCmd.Parameters.AddWithValue("@section", sectionName)
                    createCmd.ExecuteNonQuery()

                    ' Retrieve the new section_id via LAST_INSERT_ID() — avoids race conditions.
                    Dim newIdCmd As New OdbcCommand("SELECT LAST_INSERT_ID()", con)
                    Dim newIdResult As Object = newIdCmd.ExecuteScalar()

                    If newIdResult IsNot Nothing AndAlso Not IsDBNull(newIdResult) Then
                        outSectionId = Convert.ToInt32(newIdResult)
                        outSectionName = sectionName
                        Return True
                    End If

                    ' Fallback: re-query by name if LAST_INSERT_ID returned nothing
                    Dim fallbackCmd As New OdbcCommand( _
                        "SELECT section_id FROM section WHERE section = ? AND year_lvl = ?", con)
                    fallbackCmd.Parameters.AddWithValue("@section", sectionName)
                    fallbackCmd.Parameters.AddWithValue("@year_lvl", yearLevel)
                    Dim fallbackResult As Object = fallbackCmd.ExecuteScalar()

                    If fallbackResult IsNot Nothing AndAlso Not IsDBNull(fallbackResult) Then
                        outSectionId = Convert.ToInt32(fallbackResult)
                        outSectionName = sectionName
                        Return True
                    End If

                Else
                    ' Section exists — check how many students are already in it.
                    Dim existingSectionId As Integer = Convert.ToInt32(checkResult)

                    Dim countCmd As New OdbcCommand( _
                        "SELECT COUNT(*) FROM student WHERE section_id = ?", con)
                    countCmd.Parameters.AddWithValue("@section_id", existingSectionId)
                    Dim studentCount As Integer = Convert.ToInt32(countCmd.ExecuteScalar())

                    If studentCount < maxStudentsPerSection Then
                        ' Section has room — use it.
                        outSectionId = existingSectionId
                        outSectionName = sectionName
                        Return True
                    End If
                    ' Section is full — loop to try next section number.
                End If

                sectionNumber += 1
            Loop

            ' All 100 section numbers exhausted (extremely unlikely).
            outSectionId = 0
            outSectionName = ""
            Return False

        Catch ex As Exception
            MessageBox.Show("Error managing section: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            outSectionId = 0
            outSectionName = ""
            Return False
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

#Region "Password Hashing"
    Private Function HashPassword(ByVal password As String) As String
        Try
            Dim md5 As System.Security.Cryptography.MD5 = System.Security.Cryptography.MD5.Create()
            Dim data As Byte() = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
            Dim sb As New System.Text.StringBuilder()
            Dim i As Integer
            For i = 0 To data.Length - 1
                sb.Append(data(i).ToString("x2"))
            Next i
            Return sb.ToString()
        Catch ex As Exception
            MessageBox.Show("Error hashing password: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function
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