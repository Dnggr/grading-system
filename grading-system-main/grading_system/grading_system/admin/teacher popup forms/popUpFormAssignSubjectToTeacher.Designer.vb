<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class popUpFormAssignSubjectToTeacher
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
        Me.Assign_Teacher_Panel = New System.Windows.Forms.Panel
        Me.Assign_Button = New System.Windows.Forms.Button
        Me.Selected_Teacher_Label = New System.Windows.Forms.Label
        Me.Close_Button = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.AssignmentsDataGridView = New System.Windows.Forms.DataGridView
        Me.Search_Teacher_DataGridView = New System.Windows.Forms.DataGridView
        Me.Search_Teacher_TextBox = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Selected_Subject_Label = New System.Windows.Forms.Label
        Me.ClassDataGridView = New System.Windows.Forms.DataGridView
        Me.Label5 = New System.Windows.Forms.Label
        Me.ClassFilter_ComboBox = New System.Windows.Forms.ComboBox
        Me.SubjectsDataGridView = New System.Windows.Forms.DataGridView
        Me.SearchSubject_TextBox = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.CourseFilter_ComboBox = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Assign_Teacher_Panel.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.AssignmentsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Search_Teacher_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.ClassDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SubjectsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Assign_Teacher_Panel
        '
        Me.Assign_Teacher_Panel.Controls.Add(Me.Assign_Button)
        Me.Assign_Teacher_Panel.Controls.Add(Me.Selected_Teacher_Label)
        Me.Assign_Teacher_Panel.Controls.Add(Me.Close_Button)
        Me.Assign_Teacher_Panel.Controls.Add(Me.GroupBox2)
        Me.Assign_Teacher_Panel.Controls.Add(Me.Search_Teacher_DataGridView)
        Me.Assign_Teacher_Panel.Controls.Add(Me.Search_Teacher_TextBox)
        Me.Assign_Teacher_Panel.Controls.Add(Me.Label2)
        Me.Assign_Teacher_Panel.Controls.Add(Me.GroupBox1)
        Me.Assign_Teacher_Panel.Controls.Add(Me.Label1)
        Me.Assign_Teacher_Panel.Location = New System.Drawing.Point(-1, -2)
        Me.Assign_Teacher_Panel.Name = "Assign_Teacher_Panel"
        Me.Assign_Teacher_Panel.Size = New System.Drawing.Size(907, 653)
        Me.Assign_Teacher_Panel.TabIndex = 0
        '
        'Assign_Button
        '
        Me.Assign_Button.Location = New System.Drawing.Point(737, 613)
        Me.Assign_Button.Name = "Assign_Button"
        Me.Assign_Button.Size = New System.Drawing.Size(75, 23)
        Me.Assign_Button.TabIndex = 7
        Me.Assign_Button.Text = "Assign"
        Me.Assign_Button.UseVisualStyleBackColor = True
        '
        'Selected_Teacher_Label
        '
        Me.Selected_Teacher_Label.AutoSize = True
        Me.Selected_Teacher_Label.Location = New System.Drawing.Point(576, 45)
        Me.Selected_Teacher_Label.Name = "Selected_Teacher_Label"
        Me.Selected_Teacher_Label.Size = New System.Drawing.Size(89, 13)
        Me.Selected_Teacher_Label.TabIndex = 8
        Me.Selected_Teacher_Label.Text = "selected teacher:"
        '
        'Close_Button
        '
        Me.Close_Button.Location = New System.Drawing.Point(818, 613)
        Me.Close_Button.Name = "Close_Button"
        Me.Close_Button.Size = New System.Drawing.Size(75, 23)
        Me.Close_Button.TabIndex = 6
        Me.Close_Button.Text = "close"
        Me.Close_Button.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.AssignmentsDataGridView)
        Me.GroupBox2.Location = New System.Drawing.Point(27, 482)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(880, 125)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Available Subjects"
        '
        'AssignmentsDataGridView
        '
        Me.AssignmentsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AssignmentsDataGridView.Location = New System.Drawing.Point(9, 16)
        Me.AssignmentsDataGridView.Name = "AssignmentsDataGridView"
        Me.AssignmentsDataGridView.Size = New System.Drawing.Size(857, 103)
        Me.AssignmentsDataGridView.TabIndex = 8
        '
        'Search_Teacher_DataGridView
        '
        Me.Search_Teacher_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Search_Teacher_DataGridView.Location = New System.Drawing.Point(27, 64)
        Me.Search_Teacher_DataGridView.Name = "Search_Teacher_DataGridView"
        Me.Search_Teacher_DataGridView.Size = New System.Drawing.Size(866, 94)
        Me.Search_Teacher_DataGridView.TabIndex = 4
        '
        'Search_Teacher_TextBox
        '
        Me.Search_Teacher_TextBox.Location = New System.Drawing.Point(80, 38)
        Me.Search_Teacher_TextBox.Name = "Search_Teacher_TextBox"
        Me.Search_Teacher_TextBox.Size = New System.Drawing.Size(356, 20)
        Me.Search_Teacher_TextBox.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Teacher:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Selected_Subject_Label)
        Me.GroupBox1.Controls.Add(Me.ClassDataGridView)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.ClassFilter_ComboBox)
        Me.GroupBox1.Controls.Add(Me.SubjectsDataGridView)
        Me.GroupBox1.Controls.Add(Me.SearchSubject_TextBox)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.CourseFilter_ComboBox)
        Me.GroupBox1.Location = New System.Drawing.Point(27, 164)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(880, 312)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Available Subjects"
        '
        'Selected_Subject_Label
        '
        Me.Selected_Subject_Label.AutoSize = True
        Me.Selected_Subject_Label.Location = New System.Drawing.Point(571, 27)
        Me.Selected_Subject_Label.Name = "Selected_Subject_Label"
        Me.Selected_Subject_Label.Size = New System.Drawing.Size(87, 13)
        Me.Selected_Subject_Label.TabIndex = 7
        Me.Selected_Subject_Label.Text = "selected subject:"
        '
        'ClassDataGridView
        '
        Me.ClassDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ClassDataGridView.Location = New System.Drawing.Point(9, 184)
        Me.ClassDataGridView.Name = "ClassDataGridView"
        Me.ClassDataGridView.Size = New System.Drawing.Size(857, 119)
        Me.ClassDataGridView.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 165)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "class filter:"
        '
        'ClassFilter_ComboBox
        '
        Me.ClassFilter_ComboBox.FormattingEnabled = True
        Me.ClassFilter_ComboBox.Location = New System.Drawing.Point(76, 157)
        Me.ClassFilter_ComboBox.Name = "ClassFilter_ComboBox"
        Me.ClassFilter_ComboBox.Size = New System.Drawing.Size(121, 21)
        Me.ClassFilter_ComboBox.TabIndex = 8
        '
        'SubjectsDataGridView
        '
        Me.SubjectsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.SubjectsDataGridView.Location = New System.Drawing.Point(9, 46)
        Me.SubjectsDataGridView.Name = "SubjectsDataGridView"
        Me.SubjectsDataGridView.Size = New System.Drawing.Size(857, 105)
        Me.SubjectsDataGridView.TabIndex = 7
        '
        'SearchSubject_TextBox
        '
        Me.SearchSubject_TextBox.Location = New System.Drawing.Point(264, 20)
        Me.SearchSubject_TextBox.Name = "SearchSubject_TextBox"
        Me.SearchSubject_TextBox.Size = New System.Drawing.Size(285, 20)
        Me.SearchSubject_TextBox.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(208, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "search:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "course filter:"
        '
        'CourseFilter_ComboBox
        '
        Me.CourseFilter_ComboBox.FormattingEnabled = True
        Me.CourseFilter_ComboBox.Location = New System.Drawing.Point(76, 19)
        Me.CourseFilter_ComboBox.Name = "CourseFilter_ComboBox"
        Me.CourseFilter_ComboBox.Size = New System.Drawing.Size(121, 21)
        Me.CourseFilter_ComboBox.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(174, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Assign teacher to subject and class"
        '
        'popUpFormAssignSubjectToTeacher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(904, 646)
        Me.Controls.Add(Me.Assign_Teacher_Panel)
        Me.Name = "popUpFormAssignSubjectToTeacher"
        Me.Text = "popUpFormAssignSubjectToTeacher"
        Me.Assign_Teacher_Panel.ResumeLayout(False)
        Me.Assign_Teacher_Panel.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.AssignmentsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Search_Teacher_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.ClassDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SubjectsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Assign_Teacher_Panel As System.Windows.Forms.Panel
    Friend WithEvents Search_Teacher_DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Search_Teacher_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SubjectsDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents SearchSubject_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CourseFilter_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Close_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents AssignmentsDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ClassDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ClassFilter_ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Assign_Button As System.Windows.Forms.Button
    Friend WithEvents Selected_Teacher_Label As System.Windows.Forms.Label
    Friend WithEvents Selected_Subject_Label As System.Windows.Forms.Label
End Class
