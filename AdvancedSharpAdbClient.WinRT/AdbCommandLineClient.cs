// <copyright file="AdbCommandLineClient.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using AdvancedSharpAdbClient.WinRT.Extensions;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Provides methods for interacting with the <c>adb.exe</c> command line client.
    /// </summary>
    public sealed class AdbCommandLineClient : IAdbCommandLineClient, IAdbCommandLineClientAsync
    {
        internal readonly AdvancedSharpAdbClient.AdbCommandLineClient adbCommandLineClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdbCommandLineClient"/> class.
        /// </summary>
        /// <param name="adbPath">The path to the <c>adb.exe</c> executable.</param>
        public AdbCommandLineClient(string adbPath) => adbCommandLineClient = new(adbPath);

        /// <summary>
        /// Initializes a new instance of the <see cref="AdbCommandLineClient"/> class.
        /// </summary>
        /// <param name="adbPath">The path to the <c>adb.exe</c> executable.</param>
        /// <param name="isForce">Don't check adb file name when <see langword="true"/>.</param>
        public AdbCommandLineClient(string adbPath, bool isForce) => adbCommandLineClient = new(adbPath, isForce);

        /// <summary>
        /// Gets the path to the <c>adb.exe</c> executable.
        /// </summary>
        public string AdbPath => adbCommandLineClient.AdbPath;

        /// <summary>
        /// Queries adb for its version number and checks it against <see cref="AdbServer.RequiredAdbVersion"/>.
        /// </summary>
        /// <returns>A <see cref="PackageVersion"/> object that contains the version number of the Android Command Line client.</returns>
        public PackageVersion GetVersion() => adbCommandLineClient.GetVersion().GetPackageVersion();

        /// <summary>
        /// Queries adb for its version number and checks it against <see cref="AdbServer.RequiredAdbVersion"/>.
        /// </summary>
        /// <returns>A <see cref="IAsyncOperation{PackageVersion}"/> which return a <see cref="PackageVersion"/> object that contains the version number of the Android Command Line client.</returns>
        public IAsyncOperation<PackageVersion> GetVersionAsync() => AsyncInfo.Run(async (cancellationToken) => (await adbCommandLineClient.GetVersionAsync(cancellationToken)).GetPackageVersion());

        /// <summary>
        /// Starts the adb server by running the <c>adb start-server</c> command.
        /// </summary>
        public void StartServer() => adbCommandLineClient.StartServer();

        /// <summary>
        /// Starts the adb server by running the <c>adb start-server</c> command.
        /// </summary>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        public IAsyncAction StartServerAsync() => AsyncInfo.Run(adbCommandLineClient.StartServerAsync);

        /// <inheritdoc/>
        public bool IsValidAdbFile(string adbPath) => adbCommandLineClient.IsValidAdbFile(adbPath);
    }
}
