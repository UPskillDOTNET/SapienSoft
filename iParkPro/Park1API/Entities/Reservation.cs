using PublicParkAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal Value { get; set; }

        public int SlotId { get; set; }
        [ForeignKey("SlotId")]
        public Slot Slot { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
