Imports System.IO
Imports System.Data.SqlClient
Imports DevComponents.DotNetBar
Imports MySql.Data.MySqlClient
Imports System.Data.SQLite
Imports System.Drawing.Printing

Public Class Configuration


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            If ((Me.SuperTabControl1.SelectedTabIndex.ToString) = 0) Then
                If (Me.ComboBoxEx1.SelectedItem.ToString = "Windows Authentication") Then
                    Module1.sqlcon = New SqlConnection(String.Concat(New String() {"Initial Catalog=", Me.TB_SQL_DBNAME.Text, ";Data Source=", Me.TB_SQLIP.Text, ";Integrated Security=SSPI;"}))
                    If Module1.sqlsrv_connect Then
                        MessageBoxEx.Show("Connection successful!")
                    Else
                        MessageBoxEx.Show("Connection failed!")
                    End If
                Else
                    Module1.sqlcon = New SqlConnection(String.Concat(New String() {"Server=", Me.TB_SQLIP.Text, ";Database=", Me.TB_SQL_DBNAME.Text, ";User Id=", Me.TB_SQLUSERID.Text, ";Password=", Me.TB_SQLPASSWORD.Text, ";"}))
                    If Module1.sqlsrv_connect Then
                        MessageBoxEx.Show("Connection successful!")
                    Else
                        MessageBoxEx.Show("Connection failed!")
                    End If
                End If
            ElseIf ((Me.SuperTabControl1.SelectedTabIndex.ToString) = 1) Then
                Module1.mysqlcon = New MySqlConnection(String.Concat(New String() {"Server=", Me.TB_MYSQLIP.Text, ";Database='", Me.TB_MYSQLDBNAME.Text, "';Userid='", Me.TB_MYSQLUSERID.Text, "';Password='", Me.TB_MYSQLPASSWORD.Text, "';Port='", Me.TB_MYSQLPORT.Text, "'"}))
                If Module1.mysql_connect Then
                    MessageBoxEx.Show("MySql Connection successful!")
                Else
                    MessageBoxEx.Show("MySql Connection failed!")
                End If
            ElseIf ((Me.SuperTabControl1.SelectedTabIndex.ToString) = 2) Then
                Dim rptdoc As New rptPoints
                rptdoc.PrintOptions.PrinterName = CB_printers.SelectedItem.ToString
                rptdoc.PrintToPrinter(1, False, 1, 1)
            End If
        Catch exception1 As Exception

            Dim exception As Exception = exception1
            MessageBoxEx.Show(exception.Message)

        End Try

        

    End Sub

    Private Sub ComboBoxEx1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxEx1.SelectedIndexChanged
        If (Me.ComboBoxEx1.SelectedItem.ToString = "Windows Authentication") Then
            Me.TB_SQLPASSWORD.Enabled = False
            Me.TB_SQLUSERID.Enabled = False
        Else
            Me.TB_SQLPASSWORD.Enabled = True
            Me.TB_SQLUSERID.Enabled = True
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
      If (Me.SuperTabControl1.SelectedTabIndex = 0) Then
            Dim command As New SQLiteCommand With { _
                .Connection = Module1.sqlitecon, _
                .CommandText = "delete from tbl_sqlserver_config" _
            }
            command.ExecuteNonQuery()
            command.CommandText = String.Concat(New String() {"insert into tbl_sqlserver_config(identifier,host,dbname,userid,password) values('", Me.ComboBoxEx1.SelectedIndex, "','", Me.TB_SQLIP.Text, "','", Me.TB_SQL_DBNAME.Text, "','", Me.TB_SQLUSERID.Text, "','", Me.TB_SQLPASSWORD.Text, "')"})
            Dim reader As SQLiteDataReader = command.ExecuteReader
            If (reader.RecordsAffected > 0) Then
                MessageBox.Show("Sql Server Configuration saved!")
            End If
            reader.Close()
        ElseIf (Me.SuperTabControl1.SelectedTabIndex = 1) Then
            Dim command2 As New SQLiteCommand With { _
                .Connection = Module1.sqlitecon, _
                .CommandText = "delete from tbl_mysql_config" _
            }
            command2.ExecuteNonQuery()
            command2.CommandText = String.Concat(New String() {"insert into tbl_mysql_config(host,dbname,username,password,port) values('", Me.TB_MYSQLIP.Text, "','", Me.TB_MYSQLDBNAME.Text, "','", Me.TB_MYSQLUSERID.Text, "','", Me.TB_MYSQLPASSWORD.Text, "','", Me.TB_MYSQLPORT.Text, "')"})
            Dim reader2 As SQLiteDataReader = command2.ExecuteReader
            If (reader2.RecordsAffected > 0) Then
                MessageBox.Show("MySql Server Configuration saved!")
            End If
            reader2.Close()
        ElseIf (Me.SuperTabControl1.SelectedTabIndex = 2) Then
            Dim command3 As New SQLiteCommand With { _
                .Connection = Module1.sqlitecon, _
                .CommandText = "delete from tbl_store_config" _
            }
            command3.ExecuteNonQuery()
            'command3.CommandText = String.Concat(New String() {"insert into tbl_store_config(branch_id,store_id,pos_id,printer,goldi_code,fuel_code) values('", Me.cb_branch_id.SelectedItem.ToString, "','", Me.cb_store_id.SelectedItem.ToString, "','", Me.cb_pos_type.SelectedItem.ToString, "','", Me.CB_printers.SelectedItem.ToString, "','", Me.TB_goldi_code.Text, "','", Me.TB_fuel_code.Text, "')"})
            'Dim reader3 As SQLiteDataReader = command3.ExecuteReader
            'If (reader3.RecordsAffected > 0) Then
            '    MessageBox.Show("Store Configuration saved!")
            'End If
            'reader3.Close()
            command3.CommandText = "insert into tbl_store_config(branch_id,store_id,pos_id,printer,goldi_code,fuel_code) values(@branch_id,@store_id,@pos_id,@printer,@goldi_code,@fuel_code);"
            command3.Parameters.AddWithValue("@branch_id", Me.cb_branch_id.SelectedItem.ToString)
            command3.Parameters.AddWithValue("@store_id", Me.cb_store_id.SelectedItem.ToString)
            command3.Parameters.AddWithValue("@pos_id", Me.cb_pos_type.SelectedItem.ToString)
            command3.Parameters.AddWithValue("@printer", Me.CB_printers.SelectedItem.ToString)
            command3.Parameters.AddWithValue("@goldi_code", Me.TB_goldi_code.Text)
            command3.Parameters.AddWithValue("@fuel_code", Me.TB_fuel_code.Text)
            Dim reader3 As SQLiteDataReader = command3.ExecuteReader
            If (reader3.RecordsAffected > 0) Then
                MessageBox.Show("Store Configuration saved!")
            End If
            reader3.Close()
        Else
            MessageBox.Show("Undefined tab index!")
        End If


    End Sub

    Private Sub Configuration_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Banner.Timer1.Start()
        sqlsrv_close()
        mysql_close()
        retrieve_connection()
        Me.Close()
        'Application.Exit()

    End Sub

    Private Sub Configuration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '        foreach (string printname in PrinterSettings.InstalledPrinters)
        '{
        'listBox1.Items.Add(printname);
        '}

        For Each printname In Printing.PrinterSettings.InstalledPrinters
            CB_printers.Items.Add(printname)
        Next

        Me.ComboBoxEx1.SelectedIndex = Module1.msauth
        Me.TB_SQLIP.Text = Module1.mshost
        Me.TB_SQL_DBNAME.Text = Module1.msdb
        Me.TB_SQLUSERID.Text = Module1.msuserid
        Me.TB_SQLPASSWORD.Text = Module1.mspass
        Me.TB_MYSQLIP.Text = Module1.myhost
        Me.TB_MYSQLDBNAME.Text = Module1.mydb
        Me.TB_MYSQLUSERID.Text = Module1.myuserid
        Me.TB_MYSQLPASSWORD.Text = Module1.mypass
        Me.TB_MYSQLPORT.Text = Module1.myport
        Dim command As New SQLiteCommand With { _
            .Connection = Module1.sqlitecon, _
            .CommandText = "select * from tbl_stores" _
        }
        Dim reader As SQLiteDataReader = command.ExecuteReader
        Do While reader.Read
            Me.cb_store_id.Items.Add(reader.Item(1).ToString)
        Loop
        reader.Close()
        command.CommandText = "select * from tbl_branch"
        reader = command.ExecuteReader
        Do While reader.Read
            Me.cb_branch_id.Items.Add(reader.Item(1).ToString)
        Loop
        reader.Close()
        command.CommandText = "select * from tbl_pos"
        reader = command.ExecuteReader
        Do While reader.Read
            Me.cb_pos_type.Items.Add(reader.Item(1).ToString)
        Loop
        reader.Close()
        Try
            Me.cb_branch_id.SelectedItem = Module1.branch_name
            Me.cb_store_id.SelectedItem = Module1.store_name
            Me.cb_pos_type.SelectedItem = Module1.pos
            Me.CB_printers.SelectedItem = Module1.printername
            Me.TB_fuel_code.Text = Module1.fuel_code
            Me.TB_goldi_code.Text = Module1.goldicode
        Catch exception1 As Exception

            Dim exception As Exception = exception1

        End Try


    End Sub
End Class