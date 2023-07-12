// <copyright file="TcpSocket.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Implements the <see cref="ITcpSocket" /> interface using the standard <see cref="Socket"/> class.
    /// </summary>
    public sealed class TcpSocket : ITcpSocket, ITcpSocketAsync
    {
        internal readonly AdvancedSharpAdbClient.TcpSocket tcpSocket;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpSocket"/> class.
        /// </summary>
        public TcpSocket() => tcpSocket = new();

        /// <inheritdoc/>
        public bool Connected => tcpSocket.Connected;

        /// <inheritdoc/>
        public int ReceiveBufferSize
        {
            get => tcpSocket.ReceiveBufferSize;
            set => tcpSocket.ReceiveBufferSize = value;
        }

        /// <inheritdoc/>
        public void Connect(string host, int port)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException(nameof(host));
            }

            string[] values = host.Split(':');

            DnsEndPoint EndPoint = values.Length <= 0
                ? throw new ArgumentNullException(nameof(host))
                : new DnsEndPoint(values[0], values.Length > 1 && int.TryParse(values[1], out int _port) ? _port : port);

            tcpSocket.Connect(EndPoint);
        }

        /// <inheritdoc/>
        public void Reconnect() => tcpSocket.Reconnect();

        /// <inheritdoc/>
        public void Dispose() => tcpSocket.Dispose();

        /// <inheritdoc/>
        public int Send([ReadOnlyArray] byte[] buffer, int offset, int size, SocketFlags socketFlags) => tcpSocket.Send(buffer, offset, size, (System.Net.Sockets.SocketFlags)socketFlags);

        /// <inheritdoc/>
        public IAsyncOperation<int> SendAsync([ReadOnlyArray] byte[] buffer, int offset, int size, SocketFlags socketFlags) => AsyncInfo.Run((cancellationToken) => tcpSocket.SendAsync(buffer, offset, size, (System.Net.Sockets.SocketFlags)socketFlags, cancellationToken));

        /// <inheritdoc/>
        public int Receive([WriteOnlyArray] byte[] buffer, int size, SocketFlags socketFlags) => tcpSocket.Receive(buffer, size, (System.Net.Sockets.SocketFlags)socketFlags);

        /// <inheritdoc/>
        public IAsyncOperation<int> ReceiveAsync([WriteOnlyArray] byte[] buffer, int offset, int size, SocketFlags socketFlags) => AsyncInfo.Run((cancellationToken) => tcpSocket.ReceiveAsync(buffer, offset, size, (System.Net.Sockets.SocketFlags)socketFlags, cancellationToken));

        /// <inheritdoc/>
        public IRandomAccessStream GetStream() => tcpSocket.GetStream().AsRandomAccessStream();
    }
}
