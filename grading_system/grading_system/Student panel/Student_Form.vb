Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.Odbc
Imports System.Globalization
Imports System.IO

Public Class Student_Form

    Dim p As New profile()
    Dim sched As New schedule()

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
            MessageBox.Show("Error loading student info: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Button Logic"

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        p.profileinfo()
        p.BringToFront()
        s.Visible = False
        p.Visible = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            login_logic.ClearLogin()
            Me.Close()
            Login_Form.Show()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        loadGrades()
        s.BringToFront()
        p.Visible = False
        s.Visible = True
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        sched.BringToFront()
        sched.Visible = True
    End Sub

#End Region

#Region "Crystal Report"

    Private Sub loadGrades()

        Dim reportDoc As New ReportDocument()
        Dim path As String = Application.StartupPath & "\crystalreport\GradesStructure.rpt"

        If Not System.IO.File.Exists(path) Then
            MessageBox.Show("Report file not found: " & path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        reportDoc.Load(path)

        Dim dt As DataTable = GetFields()
        reportDoc.SetDataSource(dt)

        s.ReportSource = reportDoc
        s.Refresh()

    End Sub

#End Region

#Region "Database Loaders"

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

            Dim query As String = "SELECT " & _
                "CONCAT(" & _
                "UPPER(LEFT(prof.firstname,1)), LOWER(SUBSTRING(prof.firstname,2)), '-', " & _
                "UPPER(LEFT(prof.lastname,1)), LOWER(SUBSTRING(prof.lastname,2))" & _
                ") As Profname, " & _
                "CONCAT(" & _
                "UPPER(LEFT(student.firstname,1)), LOWER(SUBSTRING(student.firstname,2)), ' ', " & _
                "IFNULL(CONCAT(UPPER(LEFT(student.middlename,1)),'. '),''), " & _
                "UPPER(LEFT(student.lastname,1)), LOWER(SUBSTRING(student.lastname,2))" & _
                ") As Studentname, " & _
                "section.section As Section, " & _
                "student.course As Course, " & _
                "student.stud_id As Studentid, " & _
                "grades.grade As Grades, " & _
                "CONCAT(subject.sub_code,' ',subject.sub_name) As Subjects " & _
                "FROM student " & _
                "LEFT JOIN grades ON student.stud_id = grades.stud_id " & _
                "LEFT JOIN prof ON grades.prof_id = prof.prof_id " & _
                "LEFT JOIN subject ON grades.sub_id = subject.sub_id " & _
                "LEFT JOIN section ON student.section_id = section.section_id " & _
                "WHERE student.acc_id = ?"

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