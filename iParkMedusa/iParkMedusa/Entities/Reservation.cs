using iParkMedusa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime DateCreated { get; set; }
        public double Value { get; set; }
        public int SlotId { get; set; }
        public string Locator { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string QrCode { get; set; }
        public bool AvailableToRent { get; set; }
        public double RentValue { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int ParkId { get; set; }
        [ForeignKey("ParkId")]
        public Park Park { get; set; }
    }
}
