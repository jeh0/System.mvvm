:: @echo off
@echo Run me as administrator 

set Host_Port=8089
set Host_Name=TEMHuK
:: TEMHuK

:: Создание новой группы безопасности из командной строки 
:: net localgroup "<security group name>" /comment:"<security group description>" /add 



netsh advfirewall firewall delete rule name="PeEm.%Host_Name% (SignalR)" 
netsh advfirewall firewall add rule name="PeEm.%Host_Name% (SignalR)" dir=in action=allow protocol=TCP localport=%Host_Port%


:: УДалить правило
:: netsh http delete urlacl url=http://


:: netsh http add urlacl url=http://+:80/Temporary_Listen_Addresses/ user="<machine name>\<security group name>" 
netsh http add urlacl url=http://%Host_Name%:%Host_Port%/ user=Все
:: netsh http add urlacl url=http://TEMHuK:8089/ user=Все

:: Создание ограниченного резервирования URL-адреса
netsh http show urlacl url=http://%Host_Name%:%Host_Port%/


:: netsh http add iplisten ipaddress=0.0.0.0:%Host_Port%

:: https://msdn.microsoft.com/ru-ru/library/dd767317(v=vs.110).aspx
netsh http show urlacl > netsh_%Host_Name%.txt

pause