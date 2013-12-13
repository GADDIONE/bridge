Imports System.Runtime.InteropServices
Public Class Banner
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

    Private Sub Banner_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetDesktopLocation(My.Computer.Screen.WorkingArea.Width - Me.Width - 25, 10)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        SetWindowPos(Me.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS)
    End Sub

    Private Sub PanelEx1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PanelEx1.Click
        Form2.Show()
        Form2.BringToFront()
        Form2.BringToFront()
        Form2.BringToFront()
    End Sub
End Class