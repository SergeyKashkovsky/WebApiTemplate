IF "%~1" == "" (
  set "ServiceName=WebApiService"
) ELSE (
  set "ServiceName="%~1""
)

sc start %ServiceName%