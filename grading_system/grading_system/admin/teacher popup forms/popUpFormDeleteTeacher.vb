Imports System.Data.Odbc

Public Class popUpFormDeleteTeacher

    Private selectedAccId As Integer = -1
    Private selectedTeacherName As String = ""

    '--- Form Load ---
    Private Sub popUpFormDeleteTeacher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetupDataGridView()
        LoadTeacherGrid("")
    End Sub

    '--- Configure DataGridView: read-only, full-row select ---
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

    '--- Search: live filter as user types ---
    Private Sub Delete_Teacher_Search_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_Teacher_Search_TextBox.TextChanged
        ' Clear selection when search changes
        selectedAccId = -1
        selectedTeacherName = ""
        LoadTeacherGrid(Delete_Teacher_Search_TextBox.Text.Trim())
    End Sub

    '--- Load teachers into DataGridView ---
    Private Sub LoadTeacherGrid(ByVal searchName As String)
        Try
            Connect_me()
            Dim sql As String = "SELECT acc_id AS [ID], firstname AS [First Name], middlename AS [Middle Name], " & _
                                "lastname AS [Last Name], gender AS [Gender], email AS [Email] " & _
                                "FROM account " & _
                                "WHERE role = 'teacher' AND " & _
                                "(firstname LIKE ? OR lastname LIKE ? OR middlename LIKE ?) " & _
                                "ORDER BY lastname ASC"

            Dim da As New OdbcDataAdapter(sql, con)
            Dim pattern As String = "%" & searchName & "%"
            da.SelectCommand.Parameters.AddWithValue("@fn", pattern)
            da.SelectCommand.Parameters.AddWithValue("@ln", pattern)
            da.SelectCommand.Parameters.AddWithValue("@mn", pattern)

            Dim dt As New DataTable()
            da.Fill(dt)

            Delete_Teacher_DataGridView.DataSource = dt

            ' Hide acc_id (ID) column from user view
            If Delete_Teacher_DataGridView.Columns.Contains("ID") Then
                Delete_Teacher_DataGridView.Columns("ID").Visible = False
            End If

            Delete_Teacher_DataGridView.ClearSelection()

        Catch ex As Exception
            MessageBox.Show("Error loading teachers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--- FIRST VALIDATION: Click row in DataGridView ---
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

            Dim tempId As Integer = Convert.ToInt32(row.Cells("ID").Value)
            Dim tempFirst As String = row.Cells("First Name").Value.ToString().Trim()
            Dim tempMiddle As String = row.Cells("Middle Name").Value.ToString().Trim()
            Dim tempLast As String = row.Cells("Last Name").Value.ToString().Trim()
            Dim fullName As String = (tempFirst & " " & tempMiddle & " " & tempLast).Trim()

            ' --- FIRST VALIDATION DIALOG ---
            Dim firstConfirm As DialogResult = MessageBox.Show( _
                "You selected:" & Environment.NewLine & _
                "Name   : " & fullName & Environment.NewLine & _
                "Email  : " & row.Cells("Email").Value.ToString() & Environment.NewLine & _
                "Gender : " & row.Cells("Gender").Value.ToString() & Environment.NewLine & Environment.NewLine & _
                "Do you want to mark this teacher for deletion?" & Environment.NewLine & _
                "(You will confirm again before the record is permanently removed.)", _
                "Step 1 of 2 — Confirm Selection", _
                MessageBoxButtons.YesNo, _
                MessageBoxIcon.Question)

            If firstConfirm = DialogResult.Yes Then
                selectedAccId = tempId
                selectedTeacherName = fullName
                ' Highlight the selected row visually
                Delete_Teacher_DataGridView.Rows(rowIndex).Selected = True
                MessageBox.Show("Teacher """ & fullName & """ is now selected." & Environment.NewLine & _
                                "Click the DELETE button to permanently remove this record.", _
                                "Selection Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' User cancelled — clear selection
                selectedAccId = -1
                selectedTeacherName = ""
                Delete_Teacher_DataGridView.ClearSelection()
            End If

        Catch ex As Exception
            MessageBox.Show("Error selecting teacher: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--- SECOND VALIDATION: Delete Button ---
    Private Sub Delete_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete_Button.Click

        ' Guard: make sure a teacher was selected via the grid first
        If selectedAccId = -1 Then
            MessageBox.Show("No teacher selected." & Environment.NewLine & _
                            "Please click a teacher in the list first.", _
                            "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' --- SECOND VALIDATION DIALOG ---
        Dim secondConfirm As DialogResult = MessageBox.Show( _
            "⚠  WARNING: This action is PERMANENT and cannot be undone!" & Environment.NewLine & Environment.NewLine & _
            "You are about to permanently delete:" & Environment.NewLine & _
            "» " & selectedTeacherName & Environment.NewLine & Environment.NewLine & _
            "All related records (subjects, submissions, etc.) will also be removed by the database triggers." & Environment.NewLine & Environment.NewLine & _
            "Are you absolutely sure you want to delete this teacher?", _
            "Step 2 of 2 — Final Confirmation", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Exclamation)

        If secondConfirm = DialogResult.No Then
            MessageBox.Show("Deletion cancelled. No changes were made.", _
                            "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' --- Perform DELETE on account table ---
        ' Triggers handle cascading removal to prof/student/submission tables
        Try
            Connect_me()
            Dim sql As String = "DELETE FROM account WHERE acc_id = ? AND role = 'teacher'"
            Dim cmd As New OdbcCommand(sql, con)
            cmd.Parameters.AddWithValue("@id", selectedAccId)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show("Teacher """ & selectedTeacherName & """ has been successfully deleted.", _
                                "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
                selectedAccId = -1
                selectedTeacherName = ""
                Delete_Teacher_Search_TextBox.Text = ""
                LoadTeacherGrid("")
            Else
                MessageBox.Show("Deletion failed. The record may have already been removed.", _
                                "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting teacher: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--- Cancel Button ---
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        selectedAccId = -1
        selectedTeacherName = ""
        Me.Close()
    End Sub

End Class