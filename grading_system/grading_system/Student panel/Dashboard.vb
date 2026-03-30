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
    Private Sub LoadGPA()
        Try
            Connect_me()

            Dim query As String = "Select AVG(grade) FROM grades WHERE acc_id=?"
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("acc_id", login_logic.userid)
            Dim result = cmd.ExecuteScalar()

            If Not IsDBNull(result) Then
                Label8.Text = FormatNumber(result, 2)
            Else
                Label8.Text = "0.00"
            End If
            con.Close()

        Catch ex As Exception
            MessageBox.Show("Error loading GPA:" & ex.Message)
        End Try
    End Sub

End Class
