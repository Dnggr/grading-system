Imports System.Data.Odbc
Public Class popUpFormAssignTeacherSubject

    Private Sub popUpFormAssignTeacherSubject_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub LoadSubjects()
        Try
            Dim query As String = "SELECT sub_id, sub_code +'-'+ sub_name AS subName FROM subject"
            Dim adapter As New OdbcDataAdapter(query, con)
            Dim dt As New DataTable
            Connect_me()
            adapter.Fill(dt)
            ComboBox1.DataSource = dt
            ComboBox1.DisplayMember = "subName"
            ComboBox1.ValueMember = "sub_id"
        Catch ex As Exception
            MessageBox.Show("Error loading subjects:" & ex.ToString, "Failed to load subjects", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim sectionSelect As New dialogSectionSelect
        sectionSelect.ShowDialog()

    End Sub
End Class