Imports MySql.Data.MySqlClient
Public Class VOID
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        If keyData = (Keys.Escape) Then
            'MessageBox.Show("What the Ctrl+F?")
            Me.Close()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub TB_OR_NUMBER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TB_OR_NUMBER.KeyDown
        If e.KeyCode = Keys.Enter And TB_OR_NUMBER.Text.Length <> 0 Then
            Dim cmd As MySqlCommand
            Dim rdr As MySqlDataReader = Nothing
            Try
                mysql_connect()
                cmd = New MySqlCommand
                cmd.Connection = mysqlcon

                cmd.CommandText = "delete from tbl_transaction where or_no='" & TB_OR_NUMBER.Text & "';delete from tbl_transaction_entry where or_no='" & TB_OR_NUMBER.Text & "'"
                rdr = cmd.ExecuteReader

                'MsgBox(rdr.RecordsAffected)

                If rdr.RecordsAffected >= 1 Then
                    FormNotifier.LabelX1.Text = TB_OR_NUMBER.Text & " has been voided!"
                    rdr.Close()
                    Me.Close()
                    FormNotifier.ShowDialog()
                Else
                    FormAlert.LabelX2.Text = "No item voided. Please check OR Number " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " and try again."
                    rdr.Close()
                    Me.Close()
                    FormAlert.ShowDialog()
                End If

                rdr.Close()
                Me.Close()
            Catch ex As Exception
                rdr.Close()
                MessageBox.Show(ex.Message)
            End Try
      
        End If
    End Sub

    Private Sub VOID_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Banner.Timer1.Start()
    End Sub


    Private Sub VOID_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Banner.Timer1.Stop()
        TB_OR_NUMBER.Text = ""
    End Sub
End Class