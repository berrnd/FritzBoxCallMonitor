@echo off
pushd "%~dp0"

set fritzBox=fritz.box
set callMonitor=..\FritzBoxCallMonitor.exe

set scriptOnIncomingCall=OnIncomingCall.bat
set scriptOnOutgoingCall=OnOutgoingCall.bat
set scriptOnConnected=OnConnected.bat
set scriptOnConnectionEnd=OnConnectionEnd.bat

start "" /D "%cd%" "%callMonitor%" "FritzBox=%fritzBox%" "OnIncomingCall=%scriptOnIncomingCall%" "OnOutgoingCall=%scriptOnOutgoingCall%" "OnConnected=%scriptOnConnected%" "OnConnectionEnd=%scriptOnConnectionEnd%"