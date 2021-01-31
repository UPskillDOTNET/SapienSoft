using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Park2API.Entities
{
    public class Slot
    {
        public string Id { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        [Range(0.0000, 999.9999)]
        public decimal PricePerHour { get; set; }

        public string StatusId { get; set; }

        [ForeignKey("StatusId")]
        public string Status { get; set; }
    }
}
