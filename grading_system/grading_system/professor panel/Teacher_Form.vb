Imports System.Data.Odbc
Public Class Teacher_Form
    Dim acc_id As String = Login_Form.id
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
    Private Sub getAvailableSections(ByVal prof_id As String, ByVal year_level As String)

    End Sub

    Private Sub getAvailableYearLevel(ByVal acc_id As String)
        Try
            Dim query As String = "Select * From"
            Using adapter As New OdbcDataAdapter(query, con)
                adapter.SelectCommand.Parameters.AddWithValue("?", acc_id)
            End Using
        Catch ex As Exception

        End Try

    End Sub


    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub
End Class