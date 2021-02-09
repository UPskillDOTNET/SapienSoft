using iParkMedusa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Services.ParkingLot
{
    public interface IParkingLotService
    {
        Task<string> GetToken(string email, string password);
    }
}
