// <copyright file="AdbResponse.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// An Adb Communication Response.
    /// </summary>
    public sealed class AdbResponse
    {
        internal AdvancedSharpAdbClient.AdbResponse adbResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdbResponse"/> class.
        /// </summary>
        public AdbResponse() => adbResponse = new();

        internal AdbResponse(AdvancedSharpAdbClient.AdbResponse adbResponse) => this.adbResponse = adbResponse;

        internal static AdbResponse GetAdbResponse(AdvancedSharpAdbClient.AdbResponse adbResponse) => new(adbResponse);

        /// <summary>
        /// Gets a <see cref="AdbResponse"/> that represents the OK response sent by ADB.
        /// </summary>
        public static AdbResponse OK => GetAdbResponse(AdvancedSharpAdbClient.AdbResponse.OK);

        /// <summary>
        /// Gets or sets a value indicating whether the IO communication was a success.
        /// </summary>
        /// <value><see langword="true"/> if successful; otherwise, <see langword="false"/>.</value>
        public bool IOSuccess
        {
            get => adbResponse.IOSuccess;
            set => adbResponse.IOSuccess = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AdbResponse"/> is okay.
        /// </summary>
        /// <value><see langword="true"/> if okay; otherwise, <see langword="false"/>.</value>
        public bool Okay
        {
            get => adbResponse.Okay;
            set => adbResponse.Okay = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AdbResponse"/> is timeout.
        /// </summary>
        /// <value><see langword="true"/> if timeout; otherwise, <see langword="false"/>.</value>
        public bool Timeout
        {
            get => adbResponse.Timeout;
            set => adbResponse.Timeout = value;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get => adbResponse.Message;
            set => adbResponse.Message = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AdbResponse"/> class, based on an
        /// error message returned by adb.
        /// </summary>
        /// <param name="message">The error message returned by adb.</param>
        /// <returns>A new <see cref="AdbResponse"/> object that represents the error.</returns>
        public static AdbResponse FromError(string message) => GetAdbResponse(AdvancedSharpAdbClient.AdbResponse.FromError(message));

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="AdbResponse"/> object.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="AdbResponse"/> object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) => obj is AdbResponse other && adbResponse.Equals(other.adbResponse);

        /// <summary>
        /// Gets the hash code for the current <see cref="AdbResponse"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="AdbResponse"/>.</returns>
        public override int GetHashCode() => adbResponse.GetHashCode();

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="AdbResponse"/>.
        /// </summary>
        /// <returns><c>OK</c> if the response is an OK response, or <c>Error: {Message}</c> if the response indicates an error.</returns>
        public override string ToString() => adbResponse.ToString();
    }
}
