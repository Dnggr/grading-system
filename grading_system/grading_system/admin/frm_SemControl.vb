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

                ' ── Main semester label ──
                lblCurrentSem.Text = "S.Y. " & sy & " - " & _
                                     If(sem = 1, "1st Semester", "2nd Semester")

                ' ── Lock status label ──
                If lkd Then
                    lblLockStatus.Text = "LOCKED"
                    lblLockStatus.ForeColor = System.Drawing.Color.Red
                Else
                    lblLockStatus.Text = "OPEN"
                    lblLockStatus.ForeColor = System.Drawing.Color.Green
                End If

                btnIncrement.Enabled = Not lkd

                ' ── FIX: Update ToolStripStatusLabel2 (right panel - S.Y. + Sem) ──
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

            ' ── Friendly column headers ──
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

            ' ── Colour rows: light red = Pending, light green = Submitted ──
            For Each row As DataGridViewRow In dgvTeachers.Rows
                If row.Cells("Status").Value IsNot Nothing Then
                    If row.Cells("Status").Value.ToString() = "Pending" Then
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 204, 204)
                    Else
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(204, 255, 204)
                    End If
                End If
            Next

            ' ══════════════════════════════════════════════════════════
            ' FIX: Count totals directly from the DataTable just loaded.
            ' No second DB query needed — numbers always match the grid.
            ' ══════════════════════════════════════════════════════════
            Dim totalTeachers As Integer = dt.Rows.Count
            Dim submittedCount As Integer = 0

            For Each dataRow As DataRow In dt.Rows
                If dataRow("Status").ToString() = "Submitted" Then
                    submittedCount += 1
                End If
            Next

            Dim pendingCount As Integer = totalTeachers - submittedCount

            ' ── FIX: lblTotalTeachers ──
            lblTotalTeachers.Text = totalTeachers.ToString() & " teachers"

            ' ── FIX: lblSubmitted  (e.g. "3 / 5") ──
            lblSubmitted.Text = submittedCount.ToString() & " / " & totalTeachers.ToString()

            If submittedCount = totalTeachers AndAlso totalTeachers > 0 Then
                lblSubmitted.ForeColor = System.Drawing.Color.Green
            Else
                lblSubmitted.ForeColor = System.Drawing.Color.Red
            End If

            ' ── FIX: ToolStripStatusLabel1 (left status bar message) ──
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

            ' ── Secondary guard on Increment button based on pending count ──
            '    (Primary guard is is_locked in LoadSemStatus — this is the grade check)
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

        ' ── Step 2: Confirm with user ──
        Dim confirm As DialogResult = MessageBox.Show( _
            "Are you sure you want to advance to the next semester?", _
            "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirm <> DialogResult.Yes Then Return

        ' ── Step 3: Perform increment inside a transaction ──
        Try
            Connect_me()

            Dim tx As OdbcTransaction = con.BeginTransaction()

            Try
                ' Read current values
                Dim cmdRead As New OdbcCommand( _
                    "SELECT school_year, semester FROM sem_control LIMIT 1", con, tx)
                Dim dr As OdbcDataReader = cmdRead.ExecuteReader()

                Dim curYear As String = ""
                Dim curSem As Integer = 0

                If dr.Read() Then
                    curYear = dr("school_year").ToString()
                    curSem = Convert.ToInt32(dr("semester"))
                End If
                dr.Close()

                ' Calculate new semester / school year
                Dim newYear As String = curYear
                Dim newSem As Integer = curSem

                If curSem = 1 Then
                    newSem = 2
                Else
                    newSem = 1
                    Dim parts() As String = curYear.Split("-"c)
                    Dim y1 As Integer = Convert.ToInt32(parts(0)) + 1
                    Dim y2 As Integer = Convert.ToInt32(parts(1)) + 1
                    newYear = y1.ToString() & "-" & y2.ToString()
                End If

                ' Update sem_control
                Dim cmdUpd As New OdbcCommand( _
                    "UPDATE sem_control SET school_year = ?, semester = ?, is_locked = 0", _
                    con, tx)
                cmdUpd.Parameters.AddWithValue("@sy", newYear)
                cmdUpd.Parameters.AddWithValue("@sem", newSem)
                cmdUpd.ExecuteNonQuery()

                ' Remove old submission rows for the new period (clean slate)
                Dim cmdDel As New OdbcCommand( _
                    "DELETE FROM grade_submission WHERE school_year = ? AND semester = ?", _
                    con, tx)
                cmdDel.Parameters.AddWithValue("@sy", newYear)
                cmdDel.Parameters.AddWithValue("@sem", newSem)
                cmdDel.ExecuteNonQuery()

                ' Re-seed submission rows for all current teachers
                Dim cmdIns As New OdbcCommand( _
                    "INSERT IGNORE INTO grade_submission " & _
                    "  (prof_id, school_year, semester, submitted) " & _
                    "SELECT prof_id, ?, ?, 0 " & _
                    "FROM prof " & _
                    "WHERE LOWER(role) = 'teacher'", _
                    con, tx)
                cmdIns.Parameters.AddWithValue("@sy", newYear)
                cmdIns.Parameters.AddWithValue("@sem", newSem)
                cmdIns.ExecuteNonQuery()

                tx.Commit()

                MessageBox.Show( _
                    "Semester advanced to S.Y. " & newYear & " - " & _
                    If(newSem = 1, "1st", "2nd") & " Semester.", _
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Refresh all labels, grid, counts, and status bar
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