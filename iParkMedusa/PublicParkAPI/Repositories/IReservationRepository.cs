using PaxAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaxAPI.Repositories
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<List<Reservation>> GetReservationsBySlotId(int slotId);
        Task<int> DeleteReservationByIdAsync(int id);
    }
}
