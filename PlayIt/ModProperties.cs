using ColossalFramework.UI;
using System;
using UnityEngine;

namespace PlayIt
{
    public class ModProperties
    {
        private static ModProperties instance;

        public static ModProperties Instance
        {
            get
            {
                return instance ?? (instance = new ModProperties());
            }
        }

        public void ResetButtonPosition()
        {
            try
            {
                int modCollectionButtonPosition = 3;

                ModConfig.Instance.ButtonPositionX = 10f;
                ModConfig.Instance.ButtonPositionY = UIView.GetAView().GetScreenResolution().y * 0.875f - (modCollectionButtonPosition * 36f) - 5f;
                ModConfig.Instance.Save();
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ModProperties:ResetButtonPosition -> Exception: " + e.Message);
            }
        }

        public void ResetPanelPosition()
        {
            try
            {
                ModConfig.Instance.PanelPositionX = 10f;
                ModConfig.Instance.PanelPositionY = 10f;
                ModConfig.Instance.Save();
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ModProperties:ResetPanelPosition -> Exception: " + e.Message);
            }
        }

        public void ResetClockPosition()
        {
            try
            {
                ModConfig.Instance.ClockPositionX = (UIView.GetAView().GetScreenResolution().x * 0.5f) - 100f;
                ModConfig.Instance.ClockPositionY = 75f;
                ModConfig.Instance.Save();
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ModProperties:ResetClockPosition -> Exception: " + e.Message);
            }
        }
    }
}