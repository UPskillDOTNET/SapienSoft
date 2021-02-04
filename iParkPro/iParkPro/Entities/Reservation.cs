using iParkPro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iParkPro.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }

        public int ParkId { get; set; }
        [ForeignKey("ParkId")]
        public Park Park { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
