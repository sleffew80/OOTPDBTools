#region File Description
//---------------------------------------------------------------------------
//
// File: HistoricalCsvConverter.cs
// Author: Steven Leffew
// Copyright: (C) 2021-2024
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
using System.Text;
using OOTPDatabaseConverter.Core;
#endregion

namespace OOTPDatabaseConverter.Core
{
    /// <summary>
    /// Converts comma separated value(*.csv) files, in a Lahman Database style format, to OOTP Database(*.odb)
    /// files in OOTP's historical database format.
    /// </summary>
    public class HistoricalCsvConverter
    {
        #region Members
        private static string pathDelimiter = Utilities.FilePathDelimeter();
        private static int progressIncrement = 256;
        private static Byte zeroByte = 0;

        private FileNames fileNames;
        private String odbFolderDestination;
        private String csvFolderLocation;

        private int historicalDatabaseLineCount;
        private int historicalMinorDatabaseLineCount;
        private int historicalLineupsDatabaseLineCount;
        private int historicalTransactionsDatabaseLineCount;
        private int totalDatabaseLineCount;
        private int currentDatabaseLineNumber;
        private int nextProgressUpdate = progressIncrement;
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
                throw new Exception(ex.Message);
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
            historicalDatabaseLineCount = GetCsvCombinedLineCount(fileNames.HistoricalDatabaseAllCsvFileNames);
            historicalMinorDatabaseLineCount = GetCsvCombinedLineCount(fileNames.HistoricalMinorDatabaseAllCsvFileNames);
            historicalLineupsDatabaseLineCount = GetCsvLineCount(fileNames.LineupsFileName);
            historicalTransactionsDatabaseLineCount = GetCsvLineCount(fileNames.TransactionsFileName);
            totalDatabaseLineCount = historicalDatabaseLineCount + historicalMinorDatabaseLineCount + historicalLineupsDatabaseLineCount + historicalTransactionsDatabaseLineCount;
        }

