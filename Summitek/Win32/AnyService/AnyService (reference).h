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
#ifndef _ANYSERVICE_H
#define _ANYSERVICE_H

/*
	Call this in main() to dispatch the service.
	char* serviceName is the NULL terminated name of the service.
	returns void
*/
__declspec(dllexport) void __cdecl DispatchService(char* serviceName);


/*
	Implement these functions elsewhere.
	Use BeginService as your entry point into your service code.
	0 will be passed in.
	There are no requirements on return value.
*/
extern long __cdecl BeginService(long In);

/*
	Implement these functions elsewhere.
	Use EndService to initiate shutting down or stopping of service code.
	1 will be passed into EndService if SERVICE_CONTROL_STOP is used.
	5 will be passed into EndService if SERVICE_CONTROL_SHUTDOWN is used.
	There are no requirements on the return value.
*/
extern long __cdecl EndService(long In);


#endif	/*_ANYSERVICE_H*/
