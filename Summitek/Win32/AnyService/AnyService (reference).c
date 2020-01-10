/*
Copyright (c) 2005, Kuan Chen
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

	* Redistributions of source code must retain the above copyright notice,
this list of conditions and the following disclaimer.
	* Redistributions in binary form must reproduce the above copyright
notice, this list of conditions and the following disclaimer in the
documentation and/or other materials provided with the distribution.
	* Neither the name of the Kuan Chen nor the names of its contributors may
be used to endorse or promote products derived from this software without
specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#include "AnyService.h"

#include <windows.h>

/*strncpy*/
#include <string.h>


/*
	Used this for debugging.
*/
#include <stdio.h>
void debug(char* s)
{
	FILE* fd;
	fd = fopen("C:\\temp\\debug.txt", "a");
	fprintf(fd, "%s\n", s);
	fclose(fd);
}

/*
	Fill in the controls we are accpeting here.
	In the future consider adding in Pause and Continue.
*/
#define ACCPETED_CONTROLS SERVICE_ACCEPT_STOP | SERVICE_ACCEPT_SHUTDOWN

/*
	Maximum length of the service name.
*/
#define SERVICENAMELEN 256

static SERVICE_STATUS serviceStatus;
static SERVICE_STATUS_HANDLE serviceStatusHandle;
static char storedServiceName[SERVICENAMELEN];	/*Use this global to pass in the service name.*/

VOID WINAPI ServiceMain(DWORD dwArgc, LPTSTR* lpszArgv);
VOID WINAPI ControlServiceHandler(DWORD fdwControl);

/*
	Call this in main() to dispatch the service.
	char* serviceName is the NULL terminated name of the service.
	returns void
*/
void __cdecl DispatchService(char* serviceName)
{
	SERVICE_TABLE_ENTRY table[2];

	/*Do rudimentary sanity check.*/
	if (!serviceName)
	{
		return;		
	}

	/*Make copy of the service name. This is just in case of allocation issues
	with the location of serviceName.*/
	strncpy(storedServiceName, serviceName, 256);
	storedServiceName[SERVICENAMELEN-1] = '\0';	/*NULL terminate just in case.*/

	table[0].lpServiceName = storedServiceName;
	table[0].lpServiceProc = (LPSERVICE_MAIN_FUNCTION)ServiceMain;
	table[1].lpServiceName = NULL;
	table[1].lpServiceProc = NULL;
	StartServiceCtrlDispatcher(table);
}

VOID WINAPI ServiceMain(DWORD dwArgc, LPTSTR* lpszArgv)
{
	serviceStatus.dwServiceType = SERVICE_WIN32_OWN_PROCESS;
	serviceStatus.dwCurrentState = SERVICE_START_PENDING;
	serviceStatus.dwControlsAccepted = ACCPETED_CONTROLS;	/*defined above*/
	serviceStatus.dwWin32ExitCode = 0;	/*Not using ERROR_SERVICE_SPECIFIC_ERROR*/
	serviceStatus.dwServiceSpecificExitCode = 0;	/*No specific error code to use.*/
	serviceStatus.dwCheckPoint = 0;
	serviceStatus.dwWaitHint = 0;

	serviceStatusHandle = RegisterServiceCtrlHandler(storedServiceName, (LPHANDLER_FUNCTION)ControlServiceHandler);

	/*Set service status to running*/
	serviceStatus.dwCurrentState = SERVICE_RUNNING;
	
	/*Dispatch the service. Returns true if success*/
	if (SetServiceStatus(serviceStatusHandle, &serviceStatus))
	{
		BeginService(0);	/*Arbitrarily passing in 0.*/
	}

	/*Only reaches this point on error or when BeginService ends.*/
}


/*
	This is the handler we setup in DispatchService() to handle
	service stop and shutdown requests. Called by Windows.
*/
VOID WINAPI ControlServiceHandler(DWORD fdwControl)
{

	/*
		Call EndService() first. It is good behavior to wait until you have actually
		ended to the service before you set the service state to stop.
	*/
	switch(fdwControl)
	{
		case SERVICE_CONTROL_STOP:
			/*
				SetService return nonzero if succeeds.
				Call the external EndService routine.
			*/
			EndService(1);	/*Pass in 1 because this is STOP.*/
			break;
		case SERVICE_CONTROL_SHUTDOWN:
			/*
				SetService return nonzero if succeeds.
				Call the external EndService routine.
			*/
			EndService(5);	/*Pass in 5 because this is SHUTDOWN.*/
			break;
		default:
			/*Quit out of this is neither STOP or SHUTDOWN.*/
			break;
	}

	switch(fdwControl)
	{
		case SERVICE_CONTROL_STOP:
		case SERVICE_CONTROL_SHUTDOWN:
			serviceStatus.dwCurrentState = SERVICE_STOPPED;
			serviceStatus.dwWin32ExitCode = 0;
			SetServiceStatus(serviceStatusHandle, &serviceStatus);
			break;
		default:
			/* Return out right away if we get anything else.*/
			break;
	}

	/*Set the state again just in case, though we should never get here.*/
	SetServiceStatus(serviceStatusHandle, &serviceStatus);
}
