// <author>Stefán Örvar Sigmundsson</author>
// <copyright company="eMedia Intellect" file="FileSystemBrowserWindow.xaml.cs">
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
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>Provides a file system browser window.</summary>
    public partial class FileSystemBrowserWindow : Window
    {
        /// <summary>The file filter of the browser.</summary>
        private BrowserFilter browserFilter = null;

        /// <summary>The settings of the browser.</summary>
        private BrowserSettings browserSettings = null;

        /// <summary>The selected directories in the browser.</summary>
        private Collection<string> selectedDirectories = new Collection<string>();

        /// <summary>The selected files in the browser.</summary>
        private Collection<string> selectedFiles = new Collection<string>();

        /// <summary>Initialises a new instance of the <see cref="FileSystemBrowserWindow"/> class.</summary>
        public FileSystemBrowserWindow()
            : this(new BrowserFilter(), new BrowserSettings())
        {
        }

        /// <summary>Initialises a new instance of the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <param name="browserFilter">The file filter for the browser.</param>
        public FileSystemBrowserWindow(BrowserFilter browserFilter)
            : this(browserFilter, new BrowserSettings())
        {
        }

        /// <summary>Initialises a new instance of the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <param name="browserFilter">The file filter for the browser.</param>
        /// <param name="browserSettings">The settings for the browser.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "The method is as complex as it must be.")]
        public FileSystemBrowserWindow(BrowserFilter browserFilter, BrowserSettings browserSettings)
        {
            this.browserFilter = browserFilter;
            this.browserSettings = browserSettings;

            if (browserSettings == null)
            {
                throw new ArgumentNullException("browserSettings");
            }

            if (this.browserSettings.MinimumFiles > this.browserSettings.MaximumFiles)
            {
                throw new MinimumGreaterThanMaximumException("The minimum number of files is greater than the maximum number of files.");
            }

            if (this.browserSettings.MinimumDirectories > this.browserSettings.MaximumDirectories)
            {
                throw new MinimumGreaterThanMaximumException("The minimum number of folders is greater than the maximum number of folders.");
            }

            this.InitializeComponent();

            /*
             * this.Title
             */

            switch (this.browserSettings.BrowsingMode)
            {
                case BrowsingMode.Open:
                    if (this.browserSettings.MaximumFiles == 1 && this.browserSettings.MaximumDirectories == 0)
                    {
                        this.Title = UserControls.Resources.Titles.OpenFile;
                    }
                    else if (this.browserSettings.MaximumFiles > 1 && this.browserSettings.MaximumDirectories == 0)
                    {
                        this.Title = UserControls.Resources.Titles.OpenFiles;
                    }
                    else if (this.browserSettings.MaximumFiles == 0 && this.browserSettings.MaximumDirectories == 1)
                    {
                        this.Title = UserControls.Resources.Titles.OpenFolder;
                    }
                    else if (this.browserSettings.MinimumFiles == 0 && this.browserSettings.MaximumDirectories > 1)
                    {
                        this.Title = UserControls.Resources.Titles.OpenFolders;
                    }
                    else if (this.browserSettings.MaximumFiles == 1 && this.browserSettings.MaximumDirectories == 1)
                    {
                        this.Title = UserControls.Resources.Titles.OpenFolderAndFile;
                    }
                    else if (this.browserSettings.MaximumFiles > 1 && this.browserSettings.MaximumDirectories == 1)
                    {
                        this.Title = UserControls.Resources.Titles.OpenFolderAndFiles;
                    }
                    else if (this.browserSettings.MaximumFiles == 1 && this.browserSettings.MaximumDirectories > 1)
                    {
                        this.Title = UserControls.Resources.Titles.OpenFoldersAndFile;
                    }
                    else if (this.browserSettings.MaximumFiles > 1 && this.browserSettings.MaximumDirectories > 1)
                    {
                        this.Title = UserControls.Resources.Titles.OpenFoldersAndFiles;
                    }

                    break;
                case BrowsingMode.Select:
                    if (this.browserSettings.MaximumFiles == 1 && this.browserSettings.MaximumDirectories == 0)
                    {
                        this.Title = UserControls.Resources.Titles.SelectFile;
                    }
                    else if (this.browserSettings.MaximumFiles > 1 && this.browserSettings.MaximumDirectories == 0)
                    {
                        this.Title = UserControls.Resources.Titles.SelectFiles;
                    }
                    else if (this.browserSettings.MaximumFiles == 0 && this.browserSettings.MaximumDirectories == 1)
                    {
                        this.Title = UserControls.Resources.Titles.SelectFolder;
                    }
                    else if (this.browserSettings.MinimumFiles == 0 && this.browserSettings.MaximumDirectories > 1)
                    {
                        this.Title = UserControls.Resources.Titles.SelectFolders;
                    }
                    else if (this.browserSettings.MaximumFiles == 1 && this.browserSettings.MaximumDirectories == 1)
                    {
                        this.Title = UserControls.Resources.Titles.SelectFolderAndFile;
                    }
                    else if (this.browserSettings.MaximumFiles > 1 && this.browserSettings.MaximumDirectories == 1)
                    {
                        this.Title = UserControls.Resources.Titles.SelectFolderAndFiles;
                    }
                    else if (this.browserSettings.MaximumFiles == 1 && this.browserSettings.MaximumDirectories > 1)
                    {
                        this.Title = UserControls.Resources.Titles.SelectFoldersAndFile;
                    }
                    else if (this.browserSettings.MaximumFiles > 1 && this.browserSettings.MaximumDirectories > 1)
                    {
                        this.Title = UserControls.Resources.Titles.SelectFoldersAndFiles;
                    }

                    break;
            }

            /*
             * this.navigationStackPanel
             */

            PathButton desktopNavigationButton = new PathButton(UserControls.Resources.NavigationButtons.Desktop, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), new RoutedEventHandler(this.PathButton_Click));
            PathButton recentNavigationButton = new PathButton(UserControls.Resources.NavigationButtons.Recent, Environment.GetFolderPath(Environment.SpecialFolder.Recent), new RoutedEventHandler(this.PathButton_Click));
            PathButton documentsNavigationButton = new PathButton(UserControls.Resources.NavigationButtons.Documents, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), new RoutedEventHandler(this.PathButton_Click));
            PathButton musicNavigationButton = new PathButton(UserControls.Resources.NavigationButtons.Music, Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), new RoutedEventHandler(this.PathButton_Click));
            PathButton picturesNavigationButton = new PathButton(UserControls.Resources.NavigationButtons.Pictures, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), new RoutedEventHandler(this.PathButton_Click));
            PathButton videosNavigationButton = new PathButton(UserControls.Resources.NavigationButtons.Videos, Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), new RoutedEventHandler(this.PathButton_Click));

            this.navigationStackPanel.Children.Add(desktopNavigationButton);
            this.navigationStackPanel.Children.Add(recentNavigationButton);
            this.navigationStackPanel.Children.Add(documentsNavigationButton);
            this.navigationStackPanel.Children.Add(musicNavigationButton);
            this.navigationStackPanel.Children.Add(picturesNavigationButton);
            this.navigationStackPanel.Children.Add(videosNavigationButton);

            /*
             * this.computerListView
             */

            GridView computerGridView = new GridView();

            GridViewColumn nameComputerGridViewColumn = new GridViewColumn();

            nameComputerGridViewColumn.DisplayMemberBinding = new Binding("Name");
            nameComputerGridViewColumn.Header = UserControls.Resources.ComputerGridViewColumns.Name;

            computerGridView.Columns.Add(nameComputerGridViewColumn);

            if (this.browserSettings.HasVolumeLabel)
            {
                GridViewColumn volumeLabelComputerGridViewColumn = new GridViewColumn();

                volumeLabelComputerGridViewColumn.DisplayMemberBinding = new Binding("VolumeLabel");
                volumeLabelComputerGridViewColumn.Header = UserControls.Resources.ComputerGridViewColumns.VolumeLabel;

                computerGridView.Columns.Add(volumeLabelComputerGridViewColumn);
            }

            if (this.browserSettings.HasRootDirectory)
            {
                GridViewColumn rootDirectoryComputerGridViewColumn = new GridViewColumn();

                rootDirectoryComputerGridViewColumn.DisplayMemberBinding = new Binding("RootDirectory");
                rootDirectoryComputerGridViewColumn.Header = UserControls.Resources.ComputerGridViewColumns.RootDirectory;

                computerGridView.Columns.Add(rootDirectoryComputerGridViewColumn);
            }

            if (this.browserSettings.HasDriveType)
            {
                GridViewColumn driveTypeComputerGridViewColumn = new GridViewColumn();

                driveTypeComputerGridViewColumn.DisplayMemberBinding = new Binding("DriveType");
                driveTypeComputerGridViewColumn.Header = UserControls.Resources.ComputerGridViewColumns.DriveType;

                computerGridView.Columns.Add(driveTypeComputerGridViewColumn);
            }

            if (this.browserSettings.HasDriveFormat)
            {
                GridViewColumn driveFormatComputerGridViewColumn = new GridViewColumn();

                driveFormatComputerGridViewColumn.DisplayMemberBinding = new Binding("DriveFormat");
                driveFormatComputerGridViewColumn.Header = UserControls.Resources.ComputerGridViewColumns.DriveFormat;

                computerGridView.Columns.Add(driveFormatComputerGridViewColumn);
            }

            if (this.browserSettings.HasAvailableFreeSpace)
            {
                GridViewColumn availableFreeSpaceComputerGridViewColumn = new GridViewColumn();

                availableFreeSpaceComputerGridViewColumn.DisplayMemberBinding = new Binding("AvailableFreeSpace");
                availableFreeSpaceComputerGridViewColumn.Header = UserControls.Resources.ComputerGridViewColumns.AvailableFreeSpace;

                computerGridView.Columns.Add(availableFreeSpaceComputerGridViewColumn);
            }

            if (this.browserSettings.HasTotalFreeSpace)
            {
                GridViewColumn totalFreeSpaceComputerGridViewColumn = new GridViewColumn();

                totalFreeSpaceComputerGridViewColumn.DisplayMemberBinding = new Binding("TotalFreeSpace");
                totalFreeSpaceComputerGridViewColumn.Header = UserControls.Resources.ComputerGridViewColumns.TotalFreeSpace;

                computerGridView.Columns.Add(totalFreeSpaceComputerGridViewColumn);
            }

            if (this.browserSettings.HasTotalSize)
            {
                GridViewColumn totalSizeComputerGridViewColumn = new GridViewColumn();

                totalSizeComputerGridViewColumn.DisplayMemberBinding = new Binding("TotalSize");
                totalSizeComputerGridViewColumn.Header = UserControls.Resources.ComputerGridViewColumns.TotalSize;

                computerGridView.Columns.Add(totalSizeComputerGridViewColumn);
            }

            this.computerListView.View = computerGridView;

            /*
             * this.fileSystemListView
             */

            GridView fileSystemGridView = new GridView();

            GridViewColumn nameFileSystemGridViewColumn = new GridViewColumn();

            nameFileSystemGridViewColumn.DisplayMemberBinding = new Binding("Name");
            nameFileSystemGridViewColumn.Header = UserControls.Resources.FileSystemGridViewColumns.Name;
            nameFileSystemGridViewColumn.Width = 250;

            fileSystemGridView.Columns.Add(nameFileSystemGridViewColumn);

            GridViewColumn sizeFileSystemGridViewColumn = new GridViewColumn();

            sizeFileSystemGridViewColumn.DisplayMemberBinding = new Binding("Size");
            sizeFileSystemGridViewColumn.Header = UserControls.Resources.FileSystemGridViewColumns.Size;
            sizeFileSystemGridViewColumn.Width = 55;

            fileSystemGridView.Columns.Add(sizeFileSystemGridViewColumn);

            GridViewColumn lastWriteTimeFileSystemGridViewColumn = new GridViewColumn();

            lastWriteTimeFileSystemGridViewColumn.DisplayMemberBinding = new Binding("LastWriteTime");
            lastWriteTimeFileSystemGridViewColumn.Header = UserControls.Resources.FileSystemGridViewColumns.LastWriteTime;

            fileSystemGridView.Columns.Add(lastWriteTimeFileSystemGridViewColumn);

            GridViewColumn lastAccessTimeFileSystemGridViewColumn = new GridViewColumn();

            lastAccessTimeFileSystemGridViewColumn.DisplayMemberBinding = new Binding("LastAccessTime");
            lastAccessTimeFileSystemGridViewColumn.Header = UserControls.Resources.FileSystemGridViewColumns.LastAccessTime;

            fileSystemGridView.Columns.Add(lastAccessTimeFileSystemGridViewColumn);

            GridViewColumn creationTimeFileSystemGridViewColumn = new GridViewColumn();

            creationTimeFileSystemGridViewColumn.DisplayMemberBinding = new Binding("CreationTime");
            creationTimeFileSystemGridViewColumn.Header = UserControls.Resources.FileSystemGridViewColumns.CreationTime;

            fileSystemGridView.Columns.Add(creationTimeFileSystemGridViewColumn);

            this.fileSystemListView.View = fileSystemGridView;

            /*
             * this.actionButton, this.cancelButton
             */

            switch (this.browserSettings.BrowsingMode)
            {
                case BrowsingMode.Open:
                    this.actionButton.Content = UserControls.Resources.Buttons.Open;

                    break;
                case BrowsingMode.Select:
                    this.actionButton.Content = UserControls.Resources.Buttons.Select;

                    break;
            }

            this.cancelButton.Content = UserControls.Resources.Buttons.Cancel;

            this.Navigate(browserSettings.Path);
        }

        /// <summary>Initialises a new instance of the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <param name="browserSettings">The settings for the browser.</param>
        public FileSystemBrowserWindow(BrowserSettings browserSettings)
            : this(new BrowserFilter(), browserSettings)
        {
        }
    }
}