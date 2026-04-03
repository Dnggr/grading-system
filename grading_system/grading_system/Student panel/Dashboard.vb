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
        subjects()
        finalavg()
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

    Private Sub finalavg()
        Try
            Connect_me()

            Dim query As String = "SELECT AVG(CAST(g.numerical AS DECIMAL(5,2))) " & _
                                  "FROM grades g " & _
                                  "INNER JOIN student s ON g.stud_id = s.stud_id " & _
                                  "WHERE s.acc_id = ? "
            Dim cmd As New OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("?", OdbcType.Int).Value = login_logic.userid
            Dim result As Object = cmd.ExecuteScalar()

            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Label8.Text = FormatNumber(Convert.ToDouble(result), 2)
            Else
                Label8.Text = "0.00"
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
   
    Private Sub Panel_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Panel1.Resize, Panel2.Resize, Panel3.Resize, Panel4.Resize
        MakeRoundedPanel(CType(sender, Panel), 20)
    End Sub


    Private Sub Panel1_Paint_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint
        Panel1.Margin = New Padding(20)
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint
        Panel2.Margin = New Padding(20)
    End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint
        Panel3.Margin = New Padding(20)
    End Sub

    Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel4.Paint
        Panel4.Margin = New Padding(20)
    End Sub

    Private Sub subjects()
      
    End Sub
End Class
