// <author>Stefán Örvar Sigmundsson</author>
// <copyright company="eMedia Intellect" file="ActionButton_Click.cs">
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
    using System.Collections;
    using System.IO;
    using System.Windows;

    /// <content>Contains the <see cref="ActionButton_Click"/> method.</content>
    public partial class FileSystemBrowserWindow : Window
    {
        /// <summary>Closes the window and signals that the browsing was successful when the button is clicked.</summary>
        /// <param name="sender">The sender object of the event handler.</param>
        /// <param name="e">The state information of the event handler.</param>
        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            IList selectedItemsIList = this.fileSystemListView.SelectedItems;

            foreach (object currentItem in selectedItemsIList)
            {
                FileSystemItem currentFileSystemItem = (FileSystemItem)currentItem;

                switch (currentFileSystemItem.FileSystemItemType)
                {
                    case FileSystemItemType.Directory:
                        this.selectedDirectories.Add(this.browserSettings.Path + Path.DirectorySeparatorChar + currentFileSystemItem.Name);

                        break;
                    case FileSystemItemType.File:
                        this.selectedFiles.Add(this.browserSettings.Path + Path.DirectorySeparatorChar + currentFileSystemItem.Name);

                        break;
                }
            }

            this.DialogResult = true;
        }
    }
}