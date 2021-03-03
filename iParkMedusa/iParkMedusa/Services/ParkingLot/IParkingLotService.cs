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

        Task<ReservationDTO> PostReservation(DateTime start, DateTime end, string uri, int slotId);

        Task<string> CancelReservation(Park park, int id);
    }
}
