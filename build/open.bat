@echo off
cls
powershell .\tools\psake\psake.ps1 .\open.ps1 update_test_driven
start /b "C:\program files\Microsoft Visual Studio 9\Common7\IDE\devenv.exe" ..\solution.sln /Edit
