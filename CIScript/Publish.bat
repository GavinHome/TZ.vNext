set buildFolder=%1

cd ..

echo "Publish web project..."

"C:\Program Files\dotnet\dotnet.exe" publish "%buildFolder%\TZIWB.vNext.Web\TZIWB.vNext.Web.csproj" -c Release -f netcoreapp2.0  -o "%buildFolder%\TZIWB.vNext.Web\publish" > PublishLog.txt

find "TZIWB.vNext.Web -> %buildFolder%\TZIWB.vNext.Web\publish\" PublishLog.txt||goto go_with_error

goto go_with_success

:go_with_error
type PublishLog.txt
exit 1

:go_with_success



