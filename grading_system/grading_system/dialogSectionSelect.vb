Imports System.Windows.Forms
Imports System.Data.Odbc
Public Class dialogSectionSelect

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

   
    Private Sub dialogSectionSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim query As String = "SELECT section_id, concat(section ,' - ', year_lvl) AS sectionName FROM section"
            Dim adapter As New OdbcDataAdapter(query, con)
            Dim dt As New DataTable
            Connect_me()
            adapter.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error loading sections:" & ex.ToString, "Failed to load sections", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
