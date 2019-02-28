set buildFolder=%1
set publishFolder=%2

set webDir=%buildFolder%\TZIWB.vNext.Web

Set COPYCMD=/Y

REM del /s /q "%publishFolder%\WebSite\*"

REM del /s /q "%publishFolder%\WebSite\*.*"

REM xcopy /s /e /r /y   %buildFolder%\JobScheduler\Tzepm.TZIWB.JobScheduler\bin\Release %publishFolder%\JobScheduler\
xcopy /s /e /r /y   %buildFolder%\CIScript %publishFolder%\DBInstall\CIScript\
xcopy /s /e /r /y   %buildFolder%\DBScript %publishFolder%\DBInstall\DBScript\
xcopy /s /e /r /y   %webDir%\publish %publishFolder%\WebSite\


