<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_SemControl
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblSubmitted = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblTotalTeachers = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblLockStatus = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblCurrentSem = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.dgvTeachers = New System.Windows.Forms.DataGridView
        Me.btnIncrement = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.StatusStrip = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvTeachers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblSubmitted)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblTotalTeachers)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblLockStatus)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblCurrentSem)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnRefresh)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(733, 94)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GroupBox1"
        '
        'lblSubmitted
        '
        Me.lblSubmitted.AutoSize = True
        Me.lblSubmitted.Location = New System.Drawing.Point(515, 50)
        Me.lblSubmitted.Name = "lblSubmitted"
        Me.lblSubmitted.Size = New System.Drawing.Size(39, 13)
        Me.lblSubmitted.TabIndex = 13
        Me.lblSubmitted.Text = "Label8"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(515, 28)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "submitted"
        '
        'lblTotalTeachers
        '
        Me.lblTotalTeachers.AutoSize = True
        Me.lblTotalTeachers.Location = New System.Drawing.Point(411, 50)
        Me.lblTotalTeachers.Name = "lblTotalTeachers"
        Me.lblTotalTeachers.Size = New System.Drawing.Size(39, 13)
        Me.lblTotalTeachers.TabIndex = 11
        Me.lblTotalTeachers.Text = "Label6"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(411, 28)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Total teachers:"
        '
        'lblLockStatus
        '
        Me.lblLockStatus.AutoSize = True
        Me.lblLockStatus.Location = New System.Drawing.Point(264, 50)
        Me.lblLockStatus.Name = "lblLockStatus"
        Me.lblLockStatus.Size = New System.Drawing.Size(39, 13)
        Me.lblLockStatus.TabIndex = 9
        Me.lblLockStatus.Text = "Label4"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(264, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "lock status"
        '
        'lblCurrentSem
        '
        Me.lblCurrentSem.AutoSize = True
        Me.lblCurrentSem.Location = New System.Drawing.Point(17, 50)
        Me.lblCurrentSem.Name = "lblCurrentSem"
        Me.lblCurrentSem.Size = New System.Drawing.Size(39, 13)
        Me.lblCurrentSem.TabIndex = 7
        Me.lblCurrentSem.Text = "Label2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "current sem"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(652, 40)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvTeachers)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 140)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(733, 244)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GroupBox2"
        '
        'dgvTeachers
        '
        Me.dgvTeachers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTeachers.Location = New System.Drawing.Point(7, 71)
        Me.dgvTeachers.Name = "dgvTeachers"
        Me.dgvTeachers.Size = New System.Drawing.Size(720, 166)
        Me.dgvTeachers.TabIndex = 0
        '
        'btnIncrement
        '
        Me.btnIncrement.Location = New System.Drawing.Point(12, 408)
        Me.btnIncrement.Name = "btnIncrement"
        Me.btnIncrement.Size = New System.Drawing.Size(177, 23)
        Me.btnIncrement.TabIndex = 2
        Me.btnIncrement.Text = "advance to the next sem"
        Me.btnIncrement.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(195, 408)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(670, 408)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 436)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(757, 22)
        Me.StatusStrip.TabIndex = 5
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(120, 17)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(120, 17)
        Me.ToolStripStatusLabel2.Text = "ToolStripStatusLabel2"
        '
        'frm_SemControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(757, 458)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnIncrement)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frm_SemControl"
        Me.Text = "frm_SemControl"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvTeachers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnIncrement As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents dgvTeachers As System.Windows.Forms.DataGridView
    Friend WithEvents lblLockStatus As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCurrentSem As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblSubmitted As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblTotalTeachers As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
End Class
