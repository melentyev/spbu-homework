@echo off

IF "%startup%" NEQ "control" GOTO :NOTSTART


SET basedir=BuilderSource
SET nunit_dir="C:\Program Files (x86)\NUnit 2.6.3\bin"
set git_dir="C:\Program Files (x86)\Git\bin"
set msbuild_dir="C:\Windows\Microsoft.NET\Framework\v4.0.30319"
set blat_dir="C:\Users\objec_000\Downloads\blat323_32.full\blat323\full"
set work_dir="..\WorkDir"
set git_repo_url=https://github.com/melentyev/SEIntroGeom

set solution=SeIntroGeom.sln
set build_folder="%WorkDir%\SeIntroGeom\bin\Release"

set clone_log=clone.log
set msbuild_log=build.log

set required_files=%folder%\required_files.txt

set mail_to=romanbelkov@gmail.com
set msg_subject=Builder result
set msg_body=Build finished succesful.
set msg_attachment=%MSBuildLog%