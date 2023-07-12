// <copyright file="ITcpSocket.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Provides an interface that allows access to the standard .NET <see cref="Socket"/>
    /// class. The main purpose of this interface is to enable mocking of the <see cref="Socket"/>
    /// in unit test scenarios.
    /// </summary>
    public interface ITcpSocketAsync
    {
        /// <summary>
        /// Asynchronously sends the specified number of bytes of data to a connected
        /// <see cref="ITcpSocket"/>, starting at the specified <paramref name="offset"/>,
        /// and using the specified <paramref name="socketFlags"/>.
        /// </summary>
        /// <param name="buffer">An array of type Byte that contains the data to be sent.</param>
        /// <param name="offset">The position in the data buffer at which to begin sending data.</param>
        /// <param name="size">The number of bytes to send.</param>
        /// <param name="socketFlags">A bitwise combination of the SocketFlags values.</param>
        /// <returns>The number of bytes sent to the Socket.</returns>
        IAsyncOperation<int> SendAsync([ReadOnlyArray] byte[] buffer, int offset, int size, SocketFlags socketFlags);

        /// <summary>
        /// Receives the specified number of bytes from a bound <see cref="ITcpSocket"/> into the specified offset position of the
        /// receive buffer, using the specified SocketFlags.
        /// </summary>
        /// <param name="buffer">An array of type Byte that is the storage location for received data.</param>
        /// <param name="offset">The location in buffer to store the received data.</param>
        /// <param name="size">The number of bytes to receive.</param>
        /// <param name="socketFlags">A bitwise combination of the SocketFlags values.</param>
        /// <remarks>Cancelling the task will also close the socket.</remarks>
        /// <returns>The number of bytes received.</returns>
        IAsyncOperation<int> ReceiveAsync([WriteOnlyArray] byte[] buffer, int offset, int size, SocketFlags socketFlags);
    }
}
