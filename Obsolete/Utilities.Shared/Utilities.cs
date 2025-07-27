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

namespace Utilities
{
    /// <summary>
    /// Class containing simple platform agnostic methods.
    /// </summary>
    [Obsolete("This class has been deprecated. Use OOTPDatabaseConverter.Core.Utilities instead.")]
    public static class Utilities
    {
        [Obsolete("This method has been deprecated. Use OOTPDatabaseConverter.Core.Utilities.FilePathDelimeter() instead.")]
        public static string FilePathDelimeter()
        {
#if _WINDOWS_
            return "\\";
#else
            return "/";
#endif
        }
    }
}
