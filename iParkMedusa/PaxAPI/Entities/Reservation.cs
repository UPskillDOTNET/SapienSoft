using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaxAPI.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime DateCreated { get; set; }
        public double Value { get; set; }
        public int SlotId { get; set; }
        [ForeignKey("SlotId")]
        public Slot Slot { get; set; }
        public bool ChargingOption { get; set; }
        public bool ValletOption { get; set; }
        public bool CoverOption { get; set; }
        public bool OutsideOption { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}