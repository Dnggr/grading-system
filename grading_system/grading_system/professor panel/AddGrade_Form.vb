Imports System.Data.Odbc
Public Class AddGrade_Form
    Dim response As DialogResult
    Private lastNumericalGrade As String = ""
    Dim profID As Integer

    Private Sub AddGrade_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        defaultValues()
        getSubject()
        Timer1.Start()
        txtName.Text = Teacher_Form.studFN
        cboRemark.Items.Add("Incomplete")
        cboRemark.Items.Add("Complete")
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
    Private Function checkAllTextboxes(ByVal parent As Control) As Boolean
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is TextBox Then
                Dim txt As TextBox = CType(ctrl, TextBox)

                If txt.Text.Trim() = "" Then
                    MessageBox.Show("Please fill all fields.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt.Focus()
                    Return False
                    Exit Function
                Else
                    Return False
                    Exit Function
                End If
            End If
        Next
    End Function

    Private Function calculateGrade(ByVal raw As Decimal, ByVal max As Decimal, ByVal weight As Decimal) As Decimal
        Dim result As Decimal = (raw / max) * weight
        Return result
    End Function

    Private Sub numbersOnly()
        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf ctrl Is TextBox Then
                Dim txt As TextBox = CType(ctrl, TextBox)

                If Not IsNumeric(txt.Text.Trim()) Then
                    MessageBox.Show("Numbers Only.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt.Focus()
                    Exit Sub
                End If
            End If
        Next
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If checkAllTextboxes(GroupBox1) And checkAllTextboxes(GroupBox2) And checkAllTextboxes(GroupBox3) And checkAllTextboxes(GroupBox4) <> False Then
        insertIt()
        'End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub insertIt()
        Try
            Dim query As String = "INSERT INTO grades " & _
                      "(stud_id, prof_id, sub_id, attendance, quiz, recitation, project, " & _
                      "prelim, midterm, semis, finals, grade, remark) " & _
                      "VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
            Using cmd As New OdbcCommand(query, con)
                cmd.Parameters.AddWithValue("?", CInt(Teacher_Form.studID.Trim))
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
                cmd.Parameters.AddWithValue("?", CDec(txtNumericalGrade.Text.Trim()))
                cmd.Parameters.AddWithValue("?", cboRemark.SelectedItem.ToString())
                Connect_me()
                Dim result As Integer = cmd.ExecuteNonQuery()
                If result > 0 Then
                    MessageBox.Show(result & " row(s) inserted successfully.")
                End If


            End Using
        Catch ex As Exception
            MessageBox.Show("error" & ex.ToString)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub getSubject()
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
                adapter.SelectCommand.Parameters.AddWithValue("?", Teacher_Form.sectionID)
                Dim ds As New DataSet
                adapter.Fill(ds, "profsub")
                If ds.Tables("profsub").Rows.Count > 0 Then
                    cboSubject.DataSource = ds.Tables("profsub")
                    cboSubject.DisplayMember = "Subject"
                    cboSubject.ValueMember = "ID"
                    txtSection.Text = ds.Tables("profsub").Rows(0)("Section").ToString
                    ' Get first ProfID
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
    End Sub


#End Region
#Region "calculate overall numerical and Weight "

    Private Function CalculateNumericalGrade(ByVal GroupBox1 As Control, ByVal GroupBox2 As Control) As Decimal
        Dim Numerical As Decimal = 0
        Dim isValid As Boolean = True

        ' Process GroupBox1
        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txtTotal") Then
                Dim txt As TextBox = CType(ctrl, TextBox)

                If txt.Text.Trim <> "" Then
                    Dim value As Decimal

                    If Decimal.TryParse(txt.Text.Trim, value) Then
                        Numerical += value
                    Else
                        ' Invalid - mark as invalid and stop
                        isValid = False
                        Exit For
                    End If
                End If
            End If
        Next

        ' Only process GroupBox2 if GroupBox1 was valid
        If isValid Then
            For Each ctrl As Control In GroupBox2.Controls
                If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txtTotal") Then
                    Dim txt As TextBox = CType(ctrl, TextBox)

                    If txt.Text.Trim <> "" Then
                        Dim value As Decimal

                        If Decimal.TryParse(txt.Text.Trim, value) Then
                            Numerical += value
                        Else
                            ' Invalid - mark as invalid and stop
                            isValid = False
                            Exit For
                        End If
                    End If
                End If
            Next
        End If

        ' Update ONLY if all values are valid
        If isValid Then
            txtNumericalGrade.Text = Numerical.ToString("0.00")
            Return Numerical
        Else
            ' Optional: Show placeholder or clear
            txtNumericalGrade.Text = "---"
            ' Or keep the last valid value by not updating
        End If
    End Function

    Private Sub CalculateWeightedGrade()
        ' .NET 3.5 compatible check
        If String.IsNullOrEmpty(txtNumericalGrade.Text.Trim()) Then
            txtWeightedGrade.Text = ""
            Return
        End If

        Dim grade As Decimal

        If Decimal.TryParse(txtNumericalGrade.Text.Trim(), grade) Then
            Select Case grade
                Case Is >= 97
                    txtWeightedGrade.Text = "1.0"
                Case 94 To 96
                    txtWeightedGrade.Text = "1.25"
                Case 91 To 93
                    txtWeightedGrade.Text = "1.5"
                Case 88 To 90
                    txtWeightedGrade.Text = "1.75"
                Case 85 To 87
                    txtWeightedGrade.Text = "2.0"
                Case 82 To 84
                    txtWeightedGrade.Text = "2.25"
                Case 79 To 81
                    txtWeightedGrade.Text = "2.5"
                Case 76 To 78
                    txtWeightedGrade.Text = "2.75"
                Case 75
                    txtWeightedGrade.Text = "3.0"
                Case Is < 75
                    txtWeightedGrade.Text = "5.0"
            End Select
        Else
            txtWeightedGrade.Text = ""
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        GroupBox4.Text = DateTime.Now.ToString("hh:mm:ss tt")
        CalculateNumericalGrade(GroupBox1, GroupBox2)
        If txtNumericalGrade.Text <> lastNumericalGrade Then
            lastNumericalGrade = txtNumericalGrade.Text

        End If
    End Sub
    Private Sub txtNumericalGrade_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNumericalGrade.TextChanged
        CalculateWeightedGrade()
    End Sub
#End Region

    
End Class