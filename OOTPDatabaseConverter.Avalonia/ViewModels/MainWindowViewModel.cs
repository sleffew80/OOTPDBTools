#region File Description
//---------------------------------------------------------------------------
//
// File: MainWindowViewModel.cs
// Author: Steven Leffew
// Copyright: (C) 2021-2024
// Description: Main ViewModel for OOTP Database Converter Avalonia UI.
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OOTPDatabaseConverter.Core;

namespace OOTPDatabaseConverter.Avalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _odbFileLocation = "";

        [ObservableProperty]
        private string _csvFileLocation = "";

        [ObservableProperty]
        private string _odbFileDestination = "";

        [ObservableProperty]
        private string _csvFileDestination = "";

        [ObservableProperty]
        private bool _isOdbToCsvConverting = false;

        [ObservableProperty]
        private bool _isCsvToOdbConverting = false;

        [ObservableProperty]
        private int _odbToCsvProgress = 0;

        [ObservableProperty]
        private int _csvToOdbProgress = 0;

        [ObservableProperty]
        private string _odbToCsvStatus = "";

        [ObservableProperty]
        private string _csvToOdbStatus = "";

        [ObservableProperty]
        private string _version = "";

        [ObservableProperty]
        private string _copyOotpDataStatus = "";

        [ObservableProperty]
        private bool _isCopyingOotpData = false;

        [ObservableProperty]
        private string _backupAndCopyOdbStatus = "";

        [ObservableProperty]
        private bool _isBackingUpAndCopyingOdb = false;


        public MainWindowViewModel()
        {
            Version = "Version: " + GetAssemblyFileVersion();
        }

        // Helper method to safely update UI properties from background threads
        private async Task UpdateUIAsync(Action action)
        {
            await Dispatcher.UIThread.InvokeAsync(action);
        }

        // Method to set the storage provider (called from the view)
        public void SetStorageProvider(IStorageProvider storageProvider)
        {
            // This method is kept for compatibility but we'll get the provider directly
        }

        [RelayCommand]
        private async Task BrowseOdbLocation()
        {
            await BrowseFolder("Select ODB Files Location", path => OdbFileLocation = path, 
                () => OdbToCsvStatus = "Error: Please select ODB Files Location");
        }

        [RelayCommand]
        private async Task BrowseCsvDestination()
        {
            await BrowseFolder("Select CSV Files Destination", path => CsvFileDestination = path,
                () => OdbToCsvStatus = "Error: Please select CSV Files Destination");
        }

        [RelayCommand]
        private async Task BrowseCsvLocation()
        {
            await BrowseFolder("Select CSV Files Location", path => CsvFileLocation = path,
                () => CsvToOdbStatus = "Error: Please select CSV Files Location");
        }

        [RelayCommand]
        private async Task BrowseOdbDestination()
        {
            await BrowseFolder("Select ODB Files Destination", path => OdbFileDestination = path,
                () => CsvToOdbStatus = "Error: Please select ODB Files Destination");
        }

        private async Task BrowseFolder(string title, Action<string> setPath, Action setError)
        {
            try
            {
                // Get the storage provider from the current TopLevel
                var app = Application.Current;
                if (app?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
                {
                    setError();
                    return;
                }

                var mainWindow = desktop.MainWindow;
                if (mainWindow == null)
                {
                    setError();
                    return;
                }

                var topLevel = TopLevel.GetTopLevel(mainWindow);
                if (topLevel?.StorageProvider == null)
                {
                    setError();
                    return;
                }

                var options = new FolderPickerOpenOptions
                {
                    Title = title,
                    AllowMultiple = false
                };

                var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(options);
                if (folders.Count > 0)
                {
                    setPath(folders[0].Path.LocalPath);
                }
            }
            catch (Exception ex)
            {
                // Set error message in the appropriate status field
                if (title.Contains("ODB") && title.Contains("Location"))
                    OdbToCsvStatus = $"Error selecting folder: {ex.Message}";
                else if (title.Contains("CSV") && title.Contains("Destination"))
                    OdbToCsvStatus = $"Error selecting folder: {ex.Message}";
                else if (title.Contains("CSV") && title.Contains("Location"))
                    CsvToOdbStatus = $"Error selecting folder: {ex.Message}";
                else if (title.Contains("ODB") && title.Contains("Destination"))
                    CsvToOdbStatus = $"Error selecting folder: {ex.Message}";
            }
        }

        [RelayCommand]
        private async Task ConvertOdbToCsv()
        {
            if (string.IsNullOrEmpty(OdbFileLocation))
            {
                OdbToCsvStatus = "Error: Please select ODB Files Location";
                return;
            }

            if (string.IsNullOrEmpty(CsvFileDestination))
            {
                OdbToCsvStatus = "Error: Please select CSV Files Destination";
                return;
            }

            IsOdbToCsvConverting = true;
            OdbToCsvProgress = 0;
            OdbToCsvStatus = "Starting ODB to CSV conversion...";

            try
            {
                var converter = new OdbToCsv(OdbFileLocation, CsvFileDestination);
                
                // Create progress reporter with filename support
                var progress = new Progress<OOTPDatabaseConverter.Core.Utilities.ProgressInfo>(info =>
                {
                    OdbToCsvProgress = info.Percentage;
                    OdbToCsvStatus = $"Converting {info.CurrentFile}... {info.Percentage}%";
                });

                // Run conversion
                await Task.Run(() => converter.Start(progress));
                
                OdbToCsvStatus = "Conversion completed successfully!";
                OdbToCsvProgress = 100;
            }
            catch (Exception ex)
            {
                OdbToCsvStatus = $"Conversion failed: {ex.Message}";
            }
            finally
            {
                IsOdbToCsvConverting = false;
            }
        }

        [RelayCommand]
        private async Task ConvertCsvToOdb()
        {
            if (string.IsNullOrEmpty(CsvFileLocation))
            {
                CsvToOdbStatus = "Error: Please select CSV Files Location";
                return;
            }

            if (string.IsNullOrEmpty(OdbFileDestination))
            {
                CsvToOdbStatus = "Error: Please select ODB Files Destination";
                return;
            }

            IsCsvToOdbConverting = true;
            CsvToOdbProgress = 0;
            CsvToOdbStatus = "Starting CSV to ODB conversion...";

            try
            {
                var converter = new CsvToOdb(CsvFileLocation, OdbFileDestination);
                
                // Create progress reporter with filename support
                var progress = new Progress<OOTPDatabaseConverter.Core.Utilities.ProgressInfo>(info =>
                {
                    CsvToOdbProgress = info.Percentage;
                    CsvToOdbStatus = $"Converting {info.CurrentFile}... {info.Percentage}%";
                });

                // Run conversion
                await Task.Run(() => converter.Start(progress));
                
                CsvToOdbStatus = "Conversion completed successfully!";
                CsvToOdbProgress = 100;
            }
            catch (Exception ex)
            {
                CsvToOdbStatus = $"Conversion failed: {ex.Message}";
            }
            finally
            {
                IsCsvToOdbConverting = false;
            }
        }

        [RelayCommand]
        private async Task CopyOotpData()
        {
            IsCopyingOotpData = true;
            CopyOotpDataStatus = "Copying OOTP data files...";

            try
            {
                await Task.Run(() =>
                {
                    // Common Steam OOTP installation paths
                    var possiblePaths = new List<string>();
                    
                    if (OperatingSystem.IsWindows())
                    {
                        possiblePaths.AddRange(new[]
                        {
                            @"C:\Program Files (x86)\Steam\steamapps\common\Out of the Park Baseball 26\data\stats",
                            @"C:\Program Files\Steam\steamapps\common\Out of the Park Baseball 26\data\stats",
                            @"D:\Program Files (x86)\Steam\steamapps\common\Out of the Park Baseball 26\data\stats",
                            @"D:\Program Files\Steam\steamapps\common\Out of the Park Baseball 26\data\stats"
                        });
                    }
                    else
                    {
                        possiblePaths.AddRange(new[]
                        {
                            "/media/all-the-things/Games/Steam/steamapps/common/Out of the Park Baseball 26/data/stats",
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".steam/steam/steamapps/common/Out of the Park Baseball 26/data/stats"),
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/Steam/steamapps/common/Out of the Park Baseball 26/data/stats")
                        });
                    }

                    // Find the first valid OOTP installation
                    string ootpPath = null;
                    foreach (var path in possiblePaths)
                    {
                        if (Directory.Exists(path))
                        {
                            ootpPath = path;
                            break;
                        }
                    }

                    if (ootpPath == null)
                    {
                        CopyOotpDataStatus = "Error: Could not find OOTP 26 installation. Please ensure it's installed via Steam.";
                        return;
                    }

                    CopyOotpDataStatus = $"Found OOTP installation at: {ootpPath}";

                    // Create target directory
                    string targetDir = Path.Combine(AppContext.BaseDirectory, "test-data");
                    if (!Directory.Exists(targetDir))
                    {
                        Directory.CreateDirectory(targetDir);
                        CopyOotpDataStatus = "Created test-data directory";
                    }

                    // Find and copy all ODB files
                    var odbFiles = Directory.GetFiles(ootpPath, "*.odb", SearchOption.AllDirectories);
                    
                    if (odbFiles.Length == 0)
                    {
                        CopyOotpDataStatus = "Error: No ODB files found in OOTP installation";
                        return;
                    }

                    CopyOotpDataStatus = $"Found {odbFiles.Length} ODB files, copying...";

                    int copiedCount = 0;
                    foreach (string sourceFile in odbFiles)
                    {
                        string fileName = Path.GetFileName(sourceFile);
                        string targetFile = Path.Combine(targetDir, fileName);
                        
                        try
                        {
                            File.Copy(sourceFile, targetFile, true);
                            copiedCount++;
                            CopyOotpDataStatus = $"Copied: {fileName} ({copiedCount}/{odbFiles.Length})";
                        }
                        catch (Exception ex)
                        {
                            CopyOotpDataStatus = $"Warning: Could not copy {fileName}: {ex.Message}";
                        }
                    }

                    if (copiedCount > 0)
                    {
                        CopyOotpDataStatus = $"Successfully copied {copiedCount} ODB files to test-data directory!";
                        
                        // Pre-fill the ODB to CSV fields
                        OdbFileLocation = targetDir;
                        CsvFileDestination = Path.Combine(AppContext.BaseDirectory, "test-csv-output");
                        
                        // Create output directory if it doesn't exist
                        if (!Directory.Exists(CsvFileDestination))
                        {
                            Directory.CreateDirectory(CsvFileDestination);
                        }
                    }
                    else
                    {
                        CopyOotpDataStatus = "Error: No files were copied successfully";
                    }
                });
            }
            catch (Exception ex)
            {
                CopyOotpDataStatus = $"Error: {ex.Message}";
            }
            finally
            {
                IsCopyingOotpData = false;
            }
        }

        [RelayCommand]
        private async Task BackupAndCopyOdb()
        {
            if (string.IsNullOrEmpty(OdbFileDestination))
            {
                BackupAndCopyOdbStatus = "Error: Please select ODB Files Destination first";
                return;
            }

            if (string.IsNullOrEmpty(CsvFileLocation))
            {
                BackupAndCopyOdbStatus = "Error: Please run CSV to ODB conversion first";
                return;
            }

            IsBackingUpAndCopyingOdb = true;
            BackupAndCopyOdbStatus = "Backing up and copying ODB files...";

            try
            {
                await Task.Run(() =>
                {
                    // Get the current date for backup filename
                    string dateSuffix = DateTime.Now.ToString("yyyyMMdd");
                    
                    // Get the source directory (where the converted ODB files are)
                    string sourceDir = OdbFileDestination;
                    string targetDir = CsvFileLocation; // This should be the original ODB location
                    
                    BackupAndCopyOdbStatus = "Scanning for ODB files...";
                    
                    // Find all ODB files in the source directory
                    var odbFiles = Directory.GetFiles(sourceDir, "*.odb");
                    
                    if (odbFiles.Length == 0)
                    {
                        BackupAndCopyOdbStatus = "Error: No ODB files found in destination directory";
                        return;
                    }

                    int processedCount = 0;
                    int backupCount = 0;
                    int copyCount = 0;

                    foreach (string sourceFile in odbFiles)
                    {
                        string fileName = Path.GetFileName(sourceFile);
                        string targetFile = Path.Combine(targetDir, fileName);
                        string backupFile = Path.Combine(targetDir, $"{fileName}.{dateSuffix}.backup");

                        BackupAndCopyOdbStatus = $"Processing: {fileName}";

                        // Check if target file exists and create backup
                        if (File.Exists(targetFile))
                        {
                            try
                            {
                                File.Copy(targetFile, backupFile, true);
                                backupCount++;
                                BackupAndCopyOdbStatus = $"Backed up: {fileName}";
                            }
                            catch (Exception ex)
                            {
                                BackupAndCopyOdbStatus = $"Warning: Could not backup {fileName}: {ex.Message}";
                            }
                        }

                        // Copy the new ODB file to replace the original
                        try
                        {
                            File.Copy(sourceFile, targetFile, true);
                            copyCount++;
                            BackupAndCopyOdbStatus = $"Copied: {fileName}";
                        }
                        catch (Exception ex)
                        {
                            BackupAndCopyOdbStatus = $"Error: Could not copy {fileName}: {ex.Message}";
                        }

                        processedCount++;
                    }

                    BackupAndCopyOdbStatus = $"Completed! Processed {processedCount} files. Backed up {backupCount} files. Copied {copyCount} files.";
                });
            }
            catch (Exception ex)
            {
                BackupAndCopyOdbStatus = $"Error: {ex.Message}";
            }
            finally
            {
                IsBackingUpAndCopyingOdb = false;
            }
        }

        private static string GetAssemblyFileVersion()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersion.FileVersion ?? "5.0";
        }
    }
}
