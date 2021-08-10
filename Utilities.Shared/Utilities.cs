#region File Description
//---------------------------------------------------------------------------
//
// File: Utilities.cs
// Author: Steven Leffew
// Copyright: (C) 2021
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

using System;
using System.IO;
#if _WINDOWS_
using System.Windows.Forms;
#elif __MACOS__
using AppKit;
#endif

namespace Utilities
{
    /// <summary>
    /// Class containing simple platform agnostic methods.
    /// </summary>
    public static class Utilities
    {
        public static string FilePathDelimeter()
        {
#if _WINDOWS_
            return "\\";
#else
            return "/";
#endif
        }

        public static void MessageAlert(string messageInfoText, string messageCaptionText)
        {
#if _WINDOWS_
            MessageBox.Show(messageInfoText, messageCaptionText);
#elif __MACOS__
            var alert = new NSAlert()
            {
                AlertStyle = NSAlertStyle.Warning,
                InformativeText = messageInfoText,
                MessageText = messageCaptionText,
            };
            alert.RunModal();
#endif
        }
    }
}
