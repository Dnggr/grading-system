Imports System.Data.Odbc

Public Class popUpFormModifyStudent

#Region "Module-Level Variables"
    ' Stores the acc_id and stud_id of the student currently selected in the DataGridView
    Private _selectedAccId As Integer = -1
    Private _selectedStudId As Integer = -1

    ' Stores the ORIGINAL course and yr_lvl loaded from DB so we can detect a course change
    Private _originalCourse As String = String.Empty
    Private _originalYrLvl As Integer = 0
    Private _originalSection As String = String.Empty

    ' Flag: TRUE while we are programmatically populating fields.
    ' Prevents CourseComboBox_SelectedIndexChanged from firing section-reassignment logic
    ' when we are just loading data into the form.
    Private _isLoading As Boolean = False
#End Region

#Region "Form Load"
    Private Sub popUpFormModifyStudent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitializeComboBoxes()
        SetupDataGridView()
        LoadAllStudents()
    End Sub

    ''' <summary>
    ''' Populates all three ComboBoxes with their fixed option lists.
    ''' DropDownStyle = DropDownList so users cannot type free text.
    ''' </summary>
    Private Sub InitializeComboBoxes()
        ' --- Gender ---
        Modify_Student_Gender_ComboBox.Items.Clear()
        Modify_Student_Gender_ComboBox.Items.Add("male")
        Modify_Student_Gender_ComboBox.Items.Add("female")
        Modify_Student_Gender_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList

        ' --- Course ---
        Modify_Student_Course_ComboBox.Items.Clear()
        Modify_Student_Course_ComboBox.Items.Add("BSIT")
        Modify_Student_Course_ComboBox.Items.Add("BSCS")
        Modify_Student_Course_ComboBox.Items.Add("BSA")
        Modify_Student_Course_ComboBox.Items.Add("BSBA")
        Modify_Student_Course_ComboBox.Items.Add("BSED")
        Modify_Student_Course_ComboBox.Items.Add("BEED")
        Modify_Student_Course_ComboBox.Items.Add("BSN")
        Modify_Student_Course_ComboBox.Items.Add("BSECE")
        Modify_Student_Course_ComboBox.Items.Add("BSCE")
        Modify_Student_Course_ComboBox.Items.Add("BSME")
        Modify_Student_Course_ComboBox.Items.Add("BSEE")
        Modify_Student_Course_ComboBox.Items.Add("BSCpE")
        Modify_Student_Course_ComboBox.Items.Add("BSPSYCH")
        Modify_Student_Course_ComboBox.Items.Add("BSHRM")
        Modify_Student_Course_ComboBox.Items.Add("BSTM")
        Modify_Student_Course_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList

        ' --- Year Level (display only; not editable per requirements) ---
        ' We still need the label/textbox to show it; no combo needed.
        ' If a Modify_Student_YrLvl_Label or similar control exists, it will be
        ' populated in LoadStudentDataIntoFields().
    End Sub

    ''' <summary>
    ''' Configures the DataGridView: read-only, two visible columns (FullName, Section),
    ''' full-row selection, no auto-generated headers beyond what we define.
    ''' </summary>
    Private Sub SetupDataGridView()
        With Modify_Student_DataGridView
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoGenerateColumns = False

            ' Clear any columns already set at design time
            .Columns.Clear()

            ' Hidden column: acc_id — used internally to load correct student data
            Dim colAccId As New DataGridViewTextBoxColumn()
            colAccId.Name = "colAccId"
            colAccId.HeaderText = "acc_id"
            colAccId.DataPropertyName = "acc_id"
            colAccId.Visible = False
            .Columns.Add(colAccId)

            ' Hidden column: stud_id
            Dim colStudId As New DataGridViewTextBoxColumn()
            colStudId.Name = "colStudId"
            colStudId.HeaderText = "stud_id"
            colStudId.DataPropertyName = "stud_id"
            colStudId.Visible = False
            .Columns.Add(colStudId)

            ' Visible column: Full Name
            Dim colFullName As New DataGridViewTextBoxColumn()
            colFullName.Name = "colFullName"
            colFullName.HeaderText = "Full Name"
            colFullName.DataPropertyName = "fullname"
            colFullName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            colFullName.FillWeight = 60
            .Columns.Add(colFullName)

            ' Visible column: Section
            Dim colSection As New DataGridViewTextBoxColumn()
            colSection.Name = "colSection"
            colSection.HeaderText = "Section"
            colSection.DataPropertyName = "section"
            colSection.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            colSection.FillWeight = 40
            .Columns.Add(colSection)
        End With
    End Sub
