Module login_logic
    Public loginuser As String = ""
    Public userid As String = ""
    Public firstname As String = ""
    Public lastname As String = ""
    Public userrole As String = ""

    'for schedule
    Public sectionid As Integer = 0
    Public Courseid As Integer = 0
    Public yearlvl As Integer = 0

    Public Sub ClearLogin()
        loginuser = ""
        userid = ""
        firstname = ""
        lastname = ""
        userrole = ""
    End Sub
End Module
