Imports System.Data.Odbc

Module Module_SemControl

    ' Returns the active school_year and semester from sem_control.
    ' outYear  = e.g. "2024-2025"
    ' outSem   = 1 or 2
    Public Sub GetCurrentSem(ByRef outYear As String, ByRef outSem As Integer)
        outYear = ""
        outSem = 0
        Try
            Connect_me()
            Dim sql As String = "SELECT school_year, semester " & _
                                "FROM sem_control LIMIT 1"
            Dim cmd As New OdbcCommand(sql, con)
            Dim dr As OdbcDataReader = cmd.ExecuteReader()
            If dr.Read() Then
                outYear = dr("school_year").ToString()
                outSem = Convert.ToInt32(dr("semester"))
            End If
            dr.Close()
        Catch ex As Exception
            MessageBox.Show("GetCurrentSem error: " & ex.Message)
        End Try
    End Sub

    ' Returns True if ALL teachers have submitted for the current sem.
    Public Function AllTeachersSubmitted() As Boolean
        Try
            Connect_me()
            Dim sql As String = _
                "SELECT COUNT(*) " & _
                "FROM grade_submission gs " & _
                "JOIN sem_control sc " & _
                "  ON gs.school_year = sc.school_year " & _
                " AND gs.semester    = sc.semester " & _
                "WHERE gs.submitted  = 0"
            Dim cmd As New OdbcCommand(sql, con)
            Dim pending As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return (pending = 0)
        Catch ex As Exception
            MessageBox.Show("AllTeachersSubmitted error: " & ex.Message)
            Return False
        End Try
    End Function

End Module
