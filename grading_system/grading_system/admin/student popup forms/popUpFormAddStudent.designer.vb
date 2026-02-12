<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class popUpFormAddStudent
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
        Me.Student_Lastname_TextBox = New System.Windows.Forms.TextBox
        Me.Student_Firstname_TextBox = New System.Windows.Forms.TextBox
        Me.Student_Middlename_TextBox = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Student_Gender_ComboBox = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Student_Cancel_Button = New System.Windows.Forms.Button
        Me.Student_Register_Button = New System.Windows.Forms.Button
        Me.Student_Course_ComboBox = New System.Windows.Forms.ComboBox
        Me.Student_Email_TextBox = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Student_YrLvl_ComboBox = New System.Windows.Forms.ComboBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Student_Lastname_TextBox
        '
        Me.Student_Lastname_TextBox.Location = New System.Drawing.Point(152, 39)
        Me.Student_Lastname_TextBox.Name = "Student_Lastname_TextBox"
        Me.Student_Lastname_TextBox.Size = New System.Drawing.Size(253, 20)
        Me.Student_Lastname_TextBox.TabIndex = 0
        '
        'Student_Firstname_TextBox
        '
        Me.Student_Firstname_TextBox.Location = New System.Drawing.Point(152, 65)
        Me.Student_Firstname_TextBox.Name = "Student_Firstname_TextBox"
        Me.Student_Firstname_TextBox.Size = New System.Drawing.Size(253, 20)
        Me.Student_Firstname_TextBox.TabIndex = 1
        '
        'Student_Middlename_TextBox
        '
        Me.Student_Middlename_TextBox.Location = New System.Drawing.Point(152, 91)
        Me.Student_Middlename_TextBox.Name = "Student_Middlename_TextBox"
        Me.Student_Middlename_TextBox.Size = New System.Drawing.Size(253, 20)
        Me.Student_Middlename_TextBox.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Student_YrLvl_ComboBox)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Student_Email_TextBox)
        Me.GroupBox1.Controls.Add(Me.Student_Course_ComboBox)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Student_Gender_ComboBox)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Student_Lastname_TextBox)
        Me.GroupBox1.Controls.Add(Me.Student_Middlename_TextBox)
        Me.GroupBox1.Controls.Add(Me.Student_Firstname_TextBox)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 40)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(491, 260)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GroupBox1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(101, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Gender:"
        '
        'Student_Gender_ComboBox
        '
        Me.Student_Gender_ComboBox.FormattingEnabled = True
        Me.Student_Gender_ComboBox.Location = New System.Drawing.Point(152, 117)
        Me.Student_Gender_ComboBox.Name = "Student_Gender_ComboBox"
        Me.Student_Gender_ComboBox.Size = New System.Drawing.Size(253, 21)
        Me.Student_Gender_ComboBox.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(74, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Middle Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(87, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "First Name:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(86, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Last Name:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Student_Cancel_Button)
        Me.GroupBox2.Controls.Add(Me.Student_Register_Button)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 329)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(491, 53)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        '
        'Student_Cancel_Button
        '
        Me.Student_Cancel_Button.Location = New System.Drawing.Point(379, 19)
        Me.Student_Cancel_Button.Name = "Student_Cancel_Button"
        Me.Student_Cancel_Button.Size = New System.Drawing.Size(106, 28)
        Me.Student_Cancel_Button.TabIndex = 1
        Me.Student_Cancel_Button.Text = "Cancel"
        Me.Student_Cancel_Button.UseVisualStyleBackColor = True
        '
        'Student_Register_Button
        '
        Me.Student_Register_Button.Location = New System.Drawing.Point(267, 19)
        Me.Student_Register_Button.Name = "Student_Register_Button"
        Me.Student_Register_Button.Size = New System.Drawing.Size(106, 28)
        Me.Student_Register_Button.TabIndex = 0
        Me.Student_Register_Button.Text = "Register"
        Me.Student_Register_Button.UseVisualStyleBackColor = True
        '
        'Student_Course_ComboBox
        '
        Me.Student_Course_ComboBox.FormattingEnabled = True
        Me.Student_Course_ComboBox.Location = New System.Drawing.Point(152, 170)
        Me.Student_Course_ComboBox.Name = "Student_Course_ComboBox"
        Me.Student_Course_ComboBox.Size = New System.Drawing.Size(253, 21)
        Me.Student_Course_ComboBox.TabIndex = 10
        '
        'Student_Email_TextBox
        '
        Me.Student_Email_TextBox.Location = New System.Drawing.Point(152, 144)
        Me.Student_Email_TextBox.Name = "Student_Email_TextBox"
        Me.Student_Email_TextBox.Size = New System.Drawing.Size(253, 20)
        Me.Student_Email_TextBox.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(104, 173)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Course:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(112, 147)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Email:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(85, 200)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Year Level:"
        '
        'Student_YrLvl_ComboBox
        '
        Me.Student_YrLvl_ComboBox.FormattingEnabled = True
        Me.Student_YrLvl_ComboBox.Location = New System.Drawing.Point(152, 197)
        Me.Student_YrLvl_ComboBox.Name = "Student_YrLvl_ComboBox"
        Me.Student_YrLvl_ComboBox.Size = New System.Drawing.Size(253, 21)
        Me.Student_YrLvl_ComboBox.TabIndex = 14
        '
        'popUpFormAddStudent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(517, 394)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "popUpFormAddStudent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "popUpFormAddStudent"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Student_Lastname_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Student_Firstname_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Student_Middlename_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Student_Gender_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Student_Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Student_Register_Button As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Student_Email_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Student_Course_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Student_YrLvl_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
