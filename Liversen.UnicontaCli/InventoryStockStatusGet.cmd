@echo off
echo Get inventory stock status
echo.
set loginId=Alice
set accessIdentity=40781a63-c968-458f-bd32-c5baf536a59f
set companyId=142857
set /p valueAt=Date (format: yyyy-MM-dd): 
set /p password=Password: 
set outputPath=%USERPROFILE%\Documents\inventory-stock-status-%valueAt%.csv
%~dp0Liversen.UnicontaCli.exe --loginId %loginId% --password %password% --accessIdentity %accessIdentity% --companyId %companyId% inventory-stock-status get %valueAt% --csv --outputPath %outputPath%
if errorlevel 1 pause & goto :eof
echo Output written to %outputPath%.
pause