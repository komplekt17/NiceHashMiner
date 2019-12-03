﻿using Newtonsoft.Json;
using NHM.Common.Enums;
using System.Globalization;
using System.IO;

namespace NHM.Common
{
    public static class BuildOptions
    {
        public class BuildOptionSettings
        {
            public BuildTag BUILD_TAG { get; set; } = BuildTag.PRODUCTION;
            public bool IS_PLUGINS_TEST_SOURCE { get; set; } = false;
            public bool CUSTOM_ENDPOINTS_ENABLED { get; set; } = false;
            // core settings
            public bool FORCE_MINING { get; set; } = false;
            public bool FORCE_PROFITABLE { get; set; } = false;
            public bool SHOW_TDP_SETTINGS { get; set; } = false;
        }

        static BuildOptions()
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Culture = CultureInfo.InvariantCulture
            };
            bool isSettingsLoaded = false;
            const string customSettingsFile = "build_settings.json";
            if (File.Exists(customSettingsFile))
            {
                var customSettings = JsonConvert.DeserializeObject<BuildOptionSettings>(File.ReadAllText(customSettingsFile), jsonSettings);
                if (customSettings != null)
                {
                    isSettingsLoaded = true;
                    BUILD_TAG = customSettings.BUILD_TAG;
                    IS_PLUGINS_TEST_SOURCE = customSettings.IS_PLUGINS_TEST_SOURCE;
                    CUSTOM_ENDPOINTS_ENABLED = customSettings.CUSTOM_ENDPOINTS_ENABLED;
                    FORCE_MINING = customSettings.FORCE_MINING;
                    FORCE_PROFITABLE = customSettings.FORCE_PROFITABLE;
                    SHOW_TDP_SETTINGS = customSettings.SHOW_TDP_SETTINGS;
                }
            }
            if (!isSettingsLoaded)
            {
                // create defaults
                var defaultCustomSettings = new BuildOptionSettings{};
                File.WriteAllText(customSettingsFile, JsonConvert.SerializeObject(defaultCustomSettings, Formatting.Indented));
            }
        }

        public static BuildTag BUILD_TAG { get; private set; } = BuildTag.PRODUCTION;

        public static bool IS_PLUGINS_TEST_SOURCE { get; private set; } = false;

        public static bool CUSTOM_ENDPOINTS_ENABLED { get; private set; } = false;
        // core settings
        public static bool FORCE_MINING { get; set; } = false;
        public static bool FORCE_PROFITABLE { get; set; } = false;
        public static bool SHOW_TDP_SETTINGS { get; set; } = false;
    }
}
