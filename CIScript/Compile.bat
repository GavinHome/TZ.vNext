set version=%1
set slnName=%2
set projectkey=%3
set projectName=%4

set dotnetPath=C:\Program Files\dotnet
set scannerMSBuildPath=E:\sonar-scanner-msbuild-core2.0

echo "prepare environment"

cd ..

echo "Compiling Service project..."

"%dotnetPath%\dotnet.exe" "%scannerMSBuildPath%\SonarScanner.MSBuild.dll" begin /k:"%projectkey%" /v:"%version%" /d:sonar.host.url="http://172.16.226.144:9000"  /d:sonar.login=b069c4394d4e545e63437ae7bd17d506cf97dbdb /n:"%projectName%"

"%dotnetPath%\dotnet.exe" build %slnName% -c Release > BuildLog.txt

"%dotnetPath%\dotnet.exe" "%scannerMSBuildPath%\SonarScanner.MSBuild.dll" end /d:sonar.login=b069c4394d4e545e63437ae7bd17d506cf97dbdb

find "0 ¸ö´íÎó" BuildLog.txt|| goto go_with_error

goto go_with_success

:go_with_error
type BuildLog.txt
exit 1

:go_with_success

