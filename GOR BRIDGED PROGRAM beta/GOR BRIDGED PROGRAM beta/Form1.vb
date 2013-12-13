Imports System.IO
Imports MySql.Data.MySqlClient
Imports DevComponents.DotNetBar

Public Class Form1

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (MessageBox.Show("Do you want to close this GaddiRewards Program?", "Gaddi Rewards", MessageBoxButtons.YesNo) = DialogResult.No) Then
            e.Cancel = True
        End If

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'sqlsrv_connect()

        If retrieve_connection() = False Then
            Configuration.ShowDialog()
        End If
        loadconfig()
        'Dim rptdoc As New CrystalReport1
        'Me.Hide()
        'Form2.ShowDialog()

    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.F) Then
            'MessageBox.Show("What the Ctrl+F?")
            Configuration.ShowDialog()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub TextBoxX1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxX1.KeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                Module1.loadconfig()
                Dim command2 As New MySqlCommand
                Dim command As New MySqlCommand With { _
                    .Connection = Module1.mysqlcon _
                }
                command2.Connection = Module1.mysqlcon
                command.CommandText = ("select COUNT(emp_id) as count,first_name,gor_no from tbl_account_gor where gor_no='" & Me.TextBoxX1.Text & "'")
                Dim reader As MySqlDataReader = command.ExecuteReader
                Do While reader.Read
                    If ((reader.Item("count").ToString) > 0) Then
                        Form2.lbl_cashier.Text = reader.Item("first_name").ToString
                        Form2.lbl_userid.Text = reader.Item("gor_no").ToString
                        reader.Close()
                        command2.CommandText = String.Concat(New String() {"insert into tbl_logs(userid,time,type,branch_id,store_id) values('", Me.TextBoxX1.Text, "',NOW(),'LOGIN','", (Module1.branchid), "','", (Module1.storeid), "');"})
                        command2.ExecuteNonQuery()
                        Form2.ShowDialog()
                        Me.Hide()
                    Else
                        reader.Close()
                        FormAlert.LabelX2.Text = "ERROR: Login Failed."
                        FormAlert.ShowDialog()
                    End If
                Loop
                reader.Close()
                reader.Dispose()
            End If
            Module1.mysql_close()
        Catch exception1 As Exception

            Dim exception As Exception = exception1
            Module1.mysql_close()
            MessageBox.Show(exception.Message)
        Finally
            Module1.mysql_close()
        End Try


    End Sub


End Class
