// <copyright file="IAdbClientAsync.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Windows.Foundation;
using TimeSpan = System.TimeSpan;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// A common interface for any class that allows you to interact with the
    /// adb server and devices that are connected to that adb server.
    /// </summary>
    public interface IAdbClientAsync
    {
        /// <summary>
        /// Ask the ADB server for its internal version number.
        /// </summary>
        /// <returns>An <see cref="IAsyncOperation{TResult}"/> which return the ADB version number.</returns>
        IAsyncOperation<int> GetAdbVersionAsync();

        /// <summary>
        /// Ask the ADB server for its internal version number.
        /// </summary>
        /// <param name="timeout">A <see cref="TimeSpan"/> which can be used to cancel the asynchronous operation.</param>
        /// <returns>An <see cref="IAsyncOperation{TResult}"/> which return the ADB version number.</returns>
        IAsyncOperation<int> GetAdbVersionAsync(TimeSpan timeout);

        /// <summary>
        /// Ask the ADB server to quit immediately. This is used when the
        /// ADB client detects that an obsolete server is running after an
        /// upgrade.
        /// </summary>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        IAsyncAction KillAdbAsync();

        /// <summary>
        /// Ask the ADB server to quit immediately. This is used when the
        /// ADB client detects that an obsolete server is running after an
        /// upgrade.
        /// </summary>
        /// <param name="timeout">A <see cref="TimeSpan"/> which can be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        IAsyncAction KillAdbAsync(TimeSpan timeout);

        /// <summary>
        /// Gets the devices that are available for communication.
        /// </summary>
        /// <returns>An <see cref="IAsyncOperation{TResult}"/> which return the list of devices that are connected.</returns>
        IAsyncOperation<IEnumerable<DeviceData>> GetDevicesAsync();

        /// <summary>
        /// Gets the devices that are available for communication.
        /// </summary>
        /// <param name="timeout">A <see cref="TimeSpan"/> which can be used to cancel the asynchronous operation.</param>
        /// <returns>An <see cref="IAsyncOperation{TResult}"/> which return the list of devices that are connected.</returns>
        IAsyncOperation<IEnumerable<DeviceData>> GetDevicesAsync(TimeSpan timeout);

        /// <summary>
        /// Executes a command on the device.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="device">The device on which to run the command.</param>
        /// <param name="receiver">The receiver which will get the command output.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        IAsyncAction ExecuteRemoteCommandAsync(string command, DeviceData device, IShellOutputReceiver receiver);

        /// <summary>
        /// Executes a command on the device.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="device">The device on which to run the command.</param>
        /// <param name="receiver">The receiver which will get the command output.</param>
        /// <param name="timeout">A <see cref="TimeSpan"/> which can be used to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        IAsyncAction ExecuteRemoteCommandAsync(string command, DeviceData device, IShellOutputReceiver receiver, TimeSpan timeout);

    }
}
