using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Park1API.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal Value { get; set; }
        

        public int SlotId { get; set; }

        [ForeignKey("SlotId")]
        public Slot Slot { get; set; }
    }
}
