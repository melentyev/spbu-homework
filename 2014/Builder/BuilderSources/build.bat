@echo off

echo Building solution...

%MSBuildDir%\MSBuild.exe %repoPath%\%solution% /p:Configuration=Release;VisualStudioVersion=12.0 >%MSBuildLog%
if ERRORLEVEL 1 goto :error

echo Build completed.
goto :EOF

:Error
echo Build failed!
call email.bat
