using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Park1API.Entities
{
    public class Slot
    {
        public int Id { get; set; }
        public string Locator { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal PricePerHour { get; set; }
        public Status Status { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsChargingAvailable { get; set; }
    }
}
