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

using Emi.UserControls.Resources;
using System;

namespace Emi.UserControls {
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>Provides a file system browser window.</summary>
    public partial class FileSystemBrowserWindow : Window {

        public static readonly DependencyProperty fileSystemListViewItemsProperty =
            DependencyProperty.Register("fileSystemListViewItems", typeof(ObservableCollection<FileSystemItem>), typeof(FileSystemBrowserWindow), new PropertyMetadata(null));

        /// <summary>The available free space column in the browser.</summary>
        private GridViewColumn availableFreeSpaceGridViewColumn = null;

        /// <summary>The file filter of the browser.</summary>
        private BrowserFilter browserFilter = null;

        /// <summary>The settings of the browser.</summary>
        private BrowserSettings browserSettings = null;
        ICollectionView collectionView1;

        /// <summary>The creation time column in the browser.</summary>
        private GridViewColumn creationTimeGridViewColumn = null;

        /// <summary>The drive format column in the browser.</summary>
        private GridViewColumn driveFormatGridViewColumn = null;

        /// <summary>The drive type column in the browser.</summary>
        private GridViewColumn driveTypeGridViewColumn = null;
        /// <summary>Indicates whether navigation has been performed.</summary>
        /// <remarks>Should an error occur during navigation the <see cref="ErrorWindow"/> is shown except on initial navigation.</remarks>
        private bool isInitialNavigation = false;

        /// <summary>The last access time column in the browser.</summary>
        private GridViewColumn lastAccessTimeGridViewColumn = null;

        /// <summary>The last write time column in the browser.</summary>
        private GridViewColumn lastWriteTimeGridViewColumn = null;

        /// <summary>The name column in the browser.</summary>
        private GridViewColumn nameGridViewColumn = null;

        /// <summary>The file system path of the browser.</summary>
        private string path = string.Empty;

        /// <summary>The root directory column in the browser.</summary>
        private GridViewColumn rootDirectoryGridViewColumn = null;

        /// <summary>The selected directories in the browser.</summary>
        private Collection<string> selectedDirectories = new Collection<string>();

        /// <summary>The selected files in the browser.</summary>
        private Collection<string> selectedFiles = new Collection<string>();

        /// <summary>The size column in the browser.</summary>
        private GridViewColumn sizeGridViewColumn = null;

        /// <summary>The total free space column in the browser.</summary>
        private GridViewColumn totalFreeSpaceGridViewColumn = null;

        /// <summary>The total size column in the browser.</summary>
        private GridViewColumn totalSizeGridViewColumn = null;

        /// <summary>The volume label column in the browser.</summary>
        private GridViewColumn volumeLabelGridViewColumn = null;

        /// <summary>Initialises a new instance of the <see cref="FileSystemBrowserWindow"/> class.</summary>
        public FileSystemBrowserWindow()
            : this(new BrowserFilter(), new BrowserSettings()) {
        }

        /// <summary>Initialises a new instance of the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <param name="browserFilter">The file filter for the browser.</param>
        public FileSystemBrowserWindow(BrowserFilter browserFilter)
            : this(browserFilter, new BrowserSettings()) {
        }

        /// <summary>Initialises a new instance of the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <param name="browserSettings">The settings for the browser.</param>
        public FileSystemBrowserWindow(BrowserSettings browserSettings)
            : this(new BrowserFilter(), browserSettings) {
        }

        /// <summary>Initialises a new instance of the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <param name="browserFilter">The file filter for the browser.</param>
        /// <param name="browserSettings">The settings for the browser.</param>
        public FileSystemBrowserWindow(BrowserFilter browserFilter, BrowserSettings browserSettings) {
            this.browserFilter = browserFilter;
            this.browserSettings = browserSettings;

            if(browserSettings == null)
                throw new ArgumentNullException(nameof(browserSettings));

            if(this.browserSettings.MinimumFiles > this.browserSettings.MaximumFiles)
                throw new MinimumGreaterThanMaximumException("The minimum number of files is greater than the maximum number of files.");

            if(this.browserSettings.MinimumDirectories > this.browserSettings.MaximumDirectories)
                throw new MinimumGreaterThanMaximumException("The minimum number of folders is greater than the maximum number of folders.");

            InitializeComponent();
            fileSystemListView.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(onClick), true);
            fileSystemListViewItems = new ObservableCollection<FileSystemItem>();
            CollectionView = CollectionViewSource.GetDefaultView(fileSystemListViewItems);
            fileSystemListView.ItemsSource = CollectionView;
            /*
             * Get grid view column references.
             */

