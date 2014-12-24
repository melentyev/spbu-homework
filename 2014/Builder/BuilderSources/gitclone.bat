@ECHO OFF
IF "%startup%" NEQ "control" GOTO :NOTSTART

echo Cloning repository:

git clone git://github.com/SU-SWS/open_framework.git
%git_dir%\git.exe clone %gitURL% "%work_dir%" > nul 2 > %clone_log%

IF ERRORLEVEL 1 GOTO :Error

echo Cloning completed.
goto :EOF

:Error
ECHO Error cloning repo.
CALL %basedir%\mail.bat clone_fail
EXIT /B