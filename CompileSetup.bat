@echo off

echo Deleting data folder from build...
del D:\Workspace\C#\Ninance v2\bin\Release\net6.0-windows10.0.22000.0\data

echo Building Setup...
"D:\Program Files\Inno Setup 6\ISCC.exe" "D:\Workspace\C#\Ninance v2\Setup\Ninance_Setup.iss"

echo Done!