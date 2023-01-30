namespace PlayIt
{
    [ConfigurationPath("PlayItConfig.xml")]
    public class ModConfig
    {
        public bool ConfigUpdated { get; set; }
        public bool ShowButton { get; set; } = true;
        public float ButtonPositionX { get; set; } = 0f;
        public float ButtonPositionY { get; set; } = 0f;
        public bool ShowPanel { get; set; } = true;
        public float PanelPositionX { get; set; } = 0f;
        public float PanelPositionY { get; set; } = 0f;
        public bool ShowClock { get; set; } = false;
        public float ClockPositionX { get; set; } = 0f;
        public float ClockPositionY { get; set; } = 0f;
        public float Latitude { get; set; } = float.NaN;
        public float Longitude { get; set; } = float.NaN;
        public float GameSpeed { get; set; } = 1f;
        public float DayNightSpeed { get; set; } = 0f;
        public float RainIntensity { get; set; } = 0f;
        public float FogIntensity { get; set; } = 0f;
        public float CloudIntensity { get; set; } = 0f;
        public float NorthernLightsIntensity { get; set; } = 0f;
        public int DegreeConvention { get; set; } = 1;
        public int TimeConvention { get; set; } = 1;
        public bool ShowSpeedInPercentages { get; set; } = false;
        public bool PauseDayNightCycleOnSimulationPause { get; set; } = false;
        public bool LockRainIntensity { get; set; } = false;
        public bool LockFogIntensity { get; set; } = false;
        public bool LockCloudIntensity { get; set; } = false;
        public bool LockNorthernLightsIntensity { get; set; } = false;
        public bool ShowLatitudeAndLongitudeInClockPanel { get; set; } = false;
        public bool ShowSpeedInClockPanel { get; set; } = false;
        public int TextColorInClockPanel { get; set; } = 0;
        public bool UseOutlineInClockPanel { get; set; } = false;
        public int OutlineColorInClockPanel { get; set; } = 0;
        public bool KeymappingsEnabled { get; set; } = false;
        public int KeymappingsIncreaseLatitude { get; set; } = 0;
        public int KeymappingsDecreaseLatitude { get; set; } = 0;
        public int KeymappingsIncreaseLongitude { get; set; } = 0;
        public int KeymappingsDecreaseLongitude { get; set; } = 0;
        public int KeymappingsIncreaseGameSpeed { get; set; } = 0;
        public int KeymappingsDecreaseGameSpeed { get; set; } = 0;
        public int KeymappingsIncreaseDayNightSpeed { get; set; } = 0;
        public int KeymappingsDecreaseDayNightSpeed { get; set; } = 0;
        public int KeymappingsForwardTimeOfDay { get; set; } = 0;
        public int KeymappingsBackwardTimeOfDay { get; set; } = 0;

        private static ModConfig instance;

        public static ModConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Configuration<ModConfig>.Load();
                }

                return instance;
            }
        }

        public void Save()
        {
            Configuration<ModConfig>.Save();
            ConfigUpdated = true;
        }
    }
}