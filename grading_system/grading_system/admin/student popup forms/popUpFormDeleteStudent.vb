Imports System.Data.Odbc

Public Class popUpFormDeleteStudent

#Region "Module-Level Variables"
    Private _selectedAccId As Integer = -1
    Private _selectedFullName As String = String.Empty
#End Region

#Region "Form Load"
    Private Sub popUpFormDeleteStudent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetupDataGridView()
        LoadAllStudents()
    End Sub

    ''' <summary>
    ''' UPDATED: DataGridView now includes section_id column (hidden)
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
    ''' Query changed from account table to student table (source of truth after trigger)
    ''' </summary>
    Private Sub LoadStudents(ByVal searchName As String)
        Try
            Connect_me()

            Dim query As String
            Dim cmd As OdbcCommand

            If String.IsNullOrEmpty(searchName.Trim()) Then
                query = "SELECT s.stud_id, s.acc_id, s.section_id, " & _
                        "CONCAT(TRIM(IFNULL(s.firstname,'')), ' ', TRIM(IFNULL(s.middlename,'')), ' ', TRIM(IFNULL(s.lastname,''))) AS fullname, " & _
                        "IFNULL(sec.section, 'No Section') AS section " & _
                        "FROM student s " & _
                        "LEFT JOIN section sec ON s.section_id = sec.section_id " & _
                        "WHERE LOWER(s.role) = 'student' " & _
                        "ORDER BY s.lastname, s.firstname"
                cmd = New OdbcCommand(query, con)
            Else
                query = "SELECT s.stud_id, s.acc_id, s.section_id, " & _
                        "CONCAT(TRIM(IFNULL(s.firstname,'')), ' ', TRIM(IFNULL(s.middlename,'')), ' ', TRIM(IFNULL(s.lastname,''))) AS fullname, " & _
                        "IFNULL(sec.section, 'No Section') AS section " & _
                        "FROM student s " & _
                        "LEFT JOIN section sec ON s.section_id = sec.section_id " & _
                        "WHERE LOWER(s.role) = 'student' " & _
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

            Delete_Student_DataGridView.DataSource = dt

            _selectedAccId = -1
            _selectedFullName = String.Empty

        Catch ex As Exception
            MessageBox.Show("Error loading students: " & ex.Message, "Error", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub Delete_Student_Search_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_Student_Search_TextBox.TextChanged
        LoadStudents(Delete_Student_Search_TextBox.Text)
    End Sub
#End Region

#Region "DataGridView — Row Click"
    Private Sub Delete_Student_DataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Delete_Student_DataGridView.CellContentClick
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

        Dim confirm As DialogResult = MessageBox.Show( _
            "WARNING: You are about to delete the following student:" & vbCrLf & vbCrLf & _
            fullName & vbCrLf & vbCrLf & _
            "This action will permanently remove the student's account and all associated records." & vbCrLf & _
            "Are you sure you want to continue?", _
            "Confirm Deletion", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Warning)

        If confirm = DialogResult.Yes Then
            _selectedAccId = Convert.ToInt32(accIdVal)
            _selectedFullName = fullName
            MessageBox.Show("Student '" & fullName & "' is selected for deletion." & vbCrLf & _
                            "Click the Delete button to confirm.", _
                            "Ready to Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            _selectedAccId = -1
            _selectedFullName = String.Empty
            Delete_Student_DataGridView.ClearSelection()
        End If
    End Sub
#End Region

#Region "Delete Button"
    ''' <summary>
    ''' Deletes from account table — CASCADE DELETE automatically removes student record
    ''' </summary>
    Private Sub Delete_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_Button.Click
        If _selectedAccId = -1 Then
            MessageBox.Show("Please select a student from the list first.", _
                            "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

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

                _selectedAccId = -1
                _selectedFullName = String.Empty

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