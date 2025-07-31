#!/bin/bash

echo "Building OOTPDatabaseConverter GUI..."
dotnet build OOTPDatabaseConverter.Gui

if [ $? -eq 0 ]; then
    echo "Build successful! Starting GUI..."
    dotnet run --project OOTPDatabaseConverter.Gui
else
    echo "Build failed!"
    exit 1
fi 