@echo off
rem set start title for a window
title FB2ePub converter version builder

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

rem Path to the innosetup compiler location
set INNO_PATH="C:\Program Files (x86)\Inno Setup 5\"

rem log file name prefix
SET LOGFILE_NAME=build

rem path to VC variable set file
SET VCVARS="C:\Program Files (x86)\Microsoft Visual Studio 11.0\VC\vcvarsall.bat"

rem this actually defines packed file name , do not forget to change it when building
SET VERSION_MAJOR=1
SET VERSION_MINOR=1
SET VERSION_BUILD=4

rem here the output archived setup will be placed
SET ARCHIVE_PATH=%PROJ_ROOT%Output\

SET ARCHIVE_NAME=%ARCHIVE_PATH%Fb2ePubSetup_%VERSION_MAJOR%_%VERSION_MINOR%_%VERSION_BUILD%.zip

rem path to ZIP archiver used
SET ZIPER="C:\Program Files\WinRAR\WinRAR.exe"

rem parameters passed to archiver
SET ZIPER_PARAMS=a -y -afzip -m5 -ep



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

call %VCVARS% x86_amd64

rem
call:PrintParam "Running AnyCPU build first"
msbuild %PROJ_ROOT%Fb2ePub.sln /t:rebuild /m /property:Configuration=Release;Platform="Any CPU" >> %LOG%
if errorlevel==1 goto failed

rem 
call:PrintParam "Now building x86"
msbuild %PROJ_ROOT%Fb2ePub.sln /t:rebuild /m /property:Configuration=Release;Platform=x86 >> %LOG%
if errorlevel==1 goto failed

rem
call:PrintParam "Now building x64"
msbuild %PROJ_ROOT%Fb2ePub.sln /t:rebuild /m /property:Configuration=Release;Platform=x64 >> %LOG%
if errorlevel==1 goto failed

rem call InnoSetup compiler
call:PrintParam "Compiling the setup"
call %INNO_PATH%Compil32.exe /cc %PROJ_ROOT%Fb2ePubSetup\FB2ePubSetup.iss >> %LOG%
if errorlevel==1 goto inon_params_fail
if errorlevel==2 goto inno_fail

rem now copy changes.txt to the setup folder
call:PrintParam "Copy changes to the setup folder"
copy %PROJ_ROOT%FB2EPubConverter\changes.txt %PROJ_ROOT%Fb2ePubSetup\Output\. /Y
if errorlevel==1 goto failed

rem archive installation files into a ZIP for distribution
call:PrintParam "Creating ZIP archive"
%ZIPER% %ZIPER_PARAMS% %ARCHIVE_NAME% %PROJ_ROOT%Fb2ePubSetup\Output\*.*
if not errorlevel==0 goto winrar_failed

goto success

:winrar_failed
if errorlevel==1 goto success
call:PrintParam "WinRar error"
goto failed

:inon_params_fail
call:PrintParam "invalid parameters passed"
goto failed

:inno_fail
call:PrintParam "setup script compilation failed"
goto failed

:success
title Build SUCCEEDED
call:PrintParam "Build SUCCEEDED"
goto exit

:failed
title Build FAILED
call:PrintParam "Build FAILED"
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
pause