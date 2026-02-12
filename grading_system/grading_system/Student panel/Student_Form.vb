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
            Me.Hide()
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
        GetFields()
        Dim reportDoc As New GradesStructure()

        reportDoc.Load(Application.StartupPath & "\crystalreport\GradeStructure.rpt")
        s.ReportSource = reportDoc

    End Sub
#End Region

#Region "database loaders"
    Private Sub GetFields()
        Dim dt As New DataTable()

        dt.Columns.Add("Profname", GetType(String))
        dt.Columns.Add("Subjects", GetType(String))
        dt.Columns.Add("Grades", GetType(Decimal))
        dt.Columns.Add("Studentname", GetType(String))
        dt.Columns.Add("Studentid", GetType(String))
        dt.Columns.Add("Course", GetType(String))
        dt.Columns.Add("Section", GetType(String))

        Try
            Dim query As String = "SELECT concat(prof.fname, '-' , prof.lname) As Profname, " & _
                                  "concat(student.firstname,' ', student.middlename ,' ' , student.lastname) As Studentname," & _
                                  "student.section As Section, student.course As Course, student.stud_id As Studentid," & _
                                  "grades.grade As Grades, concat(subject.sub_code ,' ',subject.sub_name) As Subjects " & _
                                  "From grades " & _
                                  "Left Join student On grades.stud_id = student.stud_id " & _
                                  "Left Join prof On grades.prof_id = prof.prof_id " & _
                                  "Left Join subject On grades.sub_id = subject.sub_id " & _
                                  "Where grades.stud_id = ?"

            Connect_me()
            Using adapter As New OdbcDataAdapter(query, con)
                ' CORRECTED: Use login_logic.userid instead of Login_Form.id
                adapter.SelectCommand.Parameters.AddWithValue("?", login_logic.userid)
                adapter.Fill(dt)
            End Using

        Catch ex As Exception
            MessageBox.Show("Error getting data: " & ex.Message, "Error", _
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
#End Region

    Private Sub s_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles s.Load

    End Sub
End Class