# Obsolete Libraries

This directory contains deprecated libraries that have been consolidated into the `OOTPDatabaseConverter.Core` library.

## Deprecated Libraries

### CSVtoODB.Library & CSVtoODB.Shared
- **Purpose**: CSV to ODB conversion functionality
- **Replacement**: `OOTPDatabaseConverter.Core.CsvToOdb` and `OOTPDatabaseConverter.Core.HistoricalCsvConverter`
- **Classes**: `CsvToOdb`, `HistoricalCsvConverter`

### ODBtoCSV.Library & ODBtoCSV.Shared
- **Purpose**: ODB to CSV conversion functionality
- **Replacement**: `OOTPDatabaseConverter.Core.OdbToCsv` and `OOTPDatabaseConverter.Core.HistoricalDatabaseConverter`
- **Classes**: `OdbToCsv`, `HistoricalDatabaseConverter`, `StatsConverter`

### Utilities.Library & Utilities.Shared
- **Purpose**: Cross-platform utility methods
- **Replacement**: `OOTPDatabaseConverter.Core.Utilities`
- **Classes**: `Utilities`

### OOTPCommon.Library & OOTPCommon.Shared
- **Purpose**: Common OOTP database file names and version information
- **Replacement**: `OOTPDatabaseConverter.Core.FileNames` and `OOTPDatabaseConverter.Core.OdbVersion`
- **Classes**: `FileNames`, `OdbVersion`

## Migration Guide

All classes in these deprecated libraries have been marked with the `[Obsolete]` attribute. When you compile code that uses these libraries, you will receive compiler warnings directing you to the new consolidated classes in `OOTPDatabaseConverter.Core`.

### Example Migration

**Old Code:**
```csharp
using CSVtoODB;
using ODBtoCSV;
using Utilities;
using OOTPCommon;

var converter = new CsvToOdb(inputFolder, outputFolder);
var utility = Utilities.Utilities.FilePathDelimeter();
```

**New Code:**
```csharp
using OOTPDatabaseConverter.Core;

var converter = new CsvToOdb(inputFolder, outputFolder);
var utility = Utilities.FilePathDelimeter();
```

## Why This Change?

The consolidation of these separate libraries into `OOTPDatabaseConverter.Core` provides:
- **Simplified dependency management**: Single library instead of multiple
- **Better organization**: Related functionality grouped together
- **Easier maintenance**: Centralized codebase
- **Reduced complexity**: Fewer projects to manage

## Timeline

These libraries are marked as obsolete but will continue to function for backward compatibility. However, it is recommended to migrate to the new consolidated library as soon as possible. 