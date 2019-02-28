
set directory=H:\BuildFolder_TZIWB_Salary
set destFile=AssemblyInfo.cs
set filePath=H:\BuildFolder_TZIWB_Salary\CIScript\
cd /d %directory%
for /f %%i in ('dir /s/b "%destFile%"') do (
 %filePath%AssemblyInfoUtil -set:%1 %%i
)
