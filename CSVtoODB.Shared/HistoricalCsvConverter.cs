#region File Description
//---------------------------------------------------------------------------
//
// File: HistoricalCsvConverter.cs
// Author: Steven Leffew
// Copyright: (C) 2021
// Description: Comma Separated Value(*.csv) to OOTP Database(*.odb) 
//              File Converter for OOTP's "historical_X.odb files.
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
    /// Converts comma separated value(*.csv) files, in a Lahman Database style format, to OOTP Database(*.odb)
    /// files in OOTP's historical database format.
    /// </summary>
    public class HistoricalCsvConverter
    {
        #region Members
        private static String[] historicalDatabaseAllCsvFileNames = FileNames.HistoricalDatabaseAllCsvFileNames(false);
        private static String[] historicalMinorDatabaseAllCsvFileNames = FileNames.HistoricalMinorDatabaseAllCsvFileNames(false);
        private static String[] externalAllCsvFileNames = FileNames.ExternalAllCsvFileNames;
        private static string historicalLineupsCsvFileName = FileNames.LineupsFileName;
        private static string historicalTransactionsCsvFileName = FileNames.TransactionsFileName;
        private static string historicalDatabaseFileName = FileNames.HistoricalDatabaseFileName;
        private static string historicalMinorDatabaseFileName = FileNames.HistoricalMinorDatabaseFileName;
        private static string historicalLineupsDatabaseFileName = FileNames.HistoricalLineupsDatabaseFileName;
        private static string historicalTransactionsDatabaseFileName = FileNames.HistoricalTransactionsDatabaseFileName;
        private static string pathDelimiter = Utilities.Utilities.FilePathDelimeter();
        private static Byte zeroByte = 0;

        private String odbFolderDestination;
        private String csvFolderLocation;

        private int historicalDatabaseLineCount;
        private int historicalMinorDatabaseLineCount;
        private int historicalLineupsDatabaseLineCount;
        private int historicalTransactionsDatabaseLineCount;
        #endregion

        #region Helpers
        /// <summary>
        /// Gets the number of lines in a comma separated values(*.csv) file.
        /// </summary>
        /// <param name="csvFileName"></param>
        /// <returns>The number of lines in a comma separated values(*.csv) file.</returns>
        private int GetCsvLineCount(String csvFileName)
        {
            string csvLine;
            int csvLineCount = 0;
            StreamReader csvReader = new StreamReader(csvFolderLocation + csvFileName);
            try
            {
                while ((csvLine = csvReader.ReadLine()) != null)
                {
                    csvLineCount++;
                }
            }
            catch (Exception ex)
            {
                Utilities.Utilities.MessageAlert(ex.Message, "Error!");
            }
            csvReader.Close();
            return csvLineCount;
        }

        /// <summary>
        /// Gets the total number of lines within multiple comma separated values(*.csv) files.
        /// </summary>
        /// <param name="csvFileName"></param>
        /// <returns>The total number of lines of all comma separated values(*.csv) files contained in an array.</returns>
        private int GetCsvCombinedLineCount(String[] csvFileName)
        {
            int odbTable = 0;
            int csvCombinedLineCount = 0;
            while (odbTable < csvFileName.Length)
            {
                if (csvFileName[odbTable] != null)
                {
                    csvCombinedLineCount += GetCsvLineCount(csvFileName[odbTable]);
                }

                odbTable++;
            }
            return csvCombinedLineCount;
        }

        /// <summary>
        /// Gets comma separated value(*.csv) files linecounts and sets the approprite line counts 
        /// for lines necessary for resulting OOTP Database(*.odb) files.
        /// </summary>
        private void SetOdbLineCounts()
        {
            historicalDatabaseLineCount = GetCsvCombinedLineCount(historicalDatabaseAllCsvFileNames);
            historicalMinorDatabaseLineCount = GetCsvCombinedLineCount(historicalMinorDatabaseAllCsvFileNames);
            historicalLineupsDatabaseLineCount = GetCsvLineCount(historicalLineupsCsvFileName);
            historicalTransactionsDatabaseLineCount = GetCsvLineCount(historicalTransactionsCsvFileName);
        }

        /// <summary>
        /// Converts multiple comma separated value(*.csv) files into a single OOTP Database(*.odb) file.
        /// </summary>
        /// <remarks>Used for "historical_database.odb" and "historical_minor_database.odb".</remarks>
        /// <param name="csvFileName">Array of comma separated value(*.csv) file names to be converted.</param>
        /// <param name="odbFileName">Name of OOTP Database(*.odb) file to be created.</param>
        /// <param name="odbLineCount">Number of lines required for OOTP Database.</param>
        private void ConvertToMultiTableOdb(String[] csvFileName, String odbFileName, int odbLineCount)
        {
            int odbTable = 0;
            string csvLine;

            try
            {
                StreamReader csvReader = new StreamReader(csvFolderLocation + csvFileName[odbTable]);
                using (BinaryWriter writer = new BinaryWriter(File.Open(odbFolderDestination + odbFileName, FileMode.Create)))
                {
                    writer.Write(zeroByte);
                    writer.Write((UInt32)odbLineCount);
                    while (odbTable < csvFileName.Length)
                    {
                        if (csvFileName[odbTable] != null)
                        {
                            int csvLineCount = 0;

                            while ((csvLine = csvReader.ReadLine()) != null)
                            {
                                writer.Write((Byte)odbTable);
                                writer.Write((UInt16)csvLine.Length);
                                writer.Write(csvLine.Replace(",", "\t").ToCharArray());

                                csvLineCount++;
                            }
                        }
                        odbTable++;
                        if (odbTable < csvFileName.Length)
                        {
                            if (csvFileName[odbTable] != null)
                            {
                                csvReader.Close();
                                csvReader = new StreamReader(csvFolderLocation + csvFileName[odbTable]);
                            }
                        }
                    }
                    writer.Close();
                }
                csvReader.Close();
            }
            catch (Exception ex)
            {
                Utilities.Utilities.MessageAlert(ex.Message, "Error!");
            }
        }

        /// <summary>
        /// Converts a single comma separated value(*.csv) file into an OOTP Database(*.odb) file.
        /// </summary>
        /// <remarks>Used for "historical_lineups.odb" and "historical_transactions.odb".</remarks>
        /// <param name="csvFileName">Comma separated value(*.csv) file name to be converted.</param>
        /// <param name="odbFileName">Name of OOTP Database(*.odb) file to be created.</param>
        /// <param name="odbLineCount">Number of lines required for OOTP Database.</param>
        private void ConvertToSingleTableOdb(String csvFileName, String odbFileName, int odbLineCount)
        {
            string csvLine;

            try
            {
                StreamReader csvReader = new StreamReader(csvFolderLocation + csvFileName);
                using (BinaryWriter writer = new BinaryWriter(File.Open(odbFolderDestination + odbFileName, FileMode.Create)))
                {
                    int csvLineCount = 0;

                    writer.Write(zeroByte);
                    writer.Write((UInt32)odbLineCount);
                    while ((csvLine = csvReader.ReadLine()) != null)
                    {
                        writer.Write(zeroByte);
                        writer.Write((UInt16)csvLine.Length);
                        writer.Write(csvLine.Replace(",", "\t").ToCharArray());

                        csvLineCount++;
                    }
                    writer.Close();
                }
                csvReader.Close();
            }
            catch (Exception ex)
            {
                Utilities.Utilities.MessageAlert(ex.Message, "Error!");
            }
        }

        /// <summary>
        /// Update and report completion progress.
        /// </summary>
        /// <param name="progress">Interface for progress updates.</param>
        /// <param name="currentProgress">Current progress (gets converted to a scale of 100).</param>
        /// <param name="maxProgress">Maximum progress required for completion (gets converted to a scale of 100).</param>
        private void UpdateProgress(IProgress<int> progress, int currentProgress, int maxProgress)
        {
            if (progress != null)
                progress.Report(currentProgress * 100 / maxProgress);
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <b>HistoricalCsvConverter</b> class for converting comma separated 
        /// value(*.csv) files, in a Lahman Database style format, to OOTP historical databases(*.odb) files. 
        /// </summary>
        /// <remarks>
        /// Used for creating "historical_database.odb", "historical_minor_database.odb", "historical_lineups.odb", and
        /// "historical_transactions.odb" files. 
        /// </remarks>
        /// <param name="csvFolderLocation">Source folder for comma separated value(*.csv) files to be converted.</param>
        /// <param name="odbFolderDestination">Destination folder for new OOTP Database(*.odb) files to be saved.</param>
        public HistoricalCsvConverter(String csvFolderLocation, String odbFolderDestination)
        {
            this.odbFolderDestination = odbFolderDestination + pathDelimiter;
            this.csvFolderLocation = csvFolderLocation + pathDelimiter;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Converts required comma separated value(*.csv) files to OOTP historical database files while reporting progress.
        /// </summary>
        /// <param name="progress">Interface for progress updates.</param>
        public void ToOdb(IProgress<int> progress)
        {
            lock (this)
            {
                SetOdbLineCounts();
                int totalLineCount = historicalDatabaseLineCount + historicalMinorDatabaseLineCount + historicalLineupsDatabaseLineCount + historicalTransactionsDatabaseLineCount;
                int currentLineCount = 1;
                UpdateProgress(progress, currentLineCount, totalLineCount);
                ConvertToMultiTableOdb(historicalDatabaseAllCsvFileNames, historicalDatabaseFileName, historicalDatabaseLineCount);
                currentLineCount += historicalDatabaseLineCount;
                UpdateProgress(progress, currentLineCount, totalLineCount);
                ConvertToMultiTableOdb(historicalMinorDatabaseAllCsvFileNames, historicalMinorDatabaseFileName, historicalMinorDatabaseLineCount);
                currentLineCount += historicalMinorDatabaseLineCount;
                UpdateProgress(progress, currentLineCount, totalLineCount);
                ConvertToSingleTableOdb(historicalLineupsCsvFileName, historicalLineupsDatabaseFileName, historicalLineupsDatabaseLineCount);
                currentLineCount += historicalLineupsDatabaseLineCount;
                UpdateProgress(progress, currentLineCount, totalLineCount);
                ConvertToSingleTableOdb(historicalTransactionsCsvFileName, historicalTransactionsDatabaseFileName, historicalTransactionsDatabaseLineCount);
                currentLineCount += historicalTransactionsDatabaseLineCount;
                UpdateProgress(progress, currentLineCount, totalLineCount);
            }
        }

        /// <summary>
        /// Copy comma separated value(*.csv) files that exist outside of any OOTP Database(*.odb) files to the destination
        /// directory. 
        /// </summary>
        public void CopyRequiredFiles()
        {
            int currentCsvFile = 0;
            try
            {
                while (currentCsvFile < externalAllCsvFileNames.Length)
                {
                    File.Copy(csvFolderLocation + externalAllCsvFileNames[currentCsvFile], odbFolderDestination + externalAllCsvFileNames[currentCsvFile]);
                    currentCsvFile++;
                }
            }
            catch (Exception ex)
            {
                Utilities.Utilities.MessageAlert(ex.Message, "Error!");
            }
        }
        #endregion
    }
}
