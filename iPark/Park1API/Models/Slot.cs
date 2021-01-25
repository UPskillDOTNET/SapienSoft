using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Park1API.Models
{
    public class Slot
    {
        public int Id { get; set; }
        public string Locator { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal PricePerHour { get; set; }
        public string Status { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
