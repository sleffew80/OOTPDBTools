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


        public MainWindowViewModel()
        {
            Version = "Version: " + GetAssemblyFileVersion();
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
                    CopyOotpDataStatus = "Error: Could not find copy-ootp-data script";
                    return;
                }

                // Create process to run the script
                var process = new Process();
                process.StartInfo.FileName = OperatingSystem.IsWindows() ? "cmd.exe" : "/bin/bash";
                process.StartInfo.Arguments = OperatingSystem.IsWindows() ? $"/c \"{scriptPath}\"" : scriptPath;
                process.StartInfo.WorkingDirectory = AppContext.BaseDirectory;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                var output = new System.Text.StringBuilder();
                var error = new System.Text.StringBuilder();

                process.OutputDataReceived += (sender, e) => {
                    if (e.Data != null)
                    {
                        output.AppendLine(e.Data);
                        CopyOotpDataStatus = e.Data;
                    }
                };

                process.ErrorDataReceived += (sender, e) => {
                    if (e.Data != null)
                    {
                        error.AppendLine(e.Data);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await process.WaitForExitAsync();

                if (process.ExitCode == 0)
                {
                    CopyOotpDataStatus = "OOTP data copied successfully!";
                    
                    // Pre-fill the ODB to CSV fields
                    OdbFileLocation = Path.Combine(AppContext.BaseDirectory, "test-data");
                    CsvFileDestination = Path.Combine(AppContext.BaseDirectory, "test-csv-output");
                    
                    // Create output directory if it doesn't exist
                    if (!Directory.Exists(CsvFileDestination))
                    {
                        Directory.CreateDirectory(CsvFileDestination);
                    }
                }
                else
                {
                    CopyOotpDataStatus = $"Error copying OOTP data: {error.ToString()}";
                }
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

        private static string GetAssemblyFileVersion()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersion.FileVersion ?? "4.0.5";
        }
    }
}
