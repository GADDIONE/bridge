<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VOID
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
        Me.TB_OR_NUMBER = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.SuspendLayout()
        '
        'TB_OR_NUMBER
        '
        '
        '
        '
        Me.TB_OR_NUMBER.Border.Class = "TextBoxBorder"
        Me.TB_OR_NUMBER.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TB_OR_NUMBER.Location = New System.Drawing.Point(12, 12)
        Me.TB_OR_NUMBER.Name = "TB_OR_NUMBER"
        Me.TB_OR_NUMBER.Size = New System.Drawing.Size(529, 38)
        Me.TB_OR_NUMBER.TabIndex = 0
        Me.TB_OR_NUMBER.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TB_OR_NUMBER.WatermarkText = "Enter OR Number Here..."
        '
        'VOID
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(17.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(553, 60)
        Me.Controls.Add(Me.TB_OR_NUMBER)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "VOID"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Please enter OR number to void a transaction then press enter"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TB_OR_NUMBER As DevComponents.DotNetBar.Controls.TextBoxX
End Class
