// <copyright file="IDeviceExtensionsAsync.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Windows.Foundation;

namespace AdvancedSharpAdbClient.WinRT.DeviceCommands
{
    /// <summary>
    /// Provides extension methods for the <see cref="DeviceData"/> class,
    /// allowing you to run commands directory against a <see cref="DeviceData"/> object.
    /// </summary>
    public interface IDeviceExtensionsAsync
    {
        /// <summary>
        /// Executes a shell command on the device.
        /// </summary>
        /// <param name="device">The device on which to run the command.</param>
        /// <param name="command">The command to execute.</param>
        /// <param name="receiver">Optionally, a <see cref="IShellOutputReceiver"/> that processes the command output.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        IAsyncAction ExecuteShellCommandAsync(DeviceData device, string command, IShellOutputReceiver receiver);

        /// <summary>
        /// Gets the property of a device.
        /// </summary>
        /// <param name="device">The device for which to get the property.</param>
        /// <param name="property">The name of property which to get.</param>
        /// <returns>A <see cref="IAsyncOperation{String}"/> which return the value of the property on the device.</returns>
        IAsyncOperation<string> GetPropertyAsync(DeviceData device, string property);

        /// <summary>
        /// Gets the properties of a device.
        /// </summary>
        /// <param name="device">The device for which to list the properties.</param>
        /// <returns>A <see cref="IAsyncOperation{IDictionary}"/> which return a dictionary containing the properties of the device, and their values.</returns>
        IAsyncOperation<IDictionary<string, string>> GetPropertiesAsync(DeviceData device);

        /// <summary>
        /// Gets the environment variables currently defined on a device.
        /// </summary>
        /// <param name="device">The device for which to list the environment variables.</param>
        /// <returns>A <see cref="IAsyncOperation{IDictionary}"/> which return the a dictionary containing the environment variables of the device, and their values.</returns>
        IAsyncOperation<IDictionary<string, string>> GetEnvironmentVariablesAsync(DeviceData device);

        /// <summary>
        /// Uninstalls a package from the device.
        /// </summary>
        /// <param name="device">The device on which to uninstall the package.</param>
        /// <param name="packageName">The name of the package to uninstall.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        IAsyncAction UninstallPackageAsync(DeviceData device, string packageName);

        /// <summary>
        /// Requests the version information from the device.
        /// </summary>
        /// <param name="device">The device on which to uninstall the package.</param>
        /// <param name="packageName">The name of the package from which to get the application version.</param>
        /// <returns>A <see cref="IAsyncOperation{VersionInfo}"/> which return the <see cref="VersionInfo"/> of target application.</returns>
        IAsyncOperation<VersionInfo> GetPackageVersionAsync(DeviceData device, string packageName);

        /// <summary>
        /// Lists all processes running on the device.
        /// </summary>
        /// <param name="device">The device on which to list the processes that are running.</param>
        /// <returns>A <see cref="IAsyncOperation{IEnumerable}"/> which return the an <see cref="IEnumerable{AndroidProcess}"/> that will iterate over all processes
        /// that are currently running on the device.</returns>
        IAsyncOperation<IEnumerable<AndroidProcess>> ListProcessesAsync(DeviceData device);
    }
}
