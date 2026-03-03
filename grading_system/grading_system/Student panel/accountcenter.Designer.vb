<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class accountcenter
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.email = New System.Windows.Forms.Label
        Me.currentpass = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.NewPass = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.renewpass = New System.Windows.Forms.Label
        Me.ChangePass = New System.Windows.Forms.Button
        Me.confirmation = New System.Windows.Forms.Button
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.CheckBox3 = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(206, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Account Center"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 25)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Email:"
        '
        'email
        '
        Me.email.AutoSize = True
        Me.email.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.email.Location = New System.Drawing.Point(89, 94)
        Me.email.Name = "email"
        Me.email.Size = New System.Drawing.Size(71, 25)
        Me.email.TabIndex = 2
        Me.email.Text = "Email:"
        '
        'currentpass
        '
        Me.currentpass.AutoSize = True
        Me.currentpass.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.currentpass.Location = New System.Drawing.Point(12, 139)
        Me.currentpass.Name = "currentpass"
        Me.currentpass.Size = New System.Drawing.Size(189, 25)
        Me.currentpass.TabIndex = 3
        Me.currentpass.Text = "Current Password:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(207, 139)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(220, 25)
        Me.TextBox1.TabIndex = 4
        '
        'NewPass
        '
        Me.NewPass.AutoSize = True
        Me.NewPass.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewPass.Location = New System.Drawing.Point(12, 190)
        Me.NewPass.Name = "NewPass"
        Me.NewPass.Size = New System.Drawing.Size(160, 25)
        Me.NewPass.TabIndex = 5
        Me.NewPass.Text = "New Password:"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(207, 190)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(220, 25)
        Me.TextBox2.TabIndex = 6
        Me.TextBox2.Visible = False
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(259, 240)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(220, 25)
        Me.TextBox3.TabIndex = 7
        Me.TextBox3.Visible = False
        '
        'renewpass
        '
        Me.renewpass.AutoSize = True
        Me.renewpass.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.renewpass.Location = New System.Drawing.Point(12, 240)
        Me.renewpass.Name = "renewpass"
        Me.renewpass.Size = New System.Drawing.Size(241, 25)
        Me.renewpass.TabIndex = 8
        Me.renewpass.Text = "Re-type New Password:"
        '
        'ChangePass
        '
        Me.ChangePass.Location = New System.Drawing.Point(238, 293)
        Me.ChangePass.Name = "ChangePass"
        Me.ChangePass.Size = New System.Drawing.Size(123, 31)
        Me.ChangePass.TabIndex = 9
        Me.ChangePass.Text = "Change Password"
        Me.ChangePass.UseVisualStyleBackColor = True
        '
        'confirmation
        '
        Me.confirmation.Location = New System.Drawing.Point(456, 139)
        Me.confirmation.Name = "confirmation"
        Me.confirmation.Size = New System.Drawing.Size(100, 25)
        Me.confirmation.TabIndex = 10
        Me.confirmation.Text = "Confirmation"
        Me.confirmation.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(433, 148)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 11
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(433, 193)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox2.TabIndex = 12
        Me.CheckBox2.UseVisualStyleBackColor = True
        Me.CheckBox2.Visible = False
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(485, 249)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox3.TabIndex = 13
        Me.CheckBox3.UseVisualStyleBackColor = True
        Me.CheckBox3.Visible = False
        '
        'accountcenter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
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
        Me.Name = "accountcenter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "accountcenter"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents email As System.Windows.Forms.Label
    Friend WithEvents currentpass As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents NewPass As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents renewpass As System.Windows.Forms.Label
    Friend WithEvents ChangePass As System.Windows.Forms.Button
    Friend WithEvents confirmation As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
End Class
