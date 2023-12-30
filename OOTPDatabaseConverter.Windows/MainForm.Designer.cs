#region File Description
//---------------------------------------------------------------------------
//
// File: MainForm.Designer.cs
// Author: Steven Leffew
// Copyright: (C) 2021-2024
// Description: Windows Form Designer Generated Code.
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

namespace OOTPDatabaseConverter
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            odbToCsvGroupBox = new System.Windows.Forms.GroupBox();
            odbFileLocationLabel = new System.Windows.Forms.Label();
            odbFileLocationTextBox = new System.Windows.Forms.TextBox();
            odbFileLocationButton = new System.Windows.Forms.Button();
            csvFileDestinationLabel = new System.Windows.Forms.Label();
            csvFileDestinationTextBox = new System.Windows.Forms.TextBox();
            csvFileDestinationButton = new System.Windows.Forms.Button();
            odbToCsvStatusLabel = new System.Windows.Forms.Label();
            odbToCsvProgressBar = new System.Windows.Forms.ProgressBar();
            odbToCsvConvertButton = new System.Windows.Forms.Button();
            csvToOdbGroupBox = new System.Windows.Forms.GroupBox();
            csvFileLocationLabel = new System.Windows.Forms.Label();
            csvFileLocationTextBox = new System.Windows.Forms.TextBox();
            csvFileLocationButton = new System.Windows.Forms.Button();
            odbFileDestinationLabel = new System.Windows.Forms.Label();
            odbFileDestinationTextBox = new System.Windows.Forms.TextBox();
            odbFileDestinationButton = new System.Windows.Forms.Button();
            csvToOdbStatusLabel = new System.Windows.Forms.Label();
            csvToOdbProgressBar = new System.Windows.Forms.ProgressBar();
            csvToOdbConvertButton = new System.Windows.Forms.Button();
            versionLabel = new System.Windows.Forms.Label();
            odbToCsvGroupBox.SuspendLayout();
            csvToOdbGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // odbToCsvGroupBox
            // 
            odbToCsvGroupBox.Controls.Add(odbFileLocationLabel);
            odbToCsvGroupBox.Controls.Add(odbFileLocationTextBox);
            odbToCsvGroupBox.Controls.Add(odbFileLocationButton);
            odbToCsvGroupBox.Controls.Add(csvFileDestinationLabel);
            odbToCsvGroupBox.Controls.Add(csvFileDestinationTextBox);
            odbToCsvGroupBox.Controls.Add(csvFileDestinationButton);
            odbToCsvGroupBox.Controls.Add(odbToCsvStatusLabel);
            odbToCsvGroupBox.Controls.Add(odbToCsvProgressBar);
            odbToCsvGroupBox.Controls.Add(odbToCsvConvertButton);
            odbToCsvGroupBox.Location = new System.Drawing.Point(12, 12);
            odbToCsvGroupBox.Name = "odbToCsvGroupBox";
            odbToCsvGroupBox.Size = new System.Drawing.Size(463, 169);
            odbToCsvGroupBox.TabIndex = 0;
            odbToCsvGroupBox.TabStop = false;
            odbToCsvGroupBox.Text = "Convert ODB to CSV";
            // 
            // odbFileLocationLabel
            // 
            odbFileLocationLabel.AutoSize = true;
            odbFileLocationLabel.Location = new System.Drawing.Point(7, 23);
            odbFileLocationLabel.Name = "odbFileLocationLabel";
            odbFileLocationLabel.Size = new System.Drawing.Size(106, 15);
            odbFileLocationLabel.TabIndex = 0;
            odbFileLocationLabel.Text = "ODB Files Location";
            // 
            // odbFileLocationTextBox
            // 
            odbFileLocationTextBox.Location = new System.Drawing.Point(7, 41);
            odbFileLocationTextBox.Name = "odbFileLocationTextBox";
            odbFileLocationTextBox.Size = new System.Drawing.Size(365, 23);
            odbFileLocationTextBox.TabIndex = 1;
            odbFileLocationTextBox.TextChanged += odbFileLocationTextBox_TextChanged;
            // 
            // odbFileLocationButton
            // 
            odbFileLocationButton.Location = new System.Drawing.Point(378, 40);
            odbFileLocationButton.Name = "odbFileLocationButton";
            odbFileLocationButton.Size = new System.Drawing.Size(75, 23);
            odbFileLocationButton.TabIndex = 4;
            odbFileLocationButton.Text = "Browse";
            odbFileLocationButton.UseVisualStyleBackColor = true;
            odbFileLocationButton.Click += odbFileLocationButton_Click;
            // 
            // csvFileDestinationLabel
            // 
            csvFileDestinationLabel.AutoSize = true;
            csvFileDestinationLabel.Location = new System.Drawing.Point(7, 67);
            csvFileDestinationLabel.Name = "csvFileDestinationLabel";
            csvFileDestinationLabel.Size = new System.Drawing.Size(117, 15);
            csvFileDestinationLabel.TabIndex = 2;
            csvFileDestinationLabel.Text = "CSV Files Destination";
            // 
            // csvFileDestinationTextBox
            // 
            csvFileDestinationTextBox.Location = new System.Drawing.Point(7, 86);
            csvFileDestinationTextBox.Name = "csvFileDestinationTextBox";
            csvFileDestinationTextBox.Size = new System.Drawing.Size(365, 23);
            csvFileDestinationTextBox.TabIndex = 3;
            csvFileDestinationTextBox.TextChanged += csvFileDestinationTextBox_TextChanged;
            // 
            // csvFileDestinationButton
            // 
            csvFileDestinationButton.Location = new System.Drawing.Point(378, 85);
            csvFileDestinationButton.Name = "csvFileDestinationButton";
            csvFileDestinationButton.Size = new System.Drawing.Size(75, 23);
            csvFileDestinationButton.TabIndex = 5;
            csvFileDestinationButton.Text = "Browse";
            csvFileDestinationButton.UseVisualStyleBackColor = true;
            csvFileDestinationButton.Click += csvFileDestinationButton_Click;
            // 
            // odbToCsvStatusLabel
            // 
            odbToCsvStatusLabel.AutoSize = true;
            odbToCsvStatusLabel.Location = new System.Drawing.Point(7, 114);
            odbToCsvStatusLabel.Name = "odbToCsvStatusLabel";
            odbToCsvStatusLabel.Size = new System.Drawing.Size(38, 15);
            odbToCsvStatusLabel.TabIndex = 8;
            odbToCsvStatusLabel.Text = "label1";
            odbToCsvStatusLabel.Visible = false;
            // 
            // odbToCsvProgressBar
            // 
            odbToCsvProgressBar.Location = new System.Drawing.Point(7, 135);
            odbToCsvProgressBar.Name = "odbToCsvProgressBar";
            odbToCsvProgressBar.Size = new System.Drawing.Size(365, 23);
            odbToCsvProgressBar.TabIndex = 7;
            odbToCsvProgressBar.Visible = false;
            // 
            // odbToCsvConvertButton
            // 
            odbToCsvConvertButton.Location = new System.Drawing.Point(378, 135);
            odbToCsvConvertButton.Name = "odbToCsvConvertButton";
            odbToCsvConvertButton.Size = new System.Drawing.Size(75, 23);
            odbToCsvConvertButton.TabIndex = 6;
            odbToCsvConvertButton.Text = "Convert";
            odbToCsvConvertButton.UseVisualStyleBackColor = true;
            odbToCsvConvertButton.Click += odbToCsvConvertButton_Click;
            // 
            // csvToOdbGroupBox
            // 
            csvToOdbGroupBox.Controls.Add(csvFileLocationLabel);
            csvToOdbGroupBox.Controls.Add(csvFileLocationTextBox);
            csvToOdbGroupBox.Controls.Add(csvFileLocationButton);
            csvToOdbGroupBox.Controls.Add(odbFileDestinationLabel);
            csvToOdbGroupBox.Controls.Add(odbFileDestinationTextBox);
            csvToOdbGroupBox.Controls.Add(odbFileDestinationButton);
            csvToOdbGroupBox.Controls.Add(csvToOdbStatusLabel);
            csvToOdbGroupBox.Controls.Add(csvToOdbProgressBar);
            csvToOdbGroupBox.Controls.Add(csvToOdbConvertButton);
            csvToOdbGroupBox.Location = new System.Drawing.Point(12, 187);
            csvToOdbGroupBox.Name = "csvToOdbGroupBox";
            csvToOdbGroupBox.Size = new System.Drawing.Size(463, 169);
            csvToOdbGroupBox.TabIndex = 1;
            csvToOdbGroupBox.TabStop = false;
            csvToOdbGroupBox.Text = "Convert CSV to ODB";
            // 
            // csvFileLocationLabel
            // 
            csvFileLocationLabel.AutoSize = true;
            csvFileLocationLabel.Location = new System.Drawing.Point(7, 23);
            csvFileLocationLabel.Name = "csvFileLocationLabel";
            csvFileLocationLabel.Size = new System.Drawing.Size(103, 15);
            csvFileLocationLabel.TabIndex = 0;
            csvFileLocationLabel.Text = "CSV Files Location";
            // 
            // csvFileLocationTextBox
            // 
            csvFileLocationTextBox.Location = new System.Drawing.Point(7, 41);
            csvFileLocationTextBox.Name = "csvFileLocationTextBox";
            csvFileLocationTextBox.Size = new System.Drawing.Size(365, 23);
            csvFileLocationTextBox.TabIndex = 1;
            csvFileLocationTextBox.TextChanged += csvFileLocationTextBox_TextChanged;
            // 
            // csvFileLocationButton
            // 
            csvFileLocationButton.Location = new System.Drawing.Point(378, 41);
            csvFileLocationButton.Name = "csvFileLocationButton";
            csvFileLocationButton.Size = new System.Drawing.Size(75, 23);
            csvFileLocationButton.TabIndex = 4;
            csvFileLocationButton.Text = "Browse";
            csvFileLocationButton.UseVisualStyleBackColor = true;
            csvFileLocationButton.Click += csvFileLocationButton_Click;
            // 
            // odbFileDestinationLabel
            // 
            odbFileDestinationLabel.AutoSize = true;
            odbFileDestinationLabel.Location = new System.Drawing.Point(7, 67);
            odbFileDestinationLabel.Name = "odbFileDestinationLabel";
            odbFileDestinationLabel.Size = new System.Drawing.Size(120, 15);
            odbFileDestinationLabel.TabIndex = 2;
            odbFileDestinationLabel.Text = "ODB Files Destination";
            // 
            // odbFileDestinationTextBox
            // 
            odbFileDestinationTextBox.Location = new System.Drawing.Point(7, 86);
            odbFileDestinationTextBox.Name = "odbFileDestinationTextBox";
            odbFileDestinationTextBox.Size = new System.Drawing.Size(365, 23);
            odbFileDestinationTextBox.TabIndex = 3;
            odbFileDestinationTextBox.TextChanged += odbFileDestinationTextBox_TextChanged;
            // 
            // odbFileDestinationButton
            // 
            odbFileDestinationButton.Location = new System.Drawing.Point(378, 85);
            odbFileDestinationButton.Name = "odbFileDestinationButton";
            odbFileDestinationButton.Size = new System.Drawing.Size(75, 23);
            odbFileDestinationButton.TabIndex = 5;
            odbFileDestinationButton.Text = "Browse";
            odbFileDestinationButton.UseVisualStyleBackColor = true;
            odbFileDestinationButton.Click += odbFileDestinationButton_Click;
            // 
            // csvToOdbStatusLabel
            // 
            csvToOdbStatusLabel.AutoSize = true;
            csvToOdbStatusLabel.Location = new System.Drawing.Point(7, 116);
            csvToOdbStatusLabel.Name = "csvToOdbStatusLabel";
            csvToOdbStatusLabel.Size = new System.Drawing.Size(38, 15);
            csvToOdbStatusLabel.TabIndex = 8;
            csvToOdbStatusLabel.Text = "label1";
            csvToOdbStatusLabel.Visible = false;
            // 
            // csvToOdbProgressBar
            // 
            csvToOdbProgressBar.Location = new System.Drawing.Point(7, 135);
            csvToOdbProgressBar.Name = "csvToOdbProgressBar";
            csvToOdbProgressBar.Size = new System.Drawing.Size(365, 23);
            csvToOdbProgressBar.TabIndex = 7;
            csvToOdbProgressBar.Visible = false;
            // 
            // csvToOdbConvertButton
            // 
            csvToOdbConvertButton.Location = new System.Drawing.Point(378, 135);
            csvToOdbConvertButton.Name = "csvToOdbConvertButton";
            csvToOdbConvertButton.Size = new System.Drawing.Size(75, 23);
            csvToOdbConvertButton.TabIndex = 6;
            csvToOdbConvertButton.Text = "Convert";
            csvToOdbConvertButton.UseVisualStyleBackColor = true;
            csvToOdbConvertButton.Click += csvToOdbConvertButton_Click;
            // 
            // versionLabel
            // 
            versionLabel.AutoSize = true;
            versionLabel.Location = new System.Drawing.Point(381, 363);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new System.Drawing.Size(84, 15);
            versionLabel.TabIndex = 2;
            versionLabel.Text = "Version: 4.0.1.2";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(489, 387);
            Controls.Add(versionLabel);
            Controls.Add(csvToOdbGroupBox);
            Controls.Add(odbToCsvGroupBox);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "OOTP Database Converter";
            odbToCsvGroupBox.ResumeLayout(false);
            odbToCsvGroupBox.PerformLayout();
            csvToOdbGroupBox.ResumeLayout(false);
            csvToOdbGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox odbToCsvGroupBox;
        private System.Windows.Forms.GroupBox csvToOdbGroupBox;
        private System.Windows.Forms.Label odbFileLocationLabel;
        private System.Windows.Forms.Label csvFileDestinationLabel;
        private System.Windows.Forms.Label csvFileLocationLabel;
        private System.Windows.Forms.Label odbFileDestinationLabel;
        private System.Windows.Forms.Label odbToCsvStatusLabel;
        private System.Windows.Forms.Label csvToOdbStatusLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.TextBox odbFileLocationTextBox;
        private System.Windows.Forms.TextBox csvFileDestinationTextBox;
        private System.Windows.Forms.TextBox csvFileLocationTextBox;
        private System.Windows.Forms.TextBox odbFileDestinationTextBox;
        private System.Windows.Forms.Button odbFileLocationButton;
        private System.Windows.Forms.Button csvFileDestinationButton;
        private System.Windows.Forms.Button csvFileLocationButton;
        private System.Windows.Forms.Button odbFileDestinationButton;
        private System.Windows.Forms.Button odbToCsvConvertButton;
        private System.Windows.Forms.Button csvToOdbConvertButton;
        private System.Windows.Forms.ProgressBar odbToCsvProgressBar;
        private System.Windows.Forms.ProgressBar csvToOdbProgressBar;

    }
}

