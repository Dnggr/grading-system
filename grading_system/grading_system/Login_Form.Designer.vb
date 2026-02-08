<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login_Form
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
        Me.loginPanel = New System.Windows.Forms.Panel
        Me.passwordLabel = New System.Windows.Forms.Label
        Me.emailLabel = New System.Windows.Forms.Label
        Me.loginLabel = New System.Windows.Forms.Label
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.loginButton = New System.Windows.Forms.Button
        Me.loginpasswordTextBox = New System.Windows.Forms.TextBox
        Me.loginemailTextBox = New System.Windows.Forms.TextBox
        Me.loginPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'loginPanel
        '
        Me.loginPanel.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.loginPanel.Controls.Add(Me.passwordLabel)
        Me.loginPanel.Controls.Add(Me.emailLabel)
        Me.loginPanel.Controls.Add(Me.loginLabel)
        Me.loginPanel.Controls.Add(Me.LinkLabel1)
        Me.loginPanel.Controls.Add(Me.loginButton)
        Me.loginPanel.Controls.Add(Me.loginpasswordTextBox)
        Me.loginPanel.Controls.Add(Me.loginemailTextBox)
        Me.loginPanel.Location = New System.Drawing.Point(350, 159)
        Me.loginPanel.Name = "loginPanel"
        Me.loginPanel.Size = New System.Drawing.Size(655, 333)
        Me.loginPanel.TabIndex = 0
        '
        'passwordLabel
        '
        Me.passwordLabel.AutoSize = True
        Me.passwordLabel.Location = New System.Drawing.Point(76, 186)
        Me.passwordLabel.Name = "passwordLabel"
        Me.passwordLabel.Size = New System.Drawing.Size(52, 13)
        Me.passwordLabel.TabIndex = 6
        Me.passwordLabel.Text = "password"
        '
        'emailLabel
        '
        Me.emailLabel.AutoSize = True
        Me.emailLabel.Location = New System.Drawing.Point(76, 136)
        Me.emailLabel.Name = "emailLabel"
        Me.emailLabel.Size = New System.Drawing.Size(31, 13)
        Me.emailLabel.TabIndex = 5
        Me.emailLabel.Text = "email"
        '
        'loginLabel
        '
        Me.loginLabel.AutoSize = True
        Me.loginLabel.Location = New System.Drawing.Point(76, 45)
        Me.loginLabel.Name = "loginLabel"
        Me.loginLabel.Size = New System.Drawing.Size(32, 13)
        Me.loginLabel.TabIndex = 4
        Me.loginLabel.Text = "log in"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(267, 266)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(82, 13)
        Me.LinkLabel1.TabIndex = 3
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "forgot password"
        '
        'loginButton
        '
        Me.loginButton.Location = New System.Drawing.Point(178, 240)
        Me.loginButton.Name = "loginButton"
        Me.loginButton.Size = New System.Drawing.Size(257, 23)
        Me.loginButton.TabIndex = 2
        Me.loginButton.Text = "log in"
        Me.loginButton.UseVisualStyleBackColor = True
        '
        'loginpasswordTextBox
        '
        Me.loginpasswordTextBox.Location = New System.Drawing.Point(131, 179)
        Me.loginpasswordTextBox.Name = "loginpasswordTextBox"
        Me.loginpasswordTextBox.Size = New System.Drawing.Size(442, 20)
        Me.loginpasswordTextBox.TabIndex = 1
        '
        'loginemailTextBox
        '
        Me.loginemailTextBox.Location = New System.Drawing.Point(131, 129)
        Me.loginemailTextBox.Name = "loginemailTextBox"
        Me.loginemailTextBox.Size = New System.Drawing.Size(442, 20)
        Me.loginemailTextBox.TabIndex = 0
        '
        'Login_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1360, 702)
        Me.Controls.Add(Me.loginPanel)
        Me.Name = "Login_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login Form"
        Me.loginPanel.ResumeLayout(False)
        Me.loginPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents loginPanel As System.Windows.Forms.Panel
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents loginButton As System.Windows.Forms.Button
    Friend WithEvents loginpasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents loginemailTextBox As System.Windows.Forms.TextBox
    Friend WithEvents passwordLabel As System.Windows.Forms.Label
    Friend WithEvents emailLabel As System.Windows.Forms.Label
    Friend WithEvents loginLabel As System.Windows.Forms.Label

End Class
