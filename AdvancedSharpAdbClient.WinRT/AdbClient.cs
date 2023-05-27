// <copyright file="AdbClient.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using AdvancedSharpAdbClient.WinRT.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Storage.Streams;
using TimeSpan = System.TimeSpan;

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
    /// To run a command on a device, you can use the <see cref="ExecuteRemoteCommandAsync(string, DeviceData, IShellOutputReceiver, TimeSpan)"/>
    /// method.
    /// </para>
    /// </summary>
    /// <remarks><para><seealso href="https://github.com/android/platform_system_core/blob/master/adb/SERVICES.TXT">SERVICES.TXT</seealso></para>
    /// <para><seealso href="https://github.com/android/platform_system_core/blob/master/adb/adb_client.c">adb_client.c</seealso></para>
    /// <para><seealso href="https://github.com/android/platform_system_core/blob/master/adb/adb.c">adb.c</seealso></para></remarks>
    public sealed class AdbClient : IAdbClient, IAdbClientAsync
    {
        internal readonly AdvancedSharpAdbClient.AdbClient adbClient;

        /// <summary>
        /// The port at which the Android Debug Bridge server listens by default.
        /// </summary>
        public static int AdbServerPort => 5037;

        /// <summary>
        /// The default port to use when connecting to a device over TCP/IP.
        /// </summary>
        public static int DefaultPort => 5555;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdbClient"/> class.
        /// </summary>
        public AdbClient() => adbClient = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="AdbClient"/> class.
        /// </summary>
        /// <param name="address">The host name or a string representation of the IP address.</param>
        /// <param name="port">The port number associated with the address, or 0 to specify any available port. port is in host order.</param>
        public AdbClient(string address, int port) => adbClient = new(new DnsEndPoint(address, port), Factories.AdbSocketFactory);

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
        public IAsyncOperation<int> GetAdbVersionAsync() => adbClient.GetAdbVersionAsync().AsAsyncOperation();

        /// <inheritdoc/>
        public IAsyncOperation<int> GetAdbVersionAsync(TimeSpan timeout) => adbClient.GetAdbVersionAsync(timeout.GetCancellationToken()).AsAsyncOperation();

        /// <inheritdoc/>
        public void KillAdb() => adbClient.KillAdb();

        /// <inheritdoc/>
        public IAsyncAction KillAdbAsync() => adbClient.KillAdbAsync().AsAsyncAction();

        /// <inheritdoc/>
        public IAsyncAction KillAdbAsync(TimeSpan timeout) => adbClient.KillAdbAsync(timeout.GetCancellationToken()).AsAsyncAction();

        /// <inheritdoc/>
        public IEnumerable<DeviceData> GetDevices() => adbClient.GetDevices().Select(DeviceData.GetDeviceData);

        /// <inheritdoc/>
        public IAsyncOperation<IEnumerable<DeviceData>> GetDevicesAsync() => Task.Run(async () => (await adbClient.GetDevicesAsync()).Select(DeviceData.GetDeviceData)).AsAsyncOperation();

        /// <inheritdoc/>
        public IAsyncOperation<IEnumerable<DeviceData>> GetDevicesAsync(TimeSpan timeout) => Task.Run(async () => (await adbClient.GetDevicesAsync(timeout.GetCancellationToken())).Select(DeviceData.GetDeviceData)).AsAsyncOperation();

        /// <inheritdoc/>
        public int CreateForward(DeviceData device, string local, string remote, bool allowRebind) => adbClient.CreateForward(device.deviceData, local, remote, allowRebind);

        /// <inheritdoc/>
        [DefaultOverload]
        public int CreateForward(DeviceData device, ForwardSpec local, ForwardSpec remote, bool allowRebind) => adbClient.CreateForward(device.deviceData, local.forwardSpec, remote.forwardSpec, allowRebind);

        /// <inheritdoc/>
        public int CreateReverseForward(DeviceData device, string remote, string local, bool allowRebind) => adbClient.CreateReverseForward(device.deviceData, remote, local, allowRebind);

        /// <inheritdoc/>
        public void RemoveReverseForward(DeviceData device, string remote) => adbClient.RemoveReverseForward(device.deviceData, remote);

        /// <inheritdoc/>
        public void RemoveAllReverseForwards(DeviceData device) => adbClient.RemoveAllReverseForwards(device.deviceData);

        /// <inheritdoc/>
        public void RemoveForward(DeviceData device, int localPort) => adbClient.RemoveForward(device.deviceData, localPort);

        /// <inheritdoc/>
        public void RemoveAllForwards(DeviceData device) => adbClient.RemoveAllForwards(device.deviceData);

        /// <inheritdoc/>
        public IEnumerable<ForwardData> ListForward(DeviceData device) => adbClient.ListForward(device.deviceData).Select(ForwardData.GetForwardData);

        /// <inheritdoc/>
        public IEnumerable<ForwardData> ListReverseForward(DeviceData device) => adbClient.ListReverseForward(device.deviceData).Select(ForwardData.GetForwardData);

        /// <inheritdoc/>
        public void ExecuteRemoteCommand(string command, DeviceData device, IShellOutputReceiver receiver) => adbClient.ExecuteRemoteCommand(command, device.deviceData, WinRTOutputReceiver.GetShellOutputReceiver(receiver));

        /// <inheritdoc/>
        public IAsyncAction ExecuteRemoteCommandAsync(string command, DeviceData device, IShellOutputReceiver receiver) => adbClient.ExecuteRemoteCommandAsync(command, device.deviceData, WinRTOutputReceiver.GetShellOutputReceiver(receiver)).AsAsyncAction();

        /// <inheritdoc/>
        public IAsyncAction ExecuteRemoteCommandAsync(string command, DeviceData device, IShellOutputReceiver receiver, TimeSpan timeout) => adbClient.ExecuteRemoteCommandAsync(command, device.deviceData, WinRTOutputReceiver.GetShellOutputReceiver(receiver), timeout.GetCancellationToken()).AsAsyncAction();

        /// <inheritdoc/>
        public Framebuffer CreateRefreshableFramebuffer(DeviceData device) => Framebuffer.GetFramebuffer(adbClient.CreateRefreshableFramebuffer(device.deviceData));

        /// <inheritdoc/>
        public void Reboot(DeviceData device) => adbClient.Reboot(device.deviceData);

        /// <inheritdoc/>
        public void Reboot(string into, DeviceData device) => adbClient.Reboot(into, device.deviceData);

        /// <inheritdoc/>
        public string Pair(string host, string code) => adbClient.Pair(host, code);

        /// <inheritdoc/>
        public string Pair(string host, int port, string code) => adbClient.Pair(host, port, code);

        /// <inheritdoc/>
        public string Connect(string host) => adbClient.Connect(host);

        /// <inheritdoc/>
        public string Connect(string host, int port) => adbClient.Connect(host, port);

        /// <inheritdoc/>
        public string Disconnect(string host) => Disconnect(host, AdvancedSharpAdbClient.AdbClient.DefaultPort);

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
                : adbClient.Connect(new DnsEndPoint(values[0], values.Length > 1 && int.TryParse(values[1], out int _port) ? _port : port));
        }

        /// <inheritdoc/>
        public void Root(DeviceData device) => adbClient.Root(device.deviceData);

        /// <inheritdoc/>
        public void Unroot(DeviceData device) => adbClient.Unroot(device.deviceData);

        /// <inheritdoc/>
        public void Install(DeviceData device, IInputStream apk) => adbClient.Install(device.deviceData, apk.AsStreamForRead());

        /// <inheritdoc/>
        public void Install(DeviceData device, IInputStream apk, [ReadOnlyArray] params string[] arguments) => adbClient.Install(device.deviceData, apk.AsStreamForRead(), arguments);

        /// <inheritdoc/>
        public void InstallMultiple(DeviceData device, IEnumerable<IInputStream> splitAPKs, string packageName) => adbClient.InstallMultiple(device.deviceData, splitAPKs.Select((x) => x.AsStreamForRead()).ToArray(), packageName);

        /// <inheritdoc/>
        public void InstallMultiple(DeviceData device, IEnumerable<IInputStream> splitAPKs, string packageName, [ReadOnlyArray] params string[] arguments) => adbClient.InstallMultiple(device.deviceData, splitAPKs.Select((x) => x.AsStreamForRead()).ToArray(), packageName, arguments);

        /// <inheritdoc/>
        [DefaultOverload]
        public void InstallMultiple(DeviceData device, IInputStream baseAPK, IEnumerable<IInputStream> splitAPKs) => adbClient.InstallMultiple(device.deviceData, baseAPK.AsStreamForRead(), splitAPKs.Select((x) => x.AsStreamForRead()).ToArray());

        /// <inheritdoc/>
        [DefaultOverload]
        public void InstallMultiple(DeviceData device, IInputStream baseAPK, IEnumerable<IInputStream> splitAPKs, [ReadOnlyArray] params string[] arguments) => adbClient.InstallMultiple(device.deviceData, baseAPK.AsStreamForRead(), splitAPKs.Select((x) => x.AsStreamForRead()).ToArray(), arguments);

        /// <inheritdoc/>
        public string InstallCreate(DeviceData device) => adbClient.InstallCreate(device.deviceData);

        /// <inheritdoc/>
        [DefaultOverload]
        public string InstallCreate(DeviceData device, [ReadOnlyArray] params string[] arguments) => adbClient.InstallCreate(device.deviceData, arguments: arguments);

        /// <inheritdoc/>
        public string InstallCreate(DeviceData device, string packageName) => adbClient.InstallCreate(device.deviceData, packageName);

        /// <inheritdoc/>
        public string InstallCreate(DeviceData device, string packageName, [ReadOnlyArray] params string[] arguments) => adbClient.InstallCreate(device.deviceData, packageName, arguments);

        /// <inheritdoc/>
        public void InstallWrite(DeviceData device, IInputStream apk, string apkName, string session) => adbClient.InstallWrite(device.deviceData, apk.AsStreamForRead(), apkName, session);

        /// <inheritdoc/>
        public void InstallCommit(DeviceData device, string session) => adbClient.InstallCommit(device.deviceData, session);

        /// <inheritdoc/>
        public IList<string> GetFeatureSet(DeviceData device) => adbClient.GetFeatureSet(device.deviceData);

        /// <inheritdoc/>
        public XmlDocument DumpScreen(DeviceData device)
        {
            string xmlString = adbClient.DumpScreen(device.deviceData)?.OuterXml;
            if (!string.IsNullOrEmpty(xmlString))
            {
                XmlDocument doc = new();
                doc.LoadXml(xmlString);
                return doc;
            }
            return null;
        }

        /// <inheritdoc/>
        public void Click(DeviceData device, Cords cords) => adbClient.Click(device.deviceData, cords.cords);

        /// <inheritdoc/>
        public void Click(DeviceData device, int x, int y) => adbClient.Click(device.deviceData, x, y);

        /// <inheritdoc/>
        public void Swipe(DeviceData device, Element first, Element second, long speed) => adbClient.Swipe(device.deviceData, first.element, second.element, speed);

        /// <inheritdoc/>
        public void Swipe(DeviceData device, int x1, int y1, int x2, int y2, long speed) => adbClient.Swipe(device.deviceData, x1, y1, x2, y2, speed);

        /// <inheritdoc/>
        public bool IsCurrentApp(DeviceData device, string packageName) => adbClient.IsCurrentApp(device.deviceData, packageName);

        /// <inheritdoc/>
        public bool IsAppRunning(DeviceData device, string packageName) => adbClient.IsAppRunning(device.deviceData, packageName);

        /// <inheritdoc/>
        public AppStatus GetAppStatus(DeviceData device, string packageName)=> (AppStatus)adbClient.GetAppStatus(device.deviceData, packageName);

        /// <inheritdoc/>
        public Element FindElement(DeviceData device, string xpath) => Element.GetElement(adbClient.FindElement(device.deviceData, xpath));

        /// <inheritdoc/>
        public Element FindElement(DeviceData device, string xpath, TimeSpan timeout) => Element.GetElement(adbClient.FindElement(device.deviceData, xpath, timeout));

        /// <inheritdoc/>
        public IEnumerable<Element> FindElements(DeviceData device, string xpath) => adbClient.FindElements(device.deviceData, xpath).Select(Element.GetElement);

        /// <inheritdoc/>
        public IEnumerable<Element> FindElements(DeviceData device, string xpath, TimeSpan timeout) => adbClient.FindElements(device.deviceData, xpath, timeout).Select(Element.GetElement);

        /// <inheritdoc/>
        public void SendKeyEvent(DeviceData device, string key) => adbClient.SendKeyEvent(device.deviceData, key);

        /// <inheritdoc/>
        public void SendText(DeviceData device, string text) => adbClient.SendText(device.deviceData, text);

        /// <inheritdoc/>
        public void ClearInput(DeviceData device, int charCount) => adbClient.ClearInput(device.deviceData, charCount);

        /// <inheritdoc/>
        public void StartApp(DeviceData device, string packageName) => adbClient.StartApp(device.deviceData, packageName);

        /// <inheritdoc/>
        public void StopApp(DeviceData device, string packageName) => adbClient.StopApp(device.deviceData, packageName);

        /// <inheritdoc/>
        public void BackBtn(DeviceData device) => adbClient.BackBtn(device.deviceData);

        /// <inheritdoc/>
        public void HomeBtn(DeviceData device) => adbClient.HomeBtn(device.deviceData);

        /// <summary>
        /// Sets default encoding (default - UTF8)
        /// </summary>
        /// <param name="codepage">The code page identifier of the preferred encoding.</param>
        [DefaultOverload]
        public static void SetEncoding(int codepage) => AdvancedSharpAdbClient.AdbClient.SetEncoding(Encoding.GetEncoding(codepage));

        /// <summary>
        /// Sets default encoding (default - UTF8)
        /// </summary>
        /// <param name="name">The code page name of the preferred encoding.</param>
        public static void SetEncoding(string name) => AdvancedSharpAdbClient.AdbClient.SetEncoding(Encoding.GetEncoding(name));
    }
}