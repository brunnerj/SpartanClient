'mht_converter.vbs

Const adSaveCreateNotExist = 1
Const adSaveCreateOverWrite = 2
Const adTypeBinary = 1
Const adTypeText = 2

Set args = WScript.Arguments

if args.Count = 0 then
WScript.Echo "Usage: [CScript | WScript] mht_converter.vbs <html file> <mht filename>"
WScript.Quit 1
end if

Set objMessage = CreateObject("CDO.Message")
objMessage.CreateMHTMLBody args.Item(0)
SaveToFile objMessage, args.Item(1)


Sub SaveToFile(Msg, Fn)
Dim Strm, Dsk
Set Strm = CreateObject("ADODB.Stream")
Strm.Type = adTypeText
Strm.Charset = "US-ASCII"
Strm.Open
Set Dsk = Msg.DataSource
Dsk.SaveToObject Strm, "_Stream"
Strm.SaveToFile Fn, adSaveCreateOverWrite
End Sub