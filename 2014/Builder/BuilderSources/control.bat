@ECHO OFF

SET startup=control
SET basedir=%cd%

ECHO Starting...

CALL %basedir%\config.bat

CALL %basedir%\clean.bat
IF %clean_failed% EQU "1" GOTO :Email

CALL %basedir%\gitclone.bat
IF %gitclone_failed% EQU "1" GOTO :Email

CALL %basedir%\build.bat
IF %build_failed% EQU "1" GOTO :Email

ECHO after build
CALL %basedir%\build_check.bat
IF %build_check_failed% EQU "1" GOTO :Email

CALL %basedir%\run_tests.bat
IF %run_tests_failed% EQU "1" GOTO :Email

:Email
CALL %basedir%\mail.bat

ECHO Builder finished. (%msg_body%)

pause