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
        Me.Student_Button = New System.Windows.Forms.Button
        Me.Dashboard_Button = New System.Windows.Forms.Button
        Me.Dashboard_Panel = New System.Windows.Forms.Panel
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.Label4 = New System.Windows.Forms.Label
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Label6 = New System.Windows.Forms.Label
        Me.Teacher_Panel = New System.Windows.Forms.Panel
        Me.Refresh_teacher_Button = New System.Windows.Forms.Button
        Me.Back_Button = New System.Windows.Forms.Button
        Me.Delete_Teacher_Button = New System.Windows.Forms.Button
        Me.Assign_Class_To_Teacher_Button = New System.Windows.Forms.Button
        Me.Modify_Teacher_Button = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Search_Teacher_TextBox = New System.Windows.Forms.TextBox
        Me.Teacher_List_DataGridView = New System.Windows.Forms.DataGridView
        Me.Add_Teacher_Button = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Student_Panel = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.Refresh_student_Button = New System.Windows.Forms.Button
        Me.Search_Student_TextBox = New System.Windows.Forms.TextBox
        Me.Search_Student_Label = New System.Windows.Forms.Label
        Me.Student_List_DataGridView = New System.Windows.Forms.DataGridView
        Me.Delete_Student_Button = New System.Windows.Forms.Button
        Me.Modify_Student_Button = New System.Windows.Forms.Button
        Me.Add_Student_Button = New System.Windows.Forms.Button
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton
        Me.Navigation_Panel.SuspendLayout()
        Me.Dashboard_Panel.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Teacher_Panel.SuspendLayout()
        CType(Me.Teacher_List_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Student_Panel.SuspendLayout()
        CType(Me.Student_List_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Navigation_Panel
        '
        Me.Navigation_Panel.BackColor = System.Drawing.Color.PowderBlue
        Me.Navigation_Panel.Controls.Add(Me.Logout_Button)
        Me.Navigation_Panel.Controls.Add(Me.School_Year_Button)
        Me.Navigation_Panel.Controls.Add(Me.Teacher_Button)
        Me.Navigation_Panel.Controls.Add(Me.Student_Button)
        Me.Navigation_Panel.Controls.Add(Me.Dashboard_Button)
        Me.Navigation_Panel.Location = New System.Drawing.Point(0, 0)
        Me.Navigation_Panel.Name = "Navigation_Panel"
        Me.Navigation_Panel.Size = New System.Drawing.Size(177, 700)
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
        Me.Dashboard_Panel.BackColor = System.Drawing.Color.CadetBlue
        Me.Dashboard_Panel.Controls.Add(Me.TableLayoutPanel2)
        Me.Dashboard_Panel.Location = New System.Drawing.Point(177, 0)
        Me.Dashboard_Panel.Name = "Dashboard_Panel"
        Me.Dashboard_Panel.Size = New System.Drawing.Size(1168, 700)
        Me.Dashboard_Panel.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel1, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel3, 1, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.Padding = New System.Windows.Forms.Padding(30, 20, 20, 20)
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.63584!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.36416!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 541.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1168, 700)
        Me.TableLayoutPanel2.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 14.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Image = CType(resources.GetObject("Label4.Image"), System.Drawing.Image)
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Location = New System.Drawing.Point(33, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(201, 28)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "      Dashboard Panel"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(33, 141)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 284.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(553, 493)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Azure
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(547, 203)
        Me.Panel1.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(144, 86)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(225, 50)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "School year"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Image = CType(resources.GetObject("Label5.Image"), System.Drawing.Image)
        Me.Label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label5.Location = New System.Drawing.Point(16, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(141, 25)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "      Current S.Y."
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Azure
        Me.Panel2.Controls.Add(Me.TableLayoutPanel3)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 212)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(547, 278)
        Me.Panel2.TabIndex = 2
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.62601!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.37399!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label9, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label10, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label11, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label12, 0, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.Label13, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label14, 2, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label15, 2, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label16, 2, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.Label17, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label18, 3, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label19, 1, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.Label20, 3, 3)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(21, 59)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 5
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.95833!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.04167!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(503, 191)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Image = CType(resources.GetObject("Label9.Image"), System.Drawing.Image)
        Me.Label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label9.Location = New System.Drawing.Point(3, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 19)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "       Students"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 46)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(118, 15)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Number of Students:"
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Image = CType(resources.GetObject("Label11.Image"), System.Drawing.Image)
        Me.Label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label11.Location = New System.Drawing.Point(3, 109)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(82, 19)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "      Courses"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 151)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(118, 15)
        Me.Label12.TabIndex = 3
        Me.Label12.Text = "Number of Courses:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Image = CType(resources.GetObject("Label13.Image"), System.Drawing.Image)
        Me.Label13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label13.Location = New System.Drawing.Point(243, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(91, 19)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "       Teachers"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(243, 46)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(117, 15)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "Number of Teachers:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Image = CType(resources.GetObject("Label15.Image"), System.Drawing.Image)
        Me.Label15.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label15.Location = New System.Drawing.Point(243, 109)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(121, 19)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "       S.Y. and Sem"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(243, 151)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(121, 15)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "Current S.Y. and Sem:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(127, 46)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(45, 13)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "Label17"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(370, 46)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(45, 13)
        Me.Label18.TabIndex = 9
        Me.Label18.Text = "Label18"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Location = New System.Drawing.Point(127, 153)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(110, 13)
        Me.Label19.TabIndex = 10
        Me.Label19.Text = "Label19"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Location = New System.Drawing.Point(370, 153)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(130, 13)
        Me.Label20.TabIndex = 11
        Me.Label20.Text = "Label20"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Image = CType(resources.GetObject("Label8.Image"), System.Drawing.Image)
        Me.Label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label8.Location = New System.Drawing.Point(16, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 25)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "        Status"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Azure
        Me.Panel3.Controls.Add(Me.DataGridView1)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Location = New System.Drawing.Point(592, 141)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(553, 490)
        Me.Panel3.TabIndex = 2
        '
        'DataGridView1
        '
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.Azure
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.GridColor = System.Drawing.Color.Azure
        Me.DataGridView1.Location = New System.Drawing.Point(24, 64)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(504, 387)
        Me.DataGridView1.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Image = CType(resources.GetObject("Label6.Image"), System.Drawing.Image)
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label6.Location = New System.Drawing.Point(19, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(119, 25)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "        Courses"
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
        Me.Teacher_Panel.Location = New System.Drawing.Point(177, 0)
        Me.Teacher_Panel.Name = "Teacher_Panel"
        Me.Teacher_Panel.Size = New System.Drawing.Size(1700, 717)
        Me.Teacher_Panel.TabIndex = 9
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
        Me.Assign_Class_To_Teacher_Button.Size = New System.Drawing.Size(190, 28)
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
        'Student_Panel
        '
        Me.Student_Panel.BackColor = System.Drawing.Color.CadetBlue
        Me.Student_Panel.Controls.Add(Me.Label3)
        Me.Student_Panel.Controls.Add(Me.Refresh_student_Button)
        Me.Student_Panel.Controls.Add(Me.Search_Student_TextBox)
        Me.Student_Panel.Controls.Add(Me.Search_Student_Label)
        Me.Student_Panel.Controls.Add(Me.Student_List_DataGridView)
        Me.Student_Panel.Controls.Add(Me.Delete_Student_Button)
        Me.Student_Panel.Controls.Add(Me.Modify_Student_Button)
        Me.Student_Panel.Controls.Add(Me.Add_Student_Button)
        Me.Student_Panel.Location = New System.Drawing.Point(177, 0)
        Me.Student_Panel.Name = "Student_Panel"
        Me.Student_Panel.Size = New System.Drawing.Size(1523, 700)
        Me.Student_Panel.TabIndex = 11
        Me.Student_Panel.Tag = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 15.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Image = CType(resources.GetObject("Label3.Image"), System.Drawing.Image)
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Location = New System.Drawing.Point(30, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(179, 30)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "      Student Panel"
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
        'Admin_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1344, 700)
        Me.ControlBox = False
        Me.Controls.Add(Me.Dashboard_Panel)
        Me.Controls.Add(Me.Student_Panel)
        Me.Controls.Add(Me.Teacher_Panel)
        Me.Controls.Add(Me.Navigation_Panel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Admin_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Admin_Form"
        Me.Navigation_Panel.ResumeLayout(False)
        Me.Dashboard_Panel.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Teacher_Panel.ResumeLayout(False)
        Me.Teacher_Panel.PerformLayout()
        CType(Me.Teacher_List_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents School_Year_Button As System.Windows.Forms.Button
    Friend WithEvents Teacher_Button As System.Windows.Forms.Button
    Friend WithEvents Student_Button As System.Windows.Forms.Button
    Friend WithEvents Dashboard_Button As System.Windows.Forms.Button
    Friend WithEvents Logout_Button As System.Windows.Forms.Button
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
    Friend WithEvents Student_Panel As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Refresh_student_Button As System.Windows.Forms.Button
    Friend WithEvents Search_Student_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Search_Student_Label As System.Windows.Forms.Label
    Friend WithEvents Student_List_DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Delete_Student_Button As System.Windows.Forms.Button
    Friend WithEvents Modify_Student_Button As System.Windows.Forms.Button
    Friend WithEvents Add_Student_Button As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
End Class
