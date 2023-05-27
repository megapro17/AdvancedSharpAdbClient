﻿// <copyright file="Framebuffer.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using AdvancedSharpAdbClient.WinRT.Extensions;
using System;
using Windows.Foundation;
using TimeSpan = System.TimeSpan;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Provides access to the framebuffer (that is, a copy of the image being displayed on the device screen).
    /// </summary>
    public sealed class Framebuffer : IDisposable
    {
        internal AdvancedSharpAdbClient.Framebuffer framebuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Framebuffer"/> class.
        /// </summary>
        /// <param name="device">The device for which to fetch the frame buffer.</param>
        /// <param name="client">A <see cref="AdbClient"/> which manages the connection with adb.</param>
        public Framebuffer(DeviceData device, AdbClient client) => framebuffer = new(device.deviceData, client.adbClient);

        internal Framebuffer(AdvancedSharpAdbClient.Framebuffer framebuffer) => this.framebuffer = framebuffer;

        internal static Framebuffer GetFramebuffer(AdvancedSharpAdbClient.Framebuffer framebuffer) => new(framebuffer);

        /// <summary>
        /// Gets the device for which to fetch the frame buffer.
        /// </summary>
        public DeviceData Device => DeviceData.GetDeviceData(framebuffer.Device);

        /// <summary>
        /// Gets the framebuffer header. The header contains information such as the width and height and the color encoding.
        /// This property is set after you call <see cref="RefreshAsync()"/>.
        /// </summary>
        public FramebufferHeader Header => FramebufferHeader.GetFramebufferHeader(framebuffer.Header);

        /// <summary>
        /// Gets the framebuffer data. You need to parse the <see cref="FramebufferHeader"/> to interpret this data (such as the color encoding).
        /// This property is set after you call <see cref="RefreshAsync()"/>.
        /// </summary>
        public byte[] Data => framebuffer.Data;

        /// <summary>
        /// Asynchronously refreshes the framebuffer: fetches the latest framebuffer data from the device. Access the <see cref="Header"/>
        /// and <see cref="Data"/> properties to get the updated framebuffer data.
        /// </summary>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        public IAsyncAction RefreshAsync() => framebuffer.RefreshAsync().AsAsyncAction();

        /// <inheritdoc/>
        private void Dispose(bool disposing) => framebuffer?.Dispose();

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
