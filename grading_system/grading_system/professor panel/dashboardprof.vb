Imports System.Data.Odbc
Public Class dashboardprof



    Private Function ToProperCase(ByVal text As String) As String
        If text Is Nothing Then Return ""

        text = text.Trim()

        If text = "" Then Return ""

        text = text.ToLower()

        Return Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text)

    End Function
    Private Sub dashboardprof_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        schoolyear()
        sectiionyr()
        subject()
        courseass()
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
#Region "school year"
    Private Sub schoolyear()
        Try
            Connect_me()

            Dim cmd As New OdbcCommand("SELECT school_year, semester FROM sem_control ORDER BY school_year DESC LIMIT 1", con)
            Dim reader As OdbcDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Label7.Text = reader("school_year").ToString() & " " & "-" & " " & reader("semester").ToString()
            Else
                Label7.Text = "notfound"
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading school year:" & ex.Message)
        End Try
    End Sub
#End Region

#Region "sectionyrAssigned"
    Private Sub sectiionyr()
        Try
            Connect_me()

            Dim cmd1 As New OdbcCommand("SELECT section.year_lvl, section.section " & _
                                        "FROM profsectionsubject " & _
                                        "LEFT JOIN section ON profsectionsubject.section_id = section.section_id " & _
                                        "LEFT JOIN prof ON profsectionsubject.prof_id = prof.prof_id " & _
                                        "LEFT JOIN sem_control sem ON sem.semester = ? " & _
                                        "WHERE prof.prof_id = ? ", con)


            cmd1.Parameters.AddWithValue("?", login_logic.userid)
            cmd1.Parameters.AddWithValue("?", login_logic.currentsem)

            Dim da1 As New OdbcDataAdapter(cmd1)
            Dim dt1 As New DataTable

            da1.Fill(dt1)
            DataGridView3.DataSource = dt1
            'edit header
            DataGridView3.Columns("year_lvl").HeaderText = "Year Level"
            DataGridView3.Columns("section").HeaderText = "Section"

            'to control the dgv
            DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView3.AllowUserToAddRows = False
            DataGridView3.AllowUserToDeleteRows = False
            DataGridView3.AllowUserToResizeColumns = False
            DataGridView3.AllowUserToResizeRows = False
            DataGridView3.RowHeadersVisible = False
            DataGridView3.ReadOnly = True
            DataGridView3.Enabled = False

            'to fill the gray space in dgv
            If DataGridView3.Rows.Count > 0 Then

                Dim totalHeight As Integer = DataGridView3.ClientSize.Height - DataGridView3.ColumnHeadersHeight
                Dim rowHeight As Integer = totalHeight \ DataGridView3.Rows.Count

                For Each row As DataGridViewRow In DataGridView3.Rows
                    row.Height = rowHeight
                Next

            End If


            'para mabago yung font ng dgv
            DataGridView3.DefaultCellStyle.Font = New Font("segoe ui", 10, FontStyle.Bold)

            DataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView3.MultiSelect = False

        Catch ex As Exception
            MessageBox.Show("error:" & ex.Message)
        End Try
    End Sub

    Private Sub DataGridView3_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DataGridView3.SelectionChanged
        DataGridView3.ClearSelection()
    End Sub
#End Region

#Region "subject assigned"
    Private Sub subject()

        Try
            Connect_me()

            Dim cmd2 As New OdbcCommand("SELECT CONCAT(subject.sub_name) AS Subjects " & _
                  "FROM profsectionsubject " & _
                  "INNER JOIN prof ON profsectionsubject.prof_id = prof.prof_id " & _
                  "INNER JOIN subject ON profsectionsubject.sub_id = subject.sub_id " & _
                  "WHERE prof.acc_id = ?", con)

            cmd2.Parameters.AddWithValue("?", login_logic.userid)
            Dim da As New OdbcDataAdapter(cmd2)
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
            DataGridView1.DefaultCellStyle.Font = New Font("segoe ui", 15, FontStyle.Bold)

            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.MultiSelect = False
        Catch ex As Exception
            MessageBox.Show("error dgv" & ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DataGridView1.SelectionChanged
        DataGridView1.ClearSelection()
    End Sub

#End Region

#Region "course assigned"
    Private Sub courseass()

        Try
            Connect_me()

            Dim cmd3 As New OdbcCommand("SELECT CONCAT(course.course_name) AS course " & _
                  "FROM profsectionsubject " & _
                  "INNER JOIN prof ON profsectionsubject.prof_id = prof.prof_id " & _
                  "INNER JOIN section ON profsectionsubject.section_id = section.section_id " & _
                  "INNER JOIN course ON section.course_id = course.course_id " & _
                  "WHERE prof.acc_id = ?", con)

            cmd3.Parameters.AddWithValue("?", login_logic.userid)
            Dim da2 As New OdbcDataAdapter(cmd3)
            Dim dt2 As New DataTable

            da2.Fill(dt2)
            DataGridView2.DataSource = dt2

            'edit header
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

                Dim totalHeight2 As Integer = DataGridView2.ClientSize.Height - DataGridView2.ColumnHeadersHeight
                Dim rowHeight2 As Integer = totalHeight2 \ DataGridView2.Rows.Count

                For Each row As DataGridViewRow In DataGridView1.Rows
                    row.Height = rowHeight2
                Next

            End If


            'para mabago yung font ng dgv
            DataGridView2.DefaultCellStyle.Font = New Font("segoe ui", 12, FontStyle.Bold)

            DataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridView1.MultiSelect = False
        Catch ex As Exception
            MessageBox.Show("error dgv" & ex.Message)
        End Try
    End Sub

    Private Sub DataGridView2_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DataGridView2.SelectionChanged
        DataGridView2.ClearSelection()
    End Sub

#End Region


    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
        Panel1.Margin = New Padding(20)
    End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint
        Panel3.Margin = New Padding(20)
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint
        Panel2.Margin = New Padding(20)
    End Sub
    Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel4.Paint
        Panel4.Margin = New Padding(20)
    End Sub
    Private Sub Panel_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Panel1.Resize, Panel2.Resize, Panel3.Resize, Panel4.Resize
        MakeRoundedPanel(CType(sender, Panel), 20)
    End Sub

    Private Sub TableLayoutPanel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TableLayoutPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
