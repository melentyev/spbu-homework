@ECHO OFF

IF "%startup%" NEQ "control" GOTO :EOF

ECHO Removing the old repo...
IF EXIST %work_dir% GOTO :Remove

ECHO Old repo not found.
GOTO :EOF

:Remove
RMDIR /s /q "%work_dir%"
IF EXIST "%work_dir%" GOTO :Error
ECHO Clean completed.
GOTO :EOF

:Error
ECHO Clean failed!
SET clean_failed="1"