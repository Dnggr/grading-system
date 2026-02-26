Imports System.Data.Odbc
Imports System.IO
Public Class profile
    Dim response As DialogResult
    Private Sub profile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImage()
    End Sub
#Region "Load StudentInfo"


    Public Sub profileinfo()
        Try
            Connect_me()

            Dim cmdProfile As New OdbcCommand("SELECT * , section.section " & _
                                              "FROM student " & _
                                              "Left Join section On student.section_id = section.section_id " & _
                                              "WHERE email=?", con)
            cmdProfile.Parameters.AddWithValue("?", login_logic.loginuser)

            Dim reader As OdbcDataReader = cmdProfile.ExecuteReader()

            If reader.Read() Then
                id.Text = reader("stud_id").ToString()
                firstname.Text = reader("firstname").ToString()
                lastname.Text = reader("lastname").ToString()
                middlename.Text = reader("middlename").ToString()
                gender.Text = reader("gender").ToString()
                email.Text = reader("email").ToString()
                course.Text = reader("course").ToString()
                yr_lvl.Text = reader("yr_lvl").ToString()
                section.Text = reader("section").ToString()
            Else
                MessageBox.Show("No student record found.")
            End If

            reader.Close()
            con.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
#End Region
#Region "Add/Replace Image"

    ' ============================================
    ' UPLOAD IMAGE
    ' ============================================
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Open file dialog to select image
        OpenFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp"

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Try
                ' 1. Delete old image first (if exists)
                DeleteOldImage()

                ' 2. Copy new image to picture folder
                Dim sourceFile As String = OpenFileDialog1.FileName
                Dim fileName As String = Path.GetFileName(sourceFile)
                Dim destinationFile As String = GetImageDestinationPath(fileName)

                ' Create picture folder if it doesn't exist
                EnsurePictureFolderExists()

                ' Copy the file
                File.Copy(sourceFile, destinationFile, True)

                ' 3. Save filename to database
                SaveImageToDatabase(fileName)

                ' 4. Display the image
                DisplayImage(destinationFile)

                MessageBox.Show("Image uploaded successfully!", "Success")

            Catch ex As Exception
                MessageBox.Show("Error uploading image: " & ex.Message, "Error")
            End Try
        End If
    End Sub

    ' ============================================
    ' DELETE IMAGE
    ' ============================================
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        DeleteImage()
    End Sub

    Private Sub DeleteImage()
        Try
            ' 1. Get current image filename from database
            Dim currentFileName As String = GetCurrentImageFileName()

            If String.IsNullOrEmpty(currentFileName) Then
                MessageBox.Show("No image to delete. Using default profile photo.", "Default Photo")
                Return
            End If

            ' 2. Delete physical file
            DeletePhysicalFile(currentFileName)

            ' 3. Remove from database
            RemoveImageFromDatabase()

            ' 4. Load default image
            LoadImage()

            MessageBox.Show("Image deleted successfully!", "Success")

        Catch ex As Exception
            MessageBox.Show("Error deleting image: " & ex.Message, "Error")
        End Try
    End Sub

    ' ============================================
    ' LOAD IMAGE
    ' ============================================
    Private Sub LoadImage()
        Try
            Connect_me()

            ' Get image filename and gender from database
            Dim query As String = "SELECT image_path, gender FROM student WHERE acc_id = ?"
            Using cmd As New OdbcCommand(query, con)
                cmd.Parameters.AddWithValue("?", Login_Form.id)

                Using reader As OdbcDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim fileName As String = ""

                        ' Check if user has uploaded an image
                        If Not IsDBNull(reader("image_path")) Then
                            fileName = reader("image_path").ToString().Trim()
                        End If

                        If String.IsNullOrEmpty(fileName) Then
                            ' No image - show default based on gender
                            ShowDefaultImage(reader("gender").ToString().Trim())
                        Else
                            ' Has image - show it
                            ShowUploadedImage(fileName)
                        End If
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading image: " & ex.Message, "Error")
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    ' ============================================
    ' HELPER FUNCTIONS
    ' ============================================

    ' Get the picture folder path
    Private Function GetPictureFolderPath() As String
        Return Path.GetFullPath(Path.Combine(Application.StartupPath, "..\..\picture"))
    End Function

    ' Get the resources folder path (for default images)
    Private Function GetResourcesFolderPath() As String
        Return Path.GetFullPath(Path.Combine(Application.StartupPath, "..\..\Resources"))
    End Function

    ' Get full path for image destination
    Private Function GetImageDestinationPath(ByVal fileName As String) As String
        Return Path.Combine(GetPictureFolderPath(), fileName)
    End Function

    ' Ensure picture folder exists
    Private Sub EnsurePictureFolderExists()
        Dim folder As String = GetPictureFolderPath()
        If Not Directory.Exists(folder) Then
            Directory.CreateDirectory(folder)
        End If
    End Sub

    ' Show default image based on gender
    Private Sub ShowDefaultImage(ByVal gender As String)
        Try
            Dim defaultImageName As String
            If gender.ToLower() = "female" Then
                defaultImageName = "femalewojak.jpg"
            Else
                defaultImageName = "depressedwojak.jpg"
            End If

            Dim imagePath As String = Path.Combine(GetResourcesFolderPath(), defaultImageName)

            If File.Exists(imagePath) Then
                DisplayImage(imagePath)
            Else
                PictureBox1.Image = Nothing
                MessageBox.Show("Default image not found: " & imagePath, "Warning")
            End If

        Catch ex As Exception
            MessageBox.Show("Error showing default image: " & ex.Message, "Error")
        End Try
    End Sub

    ' Show uploaded image
    Private Sub ShowUploadedImage(ByVal fileName As String)
        Try
            Dim imagePath As String = GetImageDestinationPath(fileName)

            If File.Exists(imagePath) Then
                DisplayImage(imagePath)
            Else
                MessageBox.Show("Image not found: " & imagePath & "The file may have been moved or deleted.", "Error")
                PictureBox1.Image = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show("Error showing uploaded image: " & ex.Message, "Error")
        End Try
    End Sub

    ' Display image in PictureBox
    Private Sub DisplayImage(ByVal imagePath As String)
        Try
            ' Dispose old image first
            If PictureBox1.Image IsNot Nothing Then
                PictureBox1.Image.Dispose()
                PictureBox1.Image = Nothing
            End If

            ' Load new image using FileStream (releases file lock)
            Using fs As New FileStream(imagePath, FileMode.Open, FileAccess.Read)
                PictureBox1.Image = Image.FromStream(fs)
            End Using

            PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

        Catch ex As Exception
            MessageBox.Show("Error displaying image: " & ex.Message, "Error")
        End Try
    End Sub

    ' Get current image filename from database
    Private Function GetCurrentImageFileName() As String
        Try
            Connect_me()

            Dim query As String = "SELECT image_path FROM student WHERE acc_id = ?"
            Using cmd As New OdbcCommand(query, con)
                cmd.Parameters.AddWithValue("?", Login_Form.id)

                Dim result As Object = cmd.ExecuteScalar()

                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    Return result.ToString().Trim()
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show("Error getting image filename: " & ex.Message, "Error")
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try

        Return ""
    End Function

    ' Save image filename to database
    Private Sub SaveImageToDatabase(ByVal fileName As String)
        Try
            Connect_me()

            Dim query As String = "UPDATE student SET image_path = ? WHERE acc_id = ?"
            Using cmd As New OdbcCommand(query, con)
                cmd.Parameters.AddWithValue("?", fileName)
                cmd.Parameters.AddWithValue("?", Login_Form.id)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MessageBox.Show("Error removing from database: " & ex.Message, "Error")
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub RemoveImageFromDatabase()
        Try
            Connect_me()

            Dim query As String = "UPDATE student SET image_path = NULL WHERE acc_id = ?"
            Using cmd As New OdbcCommand(query, con)
                cmd.Parameters.AddWithValue("?", Login_Form.id)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MessageBox.Show("Error removing from database: " & ex.Message, "Error")
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub


    ' Delete physical image file
    Private Sub DeletePhysicalFile(ByVal fileName As String)
        Try
            Dim filePath As String = GetImageDestinationPath(fileName)

            If File.Exists(filePath) Then
                ' Dispose image first to release file lock
                If PictureBox1.Image IsNot Nothing Then
                    PictureBox1.Image.Dispose()
                    PictureBox1.Image = Nothing
                End If

                ' Delete the file
                File.Delete(filePath)
            End If

        Catch ex As Exception
            MessageBox.Show("Error deleting physical file: " & ex.Message, "Error")
        End Try
    End Sub

    ' Delete old image before uploading new one
    Private Sub DeleteOldImage()
        Try
            Dim currentFileName As String = GetCurrentImageFileName()

            If Not String.IsNullOrEmpty(currentFileName) Then
                DeletePhysicalFile(currentFileName)
            End If

        Catch ex As Exception
            ' Silently handle - not critical if old image can't be deleted
        End Try
    End Sub

#End Region

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub
End Class
