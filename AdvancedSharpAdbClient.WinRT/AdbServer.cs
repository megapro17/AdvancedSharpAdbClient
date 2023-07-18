using AdvancedSharpAdbClient.WinRT.Extensions;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// <para>
    /// Provides methods for interacting with the adb server. The adb server must be running for
    /// the rest of the <c>Managed.Adb</c> library to work.
    /// </para>
    /// <para>
    /// The adb server is a background process that runs on the host machine.
    /// Its purpose if to sense the USB ports to know when devices are attached/removed,
    /// as well as when emulator instances start/stop. The ADB server is really one
    /// giant multiplexing loop whose purpose is to orchestrate the exchange of data
    /// between clients and devices.
    /// </para>
    /// </summary>
    public sealed class AdbServer : IAdbServer, IAdbServerAsync
    {
        internal readonly AdvancedSharpAdbClient.AdbServer adbServer;

        /// <summary>
        /// The minimum version of <c>adb.exe</c> that is supported by this library.
        /// </summary>
        public static PackageVersion RequiredAdbVersion => AdvancedSharpAdbClient.AdbServer.RequiredAdbVersion.GetPackageVersion();

        /// <summary>
        /// Initializes a new instance of the <see cref="AdbServer"/> class.
        /// </summary>
        public AdbServer() => adbServer = new();

        /// <summary>
        /// Gets or sets the default instance of the <see cref="IAdbServer"/> interface.
        /// </summary>
        public static AdbServer Instance { get; set; } = new AdbServer();

        /// <inheritdoc/>
        public StartServerResult StartServer(string adbPath, bool restartServerIfNewer) => (StartServerResult)adbServer.StartServer(adbPath, restartServerIfNewer);

        /// <inheritdoc/>
        public IAsyncOperation<StartServerResult> StartServerAsync(string adbPath, bool restartServerIfNewer) => AsyncInfo.Run(async (cancellationToken) => (StartServerResult)await adbServer.StartServerAsync(adbPath, restartServerIfNewer, cancellationToken));

        /// <inheritdoc/>
        public StartServerResult RestartServer() => (StartServerResult)adbServer.RestartServer();

        /// <inheritdoc/>
        public IAsyncOperation<StartServerResult> RestartServerAsync() => AsyncInfo.Run(async (cancellationToken) => (StartServerResult)await adbServer.RestartServerAsync(cancellationToken));

        /// <inheritdoc/>
        public StartServerResult RestartServer(string adbPath) => (StartServerResult)adbServer.RestartServer(adbPath);

        /// <inheritdoc/>
        public IAsyncOperation<StartServerResult> RestartServerAsync(string adbPath) => AsyncInfo.Run(async (cancellationToken) => (StartServerResult)await adbServer.RestartServerAsync(adbPath, cancellationToken));

        /// <inheritdoc/>
        public AdbServerStatus GetStatus() => AdbServerStatus.GetAdbServerStatus(adbServer.GetStatus());

        /// <inheritdoc/>
        public IAsyncOperation<AdbServerStatus> GetStatusAsync() => AsyncInfo.Run(async (cancellationToken) => AdbServerStatus.GetAdbServerStatus(await adbServer.GetStatusAsync(cancellationToken)));
    }
}
