#region File Description
//---------------------------------------------------------------------------
//
// File: CsvToOdb.cs
// Author: Steven Leffew
// Copyright: (C) 2021-2024
// Description: Comma Separated Value(*.csv) to OOTP Database(*.odb) 
//              File Converter.
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

using OOTPCommon;
#endregion

namespace CSVtoODB
{
    /// <summary>
    /// Converts comma separated value(*.csv) files to OOTP Database(*.odb) files.
    /// </summary>
    [Obsolete("This class has been deprecated. Use OOTPDatabaseConverter.Core.CsvToOdb instead.")]
    public class CsvToOdb
    {
        #region Members
        private static string pathDelimeter = Utilities.Utilities.FilePathDelimeter();
        private static string missingFileText = "Missing Files: ";
        private FileNames fileNames;
        private String[] allCsvFileNames;
        private String configFileLocation;
        private String missingFileTextMessage;
        private String inputFolder;
        private String outputFolder;
        #endregion

        #region Helpers
        /// <summary>
        /// Reads a config file and initializes file names.
        /// </summary>
        /// <param name="fileName">Config file name.</param>
        private void ReadConfig(string fileName)
        {
            if (File.Exists(fileName))
            {
                StreamReader reader = File.OpenText(fileName);
                string[] csvFileNames = new string[32];
                string[] csvMinorFileNames = new string[32];

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.IndexOf("=", StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        string[] data = line.Split('=');
                        if (data.Length > 1)
                        {
                            string[] table = data[0].Split('_');
                            if (table.Length > 1)
                            {
                                if (table[0].Equals("Table"))
                                {
                                    if (Int32.TryParse(table[1], out int index))
                                    {
                                        csvFileNames[index] = data[1].Trim();
                                    }
                                }
                                if (table[0].Equals("MiLBTable"))
                                {
                                    if (Int32.TryParse(table[1], out int index))
                                    {
                                        csvMinorFileNames[index] = data[1].Trim();
                                    }
                                }
                            }
                        }
                    }
                }
                reader.Close();
                fileNames = new FileNames(csvFileNames, csvMinorFileNames);
            }
            else
            {
                throw new Exception(fileName + " does not exist!");
            }
        }

        /// <summary>
        /// Verifies the existence of a comma separated value(*.csv) file.
        /// </summary>
        /// <param name="csvFileName">Comma separated value(*.csv) file name.</param>
        private void VerifyCsvFile(String csvFileName)
        {
            String csvFileLocation = inputFolder + pathDelimeter + csvFileName;
            if (!File.Exists(csvFileLocation))
            {
                if (missingFileTextMessage == missingFileText)
                {
                    missingFileTextMessage = missingFileTextMessage + csvFileName;
                }
                else
                {
                    missingFileTextMessage = missingFileTextMessage + ", " + csvFileName;
                }
            }
        }

        /// <summary>
        /// Recursively verifies existence of all required comma separated value(*.csv)
        /// files.
        /// </summary>
        /// <returns>Returns true if all required files are present.</returns>
        private bool VerifyAllFiles()
        {
            missingFileTextMessage = missingFileText;
            for (int i = 0; i < allCsvFileNames.Length; i++)
            {
                if (allCsvFileNames[i] != null)
                {
                    VerifyCsvFile(allCsvFileNames[i]);
                }
            }
            if (missingFileTextMessage == missingFileText)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <b>CsvToOdb</b> class for converting comma separated value(*.csv) files
        /// to OOTP Database(*.odb) files.
        /// </summary>
        /// <param name="inputFolder">Source folder for comma separated value(*.csv) files to be converted.</param>
        /// <param name="outputFolder">Destination folder for new OOTP Database(*.odb) files to be saved.</param>
        public CsvToOdb(String inputFolder, String outputFolder)
        {
            this.inputFolder = inputFolder;
            this.outputFolder = outputFolder;
            this.configFileLocation = inputFolder + pathDelimeter + "DatabaseConfig.txt";

            ReadConfig(configFileLocation);

            allCsvFileNames = fileNames.AllCsvFileNames;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Starts the database conversion process and reports completion progress.
        /// </summary>
        /// <param name="progress">Interface for progress updates.</param>
        public void Start(IProgress<int> progress)
        {
            if (VerifyAllFiles() == true)
            {
                HistoricalCsvConverter converter = new HistoricalCsvConverter(fileNames, inputFolder, outputFolder);
                converter.ToOdb(progress);
                converter.CopyRequiredFiles();
            }
            else
            {
                throw new Exception(missingFileTextMessage + ".");
            }
        }
        #endregion
    }
}
