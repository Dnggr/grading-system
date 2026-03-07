Imports System.Data.Odbc

Public Class Teacher_Form
    Dim response As DialogResult
    Public acc_id As String = Login_Form.id
    Public Shared sy As String
    Public Shared sm As Integer
    Dim allGradesData As New DataSet()
    Private filteredView As DataView

    Private Sub Teacher_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        sy = ""
        sm = 0
        GetCurrentSem(sy, sm)   ' from Module_SemControl
        lblCurrentSem.Text = "Current: S.Y. " & sy & " - " & If(sm = 1, "1st", "2nd") & " Semester"
        ConfigureDataGridView()
        ShowEditButtons()
        ShowDeleteButtons()
        getSchoolYear()
        LoadSemesters()
       
        If ComboBox4.SelectedIndex >= 0 Then
            LoadAllGrades(ComboBox4.SelectedValue.ToString())
        End If

    End Sub

#Region "Database Getters"

    Private Sub getSchoolYear()
        Try
            Connect_me()

            ' Get school years from database
            Dim query As String = "SELECT DISTINCT school_year FROM grades ORDER BY school_year DESC"
            Dim adapter As New OdbcDataAdapter(query, con)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ' Build complete list
            Dim allYears As New List(Of String)()

            ' 1. Add current school year first (if exists)
            If Not String.IsNullOrEmpty(sy) Then
                allYears.Add(sy)
            End If

            ' 2. Add years from database
            For Each row As DataRow In dt.Rows
                Dim year As String = row("school_year").ToString()

                ' Only add if not already in list (avoid duplicate of current year)
                If Not allYears.Contains(year) Then
                    allYears.Add(year)
                End If
            Next

            ' 3. Bind to ComboBox
            If allYears.Count > 0 Then
                ComboBox4.DataSource = allYears

                ' Select current school year (which is first in the list)
                ComboBox4.SelectedIndex = 0
            Else
                MessageBox.Show("No school years available", "Info")
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub LoadAllGrades(ByVal schoolYear As String)
        Try
            Connect_me()

            Dim query As String = "SELECT DISTINCT " & _
                           "grades.grades_id AS gID, " & _
                           "student.stud_id AS ID, " & _
                           "CONCAT(student.lastname, ', ', student.firstname, ' ', student.middlename) AS FullName, " & _
                           "student.course AS Course, " & _
                           "section.section AS Section, " & _
                           "CAST(student.section_id AS INTEGER) AS SectionID, " & _
                           "subject.sub_id AS SubjectID, " & _
                           "CONCAT(subject.sub_code, ' - ', subject.sub_name) AS Subject, " & _
                           "grades.semester AS Semester, " & _
                           "grades.attendance AS Attendance," & _
                           "grades.quiz AS Quiz," & _
                           "grades.recitation AS Recitation," & _
                           "grades.project AS Project," & _
                           "grades.prelim AS Prelim," & _
                           "grades.midterm AS Midterm," & _
                           "grades.semis AS Semis," & _
                           "grades.finals AS Finals," & _
                           "grades.grade AS FinalGrade, " & _
                           "grades.remark AS Remark " & _
                           "FROM grades " & _
                           "LEFT JOIN student ON grades.stud_id = student.stud_id " & _
                           "LEFT JOIN section ON student.section_id = section.section_id " & _
                           "LEFT JOIN subject ON grades.sub_id = subject.sub_id " & _
                           "LEFT JOIN prof ON grades.prof_id = prof.prof_id " & _
                           "WHERE prof.acc_id = ? " & _
                           "AND grades.school_year = ? " & _
                           "ORDER BY student.lastname, student.firstname"

            Using adapter As New OdbcDataAdapter(query, con)
                adapter.SelectCommand.Parameters.AddWithValue("?", acc_id)
                adapter.SelectCommand.Parameters.AddWithValue("?", schoolYear)

                ' Clear existing data
                If allGradesData.Tables.Contains("grades") Then
                    allGradesData.Tables("grades").Clear()
                End If

                adapter.Fill(allGradesData, "grades")

                If allGradesData.Tables("grades").Rows.Count > 0 Then
                    ' Load filter options
                    LoadFilterOptions()

                    ' Apply initial filter (show all)
                    ApplyFilters()
                    
                Else
                    
                    DataGridView2.DataSource = Nothing

                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading grades: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

