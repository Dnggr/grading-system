Imports System.Data.Odbc

Public Class frm_SemControl

    ' ════════════════════════════════════════════════════════════════════
#Region "Form Load"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub frm_SemControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadSemStatus()
        LoadSubmissionGrid()
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Load Current Semester Status into Labels"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub LoadSemStatus()
        Try
            Connect_me()

            Dim sql As String = _
                "SELECT school_year, semester, is_locked " & _
                "FROM sem_control LIMIT 1"

            Dim cmd As New OdbcCommand(sql, con)
            Dim dr As OdbcDataReader = cmd.ExecuteReader()

            If dr.Read() Then
                Dim sy As String = dr("school_year").ToString()
                Dim sem As Integer = Convert.ToInt32(dr("semester"))
                Dim lkd As Boolean = Convert.ToInt32(dr("is_locked")) = 1

                lblCurrentSem.Text = "S.Y. " & sy & " - " & _
                                     If(sem = 1, "1st Semester", "2nd Semester")

                If lkd Then
                    lblLockStatus.Text = "LOCKED"
                    lblLockStatus.ForeColor = System.Drawing.Color.Red
                Else
                    lblLockStatus.Text = "OPEN"
                    lblLockStatus.ForeColor = System.Drawing.Color.Green
                End If

                btnIncrement.Enabled = Not lkd

                ToolStripStatusLabel2.Text = "S.Y. " & sy & "  |  Sem " & sem.ToString()
            End If

            dr.Close()

        Catch ex As Exception
            MessageBox.Show("LoadSemStatus error: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Load Teacher Submission Grid"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub LoadSubmissionGrid()
        Try
            Connect_me()

            Dim sql As String = _
                "SELECT " & _
                "  CONCAT(p.firstname, ' ', p.lastname)      AS TeacherName, " & _
                "  gs.school_year                             AS SchoolYear, " & _
                "  CASE WHEN gs.semester = 1 " & _
                "       THEN '1st' ELSE '2nd' END             AS Semester, " & _
                "  CASE WHEN gs.submitted = 1 " & _
                "       THEN 'Submitted' ELSE 'Pending' END   AS Status, " & _
                "  IFNULL(CAST(gs.submitted_at AS CHAR), '-') AS SubmittedAt " & _
                "FROM grade_submission gs " & _
                "INNER JOIN sem_control sc " & _
                "        ON gs.school_year = sc.school_year " & _
                "       AND gs.semester    = sc.semester " & _
                "INNER JOIN prof p ON gs.prof_id = p.prof_id " & _
                "ORDER BY gs.submitted ASC, p.lastname ASC, p.firstname ASC"

            Dim cmd As New OdbcCommand(sql, con)
            Dim da As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvTeachers.DataSource = dt

            If dgvTeachers.Columns.Contains("TeacherName") Then
                dgvTeachers.Columns("TeacherName").HeaderText = "Teacher Name"
            End If
            If dgvTeachers.Columns.Contains("SchoolYear") Then
                dgvTeachers.Columns("SchoolYear").HeaderText = "School Year"
            End If
            If dgvTeachers.Columns.Contains("Semester") Then
                dgvTeachers.Columns("Semester").HeaderText = "Semester"
            End If
            If dgvTeachers.Columns.Contains("Status") Then
                dgvTeachers.Columns("Status").HeaderText = "Status"
            End If
            If dgvTeachers.Columns.Contains("SubmittedAt") Then
                dgvTeachers.Columns("SubmittedAt").HeaderText = "Submitted At"
            End If

            For Each row As DataGridViewRow In dgvTeachers.Rows
                If row.Cells("Status").Value IsNot Nothing Then
                    If row.Cells("Status").Value.ToString() = "Pending" Then
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 204, 204)
                    Else
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(204, 255, 204)
                    End If
                End If
            Next

            Dim totalTeachers As Integer = dt.Rows.Count
            Dim submittedCount As Integer = 0

            For Each dataRow As DataRow In dt.Rows
                If dataRow("Status").ToString() = "Submitted" Then
                    submittedCount += 1
                End If
            Next

            Dim pendingCount As Integer = totalTeachers - submittedCount

            lblTotalTeachers.Text = totalTeachers.ToString() & " teachers"
            lblSubmitted.Text = submittedCount.ToString() & " / " & totalTeachers.ToString()

            If submittedCount = totalTeachers AndAlso totalTeachers > 0 Then
                lblSubmitted.ForeColor = System.Drawing.Color.Green
            Else
                lblSubmitted.ForeColor = System.Drawing.Color.Red
            End If

            If totalTeachers = 0 Then
                ToolStripStatusLabel1.Text = "No teachers found for this semester."
            ElseIf pendingCount = 0 Then
                ToolStripStatusLabel1.Text = "All teachers have submitted. Ready to advance semester."
            Else
                ToolStripStatusLabel1.Text = _
                    "Ready — " & pendingCount.ToString() & _
                    If(pendingCount = 1, " teacher", " teachers") & _
                    " still pending. Advance button will be blocked until all submit."
            End If

            If totalTeachers > 0 AndAlso pendingCount = 0 Then
                btnIncrement.Enabled = True
            ElseIf pendingCount > 0 Then
                btnIncrement.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show("LoadSubmissionGrid error: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Increment Semester Button"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub btnIncrement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIncrement.Click

        ' ── Step 1: Validate all teachers have submitted ──
        If Not AllTeachersSubmitted() Then
            MessageBox.Show( _
                "Cannot advance semester." & vbCrLf & _
                "One or more teachers have NOT submitted grades yet." & vbCrLf & _
                "Check the red rows in the grid.", _
                "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' ── Step 2: Read current values to know what we're advancing FROM ──
        Dim curYear As String = ""
        Dim curSem As Integer = 0

        Try
            Connect_me()
            Dim cmdPeek As New OdbcCommand( _
                "SELECT school_year, semester FROM sem_control LIMIT 1", con)
            Dim drPeek As OdbcDataReader = cmdPeek.ExecuteReader()
            If drPeek.Read() Then
                curYear = drPeek("school_year").ToString()
                curSem = Convert.ToInt32(drPeek("semester"))
            End If
            drPeek.Close()
        Catch ex As Exception
            MessageBox.Show("Could not read current semester: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try

        ' ── Step 3: Decide whether this is a mid-year or year-end advance ──
        '    Sem 1 → Sem 2  : mid-year  (NO year-level change)
        '    Sem 2 → Sem 1  : year-end  (year-level +1, 4th yr graduates)
        Dim isYearEnd As Boolean = (curSem = 2)

        ' Build the confirm message so admin knows exactly what will happen
        Dim confirmMsg As String = _
            "Are you sure you want to advance to the next semester?" & vbCrLf & vbCrLf & _
            "This will:" & vbCrLf & _
            "  • Clear all teacher subject/section assignments" & vbCrLf

        If isYearEnd Then
            confirmMsg &= _
                "  • Promote all students to the next year level" & vbCrLf & _
                "  • Move 4th-year students to GRADUATED status" & vbCrLf & _
                "  • Update their section to the matching next-year section" & vbCrLf & _
                "  • Start a NEW school year" & vbCrLf
        Else
            confirmMsg &= _
                "  • Keep student year levels and sections unchanged" & vbCrLf & _
                "  • Stay in the same school year" & vbCrLf
        End If

        confirmMsg &= vbCrLf & "The admin will need to re-assign teachers after advancing."

        Dim confirm As DialogResult = MessageBox.Show( _
            confirmMsg, _
            "Confirm Semester Advance", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Warning)

        If confirm <> DialogResult.Yes Then Return

        ' ── Step 4: Perform ALL changes inside one transaction ──
        Try
            Connect_me()

            Dim tx As OdbcTransaction = con.BeginTransaction()

            Try
                ' ── A) Calculate new semester / school year ──────────────────
                Dim newYear As String = curYear
                Dim newSem As Integer = curSem

                If curSem = 1 Then
                    ' 1st Sem → 2nd Sem, same school year, NO promotions
                    newSem = 2
                Else
                    ' 2nd Sem → 1st Sem of NEXT school year, WITH promotions
                    newSem = 1
                    Dim parts() As String = curYear.Split("-"c)
                    Dim y1 As Integer = Convert.ToInt32(parts(0)) + 1
                    Dim y2 As Integer = Convert.ToInt32(parts(1)) + 1
                    newYear = y1.ToString() & "-" & y2.ToString()
                End If

                ' ── B) CLEAR profsectionsubject ──────────────────────────────
                '    Unassigns ALL teachers from ALL subjects and sections.
                Dim cmdClearAssign As New OdbcCommand( _
                    "DELETE FROM profsectionsubject", con, tx)
                cmdClearAssign.ExecuteNonQuery()

                ' ── C) UPDATE sem_control ────────────────────────────────────
                Dim cmdUpd As New OdbcCommand( _
                    "UPDATE sem_control " & _
                    "SET school_year = ?, semester = ?, is_locked = 0", _
                    con, tx)
                cmdUpd.Parameters.AddWithValue("@sy", newYear)
                cmdUpd.Parameters.AddWithValue("@sem", newSem)
                cmdUpd.ExecuteNonQuery()

                ' ── D) YEAR-END ONLY: Promote students and graduate 4th years ─
                '    This block runs ONLY when advancing from Sem 2 → new year.
                If isYearEnd Then
                    PromoteAndGraduateStudents(con, tx)
                End If

                ' ── E) Remove any leftover grade_submission rows for new period
                Dim cmdDelSub As New OdbcCommand( _
                    "DELETE FROM grade_submission " & _
                    "WHERE school_year = ? AND semester = ?", _
                    con, tx)
                cmdDelSub.Parameters.AddWithValue("@sy", newYear)
                cmdDelSub.Parameters.AddWithValue("@sem", newSem)
                cmdDelSub.ExecuteNonQuery()

                ' ── F) Re-seed grade_submission for all current teachers ──────
                Dim cmdIns As New OdbcCommand( _
                    "INSERT IGNORE INTO grade_submission " & _
                    "  (prof_id, school_year, semester, submitted) " & _
                    "SELECT prof_id, ?, ?, 0 " & _
                    "FROM   prof " & _
                    "WHERE  LOWER(role) = 'teacher'", _
                    con, tx)
                cmdIns.Parameters.AddWithValue("@sy", newYear)
                cmdIns.Parameters.AddWithValue("@sem", newSem)
                cmdIns.ExecuteNonQuery()

                ' ── G) Commit all changes atomically ────────────────────────
                tx.Commit()

                ' Build success message
                Dim successMsg As String = _
                    "Semester advanced to S.Y. " & newYear & " - " & _
                    If(newSem = 1, "1st", "2nd") & " Semester." & vbCrLf & vbCrLf & _
                    "All teacher subject/section assignments have been cleared." & vbCrLf

                If isYearEnd Then
                    successMsg &= _
                        "Students have been promoted to the next year level." & vbCrLf & _
                        "4th-year students have been marked as GRADUATED." & vbCrLf
                End If

                successMsg &= vbCrLf & "Please re-assign teachers before the new semester begins."

                MessageBox.Show(successMsg, "Success", _
                                MessageBoxButtons.OK, MessageBoxIcon.Information)

                LoadSemStatus()
                LoadSubmissionGrid()

            Catch ex As Exception
                tx.Rollback()
                Throw
            End Try

        Catch ex As Exception
            MessageBox.Show("Increment error: " & ex.Message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try

    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Student Promotion and Graduation Logic"
    ' ════════════════════════════════════════════════════════════════════

    ''' <summary>
    ''' Called ONLY on year-end (Sem 2 → new school year).
    ''' 
    ''' What this does:
    '''   1. Graduates 4th-year students:
    '''        - Sets student.yr_lvl   = NULL  (no longer in a year level)
    '''        - Sets student.section_id = NULL  (no longer in a section)
    '''        - Sets student.role     = 'graduated'
    '''        - Also updates account.yr_lvl and account.role to match
    ''' 
    '''   2. Promotes 1st, 2nd, 3rd-year students:
    '''        - Increments student.yr_lvl by 1
    '''        - Finds the matching section in the section table for the new
    '''          year level + same course, and updates student.section_id
    '''        - Also updates account.yr_lvl to match
    ''' 
    ''' Notes on section matching:
    '''   The section table has year_lvl and course_id.
    '''   We match by:  same course_id  AND  new yr_lvl  AND same section number suffix.
    '''   If no exact match exists, section_id is set to NULL so admin can
    '''   reassign the student manually — the student is NOT lost.
    ''' </summary>
    Private Sub PromoteAndGraduateStudents(ByVal conn As OdbcConnection, _
                                            ByVal tx As OdbcTransaction)

        ' ── Step 1: Get all active (non-graduated) students with their course ──
        Dim cmdLoad As New OdbcCommand( _
            "SELECT s.stud_id, s.acc_id, s.yr_lvl, s.section_id, " & _
            "       sec.course_id, sec.section AS section_name " & _
            "FROM student s " & _
            "LEFT JOIN section sec ON s.section_id = sec.section_id " & _
            "WHERE LOWER(IFNULL(s.role, '')) <> 'graduated' " & _
            "  AND s.yr_lvl IS NOT NULL", _
            conn, tx)

        Dim da As New OdbcDataAdapter(cmdLoad)
        Dim dt As New DataTable()
        da.Fill(dt)

        For Each row As DataRow In dt.Rows
            Dim studId As Integer = Convert.ToInt32(row("stud_id"))
            Dim accId As Object = row("acc_id")
            Dim curYrLvl As Integer = Convert.ToInt32(row("yr_lvl"))
            Dim courseId As Object = row("course_id")   ' may be DBNull

            If curYrLvl >= 4 Then
                ' ── GRADUATE: 4th year (or higher) students ──────────────────
                ' Set role = 'graduated', yr_lvl = NULL, section_id = NULL
                Dim cmdGrad As New OdbcCommand( _
                    "UPDATE student " & _
                    "SET role = 'graduated', yr_lvl = NULL, section_id = NULL " & _
                    "WHERE stud_id = ?", _
                    conn, tx)
                cmdGrad.Parameters.AddWithValue("@sid", studId)
                cmdGrad.ExecuteNonQuery()

                ' Mirror to account table if acc_id is not null
                If Not IsDBNull(accId) AndAlso accId IsNot Nothing Then
                    Dim cmdGradAcc As New OdbcCommand( _
                        "UPDATE account " & _
                        "SET yr_lvl = NULL, role = 'graduated' " & _
                        "WHERE acc_id = ?", _
                        conn, tx)
                    cmdGradAcc.Parameters.AddWithValue("@aid", Convert.ToInt32(accId))
                    cmdGradAcc.ExecuteNonQuery()
                End If

            Else
                ' ── PROMOTE: 1st, 2nd, 3rd-year students ────────────────────
                Dim newYrLvl As Integer = curYrLvl + 1

                ' Find the best matching section for new year level + same course.
                ' Strategy: same course_id + new year_lvl.
                ' If the student's current section name contains a dash-number
                ' (e.g. "BSIT 1-1"), we try to keep the same group number.
                Dim newSectionId As Object = DBNull.Value

                If Not IsDBNull(courseId) AndAlso courseId IsNot Nothing Then
                    ' Try to match the group number suffix (e.g. the "1" in "BSIT 1-1")
                    Dim sectionSuffix As String = ""
                    If Not IsDBNull(row("section_name")) Then
                        Dim secName As String = row("section_name").ToString()
                        ' Extract suffix after the last "-"
                        Dim lastDash As Integer = secName.LastIndexOf("-"c)
                        If lastDash >= 0 AndAlso lastDash < secName.Length - 1 Then
                            sectionSuffix = secName.Substring(lastDash + 1).Trim()
                        End If
                    End If

                    ' Try exact suffix match first
                    If sectionSuffix <> "" Then
                        Dim cmdFindSec As New OdbcCommand( _
                            "SELECT section_id FROM section " & _
                            "WHERE course_id = ? " & _
                            "  AND year_lvl  = ? " & _
                            "  AND section LIKE ? " & _
                            "LIMIT 1", _
                            conn, tx)
                        cmdFindSec.Parameters.AddWithValue("@cid", Convert.ToInt32(courseId))
                        cmdFindSec.Parameters.AddWithValue("@yl", newYrLvl)
                        cmdFindSec.Parameters.AddWithValue("@suffix", "%-" & sectionSuffix)

                        Dim secResult As Object = cmdFindSec.ExecuteScalar()
                        If secResult IsNot Nothing AndAlso Not IsDBNull(secResult) Then
                            newSectionId = Convert.ToInt32(secResult)
                        End If
                    End If

                    ' Fallback: any section for this course + new year level
                    If IsDBNull(newSectionId) Then
                        Dim cmdFallback As New OdbcCommand( _
                            "SELECT section_id FROM section " & _
                            "WHERE course_id = ? " & _
                            "  AND year_lvl  = ? " & _
                            "LIMIT 1", _
                            conn, tx)
                        cmdFallback.Parameters.AddWithValue("@cid", Convert.ToInt32(courseId))
                        cmdFallback.Parameters.AddWithValue("@yl", newYrLvl)

                        Dim fbResult As Object = cmdFallback.ExecuteScalar()
                        If fbResult IsNot Nothing AndAlso Not IsDBNull(fbResult) Then
                            newSectionId = Convert.ToInt32(fbResult)
                        End If
                    End If
                    ' If still no match: newSectionId stays DBNull.Value
                    ' Admin will need to assign the section manually
                End If

                ' Update student table: new yr_lvl + new section_id
                Dim cmdPromote As New OdbcCommand( _
                    "UPDATE student " & _
                    "SET yr_lvl = ?, section_id = ? " & _
                    "WHERE stud_id = ?", _
                    conn, tx)
                cmdPromote.Parameters.AddWithValue("@yl", newYrLvl)
                cmdPromote.Parameters.AddWithValue("@sec", newSectionId)
                cmdPromote.Parameters.AddWithValue("@sid", studId)
                cmdPromote.ExecuteNonQuery()

                ' Mirror yr_lvl to account table if acc_id is not null
                If Not IsDBNull(accId) AndAlso accId IsNot Nothing Then
                    Dim cmdPromoteAcc As New OdbcCommand( _
                        "UPDATE account " & _
                        "SET yr_lvl = ? " & _
                        "WHERE acc_id = ?", _
                        conn, tx)
                    cmdPromoteAcc.Parameters.AddWithValue("@yl", newYrLvl)
                    cmdPromoteAcc.Parameters.AddWithValue("@aid", Convert.ToInt32(accId))
                    cmdPromoteAcc.ExecuteNonQuery()
                End If

            End If
        Next

    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Refresh Button"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadSemStatus()
        LoadSubmissionGrid()
    End Sub

#End Region

    ' ════════════════════════════════════════════════════════════════════
#Region "Close Button"
    ' ════════════════════════════════════════════════════════════════════

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

End Class