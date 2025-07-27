#!/bin/bash

echo "Building OOTPDatabaseConverter Avalonia UI..."
dotnet build OOTPDatabaseConverter.Avalonia

if [ $? -eq 0 ]; then
    echo "Build successful! Starting Avalonia UI..."
    dotnet run --project OOTPDatabaseConverter.Avalonia
else
    echo "Build failed!"
    exit 1
fi 