@echo off
set countOfStylecop=1
echo "set countOfStylecop=%countOfStylecop%"


Violation.exe StyleCopReport.violations.xml
set /p "violation="<violation.txt


echo "Current violation is %violation%."
if  %violation% gtr %countOfStylecop% goto violation_go_with_error
goto go_with_success


:violation_go_with_error
echo "StyleCop´íÎóÊýÁ¿³¬¹ý %countOfStylecop%, ¸Ï½ôÐÞ¸´Ö®."
exit 1

:go_with_success
echo "Done stylecop checking"

