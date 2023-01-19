namespace PlayIt.Helpers
{
    public static class SpeedHelper
    {
        public static string FormatGameSpeed(bool inPercentages, float value)
        {
            if (value == 1f)
            {
                return inPercentages ? "100%" : "Normal";
            }
            else
            {
                return inPercentages ? value * 100 + "%" : value.ToString() + "x";
            }
        }

        public static string FormatDayNightSpeed(bool inPercentages, float value)
        {
            if (value == -1f)
            {
                return inPercentages ? "0%" : "Paused";
            }
            else if (value == 0f)
            {
                return inPercentages ? "100%" : "Normal";
            }
            else
            {
                return inPercentages ? (value + 1) * 100 + "%" : (value + 1).ToString() + "x";
            }
        }
    }
}
