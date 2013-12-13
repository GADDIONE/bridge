Imports DevComponents.DotNetBar
Imports System.Runtime.InteropServices
Public Class FormNotifier
    Shared ReadOnly HWND_TOPMOST As New IntPtr(-1)
    Shared ReadOnly HWND_NOTOPMOST As New IntPtr(-2)
    Shared ReadOnly HWND_TOP As New IntPtr(0)
    Shared ReadOnly HWND_BOTTOM As New IntPtr(1)
    Const SWP_NOSIZE As UInt32 = &H1
    Const SWP_NOMOVE As UInt32 = &H2
    Const TOPMOST_FLAGS As UInt32 = SWP_NOMOVE Or SWP_NOSIZE
    Declare Function GetKeyState Lib "user32" Alias "GetKeyState" (ByVal ByValnVirtKey As Int32) As Int16

    <DllImport("user32.dll")> _
    Public Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, _
   ByVal uFlags As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    Private Sub ButtonX1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonX1.Click
        Me.Close()
    End Sub

    Private Sub FormNotifier_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Form2.SendToBack()
        Form2.SendToBack()
        Form2.SendToBack()
    End Sub

    Private Sub FormNotifier_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'SetWindowPos(Form2.Handle, HWND_BOTTOM, 0, 0, 0, 0, TOPMOST_FLAGS)
        Form3.Close()
        Form2.SendToBack()
        Form2.SendToBack()
        Form2.SendToBack()
    End Sub
End Class