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
            this.fileSystemListView.Items.Clear();

            GridView fileSystemGridview = (GridView)this.fileSystemListView.View;

            fileSystemGridview.Columns.Clear();

            if (this.browserSettings.Path.Length == 0)
            {
                fileSystemGridview.Columns.Insert(0, this.nameGridViewColumn);
                fileSystemGridview.Columns.Insert(1, this.volumeLabelGridViewColumn);
                fileSystemGridview.Columns.Insert(2, this.rootDirectoryGridViewColumn);
                fileSystemGridview.Columns.Insert(3, this.driveTypeGridViewColumn);
                fileSystemGridview.Columns.Insert(4, this.driveFormatGridViewColumn);
                fileSystemGridview.Columns.Insert(5, this.availableFreeSpaceGridViewColumn);
                fileSystemGridview.Columns.Insert(6, this.totalFreeSpaceGridViewColumn);
                fileSystemGridview.Columns.Insert(7, this.totalSizeGridViewColumn);

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
                    else
                    {
                        fileSystemGridview.Columns.Remove(this.volumeLabelGridViewColumn);
                    }

                    if (this.browserSettings.HasRootDirectory)
                    {
                        DirectoryInfo currentDirectoryInfo = currentDriveInfo.RootDirectory;

                        currentFileSystemItem.RootDirectory = currentDirectoryInfo.Name;
                    }
                    else
                    {
                        fileSystemGridview.Columns.Remove(this.rootDirectoryGridViewColumn);
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
                    else
                    {
                        fileSystemGridview.Columns.Remove(this.driveTypeGridViewColumn);
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
                    else
                    {
                        fileSystemGridview.Columns.Remove(this.driveFormatGridViewColumn);
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
                    else
                    {
                        fileSystemGridview.Columns.Remove(this.availableFreeSpaceGridViewColumn);
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
                    else
                    {
                        fileSystemGridview.Columns.Remove(this.totalFreeSpaceGridViewColumn);
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
                    else
                    {
                        fileSystemGridview.Columns.Remove(this.totalSizeGridViewColumn);
                    }

                    this.fileSystemListView.Items.Add(currentFileSystemItem);
                }
            }
            else
            {
                fileSystemGridview.Columns.Insert(0, this.nameGridViewColumn);
                fileSystemGridview.Columns.Insert(1, this.sizeGridViewColumn);
                fileSystemGridview.Columns.Insert(2, this.lastWriteTimeGridViewColumn);
                fileSystemGridview.Columns.Insert(3, this.lastAccessTimeGridViewColumn);
                fileSystemGridview.Columns.Insert(4, this.creationTimeGridViewColumn);

                if (this.browserSettings.Path.EndsWith(":", StringComparison.Ordinal))
                {
                    this.browserSettings.Path += "\\";
                }

                DirectoryInfo listingDirectoryInfo = new DirectoryInfo(this.browserSettings.Path);

                try
                {
                    foreach (DirectoryInfo currentDirectoryInfo in listingDirectoryInfo.GetDirectories())
                    {
                        this.fileSystemListView.Items.Add(new FileSystemItem() { ByteMultiple = this.browserSettings.ByteMultiple, FileSystemItemType = FileSystemItemType.Directory, Name = currentDirectoryInfo.Name, Accessed = currentDirectoryInfo.LastAccessTime, Written = currentDirectoryInfo.LastWriteTime, Created = currentDirectoryInfo.CreationTime });
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, this.browserSettings.Path, UserControls.Resources.Exceptions.DirectoryNotFoundMessage);

                    exceptionErrorWindow.ShowDialog();

                    this.NavigateFileSystem(this.browserSettings.Path.Substring(0, this.browserSettings.Path.LastIndexOf('\\')));

                    return;
                }
                catch (SecurityException)
                {
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, this.browserSettings.Path, UserControls.Resources.Exceptions.SecurityMessage);

                    exceptionErrorWindow.ShowDialog();

                    this.NavigateFileSystem(this.browserSettings.Path.Substring(0, this.browserSettings.Path.LastIndexOf('\\')));

                    return;
                }
                catch (UnauthorizedAccessException)
                {
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, this.browserSettings.Path, UserControls.Resources.Exceptions.UnauthorizedAccessMessage);

                    exceptionErrorWindow.ShowDialog();

                    this.NavigateFileSystem(this.browserSettings.Path.Substring(0, this.browserSettings.Path.LastIndexOf('\\')));

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
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, this.browserSettings.Path, UserControls.Resources.Exceptions.DirectoryNotFoundMessage);

                    exceptionErrorWindow.ShowDialog();

                    this.NavigateFileSystem(this.browserSettings.Path.Substring(0, this.browserSettings.Path.LastIndexOf('\\')));

                    return;
                }
            }

            if (this.fileSystemListView.HasItems)
            {
                this.fileSystemListView.ScrollIntoView(this.fileSystemListView.Items[0]);
            }
        }
    }
}