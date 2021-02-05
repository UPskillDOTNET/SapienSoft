using iParkPro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkPro.Services
{
    public interface IParkService
    {
        Task<List<Reservation>> GetAvailable(DateTime start, DateTime end);

        Task<Reservation> PostReservation(DateTime start, DateTime end);

        Task<Reservation> CancelReservation(DateTime start, DateTime end);
    }
}
