Imports System.Diagnostics

Public Class Form3
    Private Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As IntPtr) As Long

    Private Declare Auto Function FindWindow Lib "user32.dll" ( _
    ByVal lpClassName As String, _
    ByVal lpWindowName As String _
    ) As IntPtr


    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        If keyData = (Keys.Escape) Then
            'MessageBox.Show("What the Ctrl+F?")
            Me.Close()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub Form3_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Banner.Timer1.Start()

        If (Module1.pos = "PACIFIC POS") Then
            Dim process As Process
            For Each process In process.GetProcessesByName("Pacific POS")
                Dim lpClassName As String = Nothing
                Dim hwnd As IntPtr = Form3.FindWindow(lpClassName, process.MainWindowTitle)
                If (hwnd <> IntPtr.Zero) Then
                    Form3.SetForegroundWindow(hwnd)
                End If
            Next
        Else
            Dim str2 As String
            If (Module1.pos = "SMART POS") Then
                Dim process2 As Process
                For Each process2 In Process.GetProcessesByName("Store Operations")
                    str2 = Nothing
                    Dim ptr2 As IntPtr = Form3.FindWindow(str2, process2.MainWindowTitle)
                    If (ptr2 <> IntPtr.Zero) Then
                        Form3.SetForegroundWindow(ptr2)
                    End If
                Next
            ElseIf (Module1.pos = "RMS POS") Then
                Dim process3 As Process
                For Each process3 In Process.GetProcessesByName("SOPOSUSER")
                    str2 = Nothing
                    Dim ptr3 As IntPtr = Form3.FindWindow(str2, process3.MainWindowTitle)
                    If (ptr3 <> IntPtr.Zero) Then
                        Form3.SetForegroundWindow(ptr3)
                    End If
                Next
            End If
        End If

       
    End Sub
    'Private WithEvents kbHook As New KeyboardHook
    'Declare Auto Function FindWindow Lib "USER32.DLL" ( _
    'ByVal lpClassName As String, _
    'ByVal lpWindowName As String) As IntPtr

    'Declare Auto Function SetForegroundWindow Lib "USER32.DLL" _
    '(ByVal hWnd As IntPtr) As Boolean


    'Private Sub kbHook_KeyUp(ByVal Key As System.Windows.Forms.Keys) Handles kbHook.KeyUp

    '    Dim calculatorHandle As IntPtr = FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "Scan Customer Card....")
    '    'tb_scan_gor_KeyUp(Nothing, New KeyEventArgs(Key))

    '    If calculatorHandle = IntPtr.Zero Then
    '        MsgBox("Calculator is not running.")
    '        Return
    '    End If
    '    SetForegroundWindow(calculatorHandle)
    '    'SendKeys.SendWait(Key.ToString)
    'End Sub
    Private Sub Form3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub Form3_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' HookKeyboard()
        tb_scan_gor.Text = ""
        tb_scan_gor.Focus()
        Banner.Timer1.Stop()
    End Sub


    Private Sub Form3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        tb_scan_gor.Focus()
    End Sub

    Private Sub tb_scan_gor_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tb_scan_gor.KeyDown
        If e.KeyCode = Keys.Enter And tb_scan_gor.Text.Length >= 10 Then
            Form2.TB_gor_no.Text = tb_scan_gor.Text
            Me.Close()
        End If
    End Sub

End Class