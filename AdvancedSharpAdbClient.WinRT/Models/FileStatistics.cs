// <copyright file="FileStatistics.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Contains information about a file on the remote device.
    /// </summary>
    public sealed class FileStatistics
    {
        internal AdvancedSharpAdbClient.FileStatistics fileStatistics;

        /// <summary>
        /// Gets or sets the path of the file.
        /// </summary>
        public string Path
        {
            get => fileStatistics.Path;
            set => fileStatistics.Path = value;
        }

        /// <summary>
        /// Gets or sets the <see cref="UnixFileMode"/> attributes of the file.
        /// </summary>
        public UnixFileMode FileMode
        {
            get => (UnixFileMode)fileStatistics.FileMode;
            set => fileStatistics.FileMode = (AdvancedSharpAdbClient.UnixFileMode)value;
        }

        /// <summary>
        /// Gets or sets the total file size, in bytes.
        /// </summary>
        public int Size
        {
            get => fileStatistics.Size;
            set => fileStatistics.Size = value;
        }

        /// <summary>
        /// Gets or sets the time of last modification.
        /// </summary>
        public DateTimeOffset Time
        {
            get => fileStatistics.Time;
            set => fileStatistics.Time = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStatistics"/> class.
        /// </summary>
        public FileStatistics() => fileStatistics = new();

        internal FileStatistics(AdvancedSharpAdbClient.FileStatistics fileStatistics) => this.fileStatistics = fileStatistics;

        internal static FileStatistics GetFileStatistics(AdvancedSharpAdbClient.FileStatistics fileStatistics) => new(fileStatistics);

        /// <summary>
        /// Gets a <see cref="string"/> that represents the current <see cref="FileStatistics"/> object.
        /// </summary>
        /// <returns>The <see cref="Path"/> of the current <see cref="FileStatistics"/> object.</returns>
        public override string ToString() => fileStatistics.ToString();
    }
}
