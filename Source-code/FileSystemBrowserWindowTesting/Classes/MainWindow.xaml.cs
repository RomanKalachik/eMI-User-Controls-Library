// <author>Stefán Örvar Sigmundsson</author>
// <copyright company="eMedia Intellect" file="MainWindow.xaml.cs">
//    Copyright © 2016 eMedia Intellect
// </copyright>
// <licence>
//    This file is part of eMI User Controls Library.
//
//    eMI User Controls Library is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    eMI User Controls Library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with eMI User Controls Library. If not, see http://www.gnu.org/licenses/.
// </licence>

namespace Emi.UserControls
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Windows;

    /// <summary>Represents the main window.</summary>
    public partial class MainWindow : Window
    {
        /// <summary>Initialises a new instance of the <see cref="MainWindow"/> class.</summary>
        public MainWindow()
        {
            this.InitializeComponent();

            foreach (string browsingMode in Enum.GetNames(typeof(BrowsingMode)))
            {
                this.browsingModeComboBox.Items.Add(browsingMode);

                this.browsingModeComboBox.SelectedIndex = 0;
            }

            foreach (string byteMultiple in Enum.GetNames(typeof(ByteMultiple)))
            {
                this.byteMultipleComboBox.Items.Add(byteMultiple);

                this.byteMultipleComboBox.SelectedIndex = 0;
            }

            foreach (string filteringMode in Enum.GetNames(typeof(FilteringMode)))
            {
                this.filteringModeComboBox.Items.Add(filteringMode);

                this.filteringModeComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>Resets all controls to their default state when the button is clicked.</summary>
        /// <param name="sender">The sender object of the event handler.</param>
        /// <param name="e">The state information of the event handler.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "No localisation required.", MessageId = "System.Windows.Controls.TextBox.set_Text(System.String)")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", Justification = "The spelling is correct.", MessageId = "lnk")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", Justification = "The spelling is correct.", MessageId = "ini")]
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.pathTextBox.Text = string.Empty;
            this.browsingModeComboBox.SelectedIndex = 0;
            this.byteMultipleComboBox.SelectedIndex = 0;
            this.minimumFilesTextBox.Text = "1";
            this.maximumFilesTextBox.Text = "1";
            this.minimumDirectoriesTextBox.Text = "0";
            this.maximumDirectoriesTextBox.Text = "0";
            this.hasAvailableFreeSpace.IsChecked = true;
            this.hasDriveFormat.IsChecked = true;
            this.hasDriveType.IsChecked = true;
            this.hasRootDirectory.IsChecked = false;
            this.hasTotalFreeSpace.IsChecked = true;
            this.hasTotalSize.IsChecked = true;
            this.hasVolumeLabel.IsChecked = false;

            this.filteringModeComboBox.SelectedIndex = 0;
            this.extensionsTextBox.Text = ";.ini;.lnk;.sys";

            this.resultListView.Items.Clear();
        }

        /// <summary>Shows a modal <see cref="FileSystemBrowserWindow"/> when the button is clicked.</summary>
        /// <param name="sender">The sender object of the event handler.</param>
        /// <param name="e">The state information of the event handler.</param>
        private void ShowDialogButton_Click(object sender, RoutedEventArgs e)
        {
            FilteringMode filteringMode = (FilteringMode)Enum.Parse(typeof(FilteringMode), this.filteringModeComboBox.SelectedValue.ToString());

            BrowserFilter browserFilter = new BrowserFilter(filteringMode);

            foreach (string extension in this.extensionsTextBox.Text.Split(';'))
            {
                browserFilter.AddExtension(extension);
            }

            BrowserSettings browserSettings = new BrowserSettings();

            CultureInfo currentCultureInfo = Thread.CurrentThread.CurrentCulture;

            browserSettings.BrowsingMode = (BrowsingMode)Enum.Parse(typeof(BrowsingMode), this.browsingModeComboBox.SelectedValue.ToString());
            browserSettings.ByteMultiple = (ByteMultiple)Enum.Parse(typeof(ByteMultiple), this.byteMultipleComboBox.SelectedValue.ToString());
            browserSettings.HasAvailableFreeSpace = (bool)this.hasAvailableFreeSpace.IsChecked;
            browserSettings.HasDriveFormat = (bool)this.hasDriveFormat.IsChecked;
            browserSettings.HasDriveType = (bool)this.hasDriveType.IsChecked;
            browserSettings.HasRootDirectory = (bool)this.hasRootDirectory.IsChecked;
            browserSettings.HasTotalFreeSpace = (bool)this.hasTotalFreeSpace.IsChecked;
            browserSettings.HasTotalSize = (bool)this.hasTotalSize.IsChecked;
            browserSettings.HasVolumeLabel = (bool)this.hasVolumeLabel.IsChecked;
            browserSettings.MaximumFiles = int.Parse(this.maximumFilesTextBox.Text, currentCultureInfo);
            browserSettings.MaximumDirectories = int.Parse(this.maximumDirectoriesTextBox.Text, currentCultureInfo);
            browserSettings.MinimumFiles = int.Parse(this.minimumFilesTextBox.Text, currentCultureInfo);
            browserSettings.MinimumDirectories = int.Parse(this.minimumDirectoriesTextBox.Text, currentCultureInfo);
            browserSettings.Path = this.pathTextBox.Text;

            FileSystemBrowserWindow fileSystemBrowserWindow = new FileSystemBrowserWindow(browserFilter, browserSettings);

            fileSystemBrowserWindow.Owner = this;

            if (fileSystemBrowserWindow.ShowDialog() ?? true)
            {
                foreach (string directory in fileSystemBrowserWindow.SelectedDirectories)
                {
                    this.resultListView.Items.Add(new { Type = "Directory", Path = directory });
                }

                foreach (string file in fileSystemBrowserWindow.SelectedFiles)
                {
                    this.resultListView.Items.Add(new { Type = "File", Path = file });
                }
            }
        }
    }
}