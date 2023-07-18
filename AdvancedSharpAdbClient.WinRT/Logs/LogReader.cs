// <copyright file="LogReader.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace AdvancedSharpAdbClient.WinRT.Logs
{
    /// <summary>
    /// Processes Android log files in binary format. You usually get the binary format by running <c>logcat -B</c>.
    /// </summary>
    public sealed class LogReader
    {
        internal readonly AdvancedSharpAdbClient.Logs.LogReader logReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogReader"/> class.
        /// </summary>
        /// <param name="stream">A <see cref="IRandomAccessStream"/> that contains the logcat data.</param>
        public LogReader(IRandomAccessStream stream) => logReader = new(stream.AsStream());

        /// <summary>
        /// Reads the next <see cref="ILogEntry"/> from the stream.
        /// </summary>
        /// <returns>A new <see cref="ILogEntry"/> object.</returns>
        public ILogEntry ReadEntry() => LogEntry.GetLogEntry(logReader.ReadEntry());

        /// <summary>
        /// Reads the next <see cref="ILogEntry"/> from the stream.
        /// </summary>
        /// <returns>A <see cref="IAsyncOperation{ILogEntry}"/> which return a new <see cref="ILogEntry"/> object.</returns>
        public IAsyncOperation<ILogEntry> ReadEntryAsync() => AsyncInfo.Run(async (cancellationToken) => LogEntry.GetLogEntry(await logReader.ReadEntryAsync(cancellationToken)));
    }
}
