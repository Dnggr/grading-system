Imports System.Data.Odbc

Public Class AddGrade_Form
    Dim response As DialogResult
    Public profID As Integer
    Public sID As String = ""
    Public FN As String = ""
    Public secID As String = ""
    Public updateMode As Boolean = False
    Public gradeID As String = ""
    'Private isLoadingGrade As Boolean = False  ' ✅ Add flag

    Private Sub AddGrade_Form_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        defaultValues()
        cboRemark.Items.Add("Incomplete")
        cboRemark.Items.Add("Passed")
        cboRemark.Items.Add("Failed")

        If updateMode Then
            ' Load existing grade data
            LoadGradeData(gradeID)
            btnSave.Text = "Update Grade"
            Me.Text = "Edit Grade"
        Else
            ' Normal add mode
            Button1.Visible = True
        End If
    End Sub

    Private Sub LoadGradeData(ByVal gradeId As String)
        Try
            Connect_me()

            Dim query As String = "SELECT " & _
                                 "grades.grades_id, " & _
                                 "grades.school_year, " & _
                                 "grades.semester, " & _
                                 "grades.stud_id, " & _
                                 "grades.prof_id, " & _
                                 "grades.sub_id, " & _
                                 "grades.attendance, " & _
                                 "grades.quiz, " & _
                                 "grades.recitation, " & _
                                 "grades.project, " & _
                                 "grades.prelim, " & _
                                 "grades.midterm, " & _
                                 "grades.semis, " & _
                                 "grades.finals, " & _
                                 "grades.grade, " & _
                                 "grades.numerical, " & _
                                 "grades.remark, " & _
                                 "CONCAT(student.lastname, ', ', student.firstname, ' ', student.middlename) AS FullName, " & _
                                 "student.section_id, " & _
                                 "section.section, " & _
                                 "CONCAT(subject.sub_code, ' - ', subject.sub_name) AS SubjectName " & _
                                 "FROM grades " & _
                                 "INNER JOIN student ON grades.stud_id = student.stud_id " & _
                                 "INNER JOIN section ON student.section_id = section.section_id " & _
                                 "INNER JOIN subject ON grades.sub_id = subject.sub_id " & _
                                 "WHERE grades.grades_id = ?"

            Using cmd As New OdbcCommand(query, con)
                cmd.Parameters.AddWithValue("?", gradeId)

                Using reader As OdbcDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' Store IDs
                        sID = reader("stud_id").ToString()
                        profID = Convert.ToInt32(reader("prof_id"))
                        secID = reader("section_id").ToString()

                        ' Display student info
                        txtName.Text = reader("FullName").ToString()
                        txtName.ReadOnly = True

                        txtSection.Text = reader("section").ToString()
                        txtSection.ReadOnly = True

                        ' Display subject (readonly)
                        cboSubject.Items.Clear()
                        cboSubject.Items.Add(reader("SubjectName").ToString())
                        cboSubject.SelectedIndex = 0
                        cboSubject.Enabled = False

                        ' Load grade components
                        txtTotalAttendance.Text = If(IsDBNull(reader("attendance")), "", reader("attendance").ToString())
                        txtTotalQuiz.Text = If(IsDBNull(reader("quiz")), "", reader("quiz").ToString())
                        txtTotalRecitation.Text = If(IsDBNull(reader("recitation")), "", reader("recitation").ToString())
                        txtTotalProject.Text = If(IsDBNull(reader("project")), "", reader("project").ToString())
                        txtTotalPrelim.Text = If(IsDBNull(reader("prelim")), "", reader("prelim").ToString())
                        txtTotalMidterm.Text = If(IsDBNull(reader("midterm")), "", reader("midterm").ToString())
                        txtTotalSemis.Text = If(IsDBNull(reader("semis")), "", reader("semis").ToString())
                        txtTotalFinals.Text = If(IsDBNull(reader("finals")), "", reader("finals").ToString())

                        ' Load grades
                        txtAverageGrade.Text = If(IsDBNull(reader("grade")), "", reader("grade").ToString())
                        txtNumericalGrade.Text = If(IsDBNull(reader("numerical")), "", reader("numerical").ToString())

                        ' Load remark
                        If Not IsDBNull(reader("remark")) Then
                            cboRemark.SelectedItem = reader("remark").ToString()
                        End If

                        Button1.Visible = False

                    Else
                        MessageBox.Show("Grade record not found", "Error")
                        Me.Close()
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading grade: " & ex.Message, "Error")
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    ' ✅ FIXED: Added missing comma and fixed parameter order
    Private Sub UpdateGrade()
        Try
            ' Validate inputs
            If String.IsNullOrEmpty(txtTotalAttendance.Text) OrElse _
                String.IsNullOrEmpty(txtTotalQuiz.Text) OrElse _
                String.IsNullOrEmpty(txtTotalRecitation.Text) OrElse _
                String.IsNullOrEmpty(txtTotalProject.Text) OrElse _
                String.IsNullOrEmpty(txtTotalPrelim.Text) OrElse _
                String.IsNullOrEmpty(txtTotalMidterm.Text) OrElse _
                String.IsNullOrEmpty(txtTotalSemis.Text) OrElse _
                String.IsNullOrEmpty(txtTotalFinals.Text) OrElse _
                cboRemark.SelectedIndex < 0 Then
                MessageBox.Show("Please fill in all required fields", "Validation Error")
                Return
            End If

            Connect_me()

            ' ✅ FIXED: Added comma after numerical, fixed order
            Dim query As String = "UPDATE grades SET " & _
                                 "attendance = ?, " & _
                                 "quiz = ?, " & _
                                 "recitation = ?, " & _
                                 "project = ?, " & _
                                 "prelim = ?, " & _
                                 "midterm = ?, " & _
                                 "semis = ?, " & _
                                 "finals = ?, " & _
                                 "grade = ?, " & _
                                 "numerical = ?, " & _
                                 "remark = ? " & _
                                 "WHERE grades_id = ?"

            Using cmd As New OdbcCommand(query, con)
                cmd.Parameters.AddWithValue("?", txtTotalAttendance.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalQuiz.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalRecitation.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalProject.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalPrelim.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalMidterm.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalSemis.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalFinals.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtAverageGrade.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtNumericalGrade.Text.Trim())
                cmd.Parameters.AddWithValue("?", cboRemark.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("?", gradeID)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MessageBox.Show("Grade updated successfully!", "Success", _
                                  MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MessageBox.Show("No records were updated", "Warning")
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Error updating grade: " & ex.Message, "Error")
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    ' ✅ FIXED: Parameter names (@pid should be ?)
    Private Sub gradeSubmission()
        Try
            Connect_me()
            Dim sy As String = ""
            Dim sm As Integer = 0
            GetCurrentSem(sy, sm)

            Dim sql As String = _
                "INSERT INTO grade_submission " & _
                "  (prof_id, school_year, semester, submitted, submitted_at) " & _
                "VALUES (?, ?, ?, 1, NOW()) " & _
                "ON DUPLICATE KEY UPDATE submitted=1, submitted_at=NOW()"

            Using cmd As New OdbcCommand(sql, con)
                cmd.Parameters.AddWithValue("?", profID)
                cmd.Parameters.AddWithValue("?", sy)
                cmd.Parameters.AddWithValue("?", sm)
                cmd.ExecuteNonQuery()

                MessageBox.Show("Grades saved and submitted successfully.", _
                              "Saved - " & sy & " Sem " & sm, _
                              MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using

        Catch ex As Exception
            MessageBox.Show("Submit error: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub defaultValues()
        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txtW") Then
                CType(ctrl, TextBox).Text = "10"
            End If
        Next

        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txtMax") Then
                CType(ctrl, TextBox).Text = "100"
            End If
        Next

        For Each ctrl As Control In GroupBox2.Controls
            If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txtWeight") Then
                CType(ctrl, TextBox).Text = "15"
            End If
        Next

        For Each ctrl As Control In GroupBox2.Controls
            If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txtMax") Then
                CType(ctrl, TextBox).Text = "100"
            End If
        Next
    End Sub

    Private Function checkAllTextboxes() As Boolean
        Dim emptyTextbox As TextBox = Nothing
        Dim groupboxName As String = ""

        ' Check GroupBox1
        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf ctrl Is TextBox Then
                Dim txt As TextBox = CType(ctrl, TextBox)
                If txt.Text.Trim() = "" Then
                    emptyTextbox = txt
                    groupboxName = "Other Grades"
                    Exit For
                End If
            End If
        Next

        ' Check GroupBox2
        If emptyTextbox Is Nothing Then
            For Each ctrl2 As Control In GroupBox2.Controls
                If TypeOf ctrl2 Is TextBox Then
                    Dim txt2 As TextBox = CType(ctrl2, TextBox)
                    If txt2.Text.Trim() = "" Then
                        emptyTextbox = txt2
                        groupboxName = "Major Exam"
                        Exit For
                    End If
                End If
            Next
        End If

        ' Check GroupBox3
        If emptyTextbox Is Nothing Then
            For Each ctrl3 As Control In GroupBox3.Controls
                If TypeOf ctrl3 Is TextBox Then
                    Dim txt3 As TextBox = CType(ctrl3, TextBox)
                    If txt3.Text.Trim() = "" Then
                        emptyTextbox = txt3
                        groupboxName = "Student Info and Subject"
                        Exit For
                    End If
                End If
            Next
        End If

        ' Check GroupBox4 and ComboBox
        If emptyTextbox Is Nothing Then
            For Each ctrl4 As Control In GroupBox4.Controls
                If TypeOf ctrl4 Is TextBox Then
                    Dim txt4 As TextBox = CType(ctrl4, TextBox)
                    If txt4.Text.Trim() = "" Then
                        emptyTextbox = txt4
                        groupboxName = "Final Grade"
                        Exit For
                    End If
                End If
            Next

            ' Check ComboBox separately
            If emptyTextbox Is Nothing AndAlso cboRemark.SelectedIndex < 0 Then
                MessageBox.Show("Please select a remark.", "Final Grade", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cboRemark.Focus()
                Return False
            End If
        End If

        If emptyTextbox IsNot Nothing Then
            MessageBox.Show("Please fill all fields.", groupboxName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            emptyTextbox.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        If updateMode Then
            UpdateGrade()
        Else
            If checkAllTextboxes() Then
                insertIt()
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub insertIt()
        Try
            Connect_me()

            ' ✅ ADDED: Double-check that grade doesn't already exist before inserting
            Dim checkQuery As String = "SELECT COUNT(*) FROM grades " & _
                                       "WHERE stud_id = ? " & _
                                       "AND prof_id = ? " & _
                                       "AND sub_id = ? " & _
                                       "AND school_year = ? " & _
                                       "AND semester = ?"

            Using checkCmd As New OdbcCommand(checkQuery, con)
                checkCmd.Parameters.AddWithValue("?", CInt(sID.Trim()))
                checkCmd.Parameters.AddWithValue("?", profID)
                checkCmd.Parameters.AddWithValue("?", CInt(cboSubject.SelectedValue))
                checkCmd.Parameters.AddWithValue("?", Teacher_Form.sy.Trim())
                checkCmd.Parameters.AddWithValue("?", Teacher_Form.sm)

                Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                If count > 0 Then
                    MessageBox.Show("Cannot save: This student already has a grade for this subject,school year and semester " & _
                                    ". Check the dashboard.", _
                                  "Duplicate Grade", _
                                  MessageBoxButtons.OK, _
                                  MessageBoxIcon.Error)

                    Return
                End If
            End Using

            Dim query As String = "INSERT INTO grades " & _
                      "(school_year, semester, stud_id, prof_id, sub_id, attendance, quiz, recitation, project, " & _
                      "prelim, midterm, semis, finals, grade, numerical, remark) " & _
                      "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"

            Using cmd As New OdbcCommand(query, con)
                cmd.Parameters.AddWithValue("?", Teacher_Form.sy.Trim())
                cmd.Parameters.AddWithValue("?", Teacher_Form.sm)
                cmd.Parameters.AddWithValue("?", CInt(sID.Trim()))
                cmd.Parameters.AddWithValue("?", profID)
                cmd.Parameters.AddWithValue("?", CInt(cboSubject.SelectedValue))
                cmd.Parameters.AddWithValue("?", txtTotalAttendance.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalQuiz.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalRecitation.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalProject.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalPrelim.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalMidterm.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalSemis.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalFinals.Text.Trim())
                cmd.Parameters.AddWithValue("?", CDec(txtAverageGrade.Text.Trim()))
                cmd.Parameters.AddWithValue("?", txtNumericalGrade.Text.Trim())
                cmd.Parameters.AddWithValue("?", cboRemark.SelectedItem.ToString())

                Dim result As Integer = cmd.ExecuteNonQuery()

                If result > 0 Then
                    gradeSubmission()
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Insert Error")
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub getSubject(ByVal secID As String)
        Try
            Connect_me()

            Dim query As String = "SELECT DISTINCT " & _
                                 "subject.sub_id AS SubjectID, " & _
                                 "CONCAT(subject.sub_code, '-', subject.sub_name) AS Subject, " & _
                                 "section.section AS Section, " & _
                                 "prof.prof_id AS ProfId " & _
                                 "FROM profsectionsubject " & _
                                 "LEFT JOIN prof ON profsectionsubject.prof_id = prof.prof_id " & _
                                 "LEFT JOIN subject ON profsectionsubject.sub_id = subject.sub_id " & _
                                 "LEFT JOIN section ON profsectionsubject.section_id = section.section_id " & _
                                 "WHERE prof.acc_id = ? " & _
                                 "AND profsectionsubject.section_id = ? " & _
                                 "ORDER BY subject.sub_code"

            Using adapter As New OdbcDataAdapter(query, con)
                adapter.SelectCommand.Parameters.AddWithValue("?", Teacher_Form.acc_id)
                adapter.SelectCommand.Parameters.AddWithValue("?", secID)

                Dim dt As New DataTable()
                adapter.Fill(dt)

                If dt.Rows.Count > 0 Then
                    ' ✅ Store section and prof info before adding placeholder
                    txtSection.Text = dt.Rows(0)("Section").ToString()
                    profID = CInt(dt.Rows(0)("ProfId"))

                    ' ✅ Add placeholder row at the beginning
                    Dim placeholderRow As DataRow = dt.NewRow()
                    placeholderRow("SubjectID") = 0
                    placeholderRow("Subject") = "-- Select a subject --"
                    placeholderRow("Section") = txtSection.Text
                    placeholderRow("ProfId") = profID
                    dt.Rows.InsertAt(placeholderRow, 0)

                    ' Bind to ComboBox
                    cboSubject.DataSource = dt
                    cboSubject.DisplayMember = "Subject"
                    cboSubject.ValueMember = "SubjectID"
                    cboSubject.SelectedIndex = 0
                Else
                    MessageBox.Show("No subjects found for this section", "Info")
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Get Subject")
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub cboSubject_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboSubject.SelectedIndexChanged
        ' ✅ FIXED: Don't check if already loading or in update mode
        'If isLoadingGrade Then Return
        'If updateMode Then Return ' Don't check again if already in update mode

        If cboSubject.SelectedIndex = 0 Then
            ' User selected "-- Select a subject --", clear fields
            ClearGradeFields()
            Return
        End If

        If cboSubject.SelectedIndex >= 0 AndAlso _
           Not String.IsNullOrEmpty(sID) AndAlso _
           profID > 0 AndAlso _
           Not String.IsNullOrEmpty(Teacher_Form.sy) AndAlso _
           Teacher_Form.sm > 0 Then
            'GroupBox4.Text = Convert.ToInt32(cboSubject.SelectedValue)
            CheckAndLoadExistingGrade()
        End If
    End Sub

    Private Sub CheckAndLoadExistingGrade()
        Try
            ' Validate all inputs
            If cboSubject.SelectedIndex < 0 Then Return
            If String.IsNullOrEmpty(sID.Trim()) Then Return
            If profID = 0 Then Return
            If String.IsNullOrEmpty(Teacher_Form.sy) OrElse Teacher_Form.sm = 0 Then Return

            'isLoadingGrade = True  ' ✅ Set flag
            Connect_me()

            ' Get subject ID
            Dim subjectId As Integer = 0

            If IsNumeric(cboSubject.SelectedValue) Then
                subjectId = Convert.ToInt32(cboSubject.SelectedValue)
            ElseIf TypeOf cboSubject.SelectedValue Is DataRowView Then
                Dim row As DataRowView = CType(cboSubject.SelectedValue, DataRowView)
                If Not Integer.TryParse(row("SubjectID").ToString(), subjectId) Then
                    Return
                End If
            Else
                Return
            End If

            If subjectId = 0 Then Return

            ' Convert sID to integer safely
            Dim studentId As Integer = 0
            If Not Integer.TryParse(sID, studentId) Then
                MessageBox.Show("Invalid student ID", "Error")
                Return
            End If

            Dim query As String = "SELECT * FROM grades " & _
                                 "WHERE stud_id = ? " & _
                                 "AND prof_id = ? " & _
                                 "AND sub_id = ? " & _
                                 "AND school_year = ? " & _
                                 "AND semester = ?"

            Using cmd As New OdbcCommand(query, con)
                cmd.Parameters.AddWithValue("?", studentId)
                cmd.Parameters.AddWithValue("?", profID)
                cmd.Parameters.AddWithValue("?", subjectId)
                cmd.Parameters.AddWithValue("?", Teacher_Form.sy)
                cmd.Parameters.AddWithValue("?", Teacher_Form.sm)

                Using reader As OdbcDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim result As DialogResult = MessageBox.Show( _
                            "This student already has a grade for this subject." & vbCrLf & vbCrLf & _
                            "Load existing grade for editing?", _
                            "Existing Grade Found", _
                            MessageBoxButtons.YesNo, _
                            MessageBoxIcon.Question)

                        If result = DialogResult.Yes Then
                            ' ✅ Switch to update mode
                            gradeID = reader("grades_id").ToString()
                            updateMode = True
                            LoadGradeFields(reader)
                            btnSave.Text = "Update Grade"
                            Me.Text = "Edit Grade"
                        Else
                            ' ✅ User said No - clear and stay in add mode
                            ClearGradeFields()
                            gradeID = ""
                            updateMode = False
                            btnSave.Text = "Save Grade"
                        End If
                    Else
                        ' ✅ No existing grade - new entry mode
                        ClearGradeFields()
                        gradeID = ""
                        updateMode = False
                        btnSave.Text = "Save Grade"
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.ToString & vbCrLf & vbCrLf & _
                           "Debug Info:" & vbCrLf & _
                           "Student ID: '" & sID & "'" & vbCrLf & _
                           "Prof ID: " & profID & vbCrLf & _
                           "School Year: '" & Teacher_Form.sy & "'" & vbCrLf & _
                           "Semester: " & Teacher_Form.sm, _
                           "Check Existing Grade Error")
        Finally
            'isLoadingGrade = False  ' ✅ Reset flag
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub LoadGradeFields(ByVal reader As OdbcDataReader)
        txtTotalAttendance.Text = If(IsDBNull(reader("attendance")), "", reader("attendance").ToString())
        txtTotalQuiz.Text = If(IsDBNull(reader("quiz")), "", reader("quiz").ToString())
        txtTotalRecitation.Text = If(IsDBNull(reader("recitation")), "", reader("recitation").ToString())
        txtTotalProject.Text = If(IsDBNull(reader("project")), "", reader("project").ToString())
        txtTotalPrelim.Text = If(IsDBNull(reader("prelim")), "", reader("prelim").ToString())
        txtTotalMidterm.Text = If(IsDBNull(reader("midterm")), "", reader("midterm").ToString())
        txtTotalSemis.Text = If(IsDBNull(reader("semis")), "", reader("semis").ToString())
        txtTotalFinals.Text = If(IsDBNull(reader("finals")), "", reader("finals").ToString())
        txtAverageGrade.Text = If(IsDBNull(reader("grade")), "", reader("grade").ToString())
        txtNumericalGrade.Text = If(IsDBNull(reader("numerical")), "", reader("numerical").ToString())

        If Not IsDBNull(reader("remark")) Then
            cboRemark.SelectedItem = reader("remark").ToString()
        End If
    End Sub

    Private Sub ClearGradeFields()
        txtTotalAttendance.Clear()
        txtTotalQuiz.Clear()
        txtTotalRecitation.Clear()
        txtTotalProject.Clear()
        txtTotalPrelim.Clear()
        txtTotalMidterm.Clear()
        txtTotalSemis.Clear()
        txtTotalFinals.Clear()
        txtNumericalGrade.Clear()
        txtAverageGrade.Clear()
        cboRemark.SelectedIndex = -1
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Dim selectForm As New SelectStudent_Form()
        selectForm.ParentForm = Me
        selectForm.ShowDialog()
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtName.TextChanged
        If Not updateMode AndAlso Not String.IsNullOrEmpty(secID) Then
            getSubject(secID)
        End If
    End Sub



#Region "Other Grades Computation"
    Private Sub txtWAttendance_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRawAttendance.TextChanged
        Dim raw, max, weight, result As Decimal

        If txtRawAttendance.Text.Trim = "" OrElse _
           txtMaxAttendance.Text.Trim = "" OrElse _
           txtWAttendance.Text.Trim = "" Then

            txtTotalAttendance.Text = ""

            Exit Sub
        End If

        If Not IsNumeric(txtRawAttendance.Text.Trim) Then
            txtRawAttendance.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtMaxAttendance.Text.Trim) Then
            txtMaxAttendance.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtWAttendance.Text.Trim) Then
            txtWAttendance.Text = ""
            MessageBox.Show("Numbers only ", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        raw = CDec(txtRawAttendance.Text.Trim)
        max = CDec(txtMaxAttendance.Text.Trim)
        weight = CDec(txtWAttendance.Text.Trim)

        If raw > max Then
            txtRawAttendance.Text = ""
            MessageBox.Show("Raw Must be lower or equal to max")
            Exit Sub
        End If

        If max = 0 Then
            MessageBox.Show("Max cannot be zero.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        result = (raw / max) * weight
        txtTotalAttendance.Text = result.ToString("0.00")
        CalculateWeightedGrade()
    End Sub

    Private Sub txtWQuiz_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRawQuiz.TextChanged
        Dim raw, max, weight, result As Decimal

        If txtRawQuiz.Text.Trim = "" OrElse _
           txtMaxQuiz.Text.Trim = "" OrElse _
           txtWQuiz.Text.Trim = "" Then

            txtTotalQuiz.Text = ""

            Exit Sub
        End If


        If Not IsNumeric(txtRawQuiz.Text.Trim) Then
            txtRawQuiz.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtMaxQuiz.Text.Trim) Then
            txtMaxQuiz.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtWQuiz.Text.Trim) Then
            txtWQuiz.Text = ""
            MessageBox.Show("Numbers only ", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        raw = CDec(txtRawQuiz.Text.Trim)
        max = CDec(txtMaxQuiz.Text.Trim)
        weight = CDec(txtWQuiz.Text.Trim)

        If raw > max Then
            txtRawQuiz.Text = ""
            MessageBox.Show("Raw Must be lower or equal to max")
            Exit Sub
        End If

        If max = 0 Then
            MessageBox.Show("Max cannot be zero.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        result = (raw / max) * weight
        txtTotalQuiz.Text = result.ToString("0.00")
        CalculateWeightedGrade()
    End Sub

    Private Sub txtWRecitation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRawRecitation.TextChanged
        Dim raw, max, weight, result As Decimal

        If txtRawRecitation.Text.Trim = "" OrElse _
           txtMaxRecitation.Text.Trim = "" OrElse _
           txtWRecitation.Text.Trim = "" Then

            txtTotalRecitation.Text = ""

            Exit Sub
        End If

        If Not IsNumeric(txtRawRecitation.Text.Trim) Then
            txtRawRecitation.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtMaxRecitation.Text.Trim) Then
            txtMaxRecitation.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtWRecitation.Text.Trim) Then
            txtWRecitation.Text = ""
            MessageBox.Show("Numbers only ", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        raw = CDec(txtRawRecitation.Text.Trim)
        max = CDec(txtMaxRecitation.Text.Trim)
        weight = CDec(txtWRecitation.Text.Trim)

        If raw > max Then
            txtRawRecitation.Text = ""
            MessageBox.Show("Raw Must be lower or equal to max")
            Exit Sub
        End If

        If max = 0 Then
            MessageBox.Show("Max cannot be zero.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        result = (raw / max) * weight
        txtTotalRecitation.Text = result.ToString("0.00")
        CalculateWeightedGrade()
    End Sub


    Private Sub txtWProject_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRawProject.TextChanged
        Dim raw, max, weight, result As Decimal

        If txtRawProject.Text.Trim = "" OrElse _
           txtMaxProject.Text.Trim = "" OrElse _
           txtWProject.Text.Trim = "" Then

            txtTotalProject.Text = ""

            Exit Sub
        End If

        If Not IsNumeric(txtRawProject.Text.Trim) Then
            txtRawProject.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtMaxProject.Text.Trim) Then
            txtMaxProject.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtWProject.Text.Trim) Then
            txtWProject.Text = ""
            MessageBox.Show("Numbers only ", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        raw = CDec(txtRawProject.Text.Trim)
        max = CDec(txtMaxProject.Text.Trim)
        weight = CDec(txtWProject.Text.Trim)

        If raw > max Then
            txtRawProject.Text = ""
            MessageBox.Show("Raw Must be lower or equal to max")
            Exit Sub
        End If

        If max = 0 Then
            MessageBox.Show("Max cannot be zero.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        result = (raw / max) * weight
        txtTotalProject.Text = result.ToString("0.00")
        CalculateWeightedGrade()
    End Sub
#End Region
#Region "Major Examinations"
    Private Sub txtWPrelim_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRawPrelim.TextChanged
        Dim raw, max, weight, result As Decimal

        If txtRawPrelim.Text.Trim = "" OrElse _
           txtMaxPrelim.Text.Trim = "" OrElse _
           txtWeightPrelim.Text.Trim = "" Then

            txtTotalPrelim.Text = ""

            Exit Sub
        End If

        If Not IsNumeric(txtRawPrelim.Text.Trim) Then
            txtRawPrelim.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtMaxPrelim.Text.Trim) Then
            txtMaxPrelim.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtWeightPrelim.Text.Trim) Then
            txtWeightPrelim.Text = ""
            MessageBox.Show("Numbers only ", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        raw = CDec(txtRawPrelim.Text.Trim)
        max = CDec(txtMaxPrelim.Text.Trim)
        weight = CDec(txtWeightPrelim.Text.Trim)

        If raw > max Then
            txtRawPrelim.Text = ""
            MessageBox.Show("Raw Must be lower or equal to max")
            Exit Sub
        End If

        If max = 0 Then
            MessageBox.Show("Max cannot be zero.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If

        result = (raw / max) * weight
        txtTotalPrelim.Text = result.ToString("0.00")
        CalculateWeightedGrade()
    End Sub

    Private Sub txtRawMidterm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRawMidterm.TextChanged
        Dim raw, max, weight, result As Decimal

        If txtRawMidterm.Text.Trim = "" OrElse _
           txtMaxMidterm.Text.Trim = "" OrElse _
           txtWeightMidterm.Text.Trim = "" Then

            txtTotalMidterm.Text = ""
            Exit Sub
        End If


        If Not IsNumeric(txtRawMidterm.Text.Trim) Then
            txtRawMidterm.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtRawMidterm.Text.Trim) Then
            txtRawMidterm.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtRawMidterm.Text.Trim) Then
            txtRawMidterm.Text = ""
            MessageBox.Show("Numbers only ", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        raw = CDec(txtRawMidterm.Text.Trim)
        max = CDec(txtMaxMidterm.Text.Trim)
        weight = CDec(txtWeightMidterm.Text.Trim)

        If raw > max Then
            txtRawMidterm.Text = ""
            MessageBox.Show("Raw Must be lower or equal to max")
            Exit Sub
        End If

        If max = 0 Then
            MessageBox.Show("Max cannot be zero.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If

        result = (raw / max) * weight
        txtTotalMidterm.Text = result.ToString("0.00")
        CalculateWeightedGrade()
    End Sub

    Private Sub txtRawSemis_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRawSemis.TextChanged
        Dim raw, max, weight, result As Decimal

        If txtRawSemis.Text.Trim = "" OrElse _
           txtMaxSemis.Text.Trim = "" OrElse _
           txtWeightSemis.Text.Trim = "" Then

            txtTotalSemis.Text = ""
            Exit Sub
        End If

        If Not IsNumeric(txtRawSemis.Text.Trim) Then
            txtRawSemis.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtMaxSemis.Text.Trim) Then
            txtMaxSemis.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtWeightSemis.Text.Trim) Then
            txtWeightSemis.Text = ""
            MessageBox.Show("Numbers only ", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        raw = CDec(txtRawSemis.Text.Trim)
        max = CDec(txtMaxSemis.Text.Trim)
        weight = CDec(txtWeightSemis.Text.Trim)

        If raw > max Then
            txtRawSemis.Text = ""
            MessageBox.Show("Raw Must be lower or equal to max")
            Exit Sub
        End If

        If max = 0 Then
            MessageBox.Show("Max cannot be zero.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If

        result = (raw / max) * weight
        txtTotalSemis.Text = result.ToString("0.00")
        CalculateWeightedGrade()
    End Sub

    Private Sub txtRawFinals_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRawFinals.TextChanged
        Dim raw, max, weight, result As Decimal

        If txtRawFinals.Text.Trim = "" OrElse _
           txtMaxFinals.Text.Trim = "" OrElse _
           txtWeightFinals.Text.Trim = "" Then

            txtTotalFinals.Text = ""
            Exit Sub
        End If

        If Not IsNumeric(txtRawFinals.Text.Trim) Then
            txtRawFinals.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtMaxFinals.Text.Trim) Then
            txtMaxFinals.Text = ""
            MessageBox.Show("Numbers only", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If Not IsNumeric(txtWeightFinals.Text.Trim) Then
            txtWeightFinals.Text = ""
            MessageBox.Show("Numbers only ", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        raw = CDec(txtRawFinals.Text.Trim)
        max = CDec(txtMaxFinals.Text.Trim)
        weight = CDec(txtWeightFinals.Text.Trim)

        If raw > max Then
            txtRawFinals.Text = ""
            MessageBox.Show("Raw Must be lower or equal to max")
            Exit Sub
        End If

        If max = 0 Then
            MessageBox.Show("Max cannot be zero.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If

        result = (raw / max) * weight
        txtTotalFinals.Text = result.ToString("0.00")
        CalculateWeightedGrade()
    End Sub


#End Region
#Region "calculate overall numerical and Weight "

    Private Sub txtNumericalGrade_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAverageGrade.TextChanged
        If String.IsNullOrEmpty(txtAverageGrade.Text) Then Return
        Select Case CInt(txtAverageGrade.Text)
            Case Is >= 97
                txtNumericalGrade.Text = "1.0"
            Case 94 To 96
                txtNumericalGrade.Text = "1.25"
            Case 91 To 93
                txtNumericalGrade.Text = "1.5"
            Case 88 To 90
                txtNumericalGrade.Text = "1.75"
            Case 85 To 87
                txtNumericalGrade.Text = "2.0"
            Case 82 To 84
                txtNumericalGrade.Text = "2.25"
            Case 79 To 81
                txtNumericalGrade.Text = "2.5"
            Case 76 To 78
                txtNumericalGrade.Text = "2.75"
            Case 75
                txtNumericalGrade.Text = "3.0"
            Case Is < 75
                txtNumericalGrade.Text = "5.0"
        End Select
    End Sub

    Private Sub CalculateWeightedGrade()
        ' Always start fresh at 0
        Dim Grade As Decimal = 0
        Dim isValid As Boolean = True

        ' Process GroupBox1
        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txtTotal") Then
                Dim txt As TextBox = CType(ctrl, TextBox)

                ' Only add if textbox has content
                If txt.Text.Trim() <> "" Then
                    Dim value As Decimal

                    ' Parse FIRST, then add
                    If Decimal.TryParse(txt.Text.Trim(), value) Then
                        Grade += value  ' ← Add AFTER successful parse
                    Else
                        isValid = False
                        Exit For
                    End If
                End If
                ' If textbox is empty - skip it (add nothing)
            End If
        Next

        ' Process GroupBox2
        If isValid Then
            For Each ctrl As Control In GroupBox2.Controls
                If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txtTotal") Then
                    Dim txt As TextBox = CType(ctrl, TextBox)

                    If txt.Text.Trim() <> "" Then
                        Dim value As Decimal

                        If Decimal.TryParse(txt.Text.Trim(), value) Then
                            Grade += value
                        Else
                            isValid = False
                            Exit For
                        End If
                    End If
                End If
            Next
        End If

        ' Update display
        If isValid Then
            txtAverageGrade.Text = Grade.ToString("0.00")
        Else
            txtAverageGrade.Text = "0.00"
        End If
    End Sub
#End Region


End Class