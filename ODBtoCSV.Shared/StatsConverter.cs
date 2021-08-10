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
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ODBtoCSV
{
    public class StatsConverter
    {
#if WINDOWS
        private const string slash = "\\";
#else
        private const string slash = "/";
#endif
        // Private Member Variables
        private String odbName;
        private String odbFileLocation;
        private String outputFolderLocation;
        private int odbBytePosition;
        private int odbFileSize;
        private Byte odbTable;
        private String csvFileName;

        // Public Member Variables and Accessors
        public String OdbFileLocation
        {
            get { return odbFileLocation; }
            set { odbFileLocation = value; }
        }
        public String OutputFolderLocation
        {
            get { return outputFolderLocation; }
            set { outputFolderLocation = value; }
        }
        public int LastBytePosition
        {
            get { return odbBytePosition; }
        }
        public int FileSize
        {
            get { return odbFileSize; }
        }

        public StatsConverter(String odbFileLocation, String outputFolderLocation)
        {
            this.odbName = "Stats Database";
            this.odbFileLocation = odbFileLocation;
            this.outputFolderLocation = outputFolderLocation + slash;
            this.odbBytePosition = 0;
            this.odbFileSize = 0;
            this.odbTable = 0;
            this.csvFileName = "Stats.csv";
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
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
