@echo off
setlocal enabledelayedexpansion

REM OOTP Data Copy Script for Windows
REM This script finds OOTP 26 in the Steam library and copies ODB files to ./test-data for testing

echo [INFO] OOTP Data Copy Script
echo [INFO] =====================

REM Check if we're in the right directory
if not exist "OOTPDatabaseConverter.Core\OOTPDatabaseConverter.Core.csproj" (
    echo [ERROR] This script must be run from the OOTPDBTools root directory!
    exit /b 1
)

REM Find Steam library directories
echo [INFO] Searching for Steam libraries...

set "STEAM_LIBS="
set "FOUND_LIBS=0"

REM Common Steam library locations
set "COMMON_PATHS=%PROGRAMFILES(X86)%\Steam\steamapps %PROGRAMFILES%\Steam\steamapps %USERPROFILE%\Steam\steamapps"

REM Check for additional library folders from libraryfolders.vdf
set "LIBRARYFOLDERS=%USERPROFILE%\Steam\steamapps\libraryfolders.vdf"
if exist "%LIBRARYFOLDERS%" (
    echo [INFO] Found Steam library configuration at: %LIBRARYFOLDERS%
    
    REM Extract library paths from libraryfolders.vdf (simplified)
    for /f "tokens=2 delims=	" %%i in ('findstr /C:"		" "%LIBRARYFOLDERS%"') do (
        if exist "%%i\steamapps" (
            set "STEAM_LIBS=!STEAM_LIBS! %%i\steamapps"
            set /a FOUND_LIBS+=1
            echo [INFO] Found Steam library: %%i\steamapps
        )
    )
)

REM Add common paths if they exist
for %%p in (%COMMON_PATHS%) do (
    if exist "%%p" (
        set "STEAM_LIBS=!STEAM_LIBS! %%p"
        set /a FOUND_LIBS+=1
        echo [INFO] Found Steam library: %%p
    )
)

if %FOUND_LIBS%==0 (
    echo [ERROR] No Steam libraries found!
    exit /b 1
)

REM Find OOTP 26 installation
echo [INFO] Searching for OOTP 26...

set "OOTP_DIR="
for %%l in (%STEAM_LIBS%) do (
    echo [INFO] Searching in Steam library: %%l
    
    REM Look for OOTP 26 app manifest
    for /f "delims=" %%f in ('dir /b "%%l\appmanifest_*.acf" 2^>nul') do (
        findstr /i "Out of the Park Baseball 26\|OOTP Baseball 26" "%%l\%%f" >nul 2>&1
        if !errorlevel!==0 (
            echo [SUCCESS] Found OOTP 26 manifest: %%l\%%f
            
            REM Extract app ID from manifest filename
            for /f "tokens=2 delims=_" %%a in ("%%f") do (
                for /f "tokens=1 delims=." %%b in ("%%a") do (
                    echo [INFO] OOTP 26 App ID: %%b
                )
            )
            
            REM Find the actual game directory
            if exist "%%l\common\Out of the Park Baseball 26" (
                set "OOTP_DIR=%%l\common\Out of the Park Baseball 26"
                echo [SUCCESS] Found OOTP 26 installation: !OOTP_DIR!
                goto :found_ootp
            ) else if exist "%%l\common\OOTP Baseball 26" (
                set "OOTP_DIR=%%l\common\OOTP Baseball 26"
                echo [SUCCESS] Found OOTP 26 installation: !OOTP_DIR!
                goto :found_ootp
            ) else (
                echo [WARNING] Found manifest but game directory not found at expected location
            )
        )
    )
)

echo [ERROR] OOTP 26 not found in any Steam library!
echo [INFO] Please ensure OOTP 26 is installed via Steam and try again.
exit /b 1

:found_ootp
REM Copy ODB files
echo [INFO] Copying ODB files...

set "TARGET_DIR=.\test-data"

REM Create target directory if it doesn't exist
if not exist "%TARGET_DIR%" (
    mkdir "%TARGET_DIR%"
    echo [INFO] Created target directory: %TARGET_DIR%
)

REM Find and copy all ODB files directly to target directory
set "COPIED_COUNT=0"

for /r "%OOTP_DIR%" %%f in (*.odb) do (
    set "FILENAME=%%~nxf"
    echo [INFO] Copying: !FILENAME!
    
    copy "%%f" "%TARGET_DIR%\!FILENAME!" >nul 2>&1
    if !errorlevel!==0 (
        echo [SUCCESS] Copied: !FILENAME!
        set /a COPIED_COUNT+=1
    ) else (
        echo [ERROR] Failed to copy: !FILENAME!
    )
)

if %COPIED_COUNT%==0 (
    echo [WARNING] No ODB files found in: %OOTP_DIR%
    exit /b 1
)

echo [SUCCESS] Successfully copied %COPIED_COUNT% ODB file(s) to %TARGET_DIR%

REM List copied files
echo.
echo [INFO] Copied files:
dir /b "%TARGET_DIR%\*.odb" 2>nul || echo [WARNING] No ODB files in target directory

echo [SUCCESS] Data copy completed successfully!
echo [INFO] You can now use the ODB files in %TARGET_DIR% for testing.

endlocal 