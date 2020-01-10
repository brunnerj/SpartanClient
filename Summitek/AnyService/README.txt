This DLL can be used to run your LabVIEW executable as a Windows service.

The code (.c) and header (.h) files found in this folder are for reference -
they show how the code against which the "AnyService.dll" was compiled.

Here's how to use this library...

  1)  Create a DLL called "server.dll" from two LabVIEW VI's.  Call them
      "BeginService.vi" and "EndService.vi".  These will be the entry
      points to your service.  In the simple case, the "BeginService.vi"
      can contain your entire program, but it must listen for a stop
      command (which will be issued by the "EndService.vi"), and exit
      cleanly.  Both of these VI's should have a simple I32 input/output
      wired to the front panel.  Call them "In" and "Out".

  2)  Create a VI to dispatch (start) your service.  This VI needs do
      nothing more than call DLL "AnyService.dll" (call library node)
      with the name of your service.  AnyService.dll is linked to the
      BeginService routine you made in 1) above, and will call it
      when your service runs.

  3)  You still need to install your service in order for the service
      control manager to run it.  Take a look at "instsrv.exe" (Google it)

