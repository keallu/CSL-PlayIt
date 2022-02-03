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

        private UILabel _title;
        private UIButton _close;
        private UIDragHandle _dragHandle;
        private UITabstrip _tabstrip;
        private UITabContainer _tabContainer;
        private UIButton _templateButton;

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

        private UILabel _advancedTimeConventionDropDownLabel;
        private UIDropDown _advancedTimeConventionDropDown;

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

                    if (isVisible)
                    {
                        UpdateTimeOfDay();

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
                DestroyGameObject(_advancedTimeConventionDropDownLabel);
                DestroyGameObject(_advancedTimeConventionDropDown);
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

                _title = UIUtils.CreateMenuPanelTitle(this, "Play It!");
                _title.relativePosition = new Vector3(width / 2f - _title.width / 2f, 15f);

                _close = UIUtils.CreateMenuPanelCloseButton(this);
                _close.relativePosition = new Vector3(width - 37f, 3f);

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

                _tabstrip = UIUtils.CreateTabStrip(this);
                _tabstrip.width = width - 40f;
                _tabstrip.relativePosition = new Vector3(20f, 50f);

                _tabContainer = UIUtils.CreateTabContainer(this);
                _tabContainer.width = width - 40f;
                _tabContainer.height = height - 120f;
                _tabContainer.relativePosition = new Vector3(20f, 100f);

                _templateButton = UIUtils.CreateTabButton(this);

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

                    _timeDayNightSpeedSliderLabel = UIUtils.CreateLabel(panel, "TimeDayNightSpeedSliderLabel", "Day/Night Speed");
                    _timeDayNightSpeedSliderLabel.tooltip = "Set the speed of day/night cycle";

                    _timeDayNightSpeedSliderNumeral = UIUtils.CreateLabel(_timeDayNightSpeedSliderLabel, "TimeDayNightSpeedSliderNumeral", FormatDayNightSpeed(ModConfig.Instance.DayNightSpeed));
                    _timeDayNightSpeedSliderNumeral.width = 100f;
                    _timeDayNightSpeedSliderNumeral.textAlignment = UIHorizontalAlignment.Right;
                    _timeDayNightSpeedSliderNumeral.relativePosition = new Vector3(panel.width - _timeDayNightSpeedSliderNumeral.width - 10f, 0f);

                    _timeDayNightSpeedSlider = UIUtils.CreateSlider(panel, "TimeDayNightSpeedSlider", -1f, 23f, 1f, ModConfig.Instance.DayNightSpeed);
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

                    _timeTimeOfDaySlider = UIUtils.CreateSlider(panel, "TimeTimeOfDaySlider", 0f, 24f, 0.01f, 12f);
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

                    _timeDayNightCycleButton = UIUtils.CreatePanelButton(_timeDayNightCyclePanel, "TimeDayNightCycleButton", "Enable");
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

                    _weatherRainIntensitySlider = UIUtils.CreateSlider(panel, "WeatherRainIntensitySlider", 0f, 1f, 0.01f, 0f);
                    _weatherRainIntensitySlider.eventValueChanged += (component, value) =>
                    {
                        Singleton<WeatherManager>.instance.m_targetRain = value;
                        Singleton<WeatherManager>.instance.m_currentRain = value;

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

                    _weatherFogIntensitySlider = UIUtils.CreateSlider(panel, "WeatherFogIntensitySlider", 0f, 1f, 0.01f, 0f);
                    _weatherFogIntensitySlider.eventValueChanged += (component, value) =>
                    {
                        Singleton<WeatherManager>.instance.m_targetFog = value;
                        Singleton<WeatherManager>.instance.m_currentFog = value;

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

                    _weatherCloudIntensitySlider = UIUtils.CreateSlider(panel, "WeatherCloudIntensitySlider", 0f, 1f, 0.01f, 0f);
                    _weatherCloudIntensitySlider.eventValueChanged += (component, value) =>
                    {
                        Singleton<WeatherManager>.instance.m_targetCloud = value;
                        Singleton<WeatherManager>.instance.m_currentCloud = value;

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

                    _weatherNorthernLightsIntensitySlider = UIUtils.CreateSlider(panel, "WeatherNorthernLightsIntensitySlider", 0f, 1f, 0.01f, 0f);
                    _weatherNorthernLightsIntensitySlider.eventValueChanged += (component, value) =>
                    {
                        Singleton<WeatherManager>.instance.m_targetNorthernLights = value;
                        Singleton<WeatherManager>.instance.m_currentNorthernLights = value;

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

                    _weatherDynamicWeatherButton = UIUtils.CreatePanelButton(_weatherDynamicWeatherPanel, "WeatherDynamicWeatherButton", "Enable");
                    _weatherDynamicWeatherButton.relativePosition = new Vector3(0f, 25f);
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

                    _advancedTimeConventionDropDownLabel = UIUtils.CreateLabel(panel, "AdvancedTimeConventionDropDownLabel", "Time Convention");
                    _advancedTimeConventionDropDownLabel.tooltip = "Set the convention of time to either 12 or 24-hours clock";

                    _advancedTimeConventionDropDown = UIUtils.CreateDropDown(panel, "AdvancedTimeConventionDropDown");
                    _advancedTimeConventionDropDown.items = ModInvariables.TimeConvention;
                    _advancedTimeConventionDropDown.selectedIndex = ModConfig.Instance.TimeConvention;
                    _advancedTimeConventionDropDown.eventSelectedIndexChanged += (component, value) =>
                    {
                        ModConfig.Instance.TimeConvention = value;
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

        private void UpdateTimeOfDay()
        {
            try
            {
                _timeTimeOfDaySlider.value = DayNightManager.Instance.DayTimeHour;
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] MainPanel:UpdateTimeOfDay -> Exception: " + e.Message);
            }
        }

        private string FormatDayNightSpeed(float value)
        {
            if (value == -1f)
            {
                return "Paused";
            }
            else if (value == 0f)
            {
                return "Normal";
            }
            else
            {
                return (value + 1).ToString() + "x";
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
