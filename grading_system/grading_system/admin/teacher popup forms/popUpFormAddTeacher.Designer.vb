<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class popUpFormAddTeacher
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Teacher_Middlename_TextBox = New System.Windows.Forms.TextBox
        Me.Teacher_Email_TextBox = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Teacher_Gender_ComboBox = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Teacher_Lastname_TextBox = New System.Windows.Forms.TextBox
        Me.Teacher_Firstname_TextBox = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.Register_Teacher_Button = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Teacher_Middlename_TextBox)
        Me.GroupBox1.Controls.Add(Me.Teacher_Email_TextBox)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Teacher_Gender_ComboBox)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Teacher_Lastname_TextBox)
        Me.GroupBox1.Controls.Add(Me.Teacher_Firstname_TextBox)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 49)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(405, 185)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Teacher Info"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Middle Name:"
        '
        'Teacher_Middlename_TextBox
        '
        Me.Teacher_Middlename_TextBox.Location = New System.Drawing.Point(89, 87)
        Me.Teacher_Middlename_TextBox.Name = "Teacher_Middlename_TextBox"
        Me.Teacher_Middlename_TextBox.Size = New System.Drawing.Size(253, 20)
        Me.Teacher_Middlename_TextBox.TabIndex = 12
        '
        'Teacher_Email_TextBox
        '
        Me.Teacher_Email_TextBox.Location = New System.Drawing.Point(89, 140)
        Me.Teacher_Email_TextBox.Name = "Teacher_Email_TextBox"
        Me.Teacher_Email_TextBox.Size = New System.Drawing.Size(253, 20)
        Me.Teacher_Email_TextBox.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(49, 143)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "email:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(38, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Gender:"
        '
        'Teacher_Gender_ComboBox
        '
        Me.Teacher_Gender_ComboBox.FormattingEnabled = True
        Me.Teacher_Gender_ComboBox.Location = New System.Drawing.Point(89, 113)
        Me.Teacher_Gender_ComboBox.Name = "Teacher_Gender_ComboBox"
        Me.Teacher_Gender_ComboBox.Size = New System.Drawing.Size(253, 21)
        Me.Teacher_Gender_ComboBox.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "First Name:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Last Name:"
        '
        'Teacher_Lastname_TextBox
        '
        Me.Teacher_Lastname_TextBox.Location = New System.Drawing.Point(89, 35)
        Me.Teacher_Lastname_TextBox.Name = "Teacher_Lastname_TextBox"
        Me.Teacher_Lastname_TextBox.Size = New System.Drawing.Size(253, 20)
        Me.Teacher_Lastname_TextBox.TabIndex = 0
        '
        'Teacher_Firstname_TextBox
        '
        Me.Teacher_Firstname_TextBox.Location = New System.Drawing.Point(89, 61)
        Me.Teacher_Firstname_TextBox.Name = "Teacher_Firstname_TextBox"
        Me.Teacher_Firstname_TextBox.Size = New System.Drawing.Size(253, 20)
        Me.Teacher_Firstname_TextBox.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Cancel_Button)
        Me.GroupBox2.Controls.Add(Me.Register_Teacher_Button)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 240)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(405, 51)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Location = New System.Drawing.Point(315, 19)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(80, 28)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        Me.Cancel_Button.UseVisualStyleBackColor = True
        '
        'Register_Teacher_Button
        '
        Me.Register_Teacher_Button.Location = New System.Drawing.Point(229, 19)
        Me.Register_Teacher_Button.Name = "Register_Teacher_Button"
        Me.Register_Teacher_Button.Size = New System.Drawing.Size(80, 28)
        Me.Register_Teacher_Button.TabIndex = 0
        Me.Register_Teacher_Button.Text = "Register"
        Me.Register_Teacher_Button.UseVisualStyleBackColor = True
        '
        'popUpFormAddTeacher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(431, 303)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "popUpFormAddTeacher"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "popUpFormAddTeacher"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Teacher_Gender_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Teacher_Lastname_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Teacher_Firstname_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Register_Teacher_Button As System.Windows.Forms.Button
    Friend WithEvents Teacher_Email_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Teacher_Middlename_TextBox As System.Windows.Forms.TextBox
End Class
