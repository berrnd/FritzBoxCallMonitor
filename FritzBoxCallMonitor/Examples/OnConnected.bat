@echo off

set timestamp=%~1
set eventType=%~2
set connectionId=%~3
set localExtension=%~4
set remoteNumber=%~5

echo %%1 (Timestamp) = %timestamp%
echo %%2 (EventType) = %eventType%
echo %%3 (ConnectionId) = %connectionId%
echo %%4 (LocalExtension) = %localExtension%
echo %%5 (remoteNumber) = %remoteNumber%
echo.

pause