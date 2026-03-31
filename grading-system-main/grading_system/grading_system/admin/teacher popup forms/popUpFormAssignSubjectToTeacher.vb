Imports System.Data.Odbc

Public Class popUpFormAssignSubjectToTeacher

    Private selectedTeacherId As Integer = 0
    Private selectedTeacherName As String = ""
    Private selectedSubjectId As Integer = 0
    Private selectedSubjectName As String = ""
    Private selectedCourseId As Integer = 0

    Private allTeachersTable As DataTable
    Private allSubjectsTable As DataTable

#Region "Form Load"

    Private Sub popUpFormAssignSubjectToTeacher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadAllTeachers()
        LoadCourseFilter()
        LoadAllSubjects()
        LoadCurrentAssignments()

        Selected_Teacher_Label.Text = "No teacher selected"
        Selected_Subject_Label.Text = "No subject selected"
    End Sub

#End Region

#Region "Load Data Methods"

    Private Sub LoadAllTeachers()
        Try
            Connect_me()
            ' Only load ACTIVE teachers
            Dim query As String = _
                "SELECT prof_id, CONCAT(lastname, ', ', firstname, ' ', COALESCE(middlename, '')) AS fullname " & _
                "FROM prof " & _
                "WHERE status = 'active' " & _
                "ORDER BY lastname"

            Dim cmd As New OdbcCommand(query, con)
            Dim da As New OdbcDataAdapter(cmd)
            allTeachersTable = New DataTable()
            da.Fill(allTeachersTable)

            Search_Teacher_DataGridView.DataSource = allTeachersTable

            If Search_Teacher_DataGridView.Columns.Contains("prof_id") Then
                Search_Teacher_DataGridView.Columns("prof_id").Visible = False
            End If

            If Search_Teacher_DataGridView.Columns.Contains("fullname") Then
                Search_Teacher_DataGridView.Columns("fullname").HeaderText = "Teacher Name"
                Search_Teacher_DataGridView.Columns("fullname").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading teachers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub LoadCourseFilter()
        Try
            Connect_me()
            Dim query As String = "SELECT course_id, course_code, course_name FROM course ORDER BY course_code"
            Dim cmd As New OdbcCommand(query, con)
            Dim dt As New DataTable()
            Dim da As New OdbcDataAdapter(cmd)
            da.Fill(dt)

            Dim allRow As DataRow = dt.NewRow()
            allRow("course_id") = 0
            allRow("course_code") = "ALL"
            allRow("course_name") = "All Courses"
            dt.Rows.InsertAt(allRow, 0)

            CourseFilter_ComboBox.DataSource = dt
            CourseFilter_ComboBox.DisplayMember = "course_code"
            CourseFilter_ComboBox.ValueMember = "course_id"
            CourseFilter_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
            CourseFilter_ComboBox.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show("Error loading courses: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub LoadAllSubjects()
        Try
            Connect_me()
            Dim query As String = _
                "SELECT s.sub_id, s.sub_code, s.sub_name, " & _
                "COALESCE(c.course_code, 'N/A') AS course_code, " & _
                "COALESCE(s.course_id, 0) AS course_id " & _
                "FROM subject s " & _
                "LEFT JOIN course c ON s.course_id = c.course_id " & _
                "ORDER BY c.course_code, s.sub_code"

            Dim cmd As New OdbcCommand(query, con)
            Dim da As New OdbcDataAdapter(cmd)
            allSubjectsTable = New DataTable()
            da.Fill(allSubjectsTable)

            SubjectsDataGridView.DataSource = allSubjectsTable

            If SubjectsDataGridView.Columns.Contains("sub_id") Then
                SubjectsDataGridView.Columns("sub_id").Visible = False
            End If
            If SubjectsDataGridView.Columns.Contains("course_id") Then
                SubjectsDataGridView.Columns("course_id").Visible = False
            End If
            If SubjectsDataGridView.Columns.Contains("course_code") Then
                SubjectsDataGridView.Columns("course_code").HeaderText = "Course"
                SubjectsDataGridView.Columns("course_code").Width = 80
            End If
            If SubjectsDataGridView.Columns.Contains("sub_code") Then
                SubjectsDataGridView.Columns("sub_code").HeaderText = "Code"
                SubjectsDataGridView.Columns("sub_code").Width = 100
            End If
            If SubjectsDataGridView.Columns.Contains("sub_name") Then
                SubjectsDataGridView.Columns("sub_name").HeaderText = "Subject Name"
                SubjectsDataGridView.Columns("sub_name").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading subjects: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub LoadSectionsForSubject(ByVal courseId As Integer, ByVal subjectId As Integer, ByVal teacherId As Integer)
        Try
            Connect_me()
            Dim query As String = _
                "SELECT s.section_id, " & _
                "CONCAT(s.section, ' (Year ', s.year_lvl, ')') AS section_display, " & _
                "CASE WHEN pss.id IS NOT NULL THEN 1 ELSE 0 END AS is_assigned " & _
                "FROM section s " & _
                "LEFT JOIN profsectionsubject pss ON s.section_id = pss.section_id " & _
                "  AND pss.prof_id = ? AND pss.sub_id = ? " & _
                "WHERE s.course_id = ? " & _
                "ORDER BY s.section"

            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@prof_id", teacherId)
            cmd.Parameters.AddWithValue("@sub_id", subjectId)
            cmd.Parameters.AddWithValue("@course_id", courseId)

            Dim dt As New DataTable()
            Dim da As New OdbcDataAdapter(cmd)
            da.Fill(dt)

            ClassDataGridView.DataSource = Nothing
            ClassDataGridView.Columns.Clear()

            Dim chkCol As New DataGridViewCheckBoxColumn()
            chkCol.Name = "chkAssign"
            chkCol.HeaderText = "Assign"
            chkCol.Width = 60
            ClassDataGridView.Columns.Add(chkCol)

            ClassDataGridView.DataSource = dt

            If ClassDataGridView.Columns.Contains("section_id") Then
                ClassDataGridView.Columns("section_id").Visible = False
            End If
            If ClassDataGridView.Columns.Contains("is_assigned") Then
                ClassDataGridView.Columns("is_assigned").Visible = False
            End If
            If ClassDataGridView.Columns.Contains("section_display") Then
                ClassDataGridView.Columns("section_display").HeaderText = "Section"
                ClassDataGridView.Columns("section_display").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If

            For Each row As DataGridViewRow In ClassDataGridView.Rows
                If Not row.IsNewRow Then
                    Dim isAssigned As Integer = Convert.ToInt32(row.Cells("is_assigned").Value)
                    row.Cells("chkAssign").Value = (isAssigned = 1)
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Error loading sections: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub LoadCurrentAssignments()
        Try
            Connect_me()
            ' Only show assignments for ACTIVE teachers
            Dim query As String = _
                "SELECT pss.id, " & _
                "CONCAT(p.lastname, ', ', p.firstname) AS Teacher, " & _
                "COALESCE(c.course_code, 'N/A') AS Course, " & _
                "CONCAT(sub.sub_code, ' - ', sub.sub_name) AS Subject, " & _
                "sec.section AS Section " & _
                "FROM profsectionsubject pss " & _
                "INNER JOIN prof p ON pss.prof_id = p.prof_id " & _
                "INNER JOIN subject sub ON pss.sub_id = sub.sub_id " & _
                "LEFT JOIN course c ON sub.course_id = c.course_id " & _
                "INNER JOIN section sec ON pss.section_id = sec.section_id " & _
                "WHERE p.status = 'active' " & _
                "ORDER BY p.lastname, c.course_code, sub.sub_code, sec.section"

            Dim cmd As New OdbcCommand(query, con)
            Dim dt As New DataTable()
            Dim da As New OdbcDataAdapter(cmd)
            da.Fill(dt)

            AssignmentsDataGridView.DataSource = Nothing
            AssignmentsDataGridView.Columns.Clear()

            AssignmentsDataGridView.DataSource = dt

            Dim btnDelete As New DataGridViewButtonColumn()
            btnDelete.Name = "btnDelete"
            btnDelete.HeaderText = "Action"
            btnDelete.Text = "Delete"
            btnDelete.UseColumnTextForButtonValue = True
            btnDelete.Width = 80
            AssignmentsDataGridView.Columns.Add(btnDelete)

            If AssignmentsDataGridView.Columns.Contains("id") Then
                AssignmentsDataGridView.Columns("id").Visible = False
            End If
            If AssignmentsDataGridView.Columns.Contains("Teacher") Then
                AssignmentsDataGridView.Columns("Teacher").Width = 150
            End If
            If AssignmentsDataGridView.Columns.Contains("Course") Then
                AssignmentsDataGridView.Columns("Course").Width = 80
            End If
            If AssignmentsDataGridView.Columns.Contains("Subject") Then
                AssignmentsDataGridView.Columns("Subject").Width = 200
            End If
            If AssignmentsDataGridView.Columns.Contains("Section") Then
                AssignmentsDataGridView.Columns("Section").Width = 120
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading assignments: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

#End Region

#Region "Teacher Search and Selection"

    Private Sub Search_Teacher_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search_Teacher_TextBox.TextChanged
        If allTeachersTable Is Nothing Then Return

        Dim searchText As String = Search_Teacher_TextBox.Text.Trim().ToLower()

        If searchText = "" Then
            Search_Teacher_DataGridView.DataSource = allTeachersTable
        Else
            Dim filteredView As DataView = allTeachersTable.DefaultView
            filteredView.RowFilter = String.Format( _
                "fullname LIKE '%{0}%'", _
                searchText.Replace("'", "''"))
            Search_Teacher_DataGridView.DataSource = filteredView
        End If
    End Sub

    Private Sub Search_Teacher_DataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Search_Teacher_DataGridView.CellContentClick
        ' Use CellClick instead for better UX
    End Sub

    Private Sub Search_Teacher_DataGridView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Search_Teacher_DataGridView.CellClick
        If e.RowIndex < 0 Then Return

        Try
            Dim profId As Integer = Convert.ToInt32(Search_Teacher_DataGridView.Rows(e.RowIndex).Cells("prof_id").Value)
            Dim fullName As String = Search_Teacher_DataGridView.Rows(e.RowIndex).Cells("fullname").Value.ToString()

            Dim result As DialogResult = MessageBox.Show( _
                "Assign subjects to " & fullName & "?", _
                "Confirm Teacher Selection", _
                MessageBoxButtons.YesNo, _
                MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                selectedTeacherId = profId
                selectedTeacherName = fullName
                Selected_Teacher_Label.Text = "Selected: " & fullName

                ClassDataGridView.DataSource = Nothing
                ClassDataGridView.Columns.Clear()
                Selected_Subject_Label.Text = "No subject selected"
                selectedSubjectId = 0
                selectedSubjectName = ""
            End If

        Catch ex As Exception
            MessageBox.Show("Error selecting teacher: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Subject Filter and Selection"

    Private Sub CourseFilter_ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CourseFilter_ComboBox.SelectedIndexChanged
        FilterSubjects()

        ClassDataGridView.DataSource = Nothing
        ClassDataGridView.Columns.Clear()
        Selected_Subject_Label.Text = "No subject selected"
        selectedSubjectId = 0
        selectedSubjectName = ""
    End Sub

    Private Sub SearchSubject_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchSubject_TextBox.TextChanged
        FilterSubjects()
    End Sub

    Private Sub FilterSubjects()
        If allSubjectsTable Is Nothing Then Return

        Dim searchText As String = SearchSubject_TextBox.Text.Trim().ToLower()
        Dim selectedCourseId As Integer = 0

        If CourseFilter_ComboBox.SelectedValue IsNot Nothing Then
            selectedCourseId = Convert.ToInt32(CourseFilter_ComboBox.SelectedValue)
        End If

        Dim filteredView As DataView = allSubjectsTable.DefaultView
        Dim filterParts As New List(Of String)

        If selectedCourseId > 0 Then
            filterParts.Add(String.Format("course_id = {0}", selectedCourseId))
        End If

        If searchText <> "" Then
            filterParts.Add(String.Format( _
                "(sub_code LIKE '%{0}%' OR sub_name LIKE '%{0}%' OR course_code LIKE '%{0}%')", _
                searchText.Replace("'", "''")))
        End If

        If filterParts.Count > 0 Then
            filteredView.RowFilter = String.Join(" AND ", filterParts.ToArray())
        Else
            filteredView.RowFilter = ""
        End If

        SubjectsDataGridView.DataSource = filteredView
    End Sub

    Private Sub SubjectsDataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles SubjectsDataGridView.CellContentClick
        ' Use CellClick instead
    End Sub

    Private Sub SubjectsDataGridView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles SubjectsDataGridView.CellClick
        If e.RowIndex < 0 Then Return

        If selectedTeacherId <= 0 Then
            MessageBox.Show("Please select a teacher first!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            selectedSubjectId = Convert.ToInt32(SubjectsDataGridView.Rows(e.RowIndex).Cells("sub_id").Value)
            Dim subCode As String = SubjectsDataGridView.Rows(e.RowIndex).Cells("sub_code").Value.ToString()
            Dim subName As String = SubjectsDataGridView.Rows(e.RowIndex).Cells("sub_name").Value.ToString()
            Dim courseCode As String = SubjectsDataGridView.Rows(e.RowIndex).Cells("course_code").Value.ToString()
            selectedCourseId = Convert.ToInt32(SubjectsDataGridView.Rows(e.RowIndex).Cells("course_id").Value)

            selectedSubjectName = courseCode & " - " & subCode & " - " & subName
            Selected_Subject_Label.Text = "Sections for: " & selectedSubjectName

            LoadSectionsForSubject(selectedCourseId, selectedSubjectId, selectedTeacherId)

        Catch ex As Exception
            MessageBox.Show("Error selecting subject: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Class/Section Filter"

    Private Sub ClassFilter_ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClassFilter_ComboBox.SelectedIndexChanged
    End Sub

    Private Sub ClassDataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ClassDataGridView.CellContentClick
    End Sub

#End Region

#Region "Assign Button"

    Private Sub Assign_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Assign_Button.Click
        If selectedTeacherId <= 0 Then
            MessageBox.Show("Please select a teacher first!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If selectedSubjectId <= 0 Then
            MessageBox.Show("Please select a subject first!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If ClassDataGridView.Rows.Count = 0 Then
            MessageBox.Show("No sections available to assign!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Connect_me()

            Dim assignedCount As Integer = 0
            Dim removedCount As Integer = 0

            For Each row As DataGridViewRow In ClassDataGridView.Rows
                If row.IsNewRow Then Continue For

                Dim isChecked As Boolean = False
                If row.Cells("chkAssign").Value IsNot Nothing Then
                    isChecked = Convert.ToBoolean(row.Cells("chkAssign").Value)
                End If

                Dim wasAssigned As Boolean = Convert.ToBoolean(row.Cells("is_assigned").Value)
                Dim sectionId As Integer = Convert.ToInt32(row.Cells("section_id").Value)

                If isChecked AndAlso Not wasAssigned Then
                    Dim insertQuery As String = "INSERT INTO profsectionsubject (prof_id, sub_id, section_id) VALUES (?, ?, ?)"
                    Dim cmd As New OdbcCommand(insertQuery, con)
                    cmd.Parameters.AddWithValue("@prof_id", selectedTeacherId)
                    cmd.Parameters.AddWithValue("@sub_id", selectedSubjectId)
                    cmd.Parameters.AddWithValue("@section_id", sectionId)
                    cmd.ExecuteNonQuery()
                    assignedCount += 1
                End If

                If Not isChecked AndAlso wasAssigned Then
                    Dim deleteQuery As String = "DELETE FROM profsectionsubject WHERE prof_id = ? AND sub_id = ? AND section_id = ?"
                    Dim cmd As New OdbcCommand(deleteQuery, con)
                    cmd.Parameters.AddWithValue("@prof_id", selectedTeacherId)
                    cmd.Parameters.AddWithValue("@sub_id", selectedSubjectId)
                    cmd.Parameters.AddWithValue("@section_id", sectionId)
                    cmd.ExecuteNonQuery()
                    removedCount += 1
                End If
            Next

            Dim message As String = "Assignments updated successfully!" & vbCrLf & vbCrLf
            If assignedCount > 0 Then
                message &= "Added: " & assignedCount.ToString() & " section(s)" & vbCrLf
            End If
            If removedCount > 0 Then
                message &= "Removed: " & removedCount.ToString() & " section(s)"
            End If

            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadCurrentAssignments()
            LoadSectionsForSubject(selectedCourseId, selectedSubjectId, selectedTeacherId)

        Catch ex As Exception
            MessageBox.Show("Error assigning sections: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

#End Region

#Region "Delete Assignment"

    Private Sub AssignmentsDataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles AssignmentsDataGridView.CellContentClick
        If e.ColumnIndex = AssignmentsDataGridView.Columns("btnDelete").Index AndAlso e.RowIndex >= 0 Then

            Dim assignmentId As Integer = Convert.ToInt32(AssignmentsDataGridView.Rows(e.RowIndex).Cells("id").Value)
            Dim teacher As String = AssignmentsDataGridView.Rows(e.RowIndex).Cells("Teacher").Value.ToString()
            Dim subject As String = AssignmentsDataGridView.Rows(e.RowIndex).Cells("Subject").Value.ToString()
            Dim section As String = AssignmentsDataGridView.Rows(e.RowIndex).Cells("Section").Value.ToString()

            Dim result As DialogResult = MessageBox.Show( _
                "Delete this assignment?" & vbCrLf & vbCrLf & _
                "Teacher: " & teacher & vbCrLf & _
                "Subject: " & subject & vbCrLf & _
                "Section: " & section, _
                "Confirm Delete", _
                MessageBoxButtons.YesNo, _
                MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                DeleteAssignment(assignmentId)
            End If
        End If
    End Sub

    Private Sub DeleteAssignment(ByVal assignmentId As Integer)
        Try
            Connect_me()
            Dim query As String = "DELETE FROM profsectionsubject WHERE id = ?"
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("@id", assignmentId)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show("Assignment deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadCurrentAssignments()

                If selectedTeacherId > 0 AndAlso selectedSubjectId > 0 Then
                    LoadSectionsForSubject(selectedCourseId, selectedSubjectId, selectedTeacherId)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting assignment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

#End Region

#Region "Close Button"

    Private Sub Close_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Close_Button.Click
        Me.Close()
    End Sub

#End Region

End Class