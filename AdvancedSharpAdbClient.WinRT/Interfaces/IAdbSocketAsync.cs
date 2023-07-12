// <copyright file="IAdbSocketAsync.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Provides a common interface for any class that acts as a client for the
    /// Android Debug Bridge.
    /// </summary>
    public partial interface IAdbSocketAsync
    {
        /// <summary>
        /// Sends the specified number of bytes of data to a <see cref="IAdbSocket"/>,
        /// </summary>
        /// <param name="data">A <see cref="byte"/> array that acts as a buffer, containing the data to send.</param>
        /// <param name="length">The number of bytes to send.</param>
        /// <returns>A <see cref="IAsyncAction"/> that represents the asynchronous operation.</returns>
        IAsyncAction SendAsync([ReadOnlyArray] byte[] data, int length);

        /// <summary>
        /// Sends the specified number of bytes of data to a <see cref="IAdbSocket"/>,
        /// </summary>
        /// <param name="data">A <see cref="byte"/> array that acts as a buffer, containing the data to send.</param>
        /// <param name="offset">The index of the first byte in the array to send.</param>
        /// <param name="length">The number of bytes to send.</param>
        /// <returns>A <see cref="IAsyncAction"/> that represents the asynchronous operation.</returns>
        IAsyncAction SendAsync([ReadOnlyArray] byte[] data, int offset, int length);

        /// <summary>
        /// Sends a sync request to the device.
        /// </summary>
        /// <param name="command" >The command to send.</param>
        /// <param name="path">The path of the file on which the command should operate.</param>
        /// <param name="permissions">If the command is a <see cref="SyncCommand.SEND"/> command, the permissions to assign to the newly created file.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        IAsyncAction SendSyncRequestAsync(SyncCommand command, string path, int permissions);

        /// <summary>
        /// Sends a sync request to the device.
        /// </summary>
        /// <param name="command">The command to send.</param>
        /// <param name="path">The path of the file on which the command should operate.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        [DefaultOverload]
        IAsyncAction SendSyncRequestAsync(SyncCommand command, string path);

        /// <summary>
        /// Sends a sync request to the device.
        /// </summary>
        /// <param name="command">The command to send.</param>
        /// <param name="length">The length of the data packet that follows.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        IAsyncAction SendSyncRequestAsync(SyncCommand command, int length);

        /// <summary>
        /// Asynchronously sends a request to the Android Debug Bridge.To read the response, call
        /// <see cref="ReadAdbResponseAsync()"/>.
        /// </summary>
        /// <param name="request">The request to send.</param>
        /// <returns>A <see cref="IAsyncAction"/> that represents the asynchronous operation.</returns>
        IAsyncAction SendAdbRequestAsync(string request);

        /// <summary>
        /// Reads a <see cref="string"/> from an <see cref="IAdbSocket"/> instance when
        /// the connection is in sync mode.
        /// </summary>
        /// <param name="data" >The buffer to store the read data into.</param>
        /// <returns>A <see cref="IAsyncOperation{Int32}"/> that represents the asynchronous operation.</returns>
        IAsyncOperation<int> ReadAsync([WriteOnlyArray] byte[] data);

        /// <summary>
        /// Receives data from a <see cref="IAdbSocket"/> into a receive buffer.
        /// </summary>
        /// <param name="data">An array of type <see cref="byte"/> that is the storage location for the received data.</param>
        /// <param name="length">The number of bytes to receive.</param>
        /// <remarks>Cancelling the task will also close the socket.</remarks>
        /// <returns>A <see cref="IAsyncOperation{Int32}"/> that represents the asynchronous operation. The result value of the task contains the number of bytes received.</returns>
        IAsyncOperation<int> ReadAsync([WriteOnlyArray] byte[] data, int length);

        /// <summary>
        /// Asynchronously reads a <see cref="string"/> from an <see cref="IAdbSocket"/> instance.
        /// </summary>
        /// <returns>A <see cref="IAsyncOperation{String}"/> that represents the asynchronous operation. The return value of the task is the<see cref="string"/> received from the <see cref = "IAdbSocket"/>.</returns>
        IAsyncOperation<string> ReadStringAsync();

        /// <summary>
        /// Reads a <see cref="string"/> from an <see cref="IAdbSocket"/> instance when
        /// the connection is in sync mode.
        /// </summary>
        /// <returns>A <see cref="IAsyncOperation{String}"/> that represents the asynchronous operation. The return value of the task is the <see cref="string"/> received from the <see cref = "IAdbSocket"/>.</returns>
        IAsyncOperation<string> ReadSyncStringAsync();

        /// <summary>
        /// Reads the response to a sync command.
        /// </summary>
        /// <returns>A <see cref="IAsyncOperation{SyncCommand}"/> that represents the asynchronous operation. The return value of the task is the response that was sent by the device.</returns>
        IAsyncOperation<SyncCommand> ReadSyncResponseAsync();

        /// <summary>
        /// Asynchronously receives an <see cref="AdbResponse"/> message, and throws an error
        /// if the message does not indicate success.
        /// </summary>
        /// <returns>A <see cref="AdbResponse"/> object that represents the response from the Android Debug Bridge.</returns>
        IAsyncOperation<AdbResponse> ReadAdbResponseAsync();

        /// <summary>
        /// Ask to switch the connection to the device/emulator identified by
        /// <paramref name="device"/>. After this request, every client request will
        /// be sent directly to the adbd daemon running on the device.
        /// </summary>
        /// <param name="device">The device to which to connect.</param>
        /// <returns>A <see cref="IAsyncAction"/> that represents the asynchronous operation.</returns>
        /// <remarks>If <paramref name="device"/> is <see langword="null"/>, this method does nothing.</remarks>
        IAsyncAction SetDeviceAsync(DeviceData device);
    }
}
