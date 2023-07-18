// <copyright file="AdbClient.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using AdvancedSharpAdbClient.DeviceCommands;
using AdvancedSharpAdbClient.WinRT.DeviceCommands;
using AdvancedSharpAdbClient.WinRT.Extensions;
using AdvancedSharpAdbClient.WinRT.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Storage.Streams;
using AndroidProcess = AdvancedSharpAdbClient.WinRT.DeviceCommands.AndroidProcess;
using TimeSpan = System.TimeSpan;
using VersionInfo = AdvancedSharpAdbClient.WinRT.DeviceCommands.VersionInfo;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// <para>
    /// Implements the <see cref="IAdbClient"/> interface, and allows you to interact with the
    /// adb server and devices that are connected to that adb server.
    /// </para>
    /// <para>
    /// For example, to fetch a list of all devices that are currently connected to this PC, you can
    /// call the <see cref="GetDevices"/> method.
    /// </para>
    /// <para>
    /// To run a command on a device, you can use the <see cref="ExecuteRemoteCommandAsync(string, DeviceData, IShellOutputReceiver)"/>
    /// method.
    /// </para>
    /// </summary>
    /// <remarks><para><seealso href="https://github.com/android/platform_system_core/blob/master/adb/SERVICES.TXT">SERVICES.TXT</seealso></para>
    /// <para><seealso href="https://github.com/android/platform_system_core/blob/master/adb/adb_client.c">adb_client.c</seealso></para>
    /// <para><seealso href="https://github.com/android/platform_system_core/blob/master/adb/adb.c">adb.c</seealso></para></remarks>
    public sealed class AdbClient : IAdbClient, IAdbClientAsync, IDeviceExtensions, IDeviceExtensionsAsync
    {
        internal readonly AdvancedSharpAdbClient.AdbClient adbClient;

        /// <summary>
        /// The default port to use when connecting to a device over TCP/IP.
        /// </summary>
        public static int DefaultPort { get; } = AdvancedSharpAdbClient.AdbClient.DefaultPort;

        /// <summary>
        /// The port at which the Android Debug Bridge server listens by default.
        /// </summary>
        public static int DefaultAdbServerPort { get; } = AdvancedSharpAdbClient.AdbClient.DefaultAdbServerPort;

        /// <summary>
        /// Gets a new instance of the <see cref="AdbClient"/> class.
        /// </summary>
        [Deprecated("This function has been removed since SharpAdbClient. Here is a placeholder which function is gets a new instance instead of gets or sets the default instance.", DeprecationType.Deprecate, 65536)]
        public static AdbClient Instance => new();

        /// <summary>
        /// Initializes a new instance of the <see cref="AdbClient"/> class.
        /// </summary>
        public AdbClient() => adbClient = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="AdbClient"/> class.
        /// </summary>
        /// <param name="host">The host address at which the adb server is listening.</param>
        /// <param name="port">The port at which the adb server is listening.</param>
        public AdbClient(string host, int port) => adbClient = new(host, port);

        /// <summary>
        /// Get or set the <see cref="System.Text.Encoding.CodePage"/> of default encoding
        /// </summary>
        public static int Encoding
        {
            get => AdvancedSharpAdbClient.AdbClient.Encoding.CodePage;
            set => AdvancedSharpAdbClient.AdbClient.Encoding = System.Text.Encoding.GetEncoding(value);
        }

        /// <summary>
        /// Gets the current port at which the Android Debug Bridge server listens.
        /// </summary>
        public static int AdbServerPort => AdvancedSharpAdbClient.AdbClient.AdbServerPort;

        /// <inheritdoc/>
        public string EndPoint => adbClient.EndPoint.ToString();

        /// <summary>
        /// Create an ASCII string preceded by four hex digits. The opening "####"
        /// is the length of the rest of the string, encoded as ASCII hex(case
        /// doesn't matter).
        /// </summary>
        /// <param name="req">The request to form.</param>
        /// <returns>An array containing <c>####req</c>.</returns>
        public static byte[] FormAdbRequest(string req) => AdvancedSharpAdbClient.AdbClient.FormAdbRequest(req);

        /// <summary>
        /// Creates the adb forward request.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="port">The port.</param>
        /// <returns>This returns an array containing <c>"####tcp:{port}:{addStr}"</c>.</returns>
        public static byte[] CreateAdbForwardRequest(string address, int port) => AdvancedSharpAdbClient.AdbClient.CreateAdbForwardRequest(address, port);

        /// <inheritdoc/>
        public int GetAdbVersion() => adbClient.GetAdbVersion();

        /// <inheritdoc/>
        public IAsyncOperation<int> GetAdbVersionAsync() => AsyncInfo.Run(adbClient.GetAdbVersionAsync);

        /// <inheritdoc/>
        public void KillAdb() => adbClient.KillAdb();

        /// <inheritdoc/>
        public IAsyncAction KillAdbAsync() => AsyncInfo.Run(adbClient.KillAdbAsync);

        /// <inheritdoc/>
        public IEnumerable<DeviceData> GetDevices() => adbClient.GetDevices().Select(DeviceData.GetDeviceData);

        /// <inheritdoc/>
        public IAsyncOperation<IEnumerable<DeviceData>> GetDevicesAsync() => AsyncInfo.Run(async (cancellationToken) => (await adbClient.GetDevicesAsync(cancellationToken)).Select(DeviceData.GetDeviceData));

        /// <inheritdoc/>
        public int CreateForward(DeviceData device, string local, string remote, bool allowRebind) => adbClient.CreateForward(device.deviceData, local, remote, allowRebind);

        /// <inheritdoc/>
        public IAsyncOperation<int> CreateForwardAsync(DeviceData device, string local, string remote, bool allowRebind) => AsyncInfo.Run((cancellationToken) => adbClient.CreateForwardAsync(device.deviceData, local, remote, allowRebind, cancellationToken));

        /// <inheritdoc/>
        [DefaultOverload]
        public int CreateForward(DeviceData device, ForwardSpec local, ForwardSpec remote, bool allowRebind) => adbClient.CreateForward(device.deviceData, local.forwardSpec, remote.forwardSpec, allowRebind);

        /// <inheritdoc/>
        [DefaultOverload]
        public IAsyncOperation<int> CreateForwardAsync(DeviceData device, ForwardSpec local, ForwardSpec remote, bool allowRebind) => AsyncInfo.Run((cancellationToken) => adbClient.CreateForwardAsync(device.deviceData, local.forwardSpec, remote.forwardSpec, allowRebind, cancellationToken));

        /// <inheritdoc/>
        public int CreateReverseForward(DeviceData device, string remote, string local, bool allowRebind) => adbClient.CreateReverseForward(device.deviceData, remote, local, allowRebind);

        /// <inheritdoc/>
        public IAsyncOperation<int> CreateReverseForwardAsync(DeviceData device, string remote, string local, bool allowRebind) => AsyncInfo.Run((cancellationToken) => adbClient.CreateReverseForwardAsync(device.deviceData, remote, local, allowRebind, cancellationToken));

        /// <inheritdoc/>
        public void RemoveReverseForward(DeviceData device, string remote) => adbClient.RemoveReverseForward(device.deviceData, remote);

        /// <inheritdoc/>
        public IAsyncAction RemoveReverseForwardAsync(DeviceData device, string remote) => AsyncInfo.Run((cancellationToken) => adbClient.RemoveReverseForwardAsync(device.deviceData, remote, cancellationToken));

        /// <inheritdoc/>
        public void RemoveAllReverseForwards(DeviceData device) => adbClient.RemoveAllReverseForwards(device.deviceData);


        /// <inheritdoc/>
        public IAsyncAction RemoveAllReverseForwardsAsync(DeviceData device) => AsyncInfo.Run((cancellationToken) => adbClient.RemoveAllReverseForwardsAsync(device.deviceData, cancellationToken));

        /// <inheritdoc/>
        public void RemoveForward(DeviceData device, int localPort) => adbClient.RemoveForward(device.deviceData, localPort);

        /// <inheritdoc/>
        public IAsyncAction RemoveForwardAsync(DeviceData device, int localPort) => AsyncInfo.Run((cancellationToken) => adbClient.RemoveForwardAsync(device.deviceData, localPort, cancellationToken));

        /// <inheritdoc/>
        public void RemoveAllForwards(DeviceData device) => adbClient.RemoveAllForwards(device.deviceData);

        /// <inheritdoc/>
        public IAsyncAction RemoveAllForwardsAsync(DeviceData device) => AsyncInfo.Run((cancellationToken) => adbClient.RemoveAllForwardsAsync(device.deviceData, cancellationToken));

        /// <inheritdoc/>
        public IEnumerable<ForwardData> ListForward(DeviceData device) => adbClient.ListForward(device.deviceData).Select(ForwardData.GetForwardData);

        /// <inheritdoc/>
        public IAsyncOperation<IEnumerable<ForwardData>> ListForwardAsync(DeviceData device) => AsyncInfo.Run(async (cancellationToken) => (await adbClient.ListForwardAsync(device.deviceData, cancellationToken)).Select(ForwardData.GetForwardData));

        /// <inheritdoc/>
        public IEnumerable<ForwardData> ListReverseForward(DeviceData device) => adbClient.ListReverseForward(device.deviceData).Select(ForwardData.GetForwardData);

        /// <inheritdoc/>
        public IAsyncOperation<IEnumerable<ForwardData>> ListReverseForwardAsync(DeviceData device) => AsyncInfo.Run(async (cancellationToken) => (await adbClient.ListReverseForwardAsync(device.deviceData, cancellationToken)).Select(ForwardData.GetForwardData));

        /// <inheritdoc/>
        public void ExecuteRemoteCommand(string command, DeviceData device, IShellOutputReceiver receiver) => adbClient.ExecuteRemoteCommand(command, device.deviceData, WinRTOutputReceiver.GetShellOutputReceiver(receiver));

        /// <inheritdoc/>
        public IAsyncAction ExecuteRemoteCommandAsync(string command, DeviceData device, IShellOutputReceiver receiver) => AsyncInfo.Run((cancellationToken) => adbClient.ExecuteRemoteCommandAsync(command, device.deviceData, WinRTOutputReceiver.GetShellOutputReceiver(receiver), cancellationToken));

        /// <inheritdoc/>
        public void ExecuteRemoteCommand(string command, DeviceData device, IShellOutputReceiver receiver, int encoding) => adbClient.ExecuteRemoteCommand(command, device.deviceData, WinRTOutputReceiver.GetShellOutputReceiver(receiver), System.Text.Encoding.GetEncoding(encoding));

        /// <inheritdoc/>
        public IAsyncAction ExecuteRemoteCommandAsync(string command, DeviceData device, IShellOutputReceiver receiver, int encoding) => AsyncInfo.Run((cancellationToken) => adbClient.ExecuteRemoteCommandAsync(command, device.deviceData, WinRTOutputReceiver.GetShellOutputReceiver(receiver), System.Text.Encoding.GetEncoding(encoding), cancellationToken));

        /// <inheritdoc/>
        public Framebuffer CreateRefreshableFramebuffer(DeviceData device) => Framebuffer.GetFramebuffer(adbClient.CreateRefreshableFramebuffer(device.deviceData));

        /// <inheritdoc/>
        public Framebuffer GetFrameBuffer(DeviceData device) => Framebuffer.GetFramebuffer(adbClient.GetFrameBuffer(device.deviceData));

        /// <inheritdoc/>
        public IAsyncOperation<Framebuffer> GetFrameBufferAsync(DeviceData device) => AsyncInfo.Run(async (cancellationToken) => Framebuffer.GetFramebuffer(await adbClient.GetFrameBufferAsync(device.deviceData, cancellationToken)));

        /// <inheritdoc/>
        public void RunLogService(DeviceData device, MessageSink messageSink, [ReadOnlyArray] params LogId[] logNames) => adbClient.RunLogService(device.deviceData, (logEntry) => messageSink(LogEntry.GetLogEntry(logEntry)), logNames.Cast<AdvancedSharpAdbClient.Logs.LogId>().ToArray());

        /// <inheritdoc/>
        public IAsyncAction RunLogServiceAsync(DeviceData device, MessageSink messageSink, [ReadOnlyArray] params LogId[] logNames) => AsyncInfo.Run((cancellationToken) => adbClient.RunLogServiceAsync(device.deviceData, (logEntry) => messageSink(LogEntry.GetLogEntry(logEntry)), cancellationToken, logNames.Cast<AdvancedSharpAdbClient.Logs.LogId>().ToArray()));

        /// <inheritdoc/>
        public void Reboot(DeviceData device) => adbClient.Reboot(device.deviceData);

        /// <inheritdoc/>
        public IAsyncAction RebootAsync(DeviceData device) => AsyncInfo.Run((cancellationToken) => adbClient.RebootAsync(device.deviceData, cancellationToken));

        /// <inheritdoc/>
        public void Reboot(string into, DeviceData device) => adbClient.Reboot(into, device.deviceData);

        /// <inheritdoc/>
        public IAsyncAction RebootAsync(string into, DeviceData device) => AsyncInfo.Run((cancellationToken) => adbClient.RebootAsync(into, device.deviceData, cancellationToken));

        /// <inheritdoc/>
        public string Pair(string host, string code) => adbClient.Pair(host, code);

        /// <inheritdoc/>
        public IAsyncOperation<string> PairAsync(string host, string code) => AsyncInfo.Run((cancellationToken) => adbClient.PairAsync(host, code, cancellationToken));

        /// <inheritdoc/>
        public string Pair(string host, int port, string code) => adbClient.Pair(host, port, code);

        /// <inheritdoc/>
        public IAsyncOperation<string> PairAsync(string host, int port, string code) => AsyncInfo.Run((cancellationToken) => adbClient.PairAsync(host, port, code, cancellationToken));

        /// <inheritdoc/>
        public string Connect(string host) => adbClient.Connect(host);

        /// <inheritdoc/>
        public IAsyncOperation<string> ConnectAsync(string host) => AsyncInfo.Run((cancellationToken) => adbClient.ConnectAsync(host, cancellationToken: cancellationToken));

        /// <inheritdoc/>
        public string Connect(string host, int port) => adbClient.Connect(host, port);

        /// <inheritdoc/>
        public IAsyncOperation<string> ConnectAsync(string host, int port) => AsyncInfo.Run((cancellationToken) => adbClient.ConnectAsync(host, port, cancellationToken));

        /// <inheritdoc/>
        public string Disconnect(string host) => Disconnect(host, AdvancedSharpAdbClient.AdbClient.DefaultPort);

        /// <inheritdoc/>
        public IAsyncOperation<string> DisconnectAsync(string host) => DisconnectAsync(host, AdvancedSharpAdbClient.AdbClient.DefaultPort);

        /// <inheritdoc/>
        public string Disconnect(string host, int port)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException(nameof(host));
            }

            string[] values = host.Split(':');

            return values.Length <= 0
                ? throw new ArgumentNullException(nameof(host))
                : adbClient.Disconnect(new DnsEndPoint(values[0], values.Length > 1 && int.TryParse(values[1], out int _port) ? _port : port));
        }

        /// <inheritdoc/>
        public IAsyncOperation<string> DisconnectAsync(string host, int port)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException(nameof(host));
            }

            string[] values = host.Split(':');

            return values.Length <= 0
                ? throw new ArgumentNullException(nameof(host))
                : AsyncInfo.Run((cancellationToken) => adbClient.DisconnectAsync(new DnsEndPoint(values[0], values.Length > 1 && int.TryParse(values[1], out int _port) ? _port : port), cancellationToken));
        }

        /// <inheritdoc/>
        public void Root(DeviceData device) => adbClient.Root(device.deviceData);

        /// <inheritdoc/>
        public IAsyncAction RootAsync(DeviceData device) => AsyncInfo.Run((cancellationToken) => adbClient.RootAsync(device.deviceData, cancellationToken));

        /// <inheritdoc/>
        public void Unroot(DeviceData device) => adbClient.Unroot(device.deviceData);

        /// <inheritdoc/>
        public IAsyncAction UnrootAsync(DeviceData device) => AsyncInfo.Run((cancellationToken) => adbClient.UnrootAsync(device.deviceData, cancellationToken));

        /// <inheritdoc/>
        public void Install(DeviceData device, IInputStream apk) => adbClient.Install(device.deviceData, apk.AsStreamForRead());

        /// <inheritdoc/>
        public IAsyncAction InstallAsync(DeviceData device, IInputStream apk) => AsyncInfo.Run((cancellationToken) => adbClient.InstallAsync(device.deviceData, apk.AsStreamForRead(), cancellationToken));

        /// <inheritdoc/>
        public void Install(DeviceData device, IInputStream apk, [ReadOnlyArray] params string[] arguments) => adbClient.Install(device.deviceData, apk.AsStreamForRead(), arguments);

        /// <inheritdoc/>
        public IAsyncAction InstallAsync(DeviceData device, IInputStream apk, [ReadOnlyArray] params string[] arguments) => AsyncInfo.Run((cancellationToken) => adbClient.InstallAsync(device.deviceData, apk.AsStreamForRead(), cancellationToken, arguments));

        /// <inheritdoc/>
        public void InstallMultiple(DeviceData device, IEnumerable<IInputStream> splitAPKs, string packageName) => adbClient.InstallMultiple(device.deviceData, splitAPKs.Select((x) => x.AsStreamForRead()), packageName);

        /// <inheritdoc/>
        public IAsyncAction InstallMultipleAsync(DeviceData device, IEnumerable<IInputStream> splitAPKs, string packageName) => AsyncInfo.Run((cancellationToken) => adbClient.InstallMultipleAsync(device.deviceData, splitAPKs.Select((x) => x.AsStreamForRead()), packageName, cancellationToken));

        /// <inheritdoc/>
        public void InstallMultiple(DeviceData device, IEnumerable<IInputStream> splitAPKs, string packageName, [ReadOnlyArray] params string[] arguments) => adbClient.InstallMultiple(device.deviceData, splitAPKs.Select((x) => x.AsStreamForRead()), packageName, arguments);

        /// <inheritdoc/>
        public IAsyncAction InstallMultipleAsync(DeviceData device, IEnumerable<IInputStream> splitAPKs, string packageName, [ReadOnlyArray] params string[] arguments) => AsyncInfo.Run((cancellationToken) => adbClient.InstallMultipleAsync(device.deviceData, splitAPKs.Select((x) => x.AsStreamForRead()), packageName, cancellationToken, arguments));

        /// <inheritdoc/>
        [DefaultOverload]
        public void InstallMultiple(DeviceData device, IInputStream baseAPK, IEnumerable<IInputStream> splitAPKs) => adbClient.InstallMultiple(device.deviceData, baseAPK.AsStreamForRead(), splitAPKs.Select((x) => x.AsStreamForRead()));

        /// <inheritdoc/>
        [DefaultOverload]
        public IAsyncAction InstallMultipleAsync(DeviceData device, IInputStream baseAPK, IEnumerable<IInputStream> splitAPKs) => AsyncInfo.Run((cancellationToken) => adbClient.InstallMultipleAsync(device.deviceData, baseAPK.AsStreamForRead(), splitAPKs.Select((x) => x.AsStreamForRead()), cancellationToken));

        /// <inheritdoc/>
        [DefaultOverload]
        public void InstallMultiple(DeviceData device, IInputStream baseAPK, IEnumerable<IInputStream> splitAPKs, [ReadOnlyArray] params string[] arguments) => adbClient.InstallMultiple(device.deviceData, baseAPK.AsStreamForRead(), splitAPKs.Select((x) => x.AsStreamForRead()), arguments);

        /// <inheritdoc/>
        [DefaultOverload]
        public IAsyncAction InstallMultipleAsync(DeviceData device, IInputStream baseAPK, IEnumerable<IInputStream> splitAPKs, [ReadOnlyArray] params string[] arguments) => AsyncInfo.Run((cancellationToken) => adbClient.InstallMultipleAsync(device.deviceData, baseAPK.AsStreamForRead(), splitAPKs.Select((x) => x.AsStreamForRead()), cancellationToken, arguments));

        /// <inheritdoc/>
        public string InstallCreate(DeviceData device) => adbClient.InstallCreate(device.deviceData);

        /// <inheritdoc/>
        public IAsyncOperation<string> InstallCreateAsync(DeviceData device) => AsyncInfo.Run((cancellationToken) => adbClient.InstallCreateAsync(device.deviceData, null, cancellationToken));

        /// <inheritdoc/>
        public string InstallCreate(DeviceData device, [ReadOnlyArray] params string[] arguments) => adbClient.InstallCreate(device.deviceData, arguments: arguments);

        /// <inheritdoc/>
        public IAsyncOperation<string> InstallCreateAsync(DeviceData device, [ReadOnlyArray] params string[] arguments) => AsyncInfo.Run((cancellationToken) => adbClient.InstallCreateAsync(device.deviceData, null, cancellationToken, arguments));

        /// <inheritdoc/>
        [DefaultOverload]
        public string InstallCreate(DeviceData device, string packageName) => adbClient.InstallCreate(device.deviceData, packageName);

        /// <inheritdoc/>
        [DefaultOverload]
        public IAsyncOperation<string> InstallCreateAsync(DeviceData device, string packageName) => AsyncInfo.Run((cancellationToken) => adbClient.InstallCreateAsync(device.deviceData, packageName, cancellationToken));

        /// <inheritdoc/>
        public string InstallCreate(DeviceData device, string packageName, [ReadOnlyArray] params string[] arguments) => adbClient.InstallCreate(device.deviceData, packageName, arguments);

        /// <inheritdoc/>
        public IAsyncOperation<string> InstallCreateAsync(DeviceData device, string packageName, [ReadOnlyArray] params string[] arguments) => AsyncInfo.Run((cancellationToken) => adbClient.InstallCreateAsync(device.deviceData, packageName, cancellationToken, arguments));

        /// <inheritdoc/>
        public void InstallWrite(DeviceData device, IInputStream apk, string apkName, string session) => adbClient.InstallWrite(device.deviceData, apk.AsStreamForRead(), apkName, session);

        /// <inheritdoc/>
        public IAsyncAction InstallWriteAsync(DeviceData device, IInputStream apk, string apkName, string session) => AsyncInfo.Run((cancellationToken) => adbClient.InstallWriteAsync(device.deviceData, apk.AsStreamForRead(), apkName, session, cancellationToken));

        /// <inheritdoc/>
        public void InstallCommit(DeviceData device, string session) => adbClient.InstallCommit(device.deviceData, session);

        /// <inheritdoc/>
        public IAsyncAction InstallCommitAsync(DeviceData device, string session) => AsyncInfo.Run((cancellationToken) => adbClient.InstallCommitAsync(device.deviceData, session, cancellationToken));

        /// <inheritdoc/>
        public IEnumerable<string> GetFeatureSet(DeviceData device) => adbClient.GetFeatureSet(device.deviceData);

        /// <inheritdoc/>
        public IAsyncOperation<IEnumerable<string>> GetFeatureSetAsync(DeviceData device) => AsyncInfo.Run((cancellationToken) => adbClient.GetFeatureSetAsync(device.deviceData, cancellationToken));

        /// <inheritdoc/>
        public string DumpScreenString(DeviceData device) => adbClient.DumpScreenString(device.deviceData);

        /// <inheritdoc/>
        public IAsyncOperation<string> DumpScreenStringAsync(DeviceData device) => AsyncInfo.Run((cancellationToken) => adbClient.DumpScreenStringAsync(device.deviceData, cancellationToken));

        /// <inheritdoc/>
        public XmlDocument DumpScreen(DeviceData device) => adbClient.DumpScreenWinRT(device.deviceData);

        /// <inheritdoc/>
        public IAsyncOperation<XmlDocument> DumpScreenAsync(DeviceData device) => AsyncInfo.Run((cancellationToken) => adbClient.DumpScreenWinRTAsync(device.deviceData, cancellationToken));

        /// <inheritdoc/>
        public void Click(DeviceData device, Cords cords) => adbClient.Click(device.deviceData, cords.cords);

        /// <inheritdoc/>
        public IAsyncAction ClickAsync(DeviceData device, Cords cords) => AsyncInfo.Run((cancellationToken) => adbClient.ClickAsync(device.deviceData, cords.cords, cancellationToken));

        /// <inheritdoc/>
        public void Click(DeviceData device, int x, int y) => adbClient.Click(device.deviceData, x, y);

        /// <inheritdoc/>
        public IAsyncAction ClickAsync(DeviceData device, int x, int y) => AsyncInfo.Run((cancellationToken) => adbClient.ClickAsync(device.deviceData, x, y, cancellationToken));

        /// <inheritdoc/>
        public void Swipe(DeviceData device, Element first, Element second, long speed) => adbClient.Swipe(device.deviceData, first.element, second.element, speed);

        /// <inheritdoc/>
        public IAsyncAction SwipeAsync(DeviceData device, Element first, Element second, long speed) => AsyncInfo.Run((cancellationToken) => adbClient.SwipeAsync(device.deviceData, first.element, second.element, speed, cancellationToken));

        /// <inheritdoc/>
        public void Swipe(DeviceData device, int x1, int y1, int x2, int y2, long speed) => adbClient.Swipe(device.deviceData, x1, y1, x2, y2, speed);

        /// <inheritdoc/>
        public IAsyncAction SwipeAsync(DeviceData device, int x1, int y1, int x2, int y2, long speed) => AsyncInfo.Run((cancellationToken) => adbClient.SwipeAsync(device.deviceData, x1, y1, x2, y2, speed, cancellationToken));

        /// <inheritdoc/>
        public bool IsCurrentApp(DeviceData device, string packageName) => adbClient.IsCurrentApp(device.deviceData, packageName);

        /// <inheritdoc/>
        public IAsyncOperation<bool> IsCurrentAppAsync(DeviceData device, string packageName) => AsyncInfo.Run((cancellationToken) => adbClient.IsCurrentAppAsync(device.deviceData, packageName, cancellationToken));

        /// <inheritdoc/>
        public bool IsAppRunning(DeviceData device, string packageName) => adbClient.IsAppRunning(device.deviceData, packageName);

        /// <inheritdoc/>
        public IAsyncOperation<bool> IsAppRunningAsync(DeviceData device, string packageName) => AsyncInfo.Run((cancellationToken) => adbClient.IsAppRunningAsync(device.deviceData, packageName, cancellationToken));

        /// <inheritdoc/>
        public AppStatus GetAppStatus(DeviceData device, string packageName) => (AppStatus)adbClient.GetAppStatus(device.deviceData, packageName);

        /// <inheritdoc/>
        public IAsyncOperation<AppStatus> GetAppStatusAsync(DeviceData device, string packageName) => AsyncInfo.Run(async (cancellationToken) => (AppStatus)await adbClient.GetAppStatusAsync(device.deviceData, packageName, cancellationToken));

        /// <inheritdoc/>
        public Element FindElement(DeviceData device) => Element.GetElement(adbClient.FindElement(device.deviceData));

        /// <inheritdoc/>
        public IAsyncOperation<Element> FindElementAsync(DeviceData device) => Task.Run(async () => Element.GetElement(await adbClient.FindElementAsync(device.deviceData))).AsAsyncOperation();

        /// <inheritdoc/>
        [DefaultOverload]
        public Element FindElement(DeviceData device, string xpath) => Element.GetElement(adbClient.FindElement(device.deviceData, xpath));

        /// <inheritdoc/>
        [DefaultOverload]
        public IAsyncOperation<Element> FindElementAsync(DeviceData device, string xpath) => Task.Run(async () => Element.GetElement(await adbClient.FindElementAsync(device.deviceData, xpath))).AsAsyncOperation();

        /// <inheritdoc/>
        public Element FindElement(DeviceData device, TimeSpan timeout) => Element.GetElement(adbClient.FindElement(device.deviceData, timeout: timeout));

        /// <inheritdoc/>
        public IAsyncOperation<Element> FindElementAsync(DeviceData device, TimeSpan timeout) => Task.Run(async () => Element.GetElement(await adbClient.FindElementAsync(device.deviceData, cancellationToken: timeout.GetCancellationToken()))).AsAsyncOperation();

        /// <inheritdoc/>
        public Element FindElement(DeviceData device, string xpath, TimeSpan timeout) => Element.GetElement(adbClient.FindElement(device.deviceData, xpath, timeout));

        /// <inheritdoc/>
        public IAsyncOperation<Element> FindElementAsync(DeviceData device, string xpath, TimeSpan timeout) => Task.Run(async () => Element.GetElement(await adbClient.FindElementAsync(device.deviceData, xpath, timeout.GetCancellationToken()))).AsAsyncOperation();

        /// <inheritdoc/>
        public IEnumerable<Element> FindElements(DeviceData device) => adbClient.FindElements(device.deviceData).Select(Element.GetElement);

        /// <inheritdoc/>
        public IAsyncOperation<IEnumerable<Element>> FindElementsAsync(DeviceData device) => Task.Run(async () => (await adbClient.FindElementsAsync(device.deviceData)).Select(Element.GetElement)).AsAsyncOperation();

        /// <inheritdoc/>
        [DefaultOverload]
        public IEnumerable<Element> FindElements(DeviceData device, string xpath) => adbClient.FindElements(device.deviceData, xpath).Select(Element.GetElement);

        /// <inheritdoc/>
        [DefaultOverload]
        public IAsyncOperation<IEnumerable<Element>> FindElementsAsync(DeviceData device, string xpath) => Task.Run(async () => (await adbClient.FindElementsAsync(device.deviceData, xpath)).Select(Element.GetElement)).AsAsyncOperation();

        /// <inheritdoc/>
        public IEnumerable<Element> FindElements(DeviceData device, TimeSpan timeout) => adbClient.FindElements(device.deviceData, timeout: timeout).Select(Element.GetElement);

        /// <inheritdoc/>
        public IAsyncOperation<IEnumerable<Element>> FindElementsAsync(DeviceData device, TimeSpan timeout) => Task.Run(async () => (await adbClient.FindElementsAsync(device.deviceData, cancellationToken: timeout.GetCancellationToken())).Select(Element.GetElement)).AsAsyncOperation();

        /// <inheritdoc/>
        public IEnumerable<Element> FindElements(DeviceData device, string xpath, TimeSpan timeout) => adbClient.FindElements(device.deviceData, xpath, timeout).Select(Element.GetElement);

        /// <inheritdoc/>
        public IAsyncOperation<IEnumerable<Element>> FindElementsAsync(DeviceData device, string xpath, TimeSpan timeout) => Task.Run(async () => (await adbClient.FindElementsAsync(device.deviceData, xpath, timeout.GetCancellationToken())).Select(Element.GetElement)).AsAsyncOperation();

        /// <inheritdoc/>
        public void SendKeyEvent(DeviceData device, string key) => adbClient.SendKeyEvent(device.deviceData, key);

        /// <inheritdoc/>
        public IAsyncAction SendKeyEventAsync(DeviceData device, string key) => AsyncInfo.Run((cancellationToken) => adbClient.SendKeyEventAsync(device.deviceData, key, cancellationToken));

        /// <inheritdoc/>
        public void SendText(DeviceData device, string text) => adbClient.SendText(device.deviceData, text);

        /// <inheritdoc/>
        public IAsyncAction SendTextAsync(DeviceData device, string text) => AsyncInfo.Run((cancellationToken) => adbClient.SendTextAsync(device.deviceData, text, cancellationToken));

        /// <inheritdoc/>
        public void ClearInput(DeviceData device, int charCount) => adbClient.ClearInput(device.deviceData, charCount);

        /// <inheritdoc/>
        public IAsyncAction ClearInputAsync(DeviceData device, int charCount) => AsyncInfo.Run((cancellationToken) => adbClient.ClearInputAsync(device.deviceData, charCount, cancellationToken));

        /// <inheritdoc/>
        public void StartApp(DeviceData device, string packageName) => adbClient.StartApp(device.deviceData, packageName);

        /// <inheritdoc/>
        public IAsyncAction StartAppAsync(DeviceData device, string packageName) => AsyncInfo.Run((cancellationToken) => adbClient.StartAppAsync(device.deviceData, packageName, cancellationToken));

        /// <inheritdoc/>
        public void StopApp(DeviceData device, string packageName) => adbClient.StopApp(device.deviceData, packageName);

        /// <inheritdoc/>
        public IAsyncAction StopAppAsync(DeviceData device, string packageName) => AsyncInfo.Run((cancellationToken) => adbClient.StopAppAsync(device.deviceData, packageName, cancellationToken));

        /// <inheritdoc/>
        public void BackBtn(DeviceData device) => adbClient.BackBtn(device.deviceData);

        /// <inheritdoc/>
        public IAsyncAction BackBtnAsync(DeviceData device) => adbClient.BackBtnAsync(device.deviceData).AsAsyncAction();

        /// <inheritdoc/>
        public void HomeBtn(DeviceData device) => adbClient.HomeBtn(device.deviceData);

        /// <inheritdoc/>
        public IAsyncAction HomeBtnAsync(DeviceData device) => adbClient.HomeBtnAsync(device.deviceData).AsAsyncAction();

        /// <inheritdoc/>
        public void ExecuteShellCommand(DeviceData device, string command, IShellOutputReceiver receiver) => ExecuteRemoteCommand(command, device, receiver);

        /// <inheritdoc/>
        public IAsyncAction ExecuteShellCommandAsync(DeviceData device, string command, IShellOutputReceiver receiver) => ExecuteRemoteCommandAsync(command, device, receiver);

        /// <inheritdoc/>
        public string GetProperty(DeviceData device, string property) => adbClient.GetProperty(device.deviceData, property);

        /// <inheritdoc/>
        public IAsyncOperation<string> GetPropertyAsync(DeviceData device, string property) => AsyncInfo.Run((cancellationToken) => adbClient.GetPropertyAsync(device.deviceData, property, cancellationToken));

        /// <inheritdoc/>
        public IDictionary<string, string> GetProperties(DeviceData device) => adbClient.GetProperties(device.deviceData);

        /// <inheritdoc/>
        public IAsyncOperation<IDictionary<string, string>> GetPropertiesAsync(DeviceData device) => AsyncInfo.Run(async (cancellationToken) => await adbClient.GetPropertiesAsync(device.deviceData, cancellationToken) as IDictionary<string, string>);

        /// <inheritdoc/>
        public IDictionary<string, string> GetEnvironmentVariables(DeviceData device) => adbClient.GetEnvironmentVariables(device.deviceData);

        /// <inheritdoc/>
        public IAsyncOperation<IDictionary<string, string>> GetEnvironmentVariablesAsync(DeviceData device) => AsyncInfo.Run(async (cancellationToken) => await adbClient.GetEnvironmentVariablesAsync(device.deviceData, cancellationToken) as IDictionary<string, string>);

        /// <inheritdoc/>
        public void UninstallPackage(DeviceData device, string packageName) => adbClient.UninstallPackage(device.deviceData, packageName);

        /// <inheritdoc/>
        public IAsyncAction UninstallPackageAsync(DeviceData device, string packageName) => AsyncInfo.Run((cancellationToken) => adbClient.UninstallPackageAsync(device.deviceData, packageName, cancellationToken));

        /// <inheritdoc/>
        public VersionInfo GetPackageVersion(DeviceData device, string packageName) => VersionInfo.GetVersionInfo(adbClient.GetPackageVersion(device.deviceData, packageName));

        /// <inheritdoc/>
        public IAsyncOperation<VersionInfo> GetPackageVersionAsync(DeviceData device, string packageName) => AsyncInfo.Run(async (cancellationToken) => VersionInfo.GetVersionInfo(await adbClient.GetPackageVersionAsync(device.deviceData, packageName, cancellationToken)));

        /// <inheritdoc/>
        public IEnumerable<AndroidProcess> ListProcesses(DeviceData device) => adbClient.ListProcesses(device.deviceData).Select(AndroidProcess.GetAndroidProcess);

        /// <inheritdoc/>
        public IAsyncOperation<IEnumerable<AndroidProcess>> ListProcessesAsync(DeviceData device) => AsyncInfo.Run(async (cancellationToken) => (await adbClient.ListProcessesAsync(device.deviceData, cancellationToken)).Select(AndroidProcess.GetAndroidProcess));

        /// <summary>
        /// Sets default encoding (default - UTF8)
        /// </summary>
        /// <param name="codepage">The code page identifier of the preferred encoding.</param>
        [DefaultOverload]
        public static void SetEncoding(int codepage) => AdvancedSharpAdbClient.AdbClient.SetEncoding(System.Text.Encoding.GetEncoding(codepage));

        /// <summary>
        /// Sets default encoding (default - UTF8)
        /// </summary>
        /// <param name="name">The code page name of the preferred encoding.</param>
        public static void SetEncoding(string name) => AdvancedSharpAdbClient.AdbClient.SetEncoding(System.Text.Encoding.GetEncoding(name));
    }
}