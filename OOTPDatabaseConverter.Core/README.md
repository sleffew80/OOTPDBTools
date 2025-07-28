# OOTP Database Converter - Core Library

This is the core library that provides the shared conversion logic for the OOTP Database Converter. It contains all the essential functionality for converting OOTP database files between ODB and CSV formats.

## Purpose

The Core library serves as the foundation for both the Console and GUI applications, providing:

- **Shared conversion logic** - Common code used by both Console and GUI applications
- **Database handling** - ODB file reading, writing, and manipulation
- **CSV processing** - CSV file generation and parsing
- **Version support** - Compatibility with OOTP versions 17 through 26
- **Utility functions** - Common helper methods and data structures

## Key Components

### **Database Converters**
- `HistoricalDatabaseConverter` - Main conversion logic for ODB↔CSV operations
- `HistoricalCsvConverter` - CSV-specific conversion utilities
- `StatsConverter` - Statistics data conversion handling

### **File Handlers**
- `CsvToOdb` - CSV to ODB conversion implementation
- `OdbToCsv` - ODB to CSV conversion implementation
- `FileNames` - File naming conventions and utilities

### **Utilities**
- `OdbVersion` - OOTP version detection and compatibility
- `Utilities` - Common helper functions and extensions

## Usage

This library is designed to be referenced by the Console and GUI applications:

```csharp
using OOTPDatabaseConverter.Core;

// Convert ODB to CSV
var converter = new HistoricalDatabaseConverter();
converter.ConvertOdbToCsv(inputPath, outputPath);

// Convert CSV to ODB
converter.ConvertCsvToOdb(inputPath, outputPath);
```

## Dependencies

- **.NET 8.0** - Target framework
- **No external dependencies** - Self-contained library

## Building

```bash
# Build the core library
dotnet build OOTPDatabaseConverter.Core

# Build as part of the solution
dotnet build
```

## Version Information

- **Version**: 5.0
- **Target Framework**: .NET 8.0
- **License**: GNU General Public License v2.0 or later

## Architecture

The Core library follows a clean architecture pattern:

- **Separation of Concerns** - Each class has a specific responsibility
- **Dependency Inversion** - High-level modules don't depend on low-level modules
- **Single Responsibility** - Each class handles one aspect of the conversion process
- **Testability** - Logic is separated from UI concerns for easier testing

This design ensures that the conversion logic can be easily shared between different user interfaces while maintaining clean, maintainable code. 