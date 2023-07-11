// <copyright file="EventLogEntry.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AdvancedSharpAdbClient.WinRT.Logs
{
    /// <summary>
    /// Represents an entry in event buffer of the the Android log.
    /// </summary>
    /// <remarks><seealso href="https://android.googlesource.com/platform/system/core/+/master/include/log/log.h#482"/></remarks>
    public sealed class EventLogEntry : ILogEntry
    {
        internal readonly AdvancedSharpAdbClient.Logs.EventLogEntry eventLogEntry;

        /// <summary>
        /// Gets or sets the process ID of the code that generated the log message.
        /// </summary>
        public int ProcessId
        {
            get => eventLogEntry.ProcessId;
            set => eventLogEntry.ProcessId = value;
        }

        /// <summary>
        /// Gets or sets the thread ID of the code that generated the log message.
        /// </summary>
        public int ThreadId
        {
            get => eventLogEntry.ThreadId;
            set => eventLogEntry.ThreadId = value;
        }

        /// <summary>
        /// Gets or sets the date and time at which the message was logged.
        /// </summary>
        public DateTimeOffset TimeStamp
        {
            get => eventLogEntry.TimeStamp;
            set => eventLogEntry.TimeStamp = value;
        }

        /// <summary>
        /// Gets or sets the log id (v3) of the payload effective UID of logger (v2);
        /// this value is not available for v1 entries.
        /// </summary>
        public uint Id
        {
            get => eventLogEntry.Id;
            set => eventLogEntry.Id = value;
        }

        /// <summary>
        /// Gets or sets the entry's payload.
        /// </summary>
        public byte[] Data
        {
            get => eventLogEntry.Data;
            set => eventLogEntry.Data = value;
        }

        /// <summary>
        /// Gets or sets the 4 bytes integer key from <c>"/system/etc/event-log-tags"</c> file.
        /// </summary>
        public int Tag
        {
            get => eventLogEntry.Tag;
            set => eventLogEntry.Tag = value;
        }

        /// <summary>
        /// Gets or sets the values of this event log entry.
        /// </summary>
        public IList<object> Values
        {
            get => eventLogEntry.Values;
            set => eventLogEntry.Values = new Collection<object>(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLogEntry"/> class.
        /// </summary>
        public EventLogEntry() => eventLogEntry = new();

        internal EventLogEntry(AdvancedSharpAdbClient.Logs.EventLogEntry eventLogEntry) => this.eventLogEntry = eventLogEntry;
    }
}
