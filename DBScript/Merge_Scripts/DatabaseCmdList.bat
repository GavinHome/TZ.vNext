@echo off

set  filePath=.

@echo 0.ɾ����ͼ
%DBCmd% -U %user% -P %pass% -S %svr%  -d %db% -i %filePath%\0.ɾ����ͼ.sql
@echo 1.������
%DBCmd% -U %user% -P %pass% -S %svr%  -d %db% -i %filePath%\1.������.sql
@echo 2.������ͼ.sql
%DBCmd% -U %user% -P %pass% -S %svr%  -d %db% -i %filePath%\2.������ͼ.sql
@echo 3.���´洢���̼�����.sql
%DBCmd% -U %user% -P %pass% -S %svr%  -d %db% -i %filePath%\3.���´洢���̼�����.sql
@echo 4.�˵�Ȩ��.sql
%DBCmd% -U %user% -P %pass% -S %svr%  -d %db% -i %filePath%\4.�˵�Ȩ��.sql
@echo 5.��������.sql
%DBCmd% -U %user% -P %pass% -S %svr%  -d %db% -i %filePath%\5.��������.sql





