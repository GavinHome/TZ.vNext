set projectDir=%1


"C:\Program Files (x86)\Microsoft Fxcop 10.0\FxCopCmd.exe" -project:"FxCop Minimal template.FxCop" /c > fxcop.txt

find "warning" .\fxcop.txt || goto go_with_success

goto go_with_error


:go_with_error
type fxcop.txt
echo "FxCop�����Ƿ�����������, �Ͻ��޸�֮."
exit 1

:go_with_success