
Public Class Form2

    Private Sub btn_cash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cash.Click
        purchasetype = 1
        Form3.ShowDialog()

    End Sub

    Private Sub btn_po_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_po.Click
        purchasetype = 2
        Form3.ShowDialog()
    End Sub

    Private Sub btn_card_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_card.Click
        purchasetype = 1
        Form3.ShowDialog()
    End Sub

    Private Sub btn_passenger_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_passenger.Click
        purchasetype = 3
        Form3.ShowDialog()
    End Sub

    Private Sub Form2_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Banner.Close()
        mysql_close()
        Application.Exit()
    End Sub
    Private Sub Form2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Form1.Hide()
        Try
            Dim points As New rptPoints
            points.PrintOptions.PrinterName = printername
            points.SetParameterValue("gor_number", "TESTING ONLY")
            points.SetParameterValue("or_number", "TESTING ONLY")
            points.SetParameterValue("transaction_date", "TESTING ONLY")
            points.SetParameterValue("points", "TESTING ONLY")
        Catch exception1 As Exception


            MessageBox.Show(exception1.Message)
        End Try
        Me.TB_gor_no.Text = ""

        Me.TB_gor_no.Focus()
        Me.ck_auto_save.Checked = Module1.autosave
        Me.cb_printing.Checked = Module1.enable_printing
        Me.LabelX1.Text = (Module1.branch_name & "-" & Module1.store_name)

        Banner.Show()

    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.F) Then
            'MessageBox.Show("What the Ctrl+F?")
            Configuration.ShowDialog()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub TB_gor_no_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TB_gor_no.TextChanged
        If TB_gor_no.Text.Length >= 10 Then
            Dim ans As or_data = get_or_data(TB_gor_no.Text)
            TB_or_no.Text = ans.or_no
            TB_or_date.Text = ans.or_date
            TB_or_amount.Text = ans.or_amount
            TB_order_slip.Text = ans.slip_id
            'MsgBox(autosave)
            If (ck_auto_save.Checked) Then
                btn_save.PerformClick()
                Form3.Close()
            End If

        End If
    End Sub

    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click
         Dim rs As rs = Transaction_Core.save_transaction(Me.TB_gor_no.Text, Me.TB_or_no.Text, Me.TB_or_amount.Text, Me.TB_or_date.Text, Me.TB_order_slip.Text)
        If rs.stat Then
            FormNotifier.LabelX1.Text = "Transaction Saved!<br/>" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Press '<font color=""#ED1C24"">Enter</font>' to return to " & _
            "POS Window."
            FormNotifier.ShowDialog()
            If (Me.cb_printing.Checked AndAlso Not Module1.printreceipt(Me.TB_or_no.Text, Me.TB_gor_no.Text, Me.TB_or_date.Text)) Then
                MessageBox.Show("Unable to print transaction.")
            End If
            Me.TB_gor_no.Text = ""
            Me.TB_or_amount.Text = ""
            Me.TB_or_date.Text = ""
            Me.TB_or_no.Text = ""
            Module1.sqlsrv_close()
        Else
            Me.TB_gor_no.Text = ""
            Me.TB_or_amount.Text = ""
            Me.TB_or_date.Text = ""
            Me.TB_or_no.Text = ""
            Module1.sqlsrv_close()
            If rs.mesage.ToString.ToUpper.Contains("DUPLICATE") Then
                FormAlert.LabelX2.Text = "ERROR: Invalid OR Number."
            ElseIf rs.mesage.Contains("committed or is not pending") Then
                FormAlert.LabelX2.Text = "ERROR: Unable to add the same receipt."
            Else
                FormAlert.LabelX2.Text = ("ERROR: " & rs.mesage)
            End If
            FormAlert.ShowDialog()
        End If
    End Sub



    Private Sub ButtonX1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonX1.Click
        Form4.ShowDialog()
    End Sub


    Private Sub btn_void_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_void.Click
        VOID.ShowDialog()
    End Sub
End Class