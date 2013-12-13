Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Globalization

Module Transaction_Core
    Public Function get_or_data(ByVal gor_no As String) As or_data
        Dim _data2 As New or_data
        Dim command As New SqlCommand
        Module1.sqlsrv_connect()
        If (Module1.postype = "1") Then
            command = New SqlCommand With { _
                .Connection = Module1.sqlcon, _
                .CommandText = "select top 1 receiptno,CONVERT(VARCHAR (19),DateTime,120) as DateTime,totalamount from tbl_Postransaction where receiptNo=(select MAX(receiptno) from tbl_Postransaction)" _
            }
            Dim reader As SqlDataReader = command.ExecuteReader
            Do While reader.Read
                _data2.or_amount = reader.Item("totalamount").ToString
                _data2.or_date = reader.Item("DateTime").ToString
                _data2.or_no = reader.Item("receiptno").ToString
                _data2.slip_id = (0)
            Loop
            reader.Close()
            command.Dispose()
        ElseIf (Module1.postype = "3") Then
            command = New SqlCommand With { _
                .Connection = Module1.sqlcon, _
                .CommandText = "select PaymentID,AmountDue from Payment where PaymentID=(select MAX(paymentid) from payment)" _
            }
            Dim reader2 As SqlDataReader = command.ExecuteReader
            Do While reader2.Read
                _data2.or_no = reader2.Item("PaymentID").ToString
                _data2.or_amount = reader2.Item("AmountDue").ToString
            Loop
            reader2.Close()
            command.Dispose()
            command = New SqlCommand With { _
                .Connection = Module1.sqlcon, _
                .CommandText = "select checkid,CONVERT(VARCHAR(19), GETDATE(), 120) AS checkclose,discount_type,discount FROM tbl_order_payment WHERE id = (SELECT MAX(id) FROM tbl_order_payment)" _
            }
            Dim reader3 As SqlDataReader = command.ExecuteReader
            Do While reader3.Read
                _data2.slip_id = reader3.Item("checkid").ToString
                _data2.or_date = reader3.Item("checkclose").ToString
                _data2.discount_type = (reader3.Item("discount_type").ToString)
                _data2.discount = (reader3.Item("discount").ToString)
            Loop
            reader3.Close()
            command.Dispose()
        ElseIf (Module1.postype = "4") Then
            command = New SqlCommand With { _
                .Connection = Module1.sqlcon, _
                .CommandText = "SELECT TransactionNumber, CONVERT(VARCHAR (19),[Time],120) as Time, Total  FROM   [dbo].[Transaction] where TransactionNumber=(SELECT " & ChrW(9) & "MAX(TransactionNumber) from [dbo].[Transaction])" _
            }
            Dim reader4 As SqlDataReader = command.ExecuteReader
            Do While reader4.Read
                _data2.or_date = reader4.Item("Time").ToString
                _data2.or_amount = reader4.Item("Total").ToString
                _data2.or_no = reader4.Item("TransactionNumber").ToString
                _data2.slip_id = (0)
            Loop
            reader4.Close()
            command.Dispose()
        ElseIf (Module1.postype = "5") Then
        End If
        Module1.sqlsrv_close()
        Return _data2
    End Function

    Private Sub save_transaction(ByVal cmd As MySqlCommand, ByVal tr_type As Integer, ByVal or_detail_1 As or_detail, ByVal or_master_1 As or_master)

        Dim class2 As New ErrorClass
        If (tr_type = 0) Then
            Dim command As New MySqlCommand With { _
                .Connection = Module1.mysqlcon, _
                .CommandText = "insert into tbl_transaction(gor_no,or_no,or_amount,or_date,store_id,branch_id,pos_type,purchase_type) values(@gor_no1,@or_no1,@or_amount1,@or_Date1,@store_id1,@branch_id1,@pos_type1,@purchase_type1);" _
            }
            command.Parameters.Add(New MySqlParameter("@gor_no1", MySqlDbType.VarChar))
            command.Parameters.Add(New MySqlParameter("@or_no1", MySqlDbType.VarChar))
            command.Parameters.Add(New MySqlParameter("@or_amount1", MySqlDbType.Double))
            command.Parameters.Add(New MySqlParameter("@or_date1", MySqlDbType.DateTime))
            command.Parameters.Add(New MySqlParameter("@store_id1", MySqlDbType.Int16))
            command.Parameters.Add(New MySqlParameter("@branch_id1", MySqlDbType.Int16))
            command.Parameters.Add(New MySqlParameter("@pos_type1", MySqlDbType.VarChar))
            command.Parameters.Add(New MySqlParameter("@purchase_type1", MySqlDbType.VarChar))
            command.Parameters.Item("@gor_no1").Value = or_master_1.gor_no
            command.Parameters.Item("@or_no1").Value = or_master_1.or_no
            command.Parameters.Item("@or_amount1").Value = or_master_1.or_amount
            command.Parameters.Item("@or_date1").Value = or_master_1.or_date
            command.Parameters.Item("@store_id1").Value = or_master_1.store_id
            command.Parameters.Item("@branch_id1").Value = or_master_1.branch_id
            command.Parameters.Item("@pos_type1").Value = or_master_1.pos_type
            command.Parameters.Item("@purchase_type1").Value = or_master_1.purchase_type
            command.ExecuteNonQuery()
        Else
            Dim command2 As New MySqlCommand With { _
                .Connection = Module1.mysqlcon, _
                .CommandText = "insert into tbl_transaction_entry(gor_no,or_no,item_description,purchase_type,branch_id,store_id,pos_type,or_date,qty,price,type,discount) values(@gor_no,@or_no,@item_description,@purchase_type,@branch_id,@store_id,@pos_type,@or_date,@qty,@price,@type,@discount);" _
            }
            command2.Parameters.Add(New MySqlParameter("@gor_no", MySqlDbType.VarChar))
            command2.Parameters.Add(New MySqlParameter("@or_no", MySqlDbType.VarChar))
            command2.Parameters.Add(New MySqlParameter("@item_description", MySqlDbType.VarChar))
            command2.Parameters.Add(New MySqlParameter("@purchase_type", MySqlDbType.VarChar))
            command2.Parameters.Add(New MySqlParameter("@branch_id", MySqlDbType.Int16))
            command2.Parameters.Add(New MySqlParameter("@store_id", MySqlDbType.Int16))
            command2.Parameters.Add(New MySqlParameter("@pos_type", MySqlDbType.VarChar))
            command2.Parameters.Add(New MySqlParameter("@or_date", MySqlDbType.DateTime))
            command2.Parameters.Add(New MySqlParameter("@qty", MySqlDbType.Float))
            command2.Parameters.Add(New MySqlParameter("@price", MySqlDbType.Float))
            command2.Parameters.Add(New MySqlParameter("@type", MySqlDbType.VarChar))
            command2.Parameters.Add(New MySqlParameter("@discount", MySqlDbType.Float))
            command2.Parameters.Item("@gor_no").Value = or_detail_1.gor_no
            command2.Parameters.Item("@or_no").Value = or_detail_1.or_no
            command2.Parameters.Item("@item_description").Value = or_detail_1.item_description
            command2.Parameters.Item("@purchase_type").Value = or_detail_1.purchase_type
            command2.Parameters.Item("@branch_id").Value = or_detail_1.branch_id
            command2.Parameters.Item("@store_id").Value = or_detail_1.store_id
            command2.Parameters.Item("@pos_type").Value = or_detail_1.pos_type
            command2.Parameters.Item("@or_date").Value = or_detail_1.or_date
            command2.Parameters.Item("@qty").Value = or_detail_1.qty
            command2.Parameters.Item("@price").Value = or_detail_1.price
            command2.Parameters.Item("@type").Value = or_detail_1.type
            command2.Parameters.Item("@discount").Value = or_detail_1.discount
            command2.ExecuteNonQuery()
        End If
    End Sub

    Public Function save_transaction(ByVal gor_no As String, ByVal or_no As String, ByVal or_amount As String, ByVal or_date As String, Optional ByVal slip_id As String = "0") As rs
        Dim rs2 As rs = Nothing
        Dim reader As SqlDataReader
        Dim command As New SqlCommand
        Dim cmd As New MySqlCommand
        Dim num As Integer = 0
        Dim num3 As Integer = 0
        Dim num2 As Integer = 0
        Dim num4 As Integer = 0
        Dim num5 As Integer = 0
        Dim rs As New rs
        Dim class2 As New ErrorClass
        Module1.sqlsrv_connect()
        Module1.mysql_connect()
        command.Connection = Module1.sqlcon
        cmd.Connection = Module1.mysqlcon
        If (Module1.postype = "1") Then
            Try
                If (or_no = "") Then
                    Throw New Exception("Invalid OR Number!")
                End If
                If ((gor_no = "") Or (gor_no.ToString.Length <= 9)) Then
                    Throw New Exception("Invalid GOR Number!")
                End If
                command.CommandText = String.Concat(New String() {"select ItemDescription,qty,price,TotalPerItemDiscount as discount,CAST(CASE WHEN dept = '", Module1.goldicode, "'  THEN 'GOLDILOCKS' Else 'NON-GOLDILOCKS' END AS varchar) as product from tbl_Postransaction where receiptNo='", or_no, "'"})
                reader = command.ExecuteReader
                Dim transaction As MySqlTransaction = Module1.mysqlcon.BeginTransaction
                Try
                    Do While reader.Read
                        If (reader.Item("product").ToString = "GOLDILOCKS") Then
                            num2 = 1
                        Else
                            num4 = 1
                        End If
                        Dim _detail As New or_detail
                        Dim _master As New or_master
                        _detail.gor_no = gor_no
                        _detail.or_no = or_no
                        _detail.or_date = (or_date)
                        _detail.item_description = reader.Item("ItemDescription").ToString
                        _detail.purchase_type = reader.Item("product").ToString
                        _detail.branch_id = Module1.branchid
                        _detail.store_id = Module1.storeid
                        _detail.pos_type = Module1.postype
                        _detail.qty = (reader.Item("qty").ToString)
                        _detail.price = (reader.Item("price").ToString)
                        _detail.type = (Module1.purchasetype)
                        _detail.discount = reader.Item("discount").ToString
                        Transaction_Core.save_transaction(cmd, 1, _detail, _master)
                    Loop
                    reader.Close()
                    num5 = (num2 + num4)
                    If (num5 >= 2) Then
                        Dim _detail2 As New or_detail
                        Dim _master2 As New or_master With { _
                            .gor_no = gor_no, _
                            .or_no = or_no, _
                            .or_amount = (or_amount), _
                            .or_date = (or_date), _
                            .store_id = Module1.storeid, _
                            .branch_id = Module1.branchid, _
                            .pos_type = Module1.postype, _
                            .purchase_type = "MIXED" _
                        }
                        Transaction_Core.save_transaction(cmd, 0, _detail2, _master2)
                    ElseIf (num2 = 1) Then
                        Dim _detail3 As New or_detail
                        Dim _master3 As New or_master With { _
                            .gor_no = gor_no, _
                            .or_no = or_no, _
                            .or_amount = (or_amount), _
                            .or_date = (or_date), _
                            .store_id = Module1.storeid, _
                            .branch_id = Module1.branchid, _
                            .pos_type = Module1.postype, _
                            .purchase_type = "GOLDILOCKS" _
                        }
                        Transaction_Core.save_transaction(cmd, 0, _detail3, _master3)
                    Else
                        Dim _detail4 As New or_detail
                        Dim _master4 As New or_master With { _
                            .gor_no = gor_no, _
                            .or_no = or_no, _
                            .or_amount = (or_amount), _
                            .or_date = (or_date), _
                            .store_id = Module1.storeid, _
                            .branch_id = Module1.branchid, _
                            .pos_type = Module1.postype, _
                            .purchase_type = "NON-GOLDILOCKS" _
                        }
                        Transaction_Core.save_transaction(cmd, 0, _detail4, _master4)
                    End If
                Catch exception1 As MySqlException

                    Dim exception As MySqlException = exception1
                    File.AppendAllText((Application.StartupPath & "\error.log"), ("ERROR :" & (exception.Number) & ChrW(13) & ChrW(10)))
                    File.AppendAllText((Application.StartupPath & "\error.log"), ("Description: " & exception.Message & ChrW(13) & ChrW(10)))
                    rs.stat = False
                    rs.mesage = (exception.Number)
                    Try
                        transaction.Rollback()
                    Catch exception13 As MySqlException

                        Dim exception2 As MySqlException = exception13
                        File.AppendAllText((Application.StartupPath & "\error.log"), ("ERROR :" & (exception2.Number) & ChrW(13) & ChrW(10)))
                        File.AppendAllText((Application.StartupPath & "\error.log"), ("Description: " & exception2.Message & ChrW(13) & ChrW(10)))
                        rs.stat = False
                        rs.mesage = (exception2.Number)

                    Finally
                        reader.Close()
                    End Try

                Finally
                    reader.Close()
                End Try
                transaction.Commit()
                rs.stat = True
                Return rs
            Catch exception14 As MySqlException

                Dim exception3 As MySqlException = exception14
                File.AppendAllText((Application.StartupPath & "\error.log"), ("ERROR :" & (exception3.Number) & ChrW(13) & ChrW(10)))
                File.AppendAllText((Application.StartupPath & "\error.log"), ("Description: " & exception3.Message & ChrW(13) & ChrW(10)))
                rs.stat = False
                rs.mesage = (exception3.Number)

            Catch exception15 As Exception

                Dim e As Exception = exception15
                Dim trace As New StackTrace(e, True)
                File.AppendAllText((Application.StartupPath & "\error.log"), ("ERROR :" & e.StackTrace & ChrW(13) & ChrW(10)))
                File.AppendAllText((Application.StartupPath & "\error.log"), ("Description: " & e.Message & ChrW(13) & ChrW(10)))
                Dim num12 As Integer = (trace.FrameCount - 1)
                Dim i As Integer = 0
                Do While (i <= num12)
                    Dim frame As StackFrame = trace.GetFrame(i)
                    File.AppendAllText((Application.StartupPath & "\error.log"), ("Method :" & frame.GetMethod.ToString & ChrW(13) & ChrW(10)))
                    File.AppendAllText((Application.StartupPath & "\error.log"), ("File :" & frame.GetFileName & ChrW(13) & ChrW(10)))
                    File.AppendAllText((Application.StartupPath & "\error.log"), ("Line :" & (frame.GetFileLineNumber) & ChrW(13) & ChrW(10)))
                    i += 1
                Loop
                rs.stat = False
                rs.mesage = e.Message
                rs2 = rs

                Return rs2

            Finally
                Module1.sqlsrv_close()
                Module1.mysql_close()
            End Try
        ElseIf (Module1.postype <> "2") Then
            If (Module1.postype = "3") Then
                Try
                    If (or_no = "") Then
                        Throw New Exception("Invalid OR Number!")
                    End If
                    If ((gor_no = "") Or (gor_no.ToString.Length <= 9)) Then
                        Throw New Exception("Invalid GOR Number!")
                    End If
                    Dim num9 As Integer = 0
                    Dim expression As Double = 0
                    Dim num8 As Double = 0
                    command.CommandText = ("select discount_type,discount from tbl_order_payment where checkid='" & slip_id & "' and or_date !=''")
                    Dim reader2 As SqlDataReader = command.ExecuteReader
                    Do While reader2.Read
                        num9 = (reader2.Item("discount_type").ToString)
                        expression = (reader2.Item("discount").ToString)
                    Loop
                    reader2.Close()
                    num8 = expression
                    command.CommandText = ("SELECT checkID, od.qty,od.USellingAct,p.prodlongdesc FROM OrdersDet AS od INNER JOIN Product AS p ON od.ProdID = p.ProdID where od.stat=2 and od.CheckID='" & slip_id & "'")
                    reader = command.ExecuteReader
                    Dim _detail5 As New or_detail
                    Dim _master5 As New or_master
                    Dim transaction2 As MySqlTransaction = Module1.mysqlcon.BeginTransaction
                    Try
                        _master5.gor_no = gor_no
                        _master5.or_no = or_no
                        _master5.or_amount = (or_amount)
                        _master5.or_date = (or_date)
                        _master5.store_id = Module1.storeid
                        _master5.branch_id = Module1.branchid
                        _master5.pos_type = Module1.postype
                        _master5.purchase_type = "NON-GOLDILOCKS"
                        Transaction_Core.save_transaction(cmd, 0, _detail5, _master5)
                        Do While reader.Read
                            _detail5.gor_no = gor_no
                            _detail5.or_no = or_no
                            _detail5.or_date = (or_date)
                            _detail5.item_description = reader.Item("prodlongdesc").ToString
                            _detail5.purchase_type = "NON-GOLDILOCKS"
                            _detail5.branch_id = Module1.branchid
                            _detail5.store_id = Module1.storeid
                            _detail5.pos_type = Module1.postype
                            _detail5.qty = (reader.Item("qty").ToString)
                            _detail5.price = (reader.Item("USellingAct").ToString)
                            _detail5.type = (Module1.purchasetype)
                            If (num9 = 1) Then
                                _detail5.discount = (CDbl(((Conversion.Val(reader.Item("qty").ToString) * Conversion.Val(reader.Item("USellingAct").ToString)) * Conversion.Val(expression))))
                            ElseIf (num9 = 2) Then
                                _detail5.discount = (Conversion.Val(expression))
                            Else
                                _detail5.discount = (0)
                            End If
                            Transaction_Core.save_transaction(cmd, 1, _detail5, _master5)
                        Loop
                    Catch exception16 As MySqlException

                        Dim exception5 As MySqlException = exception16
                        File.AppendAllText((Application.StartupPath & "\error.log"), ("ERROR :" & (exception5.Number) & ChrW(13) & ChrW(10)))
                        File.AppendAllText((Application.StartupPath & "\error.log"), ("Description: " & exception5.Message & ChrW(13) & ChrW(10)))
                        rs.stat = False
                        rs.mesage = (exception5.Number)
                        Try
                            transaction2.Rollback()
                        Catch exception17 As MySqlException

                            Dim exception6 As MySqlException = exception17
                            File.AppendAllText((Application.StartupPath & "\error.log"), ("ERROR :" & (exception6.Number) & ChrW(13) & ChrW(10)))
                            File.AppendAllText((Application.StartupPath & "\error.log"), ("Description: " & exception6.Message & ChrW(13) & ChrW(10)))
                            rs.stat = False
                            rs.mesage = (exception6.Number)

                        Finally
                            reader.Close()
                        End Try

                    Finally
                        reader.Close()
                    End Try
                    transaction2.Commit()
                    rs.stat = True
                    Return rs
                Catch exception18 As MySqlException

                    Dim exception7 As MySqlException = exception18
                    File.AppendAllText((Application.StartupPath & "\error.log"), ("ERROR :" & (exception7.Number) & ChrW(13) & ChrW(10)))
                    File.AppendAllText((Application.StartupPath & "\error.log"), ("Description: " & exception7.Message & ChrW(13) & ChrW(10)))
                    rs.stat = False
                    rs.mesage = (exception7.Number)

                Catch exception19 As Exception

                    Dim exception8 As Exception = exception19
                    Dim trace2 As New StackTrace(exception8, True)
                    File.AppendAllText((Application.StartupPath & "\error.log"), ("ERROR :" & exception8.StackTrace & ChrW(13) & ChrW(10)))
                    File.AppendAllText((Application.StartupPath & "\error.log"), ("Description: " & exception8.Message & ChrW(13) & ChrW(10)))
                    Dim num13 As Integer = (trace2.FrameCount - 1)
                    Dim j As Integer = 0
                    Do While (j <= num13)
                        Dim frame2 As StackFrame = trace2.GetFrame(j)
                        File.AppendAllText((Application.StartupPath & "\error.log"), ("Method :" & frame2.GetMethod.ToString & ChrW(13) & ChrW(10)))
                        File.AppendAllText((Application.StartupPath & "\error.log"), ("File :" & frame2.GetFileName & ChrW(13) & ChrW(10)))
                        File.AppendAllText((Application.StartupPath & "\error.log"), ("Line :" & (frame2.GetFileLineNumber) & ChrW(13) & ChrW(10)))
                        j += 1
                    Loop
                    rs.stat = False
                    rs.mesage = exception8.Message
                    rs2 = rs

                    Return rs2

                Finally
                    Module1.sqlsrv_close()
                    Module1.mysql_close()
                End Try
            ElseIf (Module1.postype = "4") Then
                Try
                    If (or_no = "") Then
                        Throw New Exception("Invalid OR Number!")
                    End If
                    If ((gor_no = "") Or (gor_no.ToString.Length <= 9)) Then
                        Throw New Exception("Invalid GOR Number!")
                    End If
                    command.CommandText = String.Concat(New String() {"SELECT tr.TransactionNumber,te.itemid,te.Quantity,te.price,i.description,CAST(CASE WHEN c.id = ", Module1.fuel_code, "  THEN 'FUEL' Else 'LUBES' END AS varchar) as  product FROM [Transaction] tr INNER JOIN TransactionEntry te ON tr.TransactionNumber = te.TransactionNumber inner join item i on te.ItemID = i.id INNER JOIN category c on c.id=i.categoryid where tr.TransactionNumber='", or_no, "'"})
                    reader = command.ExecuteReader
                    Dim transaction3 As MySqlTransaction = Module1.mysqlcon.BeginTransaction
                    Try
                        Do While reader.Read
                            If (reader.Item("product").ToString = "FUEL") Then
                                num = 1
                            Else
                                num3 = 1
                            End If
                            Dim _detail6 As New or_detail
                            Dim _master6 As New or_master
                            _detail6.gor_no = gor_no
                            _detail6.or_no = or_no
                            _detail6.or_date = (or_date)
                            _detail6.item_description = reader.Item("description").ToString
                            _detail6.purchase_type = reader.Item("product").ToString
                            _detail6.branch_id = Module1.branchid
                            _detail6.store_id = Module1.storeid
                            _detail6.pos_type = Module1.postype
                            _detail6.qty = (reader.Item("Quantity").ToString)
                            _detail6.price = (reader.Item("price").ToString)
                            _detail6.type = (Module1.purchasetype)
                            _detail6.discount = 0
                            Transaction_Core.save_transaction(cmd, 1, _detail6, _master6)
                        Loop
                        num5 = (num + num3)
                        If (num5 >= 2) Then
                            Dim _detail7 As New or_detail
                            Dim _master7 As New or_master With { _
                                .gor_no = gor_no, _
                                .or_no = or_no, _
                                .or_amount = (or_amount), _
                                .or_date = (or_date), _
                                .store_id = Module1.storeid, _
                                .branch_id = Module1.branchid, _
                                .pos_type = Module1.postype, _
                                .purchase_type = "MIXED" _
                            }
                            Transaction_Core.save_transaction(cmd, 0, _detail7, _master7)
                        ElseIf (num = 1) Then
                            Dim _detail8 As New or_detail
                            Dim _master8 As New or_master With { _
                                .gor_no = gor_no, _
                                .or_no = or_no, _
                                .or_amount = (or_amount), _
                                .or_date = (or_date), _
                                .store_id = Module1.storeid, _
                                .branch_id = Module1.branchid, _
                                .pos_type = Module1.postype, _
                                .purchase_type = "FUEL" _
                            }
                            Transaction_Core.save_transaction(cmd, 0, _detail8, _master8)
                        Else
                            Dim _detail9 As New or_detail
                            Dim _master9 As New or_master With { _
                                .gor_no = gor_no, _
                                .or_no = or_no, _
                                .or_amount = (or_amount), _
                                .or_date = (or_date), _
                                .store_id = Module1.storeid, _
                                .branch_id = Module1.branchid, _
                                .pos_type = Module1.postype, _
                                .purchase_type = "LUBES" _
                            }
                            Transaction_Core.save_transaction(cmd, 0, _detail9, _master9)
                        End If
                    Catch exception20 As MySqlException

                        Dim exception9 As MySqlException = exception20
                        File.AppendAllText((Application.StartupPath & "\error.log"), ("ERROR :" & (exception9.Number) & ChrW(13) & ChrW(10)))
                        File.AppendAllText((Application.StartupPath & "\error.log"), ("Description: " & exception9.Message & ChrW(13) & ChrW(10)))
                        rs.stat = False
                        rs.mesage = (exception9.Number)
                        Try
                            transaction3.Rollback()
                        Catch exception21 As MySqlException

                            Dim exception10 As MySqlException = exception21
                            File.AppendAllText((Application.StartupPath & "\error.log"), ("ERROR :" & (exception10.Number) & ChrW(13) & ChrW(10)))
                            File.AppendAllText((Application.StartupPath & "\error.log"), ("Description: " & exception10.Message & ChrW(13) & ChrW(10)))
                            rs.stat = False
                            rs.mesage = (exception10.Number)

                        Finally
                            reader.Close()
                        End Try

                    Finally
                        reader.Close()
                    End Try
                    transaction3.Commit()
                    rs.stat = True
                    Return rs
                Catch exception22 As MySqlException

                    Dim exception11 As MySqlException = exception22
                    File.AppendAllText((Application.StartupPath & "\error.log"), ("ERROR :" & (exception11.Number) & ChrW(13) & ChrW(10)))
                    File.AppendAllText((Application.StartupPath & "\error.log"), ("Description: " & exception11.Message & ChrW(13) & ChrW(10)))
                    rs.stat = False
                    rs.mesage = (exception11.Number)

                Catch exception23 As Exception

                    Dim exception12 As Exception = exception23
                    Dim trace3 As New StackTrace(exception12, True)
                    File.AppendAllText((Application.StartupPath & "\error.log"), ("ERROR :" & exception12.StackTrace & ChrW(13) & ChrW(10)))
                    File.AppendAllText((Application.StartupPath & "\error.log"), ("Description: " & exception12.Message & ChrW(13) & ChrW(10)))
                    Dim num14 As Integer = (trace3.FrameCount - 1)
                    Dim k As Integer = 0
                    Do While (k <= num14)
                        Dim frame3 As StackFrame = trace3.GetFrame(k)
                        File.AppendAllText((Application.StartupPath & "\error.log"), ("Method :" & frame3.GetMethod.ToString & ChrW(13) & ChrW(10)))
                        File.AppendAllText((Application.StartupPath & "\error.log"), ("File :" & frame3.GetFileName & ChrW(13) & ChrW(10)))
                        File.AppendAllText((Application.StartupPath & "\error.log"), ("Line :" & (frame3.GetFileLineNumber) & ChrW(13) & ChrW(10)))
                        k += 1
                    Loop
                    rs.stat = False
                    rs.mesage = exception12.Message
                    rs2 = rs

                    Return rs2

                Finally
                    Module1.sqlsrv_close()
                    Module1.mysql_close()
                End Try
            ElseIf (Module1.postype = "5") Then
            End If
        End If
        Module1.sqlsrv_close()
        Module1.mysql_close()
        Return rs2
    End Function


    ' Nested Types
    Public Class ErrorClass
        ' Fields
        Friend errormessage As String
        Friend result As Boolean
    End Class

    Public Class or_data
        ' Fields
        Friend discount As Double
        Friend discount_type As Integer
        Friend or_amount As String
        Friend or_date As String
        Friend or_no As String
        Friend slip_id As String
    End Class

    Public Class or_detail
        ' Fields
        Friend branch_id As Integer
        Friend discount As String
        Friend gor_no As String
        Friend item_description As String
        Friend or_date As DateTime
        Friend or_no As String
        Friend pos_type As String
        Friend price As Double
        Friend purchase_type As String
        Friend qty As Double
        Friend store_id As Integer
        Friend type As String
    End Class

    Public Class or_master
        ' Fields
        Friend branch_id As Integer
        Friend gor_no As String
        Friend or_amount As Double
        Friend or_date As DateTime
        Friend or_no As String
        Friend pos_type As String
        Friend purchase_type As String
        Friend store_id As Integer
    End Class

    Public Class rs
        ' Fields
        Friend mesage As String
        Friend stat As Boolean
    End Class


End Module
