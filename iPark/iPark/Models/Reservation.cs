using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPark.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string SlotCode { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ParkId { get; set; }
        public Park Park { get; set; }
        public double[] GeoLocation { get; set; }
        public byte[] QrCode { get; set; }
    }
}
