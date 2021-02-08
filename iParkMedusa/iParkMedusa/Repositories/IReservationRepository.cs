using iParkMedusa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParkMedusa.Repositories
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<int> DeleteReservationByIdAsync(int id);
    }
}
