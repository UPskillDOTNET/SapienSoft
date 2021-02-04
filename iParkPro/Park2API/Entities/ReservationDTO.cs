using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Park2API.Entities
{
    public class ReservationDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal Value { get; set; }

        public int SlotId { get; set; }
        public string Locator { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool ECharging { get; set; }
        public string UserId { get; set; }
    }
}
