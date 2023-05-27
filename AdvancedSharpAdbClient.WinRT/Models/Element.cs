// <copyright file="Element.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using AdvancedSharpAdbClient.WinRT.Extensions;
using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Implement of screen element, likes Selenium.
    /// </summary>
    public sealed class Element
    {
        internal readonly AdvancedSharpAdbClient.Element element;

        /// <summary>
        /// Contains element coordinates.
        /// </summary>
        public Cords Cords
        {
            get => Cords.GetCords(element.Cords);
            set => element.Cords = value.cords;
        }

        /// <summary>
        /// Gets or sets element attributes.
        /// </summary>
        public IDictionary<string, string> Attributes
        {
            get => element.Attributes;
            set => element.Attributes = value.GetDictionary();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Element"/> class.
        /// </summary>
        /// <param name="client">The current ADB client that manages the connection.</param>
        /// <param name="device">The current device containing the element.</param>
        /// <param name="cords">Contains element coordinates .</param>
        /// <param name="attributes">Gets or sets element attributes.</param>
        public Element(AdbClient client, DeviceData device, Cords cords, IDictionary<string, string> attributes) =>
            element = new(client.adbClient, device.deviceData, cords.cords, attributes.GetDictionary());

        internal Element(AdvancedSharpAdbClient.Element element) => this.element = element;

        internal static Element GetElement(AdvancedSharpAdbClient.Element element) => new(element);

        /// <summary>
        /// Clicks on this coordinates.
        /// </summary>
        public void Click() => element.Click();

        /// <summary>
        /// Clicks on this coordinates.
        /// </summary>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        public IAsyncAction ClickAsync() => element.ClickAsync().AsAsyncAction();

        /// <summary>
        /// Send text to device. Doesn't support Russian.
        /// </summary>
        /// <param name="text">The text to send.</param>
        public void SendText(string text) => element.SendText(text);

        /// <summary>
        /// Send text to device. Doesn't support Russian.
        /// </summary>
        /// <param name="text">The text to send.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        public IAsyncAction SendTextAsync(string text) => element.SendTextAsync(text).AsAsyncAction();

        /// <summary>
        /// Clear the input text. Use <see cref="IAdbClient.ClearInput(DeviceData, int)"/> if the element is focused.
        /// </summary>
        public void ClearInput() => element.ClearInput();

        /// <summary>
        /// Clear the input text. Use <see cref="IAdbClient.ClearInputAsync(DeviceData, int)"/> if the element is focused.
        /// </summary>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        public IAsyncAction ClearInputAsync() => element.ClearInputAsync().AsAsyncAction();

        /// <summary>
        /// Clear the input text. Use <see cref="IAdbClient.ClearInput(DeviceData, int)"/> if the element is focused.
        /// </summary>
        /// <param name="charCount">The length of text to clear.</param>
        public void ClearInput(int charCount) => element.ClearInput(charCount);

        /// <summary>
        /// Clear the input text. Use <see cref="IAdbClient.ClearInputAsync(DeviceData, int)"/> if the element is focused.
        /// </summary>
        /// <param name="charCount">The length of text to clear.</param>
        /// <returns>A <see cref="IAsyncAction"/> which represents the asynchronous operation.</returns>
        public IAsyncAction ClearInputAsync(int charCount) => element.ClearInputAsync(charCount).AsAsyncAction();
    }
}
