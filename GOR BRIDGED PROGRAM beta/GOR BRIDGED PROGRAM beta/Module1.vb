Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.SQLite

Module Module1
    Public Sub loadconfig()
        If (Module1.msauth.ToString = "0") Then
            Module1.sqlcon = New SqlConnection(String.Concat(New String() {"Data Source=", Module1.mshost, ";Database=", Module1.msdb, ";Trusted_Connection=True;"}))
            If Not Module1.sqlsrv_connect Then
                MessageBox.Show("Unable to connect to MSSQL Server.")
            End If
        Else
            Module1.sqlcon = New SqlConnection(String.Concat(New String() {"Server=", Module1.mshost, ";Database=", Module1.msdb, ";User Id=", Module1.msuserid, ";Password=", Module1.mspass, ";"}))
            If Not Module1.sqlsrv_connect Then
                MessageBox.Show("Unable to connect to MSSQL Server.")
            End If
        End If
        Module1.mysqlcon = New MySqlConnection(String.Concat(New String() {"Server='", Module1.myhost, "';Database='", Module1.mydb, "';Userid='", Module1.myuserid, "';Password='", Module1.mypass, "';Port='", (Module1.myport), "';"}))
        If Not Module1.mysql_connect Then
            MessageBox.Show("Unable to connect to MySql Server.")
        End If
    End Sub

    Public Sub mysql_close()
        If (Module1.mysqlcon.State = ConnectionState.Open) Then
            Module1.mysqlcon.Close()
        End If
    End Sub

    Public Function mysql_connect() As Boolean
        Dim flag As Boolean
        Try
            If (Module1.mysqlcon.State = ConnectionState.Closed) Then
                Module1.mysqlcon.Open()
                Return True
            End If
        Catch exception1 As Exception

            Dim exception As Exception = exception1
            flag = False

            Return flag

        End Try
        Return flag
    End Function

    Public Function printreceipt(ByVal or_number As String, ByVal gor_number As String, ByVal transaction_date As String) As Boolean
        Dim flag As Boolean
        Dim points As New rptPoints
        points.PrintOptions.PrinterName = printername

        Try
            Dim command As New MySqlCommand
            Module1.mysql_connect()
            Dim reader As MySqlDataReader = New MySqlCommand() With { _
                .Connection = Module1.mysqlcon, _
                .CommandText = String.Concat(New String() {"select ROUND(SUM(Case purchase_type when 'FUEL' THEN qty when 'LUBES' THEN (qty* price)/100 when 'GOLDILOCKS' THEN ((qty*price)-discount)/20 when 'NON-GOLDILOCKS' THEN ((qty*price)-discount)/10 end ),2) as points from tbl_transaction_entry where gor_no='", gor_number, "' and or_no='", or_number, "'"}) _
            }.ExecuteReader
            Do While reader.Read
                points.SetParameterValue("gor_number", gor_number)
                points.SetParameterValue("or_number", or_number)
                points.SetParameterValue("transaction_date", transaction_date)
                points.SetParameterValue("points", reader.Item("points").ToString)
            Loop
            reader.Close()
            points.PrintToPrinter(1, False, 1, 1)
            Module1.mysql_close()
            flag = True
        Catch exception1 As Exception

            Dim exception As Exception = exception1
            Module1.mysql_close()
            flag = False

            Return flag

        End Try
        Return flag
    End Function

    Public Function retrieve_connection() As Boolean
        Dim flag4 As Boolean
        Dim flag As Boolean = False
        Dim flag2 As Boolean = False
        Dim flag3 As Boolean = False
        Try
            If (Module1.sqlitecon.State = ConnectionState.Closed) Then
                Module1.sqlitecon.Open()
                Dim command As New SQLiteCommand
                Dim command2 As New SQLiteCommand
                command.Connection = Module1.sqlitecon
                command.CommandText = "select MAX(id),* from tbl_sqlserver_config"
                Dim reader As SQLiteDataReader = command.ExecuteReader
                If reader.HasRows Then
                    flag = True
                Else
                    flag = False
                End If
                Do While reader.Read
                    Module1.msauth = (reader.Item("identifier").ToString)
                    Module1.mshost = reader.Item("host").ToString
                    Module1.msuserid = reader.Item("userid").ToString
                    Module1.mspass = reader.Item("password").ToString
                    Module1.msdb = reader.Item("dbname").ToString
                Loop
                reader.Close()
                command2.Connection = Module1.sqlitecon
                command2.CommandText = "select MAX(id),* from tbl_mysql_config"
                reader = command2.ExecuteReader
                If reader.HasRows Then
                    flag2 = True
                Else
                    flag2 = False
                End If
                Do While reader.Read
                    Module1.myhost = reader.Item("host").ToString
                    Module1.myuserid = reader.Item("username").ToString
                    Module1.mypass = reader.Item("password").ToString
                    Module1.mydb = reader.Item("dbname").ToString
                    Module1.myport = (reader.Item("port").ToString)
                Loop
                reader.Close()
                command.CommandText = "SELECT" & ChrW(9) & "MAX(p.id),p.id as pos_id,s.id AS store_id,s.description AS store_name,p.postype,b.id AS branch_id,b.description AS branch_name,c.auto_save_transaction,c.enable_printing,c.printer,c.goldi_code,c.fuel_code FROM  tbl_store_config c INNER JOIN tbl_pos p ON p.postype = c.pos_id INNER JOIN tbl_stores s ON c.store_id = s.description INNER JOIN tbl_branch b ON c.branch_id = b.description"
                reader = command.ExecuteReader
                If reader.HasRows Then
                    flag3 = True
                Else
                    flag3 = False
                End If
                Do While reader.Read
                    Module1.branchid = (reader.Item("branch_id").ToString)
                    Module1.storeid = (reader.Item("store_id").ToString)
                    Module1.postype = reader.Item("pos_id").ToString
                    Module1.branch_name = reader.Item("branch_name").ToString
                    Module1.store_name = reader.Item("store_name").ToString
                    Module1.pos = reader.Item("postype").ToString
                    Module1.enable_printing = (reader.Item("enable_printing").ToString)
                    Module1.autosave = (reader.Item("auto_save_transaction").ToString)
                    Module1.printername = reader.Item("printer").ToString
                    Module1.goldicode = reader.Item("goldi_code").ToString
                    Module1.fuel_code = reader.Item("fuel_code").ToString
                Loop
            End If
            flag4 = ((flag And flag2) And flag3)
        Catch exception1 As Exception

            Dim exception As Exception = exception1
            MessageBox.Show(exception.Message)

        End Try
        Return flag4
    End Function

    Public Sub sqlsrv_close()
        If (Module1.sqlcon.State = ConnectionState.Open) Then
            Module1.sqlcon.Close()
        End If
    End Sub

    Public Function sqlsrv_connect() As Boolean
        Dim flag As Boolean
        Try
            If (Module1.sqlcon.State = ConnectionState.Closed) Then
                Module1.sqlcon.Open()
                Return True
            End If
        Catch exception1 As Exception

            Dim exception As Exception = exception1
            flag = False

            Return flag

        End Try
        Return flag
    End Function


    ' Fields
    Public autosave As Boolean = False
    Public branch_name As String = ""
    Public branchid As Integer = 0
    Public enable_printing As Boolean = False
    Public fuel_code As String = "11"
    Public goldicode As String = "GOLDILOCKS"
    Public msauth As Integer = 0
    Public msdb As String = ""
    Public mshost As String = ""
    Public mspass As String = ""
    Public msuserid As String = ""
    Public mydb As String = ""
    Public myhost As String = ""
    Public mypass As String = ""
    Public myport As Integer = &HCEA
    Public mysqlcon As MySqlConnection = New MySqlConnection("Server=127.0.0.1;Database=;Userid=root;Password=")
    Public myuserid As String = ""
    Public pos As String = ""
    Public postype As String = ""
    Public printername As String = ""
    Public purchasetype As Integer = 0
    Public sqlcon As SqlConnection = New SqlConnection("Data Source=.\SQLExpress;Database=;Trusted_Connection=True;")
    Public sqlitecon As SQLiteConnection = New SQLiteConnection(("Data Source=" & Application.StartupPath & "\config.db;compress=true;Version=3;"))
    Public store_name As String = ""
    Public storeid As Integer = 0

End Module
