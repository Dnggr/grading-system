Imports System.Data.Odbc

Public Class popUpFormDeleteStudent

#Region "Module-Level Variables"
    ' Stores the acc_id of the student selected in the DataGridView.
    ' -1 means no student is currently selected.
    Private _selectedAccId As Integer = -1

    ' Stores the full name of the selected student for use in confirmation messages.
    Private _selectedFullName As String = String.Empty
#End Region

#Region "Form Load"
    Private Sub popUpFormDeleteStudent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetupDataGridView()
        LoadAllStudents()
    End Sub

    ''' <summary>
    ''' Configures the DataGridView to be read-only with two visible columns
    ''' (Full Name, Section) and two hidden ID columns for internal use.
    ''' </summary>
    Private Sub SetupDataGridView()
        With Delete_Student_DataGridView
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoGenerateColumns = False
            .Columns.Clear()

            ' Hidden column: acc_id — used to target the correct account row on delete
            Dim colAccId As New DataGridViewTextBoxColumn()
            colAccId.Name = "colAccId"
            colAccId.HeaderText = "acc_id"
            colAccId.DataPropertyName = "acc_id"
            colAccId.Visible = False
            .Columns.Add(colAccId)

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
    ''' Called on form load to show all students with no filter.
    ''' </summary>
    Private Sub LoadAllStudents()
        LoadStudents(String.Empty)
    End Sub

    ''' <summary>
    ''' Fetches students from the `account` table filtered by name.
    ''' When <paramref name="searchName"/> is empty, all students are returned.
    ''' Because of the trigger architecture, `account` is the single source of truth —
    ''' but we only show rows where role = 'student' (case-insensitive).
    ''' </summary>
    Private Sub LoadStudents(ByVal searchName As String)
        Try
            Connect_me()

            Dim query As String
            Dim cmd As OdbcCommand

            If String.IsNullOrEmpty(searchName.Trim()) Then
                ' No filter — return all students
                query = "SELECT acc_id, " & _
                        "CONCAT(TRIM(IFNULL(firstname,'')), ' ', TRIM(IFNULL(middlename,'')), ' ', TRIM(IFNULL(lastname,''))) AS fullname, " & _
                        "IFNULL(section, '') AS section " & _
                        "FROM account " & _
                        "WHERE LOWER(role) = 'student' " & _
                        "ORDER BY lastname, firstname"
                cmd = New OdbcCommand(query, con)
            Else
                ' Filter rows whose firstname, lastname, OR middlename contains the search term
                query = "SELECT acc_id, " & _
                        "CONCAT(TRIM(IFNULL(firstname,'')), ' ', TRIM(IFNULL(middlename,'')), ' ', TRIM(IFNULL(lastname,''))) AS fullname, " & _
                        "IFNULL(section, '') AS section " & _
                        "FROM account " & _
                        "WHERE LOWER(role) = 'student' " & _
                        "AND (firstname LIKE ? OR lastname LIKE ? OR middlename LIKE ?) " & _
                        "ORDER BY lastname, firstname"
                cmd = New OdbcCommand(query, con)
                Dim likeParam As String = "%" & searchName.Trim() & "%"
                cmd.Parameters.AddWithValue("@fn", likeParam)
                cmd.Parameters.AddWithValue("@ln", likeParam)
                cmd.Parameters.AddWithValue("@mn", likeParam)
            End If

            Dim adapter As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Delete_Student_DataGridView.DataSource = dt

            ' Clear selection tracking whenever the list is refreshed
            _selectedAccId = -1
            _selectedFullName = String.Empty

        Catch ex As Exception
            MessageBox.Show("Error loading students: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    ''' <summary>
    ''' Live search: fires on every keystroke and refreshes the DataGridView instantly.
    ''' </summary>
    Private Sub Delete_Student_Search_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_Student_Search_TextBox.TextChanged
        LoadStudents(Delete_Student_Search_TextBox.Text)
    End Sub
#End Region

#Region "DataGridView — Row Click"
    ''' <summary>
    ''' Fires when any cell is clicked. Shows a YES/NO warning before flagging the
    ''' student for deletion. Actual deletion only happens when Delete_Button is clicked.
    ''' </summary>
    Private Sub Delete_Student_DataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Delete_Student_DataGridView.CellContentClick
        ' Ignore header row clicks
        If e.RowIndex < 0 Then Return

        Dim row As DataGridViewRow = Delete_Student_DataGridView.Rows(e.RowIndex)

        Dim accIdVal As Object = row.Cells("colAccId").Value
        Dim fullNameVal As Object = row.Cells("colFullName").Value

        If accIdVal Is Nothing OrElse IsDBNull(accIdVal) Then
            MessageBox.Show("Cannot identify the selected student.", _
                            "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim fullName As String = If(fullNameVal IsNot Nothing AndAlso Not IsDBNull(fullNameVal), _
                                    fullNameVal.ToString().Trim(), "Unknown Student")

        ' Warning confirmation pop-up
        Dim confirm As DialogResult = MessageBox.Show( _
            "WARNING: You are about to delete the following student:" & vbCrLf & vbCrLf & _
            fullName & vbCrLf & vbCrLf & _
            "This action will permanently remove the student's account and all associated records." & vbCrLf & _
            "Are you sure you want to continue?", _
            "Confirm Deletion", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Warning)

        If confirm = DialogResult.Yes Then
            ' Store selection — deletion fires when the Delete Button is clicked
            _selectedAccId = Convert.ToInt32(accIdVal)
            _selectedFullName = fullName
            MessageBox.Show("Student '" & fullName & "' is selected for deletion." & vbCrLf & _
                            "Click the Delete button to confirm.", _
                            "Ready to Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            ' User chose No — clear any prior selection
            _selectedAccId = -1
            _selectedFullName = String.Empty
            Delete_Student_DataGridView.ClearSelection()
        End If
    End Sub
#End Region

#Region "Delete Button"
    ''' <summary>
    ''' Deletes the selected student from both `account` and `student` tables.
    ''' Because `student` has a CASCADE DELETE foreign key on `account.acc_id`,
    ''' deleting the `account` row will automatically remove the matching `student` row.
    ''' The `grades` table also cascades on `student.stud_id`, so grades are cleaned up too.
    ''' </summary>
    Private Sub Delete_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_Button.Click
        If _selectedAccId = -1 Then
            MessageBox.Show("Please select a student from the list first.", _
                            "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Final confirmation before irreversible action
        Dim finalConfirm As DialogResult = MessageBox.Show( _
            "FINAL WARNING" & vbCrLf & vbCrLf & _
            "Deleting: " & _selectedFullName & vbCrLf & vbCrLf & _
            "This cannot be undone. Proceed?", _
            "Final Confirmation", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Exclamation)

        If finalConfirm <> DialogResult.Yes Then Return

        DeleteStudent(_selectedAccId, _selectedFullName)
    End Sub

    ''' <summary>
    ''' Executes the DELETE statement against the `account` table.
    ''' The FK constraint (ON DELETE CASCADE) on `student.acc_id` automatically
    ''' removes the matching student row — no second DELETE needed.
    ''' </summary>
    Private Sub DeleteStudent(ByVal accId As Integer, ByVal fullName As String)
        Try
            Connect_me()

            Dim query As String = "DELETE FROM account WHERE acc_id = ? AND LOWER(role) = 'student'"
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@acc_id", accId)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show("Student '" & fullName & "' has been successfully deleted.", _
                                "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Reset tracking variables
                _selectedAccId = -1
                _selectedFullName = String.Empty

                ' Refresh the DataGridView to reflect the deletion
                LoadStudents(Delete_Student_Search_TextBox.Text)
            Else
                MessageBox.Show("No record was deleted. The student may have already been removed.", _
                                "Delete Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting student: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
#End Region

#Region "Cancel Button"
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.Close()
    End Sub
#End Region

End Class