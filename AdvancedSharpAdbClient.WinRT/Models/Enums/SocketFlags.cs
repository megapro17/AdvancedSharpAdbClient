// <copyright file="SocketFlags.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Specifies socket send and receive behaviors.
    /// </summary>
    [Flags]
    public enum SocketFlags
    {
        /// <summary>
        /// Use no flags for this call.
        /// </summary>
        None = 0,

        /// <summary>
        /// Process out-of-band data.
        /// </summary>
        OutOfBand = 1,

        /// <summary>
        /// Peek at the incoming message.
        /// </summary>
        Peek = 2,

        /// <summary>
        /// Send without using routing tables.
        /// </summary>
        DontRoute = 4,

        /// <summary>
        /// The message was too large to fit into the specified buffer and was truncated.
        /// </summary>
        Truncated = 256,

        /// <summary>
        /// Indicates that the control data did not fit into an internal 64-KB buffer and was truncated.
        /// </summary>
        ControlDataTruncated = 512,

        /// <summary>
        /// Indicates a broadcast packet.
        /// </summary>
        Broadcast = 1024,

        /// <summary>
        /// Indicates a multicast packet.
        /// </summary>
        Multicast = 2048,

        /// <summary>
        /// Partial send or receive for message.
        /// </summary>
        Partial = 32768
    }
}
