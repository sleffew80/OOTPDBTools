#region File Description
//---------------------------------------------------------------------------
//
// File: OdbToCsv.cs
// Author: Steven Leffew
// Copyright: (C) 2021-2024
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
using System.Text;
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
        private static string pathDelimeter = Utilities.Utilities.FilePathDelimeter();
        private static string missingFileText = "Missing Files: ";

        private FileNames fileNames;

        private String inputFolder;
        private String outputFolder;
        private String configFileDestination;
        private String historicalDatabaseFileLocation;
        private String historicalLineupsFileLocation;
        private String historicalTransactionsFileLocation;
        private String historicalMinorsDatabaseFileLocation;
        private String statsFileLocation;
        private String missingFileTextMessage;

        private OdbVersion odbVersion;
        private int odbTableCount;
        private int odbMinorTableCount;
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
            VerifyOdbFile(historicalDatabaseFileLocation, fileNames.HistoricalDatabaseFileName);
            VerifyOdbFile(historicalMinorsDatabaseFileLocation, fileNames.HistoricalMinorDatabaseFileName);
            VerifyOdbFile(historicalLineupsFileLocation, fileNames.HistoricalLineupsDatabaseFileName);
            VerifyOdbFile(historicalTransactionsFileLocation, fileNames.HistoricalTransactionsDatabaseFileName);
            //VerifyOdbFile(statsFileLocation, fileNames.StatsFileName);

            if (missingFileTextMessage == missingFileText)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int GetTableCount(String odbFileLocation)
        {
            int odbBytePosition = 0;
            int odbFileSize = 0;
            Byte odbTable = 0;
            Boolean valueChecked = false;

            lock (this)
            {
                try
                {
                    // Create a FileStream for database file to be read.
                    FileStream inputStream = new FileStream(odbFileLocation, FileMode.Open,
                    FileAccess.Read, FileShare.Read);
                    // Create BinaryReader using FileStream object to read input Stream.
                    using (BinaryReader reader = new BinaryReader(inputStream, Encoding.ASCII))
                    {
                        // Get the database file size in bytes.
                        odbFileSize = (int)reader.BaseStream.Length;

                        // Initialize local variables.
                        Byte currentTable = 0;
                        int stringLength = 0;
                        String databaseLine = null;
                        String checkValue = null;

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
                            }

                            // Get the length of the current database line in chars.
                            stringLength = reader.ReadByte() + (reader.ReadByte() * 256);
                            odbBytePosition += 3;

                            // Iterate through chars of current line.
                            for (int i = 0; i < stringLength; i++)
                            {
                                reader.ReadChar();
                                odbBytePosition++;
                            }
                        }

                        // Close Streams.
                        inputStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return odbTable+1;
        }

        /// <summary>
        /// Quick and dirty way of determining the .odb file layout. 
        /// </summary>
        /// <param name="odbFileLocation">Folder location of OOTP Database(*.odb) file.</param>
        private OdbVersion GetDatabaseVersion(String odbFileLocation, String odbMinorFileLocation)
        {
            OdbVersion odbVersionNumber = OdbVersion.ODB_Err;
            int odbBytePosition = 0;
            int odbFileSize = 0;
            Byte odbTable = 0;
            Boolean valueChecked = false;

            lock (this)
            {
                try
                {
                    // Create a FileStream for database file to be read.
                    FileStream inputStream = new FileStream(odbFileLocation, FileMode.Open,
                    FileAccess.Read, FileShare.Read);
                    // Create BinaryReader using FileStream object to read input Stream.
                    using (BinaryReader reader = new BinaryReader(inputStream, Encoding.ASCII))
                    {
                        // Get the database file size in bytes.
                        odbFileSize = (int)reader.BaseStream.Length;

                        // Initialize local variables.
                        Byte currentTable = 0;
                        int stringLength = 0;
                        String databaseLine = null;
                        String checkValue = null;

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
                            }

                            // Get the length of the current database line in chars.
                            stringLength = reader.ReadByte() + (reader.ReadByte() * 256);
                            odbBytePosition += 3;

                            if ((odbTable == 6) && (!valueChecked))
                            {
                                // Read chars into "databaseLine" until last char is reached.
                                for (int i = 0; i < stringLength; i++)
                                {
                                    databaseLine += reader.ReadChar();
                                    odbBytePosition++;
                                }

                                // Get the column header at index 3.
                                checkValue = databaseLine.Split('\t')[3];

                                // Prior to OOTP 22, Table[6] contained "Fielding2" and this value would be "teamID".
                                // From OOTP 22 on, Table[6] contains "FieldingOF" and this value should then be "Glf".
                                if (checkValue.Equals("Glf", StringComparison.OrdinalIgnoreCase))
                                {
                                    odbVersionNumber = OdbVersion.ODB_22;
                                }
                                else if (checkValue.Equals("teamID", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Assume version 17 unless there are more than 21 tables indexed.
                                    odbVersionNumber = OdbVersion.ODB_17;
                                }
                                else
                                {
                                    odbVersionNumber = OdbVersion.ODB_Err;
                                }

                                valueChecked = true;
                            }

                            // OOTP 17 and 18 have 22 tables stored in the historical odb. OOTP 19, 20, and 21 have more (25 total).
                            if ((odbTable == 22) && (odbVersionNumber != OdbVersion.ODB_22))
                            {
                                odbVersionNumber = OdbVersion.ODB_19;
                            }

                            else
                            {
                                // Iterate through chars of current line.
                                for (int i = 0; i < stringLength; i++)
                                {
                                    reader.ReadChar();
                                    odbBytePosition++;
                                }
                            }
                        }

                        // Assign Table sizes.
                        if (odbVersionNumber == OdbVersion.ODB_17)
                        {
                            odbTableCount = odbTable + 1;
                            odbMinorTableCount = 22;
                        }
                        else if (odbVersionNumber == OdbVersion.ODB_19)
                        {
                            odbTableCount = odbTable + 1;
                            odbMinorTableCount = 22;
                        }
                        else if (odbVersionNumber == OdbVersion.ODB_22)
                        {
                            odbTableCount = odbTable + 1;
                            odbMinorTableCount = 26;
                        }
                        else
                        {
                            odbTableCount = odbTable + 1;
                            // If version unknown, manually count minor league table indexes.
                            odbMinorTableCount = GetTableCount(odbMinorFileLocation);
                        }

                        // Close Streams.
                        inputStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return odbVersionNumber;
        }

        /// <summary>
        /// Creates a configuration file which serves a recipe for reconstructing OOTP Database(*.odb) files from resulting
        /// comma separated value(*.csv) files.
        /// </summary>
        /// <param name="fileName">Name of and where to create configuration file.</param>
        private void WriteConfig(string fileName)
        {
            try 
            { 
                StreamWriter writer = File.CreateText(fileName);
                for (int i = 0; i < odbTableCount; i++)
                {
                    if (!String.IsNullOrEmpty(fileNames.HistoricalDatabaseAllCsvFileNames[i]))
                    {
                        writer.WriteLine("Table_" + i.ToString() + "=" + fileNames.HistoricalDatabaseAllCsvFileNames[i]);
                    }    
                }

                writer.WriteLine();

                for (int i = 0; i < odbMinorTableCount; i++)
                {
                    if (!String.IsNullOrEmpty(fileNames.HistoricalMinorDatabaseAllCsvFileNames[i]))
                    {
                        writer.WriteLine("MiLBTable_" + i.ToString() + "=" + fileNames.HistoricalMinorDatabaseAllCsvFileNames[i]);
                    }
                }
                writer.Close();
            }
            catch
            {
                throw new Exception("Error writing config file.");
            }
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
            String historicalFileName = "historical_database.odb";
            String historicalMinorFileName = "historical_minor_database.odb";

            this.inputFolder = inputFolder;
            this.outputFolder = outputFolder;

            this.historicalDatabaseFileLocation = inputFolder + pathDelimeter + historicalFileName;
            this.historicalMinorsDatabaseFileLocation = inputFolder + pathDelimeter + historicalMinorFileName;

            VerifyOdbFile(historicalDatabaseFileLocation, historicalFileName);
            VerifyOdbFile(historicalMinorsDatabaseFileLocation, historicalMinorFileName);

            odbVersion = GetDatabaseVersion(historicalDatabaseFileLocation, historicalMinorsDatabaseFileLocation);

            fileNames = new FileNames(odbVersion);

            this.configFileDestination = outputFolder + pathDelimeter + "DatabaseConfig.txt";
            this.historicalDatabaseFileLocation = inputFolder + pathDelimeter + fileNames.HistoricalDatabaseFileName;
            this.historicalMinorsDatabaseFileLocation = inputFolder + pathDelimeter + fileNames.HistoricalMinorDatabaseFileName;
            this.historicalLineupsFileLocation = inputFolder + pathDelimeter + fileNames.HistoricalLineupsDatabaseFileName;
            this.historicalTransactionsFileLocation = inputFolder + pathDelimeter + fileNames.HistoricalTransactionsDatabaseFileName;
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
                long currentDatabaseByte = 0;
                long historicalDatabaseFileSize = new FileInfo(historicalDatabaseFileLocation).Length;
                long historicalMinorDatabaseFileSize = new FileInfo(historicalMinorsDatabaseFileLocation).Length;
                long historicalLineupsDatabaseFileSize = new FileInfo(historicalLineupsFileLocation).Length;
                long historicalTransactionsDatabaseFileSize = new FileInfo(historicalTransactionsFileLocation).Length;
                long combinedDatabaseFileSizes = historicalDatabaseFileSize + historicalMinorDatabaseFileSize +
                    historicalLineupsDatabaseFileSize + historicalTransactionsDatabaseFileSize;

                WriteConfig(configFileDestination);

                HistoricalDatabaseConverter historicalDatabaseConverter = new HistoricalDatabaseConverter(historicalDatabaseFileLocation, outputFolder, fileNames.HistoricalDatabaseAllCsvFileNames);
                HistoricalDatabaseConverter historicalMinorDatabaseConverter = new HistoricalDatabaseConverter(historicalMinorsDatabaseFileLocation, outputFolder, fileNames.HistoricalMinorDatabaseAllCsvFileNames);
                HistoricalDatabaseConverter historicalLineupsDatabaseConverter = new HistoricalDatabaseConverter(historicalLineupsFileLocation, outputFolder, new String[] { fileNames.LineupsFileName });
                HistoricalDatabaseConverter historicalTransactionsDatabaseConverter = new HistoricalDatabaseConverter(historicalTransactionsFileLocation, outputFolder, new String[] { fileNames.TransactionsFileName });
                //StatsConverter statsConverter = new StatsConverter(statsFileLocation, outputFolder);

                historicalDatabaseConverter.ToCsv(progress, currentDatabaseByte, combinedDatabaseFileSizes);
                historicalMinorDatabaseConverter.ToCsv(progress, currentDatabaseByte += historicalDatabaseFileSize, combinedDatabaseFileSizes);
                historicalLineupsDatabaseConverter.ToCsv(progress, currentDatabaseByte += historicalMinorDatabaseFileSize, combinedDatabaseFileSizes);
                historicalTransactionsDatabaseConverter.ToCsv(progress, currentDatabaseByte += historicalLineupsDatabaseFileSize, combinedDatabaseFileSizes);

                //statsConverter.ToCsv();
            }
            else
            {
                throw new Exception(missingFileTextMessage + ".");
            }
        }

        public void Close()
        {
            
        }
        #endregion
    }
}
