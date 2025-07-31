@echo off
echo Building Portable OOTP Database Converter...
echo ==========================================

REM Create output directory
set OUTPUT_DIR=portable-builds
if not exist "%OUTPUT_DIR%" mkdir "%OUTPUT_DIR%"

REM Build self-contained versions for Windows
echo Building for Windows x64...

REM Build Avalonia app
dotnet publish OOTPDatabaseConverter.Avalonia\OOTPDatabaseConverter.Avalonia.csproj ^
    -c Release ^
    -r win-x64 ^
    --self-contained true ^
    -p:PublishSingleFile=true ^
    -p:PublishTrimmed=true ^
    -o "%OUTPUT_DIR%\win-x64-gui"

REM Build Console app
dotnet publish OOTPDatabaseConverter.Console\OOTPDatabaseConverter.Console.csproj ^
    -c Release ^
    -r win-x64 ^
    --self-contained true ^
    -p:PublishSingleFile=true ^
    -p:PublishTrimmed=true ^
    -o "%OUTPUT_DIR%\win-x64-console"

REM Create portable package
set PACKAGE_NAME=OOTPDatabaseConverter-win-x64-portable
if not exist "%OUTPUT_DIR%\%PACKAGE_NAME%" mkdir "%OUTPUT_DIR%\%PACKAGE_NAME%"

REM Copy executables
copy "%OUTPUT_DIR%\win-x64-gui\OOTPDatabaseConverter.Gui.exe" "%OUTPUT_DIR%\%PACKAGE_NAME%\"
copy "%OUTPUT_DIR%\win-x64-console\OOTPDatabaseConverter.exe" "%OUTPUT_DIR%\%PACKAGE_NAME%\"

REM Copy scripts and documentation
if exist "copy-ootp-data.bat" copy "copy-ootp-data.bat" "%OUTPUT_DIR%\%PACKAGE_NAME%\"
if exist "README.md" copy "README.md" "%OUTPUT_DIR%\%PACKAGE_NAME%\"

REM Create simple usage instructions
echo OOTP Database Converter - Portable Version > "%OUTPUT_DIR%\%PACKAGE_NAME%\USAGE.txt"
echo ========================================= >> "%OUTPUT_DIR%\%PACKAGE_NAME%\USAGE.txt"
echo. >> "%OUTPUT_DIR%\%PACKAGE_NAME%\USAGE.txt"
echo This is a portable version that requires no installation. >> "%OUTPUT_DIR%\%PACKAGE_NAME%\USAGE.txt"
echo. >> "%OUTPUT_DIR%\%PACKAGE_NAME%\USAGE.txt"
echo USAGE: >> "%OUTPUT_DIR%\%PACKAGE_NAME%\USAGE.txt"
echo - GUI Version: Run OOTPDatabaseConverter.Avalonia.exe >> "%OUTPUT_DIR%\%PACKAGE_NAME%\USAGE.txt"
echo - Console Version: Run OOTPDatabaseConverter.exe >> "%OUTPUT_DIR%\%PACKAGE_NAME%\USAGE.txt"
echo. >> "%OUTPUT_DIR%\%PACKAGE_NAME%\USAGE.txt"
echo The application will create test-data and test-csv-output directories >> "%OUTPUT_DIR%\%PACKAGE_NAME%\USAGE.txt"
echo in the same folder when you use the "Copy OOTP Data" feature. >> "%OUTPUT_DIR%\%PACKAGE_NAME%\USAGE.txt"
echo. >> "%OUTPUT_DIR%\%PACKAGE_NAME%\USAGE.txt"
echo No installation required - just run the executable! >> "%OUTPUT_DIR%\%PACKAGE_NAME%\USAGE.txt"

echo.
echo Portable build complete!
echo Check the %OUTPUT_DIR% directory for the package.
echo.
echo Package contains:
echo - Self-contained executable (no .NET runtime needed)
echo - Copy script for OOTP data
echo - Usage instructions
echo - No installation required
pause 