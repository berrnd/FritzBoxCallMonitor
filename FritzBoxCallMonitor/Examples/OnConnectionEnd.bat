@echo off

set timestamp=%~1
set eventType=%~2
set connectionId=%~3
set durationInSeconds=%~4

echo %%1 (Timestamp) = %timestamp%
echo %%2 (EventType) = %eventType%
echo %%3 (ConnectionId) = %connectionId%
echo %%4 (DurationInSeconds) = %durationInSeconds%
echo.

pause