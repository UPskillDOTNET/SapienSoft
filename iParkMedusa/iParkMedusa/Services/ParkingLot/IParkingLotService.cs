using iParkMedusa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace iParkMedusa.Services.ParkingLot
{
    public interface IParkingLotService
    {
        Task<List<ReservationDTO>> GetAvailable(DateTime start, DateTime end);

        Task<ActionResult<ReservationDTO>> PostReservation(DateTime start, DateTime end, int slotId);
    }
}
