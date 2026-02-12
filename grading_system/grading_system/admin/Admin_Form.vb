Imports System.Data.Odbc

Public Class Admin_Form

#Region "Shared State - Selected Student ID for Modify/Delete popups"
    ''' <summary>
    ''' Stores the currently selected student's stud_id.
    ''' popUpFormModifyStudent and popUpFormDeleteStudent should read this value.
    ''' </summary>
    Public Shared SelectedStudentId As Integer = 0
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

        ' Pass selected student ID to shared state so popup can read it
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

        ' Pass selected student ID to shared state so popup can read it
        If Not SetSelectedStudentId() Then
            Return
        End If

        Dim deleteForm As New popUpFormDeleteStudent()
        deleteForm.ShowDialog()
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub

    ''' <summary>
    ''' Reads stud_id from the hidden StudentID column of the selected DataGridView row
    ''' and stores it in Admin_Form.SelectedStudentId for popup forms to consume.
    ''' Returns False if the ID cannot be read.
    ''' </summary>
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
    ''' <summary>
    ''' Loads student records into the DataGridView.
    ''' Columns from student table: stud_id, firstname, middlename, lastname, section, gender, course, yr_lvl
    ''' FIX 1: LIKE uses wildcard on both sides so partial name search works.
    ''' FIX 2: IFNULL on middlename in WHERE clause prevents NULL rows from being excluded.
    ''' FIX 3: Column alias uses no spaces so ODBC DataTable column lookup is reliable.
    ''' FIX 4: StudentID column is hidden but still accessible for Modify/Delete operations.
    ''' </summary>
    Public Sub LoadStudentData(ByVal searchText As String)
        Try
            Connect_me()

            ' Build full name safely — IFNULL handles NULL middlename in both SELECT and WHERE
            Dim query As String = _
                "SELECT " & _
                "  s.stud_id AS StudentID, " & _
                "  CONCAT(s.lastname, ', ', s.firstname, " & _
                "         IF(s.middlename IS NULL OR s.middlename = '', '', CONCAT(' ', s.middlename))) AS FullName, " & _
                "  s.gender   AS Gender, " & _
                "  s.course   AS Course, " & _
                "  s.yr_lvl   AS YearLevel, " & _
                "  s.section  AS Section " & _
                "FROM student s " & _
                "WHERE s.role = 'student' " & _
                "  AND CONCAT(s.firstname, ' ', IFNULL(s.middlename, ''), ' ', s.lastname) LIKE ? " & _
                "ORDER BY s.lastname, s.firstname"

            Dim cmd As New OdbcCommand(query, con)
            ' FIX: Wildcard on BOTH sides so typing any part of the name finds the student
            cmd.Parameters.AddWithValue("@search", "%" & searchText & "%")

            Dim adapter As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ' Bind DataTable to grid
            Student_List_DataGridView.DataSource = dt

            ' Hide the StudentID column — it's used by Modify/Delete but not shown
            If Student_List_DataGridView.Columns.Contains("StudentID") Then
                Student_List_DataGridView.Columns("StudentID").Visible = False
            End If

            ' Rename visible column headers to friendly labels
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