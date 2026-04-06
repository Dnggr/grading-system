<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class teacherpanel
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(teacherpanel))
        Me.Teacher_Panel = New System.Windows.Forms.Panel
        Me.Student_Panel = New System.Windows.Forms.Panel
        Me.Refresh_student_Button = New System.Windows.Forms.Button
        Me.Search_Student_TextBox = New System.Windows.Forms.TextBox
        Me.Search_Student_Label = New System.Windows.Forms.Label
        Me.Student_List_DataGridView = New System.Windows.Forms.DataGridView
        Me.Delete_Student_Button = New System.Windows.Forms.Button
        Me.Modify_Student_Button = New System.Windows.Forms.Button
        Me.Add_Student_Button = New System.Windows.Forms.Button
        Me.Refresh_teacher_Button = New System.Windows.Forms.Button
        Me.Back_Button = New System.Windows.Forms.Button
        Me.Delete_Teacher_Button = New System.Windows.Forms.Button
        Me.Assign_Class_To_Teacher_Button = New System.Windows.Forms.Button
        Me.Modify_Teacher_Button = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Search_Teacher_TextBox = New System.Windows.Forms.TextBox
        Me.Teacher_List_DataGridView = New System.Windows.Forms.DataGridView
        Me.Add_Teacher_Button = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Teacher_Panel.SuspendLayout()
        Me.Student_Panel.SuspendLayout()
        CType(Me.Student_List_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Teacher_List_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.Teacher_Panel.Location = New System.Drawing.Point(94, 55)
        Me.Teacher_Panel.Name = "Teacher_Panel"
        Me.Teacher_Panel.Size = New System.Drawing.Size(1153, 700)
        Me.Teacher_Panel.TabIndex = 10
        '
        'Student_Panel
        '
        Me.Student_Panel.BackColor = System.Drawing.Color.Transparent
        Me.Student_Panel.Controls.Add(Me.Label3)
        Me.Student_Panel.Controls.Add(Me.Refresh_student_Button)
        Me.Student_Panel.Controls.Add(Me.Search_Student_TextBox)
        Me.Student_Panel.Controls.Add(Me.Search_Student_Label)
        Me.Student_Panel.Controls.Add(Me.Student_List_DataGridView)
        Me.Student_Panel.Controls.Add(Me.Delete_Student_Button)
        Me.Student_Panel.Controls.Add(Me.Modify_Student_Button)
        Me.Student_Panel.Controls.Add(Me.Add_Student_Button)
        Me.Student_Panel.Location = New System.Drawing.Point(20, 398)
        Me.Student_Panel.Name = "Student_Panel"
        Me.Student_Panel.Size = New System.Drawing.Size(1153, 700)
        Me.Student_Panel.TabIndex = 3
        '
        'Refresh_student_Button
        '
        Me.Refresh_student_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Refresh_student_Button.Location = New System.Drawing.Point(984, 96)
        Me.Refresh_student_Button.Name = "Refresh_student_Button"
        Me.Refresh_student_Button.Size = New System.Drawing.Size(125, 28)
        Me.Refresh_student_Button.TabIndex = 7
        Me.Refresh_student_Button.Text = "Refresh"
        Me.Refresh_student_Button.UseVisualStyleBackColor = True
        '
        'Search_Student_TextBox
        '
        Me.Search_Student_TextBox.Location = New System.Drawing.Point(171, 101)
        Me.Search_Student_TextBox.Name = "Search_Student_TextBox"
        Me.Search_Student_TextBox.Size = New System.Drawing.Size(539, 20)
        Me.Search_Student_TextBox.TabIndex = 6
        '
        'Search_Student_Label
        '
        Me.Search_Student_Label.AutoSize = True
        Me.Search_Student_Label.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Search_Student_Label.Location = New System.Drawing.Point(44, 98)
        Me.Search_Student_Label.Name = "Search_Student_Label"
        Me.Search_Student_Label.Size = New System.Drawing.Size(121, 21)
        Me.Search_Student_Label.TabIndex = 5
        Me.Search_Student_Label.Text = "Search Student"
        '
        'Student_List_DataGridView
        '
        Me.Student_List_DataGridView.BackgroundColor = System.Drawing.Color.Azure
        Me.Student_List_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Student_List_DataGridView.Location = New System.Drawing.Point(31, 127)
        Me.Student_List_DataGridView.Name = "Student_List_DataGridView"
        Me.Student_List_DataGridView.Size = New System.Drawing.Size(1091, 561)
        Me.Student_List_DataGridView.TabIndex = 3
        '
        'Delete_Student_Button
        '
        Me.Delete_Student_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Delete_Student_Button.Location = New System.Drawing.Point(984, 12)
        Me.Delete_Student_Button.Name = "Delete_Student_Button"
        Me.Delete_Student_Button.Size = New System.Drawing.Size(125, 28)
        Me.Delete_Student_Button.TabIndex = 2
        Me.Delete_Student_Button.Text = "Delete Student"
        Me.Delete_Student_Button.UseVisualStyleBackColor = True
        '
        'Modify_Student_Button
        '
        Me.Modify_Student_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Modify_Student_Button.Location = New System.Drawing.Point(840, 12)
        Me.Modify_Student_Button.Name = "Modify_Student_Button"
        Me.Modify_Student_Button.Size = New System.Drawing.Size(125, 28)
        Me.Modify_Student_Button.TabIndex = 1
        Me.Modify_Student_Button.Text = "Modify Student"
        Me.Modify_Student_Button.UseVisualStyleBackColor = True
        '
        'Add_Student_Button
        '
        Me.Add_Student_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Add_Student_Button.Location = New System.Drawing.Point(696, 12)
        Me.Add_Student_Button.Name = "Add_Student_Button"
        Me.Add_Student_Button.Size = New System.Drawing.Size(125, 28)
        Me.Add_Student_Button.TabIndex = 0
        Me.Add_Student_Button.Text = "Add Student"
        Me.Add_Student_Button.UseVisualStyleBackColor = True
        '
        'Refresh_teacher_Button
        '
        Me.Refresh_teacher_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Refresh_teacher_Button.Location = New System.Drawing.Point(779, 141)
        Me.Refresh_teacher_Button.Name = "Refresh_teacher_Button"
        Me.Refresh_teacher_Button.Size = New System.Drawing.Size(125, 28)
        Me.Refresh_teacher_Button.TabIndex = 9
        Me.Refresh_teacher_Button.Text = "Refresh"
        Me.Refresh_teacher_Button.UseVisualStyleBackColor = True
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
        'Assign_Class_To_Teacher_Button
        '
        Me.Assign_Class_To_Teacher_Button.FlatAppearance.BorderSize = 0
        Me.Assign_Class_To_Teacher_Button.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Assign_Class_To_Teacher_Button.Location = New System.Drawing.Point(919, 140)
        Me.Assign_Class_To_Teacher_Button.Name = "Assign_Class_To_Teacher_Button"
        Me.Assign_Class_To_Teacher_Button.Size = New System.Drawing.Size(125, 28)
        Me.Assign_Class_To_Teacher_Button.TabIndex = 6
        Me.Assign_Class_To_Teacher_Button.Text = "Assign Teacher to a Class"
        Me.Assign_Class_To_Teacher_Button.UseVisualStyleBackColor = True
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
        'Search_Teacher_TextBox
        '
        Me.Search_Teacher_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_Teacher_TextBox.Location = New System.Drawing.Point(159, 148)
        Me.Search_Teacher_TextBox.Name = "Search_Teacher_TextBox"
        Me.Search_Teacher_TextBox.Size = New System.Drawing.Size(470, 20)
        Me.Search_Teacher_TextBox.TabIndex = 3
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
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 15.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Image = CType(resources.GetObject("Label3.Image"), System.Drawing.Image)
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Location = New System.Drawing.Point(30, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(179, 30)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "      Student Panel"
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
        'teacherpanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Student_Panel)
        Me.Controls.Add(Me.Teacher_Panel)
        Me.Name = "teacherpanel"
        Me.Size = New System.Drawing.Size(1283, 776)
        Me.Teacher_Panel.ResumeLayout(False)
        Me.Teacher_Panel.PerformLayout()
        Me.Student_Panel.ResumeLayout(False)
        Me.Student_Panel.PerformLayout()
        CType(Me.Student_List_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Teacher_List_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Teacher_Panel As System.Windows.Forms.Panel
    Friend WithEvents Student_Panel As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Refresh_student_Button As System.Windows.Forms.Button
    Friend WithEvents Search_Student_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Search_Student_Label As System.Windows.Forms.Label
    Friend WithEvents Student_List_DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Delete_Student_Button As System.Windows.Forms.Button
    Friend WithEvents Modify_Student_Button As System.Windows.Forms.Button
    Friend WithEvents Add_Student_Button As System.Windows.Forms.Button
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

End Class
