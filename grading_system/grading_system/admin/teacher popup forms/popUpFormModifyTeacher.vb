Imports System.Data.Odbc

Public Class popUpFormModifyTeacher

    Private selectedAccId As Integer = -1

    '--- Load form: populate gender combo ---
    Private Sub popUpFormModifyTeacher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Modify_Teacher_Gender_ComboBox.Items.Clear()
        Modify_Teacher_Gender_ComboBox.Items.Add("male")
        Modify_Teacher_Gender_ComboBox.Items.Add("female")
        Modify_Teacher_Gender_ComboBox.Items.Add("other")

        ' Setup DataGridView columns
        Modify_Teacher_DataGridView.AutoGenerateColumns = True
        LoadTeacherGrid("")
    End Sub

    '--- Search as you type ---
    Private Sub Modify_Teacher_Search_Name_Textbox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Teacher_Search_Name_Textbox.TextChanged
        LoadTeacherGrid(Modify_Teacher_Search_Name_Textbox.Text.Trim())
    End Sub

    '--- Load teachers into DataGridView ---
    Private Sub LoadTeacherGrid(ByVal searchName As String)
        Try
            Connect_me()
            Dim sql As String = "SELECT acc_id, firstname, middlename, lastname, gender, email " & _
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

            Modify_Teacher_DataGridView.DataSource = dt

            ' Hide acc_id column from user but keep it in datasource
            If Modify_Teacher_DataGridView.Columns.Contains("acc_id") Then
                Modify_Teacher_DataGridView.Columns("acc_id").Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading teachers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--- Row click: populate fields ---
    Private Sub Modify_Teacher_DataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Modify_Teacher_DataGridView.CellContentClick
        PopulateFieldsFromGrid(e.RowIndex)
    End Sub

    Private Sub Modify_Teacher_DataGridView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Modify_Teacher_DataGridView.CellClick
        PopulateFieldsFromGrid(e.RowIndex)
    End Sub

    Private Sub PopulateFieldsFromGrid(ByVal rowIndex As Integer)
        If rowIndex < 0 Then Exit Sub
        Try
            Dim row As DataGridViewRow = Modify_Teacher_DataGridView.Rows(rowIndex)

            selectedAccId = Convert.ToInt32(row.Cells("acc_id").Value)
            Modify_Teacher_Firstname_TextBox.Text = row.Cells("firstname").Value.ToString()
            Modify_Teacher_Middlename_TextBox.Text = row.Cells("middlename").Value.ToString()
            Modify_Teacher_Lastname_TextBox.Text = row.Cells("lastname").Value.ToString()
            Modify_Teacher_Email_TextBox.Text = row.Cells("email").Value.ToString()

            Dim gender As String = row.Cells("gender").Value.ToString().ToLower()
            Dim idx As Integer = Modify_Teacher_Gender_ComboBox.Items.IndexOf(gender)
            If idx >= 0 Then
                Modify_Teacher_Gender_ComboBox.SelectedIndex = idx
            End If
        Catch ex As Exception
            MessageBox.Show("Error selecting row: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--- TextChanged stubs (kept for handle binding, no logic needed) ---
    Private Sub Modify_Teacher_Lastname_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Teacher_Lastname_TextBox.TextChanged
    End Sub

    Private Sub Modify_Teacher_Firstname_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Teacher_Firstname_TextBox.TextChanged
    End Sub

    Private Sub Modify_Teacher_Middlename_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Teacher_Middlename_TextBox.TextChanged
    End Sub

    Private Sub Modify_Teacher_Gender_ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Teacher_Gender_ComboBox.SelectedIndexChanged
    End Sub

    Private Sub Modify_Teacher_Email_TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Teacher_Email_TextBox.TextChanged
    End Sub

    '--- Validation ---
    Private Function ValidateInputs() As Boolean
        ' Check empty fields
        If String.IsNullOrEmpty(Modify_Teacher_Firstname_TextBox.Text.Trim()) Then
            MessageBox.Show("First name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Teacher_Firstname_TextBox.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(Modify_Teacher_Lastname_TextBox.Text.Trim()) Then
            MessageBox.Show("Last name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Teacher_Lastname_TextBox.Focus()
            Return False
        End If

        ' Name: letters and spaces only
        Dim namePattern As New System.Text.RegularExpressions.Regex("^[a-zA-Z\s]+$")
        If Not namePattern.IsMatch(Modify_Teacher_Firstname_TextBox.Text.Trim()) Then
            MessageBox.Show("First name must contain letters only.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Teacher_Firstname_TextBox.Focus()
            Return False
        End If

        If Not namePattern.IsMatch(Modify_Teacher_Lastname_TextBox.Text.Trim()) Then
            MessageBox.Show("Last name must contain letters only.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Teacher_Lastname_TextBox.Focus()
            Return False
        End If

        If Not String.IsNullOrEmpty(Modify_Teacher_Middlename_TextBox.Text.Trim()) Then
            If Not namePattern.IsMatch(Modify_Teacher_Middlename_TextBox.Text.Trim()) Then
                MessageBox.Show("Middle name must contain letters only.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Modify_Teacher_Middlename_TextBox.Focus()
                Return False
            End If
        End If

        ' Gender
        If Modify_Teacher_Gender_ComboBox.SelectedIndex < 0 Then
            MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Teacher_Gender_ComboBox.Focus()
            Return False
        End If

        ' Email: must end with @gmail.com
        Dim email As String = Modify_Teacher_Email_TextBox.Text.Trim()
        If String.IsNullOrEmpty(email) Then
            MessageBox.Show("Email cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Teacher_Email_TextBox.Focus()
            Return False
        End If

        If Not email.ToLower().EndsWith("@gmail.com") Then
            MessageBox.Show("Email must be a valid Gmail address (e.g. name@gmail.com).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Teacher_Email_TextBox.Focus()
            Return False
        End If

        ' Basic email format check
        Dim emailPattern As New System.Text.RegularExpressions.Regex("^[a-zA-Z0-9._%+\-]+@gmail\.com$")
        If Not emailPattern.IsMatch(email) Then
            MessageBox.Show("Invalid Gmail format. Use only valid characters before @gmail.com.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Teacher_Email_TextBox.Focus()
            Return False
        End If

        ' Check no teacher selected
        If selectedAccId = -1 Then
            MessageBox.Show("Please select a teacher from the list first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    '--- Modify Button ---
    Private Sub Modify_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Button.Click
        If Not ValidateInputs() Then Exit Sub

        Try
            ' Check if email is already used by another account
            Connect_me()
            Dim checkSql As String = "SELECT COUNT(*) FROM account WHERE email = ? AND acc_id <> ? AND role = 'teacher'"
            Dim checkCmd As New OdbcCommand(checkSql, con)
            checkCmd.Parameters.AddWithValue("@email", Modify_Teacher_Email_TextBox.Text.Trim())
            checkCmd.Parameters.AddWithValue("@id", selectedAccId)
            Dim emailCount As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

            If emailCount > 0 Then
                MessageBox.Show("This email is already used by another teacher.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Modify_Teacher_Email_TextBox.Focus()
                Exit Sub
            End If

            ' Confirm update
            Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to update this teacher's information?", _
                                                           "Confirm Modify", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.No Then Exit Sub

            ' Perform UPDATE on account table
            ' (Trigger will handle syncing to prof table if applicable)
            Connect_me()
            Dim sql As String = "UPDATE account SET firstname = ?, middlename = ?, lastname = ?, gender = ?, email = ? " & _
                                "WHERE acc_id = ? AND role = 'teacher'"

            Dim cmd As New OdbcCommand(sql, con)
            cmd.Parameters.AddWithValue("@fn", Modify_Teacher_Firstname_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@mn", Modify_Teacher_Middlename_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@ln", Modify_Teacher_Lastname_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@gn", Modify_Teacher_Gender_ComboBox.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@em", Modify_Teacher_Email_TextBox.Text.Trim())
            cmd.Parameters.AddWithValue("@id", selectedAccId)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show("Teacher information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()
                LoadTeacherGrid(Modify_Teacher_Search_Name_Textbox.Text.Trim())
            Else
                MessageBox.Show("No changes were made. Please try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating teacher: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--- Cancel Button ---
    Private Sub Modify_Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Cancel_Button.Click
        ClearFields()
        Me.Close()
    End Sub

    '--- Helper: Clear all fields ---
    Private Sub ClearFields()
        selectedAccId = -1
        Modify_Teacher_Firstname_TextBox.Text = ""
        Modify_Teacher_Middlename_TextBox.Text = ""
        Modify_Teacher_Lastname_TextBox.Text = ""
        Modify_Teacher_Email_TextBox.Text = ""
        Modify_Teacher_Gender_ComboBox.SelectedIndex = -1
    End Sub

End Class