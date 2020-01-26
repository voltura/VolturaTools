@ECHO OFF

:init
setlocal DisableDelayedExpansion
set "batchPath=%~0"
for %%k in (%0) do set batchName=%%~nk
set "vbsGetPrivileges=%temp%\OEgetPriv_%batchName%.vbs"
setlocal EnableDelayedExpansion

:checkPrivileges
NET FILE 1>NUL 2>NUL
if '%errorlevel%' == '0' ( goto gotPrivileges ) else ( goto getPrivileges )

:getPrivileges
if '%1'=='ELEV' (echo ELEV & shift /1 & goto gotPrivileges)
ECHO Set UAC = CreateObject^("Shell.Application"^) > "%vbsGetPrivileges%"
ECHO args = "ELEV " >> "%vbsGetPrivileges%"
ECHO For Each strArg in WScript.Arguments >> "%vbsGetPrivileges%"
ECHO args = args ^& strArg ^& " "  >> "%vbsGetPrivileges%"
ECHO Next >> "%vbsGetPrivileges%"
ECHO UAC.ShellExecute "!batchPath!", args, "", "runas", 1 >> "%vbsGetPrivileges%"
"%SystemRoot%\System32\WScript.exe" "%vbsGetPrivileges%" %*
exit /B

:gotPrivileges
setlocal & pushd .
cd /d %~dp0
if '%1'=='ELEV' (del "%vbsGetPrivileges%" 1>nul 2>nul  &  shift /1)

SET LOGFILE=UninstallScanDevicesService.log
ECHO %DATE% %TIME% [INFO] Script start																		>%LOGFILE%

SC QUERY ScanDevicesService																					>>%LOGFILE% 2>&1
SET uninstallResult=%ERRORLEVEL%
IF NOT %uninstallResult%==0 GOTO :ERROR
:: Stop service if running
SC QUERY ScanDevicesService | FINDSTR "RUNNING" && SC STOP ScanDevicesService								>>%LOGFILE% 2>&1
:: Remove logs
IF EXIST .\bin\x86\release\InstallUtil.InstallLog DEL /F /Q .\bin\x86\release\InstallUtil.InstallLog		>>%LOGFILE% 2>&1
ECHO %DATE% %TIME% [INFO] Uninstalling ScanDevicesService...												>>%LOGFILE%
SC DELETE ScanDevicesService																				>>%LOGFILE% 2>&1
SET uninstallResult=%ERRORLEVEL%
IF %uninstallResult%==0 ECHO %DATE% %TIME% [INFO] Uninstallation succeeded									>>%LOGFILE%

:ERROR
IF NOT %uninstallResult%==0 ECHO %DATE% %TIME% [ERROR] Uninstallation failed (%uninstallResult%)			>>%LOGFILE%

:END
ECHO %DATE% %TIME% [INFO] Script end																		>>%LOGFILE%
EXIT /B 0