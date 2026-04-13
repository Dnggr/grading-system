Imports System.Data.Odbc

Public Class Dashboard
    Private Function ToProperCase(ByVal text As String) As String
        If text Is Nothing Then Return ""

        text = text.Trim()

        If text = "" Then Return ""

        text = text.ToLower()

        Return Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text)

    End Function

    Private Sub Dashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        subjects()
        finalavg()
        professor()
        sections()
        MakeRoundedPanel(Panel1, 30)
        MakeRoundedPanel(Panel2, 30)
        MakeRoundedPanel(Panel3, 30)
        MakeRoundedPanel(Panel4, 30)

        Try
            Dim fname As String = ToProperCase(login_logic.firstname)
            Dim mname As String = ToProperCase(login_logic.middlename)
            Dim lname As String = ToProperCase(login_logic.lastname)

            Dim formattedName As String = fname & " "

            If Not String.IsNullOrEmpty(mname) Then
                formattedName &= mname.Substring(0, 1).ToUpper() & ". "
            End If

            formattedName &= lname

            Label2.Text = formattedName

        Catch ex As Exception
            MessageBox.Show("Error loading student info: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MakeRoundedPanel(ByVal pnl As Panel, ByVal radius As Integer)
        Dim path As New Drawing.Drawing2D.GraphicsPath()

        path.StartFigure()
        path.AddArc(0, 0, radius, radius, 180, 90)
        path.AddArc(pnl.Width - radius, 0, radius, radius, 270, 90)
        path.AddArc(pnl.Width - radius, pnl.Height - radius, radius, radius, 0, 90)
        path.AddArc(0, pnl.Height - radius, radius, radius, 90, 90)
        path.CloseFigure()
        pnl.Region = New Region(path)
    End Sub   
    Private Sub Panel_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Panel1.Resize, Panel2.Resize, Panel3.Resize, Panel4.Resize
        MakeRoundedPanel(CType(sender, Panel), 20)
    End Sub


    Private Sub Panel1_Paint_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
        Panel1.Margin = New Padding(20)
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint
        Panel2.Margin = New Padding(20)
    End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint
        Panel3.Margin = New Padding(20)
    End Sub

    Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel4.Paint
        Panel4.Margin = New Padding(20)
    End Sub

#Region "logic dashboard"

    Private Sub finalavg()
        Try
            Connect_me()

            Dim query As String = "SELECT AVG(CAST(g.numerical AS DECIMAL(5,2))) " & _
                                  "FROM grades g " & _
                                  "INNER JOIN student s ON g.stud_id = s.stud_id " & _
                                  "WHERE s.acc_id = ? "
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("?", OdbcType.Int).Value = login_logic.userid
            Dim result As Object = cmd.ExecuteScalar()

            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Label8.Text = FormatNumber(Convert.ToDouble(result), 2)
            Else
                Label8.Text = "0.00"
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DataGridView1.SelectionChanged
        DataGridView1.ClearSelection()
    End Sub

    Private Sub DataGridView2_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DataGridView2.SelectionChanged
        DataGridView2.ClearSelection()
    End Sub
    Private Sub subjects()

        Try
            Connect_me()

            Dim cmd1 As New OdbcCommand("SELECT concat(subject.sub_code, ' ', subject.sub_name) AS Subjects " & _
                                         "FROM profsectionsubject " & _
                                         "LEFT JOIN subject ON profsectionsubject.sub_id = subject.sub_id " & _
                                         "LEFT JOIN section ON profsectionsubject.section_id = section.section_id " & _
                                         "LEFT JOIN sem_control sem ON sem.semester = sem.semester " & _
                                         "WHERE section.section_id = ? " & _
                                         "AND subject.course_id = ? " & _
                                         "AND sem.semester = ? ", con)
            cmd1.Parameters.AddWithValue("?", login_logic.secid)
            cmd1.Parameters.AddWithValue("?", login_logic.Courseid)
            cmd1.Parameters.AddWithValue("?", login_logic.currentsem)

            Dim da As New OdbcDataAdapter(cmd1)
            Dim dt As New DataTable

            da.Fill(dt)
            DataGridView1.DataSource = dt

            'edit header
            DataGridView1.Columns("Subjects").HeaderText = "Subject"

            'to control the dgv
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.AllowUserToAddRows = False
            DataGridView1.AllowUserToDeleteRows = False
            DataGridView1.AllowUserToResizeColumns = False
            DataGridView1.AllowUserToResizeRows = False
            DataGridView1.RowHeadersVisible = False
            DataGridView1.ReadOnly = True
            DataGridView1.Enabled = False

            'to fill the gray space in dgv
            If DataGridView1.Rows.Count > 0 Then

                Dim totalHeight As Integer = DataGridView1.ClientSize.Height - DataGridView1.ColumnHeadersHeight
                Dim rowHeight As Integer = totalHeight \ DataGridView1.Rows.Count

                For Each row As DataGridViewRow In DataGridView1.Rows
                    row.Height = rowHeight
                Next

            End If


            'para mabago yung font ng dgv
            DataGridView1.DefaultCellStyle.Font = New Font("segoe ui", 12, FontStyle.Regular)

            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.MultiSelect = False
        Catch ex As Exception
            MessageBox.Show("error dgv" & ex.Message)
        End Try
    End Sub
    Private Sub professor()
        Try
            Connect_me()

            Dim cmd2 As New OdbcCommand("SELECT CONCAT(prof.firstname, ' ', prof.lastname) AS profname " & _
                              "FROM profsectionsubject " & _
                              "LEFT JOIN prof ON profsectionsubject.prof_id = prof.prof_id " & _
                              "LEFT JOIN subject ON profsectionsubject.sub_id = subject.sub_id " & _
                              "LEFT JOIN section ON profsectionsubject.section_id = section.section_id " & _
                              "LEFT JOIN sem_control sem ON sem.semester = sem.semester " & _
                              "WHERE section.section_id = ? " & _
                              "AND subject.course_id = ? " & _
                              "AND sem.semester = ?", con)

            cmd2.Parameters.AddWithValue("?", login_logic.secid)
            cmd2.Parameters.AddWithValue("?", login_logic.Courseid)
            cmd2.Parameters.AddWithValue("?", login_logic.currentsem)

            Dim da1 As New OdbcDataAdapter(cmd2)
            Dim dt1 As New DataTable

            da1.Fill(dt1)
            DataGridView2.DataSource = dt1
            'edit header
            DataGridView2.Columns("profname").HeaderText = "Professor"

            'to control the dgv
            DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView2.AllowUserToAddRows = False
            DataGridView2.AllowUserToDeleteRows = False
            DataGridView2.AllowUserToResizeColumns = False
            DataGridView2.AllowUserToResizeRows = False
            DataGridView2.RowHeadersVisible = False
            DataGridView2.ReadOnly = True
            DataGridView2.Enabled = False

            'to fill the gray space in dgv
            If DataGridView2.Rows.Count > 0 Then

                Dim totalHeight As Integer = DataGridView2.ClientSize.Height - DataGridView2.ColumnHeadersHeight
                Dim rowHeight As Integer = totalHeight \ DataGridView2.Rows.Count

                For Each row As DataGridViewRow In DataGridView2.Rows
                    row.Height = rowHeight
                Next

            End If


            'para mabago yung font ng dgv
            DataGridView2.DefaultCellStyle.Font = New Font("segoe ui", 12, FontStyle.Regular)

            DataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView2.MultiSelect = False
        Catch ex As Exception
            MessageBox.Show("error dgv" & ex.Message)
        End Try
    End Sub

    Private Sub sections()
        Try
            Connect_me()
            Dim cmd3 As New OdbcCommand("SELECT * , section.section " & _
                                              "FROM student " & _
                                              "Left Join section On student.section_id = section.section_id " & _
                                              "WHERE email=?", con)
            cmd3.Parameters.AddWithValue("?", login_logic.loginuser)

            Dim reader As OdbcDataReader = cmd3.ExecuteReader
            If reader.Read Then
                section.Text = reader("section").ToString().ToUpper
            Else
                MessageBox.Show("No section found!!")
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub TableLayoutPanel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel2.Paint

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
