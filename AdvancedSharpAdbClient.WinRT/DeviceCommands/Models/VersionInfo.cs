// <copyright file="VersionInfo.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

namespace AdvancedSharpAdbClient.WinRT.DeviceCommands
{
    /// <summary>
    /// Represents a version of an Android application.
    /// </summary>
    public sealed class VersionInfo
    {
        internal readonly AdvancedSharpAdbClient.DeviceCommands.VersionInfo versionInfo;

        /// <summary>
        /// Gets or sets the version code of an Android application.
        /// </summary>
        public int VersionCode => versionInfo.VersionCode;

        /// <summary>
        /// Gets or sets the version name of an Android application.
        /// </summary>
        public string VersionName => versionInfo.VersionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionInfo"/> class.
        /// </summary>
        /// <param name="versionCode">The version code of the application.</param>
        /// <param name="versionName">The version name of the application.</param>
        public VersionInfo(int versionCode, string versionName) => versionInfo = new(versionCode, versionName);

        internal VersionInfo(AdvancedSharpAdbClient.DeviceCommands.VersionInfo versionInfo) => this.versionInfo = versionInfo;

        internal static VersionInfo GetVersionInfo(AdvancedSharpAdbClient.DeviceCommands.VersionInfo versionInfo) => new(versionInfo);

        /// <summary>
        /// Deconstruct the <see cref="VersionInfo"/> class.
        /// </summary>
        /// <param name="versionCode">The version code of the application.</param>
        /// <param name="versionName">The version name of the application.</param>
        public void Deconstruct(out int versionCode, out string versionName)
        {
            versionCode = VersionCode;
            versionName = VersionName;
        }

        /// <inheritdoc/>
        public override string ToString() => $"{VersionName} - {VersionCode}";
    }
}
