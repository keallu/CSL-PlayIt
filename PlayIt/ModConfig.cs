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
        public float GameSpeed { get; set; } = 1f;
        public float DayNightSpeed { get; set; } = 0f;
        public float RainIntensity { get; set; } = 0f;
        public float FogIntensity { get; set; } = 0f;
        public float CloudIntensity { get; set; } = 0f;
        public float NorthernLightsIntensity { get; set; } = 0f;
        public int TimeConvention { get; set; } = 1;
        public bool PauseDayNightCycleOnSimulationPause { get; set; } = false;
        public bool LockRainIntensity { get; set; } = false;
        public bool LockFogIntensity { get; set; } = false;
        public bool LockCloudIntensity { get; set; } = false;
        public bool LockNorthernLightsIntensity { get; set; } = false;

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