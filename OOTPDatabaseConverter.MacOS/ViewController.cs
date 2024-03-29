﻿#region File Description
//---------------------------------------------------------------------------
//
// File: ViewController.cs
// Author: Steven Leffew
// Copyright: (C) 2021-2024
// Description: MacOS View Controller for OOTP Database Converter GUI.
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
using System.IO;
using System.Threading.Tasks;

using AppKit;
using Foundation;

using ODBtoCSV;
using CSVtoODB;
using Utilities;

namespace OOTPDatabaseConverter
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            OdbToCsvProgressBar.Hidden = true;
            CsvToOdbProgressBar.Hidden = true;
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        partial void OdbToCsvConvertButton(Foundation.NSObject sender)
        {
            string homeFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string odbFileLocation = homeFolder + "/" + OdbFileLocationTextField.StringValue;
            string csvFileDestination = homeFolder + "/" + CsvFileDestinationTextField.StringValue;

            if (!Directory.Exists(odbFileLocation))
            {
                MessageAlert("Could not find '" + odbFileLocation + "'. Please enter a valid directory for ODB Files Location.", false);
            }
            else if (!Directory.Exists(csvFileDestination))
            {
                MessageAlert("Could not find '" + csvFileDestination + "'. Please enter a valid directory for CSV Files Destination.", false);
            }
            else
            {
                ConvertOdbToCsv(odbFileLocation, csvFileDestination);
            }
        }

        partial void CsvToOdbConvertButton(Foundation.NSObject sender)
        {
            string homeFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string csvFileLocation = homeFolder + "/" + CsvFileLocationTextField.StringValue;
            string odbFileDestination = homeFolder + "/" + OdbFileDestinationTextField.StringValue;

            if (!Directory.Exists(csvFileLocation))
            {
                MessageAlert("Could not find '" + csvFileLocation + "'. Please enter a valid directory for CSV Files Location.", false);
            }
            else if (!Directory.Exists(odbFileDestination))
            {
                MessageAlert("Could not find '" + odbFileDestination + "'. Please enter a valid directory for ODB Files Destination.", false);
            }
            else
            {
                ConvertCsvToOdb(csvFileLocation, odbFileDestination);
            }
        }

        async void ConvertOdbToCsv(string odbFileLocation, string csvFileDestination)
        {
            OdbToCsv converter = new OdbToCsv(odbFileLocation, csvFileDestination);
            OdbToCsvProgressBar.Hidden = false;

            var progress = new Progress<int>(v =>
            {
                OdbToCsvProgressBar.DoubleValue = (float)v;
            });

            try
            {
                await Task.Run(() => converter.Start(progress));
            }
            catch(Exception ex)
            {
                MessageAlert(ex.Message, true);
            }

            OdbToCsvProgressBar.Hidden = true;
        }

        async void ConvertCsvToOdb(string csvFileLocation, string odbFileDestination)
        {
            CsvToOdb converter = new CsvToOdb(csvFileLocation, odbFileDestination);
            CsvToOdbProgressBar.Hidden = false;

            var progress = new Progress<int>(v =>
            {
                CsvToOdbProgressBar.DoubleValue = (float)v;
            });

            try
            {
                await Task.Run(() => converter.Start(progress));
            }
            catch(Exception ex)
            {
                MessageAlert(ex.Message, true);
            }

            CsvToOdbProgressBar.Hidden = true;
        }

        public static void MessageAlert(string text, bool isCriticalError)
        {
            if (isCriticalError)
            {
                var alert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Critical,
                    InformativeText = text,
                    MessageText = "Error!",
                };
                alert.RunModal();
            }
            else
            {
                var alert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Warning,
                    InformativeText = text,
                    MessageText = "Warning",
                };
                alert.RunModal();
            }
        }

    }
}
