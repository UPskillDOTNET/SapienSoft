using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iParkCentralAPI.Entities
{
    public class Park
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Required]
        public string Email { get; set; }
        public string BaseUrl { get; set; }
        public bool IsCovered { get; set; }
        public bool IsChargingAvailable { get; set; }
        public string ParkUrl(DateTime start, DateTime end)
        {
            var parkUrl = "api/Reservations/Available/"+start.ToString()+"/"+end.ToString();

            return parkUrl;
        }
    }
}
