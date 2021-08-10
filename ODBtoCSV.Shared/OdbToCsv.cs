#region File Description
//---------------------------------------------------------------------------
//
// File: OdbToCsv.cs
// Author: Steven Leffew
// Copyright: (C) 2021
// Description: OOTP Database(*.odb) to Comma Separated Value(*.csv)
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

namespace ODBtoCSV
{
    /// <summary>
    /// Converts OOTP Database(*.odb) files to comma separated value(*.csv) files.
    /// </summary>
    class OdbToCsv
    {
        #region Members
        private static string historicalDatabaseFileName = FileNames.HistoricalDatabaseFileName;
        private static string historicalLineupsFileName = FileNames.HistoricalLineupsDatabaseFileName;
        private static string historicalTransactionsFileName = FileNames.HistoricalTransactionsDatabaseFileName;
        private static string historicalMinorsDatabaseFileName = FileNames.HistoricalMinorDatabaseFileName;
        private static string statsFileName = FileNames.StatsDatabaseFileName;
        private static String[] historicalDatabaseAllCsvFileName = FileNames.HistoricalDatabaseAllCsvFileNames(true);
        private static String[] historicalMinorDatabaseAllCsvFileName = FileNames.HistoricalMinorDatabaseAllCsvFileNames(true);
        private static string historicalLineupsDatabaseCsvFileName = FileNames.LineupsFileName;
        private static string historicalTransactionsDatabaseCsvFileName = FileNames.TransactionsFileName;
        private static string pathDelimeter = Utilities.Utilities.FilePathDelimeter();
        private static string missingFileText = "Missing Files: ";

        private String inputFolder;
        private String outputFolder;
        private String historicalDatabaseFileLocation;
        private String historicalLineupsFileLocation;
        private String historicalTransactionsFileLocation;
        private String historicalMinorsDatabaseFileLocation;
        private String statsFileLocation;
        private String missingFileTextMessage;
        #endregion

        #region Helpers
        /// <summary>
        /// Verifies the existence of an OOTP Database(*.odb) file.
        /// </summary>
        /// <param name="odbFileLocation">Folder location of OOTP Database(*.odb) file.</param>
        /// <param name="odbFileName">OOTP Database(*.odb) file name.</param>
        private void VerifyOdbFile(String odbFileLocation, String odbFileName)
        {
            if (!File.Exists(odbFileLocation))
            {
                if (missingFileTextMessage == missingFileText)
                {
                    missingFileTextMessage = missingFileTextMessage + odbFileName;
                }
                else
                {
                    missingFileTextMessage = missingFileTextMessage + ", " + odbFileName;
                }
            }
        }

        /// <summary>
        /// Recursively verifies existence of all OOTP Database(*.odb) files.
        /// </summary>
        private bool VerifyAllFiles()
        {
            missingFileTextMessage = missingFileText;
            VerifyOdbFile(historicalDatabaseFileLocation, historicalDatabaseFileName);
            VerifyOdbFile(historicalMinorsDatabaseFileLocation, historicalMinorsDatabaseFileName);
            VerifyOdbFile(historicalLineupsFileLocation, historicalLineupsFileName);
            VerifyOdbFile(historicalTransactionsFileLocation, historicalTransactionsFileName);
            //VerifyOdbFile(statsFileLocation, statsFileName);

            if (missingFileTextMessage == missingFileText)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Update and report completion progress.
        /// </summary>
        /// <param name="progress">Interface for progress updates.</param>
        /// <param name="currentProgress">Current progress (gets converted to a scale of 100).</param>
        private void UpdateProgress(IProgress<int> progress, int currentProgress)
        {
            if (progress != null)
                progress.Report(currentProgress);
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <b>OdbToCsv</b> class for converting OOTP Database(*.odb) files
        /// to comma separated value(*.csv) files.
        /// </summary>
        /// <param name="inputFolder">Source folder for OOTP Database(*.odb) files to be converted.</param>
        /// <param name="outputFolder">Destination folder for new comma separated value(*.csv) files to be saved.</param>
        public OdbToCsv(String inputFolder, String outputFolder)
        {
            this.inputFolder = inputFolder;
            this.outputFolder = outputFolder;
            this.historicalDatabaseFileLocation = inputFolder + pathDelimeter + historicalDatabaseFileName;
            this.historicalMinorsDatabaseFileLocation = inputFolder + pathDelimeter + historicalMinorsDatabaseFileName;
            this.historicalLineupsFileLocation = inputFolder + pathDelimeter + historicalLineupsFileName;
            this.historicalTransactionsFileLocation = inputFolder + pathDelimeter + historicalTransactionsFileName;
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
                int currentProgress = 1;

                HistoricalDatabaseConverter historicalDatabaseConverter = new HistoricalDatabaseConverter(historicalDatabaseFileLocation, outputFolder, historicalDatabaseAllCsvFileName);
                HistoricalDatabaseConverter historicalMinorDatabaseConverter = new HistoricalDatabaseConverter(historicalMinorsDatabaseFileLocation, outputFolder, historicalMinorDatabaseAllCsvFileName);
                HistoricalDatabaseConverter historicalLineupsDatabaseConverter = new HistoricalDatabaseConverter(historicalLineupsFileLocation, outputFolder, new String[] { historicalLineupsDatabaseCsvFileName });
                HistoricalDatabaseConverter historicalTransactionsDatabaseConverter = new HistoricalDatabaseConverter(historicalTransactionsFileLocation, outputFolder, new String[] { historicalTransactionsDatabaseCsvFileName });
                //StatsConverter statsConverter = new StatsConverter(statsFileLocation, outputFolder);
                currentProgress += 4;
                UpdateProgress(progress, currentProgress);

                historicalDatabaseConverter.ToCsv();
                currentProgress += 25;
                UpdateProgress(progress, currentProgress);

                historicalMinorDatabaseConverter.ToCsv();
                currentProgress += 50;
                UpdateProgress(progress, currentProgress);

                historicalLineupsDatabaseConverter.ToCsv();
                currentProgress += 10;
                UpdateProgress(progress, currentProgress);

                historicalTransactionsDatabaseConverter.ToCsv();
                currentProgress += 10;
                UpdateProgress(progress, currentProgress);

                //statsConverter.ToCsv();
            }
            else
            {
                Utilities.Utilities.MessageAlert(missingFileTextMessage, "Missing Files!");
            }
        }
        #endregion
    }
}
