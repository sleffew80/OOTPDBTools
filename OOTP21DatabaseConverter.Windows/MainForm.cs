#region File Description
//---------------------------------------------------------------------------
//
// File: MainForm.cs
// Author: Steven Leffew
// Copyright: (C) 2021
// Description: Windows WinForms GUI for OOTP Database Converter.
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
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using CSVtoODB;
using ODBtoCSV;

namespace OOTP21DatabaseConverter
{
    public partial class MainForm : Form
    {
        private String odbFileLocation = "";
        private String csvFileLocation = "";
        private String odbFileDestination = "";
        private String csvFileDestination = "";
        public MainForm()
        {
            InitializeComponent();
            versionLabel.Text = "Version: " + GetAssemblyFileVersion();
        }

        private void odbFileLocationButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "OOTP Database Files (*.odb)|*.odb";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                odbFileLocationTextBox.Text = Path.GetDirectoryName(openFileDialog.FileName);
                odbFileLocation = odbFileLocationTextBox.Text;
            }
        }

        private void csvFileDestinationButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Destination Folder";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                csvFileDestinationTextBox.Text = folderBrowserDialog.SelectedPath;
                csvFileDestination = folderBrowserDialog.SelectedPath;
            }
        }

        private void csvFileLocationButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Comma Separated Files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                csvFileLocationTextBox.Text = Path.GetDirectoryName(openFileDialog.FileName);
                csvFileLocation = csvFileLocationTextBox.Text;
            }
        }

        private void odbFileDestinationButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Destination Folder";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                odbFileDestinationTextBox.Text = folderBrowserDialog.SelectedPath;
                odbFileLocation = folderBrowserDialog.SelectedPath;
            }
        }

        private async void odbToCsvConvertButton_Click(object sender, EventArgs e)
        {
            if ((odbFileLocation.Length < 1) || (!Directory.Exists(odbFileLocation)))
                MessageBox.Show("Please enter a valid directory for ODB Files Location.", "Invalid Directory!");
            else if ((csvFileDestination.Length < 1) || (!Directory.Exists(csvFileDestination)))
                MessageBox.Show("Please enter a valid directory for CSV Files Destination.", "Invalid Directory!");
            else
            {
                OdbToCsv converter = new OdbToCsv(odbFileLocation, csvFileDestination);
                odbToCsvProgressBar.Visible = true;
                odbToCsvProgressBar.Maximum = 100;
                odbToCsvProgressBar.Step = 1;

                var progress = new Progress<int>(v =>
                {
                    // This lambda is executed in context of UI thread,
                    // so it can safely update form controls
                    odbToCsvProgressBar.Value = v;
                });

                await Task.Run(() => converter.Start(progress));

                odbToCsvProgressBar.Visible = false;
            }
        }

        private async void csvToOdbConvertButton_Click(object sender, EventArgs e)
        {
            if ((csvFileLocation.Length < 1) || (!Directory.Exists(csvFileLocation)))
                MessageBox.Show("Please enter a valid directory for CSV Files Location.", "Invalid Directory!");
            else if ((odbFileDestination.Length < 1) || (!Directory.Exists(odbFileDestination)))
                MessageBox.Show("Please enter a valid directory for ODB Files Destination.", "Invalid Directory!");
            else
            {
                CsvToOdb converter = new CsvToOdb(csvFileLocation, odbFileDestination);
                csvToOdbProgressBar.Visible = true;
                csvToOdbProgressBar.Maximum = 100;
                csvToOdbProgressBar.Step = 1;

                var progress = new Progress<int>(v =>
                {
                    // This lambda is executed in context of UI thread,
                    // so it can safely update form controls
                    csvToOdbProgressBar.Value = v;
                });

                await Task.Run(() => converter.Start(progress));

                csvToOdbProgressBar.Visible = false;
            }
        }

        private void odbFileLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            odbFileLocation = odbFileLocationTextBox.Text;
        }

        private void csvFileDestinationTextBox_TextChanged(object sender, EventArgs e)
        {
            csvFileDestination = csvFileDestinationTextBox.Text;
        }

        private void csvFileLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            csvFileLocation = csvFileLocationTextBox.Text;
        }

        private void odbFileDestinationTextBox_TextChanged(object sender, EventArgs e)
        {
            odbFileDestination = odbFileDestinationTextBox.Text;
        }

        public static string GetAssemblyFileVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersion.FileVersion;
        }
    }
}
