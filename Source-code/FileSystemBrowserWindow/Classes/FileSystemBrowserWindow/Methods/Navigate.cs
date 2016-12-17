// <author>Stefán Örvar Sigmundsson</author>
// <copyright company="eMedia Intellect" file="Navigate.cs">
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
    using System.IO;
    using System.Security;
    using System.Windows;

    /// <content>Contains the <see cref="Navigate"/> method.</content>
    public partial class FileSystemBrowserWindow
    {
        /// <summary>Navigates to the specified path.</summary>
        /// <remarks>If an exception is thrown during the try then the navigation is aborted.</remarks>
        /// <param name="path">The path to which to navigate.</param>
        private void Navigate(string path)
        {
            if (path.Length == 0)
            {
                this.browserSettings.Path = path;

                this.ListComputer();
            }
            else
            {
                try
                {
                    Directory.GetDirectories(path);
                    Directory.GetFiles(path);
                }
                catch (UnauthorizedAccessException)
                {
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, path, UserControls.Resources.Exceptions.UnauthorizedAccessMessage);

                    exceptionErrorWindow.ShowDialog();

                    return;
                }
                catch (PathTooLongException)
                {
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, path, UserControls.Resources.Exceptions.PathTooLongMessage);

                    exceptionErrorWindow.ShowDialog();

                    return;
                }
                catch (DirectoryNotFoundException)
                {
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, path, UserControls.Resources.Exceptions.DirectoryNotFoundMessage);

                    exceptionErrorWindow.ShowDialog();

                    return;
                }
                catch (IOException)
                {
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, path, UserControls.Resources.Exceptions.IOMessage);

                    exceptionErrorWindow.ShowDialog();

                    return;
                }
                catch (SecurityException)
                {
                    ErrorWindow exceptionErrorWindow = new ErrorWindow(this, path, UserControls.Resources.Exceptions.SecurityMessage);

                    exceptionErrorWindow.ShowDialog();

                    return;
                }

                this.browserSettings.Path = path;

                this.ListFileSystem();
            }

            this.GeneratePath();
        }
    }
}