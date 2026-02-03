Imports System.Data.Odbc
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Public Class Admin_Form

    Private response As DialogResult
    Private toolStripState As String = ""

    Private Sub Admin_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CloseControls()
    End Sub

    


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        toolStripState = "section"
        OpenControls()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        toolStripState = "teacher"
        OpenControls()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        toolStripState = "teacher"
        OpenControls()
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
#Region "Create"
    Private Sub CreateSection()
        Dim AddSection As New popUpFormAddSection
        AddSection.ShowDialog()
    End Sub
    Private Sub CreateTeacher()
        Dim AddTeacher As New popUpFormAddTeacher
        AddTeacher.ShowDialog()
    End Sub
    Private Sub CreateStudent()
        Dim AddStudent As New popUpFormAddStudent
        AddStudent.ShowDialog()
    End Sub
#End Region
#Region "Close/Open"
    Private Sub CloseControls()
        ToolStrip.Visible = False
        DataGridView1.Visible = False
    End Sub
    Private Sub OpenControls()
        ToolStrip.Visible = True
        DataGridView1.Visible = True
    End Sub
    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dim response As DialogResult
        response = MessageBox.Show("Are you sure you want to exit the program?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        ' If user clicks "Yes", exit the entire application
        If response = DialogResult.Yes Then
            Application.Exit() ' Ends the whole program
        End If
    End Sub
    Private Sub tsbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnClose.Click
        CloseControls()
    End Sub
#End Region

    Private Sub tsbtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnNew.Click
        Select Case toolStripState.ToLower.Trim
            Case "section"
                CreateSection()
            Case "teacher"
                CreateTeacher()
            Case "student"
                CreateStudent()
            Case Else
                Exit Sub
        End Select
    End Sub

    
    

End Class