#End Region

#Region "DataGridView — Load and Search"
    ''' <summary>
    ''' Loads ALL students (fullname + section) when the search box is empty.
    ''' Query targets the `student` table joined to `account` so we always have
    ''' both stud_id and acc_id available for the update step.
    ''' </summary>
    Private Sub LoadAllStudents()
        LoadStudents(String.Empty)
    End Sub

    ''' <summary>
    ''' Core data-load routine.  When <paramref name="searchName"/> is empty it
    ''' returns every student; otherwise it filters by first/last/middle name LIKE.
    ''' </summary>
    Private Sub LoadStudents(ByVal searchName As String)
        Try
            Connect_me()

            Dim query As String
            Dim cmd As OdbcCommand

            If String.IsNullOrEmpty(searchName.Trim()) Then
                ' No filter — return all students
                query = "SELECT s.stud_id, s.acc_id, " & _
                        "CONCAT(TRIM(s.firstname), ' ', TRIM(s.middlename), ' ', TRIM(s.lastname)) AS fullname, " & _
                        "s.section " & _
                        "FROM student s " & _
                        "WHERE s.role = 'student' " & _
                        "ORDER BY s.lastname, s.firstname"
                cmd = New OdbcCommand(query, con)
            Else
                ' Filter by any part of the name
                query = "SELECT s.stud_id, s.acc_id, " & _
                        "CONCAT(TRIM(s.firstname), ' ', TRIM(s.middlename), ' ', TRIM(s.lastname)) AS fullname, " & _
                        "s.section " & _
                        "FROM student s " & _
                        "WHERE s.role = 'student' " & _
                        "AND (s.firstname LIKE ? OR s.lastname LIKE ? OR s.middlename LIKE ?) " & _
                        "ORDER BY s.lastname, s.firstname"
                cmd = New OdbcCommand(query, con)
                Dim likeParam As String = "%" & searchName.Trim() & "%"
                cmd.Parameters.AddWithValue("@fn", likeParam)
                cmd.Parameters.AddWithValue("@ln", likeParam)
                cmd.Parameters.AddWithValue("@mn", likeParam)
            End If

            Dim adapter As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Modify_Student_DataGridView.DataSource = dt

        Catch ex As Exception
            MessageBox.Show("Error loading students: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    ''' <summary>
    ''' Live search: fires on every keystroke in the search textbox.
    ''' </summary>
    Private Sub Modify_Student_Search_Name_Textbox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Student_Search_Name_Textbox.TextChanged
        LoadStudents(Modify_Student_Search_Name_Textbox.Text)
    End Sub
#End Region

