namespace PlayIt.Helpers
{
    public static class SpeedHelper
    {
        public static string FormatGameSpeed(bool usePercentage, float value)
        {
            if (value == 1f)
            {
                return usePercentage ? "100%" : "Normal";
            }
            else
            {
                return usePercentage ? value * 100 + "%" : value.ToString() + "x";
            }
        }

        public static string FormatDayNightSpeed(bool usePercentage, float value)
        {
            if (value == -1f)
            {
                return usePercentage ? "0%" : "Paused";
            }
            else if (value == 0f)
            {
                return usePercentage ? "100%" : "Normal";
            }
            else
            {
                return usePercentage ? (value + 1) * 100 + "%" : (value + 1).ToString() + "x";
            }
        }
    }
}
