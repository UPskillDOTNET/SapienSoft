using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateRange DateRangeBooking { get; set; }
        public decimal Value { get; set; }
        public BookingStatus Status { get; set; }

        public int GuestId { get; set; }
        public int ParkId { get; set; }
    }
}

