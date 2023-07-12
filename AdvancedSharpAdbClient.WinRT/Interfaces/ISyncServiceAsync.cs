// <copyright file="ISyncServiceAsync.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Interface containing methods for file synchronization.
    /// </summary>
    public interface ISyncServiceAsync
    {
        /// <summary>
        /// Pushes (uploads) a file to the remote device.
        /// </summary>
        /// <param name="stream">A <see cref="IInputStream"/> that contains the contents of the file.</param>
        /// <param name="remotePath">The path, on the device, to which to push the file.</param>
        /// <param name="permissions">The permission octet that contains the permissions of the newly created file on the device.</param>
        /// <param name="timestamp">The time at which the file was last modified.</param>
        /// <returns>A <see cref="IAsyncActionWithProgress{Int32}"/> which represents the asynchronous operation.</returns>
        IAsyncActionWithProgress<int> PushAsync(IInputStream stream, string remotePath, int permissions, DateTimeOffset timestamp);

        /// <summary>
        /// Pulls (downloads) a file from the remote device.
        /// </summary>
        /// <param name="remotePath">The path, on the device, of the file to pull.</param>
        /// <param name="stream">A <see cref="IOutputStream"/> that will receive the contents of the file.</param>
        /// <returns>A <see cref="IAsyncActionWithProgress{Int32}"/> which represents the asynchronous operation.</returns>
        IAsyncActionWithProgress<int> PullAsync(string remotePath, IOutputStream stream);

        /// <summary>
        /// Returns information about a file on the device.
        /// </summary>
        /// <param name="remotePath">The path of the file on the device.</param>
        /// <returns>A <see cref="IAsyncOperation{FileStatistics}"/> which return a <see cref="FileStatistics"/> object that contains information about the file.</returns>
        IAsyncOperation<FileStatistics> StatAsync(string remotePath);

        /// <summary>
        /// Lists the contents of a directory on the device.
        /// </summary>
        /// <param name="remotePath">The path to the directory on the device.</param>
        /// <returns>A <see cref="IAsyncOperation{IEnumerable}"/> which return for each child item of the directory, a <see cref="FileStatistics"/> object with information of the item.</returns>
        IAsyncOperation<IEnumerable<FileStatistics>> GetDirectoryListingAsync(string remotePath);

        /// <summary>
        /// Opens this connection.
        /// </summary>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        IAsyncAction OpenAsync();
    }
}
