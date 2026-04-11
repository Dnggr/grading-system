<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class profprofile
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(profprofile))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.ame = New System.Windows.Forms.Label
        Me.fname = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.gender = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.subject = New System.Windows.Forms.Label
        Me.profid = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DarkCyan
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(98, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1021, 701)
        Me.Panel1.TabIndex = 23
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.LightCyan
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.TableLayoutPanel3)
        Me.Panel3.Location = New System.Drawing.Point(16, 202)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(991, 467)
        Me.Panel3.TabIndex = 23
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(25, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(177, 37)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Personal Info"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(18, 77)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(956, 365)
        Me.TableLayoutPanel3.TabIndex = 21
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.21053!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.78947!))
        Me.TableLayoutPanel2.Controls.Add(Me.ame, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.fname, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.gender, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label14, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label15, 1, 2)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.40984!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.59016!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 114.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(950, 359)
        Me.TableLayoutPanel2.TabIndex = 20
        '
        'ame
        '
        Me.ame.AutoSize = True
        Me.ame.Font = New System.Drawing.Font("Segoe UI Semibold", 19.0!, System.Drawing.FontStyle.Bold)
        Me.ame.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ame.Image = CType(resources.GetObject("ame.Image"), System.Drawing.Image)
        Me.ame.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ame.Location = New System.Drawing.Point(3, 0)
        Me.ame.Name = "ame"
        Me.ame.Size = New System.Drawing.Size(127, 36)
        Me.ame.TabIndex = 9
        Me.ame.Text = "     Name:"
        '
        'fname
        '
        Me.fname.AutoSize = True
        Me.fname.Font = New System.Drawing.Font("Segoe UI Semibold", 19.0!, System.Drawing.FontStyle.Bold)
        Me.fname.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.fname.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.fname.Location = New System.Drawing.Point(157, 0)
        Me.fname.Name = "fname"
        Me.fname.Size = New System.Drawing.Size(90, 36)
        Me.fname.TabIndex = 12
        Me.fname.Text = "fname"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 19.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.Image = CType(resources.GetObject("Label4.Image"), System.Drawing.Image)
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Location = New System.Drawing.Point(3, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(144, 36)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "     Gender:"
        '
        'gender
        '
        Me.gender.AutoSize = True
        Me.gender.Font = New System.Drawing.Font("Segoe UI Semibold", 19.0!, System.Drawing.FontStyle.Bold)
        Me.gender.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.gender.Location = New System.Drawing.Point(157, 123)
        Me.gender.Name = "gender"
        Me.gender.Size = New System.Drawing.Size(100, 36)
        Me.gender.TabIndex = 10
        Me.gender.Text = "gender"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 19.0!, System.Drawing.FontStyle.Bold)
        Me.Label14.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label14.Image = CType(resources.GetObject("Label14.Image"), System.Drawing.Image)
        Me.Label14.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label14.Location = New System.Drawing.Point(3, 244)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(120, 36)
        Me.Label14.TabIndex = 17
        Me.Label14.Text = "     Email:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 19.0!, System.Drawing.FontStyle.Bold)
        Me.Label15.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label15.Location = New System.Drawing.Point(157, 244)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(80, 36)
        Me.Label15.TabIndex = 18
        Me.Label15.Text = "email"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.LightCyan
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.Button3)
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.subject)
        Me.Panel2.Controls.Add(Me.profid)
        Me.Panel2.Location = New System.Drawing.Point(16, 16)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(991, 180)
        Me.Panel2.TabIndex = 22
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Azure
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(857, 24)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(92, 38)
        Me.Button1.TabIndex = 19
        Me.Button1.Text = "Add/Replace " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Image"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Azure
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(857, 97)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(92, 47)
        Me.Button3.TabIndex = 21
        Me.Button3.Text = "Account Center"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Azure
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(857, 68)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(92, 23)
        Me.Button2.TabIndex = 20
        Me.Button2.Text = "Delete Image"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 17.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label8.Image = CType(resources.GetObject("Label8.Image"), System.Drawing.Image)
        Me.Label8.Location = New System.Drawing.Point(254, 105)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 31)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "     "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 17.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label6.Location = New System.Drawing.Point(414, 105)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(20, 31)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "|"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(253, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(157, 37)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "First Name:"
        '
        'subject
        '
        Me.subject.AutoSize = True
        Me.subject.Font = New System.Drawing.Font("Segoe UI Semibold", 17.0!, System.Drawing.FontStyle.Bold)
        Me.subject.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.subject.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.subject.Location = New System.Drawing.Point(295, 105)
        Me.subject.Name = "subject"
        Me.subject.Size = New System.Drawing.Size(113, 31)
        Me.subject.TabIndex = 13
        Me.subject.Text = "Professor"
        '
        'profid
        '
        Me.profid.AutoSize = True
        Me.profid.Font = New System.Drawing.Font("Segoe UI Semibold", 17.0!, System.Drawing.FontStyle.Bold)
        Me.profid.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.profid.Location = New System.Drawing.Point(440, 105)
        Me.profid.Name = "profid"
        Me.profid.Size = New System.Drawing.Size(88, 31)
        Me.profid.TabIndex = 8
        Me.profid.Text = "section"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(21, 24)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(205, 132)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 22
        Me.PictureBox1.TabStop = False
        '
        'profprofile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkCyan
        Me.Controls.Add(Me.Panel1)
        Me.Name = "profprofile"
        Me.Size = New System.Drawing.Size(1283, 776)
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ame As System.Windows.Forms.Label
    Friend WithEvents fname As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents gender As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents subject As System.Windows.Forms.Label
    Friend WithEvents profid As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

End Class
