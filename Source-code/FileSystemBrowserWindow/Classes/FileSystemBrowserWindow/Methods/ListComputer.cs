// <author>Stefán Örvar Sigmundsson</author>
// <copyright company="eMedia Intellect℠" file="ListComputer.cs">
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
    using System.IO;
    using System.Windows;

    /// <content>Contains the <see cref="ListComputer"/> method.</content>
    public partial class FileSystemBrowserWindow
    {
        /// <summary>Lists the drives of the computer.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "The exception being thrown is unimportant.")]
        private void ListComputer()
        {
            this.computerListView.Items.Clear();

            foreach (DriveInfo currentDriveInfo in DriveInfo.GetDrives())
            {
                ComputerItem currentComputerItem = new ComputerItem() { ByteMultiple = this.browserSettings.ByteMultiple };

                currentComputerItem.Name = currentDriveInfo.Name;

                if (this.browserSettings.HasVolumeLabel)
                {
                    try
                    {
                        currentComputerItem.VolumeLabel = currentDriveInfo.VolumeLabel;
                    }
                    catch
                    {
                    }
                }

                if (this.browserSettings.HasRootDirectory)
                {
                    DirectoryInfo currentDirectoryInfo = currentDriveInfo.RootDirectory;

                    currentComputerItem.RootDirectory = currentDirectoryInfo.Name;
                }

                if (this.browserSettings.HasDriveType)
                {
                    switch (currentDriveInfo.DriveType)
                    {
                        case DriveType.CDRom:
                            currentComputerItem.DriveType = UserControls.Resources.DriveTypeEnumerations.CDRom;

                            break;
                        case DriveType.Fixed:
                            currentComputerItem.DriveType = UserControls.Resources.DriveTypeEnumerations.Fixed;

                            break;
                        case DriveType.Network:
                            currentComputerItem.DriveType = UserControls.Resources.DriveTypeEnumerations.Network;

                            break;
                        case DriveType.NoRootDirectory:
                            currentComputerItem.DriveType = UserControls.Resources.DriveTypeEnumerations.NoRootDirectory;

                            break;
                        case DriveType.Ram:
                            currentComputerItem.DriveType = UserControls.Resources.DriveTypeEnumerations.Ram;

                            break;
                        case DriveType.Removable:
                            currentComputerItem.DriveType = UserControls.Resources.DriveTypeEnumerations.Removable;

                            break;
                        case DriveType.Unknown:
                            currentComputerItem.DriveType = UserControls.Resources.DriveTypeEnumerations.Unknown;

                            break;
                        default:
                            currentComputerItem.DriveType = currentDriveInfo.DriveType.ToString();

                            break;
                    }
                }

                if (this.browserSettings.HasDriveFormat)
                {
                    try
                    {
                        currentComputerItem.DriveFormat = currentDriveInfo.DriveFormat;
                    }
                    catch
                    {
                    }
                }

                if (this.browserSettings.HasAvailableFreeSpace)
                {
                    try
                    {
                        currentComputerItem.AvailableFreeSpaceLong = currentDriveInfo.AvailableFreeSpace;
                    }
                    catch
                    {
                    }
                }

                if (this.browserSettings.HasTotalFreeSpace)
                {
                    try
                    {
                        currentComputerItem.TotalFreeSpaceLong = currentDriveInfo.TotalFreeSpace;
                    }
                    catch
                    {
                    }
                }

                if (this.browserSettings.HasTotalSize)
                {
                    try
                    {
                        currentComputerItem.TotalSizeLong = currentDriveInfo.TotalSize;
                    }
                    catch
                    {
                    }
                }

                this.computerListView.Items.Add(currentComputerItem);
            }

            this.fileSystemListView.Visibility = Visibility.Collapsed;
            this.computerListView.Visibility = Visibility.Visible;
        }
    }
}