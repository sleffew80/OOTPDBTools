#region File Description
//---------------------------------------------------------------------------
//
// File: Program.cs
// Author: Steven Leffew
// Copyright: (C) 2021-2024
// Description: Main entry point for OOTP Database Converter Console Application.
//              Provides both command-line and interactive modes for converting
//              OOTP database files between ODB and CSV formats.
//
//---------------------------------------------------------------------------
#endregion

#region License Info
//---------------------------------------------------------------------------
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//---------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.IO;
using OOTPDatabaseConverter.Core;
#endregion

namespace OOTPDatabaseConverter
{
    /// <summary>
    /// Main program class for the OOTP Database Converter Console Application.
    /// Provides functionality to convert OOTP database files between ODB and CSV formats,
    /// copy OOTP data from Steam installations, and backup/copy ODB files.
    /// </summary>
    /// <remarks>
    /// This application supports two modes of operation:
    /// 1. Command-line mode: Direct execution with arguments
    /// 2. Interactive mode: Menu-driven interface for user interaction
    /// </remarks>
    class Program
    {
        /// <summary>
        /// Main entry point for the application.
        /// Determines the mode of operation based on command-line arguments.
        /// </summary>
        /// <param name="args">Command-line arguments. If provided, runs in command-line mode.
        /// If empty, runs in interactive mode.</param>
        /// <remarks>
        /// Command-line format: OOTPDatabaseConverter [operation] [input-path] [output-path]
        /// Examples:
        /// - OOTPDatabaseConverter odb2csv /path/to/database.odb
        /// - OOTPDatabaseConverter csv2odb /path/to/data.csv /output/directory
        /// - OOTPDatabaseConverter (no args = interactive mode)
        /// </remarks>
        static void Main(string[] args)
        {
            // Display application header
            Console.WriteLine("OOTP Database Converter v5.0");
            Console.WriteLine("================================");
            Console.WriteLine();

            // Determine operation mode based on arguments
            if (args.Length > 0)
            {
                // Command line mode - process arguments directly
                ProcessCommandLine(args);
            }
            else
            {
                // Interactive mode - present menu-driven interface
                RunInteractiveMode();
            }
        }

