Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.Odbc
Imports System.Globalization
Imports System.IO
Public Class Prof_panel
#Region "roundedbutton"
    Private Sub MakeRoundedButton(ByVal btn As Button, ByVal radius As Integer)
        Dim path As New Drawing.Drawing2D.GraphicsPath()

        path.StartFigure()
        path.AddArc(0, 0, radius, radius, 180, 90)
        path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90)
        path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90)
        path.AddArc(0, btn.Height - radius, radius, radius, 90, 90)
        path.CloseFigure()
        btn.Region = New Region(path)
    End Sub
#End Region
    Dim p As New profprofile
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Teacher_Form.Show()
        Me.Hide()

    End Sub
    Private Function ToProperCase(ByVal text As String) As String
        If text Is Nothing Then Return ""

        text = text.Trim()

        If text = "" Then Return ""

        text = text.ToLower()

        Return Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text)
    End Function
#Region "load image logic"
    Private Sub LoadprofImage()
        Try
            Connect_me()

            Dim query As String = "SELECT image_path, gender FROM prof WHERE acc_id = ?"

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
            PictureBoxprof.Image = Image.FromFile(imagePath)
            PictureBoxprof.SizeMode = PictureBoxSizeMode.Zoom
        Else
            PictureBoxprof.Image = Nothing
        End If

    End Sub

    Private Sub ShowUploadedImage(ByVal fileName As String)
        Try
            Dim imagePath As String = Application.StartupPath & "\..\..\picture\" & fileName

            If File.Exists(imagePath) Then

                If PictureBoxprof.Image IsNot Nothing Then
                    PictureBoxprof.Image.Dispose()
                    PictureBoxprof.Image = Nothing
                End If

                Using fs As New FileStream(imagePath, FileMode.Open, FileAccess.Read)
                    PictureBoxprof.Image = Image.FromStream(fs)
                End Using

                PictureBoxprof.SizeMode = PictureBoxSizeMode.Zoom

            Else
                PictureBoxprof.Image = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show("Error showing uploaded image: " & ex.Message)
        End Try
    End Sub
#End Region


    Private Sub Prof_panel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MakeRoundedButton(Button1, 25)
        MakeRoundedButton(resetpass, 25)
        MakeRoundedButton(Button2, 25)
        MakeRoundedButton(Button3, 25)

        LoadprofImage()

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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        Panel1.Visible = False
        If result = DialogResult.Yes Then
            login_logic.ClearLogin()
            Me.Close()
            Login_Form.Show()
        End If
    End Sub

    Private Sub PictureBoxprof_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub resetpass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles resetpass.Click

        Panel2.Controls.Clear()
        Panel2.Visible = True

        Dim dbp As New dashboardprof
        dbp.Dock = DockStyle.Fill

        Panel2.Controls.Add(dbp)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Panel2.Controls.Clear()
        Panel2.Visible = True
        p.Dock = DockStyle.Fill
        Panel2.Controls.Add(p)
    End Sub
End Class