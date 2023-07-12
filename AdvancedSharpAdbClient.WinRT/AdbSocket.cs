// <copyright file="AdbSocket.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// <para>Implements a client for the Android Debug Bridge client-server protocol. Using the client, you
    /// can send messages to and receive messages from the Android Debug Bridge.</para>
    /// <para>The <see cref="AdbSocket"/> class implements the raw messaging protocol; that is,
    /// sending and receiving messages. For interacting with the services the Android Debug
    /// Bridge exposes, use the <see cref="AdbClient"/>.</para>
    /// <para>For more information about the protocol that is implemented here, see chapter
    /// II Protocol Details, section 1. Client &lt;-&gt;Server protocol at
    /// <see href="https://android.googlesource.com/platform/system/core/+/master/adb/OVERVIEW.TXT"/>.</para>
    /// </summary>
    public sealed class AdbSocket
    {
        internal readonly AdvancedSharpAdbClient.AdbSocket adbSocket;
    }
}
