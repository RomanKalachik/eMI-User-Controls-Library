// <author>Stefán Örvar Sigmundsson</author>
// <copyright company="eMedia Intellect℠" file="FileSystemBrowserWindow.xaml.cs">
//    Copyright © 2016–2017 eMedia Intellect℠
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

    /// <summary>Provides a file system browser window.</summary>
    public partial class FileSystemBrowserWindow : Window
    {
        /// <summary>Indicates whether navigation has been performed.</summary>
        /// <remarks>Should an error occur during navigation the <see cref="ErrorWindow"/> is shown except on initial navigation.</remarks>
        private bool isInitialNavigation = false;

        /// <summary>The file filter of the browser.</summary>
        private BrowserFilter browserFilter = null;

        /// <summary>The settings of the browser.</summary>
        private BrowserSettings browserSettings = null;

        /// <summary>The selected directories in the browser.</summary>
        private Collection<string> selectedDirectories = new Collection<string>();

        /// <summary>The selected files in the browser.</summary>
        private Collection<string> selectedFiles = new Collection<string>();

        /// <summary>The name column in the browser.</summary>
        private GridViewColumn nameGridViewColumn = null;

        /// <summary>The size column in the browser.</summary>
        private GridViewColumn sizeGridViewColumn = null;

        /// <summary>The last access time column in the browser.</summary>
        private GridViewColumn lastAccessTimeGridViewColumn = null;

        /// <summary>The last write time column in the browser.</summary>
        private GridViewColumn lastWriteTimeGridViewColumn = null;

        /// <summary>The creation time column in the browser.</summary>
        private GridViewColumn creationTimeGridViewColumn = null;

        /// <summary>The volume label column in the browser.</summary>
        private GridViewColumn volumeLabelGridViewColumn = null;

        /// <summary>The root directory column in the browser.</summary>
        private GridViewColumn rootDirectoryGridViewColumn = null;

        /// <summary>The drive type column in the browser.</summary>
        private GridViewColumn driveTypeGridViewColumn = null;

        /// <summary>The drive format column in the browser.</summary>
        private GridViewColumn driveFormatGridViewColumn = null;

        /// <summary>The available free space column in the browser.</summary>
        private GridViewColumn availableFreeSpaceGridViewColumn = null;

        /// <summary>The total free space column in the browser.</summary>
        private GridViewColumn totalFreeSpaceGridViewColumn = null;

        /// <summary>The total size column in the browser.</summary>
        private GridViewColumn totalSizeGridViewColumn = null;

        /// <summary>The file system path of the browser.</summary>
        private string path = string.Empty;

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
             * Get grid view column references.
             */

            GridView fileSystemGridView = (GridView)this.fileSystemListView.View;

            this.nameGridViewColumn = fileSystemGridView.Columns[0];
            this.sizeGridViewColumn = fileSystemGridView.Columns[1];
            this.lastAccessTimeGridViewColumn = fileSystemGridView.Columns[2];
            this.lastWriteTimeGridViewColumn = fileSystemGridView.Columns[3];
            this.creationTimeGridViewColumn = fileSystemGridView.Columns[4];
            this.volumeLabelGridViewColumn = fileSystemGridView.Columns[5];
            this.rootDirectoryGridViewColumn = fileSystemGridView.Columns[6];
            this.driveTypeGridViewColumn = fileSystemGridView.Columns[7];
            this.driveFormatGridViewColumn = fileSystemGridView.Columns[8];
            this.availableFreeSpaceGridViewColumn = fileSystemGridView.Columns[9];
            this.totalFreeSpaceGridViewColumn = fileSystemGridView.Columns[10];
            this.totalSizeGridViewColumn = fileSystemGridView.Columns[11];

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

            /*
             * this.NavigateFileSystem
             */

            this.path = PathManipulator.Sanitise(this.browserSettings.Path);

            this.NavigateFileSystem(this.path);
        }

        /// <summary>Initialises a new instance of the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <param name="browserSettings">The settings for the browser.</param>
        public FileSystemBrowserWindow(BrowserSettings browserSettings)
            : this(new BrowserFilter(), browserSettings)
        {
        }
    }
}