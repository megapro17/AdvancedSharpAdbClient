// <copyright file="IAdbCommandLineClientAsync.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using Windows.ApplicationModel;
using Windows.Foundation;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Provides a common interface for any class that provides access to the <c>adb.exe</c> executable.
    /// </summary>
    public interface IAdbCommandLineClientAsync
    {
        /// <summary>
        /// Queries adb for its version number and checks it against <see cref="AdbServer.RequiredAdbVersion"/>.
        /// </summary>
        /// <returns>A <see cref="IAsyncOperation{PackageVersion}"/> which return a <see cref="PackageVersion"/> object that contains the version number of the Android Command Line client.</returns>
        IAsyncOperation<PackageVersion> GetVersionAsync();

        /// <summary>
        /// Starts the adb server by running the <c>adb start-server</c> command.
        /// </summary>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        IAsyncAction StartServerAsync();
    }
}
