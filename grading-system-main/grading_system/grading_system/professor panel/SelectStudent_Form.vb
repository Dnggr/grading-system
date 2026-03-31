Imports System.Data.Odbc
Imports System.Windows.Forms

Public Class SelectStudent_Form
    Private studentTable As New DataTable
    Public Shadows ParentForm As AddGrade_Form

    Private Sub SelectStudent_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        getAvailableSections()
        If ComboBox1.SelectedValue IsNot Nothing Then
            getListOfStudents(ComboBox1.SelectedValue.ToString())
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub getAvailableSections()
        Try
            Dim query As String = "Select Distinct profsectionsubject.section_id As ID, section.section As SectionName, section.year_lvl As YearLevel From profsectionsubject " & _
                                    "Left Join prof On profsectionsubject.prof_id = prof.prof_id " & _
                                    "Left Join section On profsectionsubject.section_id = section.section_id " & _
                                    "Where prof.acc_id = ?"
            Using adapter As New OdbcDataAdapter(query, con)
                adapter.SelectCommand.Parameters.AddWithValue("?", Teacher_Form.acc_id)
                Dim dt As New DataTable
                adapter.Fill(dt)
                If dt.Rows.Count > 0 Then
                    ComboBox1.Enabled = True
                    ComboBox1.DataSource = dt
                    ComboBox1.DisplayMember = "SectionName"
                    ComboBox1.ValueMember = "ID"
                    ComboBox1.SelectedIndex = 0
                Else
                    ComboBox1.Enabled = False
                    ComboBox1.DataSource = Nothing
                    ComboBox1.DisplayMember = ""
                    ComboBox1.ValueMember = ""
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("error getting Sections" & ex.ToString)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub getListOfStudents(ByVal section As String)
        Try
            Dim query As String = "SELECT Distinct student.stud_id As ID," & _
                           "concat(student.lastname,',',student.firstname,' ',student.middlename) As FullName," & _
                           "student.course As Course, section.section As Section, student.section_id As SectionID " & _
                           "From student " & _
                           "Left Join section On student.section_id = section.section_id " & _
                           "Left Join profsectionsubject On section.section_id = profsectionsubject.section_id " & _
                           "Where student.section_id = ?"

            Using adapter As New OdbcDataAdapter(query, con)
                adapter.SelectCommand.Parameters.AddWithValue("?", section)

                studentTable.Clear()
                adapter.Fill(studentTable)

                If studentTable.Rows.Count > 0 Then
                    DataGridView1.DataSource = studentTable
                    DataGridView1.Columns("ID").Visible = False
                    DataGridView1.Columns("SectionID").Visible = False
                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                Else
                    DataGridView1.DataSource = Nothing
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Error:" & ex.ToString)
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedValue IsNot Nothing Then
            Dim section As String = ComboBox1.SelectedValue.ToString()
            getListOfStudents(section)
        Else
            Exit Sub
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If studentTable IsNot Nothing Then
            Dim dv As New DataView(studentTable)

            dv.RowFilter = String.Format("FullName LIKE '%{0}%'", TextBox1.Text.Trim.Replace("'", "''"))

            DataGridView1.DataSource = dv
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then Return

        ' Get the clicked row
        Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

        ' Update the parent form's variables
        If ParentForm IsNot Nothing Then
            If selectedRow.Cells("ID").Value IsNot Nothing Then
                ParentForm.sID = selectedRow.Cells("ID").Value.ToString()
            End If

            If selectedRow.Cells("FullName").Value IsNot Nothing Then
                ParentForm.FN = selectedRow.Cells("FullName").Value.ToString()
            End If

            If selectedRow.Cells("sectionID").Value IsNot Nothing Then
                ParentForm.secID = selectedRow.Cells("sectionID").Value.ToString()
            End If

            ' Update textboxes on parent form
            ParentForm.txtName.Text = ParentForm.FN
        End If

        ' Close this form
        Me.Close()
    End Sub
End Class
