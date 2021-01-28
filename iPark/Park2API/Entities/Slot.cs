using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Park2API.Entities
{
    public class Slot
    {
        public int Id { get; set; }
        public string Locator { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal PricePerHour { get; set; }
        public string Status { get; set; }
    }
}
