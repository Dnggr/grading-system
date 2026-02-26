<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class popUpFormModifyTeacher
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
        Me.Modify_Cancel_Button = New System.Windows.Forms.Button
        Me.Modify_Button = New System.Windows.Forms.Button
        Me.Modify_Teacher_DataGridView = New System.Windows.Forms.DataGridView
        Me.Modify_Teacher_Search_Name_Textbox = New System.Windows.Forms.TextBox
        Me.Modify_Student_Label1 = New System.Windows.Forms.Label
        Me.Modify_Label6 = New System.Windows.Forms.Label
        Me.Modify_Teacher_Email_TextBox = New System.Windows.Forms.TextBox
        Me.Modify_Label3 = New System.Windows.Forms.Label
        Me.Modify_Label2 = New System.Windows.Forms.Label
        Me.Modify_Label1 = New System.Windows.Forms.Label
        Me.Modify_Teacher_Lastname_TextBox = New System.Windows.Forms.TextBox
        Me.Modify_Teacher_Middlename_TextBox = New System.Windows.Forms.TextBox
        Me.Modify_Teacher_Firstname_TextBox = New System.Windows.Forms.TextBox
        Me.Modify_Label4 = New System.Windows.Forms.Label
        Me.Modify_Teacher_Gender_ComboBox = New System.Windows.Forms.ComboBox
        Me.Refresh_Button = New System.Windows.Forms.Button
        CType(Me.Modify_Teacher_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Modify_Cancel_Button
        '
        Me.Modify_Cancel_Button.Location = New System.Drawing.Point(388, 348)
        Me.Modify_Cancel_Button.Name = "Modify_Cancel_Button"
        Me.Modify_Cancel_Button.Size = New System.Drawing.Size(75, 23)
        Me.Modify_Cancel_Button.TabIndex = 52
        Me.Modify_Cancel_Button.Text = "cancel"
        Me.Modify_Cancel_Button.UseVisualStyleBackColor = True
        '
        'Modify_Button
        '
        Me.Modify_Button.Location = New System.Drawing.Point(287, 348)
        Me.Modify_Button.Name = "Modify_Button"
        Me.Modify_Button.Size = New System.Drawing.Size(75, 23)
        Me.Modify_Button.TabIndex = 51
        Me.Modify_Button.Text = "Modify"
        Me.Modify_Button.UseVisualStyleBackColor = True
        '
        'Modify_Teacher_DataGridView
        '
        Me.Modify_Teacher_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Modify_Teacher_DataGridView.Location = New System.Drawing.Point(7, 55)
        Me.Modify_Teacher_DataGridView.Name = "Modify_Teacher_DataGridView"
        Me.Modify_Teacher_DataGridView.Size = New System.Drawing.Size(465, 117)
        Me.Modify_Teacher_DataGridView.TabIndex = 50
        '
        'Modify_Teacher_Search_Name_Textbox
        '
        Me.Modify_Teacher_Search_Name_Textbox.Location = New System.Drawing.Point(94, 25)
        Me.Modify_Teacher_Search_Name_Textbox.Name = "Modify_Teacher_Search_Name_Textbox"
        Me.Modify_Teacher_Search_Name_Textbox.Size = New System.Drawing.Size(290, 20)
        Me.Modify_Teacher_Search_Name_Textbox.TabIndex = 49
        '
        'Modify_Student_Label1
        '
        Me.Modify_Student_Label1.AutoSize = True
        Me.Modify_Student_Label1.Location = New System.Drawing.Point(28, 28)
        Me.Modify_Student_Label1.Name = "Modify_Student_Label1"
        Me.Modify_Student_Label1.Size = New System.Drawing.Size(38, 13)
        Me.Modify_Student_Label1.TabIndex = 48
        Me.Modify_Student_Label1.Text = "Name:"
        '
        'Modify_Label6
        '
        Me.Modify_Label6.AutoSize = True
        Me.Modify_Label6.Location = New System.Drawing.Point(91, 295)
        Me.Modify_Label6.Name = "Modify_Label6"
        Me.Modify_Label6.Size = New System.Drawing.Size(35, 13)
        Me.Modify_Label6.TabIndex = 47
        Me.Modify_Label6.Text = "Email:"
        '
        'Modify_Teacher_Email_TextBox
        '
        Me.Modify_Teacher_Email_TextBox.Location = New System.Drawing.Point(131, 292)
        Me.Modify_Teacher_Email_TextBox.Name = "Modify_Teacher_Email_TextBox"
        Me.Modify_Teacher_Email_TextBox.Size = New System.Drawing.Size(253, 20)
        Me.Modify_Teacher_Email_TextBox.TabIndex = 45
        '
        'Modify_Label3
        '
        Me.Modify_Label3.AutoSize = True
        Me.Modify_Label3.Location = New System.Drawing.Point(53, 242)
        Me.Modify_Label3.Name = "Modify_Label3"
        Me.Modify_Label3.Size = New System.Drawing.Size(72, 13)
        Me.Modify_Label3.TabIndex = 41
        Me.Modify_Label3.Text = "Middle Name:"
        '
        'Modify_Label2
        '
        Me.Modify_Label2.AutoSize = True
        Me.Modify_Label2.Location = New System.Drawing.Point(66, 216)
        Me.Modify_Label2.Name = "Modify_Label2"
        Me.Modify_Label2.Size = New System.Drawing.Size(60, 13)
        Me.Modify_Label2.TabIndex = 40
        Me.Modify_Label2.Text = "First Name:"
        '
        'Modify_Label1
        '
        Me.Modify_Label1.AutoSize = True
        Me.Modify_Label1.Location = New System.Drawing.Point(65, 190)
        Me.Modify_Label1.Name = "Modify_Label1"
        Me.Modify_Label1.Size = New System.Drawing.Size(61, 13)
        Me.Modify_Label1.TabIndex = 39
        Me.Modify_Label1.Text = "Last Name:"
        '
        'Modify_Teacher_Lastname_TextBox
        '
        Me.Modify_Teacher_Lastname_TextBox.Location = New System.Drawing.Point(131, 187)
        Me.Modify_Teacher_Lastname_TextBox.Name = "Modify_Teacher_Lastname_TextBox"
        Me.Modify_Teacher_Lastname_TextBox.Size = New System.Drawing.Size(253, 20)
        Me.Modify_Teacher_Lastname_TextBox.TabIndex = 36
        '
        'Modify_Teacher_Middlename_TextBox
        '
        Me.Modify_Teacher_Middlename_TextBox.Location = New System.Drawing.Point(131, 239)
        Me.Modify_Teacher_Middlename_TextBox.Name = "Modify_Teacher_Middlename_TextBox"
        Me.Modify_Teacher_Middlename_TextBox.Size = New System.Drawing.Size(253, 20)
        Me.Modify_Teacher_Middlename_TextBox.TabIndex = 38
        '
        'Modify_Teacher_Firstname_TextBox
        '
        Me.Modify_Teacher_Firstname_TextBox.Location = New System.Drawing.Point(131, 213)
        Me.Modify_Teacher_Firstname_TextBox.Name = "Modify_Teacher_Firstname_TextBox"
        Me.Modify_Teacher_Firstname_TextBox.Size = New System.Drawing.Size(253, 20)
        Me.Modify_Teacher_Firstname_TextBox.TabIndex = 37
        '
        'Modify_Label4
        '
        Me.Modify_Label4.AutoSize = True
        Me.Modify_Label4.Location = New System.Drawing.Point(80, 268)
        Me.Modify_Label4.Name = "Modify_Label4"
        Me.Modify_Label4.Size = New System.Drawing.Size(45, 13)
        Me.Modify_Label4.TabIndex = 43
        Me.Modify_Label4.Text = "Gender:"
        '
        'Modify_Teacher_Gender_ComboBox
        '
        Me.Modify_Teacher_Gender_ComboBox.FormattingEnabled = True
        Me.Modify_Teacher_Gender_ComboBox.Location = New System.Drawing.Point(131, 265)
        Me.Modify_Teacher_Gender_ComboBox.Name = "Modify_Teacher_Gender_ComboBox"
        Me.Modify_Teacher_Gender_ComboBox.Size = New System.Drawing.Size(253, 21)
        Me.Modify_Teacher_Gender_ComboBox.TabIndex = 42
        '
        'Refresh_Button
        '
        Me.Refresh_Button.Location = New System.Drawing.Point(397, 25)
        Me.Refresh_Button.Name = "Refresh_Button"
        Me.Refresh_Button.Size = New System.Drawing.Size(75, 23)
        Me.Refresh_Button.TabIndex = 53
        Me.Refresh_Button.Text = "refresh"
        Me.Refresh_Button.UseVisualStyleBackColor = True
        '
        'popUpFormModifyTeacher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(480, 392)
        Me.Controls.Add(Me.Refresh_Button)
        Me.Controls.Add(Me.Modify_Cancel_Button)
        Me.Controls.Add(Me.Modify_Button)
        Me.Controls.Add(Me.Modify_Teacher_DataGridView)
        Me.Controls.Add(Me.Modify_Teacher_Search_Name_Textbox)
        Me.Controls.Add(Me.Modify_Student_Label1)
        Me.Controls.Add(Me.Modify_Label6)
        Me.Controls.Add(Me.Modify_Teacher_Email_TextBox)
        Me.Controls.Add(Me.Modify_Label4)
        Me.Controls.Add(Me.Modify_Teacher_Gender_ComboBox)
        Me.Controls.Add(Me.Modify_Label3)
        Me.Controls.Add(Me.Modify_Label2)
        Me.Controls.Add(Me.Modify_Label1)
        Me.Controls.Add(Me.Modify_Teacher_Lastname_TextBox)
        Me.Controls.Add(Me.Modify_Teacher_Middlename_TextBox)
        Me.Controls.Add(Me.Modify_Teacher_Firstname_TextBox)
        Me.Name = "popUpFormModifyTeacher"
        Me.Text = "popUpFormModifyTeacher"
        CType(Me.Modify_Teacher_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Modify_Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Modify_Button As System.Windows.Forms.Button
    Friend WithEvents Modify_Teacher_DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Modify_Teacher_Search_Name_Textbox As System.Windows.Forms.TextBox
    Friend WithEvents Modify_Student_Label1 As System.Windows.Forms.Label
    Friend WithEvents Modify_Label6 As System.Windows.Forms.Label
    Friend WithEvents Modify_Teacher_Email_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Modify_Label3 As System.Windows.Forms.Label
    Friend WithEvents Modify_Label2 As System.Windows.Forms.Label
    Friend WithEvents Modify_Label1 As System.Windows.Forms.Label
    Friend WithEvents Modify_Teacher_Lastname_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Modify_Teacher_Middlename_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Modify_Teacher_Firstname_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Modify_Label4 As System.Windows.Forms.Label
    Friend WithEvents Modify_Teacher_Gender_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Refresh_Button As System.Windows.Forms.Button
End Class
