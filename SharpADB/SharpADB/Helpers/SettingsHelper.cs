using MetroLog;
using Microsoft.Toolkit.Uwp.Helpers;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using IObjectSerializer = Microsoft.Toolkit.Helpers.IObjectSerializer;

namespace SharpADB.Helpers
{
    public static partial class SettingsHelper
    {
        public const string ADBPath = nameof(ADBPath);
        public const string UpdateDate = nameof(UpdateDate);
        public const string IsFirstRun = nameof(IsFirstRun);
        public const string CurrentLanguage = nameof(CurrentLanguage);
        public const string SelectedAppTheme = nameof(SelectedAppTheme);

        public static Type Get<Type>(string key) => LocalObject.Read<Type>(key);
        public static void Set(string key, object value) => LocalObject.Save(key, value);
        public static Task<Type> GetFile<Type>(string key) => LocalObject.ReadFileAsync<Type>(key);
        public static Task SetFile(string key, object value) => LocalObject.CreateFileAsync(key, value);

        public static void SetDefaultSettings()
        {
            if (!LocalObject.KeyExists(ADBPath))
            {
                LocalObject.Save(ADBPath, Path.Combine(ApplicationData.Current.LocalFolder.Path, @"platform-tools\adb.exe"));
            }
            if (!LocalObject.KeyExists(UpdateDate))
            {
                LocalObject.Save(UpdateDate, new DateTime());
            }
            if (!LocalObject.KeyExists(IsFirstRun))
            {
                LocalObject.Save(IsFirstRun, true);
            }
            if (!LocalObject.KeyExists(CurrentLanguage))
            {
                LocalObject.Save(CurrentLanguage, LanguageHelper.AutoLanguageCode);
            }
            if (!LocalObject.KeyExists(SelectedAppTheme))
            {
                LocalObject.Save(SelectedAppTheme, ElementTheme.Default);
            }
        }
    }

    public static partial class SettingsHelper
    {
        public static readonly UISettings UISettings = new();
        public static readonly ILogManager LogManager = LogManagerFactory.CreateLogManager();
        public static OSVersion OperatingSystemVersion = SystemInformation.Instance.OperatingSystemVersion;
        private static readonly ApplicationDataStorageHelper LocalObject = ApplicationDataStorageHelper.GetCurrent(new SystemTextJsonObjectSerializer());

        static SettingsHelper() => SetDefaultSettings();
    }

    public class SystemTextJsonObjectSerializer : IObjectSerializer
    {
        // Specify your serialization settings
        private readonly JsonSerializerSettings settings = new() { DefaultValueHandling = DefaultValueHandling.Ignore };

        string IObjectSerializer.Serialize<T>(T value) => JsonConvert.SerializeObject(value, typeof(T), Formatting.Indented, settings);

        public T Deserialize<T>(string value) => JsonConvert.DeserializeObject<T>(value, settings);
    }
}
