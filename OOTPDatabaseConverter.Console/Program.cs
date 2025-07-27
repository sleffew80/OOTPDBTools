using System;
using System.IO;
using OOTPDatabaseConverter.Core;

namespace OOTPDatabaseConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OOTP Database Converter v4.0.5");
            Console.WriteLine("================================");
            Console.WriteLine();

            if (args.Length > 0)
            {
                // Command line mode
                ProcessCommandLine(args);
            }
            else
            {
                // Interactive mode
                RunInteractiveMode();
            }
        }

        static void ProcessCommandLine(string[] args)
        {
            if (args.Length < 2)
            {
                ShowUsage();
                return;
            }

            string operation = args[0].ToLower();
            string inputPath = args[1];
            string outputPath = args.Length > 2 ? args[2] : "";

            try
            {
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

        static void RunInteractiveMode()
        {
            while (true)
            {
                Console.WriteLine("Select operation:");
                Console.WriteLine("1. Convert ODB to CSV");
                Console.WriteLine("2. Convert CSV to ODB");
                Console.WriteLine("3. Copy OOTP Data (from Steam)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice (1-4): ");

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
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void ConvertOdbToCsvInteractive()
        {
            Console.Write("Enter ODB file path: ");
            string inputPath = Console.ReadLine()?.Trim() ?? "";
            
            if (string.IsNullOrEmpty(inputPath))
            {
                Console.WriteLine("Invalid input path.");
                return;
            }

            Console.Write("Enter output directory (press Enter for same directory): ");
            string outputDir = Console.ReadLine()?.Trim();
            
            ConvertOdbToCsv(inputPath, outputDir);
        }

        static void ConvertCsvToOdbInteractive()
        {
            Console.Write("Enter CSV file path: ");
            string inputPath = Console.ReadLine()?.Trim() ?? "";
            
            if (string.IsNullOrEmpty(inputPath))
            {
                Console.WriteLine("Invalid input path.");
                return;
            }

            Console.Write("Enter output directory (press Enter for same directory): ");
            string outputDir = Console.ReadLine()?.Trim();
            
            ConvertCsvToOdb(inputPath, outputDir);
        }

        static void ConvertOdbToCsv(string inputPath, string outputPath)
        {
            if (!Directory.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input directory '{inputPath}' not found.");
                return;
            }

            try
            {
                string outputDir = string.IsNullOrEmpty(outputPath) 
                    ? Path.GetDirectoryName(inputPath) ?? "." 
                    : outputPath;

                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                var converter = new OdbToCsv(inputPath, outputDir);
                converter.Start(new Progress<OOTPDatabaseConverter.Core.Utilities.ProgressInfo>(info => 
                    Console.WriteLine($"Progress: {info.Percentage}% - {info.CurrentFile}")));
                
                Console.WriteLine($"Successfully converted '{inputPath}' to CSV files in '{outputDir}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting ODB to CSV: {ex.Message}");
            }
        }

        static void ConvertCsvToOdb(string inputPath, string outputPath)
        {
            if (!Directory.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input directory '{inputPath}' not found.");
                return;
            }

            try
            {
                string outputDir = string.IsNullOrEmpty(outputPath) 
                    ? Path.GetDirectoryName(inputPath) ?? "." 
                    : outputPath;

                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                var converter = new CsvToOdb(inputPath, outputDir);
                converter.Start(new Progress<OOTPDatabaseConverter.Core.Utilities.ProgressInfo>(info => 
                    Console.WriteLine($"Progress: {info.Percentage}% - {info.CurrentFile}")));
                
                Console.WriteLine($"Successfully converted '{inputPath}' to ODB file in '{outputDir}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting CSV to ODB: {ex.Message}");
            }
        }

        static void CopyOotpData()
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

                if (!File.Exists(scriptPath))
                {
                    Console.WriteLine("Error: Could not find copy-ootp-data script");
                    return;
                }

                // Create process to run the script
                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = OperatingSystem.IsWindows() ? "cmd.exe" : "/bin/bash";
                process.StartInfo.Arguments = OperatingSystem.IsWindows() ? $"/c \"{scriptPath}\"" : scriptPath;
                process.StartInfo.WorkingDirectory = AppContext.BaseDirectory;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

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

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();

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

        static void ShowUsage()
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
