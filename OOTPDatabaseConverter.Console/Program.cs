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
                Console.WriteLine("3. Exit");
                Console.Write("Enter choice (1-3): ");

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
                converter.Start(new Progress<int>(percent => Console.WriteLine($"Progress: {percent}%")));
                
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
                converter.Start(new Progress<int>(percent => Console.WriteLine($"Progress: {percent}%")));
                
                Console.WriteLine($"Successfully converted '{inputPath}' to ODB file in '{outputDir}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting CSV to ODB: {ex.Message}");
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
