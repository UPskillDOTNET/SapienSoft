using System;

namespace PaxAPI.Entities
{
    public class ReservationDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime DateCreated { get; set; }
        public double Value { get; set; }
        public int SlotId { get; set; }
        public string Locator { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public bool ChargingOption { get; set; }
        public bool ValletOption { get; set; }
        public bool CoverOption { get; set; }
        public bool OutsideOption { get; set; }
        public string UserId { get; set; }
        public int ExternalId { get; set; }
    }
}
