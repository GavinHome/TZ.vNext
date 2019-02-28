@echo off

set  filePath=.

@echo 0.删除视图
%DBCmd% -U %user% -P %pass% -S %svr%  -d %db% -i %filePath%\0.删除视图.sql
@echo 1.创建表
%DBCmd% -U %user% -P %pass% -S %svr%  -d %db% -i %filePath%\1.创建表.sql
@echo 2.更新视图.sql
%DBCmd% -U %user% -P %pass% -S %svr%  -d %db% -i %filePath%\2.更新视图.sql
@echo 3.更新存储过程及函数.sql
%DBCmd% -U %user% -P %pass% -S %svr%  -d %db% -i %filePath%\3.更新存储过程及函数.sql
@echo 4.菜单权限.sql
%DBCmd% -U %user% -P %pass% -S %svr%  -d %db% -i %filePath%\4.菜单权限.sql
@echo 5.更新数据.sql
%DBCmd% -U %user% -P %pass% -S %svr%  -d %db% -i %filePath%\5.更新数据.sql





