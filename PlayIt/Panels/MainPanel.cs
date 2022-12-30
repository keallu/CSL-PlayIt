using ColossalFramework;
using ColossalFramework.UI;
using System;
using UnityEngine;
using PlayIt.Managers;

namespace PlayIt.Panels
{
    public class MainPanel : UIPanel
    {
        private bool _initialized;
        private float _timer;

        private UITextureAtlas _ingameAtlas;
        private UILabel _title;
        private UIButton _close;
        private UIDragHandle _dragHandle;
        private UITabstrip _tabstrip;
        private UITabContainer _tabContainer;
        private UIButton _templateButton;

        private UILabel _timeGameSpeedSliderLabel;
        private UILabel _timeGameSpeedSliderNumeral;
        private UISlider _timeGameSpeedSlider;
        private UILabel _timeDayNightSpeedSliderLabel;
        private UILabel _timeDayNightSpeedSliderNumeral;
        private UISlider _timeDayNightSpeedSlider;
        private UILabel _timeTimeOfDaySliderLabel;
        private UILabel _timeTimeOfDaySliderNumeral;
        private UISlider _timeTimeOfDaySlider;
        private UIPanel _timeDayNightCyclePanel;
        private UILabel _timeDayNightCycleLabel;
        private UIButton _timeDayNightCycleButton;

        private UILabel _weatherRainIntensitySliderLabel;
        private UILabel _weatherRainIntensitySliderNumeral;
        private UISlider _weatherRainIntensitySlider;
        private UILabel _weatherFogIntensitySliderLabel;
        private UILabel _weatherFogIntensitySliderNumeral;
        private UISlider _weatherFogIntensitySlider;
        private UILabel _weatherCloudIntensitySliderLabel;
        private UILabel _weatherCloudIntensitySliderNumeral;
        private UISlider _weatherCloudIntensitySlider;
        private UILabel _weatherNorthernLightsIntensitySliderLabel;
        private UILabel _weatherNorthernLightsIntensitySliderNumeral;
        private UISlider _weatherNorthernLightsIntensitySlider;
        private UIPanel _weatherDynamicWeatherPanel;
        private UILabel _weatherDynamicWeatherLabel;
        private UIButton _weatherDynamicWeatherButton;

        private UILabel _advancedTimeTitle;
        private UILabel _advancedTimeConventionDropDownLabel;
        private UIDropDown _advancedTimeConventionDropDown;
        private UICheckBox _advancedShowSpeedInPercentagesCheckBox;
        private UICheckBox _advancedPauseDayNightCycleOnSimulationPauseCheckBox;
        private UILabel _advancedWeatherTitle;
        private UICheckBox _advancedLockRainIntensityCheckBox;
        private UICheckBox _advancedLockFogIntensityCheckBox;
        private UICheckBox _advancedLockCloudIntensityCheckBox;
        private UICheckBox _advancedLockNorthernLightsIntensityCheckBox;

