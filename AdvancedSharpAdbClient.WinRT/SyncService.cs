// <copyright file="SyncService.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using AdvancedSharpAdbClient.WinRT.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Storage.Streams;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Provides access to the sync service running on the Android device. Allows you to
    /// list, download and upload files on the device.
    /// </summary>
    /// <example>
    /// <para>To send files to or receive files from your Android device, you can use the following code:</para>
    /// <code>
    /// void DownloadFile()
    /// {
    ///     var device = new AdbClient().GetDevices().First();
    ///
    ///     using (SyncService service = new SyncService(new AdbSocket(), device))
    ///     using (Stream stream = File.OpenWrite(@"C:\MyFile.txt"))
    ///     {
    ///         service.Pull("/data/MyFile.txt", stream, null, CancellationToken.None);
    ///     }
    /// }
    ///
    /// void UploadFile()
    /// {
    ///     var device = new AdbClient().GetDevices().First();
    ///
    ///     using (SyncService service = new SyncService(new AdbSocket(), device))
    ///     using (Stream stream = File.OpenRead(@"C:\MyFile.txt"))
    ///     {
    ///         service.Push(stream, "/data/MyFile.txt", null, CancellationToken.None);
    ///     }
    /// }
    /// </code>
    /// </example>
    public sealed class SyncService : ISyncService, ISyncServiceAsync, IDisposable
    {
        internal readonly AdvancedSharpAdbClient.SyncService syncService;

        /// <inheritdoc/>
        public event EventHandler<SyncProgressChangedEventArgs> SyncProgressChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncService"/> class.
        /// </summary>
        /// <param name="client">A connection to an adb server.</param>
        /// <param name="device">The device on which to interact with the files.</param>
        public SyncService(AdbClient client, DeviceData device)
        {
            syncService = new(client.adbClient, device.deviceData);
            syncService.SyncProgressChanged += (sender, args) => SyncProgressChanged?.Invoke(sender, SyncProgressChangedEventArgs.GetSyncProgressChangedEventArgs(args));
        }

        /// <summary>
        /// Gets or sets the maximum size of data to transfer between the device and the PC in one block.
        /// </summary>
        public int MaxBufferSize
        {
            get => syncService.MaxBufferSize;
            set => syncService.MaxBufferSize = value;
        }

        /// <summary>
        /// Gets the device on which the file operations are being executed.
        /// </summary>
        public DeviceData Device => DeviceData.GetDeviceData(syncService.Device);

        /// <summary>
        /// Gets the <see cref="AdbSocket"/> that enables the connection with the adb server.
        /// </summary>
        public AdbSocket Socket => syncService.Socket is AdvancedSharpAdbClient.AdbSocket adbSocket ? new(adbSocket) : null;

        /// <inheritdoc/>
        public bool IsOpen => syncService.IsOpen;

        /// <inheritdoc/>
        public void Open() => syncService.Open();

        /// <inheritdoc/>
        public IAsyncAction OpenAsync() => AsyncInfo.Run(syncService.OpenAsync);

        /// <summary>
        /// Reopen this connection.
        /// </summary>
        /// <param name="socket">A <see cref="AdbSocket"/> that enables to connection with the adb server.</param>
        public void Reopen(AdbSocket socket) => syncService.Reopen(socket.adbSocket);

        /// <summary>
        /// Reopen this connection.
        /// </summary>
        /// <param name="socket">A <see cref="AdbSocket"/> that enables to connection with the adb server.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        public IAsyncAction ReopenAsync(AdbSocket socket) => AsyncInfo.Run((cancellationToken) => syncService.ReopenAsync(socket.adbSocket, cancellationToken));

        /// <summary>
        /// Reopen this connection.
        /// </summary>
        /// <param name="client">A connection to an adb server.</param>
        [DefaultOverload]
        public void Reopen(AdbClient client) => syncService.Reopen(client.adbClient);

        /// <summary>
        /// Reopen this connection.
        /// </summary>
        /// <param name="client">A connection to an adb server.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        [DefaultOverload]
        public IAsyncAction ReopenAsync(AdbClient client) => AsyncInfo.Run((cancellationToken) => syncService.ReopenAsync(client.adbClient, cancellationToken));

        /// <inheritdoc/>
        public void Push(IInputStream stream, string remotePath, int permissions, DateTimeOffset timestamp, AsyncActionProgressHandler<int> progress) => syncService.Push(stream.AsStreamForRead(), remotePath, permissions, timestamp, progress.GetProgress());

        /// <inheritdoc/>
        public IAsyncActionWithProgress<int> PushAsync(IInputStream stream, string remotePath, int permissions, DateTimeOffset timestamp) => AsyncInfo.Run<int>((cancellationToken, progress) => syncService.PushAsync(stream.AsStreamForRead(), remotePath, permissions, timestamp, progress, cancellationToken));

        /// <inheritdoc/>
        public void Pull(string remotePath, IOutputStream stream, AsyncActionProgressHandler<int> progress) => syncService.Pull(remotePath, stream.AsStreamForWrite(), progress.GetProgress());

        /// <inheritdoc/>
        public IAsyncActionWithProgress<int> PullAsync(string remotePath, IOutputStream stream) => AsyncInfo.Run<int>((cancellationToken, progress) => syncService.PullAsync(remotePath, stream.AsStreamForWrite(), progress, cancellationToken));

        /// <inheritdoc/>
        public FileStatistics Stat(string remotePath) => FileStatistics.GetFileStatistics(syncService.Stat(remotePath));

        /// <inheritdoc/>
        public IAsyncOperation<FileStatistics> StatAsync(string remotePath) => AsyncInfo.Run(async (cancellationToken) => FileStatistics.GetFileStatistics(await syncService.StatAsync(remotePath, cancellationToken)));

        /// <inheritdoc/>
        public IEnumerable<FileStatistics> GetDirectoryListing(string remotePath) => syncService.GetDirectoryListing(remotePath).Select(FileStatistics.GetFileStatistics);

        /// <inheritdoc/>
        public IAsyncOperation<IEnumerable<FileStatistics>> GetDirectoryListingAsync(string remotePath) => AsyncInfo.Run(async (cancellationToken) => (await syncService.GetDirectoryListingAsync(remotePath, cancellationToken)).Select(FileStatistics.GetFileStatistics));

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => syncService.Dispose();
    }
}
