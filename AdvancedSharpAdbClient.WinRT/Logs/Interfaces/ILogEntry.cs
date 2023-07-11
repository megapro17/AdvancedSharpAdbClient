// <copyright file="ILogEntry.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;

namespace AdvancedSharpAdbClient.WinRT.Logs
{
    /// <summary>
    /// The user space structure for version 1 of the logger_entry ABI.
    /// This structure is returned to user space by the kernel logger
    /// driver unless an upgrade to a newer ABI version is requested.
    /// </summary>
    /// <remarks><seealso href="https://android.googlesource.com/platform/system/core/+/master/include/log/logger.h"/></remarks>
    public interface ILogEntry
    {
        /// <summary>
        /// Gets or sets the process ID of the code that generated the log message.
        /// </summary>
        int ProcessId { get; set; }

        /// <summary>
        /// Gets or sets the thread ID of the code that generated the log message.
        /// </summary>
        int ThreadId { get; set; }

        /// <summary>
        /// Gets or sets the date and time at which the message was logged.
        /// </summary>
        DateTimeOffset TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the log id (v3) of the payload effective UID of logger (v2);
        /// this value is not available for v1 entries.
        /// </summary>
        uint Id { get; set; }

        /// <summary>
        /// Gets or sets the entry's payload.
        /// </summary>
        byte[] Data { get; set; }
    }
}
