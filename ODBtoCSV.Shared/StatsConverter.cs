#region File Description
//---------------------------------------------------------------------------
//
// File: StatsConverter.cs
// Author: Steven Leffew
// Copyright: (C) 2021
// Description: OOTP Database(*.odb) to Comma Separated Value(*.csv)
//              File Converter for OOTP's "stats.odb" file.
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

using OOTPCommon;

namespace ODBtoCSV
{
    public class StatsConverter
    {
        // Private Member Variables
        private String odbFileLocation;
        private String csvFileDestination;
        private int odbBytePosition;
        private int odbFileSize;
        private Byte odbTable;

        private static String csvFileName = "stats.csv";
        private static String pathDelimiter = Utilities.Utilities.FilePathDelimeter();

        // Public Member Variables and Accessors
        public String OdbFileLocation
        {
            get { return odbFileLocation; }
            set { odbFileLocation = value; }
        }
        public String CsvFileDestination
        {
            get { return csvFileDestination; }
            set { csvFileDestination = value; }
        }
        public int LastBytePosition
        {
            get { return odbBytePosition; }
        }
        public int FileSize
        {
            get { return odbFileSize; }
        }

        public StatsConverter(String odbFileLocation, String csvFileDestination)
        {
            this.odbFileLocation = odbFileLocation;
            this.csvFileDestination = csvFileDestination + pathDelimiter;
            this.odbBytePosition = 0;
            this.odbFileSize = 0;
            this.odbTable = 0;
        }

        public void ToCsv()
        {
            lock (this)
            {
                try
                {
                    //TODO
                }
                catch (Exception ex)
                {
                    //TODO;
                }
            }
        }
    }
}