        /// <summary>
        /// Processes command-line arguments and executes the requested operation.
        /// </summary>
        /// <param name="args">Array of command-line arguments</param>
        /// <remarks>
        /// Expected argument format:
        /// args[0]: Operation type (odb2csv, csv2odb, etc.)
        /// args[1]: Input path (required)
        /// args[2]: Output path (optional)
        /// </remarks>
        private static void ProcessCommandLine(string[] args)
        {
            // Validate minimum argument count
            if (args.Length < 2)
            {
                ShowUsage();
                return;
            }

            // Extract and normalize arguments
            string operation = args[0].ToLower();
            string inputPath = args[1];
            string outputPath = args.Length > 2 ? args[2] : "";

            try
            {
                // Route to appropriate conversion method based on operation
                switch (operation)
                {
                    case "odb2csv":
                    case "odb-to-csv":
                        ConvertOdbToCsv(inputPath, outputPath);
                        break;
                    case "csv2odb":
                    case "csv-to-odb":
                        ConvertCsvToOdb(inputPath, outputPath);
                        break;
                    default:
                        Console.WriteLine($"Unknown operation: {operation}");
                        ShowUsage();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Runs the interactive mode with a menu-driven interface.
        /// Provides a continuous loop until the user chooses to exit.
        /// </summary>
        /// <remarks>
        /// Interactive mode options:
        /// 1. Convert ODB to CSV - Convert OOTP database files to CSV format
        /// 2. Convert CSV to ODB - Convert CSV files back to OOTP database format
        /// 3. Copy OOTP Data - Copy ODB files from Steam installation
        /// 4. Backup & Copy ODB Files - Backup existing files and copy new ones
        /// 5. Exit - Terminate the application
        /// </remarks>
        private static void RunInteractiveMode()
        {
            while (true)
            {
                // Display main menu
                Console.WriteLine("Select operation:");
                Console.WriteLine("1. Convert ODB to CSV");
                Console.WriteLine("2. Convert CSV to ODB");
                Console.WriteLine("3. Copy OOTP Data (from Steam)");
                Console.WriteLine("4. Backup & Copy ODB Files");
                Console.WriteLine("5. Exit");
                Console.Write("Enter choice (1-5): ");

                // Get user input and process
                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        ConvertOdbToCsvInteractive();
                        break;
                    case "2":
                        ConvertCsvToOdbInteractive();
                        break;
                    case "3":
                        CopyOotpData();
                        break;
                    case "4":
                        BackupAndCopyOdbInteractive();
                        break;
                    case "5":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Interactive method for ODB to CSV conversion.
        /// Prompts user for input and output paths.
        /// </summary>
        /// <remarks>
        /// This method handles the interactive flow for ODB to CSV conversion:
        /// 1. Prompts for ODB file/directory path
        /// 2. Prompts for output directory (optional)
        /// 3. Validates input and calls the conversion method
        /// </remarks>
        private static void ConvertOdbToCsvInteractive()
        {
            // Get input path from user
            Console.Write("Enter ODB file path: ");
            string inputPath = Console.ReadLine()?.Trim() ?? "";
            
            // Validate input path
            if (string.IsNullOrEmpty(inputPath))
            {
                Console.WriteLine("Invalid input path.");
                return;
            }

            // Get output directory (optional)
            Console.Write("Enter output directory (press Enter for same directory): ");
            string outputDir = Console.ReadLine()?.Trim();
            
            // Execute conversion
            ConvertOdbToCsv(inputPath, outputDir);
        }

        /// <summary>
        /// Interactive method for CSV to ODB conversion.
        /// Prompts user for input and output paths.
        /// </summary>
        /// <remarks>
        /// This method handles the interactive flow for CSV to ODB conversion:
        /// 1. Prompts for CSV file/directory path
        /// 2. Prompts for output directory (optional)
        /// 3. Validates input and calls the conversion method
        /// </remarks>
        private static void ConvertCsvToOdbInteractive()
        {
            // Get input path from user
            Console.Write("Enter CSV file path: ");
            string inputPath = Console.ReadLine()?.Trim() ?? "";
            
            // Validate input path
            if (string.IsNullOrEmpty(inputPath))
            {
                Console.WriteLine("Invalid input path.");
                return;
            }

            // Get output directory (optional)
            Console.Write("Enter output directory (press Enter for same directory): ");
            string outputDir = Console.ReadLine()?.Trim();
            
            // Execute conversion
            ConvertCsvToOdb(inputPath, outputDir);
        }

        /// <summary>
        /// Converts ODB files to CSV format.
        /// </summary>
        /// <param name="inputPath">Path to the input ODB file or directory containing ODB files</param>
        /// <param name="outputPath">Path to the output directory for CSV files (optional)</param>
        /// <remarks>
        /// This method:
        /// 1. Validates the input path exists
        /// 2. Determines output directory (uses input directory if not specified)
        /// 3. Creates output directory if it doesn't exist
        /// 4. Initializes the OdbToCsv converter with progress reporting
        /// 5. Executes the conversion with real-time progress updates
        /// 6. Reports success or error status
        /// </remarks>
        private static void ConvertOdbToCsv(string inputPath, string outputPath)
        {
            // Validate input directory exists
            if (!Directory.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input directory '{inputPath}' not found.");
                return;
            }

            try
            {
                // Determine output directory
                string outputDir = string.IsNullOrEmpty(outputPath) 
                    ? Path.GetDirectoryName(inputPath) ?? "." 
                    : outputPath;

                // Create output directory if it doesn't exist
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Initialize converter with progress reporting
                var converter = new OdbToCsv(inputPath, outputDir);
                converter.Start(new Progress<OOTPDatabaseConverter.Core.Utilities.ProgressInfo>(info => 
                    Console.WriteLine($"Progress: {info.Percentage}% - {info.CurrentFile}")));
                
                // Report success
                Console.WriteLine($"Successfully converted '{inputPath}' to CSV files in '{outputDir}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting ODB to CSV: {ex.Message}");
            }
        }

        /// <summary>
        /// Converts CSV files to ODB format.
        /// </summary>
        /// <param name="inputPath">Path to the input CSV file or directory containing CSV files</param>
        /// <param name="outputPath">Path to the output directory for ODB files (optional)</param>
        /// <remarks>
        /// This method:
        /// 1. Validates the input path exists
        /// 2. Determines output directory (uses input directory if not specified)
        /// 3. Creates output directory if it doesn't exist
        /// 4. Initializes the CsvToOdb converter with progress reporting
        /// 5. Executes the conversion with real-time progress updates
        /// 6. Reports success or error status
        /// </remarks>
        private static void ConvertCsvToOdb(string inputPath, string outputPath)
        {
            // Validate input directory exists
            if (!Directory.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input directory '{inputPath}' not found.");
                return;
            }

            try
            {
                // Determine output directory
                string outputDir = string.IsNullOrEmpty(outputPath) 
                    ? Path.GetDirectoryName(inputPath) ?? "." 
                    : outputPath;

                // Create output directory if it doesn't exist
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Initialize converter with progress reporting
                var converter = new CsvToOdb(inputPath, outputDir);
                converter.Start(new Progress<OOTPDatabaseConverter.Core.Utilities.ProgressInfo>(info => 
                    Console.WriteLine($"Progress: {info.Percentage}% - {info.CurrentFile}")));
                
                // Report success
                Console.WriteLine($"Successfully converted '{inputPath}' to ODB file in '{outputDir}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting CSV to ODB: {ex.Message}");
            }
        }

        /// <summary>
        /// Copies OOTP data files from Steam installation to local test directories.
        /// </summary>
        /// <remarks>
        /// This method:
        /// 1. Determines the appropriate copy script based on operating system
        /// 2. Locates the script in the application directory or parent directory
        /// 3. Executes the script with proper process configuration
        /// 4. Captures and displays script output in real-time
        /// 5. Reports success or failure with helpful information
        /// 
        /// The script will:
        /// - Search for OOTP 26 installation in Steam directories
        /// - Copy ODB files to ./test-data directory
        /// - Provide information about available files and suggested output locations
        /// </remarks>
        private static void CopyOotpData()
        {
            Console.WriteLine("Copying OOTP data from Steam installation...");
            
            try
            {
                // Determine the script to run based on OS
                string scriptName = OperatingSystem.IsWindows() ? "copy-ootp-data.bat" : "copy-ootp-data.sh";
                string scriptPath = Path.Combine(AppContext.BaseDirectory, scriptName);

                // If script not found in base directory, look in parent directory
                if (!File.Exists(scriptPath))
                {
                    scriptPath = Path.Combine(Directory.GetParent(AppContext.BaseDirectory)?.FullName ?? "", scriptName);
                }

                // Validate script exists
                if (!File.Exists(scriptPath))
                {
                    Console.WriteLine("Error: Could not find copy-ootp-data script");
                    return;
                }

                // Configure process to run the script
                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = OperatingSystem.IsWindows() ? "cmd.exe" : "/bin/bash";
                process.StartInfo.Arguments = OperatingSystem.IsWindows() ? $"/c \"{scriptPath}\"" : scriptPath;
                process.StartInfo.WorkingDirectory = AppContext.BaseDirectory;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                // Set up output capture
                process.OutputDataReceived += (sender, e) => {
                    if (e.Data != null)
                    {
                        Console.WriteLine(e.Data);
                    }
                };

                process.ErrorDataReceived += (sender, e) => {
                    if (e.Data != null)
                    {
                        Console.WriteLine($"Error: {e.Data}");
                    }
                };

                // Execute script and capture output
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();

                // Report results
                if (process.ExitCode == 0)
                {
                    Console.WriteLine("OOTP data copied successfully!");
                    Console.WriteLine($"ODB files are now available in: {Path.Combine(AppContext.BaseDirectory, "test-data")}");
                    Console.WriteLine($"Suggested CSV output directory: {Path.Combine(AppContext.BaseDirectory, "test-csv-output")}");
                }
                else
                {
                    Console.WriteLine("Error copying OOTP data. Please check if OOTP 26 is installed via Steam.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Interactive method for backing up and copying ODB files.
        /// Prompts user for source and target directories.
        /// </summary>
        /// <remarks>
        /// This method handles the interactive flow for backup and copy operations:
        /// 1. Prompts for source directory (where converted ODB files are located)
        /// 2. Prompts for target directory (where original ODB files should be replaced)
        /// 3. Validates both directories exist
        /// 4. Calls the backup and copy method
        /// </remarks>
        private static void BackupAndCopyOdbInteractive()
        {
            // Get source directory from user
            Console.Write("Enter source directory (where converted ODB files are): ");
            string sourceDir = Console.ReadLine()?.Trim() ?? "";
            
            // Validate source directory
            if (string.IsNullOrEmpty(sourceDir) || !Directory.Exists(sourceDir))
            {
                Console.WriteLine("Invalid source directory.");
                return;
            }

            // Get target directory from user
            Console.Write("Enter target directory (where original ODB files are): ");
            string targetDir = Console.ReadLine()?.Trim() ?? "";
            
            // Validate target directory
            if (string.IsNullOrEmpty(targetDir) || !Directory.Exists(targetDir))
            {
                Console.WriteLine("Invalid target directory.");
                return;
            }

            // Execute backup and copy operation
            BackupAndCopyOdb(sourceDir, targetDir);
        }

        /// <summary>
        /// Backs up existing ODB files and copies new ones to replace them.
        /// </summary>
        /// <param name="sourceDir">Directory containing the new ODB files to copy</param>
        /// <param name="targetDir">Directory where original ODB files are located (will be backed up and replaced)</param>
        /// <remarks>
        /// This method performs the following operations for each ODB file:
        /// 1. Scans source directory for .odb files
        /// 2. For each file found:
        ///    - Creates a backup of the existing file (if it exists) with date suffix
        ///    - Copies the new file to replace the original
        /// 3. Reports progress and statistics for each operation
        /// 4. Provides summary of completed operations
        /// 
        /// Backup files are named with format: filename.yyyyMMdd.backup
        /// This ensures no data loss during the replacement process.
        /// </remarks>
        private static void BackupAndCopyOdb(string sourceDir, string targetDir)
        {
            Console.WriteLine("Backing up and copying ODB files...");
            
            try
            {
                // Get the current date for backup filename suffix
                string dateSuffix = DateTime.Now.ToString("yyyyMMdd");
                
                Console.WriteLine("Scanning for ODB files...");
                
                // Find all ODB files in the source directory
                var odbFiles = Directory.GetFiles(sourceDir, "*.odb");
                
                // Validate files were found
                if (odbFiles.Length == 0)
                {
                    Console.WriteLine("Error: No ODB files found in source directory");
                    return;
                }

                // Initialize counters for reporting
                int processedCount = 0;
                int backupCount = 0;
                int copyCount = 0;

                // Process each ODB file
                foreach (string sourceFile in odbFiles)
                {
                    string fileName = Path.GetFileName(sourceFile);
                    string targetFile = Path.Combine(targetDir, fileName);
                    string backupFile = Path.Combine(targetDir, $"{fileName}.{dateSuffix}.backup");

                    Console.WriteLine($"Processing: {fileName}");

                    // Check if target file exists and create backup
                    if (File.Exists(targetFile))
                    {
                        try
                        {
                            File.Copy(targetFile, backupFile, true);
                            backupCount++;
                            Console.WriteLine($"  Backed up: {fileName}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"  Warning: Could not backup {fileName}: {ex.Message}");
                        }
                    }

                    // Copy the new ODB file to replace the original
                    try
                    {
                        File.Copy(sourceFile, targetFile, true);
                        copyCount++;
                        Console.WriteLine($"  Copied: {fileName}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"  Error: Could not copy {fileName}: {ex.Message}");
                    }

                    processedCount++;
                }

                // Report final statistics
                Console.WriteLine($"Completed! Processed {processedCount} files. Backed up {backupCount} files. Copied {copyCount} files.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays usage information and command-line examples.
        /// </summary>
        /// <remarks>
        /// This method provides comprehensive help information including:
        /// - Command-line syntax
        /// - Available operations
        /// - Usage examples
        /// - Information about interactive mode
        /// </remarks>
        private static void ShowUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  OOTPDatabaseConverter <operation> <input-path> [output-path]");
            Console.WriteLine();
            Console.WriteLine("Operations:");
            Console.WriteLine("  odb2csv, odb-to-csv    Convert ODB file to CSV files");
            Console.WriteLine("  csv2odb, csv-to-odb    Convert CSV file to ODB file");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  OOTPDatabaseConverter odb2csv /path/to/database.odb");
            Console.WriteLine("  OOTPDatabaseConverter csv2odb /path/to/data.csv /output/directory");
            Console.WriteLine();
            Console.WriteLine("Run without arguments for interactive mode.");
        }
    }
}
