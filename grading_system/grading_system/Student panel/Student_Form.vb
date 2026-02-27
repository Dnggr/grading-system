Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.Odbc
Imports System.Globalization
Imports System.IO

Public Class Student_Form

    Dim p As New profile()
    Dim sched As New schedule()

    'Dim CSY As String = ""
    'Dim CSem As Integer = 0

    ' PROPER CASE FUNCTION (MANDATORY FORMAT)
    Private Function ToProperCase(ByVal text As String) As String
        If text Is Nothing Then Return ""

        text = text.Trim()

        If text = "" Then Return ""

        text = text.ToLower()

        Return Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text)
    End Function

    Private Sub Student_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadStudentImage()
        LoadSchoolYears()
        LoadSemesters()
        Panel2.Visible = False
        sched.Dock = DockStyle.Fill
        mainpanel.Controls.Add(sched)
        sched.Visible = False

        p.Dock = DockStyle.Fill
        mainpanel.Controls.Add(p)
        p.Visible = False

        ' ----- CRYSTAL REPORT VIEWER -----
        s.Dock = DockStyle.Fill
        mainpanel.Controls.Add(s)
        s.Visible = False

        Try
            ' ----- AUTO FORMAT NAME -----
            Dim fname As String = ToProperCase(login_logic.firstname)
            Dim mname As String = ToProperCase(login_logic.middlename)
            Dim lname As String = ToProperCase(login_logic.lastname)

            Dim formattedName As String = fname & " "

            If Not String.IsNullOrEmpty(mname) Then
                formattedName &= mname.Substring(0, 1).ToUpper() & ". "
            End If

            formattedName &= lname

            Label1.Text = formattedName

        Catch ex As Exception
            MessageBox.Show("Error loading student info: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Button Logic"

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Panel2.Visible = False
        p.profileinfo()
        p.BringToFront()
        s.Visible = False
        p.Visible = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        Panel2.Visible = False
        If result = DialogResult.Yes Then
            login_logic.ClearLogin()
            Me.Close()
            Login_Form.Show()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ReloadReport()
        Panel2.Visible = True
        s.BringToFront()
        p.Visible = False
        s.Visible = True
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Panel2.Visible = False
        sched.BringToFront()
        sched.Visible = True
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ReloadReport()
    End Sub

#End Region

#Region "ComboBox Loaders"
    Private Sub LoadSemesters()
        ComboBox2.Items.Clear()
        'cboSemester.Items.Add("All Semesters")
        ComboBox2.Items.Add("1st Semester")
        ComboBox2.Items.Add("2nd Semester")
        ComboBox2.SelectedIndex = 0  ' Select "1st semester" by default
    End Sub

    Private Sub LoadSchoolYears()

        Try
            Connect_me()

            Dim query As String = "SELECT DISTINCT grades.school_year " & _
                                 "FROM grades " & _
                                 "INNER JOIN student ON grades.stud_id = student.stud_id " & _
                                 "WHERE student.acc_id = ? " & _
                                 "ORDER BY grades.school_year DESC"

            Using adapter As New OdbcDataAdapter(query, con)
                adapter.SelectCommand.Parameters.AddWithValue("?", Login_Form.id)

                Dim dt As New DataTable()
                adapter.Fill(dt)

                If dt.Rows.Count > 0 Then
                    ' ✅ REMOVED: No "All School Years" placeholder
                    Dim schoolYears As New List(Of String)()

                    For Each row As DataRow In dt.Rows
                        schoolYears.Add(row("school_year").ToString())
                    Next

                    ComboBox1.DataSource = schoolYears
                    ComboBox1.SelectedIndex = 0  ' ✅ Select first school year (most recent)
                Else
                    MessageBox.Show("No grades found", "Info")
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.ToString)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub
#End Region

