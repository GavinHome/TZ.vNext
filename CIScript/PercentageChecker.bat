@echo off

set percentageOfUnittest=0.00

set lastBuildRecordFilePath=c:\lastBuildRecord\percentage_TZIWB.txt
if not exist c:\lastBuildRecord md c:\lastBuildRecord
if exist %lastBuildRecordFilePath% set /p "percentageOflastUnittest="<%lastBuildRecordFilePath% 

echo "set percentageOflastUnittest=%percentageOflastUnittest% %%"



Percentage.exe coverage.xml
set /p "percent="<percentage.txt

echo "Current percentage is %percent%."
if %percentageOfUnittest%+0 gtr %percent%+0 goto percent_go_with_error

if %percentageOflastUnittest%+0 gtr %percent%+0 goto percent_go_with_lastBuild_error


goto go_with_success

:percent_go_with_error
echo "UT¸²¸ÇÂÊµÍÓÚ %percentageOfUnittest% %%, ¸Ï½ôÐÞ¸´Ö®."
exit 1

:percent_go_with_lastBuild_error
echo "UT¸²¸ÇÂÊµÍÓÚÉÏ¸ö°æ±¾: %percentageOflastUnittest% %%, ¸Ï½ôÐÞ¸´Ö®."
exit 1



:go_with_success
copy percentage.txt %lastBuildRecordFilePath%
echo "The lastBuildPercentage is set to %percent%."