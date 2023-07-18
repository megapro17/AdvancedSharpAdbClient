// <copyright file="DateTimeHelper.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Provides helper methods for working with Unix-based date formats.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Gets EPOCH time.
        /// </summary>
        public static DateTimeOffset Epoch { get; } = AdvancedSharpAdbClient.DateTimeHelper.Epoch;

        /// <summary>
        /// Converts a <see cref="DateTimeOffset"/> to the Unix equivalent.
        /// </summary>
        /// <param name="date">The date to convert to the Unix format.</param>
        /// <returns>A <see cref="long"/> that represents the date, in Unix format.</returns>
        public static long ToUnixEpoch(this DateTimeOffset date) => AdvancedSharpAdbClient.DateTimeHelper.ToUnixEpoch(date.DateTime);

        /// <summary>
        /// Converts a Unix equivalent to the <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="time">The Unix equivalent to convert to the date.</param>
        /// <returns>A <see cref="DateTimeOffset"/> that represents the date.</returns>
        public static DateTimeOffset ToDateTime(this long time) => AdvancedSharpAdbClient.DateTimeHelper.ToDateTime(time);
    }
}
