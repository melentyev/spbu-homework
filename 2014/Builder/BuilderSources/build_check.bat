@ECHO OFF

IF "%startup%" NEQ "control" GOTO :EOF

ECHO Check build...

FOR /F "tokens=*" %%i IN (%required_list%) DO IF NOT EXIST "%work_dir%\%%i" SET missing_file=%%i& GOTO :Error

ECHO Check successful.
GOTO :EOF

:Error
ECHO Build check failed! File not found: %missing_file%
SET build_check_failed="1"
SET msg_body=Builder failed (files check).