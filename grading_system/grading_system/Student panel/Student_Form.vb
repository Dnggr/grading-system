Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.Odbc

Public Class Student_Form

    Dim p As New profile()
    Private Sub Student_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        p.Dock = DockStyle.Fill
        mainpanel.Controls.Add(p)
        p.Visible = False

        ' ----- CRYSTAL REPORT VIEWER -----
        s.Dock = DockStyle.Fill
        mainpanel.Controls.Add(s)
        s.Visible = False

        Try
            'query para ma call out yung name kung sino naka login
            Label1.Text = login_logic.firstname & " " & login_logic.lastname

        Catch ex As Exception
            MessageBox.Show("Error loading student info: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "button logic"
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        p.profileinfo()
        p.BringToFront()
        p.Visible = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "log out confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            login_logic.ClearLogin() ' Clear the login data
            Me.Close()
            Login_Form.Show()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        loadGrades()
        s.BringToFront()
        s.Visible = True
    End Sub
#End Region

#Region "crystal report"
    Private Sub loadGrades()
        Dim reportDoc As ReportDocument = Nothing
        reportDoc = New ReportDocument()

        Dim path As String = Application.StartupPath & "\crystalreport\GradesStructure.rpt"
        reportDoc.Load(path)
        If Not System.IO.File.Exists(path) Then
            MessageBox.Show("Report file not found: " & path, "Error", _
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        'reportDoc.SetDatabaseLogon("root", "", "localhost", "school_db")
        Dim dt As DataTable = GetFields()
        reportDoc.SetDataSource(dt)
        s.ReportSource = reportDoc
        s.Refresh()

    End Sub
#End Region
#Region "database loaders"
    Private Function GetFields() As DataTable
        Dim dt As New DataTable("GradeInfo")

        dt.Columns.Add("Profname", GetType(String))
        dt.Columns.Add("Subjects", GetType(String))
        dt.Columns.Add("Grades", GetType(Decimal))
        dt.Columns.Add("Studentname", GetType(String))
        dt.Columns.Add("Studentid", GetType(String))
        dt.Columns.Add("Course", GetType(String))
        dt.Columns.Add("Section", GetType(String))

        Try
            Dim query As String = "SELECT concat(prof.firstname, '-' , prof.lastname) As Profname, " & _
                                  "concat(student.firstname,' ', student.middlename ,' ' , student.lastname) As Studentname," & _
                                  "student.section As Section, student.course As Course, student.stud_id As Studentid," & _
                                  "grades.grade As Grades, concat(subject.sub_code ,' ',subject.sub_name) As Subjects " & _
                                  "From student " & _
                                    " Left Join grades On student.stud_id = grades.stud_id " & _
                                    " Left Join prof On grades.prof_id = prof.prof_id " & _
                                    " Left Join subject On grades.sub_id = subject.sub_id " & _
                                    " Where student.acc_id = ? "
            Connect_me()
            Dim adapter As New OdbcDataAdapter(query, con)
            adapter.SelectCommand.Parameters.AddWithValue("?", Login_Form.id)
            adapter.Fill(dt)
        Catch ex As Exception
            MessageBox.Show("Error getting data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
        Return dt
    End Function

#End Region

    
End Class