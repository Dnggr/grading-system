<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Admin_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Admin_Form))
        Me.Navigation_Panel = New System.Windows.Forms.Panel
        Me.Logout_Button = New System.Windows.Forms.Button
        Me.School_Year_Button = New System.Windows.Forms.Button
        Me.Teacher_Button = New System.Windows.Forms.Button
        Me.Exit_Button = New System.Windows.Forms.Button
        Me.Student_Button = New System.Windows.Forms.Button
        Me.Dashboard_Button = New System.Windows.Forms.Button
        Me.Dashboard_Panel = New System.Windows.Forms.Panel
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton
        Me.Add_Student_Button = New System.Windows.Forms.Button
        Me.Modify_Student_Button = New System.Windows.Forms.Button
        Me.Delete_Student_Button = New System.Windows.Forms.Button
        Me.Student_List_DataGridView = New System.Windows.Forms.DataGridView
        Me.Student_Label = New System.Windows.Forms.Label
        Me.Search_Student_Label = New System.Windows.Forms.Label
        Me.Search_Student_TextBox = New System.Windows.Forms.TextBox
        Me.Teacher_Panel = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Add_Teacher_Button = New System.Windows.Forms.Button
        Me.Teacher_List_DataGridView = New System.Windows.Forms.DataGridView
        Me.Search_Teacher_TextBox = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Modify_Teacher_Button = New System.Windows.Forms.Button
        Me.Assign_Class_To_Teacher_Button = New System.Windows.Forms.Button
        Me.Delete_Teacher_Button = New System.Windows.Forms.Button
        Me.Back_Button = New System.Windows.Forms.Button
        Me.Refresh_teacher_Button = New System.Windows.Forms.Button
        Me.Refresh_student_Button = New System.Windows.Forms.Button
        Me.Student_Panel = New System.Windows.Forms.Panel
        Me.Navigation_Panel.SuspendLayout()
        CType(Me.Student_List_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Teacher_Panel.SuspendLayout()
        CType(Me.Teacher_List_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Student_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Navigation_Panel
        '
        Me.Navigation_Panel.BackColor = System.Drawing.Color.PowderBlue
        Me.Navigation_Panel.Controls.Add(Me.Logout_Button)
        Me.Navigation_Panel.Controls.Add(Me.School_Year_Button)
        Me.Navigation_Panel.Controls.Add(Me.Teacher_Button)
        Me.Navigation_Panel.Controls.Add(Me.Exit_Button)
        Me.Navigation_Panel.Controls.Add(Me.Student_Button)
        Me.Navigation_Panel.Controls.Add(Me.Dashboard_Button)
        Me.Navigation_Panel.Dock = System.Windows.Forms.DockStyle.Left
        Me.Navigation_Panel.Location = New System.Drawing.Point(0, 0)
        Me.Navigation_Panel.Name = "Navigation_Panel"
        Me.Navigation_Panel.Size = New System.Drawing.Size(197, 700)
        Me.Navigation_Panel.TabIndex = 0
        '
        'Logout_Button
        '
        Me.Logout_Button.BackColor = System.Drawing.Color.Transparent
        Me.Logout_Button.FlatAppearance.BorderSize = 0
        Me.Logout_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSlateGray
        Me.Logout_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen
        Me.Logout_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Logout_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Logout_Button.Image = CType(resources.GetObject("Logout_Button.Image"), System.Drawing.Image)
        Me.Logout_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Logout_Button.Location = New System.Drawing.Point(24, 595)
        Me.Logout_Button.Name = "Logout_Button"
        Me.Logout_Button.Size = New System.Drawing.Size(147, 53)
        Me.Logout_Button.TabIndex = 5
        Me.Logout_Button.Text = "Logout"
        Me.Logout_Button.UseVisualStyleBackColor = False
        '
        'School_Year_Button
        '
        Me.School_Year_Button.BackColor = System.Drawing.Color.Transparent
        Me.School_Year_Button.FlatAppearance.BorderSize = 0
        Me.School_Year_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSlateGray
        Me.School_Year_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen
        Me.School_Year_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.School_Year_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.School_Year_Button.Image = CType(resources.GetObject("School_Year_Button.Image"), System.Drawing.Image)
        Me.School_Year_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.School_Year_Button.Location = New System.Drawing.Point(23, 362)
        Me.School_Year_Button.Name = "School_Year_Button"
        Me.School_Year_Button.Size = New System.Drawing.Size(147, 53)
        Me.School_Year_Button.TabIndex = 4
        Me.School_Year_Button.Text = "   S.Y. control"
        Me.School_Year_Button.UseVisualStyleBackColor = False
        '
        'Teacher_Button
        '
        Me.Teacher_Button.BackColor = System.Drawing.Color.Transparent
        Me.Teacher_Button.FlatAppearance.BorderSize = 0
        Me.Teacher_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSlateGray
        Me.Teacher_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen
        Me.Teacher_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Teacher_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Teacher_Button.Image = CType(resources.GetObject("Teacher_Button.Image"), System.Drawing.Image)
        Me.Teacher_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Teacher_Button.Location = New System.Drawing.Point(23, 281)
        Me.Teacher_Button.Name = "Teacher_Button"
        Me.Teacher_Button.Size = New System.Drawing.Size(147, 53)
        Me.Teacher_Button.TabIndex = 3
        Me.Teacher_Button.Text = "Teacher"
        Me.Teacher_Button.UseVisualStyleBackColor = False
        '
        'Exit_Button
        '
        Me.Exit_Button.BackColor = System.Drawing.Color.Transparent
        Me.Exit_Button.FlatAppearance.BorderSize = 0
        Me.Exit_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSlateGray
        Me.Exit_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen
        Me.Exit_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Exit_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Exit_Button.Image = CType(resources.GetObject("Exit_Button.Image"), System.Drawing.Image)
        Me.Exit_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Exit_Button.Location = New System.Drawing.Point(23, 654)
        Me.Exit_Button.Name = "Exit_Button"
        Me.Exit_Button.Size = New System.Drawing.Size(146, 34)
        Me.Exit_Button.TabIndex = 2
        Me.Exit_Button.Text = "Exit"
        Me.Exit_Button.UseVisualStyleBackColor = False
        '
        'Student_Button
        '
        Me.Student_Button.BackColor = System.Drawing.Color.Transparent
        Me.Student_Button.FlatAppearance.BorderSize = 0
        Me.Student_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSlateGray
        Me.Student_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen
        Me.Student_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Student_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Student_Button.Image = CType(resources.GetObject("Student_Button.Image"), System.Drawing.Image)
        Me.Student_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Student_Button.Location = New System.Drawing.Point(23, 205)
        Me.Student_Button.Name = "Student_Button"
        Me.Student_Button.Size = New System.Drawing.Size(147, 53)
        Me.Student_Button.TabIndex = 1
        Me.Student_Button.Text = "Student"
        Me.Student_Button.UseVisualStyleBackColor = False
        '
        'Dashboard_Button
        '
        Me.Dashboard_Button.BackColor = System.Drawing.Color.Transparent
        Me.Dashboard_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Dashboard_Button.FlatAppearance.BorderSize = 0
        Me.Dashboard_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSlateGray
        Me.Dashboard_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSeaGreen
        Me.Dashboard_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Dashboard_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dashboard_Button.Image = CType(resources.GetObject("Dashboard_Button.Image"), System.Drawing.Image)
        Me.Dashboard_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Dashboard_Button.Location = New System.Drawing.Point(23, 127)
        Me.Dashboard_Button.Name = "Dashboard_Button"
        Me.Dashboard_Button.Size = New System.Drawing.Size(147, 53)
        Me.Dashboard_Button.TabIndex = 0
        Me.Dashboard_Button.Text = "    Dashboard"
        Me.Dashboard_Button.UseVisualStyleBackColor = False
        '
        'Dashboard_Panel
        '
        Me.Dashboard_Panel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Dashboard_Panel.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Dashboard_Panel.Location = New System.Drawing.Point(195, 0)
        Me.Dashboard_Panel.Name = "Dashboard_Panel"
        Me.Dashboard_Panel.Size = New System.Drawing.Size(1153, 700)
        Me.Dashboard_Panel.TabIndex = 0
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(51, 22)
        Me.ToolStripButton1.Text = "New"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(65, 22)
        Me.ToolStripButton2.Text = "Update"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(62, 22)
        Me.ToolStripButton3.Text = "Search"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(60, 22)
        Me.ToolStripButton4.Text = "Delete"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(56, 22)
        Me.ToolStripButton5.Text = "Close"
        '
        'Add_Student_Button
        '
        Me.Add_Student_Button.Location = New System.Drawing.Point(696, 12)
        Me.Add_Student_Button.Name = "Add_Student_Button"
        Me.Add_Student_Button.Size = New System.Drawing.Size(138, 23)
        Me.Add_Student_Button.TabIndex = 0
        Me.Add_Student_Button.Text = "add student"
        Me.Add_Student_Button.UseVisualStyleBackColor = True
        '
        'Modify_Student_Button
        '
        Me.Modify_Student_Button.Location = New System.Drawing.Point(840, 12)
        Me.Modify_Student_Button.Name = "Modify_Student_Button"
        Me.Modify_Student_Button.Size = New System.Drawing.Size(138, 23)
        Me.Modify_Student_Button.TabIndex = 1
        Me.Modify_Student_Button.Text = "modify student"
        Me.Modify_Student_Button.UseVisualStyleBackColor = True
        '
        'Delete_Student_Button
        '
        Me.Delete_Student_Button.Location = New System.Drawing.Point(984, 12)
        Me.Delete_Student_Button.Name = "Delete_Student_Button"
        Me.Delete_Student_Button.Size = New System.Drawing.Size(138, 23)
        Me.Delete_Student_Button.TabIndex = 2
        Me.Delete_Student_Button.Text = "delete student"
        Me.Delete_Student_Button.UseVisualStyleBackColor = True
        '
        'Student_List_DataGridView
        '
        Me.Student_List_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Student_List_DataGridView.Location = New System.Drawing.Point(31, 127)
        Me.Student_List_DataGridView.Name = "Student_List_DataGridView"
        Me.Student_List_DataGridView.Size = New System.Drawing.Size(1091, 561)
        Me.Student_List_DataGridView.TabIndex = 3
        '
        'Student_Label
        '
        Me.Student_Label.AutoSize = True
        Me.Student_Label.Location = New System.Drawing.Point(28, 9)
        Me.Student_Label.Name = "Student_Label"
        Me.Student_Label.Size = New System.Drawing.Size(73, 13)
        Me.Student_Label.TabIndex = 4
        Me.Student_Label.Text = "Student panel"
        '
        'Search_Student_Label
        '
        Me.Search_Student_Label.AutoSize = True
        Me.Search_Student_Label.Location = New System.Drawing.Point(32, 104)
        Me.Search_Student_Label.Name = "Search_Student_Label"
        Me.Search_Student_Label.Size = New System.Drawing.Size(81, 13)
        Me.Search_Student_Label.TabIndex = 5
        Me.Search_Student_Label.Text = "Search Student"
        '
        'Search_Student_TextBox
        '
        Me.Search_Student_TextBox.Location = New System.Drawing.Point(171, 101)
        Me.Search_Student_TextBox.Name = "Search_Student_TextBox"
        Me.Search_Student_TextBox.Size = New System.Drawing.Size(539, 20)
        Me.Search_Student_TextBox.TabIndex = 6
        '
        'Teacher_Panel
        '
        Me.Teacher_Panel.BackColor = System.Drawing.Color.CadetBlue
        Me.Teacher_Panel.Controls.Add(Me.Refresh_teacher_Button)
        Me.Teacher_Panel.Controls.Add(Me.Back_Button)
        Me.Teacher_Panel.Controls.Add(Me.Delete_Teacher_Button)
        Me.Teacher_Panel.Controls.Add(Me.Assign_Class_To_Teacher_Button)
        Me.Teacher_Panel.Controls.Add(Me.Modify_Teacher_Button)
        Me.Teacher_Panel.Controls.Add(Me.Label2)
        Me.Teacher_Panel.Controls.Add(Me.Search_Teacher_TextBox)
        Me.Teacher_Panel.Controls.Add(Me.Teacher_List_DataGridView)
        Me.Teacher_Panel.Controls.Add(Me.Add_Teacher_Button)
        Me.Teacher_Panel.Controls.Add(Me.Label1)
        Me.Teacher_Panel.Location = New System.Drawing.Point(195, 0)
        Me.Teacher_Panel.Name = "Teacher_Panel"
        Me.Teacher_Panel.Size = New System.Drawing.Size(1153, 700)
        Me.Teacher_Panel.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 15.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(93, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(178, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "      Teacher Panel"
        '
        'Add_Teacher_Button
        '
        Me.Add_Teacher_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Add_Teacher_Button.Location = New System.Drawing.Point(749, 30)
        Me.Add_Teacher_Button.Name = "Add_Teacher_Button"
        Me.Add_Teacher_Button.Size = New System.Drawing.Size(125, 28)
        Me.Add_Teacher_Button.TabIndex = 1
        Me.Add_Teacher_Button.Text = "Add Teacher"
        Me.Add_Teacher_Button.UseVisualStyleBackColor = True
        '
        'Teacher_List_DataGridView
        '
        Me.Teacher_List_DataGridView.BackgroundColor = System.Drawing.Color.Azure
        Me.Teacher_List_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Teacher_List_DataGridView.Location = New System.Drawing.Point(35, 189)
        Me.Teacher_List_DataGridView.Name = "Teacher_List_DataGridView"
        Me.Teacher_List_DataGridView.Size = New System.Drawing.Size(1087, 470)
        Me.Teacher_List_DataGridView.TabIndex = 2
        '
        'Search_Teacher_TextBox
        '
        Me.Search_Teacher_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_Teacher_TextBox.Location = New System.Drawing.Point(159, 148)
        Me.Search_Teacher_TextBox.Name = "Search_Teacher_TextBox"
        Me.Search_Teacher_TextBox.Size = New System.Drawing.Size(470, 20)
        Me.Search_Teacher_TextBox.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(94, 148)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 21)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Search"
        '
        'Modify_Teacher_Button
        '
        Me.Modify_Teacher_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Modify_Teacher_Button.Location = New System.Drawing.Point(882, 30)
        Me.Modify_Teacher_Button.Name = "Modify_Teacher_Button"
        Me.Modify_Teacher_Button.Size = New System.Drawing.Size(125, 28)
        Me.Modify_Teacher_Button.TabIndex = 5
        Me.Modify_Teacher_Button.Text = "Modify Teacher"
        Me.Modify_Teacher_Button.UseVisualStyleBackColor = True
        '
        'Assign_Class_To_Teacher_Button
        '
        Me.Assign_Class_To_Teacher_Button.FlatAppearance.BorderSize = 0
        Me.Assign_Class_To_Teacher_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Assign_Class_To_Teacher_Button.Location = New System.Drawing.Point(923, 145)
        Me.Assign_Class_To_Teacher_Button.Name = "Assign_Class_To_Teacher_Button"
        Me.Assign_Class_To_Teacher_Button.Size = New System.Drawing.Size(192, 23)
        Me.Assign_Class_To_Teacher_Button.TabIndex = 6
        Me.Assign_Class_To_Teacher_Button.Text = "Assign Teacher to a Class"
        Me.Assign_Class_To_Teacher_Button.UseVisualStyleBackColor = True
        '
        'Delete_Teacher_Button
        '
        Me.Delete_Teacher_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Delete_Teacher_Button.Location = New System.Drawing.Point(1013, 30)
        Me.Delete_Teacher_Button.Name = "Delete_Teacher_Button"
        Me.Delete_Teacher_Button.Size = New System.Drawing.Size(125, 28)
        Me.Delete_Teacher_Button.TabIndex = 7
        Me.Delete_Teacher_Button.Text = "Delete Teacher"
        Me.Delete_Teacher_Button.UseVisualStyleBackColor = True
        '
        'Back_Button
        '
        Me.Back_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_Button.Location = New System.Drawing.Point(635, 146)
        Me.Back_Button.Name = "Back_Button"
        Me.Back_Button.Size = New System.Drawing.Size(75, 23)
        Me.Back_Button.TabIndex = 8
        Me.Back_Button.Text = "Back"
        Me.Back_Button.UseVisualStyleBackColor = True
        '
        'Refresh_teacher_Button
        '
        Me.Refresh_teacher_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Refresh_teacher_Button.Location = New System.Drawing.Point(828, 145)
        Me.Refresh_teacher_Button.Name = "Refresh_teacher_Button"
        Me.Refresh_teacher_Button.Size = New System.Drawing.Size(75, 23)
        Me.Refresh_teacher_Button.TabIndex = 9
        Me.Refresh_teacher_Button.Text = "Refresh"
        Me.Refresh_teacher_Button.UseVisualStyleBackColor = True
        '
        'Refresh_student_Button
        '
        Me.Refresh_student_Button.Location = New System.Drawing.Point(977, 98)
        Me.Refresh_student_Button.Name = "Refresh_student_Button"
        Me.Refresh_student_Button.Size = New System.Drawing.Size(138, 23)
        Me.Refresh_student_Button.TabIndex = 7
        Me.Refresh_student_Button.Text = "refresh"
        Me.Refresh_student_Button.UseVisualStyleBackColor = True
        '
        'Student_Panel
        '
        Me.Student_Panel.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Student_Panel.Controls.Add(Me.Refresh_student_Button)
        Me.Student_Panel.Controls.Add(Me.Search_Student_TextBox)
        Me.Student_Panel.Controls.Add(Me.Search_Student_Label)
        Me.Student_Panel.Controls.Add(Me.Student_Label)
        Me.Student_Panel.Controls.Add(Me.Student_List_DataGridView)
        Me.Student_Panel.Controls.Add(Me.Delete_Student_Button)
        Me.Student_Panel.Controls.Add(Me.Modify_Student_Button)
        Me.Student_Panel.Controls.Add(Me.Add_Student_Button)
        Me.Student_Panel.Location = New System.Drawing.Point(195, 0)
        Me.Student_Panel.Name = "Student_Panel"
        Me.Student_Panel.Size = New System.Drawing.Size(1153, 700)
        Me.Student_Panel.TabIndex = 2
        '
        'Admin_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1344, 700)
        Me.Controls.Add(Me.Navigation_Panel)
        Me.Controls.Add(Me.Teacher_Panel)
        Me.Controls.Add(Me.Student_Panel)
        Me.Controls.Add(Me.Dashboard_Panel)
        Me.Name = "Admin_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Admin_Form"
        Me.Navigation_Panel.ResumeLayout(False)
        CType(Me.Student_List_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Teacher_Panel.ResumeLayout(False)
        Me.Teacher_Panel.PerformLayout()
        CType(Me.Teacher_List_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Student_Panel.ResumeLayout(False)
        Me.Student_Panel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Navigation_Panel As System.Windows.Forms.Panel
    Friend WithEvents Dashboard_Panel As System.Windows.Forms.Panel
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents School_Year_Button As System.Windows.Forms.Button
    Friend WithEvents Teacher_Button As System.Windows.Forms.Button
    Friend WithEvents Exit_Button As System.Windows.Forms.Button
    Friend WithEvents Student_Button As System.Windows.Forms.Button
    Friend WithEvents Dashboard_Button As System.Windows.Forms.Button
    Friend WithEvents Logout_Button As System.Windows.Forms.Button
    Friend WithEvents Add_Student_Button As System.Windows.Forms.Button
    Friend WithEvents Modify_Student_Button As System.Windows.Forms.Button
    Friend WithEvents Delete_Student_Button As System.Windows.Forms.Button
    Friend WithEvents Student_List_DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Student_Label As System.Windows.Forms.Label
    Friend WithEvents Search_Student_Label As System.Windows.Forms.Label
    Friend WithEvents Search_Student_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Teacher_Panel As System.Windows.Forms.Panel
    Friend WithEvents Refresh_teacher_Button As System.Windows.Forms.Button
    Friend WithEvents Back_Button As System.Windows.Forms.Button
    Friend WithEvents Delete_Teacher_Button As System.Windows.Forms.Button
    Friend WithEvents Assign_Class_To_Teacher_Button As System.Windows.Forms.Button
    Friend WithEvents Modify_Teacher_Button As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Search_Teacher_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Teacher_List_DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Add_Teacher_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Refresh_student_Button As System.Windows.Forms.Button
    Friend WithEvents Student_Panel As System.Windows.Forms.Panel
End Class
