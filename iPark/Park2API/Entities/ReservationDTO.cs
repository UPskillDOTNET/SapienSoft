using System;

namespace Park2API.Entities
{
    public class ReservationDTO
    {
        public string SlotId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Value { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
