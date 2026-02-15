Imports System.Data.Odbc

Public Class Admin_Form

#Region "Shared State - Selected Student ID for Modify/Delete popups"
    ''' <summary>
    ''' Stores the currently selected student's acc_id and stud_id.
    ''' popUpFormModifyStudent and popUpFormDeleteStudent should read these values.
    ''' </summary>
    Public Shared SelectedStudentAccId As Integer = 0
    Public Shared SelectedStudentStudId As Integer = 0
#End Region

#Region "Form Load"
    Private Sub Admin_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set Dashboard as default view on form load
        ShowDashboard()

        ' Initialize Student DataGridView
        InitializeStudentDataGridView()

        ' Load all students on form load (empty search = all records)
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

    ''' <summary>
    ''' Reads both acc_id and stud_id from the hidden columns of the selected DataGridView row
    ''' and stores them in shared state for popup forms to consume.
    ''' Returns False if the IDs cannot be read.
    ''' </summary>
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
    ''' <summary>
    ''' Loads student records into the DataGridView.
    '''
    ''' Flow: student LEFT JOIN section (via section_id FK) → resolves section name.
    '''
    ''' FIX: Removed the AND s.email LIKE '%@google.com' filter — it was silently
    ''' hiding every record because no student in the database has a @google.com email.
    ''' The only role filter kept is WHERE LOWER(s.role) = 'student', which correctly
    ''' excludes teachers, admins, and NULL-role seed rows.
    '''
    ''' IFNULL wrappers on every nullable column prevent NULL from breaking CONCAT
    ''' in the search filter or showing blank cells in the grid.
    ''' </summary>
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

            ' ── Hide internal ID columns (needed by Modify/Delete, not for display) ──
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
            MessageBox.Show("Error loading student data: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub
#End Region

#Region "Student Panel - DataGridView Events"
    Private Sub Student_List_DataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Student_List_DataGridView.CellContentClick
        ' Reserved for future use (e.g. double-click to view student details)
    End Sub
#End Region

End Class