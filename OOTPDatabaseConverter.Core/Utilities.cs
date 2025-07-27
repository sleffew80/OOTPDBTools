#region Using Statements
using System;
using System.IO;
using System.Text;
#endregion

#region File Description
//---------------------------------------------------------------------------
//
// File: Utilities.cs
// Author: Steven Leffew
// Copyright: (C) 2021-2024
// Description: A collection of cross platform methods.  
// Note: All code requiring platform "#define" statements should go here.
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

namespace OOTPDatabaseConverter.Core
{
    /// <summary>
    /// Class containing simple platform agnostic methods.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Returns the appropriate file path delimiter for the current operating system.
        /// </summary>
        /// <returns>File path delimiter string.</returns>
        public static string FilePathDelimeter()
        {
            return Path.DirectorySeparatorChar.ToString();
        }

        /// <summary>
        /// Progress reporting structure for filename and percentage display
        /// </summary>
        public class ProgressInfo
        {
            public int Percentage { get; set; }
            public string CurrentFile { get; set; }
        }
    }
}
