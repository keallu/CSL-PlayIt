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
                height = 50f;

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
                    if (DayNightManager.Instance.IsNightTime())
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
                _timeofDayLabel.relativePosition = new Vector3((width - _timeofDayLabel.width) / 2f, (height - _timeofDayLabel.height) / 2f);
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
    }
}
