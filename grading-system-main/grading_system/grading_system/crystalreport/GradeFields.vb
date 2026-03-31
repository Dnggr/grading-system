

Partial Class GradeFields
    Partial Class GradeInfoDataTable

        Private Sub GradeInfoDataTable_GradeInfoRowChanging(ByVal sender As System.Object, ByVal e As GradeInfoRowChangeEvent) Handles Me.GradeInfoRowChanging

        End Sub

        Private Sub GradeInfoDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.ProfnameColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
