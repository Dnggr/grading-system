Imports System.Data.Odbc
Public Class Teacher_Form
    Dim response As DialogResult
    Dim acc_id As String = Login_Form.id
    Private Sub Teacher_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("1")
        ComboBox1.Items.Add("2")
        ComboBox1.Items.Add("3")
        ComboBox1.Items.Add("4")
        getListOfSubjects(acc_id)
        Button2.Text = acc_id
    End Sub
#Region "Database Getters"

    Private Sub getListOfSubjects(ByVal acc_id As String)
        Try
            Dim query As String = "SELECT Distinct profsectionsubject.id As ID," & _
                           "concat(subject.sub_code,'-',subject.sub_name) As Subject " & _
                           "From profsectionsubject " & _
                           "Left Join prof On profsectionsubject.prof_id = prof.prof_id " & _
                           "Left Join subject On profsectionsubject.sub_id = subject.sub_id " & _
                           "Left Join account On account.acc_id = prof.acc_id " & _
                           "Where prof.acc_id = ?"
            Using adapter As New OdbcDataAdapter(query, con)
                adapter.SelectCommand.Parameters.AddWithValue("?", acc_id)
                Dim dt As New DataTable
                adapter.Fill(dt)
                If dt.Rows.Count > 0 Then
                    DataGridView3.DataSource = dt
                    DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                Else
                    DataGridView3.DataSource = Nothing
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error:" & ex.ToString)
        End Try
    End Sub

    Private Sub getListOfStudents(ByVal section As String)
        Try
            Dim query As String = "SELECT Distinct student.stud_id As ID," & _
                           "concat(student.firstname,' ',student.middlename,' ',student.lastname) As FullName," & _
                           "student.course As Course, section.section As Section " & _
                           "From student " & _
                           "Left Join section On student.section_id = section.section_id " & _
                           "Left Join profsectionsubject On section.section_id = profsectionsubject.section_id " & _
                           "Left Join prof On profsectionsubject.prof_id = prof.prof_id " & _
                           "Left Join subject On profsectionsubject.sub_id = subject.sub_id " & _
                           "Where student.section_id = ?"
            Using adapter As New OdbcDataAdapter(query, con)
                adapter.SelectCommand.Parameters.AddWithValue("?", section)
                Dim dt As New DataTable
                adapter.Fill(dt)
                If dt.Rows.Count > 0 Then
                    DataGridView2.DataSource = dt
                    DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                Else
                    DataGridView2.DataSource = Nothing
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show("Error:" & ex.ToString)
        End Try
    End Sub

    Private Sub getAvailableSections(ByVal yearlevel As String)
        Try
            Dim query As String = "Select Distinct section.section_id As ID, section.section As SectionName, section.year_lvl As YearLevel From profsectionsubject " & _
                                    "Left Join prof On profsectionsubject.prof_id = prof.prof_id " & _
                                    "Left Join section On profsectionsubject.section_id = section.section_id " & _
                                    "Where prof.acc_id = ? AND section.year_lvl = ?"
            Using adapter As New OdbcDataAdapter(query, con)
                adapter.SelectCommand.Parameters.AddWithValue("?", acc_id)
                adapter.SelectCommand.Parameters.AddWithValue("?", yearlevel)
                Dim dt As New DataTable
                adapter.Fill(dt)
                If dt.Rows.Count > 0 Then
                    ComboBox2.Enabled = True
                    ComboBox2.DataSource = dt
                    ComboBox2.DisplayMember = "SectionName"
                    ComboBox2.ValueMember = "ID"
                    DataGridView1.DataSource = dt
                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                Else
                    ComboBox2.Enabled = False
                    ComboBox2.DataSource = Nothing
                    ComboBox2.DisplayMember = ""
                    ComboBox2.ValueMember = ""
                    DataGridView1.DataSource = Nothing
                    DataGridView2.DataSource = Nothing
                    MessageBox.Show("no assigned section for this Year level")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("error getting yearlevel" & ex.ToString)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
#End Region

#Region "CombiBox logic"
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim yearlevel As String = ComboBox1.SelectedItem.ToString()
        getAvailableSections(yearlevel)
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedValueChanged
        Dim section As String
        If ComboBox2.SelectedValue IsNot Nothing Then
            section = ComboBox2.SelectedValue.ToString()
            getListOfStudents(section)
            Button1.Text = section
        End If
    End Sub
#End Region
#Region "Button Logic"
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to Exit?", "log out confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
#End Region
    
End Class