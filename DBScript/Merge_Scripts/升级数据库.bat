@echo off

set  user=sa
set  pass=123456
set  svr=.
set  db=SIT_TZIWB_3.0_0724

set  DBCmd="C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\110\Tools\Binn\sqlcmd"

@echo on
call DatabaseCmdList.bat

PAUSE
Exit
MORE