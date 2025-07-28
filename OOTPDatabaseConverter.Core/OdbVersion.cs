#region Using Statements
using System;
using System.IO;
using System.Text;
#endregion

#region File Description
//---------------------------------------------------------------------------
//
// File: OdbVersion.cs
// Author: Steven Leffew
// Copyright: (C) 2021-2024
// Description: OOTP Database version info.
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
    public enum OdbVersion
    {
        ODB_Err,
        ODB_17,
        ODB_19,
        ODB_22,
        ODB_25,
        ODB_26,
        ODB_Unk
    }
}

