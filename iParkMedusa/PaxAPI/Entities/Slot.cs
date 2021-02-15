using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaxAPI.Entities
{
    public class Slot
    {
        public int Id { get; set; }
        public string Locator { get; set; }
        public double PricePerHour { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        public bool IsChargingAvailable { get; set; }
        public bool HasVallet { get; set; }
        public bool IsCovered { get; set; }
        public bool IsOutside { get; set; }
        public bool SocialDistanceFlag { get; set; }
        public bool IsCovidFreeCertified { get; set; }
        public List<VehicleType> VehicleType { get; set; }
        public bool PrioritySlot { get; set; } //deficientes, grávidas, ambulâncias, bombeiros
    }
}
