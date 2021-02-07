using System.ComponentModel.DataAnnotations.Schema;

namespace ParkAPI.Entities
{
    public class Slot
    {
        public int Id { get; set; }
        public string Locator { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double PricePerHour { get; set; }

        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
    }
}
