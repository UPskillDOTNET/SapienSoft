using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotAPI.Models
{
    public class DateRange
    {
        public static readonly DateTime MinValue = new DateTime(1900, 1, 1);

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public DateRange(DateTime checkInDate, DateTime checkOutDate)
        {
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }

        public static bool IsValid(DateTime checkInDate, DateTime checkOutDate)
        {
            return IsValidDate(checkInDate) && IsValidDate(checkOutDate);
        }

        private static bool IsValidDate(DateTime date)
        {
            return date > MinValue;
        }

        public override string ToString()
        {
            return $"from {CheckInDate} to {CheckOutDate}";
        }
    }
}
