VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "siwutil32_class"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Dim WithEvents win As HTMLWindow2
Attribute win.VB_VarHelpID = -1
Dim printed As Boolean

' This hooks in the WindowProc above and sets the old window proc pointer.
Public Sub HookWindowProc(hwnd As Long)
        
    pOldProc = SetWindowLong(hwnd, GWL_WNDPROC, AddressOf WindowProc)

End Sub
'This unhooks the WindowProc above and sets the old window proc back
Public Sub UnHookWindowProc(hwnd As Long)
    Dim retval As Long
    
    retval = SetWindowLong(hwnd, GWL_WNDPROC, pOldProc)

End Sub

Public Sub PrintWebBrowser(ByVal pDisp As Object)
   
    printed = False
    
    Set win = pDisp.document.parentWindow
    pDisp.ExecWB OLECMDID_PRINT, OLECMDEXECOPT_PROMPTUSER, Empty, Empty
    
    While Not printed
        DoEvents
    Wend
End Sub

' This function checks a database's linked tables
' against a root path passed in as a string parameter.
' Linked tables may get their links changed if the
' corresponding database and tables exist in the
' location according to the root parameter.
' Errors are indicated with the LONG return value.
' A value of 0 indicates no errors.
Public Function FixDBLinks(ByVal ROOT As String, _
                           ByVal ctrlDB As String, _
                           ByVal wrkgrpDB As String, _
                           ByVal uid As String, _
                           ByVal pwd As String) As Long
    
    Dim dbctrl As Database, dblink As Database
    Dim tdctrl As TableDef, tdlink As TableDef
        
    Dim lROOT As String
    Dim curlinkFile As String
    Dim newlinkFile As String
    Dim NewPath As Boolean
    Dim LinkFileFound As Boolean
    Dim TableFound As Boolean
    
    DefaultUser = uid
    DefaultPassword = pwd
    lROOT = ROOT & "\"    ' append backslash
        
    
    SystemDB = lROOT & wrkgrpDB     ' Set default system database
        
    On Error Resume Next            ' Trap errors following all calls.
                                    ' Errors will be returned numerically
                                    ' 0 = 'no error'
    
    Set dbctrl = OpenDatabase(lROOT & ctrlDB, True, False, "")
    If Err.Number <> 0 Then
        FixDBLinks = -1             ' Error = -1:  error opening DB or MDW
        Exit Function
    End If
    
    For Each tdctrl In dbctrl.TableDefs     ' Process each of the tables in the database
        
        If Len(tdctrl.Connect) > 0 Then    ' Is this a linked table ?
        
            curlinkFile = Mid(tdctrl.Connect, 11)       ' Get path to existing link
            newlinkFile = Mid(curlinkFile, _
                              InStrRev(curlinkFile, _
                                       "\", -1, vbTextCompare) + 1) ' Trim filename
                                       
            newlinkFile = lROOT & newlinkFile ' Build new path to linked file
        
            ' Check if new file link exists
            LinkFileFound = (Len(Dir(newlinkFile)) <> 0)
            
            ' and check if it's linked to a new path?
            NewPath = (StrComp(curlinkFile, newlinkFile, vbTextCompare) <> 0)
            
            If LinkFileFound And NewPath Then
                
                Set dblink = OpenDatabase(newlinkFile, True, False, "")
                If Err.Number <> 0 Then
                    FixDBLinks = -3             ' Error = -3:  error opening linked DB
                    Exit Function
                End If
                
                TableFound = False
                
                For Each tdlink In dblink.TableDefs   ' For each table in the linked DB
                    If tdlink.Name = tdctrl.Name Then ' See if it's name equals the linked table name
                        tdctrl.Connect = ";DATABASE=" & newlinkFile ' Set the new link path
                        tdctrl.RefreshLink                          ' and refresh the link
                        TableFound = True
                    End If
                Next
                
                dblink.Close    ' close the linked database
                
                If Not TableFound Then
                    FixDBLinks = -4             ' Error = -4:  error finding table in linked DB
                    Exit Function
                End If
            
            End If    'NewPath and LinkFileFound
    
        End If  ' not a linked table
        
    Next         ' get next table in database
    
    dbctrl.Close ' close the control database
    
    FixDBLinks = 0  ' no errors - return 0
    
End Function

Private Sub win_onafterprint()
    printed = True
End Sub

Private Sub Class_Terminate()
    On Error Resume Next
End Sub
