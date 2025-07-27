# OOTP Database Converter - Console Version

This is the cross-platform console version of the OOTP Database Converter, built with .NET 8.0.

## Requirements

- .NET 8.0 Runtime
- Any operating system (Windows, macOS, Linux)

## Installation

1. Ensure you have .NET 8.0 installed on your system
2. Clone or download this repository
3. Navigate to the project directory

## Usage

### Command Line Mode

```bash
# Convert ODB to CSV
dotnet run --project OOTPDatabaseConverter.Console -- odb2csv /path/to/database.odb

# Convert CSV to ODB
dotnet run --project OOTPDatabaseConverter.Console -- csv2odb /path/to/data.csv /output/directory

# Show help
dotnet run --project OOTPDatabaseConverter.Console -- --help
```

### Interactive Mode

Run without arguments to use the interactive interface:

```bash
dotnet run --project OOTPDatabaseConverter.Console
```

### Building

To build the application:

```bash
dotnet build OOTPDatabaseConverter.Console
```

To build a self-contained executable:

```bash
dotnet publish OOTPDatabaseConverter.Console -c Release -r linux-x64 --self-contained
```

## Features

- Convert OOTP Database (.odb) files to CSV format
- Convert CSV files back to OOTP Database (.odb) format
- Supports OOTP versions 17 through 26
- Command-line and interactive modes
- Progress reporting during conversion

## Notes

- This version uses the same core conversion logic as the Windows and macOS versions
- The application is console-based for cross-platform compatibility
- All shared libraries and utilities are included 