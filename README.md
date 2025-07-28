# OOTP Database Converter

A modern, cross-platform tool for converting OOTP Baseball database files between ODB and CSV formats.

## Features

- **Convert OOTP Database (*.odb) files to CSV format** - Extract data for analysis in spreadsheet applications
- **Convert CSV files back to OOTP Database (*.odb) format** - Import modified data back into OOTP
- **Cross-platform support** - Windows, macOS, and Linux
- **Multiple interfaces** - GUI (Avalonia) and Command-line options
- **Progress tracking** - Real-time progress indicators with filename display
- **Steam integration** - Automatic detection and copying of OOTP 26 data
- **Backup functionality** - Safe backup and replacement of ODB files

## Quick Start

### Prerequisites

- **.NET 8.0** or later
- **OOTP Baseball 17-26** (for testing with real data)

### Getting Test Data

To get ODB files for testing, use the provided scripts:

**Linux/macOS:**
```bash
./copy-ootp-data.sh
```

**Windows:**
```cmd
copy-ootp-data.bat
```

These scripts will:
1. Search for Steam libraries on your system
2. Find your OOTP 26 installation
3. Copy all *.odb files to the `./test-data` directory for safe testing

### Running the Application

**Avalonia GUI (Recommended - Cross-platform):**
```bash
./run-avalonia.sh
# or
cd OOTPDatabaseConverter.Avalonia
dotnet run
```

**Console Application:**
```bash
cd OOTPDatabaseConverter.Console
dotnet run
```

**Interactive Console Mode:**
```bash
cd OOTPDatabaseConverter.Console
dotnet run
# Then follow the menu prompts
```

## Project Structure

```
OOTPDBTools/
├── OOTPDatabaseConverter.Core/          # Core conversion library
├── OOTPDatabaseConverter.Console/       # Command-line interface
├── OOTPDatabaseConverter.Avalonia/      # Cross-platform GUI
├── build-portable.sh/.bat               # Deployment scripts
├── copy-ootp-data.sh/.bat               # OOTP data copying
└── run-avalonia.sh                      # Avalonia launcher
```

## Usage

### GUI Application (Avalonia)

1. **Convert ODB to CSV:**
   - Click "Select ODB Directory" and choose folder with *.odb files
   - Click "Select Output Directory" for CSV files
   - Click "Convert ODB to CSV"

2. **Convert CSV to ODB:**
   - Click "Select CSV Directory" and choose folder with *.csv files
   - Click "Select Output Directory" for ODB files
   - Click "Convert CSV to ODB"

3. **Copy OOTP Data:**
   - Click "Copy OOTP Data" to copy files from Steam installation

4. **Backup & Copy ODB:**
   - Click "Backup & Copy ODB" to safely replace ODB files with backups

### Console Application

**Command-line mode:**
```bash
# Convert ODB to CSV
dotnet run -- odb2csv /path/to/odb/files /output/directory

# Convert CSV to ODB
dotnet run -- csv2odb /path/to/csv/files /output/directory
```

**Interactive mode:**
```bash
dotnet run
# Then select from the menu:
# 1. Convert ODB to CSV
# 2. Convert CSV to ODB
# 3. Copy OOTP Data (from Steam)
# 4. Backup & Copy ODB Files
# 5. Exit
```

## Development

### Building

```bash
# Build all projects
dotnet build

# Build specific project
dotnet build OOTPDatabaseConverter.Avalonia
dotnet build OOTPDatabaseConverter.Console
```

### VS Code Development

The project includes VS Code configuration:
- Debug configurations for all projects
- Build tasks
- Recommended extensions

### Deployment

Create portable executables:

**Linux/macOS:**
```bash
./build-portable.sh
```

**Windows:**
```cmd
build-portable.bat
```

This creates self-contained executables for all platforms in the `dist/` directory.

## Version History

- **v5.0** - Modernized architecture, removed obsolete projects, cross-platform Avalonia UI
- **v4.x** - Legacy versions with multiple platform-specific implementations

## License

This project is licensed under the **GNU General Public License v2.0** or later.

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## Support

For issues and questions, please check the project documentation or create an issue in the repository.
