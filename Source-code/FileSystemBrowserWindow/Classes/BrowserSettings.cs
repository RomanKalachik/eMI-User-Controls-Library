// <author>Stefán Örvar Sigmundsson</author>
// <copyright company="eMedia Intellect℠" file="BrowserSettings.cs">
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

    /// <summary>Provides settings for the <see cref="FileSystemBrowserWindow"/> class.</summary>
    public class BrowserSettings
    {
        /// <summary>Indicates whether the computer list view has a column for the available free space.</summary>
        /// <remarks>The store for the <see cref="HasAvailableFreeSpace"/> property.</remarks>
        private bool hasAvailableFreeSpace = true;

        /// <summary>Indicates whether the computer list view has a column for the drive type.</summary>
        /// <remarks>The store for the <see cref="HasDriveType"/> property.</remarks>
        private bool hasDriveType = true;

        /// <summary>Indicates whether the computer list view has a column for the drive format.</summary>
        /// <remarks>The store for the <see cref="HasDriveFormat"/> property.</remarks>
        private bool hasDriveFormat = true;

        /// <summary>Indicates whether the computer list view has a column for the root directory.</summary>
        /// <remarks>The store for the <see cref="HasRootDirectory"/> property.</remarks>
        private bool hasRootDirectory = false;

        /// <summary>Indicates whether the computer list view has a column for the total free space.</summary>
        /// <remarks>The store for the <see cref="HasTotalFreeSpace"/> property.</remarks>
        private bool hasTotalFreeSpace = true;

        /// <summary>Indicates whether the computer list view has a column for the total size.</summary>
        /// <remarks>The store for the <see cref="HasTotalSize"/> property.</remarks>
        private bool hasTotalSize = true;

        /// <summary>Indicates whether the computer list view has a column for the volume label.</summary>
        /// <remarks>The store for the <see cref="HasVolumeLabel"/> property.</remarks>
        private bool hasVolumeLabel = false;

        /// <summary>The browsing mode of the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <remarks>The store for the <see cref="BrowsingMode"/> property.</remarks>
        private BrowsingMode browsingMode = BrowsingMode.Select;

        /// <summary>The byte multiple of the <see cref="FileSystemBrowserWindow.ByteConverter"/> class.</summary>
        /// <remarks>The store for the <see cref="ByteMultiple"/> property.</remarks>
        private ByteMultiple byteMultiple = ByteMultiple.Decimal;

        /// <summary>The maximum number of directories to return by the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <remarks>The store for the <see cref="MaximumDirectories"/> property.</remarks>
        private int maximumDirectories = 0;

        /// <summary>The maximum number of files to return by the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <remarks>The store for the <see cref="MaximumFiles"/> property.</remarks>
        private int maximumFiles = 1;

        /// <summary>The minimum number of directories to return by the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <remarks>The store for the <see cref="MinimumDirectories"/> property.</remarks>
        private int minimumDirectories = 0;

        /// <summary>The minimum number of files to return by the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <remarks>The store for the <see cref="MinimumFiles"/> property.</remarks>
        private int minimumFiles = 1;

        /// <summary>The path of the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <remarks>The store for the <see cref="Path"/> property.</remarks>
        private string path = string.Empty;

        /// <summary>Gets or sets a value indicating whether the computer list view has a column for the available free space.</summary>
        /// <value>Represents the <see cref="hasAvailableFreeSpace"/> field.</value>
        public bool HasAvailableFreeSpace
        {
            get
            {
                return this.hasAvailableFreeSpace;
            }

            set
            {
                this.hasAvailableFreeSpace = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether the computer list view has a column for the drive type.</summary>
        /// <value>Represents the <see cref="hasDriveType"/> field.</value>
        public bool HasDriveType
        {
            get
            {
                return this.hasDriveType;
            }

            set
            {
                this.hasDriveType = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether the computer list view has a column for the drive format.</summary>
        /// <value>Represents the <see cref="hasDriveFormat"/> field.</value>
        public bool HasDriveFormat
        {
            get
            {
                return this.hasDriveFormat;
            }

            set
            {
                this.hasDriveFormat = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether the computer list view has a column for the root directory.</summary>
        /// <value>Represents the <see cref="hasRootDirectory"/> field.</value>
        public bool HasRootDirectory
        {
            get
            {
                return this.hasRootDirectory;
            }

            set
            {
                this.hasRootDirectory = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether the computer list view has a column for the total free space.</summary>
        /// <value>Represents the <see cref="hasTotalFreeSpace"/> field.</value>
        public bool HasTotalFreeSpace
        {
            get
            {
                return this.hasTotalFreeSpace;
            }

            set
            {
                this.hasTotalFreeSpace = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether the computer list view has a column for the total size.</summary>
        /// <value>Represents the <see cref="hasTotalSize"/> field.</value>
        public bool HasTotalSize
        {
            get
            {
                return this.hasTotalSize;
            }

            set
            {
                this.hasTotalSize = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether the computer list view has a column for the volume label.</summary>
        /// <value>Represents the <see cref="hasVolumeLabel"/> field.</value>
        public bool HasVolumeLabel
        {
            get
            {
                return this.hasVolumeLabel;
            }

            set
            {
                this.hasVolumeLabel = value;
            }
        }

        /// <summary>Gets or sets a value indicating the browsing mode of the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <value>Represents the <see cref="browsingMode"/> field.</value>
        public BrowsingMode BrowsingMode
        {
            get
            {
                return this.browsingMode;
            }

            set
            {
                this.browsingMode = value;
            }
        }

        /// <summary>Gets or sets a value indicating the byte multiple of the <see cref="FileSystemBrowserWindow.ByteConverter"/> class.</summary>
        /// <value>Represents the <see cref="byteMultiple"/> field.</value>
        public ByteMultiple ByteMultiple
        {
            get
            {
                return this.byteMultiple;
            }

            set
            {
                this.byteMultiple = value;
            }
        }

        /// <summary>Gets or sets a value indicating the maximum number of directories to return by the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <value>Represents the <see cref="maximumDirectories"/> field.</value>
        public int MaximumDirectories
        {
            get
            {
                return this.maximumDirectories;
            }

            set
            {
                this.maximumDirectories = value;
            }
        }

        /// <summary>Gets or sets a value indicating the maximum number of files to return by the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <value>Represents the <see cref="maximumFiles"/> field.</value>
        public int MaximumFiles
        {
            get
            {
                return this.maximumFiles;
            }

            set
            {
                this.maximumFiles = value;
            }
        }

        /// <summary>Gets or sets a value indicating the minimum number of directories to return by the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <value>Represents the <see cref="minimumDirectories"/> field.</value>
        public int MinimumDirectories
        {
            get
            {
                return this.minimumDirectories;
            }

            set
            {
                this.minimumDirectories = value;
            }
        }

        /// <summary>Gets or sets a value indicating the minimum number of files to return by the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <value>Represents the <see cref="minimumFiles"/> field.</value>
        public int MinimumFiles
        {
            get
            {
                return this.minimumFiles;
            }

            set
            {
                this.minimumFiles = value;
            }
        }

        /// <summary>Gets or sets a value indicating the path of the <see cref="FileSystemBrowserWindow"/> class.</summary>
        /// <value>Represents the <see cref="path"/> field.</value>
        public string Path
        {
            get
            {
                return this.path;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.path = value;
            }
        }
    }
}