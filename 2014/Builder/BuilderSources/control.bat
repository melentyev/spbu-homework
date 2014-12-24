@echo off

SET startup=control

echo Starting...

CALL %basedir%\settings.bat

CALL %basedir%\cleanup.bat

::CALL %basedir%\clone.bat

::CALL %basedir%\build.bat

::CALL %basedir%\build_check.bat

::CALL %basedir%\run_tests.bat

::CALL %basedir%\email.bat success

echo Finished

::IF "%clone_failed%" == "TRUE" GOTO :Email