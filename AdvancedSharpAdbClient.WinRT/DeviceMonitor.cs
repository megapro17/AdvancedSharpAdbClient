// <copyright file="DeviceMonitor.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// A Device monitor. This connects to the Android Debug Bridge and get device and
    /// debuggable process information from it.
    /// </summary>
    /// <example>
    /// <para>To receive notifications when devices connect to or disconnect from your PC, you can use the following code:</para>
    /// <code>
    /// void Test()
    /// {
    ///     var monitor = new DeviceMonitor(new AdbSocket());
    ///     monitor.DeviceConnected += this.OnDeviceConnected;
    ///     monitor.Start();
    /// }
    ///
    /// void OnDeviceConnected(object sender, DeviceDataEventArgs e)
    /// {
    ///     Console.WriteLine($"The device {e.Device.Name} has connected to this PC");
    /// }
    /// </code>
    /// </example>
    public sealed class DeviceMonitor : IDeviceMonitor, IDeviceMonitorAsync
    {
        internal readonly AdvancedSharpAdbClient.DeviceMonitor deviceMonitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceMonitor"/> class.
        /// </summary>
        /// <param name="socket">The <see cref="IAdbSocket"/> that manages the connection with the adb server.</param>
        public DeviceMonitor(AdbSocket socket)
        {
            Socket = socket;
            deviceMonitor = new(socket.adbSocket);
            deviceMonitor.DeviceChanged += (sender, args) => DeviceChanged?.Invoke(sender, DeviceDataChangeEventArgs.GetDeviceDataChangeEventArgs(args));
            deviceMonitor.DeviceNotified += (sender, args) => DeviceNotified?.Invoke(sender, DeviceDataNotifyEventArgs.GetDeviceDataNotifyEventArgs(args));
            deviceMonitor.DeviceConnected += (sender, args) => DeviceConnected?.Invoke(sender, DeviceDataConnectEventArgs.GetDeviceDataConnectEventArgs(args));
            deviceMonitor.DeviceDisconnected += (sender, args) => DeviceDisconnected?.Invoke(sender, DeviceDataConnectEventArgs.GetDeviceDataConnectEventArgs(args));
        }

        /// <inheritdoc/>
        public event EventHandler<DeviceDataChangeEventArgs> DeviceChanged;

        /// <inheritdoc/>
        public event EventHandler<DeviceDataNotifyEventArgs> DeviceNotified;

        /// <inheritdoc/>
        public event EventHandler<DeviceDataConnectEventArgs> DeviceConnected;

        /// <inheritdoc/>
        public event EventHandler<DeviceDataConnectEventArgs> DeviceDisconnected;

        /// <inheritdoc/>
        public IEnumerable<DeviceData> Devices => deviceMonitor.Devices.Select(DeviceData.GetDeviceData);

        /// <summary>
        /// Gets the <see cref="AdbSocket"/> that represents the connection to the
        /// Android Debug Bridge.
        /// </summary>
        public AdbSocket Socket { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value><see langword="true"/> if this instance is running; otherwise, <see langword="false"/>.</value>
        public bool IsRunning => deviceMonitor.IsRunning;

        /// <inheritdoc/>
        public void Start() => deviceMonitor.Start();

        /// <inheritdoc/>
        public IAsyncAction StartAsync() => AsyncInfo.Run(deviceMonitor.StartAsync);

        /// <summary>
        /// Stops the monitoring
        /// </summary>
        public void Dispose() => deviceMonitor.Dispose();
    }
}
