@echo off
start %cd%\Klockerkarkopf_build\klockerkarkopf.exe -adapter 2 -force-d3d9 -popupwindow -screen-width 1280 -screen-height 1024
timeout /t 10 /nobreak > NUL
start %cd%\klockerkarkopf_build2\klockerkarkopf2.exe -adapter 1 -force-d3d9 -screen-width 1920 -screen-height 1080
exit
