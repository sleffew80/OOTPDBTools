# OOTP Database Converter Tools

A collection of tools for converting OOTP Baseball database files between ODB and CSV formats.

## Features

- Convert OOTP Database (*.odb) files to CSV format
- Convert CSV files back to OOTP Database (*.odb) format
- Cross-platform support (Windows, macOS, Linux)
- Progress indicators with filename display
- Multiple UI options (Avalonia, Windows Forms, Console)

## Quick Start

### Prerequisites

- .NET 8.0 or later
- OOTP Baseball 26 (for testing with real data)

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

**Avalonia UI (Cross-platform):**
```bash
cd OOTPDatabaseConverter.Avalonia
dotnet run
```

**Windows Forms:**
```bash
cd OOTPDatabaseConverter.Windows
dotnet run
```

**Console Application:**
```bash
cd OOTPDatabaseConverter.Console
dotnet run
```

## Project Structure

- `OOTPDatabaseConverter.Core/` - Main conversion library
- `OOTPDatabaseConverter.Avalonia/` - Cross-platform UI
- `OOTPDatabaseConverter.Windows/` - Windows Forms UI
- `OOTPDatabaseConverter.Console/` - Command-line interface
- `Obsolete/` - Deprecated libraries (see migration guide)

## Usage

1. **ODB to CSV Conversion:**
   - Select the folder containing your ODB files
   - Choose a destination folder for CSV output
   - Click "Convert ODB to CSV"

2. **CSV to ODB Conversion:**
   - Select the folder containing your CSV files
   - Choose a destination folder for ODB output
   - Click "Convert CSV to ODB"

## Development

### Building

```bash
dotnet build
```

### Testing

The application includes progress indicators that show which file is currently being processed, making it easy to monitor conversion progress.

## Migration from Old Libraries

If you're using the old separate libraries, they have been deprecated and moved to the `Obsolete/` directory. All classes are marked with `[Obsolete]` attributes that will guide you to the new consolidated `OOTPDatabaseConverter.Core` library.

See `Obsolete/README.md` for detailed migration instructions.

## License

This project is licensed under the GNU General Public License v2.0 or later.
