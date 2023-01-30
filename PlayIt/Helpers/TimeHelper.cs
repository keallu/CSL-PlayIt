using System;
using UnityEngine;

namespace PlayIt.Helpers
{
    public static class TimeHelper
    {
        public static string FormatTimeOfDay(bool useTwelweHourConvention, float dayTimeHour)
        {
            float timeOfDay = SimulationManager.Lagrange4(dayTimeHour, 0f, SimulationManager.SUNRISE_HOUR, SimulationManager.SUNSET_HOUR, 24f, 0f, 6f, 18f, 24f);

            int hour = (int)Mathf.Floor(timeOfDay);
            int minute = (int)Mathf.Floor((timeOfDay - hour) * 60.0f);

            DateTime dateTime = DateTime.Parse(string.Format("{0,2:00}:{1,2:00}", hour, minute));

            return useTwelweHourConvention ? dateTime.ToString("hh:mm tt") : dateTime.ToString("HH:mm");
        }
    }
}
