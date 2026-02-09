Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.Odbc
Public Class Student_Form


    Private Sub Student_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserControl11.Visible = False
        CrystalReportViewer1.Visible = False
    End Sub

#Region "button logic"
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If UserControl11.Visible = False Then
            UserControl11.loadstudentinfo()
            UserControl11.Visible = True
        Else
            UserControl11.Visible = False
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Application.Exit()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If CrystalReportViewer1.Visible = False Then
            CrystalReportViewer1.Visible = True
            loadGrades()
        Else
            CrystalReportViewer1.Visible = False
        End If
    End Sub
#End Region

#Region "crystal report"
    Private Sub loadGrades()
        GetFields()
        Dim reportDoc As New GradesStructure()

        reportDoc.Load(Application.StartupPath & "\crystalreport\GradeStructure.rpt")
        'reportDoc.SetDatabaseLogon("root", "", "localhost", "school_db")
        CrystalReportViewer1.ReportSource = reportDoc

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
                                    " Left Join student On grades.stud_id = student.stud_id " & _
                                    " Left Join prof On grades.prof_id = prof.prof_id " & _
                                    " Left Join subject On grades.sub_id = subject.sub_id " & _
                                    " Where grades.stud_id = ? "
            '"Left Join section On grades.stud_id = student.stud_id" & _


            Connect_me()
            Using adapter As New OdbcDataAdapter(query, con)
                adapter.SelectCommand.Parameters.AddWithValue("?", Login_Form.id)
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




End Class