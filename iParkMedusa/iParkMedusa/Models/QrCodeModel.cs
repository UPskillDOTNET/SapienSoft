using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Models
{
    public class QrCodeModel
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string QrCode { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string ParkName { get; set; }
    }
}
