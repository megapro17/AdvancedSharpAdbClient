// <copyright file="DeviceDataEventArgs.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// The event arguments that are passed when a device event occurs.
    /// </summary>
    public interface IDeviceDataEventArgs
    {
        /// <summary>
        /// Gets the device where the change occurred.
        /// </summary>
        /// <value>The device where the change occurred.</value>
        DeviceData Device { get; }
    }

    /// <summary>
    /// The event arguments that are passed when a device event occurs.
    /// </summary>
    public sealed class DeviceDataNotifyEventArgs
    {
        internal readonly AdvancedSharpAdbClient.DeviceDataNotifyEventArgs deviceDataNotifyEventArgs;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceDataNotifyEventArgs"/> class.
        /// </summary>
        /// <param name="devices">The list of device.</param>
        public DeviceDataNotifyEventArgs(IEnumerable<DeviceData> devices) => deviceDataNotifyEventArgs = new(devices.Select((x) => x.deviceData));

        internal DeviceDataNotifyEventArgs(AdvancedSharpAdbClient.DeviceDataNotifyEventArgs deviceDataNotifyEvent) => this.deviceDataNotifyEventArgs = deviceDataNotifyEvent;

        internal static DeviceDataNotifyEventArgs GetDeviceDataNotifyEventArgs(AdvancedSharpAdbClient.DeviceDataNotifyEventArgs deviceDataNotifyEvent) => new(deviceDataNotifyEvent);

        /// <summary>
        /// Gets the list of device where the change occurred.
        /// </summary>
        /// <value>The list of device where the change occurred.</value>
        public IEnumerable<DeviceData> Devices => deviceDataNotifyEventArgs.Devices.Select(DeviceData.GetDeviceData);
    }

    /// <summary>
    /// The event arguments that are passed when a device event occurs.
    /// </summary>
    public sealed class DeviceDataConnectEventArgs : IDeviceDataEventArgs
    {
        internal readonly AdvancedSharpAdbClient.DeviceDataConnectEventArgs deviceDataConnectEventArgs;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceDataConnectEventArgs"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="isConnect">The device after the reported change.</param>
        public DeviceDataConnectEventArgs(DeviceData device, bool isConnect) => deviceDataConnectEventArgs = new(device.deviceData, isConnect);

        internal DeviceDataConnectEventArgs(AdvancedSharpAdbClient.DeviceDataConnectEventArgs deviceDataConnectEventArgs) => this.deviceDataConnectEventArgs = deviceDataConnectEventArgs;

        internal static DeviceDataConnectEventArgs GetDeviceDataConnectEventArgs(AdvancedSharpAdbClient.DeviceDataConnectEventArgs deviceDataConnectEventArgs) => new(deviceDataConnectEventArgs);

        /// <summary>
        /// Gets the device where the change occurred.
        /// </summary>
        /// <value>The device where the change occurred.</value>
        public DeviceData Device => DeviceData.GetDeviceData(deviceDataConnectEventArgs.Device);

        /// <summary>
        /// Gets the connect state of the device after the reported change.
        /// </summary>
        public bool IsConnect => deviceDataConnectEventArgs.IsConnect;
    }

    /// <summary>
    /// The event arguments that are passed when a device event occurs.
    /// </summary>
    public sealed class DeviceDataChangeEventArgs : IDeviceDataEventArgs
    {
        internal readonly AdvancedSharpAdbClient.DeviceDataChangeEventArgs deviceDataChangeEventArgs;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceDataChangeEventArgs"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="newState">The state of the device after the reported change.</param>
        /// <param name="oldState">The state of the device before the reported change.</param>
        public DeviceDataChangeEventArgs(DeviceData device, DeviceState newState, DeviceState oldState) => deviceDataChangeEventArgs = new(device.deviceData, (AdvancedSharpAdbClient.DeviceState)newState, (AdvancedSharpAdbClient.DeviceState)oldState);

        internal DeviceDataChangeEventArgs(AdvancedSharpAdbClient.DeviceDataChangeEventArgs deviceDataChangeEventArgs) => this.deviceDataChangeEventArgs = deviceDataChangeEventArgs;

        internal static DeviceDataChangeEventArgs GetDeviceDataChangeEventArgs(AdvancedSharpAdbClient.DeviceDataChangeEventArgs deviceDataChangeEventArgs) => new(deviceDataChangeEventArgs);

        /// <summary>
        /// Gets the device where the change occurred.
        /// </summary>
        /// <value>The device where the change occurred.</value>
        public DeviceData Device => DeviceData.GetDeviceData(deviceDataChangeEventArgs.Device);

        /// <summary>
        /// Gets the state of the device after the reported change.
        /// </summary>
        public DeviceState NewState => (DeviceState)deviceDataChangeEventArgs.NewState;

        /// <summary>
        /// Gets the state of the device before the reported change.
        /// </summary>
        public DeviceState OldState => (DeviceState)deviceDataChangeEventArgs.NewState;
    }
}
