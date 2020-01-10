SpartanClient
=======

_SpartanClient - enterprise test automation and data management software_

SpartanClient is a program designed to be installed on modern Windows workstations (i.e., Windows 7+).
SpartanClient becomes the test execution agent for the Spartan enterprise backend. When
the client runs on the workstation, a TCP port is opened to listen for directions from the
backend. The backend can direct the client to load, unload and execute tests from its built-in
library of test modules. The test modules in turn utilize a built-in instrument driver library.

Currently the SpartanClient supports the following tests:

* PIM - this test uses Kaelus instrumentation to measure the passive intermodulation distortion
on items under test.
* S-Parameter - this test uses a range of vector network analyzers (VNA) to measure the 2-port
scattering parameters of items under test.
* Prompt - this test prompts the user with a message and blocks test execution until confirmed.



