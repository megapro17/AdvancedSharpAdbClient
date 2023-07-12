// <copyright file="IDeviceMonitorAsync.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using Windows.Foundation;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Provides a common interface for any class that allows you to monitor the list of devices that are currently connected to the adb server.
    /// </summary>
    public interface IDeviceMonitorAsync
    {
        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        IAsyncAction StartAsync();
    }
}
