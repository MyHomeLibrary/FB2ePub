@echo off

rem location of the standalone archived ZIP
SET SA_ARCHIVE_NAME=%ARCHIVE_PATH%Fb2ePubMINI_%VERSION_MAJOR%_%VERSION_MINOR%_%VERSION_BUILD%.zip

echo creating %SA_ARCHIVE_NAME%
%ZIPER% %ZIPER_PARAMS% %SA_ARCHIVE_NAME%  -ep1 @%PROJ_ROOT%standalone.lst 
if not errorlevel==0 goto winrar_failed
goto success

:winrar_failed
if errorlevel==1 goto success
call:PrintParam "WinRar error"
goto failed

:success
call:PrintParam "Creating standalone archive succeeded"
goto exit

:failed
call:PrintParam "Creating standalone archive FAILED"
goto exit


rem FUNCTIONS 
rem ----------------------------------------------------

rem this function prints name of the component we build 
:PrintParam
echo %~1
goto:eof

rem -----------------------------------------------------


:exit
