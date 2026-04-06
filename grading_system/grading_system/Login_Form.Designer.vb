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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login_Form))
        Me.loginPanel = New System.Windows.Forms.Panel
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.showpass = New System.Windows.Forms.CheckBox
        Me.loginLabel = New System.Windows.Forms.Label
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.loginButton = New System.Windows.Forms.Button
        Me.loginpasswordTextBox = New System.Windows.Forms.TextBox
        Me.loginemailTextBox = New System.Windows.Forms.TextBox
        Me.loginPanel.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'loginPanel
        '
        Me.loginPanel.BackColor = System.Drawing.Color.Transparent
        Me.loginPanel.Controls.Add(Me.CheckBox1)
        Me.loginPanel.Controls.Add(Me.PictureBox2)
        Me.loginPanel.Controls.Add(Me.PictureBox1)
        Me.loginPanel.Controls.Add(Me.showpass)
        Me.loginPanel.Controls.Add(Me.loginLabel)
        Me.loginPanel.Controls.Add(Me.LinkLabel1)
        Me.loginPanel.Controls.Add(Me.loginButton)
        Me.loginPanel.Controls.Add(Me.loginpasswordTextBox)
        Me.loginPanel.Controls.Add(Me.loginemailTextBox)
        Me.loginPanel.Location = New System.Drawing.Point(503, 163)
        Me.loginPanel.Name = "loginPanel"
        Me.loginPanel.Size = New System.Drawing.Size(384, 406)
        Me.loginPanel.TabIndex = 0
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(335, 225)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 10
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(36, 208)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(37, 40)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(36, 143)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(37, 40)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'showpass
        '
        Me.showpass.AutoSize = True
        Me.showpass.Location = New System.Drawing.Point(579, 182)
        Me.showpass.Name = "showpass"
        Me.showpass.Size = New System.Drawing.Size(15, 14)
        Me.showpass.TabIndex = 7
        Me.showpass.UseVisualStyleBackColor = True
        '
        'loginLabel
        '
        Me.loginLabel.AutoSize = True
        Me.loginLabel.BackColor = System.Drawing.Color.Transparent
        Me.loginLabel.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.loginLabel.ForeColor = System.Drawing.Color.White
        Me.loginLabel.Location = New System.Drawing.Point(144, 60)
        Me.loginLabel.Name = "loginLabel"
        Me.loginLabel.Size = New System.Drawing.Size(89, 37)
        Me.loginLabel.TabIndex = 4
        Me.loginLabel.Text = "Login"
        Me.loginLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkColor = System.Drawing.Color.DarkCyan
        Me.LinkLabel1.Location = New System.Drawing.Point(128, 335)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(132, 21)
        Me.LinkLabel1.TabIndex = 3
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "forgot password"
        '
        'loginButton
        '
        Me.loginButton.BackColor = System.Drawing.Color.DarkCyan
        Me.loginButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.loginButton.FlatAppearance.BorderSize = 0
        Me.loginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.loginButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.loginButton.ForeColor = System.Drawing.Color.White
        Me.loginButton.Location = New System.Drawing.Point(93, 270)
        Me.loginButton.Name = "loginButton"
        Me.loginButton.Size = New System.Drawing.Size(212, 49)
        Me.loginButton.TabIndex = 2
        Me.loginButton.Text = "log in"
        Me.loginButton.UseVisualStyleBackColor = False
        '
        'loginpasswordTextBox
        '
        Me.loginpasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.loginpasswordTextBox.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.loginpasswordTextBox.Location = New System.Drawing.Point(79, 213)
        Me.loginpasswordTextBox.Multiline = True
        Me.loginpasswordTextBox.Name = "loginpasswordTextBox"
        Me.loginpasswordTextBox.Size = New System.Drawing.Size(250, 35)
        Me.loginpasswordTextBox.TabIndex = 1
        '
        'loginemailTextBox
        '
        Me.loginemailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.loginemailTextBox.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.loginemailTextBox.Location = New System.Drawing.Point(79, 148)
        Me.loginemailTextBox.Multiline = True
        Me.loginemailTextBox.Name = "loginemailTextBox"
        Me.loginemailTextBox.Size = New System.Drawing.Size(250, 35)
        Me.loginemailTextBox.TabIndex = 0
        '
        'Login_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.grading_system.My.Resources.Resources.daniel_leone_g30P1zcOzXo_unsplash
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1360, 702)
        Me.Controls.Add(Me.loginPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "Login_Form"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login Form"
        Me.loginPanel.ResumeLayout(False)
        Me.loginPanel.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents loginPanel As System.Windows.Forms.Panel
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents loginButton As System.Windows.Forms.Button
    Friend WithEvents loginpasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents loginemailTextBox As System.Windows.Forms.TextBox
    Friend WithEvents loginLabel As System.Windows.Forms.Label
    Friend WithEvents showpass As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox

End Class