        public override void Awake()
        {
            base.Awake();

            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] MainPanel:Awake -> Exception: " + e.Message);
            }
        }

        public override void Start()
        {
            base.Start();

            try
            {
                if (ModConfig.Instance.PanelPositionX == 0f && ModConfig.Instance.PanelPositionY == 0f)
                {
                    ModProperties.Instance.ResetPanelPosition();
                }

                _ingameAtlas = ResourceLoader.GetAtlas("Ingame");

                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] MainPanel:Start -> Exception: " + e.Message);
            }
        }

        public override void Update()
        {
            base.Update();

            try
            {
                if (!_initialized)
                {
                    UpdateUI();

                    _initialized = true;
                }

                _timer += Time.deltaTime;

                if (_timer > 1)
                {
                    _timer -= 1;

                    UpdateWeather();

                    if (isVisible)
                    {
                        RefreshGameSpeed();
                        RefreshTimeOfDay();
                        RefreshWeather();

                        _timeDayNightCyclePanel.isVisible = !ModUtils.GetDayNightCycleInOptionsGameplayPanel();
                        _weatherDynamicWeatherPanel.isVisible = !ModUtils.GetDynamicWeatherInOptionsGameplayPanel();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] MainPanel:Update -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            try
            {
                DestroyGameObject(_title);
                DestroyGameObject(_close);
                DestroyGameObject(_dragHandle);
                DestroyGameObject(_tabstrip);
                DestroyGameObject(_tabContainer);
                DestroyGameObject(_templateButton);
                DestroyGameObject(_timeGameSpeedSliderLabel);
                DestroyGameObject(_timeGameSpeedSliderNumeral);
                DestroyGameObject(_timeGameSpeedSlider);
                DestroyGameObject(_timeDayNightSpeedSliderLabel);
                DestroyGameObject(_timeDayNightSpeedSliderNumeral);
                DestroyGameObject(_timeDayNightSpeedSlider);
                DestroyGameObject(_timeTimeOfDaySliderLabel);
                DestroyGameObject(_timeTimeOfDaySliderNumeral);
                DestroyGameObject(_timeTimeOfDaySlider);
                DestroyGameObject(_timeDayNightCyclePanel);
                DestroyGameObject(_timeDayNightCycleLabel);
                DestroyGameObject(_timeDayNightCycleButton);
                DestroyGameObject(_weatherRainIntensitySliderLabel);
                DestroyGameObject(_weatherRainIntensitySliderNumeral);
                DestroyGameObject(_weatherRainIntensitySlider);
                DestroyGameObject(_weatherFogIntensitySliderLabel);
                DestroyGameObject(_weatherFogIntensitySliderNumeral);
                DestroyGameObject(_weatherFogIntensitySlider);
                DestroyGameObject(_weatherCloudIntensitySliderLabel);
                DestroyGameObject(_weatherCloudIntensitySliderNumeral);
                DestroyGameObject(_weatherCloudIntensitySlider);
                DestroyGameObject(_weatherNorthernLightsIntensitySliderLabel);
                DestroyGameObject(_weatherNorthernLightsIntensitySliderNumeral);
                DestroyGameObject(_weatherNorthernLightsIntensitySlider);
                DestroyGameObject(_weatherDynamicWeatherPanel);
                DestroyGameObject(_weatherDynamicWeatherLabel);
                DestroyGameObject(_weatherDynamicWeatherButton);
                DestroyGameObject(_advancedTimeTitle);
                DestroyGameObject(_advancedTimeConventionDropDownLabel);
                DestroyGameObject(_advancedTimeConventionDropDown);
                DestroyGameObject(_advancedShowSpeedInPercentagesCheckBox);
                DestroyGameObject(_advancedPauseDayNightCycleOnSimulationPauseCheckBox);
                DestroyGameObject(_advancedWeatherTitle);
                DestroyGameObject(_advancedLockRainIntensityCheckBox);
                DestroyGameObject(_advancedLockFogIntensityCheckBox);
                DestroyGameObject(_advancedLockCloudIntensityCheckBox);
                DestroyGameObject(_advancedLockNorthernLightsIntensityCheckBox);
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] MainPanel:OnDestroy -> Exception: " + e.Message);
            }
        }

        private void DestroyGameObject(UIComponent component)
        {
            if (component != null)
            {
                Destroy(component.gameObject);
            }
        }

        public void ForceUpdateUI()
        {
            UpdateUI();
        }

        private void CreateUI()
        {
            try
            {
                name = "PlayItMainPanel";
                backgroundSprite = "MenuPanel2";
                clipChildren = true;
                isVisible = false;
                width = 400f;
                height = 400f;

                _title = UIUtils.CreateMenuPanelTitle(this, _ingameAtlas, "Play It!");
                _title.relativePosition = new Vector3(width / 2f - _title.width / 2f, 15f);

                _close = UIUtils.CreateMenuPanelCloseButton(this, _ingameAtlas);
                _close.relativePosition = new Vector3(width - 37f, 3f);
                _close.eventClick += (component, eventParam) =>
                {
                    if (!eventParam.used)
                    {
                        _close.parent.Hide();
                        ModConfig.Instance.ShowPanel = false;
                        ModConfig.Instance.Save();

                        eventParam.Use();
                    }
                };

                _dragHandle = UIUtils.CreateMenuPanelDragHandle(this);
                _dragHandle.width = width - 40f;
                _dragHandle.height = 40f;
                _dragHandle.relativePosition = Vector3.zero;
                _dragHandle.eventMouseUp += (component, eventParam) =>
                {
                    ModConfig.Instance.PanelPositionX = absolutePosition.x;
                    ModConfig.Instance.PanelPositionY = absolutePosition.y;
                    ModConfig.Instance.Save();
                };

                _tabstrip = UIUtils.CreateTabStrip(this, _ingameAtlas);
                _tabstrip.width = width - 40f;
                _tabstrip.relativePosition = new Vector3(20f, 50f);

                _tabContainer = UIUtils.CreateTabContainer(this, _ingameAtlas);
                _tabContainer.width = width - 40f;
                _tabContainer.height = height - 120f;
                _tabContainer.relativePosition = new Vector3(20f, 100f);

                _templateButton = UIUtils.CreateTabButton(this, _ingameAtlas);

                _tabstrip.tabPages = _tabContainer;

                UIPanel panel = null;

                _tabstrip.AddTab("Time", _templateButton, true);
                _tabstrip.selectedIndex = 0;
                panel = _tabstrip.tabContainer.components[0] as UIPanel;
                if (panel != null)
                {
                    panel.autoLayout = true;
                    panel.autoLayoutStart = LayoutStart.TopLeft;
                    panel.autoLayoutDirection = LayoutDirection.Vertical;
                    panel.autoLayoutPadding.left = 5;
                    panel.autoLayoutPadding.right = 0;
                    panel.autoLayoutPadding.top = 0;
                    panel.autoLayoutPadding.bottom = 10;

                    _timeGameSpeedSliderLabel = UIUtils.CreateLabel(panel, "TimeGameSpeedSliderLabel", "Game Speed");
                    _timeGameSpeedSliderLabel.tooltip = "Set the speed at which the game passes";

                    _timeGameSpeedSliderNumeral = UIUtils.CreateLabel(_timeGameSpeedSliderLabel, "TimeGameSpeedSliderNumeral", FormatGameSpeed(ModConfig.Instance.GameSpeed));
                    _timeGameSpeedSliderNumeral.width = 100f;
                    _timeGameSpeedSliderNumeral.textAlignment = UIHorizontalAlignment.Right;
                    _timeGameSpeedSliderNumeral.relativePosition = new Vector3(panel.width - _timeGameSpeedSliderNumeral.width - 10f, 0f);

                    _timeGameSpeedSlider = UIUtils.CreateSlider(panel, "TimeGameSpeedSlider", _ingameAtlas, 0.1f, 3f, 0.05f, 0.05f, ModConfig.Instance.GameSpeed);
                    _timeGameSpeedSlider.eventValueChanged += (component, value) =>
                    {
                        ModConfig.Instance.GameSpeed = value;
                        ModConfig.Instance.Save();

                        _timeGameSpeedSliderNumeral.text = FormatGameSpeed(value);
                    };
                    _timeGameSpeedSlider.eventMouseUp += (component, eventParam) =>
                    {
                        if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                        {
                            _timeGameSpeedSlider.value = 1f;
                        }
                    };

                    _timeDayNightSpeedSliderLabel = UIUtils.CreateLabel(panel, "TimeDayNightSpeedSliderLabel", "Day/Night Speed");
                    _timeDayNightSpeedSliderLabel.tooltip = "Set the speed of the day/night cycle";

                    _timeDayNightSpeedSliderNumeral = UIUtils.CreateLabel(_timeDayNightSpeedSliderLabel, "TimeDayNightSpeedSliderNumeral", FormatDayNightSpeed(ModConfig.Instance.DayNightSpeed));
                    _timeDayNightSpeedSliderNumeral.width = 100f;
                    _timeDayNightSpeedSliderNumeral.textAlignment = UIHorizontalAlignment.Right;
                    _timeDayNightSpeedSliderNumeral.relativePosition = new Vector3(panel.width - _timeDayNightSpeedSliderNumeral.width - 10f, 0f);

                    _timeDayNightSpeedSlider = UIUtils.CreateSlider(panel, "TimeDayNightSpeedSlider", _ingameAtlas, -1f, 23f, 0.5f, 0.5f, ModConfig.Instance.DayNightSpeed);
                    _timeDayNightSpeedSlider.eventValueChanged += (component, value) =>
                    {
                        ModConfig.Instance.DayNightSpeed = value;
                        ModConfig.Instance.Save();

                        _timeDayNightSpeedSliderNumeral.text = FormatDayNightSpeed(value);
                    };
                    _timeDayNightSpeedSlider.eventMouseUp += (component, eventParam) =>
                    {
                        if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                        {
                            _timeDayNightSpeedSlider.value = 0f;
                        }
                    };

                    _timeTimeOfDaySliderLabel = UIUtils.CreateLabel(panel, "TimeTimeOfDaySliderLabel", "Time of Day");
                    _timeTimeOfDaySliderLabel.tooltip = "Set the time of day";

                    _timeTimeOfDaySliderNumeral = UIUtils.CreateLabel(_timeTimeOfDaySliderLabel, "TimeTimeOfDaySliderNumeral", "12:00");
                    _timeTimeOfDaySliderNumeral.width = 100f;
                    _timeTimeOfDaySliderNumeral.textAlignment = UIHorizontalAlignment.Right;
                    _timeTimeOfDaySliderNumeral.relativePosition = new Vector3(panel.width - _timeTimeOfDaySliderNumeral.width - 10f, 0f);

                    _timeTimeOfDaySlider = UIUtils.CreateSlider(panel, "TimeTimeOfDaySlider", _ingameAtlas, 0f, 23.99f, 0.01f, 1f, 12f);
                    _timeTimeOfDaySlider.eventValueChanged += (component, value) =>
                    {
                        if (Mathf.Abs(DayNightManager.Instance.DayTimeHour - value) > 0.1f)
                        {
                            DayNightManager.Instance.DayTimeHour = value;
                        }

                        _timeTimeOfDaySliderNumeral.text = FormatTimeOfDay(ModConfig.Instance.TimeConvention == 0, value);
                    };
                    _timeTimeOfDaySlider.eventMouseUp += (component, eventParam) =>
                    {
                        if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                        {
                            _timeTimeOfDaySlider.value = 12.0f;
                        }
                    };

                    _timeDayNightCyclePanel = UIUtils.CreatePanel(panel, "TimeDayNightCyclePanel");
                    _timeDayNightCyclePanel.isVisible = false;
                    _timeDayNightCyclePanel.width = panel.parent.width - 10f;
                    _timeDayNightCyclePanel.height = 100f;

                    _timeDayNightCycleLabel = UIUtils.CreateLabel(_timeDayNightCyclePanel, "TimeDayNightCycleLabel", "Day/night cycle is not enabled.");
                    _timeDayNightCycleLabel.height = 50f;
                    _timeDayNightCycleLabel.relativePosition = new Vector3(0f, 0f);

                    _timeDayNightCycleButton = UIUtils.CreatePanelButton(_timeDayNightCyclePanel, "TimeDayNightCycleButton", _ingameAtlas, "Enable");
                    _timeDayNightCycleButton.relativePosition = new Vector3(0f, 40f);
                    _timeDayNightCycleButton.eventClick += (component, eventParam) =>
                    {
                        if (!eventParam.used)
                        {
                            ModUtils.SetDayNightCycleInOptionsGameplayPanel(true);

                            eventParam.Use();
                        }
                    };
                }

                _tabstrip.AddTab("Weather", _templateButton, true);
                _tabstrip.selectedIndex = 1;
                panel = _tabstrip.tabContainer.components[1] as UIPanel;

                if (panel != null)
                {
                    panel.autoLayout = true;
                    panel.autoLayoutStart = LayoutStart.TopLeft;
                    panel.autoLayoutDirection = LayoutDirection.Vertical;
                    panel.autoLayoutPadding.left = 5;
                    panel.autoLayoutPadding.right = 0;
                    panel.autoLayoutPadding.top = 0;
                    panel.autoLayoutPadding.bottom = 10;

                    _weatherRainIntensitySliderLabel = UIUtils.CreateLabel(panel, "WeatherRainIntensitySliderLabel", "Rain/Snow Intensity");
                    _weatherRainIntensitySliderLabel.tooltip = "Set the intensity of rain or snow";

                    _weatherRainIntensitySliderNumeral = UIUtils.CreateLabel(_weatherRainIntensitySliderLabel, "WeatherRainIntensitySliderNumeral", "0");
                    _weatherRainIntensitySliderNumeral.width = 100f;
                    _weatherRainIntensitySliderNumeral.textAlignment = UIHorizontalAlignment.Right;
                    _weatherRainIntensitySliderNumeral.relativePosition = new Vector3(panel.width - _weatherRainIntensitySliderNumeral.width - 10f, 0f);

                    _weatherRainIntensitySlider = UIUtils.CreateSlider(panel, "WeatherRainIntensitySlider", _ingameAtlas, 0f, 1f, 0.01f, 0.1f, 0f);
                    _weatherRainIntensitySlider.eventValueChanged += (component, value) =>
                    {
                        Singleton<WeatherManager>.instance.m_targetRain = value;
                        Singleton<WeatherManager>.instance.m_currentRain = value;

                        ModConfig.Instance.RainIntensity = value;
                        ModConfig.Instance.Save();

                        _weatherRainIntensitySliderNumeral.text = value.ToString();
                    };
                    _weatherRainIntensitySlider.eventMouseUp += (component, eventParam) =>
                    {
                        if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                        {
                            _weatherRainIntensitySlider.value = 0f;
                        }
                    };

                    _weatherFogIntensitySliderLabel = UIUtils.CreateLabel(panel, "WeatherFogIntensitySliderLabel", "Fog Intensity");
                    _weatherFogIntensitySliderLabel.tooltip = "Set the intensity of fog";

                    _weatherFogIntensitySliderNumeral = UIUtils.CreateLabel(_weatherFogIntensitySliderLabel, "WeatherFogIntensitySliderNumeral", "0");
                    _weatherFogIntensitySliderNumeral.width = 100f;
                    _weatherFogIntensitySliderNumeral.textAlignment = UIHorizontalAlignment.Right;
                    _weatherFogIntensitySliderNumeral.relativePosition = new Vector3(panel.width - _weatherFogIntensitySliderNumeral.width - 10f, 0f);

                    _weatherFogIntensitySlider = UIUtils.CreateSlider(panel, "WeatherFogIntensitySlider", _ingameAtlas, 0f, 1f, 0.01f, 0.1f, 0f);
                    _weatherFogIntensitySlider.eventValueChanged += (component, value) =>
                    {
                        Singleton<WeatherManager>.instance.m_targetFog = value;
                        Singleton<WeatherManager>.instance.m_currentFog = value;

                        ModConfig.Instance.FogIntensity = value;
                        ModConfig.Instance.Save();

                        _weatherFogIntensitySliderNumeral.text = value.ToString();
                    };
                    _weatherFogIntensitySlider.eventMouseUp += (component, eventParam) =>
                    {
                        if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                        {
                            _weatherFogIntensitySlider.value = 0f;
                        }
                    };

                    _weatherCloudIntensitySliderLabel = UIUtils.CreateLabel(panel, "WeatherCloudIntensitySliderLabel", "Cloud Intensity");
                    _weatherCloudIntensitySliderLabel.tooltip = "Set the intensity of cloud";

                    _weatherCloudIntensitySliderNumeral = UIUtils.CreateLabel(_weatherCloudIntensitySliderLabel, "WeatherCloudIntensitySliderNumeral", "0");
                    _weatherCloudIntensitySliderNumeral.width = 100f;
                    _weatherCloudIntensitySliderNumeral.textAlignment = UIHorizontalAlignment.Right;
                    _weatherCloudIntensitySliderNumeral.relativePosition = new Vector3(panel.width - _weatherCloudIntensitySliderNumeral.width - 10f, 0f);

                    _weatherCloudIntensitySlider = UIUtils.CreateSlider(panel, "WeatherCloudIntensitySlider", _ingameAtlas, 0f, 1f, 0.01f, 0.1f, 0f);
                    _weatherCloudIntensitySlider.eventValueChanged += (component, value) =>
                    {
                        Singleton<WeatherManager>.instance.m_targetCloud = value;
                        Singleton<WeatherManager>.instance.m_currentCloud = value;

                        ModConfig.Instance.CloudIntensity = value;
                        ModConfig.Instance.Save();

                        _weatherCloudIntensitySliderNumeral.text = value.ToString();
                    };
                    _weatherCloudIntensitySlider.eventMouseUp += (component, eventParam) =>
                    {
                        if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                        {
                            _weatherCloudIntensitySlider.value = 0f;
                        }
                    };

                    _weatherNorthernLightsIntensitySliderLabel = UIUtils.CreateLabel(panel, "WeatherNorthernLightsIntensitySliderLabel", "Northern Lights Intensity");
                    _weatherNorthernLightsIntensitySliderLabel.tooltip = "Set the intensity of northern lights";

                    _weatherNorthernLightsIntensitySliderNumeral = UIUtils.CreateLabel(_weatherNorthernLightsIntensitySliderLabel, "WeatherNorthernLightsIntensitySliderNumeral", "0");
                    _weatherNorthernLightsIntensitySliderNumeral.width = 100f;
                    _weatherNorthernLightsIntensitySliderNumeral.textAlignment = UIHorizontalAlignment.Right;
                    _weatherNorthernLightsIntensitySliderNumeral.relativePosition = new Vector3(panel.width - _weatherNorthernLightsIntensitySliderNumeral.width - 10f, 0f);

                    _weatherNorthernLightsIntensitySlider = UIUtils.CreateSlider(panel, "WeatherNorthernLightsIntensitySlider", _ingameAtlas, 0f, 1f, 0.01f, 0.1f, 0f);
                    _weatherNorthernLightsIntensitySlider.eventValueChanged += (component, value) =>
                    {
                        Singleton<WeatherManager>.instance.m_targetNorthernLights = value;
                        Singleton<WeatherManager>.instance.m_currentNorthernLights = value;

                        ModConfig.Instance.NorthernLightsIntensity = value;
                        ModConfig.Instance.Save();

                        _weatherNorthernLightsIntensitySliderNumeral.text = value.ToString();
                    };
                    _weatherNorthernLightsIntensitySlider.eventMouseUp += (component, eventParam) =>
                    {
                        if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                        {
                            _weatherNorthernLightsIntensitySlider.value = 0f;
                        }
                    };

                    _weatherDynamicWeatherPanel = UIUtils.CreatePanel(panel, "WeatherDynamicWeatherPanel");
                    _weatherDynamicWeatherPanel.isVisible = false;
                    _weatherDynamicWeatherPanel.width = panel.parent.width - 10f;
                    _weatherDynamicWeatherPanel.height = 100f;

                    _weatherDynamicWeatherLabel = UIUtils.CreateLabel(_weatherDynamicWeatherPanel, "WeatherDynamicWeatherLabel", "Dynamic weather is not enabled.");
                    _weatherDynamicWeatherLabel.height = 50f;
                    _weatherDynamicWeatherLabel.relativePosition = new Vector3(0f, 0f);

                    _weatherDynamicWeatherButton = UIUtils.CreatePanelButton(_weatherDynamicWeatherPanel, "WeatherDynamicWeatherButton", _ingameAtlas, "Enable");
                    _weatherDynamicWeatherButton.relativePosition = new Vector3(0f, 40f);
                    _weatherDynamicWeatherButton.eventClick += (component, eventParam) =>
                    {
                        if (!eventParam.used)
                        {
                            ModUtils.SetDynamicWeatherInOptionsGameplayPanel(true);

                            eventParam.Use();
                        }
                    };
                }

                _tabstrip.AddTab("Advanced", _templateButton, true);
                _tabstrip.selectedIndex = 2;
                panel = _tabstrip.tabContainer.components[2] as UIPanel;

                if (panel != null)
                {
                    panel.autoLayout = true;
                    panel.autoLayoutStart = LayoutStart.TopLeft;
                    panel.autoLayoutDirection = LayoutDirection.Vertical;
                    panel.autoLayoutPadding.left = 5;
                    panel.autoLayoutPadding.right = 0;
                    panel.autoLayoutPadding.top = 0;
                    panel.autoLayoutPadding.bottom = 10;

                    _advancedTimeTitle = UIUtils.CreateTitle(panel, "AdvancedTimeTitle", "Time");
                    _advancedTimeTitle.tooltip = "Advanced options for Time";

                    _advancedTimeConventionDropDownLabel = UIUtils.CreateLabel(panel, "AdvancedTimeConventionDropDownLabel", "Time Convention");
                    _advancedTimeConventionDropDownLabel.tooltip = "Set the convention of time to either 12 or 24-hours clock";

                    _advancedTimeConventionDropDown = UIUtils.CreateDropDown(panel, "AdvancedTimeConventionDropDown", _ingameAtlas);
                    _advancedTimeConventionDropDown.items = ModInvariables.TimeConvention;
                    _advancedTimeConventionDropDown.selectedIndex = ModConfig.Instance.TimeConvention;
                    _advancedTimeConventionDropDown.eventSelectedIndexChanged += (component, value) =>
                    {
                        ModConfig.Instance.TimeConvention = value;
                        ModConfig.Instance.Save();

                        _timeTimeOfDaySliderNumeral.text = FormatTimeOfDay(value == 0, DayNightManager.Instance.DayTimeHour);
                    };

                    _advancedShowSpeedInPercentagesCheckBox = UIUtils.CreateCheckBox(panel, "AdvancedShowSpeedAsPercentageCheckBox", _ingameAtlas, "Show Speed in percentages", ModConfig.Instance.ShowSpeedInPercentages);
                    _advancedShowSpeedInPercentagesCheckBox.tooltip = "Set if game speed and day/night speed should be shown in percentages";
                    _advancedShowSpeedInPercentagesCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.ShowSpeedInPercentages = value;
                        ModConfig.Instance.Save();

                        _timeGameSpeedSliderNumeral.text = FormatGameSpeed(ModConfig.Instance.GameSpeed);
                        _timeDayNightSpeedSliderNumeral.text = FormatDayNightSpeed(ModConfig.Instance.DayNightSpeed);
                    };

                    _advancedPauseDayNightCycleOnSimulationPauseCheckBox = UIUtils.CreateCheckBox(panel, "AdvancedPauseDayNightCycleOnSimulationPauseCheckBox", _ingameAtlas, "Pause Day/Night when Simulation Pauses", ModConfig.Instance.PauseDayNightCycleOnSimulationPause);
                    _advancedPauseDayNightCycleOnSimulationPauseCheckBox.tooltip = "Set if day/night cycle should be automatically paused when simulation pauses";
                    _advancedPauseDayNightCycleOnSimulationPauseCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.PauseDayNightCycleOnSimulationPause = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedWeatherTitle = UIUtils.CreateTitle(panel, "AdvancedWeatherTitle", "Weather");
                    _advancedWeatherTitle.tooltip = "Advanced options for Weather";

                    _advancedLockRainIntensityCheckBox = UIUtils.CreateCheckBox(panel, "AdvancedLockRainIntensityCheckBox", _ingameAtlas, "Rain/Snow Intensity locked", ModConfig.Instance.LockRainIntensity);
                    _advancedLockRainIntensityCheckBox.tooltip = "Set intensity of rain or snow to never increase or decrease";
                    _advancedLockRainIntensityCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.LockRainIntensity = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedLockFogIntensityCheckBox = UIUtils.CreateCheckBox(panel, "AdvancedLockFogIntensityCheckBox", _ingameAtlas, "Fog Intensity locked", ModConfig.Instance.LockFogIntensity);
                    _advancedLockFogIntensityCheckBox.tooltip = "Set intensity of fog to never increase or decrease";
                    _advancedLockFogIntensityCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.LockFogIntensity = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedLockCloudIntensityCheckBox = UIUtils.CreateCheckBox(panel, "AdvancedLockCloudIntensityCheckBox", _ingameAtlas, "Cloud Intensity locked", ModConfig.Instance.LockCloudIntensity);
                    _advancedLockCloudIntensityCheckBox.tooltip = "Set intensity of cloud to never increase or decrease";
                    _advancedLockCloudIntensityCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.LockCloudIntensity = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedLockNorthernLightsIntensityCheckBox = UIUtils.CreateCheckBox(panel, "AdvancedLockNorthernLightsIntensityCheckBox", _ingameAtlas, "Northern Lights Intensity locked", ModConfig.Instance.LockNorthernLightsIntensity);
                    _advancedLockNorthernLightsIntensityCheckBox.tooltip = "Set intensity of northern lights to never increase or decrease";
                    _advancedLockNorthernLightsIntensityCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.LockNorthernLightsIntensity = value;
                        ModConfig.Instance.Save();
                    };
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] MainPanel:CreateUI -> Exception: " + e.Message);
            }
        }

        private void UpdateUI()
        {
            try
            {
                isVisible = ModConfig.Instance.ShowPanel;
                absolutePosition = new Vector3(ModConfig.Instance.PanelPositionX, ModConfig.Instance.PanelPositionY);
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] MainPanel:UpdateUI -> Exception: " + e.Message);
            }
        }

        private void RefreshGameSpeed()
        {
            try
            {
                _timeGameSpeedSlider.value = GameManager.Instance.GameSpeed;
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] MainPanel:RefreshGameSpeed -> Exception: " + e.Message);
            }
        }

        private void RefreshDayNightSpeed()
        {
            try
            {
                _timeDayNightSpeedSlider.value = DayNightManager.Instance.DayNightSpeed;
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] MainPanel:RefreshDayNightSpeed -> Exception: " + e.Message);
            }
        }

        private void RefreshTimeOfDay()
        {
            try
            {
                _timeTimeOfDaySlider.value = DayNightManager.Instance.DayTimeHour;
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] MainPanel:RefreshTimeOfDay -> Exception: " + e.Message);
            }
        }

        private void RefreshWeather()
        {
            try
            {
                if (!ModConfig.Instance.LockRainIntensity)
                {
                    _weatherRainIntensitySlider.value = Singleton<WeatherManager>.instance.m_currentRain;
                }

                if (!ModConfig.Instance.LockFogIntensity)
                {
                    _weatherFogIntensitySlider.value = Singleton<WeatherManager>.instance.m_currentFog;
                }

                if (!ModConfig.Instance.LockCloudIntensity)
                {
                    _weatherCloudIntensitySlider.value = Singleton<WeatherManager>.instance.m_currentCloud;
                }

                if (!ModConfig.Instance.LockNorthernLightsIntensity)
                {
                    _weatherNorthernLightsIntensitySlider.value = Singleton<WeatherManager>.instance.m_currentNorthernLights;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] MainPanel:RefreshWeather -> Exception: " + e.Message);
            }
        }

        private void UpdateWeather()
        {
            try
            {
                if (ModConfig.Instance.LockRainIntensity)
                {
                    Singleton<WeatherManager>.instance.m_currentRain = _weatherRainIntensitySlider.value;
                    Singleton<WeatherManager>.instance.m_targetRain = _weatherRainIntensitySlider.value;
                }

                if (ModConfig.Instance.LockFogIntensity)
                {
                    Singleton<WeatherManager>.instance.m_currentFog = _weatherFogIntensitySlider.value;
                    Singleton<WeatherManager>.instance.m_targetFog = _weatherFogIntensitySlider.value;
                }

                if (ModConfig.Instance.LockCloudIntensity)
                {
                    Singleton<WeatherManager>.instance.m_currentCloud = _weatherCloudIntensitySlider.value;
                    Singleton<WeatherManager>.instance.m_targetCloud = _weatherCloudIntensitySlider.value;
                }

                if (ModConfig.Instance.LockNorthernLightsIntensity)
                {
                    Singleton<WeatherManager>.instance.m_currentNorthernLights = _weatherNorthernLightsIntensitySlider.value;
                    Singleton<WeatherManager>.instance.m_targetNorthernLights = _weatherNorthernLightsIntensitySlider.value;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] MainPanel:UpdateWeather -> Exception: " + e.Message);
            }
        }

        private string FormatGameSpeed(float value)
        {
            if (value == 1f)
            {
                return ModConfig.Instance.ShowSpeedInPercentages ? "100%" : "Normal";
            }
            else
            {
                return ModConfig.Instance.ShowSpeedInPercentages ? value * 100 + "%" : value.ToString() + "x";
            }
        }

        private string FormatDayNightSpeed(float value)
        {
            if (value == -1f)
            {
                return ModConfig.Instance.ShowSpeedInPercentages ? "0%" : "Paused";
            }
            else if (value == 0f)
            {
                return ModConfig.Instance.ShowSpeedInPercentages ? "100%" : "Normal";
            }
            else
            {
                return ModConfig.Instance.ShowSpeedInPercentages ? (value + 1) * 100 + "%" : (value + 1).ToString() + "x";
            }
        }

        private string FormatTimeOfDay(bool twelweHourConvention, float value)
        {
            int hour = (int)Mathf.Floor(value);
            int minute = (int)Mathf.Floor((value - hour) * 60.0f);

            DateTime dateTime = DateTime.Parse(string.Format("{0,2:00}:{1,2:00}", hour, minute));

            return twelweHourConvention ? dateTime.ToString("hh:mm tt") : dateTime.ToString("HH:mm");
        }
    }
}
