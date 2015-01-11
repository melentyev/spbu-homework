@ECHO OFF

IF "%startup%" NEQ "control" GOTO :EOF

:: Utilities
SET git="C:\Program Files (x86)\Git\bin\git.exe"
SET msbuild="C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
SET blat="C:\Users\user\Downloads\blat323_32.full\blat323\full\blat.exe"
SET nunit_console="C:\Users\user\Downloads\NUnit-2.6.4\NUnit-2.6.4\bin\nunit-console.exe"

:: Project related options
SET git_repo_url=https://github.com/melentyev/SEIntroGeom
SET work_dir=%basedir%\..\SEIntroGeom
SET solution="%work_dir%\SeIntroGeom\SeIntroGeom.sln"
SET build_folder="%work_dir%\SeIntroGeom\SeIntroGeom\bin\Release"
SET tests_library_bin="%work_dir%\SeIntroGeom\Tests\bin\Release\Tests.dll"

SET required_list=%basedir%\required.list

SET clone_log=clone.log
SET build_log=build.log
SET testing_log=testing.log

:: Mail notification related options
SET mail_to=ard87@mail.ru
SET subject=Builder result
SET msg_body=Builder finished succesful.
SET msg_attachment_cmd=

:: Variables
SET clean_failed="0"
SET gitclone_failed="0"
SET build_failed="0"
SET build_check_failed="0"
SET run_tests_failed="0"