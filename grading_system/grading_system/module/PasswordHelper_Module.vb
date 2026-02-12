Imports System.Security.Cryptography
Imports System.Text

''' <summary>
''' Helper module for MD5 password hashing
''' Use this module throughout your application for consistent password handling
''' </summary>
Module Module_PasswordHelper

    ''' <summary>
    ''' Generate MD5 hash from plain text password
    ''' </summary>
    ''' <param name="plainPassword">Plain text password</param>
    ''' <returns>MD5 hashed password in lowercase hexadecimal format</returns>
    Public Function HashPassword(ByVal plainPassword As String) As String
        Try
            ' Create MD5 hash provider
            Dim md5Hasher As MD5 = MD5.Create()

            ' Convert input string to byte array
            Dim data As Byte() = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(plainPassword))

            ' Create StringBuilder to collect bytes
            Dim sBuilder As New StringBuilder()

            ' Loop through each byte and format as hexadecimal
            Dim i As Integer
            For i = 0 To data.Length - 1
                sBuilder.Append(data(i).ToString("x2"))
            Next i

            ' Return hexadecimal string (32 characters)
            Return sBuilder.ToString()

        Catch ex As Exception
            ' Return empty string on error
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Verify if plain password matches hashed password
    ''' </summary>
    ''' <param name="plainPassword">Plain text password to verify</param>
    ''' <param name="hashedPassword">MD5 hashed password from database</param>
    ''' <returns>True if passwords match, False otherwise</returns>
    Public Function VerifyPassword(ByVal plainPassword As String, ByVal hashedPassword As String) As Boolean
        Try
            ' Hash the plain password
            Dim hashToCompare As String = HashPassword(plainPassword)

            ' Compare the two hashes (case-insensitive)
            Return String.Equals(hashToCompare, hashedPassword, StringComparison.OrdinalIgnoreCase)

        Catch ex As Exception
            Return False
        End Try
    End Function

End Module