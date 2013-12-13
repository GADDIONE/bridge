Module mdEncryptDecrypt
    Public Function SimpleEncrypt(ByVal toEncrypt As String) As String
        Dim tempChar As String = Nothing
        Dim i As Integer = 0
        For i = 1 To toEncrypt.Length
            If System.Convert.ToInt32(toEncrypt.Chars(i - 1)) < 128 Then
                tempChar = System.Convert.ToString(System.Convert.ToInt32(toEncrypt.Chars(i - 1)) + 100)
            ElseIf System.Convert.ToInt32(toEncrypt.Chars(i - 1)) > 128 Then
                tempChar = System.Convert.ToString(System.Convert.ToInt32(toEncrypt.Chars(i - 1)) - 100)
            End If
            toEncrypt = toEncrypt.Remove(i - 1, 1).Insert(i - 1, (CChar(ChrW(tempChar))).ToString())
        Next i
        Return toEncrypt
    End Function
End Module
