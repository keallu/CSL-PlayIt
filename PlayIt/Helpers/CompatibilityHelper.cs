namespace PlayIt.Helpers
{
    public static class CompatibilityHelper
    {
        public static readonly string[] LATITUDE_AND_LONGITUDE_MANIPULATING_MODS = { "thememixer" };

        public static bool IsAnyLatitudeAndLongitudeManipulatingModsEnabled()
        {
            if (ModUtils.IsAnyModsEnabled(LATITUDE_AND_LONGITUDE_MANIPULATING_MODS))
            {
                return true;
            }

            return false;
        }
    }
}
