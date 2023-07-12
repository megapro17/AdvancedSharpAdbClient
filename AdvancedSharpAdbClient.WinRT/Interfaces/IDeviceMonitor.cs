// <copyright file="IDeviceMonitor.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Provides a common interface for any class that allows you to monitor the list of devices that are currently connected to the adb server.
    /// </summary>
    public interface IDeviceMonitor : IDisposable
    {
        /// <summary>
        /// Occurs when the status of one of the connected devices has changed.
        /// </summary>
        event EventHandler<DeviceDataChangeEventArgs> DeviceChanged;

        /// <summary>
        /// Occurs when received a list of device from the Android Debug Bridge.
        /// </summary>
        event EventHandler<DeviceDataNotifyEventArgs> DeviceNotified;

        /// <summary>
        /// Occurs when a device has connected to the Android Debug Bridge.
        /// </summary>
        event EventHandler<DeviceDataConnectEventArgs> DeviceConnected;

        /// <summary>
        /// Occurs when a device has disconnected from the Android Debug Bridge.
        /// </summary>
        event EventHandler<DeviceDataConnectEventArgs> DeviceDisconnected;

        /// <summary>
        /// Gets the devices that are currently connected to the Android Debug Bridge.
        /// </summary>
        IEnumerable<DeviceData> Devices { get; }

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        void Start();
    }
}
