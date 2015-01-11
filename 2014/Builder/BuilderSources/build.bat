@ECHO OFF

IF "%startup%" NEQ "control" GOTO :EOF

ECHO Building solution...

%msbuild% %solution% /p:Configuration=Release;VisualStudioVersion=12.0 > %build_log%
IF ERRORLEVEL 1 GOTO :error

ECHO Build completed.
GOTO :EOF

:Error
ECHO Build failed!
SET build_failed="1"
SET msg_attachment_cmd=-attach %build_log%
SET msg_body=Builder failed (build failed).