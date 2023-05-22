// <copyright file="PackageManager.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace AdvancedSharpAdbClient.WinRT.DeviceCommands
{
    /// <summary>
    /// Allows you to get information about packages that are installed on a device.
    /// </summary>
    public sealed class PackageManager
    {
        internal readonly AdvancedSharpAdbClient.DeviceCommands.PackageManager packageManager;

        /// <summary>
        /// The path to a temporary directory to use when pushing files to the device.
        /// </summary>
        public static string TempInstallationDirectory { get; } = "/data/local/tmp/";

        /// <summary>
        /// Occurs when there is a change in the status of the installing.
        /// </summary>
        public event TypedEventHandler<object, double> InstallProgressChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageManager"/> class.
        /// </summary>
        /// <param name="client">The <see cref="IAdbClient"/> to use to communicate with the Android Debug Bridge.</param>
        /// <param name="device">The device on which to look for packages.</param>
        public PackageManager(AdbClient client, DeviceData device)
        {
            packageManager = new(client.adbClient, device.deviceData);
            packageManager.InstallProgressChanged += (s, e) => InstallProgressChanged?.Invoke(s, e);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageManager"/> class.
        /// </summary>
        /// <param name="client">The <see cref="IAdbClient"/> to use to communicate with the Android Debug Bridge.</param>
        /// <param name="device">The device on which to look for packages.</param>
        /// <param name="thirdPartyOnly"><see langword="true"/> to only indicate third party applications;
        /// <see langword="false"/> to also include built-in applications.</param>
        public PackageManager(AdbClient client, DeviceData device, bool thirdPartyOnly)
        {
            packageManager = new(client.adbClient, device.deviceData, thirdPartyOnly);
            packageManager.InstallProgressChanged += (s, e) => InstallProgressChanged?.Invoke(s, e);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageManager"/> class.
        /// </summary>
        /// <param name="client">The <see cref="IAdbClient"/> to use to communicate with the Android Debug Bridge.</param>
        /// <param name="device">The device on which to look for packages.</param>
        /// <param name="thirdPartyOnly"><see langword="true"/> to only indicate third party applications;
        /// <see langword="false"/> to also include built-in applications.</param>
        /// <param name="skipInit">A value indicating whether to skip the initial refresh of the package list or not.
        /// Used mainly by unit tests.</param>
        public PackageManager(AdbClient client, DeviceData device, bool thirdPartyOnly, bool skipInit)
        {
            packageManager = new(client.adbClient, device.deviceData, thirdPartyOnly, skipInit: skipInit);
            packageManager.InstallProgressChanged += (s, e) => InstallProgressChanged?.Invoke(s, e);
        }

        /// <summary>
        /// Gets a value indicating whether this package manager only lists third party applications,
        /// or also includes built-in applications.
        /// </summary>
        public bool ThirdPartyOnly => packageManager.ThirdPartyOnly;

        /// <summary>
        /// Gets the list of packages currently installed on the device. They key is the name of the package;
        /// the value the package path.
        /// </summary>
        public IDictionary<string, string> Packages => packageManager.Packages;

        /// <summary>
        /// Gets the device.
        /// </summary>
        public DeviceData Device => DeviceData.GetDeviceData(packageManager.Device);

        /// <summary>
        /// Refreshes the packages.
        /// </summary>
        public void RefreshPackages() => packageManager.RefreshPackages();

        /// <summary>
        /// Installs an Android application on device.
        /// </summary>
        /// <param name="packageFilePath">The absolute file system path to file on local host to install.</param>
        /// <param name="reinstall"><see langword="true"/> if re-install of app should be performed; otherwise, <see langword="false"/>.</param>
        public void InstallPackage(string packageFilePath, bool reinstall) => packageManager.InstallPackage(packageFilePath, reinstall);

        /// <summary>
        /// Installs the application package that was pushed to a temporary location on the device.
        /// </summary>
        /// <param name="remoteFilePath">absolute file path to package file on device.</param>
        /// <param name="reinstall">Set to <see langword="true"/> if re-install of app should be performed.</param>
        public void InstallRemotePackage(string remoteFilePath, bool reinstall) => packageManager.InstallRemotePackage(remoteFilePath, reinstall);

        /// <summary>
        /// Installs Android multiple application on device.
        /// </summary>
        /// <param name="basePackageFilePath">The absolute base app file system path to file on local host to install.</param>
        /// <param name="splitPackageFilePaths">The absolute split app file system paths to file on local host to install.</param>
        /// <param name="reinstall">Set to <see langword="true"/> if re-install of app should be performed.</param>
        [DefaultOverload]
        public void InstallMultiplePackage(string basePackageFilePath, IEnumerable<string> splitPackageFilePaths, bool reinstall) => packageManager.InstallMultiplePackage(basePackageFilePath, splitPackageFilePaths.ToArray(), reinstall);

        /// <summary>
        /// Installs Android multiple application on device.
        /// </summary>
        /// <param name="splitPackageFilePaths">The absolute split app file system paths to file on local host to install.</param>
        /// <param name="packageName">The absolute package name of the base app.</param>
        /// <param name="reinstall">Set to <see langword="true"/> if re-install of app should be performed.</param>
        public void InstallMultiplePackage(IEnumerable<string> splitPackageFilePaths, string packageName, bool reinstall) => packageManager.InstallMultiplePackage(splitPackageFilePaths.ToArray(), packageName, reinstall);

        /// <summary>
        /// Installs the multiple application package that was pushed to a temporary location on the device.
        /// </summary>
        /// <param name="baseRemoteFilePath">The absolute base app file path to package file on device.</param>
        /// <param name="splitRemoteFilePaths">The absolute split app file paths to package file on device.</param>
        /// <param name="reinstall">Set to <see langword="true"/> if re-install of app should be performed.</param>
        [DefaultOverload]
        public void InstallMultipleRemotePackage(string baseRemoteFilePath, IEnumerable<string> splitRemoteFilePaths, bool reinstall) => packageManager.InstallMultipleRemotePackage(baseRemoteFilePath, splitRemoteFilePaths.ToArray(), reinstall);

        /// <summary>
        /// Installs the multiple application package that was pushed to a temporary location on the device.
        /// </summary>
        /// <param name="splitRemoteFilePaths">The absolute split app file paths to package file on device.</param>
        /// <param name="packageName">The absolute package name of the base app.</param>
        /// <param name="reinstall">Set to <see langword="true"/> if re-install of app should be performed.</param>
        public void InstallMultipleRemotePackage(IEnumerable<string> splitRemoteFilePaths, string packageName, bool reinstall) => packageManager.InstallMultipleRemotePackage(splitRemoteFilePaths.ToArray(), packageName, reinstall);

        /// <summary>
        /// Uninstalls a package from the device.
        /// </summary>
        /// <param name="packageName">The name of the package to uninstall.</param>
        public void UninstallPackage(string packageName) => packageManager.UninstallPackage(packageName);

        /// <summary>
        /// Requests the version information from the device.
        /// </summary>
        /// <param name="packageName">The name of the package from which to get the application version.</param>
        public VersionInfo GetVersionInfo(string packageName) => VersionInfo.GetVersionInfo(packageManager.GetVersionInfo(packageName));
    }
}
