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
                height = 70f;

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
                _timeofDayLabel.width = 144f;
                _timeofDayLabel.height = 42f;
                _timeofDayLabel.relativePosition = new Vector3((width - _timeofDayLabel.width) / 2f, (height - _timeofDayLabel.height) / 4f);

                _gameSpeedLabel = UIUtils.CreateLabel(this, "GameSpeedLabel", "1x");
                _gameSpeedLabel.textScale = 0.75f;
                _gameSpeedLabel.textAlignment = UIHorizontalAlignment.Center;
                _gameSpeedLabel.width = 72f;
                _gameSpeedLabel.height = 21f;
                _gameSpeedLabel.relativePosition = new Vector3((width - _gameSpeedLabel.width) * 0.2f, height - 29f);

                _dayNightSpeedLabel = UIUtils.CreateLabel(this, "DayNightSpeedLabel", "1x");
                _dayNightSpeedLabel.textScale = 0.75f;
                _dayNightSpeedLabel.textAlignment = UIHorizontalAlignment.Center;

                _dayNightSpeedLabel.width = 72f;
                _dayNightSpeedLabel.height = 21f;
                _dayNightSpeedLabel.relativePosition = new Vector3((width - _dayNightSpeedLabel.width) * 0.8f, height - 29f);
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

                Color32 color = GetColor(ModConfig.Instance.TextColorInClockPanel);
                _timeofDayLabel.textColor = color;
                _gameSpeedLabel.textColor = color;
                _dayNightSpeedLabel.textColor = color;

                _timeofDayLabel.useOutline = ModConfig.Instance.UseOutlineInClockPanel;
                _gameSpeedLabel.useOutline = ModConfig.Instance.UseOutlineInClockPanel;
                _dayNightSpeedLabel.useOutline = ModConfig.Instance.UseOutlineInClockPanel;

                color = GetColor(ModConfig.Instance.OutlineColorInClockPanel);
                _timeofDayLabel.outlineColor = color;
                _gameSpeedLabel.outlineColor = color;
                _dayNightSpeedLabel.outlineColor = color;
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
                _gameSpeedLabel.text = SpeedHelper.FormatGameSpeed(ModConfig.Instance.ShowSpeedInPercentages, ModConfig.Instance.GameSpeed);
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
                _dayNightSpeedLabel.text = SpeedHelper.FormatDayNightSpeed(ModConfig.Instance.ShowSpeedInPercentages, ModConfig.Instance.DayNightSpeed);
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ClockPanel:RefreshDayNightSpeed -> Exception: " + e.Message);
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
