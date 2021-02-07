using ParkAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkAPI.Repositories
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<List<Reservation>> GetReservationsBySlotId(int slotId);
        Task<int> DeleteReservationByIdAsync(int id);
    }
}
