Imports System.Data.Odbc

Public Class Admin_Form

#Region "Shared State - Selected Student ID for Modify/Delete popups"
    Public Shared SelectedStudentId As Integer = 0
#End Region

#Region "Teacher Panel - Shared State"
    ' Tracks which professor is currently being drilled into
    Private _selectedProfId As Integer = 0
    ' Tracks whether we are in the subject-detail view or the main list view
    Private _isInSubjectView As Boolean = False
#End Region

#Region "Form Load"
    Private Sub Admin_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ShowDashboard()
        InitializeStudentDataGridView()
        LoadStudentData("")
    End Sub
#End Region

#Region "Navigation Panel - Button Click Events"
    Private Sub Dashboard_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Dashboard_Button.Click
        ShowDashboard()
    End Sub

    Private Sub Student_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Button.Click
        ShowStudentPanel()
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub

    Private Sub Teacher_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Teacher_Button.Click
        ShowTeacherPanel()
        InitializeProfessorDataGridView()
        LoadProfessorData(Search_Professor_TextBox.Text.Trim())
    End Sub

    Private Sub School_Year_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles School_Year_Button.Click
        ShowSchoolYearPanel()
    End Sub

    Private Sub Logout_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Logout_Button.Click
        Dim result As DialogResult = MessageBox.Show( _
            "Are you sure you want to logout?" & vbCrLf & "You will be redirected to the login screen.", _
            "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Me.Close()
            Dim loginForm As New Login_Form()
            loginForm.Show()
        End If
    End Sub

    Private Sub Exit_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Exit_Button.Click
        Dim result As DialogResult = MessageBox.Show( _
            "Are you sure you want to exit the application?" & vbCrLf & "All unsaved changes will be lost.", _
            "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
#End Region

#Region "Panel Navigation Helper Methods"
    Private Sub HideAllPanels()
        Dashboard_Panel.Visible = False
        Student_Panel.Visible = False
        Teacher_Panel.Visible = False
        School_Year_Panel.Visible = False
    End Sub

    Private Sub ShowDashboard()
        HideAllPanels()
        Dashboard_Panel.Visible = True
        Dashboard_Panel.BringToFront()
    End Sub

    Private Sub ShowStudentPanel()
        HideAllPanels()
        Student_Panel.Visible = True
        Student_Panel.BringToFront()
    End Sub

    Private Sub ShowTeacherPanel()
        HideAllPanels()
        Teacher_Panel.Visible = True
        Teacher_Panel.BringToFront()
    End Sub

    Private Sub ShowSchoolYearPanel()
        HideAllPanels()
        School_Year_Panel.Visible = True
        School_Year_Panel.BringToFront()
    End Sub
#End Region

#Region "Student Panel - Button Click Events"
    Private Sub Add_Student_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Add_Student_Button.Click
        Dim addForm As New popUpFormAddStudent()
        addForm.ShowDialog()
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub

    Private Sub Modify_Student_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Student_Button.Click
        If Student_List_DataGridView.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a student to modify.", "No Selection", _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not SetSelectedStudentId() Then
            Return
        End If

        Dim modifyForm As New popUpFormModifyStudent()
        modifyForm.ShowDialog()
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub

    Private Sub Delete_Student_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_Student_Button.Click
        If Student_List_DataGridView.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a student to delete.", "No Selection", _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not SetSelectedStudentId() Then
            Return
        End If

        Dim deleteForm As New popUpFormDeleteStudent()
        deleteForm.ShowDialog()
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub

    Private Function SetSelectedStudentId() As Boolean
        Try
            Dim selectedRow As DataGridViewRow = Student_List_DataGridView.SelectedRows(0)
            Dim rawId As Object = selectedRow.Cells("StudentID").Value

            If rawId Is Nothing OrElse IsDBNull(rawId) Then
                MessageBox.Show("Could not retrieve student ID. Please re-select the student.", _
                                "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            SelectedStudentId = Convert.ToInt32(rawId)
            Return True

        Catch ex As Exception
            MessageBox.Show("Error reading selected student: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
#End Region

#Region "Student Panel - Search Functionality"
    Private Sub Search_Student_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search_Student_TextBox.TextChanged
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub
#End Region

#Region "Student Panel - DataGridView Configuration"
    Private Sub InitializeStudentDataGridView()
        Try
            Student_List_DataGridView.ReadOnly = True
            Student_List_DataGridView.AllowUserToAddRows = False
            Student_List_DataGridView.AllowUserToDeleteRows = False
            Student_List_DataGridView.EditMode = DataGridViewEditMode.EditProgrammatically
            Student_List_DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Student_List_DataGridView.MultiSelect = False
            Student_List_DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Student_List_DataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray
        Catch ex As Exception
            MessageBox.Show("Error initializing DataGridView: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Student Panel - Data Loading"
    Public Sub LoadStudentData(ByVal searchText As String)
        Try
            Connect_me()

            Dim query As String = _
                "SELECT " & _
                "  s.stud_id AS StudentID, " & _
                "  CONCAT(s.lastname, ', ', s.firstname, " & _
                "         IF(s.middlename IS NULL OR s.middlename = '', '', CONCAT(' ', s.middlename))) AS FullName, " & _
                "  s.gender   AS Gender, " & _
                "  s.course   AS Course, " & _
                "  s.yr_lvl   AS YearLevel, " & _
                "  IFNULL(sec.section, 'No Section') AS Section " & _
                "FROM student s " & _
                "LEFT JOIN section sec ON s.section_id = sec.section_id " & _
                "WHERE s.role = 'student' " & _
                "  AND CONCAT(s.firstname, ' ', IFNULL(s.middlename, ''), ' ', s.lastname) LIKE ? " & _
                "ORDER BY s.lastname, s.firstname"

            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")

            Dim adapter As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Student_List_DataGridView.DataSource = dt

            If Student_List_DataGridView.Columns.Contains("StudentID") Then
                Student_List_DataGridView.Columns("StudentID").Visible = False
            End If
            If Student_List_DataGridView.Columns.Contains("FullName") Then
                Student_List_DataGridView.Columns("FullName").HeaderText = "Full Name"
            End If
            If Student_List_DataGridView.Columns.Contains("Gender") Then
                Student_List_DataGridView.Columns("Gender").HeaderText = "Gender"
            End If
            If Student_List_DataGridView.Columns.Contains("Course") Then
                Student_List_DataGridView.Columns("Course").HeaderText = "Course"
            End If
            If Student_List_DataGridView.Columns.Contains("YearLevel") Then
                Student_List_DataGridView.Columns("YearLevel").HeaderText = "Year Level"
            End If
            If Student_List_DataGridView.Columns.Contains("Section") Then
                Student_List_DataGridView.Columns("Section").HeaderText = "Section"
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading student data: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
#End Region

#Region "Student Panel - DataGridView Events"
    Private Sub Student_List_DataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Student_List_DataGridView.CellContentClick
    End Sub
#End Region

    ' ══════════════════════════════════════════════════════════════════
    '  TEACHER PANEL — ALL CODE BELOW
    ' ══════════════════════════════════════════════════════════════════

#Region "Teacher Panel - DataGridView Initialization"
    ''' <summary>
    ''' Sets up the Professor_DataGridView to be read-only and full-row-select.
    ''' Called once when the Teacher panel is first shown.
    ''' </summary>
    Private Sub InitializeProfessorDataGridView()
        Try
            Professor_DataGridView.ReadOnly = True
            Professor_DataGridView.AllowUserToAddRows = False
            Professor_DataGridView.AllowUserToDeleteRows = False
            Professor_DataGridView.EditMode = DataGridViewEditMode.EditProgrammatically
            Professor_DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Professor_DataGridView.MultiSelect = False
            Professor_DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Professor_DataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightCyan

            ' Make sure Back button is hidden when on the main list view
            Back_Button.Visible = False
            _isInSubjectView = False
            _selectedProfId = 0
        Catch ex As Exception
            MessageBox.Show("Error initializing Professor DataGridView: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Teacher Panel - Load Professor List (Default View)"
    ''' <summary>
    ''' Default view: shows FullName and Gender columns only.
    ''' Also hides a hidden ProfID column for internal use.
    ''' Filtered by searchText (empty = show all).
    ''' </summary>
    Public Sub LoadProfessorData(ByVal searchText As String)
        Try
            Connect_me()

            Dim query As String = _
                "SELECT " & _
                "  p.prof_id AS ProfID, " & _
                "  CONCAT(p.lastname, ', ', p.firstname, " & _
                "         IF(p.middlename IS NULL OR p.middlename = '', '', CONCAT(' ', p.middlename))) AS FullName, " & _
                "  p.gender AS Gender " & _
                "FROM prof p " & _
                "WHERE CONCAT(p.firstname, ' ', IFNULL(p.middlename, ''), ' ', p.lastname) LIKE ? " & _
                "ORDER BY p.lastname, p.firstname"

            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")

            Dim adapter As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Professor_DataGridView.DataSource = dt

            ' Hide the internal ID column — we use it on row click
            If Professor_DataGridView.Columns.Contains("ProfID") Then
                Professor_DataGridView.Columns("ProfID").Visible = False
            End If

            ' Set friendly headers
            If Professor_DataGridView.Columns.Contains("FullName") Then
                Professor_DataGridView.Columns("FullName").HeaderText = "Full Name"
            End If
            If Professor_DataGridView.Columns.Contains("Gender") Then
                Professor_DataGridView.Columns("Gender").HeaderText = "Gender"
            End If

            ' Ensure Back button is hidden — we are on the main list
            Back_Button.Visible = False
            _isInSubjectView = False

        Catch ex As Exception
            MessageBox.Show("Error loading teacher data: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
#End Region

#Region "Teacher Panel - Load Professor Subject-Detail View"
    ''' <summary>
    ''' Drill-down view: when a professor row is clicked, show
    ''' their assigned subjects with section info and subject code.
    ''' </summary>
    Private Sub LoadProfessorSubjects(ByVal profId As Integer)
        Try
            Connect_me()

            Dim query As String = _
                "SELECT " & _
                "  pss.id          AS AssignID, " & _
                "  sub.sub_code    AS SubjectCode, " & _
                "  sub.sub_name    AS SubjectName, " & _
                "  sec.section     AS Section " & _
                "FROM profsectionsubject pss " & _
                "INNER JOIN subject sub ON pss.sub_id = sub.sub_id " & _
                "INNER JOIN section sec ON pss.section_id = sec.section_id " & _
                "WHERE pss.prof_id = ? " & _
                "ORDER BY sec.section, sub.sub_name"

            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@profId", profId)

            Dim adapter As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Professor_DataGridView.DataSource = dt

            ' Hide internal AssignID
            If Professor_DataGridView.Columns.Contains("AssignID") Then
                Professor_DataGridView.Columns("AssignID").Visible = False
            End If

            ' Set friendly headers
            If Professor_DataGridView.Columns.Contains("SubjectCode") Then
                Professor_DataGridView.Columns("SubjectCode").HeaderText = "Subject Code"
            End If
            If Professor_DataGridView.Columns.Contains("SubjectName") Then
                Professor_DataGridView.Columns("SubjectName").HeaderText = "Subject Name"
            End If
            If Professor_DataGridView.Columns.Contains("Section") Then
                Professor_DataGridView.Columns("Section").HeaderText = "Section"
            End If

            ' Show Back button — user is now in subject-detail view
            Back_Button.Visible = True
            _isInSubjectView = True

        Catch ex As Exception
            MessageBox.Show("Error loading professor subjects: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
#End Region

#Region "Teacher Panel - DataGridView Row Click (Drill Down)"
    ''' <summary>
    ''' When a row is clicked in the main professor list view,
    ''' switch to the subject-detail view for that professor.
    ''' Does nothing if we are already in subject-detail view.
    ''' </summary>
    Private Sub Professor_DataGridView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Professor_DataGridView.CellClick
        ' Ignore header row clicks
        If e.RowIndex < 0 Then Return

        ' Only drill down from the main list — not from subject view
        If _isInSubjectView Then Return

        Try
            Dim selectedRow As DataGridViewRow = Professor_DataGridView.Rows(e.RowIndex)
            Dim rawId As Object = selectedRow.Cells("ProfID").Value

            If rawId Is Nothing OrElse IsDBNull(rawId) Then
                MessageBox.Show("Could not retrieve professor ID. Please re-select.", _
                                "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            _selectedProfId = Convert.ToInt32(rawId)
            LoadProfessorSubjects(_selectedProfId)

        Catch ex As Exception
            MessageBox.Show("Error selecting professor: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Keep the old CellContentClick stub — no-op, logic moved to CellClick above
    Private Sub Professor_DataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Professor_DataGridView.CellContentClick
    End Sub
#End Region

#Region "Teacher Panel - Search Box"
    ''' <summary>
    ''' Live search: only active in the main list view.
    ''' If in subject-detail view, search is ignored (user must press Back first).
    ''' </summary>
    Private Sub Search_Professor_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search_Professor_TextBox.TextChanged
        If _isInSubjectView Then Return
        LoadProfessorData(Search_Professor_TextBox.Text.Trim())
    End Sub
#End Region

#Region "Teacher Panel - Back Button"
    ''' <summary>
    ''' Returns from the subject-detail view back to the main professor list.
    ''' Clears the selected professor ID and hides the Back button.
    ''' </summary>
    Private Sub Back_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Back_Button.Click
        _selectedProfId = 0
        _isInSubjectView = False
        Back_Button.Visible = False
        Search_Professor_TextBox.Text = ""
        LoadProfessorData("")
    End Sub
#End Region

#Region "Teacher Panel - CRUD Button Click Events"
    ''' <summary>
    ''' Opens the Add Teacher popup, then refreshes the list.
    ''' </summary>
    Private Sub Add_Professor_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Add_Professor_Button.Click
        Dim addForm As New popUpFormAddTeacher()
        addForm.ShowDialog()
        ' After add, return to main list and refresh
        _selectedProfId = 0
        _isInSubjectView = False
        Back_Button.Visible = False
        LoadProfessorData(Search_Professor_TextBox.Text.Trim())
    End Sub

    ''' <summary>
    ''' Opens the Modify Teacher popup.
    ''' Requires a row to be selected AND we must be in the main list view.
    ''' </summary>
    Private Sub Modify_Professor_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Professor_Button.Click
        ' If in subject-detail view, force user back to list first
        If _isInSubjectView Then
            MessageBox.Show("Please press Back to return to the teacher list before modifying.", _
                            "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If Professor_DataGridView.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a teacher to modify.", "No Selection", _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not SetSelectedProfId() Then Return

        Dim modifyForm As New popUpFormModifyTeacher()
        modifyForm.ShowDialog()
        LoadProfessorData(Search_Professor_TextBox.Text.Trim())
    End Sub

    ''' <summary>
    ''' Opens the Delete Teacher popup.
    ''' Requires a row to be selected AND we must be in the main list view.
    ''' </summary>
    Private Sub Delete_Professor_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_Professor_Button.Click
        If _isInSubjectView Then
            MessageBox.Show("Please press Back to return to the teacher list before deleting.", _
                            "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If Professor_DataGridView.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a teacher to delete.", "No Selection", _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not SetSelectedProfId() Then Return

        Dim deleteForm As New popUpFormDeleteTeacher()
        deleteForm.ShowDialog()
        LoadProfessorData(Search_Professor_TextBox.Text.Trim())
    End Sub

    ''' <summary>
    ''' Opens the Assign Subject popup.
    ''' Works from EITHER view:
    '''   - From main list: uses the selected row's ProfID.
    '''   - From subject-detail view: uses _selectedProfId already stored.
    ''' Also enforces the 7-subject limit before opening the popup.
    ''' </summary>
    Private Sub Assign_Subject_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Assign_Subject_Button.Click
        Dim targetProfId As Integer = 0

        If _isInSubjectView Then
            ' Already drilled in — use the stored professor ID
            targetProfId = _selectedProfId
        Else
            ' Must have a row selected
            If Professor_DataGridView.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a teacher to assign subjects to.", _
                                "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If Not SetSelectedProfId() Then Return
            targetProfId = _selectedProfId
        End If

        ' ── Enforce the 7-subject-per-professor limit ──────────────────
        If Not CheckProfSubjectLimit(targetProfId) Then Return
        ' ──────────────────────────────────────────────────────────────

        Dim assignForm As New popUpFormAssignTeacher()
        assignForm.ShowDialog()

        ' Refresh: if we were in subject view, reload subjects; else reload list
        If _isInSubjectView Then
            LoadProfessorSubjects(_selectedProfId)
        Else
            LoadProfessorData(Search_Professor_TextBox.Text.Trim())
        End If
    End Sub
#End Region

#Region "Teacher Panel - Helper Methods"
    ''' <summary>
    ''' Reads the ProfID from the currently selected DataGridView row
    ''' and stores it in _selectedProfId.
    ''' Returns True on success, False on failure.
    ''' </summary>
    Private Function SetSelectedProfId() As Boolean
        Try
            Dim selectedRow As DataGridViewRow = Professor_DataGridView.SelectedRows(0)
            Dim rawId As Object = selectedRow.Cells("ProfID").Value

            If rawId Is Nothing OrElse IsDBNull(rawId) Then
                MessageBox.Show("Could not retrieve teacher ID. Please re-select the teacher.", _
                                "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            _selectedProfId = Convert.ToInt32(rawId)
            Return True

        Catch ex As Exception
            MessageBox.Show("Error reading selected teacher: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks whether the professor already has 7 subjects assigned.
    ''' Returns True if they are under the limit (safe to assign more).
    ''' Returns False if the limit has been reached (blocks the popup).
    ''' </summary>
    Private Function CheckProfSubjectLimit(ByVal profId As Integer) As Boolean
        Try
            Connect_me()

            Dim query As String = _
                "SELECT COUNT(*) FROM profsectionsubject WHERE prof_id = ?"

            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@profId", profId)

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If count >= 7 Then
                MessageBox.Show( _
                    "This teacher has already reached the maximum of 7 subject assignments." & vbCrLf & _
                    "Please remove an existing assignment before adding a new one.", _
                    "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Return True

        Catch ex As Exception
            MessageBox.Show("Error checking subject limit: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Function
#End Region

End Class