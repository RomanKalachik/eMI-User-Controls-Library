// <author>Stefán Örvar Sigmundsson</author>
// <copyright company="eMedia Intellect℠" file="ListFileSystemItems.cs">
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
    using System.IO;
    using System.Security;
    using System.Windows.Controls;

    /// <content>Contains the <see cref="ListFileSystemItems"/> method.</content>
    public partial class FileSystemBrowserWindow
    {
        /// <summary>Lists the drives, directories and files of the file system path.</summary>
        /// <remarks>If an exception is thrown during the tries then the file system navigation is redirected to the parent directory.</remarks>
        private void ListFileSystemItems()
        {
            /*
             * Remove all columns.
             */

            GridView fileSystemGridView = (GridView)this.fileSystemListView.View;

            fileSystemGridView.Columns.Clear();

            fileSystemGridView.Columns.Add(this.nameGridViewColumn);

            /*
             * Remove all items.
             */

            this.fileSystemListView.Items.Clear();

            /*
             * Add relevant columns and items.
             */

            if (this.path.Length == 0)
            {
                if (this.browserSettings.HasVolumeLabel)
                {
                    fileSystemGridView.Columns.Add(this.volumeLabelGridViewColumn);
                }

                if (this.browserSettings.HasRootDirectory)
                {
                    fileSystemGridView.Columns.Add(this.rootDirectoryGridViewColumn);
                }

                if (this.browserSettings.HasDriveType)
                {
                    fileSystemGridView.Columns.Add(this.driveTypeGridViewColumn);
                }

                if (this.browserSettings.HasDriveFormat)
                {
                    fileSystemGridView.Columns.Add(this.driveFormatGridViewColumn);
                }

                if (this.browserSettings.HasAvailableFreeSpace)
                {
                    fileSystemGridView.Columns.Add(this.availableFreeSpaceGridViewColumn);
                }

                if (this.browserSettings.HasTotalFreeSpace)
                {
                    fileSystemGridView.Columns.Add(this.totalFreeSpaceGridViewColumn);
                }

                if (this.browserSettings.HasTotalSize)
                {
                    fileSystemGridView.Columns.Add(this.totalSizeGridViewColumn);
                }

                foreach (DriveInfo currentDriveInfo in DriveInfo.GetDrives())
                {
                    FileSystemItem currentFileSystemItem = new FileSystemItem() { ByteMultiple = this.browserSettings.ByteMultiple };

                    currentFileSystemItem.Name = currentDriveInfo.Name;

                    if (this.browserSettings.HasVolumeLabel)
                    {
                        try
                        {
                            currentFileSystemItem.VolumeLabel = currentDriveInfo.VolumeLabel;
                        }
                        catch
                        {
                        }
                    }

                    if (this.browserSettings.HasRootDirectory)
                    {
                        DirectoryInfo currentDirectoryInfo = currentDriveInfo.RootDirectory;

                        currentFileSystemItem.RootDirectory = currentDirectoryInfo.Name;
                    }

                    if (this.browserSettings.HasDriveType)
                    {
                        switch (currentDriveInfo.DriveType)
                        {
                            case DriveType.CDRom:
                                currentFileSystemItem.DriveType = UserControls.Resources.DriveTypeEnumerations.CDRom;

                                break;
                            case DriveType.Fixed:
                                currentFileSystemItem.DriveType = UserControls.Resources.DriveTypeEnumerations.Fixed;

                                break;
                            case DriveType.Network:
                                currentFileSystemItem.DriveType = UserControls.Resources.DriveTypeEnumerations.Network;

                                break;
                            case DriveType.NoRootDirectory:
                                currentFileSystemItem.DriveType = UserControls.Resources.DriveTypeEnumerations.NoRootDirectory;

                                break;
                            case DriveType.Ram:
                                currentFileSystemItem.DriveType = UserControls.Resources.DriveTypeEnumerations.Ram;

                                break;
                            case DriveType.Removable:
                                currentFileSystemItem.DriveType = UserControls.Resources.DriveTypeEnumerations.Removable;

                                break;
                            case DriveType.Unknown:
                                currentFileSystemItem.DriveType = UserControls.Resources.DriveTypeEnumerations.Unknown;

                                break;
                            default:
                                currentFileSystemItem.DriveType = currentDriveInfo.DriveType.ToString();

                                break;
                        }
                    }

                    if (this.browserSettings.HasDriveFormat)
                    {
                        try
                        {
                            currentFileSystemItem.DriveFormat = currentDriveInfo.DriveFormat;
                        }
                        catch
                        {
                        }
                    }

                    if (this.browserSettings.HasAvailableFreeSpace)
                    {
                        try
                        {
                            currentFileSystemItem.AvailableFreeSpaceLong = currentDriveInfo.AvailableFreeSpace;
                        }
                        catch
                        {
                        }
                    }

                    if (this.browserSettings.HasTotalFreeSpace)
                    {
                        try
                        {
                            currentFileSystemItem.TotalFreeSpaceLong = currentDriveInfo.TotalFreeSpace;
                        }
                        catch
                        {
                        }
                    }

                    if (this.browserSettings.HasTotalSize)
                    {
                        try
                        {
                            currentFileSystemItem.TotalSizeLong = currentDriveInfo.TotalSize;
                        }
                        catch
                        {
                        }
                    }

                    this.fileSystemListView.Items.Add(currentFileSystemItem);
                }
            }
            else
            {
                fileSystemGridView.Columns.Add(this.sizeGridViewColumn);
                fileSystemGridView.Columns.Add(this.lastAccessTimeGridViewColumn);
                fileSystemGridView.Columns.Add(this.lastWriteTimeGridViewColumn);
                fileSystemGridView.Columns.Add(this.creationTimeGridViewColumn);

                DirectoryInfo listingDirectoryInfo = new DirectoryInfo(this.path);

                try
                {
                    foreach (DirectoryInfo currentDirectoryInfo in listingDirectoryInfo.GetDirectories())
                    {
                        this.fileSystemListView.Items.Add(new FileSystemItem() { ByteMultiple = this.browserSettings.ByteMultiple, FileSystemItemType = FileSystemItemType.Directory, Name = currentDirectoryInfo.Name, Accessed = currentDirectoryInfo.LastAccessTime, Written = currentDirectoryInfo.LastWriteTime, Created = currentDirectoryInfo.CreationTime });
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    if (!this.isInitialNavigation)
                    {
                        this.isInitialNavigation = true;

                        this.NavigateFileSystem(string.Empty);

                        return;
                    }
                    else
                    {
                        ErrorWindow exceptionErrorWindow = new ErrorWindow(this, this.path, UserControls.Resources.Exceptions.DirectoryNotFoundMessage);

                        exceptionErrorWindow.ShowDialog();

                        this.NavigateFileSystem(PathManipulator.GetParentDirectory(this.path));
                    }

                    return;
                }
                catch (SecurityException)
                {
                    if (!this.isInitialNavigation)
                    {
                        this.isInitialNavigation = true;

                        this.NavigateFileSystem(string.Empty);

                        return;
                    }
                    else
                    {
                        ErrorWindow exceptionErrorWindow = new ErrorWindow(this, this.path, UserControls.Resources.Exceptions.SecurityMessage);

                        exceptionErrorWindow.ShowDialog();

                        this.NavigateFileSystem(PathManipulator.GetParentDirectory(this.path));
                    }

                    return;
                }
                catch (UnauthorizedAccessException)
                {
                    if (!this.isInitialNavigation)
                    {
                        this.isInitialNavigation = true;

                        this.NavigateFileSystem(string.Empty);

                        return;
                    }
                    else
                    {
                        ErrorWindow exceptionErrorWindow = new ErrorWindow(this, this.path, UserControls.Resources.Exceptions.UnauthorizedAccessMessage);

                        exceptionErrorWindow.ShowDialog();

                        this.NavigateFileSystem(PathManipulator.GetParentDirectory(this.path));
                    }

                    return;
                }

                try
                {
                    foreach (FileInfo currentFileInfo in listingDirectoryInfo.GetFiles())
                    {
                        if (this.browserFilter.Process(currentFileInfo))
                        {
                            this.fileSystemListView.Items.Add(new FileSystemItem() { ByteMultiple = this.browserSettings.ByteMultiple, FileSystemItemType = FileSystemItemType.File, Name = currentFileInfo.Name, Length = currentFileInfo.Length, Accessed = currentFileInfo.LastAccessTime, Written = currentFileInfo.LastWriteTime, Created = currentFileInfo.CreationTime });
                        }
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    if (!this.isInitialNavigation)
                    {
                        this.isInitialNavigation = true;

                        this.NavigateFileSystem(string.Empty);

                        return;
                    }
                    else
                    {
                        ErrorWindow exceptionErrorWindow = new ErrorWindow(this, this.path, UserControls.Resources.Exceptions.DirectoryNotFoundMessage);

                        exceptionErrorWindow.ShowDialog();

                        this.NavigateFileSystem(PathManipulator.GetParentDirectory(this.path));
                    }

                    return;
                }
            }

            /*
             * Resize the columns to fit the data.
             */

            foreach (GridViewColumn column in fileSystemGridView.Columns)
            {
                if (double.IsNaN(column.Width))
                {
                    column.Width = column.ActualWidth;
                }
                
                column.Width = double.NaN;
            }

            /*
             * Scroll to the top of the list view.
             */

            if (this.fileSystemListView.HasItems)
            {
                this.fileSystemListView.ScrollIntoView(this.fileSystemListView.Items[0]);
            }
        }
    }
}