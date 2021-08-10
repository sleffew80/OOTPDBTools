﻿#region File Description
//---------------------------------------------------------------------------
//
// File: MainForm.Designer.cs
// Author: Steven Leffew
// Copyright: (C) 2021
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

namespace OOTP21DatabaseConverter
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
            this.odbToCsvGroupBox = new System.Windows.Forms.GroupBox();
            this.csvToOdbGroupBox = new System.Windows.Forms.GroupBox();
            this.odbFileLocationLabel = new System.Windows.Forms.Label();
            this.csvFileDestinationLabel = new System.Windows.Forms.Label();
            this.csvFileLocationLabel = new System.Windows.Forms.Label();
            this.odbFileDestinationLabel = new System.Windows.Forms.Label();
            this.odbToCsvStatusLabel = new System.Windows.Forms.Label();
            this.csvToOdbStatusLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.odbFileLocationTextBox = new System.Windows.Forms.TextBox();
            this.csvFileDestinationTextBox = new System.Windows.Forms.TextBox();
            this.csvFileLocationTextBox = new System.Windows.Forms.TextBox();
            this.odbFileDestinationTextBox = new System.Windows.Forms.TextBox();
            this.odbFileLocationButton = new System.Windows.Forms.Button();
            this.csvFileDestinationButton = new System.Windows.Forms.Button();
            this.csvFileLocationButton = new System.Windows.Forms.Button();
            this.odbFileDestinationButton = new System.Windows.Forms.Button();
            this.odbToCsvConvertButton = new System.Windows.Forms.Button();
            this.csvToOdbConvertButton = new System.Windows.Forms.Button();
            this.odbToCsvProgressBar = new System.Windows.Forms.ProgressBar();
            this.csvToOdbProgressBar = new System.Windows.Forms.ProgressBar();
            this.odbToCsvGroupBox.SuspendLayout();
            this.csvToOdbGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // odbToCsvGroupBox
            // 
            this.odbToCsvGroupBox.Controls.Add(this.odbFileLocationLabel);
            this.odbToCsvGroupBox.Controls.Add(this.odbFileLocationTextBox);
            this.odbToCsvGroupBox.Controls.Add(this.odbFileLocationButton);
            this.odbToCsvGroupBox.Controls.Add(this.csvFileDestinationLabel);
            this.odbToCsvGroupBox.Controls.Add(this.csvFileDestinationTextBox);
            this.odbToCsvGroupBox.Controls.Add(this.csvFileDestinationButton);
            this.odbToCsvGroupBox.Controls.Add(this.odbToCsvStatusLabel);
            this.odbToCsvGroupBox.Controls.Add(this.odbToCsvProgressBar);
            this.odbToCsvGroupBox.Controls.Add(this.odbToCsvConvertButton);
            this.odbToCsvGroupBox.Location = new System.Drawing.Point(12, 12);
            this.odbToCsvGroupBox.Name = "odbToCsvGroupBox";
            this.odbToCsvGroupBox.Size = new System.Drawing.Size(463, 169);
            this.odbToCsvGroupBox.TabIndex = 0;
            this.odbToCsvGroupBox.TabStop = false;
            this.odbToCsvGroupBox.Text = "Convert ODB to CSV";
            // 
            // csvToOdbGroupBox
            // 
            this.csvToOdbGroupBox.Controls.Add(this.csvFileLocationLabel);
            this.csvToOdbGroupBox.Controls.Add(this.csvFileLocationTextBox);
            this.csvToOdbGroupBox.Controls.Add(this.csvFileLocationButton);
            this.csvToOdbGroupBox.Controls.Add(this.odbFileDestinationLabel);
            this.csvToOdbGroupBox.Controls.Add(this.odbFileDestinationTextBox);
            this.csvToOdbGroupBox.Controls.Add(this.odbFileDestinationButton);
            this.csvToOdbGroupBox.Controls.Add(this.csvToOdbStatusLabel);
            this.csvToOdbGroupBox.Controls.Add(this.csvToOdbProgressBar);
            this.csvToOdbGroupBox.Controls.Add(this.csvToOdbConvertButton);
            this.csvToOdbGroupBox.Location = new System.Drawing.Point(12, 187);
            this.csvToOdbGroupBox.Name = "csvToOdbGroupBox";
            this.csvToOdbGroupBox.Size = new System.Drawing.Size(463, 169);
            this.csvToOdbGroupBox.TabIndex = 1;
            this.csvToOdbGroupBox.TabStop = false;
            this.csvToOdbGroupBox.Text = "Convert CSV to ODB";
            // 
            // odbFileLocationLabel
            // 
            this.odbFileLocationLabel.AutoSize = true;
            this.odbFileLocationLabel.Location = new System.Drawing.Point(7, 23);
            this.odbFileLocationLabel.Name = "odbFileLocationLabel";
            this.odbFileLocationLabel.Size = new System.Drawing.Size(106, 15);
            this.odbFileLocationLabel.TabIndex = 0;
            this.odbFileLocationLabel.Text = "ODB Files Location";
            // 
            // csvFileDestinationLabel
            // 
            this.csvFileDestinationLabel.AutoSize = true;
            this.csvFileDestinationLabel.Location = new System.Drawing.Point(7, 67);
            this.csvFileDestinationLabel.Name = "csvFileDestinationLabel";
            this.csvFileDestinationLabel.Size = new System.Drawing.Size(117, 15);
            this.csvFileDestinationLabel.TabIndex = 2;
            this.csvFileDestinationLabel.Text = "CSV Files Destination";
            // 
            // csvFileLocationLabel
            // 
            this.csvFileLocationLabel.AutoSize = true;
            this.csvFileLocationLabel.Location = new System.Drawing.Point(7, 23);
            this.csvFileLocationLabel.Name = "csvFileLocationLabel";
            this.csvFileLocationLabel.Size = new System.Drawing.Size(103, 15);
            this.csvFileLocationLabel.TabIndex = 0;
            this.csvFileLocationLabel.Text = "CSV Files Location";
            // 
            // odbFileDestinationLabel
            // 
            this.odbFileDestinationLabel.AutoSize = true;
            this.odbFileDestinationLabel.Location = new System.Drawing.Point(7, 67);
            this.odbFileDestinationLabel.Name = "odbFileDestinationLabel";
            this.odbFileDestinationLabel.Size = new System.Drawing.Size(120, 15);
            this.odbFileDestinationLabel.TabIndex = 2;
            this.odbFileDestinationLabel.Text = "ODB Files Destination";
            // 
            // odbToCsvStatusLabel
            // 
            this.odbToCsvStatusLabel.AutoSize = true;
            this.odbToCsvStatusLabel.Location = new System.Drawing.Point(7, 114);
            this.odbToCsvStatusLabel.Name = "odbToCsvStatusLabel";
            this.odbToCsvStatusLabel.Size = new System.Drawing.Size(38, 15);
            this.odbToCsvStatusLabel.TabIndex = 8;
            this.odbToCsvStatusLabel.Text = "label1";
            this.odbToCsvStatusLabel.Visible = false;
            // 
            // csvToOdbStatusLabel
            // 
            this.csvToOdbStatusLabel.AutoSize = true;
            this.csvToOdbStatusLabel.Location = new System.Drawing.Point(7, 116);
            this.csvToOdbStatusLabel.Name = "csvToOdbStatusLabel";
            this.csvToOdbStatusLabel.Size = new System.Drawing.Size(38, 15);
            this.csvToOdbStatusLabel.TabIndex = 8;
            this.csvToOdbStatusLabel.Text = "label1";
            this.csvToOdbStatusLabel.Visible = false;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(381, 363);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(84, 15);
            this.versionLabel.TabIndex = 2;
            this.versionLabel.Text = "Version: 1.0.0.1";
            // 
            // odbFileLocationTextBox
            // 
            this.odbFileLocationTextBox.Location = new System.Drawing.Point(7, 41);
            this.odbFileLocationTextBox.Name = "odbFileLocationTextBox";
            this.odbFileLocationTextBox.Size = new System.Drawing.Size(365, 23);
            this.odbFileLocationTextBox.TabIndex = 1;
            this.odbFileLocationTextBox.TextChanged += new System.EventHandler(this.odbFileLocationTextBox_TextChanged);
            // 
            // csvFileDestinationTextBox
            // 
            this.csvFileDestinationTextBox.Location = new System.Drawing.Point(7, 86);
            this.csvFileDestinationTextBox.Name = "csvFileDestinationTextBox";
            this.csvFileDestinationTextBox.Size = new System.Drawing.Size(365, 23);
            this.csvFileDestinationTextBox.TabIndex = 3;
            this.csvFileDestinationTextBox.TextChanged += new System.EventHandler(this.csvFileDestinationTextBox_TextChanged);
            // 
            // csvFileLocationTextBox
            // 
            this.csvFileLocationTextBox.Location = new System.Drawing.Point(7, 41);
            this.csvFileLocationTextBox.Name = "csvFileLocationTextBox";
            this.csvFileLocationTextBox.Size = new System.Drawing.Size(365, 23);
            this.csvFileLocationTextBox.TabIndex = 1;
            this.csvFileLocationTextBox.TextChanged += new System.EventHandler(this.csvFileLocationTextBox_TextChanged);
            // 
            // odbFileDestinationTextBox
            // 
            this.odbFileDestinationTextBox.Location = new System.Drawing.Point(7, 86);
            this.odbFileDestinationTextBox.Name = "odbFileDestinationTextBox";
            this.odbFileDestinationTextBox.Size = new System.Drawing.Size(365, 23);
            this.odbFileDestinationTextBox.TabIndex = 3;
            this.odbFileDestinationTextBox.TextChanged += new System.EventHandler(this.odbFileDestinationTextBox_TextChanged);
            // 
            // odbFileLocationButton
            // 
            this.odbFileLocationButton.Location = new System.Drawing.Point(378, 40);
            this.odbFileLocationButton.Name = "odbFileLocationButton";
            this.odbFileLocationButton.Size = new System.Drawing.Size(75, 23);
            this.odbFileLocationButton.TabIndex = 4;
            this.odbFileLocationButton.Text = "Browse";
            this.odbFileLocationButton.UseVisualStyleBackColor = true;
            this.odbFileLocationButton.Click += new System.EventHandler(this.odbFileLocationButton_Click);
            // 
            // csvFileDestinationButton
            // 
            this.csvFileDestinationButton.Location = new System.Drawing.Point(378, 85);
            this.csvFileDestinationButton.Name = "csvFileDestinationButton";
            this.csvFileDestinationButton.Size = new System.Drawing.Size(75, 23);
            this.csvFileDestinationButton.TabIndex = 5;
            this.csvFileDestinationButton.Text = "Browse";
            this.csvFileDestinationButton.UseVisualStyleBackColor = true;
            this.csvFileDestinationButton.Click += new System.EventHandler(this.csvFileDestinationButton_Click);
            // 
            // csvFileLocationButton
            // 
            this.csvFileLocationButton.Location = new System.Drawing.Point(378, 41);
            this.csvFileLocationButton.Name = "csvFileLocationButton";
            this.csvFileLocationButton.Size = new System.Drawing.Size(75, 23);
            this.csvFileLocationButton.TabIndex = 4;
            this.csvFileLocationButton.Text = "Browse";
            this.csvFileLocationButton.UseVisualStyleBackColor = true;
            this.csvFileLocationButton.Click += new System.EventHandler(this.csvFileLocationButton_Click);
            // 
            // odbFileDestinationButton
            // 
            this.odbFileDestinationButton.Location = new System.Drawing.Point(378, 85);
            this.odbFileDestinationButton.Name = "odbFileDestinationButton";
            this.odbFileDestinationButton.Size = new System.Drawing.Size(75, 23);
            this.odbFileDestinationButton.TabIndex = 5;
            this.odbFileDestinationButton.Text = "Browse";
            this.odbFileDestinationButton.UseVisualStyleBackColor = true;
            this.odbFileDestinationButton.Click += new System.EventHandler(this.odbFileDestinationButton_Click);
            // 
            // odbToCsvConvertButton
            // 
            this.odbToCsvConvertButton.Location = new System.Drawing.Point(378, 135);
            this.odbToCsvConvertButton.Name = "odbToCsvConvertButton";
            this.odbToCsvConvertButton.Size = new System.Drawing.Size(75, 23);
            this.odbToCsvConvertButton.TabIndex = 6;
            this.odbToCsvConvertButton.Text = "Convert";
            this.odbToCsvConvertButton.UseVisualStyleBackColor = true;
            this.odbToCsvConvertButton.Click += new System.EventHandler(this.odbToCsvConvertButton_Click);
            // 
            // csvToOdbConvertButton
            // 
            this.csvToOdbConvertButton.Location = new System.Drawing.Point(378, 135);
            this.csvToOdbConvertButton.Name = "csvToOdbConvertButton";
            this.csvToOdbConvertButton.Size = new System.Drawing.Size(75, 23);
            this.csvToOdbConvertButton.TabIndex = 6;
            this.csvToOdbConvertButton.Text = "Convert";
            this.csvToOdbConvertButton.UseVisualStyleBackColor = true;
            this.csvToOdbConvertButton.Click += new System.EventHandler(this.csvToOdbConvertButton_Click);
            // 
            // odbToCsvProgressBar
            // 
            this.odbToCsvProgressBar.Location = new System.Drawing.Point(7, 135);
            this.odbToCsvProgressBar.Name = "odbToCsvProgressBar";
            this.odbToCsvProgressBar.Size = new System.Drawing.Size(365, 23);
            this.odbToCsvProgressBar.TabIndex = 7;
            this.odbToCsvProgressBar.Visible = false;
            // 
            // csvToOdbProgressBar
            // 
            this.csvToOdbProgressBar.Location = new System.Drawing.Point(7, 135);
            this.csvToOdbProgressBar.Name = "csvToOdbProgressBar";
            this.csvToOdbProgressBar.Size = new System.Drawing.Size(365, 23);
            this.csvToOdbProgressBar.TabIndex = 7;
            this.csvToOdbProgressBar.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 387);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.csvToOdbGroupBox);
            this.Controls.Add(this.odbToCsvGroupBox);
            this.Name = "MainForm";
            this.Text = "OOTP 21 Database Converter";
            this.odbToCsvGroupBox.ResumeLayout(false);
            this.odbToCsvGroupBox.PerformLayout();
            this.csvToOdbGroupBox.ResumeLayout(false);
            this.csvToOdbGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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

