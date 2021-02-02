using iParkCentralAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iParkCentralAPI.Entities
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
        [Required]
        public int Locator { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsChargingAvailable { get; set; }
        public string QRcode { get; set; }
        public string PaymentMethod { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int ParkId { get; set; }
        [ForeignKey("ParkId")]
        public Park Park { get; set; }
    }

}
