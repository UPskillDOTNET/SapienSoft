using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Park1API.Entities
{
    public static class Globals
    {
        public static string ParkName { get; } = "Park 1";
        public static double Latitute { get; } = 41.17641163221141;
        public static double Longitude { get; } = -8.605596262580926;
        public static TimeSpan CancelationPolicy { get; } = new TimeSpan(48,0,0);
    }
}
