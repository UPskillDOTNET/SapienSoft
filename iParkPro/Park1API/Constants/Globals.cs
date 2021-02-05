using System;

namespace Park1API.Entities
{
    public static class Globals
    {
        public static string ParkName { get; } = "Park 1";
        public static TimeSpan CancelationPolicy { get; } = new TimeSpan(48,0,0);
    }
}
