// <copyright file="InstallProgressEventArgs.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;

namespace AdvancedSharpAdbClient.WinRT.DeviceCommands
{
    /// <summary>
    /// Represents the state of the installation for <see cref="PackageManager.InstallProgressChanged"/>.
    /// </summary>
    public sealed class InstallProgressEventArgs
    {
        internal readonly AdvancedSharpAdbClient.DeviceCommands.InstallProgressEventArgs installProgressEventArgs;

        /// <summary>
        /// State of the installation.
        /// </summary>
        public PackageInstallProgressState State => (PackageInstallProgressState)installProgressEventArgs.State;

        /// <summary>
        /// Number of packages which is finished operation.
        /// Used only in <see cref="PackageInstallProgressState.Uploading"/>,
        /// <see cref="PackageInstallProgressState.WriteSession"/> and
        /// <see cref="PackageInstallProgressState.PostInstall"/> state.
        /// </summary>
        public int PackageFinished => installProgressEventArgs.PackageFinished;

        /// <summary>
        /// Number of packages required for this operation.
        /// Used only in <see cref="PackageInstallProgressState.Uploading"/>,
        /// <see cref="PackageInstallProgressState.WriteSession"/> and
        /// <see cref="PackageInstallProgressState.PostInstall"/> state.
        /// </summary>
        public int PackageRequired => installProgressEventArgs.PackageRequired;

        /// <summary>
        /// Upload percentage completed.
        /// Used only in <see cref="PackageInstallProgressState.Uploading"/> state.
        /// </summary>
        public double UploadProgress => installProgressEventArgs.UploadProgress;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallProgressEventArgs"/> class.
        /// </summary>
        public InstallProgressEventArgs(PackageInstallProgressState state) =>
            installProgressEventArgs = new((AdvancedSharpAdbClient.DeviceCommands.PackageInstallProgressState)state);

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallProgressEventArgs"/> class.
        /// Which is used for <see cref="PackageInstallProgressState.Uploading"/> state.
        /// </summary>
        public InstallProgressEventArgs(int packageUploaded, int packageRequired, double uploadProgress) =>
            installProgressEventArgs = new(packageUploaded, packageRequired, uploadProgress);

        internal InstallProgressEventArgs(AdvancedSharpAdbClient.DeviceCommands.InstallProgressEventArgs installProgressEventArgs) => this.installProgressEventArgs = installProgressEventArgs;

        internal static InstallProgressEventArgs GetInstallProgressEventArgs(AdvancedSharpAdbClient.DeviceCommands.InstallProgressEventArgs installProgressEventArgs) => new(installProgressEventArgs);
    }
}
