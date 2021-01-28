using Park2API.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Park2API.Entities;
using System.Collections.Generic;

namespace Park2API.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal Value { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public int SlotId { get; set; }

        [ForeignKey("SlotId")]
        public Slot Slot { get; set; }
    }
}
