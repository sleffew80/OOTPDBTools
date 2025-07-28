# OOTP Database Converter - Console Version

Cross-platform console application for converting OOTP (Out of the Park Baseball) database files between ODB and CSV formats.

## Requirements

- .NET 8.0 Runtime
- Any operating system (Windows, macOS, Linux)

## Usage

### Command Line Mode

```bash
# Convert ODB to CSV
dotnet run --project OOTPDatabaseConverter.Console -- odb2csv /path/to/database.odb
dotnet run --project OOTPDatabaseConverter.Console -- odb-to-csv /path/to/database.odb

# Convert CSV to ODB
dotnet run --project OOTPDatabaseConverter.Console -- csv2odb /path/to/data.csv /output/directory
dotnet run --project OOTPDatabaseConverter.Console -- csv-to-odb /path/to/data.csv /output/directory

# Show help
dotnet run --project OOTPDatabaseConverter.Console -- --help
```

### Interactive Mode

Run without arguments for a menu-driven interface:

```bash
dotnet run --project OOTPDatabaseConverter.Console
```

**Menu Options:**
1. Convert ODB to CSV
2. Convert CSV to ODB
3. Copy OOTP Data (from Steam)
4. Backup & Copy ODB Files
5. Exit

## Building

```bash
# Build the application
dotnet build OOTPDatabaseConverter.Console

# Build self-contained executable
dotnet publish OOTPDatabaseConverter.Console -c Release -r linux-x64 --self-contained
```

## Features

- Convert OOTP Database (.odb) files to CSV format
- Convert CSV files back to OOTP Database (.odb) format
- Supports OOTP versions 17 through 26
- Command-line and interactive modes
- Steam integration for automatic OOTP data detection
- Backup functionality for safe ODB file replacement
- Cross-platform compatibility

## License

This project is licensed under the GNU General Public License v2.0 or later. 