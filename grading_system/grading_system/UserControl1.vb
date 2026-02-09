Imports System.Data.Odbc
Public Class UserControl1

    Private Sub UserControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'lblAccount.Text = Login_Form.id
    End Sub
    Public Sub loadstudentinfo()
        Try
            Connect_me()
            Dim query As String = "Select * From student Where acc_id = ?"
            Dim adapter As New OdbcDataAdapter(query, con)
            adapter.SelectCommand.Parameters.AddWithValue("?", Login_Form.id)
            Dim ds As New DataSet()

            adapter.Fill(ds, "student")


            lblAccount.DataBindings.Clear()
            lblStudent.DataBindings.Clear()
            lblFirst.DataBindings.Clear()
            lblMiddle.DataBindings.Clear()
            lblLast.DataBindings.Clear()
            lblGender.DataBindings.Clear()
            lblEmail.DataBindings.Clear()
            lblCourse.DataBindings.Clear()
            lblYear.DataBindings.Clear()
            lblSection.DataBindings.Clear()

            lblAccount.DataBindings.Add("text", ds.Tables("student"), "acc_id")
            lblStudent.DataBindings.Add("text", ds.Tables("student"), "stud_id")
            lblFirst.DataBindings.Add("text", ds.Tables("student"), "firstname")
            lblMiddle.DataBindings.Add("text", ds.Tables("student"), "middlename")
            lblLast.DataBindings.Add("text", ds.Tables("student"), "lastname")
            lblGender.DataBindings.Add("text", ds.Tables("student"), "gender")
            lblEmail.DataBindings.Add("text", ds.Tables("student"), "email")
            lblCourse.DataBindings.Add("text", ds.Tables("student"), "course")
            lblYear.DataBindings.Add("text", ds.Tables("student"), "yr_lvl")
            lblSection.DataBindings.Add("text", ds.Tables("student"), "section")

            con.Close()
        Catch ex As Exception
            MessageBox.Show("error:" & ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


End Class
