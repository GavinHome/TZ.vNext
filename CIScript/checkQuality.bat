set version=%1
set projectKey=%2
set loopCount=0

cd ..

goto loopCheck

:loopCheck
echo "已重复%loopCount%次"
if %loopCount% gtr 60 goto SonarqubeError

set /a loopCount+=1

::判断数据是否为最新
D:\BuildTools\curl\bin\curl "http://172.16.226.144:9000/api/components/show?component=%projectKey%&r=%random%"  > SonarqubeComponentsResult.json

type "SonarqubeComponentsResult.json" | D:\BuildTools\jq-win64.exe ".component.version" > current_Version.txt

for /f "delims=" %%i in ('type current_Version.txt') do set current_Version=%%i

if "%version%" equ %current_Version% goto checkStatus

::延迟2秒
choice /t 5 /d y /n 
goto loopCheck

:end

:checkStatus
D:\BuildTools\curl\bin\curl "http://172.16.226.144:9000/api/measures/component?component=%projectKey%&metricKeys=alert_status&r=%random%" > SonarqubeResult.json

type "SonarqubeResult.json" | D:\BuildTools\jq-win64.exe ".component.measures[].value" > remote_Result.txt
for /f "delims=" %%i in ('type remote_Result.txt') do set remote_Result=%%i

if "ERROR" equ %remote_Result% goto SonarqubeError
goto SonarqubeOK
:end

:SonarqubeError
echo "未通过代码质量检查！"
exit 1
:end

:SonarqubeOK
