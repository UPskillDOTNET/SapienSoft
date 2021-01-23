using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPark.Models
{
    public class Park
    {
        public int ParkId { get; set; }
        public string ParkUrl { get; set; }
        public string Name { get; set; }
        public double GeoLongitude { get; set; }
        public double GeoLatitude { get; set; }
    }
}