#Region "Crystal Report "

    Private Sub loadGrades(ByVal id As String, Optional ByVal schoolYear As String = "", Optional ByVal semester As Integer = 0)
        Dim reportDoc As ReportDocument = Nothing

        Try
            reportDoc = New ReportDocument()
            Dim path As String = Application.StartupPath & "\crystalreport\GradesStructure.rpt"

            If Not System.IO.File.Exists(path) Then
                MessageBox.Show("Report file not found: " & path, "Error", _
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            reportDoc.Load(path)

            ' Get filtered data
            Dim dt As DataTable = GetFields(id, schoolYear, semester)

            If dt.Rows.Count = 0 Then
                MessageBox.Show("No grades found for the selected criteria", "Info", _
                              MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            reportDoc.SetDataSource(dt)
            s.ReportSource = reportDoc
            s.Refresh()

        Catch ex As Exception
            MessageBox.Show("Error loading report: " & ex.ToString, "Error")
        Finally
            If con.State = ConnectionState.Open Then con.Close()
           
        End Try
    End Sub

    Private Sub ReloadReport()
        If ComboBox1.SelectedIndex < 0 Then Return
        If ComboBox1.SelectedIndex < 0 Then Return

        Dim schoolYear As String = ""
        Dim semester As Integer = 0
        Dim id As String = Login_Form.id

        ' Get school year
        If ComboBox1.SelectedIndex >= 0 Then  ' Not "All School Years"
            schoolYear = ComboBox1.SelectedValue.ToString()
        End If

        ' Get semester
        If ComboBox2.SelectedIndex >= 0 Then  ' Not "All Semesters"
            Select Case ComboBox2.SelectedIndex
                Case 0
                    semester = 1  ' 1st semester
                Case 1
                    semester = 2  ' 2nd semester
            End Select
        End If

        ' Load report with filters
        loadGrades(id, schoolYear, semester)
    End Sub

#End Region

#Region "Grade Data Loader"
    Private Function GetFields(ByVal id As String, Optional ByVal schoolYear As String = "", Optional ByVal semester As Integer = 0) As DataTable

        Dim dt As New DataTable("GradeInfo")

        dt.Columns.Add("Profname", GetType(String))
        dt.Columns.Add("Subjects", GetType(String))
        dt.Columns.Add("Grades", GetType(Decimal))
        dt.Columns.Add("Studentname", GetType(String))
        dt.Columns.Add("Studentid", GetType(String))
        dt.Columns.Add("Course", GetType(String))
        dt.Columns.Add("Section", GetType(String))
        dt.Columns.Add("SchoolYear", GetType(String))
        dt.Columns.Add("Semester", GetType(Integer))

        Try

            Dim query As String = "SELECT  																						  " & _
"CONCAT(UPPER(LEFT(prof.firstname,1)), LOWER(SUBSTRING(prof.firstname,2)), ' ',                   " & _
"       UPPER(LEFT(prof.lastname,1)), LOWER(SUBSTRING(prof.lastname,2))) AS Profname,             " & _
"CONCAT(UPPER(LEFT(student.firstname,1)), LOWER(SUBSTRING(student.firstname,2)), ' ',             " & _
"       IFNULL(CONCAT(UPPER(LEFT(student.middlename,1)),'. '),''),                                " & _
"       UPPER(LEFT(student.lastname,1)), LOWER(SUBSTRING(student.lastname,2))) AS Studentname,    " & _
"section.section AS Section,                                                                      " & _
"student.course AS Course,                                                                        " & _
"student.stud_id AS Studentid,                                                                    " & _
"grades.grade AS Grades,                                                                          " & _
"CONCAT(subject.sub_code, ' ', subject.sub_name) AS Subjects,                                     " & _
"grades.remark AS Remark,                                                                         " & _
"grades.school_year AS SchoolYear,                                                                " & _
"grades.semester AS Semester                                                                  " & _
"FROM student                                                                                     " & _
"LEFT JOIN section                                                                                " & _
"ON student.section_id = section.section_id                                                       " & _
"LEFT JOIN grades                                                                                 " & _
"ON student.stud_id = grades.stud_id                                                              " & _
"AND grades.school_year = ?                                                             " & _
"AND grades.semester = ?                                                                          " & _
"LEFT JOIN prof                                                                                   " & _
"ON grades.prof_id = prof.prof_id                                                                 " & _
"LEFT JOIN subject                                                                                " & _
"ON grades.sub_id = subject.sub_id                                                                " & _
"WHERE student.acc_id = ?;                                                                        "


            Dim adapter As New OdbcDataAdapter(query, con)

            adapter.SelectCommand.Parameters.AddWithValue("?", schoolYear)
            adapter.SelectCommand.Parameters.AddWithValue("?", semester)
            adapter.SelectCommand.Parameters.AddWithValue("?", id)

            Connect_me()
            adapter.Fill(dt)

            If dt.Rows.Count = 0 Then
    MessageBox.Show("You have no grades yet.", "No Grades",MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf IsDBNull(dt.Rows(0)("SchoolYear")) And IsDBNull(dt.Rows(0)("Semester")) Then
                MessageBox.Show("You have no grades yet for this Semester.", "No Grades", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error getting data: " & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try

        Return dt

    End Function

#End Region
#Region "load image logic"
    Private Sub LoadStudentImage()
        Try
            Connect_me()

            Dim query As String = "SELECT image_path, gender FROM student WHERE acc_id = ?"

            Using cmd As New OdbcCommand(query, con)
                cmd.Parameters.AddWithValue("?", Login_Form.id)

                Using reader As OdbcDataReader = cmd.ExecuteReader()
                    If reader.Read() Then

                        Dim fileName As String = ""

                        If Not IsDBNull(reader("image_path")) Then
                            fileName = reader("image_path").ToString().Trim()
                        End If

                        If String.IsNullOrEmpty(fileName) Then
                            ' No uploaded image → show default
                            ShowDefaultImage(reader("gender").ToString().Trim())
                        Else
                            ' Show uploaded image
                            ShowUploadedImage(fileName)
                        End If

                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading student image: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub ShowDefaultImage(ByVal gender As String)

        Dim defaultImageName As String

        If gender.ToLower() = "female" Then
            defaultImageName = "femalewojak.jpg"
        Else
            defaultImageName = "depressedwojak.jpg"
        End If

        Dim folderPath As String = Path.Combine(Application.StartupPath, "..\..\Resources")
        Dim imagePath As String = Path.Combine(folderPath, defaultImageName)

        If File.Exists(imagePath) Then
            PictureBoxstudent.Image = Image.FromFile(imagePath)
            PictureBoxstudent.SizeMode = PictureBoxSizeMode.Zoom
        Else
            PictureBoxstudent.Image = Nothing
        End If

    End Sub

    Private Sub ShowUploadedImage(ByVal fileName As String)
        Try
            Dim imagePath As String = Application.StartupPath & "\..\..\picture\" & fileName

            If File.Exists(imagePath) Then

                If PictureBoxstudent.Image IsNot Nothing Then
                    PictureBoxstudent.Image.Dispose()
                    PictureBoxstudent.Image = Nothing
                End If

                Using fs As New FileStream(imagePath, FileMode.Open, FileAccess.Read)
                    PictureBoxstudent.Image = Image.FromStream(fs)
                End Using

                PictureBoxstudent.SizeMode = PictureBoxSizeMode.Zoom

            Else
                PictureBoxstudent.Image = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show("Error showing uploaded image: " & ex.Message)
        End Try
    End Sub
#End Region

   
    
End Class