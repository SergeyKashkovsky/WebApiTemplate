@echo off
IF "%~1" == "" (
  set "ServiceName=WebApiService"
) ELSE (
  set "ServiceName="%~1""
)

sc query %ServiceName% > NUL
IF ERRORLEVEL 1060 (
  create_svc %ServiceName%
  echo %ServiceName% created
  start_svc %ServiceName%
  echo %ServiceName% started
) ELSE (
  echo "Service exits"
)
