using Park1API.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Park1API.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Park1API.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public DateTime TimeStart { get; set; }

        [Required]
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
