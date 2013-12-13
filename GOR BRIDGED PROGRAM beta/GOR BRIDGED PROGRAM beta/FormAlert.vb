Imports DevComponents.DotNetBar

Public Class FormAlert

    Private Sub ButtonX1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonX1.Click
        Me.Close()
    End Sub

    Private Sub FormAlert_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Form2.SendToBack()
        Form2.SendToBack()
        Form2.SendToBack()
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        If keyData = (Keys.Control) Then
            'MessageBox.Show("What the Ctrl+F?")
            Me.Text &= "@"
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class