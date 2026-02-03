Imports System.Data.Odbc

Module Module_connection
    Public con As New OdbcConnection
    Private constr As String = "Driver={MySQL ODBC 8.0 Unicode Driver};Server=localhost;Database=gradingsystem_db;Uid=root;Pwd=;Option=3;"

    Public Sub Connect_me()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.ConnectionString = constr
            con.Open()
        Catch ex As Exception
            MessageBox.Show("Database connection failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Module