// <copyright file="FramebufferHeader.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
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
    /// Whenever the <c>framebuffer:</c> service is invoked, the adb server responds with the contents
    /// of the framebuffer, prefixed with a <see cref="FramebufferHeader"/> object that contains more
    /// information about the framebuffer.
    /// </summary>
    public sealed class FramebufferHeader
    {
        internal AdvancedSharpAdbClient.FramebufferHeader framebufferHeader;

        /// <summary>
        /// Gets or sets the version of the framebuffer structure.
        /// </summary>
        public uint Version
        {
            get => framebufferHeader.Version;
            set => framebufferHeader.Version = value;
        }

        /// <summary>
        /// Gets or sets the number of bytes per pixel. Usual values include 32 or 24.
        /// </summary>
        public uint Bpp
        {
            get => framebufferHeader.Bpp;
            set => framebufferHeader.Bpp = value;
        }

        /// <summary>
        /// Gets or sets the color space. Only available starting with <see cref="Version"/> 2.
        /// </summary>
        public uint ColorSpace
        {
            get => framebufferHeader.ColorSpace;
            set => framebufferHeader.ColorSpace = value;
        }

        /// <summary>
        /// Gets or sets the total size, in bits, of the framebuffer.
        /// </summary>
        public uint Size
        {
            get => framebufferHeader.Size;
            set => framebufferHeader.Size = value;
        }
        /// <summary>
        /// Gets or sets the width, in pixels, of the framebuffer.
        /// </summary>
        public uint Width
        {
            get => framebufferHeader.Width;
            set => framebufferHeader.Width = value;
        }

        /// <summary>
        /// Gets or sets the height, in pixels, of the framebuffer.
        /// </summary>
        public uint Height
        {
            get => framebufferHeader.Height;
            set => framebufferHeader.Height = value;
        }

        /// <summary>
        /// Gets or sets information about the red color channel.
        /// </summary>
        public ColorData Red
        {
            get => ColorData.GetColorData(framebufferHeader.Red);
            set => framebufferHeader.Red = value.colorData;
        }

        /// <summary>
        /// Gets or sets information about the blue color channel.
        /// </summary>
        public ColorData Blue
        {
            get => ColorData.GetColorData(framebufferHeader.Blue);
            set => framebufferHeader.Blue = value.colorData;
        }

        /// <summary>
        /// Gets or sets information about the green color channel.
        /// </summary>
        public ColorData Green
        {
            get => ColorData.GetColorData(framebufferHeader.Green);
            set => framebufferHeader.Green = value.colorData;
        }

        /// <summary>
        /// Gets or sets information about the alpha channel.
        /// </summary>
        public ColorData Alpha
        {
            get => ColorData.GetColorData(framebufferHeader.Alpha);
            set => framebufferHeader.Alpha = value.colorData;
        }

        /// <summary>
        /// Creates a new <see cref="FramebufferHeader"/> object based on a byte array which contains the data.
        /// </summary>
        /// <param name="data">The data that feeds the <see cref="FramebufferHeader"/> structure.</param>
        /// <returns>A new <see cref="FramebufferHeader"/> object.</returns>
        public static FramebufferHeader Read([ReadOnlyArray] byte[] data) => GetFramebufferHeader(AdvancedSharpAdbClient.FramebufferHeader.Read(data));

        /// <summary>
        /// Initializes a new instance of the <see cref="FramebufferHeader"/> class.
        /// </summary>
        public FramebufferHeader() => framebufferHeader = new();

        internal FramebufferHeader(AdvancedSharpAdbClient.FramebufferHeader framebufferHeader) => this.framebufferHeader = framebufferHeader;

        internal static FramebufferHeader GetFramebufferHeader(AdvancedSharpAdbClient.FramebufferHeader framebufferHeader) => new(framebufferHeader);

#if WINDOWS_UWP
        /// <summary>
        /// Converts a <see cref="byte"/> array containing the raw frame buffer data to a <see cref="WriteableBitmap"/>.
        /// </summary>
        /// <param name="buffer">The buffer containing the image data.</param>
        /// <param name="dispatcher">The target <see cref="CoreDispatcher"/> to invoke the code on.</param>
        /// <returns>
        /// A <see cref="WriteableBitmap"/> that represents the image contained in the frame buffer, or <see langword="null"/>
        /// if the framebuffer does not contain any data. This can happen when DRM is enabled on the device.
        /// </returns>
        [DefaultOverload]
        public IAsyncOperation<WriteableBitmap> ToBitmap([ReadOnlyArray] byte[] buffer, CoreDispatcher dispatcher) => framebufferHeader.ToBitmap(buffer, dispatcher).AsAsyncOperation();

        /// <summary>
        /// Converts a <see cref="byte"/> array containing the raw frame buffer data to a <see cref="WriteableBitmap"/>.
        /// </summary>
        /// <param name="buffer">The buffer containing the image data.</param>
        /// <param name="dispatcher">The target <see cref="DispatcherQueue"/> to invoke the code on.</param>
        /// <returns>
        /// A <see cref="WriteableBitmap"/> that represents the image contained in the frame buffer, or <see langword="null"/>
        /// if the framebuffer does not contain any data. This can happen when DRM is enabled on the device.
        /// </returns>
        [ContractVersion(typeof(UniversalApiContract), 327680u)]
        public IAsyncOperation<WriteableBitmap> ToBitmap([ReadOnlyArray] byte[] buffer, DispatcherQueue dispatcher) => framebufferHeader.ToBitmap(buffer, dispatcher).AsAsyncOperation();

        /// <summary>
        /// Converts a <see cref="byte"/> array containing the raw frame buffer data to a <see cref="WriteableBitmap"/>.
        /// </summary>
        /// <param name="buffer">The buffer containing the image data.</param>
        /// <returns>
        /// A <see cref="WriteableBitmap"/> that represents the image contained in the frame buffer, or <see langword="null"/>
        /// if the framebuffer does not contain any data. This can happen when DRM is enabled on the device.
        /// </returns>
        public IAsyncOperation<WriteableBitmap> ToBitmap([ReadOnlyArray] byte[] buffer) => framebufferHeader.ToBitmap(buffer).AsAsyncOperation();
#endif
    }
}
