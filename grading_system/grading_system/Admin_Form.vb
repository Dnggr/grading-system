Imports System.Data.Odbc
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Public Class Admin_Form

    Private response As DialogResult

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim response As DialogResult
        response = MessageBox.Show("Are you sure you want to exit the program?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' If user clicks "Yes", exit the entire application
        If response = DialogResult.Yes Then
            Application.Exit() ' Ends the whole program
        End If
    End Sub
End Class