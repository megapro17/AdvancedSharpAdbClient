﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Windows.ApplicationModel;

namespace AdvancedSharpAdbClient.WinRT.Extensions
{
    internal static class Utilities
    {
        public static Version GetVersion(this PackageVersion version)
        {
            return new(version.Major, version.Minor, version.Build, version.Revision);
        }

        public static PackageVersion GetPackageVersion(this Version version)
        {
            return new()
            {
                Major = (ushort)version.Major,
                Minor = (ushort)version.Minor,
                Build = (ushort)version.Build,
                Revision = (ushort)version.Revision
            };
        }

        public static Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(this IDictionary<TKey, TValue> enumerable)
        {
            if (enumerable is Dictionary<TKey, TValue> dictionary)
            {
                return dictionary;
            }
            else
            {
                dictionary = new();
                foreach (KeyValuePair<TKey, TValue> keyValuePair in enumerable)
                {
                    dictionary.Add(keyValuePair.Key, keyValuePair.Value);
                }
                return dictionary;
            }
        }

        public static CancellationToken GetCancellationToken(this TimeSpan timeSpan)
        {
            return new CancellationTokenSource(timeSpan).Token;
        }
    }
}
