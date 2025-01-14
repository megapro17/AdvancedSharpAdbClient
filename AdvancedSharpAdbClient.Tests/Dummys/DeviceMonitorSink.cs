﻿using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace AdvancedSharpAdbClient.Tests
{
    internal class DeviceMonitorSink
    {
        public DeviceMonitorSink(DeviceMonitor monitor)
        {
            Monitor = monitor ?? throw new ArgumentNullException(nameof(monitor));
            Monitor.DeviceChanged += OnDeviceChanged;
            Monitor.DeviceNotified += OnDeviceNotified;
            Monitor.DeviceConnected += OnDeviceConnected;
            Monitor.DeviceDisconnected += OnDeviceDisconnected;

            ChangedEvents = new Collection<DeviceDataChangeEventArgs>();
            NotifiedEvents = new Collection<DeviceDataNotifyEventArgs>();
            ConnectedEvents = new Collection<DeviceDataConnectEventArgs>();
            DisconnectedEvents = new Collection<DeviceDataConnectEventArgs>();
        }

        public void ResetSignals()
        {
            ChangedEvents.Clear();
            NotifiedEvents.Clear();
            ConnectedEvents.Clear();
            DisconnectedEvents.Clear();
        }

        public Collection<DeviceDataConnectEventArgs> DisconnectedEvents { get; private set; }

        public Collection<DeviceDataConnectEventArgs> ConnectedEvents { get; private set; }

        public Collection<DeviceDataNotifyEventArgs> NotifiedEvents { get; private set; }

        public Collection<DeviceDataChangeEventArgs> ChangedEvents { get; private set; }

        public DeviceMonitor Monitor { get; private set; }

        public ManualResetEvent CreateEventSignal()
        {
            ManualResetEvent signal = new(false);
            Monitor.DeviceNotified += (sender, e) => signal.Set();
            Monitor.DeviceDisconnected += (sender, e) => signal.Set();
            return signal;
        }

        protected virtual void OnDeviceDisconnected(object sender, DeviceDataConnectEventArgs e) => DisconnectedEvents.Add(e);

        protected virtual void OnDeviceConnected(object sender, DeviceDataConnectEventArgs e) => ConnectedEvents.Add(e);

        protected virtual void OnDeviceNotified(object sender, DeviceDataNotifyEventArgs e) => NotifiedEvents.Add(e);

        protected virtual void OnDeviceChanged(object sender, DeviceDataChangeEventArgs e) => ChangedEvents.Add(e);
    }
}
