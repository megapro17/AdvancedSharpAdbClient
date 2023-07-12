// <copyright file="Framebuffer.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.System;
using Windows.UI.Core;

#if WINDOWS_UWP
using Windows.UI.Xaml.Media.Imaging;
#endif

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
        /// This property is set after you call <see cref="RefreshAsync(bool)"/>.
        /// </summary>
        public FramebufferHeader Header => FramebufferHeader.GetFramebufferHeader(framebuffer.Header);

        /// <summary>
        /// Gets the framebuffer data. You need to parse the <see cref="FramebufferHeader"/> to interpret this data (such as the color encoding).
        /// This property is set after you call <see cref="RefreshAsync(bool)"/>.
        /// </summary>
        public byte[] Data => framebuffer.Data;

        /// <summary>
        /// Refreshes the framebuffer: fetches the latest framebuffer data from the device. Access the <see cref="Header"/>
        /// and <see cref="Data"/> properties to get the updated framebuffer data.
        /// </summary>
        /// <param name="reset">Refreshes the header of framebuffer when <see langword="true"/>.</param>
        public void Refresh(bool reset) => framebuffer.Refresh(reset);

        /// <summary>
        /// Asynchronously refreshes the framebuffer: fetches the latest framebuffer data from the device. Access the <see cref="Header"/>
        /// and <see cref="Data"/> properties to get the updated framebuffer data.
        /// </summary>
        /// <param name="reset">Refreshes the header of framebuffer when <see langword="true"/>.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        public IAsyncAction RefreshAsync(bool reset) => AsyncInfo.Run((cancellationToken) => framebuffer.RefreshAsync(reset, cancellationToken));

#if WINDOWS_UWP
        /// <summary>
        /// Converts the framebuffer data to a <see cref="WriteableBitmap"/>.
        /// </summary>
        /// <returns>An <see cref="WriteableBitmap"/> which represents the framebuffer data.</returns>
        public IAsyncOperation<WriteableBitmap> ToBitmap() => framebuffer.ToBitmap().AsAsyncOperation();

        /// <summary>
        /// Converts the framebuffer data to a <see cref="WriteableBitmap"/>.
        /// </summary>
        /// <param name="dispatcher">The target <see cref="CoreDispatcher"/> to invoke the code on.</param>
        /// <returns>An <see cref="WriteableBitmap"/> which represents the framebuffer data.</returns>
        [DefaultOverload]
        public IAsyncOperation<WriteableBitmap> ToBitmap(CoreDispatcher dispatcher) => framebuffer.ToBitmap(dispatcher).AsAsyncOperation();

        /// <summary>
        /// Converts the framebuffer data to a <see cref="WriteableBitmap"/>.
        /// </summary>
        /// <param name="dispatcher">The target <see cref="DispatcherQueue"/> to invoke the code on.</param>
        /// <returns>An <see cref="WriteableBitmap"/> which represents the framebuffer data.</returns>
        [ContractVersion(typeof(UniversalApiContract), 327680u)]
        public IAsyncOperation<WriteableBitmap> ToBitmap(DispatcherQueue dispatcher) => framebuffer.ToBitmap(dispatcher).AsAsyncOperation();
#endif

        /// <inheritdoc/>
        public void Dispose()
        {
            framebuffer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
