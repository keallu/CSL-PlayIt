using ColossalFramework.UI;
using System;
using System.Reflection;
using UnityEngine;

namespace PlayIt
{
    public static class ModUtils
    {
        public static bool GetDayNightCycleInOptionsGameplayPanel()
        {
            try
            {
                OptionsGameplayPanel _optionsGameplayPanel = GameObject.Find("Gameplay")?.GetComponent<OptionsGameplayPanel>();

                if (_optionsGameplayPanel != null)
                {
                    UICheckBox dayNightCheckBox = _optionsGameplayPanel.GetType().GetField("m_EnableDayNightCheckBox", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(_optionsGameplayPanel) as UICheckBox;

                    return dayNightCheckBox.isChecked;
                }

                return false;
            }
            catch (Exception e)
            {
                Debug.Log("[Render It!] ModUtils:GetDayNightCycleInOptionsGameplayPanel -> Exception: " + e.Message);
                return false;
            }
        }

        public static void SetDayNightCycleInOptionsGameplayPanel(bool enabled)
        {
            try
            {
                OptionsGameplayPanel _optionsGameplayPanel = GameObject.Find("Gameplay")?.GetComponent<OptionsGameplayPanel>();

                if (_optionsGameplayPanel != null)
                {
                    UICheckBox dayNightCheckBox = _optionsGameplayPanel.GetType().GetField("m_EnableDayNightCheckBox", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(_optionsGameplayPanel) as UICheckBox;

                    dayNightCheckBox.isChecked = enabled;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Render It!] ModUtils:SetDayNightCycleInOptionsGameplayPanel -> Exception: " + e.Message);
            }
        }

        public static bool GetDynamicWeatherInOptionsGameplayPanel()
        {
            try
            {
                OptionsGameplayPanel _optionsGameplayPanel = GameObject.Find("Gameplay")?.GetComponent<OptionsGameplayPanel>();

                if (_optionsGameplayPanel != null)
                {
                    UICheckBox dynamicWeatherCheckBox = _optionsGameplayPanel.GetType().GetField("m_EnableWeatherCheckBox", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(_optionsGameplayPanel) as UICheckBox;

                    return dynamicWeatherCheckBox.isChecked;
                }

                return false;
            }
            catch (Exception e)
            {
                Debug.Log("[Render It!] ModUtils:GetDynamicWeatherInOptionsGameplayPanel -> Exception: " + e.Message);
                return false;
            }
        }

        public static void SetDynamicWeatherInOptionsGameplayPanel(bool enabled)
        {
            try
            {
                OptionsGameplayPanel _optionsGameplayPanel = GameObject.Find("Gameplay")?.GetComponent<OptionsGameplayPanel>();

                if (_optionsGameplayPanel != null)
                {
                    UICheckBox dynamicWeatherCheckBox = _optionsGameplayPanel.GetType().GetField("m_EnableWeatherCheckBox", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(_optionsGameplayPanel) as UICheckBox;

                    dynamicWeatherCheckBox.isChecked = enabled;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Render It!] ModUtils:SetDynamicWeatherInOptionsGameplayPanel -> Exception: " + e.Message);
            }
        }
    }
}
