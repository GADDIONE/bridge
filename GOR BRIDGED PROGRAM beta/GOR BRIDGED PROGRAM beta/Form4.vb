Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports System.IO
Imports Ionic.Zip
Imports System.Text.RegularExpressions

Public Class Form4

    Public Shared Function MySqlEscape(ByVal usString As String) As String
        If usString Is Nothing Then
            Return Nothing
        End If
        ' SQL Encoding for MySQL Recommended here:

        ' it escapes \r, \n, \x00, \x1a, baskslash, single quotes, and double quotes
        Return Regex.Replace(usString, "[\r\n\x00\x1a\\'""]", "\$0")
    End Function

    Private Sub Form4_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Banner.Timer1.Start()
    End Sub

    Private Sub Form4_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Banner.Timer1.Stop()
        DateTimeInput1.CustomFormat = "yyyy-MM-dd"
        mysql_connect()


    End Sub

    Private Sub ButtonX1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonX1.Click
        Dim command As New MySqlCommand
        Dim reader As MySqlDataReader = Nothing
        Dim reader2 As MySqlDataReader = Nothing

        Try
            
            Module1.mysql_connect()
            command.Connection = Module1.mysqlcon
            If (Me.DateTimeInput1.Text.Length <> 0) Then
                command.CommandText = ("select gor_no,or_no,DATE_FORMAT(or_date,'%Y-%m-%d %H:%i:%S') as or_date,item_description,purchase_type,qty,price,type from tbl_transaction_entry where SUBSTR(or_date,1,10)='" & Me.DateTimeInput1.Text & "'")
                reader = command.ExecuteReader

                If reader.HasRows = False Then
                    Throw New Exception("No transaction(s) to export.")
                End If
                If File.Exists(String.Concat(New String() {My.Computer.FileSystem.SpecialDirectories.Desktop, "\", Module1.branch_name, "-", Module1.store_name, "-", Me.DateTimeInput1.Text})) Then
                    File.Delete(String.Concat(New String() {My.Computer.FileSystem.SpecialDirectories.Desktop, "\", Module1.branch_name, "-", Module1.store_name, "-", Me.DateTimeInput1.Text}))
                End If
              

                Do While reader.Read
                    Dim str As String = ""
                    str = MySqlEscape(reader("item_description").ToString)
                    File.AppendAllText(String.Concat(New String() {My.Computer.FileSystem.SpecialDirectories.Desktop, _
                                                                   "\", Module1.branch_name, "-", Module1.store_name, "-", Me.DateTimeInput1.Text}), _
                                                           String.Concat(New String() {"insert into tbl_transaction_entry(gor_no,or_no,or_date,item_description,purchase_type,branch_id,store_id,pos_type,qty,price,type) values('", reader.Item("gor_no").ToString, "','", reader.Item("or_no").ToString, "','", reader.Item("or_date").ToString, "','", str, "','", reader.Item("purchase_type").ToString, "',", (Module1.branchid), ",'", (Module1.storeid), "','", Module1.pos, "','", reader.Item("qty").ToString, "','", reader.Item("price").ToString, "','", reader.Item("type").ToString, "');"}))
                Loop
                reader.Close()
                reader2 = New MySqlCommand() With { _
                    .Connection = Module1.mysqlcon, _
                    .CommandText = ("select gor_no,or_no,or_amount,DATE_FORMAT(or_date,'%Y-%m-%d %H:%i:%S') as or_date,store_id,branch_id,pos_type,purchase_type from tbl_transaction where SUBSTR(or_date,1,10)='" & Me.DateTimeInput1.Text & "'") _
                }.ExecuteReader
                Do While reader2.Read
                    File.AppendAllText(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & Module1.branch_name & "-" & Module1.store_name & "-" & Me.DateTimeInput1.Text, "insert into tbl_transaction(gor_no,or_no,or_amount,or_date,store_id,branch_id,pos_type,purchase_type) " & _
                                       "values ('" & reader2("gor_no").ToString & "','" & reader2("or_no").ToString & "'," & _
                                       "'" & reader2("or_amount").ToString & "','" & reader2("or_date").ToString & "'," & _
                                       "'" & reader2("store_id").ToString & "', '" & reader2("branch_id").ToString & "'," & _
                                       "'" & reader2("pos_type").ToString & "', '" & reader2("purchase_type").ToString & "');")
                Loop
                reader2.Close()
                Using zip As ZipFile = New ZipFile
                    zip.Password = "UnSensored Secret1"
                    zip.Encryption = EncryptionAlgorithm.WinZipAes256
                    zip.AddFile(String.Concat(New String() {My.Computer.FileSystem.SpecialDirectories.Desktop, "\", Module1.branch_name, "-", Module1.store_name, "-", Me.DateTimeInput1.Text}), "")
                    If File.Exists(String.Concat(New String() {My.Computer.FileSystem.SpecialDirectories.Desktop, "\", Module1.branch_name, "-", Module1.store_name, "-", Me.DateTimeInput1.Text, ".zip"})) Then
                        File.Delete(String.Concat(New String() {My.Computer.FileSystem.SpecialDirectories.Desktop, "\", Module1.branch_name, "-", Module1.store_name, "-", Me.DateTimeInput1.Text, ".zip"}))
                    End If
                    zip.Save(String.Concat(New String() {My.Computer.FileSystem.SpecialDirectories.Desktop, "\", Module1.branch_name, "-", Module1.store_name, "-", Me.DateTimeInput1.Text, ".zip"}))
                End Using
                If File.Exists(String.Concat(New String() {My.Computer.FileSystem.SpecialDirectories.Desktop, "\", Module1.branch_name, "-", Module1.store_name, "-", Me.DateTimeInput1.Text})) Then
                    File.Delete(String.Concat(New String() {My.Computer.FileSystem.SpecialDirectories.Desktop, "\", Module1.branch_name, "-", Module1.store_name, "-", Me.DateTimeInput1.Text}))
                End If
                MessageBox.Show(String.Concat(New String() {My.Computer.FileSystem.SpecialDirectories.Desktop, "\", Module1.branch_name, "-", Module1.store_name, "-", Me.DateTimeInput1.Text, ".zip was successfully exported to Descktop."}))
                Me.Close()
            Else
                Interaction.MsgBox("FILE EXPORT FAILED", MsgBoxStyle.ApplicationModal, Nothing)
            End If
            reader.Close()
            reader2.Close()
        Catch exception1 As Exception
            'reader.Close()
            ' reader2.Close()
            Dim exception As Exception = exception1
            MessageBox.Show(exception.Message)
            Me.Close()
        Finally
            Try
                reader.Close()
                reader2.Close()
            Catch ex As Exception

            End Try
           
            Me.Close()
            Form2.Show()
            Form2.BringToFront()
            Form2.BringToFront()
        End Try
        Me.Close()

    End Sub
End Class