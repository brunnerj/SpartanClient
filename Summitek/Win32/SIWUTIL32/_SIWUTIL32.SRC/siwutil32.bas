Attribute VB_Name = "siwutil32_module"
Public Declare Function SetWindowLong Lib "user32.dll" Alias "SetWindowLongA" (ByVal hwnd As Long, _
    ByVal nIndex As Long, ByVal dwNewLong As Long) As Long
Public Declare Function CallWindowProc Lib "user32.dll" Alias "CallWindowProcA" (ByVal lpPrevWndFunc _
    As Long, ByVal hwnd As Long, ByVal Msg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long

Private Declare Function SetForegroundWindow Lib "user32.dll" (ByVal hwnd As Long) As Long
Private Declare Function ShowWindow Lib "user32.dll" (ByVal hwnd As Long, ByVal nCmdShow As Long) As Long
Private Declare Function IsIconic Lib "user32.dll" (ByVal hwnd As Long) As Long

Public pOldProc As Long
Public Const GWL_WNDPROC = -4

Private Const SW_SHOWNORMAL = 1
Private Const SW_SHOW = 5
Private Const SW_RESTORE = 9
Private Const WM_LBUTTONDBLCLK = &H203


Private Const PIM_TRAY_ICON = &H401  ' This is defined in pimhw.vi

' Define the new window procedure.
Public Function WindowProc(ByVal hwnd As Long, ByVal uMsg As Long, ByVal wParam As Long, _
         ByVal lParam As Long) As Long
         
    Dim retval As Long
    Static called As Boolean
    
'    If Not called Then
'        MsgBox "WindowProc is hooked!", , "Message"
'   End If
    
    called = True
    
    Select Case uMsg
        Case PIM_TRAY_ICON
        
            ' If user double clicks our PIM HW icon, then restore the window
            If lParam = WM_LBUTTONDBLCLK Then
                retval = ShowWindow(hwnd, SW_SHOW)  ' display the window if it's hidden
                If IsIconic(hwnd) Then
                   retval = ShowWindow(hwnd, SW_RESTORE) ' restore the window
                Else
                   retval = ShowWindow(hwnd, SW_SHOWNORMAL) ' show and activate
                   retval = SetForegroundWindow(hwnd) ' set foreground
                End If
            End If
        
            WindowProc = 1  ' this return value doesn't really matter
        
        Case Else
            retval = CallWindowProc(pOldProc, hwnd, uMsg, wParam, lParam)
            ' Have this function return whatever the function above returned.
            WindowProc = retval
    End Select
End Function
