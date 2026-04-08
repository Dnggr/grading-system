Imports System.Data.Odbc

Public Class Admin_Form

#Region "Shared State"
    ' ── Student shared state (read by popUpFormModifyStudent / popUpFormDeleteStudent) ──
    Public Shared SelectedStudentAccId As Integer = 0
    Public Shared SelectedStudentStudId As Integer = 0

    ' ── Teacher shared state (read by popUpFormModifyTeacher / popUpFormDeleteTeacher / popUpFormAssignSubjectToTeacher) ──
    Public Shared SelectedTeacherAccId As Integer = 0
    Public Shared SelectedTeacherProfId As Integer = 0

    ' ── Tracks which view the Teacher DataGridView is in ──
    Private _teacherViewMode As String = "LIST"   ' "LIST" or "SUBJECTS"
#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Form Load"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub Admin_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Default view on startup
        ShowDashboard()

        ' ── Student panel setup ──
        InitializeStudentDataGridView()
        LoadStudentData("")

        ' ── Teacher panel setup ──
        InitializeTeacherDataGridView()
        Back_Button.Visible = False   ' Hidden until drill-down is active
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Navigation Panel - Button Click Events"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub Dashboard_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Dashboard_Button.Click
        ShowDashboard()
    End Sub

    Private Sub Student_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Button.Click
        ShowStudentPanel()
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub

    Private Sub Teacher_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Teacher_Button.Click
        ShowTeacherPanel()
        LoadTeacherData(Search_Teacher_TextBox.Text.Trim())
    End Sub

    Private Sub School_Year_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles School_Year_Button.Click
        Dim semForm As New frm_SemControl()
        semForm.ShowDialog()
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

    ' ════════════════════════════════════════════════════════════════════
