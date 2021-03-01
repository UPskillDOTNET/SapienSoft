using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MammothWeb.Models
{
    public class Reservation
    {
        public DateTime DateCreated { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string ParkName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Value { get; set; }
        public string Locator { get; set; }
        public int SlotId { get; set; }
        public string UserId { get; set; }
    }
}
