#region File Description
//---------------------------------------------------------------------------
//
// File: HistoricalDatabaseConverter.cs
// Author: Steven Leffew
// Copyright: (C) 2021-2024
// Description: OOTP Database(*.odb) to Comma Separated Value(*.csv)
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
    /// Converts OOTP Database(*.odb) files, in OOTP's historical database format, to 
    /// comma separated value(*.csv) files in a Lahman Database style format.
    /// </summary>
    public class HistoricalDatabaseConverter
    {
        #region Members
        // Private Member Variables
        private static String pathDelimiter = Utilities.FilePathDelimeter();
        private static long progressIncrement = 131072;

        private int odbVersion;
        private String odbFileLocation;
        private String csvFileDestination;
        private int odbBytePosition;
        private int odbFileSize;
        private Byte odbTable;
        private String[] csvFileName;
        private long nextProgressUpdate = progressIncrement;
        #endregion

        #region Helpers
        /// <summary>
        /// Update and report completion progress.
        /// </summary>
        /// <param name="progress">Interface for progress updates.</param>
        /// <param name="currentProgress">Current progress (gets converted to a scale of 100).</param>
        /// <param name="maxProgress">Maximum progress required for completion (gets converted to a scale of 100).</param>
        /// <param name="currentFile">Current file being processed.</param>
        private void UpdateProgress(IProgress<Utilities.ProgressInfo> progress, long currentProgress, long maxProgress, string currentFile)
        {
            if (progress != null)
                progress.Report(new Utilities.ProgressInfo 
                { 
                    Percentage = (int)(currentProgress * 100 / maxProgress),
                    CurrentFile = currentFile
                });
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <b>HistoricalDatabaseConverter</b> class for converting OOTP historical
        /// databases(*.odb) to comma separated value(*.csv) files. 
        /// <para>
        /// The resulting files are saved and formatted similarly to the original Lahman Database comma separated 
        /// value(*.csv) files they are based upon.
        /// </para>
        /// </summary>
        /// <remarks>
        /// Used for converting "historical_database.odb", "historical_minor_database.odb", "historical_lineups.odb",
        /// and "historical_transactions.odb" files. 
        /// </remarks>
        public HistoricalDatabaseConverter(String odbFileLocation, String csvFileDestination, String[] csvFileNames)
        {
            this.odbFileLocation = odbFileLocation;
            this.csvFileDestination = csvFileDestination + pathDelimiter;
            this.odbBytePosition = 0;
            this.odbFileSize = 0;
            this.odbTable = 0;
            this.csvFileName = csvFileNames;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Converts OOTP historical database files to comma separated value(*.csv) files.
        /// </summary>
        public void ToCsv(IProgress<Utilities.ProgressInfo> progress, long currentOverallByte, long combinedDatabasesfileSize, string odbFileName)
        {
            lock (this)
            {
                try
                {
                    // Create a StreamWriter for output with a 64MB buffer. Initialize to first table in the database.
#if _WINDOWS_
                    StreamWriter outputStream = new StreamWriter(csvFileDestination + csvFileName[odbTable], false, Encoding.Latin1);
#else
                    StreamWriter outputStream = new StreamWriter(csvFileDestination + csvFileName[odbTable], false, Encoding.ASCII);
#endif
                    // Create a FileStream for database file to be read.
                    FileStream inputStream = new FileStream(odbFileLocation, FileMode.Open,
                    FileAccess.Read, FileShare.ReadWrite);
                    // Create BinaryReader using FileStream object to read input Stream.
#if _WINDOWS_
                    using (BinaryReader reader = new BinaryReader(inputStream, Encoding.Latin1))
#else
                    using (BinaryReader reader = new BinaryReader(inputStream, Encoding.ASCII))
#endif
                    {
                        // Get the database file size in bytes.
                        odbFileSize = (int)reader.BaseStream.Length;

                        // Initialize local variables.
                        Byte currentTable = 0;
                        int stringLength = 0;
                        String databaseLine = null;
                        String formattedDatabaseLine = null;

                        // Skip first four bytes (file header).
                        while (odbBytePosition < 5)
                        {
                            reader.ReadByte();
                            odbBytePosition++;
                        }

                        // Read data until last byte is reached.
                        while (odbBytePosition < odbFileSize)
                        {
                            // Set the current table.
                            currentTable = reader.ReadByte();

                            // Check to see if we've reached a new table.
                            if (odbTable != currentTable)
                            {
                                // Increment table identifier.
                                odbTable = currentTable;
                                // Close the current csv file stream and then initialize the next.
                                outputStream.Close();
#if _WINDOWS_
                                outputStream = new StreamWriter(csvFileDestination + csvFileName[odbTable], false, Encoding.Latin1);
#else
                                outputStream = new StreamWriter(csvFileDestination + csvFileName[odbTable], false, Encoding.ASCII);
#endif
                            }

                            // Get the length of the current database line in chars.
                            stringLength = reader.ReadByte() + (reader.ReadByte() * 256);
                            odbBytePosition += 3;

                            // Read chars into "databaseLine" until last char is reached.
                            for (int i = 0; i < stringLength; i++)
                            {
                                databaseLine += reader.ReadChar();
                                odbBytePosition++;
                            }

                            // Update progress in increments set by "progressIncrement".
                            if (odbBytePosition >= nextProgressUpdate)
                            {
                                UpdateProgress(progress, currentOverallByte + odbBytePosition, combinedDatabasesfileSize, odbFileName);
                                nextProgressUpdate += progressIncrement;
                            }

                            // Replace "\t"(tabs) delimeters with ",".
                            formattedDatabaseLine = databaseLine.Replace("\t", ",");
                            
                            // Write the line to the current csv file stream.
                            outputStream.WriteLine(formattedDatabaseLine);

                            // Reset for the next line in the database.
                            databaseLine = null;
                            formattedDatabaseLine = null;
                        }
                        // Close Streams.
                        inputStream.Close();
                        outputStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);                 
                }
            }
        }
#endregion
    }
}
