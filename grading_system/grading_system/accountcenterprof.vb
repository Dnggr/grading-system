Imports System.Data.Odbc
Public Class accountcenterprof

    Private Sub accountcenterprof_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub accountcenter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text.Trim()
        TextBox2.Text.Trim()
        TextBox3.Text.Trim()
        TextBox1.PasswordChar = ChrW(&H25CF)
        TextBox2.PasswordChar = ChrW(&H25CF)
        TextBox3.PasswordChar = ChrW(&H25CF)

        Try
            Connect_me()

            Dim cmdProfile As New OdbcCommand("SELECT p.*, a.acc_id, a.email AS acc_email " & _
                                  "FROM account a " & _
                                  "LEFT JOIN prof p ON a.acc_id = p.acc_id " & _
                                  "WHERE a.email = ?", con)
            cmdProfile.Parameters.AddWithValue("?", login_logic.loginuser)

            Dim reader As OdbcDataReader = cmdProfile.ExecuteReader()

            If reader.Read() Then
                email.Text = reader("acc_email").ToString()
            Else
                MessageBox.Show("No teacher record found.")
            End If

            reader.Close()
            con.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub refreshme()
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewPass.Click

    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles renewpass.Click

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub confirmation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles confirmation.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("PLEASE FILL THE TEXTBOX PO SABI NI CANDO", "FILL UP!!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Try
            Connect_me()

            Dim quert As String = "SELECT pword FROM account WHERE email = ?"
            Dim cmd As New OdbcCommand(quert, con)
            cmd.Parameters.AddWithValue("?", login_logic.loginuser)

            Dim reader As OdbcDataReader = cmd.ExecuteReader
            If reader.Read Then
                Dim oldpass As String = reader("pword")
                Dim hash As String = HashPassword(TextBox1.Text.Trim())
                TextBox2.Visible = True
                TextBox3.Visible = True
                confirmation.Visible = False
                TextBox1.ReadOnly = True
                CheckBox1.Visible = False
                TextBox1.Enabled = False
                CheckBox2.Visible = True
                CheckBox3.Visible = True

            Else
                MessageBox.Show("incorrect password", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox1.Text = ""
            End If
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("error" & ex.Message)
        End Try
    End Sub

    Private Sub ChangePass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangePass.Click

        Try
            If TextBox2.Text <> TextBox3.Text Then
                MessageBox.Show("PASSWORD DO NOT MATCH")
                refreshme()
                Return
            End If
            Connect_me()

            Dim hashedPassword As String = HashPassword(TextBox2.Text.Trim())

            Dim query As String = "UPDATE account SET pword = ? WHERE email = ?"
            Dim cmd As New OdbcCommand(query, con)

            cmd.Parameters.AddWithValue("?", hashedPassword)
            cmd.Parameters.AddWithValue("?", login_logic.loginuser)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show("PASSWORD HAS BEEN SUCCESSFULLY CHANGED!")
                TextBox1.Text = ""
                refreshme()
                Me.Hide()
            Else
                MessageBox.Show("Password not updated.")
            End If
        Catch ex As Exception
            MessageBox.Show("error" & ex.Message)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox1.PasswordChar = ChrW(0)
        Else
            TextBox1.PasswordChar = ChrW(&H25CF)
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            TextBox2.PasswordChar = ChrW(0)
        Else
            TextBox2.PasswordChar = ChrW(&H25CF)
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            TextBox3.PasswordChar = ChrW(0)
        Else
            TextBox3.PasswordChar = ChrW(&H25CF)
        End If
    End Sub
End Class