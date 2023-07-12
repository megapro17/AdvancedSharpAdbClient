// <copyright file="AdbSocket.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Storage.Streams;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// <para>Implements a client for the Android Debug Bridge client-server protocol. Using the client, you
    /// can send messages to and receive messages from the Android Debug Bridge.</para>
    /// <para>The <see cref="AdbSocket"/> class implements the raw messaging protocol; that is,
    /// sending and receiving messages. For interacting with the services the Android Debug
    /// Bridge exposes, use the <see cref="AdbClient"/>.</para>
    /// <para>For more information about the protocol that is implemented here, see chapter
    /// II Protocol Details, section 1. Client &lt;-&gt;Server protocol at
    /// <see href="https://android.googlesource.com/platform/system/core/+/master/adb/OVERVIEW.TXT"/>.</para>
    /// </summary>
    public sealed class AdbSocket : IAdbSocket, IAdbSocketAsync
    {
        internal readonly AdvancedSharpAdbClient.AdbSocket adbSocket;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdbSocket"/> class.
        /// </summary>
        /// <param name="host">The host address at which the Android Debug Bridge is listening for clients.</param>
        /// <param name="port">The port at which the Android Debug Bridge is listening for clients.</param>
        public AdbSocket(string host, int port)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException(nameof(host));
            }

            string[] values = host.Split(':');

            DnsEndPoint EndPoint = values.Length <= 0
                ? throw new ArgumentNullException(nameof(host))
                : new DnsEndPoint(values[0], values.Length > 1 && int.TryParse(values[1], out int _port) ? _port : port);

            adbSocket = new(EndPoint);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdbSocket"/> class.
        /// </summary>
        /// <param name="socket">The <see cref="TcpSocket"/> at which the Android Debug Bridge is listening for clients.</param>
        public AdbSocket(TcpSocket socket) => adbSocket = new(socket.tcpSocket);

        /// <summary>
        /// Initializes a new instance of the <see cref="AdbSocket"/> class.
        /// </summary>
        internal AdbSocket(AdvancedSharpAdbClient.AdbSocket adbSocket) => this.adbSocket = adbSocket;

        /// <summary>
        /// Gets or sets the size of the receive buffer
        /// </summary>
        public static int ReceiveBufferSize
        {
            get => AdvancedSharpAdbClient.AdbSocket.ReceiveBufferSize;
            set => AdvancedSharpAdbClient.AdbSocket.ReceiveBufferSize = value;
        }

        /// <summary>
        /// Gets or sets the size of the write buffer.
        /// </summary>
        public static int WriteBufferSize
        {
            get => AdvancedSharpAdbClient.AdbSocket.WriteBufferSize;
            set => AdvancedSharpAdbClient.AdbSocket.WriteBufferSize = value;
        }

        /// <summary>
        /// Determines whether the specified reply is okay.
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <returns><see langword="true"/> if the specified reply is okay; otherwise, <see langword="false"/>.</returns>
        public static bool IsOkay([ReadOnlyArray] byte[] reply) => AdvancedSharpAdbClient.AdbSocket.IsOkay(reply);

        /// <inheritdoc/>
        public bool Connected => adbSocket.Connected;

        /// <inheritdoc/>
        public void Reconnect() => adbSocket.Reconnect();

        /// <inheritdoc/>
        public void Send([ReadOnlyArray] byte[] data, int length) => adbSocket.Send(data, length);

        /// <inheritdoc/>
        public IAsyncAction SendAsync([ReadOnlyArray] byte[] data, int length) => AsyncInfo.Run((cancellationToken) => adbSocket.SendAsync(data, length, cancellationToken));

        /// <inheritdoc/>
        public void Send([ReadOnlyArray] byte[] data, int offset, int length) => adbSocket.Send(data, offset, length);

        /// <inheritdoc/>
        public IAsyncAction SendAsync([ReadOnlyArray] byte[] data, int offset, int length) => AsyncInfo.Run((cancellationToken) => adbSocket.SendAsync(data, offset, length, cancellationToken));

        /// <inheritdoc/>
        public void SendSyncRequest(SyncCommand command, string path, int permissions) => adbSocket.SendSyncRequest((AdvancedSharpAdbClient.SyncCommand)command, path, permissions);

        /// <inheritdoc/>
        public IAsyncAction SendSyncRequestAsync(SyncCommand command, string path, int permissions) => AsyncInfo.Run((cancellationToken) => adbSocket.SendSyncRequestAsync((AdvancedSharpAdbClient.SyncCommand)command, path, permissions, cancellationToken));

        /// <inheritdoc/>
        [DefaultOverload]
        public void SendSyncRequest(SyncCommand command, string path) => adbSocket.SendSyncRequest((AdvancedSharpAdbClient.SyncCommand)command, path);

        /// <inheritdoc/>
        [DefaultOverload]
        public IAsyncAction SendSyncRequestAsync(SyncCommand command, string path) => AsyncInfo.Run((cancellationToken) => adbSocket.SendSyncRequestAsync((AdvancedSharpAdbClient.SyncCommand)command, path, cancellationToken));

        /// <inheritdoc/>
        public void SendSyncRequest(SyncCommand command, int length) => adbSocket.SendSyncRequest((AdvancedSharpAdbClient.SyncCommand)command, length);

        /// <inheritdoc/>
        public IAsyncAction SendSyncRequestAsync(SyncCommand command, int length) => AsyncInfo.Run((cancellationToken) => adbSocket.SendSyncRequestAsync((AdvancedSharpAdbClient.SyncCommand)command, length, cancellationToken));

        /// <inheritdoc/>
        public void SendAdbRequest(string request) => adbSocket.SendAdbRequest(request);

        /// <inheritdoc/>
        public IAsyncAction SendAdbRequestAsync(string request) => AsyncInfo.Run((cancellationToken) => adbSocket.SendAdbRequestAsync(request, cancellationToken));
        
        /// <inheritdoc/>
        public int Read([WriteOnlyArray] byte[] data) => adbSocket.Read(data);

        /// <inheritdoc/>
        public IAsyncOperation<int> ReadAsync([WriteOnlyArray] byte[] data) => AsyncInfo.Run((cancellationToken) => adbSocket.ReadAsync(data, cancellationToken));

        /// <inheritdoc/>
        public int Read([WriteOnlyArray] byte[] data, int length) => adbSocket.Read(data, length);

        /// <inheritdoc/>
        public IAsyncOperation<int> ReadAsync([WriteOnlyArray] byte[] data, int length) => AsyncInfo.Run((cancellationToken) => adbSocket.ReadAsync(data, length, cancellationToken));

        /// <inheritdoc/>
        public string ReadString() => adbSocket.ReadString();

        /// <inheritdoc/>
        public IAsyncOperation<string> ReadStringAsync() => AsyncInfo.Run(adbSocket.ReadStringAsync);

        /// <inheritdoc/>
        public string ReadSyncString() => adbSocket.ReadSyncString();

        /// <inheritdoc/>
        public IAsyncOperation<string> ReadSyncStringAsync() => AsyncInfo.Run(adbSocket.ReadSyncStringAsync);

        /// <inheritdoc/>
        public SyncCommand ReadSyncResponse() => (SyncCommand)adbSocket.ReadSyncResponse();

        /// <inheritdoc/>
        public IAsyncOperation<SyncCommand> ReadSyncResponseAsync() => AsyncInfo.Run(async (cancellationToken) => (SyncCommand)await adbSocket.ReadSyncResponseAsync(cancellationToken));

        /// <inheritdoc/>
        public AdbResponse ReadAdbResponse() => AdbResponse.GetAdbResponse(adbSocket.ReadAdbResponse());

        /// <inheritdoc/>
        public IAsyncOperation<AdbResponse> ReadAdbResponseAsync() => AsyncInfo.Run(async (cancellationToken) => AdbResponse.GetAdbResponse(await adbSocket.ReadAdbResponseAsync(cancellationToken)));

        /// <inheritdoc/>
        public IRandomAccessStream GetShellStream() => adbSocket.GetShellStream().AsRandomAccessStream();

        /// <inheritdoc/>
        public void SetDevice(DeviceData device) => adbSocket.SetDevice(device.deviceData);

        /// <inheritdoc/>
        public IAsyncAction SetDeviceAsync(DeviceData device) => AsyncInfo.Run((cancellationToken) => adbSocket.SetDeviceAsync(device.deviceData, cancellationToken));

        /// <summary>
        /// Releases all resources used by the current instance of the <see cref="AdbSocket"/> class.
        /// </summary>
        public void Dispose() => adbSocket.Dispose();
    }
}
