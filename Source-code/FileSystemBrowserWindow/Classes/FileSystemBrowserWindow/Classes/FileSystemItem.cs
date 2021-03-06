﻿// <author>Stefán Örvar Sigmundsson</author>
// <copyright company="eMedia Intellect℠" file="FileSystemItem.cs">
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
    using System.Globalization;
    using System.Threading;

    /// <content>Contains the <see cref="FileSystemItem"/> class.</content>
    public partial class FileSystemBrowserWindow
    {
        /// <summary>Provides a file system item for the <see cref="FileSystemBrowserWindow"/> class.</summary>
        internal class FileSystemItem
        {
            /// <summary>The current culture information.</summary>
            private CultureInfo currentCultureInfo = Thread.CurrentThread.CurrentCulture;

            /// <summary>The byte multiple of the <see cref="FileSystemBrowserWindow.ByteConverter"/> class.</summary>
            /// <remarks>The store for the <see cref="ByteMultiple"/> property.</remarks>
            private ByteMultiple byteMultiple = ByteMultiple.Decimal;

            /// <summary>The time of the creation of the file system item.</summary>
            /// <remarks>The store for the <see cref="Created"/> property.</remarks>
            private DateTime creationTime;

            /// <summary>The time of the last access to the file system item.</summary>
            /// <remarks>The store for the <see cref="Accessed"/> property.</remarks>
            private DateTime lastAccessTime;

            /// <summary>The time of the last write to the file system item.</summary>
            /// <remarks>The store for the <see cref="Written"/> property.</remarks>
            private DateTime lastWriteTime;

            /// <summary>The type of the file system item.</summary>
            /// <remarks>The store for the <see cref="FileSystemItemType"/> property.</remarks>
            private FileSystemItemType fileSystemItemType;

            /// <summary>The available free space on the drive in bytes.</summary>
            /// <remarks>The store for the <see cref="AvailableFreeSpaceLong"/> property.</remarks>
            private long availableFreeSpaceLong;

            /// <summary>The total free space on the drive in bytes.</summary>
            /// <remarks>The store for the <see cref="TotalFreeSpaceLong"/> property.</remarks>
            private long totalFreeSpaceLong;

            /// <summary>The total size of the drive in bytes.</summary>
            /// <remarks>The store for the <see cref="TotalSizeLong"/> property.</remarks>
            private long totalSizeLong;

            /// <summary>The size of the file in bytes.</summary>
            /// <remarks>The store for the <see cref="Length"/> property.</remarks>
            private long length;

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

            /// <summary>Gets or sets a value indicating the type of the file system item.</summary>
            /// <value>Represents the <see cref="fileSystemItemType"/> field.</value>
            public FileSystemItemType FileSystemItemType
            {
                get
                {
                    return this.fileSystemItemType;
                }

                set
                {
                    this.fileSystemItemType = value;
                }
            }

            /// <summary>Sets a value indicating the last access to the file system item.</summary>
            /// <value>Represents the <see cref="lastAccessTime"/> field.</value>
            public DateTime Accessed
            {
                set
                {
                    this.lastAccessTime = value;
                }
            }

            /// <summary>Sets a value indicating the time of the creation of the file system item.</summary>
            /// <value>Represents the <see cref="creationTime"/> field.</value>
            public DateTime Created
            {
                set
                {
                    this.creationTime = value;
                }
            }

            /// <summary>Sets a value indicating the last write to the file system item.</summary>
            /// <value>Represents the <see cref="lastWriteTime"/> field.</value>
            public DateTime Written
            {
                set
                {
                    this.lastWriteTime = value;
                }
            }

            /// <summary>Sets a value indicating the available free space on the drive.</summary>
            /// <value>Represents the <see cref="availableFreeSpaceLong"/> field.</value>
            public long AvailableFreeSpaceLong
            {
                set
                {
                    this.availableFreeSpaceLong = value;
                }
            }

            /// <summary>Sets a value indicating the size of the file.</summary>
            /// <value>Represents the <see cref="length"/> field.</value>
            public long Length
            {
                set
                {
                    this.length = value;
                }
            }

            /// <summary>Sets a value indicating the total free space on the drive.</summary>
            /// <value>Represents the <see cref="totalFreeSpaceLong"/> field.</value>
            public long TotalFreeSpaceLong
            {
                set
                {
                    this.totalFreeSpaceLong = value;
                }
            }

            /// <summary>Sets a value indicating the total size of the drive.</summary>
            /// <value>Represents the <see cref="totalSizeLong"/> field.</value>
            public long TotalSizeLong
            {
                set
                {
                    this.totalSizeLong = value;
                }
            }

            /// <summary>Gets a value indicating the available free space on the drive.</summary>
            /// <value>Represents the <see cref="availableFreeSpaceLong"/> field after being processed by the <see cref="ByteConverter"/> class.</value>
            public string AvailableFreeSpace
            {
                get
                {
                    return ByteConverter.Process(this.availableFreeSpaceLong, this.byteMultiple);
                }
            }

            /// <summary>Gets a value indicating the time of the creation of the file system item.</summary>
            /// <value>Represents the <see cref="creationTime"/> field after being converted to a string.</value>
            public string CreationTime
            {
                get
                {
                    return this.creationTime.ToString(this.currentCultureInfo);
                }
            }

            /// <summary>Gets or sets a value indicating the format of the drive.</summary>
            public string DriveFormat
            {
                get; set;
            }

            /// <summary>Gets or sets a value indicating the type of the drive.</summary>
            public string DriveType
            {
                get; set;
            }

            /// <summary>Gets a value indicating the path to the icon representing the file system item.</summary>
            public string Icon
            {
                get
                {
                    switch (this.fileSystemItemType)
                    {
                        case FileSystemItemType.Directory:
                            return @"..\..\Images\Directory.png";

                        case FileSystemItemType.File:
                            return @"..\..\Images\File.png";

                        default:
                            return string.Empty;
                    }
                }
            }

            /// <summary>Gets a value indicating the last access to the file system item.</summary>
            /// <value>Represents the <see cref="lastAccessTime"/> field after being converted to a string.</value>
            public string LastAccessTime
            {
                get
                {
                    return this.lastAccessTime.ToString(this.currentCultureInfo);
                }
            }

            /// <summary>Gets a value indicating the last write to the file system item.</summary>
            /// <value>Represents the <see cref="lastWriteTime"/> field after being converted to a string.</value>
            public string LastWriteTime
            {
                get
                {
                    return this.lastWriteTime.ToString(this.currentCultureInfo);
                }
            }

            /// <summary>Gets or sets a value indicating the name of the file system item.</summary>
            public string Name
            {
                get; set;
            }

            /// <summary>Gets or sets a value indicating the root directory of the drive.</summary>
            public string RootDirectory
            {
                get; set;
            }

            /// <summary>Gets a value indicating the size of the file.</summary>
            /// <value>Represents the <see cref="length"/> field after being processed by the <see cref="ByteConverter"/> class.</value>
            public string Size
            {
                get
                {
                    if (this.fileSystemItemType == FileSystemItemType.File)
                    {
                        return ByteConverter.Process(this.length, this.byteMultiple);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }

            /// <summary>Gets a value indicating the total free space on the drive.</summary>
            /// <value>Represents the <see cref="totalFreeSpaceLong"/> field after being processed by the <see cref="ByteConverter"/> class.</value>
            public string TotalFreeSpace
            {
                get
                {
                    return ByteConverter.Process(this.totalFreeSpaceLong, this.byteMultiple);
                }
            }

            /// <summary>Gets a value indicating the total size of the drive.</summary>
            /// <value>Represents the <see cref="totalSizeLong"/> field after being processed by the <see cref="ByteConverter"/> class.</value>
            public string TotalSize
            {
                get
                {
                    return ByteConverter.Process(this.totalSizeLong, this.byteMultiple);
                }
            }

            /// <summary>Gets or sets a value indicating the volume label of the drive.</summary>
            public string VolumeLabel
            {
                get; set;
            }
        }
    }
}