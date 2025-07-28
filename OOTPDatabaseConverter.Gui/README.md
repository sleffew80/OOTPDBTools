# OOTP Database Converter - Avalonia UI

A cross-platform GUI application for converting OOTP (Out of the Park Baseball) database files between ODB and CSV formats, built with Avalonia UI.

## Overview

This is a fork of the original OOTP Database Converter that has been upgraded from Windows Forms to Avalonia UI for cross-platform compatibility. The conversion logic remains unchanged - only the user interface has been modernized.

## Features

- **Cross-platform GUI** - Works on Windows, macOS, and Linux
- **ODB to CSV conversion** - Convert OOTP database files to CSV format
- **CSV to ODB conversion** - Convert CSV files back to OOTP database format
- **Progress tracking** - Real-time status updates during conversion
- **File/folder selection** - Browse buttons for easy path selection
- **Input validation** - Automatic validation of file paths and directories

## Building and Running

### Prerequisites
- .NET 8.0 SDK
- Avalonia templates: `dotnet new install Avalonia.Templates`

### Commands
```bash
# Build the project
dotnet build OOTPDatabaseConverter.Gui

# Run the application
dotnet run --project OOTPDatabaseConverter.Gui

# Or use the convenience script
./run-gui.sh
```

## Architecture

- **Avalonia UI** - Cross-platform UI framework
- **MVVM pattern** - Clean separation of concerns
- **Shared Core Library** - Uses the same conversion logic as the console version

## License

This project is licensed under the GNU General Public License v2.0 or later. 