#Region "DataGridView — Row Click"
    ''' <summary>
    ''' Fires when any cell in the DataGridView is clicked.
    ''' Shows a confirmation pop-up, then loads the student's data into the form fields.
    ''' </summary>
    Private Sub Modify_Student_DataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Modify_Student_DataGridView.CellContentClick
        ' Guard: ignore header row clicks
        If e.RowIndex < 0 Then Return

        Dim row As DataGridViewRow = Modify_Student_DataGridView.Rows(e.RowIndex)

        ' Read the hidden IDs
        Dim accIdVal As Object = row.Cells("colAccId").Value
        Dim studIdVal As Object = row.Cells("colStudId").Value
        Dim fullNameVal As Object = row.Cells("colFullName").Value

        If accIdVal Is Nothing OrElse IsDBNull(accIdVal) Then
            MessageBox.Show("Cannot identify selected student (missing account ID).", _
                            "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim fullName As String = If(fullNameVal IsNot Nothing, fullNameVal.ToString().Trim(), "this student")

        ' Confirmation pop-up as required
        Dim confirm As DialogResult = MessageBox.Show( _
            "You are about to modify the record of:" & vbCrLf & vbCrLf & _
            fullName & vbCrLf & vbCrLf & _
            "Do you want to continue?", _
            "Confirm Modification", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question)

        If confirm <> DialogResult.Yes Then Return

        ' Store IDs
        _selectedAccId = Convert.ToInt32(accIdVal)
        _selectedStudId = If(studIdVal IsNot Nothing AndAlso Not IsDBNull(studIdVal), Convert.ToInt32(studIdVal), -1)

        ' Populate fields from database
        LoadStudentDataIntoFields(_selectedAccId)
    End Sub
#End Region

#Region "Load Student Data Into Fields"
    ''' <summary>
    ''' Fetches one student's full record from the `account` table (source of truth
    ''' because of the trigger architecture) and populates all form controls.
    ''' ComboBoxes are set programmatically — no manual selection needed.
    ''' </summary>
    Private Sub LoadStudentDataIntoFields(ByVal accId As Integer)
        Try
            Connect_me()

            Dim query As String = "SELECT firstname, middlename, lastname, email, gender, course, yr_lvl, section " & _
                                  "FROM account WHERE acc_id = ?"
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@acc_id", accId)

            Dim adapter As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Student record not found in database.", "Error", _
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim row As DataRow = dt.Rows(0)

            ' --- SET LOADING FLAG to suppress section-reassignment side-effect ---
            _isLoading = True

            ' Populate TextBoxes
            Modify_Student_Firstname_TextBox.Text = If(IsDBNull(row("firstname")), "", row("firstname").ToString().Trim())
            Modify_Student_Middlename_TextBox.Text = If(IsDBNull(row("middlename")), "", row("middlename").ToString().Trim())
            Modify_Student_Lastname_TextBox.Text = If(IsDBNull(row("lastname")), "", row("lastname").ToString().Trim())
            Modify_Student_Email_TextBox.Text = If(IsDBNull(row("email")), "", row("email").ToString().Trim())

            ' Populate Gender ComboBox automatically
            Dim gender As String = If(IsDBNull(row("gender")), "", row("gender").ToString().ToLower().Trim())
            Dim genderIdx As Integer = Modify_Student_Gender_ComboBox.FindStringExact(gender)
            Modify_Student_Gender_ComboBox.SelectedIndex = If(genderIdx >= 0, genderIdx, -1)

            ' Populate Course ComboBox automatically
            Dim course As String = If(IsDBNull(row("course")), "", row("course").ToString().Trim())
            Dim courseIdx As Integer = Modify_Student_Course_ComboBox.FindStringExact(course)
            Modify_Student_Course_ComboBox.SelectedIndex = If(courseIdx >= 0, courseIdx, -1)

            ' Capture originals for change-detection
            _originalCourse = course
            _originalYrLvl = If(IsDBNull(row("yr_lvl")), 0, Convert.ToInt32(row("yr_lvl")))
            _originalSection = If(IsDBNull(row("section")), "", row("section").ToString().Trim())

            ' --- RELEASE LOADING FLAG ---
            _isLoading = False

        Catch ex As Exception
            _isLoading = False
            MessageBox.Show("Error loading student data: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
#End Region

#Region "Course ComboBox Change — Section Reassignment"
    ''' <summary>
    ''' Fires when the user changes the Course ComboBox AFTER data has been loaded.
    ''' Skipped entirely during the programmatic load phase (_isLoading = True).
    ''' When the course actually changes from the original, the student will be
    ''' removed from their old section and assigned to an available slot in the
    ''' new course's sections when Modify_Button is clicked.
    ''' (We only preview the new section name here — actual DB write happens in ModifyStudent.)
    ''' </summary>
    Private Sub Modify_Student_Course_ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Student_Course_ComboBox.SelectedIndexChanged
        ' Skip if we are in the middle of loading data programmatically
        If _isLoading Then Return

        ' Also skip if no student has been selected yet
        If _selectedAccId = -1 Then Return

        ' Skip if combo has no valid selection
        If Modify_Student_Course_ComboBox.SelectedIndex = -1 Then Return

        Dim newCourse As String = Modify_Student_Course_ComboBox.SelectedItem.ToString()

        ' If course hasn't actually changed, nothing to preview
        If newCourse = _originalCourse Then Return

        ' Inform the user what will happen on save
        MessageBox.Show("Course changed from '" & _originalCourse & "' to '" & newCourse & "'." & vbCrLf & vbCrLf & _
                        "When you click Modify, the student will be:" & vbCrLf & _
                        "  • Removed from section: " & _originalSection & vbCrLf & _
                        "  • Assigned to an available section in: " & newCourse, _
                        "Course Change Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
#End Region

#Region "Modify Button — Save Changes"
    Private Sub Modify_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Button.Click
        If _selectedAccId = -1 Then
            MessageBox.Show("Please select a student from the list first.", _
                            "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not ValidateModifyFields() Then Return

        ModifyStudent()
    End Sub

    ''' <summary>
    ''' Validates all editable fields before saving.
    ''' Year level is NOT modified per requirements so it is not checked here.
    ''' </summary>
    Private Function ValidateModifyFields() As Boolean
        If String.IsNullOrEmpty(Modify_Student_Lastname_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter student's last name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Student_Lastname_TextBox.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(Modify_Student_Firstname_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter student's first name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Student_Firstname_TextBox.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(Modify_Student_Email_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter student's email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Student_Email_TextBox.Focus()
            Return False
        End If

        If Not IsValidEmail(Modify_Student_Email_TextBox.Text.Trim()) Then
            MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Student_Email_TextBox.Focus()
            Return False
        End If

        If Modify_Student_Gender_ComboBox.SelectedIndex = -1 Then
            MessageBox.Show("Please select student's gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Student_Gender_ComboBox.Focus()
            Return False
        End If

        If Modify_Student_Course_ComboBox.SelectedIndex = -1 Then
            MessageBox.Show("Please select student's course.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Student_Course_ComboBox.Focus()
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

    ''' <summary>
    ''' Main update flow:
    ''' 1. Determine if course changed.
    ''' 2. If course changed → find/create a new section slot.
    ''' 3. Update `account` table (the trigger only fires on INSERT, not UPDATE,
    '''    so we must also update `student` directly).
    ''' 4. Update `student` table.
    ''' 5. Check for duplicate email (excluding this student's own record).
    ''' </summary>
    Private Sub ModifyStudent()
        Try
            Connect_me()

            Dim newCourse As String = Modify_Student_Course_ComboBox.SelectedItem.ToString()
            Dim newFirstname As String = Modify_Student_Firstname_TextBox.Text.Trim()
            Dim newMiddlename As String = Modify_Student_Middlename_TextBox.Text.Trim()
            Dim newLastname As String = Modify_Student_Lastname_TextBox.Text.Trim()
            Dim newEmail As String = Modify_Student_Email_TextBox.Text.Trim()
            Dim newGender As String = Modify_Student_Gender_ComboBox.SelectedItem.ToString()

            ' Check for duplicate email (must exclude the current student's own record)
            If EmailExistsForOther(newEmail, _selectedAccId) Then
                MessageBox.Show("This email address is already registered to another account.", _
                                "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Determine the section to assign
            Dim finalSection As String = _originalSection

            If newCourse <> _originalCourse Then
                ' Course has changed → find/create section in the new course
                Dim newSection As String = GetOrCreateSection(newCourse, _originalYrLvl)

                If String.IsNullOrEmpty(newSection) Then
                    MessageBox.Show("Failed to assign a section for the new course.", _
                                    "Section Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                finalSection = newSection
            End If

            ' --- UPDATE account table ---
            Dim updateAccountQuery As String = _
                "UPDATE account SET " & _
                "firstname = ?, middlename = ?, lastname = ?, " & _
                "email = ?, gender = ?, course = ?, section = ? " & _
                "WHERE acc_id = ?"

            Dim accountCmd As New OdbcCommand(updateAccountQuery, con)
            accountCmd.Parameters.AddWithValue("@firstname", newFirstname)
            accountCmd.Parameters.AddWithValue("@middlename", newMiddlename)
            accountCmd.Parameters.AddWithValue("@lastname", newLastname)
            accountCmd.Parameters.AddWithValue("@email", newEmail)
            accountCmd.Parameters.AddWithValue("@gender", newGender)
            accountCmd.Parameters.AddWithValue("@course", newCourse)
            accountCmd.Parameters.AddWithValue("@section", finalSection)
            accountCmd.Parameters.AddWithValue("@acc_id", _selectedAccId)

            Dim accountRows As Integer = accountCmd.ExecuteNonQuery()

            If accountRows = 0 Then
                MessageBox.Show("No account record was updated. Please try again.", _
                                "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' --- UPDATE student table (trigger does NOT fire on UPDATE) ---
            ' Use acc_id as the link because stud_id may be -1 for legacy rows
            Dim updateStudentQuery As String = _
                "UPDATE student SET " & _
                "firstname = ?, middlename = ?, lastname = ?, " & _
                "email = ?, gender = ?, course = ?, section = ? " & _
                "WHERE acc_id = ?"

            Dim studentCmd As New OdbcCommand(updateStudentQuery, con)
            studentCmd.Parameters.AddWithValue("@firstname", newFirstname)
            studentCmd.Parameters.AddWithValue("@middlename", newMiddlename)
            studentCmd.Parameters.AddWithValue("@lastname", newLastname)
            studentCmd.Parameters.AddWithValue("@email", newEmail)
            studentCmd.Parameters.AddWithValue("@gender", newGender)
            studentCmd.Parameters.AddWithValue("@course", newCourse)
            studentCmd.Parameters.AddWithValue("@section", finalSection)
            studentCmd.Parameters.AddWithValue("@acc_id", _selectedAccId)

            studentCmd.ExecuteNonQuery()

            ' Build success message
            Dim successMsg As String = "Student record updated successfully!" & vbCrLf & vbCrLf & _
                                       "Name: " & newFirstname & " " & newLastname & vbCrLf & _
                                       "Email: " & newEmail & vbCrLf & _
                                       "Course: " & newCourse & vbCrLf & _
                                       "Section: " & finalSection

            If newCourse <> _originalCourse Then
                successMsg &= vbCrLf & vbCrLf & _
                              "(Moved from section: " & _originalSection & ")"
            End If

            MessageBox.Show(successMsg, "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh the DataGridView and reset the form fields
            ClearFields()
            LoadStudents(Modify_Student_Search_Name_Textbox.Text)

            ' Reset selection tracking
            _selectedAccId = -1
            _selectedStudId = -1
            _originalCourse = String.Empty
            _originalSection = String.Empty
            _originalYrLvl = 0

        Catch ex As Exception
            MessageBox.Show("Error updating student: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
#End Region

#Region "Helper — Email Duplicate Check"
    ''' <summary>
    ''' Returns TRUE if <paramref name="email"/> already exists in `account`
    ''' for a record OTHER than <paramref name="currentAccId"/>.
    ''' This prevents false positives when a student keeps the same email.
    ''' </summary>
    Private Function EmailExistsForOther(ByVal email As String, ByVal currentAccId As Integer) As Boolean
        Try
            Dim query As String = "SELECT COUNT(*) FROM account WHERE email = ? AND acc_id <> ?"
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@email", email)
            cmd.Parameters.AddWithValue("@acc_id", currentAccId)
            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
        Catch ex As Exception
            MessageBox.Show("Error checking email: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
#End Region

#Region "Helper — Section Assignment"
    ''' <summary>
    ''' Identical logic to popUpFormAddStudent.GetOrCreateSection().
    ''' Finds a section for <paramref name="course"/> + <paramref name="yearLevel"/>
    ''' with fewer than 45 students, or creates a new one.
    ''' Returns the section name string, or String.Empty on failure.
    ''' </summary>
    Private Function GetOrCreateSection(ByVal course As String, ByVal yearLevel As Integer) As String
        Try
            Dim sectionNumber As Integer = 1
            Dim maxStudentsPerSection As Integer = 45

            Do While sectionNumber <= 100
                Dim sectionName As String = course & " " & yearLevel.ToString() & "-" & sectionNumber.ToString()

                ' Does this section already exist?
                Dim checkQuery As String = "SELECT section_id FROM section WHERE section = ? AND year_lvl = ?"
                Dim checkCmd As New OdbcCommand(checkQuery, con)
                checkCmd.Parameters.AddWithValue("@section", sectionName)
                checkCmd.Parameters.AddWithValue("@year_lvl", yearLevel)

                Dim result As Object = checkCmd.ExecuteScalar()

                If result Is Nothing OrElse IsDBNull(result) Then
                    ' Section does not exist — create it and return its name
                    Dim createQuery As String = "INSERT INTO section (year_lvl, section) VALUES (?, ?)"
                    Dim createCmd As New OdbcCommand(createQuery, con)
                    createCmd.Parameters.AddWithValue("@year_lvl", yearLevel)
                    createCmd.Parameters.AddWithValue("@section", sectionName)
                    createCmd.ExecuteNonQuery()
                    Return sectionName
                End If

                ' Section exists — check student count
                Dim countQuery As String = "SELECT COUNT(*) FROM student WHERE section = ?"
                Dim countCmd As New OdbcCommand(countQuery, con)
                countCmd.Parameters.AddWithValue("@section", sectionName)
                Dim studentCount As Integer = Convert.ToInt32(countCmd.ExecuteScalar())

                If studentCount < maxStudentsPerSection Then
                    Return sectionName
                End If

                sectionNumber += 1
            Loop

            Return String.Empty

        Catch ex As Exception
            MessageBox.Show("Error managing section: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return String.Empty
        End Try
    End Function
#End Region

#Region "Cancel Button"
    Private Sub Modify_Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Cancel_Button.Click
        Me.Close()
    End Sub
#End Region

#Region "Clear Fields"
    ''' <summary>
    ''' Resets all input controls to their default/empty state.
    ''' </summary>
    Private Sub ClearFields()
        Modify_Student_Lastname_TextBox.Clear()
        Modify_Student_Firstname_TextBox.Clear()
        Modify_Student_Middlename_TextBox.Clear()
        Modify_Student_Email_TextBox.Clear()
        Modify_Student_Gender_ComboBox.SelectedIndex = -1
        Modify_Student_Course_ComboBox.SelectedIndex = -1
    End Sub
#End Region

#Region "Unused Event Stubs (required by form designer)"
    Private Sub Modify_Student_Email_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Student_Email_TextBox.TextChanged
    End Sub

    Private Sub Modify_Student_Gender_ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Student_Gender_ComboBox.SelectedIndexChanged
    End Sub

    Private Sub Modify_Student_Middlename_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Student_Middlename_TextBox.TextChanged
    End Sub

    Private Sub Modify_Student_Firstname_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Student_Firstname_TextBox.TextChanged
    End Sub

    Private Sub Modify_Student_Lastname_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Student_Lastname_TextBox.TextChanged
    End Sub
#End Region

End Class