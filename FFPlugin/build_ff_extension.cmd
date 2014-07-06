rem used to build fb2epub firefox extension
rem 
@echo off
title FB2ePub converter Firefox extension builder
cls
setlocal
pushd

REM beginning of variables section
REM -----------------------------------------------------

rem project location root drive
rem change it to match the drive you retrieved it from SVN
SET RootDrive=C:

rem project location root
rem change it to match the place where you retrieved it from SVN
SET PROJ_ROOT=%RootDrive%\Project\GoogleCode\fb2epub\

rem folder to put logs into
SET LOG_FOLDER=%PROJ_ROOT%Output\Logs\

rem log file name prefix
SET LOGFILE_NAME=ff_ext_build

rem path to VC variable set file
rem we build extension with VS 2010
SET VCVARS="C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\vcvarsall.bat"

rem here the output archived setup will be placed
SET ARCHIVE_PATH=%PROJ_ROOT%Output\

SET ARCHIVE_NAME=%ARCHIVE_PATH%fb2epub_plugin@fb2epub.net.xpi

rem path to ZIP archiver used
SET ZIPER="C:\Program Files\WinRAR\WinRAR.exe"

rem parameters passed to archiver
SET ZIPER_PARAMS=a -y -afzip -m5 -ep1 -av -rr -r


REM end of variables sectio
REM -----------------------------------------------------

rem defines log file name
SET LOG="%LOG_FOLDER%%LOGFILE_NAME%.txt"

rem change to the project drive
%RootDrive%
rem change to the project folder
cd %PROJ_ROOT%

rem creates LOG folder but on the way creates build output folder too 
if not exist %LOG_FOLDER% mkdir %LOG_FOLDER%

rem remove prev. log if present
if exist %LOG%  del %LOG%
rem create/reset log file and add header entry
SET out1="Building at %DATE% , %TIME%."
call:PrintParam %out1%

rem FF extensions are built as 32 bit, since FF is 32 bit app
call %VCVARS% x86

rem
call:PrintParam "Running x86 build first"
msbuild %PROJ_ROOT%FFPlugin\xpcom_components.sln /t:rebuild /m /property:Configuration=Release;Platform="Win32" >> %LOG%
if errorlevel==1 goto failed

rem archive installation files into a ZIP for distribution
call:PrintParam "Creating XPI extension archive ( %ARCHIVE_NAME% )"
%ZIPER% %ZIPER_PARAMS% %ARCHIVE_NAME% %PROJ_ROOT%FFPlugin\Fb2ePubPlugin\*.*
if not errorlevel==0 goto winrar_failed



goto success


:success
title Build SUCCEEDED
call:PrintParam "FF Extension Build SUCCEEDED"
goto exit

:failed
title Build FAILED
call:PrintParam "FF Extension Build FAILED"
start notepad++ %LOG%
goto exit


rem FUNCTIONS 
rem ----------------------------------------------------

rem this function prints name of the component we build + additional parameter to screen and log file 
:PrintParam
echo %~1
echo %~1 >> %LOG%
goto:eof

rem -----------------------------------------------------


:exit
popd
endlocal
