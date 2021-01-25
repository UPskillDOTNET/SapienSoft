using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotAPI.Models
{
    public class Slot
    {
        public int Id { get; set; }
        public int Place { get; set; }
        public string Row { get; set; }
        public int Floor { get; set; }
        public string Availability { get; set; }
        public double Price { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsEletricChargeAvailable { get; set; } = false;

        public string CoordinatesToString()
        {
            return String.Format("{0},{1}", this.Latitude, this.Longitude);
        }
    }
}
