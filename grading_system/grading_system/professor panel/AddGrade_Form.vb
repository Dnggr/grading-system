Imports System.Data.Odbc
Public Class AddGrade_Form
    Dim response As DialogResult
    Public profID As Integer
    Public sID, FN, secID As String
    Public updateMode As Boolean = False
    Public gradeID As String

    Private Sub AddGrade_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        defaultValues()
        cboRemark.Items.Add("Incomplete")
        cboRemark.Items.Add("Passed")
        cboRemark.Items.Add("Failed")

        If updateMode Then
            ' ✅ Load existing grade data
            LoadGradeData(gradeID)

            ' Change button text
            btnSave.Text = "Update Grade"
            Me.Text = "Edit Grade"
        Else
            ' Normal add mode
            Button1.Visible = True
            'getSubject(secID)
            'txtName.Text = FN
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
                        ' ✅ Store IDs
                        sID = reader("stud_id").ToString()
                        profID = Convert.ToInt32(reader("prof_id"))
                        secID = reader("section_id").ToString()

                        ' ✅ Display student info
                        txtName.Text = reader("FullName").ToString()
                        txtSection.Text = reader("section").ToString()

                        cboSubject.Items.Clear()
                        cboSubject.Items.Add(reader("SubjectName").ToString())
                        cboSubject.SelectedIndex = 0
                        cboSubject.Enabled = False  ' Disable editing

                        ' ✅ Load grade components
                        If Not IsDBNull(reader("attendance")) Then
                            txtTotalAttendance.Text = reader("attendance").ToString()
                        End If

                        If Not IsDBNull(reader("quiz")) Then
                            txtTotalQuiz.Text = reader("quiz").ToString()
                        End If

                        If Not IsDBNull(reader("recitation")) Then
                            txtTotalRecitation.Text = reader("recitation").ToString()
                        End If

                        If Not IsDBNull(reader("project")) Then
                            txtTotalProject.Text = reader("project").ToString()
                        End If

                        If Not IsDBNull(reader("prelim")) Then
                            txtTotalPrelim.Text = reader("prelim").ToString()
                        End If

                        If Not IsDBNull(reader("midterm")) Then
                            txtTotalMidterm.Text = reader("midterm").ToString()
                        End If

                        If Not IsDBNull(reader("semis")) Then
                            txtTotalSemis.Text = reader("semis").ToString()
                        End If

                        If Not IsDBNull(reader("finals")) Then
                            txtTotalFinals.Text = reader("finals").ToString()
                        End If

                        ' ✅ Load final grade
                        If Not IsDBNull(reader("grade")) Then
                            txtAverageGrade.Text = reader("grade").ToString()
                        End If

                        ' ✅ Load remark
                        If Not IsDBNull(reader("remark")) Then
                            cboRemark.SelectedItem = reader("remark").ToString()
                        End If

                        ' Hide select student button
                        Button1.Visible = False

                        ' Make student info readonly
                        txtName.ReadOnly = True
                        txtSection.ReadOnly = True

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
            Dim cmd As New OdbcCommand(sql, con)
            cmd.Parameters.AddWithValue("@pid", profID)  ' your session prof_id variable
            cmd.Parameters.AddWithValue("@sy", sy)
            cmd.Parameters.AddWithValue("@sem", sm)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Grades saved and submitted successfully.", _
                            "Saved" & sy & sm, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Submit error: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub defaultValues()
        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txtW") Then
                CType(ctrl, TextBox).Text = 10
            End If
        Next

        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txtMax") Then
                CType(ctrl, TextBox).Text = 100
            End If
        Next

        For Each ctrl As Control In GroupBox2.Controls
            If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txtWeight") Then
                CType(ctrl, TextBox).Text = 15
            End If
        Next

        For Each ctrl As Control In GroupBox2.Controls
            If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txtMax") Then
                CType(ctrl, TextBox).Text = 100
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

        ' If found, skip other checks
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

        If emptyTextbox Is Nothing Then
            For Each ctrl4 As Control In GroupBox4.Controls
                If TypeOf ctrl4 Is TextBox Then
                    Dim txt4 As TextBox = CType(ctrl4, TextBox)
                    If txt4.Text.Trim() = "" Or cboRemark.SelectedItem = "" Then
                        emptyTextbox = txt4
                        groupboxName = "Final Grade"
                        Exit For
                    End If
                End If
            Next
        End If

        If emptyTextbox IsNot Nothing Then
            MessageBox.Show("Please fill all fields.", groupboxName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            emptyTextbox.Focus()
            Return False
        End If

        Return True

    End Function


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If updateMode Then
            UpdateGrade()
            Exit Sub
        Else
            If checkAllTextboxes() Then
                insertIt()
            End If
        End If

    End Sub

    Private Function calculateGrade(ByVal raw As Decimal, ByVal max As Decimal, ByVal weight As Decimal) As Decimal
        Dim result As Decimal = (raw / max) * weight
        Return result
    End Function


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub insertIt()
        Try
            Dim query As String = "INSERT INTO grades " & _
                      "(school_year, semester, stud_id, prof_id, sub_id, attendance, quiz, recitation, project, " & _
                      "prelim, midterm, semis, finals, grade, remark) " & _
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?,?)"
            Using cmd As New OdbcCommand(query, con)
                cmd.Parameters.AddWithValue("?", Teacher_Form.sy.ToString().Trim())
                cmd.Parameters.AddWithValue("?", CInt(Teacher_Form.sm.ToString.Trim))
                cmd.Parameters.AddWithValue("?", CInt(sID.Trim))
                cmd.Parameters.AddWithValue("?", profID)
                cmd.Parameters.AddWithValue("?", CInt(cboSubject.SelectedValue.ToString()))
                cmd.Parameters.AddWithValue("?", txtTotalAttendance.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalQuiz.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalRecitation.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalProject.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalPrelim.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalMidterm.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalSemis.Text.Trim())
                cmd.Parameters.AddWithValue("?", txtTotalFinals.Text.Trim())
                cmd.Parameters.AddWithValue("?", CDec(txtAverageGrade.Text.Trim()))
                cmd.Parameters.AddWithValue("?", cboRemark.SelectedItem.ToString())
                Connect_me()
                Dim result As Integer = cmd.ExecuteNonQuery()
                If result > 0 Then
                    gradeSubmission()
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("error" & ex.ToString)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub getSubject(ByVal secID As String)
        Try
            Dim query As String = "SELECT distinct profsectionsubject.id As ID," & _
                           "concat(subject.sub_code,'-',subject.sub_name) As Subject, " & _
                           "section.section As Section, prof.prof_id As ProfId " & _
                           "From profsectionsubject " & _
                           "Left Join prof On profsectionsubject.prof_id = prof.prof_id " & _
                           "Left Join subject On profsectionsubject.sub_id = subject.sub_id " & _
                           "Left Join section On profsectionsubject.section_id = section.section_id " & _
                           "Where prof.acc_id = ? AND profsectionsubject.section_id =?"
            Using adapter As New OdbcDataAdapter(query, con)
                adapter.SelectCommand.Parameters.AddWithValue("?", Teacher_Form.acc_id)
                adapter.SelectCommand.Parameters.AddWithValue("?", secID)
                Dim ds As New DataSet
                adapter.Fill(ds, "profsub")
                If ds.Tables("profsub").Rows.Count > 0 Then
                    cboSubject.DataSource = ds.Tables("profsub")
                    cboSubject.DisplayMember = "Subject"
                    cboSubject.ValueMember = "ID"
                    txtSection.Text = ds.Tables("profsub").Rows(0)("Section").ToString
                    profID = CInt(ds.Tables("profsub").Rows(0)("ProfId"))
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("error:" & ex.ToString)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
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


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim selectForm As New SelectStudent_Form
        selectForm.ParentForm = Me  ' Pass reference
        selectForm.ShowDialog()
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        If updateMode <> True Then
            getSubject(secID)
        End If
    End Sub
End Class