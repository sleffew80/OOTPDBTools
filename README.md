# OOTP Database Converter

The OOTP Database Converter is a tool that allows you to convert OOTP Baseball's odb files into editable csv files. You can then make edits to the data and convert those csv files back into odb files. Currently the OOTP Database Converter is compatible with all versions of OOTP 17 through OOTP 26. Support for older versions may be added if there is enough demand.

> [!NOTE]
> The OOTP Database Converter currently isn't capable of converting a database from one version of OOTP to another without understanding the varying layouts of the tables from version to version and making the appropriate updates (For example, the fielding table in OOTP 24 has additional columns that OOTP 17 doesn't have). Databases can and should only be disassembled and rebuilt for a single version of the game.

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
- **Streamlined architecture** - Single solution with 3 core projects
- **Steam integration** - Automatic OOTP data detection and copying
- **Backup functionality** - Safe ODB file replacement with automatic backups
- **VS Code support** - Debug configurations and build tasks included
