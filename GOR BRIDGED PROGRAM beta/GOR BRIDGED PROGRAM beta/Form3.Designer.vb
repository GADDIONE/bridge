<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3
    Inherits DevComponents.DotNetBar.Office2007Form

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
        Me.tb_scan_gor = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'tb_scan_gor
        '
        Me.tb_scan_gor.Font = New System.Drawing.Font("Calibri", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tb_scan_gor.Location = New System.Drawing.Point(12, 12)
        Me.tb_scan_gor.MaxLength = 10
        Me.tb_scan_gor.Name = "tb_scan_gor"
        Me.tb_scan_gor.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.tb_scan_gor.Size = New System.Drawing.Size(433, 40)
        Me.tb_scan_gor.TabIndex = 0
        Me.tb_scan_gor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.tb_scan_gor.UseSystemPasswordChar = True
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 61)
        Me.Controls.Add(Me.tb_scan_gor)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form3"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scan Customer Card...."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tb_scan_gor As System.Windows.Forms.TextBox
End Class
