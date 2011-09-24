@echo off

@echo Environment details
ruby --version
call rake --version
call gem --version
call gem list albacore

echo Building...
call rake 
IF NOT %ERRORLEVEL% == 0 goto FAILED

echo Create ZIP package
call rake package

:FAILED
