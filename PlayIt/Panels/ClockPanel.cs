using ColossalFramework.UI;
using PlayIt.Helpers;
using PlayIt.Managers;
using System;
using UnityEngine;

namespace PlayIt.Panels
{
    public class ClockPanel : UIPanel
    {
        private bool _initialized;
        private float _timer;

        private MainPanel _mainPanel;

        private UILabel _timeofDayLabel;
        private UILabel _gameSpeedLabel;
        private UILabel _dayNightSpeedLabel;
        private UILabel _latitudeLabel;
        private UILabel _longitudeLabel;

        public override void Awake()
        {
            base.Awake();

            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ClockPanel:Awake -> Exception: " + e.Message);
            }
        }

        public override void Start()
        {
            base.Start();

            try
            {
                if (_mainPanel == null)
                {
                    _mainPanel = GameObject.Find("PlayItMainPanel")?.GetComponent<MainPanel>();
                }

                if (ModConfig.Instance.ClockPositionX == 0f && ModConfig.Instance.ClockPositionY == 0f)
                {
                    ModProperties.Instance.ResetClockPosition();
                }

                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ClockPanel:Start -> Exception: " + e.Message);
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
                        RefreshTimeOfDay();

                        if (ModConfig.Instance.ShowSpeedInClockPanel)
                        {
                            RefreshGameSpeed();
                            RefreshDayNightSpeed();
                        }

                        if (ModConfig.Instance.ShowLatitudeAndLongitudeInClockPanel)
                        {
                            RefreshLatitude();
                            RefreshLongitude();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ClockPanel:Update -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            try
            {
                DestroyGameObject(_timeofDayLabel);
                DestroyGameObject(_gameSpeedLabel);
                DestroyGameObject(_dayNightSpeedLabel);
                DestroyGameObject(_latitudeLabel);
                DestroyGameObject(_longitudeLabel);
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ClockPanel:OnDestroy -> Exception: " + e.Message);
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
                name = "PlayItClockPanel";
                zOrder = 25;
                isVisible = false;
                width = 160f;
                height = 65f;

                eventMouseMove += (component, eventParam) =>
                {
                    if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                    {
                        var ratio = UIView.GetAView().ratio;
                        component.position = new Vector3(component.position.x + (eventParam.moveDelta.x * ratio), component.position.y + (eventParam.moveDelta.y * ratio), component.position.z);

                        ModConfig.Instance.ClockPositionX = component.absolutePosition.x;
                        ModConfig.Instance.ClockPositionY = component.absolutePosition.y;
                        ModConfig.Instance.Save();
                    }
                };
                eventDoubleClick += (component, eventParam) =>
                {
                    if (DayNightManager.Instance.IsNightTime() || DayNightManager.Instance.DayTimeHour == 0f)
                    {
                        _mainPanel.ForceDayTimeHour(12f);
                    }
                    else
                    {
                        _mainPanel.ForceDayTimeHour(0f);
                    }
                };

                _timeofDayLabel = UIUtils.CreateLabel(this, "TimeofDayLabel", "11:07");
                _timeofDayLabel.textScale = 2f;
                _timeofDayLabel.textAlignment = UIHorizontalAlignment.Center;
                _timeofDayLabel.width = 160f;
                _timeofDayLabel.height = 35f;
                _timeofDayLabel.relativePosition = new Vector3((width - _timeofDayLabel.width) / 2f, 0f);

                _gameSpeedLabel = UIUtils.CreateLabel(this, "GameSpeedLabel", "Normal");
                _gameSpeedLabel.textScale = 0.75f;
                _gameSpeedLabel.textAlignment = UIHorizontalAlignment.Right;
                _gameSpeedLabel.width = 75f;
                _gameSpeedLabel.height = 15f;
                _gameSpeedLabel.relativePosition = new Vector3(0f, height - 30f);

                _dayNightSpeedLabel = UIUtils.CreateLabel(this, "DayNightSpeedLabel", "Normal");
                _dayNightSpeedLabel.textScale = 0.75f;
                _dayNightSpeedLabel.textAlignment = UIHorizontalAlignment.Left;
                _dayNightSpeedLabel.width = 75f;
                _dayNightSpeedLabel.height = 15f;
                _dayNightSpeedLabel.relativePosition = new Vector3(85f, height - 30f);

                _latitudeLabel = UIUtils.CreateLabel(this, "LatitudeLabel", "0°");
                _latitudeLabel.textScale = 0.75f;
                _latitudeLabel.textAlignment = UIHorizontalAlignment.Right;
                _latitudeLabel.width = 75f;
                _latitudeLabel.height = 15f;
                _latitudeLabel.relativePosition = new Vector3(0f, height - 15f);

                _longitudeLabel = UIUtils.CreateLabel(this, "LongitudeLabel", "0°");
                _longitudeLabel.textScale = 0.75f;
                _longitudeLabel.textAlignment = UIHorizontalAlignment.Left;
                _longitudeLabel.width = 75f;
                _longitudeLabel.height = 15f;
                _longitudeLabel.relativePosition = new Vector3(85f, height - 15f);
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ClockPanel:CreateUI -> Exception: " + e.Message);
            }
        }

        private void UpdateUI()
        {
            try
            {
                isVisible = ModConfig.Instance.ShowClock;
                absolutePosition = new Vector3(ModConfig.Instance.ClockPositionX, ModConfig.Instance.ClockPositionY);
                _gameSpeedLabel.isVisible = ModConfig.Instance.ShowSpeedInClockPanel;
                _dayNightSpeedLabel.isVisible = ModConfig.Instance.ShowSpeedInClockPanel;
                _latitudeLabel.isVisible = ModConfig.Instance.ShowLatitudeAndLongitudeInClockPanel;
                _longitudeLabel.isVisible = ModConfig.Instance.ShowLatitudeAndLongitudeInClockPanel;

                Color32 color = GetColor(ModConfig.Instance.TextColorInClockPanel);
                _timeofDayLabel.textColor = color;
                _gameSpeedLabel.textColor = color;
                _dayNightSpeedLabel.textColor = color;
                _latitudeLabel.textColor = color;
                _longitudeLabel.textColor = color;

                _timeofDayLabel.useOutline = ModConfig.Instance.UseOutlineInClockPanel;
                _gameSpeedLabel.useOutline = ModConfig.Instance.UseOutlineInClockPanel;
                _dayNightSpeedLabel.useOutline = ModConfig.Instance.UseOutlineInClockPanel;
                _latitudeLabel.useOutline = ModConfig.Instance.UseOutlineInClockPanel;
                _longitudeLabel.useOutline = ModConfig.Instance.UseOutlineInClockPanel;

                color = GetColor(ModConfig.Instance.OutlineColorInClockPanel);
                _timeofDayLabel.outlineColor = color;
                _gameSpeedLabel.outlineColor = color;
                _dayNightSpeedLabel.outlineColor = color;
                _latitudeLabel.outlineColor = color;
                _longitudeLabel.outlineColor = color;
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ClockPanel:UpdateUI -> Exception: " + e.Message);
            }
        }

        private void RefreshTimeOfDay()
        {
            try
            {
                _timeofDayLabel.text = TimeHelper.FormatTimeOfDay(ModConfig.Instance.TimeConvention == 0, DayNightManager.Instance.DayTimeHour);
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ClockPanel:RefreshTimeOfDay -> Exception: " + e.Message);
            }
        }

        private void RefreshGameSpeed()
        {
            try
            {
                _gameSpeedLabel.text = SpeedHelper.FormatGameSpeed(ModConfig.Instance.ShowSpeedInPercentages, GameManager.Instance.GameSpeed);
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ClockPanel:RefreshGameSpeed -> Exception: " + e.Message);
            }
        }

        private void RefreshDayNightSpeed()
        {
            try
            {
                _dayNightSpeedLabel.text = SpeedHelper.FormatDayNightSpeed(ModConfig.Instance.ShowSpeedInPercentages, ModConfig.Instance.SeparateDayNightSpeed
                    ? DayNightManager.Instance.IsNightTime() ? DayNightManager.Instance.NightSpeed : DayNightManager.Instance.DaySpeed
                    : DayNightManager.Instance.DayNightSpeed);
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ClockPanel:RefreshDayNightSpeed -> Exception: " + e.Message);
            }
        }

        private void RefreshLatitude()
        {
            try
            {
                _latitudeLabel.text = GeoHelper.FormatDegree(ModConfig.Instance.DegreeConvention == 0, DayNightManager.Instance.Latitude);
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ClockPanel:RefreshLatitude -> Exception: " + e.Message);
            }
        }

        private void RefreshLongitude()
        {
            try
            {
                _longitudeLabel.text = GeoHelper.FormatDegree(ModConfig.Instance.DegreeConvention == 0, DayNightManager.Instance.Longitude);
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ClockPanel:RefreshLongitude -> Exception: " + e.Message);
            }
        }

        private Color32 GetColor(int color)
        {
            try
            {
                switch (color)
                {
                    case 0:
                        return new Color32(255, 255, 255, 255);
                    case 1:
                        return new Color32(0, 0, 0, 255);
                    case 2:
                        return new Color32(255, 0, 0, 255);
                    case 3:
                        return new Color32(0, 255, 0, 255);
                    case 4:
                        return new Color32(0, 0, 255, 255);
                    default:
                        return new Color32(255, 255, 255, 255);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ClockPanel:GetColor -> Exception: " + e.Message);
                return new Color32(255, 255, 255, 255);
            }
        }
    }
}