#Region "Panel Navigation Helper Methods"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub HideAllPanels()
        Dashboard_Panel.Visible = False
        Student_Panel.Visible = False
        Teacher_Panel.Visible = False
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

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Student Panel - DataGridView Initialization"
    ' ════════════════════════════════════════════════════════════════════

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
            MessageBox.Show("Error initializing Student DataGridView: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Student Panel - Data Loading"
    ' ════════════════════════════════════════════════════════════════════

    Public Sub LoadStudentData(ByVal searchText As String)
        Try
            Connect_me()

            Dim query As String = _
                "SELECT " & _
                "  s.acc_id     AS AccountID, " & _
                "  s.stud_id    AS StudentID, " & _
                "  CONCAT(s.lastname, ', ', s.firstname, " & _
                "         IF(s.middlename IS NULL OR TRIM(s.middlename) = '', " & _
                "            '', CONCAT(' ', s.middlename))) AS FullName, " & _
                "  IFNULL(s.gender,  'N/A')          AS Gender, " & _
                "  IFNULL(s.course,  'N/A')          AS Course, " & _
                "  IFNULL(s.yr_lvl,  0)              AS YearLevel, " & _
                "  IFNULL(sec.section, 'No Section') AS Section, " & _
                "  IFNULL(s.email, '')               AS Email " & _
                "FROM student s " & _
                "LEFT JOIN section sec ON s.section_id = sec.section_id " & _
                "WHERE LOWER(s.role) = 'student' " & _
                "  AND CONCAT( " & _
                "        IFNULL(s.firstname,  ''), ' ', " & _
                "        IFNULL(s.middlename, ''), ' ', " & _
                "        IFNULL(s.lastname,   '')) LIKE ? " & _
                "ORDER BY s.lastname, s.firstname"

            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")

            Dim adapter As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Student_List_DataGridView.DataSource = dt

            ' ── Hide internal ID columns ──
            If Student_List_DataGridView.Columns.Contains("AccountID") Then
                Student_List_DataGridView.Columns("AccountID").Visible = False
            End If
            If Student_List_DataGridView.Columns.Contains("StudentID") Then
                Student_List_DataGridView.Columns("StudentID").Visible = False
            End If
            If Student_List_DataGridView.Columns.Contains("Email") Then
                Student_List_DataGridView.Columns("Email").Visible = False
            End If

            ' ── Friendly column headers ──
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
            MessageBox.Show("Error loading student data: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Student Panel - Button Click Events"
    ' ════════════════════════════════════════════════════════════════════

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

        If Not SetSelectedStudentId() Then Return

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

        If Not SetSelectedStudentId() Then Return

        Dim deleteForm As New popUpFormDeleteStudent()
        deleteForm.ShowDialog()
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub

    ' ════════════════════════════════════════════════════════════════════
    ' ── Refresh Student Button: reloads Student_List_DataGridView ──
    ' ════════════════════════════════════════════════════════════════════
    Private Sub Refresh_student_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Refresh_student_Button.Click
        Try
            LoadStudentData(Search_Student_TextBox.Text.Trim())
        Catch ex As Exception
            MessageBox.Show("Error refreshing student list: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Student Panel - Search"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub Search_Student_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search_Student_TextBox.TextChanged
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Student Panel - Set Selected Student ID Helper"
    ' ════════════════════════════════════════════════════════════════════

    Private Function SetSelectedStudentId() As Boolean
        Try
            Dim selectedRow As DataGridViewRow = Student_List_DataGridView.SelectedRows(0)
            Dim rawAccId As Object = selectedRow.Cells("AccountID").Value
            Dim rawStudId As Object = selectedRow.Cells("StudentID").Value

            If rawAccId Is Nothing OrElse IsDBNull(rawAccId) Then
                MessageBox.Show("Could not retrieve account ID. Please re-select the student.", _
                                "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If rawStudId Is Nothing OrElse IsDBNull(rawStudId) Then
                MessageBox.Show("Could not retrieve student ID. Please re-select the student.", _
                                "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            SelectedStudentAccId = Convert.ToInt32(rawAccId)
            SelectedStudentStudId = Convert.ToInt32(rawStudId)
            Return True

        Catch ex As Exception
            MessageBox.Show("Error reading selected student: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Student Panel - DataGridView Events"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub Student_List_DataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Student_List_DataGridView.CellContentClick
        ' Reserved for future use
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Teacher Panel - DataGridView Initialization"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub InitializeTeacherDataGridView()
        Try
            Teacher_List_DataGridView.ReadOnly = True
            Teacher_List_DataGridView.AllowUserToAddRows = False
            Teacher_List_DataGridView.AllowUserToDeleteRows = False
            Teacher_List_DataGridView.EditMode = DataGridViewEditMode.EditProgrammatically
            Teacher_List_DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Teacher_List_DataGridView.MultiSelect = False
            Teacher_List_DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Teacher_List_DataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray
        Catch ex As Exception
            MessageBox.Show("Error initializing Teacher DataGridView: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Teacher Panel - Data Loading (List View - Default)"
    ' ════════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Default view: shows Full Name, Gender, and Status (Active/Inactive).
    ''' AccountID and ProfID are hidden but available for button actions.
    ''' Filters by role = 'teacher' in the account table.
    ''' Rows with is_active = 0 are highlighted in a different color.
    ''' </summary>
    Public Sub LoadTeacherData(ByVal searchText As String)
        Try
            Connect_me()

            Dim query As String = _
                "SELECT " & _
                "  a.acc_id   AS AccountID, " & _
                "  p.prof_id  AS ProfID, " & _
                "  CONCAT(p.lastname, ', ', p.firstname, " & _
                "         IF(p.middlename IS NULL OR TRIM(p.middlename) = '', " & _
                "            '', CONCAT(' ', p.middlename))) AS FullName, " & _
                "  IFNULL(a.gender, 'N/A') AS Gender, " & _
                "  p.status AS Status " & _
                "FROM prof p " & _
                "INNER JOIN account a ON p.acc_id = a.acc_id " & _
                "WHERE LOWER(a.role) = 'teacher' " & _
                "  AND CONCAT( " & _
                "        IFNULL(p.firstname,  ''), ' ', " & _
                "        IFNULL(p.middlename, ''), ' ', " & _
                "        IFNULL(p.lastname,   '')) LIKE ? " & _
                "ORDER BY p.lastname, p.firstname"

            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")

            Dim adapter As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Teacher_List_DataGridView.DataSource = dt

            ' ── Hide internal ID columns ──
            If Teacher_List_DataGridView.Columns.Contains("AccountID") Then
                Teacher_List_DataGridView.Columns("AccountID").Visible = False
            End If
            If Teacher_List_DataGridView.Columns.Contains("ProfID") Then
                Teacher_List_DataGridView.Columns("ProfID").Visible = False
            End If

            ' ── Friendly column headers ──
            If Teacher_List_DataGridView.Columns.Contains("FullName") Then
                Teacher_List_DataGridView.Columns("FullName").HeaderText = "Full Name"
            End If
            If Teacher_List_DataGridView.Columns.Contains("Gender") Then
                Teacher_List_DataGridView.Columns("Gender").HeaderText = "Gender"
            End If
            If Teacher_List_DataGridView.Columns.Contains("Status") Then
                Teacher_List_DataGridView.Columns("Status").HeaderText = "Status"
            End If

            ' ── Color-code Active / Inactive rows ──
            For Each row As DataGridViewRow In Teacher_List_DataGridView.Rows
                If row.Cells("Status").Value IsNot Nothing Then
                    If row.Cells("Status").Value.ToString().ToLower() = "inactive" Then
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.Gray
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.MistyRose
                    Else
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.White
                    End If
                End If
            Next

            _teacherViewMode = "LIST"
            Back_Button.Visible = False

        Catch ex As Exception
            MessageBox.Show("Error loading teacher data: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Teacher Panel - Data Loading (Subject Drill-Down View)"
    ' ════════════════════════════════════════════════════════════════════

    Public Sub LoadTeacherSubjects(ByVal profId As Integer, ByVal teacherFullName As String)
        Try
            Connect_me()

            Dim query As String = _
                "SELECT " & _
                "  sub.sub_code   AS SubjectCode, " & _
                "  sub.sub_name   AS SubjectName, " & _
                "  sec.section    AS Section " & _
                "FROM profsectionsubject pss " & _
                "INNER JOIN subject  sub ON pss.sub_id     = sub.sub_id " & _
                "INNER JOIN section  sec ON pss.section_id = sec.section_id " & _
                "WHERE pss.prof_id = ? " & _
                "ORDER BY sec.section, sub.sub_code"

            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@profId", profId)

            Dim adapter As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Teacher_List_DataGridView.DataSource = dt

            ' ── Friendly column headers ──
            If Teacher_List_DataGridView.Columns.Contains("SubjectCode") Then
                Teacher_List_DataGridView.Columns("SubjectCode").HeaderText = "Subject Code"
            End If
            If Teacher_List_DataGridView.Columns.Contains("SubjectName") Then
                Teacher_List_DataGridView.Columns("SubjectName").HeaderText = "Subject Name"
            End If
            If Teacher_List_DataGridView.Columns.Contains("Section") Then
                Teacher_List_DataGridView.Columns("Section").HeaderText = "Section"
            End If

            _teacherViewMode = "SUBJECTS"
            Back_Button.Visible = True   ' Show Back button in drill-down view

        Catch ex As Exception
            MessageBox.Show("Error loading teacher subjects: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Teacher Panel - Button Click Events"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub Add_Teacher_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Add_Teacher_Button.Click
        Dim addForm As New popUpFormAddTeacher()
        addForm.ShowDialog()
        LoadTeacherData(Search_Teacher_TextBox.Text.Trim())
    End Sub

    Private Sub Modify_Teacher_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Teacher_Button.Click
        If Teacher_List_DataGridView.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a teacher to modify.", "No Selection", _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If _teacherViewMode = "SUBJECTS" Then
            MessageBox.Show("Please press Back to return to the teacher list before modifying.", _
                            "Wrong View", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If Not SetSelectedTeacherId() Then Return

        Dim modifyForm As New popUpFormModifyTeacher()
        modifyForm.ShowDialog()
        LoadTeacherData(Search_Teacher_TextBox.Text.Trim())
    End Sub

    Private Sub Delete_Teacher_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_Teacher_Button.Click
        If Teacher_List_DataGridView.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a teacher to delete.", "No Selection", _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If _teacherViewMode = "SUBJECTS" Then
            MessageBox.Show("Please press Back to return to the teacher list before deleting.", _
                            "Wrong View", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If Not SetSelectedTeacherId() Then Return

        Dim deleteForm As New popUpFormDeleteTeacher()
        deleteForm.ShowDialog()
        LoadTeacherData(Search_Teacher_TextBox.Text.Trim())
    End Sub

    Private Sub Assign_Class_To_Teacher_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Assign_Class_To_Teacher_Button.Click
        Dim assignForm As New popUpFormAssignSubjectToTeacher()
        assignForm.ShowDialog()
    End Sub

    Private Sub Back_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Back_Button.Click
        LoadTeacherData(Search_Teacher_TextBox.Text.Trim())
    End Sub

    ' ════════════════════════════════════════════════════════════════════
    ' ── Refresh Teacher Button: reloads Teacher_List_DataGridView ──
    ' ── If currently in SUBJECTS drill-down, reloads that teacher's  ──
    ' ── subjects; otherwise reloads the full teacher list.           ──
    ' ════════════════════════════════════════════════════════════════════
    Private Sub Refresh_teacher_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Refresh_teacher_Button.Click
        Try
            If _teacherViewMode = "SUBJECTS" Then
                ' Re-load the current teacher's subjects if a teacher was previously selected
                If SelectedTeacherProfId > 0 Then
                    LoadTeacherSubjects(SelectedTeacherProfId, "")
                Else
                    ' Fallback: no teacher selected, return to list
                    LoadTeacherData(Search_Teacher_TextBox.Text.Trim())
                End If
            Else
                LoadTeacherData(Search_Teacher_TextBox.Text.Trim())
            End If
        Catch ex As Exception
            MessageBox.Show("Error refreshing teacher list: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Teacher Panel - Search"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub Search_Teacher_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search_Teacher_TextBox.TextChanged
        If _teacherViewMode = "LIST" Then
            LoadTeacherData(Search_Teacher_TextBox.Text.Trim())
        End If
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Teacher Panel - DataGridView Click (Drill-Down to Subjects)"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, _
                                        ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
        Handles Teacher_List_DataGridView.CellClick

        If e.RowIndex < 0 Then Return
        If _teacherViewMode = "SUBJECTS" Then Return

        Try
            Dim selectedRow As DataGridViewRow = Teacher_List_DataGridView.Rows(e.RowIndex)

            Dim rawProfId As Object = selectedRow.Cells("ProfID").Value
            Dim rawAccId As Object = selectedRow.Cells("AccountID").Value
            Dim rawName As Object = selectedRow.Cells("FullName").Value

            If rawProfId Is Nothing OrElse IsDBNull(rawProfId) Then Return
            If rawAccId Is Nothing OrElse IsDBNull(rawAccId) Then Return

            SelectedTeacherProfId = Convert.ToInt32(rawProfId)
            SelectedTeacherAccId = Convert.ToInt32(rawAccId)

            Dim fullName As String = "Unknown Teacher"
            If rawName IsNot Nothing AndAlso Not IsDBNull(rawName) Then
                fullName = rawName.ToString()
            End If

            LoadTeacherSubjects(SelectedTeacherProfId, fullName)

        Catch ex As Exception
            MessageBox.Show("Error reading selected teacher: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Teacher Panel - Set Selected Teacher ID Helper"
    ' ════════════════════════════════════════════════════════════════════

    Private Function SetSelectedTeacherId() As Boolean
        Try
            Dim selectedRow As DataGridViewRow = Teacher_List_DataGridView.SelectedRows(0)
            Dim rawAccId As Object = selectedRow.Cells("AccountID").Value
            Dim rawProfId As Object = selectedRow.Cells("ProfID").Value

            If rawAccId Is Nothing OrElse IsDBNull(rawAccId) Then
                MessageBox.Show("Could not retrieve account ID. Please re-select the teacher.", _
                                "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If rawProfId Is Nothing OrElse IsDBNull(rawProfId) Then
                MessageBox.Show("Could not retrieve professor ID. Please re-select the teacher.", _
                                "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            SelectedTeacherAccId = Convert.ToInt32(rawAccId)
            SelectedTeacherProfId = Convert.ToInt32(rawProfId)
            Return True

        Catch ex As Exception
            MessageBox.Show("Error reading selected teacher: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

#End Region

End Class