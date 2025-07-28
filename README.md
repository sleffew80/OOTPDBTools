# OOTP Database Converter

The OOTP Database Converter is a tool that allows you to convert OOTP Baseball's odb files into editable csv files. You can then make edits to the data and convert those csv files back into odb files. Currently the OOTP Database Converter is compatible with all versions of OOTP 17 through OOTP 26. Support for older versions may be added if there is enough demand.

> [!NOTE]
> The OOTP Database Converter currently isn't capable of converting a database from one version of OOTP to another without understanding the varying layouts of the tables from version to version and making the appropriate updates (For example, the fielding table in OOTP 24 has additional columns that OOTP 17 doesn't have). Databases can and should only be disassembled and rebuilt for a single version of the game.

## About This Fork

This is a 100% infrastructure-modernization-minded fork of the original OOTP Database Converter by Steven Leffew. **ALL code logic is unchanged** - only reorganized into a streamlined .NET 8.0 solution with cross-platform Avalonia UI. The original conversion algorithms, file handling, and business logic remain exactly as designed.

## Prerequisites

- Git
- .NET 8.0 SDK
- IDE (VS Code, Visual Studio, or Rider)
- For GUI: `dotnet new install Avalonia.Templates`

## Getting Started

```bash
# Clone the repository
git clone <repository-url>
cd OOTPDBTools

# Build the solution
dotnet build

# Run GUI (recommended)
./run-gui.sh

# Run console app
cd OOTPDatabaseConverter.Console
dotnet run
```

## Quick Usage

**GUI (Recommended):**
```bash
./run-gui.sh
```

**Console:**
```bash
cd OOTPDatabaseConverter.Console
dotnet run
```

**Get test data:**
```bash
./copy-ootp-data.sh  # Linux/macOS
copy-ootp-data.bat   # Windows
```

## Deployment

Create portable executables:
```bash
./build-portable.sh  # Linux/macOS
build-portable.bat   # Windows
```

## What's New in v5.0

- **Cross-platform Avalonia GUI** - Modern UI for Windows, macOS, Linux
- **Enhanced Console Application** - Interactive menu-driven interface with multiple operations
- **Steam Integration** - Automatic OOTP data detection and copying from Steam installations
- **Backup Functionality** - Safe ODB file replacement with automatic backup creation
- **Streamlined Architecture** - Single solution with 3 core projects (Core, Console, GUI)
- **VS Code Support** - Debug configurations and build tasks included
- **Portable Builds** - Self-contained executables for all platforms

## Features

### **Conversion Capabilities**
- Convert OOTP Database (.odb) files to CSV format
- Convert CSV files back to OOTP Database (.odb) format
- Supports OOTP versions 17 through 26
- Progress tracking during conversion
- Error handling with user-friendly messages

### **GUI Application**
- Cross-platform Avalonia UI (Windows, macOS, Linux)
- Two-section layout for ODB→CSV and CSV→ODB conversions
- File/folder selection with browse buttons
- Progress indicators with status messages
- Input validation for file paths and directories
- MVVM architecture with responsive design

### **Console Application**
- Command-line and interactive modes
- Menu-driven interface with multiple operation options
- Steam integration for automatic OOTP data detection
- Backup functionality for safe ODB file replacement
- Cross-platform compatibility

### **Utility Features**
- **Copy OOTP Data** - Automatically detect and copy OOTP data from Steam installations
- **Backup & Copy ODB Files** - Safely replace ODB files with automatic backup creation
- **Portable Builds** - Self-contained executables requiring no .NET runtime installation

## Project Structure

```
OOTPDBTools/
├── OOTPDatabaseConverter.Core/          # Shared conversion logic
├── OOTPDatabaseConverter.Console/       # Cross-platform console application
├── OOTPDatabaseConverter.Gui/           # Cross-platform Avalonia GUI
├── build-portable.sh                    # Cross-platform build script
├── build-portable.bat                   # Windows build script
├── copy-ootp-data.sh                    # Linux/macOS data copy script
├── copy-ootp-data.bat                   # Windows data copy script
└── run-gui.sh                          # GUI launcher script
```

## Building from Source

### **Build Commands**
```bash
# Build entire solution
dotnet build

# Build specific projects
dotnet build OOTPDatabaseConverter.Core
dotnet build OOTPDatabaseConverter.Console
dotnet build OOTPDatabaseConverter.Gui

# Create portable builds
./build-portable.sh  # Linux/macOS
build-portable.bat   # Windows
```

## License

This project is licensed under the GNU General Public License v2.0 or later.
