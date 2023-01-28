﻿using ColossalFramework;
using ColossalFramework.UI;
using PlayIt.Helpers;
using PlayIt.Managers;
using System;
using UnityEngine;

namespace PlayIt.Panels
{
    public class MainPanel : UIPanel
    {
        private bool _initialized;
        private float _timer;

        private UIMultiStateButton _zoomButton;

        private UITextureAtlas _ingameAtlas;
        private UILabel _title;
        private UIButton _close;
        private UIDragHandle _dragHandle;
        private UITabstrip _tabstrip;
        private UITabContainer _tabContainer;
        private UIButton _templateButton;

        private UILabel _worldLatitudeSliderLabel;
        private UILabel _worldLatitudeSliderNumeral;
        private UISlider _worldLatitudeSlider;
        private UILabel _worldLongitudeSliderLabel;
        private UILabel _worldLongitudeSliderNumeral;
        private UISlider _worldLongitudeSlider;
        private UIPanel _worldMessagePanel;
        private UILabel _worldMessageLabel;
        private UILabel _worldGameSpeedSliderLabel;
        private UILabel _worldGameSpeedSliderNumeral;
        private UISlider _worldGameSpeedSlider;
        private UILabel _worldDayNightSpeedSliderLabel;
        private UILabel _worldDayNightSpeedSliderNumeral;
        private UISlider _worldDayNightSpeedSlider;
        private UILabel _worldDayTimeHourSliderLabel;
        private UILabel _worldDayTimeHourSliderNumeral;
        private UISlider _worldDayTimeHourSlider;
        private UIPanel _worldDayNightCyclePanel;
        private UILabel _worldDayNightCycleLabel;
        private UIButton _worldDayNightCycleButton;

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

        private UIScrollablePanel _advancedScrollablePanel;
        private UIScrollbar _advancedScrollbar;
        private UISlicedSprite _advancedScrollbarTrack;
        private UISlicedSprite _advancedScrollbarThumb;
        private UILabel _advancedWorldTitle;
        private UILabel _advancedTimeConventionDropDownLabel;
        private UIDropDown _advancedTimeConventionDropDown;
        private UICheckBox _advancedShowSpeedInPercentagesCheckBox;
        private UICheckBox _advancedPauseDayNightCycleOnSimulationPauseCheckBox;
        private UISprite _advancedTimeDivider;
        private UILabel _advancedWeatherTitle;
        private UICheckBox _advancedLockRainIntensityCheckBox;
        private UICheckBox _advancedLockFogIntensityCheckBox;
        private UICheckBox _advancedLockCloudIntensityCheckBox;
        private UICheckBox _advancedLockNorthernLightsIntensityCheckBox;
        private UISprite _advancedWeatherDivider;
        private UILabel _advancedClockTitle;
        private UICheckBox _advancedShowIngameClockPanelCheckBox;
        private UICheckBox _advancedShowSpeedInClockPanelCheckBox;
        private UILabel _advancedTextColorInClockPanelDropDownLabel;
        private UIDropDown _advancedTextColorInClockPanelDropDown;
        private UICheckBox _advancedUseOutlineInClockPanelCheckBox;
        private UILabel _advancedOutlineColorInClockPanelDropDownLabel;
        private UIDropDown _advancedOutlineColorInClockPanelDropDown;
        private UISprite _advancedClockDivider;
        private UILabel _advancedKeymappingsTitle;
        private UICheckBox _advancedKeymappingsEnabledCheckBox;
        private UILabel _advancedKeymappingsIncreaseGameSpeedDropDownLabel;
        private UIDropDown _advancedKeymappingsIncreaseGameSpeedDropDown;
        private UILabel _advancedKeymappingsDecreaseGameSpeedDropDownLabel;
        private UIDropDown _advancedKeymappingsDecreaseGameSpeedDropDown;
        private UILabel _advancedKeymappingsIncreaseDayNightSpeedDropDownLabel;
        private UIDropDown _advancedKeymappingsIncreaseDayNightSpeedDropDown;
        private UILabel _advancedKeymappingsDecreaseDayNightSpeedDropDownLabel;
        private UIDropDown _advancedKeymappingsDecreaseDayNightSpeedDropDown;
        private UILabel _advancedKeymappingsForwardTimeOfDayDropDownLabel;
        private UIDropDown _advancedKeymappingsForwardTimeOfDayDropDown;
        private UILabel _advancedKeymappingsBackwardTimeOfDayDropDownLabel;
        private UIDropDown _advancedKeymappingsBackwardTimeOfDayDropDown;

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
                DefaultManager.Instance.Initialize();

                if (ModConfig.Instance.PanelPositionX == 0f && ModConfig.Instance.PanelPositionY == 0f)
                {
                    ModProperties.Instance.ResetPanelPosition();
                }

                if (float.IsNaN(ModConfig.Instance.Latitude))
                {
                    ModConfig.Instance.Latitude = (float)DefaultManager.Instance.Get("Latitude");
                }

                if (float.IsNaN(ModConfig.Instance.Longitude))
                {
                    ModConfig.Instance.Longitude = (float)DefaultManager.Instance.Get("Longitude");
                }

                _ingameAtlas = ResourceLoader.GetAtlas("Ingame");

                _zoomButton = GameObject.Find("ZoomButton")?.GetComponent<UIMultiStateButton>();

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
                        RefreshDayNightSpeed();
                        RefreshTimeOfDay();
                        RefreshWeather();

