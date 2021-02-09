using iParkMedusa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Services.ParkingLot
{
    interface ParkAPIService : IParkingLotService
    {
        public async Task<List<ReservationDTO>> GetAvailableSlots(DateTime start, DateTime end)
        {

        }
    }
}
