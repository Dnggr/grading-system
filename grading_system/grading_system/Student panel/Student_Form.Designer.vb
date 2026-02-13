<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Student_Form
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
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button3 = New System.Windows.Forms.Button
        Me.s = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.mainpanel = New System.Windows.Forms.Panel
        Me.schedule = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(24, 251)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(147, 62)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Info"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(24, 646)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(147, 60)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Log out"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel1.Controls.Add(Me.schedule)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(194, 729)
        Me.Panel1.TabIndex = 46
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 128)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 24)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Label1"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(24, 403)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(147, 61)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Grades"
        Me.Button3.UseVisualStyleBackColor = True
        '
        's
        '
        Me.s.ActiveViewIndex = -1
        Me.s.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.s.DisplayGroupTree = False
        Me.s.Dock = System.Windows.Forms.DockStyle.Fill
        Me.s.Location = New System.Drawing.Point(194, 0)
        Me.s.Name = "s"
        Me.s.SelectionFormula = ""
        Me.s.Size = New System.Drawing.Size(1156, 729)
        Me.s.TabIndex = 47
        Me.s.ViewTimeSelectionFormula = ""
        '
        'mainpanel
        '
        Me.mainpanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mainpanel.Location = New System.Drawing.Point(194, 0)
        Me.mainpanel.Name = "mainpanel"
        Me.mainpanel.Size = New System.Drawing.Size(1156, 729)
        Me.mainpanel.TabIndex = 48
        '
        'schedule
        '
        Me.schedule.Location = New System.Drawing.Point(24, 522)
        Me.schedule.Name = "schedule"
        Me.schedule.Size = New System.Drawing.Size(147, 60)
        Me.schedule.TabIndex = 5
        Me.schedule.Text = "schedule"
        Me.schedule.UseVisualStyleBackColor = True
        '
        'Student_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Controls.Add(Me.mainpanel)
        Me.Controls.Add(Me.s)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Student_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Student_Form"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents s As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mainpanel As System.Windows.Forms.Panel
    Friend WithEvents schedule As System.Windows.Forms.Button
End Class
