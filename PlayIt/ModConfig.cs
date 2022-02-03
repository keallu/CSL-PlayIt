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
        public float DayNightSpeed { get; set; } = 0f;
        public int TimeConvention { get; set; } = 1;

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

        public void Apply()
        {
            ConfigUpdated = true;
        }

        public void Save()
        {
            Configuration<ModConfig>.Save();
            ConfigUpdated = true;
        }
    }
}