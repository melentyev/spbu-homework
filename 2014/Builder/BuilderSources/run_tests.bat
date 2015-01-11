@ECHO OFF

IF "%startup%" NEQ "control" GOTO :EOF

ECHO Running tests...

%nunit_console% %tests_library_bin% > %testing_log%

IF %ERRORLEVEL% NEQ 0 GOTO :Error

ECHO Tests passed.
GOTO :EOF

:Error
ECHO Tests failed!
SET run_tests_failed="1"
SET msg_attachment_cmd=-attach %testing_log%
SET msg_body=Builder failed (running tests).