<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class accountcenterprof
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
        Me.CheckBox3 = New System.Windows.Forms.CheckBox
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.confirmation = New System.Windows.Forms.Button
        Me.ChangePass = New System.Windows.Forms.Button
        Me.renewpass = New System.Windows.Forms.Label
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.NewPass = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.currentpass = New System.Windows.Forms.Label
        Me.email = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(506, 249)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox3.TabIndex = 27
        Me.CheckBox3.UseVisualStyleBackColor = True
        Me.CheckBox3.Visible = False
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(422, 196)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox2.TabIndex = 26
        Me.CheckBox2.UseVisualStyleBackColor = True
        Me.CheckBox2.Visible = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(452, 151)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 25
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'confirmation
        '
        Me.confirmation.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.confirmation.Location = New System.Drawing.Point(473, 140)
        Me.confirmation.Name = "confirmation"
        Me.confirmation.Size = New System.Drawing.Size(100, 25)
        Me.confirmation.TabIndex = 24
        Me.confirmation.Text = "Confirmation"
        Me.confirmation.UseVisualStyleBackColor = True
        '
        'ChangePass
        '
        Me.ChangePass.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChangePass.Location = New System.Drawing.Point(226, 287)
        Me.ChangePass.Name = "ChangePass"
        Me.ChangePass.Size = New System.Drawing.Size(123, 31)
        Me.ChangePass.TabIndex = 23
        Me.ChangePass.Text = "Change Password"
        Me.ChangePass.UseVisualStyleBackColor = True
        '
        'renewpass
        '
        Me.renewpass.AutoSize = True
        Me.renewpass.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.renewpass.Location = New System.Drawing.Point(11, 231)
        Me.renewpass.Name = "renewpass"
        Me.renewpass.Size = New System.Drawing.Size(270, 32)
        Me.renewpass.TabIndex = 22
        Me.renewpass.Text = "Re-type New Password:"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(280, 238)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(220, 25)
        Me.TextBox3.TabIndex = 21
        Me.TextBox3.Visible = False
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(196, 185)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(220, 25)
        Me.TextBox2.TabIndex = 20
        Me.TextBox2.Visible = False
        '
        'NewPass
        '
        Me.NewPass.AutoSize = True
        Me.NewPass.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewPass.Location = New System.Drawing.Point(12, 181)
        Me.NewPass.Name = "NewPass"
        Me.NewPass.Size = New System.Drawing.Size(178, 32)
        Me.NewPass.TabIndex = 19
        Me.NewPass.Text = "New Password:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(226, 140)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(220, 25)
        Me.TextBox1.TabIndex = 18
        '
        'currentpass
        '
        Me.currentpass.AutoSize = True
        Me.currentpass.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.currentpass.Location = New System.Drawing.Point(11, 136)
        Me.currentpass.Name = "currentpass"
        Me.currentpass.Size = New System.Drawing.Size(212, 32)
        Me.currentpass.TabIndex = 17
        Me.currentpass.Text = "Current Password:"
        '
        'email
        '
        Me.email.AutoSize = True
        Me.email.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.email.Location = New System.Drawing.Point(95, 90)
        Me.email.Name = "email"
        Me.email.Size = New System.Drawing.Size(77, 32)
        Me.email.TabIndex = 16
        Me.email.Text = "Email:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 32)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Email:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(202, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(165, 30)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Account Center"
        '
        'accountcenterprof
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Azure
        Me.ClientSize = New System.Drawing.Size(585, 345)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.confirmation)
        Me.Controls.Add(Me.ChangePass)
        Me.Controls.Add(Me.renewpass)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.NewPass)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.currentpass)
        Me.Controls.Add(Me.email)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "accountcenterprof"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "accountcenterprof"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents confirmation As System.Windows.Forms.Button
    Friend WithEvents ChangePass As System.Windows.Forms.Button
    Friend WithEvents renewpass As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents NewPass As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents currentpass As System.Windows.Forms.Label
    Friend WithEvents email As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
