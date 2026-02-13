Imports System.Data.Odbc
Public Class schedule

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub schedule_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Connect_me()
            'dgv code
            Dim cmd As New OdbcCommand("SELECT * FROM subject ", con)
            Dim da As New OdbcDataAdapter(cmd)
            Dim dt As New DataTable()

            da.Fill(dt)
            DataGridView1.DataSource = dt

            ' UI adjustments para lumaki ang laman
            DataGridView1.DefaultCellStyle.Font = New Font("Segoe UI", 12)
            DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 12, FontStyle.Bold)
            DataGridView1.RowTemplate.Height = 40
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            con.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

End Class