        /// <summary>
        /// Converts multiple comma separated value(*.csv) files into a single OOTP Database(*.odb) file.
        /// </summary>
        /// <remarks>Used for "historical_database.odb" and "historical_minor_database.odb".</remarks>
        /// <param name="csvFileName">Array of comma separated value(*.csv) file names to be converted.</param>
        /// <param name="odbFileName">Name of OOTP Database(*.odb) file to be created.</param>
        /// <param name="odbLineCount">Number of lines required for OOTP Database.</param>
        private void ConvertToMultiTableOdb(String[] csvFileName, String odbFileName, int odbLineCount, IProgress<Utilities.ProgressInfo> progress)
        {
            // Initialize local variables.
            int odbTable = 0;
            string csvLine;

            try
            {
                // Create a StreamReader for reading a csv file.
#if _WINDOWS_
                StreamReader csvReader = new StreamReader(csvFolderLocation + csvFileName[odbTable], Encoding.Latin1);
#else
                StreamReader csvReader = new StreamReader(csvFolderLocation + csvFileName[odbTable]);
#endif
                // Create a BinaryWriter to reconstruct an OOTP Database.
#if _WINDOWS_
                using (BinaryWriter writer = new BinaryWriter(File.Open(odbFolderDestination + odbFileName, FileMode.Create), Encoding.Latin1))
#else
                using (BinaryWriter writer = new BinaryWriter(File.Open(odbFolderDestination + odbFileName, FileMode.Create)))
#endif
                {
                    // First Byte in the database is always zero.
                    writer.Write(zeroByte);

                    // Write the number of total lines the database will have in the file header as a 32 bit integer.
                    // This number is the sum of all lines from all included csv files.
                    writer.Write((UInt32)odbLineCount);
                    // Write data into database for every csv file included in the "csvFileName" array.
                    while (odbTable < csvFileName.Length)
                    {
                        // Check for unused database table entries. Proceed only if we have an entry for the current table.
                        // Read csv data and write to database until end of current csv file is reached.
                        if (csvFileName[odbTable] != null)
                        {
                            // Read a line from the current csv file stream and write it into the database.
                            // Each database line starts with the current table number followed by the length 
                            // of the current line in chars and finally the actual string of char data.
                            while ((csvLine = csvReader.ReadLine()) != null)
                            {
                                // Write current table number.
                                writer.Write((Byte)odbTable);
                                // Write length of current database line string in chars.
                                writer.Write((UInt16)csvLine.Length);
                                // Write current database line string, replacing "," with "\t"(tab).
                                writer.Write(csvLine.Replace(",", "\t").ToCharArray());

                                // Update progress in increments set by "progressIncrement".
                                if (currentDatabaseLineNumber == nextProgressUpdate)
                                {
                                    // Report current progress to the progress bar.
                                    UpdateProgress(progress, currentDatabaseLineNumber, totalDatabaseLineCount, csvFileName[odbTable]);
                                    nextProgressUpdate += progressIncrement;
                                }
                                // Increment for tracking progress and reporting to the progress bar.
                                currentDatabaseLineNumber++;
                            }
                        }
                        // Increment table identifier for the next csv file.
                        odbTable++;

                        // Check to see if there are more tables to process or if all have been completed.
                        // If there are more tables, close current csv file stream and initialize the next.
                        if (odbTable < csvFileName.Length)
                        {
                            // If database entry is unused, do nothing. Otherwise, prepare for the next.
                            if (csvFileName[odbTable] != null)
                            {
                                csvReader.Close();
                                csvReader = new StreamReader(csvFolderLocation + csvFileName[odbTable]);
                            }
                        }
                    }
                    // Close BinaryWriter stream.
                    writer.Close();
                }
                // Close csv StreamReader stream.
                csvReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Converts a single comma separated value(*.csv) file into an OOTP Database(*.odb) file.
        /// </summary>
        /// <remarks>Used for "historical_lineups.odb" and "historical_transactions.odb".</remarks>
        /// <param name="csvFileName">Comma separated value(*.csv) file name to be converted.</param>
        /// <param name="odbFileName">Name of OOTP Database(*.odb) file to be created.</param>
        /// <param name="odbLineCount">Number of lines required for OOTP Database.</param>
        private void ConvertToSingleTableOdb(String csvFileName, String odbFileName, int odbLineCount, IProgress<Utilities.ProgressInfo> progress)
        {
            string csvLine;

            try
            {
                // Create a StreamReader for reading a csv file.
#if _WINDOWS_
                StreamReader csvReader = new StreamReader(csvFolderLocation + csvFileName, Encoding.Latin1);
#else
                StreamReader csvReader = new StreamReader(csvFolderLocation + csvFileName);
#endif
                // Create a BinaryWriter to reconstruct an OOTP Database.
#if _WINDOWS_
                using (BinaryWriter writer = new BinaryWriter(File.Open(odbFolderDestination + odbFileName, FileMode.Create), System.Text.Encoding.Latin1))
#else
                using (BinaryWriter writer = new BinaryWriter(File.Open(odbFolderDestination + odbFileName, FileMode.Create)))
#endif
                {
                    // First Byte in the database is always zero.
                    writer.Write(zeroByte);

                    // Write the number of total lines the database will have in the file header as a 32 bit integer.
                    // This number is the total lines in the current csv file.
                    writer.Write((UInt32)odbLineCount);

                    // Read a line from the current csv file stream and write it into the database.
                    // Each database line starts with a zero since this database contains only one tabel.
                    // Next is the length of the current line in chars and finally the actual string of char data.
                    while ((csvLine = csvReader.ReadLine()) != null)
                    {
                        // Write current table number.
                        writer.Write(zeroByte);
                        // Write length of current database line string in chars.
                        writer.Write((UInt16)csvLine.Length);
                        // Write current database line string, replacing "," with "\t"(tab).
                        writer.Write(csvLine.Replace(",", "\t").ToCharArray());

                        // Update progress in increments set by "progressIncrement".
                        if (currentDatabaseLineNumber == nextProgressUpdate)
                        {
                            // Report current progress to the progress bar.
                            UpdateProgress(progress, currentDatabaseLineNumber, totalDatabaseLineCount, csvFileName);
                            nextProgressUpdate += progressIncrement;
                        }
                        // Increment for tracking progress and reporting to the progress bar.
                        currentDatabaseLineNumber++;
                    }
                    // Close the BinaryWriter stream.
                    writer.Close();
                }
                // Close csv StreamReader stream.
                csvReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update and report completion progress.
        /// </summary>
        /// <param name="progress">Interface for progress updates.</param>
        /// <param name="currentProgress">Current progress (gets converted to a scale of 100).</param>
        /// <param name="maxProgress">Maximum progress required for completion (gets converted to a scale of 100).</param>
        /// <param name="currentFile">Current file being processed.</param>
        private void UpdateProgress(IProgress<Utilities.ProgressInfo> progress, int currentProgress, int maxProgress, string currentFile)
        {
            if (progress != null)
                progress.Report(new Utilities.ProgressInfo 
                { 
                    Percentage = currentProgress * 100 / maxProgress,
                    CurrentFile = currentFile
                });
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
        public HistoricalCsvConverter(FileNames fileNames, String csvFolderLocation, String odbFolderDestination)
        {
            this.odbFolderDestination = odbFolderDestination + pathDelimiter;
            this.csvFolderLocation = csvFolderLocation + pathDelimiter;

            this.fileNames = fileNames;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Converts required comma separated value(*.csv) files to OOTP historical database files while reporting progress.
        /// </summary>
        /// <param name="progress">Interface for progress updates.</param>
        public void ToOdb(IProgress<Utilities.ProgressInfo> progress)
        {
            lock (this)
            {
                SetOdbLineCounts();
                ConvertToMultiTableOdb(fileNames.HistoricalDatabaseAllCsvFileNames, fileNames.HistoricalDatabaseFileName, historicalDatabaseLineCount, progress);
                ConvertToMultiTableOdb(fileNames.HistoricalMinorDatabaseAllCsvFileNames, fileNames.HistoricalMinorDatabaseFileName, historicalMinorDatabaseLineCount, progress);
                ConvertToSingleTableOdb(fileNames.LineupsFileName, fileNames.HistoricalLineupsDatabaseFileName, historicalLineupsDatabaseLineCount, progress);
                ConvertToSingleTableOdb(fileNames.TransactionsFileName, fileNames.HistoricalTransactionsDatabaseFileName, historicalTransactionsDatabaseLineCount, progress);
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
                while (currentCsvFile < fileNames.ExternalAllCsvFileNames.Length)
                {
                    File.Copy(csvFolderLocation + fileNames.ExternalAllCsvFileNames[currentCsvFile], odbFolderDestination + fileNames.ExternalAllCsvFileNames[currentCsvFile]);
                    currentCsvFile++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
