// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace OOTPDatabaseConverter
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextField CsvFileDestinationTextField { get; set; }

		[Outlet]
		AppKit.NSTextField CsvFileLocationTextField { get; set; }

		[Outlet]
		AppKit.NSProgressIndicator CsvToOdbProgressBar { get; set; }

		[Outlet]
		AppKit.NSTextField OdbFileDestinationTextField { get; set; }

		[Outlet]
		AppKit.NSTextField OdbFileLocationTextField { get; set; }

		[Outlet]
		AppKit.NSProgressIndicator OdbToCsvProgressBar { get; set; }

		[Action ("CsvToOdbConvertButton:")]
		partial void CsvToOdbConvertButton (Foundation.NSObject sender);

		[Action ("OdbToCsvConvertButton:")]
		partial void OdbToCsvConvertButton (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CsvFileDestinationTextField != null) {
				CsvFileDestinationTextField.Dispose ();
				CsvFileDestinationTextField = null;
			}

			if (CsvFileLocationTextField != null) {
				CsvFileLocationTextField.Dispose ();
				CsvFileLocationTextField = null;
			}

			if (OdbFileDestinationTextField != null) {
				OdbFileDestinationTextField.Dispose ();
				OdbFileDestinationTextField = null;
			}

			if (OdbFileLocationTextField != null) {
				OdbFileLocationTextField.Dispose ();
				OdbFileLocationTextField = null;
			}

			if (OdbToCsvProgressBar != null) {
				OdbToCsvProgressBar.Dispose ();
				OdbToCsvProgressBar = null;
			}

			if (CsvToOdbProgressBar != null) {
				CsvToOdbProgressBar.Dispose ();
				CsvToOdbProgressBar = null;
			}
		}
	}
}
