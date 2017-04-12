// <author>Stefán Örvar Sigmundsson</author>
// <copyright company="eMedia Intellect℠" file="ListFileSystem.cs">
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
    using System.Windows;

    /// <content>Contains the <see cref="ListFileSystem"/> method.</content>
    public partial class FileSystemBrowserWindow
    {
        /// <summary>Lists the directories and files of the path.</summary>
        /// <remarks>If an exception is thrown during the tries then the navigation is redirected to the parent directory.</remarks>
        private void ListFileSystem()
        {
            this.fileSystemListView.Items.Clear();

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

                this.Navigate(this.browserSettings.Path.Substring(0, this.browserSettings.Path.LastIndexOf('\\')));

                return;
            }
            catch (SecurityException)
            {
                ErrorWindow exceptionErrorWindow = new ErrorWindow(this, this.browserSettings.Path, UserControls.Resources.Exceptions.SecurityMessage);

                exceptionErrorWindow.ShowDialog();

                this.Navigate(this.browserSettings.Path.Substring(0, this.browserSettings.Path.LastIndexOf('\\')));

                return;
            }
            catch (UnauthorizedAccessException)
            {
                ErrorWindow exceptionErrorWindow = new ErrorWindow(this, this.browserSettings.Path, UserControls.Resources.Exceptions.UnauthorizedAccessMessage);

                exceptionErrorWindow.ShowDialog();

                this.Navigate(this.browserSettings.Path.Substring(0, this.browserSettings.Path.LastIndexOf('\\')));

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

                this.Navigate(this.browserSettings.Path.Substring(0, this.browserSettings.Path.LastIndexOf('\\')));

                return;
            }

            this.computerListView.Visibility = Visibility.Collapsed;

            if (this.fileSystemListView.HasItems)
            {
                this.fileSystemListView.ScrollIntoView(this.fileSystemListView.Items[0]);
            }

            this.fileSystemListView.Visibility = Visibility.Visible;
        }
    }
}