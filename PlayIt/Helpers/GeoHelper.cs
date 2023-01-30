using UnityEngine;

namespace PlayIt.Helpers
{
    public static class GeoHelper
    {
        public static string FormatDegree(bool useSexagesimalConvention, float value)
        {
            if (useSexagesimalConvention)
            {
                float degree = Mathf.Floor(value);
                float minutes = (value - Mathf.Floor(value)) * 60.0f;
                float seconds = (minutes - Mathf.Floor(minutes)) * 60.0f;
                
                return string.Format("{0}° {1:00}' {2:00}\"", degree, minutes, seconds);
            }
            else
            {
                return string.Format("{0}°", value);
            }
        }
    }
}
