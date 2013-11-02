@echo off

set timestamp=%~1
set eventType=%~2
set connectionId=%~3
set remoteNumber=%~4
set localNumber=%~5

echo %%1 (Timestamp) = %timestamp%
echo %%2 (EventType) = %eventType%
echo %%3 (ConnectionId) = %connectionId%
echo %%4 (RemoteNumber) = %remoteNumber%
echo %%5 (localNumber) = %localNumber%
echo.

pause