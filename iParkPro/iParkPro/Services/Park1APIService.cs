using iParkPro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkPro.Services
{
    public class Park1APIService : IParkService
    {
        public async Task<List<Reservation>> GetAvailable(DateTime start, DateTime end)
        {
            List<Reservation> list = new List<Reservation>();
            return list;
        }

        public async Task<Reservation> PostReservation(DateTime start, DateTime end)
        {
            Reservation reservation = new Reservation();
            return reservation;
        }

        public async Task<Reservation> CancelReservation(DateTime start, DateTime end)
        {
            Reservation reservation = new Reservation();
            return reservation;
        }
    }
}
