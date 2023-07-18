// <copyright file="AdbServerFeatures.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Lists features which an Android Debug Bridge can support.
    /// </summary>
    public static class AdbServerFeatures
    {
        /// <summary>
        /// The server supports the shell protocol.
        /// </summary>
        public static string Shell2 { get; } = AdvancedSharpAdbClient.AdbServerFeatures.Shell2;

        /// <summary>
        /// The server supports the <c>cmd</c> command.
        /// </summary>
        public static string Cmd { get; } = AdvancedSharpAdbClient.AdbServerFeatures.Cmd;

        /// <summary>
        /// The server supports the stat2 protocol.
        /// </summary>
        public static string Stat2 { get; } = AdvancedSharpAdbClient.AdbServerFeatures.Stat2;

        /// <summary>
        /// The server supports libusb.
        /// </summary>
        public static string LibUsb { get; } = AdvancedSharpAdbClient.AdbServerFeatures.LibUsb;

        /// <summary>
        /// The server supports <c>push --sync</c>.
        /// </summary>
        public static string PushSync { get; } = AdvancedSharpAdbClient.AdbServerFeatures.PushSync;
    }
}
