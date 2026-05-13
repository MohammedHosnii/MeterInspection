namespace MeterInspectionAPI
{
    public static class AppState
    {
        public static bool IsOnline { get; set; }

        public static DateTime LastCheck { get; set; }
            = DateTime.MinValue;
    }
}