            GridView fileSystemGridView = (GridView)fileSystemListView.View;

            nameGridViewColumn = fileSystemGridView.Columns[0];
            sizeGridViewColumn = fileSystemGridView.Columns[1];
            lastAccessTimeGridViewColumn = fileSystemGridView.Columns[2];
            lastWriteTimeGridViewColumn = fileSystemGridView.Columns[3];
            creationTimeGridViewColumn = fileSystemGridView.Columns[4];
            volumeLabelGridViewColumn = fileSystemGridView.Columns[5];
            rootDirectoryGridViewColumn = fileSystemGridView.Columns[6];
            driveTypeGridViewColumn = fileSystemGridView.Columns[7];
            driveFormatGridViewColumn = fileSystemGridView.Columns[8];
            availableFreeSpaceGridViewColumn = fileSystemGridView.Columns[9];
            totalFreeSpaceGridViewColumn = fileSystemGridView.Columns[10];
            totalSizeGridViewColumn = fileSystemGridView.Columns[11];

            /*
             * this.Title
             */

            switch(this.browserSettings.BrowsingMode) {
                case BrowsingMode.Open:
                    if(this.browserSettings.MaximumFiles == 1 && this.browserSettings.MaximumDirectories == 0)
                        this.Title = Titles.OpenFile;
                    else if(this.browserSettings.MaximumFiles > 1 && this.browserSettings.MaximumDirectories == 0)
                        this.Title = Titles.OpenFiles;
                    else if(this.browserSettings.MaximumFiles == 0 && this.browserSettings.MaximumDirectories == 1)
                        this.Title = Titles.OpenFolder;
                    else if(this.browserSettings.MinimumFiles == 0 && this.browserSettings.MaximumDirectories > 1)
                        this.Title = Titles.OpenFolders;
                    else if(this.browserSettings.MaximumFiles == 1 && this.browserSettings.MaximumDirectories == 1)
                        this.Title = Titles.OpenFolderAndFile;
                    else if(this.browserSettings.MaximumFiles > 1 && this.browserSettings.MaximumDirectories == 1)
                        this.Title = Titles.OpenFolderAndFiles;
                    else if(this.browserSettings.MaximumFiles == 1 && this.browserSettings.MaximumDirectories > 1)
                        this.Title = Titles.OpenFoldersAndFile;
                    else if(this.browserSettings.MaximumFiles > 1 && this.browserSettings.MaximumDirectories > 1)
                        this.Title = Titles.OpenFoldersAndFiles;

                    break;
                case BrowsingMode.Select:
                    if(this.browserSettings.MaximumFiles == 1 && this.browserSettings.MaximumDirectories == 0)
                        this.Title = Titles.SelectFile;
                    else if(this.browserSettings.MaximumFiles > 1 && this.browserSettings.MaximumDirectories == 0)
                        this.Title = Titles.SelectFiles;
                    else if(this.browserSettings.MaximumFiles == 0 && this.browserSettings.MaximumDirectories == 1)
                        this.Title = Titles.SelectFolder;
                    else if(this.browserSettings.MinimumFiles == 0 && this.browserSettings.MaximumDirectories > 1)
                        this.Title = Titles.SelectFolders;
                    else if(this.browserSettings.MaximumFiles == 1 && this.browserSettings.MaximumDirectories == 1)
                        this.Title = Titles.SelectFolderAndFile;
                    else if(this.browserSettings.MaximumFiles > 1 && this.browserSettings.MaximumDirectories == 1)
                        this.Title = Titles.SelectFolderAndFiles;
                    else if(this.browserSettings.MaximumFiles == 1 && this.browserSettings.MaximumDirectories > 1)
                        this.Title = Titles.SelectFoldersAndFile;
                    else if(this.browserSettings.MaximumFiles > 1 && this.browserSettings.MaximumDirectories > 1)
                        this.Title = Titles.SelectFoldersAndFiles;

                    break;
            }

