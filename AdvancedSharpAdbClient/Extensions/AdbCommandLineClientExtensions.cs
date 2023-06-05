﻿// <copyright file="AdbCommandLineClientExtensions.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using AdvancedSharpAdbClient.Exceptions;
using System.IO;

namespace AdvancedSharpAdbClient
{
    /// <summary>
    /// Provides extension methods for the <see cref="IAdbCommandLineClient"/> class.
    /// </summary>
    public static class AdbCommandLineClientExtensions
    {
        /// <summary>
        /// Throws an error if the path does not point to a valid instance of <c>adb.exe</c>.
        /// </summary>
        /// <param name="client">An instance of a class that implements the <see cref="IAdbCommandLineClient"/> interface.</param>
        /// <param name="adbPath">The path to validate.</param>
        public static void EnsureIsValidAdbFile(this IAdbCommandLineClient client, string adbPath)
        {
            ExceptionExtensions.ThrowIfNull(client);

            if (!client.IsValidAdbFile(adbPath))
            {
                throw new FileNotFoundException($"The adb.exe executable could not be found at {adbPath}");
            }
        }
    }
}
