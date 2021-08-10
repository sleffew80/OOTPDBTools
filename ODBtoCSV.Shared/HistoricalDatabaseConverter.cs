﻿#region File Description
//---------------------------------------------------------------------------
//
// File: HistoricalDatabaseConverter.cs
// Author: Steven Leffew
// Copyright: (C) 2021
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
#endregion

namespace ODBtoCSV
{
    /// <summary>
    /// Converts OOTP Database(*.odb) files, in OOTP's historical database format, to 
    /// comma separated value(*.csv) files in a Lahman Database style format.
    /// </summary>
    public class HistoricalDatabaseConverter
    {
        #region Members
        // Private Member Variables
        private String odbFileLocation;
        private String csvFileDestination;
        private int odbBytePosition;
        private int odbFileSize;
        private Byte odbTable;
        private String[] csvFileName;

        private static String pathDelimiter = Utilities.Utilities.FilePathDelimeter();
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
        public void ToCsv()
        {
            lock (this)
            {
                try
                {
                    //Create an object of Stream for output with 64MB buffer. Initialize to first table in the database.
                    StreamWriter outputStream = new StreamWriter(csvFileDestination + csvFileName[odbTable], false, Encoding.ASCII, 65536);
                    //Create an object of Stream for input.
                    FileStream inputStream = new FileStream(odbFileLocation, FileMode.Open,
                    FileAccess.Read, FileShare.ReadWrite);
                    //Create BinaryReader using Stream object to read input Stream.
                    using (BinaryReader reader = new BinaryReader(inputStream, Encoding.ASCII))
                    {
                        odbFileSize = (int)reader.BaseStream.Length;

                        Byte currentTable = 0;
                        int stringLength = 0;
                        String databaseLine = null;
                        String formattedDatabaseLine = null;

                        while (odbBytePosition < 5)
                        {
                            reader.ReadByte();
                            odbBytePosition++;
                        }
                        while (odbBytePosition < odbFileSize)
                        {
                            currentTable = reader.ReadByte();
                            if (odbTable != currentTable)
                            {
                                odbTable = currentTable;
                                outputStream.Close();
                                outputStream = new StreamWriter(csvFileDestination + csvFileName[odbTable], false, Encoding.ASCII, 65536);
                            }
                            stringLength = reader.ReadByte() + (reader.ReadByte() * 256);
                            odbBytePosition += 3;
                            for (int i = 0; i < stringLength; i++)
                            {
                                databaseLine = databaseLine + reader.ReadChar();
                                odbBytePosition++;
                            }
                            formattedDatabaseLine = databaseLine.Replace("\t", ",");
                            
                            outputStream.WriteLine(formattedDatabaseLine);
                            databaseLine = null;
                            formattedDatabaseLine = null;
                        }
                        inputStream.Close();
                        outputStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    Utilities.Utilities.MessageAlert(ex.Message, "Error!");
                }
            }
        }
        #endregion
    }
}
