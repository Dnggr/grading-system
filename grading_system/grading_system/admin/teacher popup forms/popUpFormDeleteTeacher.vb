Imports System.Data.Odbc

Public Class popUpFormDeleteTeacher

    Private selectedAccId As Integer = -1
    Private selectedProfId As Integer = -1
    Private selectedTeacherName As String = ""

    ' ─────────────────────────────────────────────
    '  FORM LOAD
    ' ─────────────────────────────────────────────
    Private Sub popUpFormDeleteTeacher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetupDataGridView()
        LoadTeacherGrid("")
    End Sub

    ' ─────────────────────────────────────────────
    '  DATAGRIDVIEW SETUP
    ' ─────────────────────────────────────────────
    Private Sub SetupDataGridView()
        With Delete_Teacher_DataGridView
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AutoGenerateColumns = True
            .EditMode = DataGridViewEditMode.EditProgrammatically
        End With
    End Sub

    ' ─────────────────────────────────────────────
    '  SEARCH — live filter
    ' ─────────────────────────────────────────────
    Private Sub Delete_Teacher_Search_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_Teacher_Search_TextBox.TextChanged
        selectedAccId = -1
        selectedProfId = -1
        selectedTeacherName = ""
        LoadTeacherGrid(Delete_Teacher_Search_TextBox.Text.Trim())
    End Sub

    ' ─────────────────────────────────────────────
    '  LOAD GRID  — shows all teachers + their status
    ' ─────────────────────────────────────────────
    Private Sub LoadTeacherGrid(ByVal searchName As String)
        Try
            Connect_me()

            ' Join account → prof to get prof_id and current status
            Dim sql As String = _
                "SELECT a.acc_id, p.prof_id, " & _
                "CONCAT(a.firstname, ' ', COALESCE(a.middlename,''), ' ', a.lastname) AS fullname, " & _
                "a.gender, a.email, p.status " & _
                "FROM account a " & _
                "INNER JOIN prof p ON p.acc_id = a.acc_id " & _
                "WHERE a.role = 'teacher' " & _
                "AND (a.firstname LIKE ? OR a.lastname LIKE ? OR a.middlename LIKE ?) " & _
                "ORDER BY a.lastname ASC"

            Dim da As New OdbcDataAdapter(sql, con)
            Dim pattern As String = "%" & searchName & "%"
            da.SelectCommand.Parameters.AddWithValue("@fn", pattern)
            da.SelectCommand.Parameters.AddWithValue("@ln", pattern)
            da.SelectCommand.Parameters.AddWithValue("@mn", pattern)

            Dim dt As New DataTable()
            da.Fill(dt)

            ' Rename for display
            If dt.Columns.Contains("acc_id") Then dt.Columns("acc_id").ColumnName = "ACC_ID"
            If dt.Columns.Contains("prof_id") Then dt.Columns("prof_id").ColumnName = "PROF_ID"
            If dt.Columns.Contains("fullname") Then dt.Columns("fullname").ColumnName = "Full Name"
            If dt.Columns.Contains("gender") Then dt.Columns("gender").ColumnName = "Gender"
            If dt.Columns.Contains("email") Then dt.Columns("email").ColumnName = "Email"
            If dt.Columns.Contains("status") Then dt.Columns("status").ColumnName = "Status"

            Delete_Teacher_DataGridView.DataSource = dt

            ' Hide ID columns
            If Delete_Teacher_DataGridView.Columns.Contains("ACC_ID") Then
                Delete_Teacher_DataGridView.Columns("ACC_ID").Visible = False
            End If
            If Delete_Teacher_DataGridView.Columns.Contains("PROF_ID") Then
                Delete_Teacher_DataGridView.Columns("PROF_ID").Visible = False
            End If

            ' Column widths
            If Delete_Teacher_DataGridView.Columns.Contains("Full Name") Then
                Delete_Teacher_DataGridView.Columns("Full Name").Width = 220
            End If
            If Delete_Teacher_DataGridView.Columns.Contains("Gender") Then
                Delete_Teacher_DataGridView.Columns("Gender").Width = 80
            End If
            If Delete_Teacher_DataGridView.Columns.Contains("Email") Then
                Delete_Teacher_DataGridView.Columns("Email").Width = 200
            End If
            If Delete_Teacher_DataGridView.Columns.Contains("Status") Then
                Delete_Teacher_DataGridView.Columns("Status").Width = 90
            End If

            Delete_Teacher_DataGridView.ClearSelection()

        Catch ex As Exception
            MessageBox.Show("Error loading teachers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    '  ROW SELECTION
    ' ─────────────────────────────────────────────
    Private Sub Delete_Teacher_DataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Delete_Teacher_DataGridView.CellContentClick
        HandleRowSelection(e.RowIndex)
    End Sub

    Private Sub Delete_Teacher_DataGridView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Delete_Teacher_DataGridView.CellClick
        HandleRowSelection(e.RowIndex)
    End Sub

    Private Sub HandleRowSelection(ByVal rowIndex As Integer)
        If rowIndex < 0 Then Exit Sub

        Try
            Dim row As DataGridViewRow = Delete_Teacher_DataGridView.Rows(rowIndex)
            If row.Cells("ACC_ID").Value Is Nothing Then Exit Sub

            Dim tempAccId As Integer = Convert.ToInt32(row.Cells("ACC_ID").Value)
            Dim tempProfId As Integer = Convert.ToInt32(row.Cells("PROF_ID").Value)
            Dim fullName As String = row.Cells("Full Name").Value.ToString().Trim()
            Dim email As String = row.Cells("Email").Value.ToString().Trim()
            Dim gender As String = row.Cells("Gender").Value.ToString().Trim()
            Dim status As String = row.Cells("Status").Value.ToString().Trim().ToLower()

            Dim statusDisplay As String = If(status = "active", "ACTIVE ✔", "INACTIVE ✖")

            Dim firstConfirm As DialogResult = MessageBox.Show( _
                "You selected:" & Environment.NewLine & _
                "Name   : " & fullName & Environment.NewLine & _
                "Email  : " & email & Environment.NewLine & _
                "Gender : " & gender & Environment.NewLine & _
                "Status : " & statusDisplay & Environment.NewLine & Environment.NewLine & _
                "Do you want to select this teacher?", _
                "Step 1 of 2 — Confirm Selection", _
                MessageBoxButtons.YesNo, _
                MessageBoxIcon.Question)

            If firstConfirm = DialogResult.Yes Then
                selectedAccId = tempAccId
                selectedProfId = tempProfId
                selectedTeacherName = fullName
                Delete_Teacher_DataGridView.Rows(rowIndex).Selected = True

                Dim actionHint As String = If(status = "active", _
                    "Click SET INACTIVE to deactivate this teacher.", _
                    "Click SET ACTIVE to reactivate this teacher.")

                MessageBox.Show( _
                    """" & fullName & """ is now selected." & Environment.NewLine & actionHint, _
                    "Selection Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                selectedAccId = -1
                selectedProfId = -1
                selectedTeacherName = ""
                Delete_Teacher_DataGridView.ClearSelection()
            End If

        Catch ex As Exception
            MessageBox.Show("Error selecting teacher: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    '  SET INACTIVE BUTTON
    '  Validates: teacher must NOT be currently assigned to any active class
    ' ─────────────────────────────────────────────
    Private Sub Delete_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_Button.Click

        If selectedProfId = -1 Then
            MessageBox.Show("No teacher selected." & Environment.NewLine & _
                            "Please click a teacher in the list first.", _
                            "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' ── Check current status ──
        Dim currentStatus As String = GetProfStatus(selectedProfId)
        If currentStatus = "" Then Exit Sub ' error already shown

        If currentStatus.ToLower() = "inactive" Then
            MessageBox.Show( _
                """" & selectedTeacherName & """ is already INACTIVE." & Environment.NewLine & _
                "Use the SET ACTIVE button to reactivate.", _
                "Already Inactive", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' ── VALIDATION: check if teacher is currently assigned to any class ──
        If IsTeacherAssignedToClass(selectedProfId) Then
            MessageBox.Show( _
                "Cannot set """ & selectedTeacherName & """ as INACTIVE!" & Environment.NewLine & Environment.NewLine & _
                "This teacher is currently assigned to one or more classes." & Environment.NewLine & _
                "The teacher must complete the semester first before being deactivated." & Environment.NewLine & Environment.NewLine & _
                "Please remove all class assignments from the Assign Subject screen before proceeding.", _
                "Cannot Deactivate — Active Class Assignment Found", _
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' ── SECOND CONFIRMATION ──
        Dim secondConfirm As DialogResult = MessageBox.Show( _
            "You are about to set this teacher as INACTIVE:" & Environment.NewLine & _
            ">> " & selectedTeacherName & Environment.NewLine & Environment.NewLine & _
            "The teacher record will NOT be deleted from the database." & Environment.NewLine & _
            "They can still access past semester grades for incomplete/failed students." & Environment.NewLine & Environment.NewLine & _
            "Are you sure you want to deactivate this teacher?", _
            "Step 2 of 2 — Confirm Deactivation", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Exclamation)

        If secondConfirm = DialogResult.No Then
            MessageBox.Show("Deactivation cancelled. No changes were made.", _
                            "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' ── UPDATE STATUS TO INACTIVE ──
        SetProfStatus(selectedProfId, "inactive")
    End Sub

    ' ─────────────────────────────────────────────
    '  SET ACTIVE BUTTON  (reactivate a teacher)
    ' ─────────────────────────────────────────────
    Private Sub Activate_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Activate_Button.Click

        If selectedProfId = -1 Then
            MessageBox.Show("No teacher selected." & Environment.NewLine & _
                            "Please click a teacher in the list first.", _
                            "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim currentStatus As String = GetProfStatus(selectedProfId)
        If currentStatus = "" Then Exit Sub

        If currentStatus.ToLower() = "active" Then
            MessageBox.Show( _
                """" & selectedTeacherName & """ is already ACTIVE.", _
                "Already Active", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim confirm As DialogResult = MessageBox.Show( _
            "Reactivate teacher:" & Environment.NewLine & _
            ">> " & selectedTeacherName & Environment.NewLine & Environment.NewLine & _
            "This will allow the teacher to be assigned to classes again." & Environment.NewLine & _
            "Are you sure?", _
            "Confirm Reactivation", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question)

        If confirm = DialogResult.Yes Then
            SetProfStatus(selectedProfId, "active")
        End If
    End Sub

    ' ─────────────────────────────────────────────
    '  HELPER: Get current prof status from DB
    ' ─────────────────────────────────────────────
    Private Function GetProfStatus(ByVal profId As Integer) As String
        Try
            Connect_me()
            Dim sql As String = "SELECT status FROM prof WHERE prof_id = ?"
            Dim cmd As New OdbcCommand(sql, con)
            cmd.Parameters.AddWithValue("@pid", profId)
            Dim result As Object = cmd.ExecuteScalar()
            If result Is Nothing OrElse IsDBNull(result) Then
                Return "active"   ' default if column not yet populated
            End If
            Return result.ToString().Trim().ToLower()
        Catch ex As Exception
            MessageBox.Show("Error checking teacher status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Function

    ' ─────────────────────────────────────────────
    '  HELPER: Check if teacher has any active class assignments
    ' ─────────────────────────────────────────────
    Private Function IsTeacherAssignedToClass(ByVal profId As Integer) As Boolean
        Try
            Connect_me()
            Dim sql As String = "SELECT COUNT(*) FROM profsectionsubject WHERE prof_id = ?"
            Dim cmd As New OdbcCommand(sql, con)
            cmd.Parameters.AddWithValue("@pid", profId)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return (count > 0)
        Catch ex As Exception
            MessageBox.Show("Error checking assignments: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return True   ' fail safe: block deactivation on error
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Function

    ' ─────────────────────────────────────────────
    '  HELPER: Update prof status in DB
    ' ─────────────────────────────────────────────
    Private Sub SetProfStatus(ByVal profId As Integer, ByVal newStatus As String)
        Try
            Connect_me()
            Dim sql As String = "UPDATE prof SET status = ? WHERE prof_id = ?"
            Dim cmd As New OdbcCommand(sql, con)
            cmd.Parameters.AddWithValue("@status", newStatus)
            cmd.Parameters.AddWithValue("@pid", profId)
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                Dim verb As String = If(newStatus = "inactive", "deactivated", "reactivated")
                MessageBox.Show( _
                    """" & selectedTeacherName & """ has been successfully " & verb & ".", _
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                selectedAccId = -1
                selectedProfId = -1
                selectedTeacherName = ""
                Delete_Teacher_Search_TextBox.Text = ""
                LoadTeacherGrid("")
            Else
                MessageBox.Show("Update failed. The record may have been removed.", _
                                "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating teacher status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    '  REFRESH BUTTON
    ' ─────────────────────────────────────────────
    Private Sub Refresh_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Refresh_Button.Click
        Try
            selectedAccId = -1
            selectedProfId = -1
            selectedTeacherName = ""
            Delete_Teacher_Search_TextBox.Text = ""
            LoadTeacherGrid("")
            MessageBox.Show("Teacher list has been refreshed.", "Refreshed", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error refreshing list: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    '  CANCEL BUTTON
    ' ─────────────────────────────────────────────
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        selectedAccId = -1
        selectedProfId = -1
        selectedTeacherName = ""
        Me.Close()
    End Sub

End Class