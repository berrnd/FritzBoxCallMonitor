@echo off

set timestamp=%~1
set eventType=%~2
set connectionId=%~3
set localExtension=%~4
set localNumber=%~5
set remoteNumber=%~6

echo %%1 (Timestamp) = %timestamp%
echo %%2 (EventType) = %eventType%
echo %%3 (ConnectionId) = %connectionId%
echo %%4 (LocalExtension) = %localExtension%
echo %%5 (LocalNumber) = %localNumber%
echo %%6 (RemoteNumber) = %remoteNumber%
echo.

pause