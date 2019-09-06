rem auther:yangxiaomin
rem date:20190906
rem ******auto generate start********
@echo off
set "Ymd=%date:~0,4%-%date:~5,2%-%date:~8,2%-%time:~0,2%-%time:~3,2%-%time:~6,2%"

rem "tools/7-Zip/7z.exe" a "publish_%date:~0,4%-%date:~5,2%-%date:~8,2%.zip" "publish/*"

@echo on
rem echo %date:~0,4%-%date:~5,2%-%date:~8,2%
pause
rem ******auto generate  end********