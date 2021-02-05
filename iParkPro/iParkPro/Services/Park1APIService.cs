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
    }
}
