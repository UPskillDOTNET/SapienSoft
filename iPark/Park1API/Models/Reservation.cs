using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Park1API.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int SlotId { get; set; }
        public Slot Slot { get; set; }
        public int UserId {get; set; }
        public User User { get; set; }
    }
}
