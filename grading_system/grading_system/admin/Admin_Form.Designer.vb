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
        Me.Student_Panel = New System.Windows.Forms.Panel
        Me.Teacher_Panel = New System.Windows.Forms.Panel
        Me.School_Year_Panel = New System.Windows.Forms.Panel
        Me.Add_Student_Button = New System.Windows.Forms.Button
        Me.Modify_Student_Button = New System.Windows.Forms.Button
        Me.Delete_Student_Button = New System.Windows.Forms.Button
        Me.Student_List_DataGridView = New System.Windows.Forms.DataGridView
        Me.Student_Label = New System.Windows.Forms.Label
        Me.Search_Student_Label = New System.Windows.Forms.Label
        Me.Search_Student_TextBox = New System.Windows.Forms.TextBox
        Me.Navigation_Panel.SuspendLayout()
        Me.Student_Panel.SuspendLayout()
        CType(Me.Student_List_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Navigation_Panel
        '
        Me.Navigation_Panel.BackColor = System.Drawing.SystemColors.Highlight
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
        Me.Logout_Button.Location = New System.Drawing.Point(23, 636)
        Me.Logout_Button.Name = "Logout_Button"
        Me.Logout_Button.Size = New System.Drawing.Size(146, 23)
        Me.Logout_Button.TabIndex = 5
        Me.Logout_Button.Text = "Logout"
        Me.Logout_Button.UseVisualStyleBackColor = True
        '
        'School_Year_Button
        '
        Me.School_Year_Button.Location = New System.Drawing.Point(23, 310)
        Me.School_Year_Button.Name = "School_Year_Button"
        Me.School_Year_Button.Size = New System.Drawing.Size(146, 23)
        Me.School_Year_Button.TabIndex = 4
        Me.School_Year_Button.Text = "School year control"
        Me.School_Year_Button.UseVisualStyleBackColor = True
        '
        'Teacher_Button
        '
        Me.Teacher_Button.Location = New System.Drawing.Point(23, 281)
        Me.Teacher_Button.Name = "Teacher_Button"
        Me.Teacher_Button.Size = New System.Drawing.Size(146, 23)
        Me.Teacher_Button.TabIndex = 3
        Me.Teacher_Button.Text = "Teacher"
        Me.Teacher_Button.UseVisualStyleBackColor = True
        '
        'Exit_Button
        '
        Me.Exit_Button.Location = New System.Drawing.Point(23, 665)
        Me.Exit_Button.Name = "Exit_Button"
        Me.Exit_Button.Size = New System.Drawing.Size(146, 23)
        Me.Exit_Button.TabIndex = 2
        Me.Exit_Button.Text = "Exit"
        Me.Exit_Button.UseVisualStyleBackColor = True
        '
        'Student_Button
        '
        Me.Student_Button.Location = New System.Drawing.Point(23, 252)
        Me.Student_Button.Name = "Student_Button"
        Me.Student_Button.Size = New System.Drawing.Size(146, 23)
        Me.Student_Button.TabIndex = 1
        Me.Student_Button.Text = "Student"
        Me.Student_Button.UseVisualStyleBackColor = True
        '
        'Dashboard_Button
        '
        Me.Dashboard_Button.Location = New System.Drawing.Point(23, 223)
        Me.Dashboard_Button.Name = "Dashboard_Button"
        Me.Dashboard_Button.Size = New System.Drawing.Size(146, 23)
        Me.Dashboard_Button.TabIndex = 0
        Me.Dashboard_Button.Text = "Dashboard"
        Me.Dashboard_Button.UseVisualStyleBackColor = True
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
        'Student_Panel
        '
        Me.Student_Panel.BackColor = System.Drawing.SystemColors.ActiveCaption
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
        'Teacher_Panel
        '
        Me.Teacher_Panel.Location = New System.Drawing.Point(195, 0)
        Me.Teacher_Panel.Name = "Teacher_Panel"
        Me.Teacher_Panel.Size = New System.Drawing.Size(1153, 700)
        Me.Teacher_Panel.TabIndex = 2
        '
        'School_Year_Panel
        '
        Me.School_Year_Panel.Location = New System.Drawing.Point(195, 0)
        Me.School_Year_Panel.Name = "School_Year_Panel"
        Me.School_Year_Panel.Size = New System.Drawing.Size(1153, 700)
        Me.School_Year_Panel.TabIndex = 2
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
        'Admin_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1344, 700)
        Me.Controls.Add(Me.Navigation_Panel)
        Me.Controls.Add(Me.Student_Panel)
        Me.Controls.Add(Me.Dashboard_Panel)
        Me.Controls.Add(Me.Teacher_Panel)
        Me.Controls.Add(Me.School_Year_Panel)
        Me.Name = "Admin_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Admin_Form"
        Me.Navigation_Panel.ResumeLayout(False)
        Me.Student_Panel.ResumeLayout(False)
        Me.Student_Panel.PerformLayout()
        CType(Me.Student_List_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Navigation_Panel As System.Windows.Forms.Panel
    Friend WithEvents Dashboard_Panel As System.Windows.Forms.Panel
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Student_Panel As System.Windows.Forms.Panel
    Friend WithEvents Teacher_Panel As System.Windows.Forms.Panel
    Friend WithEvents School_Year_Panel As System.Windows.Forms.Panel
    Friend WithEvents School_Year_Button As System.Windows.Forms.Button
    Friend WithEvents Teacher_Button As System.Windows.Forms.Button
    Friend WithEvents Exit_Button As System.Windows.Forms.Button
    Friend WithEvents Student_Button As System.Windows.Forms.Button
    Friend WithEvents Dashboard_Button As System.Windows.Forms.Button
    Friend WithEvents Logout_Button As System.Windows.Forms.Button
    Friend WithEvents Student_List_DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Delete_Student_Button As System.Windows.Forms.Button
    Friend WithEvents Modify_Student_Button As System.Windows.Forms.Button
    Friend WithEvents Add_Student_Button As System.Windows.Forms.Button
    Friend WithEvents Search_Student_Label As System.Windows.Forms.Label
    Friend WithEvents Student_Label As System.Windows.Forms.Label
    Friend WithEvents Search_Student_TextBox As System.Windows.Forms.TextBox
End Class
