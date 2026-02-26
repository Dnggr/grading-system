Imports System.Data.Odbc

Public Class popUpFormModifyTeacher

    Private selectedAccId As Integer = -1

    '--- Form Load ---
    Private Sub popUpFormModifyTeacher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Modify_Teacher_Gender_ComboBox.Items.Clear()
        Modify_Teacher_Gender_ComboBox.Items.Add("male")
        Modify_Teacher_Gender_ComboBox.Items.Add("female")
        Modify_Teacher_Gender_ComboBox.Items.Add("other")

        Modify_Teacher_DataGridView.AutoGenerateColumns = True
        LoadTeacherGrid("")
    End Sub

    '--- Search as you type ---
    Private Sub Modify_Teacher_Search_Name_Textbox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Teacher_Search_Name_Textbox.TextChanged
        selectedAccId = -1
        LoadTeacherGrid(Modify_Teacher_Search_Name_Textbox.Text.Trim())
    End Sub

    '--- Load teachers into DataGridView ---
    Private Sub LoadTeacherGrid(ByVal searchName As String)
        Try
            Connect_me()

            Dim sql As String = "SELECT acc_id, " & _
                                "CONCAT(firstname, ' ', COALESCE(middlename, ''), ' ', lastname) AS fullname, " & _
                                "firstname, middlename, lastname, gender, email " & _
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

            If dt.Columns.Contains("fullname") Then
                dt.Columns("fullname").ColumnName = "Full Name"
            End If

            Modify_Teacher_DataGridView.DataSource = dt

            Dim hiddenCols() As String = {"acc_id", "firstname", "middlename", "lastname"}
            For Each colName As String In hiddenCols
                If Modify_Teacher_DataGridView.Columns.Contains(colName) Then
                    Modify_Teacher_DataGridView.Columns(colName).Visible = False
                End If
            Next

            If Modify_Teacher_DataGridView.Columns.Contains("Full Name") Then
                Modify_Teacher_DataGridView.Columns("Full Name").DisplayIndex = 0
                Modify_Teacher_DataGridView.Columns("Full Name").Width = 220
            End If
            If Modify_Teacher_DataGridView.Columns.Contains("gender") Then
                Modify_Teacher_DataGridView.Columns("gender").Width = 80
            End If
            If Modify_Teacher_DataGridView.Columns.Contains("email") Then
                Modify_Teacher_DataGridView.Columns("email").Width = 200
            End If

            Modify_Teacher_DataGridView.ClearSelection()

        Catch ex As Exception
            MessageBox.Show("Error loading teachers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    '--- Row click: populate edit fields ---
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

            If row.Cells("acc_id").Value Is Nothing Then Exit Sub

            selectedAccId = Convert.ToInt32(row.Cells("acc_id").Value)

            Modify_Teacher_Firstname_TextBox.Text = If(row.Cells("firstname").Value Is Nothing, "", row.Cells("firstname").Value.ToString().Trim())
            Modify_Teacher_Middlename_TextBox.Text = If(row.Cells("middlename").Value Is Nothing, "", row.Cells("middlename").Value.ToString().Trim())
            Modify_Teacher_Lastname_TextBox.Text = If(row.Cells("lastname").Value Is Nothing, "", row.Cells("lastname").Value.ToString().Trim())
            Modify_Teacher_Email_TextBox.Text = If(row.Cells("email").Value Is Nothing, "", row.Cells("email").Value.ToString().Trim())

            Dim gender As String = If(row.Cells("gender").Value Is Nothing, "", row.Cells("gender").Value.ToString().ToLower().Trim())
            Dim idx As Integer = Modify_Teacher_Gender_ComboBox.Items.IndexOf(gender)
            If idx >= 0 Then
                Modify_Teacher_Gender_ComboBox.SelectedIndex = idx
            Else
                Modify_Teacher_Gender_ComboBox.SelectedIndex = -1
            End If

        Catch ex As Exception
            MessageBox.Show("Error selecting row: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--- TextChanged stubs ---
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

        If selectedAccId = -1 Then
            MessageBox.Show("Please select a teacher from the list first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

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

        If Modify_Teacher_Gender_ComboBox.SelectedIndex < 0 Then
            MessageBox.Show("Please select a gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Teacher_Gender_ComboBox.Focus()
            Return False
        End If

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

        Dim emailPattern As New System.Text.RegularExpressions.Regex("^[a-zA-Z0-9._%+\-]+@gmail\.com$")
        If Not emailPattern.IsMatch(email) Then
            MessageBox.Show("Invalid Gmail format. Use only valid characters before @gmail.com.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Modify_Teacher_Email_TextBox.Focus()
            Return False
        End If

        Return True
    End Function

    '--- Modify Button ---
    '   ROOT CAUSE FIX: The database only has AFTER INSERT triggers on `account`.
    '   There is NO AFTER UPDATE trigger, so updating `account` alone never syncs `prof`.
    '   Solution: Explicitly update BOTH tables in the same operation.
    Private Sub Modify_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Button.Click
        If Not ValidateInputs() Then Exit Sub

        Try
            Connect_me()

            ' --- Step 1: Check for duplicate email ---
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

            ' --- Step 2: Confirm with user ---
            Dim previewFullName As String = (Modify_Teacher_Firstname_TextBox.Text.Trim() & " " & _
                                             Modify_Teacher_Middlename_TextBox.Text.Trim() & " " & _
                                             Modify_Teacher_Lastname_TextBox.Text.Trim()).Trim()

            Dim confirm As DialogResult = MessageBox.Show( _
                "You are about to update the following teacher:" & Environment.NewLine & _
                "Full Name : " & previewFullName & Environment.NewLine & _
                "Email     : " & Modify_Teacher_Email_TextBox.Text.Trim() & Environment.NewLine & _
                "Gender    : " & Modify_Teacher_Gender_ComboBox.SelectedItem.ToString() & Environment.NewLine & Environment.NewLine & _
                "Proceed with update?", _
                "Confirm Modify", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If confirm = DialogResult.No Then Exit Sub

            ' --- Step 3: Update account table ---
            Dim sqlAccount As String = "UPDATE account SET firstname = ?, middlename = ?, lastname = ?, gender = ?, email = ? " & _
                                       "WHERE acc_id = ? AND role = 'teacher'"

            Dim cmdAccount As New OdbcCommand(sqlAccount, con)
            cmdAccount.Parameters.AddWithValue("@fn", Modify_Teacher_Firstname_TextBox.Text.Trim())
            cmdAccount.Parameters.AddWithValue("@mn", Modify_Teacher_Middlename_TextBox.Text.Trim())
            cmdAccount.Parameters.AddWithValue("@ln", Modify_Teacher_Lastname_TextBox.Text.Trim())
            cmdAccount.Parameters.AddWithValue("@gn", Modify_Teacher_Gender_ComboBox.SelectedItem.ToString())
            cmdAccount.Parameters.AddWithValue("@em", Modify_Teacher_Email_TextBox.Text.Trim())
            cmdAccount.Parameters.AddWithValue("@id", selectedAccId)

            Dim rowsAffected As Integer = cmdAccount.ExecuteNonQuery()

            ' --- Step 4: ALSO update prof table (no AFTER UPDATE trigger exists) ---
            '   prof is linked to account via acc_id (FK: fk_prof_id)
            Dim sqlProf As String = "UPDATE prof SET firstname = ?, middlename = ?, lastname = ?, gender = ?, email = ? " & _
                                    "WHERE acc_id = ?"

            Dim cmdProf As New OdbcCommand(sqlProf, con)
            cmdProf.Parameters.AddWithValue("@fn", Modify_Teacher_Firstname_TextBox.Text.Trim())
            cmdProf.Parameters.AddWithValue("@mn", Modify_Teacher_Middlename_TextBox.Text.Trim())
            cmdProf.Parameters.AddWithValue("@ln", Modify_Teacher_Lastname_TextBox.Text.Trim())
            cmdProf.Parameters.AddWithValue("@gn", Modify_Teacher_Gender_ComboBox.SelectedItem.ToString())
            cmdProf.Parameters.AddWithValue("@em", Modify_Teacher_Email_TextBox.Text.Trim())
            cmdProf.Parameters.AddWithValue("@id", selectedAccId)

            cmdProf.ExecuteNonQuery()

            ' --- Step 5: Feedback and refresh ---
            If rowsAffected > 0 Then
                MessageBox.Show("Teacher """ & previewFullName & """ updated successfully!", _
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()
                LoadTeacherGrid(Modify_Teacher_Search_Name_Textbox.Text.Trim())
            Else
                MessageBox.Show("No changes were made. Please try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating teacher: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    '--- Cancel Button ---
    Private Sub Modify_Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Modify_Cancel_Button.Click
        ClearFields()
        Me.Close()
    End Sub

    '--- Refresh Button ---
    Private Sub Refresh_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Refresh_Button.Click
        Try
            ClearFields()

            RemoveHandler Modify_Teacher_Search_Name_Textbox.TextChanged, _
                          AddressOf Modify_Teacher_Search_Name_Textbox_TextChanged
            Modify_Teacher_Search_Name_Textbox.Text = ""
            AddHandler Modify_Teacher_Search_Name_Textbox.TextChanged, _
                       AddressOf Modify_Teacher_Search_Name_Textbox_TextChanged

            LoadTeacherGrid("")

            MessageBox.Show("Teacher list has been refreshed.", _
                            "Refreshed", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error refreshing list: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--- Helper: Clear all fields and reset selection ---
    Private Sub ClearFields()
        selectedAccId = -1
        Modify_Teacher_Firstname_TextBox.Text = ""
        Modify_Teacher_Middlename_TextBox.Text = ""
        Modify_Teacher_Lastname_TextBox.Text = ""
        Modify_Teacher_Email_TextBox.Text = ""
        Modify_Teacher_Gender_ComboBox.SelectedIndex = -1
    End Sub

End Class