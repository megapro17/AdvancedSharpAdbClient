// <copyright file="SyncProgressChangedEventArgs.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Provides data for the <see cref="SyncService.SyncProgressChanged"/> event.
    /// </summary>
    public sealed class SyncProgressChangedEventArgs
    {
        internal readonly AdvancedSharpAdbClient.SyncProgressChangedEventArgs syncProgressChangedEventArgs;

        /// <summary>
        /// Gets the number of progress percentage for the sync operation.
        /// </summary>
        public double ProgressPercentage => syncProgressChangedEventArgs.ProgressPercentage;

        /// <summary>
        /// Gets the number of bytes sync to the local computer.
        /// </summary>
        /// <value>An <see cref="long"/> representing the number of sync bytes.</value>
        public long ReceivedBytesSize => syncProgressChangedEventArgs.ReceivedBytesSize;

        /// <summary>
        /// Gets the total number of bytes for the sync operation.
        /// </summary>
        /// <value>An <see cref="long"/> representing the total size of the download, in bytes.</value>
        public long TotalBytesToReceive => syncProgressChangedEventArgs.TotalBytesToReceive;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncProgressChangedEventArgs"/> class.
        /// </summary>
        public SyncProgressChangedEventArgs()
        {
        }

        internal SyncProgressChangedEventArgs(AdvancedSharpAdbClient.SyncProgressChangedEventArgs syncProgressChangedEventArgs) => this.syncProgressChangedEventArgs = syncProgressChangedEventArgs;

        internal static SyncProgressChangedEventArgs GetSyncProgressChangedEventArgs(AdvancedSharpAdbClient.SyncProgressChangedEventArgs syncProgressChangedEventArgs) => new(syncProgressChangedEventArgs);
    }
}
