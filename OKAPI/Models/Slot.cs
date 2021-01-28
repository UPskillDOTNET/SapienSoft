using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OKAPI.Models
{
    public class Slot
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(3, ErrorMessage = "It can only be 3 characters long.")]
        public string Locator { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal PricePerHour { get; set; }
        public string Status { get; set; }
    }
}