#End Region

#Region "Load Filter Options"

    Private Sub LoadFilterOptions()
        Try
            ' ✅ Always start fresh
            ComboBox2.DataSource = Nothing
            ComboBox3.DataSource = Nothing

            ' Create lists with placeholders
            Dim sectionsData As New List(Of String)()
            sectionsData.Add("All Sections")

            Dim subjectsData As New List(Of String)()
            subjectsData.Add("All Subjects")

            ' ✅ Only add data if table exists and has rows
            If allGradesData.Tables.Contains("grades") AndAlso _
               allGradesData.Tables("grades").Rows.Count > 0 Then

                ' Load sections
                Dim sections As DataTable = allGradesData.Tables("grades").DefaultView.ToTable(True, "Section")
                For Each row As DataRow In sections.Rows
                    If Not IsDBNull(row("Section")) AndAlso _
                       Not String.IsNullOrEmpty(row("Section").ToString()) Then

                        Dim sectionName As String = row("Section").ToString()
                        If Not sectionsData.Contains(sectionName) Then
                            sectionsData.Add(sectionName)
                        End If
                    End If
                Next

                ' Load subjects
                Dim subjects As DataTable = allGradesData.Tables("grades").DefaultView.ToTable(True, "Subject")
                For Each row As DataRow In subjects.Rows
                    If Not IsDBNull(row("Subject")) AndAlso _
                       Not String.IsNullOrEmpty(row("Subject").ToString()) Then

                        Dim subjectName As String = row("Subject").ToString()
                        If Not subjectsData.Contains(subjectName) Then
                            subjectsData.Add(subjectName)
                        End If
                    End If
                Next
            End If

            ' ✅ Always bind (even if only placeholders)
            ComboBox2.DataSource = sectionsData
            ComboBox2.SelectedIndex = 0

            ComboBox3.DataSource = subjectsData
            ComboBox3.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show("Error loading filter options: " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Apply Filters"

    Private Sub ApplyFilters()
        Try
            ' Check if data exists
            If Not allGradesData.Tables.Contains("grades") OrElse _
               allGradesData.Tables("grades").Rows.Count = 0 Then
                DataGridView2.DataSource = Nothing
                Return
            End If

            filteredView = New DataView(allGradesData.Tables("grades"))
            Dim filterParts As New List(Of String)()

            ' Semester filter
            If ComboBox1.SelectedIndex > 0 Then
                filterParts.Add("Semester = " & ComboBox1.SelectedItem.ToString())
            End If

            ' Section filter
            If ComboBox2.SelectedIndex > 0 Then
                filterParts.Add("Section = '" & ComboBox2.SelectedItem.ToString().Replace("'", "''") & "'")
            End If

            ' Subject filter
            If ComboBox3.SelectedIndex > 0 Then
                filterParts.Add("Subject = '" & ComboBox3.SelectedItem.ToString().Replace("'", "''") & "'")
            End If

            ' Name filter
            If Not String.IsNullOrEmpty(TextBox1.Text.Trim()) Then
                filterParts.Add("FullName LIKE '%" & TextBox1.Text.Trim().Replace("'", "''") & "%'")
            End If

            ' ✅ FIXED: Convert List to Array
            If filterParts.Count > 0 Then
                filteredView.RowFilter = String.Join(" AND ", filterParts.ToArray())
            Else
                filteredView.RowFilter = ""
            End If

            DataGridView2.DataSource = filteredView
            ConfigureDataGridView()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub HideIDColumns()
        If DataGridView2.Columns.Count = 0 Then Return

        ' Hide columns
        DataGridView2.Columns("gID").Visible = False
        DataGridView2.Columns("ID").Visible = False
        DataGridView2.Columns("SectionID").Visible = False
        DataGridView2.Columns("SubjectID").Visible = False

        ' Format grade
        If DataGridView2.Columns.Contains("FinalGrade") Then
            DataGridView2.Columns("FinalGrade").DefaultCellStyle.Format = "0.00"
        End If

        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

#End Region

#Region "ComboBox logic"
    Private Sub LoadSemesters()
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("All Semesters")
        ComboBox1.Items.Add("1")
        ComboBox1.Items.Add("2")
        ComboBox1.SelectedIndex = 0  ' Default to "All"
    End Sub


    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox4.SelectedIndex >= 0 Then
            LoadAllGrades(ComboBox4.SelectedValue.ToString())
        End If
    End Sub

    ' Semester changed - Filter existing data
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ApplyFilters()
    End Sub

    ' Section changed - Filter existing data
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        ApplyFilters()
    End Sub

    ' Subject changed - Filter existing data
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        ApplyFilters()
    End Sub

    ' Name search - Filter existing data
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox1.TextChanged
        ApplyFilters()
    End Sub


#End Region

#Region "Button Logic"

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to Exit?", _
                                                     "log out confirmation", _
                                                     MessageBoxButtons.YesNo, _
                                                     MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If


    End Sub

    ' ✅ FIXED: Use ShowDialog and refresh
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim addGrade As New AddGrade_Form
        addGrade.ShowDialog()  ' ✅ Changed from Show() to ShowDialog()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            ' Reset all filters to placeholders
            If ComboBox1 IsNot Nothing AndAlso ComboBox1.Items.Count > 0 Then
                ComboBox1.SelectedIndex = 0  ' "All Semesters"
            End If

            If ComboBox2 IsNot Nothing AndAlso ComboBox2.Items.Count > 0 Then
                ComboBox2.SelectedIndex = 0  ' "All Sections"
            End If

            If ComboBox3 IsNot Nothing AndAlso ComboBox3.Items.Count > 0 Then
                ComboBox3.SelectedIndex = 0  ' "All Subjects"
            End If

            If TextBox1 IsNot Nothing Then
                TextBox1.Clear()
            End If

            ' Reapply filters (which will show all data)
            ApplyFilters()

        Catch ex As Exception
            MessageBox.Show("Error clearing filters: " & ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        RefreshGradeData()
    End Sub

#End Region

#Region "DataGrid Row Clicked"
    'Private Sub DataGridView2_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
    '   If e.RowIndex < 0 Then Return

    'Dim selectedRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)

    '    Dim gID As String = ""
    '       If selectedRow.Cells("gID").Value IsNot Nothing Then
    '          gID = selectedRow.Cells("gID").Value.ToString()
    '     End If

    '    Label3.Text = gID

    '    Dim addGrade As New AddGrade_Form()

    '       addGrade.gradeID = gID
    '      addGrade.updateMode = True

    '     addGrade.ShowDialog()

    ' End Sub
#End Region

#Region "DataCell delete button clicked"
    Private Sub DataGridView2_CellContentClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.RowIndex < 0 Then Return

        If DataGridView2.Columns.Contains("DeleteButton") AndAlso _
           e.ColumnIndex = DataGridView2.Columns("DeleteButton").Index Then
            DeleteGrade(e.RowIndex)
        End If

        If DataGridView2.Columns.Contains("EditButton") AndAlso _
      e.ColumnIndex = DataGridView2.Columns("EditButton").Index Then
            Dim selectedRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)

            Dim gID As String = ""
            If selectedRow.Cells("gID").Value IsNot Nothing Then
                gID = selectedRow.Cells("gID").Value.ToString()
            End If

            Label3.Text = gID

            Dim addGrade As New AddGrade_Form()

            addGrade.gradeID = gID
            addGrade.updateMode = True

            addGrade.ShowDialog()
        End If
    End Sub
#End Region

#Region "Update Operation"
    Private Sub ShowEditButtons()
        If Not DataGridView2.Columns.Contains("EditButton") Then
            Dim editBtn As New DataGridViewButtonColumn()
            editBtn.Name = "EditButton"
            editBtn.HeaderText = "Edit"
            editBtn.Text = "Edit"
            editBtn.UseColumnTextForButtonValue = True
            editBtn.Width = 60
            editBtn.DefaultCellStyle.BackColor = Color.LightBlue
            DataGridView2.Columns.Add(editBtn)
        Else
            DataGridView2.Columns("EditButton").Visible = True
        End If
    End Sub
#End Region

#Region "Delete Operation"
    Private Sub ShowDeleteButtons()
        If Not DataGridView2.Columns.Contains("DeleteButton") Then
            Dim deleteBtn As New DataGridViewButtonColumn()
            deleteBtn.Name = "DeleteButton"
            deleteBtn.HeaderText = ""
            deleteBtn.Text = "Delete"
            deleteBtn.UseColumnTextForButtonValue = True
            deleteBtn.Width = 80
            deleteBtn.DefaultCellStyle.BackColor = Color.LightCoral
            deleteBtn.DefaultCellStyle.ForeColor = Color.DarkRed
            deleteBtn.DefaultCellStyle.Font = New Font(DataGridView2.Font, FontStyle.Bold)
            DataGridView2.Columns.Add(deleteBtn)
        Else
            DataGridView2.Columns("DeleteButton").Visible = True
        End If
    End Sub

    
    Private Sub DeleteGrade(ByVal rowIndex As Integer)
        ' ... your delete code ...
        Try
            Dim row As DataGridViewRow = DataGridView2.Rows(rowIndex)

            Dim gradeId As String = row.Cells("gID").Value.ToString()
            Dim studentName As String = row.Cells("FullName").Value.ToString()
            Dim subject As String = row.Cells("Subject").Value.ToString()

            Dim result As DialogResult = MessageBox.Show( _
                "Delete this grade?" & vbCrLf & vbCrLf & _
                "Student: " & studentName & vbCrLf & _
                "Subject: " & subject & vbCrLf & vbCrLf & _
                "This cannot be undone!", _
                "Confirm Delete", _
                MessageBoxButtons.YesNo, _
                MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                Connect_me()

                Dim query As String = "DELETE FROM grades WHERE grades_id = ?"

                Using cmd As New OdbcCommand(query, con)
                    cmd.Parameters.AddWithValue("?", gradeId)

                    Dim affected As Integer = cmd.ExecuteNonQuery()

                    If affected > 0 Then
                        MessageBox.Show("Grade deleted!", "Success")
                        RefreshGradeData()
                    End If
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
#End Region

    Private Sub RefreshGradeData()
        If ComboBox4.SelectedIndex >= 0 Then
            LoadAllGrades(ComboBox4.SelectedValue.ToString())
        End If
    End Sub

    Private Sub ConfigureDataGridView()
        If DataGridView2.Columns.Count = 0 Then Return

        ' Hide ID columns
        If DataGridView2.Columns.Contains("gID") Then DataGridView2.Columns("gID").Visible = False
        If DataGridView2.Columns.Contains("ID") Then DataGridView2.Columns("ID").Visible = False
        If DataGridView2.Columns.Contains("SectionID") Then DataGridView2.Columns("SectionID").Visible = False
        If DataGridView2.Columns.Contains("SubjectID") Then DataGridView2.Columns("SubjectID").Visible = False

        ' Format grade
        If DataGridView2.Columns.Contains("FinalGrade") Then
            DataGridView2.Columns("FinalGrade").DefaultCellStyle.Format = "0.00"
        End If

        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub


End Class