IF "%~1" == "" (
  set "ServiceName=WebApiService"
) ELSE (
  set "ServiceName=%~1"
)

sc create %ServiceName% binPath= %CD%\..\publish\WebApiService.exe start= auto
sc description %ServiceName% "Some Descriprion"