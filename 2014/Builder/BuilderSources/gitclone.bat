@ECHO OFF
	
IF "%startup%" NEQ "control" GOTO :EOF

ECHO Cloning repository:

%git% clone "%git_repo_url%" "%work_dir%" >nul 2>%clone_log%

IF ERRORLEVEL 1 GOTO :Error

ECHO Cloning completed.
GOTO :EOF

:Error
ECHO Clone failed.
SET gitclone_failed="1"
SET msg_attachment_cmd=-attach %clone_log%
SET msg_body=Builder failed (clone failed).