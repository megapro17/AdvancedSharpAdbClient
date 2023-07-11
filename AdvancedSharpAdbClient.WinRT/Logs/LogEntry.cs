// <copyright file="LogEntry.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;

namespace AdvancedSharpAdbClient.WinRT.Logs
{
    /// <summary>
    /// A callback which will receive the event log messages as they are received.
    /// </summary>
    public delegate void MessageSink(ILogEntry logEntry);

    /// <summary>
    /// The user space structure for version 1 of the logger_entry ABI.
    /// This structure is returned to user space by the kernel logger
    /// driver unless an upgrade to a newer ABI version is requested.
    /// </summary>
    /// <remarks><seealso href="https://android.googlesource.com/platform/system/core/+/master/include/log/logger.h"/></remarks>
    public sealed class LogEntry : ILogEntry
    {
        internal readonly AdvancedSharpAdbClient.Logs.LogEntry logEntry;

        /// <summary>
        /// Gets or sets the process ID of the code that generated the log message.
        /// </summary>
        public int ProcessId
        {
            get => logEntry.ProcessId;
            set => logEntry.ProcessId = value;
        }

        /// <summary>
        /// Gets or sets the thread ID of the code that generated the log message.
        /// </summary>
        public int ThreadId
        {
            get => logEntry.ThreadId;
            set => logEntry.ThreadId = value;
        }

        /// <summary>
        /// Gets or sets the date and time at which the message was logged.
        /// </summary>
        public DateTimeOffset TimeStamp
        {
            get => logEntry.TimeStamp;
            set => logEntry.TimeStamp = value;
        }

        /// <summary>
        /// Gets or sets the log id (v3) of the payload effective UID of logger (v2);
        /// this value is not available for v1 entries.
        /// </summary>
        public uint Id
        {
            get => logEntry.Id;
            set => logEntry.Id = value;
        }

        /// <summary>
        /// Gets or sets the entry's payload.
        /// </summary>
        public byte[] Data
        {
            get => logEntry.Data;
            set => logEntry.Data = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        public LogEntry() => logEntry = new();

        internal LogEntry(AdvancedSharpAdbClient.Logs.LogEntry logEntry) => this.logEntry = logEntry;

        internal static ILogEntry GetLogEntry(AdvancedSharpAdbClient.Logs.LogEntry logEntry) =>
            logEntry is AdvancedSharpAdbClient.Logs.EventLogEntry eventLogEntry
                ? new EventLogEntry(eventLogEntry)
                : logEntry is AdvancedSharpAdbClient.Logs.AndroidLogEntry androidLogEntry
                    ? new AndroidLogEntry(androidLogEntry)
                    : new LogEntry(logEntry);
    }
}
