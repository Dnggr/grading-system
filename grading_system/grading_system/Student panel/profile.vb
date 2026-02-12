Imports System.Data.Odbc
Public Class profile

    Private Sub profile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub
    Public Sub profileinfo()
        Try
            Connect_me()

            Dim cmdProfile As New OdbcCommand("SELECT * FROM student WHERE email=?", con)
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

End Class
