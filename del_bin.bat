@echo off

for /R %%f in (bin,obj,dist,packages,node_modules,bower_components) do (
	if exist "%%f" rmdir "%%f" /s /q
)
