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

SET LOGFILE=InstallScanDevicesService.log
ECHO %DATE% %TIME% [INFO] Script start														>%LOGFILE%

SC QUERY ScanDevicesService																	>>%LOGFILE% 2>&1
SET serviceStatus=%ERRORLEVEL%
IF NOT %serviceStatus%==1060 GOTO :SERVICE_ALREADY_INSTALLED

:INSTALL_SERVICE
IF EXIST InstallUtil.InstallLog DEL /F /Q InstallUtil.InstallLog
IF EXIST Voltura.ScanDevices.InstallLog DEL /F /Q Voltura.ScanDevices.InstallLog
ECHO %DATE% %TIME% [INFO] Installing ScanDevicesService...									>>%LOGFILE%
C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe -i Voltura.ScanDevices.exe	>>%LOGFILE% 2>&1
SET installResult=%ERRORLEVEL%
IF %installResult%==0 ECHO %DATE% %TIME% [INFO] Installation succeeded						>>%LOGFILE%
IF %installResult%==0 SC START ScanDevicesService											>>%LOGFILE% 2>&1
IF NOT %installResult%==0 ECHO Installation failed, see %LOGFILE% for details
IF NOT %installResult%==0 ECHO %DATE% %TIME% [ERROR] Installation failed (%installResult%)	>>%LOGFILE%
GOTO :END

:SERVICE_ALREADY_INSTALLED
SC QUERY ScanDevicesService | FINDSTR "RUNNING" && SC STOP ScanDevicesService				>>%LOGFILE%
SC DELETE ScanDevicesService																>>%LOGFILE%
IF %ERRORLEVEL%==0 GOTO :INSTALL_SERVICE
ECHO %DATE% %TIME% [ERROR] Installation failed, service already installed					>>%LOGFILE%

:END
ECHO %DATE% %TIME% [INFO] Script end														>>%LOGFILE%
EXIT /B 0