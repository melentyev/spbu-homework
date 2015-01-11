@ECHO OFF

IF "%startup%" NEQ "control" GOTO :EOF

%blat% -s "%subject%" -body "%msg_body%" -to %mail_to% %msg_attachment_cmd% 

IF %ERRORLEVEL% NEQ 0 GOTO :error

ECHO Sending succeeded.
GOTO :EOF

:Error
ECHO Email sending error!