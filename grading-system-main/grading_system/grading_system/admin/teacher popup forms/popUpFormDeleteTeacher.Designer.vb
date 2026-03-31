<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class popUpFormDeleteTeacher
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
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.Delete_Teacher_Search_TextBox = New System.Windows.Forms.TextBox
        Me.Delete_Teacher_DataGridView = New System.Windows.Forms.DataGridView
        Me.Label1 = New System.Windows.Forms.Label
        Me.Delete_Button = New System.Windows.Forms.Button
        Me.Refresh_Button = New System.Windows.Forms.Button
        Me.Activate_Button = New System.Windows.Forms.Button
        CType(Me.Delete_Teacher_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Location = New System.Drawing.Point(306, 263)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(75, 23)
        Me.Cancel_Button.TabIndex = 9
        Me.Cancel_Button.Text = "Cancel"
        Me.Cancel_Button.UseVisualStyleBackColor = True
        '
        'Delete_Teacher_Search_TextBox
        '
        Me.Delete_Teacher_Search_TextBox.Location = New System.Drawing.Point(96, 47)
        Me.Delete_Teacher_Search_TextBox.Name = "Delete_Teacher_Search_TextBox"
        Me.Delete_Teacher_Search_TextBox.Size = New System.Drawing.Size(285, 20)
        Me.Delete_Teacher_Search_TextBox.TabIndex = 8
        '
        'Delete_Teacher_DataGridView
        '
        Me.Delete_Teacher_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Delete_Teacher_DataGridView.Location = New System.Drawing.Point(10, 86)
        Me.Delete_Teacher_DataGridView.Name = "Delete_Teacher_DataGridView"
        Me.Delete_Teacher_DataGridView.Size = New System.Drawing.Size(402, 151)
        Me.Delete_Teacher_DataGridView.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "search"
        '
        'Delete_Button
        '
        Me.Delete_Button.Location = New System.Drawing.Point(225, 263)
        Me.Delete_Button.Name = "Delete_Button"
        Me.Delete_Button.Size = New System.Drawing.Size(75, 23)
        Me.Delete_Button.TabIndex = 6
        Me.Delete_Button.Text = "inactive"
        Me.Delete_Button.UseVisualStyleBackColor = True
        '
        'Refresh_Button
        '
        Me.Refresh_Button.Location = New System.Drawing.Point(10, 243)
        Me.Refresh_Button.Name = "Refresh_Button"
        Me.Refresh_Button.Size = New System.Drawing.Size(75, 23)
        Me.Refresh_Button.TabIndex = 10
        Me.Refresh_Button.Text = "refresh"
        Me.Refresh_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Refresh_Button.UseVisualStyleBackColor = True
        '
        'Activate_Button
        '
        Me.Activate_Button.Location = New System.Drawing.Point(144, 263)
        Me.Activate_Button.Name = "Activate_Button"
        Me.Activate_Button.Size = New System.Drawing.Size(75, 23)
        Me.Activate_Button.TabIndex = 11
        Me.Activate_Button.Text = "active"
        Me.Activate_Button.UseVisualStyleBackColor = True
        '
        'popUpFormDeleteTeacher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 337)
        Me.Controls.Add(Me.Activate_Button)
        Me.Controls.Add(Me.Refresh_Button)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.Delete_Teacher_Search_TextBox)
        Me.Controls.Add(Me.Delete_Teacher_DataGridView)
        Me.Controls.Add(Me.Delete_Button)
        Me.Controls.Add(Me.Label1)
        Me.Name = "popUpFormDeleteTeacher"
        Me.Text = "popUpFormDeleteTeacher"
        CType(Me.Delete_Teacher_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Delete_Teacher_Search_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Delete_Teacher_DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Delete_Button As System.Windows.Forms.Button
    Friend WithEvents Refresh_Button As System.Windows.Forms.Button
    Friend WithEvents Activate_Button As System.Windows.Forms.Button
End Class
