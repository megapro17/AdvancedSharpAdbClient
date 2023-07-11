// <copyright file="IDeviceExtensions.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace AdvancedSharpAdbClient.WinRT.DeviceCommands
{
    /// <summary>
    /// Provides extension methods for the <see cref="DeviceData"/> class,
    /// allowing you to run commands directory against a <see cref="DeviceData"/> object.
    /// </summary>
    public interface IDeviceExtensions
    {
        /// <summary>
        /// Executes a shell command on the device.
        /// </summary>
        /// <param name="device">The device on which to run the command.</param>
        /// <param name="command">The command to execute.</param>
        /// <param name="receiver">Optionally, a <see cref="IShellOutputReceiver"/> that processes the command output.</param>
        void ExecuteShellCommand(DeviceData device, string command, IShellOutputReceiver receiver);

        /// <summary>
        /// Gets the property of a device.
        /// </summary>
        /// <param name="device">The device for which to get the property.</param>
        /// <param name="property">The name of property which to get.</param>
        /// <returns>The value of the property on the device.</returns>
        string GetProperty(DeviceData device, string property);

        /// <summary>
        /// Gets the properties of a device.
        /// </summary>
        /// <param name="device">The device for which to list the properties.</param>
        /// <returns>A dictionary containing the properties of the device, and their values.</returns>
        IDictionary<string, string> GetProperties(DeviceData device);

        /// <summary>
        /// Gets the environment variables currently defined on a device.
        /// </summary>
        /// <param name="device">The device for which to list the environment variables.</param>
        /// <returns>A dictionary containing the environment variables of the device, and their values.</returns>
        IDictionary<string, string> GetEnvironmentVariables(DeviceData device);

        /// <summary>
        /// Uninstalls a package from the device.
        /// </summary>
        /// <param name="device">The device on which to uninstall the package.</param>
        /// <param name="packageName">The name of the package to uninstall.</param>
        void UninstallPackage(DeviceData device, string packageName);

        /// <summary>
        /// Requests the version information from the device.
        /// </summary>
        /// <param name="device">The device on which to uninstall the package.</param>
        /// <param name="packageName">The name of the package from which to get the application version.</param>
        VersionInfo GetPackageVersion(DeviceData device, string packageName);

        /// <summary>
        /// Lists all processes running on the device.
        /// </summary>
        /// <param name="device">The device on which to list the processes that are running.</param>
        /// <returns>An <see cref="IEnumerable{AndroidProcess}"/> that will iterate over all processes
        /// that are currently running on the device.</returns>
        IEnumerable<AndroidProcess> ListProcesses(DeviceData device);
    }
}
