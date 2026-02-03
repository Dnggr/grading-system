Imports System.Data.Odbc
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Public Class Admin_Form

    Private response As DialogResult
    Private toolStripState As String = ""

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dim response As DialogResult
        response = MessageBox.Show("Are you sure you want to exit the program?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' If user clicks "Yes", exit the entire application
        If response = DialogResult.Yes Then
            Application.Exit() ' Ends the whole program
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        toolStripState = "section"
    End Sub

#Region "Loaders"
    Private Sub LoadSections()
        Try

            Dim query As String = "select * from "
            'Dim adapter As New OdbcDataAdapter(query, )
            Dim dt As New DataTable

            'adapter.Fill(dt)

            DataGridView1.DataSource = dt
            

        Catch ex As Exception
            MessageBox.Show("Error:" & ex.ToString, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
End Class