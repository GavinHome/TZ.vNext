set ProfAPI_ProfilerCompatibilitySetting=EnableV2Profiler
set COMPLUS_ProfAPI_ProfilerCompatibilitySetting=EnableV2Profiler

"C:\Program Files (x86)\NCover\NCover.Console.exe" "C:\BuildTools_sh\NUnit-2.5.10.11092\bin\net-2.0\nunit-console-x86.exe"  //w "..\UT\UTAssemblies" "Tzepm.TZIWB.Controller.UT.dll" "Tzepm.TZIWB.Services.Implement.UT.dll" //a "Tzepm.TZIWB.Web;Tzepm.TZIWB.Web.Api;Tzepm.TZIWB.Services.Implement" //x ".\coverage.xml" //reg

