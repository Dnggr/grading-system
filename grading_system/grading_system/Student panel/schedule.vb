Imports System.Data.Odbc
Public Class schedule

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles first_year.Click
        LinkLabel1.Visible = True
        LinkLabel2.Visible = True
    End Sub

    Private Sub schedule_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LinkLabel1.Visible = False
        LinkLabel2.Visible = False
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Connect_me()
            Dim cmd As New OdbcCommand("SELECT concat(prof.firstname, '-' , prof.lastname) As Profname, " & _
            "concat(subject.sub_code ,' ',subject.sub_name) As Subjects, " & _
            "section.section As Section " & _
            "FROM profsectionsubject " & _
            "LEFT JOIN prof ON profsectionsubject.prof_id = prof.prof_id " & _
            "LEFT JOIN subject ON profsectionsubject.sub_id = subject.sub_id " & _
            "LEFT JOIN section ON profsectionsubject.section_id = section.section_id " & _
            "WHERE section.section_id = ? AND subject.course_id = ? ", con)

            cmd.Parameters.AddWithValue("?", login_logic.secid)
            cmd.Parameters.AddWithValue("?", login_logic.Courseid)
            Dim da As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable

            da.Fill(dt)
            DataGridView1.DataSource = dt
            'edit header
            DataGridView1.Columns("Profname").HeaderText = "Professor"
            DataGridView1.Columns("Subjects").HeaderText = "Subject"
            DataGridView1.Columns("Section").HeaderText = "Section"

            'to control the dgv
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.AllowUserToAddRows = False
            DataGridView1.AllowUserToDeleteRows = False
            DataGridView1.AllowUserToResizeColumns = False
            DataGridView1.AllowUserToResizeRows = False
            DataGridView1.RowHeadersVisible = False
            DataGridView1.ReadOnly = True

            'to fill the gray space in dgv
            If DataGridView1.Rows.Count > 0 Then

                Dim totalHeight As Integer = DataGridView1.ClientSize.Height - DataGridView1.ColumnHeadersHeight
                Dim rowHeight As Integer = totalHeight \ DataGridView1.Rows.Count

                For Each row As DataGridViewRow In DataGridView1.Rows
                    row.Height = rowHeight
                Next

            End If


            'para mabago yung font ng dgv
            DataGridView1.DefaultCellStyle.Font = New Font("segoe ui", 8, FontStyle.Bold)

            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.MultiSelect = False
        Catch ex As Exception
            MessageBox.Show("error dgv" & ex.Message)
        End Try
    End Sub
End Class
