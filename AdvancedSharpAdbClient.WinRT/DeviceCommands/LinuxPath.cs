// <copyright file="LinuxPath.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace AdvancedSharpAdbClient.WinRT.DeviceCommands
{
    /// <summary>
    /// Just like <see cref="Path"/>, except it is geared for Linux.
    /// </summary>
    public static class LinuxPath
    {
        /// <summary>
        /// The directory separator character.
        /// </summary>
        public static char DirectorySeparatorChar { get; } = AdvancedSharpAdbClient.DeviceCommands.LinuxPath.DirectorySeparatorChar;

        /// <summary>
        /// Combine the specified paths to form one path.
        /// </summary>
        /// <param name="paths">The paths.</param>
        /// <returns>The combined path.</returns>
        public static string Combine([ReadOnlyArray] params string[] paths)=> AdvancedSharpAdbClient.DeviceCommands.LinuxPath.Combine(paths);

        /// <summary>
        /// Returns the directory information for the specified path string.
        /// </summary>
        /// <returns>A <see cref="string"></see> containing directory information for path,
        /// or null if path denotes a root directory, is the empty string (""), or is null.
        /// Returns <see cref="string.Empty"></see> if path does not contain directory information.</returns>
        /// <param name="path">The path of a file or directory. </param>
        /// <exception cref="ArgumentException">The path parameter contains invalid characters, is empty,
        /// or contains only white spaces, or contains a wildcard character. </exception>
        /// <exception cref="PathTooLongException">The path parameter is longer
        /// than the system-defined maximum length.</exception>
        /// <filterpriority>1</filterpriority>
        public static string GetDirectoryName(string path) => AdvancedSharpAdbClient.DeviceCommands.LinuxPath.GetDirectoryName(path);

        /// <summary>
        /// Returns the file name and extension of the specified path string.
        /// </summary>
        /// <returns>A <see cref="string"/> consisting of the characters after the last directory character in path.
        /// If the last character of path is a directory or volume separator character,
        /// this method returns <see cref="string.Empty"/>. If path is null, this method returns null.</returns>
        /// <param name="path">The path string from which to obtain the file name and extension. </param>
        /// <exception cref="ArgumentException">path contains one or more of the invalid characters
        /// defined in <see langword="InvalidCharacters"/>, or contains a wildcard character. </exception>
        /// <filterpriority>1</filterpriority>
        public static string GetFileName(string path) => AdvancedSharpAdbClient.DeviceCommands.LinuxPath.GetFileName(path);

        /// <summary>
        /// Gets a value indicating whether the specified path string contains absolute or relative path information.
        /// </summary>
        /// <returns>true if path contains an absolute path; otherwise, false.</returns>
        /// <param name="path">The path to test.</param>
        /// <exception cref="ArgumentException">path contains one or more of the invalid characters
        /// defined in <see langword="InvalidCharacters"/>, or contains a wildcard character. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool IsPathRooted(string path) => AdvancedSharpAdbClient.DeviceCommands.LinuxPath.IsPathRooted(path);

        /// <summary>
        /// Returns an escaped version of the entry name.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The entry name.</returns>
        public static string Escape(string path) => AdvancedSharpAdbClient.DeviceCommands.LinuxPath.Escape(path);

        /// <summary>
        /// Quotes the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The quoted path.</returns>
        public static string Quote(string path) => AdvancedSharpAdbClient.DeviceCommands.LinuxPath.Quote(path);
    }
}
