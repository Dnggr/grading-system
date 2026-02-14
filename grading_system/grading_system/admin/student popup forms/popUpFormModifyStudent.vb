Imports System.Data.Odbc

Public Class popUpFormModifyStudent

#Region "Module-Level Variables"
    ' Stores the acc_id and stud_id of the student currently selected in the DataGridView
    Private _selectedAccId As Integer = -1
    Private _selectedStudId As Integer = -1

    ' Stores the ORIGINAL course, yr_lvl, and section_id loaded from DB
    Private _originalCourse As String = String.Empty
    Private _originalYrLvl As Integer = 0
    Private _originalSectionId As Integer = -1

    ' Flag: TRUE while we are programmatically populating fields
    Private _isLoading As Boolean = False
#End Region

#Region "Form Load"
    Private Sub popUpFormModifyStudent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitializeComboBoxes()
        SetupDataGridView()
        LoadAllStudents()
    End Sub

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
    End Sub

    ''' <summary>
    ''' UPDATED: DataGridView now shows section name via JOIN with section table
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
            .Columns.Clear()

            ' Hidden column: acc_id
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

            ' Hidden column: section_id
            Dim colSectionId As New DataGridViewTextBoxColumn()
            colSectionId.Name = "colSectionId"
            colSectionId.HeaderText = "section_id"
            colSectionId.DataPropertyName = "section_id"
            colSectionId.Visible = False
            .Columns.Add(colSectionId)

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
    Private Sub LoadAllStudents()
        LoadStudents(String.Empty)
    End Sub

    ''' <summary>
    ''' UPDATED: Now JOINs student with section table using section_id foreign key
    ''' </summary>
    Private Sub LoadStudents(ByVal searchName As String)
        Try
            Connect_me()

            Dim query As String
            Dim cmd As OdbcCommand

            If String.IsNullOrEmpty(searchName.Trim()) Then
                query = "SELECT s.stud_id, s.acc_id, s.section_id, " & _
                        "CONCAT(TRIM(s.firstname), ' ', TRIM(IFNULL(s.middlename,'')), ' ', TRIM(s.lastname)) AS fullname, " & _
                        "IFNULL(sec.section, 'No Section') AS section " & _
                        "FROM student s " & _
                        "LEFT JOIN section sec ON s.section_id = sec.section_id " & _
                        "WHERE s.role = 'student' " & _
                        "ORDER BY s.lastname, s.firstname"
                cmd = New OdbcCommand(query, con)
            Else
                query = "SELECT s.stud_id, s.acc_id, s.section_id, " & _
                        "CONCAT(TRIM(s.firstname), ' ', TRIM(IFNULL(s.middlename,'')), ' ', TRIM(s.lastname)) AS fullname, " & _
                        "IFNULL(sec.section, 'No Section') AS section " & _
                        "FROM student s " & _
                        "LEFT JOIN section sec ON s.section_id = sec.section_id " & _
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

    Private Sub Modify_Student_Search_Name_Textbox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Student_Search_Name_Textbox.TextChanged
        LoadStudents(Modify_Student_Search_Name_Textbox.Text)
    End Sub
#End Region

#Region "DataGridView — Row Click"
    Private Sub Modify_Student_DataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Modify_Student_DataGridView.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim row As DataGridViewRow = Modify_Student_DataGridView.Rows(e.RowIndex)

        Dim accIdVal As Object = row.Cells("colAccId").Value
        Dim studIdVal As Object = row.Cells("colStudId").Value
        Dim fullNameVal As Object = row.Cells("colFullName").Value

        If accIdVal Is Nothing OrElse IsDBNull(accIdVal) Then
            MessageBox.Show("Cannot identify selected student (missing account ID).", _
                            "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim fullName As String = If(fullNameVal IsNot Nothing, fullNameVal.ToString().Trim(), "this student")

        Dim confirm As DialogResult = MessageBox.Show( _
            "You are about to modify the record of:" & vbCrLf & vbCrLf & _
            fullName & vbCrLf & vbCrLf & _
            "Do you want to continue?", _
            "Confirm Modification", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question)

        If confirm <> DialogResult.Yes Then Return

        _selectedAccId = Convert.ToInt32(accIdVal)
        _selectedStudId = If(studIdVal IsNot Nothing AndAlso Not IsDBNull(studIdVal), Convert.ToInt32(studIdVal), -1)

        LoadStudentDataIntoFields(_selectedAccId)
    End Sub
#End Region

#Region "Load Student Data Into Fields"
    ''' <summary>
    ''' UPDATED: Now loads section_id from student table and displays section name
    ''' </summary>
    Private Sub LoadStudentDataIntoFields(ByVal accId As Integer)
        Try
            Connect_me()

            Dim query As String = "SELECT s.firstname, s.middlename, s.lastname, s.email, s.gender, " & _
                                  "s.course, s.yr_lvl, s.section_id, IFNULL(sec.section, 'No Section') AS section_name " & _
                                  "FROM student s " & _
                                  "LEFT JOIN section sec ON s.section_id = sec.section_id " & _
                                  "WHERE s.acc_id = ?"
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

            _isLoading = True

            Modify_Student_Firstname_TextBox.Text = If(IsDBNull(row("firstname")), "", row("firstname").ToString().Trim())
            Modify_Student_Middlename_TextBox.Text = If(IsDBNull(row("middlename")), "", row("middlename").ToString().Trim())
            Modify_Student_Lastname_TextBox.Text = If(IsDBNull(row("lastname")), "", row("lastname").ToString().Trim())
            Modify_Student_Email_TextBox.Text = If(IsDBNull(row("email")), "", row("email").ToString().Trim())

            Dim gender As String = If(IsDBNull(row("gender")), "", row("gender").ToString().ToLower().Trim())
            Dim genderIdx As Integer = Modify_Student_Gender_ComboBox.FindStringExact(gender)
            Modify_Student_Gender_ComboBox.SelectedIndex = If(genderIdx >= 0, genderIdx, -1)

            Dim course As String = If(IsDBNull(row("course")), "", row("course").ToString().Trim())
            Dim courseIdx As Integer = Modify_Student_Course_ComboBox.FindStringExact(course)
            Modify_Student_Course_ComboBox.SelectedIndex = If(courseIdx >= 0, courseIdx, -1)

            _originalCourse = course
            _originalYrLvl = If(IsDBNull(row("yr_lvl")), 0, Convert.ToInt32(row("yr_lvl")))
            _originalSectionId = If(IsDBNull(row("section_id")), -1, Convert.ToInt32(row("section_id")))

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
    ''' UPDATED: Now previews section name change based on section_id
    ''' </summary>
    Private Sub Modify_Student_Course_ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Student_Course_ComboBox.SelectedIndexChanged
        If _isLoading Then Return
        If _selectedAccId = -1 Then Return
        If Modify_Student_Course_ComboBox.SelectedIndex = -1 Then Return

        Dim newCourse As String = Modify_Student_Course_ComboBox.SelectedItem.ToString()

        If newCourse = _originalCourse Then Return

        Dim originalSectionName As String = GetSectionNameById(_originalSectionId)

        MessageBox.Show("Course changed from '" & _originalCourse & "' to '" & newCourse & "'." & vbCrLf & vbCrLf & _
                        "When you click Modify, the student will be:" & vbCrLf & _
                        "  • Removed from section: " & originalSectionName & vbCrLf & _
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
    ''' UPDATED: Now updates section_id instead of section name string
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

            If EmailExistsForOther(newEmail, _selectedAccId) Then
                MessageBox.Show("This email address is already registered to another account.", _
                                "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim finalSectionId As Integer = _originalSectionId

            If newCourse <> _originalCourse Then
                Dim newSectionId As Integer = GetOrCreateSectionId(newCourse, _originalYrLvl)

                If newSectionId = -1 Then
                    MessageBox.Show("Failed to assign a section for the new course.", _
                                    "Section Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                finalSectionId = newSectionId
            End If

            Dim finalSectionName As String = GetSectionNameById(finalSectionId)

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
            accountCmd.Parameters.AddWithValue("@section", finalSectionName)
            accountCmd.Parameters.AddWithValue("@acc_id", _selectedAccId)

            Dim accountRows As Integer = accountCmd.ExecuteNonQuery()

            If accountRows = 0 Then
                MessageBox.Show("No account record was updated. Please try again.", _
                                "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' --- UPDATE student table with section_id ---
            Dim updateStudentQuery As String = _
                "UPDATE student SET " & _
                "firstname = ?, middlename = ?, lastname = ?, " & _
                "email = ?, gender = ?, course = ?, section_id = ? " & _
                "WHERE acc_id = ?"

            Dim studentCmd As New OdbcCommand(updateStudentQuery, con)
            studentCmd.Parameters.AddWithValue("@firstname", newFirstname)
            studentCmd.Parameters.AddWithValue("@middlename", newMiddlename)
            studentCmd.Parameters.AddWithValue("@lastname", newLastname)
            studentCmd.Parameters.AddWithValue("@email", newEmail)
            studentCmd.Parameters.AddWithValue("@gender", newGender)
            studentCmd.Parameters.AddWithValue("@course", newCourse)
            studentCmd.Parameters.AddWithValue("@section_id", finalSectionId)
            studentCmd.Parameters.AddWithValue("@acc_id", _selectedAccId)

            studentCmd.ExecuteNonQuery()

            Dim successMsg As String = "Student record updated successfully!" & vbCrLf & vbCrLf & _
                                       "Name: " & newFirstname & " " & newLastname & vbCrLf & _
                                       "Email: " & newEmail & vbCrLf & _
                                       "Course: " & newCourse & vbCrLf & _
                                       "Section: " & finalSectionName

            If newCourse <> _originalCourse Then
                Dim originalSectionName As String = GetSectionNameById(_originalSectionId)
                successMsg &= vbCrLf & vbCrLf & _
                              "(Moved from section: " & originalSectionName & ")"
            End If

            MessageBox.Show(successMsg, "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ClearFields()
            LoadStudents(Modify_Student_Search_Name_Textbox.Text)

            _selectedAccId = -1
            _selectedStudId = -1
            _originalCourse = String.Empty
            _originalSectionId = -1
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
    ''' UPDATED: Returns section_id instead of section name
    ''' </summary>
    Private Function GetOrCreateSectionId(ByVal course As String, ByVal yearLevel As Integer) As Integer
        Try
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

                    Dim getIdQuery As String = "SELECT section_id FROM section WHERE section = ? AND year_lvl = ?"
                    Dim getIdCmd As New OdbcCommand(getIdQuery, con)
                    getIdCmd.Parameters.AddWithValue("@section", sectionName)
                    getIdCmd.Parameters.AddWithValue("@year_lvl", yearLevel)
                    Return Convert.ToInt32(getIdCmd.ExecuteScalar())
                End If

                Dim sectionId As Integer = Convert.ToInt32(result)

                Dim countQuery As String = "SELECT COUNT(*) FROM student WHERE section_id = ?"
                Dim countCmd As New OdbcCommand(countQuery, con)
                countCmd.Parameters.AddWithValue("@section_id", sectionId)

                Dim studentCount As Integer = Convert.ToInt32(countCmd.ExecuteScalar())

                If studentCount < maxStudentsPerSection Then
                    Return sectionId
                End If

                sectionNumber += 1
            Loop

            Return -1

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
            Return "No Section"
        Catch ex As Exception
            Return "No Section"
        End Try
    End Function
#End Region

#Region "Cancel Button"
    Private Sub Modify_Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Cancel_Button.Click
        Me.Close()
    End Sub
#End Region

#Region "Clear Fields"
    Private Sub ClearFields()
        Modify_Student_Lastname_TextBox.Clear()
        Modify_Student_Firstname_TextBox.Clear()
        Modify_Student_Middlename_TextBox.Clear()
        Modify_Student_Email_TextBox.Clear()
        Modify_Student_Gender_ComboBox.SelectedIndex = -1
        Modify_Student_Course_ComboBox.SelectedIndex = -1
    End Sub
#End Region

#Region "Unused Event Stubs"
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