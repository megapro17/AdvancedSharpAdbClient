// <copyright file="CrossPlatformFunc.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;

namespace AdvancedSharpAdbClient.WinRT
{
    /// <summary>
    /// Determines whether the specified file exists.
    /// </summary>
    public delegate bool CheckFileExists(string arg);

    /// <summary>
    /// Runs process, invoking a specific command, and reads the standard output and standard error output.
    /// </summary>
    /// <returns>The return code of the process.</returns>
    public delegate int RunProcess(string filename, string command, IList<string> errorOutput, IList<string> standardOutput);

    /// <summary>
    /// Runs process, invoking a specific command, and reads the standard output and standard error output.
    /// </summary>
    /// <returns>The return code of the process.</returns>
    public delegate IAsyncOperation<int> RunProcessAsync(string filename, string command, IList<string> errorOutput, IList<string> standardOutput);

    /// <summary>
    /// The functions which are used by the <see cref="IAdbCommandLineClient"/> class, but which are platform-specific.
    /// </summary>
    public static class CrossPlatformFunc
    {
        /// <summary>
        /// Determines whether the specified file exists.
        /// </summary>
        public static CheckFileExists CheckFileExists
        {
            get => (arg) => AdvancedSharpAdbClient.CrossPlatformFunc.CheckFileExists(arg);
            set => AdvancedSharpAdbClient.CrossPlatformFunc.CheckFileExists = (arg) => value(arg);
        }

        /// <summary>
        /// Runs process, invoking a specific command, and reads the standard output and standard error output.
        /// </summary>
        /// <returns>The return code of the process.</returns>
        public static RunProcess RunProcess
        {
            get => (filename, command, errorOutput, standardOutput) => AdvancedSharpAdbClient.CrossPlatformFunc.RunProcess(filename, command, errorOutput is List<string> errorOutputList ? errorOutputList : errorOutput.ToList(), standardOutput is List<string> standardOutputList ? standardOutputList : standardOutput.ToList());
            set => AdvancedSharpAdbClient.CrossPlatformFunc.RunProcess = (filename, command, errorOutput, standardOutput) => value(filename, command, errorOutput, standardOutput);
        }

        /// <summary>
        /// Runs process, invoking a specific command, and reads the standard output and standard error output.
        /// </summary>
        /// <returns>The return code of the process.</returns>
        public static RunProcessAsync RunProcessAsync
        {
            get => (filename, command, errorOutput, standardOutput) => AsyncInfo.Run((cancellationToken) => AdvancedSharpAdbClient.CrossPlatformFunc.RunProcessAsync(filename, command, errorOutput is List<string> errorOutputList ? errorOutputList : errorOutput.ToList(), standardOutput is List<string> standardOutputList ? standardOutputList : standardOutput.ToList(), cancellationToken));
            set => AdvancedSharpAdbClient.CrossPlatformFunc.RunProcessAsync = (filename, command, errorOutput, standardOutput, cancellationToken) => value(filename, command, errorOutput, standardOutput).AsTask(cancellationToken);
        }
    }
}