                        _worldDayNightCyclePanel.isVisible = !GameOptionsHelper.GetDayNightCycleInOptionsGameplayPanel();
                        _weatherDynamicWeatherPanel.isVisible = !GameOptionsHelper.GetDynamicWeatherInOptionsGameplayPanel();
                    }
                }

                if (ModConfig.Instance.KeymappingsEnabled && KeyChecker.GetKeyCombo(out int key))
                {
                    if (ModConfig.Instance.KeymappingsIncreaseGameSpeed == key && _worldGameSpeedSlider.value <= 2.95f)
                    {
                        _worldGameSpeedSlider.value = _worldGameSpeedSlider.value + 0.05f;
                    }

                    if (ModConfig.Instance.KeymappingsDecreaseGameSpeed == key && _worldGameSpeedSlider.value >= 0.06f)
                    {
                        _worldGameSpeedSlider.value = _worldGameSpeedSlider.value - 0.05f;
                    }

                    if (ModConfig.Instance.KeymappingsIncreaseDayNightSpeed == key && _worldDayNightSpeedSlider.value <= 22.95f)
                    {
                        _worldDayNightSpeedSlider.value = _worldDayNightSpeedSlider.value + 0.5f;
                    }

                    if (ModConfig.Instance.KeymappingsDecreaseDayNightSpeed == key && _worldDayNightSpeedSlider.value >= -0.95f)
                    {
                        _worldDayNightSpeedSlider.value = ModConfig.Instance.DayNightSpeed - 0.5f;
                    }

                    if (ModConfig.Instance.KeymappingsForwardTimeOfDay == key && _worldDayTimeHourSlider.value <= 22.99f)
                    {
                        _worldDayTimeHourSlider.value = _worldDayTimeHourSlider.value + 1f;
                    }

                    if (ModConfig.Instance.KeymappingsBackwardTimeOfDay == key && _worldDayTimeHourSlider.value >= 1f)
                    {
                        _worldDayTimeHourSlider.value = _worldDayTimeHourSlider.value - 1f;
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
                DestroyGameObject(_worldLatitudeSliderLabel);
                DestroyGameObject(_worldLatitudeSliderNumeral);
                DestroyGameObject(_worldLatitudeSlider);
                DestroyGameObject(_worldLongitudeSliderLabel);
                DestroyGameObject(_worldLongitudeSliderNumeral);
                DestroyGameObject(_worldLongitudeSlider);
                DestroyGameObject(_worldMessagePanel);
                DestroyGameObject(_worldMessageLabel);
                DestroyGameObject(_worldGameSpeedSliderLabel);
                DestroyGameObject(_worldGameSpeedSliderNumeral);
                DestroyGameObject(_worldGameSpeedSlider);
                DestroyGameObject(_worldDayNightSpeedSliderLabel);
                DestroyGameObject(_worldDayNightSpeedSliderNumeral);
                DestroyGameObject(_worldDayNightSpeedSlider);
                DestroyGameObject(_worldDayTimeHourSliderLabel);
                DestroyGameObject(_worldDayTimeHourSliderNumeral);
                DestroyGameObject(_worldDayTimeHourSlider);
                DestroyGameObject(_worldDayNightCyclePanel);
                DestroyGameObject(_worldDayNightCycleLabel);
                DestroyGameObject(_worldDayNightCycleButton);
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
                DestroyGameObject(_advancedScrollablePanel);
                DestroyGameObject(_advancedScrollbar);
                DestroyGameObject(_advancedScrollbarTrack);
                DestroyGameObject(_advancedScrollbarThumb);
                DestroyGameObject(_advancedWorldTitle);
                DestroyGameObject(_advancedTimeConventionDropDownLabel);
                DestroyGameObject(_advancedTimeConventionDropDown);
                DestroyGameObject(_advancedShowSpeedInPercentagesCheckBox);
                DestroyGameObject(_advancedPauseDayNightCycleOnSimulationPauseCheckBox);
                DestroyGameObject(_advancedTimeDivider);
                DestroyGameObject(_advancedWeatherTitle);
                DestroyGameObject(_advancedLockRainIntensityCheckBox);
                DestroyGameObject(_advancedLockFogIntensityCheckBox);
                DestroyGameObject(_advancedLockCloudIntensityCheckBox);
                DestroyGameObject(_advancedLockNorthernLightsIntensityCheckBox);
                DestroyGameObject(_advancedWeatherDivider);
                DestroyGameObject(_advancedClockTitle);
                DestroyGameObject(_advancedShowIngameClockPanelCheckBox);
                DestroyGameObject(_advancedShowSpeedInClockPanelCheckBox);
                DestroyGameObject(_advancedTextColorInClockPanelDropDownLabel);
                DestroyGameObject(_advancedTextColorInClockPanelDropDown);
                DestroyGameObject(_advancedUseOutlineInClockPanelCheckBox);
                DestroyGameObject(_advancedOutlineColorInClockPanelDropDownLabel);
                DestroyGameObject(_advancedOutlineColorInClockPanelDropDown);
                DestroyGameObject(_advancedClockDivider);
                DestroyGameObject(_advancedKeymappingsTitle);
                DestroyGameObject(_advancedKeymappingsEnabledCheckBox);
                DestroyGameObject(_advancedKeymappingsIncreaseGameSpeedDropDownLabel);
                DestroyGameObject(_advancedKeymappingsIncreaseGameSpeedDropDown);
                DestroyGameObject(_advancedKeymappingsDecreaseGameSpeedDropDownLabel);
                DestroyGameObject(_advancedKeymappingsDecreaseGameSpeedDropDown);
                DestroyGameObject(_advancedKeymappingsIncreaseDayNightSpeedDropDownLabel);
                DestroyGameObject(_advancedKeymappingsIncreaseDayNightSpeedDropDown);
                DestroyGameObject(_advancedKeymappingsDecreaseDayNightSpeedDropDownLabel);
                DestroyGameObject(_advancedKeymappingsDecreaseDayNightSpeedDropDown);
                DestroyGameObject(_advancedKeymappingsForwardTimeOfDayDropDownLabel);
                DestroyGameObject(_advancedKeymappingsForwardTimeOfDayDropDown);
                DestroyGameObject(_advancedKeymappingsBackwardTimeOfDayDropDownLabel);
                DestroyGameObject(_advancedKeymappingsBackwardTimeOfDayDropDown);

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

        public void ForceDayTimeHour(float dayTimeHour)
        {
            DayNightManager.Instance.DayTimeHour = dayTimeHour;
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
                height = 450f;

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

                _tabstrip.AddTab("World", _templateButton, true);
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

                    if (!CompatibilityHelper.IsAnyLatitudeAndLongitudeManipulatingModsEnabled())
                    {
                        _worldLatitudeSliderLabel = UIUtils.CreateLabel(panel, "WorldLatitudeSliderLabel", "Latitude");
                        _worldLatitudeSliderLabel.tooltip = "Set the latitude of the geographic coordinate system";

                        _worldLatitudeSliderNumeral = UIUtils.CreateLabel(_worldLatitudeSliderLabel, "WorldLatitudeSliderNumeral", ModConfig.Instance.Latitude.ToString());
                        _worldLatitudeSliderNumeral.width = 100f;
                        _worldLatitudeSliderNumeral.textAlignment = UIHorizontalAlignment.Right;
                        _worldLatitudeSliderNumeral.relativePosition = new Vector3(panel.width - _worldLatitudeSliderNumeral.width - 10f, 0f);

                        _worldLatitudeSlider = UIUtils.CreateSlider(panel, "WorldLatitudeSlider", _ingameAtlas, -80f, 80f, 0.1f, 0.1f, ModConfig.Instance.Latitude);
                        _worldLatitudeSlider.eventValueChanged += (component, value) =>
                        {
                            DayNightManager.Instance.Latitude = value;

                            ModConfig.Instance.Latitude = value;
                            ModConfig.Instance.Save();

                            _worldLatitudeSliderNumeral.text = value.ToString();
                        };
                        _worldLatitudeSlider.eventMouseUp += (component, eventParam) =>
                        {
                            if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                            {
                                _worldLatitudeSlider.value = (float)DefaultManager.Instance.Get("Latitude");
                            }
                        };

                        _worldLongitudeSliderLabel = UIUtils.CreateLabel(panel, "WorldLongitudeSliderLabel", "Longitude");
                        _worldLongitudeSliderLabel.tooltip = "Set the longitude of the geographic coordinate system";

                        _worldLongitudeSliderNumeral = UIUtils.CreateLabel(_worldLongitudeSliderLabel, "WorldLongitudeSliderNumeral", ModConfig.Instance.Longitude.ToString());
                        _worldLongitudeSliderNumeral.width = 100f;
                        _worldLongitudeSliderNumeral.textAlignment = UIHorizontalAlignment.Right;
                        _worldLongitudeSliderNumeral.relativePosition = new Vector3(panel.width - _worldLongitudeSliderNumeral.width - 10f, 0f);

                        _worldLongitudeSlider = UIUtils.CreateSlider(panel, "WorldLongitudeSlider", _ingameAtlas, -180f, 180f, 0.1f, 0.1f, ModConfig.Instance.Longitude);
                        _worldLongitudeSlider.eventValueChanged += (component, value) =>
                        {
                            DayNightManager.Instance.Longitude = value;

                            ModConfig.Instance.Longitude = value;
                            ModConfig.Instance.Save();

                            _worldLongitudeSliderNumeral.text = value.ToString();
                        };
                        _worldLongitudeSlider.eventMouseUp += (component, eventParam) =>
                        {
                            if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                            {
                                _worldLongitudeSlider.value = (float)DefaultManager.Instance.Get("Longitude");
                            }
                        };
                    }
                    else
                    {
                        _worldMessagePanel = UIUtils.CreatePanel(panel, "WorldMessagePanel");
                        _worldMessagePanel.backgroundSprite = "GenericPanelLight";
                        _worldMessagePanel.color = new Color32(206, 206, 206, 255);
                        _worldMessagePanel.height = 60f;
                        _worldMessagePanel.width = _worldMessagePanel.parent.width - 16f;
                        _worldMessagePanel.relativePosition = new Vector3(8f, 8f);

                        _worldMessageLabel = UIUtils.CreateLabel(_worldMessagePanel, "WorldMessageLabel", "Adjustment of latitude and longitude in Play It! has been disabled because you have Theme Mixer 2 enabled.");
                        _worldMessageLabel.autoHeight = true;
                        _worldMessageLabel.width = _worldMessageLabel.parent.width - 16f;
                        _worldMessageLabel.relativePosition = new Vector3(8f, 8f);
                        _worldMessageLabel.wordWrap = true;
                    }

                    _worldGameSpeedSliderLabel = UIUtils.CreateLabel(panel, "TimeGameSpeedSliderLabel", "Game Speed");
                    _worldGameSpeedSliderLabel.tooltip = "Set the speed at which the game passes";

                    _worldGameSpeedSliderNumeral = UIUtils.CreateLabel(_worldGameSpeedSliderLabel, "TimeGameSpeedSliderNumeral", SpeedHelper.FormatGameSpeed(ModConfig.Instance.ShowSpeedInPercentages, ModConfig.Instance.GameSpeed));
                    _worldGameSpeedSliderNumeral.width = 100f;
                    _worldGameSpeedSliderNumeral.textAlignment = UIHorizontalAlignment.Right;
                    _worldGameSpeedSliderNumeral.relativePosition = new Vector3(panel.width - _worldGameSpeedSliderNumeral.width - 10f, 0f);

                    _worldGameSpeedSlider = UIUtils.CreateSlider(panel, "TimeGameSpeedSlider", _ingameAtlas, 0.01f, 3f, 0.01f, 0.05f, ModConfig.Instance.GameSpeed);
                    _worldGameSpeedSlider.eventValueChanged += (component, value) =>
                    {
                        ModConfig.Instance.GameSpeed = value;
                        ModConfig.Instance.Save();

                        _worldGameSpeedSliderNumeral.text = SpeedHelper.FormatGameSpeed(ModConfig.Instance.ShowSpeedInPercentages, value);
                    };
                    _worldGameSpeedSlider.eventMouseUp += (component, eventParam) =>
                    {
                        if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                        {
                            _worldGameSpeedSlider.value = 1f;
                        }
                    };

                    _worldDayNightSpeedSliderLabel = UIUtils.CreateLabel(panel, "WorldDayNightSpeedSliderLabel", "Day/Night Speed");
                    _worldDayNightSpeedSliderLabel.tooltip = "Set the speed of the day/night cycle";

                    _worldDayNightSpeedSliderNumeral = UIUtils.CreateLabel(_worldDayNightSpeedSliderLabel, "WorldDayNightSpeedSliderNumeral", SpeedHelper.FormatDayNightSpeed(ModConfig.Instance.ShowSpeedInPercentages, ModConfig.Instance.DayNightSpeed));
                    _worldDayNightSpeedSliderNumeral.width = 100f;
                    _worldDayNightSpeedSliderNumeral.textAlignment = UIHorizontalAlignment.Right;
                    _worldDayNightSpeedSliderNumeral.relativePosition = new Vector3(panel.width - _worldDayNightSpeedSliderNumeral.width - 10f, 0f);

                    _worldDayNightSpeedSlider = UIUtils.CreateSlider(panel, "WorldDayNightSpeedSlider", _ingameAtlas, -1f, 23f, 0.5f, 0.5f, ModConfig.Instance.DayNightSpeed);
                    _worldDayNightSpeedSlider.eventValueChanged += (component, value) =>
                    {
                        ModConfig.Instance.DayNightSpeed = value;
                        ModConfig.Instance.Save();

                        _worldDayNightSpeedSliderNumeral.text = SpeedHelper.FormatDayNightSpeed(ModConfig.Instance.ShowSpeedInPercentages, value);
                    };
                    _worldDayNightSpeedSlider.eventMouseUp += (component, eventParam) =>
                    {
                        if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                        {
                            _worldDayNightSpeedSlider.value = 0f;
                        }
                    };

                    _worldDayTimeHourSliderLabel = UIUtils.CreateLabel(panel, "WorldDayTimeHourSliderLabel", "Time of Day");
                    _worldDayTimeHourSliderLabel.tooltip = "Set the time of day";

                    _worldDayTimeHourSliderNumeral = UIUtils.CreateLabel(_worldDayTimeHourSliderLabel, "WorldDayTimeHourSliderNumeral", "11:07");
                    _worldDayTimeHourSliderNumeral.width = 100f;
                    _worldDayTimeHourSliderNumeral.textAlignment = UIHorizontalAlignment.Right;
                    _worldDayTimeHourSliderNumeral.relativePosition = new Vector3(panel.width - _worldDayTimeHourSliderNumeral.width - 10f, 0f);

                    _worldDayTimeHourSlider = UIUtils.CreateSlider(panel, "TimeDayTimeHourSlider", _ingameAtlas, 0f, 23.99f, 0.01f, 1f, 12f);
                    _worldDayTimeHourSlider.eventValueChanged += (component, value) =>
                    {
                        if (Mathf.Abs(DayNightManager.Instance.DayTimeHour - value) > 0.1f)
                        {
                            DayNightManager.Instance.DayTimeHour = value;
                        }

                        _worldDayTimeHourSliderNumeral.text = TimeHelper.FormatTimeOfDay(ModConfig.Instance.TimeConvention == 0, value);
                    };
                    _worldDayTimeHourSlider.eventMouseUp += (component, eventParam) =>
                    {
                        if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                        {
                            _worldDayTimeHourSlider.value = 12f;
                        }
                    };

                    _worldDayNightCyclePanel = UIUtils.CreatePanel(panel, "WorldDayNightCyclePanel");
                    _worldDayNightCyclePanel.isVisible = false;
                    _worldDayNightCyclePanel.width = panel.parent.width - 10f;
                    _worldDayNightCyclePanel.height = 100f;

                    _worldDayNightCycleLabel = UIUtils.CreateLabel(_worldDayNightCyclePanel, "WorldDayNightCycleLabel", "Day/night cycle is not enabled.");
                    _worldDayNightCycleLabel.height = 50f;
                    _worldDayNightCycleLabel.relativePosition = new Vector3(0f, 0f);

                    _worldDayNightCycleButton = UIUtils.CreatePanelButton(_worldDayNightCyclePanel, "WorldDayNightCycleButton", _ingameAtlas, "Enable");
                    _worldDayNightCycleButton.relativePosition = new Vector3(0f, 40f);
                    _worldDayNightCycleButton.eventClick += (component, eventParam) =>
                    {
                        if (!eventParam.used)
                        {
                            GameOptionsHelper.SetDayNightCycleInOptionsGameplayPanel(true);

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
                            GameOptionsHelper.SetDynamicWeatherInOptionsGameplayPanel(true);

                            eventParam.Use();
                        }
                    };
                }

                _tabstrip.AddTab("Advanced", _templateButton, true);
                _tabstrip.selectedIndex = 2;
                panel = _tabstrip.tabContainer.components[2] as UIPanel;

                if (panel != null)
                {
                    _advancedScrollablePanel = UIUtils.CreateScrollablePanel(panel, "AdvancedScrollablePanel");
                    _advancedScrollablePanel.width = panel.width - 25f;
                    _advancedScrollablePanel.height = panel.height;
                    _advancedScrollablePanel.relativePosition = new Vector3(0f, 0f);

                    _advancedScrollablePanel.autoLayout = true;
                    _advancedScrollablePanel.autoLayoutStart = LayoutStart.TopLeft;
                    _advancedScrollablePanel.autoLayoutDirection = LayoutDirection.Vertical;
                    _advancedScrollablePanel.autoLayoutPadding.left = 5;
                    _advancedScrollablePanel.autoLayoutPadding.right = 0;
                    _advancedScrollablePanel.autoLayoutPadding.top = 0;
                    _advancedScrollablePanel.autoLayoutPadding.bottom = 10;
                    _advancedScrollablePanel.scrollWheelDirection = UIOrientation.Vertical;
                    _advancedScrollablePanel.builtinKeyNavigation = true;
                    _advancedScrollablePanel.clipChildren = true;

                    _advancedScrollbar = UIUtils.CreateScrollbar(panel, "AdvancedScrollbar");
                    _advancedScrollbar.width = 20f;
                    _advancedScrollbar.height = _advancedScrollablePanel.height;
                    _advancedScrollbar.relativePosition = new Vector3(panel.width - 20f, 0f);
                    _advancedScrollbar.orientation = UIOrientation.Vertical;
                    _advancedScrollbar.incrementAmount = 38f;

                    _advancedScrollbarTrack = UIUtils.CreateSlicedSprite(_advancedScrollbar, "AdvancedScrollbarTrack");
                    _advancedScrollbarTrack.width = _advancedScrollbar.width;
                    _advancedScrollbarTrack.height = _advancedScrollbar.height;
                    _advancedScrollbarTrack.relativePosition = new Vector3(0f, 0f);
                    _advancedScrollbarTrack.spriteName = "ScrollbarTrack";
                    _advancedScrollbarTrack.fillDirection = UIFillDirection.Vertical;

                    _advancedScrollbarThumb = UIUtils.CreateSlicedSprite(_advancedScrollbar, "AdvancedScrollbarThumb");
                    _advancedScrollbarThumb.width = _advancedScrollbar.width - 6f;
                    _advancedScrollbarThumb.relativePosition = new Vector3(3f, 0f);
                    _advancedScrollbarThumb.spriteName = "ScrollbarThumb";
                    _advancedScrollbarThumb.fillDirection = UIFillDirection.Vertical;

                    _advancedScrollablePanel.verticalScrollbar = _advancedScrollbar;
                    _advancedScrollbar.trackObject = _advancedScrollbarTrack;
                    _advancedScrollbar.thumbObject = _advancedScrollbarThumb;

                    _advancedWorldTitle = UIUtils.CreateTitle(_advancedScrollablePanel, "AdvancedWorldTitle", "World");
                    _advancedWorldTitle.tooltip = "Advanced options for World";

                    _advancedTimeConventionDropDownLabel = UIUtils.CreateLabel(_advancedScrollablePanel, "AdvancedTimeConventionDropDownLabel", "Time Convention");
                    _advancedTimeConventionDropDownLabel.tooltip = "Set the convention of time to either 12 or 24-hours clock";

                    _advancedTimeConventionDropDown = UIUtils.CreateDropDown(_advancedScrollablePanel, "AdvancedTimeConventionDropDown", _ingameAtlas);
                    _advancedTimeConventionDropDown.items = ModInvariables.TimeConventions;
                    _advancedTimeConventionDropDown.selectedIndex = ModConfig.Instance.TimeConvention;
                    _advancedTimeConventionDropDown.eventSelectedIndexChanged += (component, value) =>
                    {
                        ModConfig.Instance.TimeConvention = value;
                        ModConfig.Instance.Save();

                        _worldDayTimeHourSliderNumeral.text = TimeHelper.FormatTimeOfDay(value == 0, DayNightManager.Instance.DayTimeHour);
                    };

                    _advancedShowSpeedInPercentagesCheckBox = UIUtils.CreateCheckBox(_advancedScrollablePanel, "AdvancedShowSpeedAsPercentageCheckBox", _ingameAtlas, "Show Speed in percentages", ModConfig.Instance.ShowSpeedInPercentages);
                    _advancedShowSpeedInPercentagesCheckBox.tooltip = "Set if game speed and day/night speed should be shown in percentages";
                    _advancedShowSpeedInPercentagesCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.ShowSpeedInPercentages = value;
                        ModConfig.Instance.Save();

                        _worldGameSpeedSliderNumeral.text = SpeedHelper.FormatGameSpeed(ModConfig.Instance.ShowSpeedInPercentages, ModConfig.Instance.GameSpeed);
                        _worldDayNightSpeedSliderNumeral.text = SpeedHelper.FormatDayNightSpeed(ModConfig.Instance.ShowSpeedInPercentages, ModConfig.Instance.DayNightSpeed);
                    };

                    _advancedPauseDayNightCycleOnSimulationPauseCheckBox = UIUtils.CreateCheckBox(_advancedScrollablePanel, "AdvancedPauseDayNightCycleOnSimulationPauseCheckBox", _ingameAtlas, "Pause Day/Night when Simulation Pauses", ModConfig.Instance.PauseDayNightCycleOnSimulationPause);
                    _advancedPauseDayNightCycleOnSimulationPauseCheckBox.tooltip = "Set if day/night cycle should be automatically paused when simulation pauses";
                    _advancedPauseDayNightCycleOnSimulationPauseCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.PauseDayNightCycleOnSimulationPause = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedTimeDivider = UIUtils.CreateDivider(_advancedScrollablePanel, "AdvancedTimeDivider", _ingameAtlas);

                    _advancedWeatherTitle = UIUtils.CreateTitle(_advancedScrollablePanel, "AdvancedWeatherTitle", "Weather");
                    _advancedWeatherTitle.tooltip = "Advanced options for Weather";

                    _advancedLockRainIntensityCheckBox = UIUtils.CreateCheckBox(_advancedScrollablePanel, "AdvancedLockRainIntensityCheckBox", _ingameAtlas, "Rain/Snow Intensity locked", ModConfig.Instance.LockRainIntensity);
                    _advancedLockRainIntensityCheckBox.tooltip = "Set intensity of rain or snow to never increase or decrease";
                    _advancedLockRainIntensityCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.LockRainIntensity = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedLockFogIntensityCheckBox = UIUtils.CreateCheckBox(_advancedScrollablePanel, "AdvancedLockFogIntensityCheckBox", _ingameAtlas, "Fog Intensity locked", ModConfig.Instance.LockFogIntensity);
                    _advancedLockFogIntensityCheckBox.tooltip = "Set intensity of fog to never increase or decrease";
                    _advancedLockFogIntensityCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.LockFogIntensity = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedLockCloudIntensityCheckBox = UIUtils.CreateCheckBox(_advancedScrollablePanel, "AdvancedLockCloudIntensityCheckBox", _ingameAtlas, "Cloud Intensity locked", ModConfig.Instance.LockCloudIntensity);
                    _advancedLockCloudIntensityCheckBox.tooltip = "Set intensity of cloud to never increase or decrease";
                    _advancedLockCloudIntensityCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.LockCloudIntensity = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedLockNorthernLightsIntensityCheckBox = UIUtils.CreateCheckBox(_advancedScrollablePanel, "AdvancedLockNorthernLightsIntensityCheckBox", _ingameAtlas, "Northern Lights Intensity locked", ModConfig.Instance.LockNorthernLightsIntensity);
                    _advancedLockNorthernLightsIntensityCheckBox.tooltip = "Set intensity of northern lights to never increase or decrease";
                    _advancedLockNorthernLightsIntensityCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.LockNorthernLightsIntensity = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedWeatherDivider = UIUtils.CreateDivider(_advancedScrollablePanel, "AdvancedWeatherDivider", _ingameAtlas);

                    _advancedClockTitle = UIUtils.CreateTitle(_advancedScrollablePanel, "AdvancedClockTitle", "Clock");
                    _advancedClockTitle.tooltip = "Advanced options for Clock";

                    _advancedShowIngameClockPanelCheckBox = UIUtils.CreateCheckBox(_advancedScrollablePanel, "AdvancedShowIngameClockPanelCheckBox", _ingameAtlas, "Show In-game Clock", ModConfig.Instance.ShowClock);
                    _advancedShowIngameClockPanelCheckBox.tooltip = "Set if in-game clock panel should be visible";
                    _advancedShowIngameClockPanelCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.ShowClock = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedShowSpeedInClockPanelCheckBox = UIUtils.CreateCheckBox(_advancedScrollablePanel, "AdvancedShowSpeedInClockPanelCheckBox", _ingameAtlas, "Show Game and Day/Night Speed", ModConfig.Instance.ShowSpeedInClockPanel);
                    _advancedShowSpeedInClockPanelCheckBox.tooltip = "Set if in-game clock panel should show game and day/night speed";
                    _advancedShowSpeedInClockPanelCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.ShowSpeedInClockPanel = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedTextColorInClockPanelDropDownLabel = UIUtils.CreateLabel(_advancedScrollablePanel, "AdvancedTextColorInClockPanelDropDownLabel", "Text Color");
                    _advancedTextColorInClockPanelDropDownLabel.tooltip = "Set the text color for the in-game clock panel";

                    _advancedTextColorInClockPanelDropDown = UIUtils.CreateDropDown(_advancedScrollablePanel, "AdvancedTextColorInClockPanelDropDown", _ingameAtlas);
                    _advancedTextColorInClockPanelDropDown.items = ModInvariables.TextColors;
                    _advancedTextColorInClockPanelDropDown.selectedIndex = ModConfig.Instance.TextColorInClockPanel;
                    _advancedTextColorInClockPanelDropDown.eventSelectedIndexChanged += (component, value) =>
                    {
                        ModConfig.Instance.TextColorInClockPanel = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedUseOutlineInClockPanelCheckBox = UIUtils.CreateCheckBox(_advancedScrollablePanel, "AdvancedUseOutlineInClockPanelCheckBox", _ingameAtlas, "Use Outline", ModConfig.Instance.UseOutlineInClockPanel);
                    _advancedUseOutlineInClockPanelCheckBox.tooltip = "Set if text for in-game clock panel should use outline";
                    _advancedUseOutlineInClockPanelCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.UseOutlineInClockPanel = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedOutlineColorInClockPanelDropDownLabel = UIUtils.CreateLabel(_advancedScrollablePanel, "AdvancedOutlineColorInClockPanelDropDownLabel", "Outline Color");
                    _advancedOutlineColorInClockPanelDropDownLabel.tooltip = "Set the outline color for the in-game clock panel";

                    _advancedOutlineColorInClockPanelDropDown = UIUtils.CreateDropDown(_advancedScrollablePanel, "AdvancedOutlineColorInClockPanelDropDown", _ingameAtlas);
                    _advancedOutlineColorInClockPanelDropDown.items = ModInvariables.OutlineColors;
                    _advancedOutlineColorInClockPanelDropDown.selectedIndex = ModConfig.Instance.OutlineColorInClockPanel;
                    _advancedOutlineColorInClockPanelDropDown.eventSelectedIndexChanged += (component, value) =>
                    {
                        ModConfig.Instance.OutlineColorInClockPanel = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedClockDivider = UIUtils.CreateDivider(_advancedScrollablePanel, "AdvancedClockDivider", _ingameAtlas);

                    _advancedKeymappingsTitle = UIUtils.CreateTitle(_advancedScrollablePanel, "AdvancedKeymappingsTitle", "Keymappings");
                    _advancedKeymappingsTitle.tooltip = "Advanced options for Keymappings";

                    _advancedKeymappingsEnabledCheckBox = UIUtils.CreateCheckBox(_advancedScrollablePanel, "AdvancedKeymappingsEnabledCheckBox", _ingameAtlas, "Keymappings Enabled", ModConfig.Instance.KeymappingsEnabled);
                    _advancedKeymappingsEnabledCheckBox.tooltip = "Set if keymappings should be enabled";
                    _advancedKeymappingsEnabledCheckBox.eventCheckChanged += (component, value) =>
                    {
                        ModConfig.Instance.KeymappingsEnabled = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedKeymappingsIncreaseGameSpeedDropDownLabel = UIUtils.CreateLabel(_advancedScrollablePanel, "AdvancedKeymappingsIncreaseGameSpeedDropDownLabel", "Increase Game Speed");
                    _advancedKeymappingsIncreaseGameSpeedDropDownLabel.tooltip = "Set the keymapping for increasing game speed";

                    _advancedKeymappingsIncreaseGameSpeedDropDown = UIUtils.CreateDropDown(_advancedScrollablePanel, "AdvancedKeymappingsIncreaseGameSpeedDropDown", _ingameAtlas);
                    _advancedKeymappingsIncreaseGameSpeedDropDown.items = ModInvariables.KeymappingNames;
                    _advancedKeymappingsIncreaseGameSpeedDropDown.selectedIndex = ModConfig.Instance.KeymappingsIncreaseGameSpeed;
                    _advancedKeymappingsIncreaseGameSpeedDropDown.eventSelectedIndexChanged += (component, value) =>
                    {
                        ModConfig.Instance.KeymappingsIncreaseGameSpeed = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedKeymappingsDecreaseGameSpeedDropDownLabel = UIUtils.CreateLabel(_advancedScrollablePanel, "AdvancedKeymappingsDecreaseGameSpeedDropDownLabel", "Decrease Game Speed");
                    _advancedKeymappingsDecreaseGameSpeedDropDownLabel.tooltip = "Set the keymapping for decreasing game speed";

                    _advancedKeymappingsDecreaseGameSpeedDropDown = UIUtils.CreateDropDown(_advancedScrollablePanel, "AdvancedKeymappingsDecreaseGameSpeedDropDown", _ingameAtlas);
                    _advancedKeymappingsDecreaseGameSpeedDropDown.items = ModInvariables.KeymappingNames;
                    _advancedKeymappingsDecreaseGameSpeedDropDown.selectedIndex = ModConfig.Instance.KeymappingsDecreaseGameSpeed;
                    _advancedKeymappingsDecreaseGameSpeedDropDown.eventSelectedIndexChanged += (component, value) =>
                    {
                        ModConfig.Instance.KeymappingsDecreaseGameSpeed = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedKeymappingsIncreaseDayNightSpeedDropDownLabel = UIUtils.CreateLabel(_advancedScrollablePanel, "AdvancedKeymappingsIncreaseDayNightSpeedDropDownLabel", "Increase Day/Night Speed");
                    _advancedKeymappingsIncreaseDayNightSpeedDropDownLabel.tooltip = "Set the keymapping for increasing day/night speed";

                    _advancedKeymappingsIncreaseDayNightSpeedDropDown = UIUtils.CreateDropDown(_advancedScrollablePanel, "AdvancedKeymappingsIncreaseDayNightSpeedDropDown", _ingameAtlas);
                    _advancedKeymappingsIncreaseDayNightSpeedDropDown.items = ModInvariables.KeymappingNames;
                    _advancedKeymappingsIncreaseDayNightSpeedDropDown.selectedIndex = ModConfig.Instance.KeymappingsIncreaseDayNightSpeed;
                    _advancedKeymappingsIncreaseDayNightSpeedDropDown.eventSelectedIndexChanged += (component, value) =>
                    {
                        ModConfig.Instance.KeymappingsIncreaseDayNightSpeed = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedKeymappingsDecreaseDayNightSpeedDropDownLabel = UIUtils.CreateLabel(_advancedScrollablePanel, "AdvancedKeymappingsDecreaseDayNightSpeedDropDownLabel", "Decrease Day/Night Speed");
                    _advancedKeymappingsDecreaseDayNightSpeedDropDownLabel.tooltip = "Set the keymapping for decreasing day/night speed";

                    _advancedKeymappingsDecreaseDayNightSpeedDropDown = UIUtils.CreateDropDown(_advancedScrollablePanel, "AdvancedKeymappingsDecreaseDayNightSpeedDropDown", _ingameAtlas);
                    _advancedKeymappingsDecreaseDayNightSpeedDropDown.items = ModInvariables.KeymappingNames;
                    _advancedKeymappingsDecreaseDayNightSpeedDropDown.selectedIndex = ModConfig.Instance.KeymappingsDecreaseDayNightSpeed;
                    _advancedKeymappingsDecreaseDayNightSpeedDropDown.eventSelectedIndexChanged += (component, value) =>
                    {
                        ModConfig.Instance.KeymappingsDecreaseDayNightSpeed = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedKeymappingsForwardTimeOfDayDropDownLabel = UIUtils.CreateLabel(_advancedScrollablePanel, "AdvancedKeymappingsForwardTimeOfDayDropDownLabel", "Forward Time of Day");
                    _advancedKeymappingsForwardTimeOfDayDropDownLabel.tooltip = "Set the keymapping for forwarding time of day";

                    _advancedKeymappingsForwardTimeOfDayDropDown = UIUtils.CreateDropDown(_advancedScrollablePanel, "AdvancedKeymappingsForwardTimeOfDayDropDown", _ingameAtlas);
                    _advancedKeymappingsForwardTimeOfDayDropDown.items = ModInvariables.KeymappingNames;
                    _advancedKeymappingsForwardTimeOfDayDropDown.selectedIndex = ModConfig.Instance.KeymappingsForwardTimeOfDay;
                    _advancedKeymappingsForwardTimeOfDayDropDown.eventSelectedIndexChanged += (component, value) =>
                    {
                        ModConfig.Instance.KeymappingsForwardTimeOfDay = value;
                        ModConfig.Instance.Save();
                    };

                    _advancedKeymappingsBackwardTimeOfDayDropDownLabel = UIUtils.CreateLabel(_advancedScrollablePanel, "AdvancedKeymappingsBackwardTimeOfDayDropDownLabel", "Backward Time of Day");
                    _advancedKeymappingsBackwardTimeOfDayDropDownLabel.tooltip = "Set the keymapping for backwarding time of day";

                    _advancedKeymappingsBackwardTimeOfDayDropDown = UIUtils.CreateDropDown(_advancedScrollablePanel, "AdvancedKeymappingsDecreaseTimeOfDayDropDown", _ingameAtlas);
                    _advancedKeymappingsBackwardTimeOfDayDropDown.items = ModInvariables.KeymappingNames;
                    _advancedKeymappingsBackwardTimeOfDayDropDown.selectedIndex = ModConfig.Instance.KeymappingsBackwardTimeOfDay;
                    _advancedKeymappingsBackwardTimeOfDayDropDown.eventSelectedIndexChanged += (component, value) =>
                    {
                        ModConfig.Instance.KeymappingsBackwardTimeOfDay = value;
                        ModConfig.Instance.Save();
                    };

                    if (_zoomButton != null)
                    {
                        _zoomButton.eventMouseUp += (component, eventParam) =>
                        {
                            if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                            {
                                Vector2 position;
                                Vector2 center;
                                float angle;
                                float dayTimeHour;
                                float timeOfDay;

                                component.GetHitPosition(eventParam.ray, out position);
                                center = new Vector2(30, 30);
                                angle = Vector2.Angle(new Vector3(0, 30), position - center);

                                if (position.x > center.x)
                                {
                                    angle = 360.0f - angle;
                                }

                                dayTimeHour = angle * 12.0f / 180.0f;
                                timeOfDay = SimulationManager.Lagrange4(dayTimeHour, 0f, SimulationManager.SUNRISE_HOUR, SimulationManager.SUNSET_HOUR, 24f, 0f, 6f, 18f, 24f);

                                _worldDayTimeHourSlider.value = dayTimeHour + (dayTimeHour - timeOfDay);
                            }
                        };
                    }
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
                if (_worldGameSpeedSlider.value != GameManager.Instance.GameSpeed)
                {
                    _worldGameSpeedSlider.value = GameManager.Instance.GameSpeed;
                }
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
                if (_worldDayNightSpeedSlider.value != DayNightManager.Instance.DayNightSpeed)
                {
                    _worldDayNightSpeedSlider.value = DayNightManager.Instance.DayNightSpeed;
                }
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
                if (_worldDayTimeHourSlider.value != DayNightManager.Instance.DayTimeHour)
                {
                    _worldDayTimeHourSlider.value = DayNightManager.Instance.DayTimeHour;
                }
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
                _weatherRainIntensitySlider.value = Singleton<WeatherManager>.instance.m_currentRain;
                _weatherFogIntensitySlider.value = Singleton<WeatherManager>.instance.m_currentFog;
                _weatherCloudIntensitySlider.value = Singleton<WeatherManager>.instance.m_currentCloud;
                _weatherNorthernLightsIntensitySlider.value = Singleton<WeatherManager>.instance.m_currentNorthernLights;
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
                    Singleton<WeatherManager>.instance.m_currentRain = ModConfig.Instance.RainIntensity;
                    Singleton<WeatherManager>.instance.m_targetRain = ModConfig.Instance.RainIntensity;
                }

                if (ModConfig.Instance.LockFogIntensity)
                {
                    Singleton<WeatherManager>.instance.m_currentFog = ModConfig.Instance.FogIntensity;
                    Singleton<WeatherManager>.instance.m_targetFog = ModConfig.Instance.FogIntensity;
                }

                if (ModConfig.Instance.LockCloudIntensity)
                {
                    Singleton<WeatherManager>.instance.m_currentCloud = ModConfig.Instance.CloudIntensity;
                    Singleton<WeatherManager>.instance.m_targetCloud = ModConfig.Instance.CloudIntensity;
                }

                if (ModConfig.Instance.LockNorthernLightsIntensity)
                {
                    Singleton<WeatherManager>.instance.m_currentNorthernLights = ModConfig.Instance.NorthernLightsIntensity;
                    Singleton<WeatherManager>.instance.m_targetNorthernLights = ModConfig.Instance.NorthernLightsIntensity;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] MainPanel:UpdateWeather -> Exception: " + e.Message);
            }
        }
    }
}
