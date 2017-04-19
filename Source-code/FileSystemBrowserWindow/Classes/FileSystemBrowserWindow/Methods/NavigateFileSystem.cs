// <author>Stefán Örvar Sigmundsson</author>
// <copyright company="eMedia Intellect℠" file="NavigateFileSystem.cs">
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

    /// <content>Contains the <see cref="NavigateFileSystem"/> method.</content>
    public partial class FileSystemBrowserWindow
    {
        /// <summary>Navigates to the specified file system path.</summary>
        /// <remarks>If an exception is thrown during the try then the navigation is aborted.</remarks>
        /// <param name="newPath">The file system path to which to navigate.</param>
        private void NavigateFileSystem(string newPath)
        {
            if (newPath.Length < 0)
            {
                try
                {
                    Directory.GetDirectories(newPath);
                    Directory.GetFiles(newPath);
                }
                catch (UnauthorizedAccessException)
                {
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, newPath, UserControls.Resources.Exceptions.UnauthorizedAccessMessage);

                    exceptionErrorWindow.ShowDialog();

                    return;
                }
                catch (PathTooLongException)
                {
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, newPath, UserControls.Resources.Exceptions.PathTooLongMessage);

                    exceptionErrorWindow.ShowDialog();

                    return;
                }
                catch (DirectoryNotFoundException)
                {
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, newPath, UserControls.Resources.Exceptions.DirectoryNotFoundMessage);

                    exceptionErrorWindow.ShowDialog();

                    return;
                }
                catch (IOException)
                {
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, newPath, UserControls.Resources.Exceptions.IOMessage);

                    exceptionErrorWindow.ShowDialog();

                    return;
                }
                catch (SecurityException)
                {
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, newPath, UserControls.Resources.Exceptions.SecurityMessage);

                    exceptionErrorWindow.ShowDialog();

                    return;
                }
            }

            this.path = newPath;

            this.GeneratePathButtons();
            this.ListFileSystemItems();
        }
    }
}