            /*
             * this.navigationStackPanel
             */

            PathButton desktopNavigationButton = new PathButton(NavigationButtons.Desktop, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), new RoutedEventHandler(PathButton_Click));
            PathButton recentNavigationButton = new PathButton(NavigationButtons.Recent, Environment.GetFolderPath(Environment.SpecialFolder.Recent), new RoutedEventHandler(PathButton_Click));
            PathButton documentsNavigationButton = new PathButton(NavigationButtons.Documents, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), new RoutedEventHandler(PathButton_Click));
            PathButton musicNavigationButton = new PathButton(NavigationButtons.Music, Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), new RoutedEventHandler(PathButton_Click));
            PathButton picturesNavigationButton = new PathButton(NavigationButtons.Pictures, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), new RoutedEventHandler(PathButton_Click));
            PathButton videosNavigationButton = new PathButton(NavigationButtons.Videos, Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), new RoutedEventHandler(PathButton_Click));

            navigationStackPanel.Children.Add(desktopNavigationButton);
            navigationStackPanel.Children.Add(recentNavigationButton);
            navigationStackPanel.Children.Add(documentsNavigationButton);
            navigationStackPanel.Children.Add(musicNavigationButton);
            navigationStackPanel.Children.Add(picturesNavigationButton);
            navigationStackPanel.Children.Add(videosNavigationButton);

            /*
             * this.actionButton, this.cancelButton
             */

            switch(this.browserSettings.BrowsingMode) {
                case BrowsingMode.Open:
                    actionButton.Content = Buttons.Open;

                    break;
                case BrowsingMode.Select:
                    actionButton.Content = Buttons.Select;

                    break;
            }

            cancelButton.Content = Buttons.Cancel;

            /*
             * this.NavigateFileSystem
             */

            path = PathManipulator.Sanitise(this.browserSettings.Path);

            NavigateFileSystem(path);
        }

        void onClick(object sender, RoutedEventArgs e) {
            GridViewColumnHeader gvch = e.OriginalSource as GridViewColumnHeader;
            var bind = (Binding)gvch.Column.DisplayMemberBinding;
            string colName = bind?.Path?.Path;
            if(string.IsNullOrEmpty(colName))
                return;
            if(gvch != null)
                if(gvch.Tag == null) {
                    gvch.Tag = ListSortDirection.Ascending;
                    gvch.Column.HeaderTemplate = (DataTemplate)Resources["HeaderTemplateArrowUp"];
                    UpdateSortDescriptions(colName, ListSortDirection.Ascending);
                } else
                    if((ListSortDirection)gvch.Tag == ListSortDirection.Ascending) {
                    gvch.Tag = ListSortDirection.Descending;
                    gvch.Column.HeaderTemplate = (DataTemplate)Resources["HeaderTemplateArrowDown"];
                    UpdateSortDescriptions(colName, ListSortDirection.Descending);
                } else {
                    gvch.Tag = null;
                    gvch.Column.HeaderTemplate = null;
                    UpdateSortDescriptions(colName, null);

                }

        }

        protected void UpdateSortDescriptions(string colName, ListSortDirection? dir) {
            CollectionView.SortDescriptions.Clear();
            if(dir.HasValue)
                CollectionView.SortDescriptions.Add(new SortDescription(colName, dir.Value));

        }

        internal ObservableCollection<FileSystemItem> fileSystemListViewItems {
            get => (ObservableCollection<FileSystemItem>)GetValue(fileSystemListViewItemsProperty);
            set => SetValue(fileSystemListViewItemsProperty, value);
        }

        public ICollectionView CollectionView {
            get => collectionView1;
            set => collectionView1 = value;
        }


    }
}