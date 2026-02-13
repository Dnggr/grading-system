<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class popUpFormDeleteStudent
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.Delete_Student_Search_TextBox = New System.Windows.Forms.TextBox
        Me.Delete_Student_DataGridView = New System.Windows.Forms.DataGridView
        Me.Delete_Button = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.Delete_Student_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Cancel_Button)
        Me.Panel1.Controls.Add(Me.Delete_Student_Search_TextBox)
        Me.Panel1.Controls.Add(Me.Delete_Student_DataGridView)
        Me.Panel1.Controls.Add(Me.Delete_Button)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(-1, -1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(429, 339)
        Me.Panel1.TabIndex = 0
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Location = New System.Drawing.Point(309, 276)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(75, 23)
        Me.Cancel_Button.TabIndex = 4
        Me.Cancel_Button.Text = "Cancel"
        Me.Cancel_Button.UseVisualStyleBackColor = True
        '
        'Delete_Student_Search_TextBox
        '
        Me.Delete_Student_Search_TextBox.Location = New System.Drawing.Point(99, 60)
        Me.Delete_Student_Search_TextBox.Name = "Delete_Student_Search_TextBox"
        Me.Delete_Student_Search_TextBox.Size = New System.Drawing.Size(285, 20)
        Me.Delete_Student_Search_TextBox.TabIndex = 3
        '
        'Delete_Student_DataGridView
        '
        Me.Delete_Student_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Delete_Student_DataGridView.Location = New System.Drawing.Point(13, 99)
        Me.Delete_Student_DataGridView.Name = "Delete_Student_DataGridView"
        Me.Delete_Student_DataGridView.Size = New System.Drawing.Size(402, 151)
        Me.Delete_Student_DataGridView.TabIndex = 2
        '
        'Delete_Button
        '
        Me.Delete_Button.Location = New System.Drawing.Point(214, 276)
        Me.Delete_Button.Name = "Delete_Button"
        Me.Delete_Button.Size = New System.Drawing.Size(75, 23)
        Me.Delete_Button.TabIndex = 1
        Me.Delete_Button.Text = "Delete"
        Me.Delete_Button.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "search"
        '
        'popUpFormDeleteStudent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 337)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "popUpFormDeleteStudent"
        Me.Text = "popUpFormDeleteStudent"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Delete_Student_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Delete_Student_Search_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Delete_Student_DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Delete_Button As System.Windows.Forms.Button
End Class
