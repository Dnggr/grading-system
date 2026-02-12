Imports System.Data.Odbc

Public Class Admin_Form

#Region "Form Load"
    Private Sub Admin_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set Dashboard as default view on form load
        ShowDashboard()

        ' Initialize Student DataGridView
        InitializeStudentDataGridView()

        ' Load all students on form load
        LoadStudentData("")
    End Sub
#End Region

#Region "Navigation Panel - Button Click Events"
    Private Sub Dashboard_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Dashboard_Button.Click
        ShowDashboard()
    End Sub

    Private Sub Student_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Student_Button.Click
        ShowStudentPanel()
        ' Refresh student data when panel is shown
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub

    Private Sub Teacher_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Teacher_Button.Click
        ShowTeacherPanel()
    End Sub

    Private Sub School_Year_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles School_Year_Button.Click
        ShowSchoolYearPanel()
    End Sub

    Private Sub Logout_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Logout_Button.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?" & vbCrLf & "You will be redirected to the login screen.", _
                                                      "Logout Confirmation", _
                                                      MessageBoxButtons.YesNo, _
                                                      MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            ' Close current form
            Me.Close()

            ' Show login form
            Dim loginForm As New Login_Form()
            loginForm.Show()
        End If
    End Sub

    Private Sub Exit_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Exit_Button.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit the application?" & vbCrLf & "All unsaved changes will be lost.", _
                                                      "Exit Confirmation", _
                                                      MessageBoxButtons.YesNo, _
                                                      MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            ' Exit the entire application
            Application.Exit()
        End If
    End Sub
#End Region

#Region "Panel Navigation Helper Methods"
    ''' <summary>
    ''' Hides all panels except Navigation Panel
    ''' </summary>
    Private Sub HideAllPanels()
        Dashboard_Panel.Visible = False
        Student_Panel.Visible = False
        Teacher_Panel.Visible = False
        School_Year_Panel.Visible = False
    End Sub

    ''' <summary>
    ''' Shows Dashboard Panel and hides all others
    ''' </summary>
    Private Sub ShowDashboard()
        HideAllPanels()
        Dashboard_Panel.Visible = True
        Dashboard_Panel.BringToFront()
    End Sub

    ''' <summary>
    ''' Shows Student Panel and hides all others
    ''' </summary>
    Private Sub ShowStudentPanel()
        HideAllPanels()
        Student_Panel.Visible = True
        Student_Panel.BringToFront()
    End Sub

    ''' <summary>
    ''' Shows Teacher Panel and hides all others
    ''' </summary>
    Private Sub ShowTeacherPanel()
        HideAllPanels()
        Teacher_Panel.Visible = True
        Teacher_Panel.BringToFront()
    End Sub

    ''' <summary>
    ''' Shows School Year Panel and hides all others
    ''' </summary>
    Private Sub ShowSchoolYearPanel()
        HideAllPanels()
        School_Year_Panel.Visible = True
        School_Year_Panel.BringToFront()
    End Sub
#End Region

#Region "Student Panel - Button Click Events"
    Private Sub Add_Student_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Add_Student_Button.Click
        ' Open Add Student Popup Form
        Dim addForm As New popUpFormAddStudent()
        addForm.ShowDialog()

        ' Refresh the student list after closing the form
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub

    Private Sub Modify_Student_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Student_Button.Click
        ' Check if a student is selected
        If Student_List_DataGridView.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a student to modify.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Open Modify Student Popup Form
        Dim modifyForm As New popUpFormModifyStudent()
        modifyForm.ShowDialog()

        ' Refresh the student list after closing the form
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub

    Private Sub Delete_Student_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_Student_Button.Click
        ' Check if a student is selected
        If Student_List_DataGridView.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a student to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Open Delete Student Popup Form
        Dim deleteForm As New popUpFormDeleteStudent()
        deleteForm.ShowDialog()

        ' Refresh the student list after closing the form
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub
#End Region

#Region "Student Panel - Search Functionality"
    Private Sub Search_Student_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search_Student_TextBox.TextChanged
        ' Load student data based on search text
        LoadStudentData(Search_Student_TextBox.Text.Trim())
    End Sub
#End Region

#Region "Student Panel - DataGridView Configuration"
    ''' <summary>
    ''' Initialize Student DataGridView settings
    ''' </summary>
    Private Sub InitializeStudentDataGridView()
        Try
            ' Make DataGridView read-only (not typable)
            Student_List_DataGridView.ReadOnly = True
            Student_List_DataGridView.AllowUserToAddRows = False
            Student_List_DataGridView.AllowUserToDeleteRows = False
            Student_List_DataGridView.EditMode = DataGridViewEditMode.EditProgrammatically

            ' Set selection mode to full row
            Student_List_DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Student_List_DataGridView.MultiSelect = False

            ' Auto resize columns
            Student_List_DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            ' Alternating row color for better readability
            Student_List_DataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray

        Catch ex As Exception
            MessageBox.Show("Error initializing DataGridView: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Student Panel - Data Loading"
    ''' <summary>
    ''' Load student data into DataGridView with optional search filter
    ''' </summary>
    ''' <param name="searchText">Search text to filter student names</param>
    Public Sub LoadStudentData(ByVal searchText As String)
        Try
            ' Connect to database
            Connect_me()

            ' Create SQL query with CONCAT for full name
            Dim query As String = "SELECT " & _
                                  "CONCAT(s.firstname, ' ', IFNULL(s.middlename, ''), ' ', s.lastname) AS Name, " & _
                                  "s.stud_id AS StudentID, " & _
                                  "s.gender AS Gender, " & _
                                  "s.section AS 'Grade Level' " & _
                                  "FROM student s " & _
                                  "WHERE CONCAT(s.firstname, ' ', s.middlename, ' ', s.lastname) LIKE ? " & _
                                  "ORDER BY s.lastname, s.firstname"

            ' Create command with parameter
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@search", searchText & "%")

            ' Fill DataTable
            Dim adapter As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ' Bind to DataGridView
            Student_List_DataGridView.DataSource = dt

            ' Hide StudentID column (we'll use it for operations but don't show it)
            If Student_List_DataGridView.Columns.Contains("StudentID") Then
                Student_List_DataGridView.Columns("StudentID").Visible = False
            End If

            ' Set column headers
            If Student_List_DataGridView.Columns.Contains("Name") Then
                Student_List_DataGridView.Columns("Name").HeaderText = "Full Name"
            End If

            If Student_List_DataGridView.Columns.Contains("Gender") Then
                Student_List_DataGridView.Columns("Gender").HeaderText = "Gender"
            End If

            If Student_List_DataGridView.Columns.Contains("Grade Level") Then
                Student_List_DataGridView.Columns("Grade Level").HeaderText = "Grade Level"
            End If

            ' Close connection
            If con.State = ConnectionState.Open Then
                con.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading student data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub
#End Region

#Region "Student Panel - DataGridView Events"
    Private Sub Student_List_DataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Student_List_DataGridView.CellContentClick
        ' This event can be used for future functionality if needed
        ' For example, double-click to view student details
    End Sub
#End Region

End Class